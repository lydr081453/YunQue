using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ESP.Framework;
using ESP.Framework.Entity;
using ESP.Framework.BusinessLogic;
using System.Web.UI.HtmlControls;

namespace SEPAdmin.Management.ModelManagement
{
    public partial class DepartmentTree : ESP.Web.UI.PageBase
    {
        private const string TREE_NODE_HTML_TEMPLATE = "<input type='checkbox' class='ckbDepartment' id='ckbDepartment_{0}' name='ckbDepartments' value='{0}' />";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindTree();
            }
        }

        protected void BindTree()
        {
            ESP.Tree<DepartmentInfo> data = DepartmentManager.GetDepartmentTree();
            BindTreeRecursive(data, phTree.Controls);
        }

        private void BindTreeRecursive(ESP.Tree<DepartmentInfo> data, ControlCollection treeNodeCollection)
        {
            foreach (ESP.Tree<DepartmentInfo> child in data)
            {
                string html = string.Format(TREE_NODE_HTML_TEMPLATE, child.Key);
                HtmlGenericControl node = new HtmlGenericControl("li");
                HtmlGenericControl title = new HtmlGenericControl("span");
                if (!string.IsNullOrEmpty(child.Value.Description))
                    title.Attributes.Add("title", child.Value.Description);
                title.InnerHtml = html + child.Text;
                node.Controls.Add(title);
                if (child.Count > 0)
                {
                    HtmlGenericControl ul = new HtmlGenericControl("ul");
                    node.Controls.Add(ul);
                    BindTreeRecursive(child, ul.Controls);
                }
                treeNodeCollection.Add(node);
            }
        }
    }
}
