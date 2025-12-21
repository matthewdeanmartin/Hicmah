using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Hicmah.Web
{
    /// <summary>
    /// Creates hit objects
    /// </summary>
    /// <remarks> see http://en.wikipedia.org/wiki/The_Hit_Factory </remarks>
    public class HitFactory
    {
        /// <summary>
        /// Create a hit with current date and the UTC date.
        /// </summary>
        /// <returns></returns>
        /// <remarks>
        /// Otherwise you will have to deal with how to invoke now twice and cope with time zones.
        /// </remarks>
        public static Hit BasicHitNow()
        {
            Hit currentHit = new Hit();
            DateTimeOffset utcNow = DateTimeOffset.UtcNow;
            //Server time in server time zone
            currentHit.HitDate = utcNow.ToLocalTime().DateTime; 
            //Server time, Independent of time zone
            currentHit.UtcTime = utcNow;
            return currentHit;
        }

        public static void AddDefaultUser(Hit hit)
        {
            if (!string.IsNullOrEmpty(Thread.CurrentPrincipal.Identity.Name))
            {
                hit.User = Thread.CurrentPrincipal.Identity.Name;
            }
            else
            {
                System.Security.Principal.WindowsIdentity currentUser = System.Security.Principal.WindowsIdentity.GetCurrent();
                if(currentUser!=null)
                {
                    hit.User = currentUser.Name;
                }
            }
        }

        public static Hit Bind(System.Web.HttpRequestWrapper request)
        {
            Hit hit = BasicHitNow();

            //Dimensions
            hit.Invoker = Invoker.HttpHandler;

            if (request.Url != null)
            {
                hit.Url = request.Url.ToString();// request.RawUrl; //RawUrl is sometimes just the relative part.
            }

            if (request.UrlReferrer != null)
                hit.ReferrerUrl = request.UrlReferrer.ToString();

            hit.UserAgent = request.UserAgent;

            //TODO: ASP.NET users can have 2 - service name & authenticated name
            if (request.LogonUserIdentity != null)
                hit.User = request.LogonUserIdentity.Name;
            else
            {
                hit.User = Thread.CurrentPrincipal.Identity.Name;
            }

            switch (request.RequestType)
            {
                    //Most common
                case "GET":
                    hit.RequestType = RequestType.Get;
                    break;
                case "POST":
                    hit.RequestType = RequestType.Post;
                    break;
                case "PUT":
                    hit.RequestType = RequestType.Put;
                    break;
                case "DELETE":
                    hit.RequestType = RequestType.Delete;
                    break;
                    //Least common
                case "HEAD":
                    hit.RequestType = RequestType.Head;
                    break;
                case "TRACE":
                    hit.RequestType = RequestType.Trace;
                    break;
                case "OPTIONS":
                    hit.RequestType = RequestType.Options;
                    break;
                case "CONNECT":
                    hit.RequestType = RequestType.Connect;
                    break;
                case "PATH":
                    hit.RequestType = RequestType.Path;
                    break;
                default:
                    hit.RequestType = RequestType.None;
                    break;
            }

            //Measures
            //hit.ServerDuration Can't be inferred from request
            hit.ClientBytes = request.TotalBytes;
            if (request.QueryString["d"] != null)
            {
                int duration;
                Int32.TryParse(request.QueryString["d"], out duration);
                hit.ClientDuration = duration;
            }
            if (request.QueryString["tz"] != null)
            {
                hit.ClientTimeZone = request.QueryString["tz"];
            }
            if (request.QueryString["w"] != null)
            {
                int screenWidth;
                Int32.TryParse(request.QueryString["w"], out screenWidth);
                hit.ScreenHeight = screenWidth;
            }
            if (request.QueryString["h"] != null)
            {
                int screenWidth;
                Int32.TryParse(request.QueryString["h"], out screenWidth);
                hit.ScreenHeight = screenWidth;
            }
            if (request.QueryString["np"] != null)
            {
                hit.NavigatorPlatform = request.QueryString["np"];
            }
            return hit;
        }
    }
}
