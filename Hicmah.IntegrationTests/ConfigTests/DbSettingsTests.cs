using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Hicmah.DataConfiguration;
using NUnit.Framework;

namespace Himah.IntegrationTests.ConfigTests
{
    [TestFixture]
    public class IrishSettingsTests
    {
        [Test]
        public void GetAProperty()
        {
            //Using synchronized instance. (provides some sort thread safety magic)
            DbSettingFetcher db = DbSettingFetcher.Default;
            
            //This doesn't trigger a db request!
            //db.Properties["Something"].DefaultValue = "Bah!";

            //The following triggers the db request.
            db.SettingsSaving += new SettingsSavingEventHandler(db_SettingsSaving);
            db.Something = "Bah!";
            
            Trace.WriteLine("Provider for property :"  + db.Properties["Something"].Provider);
            foreach(Attribute attr in db.Properties["Something"].Attributes.Values)
            {
                Trace.WriteLine("   Attribute : " + attr.ToString());
            }

            db.Save();
            foreach(object p in db.Providers)
            {
                Trace.WriteLine(p);
            }
            Assert.NotNull(db.Properties["Something"]);

            object foo = db.Properties["Something"].DefaultValue;
            Assert.AreEqual(foo.ToString(), "Bah!");

            db.Reload();//This acts as it if is ignored
            db.Save();//This acts as it if is ignored
            //db.Upgrade();//This gets to the provider
        }

        void db_SettingsSaving(object sender, System.ComponentModel.CancelEventArgs e)
        {
            //throw new NotImplementedException();
        }

        [Test]
        public void SetAProperty()
        {
            DbSettingFetcher db = DbSettingFetcher.Default;

            //This doesn't trigger a db request!
            //db.Properties["Something"].DefaultValue = "Bah!";

            //The following triggers the db request.
            db.Something = "Bah!";

            Assert.NotNull(db.Something);
            db.Something = "Baz";
            Assert.AreEqual(db.Properties["Test"].DefaultValue, "baz");
        }

        [Test]
        public void SetAndSave()
        {
            DbSettingFetcher db = new DbSettingFetcher();
            Assert.NotNull(db.Properties["Test"]);
            db.Properties["Test"].DefaultValue = "baz";
            Assert.AreEqual(db.Properties["Test"].DefaultValue, "baz");
            db.Save();
        }

    }
}
