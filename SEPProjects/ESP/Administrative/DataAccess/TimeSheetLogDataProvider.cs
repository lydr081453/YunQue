using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using ESP.Administrative.Common;
using System.Data;
using ESP.Administrative.Entity;

namespace ESP.Administrative.DataAccess
{
    /// <summary>
	/// 数据访问类:TimeSheetLogDataProvider
	/// </summary>
    public partial class TimeSheetLogDataProvider
    {
        public TimeSheetLogDataProvider()
        { }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(TimeSheetLogInfo model,SqlTransaction trans)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into AD_TimeSheetLog(");
            strSql.Append("CommitId,AuditorId,AuditorName,Suggestion,AuditDate,Status,IP)");
            strSql.Append(" values (");
            strSql.Append("@CommitId,@AuditorId,@AuditorName,@Suggestion,@AuditDate,@Status,@IP)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@CommitId", SqlDbType.Int,4),
					new SqlParameter("@AuditorId", SqlDbType.Int,4),
					new SqlParameter("@AuditorName", SqlDbType.NVarChar,50),
					new SqlParameter("@Suggestion", SqlDbType.NVarChar,500),
					new SqlParameter("@AuditDate", SqlDbType.DateTime),
					new SqlParameter("@Status", SqlDbType.Int,4),
					new SqlParameter("@IP", SqlDbType.NVarChar,50)};
            parameters[0].Value = model.CommitId;
            parameters[1].Value = model.AuditorId;
            parameters[2].Value = model.AuditorName;
            parameters[3].Value = model.Suggestion;
            parameters[4].Value = model.AuditDate;
            parameters[5].Value = model.Status;
            parameters[6].Value = model.IP;

            object obj = DbHelperSQL.GetSingle(strSql.ToString(),trans.Connection,trans, parameters);
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
        public bool Update(TimeSheetLogInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update AD_TimeSheetLog set ");
            strSql.Append("CommitId=@CommitId,");
            strSql.Append("AuditorId=@AuditorId,");
            strSql.Append("AuditorName=@AuditorName,");
            strSql.Append("Suggestion=@Suggestion,");
            strSql.Append("AuditDate=@AuditDate,");
            strSql.Append("Status=@Status,");
            strSql.Append("IP=@IP");
            strSql.Append(" where Id=@Id");
            SqlParameter[] parameters = {
					new SqlParameter("@CommitId", SqlDbType.Int,4),
					new SqlParameter("@AuditorId", SqlDbType.Int,4),
					new SqlParameter("@AuditorName", SqlDbType.NVarChar,50),
					new SqlParameter("@Suggestion", SqlDbType.NVarChar,500),
					new SqlParameter("@AuditDate", SqlDbType.DateTime),
					new SqlParameter("@Status", SqlDbType.Int,4),
					new SqlParameter("@IP", SqlDbType.NVarChar,50),
					new SqlParameter("@Id", SqlDbType.Int,4)};
            parameters[0].Value = model.CommitId;
            parameters[1].Value = model.AuditorId;
            parameters[2].Value = model.AuditorName;
            parameters[3].Value = model.Suggestion;
            parameters[4].Value = model.AuditDate;
            parameters[5].Value = model.Status;
            parameters[6].Value = model.IP;
            parameters[7].Value = model.Id;

            int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int Id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from AD_TimeSheetLog ");
            strSql.Append(" where Id=@Id");
            SqlParameter[] parameters = {
					new SqlParameter("@Id", SqlDbType.Int,4)
			};
            parameters[0].Value = Id;

            int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 批量删除数据
        /// </summary>
        public bool DeleteList(string Idlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from AD_TimeSheetLog ");
            strSql.Append(" where Id in (" + Idlist + ")  ");
            int rows = DbHelperSQL.ExecuteSql(strSql.ToString());
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public TimeSheetLogInfo GetModel(int Id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 * from AD_TimeSheetLog ");
            strSql.Append(" where Id=@Id");
            SqlParameter[] parameters = {
					new SqlParameter("@Id", SqlDbType.Int,4)
			};
            parameters[0].Value = Id;

            return CBO.FillObject<TimeSheetLogInfo>(DbHelperSQL.Query(strSql.ToString(), parameters));
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<TimeSheetLogInfo> GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append(" FROM AD_TimeSheetLog ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return CBO.FillCollection<TimeSheetLogInfo>(DbHelperSQL.Query(strSql.ToString()));
        }

        /// <summary>
        /// 获得前几行数据
        /// </summary>
        public List<TimeSheetLogInfo> GetList(int Top, string strWhere, string filedOrder)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ");
            if (Top > 0)
            {
                strSql.Append(" top " + Top.ToString());
            }
            strSql.Append(" * ");
            strSql.Append(" FROM AD_TimeSheetLog ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return CBO.FillCollection<TimeSheetLogInfo>(DbHelperSQL.Query(strSql.ToString()));
        }

        /// <summary>
        /// 获取记录总数
        /// </summary>
        public int GetRecordCount(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) FROM AD_TimeSheetLog ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            object obj = DbHelperSQL.GetSingle(strSql.ToString());
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
        /// 分页获取数据列表
        /// </summary>
        public List<TimeSheetLogInfo> GetListByPage(string strWhere, string orderby, int startIndex, int endIndex)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM ( ");
            strSql.Append(" SELECT ROW_NUMBER() OVER (");
            if (!string.IsNullOrEmpty(orderby.Trim()))
            {
                strSql.Append("order by T." + orderby);
            }
            else
            {
                strSql.Append("order by T.Id desc");
            }
            strSql.Append(")AS Row, T.*  from AD_TimeSheetLog T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                strSql.Append(" WHERE " + strWhere);
            }
            strSql.Append(" ) TT");
            strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return CBO.FillCollection<TimeSheetLogInfo>(DbHelperSQL.Query(strSql.ToString()));
        }
    }
}
