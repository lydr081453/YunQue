using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using ESP.HumanResource.WebPages;

namespace SEPAdmin.HR.Statistics
{
    public partial class EmployeesChangeDetail : ESP.Web.UI.PageBase
    {
        private const string savepath = "/UserImage/UserHeadImage" + "/";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!string.IsNullOrEmpty(Request["userid"]))
                {
                    if (ESP.HumanResource.Common.SalaryPermissionsHelper.IsPermissions(Request["userid"].Trim(), this.ModuleInfo.ModuleID, this.UserID))
                    {
                        divSalary.Visible = true;
                        divSalary2.Visible = true;                        
                    }
                    else
                    {
                        divSalary.Visible = false;
                        divSalary2.Visible = false;                        
                    }
                    if (ESP.Framework.BusinessLogic.PermissionManager.HasModulePermission("011", this.ModuleInfo.ModuleID, this.UserID))
                    {
                        divBase.Visible = true;
                    }
                    else
                    {
                        divBase.Visible = false;
                    }
                    initForm();
                    DepListBind();
                }

            }
        }
        protected void initForm()
        {
            if (!string.IsNullOrEmpty(Request["userid"]))
            {
                ESP.HumanResource.Entity.EmployeeBaseInfo model = ESP.HumanResource.BusinessLogic.EmployeeBaseManager.GetModel(int.Parse(Request["userid"]));
                ESP.HumanResource.Entity.UsersInfo userModel = ESP.HumanResource.BusinessLogic.UsersManager.GetModel(int.Parse(Request["userid"]));


                //员工编号
                labUserCode.Text = model.Code;
                labCommonName.Text = model.CommonName;
                ////中文姓名

                labFullNameCn.Text = userModel.LastNameCN + userModel.FirstNameCN;
                //英文姓名
                labFullNameEn.Text = userModel.FirstNameEN + " " + userModel.LastNameEN;              
                

                labBase_Sex.Text = ESP.HumanResource.Common.Status.Gender_Names[model.Gender];
              
                //简历附件
                if (!string.IsNullOrEmpty(model.Resume))
                {
                    labResume.Text = "<a target='_blank' href='" + model.Resume + "'><img src='/Images/dc.gif' border='0' /></a>";
                    labResume.Visible = true;
                }
                else
                {
                    labResume.Text = "无附件";
                }             
                try
                {
                    //入职日期                   
                    labJob_JoinDate.Text = model.EmployeeJobInfo.joinDate.Year < 1901 ? "" : model.EmployeeJobInfo.joinDate.ToString("yyyy-MM-dd");
                }
                catch { }
                //上级主管姓名
                labJob_DirectorName.Text = model.EmployeeJobInfo.directorName;
                //上级主管职位
                labJob_DirectorJob.Text = model.EmployeeJobInfo.directorJob;
                //备注
                labJob_Memo.Text = model.Memo;
                try
                {
                    //出生日期
                    labBase_Birthday.Text = model.Birthday.Year < 1901 ? "" : model.Birthday.ToString("yyyy-MM-dd");
                }
                catch { }
                //籍贯
                labBase_PlaceOfBirth.Text = model.BirthPlace;
                //户口所在地
                labBase_DomicilePlace.Text = model.DomicilePlace;
                //毕业院校
                labBase_FinishSchool.Text = model.GraduateFrom;
                //毕业时间
                labBase_FinishSchoolDate.Text = model.GraduatedDate.ToString("yyyy-MM-dd") == "1754-01-01" ? "" : model.GraduatedDate.ToString("yyyy-MM-dd");
                //学历
                labBase_Education.Text = model.Education;
                //专业
                labBase_Speciality.Text = model.Major;

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
                labBase_WorkSpecialty.Text = model.WorkSpecialty;
                //身份证号码
                labBase_IdNo.Text = model.IDNumber;
                //紧急事件联系人
                labBase_EmergencyLinkman.Text = model.EmergencyContact;
                //健康状况
                labBase_Health.Text = model.Health;
                //婚否                

                labBase_Marriage.Text = ESP.HumanResource.Common.Status.MaritalStatus_Names[model.MaritalStatus];
                     

                //紧急事件联系人电话
                labBase_EmergencyPhone.Text = model.EmergencyContactPhone;
                //IP电话
                labBase_IPCode.Text = model.IPCode;
                //联系电话
                labBase_Phone1.Text = model.Phone1;
                //手机
                labBase_MobilePhone.Text = model.MobilePhone;
                //宅电
                labBase_HomePhone.Text = model.HomePhone;
                //通讯地址
                labBase_Address1.Text = model.Address;
                //邮政编码
                labBase_PostCode.Text = model.PostCode;
                //电子邮件
                labBase_Email.Text = userModel.Email;
                //工作简历
                labBase_WorkExperience.Text = model.WorkExperience;
                //合同公司
                labContract_Company.Text = model.EmployeeWelfareInfo.contractCompany;                
                try
                {
                    //合同起始日
                    labContract_StartDate.Text = model.EmployeeWelfareInfo.contractStartDate.Year < 1901 ? "" : model.EmployeeWelfareInfo.contractStartDate.ToString("yyyy-MM-dd");
                }
                catch { }
                try
                {
                    //合同终止日
                    labContract_EndDate.Text = model.EmployeeWelfareInfo.contractEndDate.Year < 1901 ? "" : model.EmployeeWelfareInfo.contractEndDate.ToString("yyyy-MM-dd");
                }
                catch { }                
                try
                {
                    //试用期截止日
                    labContract_ProbationPeriodDeadLine.Text = model.EmployeeWelfareInfo.probationPeriodDeadLine.Year < 1901 ? "" : model.EmployeeWelfareInfo.probationPeriodDeadLine.ToString("yyyy-MM-dd");
                }
                catch { }
                //社保所属地点
                //lsbContract_SocialInsuranceAddress.Text = model.EmployeeWelfareInfo.socialInsuranceAddress;
                try
                {
                    //转正日期
                    labContract_ProbationEndDate.Text = model.EmployeeWelfareInfo.probationEndDate.Year < 1901 ? "" : model.EmployeeWelfareInfo.probationEndDate.ToString("yyyy-MM-dd");
                }
                catch { }                
                //备注
                labContract_Memo.Text = model.EmployeeWelfareInfo.memo;

                ESP.HumanResource.Entity.SnapshotsInfo snapshote = ESP.HumanResource.BusinessLogic.SnapshotsManager.GetTopModel(model.UserID);
                if (snapshote != null)
                {
                    //本年度工资所属
                    try
                    {
                        labBase_thisYearSalary.Text = model.ThisYearSalary.Trim();
                    }
                    catch { }
                    try
                    {
                        //固定工资
                        labJob_basePay.Text = ESP.Salary.Utility.DESEncrypt.Decode(snapshote.newBasePay);
                    }
                    catch { }
                    try
                    {
                        //标准绩效
                        labJob_meritPay.Text = ESP.Salary.Utility.DESEncrypt.Decode(snapshote.newMeritPay);
                    }
                    catch { }
                }

                labRenewalCount.Text = model.EmployeeWelfareInfo.contractRenewalCount.ToString();
                labJContract_ContractInfo.Text = model.EmployeeWelfareInfo.contractSignInfo;
                //养老保险
                if (model.EmployeeWelfareInfo.endowmentInsurance)
                    labInsurance_Endowment.Text = "是";
                else
                    labInsurance_Endowment.Text = "否";
                
                //公积金
                if (model.EmployeeWelfareInfo.publicReserveFunds)
                    labPublicReserveFunds_IsPublicReserveFunds.Text = "是";
                else
                    labPublicReserveFunds_IsPublicReserveFunds.Text = "否";
                //户口所在地
                labInsurance_MemoPlace.Text = model.EmployeeWelfareInfo.InsurancePlace;
                //社保所在公司
                labInsurance_SocialInsuranceCompany.Text = model.EmployeeWelfareInfo.socialInsuranceCompany;
                //社保所属地点
                labInsurance_SocialInsuranceAddress.Text = model.EmployeeWelfareInfo.socialInsuranceAddress;                
                //医疗编号
                //labInsurance_MedicalInsuranceCode.Text = model.EmployeeWelfareInfo.medicalInsuranceCode;
                //养老/失业/工伤/生育缴费基数
                labInsurance_SocialInsuranceBase.Text = ESP.Salary.Utility.DESEncrypt.Decode(!string.IsNullOrEmpty(model.EmployeeWelfareInfo.socialInsuranceBase)?model.EmployeeWelfareInfo.socialInsuranceBase.ToString():"0");
                //医疗基数
                labInsurance_MedicalInsuranceBase.Text = ESP.Salary.Utility.DESEncrypt.Decode(!string.IsNullOrEmpty(model.EmployeeWelfareInfo.medicalInsuranceBase)?model.EmployeeWelfareInfo.medicalInsuranceBase.ToString():"0");


                
                //公积金基数
                labPublicReserveFunds_Base.Text = ESP.Salary.Utility.DESEncrypt.Decode(!string.IsNullOrEmpty(model.EmployeeWelfareInfo.publicReserveFundsBase)?model.EmployeeWelfareInfo.publicReserveFundsBase.ToString():"0");

                //养老保险公司比例
                labEIProportionOfFirms.Text = model.EmployeeWelfareInfo.EIProportionOfFirms.ToString();
                //养老保险个人比例
                labEIProportionOfIndividuals.Text = model.EmployeeWelfareInfo.EIProportionOfIndividuals.ToString();
                //失业保险公司比例
                labUIProportionOfFirms.Text = model.EmployeeWelfareInfo.UIProportionOfFirms.ToString();
                //失业保险个人比例
                labUIProportionOfIndividuals.Text = model.EmployeeWelfareInfo.UIProportionOfIndividuals.ToString();
                //生育保险公司比例
                labBIProportionOfFirms.Text = model.EmployeeWelfareInfo.BIProportionOfFirms.ToString();
                //工伤险公司比例
                labCIProportionOfFirms.Text = model.EmployeeWelfareInfo.CIProportionOfFirms.ToString();
                //医疗保险公司比例
                labMIProportionOfFirms.Text = model.EmployeeWelfareInfo.MIProportionOfFirms.ToString();
                //医疗保险个人比例
                labMIProportionOfIndividuals.Text = model.EmployeeWelfareInfo.MIProportionOfIndividuals.ToString();
                //医疗保险大额医疗个人支付额
                labMIBigProportionOfIndividuals.Text = model.EmployeeWelfareInfo.MIBigProportionOfIndividuals.ToString();
                //公积金比例
                labPRFProportionOfFirms.Text = model.EmployeeWelfareInfo.PRFProportionOfFirms.ToString();
                
                labArchive_ArchivePlace.Text = model.EmployeeWelfareInfo.ArchivePlace;
                
                //存档日期
                labArchive_ArchiveDate.Text = model.EmployeeWelfareInfo.ArchiveDate;
                //档案编号
                labArchive_Code.Text = model.EmployeeWelfareInfo.ArchiveCode;

                try
                {
                    //养老保险开始时间
                    if (model.EmployeeWelfareInfo.endowmentInsuranceStarTime.Year > 1910)
                        labEndowmentStarTime.Text = model.EmployeeWelfareInfo.endowmentInsuranceStarTime.ToString("yyyy-MM");
                }
                catch { }
                //try
                //{
                //    //失业保险开始时间        
                //    if (model.EmployeeWelfareInfo.unemploymentInsuranceStarTime.Year > 1910)
                //        labUnemploymentStarTime.Text = model.EmployeeWelfareInfo.unemploymentInsuranceStarTime.ToString("yyyy-MM-dd");
                //}
                //catch { }
                //try
                //{
                //    //生育险开始时间
                //    if (model.EmployeeWelfareInfo.birthInsuranceStarTime.Year > 1910)
                //        labBirthStarTime.Text = model.EmployeeWelfareInfo.birthInsuranceStarTime.ToString("yyyy-MM-dd");
                //}
                //catch { }
                //try
                //{
                //    //工伤险开始时间
                //    if (model.EmployeeWelfareInfo.compoInsuranceStarTime.Year > 1910)
                //        labCompoStarTime.Text = model.EmployeeWelfareInfo.compoInsuranceStarTime.ToString("yyyy-MM-dd");
                //}
                //catch { }
                //try
                //{
                //    //医疗保险开始时间
                //    if (model.EmployeeWelfareInfo.medicalInsuranceStarTime.Year > 1910)
                //        labMedicalStarTime.Text = model.EmployeeWelfareInfo.medicalInsuranceStarTime.ToString("yyyy-MM-dd");
                //}
                //catch { }
                try
                {
                    //公积金开始时间
                    if (model.EmployeeWelfareInfo.publicReserveFundsStarTime.Year > 1910)
                        labPublicReserveFundsStarTime.Text = model.EmployeeWelfareInfo.publicReserveFundsStarTime.ToString("yyyy-MM");
                }
                catch { }

                try
                {
                    //养老保险结束时间
                    if (model.EmployeeWelfareInfo.endowmentInsuranceEndTime.Year > 1910)
                        labEndowmentEndTime.Text = model.EmployeeWelfareInfo.endowmentInsuranceEndTime.ToString("yyyy-MM");
                }
                catch { }
                try
                {
                    //公积金开始时间
                    if (model.EmployeeWelfareInfo.publicReserveFundsEndTime.Year > 1910)
                        labPublicReserveFundsEndTime.Text = model.EmployeeWelfareInfo.publicReserveFundsEndTime.ToString("yyyy-MM");
                }
                catch { }

                if (model.WageMonths == 12 || model.WageMonths == 13)
                {
                    labWageMonths.Text = model.WageMonths.ToString() + "个月";
                }
                else
                {
                    labWageMonths.Text = "错误月份";
                }

                if (model.IsForeign)
                {
                    labForeign.Text = "是";
                }
                else
                {
                    labForeign.Text = "否";
                }

                if (model.IsCertification)
                {
                    labCertification.Text = "有";
                }
                else
                {
                    labCertification.Text = "无";
                }

                if (model.EmployeeWelfareInfo.ComplementaryMedical)
                {
                    try
                    {
                        labComplementaryMedical.Text = model.EmployeeWelfareInfo.ComplementaryMedicalCosts.Value.ToString("0.00") + "元";
                    }
                    catch
                    {
                        labComplementaryMedical.Text = "0.00元";
                    }
                }
                else
                {
                    labComplementaryMedical.Text = "没有补充医疗";
                }
                if (model.EmployeeWelfareInfo.AccidentInsurance)
                {
                    string value = "";
                    if (model.EmployeeWelfareInfo.AccidentInsuranceBeginTime > DateTime.Parse("1900-01-01"))
                    {
                        value += "从"+model.EmployeeWelfareInfo.AccidentInsuranceBeginTime.Year.ToString() + "年" + model.EmployeeWelfareInfo.AccidentInsuranceBeginTime.Month.ToString()+"月开始";
                    }
                    if (model.EmployeeWelfareInfo.AccidentInsuranceEndTime > DateTime.Parse("1900-01-01"))
                    {
                        value += "至" + model.EmployeeWelfareInfo.AccidentInsuranceEndTime.Year.ToString() + "年" + model.EmployeeWelfareInfo.AccidentInsuranceEndTime.Month.ToString() + "月结束";
                    }
                    labAccidentInsurance.Text = value;
                }

            }
        }

        private void DepListBind()
        {            
           List<ESP.HumanResource.Entity.EmployeesInPositionsInfo> list = ESP.HumanResource.BusinessLogic.EmployeesInPositionsManager.GetModelList(" a.userID = " + Request["userid"].ToString());
           gvList.DataSource = list;
           gvList.DataBind();

           DataSet passds = ESP.HumanResource.BusinessLogic.PassedManager.GetList(" sysid=" + Request["userid"].ToString());
           gvPassList.DataSource = passds;
           gvPassList.DataBind();

           DataSet salaryds = ESP.HumanResource.BusinessLogic.SalaryManager.GetList("and a.sysid=" + Request["userid"].ToString());
           gvSalaryList.DataSource = salaryds;
           gvSalaryList.DataBind();

           //DataSet jobds = ESP.HumanResource.BusinessLogic.TransferManager.GetList("and sysid=" + Request["userid"].ToString());
           //gvJobList.DataSource = jobds;
           //gvJobList.DataBind();

           DataSet promds = ESP.HumanResource.BusinessLogic.PromotionManager.GetList("and sysid=" + Request["userid"].ToString());
           gvPromList.DataSource = promds;
           gvPromList.DataBind();

           DataSet dimds = ESP.HumanResource.BusinessLogic.DimissionManager.GetList(" userId=" + Request["userid"].ToString());
           gvDimList.DataSource = dimds;
           gvDimList.DataBind();
        }

        /// <summary>
        /// 返回按钮的Click事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnBack_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Request["back"]) && Request["back"] == "1")
            {
                Response.Redirect("/HR/Statistics/EmployeesChangeList.aspx");
            }
            else if (!string.IsNullOrEmpty(Request["back"]) && Request["back"] == "2")
            {
                Response.Redirect("/HR/Employees/DimissionApplyList.aspx");
            }
            else
            {
                Response.Redirect("/HR/Employees/EmployeesAllList.aspx");
            }
        }
    }
}
