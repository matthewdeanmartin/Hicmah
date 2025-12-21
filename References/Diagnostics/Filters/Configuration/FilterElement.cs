// THIS CODE AND INFORMATION ARE PROVIDED AS IS WITHOUT WARRANTY OF ANY
// KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE
// IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A
// PARTICULAR PURPOSE.

using System.Configuration;

namespace Ukadc.Diagnostics.Filters.Configuration
{
    /// <summary>
    /// Defines a FilterElement object
    /// </summary>
    public class FilterElement : ConfigurationElement
    {
        /// <summary>
        /// Gets the fully qualified type name of the filter
        /// </summary>
        [ConfigurationProperty(TYPE, IsRequired = true)]
        public string TypeName
        {
            get { return (string) base[TYPE]; }
            set { base[TYPE] = value; }
        }

        /// <summary>
        /// Indicates that the comparison should be negated, which defaults to false
        /// </summary>
        /// <remarks>
        /// This allows you to change the processing logic as necessary. As an example you might want to only log events that don't contain the
        /// error message 'Oh no bad things'.
        /// </remarks>
        [ConfigurationProperty(NEGATE, DefaultValue = false)]
        public bool Negate
        {
            get { return (bool) base[NEGATE]; }
            set { base[NEGATE] = value; }
        }

        /// <summary>
        /// Initialization data for the filter element
        /// </summary>
        [ConfigurationProperty(INITIALIZE_DATA)]
        public string InitializeData
        {
            get { return (string) base[INITIALIZE_DATA]; }
            set { base[INITIALIZE_DATA] = value; }
        }

        private const string TYPE = "type";
        private const string NEGATE = "negate";
        private const string INITIALIZE_DATA = "initializeData";
    }
}