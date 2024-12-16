using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Employees_PassedCheckInDetail : ESP.Web.UI.PageBase
{
    int passedInfoId = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        //离职申请ID
        if (!string.IsNullOrEmpty(Request["passedInfoId"]))
            passedInfoId = int.Parse(Request["passedInfoId"]);
        if (!IsPostBack)
        {
            InitPage();
        }
    }

    /// <summary>
    /// 初始化页面信息
    /// </summary>
    private void InitPage()
    {
        if (passedInfoId > 0)
        {
            ESP.HumanResource.Entity.PassedInfo model = ESP.HumanResource.BusinessLogic.PassedManager.GetModel(passedInfoId);
            if (ESP.HumanResource.Common.SalaryPermissionsHelper.IsPermissions(model.sysid.ToString(), this.ModuleInfo.ModuleID, this.UserID))
            {
                divSalary.Visible = true;
            }
            else
            {
                divSalary.Visible = false;
            }  
            //SEP_PassedInfo信息
            txtuserName.Text = model.sysUserName;
            txtjoinJobDate.Text = model.joinJobDate.ToString("yyyy-MM-dd");
            txtgroupName.Text = model.groupName;
            txtdepartmentName.Text = model.departmentName;
            txtcompany.Text = model.companyName;
            labcurrentTitle.Text = model.currentTitle;
            txtoperationDate.Text = model.operationDate.ToString("yyyy-MM-dd");
            txtmemo.Text = model.memo;

            //SEP_SalaryDetailInfo信息

            ESP.HumanResource.Entity.SnapshotsInfo snapshote = ESP.HumanResource.BusinessLogic.SnapshotsManager.GetModel(model.salaryDetailID);
            txtnowBasePay.Text = ESP.Salary.Utility.DESEncrypt.Decode(snapshote.nowBasePay);
            txtnowMeritPay.Text = ESP.Salary.Utility.DESEncrypt.Decode(snapshote.nowMeritPay);
            txtnewBasePay.Text = ESP.Salary.Utility.DESEncrypt.Decode(snapshote.newBasePay);
            txtnewMeritPay.Text = ESP.Salary.Utility.DESEncrypt.Decode(snapshote.newMeritPay);
        }
    }

    /// <summary>
    /// 返回按钮的Click事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("PassedCheckInList.aspx");
    }
}
