using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using ESP.Purchase.BusinessLogic;
using ESP.Purchase.Common;
using ESP.Purchase.Entity;
using WorkFlowDAO;
using WorkFlow.Model;
using WorkFlowLibary;
using WorkFlowImpl;
using ModelTemplate;
using ModelTemplate.BLL;
using SerializeFactory = WorkFlowDAO.SerializeFactory;
using ESP.Framework.BusinessLogic;
using ESP.Framework.Entity;


public partial class UserControls_Edit_newSetAuditor : System.Web.UI.UserControl
{
    int generalid = 0;
    WFUSERS[] initiators;//工作流的发起者，有可能是由多个人同时发起创建的
    WFProcessMgrImpl processMgr = new WFProcessMgrImpl();//持久层工作流的管理类
    ModelProcess mp;//模板工作流的实例
    IWFProcess np;//持久层的工作流实例(接口对象)
    Hashtable context = new Hashtable();//所有工作流对外对象的存储器
    WorkFlowDAO.ProcessInstanceDao p;//工作流数据访问对象
    PROCESSINSTANCES pi;//一个工作流实例
    WorkFlowDAO.WorkItemDataDao workitemdao = new WorkItemDataDao();
    ModelTemplate.BLL.ModelManager manager = new ModelTemplate.BLL.ModelManager();//模板工作流的管理类，用于操作模板工作流的
    protected GeneralInfo generalInfo = null;

    private ESP.Compatible.Employee currentUser;
    public ESP.Compatible.Employee CurrentUser
    {
        get { return currentUser; }
        set { currentUser = value; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(Request[RequestName.GeneralID]))
        {
            generalid = int.Parse(Request[RequestName.GeneralID]);
            generalInfo = GeneralInfoManager.GetModel(generalid);
            if (!IsPostBack)
            {
                ViewContorl(generalInfo);
            }
        }
    }


