using System.Collections;
using System.Collections.Specialized;

namespace ESP.Configuration
{
    internal class ESPConfigurationSection
    {
        internal string CommonConfig { get; set; }
        internal int WebSiteID { get; set; }
        internal int Version { get; set; }
        internal System.Xml.XmlNode Section { get; set; }
    }
}
