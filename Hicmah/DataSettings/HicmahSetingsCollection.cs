using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IrishSettings;

namespace Hicmah.DataSettings
{
    public class HicmahSetingsCollection : Dictionary<Version, SettingsApplication>
    {
        private Version maxVersion;

        public HicmahSetingsCollection()
        {
            SettingsApplication v1 = new HicmahSettings();
            this.Add(v1.VersionObject,v1 );
            //SettingsApplication v2 = new HicmahSettings2();
            //this.Add(v2.VersionObject, v2);
            maxVersion = v1.VersionObject;
        }

        public SettingsApplication Current()
        {
            return this[maxVersion];
        }
    }
}
