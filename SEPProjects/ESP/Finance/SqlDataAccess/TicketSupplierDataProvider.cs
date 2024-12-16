using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using ESP.Finance.Entity;
using System.Collections.Generic;
using ESP.Finance.Utility;
using ESP.Finance.IDataAccess;
using ESP.Finance.DataAccess;

namespace ESP.Finance.DataAccess
{
   public  class TicketSupplierDataProvider:ITicketSupplierProvider
    {

        #region ITicketSupplierProvider 成员

        public TicketSupplier GetModel(int supplierId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select SupplierId,SupplierName,Contacter,Email,Mobile,Tel,Fax,Address,BankName,AccountName,AccountNo,FloorNo,ReceptionId,SupplySupplierId ");
            strSql.Append(" from f_ticketsupplier ");
            strSql.Append(" where SupplierId=@SupplierId ");
            SqlParameter[] parameters = {
					new SqlParameter("@supplierId", SqlDbType.Int,4)};
            parameters[0].Value = supplierId;
            return CBO.FillObject<TicketSupplier>(DbHelperSQL.Query(strSql.ToString(), parameters));
        }

        public IList<TicketSupplier> GetList(string term, List<SqlParameter> param)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select SupplierId,SupplierName,Contacter,Email,Mobile,Tel,Fax,Address,BankName,AccountName,AccountNo,FloorNo,ReceptionId,SupplySupplierId ");
            strSql.Append(" FROM f_ticketsupplier ");
            if (!string.IsNullOrEmpty(term))
            {
                strSql.Append(" where " + term);
            }
            return CBO.FillCollection<TicketSupplier>(DbHelperSQL.Query(strSql.ToString(), param));
        }

        #endregion
    }
}
