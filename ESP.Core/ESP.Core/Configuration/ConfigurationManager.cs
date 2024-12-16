using System;
using System.Collections;
using System.Reflection;
using System.Xml;
using System.Text.RegularExpressions;
using ESP.Framework.Entity;
using SysConfigurationManager = System.Configuration.ConfigurationManager;
using SysConfiguration = System.Configuration.Configuration;
using ESP.Framework.BusinessLogic;
using System.Web;
using System.Collections.Generic;
using ESP.Framework;
using System.Configuration;
using System.Text;
using System.Globalization;
using System.Collections.Specialized;
using System.Threading;
using System.Web.Configuration;
using System.Data.EntityClient;

namespace ESP.Configuration
{
    /// <summary>
    /// 配置管理类
    /// </summary>
    public static class ConfigurationManager
    {
        #region Feilds


        //private static bool _isInitialized;
        private static int _webSiteId;
        private static string _commonConfig;
        private static ArrayList _providerDefinitions = null;
        private static NameValueCollection _properties = null;
        private static Hashtable _providers = new Hashtable();

        private static string _espConnectionStringName;
        //private static object _lockObj = new object();
        private static System.IO.FileSystemWatcher _configWatcher;


        private static string _entityFrameworkConnectionString;

        private static string _workflowTimerCallbackPage = null;

        private static Dictionary<string, string> _EntitySets = null;

        //internal static int Version { get; set; }

        //internal static bool _enabledLinq2Sql;
        //internal static List<string> _tableMappingFiles = new List<string>();

        #endregion

