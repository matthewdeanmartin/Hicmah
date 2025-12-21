using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.Runtime.Serialization;
using System.Collections;
using Ukadc.Diagnostics.Utils;

namespace Ukadc.Diagnostics
{
    
    /// <summary>
    /// The DataContract used to carry trace event data by the <see cref="ProxyTraceListener"/>.
    /// </summary>
    // TODO - Ideally this should also be able to carry a list of cached PropertyReader values
    // to allow us to support the token system in the ProxyTraceListener implementation (for use
    // by IProxyTraceService implementations that might want to pipe into other, existing Ukadc.Diagnostics
    // TraceListener implementations in a remote process).
    [Serializable]
    [DataContract]
    public class TraceEvent
    {
        /// <summary>
        /// Creates an instance of TraceEvent
        /// </summary>
        public TraceEvent()
        {
        }

        /// <summary>
        /// Creates an instance of TraceEvent
        /// </summary>
        public TraceEvent(TraceEventCache eventCache, string source, TraceEventType eventType, int id, params object[] data)
        {
            Source = source;
            Id = id;
            EventType = eventType;
            Data = data;
            if (eventCache != null)
            {
                Callstack = eventCache.Callstack;
                DateTime = eventCache.DateTime;
                ProcessId = eventCache.ProcessId;
                ThreadId = eventCache.ThreadId;
                Timestamp = eventCache.Timestamp;
            }
        }

        /// <summary>
        /// Get/Set the source
        /// </summary>
        [DataMember]
        public string Source { get; set; }

        /// <summary>
        /// Get/Set the Id
        /// </summary>
        [DataMember]
        public int Id { get; set; }

        /// <summary>
        /// Get/Set the Event Type
        /// </summary>
        [DataMember]
        public TraceEventType EventType { get; set; }

        /// <summary>
        /// Get/Set the data
        /// </summary>
        [DataMember]
        public object[] Data { get; set; }

        /// <summary>
        /// Get/Set the callstack
        /// </summary>
        [DataMember]
        public string Callstack { get; set; }

        /// <summary>
        /// Get/Set the date time
        /// </summary>
        [DataMember]
        public DateTime DateTime { get; set; }

        /// <summary>
        /// Get/Set the process Id
        /// </summary>
        [DataMember]
        public int ProcessId { get; set; }

        /// <summary>
        /// Get/Set the Thread Id
        /// </summary>
        [DataMember]
        public string ThreadId { get; set; }

        /// <summary>
        /// Get/Set the timestamp
        /// </summary>
        [DataMember]
        public long Timestamp { get; set; }

        [IgnoreDataMember]
        /// Gets the message (which is formatted form the Data property)
        public string Message
        {
            get { return StringFormatter.FormatData(Data);  }
        }
    }
}
