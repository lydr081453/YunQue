using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using ESP.HumanResource.BusinessLogic;
using ESP.HumanResource.WebPages;

public partial class Employees_EmployeesEdit : ViewPage
{
    private const string savepath = "/UserImage/UserHeadImage" + "/";
    private string userid = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        #region AjaxProRegister
        AjaxPro.Utility.RegisterTypeForAjax(typeof(EmployeeBaseManager));
        #endregion
        if (!IsPostBack)
        {
            if (!string.IsNullOrEmpty(Request["userid"]))
            {
                if (ESP.HumanResource.Common.SalaryPermissionsHelper.IsPermissions(Request["userid"].Trim(), this.ModuleInfo.ModuleID, this.UserID))
                    divSalary.Visible = true;
                else
                {
                    divSalary.Visible = false;
                    divBase.Visible = false;
                }
                if (ESP.Framework.BusinessLogic.PermissionManager.HasModulePermission("011", this.ModuleInfo.ModuleID, this.UserID))
                    divBase.Visible = true;
                else
                    divBase.Visible = false;

                userid = Request["userid"].Trim();
                initForm();
                //drpJobBind();
                ListBind();
                drpPublicReserveFundsStarTimeY.Width = new Unit(60);
                drpPublicReserveFundsStarTimeM.Width = new Unit(60);
            }
        }
    }

    //protected void drpJobBind()
    //{
    //    drpJob_JoinJob.Items.Insert(0, new ListItem("请选择...", "-1"));
    //}

    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(Request["userid"]))
        {
            ESP.HumanResource.Entity.EmployeeBaseInfo model = EmployeeBaseManager.GetModel(int.Parse(Request["userid"].Trim()));

            ESP.HumanResource.Entity.SnapshotsInfo snapshots = SnapshotsManager.GetTopModel(int.Parse(Request["userid"].ToString()));

            ESP.HumanResource.Entity.UsersInfo userModel = UsersManager.GetModel(model.UserID);
            ESP.Administrative.Entity.UserAttBasicInfo userAttBasicModel = new ESP.Administrative.BusinessLogic.UserAttBasicInfoManager().GetModelByUserid(model.UserID);
            //ESP.Administrative.Entity.ALAndRLInfo alandrlModel = new ESP.Administrative.BusinessLogic.ALAndRLManager().GetALAndRLModel(model.UserID, model.EmployeeJobInfo.joinDate.Year, 1);
            //if (!string.IsNullOrEmpty(drpAnnualBase.SelectedValue))
            //{
            //    int annualBase = 0;
            //    if (!int.TryParse(drpAnnualBase.SelectedValue, out annualBase))
            //        annualBase = 0;
            //    userAttBasicModel.AnnualLeaveBase = annualBase;
            //    userAttBasicModel.UpdateTime = DateTime.Now;

            //    // 入职日期
            //    DateTime EntryTime = model.EmployeeJobInfo.joinDate.Date;
            //    // 判断当前年份是否
            //    int OneYearDays = 365;
            //    if (DateTime.IsLeapYear(EntryTime.Year))
            //    {
            //        OneYearDays = 366;
            //    }
            //    DateTime endDay = new DateTime(EntryTime.Year, 12, 31).AddDays(1);
            //    TimeSpan span = endDay - EntryTime;
            //    Console.WriteLine("工作天数" + span.TotalDays);
            //    Console.WriteLine("全年天数" + OneYearDays);

            //    double totalAnnualDays = span.TotalDays / OneYearDays * annualBase;
            //    int tempdays = (int)totalAnnualDays;
            //    if ((tempdays + 0.5) >= totalAnnualDays)
            //        totalAnnualDays = tempdays;
            //    else
            //        totalAnnualDays = tempdays + 0.5;

            //    alandrlModel.RemainingNumber = (decimal)totalAnnualDays;
            //    alandrlModel.LeaveNumber = (decimal)totalAnnualDays;
            //    alandrlModel.UpdateTime = DateTime.Now;
            //}

            initModel(ref model, ref snapshots, ref userModel);
            ESP.HumanResource.Entity.LogInfo logModel = new ESP.HumanResource.Entity.LogInfo();
            logModel.LogUserId = UserInfo.UserID;
            logModel.LogMedifiedTeme = DateTime.Now;
            logModel.Des = "修改人员[" + userModel.LastNameCN + userModel.FirstNameCN + "]信息";
            logModel.LogUserName = UserInfo.Username;

            if (EmployeeBaseManager.Update(model, userModel, snapshots, logModel, null, userAttBasicModel))
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "window.location='EmployeesList.aspx';alert('修改成功！');", true);
            }
            else
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "window.location='EmployeesEdit.aspx?userid=" + model.UserID + "';alert('修改失败,请重试！');", true);
            }
        }
    }

    protected void initForm()
    {
        if (!string.IsNullOrEmpty(Request["userid"]))
        {
            ESP.HumanResource.Entity.EmployeeBaseInfo model = EmployeeBaseManager.GetModel(int.Parse(Request["userid"].Trim()));
            ESP.HumanResource.Entity.UsersInfo userModel = UsersManager.GetModel(int.Parse(Request["userid"].Trim()));

            //FrameWork.Security.User userModel = new User(int.Parse(Request["userid"].Trim()));
            //SEP.Entities.EmployeeInfo userModel = SEP.BusinessControlling.EmployeeController.Get(int.Parse(Request["userid"].Trim()));
            //chkBaseInfoOk.Checked = model.BaseInfoOK;
            //chkContractInfoOk.Checked = model.ContractInfoOK;
            //chkArchiveInfoOk.Checked = model.ArchiveInfoOK;
            //chkInsuranceInfoOk.Checked = model.InsuranceInfoOK;

            #region 个人基本信息
            // 照片
            if (!string.IsNullOrEmpty(model.Photo))
                imgBase_Photo.ImageUrl = savepath + model.Photo;
            else
                imgBase_Photo.ImageUrl = "../../public/CutImage/image/man.GIF";

            #region 个人基本情况
            txtUserCode.Text = model.Code;  // 员工编号
            txtJob_JoinDate.Text = model.EmployeeJobInfo.joinDate.Year < 1901 ? "" : model.EmployeeJobInfo.joinDate.ToString("yyyy-MM-dd");  // 入职日期
            txtBase_Email.Text = userModel.Email;  // 公司邮箱
            txtBase_FitstNameCn.Text = userModel.FirstNameCN;  // 中文姓名
            txtBase_LastNameCn.Text = userModel.LastNameCN;
            txtBase_FirstNameEn.Text = userModel.FirstNameEN;  // 英文姓名
            txtBase_LastNameEn.Text = userModel.LastNameEN;
            txtCommonName.Text = model.CommonName;  // 用户昵称
            // 性别
            if (ESP.HumanResource.Common.Status.Gender.Male.Equals(model.Gender))
                radBase_Sex1.Checked = true;
            else if (ESP.HumanResource.Common.Status.Gender.Female.Equals(model.Gender))
                radBase_Sex2.Checked = true;
            txtBase_Birthday.Text = model.Birthday.Year < 1901 ? "" : model.Birthday.ToString("yyyy-MM-dd");  // 出生日期
            txtBase_PlaceOfBirth.Text = model.BirthPlace;  // 籍贯
            txtBase_IdNo.Text = model.IDNumber;  // 身份证号码
            txtBase_DomicilePlace.Text = model.DomicilePlace;  // 户口所在地
            // 婚否
            txtBase_Marriage.SelectedValue = model.MaritalStatus.ToString();
                   
            txtBase_Health.Text = model.Health;  // 健康状况
            #endregion

            #region 个人专业情况
            txtBase_FinishSchool.Text = model.GraduateFrom;  // 毕业院校
            txtBase_FinishSchoolDate.Text = model.GraduatedDate.ToString("yyyy-MM-dd") == "1754-01-01" ? "" : model.GraduatedDate.ToString("yyyy-MM-dd");  // 毕业时间
            txtBase_Speciality.Text = model.Major;  // 专业
            txtBase_Education.SelectedValue = model.Education;  // 学历
            txtBase_WorkSpecialty.Text = model.WorkSpecialty;  // 个人特长
            #endregion

            #region 其他个人情况
            txtBase_HomePhone.Text = model.HomePhone;  // 家庭联系电话
            txtBase_MobilePhone.Text = model.MobilePhone;  // 手机
            txtBase_PrivateEmail.Text = model.PrivateEmail;  // 个人邮箱
            txtBase_Address1.Text = model.Address;  // 家庭通讯地址
            txtBase_PostCode.Text = model.PostCode;  // 邮政编码
            txtBase_EmergencyLinkman.Text = model.EmergencyContact;  // 紧急联系人
            txtBase_EmergencyPhone.Text = model.EmergencyContactPhone;  // 紧急联系人电话
            #endregion

            #region 其他信息
            //简历附件
            if (!string.IsNullOrEmpty(model.Resume))
            {
                labResume.Text = "<a target='_blank' href='" + model.Resume + "'><img src='/Images/dc.gif' border='0' /></a>";
                labResume.Visible = true;
            }
            else
            {
                labResume.Visible = false;
            }
            txtBase_WorkExperience.Text = model.WorkExperience;  // 工作简历
            //在六个月内是否有严重的疾病或意外的事故，无/有，请详细说明
            txtBase_DiseaseInSixMonths.Text = model.DiseaseInSixMonthsInfo;
            txtJob_Memo.Text = model.Memo;  // 备注

            //上级主管姓名
            //txtJob_DirectorName.Text = model.EmployeeJobInfo.directorName;
            //上级主管职位
            //txtJob_DirectorJob.Text = model.EmployeeJobInfo.directorJob;
            //try
            //{
            //    //填表日期
            //    //txtBase_CreateDate.Text = model.CreatedTime.ToString("yyyy-MM-dd") == "1754-01-01" ? "" : model.CreatedTime.ToString("yyyy-MM-dd");
            //    txtBase_CreateDate.Text = model.CreatedTime.Year < 1901 ? DateTime.Now.ToString("yyyy-MM-dd") : model.CreatedTime.ToString("yyyy-MM-dd");
            //}
            //catch { }
            //姓名
            //txtBase_Name.Text = model.FullNameCN;            

            //-----------------------------------------------
            ////入职日期
            //txtBase_JoinDate.Text = "";
            ////入职部门
            //txtBase_01.Text = "";
            ////入职职位
            //txtBase_JoinJob.Text = "";
            //-----------------------------------------------
            //IP电话
            //txtIPCode.Text = model.IPCode;
            //联系电话
            //txtBase_Phone1.Text = model.Phone1;
            //string ph = model.Phone1;
            //if (!string.IsNullOrEmpty(ph))
            //{
            //    string[] phs = ph.Split('-');
            //    if (phs.Length == 4)
            //    {
            //        try
            //        {
            //            txtPhone1.Text = phs[0];
            //            txtPhone2.Text = phs[1];
            //            txtPhone3.Text = phs[2];
            //            txtPhone4.Text = phs[3];
            //        }
            //        catch { }
            //    }
            //}            
            //备注:(请填写其他资料：通过的考试、重要培训资格、特长或业余爱好、个人资料变更等)
            //txtBase_Memo.Text = model.memo;
            //附件
            //TextBox4.Text = "";
            #endregion
            #endregion

            #region 合同信息
            ////中文姓名
            //txtContract_NameCn.Text = model.namecn;
            ////英文姓名
            //txtContract_NameEn.Text = model.nameen;
            //合同公司
            if (!string.IsNullOrEmpty(model.EmployeeWelfareInfo.contractCompany))
                drpContract_Company.SelectedValue = model.EmployeeWelfareInfo.contractCompany;

            txtContract_Company.Text = model.EmployeeWelfareInfo.contractCompany;
            //社保所在公司
            //txtContract_Social.Text = model.EmployeeWelfareInfo.socialInsuranceCompany;
            try
            {
                //合同起始日
                txtContract_StartDate.Text = model.EmployeeWelfareInfo.contractStartDate.Year < 1901 ? "" : model.EmployeeWelfareInfo.contractStartDate.ToString("yyyy-MM-dd");
            }
            catch { }
            try
            {
                //合同终止日
                txtContract_EndDate.Text = model.EmployeeWelfareInfo.contractEndDate.Year < 1901 ? "" : model.EmployeeWelfareInfo.contractEndDate.ToString("yyyy-MM-dd");
            }
            catch { }
            //试用期时长
            //txtContract_ProbationPeriod.Text = model.EmployeeWelfareInfo.probationPeriod;
            try
            {
                //试用期截止日
                txtContract_ProbationPeriodDeadLine.Text = model.EmployeeWelfareInfo.probationPeriodDeadLine.Year < 1901 ? "" : model.EmployeeWelfareInfo.probationPeriodDeadLine.ToString("yyyy-MM-dd");
            }
            catch { }
            //社保所属地点
            //lsbContract_SocialInsuranceAddress.Text = model.EmployeeWelfareInfo.socialInsuranceAddress;
            try
            {
                //转正日期
                txtContract_ProbationEndDate.Text = model.EmployeeWelfareInfo.probationEndDate.Year < 1901 ? "" : model.EmployeeWelfareInfo.probationEndDate.ToString("yyyy-MM-dd");
            }
            catch { }
            //合同文件
            //TextBox11.Text = "";
            //备注
            txtContract_Memo.Text = model.EmployeeWelfareInfo.memo;

            ESP.HumanResource.Entity.SnapshotsInfo snapshote = SnapshotsManager.GetTopModel(model.UserID);
            if (snapshote != null)
            {
                //年度工资所属公司
                try
                {
                    //txtBase_thisYearSalary.Text = model.ThisYearSalary.Trim();
                }
                catch { }
                try
                {
                    //固定工资
                    txtJob_basePay.Text = ESP.Salary.Utility.DESEncrypt.Decode(!string.IsNullOrEmpty(snapshote.newBasePay) ? snapshote.newBasePay.Trim() : "0");
                }
                catch { }
                try
                {
                    //标准绩效
                    txtJob_meritPay.Text = ESP.Salary.Utility.DESEncrypt.Decode(!string.IsNullOrEmpty(snapshote.newMeritPay) ? snapshote.newMeritPay.Trim() : "0");
                }
                catch { }
            }

            drpRenewalCount.SelectedValue = model.EmployeeWelfareInfo.contractRenewalCount.ToString();
            drpJContract_ContractInfo.SelectedValue = model.EmployeeWelfareInfo.contractSignInfo;
            #endregion

            #region 保险福利
            ////中文姓名
            //txtInsurance_NameCn.Text = model.namecn;
            ////英文姓名
            //txtInsurance_NameEn.Text = model.nameen;
            //养老保险
            chkEnd.Checked = model.EmployeeWelfareInfo.endowmentInsurance;
            ////失业保险
            //chkInsurance_Unemployment.Checked = model.EmployeeWelfareInfo.unemploymentInsurance;
            ////生育险
            //chkInsurance_Birth.Checked = model.EmployeeWelfareInfo.birthInsurance;
            ////工伤险
            //chkInsurance_Compo.Checked = model.EmployeeWelfareInfo.compoInsurance;
            ////医疗保险
            //chkInsurance_Medical.Checked = model.EmployeeWelfareInfo.medicalInsurance;

            //公积金
            chkPRF.Checked = model.EmployeeWelfareInfo.publicReserveFunds;
            //户口所在地
            drpInsurance_MemoPlace.SelectedValue = model.EmployeeWelfareInfo.InsurancePlace;
            if (model.EmployeeWelfareInfo.InsurancePlace == "外阜城镇户口" || model.EmployeeWelfareInfo.InsurancePlace == "外阜农业户口")
                txtBIFCosts.Enabled = false;
            if (model.EmployeeWelfareInfo.InsurancePlace == "外阜农业户口")
                txtUIICosts.Enabled = false;

            IList<ESP.Finance.Entity.BranchInfo> branchList = ESP.Finance.BusinessLogic.BranchManager.GetAllList();
            if (branchList != null && branchList.Count > 0)
            {
                drpInsurance_SocialInsuranceCompany.DataSource = branchList;
                drpInsurance_SocialInsuranceCompany.DataTextField = "BranchName";
                drpInsurance_SocialInsuranceCompany.DataValueField = "BranchCode";
                drpInsurance_SocialInsuranceCompany.DataBind();
                drpInsurance_SocialInsuranceCompany.Width = 300;
                drpInsurance_SocialInsuranceCompany.Text = drpContract_Company.SelectedValue;

                drpContract_Company.DataSource = branchList;
                drpContract_Company.DataTextField = "BranchName";
                drpContract_Company.DataValueField = "BranchCode";
                drpContract_Company.DataBind();
                drpContract_Company.Width = 300;
                txtContract_Company.Text = drpContract_Company.SelectedValue;
            }

            //社保所在公司
            drpInsurance_SocialInsuranceCompany.SelectedValue = model.EmployeeWelfareInfo.socialInsuranceCompany;
            txtInsurance_SocialInsuranceCompany.Text = model.EmployeeWelfareInfo.socialInsuranceCompany;
            //社保所属地点
            drpInsurance_SocialInsuranceAddress.Text = model.EmployeeWelfareInfo.socialInsuranceAddress;
            //社保编号
            //txtInsurance_SocialInsuranceCode.Text = model.EmployeeWelfareInfo.socialInsuranceCode;
            //医疗编号
            //txtInsurance_MedicalInsuranceCode.Text = model.EmployeeWelfareInfo.medicalInsuranceCode;
            //养老/失业/工伤/生育缴费基数
            string social = ESP.Salary.Utility.DESEncrypt.Decode(model.EmployeeWelfareInfo.socialInsuranceBase);
            txtInsurance_SocialInsuranceBase.Text = !string.IsNullOrEmpty(social) ? social : "0";

            //医疗基数
            string mediacl = ESP.Salary.Utility.DESEncrypt.Decode(model.EmployeeWelfareInfo.medicalInsuranceBase);
            txtInsurance_MedicalInsuranceBase.Text = !string.IsNullOrEmpty(mediacl) ? mediacl : "0";

            //公积金基数
            string publ = ESP.Salary.Utility.DESEncrypt.Decode(model.EmployeeWelfareInfo.publicReserveFundsBase);
            txtPublicReserveFunds_Base.Text = !string.IsNullOrEmpty(publ) ? publ : "0";

            ////中文姓名
            //txtPublicReserveFunds_NameCn.Text = model.namecn;
            ////英文姓名
            //txtPublicReserveFunds_NameEn.Text = model.nameen;
            //公积金
            //chkPublicReserveFunds_IsPublicReserveFunds.Checked = true;
            //户口所在地
            //txtPublicReserveFunds_.Text = model.domicilePlace;
            //公积金所在公司
            //txtPublicReserveFunds_Company.Text = model.EmployeeWelfareInfo.publicReserveFundsCompany;
            //公积金所属地点
            //txtPublicReserveFunds_Address.Text = model.EmployeeWelfareInfo.socialInsuranceAddress;

            //公积金编号
            //txtPublicReserveFunds_Code.Text = model.EmployeeWelfareInfo.publicReserveFundsCode;  
            List<ESP.HumanResource.Entity.EmployeesInPositionsInfo> list = EmployeesInPositionsManager.GetModelList(" a.userID = " + Request["userid"].ToString());
            DataSet ds = ESP.HumanResource.BusinessLogic.SocialSecurityManager.GetList(" (begintime <= '" + DateTime.Now.ToString("yyyy-MM-dd") + "' and endtime >= '" + DateTime.Now.ToString("yyyy-MM-dd") + "') and SocialInsuranceCompany=" + list[0].CompanyID);

            if (ds.Tables[0].Rows.Count > 0)
            {
                //养老保险公司比例
                txtEIProportionOfFirms.Text = (!string.IsNullOrEmpty(ds.Tables[0].Rows[0]["EIProportionOfFirms"].ToString()) ? ds.Tables[0].Rows[0]["EIProportionOfFirms"].ToString() : "0");

                //养老保险个人比例
                txtEIProportionOfIndividuals.Text = (!string.IsNullOrEmpty(ds.Tables[0].Rows[0]["EIProportionOfIndividuals"].ToString()) ? ds.Tables[0].Rows[0]["EIProportionOfIndividuals"].ToString() : "0");

                //失业保险公司比例
                txtUIProportionOfFirms.Text = (!string.IsNullOrEmpty(ds.Tables[0].Rows[0]["UIProportionOfFirms"].ToString()) ? ds.Tables[0].Rows[0]["UIProportionOfFirms"].ToString() : "0");
                //失业保险个人比例
                txtUIProportionOfIndividuals.Text = (!string.IsNullOrEmpty(ds.Tables[0].Rows[0]["UIProportionOfIndividuals"].ToString()) ? ds.Tables[0].Rows[0]["UIProportionOfIndividuals"].ToString() : "0");
                //生育保险公司比例
                txtBIProportionOfFirms.Text = (!string.IsNullOrEmpty(ds.Tables[0].Rows[0]["BIProportionOfFirms"].ToString()) ? ds.Tables[0].Rows[0]["BIProportionOfFirms"].ToString() : "0");
                //工伤险公司比例
                txtCIProportionOfFirms.Text = (!string.IsNullOrEmpty(ds.Tables[0].Rows[0]["CIProportionOfFirms"].ToString()) ? ds.Tables[0].Rows[0]["CIProportionOfFirms"].ToString() : "0");
                //医疗保险公司比例
                txtMIProportionOfFirms.Text = (!string.IsNullOrEmpty(ds.Tables[0].Rows[0]["MIProportionOfFirms"].ToString()) ? ds.Tables[0].Rows[0]["MIProportionOfFirms"].ToString() : "0");
                //医疗保险个人比例
                txtMIProportionOfIndividuals.Text = (!string.IsNullOrEmpty(ds.Tables[0].Rows[0]["MIProportionOfIndividuals"].ToString()) ? ds.Tables[0].Rows[0]["MIProportionOfIndividuals"].ToString() : "0");
                //医疗保险大额医疗个人支付额
                txtMIBigProportionOfIndividuals.Text = (!string.IsNullOrEmpty(ds.Tables[0].Rows[0]["MIBigProportionOfIndividuals"].ToString()) ? ds.Tables[0].Rows[0]["MIBigProportionOfIndividuals"].ToString() : "0");

                //公积金比例
                txtPRFProportionOfFirms.Text = (!string.IsNullOrEmpty(ds.Tables[0].Rows[0]["PRFProportionOfFirms"].ToString()) ? ds.Tables[0].Rows[0]["PRFProportionOfFirms"].ToString() : "0");
            }
            else
            {
                //养老保险公司比例
                txtEIProportionOfFirms.Text = "0";
                //养老保险个人比例
                txtEIProportionOfIndividuals.Text = "0";
                //失业保险公司比例
                txtUIProportionOfFirms.Text = "0";
                //失业保险个人比例
                txtUIProportionOfIndividuals.Text = "0";
                //生育保险公司比例
                txtBIProportionOfFirms.Text = "0";
                //工伤险公司比例
                txtCIProportionOfFirms.Text = "0";
                //医疗保险公司比例
                txtMIProportionOfFirms.Text = "0";
                //医疗保险个人比例
                txtMIProportionOfIndividuals.Text = "0";
                //医疗保险大额医疗个人支付额
                txtMIBigProportionOfIndividuals.Text = "0";

                //公积金比例
                txtPRFProportionOfFirms.Text = "0";
            }
            //养老底线值
            ESP.HumanResource.Entity.ProtectionLineInfo prot = ProtectionLineManager.GetModel(1);
            hidProtectionLine.Value = prot.ProtectionLineNameAmount.ToString("0.00");

            //养老/失业/工伤/生育缴费基数
            decimal sbase = 0;
            try
            {
                sbase = Convert.ToDecimal(social);
            }
            catch (Exception ex) { }
            //医疗基数
            decimal mbase = 0;
            try
            {
                mbase = Convert.ToDecimal(mediacl);
            }
            catch (Exception ex) { }

            //公积金基数
            decimal pbase = 0;
            try
            {
                pbase = Convert.ToDecimal(publ);
            }
            catch (Exception ex) { }
            if (ds.Tables[0].Rows.Count > 0)
            {
                //养老保险公司比例
                decimal eif = decimal.Parse(!string.IsNullOrEmpty(ds.Tables[0].Rows[0]["EIProportionOfFirms"].ToString()) ? ds.Tables[0].Rows[0]["EIProportionOfFirms"].ToString() : "0");
                //养老保险个人比例
                decimal eii = decimal.Parse(!string.IsNullOrEmpty(ds.Tables[0].Rows[0]["EIProportionOfIndividuals"].ToString()) ? ds.Tables[0].Rows[0]["EIProportionOfIndividuals"].ToString() : "0");
                //失业保险公司比例
                decimal UIF = decimal.Parse(!string.IsNullOrEmpty(ds.Tables[0].Rows[0]["UIProportionOfFirms"].ToString()) ? ds.Tables[0].Rows[0]["UIProportionOfFirms"].ToString() : "0");
                //失业保险个人比例
                decimal UII = decimal.Parse(!string.IsNullOrEmpty(ds.Tables[0].Rows[0]["UIProportionOfIndividuals"].ToString()) ? ds.Tables[0].Rows[0]["UIProportionOfIndividuals"].ToString() : "0");
                //生育保险公司比例
                decimal BIF = decimal.Parse(!string.IsNullOrEmpty(ds.Tables[0].Rows[0]["BIProportionOfFirms"].ToString()) ? ds.Tables[0].Rows[0]["BIProportionOfFirms"].ToString() : "0");
                //工伤险公司比例
                decimal CIF = decimal.Parse(!string.IsNullOrEmpty(ds.Tables[0].Rows[0]["CIProportionOfFirms"].ToString()) ? ds.Tables[0].Rows[0]["CIProportionOfFirms"].ToString() : "0");
                //医疗保险公司比例
                decimal MIF = decimal.Parse(!string.IsNullOrEmpty(ds.Tables[0].Rows[0]["MIProportionOfFirms"].ToString()) ? ds.Tables[0].Rows[0]["MIProportionOfFirms"].ToString() : "0");
                //医疗保险个人比例
                decimal MII = decimal.Parse(!string.IsNullOrEmpty(ds.Tables[0].Rows[0]["MIProportionOfIndividuals"].ToString()) ? ds.Tables[0].Rows[0]["MIProportionOfIndividuals"].ToString() : "0");
                //医疗保险大额医疗个人支付额
                decimal MIBI = decimal.Parse(!string.IsNullOrEmpty(ds.Tables[0].Rows[0]["MIBigProportionOfIndividuals"].ToString()) ? ds.Tables[0].Rows[0]["MIBigProportionOfIndividuals"].ToString() : "0");
                //公积金比例
                decimal PRF = decimal.Parse(!string.IsNullOrEmpty(ds.Tables[0].Rows[0]["PRFProportionOfFirms"].ToString()) ? ds.Tables[0].Rows[0]["PRFProportionOfFirms"].ToString() : "0");

                //养老保险公司应缴费用
                txtEIFCosts.Text = (sbase * (eif / 100)).ToString("0.00");
                //txtEIFCosts.Text = ESP.Salary.Utility.DESEncrypt.Decode(!string.IsNullOrEmpty(model.EmployeeWelfareInfo.EIFirmsCosts) ? model.EmployeeWelfareInfo.EIFirmsCosts.ToString() : "0");
                //养老保险应缴费用
                txtEIICosts.Text = (sbase * (eii / 100)).ToString("0.00");
                //txtEIICosts.Text = ESP.Salary.Utility.DESEncrypt.Decode(!string.IsNullOrEmpty(model.EmployeeWelfareInfo.EIIndividualsCosts) ? model.EmployeeWelfareInfo.EIIndividualsCosts.ToString() : "0");
                //失业保险公司应缴费用
                txtUIFCosts.Text = (sbase * (UIF / 100)).ToString("0.00");
                //txtUIFCosts.Text = ESP.Salary.Utility.DESEncrypt.Decode(!string.IsNullOrEmpty(model.EmployeeWelfareInfo.UIFirmsCosts) ? model.EmployeeWelfareInfo.UIFirmsCosts.ToString() : "0");
                //失业保险个人应缴费用
                txtUIICosts.Text = (sbase * (UII / 100)).ToString("0.00");
                // txtUIICosts.Text = ESP.Salary.Utility.DESEncrypt.Decode(!string.IsNullOrEmpty(model.EmployeeWelfareInfo.UIIndividualsCosts) ? model.EmployeeWelfareInfo.UIIndividualsCosts.ToString() : "0");
                //生育保险公司应缴费用
                txtBIFCosts.Text = ((sbase > prot.ProtectionLineNameAmount ? sbase : prot.ProtectionLineNameAmount) * (BIF / 100)).ToString("0.00");
                //txtBIFCosts.Text = ESP.Salary.Utility.DESEncrypt.Decode(!string.IsNullOrEmpty(model.EmployeeWelfareInfo.BIFirmsCosts) ? model.EmployeeWelfareInfo.BIFirmsCosts.ToString() : "0");
                //工伤险公司应缴费用
                txtCIFCosts.Text = (sbase * (CIF / 100)).ToString("0.00");
                //txtCIFCosts.Text = ESP.Salary.Utility.DESEncrypt.Decode(!string.IsNullOrEmpty(model.EmployeeWelfareInfo.CIFirmsCosts) ? model.EmployeeWelfareInfo.CIFirmsCosts.ToString() : "0");
                //医疗保险公司应缴费用
                txtMIFCosts.Text = (mbase * (MIF / 100)).ToString("0.00");
                //txtMIFCosts.Text = ESP.Salary.Utility.DESEncrypt.Decode(!string.IsNullOrEmpty(model.EmployeeWelfareInfo.MIFirmsCosts) ? model.EmployeeWelfareInfo.MIFirmsCosts.ToString() : "0");
                //医疗保险个人应缴费用
                txtMIICosts.Text = (mbase * (MII / 100)).ToString("0.00");
                //txtMIICosts.Text = ESP.Salary.Utility.DESEncrypt.Decode(!string.IsNullOrEmpty(model.EmployeeWelfareInfo.MIIndividualsCosts) ? model.EmployeeWelfareInfo.MIIndividualsCosts.ToString() : "0");
                //公积金应缴费用
                txtPRFCosts.Text = (pbase * (PRF / 100)).ToString("0.00");
                //txtPRFCosts.Text = ESP.Salary.Utility.DESEncrypt.Decode(!string.IsNullOrEmpty(model.EmployeeWelfareInfo.PRFirmsCosts) ? model.EmployeeWelfareInfo.PRFirmsCosts.ToString() : "0");
            }
            else
            {
                //养老保险公司比例
                decimal eif = decimal.Parse("0");
                //养老保险个人比例
                decimal eii = decimal.Parse("0");
                //失业保险公司比例
                decimal UIF = decimal.Parse("0");
                //失业保险个人比例
                decimal UII = decimal.Parse("0");
                //生育保险公司比例
                decimal BIF = decimal.Parse("0");
                //工伤险公司比例
                decimal CIF = decimal.Parse("0");
                //医疗保险公司比例
                decimal MIF = decimal.Parse("0");
                //医疗保险个人比例
                decimal MII = decimal.Parse("0");
                //医疗保险大额医疗个人支付额
                decimal MIBI = decimal.Parse("0");
                //公积金比例
                decimal PRF = decimal.Parse("0");

                //养老保险公司应缴费用
                txtEIFCosts.Text = "0";
                //养老保险应缴费用
                txtEIICosts.Text = ("0");
                //失业保险公司应缴费用
                txtUIFCosts.Text = "0";
                //失业保险个人应缴费用
                txtUIICosts.Text = "0";
                //生育保险公司应缴费用
                txtBIFCosts.Text = "0";
                //工伤险公司应缴费用
                txtCIFCosts.Text = "0";
                //医疗保险公司应缴费用
                txtMIFCosts.Text = "0";
                //医疗保险个人应缴费用
                txtMIICosts.Text = "0";
                //公积金应缴费用
                txtPRFCosts.Text = "0";
            }

            if (chkEnd.Checked)
            {
                //养老保险公司比例
                txtEIProportionOfFirms.Enabled = false;
                //养老保险个人比例
                txtEIProportionOfIndividuals.Enabled = false;
                //失业保险公司比例
                txtUIProportionOfFirms.Enabled = false;
                //失业保险个人比例
                txtUIProportionOfIndividuals.Enabled = false;
                //生育保险公司比例
                txtBIProportionOfFirms.Enabled = false;
                //工伤险公司比例
                txtCIProportionOfFirms.Enabled = false;
                //医疗保险公司比例
                txtMIProportionOfFirms.Enabled = false;
                //医疗保险个人比例
                txtMIProportionOfIndividuals.Enabled = false;
                //医疗保险大额医疗个人支付额
                txtMIBigProportionOfIndividuals.Enabled = false;
                //养老保险公司应缴费用

                txtEIFCosts.Enabled = false;
                //养老保险应缴费用
                txtEIICosts.Enabled = false;
                //失业保险公司应缴费用
                txtUIFCosts.Enabled = false;
                //失业保险个人应缴费用
                txtUIICosts.Enabled = false;
                //生育保险公司应缴费用
                txtBIFCosts.Enabled = false;
                //工伤险公司应缴费用
                txtCIFCosts.Enabled = false;
                //医疗保险公司应缴费用
                txtMIFCosts.Enabled = false;
                //医疗保险个人应缴费用
                txtMIICosts.Enabled = false;
            }
            if (chkPRF.Checked)
            {
                //公积金比例
                txtPRFProportionOfFirms.Enabled = false;
                //公积金应缴费用
                txtPRFCosts.Enabled = false;
            }
            #endregion

            #region 档案管理
            ////中文姓名
            //txtArchive_NameCn.Text = model.namecn;
            ////英文姓名
            //txtArchive_NameEn.Text = model.nameen;
            //在公司挂档
            //chkArchive_IsArchive.Checked = model.EmployeeWelfareInfo.isArchive;
            drpArchive_ArchivePlace.SelectedValue = model.EmployeeWelfareInfo.ArchivePlace;
            //户口所在地
            //txtArchive_.Text = model.domicilePlace;
            //存档日期
            txtArchive_ArchiveDate.Text = model.EmployeeWelfareInfo.ArchiveDate;
            //档案编号
            txtArchive_Code.Text = model.EmployeeWelfareInfo.ArchiveCode;
            #endregion

            #region 工资基数
            txtJob_basePay.Text = ESP.Salary.Utility.DESEncrypt.Decode(!string.IsNullOrEmpty(snapshote.newBasePay) ? snapshote.newBasePay.Trim() : "0");  // 固定工资
            txtJob_meritPay.Text = ESP.Salary.Utility.DESEncrypt.Decode(!string.IsNullOrEmpty(snapshote.newMeritPay) ? snapshote.newMeritPay.Trim() : "0");  // 标准绩效
            if (model.WageMonths == 12 || model.WageMonths == 13)  // 工资发放月份
                drpWageMonths.SelectedValue = model.WageMonths.ToString();
            #endregion

            // 社保、公积金、意外保险开始时间
            int year = DateTime.Now.Year - 10;
            for (int i = 0; i < 20; i++)  // 年份
            {
                drpEndowmentStarTimeY.Items.Insert(i, new ListItem((year + i).ToString(), (year + i).ToString()));
                drpPublicReserveFundsStarTimeY.Items.Insert(i, new ListItem((year + i).ToString(), (year + i).ToString()));
                drpAccidentInsuranceBeginTimeY.Items.Insert(i, new ListItem((year + i).ToString(), (year + i).ToString()));
            }
            for (int i = 1; i <= 12; i++)  // 月份
            {
                drpEndowmentStarTimeM.Items.Insert(i - 1, new ListItem((i).ToString(), (i).ToString()));
                drpPublicReserveFundsStarTimeM.Items.Insert(i - 1, new ListItem((i).ToString(), (i).ToString()));
                drpAccidentInsuranceBeginTimeM.Items.Insert(i - 1, new ListItem(i.ToString(), i.ToString()));
            }

            DateTime endowmenttime = model.EmployeeWelfareInfo.endowmentInsuranceStarTime;  // 社保开始时间
            DateTime publicreservefundstime = model.EmployeeWelfareInfo.publicReserveFundsStarTime;  // 公积金开始时间
            DateTime accidentInsuranceBeginTime = model.EmployeeWelfareInfo.AccidentInsuranceBeginTime;  // 意外保险开始时间


            // 社保开始时间
            try
            {
                drpEndowmentStarTimeY.SelectedValue = endowmenttime.Year.ToString();
            }
            catch
            {
                drpEndowmentStarTimeY.SelectedValue = DateTime.Now.Year.ToString();
            }
            try
            {
                drpEndowmentStarTimeM.SelectedValue = endowmenttime.Month.ToString();
            }
            catch
            {
                drpEndowmentStarTimeM.SelectedValue = DateTime.Now.Month.ToString();
            }

            // 公积金开始时间
            try
            {
                drpPublicReserveFundsStarTimeY.SelectedValue = publicreservefundstime.Year.ToString();
            }
            catch
            {
                drpPublicReserveFundsStarTimeY.SelectedValue = DateTime.Now.Year.ToString();
            }
            try
            {
                drpPublicReserveFundsStarTimeM.SelectedValue = publicreservefundstime.Month.ToString();
            }
            catch
            {
                drpPublicReserveFundsStarTimeM.SelectedValue = DateTime.Now.Month.ToString();
            }

            // 意外保险开始时间
            try
            {
                drpAccidentInsuranceBeginTimeY.SelectedValue = accidentInsuranceBeginTime.Year.ToString();
            }
            catch
            {
                drpAccidentInsuranceBeginTimeY.SelectedValue = DateTime.Now.Year.ToString();
            }
            try
            {
                drpAccidentInsuranceBeginTimeM.SelectedValue = accidentInsuranceBeginTime.Month.ToString();
            }
            catch
            {
                drpAccidentInsuranceBeginTimeM.SelectedValue = DateTime.Now.Month.ToString();
            }

            //chkForeign.Checked = model.IsForeign;
            //chkCertification.Checked = model.IsCertification;
            if (model.EmployeeWelfareInfo.AccidentInsurance)
            {
                chkAccidentInsurance.Checked = model.EmployeeWelfareInfo.AccidentInsurance;
            }
            else
            {
                drpAccidentInsuranceBeginTimeY.Enabled = false;
                drpAccidentInsuranceBeginTimeM.Enabled = false;
            }
            if (model.EmployeeWelfareInfo.ComplementaryMedical)
            {
                chkComplementaryMedical.Checked = model.EmployeeWelfareInfo.ComplementaryMedical;
            }
            else
            {
                drpComplementaryMedicalY.Enabled = false;
                drpComplementaryMedicalM.Enabled = false;
            }

            try
            {
                //txtComplementaryMedical.Text = model.EmployeeWelfareInfo.ComplementaryMedicalCosts.Value.ToString("0.00");
            }
            catch
            {
                //txtComplementaryMedical.Text = "0.00";
            }
        }
    }

    protected void initModel(ref ESP.HumanResource.Entity.EmployeeBaseInfo model, ref ESP.HumanResource.Entity.SnapshotsInfo snapshots, ref ESP.HumanResource.Entity.UsersInfo userModel)
    {
        if (!string.IsNullOrEmpty(Request["userid"]))
        {
            if (snapshots == null)
            {
                snapshots = new ESP.HumanResource.Entity.SnapshotsInfo();
            }
            snapshots.Code = txtUserCode.Text.Trim();
            //model.BaseInfoOK = chkBaseInfoOk.Checked;
            //model.ContractInfoOK = chkContractInfoOk.Checked;
            //model.ArchiveInfoOK = chkArchiveInfoOk.Checked;
            //model.InsuranceInfoOK = chkInsuranceInfoOk.Checked;

            //员工编号
            snapshots.Code = model.Code = txtUserCode.Text.Trim();

            //中文姓名
            userModel.FirstNameCN = txtBase_FitstNameCn.Text.Trim();
            userModel.LastNameCN = txtBase_LastNameCn.Text.Trim();
            //英文姓名
            userModel.FirstNameEN = txtBase_FirstNameEn.Text.Trim();
            userModel.LastNameEN = txtBase_LastNameEn.Text.Trim();

            if (fileCV.PostedFile != null && fileCV.PostedFile.ContentLength > 0)
            {
                string filename = SaveFile(model.Code);
                model.Resume = filename;
            }
            // 性别
            if (radBase_Sex1.Checked)
                model.Gender = (int)ESP.HumanResource.Common.Status.Gender.Male;
            else
                model.Gender = (int)ESP.HumanResource.Common.Status.Gender.Female;
            try
            {
                //入职日期
                model.EmployeeJobInfo.joinDate = DateTime.Parse(txtJob_JoinDate.Text.Trim());
            }
            catch { }
            //入职职位
            //model.EmployeeJobInfo.joinJob = txtJob_JoinJob.Value;
            //上级主管姓名
            //model.EmployeeJobInfo.directorName = txtJob_DirectorName.Text.Trim();
            //上级主管职位
            //model.EmployeeJobInfo.directorJob = txtJob_DirectorJob.Text.Trim();

            if (snapshots != null)
            {
                //工资所属公司
                try
                {
                    //snapshots.ThisYearSalary = model.ThisYearSalary = txtBase_thisYearSalary.Text.Trim();
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
                    snapshots.newMeritPay = ESP.Salary.Utility.DESEncrypt.Encode(!string.IsNullOrEmpty(txtJob_meritPay.Text.Trim()) ? txtJob_meritPay.Text.Trim() : "0");
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
                    snapshots.nowMeritPay = ESP.Salary.Utility.DESEncrypt.Encode(!string.IsNullOrEmpty(txtJob_meritPay.Text.Trim()) ? txtJob_meritPay.Text.Trim() : "0");
                }
                catch { }
                snapshots.UserID = model.UserID;

                //snapshots.UserName = model.LastNameCN + model.FirstNameCN;
                snapshots.UserName = UserInfo.FullNameCN.Trim();
                snapshots.Creator = UserInfo.UserID;
                snapshots.CreatorName = UserInfo.Username.Trim();
                snapshots.CreatedTime = DateTime.Now;
            }
            //备注
            model.Memo = txtJob_Memo.Text.Trim();

            //填表日期
            //if (!string.IsNullOrEmpty(txtBase_CreateDate.Text.Trim()))
            //{
            model.CreatedTime = DateTime.Now;
            //}
            //姓名
            //model.LastNameCN = txtBase_Name.Text.Trim();
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
            model.CommonName = snapshots.CommonName = txtCommonName.Text.Trim();
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
            //IP电话
            //model.IPCode = txtIPCode.Text.Trim();
            //联系电话
            //model.Phone1 = txtPhone1.Text.Trim() + "-" + txtPhone2.Text.Trim() + "-" + txtPhone3.Text.Trim() + "-" + txtPhone4.Text.Trim();
            //手机
            model.MobilePhone = txtBase_MobilePhone.Text.Trim();
            //宅电
            model.HomePhone = txtBase_HomePhone.Text.Trim();
            //通讯地址
            model.Address = txtBase_Address1.Text.Trim();
            //邮政编码
            model.PostCode = txtBase_PostCode.Text.Trim();
            //电子邮件
            userModel.Email = txtBase_Email.Text.Trim();
            //工作简历
            model.WorkExperience = txtBase_WorkExperience.Text.Trim();
            //在六个月内是否有严重的疾病或意外的事故，无/有，请详细说明
            //model.diseaseInSixMonthInfo = txtBase_DiseaseInSixMonths.Text.Trim();
            //备注:(请填写其他资料：通过的考试、重要培训资格、特长或业余爱好、个人资料变更等)
            //model.memo = txtBase_Memo.Text.Trim();
            //附件
            //TextBox4.Text = "";

            //合同公司
            model.EmployeeWelfareInfo.contractCompany = drpContract_Company.SelectedValue;  //txtContract_Company.Text.Trim();
            //社保所在公司
            //model.EmployeeWelfareInfo.socialInsuranceCompany = txtContract_Social.Text.Trim();
            try
            {
                //合同起始日
                snapshots.contractStartDate = model.EmployeeWelfareInfo.contractStartDate = DateTime.Parse(txtContract_StartDate.Text.Trim());
            }
            catch { }
            try
            {
                //合同终止日
                snapshots.contractEndDate = model.EmployeeWelfareInfo.contractEndDate = DateTime.Parse(txtContract_EndDate.Text.Trim());
            }
            catch { }
            //试用期时长
            //model.EmployeeWelfareInfo.probationPeriod = txtContract_ProbationPeriod.Text.Trim();
            try
            {
                //试用期截止日
                model.EmployeeWelfareInfo.probationPeriodDeadLine = DateTime.Parse(txtContract_ProbationPeriodDeadLine.Text.Trim());
            }
            catch { }
            //社保所属地点
            //model.EmployeeWelfareInfo.socialInsuranceAddress = lsbContract_SocialInsuranceAddress.Text.Trim();
            try
            {
                //转正日期
                model.EmployeeWelfareInfo.probationEndDate = DateTime.Parse(txtContract_ProbationEndDate.Text.Trim());
            }
            catch { }
            //合同文件
            //TextBox11.Text = "";
            //备注
            model.EmployeeWelfareInfo.memo = txtContract_Memo.Text.Trim();

            model.EmployeeWelfareInfo.contractRenewalCount = int.Parse(drpRenewalCount.SelectedValue);
            model.EmployeeWelfareInfo.contractSignInfo = drpJContract_ContractInfo.SelectedValue;

            if (chkEnd.Checked)
            {
                // //养老/失业/工伤/生育缴费基数
                //snapshots.socialInsuranceBase = model.EmployeeWelfareInfo.socialInsuranceBase = ESP.Salary.Utility.DESEncrypt.Encode("0");
                ////医疗基数
                //snapshots.medicalInsuranceBase = model.EmployeeWelfareInfo.medicalInsuranceBase = ESP.Salary.Utility.DESEncrypt.Encode("0");
                //养老保险开始时间                
                snapshots.endowmentInsuranceStarTime = model.EmployeeWelfareInfo.endowmentInsuranceStarTime = DateTime.Parse("1900-01-01");
                //失业保险开始时间       
                snapshots.unemploymentInsuranceStarTime = model.EmployeeWelfareInfo.unemploymentInsuranceStarTime = DateTime.Parse("1900-01-01");
                //生育险开始时间
                snapshots.birthInsuranceStarTime = model.EmployeeWelfareInfo.birthInsuranceStarTime = DateTime.Parse("1900-01-01");
                //工伤险开始时间
                snapshots.compoInsuranceStarTime = model.EmployeeWelfareInfo.compoInsuranceStarTime = DateTime.Parse("1900-01-01");
                //医疗保险开始时间               
                snapshots.medicalInsuranceStarTime = model.EmployeeWelfareInfo.medicalInsuranceStarTime = DateTime.Parse("1900-01-01");
                ////养老保险公司比例
                //snapshots.EIProportionOfFirms = model.EmployeeWelfareInfo.EIProportionOfFirms = decimal.Parse("0");
                ////养老保险个人比例
                //snapshots.EIProportionOfIndividuals = model.EmployeeWelfareInfo.EIProportionOfIndividuals = decimal.Parse("0");
                ////失业保险公司比例
                //snapshots.UIProportionOfFirms = model.EmployeeWelfareInfo.UIProportionOfFirms = decimal.Parse("0");
                ////失业保险个人比例
                //snapshots.UIProportionOfIndividuals = model.EmployeeWelfareInfo.UIProportionOfIndividuals = decimal.Parse("0");
                ////生育保险公司比例
                //snapshots.BIProportionOfFirms = model.EmployeeWelfareInfo.BIProportionOfFirms = decimal.Parse("0");
                ////生育保险个人比例
                //snapshots.BIProportionOfIndividuals = model.EmployeeWelfareInfo.BIProportionOfIndividuals = decimal.Parse("0");
                ////工伤险公司比例
                //snapshots.CIProportionOfFirms = model.EmployeeWelfareInfo.CIProportionOfFirms = decimal.Parse("0");
                ////工伤险个人比例
                //snapshots.CIProportionOfIndividuals = model.EmployeeWelfareInfo.CIProportionOfIndividuals = decimal.Parse("0");
                ////医疗保险公司比例
                //snapshots.MIProportionOfFirms = model.EmployeeWelfareInfo.MIProportionOfFirms = decimal.Parse("0");
                ////医疗保险个人比例
                //snapshots.MIProportionOfIndividuals = model.EmployeeWelfareInfo.MIProportionOfIndividuals = decimal.Parse("0");
                //医疗保险大额医疗个人支付额
                snapshots.MIBigProportionOfIndividuals = model.EmployeeWelfareInfo.MIBigProportionOfIndividuals = decimal.Parse("0");

                //养老保险公司应缴费用
                snapshots.EIFirmsCosts = model.EmployeeWelfareInfo.EIFirmsCosts = ESP.Salary.Utility.DESEncrypt.Encode("0");
                //养老保险个人应缴费用
                snapshots.EIIndividualsCosts = model.EmployeeWelfareInfo.EIIndividualsCosts = ESP.Salary.Utility.DESEncrypt.Encode("0");
                //失业保险公司应缴费用
                snapshots.UIFirmsCosts = model.EmployeeWelfareInfo.UIFirmsCosts = ESP.Salary.Utility.DESEncrypt.Encode("0");
                //失业保险个人应缴费用
                snapshots.UIIndividualsCosts = model.EmployeeWelfareInfo.UIIndividualsCosts = ESP.Salary.Utility.DESEncrypt.Encode("0");
                //生育保险公司应缴费用
                snapshots.BIFirmsCosts = model.EmployeeWelfareInfo.BIFirmsCosts = ESP.Salary.Utility.DESEncrypt.Encode("0");
                //生育保险个人应缴费用
                snapshots.BIIndividualsCosts = model.EmployeeWelfareInfo.BIIndividualsCosts = ESP.Salary.Utility.DESEncrypt.Encode("0");
                //工伤险公司应缴费用
                snapshots.CIFirmsCosts = model.EmployeeWelfareInfo.CIFirmsCosts = ESP.Salary.Utility.DESEncrypt.Encode("0");
                //工伤险个人应缴费用
                snapshots.CIIndividualsCosts = model.EmployeeWelfareInfo.CIIndividualsCosts = ESP.Salary.Utility.DESEncrypt.Encode("0");
                //医疗保险公司应缴费用
                snapshots.MIFirmsCosts = model.EmployeeWelfareInfo.MIFirmsCosts = ESP.Salary.Utility.DESEncrypt.Encode("0");
                //医疗保险个人应缴费用
                snapshots.MIIndividualsCosts = model.EmployeeWelfareInfo.MIIndividualsCosts = ESP.Salary.Utility.DESEncrypt.Encode("0");
            }
            else
            {
                //养老/失业/工伤/生育缴费基数
                snapshots.socialInsuranceBase = model.EmployeeWelfareInfo.socialInsuranceBase = ESP.Salary.Utility.DESEncrypt.Encode(!string.IsNullOrEmpty(txtInsurance_SocialInsuranceBase.Text.Trim()) ? txtInsurance_SocialInsuranceBase.Text.Trim() : "0");
                //医疗基数
                snapshots.medicalInsuranceBase = model.EmployeeWelfareInfo.medicalInsuranceBase = ESP.Salary.Utility.DESEncrypt.Encode(!string.IsNullOrEmpty(txtInsurance_MedicalInsuranceBase.Text.Trim()) ? txtInsurance_MedicalInsuranceBase.Text.Trim() : "0");
                try
                {
                    //养老保险开始时间                
                    snapshots.endowmentInsuranceStarTime = model.EmployeeWelfareInfo.endowmentInsuranceStarTime = DateTime.Parse(drpEndowmentStarTimeY.SelectedItem.Value + "-" + drpEndowmentStarTimeM.SelectedItem.Value + "-01");
                }
                catch { }
                try
                {
                    //失业保险开始时间       
                    snapshots.unemploymentInsuranceStarTime = model.EmployeeWelfareInfo.unemploymentInsuranceStarTime = DateTime.Parse(drpEndowmentStarTimeY.SelectedItem.Value + "-" + drpEndowmentStarTimeM.SelectedItem.Value + "-01");
                }
                catch { }
                try
                {
                    //生育险开始时间
                    snapshots.birthInsuranceStarTime = model.EmployeeWelfareInfo.birthInsuranceStarTime = DateTime.Parse(drpEndowmentStarTimeY.SelectedItem.Value + "-" + drpEndowmentStarTimeM.SelectedItem.Value + "-01");
                }
                catch { }
                try
                {
                    //工伤险开始时间
                    snapshots.compoInsuranceStarTime = model.EmployeeWelfareInfo.compoInsuranceStarTime = DateTime.Parse(drpEndowmentStarTimeY.SelectedItem.Value + "-" + drpEndowmentStarTimeM.SelectedItem.Value + "-01");
                }
                catch { }
                try
                {
                    //医疗保险开始时间               
                    snapshots.medicalInsuranceStarTime = model.EmployeeWelfareInfo.medicalInsuranceStarTime = DateTime.Parse(drpEndowmentStarTimeY.SelectedItem.Value + "-" + drpEndowmentStarTimeM.SelectedItem.Value + "-01");
                }
                catch { }
                try
                {
                    //养老保险公司比例
                    snapshots.EIProportionOfFirms = model.EmployeeWelfareInfo.EIProportionOfFirms = decimal.Parse(!string.IsNullOrEmpty(txtEIProportionOfFirms.Text.Trim()) ? txtEIProportionOfFirms.Text.Trim() : "0");
                    //养老保险个人比例
                    snapshots.EIProportionOfIndividuals = model.EmployeeWelfareInfo.EIProportionOfIndividuals = decimal.Parse(!string.IsNullOrEmpty(txtEIProportionOfIndividuals.Text.Trim()) ? txtEIProportionOfIndividuals.Text.Trim() : "0");
                    //失业保险公司比例
                    snapshots.UIProportionOfFirms = model.EmployeeWelfareInfo.UIProportionOfFirms = decimal.Parse(!string.IsNullOrEmpty(txtUIProportionOfFirms.Text.Trim()) ? txtUIProportionOfFirms.Text.Trim() : "0");
                    //失业保险个人比例
                    snapshots.UIProportionOfIndividuals = model.EmployeeWelfareInfo.UIProportionOfIndividuals = decimal.Parse(!string.IsNullOrEmpty(txtUIProportionOfIndividuals.Text.Trim()) ? txtUIProportionOfIndividuals.Text.Trim() : "0");
                    //生育保险公司比例                    
                    snapshots.BIProportionOfFirms = model.EmployeeWelfareInfo.BIProportionOfFirms = decimal.Parse(!string.IsNullOrEmpty(txtBIProportionOfFirms.Text.Trim()) ? txtBIProportionOfFirms.Text.Trim() : "0");
                    //生育保险个人比例
                    snapshots.BIProportionOfIndividuals = model.EmployeeWelfareInfo.BIProportionOfIndividuals = decimal.Parse("0");
                    //工伤险公司比例
                    snapshots.CIProportionOfFirms = model.EmployeeWelfareInfo.CIProportionOfFirms = decimal.Parse(!string.IsNullOrEmpty(txtCIProportionOfFirms.Text.Trim()) ? txtCIProportionOfFirms.Text.Trim() : "0");
                    //工伤险个人比例
                    snapshots.CIProportionOfIndividuals = model.EmployeeWelfareInfo.CIProportionOfIndividuals = decimal.Parse("0");
                    //医疗保险公司比例
                    snapshots.MIProportionOfFirms = model.EmployeeWelfareInfo.MIProportionOfFirms = decimal.Parse(!string.IsNullOrEmpty(txtMIProportionOfFirms.Text.Trim()) ? txtMIProportionOfFirms.Text.Trim() : "0");
                    //医疗保险个人比例
                    snapshots.MIProportionOfIndividuals = model.EmployeeWelfareInfo.MIProportionOfIndividuals = decimal.Parse(!string.IsNullOrEmpty(txtMIProportionOfIndividuals.Text.Trim()) ? txtMIProportionOfIndividuals.Text.Trim() : "0");
                    //医疗保险大额医疗个人支付额
                    snapshots.MIBigProportionOfIndividuals = model.EmployeeWelfareInfo.MIBigProportionOfIndividuals = decimal.Parse(!string.IsNullOrEmpty(txtMIBigProportionOfIndividuals.Text.Trim()) ? txtMIBigProportionOfIndividuals.Text.Trim() : "0");
                }
                catch { }

                try
                {
                    //养老保险公司应缴费用
                    snapshots.EIFirmsCosts = model.EmployeeWelfareInfo.EIFirmsCosts = ESP.Salary.Utility.DESEncrypt.Encode(!string.IsNullOrEmpty(txtEIFCosts.Text.Trim()) ? txtEIFCosts.Text.Trim() : "0");
                    //养老保险个人应缴费用
                    snapshots.EIIndividualsCosts = model.EmployeeWelfareInfo.EIIndividualsCosts = ESP.Salary.Utility.DESEncrypt.Encode(!string.IsNullOrEmpty(txtEIICosts.Text.Trim()) ? txtEIICosts.Text.Trim() : "0");
                    //失业保险公司应缴费用
                    snapshots.UIFirmsCosts = model.EmployeeWelfareInfo.UIFirmsCosts = ESP.Salary.Utility.DESEncrypt.Encode(!string.IsNullOrEmpty(txtUIFCosts.Text.Trim()) ? txtUIFCosts.Text.Trim() : "0");
                    //失业保险个人应缴费用
                    if (drpInsurance_MemoPlace.SelectedItem.Value == "外阜农业户口")
                        snapshots.UIIndividualsCosts = model.EmployeeWelfareInfo.UIIndividualsCosts = ESP.Salary.Utility.DESEncrypt.Encode("0");
                    else
                        snapshots.UIIndividualsCosts = model.EmployeeWelfareInfo.UIIndividualsCosts = ESP.Salary.Utility.DESEncrypt.Encode(!string.IsNullOrEmpty(txtUIICosts.Text) ? txtUIICosts.Text.Trim() : "0");
                    //生育保险公司应缴费用
                    if (drpInsurance_MemoPlace.SelectedItem.Value == "外阜城镇户口" || drpInsurance_MemoPlace.SelectedItem.Value == "外阜农业户口")
                        snapshots.BIFirmsCosts = model.EmployeeWelfareInfo.BIFirmsCosts = ESP.Salary.Utility.DESEncrypt.Encode("0");
                    else
                        snapshots.BIFirmsCosts = model.EmployeeWelfareInfo.BIFirmsCosts = ESP.Salary.Utility.DESEncrypt.Encode(!string.IsNullOrEmpty(txtBIFCosts.Text.Trim()) ? txtBIFCosts.Text.Trim() : "0");
                    //生育保险个人应缴费用
                    snapshots.BIIndividualsCosts = model.EmployeeWelfareInfo.BIIndividualsCosts = ESP.Salary.Utility.DESEncrypt.Encode("0");
                    //工伤险公司应缴费用
                    snapshots.CIFirmsCosts = model.EmployeeWelfareInfo.CIFirmsCosts = ESP.Salary.Utility.DESEncrypt.Encode(!string.IsNullOrEmpty(txtCIFCosts.Text.Trim()) ? txtCIFCosts.Text.Trim() : "0");
                    //工伤险个人应缴费用
                    snapshots.CIIndividualsCosts = model.EmployeeWelfareInfo.CIIndividualsCosts = ESP.Salary.Utility.DESEncrypt.Encode(!string.IsNullOrEmpty(txtCIFCosts.Text.Trim()) ? txtCIFCosts.Text.Trim() : "0");
                    //医疗保险公司应缴费用
                    snapshots.MIFirmsCosts = model.EmployeeWelfareInfo.MIFirmsCosts = ESP.Salary.Utility.DESEncrypt.Encode(!string.IsNullOrEmpty(txtMIFCosts.Text.Trim()) ? txtMIFCosts.Text.Trim() : "0");
                    //医疗保险个人应缴费用
                    snapshots.MIIndividualsCosts = model.EmployeeWelfareInfo.MIIndividualsCosts = ESP.Salary.Utility.DESEncrypt.Encode(!string.IsNullOrEmpty(txtMIICosts.Text.Trim()) ? txtMIICosts.Text.Trim() : "0");
                }
                catch { }
            }
            if (chkPRF.Checked)
            {
                ////公积金基数
                //snapshots.publicReserveFundsBase = model.EmployeeWelfareInfo.publicReserveFundsBase = ESP.Salary.Utility.DESEncrypt.Encode("0");
                ////公积金比例
                //snapshots.PRFProportionOfFirms = snapshots.PRFProportionOfIndividuals =
                //    model.EmployeeWelfareInfo.PRFProportionOfFirms = model.EmployeeWelfareInfo.PRFProportionOfIndividuals = decimal.Parse("0");
                //公积金开始时间
                snapshots.publicReserveFundsStarTime = model.EmployeeWelfareInfo.publicReserveFundsStarTime = DateTime.Parse("1900-01-01");
                //公积金应缴费用
                snapshots.PRFirmsCosts = snapshots.PRIIndividualsCosts =
                    model.EmployeeWelfareInfo.PRFirmsCosts = model.EmployeeWelfareInfo.PRIIndividualsCosts = ESP.Salary.Utility.DESEncrypt.Encode("0");
            }
            else
            {
                //公积金基数
                snapshots.publicReserveFundsBase = model.EmployeeWelfareInfo.publicReserveFundsBase = ESP.Salary.Utility.DESEncrypt.Encode(!string.IsNullOrEmpty(txtPublicReserveFunds_Base.Text.Trim()) ? txtPublicReserveFunds_Base.Text.Trim() : "0");
                //公积金比例
                snapshots.PRFProportionOfFirms = snapshots.PRFProportionOfIndividuals =
                    model.EmployeeWelfareInfo.PRFProportionOfFirms = model.EmployeeWelfareInfo.PRFProportionOfIndividuals = decimal.Parse(!string.IsNullOrEmpty(txtPRFProportionOfFirms.Text.Trim()) ? txtPRFProportionOfFirms.Text.Trim() : "0");
                try
                {
                    //公积金开始时间
                    snapshots.publicReserveFundsStarTime = model.EmployeeWelfareInfo.publicReserveFundsStarTime = DateTime.Parse(drpPublicReserveFundsStarTimeY.SelectedItem.Value + "-" + drpPublicReserveFundsStarTimeM.SelectedItem.Value + "-01");
                }
                catch { }
                //公积金应缴费用
                snapshots.PRFirmsCosts = snapshots.PRIIndividualsCosts =
                    model.EmployeeWelfareInfo.PRFirmsCosts = model.EmployeeWelfareInfo.PRIIndividualsCosts = ESP.Salary.Utility.DESEncrypt.Encode(!string.IsNullOrEmpty(txtPRFCosts.Text.Trim()) ? txtPRFCosts.Text.Trim() : "0");
            }
            //养老保险
            model.EmployeeWelfareInfo.endowmentInsurance = chkEnd.Checked;
            //失业保险
            model.EmployeeWelfareInfo.unemploymentInsurance = chkEnd.Checked;
            //生育险
            model.EmployeeWelfareInfo.birthInsurance = chkEnd.Checked;
            //工伤险
            model.EmployeeWelfareInfo.compoInsurance = chkEnd.Checked;
            //医疗保险
            model.EmployeeWelfareInfo.medicalInsurance = chkEnd.Checked;
            //公积金
            model.EmployeeWelfareInfo.publicReserveFunds = chkPRF.Checked;
            //户口所在地
            //model.domicilePlace = txtInsurance_DomicilePlace.Text.Trim();
            //社保所在公司
            model.EmployeeWelfareInfo.socialInsuranceCompany = drpInsurance_SocialInsuranceCompany.SelectedValue;
            //社保所属地点
            model.EmployeeWelfareInfo.socialInsuranceAddress = drpInsurance_SocialInsuranceAddress.SelectedValue;
            //社保编号
            //model.EmployeeWelfareInfo.socialInsuranceCode = txtInsurance_SocialInsuranceCode.Text.Trim();
            //医疗编号
            //model.EmployeeWelfareInfo.medicalInsuranceCode = txtInsurance_MedicalInsuranceCode.Text.Trim();

            //户口所在地
            snapshots.InsurancePlace = model.EmployeeWelfareInfo.InsurancePlace = drpInsurance_MemoPlace.SelectedValue;
            //公积金所在公司
            model.EmployeeWelfareInfo.publicReserveFundsCompany = drpInsurance_SocialInsuranceCompany.SelectedValue;
            //公积金所属地点
            model.EmployeeWelfareInfo.socialInsuranceAddress = drpInsurance_SocialInsuranceAddress.SelectedValue;

            //公积金编号
            //model.EmployeeWelfareInfo.publicReserveFundsCode = txtPublicReserveFunds_Code.Text.Trim();

            //在公司挂档
            //model.EmployeeWelfareInfo.isArchive = chkArchive_IsArchive.Checked;
            model.EmployeeWelfareInfo.ArchivePlace = drpArchive_ArchivePlace.SelectedValue;
            //户口所在地
            //model.domicilePlace = txtArchive_.Text.Trim();
            //存档日期
            model.EmployeeWelfareInfo.ArchiveDate = txtArchive_ArchiveDate.Text.Trim();
            //档案编号
            model.EmployeeWelfareInfo.ArchiveCode = txtArchive_Code.Text.Trim();

            model.EmployeeWelfareInfo.ComplementaryMedical = chkComplementaryMedical.Checked;
            if (chkComplementaryMedical.Checked)
            {
                //model.EmployeeWelfareInfo.ComplementaryMedicalCosts = decimal.Parse(txtComplementaryMedical.Text.Trim() == "" ? "0" : txtComplementaryMedical.Text.Trim());
            }
            model.EmployeeWelfareInfo.AccidentInsurance = chkAccidentInsurance.Checked;
            if (chkAccidentInsurance.Checked)
            {
                model.EmployeeWelfareInfo.AccidentInsuranceBeginTime = DateTime.Parse(drpAccidentInsuranceBeginTimeY.SelectedItem.Value + "-" + drpAccidentInsuranceBeginTimeM.SelectedItem.Value + "-01");
            }

            //snapshots.IsForeign = model.IsForeign = chkForeign.Checked;
            //snapshots.IsCertification = model.IsCertification = chkCertification.Checked;
            //snapshots.WageMonths = model.WageMonths = int.Parse(drpWageMonths.SelectedValue.Trim());
        }
    }

    protected void btnReturn_Click(object sender, EventArgs e)
    {
        Response.Redirect("EmployeesList.aspx");
    }

    protected void gvList_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Del")
        {
            string ids = e.CommandArgument.ToString();
            ESP.HumanResource.Entity.LogInfo logModel = new ESP.HumanResource.Entity.LogInfo();
            logModel.LogMedifiedTeme = DateTime.Now;
            logModel.Des = "为" + ESP.Framework.BusinessLogic.EmployeeManager.Get(int.Parse(ids.Split(',')[0])).FullNameCN + "删除了部门职务";
            logModel.LogUserId = UserInfo.UserID;
            logModel.LogUserName = UserInfo.Username;
            EmployeesInPositionsManager.Delete(int.Parse(ids.Split(',')[0]), int.Parse(ids.Split(',')[1]), int.Parse(ids.Split(',')[2]), logModel);
            ListBind();
        }
    }

    protected void gvList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
    }

    /// <summary>
    /// 获得用户部门职位信息
    /// </summary>
    private void ListBind()
    {
        List<ESP.HumanResource.Entity.EmployeesInPositionsInfo> list = EmployeesInPositionsManager.GetModelList(" a.userID = " + Request["userid"].ToString());
        gvList.DataSource = list;
        gvList.DataBind();

        //if (gvList.PageCount > 1)
        //{
        //    PageBottom.Visible = true;
        //}
        //else
        //{
        //    PageBottom.Visible = false;
        //}
        //if (list.Count > 0)
        //{
        //    tabBottom.Visible = true;
        //}
        //else
        //{
        //    tabBottom.Visible = false;
        //}

        //labAllNum.Text = list.Count.ToString();
        //labPageCount.Text = (gvList.PageIndex + 1).ToString() + "/" + gvList.PageCount.ToString();
        if (gvList.PageCount > 0)
        {
            if (gvList.PageIndex + 1 == gvList.PageCount)
                disButton("last");
            else if (gvList.PageIndex == 0)
                disButton("first");
            else
                disButton("");
        }
    }

    #region 分页设置
    protected void gvList_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvList.PageIndex = e.NewPageIndex;
        ListBind();
    }

    protected void btnLast_Click(object sender, EventArgs e)
    {
        Paging(gvList.PageCount);
    }
    protected void btnFirst_Click(object sender, EventArgs e)
    {
        Paging(0);
    }
    protected void btnNext_Click(object sender, EventArgs e)
    {
        Paging((gvList.PageIndex + 1) > gvList.PageCount ? gvList.PageCount : (gvList.PageIndex + 1));
    }
    protected void btnPrevious_Click(object sender, EventArgs e)
    {
        Paging((gvList.PageIndex - 1) < 0 ? 0 : (gvList.PageIndex - 1));
    }

    /// <summary>
    /// 翻页
    /// </summary>
    /// <param name="pageIndex">页码</param>
    private void Paging(int pageIndex)
    {
        GridViewPageEventArgs e = new GridViewPageEventArgs(pageIndex);
        gvList_PageIndexChanging(new object(), e);
    }

    /// <summary>
    /// 分页按钮的显示设置
    /// </summary>
    /// <param name="page"></param>
    private void disButton(string page)
    {
        //switch (page)
        //{
        //    case "first":
        //        btnFirst.Enabled = false;
        //        btnPrevious.Enabled = false;
        //        btnNext.Enabled = true;
        //        btnLast.Enabled = true;
        //        break;
        //    case "last":
        //        btnFirst.Enabled = true;
        //        btnPrevious.Enabled = true;
        //        btnNext.Enabled = false;
        //        btnLast.Enabled = false;
        //        break;
        //    default:
        //        btnFirst.Enabled = true;
        //        btnPrevious.Enabled = true;
        //        btnNext.Enabled = true;
        //        btnLast.Enabled = true;
        //        break;
        //}
    }

    #endregion

    protected void btnAddDepart_Click(object sender, EventArgs e)
    {
        Response.Redirect("EmployeeDepartmentEdit.aspx?userid=" + Request["userid"].ToString());
    }

    /// <summary>
    /// 保存员工的简历附件
    /// </summary>
    /// <param name="Code">员工编号</param>
    /// <returns>返回员工简历路径地址</returns>
    private string SaveFile(string Code)
    {
        try
        {
            HttpPostedFile myFile = fileCV.PostedFile;
            if (myFile.FileName != null && myFile.ContentLength > 0)//&& theFile.ContentLength <= Config.PHOTO_CONTENT_LENGTH)
            {
                Random random = new Random((int)DateTime.Now.Ticks);
                string fn = "/HR/ResumeFiles/" + Code + "_" + DateTime.Now.ToString("yyyyMMdd") + random.Next(100, 999).ToString() + fileCV.FileName;
                myFile.SaveAs(System.Web.HttpContext.Current.Server.MapPath(fn));
                return fn;
            }
            else
            {
                return "";
            }
        }
        catch (Exception ex)
        {
            ESP.Logging.Logger.Add(ex.ToString());
        }
        return "";
    }
}