        /// <summary>
        /// 释放配置
        /// </summary>
        public static void Dispose()
        {
            if (_configWatcher != null)
                _configWatcher.Dispose();
        }
        /// <summary>
        /// 初始化配置
        /// </summary>
        public static void Create()
        {
            ESPConfigurationSection espConfig = SysConfigurationManager.GetSection("esp") as ESPConfigurationSection;
            _webSiteId = espConfig.WebSiteID;
            _commonConfig = espConfig.CommonConfig;
            //Version = espConfig.Version;

            _properties = new NameValueCollection();
            _providerDefinitions = new ArrayList();
            _providers = new Hashtable();
            HashSet<string> entityFrameworkMetadatas = new HashSet<string>();
            string entityFrameworkConnectionStringName;
            string entityFrameworkConnectionString;
            string entityFrameworkConnectionStringProviderName;


            if (espConfig.CommonConfig == null)
            {
                XmlNode section = espConfig.Section;
                LoadESPSettingsNode(section, _providerDefinitions, _properties);
                XmlNode entityFrameworkNode = section.SelectSingleNode("./entityFramework");
                LoadEntityFrameworkMetadatas(section, entityFrameworkMetadatas, out entityFrameworkConnectionStringName);
                entityFrameworkConnectionString = null;
                entityFrameworkConnectionStringProviderName = null;
                if (!string.IsNullOrEmpty(entityFrameworkConnectionStringName))
                {
                    ConnectionStringSettings settings = SysConfigurationManager.ConnectionStrings[entityFrameworkConnectionStringName];
                    if (settings != null)
                    {
                        entityFrameworkConnectionString = settings.ConnectionString;
                        entityFrameworkConnectionStringProviderName = settings.ProviderName;
                    }
                }
            }
            else
            {
                DateTime commonLastMod = System.IO.File.GetLastWriteTimeUtc(_commonConfig);
                DateTime appSetLastMod = DateTime.MinValue;
                DateTime constrLastMod = DateTime.MinValue;

                string appSettingsFile = HttpRuntime.AppDomainAppPath + "\\appSettings.config";
                string connectionStringsFile = HttpRuntime.AppDomainAppPath + "\\connectionStrings.config";
                if (!System.IO.File.Exists(appSettingsFile))
                {
                    using (System.IO.StreamWriter w = new System.IO.StreamWriter(appSettingsFile))
                    {
                        w.WriteLine("<appSettings>");
                        w.WriteLine("</appSettings>");
                    }
                }
                else
                {
                    constrLastMod = System.IO.File.GetLastWriteTimeUtc(connectionStringsFile);
                }
                if (!System.IO.File.Exists(connectionStringsFile))
                {
                    using (System.IO.StreamWriter w = new System.IO.StreamWriter(connectionStringsFile))
                    {
                        w.WriteLine("<connectionStrings>");
                        w.WriteLine("</connectionStrings>");
                    }
                }
                else
                {
                    appSetLastMod = System.IO.File.GetLastWriteTimeUtc(appSettingsFile);
                }

                _configWatcher = new System.IO.FileSystemWatcher(System.IO.Path.GetDirectoryName(_commonConfig), System.IO.Path.GetFileName(_commonConfig));
                _configWatcher.Changed += new System.IO.FileSystemEventHandler(configWatcher_Changed);
                _configWatcher.Deleted += new System.IO.FileSystemEventHandler(configWatcher_Changed);
                _configWatcher.Created += new System.IO.FileSystemEventHandler(configWatcher_Changed);
                _configWatcher.Error += new System.IO.ErrorEventHandler(configWatcher_Error);
                _configWatcher.NotifyFilter = System.IO.NotifyFilters.LastWrite;
                _configWatcher.EnableRaisingEvents = true;


                XmlDocument xmlDoc = new System.Configuration.ConfigXmlDocument();
                xmlDoc.Load(_commonConfig);

                string webSiteId = _webSiteId.ToString(System.Globalization.CultureInfo.InvariantCulture);

                LoadESPSettings(xmlDoc, webSiteId, entityFrameworkMetadatas, out entityFrameworkConnectionStringName);



                if (commonLastMod > appSetLastMod || commonLastMod > constrLastMod)
                {
                    SysConfiguration configuration = WebConfigurationManager.OpenWebConfiguration("~/");
                    LoadConnectionStrings(xmlDoc, configuration, webSiteId, entityFrameworkConnectionStringName, out entityFrameworkConnectionString, out entityFrameworkConnectionStringProviderName);
                    LoadAppSettings(xmlDoc, configuration, webSiteId);
                    configuration.Save(ConfigurationSaveMode.Modified);
                }
                else
                {
                    if (entityFrameworkConnectionStringName != null)
                    {
                        var cs = System.Configuration.ConfigurationManager.ConnectionStrings[entityFrameworkConnectionStringName];
                        entityFrameworkConnectionString = cs.ConnectionString;
                        entityFrameworkConnectionStringProviderName = cs.ProviderName;
                    }
                    else
                    {
                        entityFrameworkConnectionString = null;
                        entityFrameworkConnectionStringProviderName = null;
                    }
                }
            }

            if (entityFrameworkMetadatas.Count > 0)
            {
                StringBuilder connectionString = new StringBuilder();
                connectionString.Append("metadata=");
                foreach (string metadata in entityFrameworkMetadatas)
                {
                    connectionString.Append(metadata).Append('|');
                }
                connectionString.Length--;
                connectionString.Append(";provider=").Append(entityFrameworkConnectionStringProviderName).Append(";provider connection string=\"").Append(entityFrameworkConnectionString).Append("\"");
                _entityFrameworkConnectionString = connectionString.ToString();

                ParseMetadatas();
            }

            InitializeProviderTable();
            _espConnectionStringName = _properties["connectionStringName"];
            //_isInitialized = true;
        }

        internal static Dictionary<string, string> EntitySets
        {
            get
            {
                return _EntitySets;
            }
        }

        private static void ParseMetadatas()
        {
            Dictionary<string, string> entitySets = new Dictionary<string, string>();
            using (EntityConnection connection = new EntityConnection(_entityFrameworkConnectionString))
            {
                var ws = connection.GetMetadataWorkspace();
                var col = ws.GetItems<System.Data.Metadata.Edm.EntityContainer>(System.Data.Metadata.Edm.DataSpace.CSpace);
                foreach (var ec in col)
                {
                    string entityContainerName = ec.Name;
                    foreach (var es in ec.BaseEntitySets)
                    {
                        if (es.BuiltInTypeKind == System.Data.Metadata.Edm.BuiltInTypeKind.EntitySet)
                        {
                            entitySets.Add(es.ElementType.FullName, entityContainerName + "." + es.Name);
                        }
                    }
                }
            }
            _EntitySets = entitySets;
        }

