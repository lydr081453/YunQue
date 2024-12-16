using System;
using System.Collections.Generic;
using System.Text;
using ESP.Framework.DataAccess;
using ESP.Framework.Entity;

namespace ESP.Framework.BusinessLogic
{
    /// <summary>
    /// 系统设置控制类
    /// </summary>
    public static class SettingManager
    {
        #region Private Memebers
        private static ISettingsDataProvider GetProvider()
        {
            return ESP.Configuration.ProviderHelper<ISettingsDataProvider>.Instance;
        }

        #endregion

        /// <summary>
        /// 获取系统设置信息
        /// </summary>
        /// <returns>系统设置信息对象</returns>
        public static SettingsInfo Get()
        {
            IList<ESP.Framework.Entity.SettingInfo> list = GetSettings(ESP.Configuration.ConfigurationManager.WebSiteID, true);
            SettingsInfo settings = new SettingsInfo(list);
            return settings;
        }


        /// <summary>
        /// 获取站点的所有设置
        /// </summary>
        /// <param name="webSiteId">设置所属的站点ID，如果为0表示返回公共设置</param>
        /// <param name="includeSystemSettings">如果站点不为0， 该参数指定是否同时返回公共设置</param>
        /// <returns>站点的所有设置项</returns>
        public static IList<ESP.Framework.Entity.SettingInfo> GetSettings(int webSiteId, bool includeSystemSettings)
        {
            return GetProvider().GetSettings(webSiteId, includeSystemSettings);
        }

        /// <summary>
        /// 返回站点的所有设置，包括公共设置
        /// </summary>
        /// <param name="webSiteId"></param>
        /// <returns></returns>
        public static IList<ESP.Framework.Entity.SettingInfo> GetSettings(int webSiteId)
        {
            return GetSettings(webSiteId);
        }




        /// <summary>
        /// 获取站点指定名称的设置项
        /// </summary>
        /// <param name="webSiteId">设置所属的站点ID，如果为0表示返回公共设置</param>
        /// <param name="settingName">设置的名称</param>
        /// <param name="includeSystemSettings">如果站点不为0， 该参数指定是否同时返回公共设置</param>
        /// <returns>站点指定名称的设置项</returns>
        public static ESP.Framework.Entity.SettingInfo GetSetting(int webSiteId, string settingName, bool includeSystemSettings)
        {
            return GetProvider().GetSetting(webSiteId, settingName, includeSystemSettings);
        }


        /// <summary>
        /// 添加设置定义
        /// </summary>
        /// <param name="definition">设置定义</param>
        /// <return>新定义的标识ID</return>
        public static int AddDefinition(SettingDefinitionInfo definition)
        {
            return GetProvider().AddDefinition(definition);
        }

        /// <summary>
        /// 获取站点的所有设置定义
        /// </summary>
        /// <param name="webSiteId">站点ID</param>
        /// <returns>所有设置定义的列表</returns>
        /// <remarks>
        /// 返回值中包括公共设置定义中可重载的定义。
        /// </remarks>
        public static IList<SettingDefinitionInfo> GetDefinitions(int webSiteId)
        {
            return GetProvider().GetDefinitions(webSiteId);
        }


        /// <summary>
        /// 保存设置
        /// </summary>
        /// <param name="value">设置项信息</param>
        /// <param name="webSiteId">设置所属的站点的ID，如果为0， 则表示是公共设置</param>
        public static void SaveSetting(SettingInfo value, int webSiteId)
        {
            GetProvider().SaveSetting(value, webSiteId);
        }
    }
}
