<%@ Page Title="" Language="C#" MasterPageFile="~/hicmah/Hicmah.Master" AutoEventWireup="true" CodeBehind="TimeSeriesTestHarness.aspx.cs" Inherits="SampleWinForms.Graphs.TimeSereisTestHarness" %>
<%@ Import Namespace="Hicmah.Administration" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<asp:ScriptManagerProxy ID="ScriptManager1" runat="server">
     <Scripts>
         <asp:ScriptReference Path="~/Scripts/flot/jquery.flot.js" ScriptMode="Release"  />
         <asp:ScriptReference Path="~/Scripts/date.js" ScriptMode="Release"  />
     </Scripts>
</asp:ScriptManagerProxy>
<select id="SelectGraph" >
          <option value="HitsPerTimespan">HitsPerTimespan</option>
          <option value="HitsPerTimespan">HitsPerTimespan</option>
  </select>

<div id="placeholder" style="width:800px;height:300px;"></div>
<br/>
<span id="dataResults" style="width:600px;height:300px;"></span>
<script type="text/javascript">

    jQuery(document).ready(function ($) {

        $("#SelectGraph").change(function () { drawGraph(); });

        drawGraph();

        <%
        AdministrationFactory adminFactory = new AdministrationFactory();
        DatabaseAdministration admin = adminFactory.DatabaseAdministration();
        TableInfo info = admin.TableInfo();  
         %>

        function drawGraph() {

            var startUnformatted = new Date(<%= info.Earliest.Year %>, <%= info.Earliest.Month %>, 1, 0, 0, 0, 0);
            var start = startUnformatted.toString("M/d/yyyy");
<%
    string endYear;
    string endMonth;
                if((info.Latest-info.Earliest).TotalDays>365 )
                {
                    endYear = info.Earliest.AddYears(1).Year.ToString();
                    endMonth = info.Earliest.AddYears(1).Month.ToString();
                }
                else
                {
                    endYear = info.Latest.AddMonths(1).Year.ToString();
                    endMonth = info.Latest.AddMonths(1).Month.ToString();
                }

%>

            var endUnformatted = new Date(<%= endYear %>, <%= endMonth %>, 1, 0, 0, 0, 0);
            var end = endUnformatted.toString("M/d/yyyy");

            var options = {
                lines: { show: true },
                //points: { show: true }
                bars: { show: false },
                xaxis: {
                    mode: "time",
                    min: (startUnformatted).getTime(),
                    max: (endUnformatted).getTime()
                }


                //,xaxis: { tickDecimals: 0, tickSize: 1 }
            };




            var data = [];
            var placeholder = $("#placeholder");

            $.plot(placeholder, data, options);

            // fetch one series, adding to what we got
            var alreadyFetched = {};

            // find the URL in the link right next to us 
            //var dataurl = 'data-eu-gdp-growth.json';

            var dataurl = 'Data.ashx?Chart=' + $("#SelectGraph").val() + '&Style=Flot&Start=' + start + '&End=' + end;
            // then fetch the data with jQuery
            function onDataReceived(series) {

                var dataResults = $("#dataResults");
                if (series.data[0] == null ||
                    series.data[0][0] == null ||
                    series.data.Length < 1) {
                    dataResults.text('No data');
                    return;
                }

                // extract the first coordinate pair so you can see that
                // data is now an ordinary Javascript object
                var firstcoordinate = '(' + series.data[0][0] + ', ' + series.data[0][1] + ')';
                dataResults.text('Fetched ' + series.label + ', first point: ' + firstcoordinate + ' starting on ' + start + ' ending on ' + end);

                // let's add it to our current data
                if (!alreadyFetched[series.label]) {
                    alreadyFetched[series.label] = true;
                    data.push(series);
                }

                //                var ticks = [];
                //extract labels
                //                for (var i = 0; i < series.data.length; i++) {
                //                    var point = [];
                //                    point[0] = i;
                //                    point[1] = series.data[i][0];
                //                    ticks[i] = point;
                //                }

                //                //overwrite labels.
                //                for (var i = 0; i < series.data.length; i++) {
                //                    series.data[i][0] = i;
                //                }

                //               options.xaxis.ticks = ticks;

                // and plot all we got
                $.plot(placeholder, data, options);
            }

            $.ajax({
                url: dataurl,
                method: 'GET',
                dataType: 'json',
                success: onDataReceived
            });

        }
    });
</script>
</asp:Content>
