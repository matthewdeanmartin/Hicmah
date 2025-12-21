using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hicmah.DataCache;

namespace Hicmah.ChartData
{
    public class ChartHitsPerTimeSpanCached
    {
        public Dictionary<long, long> RetrieveData(DateTime start, DateTime end, string how)
        {
            CacheInDb<Dictionary<long, long>> cacher = new CacheInDb<Dictionary<long, long>>();

            const string queryName = "BuggiestPages";
            string parameters = start.ToString() + end.ToString();
            Dictionary<long, long> dataPoints = cacher.SearchDb(parameters, queryName);
            if (dataPoints != null && dataPoints.Count > 0)
                return dataPoints;

            // Cache miss
            using (ChartHitsPerTimeSpan dataCommand = new ChartHitsPerTimeSpan())
            {
                dataPoints = dataCommand.RetrieveData(start, end, how);
            }

            cacher.StoreInDb(dataPoints, parameters, queryName);

            return dataPoints;
        }
    }
}
