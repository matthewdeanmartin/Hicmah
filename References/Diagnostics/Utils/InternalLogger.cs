using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

namespace Ukadc.Diagnostics.Utils
{
    internal class InternalLogger : IInternalLogger
    {
        /// <summary>
        /// Logs any internal failures to an attached debugger or output debug string if no debugger is attached
        /// </summary>
        /// <param name="exc"></param>
        public void LogException(Exception exc)
        {
            if (Debugger.IsLogging())
            {
                Debugger.Log(5, "Error", exc.ToString());
            }
            else
            {
                SafeNativeMethods.OutputDebugString(exc.ToString());
            }
        }

        public void LogInformation(string message)
        {
            if (Debugger.IsLogging())
            {
                Debugger.Log(3, "Information", message);
            }
            else
            {
                SafeNativeMethods.OutputDebugString(message);
            }
        }
    }
}
