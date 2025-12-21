using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using System.Data.Common;
using Dataglot;

namespace Hicmah.ChartData
{
    /// <summary>
    /// These users are getting a lot of forbidden, denied errors. It could be the applications fault.
    /// </summary>
    /// <remarks>
    /// This relies on http error codes. If the page treats everything as 200 and uses the English 
    /// text to indicate a denial, then this report won't work.
    /// </remarks>
    public class ChartAttackingUsers : GenericCommand
    {
        public ChartAttackingUsers()
            : base(ConfigUtils.CurrentDataFactory())
        {
            
        }
        public DbDataReader RetrieveDataReader(DateTime start, DateTime end)
        {
            SetupCommand(start, end);

            return ExecuteReader(CommandBehavior.Default);
        }

        public Dictionary<string, decimal> RetrieveData(DateTime start, DateTime end)
        {
            Dictionary<string, decimal> dataPoints = new Dictionary<string, decimal>();

            SetupCommand(start, end);

            int sum = 0;
            int nonZeroEntries = 0;
            
            using (DbDataReader reader = ExecuteReader(CommandBehavior.Default))
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        sum = reader.GetInt32(1) + sum;
                        if (reader.GetInt32(1) > 0)
                            nonZeroEntries++;
                        System.Diagnostics.Debug.WriteLine(reader[0] + " " + reader.GetInt32(1));
                        string user;
                        if (reader.IsDBNull(0))
                            user = "N/A";
                        else
                            user = reader.GetString(0);
                        
                        dataPoints.Add(user, reader.GetInt32(1));
                    }
                }
                else
                {
                    //throw new InvalidOperationException("Failed to get data from database.");
                }
            }
            return dataPoints;
        }

        private void SetupCommand(DateTime start, DateTime end)
        {
            string sql = @"select [user],  COUNT(*) as AttackCount
from {$ns}_hit_data
where (status_code = 401 or status_code=403)  and hit_date >= @start and hit_date< @end
group by [user]
ORDER BY COUNT(*) Desc";

            ComposeSql(sql);


            AddParameter("@start", DbType.DateTime);
            AddParameter("@end", DbType.DateTime);

            command.Parameters["@start"].Value = start;
            command.Parameters["@end"].Value = end;
        }
    }
}
