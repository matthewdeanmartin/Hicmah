using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

namespace Ukadc.Diagnostics
{
    public class ExtendedTraceEventCache : TraceEventCache
    {
        public string ExtendedThreadId { get; set; }
        public int ExtendedProcessId { get; set; }
        public string ExtendedProcessName { get; set; }
    }
}
