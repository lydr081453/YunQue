using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESP.Finance.Utility;
namespace ESP.ITIL.BusinessLogic
{
    /// <summary>
    /// 财务PN运维业务逻辑
    /// </summary>
    public partial class Finance
    {
        public static int 提交PN申请(ESP.Finance.Entity.ReturnInfo returnModel, ESP.Compatible.Employee CurrentUser)
        {
            int ret = 0;
            if (ESP.Finance.BusinessLogic.ReturnManager.Payment(ESP.Finance.Utility.PaymentStatus.Submit, returnModel) == ESP.Finance.Utility.UpdateResult.Succeed)
            {
                if (returnModel.PaymentTypeID == ESP.Finance.Utility.PaymentType.冲抵押金)//冲抵押金
                {
                    returnModel.ReturnType = (int)ESP.Purchase.Common.PRTYpe.PN_KillForeGift;
                }
                else if (returnModel.PaymentTypeID == ESP.Finance.Utility.PaymentType.冲抵现金)//冲抵现金
                {
                    returnModel.ReturnType = (int)ESP.Purchase.Common.PRTYpe.PN_KillCash;
                }
                if (returnModel.ReturnType == (int)ESP.Purchase.Common.PRTYpe.MediaPR)
                {
                    ESP.Compatible.Employee financeEmp = new ESP.Compatible.Employee(ESP.Finance.BusinessLogic.BranchManager.GetModelByCode(returnModel.ProjectCode.Substring(0, 1)).FirstFinanceID);
                    //付款申请审核人日志
                    ESP.Finance.Entity.ReturnAuditHistInfo FinanceModel = new ESP.Finance.Entity.ReturnAuditHistInfo();
                    FinanceModel.ReturnID = returnModel.ReturnID;
                    FinanceModel.AuditType = ESP.Finance.Utility.auditorType.operationAudit_Type_Financial;
                    FinanceModel.AuditorUserID = int.Parse(financeEmp.SysID);
                    FinanceModel.AuditorUserCode = financeEmp.ID;
                    FinanceModel.AuditorEmployeeName = financeEmp.Name;
                    FinanceModel.AuditorUserName = financeEmp.ITCode;
                    FinanceModel.AuditeStatus = (int)ESP.Finance.Utility.AuditHistoryStatus.UnAuditing;
                    //付款申请表内的审批人记录
                    returnModel.PaymentUserID = int.Parse(financeEmp.SysID);
                    returnModel.PaymentEmployeeName = financeEmp.Name;
                    returnModel.PaymentCode = financeEmp.ID;
                    returnModel.PaymentUserName = financeEmp.ITCode;
                    returnModel.ReturnStatus = (int)ESP.Finance.Utility.PaymentStatus.MajorAudit;
                    ESP.Finance.BusinessLogic.ReturnManager.Update(returnModel);
                    ESP.Finance.BusinessLogic.ReturnAuditHistManager.DeleteByReturnID(returnModel.ReturnID);
                    ESP.Finance.BusinessLogic.ReturnAuditHistManager.Add(FinanceModel);
                    ESP.Finance.Utility.SendMailHelper.SendMailReturnCommit(returnModel, returnModel.RequestEmployeeName, financeEmp.Name, "", financeEmp.EMail);
                }
                else
                {
                    //ESP.Framework.Entity.OperationAuditManageInfo auditModel = ESP.Framework.BusinessLogic.OperationAuditManageManager.GetModelByUserId(returnModel.RequestorID.Value);
                    ESP.Framework.Entity.OperationAuditManageInfo auditModel = null;

                    if (returnModel.ProjectID != null && returnModel.ProjectID.Value != 0)
                    {
                        auditModel = ESP.Framework.BusinessLogic.OperationAuditManageManager.GetModelByProjectId(returnModel.ProjectID.Value);
                    }
                    if (auditModel == null)
                        auditModel = ESP.Framework.BusinessLogic.OperationAuditManageManager.GetModelByUserId(returnModel.RequestorID.Value); ;

                    if (auditModel == null)
                        auditModel = ESP.Framework.BusinessLogic.OperationAuditManageManager.GetModelByDepId(returnModel.DepartmentID.Value);

                    if (auditModel != null)
                        returnModel.PaymentUserID = auditModel.DirectorId;
                    ESP.Finance.BusinessLogic.ReturnManager.Update(returnModel);
                    ESP.Finance.BusinessLogic.ReturnAuditHistManager.DeleteByReturnID(returnModel.ReturnID);
                }
            }
            else
            {
                ret = -1;
            }
            return ret;
        }

