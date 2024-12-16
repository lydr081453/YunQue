using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using ESP.Purchase.Common;
using System.Data;
using ESP.Supplier.Entity;

namespace ESP.Supplier.DataAccess
{
    public class SC_SupplierMessageAuditedLogDataProvider
    {
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(SC_SupplierMessageAuditedLog model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into SC_SupplierMessageAuditedLog(");
            strSql.Append("[msgId],[createUserId],[createUserName],[createDate],[createIp],[createDesc])");
            strSql.Append(" values (");
            strSql.Append("@msgId,@createUserId,@createUserName,@createDate,@createIp,@createDesc)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@msgId", SqlDbType.Int,4),
					new SqlParameter("@createUserId", SqlDbType.Int,4),
					new SqlParameter("@createUserName", SqlDbType.NVarChar),
                    new SqlParameter("@createDate",SqlDbType.DateTime),
                    new SqlParameter("@createIp",SqlDbType.NVarChar),
                    new SqlParameter("@createDesc",SqlDbType.NVarChar)
                                        };
            parameters[0].Value = model.msgId;
            parameters[1].Value = model.CreateUserId;
            parameters[2].Value = model.CreateUserName;
            parameters[3].Value = model.CreateDate;
            parameters[4].Value = model.CreateIp;
            parameters[5].Value = model.CreateDesc;

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
        /// 增加一条数据
        /// </summary>
        public int Add(SC_SupplierMessageAuditedLog model, SqlTransaction trans)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into SC_SupplierMessageAuditedLog(");
            strSql.Append("[msgId],[createUserId],[createUserName],[createDate],[createIp],[createDesc])");
            strSql.Append(" values (");
            strSql.Append("@msgId,@createUserId,@createUserName,@createDate,@createIp,@createDesc)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@msgId", SqlDbType.Int,4),
					new SqlParameter("@createUserId", SqlDbType.Int,4),
					new SqlParameter("@createUserName", SqlDbType.NVarChar),
                    new SqlParameter("@createDate",SqlDbType.DateTime),
                    new SqlParameter("@createIp",SqlDbType.NVarChar),
                    new SqlParameter("@createDesc",SqlDbType.NVarChar)
                                        };
            parameters[0].Value = model.msgId;
            parameters[1].Value = model.CreateUserId;
            parameters[2].Value = model.CreateUserName;
            parameters[3].Value = model.CreateDate;
            parameters[4].Value = model.CreateIp;
            parameters[5].Value = model.CreateDesc;

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

    }
}
