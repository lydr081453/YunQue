using System;
using System.Collections;
using ESP.Purchase.Common;
using ESP.Purchase.Entity;

/// <summary>
///SendMailHelper 的摘要说明
/// </summary>
namespace ESP.Purchase.DataAccess
{
    public class SendMailHelper
    {
        #region 常量
        //{0}为申请人，{1}为PR单号
        static string referPRtoUser = "{0}已提交采购申请{1},您被设定为使用人。";
        static string referPRtoRec = "{0}已提交采购申请{1},您被设定为收货人。";

        //{0}为PR单号，{1}为分公司名称
        static string referPRFiliPasstoReq = "{0}已通过{1}审核,请等待采购部物料审核。";
        static string referPRFiliPasstoAcr = "{0}已通过{1}审核，请您进行物料审核。";

        static string referPROverruletoReq = "{0}被{1}采购物料审核驳回。";

        //{0}为PR单号，{1}为物料审核人
        static string referPRAcrPasstoReq = "{0}已通过{1}采购物料审核，请等待采购总监审批";
        static string referPRAcrPasstoGen = "{0}已通过{1}采购物料审核，请您进行采购审批。";

        static string referPRAcrOverruletoReq = "{0}被{1}采购物料审核驳回";

        static string referMediaAuditPass = "{0}被{1}媒介审批通过";
        static string referMediaAuditReturn = "{0}被{1}媒介审批驳回";

        static string referADAuditPass = "{0}被{1}媒体广告采买审批通过";
        static string referADAuditReturn = "{0}被{1}媒体广告采买审批驳回";

        //{0}为PR单号
        static string referPRGenPasstoReq = "{0}已经通过采购总监审批。";
        static string referPRGenPasstoAcrorFili = "{0}已通过采购总监审批，请进行下一步操作。";

        static string referPRGenOverruletoAcr = "{0}已被采购总监驳回。";

        static string referPRRCPasstoReq = "{0}已经通过风控中心审批。";
        static string referPRRCPasstoAcrorFili = "{0}已通过风控中心审批，请进行下一步操作。";
        static string referPRRCOverruletoAcr = "{0}已被风控中心驳回。";

        //{0}为PO单号
        static string referPOSendtoReq = "{0}已经发送至供应商处，请等待对方确认。";

        static string referPOSupConfirmtoReq = "{0}已通过供应商确认生效";
        static string referPOSupConfirmtoRec = "{0}已通过供应商确认生效，请按收货日期及时进行收货确认。";

        static string referPORecConfirmtoReq = "{0}已完成收货确认。";

        //{0}为pr单号，{1}为第一级业务审核人，{2}下一级业务审核人
        static string referPRFirstOperaPasstoReq = "{0}已通过{1}业务审批，请等待{2}进行下一级审批。";

        //{0}为pr单号，{1}为业务审核人，{2}分公司审核人或物料审核人，{3}为分公司或物料审核。
        static string referPRLastOperaPasstoReq = "{0}于{1}审核通过{2}，供应商为{3},采购总金额为{4}，请关注。";

        //{0}为pr单号，{1}为业务审核人,{2}为分公司或物料审核。
        static string referPRLastOpearPasstoFiliorAcr = "{0}已通过{1}业务审批。";
        //{0}为pr单号，{1}为业务审核人
        static string referPROperaOverruletoReq = "{0}被{1}业务审批驳回。";
        //{0}为pr单号，{1}为业务审核人，{2}为下一级业务审核人
        static string referPROperaZH = "{0}已通过{1}业务审批，正等待{2}进行下一级审批。";
        //{0}申请人，{1}为pr单号，{2}为业务审批人
        static string referPR0peraZH2 = "{0}已提交采购申请{1}，正等待{2}进行业务审批。";
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
        private static string GetPrHtml(int generalId)
        {
            string hostUrl = ESP.Configuration.ConfigurationManager.SafeAppSettings["MainPage"].Replace("default.aspx", "");
            string url = hostUrl + "Purchase/Requisition/Print/RequisitionPrint.aspx?id=" + generalId + "&mail=no";
            return ESP.ConfigCommon.SendMail.ScreenScrapeHtml(url).Replace("src=\"images", "src=\"" + hostUrl + "Purchase/Requisition/Print/images");
        }

