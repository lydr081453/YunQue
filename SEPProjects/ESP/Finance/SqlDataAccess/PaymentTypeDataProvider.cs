using System;
using System.Data;
using System.Text;
using System.Collections;
using System.Data.SqlClient;
using ESP.Finance.Entity;
using System.Collections.Generic;
using ESP.Finance.Utility;

namespace ESP.Finance.DataAccess
{
    internal class PaymentTypeDataProvider : ESP.Finance.IDataAccess.IPaymentTypeDataProvider
    {
        
        #region  成员方法

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public ESP.Finance.Entity.PaymentTypeInfo GetModel(int PaymentTypeID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 PaymentTypeID,PaymentTypeName,Description,IsNeedCode,IsNeedBank,Tag,IsBatch from F_PaymentType ");
            strSql.Append(" where PaymentTypeID=@PaymentTypeID ");
            SqlParameter[] parameters = {
					new SqlParameter("@PaymentTypeID", SqlDbType.Int,4)};
            parameters[0].Value = PaymentTypeID;

            return CBO.FillObject<PaymentTypeInfo>(DbHelperSQL.Query(strSql.ToString(), parameters));
        }


        /// <summary>
        /// 获得数据列表
        /// </summary>
        public IList<ESP.Finance.Entity.PaymentTypeInfo> GetList(string term, List<SqlParameter> param)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select PaymentTypeID,PaymentTypeName,Description,IsNeedCode,IsNeedBank,Tag,IsBatch ");
            strSql.Append(" FROM F_PaymentType ");
            if (!string.IsNullOrEmpty(term))
            {
                strSql.Append(" where " + term);
            }
            return CBO.FillCollection<ESP.Finance.Entity.PaymentTypeInfo>(DbHelperSQL.Query(strSql.ToString(), param));
        }
        #endregion  成员方法
    }
}
