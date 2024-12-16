using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using ESP.Finance.Utility;
namespace ESP.Finance.DataAccess
{
    internal class GetCostTypeDataProvider : ESP.Finance.IDataAccess.ICostTypeViewDataProvider
    {
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public ESP.Finance.Entity.CostTypeViewInfo GetModel(int AreaID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT top 1    a.typeid as typeid,a.typename as typename,a.parentid as parentid,isnull(b.typename,'') as parenttypename,a.typelevel as typelevel FROM V_GetCostType as a left join V_GetCostType as b on a.parentid = b.typeid ");
            strSql.Append(" where a.typeid=@typeid ");
            SqlParameter[] parameters = {
					new SqlParameter("@typeid", SqlDbType.Int,4)};
            parameters[0].Value = AreaID;

            return CBO.FillObject<Entity.CostTypeViewInfo>(DbHelperSQL.Query(strSql.ToString(), parameters));


        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public IList<Entity.CostTypeViewInfo> GetList(string term, List<SqlParameter> param)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT     a.typeid as typeid,a.typename as typename,a.parentid as parentid,isnull(b.typename,'') as parenttypename,a.typelevel as typelevel ");
            strSql.Append(" FROM      V_GetCostType as a left join V_GetCostType as b on a.parentid = b.typeid ");
            if (!string.IsNullOrEmpty(term))
            {
                strSql.Append(" where " + term);
            }
            //if (param != null && param.Count > 0)
            //{
            //    SqlParameter[] ps = param.ToArray();
            //    return CBO.FillCollection<Entity.CostTypeViewInfo>(DbHelperSQL.ExecuteReader(strSql.ToString(), ps));
            //}
            return CBO.FillCollection<Entity.CostTypeViewInfo>(DbHelperSQL.Query(strSql.ToString(),param));
        }

    }
}
