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
    public class CostRecordTask : BaseTask
    {
        /// <summary>
        /// 任务逻辑
        /// </summary>
        public override void Execute()
        {
#if DEBUG
            System.Diagnostics.Debug.WriteLine("Cost Record Task");
#endif
            try
            {
                Portal.Data.TestLogger.CostRecordLog("cost record->Execute", "begin load");
                ESP.Finance.BusinessLogic.CostRecordManager.InsertCost();
                Portal.Data.TestLogger.CostRecordLog("cost record->Execute", "complete load");
            }
            catch (Exception ex)
            {
                Portal.Data.TestLogger.CostRecordLog("cost record->Execute", string.Format("调用失败:{0}", ex.ToString()+"#"+ex.StackTrace));
            }
        }
    }
}
