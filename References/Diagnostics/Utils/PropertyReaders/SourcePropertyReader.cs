// THIS CODE AND INFORMATION ARE PROVIDED AS IS WITHOUT WARRANTY OF ANY
// KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE
// IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A
// PARTICULAR PURPOSE.
using System.Diagnostics;

namespace Ukadc.Diagnostics.Utils.PropertyReaders
{
    /// <summary>
    /// Can compare against and get the value of TraceEvent's Id
    /// </summary>
    public class SourcePropertyReader : StringPropertyReader
    {
        /// <summary>
        /// Return the source value
        /// </summary>
        /// <param name="value">The return value</param>
        /// <param name="cache">Unused</param>
        /// <param name="source">Copied to the return value</param>
        /// <param name="eventType">Unused</param>
        /// <param name="id">Unused</param>
        /// <param name="formatOrMessage">Unused</param>
        /// <param name="data">Unused</param>
        /// <returns>True</returns>
        public override bool TryGetValue(out object value, TraceEventCache cache, string source,
                                         TraceEventType eventType, int id, string formatOrMessage, object[] data)
        {
            value = source;
            return true;
        }
    }
}