using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using ESP.Purchase.Common;
using System.Data.SqlClient;

namespace ESP.Purchase.DataAccess
{
    public class SupplierSendDataProvider
    {
        public static int Add(Entity.SupplierSendInfo model,SqlTransaction trans)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into T_SupplierSend(");
            strSql.Append("dataId,dataType,email)");
            strSql.Append(" values (");
            strSql.Append("@dataId,@dataType,@email)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@dataId", SqlDbType.Int,4),
					new SqlParameter("@dataType", SqlDbType.Int,4),
					new SqlParameter("@email", SqlDbType.VarChar,50)};
            parameters[0].Value = model.DataId;
            parameters[1].Value = model.DataType;
            parameters[2].Value = model.Email;

            object obj = null;
            if (trans == null)
                obj = DbHelperSQL.GetSingle(strSql.ToString(), parameters);
            else
                obj = DbHelperSQL.GetSingle(strSql.ToString(), trans.Connection, trans, parameters);
            if (obj == null)
            {
                return 1;
            }
            else
            {
                return Convert.ToInt32(obj);
            }
        }

        public static int Delete(int dataId, int dataType)
        {
            return Delete(dataId, dataType, null);
        }

        public static int Delete(int dataId,int dataType,SqlTransaction trans)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete T_SupplierSend ");
            strSql.Append(" where dataId=@dataId and dataType=@dataType");
            SqlParameter[] parameters = {
					new SqlParameter("@dataId", SqlDbType.Int,4),
                    new SqlParameter("@dataType", SqlDbType.Int,4)};
            parameters[0].Value = dataId;
            parameters[1].Value = dataType;
            if (trans == null)
                return DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
            else
                return DbHelperSQL.ExecuteSql(strSql.ToString(), trans.Connection, trans, parameters);
        }

        public static Entity.SupplierSendInfo GetModel(int dataId, int dataType)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from T_SupplierSend ");
            strSql.Append(" where dataId=@dataId and dataType=@dataType");
            SqlParameter[] parameters = {
					new SqlParameter("@dataId", SqlDbType.Int,4),
                    new SqlParameter("@dataType", SqlDbType.Int,4)};
            parameters[0].Value = dataId;
            parameters[1].Value = dataType;
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                Entity.SupplierSendInfo model = new ESP.Purchase.Entity.SupplierSendInfo();
                model.Id = ds.Tables[0].Rows[0]["Id"] == DBNull.Value ? 0 : int.Parse(ds.Tables[0].Rows[0]["Id"].ToString());
                model.DataId = ds.Tables[0].Rows[0]["DataId"] == DBNull.Value ? 0 : int.Parse(ds.Tables[0].Rows[0]["DataId"].ToString());
                model.DataType = ds.Tables[0].Rows[0]["DataType"] == DBNull.Value ? 0 : int.Parse(ds.Tables[0].Rows[0]["DataType"].ToString());
                model.Email = ds.Tables[0].Rows[0]["Email"].ToString();
                return model;
            }
            return null;
        }
    }
}
