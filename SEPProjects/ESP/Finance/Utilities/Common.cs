using System;
using System.Collections.Generic;
using System.Text;
using System.Web.Script.Serialization;
using System.Data;
using System.Linq;
using System.Reflection;
using ESP.Compatible;
using System.IO;
using System.Globalization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using ESP.Framework.Entity;
using ESP.Framework.BusinessLogic;

namespace ESP.Finance.Utility
{
    public class Common
    {
        #region Url
        /// <summary>
        /// 项目号业务审批url
        /// </summary>
        public static string ProjectBizUrl = "/project/AuditOperation.aspx?ProjectID={0}";
        /// <summary>
        /// 项目号财务审批url
        /// </summary>
        public static string ProjectFinanceUrl = "/project/FinancialAuditOperation.aspx?ProjectID={0}";
        /// <summary>
        /// 支持方业务审批url
        /// </summary>
        public static string SupporterBizUrl = "/project/SupporterAuditOperation.aspx?SupportID={0}&ProjectID={1}";
        /// <summary>
        /// 支持方财务审批url
        /// </summary>
        public static string SupporterFinanceUrl = "/project/FinancialSupporter.aspx?SupportID={0}&ProjectID={1}";
        /// <summary>
        ///付款通知业务审批url
        /// </summary>
        public static string NotifyBizUrl = "/Return/BizOperation.aspx?PaymentID={0}";
        /// <summary>
        /// 付款通知财务审批url
        /// </summary>
        public static string NotifyFinanceUrl = "/Return/FinancialOperation.aspx?PaymentID={0}";

        public static string NotifyDelayUrl = "/Return/FinancialExtensionOperation.aspx?PaymentID={0}";
        /// <summary>
        /// 付款申请业务审批url
        /// </summary>
        public static string PaymentBizUrl = "/Purchase/PaymentEdit.aspx?ReturnID={0}";
        /// <summary>
        /// 付款申请财务审批url
        /// </summary>
        public static string PaymentFinanceUrl = "/Purchase/FinancialAudit.aspx?ReturnID={0}";
        /// <summary>
        /// 审批状态url
        /// </summary>
        public static string AuditUrl = "/project/ProjectWorkFlow.aspx?Type={0}&FlowID={1}";
        /// <summary>
        /// 押金业务审批URL
        /// </summary>
        public static string ForgiftBizUrl = "/ForeGift/operationAudit.aspx?ReturnID={0}";
        /// <summary>
        /// 押金财务审批URL
        /// </summary>
        public static string ForgiftFinanceUrl = "/ForeGift/financeAudit.aspx?ReturnID={0}";

        public static string RefundAuditUrl = "/Refund/RefundAudit.aspx?ModelID={0}";
        #endregion

        public static string[] FormType_Names = { "", "项目号", "支持方", "付款通知", "付款申请", "报销", "批次付款", "PR", "ProxyPnReport", "证据链", "发票申请", "消耗导入审批","返点导入审批","用印审批" };
        public static Dictionary<int, string> ReturnType_Names = new Dictionary<int, string> { { 30, "常规报销" }, { 31, "支票/电汇付款" }, {32,"现金借款" },
                                                                {33,"商务卡报销" }, {34,"PR现金借款冲销" }, {35,"第三方报销" }, {36,"借款冲销单" }, {37,"媒体预付申请" } };

        public static string GetLocalPath(string filename)
        {
            return System.Web.HttpContext.Current.Server.MapPath(filename);
        }

        public static ESP.Framework.Entity.UserInfo CurrentUser
        {
            //  get { return System.Web.HttpContext.Current.Session["_LSF_CurrentUser"] as Employee; }
            get { return ESP.Framework.BusinessLogic.UserManager.Get(); }
        }

        /// <summary>
        /// 当前员工系统ID
        /// </summary>
        public static int CurrentUserSysID
        {
            get { return Common.CurrentUser == null ? 0 : Convert.ToInt32(CurrentUser.UserID); }
        }

