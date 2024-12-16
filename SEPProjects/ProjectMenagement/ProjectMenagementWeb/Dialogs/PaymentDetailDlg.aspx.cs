using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FinanceWeb.Dialogs
{
    public partial class PaymentDetailDlg : System.Web.UI.Page
    {
        int paymentDetailId = 0;
        int paymentId = 0;
        ESP.Finance.Entity.PaymentInfo paymentModel = null;
        ESP.Finance.Entity.PaymentDetailInfo detailModel = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            paymentId = int.Parse(Request["PaymentId"].ToString());
            if (!IsPostBack)
            {
                BindData();
            }
        }

        private void BindData()
        {
            ESP.Finance.Entity.PaymentInfo paymentModel = ESP.Finance.BusinessLogic.PaymentManager.GetModel(paymentId);
            ESP.Finance.Entity.ProjectInfo proejctModel = ESP.Finance.BusinessLogic.ProjectManager.GetModelWithOutDetailList(paymentModel.ProjectID);
            IList<ESP.Finance.Entity.PaymentDetailInfo> detaillist = ESP.Finance.BusinessLogic.PaymentDetailManager.GetList(" PaymentId=" + paymentId);
            this.gvPayment.DataSource = detaillist;
            this.gvPayment.DataBind();

            lblProjectCode.Text = proejctModel.ProjectCode;
            lblPaymentCode.Text = paymentModel.PaymentCode;
            this.lblPaymentContent.Text = paymentModel.PaymentContent;
            this.lblPaymentDate.Text = paymentModel.PaymentPreDate.ToString("yyyy-MM-dd");
            this.lblPaymentAmount.Text = paymentModel.PaymentBudget.Value.ToString("#,##0.00");
            this.lblBudgetConfirm.Text = paymentModel.PaymentBudgetConfirm == null ? "" : paymentModel.PaymentBudgetConfirm.Value.ToString("#,##0.00");

            if (!string.IsNullOrEmpty(Request["DetailId"]))
            {
                paymentDetailId = int.Parse(Request["DetailId"].ToString());
                ESP.Finance.Entity.PaymentDetailInfo detailModel = ESP.Finance.BusinessLogic.PaymentDetailManager.GetModel(paymentDetailId);
                this.txtAmount.Text = detailModel.PaymentPreAmount.ToString("#,##.00");
                this.txtContent.Text = detailModel.PaymentContent;
                this.txtRemark.Text = detailModel.Remark;
            }

        }

       private int SavePaymentDetail()
        {
            //update
            if (!string.IsNullOrEmpty(Request["DetailId"]))
            {
                paymentDetailId = int.Parse(Request["DetailId"].ToString());
                detailModel = ESP.Finance.BusinessLogic.PaymentDetailManager.GetModel(paymentDetailId);
            }
            //insert
            else
            {
                detailModel = new ESP.Finance.Entity.PaymentDetailInfo();

            }
            detailModel.PaymentPreAmount = Convert.ToDecimal(this.txtAmount.Text);
            detailModel.PaymentContent = this.txtContent.Text;
            detailModel.Remark = this.txtRemark.Text;
            detailModel.PaymentPredate = DateTime.Now;
            detailModel.CreateDate = DateTime.Now;
            detailModel.PaymentID = paymentId;
            detailModel.Remark = this.txtRemark.Text;

            if (this.fileupDetail.FileName != string.Empty)
            {
                string fileName = "Contract_" + Guid.NewGuid().ToString() + "_" + this.fileupDetail.FileName;

                this.fileupDetail.SaveAs(ESP.Finance.Configuration.ConfigurationManager.ContractPath + fileName);

                detailModel.FileUrl = fileName;
            }

            if (!string.IsNullOrEmpty(Request["DetailId"]))
            {
                ESP.Finance.BusinessLogic.PaymentDetailManager.Update(detailModel);
            }
            else
            {
                ESP.Finance.BusinessLogic.PaymentDetailManager.Add(detailModel);
            }

            return 1;
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            SavePaymentDetail();

            Response.Redirect("PaymentDetailDlg.aspx?PaymentId=" + Request["PaymentId"].ToString());
        }

        protected void btnClose_Click(object sender, EventArgs e)
        {

        }

        protected void gvPayment_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            //if (e.CommandName == "Edit")
            //{
            //    int paymentid = int.Parse(e.CommandArgument.ToString());
            //    ESP.Finance.Entity.PaymentDetailInfo content = ESP.Finance.BusinessLogic.PaymentDetailManager.GetModel(paymentid);
            //    this.txtAmount.Text = content.PaymentPreAmount.ToString("#,##.00");
            //    this.txtContent.Text = content.PaymentContent;
            //    this.txtRemark.Text = content.Remark;
            //}
            if (e.CommandName == "Del")
            {
                int paymentdetailid = int.Parse(e.CommandArgument.ToString());
                ESP.Finance.BusinessLogic.PaymentDetailManager.Delete(paymentdetailid);
                BindData();
            }
        }

        protected void gvPayment_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label PaymentPreAmount = (Label)e.Row.FindControl("PaymentPreAmount");
                HyperLink hyperEdit = (HyperLink)e.Row.FindControl("hyperEdit");
               
                ESP.Finance.Entity.PaymentDetailInfo item = (ESP.Finance.Entity.PaymentDetailInfo)e.Row.DataItem;

                hyperEdit.NavigateUrl = "PaymentDetailDlg.aspx?PaymentId=" + item.PaymentID.ToString() + "&DetailId="+item.Id.ToString();

                PaymentPreAmount.Text = item.PaymentPreAmount.ToString("#,##.00");
                if (string.IsNullOrEmpty(item.FileUrl))
                {
                    e.Row.Cells[3].Text = "&nbsp;";
                }
            }
        }

        protected void gvPayment_RowEditing(object sender, GridViewEditEventArgs e)
        {

        }

        protected void btnExport_Click(object sender, EventArgs e)
        {
            //paymentId = int.Parse(Request["PaymentId"].ToString());
            ESP.Finance.BusinessLogic.PaymentManager.ExportPaymentDetail(paymentId, this.Response);
        }

    }
}
