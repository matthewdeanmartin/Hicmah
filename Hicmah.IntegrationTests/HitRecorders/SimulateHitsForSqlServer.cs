using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Web.Caching;
using Hicmah;
using Hicmah.Administration;
using Hicmah.Recorders;
using Hicmah.Web;
using NUnit.Framework;

namespace Himah.IntegrationTests
{
    /// <summary>
    /// This is an integration test that uses the nUnit framework. It will rollback changes,
    /// can fail if the initial state of the database is unexpected, and can be slow.
    /// </summary>
    [TestFixture]
    public class SimulateHitsForSqlServer
    {
        readonly HitMaker hitMaker = new HitMaker();

        [SetUp]
        public void ClearBefore()
        {
            DatabaseAdministration admin = (new DatabaseAdministrationForSql());
            admin.DropIndexes();
            admin.ClearData();
        }

        [TearDown]
        public void ClearAfter()
        {
            DatabaseAdministration admin = (new DatabaseAdministrationForSql());
            admin.DropIndexes();
            admin.ClearData();
        }
        [Test]
        public void SimpleHit()
        {
            FakeCache cache = new FakeCache();

            using (HitRecorderMsSql rh = new HitRecorderMsSql(cache))
            {
                Hit hit = HitMaker.SampleHit();
                rh.InsertHit(hit);
            }
        }

        [Test]
        public void TwoIdenticalHits()
        {
            //Optimized for tracing.

            FakeCache cache = new FakeCache();

            using (HitRecorderMsSql rh = new HitRecorderMsSql(cache))
            {
                Hit hit = HitMaker.SampleHit();
                rh.InsertHit(hit);
                rh.InsertHit(hit);
                rh.InsertHit(hit);
            }
        }


        [Test]
        public void ThousandIdenticalHits()
        {
            FakeCache cache = new FakeCache();
            Stopwatch watch = new Stopwatch();
            watch.Start();

            using (HitRecorderMsSql rh = new HitRecorderMsSql(cache))
            {
                for (int i = 0; i < 1000; i++)
                {

                    Hit hit = HitMaker.SampleHit();
                    rh.InsertHit(hit);

                }
            }
            Debug.WriteLine(watch.ElapsedMilliseconds / 1000 + "." + watch.ElapsedMilliseconds % 1000 + " seconds");
        }

        [Test]
        public void ThousandSampleHits()
        {
            FakeCache cache = new FakeCache();
            Stopwatch watch = new Stopwatch();
            watch.Start();
            using (HitRecorderMsSql rh = new HitRecorderMsSql(cache))
            {

                for (int i = 0; i < 1000; i++)
                {
                    Hit hit = hitMaker.SampleHitAlwaysDifferent();
                    rh.InsertHit(hit);
                }

                Debug.WriteLine(watch.ElapsedMilliseconds / 1000 + "." + watch.ElapsedMilliseconds % 1000 + " seconds");

            }
        }


    }


}
