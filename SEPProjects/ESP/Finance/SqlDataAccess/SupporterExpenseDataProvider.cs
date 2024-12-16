using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using ESP.Finance.Utility;

namespace ESP.Finance.DataAccess
{
    internal class SupporterExpenseDataProvider : ESP.Finance.IDataAccess.ISupporterExpenseDataProvider
    {
        #region  成员方法

        //        /// <summary>
        ///// 增加一条数据
        ///// </summary>
        //public int Add(Entity.SupporterExpenseInfo model)
        //{
        //    return Add(model,false);
        //}
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(Entity.SupporterExpenseInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into F_SupporterExpense(");
            strSql.Append("SupporterID,SupporterCode,Description,Expense,Remark)");
            strSql.Append(" values (");
            strSql.Append("@SupporterID,@SupporterCode,@Description,@Expense,@Remark)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@SupporterID", SqlDbType.Int,4),
					new SqlParameter("@SupporterCode", SqlDbType.NVarChar,50),
					new SqlParameter("@Description", SqlDbType.NVarChar,500),
					new SqlParameter("@Expense", SqlDbType.Decimal,9),
					new SqlParameter("@Remark", SqlDbType.NVarChar,500)};
            parameters[0].Value =model.SupporterID;
            parameters[1].Value =model.SupporterCode;
            parameters[2].Value =model.Description;
            parameters[3].Value =model.Expense;
            parameters[4].Value =model.Remark;

            object obj = DbHelperSQL.GetSingle(strSql.ToString(), parameters);
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
        //public int Update(Entity.SupporterExpenseInfo model)
        //{
        //    return Update(model,false);
        //}

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public int Update(Entity.SupporterExpenseInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update F_SupporterExpense set ");
            strSql.Append("SupporterID=@SupporterID,");
            strSql.Append("SupporterCode=@SupporterCode,");
            strSql.Append("Description=@Description,");
            strSql.Append("Expense=@Expense,");
            strSql.Append("Remark=@Remark");
            strSql.Append(" where SupporterExpenseID=@SupporterExpenseID ");
            SqlParameter[] parameters = {
					new SqlParameter("@SupporterExpenseID", SqlDbType.Int,4),
					new SqlParameter("@SupporterID", SqlDbType.Int,4),
					new SqlParameter("@SupporterCode", SqlDbType.NVarChar,50),
					new SqlParameter("@Description", SqlDbType.NVarChar,500),
					new SqlParameter("@Expense", SqlDbType.Decimal,9),
					new SqlParameter("@Remark", SqlDbType.NVarChar,500)};
            parameters[0].Value =model.SupporterExpenseID;
            parameters[1].Value =model.SupporterID;
            parameters[2].Value =model.SupporterCode;
            parameters[3].Value =model.Description;
            parameters[4].Value =model.Expense;
            parameters[5].Value =model.Remark;

            return DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public int Delete(int SupporterExpenseID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete F_SupporterExpense ");
            strSql.Append(" where SupporterExpenseID=@SupporterExpenseID ");
            SqlParameter[] parameters = {
					new SqlParameter("@SupporterExpenseID", SqlDbType.Int,4)};
            parameters[0].Value = SupporterExpenseID;

            return DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Entity.SupporterExpenseInfo GetModel(int SupporterExpenseID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 SupporterExpenseID,SupporterID,SupporterCode,Description,Expense,Remark from F_SupporterExpense ");
            strSql.Append(" where SupporterExpenseID=@SupporterExpenseID ");
            SqlParameter[] parameters = {
					new SqlParameter("@SupporterExpenseID", SqlDbType.Int,4)};
            parameters[0].Value = SupporterExpenseID;

            return CBO.FillObject<Entity.SupporterExpenseInfo>(DbHelperSQL.Query(strSql.ToString(), parameters));
        }

        //public decimal GetTotalExpense(int supporterId)
        //{
        //    return GetTotalExpense(supporterId, false);
        //}

        public decimal GetTotalExpense(int supporterId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select sum(Expense) from F_SupporterExpense");
            strSql.Append(" where SupporterID=@SupporterID ");
            SqlParameter[] parameters = {
                    new SqlParameter("@SupporterID", SqlDbType.Int,4)};
            parameters[0].Value = supporterId;

            object res = DbHelperSQL.GetSingle(strSql.ToString(),  parameters);
            if (res != null)
            {
                return Convert.ToDecimal(res);
            }
            return 0;
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public IList<Entity.SupporterExpenseInfo> GetList(string term, List<SqlParameter> param)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select SupporterExpenseID,SupporterID,SupporterCode,Description,Expense,Remark ");
            strSql.Append(" FROM F_SupporterExpense ");
            if (!string.IsNullOrEmpty(term))
            {
                strSql.Append(" where " + term);
            }
            //if (param != null && param.Count > 0)
            //{
            //    SqlParameter[] ps = param.ToArray();

            //    return CBO.FillCollection<Entity.SupporterExpenseInfo>(DbHelperSQL.ExecuteReader(strSql.ToString(), ps));
            //}
            return CBO.FillCollection<Entity.SupporterExpenseInfo>(DbHelperSQL.Query(strSql.ToString(),param));
        }

        public IList<ESP.Finance.Entity.SupporterExpenseInfo> GetList(int supportId, SqlTransaction trans)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select SupporterExpenseID,SupporterID,SupporterCode,Description,Expense,Remark ");
            strSql.Append(" FROM F_SupporterExpense where SupporterID="+supportId.ToString());

            return CBO.FillCollection<Entity.SupporterExpenseInfo>(DbHelperSQL.Query(strSql.ToString(), trans));
        }

        #endregion  成员方法


    }
}
