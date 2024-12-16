using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using ESP.Finance.Entity;
using System.Collections.Generic;
using ESP.Finance.Utility;
//using Maticsoft.DBUtility;//请先添加引用
namespace ESP.Finance.DataAccess
{
    /// <summary>
    /// 数据访问类F_BizType。
    /// </summary>
    internal class BizTypeDataProvider : ESP.Finance.IDataAccess.IBizTypeDataProvider
    {
        
        #region  成员方法
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int BizID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from F_BizType");
            strSql.Append(" where BizID=@BizID ");
            SqlParameter[] parameters = {
					new SqlParameter("@BizID", SqlDbType.Int,4)};
            parameters[0].Value = BizID;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(ESP.Finance.Entity.BizTypeInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into F_BizType(");
            strSql.Append("BizTypeName,Description)");
            strSql.Append(" values (");
            strSql.Append("@BizTypeName,@Description)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@BizTypeName", SqlDbType.NVarChar,50),
					new SqlParameter("@Description", SqlDbType.NVarChar,500)};
            parameters[0].Value =model.BizTypeName;
            parameters[1].Value =model.Description;

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
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public int Update(ESP.Finance.Entity.BizTypeInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update F_BizType set ");
            strSql.Append("BizTypeName=@BizTypeName,");
            strSql.Append("Description=@Description");
            strSql.Append(" where BizID=@BizID ");
            SqlParameter[] parameters = {
					new SqlParameter("@BizID", SqlDbType.Int,4),
					new SqlParameter("@BizTypeName", SqlDbType.NVarChar,50),
					new SqlParameter("@Description", SqlDbType.NVarChar,500)};
            parameters[0].Value =model.BizID;
            parameters[1].Value =model.BizTypeName;
            parameters[2].Value =model.Description;

           return DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public int Delete(int BizID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete F_BizType ");
            strSql.Append(" where BizID=@BizID ");
            SqlParameter[] parameters = {
					new SqlParameter("@BizID", SqlDbType.Int,4)};
            parameters[0].Value = BizID;

            return DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public ESP.Finance.Entity.BizTypeInfo GetModel(int BizID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 BizID,BizTypeName,Description from F_BizType ");
            strSql.Append(" where BizID=@BizID ");
            SqlParameter[] parameters = {
					new SqlParameter("@BizID", SqlDbType.Int,4)};
            parameters[0].Value = BizID;

            return CBO.FillObject<BizTypeInfo>(DbHelperSQL.Query(strSql.ToString(),  parameters));

            //ESP.Finance.Entity.BizTypeInfo model = new ESP.Finance.Entity.BizTypeInfo();
            //DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            //if (ds.Tables[0].Rows.Count > 0)
            //{
            //    if (ds.Tables[0].Rows[0]["BizID"].ToString() != "")
            //    {
            //        Entity.BizID = int.Parse(ds.Tables[0].Rows[0]["BizID"].ToString());
            //    }
            //    Entity.BizTypeName = ds.Tables[0].Rows[0]["BizTypeName"].ToString();
            //    Entity.Description = ds.Tables[0].Rows[0]["Description"].ToString();
            //    return model;
            //}
            //else
            //{
            //    return null;
            //}
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public IList<BizTypeInfo> GetList(string term, List<SqlParameter> param)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select BizID,BizTypeName,Description ");
            strSql.Append(" FROM F_BizType ");
            if (!string.IsNullOrEmpty(term))
            {
                strSql.Append(" where " + term);
            }
            //if (param != null && param.Count > 0)
            //{
            //    SqlParameter[] ps = param.ToArray();

            //    return CBO.FillCollection<BizTypeInfo>(DbHelperSQL.ExecuteReader(strSql.ToString(), ps));
            //}
            return CBO.FillCollection<BizTypeInfo>(DbHelperSQL.Query(strSql.ToString(),param));
        }


        #endregion  成员方法
    }
}

