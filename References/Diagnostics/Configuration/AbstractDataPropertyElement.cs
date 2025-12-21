using System.Configuration;

namespace Ukadc.Diagnostics.Configuration
{
    /// <summary>
    /// A class that defines a configuration element that includes a dynamicProperty element
    /// </summary>
    public abstract class AbstractDataPropertyElement : ConfigurationElement
    {
        private const string DATA_PROPERTY = "dynamicProperty";

        /// <summary>
        /// Get the dynamicProperty element from the configuration file
        /// </summary>
        [ConfigurationProperty(DATA_PROPERTY, IsRequired = false, DefaultValue = null)]
        public DataPropertyElement DataProperty
        {
            get { return (DataPropertyElement) base[DATA_PROPERTY]; }
            set { base[DATA_PROPERTY] = value; }
        }
    }
}