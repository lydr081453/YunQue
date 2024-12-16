using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using MyPhotoUtility;
using Microsoft.ApplicationBlocks.Data;
using MyPhotoInfo;

namespace MyPhotoSQLServerDAL
{
    public class AdminSQLProvider
    {
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from [SYP_Admin]");
            strSql.Append(" where sysuserid=@id ");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)};
            parameters[0].Value = id;

            return Convert.ToBoolean(SqlHelper.ExecuteScalar(SqlHelper.CONN_STRING, CommandType.Text, strSql.ToString(), parameters));
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public IList<AdminInfo> GetList(string term)
        {
            string strSql = "select *  FROM SYP_Admin ";
            if (!string.IsNullOrEmpty(term))
            {
                strSql += " where " + term;
            }
            return CBO.FillCollection<AdminInfo>(
                SqlHelper.ExecuteReader(SqlHelper.CONN_STRING, CommandType.Text, strSql.ToString()));
        }

    }
}