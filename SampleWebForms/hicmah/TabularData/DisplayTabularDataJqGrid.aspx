<%@ Page Title="" Language="C#" MasterPageFile="~/hicmah/Hicmah.Master" AutoEventWireup="true" CodeBehind="DisplayTabularDataJqGrid.aspx.cs" Inherits="Hicmah.TabularData.DisplayTabularDataJqGrid" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<asp:ScriptManagerProxy ID="ScriptManager1" runat="server">
     <Scripts>
         <asp:ScriptReference Path="~/Scripts/jquery.tablesorter.js" ScriptMode="Release"  />
         <asp:ScriptReference Path="~/Scripts/date.js" ScriptMode="Release"  />
     </Scripts>
</asp:ScriptManagerProxy>

<table id="list2"></table>
<div id="pager2"></div>

<script>
jQuery("#list2").jqGrid({
   	url:'server.php?q=2',
	datatype: "json",
   	colNames:['Inv No','Date', 'Client', 'Amount','Tax','Total','Notes'],
   	colModel:[
   		{name:'id',index:'id', width:55},
   		{name:'invdate',index:'invdate', width:90},
   		{name:'name',index:'name asc, invdate', width:100},
   		{name:'amount',index:'amount', width:80, align:"right"},
   		{name:'tax',index:'tax', width:80, align:"right"},		
   		{name:'total',index:'total', width:80,align:"right"},		
   		{name:'note',index:'note', width:150, sortable:false}		
   	],
   	rowNum:10,
   	rowList:[10,20,30],
   	pager: '#pager2',
   	sortname: 'id',
    viewrecords: true,
    sortorder: "desc",
    caption:"JSON Example"
});
jQuery("#list2").jqGrid('navGrid','#pager2',{edit:false,add:false,del:false});
</script>
</asp:Content>
