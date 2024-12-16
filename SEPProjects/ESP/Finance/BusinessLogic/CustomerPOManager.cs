﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESP.Finance.BusinessLogic
{
     
     
    public static class CustomerPOManager
    {
        //private readonly ESP.Finance.DataAccess.CustomerPODAL dal=new ESP.Finance.DataAccess.CustomerPODAL();

        private static ESP.Finance.IDataAccess.ICustomerPODataProvider DataProvider{get{return ESP.Configuration.ProviderHelper<ESP.Finance.IDataAccess.ICustomerPODataProvider>.Instance;}}
        //private const string _dalProviderName = "CustomerPODALProvider";

        private const string tablename = "CustomerPOInfo";

        



        #region ICustomerPOProvider 成员
         
         
        public static int Add(ESP.Finance.Entity.CustomerPOInfo model)
        {
            //trans//return DataProvider.Add(model, true);
            return DataProvider.Add(model);
        }

         
         
        public static ESP.Finance.Utility.UpdateResult Update(ESP.Finance.Entity.CustomerPOInfo model)
        {
            int res = 0;
            try
            {
                //trans//res = DataProvider.Update(model, true);
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

        public static ESP.Finance.Utility.DeleteResult Delete(int poId)
        {
            int res = 0;
            try
            {
                res = DataProvider.Delete(poId);
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

        public static ESP.Finance.Entity.CustomerPOInfo GetModel(int poId)
        {
            return DataProvider.GetModel(poId);
        }
        #region 获得数据列表
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public static IList<ESP.Finance.Entity.CustomerPOInfo> GetAllList()
        {
            return GetList(null);
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public static IList<ESP.Finance.Entity.CustomerPOInfo> GetList(string term)
        {
            return GetList(term, null);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public static IList<ESP.Finance.Entity.CustomerPOInfo> GetList(string term, List<System.Data.SqlClient.SqlParameter> param)
        {
            return DataProvider.GetList(term, param);
        }
        #endregion 获得数据列表

        #endregion
    }
}