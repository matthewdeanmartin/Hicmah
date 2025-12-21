// THIS CODE AND INFORMATION ARE PROVIDED AS IS WITHOUT WARRANTY OF ANY
// KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE
// IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A
// PARTICULAR PURPOSE.

using System.Configuration;

namespace Ukadc.Diagnostics.Filters.Configuration
{
    /// <summary>
    /// Defines an element that resides within the &lt;filterGroup&gt; element of the configuration file
    /// </summary>
    /// <remarks>
    /// A FilterGroupElement contains Name, Logic and Filters properties. 
    /// </remarks>
    public class FilterGroupElement : ConfigurationElement
    {
        private const string ELEMENT_NAME = "filters";
        private const string NAME = "name";
        private const string LOGIC = "logic";

        private static readonly ConfigurationProperty _filters
            =
            new ConfigurationProperty(ELEMENT_NAME, typeof (FilterElementCollection), new FilterElementCollection(),
                                      ConfigurationPropertyOptions.None);

        /// <summary>
        /// Return the name of the FilterGroupElement
        /// </summary>
        [ConfigurationProperty(NAME, IsKey = true, IsRequired = true)]
        public string Name
        {
            get { return (string) base[NAME]; }
            set { base[NAME] = value; }
        }

        /// <summary>
        /// Return what type of comparison is done when the filter is evaluated.
        /// </summary>
        /// <remarks>
        /// The options are 'And' and 'Or'. 
        /// </remarks>
        [ConfigurationProperty(LOGIC, DefaultValue = FilterGroupLogic.And)]
        public FilterGroupLogic Logic
        {
            get { return (FilterGroupLogic) base[LOGIC]; }
            set { base[LOGIC] = value; }
        }

        /// <summary>
        /// A collection of filter elements
        /// </summary>
        [ConfigurationProperty(ELEMENT_NAME)]
        public FilterElementCollection Filters
        {
            get { return (FilterElementCollection) base[_filters]; }
        }
    }
}