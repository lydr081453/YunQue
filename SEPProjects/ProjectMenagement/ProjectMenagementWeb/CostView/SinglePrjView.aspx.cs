using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ESP.Finance.Utility;
using ESP.Finance.Entity;
using ESP.ITIL.BusinessLogic;
using System.Collections;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using ESP.Finance.BusinessLogic;


namespace FinanceWeb.CostView
{
    public partial class SinglePrjView : System.Web.UI.Page
    {
        private ESP.Finance.Entity.ProjectInfo projectModel;
        private IList<ESP.Purchase.Entity.GeneralInfo> PRList;
        private IList<ReturnInfo> ReturnList;
        private IList<RefundInfo> RefundList;
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
            projectModel = GetProject();

            if (projectModel == null || projectModel.GroupID == null || projectModel.GroupID.Value == 0)
                return;

            var typelvl2 = ESP.Purchase.BusinessLogic.TypeManager.GetListLvl2();
            typelvl2[0] = "OOP";
            typelvl2[-1] = "[未知]";

            PRList = ESP.Purchase.BusinessLogic.GeneralInfoManager.GetModelList(projectModel.ProjectId, projectModel.GroupID.Value);
            ReturnList = ESP.Finance.BusinessLogic.ReturnManager.GetList(projectModel.ProjectId, projectModel.GroupID.Value);
            ExpenseDetails = ESP.Finance.BusinessLogic.ExpenseAccountDetailManager.GetList(ReturnList.Select(x => x.ReturnID).ToArray());
            RefundList = ESP.Finance.BusinessLogic.RefundManager.GetList(" ProjectId=" + projectModel.ProjectId + " and deptid=" + projectModel.GroupID);

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

