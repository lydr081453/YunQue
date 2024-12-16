using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESP.HumanResource.Entity;
using System.Data.SqlClient;
using System.Data;
using ESP.HumanResource.Utilities;
using ESP.HumanResource.Common;

namespace ESP.HumanResource.DataAccess
{
    public class InsuranceBaseInfoDataProvider
    {
        public InsuranceBaseInfo GetModel(string cityName)
        {
            			StringBuilder strSql=new StringBuilder();
			strSql.Append("select *  from sep_InsuranceBaseInfo ");
			strSql.Append(" where cityName=@cityName");
			SqlParameter[] parameters = {
					new SqlParameter("@cityName", SqlDbType.NVarChar,20)
			};
            parameters[0].Value = cityName;

            return CBO.FillObject<InsuranceBaseInfo>(DbHelperSQL.Query(strSql.ToString(), parameters));
        }

        /// <summary>
        /// 获取社保缴费上下线
        /// </summary>
        /// <param name="cityName"></param>
        /// <returns></returns>
        public List<InsuranceBase> GetBaseList(string cityName)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select *  from sep_InsuranceBase");
            strSql.Append(" where cityName=@cityName and beginDate <= @date and endDate>= @date");
            SqlParameter[] parameters = {
					new SqlParameter("@cityName", SqlDbType.NVarChar,20),
                    new SqlParameter("@date",SqlDbType.NVarChar,20)
			};
            parameters[0].Value = cityName;
            parameters[1].Value = DateTime.Now.ToString("yyyy-MM-dd");

            return CBO.FillCollection<InsuranceBase>(DbHelperSQL.Query(strSql.ToString(), parameters));
        }
    }
}
