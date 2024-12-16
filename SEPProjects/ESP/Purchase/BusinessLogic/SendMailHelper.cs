using System;
using System.Web;
using System.Collections;
using System.Collections.Generic;
using ESP.Purchase.Common;
using ESP.Purchase.Entity;

/// <summary>
///SendMailHelper 的摘要说明
/// </summary>
namespace ESP.Purchase.BusinessLogic
{
    public static class SendMailHelper
    {
        /// <summary>
        /// pr已提交
        /// </summary>
        /// <param name="PrNO"></param>
        /// <param name="ReqName"></param>
        /// <param name="UserEmail"></param>
        /// <param name="RecEmail"></param>
        /// <param name="FirstOperaEmail"></param>
        /// <returns></returns>
        public static string SendMailPR(GeneralInfo generalInfo, HttpRequest request, string PrNO, string ReqName, string UserEmail, string RecEmail, string FirstOperaEmail)
        {
            return DataAccess.SendMailHelper.SendMailPR(generalInfo, PrNO, ReqName, UserEmail, RecEmail, FirstOperaEmail);
        }

        /// <summary>
        /// 分公司审核
        /// </summary>
        /// <param name="PrNO"></param>
        /// <param name="FiliName"></param>
        /// <param name="ReqEmail"></param>
        /// <param name="AcrEmail"></param>
        /// <param name="isPass"></param>
        /// <returns></returns>
        public static string SendMailPRFili(GeneralInfo generalInfo, string PrNO, string FiliName, string ReqEmail, string AcrEmail, bool isPass)
        {
            if (isPass)
            {
                return DataAccess.SendMailHelper.SendMailPRFiliPass(generalInfo, PrNO, FiliName, ReqEmail, AcrEmail);
            }
            else
            {
                return DataAccess.SendMailHelper.SendMailPRFiliOverrule(generalInfo, PrNO, FiliName, ReqEmail);
            }
        }

        /// <summary>
        /// 物料审核
        /// </summary>
        /// <param name="generalInfo">The general info.</param>
        /// <param name="PrNO">The pr NO.</param>
        /// <param name="AcrName">Name of the acr.</param>
        /// <param name="ReqEmail">The req email.</param>
        /// <param name="GenEMail">The gen E mail.</param>
        /// <param name="isPass">if set to <c>true</c> [is pass].</param>
        /// <returns></returns>
        public static string SendMailPRAcr(GeneralInfo generalInfo, string PrNO, string AcrName, String ReqEmail, string GenEMail, bool isPass)
        {
            if (isPass)
            {
                return DataAccess.SendMailHelper.SendMailPRAcrPass(generalInfo, PrNO, AcrName, ReqEmail, GenEMail);
            }
            else
            {
                return DataAccess.SendMailHelper.SendMailPRAcrOverrule(generalInfo, PrNO, AcrName, ReqEmail);
            }
        }

        /// <summary>
        /// 采购审批
        /// </summary>
        /// <param name="generalInfo">The general info.</param>
        /// <param name="PrNO">The pr NO.</param>
        /// <param name="ReqEmail">The req email.</param>
        /// <param name="FiliorAcrEmail">The filior acr email.</param>
        /// <param name="isPass">if set to <c>true</c> [is pass].</param>
        /// <returns></returns>
        public static string SendMailPRGen(GeneralInfo generalInfo, string PrNO, string ReqEmail, string FiliorAcrEmail, bool isPass)
        {
            if (isPass)
            {
                return DataAccess.SendMailHelper.SendMailPRGenPass(generalInfo, PrNO, ReqEmail, FiliorAcrEmail);
            }
            else
            {
                return DataAccess.SendMailHelper.SendMailPRGenOverrule(generalInfo, PrNO, FiliorAcrEmail);
            }
        }
        /// <summary>
        /// 风控审批
        /// </summary>
        /// <param name="generalInfo">The general info.</param>
        /// <param name="PrNO">The pr NO.</param>
        /// <param name="ReqEmail">The req email.</param>
        /// <param name="FiliorAcrEmail">The filior acr email.</param>
        /// <param name="isPass">if set to <c>true</c> [is pass].</param>
        /// <returns></returns>
        public static string SendMailPRRiskControl(GeneralInfo generalInfo, string PrNO, string ReqEmail, string FiliorAcrEmail, bool isPass)
        {
            if (isPass)
            {
                return DataAccess.SendMailHelper.SendMailPRRiskControl(generalInfo, PrNO, ReqEmail, FiliorAcrEmail);
            }
            else
            {
                return DataAccess.SendMailHelper.SendMailPRRCOverrule(generalInfo, PrNO, FiliorAcrEmail);
            }
        }

