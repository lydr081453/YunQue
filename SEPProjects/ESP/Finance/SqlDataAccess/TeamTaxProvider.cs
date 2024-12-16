using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using ESP.Finance.Entity;
using System.Collections.Generic;
using ESP.Finance.Utility;
using ESP.Finance.DataAccess;

namespace ESP.Finance.DataAccess
{
    internal class TeamTaxProvider : ESP.Finance.IDataAccess.ITeamTaxProvider
    {

        #region  成员方法
        /// <summary>
        /// 是否存在该记录
        /// </summary>

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(ESP.Finance.Entity.TeamTaxInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into F_TeamTax(");
            strSql.Append("DepartmentId,DepartmentName,TaxYear,TaxMonth,Tax)");
            strSql.Append(" values (");
            strSql.Append("@DepartmentId,@DepartmentName,@TaxYear,@TaxMonth,@Tax)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@DepartmentId", SqlDbType.Int),
					new SqlParameter("@DepartmentName", SqlDbType.NVarChar,50),
					new SqlParameter("@TaxYear", SqlDbType.Int),
					new SqlParameter("@TaxMonth", SqlDbType.Int),
					new SqlParameter("@Tax", SqlDbType.Decimal,20),
				
                                        };
            parameters[0].Value = model.DepartmentId;
            parameters[1].Value = model.DepartmentName;
            parameters[2].Value = model.TaxYear;
            parameters[3].Value = model.TaxMonth;
            parameters[4].Value = model.Tax;

            object obj = DbHelperSQL.GetSingle(strSql.ToString(), parameters);
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
        /// 更新一条数据
        /// </summary>
        public int Update(ESP.Finance.Entity.TeamTaxInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update F_TeamTax set ");
            strSql.Append("DepartmentId=@DepartmentId,");
            strSql.Append("DepartmentName=@DepartmentName,");
            strSql.Append("TaxYear=@TaxYear,");
            strSql.Append("TaxMonth=@TaxMonth,");
            strSql.Append("Tax=@Tax");
            strSql.Append(" where id=@id ");

            SqlParameter[] parameters = {
					new SqlParameter("@DepartmentId", SqlDbType.Int),
					new SqlParameter("@DepartmentName", SqlDbType.NVarChar,50),
					new SqlParameter("@TaxYear", SqlDbType.Int),
					new SqlParameter("@TaxMonth", SqlDbType.Int),
					new SqlParameter("@Tax", SqlDbType.Decimal,20),
					new SqlParameter("@id", SqlDbType.Int)
                                        };
            parameters[0].Value = model.DepartmentId;
            parameters[1].Value = model.DepartmentName;
            parameters[2].Value = model.TaxYear;
            parameters[3].Value = model.TaxMonth;
            parameters[4].Value = model.Tax;
            parameters[5].Value = model.Id;

            return DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public int Delete(int deptId, int year, int month)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete F_TeamTax ");
            strSql.Append(" where DepartmentId=@DepartmentId and TaxYear=@TaxYear and TaxMonth=@TaxMonth ");
            SqlParameter[] parameters = {
					 new SqlParameter("@DepartmentId", SqlDbType.Int),
					 new SqlParameter("@TaxYear", SqlDbType.Int),
					 new SqlParameter("@TaxMonth", SqlDbType.Int)
                                         };
            parameters[0].Value = deptId;
            parameters[1].Value = year;
            parameters[2].Value = month;
            return DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }

     
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public ESP.Finance.Entity.TeamTaxInfo GetModel(int deptId, int year, int month)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"select  top 1 * from F_TeamTax ");
            strSql.Append(" where DepartmentId=@DepartmentId and TaxYear=@TaxYear and TaxMonth=@TaxMonth ");
            SqlParameter[] parameters = {
					 new SqlParameter("@DepartmentId", SqlDbType.Int),
					 new SqlParameter("@TaxYear", SqlDbType.Int),
					 new SqlParameter("@TaxMonth", SqlDbType.Int)
                                         };
            parameters[0].Value = deptId;
            parameters[1].Value = year;
            parameters[2].Value = month;
            return CBO.FillObject<TeamTaxInfo>(DbHelperSQL.Query(strSql.ToString(), parameters));

        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public IList<TeamTaxInfo> GetList(string term, List<SqlParameter> param)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"select * FROM F_TeamTax where 1=1");
            if (!string.IsNullOrEmpty(term))
            {
                strSql.Append(" and " + term);
            }
            strSql.Append(" order by taxYear desc,taxMonth desc");

            return CBO.FillCollection<TeamTaxInfo>(DbHelperSQL.Query(strSql.ToString(), param));
        }
        #endregion  成员方法
    }
}
