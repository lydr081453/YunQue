using System;
using System.Collections.Generic;
using System.Text;

namespace Portal.Task
{
    public class EmployeeTask : BaseTask
    {
        /// <summary>
        /// 任务逻辑
        /// </summary>
        public override void Execute()
        {
#if DEBUG
            System.Diagnostics.Debug.WriteLine("EmployeeTask");
#endif
            IDictionary<int, ESP.Framework.Entity.EmployeeInfo> dict = ESP.Framework.BusinessLogic.EmployeeManager.GetDictionary();
            // 将消息格式进行一下处理，将@用户名加上链接
            System.Web.HttpRuntime.Cache.Remove(Portal.Common.Global.EMPLOYEES_IDICTIONARY_CACHE_KEY);
            System.Web.HttpRuntime.Cache.Add(Portal.Common.Global.EMPLOYEES_IDICTIONARY_CACHE_KEY, dict, null, DateTime.MaxValue, TimeSpan.Zero, System.Web.Caching.CacheItemPriority.High, null);
        }
    }
}
