using System;
using System.Collections.Generic;

using System.Text;
using System.Web.UI;
using System.Web;
using ESP.Framework.Entity;
using ESP.Framework.BusinessLogic;
using ESP.Utilities;
using System.IO;
using ESP.Logging;


namespace ESP.Web.UI
{
    /// <summary>
    /// 页面基类
    /// </summary>
    public partial class PageBase : Page
    {
        #region Constants
        private const int USER_LOADED = 1;
        private const int WEBPAGE_LOADED = 2;
        private const int MODULE_LOADED = 4;
        private const int WEBSITE_LOADED = 8;
        #endregion

        #region private variables

        private WebPageInfo _WebPageInfo = null;
        private ModuleInfo _ModuleInfo = null;
        //private IList<DataAccessAuthorization.Entity.DataAccessAction> _WebPageDataAccessActionList = null;
        //private Dictionary<DataAccessAuthorization.Entity.DataAccessAction, IList<DataAccessAuthorization.Entity.DataAccessMember>> _DataAccessPermission = null;
        //private IList<DataAccessAuthorization.Entity.DataAccessAction> _DataAccessNeedToCheck = null;

        private int _loadingFlag = 0;
        private bool _SkipLogging = false;
        #endregion

        #region Associated Informations

        /// <summary>
        /// 当前用户的用户信息，如果当前用户为匿名用户，则为空引用(null)
        /// </summary>
        public UserInfo UserInfo
        {
            get
            {
                return UserManager.Get();
            }
        }
        /// <summary>
        /// 当前用户（特指员工）的完整信息
        /// 包含UserInfo、EmployeeInfo和EmployeePositionList
        /// </summary>
        public MemberInfo MemberInfo
        {
            get
            {
                return UserManager.GetMember(UserInfo);
            }
        }

        /// <summary>
        /// 当前用户的ID，如果用户为匿名用户，则为 0
        /// </summary>
        public int UserID
        {
            get
            {
                return UserInfo == null ? 0 : UserInfo.UserID;
            }
        }

        /// <summary>
        /// 当前站点的信息
        /// </summary>
        public WebSiteInfo WebSiteInfo
        {
            get
            {
                return WebSiteManager.Get();;
            }
        }

        /// <summary>
        /// 当前页面的信息
        /// </summary>
        public WebPageInfo WebPageInfo
        {
            get
            {
                if ((_loadingFlag & WEBPAGE_LOADED) == 0)
                {
                    string absolutePath = Request.Url.AbsolutePath;
                    string appPath = HttpRuntime.AppDomainAppVirtualPath;
                    int length = appPath.EndsWith("/") ? appPath.Length : appPath.Length + 1;
                    string appRelativePath = absolutePath.Substring(length);
                    _WebPageInfo = WebPageManager.GetByPath(this.WebSiteInfo.WebSiteID, appRelativePath);
                }

                return _WebPageInfo;
            }
        }

        /// <summary>
        /// 当前页面所属模块的信息
        /// </summary>
        public ModuleInfo ModuleInfo
        {
            get
            {
                if ((_loadingFlag & MODULE_LOADED) == 0)
                {
                    if (this.WebPageInfo != null)
                        _ModuleInfo = ModuleManager.Get(this.WebPageInfo.ModuleID);
                }
                return _ModuleInfo;
            }
        }

        ///// <summary>
        ///// 当前页面的权限设定
        ///// </summary>
        //public IList<DataAccessAuthorization.Entity.DataAccessAction> WebPageDataAccessActionList
        //{
        //    get
        //    {
        //        if (_WebPageDataAccessActionList == null)
        //        {
        //            return _WebPageDataAccessActionList = DataAccessAuthorization.BusinessLogic.WebPageDataAccess.GetWebPageDataAccessActionList(WebPageInfo.WebPageID);
        //        }
        //        else
        //        {
        //            return _WebPageDataAccessActionList;
        //        }
        //    }
        //}

        ///// <summary>
        ///// 当前用户拥有的数据权限
        ///// </summary>
        //public Dictionary<DataAccessAuthorization.Entity.DataAccessAction, IList<DataAccessAuthorization.Entity.DataAccessMember>> DataAccessPermission
        //{
        //    get
        //    {
        //        return _DataAccessPermission;
        //    }
        //}
        ///// <summary>
        ///// 需要在业务逻辑里面检查的数据权限
        ///// </summary>
        //public IList<DataAccessAuthorization.Entity.DataAccessAction> DataAccessNeedToCheck
        //{
        //    get
        //    {
        //        return _DataAccessNeedToCheck;
        //    }
        //}

        /// <summary>
        ///是否跳过日志记录
        ///因为有些系统操作，不需要记录日志
        /// </summary>
        public bool SkipLogging
        {
            get { return _SkipLogging; }
            set { _SkipLogging = value; }
        }

        #endregion

        #region Permission Operations

        /// <summary>
        /// 判断当前用户是否拥有指定的站点级权限
        /// </summary>
        /// <param name="permission">权限名字</param>
        /// <returns>如果当前用户有指定的权限，返回true；否则返回false。</returns>
        public bool HasWebSitePermission(string permission)
        {
            return PermissionManager.HasWebSitePermission(permission, this.WebSiteInfo.WebSiteID, this.UserID);
        }

