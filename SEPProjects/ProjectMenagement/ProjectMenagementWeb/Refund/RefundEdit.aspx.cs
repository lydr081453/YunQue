using ESP.Finance.Utility;
using ESP.Purchase.BusinessLogic;
using ESP.Purchase.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FinanceWeb.Refund
{
    public partial class RefundEdit : ESP.Web.UI.PageBase
    {
        protected ESP.Purchase.Entity.GeneralInfo generalModel = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                bindData();
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            bindData();
        }

        private void bindData()
        {
             ESP.Finance.Entity.RefundInfo refundModel= null;
            if (!string.IsNullOrEmpty(Request[RequestName.ModelID]))
            {
                int id = int.Parse(Request[RequestName.ModelID]);
                refundModel = ESP.Finance.BusinessLogic.RefundManager.GetModel(id);
                this.txtPRNO.Text = refundModel.PRNO;
                generalModel = ESP.Purchase.BusinessLogic.GeneralInfoManager.GetModel(refundModel.PRID);
                this.txtPRNO.Enabled = false;
                this.btnSearch.Enabled = false;
            }
            else 
            {
                if (!string.IsNullOrEmpty(txtPRNO.Text))
                {
                    generalModel = ESP.Purchase.BusinessLogic.GeneralInfoManager.GetModelList(" requestor=" + CurrentUserID + " and prno = '" + txtPRNO.Text.Trim() + "'", new List<System.Data.SqlClient.SqlParameter>()).FirstOrDefault();
                
                }
            }

               if (generalModel != null)
                {
                    this.hidPRID.Value = generalModel.id.ToString();
                    this.lblProjectCode.Text = generalModel.project_code;
                    this.lblApplicant.Text = generalModel.requestorname;
                    this.lblTotalprice.Text = generalModel.totalprice.ToString("#,##0.00");
                    this.lblSupplierName.Text = generalModel.account_name;
                    this.lblBank.Text = generalModel.account_bank;
                    this.lblAccount.Text = generalModel.account_number;

                    var returnList = ESP.Finance.BusinessLogic.ReturnManager.GetList(" prid= " + generalModel.id.ToString());
                    this.gvG.DataSource = returnList;
                    this.gvG.DataBind();

                    this.lblCost.Text = generalModel.thirdParty_materielDesc;
               }

               if (refundModel != null)
               {
                   this.txtFee.Text = refundModel.Amounts.ToString("#,##0.00");
                   this.txtRefundDate.Text = refundModel.RefundDate.ToString("yyyy-MM-dd");
                   this.txtRemark.Text = refundModel.Remark;
               }
            
        }

        protected void gvG_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            ESP.Finance.Entity.ReturnInfo returnModel = (ESP.Finance.Entity.ReturnInfo)e.Row.DataItem;
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblStatus = (Label)e.Row.FindControl("lblStatus");
                Label lblAmounts = (Label)e.Row.FindControl("lblAmounts");

                lblStatus.Text = ReturnPaymentType.ReturnStatusString(returnModel.ReturnStatus ?? 0, 0, returnModel.IsDiscount);
                lblAmounts.Text = returnModel.FactFee == null ? returnModel.PreFee.Value.ToString("#,##0.00") : returnModel.FactFee.Value.ToString("#,##0.00");

            }
        }

        protected void btnYes_ServerClick(object sender, EventArgs e)
        {
            ESP.Finance.Entity.RefundInfo refundModel = null;
            int refundId = 0;
            if (!string.IsNullOrEmpty(Request[RequestName.ModelID]))
            {
                int id = int.Parse(Request[RequestName.ModelID]);
                refundModel = ESP.Finance.BusinessLogic.RefundManager.GetModel(id);

                refundModel.Amounts = Convert.ToDecimal(txtFee.Text);
                refundModel.Remark = txtRemark.Text;
                refundModel.RefundDate = DateTime.Now;
                refundId = refundModel.Id;
                ESP.Finance.BusinessLogic.RefundManager.Update(refundModel);
            }
            else
            {
                if (!string.IsNullOrEmpty(this.hidPRID.Value))
                {
                    generalModel = ESP.Purchase.BusinessLogic.GeneralInfoManager.GetModel(int.Parse(hidPRID.Value));
                    refundModel = new ESP.Finance.Entity.RefundInfo();
                    refundModel.PRNO = generalModel.PrNo;
                    refundModel.DeptId = generalModel.Departmentid;
                    refundModel.PRID = generalModel.id;
                    refundModel.ProjectCode = generalModel.project_code;
                    refundModel.ProjectId = generalModel.Project_id;
                    refundModel.ProjectName = generalModel.project_descripttion;
                    refundModel.RequestDate = DateTime.Now;
                    refundModel.RefundCode = ESP.Finance.BusinessLogic.RefundManager.CreateRefundCode();
                    refundModel.RequestEmployeeName = CurrentUser.Name;
                    refundModel.RequestorID = int.Parse(CurrentUser.SysID);
                    refundModel.Status = (int)ESP.Finance.Utility.PaymentStatus.Save;
                    refundModel.SupplierAccount = generalModel.account_number;
                    refundModel.SupplierBank = generalModel.account_bank;
                    refundModel.SupplierName = generalModel.account_name;

                    if (!string.IsNullOrEmpty(generalModel.thirdParty_materielID))
                        refundModel.CostId = int.Parse(generalModel.thirdParty_materielID);
                    else
                        refundModel.CostId = 0;
                    refundModel.Amounts = Convert.ToDecimal(txtFee.Text);
                    refundModel.Remark = txtRemark.Text;
                    refundModel.RefundDate = DateTime.Now;

                    refundId = ESP.Finance.BusinessLogic.RefundManager.Add(refundModel);

                }
            }
            Response.Redirect("/Workflows/SetAuditor.aspx?" + ESP.Finance.Utility.RequestName.ModelID + "=" + refundId + "&" + ESP.Finance.Utility.RequestName.ModelType + "=" + ESP.Finance.Utility.FormType.Refund);
               
        }
        protected void btnReturn_Click(object sender, EventArgs e)
        {
            Response.Redirect("/Edit/RefundTabEdit.aspx");
        }
    }
}