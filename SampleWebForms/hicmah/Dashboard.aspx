<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Dashboard.aspx.cs" Inherits="HicmahDash.Dashboard" 
MasterPageFile="~/hicmah/Hicmah.master" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <h2>
        Graphs
    </h2>
    <ul>
    <li> <a href="FlotGraphs/BarChartTestHarness.aspx">Flot Bar Chart</a>
    <li> <a href="FlotGraphs/TimeSeriesTestHarness.aspx">Flot Time Series Chart</a>

    
    </li>
    <li> <a href="FlotGraphs/XyChartTestHarness.aspx">Flot Line (x-y) Chart</a>
    </li>

    <li> <a href="JqPlotGraphs/BarChart.aspx">Sample JqPlot Bar Chart</a>
    </li>
    </ul>
    <h2>Tabular Data</h2>
    <ul>
    <li>
    <a href="TabularData/DisplayTabularData.aspx">Tabular Data</a>
    </li>
    </ul>


    <h2>Current Data Volume</h2>
    <p><asp:Label runat="server" ID="SampleDataStats" /></p>
    
    <p>Not seeing any data? Either wait or go <a href="Installer3.aspx"> generate some sample data.</a></p>

    
    <p><asp:Label runat="server" id="Diagnostic"/></p>
</asp:Content>
