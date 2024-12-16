using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using ESP.Administrative.Common;
using ESP.Administrative.Entity;
using ESP.HumanResource.BusinessLogic;
using ESP.Finance.Entity;
using ESP.Finance.BusinessLogic;

namespace SEPAdmin.HR.Employees
{
    public partial class DepartmentsTree : ESP.Web.UI.PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            int n = 0;
            IList<ESP.Framework.Entity.DepartmentInfo> depinfo = ESP.Framework.BusinessLogic.DepartmentManager.GetAll();
            string str = "";
            bool isP = true;
            bool isHerf = true;
            hr.Visible = false;
            int depid = 0;

            if (!string.IsNullOrEmpty(Request["principal"]))
            {
                if (Request["principal"] == "1" )
                {
                    str = "<input type='radio'onclick=\"selectDept('{0}','{1}','{2}','{3}','{4}','{5}');\" positionID='{2}'  parentID='{1}' positionName='{5}'  id='chkdep' name='chkdep' />{5}";
                    isP = false;
                    isHerf = false;
                }
                else if (Request["principal"] == "2")
                {
                    str = "<input type='checkbox' onclick=\"selectDept('{0}','{1}','{2}','{3}','{4}','{5}');\" positionID='{2}'  parentID='{1}' positionName='{5}'  id='chkdep' name='chkdep'/>{5}";
                    isP = false;
                    isHerf = false;
                }
                else if (Request["principal"] == "3")
                {
                    isHerf = false;
                }
                else if (Request["principal"] == "4")
                {
                    str = "<input type='radio'onclick=\"selectDept('{0}','{1}','{2}','{3}','{4}','{5}','4');\" positionID='{2}'  parentID='{1}' positionName='{5}'  id='chkdep' name='chkdep' />{5}";
                    isP = false;
                    isHerf = false;
                }
            }

            List<ESP.HumanResource.Entity.EmployeesInPositionsInfo> empinposlist = ESP.HumanResource.BusinessLogic.EmployeesInPositionsManager.GetModelList(string.Format(" a.userid={0} ", UserInfo.UserID));
            if (ESP.Framework.BusinessLogic.PermissionManager.HasModulePermission("001", this.ModuleInfo.ModuleID, this.UserID))
            {
                depid = 0;
            }
            else if (ESP.Framework.BusinessLogic.PermissionManager.HasModulePermission("004", this.ModuleInfo.ModuleID, this.UserID))
            {
                if (empinposlist != null && empinposlist.Count > 0)
                {
                    depid = empinposlist[0].CompanyID;

                    //
                    TreeNode tn = null;
                    ESP.Framework.Entity.DepartmentInfo dep = ESP.Framework.BusinessLogic.DepartmentManager.Get(depid);
                    if (dep != null)
                    {
                        if (isHerf)
                        {
                            tn = new TreeNode(string.Format("<a href=\"DepartmentsForm.aspx?depid={0}&ParentID={2}\" style=\"font-size:12px;color:Black\">{1}</a>", dep.DepartmentID.ToString(), dep.DepartmentName, dep.ParentID.ToString()), dep.DepartmentID.ToString());
                            //显示新增根目录链接
                            hr.Visible = true;
                        }
                        else if (isP)
                        {
                            tn = new TreeNode(dep.DepartmentName, dep.DepartmentID.ToString());
                        }
                        else
                        {
                            if (dep.DepartmentLevel == 3)
                            {
                                ESP.Finance.Entity.DepartmentViewInfo deptView = ESP.Finance.BusinessLogic.DepartmentViewManager.GetModel(dep.DepartmentID);
                                tn = new TreeNode(string.Format(str, deptView.level1Id,deptView.level2Id,deptView.level3Id,deptView.level1,deptView.level2,deptView.level3));
                            }
                            else
                            {
                                tn = new TreeNode(string.Format("<input type='hidden' id='hiddep' name='hiddep' positionID='{2}'  parentID='{3}' positionName='{0}' />{0}", dep.DepartmentName, "所属部门负责人", dep.DepartmentID.ToString(), dep.ParentID.ToString()), dep.DepartmentID.ToString());
                            }
                        }
                        //SelectAction设为None和Expand，则TreeNode不会生成超级链接
                        tn.SelectAction = TreeNodeSelectAction.None;
                        tn.ShowCheckBox = false;
                        // tn.ToolTip = info.DepartmentName;
                        n++;
                        stv1.Nodes.Add(tn);
                    }
                }
            }
            BindTree(depinfo, stv1.Nodes, depid, ref  n, str, isP, isHerf);
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
        void BindTree(IList<ESP.Framework.Entity.DepartmentInfo> depinfo, TreeNodeCollection nds, int Id, ref int n, string treenode, bool isP, bool isHerf)
        {
            TreeNode tn = null;
            foreach (ESP.Framework.Entity.DepartmentInfo info in depinfo)
            {
                if (info.ParentID == Id)
                {
                    if (isHerf)
                    {
                        tn = new TreeNode(string.Format("<a href=\"DepartmentsForm.aspx?depid={0}&ParentID={2}\" style=\"font-size:12px;color:Black\">{1}</a>", info.DepartmentID.ToString(), info.DepartmentName, info.ParentID.ToString()), info.DepartmentID.ToString());
                        //显示新增根目录链接
                        hr.Visible = true;
                    }
                    else if (isP)
                    {
                        tn = new TreeNode(info.DepartmentName, info.DepartmentID.ToString());
                    }
                    else
                    {
                        if (info.DepartmentLevel == 3)
                        {
                            //ESP.Finance.Entity.DepartmentViewInfo deptView = ESP.Finance.BusinessLogic.DepartmentViewManager.GetModel(info.DepartmentID);

                            ESP.Framework.Entity.DepartmentInfo deptModel = ESP.Framework.BusinessLogic.DepartmentManager.Get(info.DepartmentID);
                            ESP.Framework.Entity.DepartmentInfo deptParent = ESP.Framework.BusinessLogic.DepartmentManager.Get(deptModel.ParentID);
                            ESP.Framework.Entity.DepartmentInfo deptRoot = ESP.Framework.BusinessLogic.DepartmentManager.Get(deptParent.ParentID);

                            tn = new TreeNode(string.Format(treenode, deptRoot.DepartmentID, deptParent.DepartmentID, deptModel.DepartmentID, deptRoot.DepartmentName, deptParent.DepartmentName, deptModel.DepartmentName));
                        }
                        else
                        {
                            tn = new TreeNode(string.Format("<input type='hidden' id='hiddep' name='hiddep' positionID='{2}'  parentID='{3}' positionName='{0}' />{0}", info.DepartmentName, "所属部门负责人", info.DepartmentID.ToString(), info.ParentID.ToString()), info.DepartmentID.ToString());
                        }
                    }

                    //SelectAction设为None和Expand，则TreeNode不会生成超级链接
                    tn.SelectAction = TreeNodeSelectAction.None;
                    tn.ShowCheckBox = false;
                    // tn.ToolTip = info.DepartmentName;
                    n++;
                    nds.Add(tn);
                    BindTree(depinfo, tn.ChildNodes, info.DepartmentID, ref n, treenode, isP, isHerf);
                }
            }
        }
    }
}
