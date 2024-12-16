using System;
using System.Collections.Generic;
using System.Text;
using ESP.Framework.Entity;

namespace ESP.Framework.DataAccess
{
    /// <summary>
    /// 系统设置抽象数据访问接口
    /// </summary>
    [ESP.Configuration.Provider]
    public interface ISettingsDataProvider
    {
        /// <summary>
        /// 获取站点的所有设置
        /// </summary>
        /// <param name="webSiteId">设置所属的站点ID，如果为0表示返回公共设置</param>
        /// <param name="includeSystemSettings">如果站点不为0， 该参数指定是否同时返回公共设置</param>
        /// <returns>站点的所有设置项</returns>
        IList<ESP.Framework.Entity.SettingInfo> GetSettings(int webSiteId, bool includeSystemSettings);

        /// <summary>
        /// 获取站点指定名称的设置项
        /// </summary>
        /// <param name="webSiteId">设置所属的站点ID，如果为0表示返回公共设置</param>
        /// <param name="settingName">设置的名称</param>
        /// <param name="includeSystemSettings">如果站点不为0， 该参数指定是否同时返回公共设置</param>
        /// <returns>站点指定名称的设置项</returns>
        ESP.Framework.Entity.SettingInfo GetSetting(int webSiteId, string settingName, bool includeSystemSettings);

        /// <summary>
        /// 添加设置定义
        /// </summary>
        /// <param name="definition">设置定义</param>
        /// <return>新定义的标识ID</return>
        int AddDefinition(SettingDefinitionInfo definition);

        /// <summary>
        /// 获取站点的所有设置定义
        /// </summary>
        /// <param name="webSiteId">站点ID</param>
        /// <returns>所有设置定义的列表</returns>
        /// <remarks>
        /// 返回值中包括公共设置定义中可重载的定义。
        /// </remarks>
        IList<SettingDefinitionInfo> GetDefinitions(int webSiteId);


        /// <summary>
        /// 保存设置
        /// </summary>
        /// <param name="value">设置项信息</param>
        /// <param name="webSiteId">设置所属的站点的ID，如果为0， 则表示是公共设置</param>
        void SaveSetting(SettingInfo value, int webSiteId);
    }
}
