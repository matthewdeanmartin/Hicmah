<%@ Page Title="" Language="C#" MasterPageFile="~/hicmah/Hicmah.Master" AutoEventWireup="true" CodeBehind="BarChart.aspx.cs" Inherits="Hicmah.JqPlotGraphs.BarChart" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
<%--
<script type="text/javascript" src="../src/plugins/jqplot.dateAxisRenderer.min.js"></script>
<script type="text/javascript" src="../src/plugins/jqplot.canvasTextRenderer.min.js"></script>
<script type="text/javascript" src="../src/plugins/jqplot.canvasAxisTickRenderer.min.js"></script>
<script type="text/javascript" src="../src/plugins/jqplot.categoryAxisRenderer.min.js"></script>
<script type="text/javascript" src="../src/plugins/jqplot.barRenderer.min.js"></script>

<asp:ScriptReference Path="~/Scripts/plugins/jqplot.json2.min.js" ScriptMode="Release"  />
--%>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<asp:ScriptManagerProxy ID="ScriptManager1" runat="server">
     <Scripts>
         <asp:ScriptReference Path="~/Scripts/jqPlot/jquery.jqplot.js" ScriptMode="Release"  />
         <asp:ScriptReference Path="~/Scripts/jqPlot/plugins/jqplot.dateAxisRenderer.min.js" ScriptMode="Release"  />
         <asp:ScriptReference Path="~/Scripts/jqPlot/plugins/jqplot.canvasTextRenderer.min.js" ScriptMode="Release"  />
         <asp:ScriptReference Path="~/Scripts/jqPlot/plugins/jqplot.canvasAxisTickRenderer.min.js" ScriptMode="Release"  />
         <asp:ScriptReference Path="~/Scripts/jqPlot/plugins/jqplot.categoryAxisRenderer.min.js" ScriptMode="Release"  />
         <asp:ScriptReference Path="~/Scripts/jqPlot/plugins/jqplot.barRenderer.min.js" ScriptMode="Release"  />
         <asp:ScriptReference Path="~/Scripts/date.js" ScriptMode="Release"  />
     </Scripts>
</asp:ScriptManagerProxy>

<select id="SelectGraph" >
          <option value="AttackedPages">Error Codes: Attacked Pages</option>
          <option value="SufferingUsers">Error Codes: Suffering Users</option>
          <option value="BuggiestPages">Error Codes: Buggiest Pages</option>
          <option value="AttackingUsers">Error Codes: Attacking Users</option>
  </select>

<div id="chart2" style="height:300px; width:800px;"></div>

<script type="text/javascript">

    $(document).ready(function () {

        var chart = drawGraph();

        $("#SelectGraph").change(function () {
            $('#chart2').empty(); 
            drawGraph();
        });
        
        function drawGraph() {
            // Our ajax data renderer which here retrieves a text file.
            // it could contact any source and pull data, however.
            // The options argument isn't used in this renderer.
            function getData(url, plot, options) {
                var ret = null;
                $.ajax({
                    // have to use synchronous here, else the function 
                    // will return before the data is fetched
                    async: false,
                    url: url,
                    dataType: "json",
                    success: function (data) {
                        ret = data;
                    }
                });
                return ret;
            };

            var start = new Date(2011, 10, 1, 0, 0, 0, 0).toString("M/d/yyyy");
            var end = new Date(2012, 10, 1, 0, 0, 0, 0).toString("M/d/yyyy");
            var dataurl = 'Data.ashx?Chart=' + $("#SelectGraph").val() + '&Style=Jq&Start=' + start + '&End=' + end;
            // The url for our json data
            var jsonurl = dataurl;

            // passing in the url string as the jqPlot data argument is a handy
            // shortcut for our renderer.  You could also have used the
            // "dataRendererOptions" option to pass in the url.
            var plot2 = $.jqplot('chart2', [getData(jsonurl)], {
                title: $("#SelectGraph option:selected").text(),

                series: [{ renderer: $.jqplot.BarRenderer}],
                axesDefaults: {
                    tickRenderer: $.jqplot.CanvasAxisTickRenderer,
                    tickOptions: {
                        angle: -30,
                        fontSize: '10pt'
                    }
                },
                axes: {
                    xaxis: {
                        renderer: $.jqplot.CategoryAxisRenderer
                    }
                }

            });
            return plot2;
        } //draw graph
    });

</script>

 
</asp:Content>
