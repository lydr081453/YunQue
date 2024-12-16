using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Transfer_JobTransferDetail : ESP.Web.UI.PageBase
{
    int userid = 13507;
    int jobinfoid = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(Request["jobid"]))
        {
            jobinfoid = int.Parse(Request["jobid"]);
        }
        if (!string.IsNullOrEmpty(Request["userid"]))
            userid = int.Parse(Request["userid"]);

        if (!IsPostBack)
        {
            ListBind();

        }

    }

    private void ListBind()
    {
        if (jobinfoid > 0)
        {
            ESP.HumanResource.Entity.TransferInfo job = ESP.HumanResource.BusinessLogic.TransferManager.GetModel(jobinfoid);
            if (ESP.HumanResource.Common.SalaryPermissionsHelper.IsPermissions(job.sysid.ToString(), this.ModuleInfo.ModuleID, this.UserID))
            {
                divSalary.Visible = true;
            }
            else
            {
                divSalary.Visible = false;
            } 
            ESP.HumanResource.Entity.EmployeeJobInfo employee = ESP.HumanResource.BusinessLogic.EmployeeJobManager.getModelBySysId(job.sysid);
            txtuserName.Text = job.sysUserName;
            txtJob_JoinDate.Text = employee.joinDate.ToString("yyyy-MM-dd");
                       
            string nowjobname = getJobName(job.nowCompanyName, job.nowDepartmentName, job.nowGroupName);
            
            ddlnowGroupName.Text = nowjobname;


            string newjobname = getJobName(job.newCompanyName, job.newDepartmentName, job.newGroupName);
            
            txtJob_NewGroupName.Text = !string.IsNullOrEmpty(newjobname) ? newjobname : "";
            
            txtJob_NewDuty.Text = job.newDuty;
            txtJob_TransferDate.Text = job.transferDate.ToString("yyyy-MM-dd");
            txtJob_TransferPlace.Text = job.transferPlace;
            txtJob_transferReason.Text = job.transferReason;
            txtJob_transferTimeLine.Text = job.transferTimeLine;
            txtJob_evenPlan.Text = job.evenPlan;
            //txtJob_nowAuditNote.Text = job.nowAuditNote;
            //txtJob_newAuditNote.Text = job.newAuditNote;
            //txtJob_hrAuditNote.Text = job.hrAuditNote;
            txtJob_Memo.Text = job.memo;
            txtoperationDate.Text = job.operationDate.ToString("yyyy-MM-dd");
          
            ESP.HumanResource.Entity.SnapshotsInfo snapshote = ESP.HumanResource.BusinessLogic.SnapshotsManager.GetModel(job.salaryDetailID);
            txtnowBasePay.Text = ESP.Salary.Utility.DESEncrypt.Decode(snapshote.nowBasePay);
            txtnowMeritPay.Text = ESP.Salary.Utility.DESEncrypt.Decode(snapshote.nowMeritPay);
            txtnewBasePay.Text = ESP.Salary.Utility.DESEncrypt.Decode(snapshote.newBasePay);
            txtnewMeritPay.Text = ESP.Salary.Utility.DESEncrypt.Decode(snapshote.newMeritPay);
        }
       

    }

    private string getJobName(string company, string department, string group)
    {
        string job = string.Empty;
        if (!string.IsNullOrEmpty(company))
        {
            job = "-" + company;
        }
        if (!string.IsNullOrEmpty(department))
            job = "-" + department + job;
        if (!string.IsNullOrEmpty(group))
            job = "-" + group + job;
        if (!string.IsNullOrEmpty(job))
            job = job.Substring(1, job.Length - 1);
        return job;
    }
    private string getJobID(string company, string department, string group)
    {
        string job = string.Empty;
        if (company != "0")
        {
            job = "-" + company;
            if (department != "0")
            {
                job = "-" + department + job;
                if (group != "0")
                    job = "-" + group + job;
            }
        }
        if (!string.IsNullOrEmpty(job))
            job = job.Substring(1, job.Length - 1);
        return job;
    }
    /// <summary>
    /// 返回按钮的Click事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("JobTransfer.aspx");
    }
}
