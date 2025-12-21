// THIS CODE AND INFORMATION ARE PROVIDED AS IS WITHOUT WARRANTY OF ANY
// KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE
// IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A
// PARTICULAR PURPOSE.

using System.Configuration;
using Ukadc.Diagnostics.Configuration;

namespace Ukadc.Diagnostics.Listeners.Configuration
{
    /// <summary>
    /// Read a parameter element from the config file
    /// </summary>
    public class ParameterElement : AbstractTokenElement
    {
        internal const string ELEMENT_NAME = "parameter";
        private const string NAME = "name";
        private const string CALL_TO_STRING = "callToString";

        /// <summary>
        /// Return the element name - in this case "parameter"
        /// </summary>
        public override string ElementName
        {
            get { return ELEMENT_NAME; }
        }

        /// <summary>
        /// Get/Set the name
        /// </summary>
        [ConfigurationProperty(NAME, IsKey = true, IsRequired = false)]
        public string Name
        {
            get { return (string) base[NAME]; }
            set { base[NAME] = value; }
        }

        /// <summary>
        /// Defines whether a call is made to the ToString() function
        /// </summary>
        [ConfigurationProperty(CALL_TO_STRING, DefaultValue=false)]
        public bool CallToString
        {
            get { return (bool) base[CALL_TO_STRING]; }
            set { base[CALL_TO_STRING] = value; }
        }
    }
}