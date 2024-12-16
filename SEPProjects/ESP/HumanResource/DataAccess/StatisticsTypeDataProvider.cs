using System;
using System.Collections.Generic;
using ESP.HumanResource.Entity;
using System.Web;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using ESP.HumanResource.Common;

namespace ESP.HumanResource.DataAccess
{
    public class StatisticsTypeDataProvider
    {
        public StatisticsTypeDataProvider()
        { }
        #region  成员方法

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(StatisticsTypeInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into ESP_StatisticsType(");
            strSql.Append("StatisticsTypeName,TypeID,IsDeleted,StatisticsTypeValue)");
            strSql.Append(" values (");
            strSql.Append("@StatisticsTypeName,@TypeID,@IsDeleted,@StatisticsTypeValue)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@StatisticsTypeName", SqlDbType.NVarChar,500),
					new SqlParameter("@TypeID", SqlDbType.NVarChar,500),
					new SqlParameter("@IsDeleted", SqlDbType.Bit,1),
					new SqlParameter("@StatisticsTypeValue", SqlDbType.NVarChar,500)};
            parameters[0].Value = model.StatisticsTypeName;
            parameters[1].Value = model.TypeID;
            parameters[2].Value = model.IsDeleted;
            parameters[3].Value = model.StatisticsTypeValue;

            object obj = DbHelperSQL.GetSingle(strSql.ToString(), parameters);
            if (obj == null)
            {
                return 1;
            }
            else
            {
                return Convert.ToInt32(obj);
            }
        }
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public void Update(StatisticsTypeInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update ESP_StatisticsType set ");
            strSql.Append("StatisticsTypeName=@StatisticsTypeName,");
            strSql.Append("TypeID=@TypeID,");
            strSql.Append("IsDeleted=@IsDeleted,");
            strSql.Append("StatisticsTypeValue=@StatisticsTypeValue");
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4),
					new SqlParameter("@StatisticsTypeName", SqlDbType.NVarChar,500),
					new SqlParameter("@TypeID", SqlDbType.NVarChar,500),
					new SqlParameter("@IsDeleted", SqlDbType.Bit,1),
					new SqlParameter("@StatisticsTypeValue", SqlDbType.NVarChar,500)};
            parameters[0].Value = model.ID;
            parameters[1].Value = model.StatisticsTypeName;
            parameters[2].Value = model.TypeID;
            parameters[3].Value = model.IsDeleted;
            parameters[4].Value = model.StatisticsTypeValue;

            DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public void Delete(int ID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete ESP_StatisticsType ");
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)};
            parameters[0].Value = ID;

            DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public StatisticsTypeInfo GetModel(int ID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 ID,StatisticsTypeName,TypeID,IsDeleted,StatisticsTypeValue from ESP_StatisticsType ");
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)};
            parameters[0].Value = ID;

            StatisticsTypeInfo model = new StatisticsTypeInfo();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["ID"].ToString() != "")
                {
                    model.ID = int.Parse(ds.Tables[0].Rows[0]["ID"].ToString());
                }
                model.StatisticsTypeName = ds.Tables[0].Rows[0]["StatisticsTypeName"].ToString();
                model.TypeID = ds.Tables[0].Rows[0]["TypeID"].ToString();
                if (ds.Tables[0].Rows[0]["IsDeleted"].ToString() != "")
                {
                    if ((ds.Tables[0].Rows[0]["IsDeleted"].ToString() == "1") || (ds.Tables[0].Rows[0]["IsDeleted"].ToString().ToLower() == "true"))
                    {
                        model.IsDeleted = true;
                    }
                    else
                    {
                        model.IsDeleted = false;
                    }
                }
                model.StatisticsTypeValue = ds.Tables[0].Rows[0]["StatisticsTypeValue"].ToString();
                return model;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ID,StatisticsTypeName,TypeID,IsDeleted,StatisticsTypeValue ");
            strSql.Append(" FROM ESP_StatisticsType ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }

        #endregion  成员方法
    }
}
