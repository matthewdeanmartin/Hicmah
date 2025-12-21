using System;
using System.Collections.Specialized;
using System.Configuration;
using System.Data.Common;
using Dataglot;
using IrishSettings;
using Hicmah.Administration;

namespace Hicmah.DataConfiguration
{
    /// <summary>
    /// A persistent settings provider as an alternative to AppSettings in the .config file.
    /// </summary>
    /// <remarks>
    /// The goal is to support a familiar interface, even if it isn't very pretty and 
    /// is optimized for Winforms programming. This also the same interface used by ASP.NET
    /// Profiles, although ASP.NET profiles is optimized for storing per user settings.
    /// </remarks>
    public class IrishSettingsProvider : SettingsProvider, IApplicationSettingsProvider
    {
        public override void Initialize(string name, NameValueCollection col)
        {
            //Load from db here?
           //LocalFileSettingsProvider l = new LocalFileSettingsProvider();
            //Sets a name and description. Called by ApplicationSettingsBase
            base.Initialize(this.ApplicationName, col);
            
        }

        // SetPropertyValue is invoked when ApplicationSettingsBase.Save is called
        // ASB makes sure to pass each provider only the values marked for that provider,
        // whether on a per setting or setting class-wide basis
        public override void SetPropertyValues(SettingsContext context, SettingsPropertyValueCollection propvals)
        {
            // Iterate through the settings to be stored
            string version = GetCurrentVersionNumber();
            foreach( SettingsPropertyValue propval in propvals )
            {
                // If property hasn't been set, no need to save it
                if( !propval.IsDirty || (propval.SerializedValue == null) ) { continue; }

                // Application-scoped settings can't change
                // NOTE: the settings machinery may cause or allow an app-scoped setting
                // to become dirty, in which case, like the LFSP, we ignore it instead
                // of throwning an exception
                if( IsApplicationScoped(propval.Property) ) { continue; }

                //TODO: INSERT/UPDATE in database
                //using( RegistryKey key = CreateRegKey(propval.Property, version) )
                //{
                    //key.SetValue(propval.Name, propval.SerializedValue);
                //}
                //using (InsertSettingsCommand insertSettingsCommand = new InsertSettingsCommand())
                //{
                //    using (UpdateApplicationSettings updateSettingsCommand = new UpdateApplicationSettings())
                //    {
                //        //using (SelectSettings select = new SelectSettings())
                //        //{
                //        //    foreach (SettingsProperty value in propvals)
                //        //    {
                //        //        using (DbDataReader reader = select.Select(value.Name))
                //        //        {
                //        //            if (reader.HasRows)
                //        //            {
                //        //                updateSettingsCommand.Update(value.Name, value.ToString());
                //        //            }
                //        //            else
                //        //            {
                //        //                insertSettingsCommand.Insert(value.Name, value.ToString(), version, "Application", "default", 0);
                //        //            }
                //        //        }


                //        //    }
                //        //}
                //    }
                //}

            }
        }

        public override string ApplicationName
        {
            get { return "Hicmah"; }
            set { }
        }

        public override SettingsPropertyValueCollection GetPropertyValues(SettingsContext context, SettingsPropertyCollection props)
        {
            // Create new collection of values
            SettingsPropertyValueCollection values = new SettingsPropertyValueCollection();
            //string version = "1";

            using (ParameterlessCommand select = new ParameterlessCommand(ConfigUtils.CurrentDataFactory()))
            {
                using (DbDataReader reader = select.ExecuteReader("Select * from {$ns}_settings_values"))
                {
                    while (reader.Read())
                    {
                        if (props[reader["name"].ToString()] != null)
                        {
                            SettingsProperty prop = props[reader["name"].ToString()];
                            SettingsPropertyValue value = new SettingsPropertyValue(prop);
                            value.SerializedValue = reader["serialized_value"];
                            value.IsDirty = false;
                            values.Add(value);
                        }
                    }
                }
                return values;
            }
        }

        SettingsPropertyValue GetPropertyValue(SettingsProperty prop, string version)
        {
            SettingsPropertyValue value = new SettingsPropertyValue(prop);

            // Only User-scoped settings can be found in the Registry.
            // By leaving the Application-scoped setting's value at null,
            // we get the "default" value
            if( IsUserScoped(prop) )
            {
                //Look up 

                //using( RegistryKey key = CreateRegKey(prop, version) )
                //{
                //    value.SerializedValue = key.GetValue(prop.Name);
                //}
            }

            value.IsDirty = false;
            return value;
        }

        // Looks in the "attribute bag" for a given property to determine if it is app-scoped
        bool IsApplicationScoped(SettingsProperty prop)
        {
            return HasSettingScope(prop, typeof(ApplicationScopedSettingAttribute));
        }

        // Looks in the "attribute bag" for a given property to determine if it is user-scoped
        bool IsUserScoped(SettingsProperty prop)
        {
            return HasSettingScope(prop, typeof(UserScopedSettingAttribute));
        }

