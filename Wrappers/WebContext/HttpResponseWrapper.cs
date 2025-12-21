using System;
using System.Collections;
using System.Collections.Specialized;
using System.IO;
using System.Text;
using System.Web;
using System.Web.Caching;

namespace Wrappers.WebContext
{
    /// <summary>
    /// Wrapper to make it easier to tests classes that use Response
    /// </summary>
    public class HttpResponseWrapper :IHttpResponse
    {
        public void AddCacheDependency(params CacheDependency[] dependencies)
        {
            HttpContext.Current.Response.AddCacheDependency(dependencies);
        }

        public void AddCacheItemDependencies(string[] cacheKeys)
        {
            HttpContext.Current.Response.AddCacheItemDependencies(cacheKeys);
        }

        public void AddCacheItemDependencies(ArrayList cacheKeys)
        {
            HttpContext.Current.Response.AddCacheItemDependencies(cacheKeys);
        }

        public void AddCacheItemDependency(string cacheKey)
        {
            HttpContext.Current.Response.AddCacheItemDependency(cacheKey);
        }

        public void AddFileDependencies(ArrayList filenames)
        {
            HttpContext.Current.Response.AddFileDependencies(filenames);
        }

        public void AddFileDependencies(string[] filenames)
        {
            HttpContext.Current.Response.AddFileDependencies(filenames);
        }

        public void AddFileDependency(string filename)
        {
            HttpContext.Current.Response.AddFileDependency(filename);
        }

        public void AddHeader(string name, string value)
        {
           HttpContext.Current.Response.AddHeader(name,value);
        }

        public void AppendCookie(HttpCookie cookie)
        {
            HttpContext.Current.Response.AppendCookie(cookie);
        }

        public void AppendHeader(string name, string value)
        {
            HttpContext.Current.Response.AppendHeader(name,value);
        }

        public void AppendToLog(string param)
        {
            HttpContext.Current.Response.AppendToLog(param);
        }

        public string ApplyAppPathModifier(string virtualPath)
        {
            return HttpContext.Current.Response.ApplyAppPathModifier(virtualPath);
        }

        public void BinaryWrite(byte[] buffer)
        {
            HttpContext.Current.Response.BinaryWrite(buffer);
        }

        public void Clear()
        {
            HttpContext.Current.Response.Clear();
        }

        public void ClearContent()
        {
            HttpContext.Current.Response.ClearContent();
        }

        public void ClearHeaders()
        {
            HttpContext.Current.Response.ClearHeaders();
        }

        public void Close()
        {
            HttpContext.Current.Response.Close();
        }

        public void DisableKernelCache()
        {
            HttpContext.Current.Response.DisableKernelCache();
        }

        public void End()
        {
            HttpContext.Current.Response.End();
        }

        public void Flush()
        {
            HttpContext.Current.Response.Flush();
        }

        //Ha, content filtering headers
        public void Pics(string value)
        {
            HttpContext.Current.Response.Pics(value);
        }

        public void Redirect(string url)
        {
            HttpContext.Current.Response.Redirect(url);
        }

        public void Redirect(string url, bool endResponse)
        {
            HttpContext.Current.Response.Redirect(url, endResponse);
        }

        public void SetCookie(HttpCookie cookie)
        {
            HttpContext.Current.Response.SetCookie(cookie);
        }

        public void TransmitFile(string filename)
        {
            HttpContext.Current.Response.TransmitFile(filename);
        }

        public void TransmitFile(string filename, long offset, long length)
        {
            HttpContext.Current.Response.TransmitFile(filename, offset, length);
        }

        public void Write(char ch)
        {
            HttpContext.Current.Response.Write(ch);
        }

        public void Write(object obj)
        {
            HttpContext.Current.Response.Write(obj);
        }

        public void Write(string s)
        {
            HttpContext.Current.Response.Write(s);
        }

        public void Write(char[] buffer, int index, int count)
        {
            HttpContext.Current.Response.Write(buffer,index,count);
        }

        public void WriteFile(string filename)
        {
            HttpContext.Current.Response.WriteFile(filename);
        }

        public void WriteFile(string filename, bool readIntoMemory)
        {
            HttpContext.Current.Response.WriteFile(filename,readIntoMemory);
        }

        public void WriteFile(IntPtr fileHandle, long offset, long size)
        {
            HttpContext.Current.Response.WriteFile(fileHandle,  offset,  size);
        }

        public void WriteFile(string filename, long offset, long size)
        {
            HttpContext.Current.Response.WriteFile(filename, offset, size);
        }

        public void WriteSubstitution(HttpResponseSubstitutionCallback callback)
        {
            HttpContext.Current.Response.WriteSubstitution(callback);
        }

        public bool Buffer
        {
            get { return HttpContext.Current.Response.Buffer; }
            set { HttpContext.Current.Response.Buffer=value; }
        }

        public bool BufferOutput
        {
            get { return HttpContext.Current.Response.BufferOutput; }
            set { HttpContext.Current.Response.BufferOutput = value; }
        }

        public HttpCachePolicy Cache
        {
            get { return HttpContext.Current.Response.Cache; }
        }

        public string CacheControl
        {
            get { return HttpContext.Current.Response.CacheControl; }
            set { HttpContext.Current.Response.CacheControl =value; }
        }

        public string Charset
        {
            get { return HttpContext.Current.Response.Charset; }
            set { HttpContext.Current.Response.Charset=value; }
        }

