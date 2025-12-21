// THIS CODE AND INFORMATION ARE PROVIDED AS IS WITHOUT WARRANTY OF ANY
// KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE
// IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A
// PARTICULAR PURPOSE.
using System;
using System.Diagnostics;

namespace Ukadc.Diagnostics.Utils.PropertyReaders
{
    /// <summary>
    /// Can compare against and get the value of TraceEvent's ProcessId
    /// </summary>
    public class ProcessIdPropertyReader : PropertyReader
    {
        /// <summary>
        /// Get the type of the property
        /// </summary>
        public override Type PropertyType
        {
            get { return typeof (int); }
        }

        /// <summary>
        /// Return the comparator
        /// </summary>
        public override IComparator Comparator
        {
            get { return NumericComparator.Instance; }
        }

        /// <summary>
        /// Return the process ID
        /// </summary>
        /// <param name="value">The process ID, or null if the TraceEventCache is null</param>
        /// <param name="cache">The trace event cache</param>
        /// <param name="source">Unused</param>
        /// <param name="eventType">Unused</param>
        /// <param name="id">Unused</param>
        /// <param name="formatOrMessage">Unused</param>
        /// <param name="data">Unused</param>
        /// <returns>True if the TraceEventCache is non-null</returns>
        public override bool TryGetValue(out object value, TraceEventCache cache, string source,
                                         TraceEventType eventType, int id, string formatOrMessage, object[] data)
        {
            if (cache == null)
            {
                value = null;
                return false;
            }

            ExtendedTraceEventCache ext = cache as ExtendedTraceEventCache;

            if (ext != null)
            {
                value = ext.ExtendedProcessId;
                return true;
            }

            value = cache.ProcessId;
            return true;
        }
    }
}