// THIS CODE AND INFORMATION ARE PROVIDED AS IS WITHOUT WARRANTY OF ANY
// KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE
// IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A
// PARTICULAR PURPOSE.
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
    /// A trace listener which outputs trace events to email
    /// </summary>
    public class SmtpTraceListener : CustomTraceListener
    {
        /// <summary>
        /// Construct the trace listener
        /// </summary>
        /// <param name="combinedFactory">A property reader factory</param>
        /// <param name="smtpService">The SMTP service</param>
        /// <param name="host">The host name</param>
        /// <param name="port">The host port number</param>
        /// <param name="username">The username used when sending an email</param>
        /// <param name="password">The password used when sending an email</param>
        /// <param name="to">The address of the user who will receive diagnostic traces</param>
        /// <param name="from">The senders address</param>
        /// <param name="subjectValueToken">A token defining the subject</param>
        /// <param name="bodyValueToken">A token defining the body</param>
        public SmtpTraceListener(IPropertyReaderFactory combinedFactory, ISmtpService smtpService, string host, int port, string username, string password, string to, string from,
                                 string subjectValueToken, string bodyValueToken)
            : base("SmtpTraceListener")
        {
            InternalConfigure(combinedFactory, smtpService, host, port, username, password, to, from, subjectValueToken, bodyValueToken);
        }

        /// <summary>
        /// Configure the trace listener based on the named configuration section
        /// </summary>
        /// <param name="sectionName">The name of the config section</param>
        public SmtpTraceListener(string sectionName)
            : base("SmtpTraceListener")
        {
            UkadcDiagnosticsSection config = UkadcDiagnosticsSection.ReadConfigSection();
            SmtpTraceListenerElement smtpTraceListenerElement = config.SmtpTraceListeners[sectionName];
            if (null == smtpTraceListenerElement)
            {
                throw new ConfigurationErrorsException(
                    string.Format(CultureInfo.CurrentCulture, Resources.SmtpTraceListenerConfigError, sectionName));
            }

            InternalConfigure(
                DefaultServiceLocator.GetService<IPropertyReaderFactory>(),
                new SmtpService(),
                smtpTraceListenerElement.Host,
                smtpTraceListenerElement.Port,
                smtpTraceListenerElement.Username,
                smtpTraceListenerElement.Password,
                smtpTraceListenerElement.To,
                smtpTraceListenerElement.From,
                smtpTraceListenerElement.Subject,
                smtpTraceListenerElement.Body);
        }

        /// <summary>
        /// Get the From property
        /// </summary>
        public string From
        {
            get;
            private set;
        }

        /// <summary>
        /// Get the To property
        /// </summary>
        public string To
        {
            get;
            private set;
        }

        /// <summary>
        /// Get the SmtpService interface
        /// </summary>
        public ISmtpService SmtpService
        {
            get;
            private set;
        }

        /// <summary>
        /// Get the subject reader
        /// </summary>
        public PropertyReader SubjectReader
        {
            get;
            private set;
        }

        /// <summary>
        /// Get the body reader
        /// </summary>
        public PropertyReader BodyReader
        {
            get;
            private set;
        }

        private void InternalConfigure(IPropertyReaderFactory readerFactory, ISmtpService smtpService, string host, int port, string username, string password, string to, string from,
                                       string subjectValueToken, string bodyValueToken)
        {
            SmtpService = smtpService;
            SmtpService.Initialize(host, port, username, password);
            From = from;
            To = to;
            SubjectReader = readerFactory.CreateCombinedReader(subjectValueToken);
            BodyReader = readerFactory.CreateCombinedReader(bodyValueToken);
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
            SendMailMessage(eventCache, source, eventType, id, message, null);
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
            SendMailMessage(eventCache, source, eventType, id, null, data);
        }

        private void SendMailMessage(TraceEventCache eventCache, string source, TraceEventType eventType, int id,
                                     string message, params object[] data)
        {
            object bodyObj, subjectObj;
            string body = null;
            string subject = null;

            // TODO Must be something we can do with generics to avoid this nonsense...
            if (BodyReader.TryGetValue(out bodyObj, eventCache, source, eventType, id, message, data))
            {
                body = bodyObj == null ? null : bodyObj.ToString();
            }
            if (SubjectReader.TryGetValue(out subjectObj, eventCache, source, eventType, id, message, data))
            {
                subject = subjectObj == null ? null : subjectObj.ToString();
            }
             
            SmtpService.SendMessage(From, To, subject, body);
        }
    }
}