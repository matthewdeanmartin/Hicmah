using System;
using System.Data.Common;
using System.Web.UI.WebControls;
using Hicmah.ChartData;

namespace Hicmah.TabularData
{
    public partial class DisplayTabularData : System.Web.UI.Page
    {

        protected override void OnInit(EventArgs e)
        {
            ddlData.SelectedIndexChanged += new EventHandler(ddlData_SelectedIndexChanged);
            Load += Page_Load;
            base.OnInit(e);
        }

        void ddlData_SelectedIndexChanged(object sender, EventArgs e)
        {
            Bind(ddlData.SelectedValue);       
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            
            if(!IsPostBack)
            {
                Bind(ddlData.SelectedValue);       
            }
        }

        protected void Bind(string query)
        {
            DateTime start = new DateTime(2011, 11, 1);
            DateTime end= new DateTime(2012, 11, 1);
            int maxRows = 500;
            switch (query)
            {
                case "BrokenLinks":
                    using (ChartBrokenLinks command = new ChartBrokenLinks())
                    {
                        using (DbDataReader reader = command.RetrieveDataReader(start, end))
                        {
                            gv.DataSource = reader;
                            gv.DataBind();
                            if(gv.HeaderRow!=null)
                            {
                                gv.HeaderRow.TableSection = TableRowSection.TableHeader;
                            }
                        }
                    }
                    break;
                case "AttackedPages":
                    break;
                case "AttackingUsers":
                    break;
                case "RecentHits":
                    using (RecentHitsTable command = new RecentHitsTable())
                    {
                        using (DbDataReader reader = command.RetrieveDataReader(start, end))
                        {
                            gv.DataSource = reader;
                            gv.DataBind();
                            gv.HeaderRow.TableSection = TableRowSection.TableHeader;
                        }
                    }
                    break;
                case "PageStats":
                    using (ChartTop command = new ChartTop())
                    {
                        string[] validMeasures = {
                                         "Hits", 
                                         "TotalClientTime", 
                                         "AvgClientTime", 
                                         "StDevClientTime", 
                                         "MinClientTime", 
                                         "MaxClientTime", 
                                         "TotalServerTime", 
                                         "AvgServerTime", 
                                         "StDevServerTime", 
                                         "MinServerTime", 
                                         "MaxServerTime"
                                     };
                        using (DbDataReader reader = command.RetrieveDataReader(maxRows,UsageRate.Top, start, end,"full_url",validMeasures))
                        {
                            gv.DataSource = reader;
                            gv.DataBind();
                            gv.HeaderRow.TableSection = TableRowSection.TableHeader;
                        }
                    }
                    break;
                case "UserStats":
                    using (ChartTop command = new ChartTop())
                    {
                        string[] validMeasures = {
                                         "Hits", 
                                         "TotalClientTime", 
                                         "AvgClientTime", 
                                         "StDevClientTime", 
                                         "MinClientTime", 
                                         "MaxClientTime", 
                                         "TotalServerTime", 
                                         "AvgServerTime", 
                                         "StDevServerTime", 
                                         "MinServerTime", 
                                         "MaxServerTime"
                                     };
                        using (DbDataReader reader = command.RetrieveDataReader(maxRows,UsageRate.Top, start, end,"user",validMeasures))
                        {
                            gv.DataSource = reader;
                            gv.DataBind();
                            gv.HeaderRow.TableSection = TableRowSection.TableHeader;
                        }
                    }
                    break;
                case "UserAgentStats":
                    using (ChartTop command = new ChartTop())
                    {
                        string[] validMeasures = {
                                         "Hits", 
                                         "TotalClientTime", 
                                         "AvgClientTime", 
                                         "StDevClientTime", 
                                         "MinClientTime", 
                                         "MaxClientTime", 
                                         "TotalServerTime", 
                                         "AvgServerTime", 
                                         "StDevServerTime", 
                                         "MinServerTime", 
                                         "MaxServerTime"
                                     };
                        using (DbDataReader reader = command.RetrieveDataReader(maxRows, UsageRate.Top, start, end, "user_agent", validMeasures))
                        {
                            gv.DataSource = reader;
                            gv.DataBind();
                            gv.HeaderRow.TableSection = TableRowSection.TableHeader;
                        }
                    }
                    break;
                    case "RequestTypeStats":
                    using (ChartTop command = new ChartTop())
                    {
                        string[] validMeasures = {
                                         "Hits", 
                                         "TotalClientTime", 
                                         "AvgClientTime", 
                                         "StDevClientTime", 
                                         "MinClientTime", 
                                         "MaxClientTime", 
                                         "TotalServerTime", 
                                         "AvgServerTime", 
                                         "StDevServerTime", 
                                         "MinServerTime", 
                                         "MaxServerTime"
                                     };
                        using (DbDataReader reader = command.RetrieveDataReader(maxRows,UsageRate.Top, start, end,"request_type",validMeasures))
                        {
                            gv.DataSource = reader;
                            gv.DataBind();
                            gv.HeaderRow.TableSection = TableRowSection.TableHeader;
                        }
                    }
                    break;
                    case "ReferralsStats":
                    using (ChartTop command = new ChartTop())
                    {
                        string[] validMeasures = {"Hits"};
                        using (DbDataReader reader = command.RetrieveDataReader(maxRows,UsageRate.Top, start, end,"referrer_url",validMeasures))
                        {
                            gv.DataSource = reader;
                            gv.DataBind();
                            gv.HeaderRow.TableSection = TableRowSection.TableHeader;
                        }
                    }
                    break;
                default:
                    throw new InvalidOperationException("Don't recognize: " + query);
            }
            
                
        }
    }
}