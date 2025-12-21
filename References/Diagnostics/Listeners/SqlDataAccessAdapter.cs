// THIS CODE AND INFORMATION ARE PROVIDED AS IS WITHOUT WARRANTY OF ANY
// KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE
// IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A
// PARTICULAR PURPOSE.
using System.Data;

namespace Ukadc.Diagnostics.Listeners
{
    /// <summary>
    /// An implementation of the <see cref="IDataAccessAdapter"/> interface for use with the <see cref="SqlTraceListener"/> that 
    /// talks to SQL server via an <see cref="IDataAccessCommand"/>.
    /// </summary>
    public class SqlDataAccessAdapter : IDataAccessAdapter
    {
        /// <summary>
        /// Gets the connection string
        /// </summary>
        public string ConnectionString
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the text for the command
        /// </summary>
        public string CommandText
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the type of the command
        /// </summary>
        public CommandType CommandType
        {
            get;
            private set;
        }

        /// <summary>
        /// Initializes the <see cref="SqlDataAccessAdapter"/>
        /// </summary>
        /// <param name="connectionString">The connection string</param>
        /// <param name="commandText">The text for the command</param>
        /// <param name="commandType">The type of the command</param>
        public void Initialize(string connectionString, string commandText, CommandType commandType)
        {
            ConnectionString = connectionString;
            CommandText = commandText;
            CommandType = commandType;
        }

        /// <summary>
        /// Creates a new <see cref="IDataAccessCommand"/>.
        /// </summary>
        /// <returns>a new <see cref="IDataAccessCommand"/></returns>
        public IDataAccessCommand CreateCommand()
        {
            return new SqlDataAccessCommand(ConnectionString, CommandText, CommandType);
        }
    }
}