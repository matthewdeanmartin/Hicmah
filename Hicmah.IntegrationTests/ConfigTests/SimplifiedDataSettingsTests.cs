using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hicmah.DataSettings;
using NUnit.Framework;

namespace Himah.IntegrationTests.ConfigTests
{
    [TestFixture]
    public class SimplifiedDataSettingsTests
    {
        [Test]
        public void InitializeTest()
        {
            //Defaults
            HicmahSettingsManager hsm = new HicmahSettingsManager(new FakeCache());
            Assert.AreEqual(hsm.Culture(), "is-is");
        }
    }
}
