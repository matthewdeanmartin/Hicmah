using System;
using System.Collections;
using System.Web;
using System.Web.Caching;

namespace Wrappers.WebContext
{

    /// <summary>
    /// The wrapper around the real Cache and the unit testable mock (fake) will implement this interface.
    /// </summary>
    public interface ICacheWrapper 
    {
        object Add(string key, object value, CacheDependency dependencies, DateTime absoluteExpiration, TimeSpan slidingExpiration, CacheItemPriority priority, CacheItemRemovedCallback onRemoveCallback);
        object Get(string key);
        IDictionaryEnumerator GetEnumerator();
        void Insert(string key, object value);
        void Insert(string key, object value, CacheDependency dependencies);
        void Insert(string key, object value, CacheDependency dependencies, DateTime absoluteExpiration, TimeSpan slidingExpiration);
        void Insert(string key, object value, CacheDependency dependencies, DateTime absoluteExpiration, TimeSpan slidingExpiration, CacheItemUpdateCallback onUpdateCallback);
        void Insert(string key, object value, CacheDependency dependencies, DateTime absoluteExpiration, TimeSpan slidingExpiration, CacheItemPriority priority, CacheItemRemovedCallback onRemoveCallback);
        object Remove(string key);
        int Count { get; }
        long EffectivePercentagePhysicalMemoryLimit { get; }
        long EffectivePrivateBytesLimit { get; }
        object this[string key] { get; set; }
    }

    /// <summary>
    /// Wrapper around the real cache, which is sealed and does not on its own implement an interface or base
    /// class that allows for easy mocking (faking)
    /// </summary>
    public class CacheWrapper : ICacheWrapper
    {
        private readonly Cache cache;
        public CacheWrapper()
        {
            cache = HttpContext.Current.Cache;
        }

        public CacheWrapper(Cache provided)
        {
            cache = provided;
        }

        public IDictionaryEnumerator GetEnumerator()
        {
            return cache.GetEnumerator();
        }

        public object Add(string key, object value, CacheDependency dependencies, DateTime absoluteExpiration, TimeSpan slidingExpiration, CacheItemPriority priority, CacheItemRemovedCallback onRemoveCallback)
        {
            return cache.Add(key, value, dependencies, absoluteExpiration, slidingExpiration,  priority,  onRemoveCallback);
        }

        public object Get(string key)
        {
            return cache.Get(key);
        }

        public void Insert(string key, object value)
        {
            cache.Insert(key,value);
        }

        public void Insert(string key, object value, CacheDependency dependencies)
        {
            cache.Insert( key,  value,  dependencies);
        }

        public void Insert(string key, object value, CacheDependency dependencies, DateTime absoluteExpiration, TimeSpan slidingExpiration)
        {
            cache.Insert(key, value,dependencies, absoluteExpiration,  slidingExpiration);
        }

        public void Insert(string key, object value, CacheDependency dependencies, DateTime absoluteExpiration, TimeSpan slidingExpiration, CacheItemUpdateCallback onUpdateCallback)
        {
            cache.Insert(key, value,  dependencies,  absoluteExpiration,  slidingExpiration,  onUpdateCallback);
        }

        public void Insert(string key, object value, CacheDependency dependencies, DateTime absoluteExpiration, TimeSpan slidingExpiration, CacheItemPriority priority, CacheItemRemovedCallback onRemoveCallback)
        {
            cache.Insert(key, value, dependencies, absoluteExpiration, slidingExpiration,  priority, onRemoveCallback);
        }

        public object Remove(string key)
        {
            return cache.Remove(key);
        }

        public int Count
        {
            get { return cache.Count; }
        }

        public long EffectivePercentagePhysicalMemoryLimit
        {
            get { return cache.EffectivePercentagePhysicalMemoryLimit; }
        }

        public long EffectivePrivateBytesLimit
        {
            get { return cache.EffectivePrivateBytesLimit; }
        }

        public object this[string key]
        {
            get { return cache[key]; }
            set { cache[key]=value; }
        }
    }
}
