using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Web;
using System.Web.Caching;
using Hicmah.Web;
using Wrappers.WebContext;

namespace Hicmah.Recorders
{
    /// <summary>
    /// Stores up hits, then records them in bulk.
    /// </summary>
    /// <remarks>
    /// This is by far the fastest. If the cache size is n, then the first n
    /// request will complete virtually instantly. The nth will be a slow request.
    /// The average of all requests is still lower than recording his one by one.
    /// 
    /// This solution only works with SQL Server.
    /// </remarks>
    public class HitRecorderMsSqlCachedBulk: IHitRecorder
    {
        //Default, can be overridden
        public int Capacity { get; set; } 
        
        private ICacheWrapper cache;


        public HitRecorderMsSqlCachedBulk(ICacheWrapper currentCache)
        {
            Capacity = ConfigUtils.BufferSize();
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
                    cache.Remove("hits");
                }
            }
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
            
            using (HitRecorderMsSql db = new HitRecorderMsSql(HttpContext.Current==null?(ICacheWrapper)new NullCacheWrapper(): (ICacheWrapper)new CacheWrapper()))
            {
                //int countnow = hitList.Count;
                for (int i = hitList.Count - 1; i >= 0; i--)
                {
                    db.LookupGaps(hitList[i]);
                }
                //if (countnow != hitList.Count)
                //    throw new InvalidOperationException("It grew!");

                Validate(hitList);
                db.BulkInsert(new HitReader(hitList));
            }
        }

        [Conditional("DEBUG")]
        private static void Validate(List<Hit> hitList)
        {
            foreach (Hit hit in hitList)
            {
                foreach (string error in hit.ListErrors())
                {
                    throw new InvalidOperationException("Invalid hit- property of " + error + " Complete hit: " + hit.ToString());
                }
            }
        }

        public void Flush()
        {
            
                List<Hit> hitList = RetrieveHitListIfAny();

                using (HitRecorderMsSql db = new HitRecorderMsSql(cache))
                {
                    int countnow = hitList.Count;
                    for (int i = hitList.Count-1; i >= 0;i-- )
                    {
                        db.LookupGaps(hitList[i]);
                    }
                    if(countnow!=hitList.Count)
                        throw new InvalidOperationException("It grew!");

                    db.BulkInsert(new HitReader(hitList));

                    if (Capacity > 999)
                    {
                        db.Checkpoint();
                    }
                }

            
        }

        public void UseAsync()
        {
            //Could implement, but haven't.
            throw new NotSupportedException();
        }

        public void Dispose()
        {
            
        }
    }
}
