using System;
using System.Data;
using System.Text;
using ESP.Finance.Utility;
using System.Data.SqlClient;
using System.Collections;
using ESP.Finance.Entity;
using System.Collections.Generic;
//using Maticsoft.DBUtility;//请先添加引用
namespace ESP.Finance.DataAccess
{
    /// <summary>
    /// 数据访问类F_BizDescription。
    /// </summary>
    internal class BizDescriptionDataProvider : ESP.Finance.IDataAccess.IBizDescriptionDataProvider
    {
        
        #region  成员方法
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int BizDescID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from F_BizDescription");
            strSql.Append(" where BizDescID=@BizDescID ");
            SqlParameter[] parameters = {
					new SqlParameter("@BizDescID", SqlDbType.Int,4)};
            parameters[0].Value = BizDescID;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(ESP.Finance.Entity.BizDescriptionInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into F_BizDescription(");
            strSql.Append("BizDescription)");
            strSql.Append(" values (");
            strSql.Append("@BizDescription)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@BizDescription", SqlDbType.NVarChar,500)};
            parameters[0].Value =model.BizDescription;

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
        public int Update(ESP.Finance.Entity.BizDescriptionInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update F_BizDescription set ");
            strSql.Append("BizDescription=@BizDescription");
            strSql.Append(" where BizDescID=@BizDescID ");
            SqlParameter[] parameters = {
					new SqlParameter("@BizDescID", SqlDbType.Int,4),
					new SqlParameter("@BizDescription", SqlDbType.NVarChar,500)};
            parameters[0].Value =model.BizDescID;
            parameters[1].Value =model.BizDescription;

            return DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public int Delete(int BizDescID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete F_BizDescription ");
            strSql.Append(" where BizDescID=@BizDescID ");
            SqlParameter[] parameters = {
					new SqlParameter("@BizDescID", SqlDbType.Int,4)};
            parameters[0].Value = BizDescID;

           return DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public ESP.Finance.Entity.BizDescriptionInfo GetModel(int BizDescID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 BizDescID,BizDescription from F_BizDescription ");
            strSql.Append(" where BizDescID=@BizDescID ");
            SqlParameter[] parameters = {
					new SqlParameter("@BizDescID", SqlDbType.Int,4)};
            parameters[0].Value = BizDescID;

            return CBO.FillObject<BizDescriptionInfo>(DbHelperSQL.Query(strSql.ToString(),  parameters));

            //ESP.Finance.Entity.BizDescriptionInfo model = new ESP.Finance.Entity.BizDescriptionInfo();
            //DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            //if (ds.Tables[0].Rows.Count > 0)
            //{
            //    if (ds.Tables[0].Rows[0]["BizDescID"].ToString() != "")
            //    {
            //        Entity.BizDescID = int.Parse(ds.Tables[0].Rows[0]["BizDescID"].ToString());
            //    }
            //    Entity.BizDescription = ds.Tables[0].Rows[0]["BizDescription"].ToString();
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
        public IList<BizDescriptionInfo> GetList(string term, List<SqlParameter> param)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select BizDescID,BizDescription ");
            strSql.Append(" FROM F_BizDescription ");
            if (!string.IsNullOrEmpty(term))
            {
                strSql.Append(" where " + term);
            }
            //if (param != null && param.Count > 0)
            //{
            //    SqlParameter[] ps = param.ToArray();

            //    return CBO.FillCollection<BizDescriptionInfo>(DbHelperSQL.ExecuteReader(strSql.ToString(), ps));
            //}
            return CBO.FillCollection<BizDescriptionInfo>(DbHelperSQL.Query(strSql.ToString(),param));
        }

        

        #endregion  成员方法
    }
}

