using System;
using System.Collections;
using System.Web.Caching;

namespace Wrappers.WebContext
{
    public class NullCacheWrapper : ICacheWrapper
    {
        public object Add(string key, object value, CacheDependency dependencies, DateTime absoluteExpiration, TimeSpan slidingExpiration, CacheItemPriority priority, CacheItemRemovedCallback onRemoveCallback)
        {
            //throw new NotImplementedException();
            return null;
        }

        public object Get(string key)
        {
            //throw new NotImplementedException();
            return null;
        }

        public IDictionaryEnumerator GetEnumerator()
        {
            //throw new NotImplementedException();
            return null;
        }

        public void Insert(string key, object value)
        {
            //throw new NotImplementedException();
        }

        public void Insert(string key, object value, CacheDependency dependencies)
        {
            //throw new NotImplementedException();
        }

        public void Insert(string key, object value, CacheDependency dependencies, DateTime absoluteExpiration, TimeSpan slidingExpiration)
        {
            //throw new NotImplementedException();
        }

        public void Insert(string key, object value, CacheDependency dependencies, DateTime absoluteExpiration, TimeSpan slidingExpiration, CacheItemUpdateCallback onUpdateCallback)
        {
            //throw new NotImplementedException();
        }

        public void Insert(string key, object value, CacheDependency dependencies, DateTime absoluteExpiration, TimeSpan slidingExpiration, CacheItemPriority priority, CacheItemRemovedCallback onRemoveCallback)
        {
            //throw new NotImplementedException();
        }

        public object Remove(string key)
        {
            //throw new NotImplementedException();
            return null;
        }

        public int Count
        {
            get { return 0; }
        }

        public long EffectivePercentagePhysicalMemoryLimit
        {
            get { return 0; }
        }

        public long EffectivePrivateBytesLimit
        {
            get { return 0; }
        }

        public object this[string key]
        {
            get { return null; }
            set {  }
        }
    }
}
