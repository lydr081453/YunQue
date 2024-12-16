using System;
using System.Data;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ESP.Purchase.Entity;
using ESP.Finance.Entity;
using ESP.Purchase.Common;
using ESP.Purchase.BusinessLogic;
using ESP.Finance.Utility;
using ESP.Finance.BusinessLogic;
using WorkFlowDAO;
using WorkFlow.Model;
using WorkFlowLibary;
using WorkFlowImpl;
using ModelTemplate;
using ModelTemplate.BLL;
using ESP.Framework.Entity;
using ESP.Framework.BusinessLogic;

namespace PurchaseWeb.WorkFlow
{
    public partial class ApproveFlow : ESP.Web.UI.PageBase
    {
        WFUSERS[] initiators;//工作流的发起者，有可能是由多个人同时发起创建的
        WFProcessMgrImpl processMgr = new WFProcessMgrImpl();//持久层工作流的管理类
        ModelProcess mp;//模板工作流的实例
        IWFProcess np;//持久层的工作流实例(接口对象)
        Hashtable context = new Hashtable();//所有工作流对外对象的存储器
        WorkFlowDAO.ProcessInstanceDao p;//工作流数据访问对象
        PROCESSINSTANCES pi;//一个工作流实例
        WorkFlowDAO.WorkItemDataDao workitemdao = new WorkItemDataDao();
        ModelTemplate.BLL.ModelManager manager = new ModelTemplate.BLL.ModelManager();//模板工作流的管理类，用于操作模板工作流的
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

            }
        }
        protected int CurrentUserID = 0;
        protected int CurrentOrderID = 0;
        protected int DeptID = 0;

        private void ViewContorlPR(GeneralInfo model)
        {
            bool isHaveYS = false;
            bool isHaveZJ = false;
            bool isHaveZJL = false;
            bool isHaveCEO = false;
            string removeTypes = "";
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
                            supportModel = lists[0];
                        }
                    }
                }
            }
            if (model.totalprice <= 100000)
            {
                removeTypes += ESP.Purchase.Common.State.operationAudit_Type_CEO + ",";

                removeTypes += ESP.Purchase.Common.State.operationAudit_Type_ZJLSP + ",";
                removeTypes += ESP.Purchase.Common.State.operationAudit_Type_ZJLZH + ",";
                removeTypes += ESP.Purchase.Common.State.operationAudit_Type_ZJLFJ + ",";
            }
            if (model.PRType == (int)PRTYpe.PR_MediaFA || model.PRType == (int)PRTYpe.PR_PriFA)
            {
                if (model.totalprice <= 100000)
                {
                    removeTypes += ESP.Purchase.Common.State.operationAudit_Type_CEO + ",";
                }
                removeTypes += ESP.Purchase.Common.State.operationAudit_Type_YS + ",";
                removeTypes += ESP.Purchase.Common.State.operationAudit_Type_ZJFJ + ",";
                removeTypes += ESP.Purchase.Common.State.operationAudit_Type_ZJSP + ",";
                removeTypes += ESP.Purchase.Common.State.operationAudit_Type_ZJZH + ",";
                removeTypes += ESP.Purchase.Common.State.operationAudit_Type_ZJLZH + ",";
                removeTypes += ESP.Purchase.Common.State.operationAudit_Type_ZJLFJ + ",";
            }
           // OperationAuditManageInfo manageModel = OperationAuditManageManager.GetModelByUserId(model.requestor);
            OperationAuditManageInfo manageModel = null;

            if (model.Project_id != 0)
            {
                manageModel = ESP.Framework.BusinessLogic.OperationAuditManageManager.GetModelByProjectId(model.Project_id);
            }
            if (manageModel == null)
                manageModel = ESP.Framework.BusinessLogic.OperationAuditManageManager.GetModelByUserId(model.requestor); ;

            if (manageModel == null)
               manageModel = OperationAuditManageManager.GetModelByDepId(model.Departmentid);

            if (isHaveYS == false && projectModel != null && model.requestor != (supportModel == null ? projectModel.ApplicantUserID : supportModel.LeaderUserID))
            {
                if ((model.PRType != (int)ESP.Purchase.Common.PRTYpe.PR_MediaFA && model.PRType != (int)ESP.Purchase.Common.PRTYpe.PR_PriFA))
                {
                    //默认项目负责人审核
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

                    YSCell1.InnerHtml = "1";
                    YSCell2.InnerHtml = supportModel == null ? projectModel.ApplicantEmployeeName : supportModel.LeaderEmployeeName;
                    YSCell3.InnerHtml = "项目负责人";
                    YSCell4.InnerHtml = "项目负责人";
                    int ResponseID = supportModel == null ? projectModel.ApplicantUserID : supportModel.LeaderUserID.Value;
                    string trId = YSRow.Attributes["id"] = "tr_" + ResponseID.ToString() + "_YSSP";
                    YSCell5.InnerHtml = "项目负责人审批";
                    YSCell6.InnerHtml = "<a href=\"\" onclick=\"openEmployee('YS');return false;\">添加</a>&nbsp;<a href=\"\" onclick=\"openEmployee1('YS','" + trId + "','" + ResponseID.ToString() + "');return false;\">更改</a>&nbsp;<a href=\"\" onclick=\"removeRow('YS','" + ResponseID.ToString() + "','" + trId + "');return false;\">删除</a>";

                    hidYS.Value += ResponseID.ToString() + ",";
                    YSRow.Cells.Add(YSCell1);
                    YSRow.Cells.Add(YSCell2);
                    YSRow.Cells.Add(YSCell3);
                    YSRow.Cells.Add(YSCell4);
                    YSRow.Cells.Add(YSCell5);
                    YSRow.Cells.Add(YSCell6);
                    YSRow.Attributes["class"] = "td";
                    tab.Rows.Add(YSRow);
                }
            }

            if (!isHaveZJ && model.PRType != (int)PRTYpe.PR_MediaFA && model.PRType != (int)PRTYpe.PR_PriFA)
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

                ZJCell1.InnerHtml = "2";
                ZJCell2.InnerHtml = manageModel.DirectorName;
                ZJCell3.InnerHtml = new ESP.Compatible.Employee(manageModel.DirectorId).PositionDescription;
                ZJCell4.InnerHtml = "10万以下";
                string trId = ZJRow.Attributes["id"] = "tr_" + manageModel.DirectorId + "_ZJSP";
                ZJCell5.InnerHtml = "总监审批";
                ZJCell6.InnerHtml = "<a href=\"\" onclick=\"openEmployee('ZJSP');return false;\">添加</a>&nbsp;<a href=\"\" onclick=\"openEmployee1('ZJSP','" + trId + "','" + manageModel.DirectorId + "');return false;\">更改</a>&nbsp;<a href=\"\" onclick=\"removeRow('ZJSP','" + manageModel.DirectorId + "','" + trId + "');return false;\">删除</a>";

                hidZJSP.Value = manageModel.DirectorId.ToString();
                ZJRow.Cells.Add(ZJCell1);
                ZJRow.Cells.Add(ZJCell2);
                ZJRow.Cells.Add(ZJCell3);
                ZJRow.Cells.Add(ZJCell4);
                ZJRow.Cells.Add(ZJCell5);
                ZJRow.Cells.Add(ZJCell6);
                ZJRow.Attributes["class"] = "td";
                tab.Rows.Add(ZJRow);
            }
            if ((model.totalprice > 100000 && !isHaveZJL) || ((model.PRType == (int)PRTYpe.PR_MediaFA || model.PRType == (int)PRTYpe.PR_PriFA) && !isHaveZJL))
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

                ZJLCell1.InnerHtml = "3";
                ZJLCell2.InnerHtml = manageModel.ManagerName;
                ZJLCell3.InnerHtml = new ESP.Compatible.Employee(manageModel.ManagerId).PositionDescription;
                ZJLCell4.InnerHtml = "10万以上";
                string trId1 = ZJLRow.Attributes["id"] = "tr_" + manageModel.ManagerId + "_ZJLSP";
                ZJLCell5.InnerHtml = "总经理审批";

                ZJLCell6.InnerHtml = "<a href=\"\" onclick=\"openEmployee('ZJLSP');return false;\">添加</a>&nbsp;<a href=\"\" onclick=\"openEmployee1('ZJLSP','" + trId1 + "','" + manageModel.ManagerId + "');return false;\">更改</a>&nbsp;<a href=\"\" onclick=\"removeRow('ZJLSP','" + manageModel.ManagerId + "','" + trId1 + "');return false;\">删除</a>";


                hidZJLSP.Value = manageModel.ManagerId.ToString();
                ZJLRow.Cells.Add(ZJLCell1);
                ZJLRow.Cells.Add(ZJLCell2);
                ZJLRow.Cells.Add(ZJLCell3);
                ZJLRow.Cells.Add(ZJLCell4);
                ZJLRow.Cells.Add(ZJLCell5);
                ZJLRow.Cells.Add(ZJLCell6);
                ZJLRow.Attributes["class"] = "td";
                tab.Rows.Add(ZJLRow);
            }
            if (model.totalprice > 50000 && !isHaveCEO)
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

                string trId1 = CEORow.Attributes["id"] = "tr_" + manageModel.CEOId + "_CEO";
                CEORow.Attributes["id"] = trId1;
                CEOCell1.InnerHtml = "4";
                CEOCell2.InnerHtml = manageModel.CEOName;
                CEOCell3.InnerHtml = new ESP.Compatible.Employee(manageModel.CEOId).PositionDescription;
                CEOCell4.InnerHtml = "10万以上";
                CEOCell5.InnerHtml = "CEO审批";
                CEOCell6.InnerHtml = "<a href=\"\" onclick=\"openEmployee('CEO');return false;\">添加</a>&nbsp;<a href=\"\" onclick=\"openEmployee1('CEO','" + trId1 + "','" + manageModel.CEOId + "');return false;\">更改</a>&nbsp;<a href=\"\" onclick=\"removeRow('CEO','" + manageModel.CEOId + "','" + trId1 + "');return false;\">删除</a>";

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

        private void ViewContorlPN(ESP.Finance.Entity.ReturnInfo model)
        {
            bool isHaveYS = false;
            bool isHaveZJ = false;
            bool isHaveZJL = false;
            bool isHaveCEO = false;
            bool isHaveFA = false;
            string removeTypes = "";
            ESP.Finance.Entity.ProjectInfo projectModel = null;
            ESP.Finance.Entity.SupporterInfo supportModel = null;
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
                            supportModel = lists[0];
                        }
                    }
                }
            }
            //如果是协议供应商的PR，不需要FA审批
            if (model.NeedPurchaseAudit)
            {
                isHaveFA = true;
                removeTypes += ESP.Finance.Utility.auditorType.operationAudit_Type_YS + ",";
                removeTypes += ESP.Finance.Utility.auditorType.operationAudit_Type_FA + ",";
            }
            //如果是GM项目，不需要FA审批
            if (model.ProjectCode.IndexOf("GM*") >= 0 || model.ProjectCode.IndexOf("*GM") >= 0 || model.ProjectCode.IndexOf(ESP.Finance.Configuration.ConfigurationManager.ShunYaShortEN) >= 0)
            {
                removeTypes += ESP.Finance.Utility.auditorType.operationAudit_Type_YS + ",";
                removeTypes += ESP.Finance.Utility.auditorType.operationAudit_Type_FA + ",";
            }
            //如果是对私单3000、3000以上和媒体稿费单3000 3000以上只保留总经理
            if (model.ReturnType == (int)ESP.Purchase.Common.PRTYpe.PR_MediaFA || model.ReturnType == (int)ESP.Purchase.Common.PRTYpe.PR_PriFA || model.ReturnType == (int)ESP.Purchase.Common.PRTYpe.MediaPR)
            {
                removeTypes += ESP.Finance.Utility.auditorType.operationAudit_Type_YS + ",";
                removeTypes += ESP.Finance.Utility.auditorType.operationAudit_Type_ZJLZH + ",";
                removeTypes += ESP.Finance.Utility.auditorType.operationAudit_Type_ZJLFJ + ",";
                removeTypes += ESP.Finance.Utility.auditorType.operationAudit_Type_Financial + ",";
                removeTypes += ESP.Finance.Utility.auditorType.operationAudit_Type_CEO + ",";
                removeTypes += ESP.Finance.Utility.auditorType.operationAudit_Type_ZJFJ + ",";
                removeTypes += ESP.Finance.Utility.auditorType.operationAudit_Type_ZJZH + ",";
                removeTypes += ESP.Finance.Utility.auditorType.operationAudit_Type_ZJSP + ",";
                removeTypes += ESP.Finance.Utility.auditorType.operationAudit_Type_FA + ",";
            }
            else
            {
                if (model.PreFee.Value <= 100000)
                {
                    removeTypes += ESP.Finance.Utility.auditorType.operationAudit_Type_CEO + ",";

                    removeTypes += ESP.Finance.Utility.auditorType.operationAudit_Type_ZJLSP + ",";
                    removeTypes += ESP.Finance.Utility.auditorType.operationAudit_Type_ZJLZH + ",";
                    removeTypes += ESP.Finance.Utility.auditorType.operationAudit_Type_ZJLFJ + ",";
                }
            }
            removeTypes += ESP.Finance.Utility.auditorType.purchase_first + ",";
            removeTypes += ESP.Finance.Utility.auditorType.purchase_major2 + ",";
            removeTypes += ESP.Finance.Utility.auditorType.operationAudit_Type_Financial + ",";
            removeTypes += ESP.Finance.Utility.auditorType.operationAudit_Type_Financial2 + ",";
            removeTypes += ESP.Finance.Utility.auditorType.operationAudit_Type_Financial3 + ",";
            IList<ESP.Finance.Entity.ReturnAuditHistInfo> operationList = ESP.Finance.BusinessLogic.ReturnAuditHistManager.GetUnDelList(model.ReturnID, removeTypes.TrimEnd(','));
            if (operationList != null && operationList.Count > 0)
            {
                int serNum = 1;
                foreach (ESP.Finance.Entity.ReturnAuditHistInfo operation in operationList)
                {
                    if (operation != null)
                    {
                        System.Web.UI.HtmlControls.HtmlTableRow addRow = new System.Web.UI.HtmlControls.HtmlTableRow();
                        System.Web.UI.HtmlControls.HtmlTableCell addCell1 = new System.Web.UI.HtmlControls.HtmlTableCell();
                        System.Web.UI.HtmlControls.HtmlTableCell addCell2 = new System.Web.UI.HtmlControls.HtmlTableCell();
                        System.Web.UI.HtmlControls.HtmlTableCell addCell3 = new System.Web.UI.HtmlControls.HtmlTableCell();
                        System.Web.UI.HtmlControls.HtmlTableCell addCell4 = new System.Web.UI.HtmlControls.HtmlTableCell();
                        System.Web.UI.HtmlControls.HtmlTableCell addCell5 = new System.Web.UI.HtmlControls.HtmlTableCell();
                        System.Web.UI.HtmlControls.HtmlTableCell addCell6 = new System.Web.UI.HtmlControls.HtmlTableCell();

                        addCell1.Align = "Center";
                        addCell2.Align = "Center";
                        addCell3.Align = "Center";
                        addCell4.Align = "Center";
                        addCell5.Align = "Center";
                        addCell6.Align = "Center";

                        ESP.Compatible.Employee EMP = new ESP.Compatible.Employee(operation.AuditorUserID.Value);
                        addCell1.InnerHtml = serNum.ToString();
                        addCell2.InnerHtml = EMP.Name;
                        addCell3.InnerHtml = EMP.PositionDescription;
                        addCell4.InnerHtml = "&nbsp;";

                        if (operation.AuditType.Value == ESP.Finance.Utility.auditorType.operationAudit_Type_YS)
                        {
                            if (model.ReturnType == (int)ESP.Purchase.Common.PRTYpe.PR_MediaFA || model.ReturnType == (int)ESP.Purchase.Common.PRTYpe.PR_PriFA || model.ReturnType == (int)ESP.Purchase.Common.PRTYpe.MediaPR)
                            {
                                isHaveYS = true;
                            }
                            else
                            {
                                string trId = addRow.Attributes["id"] = "tr_" + EMP.SysID + "_YS";
                                addCell5.InnerHtml = ESP.Finance.Utility.auditorType.operationAudit_Type_Names[ESP.Finance.Utility.auditorType.operationAudit_Type_YS];
                                addCell6.InnerHtml = "<a href=\"\" onclick=\"openEmployee('YS');return false;\">添加</a>&nbsp;<a href=\"\" onclick=\"openEmployee1('YS','" + trId + "','" + EMP.SysID + "');return false;\">更改</a>&nbsp;<a href=\"\" onclick=\"removeRow('YS','" + EMP.SysID + "','" + trId + "');return false;\">删除</a>";

                                hidYS.Value += EMP.SysID + ",";
                            }
                        }
                        else if (operation.AuditType.Value == ESP.Finance.Utility.auditorType.operationAudit_Type_ZJSP)
                        {
                            addCell4.InnerHtml = "10万以下";
                            string trId = addRow.Attributes["id"] = "tr_" + EMP.SysID + "_ZJSP";
                            addCell5.InnerHtml = ESP.Finance.Utility.auditorType.operationAudit_Type_Names[ESP.Finance.Utility.auditorType.operationAudit_Type_ZJSP];
                            addCell6.InnerHtml = "<a href=\"\" onclick=\"openEmployee('ZJSP');return false;\">添加</a>&nbsp;<a href=\"\" onclick=\"openEmployee1('ZJSP','" + trId + "','" + EMP.SysID + "');return false;\">更改</a>&nbsp;<a href=\"\" onclick=\"removeRow('ZJSP','" + EMP.SysID + "','" + trId + "');return false;\">删除</a>";
                            hidZJSP.Value += EMP.SysID + ",";
                            isHaveZJ = true;
                        }
                        else if (operation.AuditType.Value == ESP.Finance.Utility.auditorType.operationAudit_Type_ZJZH)
                        {
                            addCell4.InnerHtml = "10万以下";
                            string trId = addRow.Attributes["id"] = "tr_" + EMP.SysID + "_ZJZH";
                            addCell5.InnerHtml = ESP.Finance.Utility.auditorType.operationAudit_Type_Names[ESP.Finance.Utility.auditorType.operationAudit_Type_ZJZH];
                            addCell6.InnerHtml = "<a href=\"\" onclick=\"openEmployee('ZJZH');return false;\">添加</a>&nbsp;<a href=\"\" onclick=\"openEmployee1('ZJZH','" + trId + "','" + EMP.SysID + "');return false;\">更改</a>&nbsp;<a href=\"\" onclick=\"removeRow('ZJZH','" + EMP.SysID + "','" + trId + "');return false;\">删除</a>";
                            hidZJZH.Value += EMP.SysID + ",";
                        }
                        else if (operation.AuditType.Value == ESP.Finance.Utility.auditorType.operationAudit_Type_ZJFJ)
                        {
                            string trId = addRow.Attributes["id"] = "tr_" + EMP.SysID + "_ZJFJ";
                            addCell5.InnerHtml = ESP.Finance.Utility.auditorType.operationAudit_Type_Names[ESP.Finance.Utility.auditorType.operationAudit_Type_ZJFJ];
                            addCell6.InnerHtml = "<a href=\"\" onclick=\"openEmployee('ZJFJ');return false;\">添加</a>&nbsp;<a href=\"\" onclick=\"openEmployee1('ZJFJ','" + trId + "','" + EMP.SysID + "');return false;\">更改</a>&nbsp;<a href=\"\" onclick=\"removeRow('ZJFJ','" + EMP.SysID + "','" + trId + "');return false;\">删除</a>";
                            hidZJFJ.Value += EMP.SysID + ",";
                        }
                        else if (operation.AuditType.Value == ESP.Finance.Utility.auditorType.operationAudit_Type_ZJLSP)
                        {
                            addCell4.InnerHtml = "10万以上";
                            string trId = addRow.Attributes["id"] = "tr_" + EMP.SysID + "_ZJLSP";
                            addCell5.InnerHtml = ESP.Finance.Utility.auditorType.operationAudit_Type_Names[ESP.Finance.Utility.auditorType.operationAudit_Type_ZJLSP];
                            addCell6.InnerHtml = "<a href=\"\" onclick=\"openEmployee('ZJLSP');return false;\">添加</a>&nbsp;<a href=\"\" onclick=\"openEmployee1('ZJLSP','" + trId + "','" + EMP.SysID + "');return false;\">更改</a>&nbsp;<a href=\"\" onclick=\"removeRow('ZJLSP','" + EMP.SysID + "','" + trId + "');return false;\">删除</a>";
                            hidZJLSP.Value += EMP.SysID + ",";
                            isHaveZJL = true;
                        }
                        else if (operation.AuditType.Value == ESP.Finance.Utility.auditorType.operationAudit_Type_ZJLZH)
                        {
                            addCell4.InnerHtml = "10万以上";
                            string trId = addRow.Attributes["id"] = "tr_" + EMP.SysID + "_ZJLZH";
                            addCell5.InnerHtml = ESP.Finance.Utility.auditorType.operationAudit_Type_Names[ESP.Finance.Utility.auditorType.operationAudit_Type_ZJLZH];
                            addCell6.InnerHtml = "<a href=\"\" onclick=\"openEmployee('ZJLZH');return false;\">添加</a>&nbsp;<a href=\"\" onclick=\"openEmployee1('ZJLZH','" + trId + "','" + EMP.SysID + "');return false;\">更改</a>&nbsp;<a href=\"\" onclick=\"removeRow('ZJLZH','" + EMP.SysID + "','" + trId + "');return false;\">删除</a>";
                            hidZJLZH.Value += EMP.SysID + ",";
                        }
                        else if (operation.AuditType.Value == ESP.Finance.Utility.auditorType.operationAudit_Type_ZJLFJ)
                        {
                            string trId = addRow.Attributes["id"] = "tr_" + EMP.SysID + "_ZJLFJ";
                            addCell5.InnerHtml = ESP.Finance.Utility.auditorType.operationAudit_Type_Names[ESP.Finance.Utility.auditorType.operationAudit_Type_ZJLFJ];
                            addCell6.InnerHtml = "<a href=\"\" onclick=\"openEmployee('ZJLFJ');return false;\">添加</a>&nbsp;<a href=\"\" onclick=\"openEmployee1('ZJLFJ','" + trId + "','" + EMP.SysID + "');return false;\">更改</a>&nbsp;<a href=\"\" onclick=\"removeRow('ZJLFJ','" + EMP.SysID + "','" + trId + "');return false;\">删除</a>";
                            hidZJLFJ.Value += EMP.SysID + ",";
                        }
                        else if (operation.AuditType.Value == ESP.Finance.Utility.auditorType.operationAudit_Type_CEO)
                        {
                            addCell4.InnerHtml = "10万以上";
                            string trId = addRow.Attributes["id"] = "tr_" + EMP.SysID + "_CEO";
                            addCell5.InnerHtml = ESP.Finance.Utility.auditorType.operationAudit_Type_Names[ESP.Finance.Utility.auditorType.operationAudit_Type_CEO];
                            addCell6.InnerHtml = "<a href=\"\" onclick=\"openEmployee('CEO');return false;\">添加</a>&nbsp;<a href=\"\" onclick=\"openEmployee1('CEO','" + trId + "','" + EMP.SysID + "');return false;\">更改</a>&nbsp;<a href=\"\" onclick=\"removeRow('CEO','" + EMP.SysID + "','" + trId + "');return false;\">删除</a>";
                            hidCEO.Value += EMP.SysID + ",";
                            isHaveCEO = true;
                        }
                        else if (operation.AuditType.Value == ESP.Finance.Utility.auditorType.operationAudit_Type_FA)
                        {
                            addCell4.InnerHtml = "FA";
                            string trId = addRow.Attributes["id"] = "tr_" + EMP.SysID + "_FASH";
                            addCell5.InnerHtml = ESP.Finance.Utility.auditorType.operationAudit_Type_Names[ESP.Finance.Utility.auditorType.operationAudit_Type_FA];
                            addCell6.InnerHtml = "<a href=\"\" onclick=\"openEmployee('FA');return false;\">添加</a>&nbsp;<a href=\"\" onclick=\"openEmployee1('FA','" + trId + "','" + EMP.SysID + "');return false;\">更改</a>&nbsp;<a href=\"\" onclick=\"removeRow('FA','" + EMP.SysID + "','" + trId + "');return false;\">删除</a>";
                            hidFA.Value += EMP.SysID + ",";
                            isHaveFA = true;
                        }
                        addRow.Cells.Add(addCell1);
                        addRow.Cells.Add(addCell2);
                        addRow.Cells.Add(addCell3);
                        addRow.Cells.Add(addCell4);
                        addRow.Cells.Add(addCell5);
                        addRow.Cells.Add(addCell6);
                        addRow.Attributes["class"] = "td";
                        tab.Rows.Add(addRow);
                        serNum++;
                    }
                }
            }
           // int[] depts = new ESP.Compatible.Employee(model.RequestorID.Value).GetDepartmentIDs();
            //如果是媒体3000以上或对私3000以上的单子，找原始部门
            if (model.ReturnType == (int)ESP.Purchase.Common.PRTYpe.PR_MediaFA || model.ReturnType == (int)ESP.Purchase.Common.PRTYpe.PR_PriFA)
            {
                if (model != null && model.PRID != null && model.PRID.Value != 0)
                {
                    ESP.Purchase.Entity.MediaPREditHisInfo relationModel = ESP.Purchase.BusinessLogic.MediaPREditHisManager.GetModelByNewPRID(model.PRID.Value);
                    ESP.Purchase.Entity.GeneralInfo generalModel = ESP.Purchase.BusinessLogic.GeneralInfoManager.GetModel(relationModel.OldPRId.Value);
                    //depts = new ESP.Compatible.Employee(generalModel.requestor).GetDepartmentIDs();
                }
            }
            //根据部门获取部门总经理
            //ESP.Framework.Entity.OperationAuditManageInfo manageModel = ESP.Framework.BusinessLogic.OperationAuditManageManager.GetModelByUserId(model.RequestorID.Value);
            ESP.Framework.Entity.OperationAuditManageInfo manageModel = null;

            if (model.ProjectID!=null && model.ProjectID.Value != 0)
            {
                manageModel = ESP.Framework.BusinessLogic.OperationAuditManageManager.GetModelByProjectId(model.ProjectID.Value);
            }
            if (manageModel == null)
                manageModel = ESP.Framework.BusinessLogic.OperationAuditManageManager.GetModelByUserId(model.RequestorID.Value); ;

            if (manageModel == null)
                manageModel = ESP.Framework.BusinessLogic.OperationAuditManageManager.GetModelByDepId(model.DepartmentID.Value);

            //默认项目负责人审核
            if (isHaveYS == false && projectModel != null && model.RequestorID.Value != (supportModel == null ? projectModel.ApplicantUserID : supportModel.LeaderUserID))
            {
                if (model.ReturnType == (int)ESP.Purchase.Common.PRTYpe.PR_MediaFA || model.ReturnType == (int)ESP.Purchase.Common.PRTYpe.PR_PriFA)
                { }
                else
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

                    YSCell1.InnerHtml = "1";
                    YSCell2.InnerHtml = supportModel == null ? projectModel.ApplicantEmployeeName : supportModel.LeaderEmployeeName;
                    YSCell3.InnerHtml = "项目负责人";
                    YSCell4.InnerHtml = "项目负责人";
                    int responserid = supportModel == null ? projectModel.ApplicantUserID : supportModel.LeaderUserID.Value;
                    string trId = YSRow.Attributes["id"] = "tr_" + responserid.ToString() + "_YSSP";
                    YSCell5.InnerHtml = "项目负责人审批";
                    YSCell6.InnerHtml = "<a href=\"\" onclick=\"openEmployee('YS');return false;\">添加</a>&nbsp;<a href=\"\" onclick=\"openEmployee1('YS','" + trId + "','" + responserid.ToString() + "');return false;\">更改</a>&nbsp;<a href=\"\" onclick=\"removeRow('YS','" + responserid.ToString() + "','" + trId + "');return false;\">删除</a>";

                    hidYS.Value += (supportModel == null ? projectModel.ApplicantUserID.ToString() : supportModel.LeaderUserID.ToString()) + ",";
                    YSRow.Cells.Add(YSCell1);
                    YSRow.Cells.Add(YSCell2);
                    YSRow.Cells.Add(YSCell3);
                    YSRow.Cells.Add(YSCell4);
                    YSRow.Cells.Add(YSCell5);
                    YSRow.Cells.Add(YSCell6);
                    YSRow.Attributes["class"] = "td";
                    tab.Rows.Add(YSRow);
                }
            }
            if (!isHaveZJ && (model.ReturnType != (int)ESP.Purchase.Common.PRTYpe.PR_MediaFA && model.ReturnType != (int)ESP.Purchase.Common.PRTYpe.PR_PriFA && model.ReturnType != (int)ESP.Purchase.Common.PRTYpe.MediaPR && model.ReturnType != (int)ESP.Purchase.Common.PRTYpe.PrivatePR))
            {
                if (manageModel != null)
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

                    ZJCell1.InnerHtml = "1";
                    ZJCell2.InnerHtml = manageModel.DirectorName;
                    ZJCell3.InnerHtml = new ESP.Compatible.Employee(manageModel.DirectorId).PositionDescription;
                    ZJCell4.InnerHtml = "10万以下";
                    string trId = ZJRow.Attributes["id"] = "tr_" + manageModel.DirectorId + "_ZJSP";
                    ZJCell5.InnerHtml = ESP.Finance.Utility.auditorType.operationAudit_Type_Names[ESP.Finance.Utility.auditorType.operationAudit_Type_ZJSP];
                    ZJCell6.InnerHtml = "<a href=\"\" onclick=\"openEmployee('ZJSP');return false;\">添加</a>&nbsp;<a href=\"\" onclick=\"openEmployee1('ZJSP','" + trId + "','" + manageModel.DirectorId + "');return false;\">更改</a>&nbsp;<a href=\"\" onclick=\"removeRow('ZJSP','" + manageModel.DirectorId + "','" + trId + "');return false;\">删除</a>";

                    hidZJSP.Value = manageModel.DirectorId.ToString();
                    ZJRow.Cells.Add(ZJCell1);
                    ZJRow.Cells.Add(ZJCell2);
                    ZJRow.Cells.Add(ZJCell3);
                    ZJRow.Cells.Add(ZJCell4);
                    ZJRow.Cells.Add(ZJCell5);
                    ZJRow.Cells.Add(ZJCell6);
                    ZJRow.Attributes["class"] = "td";
                    tab.Rows.Add(ZJRow);
                }
            }
            if ((model.PreFee > 100000 || (model.ReturnType == (int)ESP.Purchase.Common.PRTYpe.PR_MediaFA || model.ReturnType == (int)ESP.Purchase.Common.PRTYpe.PR_PriFA || model.ReturnType == (int)ESP.Purchase.Common.PRTYpe.MediaPR)) && !isHaveZJL)
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

                    ZJLCell1.InnerHtml = "2";
                    ZJLCell2.InnerHtml = manageModel.ManagerName;
                    ZJLCell3.InnerHtml = new ESP.Compatible.Employee(manageModel.ManagerId).PositionDescription;
                    ZJLCell4.InnerHtml = "10万以上";
                    string trId1 = ZJLRow.Attributes["id"] = "tr_" + manageModel.ManagerId + "_ZJLSP";
                    ZJLCell5.InnerHtml = ESP.Finance.Utility.auditorType.operationAudit_Type_Names[ESP.Finance.Utility.auditorType.operationAudit_Type_ZJLSP];
                    ZJLCell6.InnerHtml = "<a href=\"\" onclick=\"openEmployee('ZJLSP');return false;\">添加</a>&nbsp;<a href=\"\" onclick=\"openEmployee1('ZJLSP','" + trId1 + "','" + manageModel.ManagerId + "');return false;\">更改</a>&nbsp;<a href=\"\" onclick=\"removeRow('ZJLSP','" + manageModel.ManagerId + "','" + trId1 + "');return false;\">删除</a>";

                    hidZJLSP.Value = manageModel.ManagerId.ToString();
                    ZJLRow.Cells.Add(ZJLCell1);
                    ZJLRow.Cells.Add(ZJLCell2);
                    ZJLRow.Cells.Add(ZJLCell3);
                    ZJLRow.Cells.Add(ZJLCell4);
                    ZJLRow.Cells.Add(ZJLCell5);
                    ZJLRow.Cells.Add(ZJLCell6);
                    ZJLRow.Attributes["class"] = "td";
                    tab.Rows.Add(ZJLRow);
                }
            }
            //if (model.PreFee > 100000 && !isHaveCEO)
            //{
            //    if (manageModel != null)
            //    {
            //        System.Web.UI.HtmlControls.HtmlTableRow CEORow = new System.Web.UI.HtmlControls.HtmlTableRow();
            //        System.Web.UI.HtmlControls.HtmlTableCell CEOCell1 = new System.Web.UI.HtmlControls.HtmlTableCell();
            //        System.Web.UI.HtmlControls.HtmlTableCell CEOCell2 = new System.Web.UI.HtmlControls.HtmlTableCell();
            //        System.Web.UI.HtmlControls.HtmlTableCell CEOCell3 = new System.Web.UI.HtmlControls.HtmlTableCell();
            //        System.Web.UI.HtmlControls.HtmlTableCell CEOCell4 = new System.Web.UI.HtmlControls.HtmlTableCell();
            //        System.Web.UI.HtmlControls.HtmlTableCell CEOCell5 = new System.Web.UI.HtmlControls.HtmlTableCell();
            //        System.Web.UI.HtmlControls.HtmlTableCell CEOCell6 = new System.Web.UI.HtmlControls.HtmlTableCell();
            //        CEOCell1.Align = "Center";
            //        CEOCell2.Align = "Center";
            //        CEOCell3.Align = "Center";
            //        CEOCell4.Align = "Center";
            //        CEOCell5.Align = "Center";
            //        CEOCell6.Align = "Center";

            //        CEOCell1.InnerHtml = "3";
            //        CEOCell2.InnerHtml = manageModel.CEOName;
            //        string trId1 = CEORow.Attributes["id"] = "tr_" + manageModel.ManagerId + "_CEO";
            //        CEOCell3.InnerHtml = new ESP.Compatible.Employee(manageModel.CEOId).PositionDescription;
            //        CEOCell4.InnerHtml = "10万以上";
            //        CEOCell5.InnerHtml = ESP.Finance.Utility.auditorType.operationAudit_Type_Names[ESP.Finance.Utility.auditorType.operationAudit_Type_CEO];
            //        CEOCell6.InnerHtml = "<a href=\"\" onclick=\"openEmployee('CEO');return false;\">添加</a>&nbsp;<a href=\"\" onclick=\"openEmployee1('CEO','" + trId1 + "','" + manageModel.ManagerId + "');return false;\">更改</a>&nbsp;<a href=\"\" onclick=\"removeRow('CEO','" + manageModel.ManagerId + "','" + trId1 + "');return false;\">删除</a>";


            //        CEORow.Attributes["id"] = "tr_" + manageModel.CEOId + "_CEO";

            //        hidCEO.Value = manageModel.CEOId.ToString();
            //        CEORow.Cells.Add(CEOCell1);
            //        CEORow.Cells.Add(CEOCell2);
            //        CEORow.Cells.Add(CEOCell3);
            //        CEORow.Cells.Add(CEOCell4);
            //        CEORow.Cells.Add(CEOCell5);
            //        CEORow.Cells.Add(CEOCell6);
            //        CEORow.Attributes["class"] = "td";
            //        tab.Rows.Add(CEORow);
            //    }
            //}
            if (!isHaveFA && (model.ReturnType != (int)ESP.Purchase.Common.PRTYpe.PR_MediaFA && model.ReturnType != (int)ESP.Purchase.Common.PRTYpe.PR_PriFA && model.ReturnType != (int)ESP.Purchase.Common.PRTYpe.MediaPR && model.ReturnType != (int)ESP.Purchase.Common.PRTYpe.PrivatePR) && (model.ProjectCode.IndexOf("GM*") < 0 && model.ProjectCode.IndexOf("*GM") < 0))
            {
                if (manageModel != null)
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

                    string num = "2";
                    if (model.PreFee <= 100000)
                        num = "2";
                    else
                        num = "3";

                    FACell1.InnerHtml = num;
                    FACell2.InnerHtml = manageModel.FAName;
                    string trId1 = FARow.Attributes["id"] = "tr_" + manageModel.FAId.ToString() + "_FASP";
                    FACell3.InnerHtml = new ESP.Compatible.Employee(manageModel.FAId).PositionDescription;
                    FACell4.InnerHtml = "&nbsp;";
                    FACell5.InnerHtml = ESP.Finance.Utility.auditorType.operationAudit_Type_Names[ESP.Finance.Utility.auditorType.operationAudit_Type_FA];
                    FACell6.InnerHtml = "<a href=\"\" onclick=\"openEmployee('FASP');return false;\">添加</a>&nbsp;<a href=\"\" onclick=\"openEmployee1('FASP','" + trId1 + "','" + manageModel.FAId.ToString() + "');return false;\">更改</a>&nbsp;<a href=\"\" onclick=\"removeRow('FASP','" + manageModel.FAId.ToString() + "','" + trId1 + "');return false;\">删除</a>";

                    FARow.Attributes["id"] = "tr_" + manageModel.FAId.ToString() + "_FASP";

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

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            this.hidCEO.Value = string.Empty;
            this.hidFA.Value = string.Empty;
            this.hidYS.Value = string.Empty;
            this.hidZJFJ.Value = string.Empty;
            this.hidZJLFJ.Value = string.Empty;
            this.hidZJLSP.Value = string.Empty;
            this.hidZJLZH.Value = string.Empty;
            this.hidZJSP.Value = string.Empty;
            this.hidZJZH.Value = string.Empty;

            int OrderType = Convert.ToInt32(ddlType.SelectedValue);
            int OrderID = Convert.ToInt32(txtID.Text);
            if (OrderType == 0)
            {
                ESP.Purchase.Entity.GeneralInfo generalModel = ESP.Purchase.BusinessLogic.GeneralInfoManager.GetModel(OrderID);
                if (generalModel == null)
                    return;
                CurrentUserID = generalModel.requestor;
                CurrentOrderID = generalModel.id;
                ESP.Compatible.Employee emp = new ESP.Compatible.Employee(CurrentUserID);
                DeptID = emp.GetDepartmentIDs()[0];
                ViewContorlPR(generalModel);
            }
            else if (OrderType == 1)
            {
                ESP.Finance.Entity.ReturnInfo returnModel = ESP.Finance.BusinessLogic.ReturnManager.GetModel(OrderID);
                if (returnModel == null)
                    return;
                CurrentUserID = returnModel.RequestorID.Value;
                CurrentOrderID = returnModel.ReturnID;
                ESP.Compatible.Employee emp = new ESP.Compatible.Employee(CurrentUserID);
                DeptID = emp.GetDepartmentIDs()[0];
                ViewContorlPN(returnModel);

            }
        }

        protected void btnCommit_Click(object sender, EventArgs e)
        {
            int OrderType = Convert.ToInt32(ddlType.SelectedValue);
            int OrderID = Convert.ToInt32(txtID.Text);
            if (OrderType == 0)
            {
                ESP.Purchase.Entity.GeneralInfo generalModel = ESP.Purchase.BusinessLogic.GeneralInfoManager.GetModel(OrderID);
                CurrentUserID = generalModel.requestor;
                CurrentOrderID = generalModel.id;
                ESP.Compatible.Employee emp = new ESP.Compatible.Employee(CurrentUserID);
                DeptID = emp.GetDepartmentIDs()[0];
                CommitPRAuditor(generalModel);
            }
            else if (OrderType == 1)
            {
                ESP.Finance.Entity.ReturnInfo returnModel = ESP.Finance.BusinessLogic.ReturnManager.GetModel(OrderID);
                CurrentUserID = returnModel.RequestorID.Value;
                CurrentOrderID = returnModel.ReturnID;
                ESP.Compatible.Employee emp = new ESP.Compatible.Employee(CurrentUserID);
                DeptID = emp.GetDepartmentIDs()[0];
                CommitPNAuditor(returnModel);

            }

            this.hidCEO.Value = string.Empty;
            this.hidFA.Value = string.Empty;
            this.hidYS.Value = string.Empty;
            this.hidZJFJ.Value = string.Empty;
            this.hidZJLFJ.Value = string.Empty;
            this.hidZJLSP.Value = string.Empty;
            this.hidZJLZH.Value = string.Empty;
            this.hidZJSP.Value = string.Empty;
            this.hidZJZH.Value = string.Empty;

        }
        private bool SavePRAuditor(GeneralInfo GeneralModel)
        {
            List<OperationAuditInfo> operationList = new List<OperationAuditInfo>();
            if (hidYS.Value != "" && hidYS.Value.TrimEnd(',') != "")
            {
                string[] YSIds = hidYS.Value.TrimEnd(',').Split(',');
                for (int i = 0; i < YSIds.Length; i++)
                {
                    if (YSIds[i].Trim() != "")
                    {
                        OperationAuditInfo model = new OperationAuditInfo();
                        model.general_id = GeneralModel.id;
                        model.aduitType = ESP.Purchase.Common.State.operationAudit_Type_YS;
                        model.auditorId = int.Parse(YSIds[i]);
                        operationList.Add(model);
                    }
                }
            }
            if (hidZJSP.Value != "" && hidZJSP.Value.TrimEnd(',') != "")
            {
                OperationAuditInfo model = new OperationAuditInfo();
                model.general_id = GeneralModel.id;
                model.aduitType = ESP.Purchase.Common.State.operationAudit_Type_ZJSP;
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
                        model.general_id = GeneralModel.id;
                        model.aduitType = ESP.Purchase.Common.State.operationAudit_Type_ZJZH;
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
                        model.general_id = GeneralModel.id;
                        model.aduitType = ESP.Purchase.Common.State.operationAudit_Type_ZJFJ;
                        model.auditorId = int.Parse(ZJFJIds[i]);
                        operationList.Add(model);
                    }
                }
            }
            if (hidZJLSP.Value != "" && hidZJLSP.Value.TrimEnd(',') != "")
            {
                OperationAuditInfo model = new OperationAuditInfo();
                model.general_id = GeneralModel.id;
                model.aduitType = ESP.Purchase.Common.State.operationAudit_Type_ZJLSP;
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
                        model.general_id = GeneralModel.id;
                        model.aduitType = ESP.Purchase.Common.State.operationAudit_Type_ZJLZH;
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
                        model.general_id = GeneralModel.id;
                        model.aduitType = ESP.Purchase.Common.State.operationAudit_Type_ZJLFJ;
                        model.auditorId = int.Parse(ZJLFJIds[i]);
                        operationList.Add(model);
                    }
                }
            }
            if (hidCEO.Value != "" && hidCEO.Value.TrimEnd(',') != "")
            {
                OperationAuditInfo model = new OperationAuditInfo();
                model.general_id = GeneralModel.id;
                model.aduitType = ESP.Purchase.Common.State.operationAudit_Type_CEO;
                model.auditorId = int.Parse(hidCEO.Value.TrimEnd(','));
                operationList.Add(model);
            }

            return OperationAuditManager.Add(operationList, GeneralModel.id);
        }

        private bool SavePNAuditor(ReturnInfo returnModel)
        {
            List<ESP.Finance.Entity.ReturnAuditHistInfo> operationList = new List<ESP.Finance.Entity.ReturnAuditHistInfo>();
            if (hidYS.Value != "" && hidYS.Value.TrimEnd(',') != "")
            {
                string[] YSIds = hidYS.Value.TrimEnd(',').Split(',');
                for (int i = 0; i < YSIds.Length; i++)
                {
                    if (YSIds[i].Trim() != "")
                    {
                        if (hidZJSP.Value != "" && hidZJSP.Value.TrimEnd(',') != "")
                        {
                            if (YSIds[i].Trim() == hidZJSP.Value.TrimEnd(','))
                            { continue; }
                        }
                        ESP.Compatible.Employee emp = new ESP.Compatible.Employee(Convert.ToInt32(YSIds[i]));
                        ESP.Finance.Entity.ReturnAuditHistInfo model = new ESP.Finance.Entity.ReturnAuditHistInfo();
                        model.ReturnID = returnModel.ReturnID;
                        model.AuditType = ESP.Finance.Utility.auditorType.operationAudit_Type_YS;
                        model.AuditorUserID = int.Parse(emp.SysID);
                        model.AuditorUserCode = emp.ID;
                        model.AuditorEmployeeName = emp.Name;
                        model.AuditorUserName = emp.ITCode;
                        model.AuditeStatus = (int)ESP.Finance.Utility.AuditHistoryStatus.UnAuditing;
                        operationList.Add(model);
                    }
                }
            }
            if (hidZJSP.Value != "" && hidZJSP.Value.TrimEnd(',') != "")
            {
                ESP.Compatible.Employee emp = new ESP.Compatible.Employee(Convert.ToInt32(hidZJSP.Value.TrimEnd(',')));
                ESP.Finance.Entity.ReturnAuditHistInfo model = new ESP.Finance.Entity.ReturnAuditHistInfo();
                model.ReturnID = returnModel.ReturnID;
                model.AuditType = ESP.Finance.Utility.auditorType.operationAudit_Type_ZJSP;
                model.AuditorUserID = int.Parse(emp.SysID);
                model.AuditorUserCode = emp.ID;
                model.AuditorEmployeeName = emp.Name;
                model.AuditorUserName = emp.ITCode;
                model.AuditeStatus = (int)ESP.Finance.Utility.AuditHistoryStatus.UnAuditing;
                operationList.Add(model);
            }

            if (hidZJFJ.Value != "" && hidZJFJ.Value.TrimEnd(',') != "")
            {
                string[] ZJFJIds = hidZJFJ.Value.TrimEnd(',').Split(',');
                for (int i = 0; i < ZJFJIds.Length; i++)
                {
                    if (ZJFJIds[i].Trim() != "")
                    {
                        ESP.Compatible.Employee emp = new ESP.Compatible.Employee(Convert.ToInt32(ZJFJIds[i]));
                        ESP.Finance.Entity.ReturnAuditHistInfo model = new ESP.Finance.Entity.ReturnAuditHistInfo();
                        model.ReturnID = returnModel.ReturnID;
                        model.AuditType = ESP.Finance.Utility.auditorType.operationAudit_Type_ZJFJ;
                        model.AuditorUserID = int.Parse(emp.SysID);
                        model.AuditorUserCode = emp.ID;
                        model.AuditorEmployeeName = emp.Name;
                        model.AuditorUserName = emp.ITCode;
                        model.AuditeStatus = (int)ESP.Finance.Utility.AuditHistoryStatus.UnAuditing;
                        operationList.Add(model);
                    }
                }
            }
            if (hidZJLSP.Value != "" && hidZJLSP.Value.TrimEnd(',') != "" && hidZJLSP.Value.TrimEnd(',') != hidZJSP.Value.TrimEnd(','))
            {
                ESP.Compatible.Employee emp = new ESP.Compatible.Employee(Convert.ToInt32(hidZJLSP.Value.TrimEnd(',')));
                ESP.Finance.Entity.ReturnAuditHistInfo model = new ESP.Finance.Entity.ReturnAuditHistInfo();
                model.ReturnID = returnModel.ReturnID;
                model.AuditType = ESP.Finance.Utility.auditorType.operationAudit_Type_ZJLSP;
                model.AuditorUserID = int.Parse(emp.SysID);
                model.AuditorUserCode = emp.ID;
                model.AuditorEmployeeName = emp.Name;
                model.AuditorUserName = emp.ITCode;
                model.AuditeStatus = (int)ESP.Finance.Utility.AuditHistoryStatus.UnAuditing;
                operationList.Add(model);
            }

            if (hidZJLFJ.Value != "" && hidZJLFJ.Value.TrimEnd(',') != "")
            {
                string[] ZJLFJIds = hidZJLFJ.Value.TrimEnd(',').Split(',');
                for (int i = 0; i < ZJLFJIds.Length; i++)
                {
                    if (ZJLFJIds[i].Trim() != "")
                    {
                        ESP.Compatible.Employee emp = new ESP.Compatible.Employee(Convert.ToInt32(ZJLFJIds[i]));
                        ESP.Finance.Entity.ReturnAuditHistInfo model = new ESP.Finance.Entity.ReturnAuditHistInfo();
                        model.ReturnID = returnModel.ReturnID;
                        model.AuditType = ESP.Finance.Utility.auditorType.operationAudit_Type_ZJLFJ;
                        model.AuditorUserID = int.Parse(emp.SysID);
                        model.AuditorUserCode = emp.ID;
                        model.AuditorEmployeeName = emp.Name;
                        model.AuditorUserName = emp.ITCode;
                        model.AuditeStatus = (int)ESP.Finance.Utility.AuditHistoryStatus.UnAuditing;
                        operationList.Add(model);
                    }
                }
            }
            if (hidCEO.Value != "" && hidCEO.Value.TrimEnd(',') != "" && hidCEO.Value.TrimEnd(',') != hidZJLSP.Value.TrimEnd(',') && hidCEO.Value.TrimEnd(',') != hidZJSP.Value.TrimEnd(','))
            {
                ESP.Compatible.Employee emp = new ESP.Compatible.Employee(Convert.ToInt32(hidCEO.Value.TrimEnd(',')));
                ESP.Finance.Entity.ReturnAuditHistInfo model = new ESP.Finance.Entity.ReturnAuditHistInfo();
                model.ReturnID = returnModel.ReturnID;
                model.AuditType = ESP.Finance.Utility.auditorType.operationAudit_Type_CEO;
                model.AuditorUserID = int.Parse(emp.SysID);
                model.AuditorUserCode = emp.ID;
                model.AuditorEmployeeName = emp.Name;
                model.AuditorUserName = emp.ITCode;
                model.AuditeStatus = (int)ESP.Finance.Utility.AuditHistoryStatus.UnAuditing;
                operationList.Add(model);
            }
            if (!returnModel.NeedPurchaseAudit)
            {
                if (hidFA.Value != "" && hidFA.Value.TrimEnd(',') != "")
                {
                    ESP.Compatible.Employee emp = new ESP.Compatible.Employee(Convert.ToInt32(hidFA.Value.TrimEnd(',')));
                    ESP.Finance.Entity.ReturnAuditHistInfo model = new ESP.Finance.Entity.ReturnAuditHistInfo();
                    model.ReturnID = returnModel.ReturnID;
                    model.AuditType = ESP.Finance.Utility.auditorType.operationAudit_Type_FA;
                    model.AuditorUserID = int.Parse(emp.SysID);
                    model.AuditorUserCode = emp.ID;
                    model.AuditorEmployeeName = emp.Name;
                    model.AuditorUserName = emp.ITCode;
                    model.AuditeStatus = (int)ESP.Finance.Utility.AuditHistoryStatus.UnAuditing;
                    operationList.Add(model);
                }

                //finace auditor
                ESP.Compatible.Employee financeEmp = new ESP.Compatible.Employee(ESP.Finance.BusinessLogic.BranchManager.GetModelByCode(returnModel.ProjectCode.Substring(0, 1)).FirstFinanceID);
                ESP.Finance.Entity.ReturnAuditHistInfo FinanceModel = new ESP.Finance.Entity.ReturnAuditHistInfo();
                FinanceModel.ReturnID = returnModel.ReturnID;
                FinanceModel.AuditType = ESP.Finance.Utility.auditorType.operationAudit_Type_Financial;
                FinanceModel.AuditorUserID = int.Parse(financeEmp.SysID);
                FinanceModel.AuditorUserCode = financeEmp.ID;
                FinanceModel.AuditorEmployeeName = financeEmp.Name;
                FinanceModel.AuditorUserName = financeEmp.ITCode;
                FinanceModel.AuditeStatus = (int)ESP.Finance.Utility.AuditHistoryStatus.UnAuditing;
                operationList.Add(FinanceModel);
            }
            else
            {
                //采购物料审核人
                ESP.Purchase.Entity.GeneralInfo generalModel = ESP.Purchase.BusinessLogic.GeneralInfoManager.GetModel(returnModel.PRID.Value);
                ESP.Compatible.Employee AuditEmp = new ESP.Compatible.Employee(getAuditor(generalModel));
                ESP.Finance.Entity.ReturnAuditHistInfo AuditModel = new ESP.Finance.Entity.ReturnAuditHistInfo();
                AuditModel.ReturnID = returnModel.ReturnID;
                AuditModel.AuditType = ESP.Finance.Utility.auditorType.purchase_first;
                AuditModel.AuditorUserID = int.Parse(AuditEmp.SysID);
                AuditModel.AuditorUserCode = AuditEmp.ID;
                AuditModel.AuditorEmployeeName = AuditEmp.Name;
                AuditModel.AuditorUserName = AuditEmp.ITCode;
                AuditModel.AuditeStatus = (int)ESP.Finance.Utility.AuditHistoryStatus.UnAuditing;
                operationList.Add(AuditModel);
            }
            return (ESP.Finance.BusinessLogic.ReturnAuditHistManager.Add(returnModel, operationList) > 0);
        }

        private int getAuditor(ESP.Purchase.Entity.GeneralInfo generalModel)
        {
            List<ESP.Purchase.Entity.OrderInfo> orderList = ESP.Purchase.BusinessLogic.OrderInfoManager.GetListByGeneralId(generalModel.id);
            ESP.Purchase.Entity.SupplierInfo supplierModel = null;
            ESP.Purchase.Entity.TypeInfo typeInfo = null;
            foreach (ESP.Purchase.Entity.OrderInfo orderModel in orderList)
            {
                if (orderModel.supplierId > 0)
                {
                    supplierModel = ESP.Purchase.BusinessLogic.SupplierManager.GetModel(orderModel.supplierId);
                    typeInfo = ESP.Purchase.BusinessLogic.TypeManager.GetModel(orderModel.producttype);
                }
            }
            if (supplierModel != null)
            {
                if (supplierModel.supplier_area.IndexOf("北京") != -1)
                {
                    return typeInfo.BJPaymentUserID;
                }
                else if (supplierModel.supplier_area.IndexOf("广州") != -1)
                {
                    return typeInfo.GZPaymentUserID;
                }
                else if (supplierModel.supplier_area.IndexOf("上海") != -1)
                {
                    return typeInfo.SHPaymentUserID;
                }
            }
            return 0;
        }

        private void CommitPRAuditor(GeneralInfo generalInfo)
        {
            string Msg = "";
            if (generalInfo.status != ESP.Purchase.Common.State.requisition_save && generalInfo.status != ESP.Purchase.Common.State.requisition_return)
            {
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('此条数据已经提交，不能重复提交！');window.location.href='/Purchase/Default.aspx';", true);
                return;
            }
            Msg = ESP.ITIL.BusinessLogic.申请单业务检查.申请单提交(generalInfo);
            if (Msg != "")
            {
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('" + Msg + "');", true);
                return;
            }
            try
            {
                if (SavePRAuditor(generalInfo))
                {
                    int processid = 0;
                    //创建工作流实例
                    ESP.Compatible.Employee CurrentUser = new ESP.Compatible.Employee(generalInfo.requestor);
                    Msg = ESP.ITIL.BusinessLogic.申请单业务设置.申请单提交(CurrentUser, ref generalInfo);
                    if (Msg != "")
                    {
                        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('" + Msg + "');", true);
                        return;
                    }
                    if (generalInfo.ProcessID != 0 && generalInfo.InstanceID != 0)
                    {
                        p = new ProcessInstanceDao();
                        p.TerminateProcess(generalInfo.ProcessID, generalInfo.InstanceID);
                    }
                    processid = this.createTemplatePRProcess(generalInfo);
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

                    GeneralInfoManager.Update(generalInfo);
                    //如果申请单为协议供应商删除付款账期信息
                    if ((generalInfo.status == ESP.Purchase.Common.State.requisition_save || generalInfo.status == ESP.Purchase.Common.State.requisition_commit || generalInfo.status == ESP.Purchase.Common.State.requisition_temporary_commit) && generalInfo.source == "协议供应商")
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
                            paymentPeriod.periodDay = ESP.Purchase.Common.State.supplierpayment[supplierPeriod].ToString();
                            paymentPeriod.beginDate = DateTime.Parse(DateTime.Now.AddMonths(2 + supplierPeriod).ToString("yyyy-MM") + "-01").AddDays(14);
                            //paymentPeriod.endDate = DateTime.Parse(DateTime.Now.AddMonths(3 + supplierPeriod).ToString("yyyy-MM") + "-01").AddDays(-1);
                            paymentPeriod.periodType = (int)ESP.Purchase.Common.State.PeriodType.period;

                            PaymentPeriodManager.Add(paymentPeriod);
                        }
                    }

                    //发信
                    ESP.Purchase.Entity.LogInfo log = new ESP.Purchase.Entity.LogInfo();
                    ESP.Framework.Entity.EmployeeInfo emp = ESP.Framework.BusinessLogic.EmployeeManager.Get(generalInfo.requestor);
                    log.Gid = generalInfo.id;
                    log.LogMedifiedTeme = DateTime.Now;
                    log.LogUserId = generalInfo.requestor;
                    log.Des = string.Format(ESP.Purchase.Common.State.log_requisition_commit, generalInfo.requestorname + "(" + emp.FullNameEN + ")", DateTime.Now.ToString());
                    ESP.Framework.Entity.AuditBackUpInfo auditBackUp = ESP.Framework.BusinessLogic.AuditBackUpManager.GetLayOffModelByUserID(generalInfo.requestor);
                    if (auditBackUp != null)
                        log.Des = ESP.Framework.BusinessLogic.EmployeeManager.Get(auditBackUp.BackupUserID).FullNameCN + "代替" + log.Des;
                    ESP.Purchase.BusinessLogic.LogManager.AddLog(log, Request);

                    ////删除业务审核日志
                    OperationAuditLogManager.DeleteByGid(generalInfo.id);

                    string exMail = string.Empty;
                    try
                    {
                        int firstAuditorId = 0;

                        if (hidYS.Value != "")
                        {
                            firstAuditorId = int.Parse(hidYS.Value.Split(',')[0]);
                        }
                        else
                        {
                            string[] majordomoIds = hidZJSP.Value.TrimEnd(',').Split(',');//总监审批
                            string[] addGeneralIds = hidZJLSP.Value.TrimEnd(',').Split(',');//总经理审批
                            List<string> ZH1Ids = hidZJZH.Value.TrimEnd(',').Split(',').ToList();
                            string ZH1Emails = "";
                            if (majordomoIds[0] == "" && addGeneralIds[0] != "")
                            {
                                firstAuditorId = int.Parse(addGeneralIds[0]);
                            }
                            else
                            {
                                firstAuditorId = int.Parse(majordomoIds[0]);
                            }
                            for (int i = 0; i < ZH1Ids.Count; i++)
                            {
                                if (ZH1Ids[i].Trim() != "")
                                    ZH1Emails += ESP.Purchase.Common.State.getEmployeeEmailBySysUserId(int.Parse(ZH1Ids[i])) + ",";
                            }
                            //给知会人员发信
                            if (ZH1Emails != "")
                            {
                                ESP.Purchase.BusinessLogic.SendMailHelper.SendMailToZH2(generalInfo, generalInfo.PrNo, generalInfo.requestorname, new ESP.Compatible.Employee(firstAuditorId).Name, ZH1Emails.TrimEnd(','));
                            }
                        }
                        string ret = ESP.Purchase.BusinessLogic.SendMailHelper.SendMailPR(generalInfo, Request, generalInfo.PrNo, generalInfo.requestorname, ESP.Purchase.Common.State.getEmployeeEmailBySysUserId(generalInfo.enduser), ESP.Purchase.Common.State.getEmployeeEmailBySysUserId(generalInfo.goods_receiver), ESP.Purchase.Common.State.getEmployeeEmailBySysUserId(firstAuditorId));
                    }
                    catch
                    {
                        exMail = "(邮件发送失败!)";
                    }
                    Page.ClientScript.RegisterStartupScript(typeof(string), "", "alert('" + generalInfo.PrNo + "已成功设置业务审核人并提交成功，请在查询中心查询审批状态。"+exMail+"');", true);
                }
                else
                {
                    throw new Exception();
                }
            }
            catch (Exception ex)
            {
                string s = "设置业务审核人失败！";
                Page.ClientScript.RegisterStartupScript(typeof(string), "", "alert('" + ESP.Utilities.JavascriptUtility.QuoteJScriptString(s, false, true) + "');", true);
            }

        }

        private void CommitPNAuditor(ReturnInfo returnModel)
        {
            ESP.Compatible.Employee CurrentUser = new ESP.Compatible.Employee(returnModel.RequestorID.Value);
            try
            {
                if (SavePNAuditor(returnModel))
                {
                    if (returnModel.ReturnType.Value != (int)ESP.Purchase.Common.PRTYpe.PrivatePR)
                    {
                        int processid = 0;
                        //创建工作流实例
                        returnModel.ReturnStatus = (int)ESP.Finance.Utility.PaymentStatus.Submit;
                        if (returnModel.ProcessID != null && returnModel.InstanceID != null)
                        {
                            p = new ProcessInstanceDao();
                            p.TerminateProcess(returnModel.ProcessID.Value, returnModel.InstanceID.Value);
                        }
                        processid = this.createTemplatePNProcess(returnModel);
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
                        // ESP.Finance.BusinessLogic.ReturnManager.UpdateWorkFlow(returnModel.ReturnID, returnModel.WorkItemID.Value, returnModel.WorkItemName, returnModel.ProcessID.Value, returnModel.InstanceID.Value);
                    }
                    else
                    {
                        returnModel.ReturnStatus = (int)ESP.Finance.Utility.PaymentStatus.Submit;
                        ESP.Finance.BusinessLogic.ReturnManager.Update(returnModel);
                    }
                    int firstAuditorId = 0;

                    if (hidYS.Value != "")
                    {
                        firstAuditorId = int.Parse(hidYS.Value.Split(',')[0]);
                    }
                    else
                    {
                        string[] majordomoIds = hidZJSP.Value.TrimEnd(',').Split(',');
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
                    string exMail = string.Empty;
                    try
                    {
                        //给第一级审核人发送邮件
                        ESP.Framework.Entity.EmployeeInfo emp = ESP.Framework.BusinessLogic.EmployeeManager.Get(firstAuditorId);
                        if (emp != null)
                            ESP.Finance.Utility.SendMailHelper.SendMailReturnCommit(returnModel, CurrentUser.Name, emp.FullNameCN, CurrentUser.EMail, emp.Email);

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
                            ESP.Finance.Utility.SendMailHelper.SendMailToZH2FK(returnModel, returnModel.ReturnCode, CurrentUser.Name, new ESP.Compatible.Employee(firstAuditorId).Name, ZH1Emails.TrimEnd(','));
                        }
                    }
                    catch
                    {
                        exMail = "(邮件发送失败!)";
                    }
                    Page.ClientScript.RegisterStartupScript(typeof(string), "", "alert('已成功设置付款业务审批人并提交成功！"+exMail+"');", true);
                }
                else
                {
                    throw new Exception();
                }
            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(typeof(string), "", "alert('设置付款业务审核人失败！" + ex.Message + "');", true);
            }
        }
        private int createTemplatePRProcess(GeneralInfo generalInfo)
        {
            int ret;
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

            //总监级附加审核人
            if (hidZJFJ.Value != "")
            {
                string[] fj1Ids = hidZJFJ.Value.TrimEnd(',').Split(',');
                for (int i = 0; i < fj1Ids.Length; i++)
                {
                    if (fj1Ids[i].Trim() != "" && !arrIds.Contains(fj1Ids[i]))
                    {
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

            //总经理级附加
            if (hidZJLFJ.Value != "")
            {
                string[] fj2Ids = hidZJLFJ.Value.TrimEnd(',').Split(',');
                for (int i = 0; i < fj2Ids.Length; i++)
                {
                    if (fj2Ids[i].Trim() != "" && !arrIds.Contains(fj2Ids[i]))
                    {
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

            //CEO
            if (hidCEO.Value != "" && !arrIds.Contains(hidCEO.Value.TrimEnd(',')))
            {
                string CEOName = new ESP.Compatible.Employee(int.Parse(hidCEO.Value.TrimEnd(','))).Name;
                ModelTemplate.ModelTask CEOTask = new ModelTask();
                CEOTask.TaskName = "PR单审核" + CEOName + hidCEO.Value.TrimEnd(',');
                CEOTask.TaskType = WfStateContants.TASKTYPE_SERIALTASK;
                CEOTask.DisPlayName = "PR单审核" + CEOName + hidCEO.Value.TrimEnd(',');
                CEOTask.RoleName = hidCEO.Value.TrimEnd(',');

                if (lastTask != null)
                {
                    ModelTemplate.Transition trans = new Transition();
                    trans.TransitionName = lastTask.DisPlayName;
                    trans.TransitionTo = CEOTask.DisPlayName;
                    lastTask.Transations.Add(trans);
                    lists.Add(lastTask);

                    lastTask = CEOTask;
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
        private int createTemplatePNProcess(ESP.Finance.Entity.ReturnInfo model)
        {
            int ret;
            string checkAuditor = ",";
            ModelTemplate.BLL.ModelManager manager = new ModelManager();
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
                            string userName = new ESP.Compatible.Employee(int.Parse(prejudicationIds[i])).Name;
                            ModelTemplate.ModelTask prejudicationTask = new ModelTask();
                            prejudicationTask.TaskName = "PN付款审核" + userName + prejudicationIds[i];
                            prejudicationTask.TaskType = WfStateContants.TASKTYPE_SERIALTASK;
                            prejudicationTask.DisPlayName = "PN付款审核" + userName + prejudicationIds[i];
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
                        string userName = new ESP.Compatible.Employee(int.Parse(ZH1Ids[i])).Name;
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
                    string userName = new ESP.Compatible.Employee(int.Parse(hidZJSP.Value.TrimEnd(','))).Name;
                    ModelTemplate.ModelTask addMajordomoTask = new ModelTask();
                    addMajordomoTask.TaskName = "PN付款审核" + userName + hidZJSP.Value.TrimEnd(',');
                    addMajordomoTask.TaskType = WfStateContants.TASKTYPE_SERIALTASK;
                    addMajordomoTask.DisPlayName = "PN付款审核" + userName + hidZJSP.Value.TrimEnd(',');
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
                            string userName = new ESP.Compatible.Employee(int.Parse(fj1Ids[i])).Name;
                            ModelTemplate.ModelTask append1Task = new ModelTask();
                            append1Task.TaskName = "PN付款审核" + userName + fj1Ids[i];
                            append1Task.TaskType = WfStateContants.TASKTYPE_SERIALTASK;
                            append1Task.DisPlayName = "PN付款审核" + userName + fj1Ids[i];
                            append1Task.RoleName = fj1Ids[i];

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
                    string userName = new ESP.Compatible.Employee(int.Parse(hidZJLSP.Value.TrimEnd(','))).Name;
                    ModelTemplate.ModelTask addGeneralTask = new ModelTask();
                    addGeneralTask.TaskName = "PN付款审核" + userName + hidZJLSP.Value.TrimEnd(',');
                    addGeneralTask.TaskType = WfStateContants.TASKTYPE_SERIALTASK;
                    addGeneralTask.DisPlayName = "PN付款审核" + userName + hidZJLSP.Value.TrimEnd(',');
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
                            string userName = new ESP.Compatible.Employee(int.Parse(fj2Ids[i])).Name;
                            ModelTemplate.ModelTask append2Task = new ModelTask();
                            append2Task.TaskName = "PN付款审核" + userName + fj2Ids[i];
                            append2Task.TaskType = WfStateContants.TASKTYPE_SERIALTASK;
                            append2Task.DisPlayName = "PN付款审核" + userName + fj2Ids[i];
                            append2Task.RoleName = fj2Ids[i];

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
                    string CEOName = new ESP.Compatible.Employee(int.Parse(hidCEO.Value.TrimEnd(','))).Name;
                    ModelTemplate.ModelTask CEOTask = new ModelTask();
                    CEOTask.TaskName = "PN付款审核" + CEOName + hidCEO.Value.TrimEnd(',');
                    CEOTask.TaskType = WfStateContants.TASKTYPE_SERIALTASK;
                    CEOTask.DisPlayName = "PN付款审核" + CEOName + hidCEO.Value.TrimEnd(',');
                    CEOTask.RoleName = hidCEO.Value.TrimEnd(',');

                    if (lastTask != null)
                    {
                        ModelTemplate.Transition trans = new Transition();
                        trans.TransitionName = lastTask.DisPlayName;
                        trans.TransitionTo = CEOTask.DisPlayName;
                        lastTask.Transations.Add(trans);
                        lists.Add(lastTask);

                        lastTask = CEOTask;
                    }
                }
            }
            //FA
            if (hidFA.Value != "")
            {
                if (checkAuditor.IndexOf(hidFA.Value.TrimEnd(',')) < 0)
                {
                    checkAuditor += hidFA.Value.TrimEnd(',') + ",";
                    string FAName = new ESP.Compatible.Employee(int.Parse(hidFA.Value.TrimEnd(','))).Name;
                    ModelTemplate.ModelTask FATask = new ModelTask();
                    FATask.TaskName = "PN付款审核" + FAName + hidFA.Value.TrimEnd(',');
                    FATask.TaskType = WfStateContants.TASKTYPE_SERIALTASK;
                    FATask.DisPlayName = "PN付款审核" + FAName + hidFA.Value.TrimEnd(',');
                    FATask.RoleName = hidFA.Value.TrimEnd(',');

                    if (lastTask != null)
                    {
                        ModelTemplate.Transition trans = new Transition();
                        trans.TransitionName = lastTask.DisPlayName;
                        trans.TransitionTo = FATask.DisPlayName;
                        lastTask.Transations.Add(trans);
                        lists.Add(lastTask);

                        lastTask = FATask;
                    }
                }
            }

            ModelTemplate.Transition endTrans = new Transition();
            endTrans.TransitionName = lastTask.DisPlayName;
            endTrans.TransitionTo = "end";
            lastTask.Transations.Add(endTrans);
            lists.Add(lastTask);
            if (model.ProcessID != null && model.InstanceID != null)
            {
                p = new ProcessInstanceDao();
                p.TerminateProcess(model.ProcessID.Value, model.InstanceID.Value);
            }
            ret = manager.ImportData("PN付款业务审核(" + model.ReturnID + ")", "PN付款业务审核(" + model.ReturnID + ")", "1.0", model.RequestEmployeeName, lists);
            return ret;
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
            object model = null;
            if (this.ddlType.SelectedValue == "0")
                model = ESP.Purchase.BusinessLogic.GeneralInfoManager.GetModel(Convert.ToInt32(this.txtID.Text));
            else if (this.ddlType.SelectedValue == "1")
                model = ESP.Finance.BusinessLogic.ReturnManager.GetModel(Convert.ToInt32(this.txtID.Text));

            workitemdao.insertItemData(Convert.ToInt32(np.get_lastActivity().getWorkItem().Id), Convert.ToInt32(np.get_instance_data().Id), ESP.Purchase.BusinessLogic.SerializeFactory.Serialize(model));
        }

    }
}
