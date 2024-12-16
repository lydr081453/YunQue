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
    public partial class TeamTaxList : ESP.Web.UI.PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            AjaxPro.Utility.RegisterTypeForAjax(typeof(FinanceWeb.Reports.TeamTaxList));
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
                BindData();

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


        protected void btnAdd_Click(object sender, EventArgs e)
        {
            if ((hidtype2.Value != "" && hidtype2.Value != "-1") && !string.IsNullOrEmpty(txtYear.Text) && !string.IsNullOrEmpty(txtMonth.Text) && !string.IsNullOrEmpty(txtTax.Text))
            {
                int deptid = int.Parse(hidtype2.Value);
                string dept = ESP.Framework.BusinessLogic.DepartmentManager.Get(deptid).DepartmentName;
                int year = 0;
                int month = 0;
                decimal tax = 0;

                int.TryParse(txtYear.Text, out year);
                int.TryParse(txtMonth.Text, out month);
                decimal.TryParse(txtTax.Text, out tax);

                if (deptid == 0 || deptid ==-1 || year == 0 || month == 0 || tax == 0)
                {
                    ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('请填写正确的信息！');", true);
                    return;
                }
                else
                {
                    ESP.Finance.Entity.TeamTaxInfo taxModel = new TeamTaxInfo();
                    taxModel.DepartmentId = deptid;
                    taxModel.DepartmentName = dept;
                    taxModel.TaxYear = year;
                    taxModel.TaxMonth = month;
                    taxModel.Tax = tax;

                    ESP.Finance.BusinessLogic.TeamTaxManager.Delete(deptid, year, month);
                    ESP.Finance.BusinessLogic.TeamTaxManager.Add(taxModel);

                    BindData();
                }
            }
            else
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('请填写完整信息！');", true);
                return;
            }

           
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            BindData();
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


        private void BindData()
        {
            string term = string.Empty;

            if (hidtype2.Value != "" && hidtype2.Value != "-1")
            {
                term = " departmentid=" + hidtype2.Value;
                
                if (!string.IsNullOrEmpty(txtYear.Text))
                {
                    term += " and TaxYear=" + txtYear.Text;
                }

                IList<TeamTaxInfo> taxList = ESP.Finance.BusinessLogic.TeamTaxManager.GetList(term, null);

                gvG.DataSource = taxList;
                gvG.DataBind();
            }
        }


        protected void gvG_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvG.PageIndex = e.NewPageIndex;
            BindData();
        }

        protected void gvG_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
            }
        }
    }
}