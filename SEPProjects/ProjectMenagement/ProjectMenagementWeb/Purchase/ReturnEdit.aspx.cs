using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ESP.Finance.BusinessLogic;
using ESP.Finance.Entity;
using AjaxPro;
using ESP.Finance.Utility;
public partial class Purchase_ReturnEdit : ESP.Finance.WebPage.EditPageForReturn
{
    int returnId = 0;
    //ProjectManagement.BLL.ReturnBLL returnBLL = new ProjectManagement.BLL.ReturnBLL();//请用BizFactory调用
    protected void Page_Load(object sender, EventArgs e)
    {
        AjaxPro.Utility.RegisterTypeForAjax(typeof(Purchase_ReturnEdit));
        this.ddlPaymentType.Attributes.Add("onChange", "selectPaymentType(this.options[this.selectedIndex].value,this.options[this.selectedIndex].text);");

        if (!IsPostBack)
        {
            if (!string.IsNullOrEmpty(Request[ESP.Finance.Utility.RequestName.ReturnID]))
            {
                returnId = Convert.ToInt32(Request[ESP.Finance.Utility.RequestName.ReturnID]);
            }
            BindInfo();
        }
    }

    private void BindInfo()
    {
        ESP.Finance.Entity.ReturnInfo returnModel = ESP.Finance.BusinessLogic.ReturnManager.GetModel(returnId);
        ESP.Purchase.Entity.GeneralInfo generalModel = null;
        if (returnModel != null)
        {
            generalModel = ESP.Purchase.BusinessLogic.GeneralInfoManager.GetModel(returnModel.PRID.Value);
            //ESP.Purchase.Entity.OrderInfo orderModel = ESP.Purchase.BusinessLogic.OrderInfoManager.GetModelByGeneralID(returnModel.PRID.Value);
            ESP.Purchase.Entity.PaymentPeriodInfo paymentPeriod = ESP.Purchase.BusinessLogic.PaymentPeriodManager.GetModelByPN(returnModel.ReturnID);

            TopMessage.Model = generalModel;

            if (generalModel.PRType == 6 && generalModel.HaveInvoice == false && paymentPeriod.TaxTypes != 0)
            {
                hidTaxShow.Value = "1";
                double tax = ESP.Purchase.BusinessLogic.OrderInfoManager.TaxCalculator(double.Parse(returnModel.PreFee.Value.ToString()), 1);
                lblTaxDesc.Text = "个税金额:" + tax.ToString() + ";     税后支付金额:" + (returnModel.PreFee.Value - decimal.Parse(tax.ToString())).ToString();
            }
        }



        if (returnModel.ReturnStatus != (int)ESP.Finance.Utility.PaymentStatus.Save)
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('你没有访问该数据的权限！');window.location.href='Default.aspx';", true);
        }
        hidPrID.Value = returnModel.PRID.ToString();
        hidProjectID.Value = returnModel.ProjectID.ToString();
        lblApplicant.Text = returnModel.RequestEmployeeName;
        lblApplicant.Attributes["onclick"] = "javascript:ShowMsg('" + ESP.Web.UI.PageBase.GetUserInfo(returnModel.RequestorID.Value) + "');";
        lblBeginDate.Text = returnModel.PreBeginDate == null ? "" : returnModel.PreBeginDate.Value.ToString("yyyy-MM-dd");
        // lblEndDate.Text = returnModel.PreEndDate == null ? "" : returnModel.PreEndDate.Value.ToString("yyyy-MM-dd");
        lblInceptDate.Text = returnModel.ReturnPreDate == null ? "" : returnModel.ReturnPreDate.Value.ToString("yyyy-MM-dd");
        lblInceptPrice.Text = returnModel.PreFee == null ? "" : returnModel.PreFee.Value.ToString("#,##0.00");
        lblPeriodType.Text = returnModel.PaymentTypeName;
        if (returnModel.ReturnType == (int)ESP.Purchase.Common.PRTYpe.CommonPR || returnModel.ReturnType == null)
            lblPRNo.Text = "<a href='" + ESP.Configuration.ConfigurationManager.SafeAppSettings["PurchaseServer"] + "Purchase\\Requisition\\OrderDetailTab.aspx?GeneralID=" + returnModel.PRID.ToString() + "'style='cursor: hand' target='_blank'>" + returnModel.PRNo + "</a>";
        else
            lblPRNo.Text = returnModel.PRNo;
        lblProjectCode.Text = returnModel.ProjectCode;
        lblReturnCode.Text = returnModel.ReturnCode;
        txtReturnContent.Text = returnModel.ReturnContent;
        txtRemark.Text = returnModel.Remark;
        lblStatus.Text = ESP.Finance.Utility.ReturnPaymentType.ReturnStatusString(returnModel.ReturnStatus.Value, 0, returnModel.IsDiscount);

