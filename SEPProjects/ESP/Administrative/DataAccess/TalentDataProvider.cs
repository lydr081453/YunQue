using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESP.Administrative.Common;
using ESP.Administrative.Entity;
using System.Data.SqlClient;
using System.Data;

namespace ESP.Administrative.DataAccess
{
   public class TalentDataProvider
    {
        public TalentDataProvider()
        { }

        #region  成员方法


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public TalentInfo GetModel(int UserId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM dbo.V_Talent ");
            strSql.Append(" WHERE UserId=@UserId");
            SqlParameter[] parameters = {
					new SqlParameter("@UserId", SqlDbType.Int,4)};
            parameters[0].Value = UserId;

            return CBO.FillObject<TalentInfo>(DbHelperSQL.Query(strSql.ToString(), parameters));

        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public IList<TalentInfo> GetList(string strWhere,string orderBy)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM V_Talent ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" WHERE " + strWhere);
            }
            if (!string.IsNullOrEmpty(orderBy))
            {
                strSql.Append(orderBy);
            }
            return CBO.FillCollection<TalentInfo>(DbHelperSQL.Query(strSql.ToString()));
        }

        #endregion  成员方法
   
    }
}
