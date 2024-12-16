using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ESP.Finance.Utility;
using ESP.Finance.Entity;
using ESP.Finance.BusinessLogic;
namespace FinanceWeb.CostView
{
    public partial class SupSinglePrjView : System.Web.UI.Page
    {
        ESP.Finance.Entity.SupporterInfo supporterModel = null;
        private IList<ESP.Purchase.Entity.GeneralInfo> PRList;
        private IList<ReturnInfo> ReturnList;
        private IList<ExpenseAccountDetailInfo> ExpenseDetails;
        private Dictionary<int, int> TypeMappings;
        private IList<ESP.Purchase.Entity.PaymentPeriodInfo> Periods;
        private IList<ESP.Purchase.Entity.MediaPREditHisInfo> MediaPREditHises;
        private List<ESP.Purchase.Entity.OrderInfo> Orders;
        private List<CostRecordInfo> ExpenseRecords;
        private List<CostRecordInfo> PRRecords;
        private IList<SupporterCostInfo> CostDetails;
        private IList<SupporterExpenseInfo> Expenses;

        Dictionary<int, decimal> CostMappings = new Dictionary<int, decimal>();
        Dictionary<int, decimal> ExpenseMappings = new Dictionary<int, decimal>();
        decimal TraficFee;
        decimal UsedCost;

        protected void Page_Load(object sender, EventArgs e)
        {
            Server.ScriptTimeout = 600;
            if (!IsPostBack)
            {
                BindData();
            }
        }

        private SupporterInfo GetSupporter()
        {
            int supporterId;
            if (int.TryParse(Request[RequestName.SupportID], out supporterId) && supporterId > 0)
            {
                return ESP.Finance.BusinessLogic.SupporterManager.GetModel(supporterId);
            }
            else
            {
                return null;
            }
        }


