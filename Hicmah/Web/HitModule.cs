using System;
using System.Diagnostics;
using System.Reflection;
using System.Web;
using Hicmah.Misc;
using Hicmah.Web;
using Ukadc.Diagnostics;
using HttpRequestWrapper = System.Web.HttpRequestWrapper;

namespace Hicmah
{
    /// <summary>
    /// This can capture all hits to any file that asp.net is handling. By default
    /// </summary>
    /// <remarks>
    /// ASP.NET in IIS does not handle all files. In the development server it does.
    /// ASP.NET can handle all files if wildcard mapping is being used, but it a lower
    /// performance scenario than letting IIS handle static content.
    /// </remarks>
    public class HitModule : IHttpModule
    {

        private Stopwatch watch;

        public void Dispose()
        {
            
        }

        public void Init(HttpApplication context)
        {
               watch = new Stopwatch();

            //Register event handlers here
            context.BeginRequest += ApplicationBeginRequest;
            context.EndRequest += ApplicationEndRequest;

            context.Error += context_Error;

            //Can throw  "This operation requires IIS integrated pipeline mode." from IIS6
            //string s = context.Request.ServerVariables["SERVER_SOFTWARE"];
            //if (context.Request.ServerVariables["SERVER_SOFTWARE"]=="foobar")
            //{
            //    context.LogRequest += context_LogRequest;
            //    context.PostLogRequest += ContextPostLogRequest;
            //}
        }


        private void context_Error(object sender, EventArgs e)
        {
            ExtendedSource trace = new ExtendedSource("Web.HitModule");

            trace.TraceEvent(TraceEventType.Information,5,"Getting reading to record a hit that threw an exception, will overwrite HTTP status with 500 if it less than 300");

            HttpApplication context = sender as HttpApplication;
            if (context == null) throw new InvalidOperationException("can't find context");

            Hit currentHit = HitFactory.Bind(new HttpRequestWrapper(context.Request));
            currentHit.HitDate = DateTime.Now;

            //Is this an error?
            Exception exception = context.Server.GetLastError();
            if (exception is HttpException)
            {
                currentHit.StatusCode = ((HttpException)exception).GetHttpCode();
            }
            else
            {
                //This can be 200, even though the Response is a yellow page!
                if (context.Response.StatusCode < 300)
                    currentHit.StatusCode = 500;//If we don't set a reasonable status code, then some reports break.
                else
                    currentHit.StatusCode = context.Response.StatusCode;
            }

            GenericHitProcessor processor = new GenericHitProcessor();

            //Push the stop watch and milliseconds to very end...
            watch.Stop();
            //BUG: long to int conversion
            currentHit.ServerDuration = (int)watch.ElapsedMilliseconds;
            processor.RecordHitToCurrentProvider(currentHit);
        }

        private void ApplicationBeginRequest(Object source,
         EventArgs e) {
            
            HttpApplication context = source as HttpApplication;
            if (context == null) throw new InvalidOperationException("can't find context");

            //context.Context.Items["RequestStart"] = watch.ElapsedMilliseconds;

            //string s = context.Request.Url.ToString();
            //Trace.WriteLine("starting: " + s);

            watch.Start();
         }

        private void ApplicationEndRequest(Object source,
         EventArgs e) {

             TraceUtils.TraceVerbose("Entering end of HttpModule to record hit");

             HttpApplication context = source as HttpApplication;
             if (context == null) throw new InvalidOperationException("can't find context");

             Hit currentHit = HitFactory.Bind(new HttpRequestWrapper(context.Request));
             currentHit.HitDate = DateTime.Now;
            
            //This never catches errors
            currentHit.StatusCode= context.Response.StatusCode;

            //To record anything that depends on the text of the response page, we need a filter.
            //string s = context.Response.OutputStream.Rs
            //if(context.Response.OutputStream.
            //currentHit.ResponseBytes = context.Response.OutputStream.Length;

             GenericHitProcessor processor = new GenericHitProcessor();
          
                 //Push the stop watch and milliseconds to very end...
                 watch.Stop();

            //Returns Hicmah. We'd have to cycle through the loaded assemblies and pick one to find out the web app assembly.
                 //Trace.WriteLine("Executing assembly " + Assembly.GetExecutingAssembly().FullName);//Hicmah
                 //Trace.WriteLine("Calling assembly " + Assembly.GetCallingAssembly().FullName);//System.Web
                 //Trace.WriteLine("Entry assembly " + Assembly.GetEntryAssembly().FullName);//Null

                 //BUG: long to int conversion
                 currentHit.ServerDuration = (int)watch.ElapsedMilliseconds;
                 processor.RecordHitToCurrentProvider(currentHit);
             

        }
    }
}
