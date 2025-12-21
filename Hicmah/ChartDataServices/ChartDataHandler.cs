using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using Hicmah.ChartData;


namespace Hicmah.ChartDataServices
{
    
       [Serializable]
    public class GraphData
    {
        public string label { get; set; }
        public double[][] data { get; set; }
    }



    public class ChartDataHandler: IHttpHandler
   {
       

       public ChartDataHandler()
       {

       }
   
       public void ProcessRequest(HttpContext context)
       {
           // Don't allow this response to be cached by the browser.
           // Note, you MIGHT want to allow it to be cached, depending on what you're doing.
           context.Response.Cache.SetCacheability(HttpCacheability.NoCache);
           context.Response.Cache.SetNoStore();
           context.Response.Cache.SetExpires(DateTime.MinValue);
   
           if (ValidateParameters(context) == false)
           {   
               //Internal Server Error
               context.Response.StatusCode = 500;
               context.Response.End();
               return;
           }
   
#if RELEASE
           if (context.User.Identity.IsAuthenticated == false)
           {
               //Forbidden
               context.Response.StatusCode = 403;
               context.Response.End();
               return;
           }
#endif

           string chartType = "";
           if(context.Request.QueryString["Chart"]!=null)
               chartType = context.Request.QueryString["Chart"];

           string style = "Flot";
           if (context.Request.QueryString["Style"] != null)
               style = context.Request.QueryString["Style"];

           switch (chartType)
           {
               case "AttackedPages":
                   context.Response.Write(ChartAttackedPagesFormatter.GetJson(style, context.Request.QueryString));
                   context.Response.ContentType = "application/json";
                   break;
               case "BuggiestPages":
                   context.Response.Write(ChartBuggiestPagesFormatter.GetJson(style, context.Request.QueryString));
                   context.Response.ContentType = "application/json";
                   break;
               case "AttackingUsers":
                   context.Response.Write(ChartAttackingUsersFormatter.GetJson(style, context.Request.QueryString));
                   context.Response.ContentType = "application/json";
                   break;
               case "SufferingUsers":
                   context.Response.Write(ChartSufferingUsersFormatter.GetJson(style, context.Request.QueryString));
                   context.Response.ContentType = "application/json";
                   break;
               case "HitsPerTimespan":
                   context.Response.Write(ChartHitsPerTimeSpanFormatter.GetJson(style, context.Request.QueryString));
                   context.Response.ContentType = "application/json";
                   break;
               default:
                   throw new HttpException(400,"Invalid Chart Parameter");
           }
       }

        public bool ValidateParameters(HttpContext context)
       {
           //Validate some stuff...true if cool, false if not
           return true;
       }
   
       /// <summary>
       /// True if this handler can be reused between calls. That's cool if you don't have any class instance data.
       /// False if you'd rather get a fresh one.
       /// </summary>
       public bool IsReusable
       {
           get
           {
               return true;
           }
       }
    }
}
