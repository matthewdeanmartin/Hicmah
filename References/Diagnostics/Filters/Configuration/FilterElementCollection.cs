// THIS CODE AND INFORMATION ARE PROVIDED AS IS WITHOUT WARRANTY OF ANY
// KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE
// IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A
// PARTICULAR PURPOSE.

using System.Configuration;
using System.Diagnostics.CodeAnalysis;

namespace Ukadc.Diagnostics.Filters.Configuration
{
    /// <summary>
    /// Exposes a collection of FilterElement objects
    /// </summary>
    [SuppressMessage("Microsoft.Design",
        "CA1010:CollectionsShouldImplementGenericInterface")]
    public class FilterElementCollection : ConfigurationElementCollection
    {
        /// <summary>
        /// Create a new filter element
        /// </summary>
        /// <returns>The newly constructed FilterElement instance</returns>
        protected override ConfigurationElement CreateNewElement()
        {
            return new FilterElement();
        }

        /// <summary>
        /// Return the key for the passed filte element
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        protected override object GetElementKey(ConfigurationElement element)
        {
            FilterElement filterElement = (FilterElement)element;

            // Each filter element should be unique
            return filterElement.TypeName + filterElement.InitializeData + filterElement.Negate.ToString();
        }

        /// <summary>
        /// Return the filter element based on the index
        /// </summary>
        /// <param name="index">The zero based indes of filter elements</param>
        /// <returns>The filter element at the appropriate index</returns>
        public FilterElement this[int index]
        {
            get { return (FilterElement) base.BaseGet(index); }
        }

        /// <summary>
        /// Return the name of the element
        /// </summary>
        protected override string ElementName
        {
            get { return ELEMENT_NAME; }
        }

        /// <summary>
        /// Return the collection type used to store the data in the configuration file
        /// </summary>
        public override ConfigurationElementCollectionType CollectionType
        {
            get { return ConfigurationElementCollectionType.BasicMap; }
        }

        private const string ELEMENT_NAME = "filter";
    }
}