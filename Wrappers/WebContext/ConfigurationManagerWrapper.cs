using System;
using System.Configuration;

namespace Wrappers.WebContext
{
    public interface IConfigurationManager
    {
        object GetSection(string sectionName);
        Configuration OpenExeConfiguration(string exePath);
        Configuration OpenExeConfiguration(ConfigurationUserLevel userLevel);
        Configuration OpenMachineConfiguration();

        Configuration OpenMappedExeConfiguration(
            ExeConfigurationFileMap fileMap, ConfigurationUserLevel userLevel);

        Configuration OpenMappedMachineConfiguration(ConfigurationFileMap fileMap);
        void RefreshSection(string sectionName);
        System.Collections.Specialized.NameValueCollection AppSettings { get; }
        ConnectionStringSettingsCollection ConnectionStrings { get; }
    }

    public class ConfigurationManagerWrapper : IConfigurationManager
    {
        public object GetSection(string sectionName)
        {
            throw new NotImplementedException();
        }

        public Configuration OpenExeConfiguration(string exePath)
        {
            throw new NotImplementedException();
        }
        public Configuration OpenExeConfiguration(ConfigurationUserLevel userLevel)
        {
            throw new NotImplementedException();
        }

        public Configuration OpenMachineConfiguration()
        {
            throw new NotImplementedException();
        }

        public Configuration OpenMappedExeConfiguration(
            ExeConfigurationFileMap fileMap, ConfigurationUserLevel userLevel)
        {
            throw new NotImplementedException();
        }


        public Configuration OpenMappedMachineConfiguration(ConfigurationFileMap fileMap)
        {
            throw new NotImplementedException();
        }

        public void RefreshSection(string sectionName)
        {
            throw new NotImplementedException();
        }

        public System.Collections.Specialized.NameValueCollection AppSettings
        {
            get { throw new NotImplementedException(); }
        }


        public ConnectionStringSettingsCollection ConnectionStrings
        {
            get { throw new NotImplementedException(); }
        }
    }
}
