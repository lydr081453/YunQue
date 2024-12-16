using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using ESP.HumanResource.Common;
using ESP.HumanResource.Entity;
using ESP.HumanResource.BusinessLogic;

public partial class Transfer_SalaryTransferEdit : ESP.Web.UI.PageBase
{
    int userid = 13507;
    int salaryinfoid = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(Request["salaryid"]))
        {           
            salaryinfoid = int.Parse(Request["salaryid"]);
        }
        if (!string.IsNullOrEmpty(Request["userid"]))
        {
            userid = int.Parse(Request["userid"]);
        } 
        if (!IsPostBack)
        {
                     
            ddlPayChange.Items.Insert(0,new ListItem("工资上调","1"));
            ddlPayChange.Items.Insert(1, new ListItem("工资下调", "2"));
            ListBind();

        }
    }

    private void ListBind()
    {
        if (salaryinfoid > 0)
        {
            ESP.HumanResource.Entity.SalaryInfo salary = null;
            txtuserName.Text = salary.sysUserName;
            
            txtjob.Text = salary.job;
            ddlPayChange.SelectedValue = salary.payChange.ToString();
            txtoperationDate.Text = salary.operationDate.ToString("yyyy-MM-dd") == "1900-01-01" ? "" : salary.operationDate.ToString("yyyy-MM-dd");
            txtmemo.Text = salary.memo;
          
            ESP.HumanResource.Entity.SnapshotsInfo snapshots = SnapshotsManager.GetModel(salary.salaryDetailID);
            txtnowBasePay.Text = ESP.Salary.Utility.DESEncrypt.Decode(snapshots.nowBasePay);
            txtnowMeritPay.Text = ESP.Salary.Utility.DESEncrypt.Decode(snapshots.nowMeritPay);
            txtnewBasePay.Text = ESP.Salary.Utility.DESEncrypt.Decode(snapshots.newBasePay);
            txtnewMeritPay.Text = ESP.Salary.Utility.DESEncrypt.Decode(snapshots.newMeritPay);
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
            // SEP_EmployeesInPositions
            List<ESP.HumanResource.Entity.EmployeesInPositionsInfo> departments = EmployeesInPositionsManager.GetModelList(" a.UserID=" + userid);
            ESP.HumanResource.Entity.EmployeeBaseInfo model = EmployeeBaseManager.GetModel(userid);
            if (departments != null && departments.Count > 0)
            {
                List<ESP.Framework.Entity.DepartmentInfo> deps = new List<ESP.Framework.Entity.DepartmentInfo>();
                //////
                ESP.Framework.BusinessLogic.DepartmentManager.GetDepartmentListByID(departments[0].DepartmentID, deps);
                if (deps.Count == 3)
                {
                    hidcompanyName.Value = deps[0].DepartmentName;
                    hidcompanyId.Value = deps[0].DepartmentID.ToString();
                    hiddepartmentName.Value = deps[1].DepartmentName;
                    hiddepartmentId.Value = deps[1].DepartmentID.ToString();
                    hidgroupName.Value = deps[2].DepartmentName;
                    hidgroupId.Value = deps[2].DepartmentID.ToString();
                }                
            }
            if (departments != null && departments.Count > 0)
            {
                txtjob.Text = departments[0].DepartmentPositionName;
            }

            ESP.Framework.Entity.UserInfo userinfo = ESP.Framework.BusinessLogic.UserManager.Get(userid);
            txtuserName.Text = userinfo.FullNameCN;
            hiduserId.Value = userinfo.UserID.ToString();

          
            SnapshotsInfo snapshots = SnapshotsManager.GetTopModel(userid);
            txtnowBasePay.Text = txtnewBasePay.Text = ESP.Salary.Utility.DESEncrypt.Decode(snapshots.newBasePay);
            txtnowMeritPay.Text = txtnewMeritPay.Text = ESP.Salary.Utility.DESEncrypt.Decode(snapshots.newMeritPay);
        }
        
    }


    protected void btnCommit_Click(object sender, EventArgs e)
    {
        SnapshotsInfo snapshots = null;
        ESP.HumanResource.Entity.SalaryInfo model = GetNewModel(ref snapshots);
        int returnValue = 0;
        string log = UserInfo.Username + "{0}了" + model.sysUserName + "的薪资调整登记";
           
        if (salaryinfoid > 0)
        {
            returnValue = SalaryManager.Update(model, snapshots, LogManager.GetLogModel(string.Format(log, "修改"), UserInfo.UserID, UserInfo.Username, model.sysid, model.sysUserName, Status.LogSalary));
        }
        else
        {
            returnValue = SalaryManager.Add(model, snapshots, LogManager.GetLogModel(string.Format(log, "提交"), UserInfo.UserID, UserInfo.Username, model.sysid, model.sysUserName, Status.LogSalary));
        }
        if (returnValue > 0)
            ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "window.location='/HR/Employees/EmployeesAllList.aspx';alert('薪资调整登记成功！');", true);
        else
            ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('薪资调整登记失败！');", true);
    }

    private ESP.HumanResource.Entity.SalaryInfo GetNewModel(ref ESP.HumanResource.Entity.SnapshotsInfo snapshots)
    {
        ESP.HumanResource.Entity.SalaryInfo model = new ESP.HumanResource.Entity.SalaryInfo();
        if(salaryinfoid > 0)
         model = SalaryManager.GetModel(salaryinfoid);
        
        model.sysid = int.Parse(hiduserId.Value == "" ? "0" : hiduserId.Value);
        model.sysUserName = txtuserName.Text.Trim();       
        model.job = txtjob.Text.Trim();
        model.payChange = int.Parse(ddlPayChange.SelectedValue);
      
        model.operationDate = DateTime.Parse(txtoperationDate.Text.Trim());
        model.declareStatus = 0;//申报状态
        model.declarer = 0; //申报人id

        model.groupID = int.Parse(hidgroupId.Value == "" ? "0" : hidgroupId.Value);
        model.groupName = hidgroupName.Value.Trim();
        model.departmentID = int.Parse(hiddepartmentId.Value == "" ? "0" : hiddepartmentId.Value);
        model.departmentName = hiddepartmentName.Value.Trim();
        model.companyID = int.Parse(hidcompanyId.Value == "" ? "0" : hidcompanyId.Value);
        model.companyName = hidcompanyName.Value.Trim(); ;

       
        model.memo = txtmemo.Text.Trim().Length > 1000 ? txtmemo.Text.Substring(0, 1000) : txtmemo.Text.Trim();

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
