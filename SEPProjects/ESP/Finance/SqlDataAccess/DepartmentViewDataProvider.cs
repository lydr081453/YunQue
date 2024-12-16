using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using ESP.Finance.DataAccess;
using ESP.Finance.Entity;
using ESP.Finance.Utility;

namespace ESP.Finance.DataAccess
{
    internal class DepartmentViewDataProvider:IDataAccess.IDepartmentViewProvider
    {

        public string GetBranchnameByDeptId(int groupId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 level1 from v_department ");
            strSql.Append(" where level3Id=@level3Id ");
            SqlParameter[] parameters = {
					new SqlParameter("@level3Id", SqlDbType.Int,4)};
            parameters[0].Value = groupId;

            string branchname = string.Empty;
            try
            {
                DbHelperSQL.Query(strSql.ToString(), parameters).Tables[0].Rows[0]["level1"].ToString();
            }
            catch
            {
                branchname = "";
            }
            return branchname;

        }


        public DepartmentViewInfo GetModel(int level3Id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select top 1 * from V_Department ");
            strSql.Append(" where level3Id=@level3Id ");
            SqlParameter[] parameters = {
					new SqlParameter("@level3Id", SqlDbType.Int,4)};
            parameters[0].Value = level3Id;

            return CBO.FillObject<DepartmentViewInfo>(DbHelperSQL.Query(strSql.ToString(), parameters));
        }

        public DataTable GetList()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from (select level3id,(level1+'-'+level2+'-'+level3) as deptname from V_Department where (level3 not like '%作废%' and level2 not like '%作废%' and level1 not like '%作废%')) a order by deptname");
                      return DbHelperSQL.Query(strSql.ToString()).Tables[0];
        }

        public List<DepartmentViewInfo> GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from V_Department where (level3 not like '%作废%' and level2 not like '%作废%' and level1 not like '%作废%') ");
            strSql.Append(strWhere);

            return CBO.FillCollection<DepartmentViewInfo>(DbHelperSQL.Query(strSql.ToString()));
        }

    }
}
