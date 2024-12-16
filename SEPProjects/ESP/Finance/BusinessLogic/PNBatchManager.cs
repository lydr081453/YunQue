using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESP.Finance.Entity;
using ESP.Finance.Utility;
using Excel = Microsoft.Office.Interop.Excel;
namespace ESP.Finance.BusinessLogic
{
    public static class PNBatchManager
    {
        private static ESP.Finance.IDataAccess.IPNBatchProvider DataProvider { get { return ESP.Configuration.ProviderHelper<ESP.Finance.IDataAccess.IPNBatchProvider>.Instance; } }

        public static int Exist(string batchCode, int batchID)
        {
            return DataProvider.Exist(batchCode, batchID);
        }
        public static int Add(ESP.Finance.Entity.PNBatchInfo model)
        {
            if (model.BatchID == 0)
            {
                // if (Exist(model.BatchCode) == 0)
                return DataProvider.Add(model);
                //else
                //    return 0;
            }
            else
            {
                DataProvider.Update(model);
                return model.BatchID;
            }
        }

        public static int Add(ESP.Finance.Entity.PNBatchInfo model, SqlTransaction trans)
        {
            if (model.BatchID == 0)
            {
                return DataProvider.Add(model, trans);
            }
            else
            {
                DataProvider.Update(model, trans);
                return model.BatchID;
            }
        }

        /// <summary>
        /// 采购审批人驳回批次
        /// </summary>
        /// <param name="BatchID"></param>
        /// <param name="CurrentUser"></param>
        /// <param name="requesition"></param>
        /// <returns></returns>
        public static bool returnBatchForPurchase(int BatchID, ESP.Compatible.Employee CurrentUser, string requesition)
        {
            return DataProvider.returnBatchForPurchase(BatchID, CurrentUser, requesition);
        }

        public static PNBatchInfo AddItems(PNBatchInfo batchModel, string[] itemIds)
        {
            return AddItems(0, batchModel, itemIds);
        }

        public static void AddItems(int batchId, string[] itemIds)
        {
            AddItems(batchId, null, itemIds);
        }

