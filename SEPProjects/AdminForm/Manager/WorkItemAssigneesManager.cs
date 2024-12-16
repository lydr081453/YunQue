using System;
using System.Data;
using System.Collections.Generic;
using System.Linq.Expressions;
using AdminForm.Model;
using AdminForm.Data;
using System.Data.SqlClient;

namespace AdminForm.Manager
{
    public static class WorkItemAssigneesManager
    {
        public static List<WorkItemAssigneesInfo> GetList(
    Expression<Func<WorkItemAssigneesInfo, bool>> predicate,
    params Expression<Func<WorkItemAssigneesInfo, OrderBy>>[] orderby)
        {
            string sql = "SELECT * FROM wf_WorkItemAssignees WHERE @@@WHERE@@@";
            if (orderby != null && orderby.Length != 0)
                sql += " ORDER BY @@@ORDERBY@@@";
            var cmd = CommandBuilder.BuildSelectCommand(sql, predicate, orderby);
            using (var reader = DatabaseHelper.ExecuteReader(cmd.CommandText, CommandType.Text, cmd.Parameters))
            {
                return CBO.FillCollection<WorkItemAssigneesInfo>(reader);
            }
        }

        public static List<WorkItemAssigneesInfo> GetList()
        {
            return GetList(null);
        }

        public static WorkItemAssigneesInfo GetSingle(
                Expression<Func<WorkItemAssigneesInfo, bool>> predicate)
        {
            string sql = "SELECT TOP (1) * FROM wf_WorkItemAssignees WHERE @@@WHERE@@@";
            var cmd = CommandBuilder.BuildSelectCommand(sql, predicate);
            using (var reader = DatabaseHelper.ExecuteReader(cmd.CommandText, CommandType.Text, cmd.Parameters))
            {
                return CBO.FillObject<WorkItemAssigneesInfo>(reader);
            }
        }

        public static List<WorkItemAssigneesInfo> GetPage(int pageSize, int pageIndex,
                Expression<Func<WorkItemAssigneesInfo, bool>> predicate,
                params Expression<Func<WorkItemAssigneesInfo, OrderBy>>[] orderby)
        {
            if (orderby == null || orderby.Length == 0)
                throw new ArgumentNullException("orderby");
            string sql = @"
SELECT TOP (@PageSize) * FROM(
    SELECT *, ROW_NUMBER() OVER (ORDER BY @@@ORDERBY@@@) AS [__i_RowNumber] FROM wf_WorkItemAssignees WHERE @@@WHERE@@@
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
                return CBO.FillCollection<WorkItemAssigneesInfo>(reader);

            }
        }

        public static int GetCount(Expression<Func<WorkItemAssigneesInfo, bool>> predicate)
        {
            string sql = "SELECT COUNT(*) FROM wf_WorkItemAssignees WHERE @@@WHERE@@@";
            var cmd = CommandBuilder.BuildSelectCommand(sql, predicate);
            var obj = DatabaseHelper.ExecuteScalar(cmd.CommandText, CommandType.Text, cmd.Parameters);
            return obj == null ? 0 : (int)obj;
        }

        public static WorkItemAssigneesInfo Get(int iD)
        {
            return GetSingle(x => x.WorkItemAssigneeId == iD);
        }

        public static int UpdateFirstFianceHandOver(List<WorkItemsInfo>wiList,int dimissionUserId, int receiverUserId)
        {
            string strwi = string.Empty;
            foreach (var wi in wiList)
            {
                strwi += wi.WorkItemId + ",";
            }
            strwi.TrimEnd(',');

            string strSql = "update wf_WorkItemAssignees set assigneeid=@ReceiverUserId where assigneeid =@OldUserId and workitemid in("+strwi+")";
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@ReceiverUserId", receiverUserId));
            parameters.Add(new SqlParameter("@OldUserId", dimissionUserId));

            return DatabaseHelper.ExecuteNonQuery(strSql, CommandType.Text, parameters.ToArray());
        }

    }
}