        /// <summary>
        /// 获得业务审批弹出页链接
        /// </summary>
        /// <param name="generalId"></param>
        /// <returns></returns>
        private static string GetAuditOpenLink(int generalId)
        {
            string openPageUrl = ESP.Configuration.ConfigurationManager.SafeAppSettings["MainPage"] + "?contentUrl=" + System.Web.HttpContext.Current.Server.UrlEncode("/purchase/requisition/operationaudit.aspx?backurl=operationauditlist.aspx&generalid=" + generalId);
            // return string.Format("<a style=\"cursor:pointer; text-decoration:underline;\" target=\"_blank\" onclick=\"window.open('{0}');\">马上进行业务审批</a>",openPageUrl);
            return string.Format("<a href='" + openPageUrl + "'>点此进行业务审批</a>");
        }

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
        public static string SendMailPR(GeneralInfo generalInfo, string PrNO, string ReqName, string UserEmail, string RecEmail, string FirstOperaEmail)
        {
            string ret = "";

            if (!string.IsNullOrEmpty(UserEmail))
            {
                string msgUser = string.Format(referPRtoUser, ReqName, PrNO);
                msgUser += "<br />" + GetPrHtml(generalInfo.id);
                ESP.ConfigCommon.SendMail.Send1("待审核", UserEmail, msgUser, true);
            }
            else
            {
                ret += "使用人没有邮箱，无法发送邮件。";
            }
            if (!string.IsNullOrEmpty(RecEmail))
            {
                if (!string.IsNullOrEmpty(UserEmail) && !UserEmail.Equals(RecEmail))
                {
                    string msgRec = string.Format(referPRtoRec, ReqName, PrNO);
                    msgRec += "<br />" + GetPrHtml(generalInfo.id);
                    ESP.ConfigCommon.SendMail.Send1("待审核", RecEmail, msgRec, true);
                }
            }
            else
            {
                ret += "收货人没有邮箱，无法发送邮件。";
            }
            if (!string.IsNullOrEmpty(FirstOperaEmail))
            {
                string msgOpera = string.Format("{0}已提交采购申请{1},请您进行业务审批。", ReqName, PrNO) + GetAuditOpenLink(generalInfo.id);
                msgOpera += "<br />" + GetPrHtml(generalInfo.id);
                ESP.ConfigCommon.SendMail.Send1("待审核", FirstOperaEmail, msgOpera, true);
            }
            return ret;
        }

        /// <summary>
        /// Sends the mail PR fili pass.
        /// </summary>
        /// <param name="generalInfo">The general info.</param>
        /// <param name="PrNO">The pr NO.</param>
        /// <param name="FiliName">Name of the fili.</param>
        /// <param name="ReqEmail">The req email.</param>
        /// <param name="AcrEmail">The acr email.</param>
        /// <returns></returns>
        public static string SendMailPRFiliPass(GeneralInfo generalInfo, string PrNO, string FiliName, string ReqEmail, string AcrEmail)
        {
            string ret = "";
            if (!string.IsNullOrEmpty(ReqEmail))
            {
                string msgReq = string.Format(referPRFiliPasstoReq, PrNO, FiliName);
                msgReq += "<br />" + GetPrHtml(generalInfo.id);
                ESP.ConfigCommon.SendMail.Send1("分公司审核通过", ReqEmail, msgReq, true);
            }
            else
            {
                ret += "申请人没有邮箱，无法发送邮件。";
            }
            if (!string.IsNullOrEmpty(AcrEmail))
            {
                string msgAcr = string.Format(referPRFiliPasstoAcr, PrNO, FiliName);
                msgAcr += "<br />" + GetPrHtml(generalInfo.id);
                ESP.ConfigCommon.SendMail.Send1("分公司审核通过", AcrEmail, msgAcr, true);
            }
            else
            {
                ret += "物料审核人没有邮箱，无法发送邮件。";
            }
            return ret;
        }

