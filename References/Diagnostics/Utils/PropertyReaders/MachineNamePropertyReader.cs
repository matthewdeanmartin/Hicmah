// THIS CODE AND INFORMATION ARE PROVIDED AS IS WITHOUT WARRANTY OF ANY
// KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE
// IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A
// PARTICULAR PURPOSE.
using System;
using System.Diagnostics;

namespace Ukadc.Diagnostics.Utils.PropertyReaders
{
    /// <summary>
    /// A <see cref="PropertyReader"/> that gets the value of the MachineName using the <see cref="Environment.MachineName"/> property.
    /// </summary>
    public class MachineNamePropertyReader : StringPropertyReader
    {
        /// <summary>
        /// Returns the value of Environment.MachineName
        /// </summary>
        /// <param name="value">The output parameter</param>
        /// <param name="cache">Unused</param>
        /// <param name="source">Unused</param>
        /// <param name="eventType">Unused</param>
        /// <param name="id">Unused</param>
        /// <param name="formatOrMessage">Unused</param>
        /// <param name="data">Unused</param>
        /// <returns>True</returns>
        public override bool TryGetValue(out object value, TraceEventCache cache, string source,
                                         TraceEventType eventType, int id, string formatOrMessage,
                                         object[] data)
        {
            value = Environment.MachineName;
            return true;
        }
    }
}