using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Data.SqlClient;

namespace FinanceWeb.project
{
    public partial class PnTaxSelect : ESP.Web.UI.PageBase
    {
        IList<ESP.Finance.Entity.BranchVATInfo> branchlvatist = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            branchlvatist=ESP.Finance.BusinessLogic.BranchVATManager.GetList("");
            if (!IsPostBack)
                ListBind();
        }

        private void ListBind()
        {
            StringBuilder strTerms = new StringBuilder();
            var lists = branchlvatist.Where(x=>x.UserId==int.Parse(CurrentUser.SysID) && x.AuditType==1 && x.IsEdit==true);
            var maxpermissions = branchlvatist.Where(x => x.UserId == int.Parse(CurrentUser.SysID) && x.AuditType == 0);
            string branchs = string.Empty;
            //string deptids = string.Empty;
            foreach (ESP.Finance.Entity.BranchVATInfo model in lists)
            {
                branchs += model.BranchId + ",";
            }
            branchs = branchs.TrimEnd(',');

            //如果不是最高管理员,进行后续判断
            if (maxpermissions == null || maxpermissions.Count() == 0)
            {
                if (!string.IsNullOrEmpty(branchs))
                {
                    strTerms.Append(" and branchid in(" + branchs + ")");
                }
                else
                {
                    strTerms.Append(" and 1=2 ");
                }

                if (txtProjectCode.Text.Trim() != "")
                {
                    strTerms.Append(" and projectCode like '%" + txtProjectCode.Text.Trim() + "%'");
                }
                else
                {
                    strTerms.Append(" and projectCode like '%V' ");
                }
            }
            else
            {
                if (txtProjectCode.Text.Trim() != "")
                {
                    strTerms.Append(" and projectCode like '%" + txtProjectCode.Text.Trim() + "%'");
                }
                else
                {
                    strTerms.Append(" and projectCode like '%V' ");
                }
            }

            IList<ESP.Finance.Entity.ProjectInfo> list = null;

            list = ESP.Finance.BusinessLogic.ProjectManager.GetList(strTerms.ToString());
            gvProject.DataSource = list;
            gvProject.DataBind();
        }


        protected void btnSelect_Click(object sender, EventArgs e)
        {
            ListBind();
        }
        protected void btnProjectReSelect_OnClick(object sender, EventArgs e)
        {
            lblReturnProjectCode.Text = string.Empty;
            tableProject.Visible = true;
            this.gvProject.Visible = true;
        }

        protected void btnReturnSelect_Click(object sender, EventArgs e)
        {
            ReturnBind();
        }


        private void ReturnBind()
        {
            SearchPN(null, this.lblReturnProjectCode.Text);
        }

        protected void gvProject_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Add")
            {
                string projectId = e.CommandArgument.ToString();
                ESP.Finance.Entity.ProjectInfo projectModel = ESP.Finance.BusinessLogic.ProjectManager.GetModelWithOutDetailList(int.Parse(projectId));
                lblReturnProjectCode.Text = projectModel.ProjectCode;
                tableProject.Visible = false;
                this.gvProject.Visible = false;
                SearchPN(projectId, null);
            }
            else if (e.CommandName == "AddProject")
            {
                string projectId = e.CommandArgument.ToString();
                Response.Write("<script>this.close();window.parent.location='TaxDetailEdit.aspx?ProjectId=" + projectId + "';</script>");
            }
        }

        protected void gvProject_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                ESP.Finance.Entity.ProjectInfo model = (ESP.Finance.Entity.ProjectInfo)e.Row.DataItem;
                Literal litGroup = (Literal)e.Row.FindControl("litGroup");
                litGroup.Text = model.GroupName;
            }
        }

        private void SearchPN(string projectid, string projectcode)
        {
            IList<ESP.Finance.Entity.ReturnInfo> returnlist = null;
            if (!string.IsNullOrEmpty(projectid))
            {
                string term = " projectid =" + projectid + " and returntype not in(34,36,11) and returnstatus not in(-1,1,0) and returnid not in(select returnid from F_TaxDetail)";
                if (!string.IsNullOrEmpty(this.txtReturnKey.Text))
                {
                    term += " and (projectcode like '%" + this.txtReturnKey.Text + "%' or returncode like '%" + this.txtReturnKey.Text + "%' or supplierName like '%" + this.txtReturnKey.Text + "%')";
                }
                returnlist = ESP.Finance.BusinessLogic.ReturnManager.GetList(term);
            }
            if (!string.IsNullOrEmpty(projectcode))
            {
                string term = " projectcode ='" + projectcode + "' and returntype not in(34,36,11) and returnstatus not in(-1,0,1) and returnid not in(select returnid from F_TaxDetail)";
                if (!string.IsNullOrEmpty(this.txtReturnKey.Text))
                {
                    term += " and (projectcode like '%" + this.txtReturnKey.Text + "%' or returncode like '%" + this.txtReturnKey.Text + "%' or supplierName like '%" + this.txtReturnKey.Text + "%')";
                }
                returnlist = ESP.Finance.BusinessLogic.ReturnManager.GetList(term);
            }
            gvPN.DataSource = returnlist;
            gvPN.DataBind();
        }

        protected void gvPN_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Add")
            {
                string returnid = e.CommandArgument.ToString();
                Response.Write("<script>this.close();window.parent.location='TaxDetailEdit.aspx?ReturnId=" + returnid + "';</script>");
            }
           

        }

        protected void gvPN_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                ESP.Finance.Entity.ReturnInfo model = (ESP.Finance.Entity.ReturnInfo)e.Row.DataItem;
                Label lblAmounts = (Label)e.Row.FindControl("lblAmounts");
                Label lblCostDept = (Label)e.Row.FindControl("lblCostDept");
                lblAmounts.Text = model.FactFee == null ? model.PreFee.Value.ToString("#,##0.00") : model.FactFee.Value.ToString("#,##0.00");
                ESP.Framework.Entity.DepartmentInfo dept = ESP.Framework.BusinessLogic.DepartmentManager.Get(model.DepartmentID.Value);
                lblCostDept.Text = dept.DepartmentName;
            }
        }


    }
}
