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
    public class LotteryUserSQLProvider
    {
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public IList<LotteryUserInfo> GetList()
        {
            string strSql = @"select ROW_NUMBER() OVER (ORDER BY userid) AS [Index], userid as UserID,Username as Username,LastnameCN+FirstNameCn as [Name]
                from sep_Users
                where userid in
	                (select distinct SupporterUserID 
		                from dbo.SYP_Supporter 
		                where SupporterUserID <> '13233') and userid not in (select userid from dbo.SYP_Lottery)";
            return CBO.FillCollection<LotteryUserInfo>(
                SqlHelper.ExecuteReader(SqlHelper.CONN_STRING, CommandType.Text, strSql.ToString()));
        }

    }
}