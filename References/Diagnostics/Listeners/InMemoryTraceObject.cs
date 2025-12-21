// THIS CODE AND INFORMATION ARE PROVIDED AS IS WITHOUT WARRANTY OF ANY
// KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE
// IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A
// PARTICULAR PURPOSE.
using System.Diagnostics;

namespace Ukadc.Diagnostics.Listeners
{
    /// <summary>
    /// A POCO used to represent a trace event as logged by the <see cref="InMemoryTraceListener"/>
    /// </summary>
    public class InMemoryTraceObject
    {
        /// <summary>
        /// Creates a new <see cref="InMemoryTraceObject"/>.
        /// </summary>
        /// <param name="traceEventCache">The <see cref="TraceEventCache"/></param>
        /// <param name="source">The source</param>
        /// <param name="traceEventType">The <see cref="TraceEventType"/></param>
        /// <param name="id">The Id</param>
        /// <param name="message">The Message</param>
        /// <param name="data">The Data</param>
        public InMemoryTraceObject(TraceEventCache traceEventCache, string source, TraceEventType traceEventType, int id,
                                   string message, params object[] data)
        {
            TraceEventCache = traceEventCache;
            Source = source;
            TraceEventType = traceEventType;
            Id = id;
            Message = message;
            Data = data;
        }

        /// <summary>
        /// Gets or sets the <see cref="TraceEventCache"/>
        /// </summary>
        public TraceEventCache TraceEventCache
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the TraceEventType;
        /// </summary>
        public TraceEventType TraceEventType
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the Id
        /// </summary>
        public int Id
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the Source
        /// </summary>
        public string Source
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the Message
        /// </summary>
        public string Message
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the Data
        /// </summary>
        public object[] Data
        {
            get;
            set;
        }
    }
}