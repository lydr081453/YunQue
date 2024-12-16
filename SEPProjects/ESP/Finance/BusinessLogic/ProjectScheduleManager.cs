using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESP.Finance.Utility;
namespace ESP.Finance.BusinessLogic
{
     
     
    public static class ProjectScheduleManager
    {

        private static ESP.Finance.IDataAccess.IProjectScheduleDataProvider DataProvider{get{return ESP.Configuration.ProviderHelper<ESP.Finance.IDataAccess.IProjectScheduleDataProvider>.Instance;}}
        //private const string _dalProviderName = "ProjectScheduleDALProvider";

        


        #region  成员方法


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public static int Add(ESP.Finance.Entity.ProjectScheduleInfo model)
        {
            return DataProvider.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public static UpdateResult Update(ESP.Finance.Entity.ProjectScheduleInfo model)
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
        public static DeleteResult Delete(int AreaID)
        {
            int res = 0;
            try
            {
                res = DataProvider.Delete(AreaID);
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
        /// 根据条件删除数据
        /// </summary>
        public static DeleteResult Delete(string condition)
        {
            if (string.IsNullOrEmpty(condition)) return DeleteResult.UnExecute;
            int res = 0;
            try
            {
                res = DataProvider.Delete(condition);
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
        public static ESP.Finance.Entity.ProjectScheduleInfo GetModel(int AreaID)
        {

            return DataProvider.GetModel(AreaID);
        }

        #region 获得数据列表
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public static IList<ESP.Finance.Entity.ProjectScheduleInfo> GetAllList()
        {
            return GetList(null);
        }

        public static IList<ESP.Finance.Entity.ProjectScheduleInfo> GetListByProject(int projectId)
        {
            return GetList("ProjectID=" + projectId);
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public static IList<ESP.Finance.Entity.ProjectScheduleInfo> GetList(string term)
        {
            return GetList(term, null);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public static IList<ESP.Finance.Entity.ProjectScheduleInfo> GetList(string term, List<System.Data.SqlClient.SqlParameter> param)
        {
            return DataProvider.GetList(term, param);
        }
        #endregion 获得数据列表
        #endregion  成员方法
    }
}
