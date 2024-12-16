using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using WorkFlowDAO;
using WorkFlow.Model;
using WorkFlowLibary;
using WorkFlowImpl;
using ModelTemplate;
using ModelTemplate.BLL;
using ESP.Finance.Utility;
using ESP.Framework.BusinessLogic;

public partial class Purchase_Requisition_SetAuditor : ESP.Web.UI.PageBase
{
    int returnId = 0;
    WFUSERS[] initiators;//工作流的发起者，有可能是由多个人同时发起创建的
    WFProcessMgrImpl processMgr = new WFProcessMgrImpl();//持久层工作流的管理类
    ModelProcess mp;//模板工作流的实例
    IWFProcess np;//持久层的工作流实例(接口对象)
    Hashtable context = new Hashtable();//所有工作流对外对象的存储器
    WorkFlowDAO.ProcessInstanceDao PIDao;//工作流数据访问对象
    PROCESSINSTANCES pi;//一个工作流实例
    WorkFlowDAO.WorkItemDataDao workitemdao = new WorkItemDataDao();
    ModelTemplate.BLL.ModelManager manager = new ModelTemplate.BLL.ModelManager();//模板工作流的管理类，用于操作模板工作流的
    protected ESP.Finance.Entity.ReturnInfo returnModel = null;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(Request[ESP.Finance.Utility.RequestName.ReturnID]))
        {
            returnId = int.Parse(Request[ESP.Finance.Utility.RequestName.ReturnID]);
            returnModel = ESP.Finance.BusinessLogic.ReturnManager.GetModel(returnId);
            if (!IsPostBack)
            {
                ViewContorl(returnModel);
            }
        }
    }


    private void ViewContorl(ESP.Finance.Entity.ReturnInfo model)
    {
        bool isHaveYS = false;
        bool isHaveZJ = false;
        bool isHaveZJL = false;
        bool isHaveCEO = false;
        bool isHaveFA = false;
        // bool isSameDept = true;
        string removeTypes = "";
        //bool IsWangZhen = false;
        int rowcount = 0;
        // string wangzhenId = "," + ESP.Configuration.ConfigurationManager.SafeAppSettings["WangZhenId"] + ",";
        //string NoFADept = System.Configuration.ConfigurationManager.AppSettings["DoNotNeedFA"].ToString();

        ESP.Finance.Entity.ProjectInfo projectModel = null;
        ESP.Finance.Entity.SupporterInfo supportModel = null;
        ESP.Purchase.Entity.GeneralInfo prModel = null;
        //判断分公司的第一级审核人是否是FA
        string branchCode = model.ProjectCode.Substring(0, 1);

        ESP.Finance.Entity.BranchInfo branchModel = ESP.Finance.BusinessLogic.BranchManager.GetModelByCode(branchCode);

        //当前登录人的所在部门
        int deptId = model.DepartmentID.Value;
        //ESP.Compatible.Department CurrentDept = ESP.Compatible.DepartmentManager.GetDepartmentByPK(deptId);
        ESP.Framework.Entity.DepartmentInfo deptParent = GetRootDepartmentID(deptId);

        //ESP.Framework.Entity.OperationAuditManageInfo manageModel = ESP.Framework.BusinessLogic.OperationAuditManageManager.GetModelByUserId(model.RequestorID.Value);
        ESP.Framework.Entity.OperationAuditManageInfo manageModel = null;

        if (model.ProjectID != null && model.ProjectID.Value != 0)
        {
            manageModel = ESP.Framework.BusinessLogic.OperationAuditManageManager.GetModelByProjectId(model.ProjectID.Value);
        }
        if (manageModel == null)
            manageModel = ESP.Framework.BusinessLogic.OperationAuditManageManager.GetModelByUserId(model.RequestorID.Value); ;

        if (manageModel == null)
            manageModel = ESP.Framework.BusinessLogic.OperationAuditManageManager.GetModelByDepId(deptId);

        if (model.ProjectID != null && model.ProjectID.Value != 0)
        {
            projectModel = ESP.Finance.BusinessLogic.ProjectManager.GetModelWithOutDetailList(model.ProjectID.Value);
            if (projectModel != null && projectModel.GroupID != model.DepartmentID)
            {
                //取得支持方的信息
                if (model.ProjectID > 0 && model.DepartmentID > 0)
                {

                    IList<ESP.Finance.Entity.SupporterInfo> lists =
                        ESP.Finance.BusinessLogic.SupporterManager.GetList(
                            string.Format("ProjectID={0} and GroupID={1}", model.ProjectID, model.DepartmentID));
                    if (lists.Count > 0)
                    {
                        foreach (ESP.Finance.Entity.SupporterInfo sup in lists)
                        {
                            if (sup.GroupID.Value == model.DepartmentID.Value)
                            {
                                supportModel = sup;
                                break;
                            }
                        }

                    }
                }
            }
        }

        ESP.HumanResource.Entity.EmployeesInPositionsInfo positionModel = ESP.HumanResource.BusinessLogic.EmployeesInPositionsManager.GetModel(model.RequestorID.Value);

        ESP.Framework.Entity.OperationAuditManageInfo selfManageModel = null;

        if (positionModel.DepartmentID != model.DepartmentID && manageModel.UserId == 0)
        {
            selfManageModel = ESP.Framework.BusinessLogic.OperationAuditManageManager.GetModelByDepId(positionModel.DepartmentID);
        }

        //如果是协议供应商的PR，不需要FA审批
        if (model.NeedPurchaseAudit)
        {
            isHaveFA = true;
            removeTypes += ESP.Finance.Utility.auditorType.operationAudit_Type_YS + ",";
            removeTypes += ESP.Finance.Utility.auditorType.operationAudit_Type_FA + ",";
        }

        if (manageModel.FAId == 0 || branchModel.FirstFinanceID == manageModel.FAId)
        {
            isHaveFA = true;
            removeTypes += ESP.Finance.Utility.auditorType.operationAudit_Type_FA + ",";
        }

        //如果是GM项目，不需要FA审批
        if (model.ProjectCode.IndexOf("GM*") >= 0 || model.ProjectCode.IndexOf("*GM") >= 0 || model.ProjectCode.IndexOf(ESP.Finance.Configuration.ConfigurationManager.ShunYaShortEN) >= 0)
        {
            removeTypes += ESP.Finance.Utility.auditorType.operationAudit_Type_YS + ",";
            if (deptParent.DepartmentID == 19)
                removeTypes += ESP.Finance.Utility.auditorType.operationAudit_Type_FA + ",";
        }

        if (model.PRID != null)
        {
            prModel = ESP.Purchase.BusinessLogic.GeneralInfoManager.GetModel(model.PRID.Value);
        }
        removeTypes += ESP.Finance.Utility.auditorType.purchase_first + ",";
        removeTypes += ESP.Finance.Utility.auditorType.purchase_major2 + ",";
        removeTypes += ESP.Finance.Utility.auditorType.operationAudit_Type_Financial + ",";
        removeTypes += ESP.Finance.Utility.auditorType.operationAudit_Type_Financial2 + ",";
        removeTypes += ESP.Finance.Utility.auditorType.operationAudit_Type_Financial3 + ",";




        #region 使用GM项目号
        if ((model.ProjectCode.IndexOf("GM*") >= 0 || model.ProjectCode.IndexOf("*GM") >= 0 || model.ProjectCode.IndexOf(ESP.Finance.Configuration.ConfigurationManager.ShunYaShortEN) >= 0))
        {
            ESP.Purchase.Entity.GeneralInfo generalModel = ESP.Purchase.BusinessLogic.GeneralInfoManager.GetModel(model.PRID.Value);
            if (manageModel != null)
            {
                if (generalModel.ValueLevel != 1)
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

                    if (CurrentUserID == manageModel.DirectorId || (CurrentUserID != manageModel.ManagerId && model.PreFee > manageModel.DirectorAmount))
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

                    if (CurrentUserID == manageModel.ManagerId || model.PreFee > manageModel.ManagerAmount)
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
                        CEOCell4.InnerHtml = manageModel.CEOAmount.ToString("#,##0.00") + "以上";
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

                if (manageModel != null && manageModel.FAId != 0 && manageModel.FAId != branchModel.FirstFinanceID)
                {
                    System.Web.UI.HtmlControls.HtmlTableRow FARow = new System.Web.UI.HtmlControls.HtmlTableRow();
                    System.Web.UI.HtmlControls.HtmlTableCell FACell1 = new System.Web.UI.HtmlControls.HtmlTableCell();
                    System.Web.UI.HtmlControls.HtmlTableCell FACell2 = new System.Web.UI.HtmlControls.HtmlTableCell();
                    System.Web.UI.HtmlControls.HtmlTableCell FACell3 = new System.Web.UI.HtmlControls.HtmlTableCell();
                    System.Web.UI.HtmlControls.HtmlTableCell FACell4 = new System.Web.UI.HtmlControls.HtmlTableCell();
                    System.Web.UI.HtmlControls.HtmlTableCell FACell5 = new System.Web.UI.HtmlControls.HtmlTableCell();
                    System.Web.UI.HtmlControls.HtmlTableCell FACell6 = new System.Web.UI.HtmlControls.HtmlTableCell();
                    FACell1.Align = "Center";
                    FACell2.Align = "Center";
                    FACell3.Align = "Center";
                    FACell4.Align = "Center";
                    FACell5.Align = "Center";
                    FACell6.Align = "Center";
                    FACell1.InnerHtml = rowcount.ToString();
                    FACell2.InnerHtml = manageModel.FAName;
                    FACell3.InnerHtml = new ESP.Compatible.Employee(manageModel.FAId).PositionDescription;
                    FACell4.InnerHtml = "FA";
                    FACell5.InnerHtml = ESP.Finance.Utility.auditorType.operationAudit_Type_Names[ESP.Finance.Utility.auditorType.operationAudit_Type_FA];
                    FACell6.InnerHtml = "&nbsp;";

                    FARow.Attributes["id"] = "tr_" + manageModel.FAId + "_FA";

                    hidFA.Value = manageModel.FAId.ToString();
                    FARow.Cells.Add(FACell1);
                    FARow.Cells.Add(FACell2);
                    FARow.Cells.Add(FACell3);
                    FARow.Cells.Add(FACell4);
                    FARow.Cells.Add(FACell5);
                    FARow.Cells.Add(FACell6);
                    FARow.Attributes["class"] = "td";
                    tab.Rows.Add(FARow);

                }


            }
        }
        #endregion

        else
        {
            rowcount = 1;
            if (selfManageModel != null && model.RequestorID != selfManageModel.DirectorId && model.RequestorID != selfManageModel.ManagerId)
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
            if (projectModel != null && model.RequestorID.Value != (supportModel == null ? projectModel.ApplicantUserID : supportModel.LeaderUserID))
            {
                if (model.ReturnType == (int)ESP.Purchase.Common.PRTYpe.PR_MediaFA || model.ReturnType == (int)ESP.Purchase.Common.PRTYpe.PR_PriFA || model.ReturnType == (int)ESP.Purchase.Common.PRTYpe.MediaPR)
                { }
                else
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
                        YSCell2.InnerHtml = supportModel == null ? projectModel.ApplicantEmployeeName : supportModel.LeaderEmployeeName;
                        YSCell3.InnerHtml = "项目负责人";
                        YSCell4.InnerHtml = "项目负责人";
                        string trId = YSRow.Attributes["id"] = "tr_" + (supportModel == null ? projectModel.ApplicantUserID.ToString() : supportModel.LeaderUserID.ToString()) + "_YSSP";
                        YSCell5.InnerHtml = "项目负责人审批";
                        YSCell6.InnerHtml = "<a href=\"\" onclick=\"openEmployee('YS');return false;\">添加</a>";

                        hidYS.Value += (supportModel == null ? projectModel.ApplicantUserID.ToString() : supportModel.LeaderUserID.ToString()) + ",";
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
            }
            #endregion

            #region 常规PR单付款申请,增加总监级审批
            // if (manageModel != null && (!OperationAuditManageManager.GetCurrentUserIsManager(CurrentUser.SysID)))
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

            #region 申请人是总监/金额大于1w 增加总经理级审批
            //if (((CurrentUser.SysID == hidZJSP.Value || model.PreFee > 10000) && !isHaveZJL))
            if (CurrentUserID == manageModel.DirectorId || (CurrentUserID != manageModel.ManagerId && model.PreFee > manageModel.DirectorAmount))
            {
                if (manageModel != null)
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
            }
            #endregion

            #region 大于10w,且ceo总监以下,增加ceo级审批
            if (CurrentUserID == manageModel.ManagerId || model.PreFee > manageModel.ManagerAmount)
            {
                if (manageModel != null)
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
                    CEOCell4.InnerHtml = manageModel.CEOAmount.ToString("#,##0.00") + "以上";
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
            }
            #endregion


        }
    }


    /// <summary>
    /// 获得用户所在公司的ID，如北京的用户登录获得总部的部门ID
    /// </summary>
    /// <param name="UserID">用户编号</param>
    /// <returns>返回部门编号</returns>
    private ESP.Framework.Entity.DepartmentInfo GetRootDepartmentID(int deptid)
    {
        ESP.Framework.Entity.DepartmentInfo model = null;
        ESP.Framework.Entity.DepartmentInfo departmentInfo = ESP.Framework.BusinessLogic.DepartmentManager.Get(deptid);
        if (!string.IsNullOrEmpty(departmentInfo.Description))
        {
            model = ESP.Framework.BusinessLogic.DepartmentManager.Get(int.Parse(departmentInfo.Description));
        }
        //List<ESP.Framework.Entity.DepartmentInfo> depList = new List<ESP.Framework.Entity.DepartmentInfo>();
        //if (departmentInfo != null && departmentInfo.DepartmentLevel == 3)
        //{
        //    // 添加当前用户上级部门信息
        //    depList = ESP.Framework.BusinessLogic.DepartmentManager.GetDepartmentListByID(deptid, depList);
        //    foreach (ESP.Framework.Entity.DepartmentInfo dm in depList)
        //    {
        //        if (dm != null && dm.DepartmentLevel == 1)
        //        {
        //            model = dm;
        //        }
        //    }
        //}

        return model;
    }

    protected void btnCommit_Click(object sender, EventArgs e)
    {
        //try
        //{
        int firstAuditorId = 0;
        string exMail = string.Empty;
        string[] majordomoIds = null;
        if (!string.IsNullOrEmpty(Request[ESP.Finance.Utility.RequestName.ReturnID]))
        {
            returnId = int.Parse(Request[ESP.Finance.Utility.RequestName.ReturnID]);
            returnModel = ESP.Finance.BusinessLogic.ReturnManager.GetModel(returnId);
        }
        //创建工作流实例
        int processid = 0;

        processid = this.createTemplateProcess(returnModel, false);
        //returnModel = ESP.Finance.BusinessLogic.ReturnManager.GetModel(returnId);
        //returnModel.ReturnStatus = (int)ESP.Finance.Utility.PaymentStatus.Submit;
        mp = manager.loadProcessModelByID(processid);
        context = new Hashtable();
        //创建一个发起者，实际应用时就是单据的创建者
        WFUSERS wfuser = new WFUSERS();
        wfuser.Id = returnModel.RequestorID.Value;
        initiators = new WFUSERS[1];
        initiators[0] = wfuser;
        context.Add(ContextConstants.CURRENT_USER, wfuser);//将发起人加入上下文
        context.Add(ContextConstants.CURRENT_USER_ASSIGNMENT, initiators);//将发起人数组加入上下文
        context.Add(ContextConstants.SUBMIT_ACTION_TYPE, "1");//提交操作代码：1
        context.Add(ContextConstants.SUBMIT_ACTION_NAME, "PN付款业务审核");//提交操作代码：1
        context.Add(ContextConstants.SUBMIT_ACTION_DISPLAYNAME, "PN付款业务审核");//提交操作代码：1


        //设置生成工作流的必备项
        np = processMgr.preCreate_process(Convert.ToInt32(mp.ProcessID), context);

        processMgr.create_process(np, initiators, "start");


        pi = np.get_instance_data();//获取工作流数据
        pi.ACTIVEPERSONID = 1;
        pi.ACTIVEWOEKITEMID = 1;
        pi.NOTIFYPARENTPROCESS = 1;
        pi.PARENTADDRESS = null;
        //以上4参数暂时没有特别用出，使用默认值即可
        //持久化工作流的事件处理
        np.persist();//持久化


        //激活start任务
        np.load_task(np.get_lastActivity().getWorkItem().Id.ToString(), np.get_lastActivity().getWorkItem().TASKNAME);
        np.get_lastActivity().active();

        returnModel.WorkItemID = Convert.ToInt32(np.get_lastActivity().getWorkItem().Id);
        returnModel.WorkItemName = np.get_lastActivity().getWorkItem().TASKNAME;
        returnModel.InstanceID = Convert.ToInt32(np.get_instance_data().Id);
        returnModel.ProcessID = processid;
        //complete start任务
        np.load_task(np.get_lastActivity().getWorkItem().Id.ToString(), np.get_lastActivity().getWorkItem().TASKNAME);
        ((MySubject)np).TaskCompleteEvent += new MySubject.TaskCompleteHandler(Purchase_Requisition_SetOperationAudit_TaskCompleteEvent);
        np.complete_task(np.get_lastActivity().getWorkItem().Id.ToString(), np.get_lastActivity().getWorkItem().TASKNAME, context[ContextConstants.SUBMIT_ACTION_NAME].ToString());

        ESP.Finance.BusinessLogic.ReturnManager.Update(returnModel);
        //   ESP.Finance.BusinessLogic.ReturnManager.UpdateWorkFlow(returnModel.ReturnID, returnModel.WorkItemID.Value, returnModel.WorkItemName, returnModel.ProcessID.Value, returnModel.InstanceID.Value);


        if (hidYS.Value != "")
        {
            firstAuditorId = int.Parse(hidYS.Value.Split(',')[0]);
        }
        else
        {
            majordomoIds = hidZJSP.Value.TrimEnd(',').Split(',');
            if (returnModel.ReturnType == (int)ESP.Purchase.Common.PRTYpe.PR_MediaFA || returnModel.ReturnType == (int)ESP.Purchase.Common.PRTYpe.PR_PriFA)
            {
                majordomoIds = hidZJLSP.Value.TrimEnd(',').Split(',');
                if (!string.IsNullOrEmpty(majordomoIds[0]))
                {
                    firstAuditorId = int.Parse(majordomoIds[0]);
                }
            }
            else
            {
                if (!string.IsNullOrEmpty(majordomoIds[0]))
                {
                    firstAuditorId = int.Parse(majordomoIds[0]);
                }
            }
        }
        try
        {
            //给第一级审核人发送邮件
            ESP.Framework.Entity.EmployeeInfo emp = ESP.Framework.BusinessLogic.EmployeeManager.Get(firstAuditorId);
            if (emp != null)
                SendMailHelper.SendMailReturnCommit(returnModel, CurrentUser.Name, emp.FullNameCN, CurrentUser.EMail, emp.Email);

            //给知会人发邮件
            List<string> ZH1Ids = hidZJZH.Value.TrimEnd(',').Split(',').ToList();
            string ZH1Emails = "";
            for (int i = 0; i < ZH1Ids.Count; i++)
            {
                if (ZH1Ids[i].Trim() != "")
                    ZH1Emails += new ESP.Compatible.Employee(int.Parse(ZH1Ids[i])).EMail + ",";
            }
            if (ZH1Emails != "")
            {
                SendMailHelper.SendMailToZH2FK(returnModel, returnModel.ReturnCode, CurrentUser.Name, new ESP.Compatible.Employee(firstAuditorId).Name, ZH1Emails.TrimEnd(','));
            }
        }
        catch
        {
            exMail = "(邮件发送失败!)";
        }
        Page.ClientScript.RegisterStartupScript(typeof(string), "", "window.location.href='PaymentGeneralList.aspx';alert('已成功设置付款业务审批人并提交成功！" + exMail + "');", true);

    }

    void Purchase_Requisition_SetOperationAudit_TaskCompleteEvent(Hashtable context)
    {
        workitemdao.insertItemData(Convert.ToInt32(np.get_lastActivity().getWorkItem().Id), Convert.ToInt32(np.get_instance_data().Id), SerializeFactory.Serialize(returnModel));

    }


    private int createTemplateProcess(ESP.Finance.Entity.ReturnInfo model, bool isSave)
    {
        int ret = 0;
        string checkAuditor = ",";
        ModelTemplate.BLL.ModelManager manager = new ModelManager();
        List<ESP.Finance.Entity.ReturnAuditHistInfo> operationList = new List<ESP.Finance.Entity.ReturnAuditHistInfo>();
        List<ModelTemplate.ModelTask> lists = new List<ModelTask>();
        ModelTemplate.ModelTask task = new ModelTask();
        task.TaskName = "start";
        task.TaskType = WfStateContants.TASKTYPE_SERIALTASK;
        task.DisPlayName = "start";
        task.RoleName = model.RequestorID.ToString();

        ModelTemplate.ModelTask lastTask = null;
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
                        string userName;
                        ESP.Compatible.Employee emp = new ESP.Compatible.Employee(int.Parse(prejudicationIds[i]));
                        userName = emp.Name;
                        ModelTemplate.ModelTask prejudicationTask = new ModelTask();
                        prejudicationTask.TaskName = "PN付款审核" + userName + prejudicationIds[i];
                        prejudicationTask.TaskType = WfStateContants.TASKTYPE_SERIALTASK;
                        prejudicationTask.DisPlayName = "PN付款审核" + userName + prejudicationIds[i];
                        prejudicationTask.RoleName = prejudicationIds[i];

                        ESP.Finance.Entity.ReturnAuditHistInfo auditModel = new ESP.Finance.Entity.ReturnAuditHistInfo();
                        auditModel.ReturnID = returnId;
                        auditModel.AuditType = ESP.Finance.Utility.auditorType.operationAudit_Type_YS;
                        auditModel.AuditorUserID = int.Parse(emp.SysID);
                        auditModel.AuditorUserCode = emp.ID;
                        auditModel.AuditorEmployeeName = emp.Name;
                        auditModel.AuditorUserName = emp.ITCode;
                        auditModel.AuditeStatus = (int)ESP.Finance.Utility.AuditHistoryStatus.UnAuditing;
                        operationList.Add(auditModel);

                        if (lastTask == null)
                        {
                            ModelTemplate.Transition trans = new Transition();
                            trans.TransitionName = "start";
                            trans.TransitionTo = prejudicationTask.TaskName;
                            task.Transations.Add(trans);
                            lists.Add(task);
                        }
                        else
                        {
                            ModelTemplate.Transition trans = new Transition();
                            trans.TransitionName = lastTask.TaskName;
                            trans.TransitionTo = prejudicationTask.TaskName;
                            lastTask.Transations.Add(trans);
                            lists.Add(lastTask);
                        }
                        lastTask = prejudicationTask;
                    }
                }
            }
        }

        //总监级知会人
        if (hidZJZH.Value != "")
        {
            string[] ZH1Ids = hidZJZH.Value.TrimEnd(',').Split(',');
            for (int i = 0; i < ZH1Ids.Length; i++)
            {
                if (ZH1Ids[i].Trim() != "")
                {
                    string userName;
                    ESP.Compatible.Employee emp = new ESP.Compatible.Employee(int.Parse(ZH1Ids[i]));
                    userName = emp.Name;
                    ModelTemplate.ModelTask ZHMajordomoTask = new ModelTask();
                    ZHMajordomoTask.TaskName = "PN付款审核知会" + userName + ZH1Ids[i];
                    ZHMajordomoTask.TaskType = WfStateContants.TASKTYPE_NOTIFY;
                    ZHMajordomoTask.DisPlayName = "PN付款审核知会" + userName + ZH1Ids[i];
                    ZHMajordomoTask.RoleName = ZH1Ids[i];

                    if (lastTask == null)
                    {
                        ModelTemplate.Transition trans = new Transition();
                        trans.TransitionName = "start";
                        trans.TransitionTo = ZHMajordomoTask.TaskName;
                        task.Transations.Add(trans);
                    }
                    else
                    {
                        ModelTemplate.Transition trans = new Transition();
                        trans.TransitionName = lastTask.TaskName;
                        trans.TransitionTo = ZHMajordomoTask.TaskName;
                        lastTask.Transations.Add(trans);
                    }
                    lists.Add(ZHMajordomoTask);

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
                ModelTemplate.ModelTask addMajordomoTask = new ModelTask();
                addMajordomoTask.TaskName = "PN付款审核" + emp.Name + hidZJSP.Value.TrimEnd(',');
                addMajordomoTask.TaskType = WfStateContants.TASKTYPE_SERIALTASK;
                addMajordomoTask.DisPlayName = "PN付款审核" + emp.Name + hidZJSP.Value.TrimEnd(',');
                addMajordomoTask.RoleName = hidZJSP.Value.TrimEnd(',');

                ESP.Finance.Entity.ReturnAuditHistInfo auditModel = new ESP.Finance.Entity.ReturnAuditHistInfo();
                auditModel.ReturnID = returnId;
                auditModel.AuditType = ESP.Finance.Utility.auditorType.operationAudit_Type_ZJSP;
                auditModel.AuditorUserID = int.Parse(emp.SysID);
                auditModel.AuditorUserCode = emp.ID;
                auditModel.AuditorEmployeeName = emp.Name;
                auditModel.AuditorUserName = emp.ITCode;
                auditModel.AuditeStatus = (int)ESP.Finance.Utility.AuditHistoryStatus.UnAuditing;
                operationList.Add(auditModel);


                if (lastTask == null)
                {
                    ModelTemplate.Transition trans = new Transition();
                    trans.TransitionName = "start";
                    trans.TransitionTo = addMajordomoTask.TaskName;
                    task.Transations.Add(trans);
                    lists.Add(task);
                }
                else
                {
                    ModelTemplate.Transition trans = new Transition();
                    trans.TransitionName = lastTask.TaskName;
                    trans.TransitionTo = addMajordomoTask.TaskName;
                    lastTask.Transations.Add(trans);
                    lists.Add(lastTask);
                }

                lastTask = addMajordomoTask;
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
                        ModelTemplate.ModelTask append1Task = new ModelTask();
                        append1Task.TaskName = "PN付款审核" + emp.Name + fj1Ids[i];
                        append1Task.TaskType = WfStateContants.TASKTYPE_SERIALTASK;
                        append1Task.DisPlayName = "PN付款审核" + emp.Name + fj1Ids[i];
                        append1Task.RoleName = fj1Ids[i];

                        ESP.Finance.Entity.ReturnAuditHistInfo auditModel = new ESP.Finance.Entity.ReturnAuditHistInfo();
                        auditModel.ReturnID = returnId;
                        auditModel.AuditType = ESP.Finance.Utility.auditorType.operationAudit_Type_ZJFJ;
                        auditModel.AuditorUserID = int.Parse(emp.SysID);
                        auditModel.AuditorUserCode = emp.ID;
                        auditModel.AuditorEmployeeName = emp.Name;
                        auditModel.AuditorUserName = emp.ITCode;
                        auditModel.AuditeStatus = (int)ESP.Finance.Utility.AuditHistoryStatus.UnAuditing;
                        operationList.Add(auditModel);

                        ModelTemplate.Transition trans = new Transition();
                        trans.TransitionName = lastTask.DisPlayName;
                        trans.TransitionTo = append1Task.DisPlayName;
                        lastTask.Transations.Add(trans);
                        lists.Add(lastTask);

                        lastTask = append1Task;
                    }
                }
            }
        }

        //总经理级知会人
        if (hidZJLZH.Value != "")
        {
            string[] zh2Ids = hidZJLZH.Value.TrimEnd(',').Split(',');
            for (int i = 0; i < zh2Ids.Length; i++)
            {
                if (zh2Ids[i].Trim() != "")
                {
                    string userName = new ESP.Compatible.Employee(int.Parse(zh2Ids[i])).Name;
                    ModelTemplate.ModelTask ZHgeneralTask = new ModelTask();
                    ZHgeneralTask.TaskName = "PN付款审核知会" + userName + zh2Ids[i];
                    ZHgeneralTask.TaskType = WfStateContants.TASKTYPE_NOTIFY;
                    ZHgeneralTask.DisPlayName = "PN付款审核知会" + userName + zh2Ids[i];
                    ZHgeneralTask.RoleName = zh2Ids[i];

                    ModelTemplate.Transition trans = new Transition();
                    trans.TransitionName = lastTask.TaskName;
                    trans.TransitionTo = ZHgeneralTask.TaskName;
                    lastTask.Transations.Add(trans);

                    lists.Add(ZHgeneralTask);
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
                ModelTemplate.ModelTask addGeneralTask = new ModelTask();
                addGeneralTask.TaskName = "PN付款审核" + emp.Name + hidZJLSP.Value.TrimEnd(',');
                addGeneralTask.TaskType = WfStateContants.TASKTYPE_SERIALTASK;
                addGeneralTask.DisPlayName = "PN付款审核" + emp.Name + hidZJLSP.Value.TrimEnd(',');
                addGeneralTask.RoleName = hidZJLSP.Value.TrimEnd(',');


                ESP.Finance.Entity.ReturnAuditHistInfo auditModel = new ESP.Finance.Entity.ReturnAuditHistInfo();
                auditModel.ReturnID = returnId;
                auditModel.AuditType = ESP.Finance.Utility.auditorType.operationAudit_Type_ZJLSP;
                auditModel.AuditorUserID = int.Parse(emp.SysID);
                auditModel.AuditorUserCode = emp.ID;
                auditModel.AuditorEmployeeName = emp.Name;
                auditModel.AuditorUserName = emp.ITCode;
                auditModel.AuditeStatus = (int)ESP.Finance.Utility.AuditHistoryStatus.UnAuditing;
                operationList.Add(auditModel);


                if (lastTask == null)
                {
                    ModelTemplate.Transition trans = new Transition();
                    trans.TransitionName = "start";
                    trans.TransitionTo = addGeneralTask.TaskName;
                    task.Transations.Add(trans);
                    lists.Add(task);
                }
                else
                {
                    ModelTemplate.Transition trans = new Transition();
                    trans.TransitionName = lastTask.DisPlayName;
                    trans.TransitionTo = addGeneralTask.DisPlayName;
                    lastTask.Transations.Add(trans);
                    lists.Add(lastTask);
                }
                lastTask = addGeneralTask;
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
                        ModelTemplate.ModelTask append2Task = new ModelTask();
                        append2Task.TaskName = "PN付款审核" + emp.Name + fj2Ids[i];
                        append2Task.TaskType = WfStateContants.TASKTYPE_SERIALTASK;
                        append2Task.DisPlayName = "PN付款审核" + emp.Name + fj2Ids[i];
                        append2Task.RoleName = fj2Ids[i];

                        ESP.Finance.Entity.ReturnAuditHistInfo auditModel = new ESP.Finance.Entity.ReturnAuditHistInfo();
                        auditModel.ReturnID = returnId;
                        auditModel.AuditType = ESP.Finance.Utility.auditorType.operationAudit_Type_ZJLFJ;
                        auditModel.AuditorUserID = int.Parse(emp.SysID);
                        auditModel.AuditorUserCode = emp.ID;
                        auditModel.AuditorEmployeeName = emp.Name;
                        auditModel.AuditorUserName = emp.ITCode;
                        auditModel.AuditeStatus = (int)ESP.Finance.Utility.AuditHistoryStatus.UnAuditing;
                        operationList.Add(auditModel);

                        ModelTemplate.Transition trans = new Transition();
                        trans.TransitionName = lastTask.DisPlayName;
                        trans.TransitionTo = append2Task.DisPlayName;
                        lastTask.Transations.Add(trans);
                        lists.Add(lastTask);

                        lastTask = append2Task;
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
                ModelTemplate.ModelTask CEOTask = new ModelTask();
                CEOTask.TaskName = "PN付款审核" + emp.Name + hidCEO.Value.TrimEnd(',');
                CEOTask.TaskType = WfStateContants.TASKTYPE_SERIALTASK;
                CEOTask.DisPlayName = "PN付款审核" + emp.Name + hidCEO.Value.TrimEnd(',');
                CEOTask.RoleName = hidCEO.Value.TrimEnd(',');

                ESP.Finance.Entity.ReturnAuditHistInfo auditModel = new ESP.Finance.Entity.ReturnAuditHistInfo();
                auditModel.ReturnID = returnId;
                auditModel.AuditType = ESP.Finance.Utility.auditorType.operationAudit_Type_CEO;
                auditModel.AuditorUserID = int.Parse(emp.SysID);
                auditModel.AuditorUserCode = emp.ID;
                auditModel.AuditorEmployeeName = emp.Name;
                auditModel.AuditorUserName = emp.ITCode;
                auditModel.AuditeStatus = (int)ESP.Finance.Utility.AuditHistoryStatus.UnAuditing;
                operationList.Add(auditModel);

                if (lastTask == null)
                {
                    ModelTemplate.Transition trans = new Transition();
                    trans.TransitionName = "start";
                    trans.TransitionTo = CEOTask.TaskName;
                    task.Transations.Add(trans);
                    lists.Add(task);
                }
                else
                {
                    ModelTemplate.Transition trans = new Transition();
                    trans.TransitionName = lastTask.DisPlayName;
                    trans.TransitionTo = CEOTask.DisPlayName;
                    lastTask.Transations.Add(trans);
                    lists.Add(lastTask);
                }
                lastTask = CEOTask;
            }
        }

        //finace auditor
        ESP.Finance.Entity.BranchInfo branchModel = ESP.Finance.BusinessLogic.BranchManager.GetModelByCode(returnModel.ProjectCode.Substring(0, 1));
        int FirstFinanceID = branchModel.FirstFinanceID;

        //增加部门判断,N的不同部门对应不同的第一级财务审批人
        ESP.Finance.Entity.BranchDeptInfo branchdept = ESP.Finance.BusinessLogic.BranchDeptManager.GetModel(branchModel.BranchID, returnModel.DepartmentID.Value);
        if (branchdept != null)
            FirstFinanceID = branchdept.FianceFirstAuditorID;

        ESP.Compatible.Employee financeEmp = new ESP.Compatible.Employee(FirstFinanceID);
        ESP.Finance.Entity.ReturnAuditHistInfo FinanceModel = new ESP.Finance.Entity.ReturnAuditHistInfo();
        FinanceModel.ReturnID = returnId;
        FinanceModel.AuditType = ESP.Finance.Utility.auditorType.operationAudit_Type_Financial;
        FinanceModel.AuditorUserID = int.Parse(financeEmp.SysID);
        FinanceModel.AuditorUserCode = financeEmp.ID;
        FinanceModel.AuditorEmployeeName = financeEmp.Name;
        FinanceModel.AuditorUserName = financeEmp.ITCode;
        FinanceModel.AuditeStatus = (int)ESP.Finance.Utility.AuditHistoryStatus.UnAuditing;
        operationList.Add(FinanceModel);


        ModelTemplate.Transition endTrans = new Transition();
        endTrans.TransitionName = lastTask.DisPlayName;
        endTrans.TransitionTo = "end";
        lastTask.Transations.Add(endTrans);
        lists.Add(lastTask);
        if (model.ProcessID != null && model.InstanceID != null)
        {
            PIDao = new ProcessInstanceDao();
            PIDao.TerminateProcess(model.ProcessID.Value, model.InstanceID.Value);
        }
        if (ESP.Finance.BusinessLogic.ReturnAuditHistManager.Add(model, operationList) > 0)
        {
            if (isSave == false)
                ret = manager.ImportData("PN付款业务审核(" + model.ReturnID + ")", "PN付款业务审核(" + model.ReturnID + ")", "1.0", model.RequestEmployeeName, lists);
            else
                ret = 1;
        }
        return ret;
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {

        string backUrl = "ReturnEdit.aspx";
        Response.Redirect(backUrl + "?" + RequestName.ReturnID + "=" + returnId);
    }


    /// <summary>
    /// 采购物料类别是否包含北京采购部专属类别
    /// </summary>
    /// <returns></returns>
    private int getAuditor(ESP.Purchase.Entity.GeneralInfo generalModel)
    {
        List<ESP.Purchase.Entity.OrderInfo> orderList = ESP.Purchase.BusinessLogic.OrderInfoManager.GetListByGeneralId(generalModel.id);
        ESP.Purchase.Entity.TypeInfo typeInfo = null;

        typeInfo = ESP.Purchase.BusinessLogic.TypeManager.GetModel(orderList[0].producttype);
        ESP.HumanResource.Entity.EmployeesInPositionsInfo positionModel = ESP.HumanResource.BusinessLogic.EmployeesInPositionsManager.GetModel(generalModel.requestor);
        ESP.Framework.Entity.DepartmentInfo deptModel = GetRootDepartmentID(positionModel.DepartmentID);

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
