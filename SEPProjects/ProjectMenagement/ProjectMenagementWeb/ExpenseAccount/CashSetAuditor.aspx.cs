/************************************************************************\
 * 报销单提交、设置审核人页
 *      
 * 根据报销单类型、金额及申请人角色设置相应审批人
 * 
 *
\************************************************************************/
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
using ESP.Administrative.Entity;
using ESP.Framework.BusinessLogic;
using ESP.Finance.BusinessLogic;

public partial class ExpenseAccount_CashSetAuditor : ESP.Web.UI.PageBase
{
    int returnId = 0;
    string BeiJingBranch = string.Empty;
    protected ESP.Finance.Entity.ReturnInfo returnModel = null;
    protected ESP.Finance.Entity.ProjectInfo projectModel = null;
    protected ESP.Finance.Entity.SupporterInfo supportModel = null;
    private IList<ESP.Finance.Entity.SupporterCostInfo> supportCostList = null;
    private IList<ESP.Finance.Entity.SupporterExpenseInfo> supportExpenseList = null;

    private IList<ESP.Purchase.Entity.GeneralInfo> PRList;
    private IList<ReturnInfo> ReturnList;
    private IList<ExpenseAccountDetailInfo> ExpenseDetails;
    private Dictionary<int, int> TypeMappings;
    private IList<ESP.Purchase.Entity.PaymentPeriodInfo> Periods;
    private IList<ESP.Purchase.Entity.MediaPREditHisInfo> MediaPREditHises;
    private List<ESP.Purchase.Entity.OrderInfo> Orders;
    private List<CostRecordInfo> ExpenseRecords;
    private List<CostRecordInfo> PRRecords;

    Dictionary<int, decimal> CostMappings = new Dictionary<int, decimal>();
    Dictionary<int, decimal> ExpenseMappings = new Dictionary<int, decimal>();
    decimal TraficFee;
    decimal UsedCost;
    private IList<ESP.Purchase.Entity.TypeInfo> typeList;



    private void AddValue(Dictionary<int, decimal> m, int key, decimal val)
    {
        decimal cv;
        if (m.TryGetValue(key, out cv))
        {
            m[key] = cv + val;
        }
        else
        {
            m.Add(key, val);
        }
    }
    private void BindData()
    {
        if (!string.IsNullOrEmpty(Request["id"]))
        {
            //returnId = int.Parse(Request["id"]);
            //returnModel = ESP.Finance.BusinessLogic.ReturnManager.GetModel(returnId);

            if (returnModel.ProjectID.Value != 0)
            {
                projectModel = ESP.Finance.BusinessLogic.ProjectManager.GetModel(returnModel.ProjectID.Value);
                if (projectModel == null || projectModel.GroupID == null || projectModel.GroupID.Value == 0)
                    return;
                if (projectModel.GroupID != returnModel.DepartmentID)
                {
                    supportModel = ESP.Finance.BusinessLogic.SupporterManager.GetList(string.Format("ProjectID={0} and GroupID={1}", returnModel.ProjectID, returnModel.DepartmentID))[0];
                    supportCostList = ESP.Finance.BusinessLogic.SupporterCostManager.GetList(supportModel.SupportID, null, null);
                    supportExpenseList = ESP.Finance.BusinessLogic.SupporterExpenseManager.GetList(" SupporterID=" + supportModel.SupportID);
                }

                var typelvl2 = ESP.Purchase.BusinessLogic.TypeManager.GetListLvl2();
                typelvl2[0] = "OOP";
                typelvl2[-1] = "[未知]";

                PRList = ESP.Purchase.BusinessLogic.GeneralInfoManager.GetModelList(returnModel.ProjectID.Value, returnModel.DepartmentID.Value);
                ReturnList = ESP.Finance.BusinessLogic.ReturnManager.GetList(returnModel.ProjectID.Value, returnModel.DepartmentID.Value);
                ExpenseDetails = ESP.Finance.BusinessLogic.ExpenseAccountDetailManager.GetList(ReturnList.Select(x => x.ReturnID).ToArray());
                Periods = ESP.Purchase.BusinessLogic.PaymentPeriodManager.GetList(PRList.Select(x => x.id).ToArray());
                MediaPREditHises = ESP.Purchase.BusinessLogic.MediaPREditHisManager.GetModelByOldPRIDs(PRList.Where(x => x.PRType == 1 || x.PRType == 6).Select(x => x.id).ToArray());
                Orders = ESP.Purchase.BusinessLogic.OrderInfoManager.GetListByGeneralIds(PRList.Select(x => x.id).ToArray());
                TypeMappings = ESP.Purchase.BusinessLogic.TypeManager.GetTypeMappings();
                typeList = ESP.Purchase.BusinessLogic.TypeManager.GetModelList("");

                ExpenseRecords = new List<CostRecordInfo>();
                PRRecords = new List<CostRecordInfo>();


                foreach (var pr in PRList)
                {
                    if (pr.PRType == (int)ESP.Purchase.Common.PRTYpe.PR_PriFA || pr.PRType == (int)ESP.Purchase.Common.PRTYpe.PR_MediaFA)
                        continue;

                    decimal paid = 0;
                    var orders = Orders.Where(x => x.general_id == pr.id);

                    var relationModel = MediaPREditHises.Where(x => x.OldPRId == pr.id).FirstOrDefault();
                    if (relationModel != null)
                    {
                        var costTypeId = orders.Select(x => x.producttype).FirstOrDefault();
                        if (!TypeMappings.TryGetValue(costTypeId, out costTypeId)) costTypeId = 0;

                        var r = ReturnList.Where(x => x.ReturnID == relationModel.NewPNId).FirstOrDefault();
                        if (r != null)
                        {
                            AddValue(CostMappings, costTypeId, r.PreFee ?? 0);
                            paid += r.FactFee ?? 0;
                        }
                        var newpr = PRList.Where(x => x.id == relationModel.NewPRId).FirstOrDefault();
                        if (newpr != null)
                        {
                            AddValue(CostMappings, costTypeId, newpr.totalprice);
                            var pnofnewpr = ReturnList.Where(x => x.PRID == newpr.id).FirstOrDefault();
                            if (pnofnewpr != null)
                                paid += pnofnewpr.FactFee ?? 0;
                        }
                    }
                    else
                    {
                        foreach (var o in orders)
                        {
                            var costTypeId = o.producttype;
                            if (!TypeMappings.TryGetValue(costTypeId, out costTypeId)) costTypeId = 0;

                            if (o.FactTotal != 0)
                                AddValue(CostMappings, costTypeId, o.FactTotal);
                            else
                                AddValue(CostMappings, costTypeId, o.total);
                        }

                        paid = ReturnList.Where(x => x.PRID == pr.id && x.ReturnStatus == 140).Sum(x => x.FactFee ?? 0);
                    }

                    var typeid = orders.Select(x => x.producttype).FirstOrDefault();
                    if (!TypeMappings.TryGetValue(typeid, out typeid)) typeid = 0;
                    CostRecordInfo detail = new CostRecordInfo()
                    {
                        PRID = pr.id,
                        PRNO = pr.PrNo,
                        SupplierName = pr.supplier_name,
                        Description = pr.project_descripttion,
                        Requestor = pr.requestorname,
                        GroupName = pr.requestor_group,
                        TypeID = typeid,
                        TypeName = typelvl2[typeid],
                        AppAmount = pr.totalprice,
                        PaidAmount = paid,
                        UnPaidAmount = pr.totalprice - paid,
                        CostPreAmount = 0 // supportModel == null ? projectModel.CostDetails.Where(x => x.CostTypeID == typeid).Select(x => x.Cost ?? 0).FirstOrDefault() : supportCostList.Where(x => x.CostTypeID == typeid).Select(x => x.Amounts).FirstOrDefault()
                    };
                    PRRecords.Add(detail);
                }

                foreach (var record in PRRecords)
                {
                    decimal v = 0M;
                    CostMappings.TryGetValue(record.TypeID, out v);
                    record.TypeTotalAmount = v;
                }

                var expenseReturns = ReturnList.Where(x => x.ReturnType == 30
                    || (x.ReturnType == 32 && x.ReturnStatus != 140)
                    || x.ReturnType == 31
                    || x.ReturnType == 37
                    || x.ReturnType == 33
                    || x.ReturnType == 40
                    || (x.ReturnType == 36 && x.ReturnStatus == 139)
                    || x.ReturnType == 35).ToList();
                foreach (var r in expenseReturns)
                {
                    var details = ExpenseDetails.Where(x => x.ReturnID == r.ReturnID && x.Status == 1).ToList();
                    foreach (var d in details)
                    {
                        if (d.TicketStatus == 1)
                            continue;
                        var e = d.ExpenseMoney ?? 0;
                        if (e != 0)
                            AddValue(ExpenseMappings, d.CostDetailID ?? 0, e);

                        var typeid = d.CostDetailID ?? 0;
                       
                        CostRecordInfo detail = new CostRecordInfo()
                        {
                            ReturnType = r.ReturnType ?? 0,
                            PRNO = r.ReturnCode,
                            Description = d.ExpenseDesc,
                            Requestor = r.RequestEmployeeName,
                            GroupName = r.DepartmentName,
                            TypeID = typeid,
                            TypeName = typelvl2[typeid],
                            AppAmount = e,
                            PaidAmount = (r.ReturnStatus == 140 || r.ReturnStatus == 139) ? e : 0,
                            UnPaidAmount = (r.ReturnStatus != 140 && r.ReturnStatus != 139) ? e : 0,
                            CostPreAmount = 0,
                            PNTotal = r.PreFee ?? 0
                        };
                        ExpenseRecords.Add(detail);
                    }
                }

                foreach (var record in ExpenseRecords)
                {
                    decimal v = 0M;
                    ExpenseMappings.TryGetValue(record.TypeID, out v);
                    record.TypeTotalAmount = v;
                }

                //TraficFee = ReturnList.Where(x => x.ReturnType == 20).Sum(x => (x.FactFee ?? (x.PreFee ?? 0)));

                UsedCost = CostMappings.Sum(x => x.Value) + ExpenseMappings.Sum(x => x.Value);
            }
        }
    }

