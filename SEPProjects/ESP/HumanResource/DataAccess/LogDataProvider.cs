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
    public class LogDataProvider
    {
        public LogDataProvider()
        {
        }
        public int Add(LogInfo model)
        {
            return Add(model, null);
        }

        public int Add(LogInfo model, SqlTransaction trans)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into SEP_Log(");
            strSql.Append("Des,LogUserId,LogModifiedTime,Status,sysId,sysUserName,LogUserName)");
            strSql.Append(" values (");
            strSql.Append("@Des,@LogUserId,@LogModifiedTime,@Status,@sysId,@sysUserName,@LogUserName)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@LogId", SqlDbType.Int,8),					
					new SqlParameter("@Des", SqlDbType.NVarChar,4000),
					new SqlParameter("@LogUserId", SqlDbType.Int,6),
					new SqlParameter("@LogModifiedTime", SqlDbType.DateTime),
					new SqlParameter("@Status", SqlDbType.Int,4),
                    new SqlParameter("@sysId",SqlDbType.Int,4),
                    new SqlParameter("@sysUserName",SqlDbType.NVarChar,50),
                    new SqlParameter("@LogUserName",SqlDbType.NVarChar,50)
            };
            parameters[0].Value = model.LogId;
            parameters[1].Value = model.Des;
            parameters[2].Value = model.LogUserId;
            parameters[3].Value = model.LogMedifiedTeme;
            parameters[4].Value = model.Status;
            parameters[5].Value = model.SysId;
            parameters[6].Value = model.SysUserName;
            parameters[7].Value = model.LogUserName;

            object obj = DbHelperSQL.GetSingle(strSql.ToString(), trans.Connection, trans, parameters);
            if (obj == null)
            {
                return 1;
            }
            else
            {
                return Convert.ToInt32(obj);
            }
        }

        public LogInfo GetLogModel(string logDes, int userid, string username, int sysid, string sysusername, int status)
        {
            LogInfo model = new LogInfo();
            model.LogUserId = userid;
            model.LogMedifiedTeme = DateTime.Now;
            model.Des = logDes;
            model.LogUserName = username;
            model.SysId = sysid;
            model.SysUserName = sysusername;
            model.Status = status;
            return model;
        }

        public DataSet GetList(string strWhere, List<SqlParameter> parms)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select LogId,Des,LogUserId,LogModifiedTime,Status,sysId,sysUserName,LogUserName ");
            strSql.Append(" FROM SEP_Log where 1=1");
            if (strWhere.Trim() != "")
            {
                strSql.Append("  " + strWhere);
            }
            strSql.Append(" Order By LogModifiedTime  desc");
            return DbHelperSQL.Query(strSql.ToString(), parms.ToArray());
        }



        public void Delete(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("Delete ");
            strSql.Append(" FROM SEP_Log ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            DbHelperSQL.ExecuteSql(strSql.ToString());
        }


    }
}
