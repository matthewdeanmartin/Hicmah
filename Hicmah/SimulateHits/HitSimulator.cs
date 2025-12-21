using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Hicmah.Web;
using Hicmah.Recorders;
using Hicmah.Administration;
using Wrappers.WebContext;

namespace Hicmah.SimulateHits
{
    public  class HitSimulator
    {
        readonly Random random = new Random(DateTime.Now.Millisecond);

        public void SimulateRealisticHits(ICacheWrapper cache, long maxSeconds)
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();
            if (maxSeconds == 0)
                maxSeconds = long.MaxValue;
            
            DatabaseAdministration admin = (new AdministrationFactory()).DatabaseAdministration();
            admin.ClearData();
            
            using (IHitRecorder recorder = (new HitRecorderFactory()).MakeCachedHitRecorder(ConfigUtils.Provider(),cache,true))
            {
                //40000 used to be a good batch size for inserts via odbc to SQL 6. 
                //Sure anything today can cope, right?
                recorder.Capacity = 100000;
                
                int day = 0;
                DateTime startDay = new DateTime(2011, 11, 1);

                DateTime currentDay = startDay;

                int totalPeople = 150;
                List<HitSimulator.Person> list = new List<HitSimulator.Person>(totalPeople);

                //Browser agents don't change mid stream.
                for (int i = 0; i < totalPeople; i++)
                {
                    HitSimulator.Person nextPerson = new HitSimulator.Person();
                    nextPerson.Name = MakeName();
                    nextPerson.UserAgent = Browsers();
                    nextPerson.HireDate = startDay.AddDays(random.Next(0, 180));
                    list.Add(nextPerson);
                }
                //simulate about four years and some.
                //need at least 30 days to show 404s
                while (day < (90))
                {
                    //No work on weekends.
                    if (currentDay.DayOfWeek == DayOfWeek.Saturday)
                    {
                        day++;
                        currentDay = currentDay.AddDays(1);
                    }
                    if (currentDay.DayOfWeek == DayOfWeek.Sunday)
                    {
                        day++;
                        currentDay = currentDay.AddDays(1);
                    }

                    int showedUpToday = random.Next((int)(.60 * totalPeople), (int)(.95 * totalPeople));
                    for (int staffInOffice = 0; staffInOffice < showedUpToday; staffInOffice++)
                    {
                        //TODO: Use linq
                        //Oops, same person could show twice!
                        List<HitSimulator.Person> validPeople = (from p in list
                                                                 where p.HireDate < currentDay
                                                                 select p).ToList<HitSimulator.Person>();
                        if (validPeople.Count == 0)
                        {
                            continue;
                        }
                        HitSimulator.Person currentPerson = validPeople[random.Next(0, validPeople.Count)];

                        //int search = 0;
                        //while (!(currentDay < currentPerson.HireDate) || search<200)
                        //{
                        //    currentPerson = list[random.Next(0, list.Count)];
                        //    search++;
                        //}
                        //Stopped an infinite loop but could still admit a pre-employment person.

                        //Usually long sessions.
                        int sessionLength = random.Next(1, 90);
                        //Office day starts between 6 and 9 or so
                        DateTime sessionStart = currentDay.AddHours(6 + random.Next(0, 4)).AddMinutes(random.Next(1, 180));

                        string lastPage = "";
                        for (int pages = 0; pages < sessionLength; pages++)
                        {
                            string currentPage = "http://localhost/" + Pages();


                            Hit hit = HitFactory.BasicHitNow();
                            //Assume no anonymous
                            hit.User = currentPerson.Name;
                            hit.UserAgent = currentPerson.UserAgent;


                            hit.Url = currentPage;
                            hit.ReferrerUrl = lastPage;
                            lastPage = currentPage;

                            //Not using a mixed strategy. It's all javascript invocations
                            hit.Invoker = Invoker.HttpHandler;

                            //Gets and Posts. No love for REST in this office.
                            if (random.Next(0, 2) == 1)
                                hit.RequestType = RequestType.Get; //Gets and posts.
                            else
                                hit.RequestType = RequestType.Post;
                            hit.StatusCode = StatusCode(currentPage, startDay, currentDay);

                            sessionStart = sessionStart.AddMinutes(random.Next(1, 5));
                            hit.HitDate = sessionStart;
                            //Make up the server's time zone.
                            hit.UtcTime = new DateTimeOffset(sessionStart,new TimeSpan(6,0,0));
                            
                            hit.ServerDuration = random.Next(150, 800);
                            hit.ClientDuration = random.Next(500, 1200);
                            if (hit.RequestType == RequestType.Post)
                                hit.ClientBytes = random.Next(50000, 175000);
                            else
                                hit.ClientBytes = random.Next(300, 395);
                            foreach(string s in hit.ListErrors())
                            {
                                throw new InvalidOperationException("Invalid value for hit property of " + s);
                            }
                            recorder.InsertHit(hit);
                        }//1 person's session
                    }//next person
                    day++;
                    currentDay = currentDay.AddDays(1);
                    if(watch.ElapsedMilliseconds>maxSeconds*1000)
                    {
                        day = int.MaxValue;
                    }
                }
            }
        }

