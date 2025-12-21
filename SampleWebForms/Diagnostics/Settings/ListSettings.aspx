<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ListSettings.aspx.cs" Inherits="SampleWebForms.Settings.ListSettings" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <asp:DropDownList ID="ApplicationsList" runat="server">
    </asp:DropDownList>
    <br />
    Schema:<br />
    <asp:Repeater runat="server" ID="ListSettingsSchema">
    <HeaderTemplate>
    <table  border="0px">
    </HeaderTemplate>
    <ItemTemplate>
    <tr>
        <td><%# Eval("Name") %></td><td> <%# Eval("DefaultValue")%> </td><td>  <%# Eval("ValueType")%> </td><td>  <%# Eval("Scope")%> </td><td>
        <%# Eval("Culture")%> </td><td> <%# Eval("Version")%> </td><td> <%# Eval("ApplicationName")%> </td><td> <%# Eval("IsUserSetting")%> </td><td> <%# Eval("IsValidForAnonymous")%> </td>
        </tr>
    </ItemTemplate>
    <FooterTemplate>
    </table>
    </FooterTemplate>
    </asp:Repeater>
    <hr />
    Current Values:<br />
    <asp:Repeater runat="server" ID="CurrentValues">
    <HeaderTemplate>
    <table  border="0px">
    </HeaderTemplate>
    <ItemTemplate>
    <tr>
        <td>
    <%# Eval("Name") %> </td><td> <%# Eval("Value")%> </td><td>  <%# Eval("SerializedValue")%> </td><td>  <%# Eval("ValueType")%> </td><td>  <%# Eval("Scope")%> </td><td>  <%# Eval("UserName")%></td>
        </tr>
    </ItemTemplate>
    <FooterTemplate>
    </table>
    </FooterTemplate>
    </asp:Repeater>


    <hr />
    New Version <asp:TextBox ID="VersionBox" runat="server" />
    <asp:Button runat="server" ID="VersionButton" Text="Upgrade To Version" />
    <hr />

</asp:Content>
