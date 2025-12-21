using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Hicmah.Misc;
using ProtoBuf;

namespace Hicmah.DataCache
{
    public class CacheInDb<T>
    {
        private readonly TraceSourceUtil trace = new TraceSourceUtil("CacheInDb");
        
        public void StoreInDb(T dataPoints, string parameters, string queryName)
        {
            trace.WriteLine("Store in cache");
            MemoryStream stream = new MemoryStream();
            Serializer.Serialize(stream, dataPoints);
            using (CacheInsert inserter = new CacheInsert())
            {
                inserter.Insert(
                    parameters, 
                    Convert.ToBase64String(stream.GetBuffer(), 0, (int)stream.Length), 
                    DateTime.Now.AddDays(1), 
                    queryName);
            }

        }

        public T SearchDb(string parameters, string queryName)
        {
            string cacheData;

            using (CacheClearExpired clearCommand = new CacheClearExpired())
            {
                trace.WriteLine("Remove expired items from cache");
                clearCommand.RemoveExpiredCacheEntries();
            }
            using (CacheRead cacheReader = new CacheRead())
            {
                cacheData = cacheReader.Read(parameters, queryName, DateTime.Now.AddDays(-1));
            }
            //if (cacheData == null) 
            //return (T)cacheData;

            if (cacheData == null)
            {
                trace.WriteLine("Cache miss for " + queryName);
                return Serializer.Deserialize<T>(new MemoryStream());
            }
            else
            {
                trace.WriteLine("Cache hit for " + queryName);
                byte[] data = Convert.FromBase64String(cacheData);
                MemoryStream stream = new MemoryStream(data);
                stream.Position = 0;
                return Serializer.Deserialize<T>(stream);
            }
            
        }
    }
}