        /// <summary>
        /// 当前员工登录名称
        /// </summary>
        public static string CurrentUserName
        {
            get { return CurrentUser == null ? string.Empty : CurrentUser.Username; }
        }

        /// <summary>
        /// 当前员工编号
        /// </summary>
        public static string CurrentUserCode
        {
            get
            {
                EmployeeInfo employee = EmployeeManager.Get(ESP.Framework.BusinessLogic.UserManager.GetCurrentUserID());
                return employee == null ? string.Empty : employee.Code;
            }
        }



        /// <summary>
        /// 当前员工中文姓名
        /// </summary>
        public static string CurrentUserEmpName
        {
            get { return CurrentUser == null ? string.Empty : CurrentUser.FullNameCN; }
        }

        /// <summary>
        /// 项目使用状态描述
        /// </summary>
        public static string[] ProjectInUse_Names = { "暂停", "正常使用" };

        public static Dictionary<int, string> AuditHistoryStatus_Names = new Dictionary<int, string>() { { 1, "审批通过" }, { 2, "审批驳回" }, { 8, "证据链驳回" }, { 9, "证据链通过" }, { 10, "发票申请驳回" }, { 11, "发票申请通过" } };

        
        public enum CustomerAttachStatus : int
        {
            /// <summary>
            /// 保存
            /// </summary>
            Saved = 0,
            /// <summary>
            /// 使用
            /// </summary>
            Used = 1,
            /// <summary>
            /// 停用
            /// </summary>
            Stoped = 2,
        }
        public static string[] CustomerAttachStatus_Names = { "保存", "使用", "停用" };

        public enum RebateRegistration_Status : int
        {
            Deleted = 0,
            Saved = 1,
            Submited = 2,
            Audited = 3
        }
        public static string[] RebateRegistrationStatus_Names = { "删除", "保存","提交","审核通过"};

        public enum ApplyForInvioce_FlowTo : int
        {
            /// <summary>
            /// 客户
            /// </summary>
            Customer = 0,
            /// <summary>
            /// 媒体返点
            /// </summary>
            Media = 1,
            /// <summary>
            /// 客户返点
            /// </summary>
            CustomerRebate = 2,
        }
        public static string[] ApplyForInvioceFlowTo_Names = { "客户", "媒体返点","客户返点" };

        public enum Invoice_Type : int
        {
            /// <summary>
            /// 增值税普通发票
            /// </summary>
            Ordinary = 0,
            /// <summary>
            /// 增值税专用发票
            /// </summary>
            Special = 1,
        }
        public static string[] InvoiceType_Names = { "普票", "专票" };
    }


    public enum DeleteResult : int
    {
        /// <summary>
        /// 失败的
        /// </summary>
        Failed = 0,

        /// <summary>
        /// 没有执行
        /// </summary>
        UnExecute = 1,

        /// <summary>
        /// 成功
        /// </summary>
        Succeed = 5000

    }

    public enum OperateType : int
    {
        /// <summary>
        /// 查询
        /// </summary>
        Query = 0,
        /// <summary>
        /// 插入
        /// </summary>
        Insert = 1,
        /// <summary>
        /// 更新
        /// </summary>
        Update = 2,
        /// <summary>
        /// 删除
        /// </summary>
        Delete = 3
    }

    public enum AuditHistoryStatus : int
    {
        /// <summary>
        /// 通过审核
        /// </summary>
        PassAuditing = 1,
        /// <summary>
        /// 尚未审核
        /// </summary>
        UnAuditing = 0,
        /// <summary>
        /// 审核驳回
        /// </summary>
        TerminateAuditing = 2,
        /// <summary>
        /// 等待合同
        /// </summary>
        WaitingContract = 3,
        /// <summary>
        /// 付款申请时，财务驳回到申请人，申请人提交到财务
        /// </summary>
        TerminateAuditingFinance = 4,
        /// <summary>
        /// 申请人撤销
        /// </summary>
        RequestorCancel = 5,
        /// <summary>
        /// 提交撤销变更过的项目
        /// </summary>
        CommitChangedProject = 6,
        /// <summary>
        /// 留言
        /// </summary>
        Tip = 7,
        /// <summary>
        /// 证据链审核驳回
        /// </summary>
        ContractAudit_Rejected = 8,
        /// <summary>
        /// 证据链审核通过
        /// </summary>
        ContractAudit_Audited = 9,
        /// <summary>
        /// 发票申请审核驳回
        /// </summary>
        ApplyForInvioceAudit_Rejected = 10,
        /// <summary>
        /// 发票申请审核通过
        /// </summary>
        ApplyForInvioceAudit_Audited = 11,
    }

