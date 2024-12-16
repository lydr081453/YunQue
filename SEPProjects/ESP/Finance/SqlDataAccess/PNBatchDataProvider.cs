using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESP.Finance.Utility;
using ESP.Finance.Entity;
using ESP.Finance.BusinessLogic;

namespace ESP.Finance.DataAccess
{
    internal class PNBatchDataProvider : ESP.Finance.IDataAccess.IPNBatchProvider
    {
        public PNBatchDataProvider()
        { }
        #region  成员方法

        public int Exist(string batchCode, int batchID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Count(*) from F_PNBatch where BatchCode=@BatchCode and batchID!=@batchID");
            SqlParameter[] parameters = {
                    new SqlParameter("@BatchCode", SqlDbType.NVarChar,50),
                    new SqlParameter("@batchID", SqlDbType.Int,4)
                                        };
            parameters[0].Value = batchCode;
            parameters[1].Value = batchID;
            object obj = DbHelperSQL.GetSingle(strSql.ToString(), parameters);
            if (obj == null)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(obj);
            }
        }

        public int Add(ESP.Finance.Entity.PNBatchInfo model)
        {
            return Add(model, null);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(ESP.Finance.Entity.PNBatchInfo model, SqlTransaction trans)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into F_PNBatch(");
            strSql.Append("BatchCode,Amounts,Status,PaymentUserID,PaymentCode,PaymentEmployeeName,PaymentUserName,SupplierBankName,SupplierBankAccount,IsInvoice,PNID,InvoiceNo,InvoiceDate,Lastupdatetime,BankID,BankName,DBCode,DBManager,BankAccount,PhoneNo,ExchangeNo,PRID,RequestPhone,BankAccountName,BankAddress,PaymentTypeID,PaymentType,PaymentDate,BranchID,PeriodID,CreateDate,CreatorID,SupplierName,BranchCode,Description,PurchaseBatchCode,BatchType,TicketReturnPoint,Creator,ProveFile)");
            strSql.Append(" values (");
            strSql.Append("@BatchCode,@Amounts,@Status,@PaymentUserID,@PaymentCode,@PaymentEmployeeName,@PaymentUserName,@SupplierBankName,@SupplierBankAccount,@IsInvoice,@PNID,@InvoiceNo,@InvoiceDate,@Lastupdatetime,@BankID,@BankName,@DBCode,@DBManager,@BankAccount,@PhoneNo,@ExchangeNo,@PRID,@RequestPhone,@BankAccountName,@BankAddress,@PaymentTypeID,@PaymentType,@PaymentDate,@BranchID,@PeriodID,@CreateDate,@CreatorID,@SupplierName,@BranchCode,@Description,@PurchaseBatchCode,@BatchType,@TicketReturnPoint,@Creator,@ProveFile)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@BatchCode", SqlDbType.NVarChar,50),
					new SqlParameter("@Amounts", SqlDbType.Decimal,20),
					new SqlParameter("@Status", SqlDbType.Int,4),
					new SqlParameter("@PaymentUserID", SqlDbType.Int,4),
					new SqlParameter("@PaymentCode", SqlDbType.NVarChar,50),
					new SqlParameter("@PaymentEmployeeName", SqlDbType.NVarChar,50),
					new SqlParameter("@PaymentUserName", SqlDbType.NVarChar,50),
					new SqlParameter("@SupplierBankName", SqlDbType.NVarChar,100),
					new SqlParameter("@SupplierBankAccount", SqlDbType.NVarChar,50),
					new SqlParameter("@IsInvoice", SqlDbType.Int,4),
					new SqlParameter("@PNID", SqlDbType.Int,4),
					new SqlParameter("@InvoiceNo", SqlDbType.NVarChar,50),
					new SqlParameter("@InvoiceDate", SqlDbType.DateTime),
					new SqlParameter("@Lastupdatetime", SqlDbType.DateTime),
					new SqlParameter("@BankID", SqlDbType.Int,4),
					new SqlParameter("@BankName", SqlDbType.NVarChar,100),
					new SqlParameter("@DBCode", SqlDbType.NVarChar,10),
					new SqlParameter("@DBManager", SqlDbType.NVarChar,10),
					new SqlParameter("@BankAccount", SqlDbType.NVarChar,50),
					new SqlParameter("@PhoneNo", SqlDbType.NVarChar,50),
					new SqlParameter("@ExchangeNo", SqlDbType.NVarChar,50),
					new SqlParameter("@PRID", SqlDbType.Int,4),
					new SqlParameter("@RequestPhone", SqlDbType.NVarChar,50),
					new SqlParameter("@BankAccountName", SqlDbType.NVarChar,50),
					new SqlParameter("@BankAddress", SqlDbType.NVarChar,100),
					new SqlParameter("@PaymentTypeID", SqlDbType.Int,4),
					new SqlParameter("@PaymentType", SqlDbType.NVarChar,50),
					new SqlParameter("@PaymentDate", SqlDbType.DateTime),
					new SqlParameter("@BranchID", SqlDbType.Int,4),
					new SqlParameter("@PeriodID", SqlDbType.Int,4),
					new SqlParameter("@CreateDate", SqlDbType.DateTime),
					new SqlParameter("@CreatorID", SqlDbType.Int,4),
					new SqlParameter("@SupplierName", SqlDbType.NVarChar,500),
					new SqlParameter("@BranchCode", SqlDbType.NVarChar,50),
					new SqlParameter("@Description", SqlDbType.NVarChar,500),
                    new SqlParameter("@PurchaseBatchCode", SqlDbType.NVarChar,500),
                    new SqlParameter("@BatchType" , SqlDbType.Int,4),
                    new SqlParameter("@TicketReturnPoint" , SqlDbType.Decimal,20),
                    new SqlParameter("@Creator", SqlDbType.NVarChar,50),
                    new SqlParameter("@ProveFile", SqlDbType.NVarChar,200)
                                        };
            parameters[0].Value = model.BatchCode;
            parameters[1].Value = model.Amounts;
            parameters[2].Value = model.Status;
            parameters[3].Value = model.PaymentUserID;
            parameters[4].Value = model.PaymentCode;
            parameters[5].Value = model.PaymentEmployeeName;
            parameters[6].Value = model.PaymentUserName;
            parameters[7].Value = model.SupplierBankName;
            parameters[8].Value = model.SupplierBankAccount;
            parameters[9].Value = model.IsInvoice;
            parameters[10].Value = model.PNID;
            parameters[11].Value = model.InvoiceNo;
            parameters[12].Value = model.InvoiceDate;
            parameters[13].Value = DateTime.Now;
            parameters[14].Value = model.BankID;
            parameters[15].Value = model.BankName;
            parameters[16].Value = model.DBCode;
            parameters[17].Value = model.DBManager;
            parameters[18].Value = model.BankAccount;
            parameters[19].Value = model.PhoneNo;
            parameters[20].Value = model.ExchangeNo;
            parameters[21].Value = model.PRID;
            parameters[22].Value = model.RequestPhone;
            parameters[23].Value = model.BankAccountName;
            parameters[24].Value = model.BankAddress;
            parameters[25].Value = model.PaymentTypeID;
            parameters[26].Value = model.PaymentType;
            parameters[27].Value = model.PaymentDate;
            parameters[28].Value = model.BranchID;
            parameters[29].Value = model.PeriodID;
            parameters[30].Value = model.CreateDate;
            parameters[31].Value = model.CreatorID;
            parameters[32].Value = model.SupplierName;
            parameters[33].Value = model.BranchCode;
            parameters[34].Value = model.Description;
            parameters[35].Value = model.PurchaseBatchCode;
            parameters[36].Value = model.BatchType;
            parameters[37].Value = model.TicketReturnPoint;
            parameters[38].Value = model.Creator;
            parameters[39].Value = model.ProveFile;

            object obj = DbHelperSQL.GetSingle(strSql.ToString(), trans, parameters);
            if (obj == null)
            {
                return 1;
            }
            else
            {
                return Convert.ToInt32(obj);
            }
        }

