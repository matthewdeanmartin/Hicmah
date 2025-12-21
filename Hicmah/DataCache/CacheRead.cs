using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using Dataglot;
using Hicmah.Misc;
using Hicmah.MsAccess;

namespace Hicmah.DataCache
{
    public class CacheRead:GenericCommand
    {
        public CacheRead()
            : base(ConfigUtils.CurrentDataFactory())
        {
            
        }
        private readonly TraceSourceUtil trace = new TraceSourceUtil("CacheRead");

        public string Read(string parameters, string queryName, DateTime oldestResults)
        {
            trace.WriteLine("Cache search for " + queryName);

            const string sql = @"SELECT cachedResults
FROM {$ns}_cache
WHERE
    parameters = @parameters 
AND
   queryName = @queryName 
AND
    insertDate > @oldestResults 
ORDER BY insertDate DESC";
            
            AddParameter("@parameters", DbType.String);
            AddParameter("@queryName", DbType.String);
            AddParameter("@oldestResults", DbType.DateTime);
            
            ComposeSql(sql);

            command.Parameters["@parameters"].Value = parameters;
            command.Parameters["@oldestResults"].Value = AccessUtils.SmallerDateTime(oldestResults);
            command.Parameters["@queryName"].Value = queryName;

            using (DbDataReader reader = ExecuteReader(CommandBehavior.Default))
            {
                if (reader.HasRows)
                {
                    reader.Read();
                    if(reader.IsDBNull(0)) return null;
                    return reader.GetString(0);
                }
                return null;
            }
        }
    }
}
