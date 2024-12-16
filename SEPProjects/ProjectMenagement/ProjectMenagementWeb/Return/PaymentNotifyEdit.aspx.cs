﻿using System;
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
public partial class Return_PaymentNotifyEdit : ESP.Finance.WebPage.EditPageForProject
{
    int PaymentID = 0;
    ESP.Finance.Entity.PaymentInfo PaymentModel = null;
    ESP.Finance.Entity.ProjectInfo ProjectModel = null;
    protected void Page_Load(object sender, EventArgs e)
    {
        AjaxPro.Utility.RegisterTypeForAjax(typeof(Return_PaymentNotifyEdit));
        this.ddlPaymentType.Attributes.Add("onChange", "selectPaymentType(this.options[this.selectedIndex].value,this.options[this.selectedIndex].text);");
        PaymentID = Convert.ToInt32(Request[ESP.Finance.Utility.RequestName.PaymentID]);
        PaymentModel = ESP.Finance.BusinessLogic.PaymentManager.GetModel(PaymentID);
        ProjectModel = ESP.Finance.BusinessLogic.ProjectManager.GetModelWithOutDetailList(PaymentModel.ProjectID);
        if (!IsPostBack)
        {
            if (!string.IsNullOrEmpty(Request[ESP.Finance.Utility.RequestName.PaymentID]))
            {
               
                if (ProjectModel != null)
                    TopMessage.ProjectModel = ProjectModel;
                InitPage(ProjectModel,PaymentModel);
            }
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
        ESP.Compatible.Employee emp=new ESP.Compatible.Employee(pmodel.ApplicantUserID);
        this.lblResponserEmail.Text = emp.EMail;
        this.lblResponserMobile.Text = emp.Mobile;
        this.lblResponserTel.Text = emp.Telephone;
        this.txtFactDate.Text = model.PaymentPreDate.ToString("yyyy-MM-dd");
        this.txtFactAmount.Text = model.PaymentBudget.Value.ToString("#,##0.00");
        this.txtRemark.Text = model.Remark;
        string paymenttypeid=model.PaymentTypeID==null?"":model.PaymentTypeID.Value.ToString();
        if (!string.IsNullOrEmpty(paymenttypeid))
            this.hidPaymentTypeID.Value = paymenttypeid + "," + model.PaymentTypeName;
    }
    protected void btnNext_Click(object sender, EventArgs e)
    {
        Response.Redirect("/Edit/NotifyTabEdit.aspx");
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        SavePaymentNotify();
    }

    private int SavePaymentNotify()
    {
        PaymentModel.PaymentFactDate = Convert.ToDateTime(this.txtFactDate.Text);
        PaymentModel.PaymentFee = Convert.ToDecimal(this.txtFactAmount.Text);
        PaymentModel.PaymentStatus = (int)ESP.Finance.Utility.ReturnStatus.Submit;
        string[] strPayment = null;
        string AuditorID = ",";
        if (!string.IsNullOrEmpty(this.hidPaymentTypeID.Value))
        {
            strPayment = this.hidPaymentTypeID.Value.Split(',');
            PaymentModel.PaymentTypeID = Convert.ToInt32(strPayment[0]);
        }
        PaymentModel.PaymentTypeName = strPayment[1];
        PaymentModel.PaymentCode = ESP.Finance.BusinessLogic.PaymentManager.CreateCode(PaymentModel.PaymentID);
        ESP.Finance.Utility.UpdateResult result= ESP.Finance.BusinessLogic.PaymentManager.Update(PaymentModel);
        if (result == ESP.Finance.Utility.UpdateResult.Succeed)
        {
            
            List<ESP.Finance.Entity.PaymentAuditHistInfo> auditlist = new List<PaymentAuditHistInfo>();
            ESP.Framework.Entity.OperationAuditManageInfo auditModel = ESP.Framework.BusinessLogic.OperationAuditManageManager.GetModelByDepId(ProjectModel.GroupID.Value);
            ESP.Compatible.Employee emp = new ESP.Compatible.Employee(auditModel.DirectorId);
            if (AuditorID.IndexOf("," + emp.SysID + ",") < 0)
            {
                ESP.Finance.Entity.PaymentAuditHistInfo model = new PaymentAuditHistInfo();
                model.PaymentID = PaymentModel.PaymentID;
                model.AuditType = (int)ESP.Finance.Utility.auditorType.operationAudit_Type_ZJSP;
                model.AuditorEmployeeName = emp.Name;
                model.AuditorUserCode = emp.ID;
                model.AuditorUserID = Convert.ToInt32(emp.SysID);
                model.AuditorUserName = emp.ITCode;
                model.AuditeStatus = (int)ESP.Finance.Utility.AuditHistoryStatus.UnAuditing;
                auditlist.Add(model);
                AuditorID += emp.SysID + ",";
            }
              
            ESP.Finance.Entity.PaymentAuditHistInfo modelFinance = new PaymentAuditHistInfo();
            modelFinance.PaymentID = PaymentModel.PaymentID;
            modelFinance.AuditeDate = DateTime.Now;
            modelFinance.AuditType = (int)ESP.Finance.Utility.auditorType.operationAudit_Type_Financial;
            ESP.Compatible.Employee empFinance = null;
            //通过BranchCode 获取付款通知的财务审核人
            ESP.Finance.Entity.BranchInfo BranchModel = ESP.Finance.BusinessLogic.BranchManager.GetModel(ProjectModel.BranchID.Value);
            empFinance = new ESP.Compatible.Employee(BranchModel.PaymentAccounter);
            
            if (AuditorID.IndexOf("," + empFinance.SysID + ",") < 0)
            {
                modelFinance.AuditorEmployeeName = empFinance.Name;
                modelFinance.AuditorUserCode = empFinance.ID;
                modelFinance.AuditorUserID = Convert.ToInt32(empFinance.SysID);
                modelFinance.AuditorUserName = empFinance.ITCode;
                modelFinance.AuditeStatus = (int)ESP.Finance.Utility.AuditHistoryStatus.UnAuditing;
                auditlist.Add(modelFinance);
                AuditorID += empFinance.SysID + ",";
                ESP.Finance.BusinessLogic.PaymentAuditHistManager.Add(auditlist);
            }
            ClientScript.RegisterStartupScript(typeof(string), new Guid().ToString(), "window.location.href='/Edit/NotifyTabEdit.aspx';alert('付款通知提交成功!');", true);
        }
        else
        {
            ClientScript.RegisterStartupScript(typeof(string), new Guid().ToString(), "alert("+result.ToString()+");", true);
        }
        return 0;
    }

    protected void gvPayment_RowCommand(object sender, GridViewCommandEventArgs e)
    {
       
    }

    protected void gvPayment_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header)
        {

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