        public int Update(ESP.Finance.Entity.PNBatchInfo model)
        {
            return Update(model, null);
        }

        public int UpdateAmounts(ESP.Finance.Entity.PNBatchInfo model, SqlTransaction trans)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update F_PNBatch set ");
            strSql.Append("Amounts=" + model.Amounts.Value.ToString());
            strSql.Append(" where BatchID=" + model.BatchID.ToString());
            if (trans != null)
                return DbHelperSQL.ExecuteSql(strSql.ToString(), trans, null);
            else
                return DbHelperSQL.ExecuteSql(strSql.ToString());
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public int Update(ESP.Finance.Entity.PNBatchInfo model, SqlTransaction trans)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update F_PNBatch set ");
            strSql.Append("BatchCode=@BatchCode,");
            strSql.Append("Amounts=@Amounts,");
            strSql.Append("Status=@Status,");
            strSql.Append("PaymentUserID=@PaymentUserID,");
            strSql.Append("PaymentCode=@PaymentCode,");
            strSql.Append("PaymentEmployeeName=@PaymentEmployeeName,");
            strSql.Append("PaymentUserName=@PaymentUserName,");
            strSql.Append("SupplierBankName=@SupplierBankName,");
            strSql.Append("SupplierBankAccount=@SupplierBankAccount,");
            strSql.Append("IsInvoice=@IsInvoice,");
            strSql.Append("PNID=@PNID,");
            strSql.Append("InvoiceNo=@InvoiceNo,");
            strSql.Append("InvoiceDate=@InvoiceDate,");
            strSql.Append("Lastupdatetime=@Lastupdatetime,");
            strSql.Append("BankID=@BankID,");
            strSql.Append("BankName=@BankName,");
            strSql.Append("DBCode=@DBCode,");
            strSql.Append("DBManager=@DBManager,");
            strSql.Append("BankAccount=@BankAccount,");
            strSql.Append("PhoneNo=@PhoneNo,");
            strSql.Append("ExchangeNo=@ExchangeNo,");
            strSql.Append("PRID=@PRID,");
            strSql.Append("RequestPhone=@RequestPhone,");
            strSql.Append("BankAccountName=@BankAccountName,");
            strSql.Append("BankAddress=@BankAddress,");
            strSql.Append("PaymentTypeID=@PaymentTypeID,");
            strSql.Append("PaymentType=@PaymentType,");
            strSql.Append("PaymentDate=@PaymentDate,");
            strSql.Append("BranchID=@BranchID,");
            strSql.Append("PeriodID=@PeriodID,");
            strSql.Append("CreateDate=@CreateDate,");
            strSql.Append("CreatorID=@CreatorID,");
            strSql.Append("SupplierName=@SupplierName,");
            strSql.Append("BranchCode=@BranchCode,");
            strSql.Append("Description=@Description,");
            strSql.Append("PurchaseBatchCode=@PurchaseBatchCode,");
            strSql.Append("BatchType=@BatchType,TicketReturnPoint=@TicketReturnPoint,Creator=@Creator,ProveFile=@ProveFile ");
            strSql.Append(" where BatchID=@BatchID ");
            SqlParameter[] parameters = {
					new SqlParameter("@BatchCode", SqlDbType.NVarChar,50),
					new SqlParameter("@Amounts", SqlDbType.Decimal,20),
					new SqlParameter("@Status", SqlDbType.Int,4),
					new SqlParameter("@PaymentUserID", SqlDbType.Int,4),
					new SqlParameter("@PaymentCode", SqlDbType.NVarChar,50),
					new SqlParameter("@PaymentEmployeeName", SqlDbType.NVarChar,50),
					new SqlParameter("@PaymentUserName", SqlDbType.NVarChar,50),
					new SqlParameter("@SupplierBankName", SqlDbType.NVarChar,100),
					new SqlParameter("@SupplierBankAccount", SqlDbType.NVarChar,50),
					new SqlParameter("@IsInvoice", SqlDbType.Int,4),
					new SqlParameter("@PNID", SqlDbType.Int,4),
					new SqlParameter("@InvoiceNo", SqlDbType.NVarChar,50),
					new SqlParameter("@InvoiceDate", SqlDbType.DateTime),
					new SqlParameter("@Lastupdatetime", SqlDbType.DateTime),
					new SqlParameter("@BankID", SqlDbType.Int,4),
					new SqlParameter("@BankName", SqlDbType.NVarChar,100),
					new SqlParameter("@DBCode", SqlDbType.NVarChar,10),
					new SqlParameter("@DBManager", SqlDbType.NVarChar,10),
					new SqlParameter("@BankAccount", SqlDbType.NVarChar,50),
					new SqlParameter("@PhoneNo", SqlDbType.NVarChar,50),
					new SqlParameter("@ExchangeNo", SqlDbType.NVarChar,50),
					new SqlParameter("@PRID", SqlDbType.Int,4),
					new SqlParameter("@RequestPhone", SqlDbType.NVarChar,50),
					new SqlParameter("@BankAccountName", SqlDbType.NVarChar,50),
					new SqlParameter("@BankAddress", SqlDbType.NVarChar,100),
					new SqlParameter("@PaymentTypeID", SqlDbType.Int,4),
					new SqlParameter("@PaymentType", SqlDbType.NVarChar,50),
					new SqlParameter("@PaymentDate", SqlDbType.DateTime),
					new SqlParameter("@BranchID", SqlDbType.Int,4),
					new SqlParameter("@PeriodID", SqlDbType.Int,4),
					new SqlParameter("@CreateDate", SqlDbType.DateTime),
					new SqlParameter("@CreatorID", SqlDbType.Int,4),
					new SqlParameter("@SupplierName", SqlDbType.NVarChar,500),
					new SqlParameter("@BranchCode", SqlDbType.NVarChar,50),
					new SqlParameter("@Description", SqlDbType.NVarChar,500),
                    new SqlParameter("@PurchaseBatchCode", SqlDbType.NVarChar,500),
                    new SqlParameter("@BatchType", SqlDbType.Int,4),
                    new SqlParameter("@BatchID", SqlDbType.Int,4),
                    new SqlParameter("@TicketReturnPoint",SqlDbType.Decimal,20),
                    new SqlParameter("@Creator", SqlDbType.NVarChar,50),
                    new SqlParameter("@ProveFile", SqlDbType.NVarChar,200)
                                        };
            parameters[0].Value = model.BatchCode;
            parameters[1].Value = model.Amounts;
            parameters[2].Value = model.Status;
            parameters[3].Value = model.PaymentUserID;
            parameters[4].Value = model.PaymentCode;
            parameters[5].Value = model.PaymentEmployeeName;
            parameters[6].Value = model.PaymentUserName;
            parameters[7].Value = model.SupplierBankName;
            parameters[8].Value = model.SupplierBankAccount;
            parameters[9].Value = model.IsInvoice;
            parameters[10].Value = model.PNID;
            parameters[11].Value = model.InvoiceNo;
            parameters[12].Value = model.InvoiceDate;
            parameters[13].Value = DateTime.Now;
            parameters[14].Value = model.BankID;
            parameters[15].Value = model.BankName;
            parameters[16].Value = model.DBCode;
            parameters[17].Value = model.DBManager;
            parameters[18].Value = model.BankAccount;
            parameters[19].Value = model.PhoneNo;
            parameters[20].Value = model.ExchangeNo;
            parameters[21].Value = model.PRID;
            parameters[22].Value = model.RequestPhone;
            parameters[23].Value = model.BankAccountName;
            parameters[24].Value = model.BankAddress;
            parameters[25].Value = model.PaymentTypeID;
            parameters[26].Value = model.PaymentType;
            parameters[27].Value = model.PaymentDate;
            parameters[28].Value = model.BranchID;
            parameters[29].Value = model.PeriodID;
            parameters[30].Value = model.CreateDate;
            parameters[31].Value = model.CreatorID;
            parameters[32].Value = model.SupplierName;
            parameters[33].Value = model.BranchCode;
            parameters[34].Value = model.Description;
            parameters[35].Value = model.PurchaseBatchCode;
            parameters[36].Value = model.BatchType;
            parameters[37].Value = model.BatchID;
            parameters[38].Value = model.TicketReturnPoint;
            parameters[39].Value = model.Creator;
            parameters[40].Value = model.ProveFile;

