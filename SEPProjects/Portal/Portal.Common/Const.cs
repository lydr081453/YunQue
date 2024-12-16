using System;
using System.Collections.Generic;
using System.Text;

namespace Portal.Common
{
    public partial class Global
    {
        /// <summary>
        /// 数据库数据提供者
        /// </summary>
        public const string PORTAL_DATABASE_PROVIDER = "PortalDatabaseProvider";
        /// <summary>
        /// 非数据库数据提供者，默认为Portal.Data.Web
        /// </summary>
        public const string PORTAL_OTHER_DATA_PROVIDER = "PortalOtherDataProvider";
        /// <summary>
        /// 任务时钟触发周期
        /// 所有任务的执行周期都要以10秒为单位
        /// 设定Period值可以决定多少个10秒执行一次
        /// </summary>
        public const int TASK_TIMER_PERIOD = 10 * 1000;
        /// <summary>
        /// 任务配置文件
        /// </summary>
        public const string TASK_CONFIG_FILE_NAME = "task.config";
        /// <summary>
        /// 停止任务时的等待时间，10秒，10000毫秒
        /// </summary>
        public const int TASK_STOP_WAIT_TIME = 10000;
        /// <summary>
        /// 停止任务的等待次数，等待超时相当于 TASK_STOP_WAIT_TIME Ｘ TASK_STOP_WAIT_TIME
        /// </summary>
        public const int TASK_STOP_MAX_WAIT_TIMES = 10;

        #region Cache Key
        /// <summary>
        /// Twitter信息的公共时间线缓存Key
        /// 缓存20个最新的Twitter信息
        /// </summary>
        public const string TWITTER_PUBLIC_TIME_LINE_KEY = "{D004AEE6-6B51-45ba-9BA5-A31B6A5DCA5F}";
        /// <summary>
        /// Employee信息
        /// </summary>
        public const string EMPLOYEES_IDICTIONARY_CACHE_KEY = "{54F32CBA-7735-4d6e-B20B-AC1E424AE812}";
        /// <summary>
        /// 用户和他的访问者们
        /// </summary>
        public const string USER_AND_VISITOR_KEY = "{A7BF61B8-8786-4472-9EA8-5DE119E26F08}";
        /// <summary>
        /// 代办事宜的缓存Key
        /// </summary>
        public const string WORK_ITEM_TASK_CACHE_KEY = "{3878C2BE-42FA-45e9-871D-6761549185EA}";
        #endregion

        /// <summary>
        /// 存储最近到访的用户数量
        /// </summary>
        public const int RECENT_VISITOR_COUNT = 8;
        /// <summary>
        /// 用户头像存储的位置
        /// </summary>
        public const string USER_ICON_FOLDER = "/images/upload/";

        public const string USER_ICON_PATH = "F:/web/S-Portal";

        //public const string USER_ICON_PATH = "E:/Projects/2017/ShunyaInside2017VS/SEPProjects/Portal/Portal";
        /// <summary>
        /// 用户默认头像
        /// </summary>
        public const string USER_DEFAULT_ICON_FOLDER = "/images/default_userheadicon.gif";

        public static readonly string ConnectionStringSettings = "PortalEntities";
        //System.Configuration.ConfigurationManager.AppSettings["PortalEntitiesConnectionString"];
    }
}
