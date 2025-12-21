// THIS CODE AND INFORMATION ARE PROVIDED AS IS WITHOUT WARRANTY OF ANY
// KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE
// IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A
// PARTICULAR PURPOSE.
using System;
using System.Diagnostics;

namespace Ukadc.Diagnostics.Utils.PropertyReaders
{
    /// <summary>
    /// Can compare against and get the value of TraceEvent's TraceEventType
    /// </summary>
    public class EventTypePropertyReader : PropertyReader
    {
        /// <summary>
        /// Return the property type
        /// </summary>
        public override Type PropertyType
        {
            get { return typeof (TraceEventType); }
        }

        /// <summary>
        /// Return the comparator
        /// </summary>
        public override IComparator Comparator
        {
            get { return NumericComparator.Instance; }
        }

        /// <summary>
        /// Return the event type
        /// </summary>
        /// <param name="value">The eventType</param>
        /// <param name="cache">Unused</param>
        /// <param name="source">Unused</param>
        /// <param name="eventType">Unused</param>
        /// <param name="id">Unused</param>
        /// <param name="formatOrMessage">Unused</param>
        /// <param name="data">Unused</param>
        /// <returns>True</returns>
        public override bool TryGetValue(out object value, TraceEventCache cache, string source,
                                         TraceEventType eventType, int id, string formatOrMessage, object[] data)
        {
            value = eventType;
            return true;
        }
    }
}