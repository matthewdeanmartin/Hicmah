// THIS CODE AND INFORMATION ARE PROVIDED AS IS WITHOUT WARRANTY OF ANY
// KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE
// IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A
// PARTICULAR PURPOSE.
using System;
using System.Diagnostics;

namespace Ukadc.Diagnostics.Utils.PropertyReaders
{
 
    // TODO - all to be tested

    /// <summary>
    /// Read a specific part of the event date time
    /// </summary>
    public class DatePartPropertyReader : PropertyReader
    {
        /// <summary>
        /// The format string used to process the DateTime
        /// </summary>
        private readonly string _formatString;

        /// <summary>
        /// Construct the reader
        /// </summary>
        /// <param name="formatString">The format string to be used against DateTime object</param>
        public DatePartPropertyReader(string formatString)
        {
            _formatString = formatString;
        }

        /// <summary>
        /// Return the type of the property
        /// </summary>
        public override Type PropertyType
        {
            get { return typeof(string); }
        }

        /// <summary>
        /// Return the comparator for this property reader
        /// </summary>
        public override IComparator Comparator
        {
            get { return StringComparator.Instance; }
        }

        /// <summary>
        /// Read the value from the passed TraceEventCache.DateTime member
        /// </summary>
        /// <param name="value">The return value, or null if the value couldn't be read</param>
        /// <param name="cache">The <see cref="TraceEventCache"/></param>
        /// <param name="source">The source of the logging event</param>
        /// <param name="eventType">The <see cref="TraceEventType"/> of the logging event</param>
        /// <param name="id">The id of the logging event</param>
        /// <param name="formatOrMessage">The format string or complete message</param>
        /// <param name="data">The array of objects</param>
        /// <returns>True if the value could be read</returns>
        public override bool TryGetValue(out object value, TraceEventCache cache, string source, TraceEventType eventType, int id, string formatOrMessage, object[] data)
        {
            if (null == cache)
            {
                value = null;
                return false;
            }
            value = cache.DateTime.ToString(_formatString);
            return true;
        }
    }
}