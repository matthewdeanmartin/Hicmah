using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Hicmah;
using Hicmah.Administration;
using Hicmah.Recorders;
using NUnit.Framework;

namespace Himah.IntegrationTests
{
    [TestFixture]
    public class CachedHitsWithSqlBulkCopy
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
        public void ThousandIdenticalHits()
        {
            FakeCache cache = new FakeCache();
            Stopwatch watch = new Stopwatch();
            watch.Start();

            using (IHitRecorder recorder = new HitRecorderMsSqlCachedBulk(cache))
            {
                for (int i = 0; i < 1000; i++)
                {
                    Hit hit = HitMaker.SampleHit();
                    recorder.InsertHit(hit);
                }
                Debug.WriteLine(watch.ElapsedMilliseconds/1000 + "." + watch.ElapsedMilliseconds%1000 + " seconds");
            }
        }

        [Test]
        public void ThousandSampleHits()
        {
            FakeCache cache = new FakeCache();
            Stopwatch watch = new Stopwatch();
            watch.Start();

            using (IHitRecorder recorder = new HitRecorderMsSqlCachedBulk(cache))
            {
                for (int i = 0; i < 1000; i++)
                {
                    Hit hit = hitMaker.SampleHitAlwaysDifferent();
                    recorder.InsertHit(hit);
                }
                Debug.WriteLine(watch.ElapsedMilliseconds/1000 + "." + watch.ElapsedMilliseconds%1000 + " seconds");
            }
        }
    }
}
