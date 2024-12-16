using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ESP.HumanResource.Entity;
using ESP.HumanResource.BusinessLogic;
using System.Net.Mail;


namespace SEPAdmin.HR.Join
{
    public partial class HeadAccountInterview : ESP.Web.UI.PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindData();

                //drpWorkCityBind();
                drpUserTypeBind();
            }

        }

        protected void initForm(int sysid)
        {
            try
            {
                ESP.HumanResource.Entity.EmployeeBaseInfo model = EmployeeBaseManager.GetModel(sysid);
                ESP.HumanResource.Entity.UsersInfo user = UsersManager.GetModel(sysid);
                ESP.Administrative.Entity.OperationAuditManageInfo audit = (new ESP.Administrative.BusinessLogic.OperationAuditManageManager()).GetOperationAuditModelByUserID(user.UserID);
                ESP.HumanResource.Entity.EmployeesInPositionsInfo position = ESP.HumanResource.BusinessLogic.EmployeesInPositionsManager.GetModel(user.UserID);
                ESP.Framework.Entity.OperationAuditManageInfo operation = ESP.Framework.BusinessLogic.OperationAuditManageManager.GetModelByDepId(position.DepartmentID);

                txtBase_LastNameCn.Text = user.LastNameCN;
                txtBase_FirstNameCn.Text = user.FirstNameCN;
                txtPrivateEmail.Text = model.PrivateEmail;
                txtJob_JoinDate.Text = model.EmployeeJobInfo.joinDate.ToString("yyyy-MM-dd");
             
                txtMobilePhone.Text = model.MobilePhone;

                hidGroupId.Value = model.EmployeeJobInfo.groupid.ToString();
                txtJob_Memo.Text = model.Memo;
                txtIDCard.Text = model.IDNumber;
                txtNowBasePay.Text = model.Pay.ToString();
                txtNowMeritPay.Text = model.Performance.ToString();
                txtNowAttendance.Text = model.Attendance.ToString();

                //if (audit != null)
                //{
                //    this.txtAuditer.Text = operation.HeadCountDirector;
                //    this.hidAuditer.Value = operation.HeadCountDirectorId.ToString();
                //}
                if (position != null)
                {
                    //this.txtPosition.Text = position.DepartmentPositionName;
                    this.txtJob_JoinJob.Value = position.DepartmentPositionID.ToString();
                    this.lblJoinPosition.Text = position.DepartmentPositionName;
                }

                List<ESP.HumanResource.Entity.EmployeesInPositionsInfo> eiplist = ESP.HumanResource.BusinessLogic.EmployeesInPositionsManager.GetModelList(" a.userid=" + model.UserID);
                ESP.HumanResource.Entity.EmployeesInPositionsInfo deps = eiplist[0];

                txtJob_JoinJob.Value = deps.DepartmentPositionID.ToString();

                if (!string.IsNullOrEmpty(model.WorkCity) || !string.IsNullOrEmpty(model.WorkCountry))
                {
                    ddltype.SelectedValue = model.WorkCity;
                }

                drpOfferTemplate.SelectedValue = model.OfferLetterTemplate.ToString();  // Offer Letter模板
                chkExamen.Checked = model.IsExamen;   // 是否是应届毕业生。
                txtSeniority.Text = model.Seniority.ToString();
            }
            catch (Exception ex)
            {
               // ESP.Logging.Logger.Add(ex.ToString());
            }
        }


        private void BindData()
        {
            int haid = int.Parse(Request["haid"]);
            HeadAccountInfo model = new HeadAccountManager().GetModel(haid);
            var operation = ESP.Framework.BusinessLogic.OperationAuditManageManager.GetModelByDepId(model.GroupId);
            hidKaoqin.Value = operation.HeadCountDirectorId.ToString();
            txtKaoqin.Text = operation.HeadCountDirector;
            hidGroupId.Value = model.GroupId.ToString();
            var deptModel = ESP.Finance.BusinessLogic.DepartmentViewManager.GetModel(model.GroupId);

            PositionLevelsInfo pb = ESP.HumanResource.BusinessLogic.PositionLevelsManager.GetModel(model.LevelId);

            this.lblDept.Text = deptModel.level1 + "-" + deptModel.level2 + "-" + deptModel.level3;
            lblCreator.Text = model.Creator;
            lblAppDate.Text = model.CreateDate.Value.ToString();
            this.lblRemark.Text = model.Remark;
            this.lblPosition.Text = model.Position;
            
            this.lblJoinPosition.Text = model.Position;
            this.txtJob_JoinJob.Value = model.PositionId.ToString();

            this.lblLevel.Text = model.LevelName;
            this.chkAAD.Checked = model.IsAAD;
            this.chkAAD.Enabled = false;

            this.lblCustomer.Text = model.CustomerName;
            this.lblReplaceReason.Text = model.ReplaceReason;
            if (model.ReplaceUserId != 0)
            {
                ESP.Framework.Entity.UserInfo replaceModel = ESP.Framework.BusinessLogic.UserManager.Get(model.ReplaceUserId);
                this.lblReplaceUser.Text = replaceModel.FullNameCN + "  " + model.ReplaceUserPosition;
            }
            this.lblRequestment.Text = model.Requestment;
            this.lblResponse.Text = model.Response;
            this.lblDimissionDate.Text =model.DimissionDate==null?"": model.DimissionDate.Value.ToString("yyyy-MM-dd");
            if (model.NewBiz == "立项")
            {
                chkCreate.Checked = true;
                chkUnCreate.Checked = false;
            }
            else if (model.NewBiz == "未立项")
            {
                chkCreate.Checked = false;
                chkUnCreate.Checked = true;
            }
            else
            {
                chkCreate.Checked = false;
                chkUnCreate.Checked = false;
            }


            var loglist = new ESP.HumanResource.BusinessLogic.HeadAccountLogManager().GetList(" and HeadAccountId=" + model.Id);

            foreach (var log in loglist)
            {
                string auditstatus = "审批通过";
                if (log.Status == 2)
                    auditstatus = "审批驳回";
                else if (log.Status == 3)
                {
                    auditstatus = "审批留言";
                }
                string logstr = string.Format("[{0}] {1}{2}：{3}（{4}）", ESP.HumanResource.Common.Status.HeadAccountStatus_Names[log.AuditType], log.Auditor, auditstatus, log.Remark, log.AuditDate.ToString());
                lblLog.Text += logstr + "</br>";
            }

            this.lblSalary.Text = pb.SalaryLow.ToString("#,##0.00") + " - " + pb.SalaryHigh.ToString("#,##0.00");

            if (model.TalentId != 0)
            {
                BindTalent(model.TalentId);
            }
            if (model.OfferLetterUserId != 0)
                initForm(model.OfferLetterUserId);
        }

        private void BindTalent(int talentId)
        {
            try
            {
                ESP.HumanResource.Entity.TalentInfo talentModel = (new TalentManager()).GetModel(talentId);

                txtBase_LastNameCn.Text = talentModel.NameCN.Substring(0, 1);
                txtBase_FirstNameCn.Text = talentModel.NameCN.Substring(1);
                txtPrivateEmail.Text = talentModel.EMail;

                txtMobilePhone.Text = talentModel.Mobile;
                txtBirthday.Text = talentModel.BirthDay.ToString("yyyy-MM-dd");

                txtHR.Text = talentModel.HRInterview;

            }
            catch (Exception ex)
            {
            }
        }



        /// <summary>
        /// 绑定用户类型信息
        /// </summary>
        protected void drpUserTypeBind()
        {
            drpUserType.DataSource = ESP.Framework.BusinessLogic.EmployeeManager.GetTypes();
            drpUserType.DataTextField = "value";
            drpUserType.DataValueField = "key";
            drpUserType.DataBind();
        }
        protected void initModel(HeadAccountInfo headAccountModel,ESP.Finance.Entity.DepartmentViewInfo deptModel, ESP.HumanResource.Entity.EmployeeBaseInfo model, ESP.HumanResource.Entity.EmployeeJobInfo jobModel, ESP.HumanResource.Entity.EmployeesInPositionsInfo depsModel)
        {
            if (!string.IsNullOrEmpty(this.hidDimissionCode.Value))
            {
                model.Code = this.hidDimissionCode.Value;
            }
            if (headAccountModel.TalentId != 0)
            {
                ESP.HumanResource.Entity.TalentInfo talentModel = (new ESP.HumanResource.BusinessLogic.TalentManager()).GetModel(headAccountModel.TalentId);
                model.Resume = talentModel.ResumeFiles;
            }
            model.LastNameCN = txtBase_LastNameCn.Text.Trim();
            model.FirstNameCN = txtBase_FirstNameCn.Text.Trim();
            model.Memo = txtJob_Memo.Text.Trim();
            model.IDNumber = txtIDCard.Text.Trim();
            model.PrivateEmail = txtPrivateEmail.Text.Trim();
            //员工工作地点
            model.WorkCity = ddltype.SelectedValue;
            model.TypeID = int.Parse(drpUserType.SelectedValue);

            model.OfferLetterTemplate = int.Parse(drpOfferTemplate.SelectedValue);  // Offer Letter模板
            model.CreatedTime = DateTime.Now;
            model.Creator = UserID;
            model.CreatorName = UserInfo.FullNameCN;
            model.LastModifiedTime = DateTime.Now;
            model.LastModifier = UserID;
            model.OfferLetterSender = UserID;
            model.OfferLetterSendTime = DateTime.Now;
            model.IsExamen = chkExamen.Checked;
            model.Seniority = string.IsNullOrEmpty(txtSeniority.Text.Trim()) ? 0 : int.Parse(txtSeniority.Text.Trim());
            model.MobilePhone = txtMobilePhone.Text.Trim();

            model.Education = this.txtEducation.Text;
            model.Birthday = string.IsNullOrEmpty(this.txtBirthday.Text) ? new DateTime(1754, 1, 1) : DateTime.Parse(this.txtBirthday.Text);
            model.Gender = (int.Parse(this.ddlGender.SelectedValue));
            model.MaritalStatus = (int.Parse(this.ddlMarry.SelectedValue));
            model.BirthPlace = this.txtLocation.Text;
            model.Address = this.txtAddress.Text;

            model.Appearance = this.ddlAppearance.SelectedValue;
            model.Know = this.ddlKnow.SelectedValue;
            model.Equal = this.ddlEqual.SelectedValue;
            model.EQ = this.txtEQ.Text;
            model.FourD = this.txt4D.Text;
            model.Quality = this.ddlQuality.SelectedValue;
            model.Motivation = this.ddlReason.SelectedValue;
            model.JoinDate = DateTime.Parse(txtJob_JoinDate.Text);
            model.WorkBegin = DateTime.Parse(txtJob_JoinDate.Text);
            model.AnnualLeaveBase=0;
            model.ContractBeginDate=DateTime.Now;
            model.ContractEndDate=DateTime.Now;
            model.ContractSignDate =DateTime.Now;
            
            string nowBasePay = ""; // 工资
            string nowMeritPay = "";  // 绩效
            if (!string.IsNullOrEmpty(txtNowBasePay.Text))
                nowBasePay = txtNowBasePay.Text.Trim();
            if (!string.IsNullOrEmpty(txtNowMeritPay.Text))
                nowMeritPay = txtNowMeritPay.Text.Trim();
            if (nowBasePay != "")
            {
                model.Pay = Convert.ToDecimal(nowBasePay);
            }
            if (nowMeritPay != "")
            {
                model.Performance = Convert.ToDecimal(nowMeritPay);
            }
            if (!string.IsNullOrEmpty(txtNowAttendance.Text.Trim()))
                model.Attendance = decimal.Parse(txtNowAttendance.Text.Trim());

            try
            {
                jobModel.joinDate = DateTime.Parse(txtJob_JoinDate.Text.Trim());
            }
            catch { }
            jobModel.joinJob = ESP.Framework.BusinessLogic.DepartmentPositionManager.Get(int.Parse(txtJob_JoinJob.Value)).DepartmentPositionName;
            jobModel.joinjobID = int.Parse(txtJob_JoinJob.Value);

            
           
            depsModel.DepartmentID = deptModel.level3Id;

            jobModel.companyid = deptModel.level1Id;
            jobModel.companyName = deptModel.level1;

            jobModel.departmentid = deptModel.level2Id;
            jobModel.departmentName = deptModel.level2;

            jobModel.groupid = deptModel.level3Id;
            jobModel.groupName = deptModel.level3;

            depsModel.DepartmentPositionID = int.Parse(txtJob_JoinJob.Value);
            depsModel.IsActing = false;
            depsModel.IsManager = false;
            depsModel.BeginDate = jobModel.joinDate;
        }



        protected void btnAudit_Click(object sender, EventArgs e)
        {
            if ( string.IsNullOrEmpty(txtHR.Text))
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('请输入约见意见！');", true);
                return;
            }
            int haid = int.Parse(Request["haid"]);
            HeadAccountInfo headAccountModel = new HeadAccountManager().GetModel(haid);
            ESP.Finance.Entity.DepartmentViewInfo deptModel = ESP.Finance.BusinessLogic.DepartmentViewManager.GetModel(headAccountModel.GroupId);

            ESP.HumanResource.Entity.HeadAccountLogInfo loghr = new HeadAccountLogInfo();
            loghr.AuditDate = DateTime.Now;
            loghr.Auditor = UserInfo.FullNameCN;
            loghr.AuditorId = UserID;
            loghr.AuditType = (int)ESP.HumanResource.Common.Status.HeadAccountStatus.InterviewHR;
            loghr.HeadAccountId = haid;
            loghr.Remark = this.txtHR.Text;
            loghr.Status = 0;

            ESP.HumanResource.Entity.HeadAccountLogInfo loggroup = null;
            if (!string.IsNullOrEmpty(txtGroup.Text))
            {
                loggroup= new HeadAccountLogInfo();
                loggroup.AuditDate = DateTime.Now;
                loggroup.Auditor = UserInfo.FullNameCN;
                loggroup.AuditorId = UserID;
                loggroup.AuditType = (int)ESP.HumanResource.Common.Status.HeadAccountStatus.InterviewGroup;
                loggroup.HeadAccountId = haid;
                loggroup.Remark = this.txtGroup.Text;
                loggroup.Status = 0;
            }

            headAccountModel.Status = (int)ESP.HumanResource.Common.Status.HeadAccountStatus.InterView;

            ESP.Framework.Entity.OperationAuditManageInfo operation = ESP.Framework.BusinessLogic.OperationAuditManageManager.GetModelByDepId(headAccountModel.GroupId);
