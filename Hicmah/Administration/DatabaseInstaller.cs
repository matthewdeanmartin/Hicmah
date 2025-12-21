using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DatabaseSchemaReader;
using DatabaseSchemaReader.DataSchema;
using DatabaseSchemaReader.SqlGen;

namespace Hicmah.Administration
{
    public class DatabaseInstaller
    {
        Ts t =new Ts();

        public string Test2()
        {
            DatabaseReader reader = new DatabaseReader(ConfigUtils.ConnectionString(),SqlType.SqlServer);
            //reader.AllTables();

            DatabaseSchema schema = reader.ReadAll();// new DatabaseSchema(ConfigUtils.ConnectionString(), ConfigUtils.Provider());
            List<DatabaseTable> listOfTablesToRemove= new List<DatabaseTable>();

            foreach (DatabaseTable table in schema.Tables)
            {
                if(!table.Name.Contains("{$ns}_"))
                {
                    listOfTablesToRemove.Add(table);
                }
            }

            foreach (DatabaseTable databaseTable in listOfTablesToRemove)
            {
                schema.Tables.Remove(databaseTable);
            }

            //TODO: Have to infer db type before this step
            DdlGeneratorFactory gen = new DdlGeneratorFactory(SqlType.SqlServer);
            ITablesGenerator tablesGenerator = gen.AllTablesGenerator(schema);
            tablesGenerator.IncludeSchema = false;
            string result = tablesGenerator.Write();
            return result;
        }

        public string Install()
        {
            //Can we even connect?

            //Do we need to create the db?
            string prefix = "test_";

            DatabaseSchema schema = new DatabaseSchema(ConfigUtils.ConnectionString(), ConfigUtils.Provider());
            
            //Do we have a hit table?
            //var item = (from table in schema.Tables where table.Name == t.Hits select table).First();
            //No? Create it.

            schema.AddTable(prefix +"Foo");

            DatabaseTable blahTable = schema.Tables[0];// new DatabaseTable();
            DatabaseColumn dc = new DatabaseColumn();
            dc.DbDataType = "VARCHAR(200)";
            dc.Name = "blah";
            blahTable.Columns.Add(dc);


            //TODO: Have to infer db type before this step
            DdlGeneratorFactory gen = new DdlGeneratorFactory(SqlType.SqlServer);
            ITablesGenerator tablesGenerator = gen.AllTablesGenerator(schema);
            tablesGenerator.IncludeSchema = false;
            string result= tablesGenerator.Write();
            return result;
            //Repeat for other tables.

            //Check for each column.  If it doesn't exist, add it.
        }


    }
}