        public static string SendMailForMediaAudit(GeneralInfo generalInfo, string prNo, string reqEmail, string auditName, bool isPass)
        {
            if (isPass)
                return DataAccess.SendMailHelper.SendMailMediaAuditPass(generalInfo, prNo, reqEmail, auditName);
            else
                return DataAccess.SendMailHelper.SendMailMediaAuditReturn(generalInfo, prNo, reqEmail, auditName);
        }

        /// <summary>
        /// 媒介广告采买审批
        /// </summary>
        /// <param name="generalInfo">The general info.</param>
        /// <param name="prNo">The pr no.</param>
        /// <param name="reqEmail">The req email.</param>
        /// <param name="auditName">Name of the audit.</param>
        /// <param name="isPass">if set to <c>true</c> [is pass].</param>
        /// <returns></returns>
        public static string SendMailForADAudit(GeneralInfo generalInfo, string prNo, string reqEmail, string auditName, bool isPass)
        {
            if (isPass)
                return DataAccess.SendMailHelper.SendMailADAuditPass(generalInfo, prNo, reqEmail, auditName);
            else
                return DataAccess.SendMailHelper.SendMailADAuditReturn(generalInfo, prNo, reqEmail, auditName);
        }


        /// <summary>
        /// 订单已发送
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
            return DataAccess.SendMailHelper.SendMailPO(generalInfo, PoNO, ReqEmail, SupEmail, SupBody, atts);

        }
        /// <summary>
        /// 订单已确认
        /// </summary>
        /// <param name="generalInfo">The general info.</param>
        /// <param name="PoNO">The po NO.</param>
        /// <param name="ReqEmail">The req email.</param>
        /// <param name="RecEmail">The rec email.</param>
        /// <param name="auditEmail">The audit email.</param>
        /// <returns></returns>
        public static string SendMailPOConfirm(GeneralInfo generalInfo, string PoNO, string ReqEmail, string RecEmail, string auditEmail)
        {
            return DataAccess.SendMailHelper.SendMailPOConfirm(generalInfo, PoNO, ReqEmail, RecEmail, auditEmail);
        }

        /// <summary>
        /// 订单收货(发给供应商发货邮件)
        /// </summary>
        /// <param name="PoNO">The po NO.</param>
        /// <param name="SupEmail">The sup email.</param>
        /// <param name="SupBody">The sup body.</param>
        /// <param name="htmlFilePath">The HTML file path.</param>
        /// <param name="msg">The MSG.</param>
        /// <returns></returns>
        public static string SendMailPORectoSup(string PoNO, string SupEmail, string SupBody, string htmlFilePath, string msg)
        {
            return DataAccess.SendMailHelper.SendMailPORectoSup(PoNO, SupEmail, SupBody, htmlFilePath, msg);
        }

        /// <summary>
        /// 订单已收货（发给申请人）
        /// </summary>
        /// <param name="generalInfo">The general info.</param>
        /// <param name="PoNO">The po NO.</param>
        /// <param name="ReqEmail">The req email.</param>
        /// <returns></returns>
        public static string SendPORecConfim(GeneralInfo generalInfo, string PoNO, string ReqEmail)
        {
            return DataAccess.SendMailHelper.SendMailPORecConfirm(generalInfo, PoNO, ReqEmail);
        }

        /// <summary>
        /// 业务审批（非最后一级）
        /// </summary>
        /// <param name="generalInfo">The general info.</param>
        /// <param name="PrNO">PR单号</param>
        /// <param name="OperaName">当前业务审批人</param>
        /// <param name="NextOperaName">下一级业务审批人</param>
        /// <param name="ReqEmail">申请人邮箱</param>
        /// <param name="NextOperaEmail">The next opera email.</param>
        /// <param name="OperaCount">当前第几级业务审批</param>
        /// <param name="isPass">是否业务审批通过</param>
        public static void SendMailPROperaPass(GeneralInfo generalInfo, string PrNO, string OperaName, string NextOperaName, string ReqEmail, string NextOperaEmail, int OperaCount, bool isPass)
        {
            if (isPass)
            {
                DataAccess.SendMailHelper.SendMailPRFirstOperaPass(generalInfo, PrNO, OperaName, NextOperaName, ReqEmail,
                                                            NextOperaEmail, OperaCount);
            }
            else
            {
                DataAccess.SendMailHelper.SendMailPROperaOverrule(generalInfo, PrNO, OperaName, ReqEmail);
            }
        }

