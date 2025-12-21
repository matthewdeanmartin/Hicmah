using System;
namespace Ukadc.Diagnostics.Utils
{
    /// <summary>
    /// Interface definition of an internal logger
    /// </summary>
    public interface IInternalLogger
    {
        /// <summary>
        /// Log an exception
        /// </summary>
        /// <param name="exc">The exception to log</param>
        void LogException(Exception exc);

        /// <summary>
        /// Log informational message
        /// </summary>
        /// <param name="message">The message to log</param>
        void LogInformation(string message);
    }
}
