using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Web;
using Hicmah.Misc;
using Newtonsoft.Json;

namespace Hicmah.ChartData
{
    public class ChartAttackedPagesFormatter
    {
        private static TraceSourceUtil trace = new TraceSourceUtil("ChartAttackedPagesFormatter");
        
        public static string GetJson(string style, NameValueCollection queryString)
        {
            ChartAttackedPagesCached query = new ChartAttackedPagesCached();
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

            Dictionary<string, decimal> dataDictionary = query.RetrieveData(start, end);

            if (style == "Flot")
            {
                BarChartData data = ChartAttackedPagesFormatter.FlotStyle(dataDictionary);
                return JsonConvert.SerializeObject(data);
            }
            else
            {
                BarChartDataJqStyle jqStyle = ChartAttackedPagesFormatter.JqStyle(dataDictionary);
                string output = JsonConvert.SerializeObject(jqStyle);
                output = output.Substring(8, output.Length - 8 - 1);
                return output;
            }
        }

        public static BarChartData FlotStyle(Dictionary<string, decimal> dataPoints)
        {
            if(dataPoints==null)
                throw new ArgumentNullException();

            BarChartData data = new BarChartData();
            data.label = "Attacked Pages";

            object[][] points = new object[dataPoints.Count][];

            int i = 0;
            foreach (KeyValuePair<string, decimal> keyValuePair in dataPoints)
            {
                points[i] = new object[] { keyValuePair.Key, keyValuePair.Value };
                i++;
            }
            data.data = points;
            return data;
        }

        public static BarChartDataJqStyle JqStyle(Dictionary<string, decimal> dataPoints)
        {
            BarChartDataJqStyle data = new BarChartDataJqStyle();
            object[][] points = new object[dataPoints.Count][];

            int i = 0;
            foreach (KeyValuePair<string, decimal> keyValuePair in dataPoints)
            {
                points[i] = new object[] { keyValuePair.Key, keyValuePair.Value };
                i++;
            }
            data.data = points;
            return data;
        }
    }
}
