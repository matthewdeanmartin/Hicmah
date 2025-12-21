// THIS CODE AND INFORMATION ARE PROVIDED AS IS WITHOUT WARRANTY OF ANY
// KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE
// IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A
// PARTICULAR PURPOSE.
using System.Configuration;
using System.Diagnostics.CodeAnalysis;

namespace Ukadc.Diagnostics.Configuration
{
    /// <summary>
    /// This class exposes the &lt;tokens&gt; collection
    /// </summary>
    /// <remarks>
    /// Tokens are our way to extend the data that can be presented to the trace listeners. A token is a named item contained within braces,
    /// and is used to extract pertinent information from a log event. There are numerous inbuilt tokens already defined.
    /// </remarks>
    [SuppressMessage("Microsoft.Design",
        "CA1010:CollectionsShouldImplementGenericInterface"),
     ConfigurationCollection(typeof (TokenElement), AddItemName = TokenElement.ELEMENT_NAME,
         CollectionType = ConfigurationElementCollectionType.BasicMap)]
    public class TokenElementCollection : ConfigurationElementCollection
    {
        /// <summary>
        /// Create a new TokenElement and return
        /// </summary>
        /// <returns></returns>
        protected override ConfigurationElement CreateNewElement()
        {
            return new TokenElement();
        }

        /// <summary>
        /// Return the key of the item
        /// </summary>
        /// <param name="element">A configuration element</param>
        /// <returns>The key of that element</returns>
        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((TokenElement) element).Name;
        }

        /// <summary>
        /// Return the item at the given index position
        /// </summary>
        /// <param name="index">The index</param>
        /// <returns>A TokenElement at the given index</returns>
        public TokenElement this[int index]
        {
            get { return (TokenElement) base.BaseGet(index); }
        }

        /// <summary>
        /// Return the element name
        /// </summary>
        protected override string ElementName
        {
            get { return TokenElement.ELEMENT_NAME; }
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