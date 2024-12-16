using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using ESP.Finance.Utility;
using ESP.HumanResource.Entity;
using ESP.HumanResource.BusinessLogic;
using ESP.Finance.Entity;
using ESP.Finance.BusinessLogic;

namespace FinanceWeb.Reports
{
    public partial class GroupWageReports : ESP.Web.UI.PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Server.ScriptTimeout = 6000;
            AjaxPro.Utility.RegisterTypeForAjax(typeof(Purchase_selectOperationAuditor));
            if (!IsPostBack)
            {
                if (CurrentUserID == 13233)
                {
                    btnCostViewSave.Visible = true;
                }

                OnLoadDDLYear();
                int[] deptids = CurrentUser.GetDepartmentIDs();
                if (deptids.Length > 0)
                {
                    if (IsJudgmentPermissions()) //如果方法返回True监察室或财务,否则普通用户
                    {
                        this.ddltype.Enabled = true;
                        this.ddltype1.Enabled = true;
                        this.ddltype2.Enabled = true;
                    }
                    else
                    {
                        ESP.Compatible.Department currentD = ESP.Compatible.DepartmentManager.GetDepartmentByPK(deptids[0]);
                        if (currentD.Level == 1)
                        {
                            hidtype.Value = currentD.UniqID.ToString();
                        }
                        else if (currentD.Level == 2)
                        {
                            hidtype1.Value = currentD.UniqID.ToString();
                            hidtype.Value = currentD.Parent.UniqID.ToString();
                        }
                        else if (currentD.Level == 3)
                        {
                            hidtype2.Value = currentD.UniqID.ToString();
                            hidtype1.Value = currentD.Parent.UniqID.ToString();
                            hidtype.Value = currentD.Parent.Parent.UniqID.ToString();
                        }
                        this.ddltype.Enabled = true;
                        this.ddltype1.Enabled = true;
                        this.ddltype2.Enabled = true;
                    }
                    //}
                    DepartmentDataBind();
                }

            }
        }

        private void OnLoadDDLYear()
        {
            int year = DateTime.Now.Year + 5;
            for (int i = 0; i <= 15; i++)
            {
                this.ddlYear.Items.Add(new ListItem((year - i).ToString() + "年", (year - i).ToString()));
                this.ddlEndYear.Items.Add(new ListItem((year - i).ToString() + "年", (year - i).ToString()));
            }
            ddlYear.SelectedValue = DateTime.Now.Year.ToString();
            this.ddlEndYear.SelectedValue = DateTime.Now.Year.ToString();
        }

        //判断是否为财务或监察室
        private bool IsJudgmentPermissions()
        {
            string monitorIds = System.Configuration.ConfigurationManager.AppSettings["MonitorIds"];
            string financeIds = System.Configuration.ConfigurationManager.AppSettings["FinanceIds"];
            string[] monitoridArray = monitorIds.Split(',');
            string[] financeArray = financeIds.Split(',');
            //foreach (int depid in deptids)
            //{
            string cuid = CurrentUserID.ToString();
            if (Array.Exists(monitoridArray, d => d == cuid) || Array.Exists(financeArray, d => d == cuid))
                return true;
            //}
            return false;
        }

        //判断是否为部门管理者
        private string IsManager()
        {
            string deptLevel3Ids = ",";
            IList<ESP.Finance.Entity.DeptTargetInfo> targetlist = ESP.Finance.BusinessLogic.DeptTargetManager.GetList(" userids like '%," + CurrentUser.SysID + ",%'");

            foreach (ESP.Finance.Entity.DeptTargetInfo dept in targetlist)
            {
                if (deptLevel3Ids.IndexOf("," + dept.DeptId.ToString() + ",") < 0)
                    deptLevel3Ids += dept.DeptId.ToString() + ",";
            }

            return deptLevel3Ids;
        }

        private void DepartmentDataBind()
        {
            object dt = ESP.Compatible.DepartmentManager.GetByParent(0);
            ddltype.DataSource = dt;
            ddltype.DataTextField = "NodeName";
            ddltype.DataValueField = "UniqID";
            ddltype.DataBind();
            ddltype.Items.Insert(0, new ListItem("请选择...", "-1"));
        }

        [AjaxPro.AjaxMethod]
        public static List<List<string>> getalist(int parentId)
        {
            List<List<string>> list = new List<List<string>>();
            ESP.Compatible.Department deps = new ESP.Compatible.Department();

            ESP.Compatible.Department dep = ESP.Compatible.DepartmentManager.GetDepartmentByPK(parentId);
            try
            {

                list = ESP.Compatible.DepartmentManager.GetListForAJAX(dep.ParentID);
            }
            catch (Exception e)
            {
                e.ToString();
            }

            List<string> c = new List<string>();
            c.Add("-1");
            c.Add("请选择...");
            list.Insert(0, c);
            return list;
        }

        protected void gvG_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //Label lblPrStatus = (Label)e.Row.FindControl("lblPrStatus");
                //if (lblDepartmentName != null && hidDepID != null && hidDepID.Value != string.Empty)
                //{
                //    ESP.Framework.Entity.DepartmentInfo det = ESP.Framework.BusinessLogic.DepartmentManager.Get(Convert.ToInt32(hidDepID.Value));
                //    if (det != null)
                //        lblDepartmentName.Text = det.DepartmentName;
                //}
                DataRowView dr = (DataRowView)e.Row.DataItem;
                ProjectInfo projectModel = ESP.Finance.BusinessLogic.ProjectManager.GetModel(int.Parse(dr["projectid"].ToString()));

                Label lblTotalAmount = (Label)e.Row.FindControl("lblTotalAmount");
                Label lblCostTotal = (Label)e.Row.FindControl("lblCostTotal");
                Label lblCostUsed = (Label)e.Row.FindControl("lblCostUsed");
                Label lblCostBalance = (Label)e.Row.FindControl("lblCostBalance");
                Label lblFeeRate = (Label)e.Row.FindControl("lblFeeRate");
                Label lblFee = (Label)e.Row.FindControl("lblFee");

                decimal totalamount = 0;
                if (dr["totalamount"] != System.DBNull.Value)
                    totalamount = decimal.Parse(dr["totalamount"].ToString());

                decimal taxfee = 0;
                decimal totalNoVAT = 0;

                ESP.Finance.Entity.TaxRateInfo rateModel = null;
                if (projectModel.ContractTaxID != null && projectModel.ContractTaxID != 0)
                {
                    rateModel = ESP.Finance.BusinessLogic.TaxRateManager.GetModel(projectModel.ContractTaxID.Value);

                    if (projectModel.IsCalculateByVAT == 1)
                    {
                        totalNoVAT = Convert.ToDecimal(projectModel.TotalAmount / rateModel.VATParam1);
                        taxfee = CheckerManager.GetTaxByVAT(projectModel, rateModel);
                    }
                    else
                    {
                        taxfee = ESP.Finance.BusinessLogic.CheckerManager.GetTax(projectModel, rateModel);
                    }
                }

                decimal serviceFee = 0;
                if (projectModel.IsCalculateByVAT == 1)
                    serviceFee = ESP.Finance.BusinessLogic.CheckerManager.GetServiceFeeByVAT(projectModel, rateModel);
                else
                    serviceFee = ESP.Finance.BusinessLogic.CheckerManager.GetServiceFee(projectModel, rateModel);


                decimal supporter = 0;
                if (dr["supporter"] != System.DBNull.Value)
                    supporter = decimal.Parse(dr["supporter"].ToString());
                decimal cost = 0;
                if (dr["cost"] != System.DBNull.Value)
                    cost = decimal.Parse(dr["cost"].ToString());

                decimal oop = 0;
                if (dr["oop"] != System.DBNull.Value)
                    oop = decimal.Parse(dr["oop"].ToString());

                decimal CostTotal = taxfee + supporter + cost + oop;


                decimal CostUsed = this.GetUsedCost(projectModel);

                lblTotalAmount.Text = totalamount.ToString("#,##0.00");
                lblCostTotal.Text = CostTotal.ToString("#,##0.00");
                lblCostUsed.Text = CostUsed.ToString("#,##0.00");
                lblCostBalance.Text = (CostTotal - CostUsed).ToString("#,##0.00");
                if (totalamount != 0)
                    lblFeeRate.Text = ((serviceFee) / totalamount * 100).ToString("#,##0.00");
                lblFee.Text = (serviceFee).ToString("#,##0.00");
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

        private decimal GetUsedCost(ESP.Finance.Entity.ProjectInfo projectModel)
        {
            decimal UsedCost;
            IList<ESP.Purchase.Entity.GeneralInfo> PRList;
            IList<ReturnInfo> ReturnList;
            IList<ExpenseAccountDetailInfo> ExpenseDetails;
            Dictionary<int, int> TypeMappings;
            IList<ESP.Purchase.Entity.PaymentPeriodInfo> Periods;
            IList<ESP.Purchase.Entity.MediaPREditHisInfo> MediaPREditHises;
            List<ESP.Purchase.Entity.OrderInfo> Orders;
            List<CostRecordInfo> ExpenseRecords;
            List<CostRecordInfo> PRRecords;

            Dictionary<int, decimal> CostMappings = new Dictionary<int, decimal>();
            Dictionary<int, decimal> ExpenseMappings = new Dictionary<int, decimal>();
            decimal TraficFee;

            if (projectModel == null || projectModel.GroupID == null || projectModel.GroupID.Value == 0)
                return 0;

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

            TraficFee = ReturnList.Where(x => x.ReturnType == 20).Sum(x => (x.FactFee ?? (x.PreFee ?? 0)));

            UsedCost = TraficFee + CostMappings.Sum(x => x.Value) + ExpenseMappings.Sum(x => x.Value);

            return UsedCost;
        }


        protected void btnSearch_Click(object sender, EventArgs e)
        {
            if (Validate())
            {
                BindGroupWage();
            }
        }

        private bool Validate()
        {
            if (IsJudgmentPermissions())
            {
                if (this.hidtype1.Value == "-1" || string.IsNullOrEmpty(this.hidtype1.Value))
                {
                    ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('请选择二级部门！');", true);
                    return false;
                }
            }

            if (this.hidtype2.Value == "-1" || string.IsNullOrEmpty(this.hidtype2.Value))
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('请选择三级部门！');", true);
                return false;
            }
            else
            {
                if (IsManager().IndexOf("," + hidtype2.Value + ",") < 0)
                {
                    ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('您没有该部门查询权限！');", true);
                    return false;
                }
            }

            return true;
        }

        protected void btnExportForFinance_Click(object sender, EventArgs e)
        {
            if (Validate())
            {
               int ret = GetDataForExportFinance();
               if (ret == 0)
               {
                   ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('暂时没有数据！');", true);
               }
            }

        }

        protected void btnCostView_Click(object sender, EventArgs e)
        {
            if (Validate())
            {
                ExportCostView();
            }
        }

        protected void btnExportForGroup_Click(object sender, EventArgs e)
        {
            if (Validate())
            {
                GetDataForExportProject();

            }
        }

        protected void gvG_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvG.PageIndex = e.NewPageIndex;
            BindGroupWage();
        }

        private void BindGroupWage()
        {
            DateTime beginDate = new DateTime();
            DateTime endDate = new DateTime();
            SetBeginDateAndEndDate(out beginDate, out endDate);

            string strCondition = " 1= 1";
            string depIds = string.Empty;
            if (hidtype2.Value != "" && hidtype2.Value != "-1")
            {
                depIds = hidtype2.Value;
            }
            else if (hidtype1.Value != "" && hidtype1.Value != "-1")
            {
                IList<ESP.Framework.Entity.DepartmentInfo> list = ESP.Framework.BusinessLogic.DepartmentManager.GetChildren(Convert.ToInt32(hidtype1.Value));
                if (list != null && list.Count > 0)
                {

                    foreach (ESP.Framework.Entity.DepartmentInfo dep in list)
                    {
                        depIds += dep.DepartmentID + ",";
                    }
                    depIds = depIds.TrimEnd(',');

                }
            }
            if (!string.IsNullOrEmpty(depIds))
            {
                strCondition += " AND groupid in(" + depIds + ")";
            }
            strCondition += " AND (enddate between '" + beginDate + "' and '" + endDate + "')";

            DataSet listSalary = DeptSalaryManager.GetDataSet(depIds, strCondition);
            this.gvG.DataSource = listSalary.Tables[0];
            this.gvG.DataBind();
        }

        private void ExportCostView()
        {
            DateTime beginDate = new DateTime();
            DateTime endDate = new DateTime();
            SetBeginDateAndEndDate(out beginDate, out endDate);
            string strCondition = string.Empty;
            string depIds = string.Empty;

            if (hidtype2.Value != "" && hidtype2.Value != "-1")
            {
                depIds = this.hidtype2.Value;
            }
            else if (hidtype1.Value != "" && hidtype1.Value != "-1")
            {
                IList<ESP.Framework.Entity.DepartmentInfo> list = ESP.Framework.BusinessLogic.DepartmentManager.GetChildren(Convert.ToInt32(hidtype1.Value));
                if (list != null && list.Count > 0)
                {

                    foreach (ESP.Framework.Entity.DepartmentInfo dep in list)
                    {
                        depIds += dep.DepartmentID + ",";
                    }
                    depIds = depIds.TrimEnd(',');
                }
            }
            if (!string.IsNullOrEmpty(depIds))
            {
                strCondition += " groupid in(" + depIds + ")";
            }
            strCondition += " AND (enddate between '" + beginDate + "' and '" + endDate + "')";

            DataSet listSalary = DeptSalaryManager.GetDataSet(depIds, strCondition);

            ESP.Finance.BusinessLogic.ReturnManager.ExportCostView(listSalary.Tables[0], this.Response);

        }

        private void SetBeginDateAndEndDate(out DateTime beginDate, out DateTime endDate)
        {
            beginDate = new DateTime(int.Parse(this.ddlYear.SelectedValue), int.Parse(this.ddlMonth.SelectedValue), 1);
            endDate = new DateTime(int.Parse(this.ddlEndYear.SelectedValue), int.Parse(this.ddlEndMonth.SelectedValue), 1);
            endDate = endDate.AddMonths(1).AddDays(-1);
        }

        private int GetDataForExportFinance()
        {
            DateTime beginDate = new DateTime();
            DateTime endDate = new DateTime();
            SetBeginDateAndEndDate(out beginDate, out endDate);

            string groupids = string.Empty;
            if (hidtype2.Value != "" && hidtype2.Value != "-1")
            {

                groupids += this.hidtype2.Value;

            }
            else if (hidtype1.Value != "" && hidtype1.Value != "-1")
            {
                IList<ESP.Framework.Entity.DepartmentInfo> list = ESP.Framework.BusinessLogic.DepartmentManager.GetChildren(Convert.ToInt32(hidtype1.Value));
                if (list != null && list.Count > 0)
                {
                    foreach (ESP.Framework.Entity.DepartmentInfo dep in list)
                    {
                        groupids += dep.DepartmentID + ",";
                    }
                    groupids = groupids.TrimEnd(',');
                }
            }

            string filename = string.Empty;
            string serverpath = Server.MapPath(ESP.Finance.Configuration.ConfigurationManager.MediaOrderPath);

            DataSet ds = DeptSalaryManager.GetDataSetForExportFinance(groupids, beginDate, endDate);//ESP.Finance.BusinessLogic.ReturnManager.
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                ESP.Finance.BusinessLogic.ReturnManager.ExportDeptSalayForFinance(CurrentUserID, ds, this.Response, beginDate, endDate, groupids);

                GC.Collect();

                return 1;
            }
            else
                return 0;
        }

        private void GetDataForExportProject()
        {
            DateTime beginDate = new DateTime();
            DateTime endDate = new DateTime();
            SetBeginDateAndEndDate(out beginDate, out endDate);

            string groupids = string.Empty;
            if (hidtype2.Value != "" && hidtype2.Value != "-1")
            {
                groupids = this.hidtype2.Value;
            }
            else
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('请选择三级部门！');", true);

                return;
            }



            string filename = string.Empty;
            string serverpath = Server.MapPath(ESP.Finance.Configuration.ConfigurationManager.MediaOrderPath);

            DataSet ds = DeptSalaryManager.GetDsForExportProject(groupids, beginDate, endDate);//ESP.Finance.BusinessLogic.ReturnManager.

            ESP.Finance.BusinessLogic.ReturnManager.ExportDeptSalayForProject(CurrentUserID, groupids, ddltype2.SelectedValue, CurrentUserID, ds.Tables[0], this.Response, beginDate, endDate);


            GC.Collect();

        }

        protected void btnCostViewSave_Click(object sender, EventArgs e)
        {
            DateTime beginDate = new DateTime();
            DateTime endDate = new DateTime();
            SetBeginDateAndEndDate(out beginDate, out endDate);

            string strCondition = " 1= 1";

            strCondition += " AND (enddate between '" + beginDate + "' and '" + endDate + "')";

            DataSet listSalary = DeptSalaryManager.GetDataSetSaveData(strCondition);

            int count = ESP.Finance.BusinessLogic.ReturnManager.ExportCostViewSaving(CurrentUserID, listSalary.Tables[0]);

            ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('共计" + count + "条记录！');", true);
        }
    }
}