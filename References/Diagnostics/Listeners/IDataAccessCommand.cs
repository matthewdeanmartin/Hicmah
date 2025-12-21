// THIS CODE AND INFORMATION ARE PROVIDED AS IS WITHOUT WARRANTY OF ANY
// KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE
// IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A
// PARTICULAR PURPOSE.
using System;

namespace Ukadc.Diagnostics.Listeners
{
    /// <summary>
    /// Defines the interface for data acces commands
    /// </summary>
    public interface IDataAccessCommand : IDisposable
    {
        /// <summary>
        /// Adds a parameter/value pair to the the command
        /// </summary>
        /// <param name="name">The name of the parameter (e.g. @Message)</param>
        /// <param name="value">The value the parameter specifies</param>
        void AddParameter(string name, object value);

        /// <summary>
        /// Executes the command.
        /// </summary>
        void Execute();
    }
}