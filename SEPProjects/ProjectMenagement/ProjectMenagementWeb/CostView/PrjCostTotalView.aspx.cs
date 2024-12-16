using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ESP.Finance.Utility;
using ESP.Finance.Entity;
namespace FinanceWeb.CostView
{
    public partial class ProjectCostTotalView : ESP.Web.UI.PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindData();
            }
        }
        private string GetUser()
        {
            string user = "," + ESP.Finance.BusinessLogic.BranchManager.GetProjectAccounters() + ",";
            user += ESP.Finance.BusinessLogic.BranchManager.GetContractAccounters() + "," + ESP.Finance.BusinessLogic.BranchManager.GetFinalAccounters() + ",";
            return user;
        }
        private string GetManagerDept()
        {
            string deptids = string.Empty;
            DataSet ds = ESP.Framework.BusinessLogic.OperationAuditManageManager.GetList(" directorid=" + CurrentUser.SysID + " or managerid=" + CurrentUser.SysID + " or ceoid=" + CurrentUser.SysID);
            if (ds != null && ds.Tables[0] != null)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    deptids += ds.Tables[0].Rows[i]["DepId"].ToString() + ",";
                }
                deptids = deptids.TrimEnd(',');
            }
            return deptids;
        }

        private string GetOtherAccounters()
        {
            string BranchID = string.Empty;
            IList<ESP.Finance.Entity.BranchInfo> branchList = ESP.Finance.BusinessLogic.BranchManager.GetList(" OtherFinancialUsers like '%,"+CurrentUser.SysID+",%'");
            if (branchList != null && branchList.Count > 0)
            { 
                foreach(ESP.Finance.Entity.BranchInfo branch in branchList)
                {
                    BranchID += branch.BranchID.ToString() + ",";
            }
            }
            BranchID = BranchID.TrimEnd(',');
            return BranchID;
        }

        private void BindData()
        {
            string term = string.Empty;
            List<System.Data.SqlClient.SqlParameter> paramlist = new List<SqlParameter>();
            string Branchs = GetOtherAccounters();
            string DeptIDs = this.GetManagerDept();
            term = "  and (projectCode is not null and projectcode<>'') and (projectid in(select dataid from t_datainfo where datatype=2 and id in(select datainfoid from t_datapermission where userid=@memberuserid)) ";
                string strdept = ESP.Finance.BusinessLogic.PrjUserRelationManager.GetUsableBranchID(CurrentUserID);
                if (!string.IsNullOrEmpty(strdept))
                {
                    term += " or branchid in(" + strdept + ")";
                }
                if (!string.IsNullOrEmpty(DeptIDs))
                {
                    term += " or groupid in(" + DeptIDs + ")";
                }
                if (!string.IsNullOrEmpty(Branchs))
                {
                    term += " or branchid in(" + Branchs + ")";
                }
                term += ")";
                 paramlist.Add(new System.Data.SqlClient.SqlParameter("@memberuserid", CurrentUserID));
    

            if (!string.IsNullOrEmpty(this.txtKey.Text.Trim()))
            {
                string customerIDs = string.Empty;
                IList<ESP.Finance.Entity.CustomerTmpInfo> customerList = ESP.Finance.BusinessLogic.CustomerTmpManager.GetList(" NameCN1 like '%" + this.txtKey.Text.Trim() + "%' or  NameEN1 like '%" + this.txtKey.Text.Trim() + "%' ");
                foreach (ESP.Finance.Entity.CustomerTmpInfo cusModel in customerList)
                {
                    customerIDs += cusModel.CustomerTmpID.ToString() + ",";
                }
                customerIDs = customerIDs.TrimEnd(',');
                term += " and (serialcode like '%'+@serialcode+'%' or BusinessDescription like '%'+@serialcode+'%' or projectcode like '%'+@serialcode+'%' or GroupName like '%'+@serialcode+'%' or BranchName like '%'+@serialcode+'%' or BusinessTypeName like '%'+@serialcode+'%' ";
                if (!string.IsNullOrEmpty(customerIDs))
                    term += " or CustomerID in(" + customerIDs + "))";
                else
                    term += ")";
                paramlist.Add(new System.Data.SqlClient.SqlParameter("@serialcode", this.txtKey.Text.Trim()));
            }

            IList<ESP.Finance.Entity.ProjectInfo> projectList = ESP.Finance.BusinessLogic.ProjectManager.GetList(term, paramlist);
            this.gvG.DataSource = projectList;
            this.gvG.DataBind();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            BindData();
        }
        protected void btnSearchAll_Click(object sender, EventArgs e)
        {
            this.txtKey.Text = string.Empty;
            BindData();
        }

        protected void gvG_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvG.PageIndex = e.NewPageIndex;
            BindData();
        }

        protected void gvG_RowCommand(object sender, GridViewCommandEventArgs e)
        {
          
        }

        protected void gvG_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                ESP.Finance.Entity.ProjectInfo projectModel =(ESP.Finance.Entity.ProjectInfo)e.Row.DataItem;
                Label lblTotalCost = (Label)e.Row.FindControl("lblTotalCost");
                Label lblUsedCost = (Label)e.Row.FindControl("lblUsedCost");
                Label lblBalanceCost = (Label)e.Row.FindControl("lblBalanceCost");
                Label labClose = (Label)e.Row.FindControl("labClose");
                labClose.Text = projectModel.EndDate.Value.ToString("yyyy-MM-dd");
                //所有部门级联字符串拼接
                Label lblGroupName = (Label)e.Row.FindControl("lblGroupName");
                List<ESP.Framework.Entity.DepartmentInfo> depList = new List<ESP.Framework.Entity.DepartmentInfo>();
                depList = ESP.Framework.BusinessLogic.DepartmentManager.GetDepartmentListByID(projectModel.GroupID.Value, depList);
                string groupname = string.Empty;
                foreach (ESP.Framework.Entity.DepartmentInfo dept in depList)
                {
                    groupname += dept.DepartmentName + "-";
                }
                if (!string.IsNullOrEmpty(groupname))
                    lblGroupName.Text = groupname.Substring(0, groupname.Length - 1);

                decimal totalCost= ESP.Finance.BusinessLogic.ContractCostManager.GetTotalAmountByProject(projectModel.ProjectId);
                //totalCost += ESP.Finance.BusinessLogic.ProjectExpenseManager.GetTotalExpense(projectModel.ProjectId);
                lblTotalCost.Text = totalCost.ToString("#,##0.00");
                decimal usedCost = ESP.Finance.BusinessLogic.CheckerManager.GetOccupyCost(projectModel.ProjectId,projectModel.GroupID.Value);
                decimal refund = ESP.Finance.BusinessLogic.CheckerManager.GetRefundTotal(projectModel.ProjectCode, projectModel.GroupID.Value);
                lblUsedCost.Text = usedCost.ToString("#,##0.00");
                lblBalanceCost.Text = (totalCost - usedCost+refund).ToString("#,##0.00");
            }
        }
    }
}
