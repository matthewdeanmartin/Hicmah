// THIS CODE AND INFORMATION ARE PROVIDED AS IS WITHOUT WARRANTY OF ANY
// KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE
// IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A
// PARTICULAR PURPOSE.
using System.Configuration;
using System.Diagnostics.CodeAnalysis;

namespace Ukadc.Diagnostics.Listeners.Configuration
{
    /// <summary>
    /// Reads SmtpTraceListenerElements
    /// </summary>
    [SuppressMessage("Microsoft.Design",
        "CA1010:CollectionsShouldImplementGenericInterface")]
    [ConfigurationCollection(typeof (SmtpTraceListenerElement), AddItemName = SmtpTraceListenerElement.ELEMENT_NAME,
        CollectionType = ConfigurationElementCollectionType.BasicMap)]
    public class SmtpTraceListenerElementCollection : ConfigurationElementCollection
    {
        /// <summary>
        /// Create a new element and return to the caller
        /// </summary>
        /// <returns></returns>
        protected override ConfigurationElement CreateNewElement()
        {
            return new SmtpTraceListenerElement();
        }

        /// <summary>
        /// Return the key of the passed element
        /// </summary>
        /// <param name="element">The elemet whose key is being requested</param>
        /// <returns>The key</returns>
        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((SmtpTraceListenerElement) element).Name;
        }

        /// <summary>
        /// Return the element based on the key
        /// </summary>
        /// <param name="key">The key</param>
        /// <returns>The element</returns>
        public new SmtpTraceListenerElement this[string key]
        {
            get { return (SmtpTraceListenerElement) base.BaseGet(key); }
        }

        /// <summary>
        /// Return the element name
        /// </summary>
        protected override string ElementName
        {
            get { return SmtpTraceListenerElement.ELEMENT_NAME; }
        }

        /// <summary>
        /// Return the type of the collection
        /// </summary>
        public override ConfigurationElementCollectionType CollectionType
        {
            get { return ConfigurationElementCollectionType.BasicMap; }
        }
    }
}