// THIS CODE AND INFORMATION ARE PROVIDED AS IS WITHOUT WARRANTY OF ANY
// KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE
// IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A
// PARTICULAR PURPOSE.
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using Ukadc.Diagnostics.Configuration;

namespace Ukadc.Diagnostics.Utils.PropertyReaders
{
    /// <summary>
    /// Factory class used to create instances of <see cref="PropertyReader"/>s based on tokens and configuration elements
    /// </summary>
    public class PropertyReaderFactory : IPropertyReaderFactory
    {
        private readonly ICombinedPropertyReaderFactory _combinedFactory;
        private readonly object _padlock = new object();

        /// <summary>
        /// Creates a new instance of the <see cref="PropertyReaderFactory"/> loading the internally configured tokens and reading the
        /// configuration file's tokens.
        /// </summary>
        public PropertyReaderFactory()
            : this(true, true)
        {
        }

        /// <summary>
        /// Creates a new instance of the <see cref="PropertyReaderFactory"/>
        /// </summary>
        /// <param name="loadConfig">Indicates whether to load any tokens in the AppDomain's configuration file</param>
        /// <param name="loadInternalTokens">Indicates whether to load the hard-coded internal token list</param>
        public PropertyReaderFactory(bool loadConfig, bool loadInternalTokens)
        {
            _combinedFactory = DefaultServiceLocator.GetService<ICombinedPropertyReaderFactory>();

            if (loadInternalTokens)
            {
                AddInternalTokens();
            }
            if (loadConfig)
            {
                LoadConfiguredTokens();
            }
        }

        private void LoadConfiguredTokens()
        {
            // We need to load the tokens from configuration the first time this is accessed.
            UkadcDiagnosticsSection section;
            if (UkadcDiagnosticsSection.TryReadConfigSection(out section))
            {
                if (section.Tokens != null)
                {
                    for (int i = 0; i < section.Tokens.Count; i++)
                    {
                        TokenElement token = section.Tokens[i];
                        AddToken(token.Name, ReadConfigToken(token));
                    }
                }
            }
        }


        private readonly Dictionary<string, PropertyReader> _readerTokenDictionary =
            new Dictionary<string, PropertyReader>();

        /// <summary>
        /// The names of the Tokens currently contained inside the factory's <see cref="PropertyReader"/> dictionary
        /// </summary>
        public IEnumerable<String> TokenNames
        {
            get
            {
                return _readerTokenDictionary.Keys;
            }
        }

        /// <summary>
        /// Adds a new token to the internal dictionary of propertyReader.
        /// </summary>
        /// <param name="token">The name of the token</param>
        /// <param name="propertyReader">The propertyReader that the token represents</param>
        /// <exception cref="System.ArgumentNullException">Thrown if the token or propertyReader parameters are null</exception>
        /// <exception cref="ArgumentException">Thrown if the given token is already in the internal dictionary, or if the token is a substring of an existing token</exception>
        public void AddToken(string token, PropertyReader propertyReader)
        {
            if (string.IsNullOrEmpty(token))
                throw new ArgumentNullException("token");
            if (null == propertyReader)
                throw new ArgumentNullException("propertyReader");

            lock (_padlock)
            {
                if (_readerTokenDictionary.ContainsKey(token))
                {
                    throw new ArgumentException(string.Format(CultureInfo.CurrentCulture,
                                                              Resources.TokenAlreadyExists,
                                                              token));
                }


                _readerTokenDictionary.Add(token, propertyReader);
            }
        }

        /// <summary>
        /// Reads a <see cref="TokenElement"/> and creates a <see cref="PropertyReader"/>
        /// </summary>
        /// <param name="tokenElement">The tokenElement configuration element</param>
        /// <returns>A <see cref="PropertyReader"/></returns>
        private PropertyReader ReadConfigToken(TokenElement tokenElement)
        {
            bool formatConfigured = !string.IsNullOrEmpty(tokenElement.Format);
            bool typeConfigured = !string.IsNullOrEmpty(tokenElement.TypeName);
            bool dataPropertyConfigured = IsDataPropertyConfigured(tokenElement);

            if ((formatConfigured && typeConfigured) ||
                (formatConfigured && dataPropertyConfigured) ||
                (typeConfigured && dataPropertyConfigured))
            {
                throw new ConfigurationErrorsException(string.Format(CultureInfo.CurrentCulture,
                                                                     Resources.TokenElementIncorrect,
                                                                     tokenElement.Name));
            }

            if (formatConfigured)
            {
                return _combinedFactory.Create(tokenElement.Format, this);
            }
            if (dataPropertyConfigured)
            {
                return Create(tokenElement.DataProperty);
            }
            Type type = Type.GetType(tokenElement.TypeName, true);
            return (PropertyReader)Activator.CreateInstance(type);

        }

