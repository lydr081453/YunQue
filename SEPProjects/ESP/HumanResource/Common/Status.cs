using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
namespace ESP.HumanResource.Common
{
    public class Status
    {
        public Status()
        {
            //
            //TODO: 在此处添加构造函数逻辑
            //
        }
        /// <summary>
        /// 婚姻状况
        /// </summary>
        public enum MaritalStatus
        {
            /// <summary>
            /// 未知
            /// </summary>
            Unknown = 0,

            /// <summary>
            /// 已婚
            /// </summary>
            Married = 1,

            /// <summary>
            /// 未婚
            /// </summary>
            Single = 2,

            /// <summary>
            /// 已婚有子
            /// </summary>
            Children = 3,

            /// <summary>
            /// 离异
            /// </summary>
            Dissociaton = 4
        }

        public static string[] MaritalStatus_Names = { "未知", "已婚", "未婚", "已婚有子", "离异" };

        /// <summary>
        /// 性别
        /// </summary>
        public enum Gender
        {
            /// <summary>
            /// 未知
            /// </summary>
            Unknown = 0,

            /// <summary>
            /// 男
            /// </summary>
            Male = 1,

            /// <summary>
            /// 女
            /// </summary>
            Female = 2
        }

        public static string[] Gender_Names = { "未知", "男", "女" };

        public enum TransferStatus
        { 
            /// <summary>
            /// 保存
            /// </summary>
            Save=0,
            /// <summary>
            /// 转入组HR提交
            /// </summary>
            HRCommit=1,
            /// <summary>
            /// 驳回
            /// </summary>
            Reject=2,
            /// <summary>
            /// 转出组HR确认
            /// </summary>
            HRConfirmed=3,
            /// <summary>
            /// 转出组总监设置交接
            /// </summary>
            LeaderConfirmOut=4,
            /// <summary>
            /// 交接人确认
            /// </summary>
            ReceiverConfirm=5,
            /// <summary>
            /// 转出组总经理确认
            /// </summary>
            ManagerConfirmOut=6,
            /// <summary>
            /// 转入组总监确认
            /// </summary>
            LeaderConfirmIn=7,
            /// <summary>
            /// 转入组总经理确认
            /// </summary>
            ManagerConfirmIn=8,
            /// <summary>
            /// HR确认转入完成
            /// </summary>
            Complete=9,
                /// <summary>
                /// 转组人确认
                /// </summary>
            TransferConfirm=10

        }
       // public static string[] TransferStatus_Names = { "保存", "转入组HR提交", "驳回", "转出组HR确认", "转出组总监设置交接", "交接人确认", "转出组总经理确认", "转入组总监确认", "转入组总经理确认", "HR确认转入完成" };
        public static string[] TransferStatus_Names = { "保存", "待转出组HR确认", "驳回", "待转出组总监审核", "已设置交接", "交接人已确认", "待转入组总监审核", "待转入组总经理审核", "待HR确认", "已完成", "待转组人确认" };

        public enum HeadAccountStatus
        {
            /// <summary>
            /// 驳回
            /// </summary>
            Reject = 0,
            /// <summary>
            ///待VP审批
            /// </summary>
            Commit = 1,
            /// <summary>
            /// VP审批通过
            /// </summary>
            VPApproved = 2,
            /// <summary>
            /// 审批通过
            /// </summary>
            FinanceApproved = 3,
            /// <summary>
            /// 面谈完毕
            /// </summary>
            InterView = 4,
            /// <summary>
            /// 面谈审核通过
            /// </summary>
            InterViewVPApproved = 5,
            /// <summary>
            /// 提交Offer
            /// </summary>
            SendOffer = 6,
            /// <summary>
            /// HR面谈记录
            /// </summary>
            InterviewHR = 7,
            /// <summary>
            /// 业务团队面谈记录
            /// </summary>
            InterviewGroup = 8,
            /// <summary>
            /// 待团队预审
            /// </summary>
            WaitPreVPAudit=9,
            OfferLetteAudited=10,
            /// <summary>
            /// 待团队终审
            /// </summary>
            WaitCFOAudit=11,

           // WaitCEOAudit=12
            FinancialAudit = 12
        }

        public static string[] HeadAccountStatus_Names = { "驳回", "待VP审批", "待VP审批", "审批通过", "面谈完毕", "面谈审核通过", "提交Offer", "HR面谈意见", "业务团队面谈意见", "待团队预审", "Offer已发送", "待团队终审","财务审阅" };

