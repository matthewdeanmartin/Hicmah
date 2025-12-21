using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Hicmah.DataCache;
using Hicmah.Misc;
using ProtoBuf;

namespace Hicmah.ChartData
{
    /// <summary>
    /// Get data from a persistent cache, or else fetch and store it
    /// </summary>
    public class ChartAttackedPagesCached
    {
         private TraceSourceUtil trace = null;
         public ChartAttackedPagesCached()
        {
            trace = new TraceSourceUtil(this.GetType().Name);
        }

        public Dictionary<string, decimal> RetrieveData(DateTime start, DateTime end)
        {
            CacheInDb<Dictionary<string, decimal>> cacher = new CacheInDb<Dictionary<string, decimal>>();

            const string queryName = "AttackedPages";
            string parameters = start.ToString() + end.ToString();
            Dictionary<string, decimal> dataPoints = cacher.SearchDb(parameters,queryName);
            if (dataPoints != null && dataPoints.Count>0)
                return dataPoints;

            // Cache miss
            using(ChartAttackedPages dataCommand = new ChartAttackedPages())
            {
                dataPoints= dataCommand.RetrieveData(start, end);
            }

            cacher.StoreInDb(dataPoints, parameters, queryName);
            
            return dataPoints;
        }

    }
}

