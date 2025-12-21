// THIS CODE AND INFORMATION ARE PROVIDED AS IS WITHOUT WARRANTY OF ANY
// KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE
// IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A
// PARTICULAR PURPOSE.

using System.Configuration;
using System.Data;

namespace Ukadc.Diagnostics.Listeners.Configuration
{
    /// <summary>
    /// Read a sqlTraceListener element from the config file
    /// </summary>
    public class SqlTraceListenerElement : ConfigurationElement
    {
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
        /// Get/Set the connection string name
        /// </summary>
        [ConfigurationProperty(CONNECTION_STRING_NAME, IsRequired = true)]
        public string ConnectionStringName
        {
            get { return (string) base[CONNECTION_STRING_NAME]; }
            set { base[CONNECTION_STRING_NAME] = value; }
        }

        /// <summary>
        /// Get/Set the Sql command type - 'Text','StoredProcedure' or 'TableDirect'
        /// </summary>
        [ConfigurationProperty(COMMAND_TYPE, DefaultValue = CommandType.Text)]
        public CommandType CommandType
        {
            get { return (CommandType) base[COMMAND_TYPE]; }
            set { base[COMMAND_TYPE] = value; }
        }

        /// <summary>
        /// Get/Set the command used
        /// </summary>
        [ConfigurationProperty(COMMAND_TEXT)]
        public string CommandText
        {
            get { return (string) base[COMMAND_TEXT]; }
            set { base[COMMAND_TEXT] = value; }
        }

        /// <summary>
        /// Get/Set the command parameters
        /// </summary>
        [ConfigurationProperty(PARAMETERS)]
        public ParameterElementCollection Parameters
        {
            get { return (ParameterElementCollection) base[_parameters]; }
        }

        private static readonly ConfigurationProperty _parameters
            =
            new ConfigurationProperty(PARAMETERS, typeof(ParameterElementCollection), new ParameterElementCollection(),
                                      ConfigurationPropertyOptions.None);

        private const string NAME = "name";
        private const string CONNECTION_STRING_NAME = "connectionStringName";
        private const string COMMAND_TYPE = "commandType";
        private const string COMMAND_TEXT = "commandText";
        private const string PARAMETERS = "parameters";

    }
}