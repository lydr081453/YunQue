using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ESP.Framework;
using ESP.Framework.Entity;
using ESP.Framework.BusinessLogic;

namespace SEPAdmin.Management.ModuleManagement
{
    public partial class ModuleTree : ESP.Web.UI.PageBase
    {
        int selectedModuleID = 0;
        int selectedWebSiteID = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindTree();
                tvModules.CollapseAll();
            }
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            this.ModuleEdit1.ModuleTreeChanged += new ModuleTreeChangedEventHandler(ModuleEdit1_ModuleTreeChanged);
        }

        void ModuleEdit1_ModuleTreeChanged(object sender, SEPAdmin.Management.ModuleManagement.ModuleTreeChangedEventArgs args)
        {
            if (args.EventType == SEPAdmin.Management.ModuleManagement.ModuleTreeChangedEventType.Created)
            {
                selectedModuleID = args.ModuleID;
                selectedWebSiteID = args.WebSiteID;
            }
            else if (args.EventType == SEPAdmin.Management.ModuleManagement.ModuleTreeChangedEventType.Deleted)
            {
                selectedModuleID = args.ParentID;
                selectedWebSiteID = args.WebSiteID;
            }
            else if (args.EventType == SEPAdmin.Management.ModuleManagement.ModuleTreeChangedEventType.Updated)
            {
                selectedModuleID = args.ModuleID;
                selectedWebSiteID = args.WebSiteID;
            }
            BindTree();
        }

        private void BindTree()
        {
            tvModules.Nodes.Clear();
            ESP.Tree<ModuleInfo> tree = ModuleManager.GetEntireTree();
            ShowTree(tree, tvModules.Nodes);
            if (tvModules.SelectedNode != null)
            {
                TreeNode node = tvModules.SelectedNode.Parent;
                while (node != null)
                {
                    node.Expand();
                    node = node.Parent;
                }
                tvModules_SelectedNodeChanged(tvModules, EventArgs.Empty);
            }
        }

        private void ShowTree(ESP.Tree<ModuleInfo> node, TreeNodeCollection treeNodeCollection)
        {
            foreach (ESP.Tree<ModuleInfo> child in node)
            {
                TreeNode viewNode = new TreeNode(child.Text, child.Key.ToString());
                viewNode.ToolTip = child.Value.Description;
                if ((child.Value.NodeType == ModuleType.WebSite || child.Value.NodeType == ModuleType.Folder)
                    && child.Count == 0)
                {
                    //TreeNode dummyNode = new TreeNode();
                    //viewNode.ChildNodes.Add(dummyNode);
                }
                if (child.Value.ModuleID == selectedModuleID && child.Value.WebSiteID == selectedWebSiteID)
                    viewNode.Select();

                treeNodeCollection.Add(viewNode);
                ShowTree(child, viewNode.ChildNodes);
            }
        }

        protected void tvModules_SelectedNodeChanged(object sender, EventArgs e)
        {
            ulong key = 0;
            ulong.TryParse(tvModules.SelectedValue, out key);
            ModuleEdit1.Visible = EditModule(key);
        }

        private bool EditModule(ulong key)
        {
            return ModuleEdit1.BindData(key);
        }
    }
}
