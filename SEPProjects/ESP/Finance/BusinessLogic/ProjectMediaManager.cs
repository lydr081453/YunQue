using System;
using System.Data;
using System.Collections.Generic;
using System.Data.SqlClient;
using ESP.Finance.Entity;
using ESP.Finance.Utility;
namespace ESP.Finance.BusinessLogic
{
    /// <summary>
    /// 业务逻辑类ProjectMediaInfo 的摘要说明。
    /// </summary>
     
     
    public static class ProjectMediaManager
    {
        //private readonly ESP.Finance.DataAccess.ProjectMediaDAL dal = new ESP.Finance.DataAccess.ProjectMediaDAL();

        private static ESP.Finance.IDataAccess.IProjectMediaDataProvider DataProvider{get{return ESP.Configuration.ProviderHelper<ESP.Finance.IDataAccess.IProjectMediaDataProvider>.Instance;}}
        //private const string _dalProviderName = "ProjectMediaDALProvider";

        


        #region  成员方法


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public static int Add(ESP.Finance.Entity.ProjectMediaInfo model)
        {
            return DataProvider.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public static UpdateResult Update(ESP.Finance.Entity.ProjectMediaInfo model)
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

        public static void UpdateAndAdd(ProjectMediaInfo oldPM,ProjectMediaInfo newPM)
        {
            using (SqlConnection conn = new SqlConnection(DataAccess.DbHelperSQL.connectionString))
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();
                try
                {
                    int r1 = DataProvider.Update(oldPM,trans);
                    int r2 = DataProvider.Add(newPM, trans);
                    if (r1 != 1)
                        throw new Exception("原媒体付款主体Update失败");
                    if(r2<=0)
                        throw new Exception("媒体付款主体Add失败");
                    trans.Commit();
                    //throw new Exception("成功");
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    throw new Exception(ex.Message);
                }
            }
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public static DeleteResult Delete(int ProjectMediaID)
        {
            int res = 0;
            try
            {
                res = DataProvider.Delete(ProjectMediaID);
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
        public static ESP.Finance.Entity.ProjectMediaInfo GetModel(int ProjectMediaID)
        {

            return DataProvider.GetModel(ProjectMediaID);
        }

        #region 获得数据列表
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public static IList<ProjectMediaInfo> GetAllList()
        {
            return GetList(null);
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public static IList<ProjectMediaInfo> GetList(string term)
        {
            return GetList(term, null);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public static IList<ProjectMediaInfo> GetList(string term, List<System.Data.SqlClient.SqlParameter> param)
        {
            return DataProvider.GetList(term, param);
        }
        #endregion 获得数据列表
        #endregion  成员方法
    }
}

