using System;
using System.Xml;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using ESP.Compatible;
using ESP.Framework.BusinessLogic;
using ESP.Framework.Entity;
using System.Collections.Generic;
using SEPConfigurationManager = ESP.Configuration.ConfigurationManager;

public partial class include_page_ModelTree : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            IList<ModuleInfo> modules = ModuleManager.GetByUser(SEPConfigurationManager.WebSiteID, ESP.Framework.BusinessLogic.UserManager.GetCurrentUserID());
            string root = HttpRuntime.AppDomainAppVirtualPath;
            if (root.Length == 0 || root[root.Length - 1] != '/')
                root += '/';
            BindTree(stv1.Nodes, modules, 0, root);
            stv1.CollapseAll();
        }
    }

    void BindTree(TreeNodeCollection nds, IList<ModuleInfo> modules, int parentId, string urlRoot)
    {
        TreeNode tn = null;
        foreach (ModuleInfo m in modules)
        {
            if (m.ParentID != parentId)
                continue;

            string url = (m.NodeType == ModuleType.Module) ? urlRoot + m.DefaultPageUrl : "javascript:void(0);";
            string target = (m.NodeType == ModuleType.Module) ? "modify" : "_self";

            tn = new TreeNode(m.ModuleName, m.ModuleID.ToString(), null, url, target);
            tn.ShowCheckBox = false;
            nds.Add(tn);

            BindTree(tn.ChildNodes, modules, m.ModuleID, urlRoot);
        }
    }
}