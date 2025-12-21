<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="false"
    CodeBehind="Default.aspx.cs" Inherits="SampleWinForms._Default" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <h1>Sample website</h1>
    <p>This is a content page of a monitored website.</p>
    <h1>Hicmah's Dashboard is in the \hicmah\ folder</h1>
    <p><a href="\hicmah\">Take me to the hit counter page!</a> </p>
    
    <h2>Hicmah's Components</h2>
    <p>Dashboard - displays charts and data about feature usage, errors, security problems, click flows, etc.</p>
    <p>Installer- makes best efforts to detect a SQL server and selfinstall hicmah. You can manually configure more options.</p>
    <p>Nuget- allows hicmah to take dependencies on other libraries without burdening developers with the installation details of those libraries</p>
    <p>Handler- Gathers client side hit counter data using javascript.</p>
    <p>Module- Gathers server side hit counter data using ASP.NET.  Modules allow collecting data that pure javascript alternatives like google analytics can't get access to.</p>
    <p>Database- Supports SQL Server and makes best efforts to support MS-Access and other databases with OleDb Providers.</p>
</asp:Content>
