using System;
using System.Data.SqlClient;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESP.Finance.BusinessLogic
{
    public static class GetAllPayemtnViewManager
    {
        private static ESP.Finance.IDataAccess.IGetAllPaymentViewProvider DataProvider { get { return ESP.Configuration.ProviderHelper<ESP.Finance.IDataAccess.IGetAllPaymentViewProvider>.Instance; } }
        
        /// <summary>
        /// 获得所有单据列表
        /// </summary>
        public static IList<ESP.Finance.Entity.GetAllPaymentViewInfo> GetTotalList(string strWhere, List<SqlParameter> parms)
        {
            return DataProvider.GetTotalList(strWhere, parms);
        }
        /// <summary>
        /// 第三方付款
        /// </summary>
        /// <param name="strWhere"></param>
        /// <param name="parms"></param>
        /// <returns></returns>
        public static  IList<ESP.Finance.Entity.GetAllPaymentViewInfo> Get3rdPartList(string strWhere, List<SqlParameter> parms)
        {
            return DataProvider.Get3rdPartList(strWhere, parms);
        }
        /// <summary>
        /// 稿费3000以上付款
        /// </summary>
        /// <param name="strWhere"></param>
        /// <param name="parms"></param>
        /// <returns></returns>
        public static IList<ESP.Finance.Entity.GetAllPaymentViewInfo> GetMediaNewList(string strWhere, List<SqlParameter> parms)
        {
            return DataProvider.GetMediaNewList(strWhere, parms);
        }
        /// <summary>
        /// 对私3000以上付款
        /// </summary>
        /// <param name="strWhere"></param>
        /// <param name="parms"></param>
        /// <returns></returns>
        public static IList<ESP.Finance.Entity.GetAllPaymentViewInfo> GetPrivateNewList(string strWhere, List<SqlParameter> parms)
        {
            return DataProvider.GetPrivateNewList(strWhere, parms);
        }
        /// <summary>
        /// 稿费未处理
        /// </summary>
        /// <param name="strWhere"></param>
        /// <param name="parms"></param>
        /// <returns></returns>
        public static IList<ESP.Finance.Entity.GetAllPaymentViewInfo> GetMediaUnDoList(string strWhere, List<SqlParameter> parms)
        {
            return DataProvider.GetMediaUnDoList(strWhere, parms);
        }
        /// <summary>
        /// 对私未处理
        /// </summary>
        /// <param name="strWhere"></param>
        /// <param name="parms"></param>
        /// <returns></returns>
        public static IList<ESP.Finance.Entity.GetAllPaymentViewInfo> GetPrivateUnDoList(string strWhere, List<SqlParameter> parms)
        {
            return DataProvider.GetPrivateUnDoList(strWhere, parms);
        }
        /// <summary>
        /// 稿费3000以下付款
        /// </summary>
        /// <param name="strWhere"></param>
        /// <param name="parms"></param>
        /// <returns></returns>
        public static IList<ESP.Finance.Entity.GetAllPaymentViewInfo> GetMediaPayableList(string strWhere, List<SqlParameter> parms)
        {
            return DataProvider.GetMediaPayableList(strWhere, parms);
        }
        /// <summary>
        /// 对私3000以下付款
        /// </summary>
        /// <param name="strWhere"></param>
        /// <param name="parms"></param>
        /// <returns></returns>
        public static IList<ESP.Finance.Entity.GetAllPaymentViewInfo> GetPrivatePayableList(string strWhere, List<SqlParameter> parms)
        {
            return DataProvider.GetPrivatePayableList(strWhere, parms);
        }
    
    }
}
