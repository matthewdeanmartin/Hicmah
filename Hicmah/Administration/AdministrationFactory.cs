// Copyright (c) 2011 Matthew D Martin
// MIT or MS-PL License as you elect, please see root directory for text of licenses.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using Hicmah.Misc;

namespace Hicmah.Administration
{
    public class AdministrationFactory
    {
        private readonly TraceSourceUtil trace = new TraceSourceUtil("AdministrationFactory");

        public DatabaseAdministration DatabaseAdministration()
        {
            
            trace.WriteLine("Creating AdminFactory and choosing the default, config file db provider");
            string how = ConfigUtils.Provider();
            
            return DatabaseAdministration(how);
        }
        public  DatabaseAdministration DatabaseAdministration(string how)
        {
            switch (how)
            {
                case "mssql":
                case "mssql-bulk":
                    {
                        trace.WriteLine("Creating AdminFactory of the Ms-Sql sort");
                        return new DatabaseAdministrationForSql();
                    }
                default:
                    throw new ArgumentException("Unknown type of database " + how, "how");
            }

        }

    }
}
