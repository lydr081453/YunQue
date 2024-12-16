using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using ESP.Finance.Entity;
using ESP.Finance.Utility;

namespace FinanceWeb.project
{
    public partial class ProjectCostView : ESP.Web.UI.PageBase
    {

        private ESP.Finance.Entity.ProjectInfo projectModel;
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
        protected void Page_Load(object sender, EventArgs e)
        {
            BindData();
            if (!IsPostBack)
            {
                BindCosts();
            }
        }

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

        private ProjectInfo GetProject()
        {
            int projectid;
            if (int.TryParse(Request[RequestName.ProjectID], out projectid) && projectid > 0)
            {
                return ESP.Finance.BusinessLogic.ProjectManager.GetModel(projectid);
            }
            else
            {
                return null;
            }
        }


        private void BindData()
        {
            projectModel = GetProject();

            if (projectModel == null || projectModel.GroupID == null || projectModel.GroupID.Value == 0)
                return;

            var typelvl2 = ESP.Purchase.BusinessLogic.TypeManager.GetListLvl2();
            typelvl2[0] = "OOP";
            typelvl2[-1] = "[未知]";

            PRList = ESP.Purchase.BusinessLogic.GeneralInfoManager.GetModelList(projectModel.ProjectId, projectModel.GroupID.Value);
            ReturnList = ESP.Finance.BusinessLogic.ReturnManager.GetList(projectModel.ProjectId, projectModel.GroupID.Value);
            ExpenseDetails = ESP.Finance.BusinessLogic.ExpenseAccountDetailManager.GetList(ReturnList.Select(x => x.ReturnID).ToArray());
            TypeMappings = ESP.Purchase.BusinessLogic.TypeManager.GetTypeMappings(/*projectModel.CostDetails.Select(x => x.CostTypeID ?? 0).ToArray()*/);
            Periods = ESP.Purchase.BusinessLogic.PaymentPeriodManager.GetList(PRList.Select(x => x.id).ToArray());
            MediaPREditHises = ESP.Purchase.BusinessLogic.MediaPREditHisManager.GetModelByOldPRIDs(PRList.Where(x => x.PRType == 1 || x.PRType == 6).Select(x => x.id).ToArray());
            Orders = ESP.Purchase.BusinessLogic.OrderInfoManager.GetListByGeneralIds(PRList.Select(x => x.id).ToArray());

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
                    CostPreAmount = projectModel.CostDetails.Where(x => x.CostTypeID == typeid).Select(x => x.Cost ?? 0).FirstOrDefault()
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
                    decimal preamount = 0;
                    if (typeid == 0)
                        preamount = projectModel.Expenses.Where(x => x.Description == "OOP").Select(x => x.Expense ?? 0).FirstOrDefault();
                    else
                    {
                        ContractCostInfo costModel = projectModel.CostDetails.Where(x => x.CostTypeID == typeid).FirstOrDefault();
                        preamount = costModel == null ? 0 : costModel.Cost.Value;
                    }
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
                        CostPreAmount = preamount,
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

            TraficFee = ReturnList.Where(x => x.ReturnType == 20).Sum(x => (x.FactFee ?? (x.PreFee ?? 0)));

            UsedCost = TraficFee + CostMappings.Sum(x => x.Value) + ExpenseMappings.Sum(x => x.Value);

            decimal costsum = projectModel.CostDetails.Sum(x => x.Cost) ?? 0;
            decimal expensesum = projectModel.Expenses.Sum(x => x.Expense) ?? 0;
            decimal total = costsum + expensesum;
            this.lblTotal.Text = "预算总计:" + total.ToString("#,##0.00");
            this.lblUsedTotal.Text = "已使用小计:" + UsedCost.ToString("#,##0.00");
        }


        private void BindCosts()
        {
            this.gvCost.DataSource = projectModel.CostDetails;
            this.gvCost.DataBind();
            this.gvExpense.DataSource = projectModel.Expenses;
            this.gvExpense.DataBind();
            if (this.gvCost.Rows.Count == 0 && this.gvExpense.Rows.Count == 0)
            {
                this.trGrid.Visible = false;
                this.trNoRecord.Visible = true;
            }
            else
            {
                this.trGrid.Visible = true;
                this.trNoRecord.Visible = false;
            }        
        }
        private void BindTotal()
        {
            this.trTotal.Visible = true;
            this.lblTotal.Text = "预算总计:";
            decimal total = projectModel.CostDetails.Sum(x => x.Cost) ?? 0 + projectModel.Expenses.Sum(x => x.Expense) ?? 0;
            if (total == 0)
            {
                this.trTotal.Visible = false;
            }
            this.lblTotal.Text = "预算总计:" + total.ToString("#,##0.00");
        }

        protected void gvCost_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblno = (Label)e.Row.FindControl("lblNo");
                lblno.Text = (e.Row.RowIndex + 1).ToString();
                ContractCostInfo item = (ContractCostInfo)e.Row.DataItem;
                Label lblCost = (Label)e.Row.FindControl("lblCost");
                Label lblUsedCost = (Label)e.Row.FindControl("lblUsedCost");
                if (lblCost != null && item.Cost != null)
                {
                    lblCost.Text = Convert.ToDecimal(item.Cost).ToString("#,##0.00");
                }

                decimal used = 0, used2 = 0;
                CostMappings.TryGetValue(item.CostTypeID.Value, out used);
                ExpenseMappings.TryGetValue(item.CostTypeID.Value, out used2);
                lblUsedCost.Text = (used + used2).ToString("#,##0.00");
            }
        }
        protected void gvExpense_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblno = (Label)e.Row.FindControl("lblNo");
                lblno.Text = (e.Row.RowIndex + 1).ToString();
                ProjectExpenseInfo item = (ProjectExpenseInfo)e.Row.DataItem;
                Label lblExpense = (Label)e.Row.FindControl("lblExpense");
                Label lblUsedCost = (Label)e.Row.FindControl("lblUsedCost");
                if (lblExpense != null && item.Expense != null)
                {
                    lblExpense.Text = Convert.ToDecimal(item.Expense).ToString("#,##0.00");
                }
                decimal used = 0M;
                if (item.Description == "OOP")
                {
                    ExpenseMappings.TryGetValue(0, out used);
                }
                else
                {
                    used = TraficFee;
                }
                lblUsedCost.Text = used.ToString("#,##0.00");
            }
        }

    }
}
