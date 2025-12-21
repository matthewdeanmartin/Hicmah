// THIS CODE AND INFORMATION ARE PROVIDED AS IS WITHOUT WARRANTY OF ANY
// KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE
// IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A
// PARTICULAR PURPOSE.

using System.Configuration;
using System.Diagnostics.CodeAnalysis;

namespace Ukadc.Diagnostics.Filters.Configuration
{
    /// <summary>
    /// A collection class used to read/write filter group elements from the configuration file
    /// </summary>
    [SuppressMessage("Microsoft.Design",
        "CA1010:CollectionsShouldImplementGenericInterface"),
     ConfigurationCollection(typeof (FilterGroupElement), AddItemName = ELEMENT_NAME,
         CollectionType = ConfigurationElementCollectionType.BasicMap)]
    public class FilterGroupElementCollection : ConfigurationElementCollection
    {
        /// <summary>
        /// Construct a new element
        /// </summary>
        /// <returns>A newly constructed FilterGroupElement</returns>
        protected override ConfigurationElement CreateNewElement()
        {
            return new FilterGroupElement();
        }

        /// <summary>
        /// Return the key for the passed configuration element
        /// </summary>
        /// <param name="element">The element for which the key is to be retrieved</param>
        /// <returns>The key value for that element</returns>
        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((FilterGroupElement) element).Name;
        }

        /// <summary>
        /// Return a filter element by name
        /// </summary>
        /// <param name="name">The name of the filter group element</param>
        /// <returns>The named filter group element</returns>
        public new FilterGroupElement this[string name]
        {
            get { return (FilterGroupElement) base.BaseGet(name); }
        }

        /// <summary>
        /// Return the name of the element - in this case 'filterGroup'
        /// </summary>
        protected override string ElementName
        {
            get { return ELEMENT_NAME; }
        }

        /// <summary>
        /// Return what type of collection we're exposing
        /// </summary>
        public override ConfigurationElementCollectionType CollectionType
        {
            get { return ConfigurationElementCollectionType.BasicMap; }
        }

        private const string ELEMENT_NAME = "filterGroup";
    }
}