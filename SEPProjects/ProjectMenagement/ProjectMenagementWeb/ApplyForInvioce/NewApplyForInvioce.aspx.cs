using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ESP.Finance.BusinessLogic;
using ESP.Finance.Entity;

namespace FinanceWeb.ApplyForInvioce
{
    public partial class NewApplyForInvioce : ESP.Web.UI.PageBase
    {
        private static int ProjectID;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!string.IsNullOrEmpty(Request["ProjectID"]))
                {
                    ProjectID = int.Parse(Request["ProjectID"]);
                    ProjectInfo project = ProjectManager.GetModel(ProjectID);
                    ddlFlowTo.Items.Insert(0, new ListItem(ESP.Finance.Utility.Common.ApplyForInvioceFlowTo_Names[0], ((int)ESP.Finance.Utility.Common.ApplyForInvioce_FlowTo.Customer).ToString()));
                    ddlFlowTo.Items.Insert(1, new ListItem(ESP.Finance.Utility.Common.ApplyForInvioceFlowTo_Names[2], ((int)ESP.Finance.Utility.Common.ApplyForInvioce_FlowTo.CustomerRebate).ToString()));
                    if (project.isRecharge)
                    {
                        ddlFlowTo.Items.Insert(2, new ListItem(ESP.Finance.Utility.Common.ApplyForInvioceFlowTo_Names[1], ((int)ESP.Finance.Utility.Common.ApplyForInvioce_FlowTo.Media).ToString()));

                        IList<ProjectMediaInfo> projectMediaList = ProjectMediaManager.GetList(" ProjectId="+project.ProjectId);
                        for(int i=0;i<projectMediaList.Count ;i++)
                        {
                            ddlMedia.Items.Insert(i, new ListItem(projectMediaList[i].MediaName,projectMediaList[i].SupplierId.ToString() ));
                        }
                    }
                    for (int i = 0; i < ESP.Finance.Utility.Common.InvoiceType_Names.Length; i++)
                    {
                        ddlInvoiceType.Items.Insert(i, new ListItem(ESP.Finance.Utility.Common.InvoiceType_Names[i], i.ToString()));
                    }
                    InitInvoiceInfo();
                }
            }
        }

        private void InitInvoiceInfo()
        {
            txtInvoiceTitle.Text = "";
            txtBankName.Text = "";
            txtBankNum.Text = "";
            txtTIN.Text = "";
            txtAddressPhone.Text = "";

            ProjectInfo project = ProjectManager.GetModel(ProjectID);
            if (int.Parse(ddlFlowTo.SelectedValue) == (int)ESP.Finance.Utility.Common.ApplyForInvioce_FlowTo.Media)
            {
                txtInvoiceTitle.Text = ddlMedia.SelectedItem.Text;
            }
            else
            {
                txtInvoiceTitle.Text = project.Customer.NameCN1;
            }
            if (!string.IsNullOrEmpty(txtInvoiceTitle.Text))
            {
                ApplyForInvioceInfo lastInfo = ApplyForInvioceManager.GetList(" InvoiceTitle='" + txtInvoiceTitle.Text.Trim()+"'").OrderByDescending(x => x.Id).FirstOrDefault();
                if (lastInfo != null)
                {
                    txtInvoiceTitle.Text = lastInfo.InvoiceTitle;
                    txtBankName.Text = lastInfo.BankName;
                    txtBankNum.Text = lastInfo.BankNum;
                    txtTIN.Text = lastInfo.TIN;
                    txtAddressPhone.Text = lastInfo.AddressPhone;
                    ddlInvoiceType.SelectedValue = ((int)lastInfo.InvoiceType).ToString();
                }
            }
        }

        private void saveInfo()
        {
            ApplyForInvioceInfo info = new ApplyForInvioceInfo();
            info.ProjectId = ProjectID;
            info.InviocePrice = decimal.Parse(txtInviocePrice.Text.Trim());
            info.Remark = txtRemark.Text.Trim();
            info.Status = (int)ESP.Finance.Utility.ApplyForInvioceStatus.Status.Wait_Submit;
            info.CreateDate = DateTime.Now;
            info.CreatorUserId = int.Parse(CurrentUser.SysID);
            info.CreatorUserName = CurrentUser.Name;
            info.FlowTo = (ESP.Finance.Utility.Common.ApplyForInvioce_FlowTo)int.Parse(ddlFlowTo.SelectedValue);

            info.InvoiceType = (ESP.Finance.Utility.Common.Invoice_Type)int.Parse(ddlInvoiceType.SelectedValue);
            info.InvoiceTitle = txtInvoiceTitle.Text.Trim();
            info.BankName = txtBankName.Text.Trim();
            info.BankNum = txtBankNum.Text.Trim();
            info.TIN = txtTIN.Text.Trim();
            info.AddressPhone = txtAddressPhone.Text.Trim();

            if (ddlMedia.Visible)
                info.SupplierId = int.Parse(ddlMedia.SelectedValue);

            if (int.Parse(ddlFlowTo.SelectedValue) == (int)ESP.Finance.Utility.Common.ApplyForInvioce_FlowTo.Customer)
            {
                //客户开票金额不能大于项目总金额
                ProjectInfo project = ProjectManager.GetModel(ProjectID);
                decimal invoiceTotal = ApplyForInvioceManager.GetList(" ProjectId=" + ProjectID + " and FlowTo=" + (int)ESP.Finance.Utility.Common.ApplyForInvioce_FlowTo.Customer
                    + " and Status<>" + (int)ESP.Finance.Utility.ApplyForInvioceStatus.Status.Rejected).Sum(x => x.InviocePrice);
                if ((invoiceTotal + info.InviocePrice) > project.TotalAmount)
                {
                    ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('客户开票金额不能大于项目总金额！');", true);
                    return;
                }
            }

            ApplyForInvioceManager.Add(info);
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            saveInfo();
            Response.Redirect("/ApplyForInvioce/ApplyForInvioceEdit.aspx?ProjectID=" + Request["ProjectID"] + "&backurl=" + Request["backurl"]);
        }

        protected void btnNext_Click(object sender, EventArgs e)
        {
            saveInfo();
            Response.Redirect("/ApplyForInvioce/NewApplyForInvioce.aspx?ProjectID=" + Request["ProjectID"] + "&backurl=" + Request["backurl"]);
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("/ApplyForInvioce/ApplyForInvioceEdit.aspx?ProjectID=" + Request["ProjectID"] + "&backurl=" + Request["backurl"]);
        }

        protected void ddlFlowTo_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlMedia.Visible = ddlFlowTo.SelectedValue == ((int)ESP.Finance.Utility.Common.ApplyForInvioce_FlowTo.Media).ToString();
            if (int.Parse(ddlFlowTo.SelectedValue) == (int)ESP.Finance.Utility.Common.ApplyForInvioce_FlowTo.Customer)
            {
                CompareValidator1.Operator = ValidationCompareOperator.GreaterThan;
                CompareValidator1.ErrorMessage = "客户发票金额应大于0";
            }
            else
            {
                CompareValidator1.Operator = ValidationCompareOperator.LessThan;
                CompareValidator1.ErrorMessage = "返点发票金额应小于0";
            }
            InitInvoiceInfo();
        }

        protected void ddlMedia_SelectedIndexChanged(object sender, EventArgs e)
        {
            InitInvoiceInfo();
        }
    }
}