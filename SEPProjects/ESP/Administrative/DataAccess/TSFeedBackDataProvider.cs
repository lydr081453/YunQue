using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESP.Administrative.Entity;
using System.Data.SqlClient;
using System.Data;
using ESP.Administrative.Common;

namespace ESP.Administrative.DataAccess
{
    public partial class TSFeedBackDataProvider
    {
        public TSFeedBackDataProvider()
        { }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(TSFeedBackInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into AD_TSFeedBack(");
            strSql.Append("UserId,UserName,FeedBack,CreateTime,CreateIp)");
            strSql.Append(" values (");
            strSql.Append("@UserId,@UserName,@FeedBack,@CreateTime,@CreateIp)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@UserId", SqlDbType.Int,4),
					new SqlParameter("@UserName", SqlDbType.NVarChar,50),
					new SqlParameter("@FeedBack", SqlDbType.NVarChar,500),
					new SqlParameter("@CreateTime", SqlDbType.DateTime),
					new SqlParameter("@CreateIp", SqlDbType.NVarChar,50)};
            parameters[0].Value = model.UserId;
            parameters[1].Value = model.UserName;
            parameters[2].Value = model.FeedBack;
            parameters[3].Value = model.CreateTime;
            parameters[4].Value = model.CreateIp;

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
        /// 得到一个对象实体
        /// </summary>
        public TSFeedBackInfo GetModel(int Id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 Id,UserId,UserName,FeedBack,CreateTime,CreateIp from AD_TSFeedBack ");
            strSql.Append(" where Id=@Id");
            SqlParameter[] parameters = {
					new SqlParameter("@Id", SqlDbType.Int,4)
			};
            parameters[0].Value = Id;

            return CBO.FillObject<TSFeedBackInfo>(DbHelperSQL.Query(strSql.ToString(), parameters));
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<TSFeedBackInfo> GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Id,UserId,UserName,FeedBack,CreateTime,CreateIp ");
            strSql.Append(" FROM AD_TSFeedBack ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return CBO.FillCollection<TSFeedBackInfo>(DbHelperSQL.Query(strSql.ToString()));
        }
    }
}
