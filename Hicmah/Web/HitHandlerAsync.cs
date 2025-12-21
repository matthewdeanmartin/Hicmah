using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading;
using System.Web;
using System.Configuration;
using Hicmah.Recorders;
using Hicmah.Web;
using Wrappers.WebContext;
using HttpRequestWrapper = System.Web.HttpRequestWrapper;

namespace Hicmah
{
    /// <summary>
    /// The idea is to give up the worker thread, run the sql on another thread and
    /// return to a worker thread to finish the response. I think.
    /// 
    /// The ADO library also attempts to do it's call asynch so that it doesn't block either.
    /// 
    /// This requires the connection string to have ASYCH=True
    /// 
    /// It is complicated enough that it is hard to say if this improve performance, or if
    /// it does only under certain load conditions. 
    /// </summary>
    public class HitHandlerAsync : IHttpAsyncHandler 
    {
        public void ProcessRequest(HttpContext context)
        {
            if (context == null)
                throw new ArgumentNullException();
            //Code must not go here?
            throw new InvalidOperationException();
        }

        public bool IsReusable
        {
            get { return true; }
        }

        public IAsyncResult BeginProcessRequest(HttpContext context, AsyncCallback cb, object extraData)
        {
            System.Diagnostics.Trace.WriteLine("Starting something");

            context.Response.Write("<p>Begin IsThreadPoolThread is " + Thread.CurrentThread.IsThreadPoolThread + "</p>\r\n");
            AsynchOperation asynch = new AsynchOperation(cb, context, extraData);
            asynch.StartAsyncWork();
            return asynch;
        }

        public void EndProcessRequest(IAsyncResult result)
        {
            System.Diagnostics.Trace.WriteLine("Ending ...");
        }
    }

    class AsynchOperation : IAsyncResult
    {
        private bool _completed;
        private Object _state;
        private AsyncCallback _callback;
        private HttpContext _context;

        bool IAsyncResult.IsCompleted { get { return _completed; } }
        WaitHandle IAsyncResult.AsyncWaitHandle { get { return null; } }
        Object IAsyncResult.AsyncState { get { return _state; } }
        bool IAsyncResult.CompletedSynchronously { get { return false; } }

        public AsynchOperation(AsyncCallback callback, HttpContext context, Object state)
        {


            _callback = callback;
            _context = context;
            _state = state;
            _completed = false;
        }

        public void StartAsyncWork()
        {
            //BUG: This uses same threads as ASP.NET, i.e. no gain.
            ThreadPool.QueueUserWorkItem(new WaitCallback(StartAsyncTask), null);
        }

        private void StartAsyncTask(Object workItemState)
        {

            //Slow task goes here (write to DB)

            Hit currentHit = HitFactory.Bind(new HttpRequestWrapper(_context.Request));

            //Cache not available here!  Give it a null cache, pretend everything is a cache miss
            using (HitRecorderMsSqlCached recorder = new HitRecorderMsSqlCached(new NullCacheWrapper()))
            {
                recorder.InsertHit(currentHit);
            }
            

            _context.Response.Write("<p>Completion IsThreadPoolThread is " + Thread.CurrentThread.IsThreadPoolThread + "</p>\r\n");

            _context.Response.Write("Hello World from Async Handler!");
            _completed = true;
            _callback(this);
        }
    }

}
