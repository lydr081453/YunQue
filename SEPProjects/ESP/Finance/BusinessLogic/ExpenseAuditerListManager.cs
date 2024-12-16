using System;
using System.Data;
using System.Collections.Generic;
using ESP.Finance.Entity;
using ESP.Finance.Utility;
using System.Data.SqlClient;
namespace ESP.Finance.BusinessLogic
{
	/// <summary>
	/// 业务逻辑类ExpenseBLL 的摘要说明。
	/// </summary>


    public static class ExpenseAuditerListManager
	{

        private static ESP.Finance.IDataAccess.IExpenseAuditerListDataProvider DataProvider { get { return ESP.Configuration.ProviderHelper<ESP.Finance.IDataAccess.IExpenseAuditerListDataProvider>.Instance; } }

        
		#region  成员方法


		/// <summary>
		/// 增加一条数据
		/// </summary>
        public static int Add(ESP.Finance.Entity.ExpenseAuditerListInfo model)
		{
			return DataProvider.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
        public static UpdateResult Update(ESP.Finance.Entity.ExpenseAuditerListInfo model)
		{
            int res = 0;
            try
            {
                res = DataProvider.Update(model);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return UpdateResult.Failed;
            }
            if (res > 0)
            {
                return UpdateResult.Succeed;
            }
            else if (res == 0)
            {
                return UpdateResult.UnExecute;
            }
            return UpdateResult.Failed;
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public static DeleteResult Delete(int id)
		{

            int res = 0;
            try
            {
                res = DataProvider.Delete(id);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return DeleteResult.Failed;
            }
            if (res > 0)
            {
                return DeleteResult.Succeed;
            }
            else if (res == 0)
            {
                return DeleteResult.UnExecute;
            }
            return DeleteResult.Failed;
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
        public static ESP.Finance.Entity.ExpenseAuditerListInfo GetModel(int id)
		{
			
			return DataProvider.GetModel(id);
		}

        #region 获得数据列表
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public static List<ExpenseAuditerListInfo> GetAllList()
        {
            return DataProvider.GetList("");
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public static List<ExpenseAuditerListInfo> GetList(string term)
        {
            return DataProvider.GetList(term);
        }

        public static int DeleteByReturnID(int id)
        {
            return DataProvider.DeleteByReturnID(id);
        }
         
        /// <summary>
        /// 获得以保存的审核人员列表
        /// </summary>
        /// <param name="id"></param>
        /// <param name="types"></param>
        /// <returns></returns>
        public static List<ExpenseAuditerListInfo> GetUnDelList(int id, string types)
        {
            string term = string.Empty;

            if (!string.IsNullOrEmpty(types))
            {
                if (types.Trim().Length > 0)
                {
                    //term = " and AuditType in (" + types + ")";
                    //DataProvider.DeleteByExpenseAccountID(id);
                }
            }

            term = " and ReturnID = " + id;
            return GetList(term);
        }

        /// <summary>
        /// 增加审批人
        /// </summary>
        /// <param name="auditList"></param>
        /// <returns></returns>
        public static bool AddAuditers(List<ESP.Finance.Entity.ExpenseAuditerListInfo> auditList,int expenseID)
        {
            using (SqlConnection conn = new SqlConnection(DataAccess.DbHelperSQL.connectionString))
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();
                try
                {

                    DataProvider.DeleteByReturnID(expenseID, trans);
                    ESP.Finance.BusinessLogic.ExpenseAuditDetailManager.DeleteByReturnID(expenseID, trans);
                    int count = 0;
                    foreach (ESP.Finance.Entity.ExpenseAuditerListInfo model in auditList)
                    {
                        int res = DataProvider.Add(model, trans);
                        if (res > 0)
                        {
                            count++;
                        }
                    }
                    if (count > 0)
                    {
                        trans.Commit();
                        return true;
                    }
                    
                    
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    return false;
                }
            }
            return false;
        }

        #endregion 获得数据列表

		#endregion  成员方法
	}
}

