using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using Dataglot;
using Hicmah.ChartData.MissingPage;

namespace Hicmah.ChartData
{
    //Dump the hits, most recent first, paged, sorted.
    public class RecentHitsTable: GenericCommand
    {
        public RecentHitsTable()
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
            //MissingPage.JqGridData dataPoints = new JqGridData();

            SetUpCommand(start, end);

            throw new NotImplementedException();
        }

        private void SetUpCommand(DateTime start, DateTime end)
        {
            string sql = @"select top 500 *
from {$ns}_hit_data
WHERE hit_date >= @start and hit_date< @end
ORDER BY hit_Date desc";

            ComposeSql(sql);

            AddParameter("@start", DbType.DateTime);
            AddParameter("@end", DbType.DateTime);

            command.Parameters["@start"].Value = start;
            command.Parameters["@end"].Value = end;
        }
    }
}

