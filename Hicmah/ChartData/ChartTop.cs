using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Data;
using Dataglot;

namespace Hicmah.ChartData
{
    public enum UsageRate
    {
        None=0,
        Top=1,
        Bottom=2
    }
    public class GenericDataPoint
    {

    }

    public class ChartTop : GenericCommand
    {
        public ChartTop()
            : base(ConfigUtils.CurrentDataFactory())
        {
            
        }
        public string[] Valid = { "full_url", "user", "user_agent", "referrer_url", "request_type" };

        private string currentDiminsion = "";

        public DbDataReader RetrieveDataReader(int limitTop, UsageRate rate, DateTime interval, DateTime endOfInterval, string diminsion,string[] measures)
        {
            SetupCommand(limitTop, rate, interval, endOfInterval, diminsion, measures);

            return ExecuteReader(CommandBehavior.Default);
        }

        /// <summary>
        /// For the given interval, who is the top user.
        /// </summary>
        public List<PercentOfTotalDataPoint> RetrieveData(int limitTop, UsageRate rate, DateTime interval, DateTime endOfInterval, string diminsion,string[] measures)
        {
            
            SetupCommand(limitTop, rate, interval, endOfInterval, diminsion,measures);

            List<PercentOfTotalDataPoint> data;
            NamedValueBinder binder = new NamedValueBinder();
            AggregateStatistics stats = binder.Bind(command, out data);

            //TODO: output to a reall UI.
            System.Diagnostics.Debug.WriteLine("Top n report for " + diminsion);
            System.Diagnostics.Debug.WriteLine("# : " + stats.Count + " - Avg  " + stats.Average + " - Sum " + stats.Sum);
            foreach (PercentOfTotalDataPoint point in data)
            {
                System.Diagnostics.Debug.WriteLine(point.Name + " -  " + point.Measure + " hits " + string.Format("{0:G4}", point.Percent) + "% of total");
            }
            return data;
        }

        private void SetupCommand(int limitTop, UsageRate rate, DateTime interval, DateTime endOfInterval, string diminsion,string[] measures)
        {
            if (!Valid.Contains(diminsion))
                throw new ArgumentException("That isn't a valid dimension");

            //For window = 24*60, this is daily number of users

            string sortBy;
            if (rate == UsageRate.Top)
            {
                sortBy = "DESC";
            }
            else
            {
                sortBy = "ASC";
            }

            string top = "";
            if (limitTop > 0)
            {
                top = " top " + limitTop;
            }

            Dictionary<string,string> validMetrics = new Dictionary<string, string>();
            validMetrics.Add("Hits", "count(*) as Hits");
validMetrics.Add("TotalClientTime","sum(client_duration) as TotalClientTime");
validMetrics.Add("AvgClientTime", "avg(client_duration) as AvgClientTime");
validMetrics.Add("StDevClientTime", "Round(stdev(client_duration),2) as StDevClientTime");
validMetrics.Add("MinClientTime", "min(client_duration) as MinClientTime");
validMetrics.Add("MaxClientTime", "max(client_duration) as MaxClientTime");
validMetrics.Add("TotalServerTime", "sum(server_duration) as TotalServerTime");
validMetrics.Add("AvgServerTime", "avg(server_duration) as AvgServerTime");
validMetrics.Add("StDevServerTime", "Round(stdev(server_duration),2) as StDevServerTime");
validMetrics.Add("MinServerTime", "min(server_duration) as MinServerTime");
validMetrics.Add("MaxServerTime", "max(server_duration) as MaxServerTime");

            List<string> list=new List<string>();
            foreach (string name in measures)
            {
                list.Add(validMetrics[name]);
            }
            string[] metricsToUse = list.ToArray();
            string currentMeasures = string.Join(", ", metricsToUse);
            if (string.IsNullOrEmpty(command.CommandText) || currentDiminsion != diminsion)
            {
                if (
                    (factory.CurrentDb == DbBrand.MsSql2005 ) && diminsion=="user")
                {
                    //User is a really bad column name. 
                    diminsion = "[user]";
                }

                string sql = @"select " + top + @" " + diminsion + @", " + currentMeasures + @"
from {$ns}_hit_data
where
hit_date>= @start and hit_date <= @end 
group by " + diminsion + @"
ORDER by COUNT(*) " + sortBy;
                ComposeSql(sql);
            }

            if (command.Parameters.Count == 0)
            {
                AddParameter("@start", DbType.DateTime);
                AddParameter("@end", DbType.DateTime);
            }

            command.Parameters["@start"].Value = interval;
            command.Parameters["@end"].Value = endOfInterval;
        }
    }
}