        /// <summary>
        /// Sends the mail PR fili overrule.
        /// </summary>
        /// <param name="generalInfo">The general info.</param>
        /// <param name="PrNO">The pr NO.</param>
        /// <param name="FiliName">Name of the fili.</param>
        /// <param name="ReqEmail">The req email.</param>
        /// <returns></returns>
        public static string SendMailPRFiliOverrule(GeneralInfo generalInfo, string PrNO, string FiliName, string ReqEmail)
        {
            string ret = "";
            if (!string.IsNullOrEmpty(ReqEmail))
            {
                string msgReq = string.Format(referPROverruletoReq, PrNO, FiliName);
                msgReq += "<br />" + GetPrHtml(generalInfo.id);
                ESP.ConfigCommon.SendMail.Send1("分公司审核驳回", ReqEmail, msgReq, true);
            }
            else
            {
                ret += "申请人没有邮箱，无法发送邮件。";
            }
            return ret;
        }

        /// <summary>
        /// Sends the mail PR acr pass.
        /// </summary>
        /// <param name="generalInfo">The general info.</param>
        /// <param name="PrNO">The pr NO.</param>
        /// <param name="AcrName">Name of the acr.</param>
        /// <param name="ReqEmail">The req email.</param>
        /// <param name="GenEMail">The gen E mail.</param>
        /// <returns></returns>
        public static string SendMailPRAcrPass(GeneralInfo generalInfo, string PrNO, string AcrName, String ReqEmail, string GenEMail)
        {
            string ret = "";
            if (!string.IsNullOrEmpty(ReqEmail))
            {
                string msgReq = string.Format(referPRAcrPasstoReq, PrNO, AcrName);
                msgReq += "<br />" + GetPrHtml(generalInfo.id);
                ESP.ConfigCommon.SendMail.Send1("物料审核通过", ReqEmail, msgReq, true);
            }
            else
            {
                ret += "申请人没有邮箱，无法发送邮件。";
            }
            string msgGen = string.Format(referPRAcrPasstoGen, PrNO, AcrName);
            if (!string.IsNullOrEmpty(GenEMail))
            {
                msgGen += "<br />" + GetPrHtml(generalInfo.id);
                ESP.ConfigCommon.SendMail.Send1("物料审核通过", GenEMail, msgGen, true);
            }
            else
            {
                ret += "采购总监没有邮箱，无法发送邮件。";
            }
            return ret;
        }

        /// <summary>
        /// Sends the mail PR acr overrule.
        /// </summary>
        /// <param name="generalInfo">The general info.</param>
        /// <param name="PrNO">The pr NO.</param>
        /// <param name="AcrName">Name of the acr.</param>
        /// <param name="ReqMail">The req mail.</param>
        /// <returns></returns>
        public static string SendMailPRAcrOverrule(GeneralInfo generalInfo, string PrNO, string AcrName, string ReqMail)
        {
            string ret = "";
            if (!string.IsNullOrEmpty(ReqMail))
            {
                string msgReq = string.Format(referPRAcrOverruletoReq, PrNO, AcrName);
                msgReq += "<br />" + GetPrHtml(generalInfo.id);
                ESP.ConfigCommon.SendMail.Send1("物料审核驳回", ReqMail, msgReq, true);
            }
            else
            {
                ret += "申请人没有邮箱，无法发送邮件。";
            }
            return ret;
        }

