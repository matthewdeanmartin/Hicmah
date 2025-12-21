using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;
using System.Data;
using Dataglot;

namespace Hicmah.Dimensions
{
    public class UpdateUrl:GenericCommand
    {
        public UpdateUrl(DbConnection prexistingConnection)
            : base(ConfigUtils.CurrentDataFactory())
        {
            con = prexistingConnection;
            needToDisposeOfConnection = false;


            ComposeSql(
    @"UPDATE {$ns}_urls
   SET host_name = @host_name 
      ,port = @port 
      ,absolute_path = @absolute_path 
      ,query = @query 
 WHERE url_id= @url_id ");
            AddParameter("@host_name", DbType.String);
            AddParameter("@port", DbType.Int32);
            AddParameter("@absolute_path", DbType.String);
            AddParameter("@query", DbType.String);
            AddParameter("@url_id", DbType.Int32);
        }

        public void SetValues(string hostName, int port, string absolutePath, string query, int urlId)
        {
            command.Parameters["@host_name"].Value=hostName;
            command.Parameters["@port"].Value=port;
            command.Parameters["@absolute_path"].Value=absolutePath;
            command.Parameters["@query"].Value=query;
            command.Parameters["@url_id"].Value=urlId;
        }

        public void Execute()
        {
            int rowsAffected = ExecuteNonQuery();
            if(rowsAffected==0)
                throw new InvalidOperationException("expected 1 row to be updated, but it was 0");
        }
    }
}
