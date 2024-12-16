using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ESP.HumanResource.BusinessLogic;

public partial class OfferLetterEdit : ESP.Web.UI.PageBase
{
    protected int userid
    {
        get { return ViewState["userid"] == null ? 0 : (int)ViewState["userid"]; }
        set { ViewState["userid"] = value; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        #region AjaxProRegister
        AjaxPro.Utility.RegisterTypeForAjax(typeof(EmployeeBaseManager));
        AjaxPro.Utility.RegisterTypeForAjax(typeof(Employees_EmployeesAllList));
        #endregion
        if (!IsPostBack)
        {
            if (!string.IsNullOrEmpty(Request["userid"]))
            {
                int uid = 0;
                if (int.TryParse(Request["userid"].Trim(), out uid))
                    userid = uid;
                initForm(userid);
            }
            drpWorkCityBind();
            drpUserTypeBind();
        }
    }

    /// <summary>
    /// 绑定工作地点信息
    /// </summary>
    protected void drpWorkCityBind()
    {
        ddltype.DataSource = ESP.Framework.BusinessLogic.DepartmentManager.GetChildren(0).OrderBy(c => c.Ordinal);
        ddltype.DataTextField = "DepartmentName";
        ddltype.DataValueField = "DepartmentID";
        ddltype.DataBind();
        ddltype.Items.Insert(0, new ListItem("请选择...", "-1"));
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

    /// <summary>
    /// Handles the Click event of the btnSave control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    protected void btnSave_Click(object sender, EventArgs e)
    {
        #region 修改员工信息
        if (userid > 0)
        {
            ESP.HumanResource.Entity.EmployeeBaseInfo model = EmployeeBaseManager.GetModel(userid);
            if (model != null)
            {
                try
                {
                    ESP.HumanResource.Entity.UsersInfo user = UsersManager.GetModel(model.UserID);
                    ESP.HumanResource.Entity.EmployeesInPositionsInfo deps = EmployeesInPositionsManager.GetModel(model.UserID, model.EmployeeJobInfo.joinjobID);
                    ESP.HumanResource.Entity.EmployeesInPositionsInfo depsOld = EmployeesInPositionsManager.GetModel(model.UserID, model.EmployeeJobInfo.joinjobID);

                    #region 日志信息
                    ESP.HumanResource.Entity.LogInfo logModel = new ESP.HumanResource.Entity.LogInfo();
                    logModel.LogMedifiedTeme = DateTime.Now;
                    logModel.Des = "修改Offer Letter信息[" + txtBase_LastNameCn.Text.Trim() + txtBase_FirstNameCn.Text.Trim() + "]";
                    logModel.LogUserId = UserInfo.UserID;
                    logModel.LogUserName = UserInfo.Username;
                    #endregion

                    #region 人员入职信息
                    model.EmployeeJobInfo.joinDate = DateTime.Parse(txtJob_JoinDate.Text.Trim());
                    model.EmployeeJobInfo.joinJob = ESP.Framework.BusinessLogic.DepartmentPositionManager.Get(int.Parse(txtJob_JoinJob.Value)).DepartmentPositionName;
                    model.EmployeeJobInfo.joinjobID = int.Parse(txtJob_JoinJob.Value);

                    if (!string.IsNullOrEmpty(txtJob_CompanyName.Text.Trim()) && !string.IsNullOrEmpty(hidCompanyId.Value))
                    {
                        model.EmployeeJobInfo.companyid = int.Parse(hidCompanyId.Value);
                        model.EmployeeJobInfo.companyName = txtJob_CompanyName.Text.Trim();
                    }
                    if (!string.IsNullOrEmpty(txtJob_DepartmentName.Text.Trim()) && !string.IsNullOrEmpty(hidDepartmentID.Value))
                    {
                        model.EmployeeJobInfo.departmentid = int.Parse(hidDepartmentID.Value);
                        model.EmployeeJobInfo.departmentName = txtJob_DepartmentName.Text.Trim();
                    }
                    if (!string.IsNullOrEmpty(txtJob_GroupName.Text.Trim()) && !string.IsNullOrEmpty(hidGroupId.Value))
                    {
                        model.EmployeeJobInfo.groupid = int.Parse(hidGroupId.Value);
                        model.EmployeeJobInfo.groupName = txtJob_GroupName.Text.Trim();
                    }
                    #endregion

                    #region 人员部门职务信息
                    deps.UserID = model.UserID;
                    deps.DepartmentPositionID = int.Parse(txtJob_JoinJob.Value);
                    deps.IsActing = false;
                    deps.IsManager = false;
                    if (!string.IsNullOrEmpty(txtJob_CompanyName.Text.Trim()) && !string.IsNullOrEmpty(hidCompanyId.Value))
                    {
                        deps.DepartmentID = int.Parse(hidCompanyId.Value);
                    }
                    if (!string.IsNullOrEmpty(txtJob_DepartmentName.Text.Trim()) && !string.IsNullOrEmpty(hidDepartmentID.Value))
                    {
                        deps.DepartmentID = int.Parse(hidDepartmentID.Value);
                    }
                    if (!string.IsNullOrEmpty(txtJob_GroupName.Text.Trim()) && !string.IsNullOrEmpty(hidGroupId.Value))
                    {
                        deps.DepartmentID = int.Parse(hidGroupId.Value);
                    }
                    #endregion

                    #region 人员基本信息修改
                    model.LastNameCN = txtBase_LastNameCn.Text.Trim();
                    model.FirstNameCN = txtBase_FirstNameCn.Text.Trim();
                    model.IDNumber = txtIDCard.Text.Trim();
                    model.Memo = txtJob_Memo.Text.Trim();
                    model.PrivateEmail = txtPrivateEmail.Text.Trim();
                    //员工工作地点
                    model.WorkCity = ddltype.SelectedValue;
                    model.TypeID = int.Parse(drpUserType.SelectedValue);
                    model.Status = ESP.HumanResource.Common.Status.IsSaved;
                    //if (chkByoComputer.Checked)  // 是否自带笔记本
                    //    model.OwnedPC = true;
                    //else
                    //    model.OwnedPC = false;
                    model.OfferLetterTemplate = int.Parse(drpOfferTemplate.SelectedValue);  // Offer Letter模板
                    model.LastModifiedTime = DateTime.Now;
                    model.LastModifier = UserID;
                    model.OfferLetterSender = UserID;
                    model.IsExamen = chkExamen.Checked;
                    model.Seniority = string.IsNullOrEmpty(txtSeniority.Text.Trim()) ? 0 : int.Parse(txtSeniority.Text.Trim());
                    model.MobilePhone = txtMobilePhone.Text.Trim();
                    #endregion

                    #region SEP用户信息修改
                    user.LastNameCN = txtBase_LastNameCn.Text.Trim();
                    user.FirstNameCN = txtBase_FirstNameCn.Text.Trim();
                    user.LastActivityDate = DateTime.Now;
                    #endregion
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
                    #region 入库
                    if (EmployeeBaseManager.Update(user, model, deps, depsOld, logModel))
                    {
                        ShowCompleteMessage("修改成功", "OfferLetterList.aspx");
                    }
                    else
                    {
                        ShowCompleteMessage("修改失败", "OfferLetterList.aspx");
                    }
                    #endregion
                }
                catch (Exception ex)
                {
                    ESP.Logging.Logger.Add(ex.ToString());
                    ShowCompleteMessage("修改失败", "OfferLetterList.aspx");
                }
            }
        }
        #endregion
        #region 新增员工信息
        else
        {
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
            model.Status = ESP.HumanResource.Common.Status.IsSaved;
            initModel(model, jobModel, depsModel);

            #region 日志信息
            ESP.HumanResource.Entity.LogInfo logModel = new ESP.HumanResource.Entity.LogInfo();
            logModel.LogMedifiedTeme = DateTime.Now;
            logModel.Des = "新增Offer Letter信息[" + txtBase_LastNameCn.Text.Trim() + txtBase_FirstNameCn.Text.Trim() + "]";
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
            #region 入库
            try
            {
                int adduserid =EmployeeBaseManager.Add(user, model, depsModel, logModel);
                if (adduserid>0)
                {
                    ESP.Framework.Entity.OperationAuditManageInfo depaudit = ESP.Framework.BusinessLogic.OperationAuditManageManager.GetModelByDepId(depsModel.DepartmentID);
                    int areaId = this.GetRootDepartmentID(user.UserID).DepartmentID;
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
                        hraudit.TeamLeaderID = int.Parse(this.hidAuditer.Value);
                        hraudit.TeamLeaderName = this.txtAuditer.Text;
                        hraudit.UpdateTime = DateTime.Now;
                        (new ESP.Administrative.BusinessLogic.OperationAuditManageManager()).Add(hraudit);
                    }

                    ESP.Framework.BusinessLogic.UserManager.ResetPassword(user.Username, "password");
                    ShowCompleteMessage("添加成功", "OfferLetterList.aspx");
                }
                else
                {
                    ShowCompleteMessage("添加失败", "OfferLetterList.aspx");
                }
            }
            catch (Exception ex)
            {
                ESP.Logging.Logger.Add(ex.ToString());
                ShowCompleteMessage("添加失败", "OfferLetterList    .aspx");
            }
            #endregion
        }
        #endregion
    }


    /// <summary>
    /// 获得用户所在公司的ID，如北京的用户登录获得总部的部门ID
    /// </summary>
    /// <param name="UserID">用户编号</param>
    /// <returns>返回部门编号</returns>
    public ESP.Framework.Entity.DepartmentInfo GetRootDepartmentID(int UserID)
    {
        ESP.Framework.Entity.DepartmentInfo model = new ESP.Framework.Entity.DepartmentInfo();
        // 获得当前登录用户的人员部门信息
        IList<ESP.Framework.Entity.EmployeePositionInfo> list = ESP.Framework.BusinessLogic.DepartmentPositionManager.GetEmployeePositions(UserID);

        if (list != null && list.Count > 0)
        {
            ESP.Framework.Entity.EmployeePositionInfo empPosInfo = list[0];
            List<ESP.Framework.Entity.DepartmentInfo> depList = new List<ESP.Framework.Entity.DepartmentInfo>();
            ESP.Framework.Entity.DepartmentInfo departmentInfo = ESP.Framework.BusinessLogic.DepartmentManager.Get(empPosInfo.DepartmentID);
            if (departmentInfo != null && departmentInfo.DepartmentLevel == 3)
            {
                // 添加当前用户上级部门信息
                if (!string.IsNullOrEmpty(departmentInfo.Description))
                {
                    model = ESP.Framework.BusinessLogic.DepartmentManager.Get(int.Parse(departmentInfo.Description));
                }

            }
        }
        return model;
    }


    protected void btnSendOffer_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(hidGroupId.Value))
        {
            ShowCompleteMessage("请选择所属部门！", "OfferLetterList.aspx");
            return;
        }
        if (string.IsNullOrEmpty(txtJob_JoinJob.Value))
        {
            ShowCompleteMessage("请选择职位！", "OfferLetterList.aspx");
            return;
        }
        if (string.IsNullOrEmpty(hidAuditer.Value))
        {
            ShowCompleteMessage("请选择考勤审批人！", "OfferLetterList.aspx");
            return;
        }
       

