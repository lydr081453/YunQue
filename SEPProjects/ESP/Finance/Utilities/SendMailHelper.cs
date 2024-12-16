using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections;
using System.Net.Mail;

/// <summary>
///SendMailHelper 的摘要说明
/// </summary>
/// 
namespace ESP.Finance.Utility
{
    public class SendMailHelper
    {
        #region 常量
        //{0}为负责人，{1}为PR单号
        const string referPRtoUser = "{0}已提交项目号申请{1},您为该项目号创建人。";
        const string evidenceCommit = "{0}已提交项目{1}的证据链文件,请您进行审核。";
        const string evidenceConfirm = "{0}已经审核通过{1}的证据链文件。";
        const string evidenceReject = "{0}已经驳回{1}的证据链文件,请及时更新。";
        const string referPRtoRec = "{0}已提交项目号申请{1},您被设定为负责人。";
        //{0}为pr单号，{1}为业务审核人，{2}为下一级业务审核人
        const string referPROperaZH = "{0}已通过{1}业务审批，正等待{2}进行下一级审批。";
        //{0}申请人，{1}为pr单号，{2}为业务审批人
        const string referPR0peraZH2 = "{0}已提交项目号申请{1}，正等待{2}进行业务审批。";
        const string ProjectApplicantBizAudit = "{0}已通过{1}项目审批,请您进行{2}。";
        const string FinancialContractAudi = "{0}已通过{1}合同审批,请您进行{2}。";
        const string ProjectAuditComplete = "{0}已完成审批，项目号为{1}。";
        const string ProjectAuditBizReject = "{0}已被业务审批驳回，驳回人为{1}。";
        const string ProjectAuditFinancialReject = "{0}已被财务审批驳回，驳回人为{1}。";
        const string ProjectAuditFinancialEdit = "{0}已通过{1}审批，项目号为{2};请您确认项目号申请单内容。";
        const string ProjectSupporterNotify = "{0}已审批通过，项目号为{1},请您进行支持方的申请。";
        const string ProjectAuditContract = "{0}未完成审批，财务部分等待提交正式合同。";
        const string SupporterZH2 = "{0}已提交支持方申请{1}，正等待{2}进行业务审批。";
        const string referSupporterToUser = "{0}已提交支持方申请{1},您为该项目号负责人。";
        const string referSupporterToRec = "{0}已提交支持方申请{1},您被设定为负责人。";

        const string SupporterBizAudit = "{0}已通过{1}业务审批,请您进行{2}。";
        const string SupporterAuditBizReject = "{0}已被业务审批驳回，驳回人为{1}。";
        const string SupporterAuditFinanceReject = "{0}已被财务审批驳回，驳回人为{1}。";
        const string SupporterFinanceAudit = "{0}已通过{1}财务审批,请您进行{2}。";
        const string SupporterAuditComplete = "{0}已完成审批。";

        const string PaymentNotifyFinancial = "{0}付款通知已被{1}变更，请您查看。";
        const string PaymentNotifyBizAudit = "{0}已通过{1}业务审批,请您进行审批。";
        const string PaymentNotifyBizReject = "{0}已被{1}业务审批驳回。";
        const string PaymentNotifyFinanceAudit = "{0}已通过{1}财务审批,请您进行审批。";
        const string PaymentNotifyFinanceReject = "{0}已被{1}财务审批驳回。";
        const string PaymentNotifyComplete = "{0}已完成审批。";

        const string ConsumptionCommit = "{0}申请的消耗{1}批次{2}已经提交，总额为{3}元。请您进行审批。";
        const string ConsumptionAudit = "{0}申请的消耗{1}批次{2}已通过{3}的审批，总额为{4}元。请您进行审批。";
        const string ConsumptionComplete = "{0}申请的消耗{1}批次{2}已通过{3}的审批，总额为{4}元。审批流程已结束。";
        const string ConsumptionReject = "{0}申请的消耗{1}批次{2}已被{3}驳回，总额为{4}元。请查看审批记录。";


        const string RefundAudit = "{0}申请的退款单{1}已通过{2}的审批，总额为{3}元。请您进行审批。";
        const string RefundComplete = "{0}申请的退款单{1}已通过{2}的审批，总额为{3}元。审批流程已结束。";
        const string RefundReject = "{0}申请的退款单{1}已被{2}驳回，总额为{3}元。请查看审批记录。";

        const string RebateRegistrationCommit = "{0}申请的媒体返点导入批次{1}已经提交，总额为{2}元。请您进行审批。";
        const string RebateRegistrationAudit = "{0}申请的媒体返点导入批次{1}已通过{2}的审批，总额为{3}元。请您进行审批。";
        const string RebateRegistrationComplete = "{0}申请的媒体返点导入批次{1}已通过{2}的审批，总额为{3}元。审批流程已结束。";
        const string RebateRegistrationReject = "{0}申请的媒体返点导入批次{1}已被{2}驳回，总额为{3}元。请查看审批记录。";

        const string PreCloseProject = "项目号{0}已经被{1}预关闭,项目号已不能创建采购申请单，请尽快处理完现有申请单，以便关闭该项目号。";
        const string CloseProject = "项目号{0}已经被{1}关闭,项目号已停止一切业务操作。";

        static string WebURL = ESP.Configuration.ConfigurationManager.SafeAppSettings["FinanceMainPage"].Replace("Default.aspx", "");
        #endregion 常量

        /// <summary>
        /// Initializes a new instance of the <see cref="SendMailHelper"/> class.
        /// </summary>
        public SendMailHelper()
        {
            //
            //TODO: 在此处添加构造函数逻辑
            //
        }

        /// <summary>
        /// Gets the pr HTML.
        /// </summary>
        /// <param name="generalId">The general id.</param>
        /// <returns></returns>
        private static string GetPrHtml(int projectid)
        {
            string hostUrl = ESP.Configuration.ConfigurationManager.SafeAppSettings["FinanceMainPage"].Replace("Default.aspx", "");
            string url = hostUrl + "project/ProjectPrint.aspx?" + RequestName.ProjectID + "=" + projectid + "&" + RequestName.Mail + "=no";
            return SendMail.ScreenScrapeHtml(url).Replace("src=\"/images", "src=\"" + hostUrl + "images");
        }

        private static string GetNotifyHtml(int PaymentID)
        {
            string hostUrl = ESP.Configuration.ConfigurationManager.SafeAppSettings["FinanceMainPage"].Replace("Default.aspx", "");
            string url = hostUrl + "Return/NotificationPrint.aspx?" + RequestName.PaymentID + "=" + PaymentID.ToString() + "&" + RequestName.Mail + "=no";
            return SendMail.ScreenScrapeHtml(url).Replace("src=\"/images", "src=\"" + hostUrl + "images");
        }
        private static string GetSupporterHtml(int supportid)
        {
            string hostUrl = ESP.Configuration.ConfigurationManager.SafeAppSettings["FinanceMainPage"].Replace("Default.aspx", "");
            string url = hostUrl + "project/SupporterPrint.aspx?" + RequestName.SupportID + "=" + supportid + "&" + RequestName.Mail + "=no";
            return SendMail.ScreenScrapeHtml(url).Replace("src=\"/images", "src=\"" + hostUrl + "images");
        }

        private static string GetPaymentHtml(int returnId)
        {
            string hostUrl = ESP.Configuration.ConfigurationManager.SafeAppSettings["FinanceMainPage"].Replace("Default.aspx", "");
            string url = hostUrl + "Purchase/Print/PaymantPrint.aspx?" + RequestName.ReturnID + "=" + returnId + "&" + RequestName.Mail + "=no";
            return SendMail.ScreenScrapeHtml(url).Replace("src=\"/images", "src=\"" + hostUrl + "images");
        }

        private static string GetTicketHtml(int returnId, int issupplier)
        {
            string hostUrl = ESP.Configuration.ConfigurationManager.SafeAppSettings["FinanceMainPage"].Replace("Default.aspx", "");
            string url = hostUrl + "ExpenseAccount/Print/TicketMail.aspx?expenseId=" + returnId.ToString() + "&supplier=" + issupplier.ToString();
            return SendMail.ScreenScrapeHtml(url).Replace("src=\"/images", "src=\"" + hostUrl + "images");
        }

