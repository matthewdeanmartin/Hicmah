// Copyright (c) 2011 Matthew D Martin
// MIT or MS-PL License as you elect, please see root directory for text of licenses.
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Data.Common;
using Dataglot;

namespace Hicmah.ChartData
{
    public class DatabaseInformationSql : GenericCommand
    {
        public DatabaseInformationSql()
            : base(ConfigUtils.CurrentDataFactory())
        {
            
        }
        public string SpaceUsed()
        {
            OpenConnection();

            command.CommandText = @"sp_spaceused";
            command.CommandType = CommandType.StoredProcedure;
            command.Connection = con;

            DbParameter parameter = factory.Parameter("@table", DbType.String);
            command.Parameters.Add(parameter);
            parameter = factory.Parameter("@updateusage", DbType.String);
            command.Parameters.Add(parameter);

            command.Parameters["@table"].Value = "{$ns}_hits";
            command.Parameters["@updateusage"].Value = "true";

            
            if (transaction != null)
                command.Transaction = transaction;
            
            using (DbDataReader reader = ExecuteReader(CommandBehavior.Default))
            {
                if (reader.HasRows)
                {
                    reader.Read();
                    return
                        reader[1] + " rows " +
                        reader[2] + " reserved " +
                        reader[3] + " data size " +
                        reader[4] + " index size " +
                        reader[5] + " unused space ";
                }
                else
                {
                    throw new InvalidOperationException("Failed to get data from database.");
                }
            }
        }
    }
}
