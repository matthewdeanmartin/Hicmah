using System;
using System.Collections;
using System.Security.Principal;
using System.Web;
using System.Web.Profile;

namespace Wrappers.WebContext
{
    /// <summary>
    /// Kind of sort of is a facory, but the reall HttpContext only returns concrete
    /// classes and a factory suitable for unit testing needs to return interfaces
    /// </summary>
    public class HttpContextWrapper : IHttpContext
    {
        //Must wrap the following.
        private HttpSessionStateWrapper _sessionWrapper = new HttpSessionStateWrapper();
        private CacheWrapper _cacheWrapper = new CacheWrapper();


        public object GetService(Type serviceType)
        {
            //Is this public or private?
            throw new NotImplementedException();
        }

        public void AddError(Exception errorInfo)
        {
            HttpContext.Current.AddError(errorInfo);
        }

        public void ClearError()
        {
            HttpContext.Current.ClearError();
        }

        public object GetConfig(string name)
        {
            //obsolete method
            throw new NotSupportedException("GetConfig is obsolete. Stop using it. Now.");
            //return HttpContext.Current.GetConfig(name);
        }

        public object GetSection(string sectionName)
        {
            return HttpContext.Current.GetSection(sectionName);
        }

        public void RemapHandler(IHttpHandler handler)
        {
            HttpContext.Current.RemapHandler(handler);
        }

        public void RewritePath(string path)
        {
            HttpContext.Current.RewritePath(path);
        }

        public void RewritePath(string path, bool rebaseClientPath)
        {
            HttpContext.Current.RewritePath(path,rebaseClientPath);
        }

        public void RewritePath(string filePath, string pathInfo, string queryString)
        {
            HttpContext.Current.RewritePath(filePath,pathInfo,queryString);
        }

        public void RewritePath(string filePath, string pathInfo, string queryString, bool setClientFilePath)
        {
            HttpContext.Current.RewritePath( filePath,  pathInfo,  queryString,  setClientFilePath);
        }

        public Exception[] AllErrors
        {
            get { return HttpContext.Current.AllErrors; }
        }

        public HttpApplicationState Application
        {
            get { return HttpContext.Current.Application; }
        }

        /// <summary>
        /// Access to global asax here.
        /// </summary>
        public HttpApplication ApplicationInstance
        {
            get { return HttpContext.Current.ApplicationInstance; }
            set { HttpContext.Current.ApplicationInstance= value; }
        }

        public ICacheWrapper Cache
        {
            get { return _cacheWrapper; }
        }

        public IHttpHandler CurrentHandler
        {
            get { return HttpContext.Current.CurrentHandler; }
        }

        public RequestNotification CurrentNotification
        {
            get { return HttpContext.Current.CurrentNotification; }
        }

        public Exception Error
        {
            get { return HttpContext.Current.Error; }
        }

        public IHttpHandler Handler
        {
            get { return HttpContext.Current.Handler; }
            set { HttpContext.Current.Handler = value; }
        }

        public bool IsCustomErrorEnabled
        {
            get { return HttpContext.Current.IsCustomErrorEnabled; }
        }

        public bool IsDebuggingEnabled
        {
            get { return HttpContext.Current.IsDebuggingEnabled; }
        }

        public bool IsPostNotification
        {
            get { return HttpContext.Current.IsPostNotification; }
        }

        public IDictionary Items
        {
            get { return HttpContext.Current.Items; }
        }

        public IHttpHandler PreviousHandler
        {
            get { return HttpContext.Current.PreviousHandler; }
        }

        public ProfileBase Profile
        {
            get { return HttpContext.Current.Profile; }
        }

        public HttpRequest Request
        {
            get { return HttpContext.Current.Request; }
        }

        public HttpResponse Response
        {
            get { return HttpContext.Current.Response; }
        }

        public HttpServerUtility Server
        {
            get { return HttpContext.Current.Server; }
        }

        public IHttpSessionState Session
        {
            get { return _sessionWrapper; }
        }

        public bool SkipAuthorization
        {
            get { return HttpContext.Current.SkipAuthorization; }
            set { HttpContext.Current.SkipAuthorization=value; }
        }

        public DateTime Timestamp
        {
            get { return HttpContext.Current.Timestamp; }
        }

        public TraceContext Trace
        {
            get { return HttpContext.Current.Trace; }
        }

        public IPrincipal User
        {
            get { return HttpContext.Current.User; }
            set { HttpContext.Current.User = value; }
        }
    }

    public interface IHttpContext : IServiceProvider
    {
        void AddError(Exception errorInfo);
        void ClearError();

        [Obsolete("The recommended alternative is System.Web.HttpContext.GetSection in System.Web.dll. http://go.microsoft.com/fwlink/?linkid=14202")]
        object GetConfig(string name);

        object GetSection(string sectionName);
        void RemapHandler(IHttpHandler handler);
        void RewritePath(string path);
        void RewritePath(string path, bool rebaseClientPath);
        void RewritePath(string filePath, string pathInfo, string queryString);
        void RewritePath(string filePath, string pathInfo, string queryString, bool setClientFilePath);
        Exception[] AllErrors { get; }
        HttpApplicationState Application { get; }
        HttpApplication ApplicationInstance { get; set; }
        ICacheWrapper Cache { get; } //Different from real HttpContext
        IHttpHandler CurrentHandler { get; }
        RequestNotification CurrentNotification { get; }
        Exception Error { get; }
        IHttpHandler Handler { get; set; }
        bool IsCustomErrorEnabled { get; }
        bool IsDebuggingEnabled { get; }
        bool IsPostNotification { get;}
        IDictionary Items { get; }
        IHttpHandler PreviousHandler { get; }
        ProfileBase Profile { get; }
        HttpRequest Request { get; }
        HttpResponse Response { get; }
        HttpServerUtility Server { get; }
        IHttpSessionState Session { get; } //Differs real HttpContext

        bool SkipAuthorization { get; set; }

        DateTime Timestamp { get; }
        TraceContext Trace { get; }

        IPrincipal User { get;  set; }
    }

  

}
