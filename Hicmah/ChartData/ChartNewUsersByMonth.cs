using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using System.Data.Common;
using Dataglot;

namespace Hicmah.ChartData
{
    public class ChartNewUsersByMonth : GenericCommand
    {
        public ChartNewUsersByMonth()
            : base(ConfigUtils.CurrentDataFactory())
        {
            
        }
        /// <summary>
        /// Calculate how many new users showed up per month
        /// </summary>
        /// <param name="window">The window (in minutes) for which users are considered concurrently online</param>
        /// <param name="startingMonth"></param>
        /// <param name="endOfInterval"></param>
        public void RetrieveData(int window, DateTime startingMonth, DateTime endOfInterval)
        {

            //A cheap pre-calculation and then we don't need to be clever with the sql
            string sql = @"
select [USER], first_use
from {$ns}_users
where first_use >= @start and first_use<  @end 
";

            ComposeSql(sql);

            AddParameter("@start", DbType.DateTime);
            AddParameter("@end", DbType.DateTime);

            //int sum = 0;
            //int nonZeroEntries = 0;
            transaction = con.BeginTransaction(IsolationLevel.ReadUncommitted);
            command.Transaction = transaction;
            while (startingMonth < endOfInterval)
            {
                command.Parameters["@start"].Value = startingMonth;
                DateTime upcomingMonth;
                if (startingMonth.Month == 12)
                {
                    upcomingMonth = new DateTime(startingMonth.Year + 1, 1, 1);
                }
                else
                {
                    upcomingMonth = new DateTime(startingMonth.Year, startingMonth.Month+1, 1);
                }

                command.Parameters["@end"].Value = upcomingMonth;

                System.Diagnostics.Debug.WriteLine("-- month of -- " + startingMonth);

                int i = 0;
                
                using (DbDataReader reader = ExecuteReader(CommandBehavior.Default))
                {
                    
                    Dictionary<string, DateTime> newUsers = new Dictionary<string, DateTime>();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            newUsers.Add(reader.GetString(0), reader.GetDateTime(1));
                            i++;
                        }
                    }
                    else
                    {
                        System.Diagnostics.Debug.WriteLine(" 0 - no one new");
                    }
                    foreach (var person in newUsers)
                    {
                        System.Diagnostics.Debug.WriteLine(string.Format("{0} started and was first sighted on {1:d}", person.Key, person.Value));
                    }
                }
                System.Diagnostics.Debug.WriteLine(i + " total new users");
                startingMonth = upcomingMonth;
            }
            
        }
        

    }
}