        #region 加载配置
        private static void LoadESPSettingsNode(XmlNode espNode, ArrayList providerDefinitions, NameValueCollection properties)
        {
            XmlNodeList providerNodes = espNode.SelectNodes("providers/add");

            foreach (XmlNode node in providerNodes)
            {
                string asm = Utility.GetXmlStringAtribute(node, "assembly", true);
                string pattern = Utility.GetXmlStringAtribute(node, "pattern", true);

                IDictionary<string, string> providerSettings = new Dictionary<string, string>(node.Attributes.Count);
                foreach (XmlAttribute attr in node.Attributes)
                {
                    providerSettings.Add(attr.Name, attr.Value);
                }

                providerDefinitions.Add(new Quaternary<string, string, IDictionary<string, string>, XmlNode>(asm, pattern, providerSettings, node));
            }

            foreach (XmlAttribute attr in espNode.Attributes)
            {
                properties[attr.Name] = attr.Value;
            }

        }

        private static void LoadEntityFrameworkMetadatas(XmlNode node, HashSet<string> entityFrameworkMetadatas, out string connectionString)
        {
            XmlNodeList addNodes = node.SelectNodes("metadatas/add");
            foreach (XmlNode n in addNodes)
            {
                string value = Utility.GetXmlStringAtribute(n, "value", true);
                entityFrameworkMetadatas.Add(value);
            }

            connectionString = Utility.GetXmlStringAtribute(node, "connectionString", false);
        }

        private static void LoadESPSettings(XmlDocument externalConfigurationDocument, string webSiteId, HashSet<string> entityFrameworkMetadatas, out string entityFrameworkConnectionStringName)
        {
            entityFrameworkConnectionStringName = null;

            XmlNode espNode = externalConfigurationDocument.DocumentElement.SelectSingleNode("./esp");
            if (espNode != null)
                LoadESPSettingsNode(espNode, _providerDefinitions, _properties);

            XmlNode webSiteESPNode = externalConfigurationDocument.DocumentElement.SelectSingleNode("webSite[@id=" + webSiteId + "]/esp");
            if (webSiteESPNode != null)
                LoadESPSettingsNode(webSiteESPNode, _providerDefinitions, _properties);


            XmlNode entityFrameworkNode = externalConfigurationDocument.DocumentElement.SelectSingleNode("./esp/entityFramework");
            if (entityFrameworkNode != null)
            {
                LoadEntityFrameworkMetadatas(entityFrameworkNode, entityFrameworkMetadatas, out entityFrameworkConnectionStringName);
            }
            XmlNode webSiteEntityFrameworkNode = externalConfigurationDocument.DocumentElement.SelectSingleNode("webSite[@id=" + webSiteId + "]/esp/entityFramework");
            if (webSiteEntityFrameworkNode != null)
            {
                LoadEntityFrameworkMetadatas(webSiteEntityFrameworkNode, entityFrameworkMetadatas, out entityFrameworkConnectionStringName);
            }


        }

        private static void LoadAppSettings(XmlDocument document, SysConfiguration configuration, string webSiteId)
        {
            KeyValueConfigurationCollection col = configuration.AppSettings.Settings;
            col.Clear();
            string xpath = "(.|webSite[@id=" + webSiteId + "])/appSettings/add";
            XmlNodeList appSettingNodes = document.DocumentElement.SelectNodes(xpath);
            foreach (XmlNode node in appSettingNodes)
            {
                string key = GetXmlNodeAttribute(node, "key");
                string value = GetXmlNodeAttribute(node, "value");
                KeyValueConfigurationElement element = col[key];
                if (element == null)
                {
                    element = new KeyValueConfigurationElement(key, value);
                    col.Add(element);
                }
                else
                {
                    element.Value = value;
                }
            }
        }

