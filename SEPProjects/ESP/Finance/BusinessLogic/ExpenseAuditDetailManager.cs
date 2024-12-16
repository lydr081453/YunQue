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


    public static class ExpenseAuditDetailManager
	{

        private static ESP.Finance.IDataAccess.IExpenseAuditDetailProvider DataProvider { get { return ESP.Configuration.ProviderHelper<ESP.Finance.IDataAccess.IExpenseAuditDetailProvider>.Instance; } }

        
		#region  成员方法

        public static System.Data.DataTable GetWorkflow(int id, int level)
        {
            return DataProvider.GetWorkflow(id,level);
        }
		/// <summary>
		/// 增加一条数据
		/// </summary>
        public static int Add(ESP.Finance.Entity.ExpenseAuditDetailInfo model)
		{
			return DataProvider.Add(model);
		}

        public static int Add(ESP.Finance.Entity.ExpenseAuditDetailInfo model,SqlTransaction trans)
        {
            return DataProvider.Add(model, trans);
        }



		/// <summary>
		/// 更新一条数据
		/// </summary>
        public static UpdateResult Update(ESP.Finance.Entity.ExpenseAuditDetailInfo model)
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

        public static int DeleteByReturnID(int returnId, SqlTransaction trans)
        {
            return DataProvider.DeleteByReturnID(returnId, trans);
        }

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
        public static ESP.Finance.Entity.ExpenseAuditDetailInfo GetModel(int id)
		{
			
			return DataProvider.GetModel(id);
		}

        #region 获得数据列表
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public static List<ExpenseAuditDetailInfo> GetAllList()
        {
            return DataProvider.GetList("");
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public static List<ExpenseAuditDetailInfo> GetList(string term)
        {
            return DataProvider.GetList(term);
        }

        /// <summary>
        /// 备份审批记录
        /// </summary>
        /// <param name="expenseAccountId"></param>
        public static void AddLogList(int expenseAccountId)
        {
            List<ESP.Finance.Entity.ExpenseAuditDetailInfo> list = GetList(" and ExpenseAuditID = " + expenseAccountId);
            ESP.Finance.Entity.AuditLogInfo loginfo = null;
            foreach (ESP.Finance.Entity.ExpenseAuditDetailInfo model in list)
            {
                try
                {
                    loginfo = new AuditLogInfo();
                    loginfo.FormID = model.ExpenseAuditID;
                    loginfo.FormType = (int)ESP.Finance.Utility.FormType.ExpenseAccount;
                    loginfo.AuditorSysID = model.AuditorUserID;
                    loginfo.AuditorUserName = model.AuditorUserName;
                    loginfo.AuditorUserCode = model.AuditorUserCode;
                    loginfo.AuditorEmployeeName = model.AuditorEmployeeName;
                    loginfo.Suggestion = model.Suggestion;
                    loginfo.AuditDate = model.AuditeDate;
                    loginfo.AuditStatus = model.AuditeStatus;
                    ESP.Finance.BusinessLogic.AuditLogManager.Add(loginfo);
                }
                catch { }
            }
        }

        #endregion 获得数据列表

		#endregion  成员方法
	}
}

