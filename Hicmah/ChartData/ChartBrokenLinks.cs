using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using System.Data.Common;
using Dataglot;
using Hicmah.ChartData.MissingPage;

namespace Hicmah.ChartData
{
    public class BrokenLinkDataPoint
    {
        public string Path { get; set; }
        public DateTime MissingSince { get; set; }
        public DateTime LastAttempedRetrieval { get; set; }
        public int Incidents { get; set; }
    }

    public class ChartBrokenLinks : GenericCommand
    {
        public ChartBrokenLinks()
            : base(ConfigUtils.CurrentDataFactory())
        {
            
        }
        public DbDataReader RetrieveDataReader(DateTime start, DateTime end)
        {
            SetUpCommand(start, end);

            return ExecuteReader(CommandBehavior.Default);   
        }

        public MissingPage.JqGridData RetrieveData(DateTime start, DateTime end)
        {
            MissingPage.JqGridData dataPoints = new JqGridData();

            SetUpCommand(start, end);

            int nonZeroEntries = 0;
            int sum = 0;

            using (DbDataReader reader = ExecuteReader(CommandBehavior.Default))
            {

                List<Row> rows = new List<Row>();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        int rowId=0;
                        
                        List<string> cells = new List<string>();
                        reader.Read();
                        sum = reader.GetInt32(0) + sum;
                        if (reader.GetInt32(0) > 0)
                            nonZeroEntries++;
                        System.Diagnostics.Debug.WriteLine(reader[0] + " " );
                            
                            //Path 
                            cells.Add( reader.GetString(0));

                            //Incidents 
                            cells.Add( reader.GetInt32(3).ToString());

                            //MissingSince
                            cells.Add( reader.GetDateTime(1).ToString());
    
                            //LastAttempedRetrieval
                            cells.Add( reader.GetDateTime(2).ToString());
                        rowId++;
                        Row r = new Row();
                        r.cell = cells.ToArray();
                        r.id = rowId.ToString();
                    }
                }

                dataPoints.page = "1";
                dataPoints.records = rows.Count().ToString();
                dataPoints.rows = rows.ToArray();

                return dataPoints;
            }
        }

        private void SetUpCommand(DateTime start, DateTime end)
        {
            string sql = @"select absolute_path, MIN(hit_date) AS MissingSince, MAX(hit_date) AS LastAttemptedRetrieval, COUNT(*) AS NotOkCount
from {$ns}_hit_data
where (status_code = 404) and hit_date >= @start and hit_date< @end
group by absolute_path";

            ComposeSql(sql);


            AddParameter("@start", DbType.DateTime);
            AddParameter("@end", DbType.DateTime);

            command.Parameters["@start"].Value = start;
            command.Parameters["@end"].Value = end;
        }
    }
}