        private static void LoadConnectionStrings(XmlDocument document, System.Configuration.Configuration configuration, string webSiteId, string entityFrameworkConnectionStringName, out string entityFrameworkConnectionString, out string entityFrameworkConnectionStringProviderName)
        {
            entityFrameworkConnectionString = null;
            entityFrameworkConnectionStringProviderName = null;

            ConnectionStringSettingsCollection col = configuration.ConnectionStrings.ConnectionStrings;
            col.Clear();
            string xpath = "(.|webSite[@id=" + webSiteId + "])/connectionStrings/add";
            XmlNodeList connectionStringNodes = document.DocumentElement.SelectNodes(xpath);
            foreach (XmlNode node in connectionStringNodes)
            {
                string name = GetXmlNodeAttribute(node, "name");
                string connectionString = GetXmlNodeAttribute(node, "connectionString");
                string providerName = GetXmlNodeAttribute(node, "providerName");
                ConnectionStringSettings settings = col[name];
                if (settings == null)
                {
                    settings = new ConnectionStringSettings(name, connectionString, providerName);
                    col.Add(settings);
                }
                else
                {
                    settings.ConnectionString = connectionString;
                    settings.ProviderName = providerName;
                }
                if (name == entityFrameworkConnectionStringName)
                {
                    entityFrameworkConnectionStringProviderName = providerName;
                    entityFrameworkConnectionString = connectionString;
                }
            }
        }

        private static string GetXmlNodeAttribute(XmlNode node, string attribute)
        {
            XmlAttribute attr = node.Attributes[attribute];
            if (attr == null)
                return null;
            return attr.Value;
        }


        private static void configWatcher_Error(object sender, System.IO.ErrorEventArgs e)
        {
            ESP.Logging.Logger.Add("公共配置文件监听器错误。", "Configuration", ESP.Logging.LogLevel.Error, e.GetException());
        }

