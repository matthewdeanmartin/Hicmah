using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hicmah.Administration;
using Hicmah.Dimensions;
using Hicmah.SimulateHits;
using NUnit.Framework;
using Hicmah;
using System.Diagnostics;
using Hicmah.ChartData;

namespace Himah.IntegrationTests
{

    [TestFixture]
    public class DataAndCharts
    {
        private TableInfo hits;

        //This integration test requires the admin and hit recorders to be in really good shape.
        [TestFixtureSetUp]
        public void SimulateRealisticHits()
        {
            FakeCache cache = new FakeCache();
            Stopwatch watch = new Stopwatch();
            watch.Start();

            DatabaseAdministration admin = (new DatabaseAdministrationForSql());
            admin.DropIndexes();
            admin.ClearData();
            //Data generation will be faster 2nd time around if we let the db files stay large
            //admin.Compact(); 

            ProcessDimensions processor = new ProcessDimensions(new FakeCache());
            processor.Execute(admin);
            admin.ClearCache();
            admin.CreateIndexes();

            HitSimulator hs = new HitSimulator();
            hs.SimulateRealisticHits(cache, 10);
            Debug.WriteLine("Data generate for " + watch.ElapsedMilliseconds/1000 + "." + watch.ElapsedMilliseconds%1000 +
                            " seconds");
            //Don't clear logs! Here we want to use the data for other things.

            hits = admin.TableInfo();
        }

        [TestFixtureTearDown]
        public void ClearDb()
        {
            DatabaseAdministration admin = (new DatabaseAdministrationForSql());
            admin.ClearData();
        }

        [Test]
        public void AttackedPages()
        {
            using (ChartAttackedPages fetch = new ChartAttackedPages())
            {
                DateTime interval = new DateTime(2011, 11, 1, 0, 0, 0);
                DateTime endOfInterval = new DateTime(2011, 12, 1, 0, 0, 0);
                fetch.RetrieveData(interval, endOfInterval);
            }
        }

        [Test]
        public void ConcurrentUsersIn15MinuteWindow()
        {
            using (ChartConcurrentUsers fetch = new ChartConcurrentUsers())
            {
                DateTime interval = hits.Earliest;
                DateTime endOfInterval = interval.AddDays(1);
                fetch.RetrieveData(15, interval, endOfInterval);
            }
        }

        [Test]
        public void NewUsersPerMonthForAYear()
        {
            using (ChartNewUsersByMonth fetch = new ChartNewUsersByMonth())
            {
                DateTime interval = hits.Earliest;
                DateTime endOfInterval = interval.AddYears(1);
                fetch.RetrieveData(15, interval, endOfInterval);
            }
        }


        [Test]
        public void UsersPerDayForOneMonth()
        {
            using (ChartConcurrentUsers fetch = new ChartConcurrentUsers())
            {
                DateTime interval = hits.Earliest.AddDays(-2);
                DateTime endOfInterval = interval.AddDays(30);
                fetch.RetrieveData(24*60, interval, endOfInterval);
            }
        }

        [Test]
        public void TopOfEachOfTheMonth()
        {
            using (ChartTop fetch = new ChartTop())
            {
                foreach (string dim in fetch.Valid)
                {
                    DateTime interval = hits.Earliest;
                    DateTime endOfInterval = interval.AddMonths(1);
                    fetch.RetrieveData(10, UsageRate.Top, interval, endOfInterval, dim, new string[] {"Hits"});
                }
            }
        }

        [Test]
        public void TopUsers()
        {
            using (ChartTop fetch = new ChartTop())
            {
                DateTime interval = hits.Earliest;
                DateTime endOfInterval = interval.AddYears(1);
                fetch.RetrieveData(10, UsageRate.Top, interval, endOfInterval, "user", new string[] {"Hits"});
            }
        }

        [Test]
        public void RareUsers()
        {
            using (ChartTop fetch = new ChartTop())
            {
                DateTime interval = hits.Earliest;
                DateTime endOfInterval = interval.AddYears(1);
                fetch.RetrieveData(10, UsageRate.Bottom, interval, endOfInterval, "user", new string[] {"Hits"});
            }
        }

        [Test]
        public void TopPages()
        {
            using (ChartTop fetch = new ChartTop())
            {
                DateTime interval = hits.Earliest;
                DateTime endOfInterval = interval.AddYears(1);
                fetch.RetrieveData(10, UsageRate.Top, interval, endOfInterval, "full_url", new string[] {"Hits"});
            }
        }

        [Test]
        public void LessUsedPages()
        {
            using (ChartTop fetch = new ChartTop())
            {
                DateTime interval = hits.Earliest;
                DateTime endOfInterval = interval.AddYears(1);
                fetch.RetrieveData(10, UsageRate.Bottom, interval, endOfInterval, "full_url", new string[] {"Hits"});
            }
        }

        

        [Test]
        public void ChartHitsPerTimeSpan()
        {
            using (ChartHitsPerTimeSpan fetch = new ChartHitsPerTimeSpan())
            {
                DateTime interval = hits.Earliest;
                DateTime endOfInterval = interval.AddYears(1);
                fetch.RetrieveData(interval, endOfInterval, "monthly");
            }
        }
    }

    [TestFixture]
    public class GenerateTestData
    {
        [Test]
        public void SimulateRealisticHits()
        {
            DatabaseAdministration admin = (new DatabaseAdministrationForSql());
            //If you don't drop the indexes, perf will be lousy on inserts
            admin.ClearData();
            admin.DropIndexes();

            FakeCache cache = new FakeCache();
            Stopwatch watch = new Stopwatch();
            HitSimulator hs = new HitSimulator();
            watch.Start();
            hs.SimulateRealisticHits(cache,5*60);
            Debug.WriteLine(watch.ElapsedMilliseconds / 1000 + "." + watch.ElapsedMilliseconds % 1000 + " seconds");

            //If you don't do the next two, queries will return nothing or will be slow
            admin.ProcessAllDiminsions();
            admin.CreateIndexes();
            //Don't clear logs! Here we want to use the data for other things.
        }
    }
}
