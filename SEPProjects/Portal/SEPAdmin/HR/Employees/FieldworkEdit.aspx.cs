using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using ESP.HumanResource.BusinessLogic;

namespace SEPAdmin.HR.Employees
{
    public partial class FieldworkEdit : ESP.Web.UI.PageBase
    {
        private const string savepath = "/UserImage/UserHeadImage" + "/";

        protected void Page_Load(object sender, EventArgs e)
        {
            #region AjaxProRegister
            AjaxPro.Utility.RegisterTypeForAjax(typeof(EmployeeBaseManager));
            #endregion           
            if (!IsPostBack)
            {
                initForm();                
                
            }
            drpJobBind();
        }
        protected void drpJobBind()
        {
            drpJob_JoinJob.Items.Insert(0, new ListItem("请选择...", "-1"));
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Request["userid"]))
            {
                ESP.HumanResource.Entity.EmployeeBaseInfo model = EmployeeBaseManager.GetModel(int.Parse(Request["userid"]));
                ESP.HumanResource.Entity.SnapshotsInfo snapshots = SnapshotsManager.GetTopModel(int.Parse(Request["userid"].ToString()));
                ESP.HumanResource.Entity.UsersInfo userModel = UsersManager.GetModel(model.UserID);
                ESP.HumanResource.Entity.EmployeesInPositionsInfo deps = EmployeesInPositionsManager.GetModel(model.UserID, model.EmployeeJobInfo.joinjobID);
                ESP.HumanResource.Entity.EmployeesInPositionsInfo depsOld = EmployeesInPositionsManager.GetModel(model.UserID, model.EmployeeJobInfo.joinjobID);

                initModel(ref model, ref snapshots, ref userModel,ref deps);
                ESP.HumanResource.Entity.LogInfo logModel = new ESP.HumanResource.Entity.LogInfo();
                string logmessage = userModel.IsDeleted ? "并设置为不能进入系统" : "并设置为可以进入系统";
                logModel.LogUserId = UserInfo.UserID;
                logModel.LogMedifiedTeme = DateTime.Now;
                logModel.Des = CurrentUser.Name+"修改人员[" + userModel.LastNameCN + userModel.FirstNameCN +model.IDNumber+ "]信息，" + logmessage;
                logModel.LogUserName = UserInfo.Username;

                if (EmployeeBaseManager.Update(userModel, model, deps, depsOld, snapshots, logModel))
                {
                    ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "window.location='FieldworkList.aspx';alert('修改成功！');", true);
                }
                else
                {
                    ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('修改失败,请重试！');", true);
                }
            }
            else
            {
                if (EmployeeBaseManager.checkUserCodeExists(txtItCode.Text.Trim()))
                {

                    ShowCompleteMessage("登录名已存在,请重新输入", "FieldworkEdit.aspx");
                }
                else
                {
                    ESP.HumanResource.Entity.EmployeeBaseInfo model = null;
                    ESP.HumanResource.Entity.SnapshotsInfo snapshots = null;
                    ESP.HumanResource.Entity.UsersInfo userModel = null;
                    ESP.HumanResource.Entity.EmployeesInPositionsInfo deps = null;

                    initModel(ref model, ref snapshots, ref userModel,ref deps);
                    string logmessage = userModel.IsDeleted ? "并设置为不能进入系统" : "并设置为可以进入系统";
                    ESP.HumanResource.Entity.LogInfo logModel = new ESP.HumanResource.Entity.LogInfo();
                    logModel.LogUserId = UserInfo.UserID;
                    logModel.LogMedifiedTeme = DateTime.Now;
                    logModel.Des = "添加实习/兼职人员[" + userModel.LastNameCN + userModel.FirstNameCN + "]信息，" + logmessage;
                    logModel.LogUserName = UserInfo.Username;

                    if (EmployeeBaseManager.Add(model, userModel,deps, snapshots, logModel))
                    {
                        try
                        {
                            ESP.Framework.BusinessLogic.UserManager.ResetPassword(userModel.Username, "password");
                        }
                        catch (System.Exception)
                        {

                        }
                        ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "window.location='FieldworkList.aspx';alert('修改成功！');", true);
                    }
                    else
                    {
                        ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('修改失败,请重试！');", true);
                    }
                }
 
            }
        }

        protected void initForm()
        {
            if (!string.IsNullOrEmpty(Request["userid"]))
            {
                ESP.HumanResource.Entity.EmployeeBaseInfo model = EmployeeBaseManager.GetModel(int.Parse(Request["userid"]));

                ESP.HumanResource.Entity.UsersInfo userModel = UsersManager.GetModel(int.Parse(Request["userid"]));

                chkBaseInfoOk.Checked = model.BaseInfoOK;
                chkContractInfoOk.Checked = model.ContractInfoOK;

                //是否能进入公司系统                
                chkIsDeleted.Checked = (!userModel.IsDeleted);

                //登录名
                txtItCode.Text = userModel.Username;
                //员工编号
                txtUserId.Text = model.Code;
                txtBase_FitstNameCn.Text = userModel.FirstNameCN;
                txtBase_LastNameCn.Text = userModel.LastNameCN;
                //英文姓名
                txtBase_FirstNameEn.Text = userModel.FirstNameEN;
                txtBase_LastNameEn.Text = userModel.LastNameEN;


                    txtBase_Sex.SelectedValue = model.Gender.ToString();
            
                try
                {
                    //入职日期                    
                    txtJob_JoinDate.Text = model.EmployeeJobInfo.joinDate.Year < 1901 ? "" : model.EmployeeJobInfo.joinDate.ToString("yyyy-MM-dd");
                }
                catch { }
                //部门
                txtJob_CompanyName.Text = model.EmployeeJobInfo.companyName;
                hidCompanyId.Value = model.EmployeeJobInfo.companyid.ToString();
                txtJob_DepartmentName.Text = model.EmployeeJobInfo.departmentName;
                hidDepartmentID.Value = model.EmployeeJobInfo.departmentid.ToString();
                txtJob_GroupName.Text = model.EmployeeJobInfo.groupName;
                hidGroupId.Value = model.EmployeeJobInfo.groupid.ToString();
                //职位
                ESP.HumanResource.Entity.EmployeesInPositionsInfo deps = EmployeesInPositionsManager.GetModel(model.UserID, model.EmployeeJobInfo.joinjobID);
                drpJob_JoinJob.DataSource = (List<ESP.Framework.Entity.DepartmentPositionInfo>)ESP.Framework.BusinessLogic.DepartmentPositionManager.GetByDepartment(deps.DepartmentID);
                drpJob_JoinJob.DataTextField = "DepartmentPositionName";
                drpJob_JoinJob.DataValueField = "DepartmentPositionID";
                drpJob_JoinJob.DataBind();

                try
                {
                    drpJob_JoinJob.SelectedValue = deps.DepartmentPositionID.ToString();
                }
                catch { }

                txtJob_JoinJob.Value = deps.DepartmentPositionID.ToString();


                //上级主管姓名
                txtJob_DirectorName.Text = model.EmployeeJobInfo.directorName;
                //上级主管职位
                txtJob_DirectorJob.Text = model.EmployeeJobInfo.directorJob;
                //备注
                txtJob_Memo.Text = model.Memo;

                try
                {
                    //出生日期
                    txtBase_Birthday.Text = model.Birthday.Year < 1901 ? "" : model.Birthday.ToString("yyyy-MM-dd");
                }
                catch { }
                //籍贯
                txtBase_PlaceOfBirth.Text = model.BirthPlace;
                //户口所在地
                txtBase_DomicilePlace.Text = model.DomicilePlace;
                //毕业院校
                txtBase_FinishSchool.Text = model.GraduateFrom;
                //毕业时间
                txtBase_FinishSchoolDate.Text = model.GraduatedDate.ToString("yyyy-MM-dd") == "1754-01-01" ? "" : model.GraduatedDate.ToString("yyyy-MM-dd");
                //学历
                txtBase_Education.SelectedValue = model.Education;
                //专业
                txtBase_Speciality.Text = model.Major;

                //照片
                if (!string.IsNullOrEmpty(model.Photo))
                {
                    imgBase_Photo.ImageUrl = savepath + model.Photo;
                }
                else
                {
                    imgBase_Photo.ImageUrl = "../../public/CutImage/image/man.GIF";
                }

                //工作特长
                txtBase_WorkSpecialty.Text = model.WorkSpecialty;
                //身份证号码
                txtBase_IdNo.Text = model.IDNumber;
                //紧急事件联系人
                txtBase_EmergencyLinkman.Text = model.EmergencyContact;
                //健康状况
                txtBase_Health.Text = model.Health;
                //婚否                
                txtBase_Marriage.SelectedValue = model.MaritalStatus.ToString();
                 

                //紧急事件联系人电话
                txtBase_EmergencyPhone.Text = model.EmergencyContactPhone;
                //IP电话
                txtBase_IPCode.Text = model.IPCode;
                //联系电话
                txtBase_Phone1.Text = model.Phone1;
                //手机
                txtBase_MobilePhone.Text = model.MobilePhone;
                //宅电
                txtBase_HomePhone.Text = model.HomePhone;
                //通讯地址
                txtBase_Address1.Text = model.Address;
                //邮政编码
                txtBase_PostCode.Text = model.PostCode;
                //电子邮件
                txtEmail.Text = userModel.Email;

                //协议公司
                txtContract_Company.Text = model.EmployeeWelfareInfo.contractCompany;

                try
                {
                    //协议起始日
                    txtContract_StartDate.Text = model.EmployeeWelfareInfo.contractStartDate.Year < 1901 ? "" : model.EmployeeWelfareInfo.contractStartDate.ToString("yyyy-MM-dd");
                }
                catch { }


                //备注
                txtContract_Memo.Text = model.EmployeeWelfareInfo.memo;

                ESP.HumanResource.Entity.SnapshotsInfo snapshote = SnapshotsManager.GetTopModel(model.UserID);
                if (snapshote != null)
                {
                    //本年度工资所属
                    try
                    {
                        txtBase_thisYearSalary.Text = model.ThisYearSalary.Trim();
                    }
                    catch { }
                    try
                    {
                        //固定工资
                        txtJob_basePay.Text = ESP.Salary.Utility.DESEncrypt.Decode(!string.IsNullOrEmpty(snapshote.newBasePay.Trim()) ? snapshote.newBasePay.Trim() : "0");
                    }
                    catch { }

                }
                linkPhoto.NavigateUrl = "UpLoadUserPhoto.aspx?userid=" + Request["userid"];
            }
            else
            {
                linkPhoto.Visible = false;
            }
        }

        protected void initModel(ref ESP.HumanResource.Entity.EmployeeBaseInfo model, ref ESP.HumanResource.Entity.SnapshotsInfo snapshots, ref ESP.HumanResource.Entity.UsersInfo userModel,ref ESP.HumanResource.Entity.EmployeesInPositionsInfo deps)
        {
            
            if (snapshots == null)
            {
                snapshots = new ESP.HumanResource.Entity.SnapshotsInfo();
            }
            if (model == null)
            {
                model = new ESP.HumanResource.Entity.EmployeeBaseInfo();
                model.EmployeeJobInfo = new ESP.HumanResource.Entity.EmployeeJobInfo();
                model.EmployeeWelfareInfo = new ESP.HumanResource.Entity.EmployeeWelfareInfo();
                model.TypeID = 4;//实习人员
            }
            if (userModel == null)
            {
                userModel = new ESP.HumanResource.Entity.UsersInfo();               

                userModel.CreatedDate = DateTime.Now;
                userModel.LastActivityDate = DateTime.Now;

                model.Status =  userModel.Status = ESP.HumanResource.Common.Status.Fieldword; //待入职
                userModel.Password = "password";
                userModel.PasswordSalt = "password";

                userModel.IsApproved = false;
                userModel.IsLockedOut = false;

                //填表日期
                model.CreatedTime = DateTime.Now;
                 
                
            }
            if (deps == null)
            {
                deps = new ESP.HumanResource.Entity.EmployeesInPositionsInfo();
            }
            //员工编号
            snapshots.Code = model.Code = txtUserId.Text.Trim();
            model.BaseInfoOK = chkBaseInfoOk.Checked;
            model.ContractInfoOK = chkContractInfoOk.Checked;  
            
            //是否能进入公司系统
            userModel.IsDeleted = (!chkIsDeleted.Checked);

            //员工登录名
            userModel.Username = txtItCode.Text.Trim();

            //中文姓名
            userModel.FirstNameCN = txtBase_FitstNameCn.Text.Trim();
            userModel.LastNameCN = txtBase_LastNameCn.Text.Trim();
            //英文姓名
            userModel.FirstNameEN = txtBase_FirstNameEn.Text.Trim();
            userModel.LastNameEN = txtBase_LastNameEn.Text.Trim();

            //性别
           snapshots.Gender = model.Gender = int.Parse(txtBase_Sex.SelectedValue);

            try
            {
                //入职日期
                model.EmployeeJobInfo.joinDate = DateTime.Parse(txtJob_JoinDate.Text.Trim());
            }
            catch { }                
            //上级主管姓名
            model.EmployeeJobInfo.directorName = txtJob_DirectorName.Text.Trim();
            //上级主管职位
            model.EmployeeJobInfo.directorJob = txtJob_DirectorJob.Text.Trim();


            if (snapshots != null)
            {
                //工资所属公司
                try
                {
                    snapshots.ThisYearSalary = model.ThisYearSalary = txtBase_thisYearSalary.Text.Trim();
                }
                catch { }
                try
                {
                    //固定工资
                    snapshots.newBasePay = ESP.Salary.Utility.DESEncrypt.Encode(!string.IsNullOrEmpty(txtJob_basePay.Text.Trim()) ? txtJob_basePay.Text.Trim() : "0");
                }
                catch { }
                try
                {
                    //标准绩效
                    snapshots.newMeritPay = ESP.Salary.Utility.DESEncrypt.Encode("0");
                }
                catch { }
                try
                {
                    //固定工资
                    snapshots.nowBasePay = ESP.Salary.Utility.DESEncrypt.Encode(!string.IsNullOrEmpty(txtJob_basePay.Text.Trim()) ? txtJob_basePay.Text.Trim() : "0");
                }
                catch { }
                try
                {
                    //标准绩效
                    snapshots.nowMeritPay = ESP.Salary.Utility.DESEncrypt.Encode("0");
                }
                catch { }

                snapshots.UserID = model.UserID;                    
                snapshots.UserName = UserInfo.FullNameCN.Trim();
                snapshots.Creator = UserInfo.UserID;
                snapshots.CreatorName = UserInfo.Username.Trim();
                snapshots.CreatedTime = DateTime.Now;
            }
            //备注
            model.Memo = txtJob_Memo.Text.Trim();
                        
                           
            try
            {
                //出生日期
                model.Birthday = DateTime.Parse(txtBase_Birthday.Text.Trim());
            }
            catch { }
            //籍贯
            model.BirthPlace = txtBase_PlaceOfBirth.Text.Trim();
            //户口所在地
            model.DomicilePlace = txtBase_DomicilePlace.Text.Trim();
            //毕业院校
            snapshots.GraduatedFrom = model.GraduateFrom = txtBase_FinishSchool.Text.Trim();
            //毕业时间
            try
            {
                model.GraduatedDate = DateTime.Parse(txtBase_FinishSchoolDate.Text.Trim());
            }
            catch { }
            //学历
            snapshots.Education = model.Education = txtBase_Education.SelectedValue;
            //专业
            snapshots.Major = model.Major = txtBase_Speciality.Text.Trim();

            //工作特长
            model.WorkSpecialty = txtBase_WorkSpecialty.Text.Trim();
            //身份证号码
            model.IDNumber = txtBase_IdNo.Text.Trim();
            //紧急事件联系人
            model.EmergencyContact = txtBase_EmergencyLinkman.Text.Trim();
            //健康状况
            model.Health = txtBase_Health.Text.Trim();
            //婚否
            snapshots.MaritalStatus = model.MaritalStatus = int.Parse(txtBase_Marriage.SelectedValue);
            //紧急事件联系人电话
            model.EmergencyContactPhone = txtBase_EmergencyPhone.Text.Trim();
            ////IP电话
            //model.IPCode = txtIPCode.Text.Trim();
            //联系电话
            model.Phone1 = txtBase_Phone1.Text.Trim();
            //手机
            model.MobilePhone = txtBase_MobilePhone.Text.Trim();
            //宅电
            model.HomePhone = txtBase_HomePhone.Text.Trim();
            //通讯地址
            model.Address = txtBase_Address1.Text.Trim();
            //邮政编码
            model.PostCode = txtBase_PostCode.Text.Trim();
            //电子邮件
            userModel.Email = txtEmail.Text.Trim();
            

            //合同公司
            model.EmployeeWelfareInfo.contractCompany = txtContract_Company.Text.Trim();
            try
            {
                //合同起始日
                snapshots.contractStartDate = model.EmployeeWelfareInfo.contractStartDate = DateTime.Parse(txtContract_StartDate.Text.Trim());
            }
            catch { }
            //备注
            model.EmployeeWelfareInfo.memo = txtContract_Memo.Text.Trim();

            #region 人员部门职务信息

            deps.UserID = model.UserID;
            deps.DepartmentPositionID = int.Parse(txtJob_JoinJob.Value);
            deps.IsActing = false;
            deps.IsManager = false;
            if (!string.IsNullOrEmpty(txtJob_CompanyName.Text.Trim()) && !string.IsNullOrEmpty(hidCompanyId.Value))
            {
                deps.DepartmentID = int.Parse(hidCompanyId.Value);
                model.EmployeeJobInfo.companyid = int.Parse(hidCompanyId.Value);
                model.EmployeeJobInfo.companyName = txtJob_CompanyName.Text.Trim();

            }
            if (!string.IsNullOrEmpty(txtJob_DepartmentName.Text.Trim()) && !string.IsNullOrEmpty(hidDepartmentID.Value))
            {
                deps.DepartmentID = int.Parse(hidDepartmentID.Value);
                model.EmployeeJobInfo.departmentid = int.Parse(hidDepartmentID.Value);
                model.EmployeeJobInfo.departmentName = txtJob_DepartmentName.Text.Trim();

            }
            if (!string.IsNullOrEmpty(txtJob_GroupName.Text.Trim()) && !string.IsNullOrEmpty(hidGroupId.Value))
            {
                deps.DepartmentID = int.Parse(hidGroupId.Value);
                model.EmployeeJobInfo.groupid = int.Parse(hidGroupId.Value);
                model.EmployeeJobInfo.groupName = txtJob_GroupName.Text.Trim();
            }
            model.EmployeeJobInfo.joinJob = ESP.Framework.BusinessLogic.DepartmentPositionManager.Get(int.Parse(txtJob_JoinJob.Value)).DepartmentPositionName;
            model.EmployeeJobInfo.joinjobID = int.Parse(txtJob_JoinJob.Value);


            #endregion

            //养老/失业/工伤/生育缴费基数
            snapshots.socialInsuranceBase = model.EmployeeWelfareInfo.socialInsuranceBase = ESP.Salary.Utility.DESEncrypt.Encode("0");
            //医疗基数
            snapshots.medicalInsuranceBase = model.EmployeeWelfareInfo.medicalInsuranceBase = ESP.Salary.Utility.DESEncrypt.Encode("0");
            //公积金基数
            snapshots.publicReserveFundsBase = model.EmployeeWelfareInfo.publicReserveFundsBase = ESP.Salary.Utility.DESEncrypt.Encode("0");

            try
            {
                //养老保险公司比例
                snapshots.EIProportionOfFirms = model.EmployeeWelfareInfo.EIProportionOfFirms = decimal.Parse( "0");
                //养老保险个人比例
                snapshots.EIProportionOfIndividuals = model.EmployeeWelfareInfo.EIProportionOfIndividuals = decimal.Parse( "0");
                //失业保险公司比例
                snapshots.UIProportionOfFirms = model.EmployeeWelfareInfo.UIProportionOfFirms = decimal.Parse("0");
                //失业保险个人比例
                snapshots.UIProportionOfIndividuals = model.EmployeeWelfareInfo.UIProportionOfIndividuals = decimal.Parse("0");
                //生育保险公司比例
                snapshots.BIProportionOfFirms = model.EmployeeWelfareInfo.BIProportionOfFirms = decimal.Parse("0");
                //生育保险个人比例
                snapshots.BIProportionOfIndividuals = model.EmployeeWelfareInfo.BIProportionOfIndividuals = decimal.Parse("0");
                //工伤险公司比例
                snapshots.CIProportionOfFirms = model.EmployeeWelfareInfo.CIProportionOfFirms = decimal.Parse("0");
                //工伤险个人比例
                snapshots.CIProportionOfIndividuals = model.EmployeeWelfareInfo.CIProportionOfIndividuals = decimal.Parse("0");
                //医疗保险公司比例
                snapshots.MIProportionOfFirms = model.EmployeeWelfareInfo.MIProportionOfFirms = decimal.Parse("0");
                //医疗保险个人比例
                snapshots.MIProportionOfIndividuals = model.EmployeeWelfareInfo.MIProportionOfIndividuals = decimal.Parse("0");
                //医疗保险大额医疗个人支付额
                snapshots.MIBigProportionOfIndividuals = model.EmployeeWelfareInfo.MIBigProportionOfIndividuals = decimal.Parse("0");
                //公积金比例
                snapshots.PRFProportionOfFirms = snapshots.PRFProportionOfIndividuals =
                    model.EmployeeWelfareInfo.PRFProportionOfFirms = model.EmployeeWelfareInfo.PRFProportionOfIndividuals = decimal.Parse("0");
            }
            catch { }

        }

        protected void btnReturn_Click(object sender, EventArgs e)
        {
            Response.Redirect("FieldList.aspx");
        }
   


        private void GetModels(ESP.HumanResource.Entity.SnapshotsInfo snap)
        {
            ESP.HumanResource.Entity.EmployeeBaseInfo ebi = EmployeeBaseManager.GetModel(int.Parse(Request["userid"].ToString()));
            if (ebi != null)
            {
                snap.UserID = ebi.UserID;
                snap.Code = ebi.Code;
                snap.TypeID = ebi.TypeID;
                snap.MaritalStatus = ebi.MaritalStatus;
                snap.Gender = ebi.Gender;
                snap.Birthday = ebi.Birthday;
                snap.Degree = ebi.Degree;
                snap.Education = ebi.Education;
                snap.GraduatedFrom = ebi.GraduateFrom;
                snap.Major = ebi.Major;
                snap.ThisYearSalary = ebi.ThisYearSalary;
                snap.Status = ebi.Status;
            }
        }  
    }
}
