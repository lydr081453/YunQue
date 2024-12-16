using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESP.Framework.BusinessLogic
{
    /// <summary>
    /// 工作项控制类
    /// </summary>
    public static class TaskItemManager
    {
        private static ESP.Framework.DataAccess.ITaskItemDataProvider GetProvider()
        {
            return ESP.Configuration.ProviderHelper<ESP.Framework.DataAccess.ITaskItemDataProvider>.Instance;
        }

        /// <summary>
        /// 获取指定ID的用户的待办事宜列表
        /// 从Web缓存中
        /// </summary>
        /// <param name="key">缓存Key</param>
        /// <param name="userId">用户ID</param>
        /// <returns>待办事宜列表</returns>
        public static IDictionary<string, IList<ESP.Framework.Entity.TaskItemInfo>> GetTaskItems(string key, int userId)
        {
            IDictionary<string, IList<ESP.Framework.Entity.TaskItemInfo>> t = GetProvider().GetTaskItems(key, userId);
            //foreach (IList<ESP.Framework.Entity.TaskItemInfo> list in t.Values)
            //{
            //    foreach(ESP.Framework.Entity.TaskItemInfo ti in list)
            //    {
            //        ti.Url = "Default.aspx?contentUrl=" + System.Web.HttpContext.Current.Server.UrlEncode(ti.Url);
            //    }
            //}

            return t;
        }

        /// <summary>
        /// 获取所有人的所有代办事宜
        /// 存入Web缓存中
        /// </summary>
        /// <returns>待办事宜列表</returns>
        public static IDictionary<int, IDictionary<string, IList<ESP.Framework.Entity.TaskItemInfo>>> GetAllTaskItems()
        {
            IDictionary<int, IDictionary<string, IList<ESP.Framework.Entity.TaskItemInfo>>> t = GetProvider().GetAllTaskItems();
            //foreach (IList<ESP.Framework.Entity.TaskItemInfo> list in t.Values)
            //{
            //    foreach(ESP.Framework.Entity.TaskItemInfo ti in list)
            //    {
            //        ti.Url = "Default.aspx?contentUrl=" + System.Web.HttpContext.Current.Server.UrlEncode(ti.Url);
            //    }
            //}

            return t;
        }
    }

}
