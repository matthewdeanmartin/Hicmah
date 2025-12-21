using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Web;
using Newtonsoft.Json;

namespace Hicmah.ChartData
{
    public class ChartHitsPerTimeSpanFormatter
    {
        public static string GetJson(string style, NameValueCollection queryString)
        {
            if (string.IsNullOrEmpty(style))
                throw new ArgumentNullException();
            if (queryString == null)
                throw new ArgumentNullException();

            ChartHitsPerTimeSpan query = new ChartHitsPerTimeSpan();
            DateTime start;
            DateTime end;
            if (queryString["Start"] != null)
                start = Convert.ToDateTime(queryString["Start"]);
            else
                throw new HttpException(400, "Missing Start parameter");

            if (queryString["End"] != null)
                end = Convert.ToDateTime(queryString["End"]);
            else
                throw new HttpException(400, "Missing End parameter");

            Dictionary<long, long> dataDictionary = query.RetrieveData(start, end, "monthly");

            if (style == "Flot")
            {
                TimeSeriesChartData data = FlotStyle(dataDictionary);
                return JsonConvert.SerializeObject(data);
            }
            else
            {
                TimeSeriesChartDataJq jqStyle = JqStyle(dataDictionary);
                string output = JsonConvert.SerializeObject(jqStyle);
                output = output.Substring(8, output.Length - 8 - 1);
                return output;
            }
        }

        public static TimeSeriesChartData FlotStyle(Dictionary<long, long> dataPoints)
        {
            TimeSeriesChartData data = new TimeSeriesChartData();
            data.label = "Hits Per Month";

            object[][] points = new object[dataPoints.Count][];

            int i = 0;
            foreach (KeyValuePair<long, long> keyValuePair in dataPoints)
            {
                points[i] = new object[] { (long) DateUtils.MillisecondsSinceEpoch(keyValuePair.Key), keyValuePair.Value };
                i++;
            }
            data.data = points;
            return data;
        }

        //TimeSeriesChartDataJq
        public static TimeSeriesChartDataJq JqStyle(Dictionary<long, long> dataPoints)
        {
            TimeSeriesChartDataJq data = new TimeSeriesChartDataJq();
            object[][] points = new object[dataPoints.Count][];

            int i = 0;
            foreach (KeyValuePair<long, long> keyValuePair in dataPoints)
            {
                points[i] = new object[] { DateUtils.GetDateFromId(keyValuePair.Key), keyValuePair.Value };
                i++;
            }
            data.data = points;
            return data;
        }
    }
}
