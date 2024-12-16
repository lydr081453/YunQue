using System;
using System.Collections.Generic;
using System.Text;
using System.Web.Caching;

using Portal.Data.Provider;
using Portal.Model;
using Portal.Common;
using ESP.Framework.Entity;

namespace Portal.Task
{
    public class WorkItemTask : BaseTask
    {
        /// <summary>
        /// 任务逻辑
        /// </summary>
        public override void Execute()
        {
           // Portal.Data.TestLogger.Log("WorkItemTask->Execute", string.Format("被调用"));
            try
            {
              //  Portal.Data.TestLogger.Log("WorkItemTask->Execute", string.Format("调用ESP.Framework.BusinessLogic.TaskItemManager.GetAllTaskItems函数"));
                IDictionary<int, IDictionary<string, IList<TaskItemInfo>>> list = ESP.Framework.BusinessLogic.TaskItemManager.GetAllTaskItems();
               // Portal.Data.TestLogger.Log("WorkItemTask->Execute", string.Format("调用ESP.Framework.BusinessLogic.TaskItemManager.GetAllTaskItems函数结束，数据量为：{0}", list.Count));
                System.Web.HttpRuntime.Cache.Remove(Global.WORK_ITEM_TASK_CACHE_KEY);
              //  Portal.Data.TestLogger.Log("WorkItemTask->Execute", string.Format("移除HttpRuntime缓存项"));
              //  ESP.Logging.Logger.Add("Cache removed");
                System.Web.HttpRuntime.Cache.Add(Global.WORK_ITEM_TASK_CACHE_KEY, list, null, DateTime.MaxValue, TimeSpan.Zero, CacheItemPriority.High, null);
              //  Portal.Data.TestLogger.Log("WorkItemTask->Execute", string.Format("添加HttpRuntime缓存项"));
              //  ESP.Logging.Logger.Add("Cache added");
            }
            catch (Exception e)
            {
                Portal.Data.TestLogger.Log("WorkItemTask->Execute", string.Format("调用失败，异常信息是：{0}", e.ToString()));
            }
        }
    }
}
