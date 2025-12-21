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
    /// Can compare against and get the value of the <see cref="Trace.CorrelationManager"/>'s ActivityId
    /// </summary>
    public class ActivityIdPropertyReader : PropertyReader
    {
        /// <summary>
        /// Return the type of the property - in this case a Guid
        /// </summary>
        public override Type PropertyType
        {
            get { return typeof (Guid); }
        }

        /// <summary>
        /// Return the comparitor
        /// </summary>
        public override IComparator Comparator
        {
            // Use the NumericComparator because GUID is IComparable
            get { return NumericComparator.Instance; }
        }

        /// <summary>
        /// Read the ActivityId 
        /// </summary>
        /// <param name="value">The returned ActivityId</param>
        /// <param name="cache">Unused</param>
        /// <param name="source">Unused</param>
        /// <param name="eventType">Unused</param>
        /// <param name="id">Unused</param>
        /// <param name="formatOrMessage">Unused</param>
        /// <param name="data">Unused</param>
        /// <returns>True</returns>
        [SuppressMessage("Microsoft.Security",
            "CA2122:DoNotIndirectlyExposeMethodsWithLinkDemands")]
        public override bool TryGetValue(out object value, TraceEventCache cache, string source,
                                         TraceEventType eventType, int id, string formatOrMessage, object[] data)
        {
            value = Trace.CorrelationManager.ActivityId;
            return true;
        }
    }
}