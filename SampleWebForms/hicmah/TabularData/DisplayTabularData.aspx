<%@ Page Title="" Language="C#" MasterPageFile="~/hicmah/Hicmah.Master" AutoEventWireup="false" CodeBehind="DisplayTabularData.aspx.cs" Inherits="Hicmah.TabularData.DisplayTabularData" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<asp:ScriptManagerProxy ID="ScriptManager1" runat="server">
     <Scripts>
     <%--<asp:ScriptReference Path="~/Scripts/jquery-latest.js" ScriptMode="Release"  />--%>
         <asp:ScriptReference Path="~/Scripts/jquery.tablesorter.js" ScriptMode="Release"  />
         
         <asp:ScriptReference Path="~/Scripts/date.js" ScriptMode="Release"  />
     </Scripts>
</asp:ScriptManagerProxy>

<asp:DropDownList runat="server" ID="ddlData" AutoPostBack="true">
<asp:ListItem value="BrokenLinks" selected="true">Dead Pages</asp:ListItem>
<asp:ListItem value="RecentHits" >Recent Hits</asp:ListItem>
<asp:ListItem value="PageStats" >Statistics by Url</asp:ListItem>
<asp:ListItem value="UserAgentStats" >Statistics by UserAgent</asp:ListItem>
<asp:ListItem value="UserStats" >Statistics by User</asp:ListItem>
<asp:ListItem value="RequestTypeStats" >Statistics by Request Type</asp:ListItem>
<asp:ListItem value="ReferralsStats" >Statistics by ReferralsStats</asp:ListItem>

</asp:DropDownList>

<!-- Must have thead and tbody -->

<asp:GridView ID="gv" runat="server"  UseAccessibleHeader="true"  CssClass="tablesorter HicmahMainContentGrid" />

<%--<table id="someTable" > 
<thead> 
<tr> 
    <th>Last Name</th> 
    <th>First Name</th> 
    <th>Email</th> 
    <th>Due</th> 
    <th>Web Site</th> 
</tr> 
</thead> 
<tbody> 
<tr> 
    <td>Smith</td> 
    <td>John</td> 
    <td>jsmith@gmail.com</td> 
    <td>$50.00</td> 
    <td>http://www.jsmith.com</td> 
</tr> 
<tr> 
    <td>Bach</td> 
    <td>Frank</td> 
    <td>fbach@yahoo.com</td> 
    <td>$50.00</td> 
    <td>http://www.frank.com</td> 
</tr> 
<tr> 
    <td>Doe</td> 
    <td>Jason</td> 
    <td>jdoe@hotmail.com</td> 
    <td>$100.00</td> 
    <td>http://www.jdoe.com</td> 
</tr> 
<tr> 
    <td>Conway</td> 
    <td>Tim</td> 
    <td>tconway@earthlink.net</td> 
    <td>$50.00</td> 
    <td>http://www.timconway.com</td> 
</tr> 
</tbody> 
</table> 
--%>
<script type="text/javascript">
    $(document).ready(function () {

        $(".HicmahMainContentGrid").tablesorter({ sortList: [[0, 0], [2, 1]], widgets: ['zebra'] });
        //$("table .myTable:first").tablesorter();

        //$("#someTable").css("color:red");
        //$("myTable").tablesorter({ sortList: [[1, 1], [2, 1]], widgets: ['zebra'] });
        //$("#someTable").tablesorter();
    }
); 
</script>    
</asp:Content>
