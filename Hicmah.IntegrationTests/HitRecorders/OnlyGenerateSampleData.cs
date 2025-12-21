using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Hicmah.Administration;
using Hicmah.Dimensions;
using Hicmah.SimulateHits;
using NUnit.Framework;

namespace Himah.IntegrationTests.HitRecorders
{
    [TestFixture]
    public class OnlyGenerateSampleData
    {
        [Test]
        public void TestIt()
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
            Debug.WriteLine("Data generated for " + watch.ElapsedMilliseconds/1000 + "." + watch.ElapsedMilliseconds%1000 +
                            " seconds");
            //Don't clear logs! Here we want to use the data for other things.

            TableInfo hits = admin.TableInfo();
        }
    }
}
