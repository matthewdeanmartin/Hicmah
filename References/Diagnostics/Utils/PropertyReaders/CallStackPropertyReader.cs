// THIS CODE AND INFORMATION ARE PROVIDED AS IS WITHOUT WARRANTY OF ANY
// KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE
// IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A
// PARTICULAR PURPOSE.
using System.Diagnostics;

namespace Ukadc.Diagnostics.Utils.PropertyReaders
{
    /// <summary>
    /// Can compare against and get the value of TraceEvent's Callstack
    /// </summary>
    public class CallstackPropertyReader : StringPropertyReader
    {
        /// <inheritdoc />
        /// <summary>
        /// Check if the cache is non-null, and if so return the Callstack element of the cached event
        /// </summary>
        public override bool TryGetValue(out object value, TraceEventCache cache, string source,
                                         TraceEventType eventType, int id, string formatOrMessage, object[] data)
        {
            if (cache == null)
            {
                value = null;
                return false;
            }

            value = cache.Callstack;
            return true;
        }
    }
}