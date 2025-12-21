using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Remoting.Messaging;

namespace Ukadc.Diagnostics.Utils
{
    /// <summary>
    /// Static class used to inject the RelatedActivityId into Thread Local Storage (<see cref="CallContext" />)
    /// during a TraceTransfer
    /// </summary>
    public static class RelatedActivityIdStore
    {
        internal const string RELATED_ACTIVITY_ID_KEY = "Ukadc.Diagnostics.RelatedActivityId";

        /// <summary>
        /// Injects the RelatedActivityId into Thread Local Storage (<see cref="CallContext" />). 
        /// Can be used with a using block to remove from TLS on disposal.
        /// </summary>
        /// <param name="relatedActivityId">The related activity id of the trace transfer</param>
        /// <returns>An <see cref="IDisposable"/> type that removes the RelatedActivityId from <see cref="CallContext" />
        /// when Dispose is called.</returns>
        [SuppressMessage("Microsoft.Security",
            "CA2122:DoNotIndirectlyExposeMethodsWithLinkDemands")]
        public static IDisposable SetRelatedActivityId(Guid relatedActivityId)
        {
            CallContext.SetData(RELATED_ACTIVITY_ID_KEY, relatedActivityId);
            return new RemoveOnDispose();
        }

        /// <summary>
        /// Removeds the RelatedActivityId from Thread Local Storage (<see cref="CallContext" />).
        /// </summary>
        [SuppressMessage("Microsoft.Security",
            "CA2122:DoNotIndirectlyExposeMethodsWithLinkDemands")]
        public static void RemoveRelatedActivityId()
        {
            CallContext.FreeNamedDataSlot(RELATED_ACTIVITY_ID_KEY);
        }

        /// <summary>
        /// Tries to retrieve the RelatedActivityId from Thread Local Storage (<see cref="CallContext"/>)
        /// </summary>
        /// <param name="relatedActivityId">The related activity id retrieved</param>
        /// <returns>Indicates whether the RelatedActivityId exists in <see cref="CallContext"/>.</returns>
        [SuppressMessage("Microsoft.Security",
            "CA2122:DoNotIndirectlyExposeMethodsWithLinkDemands")]
        public static bool TryGetRelatedActivityId(out Guid relatedActivityId)
        {
            object data = CallContext.GetData(RELATED_ACTIVITY_ID_KEY);
            if (data != null)
            {
                relatedActivityId = (Guid) data;
                return true;
            }
            relatedActivityId = Guid.Empty;
            return false;
        }

        /// <summary>
        /// Private implementation of <see cref="IDisposable" /> returned when <see cref="RelatedActivityIdStore.SetRelatedActivityId"/>
        /// is called.
        /// </summary>
        private class RemoveOnDispose : IDisposable
        {
            void IDisposable.Dispose()
            {
                RemoveRelatedActivityId();
            }
        }
    }
}