        /// <summary>
        /// Sends the mail PR gen pass.
        /// </summary>
        /// <param name="generalInfo">The general info.</param>
        /// <param name="PrNO">The pr NO.</param>
        /// <param name="ReqEmail">The req email.</param>
        /// <param name="FiliorAcrEmail">The filior acr email.</param>
        /// <returns></returns>
        public static string SendMailPRGenPass(GeneralInfo generalInfo, string PrNO, string ReqEmail, string FiliorAcrEmail)
        {
            string msgReq = string.Format(referPRGenPasstoReq, PrNO);
            string msgPass = string.Format(referPRGenPasstoAcrorFili, PrNO);

            string ret = "";
            if (!string.IsNullOrEmpty(ReqEmail))
            {
                msgReq += "<br />" + GetPrHtml(generalInfo.id);
                ESP.ConfigCommon.SendMail.Send1("采购总监审批通过", ReqEmail, msgReq, true);
            }
            else
            {
                ret += "申请人没有邮箱，无法发送邮件。";
            }

            if (!string.IsNullOrEmpty(FiliorAcrEmail))
            {
                msgPass += "<br />" + GetPrHtml(generalInfo.id);
                ESP.ConfigCommon.SendMail.Send1("采购总监审批通过", FiliorAcrEmail, msgPass, true);
            }
            else
            {
                ret += "审核人没有邮箱，无法发送邮件。";
            }
            //if (generalInfo.PRType == (int)PRTYpe.MediaPR || generalInfo.PRType == (int)PRTYpe.PR_MediaFA)//给媒介审批人发信
            //{
            //    ESP.ConfigCommon.SendMail.Send1("采购总监审批通过", State.getEmployeeEmailBySysUserId(int.Parse(ESP.Configuration.ConfigurationManager.SafeAppSettings["mediaAuditorId"])), PrNO + "已通过采购总监审批，请您进行媒介审批。" + "<br />" + GetPrHtml(generalInfo.id), true);
            //}
            return ret;
        }

        public static string SendMailPRRiskControl(GeneralInfo generalInfo, string PrNO, string ReqEmail, string FiliorAcrEmail)
        {
            string msgReq = string.Format(referPRRCPasstoReq, PrNO);
            string msgPass = string.Format(referPRRCPasstoAcrorFili, PrNO);

            string ret = "";
            if (!string.IsNullOrEmpty(ReqEmail))
            {
                msgReq += "<br />" + GetPrHtml(generalInfo.id);
                ESP.ConfigCommon.SendMail.Send1("风控中心审批通过", ReqEmail, msgReq, true);
            }
            else
            {
                ret += "申请人没有邮箱，无法发送邮件。";
            }

            if (!string.IsNullOrEmpty(FiliorAcrEmail))
            {
                msgPass += "<br />" + GetPrHtml(generalInfo.id);
                ESP.ConfigCommon.SendMail.Send1("风控中心审批通过", FiliorAcrEmail, msgPass, true);
            }
            else
            {
                ret += "审核人没有邮箱，无法发送邮件。";
            }

            return ret;
        }

        /// <summary>
        /// Sends the mail PR gen overrule.
        /// </summary>
        /// <param name="generalInfo">The general info.</param>
        /// <param name="PrNO">The pr NO.</param>
        /// <param name="AcrEmail">The acr email.</param>
        /// <returns></returns>
        public static string SendMailPRRCOverrule(GeneralInfo generalInfo, string PrNO, string AcrEmail)
        {
            string msgAcr = string.Format(referPRRCOverruletoAcr, PrNO);

            string ret = "";
            string requestorEmail = State.getEmployeeEmailBySysUserId(generalInfo.requestor);
            msgAcr += "<br />" + GetPrHtml(generalInfo.id);
            ESP.ConfigCommon.SendMail.Send1("风控中心审批驳回", requestorEmail, msgAcr, true);
            if (!string.IsNullOrEmpty(AcrEmail))
            {
                ESP.ConfigCommon.SendMail.Send1("风控中心审批驳回", AcrEmail, msgAcr, true);
            }
            else
            {
                ret += "审核人没有邮箱，无法发送邮件。";
            }
            return ret;
        }


