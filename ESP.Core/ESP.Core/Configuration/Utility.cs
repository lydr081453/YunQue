using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Configuration;

namespace ESP.Configuration
{
    /// <summary>
    /// 配置辅助类
    /// </summary>
    public static class Utility
    {
        /// <summary>
        /// 获取配置节中指定名称的属性的值。
        /// </summary>
        /// <param name="section">配置节。</param>
        /// <param name="name">属性名称。</param>
        /// <param name="isRequired">
        /// 是否不可为空。如果要访问的属性的值为空字符串或空引用
        /// 且 isRequired 参数为 true，则该方法引发
        /// <see cref="System.Configuration.ConfigurationErrorsException"/>
        /// 类型的异常。
        /// </param>
        /// <returns>属性的值。</returns>
        /// <exception cref="System.Configuration.ConfigurationErrorsException">
        /// 属性的值为空字符串或空引用，且 isRequired 参数为 true。
        /// </exception>
        public static string GetXmlStringAtribute(XmlNode section, string name, bool isRequired)
        {
            XmlAttribute attr = section.Attributes[name];
            if (attr == null || string.IsNullOrEmpty(attr.Value))
            {
                if (isRequired)
                    throw new ConfigurationErrorsException("'" + name + "' attribute is required.", section);
                return null;
            }

            return attr.Value;
        }
    }
}
