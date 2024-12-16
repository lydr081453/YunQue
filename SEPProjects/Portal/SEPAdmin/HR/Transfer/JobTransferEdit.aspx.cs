using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using ESP.HumanResource.Common;
using ESP.HumanResource.BusinessLogic;

public partial class Transfer_JobTransferEdit : ESP.Web.UI.PageBase
{
    int userid = 13507;
    int jobinfoid = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        #region AjaxProRegister
        AjaxPro.Utility.RegisterTypeForAjax(typeof(EmployeeBaseManager));
        #endregion

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
            ESP.HumanResource.Entity.TransferInfo job = TransferManager.GetModel(jobinfoid);
            if (ESP.HumanResource.Common.SalaryPermissionsHelper.IsPermissions(job.sysid.ToString(), this.ModuleInfo.ModuleID, this.UserID))
            {
                divSalary.Visible = true;
            }
            else
            {
                divSalary.Visible = false;
            }  
            ESP.HumanResource.Entity.EmployeeJobInfo employee = EmployeeJobManager.getModelBySysId(job.sysid);
            txtuserName.Text = job.sysUserName;
            txtJob_JoinDate.Text = employee.joinDate.ToString("yyyy-MM-dd");

            string newjobname = getJobName(job.newCompanyName,job.newDepartmentName,job.newGroupName);
            string newjobid = getJobID(job.newCompanyID.ToString(),job.newDepartmentID.ToString(),job.newGroupID.ToString());
            txtJob_NewGroupName.Text = !string.IsNullOrEmpty(newjobname) ? newjobname : "";
            hiddepartmentId.Value = !string.IsNullOrEmpty(newjobid) ? newjobid : "";
            SelectedModuleName.Value = newjobname;
            SelectedModuleArr.Value = newjobid;
           // txtJob_NewDuty.Text = job.newDuty;
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
            hiduserId.Value = job.sysid.ToString();

            ESP.HumanResource.Entity.SnapshotsInfo snapshots = SnapshotsManager.GetModel(job.salaryDetailID);
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
            ESP.Framework.Entity.UserInfo userinfo = ESP.Framework.BusinessLogic.UserManager.Get(userid);
            txtuserName.Text = userinfo.FullNameCN;
            hiduserId.Value = userinfo.UserID.ToString();

            //入职日期
            ESP.HumanResource.Entity.EmployeeJobInfo employeeModel = EmployeeJobManager.getModelBySysId(userid);            
            if (employeeModel != null)
            {
                txtJob_JoinDate.Text = employeeModel.joinDate.ToString("yyyy-MM-dd");
            }

            List<ESP.HumanResource.Entity.EmployeesInPositionsInfo> Positions = EmployeesInPositionsManager.GetModelList(" a.UserID=" + userid);
              
            if (Positions != null && Positions.Count > 0)
            {
                for (int j = 0; j < Positions.Count; j++)
                {
                    string jobname = "";
                    string jobid = "";
                    List<ESP.Framework.Entity.DepartmentInfo> deps = new List<ESP.Framework.Entity.DepartmentInfo>();
                    //////
                    ESP.Framework.BusinessLogic.DepartmentManager.GetDepartmentListByID(Positions[j].DepartmentID,  deps);

                    for (int i = 0; i < deps.Count; i++)
                    {
                        jobname = "-" + deps[i].DepartmentName + jobname;
                        jobid = "-" + deps[i].DepartmentID + jobid;
                    }
                    jobname = jobname.Substring(1, jobname.Length - 1);
                    jobid = jobid.Substring(1, jobid.Length - 1);
                    ddlnowGroupName.Items.Add(new ListItem(jobname,Positions[j].DepartmentPositionID+","+jobid));
                }
                

                //添加“新增部门”
                ddlnowGroupName.Items.Add(new ListItem("新增部门","department"));
                
            }

