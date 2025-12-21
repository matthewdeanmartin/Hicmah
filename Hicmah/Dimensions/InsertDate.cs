using System;
using System.Data;
using Dataglot;

namespace Hicmah.Dimensions
{
    public class InsertDate : GenericCommand
    {
        public InsertDate():base(ConfigUtils.CurrentDataFactory())
        {
            string sql = @"
INSERT INTO [{$ns}_dates]
           ([date_id]
           ,[ticks]
           ,[date_key]
           ,[year]
           ,[month]
           ,[day]
           ,[first_of_month]
           ,[fiscal_year]
           ,[fiscal_quarter]
           ,[calendar_quarter]
           ,[week_number]
           ,[workday_weekend]
           ,[holiday]
           ,[short_month]
           ,[long_month]
,short_year_month
,long_year_month
           ,[short_day]
           ,[long_day]
           ,[day_of_week]
           ,[long_date]
           ,[sortable_date]
           ,[usortable_date]
           ,[rfc1123_date])
     VALUES
           ( @date_id
           , @ticks
           , @date_key
           , @year
           , @month
           , @day
           , @first_of_month
           , @fiscal_year
           , @fiscal_quarter
           , @calendar_quarter
           , @week_number
           , @workday_weekend
           , @holiday
           , @short_month
           , @long_month
, @short_year_month
, @long_year_month
           , @short_day
           , @long_day
           , @day_of_week
           , @long_date
           , @sortable_date
           , @usortable_date
           , @rfc1123_date )
";
            ComposeSql(sql);
            AddParameter("@date_id", DbType.Int32);
            AddParameter("@ticks", DbType.Int64);
            AddParameter("@date_key", DbType.DateTime);

            AddParameter("@year", DbType.Int32);
            AddParameter("@month", DbType.Int32);
            AddParameter("@day", DbType.Int32);

            AddParameter("@first_of_month", DbType.DateTime);
            AddParameter("@fiscal_year", DbType.Int32);
            AddParameter("@fiscal_quarter", DbType.Int32);
            AddParameter("@calendar_quarter", DbType.Int32);

            AddParameter("@week_number", DbType.Int32);
            AddParameter("@workday_weekend", DbType.Int32);
            AddParameter("@holiday", DbType.Int32);

            AddParameter("@short_month", DbType.String);
            AddParameter("@long_month", DbType.String);

            AddParameter("@short_year_month", DbType.String);
            AddParameter("@long_year_month", DbType.String);

            AddParameter("@short_day", DbType.String);
            AddParameter("@long_day", DbType.String);
            AddParameter("@day_of_week", DbType.String);

            AddParameter("@long_date", DbType.String);
            AddParameter("@sortable_date", DbType.String);
            AddParameter("@usortable_date", DbType.String);
            AddParameter("@rfc1123_date", DbType.String);
            
        }

        public void Insert(
            int dateId, 
            long ticks, DateTime current, 
            int year, int month, int day, 
            DateTime firstOfMonth, int fiscalYear, int fiscalQuarter, int quarter, int weekNumber, 
            bool isWeekend, bool isHoliday, 
            string shortMonth, 
            string longMonth,
            string shortYearMonth,
            string longYearMonth, 
            string shortDay, 
            string longDay, 
            DayOfWeek dayOfWeek, 
            string longDate, 
            string sortableDate, 
            string usortableDate, 
            string rfc1123Date)
        {
            command.Parameters["@date_id"].Value = dateId;
            command.Parameters["@ticks"].Value = ticks;
            command.Parameters["@date_key"].Value = current;
            command.Parameters["@year"].Value = year;
            command.Parameters["@month"].Value = month;
            command.Parameters["@day"].Value = day;
            command.Parameters["@first_of_month"].Value = firstOfMonth;
            command.Parameters["@fiscal_year"].Value = fiscalYear;
            command.Parameters["@fiscal_quarter"].Value = fiscalQuarter;
            command.Parameters["@calendar_quarter"].Value = quarter;
            command.Parameters["@week_number"].Value = weekNumber;
            command.Parameters["@workday_weekend"].Value = isWeekend ? 1 : 0;
            command.Parameters["@holiday"].Value = isHoliday?1:0;
            command.Parameters["@short_month"].Value = shortMonth;
            command.Parameters["@long_month"].Value = longMonth;

            command.Parameters["@short_year_month"].Value = shortYearMonth;
            command.Parameters["@long_year_month"].Value = longYearMonth;

            command.Parameters["@short_day"].Value = shortDay;
            command.Parameters["@long_day"].Value = longDay;
            command.Parameters["@day_of_week"].Value = dayOfWeek;
            command.Parameters["@long_date"].Value = longDate;
            command.Parameters["@sortable_date"].Value = sortableDate;
            command.Parameters["@usortable_date"].Value = usortableDate;
            command.Parameters["@rfc1123_date"].Value = rfc1123Date;
            ExecuteNonQuery();
        }
    }
}