        #region "project mail operation"
        /// <summary>
        /// Sends the mail PR.
        /// </summary>
        /// <param name="generalInfo">The general info.</param>
        /// <param name="PrNO">The pr NO.</param>
        /// <param name="ReqName">Name of the req.</param>
        /// <param name="UserEmail">The user email.</param>
        /// <param name="RecEmail">The rec email.</param>
        /// <param name="FirstOperaEmail">The first opera email.</param>
        /// <returns></returns>
        public static string SendMailPR(ESP.Finance.Entity.ProjectInfo projectInfo, string UserEmail, string ResponserEmail, string FirstOperaEmail)
        {
            string ret = "";
            if (!string.IsNullOrEmpty(UserEmail))
            {
                string msgUser = string.Format(referPRtoUser, projectInfo.ApplicantEmployeeName, projectInfo.SerialCode);
                msgUser += "<br />" + GetPrHtml(projectInfo.ProjectId);
                SendMail.Send1("项目号申请待审核", UserEmail, msgUser, true, "");
            }
            else
            {
                ret += "使用人没有邮箱，无法发送邮件。";
            }
            if (!string.IsNullOrEmpty(ResponserEmail))
            {
                string msgRec = string.Format(referPRtoRec, projectInfo.ApplicantEmployeeName, projectInfo.SerialCode);
                msgRec += "<br />" + GetPrHtml(projectInfo.ProjectId);
                SendMail.Send1("项目号申请待审核", ResponserEmail, msgRec, true, "");
            }
            else
            {
                ret += "收货人没有邮箱，无法发送邮件。";
            }
            if (!string.IsNullOrEmpty(FirstOperaEmail))
            {
                string msgOpera = string.Format("{0}已提交项目号申请单{1},请您进行业务审批。", projectInfo.ApplicantEmployeeName, projectInfo.SerialCode);
                msgOpera += "<a href=\"" + WebURL + "/Default.aspx?contentUrl=" + System.Web.HttpUtility.UrlEncode("project\\AuditOperation.aspx?" + RequestName.ProjectID + "=" + projectInfo.ProjectId) + "\">点此进行业务审批</a>";
                msgOpera += "<br />" + GetPrHtml(projectInfo.ProjectId);
                SendMail.Send1("项目号申请待审核", FirstOperaEmail, msgOpera, true, "");
            }
            return ret;
        }

        /// <summary>
        /// 证据链提交待审核
        /// </summary>
        /// <param name="projectInfo">项目号</param>
        /// <param name="UserEmail">审核人邮箱</param>
        /// <returns></returns>
        public static string SendMailCommitEvidence(ESP.Finance.Entity.ProjectInfo projectInfo, string UserEmail)
        {
            string ret = "";
            if (!string.IsNullOrEmpty(UserEmail))
            {// "{0}已提交项目{1}的证据链文件,请您进行审核。";
                string msgUser = string.Format(evidenceCommit, projectInfo.ApplicantEmployeeName, projectInfo.ProjectCode);

                SendMail.Send1("证据链提交待审核", UserEmail, msgUser, true, "");
            }
            else
            {
                ret += "使用人没有邮箱，无法发送邮件。";
            }

            return ret;
        }
        /// <summary>
        /// 证据链审核通过
        /// </summary>
        /// <param name="projectInfo">项目号</param>
        /// <param name="UserEmail">项目负责人</param>
        /// <param name="username">审核人</param>
        /// <returns></returns>
        public static string SendMailConfirmEvidence(string ProjectCode, string UserEmail, string username)
        {
            string ret = "";
            if (!string.IsNullOrEmpty(UserEmail))
            {// "{0}已经审核通过{1}的证据链文件。";
                string msgUser = string.Format(evidenceConfirm, username, ProjectCode);

                SendMail.Send1("证据链审核通过", UserEmail, msgUser, true, "");
            }
            else
            {
                ret += "使用人没有邮箱，无法发送邮件。";
            }

            return ret;
        }
        /// <summary>
        /// 证据链审核驳回
        /// </summary>
        /// <param name="projectInfo">项目号</param>
        /// <param name="UserEmail">项目负责人邮箱</param>
        /// <param name="username">审核人</param>
        /// <returns></returns>
        public static string SendMailRejectEvidence(string ProjectCode, string UserEmail, string username)
        {
            string ret = "";
            if (!string.IsNullOrEmpty(UserEmail))
            {//"{0}已经驳回{1}的证据链文件,请及时更新。";
                string msgUser = string.Format(evidenceReject, username, ProjectCode);

                SendMail.Send1("证据链审核驳回", UserEmail, msgUser, true, "");
            }
            else
            {
                ret += "使用人没有邮箱，无法发送邮件。";
            }

            return ret;
        }

        /// <summary>
        /// 发票申请提交
        /// </summary>
        /// <param name="projectInfo"></param>
        /// <param name="UserEmail"></param>
        /// <returns></returns>
        public static string SendMailCommitApplyForInvioce(ESP.Finance.Entity.ProjectInfo projectInfo, string UserEmail)
        {
            string ret = "";
            if (!string.IsNullOrEmpty(UserEmail))
            {// "{0}已提交项目{1}的证据链文件,请您进行审核。";
                string msgUser = string.Format("{0}已提交项目{1}的发票申请,请您进行审核。", projectInfo.ApplicantEmployeeName, projectInfo.ProjectCode);

                SendMail.Send1("发票申请提交待审核", UserEmail, msgUser, true, "");
            }
            else
            {
                ret += "使用人没有邮箱，无法发送邮件。";
            }

            return ret;
        }

        /// <summary>
        /// 发票申请审核
        /// </summary>
        /// <param name="projectInfo">项目号</param>
        /// <param name="UserEmail">项目负责人邮箱</param>
        /// <param name="username">审核人</param>
        /// <returns></returns>
        public static string SendMailApplyForInvioceAudit(string ProjectCode, string UserEmail, string username, string auditType, string auditRemark)
        {
            string ret = "";
            if (!string.IsNullOrEmpty(UserEmail))
            {//"{0}已经驳回{1}的证据链文件,请及时更新。";
                string msgUser = string.Format("{0}已{1}您项目{2}的发票申请。审核备注：{2}", username, auditType, ProjectCode, auditRemark);

                SendMail.Send1("发票申请审核", UserEmail, msgUser, true, "");
            }
            else
            {
                ret += "使用人没有邮箱，无法发送邮件。";
            }

            return ret;
        }

        /// <summary>
        /// 发送业务审核知会邮件
        /// </summary>
        /// <param name="generalInfo"></param>
        /// <param name="PrNo">pr单号</param>
        /// <param name="auditor"></param>
        /// <param name="nextauditor">下一级审核人</param>
        /// <param name="ZHEmails">知会人邮箱</param>
        public static void SendMailToZH(ESP.Finance.Entity.ProjectInfo projectInfo, string auditor, string nextauditor, string ZHEmails)
        {
            if (!string.IsNullOrEmpty(ZHEmails))
            {
                string msgBody = string.Format(referPROperaZH, projectInfo.SerialCode, auditor, nextauditor);
                msgBody += "<br />" + GetPrHtml(projectInfo.ProjectId);
                SendMail.Send1("项目号申请单业务审核知会", ZHEmails, msgBody, false, "");
            }
        }
        /// <summary>
        /// 发送业务审核知会邮件(当知会人员为第一级时使用)
        /// </summary>
        /// <param name="generalInfo"></param>
        /// <param name="PrNo">pr单号</param>
        /// <param name="auditor">业务审批人</param>
        /// <param name="requestor">申请人</param>
        /// <param name="ZHEmails">知会人邮箱</param>
        public static void SendMailToZH2(ESP.Finance.Entity.ProjectInfo projectInfo, string auditor, string ZHEmails)
        {
            if (!string.IsNullOrEmpty(ZHEmails))
            {
                string msgBody = string.Format(referPR0peraZH2, projectInfo.ApplicantEmployeeName, projectInfo.SerialCode, auditor);
                msgBody += "<br />" + GetPrHtml(projectInfo.ProjectId);
                SendMail.Send1("项目号申请单业务审核知会", ZHEmails, msgBody, false, "");
            }
        }
        public static string SendMailBizOK(ESP.Finance.Entity.ProjectInfo projectInfo, string OpearName, string ReqEmail, string Flag)
        {
            string ret = "";
            string auditPage = string.Empty;
            if (Flag == "Finance")
            {
                auditPage = "project\\FinancialAuditOperation.aspx?";
            }
            else
                auditPage = "project\\AuditOperation.aspx?";
            if (!string.IsNullOrEmpty(ReqEmail))
            {
                string msgLastOpera = string.Format(ProjectApplicantBizAudit, projectInfo.SerialCode, OpearName, "项目审批");
                msgLastOpera += "<a href=\"" + WebURL + "/Default.aspx?contentUrl=" + System.Web.HttpUtility.UrlEncode(auditPage + RequestName.ProjectID + "=" + projectInfo.ProjectId) + "\">点此进行项目审批</a>";
                msgLastOpera += "<br />" + GetPrHtml(projectInfo.ProjectId);
                SendMail.Send1("项目号申请审批通过", ReqEmail, msgLastOpera, false, "");
            }
            return ret;
        }