            //职位下拉框
            IList<ESP.Framework.Entity.DepartmentPositionInfo> list = ESP.Framework.BusinessLogic.DepartmentPositionManager.GetByDepartment(Positions[0].DepartmentID);
            for (int j = 0; j < list.Count; j++)
            {
                drpJob_JoinJob.Items.Add(new ListItem(list[j].DepartmentPositionName, list[j].DepartmentPositionID.ToString()));
            }
            drpJob_JoinJob.SelectedValue = Positions[0].DepartmentPositionID.ToString();
           
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
            job = job.Substring(1,job.Length-1);
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
            job = job.Substring(1, job.Length-1);
        return job;
    }


    protected void btnCommit_Click(object sender, EventArgs e)
    {
        ESP.HumanResource.Entity.SnapshotsInfo snapshots = null;
        ESP.HumanResource.Entity.EmployeesInPositionsInfo departments = null;
        ESP.HumanResource.Entity.EmployeesInPositionsInfo depOld = null;
        //判断是否是新增部门、删除部门
        if (ddlnowGroupName.SelectedValue == "department" && string.IsNullOrEmpty(hiddepartmentId.Value))
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('新业务团队必填');", true);
            return;
        }
        if (ddlnowGroupName.SelectedValue != "department")
        {
            depOld = EmployeesInPositionsManager.GetModel(int.Parse(hiduserId.Value == "" ? "0" : hiduserId.Value), Convert.ToInt32(ddlnowGroupName.SelectedItem.Value.Trim().Split(',')[0]));
        }
        if (string.IsNullOrEmpty(hiddepartmentId.Value))
        {
            List<ESP.HumanResource.Entity.EmployeesInPositionsInfo> listeip = EmployeesInPositionsManager.GetModelList(string.Format(" a.userid={0}", hiduserId.Value == "" ? "0" : hiduserId.Value));
            if (listeip.Count <= 1)
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('员工必须拥有至少一个部门');", true);
            }
        }
        else
        {
            ESP.HumanResource.Entity.EmployeesInPositionsInfo eip = EmployeesInPositionsManager.GetModel(int.Parse(hiduserId.Value == "" ? "0" : hiduserId.Value), Convert.ToInt32(hidJob_JoinJob.Value.Trim()), Convert.ToInt32(hiddepartmentId.Value.Trim().Split('-')[0]));
            if (eip != null)
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('员工不能调整到已有部门');", true);
                return;
            }
        }
        ESP.HumanResource.Entity.TransferInfo model = GetNewModel(ref snapshots, ref departments);
        int returnValue = 0;
        string log = UserInfo.Username + "{0}了" + model.sysUserName + "的部门调整登记,部门从" + model.nowGroupName + "-" + model.nowDepartmentName + "-" + model.nowCompanyName
                + "调整到" + SelectedModuleName.Value ;
        if (jobinfoid > 0)
        {            
            returnValue = TransferManager.Update(model, snapshots, departments, LogManager.GetLogModel(string.Format(log, "修改"), UserInfo.UserID, UserInfo.Username, model.sysid, model.sysUserName, Status.LogJob), true);
        }
        else
        {
            model.creater = snapshots.Creator = UserInfo.UserID;
            snapshots.CreatorName = UserInfo.Username;
            model.createDate = snapshots.CreatedTime = DateTime.Now;            

            returnValue = TransferManager.Add(model, snapshots, departments, depOld, LogManager.GetLogModel(string.Format(log, "提交"), UserInfo.UserID, UserInfo.Username, model.sysid, model.sysUserName, Status.LogJob), true);
        }
        if (returnValue > 0)
            ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "window.location='/HR/Employees/EmployeesAllList.aspx';alert('部门调整登记成功！');", true);
        else
            ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('部门调整登记失败！');", true);
    }

    private ESP.HumanResource.Entity.TransferInfo GetNewModel(ref ESP.HumanResource.Entity.SnapshotsInfo snapshots, ref ESP.HumanResource.Entity.EmployeesInPositionsInfo departments)
    {
        ESP.HumanResource.Entity.TransferInfo model = new ESP.HumanResource.Entity.TransferInfo();
       
       
        
        if (jobinfoid > 0)
            model = TransferManager.GetModel(jobinfoid);
        

        model.sysid = int.Parse(hiduserId.Value == "" ? "0" : hiduserId.Value);
        model.sysUserName = txtuserName.Text.Trim();


        if (ddlnowGroupName.SelectedValue != "department")
        {
            departments = EmployeesInPositionsManager.GetModel(int.Parse(hiduserId.Value == "" ? "0" : hiduserId.Value), Convert.ToInt32(ddlnowGroupName.SelectedItem.Value.Trim().Split(',')[0]));

            string[] nowjobId = ddlnowGroupName.SelectedItem.Value.Trim().Split(',')[1].Split('-');
            string[] nowjobName = ddlnowGroupName.SelectedItem.Text.Trim().Split('-');
            if (nowjobId.Length == 3)
            {
                model.nowGroupID = int.Parse(nowjobId[0].Trim());
                model.nowGroupName = nowjobName[0].Trim();
                model.nowDepartmentID = int.Parse(nowjobId[1].Trim());
                model.nowDepartmentName = nowjobName[1].Trim();
                model.nowCompanyID = int.Parse(nowjobId[2].Trim());
                model.nowCompanyName = nowjobName[2].Trim();


            }
            else if (nowjobId.Length == 2)
            {
                model.nowGroupID = 0;
                model.nowGroupName = "";
                model.nowDepartmentID = int.Parse(nowjobId[0].Trim());
                model.nowDepartmentName = nowjobName[0].Trim();
                model.nowCompanyID = int.Parse(nowjobId[1].Trim());
                model.nowCompanyName = nowjobName[1].Trim();
            }
            else if (nowjobId.Length == 1)
            {
                model.nowGroupID = 0;
                model.nowGroupName = "";
                model.nowDepartmentID = 0;
                model.nowDepartmentName = "";
                model.nowCompanyID = int.Parse(nowjobId[0].Trim());
                model.nowCompanyName = nowjobName[0].Trim();
            }
        }
        else
        {
            departments = new ESP.HumanResource.Entity.EmployeesInPositionsInfo();
            departments.UserID = int.Parse(hiduserId.Value == "" ? "0" : hiduserId.Value);
            departments.UserName = txtuserName.Text.Trim();
        }

        if (!string.IsNullOrEmpty(hiddepartmentId.Value))
        {
            string[] newjobName;
            newjobName = SelectedModuleName.Value.Trim().Split('-');
            string[] newjobId = hiddepartmentId.Value.Trim().Split('-');
            if (newjobId.Length == 3)
            {
                model.newGroupID = departments.DepartmentID = int.Parse(newjobId[0].Trim());
                model.newGroupName = newjobName[0].Trim();
                model.newDepartmentID = int.Parse(newjobId[1].Trim());
                model.newDepartmentName = newjobName[1].Trim();
                model.newCompanyID = int.Parse(newjobId[2].Trim());
                model.newCompanyName = newjobName[2].Trim();
            }
            else if (newjobId.Length == 2)
            {
                model.newGroupID = 0;
                model.newGroupName = "";
                model.newDepartmentID = departments.DepartmentID = int.Parse(newjobId[0].Trim());
                model.newDepartmentName = newjobName[0].Trim();
                model.newCompanyID = int.Parse(newjobId[1].Trim());
                model.newCompanyName = newjobName[1].Trim();
            }
            else if (newjobId.Length == 1)
            {
                model.newGroupID = 0;
                model.newGroupName = "";
                model.newDepartmentID = 0;
                model.newDepartmentName = "";
                model.newCompanyID = departments.DepartmentID = int.Parse(newjobId[0].Trim());
                model.newCompanyName = newjobName[0].Trim();
            }
            model.newDuty = drpJob_JoinJob.SelectedItem.Text.Trim();

            //SEP_EmployeesInPositions信息        
            int depposid = Convert.ToInt32(hidJob_JoinJob.Value.Trim());
            departments.DepartmentPositionID = depposid;
            //departments.IsManager = chkManager.Checked;
            //departments.IsActing = chkActing.Checked;
        }
        
        model.transferDate = DateTime.Parse(txtJob_TransferDate.Text.Trim());
        model.transferPlace = txtJob_TransferPlace.Text.Trim();
        model.transferReason = txtJob_transferReason.Text.Trim();
        model.transferTimeLine = txtJob_transferTimeLine.Text.Trim();
        model.evenPlan = txtJob_evenPlan.Text.Trim();
        //model.nowAuditNote = txtJob_nowAuditNote.Text.Trim();
        model.nowAuditDate = DateTime.Now;
        //model.newAuditNote = txtJob_newAuditNote.Text.Trim();
        model.newAuditDate = DateTime.Now;
        //model.hrAuditNote = txtJob_hrAuditNote.Text.Trim();
        model.hrAuditDate = DateTime.Now;
        model.memo = txtJob_Memo.Text.Length > 50 ? txtJob_Memo.Text.Substring(0, 50) : txtJob_Memo.Text;

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
        snapshots.UserID = model.sysid;
        snapshots.UserName = model.sysUserName;

        
       

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
