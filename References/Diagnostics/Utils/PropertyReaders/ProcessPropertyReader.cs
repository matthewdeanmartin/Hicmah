using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

namespace Ukadc.Diagnostics.Utils.PropertyReaders
{
    /// <summary>
    /// Grabs the current Process object
    /// </summary>
    public class ProcessPropertyReader : PropertyReader
    {
        private readonly static bool _readOK;
        private readonly static Process _process;

        static ProcessPropertyReader()
        {
            try
            {
                _process = Process.GetCurrentProcess();
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
            value = _process;
            return _readOK;
        }

        /// <inheritdoc />
        public override Type PropertyType
        {
            get { return typeof(Process); }
        }

        /// <inheritdoc />
        public override IComparator Comparator
        {
            get { return ObjectComparator.Instance; }
        }
    }
}
