// THIS CODE AND INFORMATION ARE PROVIDED AS IS WITHOUT WARRANTY OF ANY
// KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE
// IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A
// PARTICULAR PURPOSE.

using System.Configuration;

namespace Ukadc.Diagnostics.Configuration
{
    /// <summary>
    /// Read an element from the configuration file
    /// </summary>
    public abstract class AbstractTokenElement : AbstractDataPropertyElement
    {
        private const string PROPERTY_TOKEN = "propertyToken";

        /// <summary>
        /// Return the element name
        /// </summary>
        public abstract string ElementName { get; }

        /// <summary>
        /// Get/Set the propertyToken attribute
        /// </summary>
        [ConfigurationProperty(PROPERTY_TOKEN, IsRequired = false)]
        public string PropertyToken
        {
            get { return (string) base[PROPERTY_TOKEN]; }
            set { base[PROPERTY_TOKEN] = value; }
        }
    }
}