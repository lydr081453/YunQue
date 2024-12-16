using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using ESP.Finance.Entity;
using ESP.Finance.Utility;
using System.Collections.Generic;
using ESP.Finance.IDataAccess;
namespace ESP.Finance.DataAccess
{
    internal class ProjectHistLogDataProvider : IProjectHistLogProvider
    {
        public ProjectHistLogDataProvider()
        { }
        #region  成员方法

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(ESP.Finance.Entity.ProjectHistLogInfo model, SqlTransaction trans)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into F_ProjectHistLog(");
            strSql.Append("ProjectID,Remark,LogDate,UpdateUserID,UpdateUserEmpName)");
            strSql.Append(" values (");
            strSql.Append("@ProjectID,@Remark,@LogDate,@UpdateUserID,@UpdateUserEmpName)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@ProjectID", SqlDbType.Int,4),
					new SqlParameter("@Remark", SqlDbType.NVarChar,4000),
					new SqlParameter("@LogDate", SqlDbType.DateTime),
					new SqlParameter("@UpdateUserID", SqlDbType.Int,4),
					new SqlParameter("@UpdateUserEmpName", SqlDbType.NVarChar,50)};
            parameters[0].Value = model.ProjectID;
            parameters[1].Value = model.Remark;
            parameters[2].Value = model.LogDate;
            parameters[3].Value = model.UpdateUserID;
            parameters[4].Value = model.UpdateUserEmpName;

            object obj = DbHelperSQL.GetSingle(strSql.ToString(), trans, parameters);
            if (obj == null)
            {
                return 1;
            }
            else
            {
                return Convert.ToInt32(obj);
            }
        }
        public int Add(ESP.Finance.Entity.ProjectHistLogInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into F_ProjectHistLog(");
            strSql.Append("ProjectID,Remark,LogDate,UpdateUserID,UpdateUserEmpName)");
            strSql.Append(" values (");
            strSql.Append("@ProjectID,@Remark,@LogDate,@UpdateUserID,@UpdateUserEmpName)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@ProjectID", SqlDbType.Int,4),
					new SqlParameter("@Remark", SqlDbType.NVarChar,4000),
					new SqlParameter("@LogDate", SqlDbType.DateTime),
					new SqlParameter("@UpdateUserID", SqlDbType.Int,4),
					new SqlParameter("@UpdateUserEmpName", SqlDbType.NVarChar,50)};
            parameters[0].Value = model.ProjectID;
            parameters[1].Value = model.Remark;
            parameters[2].Value = model.LogDate;
            parameters[3].Value = model.UpdateUserID;
            parameters[4].Value = model.UpdateUserEmpName;

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
        /// 得到一个对象实体
        /// </summary>
        public ESP.Finance.Entity.ProjectHistLogInfo GetModel(int LogID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 LogID,ProjectID,Remark,LogDate,UpdateUserID,UpdateUserEmpName from F_ProjectHistLog ");
            strSql.Append(" where LogID=@LogID ");
            SqlParameter[] parameters = {
					new SqlParameter("@LogID", SqlDbType.Int,4)};
            parameters[0].Value = LogID;
            return CBO.FillObject<ESP.Finance.Entity.ProjectHistLogInfo>(DbHelperSQL.Query(strSql.ToString(), parameters));
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public IList<ESP.Finance.Entity.ProjectHistLogInfo> GetList(string strWhere, List<SqlParameter> parms)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select LogID,ProjectID,Remark,LogDate,UpdateUserID,UpdateUserEmpName ");
            strSql.Append(" FROM F_ProjectHistLog ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return CBO.FillCollection<ESP.Finance.Entity.ProjectHistLogInfo>(DbHelperSQL.Query(strSql.ToString(), parms));
        }

        #endregion  成员方法
    }
}