        public static string AssetReceivingSendMail(string content, string ReqEmail)
        {
            string ret = "";

            if (!string.IsNullOrEmpty(ReqEmail))
            {
                SendMail.Send1("固定资产领用", ReqEmail, content, false, "");
            }
            return ret;
        }

        public static string AssetReturnSendMail(string content, string ReqEmail)
        {
            string ret = "";

            if (!string.IsNullOrEmpty(ReqEmail))
            {
                SendMail.Send1("固定资产归还", ReqEmail, content, false, "");
            }
            return ret;
        }

        public static string SendMailAuditBizReject(ESP.Finance.Entity.ProjectInfo projectInfo, string OperaName, string ReqEmail)
        {
            string ret = "";
            if (!string.IsNullOrEmpty(ReqEmail))
            {
                string msgLastOpera = string.Format(ProjectAuditBizReject, projectInfo.SerialCode, OperaName);
                msgLastOpera += "<br />" + GetPrHtml(projectInfo.ProjectId);
                SendMail.Send1("合同审批业务驳回", ReqEmail, msgLastOpera, false, "");
            }
            return ret;
        }
        public static string SendMailAuditFinancialReject(ESP.Finance.Entity.ProjectInfo projectInfo, string OperaName, string ReqEmail)
        {
            string ret = "";
            if (!string.IsNullOrEmpty(ReqEmail))
            {
                string msgLastOpera = string.Format(ProjectAuditFinancialReject, projectInfo.SerialCode, OperaName);
                msgLastOpera += "<br />" + GetPrHtml(projectInfo.ProjectId);
                SendMail.Send1("合同审批财务驳回", ReqEmail, msgLastOpera, false, "");
            }
            return ret;
        }
        public static string SendMailContractOK(ESP.Finance.Entity.ProjectInfo projectInfo, string OpearName, string ReqEmail)
        {
            string ret = "";
            if (!string.IsNullOrEmpty(ReqEmail))
            {
                string msgLastOpera = string.Format(FinancialContractAudi, projectInfo.SerialCode, OpearName, "财务审批");
                msgLastOpera += "<a href=\"" + WebURL + "/Default.aspx?contentUrl=" + System.Web.HttpUtility.UrlEncode("project\\FinancialAuditOperation.aspx?" + RequestName.ProjectID + "=" + projectInfo.ProjectId) + "\">点此进行业务审批</a>";
                msgLastOpera += "<br />" + GetPrHtml(projectInfo.ProjectId);
                SendMail.Send1("项目号申请合同审批通过", ReqEmail, msgLastOpera, false, "");
            }
            return ret;
        }
        public static string SendMailAuditComplete(ESP.Finance.Entity.ProjectInfo projectInfo, string OpearName, string ReqEmail)
        {
            string ret = "";
            if (!string.IsNullOrEmpty(ReqEmail))
            {
                string msgLastOpera = string.Format(ProjectAuditComplete, projectInfo.SerialCode, projectInfo.ProjectCode);
                msgLastOpera += "<br />" + GetPrHtml(projectInfo.ProjectId);
                SendMail.Send1("项目号申请合同审批完成", ReqEmail, msgLastOpera, false, "");
            }
            return ret;
        }
        public static string SendMailAuditContract(ESP.Finance.Entity.ProjectInfo projectInfo, string OpearName, string ReqEmail)
        {
            string ret = "";
            if (!string.IsNullOrEmpty(ReqEmail))
            {
                string msgLastOpera = string.Format(ProjectAuditContract, projectInfo.SerialCode);
                msgLastOpera += "<br />" + GetPrHtml(projectInfo.ProjectId);
                SendMail.Send1("项目号申请等待正式合同", ReqEmail, msgLastOpera, false, "");
            }
            return ret;
        }
        public static string SendMailToFinancialEdit(ESP.Finance.Entity.ProjectInfo projectInfo, string OpearName, string ReqEmail)
        {
            string ret = "";
            if (!string.IsNullOrEmpty(ReqEmail))
            {
                string msgLastOpera = string.Format(ProjectAuditFinancialEdit, projectInfo.SerialCode, OpearName, projectInfo.ProjectCode);
                msgLastOpera += "<a href=\"" + WebURL + "/Default.aspx?contentUrl=" + System.Web.HttpUtility.UrlEncode("project\\FinancialAuditOperation.aspx?" + RequestName.ProjectID + "=" + projectInfo.ProjectId) + "\">点此进行业务审批</a>";
                msgLastOpera += "<br />" + GetPrHtml(projectInfo.ProjectId);
                SendMail.Send1("项目号申请合同审批完成", ReqEmail, msgLastOpera, false, "");
            }
            return ret;
        }
        #endregion

        #region "supporter mail operation"
        public static string SendMailToSupporter(ESP.Finance.Entity.ProjectInfo projectInfo, string ReqEmail)
        {
            string ret = "";
            if (!string.IsNullOrEmpty(ReqEmail))
            {
                string msgLastOpera = string.Format(ProjectSupporterNotify, projectInfo.SerialCode, projectInfo.ProjectCode);
                // msgLastOpera += "<br />" + GetPrHtml(projectInfo.ProjectId);
                SendMail.Send1("项目号审批完成", ReqEmail, msgLastOpera, false, "");
            }
            return ret;
        }

        public static void SendSupporterMailToZH2(ESP.Finance.Entity.SupporterInfo supporterModel, string auditor, string ZHEmails)
        {
            if (!string.IsNullOrEmpty(ZHEmails))
            {
                string msgBody = string.Format(SupporterZH2, supporterModel.ApplicantEmployeeName, supporterModel.SupporterCode, auditor);
                msgBody += "<br />" + GetSupporterHtml(supporterModel.SupportID);
                SendMail.Send1("支持方申请业务审核知会", ZHEmails, msgBody, false, "");
            }
        }

        public static string SendSupporterMail(ESP.Finance.Entity.SupporterInfo supporterModel, string UserEmail, string ResponserEmail, string FirstOperaEmail)
        {
            string ret = "";
            if (!string.IsNullOrEmpty(UserEmail))
            {
                string msgUser = string.Format(referSupporterToUser, supporterModel.ApplicantEmployeeName, supporterModel.SupporterCode);
                msgUser += "<br />" + GetSupporterHtml(supporterModel.SupportID);
                SendMail.Send1("支持方申请待审核", UserEmail, msgUser, true, "");
            }
            else
            {
                ret += "使用人没有邮箱，无法发送邮件。";
            }
            if (!string.IsNullOrEmpty(ResponserEmail))
            {
                string msgRec = string.Format(referSupporterToRec, supporterModel.ApplicantEmployeeName, supporterModel.SupporterCode);
                msgRec += "<br />" + GetSupporterHtml(supporterModel.SupportID);
                SendMail.Send1("支持方申请待审核", ResponserEmail, msgRec, true, "");
            }
            else
            {
                ret += "收货人没有邮箱，无法发送邮件。";
            }
            if (!string.IsNullOrEmpty(FirstOperaEmail))
            {
                string msgRec = "<a href=\"" + WebURL + "/Default.aspx?contentUrl=" + System.Web.HttpUtility.UrlEncode("project\\SupporterAuditOperation.aspx?" + RequestName.ProjectID + "=" + supporterModel.ProjectID + "&" + RequestName.SupportID + "=" + supporterModel.SupportID) + "\">点此进行业务审批</a>";

                string msgOpera = string.Format("{0}已提交项目号申请单{1},请您进行业务审批,{2}。", supporterModel.ApplicantEmployeeName, supporterModel.SupporterCode, msgRec);

                msgOpera += "<br />" + GetSupporterHtml(supporterModel.SupportID);
                SendMail.Send1("支持方待审核", FirstOperaEmail, msgOpera, true, "");
            }
            return ret;
        }

        public static void SendSupporterMailToZH(ESP.Finance.Entity.SupporterInfo supporterModel, string auditor, string nextauditor, string ZHEmails)
        {
            if (!string.IsNullOrEmpty(ZHEmails))
            {
                string msgBody = string.Format(referPROperaZH, supporterModel.SupporterCode, auditor, nextauditor);
                msgBody += "<br />" + GetSupporterHtml(supporterModel.SupportID);
                SendMail.Send1("支持方业务审核知会", ZHEmails, msgBody, false, "");
            }
        }

