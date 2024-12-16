using System;
using System.Data;
using System.Collections.Generic;
using ESP.Finance.Entity;
using ESP.Finance.Utility;
namespace ESP.Finance.BusinessLogic
{
	/// <summary>
	/// 业务逻辑类ExpenseBLL 的摘要说明。
	/// </summary>


    public static class ExpenseAccountDetailHisManager
	{

        private static ESP.Finance.IDataAccess.IExpenseAccountDetailHisProvider DataProvider { get { return ESP.Configuration.ProviderHelper<ESP.Finance.IDataAccess.IExpenseAccountDetailHisProvider>.Instance; } }

        
		#region  成员方法


		/// <summary>
		/// 增加一条数据
		/// </summary>
        public static int Add(ESP.Finance.Entity.ExpenseAccountDetailHisInfo model)
		{
			return DataProvider.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
        public static void Update(ESP.Finance.Entity.ExpenseAccountDetailHisInfo model)
		{
            DataProvider.Update(model);
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
        public static ESP.Finance.Entity.ExpenseAccountDetailHisInfo GetModel(int id)
		{
			
			return DataProvider.GetModel(id);
		}

        #region 获得数据列表
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public static List<ExpenseAccountDetailHisInfo> GetAllList()
        {
            return DataProvider.GetList(""); 
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public static List<ExpenseAccountDetailHisInfo> GetList(string term)
        {
            return DataProvider.GetList(term);
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public static DataSet GetDsList(string term)
        {
            return DataProvider.GetDsList(term);
        }
        
        #endregion 获得数据列表

		#endregion  成员方法
	}
}

