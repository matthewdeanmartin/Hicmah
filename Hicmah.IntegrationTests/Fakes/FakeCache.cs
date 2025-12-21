using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Caching;
using Hicmah.Web;
using Wrappers.WebContext;

namespace Himah.IntegrationTests
{
    public class FakeCache : ICacheWrapper
    {
        private readonly Dictionary<string, object> cache = new Dictionary<string, object>();
        public object Add(string key, object value, CacheDependency dependencies, DateTime absoluteExpiration, TimeSpan slidingExpiration, CacheItemPriority priority, CacheItemRemovedCallback onRemoveCallback)
        {

            cache.Add(key, value);
            return value;
        }

        public object Get(string key)
        {
            return cache[key];
        }

        public IDictionaryEnumerator GetEnumerator()
        {
            return (IDictionaryEnumerator)cache;
        }

        public void Insert(string key, object value)
        {
            cache.Add(key, value);
        }

        public object Remove(string key)
        {
            object value = cache[key];
            cache.Remove(key);
            return value;
        }

        public int Count
        {
            get { return cache.Count; }
        }

        public long EffectivePercentagePhysicalMemoryLimit
        {
            get { return 10000; }
        }

        public long EffectivePrivateBytesLimit
        {
            get { return 10000; }
        }

        public object this[string key]
        {
            get
            {
                if (cache.ContainsKey(key))
                    return cache[key];
                else
                    return null;


            }
            set
            {
                if (cache.ContainsKey(key) && value == null)
                    cache.Remove(key);
                else
                    cache[key] = value;
            }
        }

        public void Insert(string key, object value, CacheDependency dependencies, DateTime absoluteExpiration, TimeSpan slidingExpiration, CacheItemPriority priority, CacheItemRemovedCallback onRemoveCallback)
        {
            cache.Add(key, value);
        }

        public void Insert(string key, object value, CacheDependency dependencies, DateTime absoluteExpiration, TimeSpan slidingExpiration, CacheItemUpdateCallback onUpdateCallback)
        {
            cache.Add(key, value);
        }

        public void Insert(string key, object value, CacheDependency dependencies, DateTime absoluteExpiration, TimeSpan slidingExpiration)
        {
            cache.Add(key, value);
        }

        public void Insert(string key, object value, CacheDependency dependencies)
        {
            cache.Add(key, value);
        }
    }
}
