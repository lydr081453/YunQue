using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using ESP.Administrative.Common;

namespace ESP.Administrative.DataAccess
{
    public class LogDataProvider
    {
        public LogDataProvider()
        { }
        #region  成员方法

        /// <summary>
        /// 得到最大ID
        /// </summary>
        public int GetMaxId()
        {
            return DbHelperSQL.GetMaxID("ID", "AD_Log");
        }

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from AD_Log");
            strSql.Append(" where ID= @ID");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)
				};
            parameters[0].Value = ID;
            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(ESP.Administrative.Entity.LogInfo model)
        {
            //model.ID=GetMaxId();
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into AD_Log(");
            strSql.Append("Content,Time,IsSuccess)");
            strSql.Append(" values (");
            strSql.Append("@Content,@Time,@IsSuccess)");
            SqlParameter[] parameters = {
					new SqlParameter("@Content", SqlDbType.NVarChar),
					new SqlParameter("@Time", SqlDbType.DateTime),
					new SqlParameter("@IsSuccess", SqlDbType.Bit,1)};
            parameters[0].Value = model.Content;
            parameters[1].Value = model.Time;
            parameters[2].Value = model.IsSuccess;

            DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
            return model.ID;
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public void Update(ESP.Administrative.Entity.LogInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update AD_Log set ");
            strSql.Append("Content=@Content,");
            strSql.Append("Time=@Time,");
            strSql.Append("IsSuccess=@IsSuccess");
            strSql.Append(" where ID=@ID");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4),
					new SqlParameter("@Content", SqlDbType.NVarChar),
					new SqlParameter("@Time", SqlDbType.DateTime),
					new SqlParameter("@IsSuccess", SqlDbType.Bit,1)};
            parameters[0].Value = model.ID;
            parameters[1].Value = model.Content;
            parameters[2].Value = model.Time;
            parameters[3].Value = model.IsSuccess;

            DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public void Delete(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete AD_Log ");
            strSql.Append(" where ID=@ID");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)
				};
            parameters[0].Value = ID;
            DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public ESP.Administrative.Entity.LogInfo GetModel(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from AD_Log ");
            strSql.Append(" where ID=@ID");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)};
            parameters[0].Value = ID;
            ESP.Administrative.Entity.LogInfo model = new ESP.Administrative.Entity.LogInfo();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            model.ID = ID;
            if (ds.Tables[0].Rows.Count > 0)
            {
                model.PopupData(ds.Tables[0].Rows[0]);
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
            strSql.Append("select * FROM AD_Log ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }
        #endregion  成员方法
    }
}