using System;
using System.Collections.Generic;
using System.Text;

namespace Portal.Core
{
    public class Page : ESP.Web.UI.PageBase
    {
        /// <summary>
        /// 获取缓存的EmployeeList对象，这个属性将会被移动到PageBase中
        /// </summary>
        public IDictionary<int, ESP.Framework.Entity.EmployeeInfo> CachedEmployeeList
        {
            get
            {
                if (System.Web.HttpRuntime.Cache[Common.Global.EMPLOYEES_IDICTIONARY_CACHE_KEY] == null)
                {
                    return new Dictionary<int, ESP.Framework.Entity.EmployeeInfo>();
                }
                return System.Web.HttpRuntime.Cache[Common.Global.EMPLOYEES_IDICTIONARY_CACHE_KEY] as IDictionary<int, ESP.Framework.Entity.EmployeeInfo>;
            }
        }

        /// <summary>
        /// 获取最近来访者的信息
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public List<ESP.Framework.Entity.EmployeeInfo> VisitorsInfo
        {
            get
            {
                List<ESP.Framework.Entity.EmployeeInfo> list = new List<ESP.Framework.Entity.EmployeeInfo>();
                if (IsUserSpace && (Interviewee > 0))
                {
                    if (CachedEmployeeList.Count > 0)
                    {
                        List<int> visitors = Web.GetMyVisitor(Interviewee);
                        for (int i = 0; i < visitors.Count; ++i)
                        {
                            if (CachedEmployeeList.ContainsKey(visitors[i]))
                            {
                                list.Add(CachedEmployeeList[visitors[i]]);
                            }
                        }
                    }
                }
                return list;
            }
        }
        /// <summary>
        /// 被访问者，如果没有这个信息，则返回-1
        /// </summary>
        public int Interviewee
        {
            get
            {
                int userid = -1;
                if (Request["userid"] != null)
                {
                    int.TryParse(Request["userid"], out userid);
                }
                return userid;
            }
        }

        /// <summary>
        /// 根据配置文件确认当前页面是否属于用户的空间（现在写定为user.aspx）
        /// </summary>
        public bool IsUserSpace
        {
            get
            {
                return (Request.Url.PathAndQuery.ToLower().IndexOf("user.aspx") > 0);
            }
        }

        /// <summary>
        /// 初始化页面
        /// </summary>
        /// <param name="e"></param>
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            Web.Visit(this, UserID);
        }
    }
}