        private static void configWatcher_Changed(object sender, System.IO.FileSystemEventArgs e)
        {
            HttpRuntime.UnloadAppDomain();
        }
        #endregion
        /*
        #region 初始化配置设置(过时)
        private static void InitializeConfiguration()
        {
            if (ConfigurationManager.Version > 0)
                return;

            if (_isInitialized == true)
                return;

            lock (_lockObj)
            {
                if (_isInitialized == true)
                    return;

                ESPConfigurationSection espConfig = SysConfigurationManager.GetSection("esp") as ESPConfigurationSection;
                _webSiteId = espConfig.WebSiteID;
                _commonConfig = espConfig.CommonConfig;

                LoadCommonConfig();

                _isInitialized = true;
            }
        }


        private static void LoadCommonConfig()
        {

            XmlDocument xmlDoc = new System.Configuration.ConfigXmlDocument();
            xmlDoc.Load(_commonConfig);

            _properties = new NameValueCollection();
            _providerDefinitions = new ArrayList();
            _providers = new Hashtable();

            LoadExternalConnectionStrings(xmlDoc, _webSiteId);
            LoadExternalAppSettings(xmlDoc, _webSiteId);
            LoadExternalESPSettings(xmlDoc, _webSiteId);

            InitializeProviderTable();

            _espConnectionStringName = _properties["connectionStringName"];
        }



        private static void LoadExternalESPSettingsNode(XmlNode espNode, ArrayList providerDefinitions, NameValueCollection properties)
        {
            XmlNodeList providerNodes = espNode.SelectNodes("providers/add");

            foreach (XmlNode node in providerNodes)
            {
                string asm = Utility.GetXmlStringAtribute(node, "assembly", true);
                string pattern = Utility.GetXmlStringAtribute(node, "pattern", true);

                IDictionary<string, string> providerSettings = new Dictionary<string, string>(node.Attributes.Count + 1);
                foreach (XmlAttribute attr in node.Attributes)
                {
                    providerSettings.Add(attr.Name, attr.Value);
                }

                providerDefinitions.Add(new Quaternary<string, string, IDictionary<string, string>, XmlNode>(asm, pattern, providerSettings, node));
            }

            foreach (XmlAttribute attr in espNode.Attributes)
            {
                properties[attr.Name] = attr.Value;
            }

        }

        private static void LoadExternalESPSettings(XmlDocument externalConfigurationDocument, int webSiteId)
        {
            XmlNode espNode = externalConfigurationDocument.DocumentElement.SelectSingleNode("./esp");
            if (espNode != null)
                LoadExternalESPSettingsNode(espNode, _providerDefinitions, _properties);

            XmlNode webSiteESPNode = externalConfigurationDocument.DocumentElement.SelectSingleNode("webSite[@id=" + webSiteId.ToString(CultureInfo.InvariantCulture) + "]/esp");
            if (webSiteESPNode != null)
                LoadExternalESPSettingsNode(webSiteESPNode, _providerDefinitions, _properties);

        }

        private static void LoadExternalAppSettings(XmlDocument externalConfigurationDocument, int webSiteId)
        {
            string xpath = "(.|webSite[@id=" + webSiteId.ToString(System.Globalization.CultureInfo.InvariantCulture) + "])/appSettings/add";
            XmlNodeList appSettingsNodes = externalConfigurationDocument.DocumentElement.SelectNodes(xpath);
            StringBuilder appSettingsBuilder = new StringBuilder();
            appSettingsBuilder.Append("<appSettings>");
            foreach (XmlNode node in appSettingsNodes)
            {
                appSettingsBuilder.Append(node.OuterXml);
            }
            appSettingsBuilder.Append("</appSettings>");
            string appSettings = appSettingsBuilder.ToString();

            using (System.IO.StreamWriter writer = new System.IO.StreamWriter(System.IO.Path.Combine(HttpRuntime.AppDomainAppPath, "appSettings.config")))
            {
                writer.Write(appSettings);
            }

            System.Configuration.ConfigurationManager.RefreshSection("appSettings");

        }

        private static void LoadExternalConnectionStrings(XmlDocument externalConfigurationDocument, int webSiteId)
        {
            string xpath = "(.|webSite[@id=" + webSiteId.ToString(System.Globalization.CultureInfo.InvariantCulture) + "])/connectionStrings/add";
            XmlNodeList connectionStringNodes = externalConfigurationDocument.DocumentElement.SelectNodes(xpath);
            StringBuilder connectionStringsBuilder = new StringBuilder();
            connectionStringsBuilder.Append("<connectionStrings>");
            foreach (XmlNode node in connectionStringNodes)
            {
                connectionStringsBuilder.Append(node.OuterXml);
            }
            connectionStringsBuilder.Append("</connectionStrings>");
            string connectionStrings = connectionStringsBuilder.ToString();

            using (System.IO.StreamWriter writer = new System.IO.StreamWriter(System.IO.Path.Combine(HttpRuntime.AppDomainAppPath, "connectionStrings.config")))
            {
                writer.Write(connectionStrings);
            }

            System.Configuration.ConfigurationManager.RefreshSection("connectionStrings");
        }
        #endregion
        */

        private static void InitializeProviderTable()
        {
            foreach (Quaternary<string, string, IDictionary<string, string>, XmlNode> en in _providerDefinitions)
            {
                string asmName = en.First;
                string pattern = en.Second;
                IDictionary<string, string> props = en.Third;
                XmlNode configurationSection = en.Fourth;

                Regex regex = new Regex(pattern, RegexOptions.Compiled);

                Assembly asm;
                Type[] types;
                try
                {
                    asm = AppDomain.CurrentDomain.Load(asmName);
                    types = asm.GetTypes();
                }
                catch (ReflectionTypeLoadException ex)
                {
                    string s = string.Empty;
                    foreach (var lex in ex.LoaderExceptions)
                    {
                        s += lex.Message;
                    }
                    throw new Exception(s);
                }

                foreach (Type type in types)
                {
                    if (regex.IsMatch(type.FullName))
                    {
                        Type[] interfaces = type.GetInterfaces();
                        foreach (Type iface in interfaces)
                        {
                            if (iface.IsDefined(typeof(ProviderAttribute), false))
                            {
                                _providers.Add(iface, new Triplet<Type, Type, IDictionary<string, string>>(type, iface, props));
                            }
                        }
                    }
                }
            }
        }

        #region 私有方法
        private static SettingsInfo Settings
        {
            get
            {
                HttpContext context = HttpContext.Current;

                if (context != null)
                {
                    SettingsInfo settings = context.Items["sep_settings"] as SettingsInfo;
                    if (settings == null)
                    {
                        settings = SettingManager.Get();
                        context.Items["sep_settings"] = settings;
                    }

                    return settings;
                }

                return SettingManager.Get();
            }
        }