            return DbHelperSQL.ExecuteSql(strSql.ToString(), trans, parameters);
        }

        /// <summary>
        /// 采购审批人驳回批次
        /// </summary>
        /// <param name="BatchID"></param>
        /// <returns></returns>
        public bool returnBatchForPurchase(int BatchID, ESP.Compatible.Employee CurrentUser, string requesition)
        {
            using (SqlConnection conn = new SqlConnection(DbHelperSQL.connectionString))
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();
                try
                {
                    //更新returnModel的状态
                    IList<ESP.Finance.Entity.PNBatchRelationInfo> relationList = new ESP.Finance.DataAccess.PNBatchRelationDataProvider().GetList(" batchId=" + BatchID, null);
                    foreach (ESP.Finance.Entity.PNBatchRelationInfo relationModel in relationList)
                    {
                        ESP.Finance.Entity.ReturnInfo returnModel = new ESP.Finance.DataAccess.ReturnDataProvider().GetModel(relationModel.ReturnID.Value);
                        returnModel.ReturnStatus = (int)PaymentStatus.Save;
                        new ESP.Finance.DataAccess.ReturnDataProvider().Update(returnModel, trans);

                        ESP.Finance.Entity.AuditLogInfo returnlog = new ESP.Finance.Entity.AuditLogInfo();
                        returnlog.AuditDate = DateTime.Now;
                        returnlog.AuditorEmployeeName = CurrentUser.Name;
                        returnlog.AuditorSysID = Convert.ToInt32(CurrentUser.SysID);
                        returnlog.AuditorUserCode = CurrentUser.ID;
                        returnlog.AuditorUserName = CurrentUser.ITCode;
                        returnlog.AuditStatus = (int)ESP.Finance.Utility.AuditHistoryStatus.TerminateAuditing;
                        returnlog.FormID = returnModel.ReturnID;
                        returnlog.FormType = (int)ESP.Finance.Utility.FormType.Return;
                        returnlog.Suggestion = requesition;
                        new DataAccess.AuditLogDataProvider().Add(returnlog, trans);

                        new ESP.Finance.DataAccess.ReturnAuditHistDataProvider().DeleteByReturnID(returnModel.ReturnID, "", trans, null);
                    }
                    //删除批次关联表
                    new ESP.Finance.DataAccess.PNBatchRelationDataProvider().DeleteByBatchID(BatchID, trans);
                    //删除批次
                    Delete(BatchID, trans);
                    trans.Commit();
                    return true;
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    ESP.Logging.Logger.Add(ex.Message, "采购批次驳回", ESP.Logging.LogLevel.Error, ex);
                    return false;
                }
            }
        }

        public int Delete(int BatchID)
        {
            return Delete(BatchID, null);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public int Delete(int BatchID, SqlTransaction trans)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete F_PNBatch ");
            strSql.Append(" where BatchID=@BatchID;delete f_pnbatchrelation where BatchID=@BatchID;");

            SqlParameter[] parameters = {
					new SqlParameter("@BatchID", SqlDbType.Int,4)};
            parameters[0].Value = BatchID;

            return DbHelperSQL.ExecuteSql(strSql.ToString(), trans, parameters);
        }
        public ESP.Finance.Entity.PNBatchInfo GetModelByBatchCode(string Code)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 * from F_PNBatch ");
            strSql.Append(" where BatchCode=@BatchCode ");
            SqlParameter[] parameters = {
					new SqlParameter("@BatchCode", SqlDbType.NVarChar,50)};
            parameters[0].Value = Code;
            return CBO.FillObject<ESP.Finance.Entity.PNBatchInfo>(DbHelperSQL.Query(strSql.ToString(), parameters));
        }

        public ESP.Finance.Entity.PNBatchInfo GetModel(int BatchID)
        {
            return GetModel(BatchID, null);
        }
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public ESP.Finance.Entity.PNBatchInfo GetModel(int BatchID, SqlTransaction trans)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 * from F_PNBatch ");
            strSql.Append(" where BatchID=@BatchID ");
            SqlParameter[] parameters = {
					new SqlParameter("@BatchID", SqlDbType.Int,4)};
            parameters[0].Value = BatchID;
            return CBO.FillObject<ESP.Finance.Entity.PNBatchInfo>(DbHelperSQL.Query(strSql.ToString(), trans, parameters));
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public IList<ESP.Finance.Entity.PNBatchInfo> GetList(string strWhere, List<SqlParameter> paramList)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  * from F_PNBatch ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by BatchId desc ");

            return CBO.FillCollection<ESP.Finance.Entity.PNBatchInfo>(DbHelperSQL.Query(strSql.ToString(), paramList));
        }

        public IList<ESP.Finance.Entity.ReturnInfo> GetReturnList(int batchID)
        {
            List<ESP.Finance.Entity.ReturnInfo> returnList = new List<ESP.Finance.Entity.ReturnInfo>();
            IList<ESP.Finance.Entity.PNBatchRelationInfo> relationList = ESP.Finance.BusinessLogic.PNBatchRelationManager.GetList(" batchid=" + batchID.ToString(), null);
            if (relationList != null && relationList.Count != 0)
            {
                foreach (ESP.Finance.Entity.PNBatchRelationInfo model in relationList)
                {
                    returnList.Add(ESP.Finance.BusinessLogic.ReturnManager.GetModel(model.ReturnID.Value));
                }
            }
            return returnList;
        }

        /// <summary>
        /// 获取采购批次号
        /// </summary>
        /// <returns></returns>
        public string CreatePurchaseBatchCode()
        {
            string date = DateTime.Now.ToString("yy-MM").Replace("-", "");
            string strSql = "select max(PurchaseBatchCode) as maxId from F_PNBatch where PurchaseBatchCode like 'BN" + date + "%'";
            DataSet ds = DbHelperSQL.Query(strSql.ToString());
            int num = ds.Tables[0].Rows[0]["maxId"] == DBNull.Value ? 0 : int.Parse(ds.Tables[0].Rows[0]["maxId"].ToString().Substring(6, 4).ToString());
            num++;
            return "BN" + date + num.ToString("0000");
        }

        /// <summary>
        /// 获取报销的批次审批列表
        /// </summary>
        /// <param name="whereStr"></param>
        /// <returns></returns>
        public DataTable GetBatchByExpenseAccount(string whereStr)
        {
            string strSql = @"SELECT  a.*,isnull(b.ReturnCount,0) as ReturnCount,isnull(c.TotalPreFee,0) as TotalPreFee FROM F_PNBatch as a 
                left join (select BatchID,count(ReturnID) as ReturnCount from F_PNBatchRelation group by BatchID) as b
                on a.BatchID=b.BatchID 
                left join (select BatchID,sum(b.PreFee) as TotalPreFee from F_PNBatchRelation as a inner join F_Return as b on a.ReturnID = b.ReturnID group by BatchID) as c 
                on a.BatchID=c.BatchID join f_branch d on a.branchcode =d.branchcode 
                where 1=1 ";

            if (!string.IsNullOrEmpty(whereStr))
            {
                strSql += whereStr;
            }
            strSql += " order by a.createdate desc";
            return DbHelperSQL.Query(strSql).Tables[0];
        }

        public IList<PNBatchInfo> GetWaitAuditList(int[] userIds,int batchType)
        {
            if (userIds == null || userIds.Length == 0)
                return new List<PNBatchInfo>();

            StringBuilder sql = new StringBuilder(@"
select distinct a.*
from f_pnbatch as a inner join f_consumptionAudit as b 
on a.batchid = b.batchid and b.AuditorUserID=a.PaymentUserid and b.FormType="+(int)ESP.Finance.Utility.FormType.Consumption+
@"where  a.status not in(-1,0,1,140,139) and a.batchType="+batchType+@"
 and b.AuditStatus=0 and b.AuditorUserID in (").Append(userIds[0]);

            for (var i = 1; i < userIds.Length; i++)
            {
                sql.Append(",").Append(userIds[i]);
            }

            sql.Append(")");


            using (var reader = ESP.Finance.DataAccess.DbHelperSQL.ExecuteReader(sql.ToString()))
            {
                return MyPhotoUtility.CBO.FillCollection<PNBatchInfo>(reader);
            }
        }
        /// <summary>
        /// 审批消耗
        /// </summary>
        /// <param name="batchModel"></param>
        /// <param name="currentUser"></param>
        /// <param name="status"></param>
        /// <param name="suggestion"></param>
        /// <returns></returns>
        public int AuditConsumption(ESP.Finance.Entity.PNBatchInfo batchModel, int formType, ESP.Compatible.Employee currentUser, int status, string suggestion)
        {
            int ret = 0;
            string term = string.Empty;
            List<SqlParameter> paramlist = new List<SqlParameter>();
            string DelegateUsers = string.Empty;
            IList<ESP.Framework.Entity.AuditBackUpInfo> delegates = ESP.Framework.BusinessLogic.AuditBackUpManager.GetModelsByBackUpUserID(int.Parse(currentUser.SysID));
            foreach (ESP.Framework.Entity.AuditBackUpInfo model in delegates)
            {
                DelegateUsers += model.UserID.ToString() + ",";
            }
            DelegateUsers = DelegateUsers.TrimEnd(',');

            term = " batchId=@batchId and formType=@formType and AuditStatus=@AuditStatus";

            SqlParameter p1 = new SqlParameter("@batchId", SqlDbType.Int, 4);
            p1.SqlValue = batchModel.BatchID;
            paramlist.Add(p1);

            SqlParameter p2 = new SqlParameter("@formType", SqlDbType.Int, 4);
            p2.SqlValue = formType;
            paramlist.Add(p2);

            SqlParameter p3 = new SqlParameter("@AuditStatus", SqlDbType.Int, 4);
            p3.SqlValue = (int)AuditHistoryStatus.UnAuditing;
            paramlist.Add(p3);

            var auditList = ESP.Finance.BusinessLogic.ConsumptionAuditManager.GetList(term, paramlist);

            if (auditList == null || auditList.Count == 0)
            {
                ret = 0;
            }
            else
            {
                //当前审核人
                ConsumptionAuditInfo firstRole = auditList[0];
                //下一级审核人
                ConsumptionAuditInfo nextRole = null;
                if (auditList.Count >= 2)
                    nextRole = auditList[1];

                if (firstRole.AuditorUserID == int.Parse(currentUser.SysID) || DelegateUsers.IndexOf(firstRole.AuditorUserID.ToString()) >= 0)//审核人与登录人校验
                {
                    if (status == (int)AuditHistoryStatus.PassAuditing)
                    {
                        if (nextRole != null)
                        {
                            batchModel.PaymentUserID = nextRole.AuditorUserID;
                            batchModel.PaymentEmployeeName = nextRole.AuditorEmployeeName;
                            batchModel.PaymentUserName = nextRole.AuditorUserName;
                            batchModel.PaymentCode = nextRole.AuditorUserCode;
                        }
                        switch (firstRole.AuditType.Value)
                        {
                            case 2://业务第一级审批，状态不变

                                break;
                            case 5://业务第二级审批后到财务第一级审批
                                batchModel.Status = (int)PaymentStatus.MajorAudit;
                                break;
                            case 11://财务第一级审批后到第二级审批
                                batchModel.Status = (int)PaymentStatus.FinanceLevel1;
                                break;
                            case 12://财务第2级审批后到第3级审批
                                batchModel.Status = (int)PaymentStatus.FinanceLevel2;
                                break;
                            case 13://财务第三级审批end
                                batchModel.Status = (int)PaymentStatus.FinanceComplete;
                                break;
                        }
                    }
                    else
                    {
                        batchModel.Status = (int)PaymentStatus.Rejected;
                    }

                    //更新当前审批流角色状态
                    firstRole.AuditStatus = status;
                    firstRole.AuditDate = DateTime.Now;
                    firstRole.Suggestion = suggestion;

                    var consumptionList = ESP.Finance.BusinessLogic.ConsumptionManager.GetList(" batchid=" + batchModel.BatchID);

                    #region begin transaction
                    using (SqlConnection conn = new SqlConnection(DbHelperSQL.connectionString))
                    {
                        conn.Open();
                        SqlTransaction trans = conn.BeginTransaction();
                        try
                        {
                            ESP.Finance.BusinessLogic.PNBatchManager.Update(batchModel, trans);//更新批次状态
                            ret++;
                            ESP.Finance.BusinessLogic.ConsumptionAuditManager.Update(firstRole, trans, Utility.FormType.Consumption);//更新工作流状态
                            ret++;
                            //如果是财务最后一级审批，导入数据同步到付款申请表
                            if (batchModel.Status == (int)PaymentStatus.FinanceLevel1)
                            {
                              
                                foreach (ConsumptionInfo item in consumptionList)
                                {
                                    if (item.ReturnId > 0)
                                        break;
                                    ProjectInfo projectModel = ProjectManager.GetModelWithOutDetailList(item.ProjectId, trans);
                                    ReturnInfo retModel = new ReturnInfo();
                                    retModel.BranchCode = projectModel.BranchCode;
                                    retModel.BranchID = projectModel.BranchID;
                                    retModel.BranchName = projectModel.BranchName;
                                    retModel.CommitDate = batchModel.CreateDate;
                                    retModel.DepartmentID = projectModel.GroupID;
                                    retModel.DepartmentName = projectModel.GroupName;
                                    retModel.FactFee = item.Amount;
                                    retModel.LastAuditPassTime = DateTime.Now;
                                    retModel.LastAuditUserID = firstRole.AuditorUserID;
                                    retModel.LastAuditUserName = firstRole.AuditorUserName;
                                    retModel.LastUpdateDateTime = DateTime.Now;
                                    retModel.PaymentEmployeeName = firstRole.AuditorEmployeeName;
                                    retModel.PaymentUserID = firstRole.AuditorUserID;
                                    retModel.PaymentUserName = firstRole.AuditorUserName;
                                    retModel.PreBeginDate = DateTime.Parse(item.OrderYM + "1日");
                                    retModel.PreEndDate = retModel.PreBeginDate.Value.AddMonths(1).AddDays(-1) ;
                                    retModel.PreFee = item.Amount;
                                    retModel.ProjectCode = projectModel.ProjectCode;
                                    retModel.ProjectID = projectModel.ProjectId;
                                    retModel.Remark = item.Description;
                                    retModel.RequestDate = batchModel.CreateDate;
                                    retModel.RequestEmployeeName = batchModel.Creator;
                                    retModel.RequestorID = batchModel.CreatorID;
                                    retModel.ReturnCode = new ESP.Finance.DataAccess.ReturnDataProvider().GetNewReturnCode(trans);
                                    retModel.ReturnContent = projectModel.BusinessDescription+":" + item.OrderYM+":" + item.OrderType;
                                    retModel.ReturnFactDate = DateTime.Now;
                                    retModel.ReturnPreDate = retModel.PreBeginDate;
                                    retModel.ReturnStatus = (int)PaymentStatus.FinanceComplete;
                                    retModel.ReturnType = (int)ESP.Purchase.Common.PRTYpe.PROJECT_Media;
                                    retModel.SupplierBankAccount = "";
                                    retModel.SupplierBankName = "";
                                    retModel.SupplierName = item.Media;//媒体主体
                                    
                                    int returnid = ReturnManager.Add(retModel, trans);

                                    item.ReturnId = returnid;

                                    ConsumptionManager.Update(item,trans);
                                    ret++;
                                }
                            }
                            trans.Commit();
                        }
                        catch
                        {
                            trans.Rollback();
                        }
                        finally
                        {
                            if (trans.Connection != null && trans.Connection.State != ConnectionState.Closed)
                            {
                                trans.Connection.Close();
                            }
                            if (trans != null)
                                trans = null;
                        }
                    }
                    #endregion

                    try
                    {
                        ESP.HumanResource.Entity.EmployeeBaseInfo creator = ESP.HumanResource.BusinessLogic.EmployeeBaseManager.GetModel(batchModel.CreatorID.Value);
                        
                        ESP.HumanResource.Entity.EmployeeBaseInfo nextEmp = null;
                        string nextAuditorMail = string.Empty;
                        if (nextRole != null)
                        {
                            nextEmp = ESP.HumanResource.BusinessLogic.EmployeeBaseManager.GetModel(nextRole.AuditorUserID);
                            nextAuditorMail = nextEmp.InternalEmail;
                        }

                        SendMailHelper.SendMailConsumptionAudit(batchModel, creator.InternalEmail, currentUser.Name, nextAuditorMail);
                    }
                    catch { }
                }
                else
                {
                    ret = 0;
                }
            }

            return ret;
        }

        public int AuditRebateRegistration(ESP.Finance.Entity.PNBatchInfo batchModel, ESP.Compatible.Employee currentUser, int status, string suggestion)
        {
            int ret = 0;
            string term = string.Empty;
            List<SqlParameter> paramlist = new List<SqlParameter>();
            string DelegateUsers = string.Empty;
            IList<ESP.Framework.Entity.AuditBackUpInfo> delegates = ESP.Framework.BusinessLogic.AuditBackUpManager.GetModelsByBackUpUserID(int.Parse(currentUser.SysID));
            foreach (ESP.Framework.Entity.AuditBackUpInfo model in delegates)
            {
                DelegateUsers += model.UserID.ToString() + ",";
            }
            DelegateUsers = DelegateUsers.TrimEnd(',');

            term = " batchId=@batchId and AuditStatus=@AuditStatus and formType=@formType";

            SqlParameter p1 = new SqlParameter("@batchId", SqlDbType.Int, 4);
            p1.SqlValue = batchModel.BatchID;
            paramlist.Add(p1);

            SqlParameter p2 = new SqlParameter("@formType", SqlDbType.Int, 4);
            p2.SqlValue = (int)ESP.Finance.Utility.FormType.RebateRegistration;
            paramlist.Add(p2);

            SqlParameter p3 = new SqlParameter("@AuditStatus", SqlDbType.Int, 4);
            p3.SqlValue = (int)AuditHistoryStatus.UnAuditing;
            paramlist.Add(p3);

            var auditList = ESP.Finance.BusinessLogic.ConsumptionAuditManager.GetList(term, paramlist);

            if (auditList == null || auditList.Count == 0)
            {
                ret = 0;
            }
            else
            {
                //当前审核人
                ConsumptionAuditInfo firstRole = auditList[0];
                //下一级审核人
                ConsumptionAuditInfo nextRole = null;
                if (auditList.Count >= 2)
                    nextRole = auditList[1];

                if (firstRole.AuditorUserID == int.Parse(currentUser.SysID) || DelegateUsers.IndexOf(firstRole.AuditorUserID.ToString()) >= 0)//审核人与登录人校验
                {
                    if (status == (int)AuditHistoryStatus.PassAuditing)
                    {
                        if (nextRole != null)
                        {
                            batchModel.PaymentUserID = nextRole.AuditorUserID;
                            batchModel.PaymentEmployeeName = nextRole.AuditorEmployeeName;
                            batchModel.PaymentUserName = nextRole.AuditorUserName;
                            batchModel.PaymentCode = nextRole.AuditorUserCode;
                        }
                        switch (firstRole.AuditType.Value)
                        {
                            case 2://业务第一级审批，状态不变

                                break;
                            case 5://业务第二级审批后到财务第一级审批
                                batchModel.Status = (int)PaymentStatus.MajorAudit;
                                break;
                            case 11://财务第一级审批后到第二级审批
                                batchModel.Status = (int)PaymentStatus.FinanceLevel1;
                                break;
                            case 13://财务第二级审批end
                                batchModel.Status = (int)PaymentStatus.FinanceComplete;
                                break;
                        }
                    }
                    else
                    {
                        batchModel.Status = (int)PaymentStatus.Rejected;
                    }

                    //更新当前审批流角色状态
                    firstRole.AuditStatus = status;
                    firstRole.AuditDate = DateTime.Now;
                    firstRole.Suggestion = suggestion;

                    RebateRegistrationDataProvider provider = new RebateRegistrationDataProvider();

                    List<RebateRegistrationInfo> details = provider.GetList(" a.BatchId=" + batchModel.BatchID, null).ToList();

                    #region begin transaction
                    using (SqlConnection conn = new SqlConnection(DbHelperSQL.connectionString))
                    {
                       
                        conn.Open();
                        SqlTransaction trans = conn.BeginTransaction();
                        try
                        {
                            ESP.Finance.BusinessLogic.PNBatchManager.Update(batchModel, trans);//更新批次状态
                            ret++;
                            ESP.Finance.BusinessLogic.ConsumptionAuditManager.Update(firstRole, trans, Utility.FormType.RebateRegistration);//更新工作流状态
                            ret++;
                            //如果是财务最后一级审批，更新返点明细状态
                            if (batchModel.Status == (int)PaymentStatus.FinanceLevel1)
                            {
                                
                                foreach (var detail in details)
                                {
                                    detail.Status = Common.RebateRegistration_Status.Audited;
                                    provider.Update(detail,trans);
                                }
                            }
                            trans.Commit();
                        }
                        catch
                        {
                            trans.Rollback();
                        }
                        finally
                        {
                            if (trans.Connection != null && trans.Connection.State != ConnectionState.Closed)
                            {
                                trans.Connection.Close();
                            }
                            if (trans != null)
                                trans = null;
                        }
                    }
                    #endregion

                    try
                    {
                        ESP.HumanResource.Entity.EmployeeBaseInfo creator = ESP.HumanResource.BusinessLogic.EmployeeBaseManager.GetModel(batchModel.CreatorID.Value);

                        ESP.HumanResource.Entity.EmployeeBaseInfo nextEmp = null;
                        string nextAuditorMail = string.Empty;
                        if (nextRole != null)
                        {
                            nextEmp = ESP.HumanResource.BusinessLogic.EmployeeBaseManager.GetModel(nextRole.AuditorUserID);
                            nextAuditorMail = nextEmp.InternalEmail;
                        }

                        SendMailHelper.SendMailRebateRegistrationAudit(batchModel, creator.InternalEmail, currentUser.Name, nextAuditorMail);
                    }
                    catch { }
                }
                else
                {
                    ret = 0;
                }
            }

            return ret;
        }
        #endregion  成员方法
    }
}
