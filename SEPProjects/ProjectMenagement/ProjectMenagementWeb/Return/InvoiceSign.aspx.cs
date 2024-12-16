using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AjaxPro;
using ESP.Finance.Entity;
using ESP.Finance.BusinessLogic;
using ESP.Finance.Utility;

namespace FinanceWeb.UserControls.Project
{
    public partial class InvoiceSign : ESP.Finance.WebPage.EditPageForProject
    {
        int PaymentID = 0;
        ESP.Finance.Entity.PaymentInfo PaymentModel = null;
        ESP.Finance.Entity.ProjectInfo ProjectModel = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            AjaxPro.Utility.RegisterTypeForAjax(typeof(Return_PaymentNotifyEdit));
            //this.ddlPaymentType.Attributes.Add("onChange", "selectPaymentType(this.options[this.selectedIndex].value,this.options[this.selectedIndex].text);");

            if (!IsPostBack)
            {
                if (!string.IsNullOrEmpty(Request[ESP.Finance.Utility.RequestName.PaymentID]))
                {
                    PaymentID = Convert.ToInt32(Request[ESP.Finance.Utility.RequestName.PaymentID]);
                    PaymentModel = ESP.Finance.BusinessLogic.PaymentManager.GetModel(PaymentID);
                    ProjectModel = ESP.Finance.BusinessLogic.ProjectManager.GetModelWithOutDetailList(PaymentModel.ProjectID);
                    if (ProjectModel != null)
                        TopMessage.ProjectModel = ProjectModel;
                    InitPage(ProjectModel, PaymentModel);
                    BindInvoices();
                }
            }
        }

        private void BindInvoices()
        {
            IList<InvoiceInfo> list = InvoiceManager.GetList(" PaymentID = " + Request[ESP.Finance.Utility.RequestName.PaymentID]);
            if (list != null && list.Count > 0)
            {
                grInvoices.DataSource = list;
                grInvoices.DataBind();
            }
        }

        private void InitPage(ESP.Finance.Entity.ProjectInfo pmodel, ESP.Finance.Entity.PaymentInfo model)
        {
            this.lblBizType.Text = pmodel.BusinessTypeName;
            this.lblBranchCode.Text = pmodel.BranchCode;
            this.lblBranchName.Text = pmodel.BranchName;
            this.lblContractStatus.Text = pmodel.ContractStatusName;
            this.lblGroupName.Text = pmodel.GroupName;
            this.lblPaymentAmount.Text = model.PaymentBudget.Value.ToString("#,##0.00");
            this.lblPaymentCircle.Text = pmodel.OtherRequest;
            this.lblPaymentCode.Text = model.PaymentCode;
            this.lblPaymentContent.Text = model.PaymentContent;
            this.lblPaymentPreDate.Text = model.PaymentPreDate.ToString("yyyy-MM-dd");
            this.lblProjectCode.Text = pmodel.ProjectCode;
            this.lblProjectName.Text = pmodel.BusinessDescription;
            this.lblProjectType.Text = pmodel.ProjectTypeName;
            this.lblResponser.Text = pmodel.ApplicantEmployeeName;
            lblResponser.Attributes["onclick"] = "javascript:ShowMsg('" + ESP.Web.UI.PageBase.GetUserInfo(pmodel.ApplicantUserID) + "');";
            ESP.Compatible.Employee emp = new ESP.Compatible.Employee(pmodel.ApplicantUserID);
            this.lblResponserEmail.Text = emp.EMail;
            this.lblResponserMobile.Text = emp.Mobile;
            this.lblResponserTel.Text = emp.Telephone;
            //this.txtFactDate.Text = model.PaymentPreDate.ToString("yyyy-MM-dd");
            //this.txtFactAmount.Text = model.PaymentBudget.Value.ToString("#,##0.00");
            //this.txtRemark.Text = model.Remark;
            string paymenttypeid = model.PaymentTypeID == null ? "" : model.PaymentTypeID.Value.ToString();
            //if (!string.IsNullOrEmpty(paymenttypeid))
            //    this.hidPaymentTypeID.Value = paymenttypeid + "," + model.PaymentTypeName;
        }

        private decimal SumInvoices()
        {
            decimal sum = 0;
            IList<InvoiceInfo> list = InvoiceManager.GetList(" PaymentID = " + Request[ESP.Finance.Utility.RequestName.PaymentID]);
            if (list != null && list.Count > 0)
            {
                foreach(InvoiceInfo invoice in list)
                {
                    sum += Convert.ToDecimal(invoice.InvoiceAmounts);
                }
            }
            return sum;
        }
        protected void btnNext_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, Page.GetType(), new Guid().ToString(), "window.close();", true);
            //Response.Redirect("/Edit/NotifyTabEdit.aspx");
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            SaveInvoice();
            this.txtInvoiceAmount.Text = string.Empty;
            this.txtInvoiceCode.Text = string.Empty;

            BindInvoices();
            ScriptManager.RegisterStartupScript(this, Page.GetType(), new Guid().ToString(), "alert('发票信息保存成功！');", true);
        }
        protected void btnSaveNext_Click(object sender, EventArgs e)
        {
            SaveInvoice();
            ScriptManager.RegisterStartupScript(this, Page.GetType(), new Guid().ToString(), "alert('发票信息保存成功，可以继续添加新发票信息！');window.close();", true);
                BindInvoices();
        }

        private void SaveInvoice()
        {
            if (!string.IsNullOrEmpty(Request[ESP.Finance.Utility.RequestName.PaymentID]))
            {
                //if (SumInvoices() >= Convert.ToDecimal(this.lblPaymentAmount.Text) && Convert.ToDecimal(this.txtInvoiceAmount.Text) > 0)
                //{
                //    ScriptManager.RegisterStartupScript(this, Page.GetType(), new Guid().ToString(), "alert('发票金额已超出付款通知金额！');", true);
                //    return;
                //}
                int PaymentID = Convert.ToInt32(Request[ESP.Finance.Utility.RequestName.PaymentID]);
                InvoiceInfo invoice = new InvoiceInfo();
                invoice.InvoiceCode = this.txtInvoiceCode.Text.Trim();
                invoice.InvoiceAmounts = Convert.ToDecimal(this.txtInvoiceAmount.Text);
                invoice.CreateDate = DateTime.Now;
                //invoice.PaymentID = PaymentID;
                invoice.CreatorID = CurrentUserID;
                
                InvoiceManager.Add(invoice);

            }
        }

        [AjaxPro.AjaxMethod]
        public static List<List<string>> GetPayments()
        {
            List<List<string>> retlists = new List<List<string>>();
            IList<PaymentTypeInfo> paylist = ESP.Finance.BusinessLogic.PaymentTypeManager.GetList(null, null);
            List<string> first = new List<string>();
            first.Add("-1");
            first.Add("请选择..");
            retlists.Add(first);
            foreach (PaymentTypeInfo item in paylist)
            {
                List<string> i = new List<string>();
                i.Add(item.PaymentTypeID.ToString());
                i.Add(item.PaymentTypeName);
                retlists.Add(i);
            }

            return retlists;
        }
    }
}
