using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Supply.Common;
using System.Data.SqlClient;
using System.Data;
using Supply.Entity;
using ESP.Supplier.Common;
using ESP.ConfigCommon;

namespace Supply.DataAccess
{
    public class QuestionProvider
    {
        public QuestionProvider()
        { }
        #region  成员方法

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(Question model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into Question(");
            strSql.Append("title,question,remark,validityTimeBegin,validityTimeEnd,updateTime,createUser,status)");
            strSql.Append(" values (");
            strSql.Append("@title,@question,@remark,@validityTimeBegin,@validityTimeEnd,@updateTime,@createUser,@status)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@title", SqlDbType.NVarChar,50),
					new SqlParameter("@question", SqlDbType.NVarChar,4000),
					new SqlParameter("@remark", SqlDbType.NVarChar,500),
					new SqlParameter("@validityTimeBegin", SqlDbType.DateTime),
                    new SqlParameter("@validityTimeEnd",SqlDbType.DateTime),
					new SqlParameter("@updateTime", SqlDbType.DateTime),
					new SqlParameter("@createUser", SqlDbType.Int,4),
					new SqlParameter("@status", SqlDbType.Int,4)};
            parameters[0].Value = model.title;
            parameters[1].Value = model.question;
            parameters[2].Value = model.remark;
            parameters[3].Value = model.validityTimeBegin;
            parameters[4].Value = model.validityTimeEnd;
            parameters[5].Value = model.updateTime;
            parameters[6].Value = model.createUser;
            parameters[7].Value = model.status;

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
        public void Update(Question model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Question set ");
            strSql.Append("title=@title,");
            strSql.Append("question=@question,");
            strSql.Append("remark=@remark,");
            strSql.Append("validityTimeBegin=@validityTimeBegin,validityTimeEnd=@validityTimeEnd,");
            strSql.Append("updateTime=@updateTime,");
            strSql.Append("createUser=@createUser,");
            strSql.Append("status=@status");
            strSql.Append(" where id=@id ");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4),
					new SqlParameter("@title", SqlDbType.NVarChar,50),
					new SqlParameter("@question", SqlDbType.NVarChar,4000),
					new SqlParameter("@remark", SqlDbType.NVarChar,500),
					new SqlParameter("@validityTimeBegin", SqlDbType.DateTime),
                    new SqlParameter("@validityTimeEnd",SqlDbType.DateTime),
					new SqlParameter("@updateTime", SqlDbType.DateTime),
					new SqlParameter("@createUser", SqlDbType.Int,4),
					new SqlParameter("@status", SqlDbType.Int,4)};
            parameters[0].Value = model.id;
            parameters[1].Value = model.title;
            parameters[2].Value = model.question;
            parameters[3].Value = model.remark;
            parameters[4].Value = model.validityTimeBegin;
            parameters[5].Value = model.validityTimeEnd;
            parameters[6].Value = model.updateTime;
            parameters[7].Value = model.createUser;
            parameters[8].Value = model.status;

            DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public void Delete(int id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from Question ");
            strSql.Append(" where id=@id ");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)};
            parameters[0].Value = id;

            DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Question GetModel(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 * from Question ");
            strSql.Append(" where id=@id ");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)};
            parameters[0].Value = id;

            return CBO.FillObject<Question>(DbHelperSQL.Query(strSql.ToString(), parameters));
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<Question> GetList(string strWhere,List<SqlParameter> parms)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select a.*,b.userName  ");
            strSql.Append(" FROM Question as a");
            strSql.Append(" inner join Users as b on a.createUser=b.id");
            strSql.Append(" where 1=1");
            if (strWhere.Trim() != "")
            {
                strSql.Append(strWhere);
            }
            return CBO.FillCollection<Question>(DbHelperSQL.Query(strSql.ToString(),parms.ToArray()));
        }

        #endregion  成员方法
    }
}