        private static object CreateProvider(Type type, IDictionary<string, string> properties)
        {
            ConstructorInfo[] cstrs = type.GetConstructors(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
            if (cstrs == null || cstrs.Length == 0)
                return null;

            if (properties == null)
                properties = new Dictionary<string, string>();

            ConstructorInfo cstr = cstrs[0];
            ParameterInfo[] paraInfos = cstr.GetParameters();
            object[] paras = new object[paraInfos.Length];

            try
            {
                for (int i = 0; i < paraInfos.Length; i++)
                {
                    ParameterInfo info = paraInfos[i];
                    object val = properties[info.Name];
                    if (val == null && info.ParameterType.IsValueType)
                    {
                        val = Activator.CreateInstance(info.ParameterType);
                    }
                    paras[i] = Convert.ChangeType(val, info.ParameterType);
                }

                return cstr.Invoke(paras);
            }
            catch
            {
                return null;
            }

        }

        #endregion

        #region Provider 辅助方法
        /// <summary>
        /// 创建指定的提供程序类实例
        /// </summary>
        /// <param name="providerType">提供程序接口类型</param>
        /// <returns>提供程序实例</returns>
        public static object CreateProvider(Type providerType)
        {
            if (providerType == null)
                return null;

            Triplet<Type, Type, IDictionary<string, string>> providerInfo =
                (Triplet<Type, Type, IDictionary<string, string>>)_providers[providerType];

            if (providerInfo == null)
                return null;

            Type implType = providerInfo.First;
            IDictionary<string, string> paras = providerInfo.Third;

            return CreateProvider(implType, paras);

        }

        /*
        /// <summary>
        /// 获取指定的提供程序实例类
        /// </summary>
        /// <param name="providerType">提供程序接口类型</param>
        /// <returns>提供程序实例</returns>
        public static object GetProvider(Type providerType)
        {
            if (providerType == null)
                return null;

            //InitializeConfiguration();

            Triplet<Type, Type, IDictionary<string, string>> providerInfo =
                (Triplet<Type, Type, IDictionary<string, string>>)_providers[providerType];

            if (providerInfo == null)
                return null;

            Type implType = providerInfo.First;
            IDictionary<string, string> paras = providerInfo.Third;

            return CreateProvider(implType, paras);

        }
        */
        #endregion

        /// <summary>
        /// ADO.Net Entity Framework 数据提供程序的连接字符串。
        /// </summary>
        public static string EntityFrameworkConnectionString
        {
            get
            {
                return _entityFrameworkConnectionString;
            }
        }

        /// <summary>
        /// 工作流定时服务回调入口页面。
        /// </summary>
        public static string WorkflowTimerCallbackPage
        {
            get
            {
                return _workflowTimerCallbackPage;
            }
        }

        /// <summary>
        /// 站点的ID
        /// </summary>
        public static int WebSiteID
        {
            get
            {
                //InitializeConfiguration();

                return _webSiteId;
            }
        }

        /// <summary>
        /// ESP 连接字符串
        /// </summary>
        public static string ConnectionString
        {
            get
            {
                return SafeConnectionStrings[ConnectionStringName].ConnectionString;
            }
        }

        /// <summary>
        /// ESP 连接字符串的名字
        /// </summary>
        public static string ConnectionStringName
        {
            get
            {
                //InitializeConfiguration();
                return _espConnectionStringName;
            }
        }

        /// <summary>
        /// 自定义配置项集合
        /// </summary>
        public static System.Collections.Specialized.NameValueCollection Items
        {
            get
            {
                //InitializeConfiguration();
                return _properties;
            }
        }

        #region Settings
        ///// <summary>
        ///// 站点用于加密的密钥
        ///// </summary>
        //public static string WebSiteKey
        //{
        //    get
        //    {
        //        return Settings.EncryptionKey;
        //    }
        //}

        /// <summary>
        /// 是否启用缓存
        /// </summary>
        public static bool IsCacheEnabled
        {
            get { return true; }
        }


