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
    public partial class GroupWageFeeSettings : ESP.Web.UI.PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            AjaxPro.Utility.RegisterTypeForAjax(typeof(Purchase_selectOperationAuditor));
            if (!IsPostBack)
            {
                BindYear();
                #region GC
                //int[] deptids = CurrentUser.GetDepartmentIDs();
                //if (deptids.Length > 0)
                //{
                //    if (IsJudgmentPermissions()) //如果方法返回True监察室或财务,否则普通用户
                //    {
                //        this.ddltype.Enabled = true;
                //        this.ddltype1.Enabled = true;
                //        this.ddltype2.Enabled = true;
                //    }
                //    else
                //    {
                //        string deptLevel3Ids = IsManager();
                //        if (!string.IsNullOrEmpty(deptLevel3Ids))
                //        {
                //            ESP.Compatible.Department currentD = ESP.Compatible.DepartmentManager.GetDepartmentByPK(deptids[0]);
                //            if (currentD.Level == 1)
                //            {
                //                hidtype.Value = currentD.UniqID.ToString();
                //            }
                //            else if (currentD.Level == 2)
                //            {
                //                hidtype1.Value = currentD.UniqID.ToString();
                //                hidtype.Value = currentD.Parent.UniqID.ToString();
                //            }
                //            else if (currentD.Level == 3)
                //            {
                //                hidtype2.Value = currentD.UniqID.ToString();
                //                hidtype1.Value = currentD.Parent.UniqID.ToString();
                //                hidtype.Value = currentD.Parent.Parent.UniqID.ToString();
                //            }
                //            this.ddltype.Enabled = true;
                //            this.ddltype1.Enabled = true;
                //            this.ddltype2.Enabled = true;
                //        }
                //    }
                //DepartmentDataBind();
                //}
                #endregion
                DepartmentDataBind();

            }
        }

        private void BindYear()
        {
            int iYearNow = DateTime.Now.Year;
            this.ddlYear.Items.Add(new ListItem("查看所有", "0"));
            for (int i = 0; i < 20; i++)
            {
                int itemYear = iYearNow - i;
                this.ddlYear.Items.Add(new ListItem(itemYear.ToString()+"年", itemYear.ToString()));
            }
        }

        private void BindSalarySettings()
        {
            string strCondition = " 1=1 ";
            if (hidtype2.Value != "-1")
                strCondition += " AND DeptID=" + hidtype2.Value;
            if (this.ddlYear.SelectedValue != "0")
                strCondition += " AND Year=" + ddlYear.SelectedValue;
            if (this.ddlMonth.SelectedValue != "0")
                strCondition += " AND Month=" + ddlMonth.SelectedValue;
            
            IList<DeptFeeInfo> list = DeptFeeManager.GetList(strCondition);
            this.gvG.DataSource = list;
            this.gvG.DataBind();
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
                ////DeptSalaryInfo info = (DeptSalaryInfo)e.Row.DataItem;
                //HiddenField hidDepID = (HiddenField)e.Row.FindControl("hidDepID");
                //Label lblDepartmentName = (Label)e.Row.FindControl("lblDepartmentName");
                //Label lblMonth = (Label)e.Row.FindControl("lblMonth");
                ////Label lblPrStatus = (Label)e.Row.FindControl("lblPrStatus");
                //if (lblDepartmentName != null && hidDepID != null && hidDepID.Value != string.Empty)
                //{
                //    ESP.Framework.Entity.DepartmentInfo det = ESP.Framework.BusinessLogic.DepartmentManager.Get(Convert.ToInt32(hidDepID.Value));
                //    if (det != null)
                //        lblDepartmentName.Text = det.DepartmentName;
                //}
            }
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            Response.Redirect("groupwagefeesettingsedit.aspx");
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            BindSalarySettings();
            //if (Validate())
            //{
                //BindGroupWage();
            //}
        }

        private bool Validate()
        {
            //if (IsJudgmentPermissions())
            //{
            //    if (this.hidtype1.Value == "-1" || string.IsNullOrEmpty(this.hidtype1.Value))
            //    {
            //        ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('请选择二级部门！');", true);
            //        return false;
            //    }
            //}
            //else if (!string.IsNullOrEmpty(IsManager()))
            //{
                if (this.hidtype2.Value == "-1" || string.IsNullOrEmpty(this.hidtype2.Value))
                {
                    ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('请选择您所管辖的三级部门！');", true);
                    return false;
                }
            //}
            //else
            //{
            //    ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('您没有本功能权限！');", true);
            //    return false;
            //}
            return true;
        }

        protected void gvG_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
        }
    }
}
