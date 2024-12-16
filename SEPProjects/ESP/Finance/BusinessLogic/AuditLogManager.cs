using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using ESP.Finance.DataAccess;

namespace ESP.Finance.BusinessLogic
{



    public static class AuditLogManager
    {

        private static ESP.Finance.IDataAccess.IAuditLogDataProvider DataProvider { get { return ESP.Configuration.ProviderHelper<ESP.Finance.IDataAccess.IAuditLogDataProvider>.Instance; } }
        //private const string _dalProviderName = "AuditLogProvider";



        #region IAuditLogProvider 成员

        public static int AddBatch(ESP.Finance.Entity.AuditLogInfo model)
        {
            int count = 0;
            using (SqlConnection conn = new SqlConnection(DbHelperSQL.connectionString))
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();
                try
                {
                    Add(model, trans);
                    IList<ESP.Finance.Entity.PNBatchRelationInfo> lists = ESP.Finance.BusinessLogic.PNBatchRelationManager.GetList(" batchid=" + model.FormID, new List<SqlParameter>(), trans);

                    foreach (ESP.Finance.Entity.PNBatchRelationInfo relation in lists)
                    {
                        model.FormID = relation.ReturnID;
                        model.FormType = (int)ESP.Finance.Utility.FormType.Return;
                        Add(model, trans);
                        count++;
                    }
                    trans.Commit();
                }
                catch
                {
                    trans.Rollback();
                }
                return count;
            }
        }

        public static int Add(ESP.Finance.Entity.AuditLogInfo model)
        {
            return DataProvider.Add(model);
        }
        public static int Add(ESP.Finance.Entity.AuditLogInfo model, System.Data.SqlClient.SqlTransaction trans)
        {
            return DataProvider.Add(model, trans);
        }

        public static ESP.Finance.Entity.AuditLogInfo GetModel(int logid)
        {
            return DataProvider.GetModel(logid);
        }

        public static IList<ESP.Finance.Entity.AuditLogInfo> GetList(string term, List<System.Data.SqlClient.SqlParameter> param)
        {
            return DataProvider.GetList(term, param);
        }

        public static IList<ESP.Finance.Entity.AuditLogInfo> GetProjectList(int formId)
        {
            return DataProvider.GetProjectList("and FormType =" + ((int)Utility.FormType.Project).ToString() + " and FormID=" + formId, null);
        }

        public static IList<ESP.Finance.Entity.AuditLogInfo> GetConsumptionList(int formId)
        {
            return DataProvider.GetProjectList("and FormType =" + ((int)Utility.FormType.Consumption).ToString() + " and FormID=" + formId, null);
        }

        public static IList<ESP.Finance.Entity.AuditLogInfo> GetRebateRegistrationList(int formId)
        {
            return DataProvider.GetProjectList("and FormType =" + ((int)Utility.FormType.RebateRegistration).ToString() + " and FormID=" + formId, null);
        }

        public static IList<ESP.Finance.Entity.AuditLogInfo> GetRequestForSealList(int formId)
        {
            return DataProvider.GetProjectList("and FormType =" + ((int)Utility.FormType.RequestForSeal).ToString() + " and FormID=" + formId, null);
        }

        public static IList<ESP.Finance.Entity.AuditLogInfo> GetContractList(int projectId)
        {
            return DataProvider.GetProjectList("and FormType in (" + ((int)Utility.FormType.Project).ToString() + "," +((int)Utility.FormType.Contract).ToString() + ") and FormID=" + projectId, null);
        }

        public static IList<ESP.Finance.Entity.AuditLogInfo> GetApplyForInvioceList(int projectId)
        {
            return DataProvider.GetProjectList("and FormType in (" + ((int)Utility.FormType.Project).ToString() + "," + ((int)Utility.FormType.ApplyForInvioce).ToString() + ") and FormID=" + projectId, null);
        }

        public static IList<ESP.Finance.Entity.AuditLogInfo> GetProxyPNList(int formId)
        {
            return DataProvider.GetProxyPNList("and FormType =" + ((int)Utility.FormType.ProxyPnReport).ToString() + " and FormID=" + formId, null);
        }

        public static IList<ESP.Finance.Entity.AuditLogInfo> GetSupporterList(int formId)
        {
            return DataProvider.GetSupporterList("and FormType =" + ((int)Utility.FormType.Supporter).ToString() + " and FormID=" + formId, null);
        }

        //public static IList<ESP.Finance.Entity.AuditLogInfo> GetPaymentList(Func<ESP.Finance.Entity.AuditLogInfo, bool> predicate)
        //{
        //    return DataProvider.GetPaymentList(null, null).Where(predicate).ToList<ESP.Finance.Entity.AuditLogInfo>();
        //}
        public static IList<ESP.Finance.Entity.AuditLogInfo> GetBatchList(int formId)
        {
            return DataProvider.GetBatchList("and FormType =" + ((int)Utility.FormType.PNBatch).ToString() + " and FormID=" + formId, null);
        }

        //public static IList<ESP.Finance.Entity.AuditLogInfo> GetReturnList(Func<ESP.Finance.Entity.AuditLogInfo, bool> predicate)
        //{
        //    return DataProvider.GetReturnList(null, null).Where(predicate).ToList<ESP.Finance.Entity.AuditLogInfo>();
        //}
        public static IList<ESP.Finance.Entity.AuditLogInfo> GetPaymentList(int formId)
        {
            return DataProvider.GetPaymentList("and FormType =" + ((int)Utility.FormType.Payment).ToString() + " and FormID=" + formId, null);
        }
        public static IList<ESP.Finance.Entity.AuditLogInfo> GetReturnList(int formId)
        {
            return DataProvider.GetReturnList("and FormType =" + ((int)Utility.FormType.Return).ToString() + " and FormID=" + formId, null);
        }
        public static IList<ESP.Finance.Entity.AuditLogInfo> GetRefundList(int formId)
        {
            return DataProvider.GetReturnList("and FormType =" + ((int)Utility.FormType.Refund).ToString() + " and FormID=" + formId, null);
        }

        public static IList<ESP.Finance.Entity.AuditLogInfo> GetOOPList(int formId)
        {
            return DataProvider.GetReturnList("and FormType =" + ((int)Utility.FormType.ExpenseAccount).ToString() + " and FormID=" + formId, null);
        }
        #endregion

    }
}
