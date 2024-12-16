using System;
using System.Data;
using System.Collections.Generic;
using System.Linq.Expressions;
using AdminForm.Model;
using AdminForm.Data;

namespace AdminForm.Manager
{
    public static class ReturnAuditHistManager
    {
        public static List<ReturnAuditHistInfo> GetList(
           Expression<Func<ReturnAuditHistInfo, bool>> predicate,
           params Expression<Func<ReturnAuditHistInfo, OrderBy>>[] orderby)
        {
            string sql = "SELECT * FROM f_returnaudithist WHERE @@@WHERE@@@";
            if (orderby != null && orderby.Length != 0)
                sql += " ORDER BY @@@ORDERBY@@@";
            var cmd = CommandBuilder.BuildSelectCommand(sql, predicate, orderby);
            using (var reader = DatabaseHelper.ExecuteReader(cmd.CommandText, CommandType.Text, cmd.Parameters))
            {
                return CBO.FillCollection<ReturnAuditHistInfo>(reader);
            }
        }

        public static List<ReturnAuditHistInfo> GetList()
        {
            return GetList(null);
        }

        public static ReturnAuditHistInfo GetSingle(
                Expression<Func<ReturnAuditHistInfo, bool>> predicate)
        {
            string sql = "SELECT TOP (1) * FROM f_returnaudithist WHERE @@@WHERE@@@";
            var cmd = CommandBuilder.BuildSelectCommand(sql, predicate);
            using (var reader = DatabaseHelper.ExecuteReader(cmd.CommandText, CommandType.Text, cmd.Parameters))
            {
                return CBO.FillObject<ReturnAuditHistInfo>(reader);
            }
        }

        public static List<ReturnAuditHistInfo> GetPage(int pageSize, int pageIndex,
                Expression<Func<ReturnAuditHistInfo, bool>> predicate,
                params Expression<Func<ReturnAuditHistInfo, OrderBy>>[] orderby)
        {
            if (orderby == null || orderby.Length == 0)
                throw new ArgumentNullException("orderby");
            string sql = @"
SELECT TOP (@PageSize) * FROM(
    SELECT *, ROW_NUMBER() OVER (ORDER BY @@@ORDERBY@@@) AS [__i_RowNumber] FROM f_returnaudithist WHERE @@@WHERE@@@
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
                return CBO.FillCollection<ReturnAuditHistInfo>(reader);

            }
        }

        public static int GetCount(Expression<Func<ReturnAuditHistInfo, bool>> predicate)
        {
            string sql = "SELECT COUNT(*) FROM f_returnaudithist WHERE @@@WHERE@@@";
            var cmd = CommandBuilder.BuildSelectCommand(sql, predicate);
            var obj = DatabaseHelper.ExecuteScalar(cmd.CommandText, CommandType.Text, cmd.Parameters);
            return obj == null ? 0 : (int)obj;
        }

        public static ReturnAuditHistInfo Get(int iD)
        {
            return GetSingle(x => x.ReturnAuditID == iD);
        }


    }
}
