// THIS CODE AND INFORMATION ARE PROVIDED AS IS WITHOUT WARRANTY OF ANY
// KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE
// IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A
// PARTICULAR PURPOSE.

using System.Configuration;
using System.Diagnostics.CodeAnalysis;

namespace Ukadc.Diagnostics.Listeners.Configuration
{
    /// <summary>
    /// Defines a collection of parameter elements
    /// </summary>
    [SuppressMessage("Microsoft.Design",
        "CA1010:CollectionsShouldImplementGenericInterface"),
     ConfigurationCollection(typeof (ParameterElement), AddItemName = ELEMENT_NAME,
         CollectionType = ConfigurationElementCollectionType.BasicMap)]
    public class ParameterElementCollection : ConfigurationElementCollection
    {
        private const string ELEMENT_NAME = "parameter";

        /// <summary>
        /// Create a new element and return to the caller
        /// </summary>
        /// <returns></returns>
        protected override ConfigurationElement CreateNewElement()
        {
            return new ParameterElement();
        }

        /// <summary>
        /// Return the key of the passed element
        /// </summary>
        /// <param name="element">The element</param>
        /// <returns>The Name of the parameter element</returns>
        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((ParameterElement) element).Name;
        }

        /// <summary>
        /// Return the element name, in thei case "parameter"
        /// </summary>
        protected override string ElementName
        {
            get { return ELEMENT_NAME; }
        }

        /// <summary>
        /// Return the type of collection used, in this instance a BasicMap
        /// </summary>
        public override ConfigurationElementCollectionType CollectionType
        {
            get { return ConfigurationElementCollectionType.BasicMap; }
        }
    }
}