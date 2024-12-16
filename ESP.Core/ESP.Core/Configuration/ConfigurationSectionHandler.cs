using System.Configuration;
using System.Xml;
using System.Collections.Generic;
using ESP.Framework;
using System.IO;
using System.Web;

namespace ESP.Configuration
{
    class ConfigurationSectionHandler : IConfigurationSectionHandler
    {
        private int GetWebSiteID(XmlNode section)
        {
            string value = Utility.GetXmlStringAtribute(section, "webSiteId", true);
            int webSiteId;
            if (!int.TryParse(value, out webSiteId) || webSiteId <= 0)
                throw new ConfigurationErrorsException("Invalid value of 'webSiteId' attribute.", section);

            return webSiteId;
        }

        private int GetVersion(XmlNode section)
        {
            string value = Utility.GetXmlStringAtribute(section, "version", false);
            if (value == null)
                return 0;

            int v;
            if (!int.TryParse(value, out v) || v<= 0)
                throw new ConfigurationErrorsException("Invalid value of 'version' attribute.", section);

            return v;
        }


        #region IConfigurationSectionHandler 成员

        public object Create(object parent, object configContext, System.Xml.XmlNode section)
        {
            ESPConfigurationSection espSection = new ESPConfigurationSection();

            espSection.WebSiteID = GetWebSiteID(section);
            espSection.Version = GetVersion(section);

            if (section.Attributes["commonConfig"] == null)
            {
                espSection.CommonConfig = null;
                espSection.Section = section;
                return espSection;
            }

            string file = Utility.GetXmlStringAtribute(section, "commonConfig", true);

            if (file.IndexOfAny(System.IO.Path.GetInvalidPathChars()) >= 0)
            {
                throw new ConfigurationErrorsException("Invalid commonConfig attribute.");
            }

            try
            {
                if (!Path.IsPathRooted(file))
                    file = Path.Combine(HttpRuntime.AppDomainAppPath, file);

                file = Path.GetFullPath(file);

                if (!File.Exists(file))
                {
                    throw new ConfigurationErrorsException("The file of commonConfig does not exist.");
                }
            }
            catch (System.Exception e)
            {
                throw new ConfigurationErrorsException("The file of commonConfig does not exist.", e);
            }

            espSection.CommonConfig = file;
            return espSection;
        }

        #endregion


    }
}
