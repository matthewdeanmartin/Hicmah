using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace Hicmah.UnitTests.Time
{
    [TestFixture]
    public class TimeUnitTests
    {
        [Test]
        public void TestZones()
        {
            DateTime now = DateTime.Now;
            DateUtils.ClientTime(now,"somewhere/somehow");

        }

    }
}