        /// <summary>
        /// 最后一级审批
        /// </summary>
        /// <param name="generalInfo">The general info.</param>
        /// <param name="PrNO">PR单号</param>
        /// <param name="OperaName">当前业务审批人</param>
        /// <param name="FiliorAcrName">分公司名称或物料审核人名</param>
        /// <param name="ReqEmail">申请人邮箱</param>
        /// <param name="FiliorAcrEmail">分公司或物料审核人邮箱</param>
        /// <param name="isFili">是否分公司审核</param>
        /// <param name="isPass">是否业务审批通过</param>
        public static void SendMailPRLastOperaPass(GeneralInfo generalInfo, string PrNO, string OperaName, string FiliorAcrName, string ReqEmail, string FiliorAcrEmail, bool isFili, bool isPass)
        {
            if (isPass)
            {
                DataAccess.SendMailHelper.SendMailPRLastOperaPass(generalInfo, PrNO, OperaName, FiliorAcrName,
                                                                  ReqEmail,
                                                                  FiliorAcrEmail, isFili);
            }
            else
            {
                DataAccess.SendMailHelper.SendMailPROperaOverrule(generalInfo, PrNO, OperaName, ReqEmail);
            }
        }

        public static void SendMailPRLastOperaPassCC(GeneralInfo generalInfo, string auditorName, string ccMail)
        {
            DataAccess.SendMailHelper.SendMailPRLastOperaPassCC(generalInfo, auditorName, ccMail);
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
            DataAccess.SendMailHelper.SendMailToZH(generalInfo, PrNo, auditor, nextauditor, ZHEmails);
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
            DataAccess.SendMailHelper.SendMailToZH2(generalInfo, PrNo, requestor, auditor, ZHEmails);
        }

        /// <summary>
        /// 变更prno时，给供应商发信
        /// </summary>
        /// <param name="generalInfo">The general info.</param>
        /// <param name="orderId">The order id.</param>
        /// <param name="Request">The request.</param>
        /// <param name="Server">The server.</param>
        public static void PrNoChangedMail(GeneralInfo generalInfo, string orderId, HttpRequest Request, HttpServerUtility Server)
        {
            generalInfo.orderid = orderId;
            string url = Request.Url.ToString().Substring(0, Request.Url.ToString().LastIndexOf('/') + 1) + "Print/OrderPrint.aspx?id=" + generalInfo.id + "&mail=changedPrNo";
            string body = orderId + "已变更，请等待变更后的采购订单。<br />" + ESP.ConfigCommon.SendMail.ScreenScrapeHtml(url);
            ESP.Finance.Entity.BranchInfo branchModel = ESP.Finance.BusinessLogic.BranchManager.GetModelByCode(generalInfo.project_code.Substring(0, 1));

            string htmlFilePath = Server.MapPath("~") + "ExcelTemplate\\" + "订单" + generalInfo.orderid + ".htm";
            string clause = Server.MapPath("~") + "ExcelTemplate\\" + branchModel.POTerm;

            FileHelper.DeleteFile(htmlFilePath);
            FileHelper.SaveFile(htmlFilePath, body);
            List<OrderInfo> orders = OrderInfoManager.GetListByGeneralId(generalInfo.id);
            Hashtable attFiles = new Hashtable();
            attFiles.Add(branchModel.POTerm, clause);
            attFiles.Add("", htmlFilePath);
            if (generalInfo.sow2.Trim() != "")
            {
                attFiles.Add("工作描述" + generalInfo.sow2.Substring(generalInfo.sow2.IndexOf(".")), ESP.Purchase.Common.ServiceURL.UpFilePath + generalInfo.sow2);
            }

            int filecount = 1;
            foreach (OrderInfo model in orders)
            {
                if (model.upfile.Trim() != "")
                {
                    string[] strFiles = model.upfile.Split('#');
                    for (int i = 0; i < strFiles.Length; i++)
                    {
                        attFiles.Add("采购物品报价" + filecount.ToString() + strFiles[i].Substring(model.upfile.IndexOf(".")), ESP.Purchase.Common.ServiceURL.UpFilePath + strFiles[i]);
                        filecount++;
                    }
                }
            }
            string supplierEmail = generalInfo.supplier_email;
            string ret = SendMailPO(generalInfo, "变更通知", State.getEmployeeEmailBySysUserId(generalInfo.requestor), supplierEmail, body, attFiles);
        }

    }
}