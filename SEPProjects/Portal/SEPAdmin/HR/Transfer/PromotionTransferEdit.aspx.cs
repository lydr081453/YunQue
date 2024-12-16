using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using ESP.HumanResource.Common;
using ESP.HumanResource.BusinessLogic;

public partial class Transfer_PromotionTransferEdit : ESP.Web.UI.PageBase
{
    int userid = 13507;
    int proinfoid = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        #region AjaxProRegister
        AjaxPro.Utility.RegisterTypeForAjax(typeof(EmployeeBaseManager));
        #endregion

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
            ESP.HumanResource.Entity.EmployeeJobInfo employee = EmployeeJobManager.getModelBySysId(pro.sysId);
            txtuserName.Text = pro.sysUserName;
            txtJob_JoinDate.Text = employee.joinDate.ToString("yyyy-MM-dd");         
                       
            
            txtJob_Memo.Text = pro.finalDecision;
            txtoperationDate.Text = pro.operationDate.ToString("yyyy-MM-dd");
            hiduserId.Value = pro.sysId.ToString();


            ESP.HumanResource.Entity.SnapshotsInfo snapshots = SnapshotsManager.GetModel(pro.salaryDetailID);
            hidnowBasePay.Value = txtnewBasePay.Text = txtnowBasePay.Text = ESP.Salary.Utility.DESEncrypt.Decode(snapshots.newBasePay);
            hidnowMeritPay.Value = txtnewMeritPay.Text = txtnowMeritPay.Text = ESP.Salary.Utility.DESEncrypt.Decode(snapshots.newMeritPay);
        }
        else
        {
            if (ESP.HumanResource.Common.SalaryPermissionsHelper.IsPermissions(userid.ToString(), this.ModuleInfo.ModuleID, this.UserID))
            {
                divSalary.Visible = true;
            }
            else
            {
                divSalary.Visible = false;
            } 

            ESP.HumanResource.Entity.EmployeeJobInfo employeejob = EmployeeJobManager.getModelBySysId(userid);
            if (employeejob != null)
            {
                txtJob_JoinDate.Text = employeejob.joinDate.ToString("yyyy-MM-dd");
            }
            else
            {
                txtJob_JoinDate.Text = "1900-01-01";
            }
            ESP.Framework.Entity.UserInfo userinfo = ESP.Framework.BusinessLogic.UserManager.Get(userid);
            this.txtuserName.Text = userinfo.FullNameCN;
            hiduserId.Value = userid.ToString();       


            List<ESP.HumanResource.Entity.EmployeesInPositionsInfo> departments = EmployeesInPositionsManager.GetModelList(" a.UserID=" + userid);
            

            if (departments != null && departments.Count > 0)
            {
                for (int j = 0; j < departments.Count; j++)
                {
                    List<ESP.Framework.Entity.DepartmentInfo> deps = new List<ESP.Framework.Entity.DepartmentInfo>();
                    //////
                    ESP.Framework.BusinessLogic.DepartmentManager.GetDepartmentListByID(departments[j].DepartmentID, deps);
                    string jobname = "";
                    string nowjob = "";
                    string jobid = "";
                    for (int i = 0; i < deps.Count; i++)
                    {
                        jobname = "-" + deps[i].DepartmentName + jobname;
                        jobid = "-" + deps[i].DepartmentID + jobid;
                      
                    }
                    jobname = jobname.Substring(1, jobname.Length - 1);
                    jobid = jobid.Substring(1, jobid.Length - 1);
                    nowjob = departments[j].DepartmentPositionName;
                    ddlnowGroupName.Items.Add(new ListItem(jobname, nowjob + "," + jobid + "," + departments[j].DepartmentPositionID.ToString()));
                }                
            }

            ESP.HumanResource.Entity.SnapshotsInfo snapshots = SnapshotsManager.GetTopModel(userid);
            hidnowBasePay.Value = txtnewBasePay.Text = txtnowBasePay.Text = ESP.Salary.Utility.DESEncrypt.Decode(snapshots.newBasePay);
            hidnowMeritPay.Value = txtnewMeritPay.Text = txtnowMeritPay.Text = ESP.Salary.Utility.DESEncrypt.Decode(snapshots.newMeritPay);

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


    protected void btnCommit_Click(object sender, EventArgs e)
    {
        ESP.HumanResource.Entity.SnapshotsInfo snapshots = null;
        ESP.HumanResource.Entity.EmployeesInPositionsInfo departments = null;
        ESP.HumanResource.Entity.EmployeesInPositionsInfo depsOld = EmployeesInPositionsManager.GetModel(int.Parse(hiduserId.Value == "" ? "0" : hiduserId.Value), int.Parse(ddlnowGroupName.SelectedItem.Value.Trim().Split(',')[2])); ;
        ESP.HumanResource.Entity.PromotionInfo model = GetNewModel(ref snapshots, ref departments);
        int returnValue = 0;
        string log =UserInfo.Username + "{0}了" + model.sysUserName + "的职位调整登记,职位从"+model.currentTitle+"调整为"
            + model.targetTitle ;
        if (proinfoid > 0)
        {
            returnValue = PromotionManager.Update(model, snapshots, departments, LogManager.GetLogModel(string.Format(log, "修改"), UserInfo.UserID, UserInfo.Username, model.sysId, model.sysUserName, Status.LogPromotion), true);
        }
        else
        {
            model.creater = snapshots.Creator = UserInfo.UserID;
            snapshots.CreatorName = UserInfo.Username;
            model.createDate = snapshots.CreatedTime = DateTime.Now;            
            returnValue = PromotionManager.Add(model, snapshots, departments, depsOld, LogManager.GetLogModel(string.Format(log, "提交"), UserInfo.UserID, UserInfo.Username, model.sysId, model.sysUserName, Status.LogPromotion), true);
        }
        if (returnValue > 0)
            ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "window.location='/HR/Employees/EmployeesAllList.aspx';alert('职位调整登记成功！');", true);
        else
            ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('职位调整登记失败！');", true);
    }

    private ESP.HumanResource.Entity.PromotionInfo GetNewModel(ref ESP.HumanResource.Entity.SnapshotsInfo snapshots, ref ESP.HumanResource.Entity.EmployeesInPositionsInfo departments)
    {
        ESP.HumanResource.Entity.PromotionInfo model = new ESP.HumanResource.Entity.PromotionInfo();
        
        if (proinfoid > 0)
            model = PromotionManager.GetModel(proinfoid);        

        model.sysId = int.Parse(hiduserId.Value == "" ? "0" : hiduserId.Value);
        model.sysUserName = txtuserName.Text.Trim();

        string[] nowjobId = ddlnowGroupName.SelectedItem.Value.Trim().Split(',')[1].Split('-');
        string[] nowjobName = ddlnowGroupName.SelectedItem.Text.Trim().Split('-');
        
        if (nowjobId != null && nowjobName != null)
        {            
            if (nowjobId.Length == 3)
            {
                model.groupID = int.Parse(nowjobId[0].Trim());
                model.groupName = nowjobName[0].Trim();
                model.departmentID = int.Parse(nowjobId[1].Trim());
                model.departmentName = nowjobName[1].Trim();
                model.companyID = int.Parse(nowjobId[2].Trim());
                model.companyName = nowjobName[2].Trim();


            }
            else if (nowjobId.Length == 2)
            {
                model.groupID = 0;
                model.groupName = "";
                model.departmentID = int.Parse(nowjobId[0].Trim());
                model.departmentName = nowjobName[0].Trim();
                model.companyID = int.Parse(nowjobId[1].Trim());
                model.companyName = nowjobName[1].Trim();
            }
            else if (nowjobId.Length == 1)
            {
                model.groupID = 0;
                model.groupName = "";
                model.departmentID = 0;
                model.departmentName = "";
                model.companyID = int.Parse(nowjobId[0].Trim());
                model.companyName = nowjobName[0].Trim();
            }
        }
        model.targetTitle = hidcurrentName.Value;
        model.currentTitle = ddlnowGroupName.SelectedItem.Value.Trim().Split(',')[0];
        model.joinJobDate = DateTime.Parse(txtJob_JoinDate.Text.Trim());
        model.finalDecision = txtJob_Memo.Text.Length > 500 ? txtJob_Memo.Text.Substring(0, 500) : txtJob_Memo.Text;

        model.operationDate = DateTime.Parse(txtoperationDate.Text.Trim());


        //SEP_SalaryDetailInfo信息
        
        if (string.IsNullOrEmpty(Request["userid"]))
            snapshots = new ESP.HumanResource.Entity.SnapshotsInfo();
        else
        {
            snapshots = SnapshotsManager.GetTopModel(int.Parse(Request["userid"].ToString()));
        }
        snapshots.nowBasePay = ESP.Salary.Utility.DESEncrypt.Encode(txtnowBasePay.Text.Trim());
        snapshots.nowMeritPay = ESP.Salary.Utility.DESEncrypt.Encode(txtnowMeritPay.Text.Trim());
        snapshots.newBasePay = ESP.Salary.Utility.DESEncrypt.Encode(txtnewBasePay.Text.Trim());
        snapshots.newMeritPay = ESP.Salary.Utility.DESEncrypt.Encode(txtnewMeritPay.Text.Trim());
        snapshots.Creator = UserInfo.UserID;
        snapshots.CreatorName = UserInfo.Username;
        snapshots.CreatedTime = DateTime.Now;
        snapshots.UserID = model.sysId;
        snapshots.UserName = model.sysUserName;


        // SEP_EmployeesInPositions信息        
        departments = EmployeesInPositionsManager.GetModel(model.sysId, int.Parse(ddlnowGroupName.SelectedItem.Value.Trim().Split(',')[2]));

        departments.DepartmentPositionID = int.Parse(hidcurrentTitle.Value);

        return model;

    }

    /// <summary>
    /// 返回按钮的Click事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("/HR/Employees/EmployeesAllList.aspx");
    }

   
   
}
