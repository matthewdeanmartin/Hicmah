using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hicmah;
using Hicmah.Web;

namespace Himah.IntegrationTests
{
    public class HitMaker
    {
        Random random = new Random(DateTime.Now.Millisecond);

        public static Hit SampleHit()
        {
            Hit hit = HitFactory.BasicHitNow();
            hit.User = "Matthew Martin";
            hit.Url = "http://google.com";
            hit.ReferrerUrl = "http://bing.com";
            hit.UserAgent = "Mozilla/5.0 (Windows NT 6.1) AppleWebKit/535.2 (KHTML, like Gecko) Chrome/15.0.874.120 Safari/535.2";
            hit.Invoker = Invoker.CodeInvoke;
            hit.RequestType = RequestType.Get;
            hit.HitDate = DateTime.Now;
            hit.UtcTime = DateTimeOffset.UtcNow;
            hit.ServerDuration = 500;
            hit.ClientDuration = 800;
            hit.ClientBytes = 175;
            return hit;
        }


        public Hit SampleHitAlwaysDifferent()
        {
            Hit hit = new Hit();
            hit.User = Guid.NewGuid().ToString();
            hit.Url = Guid.NewGuid().ToString();
            hit.ReferrerUrl = Guid.NewGuid().ToString();
            hit.UserAgent = Guid.NewGuid().ToString();
            hit.Invoker = Invoker.CodeInvoke;
            hit.RequestType = RequestType.Get;
            hit.HitDate = DateTime.Now;
            hit.UtcTime = DateTimeOffset.UtcNow;
            hit.ServerDuration = random.Next();
            hit.ClientDuration = random.Next();
            hit.ClientBytes = random.Next();
            return hit;
        }
    }
}
