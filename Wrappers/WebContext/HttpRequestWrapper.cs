using System;
using System.Collections.Specialized;
using System.IO;
using System.Security.Permissions;
using System.Security.Principal;
using System.Text;
using System.Web;

namespace Wrappers.WebContext
{
    /// <summary>
    /// Wrapper around the Request to help make it more unit testable, although Request can
    /// be constructed and assigned to HttpContext.Current directly.
    /// </summary>
    public class HttpRequestWrapper:IHttpRequest
    {
        public byte[] BinaryRead(int count)
        {
            return HttpContext.Current.Request.BinaryRead(count);
        }

        public int[] MapImageCoordinates(string imageFieldName)
        {
            return HttpContext.Current.Request.MapImageCoordinates(imageFieldName);
        }

        public string MapPath(string virtualPath)
        {
            return HttpContext.Current.Request.MapPath(virtualPath);
        }

        public string MapPath(string virtualPath, string baseVirtualDir, bool allowCrossAppMapping)
        {
            return HttpContext.Current.Request.MapPath(virtualPath, baseVirtualDir, allowCrossAppMapping);
        }

        public void SaveAs(string filename, bool includeHeaders)
        {
            HttpContext.Current.Request.SaveAs(filename, includeHeaders);
        }

        public void ValidateInput()
        {
            HttpContext.Current.Request.ValidateInput();
        }

        public string[] AcceptTypes
        {
            get { return HttpContext.Current.Request.AcceptTypes; }
        }

        public string AnonymousID
        {
            get { return HttpContext.Current.Request.AnonymousID; }
        }

        public string ApplicationPath
        {
            get { return HttpContext.Current.Request.ApplicationPath; }
        }

        public string AppRelativeCurrentExecutionFilePath
        {
            get { return HttpContext.Current.Request.AppRelativeCurrentExecutionFilePath; }
        }

        public HttpBrowserCapabilities Browser
        {
            get { return HttpContext.Current.Request.Browser; }
            set { HttpContext.Current.Request.Browser =value; }
        }

        public HttpClientCertificate ClientCertificate
        {
            get { return HttpContext.Current.Request.ClientCertificate; }
        }

        public Encoding ContentEncoding
        {
            get { return HttpContext.Current.Request.ContentEncoding; }
            set { HttpContext.Current.Request.ContentEncoding = value; }
        }

        public int ContentLength
        {
            get { return HttpContext.Current.Request.ContentLength; }
        }

        public string ContentType
        {
            get { return HttpContext.Current.Request.ContentType; }
            set { HttpContext.Current.Request.ContentType = value; }
        }

        public HttpCookieCollection Cookies
        {
            get { return HttpContext.Current.Request.Cookies; }
        }

        public string CurrentExecutionFilePath
        {
            get { return HttpContext.Current.Request.CurrentExecutionFilePath; }
        }

        public string FilePath
        {
            get { return HttpContext.Current.Request.FilePath; }
        }

        public HttpFileCollection Files
        {
            get { return HttpContext.Current.Request.Files; }
        }

        public Stream Filter
        {
            get { return HttpContext.Current.Request.Filter; }
            set { HttpContext.Current.Request.Filter = value; }
        }

        public NameValueCollection Form
        {
            get { return HttpContext.Current.Request.Form; }
        }

        public NameValueCollection Headers
        {
            get { return HttpContext.Current.Request.Headers; }
        }

        public string HttpMethod
        {
            get { return HttpContext.Current.Request.HttpMethod; }
        }

        public Stream InputStream
        {
            get { return HttpContext.Current.Request.InputStream; }
        }

        public bool IsAuthenticated
        {
            get { return HttpContext.Current.Request.IsAuthenticated; }
        }

        public bool IsLocal
        {
            get { throw new NotImplementedException(); }
        }

        public bool IsSecureConnection
        {
            get { throw new NotImplementedException(); }
        }

        public WindowsIdentity LogonUserIdentity
        {
            get { return HttpContext.Current.Request.LogonUserIdentity; }
        }