    public enum AuditFormType
    {
        /// <summary>
        /// 创建项目
        /// </summary>
        ProjectCreate = 0,

        /// <summary>
        /// 创建支持方
        /// </summary>
        ProjectSupporters = 1,


        /// <summary>
        /// 正常报销
        /// </summary>
        DufaultExpense = 99
    }
    /// <summary>
    /// 状态
    /// </summary>
    public enum Status : int
    {
        /// <summary>
        /// 保存
        /// </summary>
        Saved = 0,
        /// <summary>
        /// 提交
        /// </summary>
        Submit = 1,

        /// <summary>
        /// 业务驳回
        /// </summary>
        BizReject = 10,

        /// <summary>
        /// 业务审批中
        /// </summary>
        BizAuditing = 11,

        /// <summary>
        /// 业务审批完成
        /// </summary>
        BizAuditComplete = 12,
        /// <summary>
        /// 待风控审批
        /// </summary>
        WaitRiskControl = 13,
        /// <summary>
        /// 等待业务提交合同
        /// </summary>
        Waiting = 19,
        /// <summary>
        /// 合同驳回
        /// </summary>
        ContractReject = 20,

        /// <summary>
        /// 合同审批
        /// </summary>
        ContractAudit = 21,


        /// <summary>
        ///财务驳回
        /// </summary>
        FinanceReject = 30,

        /// <summary>
        /// 财务审批中
        /// </summary>
        FinanceAuditing = 31,

        /// <summary>
        /// 财务审批完成
        /// </summary>
        FinanceAuditComplete = 32,

        /// <summary>
        /// 项目变更时，负责人确认
        /// </summary>
        ResponserConfirmed = 2,
        /// <summary>
        /// 项目预关闭
        /// </summary>
        ProjectPreClose = 33,
        /// <summary>
        /// 项目已关闭
        /// </summary>
        ProjectClosed = 34,
        /// <summary>
        /// 取消项目
        /// </summary>
        projectCancel = 35

    }

    /// <summary>
    /// 项目使用状态
    /// </summary>
    public enum ProjectInUse : int
    {
        /// <summary>
        /// 暂停
        /// </summary>
        Disable = 0,
        /// <summary>
        /// 正常使用
        /// </summary>
        Use = 1
    }

    public enum PaymentStatus : int
    {
        //when 0 then '帐期已创建' when 1 then '保存' when 2 then '等待付款' when 3 then '待定2' when 4 then '待定3' when 5 then '已付款
        //驳回
        Rejected = -1,
        Created = 0,
        /// <summary>
        /// 帐期已创建
        /// </summary>
        Save = 1,//创建帐期，未和收货关联
        /// <summary>
        /// 等待付款
        /// </summary>
        Submit = 2,//已和收货关联，未付款
        /// <summary>
        /// 待定1
        /// </summary>
        Other1 = 3,//待定1
        /// <summary>
        /// 待定2
        /// </summary>
        Other2 = 4,//待定2
        /// <summary>
        /// 已付款
        /// </summary>
        Over = 5,//已付款（由财务部门最终支付时确定）
        /// <summary>
        /// 预审审核
        /// </summary>
        PrepareAudit = 98,
        /// <summary>
        /// 项目负责人审核
        /// </summary>
        ProjectManagerAudit = 99,
        /// <summary>
        /// 总监审核
        /// </summary>
        MajorAudit = 100,
        /// <summary>
        /// 总经理审核
        /// </summary>
        GeneralManagerAudit = 101,
        /// <summary>
        /// CEO审核
        /// </summary>
        CEOAudit = 102,
        /// <summary>
        /// FA审核
        /// </summary>
        FAAudit = 103,
        /// <summary>
        /// 集团财务总监审核
        /// </summary>
        EddyAudit = 104,
        /// <summary>
        /// 已收货
        /// </summary>
        Recived = 105,
        /// <summary>
        /// 附加收货
        /// </summary>
        FJ_Recived = 106,
        /// <summary>
        /// 机票前台确认
        /// </summary>
        Ticket_ReceptionConfirm = 107,
        /// <summary>
        /// 机票供应商确认
        /// </summary>
        Ticket_SupplierConfirm = 108,
        /// <summary>
        /// 机票签收
        /// </summary>
        Ticket_Received = 109,
        /// <summary>
        /// 财务出纳1审核
        /// </summary>
        FinanceLevel1 = 110,
        /// <summary>
        /// 财务出纳2审核
        /// </summary>
        FinanceLevel2 = 120,
        /// <summary>
        /// waiting for li yane audit
        /// </summary>
        FinanceLevel3 = 130,

