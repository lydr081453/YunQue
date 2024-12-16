using System;
using System.Collections.Generic;
using System.Text;

namespace ESP.Framework.Entity
{
    /// <summary>
    /// 系统设置信息
    /// </summary>
    public class SettingsInfo
    {
        /// <summary>
        /// 根据 SettingInfo 列表构造一个当前类对象
        /// </summary>
        /// <param name="settings"></param>
        public SettingsInfo(IList<SettingInfo> settings)
        {
            Items = new Dictionary<string, SettingInfo>();
            foreach (SettingInfo setting in settings)
            {
                //if (setting.WebSiteID == 0)
                //{
                //    if (Items.ContainsKey(setting.SettingName))
                //        continue;
                //}

                Items[setting.SettingName] = setting;
            }
        }

        /// <summary>
        /// 获取指定名称的设置的值，并以 T 类型返回
        /// </summary>
        /// <typeparam name="T">返回类型</typeparam>
        /// <param name="name">设置名称</param>
        /// <returns>指定名称的设置的值</returns>
        /// <exception cref="System.InvalidCastException">
        /// 设置的值无法转换为指定的 T 类型
        /// </exception>
        public T Value<T>(string name)
        {
            SettingInfo info;
            if (Items.TryGetValue(name, out info))
            {
                return info.GetValue<T>();
            }
            return default(T);
        }

        /// <summary>
        /// 所有设置项的 名称-设置 字典
        /// </summary>
        public IDictionary<string, SettingInfo> Items { get; private set; }

        /// <summary>
        /// Portal站点的ID
        /// </summary>
        public int PortalWebSite { get { return Value<int>("PortalWebSite"); } }

        /// <summary>
        /// 邮件服务器地址
        /// </summary>
        public string SmtpServer { get { return Value<string>("SmtpServer"); } }

        /// <summary>
        /// 邮件服务器端口
        /// </summary>
        public int SmtpServerPort { get { return Value<int>("SmtpServerPort"); } }

        /// <summary>
        /// 邮件服务器用户名
        /// </summary>
        public string SmtpUsername { get { return Value<string>("SmtpUsername"); } }
        /// <summary>
        /// 邮件服务器用户密码
        /// </summary>
        public string SmtpPassword { get { return Value<string>("SmtpPassword"); } }

        /// <summary>
        /// 系统邮件地址
        /// </summary>
        public string MailFrom { get { return Value<string>("MailFrom"); } }

        /// <summary>
        /// 登录页面ID
        /// </summary>
        public int PassportWebSite { get { return Value<int>("PassportWebSite"); } }

        ///// <summary>
        ///// 三重DES加密密钥
        ///// </summary>
        //public string EncryptionKey { get { return Value<string>("EncryptionKey"); } }


        /// <summary>
        /// AES加密密钥
        /// </summary>
        public string AESKey { get { return Value<string>("AESKey"); } }
             

        /// <summary>
        /// 主域
        /// </summary>
        public string TopDomain { get { return Value<string>("TopDomain"); } }

        /// <summary>
        /// 员工编号生成模式
        /// </summary>
        /// <remarks>
        /// 可以包含除“{”、“}”之外的任意常量字符，和以“{”与“}”定界的替换令牌
        /// 如 A-{auto:0000} 指定的员工编号模式为 A-0000, A-0001, A-0002
        /// </remarks>
        public string EmployeeCodePattern { get { return Value<string>("EmployeeCodePattern"); } }
    }
}
