<%@ Page Title="About Us" Language="C#" MasterPageFile="~/hicmah/Hicmah.master" AutoEventWireup="true"
    CodeBehind="About.aspx.cs" Inherits="Hicmah.About" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <h2>
        About
    </h2>
    <p>
        This is the test website for the Hicmah hit counter.
    </p>
    <p>Hicmah is optimized for use in an intranet application. If you have an internet application, use google analytics. 
    Even if you have an intranet application, you still might want to see if google analytics is good enough.
    Hicmah focuses on questions more relevant to intranet applications.</p>

        <p>
        This application is inspired by the successful open source project Elmah. Instead of tracking errors, 
        this is a drop in component for tracking website usage that is suitable for usage when Google 
        Analytics is not an option. The counter data gathering is oriented towards the typical scenarios of
        an intranet application.
    </p>
    <h2>Sample Data, Indexing and Pre-calculating Queries</h2>
    <p>A few years of hits can be a lot of data. The dashboard queries will typically read all or most rows, so queries on unindexed tables are slow.  
    To ensure that recording hits is extremely fast, some calculations are deferred, such as parsing the URLs.
    You can get acceptable performance by creating indexes, which takes time and a significant amount of drive space.</p>

    <h2>Core Dependencies</h2>
    <ul>
    <li>Nuget : <a href="http://code.google.com/p/protobuf-net">Protobuf-net, MIT license</a></li>
    <li>Nuget: <a href="http://json.codeplex.com/">Newtonsoft.Json</a>- <a href="http://json.codeplex.com/license">MIT License</a></li>
    <li>Nuget: <a href="http://essentialdiagnostics.codeplex.com/">EssentialDiagnostics</a> </li>
    <li>Nuget: <a href="http://www.jqplot.com/">jqplot</a> </li>

    <li>Javascript: Flot graphs- MIT license</li>
    <li>Javascript: <a href="http://www.coolite.com"> date.js- Coolite Inc.</a> - MIT</li>
     <li>Javascript: <a href="https://bitbucket.org/pellepim/jstimezonedetect">jstz.js</a>* Provided under the Do Whatever You Want With This Code License.</li>
    <li>Javascript: <a href="http://tablesorter.com">Table Sorter </a>jquery plugin</li>
    
    
    <li>Codeplex: <a href="http://ukadcdiagnostics.codeplex.com/">Ukadc Diagonstics</a> </li>
    <li>Codeplex:<a href="http://sharpdom.codeplex.com/"> SharpDom- MS-PL</a></li>
    <li>Other: SQLite + the ADO.NET drivers</li>
    <li>Other: <a href="http://msdn.microsoft.com/en-us/library/aa478948.aspx">ASP.NET Provider Toolkit SQL Samples</a>- MS-PL license </li>
    </ul>
    
    
    <h2>Non-Core Dependnecies, Demo code, etc</h2>
    <ul>
    <li>Nuget: Elmah- Apache v2.0 license</li>
    <li>Nuget: Nunit</li>
    </ul>

    <h2>Tools and Other Things Used</h2>
    <ul>
    <li><a href="http://www.flickr.com/photos/wheatfields/4313193969/sizes/m/in/photostream/">Source image for favicon</a></li>
    <li><a href="http://www.favicon.cc/">Favicon maker</a> </li>
    <li><a href="http://json2csharp.com/">Json To C#</a></li>
    </ul>
</asp:Content>
