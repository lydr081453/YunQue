using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESP.Framework.Entity;
using ESP.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data;
using System.Data.Common;
using ESP.Framework.DataAccess.Utilities;
using System.Data.SqlClient;
using WorkFlowModel;
using WorkFlowImpl;
using WorkItemData = WorkFlowModel.WorkItemData;
using System.Web;

namespace ESP.Framework.SqlDataAccess
{
    public class SqlTaskItemDataProvider : ESP.Framework.DataAccess.ITaskItemDataProvider
    {

        /// <summary>
        /// 采购总监ID
        /// </summary>
        private static int majordomoId = int.Parse(ESP.Configuration.ConfigurationManager.Items["majordomoId"]);
        /// <summary>
        /// 媒介审批人ID
        /// </summary>
        private static int mediaId = int.Parse(ESP.Configuration.ConfigurationManager.Items["mediaId"]);
        /// <summary>
        /// 广告媒体采买审批人ID
        /// </summary>
        private static int billId = int.Parse(ESP.Configuration.ConfigurationManager.Items["billId"]);

        /// <summary>
        /// 项目号业务审批url
        /// </summary>
        private string ProjectBizUrl = "/project/AuditOperation.aspx?ProjectID={0}";
        /// <summary>
        /// 项目号财务审批url
        /// </summary>
        private string ProjectFinanceUrl = "/project/FinancialAuditOperation.aspx?ProjectID={0}";
        /// <summary>
        /// 支持方业务审批url
        /// </summary>
        private string SupporterBizUrl = "/project/SupporterAuditOperation.aspx?SupportID={0}&ProjectID={1}";
        /// <summary>
        /// 支持方财务审批url
        /// </summary>
        private string SupporterFinanceUrl = "/project/FinancialSupporter.aspx?SupportID={0}&ProjectID={1}";
        /// <summary>
        ///付款通知业务审批url
        /// </summary>
        private string NotifyBizUrl = "/Return/BizOperation.aspx?PaymentID={0}";
        /// <summary>
        /// 付款通知财务审批url
        /// </summary>
        private string NotifyFinanceUrl = "/Return/FinancialOperation.aspx?PaymentID={0}";
        /// <summary>
        /// 付款申请业务审批url
        /// </summary>
        private string PaymentBizUrl = "/Purchase/PaymentEdit.aspx?ReturnID={0}";
        /// <summary>
        /// 付款申请财务审批url
        /// </summary>
        private string PaymentFinanceUrl = "/Purchase/FinancialAudit.aspx?ReturnID={0}";
        /// <summary>
        /// 审批状态url
        /// </summary>
        private string AuditUrl = "/project/ProjectWorkFlow.aspx?Type={0}&FlowID={1}";
        /// <summary>
        /// 请假审批URL
        /// </summary>
        private string LeaveAuditUrl = "/Attendance/LeaveAuditEdit.aspx?attendanceid={0}";
        /// <summary>
        /// 考勤审批URL
        /// </summary>
        private string AttendanceAuditUrl = "/Attendance/AttendanceAuditEdit.aspx?attendanceid={0}";
        /// <summary>
        /// OT审批URL
        /// </summary>
        private string OverTimeAuditUrl = "/Overtime/OvertimeAuditEdit.aspx?overTimeId={0}";
        /// <summary>
        /// 押金业务审批URL
        /// </summary>
        private string ForgiftBizUrl = "/ForeGift/operationAudit.aspx?ReturnID={0}";
        /// <summary>
        /// 押金财务审批URL
        /// </summary>
        private string ForgiftFinanceUrl = "/ForeGift/financeAudit.aspx?ReturnID={0}";
        /// <summary>
        /// 离职审批URL
        /// </summary>
        private string DimissionAuditUrl = "/HR/Dimission/DimissionAuditEdit.aspx?dimissionid={0}";
        /// <summary>
        /// 离职交接
        /// </summary>
        private string DimissionReceiverUrl = "/HR/Dimission/ReceiverAuditList.aspx";
        /// <summary>
        /// 证据链审批
        /// </summary>
        private string ContractFileUploadUrl = "/ContractFiles/ContractAudit.aspx?ProjectID={0}";
        /// <summary>
        /// 发票申请
        /// </summary>
        private string InvoiceApplyUrl = "/ApplyForInvioce/ApplyForInvioceAudit.aspx?ProjectID={0}";