        /// <summary>
        /// 待收货
        /// </summary>
        WaitReceiving = 136,
        /// <summary>
        /// 冲销中
        /// </summary>
        Receiving = 137,
        /// <summary>
        /// 冲销单待提交
        /// </summary>
        ConfirmReceiving = 138,
        /// <summary>
        /// 冲销完成
        /// </summary>
        ReceivingEnd = 139,
        /// <summary>
        /// 财务审核完成
        /// </summary>
        FinanceComplete = 140,
        /// <summary>
        /// 驳回到申请人，申请人再次提交直接提交到财务，不需要业务审核
        /// </summary>
        FinanceReject = 150,
        /// <summary>
        /// 财务挂账
        /// </summary>
        FinanceHold = 6,
        /// <summary>
        /// 平帐
        /// </summary>
        FinanceOver = 7,
        /// <summary>
        /// 待初审人审核
        /// </summary>
        PurchaseFirst = 90,
        /// <summary>
        /// 待采购一级总监审核
        /// </summary>
        PurchaseMajor1 = 91,
        /// <summary>
        /// 待采购二级总监审核
        /// </summary>
        // PurchaseMajor2 = 92,
        /// <summary>
        /// 腾信抵扣状态
        /// </summary>
        FinanceDiscount = 170
    }

    public enum PaymentExtensionStatus : int
    {
        /// <summary>
        /// 延期保存，转到负责任审判
        /// </summary>
        Save = 1,

        /// <summary>
        /// 项目负责人已审核，转到财务审批
        /// </summary>
        PrepareAudit = 2,
        /// <summary>
        /// 财务已审核，转至收款
        /// </summary>
        FinanceAudit = 3,
        /// <summary>
        /// 已收款
        /// </summary>
        Paymented = 100,
        /// <summary>
        /// 收款已登记
        /// </summary>
        PaymentedRegister = 110,
    }

    public static class auditorType
    {
        #region 业务审批类型

