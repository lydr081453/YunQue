using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using System.Text;
using ESP.Framework;
using ESP.Framework.Entity;
using ESP.Framework.BusinessLogic;

namespace SEPAdmin
{
    public partial class RolePermissionModify : ESP.Web.UI.PageBase
    {
        private const string TREE_NODE_HTML_TEMPLATE = "<input type='checkbox' id='ckbModules_{0}' name='ckbModules' value='{0}' parent-module-key='{1}' />";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (this.RoleID <= 0 || CheckRoleID(this.RoleID, this.IsFakeRole) == false)
            {
                TryNavigateBack();
                Response.End();
                return;
            }

            StringBuilder sb = new StringBuilder();

            string postbackData = Request.Form["ckbModules"];
            string selectedKeysScriptString = postbackData == null ? "''" : "'," + postbackData + ",'";

            if (!this.IsPostBack)
            {
                ESP.Tree<ModuleInfo> moduleData = ModuleManager.GetEntireTree();
                IList<PermissionDefinitionInfo> permissionDefData = PermissionManager.GetAllDefinitions();
                BindTree(moduleData, permissionDefData, stv1.Nodes);

                IList<KeyValuePair<int, int>> selectedKeys = PermissionManager.GetRolePermisssions(this.RoleID, this.IsFakeRole);
                sb.Append(',');
                for (int i = 0; i < selectedKeys.Count; i++)
                {
                    KeyValuePair<int, int> pair = selectedKeys[i];
                    string key;
                    if (pair.Key == 0)
                    {
                        key = ModuleManager.MakeKey(ModuleType.Module, pair.Value).ToString();
                    }
                    else
                    {
                        key = ModuleManager.MakeKey(ModuleType.Module, pair.Value) + "-" + pair.Key;
                    }
                    sb.Append(key).Append(',');
                }
                selectedKeysScriptString = sb.ToString();
                sb.Length = 0;
            }

            hdnSelectedKeys.Value = selectedKeysScriptString;

            /* 
             * Sys.Application.add_load(
             *     function(){
             *         sep_RolePermissions_wrapperCheckboxes('ckbModules', 'ckbModules_', 'ctl_ctl_hdnSelectedKeys');
             *     }
             * );
             */
            sb.Append(@"
/********************************************************************************/
Sys.Application.add_load(
    function(){
        sep_RolePermissions_wrapperCheckboxes('ckbModules', 'ckbModules_', '").Append(hdnSelectedKeys.ClientID).Append(@"');
    }
);
/********************************************************************************/
");

            ClientScript.RegisterStartupScript(typeof(RolePermissionModify), "sep_RolePermissions_wrapperCheckboxes", sb.ToString(), true);
        }

        bool IsFakeRole
        {
            get
            {
                if (this.ViewState["IsFakeRole"] == null)
                {
                    this.ViewState["IsFakeRole"] = (Request.QueryString["isfr"] != null);
                }
                return (bool)this.ViewState["IsFakeRole"];
            }
        }

        bool CheckRoleID(int roleId, bool isFakeRole)
        {
            if (IsFakeRole)
            {
                if (roleId < 1 || roleId > 3)
                    return false;
                else
                    return true;
            }
            else
            {
                RoleInfo r = RoleManager.Get(roleId);
                if (r == null)
                    return false;
                else
                    return true;
            }
        }

        int RoleID
        {
            get
            {
                if (this.ViewState["RoleID"] == null)
                {
                    int roleId = 0;
                    int.TryParse(Request.QueryString["id"], out roleId);
                    this.ViewState["RoleID"] = roleId;
                }

                return (int)this.ViewState["RoleID"];

            }
        }

        void BindTree(ESP.Tree<ModuleInfo> moduleData, IList<PermissionDefinitionInfo> permissionDefData, TreeNodeCollection treeNodeCollection)
        {
            foreach (ESP.Tree<ModuleInfo> child in moduleData)
            {
                string html = string.Format(TREE_NODE_HTML_TEMPLATE, child.Key, moduleData.Key);
                TreeNode viewNode = new TreeNode(html + child.Text, child.Key.ToString());
                viewNode.ToolTip = child.Value.Description;
                viewNode.SelectAction = TreeNodeSelectAction.Expand;
                treeNodeCollection.Add(viewNode);

                if (child.Value.NodeType == ModuleType.Module)
                {
                    for (int i = 0; i < permissionDefData.Count; i++)
                    {
                        PermissionDefinitionInfo pd = permissionDefData[i];
                        if (pd.ReferredEntityType == EntityType.Module
                            && (pd.ReferredEntityID == child.Value.ModuleID || pd.ReferredEntityID == 0))
                        {
                            string key = child.Key + "-" + pd.PermissionDefinitionID;
                            string html2 = string.Format(TREE_NODE_HTML_TEMPLATE, key, child.Key);
                            TreeNode viewNodePd = new TreeNode(html2 + pd.Description, key);
                            viewNode.ChildNodes.Add(viewNodePd);
                        }
                    }
                }

                BindTree(child, permissionDefData, viewNode.ChildNodes);
            }
        }

        private void SetErrorText(string text)
        {
            lblMsg.Text = text;
            lblMsg.ForeColor = Color.Red;
        }
        private void SetInformationText(string text)
        {
            lblMsg.Text = text;
            lblMsg.ForeColor = Color.Green;
        }

        private void TryNavigateBack()
        {
            Response.Redirect("~/Management/ModelManagement/RoleBrowse.aspx");
            if (Request.QueryString[""] != null)
            {

            }
            else
            {
            }
        }

        private IList<KeyValuePair<int, int>> ParsePostbackData(string postbackData)
        {
            if (postbackData == null) postbackData = string.Empty;
            string[] arr = postbackData.Split(',');
            IList<KeyValuePair<int, int>> list = new List<KeyValuePair<int, int>>(arr.Length);
            for (int i = 0; i < arr.Length; i++)
            {
                string item = arr[i];
                if (string.IsNullOrEmpty(item))
                    continue;

                ulong key;
                int pid;
                int mid;

                int index = item.IndexOf('-');
                if (index < 0)
                {
                    if (!ulong.TryParse(item, out key))
                        continue;

                    pid = 0;
                }
                else
                {
                    string item_key = item.Substring(0, index);
                    string item_pid = item.Substring(index + 1);

                    if (!ulong.TryParse(item_key, out key))
                        continue;
                    if (!int.TryParse(item_pid, out pid))
                        continue;
                }

                ModuleType type = (ModuleType)(key >> 32);
                if (type != ModuleType.Module)
                    continue;

                mid = (int)(key & 0xffffffff);

                if (mid <= 0 || pid < 0)
                    continue;

                list.Add(new KeyValuePair<int, int>(pid, mid));
            }

            return list;
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            string postbackData = Request.Form["ckbModules"];
            IList<KeyValuePair<int, int>> list = ParsePostbackData(postbackData);

            PermissionManager.UpdateRolePermissions(list, this.RoleID, this.IsFakeRole);

            SetInformationText(Request.Form["ckbModules"]);
            TryNavigateBack();
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            TryNavigateBack();
        }
    }
}
