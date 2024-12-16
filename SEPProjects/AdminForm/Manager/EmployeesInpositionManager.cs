using System;
using System.Data;
using System.Collections.Generic;
using System.Linq.Expressions;
using AdminForm.Model;
using AdminForm.Data;

namespace AdminForm.Manager
{
    public static class EmployeesInpositionManager
    {
        public static List<EmployeesInPositionsInfo> GetList(
              Expression<Func<EmployeesInPositionsInfo, bool>> predicate,
              params Expression<Func<EmployeesInPositionsInfo, OrderBy>>[] orderby)
        {
            string sql = "SELECT * FROM sep_EmployeesInPositions WHERE @@@WHERE@@@";
            if (orderby != null && orderby.Length != 0)
                sql += " ORDER BY @@@ORDERBY@@@";
            var cmd = CommandBuilder.BuildSelectCommand(sql, predicate, orderby);
            using (var reader = DatabaseHelper.ExecuteReader(cmd.CommandText, CommandType.Text, cmd.Parameters))
            {
                return CBO.FillCollection<EmployeesInPositionsInfo>(reader);
            }
        }

        public static List<EmployeesInPositionsInfo> GetList()
        {
            return GetList(null);
        }

        public static EmployeesInPositionsInfo GetSingle(
                Expression<Func<EmployeesInPositionsInfo, bool>> predicate)
        {
            string sql = "SELECT TOP (1) * FROM sep_EmployeesInPositions WHERE @@@WHERE@@@";
            var cmd = CommandBuilder.BuildSelectCommand(sql, predicate);
            using (var reader = DatabaseHelper.ExecuteReader(cmd.CommandText, CommandType.Text, cmd.Parameters))
            {
                return CBO.FillObject<EmployeesInPositionsInfo>(reader);
            }
        }

        public static List<EmployeesInPositionsInfo> GetPage(int pageSize, int pageIndex,
                Expression<Func<EmployeesInPositionsInfo, bool>> predicate,
                params Expression<Func<EmployeesInPositionsInfo, OrderBy>>[] orderby)
        {
            if (orderby == null || orderby.Length == 0)
                throw new ArgumentNullException("orderby");
            string sql = @"
SELECT TOP (@PageSize) * FROM(
    SELECT *, ROW_NUMBER() OVER (ORDER BY @@@ORDERBY@@@) AS [__i_RowNumber] FROM sep_EmployeesInPositions WHERE @@@WHERE@@@
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
                return CBO.FillCollection<EmployeesInPositionsInfo>(reader);

            }
        }

        public static int GetCount(Expression<Func<EmployeesInPositionsInfo, bool>> predicate)
        {
            string sql = "SELECT COUNT(*) FROM sep_EmployeesInPositions WHERE @@@WHERE@@@";
            var cmd = CommandBuilder.BuildSelectCommand(sql, predicate);
            var obj = DatabaseHelper.ExecuteScalar(cmd.CommandText, CommandType.Text, cmd.Parameters);
            return obj == null ? 0 : (int)obj;
        }

        public static EmployeesInPositionsInfo Get(int iD)
        {
            return GetSingle(x => x.UserID == iD);
        }
   
    }
}