        /// <summary>
        /// 预审
        /// </summary>
        public static int operationAudit_Type_YS = 1;
        /// <summary>
        /// 总监审批
        /// </summary>
        public static int operationAudit_Type_ZJSP = 2;
        /// <summary>
        /// 总监知会
        /// </summary>
        public static int operationAudit_Type_ZJZH = 3;
        /// <summary>
        /// 总监知会
        /// </summary>
        public static int operationAudit_Type_ZJFJ = 4;
        /// <summary>
        /// 总经理审批
        /// </summary>
        public static int operationAudit_Type_ZJLSP = 5;
        /// <summary>
        /// 总经理知会
        /// </summary>
        public static int operationAudit_Type_ZJLZH = 6;
        /// <summary>
        /// 总经理附加
        /// </summary>
        public static int operationAudit_Type_ZJLFJ = 7;
        /// <summary>
        /// CEO审批
        /// </summary>
        public static int operationAudit_Type_CEO = 8;
        /// <summary>
        /// FA审批
        /// </summary>
        public static int operationAudit_Type_FA = 9;
        public static int operationAudit_Type_Contract = 10;
        public static int operationAudit_Type_Financial = 11;
        public static int operationAudit_Type_Financial2 = 12;
        public static int operationAudit_Type_Financial3 = 13;
        public static int purchase_first = 14;//采购初审人
        public static int operationAudit_Type_RiskControl = 15;//风控
        public static int purchase_major2 = 16;//采购二级总监
        /// <summary>
        /// 项目负责人
        /// </summary>
        public static int operationAudit_Type_XMFZ = 17;
        /// <summary>
        /// 集团财务总监
        /// </summary>
        public static int operationAudit_Type_FinancialMajor = 18;
        /// <summary>
        /// 附加收货
        /// </summary>
        public static int operationAudit_Type_ReciveFJ = 19;
        public static string[] operationAudit_Type_Names = { "", "预审", "总监级审批", "总监级知会", "总监级附加", "总经理级审批", "总经理级知会", "总经理级附加", "CEO级审批", "FA审批", "", "", "", "", "", "风控审批", "", "项目负责人审批", "集团财务总监", "附加收货" };
        #endregion
    }

    /// <summary>
    /// 数据库中记录状态
    /// </summary>
    public enum RecordStatus : int
    {
        /// <summary>
        /// 可用
        /// </summary>
        Usable = 0,
        /// <summary>
        /// 已经删除
        /// </summary>
        Del = 1
    }

    /// <summary>
    /// 合同是否正在使用
    /// </summary>
    public enum ContractUsable : int
    {

        /// <summary>
        /// 已作废
        /// </summary>
        UnUsable = 0,
        /// <summary>
        /// 正在使用
        /// </summary>
        Usable = 1
    }

    public static class QueryString
    {
        public static string AddParam(this string querystring, string paramname, object value)
        {
            string q = querystring.Substring(querystring.IndexOf('?') + 1);
            q = RemoveParam(querystring, paramname);
            q += string.Format("&{0}={1}", paramname, value == null ? string.Empty : value.ToString());
            querystring = q;
            return q;
        }


        public static string ModifyParam(this string querystring, string paramname, object value)
        {
            if (value == null) return querystring;
            string q = querystring.Substring(querystring.IndexOf('?') + 1);
            if (q.IndexOf(paramname) == -1) return q;
            string[] ps = q.Split('&');
            string newps = string.Empty;
            for (int i = 0; i < ps.Length; i++)
            {
                if (ps[i].Split('=')[0] == paramname)
                {
                    ps[i].Split('=')[1] = value.ToString();
                }
                newps += ps[i];
            }
            querystring = newps;
            return newps;
        }

        public static string RemoveParam(this string querystring, string paramname)
        {
            string q = querystring.Substring(querystring.IndexOf('?') + 1);
            string[] ps = q.Split('&');
            string newps = string.Empty;
            for (int i = 0; i < ps.Length; i++)
            {
                if (ps[i].Split('=')[0] != paramname)
                {
                    newps += "&" + ps[i];
                }

            }
            querystring = newps.TrimStart('&');
            return querystring;
        }
    }
    /// <summary>
    /// 应收状态
    /// </summary>
    public enum ReturnStatus : int
    {
        /// <summary>
        /// 保存
        /// </summary>
        Save = 0,
        /// <summary>
        /// 提交
        /// </summary>
        Submit = 1,
        /// <summary>
        /// 总监确认
        /// </summary>
        MajorCommit = 2,
        /// <summary>
        /// 财务挂账
        /// </summary>
        FinancialHold = 3,
        /// <summary>
        /// 平帐
        /// </summary>
        FinancialOver = 4
    }


