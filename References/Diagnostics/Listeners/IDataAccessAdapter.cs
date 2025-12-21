// THIS CODE AND INFORMATION ARE PROVIDED AS IS WITHOUT WARRANTY OF ANY
// KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE
// IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A
// PARTICULAR PURPOSE.
using System.Data;

namespace Ukadc.Diagnostics.Listeners
{
    /// <summary>
    /// The interface exposed by all data access adaptors
    /// </summary>
    public interface IDataAccessAdapter
    {
        /// <summary>
        /// Initializes the <see cref="SqlDataAccessAdapter"/>
        /// </summary>
        /// <param name="connectionString">The connection string</param>
        /// <param name="commandText">The text for the command</param>
        /// <param name="commandType">The type of the command</param>
        void Initialize(string connectionString, string commandText, CommandType commandType);
        /// <summary>
        /// Creates a new <see cref="IDataAccessCommand"/>.
        /// </summary>
        /// <returns>a new <see cref="IDataAccessCommand"/></returns>
        IDataAccessCommand CreateCommand();
    }
}