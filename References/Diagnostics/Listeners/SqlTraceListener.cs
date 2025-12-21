// THIS CODE AND INFORMATION ARE PROVIDED AS IS WITHOUT WARRANTY OF ANY
// KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE
// IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A
// PARTICULAR PURPOSE.
using System.Collections.ObjectModel;
using System.Configuration;
using System.Diagnostics;
using System.Globalization;
using Ukadc.Diagnostics.Configuration;
using Ukadc.Diagnostics.Listeners.Configuration;
using Ukadc.Diagnostics.Utils.PropertyReaders;
using Ukadc.Diagnostics.Utils;

namespace Ukadc.Diagnostics.Listeners
{
    /// <summary>
    /// An implementation of the <see cref="CustomTraceListener"/> used to trace data to an instance
    /// of SQL Server.
    /// </summary>
    public class SqlTraceListener : CustomTraceListener
    {
        private readonly Collection<SqlTraceParameter> _parameters = new Collection<SqlTraceParameter>();

        /// <summary>
        /// Creates a new instance of the <see cref="SqlTraceListener"/> using the specified <see cref="IDataAccessAdapter"/>.
        /// </summary>
        /// <param name="dataAccessAdapter"></param>
        public SqlTraceListener(IDataAccessAdapter dataAccessAdapter)
            : base("SqlTraceListener")
        {
            DataAccessAdapter = dataAccessAdapter;
        }


        /// <summary>
        /// Creates a new <see cref="SqlTraceListener"/> using the specified settings name
        /// </summary>
        /// <param name="settingsName">the name of the SqlTraceListener settings to use from configuration</param>
        public SqlTraceListener(string settingsName)
            : base(settingsName)
        {
            UkadcDiagnosticsSection ukadcDiagnosticsSection = UkadcDiagnosticsSection.ReadConfigSection();
            SqlTraceListenerElement sqlTraceListenerElement = ukadcDiagnosticsSection.SqlTraceListeners[settingsName];
            if (null == sqlTraceListenerElement)
            {
                throw new ConfigurationErrorsException(
                    string.Format(CultureInfo.CurrentCulture, Resources.SqlTraceListenerConfigError, settingsName));
            }

            string connectionString =
                ConfigurationManager.ConnectionStrings[sqlTraceListenerElement.ConnectionStringName].ConnectionString;

            // use default data adapter
            IDataAccessAdapter adapter = new SqlDataAccessAdapter();
            adapter.Initialize(
                connectionString,
                sqlTraceListenerElement.CommandText,
                sqlTraceListenerElement.CommandType);
            DataAccessAdapter = adapter;

            IPropertyReaderFactory readerFactory = DefaultServiceLocator.GetService<IPropertyReaderFactory>();

            foreach (ParameterElement param in sqlTraceListenerElement.Parameters)
            {
                PropertyReader propertyReader = readerFactory.Create(param);

                this.Parameters.Add(
                    new SqlTraceParameter(param.Name, propertyReader, param.CallToString));
            }
        }

        /// <summary>
        /// A list of <see cref="SqlTraceParameter"/>s that will be used by this <see cref="SqlTraceListener"/> to 
        /// call the database.
        /// </summary>
        public Collection<SqlTraceParameter> Parameters
        {
            get { return _parameters; }
        }

        /// <summary>
        /// The <see cref="IDataAccessAdapter"/> used by this <see cref="SqlTraceListener"/> to talk to the database
        /// </summary>
        public IDataAccessAdapter DataAccessAdapter
        {
            get;
            private set;
        }

        /// <summary>
        /// This method must be overriden and forms the core logging method called by all other TraceEvent methods.
        /// </summary>
        /// <param name="eventCache">A cache of data that defines the trace event</param>
        /// <param name="source">The trace source</param>
        /// <param name="eventType">The type of event</param>
        /// <param name="id">The unique ID of the trace event</param>
        /// <param name="message">A message to be output regarding the trace event</param>
        protected override void TraceEventCore(TraceEventCache eventCache, string source, TraceEventType eventType,
                                               int id, string message)
        {
            using (IDataAccessCommand command = DataAccessAdapter.CreateCommand())
            {
                foreach (SqlTraceParameter parameter in Parameters)
                {
                    object paramValue = null;
                    bool gotValue =
                        parameter.PropertyReader.TryGetValue(out paramValue, eventCache, source, eventType, id, message,
                                                             null);
                    AddParameterToCommand(command, parameter, gotValue ? paramValue : null);
                }

                command.Execute();
            }
        }

        private static object AddParameterToCommand(IDataAccessCommand command, SqlTraceParameter parameter,
                                                    object paramValue)
        {
            if (parameter.CallToString && paramValue != null)
            {
                paramValue = paramValue.ToString();
            }
            command.AddParameter(parameter.Name, paramValue);
            return paramValue;
        }

        /// <summary>
        /// This method must be overriden and forms the core logging method called by all otherTraceData methods.
        /// </summary>
        /// <param name="eventCache">A cache of data that defines the trace event</param>
        /// <param name="source">The trace source</param>
        /// <param name="eventType">The type of event</param>
        /// <param name="id">The unique ID of the trace event</param>
        /// <param name="data">The data to be logged</param>
        protected override void TraceDataCore(TraceEventCache eventCache, string source, TraceEventType eventType,
                                              int id, params object[] data)
        {
            using (IDataAccessCommand command = DataAccessAdapter.CreateCommand())
            {
                foreach (SqlTraceParameter parameter in Parameters)
                {
                    object paramValue = null;
                    bool gotValue =
                        parameter.PropertyReader.TryGetValue(out paramValue, eventCache, source, eventType, id, null,
                                                             data);
                    AddParameterToCommand(command, parameter, gotValue ? paramValue : null);
                }

                command.Execute();
            }
        }
    }
}