    public enum FixedAssetStatus : int
    {
        /// <summary>
        /// 删除
        /// </summary>
        Deleted = 0,
        /// <summary>
        /// 在库
        /// </summary>
        Normal = 1,
        /// <summary>
        /// 领用待确认
        /// </summary>
        Receiving = 2,
        /// <summary>
        /// 已领用
        /// </summary>
        Received = 3,
        /// <summary>
        /// 报废待财务确认
        /// </summary>
        Scrapping = 4,
        /// <summary>
        /// 报废待组长确认
        /// </summary>
        Scrapping2 = 7,
        /// <summary>
        /// 已报废
        /// </summary>
        Scrapped = 5,
        /// <summary>
        /// 已损坏
        /// </summary>
        Bad = 6,

    }

    public enum InvoiceType
    {
        /// <summary>
        ///  服务类发票
        /// </summary>
        ServiceType = 0,
        /// <summary>
        /// 广告类发票
        /// </summary>
        ADType = 1
    }

    public static class ProjectExpense_Desc
    {
        public static string OOP = "OOP";
        public static string Media = "Media";
        /// <summary>
        /// 项目充值
        /// </summary>
        public static string Recharge = "媒体充值金额";
    }


    public static class ReturnPaymentType
    {
        public static string ReturnStatusString(int statusInt, int orderType, bool? isdiscount)
        {
            switch (statusInt)
            {
                case -1:
                    return "驳回";
                    break;
                case 0:
                    return "已创建";
                case 1:
                    return "已创建";
                case 2:
                    return "待业务审批";
                case 3:
                    return "待定1";
                case 4:
                    return "待定2";
                case 5:
                    return "已付款";
                case 90:
                    return "待物料审核";
                case 91:
                    return "待采购总监审核";
                case 92:
                    return "待采购总监审核";
                case 93:
                    return "待总经理审核";
                case 98:
                    return "预审审核通过";
                case 99:
                    return "待业务审批";
                case 100:
                    if (orderType == 40)
                        return "审核中";
                    else
                        return "待财务预审";
                case 101:
                    if (orderType == 40)
                        return "审核中";
                    else
                        return "总经理审核通过";
                case 102:
                    return "业务审核通过";
                case 103:
                    return "FA审核通过";
                case 104:
                    return "集团财务总监审核通过";
                case 105:
                    return "已收货";
                case 106:
                    return "附加收货通过";
                case 107:
                    return "待供应商审核";//前台已确认
                case 108:
                    return "已出票";//供应商已经确认
                case 109:
                    return "待财务预审";//
                case 110:
                    return "待财务复审";
                case 120:
                    return "待财务终审";
                case 130:
                    return "待财务终审";
                case 136:
                    if (orderType == 11)
                        return "押金待销账";
                    else
                        return "借款待报销";
                case 137:
                    return "冲销中";
                case 138:
                    return "冲销中";
                case 139:
                    return "已冲销";
                case 140:
                    if (isdiscount != null && isdiscount.Value != false)
                    {
                        return "<font color='red'>已抵扣</font>";
                    }
                    else
                        return "已付款";
                case 150:
                    return "重汇";
                case 160:
                    return "已销账";
                default:
                    return "";
            }
        }
    }

    public static class CheckStatus
    {
        /// <summary>
        /// 新支票
        /// </summary>
        public static int New = 0;
        /// <summary>
        /// 已经使用的支票
        /// </summary>
        public static int Used = 1;
        /// <summary>
        /// 作废的支票
        /// </summary>
        public static int Destroy = 2;

        public static string GetStatusDesc(int status)
        {
            string ret = string.Empty;
            switch (status)
            {
                case 0:
                    ret = "可用";
                    break;
                case 1:
                    ret = "已使用";
                    break;
                case 2:
                    ret = "作废";
                    break;
            }
            return ret;
        }
    }

    public static class InvoiceStatus
    {
        /// <summary>
        /// 新发票
        /// </summary>
        public static int New = 0;
        /// <summary>
        /// 已经使用的发票
        /// </summary>
        public static int Used = 1;
        /// <summary>
        /// 作废的发票
        /// </summary>
        public static int Destroy = 2;

        public static string GetStatusDesc(int status)
        {
            string ret = string.Empty;
            switch (status)
            {
                case 0:
                    ret = "可用";
                    break;
                case 1:
                    ret = "已使用";
                    break;
                case 2:
                    ret = "作废";
                    break;
            }
            return ret;
        }
    }

