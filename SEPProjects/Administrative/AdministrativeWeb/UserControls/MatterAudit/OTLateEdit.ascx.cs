using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ESP.Administrative.Entity;
using ESP.Administrative.BusinessLogic;
using ESP.Administrative.Common;
using AdministrativeWeb.UserControls.Matter;
using System.Net.Mail;
using ESP.Framework.Entity;

namespace AdministrativeWeb.UserControls.MatterAudit
{
    public partial class OTLateEdit : MatterUserControl
    {
        #region 变量
        /// <summary>
        /// 考勤事由业务实现类
        /// </summary>
        MattersManager matterManager = new MattersManager();
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
        /// OT业务信息类
        /// </summary>
        private SingleOvertimeManager singleManager = new SingleOvertimeManager();
        /// <summary>
        /// 审批记录业务对象
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
        /// 初始化页面信息
        /// </summary>
        private void InitPage()
        {
            // 判断单据类型
            if (!string.IsNullOrEmpty(Request["mattertype"]))
            {
                int type = int.Parse(Request["mattertype"]);
                if (type == (int)Status.MattersSingle.MattersSingle_OTLate)
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
                        MattersInfo model = matterManager.GetModel(applogInfo.ApproveDateID);
                        if (model != null)
                        {
                            if (model.MatterType == Status.MattersType_OTLate)
                            {
                                ESP.Framework.Entity.UserInfo userinfoModel = ESP.Framework.BusinessLogic.UserManager.Get(model.UserID);

                                hidOffMatterId.Value = model.ID.ToString();
                                hidOffUserid.Value = model.UserID.ToString();
                                txtOffUserName.Text = userinfoModel.FullNameCN;
                                ESP.Framework.Entity.EmployeeInfo emp = ESP.Framework.BusinessLogic.EmployeeManager.Get(model.UserID);
                                txtOffUserCode.Text = emp.Code;
                                IList<ESP.Framework.Entity.EmployeePositionInfo> list =
                                    ESP.Framework.BusinessLogic.DepartmentPositionManager.GetEmployeePositions(model.UserID);
                                if (list != null && list.Count > 0)
                                {
                                    ESP.Framework.Entity.EmployeePositionInfo emppos = list[0];
                                    txtOffGroup.Text = emppos.DepartmentName;
                                    int parnetId = ESP.Framework.BusinessLogic.DepartmentManager.Get(emppos.DepartmentID).ParentID;
                                    txtOffTeam.Text = ESP.Framework.BusinessLogic.DepartmentManager.Get(parnetId).DepartmentName;
                                }
                                labOffAppTime.Text = model.CreateTime.ToString("yyyy年MM月dd日 HH时");

                                OffPickerFrom1.SelectedDate = model.BeginTime;
                                OffPickerTo1.SelectedDate = model.EndTime;
                                // 判断是否是事后申请
                                if (model.CreateTime > model.BeginTime)
                                {
                                    labAfterApprove.Text = "事后申请：<img src=\"../../images/gridview/flag_red.gif\" width=\"12\" height=\"12\" border=\"0\">";
                                }
                                txtOTCause.Text = model.MatterContent;
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
        /// 返回到上一个页面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnOffBack_Click(object sender, EventArgs e)
        {
            Response.Redirect(BackUrl);
        }

        /// 审批驳回
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnOverrule_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(hidLeaveApproveId.Value) && !string.IsNullOrEmpty(hidOffMatterId.Value))
            {
                // 审批记录对象
                int approveId = int.Parse(hidLeaveApproveId.Value);
                // 请假单ID
                int overTimeId = int.Parse(hidOffMatterId.Value);

                ApproveLogInfo approveLogInfo = approvelogManager.GetModel(approveId);   // 获得一个审批对象
                MattersInfo overTimeInfo = matterManager.GetModel(overTimeId);  // 获得一个请假单对象
                if (approveLogInfo != null)
                {
                    approveLogInfo.ApproveState = 2;   // 审批驳回
                    approveLogInfo.UpdateTime = DateTime.Now;
                    approveLogInfo.OperateorID = UserID;
                    approveLogInfo.Approveremark = txtOffTuneApproveRemark.Text.Trim();
                    approvelogManager.Update(approveLogInfo);
                }
                if (overTimeInfo != null)
                {
                    overTimeInfo.UpdateTime = DateTime.Now;
                    overTimeInfo.OperateorID = UserID;
                    // 设置事由单的状态为驳回状态
                    overTimeInfo.MatterState = Status.MattersState_Overrule;
                    overTimeInfo.Approveid = UserID;
                    overTimeInfo.Approvedesc = txtOffTuneApproveRemark.Text.Trim();
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
                        body += "您的晚到申请申请单，审批已被驳回";
                        body += "<br><br><a href='" + ESP.Configuration.ConfigurationManager.SafeAppSettings["AdminSite"] + "'>" + ESP.Configuration.ConfigurationManager.SafeAppSettings["AdminSite"] + "</a>";
                        MailAddress[] recipientAddress = { new MailAddress(email) };

                        ESP.Mail.MailManager.Send("考勤事由被驳回", body, true, null, recipientAddress, null, null, null);
                    }
                }
                catch
                {
                    ESP.Logging.Logger.Add(UserInfo.FullNameCN + "(" + UserID + ")驳回了," + overTimeInfo.Username + "(" + overTimeInfo.UserID + ")申请的晚到申请单，发送邮件失败",
                                "考勤系统事由审批", ESP.Logging.LogLevel.Information);
                }
                ESP.Logging.Logger.Add(UserInfo.FullNameCN + "(" + UserID + ")驳回了," + overTimeInfo.Username + "(" + overTimeInfo.UserID + ")申请的晚到申请单。",
                                "考勤系统事由审批", ESP.Logging.LogLevel.Information);
                ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), "window.location='" + BackUrl + "';alert('晚到申请单审批驳回成功。');", true);
            }
        }

