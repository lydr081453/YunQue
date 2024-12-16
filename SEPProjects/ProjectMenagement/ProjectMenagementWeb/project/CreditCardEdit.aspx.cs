using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FinanceWeb.project
{
    public partial class CreditCardEdit : ESP.Web.UI.PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            AjaxPro.Utility.RegisterTypeForAjax(typeof(FinanceWeb.project.CreditCardEdit));
            this.ddlBranch.Attributes.Add("onChange", "selectBranch(this.options[this.selectedIndex].value,this.options[this.selectedIndex].text);");

            if (!IsPostBack)
            {
                if (!string.IsNullOrEmpty(Request["CardId"]))
                {
                    bindData(Convert.ToInt32(Request["CardId"]));
                }
            }
        }
        private void bindData(int cardId)
        {
            ESP.Finance.Entity.BusinessCardInfo cardModel = ESP.Finance.BusinessLogic.BusinessCardManager.GetModel(cardId);
            this.txtAvailable.Text = cardModel.AvailableCredit.ToString("#,##0.00");
            this.txtLine.Text = cardModel.LineOfCredit.ToString("#,##0.00");
            if (cardModel.BeginTime != null && cardModel.BeginTime != new DateTime(1900, 1, 1) && cardModel.BeginTime != new DateTime(1, 1, 1))
                this.txtBeginTime.Text = cardModel.BeginTime.ToString("yyyy-MM-dd");
            if (cardModel.EndTime != null && cardModel.EndTime != new DateTime(1900, 1, 1) && cardModel.EndTime != new DateTime(1, 1, 1))
                this.txtEndTime.Text = cardModel.EndTime.ToString("yyyy-MM-dd");
            this.txtCardNo.Text = cardModel.BusinessCardNo;
            this.txtCardNo2.Text = cardModel.HouseholdNo;
            this.lblUserCode.Text = cardModel.UserCode;
            this.lblUserName.Text = cardModel.UserName;
            this.ddlStatus.SelectedValue = cardModel.CardStatus.ToString();
            this.ddlDraw.SelectedValue = cardModel.DrawStatus.ToString();
            this.hidBranchId.Value = cardModel.BranchId.ToString();
            this.hidUserId.Value = cardModel.UserId.ToString();
            if (cardModel.CancellationDate != null && cardModel.CancellationDate != new DateTime(1900, 1, 1) && cardModel.CancellationDate != new DateTime(1, 1, 1))
                this.txtCancelDate.Text = cardModel.CancellationDate.ToString("yyyy-MM-dd");
        }

        protected void btnOk_Click(object sender, EventArgs e)
        {
            ESP.Finance.Entity.BranchInfo branchModel = null;
            if (!string.IsNullOrEmpty(this.hidBranchId.Value) && this.hidBranchId.Value != "0")
                branchModel = ESP.Finance.BusinessLogic.BranchManager.GetModel(Convert.ToInt32(this.hidBranchId.Value));

            ESP.Compatible.Employee emp = null;
            if (!string.IsNullOrEmpty(hidUserId.Value) && hidUserId.Value != "0")
                emp = new ESP.Compatible.Employee(Convert.ToInt32(hidUserId.Value));
            if (!string.IsNullOrEmpty(Request["CardId"]))
            {
                ESP.Finance.Entity.BusinessCardInfo cardModel = ESP.Finance.BusinessLogic.BusinessCardManager.GetModel(Convert.ToInt32(Request["CardId"]));
                if (!string.IsNullOrEmpty(this.txtAvailable.Text))
                cardModel.AvailableCredit = Convert.ToDecimal(this.txtAvailable.Text);
                if (!string.IsNullOrEmpty(this.txtBeginTime.Text))
                    cardModel.BeginTime = Convert.ToDateTime(this.txtBeginTime.Text);
                else
                    cardModel.BeginTime = new DateTime(1900,1,1);
                if (branchModel != null)
                {
                    cardModel.BranchCode = branchModel.BranchCode;
                    cardModel.BranchId = branchModel.BranchID;
                }
                cardModel.BusinessCardNo = this.txtCardNo.Text;
                if (cardModel.CardStatus == (int)ESP.Finance.Utility.BusinessCardStatus.Available && Convert.ToInt32(ddlStatus.SelectedValue) == (int)ESP.Finance.Utility.BusinessCardStatus.LogOut && !string.IsNullOrEmpty(this.txtCancelDate.Text))
                {
                    cardModel.CancellationDate = Convert.ToDateTime(this.txtCancelDate.Text);
                }
                else
                {
                    cardModel.CancellationDate = new DateTime(1900, 1, 1);
                }
                cardModel.CardStatus = Convert.ToInt32(ddlStatus.SelectedValue);
                cardModel.DrawStatus = Convert.ToInt32(ddlDraw.SelectedValue);
                cardModel.EndTime = Convert.ToDateTime(this.txtEndTime.Text);
                cardModel.HouseholdNo = this.txtCardNo2.Text;
                cardModel.LineOfCredit = Convert.ToDecimal(this.txtLine.Text);
                cardModel.UpdateTime = DateTime.Now;
                if (emp != null)
                {
                    cardModel.UserCode = emp.ID;
                    cardModel.UserId = Convert.ToInt32(emp.SysID);
                    cardModel.UserName = emp.Name;
                }
                ESP.Finance.BusinessLogic.BusinessCardManager.Update(cardModel);
            }
            else
            {
                ESP.Finance.Entity.BusinessCardInfo cardModel = new ESP.Finance.Entity.BusinessCardInfo();
                if (!string.IsNullOrEmpty(this.txtAvailable.Text))
                    cardModel.AvailableCredit = Convert.ToDecimal(this.txtAvailable.Text);
                if (!string.IsNullOrEmpty(this.txtBeginTime.Text))
                    cardModel.BeginTime = Convert.ToDateTime(this.txtBeginTime.Text);
                else
                    cardModel.BeginTime = new DateTime(1900, 1, 1);

                if (branchModel != null)
                {
                    cardModel.BranchCode = branchModel.BranchCode;
                    cardModel.BranchId = branchModel.BranchID;
                }
                cardModel.BusinessCardNo = this.txtCardNo.Text;
                cardModel.CardStatus = Convert.ToInt32(ddlStatus.SelectedValue);
                cardModel.DrawStatus = Convert.ToInt32(ddlDraw.SelectedValue);
                if (!string.IsNullOrEmpty(this.txtEndTime.Text))
                    cardModel.EndTime = Convert.ToDateTime(this.txtEndTime.Text);
                else
                    cardModel.EndTime = new DateTime(1900,1,1);
                cardModel.HouseholdNo = this.txtCardNo2.Text;
                if (!string.IsNullOrEmpty(this.txtLine.Text))
                    cardModel.LineOfCredit = Convert.ToDecimal(this.txtLine.Text);
                cardModel.UpdateTime = DateTime.Now;
                if (emp != null)
                {
                    cardModel.UserCode = emp.ID;
                    cardModel.UserId = Convert.ToInt32(emp.SysID);
                    cardModel.UserName = emp.Name;
                }
                cardModel.CreateTime = DateTime.Now;
                if (Convert.ToInt32(ddlStatus.SelectedValue) == (int)ESP.Finance.Utility.BusinessCardStatus.LogOut && !string.IsNullOrEmpty(this.txtCancelDate.Text))
                {
                    cardModel.CancellationDate = Convert.ToDateTime(this.txtCancelDate.Text);
                }
                else
                {
                    cardModel.CancellationDate = new DateTime(1900, 1, 1);
                }
                ESP.Finance.BusinessLogic.BusinessCardManager.Add(cardModel);
            }
            Response.Redirect("CreditCardList.aspx");
        }
        protected void btnReturn_Click(object sender, EventArgs e)
        {
            Response.Redirect("CreditCardList.aspx");
        }

        [AjaxPro.AjaxMethod]
        public static List<List<string>> GetBranchs()
        {
            List<List<string>> retlists = new List<List<string>>();
            IList<ESP.Finance.Entity.BranchInfo> branchList = ESP.Finance.BusinessLogic.BranchManager.GetList("");
            List<string> first = new List<string>();
            first.Add("-1");
            first.Add("请选择..");
            retlists.Add(first);
            foreach (ESP.Finance.Entity.BranchInfo item in branchList)
            {
                List<string> i = new List<string>();
                i.Add(item.BranchID.ToString());
                i.Add(item.BranchCode);
                retlists.Add(i);
            }
            return retlists;
        }

    }
}
