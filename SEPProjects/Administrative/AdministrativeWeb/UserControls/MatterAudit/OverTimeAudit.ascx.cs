using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ESP.Framework.Entity;
using ESP.Administrative.Entity;
using ESP.Administrative.BusinessLogic;
using ESP.Administrative.Common;
using AdministrativeWeb.UserControls.Matter;
using System.Net.Mail;

namespace AdministrativeWeb.UserControls.MatterAudit
{
    public partial class OverTimeAudit : MatterUserControl
    {
        #region 成员变量
        /// <summary>
        /// OT单业务类
        /// </summary>
        private SingleOvertimeManager overtimeManager = new SingleOvertimeManager();
        /// <summary>
        /// 审批日志业务类
        /// </summary>
        private ApproveLogManager approvelogManager = new ApproveLogManager();
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                InitPage();
            }
        }

        /// <summary>
        /// 初始化页面内容
        /// </summary>
        protected void InitPage()
        {
            // 判断单据类型
            if (!string.IsNullOrEmpty(Request["mattertype"]))
            { 
                int type = int.Parse(Request["mattertype"]);
                if (type == (int)Status.MattersSingle.MattersSingle_OverTime)
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

                    if (applogInfo != null && (applogInfo.ApproveID == UserID || approveuserid.IndexOf(applogInfo.ApproveID.ToString()) != -1) && applogInfo.ApproveState == Status.ApproveState_NoPassed)
                    {
                        // 获得OT单信息
                        SingleOvertimeInfo overtimeInfo = overtimeManager.GetModel(applogInfo.ApproveDateID);
                        if (overtimeInfo != null)
                        {
                            singlOverTimeId.Value = overtimeInfo.ID.ToString();
                            hidUserid.Value = overtimeInfo.UserID.ToString();
                            txtUserName.Text = overtimeInfo.EmployeeName;
                            txtUserCode.Text = overtimeInfo.UserCode;
                            IList<ESP.Framework.Entity.EmployeePositionInfo> list = ESP.Framework.BusinessLogic.DepartmentPositionManager.GetEmployeePositions(overtimeInfo.UserID);
                            if (list != null && list.Count > 0)
                            {
                                ESP.Framework.Entity.EmployeePositionInfo emppos = list[0];
                                txtGroup.Text = emppos.DepartmentName;
                                int parnetId = ESP.Framework.BusinessLogic.DepartmentManager.Get(emppos.DepartmentID).ParentID;
                                txtTeam.Text = ESP.Framework.BusinessLogic.DepartmentManager.Get(parnetId).DepartmentName;
                            }
                            labAppTime.Text = overtimeInfo.AppTime.ToString("yyyy年MM月dd日 HH时");
                            hidOverTimeProjectId.Value = overtimeInfo.ProjectID.ToString();
                            txtOverTimeProjectNo.Text = overtimeInfo.ProjectNo;
                            PickerFrom1.SelectedDate = overtimeInfo.BeginTime;
                            PickerTo1.SelectedDate = overtimeInfo.EndTime;
                            txtDes.Text = overtimeInfo.OverTimeCause;

                            this.grdMatterDetails.DataSource = new MatterReasonManager().GetList(" SingleOverTimeID=" + overtimeInfo.ID.ToString());
                            this.grdMatterDetails.DataBind();

                            // 判断是否是事后申请
                            if (overtimeInfo.CreateTime > overtimeInfo.BeginTime)
                            {
                                labAfterApprove.Text = "事后申请：<img src=\"../../images/gridview/flag_red.gif\" width=\"12\" height=\"12\" border=\"0\">";
                            }
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
        /// 返回
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnBack_Click(object sender, EventArgs e)
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
            if (!string.IsNullOrEmpty(hidLeaveApproveId.Value) && !string.IsNullOrEmpty(singlOverTimeId.Value))
            {
                // 审批记录对象
                int approveId = int.Parse(hidLeaveApproveId.Value);
                // 请假单ID
                int overTimeId = int.Parse(singlOverTimeId.Value);

                ApproveLogInfo approveLogInfo = approvelogManager.GetModel(approveId);   // 获得一个审批对象
                SingleOvertimeInfo overTimeInfo = overtimeManager.GetModel(overTimeId);  // 获得一个请假单对象
                if (approveLogInfo != null)
                {
                    approveLogInfo.ApproveState = 2;   // 审批驳回
                    approveLogInfo.UpdateTime = DateTime.Now;
                    approveLogInfo.OperateorID = UserID;
                    approveLogInfo.Approveremark = txtOverTimeApproveRemark.Text.Trim();
                    approvelogManager.Update(approveLogInfo);
                }
                if (overTimeInfo != null)
                {
                    overTimeInfo.UpdateTime = DateTime.Now;
                    overTimeInfo.OperateorID = UserID;
                    // 审批驳回
                    overTimeInfo.Approvestate = Status.OverTimeState_Overrule;
                    overTimeInfo.ApproveID = UserID;
                    overTimeInfo.ApproveRemark = txtOverTimeApproveRemark.Text.Trim();
                    overtimeManager.Update(overTimeInfo);
                }
                //发邮件
                try
                {
                    string email = new ESP.Compatible.Employee(overTimeInfo.UserID).EMail;
                    if (!string.IsNullOrEmpty(email))
                    {
                        string url = "http://" + Request.Url.Authority + "/MailTemplate/MattersMail.aspx?singleid=" + overTimeInfo.ID + "&flag=1";
                        string body = ESP.HumanResource.Common.SendMailHelper.ScreenScrapeHtml(url);
                        body += "<br><br>您的OT申请单，审批已被驳回";
                        body += "<br><br><a href='" + ESP.Configuration.ConfigurationManager.SafeAppSettings["AdminSite"] + "'>" + ESP.Configuration.ConfigurationManager.SafeAppSettings["AdminSite"] + "</a>";
                        MailAddress[] recipientAddress = { new MailAddress(email) };

                        ESP.Mail.MailManager.Send("考勤事由审批被驳回", body, true, null, recipientAddress, null, null, null);
                    }
                }
                catch
                {
                    ESP.Logging.Logger.Add(UserInfo.FullNameCN + "(" + UserID + ")驳回了," + overTimeInfo.EmployeeName + "(" + overTimeInfo.UserID + ")申请的OT单，发送邮件失败",
                                "考勤系统事由审批", ESP.Logging.LogLevel.Information);
                }
                ESP.Logging.Logger.Add(UserInfo.FullNameCN + "(" + UserID + ")驳回了," + overTimeInfo.EmployeeName + "(" + overTimeInfo.UserID + ")申请的OT单。",
                                "考勤系统事由审批", ESP.Logging.LogLevel.Information);
                ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), "window.location='" + BackUrl + "';alert('OT单审批驳回成功。');", true);
            }
        }

        /// <summary>
        /// 审批通过
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnPass_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(hidLeaveApproveId.Value) && !string.IsNullOrEmpty(singlOverTimeId.Value))
            {
                // 审批记录对象
                int approveId = int.Parse(hidLeaveApproveId.Value);
                // 请假单对象
                int overTimeId = int.Parse(singlOverTimeId.Value);

                ApproveLogInfo approveLogInfo = approvelogManager.GetModel(approveId);
                SingleOvertimeInfo overTimeInfo = overtimeManager.GetModel(overTimeId);
                if (approveLogInfo != null)
                {
                    approveLogInfo.ApproveState = 1;    // 审批通过
                    approveLogInfo.UpdateTime = DateTime.Now;
                    approveLogInfo.OperateorID = UserID;
                    approveLogInfo.Approveremark = txtOverTimeApproveRemark.Text.Trim();
                    approvelogManager.Update(approveLogInfo);
                }
                if (overTimeInfo != null)
                {
                    overTimeInfo.UpdateTime = DateTime.Now;
                    overTimeInfo.OperateorID = UserID;
                    overTimeInfo.Approvestate = Status.OverTimeState_Passed;    // OT单审批通过
                    overTimeInfo.ApproveID = UserID;
                    overTimeInfo.ApproveRemark = txtOverTimeApproveRemark.Text.Trim();

                    //发邮件
                    try
                    {
                        string email = new ESP.Compatible.Employee(overTimeInfo.UserID).EMail;
                        if (!string.IsNullOrEmpty(email))
                        {
                            string url = "http://" + Request.Url.Authority + "/MailTemplate/MattersMail.aspx?singleid=" + overTimeInfo.ID + "&flag=1";
                            string body = ESP.HumanResource.Common.SendMailHelper.ScreenScrapeHtml(url);
                            body += "<br><br>您的OT单审批已通过";
                            body += "<br><br><a href='" + ESP.Configuration.ConfigurationManager.SafeAppSettings["AdminSite"] + "'>" + ESP.Configuration.ConfigurationManager.SafeAppSettings["AdminSite"] + "</a>";
                            MailAddress[] recipientAddress = { new MailAddress(email) };

                            ESP.Mail.MailManager.Send("考勤事由审批通过", body, true, null, recipientAddress, null, null, null);
                        }
                    }
                    catch
                    {
                        ESP.Logging.Logger.Add(UserInfo.FullNameCN + "(" + UserID + ")审批通过了," + overTimeInfo.EmployeeName + "(" + overTimeInfo.UserID + ")申请的OT单，发送邮件失败",
                                "考勤系统事由审批", ESP.Logging.LogLevel.Information);
                    }
                    overtimeManager.Update(overTimeInfo);
                    ESP.Logging.Logger.Add(UserInfo.FullNameCN + "(" + UserID + ")审批通过了," + overTimeInfo.EmployeeName + "(" + overTimeInfo.UserID + ")申请的OT单。",
                                "考勤系统事由审批", ESP.Logging.LogLevel.Information);
                }
                ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), "window.location='" + BackUrl + "';alert('OT单审批通过。');", true);
            }
        }


        protected void grdMatterDetails_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            MatterReasonInfo info = (MatterReasonInfo)e.Row.DataItem;
            if (info != null)
            {
                Label lblTimeStart = (Label)e.Row.FindControl("lblTimeStart");
                Label lblTimeEnd = (Label)e.Row.FindControl("lblTimeEnd");
                Label txtMatterDetails = (Label)e.Row.FindControl("txtMatterDetails");

                lblTimeStart.Text = info.StartDate.ToString();
                lblTimeEnd.Text = info.EndDate.ToString();

                txtMatterDetails.Text = info.Details;
            }
        }
    }
}