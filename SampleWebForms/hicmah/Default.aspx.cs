using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Hicmah
{
    public partial class Default : TracedPage
    {
        private void TestSqlConnection()
        {
            //TODO: Test all connection strings.
            //foreach (ConnectionStringSettings c in System.Configuration.ConfigurationManager.ConnectionStrings)
            {
            }

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

                    return;
                }
                throw;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
           

            Response.Redirect("Dashboard.aspx");

            
        }
    }
}