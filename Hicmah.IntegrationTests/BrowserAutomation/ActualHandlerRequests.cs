using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Cache;
using System.Text;
using System.Threading;
using NUnit.Framework;

namespace Himah.IntegrationTests
{
    [Ignore]
    [TestFixture]
    public class ActualHandlerRequests
    {
        //TODO: Setup, confirm server exists at expected location. If not, ignore all.
        


        [Test]
        public void ThousandIdenticalDirectHits()
        {
            VaryIdenticalByHandler("http://localhost:55466/hicmah.ashx");
        }

        [Test]
        public void ThousandIdenticalDirectHitsAsynchHandler()
        {
            VaryIdenticalByHandler("http://localhost:55466/HitHandlerAsync.ashx");
        }

        private static void VaryIdenticalByHandler(string url)
        {
            Uri targetUri = new Uri(url);
            Stopwatch watch = new Stopwatch();
            watch.Start();

            for (int i = 0; i < 1000; i++)
            {
                try
                {
                    System.Net.WebRequest fr = HttpWebRequest.Create(targetUri);
                    HttpRequestCachePolicy noCachePolicy = new HttpRequestCachePolicy(HttpRequestCacheLevel.NoCacheNoStore);
                    fr.CachePolicy = noCachePolicy;
                    if ((fr.GetResponse().ContentLength > 0))
                    {
                        WebResponse response = fr.GetResponse();
                        Assert.IsFalse(response.IsFromCache);
                        using (Stream responseStream = response.GetResponseStream())
                        {
                            if (responseStream == null)
                            {
                                throw new InvalidOperationException("Response stream was null");
                            }
                            using (StreamReader str = new StreamReader(responseStream))
                            {
                                string result = str.ReadToEnd();
                                //Assert.IsTrue(result.Contains("This is the smallest possible SharpDom view"));
                                str.Close();
                            }
                            responseStream.Close();
                        }
                        //Must call this or web dev server will die at ~50 requests.
                        response.Close();
                        //Debug.WriteLine("Request #" + i + " took " + watch.ElapsedMilliseconds + " milliseconds");
                        //Thread.Sleep(250);
                        //watch.Reset();
                    }
                }
                catch (WebException ex)
                {
                    System.Diagnostics.Debug.WriteLine(ex.Message);
                }
            }
            Debug.WriteLine(watch.ElapsedMilliseconds/1000 + "." + watch.ElapsedMilliseconds%1000 + " seconds");
        }


        [Test]
        public void ThousandDifferentDirectHits()
        {
            Uri targetUri = new Uri("http://localhost:55466/hicmah.ashx");
            Stopwatch watch = new Stopwatch();
            watch.Start();

            for (int i = 0; i < 1000; i++)
            {
                try
                {

                    System.Net.WebRequest fr = HttpWebRequest.Create(targetUri +"?stuff=" + Guid.NewGuid().ToString());
                    HttpRequestCachePolicy noCachePolicy = new HttpRequestCachePolicy(HttpRequestCacheLevel.NoCacheNoStore);
                    fr.CachePolicy = noCachePolicy;
                    if ((fr.GetResponse().ContentLength > 0))
                    {
                        WebResponse response = fr.GetResponse();
                        Assert.IsFalse(response.IsFromCache);
                        using (Stream responseStream = response.GetResponseStream())
                        {
                            if (responseStream == null)
                            {
                                throw new InvalidOperationException("Response stream was null");
                            }
                            using (StreamReader str = new StreamReader(responseStream))
                            {
                                string result = str.ReadToEnd();
                                Assert.IsTrue(result.Contains("This is the smallest possible SharpDom view"));
                                str.Close();
                            }
                            responseStream.Close();
                        }
                        //Must call this or web dev server will die at ~50 requests.
                        response.Close();
                        //Debug.WriteLine("Request #" + i + " took " + watch.ElapsedMilliseconds + " milliseconds");
                        //Thread.Sleep(250);
                        //watch.Reset();
                    }
                }
                catch (WebException ex)
                {
                    System.Diagnostics.Debug.WriteLine(ex.Message);
                }
            }
            Debug.WriteLine(watch.ElapsedMilliseconds / 1000 + "." + watch.ElapsedMilliseconds % 1000 + " seconds");
        }
    }
}
