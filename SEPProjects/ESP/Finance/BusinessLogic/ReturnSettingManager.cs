using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESP.Finance.Utility;
using System.Data.SqlClient;

namespace ESP.Finance.BusinessLogic
{
     
     
    public static class ReturnSettingManager
    {
	//private readonly ESP.Finance.DataAccess.ReturnDAL dal=new ESP.Finance.DataAccess.ReturnDAL();

        private static ESP.Finance.IDataAccess.IReturnSettingDataProvider DataProvider{get{return ESP.Configuration.ProviderHelper<ESP.Finance.IDataAccess.IReturnSettingDataProvider>.Instance;}}
        //private const string _dalProviderName = "ReturnSettingDALProvider";

        
		#region  成员方法


		/// <summary>
		/// 增加一条数据
		/// </summary>
         
         
         
		public static int  Add(ESP.Finance.Entity.ReturnSettingInfo model)
		{
			//trans//return DataProvider.Add(model,true);
            return DataProvider.Add(model);
		}


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public static int Add(List<ESP.Finance.Entity.ReturnSettingInfo> models,int ReturnId)
        {
            if (ReturnId == 0) return 0;
            using (SqlConnection conn = new SqlConnection(DataAccess.DbHelperSQL.connectionString))
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();
                try
                {
                    DataProvider.DeleteByReturnId(ReturnId,"",null,trans);
                    int counter = 0;
                    foreach (Entity.ReturnSettingInfo model in models)
                    {
                        DataProvider.Add(model,trans);
                        counter++;
                    }
                    trans.Commit();
                    return counter;
                }
                catch
                {
                    trans.Rollback();
                    return 0;
                }
            }

        }


		/// <summary>
		/// 更新一条数据
		/// </summary>
		public static UpdateResult Update(ESP.Finance.Entity.ReturnSettingInfo model)
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
		public static DeleteResult Delete(int ReturnSettingID)
		{

            int res = 0;
            try
            {
                res = DataProvider.Delete(ReturnSettingID);
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
        public static ESP.Finance.Entity.ReturnSettingInfo GetModel(int ReturnSettingID)
		{

            return DataProvider.GetModel(ReturnSettingID);
		}

        #region 获得数据列表
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public static IList<ESP.Finance.Entity.ReturnSettingInfo> GetAllList()
        {
            return GetList(null);
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public static IList<ESP.Finance.Entity.ReturnSettingInfo> GetList(string term)
        {
            return GetList(term, null);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public static IList<ESP.Finance.Entity.ReturnSettingInfo> GetList(string term, List<System.Data.SqlClient.SqlParameter> param)
        {
            return DataProvider.GetList(term, param);
        }

        /// <summary>
        /// 根据ReturnID 获取 根据auditType删除 后的列表 
        /// </summary>
        /// <param name="ReturnId">ReturnID</param>
        /// <param name="types">auditType</param>
        /// <returns>根据ReturnID 获取 根据auditType删除 后的列表</returns>
        public static IList<ESP.Finance.Entity.ReturnSettingInfo> GetUnDelList(int ReturnId, string types)
        {
            string term = string.Empty;
            if (!string.IsNullOrEmpty(types))
            {
                if (types.Trim().Length > 0)
                {
                    term = " and AuditType in (" + types + ")";
                    DataProvider.DeleteByReturnId(ReturnId, term, null);
                }
            }
            term = " ReturnId = @ReturnID ";
            List<System.Data.SqlClient.SqlParameter> param = new List<System.Data.SqlClient.SqlParameter>();
            System.Data.SqlClient.SqlParameter sp = new System.Data.SqlClient.SqlParameter("@ReturnID",System.Data.SqlDbType.Int,4);
            sp.Value = ReturnId;
            param.Add(sp);
            return GetList(term, param);
        }
        #endregion 获得数据列表

		#endregion  成员方法
    }
}
