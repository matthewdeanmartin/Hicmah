using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Text;
using System.Threading;
using System.Web;
using Hicmah.Web;
using System.IO;
using Ukadc.Diagnostics;
using HttpRequestWrapper = System.Web.HttpRequestWrapper;

namespace Hicmah
{
    /// <summary>
    /// This is the way to invoke the hit counter by calling a URL from javascript (xmlhttp or the like) or html (image).  
    /// </summary>
    /// <remarks>
    /// Unless you do browser or javascript magic, calling ULR will (may?) make the browser think it must wait for a response.
    /// 
    /// The information on the request is recorded. The browser invokes this and it would be invoked once per request
    /// not once per file (i.e. css file, jpg hits wouldn't be recorded this way)
    /// 
    /// Also, if this isn't called by the HTML or JS code snippet, then the hit will not be recorded.
    /// </remarks>
   public class HitHandler : IHttpHandler
   {
       private const string CONSTSOMEPARAM = "SomeParam";

       public HitHandler()
       {

       }
   
       public void ProcessRequest(HttpContext context)
       {
           ExtendedSource trace = new ExtendedSource("Web.HitHander");

           if (context == null)
               throw new ArgumentNullException();

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
   

           //if (context.User.Identity.IsAuthenticated == false)
           //{
           //    //Forbidden
           //    context.Response.StatusCode = 403;
           //    context.Response.End();
           //    return;
           //}

   
           string someParam = context.Request.QueryString[CONSTSOMEPARAM];

           Hit currentHit = HitFactory.Bind(new HttpRequestWrapper(context.Request));
           

           //Ok, I'm stumped. I can't think of a meaningful way to get server duration (server time req to create/load page).
           //Maybe if all pages added a server duration as a query string and the JS invoker then copied that to the ashx invokcation string?

           GenericHitProcessor processor = new GenericHitProcessor();
           trace.TraceInformation("Recording hit via handler for " + context.Request.Url.PathAndQuery);
           processor.RecordHitToCurrentProvider(currentHit);

           //SomethingReponse response = SomeService.SomeImportantThing(someParam);
           //if(response.Header == null || response.Header.Success == false)
           //{
           //    //Whatever wasn't found
           //    context.Response.StatusCode = 404;
           //    context.Response.End();
           //    return;
           //}

//#if DEBUG
//           var pageBuilder = new PageBuilder();
//           using (var writer = new StringWriter())
//           {
//               // then we supply the page builder with the model
//               pageBuilder.Render(currentHit, writer);

//               context.Response.Write(writer.ToString());
//               context.Response.ContentType = "text/html";
//           }
//#endif
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
