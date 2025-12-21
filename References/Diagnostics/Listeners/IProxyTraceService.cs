using System;
using System.Collections.Generic;
using System.Text;
using System.ServiceModel;

namespace Ukadc.Diagnostics.Listeners
{
    /// <summary>
    /// Service contract used by the ProxyTraceListener to send events remotely
    /// </summary>
    [ServiceContract]
    public interface IProxyTraceService
    {
        /// <summary>
        /// Used to send a <see cref="TraceEvent"/> to a remote ProxyTraceService
        /// </summary>
        /// <param name="traceEvent"></param>
        [OperationContract(IsOneWay=true)]
        void SendTraceEvent(TraceEvent traceEvent);
    }
}
