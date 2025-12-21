using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hicmah.ChartData.MissingPage
{
    public class Row
    {
        public string id { get; set; }
        public string[] cell { get; set; }
    }


    /*
     * Sample
     * 
            {name:'id',index:'id', width:55},
            {name:'invdate',index:'invdate', width:90},
            {name:'name',index:'name asc, invdate', width:100},
            {name:'amount',index:'amount', width:80, align:"right"},
            {name:'tax',index:'tax', width:80, align:"right"},		
            {name:'total',index:'total', width:80,align:"right"},		
            {name:'note',index:'note', width:150, sortable:false}	
    */
    public class Userdata
    {
        public string Path { get; set; }
        public DateTime MissingSince { get; set; }
        public DateTime LastAttempedRetrieval { get; set; }
        public int Incidents { get; set; }

        //public int amount { get; set; }
        //public int tax { get; set; }
        //public int total { get; set; }
        //public string name { get; set; }
    }

    public class JqGridData
    {
        public string page { get; set; }
        public int total { get; set; }
        public string records { get; set; }
        public Row[] rows { get; set; }
        public Userdata userdata { get; set; }
    }

}
