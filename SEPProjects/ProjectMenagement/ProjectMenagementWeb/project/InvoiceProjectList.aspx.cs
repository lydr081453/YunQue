using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;

namespace FinanceWeb.project
{
    public partial class InvoiceProjectList : ESP.Web.UI.PageBase
    {
        IList<ESP.Finance.Entity.BranchVATInfo> branchlvatist = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            branchlvatist = ESP.Finance.BusinessLogic.BranchVATManager.GetList("");
            if (!IsPostBack)
                ListBind();
        }

        private void ListBind()
        {
            StringBuilder strTerms = new StringBuilder();
            string branchs = string.Empty;
            var lists = branchlvatist.Where(x => x.UserId == int.Parse(CurrentUser.SysID) && ((x.AuditType == 2 || x.AuditType == 0) && x.IsEdit == true));
            foreach (ESP.Finance.Entity.BranchVATInfo model in lists)
            {
                branchs += "'" + model.BranchCode + "',";
            }
            if (!string.IsNullOrEmpty(branchs))
            {
                branchs = " and branchcode in(" + branchs.TrimEnd(',') + ")";
            }
            else
                branchs = " and 1=2";

            strTerms.Append(" and ContractTaxID in(select TaxRateID from F_TaxRate where remark='增值服务类发票') and status not in (-1,0,12,1,34) and projectcode <>'' ");
            if (txtProjectCode.Text.Trim() != "")
            {
                strTerms.Append(" and projectCode like '%" + txtProjectCode.Text.Trim() + "%'");
            }

            strTerms.Append(branchs);

            IList<ESP.Finance.Entity.ProjectInfo> list = null;

            list = ESP.Finance.BusinessLogic.ProjectManager.GetList(strTerms.ToString());
            gvProject.DataSource = list;
            gvProject.DataBind();
        }


        protected void btnSelect_Click(object sender, EventArgs e)
        {
            ListBind();
        }

        protected void gvProject_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "AddProject")
            {
                string[] projectInfo = e.CommandArgument.ToString().Split('#');
                Response.Write("<script>window.parent.setProjectInfo(" + projectInfo[0] + ",'" + projectInfo[1] + "');window.location.href='InvoicePaymentList.aspx?pid=" + projectInfo[0] + "';</script>");
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
    }
}
