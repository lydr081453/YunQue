using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESP.Supplier.Common;
using ESP.Supplier.Entity;
using System.Data;
using System.Data.SqlClient;

namespace ESP.Supplier.DataAccess
{
    public class SC_ReturnMessageWillReadDataProvider
    {
        public SC_ReturnMessageWillReadDataProvider()
        { }
        #region  成员方法


        /// <summary>
        /// 添加一条数据
        /// </summary>
        public int Add(SC_ReturnMessageWillRead model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into SC_ReturnMessageWillRead ( ");
            strSql.Append("ReturnMsgID,");
            strSql.Append("UserID,");
            strSql.Append("MsgUserType,");
            strSql.Append("CreatedDate,");
            strSql.Append("CreatedUserName,");
            strSql.Append("IsWillRead)");
            strSql.Append(" Values (");
            strSql.Append("@ReturnMsgID,");
            strSql.Append("@UserID,");
            strSql.Append("@MsgUserType,");
            strSql.Append("@CreatedDate,");
            strSql.Append("@CreatedUserName,");
            strSql.Append("@IsWillRead");
            strSql.Append(" )");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
                    //new SqlParameter("@ID", SqlDbType.Int,4),
                    new SqlParameter("@ReturnMsgID", SqlDbType.Int,4),
                    new SqlParameter("@UserID", SqlDbType.Int,4),
                    new SqlParameter("@MsgUserType", SqlDbType.Int,4),
					new SqlParameter("@CreatedDate", SqlDbType.DateTime),
                    new SqlParameter("@CreatedUserName", SqlDbType.NVarChar),
					new SqlParameter("@IsWillRead", SqlDbType.Bit)};
            //parameters[0].Value = model.ID;
            parameters[0].Value = model.ReturnMsgID;
            parameters[1].Value = model.UserID;
            parameters[2].Value = model.MsgUserType;
            parameters[3].Value = DateTime.Now;;
            parameters[4].Value = model.CreatedUserName;
            parameters[5].Value = model.IsWillRead;
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

        #endregion
    }
}