    private bool validMoney(ReturnInfo returnModel)
    {
        decimal usedoop = 0;
        decimal usedcost = 0;
        decimal oopTotal = 0;
        decimal currentOrderOOP = 0;

        // decimal costTotal = 0;
        bool ret = true;
        usedoop = 0M;
        if (projectModel == null)
            return true;

        List<ExpenseAccountDetailInfo> detailList = ESP.Finance.BusinessLogic.ExpenseAccountDetailManager.GetList(" and returnid =" + returnModel.ReturnID + " and ticketstatus =0 and status=1");


        if (supportModel == null)
        {
            if (projectModel == null || projectModel.Expenses == null || projectModel.Expenses.Count == 0)
                oopTotal = 0;
            else
            {
                oopTotal = projectModel.Expenses.Sum(x=>x.Expense).Value;
            }
        }
        else
        {
            if (supportExpenseList == null || supportExpenseList.Count == 0)
                oopTotal = 0;
            else
                oopTotal = supportExpenseList.Sum(x => x.Expense).Value;
        }

        foreach (var detail in detailList)
        {
            if (detail.CostDetailID == 0)
            {
                usedoop = ExpenseMappings.Where(x => x.Key == 0).Sum(x => x.Value);

                if (usedoop + detail.ExpenseMoney + currentOrderOOP > oopTotal)
                {
                    ret = false;
                    break;
                }
                currentOrderOOP += detail.ExpenseMoney ?? 0;
            }
            else
            {
                usedoop = ExpenseMappings.Where(x => x.Key == detail.CostDetailID).Sum(x => x.Value);
                usedcost = CostMappings.Where(x => x.Key == detail.CostDetailID).Sum(x => x.Value);
                var currentTypeCost = detailList.Where(x => x.CostDetailID == detail.CostDetailID).Sum(x => x.ExpenseMoney);

                if (supportModel == null)//主申请方
                {
                    //取退款申请释放成本
                    decimal refundTotal = RefundManager.GetList(" projectId =" + projectModel.ProjectId + " and status not in(0,-1,1) and deptid="+projectModel.GroupID).Sum(x => x.Amounts);
                    usedcost = usedcost - refundTotal;

                    var costDetailModel = projectModel.CostDetails.Where(x => x.CostTypeID == detail.CostDetailID).FirstOrDefault();
                    if (usedoop + usedcost + currentTypeCost > costDetailModel.Cost)
                    {
                        ret = false;
                        break;
                    }
                }
                else//支持放
                {

                    //取退款申请释放成本
                    decimal refundTotal = RefundManager.GetList(" projectId =" + supportModel.ProjectID + " and status not in(0,-1,1) and deptid=" + supportModel.GroupID).Sum(x => x.Amounts);
                    usedcost = usedcost - refundTotal;

                    var costDetailModel = supportCostList.Where(x => x.CostTypeID == detail.CostDetailID).FirstOrDefault();
                    if (usedoop + usedcost + currentTypeCost > costDetailModel.Amounts)
                    {
                        ret = false;
                        break;
                    }
                }
            }
        }

        return ret;
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        BeiJingBranch = System.Configuration.ConfigurationManager.AppSettings["FinanceAuditBJBranch"];
        if (!string.IsNullOrEmpty(Request["id"]))
        {
            returnId = int.Parse(Request["id"]);
            returnModel = ESP.Finance.BusinessLogic.ReturnManager.GetModel(returnId);
        }
        BindData();

        if (!IsPostBack)
        {
            ViewContorl(returnModel);
        }

    }

