using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ESP.HumanResource.BusinessLogic;

public partial class JoinFormPrint : ESP.Web.UI.PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            InitForm();            
        }
    }

    protected void InitForm()
    {
        ESP.HumanResource.Entity.EmployeeBaseInfo model = EmployeeBaseManager.GetModel(UserID);
        ESP.HumanResource.Entity.UsersInfo userModel = UsersManager.GetModel(UserID);

        //if (model.EmployeeJobInfo.companyid == 19)
        //    labGroupName.Text = model.EmployeeJobInfo.departmentName;
        //else
        //    labGroupName.Text = model.EmployeeJobInfo.companyName;
        labUserCode.Text = model.Code;  //员工编号
        //labCreateDate.Text = model.CreatedTime.ToString("yyyy-MM-dd");  // 填表日期
        labUserName.Text = userModel.LastNameCN + userModel.FirstNameCN;  // 姓名
        //if (model.Gender == ESP.Framework.Entity.Gender.Male)
        //    labSex.Text = "男";
        //else if (model.Gender == ESP.Framework.Entity.Gender.Female)
        //    labSex.Text = "女";
        //else
        //    labSex.Text = "未知";
        IList<ESP.HumanResource.Entity.EmployeesInPositionsInfo> positionList = ESP.HumanResource.BusinessLogic.EmployeesInPositionsManager.GetModelList(" e.userid="+model.UserID.ToString());
        if (positionList != null && positionList.Count > 0)
        {
            ESP.HumanResource.Entity.EmployeesInPositionsInfo positionModel = positionList[0];
            labUserCode.Text = positionModel.CompanyName + "-" + positionModel.DepartmentName + "-" + positionModel.GroupName + "  /  " + positionModel.DepartmentPositionName;
        }
        labBirthday.Text = model.Birthday.Year < 1901 ? "" : model.Birthday.ToString("yyyy-MM-dd");  // 出生日期
        labPlaceOfBirth.Text = model.BirthPlace;  // 籍贯
        labDomicilePlace.Text = model.DomicilePlace;  // 户口所在地
        labFinishSchool.Text = model.GraduateFrom;  // 毕业学校
        labFinishSchoolDate.Text = model.GraduatedDate.Year < 1901 ? "" : model.GraduatedDate.ToString("yyyy-MM-dd");  // 毕业时间
        labEducation.Text = model.Education;  // 学历
        labSpeciality.Text = model.Major;  // 专业
        labJoinDate.Text = model.EmployeeJobInfo.joinDate.Year < 1901 ? "" : model.EmployeeJobInfo.joinDate.ToString("yyyy-MM-dd");  // 入职日期
        
        labWorkSpecialty.Text = model.WorkSpecialty;  // 工作特长
        labIDNumber.Text = model.IDNumber;  // 身份证号码
        labHealth.Text = model.Health;  // 健康状况
        // 婚否/有无子女
        labMarriage.Text = ESP.HumanResource.Common.Status.MaritalStatus_Names[model.MaritalStatus];
              
        labEmergencyLinkman.Text = model.EmergencyContact;  // 紧急事件联系人
        labEmergencyPhone.Text = model.EmergencyContactPhone;  // 紧急事件联系人电话
        labMobilePhone.Text = model.MobilePhone;  // 手机
        labHomePhone.Text = model.HomePhone;  // 宅电
        labWorkExperience.Text = model.WorkExperience;  // 工作简历
        labMemo.Text = model.Memo;  // 备注
        labPeiOu.Text = model.MateName;
        labAddress.Text = model.Address;
        labPostCode.Text = model.PostCode;  // 邮政编码
        labAddress1.Text = model.AdrressNow;
        labPostCode2.Text = model.PostCodeNow;
        lblFamilly.Text = model.FamillyDesc;
        lblResidence.Text = model.Residence;
    }
}