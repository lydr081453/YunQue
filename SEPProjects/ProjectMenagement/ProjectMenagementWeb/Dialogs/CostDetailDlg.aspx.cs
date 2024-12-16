﻿using System;
using System.Text.RegularExpressions;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ESP.Finance.Entity;
using ESP.Finance.BusinessLogic;
using ESP.Finance.Utility;

public partial class Dialogs_CostDetailDlg : System.Web.UI.Page
{
    private int projectid = 0;
    private string script = string.Empty;
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
    private IList<ESP.Purchase.Entity.TypeInfo> typeList;

    protected void Page_Load(object sender, EventArgs e)
    {
        BindData();
        if (!IsPostBack)
        {
            decimal totalCost = 0;
            totalCost = projectModel.CostDetails.Sum(x => x.Cost) ?? 0;

            BindExpense();
            BindRP1();

            if (!string.IsNullOrEmpty(this.txtCost.Text))
                totalCost += Convert.ToDecimal(this.txtCost.Text.Trim().Replace(",", ""));
            //if (!string.IsNullOrEmpty(this.txtMedia.Text))
            //    totalCost += Convert.ToDecimal(this.txtMedia.Text.Trim().Replace(",", ""));
            //if (!string.IsNullOrEmpty(this.txtCostSaving.Text))
            //    totalCost += Convert.ToDecimal(this.txtCostSaving.Text.Trim().Replace(",", ""));
            this.hidTotalCost.Value = totalCost.ToString();
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
        projectid = Convert.ToInt32(Request[ESP.Finance.Utility.RequestName.ProjectID]);
        projectModel = ESP.Finance.BusinessLogic.ProjectManager.GetModel(projectid);


        if (projectModel == null || projectModel.GroupID == null || projectModel.GroupID.Value == 0)
            return;

        var typelvl2 = ESP.Purchase.BusinessLogic.TypeManager.GetListLvl2();
        typelvl2[0] = "OOP";
        typelvl2[-1] = "[未知]";

        //listCostsForProject = ESP.Finance.BusinessLogic.ContractCostManager.GetListByProject(projectid, null, null);
        //expenseList = ESP.Finance.BusinessLogic.ProjectExpenseManager.GetList("ProjectID = " + projectid.ToString());
        PRList = ESP.Purchase.BusinessLogic.GeneralInfoManager.GetModelList(projectModel.ProjectId, projectModel.GroupID.Value);
        ReturnList = ESP.Finance.BusinessLogic.ReturnManager.GetList(projectModel.ProjectId, projectModel.GroupID.Value);
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
    }


    private void BindRP1()
    {
        var list = typeList.Where(x => x.typelevel == 1 && x.status == 1);
        this.rp1.DataSource = list.ToList();
        this.rp1.DataBind();
    }

    //protected void btnSearch_Click(object sender, EventArgs e)
    //{
    //    if (this.txtKey.Text.Trim() == string.Empty)
    //    {
    //        BindRP1();
    //    }
    //    else
    //    {
    //        this.rp1.DataSource = typeList.Where(x => x.typelevel == 1 && x.status == 1 && x.typename.Contains(this.txtKey.Text));
    //        //ESP.Finance.BusinessLogic.CostTypeViewManager.GetList(" a.typeid in( select distinct parentid from CostTypeViewInfo where typeLevel =2 and typename like ('%" + this.txtKey.Text.Trim() + "%') )", null);
    //        this.rp1.DataBind();
    //    }
    //}

    private void BindExpense()
    {
        var oop = projectModel.Expenses.Where(x => x.Description == "OOP").FirstOrDefault();
        var media = projectModel.Expenses.Where(x => x.Description == "Media").FirstOrDefault();
        var costSaving = projectModel.Expenses.Where(x => x.Description == "COST SAVING").FirstOrDefault();
        decimal usedoop = 0;
        if (oop != null)
        {
            this.txtCost.Text = oop.Expense.Value.ToString("0.00");
            usedoop = 0M;
            ExpenseMappings.TryGetValue(0, out usedoop);
            this.lblOOP.Text = usedoop.ToString("#,##0.00");
        }

        //if (media != null)
        //    this.txtMedia.Text = media.Expense.Value.ToString("0.00");

        //if (costSaving != null)
        //{
        //    this.txtCostSaving.Text = costSaving.Expense.Value.ToString("0.00");

        //}
        //if (!string.IsNullOrEmpty(projectModel.ProjectCode))
        //{
        //    this.lblTraffic.Text = TraficFee.ToString("#,##0.00");
        //}

    }

    protected void rp1_ItemDataBound(object sender, DataListItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            ESP.Purchase.Entity.TypeInfo type1 = e.Item.DataItem as ESP.Purchase.Entity.TypeInfo;

            Label lblMain = (Label)e.Item.FindControl("lblMain");
            lblMain.Text = type1.typename;
            DataList ListLevel2 = (DataList)e.Item.FindControl("ListLevel2");
            //if (this.txtKey.Text != string.Empty)
            //{
            //    //ESP.Finance.BusinessLogic.CostTypeViewManager.GetList("a.typelevel = 2 AND a.typename like('%" + this.txtKey.Text.Trim() + "%') AND a.parentid =" + type1.TypeID.ToString());
            //    ListLevel2.DataSource = typeList.Where(x => x.parentId == type1.typeid && x.typelevel == 2 && x.typename.Contains(this.txtKey.Text));
            //    ListLevel2.DataBind();
            //}
            //else
            //{
            ListLevel2.DataSource = typeList.Where(x => x.parentId == type1.typeid && x.typelevel == 2).OrderBy(x => x.Sort);
                ListLevel2.DataBind();
            //}

        }

    }