    /// <summary>
    /// 初始化审批人
    /// </summary>
    /// <param name="model"></param>
    private void ViewContorl(ESP.Finance.Entity.ReturnInfo model)
    {
        bool isHaveYS = false;
        bool isHaveZJ = false;
        bool isHaveZJL = false;
        bool isHaveCEO = false;
        bool isHaveFA = false;
        bool isHaveCWZJ = false;

        string removeTypes = "";
        //判断分公司的第一级审核人是否是FA
        string branchCode = model.ProjectCode.Substring(0, 1);

        int rowCount = 1;
        int deptId = model.DepartmentID.Value;

        ESP.Finance.Entity.BranchInfo branchModel = ESP.Finance.BusinessLogic.BranchManager.GetModelByCode(branchCode);


        //根据部门获取部门各级审批人
        ESP.Framework.Entity.OperationAuditManageInfo manageModel = null;

        if (model.ProjectID != null && model.ProjectID.Value != 0)
        {
            manageModel = ESP.Framework.BusinessLogic.OperationAuditManageManager.GetModelByProjectId(model.ProjectID.Value);
        }
        if (manageModel == null)
            manageModel = ESP.Framework.BusinessLogic.OperationAuditManageManager.GetModelByUserId(model.RequestorID.Value); ;

        if (manageModel == null)
            manageModel = ESP.Framework.BusinessLogic.OperationAuditManageManager.GetModelByDepId(deptId);

        if (manageModel.FAId == 0 || branchModel.FirstFinanceID == manageModel.FAId)
        {
            isHaveFA = true;
            removeTypes += ESP.Finance.Utility.auditorType.operationAudit_Type_FA + ",";
        }

        removeTypes += ESP.Finance.Utility.auditorType.purchase_first + ",";
        removeTypes += ESP.Finance.Utility.auditorType.purchase_major2 + ",";
        removeTypes += ESP.Finance.Utility.auditorType.operationAudit_Type_Financial + ",";
        removeTypes += ESP.Finance.Utility.auditorType.operationAudit_Type_Financial2 + ",";
        removeTypes += ESP.Finance.Utility.auditorType.operationAudit_Type_Financial3 + ",";


        ESP.HumanResource.Entity.EmployeesInPositionsInfo positionModel = ESP.HumanResource.BusinessLogic.EmployeesInPositionsManager.GetModel(model.RequestorID.Value);

        ESP.Framework.Entity.OperationAuditManageInfo selfManageModel = null;//申请人所属部门对应的审批路径

        if (positionModel.DepartmentID != model.DepartmentID.Value && manageModel.UserId == 0)
        {
            selfManageModel = ESP.Framework.BusinessLogic.OperationAuditManageManager.GetModelByDepId(positionModel.DepartmentID);
        }

        //得到北上广三地部门ID
        List<ESP.Framework.Entity.DepartmentInfo> BSGDeps = (List<ESP.Framework.Entity.DepartmentInfo>)ESP.Finance.Configuration.ConfigurationManager.GetFinanceDept();

        string financeType = this.GetFinanceType(BSGDeps, deptId);

        //是否行政人员
        bool isAdministrative = false;
        if (ESP.Configuration.ConfigurationManager.SafeAppSettings["AdministrativeIDs"].IndexOf("," + deptId.ToString() + ",") >= 0)
        {
            isAdministrative = true;
        }

        #region 如果是GM项目，不需要FA审批
        if (model.ProjectCode.IndexOf("GM*") >= 0 || model.ProjectCode.IndexOf("*GM") >= 0 || model.ProjectCode.IndexOf(ESP.Finance.Configuration.ConfigurationManager.ShunYaShortEN) >= 0)
        {
            int rowcount = 0;

            //默认总监
            if (CurrentUserID != manageModel.DirectorId && CurrentUserID != manageModel.ManagerId)
            {
                string trId = string.Empty;

                //默认总监
                System.Web.UI.HtmlControls.HtmlTableRow ZJFJRow = new System.Web.UI.HtmlControls.HtmlTableRow();
                System.Web.UI.HtmlControls.HtmlTableCell ZJFJCell1 = new System.Web.UI.HtmlControls.HtmlTableCell();
                System.Web.UI.HtmlControls.HtmlTableCell ZJFJCell2 = new System.Web.UI.HtmlControls.HtmlTableCell();
                System.Web.UI.HtmlControls.HtmlTableCell ZJFJCell3 = new System.Web.UI.HtmlControls.HtmlTableCell();
                System.Web.UI.HtmlControls.HtmlTableCell ZJFJCell4 = new System.Web.UI.HtmlControls.HtmlTableCell();
                System.Web.UI.HtmlControls.HtmlTableCell ZJFJCell5 = new System.Web.UI.HtmlControls.HtmlTableCell();
                System.Web.UI.HtmlControls.HtmlTableCell ZJFJCell6 = new System.Web.UI.HtmlControls.HtmlTableCell();
                ZJFJCell1.Align = "Center";
                ZJFJCell2.Align = "Center";
                ZJFJCell3.Align = "Center";
                ZJFJCell4.Align = "Center";
                ZJFJCell5.Align = "Center";
                ZJFJCell6.Align = "Center";

                ZJFJCell1.InnerHtml = rowCount.ToString();
                rowCount++;
                ZJFJCell2.InnerHtml = manageModel.DirectorName;
                ZJFJCell3.InnerHtml = new ESP.Compatible.Employee(manageModel.DirectorId).PositionDescription;
                ZJFJCell4.InnerHtml = manageModel.DirectorAmount.ToString("#,##0.00"); 
                trId = ZJFJRow.Attributes["id"] = "tr_" + manageModel.DirectorId + "_ZJSP";
                ZJFJCell5.InnerHtml = ESP.Finance.Utility.auditorType.operationAudit_Type_Names[ESP.Finance.Utility.auditorType.operationAudit_Type_ZJSP];
                ZJFJCell6.InnerHtml = "<a href=\"\" onclick=\"openEmployee('ZJSP');return false;\">添加</a>&nbsp;<a href=\"\" onclick=\"openEmployee1('ZJSP','" + trId + "','" + manageModel.DirectorId + "');return false;\">更改</a>";//&nbsp;<a href=\"\" onclick=\"removeRow('ZJSP','" + manageModel.DirectorId + "','" + trId + "');return false;\">删除</a>";


                this.hidZJSP.Value = manageModel.DirectorId.ToString();
                ZJFJRow.Cells.Add(ZJFJCell1);
                ZJFJRow.Cells.Add(ZJFJCell2);
                ZJFJRow.Cells.Add(ZJFJCell3);
                ZJFJRow.Cells.Add(ZJFJCell4);
                ZJFJRow.Cells.Add(ZJFJCell5);
                ZJFJRow.Cells.Add(ZJFJCell6);
                ZJFJRow.Attributes["class"] = "td";
                tab.Rows.Add(ZJFJRow);
            }
            if ((CurrentUserID == manageModel.DirectorId && CurrentUserID != manageModel.ManagerId) || model.PreFee > manageModel.DirectorAmount)
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
                ZJLCell4.InnerHtml = manageModel.ManagerAmount.ToString("#,##0.00");
                if (manageModel.ManagerId.ToString() == ESP.Configuration.ConfigurationManager.SafeAppSettings["DavidZhangID"].Trim())
                    ZJLCell5.InnerHtml = ESP.Finance.Utility.auditorType.operationAudit_Type_Names[ESP.Finance.Utility.auditorType.operationAudit_Type_CEO];
                else
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

            //10W CEO审批
            if (manageModel.ManagerId == CurrentUserID || model.PreFee > manageModel.ManagerAmount)
            {
                ESP.Compatible.Employee empCEO = new ESP.Compatible.Employee(manageModel.CEOId);

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

                CEOCell1.InnerHtml = rowCount.ToString();
                rowCount++;
                CEOCell2.InnerHtml = empCEO.Name;
                CEOCell3.InnerHtml = empCEO.PositionDescription;

                if (empCEO.IntID == manageModel.RiskControlAccounter)
                    CEOCell5.InnerHtml = ESP.Finance.Utility.auditorType.operationAudit_Type_Names[ESP.Finance.Utility.auditorType.operationAudit_Type_RiskControl];
                else
                {
                    CEOCell4.InnerHtml = manageModel.CEOAmount.ToString("#,##0.00") + "以上";
                    CEOCell5.InnerHtml = ESP.Finance.Utility.auditorType.operationAudit_Type_Names[ESP.Finance.Utility.auditorType.operationAudit_Type_CEO];
                }
                CEOCell6.InnerHtml = "&nbsp;";

                CEORow.Attributes["id"] = "tr_" + empCEO.IntID.ToString() + "_CEO";

                hidCEO.Value = empCEO.IntID.ToString();
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
        #endregion

        #region 非GM项目的流程
        else
        {
            #region 普通员工 默认的审批人
            
            //当前申请人与项目不是同一组别，先本部门总监预审
            if (selfManageModel!=null && model.RequestorID.Value != selfManageModel.DirectorId && model.RequestorID.Value != selfManageModel.ManagerId)
            {
                System.Web.UI.HtmlControls.HtmlTableRow ysRow = new System.Web.UI.HtmlControls.HtmlTableRow();
                System.Web.UI.HtmlControls.HtmlTableCell ysCell1 = new System.Web.UI.HtmlControls.HtmlTableCell();
                System.Web.UI.HtmlControls.HtmlTableCell ysCell2 = new System.Web.UI.HtmlControls.HtmlTableCell();
                System.Web.UI.HtmlControls.HtmlTableCell ysCell3 = new System.Web.UI.HtmlControls.HtmlTableCell();
                System.Web.UI.HtmlControls.HtmlTableCell ysCell4 = new System.Web.UI.HtmlControls.HtmlTableCell();
                System.Web.UI.HtmlControls.HtmlTableCell ysCell5 = new System.Web.UI.HtmlControls.HtmlTableCell();
                System.Web.UI.HtmlControls.HtmlTableCell ysCell6 = new System.Web.UI.HtmlControls.HtmlTableCell();
                ysCell1.Align = "Center";
                ysCell2.Align = "Center";
                ysCell3.Align = "Center";
                ysCell4.Align = "Center";
                ysCell5.Align = "Center";
                ysCell6.Align = "Center";
                ysCell1.InnerHtml = rowCount.ToString();
                rowCount++;
                ysCell2.InnerHtml = selfManageModel.DirectorName;
                ysCell3.InnerHtml = new ESP.Compatible.Employee(selfManageModel.DirectorId).PositionDescription;
                ysCell4.InnerHtml = selfManageModel.DirectorAmount.ToString("#,##0.00");
                string trId1 = ysRow.Attributes["id"] = "tr_" + selfManageModel.DirectorId + "_YSSP";

                ysCell5.InnerHtml = ESP.Finance.Utility.auditorType.operationAudit_Type_Names[ESP.Finance.Utility.auditorType.operationAudit_Type_YS];
                ysCell6.InnerHtml = "&nbsp;";

                hidYS.Value = selfManageModel.DirectorId.ToString() + ",";
                ysRow.Cells.Add(ysCell1);
                ysRow.Cells.Add(ysCell2);
                ysRow.Cells.Add(ysCell3);
                ysRow.Cells.Add(ysCell4);
                ysRow.Cells.Add(ysCell5);
                ysRow.Cells.Add(ysCell6);
                ysRow.Attributes["class"] = "td";
                tab.Rows.Add(ysRow);

            }

            //默认项目负责人审核
            if (manageModel.UserId == 0 && isHaveYS == false && projectModel != null && model.RequestorID.Value != (supportModel == null ? projectModel.ApplicantUserID : supportModel.LeaderUserID))
            {
                if (isAdministrative)
                { }
                else
                {
                    if (manageModel.DirectorId != Convert.ToInt32(supportModel == null ? projectModel.ApplicantUserID.ToString() : supportModel.LeaderUserID.ToString()))
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
                        YSCell1.InnerHtml = rowCount.ToString();
                        rowCount++;
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
                    }
                }

            }


            if (!isHaveZJ && returnModel.RequestorID.Value != manageModel.DirectorId)
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
                    ZJCell1.InnerHtml = rowCount.ToString();
                    rowCount++;
                    ZJCell2.InnerHtml = manageModel.DirectorName;
                    ZJCell3.InnerHtml = new ESP.Compatible.Employee(manageModel.DirectorId).PositionDescription;
                    ZJCell4.InnerHtml = manageModel.DirectorAmount.ToString("#,##0.00");
                    string trId = ZJRow.Attributes["id"] = "tr_" + manageModel.DirectorId + "_ZJSP";
                    ZJCell5.InnerHtml = ESP.Finance.Utility.auditorType.operationAudit_Type_Names[ESP.Finance.Utility.auditorType.operationAudit_Type_ZJSP];
                    ZJCell6.InnerHtml = "<a href=\"\" onclick=\"openEmployee('ZJSP');return false;\">添加</a>&nbsp;<a href=\"\" onclick=\"openEmployee1('ZJSP','" + trId + "','" + manageModel.DirectorId + "');return false;\">更改</a>";//&nbsp;<a href=\"\" onclick=\"removeRow('ZJSP','" + manageModel.DirectorId + "','" + trId + "');return false;\">删除</a>";
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

            if (model.PreFee > manageModel.DirectorAmount || model.RequestorID.Value == manageModel.DirectorId)
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
                    ZJLCell1.InnerHtml = rowCount.ToString();
                    rowCount++;
                    ZJLCell2.InnerHtml = manageModel.ManagerName;
                    ZJLCell3.InnerHtml = new ESP.Compatible.Employee(manageModel.ManagerId).PositionDescription;
                    ZJLCell4.InnerHtml = manageModel.ManagerAmount.ToString("#,##0.00");
                    string trId1 = ZJLRow.Attributes["id"] = "tr_" + manageModel.ManagerId + "_ZJLSP";
                    if (manageModel.ManagerId.ToString() == ESP.Configuration.ConfigurationManager.SafeAppSettings["DavidZhangID"].Trim())
                        ZJLCell5.InnerHtml = ESP.Finance.Utility.auditorType.operationAudit_Type_Names[ESP.Finance.Utility.auditorType.operationAudit_Type_CEO];
                    else
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
                }
            }






            //10W CEO审批
            if (manageModel.ManagerId == CurrentUserID || model.PreFee > manageModel.ManagerAmount)
            {
                ESP.Compatible.Employee empCEO = new ESP.Compatible.Employee(manageModel.CEOId);

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

                CEOCell1.InnerHtml = rowCount.ToString();
                rowCount++;
                CEOCell2.InnerHtml = empCEO.Name;
                CEOCell3.InnerHtml = empCEO.PositionDescription;

                if (empCEO.IntID == manageModel.RiskControlAccounter)
                    CEOCell5.InnerHtml = ESP.Finance.Utility.auditorType.operationAudit_Type_Names[ESP.Finance.Utility.auditorType.operationAudit_Type_RiskControl];
                else
                {
                    CEOCell4.InnerHtml = manageModel.CEOAmount.ToString("#,##0.00") + "以上";
                    CEOCell5.InnerHtml = ESP.Finance.Utility.auditorType.operationAudit_Type_Names[ESP.Finance.Utility.auditorType.operationAudit_Type_CEO];
                }
                CEOCell6.InnerHtml = "&nbsp;";

                CEORow.Attributes["id"] = "tr_" + empCEO.IntID.ToString() + "_CEO";

                hidCEO.Value = empCEO.IntID.ToString();
                CEORow.Cells.Add(CEOCell1);
                CEORow.Cells.Add(CEOCell2);
                CEORow.Cells.Add(CEOCell3);
                CEORow.Cells.Add(CEOCell4);
                CEORow.Cells.Add(CEOCell5);
                CEORow.Cells.Add(CEOCell6);
                CEORow.Attributes["class"] = "td";
                tab.Rows.Add(CEORow);

            }

            #endregion


            #region 是总监
            if (CurrentUserID == manageModel.DirectorId)
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
                hidZJFJ.Value = "";
                hidZJLSP.Value = "";
                hidCEO.Value = "";
                rowCount = 1;

                if (manageModel != null && CurrentUserID != manageModel.ManagerId)
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

                    ZJLCell1.InnerHtml = rowCount.ToString();
                    rowCount++;
                    ZJLCell2.InnerHtml = manageModel.ManagerName;
                    ZJLCell3.InnerHtml = new ESP.Compatible.Employee(manageModel.ManagerId).PositionDescription;
                    ZJLCell4.InnerHtml = manageModel.ManagerAmount.ToString("#,##0.00");
                    string trId1 = ZJLRow.Attributes["id"] = "tr_" + manageModel.ManagerId + "_ZJLSP";
                    if (manageModel.ManagerId.ToString() == ESP.Configuration.ConfigurationManager.SafeAppSettings["DavidZhangID"].Trim())
                        ZJLCell5.InnerHtml = ESP.Finance.Utility.auditorType.operationAudit_Type_Names[ESP.Finance.Utility.auditorType.operationAudit_Type_CEO];
                    else
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
                }

