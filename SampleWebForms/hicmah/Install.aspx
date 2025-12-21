<%@ Page Title="" Language="C#" MasterPageFile="~/hicmah/Hicmah.Master" AutoEventWireup="true" CodeBehind="Install.aspx.cs" Inherits="Hicmah.Install" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>
        Installation
    </h2>
    
    <p><a href="Installer2.aspx">This page </a>will make best efforts to set up a SQL database for you and it <i>doesn't have to</i> use user instances. It mostly expects that there is a 
    SQL Instance available that you have rights to.</p>

    <p>
 <a href="http://msdn.microsoft.com/en-us/library/46c5ddfy.aspx">General HttpHandler guidance from MS</a>
        Add to your website by adding it to the bin folder and editing the web.config.
    </p>

    <h2>
        IIS6
    </h2>
    <p>
        Module<br />
        Handler<br />
        Connection String<br />
        Custom config section?<br />
        
        TODO: web.config goes here.

    </p>

    <h2>
        IIS7 and IIS Express
    </h2>
    <p>
        TODO: web.config goes here.
    </p>

</asp:Content>
