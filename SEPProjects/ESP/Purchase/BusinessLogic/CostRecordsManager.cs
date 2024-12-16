using System;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESP.Purchase.DataAccess;
using ESP.Purchase.Entity;
using ESP.Finance.Entity;

namespace ESP.Purchase.BusinessLogic
{
    public static class CostRecordsManager
    {
        private static CostRecordsProvider dal = new CostRecordsProvider();

        public static int InsertProject(ESP.Finance.Entity.ProjectInfo projectModel, IList<ContractCostInfo> costlist,IList<ProjectExpenseInfo> expenselist, SqlTransaction trans)
        {
            return dal.InsertProject(projectModel, costlist,expenselist, trans);
        }

        public static int InsertSupporter(ESP.Finance.Entity.SupporterInfo supporterModel, IList<SupporterCostInfo> costlist, IList<SupporterExpenseInfo> expenselist, SqlTransaction trans)
        {
            return dal.InsertSupporter(supporterModel, costlist, expenselist, trans);
        }
        /// <summary>
        /// PR单执行过程中对成本的占用
        /// </summary>
        /// <param name="recordModel">cost record model</param>
        /// <param name="trans">sqlclient transaction</param>
        /// <returns>大于等于1操作成功</returns>
        public static int InsertPR(ESP.Purchase.Entity.CostRecordsInfo recordModel, System.Data.SqlClient.SqlTransaction trans)
        {
            return dal.InsertPR(recordModel,trans);
        }

        /// <summary>
        /// 付款申请、traffic fee执行过程中对成本的占用
        /// </summary>
        /// <param name="recordModel">cost record model</param>
        /// <param name="trans">sqlclient transaction</param>
        /// <returns>大于等于1操作成功</returns>
        public static int InsertPN(ESP.Purchase.Entity.CostRecordsInfo recordModel, System.Data.SqlClient.SqlTransaction trans)
        {
            return dal.InsertPN(recordModel, trans);
        }

        /// <summary>
        /// 报销单、机票单执行过程中对成本的占用
        /// </summary>
        /// <param name="recordModel">cost record model</param>
        /// <param name="trans">sqlclient transaction</param>
        /// <returns>大于等于1操作成功</returns>
        public static int InsertOOP(ESP.Purchase.Entity.CostRecordsInfo recordModel, System.Data.SqlClient.SqlTransaction trans)
        {
            return dal.InsertOOP(recordModel, trans);
        }
    }
}
