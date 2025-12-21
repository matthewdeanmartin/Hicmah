// THIS CODE AND INFORMATION ARE PROVIDED AS IS WITHOUT WARRANTY OF ANY
// KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE
// IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A
// PARTICULAR PURPOSE.
using System;
using System.Diagnostics;

namespace Ukadc.Diagnostics.Utils.PropertyReaders
{
    /// <summary>
    /// Can compare against and get the value of TraceEvent's ProcessId
    /// </summary>
    public class ProcessNamePropertyReader : StringPropertyReader
    {
        private readonly static bool _readOK;
        private readonly static string _processName;

        static ProcessNamePropertyReader()
        {
            try
            {
                _processName = Process.GetCurrentProcess().ProcessName;
                _readOK = true;
            }
            catch (Exception exc)
            {
                _readOK = false;
                DefaultServiceLocator.GetService<IInternalLogger>().LogException(exc);
            }
        }

        /// <summary>
        /// Return the process name
        /// </summary>
        /// <param name="value">The process name</param>
        /// <param name="cache">Unused</param>
        /// <param name="source">Unused</param>
        /// <param name="eventType">Unused</param>
        /// <param name="id">Unused</param>
        /// <param name="formatOrMessage">Unused</param>
        /// <param name="data">Unused</param>
        /// <returns>True</returns>
        public override bool TryGetValue(out object value, TraceEventCache cache, string source,
                                         TraceEventType eventType, int id, string formatOrMessage, object[] data)
        {
            ExtendedTraceEventCache ext = cache as ExtendedTraceEventCache;

            if (ext != null)
            {
                value = ext.ExtendedProcessName;
                return true;
            }

            value = _processName;
            return _readOK;
        }
    }
}