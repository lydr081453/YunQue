using System;
using System.Collections.Generic;
using ESP.HumanResource.BusinessLogic;
using ESP.HumanResource.Entity;

public partial class DimissionAuditStatus : ESP.Web.UI.PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (!string.IsNullOrEmpty(Request["dimissionId"]))
            {
                int dimissionId = 0;
                if (int.TryParse(Request["dimissionId"], out dimissionId))
                {
                    InitPage(dimissionId);
                }
            }
        }
    }

    /// <summary>
    /// 初始化离职单审批状态页面信息
    /// </summary>
    /// <param name="dimissionId">离职单编号</param>
    protected void InitPage(int dimissionId)
    {
        DimissionFormInfo dimissionForm = DimissionFormManager.GetModel(dimissionId);
        if (dimissionForm != null)
        {
            List<ESP.HumanResource.Entity.HRAuditLogInfo> list =
                ESP.HumanResource.BusinessLogic.HRAuditLogManager.GetAuditLogInfo(dimissionId, (int)ESP.HumanResource.Common.HRFormType.DimissionForm);
            if (list != null && list.Count > 0)
            {
                foreach (ESP.HumanResource.Entity.HRAuditLogInfo audit in list)
                {
                    switch (audit.AuditLevel)
                    {
                        case (int)ESP.HumanResource.Common.DimissionFormStatus.WaitPreAuditor:  // 预审审批
                            litBiz.Text += "<tr><td width='50%' height='40' align='right'><img style='cursor:pointer;' width='24' height='24' hspace='5' src='" +
                                GetImage(audit.AuditStatus) + "'/></td><td width='50%' align='left'>" +
                                audit.AuditorName + "</td>";
                            break;
                        case (int)ESP.HumanResource.Common.DimissionFormStatus.WaitDirector:  // 待总监审批
                            litBiz.Text += "<tr><td width='50%' height='40' align='right'><img style='cursor:pointer;' width='24' height='24' hspace='5' src='" +
                                GetImage(audit.AuditStatus) + "'/></td><td width='50%' align='left'>" +
                                audit.AuditorName + //this.GetDelegateUser(model.AuditorUserID.Value) + 
                                "</td>";
                            break;
                        case (int)ESP.HumanResource.Common.DimissionFormStatus.WaitReceiver:  //待交接人确认
                            //litBiz.Text = GetBizScript(dimissionForm, list);

                            break;
                        case (int)ESP.HumanResource.Common.DimissionFormStatus.WaitManager:  // 待总经理审批
                            litBiz.Text += "<tr><td width='50%' height='40' align='right'><img style='cursor:pointer' width='24' height='24' hspace='5' src='" +
                                GetImage(audit.AuditStatus) + "'/></td><td width='50%' align='left'>" +
                                audit.AuditorName + //this.GetDelegateUser(model.AuditorUserID.Value) + 
                                "</td>";
                            break;
                        case (int)ESP.HumanResource.Common.DimissionFormStatus.WaitGroupHR:  // 待团队行政审批
                            litGroup.Text += "<tr><td width='50%' height='40' align='right'><img style='cursor:pointer' width='24' height='24' hspace='5' src='" +
                                GetImage(audit.AuditStatus) + "'/></td><td width='50%' align='left'>" +
                                audit.AuditorName + //this.GetDelegateUser(model.AuditorUserID.Value) + 
                                "</td>";
                            break;
                        case (int)ESP.HumanResource.Common.DimissionFormStatus.WaitHRIT:  // 待集团人力资源、IT部审批
                            litHRIT.Text += "<tr><td width='50%' height='40' align='right'><img style='cursor:pointer' width='24' height='24' hspace='5' src='" +
                                GetImage(audit.AuditStatus) + "'/></td><td width='50%' align='left'>" +
                                audit.AuditorName + //this.GetDelegateUser(model.AuditorUserID.Value) + 
                                "</td>";
                            break;
                        case (int)ESP.HumanResource.Common.DimissionFormStatus.WaitHRDirector:  // 待集团人力资源、IT部审批
                            litHRIT.Text += "<tr><td width='50%' height='40' align='right'><img style='cursor:pointer' width='24' height='24' hspace='5' src='" +
                                GetImage(audit.AuditStatus) + "'/></td><td width='50%' align='left'>" +
                                audit.AuditorName + //this.GetDelegateUser(model.AuditorUserID.Value) + 
                                "</td>";
                            break;
                        case (int)ESP.HumanResource.Common.DimissionFormStatus.WaitFinance:  // 待财务审批
                            litFinance.Text += "<tr><td width='50%' height='40' align='right'><img style='cursor:pointer' width='24' height='24' hspace='5' src='" +
                                GetImage(audit.AuditStatus) + "'/></td><td width='50%' align='left'>" +
                                audit.AuditorName + //this.GetDelegateUser(model.AuditorUserID.Value) + 
                                "</td>";
                            break;
                        case (int)ESP.HumanResource.Common.DimissionFormStatus.WaitIT:  // 待IT确认
                            break;
                        case (int)ESP.HumanResource.Common.DimissionFormStatus.WaitAdministration:  // 待集团行政审批
                            litAD.Text += "<tr><td width='50%' height='40' align='right'><img style='cursor:pointer' width='24' height='24' hspace='5' src='" +
                                GetImage(audit.AuditStatus) + "'/></td><td width='50%' align='left'>" +
                                audit.AuditorName + //this.GetDelegateUser(model.AuditorUserID.Value) + 
                                "</td>";
                            break;
                        //case (int)ESP.HumanResource.Common.DimissionFormStatus.WaitHR2:  // 待集团人力资源审批
                        //    litHR.Text += "<tr><td width='50%' height='40' align='right'><img style='cursor:pointer' width='24' height='24' hspace='5' src='" +
                        //        GetImage(audit.AuditStatus) + "'/></td><td width='50%' align='left'>" +
                        //        audit.AuditorName + //this.GetDelegateUser(model.AuditorUserID.Value) + 
                        //        "</td>";
                        //    break;
                        case (int)ESP.HumanResource.Common.DimissionFormStatus.AuditComplete:  // 审批通过
                            break;
                        case (int)ESP.HumanResource.Common.DimissionFormStatus.Overrule:  // 审批驳回
                            break;
                    }
                }

            }
            //财务商务卡/工资

        }
    }

    /// <summary>
    /// 获得业务审批信息
    /// </summary>
    /// <param name="dimissionForm">离职单信息</param>
    /// <param name="list">待审批日志信息</param>
    /// <returns>返回</returns>
    private string GetBizScript(DimissionFormInfo dimissionForm, List<ESP.HumanResource.Entity.HRAuditLogInfo> list)
    {
        string script = string.Empty;
        if (dimissionForm.Status == (int)ESP.HumanResource.Common.DimissionFormStatus.WaitDirector)
        {
            if (list != null && list.Count > 0)
            {
                foreach (ESP.HumanResource.Entity.HRAuditLogInfo hrAudit in list)
                {
                    script += "<tr><td width='50%' height='40' align='right'><img style='cursor:pointer' width='24' height='24' hspace='5' src='" +
                        GetImage(hrAudit.AuditStatus) + "'/></td><td width='50%' align='left'>" +
                        hrAudit.AuditorName + //this.GetDelegateUser(model.AuditorUserID.Value) + 
                        "</td>";
                        //onclick='ShowMsg(\"" + getUserInfo(model.AuditorUserID.Value, suggestions) + "\");'
                }
            }
        }
        else if (dimissionForm.Status == (int)ESP.HumanResource.Common.DimissionFormStatus.WaitReceiver)
        {
            script += "<tr><td width='50%' height='40' align='right'><img style='cursor:pointer' width='24' height='24' hspace='5' src='" +
                GetImage((int)ESP.HumanResource.Common.AuditStatus.Audited) + "'/></td><td width='50%' align='left'>" +
                dimissionForm.DirectorName + //this.GetDelegateUser(model.AuditorUserID.Value) + 
                "</td>";
            List<ESP.HumanResource.Entity.DimissionDetailsInfo> detailList =
                ESP.HumanResource.BusinessLogic.DimissionDetailsManager.GetDimissionData(dimissionForm.DimissionId);
        }
        else
        {
            script += "<tr><td width='50%' height='40' align='right'><img style='cursor:pointer' width='24' height='24' hspace='5' src='" +
                GetImage((int)ESP.HumanResource.Common.AuditStatus.Audited) + "'/></td><td width='50%' align='left'>" +
                dimissionForm.DirectorName + //this.GetDelegateUser(model.AuditorUserID.Value) + 
                "</td>";
        }
        return script;
    }

    /// <summary>
    /// 获得审批状态图片信息
    /// </summary>
    /// <param name="AuditStatus">审批状态</param>
    /// <returns>返回审批状态图片路径</returns>
    private string GetImage(int AuditStatus)
    {
        string ret = string.Empty;
        switch (AuditStatus)
        {
            case (int)ESP.HumanResource.Common.AuditStatus.NotAudit:
                ret = "../../Images/WF_Waiting.gif' alt='待审批";
                break;
            case (int)ESP.HumanResource.Common.AuditStatus.Overrule:
                ret = "../../Images/WF_Reject.gif' alt='审批驳回,请重新编辑提交";
                break;
            case (int)ESP.HumanResource.Common.AuditStatus.Audited:
                ret = "../../Images/WF_Pass.gif' alt='审批通过";
                break;
        }
        return ret;
    }
}
