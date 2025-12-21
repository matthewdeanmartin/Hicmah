<%@ Page Title="" Language="C#" MasterPageFile="~/hicmah/Hicmah.Master" AutoEventWireup="true" CodeBehind="ClickStream.aspx.cs" Inherits="Hicmah.TabularData.ClickStream" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<asp:Repeater ID="Repeater1" runat="server">
    <HeaderTemplate>
    <h2>Click stream for user </h2>
    </HeaderTemplate>

    <ItemTemplate>

    </ItemTemplate>

    <FooterTemplate>

    </FooterTemplate>

</asp:Repeater>

</asp:Content>
