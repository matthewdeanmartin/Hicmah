using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IrishSettings;
using Hicmah.Web;
using Wrappers.WebContext;

namespace Hicmah.DataSettings
{
    public class HicmahSettingsManager
    {
         private readonly IrishSettingsManagerCached manager;

         public HicmahSettingsManager(ICacheWrapper newCache)
         {
             if (newCache == null)
             {
                 throw new ArgumentException("Cache can't be null. If you need a non-cached SettingsManager, use IrishSettingsManager");
             }
             HicmahSetingsCollection collection = new HicmahSetingsCollection();
             //Could pick up pref benefit by selecting this manually here.
             manager = new IrishSettingsManagerCached(collection.Current(), newCache);
         }

         public  string Culture()
        {
            return manager.Setting("culture").Value.ToString();
        }

        public string ServerTimeZone()
        {
            return manager.Setting("timezone").Value.ToString();
        }

        public int FiscalYearStartMonth()
        {
            return Convert.ToInt32(manager.Setting("fiscalStart").Value);
        }

        public int WorkdayEnd()
        {
            return Convert.ToInt32(manager.Setting("workdayEnd").Value);
        }

        public int WorkdayStart()
        {
            return Convert.ToInt32(manager.Setting("workdayStart").Value);
        }
        public DateTime EarliestHitDate()
        {
            return Convert.ToDateTime(manager.Setting("earliestHitDate").Value);
        }
    }
}
