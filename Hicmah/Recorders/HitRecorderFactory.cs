using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hicmah.Misc;
using Hicmah.Web;
using Wrappers.WebContext;

namespace Hicmah.Recorders
{
    public class HitRecorderFactory
    {
        private TraceSourceUtil trace = null;
        public HitRecorderFactory()
        {
            trace = new TraceSourceUtil(this.GetType().Name);
        }

        public IHitRecorder MakeCachedHitRecorder(string how, ICacheWrapper cache, bool lockDb)
        {
            trace.WriteLine("MakeCachedHitRecorder for " + how);
            switch (how)
            {
                case "mssql":
                case "mssql-bulk":
                    return new HitRecorderMsSqlCachedBulk(cache);
                default:
                    throw new ArgumentException("Unknown type of Cached hit recorder " + how, "how");
            }

        }

        public IHitRecorder MakeCachedHitRecorder(string how, ICacheWrapper cache)
        {
            trace.WriteLine("MakeCachedHitRecorder for " + how);
            switch(how)
            {
                case "mssql":
                case "mssql-bulk":
                    return new HitRecorderMsSqlCachedBulk(cache);
                default:
                    throw new ArgumentException("Unknown type of Cached hit recorder " + how, "how");
            }
        
        }

        public IHitRecorder MakeHitRecorder(string how, ICacheWrapper cache)
        {
            trace.WriteLine("MakeHitRecorder for " + how);

            switch(how)
            {
                case "mssql":
                case "mssql-bulk":
                    return new HitRecorderMsSql(cache);
                default:
                    throw new ArgumentException("Unknown type of hit recorder " + how, "how");
            }
        
        }
    }
}
