using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using ESP.HumanResource.BusinessLogic;
using ESP.HumanResource.Entity;
using ESP.HumanResource.Common;

namespace SEPAdmin.Management.UserManagement
{
    public partial class EmpMgt : ESP.Web.UI.PageBase
    {

        private const string photopath = "http://xy.shunyagroup.com/images/upload/";

        protected void Page_Load(object sender, EventArgs e)
        {
            #region AjaxProRegister

            AjaxPro.Utility.RegisterTypeForAjax(typeof(EmployeeBaseManager));
            AjaxPro.Utility.RegisterTypeForAjax(typeof(SEPAdmin.Management.UserManagement.EmpMgt));

            this.drpContract_Company.Attributes.Add("onChange", "changeContractCompany(this.options[this.selectedIndex].value,this.options[this.selectedIndex].text);");
            this.ddlSocialBranch.Attributes.Add("onChange", "changeSocialCompany(this.options[this.selectedIndex].value,this.options[this.selectedIndex].text);");

            #endregion

            if (!IsPostBack)
                initForm();


        }

        [AjaxPro.AjaxMethod]
        public static List<List<string>> getBranch()
        {
            IList<ESP.Finance.Entity.BranchInfo> branchList = ESP.Finance.BusinessLogic.BranchManager.GetAllList();
            List<List<string>> list = new List<List<string>>();
            List<string> item = null;
            foreach (ESP.Finance.Entity.BranchInfo branch in branchList)
            {
                item = new List<string>();
                item.Add(branch.BranchCode.ToString());
                item.Add(branch.BranchName);
                list.Add(item);
            }
            List<string> c = new List<string>();
            c.Add("-1");
            c.Add("请选择...");
            list.Insert(0, c);
            return list;
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            int userid = CurrentUserID;
            if (!string.IsNullOrEmpty(Request["userid"]))
            {
                userid = int.Parse(Request["userid"]);
            }
            ESP.HumanResource.Entity.EmployeeBaseInfo model = EmployeeBaseManager.GetModel(userid);
            IList<ESP.HumanResource.Entity.EmpEducationInfo> edulist = EmpEducationManager.GetList(" userid=" + model.UserID);

            if (string.IsNullOrEmpty(model.Photo) || model.Photo == "blank.jpg")
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('请上传个人照片！');", true);
                return;
            }
            if (edulist == null || edulist.Count == 0)
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('请上传个人教育情况！');", true);
                return;
            }
            if (!string.IsNullOrEmpty(this.txtBase_Birthday.Text))
            {
                model.Birthday = Convert.ToDateTime(this.txtBase_Birthday.Text);
            }
            model.Nation = this.txtNation.Text;
            model.BirthPlace = this.txtBase_PlaceOfBirth.Text;
            model.Political = this.ddlPolicity.SelectedItem.Text;

            model.Gender = int.Parse(this.txtBase_Sex.SelectedItem.Value);

            model.CreatedTime = Convert.ToDateTime(this.txtBase_CreateDate.Text);
            model.FirstNameCN = txtBase_FitstNameCn.Text;
            model.LastNameCN = txtBase_LastNameCn.Text;
            model.FirstNameEN = txtBase_FirstNameEn.Text;
            model.LastNameEN = txtBase_LastNameEn.Text;
            model.DomicilePlace = txtBase_DomicilePlace.Text;
            
            model.EmpProperty = int.Parse(ddlForeign.SelectedValue);

            model.WorkSpecialty = txtBase_WorkSpecialty.Text;
            model.IDNumber = txtBase_IdNo.Text;
            model.PrivateEmail = txtPrivateEmail.Text;
            model.IDValid = Convert.ToDateTime(txtIDValid.Text);
            model.Address2 = txtIdAddress.Text;
            model.Education = txtEducation.Text;
            model.EmergencyContact = txtBase_EmergencyLinkman.Text;

            model.WorkCity = ddlWorkCity.SelectedValue;

            model.MaritalStatus = int.Parse(txtBase_Marriage.SelectedItem.Value);

            if (ddlAnnualType.SelectedValue != "-1")
                model.AnnualLeaveBase = int.Parse(ddlAnnualType.SelectedValue);


            if (fileCV.PostedFile != null && fileCV.PostedFile.ContentLength > 0)
            {
                string filename = SaveFile(model.Code);
                model.Resume = filename;

            }
            else
            {
                if (string.IsNullOrEmpty(model.Resume))
                {
                    ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('请上传个人简历附件！');", true);
                    return;
                }
            }


