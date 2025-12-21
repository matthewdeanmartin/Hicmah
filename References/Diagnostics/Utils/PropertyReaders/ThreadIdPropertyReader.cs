// THIS CODE AND INFORMATION ARE PROVIDED AS IS WITHOUT WARRANTY OF ANY
// KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE
// IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A
// PARTICULAR PURPOSE.
using System.Diagnostics;

namespace Ukadc.Diagnostics.Utils.PropertyReaders
{
    /// <summary>
    /// Can compare against and get the value of TraceEvent's ThreadId
    /// </summary>
    public class ThreadIdPropertyReader : StringPropertyReader
    {
        /// <summary>
        /// Get the ThreadId value from the TraceEventCache object
        /// </summary>
        /// <param name="value">The ThreadId if the TraceEventCache is not null</param>
        /// <param name="cache">The TraceEventCache from which to read the ThreadId</param>
        /// <param name="source">Unused</param>
        /// <param name="eventType">Unused</param>
        /// <param name="id">Unused</param>
        /// <param name="formatOrMessage">Unused</param>
        /// <param name="data">Unused</param>
        /// <returns>True if the cache value is not null otherwise false</returns>
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
                value = ext.ExtendedThreadId;
                return true;
            }

            value = cache.ThreadId;
            return true;
        }
    }
}