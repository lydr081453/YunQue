using System;
using System.Collections.Generic;
using System.Web;
using ESP.Finance.Utility;

/// <summary>
///RequestName 的摘要说明
/// </summary>
/// 
namespace ESP.Finance.Utility
{
    public class RequestName
    {
        public const string ProjectID = "ProjectID";//项目ID
        public const string ProjectStep = "ProjectStep";//项目ID
        public const string ProjectCode = "ProjectCode";//项目号
        public const string CustomerID = "CustomerID";//客户ID
        public const string CustomerAttachID = "CustomerID";//客户附件ID
        public const string Alert = "alert";
        public const string Operate = "Operate";//操作
        public const string BackUrl = "backurl";//返回路径
        public const string SORT = "Sort";//grid 排序列名
        public const string DIR = "dir";//排序asc or desc
        public const string PageSize = "limit";//gridview每页显示行数
        public const string Offset = "start";//grid起始页
        public const string SearchField = "field";//客户查询的列名
        public const string SearchValue = "fieldValue";//客户查询的列值
        public const string UserName = "username";//模糊查询用户名称
        public const string SearchType = "searchType";//选择员工的opener划分
        public const string DeptID = "DeptID";
        public const string DeptName = "DeptName";
        public const string Percent = "Percent";//预计各月完成percent
        public const string BeginYear = "year";
        public const string BeginMonth = "month";
        public const string ResponserID = "ResponserID";
        public const string ResponserName = "ResponserName";
        public const string BranchID = "BranchID";
        public const string BranchCode = "BranchCode";
        public const string BranchID2 = "BranchID2";
        public const string Mail = "mail";
        //public const string WorkItemID = "WorkItemID";
        //public const string WorkItemName = "WorkItemName";
        //public const string ProcessID = "ProcessID";
        //public const string InstanceID = "InstanceID";
        public const string SupportID = "SupportID";
        public const string ContractID = "ContractID";
        public const string ContractCostID = "ContractCostID";
        public const string CostType = "CostType";//cost or expense
        public const string ReturnID = "ReturnID";
        public const string RefundID = "RefundID";
        public const string ModelID = "ModelID";
        public const string ModelType = "ModelType";
        public const string PaymentID = "PaymentID";
        public const string SupporterContractCostID = "SupporterContractCostID";
        public const string ProjectExpenseID = "ProjectExpenseID";
        public const string BankID = "BankID";
        public static string InvoiceID = "InvoiceID";
        public static string InvoiceDetailID = "InvoiceDetailID";
        public static string BatchID = "BatchID";
        public static string BatchCode = "BatchCode";
        public const string NotPostBack = "NotPostBack";
        public const string PaymentDate = "PaymentDate";
    }

    public class State
    {

        public static string addstatus_1 = "ProjectStep1.aspx?" + RequestName.ProjectID + "={0}";
        public static string addstatus_2 = "ProjectStep11.aspx?" + RequestName.ProjectID + "={0}";
        public static string addstatus_3 = "ProjectStep2.aspx?" + RequestName.ProjectID + "={0}";
        public static string addstatus_4 = "ProjectStep3.aspx?" + RequestName.ProjectID + "={0}";
        public static string addstatus_5 = "ProjectStep4.aspx?" + RequestName.ProjectID + "={0}";
        public static string addstatus_6 = "ProjectStep5.aspx?" + RequestName.ProjectID + "={0}";
        public static string addstatus_7 = "ProjectStep21.aspx?" + RequestName.ProjectID + "={0}";
        public static string addstatus_8 = "ProjectEdit.aspx?" + RequestName.ProjectID + "={0}";
        //业务审核名称
        /// <summary>
        /// 项目负责人
        /// </summary>
        public static string operationAudit_principal = "项目负责人";

        public static string operationAudit_FA = "FA";
        /// <summary>
        /// 媒介总监
        /// </summary>
        public static string operationAudit_MediaMajor = "媒介总监";
        /// <summary>
        /// 总监
        /// </summary>
        public static string operationAudit_majordomo = "总监";
        /// <summary>
        /// 总经理
        /// </summary>
        public static string operationAudit_generalmanager = "总经理";
        /// <summary>
        /// CEO
        /// </summary>
        public static string operationAudit_CEO = "CEO";


        public static string SetState(int state)
        {
            string ret = string.Empty;
            switch (state)
            {
                case (int)Status.Saved:
                    ret = "未提交";
                    break;
                case (int)Status.Submit:
                    ret = "待业务审批";
                    break;
                case (int)Status.BizAuditing:
                    ret = "业务审批中";
                    break;
                case (int)Status.WaitRiskControl:
                    ret = "风控审批中";
                    break;
                case (int)Status.BizAuditComplete:
                    ret = "待财务审批";
                    break;
                case (int)Status.BizReject:
                    ret = "业务审批驳回";
                    break;
                case (int)Status.ContractAudit:
                    ret = "财务审批中";
                    break;
                case (int)Status.Waiting:
                    ret = "等待合同";
                    break;
                case (int)Status.ContractReject:
                    ret = "财务审批驳回";
                    break;
                case (int)Status.FinanceAuditing:
                    ret = "财务审批中";
                    break;
                case (int)Status.FinanceReject:
                    ret = "财务审批驳回";
                    break;
                case (int)Status.FinanceAuditComplete:
                    ret = "审批已完成";
                    break;
                case (int)Status.ProjectPreClose:
                    ret = "预关闭";
                    break;
                case (int)Status.ProjectClosed:
                    ret = "已关闭";
                    break;
                case (int)Status.projectCancel:
                    ret = "取消";
                    break;

            }

            return ret;
        }
    }
}