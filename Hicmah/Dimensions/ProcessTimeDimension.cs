using System;
using System.Globalization;
using System.Web;
using Dataglot;
using Hicmah.Administration;
using Hicmah.DataSettings;
using Hicmah.Misc;
using Hicmah.Web;
using Wrappers.WebContext;

namespace Hicmah.Dimensions
{
    public class ProcessTimeDimension
    {
        private readonly TraceSourceUtil trace = new TraceSourceUtil("ProcessTimeDimension");
        private readonly ICacheWrapper cache;
        private readonly HicmahSettingsManager settings;
        private int workdayStart;
        private int workdayEnd;
        private CultureInfo culture;

        public ProcessTimeDimension()
        {
            if (HttpContext.Current==null)
                throw new InvalidOperationException("You need to call the constructor with the ICacheWrapper (maybe this is a unit or integration test?)");
            cache=new CacheWrapper();
            settings = new HicmahSettingsManager(cache);
        }
        //To be supplied by tests, etc.
        public ProcessTimeDimension(ICacheWrapper cacheWrapper)
        {
            cache=cacheWrapper;
            settings = new HicmahSettingsManager(cache);
        }
         

        /// <summary>
        /// Create rows in {$ns}_times, one per minute for an entire day.
        /// </summary>
        public void Process()
        {
            //We assume this is a cheap dimension to process, so we will just recreate it.
            using (ParameterlessCommand com = new ParameterlessCommand(ConfigUtils.CurrentDataFactory()))
            {
                com.Execute("DELETE FROM {$ns}_times");
            }
            trace.TraceInformation("Cleared the _times diminsion table.");

            //TODO: Make config driven.
            culture = new CultureInfo(settings.Culture(), true);
            workdayEnd = settings.WorkdayEnd();
            workdayStart = settings.WorkdayStart();
            
            //CultureInfo culture = CultureInfo.CreateSpecificCulture("en-US");
            DateTimeOffset current = new DateTime(1970, 1, 1, 0, 0, 0);//Known, timezone free day with no DST oddities.
            DateTimeOffset end = new DateTime(1970,1,2,0,0,0);//Known, timezone free day with no DST oddities.
            using(InsertTime command = new InsertTime())
            {
                while (current <end)
                {
                    double key = 1440 - (end - current).TotalMinutes + 1;

                    command.Insert(
                        (int) key,
                        current.Hour,//24?
                        current.Hour,
                        current.Minute,
                        current.ToString("tt",culture),//AM or PM
                        current.ToString("t",culture),//Short
                        current.ToString("T",culture),//long
                        Shift(current),
                        IsWorkHours(current)
                        );
                    current = current.AddMinutes(1);
                }
            }
        }

        //TODO: Make config driven
        private bool IsWorkHours(DateTimeOffset current)
        {
            if (current.Hour < workdayStart || current.Hour > workdayEnd) return false;
            return true;
        }

        private int Shift(DateTimeOffset current)
        {
            if (current.Hour < workdayStart) return 1;
            if (current.Hour > workdayEnd) return 3;
            return 2;
        }
    }
}
