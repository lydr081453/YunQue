using System;
using System.Collections.Generic;
using System.Collections;
using ESP.Finance.Utility;

/// <summary>
///SendMailHelper 的摘要说明
/// </summary>
/// 
namespace ESP.Salary.Utility
{
    public class SendMailHelper
    {
        #region 常量
        //{0}为负责人，{1}为PR单号
        const string referPRtoUser = "{0}已提交项目号申请{1},您为该项目号负责人。";
        const string referPRtoRec = "{0}已提交项目号申请{1},您被设定为负责人。";
        //{0}为pr单号，{1}为业务审核人，{2}为下一级业务审核人
        const string referPROperaZH = "{0}已通过{1}业务审批，正等待{2}进行下一级审批。";
        //{0}申请人，{1}为pr单号，{2}为业务审批人
        const string referPR0peraZH2 = "{0}已提交项目号申请{1}，正等待{2}进行业务审批。";
        const string ProjectApplicantBizAudit = "{0}已通过{1}业务审批,请您进行{2}。";
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

        static string WebURL = ESP.Configuration.ConfigurationManager.SafeAppSettings["MainPage"].Replace("Default.aspx", "");
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
            string hostUrl = ESP.Configuration.ConfigurationManager.SafeAppSettings["MainPage"].Replace("Default.aspx", "");
            string url = hostUrl + "project/ProjectPrint.aspx?" + RequestName.ProjectID + "=" + projectid + "&" + RequestName.Mail + "=no";
            return SendMail.ScreenScrapeHtml(url).Replace("src=\"/images", "src=\"" + hostUrl + "/images");
        }

        private static string GetNotifyHtml(int PaymentID)
        {
            string hostUrl = ESP.Configuration.ConfigurationManager.SafeAppSettings["MainPage"].Replace("Default.aspx", "");
            string url = hostUrl + "Return/NotificationPrint.aspx?" + RequestName.PaymentID + "=" + PaymentID.ToString() + "&" + RequestName.Mail + "=no";
            return SendMail.ScreenScrapeHtml(url).Replace("src=\"/images", "src=\"" + hostUrl + "/images");
        }
        private static string GetSupporterHtml(int supportid)
        {
            string hostUrl = ESP.Configuration.ConfigurationManager.SafeAppSettings["MainPage"].Replace("Default.aspx", "");
            string url = hostUrl + "project/SupporterPrint.aspx?" + RequestName.SupportID + "=" + supportid + "&" + RequestName.Mail + "=no";
            return SendMail.ScreenScrapeHtml(url).Replace("src=\"/images", "src=\"" + hostUrl + "/images");
        }

        private static string GetPaymentHtml(int returnId)
        {
            string hostUrl = ESP.Configuration.ConfigurationManager.SafeAppSettings["MainPage"].Replace("Default.aspx", "");
            string url = hostUrl + "Purchase/Print/PaymantPrint.aspx?" + RequestName.ReturnID + "=" + returnId + "&" + RequestName.Mail + "=no";
            return SendMail.ScreenScrapeHtml(url).Replace("src=\"/images", "src=\"" + hostUrl + "/images");
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
                string msgLastOpera = string.Format(ProjectApplicantBizAudit, projectInfo.SerialCode, OpearName, "业务审批");
                msgLastOpera += "<a href=\"" + WebURL + "/Default.aspx?contentUrl=" + System.Web.HttpUtility.UrlEncode(auditPage + RequestName.ProjectID + "=" + projectInfo.ProjectId) + "\">点此进行业务审批</a>";
                msgLastOpera += "<br />" + GetPrHtml(projectInfo.ProjectId);
                SendMail.Send1("项目号申请业务审批通过", ReqEmail, msgLastOpera, false, "");
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
                string msgNextOpera = string.Format("{0}已通过{1}付款业务审批，请您进行付款业务审批。", returnCode, FirstOperaName);
                msgNextOpera += "<a href=\"" + WebURL + "/Default.aspx?contentUrl=" + System.Web.HttpUtility.UrlEncode(auditPage + RequestName.ReturnID + "=" + returnModel.ReturnID) + "\">点此进行业务审批</a>";
                msgNextOpera += "<br />" + GetPaymentHtml(returnModel.ReturnID);
                SendMail.Send1("付款业务审批通过", NextOperaEmail, msgNextOpera, false, "");
            }

            return ret;
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

        public static string SendMailReturnComplete(ESP.Finance.Entity.ReturnInfo returnModel, string OpearName, string ReqEmail)
        {
            string ret = "";
            if (!string.IsNullOrEmpty(ReqEmail))
            {
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
                msgLastOpera += "<br />" + GetPaymentHtml(returnModel.ReturnID);
                SendMail.Send1("付款申请审批完成，财务已付款。", ReqEmail, msgLastOpera, false, "");
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

        //{0}为pr单号，{1}为第一级业务审核人，{2}下一级业务审核人
        static string referPRFirstOperaPasstoReqFK = "{0}已通过{1}付款业务审批，请等待{2}进行下一级审批。";
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
    }
}