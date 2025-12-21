// THIS CODE AND INFORMATION ARE PROVIDED AS IS WITHOUT WARRANTY OF ANY
// KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE
// IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A
// PARTICULAR PURPOSE.
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace Ukadc.Diagnostics.Listeners
{

    /// <summary>
    /// A <see cref="TraceListener"/> that can be used to log events in memory and is useful when automatically testing 
    /// <see cref="TraceFilter"/>s etc. The events are stored on the static <see cref="TraceObjects"/> property.
    /// </summary>
    public class InMemoryTraceListener : CustomTraceListener
    {
        static InMemoryTraceListener()
        {
            TraceObjects = new Collection<InMemoryTraceObject>();
        }

        /// <summary>
        /// Construct an InMemoryTraceListener with the default name ("InMemoryTraceListener")
        /// </summary>
        public InMemoryTraceListener()
            : this("InMemoryTraceListener")
        {
        }

        /// <summary>
        /// Construct a named InMemoryTraceListener
        /// </summary>
        /// <param name="name">The name of the trace listener</param>
        public InMemoryTraceListener(string name)
            : base(name)
        {
        }

        /// <summary>
        /// Gets the collection of <see cref="InMemoryTraceObject"/>s that have been logged using any <see cref="InMemoryTraceListener"/>
        /// in this AppDomain. The collection can be cleared programatically.
        /// </summary>
        public static Collection<InMemoryTraceObject> TraceObjects
        {
            get;
            private set;
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
            TraceObjects.Add(new InMemoryTraceObject(eventCache, source, eventType, id, message));
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
            TraceObjects.Add(new InMemoryTraceObject(eventCache, source, eventType, id, null, data));
        }
    }
}