using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Dataglot;

namespace Hicmah.Dimensions
{
    //Unlike Date, we only expect this to run once, however the shift could vary depending on configuration
    public class InsertTime : GenericCommand
    {
        public InsertTime()
            : base(ConfigUtils.CurrentDataFactory())
        
        {
            const string sql = @"INSERT INTO [{$ns}_times]
           ([time_id]
           ,[twenty_four_hour]
           ,[hour]
           ,[minute]
           ,[AM_PM]
           ,[short_time]
           ,[long_time]
           ,[three_shift]
           ,[is_business_hours])
     VALUES
           (@time_id
           ,@twenty_four_hour
           ,@hour
           ,@minute
           ,@AM_PM
           ,@short_time
           ,@long_time
           ,@three_shift
           ,@is_business_hours)";
            ComposeSql(sql);
            AddParameter("@time_id", DbType.Int32);
            AddParameter("@twenty_four_hour", DbType.Int64);
            AddParameter("@hour", DbType.Int32);
            AddParameter("@minute", DbType.Int32);
            AddParameter("@AM_PM", DbType.String);
            AddParameter("@short_time", DbType.String);
            AddParameter("@long_time", DbType.String);
            AddParameter("@three_shift", DbType.Int32);
            AddParameter("@is_business_hours", DbType.Int32);
        }

        public void Insert(
            int timeId,
            int twentyFourHour,
            int hour,
            int minute,
            string amPm,
            string shortTime,
            string longTime,
            int threeShift,
            bool isBusinessHours
            )
        {
            command.Parameters["@time_id"].Value = timeId;
            command.Parameters["@twenty_four_hour"].Value = twentyFourHour;
            command.Parameters["@hour"].Value = hour;
            command.Parameters["@minute"].Value = minute;
            command.Parameters["@AM_PM"].Value = amPm;
            command.Parameters["@short_time"].Value = shortTime;
            command.Parameters["@long_time"].Value = longTime;
            command.Parameters["@three_shift"].Value = threeShift;
            command.Parameters["@is_business_hours"].Value = isBusinessHours;

            ExecuteNonQuery();
        }

    }
}