        public NameValueCollection Params
        {
            get { return HttpContext.Current.Request.Params; }
        }

        public string Path
        {
            get { return HttpContext.Current.Request.Path; }
        }

        public string PathInfo
        {
            get { return HttpContext.Current.Request.PathInfo; }
        }

        public string PhysicalApplicationPath
        {
            get { return HttpContext.Current.Request.PhysicalApplicationPath; }
        }

        public string PhysicalPath
        {
            get { return HttpContext.Current.Request.PhysicalPath; }
        }

        public NameValueCollection QueryString
        {
            get { return HttpContext.Current.Request.QueryString; }
        }

        public string RawUrl
        {
            get { return HttpContext.Current.Request.RawUrl; }
        }

        public string RequestType
        {
            get { return HttpContext.Current.Request.RequestType; }
            set { HttpContext.Current.Request.RequestType = value; }
        }

        public NameValueCollection ServerVariables
        {
            get { return HttpContext.Current.Request.ServerVariables; }
        }

        public int TotalBytes
        {
            get { return HttpContext.Current.Request.TotalBytes; }
        }

        public Uri Url
        {
            get { return HttpContext.Current.Request.Url; }
        }

        public Uri UrlReferrer
        {
            get { return HttpContext.Current.Request.UrlReferrer; }
        }

        public string UserAgent
        {
            get { return HttpContext.Current.Request.UserAgent; }
        }

        public string UserHostAddress
        {
            get { return HttpContext.Current.Request.UserHostAddress; }
        }

        public string UserHostName
        {
            get { return HttpContext.Current.Request.UserHostName; }
        }

        public string[] UserLanguages
        {
            get { return HttpContext.Current.Request.UserLanguages; }
        }

        public string this[string key]
        {
            get { return HttpContext.Current.Request[key]; }
        }
    }

    public interface IHttpRequest
    {
        byte[] BinaryRead(int count);
        int[] MapImageCoordinates(string imageFieldName);
        string MapPath(string virtualPath);
        string MapPath(string virtualPath, string baseVirtualDir, bool allowCrossAppMapping);
        void SaveAs(string filename, bool includeHeaders);
        void ValidateInput();
        string[] AcceptTypes { get; }
        string AnonymousID { get; }
        string ApplicationPath { get; }
        string AppRelativeCurrentExecutionFilePath { get; }
        HttpBrowserCapabilities Browser { get; set; }

        HttpClientCertificate ClientCertificate { [AspNetHostingPermission(SecurityAction.Demand, Level = AspNetHostingPermissionLevel.Low)]
        get; }

        Encoding ContentEncoding { get; set; }
        int ContentLength { get; }
        string ContentType { get; set; }
        HttpCookieCollection Cookies { get; }
        string CurrentExecutionFilePath { get; }
        string FilePath { get; }
        HttpFileCollection Files { get; }
        Stream Filter { get; set; }
        NameValueCollection Form { get; }
        NameValueCollection Headers { get; }
        string HttpMethod { get; }
        Stream InputStream { get; }
        bool IsAuthenticated { get; }
        bool IsLocal { get; }
        bool IsSecureConnection { get; }

        WindowsIdentity LogonUserIdentity { [AspNetHostingPermission(SecurityAction.Demand, Level = AspNetHostingPermissionLevel.Medium)]
        get; }

        NameValueCollection Params { get; }
        string Path { get; }
        string PathInfo { get; }
        string PhysicalApplicationPath { get; }
        string PhysicalPath { get; }
        NameValueCollection QueryString { get; }
        string RawUrl { get; }
        string RequestType { get; set; }
        NameValueCollection ServerVariables { get; }
        int TotalBytes { get; }
        Uri Url { get; }
        Uri UrlReferrer { get; }
        string UserAgent { get; }
        string UserHostAddress { get; }
        string UserHostName { get; }
        string[] UserLanguages { get; }

        string this[string key] { get; }
    }

}
