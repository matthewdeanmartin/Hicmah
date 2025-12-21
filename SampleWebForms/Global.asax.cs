using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;

namespace SampleWinForms
{
    public class Global : System.Web.HttpApplication
    {

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern bool AllocConsole();

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool FreeConsole();

        public Process powershell;
        void Application_Start(object sender, EventArgs e)
        {

            AllocConsole();
            Console.WriteLine("Hello to the console!");

            /*
            Process p = Process.GetCurrentProcess();
            p.OutputDataReceived += new DataReceivedEventHandler(p_OutputDataReceived);
            
            
            powershell = Process.Start(@"C:\Windows\System32\WindowsPowershell\v1.0\powershell.exe");
            
            //Console.ReadLine();//Blocks but console stays up until dev server ends.
            Trace.WriteLine("Hello to trace");
            Debug.WriteLine("Hello to debug");
            Console.WriteLine("Hello to the console, again!");
            p.StartInfo.RedirectStandardInput = true;
             */
            //p.StandardInput.WriteLine("Hello to standard input of current process");
        }

        void p_OutputDataReceived(object sender, DataReceivedEventArgs e)
        {
            powershell.StandardInput.Write(e.Data);
        }

        void Application_End(object sender, EventArgs e)
        {
            FreeConsole();
            //  Code that runs on application shutdown

        }

        void Application_Error(object sender, EventArgs e)
        {
            // Code that runs when an unhandled error occurs

        }

        void Session_Start(object sender, EventArgs e)
        {
            // Code that runs when a new session is started

        }

        void Session_End(object sender, EventArgs e)
        {
            // Code that runs when a session ends. 
            // Note: The Session_End event is raised only when the sessionstate mode
            // is set to InProc in the Web.config file. If session mode is set to StateServer 
            // or SQLServer, the event is not raised.

        }

    }
}
