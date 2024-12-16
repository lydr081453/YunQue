using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace SEPAdmin.HR.Employees
{
    public partial class DepartmentsTree2 : ESP.Web.UI.PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            int n = 0;

            List<ESP.Administrative.Entity.OperationAuditManagerExtendInfo> elist = new ESP.Administrative.BusinessLogic.OperationAuditManageManager().GetModelListIncludeUserName(" o.deleted= 0");
            string str = "";
            hr.Visible = false;

            stv1.ExpandDepth = 1;
            floatDiv.Attributes["style"] = "width:490px;height:465px;overflow:scroll";
            //TODO userid写死
            BindTree(elist, stv1.Nodes, 13369, ref  n, str, true, false);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="depinfo">部门信息</param>
        /// <param name="nds">树节点</param>
        /// <param name="Id">部门id</param>
        /// <param name="n">计数器</param>
        /// <param name="treenode">树选择框形式</param>
        /// <param name="isP">是否不要选择框</param>
        /// <param name="isHerf">是否要链接</param>
        void BindTree(List<ESP.Administrative.Entity.OperationAuditManagerExtendInfo> elist, TreeNodeCollection nds, int Id, ref int n, string treenode, bool isP, bool isHerf)
        {
            TreeNode tn = null;
            foreach (ESP.Administrative.Entity.OperationAuditManagerExtendInfo info in elist)
            {
                if (info.TeamLeaderID == Id)
                {
                    tn = new TreeNode(info.UserName, info.TeamLeaderID.ToString());
                    //SelectAction设为None和Expand，则TreeNode不会生成超级链接
                    tn.SelectAction = TreeNodeSelectAction.None;
                    tn.ShowCheckBox = false;
                    // tn.ToolTip = info.DepartmentName;
                    n++;
                    nds.Add(tn);
                    BindTree(elist, tn.ChildNodes, info.UserID, ref n, treenode, isP, isHerf);
                }
            }
        }
    }
}
