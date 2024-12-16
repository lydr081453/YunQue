using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ESP.Finance.Entity;
using ESP.Finance.Utility;
using ESP.Finance.BusinessLogic;
namespace FinanceWeb.Purchase
{
    public partial class ReturnFinanceSave : ESP.Web.UI.PageBase
    {
        int returnId = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!string.IsNullOrEmpty(Request[ESP.Finance.Utility.RequestName.ReturnID]))
                {
                    returnId = Convert.ToInt32(Request[ESP.Finance.Utility.RequestName.ReturnID]);
                }
                BindInfo();
            }
        }


        private string GetAuditLog(int Rid)
        {
            ReturnInfo ReturnModel = ESP.Finance.BusinessLogic.ReturnManager.GetModel(Rid);
            if (ReturnModel != null)
            {
                if (ReturnModel.PRID != null && ReturnModel.PRID.Value != 0)
                    TopMessage.Model = ESP.Purchase.BusinessLogic.GeneralInfoManager.GetModel(ReturnModel.PRID.Value);
                if ((ReturnModel.PRID == null || ReturnModel.PRID.Value == 0) && ReturnModel.ProjectID != null && ReturnModel.ProjectID.Value != 0)
                    TopMessage.ProjectModel = ESP.Finance.BusinessLogic.ProjectManager.GetModelWithOutDetailList(ReturnModel.ProjectID.Value);
            }
            IList<ESP.Finance.Entity.AuditLogInfo> histList = ESP.Finance.BusinessLogic.AuditLogManager.GetReturnList(ReturnModel.ReturnID);
            string loginfo = string.Empty;
            foreach (ESP.Finance.Entity.AuditLogInfo model in histList)
            {
                string austatus = string.Empty;
                if (model.AuditStatus == (int)AuditHistoryStatus.PassAuditing)
                {
                    austatus = "审批通过";
                }
                else if (model.AuditStatus == (int)AuditHistoryStatus.TerminateAuditing)
                {
                    austatus = "审批驳回";
                }
                string auditdate = model.AuditDate == null ? "" : model.AuditDate.Value.ToString("yyyy-MM-dd HH:mm:ss");
                loginfo += model.AuditorEmployeeName + "(" + model.AuditorUserName + ")" + austatus + "[" + auditdate + "]" + model.Suggestion + "<br/>";

            }
            return loginfo;
        }

        private void BindInfo()
        {
            ReturnInfo returnModel = ESP.Finance.BusinessLogic.ReturnManager.GetModel(returnId);
            PaymentTypeInfo paymentType = null;
            if (returnModel.PaymentTypeID != null)
                paymentType = ESP.Finance.BusinessLogic.PaymentTypeManager.GetModel(returnModel.PaymentTypeID.Value);
            hidPrID.Value = returnModel.PRID.ToString();
            hidProjectID.Value = returnModel.ProjectID.ToString();
            lblApplicant.Text = returnModel.RequestEmployeeName;

            lblApplicant.Attributes["onclick"] = "javascript:ShowMsg('" + ESP.Web.UI.PageBase.GetUserInfo(returnModel.RequestorID.Value) + "');";
            lblBeginDate.Text = returnModel.PreBeginDate == null ? "" : returnModel.PreBeginDate.Value.ToString("yyyy-MM-dd");
            // lblEndDate.Text = returnModel.PreEndDate == null ? "" : returnModel.PreEndDate.Value.ToString("yyyy-MM-dd");
            lblInceptDate.Text = returnModel.ReturnPreDate == null ? "" : returnModel.ReturnPreDate.Value.ToString("yyyy-MM-dd");
            lblInceptPrice.Text = returnModel.PreFee == null ? "" : returnModel.PreFee.Value.ToString("#,##0.00");
            lblPeriodType.Text = returnModel.PaymentTypeName;
            txtPayRemark.Text = returnModel.ReturnContent;
            lblPayCode.Text = returnModel.PaymentTypeCode;
            if (returnModel.DepartmentID != null && returnModel.DepartmentID.Value != 0)
            {
                ESP.Compatible.Department dept = ESP.Compatible.DepartmentManager.GetDepartmentByPK(returnModel.DepartmentID.Value);
                labDepartment.Text = dept.DepartmentName;
            }

            if (returnModel.ReturnType == (int)ESP.Purchase.Common.PRTYpe.CommonPR || returnModel.ReturnType == null)
                lblPRNo.Text = "<a href='" + ESP.Configuration.ConfigurationManager.SafeAppSettings["PurchaseServer"] + "Purchase\\Requisition\\OrderDetailTab.aspx?GeneralID=" + returnModel.PRID.ToString() + "'style='cursor: hand' target='_blank'>" + returnModel.PRNo + "</a>";
            else
                lblPRNo.Text = returnModel.PRNo;
            lblProjectCode.Text = returnModel.ProjectCode;
            lblReturnCode.Text = returnModel.ReturnCode;
            radioInvoice.SelectedValue = returnModel.IsInvoice == null ? "-1" : returnModel.IsInvoice.Value.ToString();
            lblStatus.Text = ESP.Finance.Utility.ReturnPaymentType.ReturnStatusString(returnModel.ReturnStatus.Value, 0, returnModel.IsDiscount);

            ESP.Finance.Entity.ReturnGeneralInfoListViewInfo vmodel = ESP.Finance.BusinessLogic.ReturnGeneralInfoListViewManager.GetModel(returnModel.ReturnID);
            if (vmodel != null && returnModel.ReturnType != (int)ESP.Purchase.Common.PRTYpe.PN_KillForeGift)
            {
                this.lblSupplierName.Text = vmodel.Account_name;
                this.txtSupplierBank.Text = vmodel.Account_bank;
                this.txtSupplierAccount.Text = vmodel.Account_number;
            }

            this.txtFactFee.Text = returnModel.FactFee == null ? "" : returnModel.FactFee.Value.ToString("#,##0.00");

            if (returnModel.ReturnStatus == (int)PaymentStatus.MajorAudit)
            {
                this.txtFactFee.Text = returnModel.PreFee == null ? "" : returnModel.PreFee.Value.ToString("#,##0.00");
            }
            if ((returnModel.ReturnStatus == (int)PaymentStatus.FinanceLevel2) || returnModel.ReturnStatus == (int)PaymentStatus.FinanceLevel3 || returnModel.ReturnStatus == (int)PaymentStatus.WaitReceiving)
            {
                this.txtFactFee.ReadOnly = false;
            }
        }

        protected void btnReturn_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Request[RequestName.BackUrl]))
            {
                Response.Redirect(Request[RequestName.BackUrl]);
            }
            else
            {
                ClientScript.RegisterStartupScript(typeof(string), "", "window.close();", true);
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Request[ESP.Finance.Utility.RequestName.ReturnID]))
            {
                ReturnInfo returnModel = ESP.Finance.BusinessLogic.ReturnManager.GetModel(Convert.ToInt32(Request[ESP.Finance.Utility.RequestName.ReturnID]));
                if (returnModel != null)
                {
                    if (!string.IsNullOrEmpty(this.txtSupplierAccount.Text.Trim()))
                        returnModel.SupplierBankAccount = this.txtSupplierAccount.Text.Trim();

                    if (!string.IsNullOrEmpty(this.txtSupplierBank.Text.Trim()))
                        returnModel.SupplierBankName = this.txtSupplierBank.Text.Trim();

                    returnModel.ReturnContent = this.txtPayRemark.Text;
                }
                if (this.radioInvoice.SelectedIndex >= 0)
                    returnModel.IsInvoice = Convert.ToInt32(this.radioInvoice.SelectedValue);

                if (!string.IsNullOrEmpty(txtFactFee.Text.Trim()))
                {
                    decimal totalamounts = Convert.ToDecimal(txtFactFee.Text.Trim());
                    if (totalamounts > returnModel.PreFee.Value * 110 / 100)
                    {
                        ClientScript.RegisterStartupScript(typeof(string), "", "alert('实际付款金额不能超出原申请金额的10%');", true);
                        return;
                    }
                    else
                        returnModel.FactFee = totalamounts;
                }

                ReturnManager.Update(returnModel);
                if (!string.IsNullOrEmpty(Request[RequestName.BackUrl]))
                {
                    Response.Redirect(Request[RequestName.BackUrl]);
                }
                else
                {
                    ClientScript.RegisterStartupScript(typeof(string), "", "window.close();", true);
                }
            }
        }


        private bool ValidAudited()
        {
            List<SqlParameter> paramlist = new List<SqlParameter>();
            string term = string.Empty;
            string DelegateUsers = string.Empty;
            IList<ESP.Framework.Entity.AuditBackUpInfo> delegates = ESP.Framework.BusinessLogic.AuditBackUpManager.GetModelsByBackUpUserID(Convert.ToInt32(CurrentUser.SysID));
            foreach (ESP.Framework.Entity.AuditBackUpInfo model in delegates)
            {
                DelegateUsers += model.UserID.ToString() + ",";
            }
            DelegateUsers = DelegateUsers.TrimEnd(',');
            if (!string.IsNullOrEmpty(DelegateUsers))
                term = " (ReturnStatus=@status1 or  ReturnStatus=@status2 or ReturnStatus=@status3 or ReturnStatus=@status4 or ReturnStatus=@status5) AND (PaymentUserID=@sysID or PaymentUserID in(" + DelegateUsers + "))";
            else
                term = " (ReturnStatus=@status1 or  ReturnStatus=@status2 or ReturnStatus=@status3 or ReturnStatus=@status4 or ReturnStatus=@status5) AND PaymentUserID=@sysID";
            SqlParameter p3 = new SqlParameter("@status3", System.Data.SqlDbType.Int, 4);
            p3.SqlValue = (int)PaymentStatus.MajorAudit;
            paramlist.Add(p3);
            SqlParameter p1 = new SqlParameter("@status1", System.Data.SqlDbType.Int, 4);
            p1.SqlValue = (int)PaymentStatus.FinanceLevel1;
            paramlist.Add(p1);
            SqlParameter p2 = new SqlParameter("@status2", System.Data.SqlDbType.Int, 4);
            p2.SqlValue = (int)PaymentStatus.FinanceLevel2;
            paramlist.Add(p2);
            SqlParameter p4 = new SqlParameter("@status4", System.Data.SqlDbType.Int, 4);
            p4.SqlValue = (int)PaymentStatus.FinanceLevel3;
            paramlist.Add(p4);
            SqlParameter p5 = new SqlParameter("@status5", System.Data.SqlDbType.Int, 4);
            p5.SqlValue = (int)PaymentStatus.WaitReceiving;
            paramlist.Add(p5);

            SqlParameter p6 = new SqlParameter("@sysID", System.Data.SqlDbType.Int, 4);
            p6.SqlValue = CurrentUser.SysID;
            paramlist.Add(p6);
            IList<ReturnInfo> returnlist = ESP.Finance.BusinessLogic.ReturnManager.GetList(term, paramlist);
            foreach (ReturnInfo pro in returnlist)
            {
                if (pro.ReturnID.ToString() == Request[ESP.Finance.Utility.RequestName.ReturnID])
                {
                    return true;
                }
            }
            return false;
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

        [AjaxPro.AjaxMethod]
        public static List<List<string>> GetBanks(int returnid)
        {
            ReturnInfo model = ESP.Finance.BusinessLogic.ReturnManager.GetModel(returnid);
            string branchcode = string.Empty;
            if (model != null && !string.IsNullOrEmpty(model.ProjectCode))
            {
                branchcode = model.ProjectCode.Substring(0, 1);
            }

            List<List<string>> retlists = new List<List<string>>();
            List<System.Data.SqlClient.SqlParameter> paramlist = new List<System.Data.SqlClient.SqlParameter>();
            paramlist.Add(new System.Data.SqlClient.SqlParameter("@branchcode", branchcode));
            IList<BankInfo> paylist = ESP.Finance.BusinessLogic.BankManager.GetList(" branchcode=@branchcode", paramlist);
            List<string> first = new List<string>();
            first.Add("-1");
            first.Add("请选择..");
            retlists.Add(first);
            foreach (BankInfo item in paylist)
            {
                List<string> i = new List<string>();
                i.Add(item.BankID.ToString());
                i.Add(item.BankName);
                retlists.Add(i);
            }

            return retlists;
        }

        [AjaxPro.AjaxMethod]
        public static List<string> GetPaymentTypeModel(int paymentTypeID)
        {
            List<string> list = new List<string>();
            ESP.Finance.Entity.PaymentTypeInfo model = ESP.Finance.BusinessLogic.PaymentTypeManager.GetModel(paymentTypeID);
            list.Add(model.PaymentTypeID.ToString());
            list.Add(model.PaymentTypeName);
            list.Add(model.IsNeedCode.ToString());
            list.Add(model.IsNeedBank.ToString());
            list.Add(model.Tag.ToString());
            return list;
        }

        [AjaxPro.AjaxMethod]
        public static List<string> GetBankModel(int bankID)
        {
            List<string> list = new List<string>();
            ESP.Finance.Entity.BankInfo bankmodel = ESP.Finance.BusinessLogic.BankManager.GetModel(bankID);
            list.Add(bankmodel.BankID.ToString());
            list.Add(bankmodel.BankName);
            list.Add(bankmodel.BankAccount);
            list.Add(bankmodel.BankAccountName);
            list.Add(bankmodel.Address);

            return list;
        }
    }
}
