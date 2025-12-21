using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Dataglot;
using Hicmah.Misc;
//using Hicmah.MsAccess;
using Hicmah.MsAccess;

namespace Hicmah.DataCache
{
    public class CacheInsert:GenericCommand
    {
        public CacheInsert():base(ConfigUtils.CurrentDataFactory())
        {
            
        }
        private readonly TraceSourceUtil trace = new TraceSourceUtil("CacheInsert");

            
        public void Insert(string parameters, string cachedResults, DateTime expirationDate, string queryName)
        {
            trace.WriteLine("Cache insert for " + queryName);
            const string sql = 
@"INSERT INTO [{$ns}_cache]
           ([parameters]
           ,[cachedResults]
           ,[expirationDate]
           ,[queryName]
           ,[insertDate])
     VALUES
           ( @parameters 
           , @cachedResults 
           , @expirationDate 
           , @queryName 
           , @insertDate )";
            

            AddParameter("@parameters", DbType.String);
            AddParameter("@cachedResults", DbType.String);
            AddParameter("@expirationDate", DbType.DateTime);
            AddParameter("@queryName", DbType.String);
            AddParameter("@insertDate", DbType.DateTime);

            ComposeSql(sql);

            command.Parameters["@parameters"].Value = parameters;
            command.Parameters["@cachedResults"].Value = cachedResults;
            command.Parameters["@expirationDate"].Value = AccessUtils.SmallerDateTime(expirationDate);
            command.Parameters["@queryName"].Value = queryName;
            command.Parameters["@insertDate"].Value = AccessUtils.SmallerDateTime(DateTime.Now);

            int rowsAffected = ExecuteNonQuery();
            if(rowsAffected==0)
            {
                System.Diagnostics.Debug.WriteLine("rows Affected was 0 on cache insert. This is suspicious or the db just doesn't support rows affected.");
            }
        }
    }
}