        /// <summary>
        /// 媒介审批通过
        /// </summary>
        /// <param name="generalInfo"></param>
        /// <param name="PrNO"></param>
        /// <param name="ReqEmail"></param>
        /// <param name="auditName"></param>
        /// <returns></returns>
        public static string SendMailMediaAuditPass(GeneralInfo generalInfo, string PrNO, string ReqEmail, string auditName)
        {
            string msgPass = string.Format(referMediaAuditPass, PrNO, auditName);

            string ret = "";
            if (!string.IsNullOrEmpty(ReqEmail))
            {
                msgPass += "<br />" + GetPrHtml(generalInfo.id);
                ESP.ConfigCommon.SendMail.Send1("媒介总监审批通过", ReqEmail, msgPass, true);
            }
            else
            {
                ret += "申请人没有邮箱，无法发送邮件。";
            }
            //ESP.ConfigCommon.SendMail.Send1("媒介总监审批通过", State.getEmployeeEmailBySysUserId(int.Parse(ESP.Configuration.ConfigurationManager.SafeAppSettings["mediaFAAuditorId"])), msgPass, true);
            return ret;
        }
        /// <summary>
        /// 媒介审批驳回
        /// </summary>
        /// <param name="generalInfo"></param>
        /// <param name="PrNO"></param>
        /// <param name="ReqMail"></param>
        /// <param name="auditName"></param>
        /// <returns></returns>
        public static string SendMailMediaAuditReturn(GeneralInfo generalInfo, string PrNO, string ReqMail, string auditName)
        {
            string ret = "";
            if (!string.IsNullOrEmpty(ReqMail))
            {
                string msgReq = string.Format(referMediaAuditReturn, PrNO, auditName);
                msgReq += "<br />" + GetPrHtml(generalInfo.id);
                ESP.ConfigCommon.SendMail.Send1("媒介总监审批驳回", ReqMail, msgReq, true);
            }
            else
            {
                ret += "申请人没有邮箱，无法发送邮件。";
            }
            return ret;
        }

        /// <summary>
        /// 媒介审批通过
        /// </summary>
        /// <param name="generalInfo"></param>
        /// <param name="PrNO"></param>
        /// <param name="ReqEmail"></param>
        /// <param name="auditName"></param>
        /// <returns></returns>
        public static string SendMailADAuditPass(GeneralInfo generalInfo, string PrNO, string ReqEmail, string auditName)
        {
            string msgPass = string.Format(referADAuditPass, PrNO, auditName);

            string ret = "";
            if (!string.IsNullOrEmpty(ReqEmail))
            {
                msgPass += "<br />" + GetPrHtml(generalInfo.id);
                ESP.ConfigCommon.SendMail.Send1("媒体广告采买总监审批通过", ReqEmail, msgPass, true);
            }
            else
            {
                ret += "申请人没有邮箱，无法发送邮件。";
            }
            return ret;
        }
        /// <summary>
        /// 媒介审批驳回
        /// </summary>
        /// <param name="generalInfo"></param>
        /// <param name="PrNO"></param>
        /// <param name="ReqMail"></param>
        /// <param name="auditName"></param>
        /// <returns></returns>
        public static string SendMailADAuditReturn(GeneralInfo generalInfo, string PrNO, string ReqMail, string auditName)
        {
            string ret = "";
            if (!string.IsNullOrEmpty(ReqMail))
            {
                string msgReq = string.Format(referADAuditReturn, PrNO, auditName);
                msgReq += "<br />" + GetPrHtml(generalInfo.id);
                ESP.ConfigCommon.SendMail.Send1("媒体广告采买总监审批驳回", ReqMail, msgReq, true);
            }
            else
            {
                ret += "申请人没有邮箱，无法发送邮件。";
            }
            return ret;
        }