        public static string SendSupporterMailBizOK(ESP.Finance.Entity.SupporterInfo supporterModel, string OpearName, string ReqEmail, string Flag)
        {
            string ret = "";
            string auditPage = string.Empty;
            if (Flag == "Finance")
            {
                auditPage = "project\\FinancialSupporter.aspx?";
            }
            else
                auditPage = "project\\SupporterAuditOperation.aspx?";

            if (!string.IsNullOrEmpty(ReqEmail))
            {
                string msgLastOpera = string.Format(SupporterBizAudit, supporterModel.SupporterCode, OpearName, "支持方申请业务审批");
                msgLastOpera += "<a href=\"" + WebURL + "/Default.aspx?contentUrl=" + System.Web.HttpUtility.UrlEncode(auditPage + RequestName.ProjectID + "=" + supporterModel.ProjectID + "&" + RequestName.SupportID + "=" + supporterModel.SupportID) + "\">点此进行业务审批</a>";
                msgLastOpera += "<br />" + GetSupporterHtml(supporterModel.SupportID);
                SendMail.Send1("支持方业务审批通过", ReqEmail, msgLastOpera, false, "");
            }
            return ret;
        }

        public static string SendSupporterMailFinanceOK(ESP.Finance.Entity.SupporterInfo supporterModel, string OpearName, string ReqEmail)
        {
            string ret = "";
            if (!string.IsNullOrEmpty(ReqEmail))
            {
                string msgLastOpera = string.Format(SupporterFinanceAudit, supporterModel.SupporterCode, OpearName, "支持方申请财务审批");
                msgLastOpera += "<a href=\"" + WebURL + "/Default.aspx?contentUrl=" + System.Web.HttpUtility.UrlEncode("project\\FinancialSupporter.aspx?" + RequestName.ProjectID + "=" + supporterModel.ProjectID + "&" + RequestName.SupportID + "=" + supporterModel.SupportID) + "\">点此进行财务审批</a>";
                msgLastOpera += "<br />" + GetSupporterHtml(supporterModel.SupportID);
                SendMail.Send1("支持方财务审批通过", ReqEmail, msgLastOpera, false, "");
            }
            return ret;
        }

        public static string SendMailSupporterComplete(ESP.Finance.Entity.SupporterInfo supporterModel, string OpearName, string ReqEmail)
        {
            string ret = "";
            if (!string.IsNullOrEmpty(ReqEmail))
            {
                string msgLastOpera = string.Format(SupporterAuditComplete, supporterModel.SupporterCode);
                msgLastOpera += "<br />" + GetSupporterHtml(supporterModel.SupportID);
                SendMail.Send1("支持方财务审批完成", ReqEmail, msgLastOpera, false, "");
            }
            return ret;
        }

        public static string SendSupporterMailAuditFinancialReject(ESP.Finance.Entity.SupporterInfo supporterModel, string OperaName, string ReqEmail)
        {
            string ret = "";
            if (!string.IsNullOrEmpty(ReqEmail))
            {
                string msgLastOpera = string.Format(SupporterAuditFinanceReject, supporterModel.SupporterCode, OperaName);
                msgLastOpera += "<br />" + GetSupporterHtml(supporterModel.SupportID);
                SendMail.Send1("支持方财务审批驳回", ReqEmail, msgLastOpera, false, "");
            }
            return ret;
        }

        public static string SendSupporterMailAuditBizReject(ESP.Finance.Entity.SupporterInfo supporterModel, string OperaName, string ReqEmail)
        {
            string ret = "";
            if (!string.IsNullOrEmpty(ReqEmail))
            {
                string msgLastOpera = string.Format(SupporterAuditBizReject, supporterModel.SupporterCode, OperaName);
                msgLastOpera += "<br />" + GetSupporterHtml(supporterModel.SupportID);
                SendMail.Send1("支持方业务审批驳回", ReqEmail, msgLastOpera, false, "");
            }
            return ret;
        }
        #endregion

        /// <summary>
        /// 发送业务审核知会邮件
        /// </summary>
        /// <param name="generalInfo"></param>
        /// <param name="PrNo">pr单号</param>
        /// <param name="auditor"></param>
        /// <param name="nextauditor">下一级审核人</param>
        /// <param name="ZHEmails">知会人邮箱</param>
        public static void SendMailToZHFK(ESP.Finance.Entity.ReturnInfo returnModel, string returnCode, string auditor, string nextauditor, string ZHEmails)
        {
            if (!string.IsNullOrEmpty(ZHEmails))
            {
                string msgBody = string.Format(referPROperaZHFK, returnCode, auditor, nextauditor);
                msgBody += "<br />" + GetPaymentHtml(returnModel.ReturnID);
                SendMail.Send1("付款业务审批知会", ZHEmails, msgBody, false, "");
            }
        }

        /// <summary>
        /// 发送业务审核知会邮件(当知会人员为第一级时使用)
        /// </summary>
        /// <param name="generalInfo"></param>
        /// <param name="PrNo">pr单号</param>
        /// <param name="auditor">业务审批人</param>
        /// <param name="requestor">申请人</param>
        /// <param name="ZHEmails">知会人邮箱</param>
        public static void SendMailToZH2FK(ESP.Finance.Entity.ReturnInfo returnModel, string returnCode, string requestor, string auditor, string ZHEmails)
        {
            if (!string.IsNullOrEmpty(ZHEmails))
            {
                string msgBody = string.Format(referPR0peraZH2FK, requestor, returnCode, auditor);
                msgBody += "<br />" + GetPaymentHtml(returnModel.ReturnID);
                SendMail.Send1("付款业务审批知会", ZHEmails, msgBody, false, "");
            }
        }
        public static string SendMailReturnCommit(ESP.Finance.Entity.ReturnInfo returnModel, string commitName, string FirstAuditName, string ReqEmail, string NextOperaEmail)
        {
            string ret = "";
            if (!string.IsNullOrEmpty(ReqEmail))
            {
                string msgFirstOpera = string.Format(ReturnCommit, returnModel.ReturnCode, commitName);
                msgFirstOpera += "<br />" + GetPaymentHtml(returnModel.ReturnID);
                SendMail.Send1("付款申请提交完成", ReqEmail, msgFirstOpera, false, "");
            }
            if (!string.IsNullOrEmpty(NextOperaEmail))
            {
                string msgNextOpera = string.Format("{0}已由{1}提交完成，请您进行付款业务审批。", returnModel.ReturnCode, commitName);
                msgNextOpera += "<a href=\"" + WebURL + "/Default.aspx?contentUrl=" + System.Web.HttpUtility.UrlEncode("Purchase\\PaymentEdit.aspx?" + RequestName.ReturnID + "=" + returnModel.ReturnID) + "\">点此进行业务审批</a>";
                msgNextOpera += "<br />" + GetPaymentHtml(returnModel.ReturnID);
                SendMail.Send1("付款申请提交完成", NextOperaEmail, msgNextOpera, false, "");
            }

            return ret;
        }

        public static string SendMailRefundCommit(ESP.Finance.Entity.RefundInfo refundModel, string commitName, string FirstAuditName, string ReqEmail, string NextOperaEmail)
        {
            string ret = "";
            if (!string.IsNullOrEmpty(ReqEmail))
            {
                string msgFirstOpera = string.Format(ReturnCommit, refundModel.RefundCode, commitName);
                
                SendMail.Send1("退款申请提交完成", ReqEmail, msgFirstOpera, false, "");
            }
            if (!string.IsNullOrEmpty(NextOperaEmail))
            {
                string msgNextOpera = string.Format("{0}已由{1}提交完成，请您进行退款业务审批。", refundModel.RefundCode, commitName);

                SendMail.Send1("退款申请提交完成", NextOperaEmail, msgNextOpera, false, "");
            }

            return ret;
        }

        public static string SendMailPRFirstOperaPassFKCC(ESP.Finance.Entity.ReturnInfo returnModel, string auditorName, string CCMail)
        {
            string ret = "";

            if (!string.IsNullOrEmpty(CCMail))
            {//{0}于{1}审核通过{2}付款申请，供应商为{3}，申请金额{4},请关注。
                string msgFirstOpera = string.Format(referPRFirstOperaPasstoReqFKCC, auditorName, DateTime.Now.ToString("yyyy-MM-dd"), returnModel.ReturnCode, returnModel.SupplierName, returnModel.PreFee.Value.ToString("#,##0.00"));
                msgFirstOpera += "<br />" + GetPaymentHtml(returnModel.ReturnID);
                SendMail.Send1("付款业务审批通过", CCMail, msgFirstOpera, false, "");
            }

            return ret;
        }


        public static string SendMailOOPCC(ESP.Finance.Entity.ReturnInfo returnModel, string auditorName, string CCMail)
        {
            string ret = "";

            if (!string.IsNullOrEmpty(CCMail))
            {//{0}于{1}审核通过{2}付款申请，申请金额{4},请关注。
                string msgFirstOpera = string.Format(referOOPCC, auditorName, DateTime.Now.ToString("yyyy-MM-dd"), returnModel.ReturnCode, returnModel.PreFee.Value.ToString("#,##0.00"));
                msgFirstOpera += "<br />" + GetPaymentHtml(returnModel.ReturnID);
                SendMail.Send1("报销申请审批通过", CCMail, msgFirstOpera, false, "");
            }

            return ret;
        }

