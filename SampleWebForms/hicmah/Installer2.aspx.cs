using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IrishSettings.Installation;
using Hicmah;
using Hicmah.Administration;


namespace HicmahDash
{
    public partial class SelfInstall : System.Web.UI.Page
    {
        protected override void OnInit(EventArgs e)
        {
            InstallButton.Click += new EventHandler(InstallButton_Click);
            GuessButton.Click += new EventHandler(Guess_Click);
            CreateDatabaseButton.Click += new EventHandler(CreateDatabaseButton_Click);
            InstallJustSettings.Click += new EventHandler(InstallJustSettings_Click);
            base.OnInit(e);
        }

        void InstallJustSettings_Click(object sender, EventArgs e)
        {
            InstallIrishSettings.CreateAllTables("HicmahDb", PreferredNamespace.Text);
        }

        void CreateDatabaseButton_Click(object sender, EventArgs e)
        {
            ConfigUtils.CreateDbWithAutomaticString();

            Results.Text = HttpUtility.HtmlEncode(string.Format(@"
    <connectionStrings>
        <add name=""HicmahDb"" connectionString=""Data Source={0};"" providerName=""System.Data.SqlClient""/>
    </connectionStrings>", "hicmah"));

        }

        void Guess_Click(object sender, EventArgs e)
        {
            string result = ConfigUtils.AutomaticConnectionString();
              
            Results.Text = HttpUtility.HtmlEncode(string.Format(@"
    <connectionStrings>
        <add name=""HicmahDb"" connectionString=""Data Source={0};"" providerName=""System.Data.SqlClient""/>
    </connectionStrings>", result));
        }

        void InstallButton_Click(object sender, EventArgs e)
        {
            
            AdministrationFactory factory = new AdministrationFactory();
            DatabaseAdministration admin= factory.DatabaseAdministration("mssql");
            admin.CreateTables();
            InstallIrishSettings.CreateAllTables("HicmahDb", PreferredNamespace.Text);

            
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                DbConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["HicmahDb"].ConnectionString);
                con.Open();
            }
            catch (SqlException exception)
            {
                //System.Data.SqlClient.SqlException: Cannot open database "hicmah" requested by the login. 
                //The login failed. Login failed for user '{domain}\{user}'.
                if (exception.Message.Contains("Cannot open database"))
                {
                    this.InstallButton.Visible = true;

                    return;
                }
                throw;
            }
            InstallButton.Visible = false;
        }
    }
}