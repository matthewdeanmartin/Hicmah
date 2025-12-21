using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using Dataglot;

namespace Hicmah.ChartData
{
    public class ChartHitsPerTimeSpan : GenericCommand
    {
        public ChartHitsPerTimeSpan()
            : base(ConfigUtils.CurrentDataFactory())
        {
            
        }
        public DbDataReader RetrieveDataReader(DateTime start, DateTime end, string how)
        {
            SetupCommand(start, end,how);

            return ExecuteReader(CommandBehavior.Default);
        }
        public Dictionary<long, long> RetrieveData(DateTime start, DateTime end, string how)
        {
            Dictionary<long, long> dataPoints = new Dictionary<long, long>();
            SetupCommand(start, end,how);

            //int sum = 0;
//            int nonZeroEntries = 0;

            using (DbDataReader reader = ExecuteReader(CommandBehavior.Default))
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        //sum = reader.GetInt32(2) + sum;
                        //if (reader.GetInt64(2) > 2)
                        //    nonZeroEntries++;
                        long count;
                        if (reader.IsDBNull(2))
                            count = 0;
                        else
                        {
                            try
                            {
                                object hitCount = reader[2];
                                count = Convert.ToInt64(hitCount); //reader.GetDecimal(3);
                            }
                            catch (InvalidCastException)
                            {
                                System.Diagnostics.Debug.WriteLine("Bad Value " + reader[2]);
                                throw;
                            }
                        }


                        //DateTime url;
                        //if (reader.IsDBNull(0))
                        //    url = DateTime.MinValue;
                        //else
                        //    url = reader.GetDateTime(0);

                        long  timeKey;
                        if (reader.IsDBNull(0))
                            timeKey = 0;
                        else
                        {
                            object timeKeyHolder = reader[0];
                            timeKey = Convert.ToInt64(timeKeyHolder);
                        }
                        

                        dataPoints.Add(timeKey, count);
                    }
                }
                else
                {
                    //throw new InvalidOperationException("Failed to get data from database.");
                }
            }
            return dataPoints;
        }

        private void SetupCommand(DateTime start, DateTime end, string how)
        {
            string timeSlice="";
            if(how=="monthly")
                timeSlice = "long_year_month";
            else if(how=="annual")
                timeSlice = "Year";

            string sql = 
@"SELECT MIN(dbo.{$ns}_dates.date_id), dbo.{$ns}_dates.["+timeSlice+@"], COUNT(*)
FROM dbo.{$ns}_hits 
INNER JOIN dbo.{$ns}_dates 
ON dbo.{$ns}_dates.date_id = {$ns}_hits.date_id
WHERE  hit_date >= @start and hit_date< @end
GROUP BY dbo.{$ns}_dates.[" + timeSlice + @"] 
";

            ComposeSql(sql);

            AddParameter("@start", DbType.DateTime);
            AddParameter("@end", DbType.DateTime);

            command.Parameters["@start"].Value = start;
            command.Parameters["@end"].Value = end;
        }
    }
}
