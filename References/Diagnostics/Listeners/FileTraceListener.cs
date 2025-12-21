using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.IO;
using Ukadc.Diagnostics.Utils.PropertyReaders;
using Ukadc.Diagnostics.Utils;
using System.Timers;
using System.Configuration;

namespace Ukadc.Diagnostics.Listeners
{
    /// <summary>
    /// This class implements a file based trace listener
    /// </summary>
    public class FileTraceListener : CustomTraceListener
    {
        /// <summary>
        /// Get the file path
        /// </summary>
        public string FilePath
        {
            get { return Attributes[FILE_PATH_ATTR]; }
        }

        /// <summary>
        /// Unsure what this is used for...
        /// </summary>
        public string Output
        {
            get { return Attributes[OUTPUT_ATTR]; }
        }

        /// <summary>
        /// Return the interval used to determine when stale files should be cleansed
        /// </summary>
        public TimeSpan CleanInterval
        {
            get
            {
                Init();
                return _cleanInterval;
            }
        }

        /// <summary>
        /// Return which attributes this trace listener supports
        /// </summary>
        /// <returns></returns>
        protected override string[] GetSupportedAttributes()
        {
            return new string[] { FILE_PATH_ATTR, OUTPUT_ATTR, CLEAN_INTERVAL };
        }

        /// <summary>
        /// Construct the trace listener
        /// </summary>
        public FileTraceListener()
            : base("FileTraceListener")
        {
          
        }

        private void Init()
        {
            if (!_initialised)
            {
                lock (_initlock)
                {
                    if (!_initialised)
                    {
                        IPropertyReaderFactory readerFactory = DefaultServiceLocator.GetService<IPropertyReaderFactory>();
                        _initialised = true;
                        _filePathReader = readerFactory.CreateCombinedReader(FilePath);
                        _outputReader = readerFactory.CreateCombinedReader(Output);
                        _writerCache = DefaultServiceLocator.GetService<IStreamWriterCache>();

                        try
                        {
                            SafeParseCleanInterval();
                        }
                        // need to start the timer with the default value, even if this throws
                        finally
                        {
                            Timer timer = new Timer(_cleanInterval.TotalMilliseconds);
                            timer.Elapsed += new ElapsedEventHandler(timer_Elapsed);
                            timer.Start();
                        }
                    }
                }
            }
        }

        private void timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            InternalLogger.LogInformation("FileListener: Cleaning old streams");
            _writerCache.ClearOldStreams(e.SignalTime - _cleanInterval);
        }

        private void SafeParseCleanInterval()
        {
            string cleanIntervalAttribute = Attributes[CLEAN_INTERVAL];

            if (!TimeSpan.TryParse(cleanIntervalAttribute, out _cleanInterval))
            {
                // Reset default value
                _cleanInterval = DEFAULT_CLEAN_INTERVAL;
                InternalLogger.LogInformation(string.Format(
                    Resources.CleanIntervalInvalid, 
                    cleanIntervalAttribute,
                    _cleanInterval));
            }
            if (_cleanInterval == TimeSpan.Zero)
            {
                _cleanInterval = DEFAULT_CLEAN_INTERVAL;
                InternalLogger.LogInformation(string.Format(
                    Resources.CleanIntervalCannotBeZero,
                    cleanIntervalAttribute,
                    _cleanInterval));
            }
        }


        /// <summary>
        /// This method must be overriden and forms the core logging method called by all other TraceEvent methods.
        /// </summary>
        /// <param name="eventCache">A cache of data that defines the trace event</param>
        /// <param name="source">The trace source</param>
        /// <param name="eventType">The type of event</param>
        /// <param name="id">The unique ID of the trace event</param>
        /// <param name="message">A message to be output regarding the trace event</param>
        protected override void TraceEventCore(TraceEventCache eventCache, string source, TraceEventType eventType, int id, string message)
        {
            Init();
            object path, output;
            _filePathReader.TryGetValue(out path, eventCache, source, eventType, id, message, null);
            _outputReader.TryGetValue(out output, eventCache, source, eventType, id, message, null);

            InternalWrite(path, output);
        }

        /// <summary>
        /// This method must be overriden and forms the core logging method called by all otherTraceData methods.
        /// </summary>
        /// <param name="eventCache">A cache of data that defines the trace event</param>
        /// <param name="source">The trace source</param>
        /// <param name="eventType">The type of event</param>
        /// <param name="id">The unique ID of the trace event</param>
        /// <param name="data">The data to be logged</param>
        protected override void TraceDataCore(TraceEventCache eventCache, string source, TraceEventType eventType, int id, params object[] data)
        {
            Init();
            object path, output;
            _filePathReader.TryGetValue(out path, eventCache, source, eventType, id, null, data);
            _outputReader.TryGetValue(out output, eventCache, source, eventType, id, null, data);

            InternalWrite(path, output);
        }

        private void InternalWrite(object path, object output)
        {
            // TODO - what if null - need to do some sensible checking and error throwing here!
            StreamWriter sw = _writerCache.GetStreamWriter(StringFormatter.SafeToString(path));
            sw.WriteLine(StringFormatter.SafeToString(output));
        }

        public override void Flush()
        {
            _writerCache.FlushAllStreams();
        }

        private const string FILE_PATH_ATTR = "filePath";
        private const string OUTPUT_ATTR = "output";
        private const string CLEAN_INTERVAL = "cleanInterval";

        /// <summary>
        /// The default file cleansing interval used when none is supplied
        /// </summary>
        public static readonly TimeSpan DEFAULT_CLEAN_INTERVAL = TimeSpan.FromHours(1);

        private IStreamWriterCache _writerCache;
        private TimeSpan _cleanInterval = DEFAULT_CLEAN_INTERVAL;

        private readonly object _initlock = new object();
        private bool _initialised = false;
        private PropertyReader _filePathReader;
        private PropertyReader _outputReader;
    }
}
