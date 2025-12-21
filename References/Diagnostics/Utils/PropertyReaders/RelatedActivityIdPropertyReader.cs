// THIS CODE AND INFORMATION ARE PROVIDED AS IS WITHOUT WARRANTY OF ANY
// KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE
// IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A
// PARTICULAR PURPOSE.
using System;
using System.Diagnostics;

namespace Ukadc.Diagnostics.Utils.PropertyReaders
{
    /// <summary>
    /// Read the related activity id
    /// </summary>
    public class RelatedActivityIdPropertyReader : PropertyReader
    {
        /// <summary>
        /// Return the property type
        /// </summary>
        public override Type PropertyType
        {
            get { return typeof (Guid); }
        }

        /// <summary>
        /// Return the comparator
        /// </summary>
        public override IComparator Comparator
        {
            get { return NumericComparator.Instance; }
        }

        /// <summary>
        /// Lookup the RelatedActivityIdStore
        /// </summary>
        /// <param name="value">The value of the RelatedActivityId property if it is defined, or null</param>
        /// <param name="cache">Unused</param>
        /// <param name="source">Unused</param>
        /// <param name="eventType">Unused</param>
        /// <param name="id">Unused</param>
        /// <param name="formatOrMessage">Unused</param>
        /// <param name="data">Unused</param>
        /// <returns>Trus if there is a Guid in the RelatedActivityId store</returns>
        public override bool TryGetValue(out object value, TraceEventCache cache, string source,
                                         TraceEventType eventType, int id, string formatOrMessage,
                                         object[] data)
        {
            Guid relatedActivityId;
            bool response = RelatedActivityIdStore.TryGetRelatedActivityId(out relatedActivityId);
            value = relatedActivityId;
            return response;
        }
    }
}