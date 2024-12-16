using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using ESP.Finance.Entity;
using ESP.Finance.Utility;
using ESP.Finance.IDataAccess;
using System.Collections.Generic;
namespace ESP.Finance.DataAccess
{
    internal class DeptTargetDataProvider:IDeptTargetProvider
    {
        public ESP.Finance.Entity.DeptTargetInfo GetModel(int deptid ,int year)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  * from F_DeptTarget ");
            strSql.Append(" where deptid=@deptid and year =@year ");
            SqlParameter[] parameters = {
					new SqlParameter("@deptid", SqlDbType.Int,4),
                    new SqlParameter("@year", SqlDbType.Int,4)};
            parameters[0].Value = deptid;
               parameters[1].Value = year;

            return CBO.FillObject<DeptTargetInfo>(DbHelperSQL.Query(strSql.ToString(), parameters));


        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public IList<DeptTargetInfo> GetList(string term)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append(" FROM F_DeptTarget ");
            if (!string.IsNullOrEmpty(term))
            {
                strSql.Append(" where " + term);
            }
            return CBO.FillCollection<DeptTargetInfo>(DbHelperSQL.Query(strSql.ToString()));
        }

    }
}