        //6,11,12,10,9,-1,0
        public static int IsDeleted = -1;
        public static int WaitEntry = 0; // 待入职
        public static int Entry = 1;  // 入职
        public static int WaitPassed = 2;  // 待转正
        public static int Passed = 3;  // 转正
        public static int WaitDimission = 4;  // 待离职
        public static int Dimission = 5;  // 离职
        public static int Fieldword = 6;  // 实习或兼职
        public static int DimissionReceiving = 7;   // 提交离职申请单
        public static int DimissionFinanceAudit = 8;  // 财务审批通过
        public static int IsSaved = 9;  // 已保存员工信息
        public static int IsSendOfferLetter = 10;  // 是否已经发送Offer Letter
        public static int Reject = 11;
        public static int OfferHRAudit = 12;//offer待HR总监审批
        public static int OfferFinanceAudit = 13;//offer待财务总监审批
        public static string[] Employee_StatusName = { "待入职", "入职", "待转正", "转正", "待离职", "离职", "实习或兼职", "", "", "已保存员工", "已发送Offer", "Reject", "offer待HR总监审批", "offer待财务审批" };

        public static int Log = 0;
        public static int LogEntry = 1;
        public static int LogPassed = 2;
        public static int LogDimission = 3;
        public static int LogSalary = 4;
        public static int LogJob = 5;
        public static int LogPromotion = 6;

        //发信的类型
        //待入职
        public static int WaitEntrySendMail = 1;
        //离职
        public static int DimissionSendMail = 2;
        //入职
        public static int EntrySendMail = 3;
        // offer确认收件人
        public static int OfferConfirmMail = 4;
        //离职IT
        public static int DimissionSendMailIT = 5;


        public static Dictionary<int, string> WorkCity = new Dictionary<int, string> { { 19, "北京" }, { 230, "重庆" } };

        public static Dictionary<string, string> WorkAddress = new Dictionary<string, string> { { "北京", "北京市朝阳区双桥路12号 电子城数字新媒体创新产业园D1楼" }, { "重庆", "重庆市渝北区大竹林街道杨柳路6号三狼公园6号D4-102" }, { "杭州", "杭州市滨江区西兴街道协同路258号(" } };

    }

    /// <summary>
    /// 离职单状态
    /// </summary>
    public enum DimissionFormStatus
    {
        /// <summary>
        /// 未提交
        /// </summary>
        NotSubmit = 1,

        /// <summary>
        /// 待总监审批
        /// </summary>
        WaitDirector = 2,

        /// <summary>
        /// 待交接人确认
        /// </summary>
        WaitReceiver = 3,

        /// <summary>
        /// 待总经理审批
        /// </summary>
        WaitManager = 4,

        /// <summary>
        /// 待团队行政审批
        /// </summary>
        WaitGroupHR = 5,

        /// <summary>
        /// 待人力资源、IT审批
        /// </summary>
        WaitHRIT = 6,

        /// <summary>
        /// 待集团人力资源审批
        /// </summary>
        WaitHR1 = 7,

        /// <summary>
        /// 待财务审批
        /// </summary>
        WaitFinance = 8,

        /// <summary>
        /// 待IT部审批
        /// </summary>
        WaitIT = 9,

        /// <summary>
        /// 待集团行政审批
        /// </summary>
        WaitAdministration = 10,

        /// <summary>
        /// 待集团人力资源审批
        /// </summary>
        WaitHR2 = 11,

        /// <summary>
        /// 审批通过
        /// </summary>
        AuditComplete = 12,

        /// <summary>
        /// 审批驳回
        /// </summary>
        Overrule = 13,

        /// <summary>
        /// 等待集团人力总监审批
        /// </summary>
        WaitHRDirector = 14,

        /// <summary>
        /// message
        /// </summary>
        DimissionTip = 15,
        /// <summary>
        /// 待预审
        /// </summary>
        WaitPreAuditor=16
    }

    /// <summary>
    /// 人力系统单据类型
    /// </summary>
    public enum HRFormType
    {
        /// <summary>
        /// 入职单
        /// </summary>
        EntryForm = 1,

        /// <summary>
        /// 转正单
        /// </summary>
        PositiveForm = 2,

        /// <summary>
        /// 调职单
        /// </summary>
        TransferForm = 3,

        /// <summary>
        /// 离职单
        /// </summary>
        DimissionForm = 4,
    }

    /// <summary>
    /// 审批日志状态
    /// </summary>
    public enum AuditStatus
    {
        /// <summary>
        /// 未审批
        /// </summary>
        NotAudit = 0,

        /// <summary>
        /// 审批通过
        /// </summary>
        Audited = 1,

        /// <summary>
        /// 审批驳回
        /// </summary>
        Overrule = 2,
        /// <summary>
        /// 留言
        /// </summary>
        Tip = 3,
    }
    /// <summary>
    /// 推荐用户状态
    /// </summary>
    public enum RecommendStatus
    {
        /// <summary>
        /// 待审核
        /// </summary>
        Audit = 0,

        /// <summary>
        /// 已审核
        /// </summary>
        Entry = 1,

        /// <summary>
        /// 已入职
        /// </summary>
        Award = 2,
    }


}
