using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using ESP.HumanResource.BusinessLogic;
using System.Net.Mail;
using ESP.HumanResource.Common;
using ESP.HumanResource.Entity;
using ESP.Framework.DataAccess.Utilities;
using ESP.HumanResource.WebPages;
using System.Linq;
using System.Web.Security;

public partial class Employees_EmployeeReadyEdit : ESP.Web.UI.PageBase
{
    private string userid = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        #region AjaxProRegister
        AjaxPro.Utility.RegisterTypeForAjax(typeof(EmployeeBaseManager));
        AjaxPro.Utility.RegisterTypeForAjax(typeof(Employees_EmployeesAllList));
        #endregion

       
        if (!IsPostBack)
        {
            drpUserTypeBind();
            drpJobBind();
            //drpWorkCityBind();

            labResume.Visible = false;
            if (!string.IsNullOrEmpty(Request["userid"]))
            {
                userid = Request["userid"].Trim();
                initForm(int.Parse(Request["userid"].Trim().ToString()));
            }
            // chkSendMail.Checked = true;
           
        }
    }

    protected void btnReturn_Click(object sender, EventArgs e)
    {
        Response.Redirect("NewEmployessList.aspx");
    }

    protected void btnTel_Click(object sender, EventArgs e)
    {

    }

    protected void drpUserTypeBind()
    {
        drpUserType.DataSource = ESP.Framework.BusinessLogic.EmployeeManager.GetTypes();
        drpUserType.DataTextField = "value";
        drpUserType.DataValueField = "key";
        drpUserType.DataBind();
    }

    protected void drpJobBind()
    {
        //drpJob_JoinJob.Items.Insert(0, new ListItem("请选择...", "-1"));
    }

    /// <summary>
    /// Handles the Click event of the btnSave control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(Request["userid"]))
        {
            ESP.HumanResource.Entity.EmployeeBaseInfo model = EmployeeBaseManager.GetModel(int.Parse(Request["userid"].Trim().ToString()));
            if (model != null)
            {
                if (EmployeeBaseManager.checkUserCodeExists(txtItCode.Text.Trim(), model.UserID))
                {

                    ShowCompleteMessage("登录名已存在,请重新输入", "EmployeeReadyEdit.aspx?userid=" + model.UserID);
                }
                else
                {
                    ESP.HumanResource.Entity.UsersInfo user = UsersManager.GetModel(model.UserID);
                    ESP.HumanResource.Entity.EmployeesInPositionsInfo deps = EmployeesInPositionsManager.GetModel(model.UserID);

                    ESP.Framework.Entity.DepartmentInfo deptView =ESP.Framework.BusinessLogic.DepartmentManager.Get(deps.DepartmentID);



                    //ESP.HumanResource.Entity.EmployeesInPositionsInfo depsOld = EmployeesInPositionsManager.GetModel(model.UserID, model.EmployeeJobInfo.joinjobID);
                    #region 日志信息
                    ESP.HumanResource.Entity.LogInfo logModel = new ESP.HumanResource.Entity.LogInfo();
                    logModel.LogMedifiedTeme = DateTime.Now;
                    logModel.Des = CurrentUser.Name + "修改了待入职人员信息[" + txtBase_LastNameCn.Text.Trim() + txtBase_FirstNameCn.Text.Trim() + txtIDCard.Text + "]";
                    logModel.LogUserId = UserInfo.UserID;
                    logModel.LogUserName = UserInfo.Username;
                    #endregion

                    #region 人员入职信息
                    try
                    {
                        model.EmployeeJobInfo.joinDate = DateTime.Parse(txtJob_JoinDate.Text.Trim());
                        model.JoinDate = DateTime.Parse(txtJob_JoinDate.Text.Trim());
                    }
                    catch { }

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

                    //model.EmployeeJobInfo.selfIntroduction = txtJob_SelfIntroduction.Text.Trim();
                    //model.EmployeeJobInfo.objective = txtJob_Objective.Text.Trim();
                    //model.EmployeeJobInfo.workingExperience = txtJob_WorkingExperience.Text.Trim();
                    //model.EmployeeJobInfo.educationalBackground = txtJob_EducationalBackground.Text.Trim();
                    //model.EmployeeJobInfo.languagesAndDialect = txtJob_LanguagesAndDialect.Text.Trim();
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
                    model.FirstNameEN = txtBase_FirstNameEn.Text.Trim();
                    model.LastNameEN = txtBase_LastNameEn.Text.Trim();
                    model.Gender = int.Parse(txtBase_Sex.SelectedValue);
                    model.Phone2 = txtTel.Text.Trim();
                    model.CommonName = txtCommonName.Text.Trim();

                    model.ContractBeginDate = new DateTime(1900,1,1);
                    model.ContractEndDate = new DateTime(1900, 1, 1);
                    model.ContractSignDate = new DateTime(1900, 1, 1);
                    model.WorkBegin = new DateTime(1900, 1, 1);


                    //if (deptView.Description== "19" &&  string.IsNullOrEmpty(model.Phone1))
                    //{
                    //    var tellist = ESP.HumanResource.BusinessLogic.TelManager.GetModelList("");
                    //    ESP.HumanResource.Entity.TELInfo tel = null;
                    //    if (tellist != null && tellist.Count > 0)
                    //    {
                    //        model.Phone1 = tellist[0].Tel;
                    //        tel = tellist[0];

                    //        tel.Status = 0;
                    //        ESP.HumanResource.BusinessLogic.TelManager.Update(tel);

                    //    }
                        

                    //}
                    //else
                    //{
                    //    if (string.IsNullOrEmpty(txtTelPhone.Text.Trim()))
                    //    {
                    //        ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('请输入分机号！');", true);
                    //        return; 
                    //    }
                        model.Phone1 = txtTelPhone.Text.Trim();
                    //}
                   
                    model.IDNumber = txtIDCard.Text.Trim();

                    if ((string.IsNullOrEmpty(txtUserId.Text.Trim()) || txtUserId.Text.IndexOf("I") >= 0) && model.TypeID==1)
                        model.Code = ESP.Framework.BusinessLogic.EmployeeManager.CreateEmployeeCode();
                    else
                        model.Code = txtUserId.Text.Trim();

                    model.FirstNameEN = txtBase_FirstNameEn.Text.Trim();
                    model.LastNameEN = txtBase_LastNameEn.Text.Trim();
                    if (fileCV.HasFile)
                    {
                        string filename = SaveFile(model.Code);
                        model.Resume = filename;
                    }
                    model.Memo = txtJob_Memo.Text.Trim();
                    model.TypeID = int.Parse(drpUserType.SelectedValue);
                    model.TypeName = drpUserType.SelectedItem.Text;
                    //model.IsForeign = chkForeign.Checked;
                    //model.IsCertification = chkCertification.Checked;
                    model.WageMonths = int.Parse(drpWageMonths.SelectedValue.Trim());

                    model.IDNumber = txtIDCard.Text.Trim();

                    //员工工作地点
                    model.WorkCity = ddltype.SelectedValue;
                    // model.WorkCity = hidtype.Value.Trim();
                    //暂时将此字段用于员工异地工作部门
                    //  model.WorkCountry = hidtype1.Value.Trim();
                    //暂时将此字段用于员工异地工作组别
                    //  model.WorkAddress = hidtype2.Value.Trim();
                    model.IsExamen = chkExamen.Checked;  // 是否应届毕业生。
                    if (chkByoComputer.Checked)  // 是否自带笔记本
                        model.OwnedPC = true;
                    else
                        model.OwnedPC = false;
                    model.Seniority = string.IsNullOrEmpty(txtSeniority.Text.Trim()) ? 0 : int.Parse(txtSeniority.Text.Trim());  // 员工社会工龄
                    model.InternalEmail = txtEmail.Text.Trim();

                    #endregion

                    #region SEP用户信息修改

                    user.LastNameCN = txtBase_LastNameCn.Text.Trim();
                    user.FirstNameCN = txtBase_FirstNameCn.Text.Trim();
                    user.LastNameEN = txtBase_LastNameEn.Text.Trim();
                    user.FirstNameEN = txtBase_FirstNameEn.Text.Trim();

                    user.Email = txtEmail.Text.Trim();
                    user.LastActivityDate = DateTime.Now;

                    #endregion

                    #region 入库
                    deps.BeginDate = DateTime.Now;
                        if (EmployeeBaseManager.Update(user, model, deps, deps, logModel))
                        {
                            ShowCompleteMessage("修改成功!", "NewEmployessList.aspx");
                        }
                        else
                        {
                            ShowCompleteMessage("修改失败!", "NewEmployessList.aspx");
                        }

                    #endregion
                }
            }
        }
        else
        {
            if (EmployeeBaseManager.checkUserCodeExists(txtItCode.Text.Trim()))
            {
                ShowCompleteMessage("登录名已存在,请重新输入", "EmployeeReadyEdit.aspx");
            }
            else
            {
                #region SEP用户信息新增
                ESP.HumanResource.Entity.UsersInfo user = new ESP.HumanResource.Entity.UsersInfo();

                user.Username = txtItCode.Text.Trim();
                user.LastNameCN = txtBase_LastNameCn.Text.Trim();
                user.FirstNameCN = txtBase_FirstNameCn.Text.Trim();
                user.LastNameEN = txtBase_LastNameEn.Text.Trim();
                user.FirstNameEN = txtBase_FirstNameEn.Text.Trim();
                user.Email = txtEmail.Text.Trim();

                user.CreatedDate = DateTime.Now;
                user.LastActivityDate = DateTime.Now;

                user.Status = ESP.HumanResource.Common.Status.WaitEntry; //待入职
                user.Password = "password";
                user.PasswordSalt = "password";

                user.IsApproved = false;
                user.IsLockedOut = false;
                //特殊人员不使用系统
                if (drpUserType.SelectedItem.Value == "5")
                {
                    user.IsDeleted = true;
                }
                else
                {
                    user.IsDeleted = false;
                }

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

                #region 日志信息
                ESP.HumanResource.Entity.LogInfo logModel = new ESP.HumanResource.Entity.LogInfo();
                logModel.LogMedifiedTeme = DateTime.Now;
                logModel.Des = CurrentUser.Name + "新增了待入职人员信息[" + txtBase_LastNameCn.Text.Trim() + txtBase_FirstNameCn.Text.Trim() + txtIDCard.Text + "]";
                logModel.LogUserId = UserInfo.UserID;
                logModel.LogUserName = UserInfo.Username;
                #endregion

                initModel(model, jobModel, depsModel);

                model.EmployeeWelfareInfo = welfareModel;
                model.EmployeeJobInfo = jobModel;
                //model.Phone1 = txtTelPhone.Text.Trim();
                model.IDNumber = txtIDCard.Text.Trim();
                //员工工作地点
                model.WorkCity = ddltype.SelectedValue;
                //暂时将此字段用于员工异地工作部门
                //  model.WorkCountry = hidtype1.Value.Trim();
                //暂时将此字段用于员工异地工作组别
                //   model.WorkAddress = hidtype2.Value.Trim();

                #region 入库
                try
                {
                    if (EmployeeBaseManager.Add(user, model, depsModel, logModel) > 0)
                    {
                        try
                        {
                            ESP.Framework.BusinessLogic.UserManager.ResetPassword(user.Username, "password");
                        }
                        catch (System.Exception)
                        {

                        }
                        //只为北京员工的发信
                        //if (model.EmployeeJobInfo.companyid == 19)
                        //{
                        //    if (chkSendMail.Checked)
                        //    {
                        //        try
                        //        {
                        //            string sendmailer = "";
                        //            SendMail(ESP.Framework.BusinessLogic.UserManager.Get(user.Username).UserID,ref sendmailer);
                        //            ShowCompleteMessage("添加成功,已向"+sendmailer+"发送邮件", "NewEmployessList.aspx");
                        //        }
                        //        catch { ShowCompleteMessage("添加成功,向辅助人员发送邮件失败", "NewEmployessList.aspx"); }
                        //    }
                        //    else
                        //    {
                        //        ShowCompleteMessage("添加成功", "NewEmployessList.aspx"); 
                        //    }
                        //}
                        //else
                        //{
                        //    ShowCompleteMessage("添加成功", "NewEmployessList.aspx");
                        //}
                        ShowCompleteMessage("添加成功", "NewEmployessList.aspx");
                    }
                    else
                    {
                        ShowCompleteMessage("添加失败", "NewEmployessList.aspx");
                    }
                }
                catch (Exception ex)
                {
                    ESP.Logging.Logger.Add(ex.ToString());
                    ShowCompleteMessage("添加失败", "NewEmployessList.aspx");
                }
                #endregion
            }
        }
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
        model.Gender = int.Parse(txtBase_Sex.SelectedValue);
        model.Phone2 = txtTel.Text.Trim();
        model.Phone1 = "---";
        model.CommonName = txtCommonName.Text.Trim();
        model.Username = txtItCode.Text.Trim();

        model.Code = txtUserId.Text.Trim();
        model.FirstNameEN = txtBase_FirstNameEn.Text.Trim();
        model.LastNameEN = txtBase_LastNameEn.Text.Trim();
        if (fileCV.HasFile)
        {
            string filename = SaveFile(model.Code);
            model.Resume = filename;
        }
        model.Memo = txtJob_Memo.Text.Trim();
        model.TypeID = int.Parse(drpUserType.SelectedValue);
        model.TypeName = drpUserType.SelectedItem.Text;
        //model.IsForeign = chkForeign.Checked;
        //model.IsCertification = chkCertification.Checked;
        model.WageMonths = int.Parse(drpWageMonths.SelectedValue.Trim());

        model.IDNumber = txtIDCard.Text.Trim();
        model.IsExamen = chkExamen.Checked;  // 是否应届毕业生。
        if (chkByoComputer.Checked)  // 是否自带笔记本
            model.OwnedPC = true;
        else
            model.OwnedPC = false;
        model.Seniority = string.IsNullOrEmpty(txtSeniority.Text.Trim()) ? 0 : int.Parse(txtSeniority.Text.Trim());
        try
        {
            jobModel.joinDate = DateTime.Parse(txtJob_JoinDate.Text.Trim());
            model.JoinDate = jobModel.joinDate;
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
        ESP.HumanResource.Entity.EmployeeBaseInfo model = EmployeeBaseManager.GetModel(sysid);
        EmployeesInPositionsInfo positionModel = ESP.HumanResource.BusinessLogic.EmployeesInPositionsManager.GetModel(sysid);
        ESP.Framework.Entity.DepartmentInfo deptView = ESP.Framework.BusinessLogic.DepartmentManager.Get(positionModel.DepartmentID);

        ESP.HumanResource.Entity.UsersInfo user = UsersManager.GetModel(sysid);

        txtBase_LastNameCn.Text = user.LastNameCN;
        txtBase_FirstNameCn.Text = user.FirstNameCN;
        txtBase_FirstNameEn.Text = user.FirstNameEN;
        txtBase_LastNameEn.Text = user.LastNameEN;
        if (!string.IsNullOrEmpty(model.Resume))
        {
            labResume.Text = "<a target='_blank' href='" + model.Resume + "'><img src='/Images/dc.gif' border='0' /></a>";
            labResume.Visible = true;
        }


        txtBase_Sex.SelectedValue = model.Gender.ToString();
      

        txtItCode.Text = user.Username;
        txtTel.Text = model.Phone2;
        txtEmail.Text = user.Email;
        txtCommonName.Text = model.CommonName;
        txtUserId.Text = model.Code;

       

        chkExamen.Checked = model.IsExamen;
        try
        {
            drpUserType.SelectedValue = model.TypeID.ToString();
        }
        catch { }

        try
        {
            txtJob_JoinDate.Text = model.EmployeeJobInfo.joinDate.ToString("yyyy-MM-dd");
        }
        catch { }

        txtJob_CompanyName.Text = model.EmployeeJobInfo.companyName;
        hidCompanyId.Value = model.EmployeeJobInfo.companyid.ToString();

        txtJob_DepartmentName.Text = model.EmployeeJobInfo.departmentName;
        hidDepartmentID.Value = model.EmployeeJobInfo.departmentid.ToString();

        txtJob_GroupName.Text = model.EmployeeJobInfo.groupName;
        hidGroupId.Value = model.EmployeeJobInfo.groupid.ToString();

        txtJob_Memo.Text = model.Memo;
        txtIDCard.Text = model.IDNumber;
        if (model.OwnedPC)  // 是否自带笔记本
            chkByoComputer.Checked = true;
        else
            chkByoComputer.Checked = false;
        txtSeniority.Text = model.Seniority.ToString();

        List<ESP.HumanResource.Entity.EmployeesInPositionsInfo> eiplist = ESP.HumanResource.BusinessLogic.EmployeesInPositionsManager.GetModelList(" a.userid=" + model.UserID);
        ESP.HumanResource.Entity.EmployeesInPositionsInfo deps = eiplist[0];

        txtJob_JoinJob.Value = deps.DepartmentPositionID.ToString();
        txtPosition.Text = deps.DepartmentPositionName;

        if (model.WageMonths == 12 || model.WageMonths == 13)
        {
            drpWageMonths.SelectedValue = model.WageMonths.ToString();
        }

        try
        {
            if (!string.IsNullOrEmpty(model.WorkCity) || !string.IsNullOrEmpty(model.WorkCountry))
            {
                ddltype.SelectedValue = model.WorkCity;
            }
        }
        catch { }
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

    #region 发送邮件

    //private void SendMail(int sysid,ref string sendmailer)
    //{      

    //    try
    //    {

    //        string recipientAddress = "";

    //        List<ESP.HumanResource.Entity.UsersInfo> list = UsersManager.GetUserList();

    //        foreach(ESP.HumanResource.Entity.UsersInfo info in list)
    //        {
    //            if (!string.IsNullOrEmpty(info.Email.Trim()))
    //            {
    //                recipientAddress += info.Email.Trim() + " ,";
    //                sendmailer += info.LastNameCN + info.FirstNameCN + ",";
    //            }
    //        }

    //        recipientAddress += GetInformationMail(ref sendmailer);
    //        recipientAddress += GetDepartmentAdminMail(ref sendmailer);

    //        if (recipientAddress.Trim().Length > 1)
    //        {
    //            recipientAddress = recipientAddress.Substring(0, recipientAddress.Length - 1);
    //        }
    //        if (sendmailer.Trim().Length > 1)
    //        {
    //            sendmailer = sendmailer.Substring(0, sendmailer.Length - 1);
    //        }

    //        string url = "http://" + Request.Url.Authority + "/HR/Print/NewEmployeeMail.aspx?userid=" + sysid.ToString();

    //       string body = ESP.HumanResource.Common.SendMailHelper.ScreenScrapeHtml(url);

    //       SendMailHelper.SendMail("新员工入职通知", recipientAddress, body, null);

    //  //     SendMailHelper.SendMail("新员工入职通知", "richchow@126.com", body, null);
    //    }
    //    catch (Exception ex) { ESP.Logging.Logger.Add(ex.ToString()); }

    //}
    //#endregion
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

    //#region 获得楼层前台的邮箱
    //private string GetInformationMail(ref string sendmailer)
    //{
    //    string mail = "";
    //    try
    //    {
    //        int groupid = 0;
    //        if (!string.IsNullOrEmpty(txtJob_GroupName.Text.Trim()) && !string.IsNullOrEmpty(hidGroupId.Value))
    //        {
    //            groupid = int.Parse(hidGroupId.Value);
    //        }

    //        List<ESP.HumanResource.Entity.UsersInfo> userlist = UsersManager.GetUserListByGroupID(groupid);

    //        foreach (ESP.HumanResource.Entity.UsersInfo info in userlist)
    //        {
    //            if (!string.IsNullOrEmpty(info.Email.Trim()))
    //            {
    //                mail += info.Email.Trim() + " ,";
    //                sendmailer += info.LastNameCN + info.FirstNameCN + ",";
    //            }
    //        }
    //    }
    //    catch{ }
    //    return mail;
    //}
    //#endregion 

    //#region 获得团队Admin的邮箱
    //private string GetDepartmentAdminMail(ref string sendmailer)
    //{
    //    string mail = "";
    //    try
    //    {
    //        int departmentid = 0;
    //        if (!string.IsNullOrEmpty(txtJob_DepartmentName.Text.Trim()) && !string.IsNullOrEmpty(hidDepartmentID.Value))
    //        {
    //            departmentid = int.Parse(hidDepartmentID.Value);
    //        }

    //        List<ESP.HumanResource.Entity.UsersInfo> userlist = UsersManager.GetUserListByDepartmentID(departmentid);

    //        foreach (ESP.HumanResource.Entity.UsersInfo info in userlist)
    //        {
    //            if (!string.IsNullOrEmpty(info.Email.Trim()))
    //            {
    //                mail += info.Email.Trim() + " ,";
    //                sendmailer += info.LastNameCN + info.FirstNameCN + ",";
    //            }
    //        }
    //    }
    //    catch { }
    //    return mail;

    //}
    #endregion

}
