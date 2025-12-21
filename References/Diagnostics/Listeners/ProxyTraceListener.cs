using System;
using System.Collections.Generic;
using System.Text;
using System.ServiceModel;

namespace Ukadc.Diagnostics.Listeners
{
    // TODO - configuration options should specify the behavior for faulted proxy clients

    /// <summary>
    /// The ProxyTraceListener uses WCF to send logging events to a remote listening service.
    /// </summary>
    /// <remarks>By default the ProxyTraceListener targets a named pipe listener (NetNamedPipeBinding) with
    /// no transport security at the url net.pipe://localhost/ProxyTraceService/.
    /// Any implementation of <see cref="IProxyTraceService"/> can be used as a service to listen to these events 
    /// (even in-process ones). To modify the binding used by the ProxyTraceListener use the constructor
    /// that takes a configuration name. This will look in the system.serviceModel configuration section to configure
    /// the client endpoint.
    /// <code>
    /// &lt;client>
    ///  &lt;endpoint address="http://localhost/ProxyTraceService/"
    ///            binding="basicHttpBinding"
    ///            contract="Ukadc.Diagnostics.Listeners.IProxyTraceService" 
    ///            name="customWcfClient"/>
    /// &lt;/client>
    /// </code>
    /// </remarks>
    public class ProxyTraceListener : CustomTraceListener
    {
        /// <summary>
        /// The endpoint address for the ProxyTraceListener's default binding.
        /// </summary>
        public static readonly Uri DefaultEndpointAddress = new Uri("net.pipe://localhost/ProxyTraceService/", UriKind.Absolute);
        private IProxyTraceService _client;
        private ICommunicationObject _commObj;
        private readonly object _padlock = new object();
        private readonly string _configurationName;

        /// <summary>
        /// Creates an instance of ProxyTraceListener using the default unsecured NetNamedPipeBinding.
        /// </summary>
        public ProxyTraceListener() 
            : base("ProxyTraceListener")
        {
            InitChannel();
        }

        /// <summary>
        /// Creates an instance of ProxyTracelistener using the client endpoint name specified.
        /// </summary>
        /// <param name="configurationName">The system.serviceModel client endpoint name</param>
        public ProxyTraceListener(string configurationName)
            : base("ProxyTraceListener")
        {
            _configurationName = configurationName;
            InitChannel();
        }

        /// <summary>
        /// Creates a new client channel if the communication object is null or
        /// is faulted, closed or closing.
        /// </summary>
        private void InitChannel()
        {
            lock (_padlock)
            {
                if (_commObj == null || 
                    (_commObj.State != CommunicationState.Opened &&
                    _commObj.State != CommunicationState.Opening))
                {
                    if (!string.IsNullOrEmpty(_configurationName))
                    {
                        _client = new ChannelFactory<IProxyTraceService>(_configurationName).CreateChannel();
                    }
                    else
                    {
                        // Todo - is None really the best choice of security mode?
                        // maybe we should secure the transport by default???
                        NetNamedPipeBinding binding = new NetNamedPipeBinding(NetNamedPipeSecurityMode.None);
                        EndpointAddress address = new EndpointAddress(DefaultEndpointAddress);
                        _client = ChannelFactory<IProxyTraceService>.CreateChannel(binding, address);
                        
                    }
                    _commObj = (ICommunicationObject)_client;
                }
            }
        }

        /// <summary>
        /// Override of TraceEventCore that handles the sending of data via the WCF client proxy.
        /// </summary>
        protected override void TraceEventCore(System.Diagnostics.TraceEventCache eventCache, string source, System.Diagnostics.TraceEventType eventType, int id, string message)
        {
            if (_commObj.State == CommunicationState.Faulted)
            {
                InitChannel();
            }
            TraceEvent traceEvent = new TraceEvent(eventCache, source, eventType, id, message);
            _client.SendTraceEvent(traceEvent);
        }

        /// <summary>
        /// Override of TraceDataCore that handles the sending of data via the WCF client proxy.
        /// </summary>
        protected override void TraceDataCore(System.Diagnostics.TraceEventCache eventCache, string source, System.Diagnostics.TraceEventType eventType, int id, params object[] data)
        {
            if (_commObj.State == CommunicationState.Faulted)
            {
                InitChannel();
            }
            // TODO - check for objects that aren't serializable (e.g. IXPathNavigable).
            TraceEvent traceEvent = new TraceEvent(eventCache, source, eventType, id, data);
            _client.SendTraceEvent(traceEvent);
        }
    }
}