        /// <summary>
        /// Adds the built-in internal tokens to the <see cref="_readerTokenDictionary"/>.
        /// </summary>
        private void AddInternalTokens()
        {
            AddToken("Message", new MessagePropertyReader());
            AddToken("Id", new IdPropertyReader());
            AddToken("ThreadId", new ThreadIdPropertyReader());
            AddToken("ProcessId", new ProcessIdPropertyReader());
            AddToken("ProcessName", new ProcessNamePropertyReader());
            AddToken("Process", new ProcessPropertyReader());
            AddToken("Callstack", new CallstackPropertyReader());
            AddToken("DateTime", new DateTimePropertyReader());
            AddToken("LocalTime", new LocalTimePropertyReader());
            AddToken("EventType", new EventTypePropertyReader());
            AddToken("Source", new SourcePropertyReader());
            AddToken("ActivityId", new ActivityIdPropertyReader());
            AddToken("RelatedActivityId", new RelatedActivityIdPropertyReader());
            AddToken("MachineName", new MachineNamePropertyReader());
            AddToken("Timestamp", new TimestampPropertyReader());
            AddToken("PrincipalName", new PrincipalNamePropertyReader());
            AddToken("WindowsIdentity", new WindowsIdentityPropertyReader());

            AddToken("Year", new DatePartPropertyReader("yyyy"));
            AddToken("Month", new DatePartPropertyReader("MM"));
            AddToken("Day", new DatePartPropertyReader("dd"));
            AddToken("Hour", new DatePartPropertyReader("HH"));
            AddToken("Minute", new DatePartPropertyReader("mm"));
            AddToken("Second", new DatePartPropertyReader("ss"));
            AddToken("Millisecond", new DatePartPropertyReader("fff"));
        }

        /// <inheritdoc />
        /// <remarks>
        /// <para>
        /// A token is made up of a Name and then an optional set of arguments. These arguments are a property specification and a
        /// format specification - i.e. TOKEN{.property specification}{:format specification}.
        /// </para>
        /// <para>
        /// As an example, the following are all legal token specifications...
        /// <list type="table">
        ///     <listheader>
        ///         <term>Token String</term>
        ///         <description>Description</description>
        ///     </listheader>
        ///     <item>
        ///         <term>Process</term>
        ///         <description>Return the process object</description>
        ///     </item>
        ///     <item>
        ///         <term>Process.ProcessName</term>
        ///         <description>At runtime the process name of the current executing process will be output</description>
        ///     </item>
        ///     <item>
        ///         <term>Process.TotalProcessorTime:hh:mm:ss</term>
        ///         <description>This will output the total processor time formatted as hours:minutes:seconds</description>
        ///     </item>
        /// </list>
        /// </para>
        /// <para>
        /// A property specification consists of one or more property (or field) names separated by a period.
        /// </para>
        /// <para>
        /// A format specification is a standard .NET format specification, and is simply used in a ToString() call on the property
        /// value returned at runtime. So using the last example above, the code would effectively be 
        /// ((IFormattable)object).ToString(FormatSpecification).
        /// </para>
        /// </remarks>
        public PropertyReader Create(string tokenString)
        {
            if (string.IsNullOrEmpty(tokenString))
                throw new ArgumentNullException("tokenString");

            Token token = new Token(tokenString);

            return Create(token);
        }