        /// <summary>
        /// Sends the mail PR gen overrule.
        /// </summary>
        /// <param name="generalInfo">The general info.</param>
        /// <param name="PrNO">The pr NO.</param>
        /// <param name="AcrEmail">The acr email.</param>
        /// <returns></returns>
        public static string SendMailPRGenOverrule(GeneralInfo generalInfo, string PrNO, string AcrEmail)
        {
            string msgAcr = string.Format(referPRGenOverruletoAcr, PrNO);

            string ret = "";
            string requestorEmail = State.getEmployeeEmailBySysUserId(generalInfo.requestor);
            msgAcr += "<br />" + GetPrHtml(generalInfo.id);
            ESP.ConfigCommon.SendMail.Send1("采购总监审批驳回", requestorEmail, msgAcr, true);
            if (!string.IsNullOrEmpty(AcrEmail))
            {
                ESP.ConfigCommon.SendMail.Send1("采购总监审批驳回", AcrEmail, msgAcr, true);
            }
            else
            {
                ret += "审核人没有邮箱，无法发送邮件。";
            }
            return ret;
        }

        /// <summary>
        /// Sends the mail PO.
        /// </summary>
        /// <param name="generalInfo">The general info.</param>
        /// <param name="PoNO">The po NO.</param>
        /// <param name="ReqEmail">The req email.</param>
        /// <param name="SupEmail">The sup email.</param>
        /// <param name="SupBody">The sup body.</param>
        /// <param name="atts">The atts.</param>
        /// <returns></returns>
        public static string SendMailPO(GeneralInfo generalInfo, string PoNO, string ReqEmail, string SupEmail, string SupBody, Hashtable atts)
        {
            string ret = "";
            ESP.Finance.Entity.BranchInfo branchModel = ESP.Finance.BusinessLogic.BranchManager.GetModelByCode(generalInfo.project_code.Substring(0, 1));

            //给供应商发送PO单
            if (!string.IsNullOrEmpty(SupEmail))
            {
                ESP.ConfigCommon.SendMail.Send1(branchModel.BranchName + "采购订单" + PoNO, SupEmail, SupBody, false, atts);
            }
            else
            {
                ret += "供应商没有邮箱，无法发送邮件。";
            }
            return ret;
        }

        /// <summary>
        /// Sends the mail PO confirm.
        /// </summary>
        /// <param name="generalInfo">The general info.</param>
        /// <param name="PoNO">The po NO.</param>
        /// <param name="ReqEmail">The req email.</param>
        /// <param name="RecEmail">The rec email.</param>
        /// <param name="auditEmail">The audit email.</param>
        /// <returns></returns>
        public static string SendMailPOConfirm(GeneralInfo generalInfo, string PoNO, string ReqEmail, string RecEmail, string auditEmail)
        {
            string ret = "";
            string msgReq = string.Format(referPOSupConfirmtoReq, PoNO);
            string msgRec = string.Format(referPOSupConfirmtoRec, PoNO);
            if (!string.IsNullOrEmpty(ReqEmail))
            {
                ESP.ConfigCommon.SendMail.Send1("订单已确认", ReqEmail, msgReq + "<br />" + GetPrHtml(generalInfo.id), true);
            }
            else
            {
                ret += "申请人没有邮箱，无法发送邮件。";
            }

            if (!string.IsNullOrEmpty(RecEmail))
            {
                if (!string.IsNullOrEmpty(ReqEmail) && !ReqEmail.Equals(RecEmail))
                {
                    msgRec += "<br />" + GetPrHtml(generalInfo.id);
                    ESP.ConfigCommon.SendMail.Send1("订单已确认", RecEmail, msgRec, true);
                }
            }
            else
            {
                ret += "收货人没有邮箱，无法发送邮件。";
            }

            if (!string.IsNullOrEmpty(auditEmail))
            {
                ESP.ConfigCommon.SendMail.Send1("订单已确认", auditEmail, msgReq + "<br />" + GetPrHtml(generalInfo.id), true);
            }
            else
            {
                ret += "审核人没有邮箱，无法发送邮件。";
            }
            return ret;
        }