        public string MakeName()
        {
            //Expecting small or medium number of people, not millions, this is an intranet app!
            string[] fnames = { "Jon", "Sally", "Janet", "Rikki", "Bill", "Janet", "Fred", "Jojo" };
            string[] initials = { "Q", "Z", "R", "L", "N", "O" };
            string[] lnames = { "Smith", "Zarquart", "Fuchs", "Beliov", "Jonson", "Martin", "Fitch", "Flossum" };
            return fnames[random.Next(0, 8)] + " " + initials[random.Next(0, 6)] + " " + lnames[random.Next(0, 6)];
        }

        public string Pages()
        {
            string[] values = { "Home.html", "Help.html", "Search.html", "Search.html?s=" + Search(), "Cats.html", "Fun.html", "Dashboard.html", "EmployeeOfTheYear.html", "Vacation.html" };
            return values[random.Next(0, 9)];
        }

        public string[] MissingPages()
        {
            return new string[] { "Help.html", "Vacation.html" };
        }

        public string Search()
        {
            string[] values = { "cats", "pengquins", "way out", "<script>alert('gotcha');</script>", "more", "less", "lizards" };
            return values[random.Next(0, 6)];
        }

        public int StatusCode(string page, DateTime startDate, DateTime currentDate)
        {

            if (currentDate > startDate.AddDays(30))
            {
                foreach (string s in MissingPages())
                {
                    if (page.Contains(s))
                        return 404;
                }
            }
            if (random.Next(0, 6) < 5)
                return 200;
            else
            {
                //Longer title means more errors. Ha!
                if (random.Next(0, page.Length) > 3)
                {
                    switch (random.Next(0, 5))
                    {
                        case 0:
                            return 401; //unauth
                        case 1:
                            return 403;//forbidden!
                        case 2:
                            return 500; //server error
                        case 3:
                            return 501; //not implemented
                        case 4:
                            return 400; //bad request
                        default:
                            return 418;
                    }
                }
                return 200;
            }
        }

        public string Browsers()
        {
            //Expecting a small number of browsers (this is an intranet app!)
            //All three OS's represented.
            string[] browsers = {
                                    "Mozilla/5.0 (Windows; U; Windows NT 5.1; en-GB; rv:1.8.1.6) Gecko/20070725 Firefox/2.0.0.6", 
                                    "Opera/9.00 (Windows NT 5.1; U; en)",
                                    "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.1; .NET CLR 1.1.4322)", 
                                    "Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 5.1; .NET CLR 1.1.4322; .NET CLR 2.0.50727; .NET CLR 3.0.04506.30)", 
                                    "Mozilla/5.0 (X11; U; Linux i686; en-US; rv:1.6) Gecko/20050614 Firefox/0.8",
                                    "Mozilla/5.0 (Macintosh; U; PPC Mac OS X; en) AppleWebKit/125.2 (KHTML, like Gecko) Safari/85.8",
                                    "Mozilla/4.0 (compatible; MSIE 5.5; Windows 98; Win 9x 4.90)"};
            return browsers[random.Next(0, 6)];
        }

        public class Person
        {
            public string Name { get; set; }
            public string UserAgent { get; set; }
            public DateTime HireDate { get; set; }
        }


    }
}
