using System;
using System.Globalization;
using System.Web;
using Hicmah.Administration;
using Hicmah.DataSettings;
using Hicmah.Misc;
using Hicmah.Web;
using Wrappers.WebContext;

namespace Hicmah.Dimensions
{
    public  class ProcessDateDimension
    {
        private readonly TraceSourceUtil trace = new TraceSourceUtil("ProcessDateDimension");

        private readonly ICacheWrapper cache;
        private readonly HicmahSettingsManager settings;
        private CultureInfo culture;
        private int fiscalStartMonth;

        public ProcessDateDimension()
        {
            if (HttpContext.Current == null)
                throw new InvalidOperationException("You need to call the constructor with the ICacheWrapper (maybe this is a unit or integration test?)");
            cache=new CacheWrapper();
            settings = new HicmahSettingsManager(cache);
        }
        //To be supplied by 
        public ProcessDateDimension(ICacheWrapper cacheWrapper)
        {
            cache=cacheWrapper;
            settings= new HicmahSettingsManager(cache);
        }

        public void ProcessWithZonedDateTime()
        {
            //Instant now = Instant.FromDateTimeUtc(new DateTime(2011, 1, 1));
            //ZonedDateTime zdt = new ZonedDateTime(now, DateTimeZone.SystemDefault);//Use server DTZ
            ////LocalDateTime ldt = new LocalDateTime(2011,11,1,0,0,0);
            
            //Instant end = Instant.FromDateTimeUtc(new DateTime(2012, 1, 1));
            //ZonedDateTime zdtEnd = new ZonedDateTime(end, DateTimeZone.SystemDefault);//Use server DTZ
            //LocalDateTime ldt = new LocalDateTime();
            ////ldt.
            //using(InsertDate command = new InsertDate())
            //{
            //    while(zdt.ToInstant()<zdtEnd.ToInstant())
            //    {
            //        command.Insert(
            //            zdt.DayOfYear,
            //            zdt.MonthOfYear,
            //            zdt.DayOfYear,
            //            zdt.
            //            )

            //    }
            //}
            
        }

        public void ProcessWithDateTime(DatabaseAdministration admin)
        {
            trace.TraceInformation("We didn't truncate the date table, we are just adding to it.");
            
            //user can pick a culture, a server time zone, start, end, fiscal year start date
            fiscalStartMonth = settings.FiscalYearStartMonth();
            culture = new CultureInfo(settings.Culture(), true);// CultureInfo.CurrentCulture;
            //CultureInfo culture = CultureInfo.CreateSpecificCulture();

            TableInfo info = admin.TableInfo();

            //n.b. earliest is earliest hit, which could be in the middle of the day.
            DateTime current = new DateTime(info.Earliest.Year, info.Earliest.Month, info.Earliest.Day); 
            if(current==DateTime.MinValue)// new DateTime(2010, 1, 1);
            {
                trace.TraceInformation("Didn't find any pre-existing hits, using settings table for default start of date dimension");
                current = settings.EarliestHitDate();
            }
            DateTime end = DateTime.Now.AddDays(30);

            //Mostly assuming Gregorian calendars
            using(InsertDate command = new InsertDate())
            {
                while(current<end)
                {
                    try
                    {
                        command.Insert(DateUtils.GetDateId(current),current.Ticks, // Primary key, if you can convert date to ticks, you have the PK, i.e. no lookup
                                   current, current.Year, current.Month, current.Day, FirstOfMonth(current), FiscalYear(current, fiscalStartMonth), FiscalQuarter(current, fiscalStartMonth), Quarter(current.Month), WeekNumber(current), //Week number using some plausible defaults
                                   IsWeekend(current.DayOfWeek), IsHoliday(current, CultureInfo.CurrentCulture),//
                                   current.ToString("MMM", culture), //should be the month in the server's language
                                   current.ToString("MMMM", culture), //should be the month in the server's language

                                   current.ToString("MMM yy", culture) , //should be the month in the server's language
                                   current.ToString("MMMM yyyy", culture), //should be the month in the server's language

                                   current.ToString("ddd", culture), //should be the day in the server's language, abbreviated
                                   current.ToString("dddd", culture), //should be the day in the server's language
                                   current.DayOfWeek, current.ToString("D", culture), //long date
                                   current.ToString("s", culture), //sortable
                                   current.ToString("u", culture), //universal sortable
                                   current.ToString("r", culture) //RFC1123 standards
                        );
                    }
                    catch (Exception ex)
                    {
                        //TODO: Get a 'dimension info' query to determine where to pickup from.
                        //Hmm, likely to vary by implementation
                        if(ex.Message.Contains("Violation of PRIMARY KEY constraint"))
                        {
                            //carry on. 
                        }
                        else
                            throw;
                    }
                    
                    current=current.AddDays(1);
                }
            }

        }

        private bool IsHoliday(DateTime current, CultureInfo currentCulture)
        {
            return false;
            //if(currentCulture.EnglishName=="en-US")
            //{
              //  if(current.Month==1 && current.Day ==1 && !IsWeekend(current.DayOfWeek)) return true;
//                if (current.Month == 1 && current.Day == 2 && !IsWeekend(current.DayOfWeek)) return true;

                //Monday, January 2*	New Year's Day
                //Monday, January 16	Birthday of Martin Luther King, Jr.
                //Monday, February 20**	Washington's Birthday
                //Monday, May 28	Memorial Day
                //Wednesday, July 4	Independence Day
                //Monday, September 3	Labor Day
                //Monday, October 8	Columbus Day
                //Monday, November 12***	Veterans Day
                //Thursday, November 22	Thanksgiving Day
                //Tuesday, December 25	Christmas Day
  //          }
        }

        public static int FiscalQuarter(DateTime current, int fiscalStartMonth)
        {
            //TODO: Need to unit test this.
            return 1;// Quarter(12 % (current.Month + (12 - fiscalStartMonth)));
        }

        private static int FiscalYear(DateTime current, int fiscalStartMonth)
        {
            if (current.Month > fiscalStartMonth)
                return current.Year + 1;
            else
            {
                return current.Year;
            }
        }

        public DateTime FirstOfMonth(DateTime current)
        {
            return new DateTime(current.Year,current.Month,1);
        }

        //http://madskristensen.net/post/Working-with-weeks-in-C-not-that-obvious.aspx
        public static int WeekNumber(DateTime date)
        {
            GregorianCalendar cal = new GregorianCalendar(GregorianCalendarTypes.Localized);
            return cal.GetWeekOfYear(date, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
        }

        public bool IsWeekend(DayOfWeek dayOfWeek)
        {
            return dayOfWeek == DayOfWeek.Saturday || dayOfWeek == DayOfWeek.Sunday ? true: false;
        }

        public int Quarter(int month)
        {
            if(month>12)
                throw new ArgumentException("Month can't be greater than 12");
            if (month < 1)
                throw new ArgumentException("Month can't be less than 1");
            if (month >= 1 && month <= 3) return 1;
            if (month >= 4 && month <= 6) return 2;
            if (month >= 7 && month <= 9) return 3;
            if (month >= 10 && month <= 12) return 4;
            throw new InvalidOperationException("Failed to identify quarter");
        }
    }
}