        // txtBeginDate.Text = returnModel.PreBeginDate == null ? "" : returnModel.PreBeginDate.Value.ToString("yyyy-MM-dd");
        //del by gew 账期只留起始，结束不留
        //txtEndDate.Text = returnModel.PreEndDate == null ? "" : returnModel.PreEndDate.Value.ToString("yyyy-MM-dd");
        txtFee.Text = returnModel.PreFee == null ? "0.00" : returnModel.PreFee.Value.ToString("#,##0.00");
        //如果重汇过，bankcancel表中会有最新的供应商信息，从那里获取
        IList<ESP.Finance.Entity.BankCancelInfo> cancelList = ESP.Finance.BusinessLogic.BankCancelManager.GetList(" ReturnID=" + returnModel.ReturnID.ToString() + " and (ordertype is null or ordertype=1 )");
        //无重汇的单子直接从视图获取
        ESP.Finance.Entity.ReturnGeneralInfoListViewInfo vmodel = ESP.Finance.BusinessLogic.ReturnGeneralInfoListViewManager.GetModel(returnModel.ReturnID);
        if (returnModel.ReturnType != (int)ESP.Purchase.Common.PRTYpe.PN_ForeGift)
        {
            if (cancelList != null && cancelList.Count > 0)
            {
                this.lblSupplierName.Text = cancelList[cancelList.Count - 1].OldBankAccountName;
                this.txtSupplierBank.Text = cancelList[cancelList.Count - 1].NewBankName;
                this.txtSupplierAccount.Text = cancelList[cancelList.Count - 1].NewBankAccount;
            }
            else if (vmodel != null)
            {
                this.lblSupplierName.Text = vmodel.Account_name;
                this.txtSupplierBank.Text = vmodel.Account_bank;
                this.txtSupplierAccount.Text = vmodel.Account_number;
            }
        }
        else
        {
            this.lblSupplierName.Text = returnModel.SupplierName;
            this.txtSupplierBank.Text = returnModel.SupplierBankName;
            this.txtSupplierAccount.Text = returnModel.SupplierBankAccount;
        }
        if (returnModel.PaymentTypeID == null || returnModel.PaymentTypeID.Value == 0)
            this.hidPaymentTypeID.Value = "";
        else
            this.hidPaymentTypeID.Value = returnModel.PaymentTypeID.Value.ToString() + "," + returnModel.PaymentTypeName;
    }

    protected void txtFee_TextChanged(object sender, EventArgs e)
    {
        if (hidTaxShow.Value == "1")
        {
            double tax = ESP.Purchase.BusinessLogic.OrderInfoManager.TaxCalculator(double.Parse(txtFee.Text), 1);
            lblTaxDesc.Text = "个税金额:" + tax.ToString() + ";     税后支付金额:" + (decimal.Parse(txtFee.Text) - decimal.Parse(tax.ToString())).ToString();
        }
    }
    protected void btnReturn_Click(object sender, EventArgs e)
    {
        Response.Redirect("/Edit/ReturnTabEdit.aspx");
    }

    private ESP.Framework.Entity.DepartmentInfo GetRootDepartmentID(int deptid)
    {
        ESP.Framework.Entity.DepartmentInfo model = null;
        ESP.Framework.Entity.DepartmentInfo departmentInfo = ESP.Framework.BusinessLogic.DepartmentManager.Get(deptid);
        if (!string.IsNullOrEmpty(departmentInfo.Description))
        {
            model = ESP.Framework.BusinessLogic.DepartmentManager.Get(int.Parse(departmentInfo.Description));
        }
        return model;
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        string NoFADept = System.Configuration.ConfigurationManager.AppSettings["DoNotNeedFA"].ToString();
        ESP.Finance.Entity.ReturnInfo returnModel = ESP.Finance.BusinessLogic.ReturnManager.GetModel(Convert.ToInt32(Request[ESP.Finance.Utility.RequestName.ReturnID]));
        ESP.Finance.Entity.ProjectInfo projectModel = ESP.Finance.BusinessLogic.ProjectManager.GetModelByProjectCode(returnModel.ProjectCode);
        ESP.Purchase.Entity.GeneralInfo generalModel = null;

        if (string.IsNullOrEmpty(this.txtFee.Text) || Convert.ToDecimal(this.txtFee.Text) > returnModel.PreFee.Value)
        {
            ClientScript.RegisterStartupScript(typeof(string), "", "alert('预计付款金额不能大于原申请金额!');", true);
            return;
        }

        generalModel = ESP.Purchase.BusinessLogic.GeneralInfoManager.GetModel(returnModel.PRID.Value);
        //如果不是GM项目，且PR提交日期小于6月25日关闭日期，且项目号已经关闭
        if (returnModel.ReturnType != (int)ESP.Purchase.Common.PRTYpe.PROJECT_Media && returnModel.ReturnType != (int)ESP.Purchase.Common.PRTYpe.PR_MediaFA && returnModel.ReturnType != (int)ESP.Purchase.Common.PRTYpe.PR_PriFA && returnModel.ReturnType != (int)ESP.Purchase.Common.PRTYpe.MediaPR && returnModel.ReturnType != (int)ESP.Purchase.Common.PRTYpe.PrivatePR)
        {
            generalModel = ESP.Purchase.BusinessLogic.GeneralInfoManager.GetModel(returnModel.PRID.Value);
            if (projectModel != null)
            {
                if (CheckerManager.CheckPNOver(returnModel))
                {
                    ClientScript.RegisterStartupScript(typeof(string), "", "alert('已经超出项目总成本，请检查!');", true);
                    return;
                }
            }
        }
        //returnModel.PreBeginDate = DateTime.Parse(txtBeginDate.Text);
        returnModel.PreFee = decimal.Parse(txtFee.Text.Replace(",", ""));
        returnModel.ReturnContent = txtReturnContent.Text.Trim();
        returnModel.Remark = txtRemark.Text.Trim();

        try
        {
            if (ESP.Finance.BusinessLogic.ReturnManager.Payment(ESP.Finance.Utility.PaymentStatus.Submit, returnModel) == ESP.Finance.Utility.UpdateResult.Succeed)
            {
                returnModel = ESP.Finance.BusinessLogic.ReturnManager.GetModel(Convert.ToInt32(Request[ESP.Finance.Utility.RequestName.ReturnID]));
                if (!string.IsNullOrEmpty(this.hidPaymentTypeID.Value))
                {
                    string[] strs = this.hidPaymentTypeID.Value.Split(',');
                    returnModel.PaymentTypeID = Convert.ToInt32(strs[0]);
                    returnModel.PaymentTypeName = strs[1];
                    if (returnModel.PaymentTypeID == 8)//冲抵押金
                    {
                        returnModel.ReturnType = (int)ESP.Purchase.Common.PRTYpe.PN_KillForeGift;
                    }
                    else if (returnModel.PaymentTypeID == 9)//冲抵现金
                    {
                        returnModel.ReturnType = (int)ESP.Purchase.Common.PRTYpe.PN_KillCash;
                    }
                }
                returnModel.Remark = txtRemark.Text.Trim();

                int rootdept = this.GetRootDepartmentID(returnModel.DepartmentID.Value).DepartmentID;

                if (generalModel.ValueLevel == 1 && returnModel.NeedPurchaseAudit == false && ((NoFADept.IndexOf("," + returnModel.DepartmentID.ToString() + ",") >= 0) || ((rootdept == 19 && (returnModel.ProjectCode.IndexOf("GM*") >= 0 || returnModel.ProjectCode.IndexOf("*GM") >= 0 || returnModel.ProjectCode.IndexOf(ESP.Finance.Configuration.ConfigurationManager.ShunYaShortEN) >= 0)))))
                {

                    ESP.Finance.Entity.BranchInfo branchModel = ESP.Finance.BusinessLogic.BranchManager.GetModelByCode(returnModel.ProjectCode.Substring(0, 1));
                    int FirstFinanceID = branchModel.FirstFinanceID;
                    //增加部门判断,N的不同部门对应不同的第一级财务审批人
                    ESP.Finance.Entity.BranchDeptInfo branchdept = ESP.Finance.BusinessLogic.BranchDeptManager.GetModel(branchModel.BranchID, returnModel.DepartmentID.Value);
                    if (branchdept != null)
                        FirstFinanceID = branchdept.FianceFirstAuditorID;
                    ESP.Compatible.Employee financeEmp = new ESP.Compatible.Employee(FirstFinanceID);
                    //付款申请审核人日志
                    ESP.Finance.Entity.ReturnAuditHistInfo FinanceModel = new ESP.Finance.Entity.ReturnAuditHistInfo();
                    FinanceModel.ReturnID = returnModel.ReturnID;
                    FinanceModel.AuditType = ESP.Finance.Utility.auditorType.operationAudit_Type_Financial;
                    FinanceModel.AuditorUserID = int.Parse(financeEmp.SysID);
                    FinanceModel.AuditorUserCode = financeEmp.ID;
                    FinanceModel.AuditorEmployeeName = financeEmp.Name;
                    FinanceModel.AuditorUserName = financeEmp.ITCode;
                    FinanceModel.AuditeStatus = (int)ESP.Finance.Utility.AuditHistoryStatus.UnAuditing;
                    //付款申请表内的审批人记录
                    returnModel.PaymentUserID = int.Parse(financeEmp.SysID);
                    returnModel.PaymentEmployeeName = financeEmp.Name;
                    returnModel.PaymentCode = financeEmp.ID;
                    returnModel.PaymentUserName = financeEmp.ITCode;
                    returnModel.ReturnStatus = (int)ESP.Finance.Utility.PaymentStatus.MajorAudit;
                    ESP.Finance.BusinessLogic.ReturnManager.Update(returnModel);
                    ESP.Finance.BusinessLogic.ReturnAuditHistManager.DeleteByReturnID(returnId);
                    ESP.Finance.BusinessLogic.ReturnAuditHistManager.Add(FinanceModel);

                    string exMail = string.Empty;
                    try
                    {
                        ESP.Finance.Utility.SendMailHelper.SendMailReturnCommit(returnModel, returnModel.RequestEmployeeName, financeEmp.Name, "", financeEmp.EMail);
                    }
                    catch
                    {
                        exMail = "(邮件发送失败!)";
                    }
                    ShowCompleteMessage("已提交至财务人员" + financeEmp.Name + "！" + exMail, "/Edit/ReturnTabEdit.aspx");
                }
                else
                {
                    if (generalModel.ValueLevel == 1 && returnModel.NeedPurchaseAudit == true)
                    {
                        ESP.Compatible.Employee AuditEmp = new ESP.Compatible.Employee(getAuditor(generalModel));
                        ESP.Finance.Entity.ReturnAuditHistInfo AuditModel = new ESP.Finance.Entity.ReturnAuditHistInfo();
                        AuditModel.ReturnID = returnId;
                        AuditModel.AuditType = ESP.Finance.Utility.auditorType.purchase_first;
                        AuditModel.AuditorUserID = int.Parse(AuditEmp.SysID);
                        AuditModel.AuditorUserCode = AuditEmp.ID;
                        AuditModel.AuditorEmployeeName = AuditEmp.Name;
                        AuditModel.AuditorUserName = AuditEmp.ITCode;
                        AuditModel.AuditeStatus = (int)ESP.Finance.Utility.AuditHistoryStatus.UnAuditing;
                        ESP.Finance.BusinessLogic.ReturnAuditHistManager.DeleteByReturnID(returnModel.ReturnID);
                        ESP.Finance.BusinessLogic.ReturnAuditHistManager.Add(AuditModel);

                        returnModel.PaymentUserID = int.Parse(AuditEmp.SysID);
                        returnModel.PaymentEmployeeName = AuditEmp.Name;
                        returnModel.PaymentCode = AuditEmp.ID;
                        returnModel.PaymentUserName = AuditEmp.ITCode;
                        returnModel.ReturnStatus = (int)ESP.Finance.Utility.PaymentStatus.PurchaseFirst;

                        ESP.Finance.BusinessLogic.ReturnManager.Update(returnModel);
                        string exMail = string.Empty;
                        try
                        {
                            ESP.Finance.Utility.SendMailHelper.SendMailReturnCommit(returnModel, returnModel.RequestEmployeeName, AuditEmp.Name, "", AuditEmp.EMail);
                        }
                        catch
                        {
                            exMail = "(邮件发送失败!)";
                        }

                        ShowCompleteMessage("已提交至采购人员" + AuditEmp.Name + "！" + exMail, "/Edit/ReturnTabEdit.aspx");
                    }
                    else
                    {
                        //int[] depts = new ESP.Compatible.Employee(int.Parse(CurrentUser.SysID)).GetDepartmentIDs();
                        // ESP.Framework.Entity.OperationAuditManageInfo auditModel = ESP.Framework.BusinessLogic.OperationAuditManageManager.GetModelByUserId(returnModel.RequestorID.Value);
                        ESP.Framework.Entity.OperationAuditManageInfo auditModel = null;

                        if (returnModel.ProjectID != null && returnModel.ProjectID.Value != 0)
                        {
                            auditModel = ESP.Framework.BusinessLogic.OperationAuditManageManager.GetModelByProjectId(returnModel.ProjectID.Value);
                        }
                        if (auditModel == null)
                            auditModel = ESP.Framework.BusinessLogic.OperationAuditManageManager.GetModelByUserId(returnModel.RequestorID.Value); ;

                        if (auditModel == null)
                            auditModel = ESP.Framework.BusinessLogic.OperationAuditManageManager.GetModelByDepId(returnModel.DepartmentID.Value);

                        if (auditModel != null)
                        {
                            if (returnModel.ReturnType == (int)ESP.Purchase.Common.PRTYpe.PrivatePR)
                                returnModel.PaymentUserID = auditModel.ManagerId;
                            else
                                returnModel.PaymentUserID = auditModel.DirectorId;
                        }
                        returnModel.SupplierName = this.lblSupplierName.Text;
                        returnModel.SupplierBankName = this.txtSupplierBank.Text;
                        returnModel.SupplierBankAccount = this.txtSupplierAccount.Text;
                        if (returnModel.PRID != null && returnModel.PRID.Value != 0)
                        {
                            ESP.Purchase.Entity.GeneralInfo gmodel = ESP.Purchase.BusinessLogic.GeneralInfoManager.GetModel(returnModel.PRID.Value);
                            gmodel.account_bank = this.txtSupplierBank.Text;
                            gmodel.account_number = this.txtSupplierAccount.Text;
                            ESP.Purchase.BusinessLogic.GeneralInfoManager.Update(gmodel);
                        }
                        ESP.Finance.BusinessLogic.ReturnManager.Update(returnModel);
                        ESP.Finance.BusinessLogic.ReturnAuditHistManager.DeleteByReturnID(returnModel.ReturnID);
                        Response.Redirect("SetAuditor.aspx?" + RequestName.ReturnID + "=" + Request[ESP.Finance.Utility.RequestName.ReturnID]);
                        //ShowCompleteMessage("提交成功！", "ReturnList.aspx");
                    }
                }
            }
            else
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('保存失败！');", true);
                //ShowCompleteMessage("提交失败！", "ReturnList.aspx");
            }
        }
        catch (Exception ex)
        {
            ESP.Logging.Logger.Add(string.Format("{0}对F_Return表中的id={1}的数据完成 {2} 的操作", CurrentUser.Name, Request[RequestName.ReturnID], "付款申请编辑"), "付款申请", ESP.Logging.LogLevel.Error, ex);
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {

        ESP.Finance.Entity.ReturnInfo returnModel = ESP.Finance.BusinessLogic.ReturnManager.GetModel(Convert.ToInt32(Request[ESP.Finance.Utility.RequestName.ReturnID]));
        if (string.IsNullOrEmpty(this.txtFee.Text) || Convert.ToDecimal(this.txtFee.Text) > returnModel.PreFee.Value)
        {
            ClientScript.RegisterStartupScript(typeof(string), "", "alert('预计付款金额不能大于原申请金额!');", true);
            return;
        }
        //returnModel.PreBeginDate = DateTime.Parse(txtBeginDate.Text);

        returnModel.PreEndDate = returnModel.PreBeginDate;
        returnModel.ReturnContent = txtReturnContent.Text.Trim();
        returnModel.Remark = txtRemark.Text.Trim();
        if (!string.IsNullOrEmpty(this.hidPaymentTypeID.Value))
        {
            string[] strs = this.hidPaymentTypeID.Value.Split(',');
            returnModel.PaymentTypeID = Convert.ToInt32(strs[0]);
            returnModel.PaymentTypeName = strs[1];
        }
        returnModel.PreFee = decimal.Parse(txtFee.Text.Replace(",", ""));
        ESP.Finance.BusinessLogic.ReturnManager.Update(returnModel);
        ShowCompleteMessage("保存成功！", "/Edit/ReturnTabEdit.aspx");
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





    private int getAuditor(ESP.Purchase.Entity.GeneralInfo generalModel)
    {
        List<ESP.Purchase.Entity.OrderInfo> orderList = ESP.Purchase.BusinessLogic.OrderInfoManager.GetListByGeneralId(generalModel.id);
        ESP.Purchase.Entity.SupplierInfo supplierModel = null;
        ESP.Purchase.Entity.TypeInfo typeInfo = null;
        // ESP.Framework.Entity.OperationAuditManageInfo operation = ESP.Framework.BusinessLogic.OperationAuditManageManager.GetModelByUserId(generalModel.requestor);
        ESP.Framework.Entity.OperationAuditManageInfo operation = null;

        if (generalModel.Project_id != 0)
        {
            operation = ESP.Framework.BusinessLogic.OperationAuditManageManager.GetModelByProjectId(generalModel.Project_id);
        }
        if (operation == null)
            operation = ESP.Framework.BusinessLogic.OperationAuditManageManager.GetModelByUserId(generalModel.requestor); ;

        if (operation == null)
            operation = ESP.Framework.BusinessLogic.OperationAuditManageManager.GetModelByDepId(generalModel.Departmentid);

        foreach (ESP.Purchase.Entity.OrderInfo orderModel in orderList)
        {
            if (orderModel.supplierId > 0)
            {
                supplierModel = ESP.Purchase.BusinessLogic.SupplierManager.GetModel(orderModel.supplierId);
                typeInfo = ESP.Purchase.BusinessLogic.TypeManager.GetModel(orderModel.producttype);
                break;
            }
        }
        if (supplierModel != null)
        {
            if (operation != null && operation.PurchaseAuditorId != 0)
            {
                return operation.PurchaseAuditorId;
            }
            else
            {
                ESP.Framework.Entity.DepartmentInfo deptModel = GetRootDepartmentID(generalModel.Departmentid);

                if (deptModel.DepartmentID == 19)
                {
                    return typeInfo.BJPaymentUserID;
                }
                else if (deptModel.DepartmentID == 17)
                {
                    return typeInfo.SHPaymentUserID;
                }
                else if (deptModel.DepartmentID == 18)
                {
                    return typeInfo.GZPaymentUserID;
                }
                else
                {
                    return typeInfo.BJPaymentUserID;
                }
            }
        }

        return 0;

    }

}
