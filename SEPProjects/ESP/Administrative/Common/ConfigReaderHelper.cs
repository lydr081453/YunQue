using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Xml;

namespace ESP.Administrative.Common
{
    /// <summary>
    /// 配置文件信息读取类
    /// </summary>
    public class ConfigReaderHelper
    {
        private static ConfigReaderHelper configReaderHelper = new ConfigReaderHelper();
        private static XmlDocument document = null;
        private static Hashtable Elements = null;
        private ConfigReaderHelper()
        { 
        }

        /// <summary>
        /// 获得配置文件读取实例类
        /// </summary>
        /// <returns></returns>
        public static ConfigReaderHelper GetInstance(string serverPath)
        {
            if (document == null)
            {
                //F:\SEPProjects2.0\Administrative\AdministrativeWeb\Config.xml
                document = new XmlDocument();
                document.Load(serverPath + Constants.APPLICATION_CONFIGPATH);
                XmlNodeList nodeList = document.GetElementsByTagName("member");
                if (nodeList != null && nodeList.Count > 0)
                {
                    foreach (XmlNode node in nodeList)
                    {
                        Elements.Add(node.Attributes["name"].Value, node.Attributes["value"].Value);
                    }
                }
            }
            return configReaderHelper;
        }

        /// <summary>
        /// 获得系统管理员的编号
        /// </summary>
        /// <returns></returns>
        public string GetAdministratorId()
        {
            string id = "0";
            
            return id;
        }

        /// <summary>
        /// 获得默认的上班时间
        /// </summary>
        /// <returns></returns>
        public string GetDefaultGoWorkTime()
        {
            return Elements["DefaultGoWorkTime"].ToString();
        }

        /// <summary>
        /// 获得默认的下班时间
        /// </summary>
        /// <returns></returns>
        public string GetDefaultOffWorkTime()
        {
            return Elements["DefaultOffWorkTime"].ToString();
        }

        /// <summary>
        /// 获得总部编号
        /// </summary>
        /// <returns></returns>
        public int GetHeadOffice()
        {
            return int.Parse(Elements["DefaultOffWorkTime"].ToString().Trim());
        }

        /// <summary>
        /// 获得上海分公司编号
        /// </summary>
        /// <returns></returns>
        public int GetShanghaiOffice()
        {
            return int.Parse(Elements["ShanghaiOffice"].ToString().Trim());
        }

        /// <summary>
        /// 获得广州分公司编号
        /// </summary>
        /// <returns></returns>
        public int GetGuangzhouOffice()
        {
            return int.Parse(Elements["GuangzhouOffice"].ToString().Trim());
        }

        /// <summary>
        /// 获得成都分公司编号
        /// </summary>
        /// <returns></returns>
        public int GetChengduOffice()
        {
            return int.Parse(Elements["ChengduOffice"].ToString().Trim());
        }
    }
}
