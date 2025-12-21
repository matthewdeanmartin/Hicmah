using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;
using System.Data;

namespace Hicmah.ChartData
{

    public class PercentOfTotalDataPoint
    {
        public string Name { get; set; }
        public decimal Measure { get; set; }
        public decimal? Percent { get; set; }
    }

    public class AggregateStatistics
    {
        public decimal Sum { get; set; }
        public decimal? Average { get; set; }
        public int Count { get; set; }
    }

    public class DateValueBinder
    {
        //name, hits, percent of total
        public AggregateStatistics Bind(DbCommand chartCommand, out List<PercentOfTotalDataPoint> data)
        {

            decimal sum = 0;
            int entries = 0;
            using (DbDataReader reader = chartCommand.ExecuteReader(CommandBehavior.Default))
            {
                if (reader.HasRows)
                {
                    data = new List<PercentOfTotalDataPoint>();
                    while (reader.Read())
                    {
                        sum = reader.GetInt32(1) + sum;
                        entries++;
                        data.Add(new PercentOfTotalDataPoint()
                            {
                                Name = reader.GetString(0), //e.g. name of user
                                Measure = reader.GetInt32(1) //hits by that user
                            });

                    }

                    foreach (PercentOfTotalDataPoint point in data)
                    {
                        if (sum > 0)
                            point.Percent = (point.Measure / sum) * 100;
                        else
                            point.Percent = null;
                    }
                }
                else
                {
                    throw new InvalidOperationException("Failed to get data from database.");
                }
            }

            return new AggregateStatistics() { Sum = sum, Average = sum / entries, Count = entries };
        }
    }

    public class NamedValueBinder
    {
        //name, hits, percent of total
        //Percent of total is percent of retrieve rows! If returning top n, 2nd query will need to be run to get true sum.
        public AggregateStatistics Bind(DbCommand chartCommand, out List<PercentOfTotalDataPoint> data)
        {
            
            decimal sum = 0;
            int entries = 0;
            using (DbDataReader reader = chartCommand.ExecuteReader(CommandBehavior.Default))
            {
                if (reader.HasRows)
                {
                    data = new List<PercentOfTotalDataPoint>();
                    while (reader.Read())
                    {
                        sum = reader.GetInt32(1) + sum;
                        entries++;
                        data.Add(new PercentOfTotalDataPoint()
                            {
                                Name = reader.GetString(0), //e.g. name of user
                                Measure = reader.GetInt32(1) //hits by that user
                            });
                        
                    }

                    foreach (PercentOfTotalDataPoint point in data)
                    {
                        point.Percent = (point.Measure / sum )* 100;
                    }
                }
                else
                {
                     data = new List<PercentOfTotalDataPoint>();
                     return new AggregateStatistics();
                   // throw new InvalidOperationException("Failed to get data from database.");
                }
            }

            
            return new AggregateStatistics() { Sum = sum, Average = entries>0?(decimal?) (sum / entries):null, Count = entries };
        }
    }
}
