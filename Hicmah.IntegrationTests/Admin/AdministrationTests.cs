using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Hicmah.Administration;
using Hicmah.Dimensions;

namespace Himah.IntegrationTests
{
    [TestFixture]
    public class AdministrationTests
    {
        [Test]
        public void ClearDb()
        {
            DatabaseAdministration admin = (new DatabaseAdministrationForSql());
            admin.ClearData();
        }
        
        
        

            [Test]
        public void ProcessTimeDimension()
        {
            ProcessTimeDimension processor = new ProcessTimeDimension(new FakeCache());
            processor.Process();
        }

        [Test]
        public void ProcessDateTimeDimension()
        { 
            //using(
                ProcessDateDimension processor = new ProcessDateDimension(new FakeCache());
            //{
                processor.ProcessWithDateTime( new DatabaseAdministrationForSql());
            //}
        }

        [Test]
        public void ProcessUrlDiminision()
        { 
            using(ProcessUriDimension processor = new ProcessUriDimension())
            {
                processor.Process();
            }
        }

        [Test]
        public void ProcessUserDimension()
        {
            using (ProcessUserDimension processor = new ProcessUserDimension())
            {
                processor.Process();
            }
        }

        
    }
}