        protected string getUrl(string id, string prno)
        {
            return "<a href='" + ESP.Configuration.ConfigurationManager.SafeAppSettings["PurchaseServer"] + "\\Purchase\\Requisition\\OrderDetailTab.aspx?GeneralID=" + id + "'style='cursor: hand' target='_blank'>" + prno + "</a>";
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
        private void BindData()
        {
            supporterModel = GetSupporter();

            if (supporterModel == null || supporterModel.GroupID == null || supporterModel.GroupID.Value == 0)
                return;

            var typelvl2 = ESP.Purchase.BusinessLogic.TypeManager.GetListLvl2();
            typelvl2[0] = "OOP";
            typelvl2[-1] = "[未知]";

            PRList = ESP.Purchase.BusinessLogic.GeneralInfoManager.GetModelList(supporterModel.ProjectID, supporterModel.GroupID.Value);
            ReturnList = ESP.Finance.BusinessLogic.ReturnManager.GetList(supporterModel.ProjectID, supporterModel.GroupID.Value);
            ExpenseDetails = ESP.Finance.BusinessLogic.ExpenseAccountDetailManager.GetList(ReturnList.Select(x => x.ReturnID).ToArray());
            TypeMappings = ESP.Purchase.BusinessLogic.TypeManager.GetTypeMappings(/*projectModel.CostDetails.Select(x => x.CostTypeID ?? 0).ToArray()*/);
            Periods = ESP.Purchase.BusinessLogic.PaymentPeriodManager.GetList(PRList.Select(x => x.id).ToArray());
            MediaPREditHises = ESP.Purchase.BusinessLogic.MediaPREditHisManager.GetModelByOldPRIDs(PRList.Where(x => x.PRType == 1 || x.PRType == 6).Select(x => x.id).ToArray());
            Orders = ESP.Purchase.BusinessLogic.OrderInfoManager.GetListByGeneralIds(PRList.Select(x => x.id).ToArray());
            CostDetails = SupporterCostManager.GetList(supporterModel.SupportID, null, null);//成本明细列表
            Expenses = SupporterExpenseManager.GetList(supporterModel.SupportID, null);

            ExpenseRecords = new List<CostRecordInfo>();
            PRRecords = new List<CostRecordInfo>();


            foreach (var pr in PRList)
            {
                if (pr.PRType == (int)ESP.Purchase.Common.PRTYpe.PR_PriFA || pr.PRType == (int)ESP.Purchase.Common.PRTYpe.PR_MediaFA)
                    continue;

                decimal paid = 0;
                var orders = Orders.Where(x => x.general_id == pr.id);

                paid = ReturnList.Where(x => x.PRID == pr.id && x.ReturnType != 11 && x.ReturnStatus == 140).Sum(x => x.FactFee ?? 0);

                foreach (var o in orders)
                {
                    var costTypeId = o.producttype;
                    if (!TypeMappings.TryGetValue(costTypeId, out costTypeId)) costTypeId = 0;

                    if (o.FactTotal != 0)
                        AddValue(CostMappings, costTypeId, o.FactTotal);
                    else
                    {
                            AddValue(CostMappings, costTypeId, o.total);
                    }

                    CostRecordInfo detail = new CostRecordInfo()
                    {
                        PRID = pr.id,
                        PRNO = pr.PrNo,
                        SupplierName = pr.supplier_name,
                        Description = pr.project_descripttion,
                        Requestor = pr.requestorname,
                        GroupName = pr.requestor_group,
                        TypeID = costTypeId,
                        TypeName = typelvl2[costTypeId],
                        OrderTotal = o.total,
                        AppAmount = pr.totalprice,
                        PaidAmount = paid,
                        UnPaidAmount = pr.totalprice - paid,
                        CostPreAmount = CostDetails.Where(x => x.CostTypeID == costTypeId).Select(x => x.Amounts).FirstOrDefault()
                    };
                    PRRecords.Add(detail);
                }
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
                        preamount = Expenses.Where(x => x.Description == "OOP").Select(x => x.Expense ?? 0).FirstOrDefault();
                    else
                    {
                        ESP.Finance.Entity.SupporterCostInfo costmodel = CostDetails.Where(x => x.CostTypeID == typeid).FirstOrDefault();
                        preamount = costmodel == null ? 0 : costmodel.Amounts;
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


            this.Supporter.Supporter = this.supporterModel;
            this.Supporter.list = CostDetails;
            this.Supporter.expenselist = Expenses;
            this.Supporter.CostMappings = this.CostMappings;
            this.Supporter.ExpenseMappings = this.ExpenseMappings;
            this.Supporter.TraficFee = this.TraficFee;
            this.Supporter.PRRecords = this.PRRecords;
            this.Supporter.ExpenseRecords = this.ExpenseRecords;
            this.Supporter.ReturnList = this.ReturnList;
            this.Supporter.BindData();
            

            GridPR.DataSource = PRRecords.OrderBy(x => x.TypeID).ToList(); ;
            GridPR.DataBind();

            var g = Request.Form["ExpenseGroupBy"];
            if (!string.IsNullOrEmpty(g))
            {
                this.TabContainer1.ActiveTabIndex = 2;
            }
            if (g != "1") g = "0";
            raList.SelectedIndex = raList.Items.IndexOf(raList.Items.FindByValue(g));
            if (raList.SelectedValue == "0")
            {
                this.GridOOP.GroupBy = "TypeID desc";
                this.GridOOP.Levels[0].GroupHeadingClientTemplateId = "GroupByTemplate2";

                this.GridOOP.DataSource = ExpenseRecords.OrderBy(x => x.TypeID).ToList();
            }
            else
            {
                this.GridOOP.GroupBy = "PrNo desc";
                this.GridOOP.Levels[0].GroupHeadingClientTemplateId = "GroupByTemplate3";

                this.GridOOP.DataSource = ExpenseRecords.OrderByDescending(x => x.PRNO).ToList();
            }
            this.GridOOP.DataBind();
        }

        protected void btnReturn_Click(object sender, EventArgs e)
        {
            Response.Redirect("SupCostTotalView.aspx");
        }

        //protected void gvCost_RowDataBound(object sender, GridViewRowEventArgs e)
        //{
        //    if (e.Row.RowType == DataControlRowType.DataRow)
        //    {
        //        Label lblno = (Label)e.Row.FindControl("lblNo");
        //        lblno.Text = (e.Row.RowIndex + 1).ToString();
        //        ContractCostInfo item = (ContractCostInfo)e.Row.DataItem;
        //        Label lblCost = (Label)e.Row.FindControl("lblCost");
        //        if (lblCost != null && item.Cost != null)
        //        {
        //            lblCost.Text = Convert.ToDecimal(item.Cost).ToString("#,##0.00");
        //        }
        //    }
        //}
        //protected void gvSupporter_RowDataBound(object sender, GridViewRowEventArgs e)
        //{
        //    if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header)
        //    {
        //        e.Row.Cells[0].Visible = false;
        //        e.Row.Cells[1].Visible = false;
        //        e.Row.Cells[4].Visible = false;
        //        if (e.Row.RowType == DataControlRowType.DataRow)
        //        {
        //            ESP.Finance.Entity.SupporterInfo supporter = (ESP.Finance.Entity.SupporterInfo)e.Row.DataItem;
        //            Label lblNo = (Label)e.Row.FindControl("lblNo");
        //            lblNo.Text = (e.Row.RowIndex + 1).ToString();
        //            SupporterInfo item = (SupporterInfo)e.Row.DataItem;
        //            Label lblBudgetAllocation = (Label)e.Row.FindControl("lblBudgetAllocation");
        //            if (lblBudgetAllocation != null && item.BudgetAllocation != null)
        //            {
        //                lblBudgetAllocation.Text = Convert.ToDecimal(item.BudgetAllocation).ToString("#,##0.00");
        //            }
        //            //所有部门级联字符串拼接
        //            Label lblGroupName = (Label)e.Row.FindControl("lblGroupName");
        //            List<ESP.Framework.Entity.DepartmentInfo> depList = new List<ESP.Framework.Entity.DepartmentInfo>();
        //            depList = ESP.Framework.BusinessLogic.DepartmentManager.GetDepartmentListByID(supporter.GroupID.Value, depList);
        //            string groupname = string.Empty;
        //            foreach (ESP.Framework.Entity.DepartmentInfo dept in depList)
        //            {
        //                groupname += dept.DepartmentName + "-";
        //            }
        //            if (!string.IsNullOrEmpty(groupname))
        //                lblGroupName.Text = groupname.Substring(0, groupname.Length - 1);
        //        }
        //    }
        //    if (e.Row.RowType == DataControlRowType.DataRow)
        //    {
        //        Label lblName = (Label)e.Row.FindControl("lblName");
        //        lblName.Attributes.Add("onclick", "javascript:ShowMsg('" + ESP.Web.UI.PageBase.GetUserInfo(Convert.ToInt32(e.Row.Cells[4].Text)) + "');");

        //    }
        //}
        protected void gvExpense_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblno = (Label)e.Row.FindControl("lblNo");
                lblno.Text = (e.Row.RowIndex + 1).ToString();
                ProjectExpenseInfo item = (ProjectExpenseInfo)e.Row.DataItem;
                Label lblExpense = (Label)e.Row.FindControl("lblExpense");
                if (lblExpense != null && item.Expense != null)
                {
                    lblExpense.Text = Convert.ToDecimal(item.Expense).ToString("#,##0.00");
                }
            }
        }

        //protected void raList_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    int projectid=0;
        //   //ESP.Finance.Entity.ProjectInfo projectModel =null;
        //    ESP.Finance.Entity.SupporterInfo supporterModel=null;
        //    //     if (!string.IsNullOrEmpty(Request[RequestName.ProjectID]))
        //    //{
        //    //    projectid = Convert.ToInt32(Request[RequestName.ProjectID]);
        //    //    projectModel = ESP.Finance.BusinessLogic.ProjectManager.GetModelWithOutDetailList(projectid);
        //    //}
        //    if (!string.IsNullOrEmpty(Request[RequestName.SupportID]))
        //    {
        //        supporterModel = ESP.Finance.BusinessLogic.SupporterManager.GetModel(Convert.ToInt32(Request[RequestName.SupportID]));
        //    }
        //    if (raList.SelectedValue == "0")
        //    {
        //        string term = string.Format(" projectid={0} and departmentid={1} and recordType=2", supporterModel.ProjectID.ToString(), supporterModel.GroupID.Value.ToString());
        //        IList<ESP.Finance.Entity.CostRecordInfo> OOPList = ESP.Finance.BusinessLogic.CostRecordManager.GetList(term, new List<System.Data.SqlClient.SqlParameter>());
        //        var tmpList = OOPList.OrderBy(N => N.TypeID);
        //        GridOOP.DataSource = tmpList.ToList();
        //        GridOOP.DataBind();
        //        this.GridOOP.GroupBy = "TypeID desc";
        //        this.GridOOP.Levels[0].GroupHeadingClientTemplateId = "GroupByTemplate2";
        //        this.TabContainer1.ActiveTabIndex = 2;
        //    }
        //    else
        //    {
        //        string term = string.Format(" projectid={0} and departmentid={1} and recordType=2", supporterModel.ProjectID.ToString(), supporterModel.GroupID.Value.ToString());
        //        IList<ESP.Finance.Entity.CostRecordInfo> OOPList = ESP.Finance.BusinessLogic.CostRecordManager.GetList(term, new List<System.Data.SqlClient.SqlParameter>());
        //        var tmpList = OOPList.OrderBy(N => N.PRNO);
        //        GridOOP.DataSource = tmpList.ToList();
        //        GridOOP.DataBind();
        //        this.GridOOP.GroupBy = "PrNo desc";
        //        this.GridOOP.Levels[0].GroupHeadingClientTemplateId = "GroupByTemplate3";
        //        this.TabContainer1.ActiveTabIndex = 2;
        //    }
        //}
      
    }
}