        /// <summary>
        /// Sends the mail PO recto sup.
        /// </summary>
        /// <param name="PoNO">The po NO.</param>
        /// <param name="SupEmail">The sup email.</param>
        /// <param name="SupBody">The sup body.</param>
        /// <param name="htmlFilePath">The HTML file path.</param>
        /// <param name="msg">The MSG.</param>
        /// <returns></returns>
        public static string SendMailPORectoSup(string PoNO, string SupEmail, string SupBody, string htmlFilePath, string msg)
        {
            string ret = "";
            if (!string.IsNullOrEmpty(SupEmail))
            {
                msg = string.Format("订单已收货，此次是{0}收货", msg);
                ESP.ConfigCommon.SendMail.Send1(msg, SupEmail, SupBody, false, htmlFilePath);
            }
            else
            {
                ret += "供应商没有邮箱，无法发送邮件。";
            }
            return ret;
        }

        /// <summary>
        /// Sends the mail PO rec confirm.
        /// </summary>
        /// <param name="generalInfo">The general info.</param>
        /// <param name="PoNO">The po NO.</param>
        /// <param name="ReqEmail">The req email.</param>
        /// <returns></returns>
        public static string SendMailPORecConfirm(GeneralInfo generalInfo, string PoNO, string ReqEmail)
        {
            string ret = "";
            string msgConfirm = string.Format(referPORecConfirmtoReq, PoNO);
            if (!string.IsNullOrEmpty(ReqEmail))
            {
                msgConfirm += "<br />" + GetPrHtml(generalInfo.id);
                ESP.ConfigCommon.SendMail.Send1("订单已收货", ReqEmail, msgConfirm, true);
            }
            else
            {
                ret += "申请人没有邮箱，无法发送邮件。";
            }
            return ret;
        }

        /// <summary>
        /// Sends the mail PR first opera pass.
        /// </summary>
        /// <param name="generalInfo">The general info.</param>
        /// <param name="PrNO">The pr NO.</param>
        /// <param name="FirstOperaName">First name of the opera.</param>
        /// <param name="NextOperaName">Name of the next opera.</param>
        /// <param name="ReqEmail">The req email.</param>
        /// <param name="NextOperaEmail">The next opera email.</param>
        /// <param name="OperaCount">The opera count.</param>
        /// <returns></returns>
        public static string SendMailPRFirstOperaPass(GeneralInfo generalInfo, string PrNO, string FirstOperaName, string NextOperaName, string ReqEmail, string NextOperaEmail, int OperaCount)
        {
            string ret = "";
            if (!string.IsNullOrEmpty(ReqEmail))
            {
                string msgFirstOpera = string.Format(referPRFirstOperaPasstoReq, PrNO, FirstOperaName, NextOperaName);
                msgFirstOpera += "<br />" + GetPrHtml(generalInfo.id);
                ESP.ConfigCommon.SendMail.Send1("业务审批通过", ReqEmail, msgFirstOpera, false);
            }
            if (!string.IsNullOrEmpty(NextOperaEmail))
            {
                string msgNextOpera = string.Format("{0}已通过{1}业务审批，请您进行业务审批。", PrNO, FirstOperaName) + GetAuditOpenLink(generalInfo.id);
                msgNextOpera += "<br />" + GetPrHtml(generalInfo.id);
                ESP.ConfigCommon.SendMail.Send1("业务审批通过", NextOperaEmail, msgNextOpera, false);
            }
            return ret;
        }


