using System;
using System.Data;
using System.Collections.Generic;
using System.Linq.Expressions;
using AdminForm.Model;
using AdminForm.Data;

namespace AdminForm.Manager
{
    public static class OrderManager
    {
        public static List<OrderInfo> GetList(
                Expression<Func<OrderInfo, bool>> predicate,
                params Expression<Func<OrderInfo, OrderBy>>[] orderby)
        {
            string sql = "SELECT * FROM t_orderinfo WHERE @@@WHERE@@@";
            if (orderby != null && orderby.Length != 0)
                sql += " ORDER BY @@@ORDERBY@@@";
            var cmd = CommandBuilder.BuildSelectCommand(sql, predicate, orderby);
            using (var reader = DatabaseHelper.ExecuteReader(cmd.CommandText, CommandType.Text, cmd.Parameters))
            {
                return CBO.FillCollection<OrderInfo>(reader);
            }
        }

        public static List<OrderInfo> GetList()
        {
            return GetList(null);
        }

        public static OrderInfo GetSingle(
                Expression<Func<OrderInfo, bool>> predicate)
        {
            string sql = "SELECT TOP (1) * FROM t_orderinfo WHERE @@@WHERE@@@";
            var cmd = CommandBuilder.BuildSelectCommand(sql, predicate);
            using (var reader = DatabaseHelper.ExecuteReader(cmd.CommandText, CommandType.Text, cmd.Parameters))
            {
                return CBO.FillObject<OrderInfo>(reader);
            }
        }

        public static List<OrderInfo> GetPage(int pageSize, int pageIndex,
                Expression<Func<OrderInfo, bool>> predicate,
                params Expression<Func<OrderInfo, OrderBy>>[] orderby)
        {
            if (orderby == null || orderby.Length == 0)
                throw new ArgumentNullException("orderby");
            string sql = @"
SELECT TOP (@PageSize) * FROM(
    SELECT *, ROW_NUMBER() OVER (ORDER BY @@@ORDERBY@@@) AS [__i_RowNumber] FROM t_orderinfo WHERE @@@WHERE@@@
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
                return CBO.FillCollection<OrderInfo>(reader);

            }
        }

        public static int GetCount(Expression<Func<OrderInfo, bool>> predicate)
        {
            string sql = "SELECT COUNT(*) FROM t_orderinfo WHERE @@@WHERE@@@";
            var cmd = CommandBuilder.BuildSelectCommand(sql, predicate);
            var obj = DatabaseHelper.ExecuteScalar(cmd.CommandText, CommandType.Text, cmd.Parameters);
            return obj == null ? 0 : (int)obj;
        }

        public static OrderInfo Get(int iD)
        {
            return GetSingle(x => x.id == iD);
        }

    }
}
