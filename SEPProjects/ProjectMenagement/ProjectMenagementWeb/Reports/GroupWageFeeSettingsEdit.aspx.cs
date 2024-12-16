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
    public partial class GroupWageFeeSettingsEdit : ESP.Web.UI.PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            AjaxPro.Utility.RegisterTypeForAjax(typeof(Purchase_selectOperationAuditor));
            if (!IsPostBack)
            {
                BindYear();

                if (!string.IsNullOrEmpty(Request["salaryid"]))
                    BindSalarySettings();
                else
                    DepartmentDataBind();
            }
        }

        private void BindYear()
        {
            int iYearNow = DateTime.Now.Year;
            for (int i = 0; i < 20; i++)
            {
                int itemYear = iYearNow - i;
                this.ddlYear.Items.Add(new ListItem(itemYear.ToString() + "年", itemYear.ToString()));
            }
        }

        private void BindSalarySettings()
        {
            this.ddltype.Visible = false;
            this.ddltype1.Visible = false;
            this.ddltype2.Visible = false;
            this.lblDepartmentNameLevel1ToLevel3.Visible = true;

            DeptFeeInfo info = DeptFeeManager.GetModel(Convert.ToInt32(Request["salaryid"]));
            if (info != null)
            {
                this.ddlYear.SelectedValue = info.Year.ToString();
                this.ddlMonth.SelectedValue = info.Month.ToString();
                this.txtFeeTotal.Text = info.TotalFee.ToString("0.00");
                this.txtSalaryTotal.Text = info.TotalSalary.ToString("0.00");
                this.txtEmpAmount.Text = info.EmpAmounts.ToString();

                ESP.Framework.Entity.DepartmentInfo deptLevel3Info = ESP.Framework.BusinessLogic.DepartmentManager.Get(info.DeptId);
                ESP.Framework.Entity.DepartmentInfo deptLevel2Info = null;
                ESP.Framework.Entity.DepartmentInfo deptLevel1Info = null;
                if(deptLevel3Info != null && deptLevel3Info.ParentID > 0)
                    deptLevel2Info = ESP.Framework.BusinessLogic.DepartmentManager.Get(deptLevel3Info.ParentID);
                if(deptLevel2Info != null && deptLevel2Info.ParentID > 0)
                    deptLevel1Info = ESP.Framework.BusinessLogic.DepartmentManager.Get(deptLevel2Info.ParentID);
                if (deptLevel1Info != null)
                {
                    lblDepartmentNameLevel1ToLevel3.Text = deptLevel1Info.DepartmentName + " -- " + deptLevel2Info.DepartmentName + " -- " + deptLevel3Info.DepartmentName;
                }
            //还差部门
            }
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

        protected void btnReturn_Click(object sender, EventArgs e)
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "window.location.href='GroupWageFeeSettings.aspx';", true);
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if(Save())
                ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('保存成功，现在将返回查询页！');window.location.href='GroupWageFeeSettings.aspx';", true);
        }

        protected void btnSaveNew_Click(object sender, EventArgs e)
        {
            if (Save())
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('保存成功，您可继续录入新的记录！');window.location.href='GroupWageFeeSettingsEdit.aspx';", true);
                this.txtFeeTotal.Text = string.Empty;
                this.txtSalaryTotal.Text = string.Empty;
                this.txtEmpAmount.Text = string.Empty;
                this.ddlYear.Items.Clear();
                BindYear();
                this.ddlMonth.SelectedIndex = 0;
                this.ddltype.Visible = true;
                this.ddltype1.Visible = true;
                this.ddltype2.Visible = true;
                this.lblDepartmentNameLevel1ToLevel3.Visible = false;
                DepartmentDataBind();
            }
        }

        private bool Save()
        {
            try
            {
                if (Validate())
                {
                    IList<DeptFeeInfo> list = DeptFeeManager.GetList(" DeptID=" + this.hidtype2.Value + " AND [Year]=" + this.ddlYear.SelectedValue + " AND [Month]=" + this.ddlMonth.SelectedValue);

                    DeptFeeInfo info = new DeptFeeInfo();
                    info.Year = int.Parse(this.ddlYear.SelectedValue);
                    info.Month = int.Parse(this.ddlMonth.SelectedValue);
                    info.TotalFee = decimal.Parse(this.txtFeeTotal.Text);
                    info.TotalSalary = decimal.Parse(this.txtSalaryTotal.Text);
                    info.EmpAmounts = int.Parse(this.txtEmpAmount.Text);
                    ESP.Framework.Entity.DepartmentInfo dept = ESP.Framework.BusinessLogic.DepartmentManager.Get(int.Parse(this.hidtype2.Value));
                    if (dept != null)
                        info.DeptName = dept.DepartmentName;

                    if (!string.IsNullOrEmpty(Request["salaryid"]))
                    {
                        if (list != null && list.Count > 0 && list[0].Id.ToString() != Request["salaryid"])
                        {
                            ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('已有该部门此时间段内的数据！不能添加同样条件的数据！');", true);
                            return false;
                        }
                        info.Id = Convert.ToInt32(Request["salaryid"]);
                        DeptFeeManager.Update(info);
                        return true;
                    }
                    else
                    {
                        if (list != null && list.Count > 0)
                        {
                            ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('已有该部门此时间段内的数据！不能添加同样条件的数据！');", true);
                            return false;
                        }
                        info.DeptId = int.Parse(this.hidtype2.Value);
                        DeptFeeManager.Add(info);
                    }
                    return true;
                }
                else
                    return false;
            }
            catch
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('保存时发生错误，请联系管理员！');", true);
                return false;
            }
        }

        private bool Validate()
        {
            if (string.IsNullOrEmpty(Request["salaryid"]) && (this.hidtype2.Value == "-1" || string.IsNullOrEmpty(this.hidtype2.Value)) )
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('请选择三级部门！');", true);
                return false;
            }
            if (string.IsNullOrEmpty(this.txtEmpAmount.Text) || int.Parse(this.txtEmpAmount.Text) < 0)
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('请填写正确的人数！');", true);
                return false;
            }
            return true;
        }
    }
}