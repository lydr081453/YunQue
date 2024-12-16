using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Data;
using System.Web;
using System.Web.UI.WebControls;
using ESP.Purchase.Common;
using ESP.Purchase.BusinessLogic;
using ESP.Compatible;
using ESP.Framework.Entity;

public partial class include_page_ModelTree : ESP.Web.UI.PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            IList<ModuleInfo> list =
                ESP.Framework.BusinessLogic.ModuleManager.GetByUser(ESP.Configuration.ConfigurationManager.WebSiteID, UserID);
            BindTree(list);
        }
    }

    private void BindTree(IList<ModuleInfo> list)
    {
        // 获得根路径
        string root = HttpRuntime.AppDomainAppVirtualPath;
        if (root.Length == 0 || root[root.Length - 1] != '/')
            root += '/';

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
                        ComponentArt.Web.UI.NavBarItem newItem = new ComponentArt.Web.UI.NavBarItem();
                        newItem.ClientTemplateId = "TopItemTemplate";
                        newItem.Value = "mail";
                        newItem.Text = m.ModuleName;
                        newItem.ID = m.ModuleID.ToString();
                        navBarModel.Items.Add(newItem);
                    }
                }
            }

            foreach (ESP.Framework.Entity.ModuleInfo m in list)
            {
                if (m.NodeType == ESP.Framework.Entity.ModuleType.WebSite)
                    continue;
                // 如果节点的级别是第一级的时候
                if (m.NodeLevel == 2)
                {
                    ComponentArt.Web.UI.NavBarItem parentItem = navBarModel.FindItemById(m.ParentID.ToString());
                    ComponentArt.Web.UI.NavBarItem newItem = new ComponentArt.Web.UI.NavBarItem();
                    newItem.ClientTemplateId = "SubItemTemplate";
                    newItem.Value = "mailbox";
                    newItem.Text = m.ModuleName;
                    newItem.ID = m.ModuleID.ToString();
                    newItem.Target = "modify";
                    newItem.NavigateUrl = root + m.DefaultPageUrl;
                    parentItem.Items.Add(newItem);
                }
            }
        }
    }
}