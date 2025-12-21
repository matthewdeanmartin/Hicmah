// THIS CODE AND INFORMATION ARE PROVIDED AS IS WITHOUT WARRANTY OF ANY
// KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE
// IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A
// PARTICULAR PURPOSE.
namespace Ukadc.Diagnostics.Listeners
{
    /// <summary>
    /// Definintion of the SmtpService interface
    /// </summary>
    public interface ISmtpService
    {
        /// <summary>
        /// Gets the port to use on the SMTP server
        /// </summary>
        int Port { get; }
        /// <summary>
        /// Gets the name of the SMTP server
        /// </summary>
        string Host { get; }
        /// <summary>
        /// The username for authentication with the SMTP server
        /// </summary>
        string Username { get; }
        /// <summary>
        /// The password for authentication
        /// </summary>
        string Password { get; }

        /// <summary>
        /// Initializes the <see cref="SmtpService"/>.
        /// </summary>
        /// <param name="host">The name of the SMTP server</param>
        /// <param name="port">The port to use on the SMTP server</param>
        /// <param name="username">The username for authentication with the SMTP server (null to not specify)</param>
        /// <param name="password">The password for authentication (null to not specify)</param>
        void Initialize(string host, int port, string username, string password);
        /// <summary>
        /// Sends a message using the host and port specified in the <see cref="Initialize"/> method.
        /// </summary>
        /// <param name="from">The e-mail address the e-mail will be sent from</param>
        /// <param name="to">The e-mail address the e-mail will be sent to</param>
        /// <param name="subject">The subject of the e-mail</param>
        /// <param name="body">The body of the e-mail</param>
        void SendMessage(string from, string to, string subject, string body);
    }
}