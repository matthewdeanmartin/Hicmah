using System.Configuration;

namespace Hicmah.DataConfiguration
{
    ///<summary>This is a handwritten version of the machine generated .settings file.
    ///Designers for for wussies.
    ///</summary>
    /// <remarks>
    /// The chain of events here is remarkable. The AppSettingBase constructor uses reflection 
    /// to iterate over the properties of this class then calls the SettingsProvider to
    /// go fill up the collection of keys and values from what ever the data source is.
    /// </remarks>
    [SettingsProvider(typeof(IrishSettingsProvider))]
    public class DbSettingFetcher : ApplicationSettingsBase
    {
        private static DbSettingFetcher defaultInstance = ((DbSettingFetcher)(global::System.Configuration.ApplicationSettingsBase.Synchronized(new DbSettingFetcher())));

        public static DbSettingFetcher Default
        {
            get
            {
                return defaultInstance;
            }
        }

        [System.Configuration.ApplicationScopedSetting()]
        [DefaultSettingValue("Yo!")]
        public string Something
        {
            get { return this["Something"].ToString(); }
            set
            {
                SettingsProperty property = Properties["Something"];
                SettingsPropertyValue spv = new SettingsPropertyValue(property);
                spv.IsDirty = true;
                spv.PropertyValue = value;
                //this["Something"] = value;
            }
        }
        //public override void Save()
        //{
        //}

        public override object this[string propertyName]
        {
            get
            {
                return base[propertyName];
            }
            set
            {
                base[propertyName] = value;
            }
        }

    }
}
