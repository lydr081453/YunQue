using System.Collections;
using System.Collections.Specialized;
using System.Configuration;

namespace AdminForm.Configuration
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class ESPConfigurationSection : ConfigurationSection
    {
        private static ConfigurationPropertyCollection _Properties;

        private static readonly ConfigurationProperty _ConnectionStringProperty;
        private static readonly ConfigurationProperty _ProvidersProperty;
        private static readonly ConfigurationProperty _SecurityProperty;
        private static readonly ConfigurationProperty _WebSiteProperty;

        static ESPConfigurationSection()
        {
            _Properties = new ConfigurationPropertyCollection();

            _ConnectionStringProperty = new ConfigurationProperty("connectionString", typeof(string), null, ConfigurationPropertyOptions.IsRequired);
            _ProvidersProperty = new ConfigurationProperty("providers", typeof(ProviderSettingsCollection), new ProviderSettingsCollection());
            _SecurityProperty = new ConfigurationProperty("security", typeof(SecuritySettings), new SecuritySettings());

            _Properties.Add(_ConnectionStringProperty);
            _Properties.Add(_ProvidersProperty);
            _Properties.Add(_SecurityProperty);
            _Properties.Add(_WebSiteProperty);
        }


        /// <summary>
        /// 
        /// </summary>
        protected override ConfigurationPropertyCollection Properties
        {
            get
            {
                return _Properties;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public string ConnectionString
        {
            get
            {
                return (string)this["connectionString"];
            }
            set
            {
                this["connectionString"] = value;
            }
        }

        public ProviderSettingsCollection Providers
        {
            get
            {
                return (ProviderSettingsCollection)base[_ProvidersProperty];
            }
        }



        public SecuritySettings Security
        {
            get
            {
                return (SecuritySettings)base[_SecurityProperty];
            }
        }
    }
}
