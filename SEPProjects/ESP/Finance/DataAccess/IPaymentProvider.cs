using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace ESP.Finance.IDataAccess
{
    [ESP.Configuration.Provider]
    public interface IPaymentDataProvider
    {
        int Add(ESP.Finance.Entity.PaymentInfo model);
        int Add(ESP.Finance.Entity.PaymentInfo model,System.Data.SqlClient.SqlTransaction trans);
        int Update(ESP.Finance.Entity.PaymentInfo model);
        int Delete(int paymentId);
        int DeleteByProject(int projectId);
        int DeleteByProject(int projectId,System.Data.SqlClient.SqlTransaction trans);
        ESP.Finance.Entity.PaymentInfo GetModel(int paymentId);
        int UpdateProjectCode(int projectId, string projectCode);
        int UpdatePaymentBankInfo(int projectId, int bankId);
        int UpdateProjectCode(int projectId, string projectCode,System.Data.SqlClient.SqlTransaction trans);
        string CreateCode(string branchDesCode);
        bool PaymentCodeExist(int paymentId, string paymentCode);
        //IList<ESP.Finance.Entity.PaymentInfo> GetAllList();
        //IList<ESP.Finance.Entity.PaymentInfo> GetList(string term);
        IList<ESP.Finance.Entity.PaymentInfo> GetList(string term, List<System.Data.SqlClient.SqlParameter> param);
        IList<ESP.Finance.Entity.PaymentInfo> GetList(string term, List<System.Data.SqlClient.SqlParameter> param,System.Data.SqlClient.SqlTransaction trans);
        IList<ESP.Finance.Entity.PaymentInfo> GetListByProject(int projectID, string term, List<System.Data.SqlClient.SqlParameter> param);
        IList<ESP.Finance.Entity.PaymentInfo> GetListByProject(int projectID, string term, List<System.Data.SqlClient.SqlParameter> param, System.Data.SqlClient.SqlTransaction trans);
        IList<ESP.Finance.Entity.PaymentInfo> GetWaitAuditList(string userIds, string strTerms, List<System.Data.SqlClient.SqlParameter> parms);
        #region is in Trans
        //int DeleteByProject(int projectId,bool isInTrans);
        //int Add(ESP.Finance.Entity.PaymentInfo model,bool isInTrans);
        //int Update(ESP.Finance.Entity.PaymentInfo model,bool isInTrans);
        //int UpdateProjectCode(int projectId, string projectCode, bool isInTrans);


        //ESP.Finance.Entity.PaymentInfo GetModel(int paymentId,bool isInTrans);
        //string CreateCode(string branchDesCode,bool isInTrans);
        //IList<ESP.Finance.Entity.PaymentInfo> GetList(string term, List<System.Data.SqlClient.SqlParameter> param,bool isInTrans);
        #endregion

        IList<ESP.Finance.Entity.PaymentInfo> GetWaitAuditList(int[] userIds);
        DataTable GetPaymentReportList(string term);
        DataTable GetPaymentReportListByMonth(string  groupids,int year,int month);
        DataTable GetPaymentReportByGroup(string term);
        DataTable GetPaymentReportByCustomer(string term);
        int UpdatePaymentConfirmMonth(int projectId, int year, int month);
    }
}
