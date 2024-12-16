using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FinanceWeb.project
{
    public partial class CreditCardList : ESP.Web.UI.PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            BindList();
        }

        private void BindList()
        {
            IList<ESP.Finance.Entity.BranchInfo> branchlist = null;
            if (int.Parse(CurrentUser.SysID) == int.Parse(System.Configuration.ConfigurationManager.AppSettings["EddyBinID"]))
            {
                branchlist = ESP.Finance.BusinessLogic.BranchManager.GetList("");
            }
            else
                branchlist = ESP.Finance.BusinessLogic.BranchManager.GetList(" BusinessCardAccounter = '" + CurrentUser.SysID + "'");
            string branchIds = string.Empty;
            foreach (ESP.Finance.Entity.BranchInfo model in branchlist)
            {
                branchIds += model.BranchID.ToString() + ",";
            }
            branchIds = branchIds.TrimEnd(',');
            if (!string.IsNullOrEmpty(branchIds))
            {
                string str = string.Empty;
                if (!string.IsNullOrEmpty(txtKey.Text))
                {
                    str = " and (BusinessCardNo like '%" + txtKey.Text + "%' or userCode like  '%" + txtKey.Text + "%' or username like  '%" + txtKey.Text + "%')";
                }
                if (ddlStatus.SelectedValue != "-1")
                {
                    str += " and drawstatus =" + ddlStatus.SelectedValue;
                }
                IList<ESP.Finance.Entity.BusinessCardInfo> cardList = ESP.Finance.BusinessLogic.BusinessCardManager.GetList(" branchid in(" + branchIds + ") " + str, null);
                this.gvG.DataSource = cardList;
                this.gvG.DataBind();
            }
        }

        protected void gvG_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            ESP.Finance.Entity.BusinessCardInfo cardModel = (ESP.Finance.Entity.BusinessCardInfo)e.Row.DataItem;
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label labCardStatus = (Label)e.Row.FindControl("labCardStatus");
                Label labDrawStatus = (Label)e.Row.FindControl("labDrawStatus");
                Label labBeginTime = (Label)e.Row.FindControl("labBeginTime");
                Label labEndTime = (Label)e.Row.FindControl("labEndTime");
                Label labAvailableCredit = (Label)e.Row.FindControl("labAvailableCredit");
                Label labLineOfCredit = (Label)e.Row.FindControl("labLineOfCredit");
                HyperLink hylEdit = (HyperLink)e.Row.FindControl("hylEdit");

                labCardStatus.Text = ESP.Finance.Utility.BusinessCard.GetStatus(cardModel.CardStatus);
                labDrawStatus.Text = ESP.Finance.Utility.BusinessCard.GetDrawStatus(cardModel.DrawStatus);
                labBeginTime.Text = cardModel.BeginTime.ToString("yyyy-MM-dd");
                labEndTime.Text = cardModel.EndTime.ToString("yyyy-MM-dd");
                labAvailableCredit.Text = cardModel.AvailableCredit.ToString("#,##0.00");
                labLineOfCredit.Text = cardModel.LineOfCredit.ToString("#,##0.00");
                hylEdit.NavigateUrl = "CreditCardEdit.aspx?CardId=" + cardModel.BusinessCardId.ToString();
            }
        }

        protected void gvG_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvG.PageIndex = e.NewPageIndex;
            BindList();
        }

        protected void lbNewCard_Click(object sender, EventArgs e)
        {
            Response.Redirect("CreditCardEdit.aspx");
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            BindList();
        }
        protected void btnSearchAll_Click(object sender, EventArgs e)
        {
            txtKey.Text = "";
            BindList();
        }
    }
}