        public static int PN业务审核(ESP.Finance.Entity.ReturnInfo returnModel, ESP.Compatible.Employee CurrentUser, string auditRemark)
        {
            string operationFlag = "";
            int ret = 0;
            ESP.Compatible.Employee emp = null;
            if (!returnModel.NeedPurchaseAudit)
            {
                emp = new ESP.Compatible.Employee(ESP.Finance.BusinessLogic.BranchManager.GetModelByCode(returnModel.ProjectCode.Substring(0, 1)).FirstFinanceID);
                operationFlag = "Finance";
            }
            else
            {
                ESP.Purchase.Entity.GeneralInfo generalModel = ESP.Purchase.BusinessLogic.GeneralInfoManager.GetModel(returnModel.PRID.Value);
                emp = new ESP.Compatible.Employee(getAuditor(generalModel));
                operationFlag = "Purchase";
            }
            int nextId = int.Parse(emp.SysID);
            string nextName = emp.Name;
            bool isLast = true;
            ret = updateReturnInfo(returnModel, nextId, nextName, isLast, auditRemark, CurrentUser);
            //发信
            ESP.Finance.Utility.SendMailHelper.SendMailPRFirstOperaPassFK(returnModel, returnModel.ReturnCode, CurrentUser.Name, emp.Name, new ESP.Compatible.Employee(returnModel.RequestorID.Value).EMail, emp.EMail, 0, operationFlag);
            return ret;
        }

        public static ESP.Finance.Entity.ReturnInfo PN财务审批状态检查(ESP.Finance.Entity.ReturnInfo returnModel)
        {
            if ((returnModel.ReturnStatus == (int)PaymentStatus.FinanceLevel2 && returnModel.PreFee <= 10000) || returnModel.ReturnStatus == (int)PaymentStatus.FinanceLevel3)
            {
                //当在财务总监最终付款时，如果付款类型是现金的话，PN状态变成待报销
                if ((returnModel.PaymentTypeID == 1 && returnModel.ReturnType != (int)ESP.Purchase.Common.PRTYpe.PN_Cash10Down && returnModel.ReturnType != (int)ESP.Purchase.Common.PRTYpe.MediaPR && returnModel.ReturnType != (int)ESP.Purchase.Common.PRTYpe.PrivatePR) )
                {
                    returnModel.ReturnStatus = (int)PaymentStatus.WaitReceiving;
                }
                else
                {
                    returnModel.ReturnStatus = (int)PaymentStatus.FinanceComplete;
                }
                //当在财务总监最终付款时，如果是押金或抵押金付款，PN状态变成待报销
                if (returnModel.ReturnType == (int)ESP.Purchase.Common.PRTYpe.PN_ForeGift || returnModel.ReturnType == (int)ESP.Purchase.Common.PRTYpe.PN_KillForeGift)
                    returnModel.ReturnStatus = (int)PaymentStatus.WaitReceiving;
                else if (returnModel.ReturnType == (int)ESP.Purchase.Common.PRTYpe.PN_KillCash)
                    returnModel.ReturnStatus = (int)PaymentStatus.WaitReceiving; //当在财务总监最终付款时,如果是抵消现金，PN状态变成待报销
                else
                {
                    if ((returnModel.PaymentTypeID == 1 && returnModel.ReturnType != (int)ESP.Purchase.Common.PRTYpe.PN_Cash10Down && returnModel.ReturnType != (int)ESP.Purchase.Common.PRTYpe.MediaPR && returnModel.ReturnType != (int)ESP.Purchase.Common.PRTYpe.PrivatePR))
                    {
                        returnModel.ReturnStatus = (int)PaymentStatus.WaitReceiving;
                    }
                    else
                    {
                        returnModel.ReturnStatus = (int)PaymentStatus.FinanceComplete;
                    }
                }
            }
            else if (returnModel.ReturnStatus == (int)PaymentStatus.FinanceLevel2 && returnModel.PreFee > 10000)
            {
                returnModel.ReturnStatus = (int)PaymentStatus.FinanceLevel3;
            }
            else if (returnModel.ReturnStatus == (int)PaymentStatus.WaitReceiving)
            {
                returnModel.ReturnStatus = (int)PaymentStatus.FinanceComplete;
            }
            else if (returnModel.ReturnStatus == (int)PaymentStatus.FinanceLevel1)
            {
                returnModel.ReturnStatus = (int)PaymentStatus.FinanceLevel2;
            }
            else if (returnModel.ReturnStatus == (int)PaymentStatus.MajorAudit)
            {
                returnModel.ReturnStatus = (int)PaymentStatus.FinanceLevel1;
            }
            return returnModel;
        }

