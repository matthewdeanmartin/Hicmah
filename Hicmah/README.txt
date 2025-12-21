Installation Via Nuget
----------------------
After installing with nuget, your web.config will be modified to add the new handlers and modules.

The module will collect server side information about each web hit. The handler must be invoke by JavaScript
on the client to collect client side information.

To invoke the handlers, you will need to add the following to your master page:

<head runat="server">
    <script type="text/javascript">
        var startTime = (new Date()).getTime();
    </script>
</head>
<body>
    <form runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
     <Scripts>
         <asp:ScriptReference Path="~/Scripts/jquery-1.4.1.js" ScriptMode="Release"  />
         <asp:ScriptReference Path="~/Scripts/jstz.js" ScriptMode="Release"  />
         <asp:ScriptReference Path="~/Scripts/jquery.hicmah.plugin.js" ScriptMode="Release"  />
     </Scripts>

There is more than one way to add the javascript references and the startTime variable, so use your professional judgement.

Installing the Database
-----------------------
Navigate to the Dashboard \Hicmah\Default.aspx  - If no database is detected, you can let Hicmah create a database for you. 
This auto-installation works best with a local SQL Server.

I tried to support file based databases, but performance generally is poor for plausible quantities of hit counter data. Right
now the MS-Access version has the best support among file based databases.

Sample Data
-----------
The application can generate sample data.  This can be very time consuming if there are indexes on the tables.

Using the Dashboard
-------------------
From the dashboard you can view a variety of chart and tabular data.  Scenarios are optimized for the administrator,
maintenance developer, tester, security analyst, and project manager-- in otherwords I hope these charts and graphs 
appear to answer typical questions for the support staff roles for a typical line of business application.

Settings
--------
Hicmah uses database driven settings. 

Activating All Features
-----------------------
* If authentication is done using the standard API's, I can detect your user's information and use that in analytics.
* Client side statistics require a bit of javascript on each page. Otherwise only server side statics are available.

Troubleshooting Hit Counters
----------------------------
Check you error log. If you don't have an error log, add this to your web.config:
<system.diagnostics configSource="web.Hicmah.Errors.config"/>

If that doesn't show the error message, try the app trace or the sql trace.

