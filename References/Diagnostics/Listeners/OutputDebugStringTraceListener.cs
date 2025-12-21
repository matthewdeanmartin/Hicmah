// THIS CODE AND INFORMATION ARE PROVIDED AS IS WITHOUT WARRANTY OF ANY
// KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE
// IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A
// PARTICULAR PURPOSE.
using System.Diagnostics;
using Ukadc.Diagnostics.Utils;
using Ukadc.Diagnostics.Utils.PropertyReaders;

namespace Ukadc.Diagnostics.Listeners
{
    /// <summary>
    /// A TraceListener that supports formatted output and writes using OutputDebugString
    /// </summary>
    public class OutputDebugStringTraceListener : CustomTraceListener
    {
        private readonly PropertyReader _propertyReader;

        /// <summary>
        /// Creates a new <see cref="OutputDebugStringTraceListener"/> using the specified "combinedToken"
        /// </summary>
        /// <param name="combinedToken">A combinedToken (e.g. "Level: {EventType}, Id: {Id}")</param>
        public OutputDebugStringTraceListener(string combinedToken)
            : base("OutputDebugStringTraceListener")
        {
            _propertyReader = DefaultServiceLocator.GetService<IPropertyReaderFactory>().CreateCombinedReader(combinedToken);
        }

        /// <summary>
        /// This method must be overriden and forms the core logging method called by all other TraceEvent methods.
        /// </summary>
        /// <param name="eventCache">A cache of data that defines the trace event</param>
        /// <param name="source">The trace source</param>
        /// <param name="eventType">The type of event</param>
        /// <param name="id">The unique ID of the trace event</param>
        /// <param name="message">A message to be output regarding the trace event</param>
        protected override void TraceEventCore(TraceEventCache eventCache, string source, TraceEventType eventType,
                                               int id, string message)
        {
            object value;
            if (_propertyReader.TryGetValue(out value, eventCache, source, eventType, id, message, null))
            {
                SafeNativeMethods.OutputDebugString(StringFormatter.SafeToString(value));
            }
        }

        /// <summary>
        /// This method must be overriden and forms the core logging method called by all otherTraceData methods.
        /// </summary>
        /// <param name="eventCache">A cache of data that defines the trace event</param>
        /// <param name="source">The trace source</param>
        /// <param name="eventType">The type of event</param>
        /// <param name="id">The unique ID of the trace event</param>
        /// <param name="data">The data to be logged</param>
        protected override void TraceDataCore(TraceEventCache eventCache, string source, TraceEventType eventType,
                                              int id, params object[] data)
        {
            object value;
            if (_propertyReader.TryGetValue(out value, eventCache, source, eventType, id, null, data))
            {
                SafeNativeMethods.OutputDebugString(StringFormatter.SafeToString(value));
            }
        }
    }
}