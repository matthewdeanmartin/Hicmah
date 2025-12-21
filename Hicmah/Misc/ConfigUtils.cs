using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using Dataglot;
using Dataglot.CrossDb;
using Wrappers.WebContext;

namespace Hicmah
{
    public class ConfigUtils
    {
        //Expect an System.Data.Common Compliant connection string here.
        public static DataFactory CurrentDataFactory()
        {
            DataFactory factory = new DataFactory("HicmahDb");
            return factory;
        }

        public static DataFactory SpecificFactory(DbBrand brand )
        {
            DataFactory factory = new DataFactory(brand, ConfigurationManager.ConnectionStrings["HicmahDb"].ConnectionString);
            return factory;
        }

        private static bool ValidateConnectionString(string name)
        {
            if (ConfigurationManager.ConnectionStrings[name] == null)
            {
                if(!AutomaticConfiguration())
                {
                    throw new InvalidOperationException("Expected a connection string with a name of '" + name + "' in the web.config or app.config file, but didn't find one.");
                }
                return false;
            }
            return true;
        }

        private static bool ValidateAppSetting(string name)
        {
            if (ConfigurationManager.AppSettings[name] == null)
            {
                if (!AutomaticConfiguration())
                {
                    throw new InvalidOperationException("Expected an AppSettings with a name of '" + name +
                                                        "' in the web.config or app.config file, but didn't find one.");
                }
                return false;
            }
            return true;
        }

        public static bool AutomaticConfiguration()
        {
            //ValidateAppSetting("Hicmah.AutomaticConfiguration");
            if (ConfigurationManager.AppSettings["Hicmah.AutomaticConfiguration"] == null) return false;
            if (ConfigurationManager.AppSettings["Hicmah.AutomaticConfiguration"] == "1") return true;
            if (ConfigurationManager.AppSettings["Hicmah.AutomaticConfiguration"] == "True") return true;
            if (ConfigurationManager.AppSettings["Hicmah.AutomaticConfiguration"] == "true") return true;
            string result = ConfigurationManager.AppSettings["Hicmah.AutomaticConfiguration"];
            return Convert.ToBoolean(result);
        }

        public static string AsyncConnectionString()
        {
            if(!ValidateConnectionString("asyncConnectionString"))
            {
                return @"Data Source=.\sqlexpress;Initial Catalog=master;Integrated Security=SSPI;ASYNC=TRUE;MultipleActiveResultSets=True";
            }
            return ConfigurationManager.ConnectionStrings["asyncConnectionString"].ConnectionString;
        }

        //Requires cache because guessing is expensive.
        public static string AutomaticConnectionString()
        {
            return AutomaticConnectionString(new CacheWrapper(),"hicmah");
        }

        public static string AutomaticConnectionString(ICacheWrapper cache, string preferredName)
        {
            if (cache["HicmahDb"] != null)
            {
                return cache["HicmahDb"].ToString();
            }

            //We could at this point attempt to guess a better string.. for example, different instance name
            //but that would be slower.

            SqlDataSourceEnumerator instance = SqlDataSourceEnumerator.Instance;

            //Schema for table
            //servername, instancename, isclustered, version
            System.Data.DataTable table = instance.GetDataSources();
            DataRow[] localInstances = table.Select("ServerName = '" + Environment.MachineName +"'");
            if(!localInstances.Any())
            {
                    throw new InvalidOperationException("Can't detect any local instances of SQL Server");
            }

            string preferredServer=null;
            DataRow[] defaultInstance = table.Select("ServerName = '" + Environment.MachineName + "' AND InstanceName = ''");
            if(defaultInstance.Count()==1)
            {
                preferredServer = defaultInstance[0]["ServerName"].ToString();
            }
            else
            {
                DataRow[] sqlExpress = table.Select("ServerName = '" + Environment.MachineName + "' AND InstanceName = 'SQLEXPRESS'");
                if(sqlExpress.Count()==1)
                {
                    preferredServer = @".\SQLEXPRESS";
                }
            }
            if(string.IsNullOrEmpty(preferredServer))
            {
                DataRow[] sqlExpress = table.Select("ServerName = '" + Environment.MachineName + "'  AND InstanceName <> ''");
                if (sqlExpress.Count() == 1)
                {
                    preferredServer = @".\" + defaultInstance[0]["InstanceName"]; 
                }
            }

            if (string.IsNullOrEmpty(preferredServer))
            {
                throw new InvalidOperationException("Can't decide which local server to use. Tried, in order, default, SQLEXPRESS and any instance.");
            }

            string defaultString = "Data Source=" + preferredServer +  @";Initial Catalog=" + preferredName + ";Integrated Security=SSPI;";

            return defaultString;
        }

        //TODO: This has to vary based on available and desired datasources.
        public static string CreateDbWithAutomaticString()
        {
            string connectionString = AutomaticConnectionString();
            DataFactory factory = new DataFactory(DbBrand.MsSql2005, connectionString);
            DbConnection con = factory.Connection();

            con.ConnectionString = connectionString;

            ConnectToMaster guesser = new ConnectToMaster();
            return guesser.CreateDatabaseIfNeeded((SqlConnection)con, "hicmah");   
        }

        public static string ConnectionString()
        {
            if(!ValidateConnectionString("HicmahDb"))
            {
                if(HttpContext.Current!=null)
                {
                   // HttpContext.Current.Response.Redirect("Install.aspx");
                }
                throw new InvalidOperationException("We need a connection string in a predicable place.");
            }
            return ConfigurationManager.ConnectionStrings["HicmahDb"].ConnectionString;
        }

        public static int BufferSize()
        {
            if(!ValidateAppSetting("hicmahBuffer"))
            {
                return 100;
            }
            string buffer = ConfigurationManager.AppSettings["hicmahBuffer"];
            return int.Parse(buffer);
        }

        public static string Provider()
        {
            if(!ValidateAppSetting("Dataglot.Provider"))
            {
                return "mssql";
            }
            string how = ConfigurationManager.AppSettings["Dataglot.Provider"].ToLower();
            if (string.IsNullOrEmpty(how))
                throw new InvalidOperationException("Dataglot.Provider is missing from AppSettings");
            return how;
        }
    }
}
