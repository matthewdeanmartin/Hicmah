<%@ Page Title="" Language="C#" MasterPageFile="~/hicmah/Hicmah.Master" AutoEventWireup="true" CodeBehind="XyChartTestHarness.aspx.cs" Inherits="SampleWinForms.Graphs.HitsOverTime" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">

 <!--[if lte IE 8]><script language="javascript" type="text/javascript" src="../Scripts/excanvas.min.js"></script><![endif]-->


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<asp:ScriptManagerProxy ID="ScriptManager1" runat="server">
     <Scripts>
         <asp:ScriptReference Path="~/Scripts/flot/jquery.flot.js" ScriptMode="Release"  />
     </Scripts>
</asp:ScriptManagerProxy>

<div id="placeholder" style="width:600px;height:300px;"></div>
<br/>
<span id="dataResults" style="width:600px;height:300px;"></span>
<script type="text/javascript">
    jQuery(document).ready(function ($) {


        $(function () {
            var options = {
                lines: { show: true },
                points: { show: true },
                xaxis: { tickDecimals: 0, tickSize: 1 }
            };
            var data = [];
            var placeholder = $("#placeholder");

            $.plot(placeholder, data, options);

            // fetch one series, adding to what we got
            var alreadyFetched = {};

            // find the URL in the link right next to us 
            //var dataurl = 'data-eu-gdp-growth.json';
            var dataurl = 'Data.ashx';
            // then fetch the data with jQuery
            function onDataReceived(series) {
                // extract the first coordinate pair so you can see that
                // data is now an ordinary Javascript object
                var firstcoordinate = '(' + series.data[0][0] + ', ' + series.data[0][1] + ')';

                var dataResults = $("#dataResults");
                dataResults.text('Fetched ' + series.label + ', first point: ' + firstcoordinate);

                // let's add it to our current data
                if (!alreadyFetched[series.label]) {
                    alreadyFetched[series.label] = true;
                    data.push(series);
                }

                // and plot all we got
                $.plot(placeholder, data, options);
            }

            $.ajax({
                url: dataurl,
                method: 'GET',
                dataType: 'json',
                success: onDataReceived
            });
            //});

        });

    });
</script>
</asp:Content>
