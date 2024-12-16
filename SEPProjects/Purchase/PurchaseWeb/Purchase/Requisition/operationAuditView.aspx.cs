using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ESP.Purchase.BusinessLogic;
using ESP.Purchase.Common;
using ESP.Purchase.Entity;
using WorkFlowDAO;
using WorkFlow.Model;
using WorkFlowLibary;
using System.Configuration;

public partial class Purchase_Requisition_operationAuditView : ESP.Web.UI.PageBase
{

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            GeneralInfo generalInfo = GeneralInfoManager.GetModel(int.Parse(Request[RequestName.GeneralID]));
            setOperationList(generalInfo);
            setPurchaseList(generalInfo);
            setRCList(generalInfo);
        }
    }

    #region 业务审核
    /// <summary>
    /// 业务审核人
    /// </summary>
    private void setOperationList(GeneralInfo generalInfo)
    {
        string script = "";
        WorkFlowDAO.ProcessInstanceDao idao = new ProcessInstanceDao();
        IList<WorkFlow.Model.WORKITEMS> newItems = idao.GetProcessRepresent(generalInfo.InstanceID, generalInfo.ProcessID);

        foreach (WorkFlow.Model.WORKITEMS item in newItems)
        {
            script += "<tr><td width='50%' height='40' align='right'><img style='cursor:hand' width='24' height='24' hspace='5' src='" + getImg(item.STATE) + "' onclick='ShowMsg(\"" + getUserInfo(int.Parse(item.RoleID), getRemark(int.Parse(item.RoleID))) + "\");'/></td><td width='50%' align='left'>" + ESP.Framework.BusinessLogic.EmployeeManager.Get(int.Parse(item.RoleID)).FullNameCN + "</td>";
        }
        script += "</tr> <tr><td align='right' bgcolor='#FFFFFF'></td> <td align='left' bgcolor='#FFFFFF'></td></tr>";
        litBiz.Text = script;
    }

    /// <summary>
    /// 获得业务审批备注
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    private string getRemark(int userId)
    {
        OperationAuditLogInfo log = OperationAuditLogManager.GetModel(int.Parse(Request[RequestName.GeneralID]), userId);
        return log == null ? "" : log.auditRemark;
    }

    /// <summary>
    /// 根据业务审核状态，获得显示图片
    /// </summary>
    /// <param name="auditType"></param>
    /// <returns></returns>
    private string getImg(int auditType)
    {
        if (auditType == WorkFlowLibary.WfStateContants.TASKSTATE_COMPLETED)
            return "/images/WF_Pass.gif' alt='审批通过";
        else if (auditType == WorkFlowLibary.WfStateContants.PROCESS_TERMINATED)
            return "/images/WF_Reject.gif' alt='审批驳回";
        else
            return "/images/WF_Waiting.gif' alt='待审批";
    }
    #endregion

    #region 分公司、采购物料、采购总监审核
    private void setPurchaseList(GeneralInfo generalInfo)
    {
        string script = "";

        if (generalInfo.status != State.order_ADAuditWait && generalInfo.PRType != (int)PRTYpe.MPPR && generalInfo.PRType != (int)PRTYpe.ADPR && generalInfo.PRType != (int)PRTYpe.PR_MediaFA && generalInfo.PRType != (int)PRTYpe.PR_PriFA)
        {
            //采购物料
            if (generalInfo.first_assessor > 0)
            {
                if (generalInfo.status >= State.order_commit && generalInfo.status != State.order_return && generalInfo.status != State.requisition_temporary_commit && generalInfo.status != State.requisition_operationAduit && generalInfo.status != State.requisition_RiskControl)
                    script += "<tr><td width='50%' height='40' align='right'><img style='cursor:hand' width='24' height='24' hspace='5' src='/images/WF_Pass.gif' alt='审核通过' onclick='ShowMsg(\"" + getUserInfo(generalInfo.first_assessor, generalInfo.requisition_overrule) + "\");'/></td><td width='50%' align='left'>" + generalInfo.first_assessorname + "</td>";
                else
                    script += "<tr><td width='50%' height='40' align='right'><img style='cursor:hand' width='24' height='24' hspace='5' src='/images/WF_Waiting.gif' alt='待审核' onclick='ShowMsg(\"" + getUserInfo(generalInfo.first_assessor, "") + "\");'/></td><td width='50%' align='left'>" + generalInfo.first_assessorname + "</td>";
            }
            //采购总监
            if (generalInfo.status >= State.order_ok && generalInfo.status != State.requisition_temporary_commit && generalInfo.status != State.requisition_operationAduit && generalInfo.status != State.requisition_RiskControl)
            {
                script += "<tr><td width='50%' height='40' align='right'><img style='cursor:hand' width='24' height='24' hspace='5' src='/images/WF_Pass.gif' alt='审批通过' onclick='ShowMsg(\"" + getUserInfo(generalInfo.purchaseAuditor, generalInfo.requisition_overrule) + "\");'/></td><td width='50%' align='left'>" + generalInfo.purchaseAuditorName + "</td>";
            }

            else
            {
                if (generalInfo.purchaseAuditor != 0)
                    script += "<tr><td width='50%' height='40' align='right'><img style='cursor:hand' width='24' height='24' hspace='5' src='/images/WF_Waiting.gif' alt='待审核' onclick='ShowMsg(\"" + getUserInfo(generalInfo.purchaseAuditor, "") + "\");'/></td><td width='50%' align='left'>" + generalInfo.purchaseAuditorName + "</td>";
            }

        }

        script += "</tr> <tr><td align='right' bgcolor='#FFFFFF'></td> <td align='left' bgcolor='#FFFFFF'></td></tr>";
        litContract.Text = script;
    }
    #endregion


    #region 风控审核
    private void setRCList(GeneralInfo generalInfo)
    {
        string script = "";

        if (generalInfo.RCAuditor > 0)
        {
            if (generalInfo.status == State.requisition_operationAduit || generalInfo.status == State.requisition_RiskControl)
            {
                script += "<tr><td width='50%' height='40' align='right'><img style='cursor:hand' width='24' height='24' hspace='5' src='/images/WF_Waiting.gif' alt='待审核' onclick='ShowMsg(\"" + getUserInfo(generalInfo.RCAuditor.Value, "") + "\");'/></td><td width='50%' align='left'>" + generalInfo.RCAuditorName + "</td>";
            }
            else
                script += "<tr><td width='50%' height='40' align='right'><img style='cursor:hand' width='24' height='24' hspace='5' src='/images/WF_Pass.gif' alt='审核通过' onclick='ShowMsg(\"" + getUserInfo(generalInfo.RCAuditor.Value, generalInfo.requisition_overrule) + "\");'/></td><td width='50%' align='left'>" + generalInfo.RCAuditorName + "</td>";

        }


        script += "</tr> <tr><td align='right' bgcolor='#FFFFFF'></td> <td align='left' bgcolor='#FFFFFF'></td></tr>";

        litRisk.Text = script;
    }
    #endregion

    private string getUserInfo(int userid, string auditdesc)
    {
        return ESP.Web.UI.PageBase.GetUserInfo(userid, auditdesc);
    }
}
