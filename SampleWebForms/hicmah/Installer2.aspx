<%@ Page Title="" Language="C#" MasterPageFile="~/hicmah/Hicmah.Master" AutoEventWireup="false" CodeBehind="Install.aspx.cs" Inherits="HicmahDash.SelfInstall" %>
<%@ Import Namespace="System.Data.SqlClient" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<%
if(ConfigurationManager.ConnectionStrings["HicmahDb"]!=null)
{
    ConString.Text = ConfigurationManager.ConnectionStrings["HicmahDb"].ConnectionString;
    GuessItem.Visible = false;
}
else
{
    if(ConfigurationManager.ConnectionStrings["HicmahDb"].ProviderName=="System.Data.SqlClient")
    {
        try
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["HicmahDb"].ConnectionString))
            {
                con.Open();
                con.Close();
            }
        }
        catch
        {
            
        }
    }

}
%>

Namespace: <asp:TextBox runat="server" TextMode="SingleLine" ID="PreferredNamespace" text="hicmah" /> *nb, if you pick something other than hicmah, you need to set it up in appConfig, eg. add key="Dataglot.Namespace" value="foo" <br />
<!-- Preferred Db Name: <asp:TextBox runat="server" TextMode="SingleLine" ID="PreferredDbName" text="hicmah" /><br /> -->

Current Connection String:
<asp:Label runat="server" ID="ConString" /><br />

<ul>
<li runat="server" id="GuessItem">
<asp:Button runat="server" ID="GuessButton" Text="Guess Connection String" /> 
</li>
<li runat="server" id="DbItem">
<asp:Button runat="server" ID="CreateDatabaseButton" Text="Create the Database" /> This button will try to create a database, preferably named hicmah
</li>
<li>
<asp:Button runat="server" ID="InstallButton" Text="Install" /> Create tables using the "HicmahDb" connection string.
</li>
<li>
<asp:Button runat="server" ID="InstallJustSettings" Text="Install" /> Create just the settings tables using the "HicmahDb" connection string.
</li>

</ul>

<pre>
<asp:Label runat="server" ID="Results" />
</pre>

</asp:Content>