        /// <inheritdoc />
        /// <remarks>
        /// <para>
        /// A token is made up of a Name and then an optional set of arguments. These arguments are a property specification and a
        /// format specification - i.e. TOKEN{.property specification}{:format specification}.
        /// </para>
        /// <para>
        /// As an example, the following are all legal token specifications...
        /// <list type="table">
        ///     <listheader>
        ///         <term>Token String</term>
        ///         <description>Description</description>
        ///     </listheader>
        ///     <item>
        ///         <term>Process</term>
        ///         <description>Return the process object</description>
        ///     </item>
        ///     <item>
        ///         <term>Process.ProcessName</term>
        ///         <description>At runtime the process name of the current executing process will be output</description>
        ///     </item>
        ///     <item>
        ///         <term>Process.TotalProcessorTime:hh:mm:ss</term>
        ///         <description>This will output the total processor time formatted as hours:minutes:seconds</description>
        ///     </item>
        /// </list>
        /// </para>
        /// <para>
        /// A property specification consists of one or more property (or field) names separated by a period.
        /// </para>
        /// <para>
        /// A format specification is a standard .NET format specification, and is simply used in a ToString() call on the property
        /// value returned at runtime. So using the last example above, the code would effectively be 
        /// ((IFormattable)object).ToString(FormatSpecification).
        /// </para>
        /// </remarks>
        public PropertyReader Create(Token token)
        {
            if (null == token)
                throw new ArgumentNullException("token");

            if (!_readerTokenDictionary.ContainsKey(token.TokenName))
            {
                throw new ArgumentException(string.Format(CultureInfo.CurrentCulture,
                                                          Resources.UnrecognizedToken,
                                                          token));
            }

            PropertyReader reader = _readerTokenDictionary[token.TokenName];

            if (!string.IsNullOrEmpty(token.SubProperty))
            {
                reader = new FastPropertyReader(reader, token.SubProperty);
            }

            if (!string.IsNullOrEmpty(token.FormatString))
            {
                reader = new FormattedPropertyReader(reader, token.FormatString);
            }

            return reader;
        }

        /// <summary>
        /// Create a property reader based on the passed DataPropertyElement
        /// </summary>
        /// <param name="dataProperty">A DataPropertyElement</param>
        /// <returns>A PropertyReader</returns>
        public PropertyReader Create(DataPropertyElement dataProperty)
        {
            if (null == dataProperty)
                throw new ArgumentNullException("dataProperty");

            return new DynamicPropertyReader(
                dataProperty.SourceType,
                dataProperty.PropertyName);
        }

        /// <summary>
        /// Creates a <see cref="PropertyReader"/> based on an <see cref="Ukadc.Diagnostics.Configuration.AbstractTokenElement"/> configuration element.
        /// </summary>
        /// <param name="abstractTokenElement">The configuration element</param>
        /// <returns>A <see cref="PropertyReader"/></returns>
        public PropertyReader Create(AbstractTokenElement abstractTokenElement)
        {
            if (IsDataPropertyConfigured(abstractTokenElement))
            {
                if (!string.IsNullOrEmpty(abstractTokenElement.PropertyToken))
                {
                    throw new ConfigurationErrorsException(string.Format(CultureInfo.CurrentCulture,
                                                                         Resources.
                                                                             BothPropertyTokenAndDataPropertyDefined,
                                                                         abstractTokenElement.PropertyToken,
                                                                         abstractTokenElement.ElementName));
                }
                return Create(abstractTokenElement.DataProperty);
            }
            else
            {
                return Create(abstractTokenElement.PropertyToken);
            }
        }

        /// <summary>
        /// Analyses the DataPropertyElement to see if it has been configured
        /// </summary>
        /// <param name="element">the DataPropertyElement</param>
        /// <returns>true if the DataPropertyElement has been configured</returns>
        private bool IsDataPropertyConfigured(AbstractDataPropertyElement element)
        {
            return !(element.DataProperty == null ||
                     (string.IsNullOrEmpty(element.DataProperty.SourceType) &&
                      string.IsNullOrEmpty(element.DataProperty.PropertyName)));
        }

        /// <summary>
        /// Creates a <see cref="PropertyReader"/> by parsing the combinedToken parameter.
        /// </summary>
        /// <param name="combinedToken"></param>
        /// <returns>Usually returns a <see cref="CombinedPropertyReader"/> but can optimise to return a specific <see cref="PropertyReader"/>
        /// or <see cref="LiteralPropertyReader"/> if there is no need for a combined version</returns>
        public PropertyReader CreateCombinedReader(string combinedToken)
        {
            return _combinedFactory.Create(combinedToken, this);
        }

        /// <inheritDoc />
        public bool ContainsToken(string tokenName)
        {
            return _readerTokenDictionary.ContainsKey(tokenName);
        }
    }
}