        public static string SendMailPRFirstOperaPassFK(ESP.Finance.Entity.ReturnInfo returnModel, string returnCode, string FirstOperaName, string NextOperaName, string ReqEmail, string NextOperaEmail, int OperaCount, string Flag)
        {
            string ret = "";
            string auditPage = string.Empty;
            if (Flag == "Finance")
            {
                auditPage = "Purchase\\FinancialAudit.aspx?";
            }
            else
                auditPage = "Purchase\\PaymentEdit.aspx?";


            if (!string.IsNullOrEmpty(ReqEmail))
            {
                string msgFirstOpera = string.Format(referPRFirstOperaPasstoReqFK, returnCode, FirstOperaName, NextOperaName);
                msgFirstOpera += "<br />" + GetPaymentHtml(returnModel.ReturnID);
                SendMail.Send1("付款业务审批通过", ReqEmail, msgFirstOpera, false, "");
            }
            if (!string.IsNullOrEmpty(NextOperaEmail))
            {
                string msgNextOpera = "";
                if (Flag != "Purchase")
                {
                    msgNextOpera = string.Format("{0}已通过{1}付款业务审批，请您进行付款业务审批。", returnCode, FirstOperaName);
                    msgNextOpera += "<a href=\"" + WebURL + "/Default.aspx?contentUrl=" + System.Web.HttpUtility.UrlEncode(auditPage + RequestName.ReturnID + "=" + returnModel.ReturnID) + "\">点此进行业务审批</a>";
                    msgNextOpera += "<br />" + GetPaymentHtml(returnModel.ReturnID);

                }
                else
                {
                    msgNextOpera = string.Format("{0}已通过{1}付款业务审批，请您进行采购审批。", returnCode, FirstOperaName);
                    msgNextOpera += "<br />" + GetPaymentHtml(returnModel.ReturnID);
                }
                SendMail.Send1("付款业务审批通过", NextOperaEmail, msgNextOpera, false, "");
            }

            return ret;
        }

        /// <summary>
        /// 采购审批发信
        /// </summary>
        /// <param name="auditOk"></param>
        /// <param name="isLast"></param>
        /// <param name="returnId"></param>
        /// <param name="returnCode"></param>
        /// <param name="FirstOperaName"></param>
        /// <param name="NextOperaName"></param>
        /// <param name="ReqEmail"></param>
        /// <param name="NextOperaEmail"></param>
        public static void SendMailReturnForPurchaseAduit(bool auditOk, bool isLast, int returnId, string returnCode, string FirstOperaName, string NextOperaName, string ReqEmail, string NextOperaEmail)
        {
            if (auditOk)
            {
                if (!string.IsNullOrEmpty(ReqEmail))//申请人
                {
                    string msgFirstOpera = returnCode + "已通过" + FirstOperaName + "的采购审批,请等待" + NextOperaName + "进行" + (isLast == true ? "付款财务审批" : "付款申请审批") + ".";
                    msgFirstOpera += "<br />" + GetPaymentHtml(returnId);
                    SendMail.Send1("付款申请审批", ReqEmail, msgFirstOpera, false, "");
                }
                if (!string.IsNullOrEmpty(NextOperaEmail))//下一级审核人
                {
                    string msgFirstOpera = returnCode + "已通过" + FirstOperaName + "的采购审批,请您进行" + (isLast == true ? "付款财务审批" : "付款申请审批") + ".";
                    msgFirstOpera += "<br />" + GetPaymentHtml(returnId);
                    SendMail.Send1("付款申请审批", NextOperaEmail, msgFirstOpera, false, "");
                }
            }
        }

        /// <summary>
        /// 采购批次审批
        /// </summary>
        /// <param name="auditOk"></param>
        /// <param name="isLast"></param>
        /// <param name="FirstOperaName"></param>
        /// <param name="NextOperaName"></param>
        /// <param name="NextOperaEmail"></param>
        public static void SendMailPurchaseBatch(bool auditOk, bool isLast, string NextOperaName, string NextOperaEmail, ESP.Compatible.Employee currentUser, ESP.Finance.Entity.PNBatchInfo batchModel)
        {
            string mails = string.Empty;

            if (auditOk && !isLast)
            {
                if (!string.IsNullOrEmpty(NextOperaEmail))//下一级审核人
                {
                    string openPageUrl = ESP.Configuration.ConfigurationManager.SafeAppSettings["FinanceMainPage"] + "?contentUrl=" + System.Web.HttpContext.Current.Server.UrlEncode("/purchase/BatchPurchase.aspx?" + ESP.Finance.Utility.RequestName.BatchID + "=" + batchModel.BatchID);
                    string Msg = batchModel.PurchaseBatchCode + "已通过" + currentUser.Name + "的采购审批,请您进行采购批次付款申请审批，<a href='" + openPageUrl + "'>点击进行批次付款申请审批</a>";
                    mails = NextOperaEmail;

                    ESP.Compatible.Employee creator = new Compatible.Employee(batchModel.CreatorID.Value);


                    if (creator != null && !string.IsNullOrEmpty(creator.EMail) && batchModel.Status == (int)PaymentStatus.MajorAudit)
                    {
                        mails += "," + creator.EMail;
                    }
                    SendMail.Send1("批次付款申请审批", mails, Msg, false, "");
                }
            }
        }

        public static string SendMailReturnFinanceOK(ESP.Finance.Entity.ReturnInfo returnModel, string FirstOperaName, string NextOperaName, string NextOperaEmail)
        {
            string ret = "";
            if (!string.IsNullOrEmpty(NextOperaEmail))
            {
                string msgNextOpera = string.Format("{0}已通过{1}付款财务审批，请您进行付款财务审批。", returnModel.ReturnCode, FirstOperaName);
                msgNextOpera += "<a href=\"" + WebURL + "/Default.aspx?contentUrl=" + System.Web.HttpUtility.UrlEncode("Purchase\\FinancialAudit.aspx?" + RequestName.ReturnID + "=" + returnModel.ReturnID) + "\">点此进行业务审批</a>";
                msgNextOpera += "<br />" + GetPaymentHtml(returnModel.ReturnID);
                SendMail.Send1("付款财务审批通过", NextOperaEmail, msgNextOpera, false, "");
            }

            return ret;
        }

        public static string SendMailFactoringNotify(int returnid, string returnCode, string operaName, string notifyTime, string operaEmail)
        {
            string ret = "";
            if (!string.IsNullOrEmpty(operaEmail))
            {
                string msgNextOpera = string.Format("{0}按保理流程操作，预计付款时间为{1}。{2}为当前审批人，请您及时进行付款财务审批。", returnCode, notifyTime, operaName);
                msgNextOpera += "<a href=\"" + WebURL + "/Default.aspx?contentUrl=" + System.Web.HttpUtility.UrlEncode("Purchase\\FinancialAudit.aspx?" + RequestName.ReturnID + "=" + returnid) + "\">点此进行审批</a>";
                msgNextOpera += "<br />" + GetPaymentHtml(returnid);
                SendMail.Send1("保理付款流程提醒", operaEmail, msgNextOpera, false, "");
            }

            return ret;
        }