//            headAccountModel.InterviewVPId = operation.HeadCountDirectorId;
            headAccountModel.InterviewVPId = operation.HeadCountAuditorId;



            #region 新增员工信息

            #region SEP用户信息新增
            ESP.HumanResource.Entity.UsersInfo user = new ESP.HumanResource.Entity.UsersInfo();
            user.LastNameCN = txtBase_LastNameCn.Text.Trim();
            user.FirstNameCN = txtBase_FirstNameCn.Text.Trim();
            user.CreatedDate = DateTime.Now;
            user.LastActivityDate = DateTime.Now;
            user.Status = ESP.HumanResource.Common.Status.WaitEntry; //待入职
            user.Password = "u7Y3WVwlKK+wXaGfha/V66bbMKI=";
            user.PasswordSalt = "2YCKUH7yKMr1PVNRSDLt5Q==";
            user.IsApproved = false;
            user.IsLockedOut = false;
            #endregion

            #region 人员基本信息
            ESP.HumanResource.Entity.EmployeeBaseInfo model = new ESP.HumanResource.Entity.EmployeeBaseInfo();
            #endregion

            #region 人员入职信息
            ESP.HumanResource.Entity.EmployeeJobInfo jobModel = new ESP.HumanResource.Entity.EmployeeJobInfo();
            #endregion

            #region 人员福利信息
            ESP.HumanResource.Entity.EmployeeWelfareInfo welfareModel = new ESP.HumanResource.Entity.EmployeeWelfareInfo();
            welfareModel.contractStartDate = DateTime.Parse("1900-1-1");
            welfareModel.contractEndDate = DateTime.Parse("1900-1-1");
            welfareModel.probationEndDate = DateTime.Parse("1900-1-1");
            welfareModel.probationPeriodDeadLine = DateTime.Parse("1900-1-1");
            #endregion

            #region 人员部门职务信息
            ESP.HumanResource.Entity.EmployeesInPositionsInfo depsModel = new ESP.HumanResource.Entity.EmployeesInPositionsInfo();
            #endregion

            model.Status = ESP.HumanResource.Common.Status.IsSaved; //ESP.HumanResource.Common.Status.OfferHRAudit;


            initModel(headAccountModel,deptModel, model, jobModel, depsModel);

            #region 日志信息
            ESP.HumanResource.Entity.LogInfo logModel = new ESP.HumanResource.Entity.LogInfo();
            logModel.LogMedifiedTeme = DateTime.Now;
            logModel.Des = "发送Offer Letter[" + txtBase_LastNameCn.Text.Trim() + txtBase_FirstNameCn.Text.Trim() + "]";
            logModel.LogUserId = UserInfo.UserID;
            logModel.LogUserName = UserInfo.Username;
            #endregion

            model.EmployeeWelfareInfo = welfareModel;
            model.EmployeeJobInfo = jobModel;

            string nowBasePay = ""; // 工资
            string nowMeritPay = "";  // 绩效
            if (!string.IsNullOrEmpty(txtNowBasePay.Text))
                nowBasePay = txtNowBasePay.Text.Trim();
            if (!string.IsNullOrEmpty(txtNowMeritPay.Text))
                nowMeritPay = txtNowMeritPay.Text.Trim();
            if (nowBasePay != "")
            {
                model.Pay = Convert.ToDecimal(nowBasePay);
            }
            if (nowMeritPay != "")
            {
                model.Performance = Convert.ToDecimal(nowMeritPay);
            }
            if (!string.IsNullOrEmpty(txtNowAttendance.Text.Trim()))
            {
                model.Attendance = decimal.Parse(txtNowAttendance.Text.Trim());
            }

            #region 入库

            int adduserid = new HeadAccountManager().AuditInterview(headAccountModel, loghr, loggroup,user, model, depsModel, logModel);


            if (adduserid > 0)
            {


                ESP.Framework.Entity.OperationAuditManageInfo depaudit = ESP.Framework.BusinessLogic.OperationAuditManageManager.GetModelByDepId(depsModel.DepartmentID);
                int areaId = deptModel.level1Id;
                int count = (new ESP.Administrative.BusinessLogic.OperationAuditManageManager()).GetList(" userid=" + adduserid).Tables[0].Rows.Count;
                if (count == 0)
                {
                    ESP.Administrative.Entity.OperationAuditManageInfo hraudit = new ESP.Administrative.Entity.OperationAuditManageInfo();
                    hraudit.AreaID = areaId;
                    hraudit.CreateTime = DateTime.Now;
                    hraudit.Deleted = false;
                    hraudit.HRAdminID = depaudit.HRId;
                    hraudit.HRAdminName = depaudit.HRName;
                    hraudit.ManagerID = depaudit.ManagerId;
                    hraudit.ManagerName = depaudit.ManagerName;
                    hraudit.OperatorID = int.Parse(CurrentUser.SysID);
                    hraudit.StatisticianID = depaudit.Hrattendanceid;
                    hraudit.StatisticianName = depaudit.Hrattendancename;
                    hraudit.UserID = adduserid;
                    hraudit.TeamLeaderID = int.Parse(this.hidKaoqin.Value);
                    hraudit.TeamLeaderName = this.txtKaoqin.Text;
                    hraudit.UpdateTime = DateTime.Now;
                    (new ESP.Administrative.BusinessLogic.OperationAuditManageManager()).Add(hraudit);
                }

            }
            else
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('提交失败，请检查信息是否正确！');window.location.href='HeadAccountList.aspx';", true);
                return;
            }


            #endregion

            #endregion



            if (adduserid >0)
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('约见记录填写完毕！');window.location.href='HeadAccountList.aspx';", true);
            }
        }

        protected void btnReturn_Click(object sender, EventArgs e)
        {
            Response.Redirect("HeadAccountList.aspx");
        }

        protected void btnDimission_Click(object sender, EventArgs e)
        {
            if (this.hidDimissionUserId.Value == "")
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('请选择离职/实习人员信息！');", true);
                return;
            }

            int uid =int.Parse(this.hidDimissionUserId.Value);

            ESP.HumanResource.Entity.EmployeeBaseInfo empModel = ESP.HumanResource.BusinessLogic.EmployeeBaseManager.GetModel(uid);
            if (empModel != null)
            {
                ESP.Framework.Entity.UserInfo userModel = ESP.Framework.BusinessLogic.UserManager.Get(empModel.UserID);

                hidDimissionCode.Value = empModel.Code;

                this.txtBase_LastNameCn.Text = userModel.LastNameCN;
                this.txtBase_FirstNameCn.Text = userModel.FirstNameCN;
                this.txtIDCard.Text = empModel.IDNumber.Replace("$","");
               
                this.txtPrivateEmail.Text = empModel.PrivateEmail;
                this.txtMobilePhone.Text = empModel.MobilePhone;
                chkExamen.Checked = empModel.IsExamen;
                this.ddlGender.SelectedValue = empModel.Gender.ToString();
                this.txtBirthday.Text = empModel.Birthday.ToString("yyyy-MM-dd") ;
                ddlMarry.SelectedValue = empModel.MaritalStatus.ToString();
                txtLocation.Text = empModel.BirthPlace;
                txtAddress.Text = empModel.Address;
                txtEducation.Text = empModel.Education;
                
            }

            else
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('未找到该员工详细信息！');", true);
                return;
            }
        }


    }
}