        private string RequestForSealUrl = "/RequestForSeal/RequestForSealAudit.aspx?RfsId={0}";
        /// <summary>
        /// 固定资产领用人确认
        /// </summary>
        private string AssetReceivingUrl = "/ITAsset/AssetView.aspx?op=5&assetId={0}";
        private string AssetScrapUrl = "/ITAsset/AssetView.aspx?op=7&assetId={0}";

        private string ConsumptionAuditUrl = "/Consumption/ConsumptionAudit.aspx?batchId={0}";
        private string RebateRegistrationAuditUrl = "/RebateRegistration/RebateRegistrationAudit.aspx?batchId={0}";

        //        private string UrlEncode(string s)
        //        {
        //            return HttpContext.Current.Server.UrlEncode(s);
        //        }

        private string UrlEncodeNew(string s)
        {
            return HttpUtility.UrlEncode(s);
        }

        //        private string getDelegateUsers(int UserID)
        //        {
        //            string ret = string.Empty;
        //            IList<ESP.Framework.Entity.AuditBackUpInfo> delegateUsers = ESP.Framework.BusinessLogic.AuditBackUpManager.GetModelsByBackUpUserID(UserID);
        //            foreach (ESP.Framework.Entity.AuditBackUpInfo model in delegateUsers)
        //            {
        //                ret += model.UserID.ToString() + ",";
        //            }
        //            return ret.TrimEnd(',');
        //        }

        private int[] getUserByDelegateUser(int UserID)
        {
            string ret = string.Empty;
            ESP.Framework.Entity.AuditBackUpInfo delegateUsers = ESP.Framework.BusinessLogic.AuditBackUpManager.GetModelByUserID(UserID);
            if (null != delegateUsers && delegateUsers.BackupUserID != UserID)
            {
                return new int[] { delegateUsers.BackupUserID };
            }
            else
            {
                return new int[0];
            }
        }

        /// <summary>
        /// 获取所有待办事宜
        /// </summary>
        /// <param name="key">Cache Key</param>
        /// <param name="userId">当前登录人的ID</param>
        /// <returns>待办事宜的List</returns>
        public IDictionary<string, IList<TaskItemInfo>> GetTaskItems(string key, int userId)
        {
            if (HttpContext.Current.Cache[key] != null)
            {
                IDictionary<int, IDictionary<string, IList<TaskItemInfo>>> dict =
                    (IDictionary<int, IDictionary<string, IList<TaskItemInfo>>>)HttpContext.Current.Cache[key];
                if (dict.ContainsKey(userId)) return dict[userId];
            }
            return null;
        }

