// THIS CODE AND INFORMATION ARE PROVIDED AS IS WITHOUT WARRANTY OF ANY
// KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE
// IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A
// PARTICULAR PURPOSE.

using System.Configuration;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using Ukadc.Diagnostics.Filters.Configuration;
using Ukadc.Diagnostics.Listeners.Configuration;

namespace Ukadc.Diagnostics.Configuration
{
    /// <summary>
    /// Represents the Ukadc.Diagnostics configuration section
    /// </summary>
    /// <remarks>
    /// This configuration section is used to define additional data that we cannot include in the standard &lt;system.diagnostics&gt; section. This
    /// configuration section is defined using the following layout :-
    /// <code>
    /// &lt;ukadc.diagnostics&gt;
    ///   &lt;filterGroups&gt;
    ///   &lt;/filterGroups&gt;
    ///   &lt;propertyFilters&gt;
    ///   &lt;/propertyFilters&gt;
    ///   &lt;tokens&gt;
    ///   &lt;/tokens&gt;
    ///   &lt;sqlTraceListeners&gt;
    ///   &lt;/sqlTraceListeners&gt;
    ///   &lt;smtpTraceListeners&gt;
    ///   &lt;/smtpTraceListeners&gt;
    ///   &lt;&gt;
    /// &lt;/ukadc.diagnostics&gt;
    /// </code>
    /// </remarks>
    public class UkadcDiagnosticsSection : ConfigurationSection
    {
        /// <summary>
        /// Reads the <see cref="UkadcDiagnosticsSection" /> from configuration. If the section can't be found
        /// a <see cref="ConfigurationErrorsException"/> is thrown.
        /// </summary>
        /// <returns>The configuration section</returns>
        [SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate")]
        public static UkadcDiagnosticsSection ReadConfigSection()
        {
            UkadcDiagnosticsSection section;
            if (!TryReadConfigSection(out section))
            {
                throw new ConfigurationErrorsException(string.Format(CultureInfo.CurrentCulture,
                                                                     Resources.NoConfigurationSectionFound,
                                                                     SECTION_NAME,
                                                                     typeof (UkadcDiagnosticsSection).FullName));
            }
            return section;
        }

        /// <summary>
        /// Tries to read the configuration section using the <see cref="ReadConfigSection"/> method. However,
        /// this method returns false is the configuration section was unavailable.
        /// </summary>
        /// <param name="section">The section loaded from the configuration file</param>
        /// <returns>The configuration section</returns>
        public static bool TryReadConfigSection(out UkadcDiagnosticsSection section)
        {
            section = (UkadcDiagnosticsSection) ConfigurationManager.GetSection(SECTION_NAME);
            return section != null;
        }

        private static readonly ConfigurationProperty _filterGroups
            =
            new ConfigurationProperty(FILTER_GROUPS_ELEMENT, typeof (FilterGroupElementCollection),
                                      new FilterGroupElementCollection(), ConfigurationPropertyOptions.None);

        /// <summary>
        /// Returns the &lt;filterGroups&gt; element from within the &lt;ukadc.diagnostics&gt; configuration section
        /// </summary>
        [ConfigurationProperty(FILTER_GROUPS_ELEMENT)]
        public FilterGroupElementCollection FilterGroups
        {
            get { return (FilterGroupElementCollection) base[_filterGroups]; }
        }

        private static readonly ConfigurationProperty _propertyFilters
            =
            new ConfigurationProperty(PROPERTY_FILTERS_ELEMENT, typeof (PropertyFilterElementCollection),
                                      new PropertyFilterElementCollection(), ConfigurationPropertyOptions.None);

        /// <summary>
        /// Returns the &lt;propertyFilters&gt; element from within the &lt;ukadc.diagnostics&gt; configuration section
        /// </summary>
        [ConfigurationProperty(PROPERTY_FILTERS_ELEMENT)]
        public PropertyFilterElementCollection PropertyFilters
        {
            get { return (PropertyFilterElementCollection) base[_propertyFilters]; }
        }

        private static readonly ConfigurationProperty _tokens
            =
            new ConfigurationProperty(TOKENS_ELEMENT, typeof (TokenElementCollection), new TokenElementCollection(),
                                      ConfigurationPropertyOptions.None);

        /// <summary>
        /// Returns the &lt;tokens&gt; element from within the &lt;ukadc.diagnostics&gt; configuration section
        /// </summary>
        [ConfigurationProperty(TOKENS_ELEMENT)]
        public TokenElementCollection Tokens
        {
            get { return (TokenElementCollection) base[_tokens]; }
        }

        private static readonly ConfigurationProperty _sqlTraceListenersElementCollection
            =
            new ConfigurationProperty(SQL_TRACE_LISTENERS_ELEMENT, typeof (SqlTraceListenerElementCollection),
                                      new SqlTraceListenerElementCollection(), ConfigurationPropertyOptions.None);
        /// <summary>
        /// Returns the &lt;sqlTraceListeners&gt; element from within the &lt;ukadc.diagnostics&gt; configuration section
        /// </summary>
        [ConfigurationProperty(SQL_TRACE_LISTENERS_ELEMENT)]
        public SqlTraceListenerElementCollection SqlTraceListeners
        {
            get { return (SqlTraceListenerElementCollection) base[_sqlTraceListenersElementCollection]; }
        }

        private static readonly ConfigurationProperty _smtpTraceListenersElementCollection
            =
            new ConfigurationProperty(SMTP_TRACE_LISTENERS_ELEMENT, typeof (SmtpTraceListenerElementCollection),
                                      new SmtpTraceListenerElementCollection(), ConfigurationPropertyOptions.None);

        /// <summary>
        /// Returns the &lt;smtpTraceListeners&gt; element from within the &lt;ukadc.diagnostics&gt; configuration section
        /// </summary>
        [ConfigurationProperty(SMTP_TRACE_LISTENERS_ELEMENT)]
        public SmtpTraceListenerElementCollection SmtpTraceListeners
        {
            get { return (SmtpTraceListenerElementCollection) base[_smtpTraceListenersElementCollection]; }
        }

        private const string SECTION_NAME = "ukadc.diagnostics";
        private const string FILTER_GROUPS_ELEMENT = "filterGroups";
        private const string PROPERTY_FILTERS_ELEMENT = "propertyFilters";
        private const string TOKENS_ELEMENT = "tokens";
        private const string SQL_TRACE_LISTENERS_ELEMENT = "sqlTraceListeners";
        private const string SMTP_TRACE_LISTENERS_ELEMENT = "smtpTraceListeners";
    }
}