                    paid = ReturnList.Where(x => x.PRID == pr.id && x.ReturnType != 11 && x.ReturnStatus == 140).Sum(x => x.FactFee ?? 0);
                    foreach (var o in orders)
                    {
                        var costTypeId = o.producttype;
                        if (!TypeMappings.TryGetValue(costTypeId, out costTypeId)) costTypeId = 0;

                        if (o.FactTotal != 0)
                            AddValue(CostMappings, costTypeId, o.FactTotal);
                        else
                        {
                            //if (paid > o.total)
                            //    AddValue(CostMappings, costTypeId, paid);
                            //else
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
                            OrderTotal=o.total,
                            AppAmount = pr.totalprice,
                            PaidAmount = paid,
                            UnPaidAmount = pr.totalprice - paid,
                            CostPreAmount = projectModel.CostDetails.Where(x => x.CostTypeID == costTypeId).Select(x => x.Cost ?? 0).FirstOrDefault()
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
                        preamount = projectModel.Expenses.Where(x => x.Description == "OOP").Select(x => x.Expense ?? 0).FirstOrDefault();
                    else
                    {
                        ESP.Finance.Entity.ContractCostInfo costmodel = projectModel.CostDetails.Where(x => x.CostTypeID == typeid).FirstOrDefault();
                        preamount = costmodel == null ? 0 : costmodel.Cost.Value;
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

            TraficFee = ReturnList.Where(x => x.ReturnType == 20 && x.ReturnStatus==140).Sum(x => (x.FactFee ?? (x.PreFee ?? 0)));

            UsedCost = TraficFee + CostMappings.Sum(x => x.Value) + ExpenseMappings.Sum(x => x.Value);
            //取退款申请释放成本
            decimal refundTotal = RefundManager.GetList(" projectId =" + projectModel.ProjectId + " and status not in(0,-1,1) and deptid=" + projectModel.GroupID).Sum(x => x.Amounts);
            UsedCost = UsedCost - refundTotal;



            InitPrepareInfo(projectModel);
            BindProjectInfo(projectModel);
            BindCosts(projectModel);
            if (projectModel.Supporters == null || projectModel.Supporters.Count == 0)
            {
                this.trNoSupporter.Visible = true;
                this.trSupporterGrid.Visible = false;
            }
            else
            {
                this.trNoSupporter.Visible = false;
                this.trSupporterGrid.Visible = true;

                this.gvSupporter.DataSource = projectModel.Supporters;
                this.gvSupporter.DataBind();
            }


            GridPR.DataSource = PRRecords.OrderBy(x => x.TypeID).ToList();
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

            bind_Consumption(projectModel.ProjectId);
            Bind_RebateRegistrationList(projectModel.ProjectId);
            bindRefund(projectModel.ProjectId);
        }

        private void InitPrepareInfo(ESP.Finance.Entity.ProjectInfo project)
        {
            this.lblBDProject.Text = project.BDProjectCode;
            this.lblBizDesc.Text = project.BusinessDescription;
            this.lblContactStatus.Text = project.ContractStatusName;
            //所有部门级联字符串拼接
            List<ESP.Framework.Entity.DepartmentInfo> depList = new List<ESP.Framework.Entity.DepartmentInfo>();
            depList = ESP.Framework.BusinessLogic.DepartmentManager.GetDepartmentListByID(project.GroupID.Value, depList);
            string groupname = string.Empty;
            foreach (ESP.Framework.Entity.DepartmentInfo dept in depList)
            {
                groupname += dept.DepartmentName + "-";
            }
            this.lblGroup.Text = groupname.Substring(0, groupname.Length - 1);
            this.lblProjectCode.Text = project.ProjectCode;
            this.lblApplicant.Text = project.ApplicantEmployeeName;
            lblApplicant.Attributes["onclick"] = "javascript:showUserInfoAsync(" + project.ApplicantUserID + ");";
            this.lblSerialCode.Text = project.SerialCode;
            this.lblProjectType.Text = project.ProjectTypeName;
            this.lblFromJoint.Text = project.IsFromJoint == false ? "否" : "是";
            this.lblBizType.Text = project.BusinessTypeName;
        }


        private void BindProjectInfo(ESP.Finance.Entity.ProjectInfo project)
        {
            if (project != null)
            {
                ESP.Finance.Entity.TaxRateInfo rateModel = null;
                decimal VATParam1 = 1;
                if (project.ContractTaxID != null && project.ContractTaxID.Value != 0)
                {
                    rateModel = ESP.Finance.BusinessLogic.TaxRateManager.GetModel(project.ContractTaxID.Value);
                    VATParam1 = rateModel.VATParam1;
                }
                if (project.TotalAmount != null)
                    this.txtTotalAmount.Text = Convert.ToDecimal(project.TotalAmount).ToString("#,##0.00");
                if (project.EndDate != null)
                    this.txtEndDate.Text = Convert.ToDateTime(project.EndDate).ToString("yyyy-MM-dd");
                if (project.BeginDate != null)
                    this.txtBeginDate.Text = Convert.ToDateTime(project.BeginDate).ToString("yyyy-MM-dd");
                this.txtBranchName.Text = project.BranchName;
                // if (project.ContractTax != null)
                //    this.txtTaxRate.Text = project.ContractTax.ToString();
                decimal taxfee = 0;
                decimal seviceFee = 0;

                if (rateModel != null)
                {
                    this.txtTaxRate.Text = rateModel.Remark;
                    if (project.IsCalculateByVAT == 1)
                    {
                        taxfee = ESP.Finance.BusinessLogic.CheckerManager.GetTaxByVAT(project, rateModel);
                    }
                    else
                    {
                        taxfee = ESP.Finance.BusinessLogic.CheckerManager.GetTax(project, rateModel);
                    }
                }
                if (project.IsCalculateByVAT == 1)
                {
                    decimal supporterTax = 0;
                    foreach (SupporterInfo sup in project.Supporters)
                    {
                        supporterTax += ESP.Finance.BusinessLogic.CheckerManager.GetSupporterTaxByVAT(sup, project, rateModel);
                    }
                    this.lblTaxSupporter.Text = supporterTax.ToString("#,##0.00");

                    seviceFee = ESP.Finance.BusinessLogic.CheckerManager.GetServiceFeeByVAT(project, rateModel);
                    if (project.TotalAmount != null)
                        lblTotalNoVAT.Text = (project.TotalAmount.Value / VATParam1).ToString("#,##0.00");
                }
                else
                    seviceFee = ESP.Finance.BusinessLogic.CheckerManager.GetServiceFee(project, rateModel);

                this.lblTaxFee.Text = taxfee.ToString("#,##0.00");


                this.lblServiceFee.Text = seviceFee.ToString("#,##0.00");
                if (project.TotalAmount > 0)
                {
                    lblProfileRate.Text = (seviceFee / (project.TotalAmount ?? 0) * 100).ToString("0.00");
                }
                decimal totalCost = project.CostDetails.Sum(x => x.Cost ?? 0);
                totalCost += project.Expenses.Sum(x => x.Expense ?? 0);
                this.lblCostTot.Text = totalCost.ToString("#,##0.00");
                decimal totalSupCost = project.Supporters.Sum(x => x.BudgetAllocation ?? 0);
                this.lblTotalSupporter.Text = totalSupCost.ToString("#,##0.00");
                decimal totalMediaRebate = RebateRegistrationManager.GetList(" a.projectId =" + project.ProjectId + " and a.status =" + (int)ESP.Finance.Utility.Common.RebateRegistration_Status.Audited).Sum(x => x.RebateAmount);
                

                lblUsedCost.Text = UsedCost.ToString("#,##0.00");
                lblBalanceCost.Text = (totalCost - UsedCost).ToString("#,##0.00");
                lblMediaRebate.Text = totalMediaRebate.ToString("#,##0.00");
                lblPaid.Text = (PRRecords.Sum(x => x.PaidAmount) + ExpenseRecords.Sum(x => x.PaidAmount)).ToString("#,##0.00");
                //回款金额
                decimal paymentCash = project.Payments.Where(x => x.BillDate == null && x.PaymentFee != 0).Sum(x => x.PaymentFee);
                decimal paymentBill = project.Payments.Where(x => x.BillDate != null && x.BillDate <= DateTime.Now).Sum(x => x.PaymentFee);
                lblPaymentFee.Text = (paymentCash + paymentBill).ToString("#,##0.00");
            }
        }
        private void BindCosts(ESP.Finance.Entity.ProjectInfo project)
        {
            this.gvCost.DataSource = project.CostDetails;
            this.gvCost.DataBind();
            this.gvExpense.DataSource = project.Expenses;
            this.gvExpense.DataBind();
            BindTotal(project.CostDetails, project.Expenses);
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
        private void BindTotal(IList<ESP.Finance.Entity.ContractCostInfo> list, IList<ESP.Finance.Entity.ProjectExpenseInfo> expenselist)
        {
            this.trTotal.Visible = true;
            this.lblTotal.Text = "总计:";
            decimal total = 0;
            foreach (ESP.Finance.Entity.ContractCostInfo item in list)
            {
                total += Convert.ToDecimal(item.Cost);
            }
            foreach (ESP.Finance.Entity.ProjectExpenseInfo eitem in expenselist)
            {
                total += Convert.ToDecimal(eitem.Expense);
            }
            if (total == 0)
            {
                this.trTotal.Visible = false;
            }
            this.lblTotal.Text = "总计:" + total.ToString("#,##0.00");
        }

        protected void btnReturn_Click(object sender, EventArgs e)
        {
            Response.Redirect("PrjCostTotalView.aspx");
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
                decimal refundTotal = RefundList.Where(x => x.CostId == item.CostTypeID).Sum(x=>x.Amounts);

                lblUsedCost.Text = (used + used2-refundTotal).ToString("#,##0.00");
            }
        }
        protected void gvSupporter_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header)
            {
                e.Row.Cells[0].Visible = false;
                e.Row.Cells[1].Visible = false;
                e.Row.Cells[4].Visible = false;
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    ESP.Finance.Entity.SupporterInfo supporter = (ESP.Finance.Entity.SupporterInfo)e.Row.DataItem;
                    ESP.Finance.Entity.ProjectInfo projectModel = ESP.Finance.BusinessLogic.ProjectManager.GetModelWithOutDetailList(supporter.ProjectID);
                    ESP.Finance.Entity.TaxRateInfo rateModel = null;
                    if (projectModel.ContractTaxID != null && projectModel.ContractTaxID.Value != 0)
                        rateModel = ESP.Finance.BusinessLogic.TaxRateManager.GetModel(projectModel.ContractTaxID.Value);

                    Label lblNo = (Label)e.Row.FindControl("lblNo");
                    lblNo.Text = (e.Row.RowIndex + 1).ToString();
                    SupporterInfo item = (SupporterInfo)e.Row.DataItem;
                    Label lblBudgetAllocation = (Label)e.Row.FindControl("lblBudgetAllocation");
                    Label lblBudgetNoVAT = (Label)e.Row.FindControl("lblBudgetNoVAT");
                    Label lblTaxVAT = (Label)e.Row.FindControl("lblTaxVAT");

                    if (lblBudgetAllocation != null && item.BudgetAllocation != null)
                    {
                        lblBudgetAllocation.Text = Convert.ToDecimal(item.BudgetAllocation).ToString("#,##0.00");
                        if (rateModel != null && projectModel.IsCalculateByVAT == 1)
                        {
                            lblBudgetNoVAT.Text = (item.BudgetAllocation.Value / rateModel.VATParam1).ToString("#,##0.00");
                            lblTaxVAT.Text = ESP.Finance.BusinessLogic.CheckerManager.GetSupporterTaxByVAT(supporter, projectModel, rateModel).ToString("#,##0.00");
                        }
                    }
                    //所有部门级联字符串拼接
                    Label lblGroupName = (Label)e.Row.FindControl("lblGroupName");
                    List<ESP.Framework.Entity.DepartmentInfo> depList = new List<ESP.Framework.Entity.DepartmentInfo>();
                    depList = ESP.Framework.BusinessLogic.DepartmentManager.GetDepartmentListByID(supporter.GroupID.Value, depList);
                    string groupname = string.Empty;
                    foreach (ESP.Framework.Entity.DepartmentInfo dept in depList)
                    {
                        groupname += dept.DepartmentName + "-";
                    }
                    if (!string.IsNullOrEmpty(groupname))
                        lblGroupName.Text = groupname.Substring(0, groupname.Length - 1);
                }
            }
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblName = (Label)e.Row.FindControl("lblName");
                lblName.Attributes.Add("onclick", "javascript:ShowMsg('" + ESP.Web.UI.PageBase.GetUserInfo(Convert.ToInt32(e.Row.Cells[4].Text)) + "');");

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


