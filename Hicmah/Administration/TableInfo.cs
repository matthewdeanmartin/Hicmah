// Copyright (c) 2011 Matthew D Martin
// MIT or MS-PL License as you elect, please see root directory for text of licenses.
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using Dataglot;

namespace Hicmah.Administration
{
    // Size, min, max, etc.
    public class TableInfo
    {
        public TableInfo()
        {
            Dimensions = new List<TableInfo>(10);
        }

        public string TableName { get; set; } 
        public DateTime Earliest { get; set; } //presumably constant
        public DateTime Latest { get; set; } //Likely to be close to down if the app is live.
        
        //sp_spaceused
        public int Rows { get; set; }
        public string Reserved { get; set; }
        public string Data { get; set; }
        public string IndexSize { get; set; }
        public string Unused { get; set; }

        public List<TableInfo> Dimensions { get; set; }


        public List<string> ValidateForReports()
        {
            List<string> s = new List<string>();
            if(Rows==0)
            {
                s.Add("Reports will run, but return no data since no hits have been recorded");
            }

            foreach (TableInfo dims in Dimensions)
            {
                if(dims.Rows==0)
                {
                    //These should be populated even if there is no data.
                    switch (dims.TableName)
                    {
                        case "dates":
                        case "times":
                            s.Add("Need to process " + dims.TableName + " dimension");
                            continue;
                        case "request_types":
                        case "invoker":
                            s.Add("Initial schema set up wrong. Basic constant values missing from dimension table '" +
                                  dims.TableName + "'");
                            continue;
                    }
                }

                if(Rows>0)
                {
                    if(dims.Rows==0)
                    {
                        s.Add("Dimension " + dims.TableName + " is empty. Either page hits have never included this value (unlikely), or something is wrong.");
                    }
                }

            }
            return s;
        }
    }

    public class TableInfoQuery : GenericCommand
    {
        public TableInfoQuery()
            : base(ConfigUtils.CurrentDataFactory())
        {
            
        }
        public TableInfo Execute()
        {
            TableInfo info = ExecuteTableInfo("hits");
            info.TableName = "hits";
            //Redundant with sp_spaceused, but not all databases will have equivalent to it.
            ComposeSql("Select min(hit_date) as MinDate, max(hit_date) as MaxDate, count(*) as CountOfRows FROM {$ns}_hits");
            command.CommandType = CommandType.Text;
            

            using (DbDataReader reader = ExecuteReader(CommandBehavior.SingleRow))
            {
                if (reader.HasRows)
                {
                    reader.Read();
                    if (!reader.IsDBNull(0))
                        info.Earliest = reader.GetDateTime(0);
                    if (!reader.IsDBNull(1))
                        info.Latest = reader.GetDateTime(1);
                    info.Rows = Convert.ToInt32(reader[2]);
                }
            }

            string[] dimensions = {
                                      "dates",//1
                                      "invoker",//2
                                      //"queries",//3
                                      "request_types",//4
                                      "servers",//5
                                      "time_zones",//6
                                      "times",//7
                                      "urls",//8
                                      "user_agents",//9
                                      "users"//10
                                  };

            foreach(string table in dimensions)
            {
                TableInfo dimension = ExecuteTableInfo(table);
                AddRows(dimension);
                info.Dimensions.Add(dimension);
            }

            return info;
        }

        private void AddRows(TableInfo info)
        {
            ComposeSql("Select count(*) as CountOfRows FROM {$ns}_" + info.TableName);
            command.CommandType = CommandType.Text;


            using (DbDataReader reader = ExecuteReader(CommandBehavior.SingleRow))
            {
                if (reader.HasRows)
                {
                    reader.Read();
                    info.Rows = Convert.ToInt32(reader[0]);
                }
            }
        }

        private TableInfo ExecuteTableInfo(string tableName)
        {
            TableInfo info = new TableInfo();
            info.TableName = tableName;
            if (factory.CurrentDb == DbBrand.MsSql2005)
            {
                command.CommandText = Compose("EXECUTE sp_spaceused '{$ns}_" + tableName + "'");

                using (DbDataReader reader = ExecuteReader(CommandBehavior.SingleRow))
                {
                    if (reader.HasRows)
                    {
                        reader.Read();
                        //info.Rows = Convert.ToInt32(reader[1]);
                        info.Reserved= reader.GetString(2);
                        info.Data = reader.GetString(3);
                        info.IndexSize = reader.GetString(4);
                        info.Unused= reader.GetString(5);
                        return info;
                    }
                    else
                    {
                        return info;
                    }
                }
            }
            else
            {
                return info;//return what we got
            }
        }
    }
}