        #region "私有方法"
        private static int updateReturnInfo(ESP.Finance.Entity.ReturnInfo returnModel, int nextId, string nextName, bool isLast, string auditRemark, ESP.Compatible.Employee CurrentUser)
        {
            int ret = 0;
            returnModel.ReturnStatus = (int)ESP.Finance.Utility.PaymentStatus.Submit;
            if (isLast)
            {
                if (!returnModel.NeedPurchaseAudit)
                    returnModel.ReturnStatus = (int)ESP.Finance.Utility.PaymentStatus.MajorAudit;
                else
                    returnModel.ReturnStatus = (int)ESP.Finance.Utility.PaymentStatus.PurchaseFirst;
            }
            ESP.Finance.Utility.UpdateResult result = ESP.Finance.BusinessLogic.ReturnManager.Update(returnModel);
            if (result == ESP.Finance.Utility.UpdateResult.Succeed)
            {
                ret = 1;
                SetAuditHistory(returnModel, 0, (int)ESP.Finance.Utility.AuditHistoryStatus.PassAuditing, auditRemark, CurrentUser);
            }
            return ret;
        }

        private static void SetAuditHistory(ESP.Finance.Entity.ReturnInfo model, int audittype, int auditstatus, string auditRemark, ESP.Compatible.Employee CurrentUser)
        {
            string term = string.Format(" ReturnID={0}  and AuditeStatus={1}", model.ReturnID, (int)ESP.Finance.Utility.AuditHistoryStatus.UnAuditing);
            IList<ESP.Finance.Entity.ReturnAuditHistInfo> auditlist = ESP.Finance.BusinessLogic.ReturnAuditHistManager.GetList(term);
            if (auditlist != null && auditlist.Count > 0)
            {
                ESP.Finance.Entity.ReturnAuditHistInfo audit = auditlist[0];
                if (audit.AuditorUserID.Value != Convert.ToInt32(CurrentUser.SysID))
                {
                    audit.Suggestion = auditRemark + "[" + CurrentUser.Name + "为" + audit.AuditorEmployeeName + "的代理人]";
                }
                else
                {
                    audit.Suggestion = auditRemark;
                }
                audit.AuditeStatus = auditstatus;
                audit.AuditeDate = DateTime.Now;
                ESP.Finance.BusinessLogic.ReturnAuditHistManager.Update(audit);
            }
        }

        private static int getAuditor(ESP.Purchase.Entity.GeneralInfo generalModel)
        {
            List<ESP.Purchase.Entity.OrderInfo> orderList = ESP.Purchase.BusinessLogic.OrderInfoManager.GetListByGeneralId(generalModel.id);
            ESP.Purchase.Entity.SupplierInfo supplierModel = null;
            ESP.Purchase.Entity.TypeInfo typeInfo = null;
            foreach (ESP.Purchase.Entity.OrderInfo orderModel in orderList)
            {
                if (orderModel.supplierId > 0)
                {
                    supplierModel = ESP.Purchase.BusinessLogic.SupplierManager.GetModel(orderModel.supplierId);
                    typeInfo = ESP.Purchase.BusinessLogic.TypeManager.GetModel(orderModel.producttype);
                }
            }
            if (supplierModel != null)
            {
                if (supplierModel.supplier_area.IndexOf("北京") != -1)
                {
                    return typeInfo.BJPaymentUserID;
                }
                else if (supplierModel.supplier_area.IndexOf("广州") != -1)
                {
                    return typeInfo.GZPaymentUserID;
                }
                else if (supplierModel.supplier_area.IndexOf("上海") != -1)
                {
                    return typeInfo.SHPaymentUserID;
                }
            }
            return 0;
        }
        #endregion
    }
}
