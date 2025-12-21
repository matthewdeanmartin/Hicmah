// THIS CODE AND INFORMATION ARE PROVIDED AS IS WITHOUT WARRANTY OF ANY
// KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE
// IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A
// PARTICULAR PURPOSE.
using System;
using System.Diagnostics;

namespace Ukadc.Diagnostics.Utils.PropertyReaders
{
    /// <summary>
    /// Can compare against and get the value of TraceEvent's DateTime
    /// </summary>
    public class DateTimePropertyReader : PropertyReader
    {
        /// <summary>
        /// Return the type of property
        /// </summary>
        public override Type PropertyType
        {
            get { return typeof (DateTime); }
        }

        /// <summary>
        /// Return the comparator used
        /// </summary>
        public override IComparator Comparator
        {
            get { return NumericComparator.Instance; }
        }

        /// <summary>
        /// Reads the DateTime value from the passed TraceEventCache instance
        /// </summary>
        /// <param name="value">The return value, null if the TraceEventCache is null</param>
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
            if (cache == null)
            {
                value = null;
                return false;
            }

            value = cache.DateTime;
            return true;
        }
    }
}