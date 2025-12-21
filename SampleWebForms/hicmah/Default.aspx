<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Hicmah.Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <h1>Invalid Connection String</h1>
    <p>It appears that a database hasn't been set up. Would you like me to set one up for you?</p>
    
    <p><a href="Installer2.aspx">Yes</a></p>
    
    <h2>No</h2>
    <p>You will need to create a database and user that has enough rights to create tables.
     If you can't grant the rights to create tables, then you will also need to run the table creation scripts.  You will need to update
     all the appropriate connection strings in web.ConnectionStrings.config, one are required for high performance hit logging,
     one for asynch features, some for cross database support. Of those, only the cross database connection string is required.</p>
    
    </div>
    </form>
</body>
</html>
