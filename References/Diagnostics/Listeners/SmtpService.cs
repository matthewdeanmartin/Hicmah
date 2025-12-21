// THIS CODE AND INFORMATION ARE PROVIDED AS IS WITHOUT WARRANTY OF ANY
// KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE
// IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A
// PARTICULAR PURPOSE.
using System.Net.Mail;
using System.Net;

namespace Ukadc.Diagnostics.Listeners
{
    /// <summary>
    /// An implementation of the <see cref="ISmtpService"/> that calls an SMTP server
    /// using the <see cref="SmtpClient"/> class.
    /// </summary>
    public class SmtpService : ISmtpService
    {
        /// <summary>
        /// Initializes the <see cref="SmtpService"/>.
        /// </summary>
        /// <param name="host">The name of the SMTP server</param>
        /// <param name="port">The port to use on the SMTP server</param>
        /// <param name="username">The username for authentication with the SMTP server</param>
        /// <param name="password">The password for authentication</param>
        public void Initialize(string host, int port, string username, string password)
        {
            Host = host;
            Port = port;
            Username = username;
            Password = password;
        }


        /// <summary>
        /// Sends a message using the host and port specified in the <see cref="Initialize"/> method.
        /// </summary>
        /// <param name="from">The e-mail address the e-mail will be sent from</param>
        /// <param name="to">The e-mail address the e-mail will be sent to</param>
        /// <param name="subject">The subject of the e-mail</param>
        /// <param name="body">The body of the e-mail</param>
        public void SendMessage(string from, string to, string subject, string body)
        {
            using (MailMessage mailMessage = new MailMessage(from, to, subject, body))
            {
                SmtpClient client = new SmtpClient(Host, Port);
                if (!string.IsNullOrEmpty(Username) || !string.IsNullOrEmpty(Password))
                {
                    client.Credentials = new NetworkCredential(Username, Password);
                }
                client.Send(mailMessage);
            }
        }

        /// <summary>
        /// Gets the port to use on the SMTP server
        /// </summary>
        public int Port
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the name of the SMTP server
        /// </summary>
        public string Host
        {
            get;
            private set;
        }

        /// <summary>
        /// The username for authentication with the SMTP server
        /// </summary>
        public string Username
        {
            get;
            private set;
        }

        /// <summary>
        /// The password for authentication
        /// </summary>
        public string Password
        {
            get;
            private set;
        }
    }
}