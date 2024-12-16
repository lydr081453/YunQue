using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ESP.Administrative.BusinessLogic;
using ESP.Administrative.Entity;
using ESP.Administrative.Common;
using ESP.Framework.Entity;
using AdministrativeWeb.UserControls.Matter;
using System.Net.Mail;

namespace AdministrativeWeb.UserControls.MatterAudit
{
    /// <summary>
    /// 请假单审批页面
    /// 请假单被驳回后不受七天提交的限制
    /// </summary>
    public partial class LeaveAudit : MatterUserControl
    {
        #region private 变量定义
        /// <summary>
        /// 事由业务实现类
        /// </summary>
        private MattersManager matterManager = new MattersManager();
        /// <summary>
        /// 考勤时间记录信息业务对象
        /// </summary>
        private ClockInManager clockInManager = new ClockInManager();
        /// <summary>
        /// 考勤业务对象类
        /// </summary>
        private ESP.Administrative.BusinessLogic.AttendanceManager attMan =
            new ESP.Administrative.BusinessLogic.AttendanceManager();
        /// <summary>
        /// 审批记录业务对象
        /// </summary>
        private ApproveLogManager approvelogManager = new ApproveLogManager();
        #endregion

        #region protected MemberMethod

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                initPage();
            }
        }

        /// <summary>
        /// 初始化请假单页面
        /// </summary>
        private void initPage()
        {
            // 判断单据类型
            if (!string.IsNullOrEmpty(Request["mattertype"]))
            {
                int type = int.Parse(Request["mattertype"]);
                if (type == (int)Status.MattersSingle.MattersSingle_Leave)
                {
                    int approveId = int.Parse(Request["matterid"]);
                    // 审批记录id
                    hidLeaveApproveId.Value = approveId.ToString();
                    ApproveLogInfo applogInfo = approvelogManager.GetModel(approveId);

                    // 审批人用户ID
                    string approveuserid = UserID.ToString();
                    // 获得用户所代理的审批人
                    IList<ESP.Framework.Entity.AuditBackUpInfo> Delegates = ESP.Framework.BusinessLogic.AuditBackUpManager.GetModelsByBackUpUserID(UserID);
                    if (Delegates != null && Delegates.Count > 0)
                    {
                        foreach (ESP.Framework.Entity.AuditBackUpInfo auditbackup in Delegates)
                        {
                            approveuserid += "," + auditbackup.UserID;
                        }
                    }

                    if (applogInfo != null && (applogInfo.ApproveID == UserID || approveuserid.IndexOf(applogInfo.ApproveID.ToString()) != -1)
                        && applogInfo.ApproveState == Status.ApproveState_NoPassed)
                    {
                        MattersInfo model = matterManager.GetModel(applogInfo.ApproveDateID);
                        hidLeaveID.Value = model.ID.ToString();
                        // 判断请假单的类型(病假、事假)
                        if (model.MatterType == Status.MattersType_Leave || model.MatterType == Status.MattersType_Sick)
                        {
                            if (model.MatterType == Status.MattersType_Leave)
                                chkThing.Checked = true;
                            else
                                chkSick.Checked = true;

                            LeavePickerFrom1.SelectedDate = model.BeginTime;
                            LeavePickerTo1.SelectedDate = model.EndTime;
                            LeavePickerFrom2.SelectedDate = model.BeginTime;
                            LeavePickerTo2.SelectedDate = model.EndTime;
                            LeavePickerFrom3.SelectedDate = model.BeginTime;
                            LeavePickerTo3.SelectedDate = model.EndTime;
                        }
                        // 判断请假单的类型(年假、产前检查)
                        else if (model.MatterType == Status.MattersType_Annual || model.MatterType == Status.MattersType_Annual_Last || model.MatterType == Status.MattersType_PrenatalCheck || model.MatterType == Status.MattersType_Incentive)
                        {
                            if (model.MatterType == Status.MattersType_Annual)
                                chkAnnual.Checked = true;
                            else if (model.MatterType == Status.MattersType_Annual_Last)
                                chkAnnualLast.Checked = true;
                            else if (model.MatterType == Status.MattersType_PrenatalCheck)
                                chkPrenatalCheck.Checked = true;
                            //else
                            //    chkIncentive.Checked = true;

                            LeavePickerFrom1.SelectedDate = model.BeginTime;
                            LeavePickerTo1.SelectedDate = model.EndTime;
                            LeavePickerFrom2.SelectedDate = model.BeginTime;
                            LeavePickerTo2.SelectedDate = model.EndTime;
                            LeavePickerFrom3.SelectedDate = model.BeginTime;
                            LeavePickerTo3.SelectedDate = model.EndTime;
                        }
                        // 判断请假单的类型(婚假、丧假、产假)
                        else if (model.MatterType == Status.MattersType_Bereavement
                            || model.MatterType == Status.MattersType_Marriage
                            || model.MatterType == Status.MattersType_Maternity
                            || model.MatterType == Status.MattersType_PeiChanJia
                            )
                        {
                            if (model.MatterType == Status.MattersType_Bereavement)
                                chkBereavement.Checked = true;
                            else if (model.MatterType == Status.MattersType_Marriage)
                                chkMarriage.Checked = true;
                            else if (model.MatterType == Status.MattersType_Maternity)
                                chkMaternity.Checked = true;
                            else if (model.MatterType == Status.MattersType_PeiChanJia)
                                chkPeiChanJia.Checked = true;

                            LeavePickerFrom1.SelectedDate = model.BeginTime;
                            LeavePickerTo1.SelectedDate = model.EndTime;
                            LeavePickerFrom2.SelectedDate = model.BeginTime;
                            LeavePickerTo2.SelectedDate = model.EndTime;
                            LeavePickerFrom3.SelectedDate = model.BeginTime;
                            LeavePickerTo3.SelectedDate = model.EndTime;
                        }
                        else
                        {
                            chkSick.Checked = true;
                            LeavePickerFrom1.SelectedDate = DateTime.Parse(_selectDateTime);
                            LeavePickerTo1.SelectedDate = DateTime.Parse(_selectDateTime);
                            LeavePickerFrom2.SelectedDate = DateTime.Parse(_selectDateTime);
                            LeavePickerTo2.SelectedDate = DateTime.Parse(_selectDateTime);
                            return;
                        }
                        txtLeaveCause.Text = model.MatterContent;
                        // 获得用户基本信息和用户部门组别信息
                        ESP.Framework.Entity.UserInfo userinfoModel = ESP.Framework.BusinessLogic.UserManager.Get(model.UserID);
                        hidLeaveUserid.Value = model.UserID.ToString();
                        txtLeaveUserName.Text = userinfoModel.FullNameCN;
                        ESP.Framework.Entity.EmployeeInfo emp = ESP.Framework.BusinessLogic.EmployeeManager.Get(model.UserID);
                        txtLeaveUserCode.Text = emp.Code;
                        IList<ESP.Framework.Entity.EmployeePositionInfo> list =
                            ESP.Framework.BusinessLogic.DepartmentPositionManager.GetEmployeePositions(model.UserID);
                        if (list != null && list.Count > 0)
                        {
                            ESP.Framework.Entity.EmployeePositionInfo emppos = list[0];
                            txtLeaveGroup.Text = emppos.DepartmentName;
                            int parnetId = ESP.Framework.BusinessLogic.DepartmentManager.Get(emppos.DepartmentID).ParentID;
                            txtLeaveTeam.Text = ESP.Framework.BusinessLogic.DepartmentManager.Get(parnetId).DepartmentName;
                        }
                        // 判断是否是事后申请
                        if (model.CreateTime > model.BeginTime)
                        {
                            labAfterApprove.Text = "事后申请：<img src=\"../../images/gridview/flag_red.gif\" width=\"12\" height=\"12\" border=\"0\">";
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), Guid.NewGuid().ToString(), "window.location='" + BackUrl + "';alert('您没有权限操作此数据。');", true);
                    }
                }
            }
        }

        /// <summary>
        /// 返回到上一个页面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnLeaveBack_Click(object sender, EventArgs e)
        {
            Response.Redirect(BackUrl);
        }

        /// <summary>
        /// 审批驳回
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnOverrule_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(hidLeaveApproveId.Value) && !string.IsNullOrEmpty(hidLeaveID.Value))
            {
                // 审批记录对象
                int approveId = int.Parse(hidLeaveApproveId.Value);
                // 请假单ID
                int overTimeId = int.Parse(hidLeaveID.Value);

                ApproveLogInfo approveLogInfo = approvelogManager.GetModel(approveId);   // 获得一个审批对象
                MattersInfo overTimeInfo = matterManager.GetModel(overTimeId);  // 获得一个请假单对象
                if (approveLogInfo != null)
                {
                    approveLogInfo.ApproveState = 2;   // 审批驳回
                    approveLogInfo.UpdateTime = DateTime.Now;
                    approveLogInfo.OperateorID = UserID;
                    approveLogInfo.Approveremark = txtLeaveApproveRemark.Text.Trim();
                    approvelogManager.Update(approveLogInfo);
                }
                if (overTimeInfo != null)
                {
                    overTimeInfo.UpdateTime = DateTime.Now;
                    overTimeInfo.OperateorID = UserID;
                    // 设置事由单的状态为驳回状态
                    overTimeInfo.MatterState = Status.MattersState_Overrule;
                    overTimeInfo.Approveid = UserID;
                    overTimeInfo.Approvedesc = txtLeaveApproveRemark.Text.Trim();
                    matterManager.Update(overTimeInfo);
                }
                //发邮件
                try
                {
                    string email = new ESP.Compatible.Employee(overTimeInfo.UserID).EMail;
                    if (!string.IsNullOrEmpty(email))
                    {
                        string url = "http://" + Request.Url.Authority + "/MailTemplate/MattersMail.aspx?matterid=" + overTimeInfo.ID + "&flag=1";
                        string body = ESP.HumanResource.Common.SendMailHelper.ScreenScrapeHtml(url);
                        body += "<br><br>您的事由申请单，审批已被驳回";
                        body += "<br><br><a href='" + ESP.Configuration.ConfigurationManager.SafeAppSettings["AdminSite"] + "'>" + ESP.Configuration.ConfigurationManager.SafeAppSettings["AdminSite"] + "</a>";
                        MailAddress[] recipientAddress = { new MailAddress(email) };

                        ESP.Mail.MailManager.Send("考勤事由被驳回", body, true, null, recipientAddress, null, null, null);
                    }
                }
                catch
                {
                    ESP.Logging.Logger.Add(UserInfo.FullNameCN + "(" + UserID + ")驳回了," + overTimeInfo.Username + "(" + overTimeInfo.UserID + ")申请的请假单，发送邮件失败",
                                "考勤系统事由审批", ESP.Logging.LogLevel.Information);
                }
                ESP.Logging.Logger.Add(UserInfo.FullNameCN + "(" + UserID + ")驳回了," + overTimeInfo.Username + "(" + overTimeInfo.UserID + ")申请的请假单。",
                                "考勤系统事由审批", ESP.Logging.LogLevel.Information);
                ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), "window.location='" + BackUrl + "';alert('请假单审批驳回成功。');", true);
            }
        }

        /// <summary>
        /// 审批通过
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnPass_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(hidLeaveApproveId.Value) && !string.IsNullOrEmpty(hidLeaveID.Value))
            {
                // 审批记录对象
                int approveId = int.Parse(hidLeaveApproveId.Value);
                // 请假单对象
                int overTimeId = int.Parse(hidLeaveID.Value);

                ApproveLogInfo approveLogInfo = approvelogManager.GetModel(approveId);
                MattersInfo overTimeInfo = matterManager.GetModel(overTimeId);
                if (approveLogInfo != null)
                {
                    approveLogInfo.ApproveState = 1;    // 审批通过
                    approveLogInfo.UpdateTime = DateTime.Now;
                    approveLogInfo.OperateorID = UserID;
                    approveLogInfo.Approveremark = txtLeaveApproveRemark.Text.Trim();
                    approvelogManager.Update(approveLogInfo);
                }
                if (overTimeInfo != null)
                {
                    // 判断是否是等待总监审批状态
                    if (overTimeInfo.MatterState == Status.MattersState_WaitHR)
                    {
                        OperationAuditManageManager operationAuditManage = new OperationAuditManageManager();
                        ESP.Administrative.Entity.OperationAuditManageInfo operationAuditModel = operationAuditManage.GetOperationAuditModelByUserID(overTimeInfo.UserID);
                        // DataCodeManager dataCodeManager = new DataCodeManager();
                        int hrid = 0;
                        // List<ESP.Administrative.Entity.DataCodeInfo> dataCodeList = dataCodeManager.GetDataCodeByType("TCGHR");
                        //if (dataCodeList != null && dataCodeList.Count > 0)
                        //{
                        //    hrid = int.Parse(dataCodeList[0].Code);
                        //}
                        // 判断用户如果是TCG集团的，HR审批通过后又直接领导审批
                        if (operationAuditModel != null && operationAuditModel.HRAdminID == UserID)
                        {
                            overTimeInfo.UpdateTime = DateTime.Now;
                            overTimeInfo.OperateorID = UserID;
                            overTimeInfo.MatterState = Status.MattersState_WaitDirector;
                            overTimeInfo.Approveid = UserID;
                            overTimeInfo.Approvedesc = txtLeaveApproveRemark.Text.Trim();

                            #region 审批记录信息
                            ApproveLogInfo applog = new ApproveLogInfo();
                            applog.ApproveDateID = overTimeInfo.ID;
                            applog.ApproveState = 0;

                            // ESP.Administrative.BusinessLogic.OperationAuditManageManager operationAuditManager = new ESP.Administrative.BusinessLogic.OperationAuditManageManager();
                            // ESP.Administrative.Entity.OperationAuditManageInfo opearmodel = operationAuditManager.GetOperationAuditModelByUserID(overTimeInfo.UserID);
                            if (operationAuditModel != null)
                            {
                                applog.ApproveID = operationAuditModel.TeamLeaderID;
                                applog.ApproveName = operationAuditModel.TeamLeaderName;
                            }
                            applog.ApproveType = (int)Status.MattersSingle.MattersSingle_Leave;
                            applog.ApproveUpUserID = UserID;
                            applog.CreateTime = DateTime.Now;
                            applog.Deleted = false;
                            applog.IsLastApprove = 1;
                            applog.OperateorID = overTimeInfo.UserID;
                            applog.Sort = 0;
                            applog.UpdateTime = DateTime.Now;
                            applog.ID = approvelogManager.Add(applog);
                            #endregion

                            //发邮件
                            try
                            {
                                string email = new ESP.Compatible.Employee(applog.ApproveID).EMail;
                                if (!string.IsNullOrEmpty(email))
                                {
                                    string url = "http://" + Request.Url.Authority + "/MailTemplate/MattersMail.aspx?matterid=" + overTimeInfo.ID + "&flag=1";
                                    string body = ESP.HumanResource.Common.SendMailHelper.ScreenScrapeHtml(url);
                                    body += "<br><br>" + ESP.Framework.BusinessLogic.UserManager.Get(overTimeInfo.UserID).FullNameCN
                                        + "提交的请假单等待您的审批";
                                    string pageurl = "Attendance/AuditEdit.aspx?mattertype={0}&matterid={1}&backurl=AuditList.aspx";
                                    body += "<br><br><a href='" + ESP.Configuration.ConfigurationManager.SafeAppSettings["AdminSite"] +
                                        "/" + string.Format(pageurl, applog.ApproveType, applog.ID) + "'>" +
                                        ESP.Configuration.ConfigurationManager.SafeAppSettings["AdminSite"] + "</a>";
                                    MailAddress[] recipientAddress = { new MailAddress(email) };

                                    ESP.Mail.MailManager.Send("考勤事由等待审批", body, true, null, recipientAddress, null, null, null);
                                }
                            }
                            catch
                            {
                                ESP.Logging.Logger.Add(UserInfo.FullNameCN + "(" + UserID + ")审批通过了," + overTimeInfo.Username + "(" + overTimeInfo.UserID + ")申请的请假单，发送邮件失败",
                                    "考勤系统事由审批", ESP.Logging.LogLevel.Information);
                            }
                        }
                        else
                        {
                            // 判断申请的请假单的类型是否是
                            if (overTimeInfo.MatterType == Status.MattersType_Bereavement
                                || overTimeInfo.MatterType == Status.MattersType_Marriage
                                || overTimeInfo.MatterType == Status.MattersType_Maternity
                                || overTimeInfo.MatterType == Status.MattersType_PrenatalCheck
                                 || overTimeInfo.MatterType == Status.MattersType_PeiChanJia)
                            {
                                overTimeInfo.UpdateTime = DateTime.Now;
                                overTimeInfo.OperateorID = UserID;
                                overTimeInfo.MatterState = Status.MattersState_WaitDirector;
                                overTimeInfo.Approveid = UserID;
                                overTimeInfo.Approvedesc = txtLeaveApproveRemark.Text.Trim();

                                #region 审批记录信息
                                ApproveLogInfo applog = new ApproveLogInfo();
                                applog.ApproveDateID = overTimeInfo.ID;
                                applog.ApproveState = 0;

                                ESP.Administrative.BusinessLogic.OperationAuditManageManager operationAuditManager = new ESP.Administrative.BusinessLogic.OperationAuditManageManager();
                                ESP.Administrative.Entity.OperationAuditManageInfo opearmodel = operationAuditManager.GetOperationAuditModelByUserID(overTimeInfo.UserID);
                                if (opearmodel != null)
                                {
                                    applog.ApproveID = opearmodel.TeamLeaderID;
                                    applog.ApproveName = opearmodel.TeamLeaderName;
                                }
                                applog.ApproveType = (int)Status.MattersSingle.MattersSingle_Leave;
                                applog.ApproveUpUserID = UserID;
                                applog.CreateTime = DateTime.Now;
                                applog.Deleted = false;
                                applog.IsLastApprove = 1;
                                applog.OperateorID = overTimeInfo.UserID;
                                applog.Sort = 0;
                                applog.UpdateTime = DateTime.Now;
                                applog.ID = approvelogManager.Add(applog);
                                #endregion

                                //发邮件
                                try
                                {
                                    string email = new ESP.Compatible.Employee(applog.ApproveID).EMail;
                                    if (!string.IsNullOrEmpty(email))
                                    {
                                        string url = "http://" + Request.Url.Authority + "/MailTemplate/MattersMail.aspx?matterid=" + overTimeInfo.ID + "&flag=1";
                                        string body = ESP.HumanResource.Common.SendMailHelper.ScreenScrapeHtml(url);
                                        body += "<br><br>" + ESP.Framework.BusinessLogic.UserManager.Get(overTimeInfo.UserID).FullNameCN
                                            + "提交的请假单等待您的审批";
                                        string pageurl = "Attendance/AuditEdit.aspx?mattertype={0}&matterid={1}&backurl=AuditList.aspx";
                                        body += "<br><br><a href='" + ESP.Configuration.ConfigurationManager.SafeAppSettings["AdminSite"] +
                                            "/" + string.Format(pageurl, applog.ApproveType, applog.ID) + "'>" +
                                            ESP.Configuration.ConfigurationManager.SafeAppSettings["AdminSite"] + "</a>";
                                        MailAddress[] recipientAddress = { new MailAddress(email) };

                                        ESP.Mail.MailManager.Send("考勤事由等待审批", body, true, null, recipientAddress, null, null, null);
                                    }
                                }
                                catch
                                {
                                    ESP.Logging.Logger.Add(UserInfo.FullNameCN + "(" + UserID + ")审批通过了," + overTimeInfo.Username + "(" + overTimeInfo.UserID + ")申请的请假单，发送邮件失败",
                                        "考勤系统事由审批", ESP.Logging.LogLevel.Information);
                                }
                            }
                            else
                            {
                                overTimeInfo.MatterState = Status.MattersState_Passed;
                                overTimeInfo.Approveid = UserID;
                                overTimeInfo.Approvedesc = txtLeaveApproveRemark.Text.Trim();

                                //发邮件
                                try
                                {
                                    string email = new ESP.Compatible.Employee(overTimeInfo.UserID).EMail;
                                    if (!string.IsNullOrEmpty(email))
                                    {
                                        string url = "http://" + Request.Url.Authority + "/MailTemplate/MattersMail.aspx?matterid=" + overTimeInfo.ID + "&flag=1";
                                        string body = ESP.HumanResource.Common.SendMailHelper.ScreenScrapeHtml(url);
                                        body += "<br><br>您的请假单审批已通过";
                                        body += "<br><br><a href='" + ESP.Configuration.ConfigurationManager.SafeAppSettings["AdminSite"] + "'>" + ESP.Configuration.ConfigurationManager.SafeAppSettings["AdminSite"] + "</a>";
                                        MailAddress[] recipientAddress = { new MailAddress(email) };

                                        ESP.Mail.MailManager.Send("考勤事由审批通过", body, true, null, recipientAddress, null, null, null);
                                    }
                                }
                                catch
                                {
                                    ESP.Logging.Logger.Add(UserInfo.FullNameCN + "(" + UserID + ")审批通过了," + overTimeInfo.Username + "(" + overTimeInfo.UserID + ")申请的请假单，发送邮件失败",
                                        "考勤系统事由审批", ESP.Logging.LogLevel.Information);
                                }
                            }
                        }
                    }
                    // 判断是否是等待考勤员审批状态
                    else if (overTimeInfo.MatterState == Status.MattersState_WaitDirector)
                    {
                        overTimeInfo.UpdateTime = DateTime.Now;
                        overTimeInfo.OperateorID = UserID;
                        overTimeInfo.MatterState = Status.MattersState_Passed;    // 请假单审批通过
                        overTimeInfo.Approveid = UserID;
                        overTimeInfo.Approvedesc = txtLeaveApproveRemark.Text.Trim();

                        //发邮件
                        try
                        {
                            string email = new ESP.Compatible.Employee(overTimeInfo.UserID).EMail;
                            if (!string.IsNullOrEmpty(email))
                            {
                                string url = "http://" + Request.Url.Authority + "/MailTemplate/MattersMail.aspx?matterid=" + overTimeInfo.ID + "&flag=1";
                                string body = ESP.HumanResource.Common.SendMailHelper.ScreenScrapeHtml(url);
                                body += "<br><br>您的请假单审批已通过";
                                body += "<br><br><a href='" + ESP.Configuration.ConfigurationManager.SafeAppSettings["AdminSite"] + "'>" + ESP.Configuration.ConfigurationManager.SafeAppSettings["AdminSite"] + "</a>";
                                MailAddress[] recipientAddress = { new MailAddress(email) };

                                ESP.Mail.MailManager.Send("考勤事由审批通过", body, true, null, recipientAddress, null, null, null);
                            }
                        }
                        catch
                        {
                            ESP.Logging.Logger.Add(UserInfo.FullNameCN + "(" + UserID + ")审批通过了," + overTimeInfo.Username + "(" + overTimeInfo.UserID + ")申请的请假单，发送邮件失败",
                                    "考勤系统事由审批", ESP.Logging.LogLevel.Information);
                        }
                    }
                    matterManager.Update(overTimeInfo);
                    ESP.Logging.Logger.Add(UserInfo.FullNameCN + "(" + UserID + ")审批通过了," + overTimeInfo.Username + "(" + overTimeInfo.UserID + ")申请的请假单。",
                                "考勤系统事由审批", ESP.Logging.LogLevel.Information);
                }
                ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), "window.location='" + BackUrl + "';alert('请假单审批通过。');", true);
            }
        }

        #endregion
    }
}
