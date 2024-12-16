using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ESP.HumanResource.BusinessLogic;
using ESP.HumanResource.Common;

namespace SEPAdmin.HR.Employees
{
    public partial class RejectEdit : ESP.Web.UI.PageBase
    {
        private string userid = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Request["userid"]))
            {
                userid = Request["userid"].Trim();
            }
            if (!IsPostBack)
            {
                initForm(int.Parse(Request["userid"].Trim().ToString()));
            }
        }

        protected void initForm(int sysid)
        {
            ESP.HumanResource.Entity.EmployeeBaseInfo model = EmployeeBaseManager.GetModel(sysid);

            ESP.HumanResource.Entity.UsersInfo user = UsersManager.GetModel(sysid);

            txtEmail.Text = user.Email;
            txtItCode.Text = user.Username;
            txtTel.Text = model.Phone2;    
            txtUserId.Text = model.Code;

            drpUserType.Text = !string.IsNullOrEmpty(ESP.Framework.BusinessLogic.EmployeeManager.GetTypeName(model.TypeID)) ? ESP.Framework.BusinessLogic.EmployeeManager.GetTypeName(model.TypeID) : "正式员工";

            labBase_NameCn.Text = user.LastNameCN + user.FirstNameCN;

            labBase_Sex.Text = ESP.HumanResource.Common.Status.Gender_Names[model.Gender];
         
            try
            {
                labJob_JoinDate.Text = model.EmployeeJobInfo.joinDate.ToString("yyyy-MM-dd");
            }
            catch { }
            labJob_JoinJob.Text = model.EmployeeJobInfo.joinJob;
            if (!string.IsNullOrEmpty(model.Resume))
            {
                labResume.Text = "<a target='_blank' href='" + model.Resume + "'><img src='/Images/dc.gif' border='0' /></a>";
            }
            else
            {
                labResume.Text = "未上传简历";
            }
            if (model.OwnedPC)
                labOwnedPC.Text = "是";
            else
                labOwnedPC.Text = "否";

            labSeniority.Text = model.Seniority.ToString();  // 员工社会工龄

            labJob_CompanyName.Text = model.EmployeeJobInfo.companyName;
            labJob_DepartmentName.Text = model.EmployeeJobInfo.departmentName;
            labJob_GroupName.Text = model.EmployeeJobInfo.groupName;
            labIDCard.Text = model.IDNumber;
            try
            {
                labWorkCity.Text = ESP.Framework.BusinessLogic.DepartmentManager.Get(int.Parse(model.WorkCity)).DepartmentName;
            }
            catch (Exception ex) { }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            //修改员工状态 
            int userId = int.Parse(userid);
            ESP.HumanResource.Entity.EmployeeBaseInfo info = EmployeeBaseManager.GetModel(userId);
            info.Status = Status.Reject;
            EmployeeBaseManager.Update(info);

            ESP.HumanResource.Entity.HeadAccountInfo hc = (new ESP.HumanResource.BusinessLogic.HeadAccountManager()).GetModelByUserid(info.UserID);
            if (hc != null && hc.TalentId!=0)
            {
                var talentManager =new ESP.HumanResource.BusinessLogic.TalentManager();

                ESP.HumanResource.Entity.TalentInfo talent = talentManager.GetModel(hc.TalentId);
                talent.Status = 0;
                talentManager.Update(talent);
            }

            //记录Reject日志
            ESP.HumanResource.Entity.RejectLogInfo rejectLog = new ESP.HumanResource.Entity.RejectLogInfo();
            rejectLog.UserID = int.Parse(userid);
            rejectLog.Operator = int.Parse(CurrentUser.SysID);
            rejectLog.OperateDate = DateTime.Now;
            rejectLog.Description = txtDescription.Text.Trim();
            ESP.HumanResource.BusinessLogic.RejectLogManager.Insert(rejectLog);

            string backUrl = Request["backUrl"];
            ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('用户已归入Reject人员库！');window.location.href='"+backUrl+"';", true);
        }

        protected void btnReturn_Click(object sender, EventArgs e)
        {
            Response.Redirect(Request["backUrl"]);
        }
    }
}
