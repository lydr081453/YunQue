using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESP.Finance.Utility;

namespace ESP.Finance.DataAccess
{
    internal class PrjUserRelationDataProvider : ESP.Finance.IDataAccess.IPrjUserRelationProvider
    {
        public ESP.Finance.Entity.PrjUserRelationInfo GetModel(int PID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 PID,DeptID,UserID,BranchID,BranchCode,Usable,GM from F_PrjUserRelation ");
            strSql.Append(" where PID=@PID and GM=@GM ");
            SqlParameter[] parameters = {
					new SqlParameter("@PID", SqlDbType.Int,4)};
            parameters[0].Value = PID;
            return CBO.FillObject<ESP.Finance.Entity.PrjUserRelationInfo>(DbHelperSQL.Query(strSql.ToString(), parameters));
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public IList<ESP.Finance.Entity.PrjUserRelationInfo> GetList(string strWhere,List<System.Data.SqlClient.SqlParameter> paramlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select PID,DeptID,UserID,BranchID,BranchCode,Usable,GM ");
            strSql.Append(" FROM F_PrjUserRelation ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return CBO.FillCollection<ESP.Finance.Entity.PrjUserRelationInfo>(DbHelperSQL.Query(strSql.ToString(), paramlist));
        }
    }
}
