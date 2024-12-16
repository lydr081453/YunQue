using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using ESP.Framework.BusinessLogic;
using ESP.Configuration;
using ESP.Framework.Entity;

namespace SEPAdmin
{
    public partial class _Default : ESP.Web.UI.PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                #region HR系统
                int usercode = 63;
                if (UserInfo != null)
                {
                    int[] usercodes = RoleManager.GetUserRoleIDs(UserInfo.UserID);

                    int admin = int.Parse(ESP.Configuration.ConfigurationManager.SafeAppSettings["AdminRoles"]);

                    if (usercodes.Length > 0)
                    {
                        for (int i = 0; i < usercodes.Length; i++)
                        {
                            if (admin == usercodes[i])
                            {
                                usercode = 0;
                                break;
                            }
                        }
                    }
                }
                tb.InnerHtml = BindTree(usercode);
                #endregion

                if (UserInfo == null)
                    lblCaption.Text = "匿名用户";
                else
                    lblCaption.Text = UserInfo.FullNameCN;

            }
        }


        #region 管理员系统树
        private string GetKey(ModuleInfo m)
        {
            return (m.WebSiteID + "x" + m.ModuleID).ToString();
        }

        private void BindTreeToView(ESP.Tree<ModuleInfo> tree, TreeNodeCollection treeNodeCollection)
        {
            if (tree.Value.NodeType == ModuleType.Module && tree.Value.DefaultPageID == 0)
                return;

            string url = NavigateToModule2(tree.Value);
            TreeNode viewNode = new TreeNode(tree.Value.ModuleName, GetKey(tree.Value), null, NavigateToModule2(tree.Value), "modify");
            treeNodeCollection.Add(viewNode);

            if (IsFocusNode(tree.Value))
                viewNode.Selected = true;

            if (string.IsNullOrEmpty(url))
                viewNode.SelectAction = TreeNodeSelectAction.Expand;

            for (int i = 0; i < tree.Count; i++)
            {
                ESP.Tree<ModuleInfo> child = tree[i];
                BindTreeToView(child, viewNode.ChildNodes);
            }
        }


        private string NavigateToModule2(ModuleInfo module)
        {
            if (string.IsNullOrEmpty(module.DefaultPageUrl))
                return null;

            return "~/" + module.DefaultPageUrl;
        }
        private bool IsFocusNode(ModuleInfo module)
        {
            if (!(this.Page is ESP.Web.UI.PageBase))
                return false;
            WebPageInfo info = ((ESP.Web.UI.PageBase)this.Page).WebPageInfo;
            if (info == null)
                return false;

            return info.ModuleID == module.ModuleID;
        }
        #endregion
        #region HR系统树
        private string BindTree(int parentId)
        {
            StringBuilder strTABContent = new StringBuilder();
            int n = 0;
            IList<ModuleInfo> list = ModuleManager.GetByUser(ConfigurationManager.WebSiteID, UserManager.GetCurrentUserID());

            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].ParentID == parentId)
                {
                    //strTABContent.Append("<tr>");
                    //if (n > 0)
                    //{
                    //    strTABContent.Append("<td  background='../../images/sys_theme12_62.gif' style=' padding-left:30px;cursor:pointer' height='39' ");
                    //}
                    //else
                    //{
                    //    strTABContent.Append("<td  align='left' background='../../images/sys_theme12_49.gif' style='padding-left:30px;cursor:pointer' height='39' ");
                    //}
                    //strTABContent.Append(" onclick=\"showdiv('div" + n.ToString() + "');\">");
                    //strTABContent.Append("&nbsp;<strong>" + list[i].ModuleName + "</strong>");
                    //strTABContent.Append("</td></tr>");
                    //strTABContent.Append("<tr><td align='left' class='left_menu' style='padding: 5px 0 5px 5px;'>");
                    //strTABContent.Append("<div id='div" + n.ToString() + "' style=\"display:none\"><table width='100%' border='0' cellspacing='10' cellpadding='0'>");
                    //strTABContent.Append(BindChildTree(Convert.ToInt32(list[i].ModuleID), list, ""));
                    //strTABContent.Append("</table></div></td></tr>");


                    strTABContent.Append("<tr>");
                    if (n > 0)
                        strTABContent.Append("<td id='parent" + n.ToString() + "' class='menu-normal'>");
                    else
                        strTABContent.Append("<td id='parent" + n.ToString() + "' class='menu-orag'>");
                    strTABContent.Append("<span style='line-height: 24px;cursor:pointer;' onclick=\"showdiv('" + n.ToString() + "');\" >" + list[i].ModuleName + "</span>");
                    strTABContent.Append("</td>");
                    strTABContent.Append("</tr>");
                    strTABContent.Append("<tr id='sub" + n.ToString() + "' class='hideSub'><td class='menu-normal-se'>" + BindChildTree(Convert.ToInt32(list[i].ModuleID), list, "") + "</td></tr>");
                    n++;
                }
            }

            return strTABContent.ToString();
        }

        private string BindChildTree(int parentId, IList<ModuleInfo> list, string prefix)
        {
            StringBuilder strTABContent = new StringBuilder();
            //  IList<ModuleInfo> list = ModuleController.GetByUser(ConfigurationManager.WebSiteID, UserController.GetCurrentUserID());

            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].ParentID == parentId)
                {
                    //strTABContent.Append("<tr><td class='treelist' style=' padding:0px 0 5px 15px;'>");
                    strTABContent.Append("&nbsp;<a href='javascript:void(0);' target='modify' class='outA' id='a" + parentId + "_" + i.ToString() + "'  onclick=\"aSelected('" + parentId + "_" + i.ToString() + "','" + NavigateToModule(list[i]) + "');\">" + prefix + list[i].ModuleName + "</a></br>");
                    //strTABContent.Append("&nbsp;<a href=\"" + NavigateToModule(list[i]) + "\" target='modify' class='outA'  onclick='aSelected(this);'>" + prefix + list[i].ModuleName + "</a></br>");
                    //strTABContent.Append("</td></tr>");
                    strTABContent.Append(BindChildTree(Convert.ToInt32(list[i].ModuleID), list, "&nbsp;&nbsp;&gt;&nbsp;&nbsp;"));
                }
            }

            return strTABContent.ToString();
        }

        private string NavigateToModule(ModuleInfo module)
        {
            if (string.IsNullOrEmpty(module.DefaultPageUrl))
                return "javascript:void(0);";

            return "/" + module.DefaultPageUrl;
        }
        #endregion

        protected string WorkSpaceUrl
        {
            get
            {
                ESP.HumanResource.Entity.EmployeeBaseInfo emp = ESP.HumanResource.BusinessLogic.EmployeeBaseManager.GetModel(CurrentUserID);
                if (emp.BaseInfoOK == false)
                {
                    return "/Management/UserManagement/EmpMgt.aspx";
                }
                string workSpaceUrl = Request.QueryString["contentUrl"];
                //if (workSpaceUrl != null)
                //{
                //    workSpaceUrl = workSpaceUrl.Trim().ToLower();
                //    if (workSpaceUrl.Length > 0 && workSpaceUrl[0] == '/')
                //        workSpaceUrl = workSpaceUrl.Substring(1);

                //    int index = workSpaceUrl.IndexOf('?');
                //    if (index == 0)
                //    {
                //        workSpaceUrl = string.Empty;
                //    }
                //    else if (index > 0)
                //    {
                //        workSpaceUrl = workSpaceUrl.Substring(0, index);
                //    }

                //}
                if (workSpaceUrl == null || workSpaceUrl.Length == 0 || "default.aspx" == workSpaceUrl)
                    return "HR/default.aspx";
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
