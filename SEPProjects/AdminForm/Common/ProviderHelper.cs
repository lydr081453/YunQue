using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AdminForm.Configuration;

namespace AdminForm.Common
{
    /// <summary>
    /// 用于创建指定的提供程序类实例
    /// </summary>
    /// <typeparam name="T">要获取的 Provider 的接口类型。</typeparam>
    public static class ProviderHelper<T> where T : class
    {
        private static object _lockObj = new object();
        private static T _provider = null;

        /// <summary>
        /// 获取 Provider 实例。
        /// </summary>
        public static T Instance
        {
            get
            {
                if (_provider != null)
                    return _provider;

                lock (_lockObj)
                {
                    if (_provider != null)
                        return _provider;

                    ESPConfigurationSection section = (ESPConfigurationSection)System.Configuration.ConfigurationManager.GetSection("ESP");
                    _provider = (T)Activator.CreateInstance(section.Providers[typeof(T)].Value);


                    return _provider;
                }
            }
        }
    }
}


