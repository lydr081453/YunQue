using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Portal.WebSite
{
    public partial class RefreshWorkItem : System.Web.UI.Page
    {
        System.Threading.Thread t = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.UserHostAddress != "123.124.199.47") return;
            // 获得当前登陆用户的待处理事件集合
            t = new System.Threading.Thread(new System.Threading.ThreadStart(GetData));
            t.Start();
        }

        void GetData()
        {
            try
            {
                CachedWorkItemsRef.CachedWorkItemsClient client = new Portal.WebSite.CachedWorkItemsRef.CachedWorkItemsClient();
                Dictionary<int, Dictionary<string, Portal.WebSite.CachedWorkItemsRef.TaskItemInfo[]>> list = client.GetAll();
                System.Web.HttpRuntime.Cache.Remove(Portal.Common.Global.WORK_ITEM_TASK_CACHE_KEY);
                System.Web.HttpRuntime.Cache.Add(Portal.Common.Global.WORK_ITEM_TASK_CACHE_KEY, list, null, DateTime.MaxValue, TimeSpan.Zero, System.Web.Caching.CacheItemPriority.High, null);
            }
            catch (Exception e)
            {
                e.ToString();
            }
        }
    }
}
