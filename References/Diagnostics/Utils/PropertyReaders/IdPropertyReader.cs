// THIS CODE AND INFORMATION ARE PROVIDED AS IS WITHOUT WARRANTY OF ANY
// KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE
// IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A
// PARTICULAR PURPOSE.
using System;
using System.Diagnostics;

namespace Ukadc.Diagnostics.Utils.PropertyReaders
{
    /// <summary>
    /// Can compare against and get the value of TraceEvent's Id
    /// </summary>
    public class IdPropertyReader : PropertyReader
    {
        /// <summary>
        /// Return the type of the property
        /// </summary>
        public override Type PropertyType
        {
            get { return typeof (int); }
        }

        /// <summary>
        /// Return the comparator used
        /// </summary>
        public override IComparator Comparator
        {
            get { return NumericComparator.Instance; }
        }

        /// <summary>
        /// Return the trace event Id
        /// </summary>
        /// <param name="value">The return value, usually null if the value couldn't be read</param>
        /// <param name="cache">The <see cref="TraceEventCache"/></param>
        /// <param name="source">The source of the logging event</param>
        /// <param name="eventType">The <see cref="TraceEventType"/> of the logging event</param>
        /// <param name="id">The id of the logging event</param>
        /// <param name="formatOrMessage">The format string or complete message</param>
        /// <param name="data">The array of objects</param>
        /// <returns>True if the value could be read</returns>
        public override bool TryGetValue(out object value, TraceEventCache cache, string source,
                                         TraceEventType eventType, int id, string formatOrMessage, object[] data)
        {
            value = id;
            return true;
        }
    }
}