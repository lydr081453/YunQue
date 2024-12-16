using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESP.Finance.BusinessLogic
{


    public static class ProjectExpenseTmpManager
    {
        private static ESP.Finance.IDataAccess.IProjectExpenseTmpDataProvider DataProvider { get { return ESP.Configuration.ProviderHelper<ESP.Finance.IDataAccess.IProjectExpenseTmpDataProvider>.Instance; } }
        private const string tablename = "ProjectExpenseTmpInfo";





        #region IProjectExpenseProvider 成员


        public static int Add(ESP.Finance.Entity.ProjectExpenseTmpInfo model)
        {
            if (model.ProjectID == null) return 0;
            if (!CheckerManager.CheckProjectOddAmount(model.ProjectID.Value, model.Expense ?? 0))
            {
                return 0;
            }
            //trans//return DataProvider.Add(model,true);
            return DataProvider.Add(model);
        }


        public static ESP.Finance.Utility.UpdateResult Update(ESP.Finance.Entity.ProjectExpenseTmpInfo model)
        {
            if (model.ProjectID == null) return 0;
            if (!CheckerManager.CheckProjectOddAmount(model.ProjectID.Value, model.Expense ?? 0))
            {
                return ESP.Finance.Utility.UpdateResult.AmountOverflow;
            }
            int res = 0;
            try
            {
                res = DataProvider.Update(model);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return ESP.Finance.Utility.UpdateResult.Failed;
            }
            if (res > 0)
            {
                return ESP.Finance.Utility.UpdateResult.Succeed;
            }
            else if (res == 0)
            {
                return ESP.Finance.Utility.UpdateResult.UnExecute;
            }
            return ESP.Finance.Utility.UpdateResult.Failed;
        }

        public static ESP.Finance.Utility.DeleteResult Delete(int prjExpenseId)
        {
            int res = 0;
            try
            {
                res = DataProvider.Delete(prjExpenseId);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return ESP.Finance.Utility.DeleteResult.Failed;
            }
            if (res > 0)
            {
                return ESP.Finance.Utility.DeleteResult.Succeed;
            }
            else if (res == 0)
            {
                return ESP.Finance.Utility.DeleteResult.UnExecute;
            }
            return ESP.Finance.Utility.DeleteResult.Failed;
        }




        public static decimal GetTotalExpense(int projectId)
        {
            //trans//return DataProvider.GetTotalExpense(projectId, true);
            return DataProvider.GetTotalExpense(projectId);
        }

        public static ESP.Finance.Entity.ProjectExpenseTmpInfo GetModel(int prjExpenseId)
        {
            return DataProvider.GetModel(prjExpenseId);
        }


        #region 获得数据列表
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public static IList<ESP.Finance.Entity.ProjectExpenseTmpInfo> GetAllList()
        {
            return GetList(null);
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public static IList<ESP.Finance.Entity.ProjectExpenseTmpInfo> GetList(string term)
        {
            return GetList(term, null);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public static IList<ESP.Finance.Entity.ProjectExpenseTmpInfo> GetList(string term, List<System.Data.SqlClient.SqlParameter> param)
        {
            return DataProvider.GetList(term, param);
        }
        #endregion 获得数据列表

        #endregion


    }
}
