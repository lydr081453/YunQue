using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using ESP.Finance.Utility;

namespace ESP.Finance.DataAccess
{
    internal class AdminExpenseDetailProvider : ESP.Finance.IDataAccess.IAdminExpenseDetailProvider
    {
        public int Update(ESP.Finance.Entity.AdminExpenseDetailInfo model)
        {
            return Update(model, null);
        }

        public int Update(ESP.Finance.Entity.AdminExpenseDetailInfo model,System.Data.SqlClient.SqlTransaction trans)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update F_AdminExpenseDetail set ");
            strSql.Append("AreaId=@AreaId,");
            strSql.Append("ExpenseTypeId=@ExpenseTypeId,");
            strSql.Append("ExpenseTotalCount=@ExpenseTotalCount,");
            strSql.Append("ExpenseUsedCount=@ExpenseUsedCount");
            strSql.Append(" where Id=@Id");
            SqlParameter[] parameters = {
					new SqlParameter("@Id", SqlDbType.Int,4),
					new SqlParameter("@AreaId", SqlDbType.Int,4),
					new SqlParameter("@ExpenseTypeId", SqlDbType.Int,4),
					new SqlParameter("@ExpenseTotalCount", SqlDbType.Int,4),
					new SqlParameter("@ExpenseUsedCount", SqlDbType.Int,4)};
            parameters[0].Value = model.Id;
            parameters[1].Value = model.AreaId;
            parameters[2].Value = model.ExpenseTypeId;
            parameters[3].Value = model.ExpenseTotalCount;
            parameters[4].Value = model.ExpenseUsedCount;

            return DbHelperSQL.ExecuteSql(strSql.ToString(),trans, parameters);
        }

        public ESP.Finance.Entity.AdminExpenseDetailInfo GetModel(int AreaId, int ExpenseTypeId)
        {
            return GetModel(AreaId, ExpenseTypeId, null);
        }

        public ESP.Finance.Entity.AdminExpenseDetailInfo GetModel(int AreaId, int ExpenseTypeId,System.Data.SqlClient.SqlTransaction trans)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from F_AdminExpenseDetail ");
            strSql.Append(" where AreaId=@AreaId and ExpenseTypeId=@ExpenseTypeId");
            SqlParameter[] parameters = {
					new SqlParameter("@AreaId", SqlDbType.Int,4),
                    new SqlParameter("@ExpenseTypeId",SqlDbType.Int,4)                    
                                        };
            parameters[0].Value = AreaId;
            parameters[1].Value = ExpenseTypeId;
            return CBO.FillObject<ESP.Finance.Entity.AdminExpenseDetailInfo>(DbHelperSQL.Query(strSql.ToString(),trans, parameters));
        }

    }
}
