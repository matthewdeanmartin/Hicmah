// THIS CODE AND INFORMATION ARE PROVIDED AS IS WITHOUT WARRANTY OF ANY
// KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE
// IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A
// PARTICULAR PURPOSE.
using System.Configuration;

namespace Ukadc.Diagnostics.Configuration
{
    /// <summary>
    /// Represents an abstract data property element from the config file
    /// </summary>
    public class TokenElement : AbstractDataPropertyElement
    {
        internal const string ELEMENT_NAME = "token";
        private const string NAME = "name";
        private const string TYPE = "type";
        private const string FORMAT = "format";

        /// <summary>
        /// Get/Set the name
        /// </summary>
        [ConfigurationProperty(NAME, IsKey = true, IsRequired = true)]
        public string Name
        {
            get { return (string) base[NAME]; }
            set { base[NAME] = value; }
        }

        /// <summary>
        /// Get/Set the type name
        /// </summary>
        [ConfigurationProperty(TYPE, IsRequired = false)]
        public string TypeName
        {
            get { return (string) base[TYPE]; }
            set { base[TYPE] = value; }
        }

        /// <summary>
        /// Get/Set the format representation
        /// </summary>
        [ConfigurationProperty(FORMAT, IsRequired = false)]
        public string Format
        {
            get { return (string) base[FORMAT]; }
            set { base[FORMAT] = value; }
        }
    }
}