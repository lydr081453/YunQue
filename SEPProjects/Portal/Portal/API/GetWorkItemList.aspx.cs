using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Portal.WebSite.API
{
    public partial class GetWorkItemList : ESP.Web.UI.PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // 获得当前登陆用户的待处理事件集合
            try
            {
                if (Cache[Portal.Common.Global.WORK_ITEM_TASK_CACHE_KEY] == null) Response.Write(Newtonsoft.Json.JavaScriptConvert.SerializeObject(new List<string>()));
                Dictionary<int, Dictionary<string, Portal.WebSite.CachedWorkItemsRef.TaskItemInfo[]>> all = (Dictionary<int, Dictionary<string, Portal.WebSite.CachedWorkItemsRef.TaskItemInfo[]>>)System.Web.HttpRuntime.Cache[Portal.Common.Global.WORK_ITEM_TASK_CACHE_KEY];
                if (all.ContainsKey(this.UserID) || all.ContainsKey(CurrentUser.GetDepartmentIDs()[0]))
                {
                    IDictionary<string, CachedWorkItemsRef.TaskItemInfo[]> list = all[this.UserID];
                    if (list != null && list.Count > 0)
                    {
                        Response.Write(Newtonsoft.Json.JavaScriptConvert.SerializeObject(list));
                        return;
                    }
                }
                Response.Write(Newtonsoft.Json.JavaScriptConvert.SerializeObject(new List<string>()));
            }
            catch (System.Exception ex)
            {
                Response.Write(ESP.Utilities.JavascriptUtility.QuoteJScriptString(ex.Message + "\n" + ex.StackTrace, false, true));
            }
        }
    }
}