            model.EmergencyContactPhone = txtBase_EmergencyPhone.Text;

            model.MobilePhone = txtBase_MobilePhone.Text;
            model.Address = txtBase_Address1.Text;
            model.PostCode = txtBase_PostCode.Text;
            model.HomePhone = txtBase_HomePhone.Text;

            if (!string.IsNullOrEmpty(txtJob_JoinDate.Text))
            {
                model.EmployeeJobInfo.joinDate = Convert.ToDateTime(txtJob_JoinDate.Text);
                model.JoinDate = Convert.ToDateTime(txtJob_JoinDate.Text);
            }
            if (!string.IsNullOrEmpty(txtWorkBegin.Text))
            {
                model.WorkBegin = Convert.ToDateTime(txtWorkBegin.Text);
            }
            //工资卡信息
            model.SalaryBank = txtSalaryBank.Text;
            model.SalaryCardNo = txtSalaryCardNo.Text;

            model.InternalEmail = txtBase_Email.Text;

            model.Residence = ddlHuji.SelectedValue;
            //备注
            model.Memo = txtJob_Memo.Text;
            model.WorkExperience = txtBase_WorkExperience.Text;

            model.BaseInfoOK = true;

            ESP.HumanResource.Entity.UsersInfo userModel = UsersManager.GetModel(model.UserID);

            ESP.HumanResource.Entity.LogInfo logModel = new ESP.HumanResource.Entity.LogInfo();
            logModel.LogUserId = userModel.UserID;
            logModel.LogMedifiedTeme = DateTime.Now;
            logModel.Des = "修改人员[" + userModel.LastNameCN + userModel.FirstNameCN + "]信息";
            logModel.LogUserName = userModel.Username;
            logModel.SysId = CurrentUserID;
            logModel.SysUserName = CurrentUserName;

