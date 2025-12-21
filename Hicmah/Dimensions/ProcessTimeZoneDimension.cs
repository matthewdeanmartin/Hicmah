using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using Dataglot;
using Hicmah.Misc;

namespace Hicmah.Dimensions
{
    public class ProcessTimeZoneDimension
    {
        private readonly TraceSourceUtil trace = new TraceSourceUtil("ProcessTimeZoneDimension");
        public void Process()
        {
            //We assume this is a cheap dimension to process, so we will just recreate it.
            using (ParameterlessCommand com = new ParameterlessCommand(ConfigUtils.CurrentDataFactory()))
            {
                com.Execute("DELETE FROM {$ns}_time_zones");
            }
            trace.TraceInformation("Cleared the _times diminsion table.");

            ReadOnlyCollection<TimeZoneInfo> tzCollection= TimeZoneInfo.GetSystemTimeZones();
            foreach (TimeZoneInfo timeZoneInfo in tzCollection)
            {
                throw new NotImplementedException();
                //Fill Chart. Look up Olsen from  http://unicode.org/repos/cldr/trunk/common/supplemental/windowsZones.xml
                //timeZoneInfo.
            }
        }
    }
}
