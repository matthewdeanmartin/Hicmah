using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Dataglot;
using Hicmah.Misc;

namespace Hicmah.DataCache
{
    public class CacheClearExpired:GenericCommand
    {
        public CacheClearExpired()
            : base(ConfigUtils.CurrentDataFactory())
        {
            
        }
        private readonly TraceSourceUtil trace = new TraceSourceUtil("CacheClearExpired");

        public void RemoveExpiredCacheEntries()
        {
            const string sql = @"DELETE FROM [{$ns}_cache] WHERE expirationDate< @removeBeforeDate ";
            ComposeSql(sql);

            AddParameter("@removeBeforeDate", DbType.DateTime);

            command.Parameters["@removeBeforeDate"].Value = MsAccess.AccessUtils.SmallerDateTime(DateTime.Now);

            int rowsAffected = ExecuteNonQuery();
            if (rowsAffected> 0)
            {
                System.Diagnostics.Debug.WriteLine("Removed " + rowsAffected + " rows from the cache");
            }
        }
    }
}
