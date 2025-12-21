// THIS CODE AND INFORMATION ARE PROVIDED AS IS WITHOUT WARRANTY OF ANY
// KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE
// IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A
// PARTICULAR PURPOSE.

using System.Configuration;
using System.Diagnostics.CodeAnalysis;

namespace Ukadc.Diagnostics.Listeners.Configuration
{
    /// <summary>
    /// Reads the sqlTraceListeners section from the config file
    /// </summary>
    [SuppressMessage("Microsoft.Design",
        "CA1010:CollectionsShouldImplementGenericInterface")]
    [ConfigurationCollection(typeof (SqlTraceListenerElement),
        AddItemName = ELEMENT_NAME,
        CollectionType = ConfigurationElementCollectionType.BasicMap)]
    public class SqlTraceListenerElementCollection : ConfigurationElementCollection
    {
        private const string ELEMENT_NAME = "sqlTraceListener";

        /// <summary>
        /// Create a new element
        /// </summary>
        /// <returns></returns>
        protected override ConfigurationElement CreateNewElement()
        {
            return new SqlTraceListenerElement();
        }

        /// <summary>
        /// Return the key of the passed element
        /// </summary>
        /// <param name="element">A SqlTraceListenerElement</param>
        /// <returns>The key</returns>
        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((SqlTraceListenerElement) element).Name;
        }

        /// <summary>
        /// Get a SqlTraceListener element based on the passed key
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public new SqlTraceListenerElement this[string key]
        {
            get { return (SqlTraceListenerElement) base.BaseGet(key); }
        }

        /// <summary>
        /// Return the element name
        /// </summary>
        protected override string ElementName
        {
            get { return ELEMENT_NAME; }
        }

        /// <summary>
        /// Return the collection type
        /// </summary>
        public override ConfigurationElementCollectionType CollectionType
        {
            get { return ConfigurationElementCollectionType.BasicMap; }
        }
    }
}