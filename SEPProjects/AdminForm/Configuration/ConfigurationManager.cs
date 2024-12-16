using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace AdminForm.Configuration
{
    public static class ConfigurationManager
    {
        private static ESPConfigurationSection _section = null;
        

        static ConfigurationManager()
        {
            _section = (ESPConfigurationSection)System.Configuration.ConfigurationManager.GetSection("esp");
        }

        public static string ConnectionStringName
        {
            get
            {
                return _section == null ? null : _section.ConnectionString;
            }
        }

        public static byte[] EncryptionKey
        {
            get
            {
                return _section.Security.EncryptionKey;
            }
        }

        public static ProviderSettingsCollection Providers
        {
            get
            {
                return _section.Providers;
            }
        }

        public static SecuritySettings Security
        {
            get
            {
                return _section.Security;
            }
        }
    }
}
