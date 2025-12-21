using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Hicmah
{
    /// <summary>
    /// A page hit, HTTP request, or feature usage.
    /// </summary>
    /// <remarks>
    /// This intentionally looks like the fact of a fact table from a star schema.
    /// </remarks>
    [Serializable]
    public class Hit
    {
        public bool IsValid()
        {
            if(ListErrors().Length==0) return true;
            return false;
        }

        public  string[] ListErrors()
        {
            List<string> foo = new List<string>();

            //smallint
            if(TimeId>32767) foo.Add("TimeId");
            if(ClientTimeId>32767) foo.Add("ClientTimeId");
            if(ClientTimeZoneId>32767) foo.Add("ClientTimeZoneId");
            if(UrlId>32767) foo.Add("UrlId");
            if(ReferrerUrlId>32767) foo.Add("ReferrerUrlId");
            if(UserAgentId>32767) foo.Add("UserAgentId");
            if(UserId>32767) foo.Add("UserId");
            if (StatusCode > 32767) foo.Add("StatusCode");
            if (ClientDuration > 32767) foo.Add("SecondsOnPage");
            //NET.'s int is smaller than SQL int
            //if(DateId>2147483648) foo.Add("DateId");
            //if(ServerDuration>2147483648) foo.Add("ServerDuration");
            //if(ClientDuration>2147483648) foo.Add("ClientDuration");
            //if(ClientBytes>2147483648) foo.Add("ClientBytes");

            //Tiny (unlikely problem if using Enums)
            if((int)RequestType>255) foo.Add("RequestType");
            if ((int)Invoker > 255) foo.Add("Invoker");

            return foo.ToArray();
        }

        //Primary Key
        public string Id { get; set; }

        //Dimensions

        //Time
        //Time independent of server, client time zones.
        public DateTimeOffset UtcTime { get; set; }

        //Time on server
        public DateTime HitDate { get; set; }
        public int DateId { get; set; } //This can be calculated from hit date, no need to lookup.
        public int TimeId { get; set; }

        public string ClientTimeZone { get; set; }
        public int ClientTimeZoneId { get; set; }
        public int ClientDateId { get; set; }
        public int ClientTimeId { get; set; }

        //Page, Url, Request, Feature
        public string Url { get; set; } //Small set of values, can grow over time
        public int UrlId { get; set; }

        //Sending page
        public string ReferrerUrl { get; set; } //Small set of values, can grow over time
        public int ReferrerUrlId { get; set; }

        //User and any other info available from authentication 
        public string User { get; set; } //Expected small set of values, can grow over time
        public int UserId { get; set; }

        //Type of Hicmah hit counter
        public Invoker Invoker { get; set; }

        //HTTP protocol dimension
        public RequestType RequestType { get; set; } //Fix set of possible values
        public int StatusCode { get; set; }
        
        //User machine 
        public int ScreenWidth { get; set; }
        public int ScreenHeight { get; set; }

        //Browser, OS, sometimes capabilities
        public string UserAgent { get; set; } //Small set of values, can grow over time
        public int UserAgentId { get; set; }
        public string NavigatorPlatform { get; set; }

        //Measures
        //Performance
        public int ServerDuration { get; set; }
        public int ClientDuration { get; set; }

        //Bandwidth
        public int ClientBytes { get; set; }
        public long ResponseBytes { get; set; }

        //Calculated
        public long TimeOnPage { get; set; }

        //Not Cheap!
        public override string ToString()
        {
            //http://www.csharp-examples.net/reflection-property-names/
            // get all public static properties of MyClass type
            PropertyInfo[] propertyInfos;
            propertyInfos = typeof(Hit).GetProperties(BindingFlags.Public |
                                                          BindingFlags.Static);
            // sort properties by name
            Array.Sort(propertyInfos,
                    delegate(PropertyInfo propertyInfo1, PropertyInfo propertyInfo2)
                    { return propertyInfo1.Name.CompareTo(propertyInfo2.Name); });

            StringBuilder sb = new StringBuilder();

            // write property names
            foreach (PropertyInfo propertyInfo in propertyInfos)
            {
              sb.Append(propertyInfo.Name);
                sb.Append(":");
              sb.AppendLine(propertyInfo.GetValue(this,null).ToString());
            }
            
            return sb.ToString();
        }
    }
}
