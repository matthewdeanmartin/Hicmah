using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hicmah.ChartData
{
    //Flot style
    public class TimeSeriesChartData
    {
        public string label { get; set; }
        //Number (ticks), Number (quantity to chart)
        public object[][] data { get; set; }
    }

    public class TimeSeriesChartDataJq
    {
        //date in format of 2008-11-30 4:00PM, Number (quantity to chart)
        public object[][] data { get; set; }
    }
}
