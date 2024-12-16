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
    internal class BranchProjectProvider : ESP.Finance.IDataAccess.IBranchProjectProvider
    {
        public ESP.Finance.Entity.BranchProjectInfo GetModel(int branchid, int deptid)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 * from F_BranchProject ");
            strSql.Append(" where BranchId=@BranchId and DeptId =@DeptId ");
            SqlParameter[] parameters = {
					new SqlParameter("@BranchId", SqlDbType.Int,4),
                    new SqlParameter("@DeptId", SqlDbType.Int,4)};
            parameters[0].Value = branchid;
            parameters[1].Value = deptid;

            return CBO.FillObject<BranchProjectInfo>(DbHelperSQL.Query(strSql.ToString(), parameters));
        }

        public IList<BranchProjectInfo> GetList(int auditorId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"select * FROM F_BranchProject where auditorId=@auditorId");
            SqlParameter[] parameters = {
					new SqlParameter("@auditorId", SqlDbType.Int,4)
                                        };
            parameters[0].Value = auditorId;

            return CBO.FillCollection<BranchProjectInfo>(DbHelperSQL.Query(strSql.ToString(), parameters));
        }
    }
}
