using System;
using System.Collections.Generic;
using ESP.HumanResource.BusinessLogic;
using ESP.HumanResource.Common;
using System.Data;
using System.Net.Mail;
using ESP.Framework.BusinessLogic;
using System.Data.SqlClient;

public partial class DimissionFormEdit : ESP.Web.UI.PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            InitPage();
        }
    }

    /// <summary>
    /// 初始化页面信息
    /// </summary>
    private void InitPage()
    {
        ESP.HumanResource.Entity.DimissionFormInfo dimissionFormInfo = ESP.HumanResource.BusinessLogic.DimissionFormManager.GetModelByUserid(UserID);  // 获得当前登录用户的离职单信息
        // 判断离职单信息是否不为空
        if (dimissionFormInfo != null)
        {
            // 判断如果不未提交或审批驳回状态，将进入查看页
            if (dimissionFormInfo.Status != (int)ESP.HumanResource.Common.DimissionFormStatus.NotSubmit
                && dimissionFormInfo.Status != (int)ESP.HumanResource.Common.DimissionFormStatus.Overrule )
                Response.Redirect("DimissionFormView.aspx");

            hidDimissionFormID.Value = dimissionFormInfo.DimissionId.ToString();
            txtdimissionDate.Text = dimissionFormInfo.HopeLastDay == null ? "" : dimissionFormInfo.HopeLastDay.Value.ToString("yyyy-MM-dd");
            txtdimissionCause.Text = dimissionFormInfo.Reason;
            txtMobilePhone.Text = dimissionFormInfo.MobilePhone;
            txtEmail.Text = dimissionFormInfo.PrivateMail;
        }

        List<ESP.HumanResource.Entity.EmployeesInPositionsInfo> departments = EmployeesInPositionsManager.GetModelList(" a.UserID=" + UserID);  // 用户部门信息
        ESP.HumanResource.Entity.EmployeeBaseInfo employeeModel = EmployeeBaseManager.GetModel(UserID);  // 用户基本信息

        labCode.Text = employeeModel.Code;
        txtPhone.Text = employeeModel.Phone1;
        txtMobilePhone.Text = employeeModel.MobilePhone;

        if (departments != null && departments.Count > 0)
        {
            List<ESP.Framework.Entity.DepartmentInfo> deps = new List<ESP.Framework.Entity.DepartmentInfo>();
            ESP.Framework.BusinessLogic.DepartmentManager.GetDepartmentListByID(departments[0].GroupID, deps);
            if (deps.Count == 3)
            {
                foreach (ESP.Framework.Entity.DepartmentInfo department in deps)
                {
                    if (department.DepartmentLevel == 1)
                    {
                        txtcompanyName.Text = department.DepartmentName;
                        hidcompanyId.Value = department.DepartmentID.ToString();
                    }
                    else if (department.DepartmentLevel == 2)
                    {
                        txtdepartmentName.Text = department.DepartmentName;
                        hiddepartmentId.Value = department.DepartmentID.ToString();
                    }
                    else if (department.DepartmentLevel == 3)
                    {
                        txtgroupName.Text = department.DepartmentName;
                        hidgroupId.Value = department.DepartmentID.ToString();
                    }
                }
            }
            else if (deps.Count == 2)
            {
                foreach (ESP.Framework.Entity.DepartmentInfo department in deps)
                {
                    if (department.DepartmentLevel == 2)
                    {
                        txtdepartmentName.Text = department.DepartmentName;
                        hiddepartmentId.Value = department.DepartmentID.ToString();
                    }
                }
            }
            txtPosition.Text = departments[0].DepartmentPositionName;
        }

        //入职日期
        ESP.HumanResource.Entity.EmployeeJobInfo employeeJobModel = EmployeeJobManager.getModelBySysId(UserID);
        txtjoinJobDate.Text = employeeModel == null ? "1900-01-01" : employeeJobModel.joinDate.ToString("yyyy-MM-dd");

        ESP.Framework.Entity.UserInfo userinfo = ESP.Framework.BusinessLogic.UserManager.Get(UserID);
        this.txtuserName.Text = userinfo.FullNameCN;


            pnlCash.Visible = false;

        string terms = " usercode=@usercode and status=@status";
        List<SqlParameter> parms = new List<SqlParameter>();
        parms.Add(new SqlParameter("@usercode", employeeModel.Code));
        parms.Add(new SqlParameter("@status", (int)ESP.Finance.Utility.FixedAssetStatus.Received));

        IList<ESP.Finance.Entity.ITAssetReceivingInfo> assetlist = ESP.Finance.BusinessLogic.ITAssetReceivingManager.GetList(terms, parms);
        this.gvAsset.DataSource = assetlist;
        this.gvAsset.DataBind();
    }

    /// <summary>
    /// 保存用户离职单信息
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSave_Click(object sender, EventArgs e)
    {
        int dimissionFormID = 0;
        ESP.HumanResource.Entity.DimissionFormInfo dimissionFormInfo = null;
        if (hidDimissionFormID.Value != null)
        {
            if (int.TryParse(hidDimissionFormID.Value, out dimissionFormID))
                dimissionFormInfo = ESP.HumanResource.BusinessLogic.DimissionFormManager.GetModel(dimissionFormID);
        }

        try
        {
            if (dimissionFormInfo == null)
            {
                dimissionFormInfo = new ESP.HumanResource.Entity.DimissionFormInfo();
                dimissionFormInfo.CreateTime = DateTime.Now;
                this.GetDimissionInfo(dimissionFormInfo);
                dimissionFormInfo.Status = (int)DimissionFormStatus.NotSubmit;
                ESP.HumanResource.BusinessLogic.DimissionFormManager.Add(dimissionFormInfo);
            }
            else
            {
                this.GetDimissionInfo(dimissionFormInfo);
                dimissionFormInfo.Status = (int)DimissionFormStatus.NotSubmit;
                ESP.HumanResource.BusinessLogic.DimissionFormManager.Update(dimissionFormInfo);
            }
            ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('离职申请单保存成功！');window.location='DimissionFormEdit.aspx';", true);
        }
        catch (Exception ex)
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('离职申请单保存失败！');window.location='DimissionFormEdit.aspx';", true);
            ESP.Logging.Logger.Add("HumanResource", ex.ToString(), ESP.Logging.LogLevel.Error, ex);
        }
    }

    /// <summary>
    /// 提交用户离职单信息
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        int dimissionFormID = 0;
        ESP.HumanResource.Entity.DimissionFormInfo dimissionFormInfo = null;
        ESP.Framework.Entity.OperationAuditManageInfo manageModel = null;
        if (hidDimissionFormID.Value != null)
        {
            if (int.TryParse(hidDimissionFormID.Value, out dimissionFormID))
                dimissionFormInfo = ESP.HumanResource.BusinessLogic.DimissionFormManager.GetModel(dimissionFormID);
        }

        try
        {
            bool b = false;
            if (dimissionFormInfo == null)
            {
                // 离职单信息
                dimissionFormInfo = new ESP.HumanResource.Entity.DimissionFormInfo();
                dimissionFormInfo.CreateTime = DateTime.Now;
                this.GetDimissionInfo(dimissionFormInfo);
                dimissionFormInfo.Status = (int)DimissionFormStatus.WaitPreAuditor;
                dimissionFormInfo.AppDate = DateTime.Now;

                // 审批日志信息
                ESP.HumanResource.Entity.HRAuditLogInfo hrAuditLogInfo = new ESP.HumanResource.Entity.HRAuditLogInfo();
                manageModel = ESP.Framework.BusinessLogic.OperationAuditManageManager.GetModelByUserId(dimissionFormInfo.UserId);
                if (manageModel == null)
                    manageModel = ESP.Framework.BusinessLogic.OperationAuditManageManager.GetModelByDepId(dimissionFormInfo.DepartmentId);

                if (OperationAuditManageManager.GetCurrentUserIsDirector(CurrentUser.SysID))  // 判断当前离职人是否是总监
                {
                    hrAuditLogInfo.AuditorId = manageModel.DimissionManagerId;  // 总监级离职总经理审批
                    hrAuditLogInfo.AuditorName = manageModel.DimissionManagerName;

                    dimissionFormInfo.PreAuditorId = manageModel.DimissionManagerId;
                    dimissionFormInfo.PreAuditor = manageModel.DimissionManagerName;

                    dimissionFormInfo.DirectorId = manageModel.DimissionManagerId;
                    dimissionFormInfo.DirectorName = manageModel.DimissionManagerName;

                    dimissionFormInfo.ManagerId = manageModel.CEOId;
                    dimissionFormInfo.ManagerName = manageModel.CEOName;

                }
                else if (OperationAuditManageManager.GetCurrentUserIsManager(CurrentUser.SysID))  // 判断当前离职人是否是总经理
                {
                    hrAuditLogInfo.AuditorId = manageModel.CEOId;  // 总经理离职CEO审批
                    hrAuditLogInfo.AuditorName = manageModel.CEOName;

                    dimissionFormInfo.PreAuditorId = manageModel.CEOId;
                    dimissionFormInfo.PreAuditor = manageModel.CEOName;

                    dimissionFormInfo.DirectorId = manageModel.CEOId;
                    dimissionFormInfo.DirectorName = manageModel.CEOName;

                    dimissionFormInfo.ManagerId = manageModel.CEOId;
                    dimissionFormInfo.ManagerName = manageModel.CEOName;

                }
                else if (manageModel.DimissionPreAuditorId == CurrentUserID)//离职人是预审
                {
                    hrAuditLogInfo.AuditorId = manageModel.DimissionDirectorid;  // 预审
                    hrAuditLogInfo.AuditorName = manageModel.DimissionDirectorName;

                    dimissionFormInfo.PreAuditorId = manageModel.DimissionDirectorid;
                    dimissionFormInfo.PreAuditor = manageModel.DimissionDirectorName;

                    dimissionFormInfo.DirectorId = manageModel.DimissionDirectorid;
                    dimissionFormInfo.DirectorName = manageModel.DimissionDirectorName;

                    dimissionFormInfo.ManagerId = manageModel.DimissionManagerId;
                    dimissionFormInfo.ManagerName = manageModel.DimissionManagerName;

                }
                else
                {
                    hrAuditLogInfo.AuditorId = manageModel.DimissionPreAuditorId;  // 预审
                    hrAuditLogInfo.AuditorName = manageModel.DimissionPreAuditor;

                    dimissionFormInfo.PreAuditorId = manageModel.DimissionPreAuditorId;
                    dimissionFormInfo.PreAuditor = manageModel.DimissionPreAuditor;

                    dimissionFormInfo.DirectorId = manageModel.DimissionDirectorid;
                    dimissionFormInfo.DirectorName = manageModel.DimissionDirectorName;

                    dimissionFormInfo.ManagerId = manageModel.DimissionManagerId;
                    dimissionFormInfo.ManagerName = manageModel.DimissionManagerName;

                }

                hrAuditLogInfo.AuditLevel = (int)DimissionFormStatus.WaitPreAuditor;

                // 更新员工离职状态信息
                ESP.HumanResource.Entity.EmployeeBaseInfo empBaseInfo = ESP.HumanResource.BusinessLogic.EmployeeBaseManager.GetModel(dimissionFormInfo.UserId);
                empBaseInfo.DimissionStatus = ESP.HumanResource.Common.Status.DimissionReceiving;

                ESP.HumanResource.Entity.DimissionFormInfo dmodel = ESP.HumanResource.BusinessLogic.DimissionFormManager.GetModelByUserid(dimissionFormInfo.UserId);

                if (dmodel == null)
                    b = ESP.HumanResource.BusinessLogic.DimissionFormManager.Add(dimissionFormInfo, hrAuditLogInfo, empBaseInfo);
                else
                {
                    ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('您已经提交过离职单，无法重复提交!');window.location='DimissionFormEdit.aspx';", true);

                    return;
                }
            }
            else
            {
                if (dimissionFormInfo.Status == (int)DimissionFormStatus.WaitPreAuditor)
                {
                    ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('离职申请单已经提交，请等待审批！');window.location='DimissionFormEdit.aspx';", true);
    
                    return;
                }
                // 离职单信息
                this.GetDimissionInfo(dimissionFormInfo);
                dimissionFormInfo.Status = (int)DimissionFormStatus.WaitPreAuditor;
                dimissionFormInfo.AppDate = DateTime.Now;

                // 审批日志信息
                ESP.HumanResource.Entity.HRAuditLogInfo hrAuditLogInfo = new ESP.HumanResource.Entity.HRAuditLogInfo();

                manageModel = ESP.Framework.BusinessLogic.OperationAuditManageManager.GetModelByUserId(dimissionFormInfo.UserId);
                if (manageModel == null)
                    manageModel = ESP.Framework.BusinessLogic.OperationAuditManageManager.GetModelByDepId(dimissionFormInfo.DepartmentId);

                if (OperationAuditManageManager.GetCurrentUserIsDirector(CurrentUser.SysID))  // 判断当前离职人是否是总监
                {
                    hrAuditLogInfo.AuditorId = manageModel.DimissionManagerId;  // 总监级离职总经理审批
                    hrAuditLogInfo.AuditorName = manageModel.DimissionManagerName;
                    
                    dimissionFormInfo.PreAuditorId = manageModel.DimissionManagerId;
                    dimissionFormInfo.PreAuditor = manageModel.DimissionManagerName;

                    dimissionFormInfo.DirectorId = manageModel.DimissionManagerId;
                    dimissionFormInfo.DirectorName = manageModel.DimissionManagerName;
                    dimissionFormInfo.ManagerId = manageModel.CEOId;
                    dimissionFormInfo.ManagerName = manageModel.CEOName;
                }
                else if (OperationAuditManageManager.GetCurrentUserIsManager(CurrentUser.SysID))  // 判断当前离职人是否是总经理
                {
                    hrAuditLogInfo.AuditorId = manageModel.CEOId;  // 总经理离职CEO审批
                    hrAuditLogInfo.AuditorName = manageModel.CEOName;

                    dimissionFormInfo.PreAuditorId = manageModel.CEOId;
                    dimissionFormInfo.PreAuditor = manageModel.CEOName;

                    dimissionFormInfo.DirectorId = manageModel.CEOId;
                    dimissionFormInfo.DirectorName = manageModel.CEOName;
                    dimissionFormInfo.ManagerId = manageModel.CEOId;
                    dimissionFormInfo.ManagerName = manageModel.CEOName;
                }
                else if (manageModel.DimissionPreAuditorId == CurrentUserID)//离职人是预审
                {
                    hrAuditLogInfo.AuditorId = manageModel.DimissionDirectorid;  // 预审
                    hrAuditLogInfo.AuditorName = manageModel.DimissionDirectorName;

                    dimissionFormInfo.PreAuditorId = manageModel.DimissionDirectorid;
                    dimissionFormInfo.PreAuditor = manageModel.DimissionDirectorName;

                    dimissionFormInfo.DirectorId = manageModel.DimissionDirectorid;
                    dimissionFormInfo.DirectorName = manageModel.DimissionDirectorName;

                    dimissionFormInfo.ManagerId = manageModel.DimissionManagerId;
                    dimissionFormInfo.ManagerName = manageModel.DimissionManagerName;

                }
                else
                {
                    hrAuditLogInfo.AuditorId = manageModel.DimissionPreAuditorId;  // 总监审批
                    hrAuditLogInfo.AuditorName = manageModel.DimissionPreAuditor;

                    dimissionFormInfo.PreAuditorId = manageModel.DimissionPreAuditorId;
                    dimissionFormInfo.PreAuditor = manageModel.DimissionPreAuditor;

                    dimissionFormInfo.DirectorId = manageModel.DimissionDirectorid;
                    dimissionFormInfo.DirectorName = manageModel.DimissionDirectorName;

                    dimissionFormInfo.ManagerId = manageModel.DimissionManagerId;
                    dimissionFormInfo.ManagerName = manageModel.DimissionManagerName;
                }
                hrAuditLogInfo.AuditLevel = (int)DimissionFormStatus.WaitPreAuditor;

                // 更新员工离职状态信息
                ESP.HumanResource.Entity.EmployeeBaseInfo empBaseInfo = ESP.HumanResource.BusinessLogic.EmployeeBaseManager.GetModel(dimissionFormInfo.UserId);
                empBaseInfo.DimissionStatus = ESP.HumanResource.Common.Status.DimissionReceiving;

                b = ESP.HumanResource.BusinessLogic.DimissionFormManager.Update(dimissionFormInfo, hrAuditLogInfo, empBaseInfo);
            }
            if (b)
            {
                int cardCount = ESP.Finance.BusinessLogic.BusinessCardManager.IsHaveCard(UserID);
                string tipStr = "离职申请单提交成功。";
                if (cardCount > 0)
                    tipStr += "\\n请在Last Day的一周前归还“商务卡”到财务；\\n请在Last Day的两天前提交报销、冲销、发票；";
                else
                    tipStr += "\\n请在Last Day的两天前提交报销、冲销、发票；";
                tipStr += "\\n如是自带笔记本请在Last Day将网线交IT部；\\n如是使用公司电脑请在Last Day整理个人数据并与IT部联系；\\n如有借用公司固定资产（相机、录音笔等）请在Last Day交还与IT部。";

                // 计算剩余年假天数和预支年假天数
                //DateTime lastday = dimissionFormInfo.LastDay == null ? dimissionFormInfo.HopeLastDay.Value : dimissionFormInfo.LastDay.Value;

                ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('" + tipStr + "');window.location='DimissionFormEdit.aspx';", true);

                List<MailAddress> mailAddressList = new List<MailAddress>();
                List<MailAddress> mailAddressList2 = new List<MailAddress>();

                // 提交成功后发送提醒邮件
                ESP.Framework.Entity.UserInfo userInfo = ESP.Framework.BusinessLogic.UserManager.Get(dimissionFormInfo.PreAuditorId);
                ESP.Administrative.Entity.OperationAuditManageInfo operationAudit = (new ESP.Administrative.BusinessLogic.OperationAuditManageManager()).GetOperationAuditModelByUserID(dimissionFormInfo.UserId);
                ESP.Administrative.BusinessLogic.UserAttBasicInfoManager userAtt = new ESP.Administrative.BusinessLogic.UserAttBasicInfoManager();
                ESP.Framework.Entity.DepartmentInfo companyDep = userAtt.GetRootDepartmentID(dimissionFormInfo.UserId);
                List<ESP.HumanResource.Entity.UsersInfo> list = UsersManager.GetUserList(companyDep.DepartmentID, ESP.HumanResource.Common.Status.DimissionSendMail);
                if (list != null && list.Count > 0)
                {
                    foreach (ESP.HumanResource.Entity.UsersInfo userModel in list)
                    {
                        mailAddressList2.Add(new MailAddress(userModel.Email));
                    }
                }
                if (operationAudit != null)
                {
                    ESP.Framework.Entity.UserInfo hruser = ESP.Framework.BusinessLogic.UserManager.Get(operationAudit.HRAdminID);
                    if (hruser != null && !string.IsNullOrEmpty(hruser.Email))
                    {
                        mailAddressList2.Add(new MailAddress(hruser.Email));
                    }
                }
                if (userInfo != null && !string.IsNullOrEmpty(userInfo.Email))
                {
                    string url = "http://" + Request.Url.Authority + "/HR/Print/NewDimissionMail.aspx?dimissionId=" + dimissionFormInfo.DimissionId + "&type=1";
                    string body = ESP.HumanResource.Common.SendMailHelper.ScreenScrapeHtml(url);

                    mailAddressList.Add(new MailAddress(userInfo.Email));
                    try
                    {
                        ESP.Mail.MailManager.Send("离职管理", body, true, mailAddressList.ToArray());
                    }
                    catch
                    { }
                }
                if (mailAddressList2.Count != 0)
                {
                    try
                    {
                        string url2 = "http://" + Request.Url.Authority + "/HR/Print/NewDimissionMail.aspx?dimissionId=" + dimissionFormInfo.DimissionId;
                        string body2 = ESP.HumanResource.Common.SendMailHelper.ScreenScrapeHtml(url2);
                        ESP.Mail.MailManager.Send("离职管理", body2, true, mailAddressList2.ToArray());
                    }
                    catch
                    { }
                }
            }
            else
                ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('离职申请单提交失败！');window.location='DimissionFormEdit.aspx';", true);
        }
        catch (Exception ex)
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('离职申请单提交失败！');window.location='DimissionFormEdit.aspx';", true);
        }
    }

    /// <summary>
    /// 获得离职单信息
    /// </summary>
    /// <param name="dimissionFormInfo">离职单信息</param>
    private void GetDimissionInfo(ESP.HumanResource.Entity.DimissionFormInfo dimissionFormInfo)
    {
        if (dimissionFormInfo == null)
            dimissionFormInfo = new ESP.HumanResource.Entity.DimissionFormInfo();

        dimissionFormInfo.UserId = UserID;
        dimissionFormInfo.UserCode = labCode.Text;
        dimissionFormInfo.UserName = UserInfo.FullNameCN;
        dimissionFormInfo.DepartmentId = int.Parse(hidgroupId.Value);
        dimissionFormInfo.DepartmentName = txtcompanyName.Text + "-" + txtdepartmentName.Text + "-" + txtgroupName.Text;
        dimissionFormInfo.HopeLastDay = DateTime.Parse(txtdimissionDate.Text);  // 期望离职日期
        dimissionFormInfo.LastDay = dimissionFormInfo.HopeLastDay;
        dimissionFormInfo.Reason = txtdimissionCause.Text;
        dimissionFormInfo.MobilePhone = txtMobilePhone.Text;
        dimissionFormInfo.PrivateMail = txtEmail.Text;
    }
}