        protected string getUrl(string id, string prno)
        {
            
            return "<a href='" + ESP.Configuration.ConfigurationManager.SafeAppSettings["PurchaseServer"] + "\\Purchase\\Requisition\\OrderDetailTab.aspx?GeneralID=" + id + "'style='cursor: hand' target='_blank'>" + prno + "</a>";
        }


        protected void bind_Consumption(int projectId)
        {

            var consumptionList = ESP.Finance.BusinessLogic.ConsumptionManager.GetCostList(" a.projectId=" + projectId);  

            this.lblConsumptionTotal.Text = consumptionList.Sum(x => x.Amount).ToString("#,##0.00");
            gvConsumption.DataSource = consumptionList;
            gvConsumption.DataBind();
        }

        private void bindRefund(int projectId)
        {
            var refundList = ESP.Finance.BusinessLogic.RefundManager.GetList(" projectId=" + projectId);

            this.lblRefundTotal.Text = refundList.Sum(x => x.Amounts).ToString("#,##0.00");
            gvRefund.DataSource = refundList;
            gvRefund.DataBind();
        }

        private void Bind_RebateRegistrationList(int projectId)
        {
            string strWhere = " a.status=@status and a.projectId=@projectId";
            List<System.Data.SqlClient.SqlParameter> sqlParams = new List<System.Data.SqlClient.SqlParameter>();
            sqlParams.Add(new System.Data.SqlClient.SqlParameter("@status", (int)ESP.Finance.Utility.Common.RebateRegistration_Status.Audited));
            sqlParams.Add(new System.Data.SqlClient.SqlParameter("@projectId", projectId));
            List<RebateRegistrationInfo> list = ESP.Finance.BusinessLogic.RebateRegistrationManager.GetList(strWhere, sqlParams).OrderByDescending(x => x.Id).ToList();
            gvRebateRegistration.DataSource = list;
            gvRebateRegistration.DataBind();

            labRebateRegistrationTotal.Text = list.Sum(x => x.RebateAmount).ToString("#,##0.00");
        }

        protected void gvConsumption_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Cells[0].Text = (e.Row.RowIndex + 1).ToString();
            }
        }

        protected void gvRefund_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Cells[0].Text = (e.Row.RowIndex + 1).ToString();

                Label lblStatus = (Label)e.Row.FindControl("lblStatus");
                Label lblCost = (Label)e.Row.FindControl("lblCost");

                RefundInfo refundModel = (RefundInfo)e.Row.DataItem;
                ESP.Purchase.Entity.TypeInfo typeModel = ESP.Purchase.BusinessLogic.TypeManager.GetModel(refundModel.CostId);
                lblStatus.Text = ReturnPaymentType.ReturnStatusString(refundModel.Status, 0, false);
                lblCost.Text = typeModel.typename;
            }
        }
    }
}
