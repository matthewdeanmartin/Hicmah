// THIS CODE AND INFORMATION ARE PROVIDED AS IS WITHOUT WARRANTY OF ANY
// KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE
// IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A
// PARTICULAR PURPOSE.
using System;
using System.Data;
using System.Data.SqlClient;

namespace Ukadc.Diagnostics.Listeners
{
    /// <summary>
    /// Represents a <see cref="SqlCommand"/> for use by the <see cref="SqlTraceListener"/>.
    /// </summary>
    public sealed class SqlDataAccessCommand : IDataAccessCommand
    {
        private bool _disposed;
        private SqlConnection _connection;
        private SqlCommand _command;

        /// <summary>
        /// Creates a new <see cref="SqlDataAccessCommand" />
        /// </summary>
        /// <param name="connectionString">The connection string to use</param>
        /// <param name="commandText">The command text (SQL text or Stored Procedure based on the <see cref="System.Data.CommandType"/> parameter)</param>
        /// <param name="commandType">Specifies whether the 'commandText' parameter is Text or a StoredProcedure call</param>
        public SqlDataAccessCommand(string connectionString, string commandText, CommandType commandType)
        {
            _connection = new SqlConnection(connectionString);
            _command = _connection.CreateCommand();
            _command.CommandText = commandText;
            _command.CommandType = commandType;
            // TODO _command.CommandTimeout = ;
        }

        /// <summary>
        /// Adds a parameter/value pair to the the command
        /// </summary>
        /// <param name="name">The name of the parameter (e.g. @Message)</param>
        /// <param name="value">The value the parameter specifies</param>
        public void AddParameter(string name, object value)
        {
            if (ReferenceEquals(null, value))
            {
                value = DBNull.Value;
            }
            _command.Parameters.Add(new SqlParameter(name, value));
        }

        /// <summary>
        /// Executes the command (and opens the connection).
        /// </summary>
        public void Execute()
        {
            _connection.Open();
            _command.ExecuteNonQuery();
        }

        /// <summary>
        /// Disposes the internal <see cref="SqlCommand"/> and <see cref="SqlConnection" />.
        /// </summary>
        public void Dispose()
        {
            if (!_disposed)
            {
                _disposed = true;
                _command.Dispose();
                _connection.Dispose();
            }
        }
    }
}