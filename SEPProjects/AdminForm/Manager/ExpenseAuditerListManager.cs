using System;
using System.Data;
using System.Collections.Generic;
using System.Linq.Expressions;
using AdminForm.Model;
using AdminForm.Data;
using System.Data.SqlClient;

namespace AdminForm.Manager
{
    public static class ExpenseAuditerListManager
    {
        public static List<ExpenseAuditerListInfo> GetList(
               Expression<Func<ExpenseAuditerListInfo, bool>> predicate,
               params Expression<Func<ExpenseAuditerListInfo, OrderBy>>[] orderby)
        {
            string sql = "SELECT * FROM f_ExpenseAuditerList WHERE @@@WHERE@@@";
            if (orderby != null && orderby.Length != 0)
                sql += " ORDER BY @@@ORDERBY@@@";
            var cmd = CommandBuilder.BuildSelectCommand(sql, predicate, orderby);
            using (var reader = DatabaseHelper.ExecuteReader(cmd.CommandText, CommandType.Text, cmd.Parameters))
            {
                return CBO.FillCollection<ExpenseAuditerListInfo>(reader);
            }
        }

        public static List<ExpenseAuditerListInfo> GetList()
        {
            return GetList(null);
        }

        public static ExpenseAuditerListInfo GetSingle(
                Expression<Func<ExpenseAuditerListInfo, bool>> predicate)
        {
            string sql = "SELECT TOP (1) * FROM f_ExpenseAuditerList WHERE @@@WHERE@@@";
            var cmd = CommandBuilder.BuildSelectCommand(sql, predicate);
            using (var reader = DatabaseHelper.ExecuteReader(cmd.CommandText, CommandType.Text, cmd.Parameters))
            {
                return CBO.FillObject<ExpenseAuditerListInfo>(reader);
            }
        }

        public static List<ExpenseAuditerListInfo> GetPage(int pageSize, int pageIndex,
                Expression<Func<ExpenseAuditerListInfo, bool>> predicate,
                params Expression<Func<ExpenseAuditerListInfo, OrderBy>>[] orderby)
        {
            if (orderby == null || orderby.Length == 0)
                throw new ArgumentNullException("orderby");
            string sql = @"
SELECT TOP (@PageSize) * FROM(
    SELECT *, ROW_NUMBER() OVER (ORDER BY @@@ORDERBY@@@) AS [__i_RowNumber] FROM f_ExpenseAuditerList WHERE @@@WHERE@@@
) t
WHERE t.[__i_RowNumber] > @PageStart
";
            int pageStart = pageSize * pageIndex;
            var pageSzieParameter = DatabaseHelper.CreateInputParameter("@PageSize", pageSize);
            var pageStartParameter = DatabaseHelper.CreateInputParameter("@PageStart", pageStart);
            var cmd = CommandBuilder.BuildSelectCommand(sql, predicate, orderby);
            var parameters = new List<IDataParameter>();
            parameters.AddRange(cmd.Parameters);
            parameters.Add(pageSzieParameter);
            parameters.Add(pageStartParameter);
            using (var reader = DatabaseHelper.ExecuteReader(cmd.CommandText, CommandType.Text, parameters.ToArray()))
            {
                return CBO.FillCollection<ExpenseAuditerListInfo>(reader);

            }
        }

        public static int GetCount(Expression<Func<ExpenseAuditerListInfo, bool>> predicate)
        {
            string sql = "SELECT COUNT(*) FROM f_ExpenseAuditerList WHERE @@@WHERE@@@";
            var cmd = CommandBuilder.BuildSelectCommand(sql, predicate);
            var obj = DatabaseHelper.ExecuteScalar(cmd.CommandText, CommandType.Text, cmd.Parameters);
            return obj == null ? 0 : (int)obj;
        }

        public static ExpenseAuditerListInfo Get(int iD)
        {
            return GetSingle(x => x.ID == iD);
        }

        public static int UpdateFirstFinanceHandOver(ReturnInfo returnModel ,UserInfo dimissionUser, UserInfo ReceiverUser)
        {
            string strSql = "update f_ExpenseAuditerList set auditer=@auditer,auditerName=@auditerName where auditer =@Oldauditer";
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@auditer", ReceiverUser.UserID));
            parameters.Add(new SqlParameter("@auditerName", ReceiverUser.LastNameCN + ReceiverUser.FirstNameCN));
            parameters.Add(new SqlParameter("@Oldauditer", dimissionUser.UserID));

            return DatabaseHelper.ExecuteNonQuery(strSql, CommandType.Text, parameters.ToArray());
        }
    }
}
