using System;
using System.Collections.Generic;
using System.Web.Caching;
using Hicmah.Web;
using Wrappers.WebContext;

namespace Hicmah.Recorders
{
    
    public class HitRecorderMsSqlCached: IHitRecorder
    {
        public int Capacity {get;set;}
        private readonly ICacheWrapper cache;

        private bool useAsync = false;
        public HitRecorderMsSqlCached(ICacheWrapper currentCache)
        {
            Capacity= ConfigUtils.BufferSize();
            cache = currentCache;
        }


        private static object ThisLock = new object();
        //X calls to this happen instantly. X+1 is slow.
        public void InsertHit(Hit hit)
        {
            using (TimedLock.Lock(ThisLock))
            {
                List<Hit> hitList = RetrieveHitListIfAny();

                hitList.Add(hit);
                if (hitList.Count >= Capacity)
                {
                    Flush();
                }
            }
        }

        public void Flush()
        {
            //This will be the first place where we learn if the connection string works or not 
            //(for hit recorders, not for the dashboard)

            List<Hit> hitList = RetrieveHitListIfAny();

            using (HitRecorderMsSql db = new HitRecorderMsSql(cache))
            {
                if (useAsync)
                    db.UseAsync();
                foreach (Hit item in hitList)
                {
                    db.InsertHit(item);
                }
            }
            cache.Remove("hits");
        }


        private List<Hit> RetrieveHitListIfAny()
        {
            object hits = cache["hits"];
            if (hits == null)
            {
                List<Hit> hitList = new List<Hit>(Capacity);
                //TODO: Hmm, this is a volatile store... How do we save on eviction?
                cache.Add("hits", hitList, null, DateTime.MaxValue, TimeSpan.Zero, CacheItemPriority.Normal, OnRemove);
                return hitList;
            }
            return (List<Hit>)hits;    
        }

        public static void OnRemove(string key, object cacheItem, CacheItemRemovedReason reason)
        {
            List<Hit> hitList = (List<Hit>)cacheItem;

            using (HitRecorderMsSql db = new HitRecorderMsSql(new CacheWrapper()))
            {
                foreach (Hit item in hitList)
                {
                    db.InsertHit(item);
                }
            }
        }
        


        public void UseAsync()
        {
            useAsync = true;
        }

        public void Dispose()
        {
            
        }
    }
}
