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

namespace FinanceWeb.Reports
{
    public partial class ProjectReports : ESP.Web.UI.PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            AjaxPro.Utility.RegisterTypeForAjax(typeof(Purchase_selectOperationAuditor));
            Server.ScriptTimeout = 6000;
            if (!IsPostBack)
            {
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
                        string deptLevel3Ids = IsManager();
                        if (!string.IsNullOrEmpty(deptLevel3Ids))
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
                    }
                    DepartmentDataBind();
                }

            }
        }

        private void OnLoadDDLYear()
        {
            int year = DateTime.Now.Year;
            for (int i = 0; i <= 15; i++)
            {
                this.ddlYear.Items.Add(new ListItem((year - i).ToString() + "年", (year - i).ToString()));
                this.ddlEndYear.Items.Add(new ListItem((year - i).ToString() + "年", (year - i).ToString()));
            }
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
            DataSet ds = ESP.Framework.BusinessLogic.OperationAuditManageManager.GetDeptListByUserID(CurrentUserID);
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    deptLevel3Ids += (dr["DepId"].ToString() + ",");
                }
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
                DataRowView dr = (DataRowView)e.Row.DataItem;
                Label lblTotalAmount = (Label)e.Row.FindControl("lblTotalAmount");
                Label lblCostTotal = (Label)e.Row.FindControl("lblCostTotal");
                Label lblCostUsed = (Label)e.Row.FindControl("lblCostUsed");
                Label lblCostBalance = (Label)e.Row.FindControl("lblCostBalance");
                Label lblFeeRate = (Label)e.Row.FindControl("lblFeeRate");
                Label lblFee = (Label)e.Row.FindControl("lblFee");

                decimal totalamount = 0;
                if (dr["totalamount"] != System.DBNull.Value)
                    totalamount = decimal.Parse(dr["totalamount"].ToString());

                decimal tax = 0;
                if (dr["tax"] != System.DBNull.Value)
                    tax = decimal.Parse(dr["tax"].ToString());

                decimal supporter = 0;
                if (dr["supporter"] != System.DBNull.Value)
                    supporter = decimal.Parse(dr["supporter"].ToString());
                decimal cost = 0;
                if (dr["cost"] != System.DBNull.Value)
                    cost = decimal.Parse(dr["cost"].ToString());

                decimal oop = 0;
                if (dr["oop"] != System.DBNull.Value)
                    oop = decimal.Parse(dr["oop"].ToString());

                decimal cost1 = 0;
                if (dr["cost1"] != System.DBNull.Value)
                    cost1 = decimal.Parse(dr["cost1"].ToString());

                decimal cost2 = 0;
                if (dr["cost2"] != System.DBNull.Value)
                    cost2 = decimal.Parse(dr["cost2"].ToString());

                decimal cost3 = 0;
                if (dr["cost3"] != System.DBNull.Value)
                    cost3 = decimal.Parse(dr["cost3"].ToString());

                decimal cost4 = 0;
                if (dr["cost4"] != System.DBNull.Value)
                    cost4 = decimal.Parse(dr["cost4"].ToString());

                decimal cost5 = 0;
                if (dr["cost5"] != System.DBNull.Value)
                    cost5 = decimal.Parse(dr["cost5"].ToString());

                decimal CostTotal = tax + supporter + cost + oop;
                decimal CostUsed = cost1 + cost2 + cost3 + cost4 + cost5;

                lblTotalAmount.Text = totalamount.ToString("#,##0.00");
                lblCostTotal.Text = CostTotal.ToString("#,##0.00");
                lblCostUsed.Text = CostUsed.ToString("#,##0.00");
                lblCostBalance.Text = (cost + oop - CostUsed).ToString("#,##0.00");
                lblFeeRate.Text = ((totalamount - CostTotal) / totalamount * 100).ToString("#,##0.00");
                lblFee.Text = (totalamount - CostTotal).ToString("#,##0.00");
            }
        }

        private bool ValidateUser()
        {
            string depts = IsManager();
            if (!string.IsNullOrEmpty(depts))
            {
                if (this.hidtype2.Value == "-1" || string.IsNullOrEmpty(this.hidtype2.Value))
                {
                    lblMsg.Text = "请选择三级部门！";
                    return false;
                }
                else
                {
                    if (depts.IndexOf("" + this.hidtype2.Value + "") >= 0)
                    {
                        return true;
                    }
                    else
                    {
                        lblMsg.Text = "您没有该部门权限！";
                        return false;
                    }
                }
            }
            else
            {
                lblMsg.Text = "您没有本功能权限！";
                return false;
            }

        }

        protected void btnExport_Click(object sender, EventArgs e)
        {
            lblMsg.Text = "";
            if (ValidateUser())
            {
                GetDataForExport();
            }
        }

        protected void btnProjectView_click(object sender, EventArgs e)
        {
            lblMsg.Text = "";
            string depts = IsManager();

            ESP.Finance.Entity.ProjectInfo proinfo = ESP.Finance.BusinessLogic.ProjectManager.GetModelByProjectCode(this.txtProjectCode.Text.Trim());


            if (!IsJudgmentPermissions() && depts.IndexOf("," + proinfo.GroupID.ToString() + ",") < 0)
            {
                lblMsg.Text = "您没有该项目权限！";
                return;
            }

            if (proinfo != null)
            {
                lblBegin.Text = proinfo.BeginDate.Value.ToString("yyyy-MM-dd");
                lblEnd.Text = proinfo.EndDate.Value.ToString("yyyy-MM-dd");
                lblLeader.Text = proinfo.ApplicantEmployeeName;
                lblPrjName.Text = proinfo.BusinessDescription;

                ListItem def = new ListItem();
                def.Text = "请选择";
                def.Value = "0";
                def.Selected = true;
                this.ddlDept.Items.Add(def);

                string approveddepts = "0" + IsManager() + "0";
                if (approveddepts.IndexOf(proinfo.GroupID.ToString()) > 0)
                {
                    ListItem pdept = new ListItem();
                    pdept.Text = proinfo.GroupName;
                    pdept.Value = proinfo.GroupID.ToString();
                    this.ddlDept.Items.Add(pdept);
                }

                IList<ESP.Finance.Entity.SupporterInfo> suplist = ESP.Finance.BusinessLogic.SupporterManager.GetList(" projectid =" + proinfo.ProjectId + " and groupid in(" + approveddepts + ")");

                foreach (ESP.Finance.Entity.SupporterInfo sup in suplist)
                {
                    ListItem pdept = new ListItem();
                    pdept.Text = sup.GroupName;
                    pdept.Value = sup.GroupID.ToString();
                    this.ddlDept.Items.Add(pdept);
                }

            }
        }


        protected void btnExportProject_Click(object sender, EventArgs e)
        {
            lblMsg.Text = "";
            GetDataForExportProject();
        }

        protected void gvG_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
        }

        private void SetBeginDateAndEndDate(out DateTime beginDate, out DateTime endDate)
        {
            //beginDate = new DateTime(int.Parse(this.ddlYear.SelectedValue), int.Parse(this.ddlMonth.SelectedValue), 1);
            //endDate = new DateTime(int.Parse(this.ddlEndYear.SelectedValue), int.Parse(this.ddlEndMonth.SelectedValue), 1);
            //endDate = endDate.AddMonths(1).AddDays(-1);
            beginDate = Convert.ToDateTime(this.txtBeginDate.Text);
            endDate = Convert.ToDateTime(this.txtEndDate.Text);
        }

        private void GetDataForExport()
        {
            DateTime beginDate = new DateTime();
            DateTime endDate = new DateTime();
            SetBeginDateAndEndDate(out beginDate, out endDate);

            string filename = string.Empty;
            string serverpath = Server.MapPath(ESP.Finance.Configuration.ConfigurationManager.MediaOrderPath);

            //DataSet ds = DeptSalaryManager.GetDsForExportProject(Convert.ToInt32(this.hidtype2.Value), beginDate, endDate);//ESP.Finance.BusinessLogic.ReturnManager.
            //if (ds != null && ds.Tables[0].Rows.Count > 0)
            //{
            ESP.Finance.BusinessLogic.ReturnManager.ExportMonthProjectGroupReport(beginDate, endDate, Convert.ToInt32(this.hidtype2.Value), this.Response, this.chkMonth.Checked, this.chkWeek.Checked);

            GC.Collect();
            //}
        }

        private void GetDataForExportProject()
        {
            DateTime beginDate = new DateTime(Convert.ToInt32(this.ddlYear.SelectedValue), Convert.ToInt32(this.ddlMonth.SelectedValue), 1);
            DateTime endDate = new DateTime(Convert.ToInt32(this.ddlEndYear.SelectedValue), Convert.ToInt32(this.ddlEndMonth.SelectedValue), 1);

            string filename = string.Empty;
            string serverpath = Server.MapPath(ESP.Finance.Configuration.ConfigurationManager.MediaOrderPath);

            ESP.Finance.Entity.ProjectInfo proinfo = ESP.Finance.BusinessLogic.ProjectManager.GetModelByProjectCode(this.txtProjectCode.Text.Trim());
            if (proinfo != null)
            {
                if (IsJudgmentPermissions())
                    ESP.Finance.BusinessLogic.ReturnManager.ExportProjectReportOnTimeSheet(CurrentUserID, this.Response, beginDate, endDate, proinfo, ddlDept.SelectedValue);
                else
                {
                    if (ddlDept.SelectedValue != "0")
                    {
                        ESP.Finance.BusinessLogic.ReturnManager.ExportProjectReportOnTimeSheet(CurrentUserID, this.Response, beginDate, endDate, proinfo, ddlDept.SelectedValue);
                    }
                }

                GC.Collect();
            }
        }
    }
}