            userModel.Username = model.FirstNameEN + "." + model.LastNameEN;
            userModel.FirstNameCN = txtBase_FitstNameCn.Text;
            userModel.LastNameCN = txtBase_LastNameCn.Text;
            userModel.Email = txtBase_Email.Text;
            if (EmployeeBaseManager.Update(model, userModel, null, logModel))
            {
                string backurl = "";
                if (!string.IsNullOrEmpty(Request["backurl"]))
                {
                    backurl = Request["backurl"];
                    ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('修改成功！');", true);
                }
                else
                {
                    ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('修改成功！');", true);
                }
            }
            else
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('修改失败,请重试！');", true);
            }


        }

        protected void initForm()
        {
            int userid = CurrentUserID;
            if (!string.IsNullOrEmpty(Request["userid"]) && int.Parse(Request["userid"]) != 0)
            {
                userid = int.Parse(Request["userid"]);
            }
            if (!string.IsNullOrEmpty(Request["tabindex"]))
            {
                this.TabContainer1.ActiveTabIndex = int.Parse(Request["tabindex"]);
            }

            ESP.HumanResource.Entity.EmployeeBaseInfo model = EmployeeBaseManager.GetModel(userid);

            if (CurrentUserID == userid && model.BaseInfoOK == false)
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('请您在使用星言云汇内网系统前，完善个人信息，谢谢！');", true);
            }

            ESP.HumanResource.Entity.UsersInfo userModel = UsersManager.GetModel(userid);

            lblUserCode.Text = model.Code;
            lblUserName.Text = userModel.LastNameCN + userModel.FirstNameCN + "(" + userModel.Username + ")";
            lblMobile.Text = model.Phone2;


            if (CurrentUserID == model.UserID)
            {
                TabPanel2.Enabled = false;
                TabPanel3.Enabled = false;
                TabPanel5.Enabled = false;
                TabPanel7.Enabled = false;
                ddlAnnualType.Enabled = false;
            }

            this.hidUserId.Value = model.UserID.ToString();
            //员工编号
            txtUserCode.Text = model.Code;
            // txtCommonName.Text = model.CommonName;
            txtBase_FitstNameCn.Text = userModel.FirstNameCN;
            txtBase_LastNameCn.Text = userModel.LastNameCN;
            //英文姓名
            txtBase_FirstNameEn.Text = userModel.FirstNameEN;
            txtBase_LastNameEn.Text = userModel.LastNameEN;

            if (!string.IsNullOrEmpty(model.Residence))
                ddlHuji.SelectedValue = model.Residence;

            txtBase_Sex.SelectedValue = model.Gender.ToString();

            ddlAnnualType.SelectedValue = model.AnnualLeaveBase.ToString();

            txtNation.Text = model.Nation;
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


            try
            {
                //入职日期
                txtJob_JoinDate.Text = model.EmployeeJobInfo.joinDate.Year < 1901 ? "" : model.EmployeeJobInfo.joinDate.ToString("yyyy-MM-dd");
                txtWorkBegin.Text = model.WorkBegin == null ? "" : model.WorkBegin.Value.ToString("yyyy-MM-dd");
            }
            catch { }
            //工资卡信息
            txtSalaryBank.Text = model.SalaryBank;
            txtSalaryCardNo.Text = model.SalaryCardNo;

            //备注
            txtJob_Memo.Text = model.Memo;
            try
            {
                //填表日期
                txtBase_CreateDate.Text = model.CreatedTime.Year < 1901 ? DateTime.Now.ToString("yyyy-MM-dd") : model.CreatedTime.ToString("yyyy-MM-dd");
            }
            catch { }
            //姓名
            //txtBase_Name.Text = model.FullNameCN;
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

            ddlPolicity.SelectedValue = model.Political;

            //照片
            if (!string.IsNullOrEmpty(model.Photo))
            {
                imgBase_Photo.ImageUrl = photopath + model.Photo;
            }

            //工作特长
            txtBase_WorkSpecialty.Text = model.WorkSpecialty;
            //身份证号码
            txtBase_IdNo.Text = model.IDNumber;
            txtPrivateEmail.Text = model.PrivateEmail;
            txtIdAddress.Text = model.Address2;
            if (model.IDValid != null)
            {
                txtIDValid.Text = model.IDValid.Value.ToString("yyyy-MM-dd");
            }
            txtEducation.Text = model.Education;
            //紧急事件联系人
            txtBase_EmergencyLinkman.Text = model.EmergencyContact;
            //健康状况
            // txtBase_Health.Text = model.Health;
            //婚否
            txtBase_Marriage.SelectedValue = model.MaritalStatus.ToString();

            ddlForeign.SelectedValue = model.EmpProperty.ToString();
            //紧急事件联系人电话
            txtBase_EmergencyPhone.Text = model.EmergencyContactPhone;

            ddlWorkCity.SelectedValue = model.WorkCity;
            //手机
            txtBase_MobilePhone.Text = model.MobilePhone;
            //宅电
            txtBase_HomePhone.Text = model.HomePhone;
            //通讯地址
            txtBase_Address1.Text = model.Address;
            //邮政编码
            txtBase_PostCode.Text = model.PostCode;
            //电子邮件
            txtBase_Email.Text = userModel.Email;
            //工作简历
            txtBase_WorkExperience.Text = model.WorkExperience;

            IList<ESP.HumanResource.Entity.EmpEducationInfo> edulist = EmpEducationManager.GetList(" userid=" + model.UserID);
            this.gvEducation.DataSource = edulist;
            this.gvEducation.DataBind();

            IList<ESP.HumanResource.Entity.EmpEstimateInfo> estlist = EmpEstimateManager.GetList(" userid=" + model.UserID);
            this.gvPingGu.DataSource = estlist;
            this.gvPingGu.DataBind();

            List<ESP.HumanResource.Entity.EmployeesInPositionsInfo> positionlist = EmployeesInPositionsManager.GetModelList(" a.userID = " + userid);
            gvPositionList.DataSource = positionlist;
            gvPositionList.DataBind();

            List<ESP.HumanResource.Entity.PositionLogInfo> positionloglist = ESP.HumanResource.BusinessLogic.PositionLogManager.GetList(" userid =" + model.UserID);
            gvPositionLog.DataSource = positionloglist;
            gvPositionLog.DataBind();

            IList<ESP.HumanResource.Entity.EmpContractInfo> contractlist = ESP.HumanResource.BusinessLogic.EmpContractManager.GetList(" userid=" + model.UserID.ToString());
            gvContract.DataSource = contractlist;
            gvContract.DataBind();

            ESP.HumanResource.Entity.EmployeesInPositionsInfo positionModel = ESP.HumanResource.BusinessLogic.EmployeesInPositionsManager.GetModel(model.UserID);

            ESP.Framework.Entity.DepartmentPositionInfo position = null;
            if (positionModel != null)
            {
                ESP.Framework.Entity.DepartmentInfo dept = null;
                ESP.Framework.Entity.DepartmentInfo dept2 = null;
                ESP.Framework.Entity.DepartmentInfo deptroot = null;

                dept = ESP.Framework.BusinessLogic.DepartmentManager.Get(positionModel.DepartmentID);
                dept2 = ESP.Framework.BusinessLogic.DepartmentManager.Get(dept.ParentID);
                deptroot = ESP.Framework.BusinessLogic.DepartmentManager.Get(dept2.ParentID);
                position = ESP.Framework.BusinessLogic.DepartmentPositionManager.Get(positionModel.DepartmentPositionID);

                this.txtJob_CompanyName.Text = deptroot.DepartmentName;
                this.hidCompanyId.Value = deptroot.DepartmentID.ToString();

                this.hidDepartmentID.Value = positionModel.DepartmentID.ToString();
                this.txtJob_DepartmentName.Text = dept2.DepartmentName;

                this.txtJob_GroupName.Text = dept.DepartmentName;
                this.hidGroupId.Value = dept.DepartmentID.ToString();

                txtJob_JoinJob.Value = position.DepartmentPositionID.ToString();
                txtPosition.Text = position.DepartmentPositionName;
                txtPositionDate.Text = positionModel.BeginDate.ToString("yyyy-MM-dd");
                lblDept.Text = deptroot.DepartmentName + "-" + dept2.DepartmentName + "-" + dept.DepartmentName;

            }
            ESP.Administrative.Entity.OperationAuditManageInfo audit = (new ESP.Administrative.BusinessLogic.OperationAuditManageManager()).GetOperationAuditModelByUserID(model.UserID);

            if (audit != null)
            {
                this.hidAuditer.Value = audit.TeamLeaderID.ToString();
                this.txtAuditer.Text = audit.TeamLeaderName;
            }

            //试用期
            txtProbation.Text = model.ProbationDate == null ? model.EmployeeJobInfo.joinDate.AddMonths(6).AddDays(-1).ToString("yyyy-MM-dd") : model.ProbationDate.Value.ToString("yyyy-MM-dd");
            //入职日期
            lblJoinDate.Text = model.EmployeeJobInfo.joinDate.ToString("yyyy-MM-dd");

            txtSignDate.Text = model.ContractSignDate == null ? DateTime.Now.ToString("yyyy-MM-dd") : model.ContractSignDate.Value.ToString("yyyy-MM-dd");

            this.txtContractYear.Text = model.ContractYear == 0 ? "3" : model.ContractYear.ToString();

            txtContractBegin.Text = model.FirstContractBeginDate == null ? model.EmployeeJobInfo.joinDate.ToString("yyyy-MM-dd") : model.FirstContractBeginDate.Value.ToString("yyyy-MM-dd");

            txtContractEnd.Text = model.FirstContractEndDate == null ? model.EmployeeJobInfo.joinDate.AddYears(3).AddDays(-1).ToString("yyyy-MM-dd") : model.FirstContractEndDate.Value.ToString("yyyy-MM-dd");

            txtSignDate.Text = model.ContractSignDate == null ? DateTime.Now.ToString("yyyy-MM-dd") : model.ContractSignDate.Value.ToString("yyyy-MM-dd");

            drpContract_Company.SelectedValue = model.BranchCode;
            txtBranch.Text = model.BranchCode;
            hidContactBranch.Value = model.BranchCode;

            ESP.HumanResource.Entity.EmployeeWelfareInfo social = ESP.HumanResource.BusinessLogic.EmployeeWelfareManager.getModelBySysId(model.UserID);
            if (social != null)
            {
                ddlSocialBranch.SelectedValue = social.socialInsuranceCompany;
                txtSocialBranch.Text = social.socialInsuranceCompany;
                hidSocialBranch.Value = social.socialInsuranceCompany;
                txtWelfare.Text = social.socialInsuranceAddress;
                if (social.isArchive == true)
                    ddlFile.SelectedIndex = 0;//公司存档
                else
                    ddlFile.SelectedIndex = 1;

            }

            IList<EmpLogInfo> welfareLog = EmpLogManager.GetList(" userid = " + model.UserID);
            if (welfareLog != null && welfareLog.Count > 0)
            {
                foreach (EmpLogInfo log in welfareLog)
                {
                    lblWelfareLog.Text += log.Operator + "于" + log.EditTime.ToString("yyyy-MM-dd") + "操作:" + log.Remark + "<br/>";
                }
            }
        }



        protected void gvPositionList_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

        protected void btnSaveEstimate_Click(object sender, EventArgs e)
        {

        }


        protected void btnContractSave_Click(object sender, EventArgs e)
        {
            int userid = CurrentUserID;
            if (!string.IsNullOrEmpty(Request["userid"]))
            {
                userid = int.Parse(Request["userid"]);
            }

            ESP.HumanResource.Entity.EmployeeBaseInfo model = EmployeeBaseManager.GetModel(userid);

            IList<ESP.HumanResource.Entity.EmpContractInfo> contractAttendlist = ESP.HumanResource.BusinessLogic.EmpContractManager.GetList(" userid=" + userid);

            model.ContractYear = string.IsNullOrEmpty(this.txtContractYear.Text) ? 3 : Convert.ToInt32(this.txtContractYear.Text);

            model.BranchCode = hidContactBranch.Value;

            model.FirstContractBeginDate = string.IsNullOrEmpty(txtContractBegin.Text) ? DateTime.Now : Convert.ToDateTime(txtContractBegin.Text);
            model.FirstContractEndDate = string.IsNullOrEmpty(txtContractEnd.Text) ? DateTime.Now : Convert.ToDateTime(txtContractEnd.Text);
            model.ContractSignDate = string.IsNullOrEmpty(txtSignDate.Text) ? DateTime.Now : Convert.ToDateTime(txtSignDate.Text);
            model.ProbationDate = string.IsNullOrEmpty(txtProbation.Text) ? model.EmployeeJobInfo.joinDate.AddMonths(6).AddDays(-1) : Convert.ToDateTime(txtProbation.Text);

            if (contractAttendlist == null || contractAttendlist.Count == 0)
            {
                model.ContractBeginDate = model.FirstContractBeginDate;
                model.ContractEndDate = model.FirstContractEndDate;

            }

            ESP.HumanResource.BusinessLogic.EmployeeBaseManager.Update(model);

            initForm();

        }


        protected void btnDelEsitimate_Click(object sender, EventArgs e)
        {

        }
        protected void btnSocialSave_Click(object sender, EventArgs e)
        {
            int userid = CurrentUserID;
            if (!string.IsNullOrEmpty(Request["userid"]))
            {
                userid = int.Parse(Request["userid"]);
            }
            ESP.HumanResource.Entity.EmployeeWelfareInfo model = ESP.HumanResource.BusinessLogic.EmployeeWelfareManager.getModelBySysId(userid);

            string originalCompany = model.socialInsuranceCompany;
            string originalAddress = model.socialInsuranceAddress;

            if (model == null)
            {
                model = new EmployeeWelfareInfo();
            }
            model.socialInsuranceCompany = hidSocialBranch.Value;
            model.socialInsuranceAddress = txtWelfare.Text;

            if (ddlFile.SelectedIndex == 0)
                model.isArchive = true;//公司存档
            else
                model.isArchive = false;

            model.sysid = userid;

            if (model.id == 0)
            {
                ESP.HumanResource.BusinessLogic.EmployeeWelfareManager.Add(model, null);
            }
            else
            {
                ESP.HumanResource.BusinessLogic.EmployeeWelfareManager.Update(model, null);

                if (originalCompany != model.socialInsuranceCompany || originalAddress != model.socialInsuranceAddress)
                {
                    EmpLogInfo log = new EmpLogInfo();
                    log.OperateType = 1;
                    log.Operator = CurrentUserName;
                    log.OperatorId = CurrentUserID;
                    log.EditTime = DateTime.Now;
                    log.UserId = model.sysid;
                    log.Remark = "社保公司由" + originalCompany + "(" + originalAddress + ")" + "变更为" + model.socialInsuranceCompany + "(" + model.socialInsuranceAddress + ")";
                    EmpLogManager.Add(log);
                }
            }

            initForm();
        }

        protected void btnKaoqin_Click(object sender, EventArgs e)
        {
            int userid = CurrentUserID;
            if (!string.IsNullOrEmpty(Request["userid"]))
            {
                userid = int.Parse(Request["userid"]);
            }
            ESP.Administrative.Entity.OperationAuditManageInfo audit = (new ESP.Administrative.BusinessLogic.OperationAuditManageManager()).GetOperationAuditModelByUserID(userid);
            if (audit != null && audit.TeamLeaderID != int.Parse(hidAuditer.Value))
            {
                audit.TeamLeaderID = int.Parse(hidAuditer.Value);
                audit.TeamLeaderName = this.txtAuditer.Text;
                (new ESP.Administrative.BusinessLogic.OperationAuditManageManager()).Update(audit);

            }
            else
            {
                ESP.HumanResource.Entity.EmployeesInPositionsInfo positionModel = ESP.HumanResource.BusinessLogic.EmployeesInPositionsManager.GetModel(userid);
                if (positionModel != null)
                {
                    ESP.Framework.Entity.OperationAuditManageInfo depaudit = ESP.Framework.BusinessLogic.OperationAuditManageManager.GetModelByDepId(positionModel.DepartmentID);
                    ESP.Framework.Entity.DepartmentInfo deptModel = ESP.Framework.BusinessLogic.DepartmentManager.Get(positionModel.DepartmentID);

                    audit = new ESP.Administrative.Entity.OperationAuditManageInfo();
                    audit.AreaID = int.Parse(deptModel.Description);
                    audit.CreateTime = DateTime.Now;
                    audit.Deleted = false;
                    audit.HRAdminID = depaudit.HRId;
                    audit.HRAdminName = depaudit.HRName;
                    audit.ManagerID = depaudit.ManagerId;
                    audit.ManagerName = depaudit.ManagerName;
                    audit.OperatorID = int.Parse(CurrentUser.SysID);
                    audit.StatisticianID = depaudit.Hrattendanceid;
                    audit.StatisticianName = depaudit.Hrattendancename;
                    audit.UserID = userid;
                    audit.TeamLeaderID = int.Parse(hidAuditer.Value);
                    audit.TeamLeaderName = this.txtAuditer.Text;
                    audit.UpdateTime = DateTime.Now;
                    (new ESP.Administrative.BusinessLogic.OperationAuditManageManager()).Add(audit);
                }

            }
            initForm();
        }

        protected void btnSavePosition_Click(object sender, EventArgs e)
        {
            int userid = CurrentUserID;
            if (!string.IsNullOrEmpty(Request["userid"]))
            {
                userid = int.Parse(Request["userid"]);
            }

            //新增职务
            ESP.HumanResource.Entity.EmployeesInPositionsInfo posmodel = new ESP.HumanResource.Entity.EmployeesInPositionsInfo();
            //当前职务
            ESP.HumanResource.Entity.EmployeesInPositionsInfo oldPosition = ESP.HumanResource.BusinessLogic.EmployeesInPositionsManager.GetModel(userid);

            ESP.HumanResource.Entity.EmployeeBaseInfo model = EmployeeBaseManager.GetModel(userid);
            ESP.HumanResource.Entity.UsersInfo userModel = UsersManager.GetModel(userid);

            if (!string.IsNullOrEmpty(hidGroupId.Value))
            {
                posmodel.DepartmentID = int.Parse(hidGroupId.Value);
            }
            posmodel.DepartmentPositionID = int.Parse(txtJob_JoinJob.Value);
            posmodel.UserID = userid;
            posmodel.IsManager = false;
            posmodel.IsActing = false;
            posmodel.BeginDate = DateTime.Parse(txtPositionDate.Text);

            PositionLogInfo posLogInfo = null;

            if (oldPosition != null)
            {
                posLogInfo = new PositionLogInfo();
                ESP.Framework.Entity.DepartmentPositionInfo dPInfo = ESP.Framework.BusinessLogic.DepartmentPositionManager.Get(oldPosition.DepartmentPositionID);
                PositionBaseInfo pBInfo = PositionBaseManager.GetModel(dPInfo.PositionBaseId);

                posLogInfo.UserId = userModel.UserID;
                posLogInfo.UserName = userModel.Username;
                posLogInfo.UserCode = model.Code;
                posLogInfo.DepartmentId = oldPosition.DepartmentID;
                posLogInfo.DepartmentName = dPInfo.DepartmentName;
                posLogInfo.PositionId = dPInfo.DepartmentPositionID;
                posLogInfo.PositionName = dPInfo.DepartmentPositionName;

                if (pBInfo != null)
                {
                    posLogInfo.PositionBaseId = pBInfo.Id;
                    posLogInfo.PositionBaseName = pBInfo.PositionName;

                    posLogInfo.LevelId = pBInfo.LeveId;
                    posLogInfo.LevelName = pBInfo.LevelName;

                }

                posLogInfo.BeginDate = oldPosition.BeginDate;
                posLogInfo.EndDate = posmodel.BeginDate;
                posLogInfo.CreateDate = DateTime.Now;
            }

            ESP.HumanResource.Entity.LogInfo logModel = new ESP.HumanResource.Entity.LogInfo();
            logModel.LogMedifiedTeme = DateTime.Now;
            logModel.Des = "为" + ESP.Framework.BusinessLogic.EmployeeManager.Get(userid).FullNameCN + "新增部门职务";
            logModel.LogUserId = model.UserID;
            logModel.LogUserName = model.Username;
            logModel.SysId = CurrentUserID;
            logModel.SysUserName = CurrentUserName;



            EmployeesInPositionsManager.Add(posmodel, logModel, posLogInfo);

            initForm();
        }

        //gvEducation
        protected void gvEducation_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Del")
            {
                string workid = e.CommandArgument.ToString();
                ESP.HumanResource.BusinessLogic.EmpEducationManager.Delete(int.Parse(workid));
                initForm();
            }
        }

        //gvEducation
        protected void gvContract_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Del")
            {
                string contractid = e.CommandArgument.ToString();
                ESP.HumanResource.BusinessLogic.EmpContractManager.Delete(int.Parse(contractid));
                initForm();
            }
        }

        protected void gvWork_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Del")
            {
                string workid = e.CommandArgument.ToString();
                ESP.HumanResource.BusinessLogic.EmpWorkExpManager.Delete(int.Parse(workid));
                initForm();
            }
        }

        protected void btnReturn_Click(object sender, EventArgs e)
        {
            Response.Redirect("/HR/Employees/EmployeesAllList.aspx");
        }
        //gvMember
        protected void gvMember_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Del")
            {
                string memberid = e.CommandArgument.ToString();
                ESP.HumanResource.BusinessLogic.EmpFamilyManager.Delete(int.Parse(memberid));
                initForm();
            }
        }

        protected void gvPositionList_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Del")
            {
                string ids = e.CommandArgument.ToString();
                ESP.Framework.Entity.EmployeeInfo emp = ESP.Framework.BusinessLogic.EmployeeManager.Get(int.Parse(ids.Split(',')[0]));

                ESP.HumanResource.Entity.LogInfo logModel = new ESP.HumanResource.Entity.LogInfo();
                logModel.LogMedifiedTeme = DateTime.Now;
                logModel.Des = "为" + emp.FullNameCN + "删除了部门职务";
                logModel.LogUserId = emp.UserID;
                logModel.LogUserName = emp.Username;
                logModel.SysId = CurrentUserID;
                logModel.SysUserName = CurrentUserName;

                EmployeesInPositionsManager.Delete(int.Parse(ids.Split(',')[0]), int.Parse(ids.Split(',')[1]), int.Parse(ids.Split(',')[2]), logModel);

                //更新PositionLog信息
                PositionLogInfo pLInfo = PositionLogManager.GetModel(UserInfo.UserID, int.Parse(ids.Split(',')[1]));
                if (pLInfo != null)
                {
                    pLInfo.EndDate = DateTime.Now;
                    PositionLogManager.Update(pLInfo);
                }
                initForm();
            }


        }
        protected void gvPingGu_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

        protected void btnAddDepart_Click(object sender, EventArgs e)
        {
            int userid = CurrentUserID;
            if (!string.IsNullOrEmpty(Request["userid"]))
            {
                userid = int.Parse(Request["userid"]);
            }
            Response.Redirect("EmployeeDepartmentEdit.aspx?userid=" + userid);
        }

        private void GetModels(ESP.HumanResource.Entity.SnapshotsInfo snap)
        {
            int userid = CurrentUserID;
            if (!string.IsNullOrEmpty(Request["userid"]))
            {
                userid = int.Parse(Request["userid"]);
            }

            ESP.HumanResource.Entity.EmployeeBaseInfo ebi = EmployeeBaseManager.GetModel(userid);

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
            if (ebi.EmployeeWelfareInfo.endowmentInsuranceStarTime.Year > 1910)
                snap.endowmentInsuranceStarTime = ebi.EmployeeWelfareInfo.endowmentInsuranceStarTime;

            if (ebi.EmployeeWelfareInfo.endowmentInsuranceEndTime.Year > 1910)
                snap.endowmentInsuranceEndTime = ebi.EmployeeWelfareInfo.endowmentInsuranceEndTime;

            if (ebi.EmployeeWelfareInfo.unemploymentInsuranceStarTime.Year > 1910)
                snap.unemploymentInsuranceStarTime = ebi.EmployeeWelfareInfo.unemploymentInsuranceStarTime;

            if (ebi.EmployeeWelfareInfo.unemploymentInsuranceEndTime.Year > 1910)
                snap.unemploymentInsuranceEndTime = ebi.EmployeeWelfareInfo.unemploymentInsuranceEndTime;

            if (ebi.EmployeeWelfareInfo.birthInsuranceStarTime.Year > 1910)
                snap.birthInsuranceStarTime = ebi.EmployeeWelfareInfo.birthInsuranceStarTime;

            if (ebi.EmployeeWelfareInfo.birthInsuranceEndTime.Year > 1910)
                snap.birthInsuranceEndTime = ebi.EmployeeWelfareInfo.birthInsuranceEndTime;

            if (ebi.EmployeeWelfareInfo.compoInsuranceStarTime.Year > 1910)
                snap.compoInsuranceStarTime = ebi.EmployeeWelfareInfo.compoInsuranceStarTime;

            if (ebi.EmployeeWelfareInfo.compoInsuranceEndTime.Year > 1910)
                snap.compoInsuranceEndTime = ebi.EmployeeWelfareInfo.compoInsuranceEndTime;

            if (ebi.EmployeeWelfareInfo.medicalInsuranceStarTime.Year > 1910)
                snap.medicalInsuranceStarTime = ebi.EmployeeWelfareInfo.medicalInsuranceStarTime;

            if (ebi.EmployeeWelfareInfo.medicalInsuranceEndTime.Year > 1910)
                snap.medicalInsuranceEndTime = ebi.EmployeeWelfareInfo.medicalInsuranceEndTime;

            if (ebi.EmployeeWelfareInfo.publicReserveFundsStarTime.Year > 1910)
                snap.publicReserveFundsStarTime = ebi.EmployeeWelfareInfo.publicReserveFundsStarTime;

            if (ebi.EmployeeWelfareInfo.publicReserveFundsEndTime.Year > 1910)
                snap.publicReserveFundsEndTime = ebi.EmployeeWelfareInfo.publicReserveFundsEndTime;

            if (ebi.EmployeeWelfareInfo.contractStartDate.Year > 1910)
                snap.contractStartDate = ebi.EmployeeWelfareInfo.contractStartDate;

            if (ebi.EmployeeWelfareInfo.contractEndDate.Year > 1910)
                snap.contractEndDate = ebi.EmployeeWelfareInfo.contractEndDate;

            if (ebi.EmployeeWelfareInfo.probationEndDate.Year > 1910)
                snap.probationEndDate = ebi.EmployeeWelfareInfo.probationEndDate;

            snap.socialInsuranceBase = ebi.EmployeeWelfareInfo.socialInsuranceBase;
            snap.medicalInsuranceBase = ebi.EmployeeWelfareInfo.medicalInsuranceBase;
            snap.publicReserveFundsBase = ebi.EmployeeWelfareInfo.publicReserveFundsBase;
            snap.isArchive = ebi.EmployeeWelfareInfo.isArchive;
            snap.contractSignInfo = ebi.EmployeeWelfareInfo.contractSignInfo;
            snap.InsurancePlace = ebi.EmployeeWelfareInfo.InsurancePlace;

        }

        private string SaveFile(string Code)
        {
            HttpPostedFile myFile = fileCV.PostedFile;

            if (myFile.FileName != null && myFile.ContentLength > 0)//&& theFile.ContentLength <= Config.PHOTO_CONTENT_LENGTH)
            {
                Random random = new Random((int)DateTime.Now.Ticks);
                string fn = "/HR/ResumeFiles/" + Code + "_" + DateTime.Now.ToString("yyyyMMdd") + random.Next(100, 999).ToString() + fileCV.FileName;
                //try
                //{
                myFile.SaveAs(System.Web.HttpContext.Current.Server.MapPath(fn));
                //}
                //catch (Exception e)
                //{ }
                return fn;
            }
            else
            {
                return "";
            }

        }

        protected void btnSavePhoto_Click(object sender, EventArgs e)
        {
            int userid = CurrentUserID;
            if (!string.IsNullOrEmpty(Request["userid"]))
            {
                userid = int.Parse(Request["userid"]);
            }

            if (Request.Files[0].ContentLength > 0)
            {
                ESP.Framework.Entity.EmployeeInfo info = ESP.Framework.BusinessLogic.EmployeeManager.Get(userid);
                info.Photo = Portal.Common.ImageHelper.SaveIcon(Request.Files[0].InputStream, userid, "", true) + ".jpg";
                ESP.Framework.BusinessLogic.EmployeeManager.Update(info);

                imgBase_Photo.ImageUrl = photopath + info.Photo;
            }
        }

    }
}