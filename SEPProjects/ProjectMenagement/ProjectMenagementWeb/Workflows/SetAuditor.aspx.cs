using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using ESP.Finance.Utility;
using ESP.Finance.Entity;
using ESP.Finance.BusinessLogic;
using ESP.Framework.BusinessLogic;

namespace FinanceWeb.Workflows
{
    public partial class SetAuditor : ESP.Web.UI.PageBase
    {

        int modelId = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Request[ESP.Finance.Utility.RequestName.ModelID]) && !string.IsNullOrEmpty(Request[ESP.Finance.Utility.RequestName.ModelType]))
            {
                int deptId = 0;
                int userId = 0;
                string projectCode = string.Empty;
                decimal totalPrice = 0;
                modelId = int.Parse(Request[ESP.Finance.Utility.RequestName.ModelID]);

                if (Request[ESP.Finance.Utility.RequestName.ModelType] == "Refund")
                {
                    RefundInfo refundModel = ESP.Finance.BusinessLogic.RefundManager.GetModel(modelId);
                    deptId = refundModel.DeptId;
                    userId = refundModel.RequestorID;
                    projectCode = refundModel.ProjectCode;
                    totalPrice = refundModel.Amounts;
                }
                if (!IsPostBack)
                {
                    ViewContorl(deptId, userId, projectCode, totalPrice);
                }
            }
        }



        private void ViewContorl(int deptId, int userId, string projectCode,decimal totalPrice)
        {
            string removeTypes = "";
            int rowcount = 0;

          
            ProjectInfo projectModel = ESP.Finance.BusinessLogic.ProjectManager.GetModelByProjectCode(projectCode);

            ESP.Framework.Entity.OperationAuditManageInfo manageModel = null;

            manageModel = ESP.Framework.BusinessLogic.OperationAuditManageManager.GetModelByUserId(userId);
            if (manageModel == null)
                manageModel = ESP.Framework.BusinessLogic.OperationAuditManageManager.GetModelByDepId(deptId);

            ESP.HumanResource.Entity.EmployeesInPositionsInfo positionModel = ESP.HumanResource.BusinessLogic.EmployeesInPositionsManager.GetModel(userId);

            ESP.Framework.Entity.OperationAuditManageInfo selfManageModel = null;

            if (positionModel.DepartmentID != deptId && manageModel.UserId == 0)
            {
                selfManageModel = ESP.Framework.BusinessLogic.OperationAuditManageManager.GetModelByDepId(positionModel.DepartmentID);
            }

            removeTypes += ESP.Finance.Utility.auditorType.operationAudit_Type_YS + ",";
            removeTypes += ESP.Finance.Utility.auditorType.operationAudit_Type_FA + ",";

            removeTypes += ESP.Finance.Utility.auditorType.purchase_first + ",";
            removeTypes += ESP.Finance.Utility.auditorType.purchase_major2 + ",";
            removeTypes += ESP.Finance.Utility.auditorType.operationAudit_Type_Financial + ",";
            removeTypes += ESP.Finance.Utility.auditorType.operationAudit_Type_Financial2 + ",";
            removeTypes += ESP.Finance.Utility.auditorType.operationAudit_Type_Financial3 + ",";

            #region 使用GM项目号
            if ((projectCode.IndexOf("GM*") >= 0 || projectCode.IndexOf("*GM") >= 0 || projectCode.IndexOf(ESP.Finance.Configuration.ConfigurationManager.ShunYaShortEN) >= 0))
            {
                if (manageModel != null)
                {
                    if (CurrentUserID != manageModel.DirectorId && CurrentUserID != manageModel.ManagerId)
                    {
                        //默认总监
                        System.Web.UI.HtmlControls.HtmlTableRow ZJRow = new System.Web.UI.HtmlControls.HtmlTableRow();
                        System.Web.UI.HtmlControls.HtmlTableCell ZJCell1 = new System.Web.UI.HtmlControls.HtmlTableCell();
                        System.Web.UI.HtmlControls.HtmlTableCell ZJCell2 = new System.Web.UI.HtmlControls.HtmlTableCell();
                        System.Web.UI.HtmlControls.HtmlTableCell ZJCell3 = new System.Web.UI.HtmlControls.HtmlTableCell();
                        System.Web.UI.HtmlControls.HtmlTableCell ZJCell4 = new System.Web.UI.HtmlControls.HtmlTableCell();
                        System.Web.UI.HtmlControls.HtmlTableCell ZJCell5 = new System.Web.UI.HtmlControls.HtmlTableCell();
                        System.Web.UI.HtmlControls.HtmlTableCell ZJCell6 = new System.Web.UI.HtmlControls.HtmlTableCell();
                        ZJCell1.Align = "Center";
                        ZJCell2.Align = "Center";
                        ZJCell3.Align = "Center";
                        ZJCell4.Align = "Center";
                        ZJCell5.Align = "Center";
                        ZJCell6.Align = "Center";

                        ZJCell1.InnerHtml = rowcount.ToString();
                        ZJCell2.InnerHtml = manageModel.DirectorName;
                        ZJCell3.InnerHtml = new ESP.Compatible.Employee(manageModel.DirectorId).PositionDescription;
                        ZJCell4.InnerHtml = manageModel.DirectorAmount.ToString("#,##0.00");
                        string trId = ZJRow.Attributes["id"] = "tr_" + manageModel.DirectorId + "_ZJSP";
                        ZJCell5.InnerHtml = ESP.Finance.Utility.auditorType.operationAudit_Type_Names[ESP.Finance.Utility.auditorType.operationAudit_Type_ZJSP];
                        ZJCell6.InnerHtml = "<a href=\"\" onclick=\"openEmployee('ZJSP');return false;\">添加</a>&nbsp;<a href=\"\" onclick=\"openEmployee1('ZJSP','" + trId + "','" + manageModel.DirectorId + "');return false;\">更改</a>";//&nbsp;<a href=\"\" onclick=\"removeRow('ZJSP','" + manageModel.DirectorId + "','" + trId + "');return false;\">删除</a>";

                        hidZJSP.Value = manageModel.DirectorId.ToString().Trim();
                        ZJRow.Cells.Add(ZJCell1);
                        ZJRow.Cells.Add(ZJCell2);
                        ZJRow.Cells.Add(ZJCell3);
                        ZJRow.Cells.Add(ZJCell4);
                        ZJRow.Cells.Add(ZJCell5);
                        ZJRow.Cells.Add(ZJCell6);
                        ZJRow.Attributes["class"] = "td";
                        tab.Rows.Add(ZJRow);
                        rowcount++;
                    }

                    if (CurrentUserID == manageModel.DirectorId || (CurrentUserID != manageModel.ManagerId && totalPrice > manageModel.DirectorAmount))
                    {
                        //默认manager
                        System.Web.UI.HtmlControls.HtmlTableRow ZJLRow = new System.Web.UI.HtmlControls.HtmlTableRow();
                        System.Web.UI.HtmlControls.HtmlTableCell ZJLCell1 = new System.Web.UI.HtmlControls.HtmlTableCell();
                        System.Web.UI.HtmlControls.HtmlTableCell ZJLCell2 = new System.Web.UI.HtmlControls.HtmlTableCell();
                        System.Web.UI.HtmlControls.HtmlTableCell ZJLCell3 = new System.Web.UI.HtmlControls.HtmlTableCell();
                        System.Web.UI.HtmlControls.HtmlTableCell ZJLCell4 = new System.Web.UI.HtmlControls.HtmlTableCell();
                        System.Web.UI.HtmlControls.HtmlTableCell ZJLCell5 = new System.Web.UI.HtmlControls.HtmlTableCell();
                        System.Web.UI.HtmlControls.HtmlTableCell ZJLCell6 = new System.Web.UI.HtmlControls.HtmlTableCell();
                        ZJLCell1.Align = "Center";
                        ZJLCell2.Align = "Center";
                        ZJLCell3.Align = "Center";
                        ZJLCell4.Align = "Center";
                        ZJLCell5.Align = "Center";
                        ZJLCell6.Align = "Center";

                        ZJLCell1.InnerHtml = rowcount.ToString();
                        ZJLCell2.InnerHtml = manageModel.ManagerName;
                        ZJLCell3.InnerHtml = new ESP.Compatible.Employee(manageModel.ManagerId).PositionDescription;
                        ZJLCell4.InnerHtml = manageModel.ManagerAmount.ToString("#,##0.00");
                        ZJLCell5.InnerHtml = ESP.Finance.Utility.auditorType.operationAudit_Type_Names[ESP.Finance.Utility.auditorType.operationAudit_Type_ZJLSP];
                        ZJLCell6.InnerHtml = "<a href=\"\" onclick=\"openEmployee('ZJLSP');return false;\">添加</a>";

                        hidZJLSP.Value = manageModel.ManagerId.ToString();
                        ZJLRow.Cells.Add(ZJLCell1);
                        ZJLRow.Cells.Add(ZJLCell2);
                        ZJLRow.Cells.Add(ZJLCell3);
                        ZJLRow.Cells.Add(ZJLCell4);
                        ZJLRow.Cells.Add(ZJLCell5);
                        ZJLRow.Cells.Add(ZJLCell6);
                        ZJLRow.Attributes["class"] = "td";
                        tab.Rows.Add(ZJLRow);
                        rowcount++;
                    }

                    if (CurrentUserID == manageModel.ManagerId || totalPrice > manageModel.ManagerAmount)
                    {
                        //ceo审批
                        System.Web.UI.HtmlControls.HtmlTableRow CEORow = new System.Web.UI.HtmlControls.HtmlTableRow();
                        System.Web.UI.HtmlControls.HtmlTableCell CEOCell1 = new System.Web.UI.HtmlControls.HtmlTableCell();
                        System.Web.UI.HtmlControls.HtmlTableCell CEOCell2 = new System.Web.UI.HtmlControls.HtmlTableCell();
                        System.Web.UI.HtmlControls.HtmlTableCell CEOCell3 = new System.Web.UI.HtmlControls.HtmlTableCell();
                        System.Web.UI.HtmlControls.HtmlTableCell CEOCell4 = new System.Web.UI.HtmlControls.HtmlTableCell();
                        System.Web.UI.HtmlControls.HtmlTableCell CEOCell5 = new System.Web.UI.HtmlControls.HtmlTableCell();
                        System.Web.UI.HtmlControls.HtmlTableCell CEOCell6 = new System.Web.UI.HtmlControls.HtmlTableCell();
                        CEOCell1.Align = "Center";
                        CEOCell2.Align = "Center";
                        CEOCell3.Align = "Center";
                        CEOCell4.Align = "Center";
                        CEOCell5.Align = "Center";
                        CEOCell6.Align = "Center";

                        CEOCell1.InnerHtml = rowcount.ToString();
                        CEOCell2.InnerHtml = manageModel.CEOName;
                        CEOCell3.InnerHtml = new ESP.Compatible.Employee(manageModel.CEOId).PositionDescription;
                        CEOCell4.InnerHtml = manageModel.CEOAmount.ToString("#,##0.00");
                        CEOCell5.InnerHtml = ESP.Finance.Utility.auditorType.operationAudit_Type_Names[ESP.Finance.Utility.auditorType.operationAudit_Type_CEO];
                        CEOCell6.InnerHtml = "&nbsp;";

                        CEORow.Attributes["id"] = "tr_" + manageModel.CEOId.ToString() + "_CEO";

                        hidCEO.Value = manageModel.CEOId.ToString();
                        CEORow.Cells.Add(CEOCell1);
                        CEORow.Cells.Add(CEOCell2);
                        CEORow.Cells.Add(CEOCell3);
                        CEORow.Cells.Add(CEOCell4);
                        CEORow.Cells.Add(CEOCell5);
                        CEORow.Cells.Add(CEOCell6);
                        CEORow.Attributes["class"] = "td";
                        tab.Rows.Add(CEORow);

                        rowcount++;
                    }

                }
            }
            #endregion

            else
            {


                rowcount = 1;
                if (selfManageModel != null && userId != selfManageModel.DirectorId && userId != selfManageModel.ManagerId)
                {
                    System.Web.UI.HtmlControls.HtmlTableRow YSRow = new System.Web.UI.HtmlControls.HtmlTableRow();
                    System.Web.UI.HtmlControls.HtmlTableCell YSCell1 = new System.Web.UI.HtmlControls.HtmlTableCell();
                    System.Web.UI.HtmlControls.HtmlTableCell YSCell2 = new System.Web.UI.HtmlControls.HtmlTableCell();
                    System.Web.UI.HtmlControls.HtmlTableCell YSCell3 = new System.Web.UI.HtmlControls.HtmlTableCell();
                    System.Web.UI.HtmlControls.HtmlTableCell YSCell4 = new System.Web.UI.HtmlControls.HtmlTableCell();
                    System.Web.UI.HtmlControls.HtmlTableCell YSCell5 = new System.Web.UI.HtmlControls.HtmlTableCell();
                    System.Web.UI.HtmlControls.HtmlTableCell YSCell6 = new System.Web.UI.HtmlControls.HtmlTableCell();
                    YSCell1.Align = "Center";
                    YSCell2.Align = "Center";
                    YSCell3.Align = "Center";
                    YSCell4.Align = "Center";
                    YSCell5.Align = "Center";
                    YSCell6.Align = "Center";

                    YSCell1.InnerHtml = rowcount.ToString();
                    YSCell2.InnerHtml = selfManageModel.DirectorName;
                    YSCell3.InnerHtml = new ESP.Compatible.Employee(selfManageModel.DirectorId).PositionDescription;
                    YSCell4.InnerHtml = selfManageModel.DirectorAmount.ToString("#,##0.00");
                    string trId = YSRow.Attributes["id"] = "tr_" + selfManageModel.DirectorId + "_YSSP";
                    YSCell5.InnerHtml = ESP.Finance.Utility.auditorType.operationAudit_Type_Names[ESP.Finance.Utility.auditorType.operationAudit_Type_ZJSP];
                    YSCell6.InnerHtml = "";

                    hidYS.Value += selfManageModel.DirectorId.ToString() + ",";
                    YSRow.Cells.Add(YSCell1);
                    YSRow.Cells.Add(YSCell2);
                    YSRow.Cells.Add(YSCell3);
                    YSRow.Cells.Add(YSCell4);
                    YSRow.Cells.Add(YSCell5);
                    YSRow.Cells.Add(YSCell6);
                    YSRow.Attributes["class"] = "td";
                    tab.Rows.Add(YSRow);
                    rowcount++;
                }
                #region 默认项目负责人审核
                if (projectModel != null && userId != (projectModel.ApplicantUserID))
                {

                    if (CurrentUserID != manageModel.DirectorId && CurrentUserID != manageModel.ManagerId)
                    {
                        System.Web.UI.HtmlControls.HtmlTableRow YSRow = new System.Web.UI.HtmlControls.HtmlTableRow();
                        System.Web.UI.HtmlControls.HtmlTableCell YSCell1 = new System.Web.UI.HtmlControls.HtmlTableCell();
                        System.Web.UI.HtmlControls.HtmlTableCell YSCell2 = new System.Web.UI.HtmlControls.HtmlTableCell();
                        System.Web.UI.HtmlControls.HtmlTableCell YSCell3 = new System.Web.UI.HtmlControls.HtmlTableCell();
                        System.Web.UI.HtmlControls.HtmlTableCell YSCell4 = new System.Web.UI.HtmlControls.HtmlTableCell();
                        System.Web.UI.HtmlControls.HtmlTableCell YSCell5 = new System.Web.UI.HtmlControls.HtmlTableCell();
                        System.Web.UI.HtmlControls.HtmlTableCell YSCell6 = new System.Web.UI.HtmlControls.HtmlTableCell();
                        YSCell1.Align = "Center";
                        YSCell2.Align = "Center";
                        YSCell3.Align = "Center";
                        YSCell4.Align = "Center";
                        YSCell5.Align = "Center";
                        YSCell6.Align = "Center";

                        YSCell1.InnerHtml = rowcount.ToString();
                        YSCell2.InnerHtml = projectModel.ApplicantEmployeeName;
                        YSCell3.InnerHtml = "项目负责人";
                        YSCell4.InnerHtml = "项目负责人";
                        string trId = YSRow.Attributes["id"] = "tr_" + (projectModel.ApplicantUserID.ToString()) + "_YSSP";
                        YSCell5.InnerHtml = "项目负责人审批";
                        YSCell6.InnerHtml = "<a href=\"\" onclick=\"openEmployee('YS');return false;\">添加</a>";

                        hidYS.Value += (projectModel.ApplicantUserID.ToString()) + ",";
                        YSRow.Cells.Add(YSCell1);
                        YSRow.Cells.Add(YSCell2);
                        YSRow.Cells.Add(YSCell3);
                        YSRow.Cells.Add(YSCell4);
                        YSRow.Cells.Add(YSCell5);
                        YSRow.Cells.Add(YSCell6);
                        YSRow.Attributes["class"] = "td";
                        tab.Rows.Add(YSRow);
                        rowcount++;
                    }
                }

                #endregion

                #region 总监级审批
                if (manageModel != null && (CurrentUserID != manageModel.DirectorId && CurrentUserID != manageModel.ManagerId))
                {

                    System.Web.UI.HtmlControls.HtmlTableRow ZJRow = new System.Web.UI.HtmlControls.HtmlTableRow();
                    System.Web.UI.HtmlControls.HtmlTableCell ZJCell1 = new System.Web.UI.HtmlControls.HtmlTableCell();
                    System.Web.UI.HtmlControls.HtmlTableCell ZJCell2 = new System.Web.UI.HtmlControls.HtmlTableCell();
                    System.Web.UI.HtmlControls.HtmlTableCell ZJCell3 = new System.Web.UI.HtmlControls.HtmlTableCell();
                    System.Web.UI.HtmlControls.HtmlTableCell ZJCell4 = new System.Web.UI.HtmlControls.HtmlTableCell();
                    System.Web.UI.HtmlControls.HtmlTableCell ZJCell5 = new System.Web.UI.HtmlControls.HtmlTableCell();
                    System.Web.UI.HtmlControls.HtmlTableCell ZJCell6 = new System.Web.UI.HtmlControls.HtmlTableCell();
                    ZJCell1.Align = "Center";
                    ZJCell2.Align = "Center";
                    ZJCell3.Align = "Center";
                    ZJCell4.Align = "Center";
                    ZJCell5.Align = "Center";
                    ZJCell6.Align = "Center";



                    ZJCell1.InnerHtml = rowcount.ToString();
                    ZJCell2.InnerHtml = manageModel.DirectorName;
                    ZJCell3.InnerHtml = new ESP.Compatible.Employee(manageModel.DirectorId).PositionDescription;
                    ZJCell4.InnerHtml = manageModel.DirectorAmount.ToString("#,##0.00");
                    string trId = ZJRow.Attributes["id"] = "tr_" + manageModel.DirectorId + "_ZJSP";
                    ZJCell5.InnerHtml = ESP.Finance.Utility.auditorType.operationAudit_Type_Names[ESP.Finance.Utility.auditorType.operationAudit_Type_ZJSP];
                    ZJCell6.InnerHtml = "<a href=\"\" onclick=\"openEmployee('ZJSP');return false;\">添加</a>&nbsp;<a href=\"\" onclick=\"openEmployee1('ZJSP','" + trId + "','" + manageModel.DirectorId + "');return false;\">更改</a>";//&nbsp;<a href=\"\" onclick=\"removeRow('ZJSP','" + manageModel.DirectorId + "','" + trId + "');return false;\">删除</a>";

                    hidZJSP.Value = manageModel.DirectorId.ToString().Trim();
                    ZJRow.Cells.Add(ZJCell1);
                    ZJRow.Cells.Add(ZJCell2);
                    ZJRow.Cells.Add(ZJCell3);
                    ZJRow.Cells.Add(ZJCell4);
                    ZJRow.Cells.Add(ZJCell5);
                    ZJRow.Cells.Add(ZJCell6);
                    ZJRow.Attributes["class"] = "td";
                    tab.Rows.Add(ZJRow);
                    rowcount++;
                    //  }
                }

                #endregion

                #region 总经理级审批
                if (CurrentUserID == manageModel.DirectorId || (CurrentUserID != manageModel.ManagerId && totalPrice > manageModel.DirectorAmount))
                {
                    //默认总经理
                    System.Web.UI.HtmlControls.HtmlTableRow ZJLRow = new System.Web.UI.HtmlControls.HtmlTableRow();
                    System.Web.UI.HtmlControls.HtmlTableCell ZJLCell1 = new System.Web.UI.HtmlControls.HtmlTableCell();
                    System.Web.UI.HtmlControls.HtmlTableCell ZJLCell2 = new System.Web.UI.HtmlControls.HtmlTableCell();
                    System.Web.UI.HtmlControls.HtmlTableCell ZJLCell3 = new System.Web.UI.HtmlControls.HtmlTableCell();
                    System.Web.UI.HtmlControls.HtmlTableCell ZJLCell4 = new System.Web.UI.HtmlControls.HtmlTableCell();
                    System.Web.UI.HtmlControls.HtmlTableCell ZJLCell5 = new System.Web.UI.HtmlControls.HtmlTableCell();
                    System.Web.UI.HtmlControls.HtmlTableCell ZJLCell6 = new System.Web.UI.HtmlControls.HtmlTableCell();
                    ZJLCell1.Align = "Center";
                    ZJLCell2.Align = "Center";
                    ZJLCell3.Align = "Center";
                    ZJLCell4.Align = "Center";
                    ZJLCell5.Align = "Center";
                    ZJLCell6.Align = "Center";



                    ZJLCell1.InnerHtml = rowcount.ToString();
                    ZJLCell2.InnerHtml = manageModel.ManagerName;
                    ZJLCell3.InnerHtml = new ESP.Compatible.Employee(manageModel.ManagerId).PositionDescription;
                    ZJLCell4.InnerHtml = manageModel.ManagerAmount.ToString("#,##0.00");
                    string trId1 = ZJLRow.Attributes["id"] = "tr_" + manageModel.ManagerId + "_ZJLSP";
                    ZJLCell5.InnerHtml = ESP.Finance.Utility.auditorType.operationAudit_Type_Names[ESP.Finance.Utility.auditorType.operationAudit_Type_ZJLSP];
                    ZJLCell6.InnerHtml = "<a href=\"\" onclick=\"openEmployee('ZJLSP');return false;\">添加</a>&nbsp;<a href=\"\" onclick=\"openEmployee1('ZJLSP','" + trId1 + "','" + manageModel.ManagerId + "');return false;\">更改</a>";//&nbsp;<a href=\"\" onclick=\"removeRow('ZJLSP','" + manageModel.ManagerId + "','" + trId1 + "');return false;\">删除</a>";

                    hidZJLSP.Value = manageModel.ManagerId.ToString();
                    ZJLRow.Cells.Add(ZJLCell1);
                    ZJLRow.Cells.Add(ZJLCell2);
                    ZJLRow.Cells.Add(ZJLCell3);
                    ZJLRow.Cells.Add(ZJLCell4);
                    ZJLRow.Cells.Add(ZJLCell5);
                    ZJLRow.Cells.Add(ZJLCell6);
                    ZJLRow.Attributes["class"] = "td";
                    tab.Rows.Add(ZJLRow);
                    rowcount++;


                }
                #endregion

                #region ceo级审批
                if (CurrentUserID == manageModel.ManagerId || totalPrice > manageModel.ManagerAmount)
                {
                    System.Web.UI.HtmlControls.HtmlTableRow CEORow = new System.Web.UI.HtmlControls.HtmlTableRow();
                    System.Web.UI.HtmlControls.HtmlTableCell CEOCell1 = new System.Web.UI.HtmlControls.HtmlTableCell();
                    System.Web.UI.HtmlControls.HtmlTableCell CEOCell2 = new System.Web.UI.HtmlControls.HtmlTableCell();
                    System.Web.UI.HtmlControls.HtmlTableCell CEOCell3 = new System.Web.UI.HtmlControls.HtmlTableCell();
                    System.Web.UI.HtmlControls.HtmlTableCell CEOCell4 = new System.Web.UI.HtmlControls.HtmlTableCell();
                    System.Web.UI.HtmlControls.HtmlTableCell CEOCell5 = new System.Web.UI.HtmlControls.HtmlTableCell();
                    System.Web.UI.HtmlControls.HtmlTableCell CEOCell6 = new System.Web.UI.HtmlControls.HtmlTableCell();
                    CEOCell1.Align = "Center";
                    CEOCell2.Align = "Center";
                    CEOCell3.Align = "Center";
                    CEOCell4.Align = "Center";
                    CEOCell5.Align = "Center";
                    CEOCell6.Align = "Center";

                    CEOCell1.InnerHtml = rowcount.ToString();
                    CEOCell2.InnerHtml = manageModel.CEOName;
                    CEOCell3.InnerHtml = new ESP.Compatible.Employee(manageModel.CEOId).PositionDescription;
                    CEOCell4.InnerHtml = manageModel.CEOAmount.ToString("#,##0.00");
                    CEOCell5.InnerHtml = ESP.Finance.Utility.auditorType.operationAudit_Type_Names[ESP.Finance.Utility.auditorType.operationAudit_Type_CEO];
                    CEOCell6.InnerHtml = "&nbsp;";

                    CEORow.Attributes["id"] = "tr_" + manageModel.CEOId + "_CEO";

                    hidCEO.Value = manageModel.CEOId.ToString();
                    CEORow.Cells.Add(CEOCell1);
                    CEORow.Cells.Add(CEOCell2);
                    CEORow.Cells.Add(CEOCell3);
                    CEORow.Cells.Add(CEOCell4);
                    CEORow.Cells.Add(CEOCell5);
                    CEORow.Cells.Add(CEOCell6);
                    CEORow.Attributes["class"] = "td";
                    tab.Rows.Add(CEORow);
                    rowcount++;

                }
                #endregion


            }
        }

        protected void btnCommit_Click(object sender, EventArgs e)
        {
            int firstAuditorId = 0;
             string exMail = string.Empty;
            if (!string.IsNullOrEmpty(Request[ESP.Finance.Utility.RequestName.ModelID]) && !string.IsNullOrEmpty(Request[ESP.Finance.Utility.RequestName.ModelType]))
            {
                modelId = int.Parse(Request[ESP.Finance.Utility.RequestName.ModelID]);

                if (Request[ESP.Finance.Utility.RequestName.ModelType] == "Refund")
                {
                    RefundInfo refundModel = ESP.Finance.BusinessLogic.RefundManager.GetModel(modelId);
                   
                    List<ESP.Finance.Entity.WorkFlowInfo> operationList = createTemplateProcess((int)ESP.Finance.Utility.FormType.Refund, refundModel.ProjectCode);
                    firstAuditorId = operationList[0].AuditorUserID;
                    refundModel.Status = (int)PaymentStatus.Submit;
                    refundModel.PaymentUserID = firstAuditorId;
                    ESP.Finance.BusinessLogic.RefundManager.CommitWorkflow(refundModel, operationList);
                    

                    try
                    {
                        //给第一级审核人发送邮件
                        ESP.Framework.Entity.EmployeeInfo emp = ESP.Framework.BusinessLogic.EmployeeManager.Get(firstAuditorId);
                        if (emp != null)
                        {
                            SendMailHelper.SendMailRefundCommit(refundModel, CurrentUser.Name, emp.FullNameCN, CurrentUser.EMail, emp.Email);
                        }
                    }
                    catch
                    {
                        exMail = "(邮件发送失败!)";
                    }
                    Page.ClientScript.RegisterStartupScript(typeof(string), "", "window.location.href='/Edit/RefundTabEdit.aspx';alert('已成功设置付款业务审批人并提交成功！" + exMail + "');", true);
                }
            }
        }


        private List<ESP.Finance.Entity.WorkFlowInfo> createTemplateProcess(int modelType, string projectCode)
        {
            string checkAuditor = ",";
            List<ESP.Finance.Entity.WorkFlowInfo> operationList = new List<ESP.Finance.Entity.WorkFlowInfo>();
            //预审人
            if (hidYS.Value != "")
            {
                string[] prejudicationIds = hidYS.Value.TrimEnd(',').Split(',');
                for (int i = 0; i < prejudicationIds.Length; i++)
                {
                    if (prejudicationIds[i].Trim() != "")
                    {
                        if (checkAuditor.IndexOf(prejudicationIds[i].Trim()) < 0)
                        {
                            checkAuditor += prejudicationIds[i].Trim() + ",";
                            ESP.Compatible.Employee emp = new ESP.Compatible.Employee(int.Parse(prejudicationIds[i]));

                            ESP.Finance.Entity.WorkFlowInfo auditModel = new ESP.Finance.Entity.WorkFlowInfo();
                            auditModel.ModelId = modelId;
                            auditModel.AuditType = ESP.Finance.Utility.auditorType.operationAudit_Type_YS;
                            auditModel.AuditorUserID = int.Parse(emp.SysID);
                            auditModel.AuditorUserCode = emp.ID;
                            auditModel.AuditorEmployeeName = emp.Name;
                            auditModel.AuditorUserName = emp.ITCode;
                            auditModel.AuditStatus = (int)ESP.Finance.Utility.AuditHistoryStatus.UnAuditing;
                            auditModel.ModelType = modelType;
                            operationList.Add(auditModel);

                        }
                    }
                }
            }


            //总监级审核人
            if (hidZJSP.Value != "")
            {
                if (checkAuditor.IndexOf(hidZJSP.Value.TrimEnd(',')) < 0)
                {
                    checkAuditor += hidZJSP.Value.TrimEnd(',') + ",";
                    ESP.Compatible.Employee emp = new ESP.Compatible.Employee(int.Parse(hidZJSP.Value.TrimEnd(',')));

                    ESP.Finance.Entity.WorkFlowInfo auditModel = new ESP.Finance.Entity.WorkFlowInfo();
                    auditModel.ModelId = modelId;
                    auditModel.AuditType = ESP.Finance.Utility.auditorType.operationAudit_Type_ZJSP;
                    auditModel.AuditorUserID = int.Parse(emp.SysID);
                    auditModel.AuditorUserCode = emp.ID;
                    auditModel.AuditorEmployeeName = emp.Name;
                    auditModel.AuditorUserName = emp.ITCode;
                    auditModel.AuditStatus = (int)ESP.Finance.Utility.AuditHistoryStatus.UnAuditing;
                    auditModel.ModelType = modelType;
                    operationList.Add(auditModel);

                }
            }

            //总监级附加审核人
            if (hidZJFJ.Value != "")
            {
                string[] fj1Ids = hidZJFJ.Value.TrimEnd(',').Split(',');
                for (int i = 0; i < fj1Ids.Length; i++)
                {
                    if (fj1Ids[i].Trim() != "")
                    {
                        if (checkAuditor.IndexOf(fj1Ids[i].Trim()) < 0)
                        {
                            checkAuditor += fj1Ids[i].Trim() + ",";
                            ESP.Compatible.Employee emp = new ESP.Compatible.Employee(int.Parse(fj1Ids[i]));

                            ESP.Finance.Entity.WorkFlowInfo auditModel = new ESP.Finance.Entity.WorkFlowInfo();
                            auditModel.ModelId = modelId;
                            auditModel.AuditType = ESP.Finance.Utility.auditorType.operationAudit_Type_ZJFJ;
                            auditModel.AuditorUserID = int.Parse(emp.SysID);
                            auditModel.AuditorUserCode = emp.ID;
                            auditModel.AuditorEmployeeName = emp.Name;
                            auditModel.AuditorUserName = emp.ITCode;
                            auditModel.AuditStatus = (int)ESP.Finance.Utility.AuditHistoryStatus.UnAuditing;
                            auditModel.ModelType = modelType;
                            operationList.Add(auditModel);

                        }
                    }
                }
            }


            //总经理级审核人
            if (hidZJLSP.Value != "")
            {
                if (checkAuditor.IndexOf(hidZJLSP.Value.TrimEnd(',')) < 0)
                {
                    checkAuditor += hidZJLSP.Value.TrimEnd(',') + ",";
                    ESP.Compatible.Employee emp = new ESP.Compatible.Employee(int.Parse(hidZJLSP.Value.TrimEnd(',')));
                    ESP.Finance.Entity.WorkFlowInfo auditModel = new ESP.Finance.Entity.WorkFlowInfo();
                    auditModel.ModelId = modelId;
                    auditModel.AuditType = ESP.Finance.Utility.auditorType.operationAudit_Type_ZJLSP;
                    auditModel.AuditorUserID = int.Parse(emp.SysID);
                    auditModel.AuditorUserCode = emp.ID;
                    auditModel.AuditorEmployeeName = emp.Name;
                    auditModel.AuditorUserName = emp.ITCode;
                    auditModel.AuditStatus = (int)ESP.Finance.Utility.AuditHistoryStatus.UnAuditing;
                    auditModel.ModelType = modelType;
                    operationList.Add(auditModel);
                }
            }

            //总经理级附加
            if (hidZJLFJ.Value != "")
            {
                string[] fj2Ids = hidZJLFJ.Value.TrimEnd(',').Split(',');
                for (int i = 0; i < fj2Ids.Length; i++)
                {
                    if (fj2Ids[i].Trim() != "")
                    {
                        if (checkAuditor.IndexOf(fj2Ids[i].Trim()) < 0)
                        {
                            checkAuditor += fj2Ids[i].Trim() + ",";
                            ESP.Compatible.Employee emp = new ESP.Compatible.Employee(int.Parse(fj2Ids[i]));

                            ESP.Finance.Entity.WorkFlowInfo auditModel = new ESP.Finance.Entity.WorkFlowInfo();
                            auditModel.ModelId = modelId;
                            auditModel.AuditType = ESP.Finance.Utility.auditorType.operationAudit_Type_ZJLFJ;
                            auditModel.AuditorUserID = int.Parse(emp.SysID);
                            auditModel.AuditorUserCode = emp.ID;
                            auditModel.AuditorEmployeeName = emp.Name;
                            auditModel.AuditorUserName = emp.ITCode;
                            auditModel.AuditStatus = (int)ESP.Finance.Utility.AuditHistoryStatus.UnAuditing;
                            auditModel.ModelType = modelType;
                            operationList.Add(auditModel);

                        }
                    }
                }
            }

            //CEO
            if (hidCEO.Value != "" && hidCEO.Value != hidZJLSP.Value)
            {
                if (checkAuditor.IndexOf(hidCEO.Value.TrimEnd(',')) < 0)
                {
                    checkAuditor += hidCEO.Value.TrimEnd(',') + ",";
                    ESP.Compatible.Employee emp = new ESP.Compatible.Employee(int.Parse(hidCEO.Value.TrimEnd(',')));

                    ESP.Finance.Entity.WorkFlowInfo auditModel = new ESP.Finance.Entity.WorkFlowInfo();
                    auditModel.ModelId = modelId;
                    auditModel.AuditType = ESP.Finance.Utility.auditorType.operationAudit_Type_CEO;
                    auditModel.AuditorUserID = int.Parse(emp.SysID);
                    auditModel.AuditorUserCode = emp.ID;
                    auditModel.AuditorEmployeeName = emp.Name;
                    auditModel.AuditorUserName = emp.ITCode;
                    auditModel.AuditStatus = (int)ESP.Finance.Utility.AuditHistoryStatus.UnAuditing;
                    auditModel.ModelType = modelType;
                    operationList.Add(auditModel);
                }
            }

            //finace auditor
            ESP.Finance.Entity.BranchInfo branchModel = ESP.Finance.BusinessLogic.BranchManager.GetModelByCode(projectCode.Substring(0, 1));
            int FirstFinanceID = branchModel.FirstFinanceID;

            ESP.Compatible.Employee financeEmp = new ESP.Compatible.Employee(FirstFinanceID);
            ESP.Finance.Entity.WorkFlowInfo FinanceModel = new ESP.Finance.Entity.WorkFlowInfo();
            FinanceModel.ModelId = modelId;
            FinanceModel.AuditType = ESP.Finance.Utility.auditorType.operationAudit_Type_Financial;
            FinanceModel.AuditorUserID = FirstFinanceID;
            FinanceModel.AuditorUserCode = financeEmp.ID;
            FinanceModel.AuditorEmployeeName = financeEmp.Name;
            FinanceModel.AuditorUserName = financeEmp.ITCode;
            FinanceModel.AuditStatus = (int)ESP.Finance.Utility.AuditHistoryStatus.UnAuditing;
            FinanceModel.ModelType = modelType;
            operationList.Add(FinanceModel);

            return operationList;
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            if (Request[ESP.Finance.Utility.RequestName.ModelType] == "Refund")
            {
                Response.Redirect("/Edit/RefundTabEdit.aspx");
            }
        }

    }
}