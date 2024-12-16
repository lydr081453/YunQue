using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
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
using ExtExtenders;
using ESP.Compatible;
using ESP.Finance.Utility;

public partial class _Default : ESP.Web.UI.PageBase
{
    protected void Page_Load()
    {
        ESP.HumanResource.Entity.EmployeeBaseInfo emp = ESP.HumanResource.BusinessLogic.EmployeeBaseManager.GetModel(CurrentUserID);
        if (emp.BaseInfoOK == false)
        {
            Response.Redirect("http://xhr.shunyagroup.com/");
        }
       

        if (!IsPostBack)
        {
            CenterPanel.Attributes["src"] = WorkSpaceUrl;

            /*
             */
            string node = Request.Form["node"];
            if (node != null && node.Length > 0)
            {
                BindTree(node);
            }
            else
            {
                ExtExtenders.TreeNode tn = new ExtExtenders.TreeNode();
                tn.id = "root";
                tn.IsRoot = true;
                tn.leaf = false;
                TreePane.TreeNodes.Add(tn);
            }

            /*
            int n = 0;
            ExtExtenders.TreeNode tn = null;
            tn = new ExtExtenders.TreeNode();
            tn.id = "root";
            tn.IsRoot = true;
            tn.leaf = false;
            TreePane.TreeNodes.Add(tn);
            BindTree(this.TreePane.TreeNodes, 0, ref n);
            //BindTree(this.TreePane.TreeNodes, ESP.Finance.Configuration.ConfigurationManager.TreeRoot, ref n);
             for (int i = 0; i < TreePane.TreeNodes.Count; i++)
             {
                 if (TreePane.TreeNodes[i].id!="root")
                 BindTree(this.TreePane.TreeNodes, Convert.ToInt32(TreePane.TreeNodes[i].id), ref n);
             }
             */
        }
    }

    void BindTree(string node)
    {
        int nodeId = 0;
        int.TryParse(node, out nodeId);

        IList<ESP.Framework.Entity.ModuleInfo> list = ESP.Framework.BusinessLogic.ModuleManager.GetByUser(ESP.Configuration.ConfigurationManager.WebSiteID, UserID);

        string root = HttpRuntime.AppDomainAppVirtualPath;
        if (root.Length == 0 || root[root.Length - 1] != '/')
            root += '/';

        List<ExtExtenders.TreeNode> nodes = TreePane.TreeNodes;

        foreach (ESP.Framework.Entity.ModuleInfo m in list)
        {
            if (m.NodeType == ESP.Framework.Entity.ModuleType.WebSite || m.ParentID != nodeId)
                continue;

            ExtExtenders.TreeNode tn = new ExtExtenders.TreeNode();
            tn.text = "<span style='font-size:9pt'>" + m.ModuleName + "</span>";
            tn.id = m.ModuleID.ToString();
            tn.leaf = m.NodeType == ESP.Framework.Entity.ModuleType.Module;
            tn.parentNodeId = node;
            if (m.ModuleName.IndexOf("中心") >= 0)
                tn.icon = "/images/folder.gif";
            tn.IsRoot = false;
            if (tn.leaf)
            {
                tn.hrefTarget = "CenterPanel";
                tn.href = root + m.DefaultPageUrl;
            }

            nodes.Add(tn);
        }
    }

    //protected override void OnInit(EventArgs e)
    //{
        //base.OnInit(e);
     //   this.Load += new System.EventHandler(this.Page_Load);
    //}
    //protected void TreePane_NodeClicked(object sender, NodeClickedEventArgs e)
    //{
        //if (!e.NodeClicked.leaf)
        //{
            //return;
        //}
    //}
    /*
    void BindTree(List<ExtExtenders.TreeNode> nds, int parentId, ref int n)
    {
        DataTable dt = UserPrivilegeManager.GetNavigateTreeDT(this.UserID);
        ExtExtenders.TreeNode tn = null;
        foreach (DataRow dr in dt.Select("parentId=" + parentId, "nodesort"))
        {
            if (dr["link"].ToString().Length == 0)
            {
                tn = new ExtExtenders.TreeNode();
                tn.text=dr["name"].ToString();
                tn.id= dr["id"].ToString();
                tn.leaf = false;
                tn.parentNodeId = "root";
                tn.hrefTarget= "";
                tn.childrenRendered = true;
            }
            else
            {
                tn = new ExtExtenders.TreeNode();
                tn.text = dr["name"].ToString();
                tn.id = dr["id"].ToString();
                tn.href = dr["link"].ToString();
                tn.leaf = true;
                tn.hrefTarget = "CenterPanel";
                tn.parentNodeId = dr["parentId"].ToString();
                tn.childrenRendered = true;
            }
            tn.IsChecked = false;
            n++;

            nds.Add(tn);
           
        }
    }
     */

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


            if (workSpaceUrl == null || workSpaceUrl.Length == 0 || "default.aspx" == workSpaceUrl)
                return "project/Default.aspx";
            return workSpaceUrl;
        }
    }
}
