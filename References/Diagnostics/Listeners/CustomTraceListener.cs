// THIS CODE AND INFORMATION ARE PROVIDED AS IS WITHOUT WARRANTY OF ANY
// KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE
// IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A
// PARTICULAR PURPOSE.
using System;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.Remoting.Messaging;
using Ukadc.Diagnostics.Utils;

namespace Ukadc.Diagnostics.Listeners
{
    /// <summary>
    /// An abstract class designed to make creating a <see cref="TraceListener"/> easier with just two 
    /// methods to override (<see cref="TraceEventCore"/> and <see cref="TraceDataCore"/>).
    /// </summary>
    public abstract class CustomTraceListener : TraceListener
    {
        /// <summary>
        /// A logger used by this trace listener
        /// </summary>
        protected readonly IInternalLogger InternalLogger;

        /// <summary>
        /// Construct an instance of the trace listener
        /// </summary>
        /// <param name="name">The name of the trace listener</param>
        protected CustomTraceListener(string name)
            : base(name)
        {
            InternalLogger = DefaultServiceLocator.GetService<IInternalLogger>();
        }

        /// <summary>
        /// Determines whether a filter is attached to this listener and, if so, asks whether it ShouldTrace applies to this data.
        /// </summary>
        protected virtual bool ShouldTrace(TraceEventCache eventCache, string source, TraceEventType eventType, int id,
                                           string formatOrMessage, object[] args, object data1, object[] data)
        {
            return
                !(Filter != null &&
                  !Filter.ShouldTrace(eventCache, source, eventType, id, formatOrMessage, args, data1, data));
        }

        /// <summary>
        /// Called before the main TraceEventCore method and applies any filter by calling ShouldTrace.
        /// </summary>
        protected virtual void FilterTraceEventCore(TraceEventCache eventCache, string source, TraceEventType eventType,
                                                    int id, string message)
        {
            try
            {
                if (!ShouldTrace(eventCache, source, eventType, id, message, null, null, null))
                    return;
                TraceEventCore(eventCache, source, eventType, id, message);
            }
            catch (Exception exc)
            {
                InternalLogger.LogException(exc);
            }
        }

        /// <summary>
        /// Called before the main TraceDataCore method and applies any filter by calling ShouldTrace.
        /// </summary>
        protected virtual void FilterTraceDataCore(TraceEventCache eventCache, string source, TraceEventType eventType,
                                                   int id, params object[] data)
        {
            try
            {
                if (!ShouldTrace(eventCache, source, eventType, id, null, null, null, data))
                    return;
                TraceDataCore(eventCache, source, eventType, id, data);
            }
            catch (Exception exc)
            {
                InternalLogger.LogException(exc);
            }
        }

        /// <summary>
        /// Logs a transfer to a new ActivityId (and uses the <see cref="RelatedActivityIdStore"/> to store the 
        /// relatedActivityId in <see cref="CallContext"/> for the duration of the call)
        /// </summary>
        /// <param name="eventCache">A cache of data that defines the trace event</param>
        /// <param name="source">The trace source</param>
        /// <param name="id">The unique ID of the trace event</param>
        /// <param name="message">A message to be output regarding the trace event</param>
        /// <param name="relatedActivityId">The activityId to which we are transferring</param>
        public override void TraceTransfer(TraceEventCache eventCache, string source, int id, string message,
                                           Guid relatedActivityId)
        {
            //TODO: I don't think this code is right here - as this inserts a related activity ID, and then 
            // promptly removes it again on dispose()
            using (RelatedActivityIdStore.SetRelatedActivityId(relatedActivityId))
            {
                base.TraceTransfer(eventCache, source, id, message, relatedActivityId);
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
        protected abstract void TraceEventCore(TraceEventCache eventCache, string source, TraceEventType eventType,
                                               int id, string message);

        /// <summary>
        /// This method must be overriden and forms the core logging method called by all otherTraceData methods.
        /// </summary>
        /// <param name="eventCache">A cache of data that defines the trace event</param>
        /// <param name="source">The trace source</param>
        /// <param name="eventType">The type of event</param>
        /// <param name="id">The unique ID of the trace event</param>
        /// <param name="data">The data to be logged</param>
        protected abstract void TraceDataCore(TraceEventCache eventCache, string source, TraceEventType eventType,
                                              int id, params object[] data);

        /// <summary>
        /// Write a message to the trace listeners
        /// </summary>
        /// <param name="message">The message to write</param>
        public override void Write(string message)
        {
            FilterTraceEventCore(null, string.Empty, TraceEventType.Information, 0, message);
        }

        /// <summary>
        /// Write a message to the trace listeners
        /// </summary>
        /// <param name="message">The message to write</param>
        public override void WriteLine(string message)
        {
            Write(message);
        }

        /// <summary>
        /// Write a trace event
        /// </summary>
        /// <param name="eventCache">A cache of data that defines the trace event</param>
        /// <param name="source">The trace source</param>
        /// <param name="eventType">The type of event</param>
        /// <param name="id">The unique ID of the trace event</param>
        /// <param name="message">A message to be output regarding the trace event</param>
        public override sealed void TraceEvent(TraceEventCache eventCache, string source, TraceEventType eventType,
                                               int id, string message)
        {
            FilterTraceEventCore(eventCache, source, eventType, id, message);
        }

        /// <summary>
        /// Write a trace event
        /// </summary>
        /// <param name="eventCache">A cache of data that defines the trace event</param>
        /// <param name="source">The trace source</param>
        /// <param name="eventType">The type of event</param>
        /// <param name="id">The unique ID of the trace event</param>
        /// <param name="format">A string format specification for the trace event</param>
        /// <param name="args">Arguments used within the format specification string</param>
        public override sealed void TraceEvent(TraceEventCache eventCache, string source, TraceEventType eventType,
                                               int id, string format, params object[] args)
        {
            string message = format;
            if (args != null)
            {
                message = string.Format(CultureInfo.CurrentCulture, format, args);
            }
            FilterTraceEventCore(eventCache, source, eventType, id, message);
        }

        /// <summary>
        /// Write a trace event
        /// </summary>
        /// <param name="eventCache">A cache of data that defines the trace event</param>
        /// <param name="source">The trace source</param>
        /// <param name="eventType">The type of event</param>
        /// <param name="id">The unique ID of the trace event</param>
        public override sealed void TraceEvent(TraceEventCache eventCache, string source, TraceEventType eventType,
                                               int id)
        {
            FilterTraceEventCore(eventCache, source, eventType, id, null);
        }

        /// <summary>
        /// Write a trace event
        /// </summary>
        /// <param name="eventCache">A cache of data that defines the trace event</param>
        /// <param name="source">The trace source</param>
        /// <param name="eventType">The type of event</param>
        /// <param name="id">The unique ID of the trace event</param>
        /// <param name="data">The data to be written</param>
        public override sealed void TraceData(TraceEventCache eventCache, string source, TraceEventType eventType,
                                              int id, object data)
        {
            FilterTraceDataCore(eventCache, source, eventType, id, data);
        }

        /// <summary>
        /// Write a trace event
        /// </summary>
        /// <param name="eventCache">A cache of data that defines the trace event</param>
        /// <param name="source">The trace source</param>
        /// <param name="eventType">The type of event</param>
        /// <param name="id">The unique ID of the trace event</param>
        /// <param name="data">The data to be written</param>
        public override sealed void TraceData(TraceEventCache eventCache, string source, TraceEventType eventType,
                                              int id, params object[] data)
        {
            FilterTraceDataCore(eventCache, source, eventType, id, data);
        }
    }
}