    protected void List2_ItemCommand(object sender, DataListCommandEventArgs e)
    {

    }

    protected void List2_ItemDataBound(object sender, DataListItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            ESP.Purchase.Entity.TypeInfo type2 = e.Item.DataItem as ESP.Purchase.Entity.TypeInfo;
            //在此处增加采购系统验证

            DataList ListLevel3 = (DataList)e.Item.FindControl("ListLevel3");
            var type3 = typeList.Where(x => x.parentId == type2.typeid).OrderBy(x=>x.Sort);
            ListLevel3.DataSource = type3;
            ListLevel3.DataBind();
            Label lblUsedAmount = (Label)e.Item.FindControl("lblUsedAmount");
            CheckBox ckxSelected = (CheckBox)e.Item.FindControl("ckxSelected");
            TextBox txtAmount = (TextBox)e.Item.FindControl("txtAmount");
            HiddenField hidTypeID = (HiddenField)e.Item.FindControl("hidTypeID");
            foreach (ContractCostInfo costItem in projectModel.CostDetails)
            {
                if (type2.typeid == costItem.CostTypeID)
                {
                    if (ckxSelected != null && txtAmount != null && hidTypeID != null)
                    {
                        ckxSelected.Checked = true;
                        txtAmount.Text = Convert.ToDecimal(costItem.Cost).ToString("0.00");
                    }

                    foreach (var t in type3)
                    {
                        if (Orders.Where(x => x.producttype == t.typeid).Count() > 0)
                        {
                            ckxSelected.Enabled = false;
                            break;
                        }
                    }

                }
            }


            if (ckxSelected.Checked == true)
            {
                decimal used = 0, used2 = 0;
                CostMappings.TryGetValue(type2.typeid, out used);
                ExpenseMappings.TryGetValue(type2.typeid, out used2);
                lblUsedAmount.Text = (used + used2).ToString("#,##0.00");
            }
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(Request[ESP.Finance.Utility.RequestName.ProjectID]))
        {
            if (projectModel != null)
            {
                if (CheckCost(projectModel) == -1)
                {
                    ClientScript.RegisterStartupScript(typeof(string), "", "alert('成本项金额小于采购系统中已申请金额!');", true);
                    return;
                }

                if (string.IsNullOrEmpty(this.txtCost.Text))
                {
                    this.txtCost.Text = "0.00";
                }
                decimal oopmax = projectModel.TotalAmount.Value * 3 / 100;
                decimal oopused = string.IsNullOrEmpty(lblOOP.Text) ? 0 : decimal.Parse(lblOOP.Text);

                if (Convert.ToDecimal(this.txtCost.Text) < oopused)
                {
                    ClientScript.RegisterStartupScript(typeof(string), "", "alert('OOP金额小于系统中已申请金额!');", true);
                    return;
                }

                SaveCost(projectModel);
                SaveExpense(projectModel);
                decimal totalCost = 0;
                IList<ESP.Finance.Entity.ContractCostInfo> CostDetails = ESP.Finance.BusinessLogic.ContractCostManager.GetListByProject(projectModel.ProjectId, null, null);
                if (CostDetails != null && CostDetails.Count > 0)
                {
                    foreach (ContractCostInfo model in CostDetails)
                    {
                        totalCost += model.Cost.Value;
                    }
                }
                IList<ProjectExpenseInfo> expenseList = ESP.Finance.BusinessLogic.ProjectExpenseManager.GetList("ProjectID = " + projectModel.ProjectId.ToString());
                if (expenseList != null && expenseList.Count > 0)
                {
                    foreach (ProjectExpenseInfo m in expenseList)
                    {
                        totalCost += m.Expense.Value;
                    }
                }
                if (totalCost != Convert.ToDecimal(this.hidTotalCost.Value) && (projectModel.Status == (int)ESP.Finance.Utility.Status.FinanceAuditComplete || projectModel.Status == (int)ESP.Finance.Utility.Status.ProjectPreClose))
                {
                    ESP.Finance.BusinessLogic.ProjectManager.ChangeStatus(projectModel, Status.Saved);
                }
                script = @"var uniqueId = 'ctl00$ContentPlaceHolder1$ProjectContractCost$';
opener.__doPostBack(uniqueId + 'btnRet', '');
window.close(); ";

                ScriptManager.RegisterStartupScript(this, Page.GetType(), new Guid().ToString(), script, true);

            }
            // }
        }
    }

    private bool IsCheckAmountForProject(ProjectInfo project)
    {
        decimal amount = 0;
        decimal excludeAmount = 0;
        decimal cost;
        if (decimal.TryParse(txtCost.Text, out cost))
            amount += cost;
        decimal meidiacost;
        //if (decimal.TryParse(txtMedia.Text, out meidiacost))
        //    amount += meidiacost;
        //decimal costsaving;
        //if (decimal.TryParse(txtCostSaving.Text, out costsaving))
        //    amount += costsaving;


        foreach (DataListItem item in this.rp1.Items)
        {
            DataList ListLevel2 = (DataList)item.FindControl("ListLevel2");
            if (ListLevel2 != null)
            {
                foreach (DataListItem itemLevel in ListLevel2.Items)
                {
                    CheckBox ckxSelected = (CheckBox)itemLevel.FindControl("ckxSelected");
                    TextBox txtAmount = (TextBox)itemLevel.FindControl("txtAmount");
                    HiddenField hidTypeID = (HiddenField)itemLevel.FindControl("hidTypeID");
                    Label lblName = (Label)itemLevel.FindControl("lblName");
                    if (!checkMoney(txtAmount.Text.Trim().Replace(",", string.Empty)))
                        continue;
                    decimal itemAmount;

                    if (ckxSelected != null && ckxSelected.Checked && txtAmount != null && txtAmount.Text != string.Empty
                        && hidTypeID != null && hidTypeID.Value != string.Empty && decimal.TryParse(txtAmount.Text.Trim(), out itemAmount))
                    {
                        if (CheckerManager.isExclude(Convert.ToInt32(hidTypeID.Value)))
                        {
                            excludeAmount += itemAmount;
                        }
                        else
                        {
                            amount += itemAmount;
                        }
                    }
                }
            }
        }
        return ESP.Finance.BusinessLogic.CheckerManager.CheckProjectCost(project.ProjectId, amount, excludeAmount);
    }

    private int CheckCost(ProjectInfo project)
    {
        int ret = 0;
        foreach (DataListItem item in this.rp1.Items)
        {
            DataList ListLevel2 = (DataList)item.FindControl("ListLevel2");
            if (ListLevel2 != null)
            {
                foreach (DataListItem itemLevel in ListLevel2.Items)
                {

                    CheckBox ckxSelected = (CheckBox)itemLevel.FindControl("ckxSelected");
                    TextBox txtAmount = (TextBox)itemLevel.FindControl("txtAmount");
                    HiddenField hidTypeID = (HiddenField)itemLevel.FindControl("hidTypeID");
                    Label lblName = (Label)itemLevel.FindControl("lblName");

                    if (!checkMoney(txtAmount.Text.Trim().Replace(",", string.Empty)))
                        continue;
                    decimal itemAmount;
                    if (ckxSelected != null && ckxSelected.Checked && txtAmount != null && txtAmount.Text != string.Empty
                        && hidTypeID != null && hidTypeID.Value != string.Empty && decimal.TryParse(txtAmount.Text.Trim(), out itemAmount))
                    {
                        decimal used = 0, used2 = 0;
                        CostMappings.TryGetValue(Convert.ToInt32(hidTypeID.Value), out used);
                        ExpenseMappings.TryGetValue(Convert.ToInt32(hidTypeID.Value), out used2);

                        if (used + used2 > itemAmount && itemAmount>0)
                        {
                            ret = -1;
                            break;
                        }
                    }
                }
            }
        }
        return ret;
    }

    private void SaveCost(ProjectInfo project)
    {
        //Delete old rows
        //IList<ContractCostInfo> listCosts = ESP.Finance.BusinessLogic.ContractCostManager.GetListByProject(project.ProjectId, null, null);
        foreach (ContractCostInfo cos in projectModel.CostDetails)
        {
            ESP.Finance.BusinessLogic.ContractCostManager.Delete(cos.ContractCostID);
        }
        //Add new members
        foreach (DataListItem item in this.rp1.Items)
        {
            DataList ListLevel2 = (DataList)item.FindControl("ListLevel2");
            if (ListLevel2 != null)
            {
                foreach (DataListItem itemLevel in ListLevel2.Items)
                {

                    CheckBox ckxSelected = (CheckBox)itemLevel.FindControl("ckxSelected");
                    TextBox txtAmount = (TextBox)itemLevel.FindControl("txtAmount");
                    HiddenField hidTypeID = (HiddenField)itemLevel.FindControl("hidTypeID");
                    Label lblName = (Label)itemLevel.FindControl("lblName");

                    if (!checkMoney(txtAmount.Text.Trim().Replace(",", string.Empty)))
                        continue;

                    decimal itemAmount;
                    if (ckxSelected != null && ckxSelected.Checked && txtAmount != null && txtAmount.Text != string.Empty
                        && hidTypeID != null && hidTypeID.Value != string.Empty && decimal.TryParse(txtAmount.Text.Trim(), out itemAmount))
                    {

                        ContractCostInfo cost = new ContractCostInfo();
                        cost.ProjectID = project.ProjectId;
                        cost.ProjectCode = project.ProjectCode;
                        cost.Cost = itemAmount;
                        cost.CostTypeID = Convert.ToInt32(hidTypeID.Value.Trim());
                        cost.Description = lblName.Text;
                        int id = ESP.Finance.BusinessLogic.ContractCostManager.Add(cost);
                    }
                }
            }
        }
    }

    private bool checkMoney(string cost)
    {
        string monval = @"^[-+]?\d+(\.\d+)?$";
        return Regex.IsMatch(cost, monval);

    }
    private void SaveExpense(ProjectInfo project)
    {
        //IList<ProjectExpenseInfo> expenseList = ESP.Finance.BusinessLogic.ProjectExpenseManager.GetList("ProjectID = " + project.ProjectId.ToString());
        if (projectModel.Expenses != null && projectModel.Expenses.Count > 0)
        {
            foreach (ProjectExpenseInfo exp in projectModel.Expenses)
            {
                if(exp.Description != ESP.Finance.Utility.ProjectExpense_Desc.Recharge)
                    ESP.Finance.BusinessLogic.ProjectExpenseManager.Delete(exp.ProjectExpenseID);
            }
        }
        if (this.txtCost.Text != string.Empty && checkMoney(txtCost.Text.Trim().Replace(",", string.Empty)) == true && Convert.ToDecimal(txtCost.Text.Trim()) != 0)
        {
            ProjectExpenseInfo expense = new ProjectExpenseInfo();
            expense.ProjectID = project.ProjectId;
            expense.ProjectCode = project.ProjectCode;
            expense.Expense = Convert.ToDecimal(this.txtCost.Text.Trim());
            expense.Description = ESP.Finance.Utility.ProjectExpense_Desc.OOP;
            ESP.Finance.BusinessLogic.ProjectExpenseManager.Add(expense);
        }
        //if (this.txtMedia.Text != string.Empty && checkMoney(txtMedia.Text.Trim().Replace(",", string.Empty)) == true && Convert.ToDecimal(txtMedia.Text.Trim()) != 0)
        //{
        //    ProjectExpenseInfo expenseMediaCost = new ProjectExpenseInfo();
        //    expenseMediaCost.ProjectID = project.ProjectId;
        //    expenseMediaCost.ProjectCode = project.ProjectCode;
        //    expenseMediaCost.Expense = Convert.ToDecimal(this.txtMedia.Text.Trim());
        //    expenseMediaCost.Description = "Media";

        //    ESP.Finance.BusinessLogic.ProjectExpenseManager.Add(expenseMediaCost);
        //}
        //if (this.txtCostSaving.Text != string.Empty && checkMoney(txtCostSaving.Text.Trim().Replace(",", string.Empty)) == true && Convert.ToDecimal(txtCostSaving.Text.Trim()) != 0)
        //{
        //    ProjectExpenseInfo CostSaving = new ProjectExpenseInfo();
        //    CostSaving.ProjectID = project.ProjectId;
        //    CostSaving.ProjectCode = project.ProjectCode;
        //    CostSaving.Expense = Convert.ToDecimal(this.txtCostSaving.Text.Trim());
        //    CostSaving.Description = "COST SAVING";

        //    ESP.Finance.BusinessLogic.ProjectExpenseManager.Add(CostSaving);
        //}
    }

    protected void btnClose_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(this, Page.GetType(), new Guid().ToString(), "window.close(); ", true);
    }

    protected void gvMaterial_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[1].Visible = false;
        }
    }
}