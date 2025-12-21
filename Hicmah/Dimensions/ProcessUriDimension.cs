using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;
using System.Data;
using Dataglot;
using Hicmah.Misc;

namespace Hicmah.Dimensions
{
    public class ProcessUriDimension:GenericCommand 
    {
        public ProcessUriDimension()
            : base(ConfigUtils.CurrentDataFactory())
        {
            
        }
        private readonly TraceSourceUtil trace = new TraceSourceUtil("ProcessUriDimension");

        /// <summary>
        /// Fill in URI columns in {$ns}_urls table
        /// </summary>
        public void Process()
        {
            const string sql = 
@"select url, url_id from {$ns}_urls where (host_name is null or Len(host_name)=0) and url_id>0";

        ComposeSql(sql);
            using (DbDataReader reader = command.ExecuteReader(CommandBehavior.Default))
                {
                    if (reader.HasRows)
                    {
                        UpdateUrl update = new UpdateUrl(con);

                        while (reader.Read())
                        {
                            try
                            {
                                Uri url = new Uri(reader.GetString(0));        
                                //Get object because msaccess and sql have different datatypes
                                update.SetValues(url.Host, url.Port, url.AbsolutePath, url.Query, Convert.ToInt32(reader[1]));
                                update.Execute();
                            }
                            catch (UriFormatException ex)
                            {
                                trace.TraceInformation("Invalid Uri: " + reader.GetString(0) + " error :" + ex.Message);
                            }
                        }
                    }
                    else
                    {
                        trace.TraceInformation("There don't appear to be any hits with host names, the Uri dimension will not be updated.");
                    }
                }
        }
    }
}