        #region 修改员工信息
        if (userid > 0)
        {
            ESP.HumanResource.Entity.EmployeeBaseInfo model = EmployeeBaseManager.GetModel(userid);
            if (model != null)
            {
                //try
                //{
                    ESP.HumanResource.Entity.UsersInfo user = UsersManager.GetModel(model.UserID);
                    ESP.HumanResource.Entity.EmployeesInPositionsInfo deps = EmployeesInPositionsManager.GetModel(model.UserID, model.EmployeeJobInfo.joinjobID);
                    ESP.HumanResource.Entity.EmployeesInPositionsInfo depsOld = EmployeesInPositionsManager.GetModel(model.UserID, model.EmployeeJobInfo.joinjobID);

                    #region 日志信息
                    ESP.HumanResource.Entity.LogInfo logModel = new ESP.HumanResource.Entity.LogInfo();
                    logModel.LogMedifiedTeme = DateTime.Now;
                    logModel.Des = "发送Offer Letter[" + txtBase_LastNameCn.Text.Trim() + txtBase_FirstNameCn.Text.Trim() + "]";
                    logModel.LogUserId = UserInfo.UserID;
                    logModel.LogUserName = UserInfo.Username;
                    #endregion

                    #region 人员入职信息
                    model.EmployeeJobInfo.joinDate = DateTime.Parse(txtJob_JoinDate.Text.Trim());
                    model.EmployeeJobInfo.joinJob = ESP.Framework.BusinessLogic.DepartmentPositionManager.Get(int.Parse(txtJob_JoinJob.Value)).DepartmentPositionName;
                    model.EmployeeJobInfo.joinjobID = int.Parse(txtJob_JoinJob.Value);

                    if (!string.IsNullOrEmpty(txtJob_CompanyName.Text.Trim()) && !string.IsNullOrEmpty(hidCompanyId.Value))
                    {
                        model.EmployeeJobInfo.companyid = int.Parse(hidCompanyId.Value);
                        model.EmployeeJobInfo.companyName = txtJob_CompanyName.Text.Trim();
                    }
                    if (!string.IsNullOrEmpty(txtJob_DepartmentName.Text.Trim()) && !string.IsNullOrEmpty(hidDepartmentID.Value))
                    {
                        model.EmployeeJobInfo.departmentid = int.Parse(hidDepartmentID.Value);
                        model.EmployeeJobInfo.departmentName = txtJob_DepartmentName.Text.Trim();
                    }
                    if (!string.IsNullOrEmpty(txtJob_GroupName.Text.Trim()) && !string.IsNullOrEmpty(hidGroupId.Value))
                    {
                        model.EmployeeJobInfo.groupid = int.Parse(hidGroupId.Value);
                        model.EmployeeJobInfo.groupName = txtJob_GroupName.Text.Trim();
                    }
                    #endregion

                    #region 人员部门职务信息
                    deps.UserID = model.UserID;
                    deps.DepartmentPositionID = int.Parse(txtJob_JoinJob.Value);
                    deps.IsActing = false;
                    deps.IsManager = false;
                    if (!string.IsNullOrEmpty(txtJob_CompanyName.Text.Trim()) && !string.IsNullOrEmpty(hidCompanyId.Value))
                    {
                        deps.DepartmentID = int.Parse(hidCompanyId.Value);
                    }
                    if (!string.IsNullOrEmpty(txtJob_DepartmentName.Text.Trim()) && !string.IsNullOrEmpty(hidDepartmentID.Value))
                    {
                        deps.DepartmentID = int.Parse(hidDepartmentID.Value);
                    }
                    if (!string.IsNullOrEmpty(txtJob_GroupName.Text.Trim()) && !string.IsNullOrEmpty(hidGroupId.Value))
                    {
                        deps.DepartmentID = int.Parse(hidGroupId.Value);
                    }
                    #endregion

                    #region 人员基本信息修改
                    model.LastNameCN = txtBase_LastNameCn.Text.Trim();
                    model.FirstNameCN = txtBase_FirstNameCn.Text.Trim();
                    model.IDNumber = txtIDCard.Text.Trim();
                    model.Memo = txtJob_Memo.Text.Trim();
                    model.PrivateEmail = txtPrivateEmail.Text.Trim();
                    //员工工作地点
                    model.WorkCity = ddltype.SelectedValue;
                    model.TypeID = int.Parse(drpUserType.SelectedValue);
                    //如果offer创建人不是rosa，增加财务审批
                    //rosa创建的offer不需要审批
                    if (CurrentUser.SysID == System.Configuration.ConfigurationManager.AppSettings["OfferLetterAudit"])
                    {
                        model.Status = ESP.HumanResource.Common.Status.OfferHRAudit;
                    }
                    else
                    {
                        model.Status = ESP.HumanResource.Common.Status.OfferFinanceAudit;
                    }
                    
                    //if (chkByoComputer.Checked)  // 是否自带笔记本
                    //    model.OwnedPC = true;
                    //else
                    //    model.OwnedPC = false;
                    model.OfferLetterTemplate = int.Parse(drpOfferTemplate.SelectedValue);  // Offer Letter模板
                    model.LastModifiedTime = DateTime.Now;
                    model.LastModifier = UserID;
                    model.OfferLetterSender = UserID;
                    model.OfferLetterSendTime = DateTime.Now;
                    model.IsExamen = chkExamen.Checked;
                    model.Seniority = string.IsNullOrEmpty(txtSeniority.Text.Trim()) ? 0 : int.Parse(txtSeniority.Text.Trim());
                    model.MobilePhone = txtMobilePhone.Text.Trim();
                    #endregion

                    #region SEP用户信息修改
                    user.LastNameCN = txtBase_LastNameCn.Text.Trim();
                    user.FirstNameCN = txtBase_FirstNameCn.Text.Trim();
                    user.LastActivityDate = DateTime.Now;
                    #endregion

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
                    #region 入库
                    if (EmployeeBaseManager.Update(user, model, deps, depsOld, logModel))
                    {
                        ESP.Framework.Entity.OperationAuditManageInfo depaudit = ESP.Framework.BusinessLogic.OperationAuditManageManager.GetModelByDepId(deps.DepartmentID);
                        int areaId = this.GetRootDepartmentID(user.UserID).DepartmentID;
                         int count= (new ESP.Administrative.BusinessLogic.OperationAuditManageManager()).GetList(" userid=" + user.UserID).Tables[0].Rows.Count;
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
                             hraudit.UserID = user.UserID;
                             hraudit.TeamLeaderID = int.Parse(this.hidAuditer.Value);
                             hraudit.TeamLeaderName = this.txtAuditer.Text;
                             hraudit.UpdateTime = DateTime.Now;
                             (new ESP.Administrative.BusinessLogic.OperationAuditManageManager()).Add(hraudit);
                         }

                        string audit = "";
                        int showSalary = 0;
                        if (CurrentUser.SysID == System.Configuration.ConfigurationManager.AppSettings["OfferLetterAudit"])
                        {
                            audit = ESP.Configuration.ConfigurationManager.SafeAppSettings["OfferLetterAudit"];
                            showSalary = 1;
                        }
                        else
                        {
                            audit = ESP.Configuration.ConfigurationManager.SafeAppSettings["OfferLetterFinanceAudit"];
                        }


                        ESP.Framework.Entity.UserInfo userInfo = new ESP.Framework.Entity.UserInfo();
                        if (audit != string.Empty)
                        {
                            userInfo = ESP.Framework.BusinessLogic.UserManager.Get(int.Parse(audit));
                        }
                        if (!string.IsNullOrEmpty(userInfo.Email))
                        {
                            string url = "";
                            if (model.OfferLetterTemplate == 3)
                                url = "http://" + Request.Url.Authority + "/HR/Join/InternAuditOfferLetter.aspx?userid=" + model.UserID + "&nowBasePay=" + model.Pay+"&showSalary="+showSalary.ToString();
                            else
                                url = "http://" + Request.Url.Authority + "/HR/Join/AuditOfferLetter.aspx?userid=" + model.UserID + "&nowBasePay=" + model.Pay + "&nowMeritPay=" + model.Performance + "&showSalary=" + showSalary.ToString();
                            string body = ESP.HumanResource.Common.SendMailHelper.ScreenScrapeHtml(url);
                            ESP.Mail.MailManager.Send("待审核(聘用任职信)", body, true, new MailAddress[] { new MailAddress(userInfo.Email) });
                            ShowCompleteMessage("提交成功，邮件已发送，请等待审核！", "OfferLetterList.aspx");
                        }
                        else
                        {
                            ESP.Logging.Logger.Add("没有填写" + model.FullNameCN + "个人邮箱。");
                            ShowCompleteMessage("提交成功，邮件发送失败！", "OfferLetterList.aspx");
                        }
                    }
                    else
                    {
                        ShowCompleteMessage("提交失败！", "OfferLetterList.aspx");
                    }
                    #endregion
                //}
                //catch (Exception ex)
                //{
                //    ESP.Logging.Logger.Add(ex.ToString());
                //    ShowCompleteMessage("提交失败！", "OfferLetterList.aspx");
                //}
            }
        }
        #endregion
        #region 新增员工信息
        else
        {
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

            //如果offer创建人不是rosa，增加财务审批
            //rosa创建的offer不需要审批
            //if (CurrentUser.SysID == System.Configuration.ConfigurationManager.AppSettings["OfferLetterAudit"])
            //{
            //    model.Status = ESP.HumanResource.Common.Status.OfferHRAudit;
            //}
            //else
            //{
            //    model.Status = ESP.HumanResource.Common.Status.OfferFinanceAudit;
            //}
            model.Status = ESP.HumanResource.Common.Status.IsSendOfferLetter;
                   
            initModel(model, jobModel, depsModel);

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
            //if(!string.IsNullOrEmpty(txtNowAttendance.Text))
            //    model.Attendance = decimal.Parse(txtNowAttendance.Text.Trim());
            #region 入库
            //try
            //{
                int adduserid =EmployeeBaseManager.Add(user, model, depsModel, logModel);
                if (adduserid > 0)
                {
                   // string audit = "";
                    int showSalary = 0;
                    //if (CurrentUser.SysID == System.Configuration.ConfigurationManager.AppSettings["OfferLetterAudit"])
                    //{
                    //    audit = ESP.Configuration.ConfigurationManager.SafeAppSettings["OfferLetterAudit"];
                    //    showSalary = 1;
                    //}
                    //else
                    //{
                    //    audit = ESP.Configuration.ConfigurationManager.SafeAppSettings["OfferLetterFinanceAudit"];
                    //}
                     ESP.Framework.Entity.OperationAuditManageInfo depaudit = ESP.Framework.BusinessLogic.OperationAuditManageManager.GetModelByDepId(depsModel.DepartmentID);
                    int areaId = this.GetRootDepartmentID(user.UserID).DepartmentID;
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
                        hraudit.TeamLeaderID = int.Parse(this.hidAuditer.Value);
                        hraudit.TeamLeaderName = this.txtAuditer.Text;
                        hraudit.UpdateTime = DateTime.Now;
                        (new ESP.Administrative.BusinessLogic.OperationAuditManageManager()).Add(hraudit);
                    }
                   

                    EmployeeBaseManager.Update(model);

                    if (!string.IsNullOrEmpty(model.PrivateEmail))
                    {
                        string url = "";
                        //if (model.OfferLetterTemplate == 3)
                        //    url = "http://" + Request.Url.Authority + "/HR/Join/InternOfferLetter.aspx?userid=" + model.UserID + "&nowBasePay=" + model.Pay.ToString("#,##0.00") + "&nowMeritPay=" + model.Performance.ToString("#,##0.00") + "&showSalary=1";
                        //else if (model.OfferLetterTemplate == 4)
                        //    url = "http://" + Request.Url.Authority + "/HR/Join/SalesOfferLetter.aspx?userid=" + model.UserID + "&nowBasePay=" + model.Pay.ToString("#,##0.00") + "&nowMeritPay=" + model.Performance.ToString("#,##0.00") + "&showSalary=1";
                        //else if (model.OfferLetterTemplate == 5)
                        //    url = "http://" + Request.Url.Authority + "/HR/Join/OfferLetter12.aspx?userid=" + model.UserID + "&nowBasePay=" + model.Pay.ToString("#,##0.00") + "&nowMeritPay=" + model.Performance.ToString("#,##0.00") + "&showSalary=1";
                        //else if (model.OfferLetterTemplate == 6)
                        //    url = "http://" + Request.Url.Authority + "/HR/Join/SalesOfferLetter.aspx?userid=" + model.UserID + "&nowBasePay=" + model.Pay.ToString("#,##0.00") + "&nowMeritPay=" + model.Performance.ToString("#,##0.00") + "&showSalary=1";
                        //else
                        url = "http://" + Request.Url.Authority + "/HR/Join/OfferLetter.aspx?userid=" + model.UserID + "&nowBasePay=" + model.Pay.ToString("#,##0.00") + "&nowMeritPay=" + model.Performance.ToString("#,##0.00") + "&nowAttendance=" + model.Attendance.ToString("#,##0.00") + "&showSalary=1";
                        string body = ESP.HumanResource.Common.SendMailHelper.ScreenScrapeHtml(url);
                        ESP.Mail.MailManager.Send("聘用任职信", body, true, new MailAddress[] { new MailAddress(model.PrivateEmail),new MailAddress(CurrentUser.EMail) });
                        ShowCompleteMessage("聘用任职信发送成功！", "OfferLetterAuditList.aspx");
                    }
                    else
                    {
                        ShowCompleteMessage("聘用任职信发送失败！", "OfferLetterAuditList.aspx");
                    }
                }
                else
                {
                    ClientScript.RegisterStartupScript(typeof(string), "", "alert('提交失败，请检查身份证是否错误或重复!');", true);
                    return;    
                }
            //}
            //catch (Exception ex)
            //{
            //    ESP.Logging.Logger.Add(ex.ToString());
            //    ShowCompleteMessage("提交失败！", "OfferLetterList.aspx");
            //}
            #endregion
        }
        #endregion
    }

    #region 根据页面控件 初始化Model
    /// <summary>
    /// Inits the model.
    /// </summary>
    /// <param name="model">The model.</param>
    /// <param name="jobModel">The job model.</param>
    /// <param name="depsModel">The deps model.</param>
    protected void initModel(ESP.HumanResource.Entity.EmployeeBaseInfo model, ESP.HumanResource.Entity.EmployeeJobInfo jobModel, ESP.HumanResource.Entity.EmployeesInPositionsInfo depsModel)
    {
        model.LastNameCN = txtBase_LastNameCn.Text.Trim();
        model.FirstNameCN = txtBase_FirstNameCn.Text.Trim();
        model.Memo = txtJob_Memo.Text.Trim();
        model.IDNumber = txtIDCard.Text.Trim();
        model.PrivateEmail = txtPrivateEmail.Text.Trim();
        //员工工作地点
        model.WorkCity = ddltype.SelectedValue;
        model.TypeID = int.Parse(drpUserType.SelectedValue);
        //if (chkByoComputer.Checked)  // 是否自带笔记本
        //    model.OwnedPC = true;
        //else
        //    model.OwnedPC = false;
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

        try
        {
            jobModel.joinDate = DateTime.Parse(txtJob_JoinDate.Text.Trim());
        }
        catch { }
        jobModel.joinJob = ESP.Framework.BusinessLogic.DepartmentPositionManager.Get(int.Parse(txtJob_JoinJob.Value)).DepartmentPositionName;
        jobModel.joinjobID = int.Parse(txtJob_JoinJob.Value);


        if (!string.IsNullOrEmpty(txtJob_CompanyName.Text.Trim()) && !string.IsNullOrEmpty(hidCompanyId.Value))
        {
            depsModel.DepartmentID = int.Parse(hidCompanyId.Value);

            jobModel.companyid = int.Parse(hidCompanyId.Value);
            jobModel.companyName = txtJob_CompanyName.Text.Trim();
        }
        if (!string.IsNullOrEmpty(txtJob_DepartmentName.Text.Trim()) && !string.IsNullOrEmpty(hidDepartmentID.Value))
        {
            depsModel.DepartmentID = int.Parse(hidDepartmentID.Value);

            jobModel.departmentid = int.Parse(hidDepartmentID.Value);
            jobModel.departmentName = txtJob_DepartmentName.Text.Trim();
        }
        if (!string.IsNullOrEmpty(txtJob_GroupName.Text.Trim()) && !string.IsNullOrEmpty(hidGroupId.Value))
        {
            depsModel.DepartmentID = int.Parse(hidGroupId.Value);

            jobModel.groupid = int.Parse(hidGroupId.Value);
            jobModel.groupName = txtJob_GroupName.Text.Trim();
        }
        depsModel.DepartmentPositionID = int.Parse(txtJob_JoinJob.Value);
        depsModel.IsActing = false;
        depsModel.IsManager = false;
        depsModel.BeginDate = jobModel.joinDate;
    }
    #endregion

    #region 根据Model对象，初始化页面控件
    /// <summary>
    /// Inits the form.
    /// </summary>
    /// <param name="sysid">The sysid.</param>
    protected void initForm(int sysid)
    {
        try
        {
            ESP.HumanResource.Entity.EmployeeBaseInfo model = EmployeeBaseManager.GetModel(sysid);
            ESP.HumanResource.Entity.UsersInfo user = UsersManager.GetModel(sysid);
            ESP.Administrative.Entity.OperationAuditManageInfo audit =(new ESP.Administrative.BusinessLogic.OperationAuditManageManager()).GetOperationAuditModelByUserID(user.UserID);
            ESP.HumanResource.Entity.EmployeesInPositionsInfo position = ESP.HumanResource.BusinessLogic.EmployeesInPositionsManager.GetModel(user.UserID);
            txtBase_LastNameCn.Text = user.LastNameCN;
            txtBase_FirstNameCn.Text = user.FirstNameCN;
            txtPrivateEmail.Text = model.PrivateEmail;
            txtJob_JoinDate.Text = model.EmployeeJobInfo.joinDate.ToString("yyyy-MM-dd");
            txtJob_CompanyName.Text = model.EmployeeJobInfo.companyName;
            hidCompanyId.Value = model.EmployeeJobInfo.companyid.ToString();
            txtMobilePhone.Text = model.MobilePhone;

            txtJob_DepartmentName.Text = model.EmployeeJobInfo.departmentName;
            hidDepartmentID.Value = model.EmployeeJobInfo.departmentid.ToString();

            txtJob_GroupName.Text = model.EmployeeJobInfo.groupName;
            hidGroupId.Value = model.EmployeeJobInfo.groupid.ToString();
            txtJob_Memo.Text = model.Memo;
            txtIDCard.Text = model.IDNumber;
            txtNowBasePay.Text = model.Pay.ToString();
            txtNowMeritPay.Text = model.Performance.ToString();

            if(audit!=null)
            {
              this.txtAuditer.Text=audit.TeamLeaderName;
                this.hidAuditer.Value=audit.TeamLeaderID.ToString();
            }
            if (position != null)
            {
                this.txtPosition.Text = position.DepartmentPositionName;
                this.txtJob_JoinJob.Value = position.DepartmentPositionID.ToString();
            }

            List<ESP.HumanResource.Entity.EmployeesInPositionsInfo> eiplist = ESP.HumanResource.BusinessLogic.EmployeesInPositionsManager.GetModelList(" a.userid=" + model.UserID);
            ESP.HumanResource.Entity.EmployeesInPositionsInfo deps = eiplist[0];

            txtJob_JoinJob.Value = deps.DepartmentPositionID.ToString();

            if (!string.IsNullOrEmpty(model.WorkCity) || !string.IsNullOrEmpty(model.WorkCountry))
            {
                ddltype.SelectedValue = model.WorkCity;
            }

            //if (model.OwnedPC)  // 是否自带笔记本
            //    chkByoComputer.Checked = true;
            //else
            //    chkByoComputer.Checked = false;
            drpOfferTemplate.SelectedValue = model.OfferLetterTemplate.ToString();  // Offer Letter模板
            chkExamen.Checked = model.IsExamen;   // 是否是应届毕业生。
            txtSeniority.Text = model.Seniority.ToString();
        }
        catch (Exception ex)
        {
            ESP.Logging.Logger.Add(ex.ToString());
        }
    }

    [AjaxPro.AjaxMethod]
    public static List<List<string>> getalist(int parentId)
    {
        List<List<string>> list = new List<List<string>>();
        try
        {
            list = ESP.Compatible.DepartmentManager.GetListForAJAX(parentId);
        }
        catch (Exception e)
        {
            e.ToString();
        }

        List<string> c = new List<string>();
        c.Add("-1");
        c.Add("请选择...");
        list.Insert(0, c);
        return list;
    }
    #endregion
}