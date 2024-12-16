using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESP.Finance.Utility;
using System.Data.SqlClient;

namespace ESP.Finance.DataAccess
{
    internal class GetReturnGeneralInfoListDataProvider : ESP.Finance.IDataAccess.IGetReturnGeneralInfoListDataProvider
    {

        #region IV_GetReturnGeneralInfoListProvider 成员

        public ESP.Finance.Entity.ReturnGeneralInfoListViewInfo GetModel(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"select ReturnID,DeferFee,DeferDay,ReturnStatus,IsInvoice,InvoiceNo,InvoiceDate,
                            BranchID,BranchCode,BranchName,Lastupdatetime,ReturnCode,ProjectID,ProjectCode,
                            ReturnPreDate,ReturnFactDate,ReturnContent,PreFee,FactFee,
                            PaymentUserID,PaymentCode,PaymentEmployeeName,PaymentUserName,
                            PurchasePayID,PRID,PRNo,
                            RequestorID,RequestUserCode,RequestUserName,
                            RequestEmployeeName,RequestDate,PreBeginDate,PreEndDate,
                            Department,Orderid,Account_name,account_bank,Account_number ");
            strSql.Append(" FROM V_GetReturnGeneralInfoList ");
            strSql.Append(" where ReturnID=@ReturnID ");
            SqlParameter[] parameters = {
					new SqlParameter("@ReturnID", System.Data.SqlDbType.Int,4)};
            parameters[0].Value = id;

            return CBO.FillObject<Entity.ReturnGeneralInfoListViewInfo>(DbHelperSQL.Query(strSql.ToString(), parameters));
        }

        public IList<Entity.ReturnGeneralInfoListViewInfo> GetList(string term, List<System.Data.SqlClient.SqlParameter> param)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"select ReturnID,DeferFee,DeferDay,ReturnStatus,IsInvoice,InvoiceNo,InvoiceDate,
                            BranchID,BranchCode,BranchName,Lastupdatetime,ReturnCode,ProjectID,ProjectCode,
                            ReturnPreDate,ReturnFactDate,ReturnContent,PreFee,FactFee,
                            PaymentUserID,PaymentCode,PaymentEmployeeName,PaymentUserName,
                            PurchasePayID,PRID,PRNo,
                            RequestorID,RequestUserCode,RequestUserName,
                            RequestEmployeeName,RequestDate,PreBeginDate,PreEndDate,
                            Department,Orderid,Account_name,account_bank,Account_number 
                            ");
            strSql.Append(" FROM V_GetReturnGeneralInfoList ");
            if (!string.IsNullOrEmpty(term))
            {
                strSql.Append(" where " + term);
            }
            //if (param != null && param.Count > 0)
            //{
            //    SqlParameter[] ps = param.ToArray();

            //    return CBO.FillCollection<Entity.ReturnGeneralInfoListViewInfo>(DbHelperSQL.ExecuteReader(strSql.ToString(), ps));
            //}
            return CBO.FillCollection<Entity.ReturnGeneralInfoListViewInfo>(DbHelperSQL.Query(strSql.ToString(),param));
        }

        #endregion
    }
}
