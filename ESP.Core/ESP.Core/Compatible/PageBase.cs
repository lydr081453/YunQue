using System;
using System.Collections.Generic;
using System.Text;
using ESP.Compatible;
using ESP.Framework.Entity;
using ESP.Utilities;
using System.Web;
using EspDepartmentManager = ESP.Framework.BusinessLogic.DepartmentManager;
using EspEmployeeManager = ESP.Framework.BusinessLogic.EmployeeManager;

namespace ESP.Web.UI
{
    public partial class PageBase : System.Web.UI.Page
    {
        #region Compatible

        /// <summary>
        /// 当前员工
        /// </summary>
        [Obsolete("仅用于向前兼容，请使用更新的 ESP.Web.UI.PageBase.UserInfo 代替。")]
        public Employee CurrentUser
        {
            get
            {
                if (this.UserID > 0)
                {
                    EmployeeInfo e = EspEmployeeManager.Get(this.UserID);
                    if (e != null)
                        return Employee.CreateFromEmployeeInfo(e);
                }
                return null;
            }
        }

        /// <summary>
        /// 当前用户ID
        /// </summary>
        [Obsolete("仅用于向前兼容，请使用更新的 ESP.Web.UI.PageBase.UserID 代替。")]
        public int CurrentUserID
        {
            get { return this.UserID; }
        }

        /// <summary>
        /// 当前用户登录名
        /// </summary>
        [Obsolete("仅用于向前兼容，请使用更新的 ESP.Web.UI.PageBase.UserInfo.Username 代替。")]
        public string CurrentUserCode
        {
            get { return this.UserInfo == null ? null : this.UserInfo.Username; }
        }

        /// <summary>
        /// 当前用户中文名
        /// </summary>
        [Obsolete("仅用于向前兼容，请使用更新的 ESP.Web.UI.PageBase.UserInfo.FullNameCN 代替。")]
        public string CurrentUserName
        {
            get { return this.UserInfo == null ? null : this.UserInfo.FullNameCN; }
        }

        /// <summary>
        /// 根据部门ID获取所有直接子部门
        /// </summary>
        /// <param name="parentId">部门ID</param>
        /// <returns>所有直接子部门</returns>
        [Obsolete("仅用于向前兼容，请使用更新的 ESP.BusinessControlling.EspDepartmentManager.GetChildren 代替。")]
        public List<Department> GetDepartmentListByParentID(int parentId)
        {
            //IList<Department> list = ESP.BusinessControlling.EspDepartmentManager.GetAll
            return DepartmentManager.GetByParent(parentId);
        }

        /// <summary>
        /// 获取指定ID的部门下所有叶子部门结点
        /// </summary>
        /// <param name="parentId">部门ID</param>
        /// <returns>所有叶子部门结点</returns>
        [Obsolete("仅用于向前兼容，请使用更新的 ESP.BusinessControlling.EspDepartmentManager.GetChildrenRecursion 代替。")]
        public List<Department> GetLeafChildDepartments(int parentId)
        {
            IList<DepartmentInfo> list = EspDepartmentManager.GetChildrenRecursion(parentId);
            if (list == null)
                return new List<Department>();

            List<Department> ret = new List<Department>(list.Count);
            foreach (DepartmentInfo d in list)
            {
                if(d.DepartmentLevel == 3)
                    ret.Add(new Department(d));
            }

            return ret;
        }

        //[Obsolete("仅用于向前兼容。")]
        //public PageSecurityMode SecurityMode { get; set; }

        /// <summary>
        /// 导航到显示信息页面
        /// </summary>
        /// <param name="title">标题</param>
        /// <param name="message">显示信息</param>
        /// <param name="backUrl">返回Url</param>
        /// <remarks>
        ///   信息显示结束后返回该功能首页
        /// </remarks>
        [Obsolete("仅用于向前兼容。")]
        protected void ShowMessage(string title, string message, string backUrl)
        {
            message = Server.UrlEncode(message);
            string url = ConfigManager.DefaultSiteErrorPage + "?message=" + message + "&title=" + title + "&backurl=" + this.WebSiteInfo.HttpRootUrl + backUrl;
            Response.Redirect(url, false);
        }

        #endregion
    }

    /// <summary>
    /// 页面安全模式
    /// </summary>
    [Obsolete("仅用于向前兼容。")]
    public enum PageSecurityMode
    {
        /// <summary>
        /// 关闭安全检查
        /// </summary>
        None
    }
}