        /// <summary>
        /// 获取所有人的所有代办事宜
        /// </summary>
        /// <returns></returns>
        public IDictionary<int, IDictionary<string, IList<TaskItemInfo>>> GetAllTaskItems()
        {
            //尚未实现，由葛维完成

            //数据库联接字符串
            string ConnectionString = ESP.Configuration.ConfigurationManager.Items["CG"];
            //财务系统的主机头
            string financialHeader = ESP.Configuration.ConfigurationManager.Items["FinancialHeader"];
            //采购系统的主机头
            string PurchaseHeader = ESP.Configuration.ConfigurationManager.Items["PurchaseHeader"];
            //考勤系统的主机头
            string AttendanceHeader = ESP.Configuration.ConfigurationManager.Items["AttendanceHeader"];
            //人事系统的主机头
            string HRHeader = ESP.Configuration.ConfigurationManager.Items["HRHeader"];

            //返回参数
            IDictionary<int, IDictionary<string, IList<TaskItemInfo>>> dict =
                new Dictionary<int, IDictionary<string, IList<TaskItemInfo>>>();
            IDictionary<string, IList<TaskItemInfo>> dici;
            IList<TaskItemInfo> list;

            try
            {
                Database db = DatabaseFactory.CreateDatabase(ConnectionString);
                DbCommand cmd = db.GetStoredProcCommand("P_GetAllTaskItems");

                System.Data.DataTable dt = db.ExecuteDataSet(cmd).Tables[0];

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    TaskItemInfo model = new TaskItemInfo();
                    if (null != dt.Rows[i]["operationtype"] && dt.Rows[i]["operationtype"].ToString() != "采购申请单业务审核列表")
                    {
                        model.ApplicantID = Convert.ToInt32(dt.Rows[i]["ApplicantID"].ToString());
                        model.ApplicantName = dt.Rows[i]["ApplicantName"].ToString();
                        object obj = dt.Rows[i]["AppliedTime"];
                        if (obj == null || obj == DBNull.Value)
                            model.AppliedTime = new DateTime(1900, 1, 1);
                        else
                            model.AppliedTime = Convert.ToDateTime(obj);

                        model.AppliedTime = model.AppliedTime.ToUniversalTime();

                        model.Description = dt.Rows[i]["Description"].ToString();
                        model.FormID = Convert.ToInt32(dt.Rows[i]["FromID"]);
                        model.FormNumber = dt.Rows[i]["FormNumber"].ToString();
                        model.FormType = dt.Rows[i]["FormType"].ToString();
                        model.ApproverID = Convert.ToInt32(dt.Rows[i]["ApproverID"].ToString());
                        model.ApproverName = dt.Rows[i]["ApproverName"].ToString();
                        object audittype = dt.Rows[i]["audittype"].ToString();
                        switch (dt.Rows[i]["operationtype"].ToString())
                        {
                            case "待分公司审核":
                                model.Url = PurchaseHeader + "/Default.aspx?contentUrl=" +
                                            UrlEncodeNew(
                                                string.Format(
                                                    "/Purchase/Requisition/Filiale_RequisitionEdit.aspx?GeneralID={0}",
                                                    model.FormID));
                                model.ApproversUrl = PurchaseHeader +
                                                     string.Format(
                                                         "/Purchase/Requisition/operationAuditView.aspx?GeneralID={0}",
                                                         model.FormID);
                                break;
                            case "待物料审核":
                                model.Url = PurchaseHeader + "/Default.aspx?contentUrl=" +
                                            UrlEncodeNew(string.Format(
                                                          "/Purchase/Requisition/EditOrder.aspx?GeneralID={0}",
                                                          model.FormID));
                                model.ApproversUrl = PurchaseHeader +
                                                     string.Format(
                                                         "/Purchase/Requisition/operationAuditView.aspx?GeneralID={0}",
                                                         model.FormID);
                                break;
                            case "待风控审核":
                                model.Url = PurchaseHeader + "/Default.aspx?contentUrl=" +
                                            UrlEncodeNew(
                                                string.Format(
                                                    "/Purchase/Requisition/MajordomoAudit.aspx?backUrl=AuditRequistion.aspx&GeneralID={0}",
                                                    model.FormID));
                                model.ApproversUrl = PurchaseHeader +
                                                     string.Format(
                                                         "/Purchase/Requisition/operationAuditView.aspx?GeneralID={0}",
                                                         model.FormID);
                                break;
                            case "待采购总监审核":
                                model.Url = PurchaseHeader + "/Default.aspx?contentUrl=" +
                                            UrlEncodeNew(
                                                string.Format(
                                                    "/Purchase/Requisition/MajordomoAudit.aspx?backUrl=AuditRequistion.aspx&GeneralID={0}",
                                                    model.FormID));
                                model.ApproversUrl = PurchaseHeader +
                                                     string.Format(
                                                         "/Purchase/Requisition/operationAuditView.aspx?GeneralID={0}",
                                                         model.FormID);
                                break;
                            case "待媒介总监审核":
                                model.Url = PurchaseHeader + "/Default.aspx?contentUrl=" +
                                            UrlEncodeNew(
                                                string.Format(
                                                    "/Purchase/Requisition/prMediaAudit.aspx?backUrl=prMediaAuditList.aspx&GeneralID={0}",
                                                    model.FormID));
                                model.ApproversUrl = PurchaseHeader +
                                                     string.Format(
                                                         "/Purchase/Requisition/operationAuditView.aspx?GeneralID={0}",
                                                         model.FormID);
                                break;
                            case "待广告媒体采买审批":
                                model.Url = PurchaseHeader + "/Default.aspx?contentUrl=" +
                                            UrlEncodeNew(string.Format(
                                                          "/Purchase/Requisition/prADAudit.aspx?GeneralID={0}",
                                                          model.FormID));
                                model.ApproversUrl = PurchaseHeader +
                                                     string.Format(
                                                         "/Purchase/Requisition/operationAuditView.aspx?GeneralID={0}",
                                                         model.FormID);
                                break;
                            case "待审批项目号":
                                if (Convert.ToInt32(audittype) < 10)
                                {
                                    model.Url = financialHeader + "/Default.aspx?contentUrl=" +
                                                UrlEncodeNew(string.Format(ProjectBizUrl, model.FormID));
                                }
                                else
                                {
                                    model.Url = financialHeader + "/Default.aspx?contentUrl=" +
                                                UrlEncodeNew(string.Format(ProjectFinanceUrl, model.FormID));
                                }
                                model.ApproversUrl = financialHeader + string.Format(AuditUrl, "project", model.FormID);
                                break;
                            case "待审批支持方":

                                if (Convert.ToInt32(audittype) < 10)
                                {
                                    model.Url = financialHeader + "/Default.aspx?contentUrl=" +
                                                UrlEncodeNew(string.Format(SupporterBizUrl, model.FormID,
                                                                        dt.Rows[i]["Url"].ToString()));
                                }
                                else
                                {
                                    model.Url = financialHeader + "/Default.aspx?contentUrl=" +
                                                UrlEncodeNew(string.Format(SupporterFinanceUrl, model.FormID,
                                                                        dt.Rows[i]["Url"].ToString()));
                                }
                                model.ApproversUrl = financialHeader +
                                                     string.Format(AuditUrl, "supporter", model.FormID);
                                break;
                            case "待审批付款申请":
                                ESP.Finance.Entity.ReturnInfo returnModel = ESP.Finance.BusinessLogic.ReturnManager.GetModel(model.FormID);
                                if (returnModel.ReturnType != (int)ESP.Purchase.Common.PRTYpe.PN_ForeGift)
                                {
                                    if (Convert.ToInt32(audittype) < 10)
                                    {
                                        model.Url = financialHeader + "/Default.aspx?contentUrl=" +
                                                    UrlEncodeNew(string.Format(PaymentBizUrl, model.FormID));
                                    }
                                    else
                                    {
                                        model.Url = financialHeader + "/Default.aspx?contentUrl=" +
                                                    UrlEncodeNew(string.Format(PaymentFinanceUrl, model.FormID));
                                    }
                                }
                                else
                                {
                                    if (Convert.ToInt32(audittype) < 10)
                                    {
                                        model.Url = financialHeader + "/Default.aspx?contentUrl=" +
                                                    UrlEncodeNew(string.Format(ForgiftBizUrl, model.FormID));
                                    }
                                    else
                                    {
                                        model.Url = financialHeader + "/Default.aspx?contentUrl=" +
                                                    UrlEncodeNew(string.Format(ForgiftFinanceUrl, model.FormID));
                                    }
                                }
                                model.ApproversUrl = financialHeader + string.Format(AuditUrl, "return", model.FormID);
                                break;
                            case "待审批保理流程":
                                ESP.Finance.Entity.ReturnInfo factorModel = ESP.Finance.BusinessLogic.ReturnManager.GetModel(model.FormID);

                                model.Url = financialHeader + "/Default.aspx?contentUrl=" +
                                                   UrlEncodeNew(string.Format(PaymentFinanceUrl, model.FormID));

                                model.ApproversUrl = financialHeader + string.Format(AuditUrl, "return", model.FormID);
                                break;
                            case "待审批付款通知":

                                if (Convert.ToInt32(audittype) < 10)
                                {
                                    model.Url = financialHeader + "/Default.aspx?contentUrl=" +
                                                UrlEncodeNew(string.Format(NotifyBizUrl, model.FormID));
                                }
                                else
                                {
                                    model.Url = financialHeader + "/Default.aspx?contentUrl=" +
                                                UrlEncodeNew(string.Format(NotifyFinanceUrl, model.FormID));
                                }
                                model.ApproversUrl = financialHeader + string.Format(AuditUrl, "payment", model.FormID);
                                break;
                            case "采购申请单业务审核列表":
                                break;

                            case "待收货列表":
                                model.Url = PurchaseHeader + "/Default.aspx?contentUrl=" +
                                            UrlEncodeNew(string.Format(
                                                          "/Purchase/Requisition/RecipientDetail.aspx?backUrl=RecipientList.aspx&GeneralID={0}",
                                                          model.FormID));
                                model.ApproversUrl = PurchaseHeader +
                                                     string.Format(
                                                         "/Purchase/Requisition/operationAuditView.aspx?GeneralID={0}",
                                                         model.FormID);
                                break;

                            case "收货待确认列表":
                                model.Url = "";
                                model.ApproversUrl = "";
                                break;

                            case "待付款列表":
                                model.Url = PurchaseHeader + "/Default.aspx?contentUrl=" +
                                            UrlEncodeNew(string.Format(
                                                          "/Purchase/Requisition/PaymentRecipientList.aspx?backUrl=PaymentGeneralList.aspx&GeneralID={0}",
                                                          model.FormID));
                                model.ApproversUrl = PurchaseHeader +
                                                     string.Format(
                                                         "/Purchase/Requisition/operationAuditView.aspx?GeneralID={0}",
                                                         model.FormID);
                                break;

                            case "待审批考勤信息":
                                string url = "/Attendance/AuditEdit.aspx?mattertype={0}&matterid={1}&backurl=AuditList.aspx";
                                model.Url = AttendanceHeader + "/Default.aspx?contentUrl=" + UrlEncodeNew(string.Format(url, dt.Rows[i]["Url"], model.FormID));
                                break;

                            case "PR现金借款冲销":
                            case "第三方报销单":
                            case "借款冲销单":
                            case "商务卡报销单":
                            case "报销单":
                            case "支票/电汇付款单":
                            case "现金借款单":
                            case "媒体预付申请":
                            case "机票申请单":
                                model.Url = financialHeader + dt.Rows[i]["Url"].ToString();
                                model.ApproversUrl = financialHeader + string.Format(AuditUrl, "oop", model.FormID);
                                break;
                            case "离职单":
                                model.Url = HRHeader + "/Default.aspx?contentUrl=" + UrlEncodeNew(string.Format(DimissionAuditUrl, model.FormID));
                                model.ApproversUrl = HRHeader +
                                                    string.Format(
                                                        "/HR/Dimission/DimissionAuditStatus.aspx?dimissionId={0}",
                                                        model.FormID);
                                break;
                            case "离职交接":
                                model.Url = HRHeader + "/Default.aspx?contentUrl=" +
                                                   UrlEncodeNew(DimissionReceiverUrl);
                                break;

                            case "证据链":
                                model.Url = financialHeader + "/Default.aspx?contentUrl=" +
                                                    UrlEncodeNew(string.Format(ContractFileUploadUrl, model.FormID));
                                break;
                            case "发票申请":
                                model.Url = financialHeader + "/Default.aspx?contentUrl=" +
                                                    UrlEncodeNew(string.Format(InvoiceApplyUrl, model.FormID));
                                break;
                            case "待审批消耗导入":
                                model.Url = financialHeader + "/Default.aspx?contentUrl=" +
                                                   UrlEncodeNew(string.Format(ConsumptionAuditUrl, model.FormID));
                                model.ApproversUrl = financialHeader + string.Format(AuditUrl, "consumption", model.FormID);
                                break;
                            case "待审批媒体返点导入":
                                model.Url = financialHeader + "/Default.aspx?contentUrl=" +
                                                   UrlEncodeNew(string.Format(RebateRegistrationAuditUrl, model.FormID));
                                model.ApproversUrl = financialHeader + string.Format(AuditUrl, "RebateRegistration", model.FormID);
                                break;
                            case "固定资产领用":
                                model.Url = financialHeader + "/Default.aspx?contentUrl=" +
                                                   UrlEncodeNew(string.Format(AssetReceivingUrl, model.FormID));
                                break;
                            case "固定资产报废":
                                model.Url = financialHeader + "/Default.aspx?contentUrl=" +
                                                   UrlEncodeNew(string.Format(AssetScrapUrl, model.FormID));
                                break;
                            case "用印申请":
                                model.Url = AttendanceHeader + "/Default.aspx?contentUrl=" + UrlEncodeNew(string.Format(RequestForSealUrl, model.FormID));
                                break;
                            default:
                                break;
                        }
                    }
                    else
                    {
                        ESP.Purchase.Entity.GeneralInfo general =
                            (ESP.Purchase.Entity.GeneralInfo)
                            ESP.Finance.Utility.UtilSerializeFactory.ObjectDeserialize<ESP.Purchase.Entity.GeneralInfo>(
                                (byte[])dt.Rows[i]["dataitem"]);

                        model.ApplicantID = general.requestor;
                        model.ApplicantName = general.requestorname;
                        model.AppliedTime = general.requisition_committime;

                        model.AppliedTime = model.AppliedTime.ToUniversalTime();

                        model.Description = general.project_descripttion;
                        model.FormID = general.id;
                        model.FormNumber = general.PrNo;
                        model.FormType = "采购申请单";
                        model.ApproverID = 0;
                        model.ApproverName = "";
                        model.Url = PurchaseHeader + "/Default.aspx?contentUrl=" +
                                    UrlEncodeNew(
                                        string.Format(
                                            "/Purchase/Requisition/OperationAudit.aspx?backUrl=OperationAuditList.aspx&GeneralID={0}",
                                            model.FormID));
                        model.ApproversUrl =
                            string.Format(
                                PurchaseHeader + "/Purchase/Requisition/operationAuditView.aspx?GeneralID={0}",
                                model.FormID);
                    }
                    if (dict.ContainsKey(int.Parse(dt.Rows[i]["userid"].ToString())))
                    {
                        dici = dict[int.Parse(dt.Rows[i]["userid"].ToString())];
                        if (dici.ContainsKey(dt.Rows[i]["operationtype"].ToString()))
                        {
                            list = dici[dt.Rows[i]["operationtype"].ToString()];
                            list.Add(model);
                        }
                        else
                        {
                            list = new List<TaskItemInfo>();
                            list.Add(model);
                            dici.Add(dt.Rows[i]["operationtype"].ToString(), list);
                        }
                    }
                    else
                    {
                        dici = new Dictionary<string, IList<TaskItemInfo>>();
                        list = new List<TaskItemInfo>();
                        list.Add(model);
                        dici.Add(dt.Rows[i]["operationtype"].ToString(), list);
                        dict.Add(int.Parse(dt.Rows[i]["userid"].ToString()), dici);
                    }
                }
                IDictionary<int, IDictionary<string, IList<TaskItemInfo>>> tmp =
                    new Dictionary<int, IDictionary<string, IList<TaskItemInfo>>>();
                IDictionary<string, IList<TaskItemInfo>> tmp2 = new Dictionary<string, IList<TaskItemInfo>>();
                IList<TaskItemInfo> tmp3 = new List<TaskItemInfo>();

                dict = ProcessTaskItems(dict);
            }
            catch (Exception e)
            {
                ESP.Logging.Logger.Add(e.Message + "#" + e.StackTrace);
            }
            return dict;
        }


