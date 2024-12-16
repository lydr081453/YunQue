using System;
using System.Data;
using System.Collections.Generic;
using System.Data.SqlClient;
using ESP.Finance.Entity;
using ESP.Finance.Utility;
namespace ESP.Finance.BusinessLogic
{
        public static class ReturnInvoiceManager
    {
            private static ESP.Finance.IDataAccess.IReturnInvoiceProvider DataProvider { get { return ESP.Configuration.ProviderHelper<ESP.Finance.IDataAccess.IReturnInvoiceProvider>.Instance; } }
            
            public static int Add(ESP.Finance.Entity.ReturnInvoiceInfo model)
            {
                int ret= DataProvider.Add(model);
                if (ret > 0)
                {
                    ESP.Finance.Entity.AuditLogInfo log = new AuditLogInfo();
                    ESP.Compatible.Employee auditor = new ESP.Compatible.Employee(model.RequestorID.Value);
                    log.AuditDate = DateTime.Now;
                    log.AuditorEmployeeName = auditor.Name;
                    log.AuditorSysID = Convert.ToInt32(auditor.SysID);
                    log.AuditorUserCode = auditor.ID;
                    log.AuditorUserName = auditor.ITCode;
                    log.AuditStatus = (int)ESP.Finance.Utility.AuditHistoryStatus.PassAuditing;
                    log.FormID = model.ReturnID;
                    log.FormType = (int)ESP.Finance.Utility.FormType.Return;
                    log.Suggestion = "提交发票"+model.RequestRemark;
                    ESP.Finance.BusinessLogic.AuditLogManager.Add(log);
                }
                return ret;
            }
            public static int Update(ESP.Finance.Entity.ReturnInvoiceInfo model)
            {
                int ret= DataProvider.Update(model);
                if (ret > 0)
                {
                    ESP.Finance.Entity.AuditLogInfo log = new AuditLogInfo();
                    ESP.Compatible.Employee auditor = null;
                    if (model.Status == 1)
                        auditor = new ESP.Compatible.Employee(model.FAID.Value);
                    else if(model.Status==2)
                        auditor = new ESP.Compatible.Employee(model.FinanceID.Value);
                    log.AuditDate = DateTime.Now;
                    log.AuditorEmployeeName = auditor.Name;
                    log.AuditorSysID = Convert.ToInt32(auditor.SysID);
                    log.AuditorUserCode = auditor.ID;
                    log.AuditorUserName = auditor.ITCode;
                    log.AuditStatus = (int)ESP.Finance.Utility.AuditHistoryStatus.PassAuditing;
                    log.FormID = model.ReturnID;
                    log.FormType = (int)ESP.Finance.Utility.FormType.Return;
                    if (model.Status == 1)
                        log.Suggestion ="接收发票"+ model.FARemark;
                    else if (model.Status == 2)
                        log.Suggestion = "接收发票" + model.FinanceRemark;
                    ESP.Finance.BusinessLogic.AuditLogManager.Add(log);
                    if (model.Status == 2)
                    {
                        ESP.Finance.BusinessLogic.ReturnManager.UpdateIsInvoice(model.ReturnID.Value, 1);
                    }
                }
                return ret;
            }
            public static int Delete(int InvId)
            {
                return DataProvider.Delete(InvId);
            }
            public static ESP.Finance.Entity.ReturnInvoiceInfo GetModel(int InvId)
            {
                return DataProvider.GetModel(InvId);
            }
            public static ESP.Finance.Entity.ReturnInvoiceInfo GetModelByReturnID(int ReturnId)
            {
                return DataProvider.GetModelByReturnID(ReturnId);
            }
            public static IList<ESP.Finance.Entity.ReturnInvoiceInfo> GetList(string term, List<System.Data.SqlClient.SqlParameter> param)
            {
                return DataProvider.GetList(term, param);
            }
    }
}
