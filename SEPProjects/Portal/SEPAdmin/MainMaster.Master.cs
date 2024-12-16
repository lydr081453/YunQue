using System;


namespace SEPAdmin
{
    public partial class MainMaster : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //if (!IsPostBack)
            //{
            //    Tree<ModuleInfo> tree = ModuleController.GetUserTree(ConfigurationManager.WebSiteID, UserController.GetCurrentUserID());

            //    BindTreeToView(tree, treeNav.Nodes);
            //}
        }

        //private string GetKey(ModuleInfo m)
        //{
        //    return (m.WebSiteID + "x" + m.ModuleID).ToString();
        //}

        //private void BindTreeToView(Tree<ModuleInfo> tree, TreeNodeCollection treeNodeCollection)
        //{
        //    string url = NavigateToModule(tree.Value);
        //    TreeNode viewNode = new TreeNode(tree.Value.ModuleName, GetKey(tree.Value), null, NavigateToModule(tree.Value), null);
        //    treeNodeCollection.Add(viewNode);

        //    if (IsFocusNode(tree.Value))
        //        viewNode.Selected = true;

        //    if (string.IsNullOrEmpty(url))
        //        viewNode.SelectAction = TreeNodeSelectAction.Expand;

        //    for (int i = 0; i < tree.Count; i++)
        //    {
        //        Tree<ModuleInfo> child = tree[i];
        //        BindTreeToView(child, viewNode.ChildNodes);
        //    }
        //}

        //private string NavigateToModule(ModuleInfo module)
        //{
        //    if (string.IsNullOrEmpty(module.DefaultPageUrl))
        //        return null;

        //    return "~/" + module.DefaultPageUrl;
        //}
        //private bool IsFocusNode(ModuleInfo module)
        //{
        //    if (!(this.Page is SEP.Web.UI.PageBase))
        //        return false;
        //    WebPageInfo info = ((SEP.Web.UI.PageBase)this.Page).WebPageInfo ;
        //    if (info == null)
        //        return false;

        //    return info.ModuleID == module.ModuleID;
        //}
    }
}
