using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESP.Finance.DataAccess
{
    internal class InvoiceDetailReportDataProvider : ESP.Finance.IDataAccess.IInvoiceDetailReportDataProvider
    {
        #region IInvoiceDetailProvier 成员

        public IList<Entity.InvoiceDetailReporterInfo> GetList(string term, List<System.Data.SqlClient.SqlParameter> param)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ");
            strSql.Append(@"a.InvoiceDetailID,a.PaymentID,a.PaymentCode,a.InvoiceID,a.InvoiceNo,
                            a.Amounts,a.USDDiffer,a.ResponseUserID,a.ResponseUserName,a.ResponseCode,
                            a.ResponseEmployeeName,a.ProjectID,a.ProjectCode,a.CreateDate,a.Description,
                            a.Remark,invoice.CustomerID,invoice.CustomerName ");
            strSql.Append(@" FROM F_InvoiceDetail as a 
                                        inner join F_Payment as payment on a.PaymentID = payment.PaymentID 
                                        --inner join F_Project as project on payment.ProjectID = project.ProjectID 
                                        inner join F_Invoice as invoice on a.InvoiceID = invoice.InvoiceID                                     
                                        ");
            if (!string.IsNullOrEmpty(term))
            {
                strSql.Append(" where " + term);
            }
            return Utility.CBO.FillCollection<Entity.InvoiceDetailReporterInfo>(DbHelperSQL.Query(strSql.ToString(), param));
        }

        #endregion
    }
}