    /// <summary>
    /// 项目号申请合同更新
    /// </summary>
    public enum ProjectCheckContract
    {
        /// <summary>
        /// 项目号申请的初始合同
        /// </summary>
        InitialContract = 0,
        /// <summary>
        /// 项目号申请审核通过后，合同更新
        /// </summary>
        ContractUpdate = 1
    }

    public enum ProjectExtention
    {
        /// <summary>
        /// 正常分税
        /// </summary>
        A = 0,
        /// <summary>
        /// 暂不考虑
        /// </summary>
        B = 1,
        /// <summary>
        /// 没有税金，不开发票
        /// </summary>
        C = 2,
        /// <summary>
        /// 广告投放
        /// </summary>
        D = 4
    }

    /// <summary>
    /// 审批日志表对应的审批业务单据
    /// </summary>
    public enum FormType
    {
        /// <summary>
        /// 项目号
        /// </summary>
        Project = 1,
        /// <summary>
        /// 支持方
        /// </summary>
        Supporter = 2,
        /// <summary>
        /// 付款通知
        /// </summary>
        Payment = 3,
        /// <summary>
        /// 付款申请
        /// </summary>
        Return = 4,
        /// <summary>
        /// 报销
        /// </summary>
        ExpenseAccount = 5,
        /// <summary>
        /// 批次付款
        /// </summary>
        PNBatch = 6,
        PR = 7,
        ProxyPnReport = 8,
        /// <summary>
        /// 证据链
        /// </summary>
        Contract = 9,
        /// <summary>
        /// 发票申请
        /// </summary>
        ApplyForInvioce = 10,
        /// <summary>
        /// 消耗导入审批
        /// </summary>
        Consumption=11,
        /// <summary>
        /// 返点导入审批
        /// </summary>
        RebateRegistration=12,
        /// <summary>
        /// 退款申请
        /// </summary>
        Refund=13,
        /// <summary>
        /// 用印审批
        /// </summary>
        RequestForSeal = 14,

    }

    public static class ProjectType
    {
        public static string LongTerm = "长期项目";
        public static string ShortTerm = "短期项目";
        public static string BDProject = "BD项目";
    }


    /// <summary>
    /// 报销审批状态
    /// </summary>
    public static class ExpenseAccountAuditStatus
    {
        #region 报销审批状态
        /// <summary>
        /// 提交
        /// </summary>
        public static int AuditStatus_Submit = 1;
        /// <summary>
        /// 通过
        /// </summary>
        public static int AuditStatus_Passed = 2;
        /// <summary>
        /// 驳回
        /// </summary>
        public static int AuditStatus_Reject = 3;
        /// <summary>
        /// 驳回
        /// </summary>
        public static int AuditStatus_RejectToF1 = 4;
        /// <summary>
        /// 审批结束
        /// </summary>
        public static int AuditStatus_Close = 5;

        public static int AuditStatus_Tip = 6;
        public static string[] AuditStatus_Names = { "", "提交", "审批通过", "审批驳回至申请人", "审批驳回至财务第一级", "审批结束", "留言" };

        public static string[] ConsumptionAuditStatus_Names = {"", "审批通过", "审批驳回", "", "", "留言" };
        #endregion
    }


    public static class PaymentType
    {
        public static int 现金 = 1;
        public static int 支票付款 = 2;
        public static int 银行转帐 = 3;
        public static int 承兑汇票 = 4;
    //    public static int TrafficFee = 6;
        public static int 商务卡报销 = 7;
        public static int 冲抵押金 = 8;
        public static int 冲抵现金 = 9;
        public static string[] PaymentTypeName = { "", "现金", "支票付款", "银行转帐", "承兑汇票", "", "", "商务卡报销", "冲抵押金", "冲抵现金" };

    }

