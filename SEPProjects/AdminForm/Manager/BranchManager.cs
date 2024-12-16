using System;
using System.Data;
using System.Collections.Generic;
using System.Linq.Expressions;
using AdminForm.Model;
using AdminForm.Data;

namespace AdminForm.Manager
{
    public static class BranchManager
    {
        public static List<BranchInfo> GetList(
                Expression<Func<BranchInfo, bool>> predicate,
                params Expression<Func<BranchInfo, OrderBy>>[] orderby)
        {
            string sql = "SELECT * FROM f_branch WHERE @@@WHERE@@@";
            if (orderby != null && orderby.Length != 0)
                sql += " ORDER BY @@@ORDERBY@@@";
            var cmd = CommandBuilder.BuildSelectCommand(sql, predicate, orderby);
            using (var reader = DatabaseHelper.ExecuteReader(cmd.CommandText, CommandType.Text, cmd.Parameters))
            {
                return CBO.FillCollection<BranchInfo>(reader);
            }
        }

        public static List<BranchInfo> GetList()
        {
            return GetList(null);
        }

        public static BranchInfo GetSingle(
                Expression<Func<BranchInfo, bool>> predicate)
        {
            string sql = "SELECT TOP (1) * FROM f_branch WHERE @@@WHERE@@@";
            var cmd = CommandBuilder.BuildSelectCommand(sql, predicate);
            using (var reader = DatabaseHelper.ExecuteReader(cmd.CommandText, CommandType.Text, cmd.Parameters))
            {
                return CBO.FillObject<BranchInfo>(reader);
            }
        }

        public static List<BranchInfo> GetPage(int pageSize, int pageIndex,
                Expression<Func<BranchInfo, bool>> predicate,
                params Expression<Func<BranchInfo, OrderBy>>[] orderby)
        {
            if (orderby == null || orderby.Length == 0)
                throw new ArgumentNullException("orderby");
            string sql = @"
SELECT TOP (@PageSize) * FROM(
    SELECT *, ROW_NUMBER() OVER (ORDER BY @@@ORDERBY@@@) AS [__i_RowNumber] FROM f_branch WHERE @@@WHERE@@@
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
                return CBO.FillCollection<BranchInfo>(reader);

            }
        }

        public static int GetCount(Expression<Func<BranchInfo, bool>> predicate)
        {
            string sql = "SELECT COUNT(*) FROM f_branch WHERE @@@WHERE@@@";
            var cmd = CommandBuilder.BuildSelectCommand(sql, predicate);
            var obj = DatabaseHelper.ExecuteScalar(cmd.CommandText, CommandType.Text, cmd.Parameters);
            return obj == null ? 0 : (int)obj;
        }

        public static BranchInfo Get(int iD)
        {
            return GetSingle(x => x.BranchID == iD);
        }

        public static void Update(BranchInfo entity)
        {
            var keySelectors = new Expression<Func<BranchInfo, KeySelector>>[]
            {
                x => x.BranchID.KeySelector()
            };
            var exclude = new Expression<Func<BranchInfo, KeySelector>>[]
            {
                x => x.BranchID.KeySelector()
            };
            var cmd = CommandBuilder.BuildUpdateCommand<BranchInfo>("f_branch", entity, keySelectors, exclude);
            DatabaseHelper.ExecuteNonQuery(cmd.CommandText, CommandType.Text, cmd.Parameters);
        }

    }
}
