// THIS CODE AND INFORMATION ARE PROVIDED AS IS WITHOUT WARRANTY OF ANY
// KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE
// IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A
// PARTICULAR PURPOSE.

using System.Configuration;

namespace Ukadc.Diagnostics.Configuration
{
    /// <summary>
    /// Represents a data property
    /// </summary>
    public class DataPropertyElement : ConfigurationElement
    {
        private const string SOURCE_TYPE = "sourceType";
        private const string PROPERTY_NAME = "propertyName";

        /// <summary>
        /// Get/Set the sourceType attribute
        /// </summary>
        [ConfigurationProperty(SOURCE_TYPE, IsKey = true, IsRequired = true)]
        public string SourceType
        {
            get { return (string) base[SOURCE_TYPE]; }
            set { base[SOURCE_TYPE] = value; }
        }

        /// <summary>
        /// Get/Set the propertyName
        /// </summary>
        [ConfigurationProperty(PROPERTY_NAME, IsRequired = true)]
        public string PropertyName
        {
            get { return (string) base[PROPERTY_NAME]; }
            set { base[PROPERTY_NAME] = value; }
        }
    }
}