        // Checks for app or user-scoped based on the attributeType argument
        // Also checks for sanity, i.e. a setting not marked as both or neither scope
        // (just like the LFSP)
        bool HasSettingScope(SettingsProperty prop, Type attributeType)
        {
            //ebug.Assert((attributeType == typeof(ApplicationScopedSettingAttribute)) || (attributeType == typeof(UserScopedSettingAttribute)));
            bool isAppScoped = prop.Attributes[typeof(ApplicationScopedSettingAttribute)] != null;
            bool isUserScoped = prop.Attributes[typeof(UserScopedSettingAttribute)] != null;

            // Check constraints
            if( isUserScoped && isAppScoped )
            {
                throw new Exception("Can't mark a setting as User and Application-scoped: " + prop.Name);
            }
            else if( !isUserScoped && !isAppScoped )
            {
                throw new Exception("Must mark a setting as User or Application-scoped: " + prop.Name);
            }

            // Return scope check result
            if( attributeType == typeof(ApplicationScopedSettingAttribute) )
            {
                return isAppScoped;
            }
            else if( attributeType == typeof(UserScopedSettingAttribute) )
            {
                return isUserScoped;
            }
            else
            {
                //uh oh
                //Debug.Assert(false);
                return false;
            }
        }

        string GetPreviousVersionNumber()
        {
            Version current = new Version(GetCurrentVersionNumber());
            Version previous = null;

            // Enum setting versions
            //using( RegistryKey key = Registry.CurrentUser.OpenSubKey(GetVersionIndependentSubKeyPath(), false) )
            //{
            //    foreach( string keyName in key.GetSubKeyNames() )
            //    {
            //        try
            //        {
            //            Version version = new Version(keyName);
            //            if( version >= current ) { continue; }
            //            if( previous == null || version > previous ) { previous = version; }
            //        }
            //        catch
            //        {
            //            // If the version can't be created, don't cry about it...
            //            continue;
            //        }
            //    }
            //}

            // Return the string of the previous version
            return (previous != null ? previous.ToString() : null);
        }

        string GetCurrentVersionNumber()
        {
            // The compiler will make sure this is a sane value
            return "1";
        }

        // Will be called when MySettingsClass.GetPreviousVersion(propName) is called
        // This method's job is to retrieve a setting value from the previous version
        // of the settings w/o updating the setting at the storage location
        public SettingsPropertyValue GetPreviousVersion(SettingsContext context, SettingsProperty prop)
        {
            // If there's no previous setting version, return an empty property
            // NOTE: the LFSP returns an empty property for all app-scoped settings, so so do we
            string previousVersion = GetPreviousVersionNumber();
            if( IsApplicationScoped(prop) || string.IsNullOrEmpty(previousVersion) ) {
                // NOTE: can't just return null, as the settings engine turns that into
                // a default property -- have to return a SettingsPropertyValue object
                // with the PropertyValue set to null to really build an empty property
                SettingsPropertyValue propval = new SettingsPropertyValue(prop);
                propval.PropertyValue = null;
                return propval;
            }

            // Get the property value from the previous version
            // NOTE: if it's null, the settings machinery will assume the current default value
            // ideally, we'd want to use the previous version's default value, but a) that's
            // likely to be the current default value and b) if it's not, that data is lost
            return GetPropertyValue(prop, previousVersion);
        }

        // Will be called when MySettingsClass.Reset() is called
        // This method's job is to update the location where the settings are stored
        // with the default settings values. GetPropertyValues, overriden from the
        // SettingsProvider base, will be called to retrieve the new values from the
        // storage location
        public void Reset(SettingsContext context)
        {
            // Delete the user's current settings so that default values are used
            try
            {
                //TODO: Restore settings to factory default?
               // Registry.CurrentUser.DeleteSubKeyTree(GetSubKeyPath(GetCurrentVersionNumber()));
            }
            catch( ArgumentException )
            {
                // If the key's not there, this is the exception we'll get
                // TODO: figure out a way to detect that w/o consuming
                // an overly generic exception...
            }
        }

        // Will be called when MySettingsClass.Upgrade() is called
        // This method's job is to update the location where the settings are stored
        // with the previous version's values. GetPropertyValues, overriden from the
        // SettingsProvider base, will be called to retrieve the new values from the
        // storage location
        public void Upgrade(SettingsContext context, SettingsPropertyCollection properties)
        {
           
            // If there's no previous version, do nothing (just like the LFSP)
            string previousVersion = GetPreviousVersionNumber();
            if( string.IsNullOrEmpty(previousVersion) ) { return; }

            // Delete the current setting values
            Reset(context);

            // Copy the old settings to the new version
            string currentVersion = GetCurrentVersionNumber();
            //using( RegistryKey keyPrevious = Registry.CurrentUser.OpenSubKey(GetSubKeyPath(previousVersion), false) )
            //using( RegistryKey keyCurrent = Registry.CurrentUser.CreateSubKey(GetSubKeyPath(currentVersion), RegistryKeyPermissionCheck.ReadWriteSubTree) )
            //{
            //    foreach( string valueName in keyPrevious.GetValueNames() )
            //    {
            //        object serializedValue = keyPrevious.GetValue(valueName);
            //        if( serializedValue != null ) { keyCurrent.SetValue(valueName, serializedValue); }
            //    }
            //}
        }
    }
}