        /// <summary>
        /// 判断当前用户是否拥有指定的页面级权限
        /// </summary>
        /// <param name="permission">权限名字</param>
        /// <returns>如果当前用户有指定的权限，返回true；否则返回false。</returns>
        public bool HasWebPagePermission(string permission)
        {
            return PermissionManager.HasWebPagePermission(permission, this.WebPageInfo.ModuleID, this.UserID);
        }

        #endregion

        #region Data Bind Helper Methods

        /// <summary>
        /// 根据当前数据绑定上下文计算表达式的值，如果值为日期类型
        /// 且等于ESP.Framework.DataAccess.Utilities.NullValues，则返回空字符串
        /// </summary>
        /// <param name="expression">表达式</param>
        /// <returns>表达示的值</returns>
        protected string EvalX(string expression)
        {
            return this.EvalX(expression, null);
        }

        /// <summary>
        /// 根据当前数据绑定上下文计算表达式的值，如果值为日期类型
        /// 且等于ESP.Framework.DataAccess.Utilities.NullValues，则返回空字符串
        /// </summary>
        /// <param name="expression">表达式</param>
        /// <param name="format">格式化字符串</param>
        /// <returns>表达示的值</returns>
        protected string EvalX(string expression, string format)
        {
            object obj = DataBinder.Eval(this.GetDataItem(), expression);
            if (obj is DateTime)
            {
                DateTime dt = (DateTime)obj;
                if (dt <= ESP.Framework.DataAccess.Utilities.NullValues.DateTime)
                    return string.Empty;
            }

            return base.Eval(expression, format);
        }
        #endregion

        #region Script Helper Methods

        /// <summary>
        /// 输出脚本在客户端显示提示对话框并跳转到指定Url
        /// </summary>
        /// <param name="message">提示信息</param>
        /// <param name="redirectTo">目标Url</param>
        public void ShowCompleteMessage(string message, string redirectTo)
        {
            string script = "window.alert(\"" + JavascriptUtility.QuoteJScriptString(message, false) + "\"); \n"
                + "window.location.href = \"" + JavascriptUtility.QuoteJScriptString(redirectTo, false) + "\";";
            this.ClientScript.RegisterStartupScript(this.GetType(), "CompleteMessage" + DateTime.Now, script, true);
        }
        #endregion

        /// <summary>
        /// 默认错误处理
        /// </summary>
        /// <param name="e">默认事件参数</param>
        protected override void OnError(EventArgs e)
        {
            base.OnError(e);

            try
            {
                Exception ex = Server.GetLastError().GetBaseException();
                Logger.Add(ex.Message, "PageError", LogLevel.Error, ex);
            }
            catch
            {

            }
        }

        /// <summary>
        /// 页面初始化
        /// </summary>
        /// <param name="e">事件参数。</param>
        protected override void OnInit(EventArgs e)
        {
            if (!SkipLogging)
            {
                ESP.Logging.Logger.Add("页面被访问。", "PageAccess", LogLevel.Information);
            }
            base.OnInit(e);
        }

        /// <summary>
        /// 当前用户是否是管理员
        /// </summary>
        public bool IsAdministrator
        {
            get
            {
                int[] ids = RoleManager.GetUserRoleIDs(this.UserID);
                if (ids != null)
                    return Array.IndexOf<int>(ids, 1) >= 0;

                return false;
            }
        }

        /// <summary>
        /// 获取人员信息
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <returns>人员信息描述</returns>
        public static string GetUserInfo(int userId)
        {
            return GetUserInfo(userId, null);
        }


        /// <summary>
        /// 获取人员信息
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="auditDesc">审核备注</param>
        /// <returns>人员信息描述</returns>
        public static string GetUserInfo(int userId, string auditDesc)
        {
            if (userId <= 0)
                return string.Empty;

            ESP.Framework.Entity.EmployeeInfo employee = ESP.Framework.BusinessLogic.EmployeeManager.Get(userId);

            if (employee == null)
                return string.Empty;

            IList<EmployeePositionInfo> positions = DepartmentPositionManager.GetEmployeePositions(userId);
            StringBuilder sb = new StringBuilder();
            sb.Append("<ul>");
            sb.Append("<li>员工姓名：").Append(employee.FullNameCN).Append('[').Append(employee.Code).Append("]</li>");
            sb.Append("<li>员工帐号：").Append(employee.Username).Append("</li>");
            sb.Append("<li>移动电话：").Append(employee.MobilePhone).Append("</li>");
            sb.Append("<li>电子邮箱：").Append(employee.Email).Append("</li>");
            sb.Append("<li>公司电话：").Append(employee.Phone1).Append("</li>");
            sb.Append("<li>所属部门：");
            foreach (EmployeePositionInfo p in positions)
            {
                sb.Append(p.DepartmentName).Append(',');
            }
            sb.Length--;
            sb.Append("</li>");

            if (auditDesc != null)
            {
                sb.Append("<li>审批信息：").Append(auditDesc).Append("</li>");
            }

            sb.Append("</ul>");

            return sb.ToString();
        }

    }



}
