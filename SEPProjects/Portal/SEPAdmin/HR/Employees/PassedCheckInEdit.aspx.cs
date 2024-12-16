using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using ESP.HumanResource.Common;
using ESP.HumanResource.BusinessLogic;

public partial class Employees_PassedCheckInEdit : ESP.Web.UI.PageBase
{
    int passedInfoId = 0;
    int sysId = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        #region AjaxProRegister
        AjaxPro.Utility.RegisterTypeForAjax(typeof(EmployeeBaseManager));
        #endregion

        //转正申请ID
        if (!string.IsNullOrEmpty(Request["passedInfoId"]))
            passedInfoId = int.Parse(Request["passedInfoId"]);
        if (!string.IsNullOrEmpty(Request["userid"]))
            sysId = int.Parse(Request["userid"]);
        if (!IsPostBack)
        {           
            InitPage();
            drpJobBind();
        }
    }

    protected void drpJobBind()
    {
        drpJob_JoinJob.Items.Insert(0, new ListItem("请选择...", "-1"));
    }

    /// <summary>
    /// 初始化页面信息
    /// </summary>
    private void InitPage()
    {
        if (passedInfoId > 0)
        {
            ESP.HumanResource.Entity.PassedInfo model = GetModel();
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
            hiduserId.Value = model.sysid.ToString();
            txtjoinJobDate.Text = model.joinJobDate.ToString("yyyy-MM-dd");
            SelectedModuleName.Value = txtnowDepartment.Text = txtnewDepartmente.Text = getJobName(model.companyName, model.departmentName, model.groupName);
            SelectedModuleArr.Value =  getJobID(model.companyID.ToString(), model.departmentID.ToString(), model.groupID.ToString());
            txtnowTitle.Text = model.currentTitle;
         //   txtnewTitle.Text = model.currentTitle;
            txtoperationDate.Text = model.operationDate.ToString("yyyy-MM-dd");
            txtmemo.Text = model.memo;


            ESP.HumanResource.Entity.SnapshotsInfo snapshote = SnapshotsManager.GetModel(model.salaryDetailID);
            txtnowBasePay.Text = ESP.Salary.Utility.DESEncrypt.Decode(snapshote.nowBasePay);
            txtnowMeritPay.Text = ESP.Salary.Utility.DESEncrypt.Decode(snapshote.nowMeritPay);
            txtnewBasePay.Text = ESP.Salary.Utility.DESEncrypt.Decode(snapshote.newBasePay);
            txtnewMeritPay.Text = ESP.Salary.Utility.DESEncrypt.Decode(snapshote.newMeritPay);

            //部门信息
            List<ESP.HumanResource.Entity.EmployeesInPositionsInfo> departments = EmployeesInPositionsManager.GetModelList(" UserID=" + sysId);
            if (departments != null && departments.Count > 0)
            {
                hiddepartmentId.Value = departments[0].GroupID.ToString();
                //chkActing.Checked = departments[0].IsActing;
                //chkManager.Checked = departments[0].IsManager;
            }
        }
        else
        {
            if (ESP.HumanResource.Common.SalaryPermissionsHelper.IsPermissions(sysId.ToString(), this.ModuleInfo.ModuleID, this.UserID))
            {
                divSalary.Visible = true;
            }
            else
            {
                divSalary.Visible = false;
            }  
            //部门信息
            List<ESP.HumanResource.Entity.EmployeesInPositionsInfo> departments = EmployeesInPositionsManager.GetModelList(" a.UserID=" + sysId);
            string jobname = "";
            string jobid = "";            

            if (departments != null && departments.Count > 0)
            {
                ESP.Framework.Entity.UserInfo userinfo = ESP.Framework.BusinessLogic.UserManager.Get(sysId);
                this.txtuserName.Text = userinfo.FullNameCN;
                hiduserId.Value = sysId.ToString();

                List<ESP.Framework.Entity.DepartmentInfo> deps = new List<ESP.Framework.Entity.DepartmentInfo>();
                //////
                ESP.Framework.BusinessLogic.DepartmentManager.GetDepartmentListByID(departments[0].GroupID, deps);

                for (int i = 0; i < deps.Count; i++)
                {
                    jobname = "-" + deps[i].DepartmentName + jobname;
                    jobid = "-" + deps[i].DepartmentID + jobid;
                }
                if (!string.IsNullOrEmpty(jobname) && !string.IsNullOrEmpty(jobid))
                {
                    jobname = jobname.Substring(1, jobname.Length - 1);
                    jobid = jobid.Substring(1,jobid.Length - 1);
                    SelectedModuleName.Value = txtnowDepartment.Text = txtnewDepartmente.Text = jobname;
                    SelectedModuleArr.Value = jobid;                    

                    //职位下拉框
                    IList<ESP.Framework.Entity.DepartmentPositionInfo> list = ESP.Framework.BusinessLogic.DepartmentPositionManager.GetByDepartment(departments[0].GroupID);
                    for (int j = 0; j < list.Count; j++)
                    {
                        drpJob_JoinJob.Items.Add(new ListItem(list[j].DepartmentPositionName,list[j].DepartmentPositionID.ToString()));
                    }

                    ESP.Framework.Entity.DepartmentPositionInfo deppos = ESP.Framework.BusinessLogic.DepartmentPositionManager.Get(departments[0].DepartmentPositionID);
                    if (deppos != null)
                    {
                        txtnowTitle.Text = deppos.DepartmentPositionName;
                        drpJob_JoinJob.SelectedValue = deppos.DepartmentPositionID.ToString();
                        hidJob_JoinJob.Value = hidnowjob.Value = deppos.DepartmentPositionID.ToString();                        
                    }
                    hiddepartmentId.Value = departments[0].GroupID.ToString();
                }
                //chkActing.Checked = departments[0].IsActing;
                //chkManager.Checked = departments[0].IsManager;
            }

            //薪资信息
            
            ESP.HumanResource.Entity.SnapshotsInfo snapshots = SnapshotsManager.GetTopModel(sysId);
            if (snapshots != null)
            {
                txtnowBasePay.Text = txtnewBasePay.Text = ESP.Salary.Utility.DESEncrypt.Decode(snapshots.newBasePay);
                txtnowMeritPay.Text = txtnewMeritPay.Text = ESP.Salary.Utility.DESEncrypt.Decode(snapshots.newMeritPay);
            }
            else
            {
                txtnowBasePay.Text = "0";
                txtnowMeritPay.Text = "0";
            }

            //入职日期
            ESP.HumanResource.Entity.EmployeeJobInfo employeeModel = EmployeeJobManager.getModelBySysId(sysId);
            if (employeeModel != null)
            {
                txtjoinJobDate.Text = employeeModel.joinDate.ToString("yyyy-MM-dd");
            }
            else
            {
                txtjoinJobDate.Text = "1900-01-01";
            }
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
    /// 获得转正申请的对象
    /// </summary>
    /// <returns></returns>
    private ESP.HumanResource.Entity.PassedInfo GetModel()
    {
        ESP.HumanResource.Entity.PassedInfo model = new ESP.HumanResource.Entity.PassedInfo();
        if (passedInfoId > 0)
            model = PassedManager.GetModel(passedInfoId);
        return model;
    }

    /// <summary>
    /// 获得转正申请的编辑后的对象
    /// </summary>
    /// <returns></returns>
    private ESP.HumanResource.Entity.PassedInfo GetNewModel(ref ESP.HumanResource.Entity.SnapshotsInfo snapshots, ref ESP.HumanResource.Entity.EmployeesInPositionsInfo departments)
    {
        ESP.HumanResource.Entity.PassedInfo model = GetModel();

        departments = EmployeesInPositionsManager.GetModel(int.Parse(hiduserId.Value == "" ? "0" : hiduserId.Value), Convert.ToInt32(hidnowjob.Value), Convert.ToInt32(hiddepartmentId.Value));
        //SEP_PassedInfo信息
        model.sysid = int.Parse(hiduserId.Value == "" ? "0" : hiduserId.Value);
        model.sysUserName = txtuserName.Text.Trim();
        model.joinJobDate = DateTime.Parse(txtjoinJobDate.Text.Trim());

        

        string[] newjobId = SelectedModuleArr.Value.Trim().Split('-');
        string[] newjobName = SelectedModuleName.Value.Trim().Split('-');
        if (newjobId.Length == 3)
        {
            model.groupID = departments.DepartmentID = int.Parse(newjobId[0].Trim());
            model.groupName = newjobName[0].Trim();
            model.departmentID = int.Parse(newjobId[1].Trim());
            model.departmentName = newjobName[1].Trim();
            model.companyID = int.Parse(newjobId[2].Trim());
            model.companyName = newjobName[2].Trim();

            
        }
        else if (newjobId.Length == 2)
        {
            model.groupID = 0;
            model.groupName = "";
            model.departmentID = departments.DepartmentID = int.Parse(newjobId[0].Trim());
            model.departmentName = newjobName[0].Trim();
            model.companyID = int.Parse(newjobId[1].Trim());
            model.companyName = newjobName[1].Trim();
        }
        else if (newjobId.Length == 1)
        {
            model.groupID = 0;
            model.groupName = "";
            model.departmentID = 0;
            model.departmentName = "";
            model.companyID = departments.DepartmentID = int.Parse(newjobId[0].Trim());
            model.companyName = newjobName[0].Trim();
        }
        int depposid = Convert.ToInt32(hidJob_JoinJob.Value.Trim());
        ESP.Framework.Entity.DepartmentPositionInfo depposinfo = ESP.Framework.BusinessLogic.DepartmentPositionManager.Get(depposid);
        model.currentTitle = depposinfo.DepartmentPositionName;

        model.operationDate = DateTime.Parse(txtoperationDate.Text.Trim());
        model.memo = txtmemo.Text.Trim().Length > 1000 ? txtmemo.Text.Substring(0,1000) : txtmemo.Text.Trim();

        //SEP_SalaryDetailInfo信息
        snapshots = SnapshotsManager.GetModel(model.salaryDetailID);
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
        snapshots.UserID = model.sysid;
        snapshots.UserName = model.sysUserName;

        //SEP_EmployeesInPositions信息
         
        string nowjobId = hiddepartmentId.Value.Trim();
        if (!string.IsNullOrEmpty(nowjobId))
        {
            departments.DepartmentPositionID = depposid;            
            //departments.IsManager = chkManager.Checked;
            //departments.IsActing = chkActing.Checked;
        }
        return model;
    }
       



    /// <summary>
    /// 转正按钮的Click事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnCommit_Click(object sender, EventArgs e)
    {
        ESP.HumanResource.Entity.SnapshotsInfo snapshots = null;
        ESP.HumanResource.Entity.EmployeesInPositionsInfo departments = null;
        ESP.HumanResource.Entity.EmployeesInPositionsInfo oldDep = EmployeesInPositionsManager.GetModel(int.Parse(hiduserId.Value == "" ? "0" : hiduserId.Value), Convert.ToInt32(hidnowjob.Value), Convert.ToInt32(hiddepartmentId.Value));
        ESP.HumanResource.Entity.PassedInfo model = GetNewModel(ref snapshots, ref departments);
        int returnValue = 0;
        string log = UserInfo.Username + "{0}了" + model.sysUserName + "的转正登记,,部门从" + txtnowDepartment.Text
                + "调整到" + SelectedModuleName.Value.Trim() + ",职位从" + txtnowTitle.Text + "调整为" + model.currentTitle;
        if (passedInfoId > 0)
            returnValue = PassedManager.Update(model, snapshots, departments, LogManager.GetLogModel(string.Format(log, "修改"), UserInfo.UserID, UserInfo.Username, model.sysid, model.sysUserName, Status.LogPassed), true);
        else
        {
            model.creater = UserInfo.UserID;
            model.createrName = UserInfo.Username;
            model.createDate = DateTime.Now;
            returnValue = PassedManager.Add(model, snapshots, departments, oldDep, LogManager.GetLogModel(string.Format(log, " 提交"), UserInfo.UserID, UserInfo.Username, model.sysid, model.sysUserName, Status.LogPassed), true);
        }
        if (returnValue > 0)
            ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "window.location='EmployeesAllList.aspx';alert('转正登记成功！');", true);
        else
            ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('转正登记失败！');", true);
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