        public static string SendMailReturnReject(ESP.Finance.Entity.ReturnInfo returnModel, string FinanceName, string RequestorName, string RequestorEMail)
        {
            string ret = "";
            if (!string.IsNullOrEmpty(RequestorEMail))
            {
                string msgNextOpera = string.Format("{0}已被{1}审批驳回，请您重新进行提交。", returnModel.ReturnCode, FinanceName);
                if (returnModel.ReturnType != (int)ESP.Purchase.Common.PRTYpe.CommonPR)//媒介的单子
                {
                    if (!string.IsNullOrEmpty(returnModel.MediaOrderIDs))
                    {
                        List<ESP.Purchase.Entity.MediaOrderInfo> mediaList = ESP.Purchase.BusinessLogic.MediaOrderManager.GetModelList(" MeidaOrderID in(" + returnModel.MediaOrderIDs + ")");
                        if (mediaList != null && mediaList.Count > 0)
                        {
                            ESP.Purchase.Entity.OrderInfo orderModel = ESP.Purchase.BusinessLogic.OrderInfoManager.GetModel(mediaList[0].OrderID.Value);
                            ESP.Purchase.Entity.GeneralInfo generalModel = ESP.Purchase.BusinessLogic.GeneralInfoManager.GetModel(orderModel.general_id);
                            ESP.Compatible.Employee emp = new ESP.Compatible.Employee(generalModel.requestor);
                            string mediaInfo = "付款申请记者信息：<br/>";
                            foreach (ESP.Purchase.Entity.MediaOrderInfo m in mediaList)
                            {
                                mediaInfo += m.MediaName + "    " + m.ReporterName + "    " + m.TotalAmount.Value.ToString("#,##0.00") + "<br/>";
                            }
                            msgNextOpera += "<br />" + mediaInfo;
                            msgNextOpera += "<br />" + GetPaymentHtml(returnModel.ReturnID);
                            if (!string.IsNullOrEmpty(emp.EMail))
                                SendMail.Send1("付款财务审批驳回", emp.EMail, msgNextOpera, false, "");
                        }
                    }
                }
                msgNextOpera += "<br />" + GetPaymentHtml(returnModel.ReturnID);
                SendMail.Send1("付款财务审批驳回", RequestorEMail, msgNextOpera, false, "");
            }

            return ret;
        }
        public static string SendMailReturnReDo(ESP.Finance.Entity.ReturnInfo returnModel, string FinanceName, string RequestorName, string RequestorEMail)
        {
            string ret = "";
            if (!string.IsNullOrEmpty(RequestorEMail))
            {
                string msgNextOpera = string.Format("{0}已被银行退回，{1}要求您进行银行信息确认，请您重新进行提交。", returnModel.ReturnCode, FinanceName);
                if (returnModel.ReturnType != (int)ESP.Purchase.Common.PRTYpe.CommonPR)//媒介的单子
                {
                    if (!string.IsNullOrEmpty(returnModel.MediaOrderIDs))
                    {
                        List<ESP.Purchase.Entity.MediaOrderInfo> mediaList = ESP.Purchase.BusinessLogic.MediaOrderManager.GetModelList(" MeidaOrderID in(" + returnModel.MediaOrderIDs + ")");
                        if (mediaList != null && mediaList.Count > 0)
                        {
                            ESP.Purchase.Entity.OrderInfo orderModel = ESP.Purchase.BusinessLogic.OrderInfoManager.GetModel(mediaList[0].OrderID.Value);
                            ESP.Purchase.Entity.GeneralInfo generalModel = ESP.Purchase.BusinessLogic.GeneralInfoManager.GetModel(orderModel.general_id);
                            ESP.Compatible.Employee emp = new ESP.Compatible.Employee(generalModel.requestor);
                            string mediaInfo = "付款申请记者信息：<br/>";
                            foreach (ESP.Purchase.Entity.MediaOrderInfo m in mediaList)
                            {
                                mediaInfo += m.MediaName + "    " + m.ReporterName + "    " + m.TotalAmount.Value.ToString("#,##0.00") + "<br/>";
                            }
                            msgNextOpera += "<br />" + mediaInfo;
                            msgNextOpera += "<br />" + GetPaymentHtml(returnModel.ReturnID);
                            if (!string.IsNullOrEmpty(emp.EMail))
                                SendMail.Send1("付款财务重汇", emp.EMail, msgNextOpera, false, "");
                        }
                    }
                }
                msgNextOpera += "<br />" + GetPaymentHtml(returnModel.ReturnID);
                SendMail.Send1("付款财务重汇", RequestorEMail, msgNextOpera, false, "");
            }

            return ret;
        }

        public static string SendMailBatchRepay(ESP.Finance.Entity.PNBatchInfo batchModel, string FinanceName, string RequestorName, string RequestorEMail)
        {
            string ret = "";
            if (!string.IsNullOrEmpty(RequestorEMail))
            {
                string msgNextOpera = string.Format("{0}已被银行退回，{1}要求您进行银行信息确认，请您重新进行提交。", batchModel.PurchaseBatchCode, FinanceName);

                msgNextOpera += "<br />" + GetPaymentHtml(batchModel.BatchID);
                SendMail.Send1("批次付款财务重汇", RequestorEMail, msgNextOpera, false, "");
            }

            return ret;
        }

        public static string SendMediaToOriginal(ESP.Finance.Entity.ReturnInfo returnModel)
        {
            string ret = "";
            string msgLastOpera = string.Format(SupporterAuditComplete, returnModel.ReturnCode);
            if (returnModel.ReturnType != (int)ESP.Purchase.Common.PRTYpe.CommonPR)//媒介的单子
            {
                if (!string.IsNullOrEmpty(returnModel.MediaOrderIDs))
                {
                    List<ESP.Purchase.Entity.MediaOrderInfo> mediaList = ESP.Purchase.BusinessLogic.MediaOrderManager.GetModelList(" MeidaOrderID in(" + returnModel.MediaOrderIDs + ")");
                    if (mediaList != null && mediaList.Count > 0)
                    {
                        ESP.Purchase.Entity.OrderInfo orderModel = ESP.Purchase.BusinessLogic.OrderInfoManager.GetModel(mediaList[0].OrderID.Value);
                        ESP.Purchase.Entity.GeneralInfo generalModel = ESP.Purchase.BusinessLogic.GeneralInfoManager.GetModel(orderModel.general_id);
                        ESP.Compatible.Employee emp = new ESP.Compatible.Employee(generalModel.requestor);
                        string mediaInfo = "付款申请记者信息：<br/>";
                        foreach (ESP.Purchase.Entity.MediaOrderInfo m in mediaList)
                        {
                            mediaInfo += m.MediaName + "    " + m.ReporterName + "    " + m.TotalAmount.Value.ToString("#,##0.00") + "<br/>";
                        }
                        msgLastOpera += "<br />" + mediaInfo;
                        msgLastOpera += "<br />" + GetPaymentHtml(returnModel.ReturnID);
                        if (!string.IsNullOrEmpty(emp.EMail))
                            SendMail.Send1("付款申请审批完成，财务已付款。", emp.EMail, msgLastOpera, false, "");
                    }
                }
            }
            return ret;
        }
        public static string SendMailReturnComplete(ESP.Finance.Entity.ReturnInfo returnModel, string OpearName, string ReqEmail)
        {
            string ret = "";
            if (!string.IsNullOrEmpty(ReqEmail))
            {
                string msgLastOpera = string.Format(SupporterAuditComplete, returnModel.ReturnCode);
                if (returnModel.ReturnType != (int)ESP.Purchase.Common.PRTYpe.CommonPR)//媒介的单子
                {
                    if (!string.IsNullOrEmpty(returnModel.MediaOrderIDs) && (returnModel.ReturnType == (int)ESP.Purchase.Common.PRTYpe.MediaPR || returnModel.ReturnType == (int)ESP.Purchase.Common.PRTYpe.PR_MediaFA))
                    {
                        List<ESP.Purchase.Entity.MediaOrderInfo> mediaList = ESP.Purchase.BusinessLogic.MediaOrderManager.GetModelList(" MeidaOrderID in(" + returnModel.MediaOrderIDs + ")");
                        var paidList = from media in mediaList where media.IsPayment == 1 select media;
                        var unPay = from media in mediaList where media.IsPayment == 0 select media;
                        List<ESP.Purchase.Entity.MediaOrderInfo> PaidMediaList = paidList.ToList();
                        List<ESP.Purchase.Entity.MediaOrderInfo> UnPayMediaList = unPay.ToList();
                        //已付款记者
                        if (PaidMediaList != null && PaidMediaList.Count > 0)
                        {
                            ESP.Purchase.Entity.OrderInfo orderModel = ESP.Purchase.BusinessLogic.OrderInfoManager.GetModel(mediaList[0].OrderID.Value);
                            ESP.Purchase.Entity.GeneralInfo generalModel = ESP.Purchase.BusinessLogic.GeneralInfoManager.GetModel(orderModel.general_id);
                            ESP.Compatible.Employee emp = new ESP.Compatible.Employee(generalModel.requestor);
                            string mediaInfo = "已付款记者信息：<br/>";
                            foreach (ESP.Purchase.Entity.MediaOrderInfo m in mediaList)
                            {
                                mediaInfo += m.MediaName + "    " + m.ReporterName + "    " + m.TotalAmount.Value.ToString("#,##0.00") + "<br/>";
                            }
                            msgLastOpera += "<br />" + mediaInfo;
                            msgLastOpera += "<br />" + GetPaymentHtml(returnModel.ReturnID);
                            if (!string.IsNullOrEmpty(emp.EMail))
                                SendMail.Send1("付款申请审批完成", emp.EMail, msgLastOpera, false, "");
                        }
                        //未付款记者
                        msgLastOpera = string.Format(SupporterAuditComplete, returnModel.ReturnCode);
                        if (UnPayMediaList != null && UnPayMediaList.Count > 0)
                        {
                            ESP.Purchase.Entity.OrderInfo orderModel = ESP.Purchase.BusinessLogic.OrderInfoManager.GetModel(mediaList[0].OrderID.Value);
                            ESP.Purchase.Entity.GeneralInfo generalModel = ESP.Purchase.BusinessLogic.GeneralInfoManager.GetModel(orderModel.general_id);
                            ESP.Compatible.Employee emp = new ESP.Compatible.Employee(generalModel.requestor);
                            string mediaInfo = "自收到邮件之日起一周内，以下记者费用将会付出：<br/>";
                            foreach (ESP.Purchase.Entity.MediaOrderInfo m in UnPayMediaList)
                            {
                                mediaInfo += m.MediaName + "    " + m.ReporterName + "    " + m.TotalAmount.Value.ToString("#,##0.00") + "<br/>";
                            }
                            msgLastOpera += "<br />" + mediaInfo;
                            msgLastOpera += "<br />" + GetPaymentHtml(returnModel.ReturnID);
                            if (!string.IsNullOrEmpty(emp.EMail))
                                SendMail.Send1("付款申请审批完成", emp.EMail, msgLastOpera, false, "");
                        }
                    }
                }
                else
                {
                    msgLastOpera = string.Format(SupporterAuditComplete, returnModel.ReturnCode);
                    msgLastOpera += "<br />" + GetPaymentHtml(returnModel.ReturnID);
                    SendMail.Send1("付款申请审批完成，财务已付款。", ReqEmail, msgLastOpera, false, "");
                }
            }
            return ret;
        }
        public static string SendMailPROperaOverruleFK(ESP.Finance.Entity.ReturnInfo returnModel, string returnCode, string OperaName, string ReqEmail)
        {
            string ret = "";
            if (!string.IsNullOrEmpty(ReqEmail))
            {
                string msgOverrule = string.Format(referPROperaOverruletoReqFK, returnCode, OperaName);
                msgOverrule += "<br />" + GetPaymentHtml(returnModel.ReturnID);
                SendMail.Send1("付款业务审批驳回", ReqEmail, msgOverrule, false, "");
            }
            return ret;
        }

