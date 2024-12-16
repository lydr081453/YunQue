using System;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESP.Finance.IDataAccess
{
    [ESP.Configuration.Provider]
    public interface IGetAllPaymentViewProvider
    {
        /// <summary>
        /// 获得所有单据列表
        /// </summary>
        IList<ESP.Finance.Entity.GetAllPaymentViewInfo> GetTotalList(string strWhere, List<SqlParameter> parms);
        /// <summary>
        /// 第三方付款
        /// </summary>
        /// <param name="strWhere"></param>
        /// <param name="parms"></param>
        /// <returns></returns>
        IList<ESP.Finance.Entity.GetAllPaymentViewInfo> Get3rdPartList(string strWhere, List<SqlParameter> parms);
      
        /// <summary>
        /// 稿费3000以上付款
        /// </summary>
        /// <param name="strWhere"></param>
        /// <param name="parms"></param>
        /// <returns></returns>
        IList<ESP.Finance.Entity.GetAllPaymentViewInfo> GetMediaNewList(string strWhere, List<SqlParameter> parms);
        /// <summary>
        /// 对私3000以上付款
        /// </summary>
        /// <param name="strWhere"></param>
        /// <param name="parms"></param>
        /// <returns></returns>
        IList<ESP.Finance.Entity.GetAllPaymentViewInfo> GetPrivateNewList(string strWhere, List<SqlParameter> parms);
        /// <summary>
        /// 稿费未处理
        /// </summary>
        /// <param name="strWhere"></param>
        /// <param name="parms"></param>
        /// <returns></returns>
        IList<ESP.Finance.Entity.GetAllPaymentViewInfo> GetMediaUnDoList(string strWhere, List<SqlParameter> parms);
       
        /// <summary>
        /// 对私未处理
        /// </summary>
        /// <param name="strWhere"></param>
        /// <param name="parms"></param>
        /// <returns></returns>
        IList<ESP.Finance.Entity.GetAllPaymentViewInfo> GetPrivateUnDoList(string strWhere, List<SqlParameter> parms);
       
        /// <summary>
        /// 稿费3000以下付款
        /// </summary>
        /// <param name="strWhere"></param>
        /// <param name="parms"></param>
        /// <returns></returns>
        IList<ESP.Finance.Entity.GetAllPaymentViewInfo> GetMediaPayableList(string strWhere, List<SqlParameter> parms);
        /// <summary>
        /// 对私3000以下付款
        /// </summary>
        /// <param name="strWhere"></param>
        /// <param name="parms"></param>
        /// <returns></returns>
        IList<ESP.Finance.Entity.GetAllPaymentViewInfo> GetPrivatePayableList(string strWhere, List<SqlParameter> parms);
       
    }
}
