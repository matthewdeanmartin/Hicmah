// THIS CODE AND INFORMATION ARE PROVIDED AS IS WITHOUT WARRANTY OF ANY
// KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE
// IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A
// PARTICULAR PURPOSE.
using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

namespace Ukadc.Diagnostics.Utils.PropertyReaders
{
    /// <summary>
    /// A PropertyReader is used throughout the library to read data values from log events in a highly performant manner.
    /// </summary>
    public abstract class PropertyReader
    {
        /// <summary>
        /// The type of the result returned by the <see cref="PropertyReader" />
        /// </summary>
        public abstract Type PropertyType { get; }

        /// <summary>
        /// Gets the <see cref="IComparator"/> recommended by this <see cref="PropertyReader"/> when wanting
        /// to compare the value with another value.
        /// </summary>
        public abstract IComparator Comparator { get; }

        // TODO - research if there's any way of improving this signature (such as using Generics) but still support the polymorphic abstract PropertyReader 

        /// <summary>
        /// Attempts to read a value based on the parameters or some other data (such as statically available data).
        /// </summary>
        /// <param name="value">The return value, usually null if the value couldn't be read</param>
        /// <param name="cache">The <see cref="TraceEventCache"/></param>
        /// <param name="source">The source of the logging event</param>
        /// <param name="eventType">The <see cref="TraceEventType"/> of the logging event</param>
        /// <param name="id">The id of the logging event</param>
        /// <param name="formatOrMessage">The format string or complete message</param>
        /// <param name="data">The array of objects</param>
        /// <returns>True if the value could be read</returns>
        [SuppressMessage("Microsoft.Design", "CA1021:AvoidOutParameters",
            MessageId = "0#")]
        public abstract bool TryGetValue(out object value, TraceEventCache cache, string source,
                                         TraceEventType eventType, int id, string formatOrMessage, object[] data);

        /// <summary>
        /// Attempts to read a value based on the parameters or some other data (such as statically available data).
        /// </summary>
        /// <param name="value">The return value, usually null if the value couldn't be read</param>
        /// <param name="cache">The <see cref="TraceEventCache"/></param>
        /// <param name="source">The source of the logging event</param>
        /// <param name="eventType">The <see cref="TraceEventType"/> of the logging event</param>
        /// <param name="id">The id of the logging event</param>
        /// <param name="formatOrMessage">The format string or complete message</param>
        /// <param name="args">Ignored by default - override to implement</param>
        /// <param name="data1">Ignored by default - override to implement</param>
        /// <param name="data">The array of objects</param>
        /// <returns>True if the value could be read></returns>
        [SuppressMessage("Microsoft.Design", "CA1021:AvoidOutParameters",
            MessageId = "0#")]
        public virtual bool TryGetValue(out object value, TraceEventCache cache, string source, TraceEventType eventType,
                                        int id, string formatOrMessage, object[] args, object data1, object[] data)
        {
            return TryGetValue(out value, cache, source, eventType, id, formatOrMessage, data);
        }
    }
}