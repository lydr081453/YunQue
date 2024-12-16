using System;
using System.Collections.Generic;
using ESP.HumanResource.BusinessLogic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Transfer_PromotionTransferDetail : ESP.Web.UI.PageBase
{
    int userid = 13507;
    int proinfoid = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(Request["proid"]))
        {
            proinfoid = int.Parse(Request["proid"]);
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
        if (proinfoid > 0)
        {
            ESP.HumanResource.Entity.PromotionInfo pro = PromotionManager.GetModel(proinfoid);
            if (ESP.HumanResource.Common.SalaryPermissionsHelper.IsPermissions(pro.sysId.ToString(), this.ModuleInfo.ModuleID, this.UserID))
            {
                divSalary.Visible = true;
            }
            else
            {
                divSalary.Visible = false;
            } 
            txtuserName.Text = pro.sysUserName;
            txtJob_JoinDate.Text = pro.joinJobDate.ToString("yyyy-MM-dd");
            
            string jobname = getJobName(pro.companyName, pro.departmentName, pro.groupName);
            ddlnowGroupName.Text = jobname;
            labnowjob.Text = pro.currentTitle;
            ddlcurrentTitle.Text = pro.targetTitle;

            txtJob_Memo.Text = pro.finalDecision;
            txtoperationDate.Text = pro.operationDate.ToString("yyyy-MM-dd");

            ESP.HumanResource.Entity.SnapshotsInfo snapshote = SnapshotsManager.GetModel(pro.salaryDetailID);
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
        Response.Redirect("PromotionTransfer.aspx");
    }
}
