using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using ESP.Finance.Entity;
using ESP.Finance.Utility;
using System.Collections.Generic;

namespace ESP.Finance.DataAccess
{
    /// <summary>
    /// 数据访问类F_BranchDept
    /// </summary>
    internal class BranchDeptProvider : ESP.Finance.IDataAccess.IBranchDeptProvider
    {
        public ESP.Finance.Entity.BranchDeptInfo GetModel(int branchid, int deptid)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 * from F_BranchDept ");
            strSql.Append(" where BranchId=@BranchId and DeptId =@DeptId ");
            SqlParameter[] parameters = {
					new SqlParameter("@BranchId", SqlDbType.Int,4),
                    new SqlParameter("@DeptId", SqlDbType.Int,4)};
            parameters[0].Value = branchid;
            parameters[1].Value = deptid;

            return CBO.FillObject<BranchDeptInfo>(DbHelperSQL.Query(strSql.ToString(), parameters));
        }

        public IList<ESP.Finance.Entity.BranchDeptInfo> GetList(int userid)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"select * FROM F_BranchDept where  FianceFirstAuditorID=@FianceFirstAuditorID");
            SqlParameter[] parameters = {
					new SqlParameter("@FianceFirstAuditorID", SqlDbType.Int,4)};
            parameters[0].Value = userid;

            return CBO.FillCollection<BranchDeptInfo>(DbHelperSQL.Query(strSql.ToString(), parameters));
        }
    }
}
