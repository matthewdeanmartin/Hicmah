<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="EditApplictionSettings.aspx.cs" Inherits="SampleWebForms.Settings.EditApplictionSettings" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<asp:ObjectDataSource runat="server" ID="SettingsDataSource" 
        DataObjectTypeName="IrishSettings.GenericSettingStored" DeleteMethod="Delete" 
        InsertMethod="Insert" SelectMethod="SelectList" 
        TypeName="IrishSettings.SettingsForApplicationController" UpdateMethod="Update" >
    <DeleteParameters>
        <asp:Parameter Name="id" Type="Int64" />
    </DeleteParameters>
    <SelectParameters>
        <asp:Parameter DefaultValue="0" Name="from" Type="Int32" />
        <asp:Parameter DefaultValue="0" Name="to" Type="Int32" />
    </SelectParameters>
    </asp:ObjectDataSource>
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
        DataSourceID="SettingsDataSource" EnableModelValidation="True">
        <Columns>
            <asp:BoundField DataField="Id" HeaderText="Id" SortExpression="Id" />
            <asp:BoundField DataField="Name" HeaderText="Name" SortExpression="Name" />
            <asp:BoundField DataField="SerializedValue" HeaderText="SerializedValue" 
                SortExpression="SerializedValue" />
            <asp:BoundField DataField="ValueType" HeaderText="ValueType" 
                SortExpression="ValueType" />
            <asp:BoundField DataField="Application" HeaderText="Application" 
                SortExpression="Application" />
            <asp:BoundField DataField="Version" HeaderText="Version" 
                SortExpression="Version" />
            <asp:BoundField DataField="Culture" HeaderText="Culture" 
                SortExpression="Culture" />
        </Columns>
    </asp:GridView>
</asp:Content>
