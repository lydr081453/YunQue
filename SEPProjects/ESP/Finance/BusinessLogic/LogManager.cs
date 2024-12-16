using System;
using System.Data;
using System.Collections.Generic;
using ESP.Finance.Entity;
using ESP.Finance.Utility;
using System.Data.SqlClient;
namespace ESP.Finance.BusinessLogic
{
	/// <summary>
	/// 业务逻辑类LogBLL 的摘要说明。
	/// </summary>
     
     
    public static class LogManager
	{
		//private readonly ESP.Finance.DataAccess.LogDAL dal=new ESP.Finance.DataAccess.LogDAL();

        private static ESP.Finance.IDataAccess.ILogDataProvider DataProvider{get{return ESP.Configuration.ProviderHelper<ESP.Finance.IDataAccess.ILogDataProvider>.Instance;}}
        //private const string _dalProviderName = "LogDALProvider";

        
		#region  成员方法




        /// <summary>
        /// 增加一条数据
        /// </summary>
         
         
        public static int Add(ESP.Finance.Entity.LogInfo model)
        {
            try
            {
                //trans//return DataProvider.Add(model,true);
                return DataProvider.Add(model);
            }
            catch (System.Exception)
            {
                return 0;
            }
            
        }

        public static int AddException(Exception ex)
        {
            

            try
            {
                return DataProvider.AddException(ex);
            }
            catch (System.Exception)
            {
                return 0;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="operation">操作类型</param>
        /// <param name="tablename">表名</param>
        /// <param name="des">描述</param>
        /// <returns></returns>
         
         
        public static int Add(string operation, string tablename, string des)
        {
            try
            {
                //trans//return DataProvider.Add(operation, tablename, des,true);
                return DataProvider.Add(operation, tablename, des);
            }
            catch (System.Exception)
            {
                return 0;
            }
            
        }

        public static int OnlyAdd(string operation, string tablename, string des)
        {
            try
            {
                return DataProvider.Add(operation, tablename, des);
            }
            catch (System.Exception)
            {
            	return 0;
            }
            
        }


		/// <summary>
		/// 更新一条数据
		/// </summary>
         
         
		public static UpdateResult Update(ESP.Finance.Entity.LogInfo model)
		{
            int res = 0;
            try
            {
                //trans//res = DataProvider.Update(model,true);
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
		public static DeleteResult Delete(int LogID)
		{

            int res = 0;
            try
            {
                res = DataProvider.Delete(LogID);
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
		public static ESP.Finance.Entity.LogInfo GetModel(int LogID)
		{
			
			return DataProvider.GetModel(LogID);
		}

        #region 获得数据列表
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public static IList<LogInfo> GetAllList()
        {
            return GetList(null);
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public static IList<LogInfo> GetList(string term)
        {
            return GetList(term, null);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public static IList<LogInfo> GetList(string term, List<System.Data.SqlClient.SqlParameter> param)
        {
            return DataProvider.GetList(term, param);
        }

 
        #endregion 获得数据列表

		#endregion  成员方法
	}
}

