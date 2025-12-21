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
    public class ChartBuggiestPages : GenericCommand
    {
        public ChartBuggiestPages()
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
            int sum=0;
            int nonZeroEntries = 0;
            using (DbDataReader reader = ExecuteReader(CommandBehavior.Default))
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        sum = reader.GetInt32(2) + sum;
                        if (reader.GetInt32(2) > 0)
                            nonZeroEntries++;
                        System.Diagnostics.Debug.WriteLine(reader[0] + " " );

                        decimal ratio;
                        if (reader.IsDBNull(3))
                            ratio = 0;
                        else
                        {
                            try
                            {
                                object ratioValue = reader[3];
                                ratio = Convert.ToDecimal(ratioValue);//Msaccess can't cope with this-> reader.GetDecimal(3);
                            }
                            catch (InvalidCastException)
                            {
                                System.Diagnostics.Debug.WriteLine("Bad Value " + reader[3]);
                                throw;
                            }
                        }
                        
                        string url;
                        if (reader.IsDBNull(0))
                            url = "N/A";
                        else
                            url = reader.GetString(0);

                        System.Diagnostics.Debug.WriteLine(url + " " + ratio);

                        if(dataPoints.ContainsKey(url))
                            dataPoints.Add(url + Guid.NewGuid(), ratio);
                        else
                            dataPoints.Add(url, ratio);
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
            string sql = @"select OkQuery.absolute_path, OkQuery.OkCount,NotOkQuery.NotOkCount, (NotOkQuery.NotOkCount *1.0)/OkQuery.OkCount AS Ratio
from (select  absolute_path,  COUNT(*) AS OkCount
from {$ns}_hit_data
where status_code = 200 and hit_date >= @start and hit_date< @end 
group by  absolute_path
) as OkQuery
right outer join (select  absolute_path,  COUNT(*) AS NotOkCount
from {$ns}_hit_data
where status_code <> 200 and hit_date >= @start2 and hit_date< @end2 
group by  absolute_path
) as NotOkQuery
on OkQuery.absolute_path = NotOkQuery.absolute_path
ORDER BY (NotOkQuery.NotOkCount *1.0)/OkQuery.OkCount DESC";

            ComposeSql(sql);


            AddParameter("@start", DbType.DateTime);
            AddParameter("@end", DbType.DateTime);
            AddParameter("@start2", DbType.DateTime);
            AddParameter("@end2", DbType.DateTime);


            command.Parameters["@start"].Value = start;
            command.Parameters["@end"].Value = end;
            command.Parameters["@start2"].Value = start;
            command.Parameters["@end2"].Value = end;
        }
    }
}
