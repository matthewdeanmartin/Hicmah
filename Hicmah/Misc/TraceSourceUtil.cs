using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Ukadc.Diagnostics;

namespace Hicmah.Misc
{
    public class TraceSourceUtil : ExtendedSource
    {
        public TraceSourceUtil(string sourceName) : base(sourceName)
        {
        }

        public TraceSourceUtil(string sourceName, SourceLevels defaultLevel) : base(sourceName, defaultLevel)
        {
        }


        /// <summary>
        /// Information
        /// </summary>
        /// <param name="message"></param>
        public void WriteLine(string message)
        {
            this.TraceInformation(message);
        }
        public void WriteLine(string message,params object[] args)
        {
            this.TraceInformation(message,args);
        }
    }
}
