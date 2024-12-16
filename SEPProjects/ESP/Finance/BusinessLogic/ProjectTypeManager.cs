using System;
using System.Data;
using System.Collections.Generic;
using ESP.Finance.Entity;
using ESP.Finance.Utility;
namespace ESP.Finance.BusinessLogic
{
	/// <summary>
	/// 业务逻辑类ProjectTypeBLL 的摘要说明。
	/// </summary>
     
     
    public static class ProjectTypeManager
	{
		//private readonly ESP.Finance.DataAccess.ProjectTypeDAL dal=new ESP.Finance.DataAccess.ProjectTypeDAL();

        private static ESP.Finance.IDataAccess.IProjectTypeDataProvider DataProvider{get{return ESP.Configuration.ProviderHelper<ESP.Finance.IDataAccess.IProjectTypeDataProvider>.Instance;}}
        //private const string _dalProviderName = "ProjectTypeDALProvider";

        
		#region  成员方法

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public static int  Add(ESP.Finance.Entity.ProjectTypeInfo model)
		{
			return DataProvider.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public static UpdateResult Update(ESP.Finance.Entity.ProjectTypeInfo model)
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
		public static DeleteResult Delete(int ProjectTypeID)
		{

            int res = 0;
            try
            {
                res = DataProvider.Delete(ProjectTypeID);
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
		public static ESP.Finance.Entity.ProjectTypeInfo GetModel(int ProjectTypeID)
		{
			
			return DataProvider.GetModel(ProjectTypeID);
		}

        #region 获得数据列表
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public static IList<ProjectTypeInfo> GetAllList()
        {
            return GetList(null);
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public static IList<ProjectTypeInfo> GetList(string term)
        {
            return GetList(term, null);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public static IList<ProjectTypeInfo> GetList(string term, List<System.Data.SqlClient.SqlParameter> param)
        {
            return DataProvider.GetList(term, param);
        }
        #endregion 获得数据列表

		#endregion  成员方法
	}
}

