using System;
using System.Data;
using System.Collections.Generic;
using System.Linq.Expressions;
using AdminForm.Model;
using AdminForm.Data;

namespace AdminForm.Manager
{
    public static class BranchDeptManager
    {
        public static List<BranchDeptInfo> GetList(
              Expression<Func<BranchDeptInfo, bool>> predicate,
              params Expression<Func<BranchDeptInfo, OrderBy>>[] orderby)
        {
            string sql = "SELECT * FROM f_branchDept WHERE @@@WHERE@@@";
            if (orderby != null && orderby.Length != 0)
                sql += " ORDER BY @@@ORDERBY@@@";
            var cmd = CommandBuilder.BuildSelectCommand(sql, predicate, orderby);
            using (var reader = DatabaseHelper.ExecuteReader(cmd.CommandText, CommandType.Text, cmd.Parameters))
            {
                return CBO.FillCollection<BranchDeptInfo>(reader);
            }
        }

        public static List<BranchDeptInfo> GetList()
        {
            return GetList(null);
        }

        public static BranchDeptInfo GetSingle(
                Expression<Func<BranchDeptInfo, bool>> predicate)
        {
            string sql = "SELECT TOP (1) * FROM f_branchDept WHERE @@@WHERE@@@";
            var cmd = CommandBuilder.BuildSelectCommand(sql, predicate);
            using (var reader = DatabaseHelper.ExecuteReader(cmd.CommandText, CommandType.Text, cmd.Parameters))
            {
                return CBO.FillObject<BranchDeptInfo>(reader);
            }
        }

        public static List<BranchDeptInfo> GetPage(int pageSize, int pageIndex,
                Expression<Func<BranchDeptInfo, bool>> predicate,
                params Expression<Func<BranchDeptInfo, OrderBy>>[] orderby)
        {
            if (orderby == null || orderby.Length == 0)
                throw new ArgumentNullException("orderby");
            string sql = @"
SELECT TOP (@PageSize) * FROM(
    SELECT *, ROW_NUMBER() OVER (ORDER BY @@@ORDERBY@@@) AS [__i_RowNumber] FROM f_branchDept WHERE @@@WHERE@@@
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
                return CBO.FillCollection<BranchDeptInfo>(reader);

            }
        }

        public static int GetCount(Expression<Func<BranchDeptInfo, bool>> predicate)
        {
            string sql = "SELECT COUNT(*) FROM f_branchDept WHERE @@@WHERE@@@";
            var cmd = CommandBuilder.BuildSelectCommand(sql, predicate);
            var obj = DatabaseHelper.ExecuteScalar(cmd.CommandText, CommandType.Text, cmd.Parameters);
            return obj == null ? 0 : (int)obj;
        }

        public static BranchDeptInfo Get(int iD)
        {
            return GetSingle(x => x.Id == iD);
        }

    }
}
