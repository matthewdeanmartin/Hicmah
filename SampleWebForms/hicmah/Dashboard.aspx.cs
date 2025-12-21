using System;
using System.Collections.Generic;
using System.Configuration;
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

namespace HicmahDash
{
    public partial class Dashboard : System.Web.UI.Page
    {

        

        readonly AdministrationFactory adminFactory = new AdministrationFactory();


        

        protected void Page_Load(object sender, EventArgs e)
        {
            //if(!IsPostBack)
            DatabaseAdministration admin = adminFactory.DatabaseAdministration();
            try
            {

                TableInfo info = admin.TableInfo();
                StringBuilder sb = new StringBuilder();
                sb.Append(HtmlFormatForTableInfo(info));
                sb.Append("<ul>");
                foreach (TableInfo dimension in info.Dimensions)
                {
                    sb.Append("<li>");
                    sb.Append(dimension.TableName);
                    sb.Append(" : ");
                    sb.Append(HtmlFormatForTableInfo(dimension));
                    sb.Append("</li>");
                }
                sb.Append("</ul>");

                foreach (string error in info.ValidateForReports())
                {
                    sb.Append(error + "<br/>");
                }
                SampleDataStats.Text = sb.ToString();

            }
            catch (SqlException ex)
            {
                if (ex.Message.Contains("Invalid object name"))
                {
                    //Nothing created.
                    admin.CreateTables();
                }
                throw;
            }
            //            Response.Write(System.Environment.GetEnvironmentVariable("COMPUTERNAME2"));

            //            //Followiung requires registry permission. Can't seem to grant to self.
            //            Configuration config;
            //            config = WebConfigurationManager.OpenWebConfiguration("~");
            //            config.SaveAs(@"c:\code\Hicmah\SampleWinForms\App_Data\AppData\backup.config");


            //            Diagnostic.Text = "Trust level is: " + GetCurrentTrustLevel();
            //            //ConnectionStringsSection section = (ConnectionStringsSection)ConfigurationManager.GetSection("connectionStrings");
            //            //section.ConnectionStrings.Add(new ConnectionStringSettings("test", "test1"));
            //            //ConfigurationManager.AppSettings.Add("test", "value");
            ////            ConfigurationSection section = (ConfigurationSection)ConfigurationManager.GetSection("connectionStrings");

            //            //Response.Write(section);
        }

        private string HtmlFormatForTableInfo(TableInfo info)
        {
            StringBuilder sb = new StringBuilder();
            if (!(info.Earliest == DateTime.MinValue && info.Latest == DateTime.MinValue))
            {
                sb.Append("Data ranging from <b>");
                sb.Append(info.Earliest);
                sb.Append("</b> to <b>");
                sb.Append(info.Latest);
                sb.Append("</b>");
            }

            sb.Append(" with a size of <b>");
            sb.Append(RedIfZero(info.Rows));
            sb.Append("</b> rows, using <b>");
            sb.Append(info.Data);
            sb.Append("</b> of data space and <b>");
            sb.Append(info.IndexSize);
            sb.Append("</b> of index space ");

            return sb.ToString();
        }

        private string RedIfZero(int rows)
        {
            if (rows == 0)
                return string.Format("<span style=\"color:red;\">{0}</span>", rows);
            else
            {
                return rows.ToString();
            }
        }

        AspNetHostingPermissionLevel GetCurrentTrustLevel()
        {
            foreach (AspNetHostingPermissionLevel trustLevel in
                    new AspNetHostingPermissionLevel[] {
            AspNetHostingPermissionLevel.Unrestricted,
            AspNetHostingPermissionLevel.High,
            AspNetHostingPermissionLevel.Medium,
            AspNetHostingPermissionLevel.Low,
            AspNetHostingPermissionLevel.Minimal 
        })
            {
                try
                {
                    new AspNetHostingPermission(trustLevel).Demand();
                }
                catch (System.Security.SecurityException)
                {
                    continue;
                }

                return trustLevel;
            }

            return AspNetHostingPermissionLevel.None;
        }
    }
}