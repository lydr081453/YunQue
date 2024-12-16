using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ESP.Framework.Entity;
using System.Text;

namespace Web
{
    public partial class _Default : ESP.Web.UI.PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ESP.HumanResource.Entity.EmployeeBaseInfo emp = ESP.HumanResource.BusinessLogic.EmployeeBaseManager.GetModel(UserID);
                if (emp.BaseInfoOK == false)
                {
                    Response.Redirect("http://xhr.shunyagroup.com/");
                }

                IList<ModuleInfo> list =
                    ESP.Framework.BusinessLogic.ModuleManager.GetByUser(ESP.Configuration.ConfigurationManager.WebSiteID, UserID);
                tb.InnerHtml = BindTree(list);

                if (UserInfo == null)
                    lblCaption.Text = "匿名用户";
                else
                    lblCaption.Text = UserInfo.FullNameCN;
            }
        }

         private string BindTree(IList<ModuleInfo> list)
        {
            StringBuilder strTABContent = new StringBuilder();
            int n = 0;
            if (list != null && list.Count > 0)
            {
                foreach (ESP.Framework.Entity.ModuleInfo m in list)
                {
                    if (m.NodeType == ESP.Framework.Entity.ModuleType.WebSite)
                        continue;

                    // 如果节点的级别是第一级的时候
                    if (m.NodeLevel == 1)
                    {
                        if (m.ModuleName.IndexOf("系统首页") == -1)
                        {

                            strTABContent.Append("<tr>");
                            if (n > 0)
                                strTABContent.Append("<td id='parent" + n.ToString() + "' class='menu-normal'>");
                            else
                                strTABContent.Append("<td id='parent" + n.ToString() + "' class='menu-orag'>");
                            strTABContent.Append("<span style='line-height: 24px;cursor:pointer;' onclick=\"showdiv('" + n.ToString() + "');\" >" + m.ModuleName + "</span>");
                            strTABContent.Append("</td>");
                            strTABContent.Append("</tr>");
                            strTABContent.Append("<tr id='sub" + n.ToString() + "' class='hideSub'><td class='menu-normal-se'>" + BindChildTree(Convert.ToInt32(m.ModuleID), list, "") + "</td></tr>");
                            n++;
                        }
                    }
                }
            }

            return strTABContent.ToString();
        }

        private string BindChildTree(int parentId, IList<ModuleInfo> list, string prefix)
        {
            StringBuilder strTABContent = new StringBuilder();

            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].ParentID == parentId)
                {
                    strTABContent.Append("&nbsp;<a href='javascript:void(0);' target='modify' class='outA' id='a" + parentId + "_" + i.ToString() + "'  onclick=\"aSelected('" + parentId + "_" + i.ToString() + "','" + NavigateToModule(list[i]) + "');\">" + prefix + list[i].ModuleName + "</a></br>");
                    strTABContent.Append(BindChildTree(Convert.ToInt32(list[i].ModuleID), list, "&nbsp;&nbsp;&gt;&nbsp;&nbsp;"));
                }
            }

            return strTABContent.ToString();
        }

        private string NavigateToModule(ModuleInfo module)
        {
            if (string.IsNullOrEmpty(module.DefaultPageUrl))
                return "javascript:void(0);";

            string root = HttpRuntime.AppDomainAppVirtualPath;
            if (root.Length == 0 || root[root.Length - 1] != '/')
                root += '/';

            return root + module.DefaultPageUrl;
        }




        protected string WorkSpaceUrl
        {
            get
            {
                string workSpaceUrl = Request.QueryString["contentUrl"];
                if (workSpaceUrl != null)
                {
                    workSpaceUrl = workSpaceUrl.Trim().ToLower();
                    if (workSpaceUrl.Length > 0 && workSpaceUrl[0] == '/')
                        workSpaceUrl = workSpaceUrl.Substring(1);
                }


                if (workSpaceUrl == null || workSpaceUrl.Length == 0 || "default.aspx" == workSpaceUrl || "default.aspx?" == workSpaceUrl)
                    return "include/page/defaultworkspace.aspx";
                return workSpaceUrl;
            }
        }

        protected void ImageButton_Click(object sender, EventArgs e)
        {
            string logout = ESP.Security.PassportAuthentication.GetLogoutUrl("/default.aspx");
            Response.Redirect(logout);

        }
    }
}
