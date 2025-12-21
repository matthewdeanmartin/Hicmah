using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hicmah.DataCache;

namespace Hicmah.ChartData
{
    public class ChartBuggiestPagesCached
    {
        public Dictionary<string, decimal> RetrieveData(DateTime start, DateTime end)
        {
            CacheInDb<Dictionary<string, decimal>> cacher = new CacheInDb<Dictionary<string, decimal>>();

            const string queryName = "BuggiestPages";
            string parameters = start.ToString() + end.ToString();
            Dictionary<string, decimal> dataPoints = cacher.SearchDb(parameters, queryName);
            if (dataPoints != null && dataPoints.Count > 0)
                return dataPoints;

            // Cache miss
            using (ChartBuggiestPages dataCommand = new ChartBuggiestPages())
            {
                dataPoints = dataCommand.RetrieveData(start, end);
            }

            cacher.StoreInDb(dataPoints, parameters, queryName);

            return dataPoints;
        }
    }
}
