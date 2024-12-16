using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ESP.Finance.Entity;
using ESP.Finance.BusinessLogic;
using System.Web.Script.Services;
using System.Web.Services;
using System.Collections;

namespace FinanceWeb.project
{
    public partial class InvoiceEdit : ESP.Web.UI.PageBase
    {
        int invoiceId = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Request["invoiceId"]))
            {
                invoiceId = int.Parse(Request["invoiceId"]);
            }

            if (!IsPostBack)
            {
                BindInfo();
            }
        }

        private void BindInfo()
        {
            InvoiceInfo model = InvoiceManager.GetModel(invoiceId);
            if (model != null)
            {
                lblProjectCode.Text = model.ProjectCode;
                hidprojectId.Value = model.ProjectId.ToString();
                txtInvoicePrice.Text = model.InvoiceAmounts.Value.ToString();
                txtInvoiceCode.Text = model.InvoiceCode;

                ProjectInfo projectInfo = ProjectManager.GetModel(model.ProjectId);

                TaxRateInfo tm = TaxRateManager.GetModel(projectInfo.ContractTaxID.Value);

                decimal invoicePrice = decimal.Parse(txtInvoicePrice.Text);

                labPirce.Text = (invoicePrice - (invoicePrice / (1 + tm.InvoiceRate.Value) * tm.InvoiceRate.Value)).ToString("#,##0.00");
                labTax.Text = (invoicePrice / (1 + tm.InvoiceRate.Value) * tm.InvoiceRate.Value).ToString("#,##0.00");

                txtInvoiceDate.Text = model.InvoiceDate.Value.ToString("yyyy-MM-dd");
                labCancelDate.Text = model.CancelDate == null ? "" : model.CancelDate.Value.ToString("yyyy-MM-dd");
                txtRemark.Text = model.Remark;


                var dlist = InvoiceDetailManager.GetList(" InvoiceID=" + model.InvoiceID);
                hidpaymentIds.Value = ",";
                foreach (var l in dlist)
                {
                    hidpaymentIds.Value += l.PaymentID + ",";
                }
                lnkList_Click(new object(), new EventArgs());

                if (model.InvoiceStatus == ESP.Finance.Utility.InvoiceStatus.Used || model.InvoiceStatus == ESP.Finance.Utility.InvoiceStatus.Destroy)
                {
                    btnYes.Enabled = btnSubmit.Enabled = false;
                }
            }
        }

        protected void lnkDel_Click(object sender, EventArgs e)
        {
            hidpaymentIds.Value = hidpaymentIds.Value.Replace("," + ((LinkButton)sender).CommandArgument.ToString() + ",", ",");
            lnkList_Click(new object(), new EventArgs());
        }

        protected void lnkList_Click(object sender, EventArgs e)
        {
            string paymentIds = hidpaymentIds.Value.Trim(new char[] { ',' });
            var list = PaymentManager.GetList(" paymentid in (" + (paymentIds == "" ? "0" : paymentIds) + ")");
            gvPayment.DataSource = list;
            gvPayment.DataBind();
        }


        [WebMethod]
        public static Hashtable changePirce(string projectId1, string invoicePrice1)
        {

            int projectId = int.Parse(projectId1);
            decimal invoicePrice = decimal.Parse(invoicePrice1);

            ProjectInfo projectInfo = ProjectManager.GetModel(projectId);

            TaxRateInfo tm = TaxRateManager.GetModel(projectInfo.ContractTaxID.Value);

            //decimal invoicePrice = decimal.Parse(txtInvoicePrice.Text);

            string price = (invoicePrice - (invoicePrice / (1 + tm.InvoiceRate.Value) * tm.InvoiceRate.Value)).ToString("#,##0.00");
            string tax = (invoicePrice / (1 + tm.InvoiceRate.Value) * tm.InvoiceRate.Value).ToString("#,##0.00");
            Hashtable hs = new Hashtable();
            hs.Add("price", price);
            hs.Add("tax", tax);
            return hs;

        }

        private bool SaveInfo(int status)
        {
            InvoiceInfo model = InvoiceManager.GetModel(invoiceId);
            if (model == null)
            {
                model = new InvoiceInfo();
                model.CreateDate = DateTime.Now;
                model.CreatorEmployeeName = CurrentUser.Name;
                model.CreatorID = int.Parse(CurrentUser.SysID);
                model.CreatorUserCode = CurrentUser.ID;
                model.CreatorUserName = CurrentUser.ITCode;
            }

            model.ProjectId = int.Parse(hidprojectId.Value);

            ProjectInfo projectInfo = ProjectManager.GetModel(model.ProjectId);

            model.ProjectCode = projectInfo.ProjectCode;
            model.InvoiceAmounts = decimal.Parse(txtInvoicePrice.Text);
            model.InvoiceDate = DateTime.Parse(txtInvoiceDate.Text);
            model.GroupId = projectInfo.GroupID.Value;
            model.GroupName = projectInfo.GroupName;
            model.InvoiceCode = txtInvoiceCode.Text;
            model.InvoiceStatus = status;
            model.Remark = txtRemark.Text;

            List<InvoiceDetailInfo> dlist = new List<InvoiceDetailInfo>();
            string dIds = hidpaymentIds.Value.Trim(new char[] { ',' });

            var l = PaymentManager.GetList(" PaymentID in (" + dIds + ")");
            foreach (PaymentInfo p in l)
            {
                InvoiceDetailInfo d = new InvoiceDetailInfo();
                d.Amounts = model.InvoiceAmounts;
                d.CreateDate = DateTime.Now;
                d.InvoiceNo = model.InvoiceCode;
                d.PaymentCode = p.PaymentCode;
                d.PaymentID = p.PaymentID;
                d.ProjectCode = model.ProjectCode;
                d.ProjectID = model.ProjectId;

                dlist.Add(d);
            }

            return InvoiceManager.InsertInvoiceAndDetail(model, dlist);
        }

        protected void btnYes_Click(object sender, EventArgs e)
        {
            SaveInfo(ESP.Finance.Utility.InvoiceStatus.New);
            Response.Redirect("InvoiceList.aspx");
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            SaveInfo(ESP.Finance.Utility.InvoiceStatus.Used);
            Response.Redirect("InvoiceList.aspx");
        }

        protected void btnReturn_Click(object sender, EventArgs e)
        {
            Response.Redirect("InvoiceList.aspx");
        }
    }
}
