using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ESP.Finance.BusinessLogic;
using ESP.Finance.Entity;
using System.Data.SqlClient;

namespace FinanceWeb.project
{
    public partial class InvoiceList : ESP.Web.UI.PageBase
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
            string terms = "";
            List<SqlParameter> parms = new List<SqlParameter>();
            string branchs = "(";


            var lists = branchlvatist.Where(x => x.UserId == int.Parse(CurrentUser.SysID) && ((x.AuditType == 2 || x.AuditType == 0) && x.IsEdit == true));

            foreach (ESP.Finance.Entity.BranchVATInfo model in lists)
            {
                branchs += "projectcode like '" + model.BranchCode + "%' or ";
            }
            if (branchs.Length > 3)
            {
                branchs = branchs.Substring(0, branchs.Length - 3) + ")";
            }
            else
                branchs = "1=2";

            terms += branchs;

            if (!string.IsNullOrEmpty(txtKey.Text))
            {
                terms += " and (projectcode like '%'+@keys+'%' or InvoiceCode like '%'+@keys+'%' or GroupName like '%'+@keys+'%' or CreatorEmployeeName like '%'+@keys+'%' )";
                parms.Add(new SqlParameter("@keys", txtKey.Text.Trim()));
            }

            var list = InvoiceManager.GetList(terms, parms);
            gvG.DataSource = list;
            gvG.DataBind();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            ListBind();
        }

        protected void gvG_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Confirm")
            {
                int id = int.Parse(e.CommandArgument.ToString());
                InvoiceInfo model = InvoiceManager.GetModel(id);
                model.InvoiceStatus = ESP.Finance.Utility.InvoiceStatus.Destroy;
                model.CancelDate = DateTime.Now;
                InvoiceManager.Update(model);
                ListBind();
            }
        }


        protected void gvG_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                var model = (InvoiceInfo)e.Row.DataItem;
                if (model.InvoiceStatus == ESP.Finance.Utility.InvoiceStatus.Destroy)
                {
                    e.Row.FindControl("lnkConfirm").Visible = false;
                }
                else
                {
                    var vatconfirmist = branchlvatist.Where(x => x.UserId == int.Parse(CurrentUser.SysID) && ((x.AuditType == 2 || x.AuditType == 0) && x.IsAudit == true));
                    if (vatconfirmist == null || vatconfirmist.Count() == 0)
                    {
                        e.Row.FindControl("lnkConfirm").Visible = false;
                    }
                }
            }
        }

        protected void gvG_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvG.PageIndex = e.NewPageIndex;
            ListBind();
        }
    }
}