        /// <summary>
        /// 添加批次内的PN项
        /// </summary>
        /// <param name="batchId"></param>
        /// <param name="itemIds"></param>
        /// <param name="CurrentUser"></param>
        public static PNBatchInfo AddItems(int batchId, PNBatchInfo batch, string[] itemIds)
        {

            for (int i = 0; i < itemIds.Count(); i++)
            {
                PNBatchRelationInfo rmodel = PNBatchRelationManager.GetModelByReturnId(int.Parse(itemIds[i]), null);
                if (rmodel != null)
                    return null;
            }
            using (SqlConnection conn = new SqlConnection(DataAccess.DbHelperSQL.connectionString))
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();
                try
                {
                    if (batchId == 0)
                    {
                        //添加批次
                        batchId = DataProvider.Add(batch, trans);
                    }
                    IList<ESP.Finance.Entity.PNBatchRelationInfo> itemList = ESP.Finance.BusinessLogic.PNBatchRelationManager.GetList(" batchID=" + batchId.ToString(), null);
                    List<ESP.Finance.Entity.PNBatchRelationInfo> newItemList = new List<PNBatchRelationInfo>();
                    string originalSupplier = string.Empty;
                    string originalBank = string.Empty;
                    string originalAccount = string.Empty;
                    string originalBranchCode = string.Empty;
                    for (int i = 0; i < itemIds.Length; i++)
                    {
                        bool isHave = false;
                        foreach (ESP.Finance.Entity.PNBatchRelationInfo model in itemList)
                        {
                            if (int.Parse(itemIds[i].ToString()) == model.ReturnID.Value)
                                isHave = true;
                            if (string.IsNullOrEmpty(originalSupplier))
                            {
                                ESP.Finance.Entity.ReturnInfo returnModel = ESP.Finance.BusinessLogic.ReturnManager.GetModel(model.ReturnID.Value);
                                originalSupplier = returnModel.SupplierName;
                                originalBank = returnModel.SupplierBankName;
                                originalAccount = returnModel.SupplierBankAccount;
                                originalBranchCode = returnModel.ProjectCode.Substring(0, 1);
                            }
                        }
                        if (!isHave)
                        {
                            ESP.Finance.Entity.ReturnInfo returnModel = ESP.Finance.BusinessLogic.ReturnManager.GetModel(int.Parse(itemIds[i].ToString()));
                            if (string.IsNullOrEmpty(originalSupplier))
                            {
                                originalSupplier = returnModel.SupplierName;
                                originalBank = returnModel.SupplierBankName;
                                originalAccount = returnModel.SupplierBankAccount;
                                originalBranchCode = returnModel.ProjectCode.Substring(0, 1);
                            }
                            else
                            {
                                if (originalSupplier != returnModel.SupplierName || originalBank != returnModel.SupplierBankName || originalAccount != returnModel.SupplierBankAccount || originalBranchCode != returnModel.ProjectCode.Substring(0, 1))
                                {
                                    throw new Exception("您选择了不同的供应商或分公司申请单");
                                }
                            }
                            ESP.Finance.Entity.PNBatchRelationInfo relationModel = new ESP.Finance.Entity.PNBatchRelationInfo();
                            relationModel.BatchID = batchId;
                            relationModel.ReturnID = int.Parse(itemIds[i].ToString());
                            newItemList.Add(relationModel);
                        }
                    }
                    ESP.Finance.Entity.PNBatchInfo batchModel = DataProvider.GetModel(batchId, trans);
                    batchModel.SupplierName = originalSupplier;
                    batchModel.SupplierBankAccount = originalAccount;
                    batchModel.SupplierBankName = originalBank;
                    DataProvider.Update(batchModel, trans);
                    new DataAccess.PNBatchRelationDataProvider().Add(newItemList, trans);
                    trans.Commit();
                    return batchModel;
                }
                catch (Exception ee)
                {
                    trans.Rollback();
                    throw new Exception(ee.Message);
                }
            }

        }
        public static int BatchTicketCreate(ESP.Finance.Entity.PNBatchInfo model, ArrayList SelectedItems, ESP.Compatible.Employee CurrentUser)
        {

            List<ESP.Finance.Entity.PNBatchRelationInfo> batchList = new List<ESP.Finance.Entity.PNBatchRelationInfo>();
            IList<ESP.Finance.Entity.PNBatchRelationInfo> batchAdded = null;
            int NewBatchID = 0;
            string originalBranchCode = string.Empty;

            if (model.BatchID != 0)
            {
                batchAdded = ESP.Finance.BusinessLogic.PNBatchRelationManager.GetList(" batchID=" + model.BatchID.ToString(), null);
            }
            ESP.Finance.Entity.ReturnInfo firstmodel = ESP.Finance.BusinessLogic.ReturnManager.GetModel(Convert.ToInt32(SelectedItems[0]));
            ESP.Finance.Entity.BranchInfo branchModel = ESP.Finance.BusinessLogic.BranchManager.GetModelByCode(firstmodel.BranchCode);
            ESP.Compatible.Employee financeEmp = new ESP.Compatible.Employee(branchModel.FirstFinanceID);

            originalBranchCode = firstmodel.ProjectCode.Substring(0, 1);

            using (SqlConnection conn = new SqlConnection(DataAccess.DbHelperSQL.connectionString))
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();
                decimal amounts = 0;
                string FirstBrachcode = string.Empty;
                try
                {

                    for (int i = 0; i < SelectedItems.Count; i++)
                    {
                        ESP.Finance.Entity.ReturnInfo returnModel = ESP.Finance.BusinessLogic.ReturnManager.GetModel(Convert.ToInt32(SelectedItems[i]), trans);
                        ESP.Framework.Entity.OperationAuditManageInfo operationModel = ESP.Framework.BusinessLogic.OperationAuditManageManager.GetModelByDepId(returnModel.DepartmentID.Value, trans);

                        if (returnModel.ReturnStatus != (int)ESP.Finance.Utility.PaymentStatus.Ticket_SupplierConfirm)
                        {
                            throw new Exception(returnModel.ReturnCode + "供应商尚未确认价格,请检查!");
                        }
                        FirstBrachcode = returnModel.ProjectCode.Substring(0, 1);
                        if (returnModel.ProjectCode.Substring(0, 1) != FirstBrachcode)
                        {
                            throw new Exception("请选择同一公司代码的申请单!");
                        }
                        if (returnModel.SupplierName != firstmodel.SupplierName)
                        {
                            throw new Exception("请选择同一供应商!");
                        }

                        amounts += returnModel.PreFee.Value;

                        IList<ESP.Finance.Entity.PNBatchRelationInfo> pnlist = ESP.Finance.BusinessLogic.PNBatchRelationManager.GetList(" returnid=" + returnModel.ReturnID.ToString(), new List<SqlParameter>(), trans);
                        if (pnlist != null && pnlist.Count > 0)
                        {
                            throw new Exception(returnModel.ReturnCode + "已经创建批次");
                        }
                        if (i == 0)
                        {
                            model.CreateDate = DateTime.Now;
                            model.CreatorID = Convert.ToInt32(CurrentUser.SysID);
                            model.Creator = CurrentUser.Name;
                            model.PaymentType = returnModel.PaymentTypeName;
                            model.PaymentTypeID = returnModel.PaymentTypeID;
                            model.BranchID = returnModel.BranchID;
                            model.BranchCode = returnModel.BranchCode;
                            model.PaymentUserID = int.Parse(financeEmp.SysID);
                            model.PaymentUserName = financeEmp.ITCode;
                            model.PaymentEmployeeName = financeEmp.Name;
                            model.PaymentCode = financeEmp.ID;


                            model.Status = (int)ESP.Finance.Utility.PaymentStatus.Save;
                            NewBatchID = ESP.Finance.BusinessLogic.PNBatchManager.Add(model, trans);
                        }
                        if (NewBatchID == 0)
                            throw new Exception("您提交的批次号已经存在");
                        if (returnModel.ReturnType != (int)ESP.Purchase.Common.PRTYpe.PrivatePR && returnModel.ReturnType != (int)ESP.Purchase.Common.PRTYpe.MediaPR)
                        {
                            if (originalBranchCode != returnModel.ProjectCode.Substring(0, 1))
                            {
                                throw new Exception("您选择了不同的分公司申请单");
                            }
                        }
                        if (batchAdded != null)
                        {
                            foreach (ESP.Finance.Entity.PNBatchRelationInfo relation in batchAdded)
                            {
                                ESP.Finance.Entity.ReturnInfo rmodel = ESP.Finance.BusinessLogic.ReturnManager.GetModel(relation.ReturnID.Value, trans);
                                if (originalBranchCode != rmodel.ProjectCode.Substring(0, 1))
                                {
                                    throw new Exception("您选择了不同的供应商或分公司申请单");
                                }
                            }
                        }
                        ESP.Finance.Entity.PNBatchRelationInfo relationModel = new ESP.Finance.Entity.PNBatchRelationInfo();
                        relationModel.BatchID = NewBatchID;
                        relationModel.ReturnID = returnModel.ReturnID;
                        batchList.Add(relationModel);
                    }
                    new DataAccess.PNBatchRelationDataProvider().Add(batchList, trans);

                    trans.Commit();

                    ESP.Finance.Entity.PNBatchInfo batchModel = ESP.Finance.BusinessLogic.PNBatchManager.GetModel(NewBatchID);
                    batchModel.Amounts = amounts;
                    ESP.Finance.BusinessLogic.PNBatchManager.Update(batchModel);

                    return NewBatchID;
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    throw new Exception(ex.Message);
                }
            }
        }

        public static int Add(ESP.Finance.Entity.PNBatchInfo model, ArrayList SelectedItems, ESP.Compatible.Employee CurrentUser)
        {
            using (SqlConnection conn = new SqlConnection(DataAccess.DbHelperSQL.connectionString))
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();
                try
                {
                    List<ESP.Finance.Entity.PNBatchRelationInfo> batchList = new List<ESP.Finance.Entity.PNBatchRelationInfo>();
                    IList<ESP.Finance.Entity.PNBatchRelationInfo> batchAdded = null;
                    int NewBatchID = 0;
                    string originalSupplier = string.Empty;
                    string originalBank = string.Empty;
                    string originalAccount = string.Empty;
                    string originalBranchCode = string.Empty;
                    decimal amounts = 0;
                    if (model.BatchID != 0)
                    {
                        batchAdded = ESP.Finance.BusinessLogic.PNBatchRelationManager.GetList(" batchID=" + model.BatchID.ToString(), null);
                    }

                    for (int i = 0; i < SelectedItems.Count; i++)
                    {
                        ESP.Finance.Entity.ReturnInfo returnModel = ESP.Finance.BusinessLogic.ReturnManager.GetModel(Convert.ToInt32(SelectedItems[i]));
                        IList<ESP.Finance.Entity.PNBatchRelationInfo> pnlist = ESP.Finance.BusinessLogic.PNBatchRelationManager.GetList(" returnid=" + returnModel.ReturnID.ToString(), null);
                        if (pnlist != null && pnlist.Count > 0)
                        {
                            throw new Exception(returnModel.ReturnCode + "已经创建批次");
                        }
                        if (i == 0)
                        {
                            model.CreateDate = DateTime.Now;
                            model.CreatorID = Convert.ToInt32(CurrentUser.SysID);
                            model.Creator = CurrentUser.Name;
                            model.SupplierName = returnModel.SupplierName;
                            model.SupplierBankAccount = returnModel.SupplierBankAccount;
                            model.SupplierBankName = returnModel.SupplierBankName;
                            model.PaymentType = returnModel.PaymentTypeName;
                            model.PaymentTypeID = returnModel.PaymentTypeID;

                            model.PaymentUserID = returnModel.PaymentUserID;
                            model.PaymentUserName = returnModel.PaymentUserName;
                            model.PaymentEmployeeName = returnModel.PaymentEmployeeName;
                            model.PaymentCode = returnModel.PaymentCode;

                            originalSupplier = returnModel.SupplierName;
                            originalBank = returnModel.SupplierBankName;
                            originalAccount = returnModel.SupplierBankAccount;
                            originalBranchCode = returnModel.ProjectCode.Substring(0, 1);

                            model.Status = returnModel.ReturnStatus;

                        }
                        //if (NewBatchID == 0)
                        //    throw new Exception("您提交的批次号已经存在");
                        if (returnModel.ReturnType != (int)ESP.Purchase.Common.PRTYpe.PrivatePR && returnModel.ReturnType != (int)ESP.Purchase.Common.PRTYpe.MediaPR)
                        {
                            if (originalSupplier != returnModel.SupplierName || originalBank != returnModel.SupplierBankName || originalAccount != returnModel.SupplierBankAccount || originalBranchCode != returnModel.ProjectCode.Substring(0, 1))
                            {
                                throw new Exception("您选择了不同的供应商或分公司申请单");
                            }
                        }
                        if (batchAdded != null)
                        {
                            foreach (ESP.Finance.Entity.PNBatchRelationInfo relation in batchAdded)
                            {
                                ESP.Finance.Entity.ReturnInfo rmodel = ESP.Finance.BusinessLogic.ReturnManager.GetModel(relation.ReturnID.Value);
                                if (originalSupplier != rmodel.SupplierName || originalBank != rmodel.SupplierBankName || originalAccount != rmodel.SupplierBankAccount || originalBranchCode != rmodel.ProjectCode.Substring(0, 1))
                                {
                                    throw new Exception("您选择了不同的供应商或分公司申请单");
                                }
                            }
                        }
                        ESP.Finance.Entity.PNBatchRelationInfo relationModel = new ESP.Finance.Entity.PNBatchRelationInfo();
                        relationModel.ReturnID = returnModel.ReturnID;
                        batchList.Add(relationModel);
                        amounts += returnModel.PreFee.Value;
                    }
                    model.Amounts = amounts;
                    NewBatchID = ESP.Finance.BusinessLogic.PNBatchManager.Add(model, trans);
                    foreach (ESP.Finance.Entity.PNBatchRelationInfo r in batchList)
                    {
                        r.BatchID = NewBatchID;
                    }
                    new DataAccess.PNBatchRelationDataProvider().Add(batchList, trans);
                    trans.Commit();
                    return NewBatchID;
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    throw new Exception(ex.Message);
                }
            }
        }

        public static decimal GetTotalAmounts(ESP.Finance.Entity.PNBatchInfo batchModel)
        {
            decimal totalAmounts = 0;
            IList<ESP.Finance.Entity.PNBatchRelationInfo> relationList = ESP.Finance.BusinessLogic.PNBatchRelationManager.GetList(" batchid=" + batchModel.BatchID.ToString(), null);
            foreach (ESP.Finance.Entity.PNBatchRelationInfo relation in relationList)
            {
                ESP.Finance.Entity.ReturnInfo returnModel = ESP.Finance.BusinessLogic.ReturnManager.GetModel(relation.ReturnID.Value);
                if (returnModel != null)
                {
                    if (returnModel.FactFee != null && returnModel.FactFee.Value != 0)
                        totalAmounts += returnModel.FactFee.Value;
                    else
                        totalAmounts += returnModel.PreFee.Value;
                }
            }
            return totalAmounts;
        }

        public static decimal GetTotalAmounts(int batchId, SqlTransaction trans)
        {
            decimal totalAmounts = 0;
            IList<ESP.Finance.Entity.PNBatchRelationInfo> relationList = ESP.Finance.BusinessLogic.PNBatchRelationManager.GetList(" batchid=" + batchId.ToString(), new List<SqlParameter>(), trans);
            foreach (ESP.Finance.Entity.PNBatchRelationInfo relation in relationList)
            {
                ESP.Finance.Entity.ReturnInfo returnModel = ESP.Finance.BusinessLogic.ReturnManager.GetModel(relation.ReturnID.Value, trans);
                if (returnModel != null)
                {
                    if (returnModel.FactFee != null && returnModel.FactFee.Value != 0)
                        totalAmounts += returnModel.FactFee.Value;
                    else
                        totalAmounts += returnModel.PreFee.Value;
                }
            }
            return totalAmounts;
        }

        public static Dictionary<int, ESP.Triplet<int, int, decimal>> GetTotalAmounts(int[] batchIds)
        {

            Dictionary<int, ESP.Triplet<int, int, decimal>> dict = new Dictionary<int, ESP.Triplet<int, int, decimal>>();
            if (batchIds == null || batchIds.Length == 0)
                return dict;

            int start = 0;

            while (start < batchIds.Length)
            {
                int count = batchIds.Length - start;
                if (count > 200)
                    count = 200;

                StringBuilder sql = new StringBuilder(@"
select br.BatchID, r.FactFee, r.PreFee
from F_Return r join F_PNBatchRelation br on r.ReturnID = br.ReturnID
where br.BatchID in (
");
                sql.Append(batchIds[start]);
                for (var i = 1; i < count; i++)
                {
                    sql.Append(',').Append(batchIds[start + i]);
                }
                sql.Append(@"
)
");
                ESP.Triplet<int, int, decimal> item;
                using (var reader = ESP.Finance.DataAccess.DbHelperSQL.ExecuteReader(sql.ToString()))
                {
                    while (reader.Read())
                    {
                        var bid = reader.GetInt32(0);
                        var fee = reader.IsDBNull(1) ? (reader.IsDBNull(2) ? 0 : reader.GetDecimal(2)) : reader.GetDecimal(1);

                        if (dict.TryGetValue(bid, out item))
                        {
                            item.Second++;
                            item.Third += fee;
                        }
                        else
                        {
                            item = new Triplet<int, int, decimal>();
                            item.First = bid;
                            item.Second = 1;
                            item.Third = fee;
                            dict.Add(bid, item);
                        }
                    }
                }

                start += count;
            }

            return dict;
        }

        public static bool CheckBatchCode(string BatchCode)
        {
            string term = " batchcode=@batchcode";
            List<System.Data.SqlClient.SqlParameter> paramList = new List<SqlParameter>();
            SqlParameter p = new SqlParameter("@batchcode", SqlDbType.NVarChar, 50);
            p.Value = BatchCode;
            paramList.Add(p);
            IList<ESP.Finance.Entity.PNBatchInfo> batchList = ESP.Finance.BusinessLogic.PNBatchManager.GetList(term, paramList);
            if (batchList != null && batchList.Count > 0)
                return true;
            else
                return false;
        }

        public static bool CheckBatchCode(string BatchCode, int batchID)
        {
            string term = " batchcode=@batchcode and BatchID != @batchID";
            List<System.Data.SqlClient.SqlParameter> paramList = new List<SqlParameter>();
            SqlParameter p = new SqlParameter("@batchcode", SqlDbType.NVarChar, 50);
            p.Value = BatchCode;
            paramList.Add(p);
            p = new SqlParameter("@batchID", SqlDbType.Int, 4);
            p.Value = batchID;
            paramList.Add(p);
            IList<ESP.Finance.Entity.PNBatchInfo> batchList = ESP.Finance.BusinessLogic.PNBatchManager.GetList(term, paramList);
            if (batchList != null && batchList.Count > 0)
                return true;
            else
                return false;
        }

        public static int Update(ESP.Finance.Entity.PNBatchInfo model)
        {
            return DataProvider.Update(model);
        }

        public static int Update(ESP.Finance.Entity.PNBatchInfo model, SqlTransaction trans)
        {
            return DataProvider.Update(model, trans);
        }

        public static int UpdateAmounts(ESP.Finance.Entity.PNBatchInfo model, SqlTransaction trans)
        {
            return DataProvider.UpdateAmounts(model, trans);
        }

        private static void SaveHistoryForPurchase(ESP.Finance.Entity.ReturnInfo returnModel, ESP.Finance.Entity.PNBatchInfo batchModel, ESP.Compatible.Employee CurrentUser, int status, int currentAuditType, int nextAuditType, SqlTransaction trans, string suggestion)
        {
            string term = "";
            List<SqlParameter> paramlist = new List<SqlParameter>();
            IList<ESP.Finance.Entity.ReturnAuditHistInfo> auditList;
            //查询审批历史中是否存在当前审批人
            string DelegateUsers = string.Empty;
            string DelegateUserNames = string.Empty;
            IList<ESP.Framework.Entity.AuditBackUpInfo> delegates = ESP.Framework.BusinessLogic.AuditBackUpManager.GetModelsByBackUpUserID(Convert.ToInt32(CurrentUser.SysID));
            foreach (ESP.Framework.Entity.AuditBackUpInfo model in delegates)
            {
                DelegateUsers += model.UserID.ToString() + ",";
                DelegateUserNames += new ESP.Compatible.Employee(model.UserID).Name;
            }
            DelegateUsers = DelegateUsers.TrimEnd(',');
            DelegateUserNames = DelegateUserNames.TrimEnd(',');
            if (!string.IsNullOrEmpty(DelegateUsers))
                term = " AuditType=@AuditType and (AuditorUserID=@AuditorUserID or AuditorUserID in(" + DelegateUsers + "))";
            else
                term = " AuditType=@AuditType and AuditorUserID=@AuditorUserID";
            term += " and returnID=@ReturnID and auditeStatus=" + (int)ESP.Finance.Utility.AuditHistoryStatus.UnAuditing;
            SqlParameter p1 = new SqlParameter("@AuditType", SqlDbType.Int, 4);
            p1.Value = currentAuditType;
            paramlist.Add(p1);
            SqlParameter p2 = new SqlParameter("@AuditorUserID", SqlDbType.Int, 4);
            p2.Value = CurrentUser.SysID;
            paramlist.Add(p2);
            SqlParameter p3 = new SqlParameter("@ReturnID", SqlDbType.Int, 4);
            p3.Value = returnModel.ReturnID;
            paramlist.Add(p3);
            auditList = ESP.Finance.BusinessLogic.ReturnAuditHistManager.GetList(term, paramlist, trans);
            ReturnAuditHistInfo auditHist;
            if (auditList.Count == 0)
            {//如果不存在新建一个历史记录
                auditHist = new ReturnAuditHistInfo();
                auditHist.ReturnID = returnModel.ReturnID;
                auditHist.AuditorUserID = int.Parse(CurrentUser.SysID);
                auditHist.AuditeDate = DateTime.Now;
                auditHist.AuditeStatus = status;
                auditHist.Suggestion = suggestion;
                auditHist.AuditorUserCode = CurrentUser.ID;
                auditHist.AuditorUserName = CurrentUser.ITCode;
                auditHist.AuditorEmployeeName = CurrentUser.Name;
                auditHist.AuditType = currentAuditType;
                new DataAccess.ReturnAuditHistDataProvider().Add(auditHist, trans);

                //if (ESP.Configuration.ConfigurationManager.SafeAppSettings["AuditorId"].ToString().ToLower() == CurrentUser.SysID)//daivd.duan专用
                //{
                //    //删除采购部人员未审核信息
                //    new DataAccess.ReturnAuditHistDataProvider().DeleteNotAudit(returnModel.ReturnID, trans);
                //}
            }
            else
            {//否则更新审批历史
                auditHist = auditList[0];
                //auditHist.AuditorUserID = int.Parse(CurrentUser.SysID);
                //auditHist.AuditorUserCode = CurrentUser.ID;
                //auditHist.AuditorUserName = CurrentUser.ITCode;
                //auditHist.AuditorEmployeeName = CurrentUser.Name;
                auditHist.AuditeDate = DateTime.Now;
                auditHist.AuditeStatus = status;
                auditHist.Suggestion = suggestion;
                new DataAccess.ReturnAuditHistDataProvider().Update(auditHist, trans);
            }
            term = " AuditType=@AuditType and AuditorUserID=@AuditorUserID and returnID=@ReturnID and auditeStatus=" + (int)ESP.Finance.Utility.AuditHistoryStatus.UnAuditing;
            paramlist.Clear();
            p1 = new SqlParameter("@AuditType", SqlDbType.Int, 4);
            p1.Value = nextAuditType;
            paramlist.Add(p1);
            p2 = new SqlParameter("@AuditorUserID", SqlDbType.Int, 4);
            p2.Value = batchModel.PaymentUserID;
            paramlist.Add(p2);
            p3 = new SqlParameter("@ReturnID", SqlDbType.Int, 4);
            p3.Value = returnModel.ReturnID;
            paramlist.Add(p3);

            if (batchModel.PaymentUserID != 0 && returnModel.ReturnStatus != (int)ESP.Finance.Utility.PaymentStatus.Save)
            {
                auditList = ESP.Finance.BusinessLogic.ReturnAuditHistManager.GetList(term, paramlist, trans);
                if (auditList.Count == 0)
                {
                    ReturnAuditHistInfo NextAuditHist = new ReturnAuditHistInfo();
                    ESP.Framework.Entity.EmployeeInfo emp = ESP.Framework.BusinessLogic.EmployeeManager.Get(batchModel.PaymentUserID.Value);
                    NextAuditHist.ReturnID = returnModel.ReturnID;
                    NextAuditHist.AuditorUserID = emp.UserID;
                    NextAuditHist.AuditorUserName = emp.Username;
                    NextAuditHist.AuditeStatus = (int)ESP.Finance.Utility.AuditHistoryStatus.UnAuditing;
                    NextAuditHist.AuditorUserCode = emp.Code;
                    NextAuditHist.AuditorEmployeeName = emp.FullNameCN;
                    NextAuditHist.AuditType = nextAuditType;
                    new DataAccess.ReturnAuditHistDataProvider().Add(NextAuditHist, trans);
                }
            }
        }

        private static void SaveHistory(ESP.Finance.Entity.ReturnInfo returnModel, ESP.Finance.Entity.PNBatchInfo batchModel, ESP.Compatible.Employee CurrentUser, int status, SqlTransaction trans)
        {
            string term = " AuditType=@AuditType and AuditorUserID=@AuditorUserID and returnID=@ReturnID ";

            var operationModel = ESP.Framework.BusinessLogic.OperationAuditManageManager.GetModelByDepId(returnModel.DepartmentID.Value);

            int auditorType = 0;

            List<SqlParameter> paramlist = new List<SqlParameter>();

            SqlParameter p1 = new SqlParameter("@AuditType", SqlDbType.Int, 4);

            // if (status == (int)ESP.Finance.Utility.AuditHistoryStatus.PassAuditing)

            if (returnModel.ReturnStatus == (int)ESP.Finance.Utility.PaymentStatus.PurchaseFirst)
                auditorType = (int)ESP.Finance.Utility.auditorType.purchase_first;
            else
                auditorType = (int)ESP.Finance.Utility.auditorType.operationAudit_Type_Financial;
            p1.Value = auditorType;
            paramlist.Add(p1);

            SqlParameter p2 = new SqlParameter("@AuditorUserID", SqlDbType.Int, 4);
            p2.Value = returnModel.PaymentUserID;
            paramlist.Add(p2);
            SqlParameter p3 = new SqlParameter("@ReturnID", SqlDbType.Int, 4);
            p3.Value = returnModel.ReturnID;
            paramlist.Add(p3);
            IList<ESP.Finance.Entity.ReturnAuditHistInfo> auditList;
            if (batchModel.PaymentUserID != 0 && returnModel.ReturnStatus != (int)ESP.Finance.Utility.PaymentStatus.MajorAudit && returnModel.ReturnStatus != (int)ESP.Finance.Utility.PaymentStatus.Save)
            {
                auditList = ESP.Finance.BusinessLogic.ReturnAuditHistManager.GetList(term, paramlist, trans);
                if (auditList.Count == 0)
                {
                    ReturnAuditHistInfo NextAuditHist = new ReturnAuditHistInfo();
                    ESP.Framework.Entity.EmployeeInfo emp = ESP.Framework.BusinessLogic.EmployeeManager.Get(batchModel.PaymentUserID.Value);
                    NextAuditHist.ReturnID = returnModel.ReturnID;
                    NextAuditHist.AuditorUserID = emp.UserID;
                    NextAuditHist.AuditorUserName = emp.Username;
                    NextAuditHist.AuditeStatus = (int)ESP.Finance.Utility.AuditHistoryStatus.UnAuditing;
                    NextAuditHist.AuditorUserCode = emp.Code;
                    NextAuditHist.AuditorEmployeeName = emp.FullNameCN;
                    NextAuditHist.AuditType = auditorType;
                    new DataAccess.ReturnAuditHistDataProvider().Add(NextAuditHist, trans);
                }
            }
            //查询审批历史中是否存在当前审批人
            paramlist.Clear();
            // paramlist.Add(p1);
            p2 = new SqlParameter("@AuditorUserID", SqlDbType.Int, 4);
            p2.Value = CurrentUser.SysID;
            paramlist.Add(p2);
            paramlist.Add(p3);

            term = " AuditorUserID=@AuditorUserID and returnID=@ReturnID ";

            auditList = ESP.Finance.BusinessLogic.ReturnAuditHistManager.GetList(term, paramlist, trans);
            ReturnAuditHistInfo auditHist;
            if (auditList.Count == 0)
            {//如果不存在新建一个历史记录
                auditHist = new ReturnAuditHistInfo();
                auditHist.ReturnID = returnModel.ReturnID;
                auditHist.AuditorUserID = int.Parse(CurrentUser.SysID);
                auditHist.AuditeDate = DateTime.Now;
                auditHist.AuditeStatus = status;
                auditHist.Suggestion = batchModel.Description;
                auditHist.AuditorUserCode = CurrentUser.ID;
                auditHist.AuditorUserName = CurrentUser.ITCode;
                auditHist.AuditorEmployeeName = CurrentUser.Name;

                int defaultAuditor = operationModel.PurchaseDirectorId;

                if (defaultAuditor == 0)
                {
                    defaultAuditor = int.Parse(ESP.Configuration.ConfigurationManager.SafeAppSettings["AuditorId"]);
                }
                if (defaultAuditor.ToString() == CurrentUser.SysID)//daivd.duan专用
                {
                    //删除采购部人员未审核信息
                    new DataAccess.ReturnAuditHistDataProvider().DeleteNotAudit(returnModel.ReturnID, trans);
                }
                if (returnModel.ReturnStatus == (int)ESP.Finance.Utility.PaymentStatus.PurchaseFirst)
                    auditHist.AuditType = ESP.Finance.Utility.auditorType.purchase_major2;
                else
                    auditHist.AuditType = ESP.Finance.Utility.auditorType.operationAudit_Type_Financial;
                new DataAccess.ReturnAuditHistDataProvider().Add(auditHist, trans);
            }
            else
            {//否则更新审批历史
                auditHist = auditList[0];
                auditHist.AuditorUserCode = CurrentUser.ID;
                auditHist.AuditorUserName = CurrentUser.ITCode;
                auditHist.AuditorEmployeeName = CurrentUser.Name;
                auditHist.AuditeDate = DateTime.Now;
                auditHist.AuditeStatus = status;
                auditHist.Suggestion = batchModel.Description;
                new DataAccess.ReturnAuditHistDataProvider().Update(auditHist, trans);
            }

            if (returnModel.ReturnStatus == (int)ESP.Finance.Utility.PaymentStatus.MajorAudit)
            {//驳回到财务第一级
                term = " AuditType=@AuditType and ReturnID=@ReturnID";
                paramlist.Clear();
                p1 = new SqlParameter("@AuditType", SqlDbType.Int, 4);
                p1.Value = (int)ESP.Finance.Utility.auditorType.operationAudit_Type_Financial;
                paramlist.Add(p1);
                p2 = new SqlParameter("@ReturnID", SqlDbType.Int, 4);
                p2.Value = returnModel.ReturnID;
                paramlist.Add(p2);
                auditList = ESP.Finance.BusinessLogic.ReturnAuditHistManager.GetList(term, paramlist, trans);
                foreach (ESP.Finance.Entity.ReturnAuditHistInfo model in auditList)
                {
                    model.Suggestion = string.Empty;
                    model.AuditeStatus = (int)ESP.Finance.Utility.AuditHistoryStatus.UnAuditing;
                    new DataAccess.ReturnAuditHistDataProvider().Update(model, trans);
                }
            }
            //else if (returnModel.ReturnStatus == (int)ESP.Finance.Utility.PaymentStatus.PurchaseFirst)
            //{ 
            //    string terms=" audittype in({0},{1},{2})";
            //    terms = string.Format(terms, (int)ESP.Finance.Utility.auditorType.purchase_major1, (int)ESP.Finance.Utility.auditorType.purchase_major2, (int)ESP.Finance.Utility.auditorType.operationAudit_Type_Financial);
            //    ESP.Finance.BusinessLogic.ReturnAuditHistManager.DeleteByReturnID(returnModel.ReturnID, terms, null);
            //    terms = " returnID={0} and auditType={1}";
            //    terms = string.Format(terms,returnModel.ReturnID,(int)ESP.Finance.Utility.auditorType.purchase_first);
            //    IList<ESP.Finance.Entity.ReturnAuditHistInfo> histList = ESP.Finance.BusinessLogic.ReturnAuditHistManager.GetList(terms);
            //    if (histList != null && histList.Count > 0)
            //    {
            //        histList[0].AuditeStatus = (int)ESP.Finance.Utility.AuditHistoryStatus.UnAuditing;
            //        histList[0].Suggestion = string.Empty;
            //        ESP.Finance.BusinessLogic.ReturnAuditHistManager.Update(histList[0]);
            //    }
            //}
        }

        public static int BatchAudit(ESP.Finance.Entity.PNBatchInfo model, List<int> mediaList, string mediaorderIDs, ESP.Compatible.Employee CurrentUser, int status, bool isSave)
        {
            return BatchAudit(model, mediaList, mediaorderIDs, CurrentUser, status, false, isSave);
        }

        public static int BatchAudit(ESP.Finance.Entity.PNBatchInfo model, List<int> mediaList, string mediaorderIDs, ESP.Compatible.Employee CurrentUser, int status, bool isDeleteBatch, bool isSave)
        {
            IList<ESP.Finance.Entity.PNBatchRelationInfo> relationList = ESP.Finance.BusinessLogic.PNBatchRelationManager.GetList(" BatchID=" + model.BatchID.ToString(), null);

            ESP.Finance.Entity.ReturnInfo firstReturnModel = ESP.Finance.BusinessLogic.ReturnManager.GetModel(relationList[0].ReturnID.Value);

            if (firstReturnModel.ReturnType == (int)ESP.Purchase.Common.PRTYpe.PN_AirTicket && model.Status == (int)ESP.Finance.Utility.PaymentStatus.PurchaseFirst)
            {
                ESP.Framework.Entity.OperationAuditManageInfo operationModel = ESP.Framework.BusinessLogic.OperationAuditManageManager.GetModelByDepId(firstReturnModel.DepartmentID.Value);
                ESP.Compatible.Employee purchaseEmp = new ESP.Compatible.Employee(operationModel.TicketPurchaseId);
                model.PaymentUserID = int.Parse(purchaseEmp.SysID);
                model.PaymentUserName = purchaseEmp.ITCode;
                model.PaymentEmployeeName = purchaseEmp.Name;
                model.PaymentCode = purchaseEmp.ID;
            }

            using (SqlConnection conn = new SqlConnection(DataAccess.DbHelperSQL.connectionString))
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();
                try
                {
                    DataProvider.Update(model, trans);

                    if (isSave == false)
                    {
                        ESP.Finance.Entity.AuditLogInfo logModel = new AuditLogInfo();
                        logModel.AuditDate = DateTime.Now;
                        logModel.AuditorEmployeeName = CurrentUser.Name;
                        logModel.AuditorSysID = Convert.ToInt32(CurrentUser.SysID);
                        logModel.AuditorUserCode = CurrentUser.ID;
                        logModel.AuditorUserName = CurrentUser.ITCode;
                        logModel.AuditStatus = status;
                        logModel.FormID = model.BatchID;
                        logModel.FormType = (int)ESP.Finance.Utility.FormType.PNBatch;
                        logModel.Suggestion = model.Description;
                        new DataAccess.AuditLogDataProvider().Add(logModel, trans);
                    }
                    if (relationList != null && relationList.Count > 0)
                    {
                        foreach (ESP.Finance.Entity.PNBatchRelationInfo rmodel in relationList)
                        {
                            ESP.Finance.Entity.ReturnInfo returnModel = ESP.Finance.BusinessLogic.ReturnManager.GetModel(rmodel.ReturnID.Value, trans);


                            returnModel.IsInvoice = model.IsInvoice;
                            returnModel.CreditCode = model.BatchCode;
                            if (returnModel.FactFee == null || returnModel.FactFee.Value == 0)
                                returnModel.FactFee = returnModel.PreFee;
                            returnModel.ReturnPreDate = model.PaymentDate;


                            returnModel.BankID = model.BankID;
                            returnModel.BankName = model.BankName;
                            returnModel.BranchCode = model.BranchCode;
                            returnModel.BranchID = model.BranchID;
                            returnModel.DBCode = model.DBCode;
                            returnModel.DBManager = model.DBManager;
                            returnModel.BankAccount = model.BankAccount;
                            returnModel.BankAccountName = model.BankAccountName;
                            returnModel.BankAddress = model.BankAddress;
                            returnModel.Remark = model.Description;
                            returnModel.PaymentTypeCode = model.BatchCode;
                            returnModel.PaymentUserID = model.PaymentUserID;
                            returnModel.PaymentUserName = model.PaymentUserName;
                            returnModel.PaymentEmployeeName = model.PaymentEmployeeName;
                            returnModel.PaymentCode = model.PaymentCode;

                            returnModel.PaymentTypeID = model.PaymentTypeID;
                            returnModel.PaymentTypeName = model.PaymentType;
                            returnModel.ReturnStatus = model.Status;
                            if (isSave == false)
                                SaveHistory(returnModel, model, CurrentUser, status, trans);
                            //if (model.Status == (int)ESP.Finance.Utility.PaymentStatus.FinanceComplete)
                            //{
                            //    returnModel.ReturnFactDate = DateTime.Now;
                            //}
                            ESP.Finance.BusinessLogic.ReturnManager.Update(returnModel, trans);
                            if (isSave == false)
                            {
                                ESP.Finance.Entity.AuditLogInfo returnlogModel = new AuditLogInfo();
                                returnlogModel.AuditDate = DateTime.Now;
                                returnlogModel.AuditorEmployeeName = CurrentUser.Name;
                                returnlogModel.AuditorSysID = Convert.ToInt32(CurrentUser.SysID);
                                returnlogModel.AuditorUserCode = CurrentUser.ID;
                                returnlogModel.AuditorUserName = CurrentUser.ITCode;
                                returnlogModel.AuditStatus = status;
                                returnlogModel.FormID = returnModel.ReturnID;
                                returnlogModel.FormType = (int)ESP.Finance.Utility.FormType.Return;
                                returnlogModel.Suggestion = model.Description;
                                new DataAccess.AuditLogDataProvider().Add(returnlogModel, trans);
                            }
                            if (isDeleteBatch)
                            {
                                ESP.Finance.BusinessLogic.PNBatchManager.Delete(model.BatchID, trans);
                            }
                            ESP.Purchase.BusinessLogic.MediaOrderManager.UpdateMediaIsPayment(mediaList, mediaorderIDs, Convert.ToInt32(CurrentUser.SysID), trans);
                        }
                    }
                    trans.Commit();

                }
                catch
                {
                    trans.Rollback();
                    return 0;
                }

                if (model.Status == (int)ESP.Finance.Utility.PaymentStatus.FinanceComplete)
                {
                    foreach (ESP.Finance.Entity.PNBatchRelationInfo rmodel in relationList)
                    {
                        ESP.Finance.Entity.ReturnInfo returnModel = ESP.Finance.BusinessLogic.ReturnManager.GetModel(rmodel.ReturnID.Value);
                        ESP.Framework.Entity.EmployeeInfo emp = ESP.Framework.BusinessLogic.EmployeeManager.Get(returnModel.RequestorID.Value);
                        try
                        {
                            SendMailHelper.SendMailReturnComplete(returnModel, returnModel.RequestUserName, emp.InternalEmail);
                        }
                        catch
                        {

                        }
                    }
                }
                return 1;
            }
        }
        /// <summary>
        /// 采购批次审批通过
        /// </summary>
        /// <param name="operationModel"></param>
        /// <param name="oldBatchModel"></param>
        /// <param name="model"></param>
        /// <param name="CurrentUser"></param>
        /// <param name="status"></param>
        /// <param name="currentAduitType"></param>
        /// <param name="nextAuditType"></param>
        /// <returns></returns>
        public static int BatchAuditForPurchase(ESP.Finance.Entity.PNBatchInfo oldBatchModel, ESP.Finance.Entity.PNBatchInfo model, ESP.Compatible.Employee CurrentUser, int status, int currentAduitType, int nextAuditType)
        {
            // ESP.Compatible.Employee empDirector = new ESP.Compatible.Employee(operationModel.PurchaseDirectorId);

            using (SqlConnection conn = new SqlConnection(DataAccess.DbHelperSQL.connectionString))
            {
                string suggestion = string.Empty;
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();
                try
                {
                    DataProvider.Update(model, trans);
                    ESP.Finance.Entity.AuditLogInfo logModel = new AuditLogInfo();
                    logModel.AuditDate = DateTime.Now;
                    logModel.AuditorEmployeeName = CurrentUser.Name;
                    logModel.AuditorSysID = Convert.ToInt32(CurrentUser.SysID);
                    logModel.AuditorUserCode = CurrentUser.ID;
                    logModel.AuditorUserName = CurrentUser.ITCode;
                    logModel.AuditStatus = status;
                    logModel.FormID = model.BatchID;
                    logModel.FormType = (int)ESP.Finance.Utility.FormType.PNBatch;
                    //if (int.Parse(CurrentUser.SysID) != operationModel.PurchaseDirectorId && (oldBatchModel != null && oldBatchModel.Status == (int)ESP.Finance.Utility.PaymentStatus.PurchaseMajor1))
                    //{
                    //    suggestion = model.Description + "[" + CurrentUser.Name + "为" + operationModel.PurchaseDirector + "的代理人]";
                    //}
                    //else
                    suggestion = model.Description;
                    logModel.Suggestion = suggestion;
                    new DataAccess.AuditLogDataProvider().Add(logModel, trans);
                    IList<ESP.Finance.Entity.PNBatchRelationInfo> relationList = ESP.Finance.BusinessLogic.PNBatchRelationManager.GetList(" BatchID=" + model.BatchID.ToString(), null);
                    if (relationList != null && relationList.Count > 0)
                    {
                        foreach (ESP.Finance.Entity.PNBatchRelationInfo rmodel in relationList)
                        {
                            ESP.Finance.Entity.ReturnInfo returnModel = ESP.Finance.BusinessLogic.ReturnManager.GetModel(rmodel.ReturnID.Value);
                            returnModel.IsInvoice = model.IsInvoice;
                            returnModel.CreditCode = model.BatchCode;
                            if (returnModel.FactFee == null || returnModel.FactFee.Value == 0)
                                returnModel.FactFee = returnModel.PreFee;
                            returnModel.SupplierName = model.SupplierName;
                            returnModel.SupplierBankName = model.SupplierBankName;
                            returnModel.SupplierBankAccount = model.SupplierBankAccount;
                            returnModel.ReturnPreDate = model.PaymentDate;

                            returnModel.BankID = model.BankID;
                            returnModel.BankName = model.BankName;
                            returnModel.BranchCode = model.BranchCode;
                            returnModel.BranchID = model.BranchID;
                            returnModel.DBCode = model.DBCode;
                            returnModel.DBManager = model.DBManager;
                            returnModel.BankAccount = model.BankAccount;
                            returnModel.BankAccountName = model.BankAccountName;
                            returnModel.BankAddress = model.BankAddress;
                            returnModel.Remark = model.Description;

                            returnModel.PaymentUserID = model.PaymentUserID;
                            returnModel.PaymentUserName = model.PaymentUserName;
                            returnModel.PaymentEmployeeName = model.PaymentEmployeeName;
                            returnModel.PaymentCode = model.PaymentCode;

                            returnModel.PaymentTypeID = model.PaymentTypeID;
                            returnModel.PaymentTypeName = model.PaymentType;
                            returnModel.ReturnStatus = model.Status;
                            SaveHistoryForPurchase(returnModel, model, CurrentUser, status, currentAduitType, nextAuditType, trans, suggestion);
                            ESP.Finance.BusinessLogic.ReturnManager.Update(returnModel, trans);

                            //增加日志到每一条付款申请
                            ESP.Finance.Entity.AuditLogInfo returnlog = new AuditLogInfo();
                            returnlog.AuditDate = DateTime.Now;
                            returnlog.AuditorEmployeeName = CurrentUser.Name;
                            returnlog.AuditorSysID = Convert.ToInt32(CurrentUser.SysID);
                            returnlog.AuditorUserCode = CurrentUser.ID;
                            returnlog.AuditorUserName = CurrentUser.ITCode;
                            returnlog.AuditStatus = status;
                            returnlog.FormID = returnModel.ReturnID;
                            returnlog.FormType = (int)ESP.Finance.Utility.FormType.Return;
                            returnlog.Suggestion = suggestion;
                            new DataAccess.AuditLogDataProvider().Add(returnlog, trans);
                        }
                    }
                    trans.Commit();
                    return 1;
                }
                catch
                {
                    trans.Rollback();
                    return 0;
                }
            }
        }

        public static int Delete(int BatchID)
        {
            using (SqlConnection conn = new SqlConnection(DataAccess.DbHelperSQL.connectionString))
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();
                try
                {
                    int ret = DataProvider.Delete(BatchID, trans);
                    ret = ESP.Finance.BusinessLogic.PNBatchRelationManager.DeleteByBatchID(BatchID, trans);
                    trans.Commit();
                    return ret;
                }
                catch
                {
                    trans.Rollback();
                    return 0;
                }
            }

        }

        public static int DeleteConsumption(int BatchID)
        {
            using (SqlConnection conn = new SqlConnection(DataAccess.DbHelperSQL.connectionString))
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();
                try
                {
                    int ret = DataProvider.Delete(BatchID, trans);
                    ret = ESP.Finance.BusinessLogic.ConsumptionManager.DeleteByBatch(BatchID, trans);
                    trans.Commit();
                    return ret;
                }
                catch
                {
                    trans.Rollback();
                    return 0;
                }
                finally
                {
                    if (conn != null && conn.State != ConnectionState.Closed)
                    {
                        conn.Close();
                    }
                }
            }

        }

        public static int DeleteRebateRegistration(int BatchID)
        {
            using (SqlConnection conn = new SqlConnection(DataAccess.DbHelperSQL.connectionString))
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();
                try
                {
                    int ret = DataProvider.Delete(BatchID, trans);
                    ret = ESP.Finance.BusinessLogic.RebateRegistrationManager.DeleteByBatch(BatchID, trans);
                    trans.Commit();
                    return ret;
                }
                catch
                {
                    trans.Rollback();
                    return 0;
                }
                finally
                {
                    if (conn != null && conn.State != ConnectionState.Closed)
                    {
                        conn.Close();
                    }
                }
            }

        }
        public static int Delete(int BatchID, SqlTransaction trans)
        {
            int ret = DataProvider.Delete(BatchID, trans);
            ret = new DataAccess.PNBatchRelationDataProvider().DeleteByBatchID(BatchID, trans);
            return ret;
        }

        public static ESP.Finance.Entity.PNBatchInfo GetModel(int BatchID)
        {
            return DataProvider.GetModel(BatchID);
        }
        public static ESP.Finance.Entity.PNBatchInfo GetModel(int BatchID, SqlTransaction trans)
        {
            return DataProvider.GetModel(BatchID, trans);
        }
        public static ESP.Finance.Entity.PNBatchInfo GetModelByBatchCode(string Code)
        {
            return DataProvider.GetModelByBatchCode(Code);
        }
        public static IList<ESP.Finance.Entity.PNBatchInfo> GetList(string strWhere, List<SqlParameter> paramList)
        {
            return DataProvider.GetList(strWhere, paramList);
        }

        public static IList<ESP.Finance.Entity.ReturnInfo> GetReturnListByBatch(string BatchCode)
        {
            string term = " batchcode=@batchcode";
            string returnIDs = string.Empty;
            List<SqlParameter> paramList = new List<SqlParameter>();
            SqlParameter p1 = new SqlParameter("@batchcode", SqlDbType.NVarChar, 50);
            p1.Value = BatchCode;
            paramList.Add(p1);
            IList<ESP.Finance.Entity.PNBatchInfo> batchList = ESP.Finance.BusinessLogic.PNBatchManager.GetList(term, paramList);
            foreach (ESP.Finance.Entity.PNBatchInfo model in batchList)
            {
                returnIDs += model.PNID.Value.ToString() + ",";
            }
            returnIDs = returnIDs.TrimEnd(',');
            if (!string.IsNullOrEmpty(returnIDs))
                return ReturnManager.GetList(" returnid in(" + returnIDs + ")");
            else
                return null;
        }

        public static IList<ESP.Finance.Entity.ReturnInfo> GetReturnList(int batchID)
        {
            return DataProvider.GetReturnList(batchID);
        }

        public static string ExportPurchasePN(IList<ESP.Finance.Entity.ReturnInfo> returnList, System.Web.HttpResponse response)
        {
            string sourceFileName = "/Tmp/PurchaseBatch/PurchaseBatchTemplate.xls";
            string sourceFile = Common.GetLocalPath(sourceFileName);
            if (returnList == null || returnList.Count == 0) return sourceFileName;

            ExcelHandle excel = new ExcelHandle();
            excel.Load(sourceFile);
            excel.CurSheet = (Excel._Worksheet)excel.CurBook.Sheets[1];
            int counter = 0;//计数器
            int lineoffset = 4;//行数索引
            foreach (ESP.Finance.Entity.ReturnInfo model in returnList)
            {
                ESP.Compatible.Employee emp = new ESP.Compatible.Employee(model.RequestorID.Value);
                //项目号
                string projectCode_cell = "B" + (lineoffset + 1).ToString();
                ExcelHandle.WriteCell(excel.CurSheet, projectCode_cell, model.ProjectCode);
                //费用发生日期
                string date_cell = "C" + (lineoffset + 1).ToString();
                ExcelHandle.WriteCell(excel.CurSheet, date_cell, model.RequestDate == null ? "" : model.RequestDate.Value.ToString("yyyy-MM-dd"));
                //	申请人
                string app_cell = "D" + (lineoffset + 1).ToString();
                ExcelHandle.WriteCell(excel.CurSheet, app_cell, model.RequestEmployeeName);
                //	员工编号
                string empcode_cell = "E" + (lineoffset + 1).ToString();
                ExcelHandle.WriteCell(excel.CurSheet, empcode_cell, emp.ID);
                //费用所属组
                string group_cell = "F" + (lineoffset + 1).ToString();
                ExcelHandle.WriteCell(excel.CurSheet, group_cell, model.DepartmentName);
                //费用明细描述
                string detail_cell = "G" + (lineoffset + 1).ToString();
                ExcelHandle.WriteCell(excel.CurSheet, detail_cell, model.ReturnContent);
                //申请金额
                string appamount_cell = "H" + (lineoffset + 1).ToString();
                ExcelHandle.WriteCell(excel.CurSheet, appamount_cell, model.PreFee.Value.ToString("#,##0.00"));
                //订单号
                string po_cell = "I" + (lineoffset + 1).ToString();
                ExcelHandle.WriteCell(excel.CurSheet, po_cell, model.PRNo);
                //付款申请单号
                string pn_cell = "J" + (lineoffset + 1).ToString();
                ExcelHandle.WriteCell(excel.CurSheet, pn_cell, model.ReturnCode);
                //备注
                string remark_cell = "K" + (lineoffset + 1).ToString();
                ExcelHandle.WriteCell(excel.CurSheet, remark_cell, model.Remark);
                lineoffset++;
            }
            ExcelHandle.WriteCell(excel.CurSheet, "B" + (lineoffset + 4).ToString(), "公司名称");
            ExcelHandle.WriteCell(excel.CurSheet, "B" + (lineoffset + 5).ToString(), "开户行名称");
            ExcelHandle.WriteCell(excel.CurSheet, "B" + (lineoffset + 6).ToString(), "银行帐号");

            string suppliername_cell = "E" + (lineoffset + 4).ToString();
            ExcelHandle.WriteCell(excel.CurSheet, suppliername_cell, returnList[0].SupplierName);
            string supplierBank_cell = "E" + (lineoffset + 5).ToString();
            ExcelHandle.WriteCell(excel.CurSheet, supplierBank_cell, returnList[0].SupplierBankName);
            string supplierAccount_cell = "E" + (lineoffset + 6).ToString();
            ExcelHandle.WriteCell(excel.CurSheet, supplierAccount_cell, "'" + returnList[0].SupplierBankAccount);

            string serverpath = Common.GetLocalPath("/Tmp/PurchaseBatch");
            if (!System.IO.Directory.Exists(serverpath))
                System.IO.Directory.CreateDirectory(serverpath);
            string desFilename = Guid.NewGuid().ToString() + ".xls";
            string desFile = serverpath + "/" + desFilename;
            string desPath = "/Tmp/PurchaseBatch/" + desFilename;
            ExcelHandle.SaveAS(excel.CurBook, desFile);
            excel.Dispose();
            ESP.Purchase.Common.FileHelper.outExcel(desFile, desFilename, response);
            return desPath;
        }

        public static string ExportConsumption(int batchId, System.Web.HttpResponse response)
        {
            string sourceFileName = "/Tmp/PurchaseBatch/ConsumptionExTemplate.xlsx";
            string sourceFile = Common.GetLocalPath(sourceFileName);
            ESP.Finance.Entity.PNBatchInfo batchModel = ESP.Finance.BusinessLogic.PNBatchManager.GetModel(batchId);
            var consumptionList = ESP.Finance.BusinessLogic.ConsumptionManager.GetList(" batchid=" + batchId);

            ESP.HumanResource.Entity.EmployeeBaseInfo creator = ESP.HumanResource.BusinessLogic.EmployeeBaseManager.GetModel(batchModel.CreatorID.Value);
            ESP.HumanResource.Entity.EmployeesInPositionsInfo position = ESP.HumanResource.BusinessLogic.EmployeesInPositionsManager.GetModel(batchModel.CreatorID.Value);
            string deptName = string.Empty;

            List<ESP.Framework.Entity.DepartmentInfo> depList = new List<ESP.Framework.Entity.DepartmentInfo>();
            if (position != null && position.DepartmentID != 0)
            {
                depList = ESP.Framework.BusinessLogic.DepartmentManager.GetDepartmentListByID(position.DepartmentID, depList);
                string groupname = string.Empty;
                foreach (ESP.Framework.Entity.DepartmentInfo dept in depList)
                {
                    groupname += dept.DepartmentName + "-";
                }
                if (!string.IsNullOrEmpty(groupname))
                    deptName = groupname.Substring(0, groupname.Length - 1);
            }

            if (consumptionList == null || consumptionList.Count == 0) return sourceFileName;

            ExcelHandle excel = new ExcelHandle();
            excel.Load(sourceFile);
            excel.CurSheet = (Excel._Worksheet)excel.CurBook.Sheets[1];
            int lineoffset = 5;//行数索引
            int icount = 1;

            ExcelHandle.WriteCell(excel.CurSheet, "C2", batchModel.PurchaseBatchCode);
            ExcelHandle.WriteCell(excel.CurSheet, "C3", batchModel.CreateDate == null ? "" : batchModel.CreateDate.Value.ToString("yyyy-MM-dd"));
            ExcelHandle.WriteCell(excel.CurSheet, "F2", batchModel.Creator);
            ExcelHandle.WriteCell(excel.CurSheet, "F3", deptName);
            ExcelHandle.WriteCell(excel.CurSheet, "H2", batchModel.Amounts == null ? "0" : batchModel.Amounts.Value.ToString("#,##0.00"));

            foreach (ESP.Finance.Entity.ConsumptionInfo model in consumptionList)
            {
                string no_cell = "A" + (lineoffset + 1).ToString();
                ExcelHandle.WriteCell(excel.CurSheet, no_cell, icount.ToString());

                //费用发生日期
                string date_cell = "B" + (lineoffset + 1).ToString();
                ExcelHandle.WriteCell(excel.CurSheet, date_cell, model.OrderYM);
                //项目号
                string projectCode_cell = "C" + (lineoffset + 1).ToString();
                ExcelHandle.WriteCell(excel.CurSheet, projectCode_cell, model.ProjectCode);
                //	JS
                string js_cell = "D" + (lineoffset + 1).ToString();
                ExcelHandle.WriteCell(excel.CurSheet, js_cell, model.JSCode);
                //	xm
                string xm_cell = "E" + (lineoffset + 1).ToString();
                ExcelHandle.WriteCell(excel.CurSheet, xm_cell, model.XMCode);
                //desc
                string desc_cell = "F" + (lineoffset + 1).ToString();
                ExcelHandle.WriteCell(excel.CurSheet, desc_cell, model.Description);
                //申请金额
                string amount_cell = "G" + (lineoffset + 1).ToString();
                ExcelHandle.WriteCell(excel.CurSheet, amount_cell, model.Amount.ToString("#,##0.00"));
                //media
                string media_cell = "H" + (lineoffset + 1).ToString();
                ExcelHandle.WriteCell(excel.CurSheet, media_cell, model.Media);
                //订单Type
                string type_cell = "I" + (lineoffset + 1).ToString();
                ExcelHandle.WriteCell(excel.CurSheet, type_cell, model.OrderType);
                icount++;
                lineoffset++;
            }

            string serverpath = Common.GetLocalPath("/Tmp/PurchaseBatch");
            if (!System.IO.Directory.Exists(serverpath))
                System.IO.Directory.CreateDirectory(serverpath);
            string desFilename = Guid.NewGuid().ToString() + ".xlsx";
            string desFile = serverpath + "/" + desFilename;
            string desPath = "/Tmp/PurchaseBatch/" + desFilename;
            ExcelHandle.SaveAS(excel.CurBook, desFile);
            excel.Dispose();
            ESP.Purchase.Common.FileHelper.outExcel(desFile, desFilename, response);
            return desPath;

        }

        public static string ExportRebateRegistration(int batchId, System.Web.HttpResponse response)
        {
            string sourceFileName = "/Tmp/PurchaseBatch/RebateRegistrationExTemplate.xlsx";
            string sourceFile = Common.GetLocalPath(sourceFileName);
            ESP.Finance.Entity.PNBatchInfo batchModel = ESP.Finance.BusinessLogic.PNBatchManager.GetModel(batchId);
            var consumptionList = ESP.Finance.BusinessLogic.RebateRegistrationManager.GetList(" a.batchid=" + batchId);

            ESP.HumanResource.Entity.EmployeeBaseInfo creator = ESP.HumanResource.BusinessLogic.EmployeeBaseManager.GetModel(batchModel.CreatorID.Value);
            ESP.HumanResource.Entity.EmployeesInPositionsInfo position = ESP.HumanResource.BusinessLogic.EmployeesInPositionsManager.GetModel(batchModel.CreatorID.Value);
            string deptName = string.Empty;

            List<ESP.Framework.Entity.DepartmentInfo> depList = new List<ESP.Framework.Entity.DepartmentInfo>();
            if (position != null && position.DepartmentID != 0)
            {
                depList = ESP.Framework.BusinessLogic.DepartmentManager.GetDepartmentListByID(position.DepartmentID, depList);
                string groupname = string.Empty;
                foreach (ESP.Framework.Entity.DepartmentInfo dept in depList)
                {
                    groupname += dept.DepartmentName + "-";
                }
                if (!string.IsNullOrEmpty(groupname))
                    deptName = groupname.Substring(0, groupname.Length - 1);
            }

            if (consumptionList == null || consumptionList.Count == 0) return sourceFileName;

            ExcelHandle excel = new ExcelHandle();
            excel.Load(sourceFile);
            excel.CurSheet = (Excel._Worksheet)excel.CurBook.Sheets[1];
            int lineoffset = 10;//行数索引
            int icount = 1;

            ExcelHandle.WriteCell(excel.CurSheet, "B4", batchModel.PurchaseBatchCode);
            ExcelHandle.WriteCell(excel.CurSheet, "F5", batchModel.CreateDate == null ? "" : batchModel.CreateDate.Value.ToString("yyyy-MM-dd"));
            ExcelHandle.WriteCell(excel.CurSheet, "B5", batchModel.Creator);
            ExcelHandle.WriteCell(excel.CurSheet, "B6", deptName);
            ExcelHandle.WriteCell(excel.CurSheet, "B8", batchModel.Amounts == null ? "0" : batchModel.Amounts.Value.ToString("#,##0.00"));
            ExcelHandle.WriteCell(excel.CurSheet, "B7", batchModel.Description);

            for (int i = 2; i < consumptionList.Count; i++)
            {
                Excel.Range range = (Excel.Range)excel.CurSheet.Rows[lineoffset + i, Type.Missing];
                Excel.Range range1 = (Excel.Range)excel.CurSheet.Rows[lineoffset + i + 1];
                range.Copy(range1);
            }

            foreach (ESP.Finance.Entity.RebateRegistrationInfo model in consumptionList)
            {

                //费用发生日期
                string date_cell = "A" + (lineoffset + 1).ToString();
                ExcelHandle.WriteCell(excel.CurSheet, date_cell, model.CreditedDate);
                //项目号
                string projectCode_cell = "B" + (lineoffset + 1).ToString();
                ExcelHandle.WriteCell(excel.CurSheet, projectCode_cell, model.Project == null ? "" : model.Project.ProjectCode);
                //desc
                string desc_cell = "F" + (lineoffset + 1).ToString();
                ExcelHandle.WriteCell(excel.CurSheet, desc_cell, model.Remark);
                //申请金额
                string amount_cell = "E" + (lineoffset + 1).ToString();
                ExcelHandle.WriteCell(excel.CurSheet, amount_cell, model.RebateAmount.ToString("#,##0.00"));
                //media
                string media_cell = "C" + (lineoffset + 1).ToString();
                ExcelHandle.WriteCell(excel.CurSheet, media_cell, model.Supplier == null ? "" : model.Supplier.supplier_name);

                string num_cell = "G" + (lineoffset + 1).ToString();
                ExcelHandle.WriteCell(excel.CurSheet, num_cell, model.AccountingNum);

                string type_cell = "H" + (lineoffset + 1).ToString();
                ExcelHandle.WriteCell(excel.CurSheet, type_cell, model.SettleType);

                string branch_cell = "I" + (lineoffset + 1).ToString();
                ExcelHandle.WriteCell(excel.CurSheet, branch_cell, model.Branch);

                icount++;
                lineoffset++;
            }

            string serverpath = Common.GetLocalPath("/Tmp/PurchaseBatch");
            if (!System.IO.Directory.Exists(serverpath))
                System.IO.Directory.CreateDirectory(serverpath);
            string desFilename = Guid.NewGuid().ToString() + ".xlsx";
            string desFile = serverpath + "/" + desFilename;
            string desPath = "/Tmp/PurchaseBatch/" + desFilename;
            ExcelHandle.SaveAS(excel.CurBook, desFile);
            excel.Dispose();
            ESP.Purchase.Common.FileHelper.outExcel(desFile, desFilename, response);
            return desPath;

        } 

        /// <summary>
        /// 获取采购批次号
        /// </summary>
        /// <returns></returns>
        public static string CreatePurchaseBatchCode()
        {
            return DataProvider.CreatePurchaseBatchCode();
        }


        /// <summary>
        /// 批次审批批次内的报销单
        /// </summary>
        /// <param name="batch"></param>
        /// <param name="auditList"></param>
        /// <param name="nextAuditerList"></param>
        /// <returns></returns>
        public static bool AddItemsByExpenseAccount(PNBatchInfo batch, List<ESP.Finance.Entity.ExpenseAccountBatchAudit> auditList, List<int> nextAuditerList, bool isNeedFinanceDirector)
        {
            using (SqlConnection conn = new SqlConnection(DataAccess.DbHelperSQL.connectionString))
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();
                try
                {
                    ESP.Finance.BusinessLogic.PNBatchManager.Update(batch);

                    foreach (ESP.Finance.Entity.ExpenseAccountBatchAudit auditInfo in auditList)
                    {

                        Dictionary<string, object> nextAuditer = null;
                        auditInfo.Prarms.Add("PaymentTypeCode", batch.BatchCode);


                        if (auditInfo.Workitem != null && !string.IsNullOrEmpty(auditInfo.Workitem.WebPage) && auditInfo.Workitem.WebPage.IndexOf("step=f1") > 0)
                        {
                            if (isNeedFinanceDirector)
                            {
                                nextAuditer = new Dictionary<string, object>() 
                                { 
                                    { "Finance2Id" ,  nextAuditerList.ToArray() }
                                };
                            }
                            else
                            {
                                nextAuditer = new Dictionary<string, object>() 
                                { 
                                    { "Finance3Id" ,  nextAuditerList.ToArray() }
                                };
                            }
                        }

                        //调用工作流
                        ESP.Workflow.WorkItemManager.CloseWorkItem(auditInfo.Workitem.WorkItemId, auditInfo.CurrentUserId, "Approved", nextAuditer, new Action<object>(ESP.Finance.BusinessLogic.ExpenseAccountManager.UpdateStatus), auditInfo.Prarms);

                    }

                    trans.Commit();
                    return true;
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    return false;
                }
            }

        }


        /// <summary>
        /// 把报销单加入批次中
        /// </summary>
        /// <param name="batchModel"></param>
        /// <param name="returnIDS"></param>
        /// <returns></returns>
        public static bool InsertExpenseAccoutToBatch(PNBatchInfo batchModel, string[] returnIDS)
        {
            using (SqlConnection conn = new SqlConnection(DataAccess.DbHelperSQL.connectionString))
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();
                try
                {

                    List<ESP.Finance.Entity.PNBatchRelationInfo> newItemList = new List<PNBatchRelationInfo>();
                    ESP.Finance.Entity.ReturnInfo returnModel = null;
                    ESP.Finance.Entity.PNBatchRelationInfo relationModel = null;
                    for (int i = 0; i < returnIDS.Length; i++)
                    {
                        returnModel = ESP.Finance.BusinessLogic.ReturnManager.GetModel(int.Parse(returnIDS[i].ToString()));
                        relationModel = new ESP.Finance.Entity.PNBatchRelationInfo();
                        relationModel.BatchID = batchModel.BatchID;
                        relationModel.ReturnID = int.Parse(returnIDS[i].ToString());
                        newItemList.Add(relationModel);

                    }
                    new DataAccess.PNBatchRelationDataProvider().Add(newItemList, trans);

                    trans.Commit();
                    return true;
                }
                catch
                {
                    trans.Rollback();
                    return false;
                }
            }

        }

        /// <summary>
        /// 从批次中移除报销单
        /// </summary>
        /// <param name="batchModel"></param>
        /// <param name="returnIDS"></param>
        /// <returns></returns>
        public static bool RemoveExpenseAccoutInBatch(PNBatchInfo batchModel, string[] returnIDS)
        {
            using (SqlConnection conn = new SqlConnection(DataAccess.DbHelperSQL.connectionString))
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();
                try
                {

                    for (int i = 0; i < returnIDS.Length; i++)
                    {
                        ESP.Finance.BusinessLogic.PNBatchRelationManager.Delete(batchModel.BatchID, int.Parse(returnIDS[i].ToString()));
                    }
                    trans.Commit();
                    return true;
                }
                catch
                {
                    trans.Rollback();
                    return false;
                }
            }

        }

        /// <summary>
        /// 获取报销的批次审批列表
        /// </summary>
        /// <param name="whereStr"></param>
        /// <returns></returns>
        public static DataTable GetBatchByExpenseAccount(string whereStr)
        {
            return DataProvider.GetBatchByExpenseAccount(whereStr);
        }

        public static IList<PNBatchInfo> GetWaitAuditList(int[] userIds, int batchType)
        {
            return DataProvider.GetWaitAuditList(userIds,batchType);
        }

        public static int AuditConsumption(ESP.Finance.Entity.PNBatchInfo batchModel,int formType,ESP.Compatible.Employee currentUser, int status, string suggestion)
        {
            return DataProvider.AuditConsumption(batchModel,formType, currentUser, status, suggestion);
        }

        public static int AuditRebateRegistration(ESP.Finance.Entity.PNBatchInfo batchModel, ESP.Compatible.Employee currentUser, int status, string suggestion)
        {
            return DataProvider.AuditRebateRegistration(batchModel, currentUser, status, suggestion);
        }
    }
}
