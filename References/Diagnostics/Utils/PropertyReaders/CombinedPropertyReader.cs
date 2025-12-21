// THIS CODE AND INFORMATION ARE PROVIDED AS IS WITHOUT WARRANTY OF ANY
// KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE
// IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A
// PARTICULAR PURPOSE.
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Ukadc.Diagnostics.Utils.PropertyReaders
{
    /// <summary>
    /// A <see cref="PropertyReader"/> implementation that uses multiple <see cref="PropertyReader"/>s
    /// and uses a stringBuilder to create a concatenated string output
    /// </summary>
    public class CombinedPropertyReader : StringPropertyReader
    {
        private readonly List<PropertyReader> _propertyReaders = new List<PropertyReader>();

        /// <summary>
        /// A list of property readers
        /// </summary>
        public List<PropertyReader> PropertyReaders
        {
            get { return _propertyReaders; }
        }

        /// <summary>
        /// Returns the value of the combined property readers
        /// </summary>
        /// <param name="value">The return value, usually null if the value couldn't be read</param>
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
            StringBuilder builder = new StringBuilder();
            foreach (PropertyReader reader in PropertyReaders)
            {
                object val = null;
                if (reader.TryGetValue(out val, cache, source, eventType, id, formatOrMessage, data))
                {
                    builder.Append(val);
                }
            }
            value = builder.ToString();
            return true;
        }
    }
}