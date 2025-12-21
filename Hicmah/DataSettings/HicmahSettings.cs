using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using IrishSettings;

namespace Hicmah.DataSettings
{
    public class HicmahSettings : SettingsApplication
    {
        public HicmahSettings()
        {
            
            Application = "hicmah";
            VersionObject = new Version(0, 1);
            CultureObject = new CultureInfo("en-US");//This drives serialization/deserialization. Used by the settings library.

            Schema.Add("culture", Default("culture", "en-US")); //is-is, this is for the using app.
            Schema.Add("timezone", Default("timezone", "EST"));
            Schema.Add("fiscalStart", Default("fiscalStart", "10"));
            Schema.Add("workdayStart", Default("workdayStart", "8"));
            Schema.Add("workdayEnd", Default("workdayEnd", "16"));
            Schema.Add("earliestHitDate", Default("earliestHitDate", "1/1/2011"));

            Schema.Add("testRemoval", Default("testRemoval", "1/1/2011"));
        }

        private static GenericSettingSchema Default(string name, string defaultValue)
        {
            GenericSettingSchema item = new GenericSettingSchema();
            item.ValueTypeObject = typeof(string);
            item.Name = name;
            item.DefaultSerializedValue = defaultValue;
            item.Scope = SettingScope.Application;
            return item;
        }
    }


    public class HicmahSettings2 : SettingsApplication
    {
        public HicmahSettings2()
        {

            Application = "hicmah";
            VersionObject = new Version(2, 1);
            CultureObject = new CultureInfo("en-US");//This drives serialization/deserialization. Used by the settings library.

            Schema.Add("culture", Default("culture", "en-US")); //is-is, this is for the using app.
            Schema.Add("timezone", Default("timezone", "EST"));
            Schema.Add("fiscalStart", Default("fiscalStart", "10"));
            Schema.Add("workdayStart", Default("workdayStart", "8"));
            Schema.Add("workdayEnd", Default("workdayEnd", "16"));
            Schema.Add("earliestHitDate", Default("earliestHitDate", "1/1/2011"));

            //Schema.Add("testRemoval", Default("testRemoval", "1/1/2011"));

            //New Items
            Schema.Add("testNewItem", Default("testNewItem", "1/1/2011"));

        }

        private static GenericSettingSchema Default(string name, string defaultValue)
        {
            GenericSettingSchema item = new GenericSettingSchema();
            item.ValueTypeObject = typeof(string);
            item.Name = name;
            item.DefaultSerializedValue = defaultValue;
            item.Scope = SettingScope.Application;
            return item;
        }
    }
}