                if (model.PreFee > manageModel.ManagerAmount || CurrentUserID == manageModel.ManagerId)
                {
                    //CEO
                    ESP.Compatible.Employee ceoEmp = new ESP.Compatible.Employee(manageModel.CEOId);
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

                    CEOCell1.InnerHtml = rowCount.ToString();
                    rowCount++;
                    CEOCell2.InnerHtml = ceoEmp.Name;
                    CEOCell3.InnerHtml = ceoEmp.PositionDescription;

                    if (manageModel.CEOId == manageModel.RiskControlAccounter)
                        CEOCell5.InnerHtml = ESP.Finance.Utility.auditorType.operationAudit_Type_Names[ESP.Finance.Utility.auditorType.operationAudit_Type_RiskControl];
                    else
                    {
                        CEOCell4.InnerHtml = manageModel.CEOAmount.ToString("#,##0.00") + "以上";
                        CEOCell5.InnerHtml = ESP.Finance.Utility.auditorType.operationAudit_Type_Names[ESP.Finance.Utility.auditorType.operationAudit_Type_CEO];
                    }
                    CEOCell6.InnerHtml = "<a href=\"\" onclick=\"openEmployee('CEOSP');return false;\">添加</a>&nbsp;";


                    CEORow.Attributes["id"] = "tr_" + ceoEmp.SysID + "_CEO";

                    hidCEO.Value = ceoEmp.SysID;
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
            #endregion
            #region 是总经理
            if (CurrentUserID == manageModel.ManagerId || (selfManageModel != null && CurrentUserID == selfManageModel.ManagerId))
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
                hidZJFJ.Value = "";
                hidZJLSP.Value = "";
                hidCEO.Value = "";
                rowCount = 1;

                //CEO
                ESP.Compatible.Employee ceoEmp = new ESP.Compatible.Employee(manageModel.CEOId);
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

                CEOCell1.InnerHtml = rowCount.ToString();
                rowCount++;
                CEOCell2.InnerHtml = ceoEmp.Name;
                CEOCell3.InnerHtml = ceoEmp.PositionDescription;

                if (manageModel.CEOId == manageModel.RiskControlAccounter)
                    CEOCell5.InnerHtml = ESP.Finance.Utility.auditorType.operationAudit_Type_Names[ESP.Finance.Utility.auditorType.operationAudit_Type_RiskControl];
                else
                {
                    CEOCell4.InnerHtml = manageModel.CEOAmount.ToString("#,##0.00") + "以上";
                    CEOCell5.InnerHtml = ESP.Finance.Utility.auditorType.operationAudit_Type_Names[ESP.Finance.Utility.auditorType.operationAudit_Type_CEO];
                }
                CEOCell6.InnerHtml = "&nbsp;";

                CEORow.Attributes["id"] = "tr_" + ceoEmp.SysID + "_CEO";

                hidZJSP.Value = ceoEmp.SysID;
                CEORow.Cells.Add(CEOCell1);
                CEORow.Cells.Add(CEOCell2);
                CEORow.Cells.Add(CEOCell3);
                CEORow.Cells.Add(CEOCell4);
                CEORow.Cells.Add(CEOCell5);
                CEORow.Cells.Add(CEOCell6);
                CEORow.Attributes["class"] = "td";
                tab.Rows.Add(CEORow);
            }
            #endregion
        }
        #endregion

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


    /// <summary>
    /// 获得北上广财务部类别
    /// </summary>
    /// <param name="BSGDeps"></param>
    /// <param name="deps"></param>
    /// <returns></returns>
    protected String GetFinanceType(List<ESP.Framework.Entity.DepartmentInfo> BSGDeps, int deps)
    {
        string financeType = "";

        foreach (ESP.Framework.Entity.DepartmentInfo dep in BSGDeps)
        {
            if ("北京财务部".Equals(dep.DepartmentName))
            {
                if (deps == dep.DepartmentID)
                {
                    financeType = "BJ";
                }
            }
            else if ("上海财务部".Equals(dep.DepartmentName))
            {
                if (deps == dep.DepartmentID)
                {
                    financeType = "SH";
                }
            }
            else if ("广州财务部".Equals(dep.DepartmentName))
            {
                if (deps == dep.DepartmentID)
                {
                    financeType = "GZ";
                }
            }
        }
        if (string.IsNullOrEmpty(financeType))
        {
            financeType = "Other";
        }
        return financeType;
    }

    /// <summary>
    /// 提交 启动工作流
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnCommit_Click(object sender, EventArgs e)
    {
        if (returnModel.PreFee.Value <= 0)
        {
            Page.ClientScript.RegisterStartupScript(typeof(string), "", "alert('预计报销金额为0，无法提交通过，请检查！');window.location='CashExpenseAccountEdit.aspx?id=" + returnId + "';", true);
            return;
        }
        if (returnModel.ProjectID != 0)
        {
            ESP.Finance.Entity.ProjectInfo projectModel = ESP.Finance.BusinessLogic.ProjectManager.GetModelWithOutDetailList(returnModel.ProjectID.Value);
            if (projectModel.Status != (int)ESP.Finance.Utility.Status.FinanceAuditComplete)
            {
                Page.ClientScript.RegisterStartupScript(typeof(string), "", "alert('项目号变更中或已经关闭，无法提交通过，请检查！');window.location='CashExpenseAccountEdit.aspx?id=" + returnId + "';", true);
                return;
            }
        }
        //ESP.Framework.Entity.DepartmentInfo deptParent = GetRootDepartmentID(Convert.ToInt32(CurrentUser.SysID));

        //保存并提交
        if (SaveOperationAuditor())
        {
            List<ESP.Finance.Entity.ExpenseAuditerListInfo> auditList = ESP.Finance.BusinessLogic.ExpenseAuditerListManager.GetList(" and ReturnID = " + returnId);

            //预审
            List<int> PreApproverList = new List<int>();
            //项目负责人
            List<int> PorjectManagerList = new List<int>();
            //总监
            List<int> MajorList = new List<int>();
            //总经理
            List<int> GeneralManagerList = new List<int>();
            //财务总监
            List<int> CWZJList = new List<int>();
            //CEO
            List<int> CEOList = new List<int>();
            //FA
            List<int> FAList = new List<int>();
            //财务一级
            List<int> Finance1List = new List<int>();


            if (auditList.Count > 0)
            {

                //设置工作流需要的各级审批人列表
                foreach (ESP.Finance.Entity.ExpenseAuditerListInfo auditer in auditList)
                {
                    if (auditer.AuditType.Value == ESP.Finance.Utility.auditorType.operationAudit_Type_YS)
                    {
                        PreApproverList.Add(auditer.Auditer.Value);
                    }
                    if (auditer.AuditType.Value == ESP.Finance.Utility.auditorType.operationAudit_Type_XMFZ)
                    {
                        PorjectManagerList.Add(auditer.Auditer.Value);
                    }
                    else if (auditer.AuditType.Value == ESP.Finance.Utility.auditorType.operationAudit_Type_ZJSP || auditer.AuditType.Value == ESP.Finance.Utility.auditorType.operationAudit_Type_ZJFJ)
                    {
                        MajorList.Add(auditer.Auditer.Value);
                    }
                    else if (auditer.AuditType.Value == ESP.Finance.Utility.auditorType.operationAudit_Type_ZJLSP || auditer.AuditType.Value == ESP.Finance.Utility.auditorType.operationAudit_Type_ZJLFJ)
                    {
                        GeneralManagerList.Add(auditer.Auditer.Value);
                    }
                    else if (auditer.AuditType.Value == ESP.Finance.Utility.auditorType.operationAudit_Type_FinancialMajor)
                    {
                        CWZJList.Add(auditer.Auditer.Value);
                    }
                    else if (auditer.AuditType.Value == ESP.Finance.Utility.auditorType.operationAudit_Type_CEO)
                    {
                        CEOList.Add(auditer.Auditer.Value);
                    }
                    else if (auditer.AuditType.Value == ESP.Finance.Utility.auditorType.operationAudit_Type_FA)
                    {
                        FAList.Add(auditer.Auditer.Value);
                    }
                    else if (auditer.AuditType.Value == ESP.Finance.Utility.auditorType.operationAudit_Type_Financial)
                    {
                        Finance1List.Add(auditer.Auditer.Value);
                    }
                }

                //设置审核日志记录
                //ESP.Finance.Entity.AuditLogInfo logModel = new ESP.Finance.Entity.AuditLogInfo();
                ESP.Finance.Entity.ExpenseAuditDetailInfo logModel = new ESP.Finance.Entity.ExpenseAuditDetailInfo();
                logModel.AuditeDate = DateTime.Now;
                logModel.AuditorEmployeeName = CurrentUser.Name;
                logModel.AuditorUserID = Convert.ToInt32(CurrentUser.SysID);
                logModel.AuditorUserCode = CurrentUser.ID;
                logModel.AuditorUserName = CurrentUser.ITCode;
                logModel.AuditType = 0;
                logModel.ExpenseAuditID = returnId;
                logModel.AuditeStatus = (int)ESP.Finance.Utility.PaymentStatus.Submit;
                logModel.Suggestion = "";

                //设置工作流委托方法的参数
                Dictionary<string, object> prarms = new Dictionary<string, object>() 
                    { 
                        { "EntID", returnId } ,
                        { "ReturnStatus", (int)ESP.Finance.Utility.PaymentStatus.Submit } ,
                        { "SubmitDate", DateTime.Now } ,
                        { "LogModel", logModel }
                    };

                //根据部门获取部门总经理
                int deptId = 0;
                deptId = returnModel.DepartmentID.Value;

                //ESP.Framework.Entity.OperationAuditManageInfo manageModel = ESP.Framework.BusinessLogic.OperationAuditManageManager.GetModelByUserId(returnModel.RequestorID.Value);

                ESP.Framework.Entity.OperationAuditManageInfo manageModel = null;

                if (returnModel.ProjectID != null && returnModel.ProjectID.Value != 0)
                {
                    manageModel = ESP.Framework.BusinessLogic.OperationAuditManageManager.GetModelByProjectId(returnModel.ProjectID.Value);
                }
                if (manageModel == null)
                    manageModel = ESP.Framework.BusinessLogic.OperationAuditManageManager.GetModelByUserId(returnModel.RequestorID.Value); ;

                if (manageModel == null)
                    manageModel = ESP.Framework.BusinessLogic.OperationAuditManageManager.GetModelByDepId(deptId);

                //如果是GM项目，不需要FA审批
                bool IsGMProject = false;

                //判断是否项目负责人 和 是否项目总监
                bool isProjectManager = false;
                bool isMajor = false;
                bool isGeneralManager = false;

                //是否是上每财务人员提交
                bool isFinanceShanghai = false;
                //是否是重庆财务人员提交
                bool isFinanceChongqing = false;
                //是否是北京财务人员提交
                bool isFinanceBeijing = false;
                //是否是财务总监（Eddy）提交
                bool isFinanceMajor = false;

                if (returnModel.ProjectCode.IndexOf("GM*") >= 0 || returnModel.ProjectCode.IndexOf("*GM") >= 0 || returnModel.ProjectCode.IndexOf(ESP.Finance.Configuration.ConfigurationManager.ShunYaShortEN) >= 0)
                {
                    IsGMProject = true;
                }
                else
                {
                    if (returnModel.RequestorID.Value == (supportModel == null ? projectModel.ApplicantUserID : supportModel.LeaderUserID))
                    {
                        isProjectManager = true;
                    }
                    //是否总监
                    //if (manageModel.DirectorId == returnModel.RequestorID.Value || (selfManageModel != null && selfManageModel.DirectorId == returnModel.RequestorID.Value))
                    if (manageModel.DirectorId == returnModel.RequestorID.Value)
                    {
                        isMajor = true;
                    }
                    //是否总经理
                    if (manageModel.ManagerId == returnModel.RequestorID.Value)
                    {
                        isGeneralManager = true;
                    }
                }

                //判断总金额是否大于10000
                bool isGT_100000 = false;
                bool isGT_50000 = false;
                bool isWithoutEddyAudit = false;
                bool isBeijingDept = false;

                if (BeiJingBranch.IndexOf(returnModel.BranchCode.ToLower()) >= 0)
                {
                    isBeijingDept = true;
                }
                //大于1W是总经理审批
                if (returnModel.PreFee > manageModel.DirectorAmount || manageModel.DirectorId == returnModel.RequestorID.Value)
                {
                    isGT_100000 = true;
                }
                //大于10W是CEO审批，金额幅度调整，所以参数命名与实际不符
                if (returnModel.PreFee > manageModel.ManagerAmount)
                {
                    isGT_50000 = true;
                }


                //判断单笔餐费是否大于2000 判断报销单中单笔金额是否有大于2000的
                bool isGT_2000 = ESP.Finance.BusinessLogic.ExpenseAccountManager.ExpenseMoneyGreaterThan2000(returnModel.ReturnID);

                //判断是否外出人员
                bool isOutEmployee = false;
                isOutEmployee = new ESP.Administrative.BusinessLogic.ClientUsersManager().checkIsClientUser(UserInfo.UserID);

                //验证项目号合法性
                if (returnModel.ProjectID != null && returnModel.ProjectID.Value != 0)
                {
                    if (projectModel.Status != (int)ESP.Finance.Utility.Status.FinanceAuditComplete && !IsCommitDateLessCloseDate(projectModel, returnModel))
                    {
                        Page.ClientScript.RegisterStartupScript(typeof(string), "", "alert('" + projectModel.ProjectCode + "项目号不可用，无法提交通过，请检查！');window.location='ExpenseAccountEdit.aspx?id=" + returnId + "';", true);
                        return;
                    }
                }

                //是否Hover
                bool isHover = false;
   
                //是否行政人员
                bool isAdministrative = false;
                if (ESP.Configuration.ConfigurationManager.SafeAppSettings["AdministrativeIDs"].IndexOf("," + deptId.ToString() + ",") >= 0)
                {
                        isAdministrative = true;
                }

                if (validMoney(returnModel))
                {
                    if (returnModel.ReturnType == (int)ESP.Purchase.Common.PRTYpe.PN_ExpenseAccount || returnModel.ReturnType == (int)ESP.Purchase.Common.PRTYpe.PN_ExpenseAccountThirdParty || returnModel.ReturnType == (int)ESP.Purchase.Common.PRTYpe.PN_ExpenseAccountMedia)
                    {
                        //启动工作流
                        Guid instanceId = ESP.Workflow.WorkflowManager.StartWorkflow("~/Workflows/Reimbursement.xpdl", "Reimbursement", int.Parse(CurrentUser.SysID),
                        new Dictionary<string, object>() 
                            { 
                                { "PreApproverId" , PreApproverList.ToArray() },
                                { "PorjectManagerId" , PorjectManagerList.ToArray() },
                                { "MajorId", MajorList.ToArray() } ,
                                { "GeneralManagerId", GeneralManagerList.ToArray() } ,
                                { "FinanceMajorId" , CWZJList.ToArray() },
                                { "CEOId" , CEOList.ToArray() },
                                { "FAId", FAList.ToArray() } ,
                                { "Finance1Id", Finance1List.ToArray() } ,
                                { "EntityId", returnId },
                                { "IsProjectManager" , isProjectManager},
                                { "IsMajor" , isMajor},
                                { "IsGM" , IsGMProject},
                                { "IsGT_10000" , isGT_100000},
                                { "IsGT_50000" , isGT_50000},
                                { "IsGT_2000" , isGT_2000},
                                { "IsFinanceShanghai" , isFinanceShanghai},
                                { "IsFinanceChongqing" , isFinanceChongqing},
                                { "IsFinanceBeijing" , isFinanceBeijing},
                                { "IsFinanceMajor" , isFinanceMajor},
                                { "IsHover" , isHover},
                                { "IsGeneralManager" , isGeneralManager},
                                { "IsAdministration" , isAdministrative},
                                {"ReturnType",returnModel.ReturnType},
                                {"IsFinance1W",isWithoutEddyAudit},
                                {"IsBeijingDept",isBeijingDept}
                            }, new Action<object>(ESP.Finance.BusinessLogic.ExpenseAccountManager.UpdateStatus), prarms);
                    }
                    else// if (returnModel.ReturnType == (int)ESP.Purchase.Common.PRTYpe.PN_ExpenseAccountCashBorrow)
                    {
                        //启动工作流
                        Guid instanceId = ESP.Workflow.WorkflowManager.StartWorkflow("~/Workflows/Reimbursement.xpdl", "Loan", int.Parse(CurrentUser.SysID),
                        new Dictionary<string, object>() 
                            { 
                                { "PreApproverId" , PreApproverList.ToArray() },
                                { "PorjectManagerId" , PorjectManagerList.ToArray() },
                                { "MajorId", MajorList.ToArray() } ,
                                { "GeneralManagerId", GeneralManagerList.ToArray() } ,
                                { "FinanceMajorId" , CWZJList.ToArray() },
                                { "CEOId" , CEOList.ToArray() },
                                { "FAId", FAList.ToArray() } ,
                                { "Finance1Id", Finance1List.ToArray() } ,
                                { "EntityId", returnId },
                                { "IsProjectManager" , isProjectManager},
                                { "IsMajor" , isMajor},
                                { "IsGM" , IsGMProject},
                                { "IsGT_10000" , isGT_100000},
                                { "IsGT_50000" , isGT_50000},
                                { "IsGT_2000" , isGT_2000},
                                { "IsFinanceShanghai" , isFinanceShanghai},
                                { "IsFinanceChongqing" , isFinanceChongqing},
                                { "IsFinanceBeijing" , isFinanceBeijing},
                                { "IsFinanceMajor" , isFinanceMajor},
                                { "IsHover" , isHover},
                                { "IsGeneralManager" , isGeneralManager},
                                { "IsAdministration" , isAdministrative},
                                {"ReturnType",returnModel.ReturnType},
                                {"IsFinance1W",isWithoutEddyAudit},
                                {"IsBeijingDept",isBeijingDept}
                            }, new Action<object>(ESP.Finance.BusinessLogic.ExpenseAccountManager.UpdateStatus), prarms);
                    }

                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(typeof(string), "", "alert('项目成本金额不足，无法提交通过，请检查！');window.location='CashExpenseAccountEdit.aspx?id=" + returnId + "';", true);
                    return;
                }
                Page.ClientScript.RegisterStartupScript(typeof(string), "", "alert('已成功设置报销业务审批人并提交成功！');window.location='/Edit/OOPTabEdit.aspx?ReturnType=" + returnModel.ReturnType + "';", true);
                return;
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(typeof(string), "", "alert('设置报销业务审核人失败！未设置审核人！');window.location='CashExpenseAccountEdit.aspx?id=" + returnId + "';", true);
                return;
            }
        }
        else
        {
            Page.ClientScript.RegisterStartupScript(typeof(string), "", "alert('设置报销业务审核人失败！');window.location='CashExpenseAccountEdit.aspx?id=" + returnId + "';", true);
            return;
        }
    }

    private bool IsCommitDateLessCloseDate(ESP.Finance.Entity.ProjectInfo projectModel, ESP.Finance.Entity.ReturnInfo returnModel)
    {
        if (projectModel.Status == (int)ESP.Finance.Utility.Status.ProjectPreClose)
        {
            IList<ESP.Finance.Entity.TimingLogInfo> closeList = ESP.Finance.BusinessLogic.TimingLogManager.GetList(" projectid=0 and ','+remark+',' like '%," + projectModel.ProjectId.ToString() + ",%'");
            if (returnModel.CommitDate != null && (closeList != null && closeList.Count > 0))
            {
                if (returnModel.CommitDate.Value <= closeList[0].Time)
                {
                    if (closeList[0].Time.AddMonths(1) < DateTime.Now)//项目关闭不超过一个月
                    {
                        return false;
                    }
                    else
                        return true;
                }
                else
                    return false;
            }
            else
                return false;
        }
        else
        {
            return true;
        }
    }

    /// <summary>
    /// 返回按钮
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("CashExpenseAccountEdit.aspx?id=" + returnId);
    }

    /// <summary>
    /// 保存按钮
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSave_Click(object sender, EventArgs e)
    {

        if (SaveOperationAuditor())
        {
            Page.ClientScript.RegisterStartupScript(typeof(string), "", "alert('保存业务审批人成功！');window.location='CashExpenseAccountEdit.aspx?id=" + returnId + "';", true);
            return;
        }
        else
        {
            Page.ClientScript.RegisterStartupScript(typeof(string), "", "alert('保存业务审批人失败！');window.location='CashExpenseAccountEdit.aspx?id=" + returnId + "';", true);
            return;
        }
    }

    /// <summary>
    /// 保存业务审批人
    /// </summary>
    /// <returns></returns>
    private bool SaveOperationAuditor()
    {
        string userList = ",";
        List<ESP.Finance.Entity.ExpenseAuditerListInfo> operationList = new List<ESP.Finance.Entity.ExpenseAuditerListInfo>();

        if (hidYS.Value != "" && hidYS.Value.TrimEnd(',') != "")
        {

            string[] YSIds = hidYS.Value.TrimEnd(',').Split(',');
            for (int i = 0; i < YSIds.Length; i++)
            {
                if (YSIds[i].Trim() != "")
                {
                    if (userList.IndexOf("," + YSIds[i] + ",") < 0)
                    {
                        ESP.Compatible.Employee emp = new ESP.Compatible.Employee(Convert.ToInt32(YSIds[i]));
                        if (emp == null || emp.SysID == null)
                        { continue; }
                        ESP.Finance.Entity.ExpenseAuditerListInfo model = new ESP.Finance.Entity.ExpenseAuditerListInfo();

                        model.ReturnID = returnId;

                        model.Auditer = int.Parse(emp.SysID);
                        model.AuditerName = emp.Name;

                        if (returnModel.ProjectCode.IndexOf("GM*") >= 0 || returnModel.ProjectCode.IndexOf("*GM") >= 0 || returnModel.ProjectCode.IndexOf(ESP.Finance.Configuration.ConfigurationManager.ShunYaShortEN) >= 0)
                        {
                            model.AuditType = ESP.Finance.Utility.auditorType.operationAudit_Type_YS;
                            operationList.Add(model);
                        }
                        else
                        {
                            if ((supportModel == null ? projectModel.ApplicantUserID : supportModel.LeaderUserID) == int.Parse(emp.SysID))
                            {
                                model.AuditType = ESP.Finance.Utility.auditorType.operationAudit_Type_XMFZ;
                                operationList.Add(model);
                            }
                            else
                            {
                                model.AuditType = ESP.Finance.Utility.auditorType.operationAudit_Type_YS;
                                operationList.Add(model);
                            }
                        }
                        userList += YSIds[i] + ",";
                    }
                }

            }
        }
        if (hidZJSP.Value != "" && hidZJSP.Value.TrimEnd(',') != "" && hidZJLSP.Value.TrimEnd(',') != hidZJSP.Value.TrimEnd(','))
        {
            if (userList.IndexOf("," + hidZJSP.Value.TrimEnd(',') + ",") < 0)
            {
                ESP.Compatible.Employee emp = new ESP.Compatible.Employee(Convert.ToInt32(hidZJSP.Value.TrimEnd(',')));
                ESP.Finance.Entity.ExpenseAuditerListInfo model = new ESP.Finance.Entity.ExpenseAuditerListInfo();
                model.ReturnID = returnId;
                model.AuditType = ESP.Finance.Utility.auditorType.operationAudit_Type_ZJSP;

                model.Auditer = int.Parse(emp.SysID);
                model.AuditerName = emp.Name;
                operationList.Add(model);
                userList += hidZJSP.Value.TrimEnd(',') + ",";
            }
        }
        if (hidZJFJ.Value != "" && hidZJFJ.Value.TrimEnd(',') != "")
        {
            string[] ZJFJIds = hidZJFJ.Value.TrimEnd(',').Split(',');
            for (int i = 0; i < ZJFJIds.Length; i++)
            {
                if (ZJFJIds[i].Trim() != "")
                {
                    if (userList.IndexOf("," + ZJFJIds[i].Trim() + ",") < 0)
                    {
                        ESP.Compatible.Employee emp = new ESP.Compatible.Employee(Convert.ToInt32(ZJFJIds[i]));
                        if (emp == null || emp.SysID == null)
                        { continue; }
                        ESP.Finance.Entity.ExpenseAuditerListInfo model = new ESP.Finance.Entity.ExpenseAuditerListInfo();
                        model.ReturnID = returnId;
                        model.AuditType = ESP.Finance.Utility.auditorType.operationAudit_Type_ZJFJ;

                        model.Auditer = int.Parse(emp.SysID);
                        model.AuditerName = emp.Name;
                        operationList.Add(model);
                        userList += ZJFJIds[i].Trim() + ",";
                    }
                }
            }
        }
        if (hidZJLSP.Value != "" && hidZJLSP.Value.TrimEnd(',') != "")
        {
            if (userList.IndexOf("," + hidZJLSP.Value.TrimEnd(',') + ",") < 0)
            {
                ESP.Compatible.Employee emp = new ESP.Compatible.Employee(Convert.ToInt32(hidZJLSP.Value.TrimEnd(',')));
                ESP.Finance.Entity.ExpenseAuditerListInfo model = new ESP.Finance.Entity.ExpenseAuditerListInfo();
                model.ReturnID = returnId;
                model.AuditType = ESP.Finance.Utility.auditorType.operationAudit_Type_ZJLSP;

                model.Auditer = int.Parse(emp.SysID);
                model.AuditerName = emp.Name;
                operationList.Add(model);
                userList += hidZJLSP.Value.TrimEnd(',') + ",";
            }
        }

        if (hidZJLFJ.Value != "" && hidZJLFJ.Value.TrimEnd(',') != "")
        {
            string[] ZJLFJIds = hidZJLFJ.Value.TrimEnd(',').Split(',');
            for (int i = 0; i < ZJLFJIds.Length; i++)
            {
                if (ZJLFJIds[i].Trim() != "")
                {
                    if (userList.IndexOf("," + ZJLFJIds[i] + ",") < 0)
                    {
                        ESP.Compatible.Employee emp = new ESP.Compatible.Employee(Convert.ToInt32(ZJLFJIds[i]));
                        if (emp == null || emp.SysID == null)
                        { continue; }
                        ESP.Finance.Entity.ExpenseAuditerListInfo model = new ESP.Finance.Entity.ExpenseAuditerListInfo();
                        model.ReturnID = returnId;
                        model.AuditType = ESP.Finance.Utility.auditorType.operationAudit_Type_ZJLFJ;

                        model.Auditer = int.Parse(emp.SysID);
                        model.AuditerName = emp.Name;
                        operationList.Add(model);
                        userList += ZJLFJIds[i] + ",";
                    }
                }
            }
        }

        if (hidCWZJ.Value != "" && hidCWZJ.Value.TrimEnd(',') != "")
        {
            ESP.Compatible.Employee emp = new ESP.Compatible.Employee(Convert.ToInt32(hidCWZJ.Value.TrimEnd(',')));
            ESP.Finance.Entity.ExpenseAuditerListInfo model = new ESP.Finance.Entity.ExpenseAuditerListInfo();
            model.ReturnID = returnId;
            model.AuditType = ESP.Finance.Utility.auditorType.operationAudit_Type_FinancialMajor;

            model.Auditer = int.Parse(emp.SysID);
            model.AuditerName = emp.Name;
            operationList.Add(model);
            userList += hidCWZJ.Value.TrimEnd(',') + ",";
        }

        if (hidCEO.Value != "" && hidCEO.Value.TrimEnd(',') != "")
        {
            if (userList.IndexOf("," + hidCEO.Value.TrimEnd(',') + ",") < 0)
            {
                ESP.Compatible.Employee emp = new ESP.Compatible.Employee(Convert.ToInt32(hidCEO.Value.TrimEnd(',')));
                ESP.Finance.Entity.ExpenseAuditerListInfo model = new ESP.Finance.Entity.ExpenseAuditerListInfo();
                model.ReturnID = returnId;
                model.AuditType = ESP.Finance.Utility.auditorType.operationAudit_Type_CEO;

                model.Auditer = int.Parse(emp.SysID);
                model.AuditerName = emp.Name;
                operationList.Add(model);
                userList += hidCEO.Value.TrimEnd(',') + ",";
            }
        }

        if (hidFA.Value != "" && hidFA.Value.TrimEnd(',') != "")
        {
            ESP.Compatible.Employee emp = new ESP.Compatible.Employee(Convert.ToInt32(hidFA.Value.TrimEnd(',')));
            ESP.Finance.Entity.ExpenseAuditerListInfo model = new ESP.Finance.Entity.ExpenseAuditerListInfo();
            model.ReturnID = returnId;
            model.AuditType = ESP.Finance.Utility.auditorType.operationAudit_Type_FA;

            model.Auditer = int.Parse(emp.SysID);
            model.AuditerName = emp.Name;
            operationList.Add(model);
        }

        //finace auditor

        ESP.Finance.Entity.BranchInfo branchModel = ESP.Finance.BusinessLogic.BranchManager.GetModelByCode(returnModel.ProjectCode.Substring(0, 1));
        int FirstFinanceID = branchModel.FirstFinanceID;
        //增加部门判断,N的不同部门对应不同的第一级财务审批人
        ESP.Finance.Entity.BranchDeptInfo branchdept = ESP.Finance.BusinessLogic.BranchDeptManager.GetModel(branchModel.BranchID, returnModel.DepartmentID.Value);
        if (branchdept != null)
            FirstFinanceID = branchdept.FianceFirstAuditorID;

        //媒体预付第一级审核人
        if (returnModel.ReturnType == (int)ESP.Purchase.Common.PRTYpe.PN_ExpenseAccountMedia)
            FirstFinanceID = branchModel.FinalAccounter;

        ESP.Compatible.Employee financeEmp = new ESP.Compatible.Employee(FirstFinanceID);

        ESP.Finance.Entity.ExpenseAuditerListInfo FinanceModel = new ESP.Finance.Entity.ExpenseAuditerListInfo();
        FinanceModel.ReturnID = returnId;
        FinanceModel.AuditType = ESP.Finance.Utility.auditorType.operationAudit_Type_Financial;

        FinanceModel.Auditer = int.Parse(financeEmp.SysID);
        FinanceModel.AuditerName = financeEmp.Name;
        operationList.Add(FinanceModel);

        return ESP.Finance.BusinessLogic.ExpenseAuditerListManager.AddAuditers(operationList, returnId);
    }

    /// <summary>
    /// 采购物料类别是否包含北京采购部专属类别
    /// </summary>
    /// <returns></returns>
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
}
