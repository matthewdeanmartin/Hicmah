// THIS CODE AND INFORMATION ARE PROVIDED AS IS WITHOUT WARRANTY OF ANY
// KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE
// IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A
// PARTICULAR PURPOSE.

using System.Configuration;
using Ukadc.Diagnostics.Configuration;
using Ukadc.Diagnostics.Utils;
using System.ComponentModel;

namespace Ukadc.Diagnostics.Filters.Configuration
{
    /// <summary>
    /// Defines the propertyFilter element
    /// </summary>
    public class PropertyFilterElement : AbstractTokenElement
    {
        /// <summary>
        /// Return the element name
        /// </summary>
        public override string ElementName
        {
            get { return ELEMENT_NAME; }
        }

        /// <summary>
        /// Get/set the name of the property filter
        /// </summary>
        [ConfigurationProperty(NAME, IsKey = true, IsRequired = false)]
        public string Name
        {
            get { return (string) base[NAME]; }
            set { base[NAME] = value; }
        }

        /// <summary>
        /// Get/Set the operation used, the default being Equals
        /// </summary>
        [ConfigurationProperty(OPERATION, DefaultValue = Operation.Equals)]
        [TypeConverter(typeof(OperationConverter))]
        public Operation Operation
        {
            get { return (Operation) base[OPERATION]; }
            set { base[OPERATION] = value; }
        }

        /// <summary>
        /// Get/Set the value being compared against
        /// </summary>
        [ConfigurationProperty(VALUE)]
        public string Value
        {
            get { return (string) base[VALUE]; }
            set { base[VALUE] = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        [ConfigurationProperty(DEFAULT_EVALUATION, IsRequired=false, DefaultValue=true)]
        public bool DefaultEvaluation
        {
            get { return (bool) base[DEFAULT_EVALUATION]; }
            set { base[DEFAULT_EVALUATION] = value; }
        }

        internal const string ELEMENT_NAME = "propertyFilter";
        private const string NAME = "name";
        private const string OPERATION = "operation";
        private const string VALUE = "value";
        private const string DEFAULT_EVALUATION = "defaultEvaluation";
    }
}