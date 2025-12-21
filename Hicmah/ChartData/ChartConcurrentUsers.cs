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
    public class ChartConcurrentUsers : GenericCommand
    {
        public ChartConcurrentUsers()
            : base(ConfigUtils.CurrentDataFactory())
        {
            
        }
        /// <summary>
        /// Generates dictionary of the number of online users at each point of the day.
        /// </summary>
        /// <param name="window">The window (in minutes) for which users are considered concurrently online</param>
        /// <param name="interval"></param>
        /// <param name="endOfInterval"></param>
        public Dictionary<DateTime,decimal> RetrieveData(int window, DateTime interval, DateTime endOfInterval)
        {
            Dictionary<DateTime, decimal> dataPoints = new Dictionary<DateTime, decimal>();

            //For window = 24*60, this is daily number of users

            string sql = @"select COUNT(*)
FROM
(select distinct {$ns}_hits.user_name_id
from {$ns}_hits 
where hit_date>= @start and hit_date < @end ) as results";

            ComposeSql(sql);


            AddParameter("@start", DbType.DateTime);
            AddParameter("@end", DbType.DateTime);

            int sum = 0;
            int nonZeroEntries = 0;
            while (interval < endOfInterval)
            {
                command.Parameters["@start"].Value = interval;
                command.Parameters["@end"].Value = interval.AddMinutes(window);

                using (DbDataReader reader = ExecuteReader(CommandBehavior.Default))
                {
                    if (reader.HasRows)
                    {
                        reader.Read();
                        sum = reader.GetInt32(0) + sum;
                        if (reader.GetInt32(0) > 0)
                            nonZeroEntries++;
                        System.Diagnostics.Debug.WriteLine(reader[0] + " " + interval);
                        dataPoints.Add(interval,reader.GetInt32(0));
                    }
                    else
                    {
                        throw new InvalidOperationException("Failed to get data from database.");
                    }
                }
                interval = interval.AddMinutes(window);
            }
            System.Diagnostics.Debug.WriteLine("Average Concurrent Users (ignoring periods of no usage) : " + ((nonZeroEntries > 0) ? ((decimal?)sum / (decimal?)nonZeroEntries) : null));

            return dataPoints;
        }


    }
}