    private void ViewContorl(GeneralInfo model)
    {
        bool isHaveYS = false;
        bool isHaveZJ = false;
        bool isHaveZJL = false;
        bool isHaveCEO = false;
        //bool isSameDept = true;
        string removeTypes = "";
        bool IsWangZhen = false;
        int rowcount = 0;
        int deptID = 0;

        ESP.Finance.Entity.ProjectInfo projectModel = null;
        ESP.Finance.Entity.SupporterInfo supportModel = null;
        if (model.Project_id != 0)
        {
            projectModel = ESP.Finance.BusinessLogic.ProjectManager.GetModelWithOutDetailList(model.Project_id);
            if (null != projectModel && projectModel.GroupID != model.Departmentid)
            {
                //取得支持方的信息
                if (model.Project_id > 0 && model.Departmentid > 0)
                {

                    IList<ESP.Finance.Entity.SupporterInfo> lists =
                        ESP.Finance.BusinessLogic.SupporterManager.GetList(
                            string.Format("ProjectID={0} and GroupID={1}", model.Project_id, model.Departmentid));
                    if (lists.Count > 0)
                    {
                        foreach (ESP.Finance.Entity.SupporterInfo sup in lists)
                        {
                            if (sup.GroupID.Value == model.Departmentid)
                            {
                                supportModel = sup;
                                break;
                            }
                        }

                    }
                }
            }
        }

        deptID = model.Departmentid;

        //OperationAuditManageInfo manageModel = OperationAuditManageManager.GetModelByUserId(model.requestor);
        ESP.Framework.Entity.OperationAuditManageInfo manageModel = null;

        if (model.Project_id != 0)
        {
            manageModel = ESP.Framework.BusinessLogic.OperationAuditManageManager.GetModelByProjectId(model.Project_id);
        }
        if (manageModel == null)
            manageModel = ESP.Framework.BusinessLogic.OperationAuditManageManager.GetModelByUserId(model.requestor); ;

        if (manageModel == null)
            manageModel = OperationAuditManageManager.GetModelByDepId(deptID);

        ESP.HumanResource.Entity.EmployeesInPositionsInfo positionModel = ESP.HumanResource.BusinessLogic.EmployeesInPositionsManager.GetModel(model.requestor);

        ESP.Framework.Entity.OperationAuditManageInfo selfManageModel = null;

        if (positionModel.DepartmentID != model.Departmentid && manageModel.UserId == 0)
        {
            selfManageModel = ESP.Framework.BusinessLogic.OperationAuditManageManager.GetModelByDepId(positionModel.DepartmentID);
        }

        //框架合同，金额判断取押金
        decimal chkTotalPrice = model.Requisitionflow == ESP.Purchase.Common.State.requisitionflow_toFC ? model.Foregift : model.totalprice;
        if (chkTotalPrice == 0)
            chkTotalPrice = 1;

        #region 使用GM项目号

        ESP.Compatible.Department CurrentDept = ESP.Compatible.DepartmentManager.GetDepartmentByPK(CurrentUser.GetDepartmentIDs()[0]);

        if ((model.project_code.IndexOf("GM*") >= 0 || model.project_code.IndexOf("*GM") >= 0 || model.project_code.IndexOf(ESP.Finance.Configuration.ConfigurationManager.ShunYaShortEN) >= 0))
        {
            int tabRowCount = tab.Rows.Count;
            for (int i = tabRowCount; i > 0; i--)
            {
                if ((i - 1) != 0)
                {
                    tab.Rows.RemoveAt(i - 1);
                }
            }
            hidYS.Value = "";
            hidZJSP.Value = "";
            hidZJLSP.Value = "";
            hidZJLFJ.Value = "";
            hidCEO.Value = "";
            isHaveCEO = false;
            isHaveZJ = false;
            isHaveZJL = false;
            isHaveYS = false;

            if (manageModel != null)
            {
                if (int.Parse(CurrentUser.SysID) != manageModel.DirectorId && int.Parse(CurrentUser.SysID) != manageModel.ManagerId )
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
                    ZJCell4.InnerHtml = "";
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



                if ((int.Parse(CurrentUser.SysID) == manageModel.DirectorId || (int.Parse(CurrentUser.SysID) != manageModel.ManagerId && chkTotalPrice > manageModel.DirectorAmount)))
                {
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
                    ZJLCell4.InnerHtml = "总经理预审";
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

                if (int.Parse(CurrentUser.SysID) == manageModel.ManagerId || chkTotalPrice > manageModel.ManagerAmount)
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
                    CEOCell4.InnerHtml = "CEO审批";
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
                }


            }
        }

        #endregion

        else
        {
            rowcount = 1;

            if (selfManageModel != null && model.requestor != selfManageModel.DirectorId && model.requestor != selfManageModel.ManagerId)
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
                YSCell3.InnerHtml = "总监审批";
                YSCell4.InnerHtml = "总监审批";
                string trId = YSRow.Attributes["id"] = "tr_" + selfManageModel.DirectorId.ToString() + "_YSSP";
                YSCell5.InnerHtml = "总监审批";
                YSCell6.InnerHtml = "";

                hidYS.Value += selfManageModel.DirectorId + ",";
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
            if (isHaveYS == false && projectModel != null && model.requestor != (supportModel == null ? projectModel.ApplicantUserID : supportModel.LeaderUserID))
            {
                int appId = (supportModel == null ? projectModel.ApplicantUserID : supportModel.LeaderUserID.Value);
                string appName = supportModel == null ? projectModel.ApplicantEmployeeName : supportModel.LeaderEmployeeName;

                //(model.PRType != (int)ESP.Purchase.Common.PRTYpe.PR_MediaFA && model.PRType != (int)ESP.Purchase.Common.PRTYpe.PR_PriFA)

                if ((int.Parse(CurrentUser.SysID) != manageModel.DirectorId && int.Parse(CurrentUser.SysID) != manageModel.ManagerId) && appId != manageModel.PurchaseDirectorId)
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
                    YSCell2.InnerHtml = appName;
                    YSCell3.InnerHtml = "项目负责人";
                    YSCell4.InnerHtml = "项目负责人";
                    string trId = YSRow.Attributes["id"] = "tr_" + (supportModel == null ? projectModel.ApplicantUserID.ToString() : supportModel.LeaderUserID.ToString()) + "_YSSP";
                    YSCell5.InnerHtml = "项目负责人审批";
                    YSCell6.InnerHtml = "<a href=\"\" onclick=\"openEmployee('YS');return false;\">添加</a>";

                    hidYS.Value += appId + ",";
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

            #region 默认总监
            if (!isHaveZJ && (int.Parse(CurrentUser.SysID) != manageModel.DirectorId && int.Parse(CurrentUser.SysID) != manageModel.ManagerId) )
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
                ZJCell5.InnerHtml = "总监审批";
                ZJCell6.InnerHtml = "<a href=\"\" onclick=\"openEmployee('ZJSP');return false;\">添加</a>&nbsp;<a href=\"\" onclick=\"openEmployee1('ZJSP','" + trId + "','" + manageModel.DirectorId + "');return false;\">更改</a>";

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
            #endregion


            #region 默认总经理
            if ((int.Parse(CurrentUser.SysID) == manageModel.DirectorId || (int.Parse(CurrentUser.SysID) != manageModel.ManagerId && chkTotalPrice > manageModel.DirectorAmount)))
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
                ZJLCell4.InnerHtml =manageModel.ManagerAmount.ToString("#,##0.00"); 
                string trId1 = ZJLRow.Attributes["id"] = "tr_" + manageModel.ManagerId + "_ZJLSP";
                ZJLCell5.InnerHtml = "总经理审批";
                ZJLCell6.InnerHtml = "<a href=\"\" onclick=\"openEmployee('ZJLSP');return false;\">添加</a>&nbsp;<a href=\"\" onclick=\"openEmployee1('ZJLSP','" + trId1 + "','" + manageModel.ManagerId + "');return false;\">更改</a>";//&nbsp;<a href=\"\" onclick=\"removeRow('ZJSP','" + manageModel.DirectorId + "','" + trId + "');return false;\">删除</a>";

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

            if (int.Parse(CurrentUser.SysID) == manageModel.ManagerId || chkTotalPrice > manageModel.ManagerAmount)
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

            }


        }
    }

