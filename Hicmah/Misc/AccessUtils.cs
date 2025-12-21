using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hicmah.MsAccess
{
    public class AccessUtils
    {
        public static DateTime SmallerDateTime(DateTime dateWithMillis)
        {
            return new DateTime(dateWithMillis.Year, dateWithMillis.Month, dateWithMillis.Day, dateWithMillis.Hour, dateWithMillis.Minute, dateWithMillis.Second);
        }
    }
}
