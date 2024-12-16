using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace SEPAdmin.Management.UserManagement
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
            floatDiv.Attributes["style"] = "";
            
            if (!string.IsNullOrEmpty(Request["principal"]))
            {
                if (Request["principal"] == "1")
                {
                    str = "<input type='radio' id='chkdep' name='chkdep' positionID='{2}'  parentID='{3}' positionName='{0}' />{0}";
                    isP = false;
                    isHerf = false;
                    floatDiv.Attributes["style"] = "width:490px;height:465px;overflow:scroll";
                }
                else if (Request["principal"] == "2")
                {
                    str = "<input type='checkbox' id='chkdep' name='chkdep' positionID='{2}'  parentID='{3}' positionName='{0}' />{0}";
                    isP = false;
                    isHerf = false;
                    floatDiv.Attributes["style"] = "width:490px;height:465px;overflow:scroll";
                }
                else if (Request["principal"] == "3")
                {
                    isHerf = false;
                    floatDiv.Attributes["style"] = "width:490px;height:465px;overflow:scroll";
                }
                    
            }
            BindTree(depinfo, stv1.Nodes, depid, ref  n,str,isP,isHerf);
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
        void BindTree(IList<ESP.Framework.Entity.DepartmentInfo> depinfo, TreeNodeCollection nds, int Id, ref int n,string treenode ,bool isP,bool isHerf)
        {

            TreeNode tn = null;
            foreach (ESP.Framework.Entity.DepartmentInfo info in depinfo)
            {
                if (info.ParentID == Id)
                {
                    if (isHerf)
                    {
                        tn = new TreeNode(string.Format("<a href=\"DepartmentsForm.aspx?depid={0}&ParentID={2}\" style=\"font-size:12px;color:Black\">{1}</a>", info.DepartmentID.ToString(), info.DepartmentName,info.ParentID.ToString()), info.DepartmentID.ToString());
                        //显示新增根目录链接
                        hr.Visible = true;
                    }
                    else if (isP)
                    {                        
                        tn = new TreeNode(info.DepartmentName, info.DepartmentID.ToString());                        
                    }
                    else
                    {
                        if(info.DepartmentLevel == 3)
                        {
                            tn = new TreeNode(string.Format(treenode, info.DepartmentName, "所属部门负责人", info.DepartmentID.ToString(), info.ParentID.ToString()), info.DepartmentID.ToString());
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
                    BindTree(depinfo, tn.ChildNodes, info.DepartmentID, ref n,treenode,isP,isHerf);
                }
            }
        }
    }
}
