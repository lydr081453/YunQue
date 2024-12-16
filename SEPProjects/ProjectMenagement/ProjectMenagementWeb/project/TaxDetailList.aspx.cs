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
    public partial class TaxDetailList : ESP.Web.UI.PageBase
    {
        IList<ESP.Finance.Entity.BranchVATInfo> branchlvatist = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            branchlvatist = ESP.Finance.BusinessLogic.BranchVATManager.GetList("");
            if (!IsPostBack)
            {
                DataBind();
            }
        }

        private void DataBind()
        {
            var lists = branchlvatist.Where(x => x.UserId == int.Parse(CurrentUser.SysID) && ((x.AuditType == 1 && x.IsEdit == true) || x.AuditType==0));
            string branchs = "(";

            foreach (ESP.Finance.Entity.BranchVATInfo model in lists)
            {
                branchs += "projectcode like '"+model.BranchCode+"%' or ";
            }
            if (branchs.Length>3)
            {
                branchs = branchs.Substring(0, branchs.Length - 3) +")";
            }
            else
                branchs = "1=2";

            var list = ESP.Finance.BusinessLogic.TaxDetailManager.GetList(branchs);
            this.gvG.DataSource = list;
            this.gvG.DataBind();
        }

        protected void lbNewTax_Click(object sender, EventArgs e)
        {
            Response.Redirect("TaxDetailEdit.aspx");
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            DataBind();
        }

        protected void btnSearchAll_Click(object sender, EventArgs e)
        {
            this.txtKey.Text = "";
            DataBind();
        }

        protected void gvG_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header)
            {
                e.Row.Cells[0].Visible = false;
            }
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                ESP.Finance.Entity.TaxDetailInfo model = (ESP.Finance.Entity.TaxDetailInfo)e.Row.DataItem;
                if (model != null)
                {
                    Label lblFee = (Label)e.Row.FindControl("lblFee");
                    Label lblTax = (Label)e.Row.FindControl("lblTax");
                    Label lblTaxDate = (Label)e.Row.FindControl("lblTaxDate");
                    Label lblStatus = (Label)e.Row.FindControl("lblStatus");

                    LinkButton lnkDelete = (LinkButton)e.Row.FindControl("lnkDelete");
                    HyperLink hylEdit = (HyperLink)e.Row.FindControl("hylEdit");

                    LinkButton lnkConfirm = (LinkButton)e.Row.FindControl("lnkConfirm");
                    lblFee.Text = model.Total.ToString("#,##0.00");
                    lblTax.Text = model.Tax.ToString("#,##0.00");
                    lblTaxDate.Text = model.TaxDate.ToString("yyyy-MM-dd");
                    if (model.Status == 0)
                    {
                        lblStatus.Text = "待确认";
                        hylEdit.NavigateUrl = "TaxDetailEdit.aspx?DetailId=" + model.Id.ToString();
                        var vatconfirmist = branchlvatist.Where(x => x.UserId == int.Parse(CurrentUser.SysID) && x.AuditType == 1 && x.IsAudit == true);
                        if (vatconfirmist == null || vatconfirmist.Count() == 0)
                        {
                            lnkConfirm.Visible = false;
                        }
                    }
                    else
                    {
                        lblStatus.Text = "已确认";
                        lnkDelete.Visible = false;
                        hylEdit.Visible = false;
                        lnkConfirm.Visible = false;
                    }
                }
            }
        }

        protected void gvG_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Del")
            {
                int id = int.Parse(e.CommandArgument.ToString());
                ESP.Finance.BusinessLogic.TaxDetailManager.Delete(id);
                DataBind();
            }
            else if (e.CommandName == "Confirm")
            {
                int id = int.Parse(e.CommandArgument.ToString());
                ESP.Finance.Entity.TaxDetailInfo model = ESP.Finance.BusinessLogic.TaxDetailManager.GetModel(id);
                model.Status = 1;
                model.AuditDate = DateTime.Now;
                model.AuditerId = int.Parse(CurrentUser.SysID);
                model.Auditer = CurrentUser.Name;
                ESP.Finance.BusinessLogic.TaxDetailManager.Update(model);
            }

        }
        protected void gvG_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvG.PageIndex = e.NewPageIndex;
            DataBind();
        }
    }
}
