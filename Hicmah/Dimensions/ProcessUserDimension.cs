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
    public class ProcessUserDimension:GenericCommand 
    {
        public ProcessUserDimension()
            : base(ConfigUtils.CurrentDataFactory())
        {
            
        }
        private readonly TraceSourceUtil trace = new TraceSourceUtil("ProcessUserDimension");
        /// <summary>
        /// Fill in URI columns in {$ns}_urls table
        /// </summary>
        public void Process()
        {
            const string sql = @"
select user_name_ID, MIN(hit_Date) AS FirstSeen, MAX(hit_date) AS LastSeen
from {$ns}_hits inner join {$ns}_users on {$ns}_users.user_id={$ns}_hits.user_name_id
where {$ns}_users.first_use is null and {$ns}_users.user_id>0
group by user_name_id
";
        ComposeSql(sql);
            using (DbDataReader reader = command.ExecuteReader(CommandBehavior.Default))
                {
                    if (reader.HasRows)
                    {
                        UpdateUserUsage update = new UpdateUserUsage(con);

                        while (reader.Read())
                        {
                            //msaccess can't deal with Int16
                            update.SetValues(reader.GetDateTime(1),reader.GetDateTime(2), Convert.ToInt32(reader[0]));
                            update.Execute();   
                        }
                    }
                    else
                    {
                        trace.TraceInformation("There don't appear to be any unprocessed users. The user dimension will not be updated.");
                    }
                }
        }
    }
}
