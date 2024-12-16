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
    public class LotterySQLProvider
    {
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from [SYP_Lottery]");
            strSql.Append(" where sysuserid=@id ");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)};
            parameters[0].Value = id;

            return Convert.ToBoolean(SqlHelper.ExecuteScalar(SqlHelper.CONN_STRING, CommandType.Text, strSql.ToString(), parameters));
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(LotteryInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into [SYP_Lottery](");
            strSql.Append(@"[UserID]
                           ,[CreatedDate]
                           ,[LotteryLevel],[IP])");
            strSql.Append(" values (");
            strSql.Append(@"@UserID
                           ,@CreatedDate
                           ,@LotteryLevel,@IP)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@UserID", SqlDbType.Int, 6),
					new SqlParameter("@CreatedDate", SqlDbType.DateTime),
					new SqlParameter("@LotteryLevel", SqlDbType.NVarChar,500),
					new SqlParameter("@IP", SqlDbType.NVarChar,500)};
            parameters[0].Value = model.UserID;
            parameters[1].Value = DateTime.Now;
            parameters[2].Value = model.LotteryLevel;
            parameters[3].Value = model.IP;

            object obj = SqlHelper.ExecuteScalar(SqlHelper.CONN_STRING, CommandType.Text, strSql.ToString(), parameters);
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
        /// 获得数据列表
        /// </summary>
        public IList<LotteryInfo> GetList(string term)
        {
            string strSql = "select * FROM SYP_Lottery ";
            if (!string.IsNullOrEmpty(term))
            {
                strSql += " where " + term;
            }
            return CBO.FillCollection<LotteryInfo>(
                SqlHelper.ExecuteReader(SqlHelper.CONN_STRING, CommandType.Text, strSql.ToString()));
        }

    }
}