    private void AddUserPoint(int generalid)
    {
        OrderInfo order = ESP.Purchase.BusinessLogic.OrderInfoManager.GetModelByGeneralID(generalid);
        if (order != null && order.supplierId != 0)
        {
            ESPAndSupplySuppliersRelation relation = ESP.Purchase.BusinessLogic.ESPAndSupplySuppliersRelationManager.GetModelByEid(order.supplierId);
            if (relation != null)
            {
                string sql = string.Format(" supplysupplierid={0} and receiverid={1}", relation.SupplySupplierId, currentUser.SysID);
                List<SupplierShareInfo> sharelist = ESP.Purchase.BusinessLogic.SupplierShareManager.GetList(sql);
                if (sharelist != null && sharelist.Count > 0)
                {
                    foreach (SupplierShareInfo share in sharelist)
                    {
                        ESP.UserPoint.BusinessLogic.UserPointFacade.ShareSupplier(share.UserId, order.supplierId, "Supplier_Used");
                    }
                    ESP.Purchase.BusinessLogic.SupplierShareManager.UpdateStatus(relation.SupplySupplierId, int.Parse(currentUser.SysID));
                }
            }
        }
    }

    protected void btnCommit_Click(object sender, EventArgs e)
    {
        string Msg = "";

        if (generalInfo.status != State.requisition_save && generalInfo.status != State.requisition_return)
        {
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('此条数据已经提交，不能重复提交！');window.location.href='/Purchase/Default.aspx';", true);
            return;
        }
        //李彦娥生成的大于3000的媒体稿费单和对私单不进行验证
        if (generalInfo.PRType == (int)ESP.Purchase.Common.PRTYpe.PR_PriFA || generalInfo.PRType == (int)ESP.Purchase.Common.PRTYpe.PR_MediaFA)
        {
            Msg = "";
        }
        else
        {
            Msg = ESP.ITIL.BusinessLogic.申请单业务检查.申请单提交(generalInfo);
        }
        if (Msg != "")
        {
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('" + Msg + "');", true);
            return;
        }
        //try
        //{
        if (SaveOperationAuditor())
        {
            int processid = 0;
            Msg = ESP.ITIL.BusinessLogic.申请单业务设置.申请单提交(CurrentUser, ref generalInfo);
            if (Msg != "")
            {
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('" + Msg + "');", true);
                return;
            }
            AddUserPoint(generalInfo.id);
            processid = this.createTemplateProcess(generalInfo);
            mp = manager.loadProcessModelByID(processid);
            context = new Hashtable();
            //创建一个发起者，实际应用时就是单据的创建者
            WFUSERS wfuser = new WFUSERS();
            wfuser.Id = generalInfo.requestor;
            initiators = new WFUSERS[1];
            initiators[0] = wfuser;
            context.Add(ContextConstants.CURRENT_USER, wfuser);//将发起人加入上下文
            context.Add(ContextConstants.CURRENT_USER_ASSIGNMENT, initiators);//将发起人数组加入上下文
            context.Add(ContextConstants.SUBMIT_ACTION_TYPE, "1");//提交操作代码：1
            context.Add(ContextConstants.SUBMIT_ACTION_NAME, "PR单业务审核");//提交操作代码：1
            context.Add(ContextConstants.SUBMIT_ACTION_DISPLAYNAME, "PR单业务审核");//提交操作代码：1


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

            generalInfo.WorkitemID = Convert.ToInt32(np.get_lastActivity().getWorkItem().Id);
            generalInfo.WorkItemName = np.get_lastActivity().getWorkItem().TASKNAME;
            generalInfo.InstanceID = Convert.ToInt32(np.get_instance_data().Id);
            generalInfo.ProcessID = processid;
            //complete start任务
            np.load_task(np.get_lastActivity().getWorkItem().Id.ToString(), np.get_lastActivity().getWorkItem().TASKNAME);
            ((MySubject)np).TaskCompleteEvent += new MySubject.TaskCompleteHandler(Purchase_Requisition_SetOperationAudit_TaskCompleteEvent);
            np.complete_task(np.get_lastActivity().getWorkItem().Id.ToString(), np.get_lastActivity().getWorkItem().TASKNAME, context[ContextConstants.SUBMIT_ACTION_NAME].ToString());


            bool issuccess = GeneralInfoManager.UpdateAndAddLog(generalInfo, null, null, Request, ESP.Purchase.Common.State.PR_CostRecordsType.PR单提交);

            if (!issuccess)
            {
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('采购申请单提交失败!');", true);
                return;
            }

            //如果申请单为协议供应商删除付款账期信息
            if ((generalInfo.status == State.requisition_save || generalInfo.status == State.requisition_commit || generalInfo.status == State.requisition_temporary_commit) && generalInfo.source == "协议供应商")
            {
                int periodsCount = PaymentPeriodManager.GetList(" gid = " + generalInfo.id).Tables[0].Rows.Count;
                if (periodsCount == 0)
                {
                    int supplierPeriod = 0;
                    //获得协议供应商的账期
                    List<OrderInfo> orderList = OrderInfoManager.GetListByGeneralId(generalInfo.id);
                    if (orderList != null && orderList.Count > 0)
                    {
                        foreach (OrderInfo order in orderList)
                        {
                            if (order.supplierId > 0)
                            {
                                SupplierInfo supplier = SupplierManager.GetModel(order.supplierId);
                                try
                                {
                                    supplierPeriod = int.Parse(supplier.business_paytime);
                                }
                                catch { }
                                break;
                            }
                        }
                    }

                    PaymentPeriodInfo paymentPeriod = new PaymentPeriodInfo();
                    paymentPeriod.gid = generalInfo.id;
                    paymentPeriod.expectPaymentPrice = getTotalPrice(" general_id = " + generalInfo.id);
                    paymentPeriod.expectPaymentPercent = 100;
                    paymentPeriod.periodDay = State.supplierpayment[supplierPeriod].ToString();
                    paymentPeriod.beginDate = DateTime.Parse(DateTime.Now.AddMonths(2 + supplierPeriod).ToString("yyyy-MM") + "-01").AddDays(14);
                    //paymentPeriod.endDate = DateTime.Parse(DateTime.Now.AddMonths(3 + supplierPeriod).ToString("yyyy-MM") + "-01").AddDays(-1);
                    paymentPeriod.periodType = (int)State.PeriodType.period;

                    PaymentPeriodManager.Add(paymentPeriod);
                }
            }

            //发信
            LogInfo log = new LogInfo();
            log.Gid = generalInfo.id;
            log.LogMedifiedTeme = DateTime.Now;
            log.LogUserId = int.Parse(CurrentUser.SysID);
            log.Des = string.Format(State.log_requisition_commit, CurrentUser.Name + "(" + currentUser.ITCode + ")", DateTime.Now.ToString());
            ESP.Framework.Entity.AuditBackUpInfo auditBackUp = ESP.Framework.BusinessLogic.AuditBackUpManager.GetLayOffModelByUserID(int.Parse(CurrentUser.SysID));
            if (auditBackUp != null)
                log.Des = ESP.Framework.BusinessLogic.EmployeeManager.Get(auditBackUp.BackupUserID).FullNameCN + "代替" + log.Des;
            LogManager.AddLog(log, Request);

            ////删除业务审核日志
            OperationAuditLogManager.DeleteByGid(generalid);

            int firstAuditorId = 0;

            if (hidYS.Value != "")
            {
                firstAuditorId = int.Parse(hidYS.Value.Split(',')[0]);
            }
            else
            {
                string CGId = hidCGSP.Value.TrimEnd(',');
                string CGZJId = hidCGZJSP.Value.TrimEnd(',');

                string[] majordomoIds = hidZJSP.Value.TrimEnd(',').Split(',');//总监审批
                string[] addGeneralIds = hidZJLSP.Value.TrimEnd(',').Split(',');//总经理审批
                string ceoId = hidCEO.Value.TrimEnd(',');                         //ceo
                List<string> ZH1Ids = hidZJZH.Value.TrimEnd(',').Split(',').ToList();
                string ZH1Emails = "";

                if (!string.IsNullOrEmpty(CGId))
                {
                    firstAuditorId = int.Parse(CGId);
                }
                else if (majordomoIds[0] == "" && addGeneralIds[0] != "")
                {
                    firstAuditorId = int.Parse(addGeneralIds[0]);
                }
                else if (majordomoIds[0] == "" && addGeneralIds[0] == "" && ceoId != "")
                {
                    firstAuditorId = int.Parse(ceoId);
                }
                else
                {
                    firstAuditorId = int.Parse(majordomoIds[0]);
                }
            }
            string exMail = string.Empty;
            try
            {
                string ret = ESP.Purchase.BusinessLogic.SendMailHelper.SendMailPR(generalInfo, Request, generalInfo.PrNo, generalInfo.requestorname, State.getEmployeeEmailBySysUserId(generalInfo.enduser), State.getEmployeeEmailBySysUserId(generalInfo.goods_receiver), State.getEmployeeEmailBySysUserId(firstAuditorId));
            }
            catch
            {
                exMail = "(邮件发送失败!)";
            }
                Page.ClientScript.RegisterStartupScript(typeof(string), "", "window.location.href='RequisitionSaveList.aspx';alert('" + generalInfo.PrNo + "已成功设置业务审核人并提交成功，请在查询中心查询审批状态。"+exMail+"');", true);
        }
        else
        {
            throw new Exception();
        }
        //}
        //catch (Exception ex)
        //{
        //    string s = ex.Message + "设置业务审核人失败！";
        //    Page.ClientScript.RegisterStartupScript(typeof(string), "", "alert(" + ESP.Utilities.JavascriptUtility.QuoteJScriptString(s, false, true) + ");", true);
        //}
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

    public decimal getTotalPrice(string term)
    {
        DataSet ds = OrderInfoManager.GetList(term);

        decimal totalprice = 0;
        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        {
            totalprice += decimal.Parse(ds.Tables[0].Rows[i]["total"].ToString());
        }
        return totalprice;
    }

    void Purchase_Requisition_SetOperationAudit_TaskCompleteEvent(Hashtable context)
    {
        workitemdao.insertItemData(Convert.ToInt32(np.get_lastActivity().getWorkItem().Id), Convert.ToInt32(np.get_instance_data().Id), SerializeFactory.Serialize(generalInfo));
    }

    private int createTemplateProcess(GeneralInfo generalInfo)
    {
        int ret;
        string checkAuditor = ",";
        ModelTemplate.BLL.ModelManager manager = new ModelManager();
        List<ModelTemplate.ModelTask> lists = new List<ModelTask>();
        ModelTemplate.ModelTask task = new ModelTask();
        task.TaskName = "start";
        task.TaskType = WfStateContants.TASKTYPE_SERIALTASK;
        task.DisPlayName = "start";
        task.RoleName = generalInfo.requestor.ToString();
        ArrayList arrIds = new ArrayList();
        ModelTemplate.ModelTask lastTask = null;
        //预审人
        if (hidYS.Value != "")
        {
            string[] prejudicationIds = hidYS.Value.TrimEnd(',').Split(',');
            for (int i = 0; i < prejudicationIds.Length; i++)
            {
                if (prejudicationIds[i].Trim() != "" && !arrIds.Contains(prejudicationIds[i]))
                {
                    if (checkAuditor.IndexOf(prejudicationIds[i].Trim()) < 0)
                    {
                        checkAuditor += prejudicationIds[i].Trim() + ",";
                        string userName = new ESP.Compatible.Employee(int.Parse(prejudicationIds[i])).Name;
                        ModelTemplate.ModelTask prejudicationTask = new ModelTask();
                        prejudicationTask.TaskName = "PR单审核" + userName + prejudicationIds[i];
                        prejudicationTask.TaskType = WfStateContants.TASKTYPE_SERIALTASK;
                        prejudicationTask.DisPlayName = "PR单审核" + userName + prejudicationIds[i];
                        prejudicationTask.RoleName = prejudicationIds[i];
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
                        arrIds.Add(prejudicationIds[i]);
                    }
                }
            }
        }


        //采购审批
        if (hidCGSP.Value != "" && !arrIds.Contains(hidCGSP.Value.TrimEnd(',')))
        {
            if (checkAuditor.IndexOf(hidCGSP.Value) < 0)
            {
                checkAuditor += hidCGSP.Value + ",";
                string userName = new ESP.Compatible.Employee(int.Parse(hidCGSP.Value.TrimEnd(','))).Name;
                ModelTemplate.ModelTask addMajordomoTask = new ModelTask();
                addMajordomoTask.TaskName = "PR单审核" + userName + hidCGSP.Value.TrimEnd(',');
                addMajordomoTask.TaskType = WfStateContants.TASKTYPE_SERIALTASK;
                addMajordomoTask.DisPlayName = "PR单审核" + userName + hidCGSP.Value.TrimEnd(',');
                addMajordomoTask.RoleName = hidCGSP.Value.TrimEnd(',');

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
                arrIds.Add(hidCGSP.Value.TrimEnd(','));
            }
        }

        //总监级知会人
        if (hidZJZH.Value != "")
        {
            string[] ZH1Ids = hidZJZH.Value.TrimEnd(',').Split(',');
            for (int i = 0; i < ZH1Ids.Length; i++)
            {
                if (ZH1Ids[i].Trim() != "" && !arrIds.Contains(ZH1Ids[i]))
                {
                    string userName = new ESP.Compatible.Employee(int.Parse(ZH1Ids[i])).Name;
                    ModelTemplate.ModelTask ZHMajordomoTask = new ModelTask();
                    ZHMajordomoTask.TaskName = "PR单审核知会" + userName + ZH1Ids[i];
                    ZHMajordomoTask.TaskType = WfStateContants.TASKTYPE_NOTIFY;
                    ZHMajordomoTask.DisPlayName = "PR单审核知会" + userName + ZH1Ids[i];
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
                    arrIds.Add(ZH1Ids[i]);
                }
            }
        }
        //总监级审核人
        if (hidZJSP.Value != "" && !arrIds.Contains(hidZJSP.Value.TrimEnd(',')))
        {
            if (checkAuditor.IndexOf(hidZJSP.Value) < 0)
            {
                checkAuditor += hidZJSP.Value + ",";
                string userName = new ESP.Compatible.Employee(int.Parse(hidZJSP.Value.TrimEnd(','))).Name;
                ModelTemplate.ModelTask addMajordomoTask = new ModelTask();
                addMajordomoTask.TaskName = "PR单审核" + userName + hidZJSP.Value.TrimEnd(',');
                addMajordomoTask.TaskType = WfStateContants.TASKTYPE_SERIALTASK;
                addMajordomoTask.DisPlayName = "PR单审核" + userName + hidZJSP.Value.TrimEnd(',');
                addMajordomoTask.RoleName = hidZJSP.Value.TrimEnd(',');

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
                arrIds.Add(hidZJSP.Value.TrimEnd(','));
            }
        }

        //总监级附加审核人
        if (hidZJFJ.Value != "")
        {
            string[] fj1Ids = hidZJFJ.Value.TrimEnd(',').Split(',');
            for (int i = 0; i < fj1Ids.Length; i++)
            {
                if (fj1Ids[i].Trim() != "" && !arrIds.Contains(fj1Ids[i]))
                {
                    if (checkAuditor.IndexOf(fj1Ids[i]) < 0)
                    {
                        checkAuditor += fj1Ids[i].Trim() + ",";
                        string userName = new ESP.Compatible.Employee(int.Parse(fj1Ids[i])).Name;
                        ModelTemplate.ModelTask append1Task = new ModelTask();
                        append1Task.TaskName = "PR单审核" + userName + fj1Ids[i];
                        append1Task.TaskType = WfStateContants.TASKTYPE_SERIALTASK;
                        append1Task.DisPlayName = "PR单审核" + userName + fj1Ids[i];
                        append1Task.RoleName = fj1Ids[i];

                        ModelTemplate.Transition trans = new Transition();
                        trans.TransitionName = lastTask.DisPlayName;
                        trans.TransitionTo = append1Task.DisPlayName;
                        lastTask.Transations.Add(trans);
                        lists.Add(lastTask);

                        lastTask = append1Task;
                        arrIds.Add(fj1Ids[i]);
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
                if (zh2Ids[i].Trim() != "" && !arrIds.Contains(zh2Ids[i]))
                {
                    string userName = new ESP.Compatible.Employee(int.Parse(zh2Ids[i])).Name;
                    ModelTemplate.ModelTask ZHgeneralTask = new ModelTask();
                    ZHgeneralTask.TaskName = "PR单审核知会" + userName + zh2Ids[i];
                    ZHgeneralTask.TaskType = WfStateContants.TASKTYPE_NOTIFY;
                    ZHgeneralTask.DisPlayName = "PR单审核知会" + userName + zh2Ids[i];
                    ZHgeneralTask.RoleName = zh2Ids[i];

                    ModelTemplate.Transition trans = new Transition();
                    trans.TransitionName = lastTask.TaskName;
                    trans.TransitionTo = ZHgeneralTask.TaskName;
                    lastTask.Transations.Add(trans);

                    lists.Add(ZHgeneralTask);
                    arrIds.Add(zh2Ids[i]);
                }
            }
        }

        //总经理级审核人
        if (hidZJLSP.Value != "" && !arrIds.Contains(hidZJLSP.Value.TrimEnd(',')))
        {
            if (checkAuditor.IndexOf(hidZJLSP.Value) < 0)
            {
                checkAuditor += hidZJLSP.Value + ",";
                string userName = new ESP.Compatible.Employee(int.Parse(hidZJLSP.Value.TrimEnd(','))).Name;
                ModelTemplate.ModelTask addGeneralTask = new ModelTask();
                addGeneralTask.TaskName = "PR单审核" + userName + hidZJLSP.Value.TrimEnd(',');
                addGeneralTask.TaskType = WfStateContants.TASKTYPE_SERIALTASK;
                addGeneralTask.DisPlayName = "PR单审核" + userName + hidZJLSP.Value.TrimEnd(',');
                addGeneralTask.RoleName = hidZJLSP.Value.TrimEnd(',');

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
                arrIds.Add(hidZJLSP.Value.TrimEnd(','));
            }
        }
        //总经理级附加
        if (hidZJLFJ.Value != "")
        {
            string[] fj2Ids = hidZJLFJ.Value.TrimEnd(',').Split(',');
            for (int i = 0; i < fj2Ids.Length; i++)
            {
                if (fj2Ids[i].Trim() != "" && !arrIds.Contains(fj2Ids[i]))
                {
                    if (checkAuditor.IndexOf(fj2Ids[i].Trim()) < 0)
                    {
                        checkAuditor += fj2Ids[i].Trim() + ",";
                        string userName = new ESP.Compatible.Employee(int.Parse(fj2Ids[i])).Name;
                        ModelTemplate.ModelTask append2Task = new ModelTask();
                        append2Task.TaskName = "PR单审核" + userName + fj2Ids[i];
                        append2Task.TaskType = WfStateContants.TASKTYPE_SERIALTASK;
                        append2Task.DisPlayName = "PR单审核" + userName + fj2Ids[i];
                        append2Task.RoleName = fj2Ids[i];

                        ModelTemplate.Transition trans = new Transition();
                        trans.TransitionName = lastTask.DisPlayName;
                        trans.TransitionTo = append2Task.DisPlayName;
                        lastTask.Transations.Add(trans);
                        lists.Add(lastTask);

                        lastTask = append2Task;
                        arrIds.Add(fj2Ids[i]);
                    }
                }
            }
        }
        //CEO
        if (hidCEO.Value != "" && !arrIds.Contains(hidCEO.Value.TrimEnd(',')))
        {
            if (checkAuditor.IndexOf(hidCEO.Value) < 0)
            {
                checkAuditor += hidCEO.Value + ",";
                string CEOName = new ESP.Compatible.Employee(int.Parse(hidCEO.Value.TrimEnd(','))).Name;
                ModelTemplate.ModelTask CEOTask = new ModelTask();
                CEOTask.TaskName = "PR单审核" + CEOName + hidCEO.Value.TrimEnd(',');
                CEOTask.TaskType = WfStateContants.TASKTYPE_SERIALTASK;
                CEOTask.DisPlayName = "PR单审核" + CEOName + hidCEO.Value.TrimEnd(',');
                CEOTask.RoleName = hidCEO.Value.TrimEnd(',');

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

        if (hidCGZJSP.Value != "" && !arrIds.Contains(hidCGZJSP.Value.TrimEnd(',')))
        {
            if (checkAuditor.IndexOf(hidCGZJSP.Value) < 0)
            {
                checkAuditor += hidCGZJSP.Value.TrimEnd(',') + ",";
                string CGZJName = new ESP.Compatible.Employee(int.Parse(hidCGZJSP.Value.TrimEnd(','))).Name;

                ModelTemplate.ModelTask append3Task = new ModelTask();
                append3Task.TaskName = "PR单业务审核" + CGZJName + hidCGZJSP.Value.TrimEnd(',');
                append3Task.TaskType = WfStateContants.TASKTYPE_SERIALTASK;
                append3Task.DisPlayName = "PR单业务审核" + CGZJName + hidCGZJSP.Value.TrimEnd(',');
                append3Task.RoleName = hidCGZJSP.Value.TrimEnd(',');

                if (lastTask != null)
                {
                    ModelTemplate.Transition trans = new Transition();
                    trans.TransitionName = lastTask.DisPlayName;
                    trans.TransitionTo = append3Task.DisPlayName;
                    lastTask.Transations.Add(trans);
                    lists.Add(lastTask);

                    lastTask = append3Task;
                }
            }
        }

        ModelTemplate.Transition endTrans = new Transition();
        endTrans.TransitionName = lastTask.DisPlayName;
        endTrans.TransitionTo = "end";
        lastTask.Transations.Add(endTrans);
        lists.Add(lastTask);

        ret = manager.ImportData("PR单业务审核(" + generalInfo.PrNo + ")", "PR单业务审核(" + generalInfo.PrNo + ")", "1.0", generalInfo.requestorname, lists);
        return ret;
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request["backUrl"] + "?" + RequestName.GeneralID + "=" + Request[RequestName.GeneralID]);
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {

        if (SaveOperationAuditor())
            Response.Redirect("RequisitionSaveList.aspx");
        else
            Page.ClientScript.RegisterStartupScript(typeof(string), "", "alert('保存业务审批人失败！')", true);
    }

    /// <summary>
    /// 保存业务审批人
    /// </summary>
    /// <returns></returns>
    private bool SaveOperationAuditor()
    {
        generalid = int.Parse(Request[RequestName.GeneralID]);
        generalInfo = GeneralInfoManager.GetModel(generalid);

        List<OperationAuditInfo> operationList = new List<OperationAuditInfo>();
        if (hidYS.Value != "" && hidYS.Value.TrimEnd(',') != "")
        {
            string[] YSIds = hidYS.Value.TrimEnd(',').Split(',');
            for (int i = 0; i < YSIds.Length; i++)
            {
                if (YSIds[i].Trim() != "")
                {
                    OperationAuditInfo model = new OperationAuditInfo();
                    model.general_id = generalid;
                    model.aduitType = State.operationAudit_Type_YS;
                    model.auditorId = int.Parse(YSIds[i]);
                    operationList.Add(model);
                }
            }
        }
        if (hidCGSP.Value != "" && hidCGSP.Value.TrimEnd(',') != "")
        {
            OperationAuditInfo model = new OperationAuditInfo();
            model.general_id = generalid;
            model.aduitType = State.operationAudit_Type_CG;
            model.auditorId = int.Parse(hidCGSP.Value.TrimEnd(','));
            operationList.Add(model);
        }

        if (hidZJSP.Value != "" && hidZJSP.Value.TrimEnd(',') != "")
        {
            OperationAuditInfo model = new OperationAuditInfo();
            model.general_id = generalid;
            model.aduitType = State.operationAudit_Type_ZJSP;
            model.auditorId = int.Parse(hidZJSP.Value.TrimEnd(','));
            operationList.Add(model);
        }
        if (hidZJZH.Value != "" && hidZJZH.Value.TrimEnd(',') != "")
        {
            string[] ZJZHIds = hidZJZH.Value.TrimEnd(',').Split(',');
            for (int i = 0; i < ZJZHIds.Length; i++)
            {
                if (ZJZHIds[i].Trim() != "")
                {
                    OperationAuditInfo model = new OperationAuditInfo();
                    model.general_id = generalid;
                    model.aduitType = State.operationAudit_Type_ZJZH;
                    model.auditorId = int.Parse(ZJZHIds[i]);
                    operationList.Add(model);
                }
            }
        }
        if (hidZJFJ.Value != "" && hidZJFJ.Value.TrimEnd(',') != "")
        {
            string[] ZJFJIds = hidZJFJ.Value.TrimEnd(',').Split(',');
            for (int i = 0; i < ZJFJIds.Length; i++)
            {
                if (ZJFJIds[i].Trim() != "")
                {
                    OperationAuditInfo model = new OperationAuditInfo();
                    model.general_id = generalid;
                    model.aduitType = State.operationAudit_Type_ZJFJ;
                    model.auditorId = int.Parse(ZJFJIds[i]);
                    operationList.Add(model);
                }
            }
        }
        if (hidZJLSP.Value != "" && hidZJLSP.Value.TrimEnd(',') != "")
        {
            OperationAuditInfo model = new OperationAuditInfo();
            model.general_id = generalid;
            model.aduitType = State.operationAudit_Type_ZJLSP;
            model.auditorId = int.Parse(hidZJLSP.Value.TrimEnd(','));
            operationList.Add(model);
        }
        if (hidZJLZH.Value != "" && hidZJLZH.Value.TrimEnd(',') != "")
        {
            string[] ZJLZHIds = hidZJLZH.Value.TrimEnd(',').Split(',');
            for (int i = 0; i < ZJLZHIds.Length; i++)
            {
                if (ZJLZHIds[i].Trim() != "")
                {
                    OperationAuditInfo model = new OperationAuditInfo();
                    model.general_id = generalid;
                    model.aduitType = State.operationAudit_Type_ZJLZH;
                    model.auditorId = int.Parse(ZJLZHIds[i]);
                    operationList.Add(model);
                }
            }
        }
        if (hidZJLFJ.Value != "" && hidZJLFJ.Value.TrimEnd(',') != "")
        {
            string[] ZJLFJIds = hidZJLFJ.Value.TrimEnd(',').Split(',');
            for (int i = 0; i < ZJLFJIds.Length; i++)
            {
                if (ZJLFJIds[i].Trim() != "")
                {
                    OperationAuditInfo model = new OperationAuditInfo();
                    model.general_id = generalid;
                    model.aduitType = State.operationAudit_Type_ZJLFJ;
                    model.auditorId = int.Parse(ZJLFJIds[i]);
                    operationList.Add(model);
                }
            }
        }
        if (hidCEO.Value != "" && hidCEO.Value.TrimEnd(',') != "")
        {
            OperationAuditInfo model = new OperationAuditInfo();
            model.general_id = generalid;
            model.aduitType = State.operationAudit_Type_CEO;
            model.auditorId = int.Parse(hidCEO.Value.TrimEnd(','));
            operationList.Add(model);
        }
        //如果是PFT需要在最后一级加上CEO，无论金额大小
        if (generalInfo.project_code.IndexOf("GM*") >= 0 || generalInfo.project_code.IndexOf("*GM") >= 0 || generalInfo.project_code.IndexOf(ESP.Finance.Configuration.ConfigurationManager.ShunYaShortEN) >= 0)
        {
            string advanceid = string.Empty;
            //判断申请人是否是财务部的人
            int[] depts = CurrentUser.GetDepartmentIDs();
            //如果是财务部的人，carrie 改为david zhang
            //<add key="DavidZhangAudit" value="0"/>
            if (ESP.Finance.Configuration.ConfigurationManager.FADeptID.IndexOf(depts[0].ToString() + "/") >= 0 && ESP.Finance.Configuration.ConfigurationManager.DavidZhangAudit == 1)
            {
                advanceid = ESP.Finance.Configuration.ConfigurationManager.DavidZhangID;
            }
            else
            {
                advanceid = ESP.Finance.Configuration.ConfigurationManager.AdvanceID;
            }
            ESP.Compatible.Employee emp = new ESP.Compatible.Employee(Convert.ToInt32(advanceid));
            OperationAuditInfo model = new OperationAuditInfo();
            model.general_id = generalid;
            model.aduitType = State.operationAudit_Type_ZJLFJ;
            model.auditorId = int.Parse(emp.SysID);
            var tmplist = from item in operationList where item.auditorId == Convert.ToInt32(emp.SysID) select item;
            if (tmplist.ToList().Count <= 0)
                operationList.Add(model);
        }

        if (hidCGZJSP.Value != "" && hidCGZJSP.Value.TrimEnd(',') != "")
        {
            OperationAuditInfo model = new OperationAuditInfo();
            model.general_id = generalid;
            model.aduitType = State.operationAudit_Type_CGZJ;
            model.auditorId = int.Parse(hidCGZJSP.Value.TrimEnd(','));
            operationList.Add(model);
        }

        return OperationAuditManager.Add(operationList, generalid);
    }
}