        public Encoding ContentEncoding
        {
            get { return HttpContext.Current.Response.ContentEncoding; }
            set { HttpContext.Current.Response.ContentEncoding = value; }
        }

        public string ContentType
        {
            get { return HttpContext.Current.Response.ContentType; }
            set { HttpContext.Current.Response.ContentType=value; }
        }

        public HttpCookieCollection Cookies
        {
            get { return HttpContext.Current.Response.Cookies; }
        }

        public int Expires
        {
            get { return HttpContext.Current.Response.Expires; }
            set { HttpContext.Current.Response.Expires=value; }
        }

        public DateTime ExpiresAbsolute
        {
            get { return HttpContext.Current.Response.ExpiresAbsolute; }
            set { HttpContext.Current.Response.ExpiresAbsolute=value; }
        }

        public Stream Filter
        {
            get { return HttpContext.Current.Response.Filter; }
            set { HttpContext.Current.Response.Filter=value; }
        }

        public Encoding HeaderEncoding
        {
            get { return HttpContext.Current.Response.HeaderEncoding; }
            set { HttpContext.Current.Response.HeaderEncoding=value; }
        }

        public NameValueCollection Headers
        {
            get { return HttpContext.Current.Response.Headers; }
        }

        public bool IsClientConnected
        {
            get { return HttpContext.Current.Response.IsClientConnected; }
        }

        public bool IsRequestBeingRedirected
        {
            get { return HttpContext.Current.Response.IsRequestBeingRedirected; }
        }

        public TextWriter Output
        {
            get { return HttpContext.Current.Response.Output; }
        }

        public Stream OutputStream
        {
            get { return HttpContext.Current.Response.OutputStream; }
        }

        public string RedirectLocation
        {
            get { return HttpContext.Current.Response.RedirectLocation; }
            set { HttpContext.Current.Response.RedirectLocation=value; }
        }

        public string Status
        {
            get { return HttpContext.Current.Response.Status; }
            set { HttpContext.Current.Response.Status=value; }
        }

        public int StatusCode
        {
            get { return HttpContext.Current.Response.StatusCode; }
            set { HttpContext.Current.Response.StatusCode=value; }
        }

        public string StatusDescription
        {
            get { return HttpContext.Current.Response.StatusDescription; }
            set { HttpContext.Current.Response.StatusDescription = value; }
        }

        public int SubStatusCode
        {
            get { return HttpContext.Current.Response.SubStatusCode; }
            set { HttpContext.Current.Response.SubStatusCode=value; }
        }

        public bool SuppressContent
        {
            get { return HttpContext.Current.Response.SuppressContent; }
            set { HttpContext.Current.Response.SuppressContent=value; }
        }

        public bool TrySkipIisCustomErrors
        {
            get { return HttpContext.Current.Response.TrySkipIisCustomErrors; }
            set { HttpContext.Current.Response.TrySkipIisCustomErrors=value; }
        }
    }

    public interface IHttpResponse
    {
        void AddCacheDependency(params CacheDependency[] dependencies);
        void AddCacheItemDependencies(string[] cacheKeys);
        void AddCacheItemDependencies(ArrayList cacheKeys);
        void AddCacheItemDependency(string cacheKey);
        void AddFileDependencies(ArrayList filenames);
        void AddFileDependencies(string[] filenames);
        void AddFileDependency(string filename);
        void AddHeader(string name, string value);
        void AppendCookie(HttpCookie cookie);
        void AppendHeader(string name, string value);

        void AppendToLog(string param);

        string ApplyAppPathModifier(string virtualPath);
        void BinaryWrite(byte[] buffer);
        void Clear();
        void ClearContent();
        void ClearHeaders();
        void Close();
        void DisableKernelCache();
        void End();
        void Flush();
        void Pics(string value);
        void Redirect(string url);
        void Redirect(string url, bool endResponse);
        void SetCookie(HttpCookie cookie);
        void TransmitFile(string filename);
        void TransmitFile(string filename, long offset, long length);
        void Write(char ch);
        void Write(object obj);
        void Write(string s);
        void Write(char[] buffer, int index, int count);
        void WriteFile(string filename);
        void WriteFile(string filename, bool readIntoMemory);

        void WriteFile(IntPtr fileHandle, long offset, long size);

        void WriteFile(string filename, long offset, long size);
        void WriteSubstitution(HttpResponseSubstitutionCallback callback);
        bool Buffer { get; set; }
        bool BufferOutput { get; set; }
        HttpCachePolicy Cache { get; }
        string CacheControl { get; set; }
        string Charset { get; set; }
        Encoding ContentEncoding { get; set; }
        string ContentType { get; set; }
        HttpCookieCollection Cookies { get; }
        int Expires { get; set; }
        DateTime ExpiresAbsolute { get; set; }
        Stream Filter { get; set; }
        Encoding HeaderEncoding { get; set; }
        NameValueCollection Headers { get; }
        bool IsClientConnected { get; }
        bool IsRequestBeingRedirected { get; }
        TextWriter Output { get; }
        Stream OutputStream { get; }
        string RedirectLocation { get; set; }
        string Status { get; set; }
        int StatusCode { get; set; }
        string StatusDescription { get; set; }
        int SubStatusCode { get; set; }
        bool SuppressContent { get; set; }
        bool TrySkipIisCustomErrors { get; set; }
    }

}
