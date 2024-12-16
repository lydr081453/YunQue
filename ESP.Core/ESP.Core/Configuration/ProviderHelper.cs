using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESP.Configuration
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
        /// 以获取TProviderInterface类型提供程序实现类的实例
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

                    //if(ConfigurationManager.Version < 1)
                    //    _provider = (T)ESP.Configuration.ConfigurationManager.GetProvider(typeof(T));
                    //else
                    
                    _provider = (T)ESP.Configuration.ConfigurationManager.CreateProvider(typeof(T));

                    return _provider;
                }
            }
        }
    }

    /// <summary>
    /// 将接口标记为 Provider 接口。
    /// </summary>
    [AttributeUsage(AttributeTargets.Interface)]
    public sealed class ProviderAttribute : Attribute
    {
    }
}


