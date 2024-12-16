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
    public partial class PaymentReports : ESP.Web.UI.PageBase
    {

        private static int showAlertUserid = 0;
        private static int alertvalue = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            showAlertUserid = CurrentUserID;
            alertvalue = Request["alert"] == null ? 0 : int.Parse(Request["alert"].ToString());

            Server.ScriptTimeout = 6000;
            AjaxPro.Utility.RegisterTypeForAjax(typeof(FinanceWeb.Reports.PaymentReports));
            if (!IsPostBack)
            {
                #region 登录人部门权限
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
                            hidtype2.Value = "-1";
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
                #endregion

                btnExportByMonth3.Visible = CurrentUserRight();

            }
        }

        private bool CurrentUserIsFinance = false;

        private bool CurrentUserRight()
        {
            bool curRight = false;
            string sql = string.Format(" (FirstFinanceID={0} or PaymentAccounter ={0} or ProjectAccounter={0})", CurrentUserID);

            IList<BranchInfo> branchList = ESP.Finance.BusinessLogic.BranchManager.GetList(sql);

            string financeIds = System.Configuration.ConfigurationManager.AppSettings["FinanceIds"];

            if ((branchList != null && branchList.Count > 0) || (financeIds.IndexOf("," + CurrentUserID + ",")) >= 0)
                curRight = true;

            return curRight;
        }

        private void DepartmentDataBind()
        {
            object dt = ESP.Compatible.DepartmentManager.GetByParent(0);
            ddltype.DataSource = dt;
            ddltype.DataTextField = "NodeName";
            ddltype.DataValueField = "UniqID";
            ddltype.DataBind();
            ddltype.Items.Insert(0, new ListItem("全部", "-1"));
        }

        [AjaxPro.AjaxMethod]
        public static List<List<string>> getalist(int parentId)
        {
            List<List<string>> list = new List<List<string>>();
            try
            {
                list = ESP.Compatible.DepartmentManager.GetListForAJAXReports(parentId);
            }
            catch (Exception e)
            {
                e.ToString();
            }

            List<string> c = new List<string>();
            c.Add("-1");
            c.Add("全部");
            list.Insert(0, c);
            return list;
        }

        [AjaxPro.AjaxMethod]
        public static string showAlert()
        {
            string PaymentReportUsers = ESP.Configuration.ConfigurationManager.SafeAppSettings["PaymentReportUsers"];

            if (alertvalue == 1 && PaymentReportUsers.IndexOf("," + showAlertUserid.ToString() + ",") >= 0 && !PaymentReportLogManager.Exists(showAlertUserid, new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0), new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 23, 59, 59)))
            {
                PaymentReportLogInfo log = new PaymentReportLogInfo();
                log.UserId = showAlertUserid;
                log.ReadTime = DateTime.Now;

                PaymentReportLogManager.Add(log);
                return "true";
            }
            else
                return "false";
        }

        private string GetBranches()
        {
            string branches = ",";
            string strwhere = string.Format(" firstfinanceid={0} or paymentAccounter={0} or projectAccounter={0}", UserID);

            IList<ESP.Finance.Entity.BranchInfo> branchlist = ESP.Finance.BusinessLogic.BranchManager.GetList(strwhere);

            foreach (ESP.Finance.Entity.BranchInfo branch in branchlist)
            {
                branches += "'" + branch.BranchCode.ToString() + "',";
            }

            return branches;
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
        private string GetDeptRight()
        {
            string deptLevel3Ids = ",";
            string whereStr = string.Format(" ARReportUsers like '%,{0},%'", CurrentUserID);
            IList<ESP.Framework.Entity.OperationAuditManageInfo> operationlist = ESP.Framework.BusinessLogic.OperationAuditManageManager.GetModelList(whereStr);

            foreach (ESP.Framework.Entity.OperationAuditManageInfo dept in operationlist)
            {
                deptLevel3Ids += dept.DepId.ToString() + ",";
            }

            return deptLevel3Ids; 
        }

        //判断是否为部门管理者
        //private string GetCostViewRight()
        //{
        //    string deptLevel3Ids = ",";
        //    string whereStr = string.Format(" CostView like ',%{0}%,'", CurrentUserID);
        //    IList<ESP.Framework.Entity.OperationAuditManageInfo> operationlist = ESP.Framework.BusinessLogic.OperationAuditManageManager.GetModelList(whereStr);

        //    foreach (ESP.Framework.Entity.OperationAuditManageInfo dept in operationlist)
        //    {
        //        deptLevel3Ids += dept.DepId.ToString() + ",";
        //    }

        //    return deptLevel3Ids;
        //}

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            //if (Validate())
            BindGrid();

        }
        private void BindGrid()
        {
            DataTable dt = BindData();
            this.gvG.DataSource = dt;
            this.gvG.DataBind();
        }

        private string GetSelectSql()
        {
            string depts = this.GetDeptRight();
            string deptsOriginal = depts;

            string branches = this.GetBranches();
            string term = string.Empty;
            if (depts == ",")
                depts = string.Empty;
            else
                depts = depts.Substring(1, depts.Length - 2);

            if (branches == ",")
                branches = string.Empty;
            else
            {
                branches = branches.Substring(1, branches.Length - 2);
                CurrentUserIsFinance = true;
            }

            string strBranchProject = "(";

            IList<ESP.Finance.Entity.BranchProjectInfo> branchProjectList = ESP.Finance.BusinessLogic.BranchProjectManager.GetList(CurrentUserID);
            if (branchProjectList != null && branchProjectList.Count > 0)
            {
                CurrentUserIsFinance = true;

                foreach (var model in branchProjectList)
                {
                    strBranchProject += string.Format("( b.branchcode='{0}' and b.groupid={1}) or ", model.BranchCode, model.DeptId);
                }

                strBranchProject = strBranchProject.Substring(0, strBranchProject.Length - 3);
                strBranchProject += ")";
            }
            else
            {
                strBranchProject = string.Empty;
            }

            if (IsJudgmentPermissions() == false)//eddy，caroline导出所有，不用区分分公司代码
            {
                if (!string.IsNullOrEmpty(branches))
                {
                    term = " and (b.branchcode in(" + branches + ")";
                    if (!string.IsNullOrEmpty(strBranchProject))
                    {
                        term += " or " + strBranchProject;
                    }
                    term += ")";
                }
                else
                {
                    if (!string.IsNullOrEmpty(strBranchProject))
                    {
                        term += " and " + strBranchProject;
                    }
                    else
                        term = string.Empty;
                }
            }
            else
            {
                CurrentUserIsFinance = true;
            }




            if (this.ddlMonth.SelectedValue != "-1")
            {
                int val = int.Parse(ddlMonth.SelectedValue);
                switch (val)
                {
                    case 1:
                        term += " and ( datediff(day,PaymentPreDate,GETDATE())<180)";
                        break;
                    case 2:
                        term += " and ( datediff(day,PaymentPreDate,GETDATE()) between 180 and 365)";
                        break;
                    case 3:
                        term += " and ( datediff(day,PaymentPreDate,GETDATE()) between 365 and 730)";
                        break;
                    case 4:
                        term += " and ( datediff(day,PaymentPreDate,GETDATE()) >730)";
                        break;
                    default:
                        break;
                }
            }

            if (!string.IsNullOrEmpty(txtBeginDate.Text) && !string.IsNullOrEmpty(txtEndDate.Text))
            {
                term += " and (PaymentFactDate between '" + txtBeginDate.Text + "' and '" + txtEndDate.Text + "')";
            }

            if (!string.IsNullOrEmpty(txtKey.Text))
            {
                term += " and (b.projectcode like '%" + txtKey.Text.Trim() + "%') ";
            }

            //仅导出未回款
            if (ddlPaid.SelectedValue == "1")
            {
                term += " and PaymentFee=0 ";
            }
            //仅导出已回款
            else if (ddlPaid.SelectedValue == "2")
            {
                term += " and PaymentFee<>0 ";
            }
            else
            {

            }

            if (!CurrentUserIsFinance)
            {
                term += " and ((a.BadDebt is null) or a.BadDebt=0) and ((a.innerRelation is null) or a.innerRelation=0)  ";
            }

            if (!string.IsNullOrEmpty(depts))
            {
                if (hidtype2.Value != "" && hidtype2.Value != "-1")
                {
                    if (deptsOriginal.IndexOf("," + hidtype2.Value + ",") < 0)
                    {
                        ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('您没有该部门查询权限！');", true);
                        return null;
                    }
                    else
                        term += " AND (b.groupid in(" + hidtype2.Value + ") or b.ApplicantUserID=" + UserID.ToString() + ")";
                }
                else if (hidtype1.Value != "" && hidtype1.Value != "-1")
                {
                    int rightCount = 0;

                    var deptlist = ESP.Framework.BusinessLogic.DepartmentManager.GetChildren(int.Parse(hidtype1.Value));

                    string ProjectAdministrativeIDs = System.Configuration.ConfigurationManager.AppSettings["ProjectAdministrativeIDs"];

                    if (deptlist != null && deptlist.Count > 0)
                    {
                        string depIds = string.Empty;

                        foreach (ESP.Framework.Entity.DepartmentInfo dept in deptlist)
                        {
                            if (deptsOriginal.IndexOf("," + dept.DepartmentID + ",") >= 0 || ProjectAdministrativeIDs.IndexOf("," + dept.DepartmentID.ToString() + ",") >= 0)
                            {
                                rightCount += 1;

                                depIds += dept.DepartmentID + ",";
                            }
                        }

                        if (rightCount == deptlist.Count)
                        {
                            depIds = depIds.TrimEnd(',');
                            term += " AND (b.groupid in(" + depIds + ") or b.ApplicantUserID=" + UserID.ToString() + ")";
                        }
                        else
                        {
                            term += " AND (b.groupid in(" + depts + ") or b.ApplicantUserID=" + UserID.ToString() + ")";
                        }
                    }
                }
                else
                {
                    term += " AND (b.groupid in(" + depts + ") or b.ApplicantUserID=" + UserID.ToString() + ")";
                }
            }
            else
            {
                if (string.IsNullOrEmpty(strBranchProject) && string.IsNullOrEmpty(branches) && IsJudgmentPermissions() == false)
                    term += " and b.ApplicantUserID=" + UserID.ToString();
                else
                {
                    if (hidtype2.Value != "" && hidtype2.Value != "-1")
                    {
                        term += " AND (b.groupid in(" + hidtype2.Value + ") or b.ApplicantUserID=" + UserID.ToString() + ")";
                    }
                    else if (hidtype1.Value != "" && hidtype1.Value != "-1")
                    {
                        var deptlist = ESP.Framework.BusinessLogic.DepartmentManager.GetChildren(int.Parse(hidtype1.Value));

                        if (deptlist != null && deptlist.Count > 0)
                        {
                            string depIds = string.Empty;

                            foreach (ESP.Framework.Entity.DepartmentInfo dept in deptlist)
                            {
                                depIds += dept.DepartmentID + ",";
                            }
                            depIds = depIds.TrimEnd(',');
                            term += " AND (b.groupid in(" + depIds + ") or b.ApplicantUserID=" + UserID.ToString() + ")";
                        }
                    }
                }
            }
            return term;
        }

        private string GetSelectSqlMonth3()
        {
            string depts = this.GetDeptRight();//应收查看权限
            string deptsOriginal = depts;

            string branches = this.GetBranches();
            string term = string.Empty;
            if (depts == ",")
                depts = string.Empty;
            else
                depts = depts.Substring(1, depts.Length - 2);

            if (branches == ",")
                branches = string.Empty;
            else
                branches = branches.Substring(1, branches.Length - 2);

            string strBranchProject = "(";

            IList<ESP.Finance.Entity.BranchProjectInfo> branchProjectList = ESP.Finance.BusinessLogic.BranchProjectManager.GetList(CurrentUserID);
            if (branchProjectList != null && branchProjectList.Count > 0)
            {
                foreach (var model in branchProjectList)
                {
                    strBranchProject += string.Format("( b.branchcode='{0}' and a.groupid={1}) or ", model.BranchCode, model.DeptId);
                }

                strBranchProject = strBranchProject.Substring(0, strBranchProject.Length - 3);
                strBranchProject += ")";
            }
            else
            {
                strBranchProject = string.Empty;
            }

            if (IsJudgmentPermissions() == false)//eddy，caroline导出所有，不用区分分公司代码
            {
                if (!string.IsNullOrEmpty(branches))
                {
                    term = " and (b.branchcode in(" + branches + ")";
                    if (!string.IsNullOrEmpty(strBranchProject))
                    {
                        term += " or " + strBranchProject;
                    }
                    term += ")";
                }
                else
                {
                    if (!string.IsNullOrEmpty(strBranchProject))
                    {
                        term += " and " + strBranchProject;
                    }
                    else
                        term = string.Empty;
                }
            }


            if (!string.IsNullOrEmpty(depts))
            {
                if (hidtype2.Value != "" && hidtype2.Value != "-1")
                {
                    if (deptsOriginal.IndexOf("," + hidtype2.Value + ",") < 0)
                    {
                        ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('您没有该部门查询权限！');", true);
                        return null;
                    }
                    else
                        term += " AND (a.groupid in(" + hidtype2.Value + "))";
                }
                else if (hidtype1.Value != "" && hidtype1.Value != "-1")
                {
                    int rightCount = 0;

                    var deptlist = ESP.Framework.BusinessLogic.DepartmentManager.GetChildren(int.Parse(hidtype1.Value));

                    string ProjectAdministrativeIDs = System.Configuration.ConfigurationManager.AppSettings["ProjectAdministrativeIDs"];

                    if (deptlist != null && deptlist.Count > 0)
                    {
                        string depIds = string.Empty;

                        foreach (ESP.Framework.Entity.DepartmentInfo dept in deptlist)
                        {
                            if (deptsOriginal.IndexOf("," + dept.DepartmentID + ",") >= 0 || ProjectAdministrativeIDs.IndexOf("," + dept.DepartmentID.ToString() + ",") >= 0)
                            {
                                rightCount += 1;

                                depIds += dept.DepartmentID + ",";
                            }
                        }

                        if (rightCount == deptlist.Count)
                        {
                            depIds = depIds.TrimEnd(',');
                            term += " AND (a.groupid in(" + depIds + "))";
                        }
                        else
                        {
                            ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('您没有该部门所有三级部门查询权限！');", true);

                            return null;
                        }
                    }
                }
                else
                {
                    term += " AND (a.groupid in(" + depts + "))";
                }
            }

            return term;
        }


        private DataTable BindData()
        {
            string term = this.GetSelectSql();
            if (!string.IsNullOrEmpty(term))
                return PaymentManager.GetPaymentReportList(term);
            else
                return null;

        }


        protected void gvG_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvG.PageIndex = e.NewPageIndex;
            BindGrid();
        }

        protected void gvG_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataRowView dr = (DataRowView)e.Row.DataItem;
                //ProjectInfo projectModel = ESP.Finance.BusinessLogic.ProjectManager.GetModelWithOutDetailList(int.Parse(dr["projectid"].ToString()));
                //PaymentInfo paymentModel = ESP.Finance.BusinessLogic.PaymentManager.GetModel(int.Parse(dr["paymentId"].ToString()));


            }
        }

        protected void btnExportForFinance_Click(object sender, EventArgs e)
        {
            string term = this.GetSelectSql();
            if (!string.IsNullOrEmpty(term))
                ESP.Finance.BusinessLogic.PaymentManager.ExportPaymentList(term, CurrentUserIsFinance, this.Response);

        }

        protected void btnExportByMonth3_Click(object sender, EventArgs e)
        {
            string term = this.GetSelectSqlMonth3();

            if (!string.IsNullOrEmpty(term))
                ESP.Finance.BusinessLogic.PaymentManager.ExportPaymentListByMonth3(term, this.Response);

        }

    }
}