        public static string SendMailPaymentNotify(ESP.Finance.Entity.ProjectInfo ProjectModel, string FirstOperaName, string NextOperaName, string NextOperaEmail)
        {
            string ret = "";
            if (!string.IsNullOrEmpty(NextOperaEmail))
            {
                string msgFirstOpera = string.Format(PaymentNotifyFinancial, ProjectModel.ProjectCode, FirstOperaName);
                msgFirstOpera += "<br />" + GetPrHtml(ProjectModel.ProjectId);
                SendMail.Send1("付款通知变更", NextOperaEmail, msgFirstOpera, false, "");
            }
            return ret;
        }

        public static string SendMailConsumptionCommit(ESP.Finance.Entity.PNBatchInfo batchModel, string nextAuditorMail)
        {
            string ret = "";
            string adj = "导入";
            if (batchModel.PeriodID != null && batchModel.PeriodID == 1)
                adj = "调整";
            if (!string.IsNullOrEmpty(nextAuditorMail))
            {
                string msgFirstOpera = string.Format(ConsumptionCommit, batchModel.Creator,adj, batchModel.PurchaseBatchCode, batchModel.Amounts.Value.ToString("#,##0.00"));
                SendMail.Send1("消耗批次提交", nextAuditorMail, msgFirstOpera, false, "");
            }
            return ret;
        }

        public static string SendMailConsumptionAudit(ESP.Finance.Entity.PNBatchInfo batchModel, string CreatorEmail, string currentRoleName, string nextAuditorMail)
        {
            string ret = "";
            string adj = "导入";
            if (batchModel.PeriodID != null && batchModel.PeriodID == 1)
                adj = "调整";
            string msgFirstOpera = string.Empty;
            List<MailAddress> mailAddressList = new List<MailAddress>();
            mailAddressList.Add(new MailAddress(CreatorEmail));

            if (batchModel.Status == (int)ESP.Finance.Utility.PaymentStatus.Rejected)
            {//{0}申请的消耗导入批次{1}已被{2}驳回，总额为{3}元。请查看审批记录
                msgFirstOpera = string.Format(ConsumptionReject, batchModel.Creator,adj, batchModel.PurchaseBatchCode, currentRoleName, batchModel.Amounts.Value.ToString("#,##0.00"));
            }
            else if (batchModel.Status == (int)ESP.Finance.Utility.PaymentStatus.FinanceComplete)
            {
                msgFirstOpera = string.Format(ConsumptionComplete,adj, batchModel.Creator, batchModel.PurchaseBatchCode, currentRoleName, batchModel.Amounts.Value.ToString("#,##0.00"));
            }
            else
            {
                if (!string.IsNullOrEmpty(nextAuditorMail))
                {
                    mailAddressList.Add(new MailAddress(nextAuditorMail));

                    msgFirstOpera = string.Format(ConsumptionAudit, batchModel.Creator,adj, batchModel.PurchaseBatchCode, currentRoleName, batchModel.Amounts.Value.ToString("#,##0.00"));
                }
            }
            ESP.Mail.MailManager.Send("消耗批次审批", msgFirstOpera, true, null, mailAddressList.ToArray(), null, null, null);

            return ret;
        }



        public static string SendMailRefundAudit(ESP.Finance.Entity.RefundInfo refundModel, string CreatorEmail, string currentRoleName, string nextAuditorMail)
        {
            string ret = "";
            string msgFirstOpera = string.Empty;
            List<MailAddress> mailAddressList = new List<MailAddress>();
            mailAddressList.Add(new MailAddress(CreatorEmail));

            if (refundModel.Status == (int)ESP.Finance.Utility.PaymentStatus.Rejected)
            {//{0}申请的退款单{1}已被{2}驳回，总额为{3}元。请查看审批记录。
                msgFirstOpera = string.Format(RefundReject, refundModel.RequestEmployeeName, refundModel.RefundCode, currentRoleName, refundModel.Amounts.ToString("#,##0.00"));
            }
            else if (refundModel.Status == (int)ESP.Finance.Utility.PaymentStatus.FinanceComplete)
            {
                msgFirstOpera = string.Format(RefundComplete, refundModel.RequestEmployeeName, refundModel.RefundCode, currentRoleName, refundModel.Amounts.ToString("#,##0.00"));
            }
            else
            {
                if (!string.IsNullOrEmpty(nextAuditorMail))
                {
                    mailAddressList.Add(new MailAddress(nextAuditorMail));

                    msgFirstOpera = string.Format(RefundAudit, refundModel.RequestEmployeeName, refundModel.RefundCode, currentRoleName, refundModel.Amounts.ToString("#,##0.00"));
                }
            }
            ESP.Mail.MailManager.Send("退款申请审批", msgFirstOpera, true, null, mailAddressList.ToArray(), null, null, null);

            return ret;
        }

        public static string SendMailRebateRegistrationCommit(ESP.Finance.Entity.PNBatchInfo batchModel, string nextAuditorMail)
        {
            string ret = "";
            if (!string.IsNullOrEmpty(nextAuditorMail))
            {
                string msgFirstOpera = string.Format(RebateRegistrationCommit, batchModel.Creator, batchModel.PurchaseBatchCode, batchModel.Amounts.Value.ToString("#,##0.00"));

                SendMail.Send1("媒体返点批次导入提交", nextAuditorMail, msgFirstOpera, false, "");
            }
            return ret;
        }

        public static string SendMailRebateRegistrationAudit(ESP.Finance.Entity.PNBatchInfo batchModel, string CreatorEmail, string currentRoleName, string nextAuditorMail)
        {
            string ret = "";
            string msgFirstOpera = string.Empty;
            List<MailAddress> mailAddressList = new List<MailAddress>();
            mailAddressList.Add(new MailAddress(CreatorEmail));

            if (batchModel.Status == (int)ESP.Finance.Utility.PaymentStatus.Rejected)
            {//{0}申请的消耗导入批次{1}已被{2}驳回，总额为{3}元。请查看审批记录
                msgFirstOpera = string.Format(RebateRegistrationReject, batchModel.Creator, batchModel.PurchaseBatchCode, currentRoleName, batchModel.Amounts.Value.ToString("#,##0.00"));
            }
            else if (batchModel.Status == (int)ESP.Finance.Utility.PaymentStatus.FinanceComplete)
            {
                msgFirstOpera = string.Format(RebateRegistrationComplete, batchModel.Creator, batchModel.PurchaseBatchCode, currentRoleName, batchModel.Amounts.Value.ToString("#,##0.00"));
            }
            else
            {
                if (!string.IsNullOrEmpty(nextAuditorMail))
                {
                    mailAddressList.Add(new MailAddress(nextAuditorMail));

                    msgFirstOpera = string.Format(RebateRegistrationAudit, batchModel.Creator, batchModel.PurchaseBatchCode, currentRoleName, batchModel.Amounts.Value.ToString("#,##0.00"));
                }
            }
            ESP.Mail.MailManager.Send("媒体返点批次导入审批", msgFirstOpera, true, null, mailAddressList.ToArray(), null, null, null);

            return ret;
        }

