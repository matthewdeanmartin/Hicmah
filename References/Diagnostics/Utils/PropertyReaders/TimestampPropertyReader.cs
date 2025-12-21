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
    /// Can compare against and get the value of the Timestamp (DateTime of the log event in ticks)
    /// </summary>
    public class TimestampPropertyReader : PropertyReader
    {
        /// <summary>
        /// Get the property type
        /// </summary>
        public override Type PropertyType
        {
            get { return typeof (long); }
        }

        /// <summary>
        /// Get the comparitor
        /// </summary>
        public override IComparator Comparator
        {
            get { return NumericComparator.Instance; }
        }

        /// <summary>
        /// Try to get the value
        /// </summary>
        /// <param name="value">The value of cache.Timestamp</param>
        /// <param name="cache">The cached event data</param>
        /// <param name="source">Unused</param>
        /// <param name="eventType">Unused</param>
        /// <param name="id">Unused</param>
        /// <param name="formatOrMessage">Unused</param>
        /// <param name="data">Unused</param>
        /// <returns>True if the cache object is non-null</returns>
        [SuppressMessage("Microsoft.Security",
            "CA2122:DoNotIndirectlyExposeMethodsWithLinkDemands")]
        public override bool TryGetValue(out object value, TraceEventCache cache, string source,
                                         TraceEventType eventType, int id, string formatOrMessage, object[] data)
        {
            if (cache == null)
            {
                value = null;
                return false;
            }

            value = cache.Timestamp;
            return true;
        }
    }
}