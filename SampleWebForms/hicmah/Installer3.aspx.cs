using System;
using System.Data.SqlClient;
using System.Text;
using System.Web;

using System.Web.UI.WebControls;
using Dataglot;
using Hicmah.Administration;
using Hicmah.Dimensions;
using Hicmah.Misc;
using Hicmah.SimulateHits;

using Wrappers.WebContext;

namespace SampleWebForms.hicmah
{
    public partial class Installer3 : System.Web.UI.Page
    {
        readonly AdministrationFactory adminFactory = new AdministrationFactory();
        private readonly TraceSourceUtil trace = new TraceSourceUtil("Default.aspx");

        protected override void OnInit(EventArgs e)
        {
            //Load += Page_Load;
            CreateIndexes.Command += CreateIndexes_Command;
            CreateSampleData.Command += CreateSampleData_Command;
            DropIndexes.Command += DropIndexes_Command;
            CacheQueries.Command += CacheQueries_Command;
            ClearCache.Command += new CommandEventHandler(ClearCache_Command);
            ProcessDimensions.Command += new CommandEventHandler(ProcessDimensions_Command);
            base.OnInit(e);
        }

        void ProcessDimensions_Command(object sender, CommandEventArgs e)
        {
            DatabaseAdministration admin = adminFactory.DatabaseAdministration();
            ProcessDimensions processor = new ProcessDimensions();
            processor.Execute(admin);
            WriteMessage("Process dimension finished");
        }

        private void WriteMessage(string message)
        {
            trace.TraceInformation(message);
            Results.Text = message;
        }
    

        void ClearCache_Command(object sender, CommandEventArgs e)
        {
            trace.TraceInformation("Starting");
            DatabaseAdministration admin = adminFactory.DatabaseAdministration();
            admin.ClearCache();
            WriteMessage("Clear Cache button has finished");
        }

        void CacheQueries_Command(object sender, CommandEventArgs e)
        {
            WriteMessage("Not implemented Yet");
        }

        void DropIndexes_Command(object sender, CommandEventArgs e)
        {
            trace.TraceInformation("Drop Indexes button was clicked");
            DatabaseAdministration admin = adminFactory.DatabaseAdministration();
            admin.DropIndexes();
            admin.ClearData();
            admin.Compact();
            WriteMessage("Drop indexes finished");
        }

        void CreateSampleData_Command(object sender, CommandEventArgs e)
        {
            trace.TraceInformation("Create Sample Data button was clicked");
            DatabaseAdministration admin = adminFactory.DatabaseAdministration();
            admin.DropIndexes();
            admin.ClearData();
            //Data generation will be faster 2nd time around if we let the db files stay large
            //admin.Compact(); 
            HitSimulator hs = new HitSimulator();
            hs.SimulateRealisticHits(new CacheWrapper(), 60);

            ProcessDimensions processor = new ProcessDimensions();
            processor.Execute(admin);
            admin.ClearCache();
            admin.CreateIndexes();
            //TODO: pre calc common queries
            
            WriteMessage("Dropped indexes, cleared data, generated sample data, processed diminsions, cleared the cache, create indexes");
        }

        void CreateIndexes_Command(object sender, CommandEventArgs e)
        {
            trace.TraceInformation("Create Indexes button was clicked");
            DatabaseAdministration admin = adminFactory.DatabaseAdministration();
            admin.CreateIndexes();
        }
    }
}