        public static string SendMailTicket(int returnId, string supplierMail, int issupplier)
        {
            string ret = "";
            if (!string.IsNullOrEmpty(supplierMail))
            {
                string msgFirstOpera = GetTicketHtml(returnId, issupplier);
                SendMail.Send1("机票申请单", supplierMail, msgFirstOpera, false, "");
            }
            return ret;
        }

        //{0}为pr单号，{1}为第一级业务审核人，{2}下一级业务审核人
        static string referPRFirstOperaPasstoReqFK = "{0}已通过{1}付款业务审批，请等待{2}进行下一级审批。";
        static string referPRFirstOperaPasstoReqFKCC = "{0}于{1}审核通过{2}付款申请，供应商为{3}，申请金额{4},请关注。";
        static string referOOPCC = "{0}于{1}审核通过{2}报销单，申请金额{3},请关注。";
        static string ReturnCommit = "{0}已由{1}提交，请您进行业务审批。";
        //{0}为pr单号，{1}为业务审核人，{2}分公司审核人或物料审核人，{3}为分公司或物料审核。
        //static string referPRLastOperaPasstoReq = "{0}已通过{1}付款业务审批，请等待{2}进行{3}。";

        //{0}为pr单号，{1}为业务审核人,{2}为分公司或物料审核。
        //static string referPRLastOpearPasstoFiliorAcr = "{0}已通过{1}付款业务审批,请您进行{2}。";
        //{0}为pr单号，{1}为业务审核人
        static string referPROperaOverruletoReqFK = "{0}被{1}付款业务审批驳回。";
        //{0}为pr单号，{1}为业务审核人，{2}为下一级业务审核人
        static string referPROperaZHFK = "{0}已通过{1}付款业务审批，正等待{2}进行下一级付款审批。";
        //{0}申请人，{1}为pr单号，{2}为业务审批人
        static string referPR0peraZH2FK = "{0}已提交付款申请{1}，正等待{2}进行付款业务审批。";

        public static string SendMailPreCloseProject(ESP.Finance.Entity.ProjectInfo projectModel, string OperaName, string ReqEmail)
        {
            if (!string.IsNullOrEmpty(ReqEmail))
            {
                string msgOverrule = string.Format(PreCloseProject, projectModel.ProjectCode, OperaName);
                SendMail.Send1("项目号预关闭", ReqEmail, msgOverrule, false, "");
            }
            return "";
        }

        public static string SendMailCloseProject(ESP.Finance.Entity.ProjectInfo projectModel, string OperaName, string ReqEmail)
        {
            if (!string.IsNullOrEmpty(ReqEmail))
            {
                string msgOverrule = string.Format(CloseProject, projectModel.ProjectCode, OperaName);
                SendMail.Send1("项目号预关闭", ReqEmail, msgOverrule, false, "");
            }
            return "";
        }


        public static void SendReporterToBiz(string info, string bizEmail)
        {
            if (!string.IsNullOrEmpty(bizEmail))
            {
                SendMail.Send1("记者稿费已付款", bizEmail, info, false, "");
            }
        }
        /// <summary>
        /// 项目号预关闭提醒
        /// </summary>
        public static void SendRemindEmail(IList<ESP.Finance.Entity.ProjectInfo> projectList)
        {

            foreach (ESP.Finance.Entity.ProjectInfo project in projectList)
            {
                string title = "{0}项目关闭提醒";
                string body = @"{0}{1}项目按计划将于{2}关闭。<br /><br />

                            请各位项目组成员在截止日期前提交此项目相关的采购、报销、个人借款冲销，逾期将不能做任何申请。<br /><br />";

                body = string.Format(body, project.ProjectCode, project.BusinessDescription, project.EndDate.Value.AddMonths(1).ToString("yyyy-MM-dd"));
                title = string.Format(title, project.ProjectCode);
                try
                {
                    string RemindEmail = GetRemindEmail(project.ProjectId);
                    ESP.Finance.Utility.SendMail.Send1(title, RemindEmail, body, true);
                    //string sql = "insert f_timingLog values(" + project.ProjectId + ",'" + DateTime.Now + "','" + project.ProjectCode + "项目关闭提醒已发送。')";
                    //ESP.Finance.DataAccess.DbHelperSQL.ExecuteSql(sql);
                }
                catch (Exception ex)
                {
                    //string sql = "insert f_timingLog values(" + DateTime.Now + ",'" + project.ProjectCode + "项目关闭提醒发送失败。')";
                    //ESP.Finance.DataAccess.DbHelperSQL.ExecuteSql(sql);
                    continue;
                }
            }
        }

        public static void SendProjectLeaderEmail(IList<ESP.Finance.Entity.ProjectInfo> projectList)
        {

            foreach (ESP.Finance.Entity.ProjectInfo project in projectList)
            {
                string title = "{0}项目关闭提醒";
                string body = @"{0}{1}项目按计划将于{2}结束。<br /><br />

                                您为此项目负责人，请及时提供项目完工证明；若因客户原因此项目需延期，请在预计结束日期前提供客户确认邮件,

                                在系统中做项目号延期变更，注意变更支持文件上传时在合同描述中写明延期截止日期，由财务部确认后在系统中更新项目结束日期。<br /><br />";

                body = string.Format(body, project.ProjectCode, project.BusinessDescription, project.EndDate.Value.ToString("yyyy-MM-dd"));
                title = string.Format(title, project.ProjectCode);
                try
                {
                    ESP.Framework.Entity.EmployeeInfo emp = ESP.Framework.BusinessLogic.EmployeeManager.Get(project.ApplicantUserID);
                    List<MailAddress> mailAddressList = new List<MailAddress>();
                    mailAddressList.Add(new MailAddress(emp.Email));

                    List<MailAddress> cclist = GetProjectLeaderEmail(project);

                    ESP.Mail.MailManager.Send(title, body, true, null, mailAddressList.ToArray(), cclist.ToArray(), null, null);
                }
                catch (Exception ex)
                {
                    continue;
                }
            }
        }

        public static void WorkItemSend(string username, string userEmail, int Items)
        {
            string title = "待办事项系统提醒";
            string body = @"{0}您好:<br/>您在系统中尚有{1}项待办事宜没有处理，请您<a href={2}>登录</a>系统及时处理。";
            body = string.Format(body, username, Items, "http://xy.shunyagroup.com");
            try
            {
                ESP.Finance.Utility.SendMail.Send1(title, userEmail, body, true);
            }
            catch (Exception ex)
            {
            }
        }

        private static List<MailAddress> GetProjectLeaderEmail(ESP.Finance.Entity.ProjectInfo projectModel)
        {
            List<MailAddress> mailAddressList = new List<MailAddress>();
            ESP.Framework.Entity.OperationAuditManageInfo operationModel = ESP.Framework.BusinessLogic.OperationAuditManageManager.GetModelByDepId(projectModel.GroupID.Value);

            ESP.Framework.Entity.EmployeeInfo director = ESP.Framework.BusinessLogic.EmployeeManager.Get(operationModel.DirectorId);
            ESP.Framework.Entity.EmployeeInfo manager = ESP.Framework.BusinessLogic.EmployeeManager.Get(operationModel.ManagerId);
            if (director != null)
            {
                mailAddressList.Add(new MailAddress(director.Email));
            }
            if (manager != null)
            {
                mailAddressList.Add(new MailAddress(manager.Email));
            }

            return mailAddressList;
        }

        private static string GetRemindEmail(int projectId)
        {
            string strEmails = string.Empty;
            string terms = " projectID=" + projectId;
            IList<ESP.Finance.Entity.ProjectMemberInfo> projectMembers = new ESP.Finance.DataAccess.ProjectMemberDataProvider().GetList(terms, null);
            IList<ESP.Finance.Entity.SupportMemberInfo> supportMembers = new ESP.Finance.DataAccess.SupportMemberDataProvider().GetList(terms, null);
            foreach (ESP.Finance.Entity.ProjectMemberInfo project in projectMembers)
            {
                ESP.Framework.Entity.EmployeeInfo emp = ESP.Framework.BusinessLogic.EmployeeManager.Get(project.MemberUserID.Value);
                if (emp != null)
                {
                    string email = emp.Email;
                    if (email.Trim() != "")
                        strEmails += email + ",";
                }
            }
            foreach (ESP.Finance.Entity.SupportMemberInfo support in supportMembers)
            {
                ESP.Framework.Entity.EmployeeInfo emp = ESP.Framework.BusinessLogic.EmployeeManager.Get(support.MemberUserID.Value);
                if (emp != null)
                {
                    string email = emp.Email;
                    if (email.Trim() != "")
                        strEmails += email + ",";
                }
            }
            return strEmails;
        }
    }
}