// THIS CODE AND INFORMATION ARE PROVIDED AS IS WITHOUT WARRANTY OF ANY
// KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE
// IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A
// PARTICULAR PURPOSE.

using System.Configuration;

namespace Ukadc.Diagnostics.Listeners.Configuration
{
    /// <summary>
    /// Reads the configuration file for an SmtpTraceListener element
    /// </summary>
    public class SmtpTraceListenerElement : ConfigurationElement
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
        /// Get/Set the host name
        /// </summary>
        [ConfigurationProperty(HOST, IsRequired = true)]
        public string Host
        {
            get { return (string) base[HOST]; }
            set { base[HOST] = value; }
        }

        /// <summary>
        /// Get/Set the port number
        /// </summary>
        [ConfigurationProperty(PORT, IsRequired=true)]
        public int Port
        {
            get { return (int) base[PORT]; }
            set { base[PORT] = value; }
        }

        /// <summary>
        /// Get/Set the username
        /// </summary>
        [ConfigurationProperty(USERNAME, IsRequired = false)]
        public string Username
        {
            get { return (string)base[USERNAME]; }
            set { base[USERNAME] = value; }
        }

        /// <summary>
        /// Get/Set the password
        /// </summary>
        [ConfigurationProperty(PASSWORD, IsRequired = false)]
        public string Password
        {
            get { return (string)base[PASSWORD]; }
            set { base[PASSWORD] = value; }
        }

        /// <summary>
        /// Get/Set the email subject
        /// </summary>
        [ConfigurationProperty(SUBJECT, IsRequired=true)]
        public string Subject
        {
            get { return (string) base[SUBJECT]; }
            set { base[SUBJECT] = value; }
        }

        /// <summary>
        /// Get/Set the email bidy
        /// </summary>
        [ConfigurationProperty(BODY, IsRequired=true)]
        public string Body
        {
            get { return (string) base[BODY]; }
            set { base[BODY] = value; }
        }

        /// <summary>
        /// Get/Set the recipient
        /// </summary>
        [ConfigurationProperty(TO, IsRequired=true)]
        public string To
        {
            get { return (string) base[TO]; }
            set { base[TO] = value; }
        }

        /// <summary>
        /// Get/Set the sender
        /// </summary>
        [ConfigurationProperty(FROM, IsRequired=true)]
        public string From
        {
            get { return (string) base[FROM]; }
            set { base[FROM] = value; }
        }

        internal const string ELEMENT_NAME = "smtpTraceListener";

        private const string NAME = "name";
        private const string HOST = "host";
        private const string PORT = "port";
        private const string USERNAME = "username";
        private const string PASSWORD = "password";
        private const string SUBJECT = "subject";
        private const string BODY = "body";
        private const string TO = "to";
        private const string FROM = "from";
    }
}