// THIS CODE AND INFORMATION ARE PROVIDED AS IS WITHOUT WARRANTY OF ANY
// KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE
// IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A
// PARTICULAR PURPOSE.

using System.Configuration;
using System.Diagnostics.CodeAnalysis;

namespace Ukadc.Diagnostics.Filters.Configuration
{
    /// <summary>
    /// Read property filter elements from the configuration file
    /// </summary>
    [SuppressMessage("Microsoft.Design",
        "CA1010:CollectionsShouldImplementGenericInterface")]
    [ConfigurationCollection(typeof (FilterElement), AddItemName = PropertyFilterElement.ELEMENT_NAME,
        CollectionType = ConfigurationElementCollectionType.BasicMap)]
    public class PropertyFilterElementCollection : ConfigurationElementCollection
    {
        /// <summary>
        /// Construct a new PropertyFilterElement and return to the caller
        /// </summary>
        /// <returns></returns>
        protected override ConfigurationElement CreateNewElement()
        {
            return new PropertyFilterElement();
        }

        /// <summary>
        /// Return the key for the passed element
        /// </summary>
        /// <param name="element">A PropertyFilterElement</param>
        /// <returns>The name of the element</returns>
        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((PropertyFilterElement) element).Name;
        }

        /// <summary>
        /// Get the named element
        /// </summary>
        /// <param name="key">The key of the element</param>
        /// <returns>The element</returns>
        public new PropertyFilterElement this[string key]
        {
            get { return (PropertyFilterElement) base.BaseGet(key); }
        }

        /// <summary>
        /// Return the element name
        /// </summary>
        protected override string ElementName
        {
            get { return PropertyFilterElement.ELEMENT_NAME; }
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