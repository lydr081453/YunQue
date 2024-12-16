using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using ESP.Finance.Entity;
using System.Collections.Generic;
using ESP.Finance.Utility;

namespace ESP.Finance.DataAccess
{
    internal class CustomerPODataProvider : ESP.Finance.IDataAccess.ICustomerPODataProvider
    {
        #region  成员方法
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int CustomerPOID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from F_CustomerPO");
            strSql.Append(" where CustomerPOID=@CustomerPOID ");
            SqlParameter[] parameters = {
					new SqlParameter("@CustomerPOID", SqlDbType.Int,4)};
            parameters[0].Value = CustomerPOID;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(CustomerPOInfo model)
        {
            return Add(model, null);
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(CustomerPOInfo model,SqlTransaction trans)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into F_CustomerPO(");
            strSql.Append("CustomerTMPID,POAmount,POCode)");
            strSql.Append(" values (");
            strSql.Append("@CustomerTMPID,@POAmount,@POCode)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@CustomerTMPID", SqlDbType.Int,4),
					new SqlParameter("@POAmount", SqlDbType.Decimal,9),
					new SqlParameter("@POCode", SqlDbType.NVarChar,50)};
            parameters[0].Value =model.CustomerTMPID;
            parameters[1].Value =model.POAmount;
            parameters[2].Value =model.POCode;

            object obj = DbHelperSQL.GetSingle(strSql.ToString(),trans, parameters);
            if (obj == null)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(obj);
            }
        }

        ///// <summary>
        ///// 更新一条数据
        ///// </summary>
        //public int Update(CustomerPOInfo model)
        //{
        //    return Update(model, false);
        //}
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public int Update(CustomerPOInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update F_CustomerPO set ");
            strSql.Append("CustomerTMPID=@CustomerTMPID,");
            strSql.Append("POAmount=@POAmount,");
            strSql.Append("POCode=@POCode");
            strSql.Append(" where CustomerPOID=@CustomerPOID ");
            SqlParameter[] parameters = {
					new SqlParameter("@CustomerPOID", SqlDbType.Int,4),
					new SqlParameter("@CustomerTMPID", SqlDbType.Int,4),
					new SqlParameter("@POAmount", SqlDbType.Decimal,9),
					new SqlParameter("@POCode", SqlDbType.NVarChar,50)};
            parameters[0].Value =model.CustomerPOID;
            parameters[1].Value =model.CustomerTMPID;
            parameters[2].Value =model.POAmount;
            parameters[3].Value =model.POCode;

            return DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public int Delete(int CustomerPOID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete F_CustomerPO ");
            strSql.Append(" where CustomerPOID=@CustomerPOID ");
            SqlParameter[] parameters = {
					new SqlParameter("@CustomerPOID", SqlDbType.Int,4)};
            parameters[0].Value = CustomerPOID;

            return DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Entity.CustomerPOInfo GetModel(int CustomerPOID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 CustomerPOID,CustomerTMPID,POAmount,POCode from F_CustomerPO ");
            strSql.Append(" where CustomerPOID=@CustomerPOID ");
            SqlParameter[] parameters = {
					new SqlParameter("@CustomerPOID", SqlDbType.Int,4)};
            parameters[0].Value = CustomerPOID;

            return CBO.FillObject<CustomerPOInfo>(DbHelperSQL.Query(strSql.ToString(), parameters));
        }



        /// <summary>
        /// 获得数据列表
        /// </summary>
        public IList<CustomerPOInfo> GetList(string term, List<SqlParameter> param)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select CustomerPOID,CustomerTMPID,POAmount,POCode ");
            strSql.Append(" FROM F_CustomerPO ");
            if (!string.IsNullOrEmpty(term))
            {
                strSql.Append(" where " + term);
            }
            //if (param != null && param.Count > 0)
            //{
            //    SqlParameter[] ps = param.ToArray();

            //    return CBO.FillCollection<CustomerPOInfo>(DbHelperSQL.ExecuteReader(strSql.ToString(), ps));
            //}
            return CBO.FillCollection<CustomerPOInfo>(DbHelperSQL.Query(strSql.ToString(),param));
        }

        #endregion  成员方法
    }
}
