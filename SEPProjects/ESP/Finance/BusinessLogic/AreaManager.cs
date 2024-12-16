﻿using System;
using System.Data;
using System.Collections.Generic;
using System.Data.SqlClient;
using ESP.Finance.Entity;
using ESP.Finance.Utility;
namespace ESP.Finance.BusinessLogic
{
    /// <summary>
    /// 业务逻辑类AreaInfo 的摘要说明。
    /// </summary>
     
     
    public static class AreaManager
    {
        //private readonly ESP.Finance.DataAccess.AreaDAL dal = new ESP.Finance.DataAccess.AreaDAL();

        private static ESP.Finance.IDataAccess.IAreaDataProvider DataProvider{get{return ESP.Configuration.ProviderHelper<ESP.Finance.IDataAccess.IAreaDataProvider>.Instance;}}
        //private const string _dalProviderName = "AreaDALProvider";

        


        #region  成员方法


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public static int Add(ESP.Finance.Entity.AreaInfo model)
        {
            return DataProvider.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public static UpdateResult Update(ESP.Finance.Entity.AreaInfo model)
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
        /// 得到一个对象实体
        /// </summary>
        public static ESP.Finance.Entity.AreaInfo GetModel(int AreaID)
        {

            return DataProvider.GetModel(AreaID);
        }

        #region 获得数据列表
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public static IList<AreaInfo> GetAllList()
        {
            return GetList(null);
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public static IList<AreaInfo> GetList(string term)
        {
            return GetList(term, null);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public static IList<AreaInfo> GetList(string term, List<System.Data.SqlClient.SqlParameter> param)
        {
            return DataProvider.GetList(term, param);
        }
        #endregion 获得数据列表
        #endregion  成员方法
    }
}

