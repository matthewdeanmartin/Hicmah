using System;
using System.Collections;
using System.Collections.Specialized;
using System.Web;
using System.Web.SessionState;

namespace Wrappers.WebContext
{
    /// <summary>
    /// Classes that use this wrapper are unit testable by injecting an Fake object
    /// that supports IHttpSessionState
    /// </summary>
    /// <remarks>
    /// This is Tsa.WebUI.IHttpSessionState, we may want to change it to make it unique.
    /// </remarks>
    public class HttpSessionStateWrapper : IHttpSessionState
    {
        public void Abandon()
        {
            HttpContext.Current.Session.Abandon();
        }

        public void Add(string name, object value)
        {
            HttpContext.Current.Session.Add(name,value);
        }

        public void Clear()
        {
            HttpContext.Current.Session.Clear();
        }

        public void CopyTo(Array array, int index)
        {
            HttpContext.Current.Session.CopyTo(array,index);
        }

        public int Count
        {
            get { return HttpContext.Current.Session.Count; }
        }

        public object SyncRoot
        {
            get { return HttpContext.Current.Session.SyncRoot; }
        }

        public int Timeout
        {
            get { return HttpContext.Current.Session.Timeout; }
            set { HttpContext.Current.Session.Timeout = value; }
        }

        object IHttpSessionState.this[int index]
        {
            get { return HttpContext.Current.Session[index]; }
            set { HttpContext.Current.Session[index]=value; }
        }

        object IHttpSessionState.this[string name]
        {
            get { return HttpContext.Current.Session[name]; }
            set { HttpContext.Current.Session[name] =value; }
        }

        public bool IsCookieless
        {
            get { return HttpContext.Current.Session.IsCookieless; }
        }

        public bool IsNewSession
        {
            get { return HttpContext.Current.Session.IsNewSession; }
        }

        public bool IsReadOnly
        {
            get { return HttpContext.Current.Session.IsReadOnly; }
        }

        public bool IsSynchronized
        {
            get { return HttpContext.Current.Session.IsSynchronized; }
        }

        public NameObjectCollectionBase.KeysCollection Keys
        {
            get { return HttpContext.Current.Session.Keys; }
        }

        public int LCID
        {
            get { return HttpContext.Current.Session.LCID; }
            set { HttpContext.Current.Session.LCID= value; }
        }

        public SessionStateMode Mode
        {
            get { return HttpContext.Current.Session.Mode; }
        }

        public string SessionID
        {
            get { return HttpContext.Current.Session.SessionID; }
        }

        public HttpStaticObjectsCollection StaticObjects
        {
            get { return HttpContext.Current.Session.StaticObjects; }
        }

        public IEnumerator GetEnumerator()
        {
            return HttpContext.Current.Session.GetEnumerator();
        }

        public void Remove(string name)
        {
            HttpContext.Current.Session.Remove(name);
        }

        public void RemoveAll()
        {
            HttpContext.Current.Session.RemoveAll();
        }

        public void RemoveAt(int index)
        {
            HttpContext.Current.Session.RemoveAt(index);
        }

        public int CodePage
        {
            get { return HttpContext.Current.Session.CodePage; }
            set { HttpContext.Current.Session.CodePage = value; }
        }

        public HttpSessionState Contents
        {
            get { return HttpContext.Current.Session.Contents; }
        }

        public HttpCookieMode CookieMode
        {
            get { return HttpContext.Current.Session.CookieMode; }
        }
    }

    public interface IHttpSessionState : ICollection
    {
        void Abandon();
        void Add(string name, object value);
        void Clear();
        //new void CopyTo(Array array, int index);
        //new IEnumerator GetEnumerator();
        void Remove(string name);
        void RemoveAll();
        void RemoveAt(int index);
        int CodePage { get; set; }
        HttpSessionState Contents { get; }
        HttpCookieMode CookieMode { get; }
        //new int Count { get; }
        bool IsCookieless { get; }
        bool IsNewSession { get; }
        bool IsReadOnly { get; }
        //new bool IsSynchronized { get; }
        NameObjectCollectionBase.KeysCollection Keys { get; }
        int LCID { get; set; }
        SessionStateMode Mode { get; }
        string SessionID { get; }
        HttpStaticObjectsCollection StaticObjects { get; }
        //object SyncRoot { get; }
        int Timeout { get; set; }
        object this[int index] { get; set; }
        object this[string name] { get; set; }
    }

}
