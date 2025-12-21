using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hicmah.ChartData
{
    //Flot style
    //http://json2csharp.com/
    public class BarChartData
    {
        public string label { get; set; }
        public object[][] data { get; set; }
    }


    public class BarChartDataJqStyle
    {
        public object[][] data { get; set; }
    }
}
