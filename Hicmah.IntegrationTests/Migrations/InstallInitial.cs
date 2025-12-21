using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using Hicmah.Administration;
using NUnit.Framework;

namespace Hicmah.UnitTests.Migrations
{
    [TestFixture]
    public class InstallInitial
    {
        

        [Test]
        public void RunIt()
        {
            DatabaseInstaller di = new DatabaseInstaller();
            string result = di.Install();
            Assert.IsNotNull(result);
        }


        [Test]
        public void TestingTraceDebugBehaviorOfResharperTestRunner()
        {
            //All three are always output to the resharper test window.
            Debug.WriteLine("Hello from debug");
            Trace.WriteLine("Hello from trace");
            Console.WriteLine("Hello from console");

            
        }
    }
}
