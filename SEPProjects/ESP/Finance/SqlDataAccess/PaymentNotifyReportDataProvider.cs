using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESP.Finance.Entity;

namespace ESP.Finance.DataAccess
{
    internal class PaymentNotifyReportDataProvider : ESP.Finance.IDataAccess.IPaymentNotifyReportDataProvider
    {
        public IList<PaymentNotifyReporterInfo> GetList(string term, List<System.Data.SqlClient.SqlParameter> param)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"select 
                            project.customerid,project.branchid,project.branchcode,project.branchname,
                            project.groupid,project.groupname,
                            detail.InvoiceDetailID,detail.InvoiceID,detail.InvoiceNo,
                            detail.Amounts,detail.USDDiffer,detail.ResponseUserID,detail.ResponseUserName,detail.ResponseCode,
                            detail.ResponseEmployeeName,detail.ProjectID,detail.ProjectCode,detail.CreateDate,detail.Description,
                            detail.Remark,
                            payment.PaymentID,payment.PaymentCode,payment.ProjectID,payment.ProjectCode,payment.PaymentPreDate,
                            payment.PaymentFactDate,payment.PaymentContent,payment.PaymentStatus,payment.PaymentBudget,payment.PaymentFee,
                            payment.InvoiceDate,payment.BranchID,payment.BranchCode,payment.BranchName,
                            payment.Lastupdatetime,payment.Remark,payment.BankID,payment.BankName,payment.DBCode,
                            payment.DBManager,payment.BankAccount,payment.BankAccountName,payment.PhoneNo,payment.ExchangeNo,
                            payment.RequestPhone,payment.BankAddress,payment.PaymentTypeID,payment.PaymentTypeName,payment.PaymentTypeCode,
                            payment.PaymentUserID,payment.PaymentUserCode,payment.PaymentEmployeeName,payment.PaymentUserName,
                            payment.CreditCode,
                            cust.NameCN1 as custNameCN1,cust.NameCN2 as custNameCN2,cust.NameEN1 as custNameEN1,
                            cust.NameEN2 as custNameEN2,cust.shortCN as custshortCN,cust.shortEN as custshortEN,
                            cust.AreaCode as custAreaCode,cust.AreaName as custAreaName,
                            cust.IndustryCode as custIndustryCode,cust.IndustryName as custIndustryName,
                            project.BusinessDescription as projectname,project.ProjectTypeCode ");
            strSql.Append(@" from f_invoiceDetail as detail 
                                inner join f_payment as payment on detail.paymentid = payment.paymentid 
                                inner join f_project as project on payment.projectid = project.projectid
                                left join f_customertmp as cust on project.customerid = cust.customertmpid
                                ");
            if (!string.IsNullOrEmpty(term))
            {
                strSql.Append(" where " + term);
            }
            return Utility.CBO.FillCollection<Entity.PaymentNotifyReporterInfo>(DbHelperSQL.Query(strSql.ToString(), param));
        }
    }
}