        /// <summary>
        /// 员工代码生成模式
        /// </summary>
        /// <remarks>
        /// 可以包含除“{”、“}”之外的任意常量字符，和以“{”与“}”定界的替换令牌
        /// 如 A-{auto:0000} 指定的员工编号模式为 A-0000, A-0001, A-0002
        /// </remarks>
        public static string EmployeeCodePattern
        {
            get
            {
                return Settings.EmployeeCodePattern;
            }
        }

        #region User management settings

        private static string _usernameRegularExpression = @"[a-z]+[a-z0-9_\.\-]*";
        private static string _passwordStrengthRegularExpression = null;// "^(?=(.*[^a-zA-Z0-9]){3})(?=.{6,}).*$";
        private static string _emailRegularExpression = @"^[0-9a-z]+([_\-\+\.\'][0-9a-z]+)*@[0-9a-z]+([\-\.][0-9a-z]+)*\.[0-9a-z]+([\-\.][0-9a-z]+)*$";
        private static int _maxInvalidPasswordAttempts = 10;
        private static int _passwordAttemptWindow = 1440;
        private static bool _isUniqueEmailRequired = false;

        /// <summary>
        /// 用户名的验证表达式
        /// </summary>
        public static string UsernameRegularExpression
        {
            get { return ConfigurationManager._usernameRegularExpression; }
        }

        /// <summary>
        /// 邮件的验证表达式
        /// </summary>
        public static string EmailRegularExpression
        {
            get { return ConfigurationManager._emailRegularExpression; }
        }

        /// <summary>
        /// 密码的验证表达式
        /// </summary>
        public static string PasswordStrengthRegularExpression
        {
            get { return ConfigurationManager._passwordStrengthRegularExpression; }
        }

        /// <summary>
        /// 最大密码重试次数
        /// </summary>
        public static int MaxInvalidPasswordAttempts
        {
            get { return ConfigurationManager._maxInvalidPasswordAttempts; }
        }

        /// <summary>
        /// 密码重试间隔
        /// </summary>
        public static int PasswordAttemptWindow
        {
            get { return ConfigurationManager._passwordAttemptWindow; }
        }

        /// <summary>
        /// 邮件是否要求唯一
        /// </summary>
        public static bool IsUniqueEmailRequired
        {
            get { return ConfigurationManager._isUniqueEmailRequired; }
        }

        #endregion

        #endregion

        /// <summary>
        /// 对 System.Configuration.ConfigurationManager.AppSettings 的包装
        /// </summary>
        public class AppSettingsWrapper
        {
            /// <summary>
            /// 对 System.Configuration.ConfigurationManager.AppSettings 的包装
            /// </summary>
            /// <param name="key">设置的键</param>
            /// <returns>设置的值</returns>
            public string this[string key]
            {
                get
                {
                    //ConfigurationManager.InitializeConfiguration();
                    return System.Configuration.ConfigurationManager.AppSettings[key];
                }
            }
        }

        /// <summary>
        /// 对 System.Configuration.ConfigurationManager.ConnectionStrings 的包装
        /// </summary>
        public class ConnectionStringsWrapper
        {
            /// <summary>
            /// 对 System.Configuration.ConfigurationManager.ConnectionStrings 的包装
            /// </summary>
            /// <param name="name">连接字符串的名称</param>
            /// <returns>连接字符串</returns>
            public ConnectionStringSettings this[string name]
            {
                get
                {
                    //ConfigurationManager.InitializeConfiguration();
                    return System.Configuration.ConfigurationManager.ConnectionStrings[name];
                }
            }
        }


        /// <summary>
        /// 对 System.Configuration.ConfigurationManager.ConnectionStrings 的包装
        /// </summary>
        public static ConnectionStringsWrapper SafeConnectionStrings
        {
            get
            {
                return new ConnectionStringsWrapper();
            }
        }

        /// <summary>
        /// 对 System.Configuration.ConfigurationManager.AppSettings 的包装
        /// </summary>
        public static AppSettingsWrapper SafeAppSettings
        {
            get
            {
                return new AppSettingsWrapper();
            }
        }
    }

}
