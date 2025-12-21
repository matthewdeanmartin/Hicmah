// THIS CODE AND INFORMATION ARE PROVIDED AS IS WITHOUT WARRANTY OF ANY
// KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE
// IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A
// PARTICULAR PURPOSE.
using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Security.Principal;
using System.Threading;

namespace Ukadc.Diagnostics.Utils.PropertyReaders
{
    /// <summary>
    /// Can compare against and get the value of the CurrentThread's IPrincipal. Caution, in an ASP.NET application the Principal isn't set
    /// until after BeginRequest and therefore this would return null before that point. 
    /// </summary>
    public class PrincipalNamePropertyReader : StringPropertyReader
    {
        /// <summary>
        /// Reads the current principal name
        /// </summary>
        /// <param name="value">The value of Thread.CurrentPrincipal.Identity.Name or none if no principal is defined</param>
        /// <param name="cache">Unused</param>
        /// <param name="source">Unused</param>
        /// <param name="eventType">Unused</param>
        /// <param name="id">Unused</param>
        /// <param name="formatOrMessage">Unused</param>
        /// <param name="data">Unused</param>
        /// <returns>The current principal name or null if none is defined</returns>
        public override bool TryGetValue(out object value, TraceEventCache cache, string source,
                                         TraceEventType eventType, int id, string formatOrMessage, object[] data)
        {
            if (Thread.CurrentPrincipal != null && Thread.CurrentPrincipal.Identity != null)
            {
                value = Thread.CurrentPrincipal.Identity.Name;
                return true;
            }
            else
            {
                value = null;
                return false;
            }
        }
    }
}