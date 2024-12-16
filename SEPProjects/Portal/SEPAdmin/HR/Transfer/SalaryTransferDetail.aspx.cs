using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Transfer_SalaryTransferDetail : ESP.Web.UI.PageBase
{
    
    string salaryid = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (!string.IsNullOrEmpty(Request["salaryid"]))
            {
                salaryid = Request["salaryid"];               
                ListBind();
            }           
        }

    }

    private void ListBind()
    {
        ESP.HumanResource.Entity.SalaryInfo model = ESP.HumanResource.BusinessLogic.SalaryManager.GetModel(int.Parse(salaryid));
        if (ESP.HumanResource.Common.SalaryPermissionsHelper.IsPermissions(model.sysid.ToString(), this.ModuleInfo.ModuleID, this.UserID))
        {
            divSalary.Visible = true;
        }
        else
        {
            divSalary.Visible = false;
        }  
        labuserName.Text = model.sysUserName;  
        labPayChange.Text = model.payChange == 1 ? "工资上调" : "工资下调";
        labjob.Text = model.job;
        laboperationDate.Text = model.operationDate.ToString("yyyy-MM-dd");
        labmemo.Text = model.memo;


        ESP.HumanResource.Entity.SnapshotsInfo snap = ESP.HumanResource.BusinessLogic.SnapshotsManager.GetModel(model.salaryDetailID);
        labnowBasePay.Text = ESP.Salary.Utility.DESEncrypt.Decode(snap.nowBasePay);
        labnowMeritPay.Text = ESP.Salary.Utility.DESEncrypt.Decode(snap.nowMeritPay);
        labnewBasePay.Text = ESP.Salary.Utility.DESEncrypt.Decode(snap.newBasePay);
        labnewMeritPay.Text = ESP.Salary.Utility.DESEncrypt.Decode(snap.newMeritPay);
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("SalaryTransfer.aspx");
    }
}