        public static string SendMailPRLastOperaPassCC(GeneralInfo generalInfo, string auditorName, string ccMail)
        {
            string ret = "";
            if (!string.IsNullOrEmpty(ccMail))
            {//{0}于{1}审核通过{2}，供应商为{1},采购总金额为{2}。
                string msgLastOpera = string.Format(referPRLastOperaPasstoReq, auditorName, DateTime.Now.ToString("yyyy-MM-dd"), generalInfo.PrNo, generalInfo.supplier_name, generalInfo.totalprice.ToString("#,##0.00"));
                msgLastOpera += "<br />" + GetPrHtml(generalInfo.id);
                ESP.ConfigCommon.SendMail.Send1("业务审批通过", ccMail, msgLastOpera, false);
            }

            return ret;
        }

        /// <summary>
        /// Sends the mail PR last opera pass.
        /// </summary>
        /// <param name="generalInfo">The general info.</param>
        /// <param name="PrNO">The pr NO.</param>
        /// <param name="OpearName">Name of the opear.</param>
        /// <param name="FiliorAcrName">Name of the filior acr.</param>
        /// <param name="ReqEmail">The req email.</param>
        /// <param name="FiiliorAcrEmail">The fiilior acr email.</param>
        /// <param name="isFili">if set to <c>true</c> [is fili].</param>
        /// <returns></returns>
        public static string SendMailPRLastOperaPass(GeneralInfo generalInfo, string PrNO, string OpearName, string FiliorAcrName, string ReqEmail, string FiiliorAcrEmail, bool isFili)
        {
            string ret = "";
            if (!string.IsNullOrEmpty(ReqEmail))
            {
                string msgLastOpera = string.Format(referPRLastOperaPasstoReq, OpearName, DateTime.Now.ToString("yyyy-MM-dd"), generalInfo.PrNo, generalInfo.supplier_name, generalInfo.totalprice.ToString("#,##0.00"));
                msgLastOpera += "<br />" + GetPrHtml(generalInfo.id);
                ESP.ConfigCommon.SendMail.Send1("业务审批通过", ReqEmail, msgLastOpera, false);
            }

            return ret;
        }

        /// <summary>
        /// Sends the mail PR opera overrule.
        /// </summary>
        /// <param name="generalInfo">The general info.</param>
        /// <param name="PrNO">The pr NO.</param>
        /// <param name="OperaName">Name of the opera.</param>
        /// <param name="ReqEmail">The req email.</param>
        /// <returns></returns>
        public static string SendMailPROperaOverrule(GeneralInfo generalInfo, string PrNO, string OperaName, string ReqEmail)
        {
            string ret = "";
            if (!string.IsNullOrEmpty(ReqEmail))
            {
                string msgOverrule = string.Format(referPROperaOverruletoReq, PrNO, OperaName);
                msgOverrule += "<br />" + GetPrHtml(generalInfo.id);
                ESP.ConfigCommon.SendMail.Send1("业务驳回", ReqEmail, msgOverrule, false);
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
        public static void SendMailToZH(GeneralInfo generalInfo, string PrNo, string auditor, string nextauditor, string ZHEmails)
        {
            if (!string.IsNullOrEmpty(ZHEmails))
            {
                string msgBody = string.Format(referPROperaZH, PrNo, auditor, nextauditor);
                msgBody += "<br />" + GetPrHtml(generalInfo.id);
                ESP.ConfigCommon.SendMail.Send1("业务审核知会", ZHEmails, msgBody, false);
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
        public static void SendMailToZH2(GeneralInfo generalInfo, string PrNo, string requestor, string auditor, string ZHEmails)
        {
            if (!string.IsNullOrEmpty(ZHEmails))
            {
                string msgBody = string.Format(referPR0peraZH2, requestor, PrNo, auditor);
                msgBody += "<br />" + GetPrHtml(generalInfo.id);
                ESP.ConfigCommon.SendMail.Send1("业务审核知会", ZHEmails, msgBody, false);
            }
        }


        public static void SendReporterToBiz(string info, string bizEmail)
        {
            if (!string.IsNullOrEmpty(bizEmail))
            {
                ESP.ConfigCommon.SendMail.Send1("记者稿费已付款", bizEmail, info, false, "");
            }
        }
    }
}