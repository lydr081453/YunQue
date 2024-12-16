using System;
using System.Collections.Generic;
using System.Web;
using System.Data.SqlClient;
using ESP.HumanResource.Common;
using System.Data;
using System.Text;
using ESP.HumanResource.Entity;
using ESP.HumanResource.Utilities;

namespace ESP.HumanResource.DataAccess
{
    public class RejectLogDataProvider
    {
        public int Insert(RejectLogInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into sep_RejectLog values(@UserID,@Description,@Operator,@OperateDate);select @@IDENTITY");
            SqlParameter[] parameters = { 
                           new SqlParameter("@UserID",SqlDbType.Int,4),
                           new SqlParameter("@Description",SqlDbType.NVarChar,1000),
                           new SqlParameter("@Operator",SqlDbType.Int,4),
                           new SqlParameter("@OperateDate",SqlDbType.DateTime)
                                        };
            parameters[0].Value = model.UserID;
            parameters[1].Value = model.Description;
            parameters[2].Value = model.Operator;
            parameters[3].Value = model.OperateDate;
            object obj = DbHelperSQL.GetSingle(strSql.ToString(),parameters);
            if (obj == null)
            {
                return 1;
            }
            else
            {
                return Convert.ToInt32(obj);
            }
        }

        public List<RejectLogInfo> GetList(int userId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select *");
            strSql.Append(" FROM SEP_RejectLog ");
            strSql.Append(" where UserID=" + userId);
            return CBO.FillCollection<RejectLogInfo>(DbHelperSQL.Query(strSql.ToString()));
        }
    }
}
