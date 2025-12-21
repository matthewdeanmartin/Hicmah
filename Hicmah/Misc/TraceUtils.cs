using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Hicmah.Misc
{
    public class TraceUtils
    {
        static TraceSource source = new TraceSource("TraceUtils");
        

        public static void TraceError(string message)
        {
               source.TraceData(TraceEventType.Warning,0,message); 
                //Trace.TraceWarning(message);
                //Trace.WriteLine(message);
            
        }

        public static void TraceInfo(string message)
        {
                source.TraceData(TraceEventType.Information, 0, message); 
                //Trace.TraceInformation(message);
                //Trace.WriteLine(message);
            
        }

        public static void TraceWarn(string message)
        {
                source.TraceData(TraceEventType.Warning, 0, message); 
                //Trace.TraceWarning(message);
                //Trace.WriteLine(message);
            
        }

        public static void TraceVerbose(string message)
        {
            source.TraceData(TraceEventType.Verbose, 0, message); 
            //Trace.WriteLine(message);
            
        }
    }
}
