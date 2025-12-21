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
    public class WindowsIdentityPropertyReader : StringPropertyReader
    {
        /// <summary>
        /// Get the windows identity if one is available
        /// </summary>
        /// <param name="value">The Windows identity or null</param>
        /// <param name="cache">Unused</param>
        /// <param name="source">Unused</param>
        /// <param name="eventType">Unused</param>
        /// <param name="id">Unused</param>
        /// <param name="formatOrMessage">Unused</param>
        /// <param name="data">Unused</param>
        /// <returns>True if the WIndows identity was read</returns>
        public override bool TryGetValue(out object value, TraceEventCache cache, string source,
                                         TraceEventType eventType, int id, string formatOrMessage, object[] data)
        {
            WindowsIdentity windowsIdentity = WindowsIdentity.GetCurrent();
            if (windowsIdentity != null)
            {
                value = windowsIdentity.Name;
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