    public static class ExpenseStatusName
    {
        public static string GetExpenseStatusName(int status, int type)
        {
            switch (status)
            {
                case -1:
                    return "审批驳回";
                case 0:
                    return "待提交";
                case 1:
                    return "待提交";
                case 2:
                    return "待审核";
                case 3:
                    return "待定1";
                case 4:
                    return "待定2";
                case 5:
                    return "已付款";
                case 90:
                    return "待物料审核";
                case 91:
                    return "待采购总监审核";
                case 92:
                    return "待采购总监审核";
                case 93:
                    return "待总经理审核";
                case 98:
                    return "预审审核通过";
                case 99:
                    return "项目负责人审核通过";
                case 100:
                    if (type == 40)
                        return "待前台确认";
                    else
                        return "待财务预审";
                case 101:
                    return "总经理审核通过";
                case 102:
                    return "业务审核通过";
                case 103:
                    return "FA审核通过";
                case 104:
                    return "集团财务总监审核通过";
                case 105:
                    return "已收货";
                case 106:
                    return "附加收货通过";
                case 107:
                    return "待供应商确认";
                case 108:
                    return "已出票";
                case 109:
                    return "待批次提交";
                case 110:
                    return "财务一级审核通过";
                case 120:
                    return "财务二级审核通过";
                case 130:
                    return "待财务终审";
                case 136:
                    return "待冲销";
                case 137:
                    return "冲销中";
                case 138:
                    return "冲销单待提交";
                case 139:
                    return "冲销完成";
                case 140:
                    if (type == 33)
                        return "财务审核完成";
                    else
                        return "已还款";
                case 150:
                    return "驳回到申请人";
                case 160:
                    return "已销账";
                default:
                    return "";
            }
        }
    }
    /// <summary>
    /// 商务卡状态
    /// </summary>
    public enum BusinessCardStatus : int
    {
        /// <summary>
        /// 注销
        /// </summary>
        LogOut = 0,
        /// <summary>
        /// 正常
        /// </summary>
        Available = 1,
        /// <summary>
        /// 挂失
        /// </summary>
        Holding = 2
    }

    /// <summary>
    /// 证据链审核状态
    /// </summary>
    public class ContractStatus
    {

        public enum Status : int
        {
            /// <summary>
            /// 待提交
            /// </summary>
            Wait_Submit = 0,
            /// <summary>
            /// 审核中
            /// </summary>
            Auditing = 1,
            /// <summary>
            /// 审核驳回
            /// </summary>
            Rejected = 2,
            /// <summary>
            /// 审核通过
            /// </summary>
            Audited = 3,
        }

        public static string[] Status_Names = { "待提交", "审核中", "审核驳回", "审核通过" };
    }

    public class ApplyForInvioceStatus
    {
        public enum Status : int
        {
            /// <summary>
            /// 待提交
            /// </summary>
            Wait_Submit = 0,
            /// <summary>
            /// 审核中
            /// </summary>
            Auditing = 1,
            /// <summary>
            /// 审核驳回
            /// </summary>
            Rejected = 2,
            /// <summary>
            /// 审核通过
            /// </summary>
            Audited = 3,
        }
        public static string[] Status_Names = { "待提交", "审核中", "审核驳回", "审核通过" };
    }



    public enum BusinessCarddraw : int
    {
        /// <summary>
        /// 卡丢失本人已挂失
        /// </summary>
        LogOut = 0,
        /// <summary>
        /// 已领
        /// </summary>
        Drew = 1,
        /// <summary>
        /// 卡已剪
        /// </summary>
        Cut = 2
    }

    public static class BusinessCard
    {
        public static string GetStatus(int status)
        {
            string ret = string.Empty;
            switch (status)
            {
                case 0:
                    ret = "注销";
                    break;
                case 1:
                    ret = "正常";
                    break;
                case 2:
                    ret = "挂失";
                    break;
            }
            return ret;
        }

        public static string GetDrawStatus(int status)
        {
            string ret = string.Empty;
            switch (status)
            {
                case 0:
                    ret = "卡丢失本人已挂失";
                    break;
                case 1:
                    ret = "已领";
                    break;
                case 2:
                    ret = "卡已剪";
                    break;
            }
            return ret;
        }
    }
}
