using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ESP.HumanResource.BusinessLogic;

public partial class EmployeeReadyView : ESP.Web.UI.PageBase
{
    private string userid = "";
    private bool bo = false;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (!string.IsNullOrEmpty(Request["userid"]))
            {
                userid = Request["userid"].Trim();
                initForm(int.Parse(Request["userid"].Trim().ToString()));
            }
        }
    }

    protected void initForm(int sysid)
    {
        ESP.HumanResource.Entity.EmployeeBaseInfo model = EmployeeBaseManager.GetModel(sysid);

        ESP.HumanResource.Entity.UsersInfo user = UsersManager.GetModel(sysid);

        txtEmail.Text = user.Email;
        txtItCode.Text = user.Username;
        txtTel.Text = model.Phone2;
        //if (!string.IsNullOrEmpty(Request["type"]))
        //{
        //  //  txtUserId.Text = ESP.HumanResource.BusinessLogic.UsersManager.GetDBAutoNumber("employee").ToString();
        //    txtUserId.Text = ESP.Framework.BusinessLogic.EmployeeManager.CreateEmployeeCode(); 

        //}
        //else
        //{
        //    txtUserId.Text = model.Code; 
        //}        
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

        //if (model.WageMonths == 12 || model.WageMonths == 13)
        //{
        //    labWageMonths.Text = model.WageMonths.ToString() + "个月";
        //}
        //else
        //{
        //    labWageMonths.Text = "错误月份";
        //}

        //if (model.IsForeign)
        //{
        //    labForeign.Text = "是";
        //}
        //else
        //{
        //    labForeign.Text = "否";
        //}

        //if (model.IsCertification)
        //{
        //    labCertification.Text = "有";
        //}
        //else
        //{
        //    labCertification.Text = "无";
        //}

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

        //labJob_SelfIntroduction.Text = model.EmployeeJobInfo.selfIntroduction;
        //labJob_Objective.Text = model.EmployeeJobInfo.objective;
        //labJob_WorkingExperience.Text = model.EmployeeJobInfo.workingExperience;
        //labJob_EducationalBackground.Text = model.EmployeeJobInfo.educationalBackground;
        //labJob_LanguagesAndDialect.Text = model.EmployeeJobInfo.languagesAndDialect;
        labJob_Memo.Text = model.Memo;
    }

    protected void btnReturn_Click(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(Request["type"]))
            Response.Redirect("FieldworkList.aspx");
        else
            Response.Redirect("NewEmployessList.aspx");
    }
}