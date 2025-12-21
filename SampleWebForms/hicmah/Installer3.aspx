<%@ Page Title="" Language="C#" MasterPageFile="~/hicmah/Hicmah.Master" AutoEventWireup="false" CodeBehind="Installer3.aspx.cs" Inherits="SampleWebForms.hicmah.Installer3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h3>Optional: Create Some Sample Data</h3>
    <p>Use the buttons belwo to generate sample data. In general, creating data is fastest when there are no indexes.
    But queries are unbearably slow without indexes.</p>
    <ul>
    <li>(1-2 minutes) Performs most actions necessary to try out the application To improve perf, increase buffer size and turn off any trace<br/>
        <asp:Button runat="server" ID="CreateSampleData" Text="Create Sample Data" /> 
    </li>
    <li><asp:Button runat="server" ID="CreateIndexes" Text="Create Indexes" />For recreating indexes after initial loading.</li>
    
    <li><asp:Button runat="server" ID="ProcessDimensions" Text="Process Dimensions" /> Need to run periodically to pick up new servers, users, etc</li>
    <li><asp:Button runat="server" ID="CacheQueries" Text="Cache Queries for Default Parameters" />(Not implemented yet)</li>
    <li><asp:Button runat="server" ID="ClearCache" Text="Clear all queries from Cache" />Slow queries are cached and will not reflect new hits until the cache is cleared/expired</li>
    <li><asp:Button runat="server" ID="DropIndexes" Text="Drop data, Index and Compact" /></li>
    </ul>
    
    <asp:Label runat="server" ID="Results"></asp:Label>

</asp:Content>