        /// <summary>
        /// 审批通过
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnPass_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(hidLeaveApproveId.Value) && !string.IsNullOrEmpty(hidOffMatterId.Value))
            {
                // 审批记录对象
                int approveId = int.Parse(hidLeaveApproveId.Value);
                // 请假单对象
                int overTimeId = int.Parse(hidOffMatterId.Value);

                ApproveLogManager appManager = new ApproveLogManager();

                ApproveLogInfo approveLogInfo = approvelogManager.GetModel(approveId);
                // MattersInfo overTimeInfo = matterManager.GetModel(overTimeId);
                UserInfo userInfo = ESP.Framework.BusinessLogic.UserManager.Get(UserID);
                MattersInfo mattersModel = matterManager.GetModel(approveLogInfo.ApproveDateID);

                if (approveLogInfo != null)
                {
                    approveLogInfo.ApproveState = 1;    // 审批通过
                    approveLogInfo.UpdateTime = DateTime.Now;
                    approveLogInfo.OperateorID = UserID;
                    approveLogInfo.Approveremark = txtOffTuneApproveRemark.Text.Trim();
                    approvelogManager.Update(approveLogInfo);

                    if (mattersModel.MatterState == Status.MattersState_WaitHR)
                    {
                        mattersModel.UpdateTime = DateTime.Now;
                        mattersModel.OperateorID = UserID;
                        mattersModel.MatterState = Status.MattersState_WaitDirector;
                        mattersModel.Approveid = UserID;
                        mattersModel.Approvedesc += DateTime.Now.ToString("yyyy-MM-dd HH:mm") + userInfo.FullNameCN + "审批通过。";
                        matterManager.Update(mattersModel);

                        #region 审批记录信息
                        ApproveLogInfo applog = new ApproveLogInfo();
                        applog.ApproveDateID = mattersModel.ID;
                        applog.ApproveState = 0;

                        ESP.Administrative.BusinessLogic.OperationAuditManageManager operationAuditManager = new ESP.Administrative.BusinessLogic.OperationAuditManageManager();
                        ESP.Administrative.Entity.OperationAuditManageInfo opearmodel = operationAuditManager.GetOperationAuditModelByUserID(mattersModel.UserID);
                        if (opearmodel != null)
                        {
                            applog.ApproveID = opearmodel.TeamLeaderID;
                            applog.ApproveName = opearmodel.TeamLeaderName;
                        }
                        applog.ApproveType = (int)Status.MattersSingle.MattersSingle_OTLate;
                        applog.ApproveUpUserID = UserID;
                        applog.CreateTime = DateTime.Now;
                        applog.Deleted = false;
                        applog.IsLastApprove = 1;
                        applog.OperateorID = mattersModel.UserID;
                        applog.Sort = 0;
                        applog.UpdateTime = DateTime.Now;
                        appManager.Add(applog);
                        #endregion
                    }
                    else if (mattersModel.MatterState == Status.MattersState_WaitDirector)
                    {
                        mattersModel.UpdateTime = DateTime.Now;
                        mattersModel.OperateorID = UserID;
                        mattersModel.MatterState = Status.MattersState_Passed;    // 请假单审批通过
                        mattersModel.Approveid = UserID;
                        mattersModel.Approvedesc += DateTime.Now.ToString("yyyy-MM-dd HH:mm") + userInfo.FullNameCN + "审批通过。";
                        matterManager.Update(mattersModel);
                    }
                }
              
                ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), "window.location='" + BackUrl + "';alert('晚到申请单审批通过。');", true);
            }
        }
    
  
    }
}