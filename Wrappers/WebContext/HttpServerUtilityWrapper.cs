using System;
using System.Collections.Specialized;
using System.IO;
using System.Web;

namespace Wrappers.WebContext
{
    public class HttpServerUtilityWrapper : IHttpServerUtility
    {
        public void ClearError()
        {
            HttpContext.Current.Server.ClearError();
        }

        public object CreateObject(string progID)
        {
            return HttpContext.Current.Server.CreateObject(progID);
        }

        public object CreateObject(Type type)
        {
            return HttpContext.Current.Server.CreateObject(type);
        }

        public object CreateObjectFromClsid(string clsid)
        {
            return HttpContext.Current.Server.CreateObjectFromClsid(clsid);
        }

        public void Execute(string path)
        {
            HttpContext.Current.Server.Execute(path);
        }

        public void Execute(string path, bool preserveForm)
        {
            HttpContext.Current.Server.Execute(path, preserveForm);
        }

        public void Execute(string path, TextWriter writer)
        {
            HttpContext.Current.Server.Execute(path, writer);
        }

        public void Execute(string path, TextWriter writer, bool preserveForm)
        {
            HttpContext.Current.Server.Execute(path, writer, preserveForm);
        }

        public void Execute(IHttpHandler handler, TextWriter writer, bool preserveForm)
        {
            HttpContext.Current.Server.Execute(handler, writer, preserveForm);
        }

        public Exception GetLastError()
        {
            return HttpContext.Current.Server.GetLastError();
        }

        public string HtmlDecode(string s)
        {
            return HttpContext.Current.Server.HtmlDecode(s);
        }

        public void HtmlDecode(string s, TextWriter output)
        {
            HttpContext.Current.Server.HtmlDecode(s,output);
        }

        public string HtmlEncode(string s)
        {
            return HttpContext.Current.Server.HtmlEncode(s);
        }

        public void HtmlEncode(string s, TextWriter output)
        {
            HttpContext.Current.Server.HtmlEncode(s,output);
        }

        public string MapPath(string path)
        {
            return HttpContext.Current.Server.MapPath(path);
        }

        public void Transfer(string path)
        {
            HttpContext.Current.Server.Transfer(path);
        }

        public void Transfer(string path, bool preserveForm)
        {
            HttpContext.Current.Server.Transfer(path, preserveForm);
        }

        public void Transfer(IHttpHandler handler, bool preserveForm)
        {
            HttpContext.Current.Server.Transfer(handler, preserveForm);
        }

        public void TransferRequest(string path)
        {
            HttpContext.Current.Server.TransferRequest(path);
        }

        public void TransferRequest(string path, bool preserveForm)
        {
            HttpContext.Current.Server.TransferRequest(path, preserveForm);
        }

        public void TransferRequest(string path, bool preserveForm, string method, NameValueCollection headers)
        {
            HttpContext.Current.Server.TransferRequest(path, preserveForm, method, headers);
        }

        public string UrlDecode(string s)
        {
            return HttpContext.Current.Server.UrlDecode(s);
        }

        public void UrlDecode(string s, TextWriter output)
        {
            HttpContext.Current.Server.UrlDecode(s, output);
        }

        public string UrlEncode(string s)
        {
            return HttpContext.Current.Server.UrlEncode(s);
        }

        public void UrlEncode(string s, TextWriter output)
        {
            HttpContext.Current.Server.UrlEncode(s, output);
        }

        public string UrlPathEncode(string s)
        {
            return HttpContext.Current.Server.UrlPathEncode(s);
        }

        public string MachineName
        {
            get { return HttpContext.Current.Server.MachineName; }
        }

        public int ScriptTimeout
        {
            get { return HttpContext.Current.Server.ScriptTimeout; }
            set { HttpContext.Current.Server.ScriptTimeout = value; }
        }
    }

    public interface IHttpServerUtility
    {
        void ClearError();
        object CreateObject(string progID);
        object CreateObject(Type type);
        object CreateObjectFromClsid(string clsid);

        void Execute(string path);
        void Execute(string path, bool preserveForm);
        void Execute(string path, TextWriter writer);
        void Execute(string path, TextWriter writer, bool preserveForm);
        void Execute(IHttpHandler handler, TextWriter writer, bool preserveForm);
        Exception GetLastError();
        string HtmlDecode(string s);
        void HtmlDecode(string s, TextWriter output);
        string HtmlEncode(string s);
        void HtmlEncode(string s, TextWriter output);
        string MapPath(string path);
        void Transfer(string path);
        void Transfer(string path, bool preserveForm);
        void Transfer(IHttpHandler handler, bool preserveForm);
        void TransferRequest(string path);
        void TransferRequest(string path, bool preserveForm);
        void TransferRequest(string path, bool preserveForm, string method, NameValueCollection headers);
        string UrlDecode(string s);
        void UrlDecode(string s, TextWriter output);
        string UrlEncode(string s);
        void UrlEncode(string s, TextWriter output);
        string UrlPathEncode(string s);

        string MachineName { get; }

        int ScriptTimeout { get; set; }
    }

}
