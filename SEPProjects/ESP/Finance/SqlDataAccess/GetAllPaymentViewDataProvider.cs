using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using ESP.Finance.Entity;
using System.Collections.Generic;
using ESP.Finance.Utility;
namespace ESP.Finance.DataAccess
{
    internal class GetAllPaymentViewDataProvider : ESP.Finance.IDataAccess.IGetAllPaymentViewProvider
    {
         /// <summary>
        /// 获得所有单据列表
        /// </summary>
        public IList<ESP.Finance.Entity.GetAllPaymentViewInfo> GetTotalList(string strWhere,List<SqlParameter> parms)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select id,orderid,first_assessorname,status,thirdParty_materielDesc,Filiale_AuditName,DepartmentId,Department,requisitionflow,totalprice,PRType,prNo,account_name,account_bank,account_number,OperationType,pid,periodType,expectPaymentPrice,inceptPrice,paymentstatus,ReturnId,requestor,ReturnCode,ReturnContent,PreFee,FactFee,ReturnStatus,IsInvoice,moneytype,requestorname,project_code,project_id,project_descripttion,supplier_name,source ");
            strSql.Append(" FROM V_GetAllPayment ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return CBO.FillCollection<ESP.Finance.Entity.GetAllPaymentViewInfo>(DbHelperSQL.Query(strSql.ToString(), parms));
        }
        /// <summary>
        /// 第三方付款
        /// </summary>
        /// <param name="strWhere"></param>
        /// <param name="parms"></param>
        /// <returns></returns>
        public IList<ESP.Finance.Entity.GetAllPaymentViewInfo> Get3rdPartList(string strWhere, List<SqlParameter> parms)
        {
            strWhere = "moneytype='第三方付款' and " + strWhere;
            return GetTotalList(strWhere, parms);
        }
        /// <summary>
        /// 稿费3000以上付款
        /// </summary>
        /// <param name="strWhere"></param>
        /// <param name="parms"></param>
        /// <returns></returns>
        public IList<ESP.Finance.Entity.GetAllPaymentViewInfo> GetMediaNewList(string strWhere, List<SqlParameter> parms)
        {
            strWhere = "moneytype='稿费3000以上付款' and " + strWhere;
            return GetTotalList(strWhere, parms);
        }
        /// <summary>
        /// 对私3000以上付款
        /// </summary>
        /// <param name="strWhere"></param>
        /// <param name="parms"></param>
        /// <returns></returns>
        public IList<ESP.Finance.Entity.GetAllPaymentViewInfo> GetPrivateNewList(string strWhere, List<SqlParameter> parms)
        {
            strWhere = "moneytype='对私3000以上付款' and " + strWhere;
            return GetTotalList(strWhere, parms);
        }
        /// <summary>
        /// 稿费未处理
        /// </summary>
        /// <param name="strWhere"></param>
        /// <param name="parms"></param>
        /// <returns></returns>
        public IList<ESP.Finance.Entity.GetAllPaymentViewInfo> GetMediaUnDoList(string strWhere, List<SqlParameter> parms)
        {
            strWhere = "moneytype='稿费未处理' and " + strWhere;
            return GetTotalList(strWhere, parms);
        }
        /// <summary>
        /// 对私未处理
        /// </summary>
        /// <param name="strWhere"></param>
        /// <param name="parms"></param>
        /// <returns></returns>
        public IList<ESP.Finance.Entity.GetAllPaymentViewInfo> GetPrivateUnDoList(string strWhere, List<SqlParameter> parms)
        {
            strWhere = "moneytype='对私未处理' and " + strWhere;
            return GetTotalList(strWhere, parms);
        }
        /// <summary>
        /// 稿费3000以下付款
        /// </summary>
        /// <param name="strWhere"></param>
        /// <param name="parms"></param>
        /// <returns></returns>
        public IList<ESP.Finance.Entity.GetAllPaymentViewInfo> GetMediaPayableList(string strWhere, List<SqlParameter> parms)
        {
            strWhere = "moneytype='稿费3000以下付款' and " + strWhere;
            return GetTotalList(strWhere, parms);
        }
        /// <summary>
        /// 对私3000以下付款
        /// </summary>
        /// <param name="strWhere"></param>
        /// <param name="parms"></param>
        /// <returns></returns>
        public IList<ESP.Finance.Entity.GetAllPaymentViewInfo> GetPrivatePayableList(string strWhere, List<SqlParameter> parms)
        {
            strWhere = "moneytype='对私3000以下付款' and " + strWhere;
            return GetTotalList(strWhere, parms);
        }
    }
}
