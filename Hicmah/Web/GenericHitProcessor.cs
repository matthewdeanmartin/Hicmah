using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading;
using Hicmah.Recorders;
using Hicmah.Web;
using Wrappers.WebContext;
using HttpRequestWrapper = System.Web.HttpRequestWrapper;

namespace Hicmah
{
    public class GenericHitProcessor
    {
        public void RecordHitToCurrentProvider(Hit currentHit)
        {
            string provider = ConfigUtils.Provider();
            switch (provider)
            {
                case "mssql":
                    using (HitRecorderMsSqlCachedBulk recorder = new HitRecorderMsSqlCachedBulk(new CacheWrapper()))
                    {
                       
                        recorder.InsertHit(currentHit);
                    }
                    break;
                default:
                    throw new InvalidOperationException("Provider not recognized : ");
            }
        }
    }
}
