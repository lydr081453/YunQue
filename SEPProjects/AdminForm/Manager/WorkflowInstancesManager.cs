using System;
using System.Data;
using System.Collections.Generic;
using System.Linq.Expressions;
using AdminForm.Model;
using AdminForm.Data;

namespace AdminForm.Manager
{
    public static class WorkflowInstancesManager
    {
        public static List<WorkflowInstancesInfo> GetList(
      Expression<Func<WorkflowInstancesInfo, bool>> predicate,
      params Expression<Func<WorkflowInstancesInfo, OrderBy>>[] orderby)
        {
            string sql = "SELECT * FROM wf_WorkflowInstances WHERE @@@WHERE@@@";
            if (orderby != null && orderby.Length != 0)
                sql += " ORDER BY @@@ORDERBY@@@";
            var cmd = CommandBuilder.BuildSelectCommand(sql, predicate, orderby);
            using (var reader = DatabaseHelper.ExecuteReader(cmd.CommandText, CommandType.Text, cmd.Parameters))
            {
                return CBO.FillCollection<WorkflowInstancesInfo>(reader);
            }
        }

        public static List<WorkflowInstancesInfo> GetList()
        {
            return GetList(null);
        }

        public static WorkflowInstancesInfo GetSingle(
                Expression<Func<WorkflowInstancesInfo, bool>> predicate)
        {
            string sql = "SELECT TOP (1) * FROM wf_WorkflowInstances WHERE @@@WHERE@@@";
            var cmd = CommandBuilder.BuildSelectCommand(sql, predicate);
            using (var reader = DatabaseHelper.ExecuteReader(cmd.CommandText, CommandType.Text, cmd.Parameters))
            {
                return CBO.FillObject<WorkflowInstancesInfo>(reader);
            }
        }

        public static List<WorkflowInstancesInfo> GetPage(int pageSize, int pageIndex,
                Expression<Func<WorkflowInstancesInfo, bool>> predicate,
                params Expression<Func<WorkflowInstancesInfo, OrderBy>>[] orderby)
        {
            if (orderby == null || orderby.Length == 0)
                throw new ArgumentNullException("orderby");
            string sql = @"
SELECT TOP (@PageSize) * FROM(
    SELECT *, ROW_NUMBER() OVER (ORDER BY @@@ORDERBY@@@) AS [__i_RowNumber] FROM wf_WorkflowInstances WHERE @@@WHERE@@@
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
                return CBO.FillCollection<WorkflowInstancesInfo>(reader);

            }
        }

        public static int GetCount(Expression<Func<WorkflowInstancesInfo, bool>> predicate)
        {
            string sql = "SELECT COUNT(*) FROM wf_WorkflowInstances WHERE @@@WHERE@@@";
            var cmd = CommandBuilder.BuildSelectCommand(sql, predicate);
            var obj = DatabaseHelper.ExecuteScalar(cmd.CommandText, CommandType.Text, cmd.Parameters);
            return obj == null ? 0 : (int)obj;
        }

        public static WorkflowInstancesInfo Get(string  iD)
        {
            return GetSingle(x => x.InstanceId == iD);
        }

        public static void Update(WorkflowInstancesInfo entity)
        {
            var keySelectors = new Expression<Func<WorkflowInstancesInfo, KeySelector>>[]
            {
                x => x.InstanceId.KeySelector()
            };
            var exclude = new Expression<Func<WorkflowInstancesInfo, KeySelector>>[]
            {
                x => x.InstanceId.KeySelector()
            };
            var cmd = CommandBuilder.BuildUpdateCommand<WorkflowInstancesInfo>("wf_WorkflowInstances", entity, keySelectors, exclude);
            DatabaseHelper.ExecuteNonQuery(cmd.CommandText, CommandType.Text, cmd.Parameters);
        }
    }
}