        private IDictionary<int, IDictionary<string, IList<TaskItemInfo>>> ProcessTaskItems(IDictionary<int, IDictionary<string, IList<TaskItemInfo>>> workitems)
        {
            IDictionary<int, IDictionary<string, IList<TaskItemInfo>>> copy = new Dictionary<int, IDictionary<string, IList<TaskItemInfo>>>(workitems);
            foreach (KeyValuePair<int, IDictionary<string, IList<TaskItemInfo>>> en in workitems)
            {
                int userId = en.Key;
                IDictionary<string, IList<TaskItemInfo>> userItems = en.Value;

                int[] backupUsers = getUserByDelegateUser(userId);

                foreach (int bkUserId in backupUsers)
                {
                    IDictionary<string, IList<TaskItemInfo>> bkUserItems;
                    if (copy.ContainsKey(bkUserId))
                    {
                        bkUserItems = copy[bkUserId];
                    }
                    else
                    {
                        bkUserItems = new Dictionary<string, IList<TaskItemInfo>>();
                        copy.Add(bkUserId, bkUserItems);
                    }
                    MergeDictionary(bkUserItems, userItems);
                }

            }

            return copy;
        }

        private int[] GetBackupUsers(int userId)
        {
            throw new NotImplementedException();
        }

        public static void MergeDictionary(IDictionary<string, IList<TaskItemInfo>> dest, IDictionary<string, IList<TaskItemInfo>> source)
        {
            foreach (KeyValuePair<string, IList<TaskItemInfo>> en in source)
            {
                if (dest.ContainsKey(en.Key))
                {
                    MergeList(dest[en.Key], en.Value);
                }
                else
                {
                    dest.Add(en.Key, new List<TaskItemInfo>(en.Value));
                }
            }
        }

        public static void MergeList(IList<TaskItemInfo> dest, IList<TaskItemInfo> source)
        {
            foreach (TaskItemInfo obj in source)
            {
                dest.Add(obj);
            }
        }

        public static void LoadCostRecord()
        {
            ESP.Finance.BusinessLogic.CostRecordManager.InsertCost();
        }
    }
}
