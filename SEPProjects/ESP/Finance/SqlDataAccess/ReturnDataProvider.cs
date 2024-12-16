using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using ESP.Finance.Entity;
using System.Collections.Generic;
using ESP.Finance.Utility;
using ESP.Purchase.BusinessLogic;
//using Maticsoft.DBUtility;//请先添加引用
namespace ESP.Finance.DataAccess
{
    /// <summary>
    /// 数据访问类ReturnDAL。
    /// </summary>
    internal class ReturnDataProvider : ESP.Finance.IDataAccess.IReturnDataProvider
    {

        #region  成员方法

        public int SettingOOPCurrentAudit(string returncode)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update F_Return set ");
            strSql.Append(" lastauditpasstime=@lastauditpasstime ");
            strSql.Append(" where ReturnCode=@ReturnCode ");
            SqlParameter[] parameters = {
					new SqlParameter("@lastauditpasstime", SqlDbType.DateTime),
					new SqlParameter("@ReturnCode", SqlDbType.NVarChar,10)};
            parameters[0].Value = DateTime.Now.AddMonths(-2);
            parameters[1].Value = returncode;
            return DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int ReturnID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from F_Return");
            strSql.Append(" where ReturnID=@ReturnID ");
            SqlParameter[] parameters = {
					new SqlParameter("@ReturnID", SqlDbType.Int,4)};
            parameters[0].Value = ReturnID;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }

        public int UpdateIsInvoice(int ReturnID, int IsInvoice)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update F_Return set ");
            strSql.Append(" IsInvoice=@IsInvoice ");
            strSql.Append(" where ReturnID=@ReturnID ");
            SqlParameter[] parameters = {
					new SqlParameter("@IsInvoice", SqlDbType.Int,4),
					new SqlParameter("@ReturnID", SqlDbType.Int,4)};
            parameters[0].Value = IsInvoice;
            parameters[1].Value = ReturnID;
            return DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }

        public int Add(List<ESP.Finance.Entity.ReturnInfo> returnList)
        {
            int count = 0;
            using (SqlConnection conn = new SqlConnection(DbHelperSQL.connectionString))
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();
                try
                {
                    foreach (ESP.Finance.Entity.ReturnInfo model in returnList)
                    {
                        if (Add(model, trans) > 0)
                            count++;
                    }
                    trans.Commit();
                }
                catch
                {
                    trans.Rollback();
                }
                if (count != returnList.Count)
                {
                    trans.Rollback();
                    count = 0;
                }
                return count;
            }
        }
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(ESP.Finance.Entity.ReturnInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into F_Return(");
            strSql.Append("PreFee,FactFee,DeferFee,DeferDay,ReturnStatus,IsInvoice,InvoiceNo,InvoiceDate,BranchID,BranchCode,ReturnCode,BranchName,PurchasePayID,PRID,PRNo,PaymentUserID,PaymentCode,PaymentEmployeeName,PaymentUserName,RequestorID,ProjectID,RequestUserCode,RequestUserName,RequestEmployeeName,RequestDate,WorkItemID,WorkItemName,ProcessID,InstanceID,Attachment,BankID,ProjectCode,BankName,DBCode,DBManager,BankAccount,PhoneNo,ExchangeNo,RequestPhone,PaymentTypeID,PaymentTypeName,PaymentTypeCode,ReturnPreDate,PreBeginDate,PreEndDate,ReturnFactDate,ReturnContent,BankAccountName,BankAddress,ReturnType,LastUpdateDateTime,MediaOrderIDs,SupplierName,SupplierBankName,SupplierBankAccount,RePaymentSuggestion,NeedPurchaseAudit,DepartmentID,DepartmentName,Remark,CommitDate,ParentID,LastAuditPassTime,RecipientIds,FaAuditPassTime,LastAuditUserName,FaAuditUserName,LastAuditUserID,FaAuditUserID,IsFixCheque,IsDiscount,DiscountDate,TicketNo,TicketSupplierId,ReceptionId,ReciptionDate,IsMediaOrder)");
            strSql.Append(" values (");
            strSql.Append("@PreFee,@FactFee,@DeferFee,@DeferDay,@ReturnStatus,@IsInvoice,@InvoiceNo,@InvoiceDate,@BranchID,@BranchCode,@ReturnCode,@BranchName,@PurchasePayID,@PRID,@PRNo,@PaymentUserID,@PaymentCode,@PaymentEmployeeName,@PaymentUserName,@RequestorID,@ProjectID,@RequestUserCode,@RequestUserName,@RequestEmployeeName,@RequestDate,@WorkItemID,@WorkItemName,@ProcessID,@InstanceID,@Attachment,@BankID,@ProjectCode,@BankName,@DBCode,@DBManager,@BankAccount,@PhoneNo,@ExchangeNo,@RequestPhone,@PaymentTypeID,@PaymentTypeName,@PaymentTypeCode,@ReturnPreDate,@PreBeginDate,@PreEndDate,@ReturnFactDate,@ReturnContent,@BankAccountName,@BankAddress,@ReturnType,getDate(),@MediaOrderIDs,@SupplierName,@SupplierBankName,@SupplierBankAccount,@RePaymentSuggestion,@NeedPurchaseAudit,@DepartmentID,@DepartmentName,@Remark,@CommitDate,@ParentID,@LastAuditPassTime,@RecipientIds,@FaAuditPassTime,@LastAuditUserName,@FaAuditUserName,@LastAuditUserID,@FaAuditUserID,@IsFixCheque,@IsDiscount,@DiscountDate,@TicketNo,@TicketSupplierId,@ReceptionId,@ReciptionDate,@IsMediaOrder)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@PreFee", SqlDbType.Decimal,9),
					new SqlParameter("@FactFee", SqlDbType.Decimal,9),
					new SqlParameter("@DeferFee", SqlDbType.Decimal,9),
					new SqlParameter("@DeferDay", SqlDbType.Int,4),
					new SqlParameter("@ReturnStatus", SqlDbType.Int,4),
					new SqlParameter("@IsInvoice", SqlDbType.Int,4),
					new SqlParameter("@InvoiceNo", SqlDbType.NVarChar,50),
					new SqlParameter("@InvoiceDate", SqlDbType.DateTime),
					new SqlParameter("@BranchID", SqlDbType.Int,4),
					new SqlParameter("@BranchCode", SqlDbType.NVarChar,10),
					new SqlParameter("@ReturnCode", SqlDbType.NVarChar,50),
					new SqlParameter("@BranchName", SqlDbType.NVarChar,200),
					new SqlParameter("@PurchasePayID", SqlDbType.Int,4),
					new SqlParameter("@PRID", SqlDbType.Int,4),
					new SqlParameter("@PRNo", SqlDbType.NVarChar,50),
					new SqlParameter("@PaymentUserID", SqlDbType.Int,4),
					new SqlParameter("@PaymentCode", SqlDbType.NVarChar,50),
					new SqlParameter("@PaymentEmployeeName", SqlDbType.NVarChar,50),
					new SqlParameter("@PaymentUserName", SqlDbType.NVarChar,50),
					new SqlParameter("@RequestorID", SqlDbType.Int,4),
					new SqlParameter("@ProjectID", SqlDbType.Int,4),
					new SqlParameter("@RequestUserCode", SqlDbType.NVarChar,50),
					new SqlParameter("@RequestUserName", SqlDbType.NVarChar,50),
					new SqlParameter("@RequestEmployeeName", SqlDbType.NVarChar,50),
					new SqlParameter("@RequestDate", SqlDbType.DateTime),
					new SqlParameter("@WorkItemID", SqlDbType.Int,4),
					new SqlParameter("@WorkItemName", SqlDbType.NVarChar,50),
					new SqlParameter("@ProcessID", SqlDbType.Int,4),
					new SqlParameter("@InstanceID", SqlDbType.Int,4),
					new SqlParameter("@Attachment", SqlDbType.NVarChar,200),
					new SqlParameter("@BankID", SqlDbType.Int,4),
					new SqlParameter("@ProjectCode", SqlDbType.NVarChar,50),
					new SqlParameter("@BankName", SqlDbType.NVarChar,100),
					new SqlParameter("@DBCode", SqlDbType.NVarChar,10),
					new SqlParameter("@DBManager", SqlDbType.NVarChar,10),
					new SqlParameter("@BankAccount", SqlDbType.NVarChar,50),
					new SqlParameter("@PhoneNo", SqlDbType.NVarChar,50),
					new SqlParameter("@ExchangeNo", SqlDbType.NVarChar,50),
					new SqlParameter("@RequestPhone", SqlDbType.NVarChar,50),
					new SqlParameter("@PaymentTypeID", SqlDbType.Int,4),
					new SqlParameter("@PaymentTypeName", SqlDbType.NVarChar,50),
					new SqlParameter("@PaymentTypeCode", SqlDbType.NVarChar,50),
					new SqlParameter("@ReturnPreDate", SqlDbType.DateTime),
					new SqlParameter("@PreBeginDate", SqlDbType.DateTime),
					new SqlParameter("@PreEndDate", SqlDbType.DateTime),
					new SqlParameter("@ReturnFactDate", SqlDbType.DateTime),
                    new SqlParameter("@ReturnContent", SqlDbType.NVarChar,500),
					new SqlParameter("@BankAccountName", SqlDbType.NVarChar,50),
                    new SqlParameter("@BankAddress", SqlDbType.NVarChar,100),
                    new SqlParameter("@ReturnType", SqlDbType.Int,4),
                    new SqlParameter("@MediaOrderIDs",SqlDbType.NVarChar,4000),
                    new SqlParameter("@SupplierName",SqlDbType.NVarChar,200),
                    new SqlParameter("@SupplierBankName",SqlDbType.NVarChar,100),
                    new SqlParameter("@SupplierBankAccount",SqlDbType.NVarChar,50),
                    new SqlParameter("@RePaymentSuggestion",SqlDbType.NVarChar,500),
                    new SqlParameter("@NeedPurchaseAudit",SqlDbType.Bit),
                    new SqlParameter("@DepartmentID",SqlDbType.Int,4),
                    new SqlParameter("@DepartmentName",SqlDbType.NVarChar,30),
                    new SqlParameter("@Remark",SqlDbType.NVarChar,2000),
                    new SqlParameter("@CommitDate",SqlDbType.DateTime),
                    new SqlParameter("@ParentID",SqlDbType.Int,4),
                    new SqlParameter("@LastAuditPassTime",SqlDbType.DateTime),
                    new SqlParameter("@RecipientIds",SqlDbType.NVarChar,500),
                    new SqlParameter("@FaAuditPassTime",SqlDbType.DateTime),
                    new SqlParameter("@LastAuditUserName",SqlDbType.NVarChar,50),
                    new SqlParameter("@FaAuditUserName",SqlDbType.NVarChar,50),
                    new SqlParameter("@LastAuditUserID",SqlDbType.Int,4),
                    new SqlParameter("@FaAuditUserID",SqlDbType.Int,4),
                    new SqlParameter("@IsFixCheque",SqlDbType.Bit),
                    new SqlParameter("@Discount",SqlDbType.Bit),
                    new SqlParameter("@DiscountDate",SqlDbType.DateTime),
                    new SqlParameter("@TicketNo",SqlDbType.Int),
                    new SqlParameter("@TicketSupplierId",SqlDbType.Int),
                    new SqlParameter("@ReceptionId",SqlDbType.Int),
                    new SqlParameter("@ReciptionDate",SqlDbType.DateTime),
                     new SqlParameter("@IsMediaOrder",SqlDbType.Int)
                                        };
            parameters[0].Value = model.PreFee;
            parameters[1].Value = model.FactFee;
            parameters[2].Value = model.DeferFee;
            parameters[3].Value = model.DeferDay;
            parameters[4].Value = model.ReturnStatus;
            parameters[5].Value = model.IsInvoice;
            parameters[6].Value = model.InvoiceNo;
            parameters[7].Value = model.InvoiceDate;
            parameters[8].Value = model.BranchID;
            if (string.IsNullOrEmpty(model.BranchCode))
                parameters[9].Value = model.ProjectCode.Substring(0, 1);
            else
                parameters[9].Value = model.BranchCode;
            parameters[10].Value = model.ReturnCode;
            parameters[11].Value = model.BranchName;
            parameters[12].Value = model.PurchasePayID;
            parameters[13].Value = model.PRID;
            parameters[14].Value = model.PRNo;
            parameters[15].Value = model.PaymentUserID;
            parameters[16].Value = model.PaymentCode;
            parameters[17].Value = model.PaymentEmployeeName;
            parameters[18].Value = model.PaymentUserName;
            parameters[19].Value = model.RequestorID;
            parameters[20].Value = model.ProjectID;
            parameters[21].Value = model.RequestUserCode;
            parameters[22].Value = model.RequestUserName;
            parameters[23].Value = model.RequestEmployeeName;
            parameters[24].Value = model.RequestDate;
            parameters[25].Value = model.WorkItemID;
            parameters[26].Value = model.WorkItemName;
            parameters[27].Value = model.ProcessID;
            parameters[28].Value = model.InstanceID;
            parameters[29].Value = model.Attachment;
            parameters[30].Value = model.BankID;
            parameters[31].Value = model.ProjectCode;
            parameters[32].Value = model.BankName;
            parameters[33].Value = model.DBCode;
            parameters[34].Value = model.DBManager;
            parameters[35].Value = model.BankAccount;
            parameters[36].Value = model.PhoneNo;
            parameters[37].Value = model.ExchangeNo;
            parameters[38].Value = model.RequestPhone;
            parameters[39].Value = model.PaymentTypeID;
            parameters[40].Value = model.PaymentTypeName;
            parameters[41].Value = model.PaymentTypeCode;
            parameters[42].Value = model.ReturnPreDate;
            parameters[43].Value = model.PreBeginDate;
            parameters[44].Value = model.PreEndDate;
            parameters[45].Value = model.ReturnFactDate;
            parameters[46].Value = model.ReturnContent;
            parameters[47].Value = model.BankAccountName;
            parameters[48].Value = model.BankAddress;
            parameters[49].Value = model.ReturnType;
            parameters[50].Value = model.MediaOrderIDs;
            parameters[51].Value = model.SupplierName;
            parameters[52].Value = model.SupplierBankName;
            parameters[53].Value = model.SupplierBankAccount;
            parameters[54].Value = model.RePaymentSuggestion == null ? "" : model.RePaymentSuggestion;
            parameters[55].Value = model.NeedPurchaseAudit;
            parameters[56].Value = model.DepartmentID;
            parameters[57].Value = model.DepartmentName;
            parameters[58].Value = model.Remark;
            parameters[59].Value = model.CommitDate;
            parameters[60].Value = model.ParentID;
            parameters[61].Value = model.LastAuditPassTime;
            parameters[62].Value = model.RecipientIds;
            parameters[63].Value = model.FaAuditPassTime;
            parameters[64].Value = model.LastAuditUserName;
            parameters[65].Value = model.FaAuditUserName;
            parameters[66].Value = model.LastAuditUserID;
            parameters[67].Value = model.FaAuditUserID;
            parameters[68].Value = model.IsFixCheque;
            parameters[69].Value = model.IsDiscount;
            parameters[70].Value = model.DiscountDate;
            parameters[71].Value = model.TicketNo;
            parameters[72].Value = model.TicketSupplierId;
            parameters[73].Value = model.ReceptionId;
            parameters[74].Value = model.ReciptionDate;
            parameters[75].Value = model.IsMediaOrder;

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

        public int Add(ESP.Finance.Entity.ReturnInfo model, System.Data.SqlClient.SqlTransaction trans)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into F_Return(");
            strSql.Append("PreFee,FactFee,DeferFee,DeferDay,ReturnStatus,IsInvoice,InvoiceNo,InvoiceDate,BranchID,BranchCode,ReturnCode,BranchName,PurchasePayID,PRID,PRNo,PaymentUserID,PaymentCode,PaymentEmployeeName,PaymentUserName,RequestorID,ProjectID,RequestUserCode,RequestUserName,RequestEmployeeName,RequestDate,WorkItemID,WorkItemName,ProcessID,InstanceID,Attachment,BankID,ProjectCode,BankName,DBCode,DBManager,BankAccount,PhoneNo,ExchangeNo,RequestPhone,PaymentTypeID,PaymentTypeName,PaymentTypeCode,ReturnPreDate,PreBeginDate,PreEndDate,ReturnFactDate,ReturnContent,BankAccountName,BankAddress,ReturnType,LastUpdateDateTime,MediaOrderIDs,SupplierName,SupplierBankName,SupplierBankAccount,RePaymentSuggestion,NeedPurchaseAudit,DepartmentID,DepartmentName,Remark,CommitDate,ParentID,LastAuditPassTime,RecipientIds,FaAuditPassTime,LastAuditUserName,FaAuditUserName,LastAuditUserID,FaAuditUserID,IsFixCheque,IsDiscount,DiscountDate,TicketNo,TicketSupplierId,ReceptionId,ReciptionDate,IsMediaOrder)");
            strSql.Append(" values (");
            strSql.Append("@PreFee,@FactFee,@DeferFee,@DeferDay,@ReturnStatus,@IsInvoice,@InvoiceNo,@InvoiceDate,@BranchID,@BranchCode,@ReturnCode,@BranchName,@PurchasePayID,@PRID,@PRNo,@PaymentUserID,@PaymentCode,@PaymentEmployeeName,@PaymentUserName,@RequestorID,@ProjectID,@RequestUserCode,@RequestUserName,@RequestEmployeeName,@RequestDate,@WorkItemID,@WorkItemName,@ProcessID,@InstanceID,@Attachment,@BankID,@ProjectCode,@BankName,@DBCode,@DBManager,@BankAccount,@PhoneNo,@ExchangeNo,@RequestPhone,@PaymentTypeID,@PaymentTypeName,@PaymentTypeCode,@ReturnPreDate,@PreBeginDate,@PreEndDate,@ReturnFactDate,@ReturnContent,@BankAccountName,@BankAddress,@ReturnType,getDate(),@MediaOrderIDs,@SupplierName,@SupplierBankName,@SupplierBankAccount,@RePaymentSuggestion,@NeedPurchaseAudit,@DepartmentID,@DepartmentName,@Remark,@CommitDate,@ParentID,@LastAuditPassTime,@RecipientIds,@FaAuditPassTime,@LastAuditUserName,@FaAuditUserName,@LastAuditUserID,@FaAuditUserID,@IsFixCheque,@IsDiscount,@DiscountDate,@TicketNo,@TicketSupplierId,@ReceptionId,@ReciptionDate,@IsMediaOrder)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@PreFee", SqlDbType.Decimal,9),
					new SqlParameter("@FactFee", SqlDbType.Decimal,9),
					new SqlParameter("@DeferFee", SqlDbType.Decimal,9),
					new SqlParameter("@DeferDay", SqlDbType.Int,4),
					new SqlParameter("@ReturnStatus", SqlDbType.Int,4),
					new SqlParameter("@IsInvoice", SqlDbType.Int,4),
					new SqlParameter("@InvoiceNo", SqlDbType.NVarChar,50),
					new SqlParameter("@InvoiceDate", SqlDbType.DateTime),
					new SqlParameter("@BranchID", SqlDbType.Int,4),
					new SqlParameter("@BranchCode", SqlDbType.NVarChar,10),
					new SqlParameter("@ReturnCode", SqlDbType.NVarChar,50),
					new SqlParameter("@BranchName", SqlDbType.NVarChar,200),
					new SqlParameter("@PurchasePayID", SqlDbType.Int,4),
					new SqlParameter("@PRID", SqlDbType.Int,4),
					new SqlParameter("@PRNo", SqlDbType.NVarChar,50),
					new SqlParameter("@PaymentUserID", SqlDbType.Int,4),
					new SqlParameter("@PaymentCode", SqlDbType.NVarChar,50),
					new SqlParameter("@PaymentEmployeeName", SqlDbType.NVarChar,50),
					new SqlParameter("@PaymentUserName", SqlDbType.NVarChar,50),
					new SqlParameter("@RequestorID", SqlDbType.Int,4),
					new SqlParameter("@ProjectID", SqlDbType.Int,4),
					new SqlParameter("@RequestUserCode", SqlDbType.NVarChar,50),
					new SqlParameter("@RequestUserName", SqlDbType.NVarChar,50),
					new SqlParameter("@RequestEmployeeName", SqlDbType.NVarChar,50),
					new SqlParameter("@RequestDate", SqlDbType.DateTime),
					new SqlParameter("@WorkItemID", SqlDbType.Int,4),
					new SqlParameter("@WorkItemName", SqlDbType.NVarChar,50),
					new SqlParameter("@ProcessID", SqlDbType.Int,4),
					new SqlParameter("@InstanceID", SqlDbType.Int,4),
					new SqlParameter("@Attachment", SqlDbType.NVarChar,200),
					new SqlParameter("@BankID", SqlDbType.Int,4),
					new SqlParameter("@ProjectCode", SqlDbType.NVarChar,50),
					new SqlParameter("@BankName", SqlDbType.NVarChar,100),
					new SqlParameter("@DBCode", SqlDbType.NVarChar,10),
					new SqlParameter("@DBManager", SqlDbType.NVarChar,10),
					new SqlParameter("@BankAccount", SqlDbType.NVarChar,50),
					new SqlParameter("@PhoneNo", SqlDbType.NVarChar,50),
					new SqlParameter("@ExchangeNo", SqlDbType.NVarChar,50),
					new SqlParameter("@RequestPhone", SqlDbType.NVarChar,50),
					new SqlParameter("@PaymentTypeID", SqlDbType.Int,4),
					new SqlParameter("@PaymentTypeName", SqlDbType.NVarChar,50),
					new SqlParameter("@PaymentTypeCode", SqlDbType.NVarChar,50),
					new SqlParameter("@ReturnPreDate", SqlDbType.DateTime),
					new SqlParameter("@PreBeginDate", SqlDbType.DateTime),
					new SqlParameter("@PreEndDate", SqlDbType.DateTime),
					new SqlParameter("@ReturnFactDate", SqlDbType.DateTime),
                    new SqlParameter("@ReturnContent", SqlDbType.NVarChar,500),
					new SqlParameter("@BankAccountName", SqlDbType.NVarChar,50),
                    new SqlParameter("@BankAddress", SqlDbType.NVarChar,100),
                    new SqlParameter("@ReturnType", SqlDbType.Int,4),
                    new SqlParameter("@MediaOrderIDs",SqlDbType.NVarChar,4000),
                    new SqlParameter("@SupplierName",SqlDbType.NVarChar,200),
                    new SqlParameter("@SupplierBankName",SqlDbType.NVarChar,100),
                    new SqlParameter("@SupplierBankAccount",SqlDbType.NVarChar,50),
                    new SqlParameter("@RePaymentSuggestion",SqlDbType.NVarChar,500),
                    new SqlParameter("@NeedPurchaseAudit",SqlDbType.Bit),
                    new SqlParameter("@DepartmentID",SqlDbType.Int,4),
                    new SqlParameter("@DepartmentName",SqlDbType.NVarChar,30),
                    new SqlParameter("@Remark",SqlDbType.NVarChar,2000),
                    new SqlParameter("@CommitDate",SqlDbType.DateTime),
                    new SqlParameter("@ParentID",SqlDbType.Int,4),
                    new SqlParameter("@LastAuditPassTime",SqlDbType.DateTime),
                    new SqlParameter("@RecipientIds",SqlDbType.NVarChar,500),
                    new SqlParameter("@FaAuditPassTime",SqlDbType.DateTime),
                    new SqlParameter("@LastAuditUserName",SqlDbType.NVarChar,50),
                    new SqlParameter("@FaAuditUserName",SqlDbType.NVarChar,50),
                    new SqlParameter("@LastAuditUserID",SqlDbType.Int,4),
                    new SqlParameter("@FaAuditUserID",SqlDbType.Int,4),
                    new SqlParameter("@IsFixCheque",SqlDbType.Bit),
                    new SqlParameter("@IsDiscount",SqlDbType.Bit),
                    new SqlParameter("@DiscountDate",SqlDbType.DateTime),
                    new SqlParameter("@TicketNo",SqlDbType.Int),
                    new SqlParameter("@TicketSupplierId",SqlDbType.Int),
                    new SqlParameter("@ReceptionId",SqlDbType.Int),
                    new SqlParameter("@ReciptionDate",SqlDbType.DateTime),
                    new SqlParameter("@IsMediaOrder",SqlDbType.Int)
                                        };
            parameters[0].Value = model.PreFee;
            parameters[1].Value = model.FactFee;
            parameters[2].Value = model.DeferFee;
            parameters[3].Value = model.DeferDay;
            parameters[4].Value = model.ReturnStatus;
            parameters[5].Value = model.IsInvoice;
            parameters[6].Value = model.InvoiceNo;
            parameters[7].Value = model.InvoiceDate;
            parameters[8].Value = model.BranchID;
            if (string.IsNullOrEmpty(model.BranchCode))
                parameters[9].Value = model.ProjectCode.Substring(0, 1);
            else
                parameters[9].Value = model.BranchCode;
            parameters[10].Value = model.ReturnCode;
            parameters[11].Value = model.BranchName;
            parameters[12].Value = model.PurchasePayID;
            parameters[13].Value = model.PRID;
            parameters[14].Value = model.PRNo;
            parameters[15].Value = model.PaymentUserID;
            parameters[16].Value = model.PaymentCode;
            parameters[17].Value = model.PaymentEmployeeName;
            parameters[18].Value = model.PaymentUserName;
            parameters[19].Value = model.RequestorID;
            parameters[20].Value = model.ProjectID;
            parameters[21].Value = model.RequestUserCode;
            parameters[22].Value = model.RequestUserName;
            parameters[23].Value = model.RequestEmployeeName;
            parameters[24].Value = model.RequestDate;
            parameters[25].Value = model.WorkItemID;
            parameters[26].Value = model.WorkItemName;
            parameters[27].Value = model.ProcessID;
            parameters[28].Value = model.InstanceID;
            parameters[29].Value = model.Attachment;
            parameters[30].Value = model.BankID;
            parameters[31].Value = model.ProjectCode;
            parameters[32].Value = model.BankName;
            parameters[33].Value = model.DBCode;
            parameters[34].Value = model.DBManager;
            parameters[35].Value = model.BankAccount;
            parameters[36].Value = model.PhoneNo;
            parameters[37].Value = model.ExchangeNo;
            parameters[38].Value = model.RequestPhone;
            parameters[39].Value = model.PaymentTypeID;
            parameters[40].Value = model.PaymentTypeName;
            parameters[41].Value = model.PaymentTypeCode;
            parameters[42].Value = model.ReturnPreDate;
            parameters[43].Value = model.PreBeginDate;
            parameters[44].Value = model.PreEndDate;
            parameters[45].Value = model.ReturnFactDate;
            parameters[46].Value = model.ReturnContent;
            parameters[47].Value = model.BankAccountName;
            parameters[48].Value = model.BankAddress;
            parameters[49].Value = model.ReturnType;
            parameters[50].Value = model.MediaOrderIDs;
            parameters[51].Value = model.SupplierName;
            parameters[52].Value = model.SupplierBankName;
            parameters[53].Value = model.SupplierBankAccount;
            parameters[54].Value = model.RePaymentSuggestion == null ? "" : model.RePaymentSuggestion;
            parameters[55].Value = model.NeedPurchaseAudit;
            parameters[56].Value = model.DepartmentID;
            parameters[57].Value = model.DepartmentName;
            parameters[58].Value = model.Remark;
            parameters[59].Value = model.CommitDate;
            parameters[60].Value = model.ParentID;
            parameters[61].Value = model.LastAuditPassTime;
            parameters[62].Value = model.RecipientIds;
            parameters[63].Value = model.FaAuditPassTime;
            parameters[64].Value = model.LastAuditUserName;
            parameters[65].Value = model.FaAuditUserName;
            parameters[66].Value = model.LastAuditUserID;
            parameters[67].Value = model.FaAuditUserID;
            parameters[68].Value = model.IsFixCheque;
            parameters[69].Value = model.IsDiscount;
            parameters[70].Value = model.DiscountDate;
            parameters[71].Value = model.TicketNo;
            parameters[72].Value = model.TicketSupplierId;
            parameters[73].Value = model.ReceptionId;
            parameters[74].Value = model.ReciptionDate;
            parameters[75].Value = model.IsMediaOrder;

            object obj = DbHelperSQL.GetSingle(strSql.ToString(), trans, parameters);
            if (obj == null)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(obj);
            }
        }
        /// <summary>
        /// 根据项目ID更新项目号
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="projectCode"></param>
        /// <param name="isInTrans"></param>
        /// <returns></returns>
        public int UpdateProjectCode(int projectId, string projectCode)
        {
            return UpdateProjectCode(projectId, projectCode, null);
        }

        /// <summary>
        /// 根据项目ID更新项目号
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="projectCode"></param>
        /// <param name="isInTrans"></param>
        /// <returns></returns>
        public int UpdateProjectCode(int projectId, string projectCode, SqlTransaction trans)
        {
            IList<ESP.Finance.Entity.ReturnInfo> returnList = GetList(" projectId=" + projectId.ToString(), new List<SqlParameter>());
            if (returnList != null && returnList.Count != 0)
            {
                foreach (ESP.Finance.Entity.ReturnInfo ret in returnList)
                {
                    if (ret.ProjectCode != projectCode)
                    {
                        ESP.Finance.Entity.AuditLogInfo log = new AuditLogInfo();
                        log.AuditDate = DateTime.Now;
                        log.AuditorEmployeeName = "";
                        log.AuditorSysID = 0;
                        log.AuditorUserCode = "";
                        log.AuditorUserName = "";
                        log.AuditStatus = (int)ESP.Finance.Utility.AuditHistoryStatus.PassAuditing;
                        log.FormID = ret.ReturnID;
                        log.FormType = (int)ESP.Finance.Utility.FormType.Return;
                        log.Suggestion = "<font color='red'>原项目号:" + ret.ProjectCode + "</font>";
                        ESP.Finance.BusinessLogic.AuditLogManager.Add(log, trans);
                    }
                }
            }
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update F_Return set ");
            strSql.Append(" ProjectCode=@ProjectCode ");
            strSql.Append(" where ProjectID=@ProjectID ");
            SqlParameter[] parameters = {
					new SqlParameter("@ProjectID", SqlDbType.Int,4),
					new SqlParameter("@ProjectCode", SqlDbType.NVarChar,50)};
            parameters[0].Value = projectId;
            parameters[1].Value = projectCode;
            return DbHelperSQL.ExecuteSql(strSql.ToString(), trans, parameters);
        }

        public DataTable GetProxyReturnList(string term)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select returncode,projectcode,prebegindate,requestemployeename,b.code,c.departmentname,a.suppliername,a.returncontent,a.prefee from ");
            strSql.Append(" f_return a join sep_employees b on a.requestorid=b.userid ");
            strSql.Append(" join sep_departments c on a.departmentid=c.departmentid ");
            strSql.Append(" where returnid in(");
            strSql.Append(" select returnid from f_pnbatchrelation where batchid in(");
            strSql.Append(" select batchid from f_pnbatch where batchtype=1 and (purchasebatchcode is not null and purchasebatchcode!='')");
            if (!string.IsNullOrEmpty(term))
            {
                strSql.Append(" and " + term);
            }
            strSql.Append(" ))");

            return DbHelperSQL.Query(strSql.ToString()).Tables[0];


        }
        //public int UpdateWorkFlow(int ReturnID, int workItemID, string workItemName, int processID, int instanceID)
        //{
        //    return UpdateWorkFlow(ReturnID, workItemID, workItemName, processID, instanceID, false);
        //}

        public int UpdateWorkFlow(int ReturnID, int workItemID, string workItemName, int processID, int instanceID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update F_Return set ");
            strSql.Append("WorkItemID=@WorkItemID,");
            strSql.Append("WorkItemName=@WorkItemName,");
            strSql.Append("ProcessID=@ProcessID,");
            strSql.Append("InstanceID=@InstanceID ");
            strSql.Append(" where ReturnID=@ReturnID ");
            SqlParameter[] parameters = {
					new SqlParameter("@ReturnID", SqlDbType.Int,4),
					new SqlParameter("@WorkItemID", SqlDbType.Int,4),
					new SqlParameter("@WorkItemName", SqlDbType.NVarChar,50),
					new SqlParameter("@ProcessID", SqlDbType.Int,4),
					new SqlParameter("@InstanceID", SqlDbType.Int,4)
                                        };
            parameters[0].Value = ReturnID;
            parameters[1].Value = workItemID;
            parameters[2].Value = workItemName;
            parameters[3].Value = processID;
            parameters[4].Value = instanceID;

            return DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }

        public int Update(ESP.Finance.Entity.ReturnInfo model)
        {
            return Update(model, null);
        }
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public int Update(ESP.Finance.Entity.ReturnInfo model, SqlTransaction trans)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update F_Return set ");
            strSql.Append("PreFee=@PreFee,");
            strSql.Append("FactFee=@FactFee,");
            strSql.Append("DeferFee=@DeferFee,");
            strSql.Append("DeferDay=@DeferDay,");
            strSql.Append("ReturnStatus=@ReturnStatus,");
            strSql.Append("IsInvoice=@IsInvoice,");
            strSql.Append("InvoiceNo=@InvoiceNo,");
            strSql.Append("InvoiceDate=@InvoiceDate,");
            strSql.Append("BranchID=@BranchID,");
            strSql.Append("BranchCode=@BranchCode,");
            strSql.Append("ReturnCode=@ReturnCode,");
            strSql.Append("BranchName=@BranchName,");
            // strSql.Append("Lastupdatetime=@Lastupdatetime,");
            strSql.Append("PurchasePayID=@PurchasePayID,");
            strSql.Append("PRID=@PRID,");
            strSql.Append("PRNo=@PRNo,");
            strSql.Append("PaymentUserID=@PaymentUserID,");
            strSql.Append("PaymentCode=@PaymentCode,");
            strSql.Append("PaymentEmployeeName=@PaymentEmployeeName,");
            strSql.Append("PaymentUserName=@PaymentUserName,");
            strSql.Append("RequestorID=@RequestorID,");
            strSql.Append("ProjectID=@ProjectID,");
            strSql.Append("RequestUserCode=@RequestUserCode,");
            strSql.Append("RequestUserName=@RequestUserName,");
            strSql.Append("RequestEmployeeName=@RequestEmployeeName,");
            strSql.Append("RequestDate=@RequestDate,");
            strSql.Append("WorkItemID=@WorkItemID,");
            strSql.Append("WorkItemName=@WorkItemName,");
            strSql.Append("ProcessID=@ProcessID,");
            strSql.Append("InstanceID=@InstanceID,");
            strSql.Append("Attachment=@Attachment,");
            strSql.Append("BankID=@BankID,");
            strSql.Append("ProjectCode=@ProjectCode,");
            strSql.Append("BankName=@BankName,");
            strSql.Append("DBCode=@DBCode,");
            strSql.Append("DBManager=@DBManager,");
            strSql.Append("BankAccount=@BankAccount,");
            strSql.Append("PhoneNo=@PhoneNo,");
            strSql.Append("ExchangeNo=@ExchangeNo,");
            strSql.Append("RequestPhone=@RequestPhone,");
            strSql.Append("PaymentTypeID=@PaymentTypeID,");
            strSql.Append("PaymentTypeName=@PaymentTypeName,");
            strSql.Append("PaymentTypeCode=@PaymentTypeCode,");
            strSql.Append("ReturnPreDate=@ReturnPreDate,");
            strSql.Append("PreBeginDate=@PreBeginDate,");
            strSql.Append("PreEndDate=@PreEndDate,");
            strSql.Append("ReturnFactDate=@ReturnFactDate,");
            strSql.Append("BankAccountName=@BankAccountName,");
            strSql.Append("BankAddress=@BankAddress,");
            strSql.Append("ReturnContent=@ReturnContent,");
            strSql.Append("ReturnType=@ReturnType,");
            strSql.Append("LastUpdateDateTime=getdate(),");
            strSql.Append("MediaOrderIDs=@MediaOrderIDs,");
            strSql.Append("SupplierName=@SupplierName,");
            strSql.Append("SupplierBankName=@SupplierBankName,");
            strSql.Append("SupplierBankAccount=@SupplierBankAccount,");
            strSql.Append("RePaymentSuggestion=@RePaymentSuggestion");
            strSql.Append(",NeedPurchaseAudit=@NeedPurchaseAudit");
            strSql.Append(",DepartmentID=@DepartmentID,DepartmentName=@DepartmentName,");
            strSql.Append("Remark=@Remark,");
            strSql.Append("CommitDate=@CommitDate,");
            strSql.Append("ParentID=@ParentID,");
            strSql.Append("LastAuditPassTime=@LastAuditPassTime,");
            strSql.Append("RecipientIds=@RecipientIds,");
            strSql.Append("FaAuditPassTime=@FaAuditPassTime, ");
            strSql.Append("LastAuditUserName=@LastAuditUserName, ");
            strSql.Append("FaAuditUserName=@FaAuditUserName, ");
            strSql.Append("LastAuditUserID=@LastAuditUserID, ");
            strSql.Append("FaAuditUserID=@FaAuditUserID, ");
            strSql.Append("IsFixCheque=@IsFixCheque,");
            strSql.Append("IsDiscount=@IsDiscount,");
            strSql.Append("DiscountDate=@DiscountDate,TicketNo=@TicketNo,");
            strSql.Append("TicketSupplierId=@TicketSupplierId,ReceptionId=@ReceptionId,ReciptionDate=@ReciptionDate,IsMediaOrder=@IsMediaOrder");

            strSql.Append(" where ReturnID=@ReturnID ");
            SqlParameter[] parameters = {
					new SqlParameter("@ReturnID", SqlDbType.Int,4),
					new SqlParameter("@PreFee", SqlDbType.Decimal,9),
					new SqlParameter("@FactFee", SqlDbType.Decimal,9),
					new SqlParameter("@DeferFee", SqlDbType.Decimal,9),
					new SqlParameter("@DeferDay", SqlDbType.Int,4),
					new SqlParameter("@ReturnStatus", SqlDbType.Int,4),
					new SqlParameter("@IsInvoice", SqlDbType.Int,4),
					new SqlParameter("@InvoiceNo", SqlDbType.NVarChar,50),
					new SqlParameter("@InvoiceDate", SqlDbType.DateTime),
					new SqlParameter("@BranchID", SqlDbType.Int,4),
					new SqlParameter("@BranchCode", SqlDbType.NVarChar,10),
					new SqlParameter("@ReturnCode", SqlDbType.NVarChar,50),
					new SqlParameter("@BranchName", SqlDbType.NVarChar,200),
					new SqlParameter("@Lastupdatetime", SqlDbType.Timestamp,8),
					new SqlParameter("@PurchasePayID", SqlDbType.Int,4),
					new SqlParameter("@PRID", SqlDbType.Int,4),
					new SqlParameter("@PRNo", SqlDbType.NVarChar,50),
					new SqlParameter("@PaymentUserID", SqlDbType.Int,4),
					new SqlParameter("@PaymentCode", SqlDbType.NVarChar,50),
					new SqlParameter("@PaymentEmployeeName", SqlDbType.NVarChar,50),
					new SqlParameter("@PaymentUserName", SqlDbType.NVarChar,50),
					new SqlParameter("@RequestorID", SqlDbType.Int,4),
					new SqlParameter("@ProjectID", SqlDbType.Int,4),
					new SqlParameter("@RequestUserCode", SqlDbType.NVarChar,50),
					new SqlParameter("@RequestUserName", SqlDbType.NVarChar,50),
					new SqlParameter("@RequestEmployeeName", SqlDbType.NVarChar,50),
					new SqlParameter("@RequestDate", SqlDbType.DateTime),
					new SqlParameter("@WorkItemID", SqlDbType.Int,4),
					new SqlParameter("@WorkItemName", SqlDbType.NVarChar,50),
					new SqlParameter("@ProcessID", SqlDbType.Int,4),
					new SqlParameter("@InstanceID", SqlDbType.Int,4),
					new SqlParameter("@Attachment", SqlDbType.NVarChar,200),
					new SqlParameter("@BankID", SqlDbType.Int,4),
					new SqlParameter("@ProjectCode", SqlDbType.NVarChar,50),
					new SqlParameter("@BankName", SqlDbType.NVarChar,100),
					new SqlParameter("@DBCode", SqlDbType.NVarChar,10),
					new SqlParameter("@DBManager", SqlDbType.NVarChar,10),
					new SqlParameter("@BankAccount", SqlDbType.NVarChar,50),
					new SqlParameter("@PhoneNo", SqlDbType.NVarChar,50),
					new SqlParameter("@ExchangeNo", SqlDbType.NVarChar,50),
					new SqlParameter("@RequestPhone", SqlDbType.NVarChar,50),
					new SqlParameter("@PaymentTypeID", SqlDbType.Int,4),
					new SqlParameter("@PaymentTypeName", SqlDbType.NVarChar,50),
					new SqlParameter("@PaymentTypeCode", SqlDbType.NVarChar,50),
					new SqlParameter("@ReturnPreDate", SqlDbType.DateTime),
					new SqlParameter("@PreBeginDate", SqlDbType.DateTime),
					new SqlParameter("@PreEndDate", SqlDbType.DateTime),
					new SqlParameter("@ReturnFactDate", SqlDbType.DateTime),
                    new SqlParameter("@BankAccountName", SqlDbType.NVarChar,50),
                    new SqlParameter("@BankAddress", SqlDbType.NVarChar,100),
					new SqlParameter("@ReturnContent", SqlDbType.NVarChar,500),
                    new SqlParameter("@ReturnType", SqlDbType.Int,4),
                    new SqlParameter("@MediaOrderIDs",SqlDbType.NVarChar,4000),
                    new SqlParameter("@SupplierName",SqlDbType.NVarChar,200),
                    new SqlParameter("@SupplierBankName",SqlDbType.NVarChar,100),
                    new SqlParameter("@SupplierBankAccount",SqlDbType.NVarChar,50),
                    new SqlParameter("@RePaymentSuggestion",SqlDbType.NVarChar,500),
                    new SqlParameter("@NeedPurchaseAudit",SqlDbType.Bit),
                    new SqlParameter("@DepartmentID",SqlDbType.Int,4),
                    new SqlParameter("@DepartmentName",SqlDbType.NVarChar,30),
                    new SqlParameter("@Remark",SqlDbType.NVarChar,2000),
                    new SqlParameter("@CommitDate",SqlDbType.DateTime),
                    new SqlParameter("@ParentID",SqlDbType.Int,4),
                    new SqlParameter("@LastAuditPassTime",SqlDbType.DateTime),
                    new SqlParameter("@RecipientIds",SqlDbType.NVarChar,500),
                    new SqlParameter("@FaAuditPassTime",SqlDbType.DateTime),
                    new SqlParameter("@LastAuditUserName",SqlDbType.NVarChar,50),
                    new SqlParameter("@FaAuditUserName",SqlDbType.NVarChar,50),
                    new SqlParameter("@LastAuditUserID",SqlDbType.Int,4),
                    new SqlParameter("@FaAuditUserID",SqlDbType.Int,4),
                    new SqlParameter("@IsFixCheque",SqlDbType.Bit),
                    new SqlParameter("@IsDiscount",SqlDbType.Bit),
                    new SqlParameter("@DiscountDate",SqlDbType.DateTime),
                    new SqlParameter("@TicketNo",SqlDbType.Int),
                    new SqlParameter("@TicketSupplierId",SqlDbType.Int),
                    new SqlParameter("@ReceptionId",SqlDbType.Int),
                    new SqlParameter("@ReciptionDate",SqlDbType.DateTime),
                     new SqlParameter("@IsMediaOrder",SqlDbType.Int)
                                        };
            parameters[0].Value = model.ReturnID;
            parameters[1].Value = model.PreFee;
            parameters[2].Value = model.FactFee;
            parameters[3].Value = model.DeferFee;
            parameters[4].Value = model.DeferDay;
            parameters[5].Value = model.ReturnStatus;
            parameters[6].Value = model.IsInvoice;
            parameters[7].Value = model.InvoiceNo;
            parameters[8].Value = model.InvoiceDate;
            parameters[9].Value = model.BranchID;
            //if (string.IsNullOrEmpty(model.BranchCode))
            parameters[10].Value = model.ProjectCode.Substring(0, 1);
            //else
            //    parameters[10].Value = model.BranchCode;
            parameters[11].Value = model.ReturnCode;
            parameters[12].Value = model.BranchName;
            parameters[13].Value = model.Lastupdatetime;
            parameters[14].Value = model.PurchasePayID;
            parameters[15].Value = model.PRID;
            parameters[16].Value = model.PRNo;
            parameters[17].Value = model.PaymentUserID;
            parameters[18].Value = model.PaymentCode;
            parameters[19].Value = model.PaymentEmployeeName;
            parameters[20].Value = model.PaymentUserName;
            parameters[21].Value = model.RequestorID;
            parameters[22].Value = model.ProjectID;
            parameters[23].Value = model.RequestUserCode;
            parameters[24].Value = model.RequestUserName;
            parameters[25].Value = model.RequestEmployeeName;
            parameters[26].Value = model.RequestDate;
            parameters[27].Value = model.WorkItemID;
            parameters[28].Value = model.WorkItemName;
            parameters[29].Value = model.ProcessID;
            parameters[30].Value = model.InstanceID;
            parameters[31].Value = model.Attachment;
            parameters[32].Value = model.BankID;
            parameters[33].Value = model.ProjectCode;
            parameters[34].Value = model.BankName;
            parameters[35].Value = model.DBCode;
            parameters[36].Value = model.DBManager;
            parameters[37].Value = model.BankAccount;
            parameters[38].Value = model.PhoneNo;
            parameters[39].Value = model.ExchangeNo;
            parameters[40].Value = model.RequestPhone;
            parameters[41].Value = model.PaymentTypeID;
            parameters[42].Value = model.PaymentTypeName;
            parameters[43].Value = model.PaymentTypeCode;
            parameters[44].Value = model.ReturnPreDate;
            parameters[45].Value = model.PreBeginDate;
            parameters[46].Value = model.PreEndDate;
            parameters[47].Value = model.ReturnFactDate;
            parameters[48].Value = model.BankAccountName;
            parameters[49].Value = model.BankAddress;
            parameters[50].Value = model.ReturnContent;
            parameters[51].Value = model.ReturnType;
            parameters[52].Value = model.MediaOrderIDs;
            parameters[53].Value = model.SupplierName;
            parameters[54].Value = model.SupplierBankName;
            parameters[55].Value = model.SupplierBankAccount;
            parameters[56].Value = model.RePaymentSuggestion;
            parameters[57].Value = model.NeedPurchaseAudit;
            parameters[58].Value = model.DepartmentID;
            parameters[59].Value = model.DepartmentName;
            parameters[60].Value = model.Remark;
            if (model.CommitDate == null && model.ReturnStatus == 2)
                parameters[61].Value = DateTime.Now;
            else
                parameters[61].Value = model.CommitDate;
            parameters[62].Value = model.ParentID;
            parameters[63].Value = model.LastAuditPassTime;
            parameters[64].Value = model.RecipientIds;
            parameters[65].Value = model.FaAuditPassTime;
            parameters[66].Value = model.LastAuditUserName;
            parameters[67].Value = model.FaAuditUserName;
            parameters[68].Value = model.LastAuditUserID;
            parameters[69].Value = model.FaAuditUserID;
            parameters[70].Value = model.IsFixCheque;
            parameters[71].Value = model.IsDiscount;
            parameters[72].Value = model.DiscountDate;
            parameters[73].Value = model.TicketNo;
            parameters[74].Value = model.TicketSupplierId;
            parameters[75].Value = model.ReceptionId;

            parameters[76].Value = model.ReciptionDate == null ? model.RequestDate : model.ReciptionDate;

            parameters[77].Value = model.IsMediaOrder;

            return DbHelperSQL.ExecuteSql(strSql.ToString(), trans, parameters);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public int Delete(int ReturnID)
        {
            int ret = 0;
            ESP.Finance.Entity.ReturnInfo returnModel = GetModel(ReturnID);
            using (SqlConnection conn = new SqlConnection(DbHelperSQL.connectionString))
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();
                try
                {
                    ret = Delete(ReturnID, trans);
                    if (ret > 0 && (returnModel.ReturnType == (int)ESP.Purchase.Common.PRTYpe.PN_ExpenseAccountPRWriteOff || returnModel.ReturnType == (int)ESP.Purchase.Common.PRTYpe.PN_ExpenseAccountWaitReceiving))//如果是PR现金冲销,删除冲销单时需要将原PN单状态该回待冲销状态
                    {
                        ESP.Finance.Entity.ReturnInfo parentModel = GetModel(returnModel.ParentID.Value, trans);
                        parentModel.ReturnStatus = (int)ESP.Finance.Utility.PaymentStatus.WaitReceiving;
                        Update(parentModel);
                    }
                    trans.Commit();
                }
                catch (Exception ex)
                {
                    // ESP.Logging.Logger.Add(ex.Message, "撤销付款申请失败", ESP.Logging.LogLevel.Error, ex);
                    trans.Rollback();
                }

            }
            return ret;
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public int Delete(int ReturnID, SqlTransaction trans)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete F_Return ");
            strSql.Append(" where ReturnID=@ReturnID ");
            SqlParameter[] parameters = {
					new SqlParameter("@ReturnID", SqlDbType.Int,4)};
            parameters[0].Value = ReturnID;

            return DbHelperSQL.ExecuteSql(strSql.ToString(), trans, parameters);
        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public ESP.Finance.Entity.ReturnInfo GetModel(int ReturnID, SqlTransaction trans)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"select * from F_Return ");
            strSql.Append(" where ReturnID=@ReturnID ");
            SqlParameter[] parameters = {
					new SqlParameter("@ReturnID", SqlDbType.Int,4)};
            parameters[0].Value = ReturnID;
            return CBO.FillObject<ReturnInfo>(DbHelperSQL.Query(strSql.ToString(), trans, parameters));

        }

        public ESP.Finance.Entity.ReturnInfo GetModel(int ReturnID)
        {
            return GetModel(ReturnID, null);
        }

        /// <summary>
        /// 撤销付款申请至采购系统
        /// </summary>
        /// <param name="returnId"></param>
        /// <returns></returns>
        public bool returnPaymentInfo(int returnId)
        {
            bool returnValue = false;
            using (SqlConnection conn = new SqlConnection(DbHelperSQL.connectionString))
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();
                try
                {
                    ESP.Finance.Entity.ReturnInfo returnModel = GetModel(returnId, trans);
                    Delete(returnId, trans); //删除F_Return信息
                    ESP.Purchase.DataAccess.PaymentPeriodDataHelper paymentHelper = new ESP.Purchase.DataAccess.PaymentPeriodDataHelper();
                    ESP.Purchase.Entity.PaymentPeriodInfo paymentModel = paymentHelper.GetModelByReturnId(returnId, trans);

                    if (paymentModel == null)
                    {
                        IList<ESP.Purchase.Entity.PaymentPeriodInfo> paymentlist = paymentHelper.GetModelList(" gid=" + returnModel.PRID.ToString(), trans);
                        if (paymentlist.Count == 1)
                            paymentModel = paymentlist[0];
                    }

                        paymentModel.Status = (int)ESP.Purchase.Common.State.PaymentStatus_save;


                        paymentModel.ReturnCode = string.Empty;


                        paymentModel.inceptPrice = 0;
                        paymentModel.ReturnId = 0;
                        paymentModel.ReturnId = 0;
                        paymentModel.ReturnCode = "";
                        PaymentPeriodManager.DeletePeriod(paymentModel, trans);

                    trans.Commit();
                    returnValue = true;

                }
                catch (Exception ex)
                {
                    trans.Rollback();
                }
            }
            return returnValue;
        }

        /// <summary>
        /// 生成新的ReturnCode
        /// </summary>
        /// <param name="trans"></param>
        /// <returns></returns>
        public string GetNewReturnCode(SqlTransaction trans)
        {
            object RccCount = null;
            string returncode = string.Empty; ;
            object NowDate = DbHelperSQL.GetSingle("Select Substring(Convert(Varchar(6),GetDate(),112),3,4);", trans.Connection, trans);
            object ReturnCount = DbHelperSQL.GetSingle("select count(ReturnCount) from dbo.F_ReturnCounter where ReturnBaseTime like '%" + NowDate.ToString() + "%';", trans.Connection, trans);
            if (Convert.ToInt32(ReturnCount) > 0)
            {
                RccCount = DbHelperSQL.GetSingle("select ReturnCount from F_ReturnCounter where ReturnBaseTime like '%" + NowDate.ToString() + "%' ", trans.Connection, trans);
                DbHelperSQL.ExecuteSql("UPDATE F_ReturnCounter SET [ReturnCount] = " + (Convert.ToInt32(RccCount) + 1).ToString() + " WHERE ReturnBaseTime like '%" + NowDate.ToString() + "%' ", trans);
            }
            else
            {
                DbHelperSQL.ExecuteSql("INSERT INTO F_ReturnCounter (ReturnBaseTime,ReturnCount) VALUES ('" + NowDate.ToString() + "',2)", trans);
                RccCount = "1";
            }
            string strRccCount = RccCount.ToString();
            while (strRccCount.Length < 4)
                strRccCount = "0" + strRccCount;
            returncode = "PN" + NowDate.ToString() + strRccCount.ToString();

            object pndistinct = DbHelperSQL.GetSingle("select count(*) from F_Return where Returncode='" + returncode + "'", trans.Connection, trans);
            if (Convert.ToInt32(pndistinct) == 0)
                return returncode;
            else
                return "";

        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public IList<ReturnInfo> GetList(string term, List<SqlParameter> param)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * FROM F_Return ");
            if (!string.IsNullOrEmpty(term))
            {
                strSql.Append(" where " + term);
            }
            strSql.Append(" order by Lastupdatetime desc ");

            using (var reader = DbHelperSQL.ExecuteReader(strSql.ToString(), param))
            {
                return MyPhotoUtility.CBO.FillCollection<ReturnInfo>(reader);
            }
        }

        public int GetRecordsCount(string strWhere, List<SqlParameter> parms)
        {

            List<ReturnInfo> list = new List<ReturnInfo>();
            StringBuilder strB = new StringBuilder();
            strB.Append(" select count(*) from f_return ");
            string sql = string.Format(strB.ToString() + " where 1=1 {0}", strWhere);

            return int.Parse(DbHelperSQL.GetSingle(sql, parms.ToArray()).ToString());
        }

        public List<ReturnInfo> GetModelListPage(int pageSize, int pageIndex,
    string strWhere, List<SqlParameter> parms)
        {

            List<ReturnInfo> list = new List<ReturnInfo>();
            StringBuilder strB = new StringBuilder();
            strB.Append("SELECT TOP (@PageSize) * FROM(");

            strB.Append(@" select ReturnID,ReturnCode,ProjectID,ProjectCode,ReturnPreDate,PreBeginDate,PreEndDate,ReturnFactDate
,PreFee,FactFee,DeferFee,DeferDay,ReturnStatus,IsInvoice,InvoiceNo,InvoiceDate,BranchID,BranchCode,BranchName
,Lastupdatetime,PurchasePayID,PRID,PRNo,PaymentUserID,PaymentCode,PaymentEmployeeName,PaymentUserName,PaymentTypeID
,PaymentTypeName,PaymentTypeCode,RequestorID,RequestUserCode,RequestUserName,RequestEmployeeName,RequestDate,WorkItemID
,WorkItemName,ProcessID,InstanceID,Attachment,BankID,BankName,DBCode,DBManager,BankAccount,PhoneNo,ExchangeNo,RequestPhone
,BankAccountName,BankAddress,ReturnType,LastUpdateDateTime,MediaOrderIDS,SupplierName,SupplierBankName,SupplierBankAccount
,RePaymentSuggestion,FinanceHoldStatus,CreditCode,NeedPurchaseAudit,DepartmentId,DepartmentName,Remark,CommitDate,ParentID
,LastAuditPassTime,RecipientIDs,FaAuditPassTime,LastAuditUserName,LastAuditUserID,FaAuditUserName,FaAuditUserID,IsFixCheque
,IsDiscount,DiscountDate,TicketNo,TicketSupplierId,ReceptionId,ReciptionDate,returncontent,ROW_NUMBER() OVER (ORDER BY returncode desc) AS [__i_RowNumber] from f_return ");

            string sql = string.Format(strB.ToString() + " where 1=1 {0} ", strWhere);
            sql += ") t WHERE t.[__i_RowNumber] > @PageStart order by returncode desc";
            SqlParameter psize = new SqlParameter("@PageSize", pageSize);
            SqlParameter pstart = new SqlParameter("@PageStart", pageIndex * pageSize);
            parms.Add(psize);
            parms.Add(pstart);
            return ESP.Finance.Utility.CBO.FillCollection<ReturnInfo>(ESP.Finance.DataAccess.DbHelperSQL.Query(sql, parms.ToArray()));
        }




        public int DeleteWorkFlow(int returnid)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete wf_workitems ");
            strSql.Append(" where entityid=@entityid ");
            SqlParameter[] parameters = {
					new SqlParameter("@entityid", SqlDbType.Int,4)};
            parameters[0].Value = returnid;

            return DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }

        public IList<ReturnInfo> GetTicketBatch(int batchid)
        {
            string strWhere = string.Format("select * from f_return where returnid in(select returnid from f_pnbatchrelation where batchid ={0}) order by ReciptionDate asc", batchid);

            return CBO.FillCollection<ReturnInfo>(DbHelperSQL.Query(strWhere));
        }

        public IList<ReturnInfo> GetList(string term, SqlTransaction trans)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * FROM F_Return ");
            if (!string.IsNullOrEmpty(term))
            {
                strSql.Append(" where " + term);
            }
            strSql.Append(" order by Lastupdatetime desc ");

            return CBO.FillCollection<ReturnInfo>(DbHelperSQL.Query(strSql.ToString(), trans, null));
        }

        /// <summary>
        /// 获得关联PR的PN信息列表
        /// </summary>
        /// <param name="terms"></param>
        /// <param name="parms"></param>
        /// <returns></returns>
        public DataTable GetPNTableLinkPR(string terms, List<SqlParameter> parms)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select b.first_assessor,b.first_assessorname,b.orderid,a.Departmentname as Department, a.ReturnID,PreFee,FactFee,DeferFee,DeferDay,ReturnStatus,IsInvoice,InvoiceNo,InvoiceDate,BranchID,BranchCode,ReturnCode,BranchName,Lastupdatetime,PurchasePayID,PRID,a.PRNo,a.PaymentUserID,a.PaymentCode,a.PaymentEmployeeName,a.PaymentUserName,RequestorID,a.ProjectID,RequestUserCode,RequestUserName,RequestEmployeeName,RequestDate,WorkItemID,WorkItemName,a.ProcessID,a.InstanceID,Attachment,BankID,ProjectCode,BankName,DBCode,DBManager,BankAccount,PhoneNo,ExchangeNo,RequestPhone,PaymentTypeID,PaymentTypeName,PaymentTypeCode,ReturnPreDate,PreBeginDate,PreEndDate,ReturnFactDate,ReturnContent,BankAccountName,BankAddress,ReturnType,LastUpdateDateTime,MediaOrderIDs,SupplierName,SupplierBankName,SupplierBankAccount,RePaymentSuggestion,NeedPurchaseAudit,a.DepartmentID,a.DepartmentName,a.Remark,a.CommitDate,a.ParentID,a.LastAuditPassTime,a.RecipientIds,a.FaAuditPassTime ");
            strSql.Append(" FROM F_Return as a left join t_generalinfo as b on a.prid=b.id left join f_pnbatchrelation c on a.returnid=c.returnid where 1=1 ");
            strSql.Append(terms + " order by c.batchrelationid asc");
            return DbHelperSQL.Query(strSql.ToString(), parms).Tables[0];
        }

        /// <summary>
        /// 获取PN列表
        /// </summary>
        /// <param name="terms"></param>
        /// <param name="parms"></param>
        /// <returns></returns>
        public DataTable GetPNTableForPurchasePN(string terms, List<SqlParameter> parms)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" select a.*,d.supplier_name from f_return as a ");
            strSql.Append(" inner join t_generalinfo as b on a.prid=b.id ");
            strSql.Append(" inner join (select general_id,max(supplierid) as supplierId from t_orderinfo group by general_id) as c on b.id=c.general_id ");
            strSql.Append(" inner join t_supplier as d on c.supplierId=d.id ");
            strSql.Append(terms);
            strSql.Append(" order by a.RequestDate desc");
            return DbHelperSQL.Query(strSql.ToString(), parms).Tables[0];
        }

        /// <summary>
        /// 根据returnId获得收货ID
        /// </summary>
        /// <param name="returnIds"></param>
        /// <returns></returns>
        public DataTable GetRecipientIds(string returnIds)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select recipientid,recipientno from t_periodrecipient as a inner join t_recipient as b on a.recipientid=b.id where periodid in ( select id from t_paymentperiod where returnid in (" + returnIds + "))");
            return DbHelperSQL.Query(strSql.ToString()).Tables[0];
        }

        public int Payment(ESP.Finance.Utility.PaymentStatus status, ESP.Finance.Entity.ReturnInfo model)
        {
            return Payment(status, model, null);
        }

        public int Payment(ESP.Finance.Utility.PaymentStatus status, ESP.Finance.Entity.ReturnInfo model, SqlTransaction trans)
        {
            int result = 0;
            SqlParameter[] parameters = {
					new SqlParameter("@ReturnID", SqlDbType.Int,4),
					new SqlParameter("@DeferFee", SqlDbType.Decimal,9),
					new SqlParameter("@DeferDay", SqlDbType.Int,4),
					new SqlParameter("@ReturnStatus", SqlDbType.Int,4),
					new SqlParameter("@IsInvoice", SqlDbType.Int,4),
					new SqlParameter("@InvoiceNo", SqlDbType.NVarChar,50),
					new SqlParameter("@BranchID", SqlDbType.Int,4),
					new SqlParameter("@BranchCode", SqlDbType.NVarChar,10),
					new SqlParameter("@BranchName", SqlDbType.NVarChar,200),
					//,
					new SqlParameter("@ReturnCode", SqlDbType.NVarChar,50),
					new SqlParameter("@ProjectID", SqlDbType.Int,4),
					new SqlParameter("@ProjectCode", SqlDbType.NVarChar,50),
					new SqlParameter("@ReturnPreDate", SqlDbType.DateTime),
					new SqlParameter("@ReturnFactDate", SqlDbType.DateTime),
					new SqlParameter("@ReturnContent", SqlDbType.NVarChar,200),
					new SqlParameter("@PreFee", SqlDbType.Decimal,9),
					new SqlParameter("@FactFee", SqlDbType.Decimal,9),
                    new SqlParameter("@Lastupdatetime", SqlDbType.Timestamp,8),
                    new SqlParameter("@InvoiceDate",SqlDbType.DateTime,8),

                    new SqlParameter("@PaymentUserID", SqlDbType.Int,4),
					new SqlParameter("@PaymentCode", SqlDbType.NVarChar,50),
					new SqlParameter("@PaymentEmployeeName", SqlDbType.NVarChar,50),
					new SqlParameter("@PaymentUserName", SqlDbType.NVarChar,50),
					new SqlParameter("@PurchasePayID", SqlDbType.Int,4),
					new SqlParameter("@PRID", SqlDbType.Int,4),
                    new SqlParameter("@PRNo",SqlDbType.NVarChar,50),

                    new SqlParameter("@RequestorID", SqlDbType.Int,4),
                    new SqlParameter("@RequestUserCode", SqlDbType.NVarChar,50),
					new SqlParameter("@RequestUserName", SqlDbType.NVarChar,50),
					new SqlParameter("@RequestEmployeeName", SqlDbType.NVarChar,50),
					new SqlParameter("@RequestDate", SqlDbType.DateTime),
                    new SqlParameter("@PaymentTypeID", SqlDbType.Int,4),
                    new SqlParameter("@PreBeginDate", SqlDbType.DateTime),
					new SqlParameter("@PreEndDate", SqlDbType.DateTime),
                    new SqlParameter("@Status",SqlDbType.Int,4),
                    new SqlParameter("@RowCount",SqlDbType.Int,4)
                                        };
            parameters[0].Value = model.ReturnID;
            parameters[1].Value = model.DeferFee;
            parameters[2].Value = model.DeferDay;
            parameters[3].Value = model.ReturnStatus;
            parameters[4].Value = model.IsInvoice;
            parameters[5].Value = model.InvoiceNo;
            parameters[6].Value = model.BranchID;
            parameters[7].Value = model.BranchCode;
            parameters[8].Value = model.BranchName;
            //
            parameters[9].Value = model.ReturnCode;
            parameters[10].Value = model.ProjectID;
            parameters[11].Value = model.ProjectCode;
            parameters[12].Value = model.ReturnPreDate;
            parameters[13].Value = model.ReturnFactDate;
            parameters[14].Value = model.ReturnContent;
            parameters[15].Value = model.PreFee;
            parameters[16].Value = model.FactFee;
            parameters[17].Value = model.Lastupdatetime;
            parameters[18].Value = model.InvoiceDate;

            parameters[19].Value = model.PaymentUserID;
            parameters[20].Value = model.PaymentCode;
            parameters[21].Value = model.PaymentEmployeeName;
            parameters[22].Value = model.PaymentUserName;
            parameters[23].Value = model.PurchasePayID;
            parameters[24].Value = model.PRID;
            parameters[25].Value = model.PRNo;

            parameters[26].Value = model.RequestorID;
            parameters[27].Value = model.RequestUserCode;
            parameters[28].Value = model.RequestUserName;
            parameters[29].Value = model.RequestEmployeeName;
            parameters[30].Value = model.RequestDate;
            parameters[31].Value = model.PaymentTypeID;
            parameters[32].Value = model.PreBeginDate;
            parameters[33].Value = model.PreEndDate;
            parameters[34].Value = (int)status;
            parameters[35].Direction = ParameterDirection.Output;
            DbHelperSQL.RunProcedure("P_FinallyPaymentByFinance", parameters, trans, out result);
            return Convert.ToInt32(parameters[35].Value);
        }

        /// <summary>
        /// 抵消押金
        /// </summary>
        /// <param name="returnList"></param>
        /// <param name="list"></param>
        /// <returns></returns>
        public bool updataSatatusAndAddKillForegift(ESP.Finance.Entity.ReturnInfo returnModel, ESP.Finance.Entity.ForeGiftLinkInfo foregift, int status)
        {
            bool returnValue = false;
            bool foregiftExit = new ESP.Finance.DataAccess.ForeGiftLinkDataProvider().Exists(returnModel.ReturnID);
            using (SqlConnection conn = new SqlConnection(DbHelperSQL.connectionString))
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();
                try
                {

                    //更新押金和抵消押金PN的状态为FinanceComplete
                    returnModel.ReturnStatus = status;
                    Update(returnModel, trans);
                    if (foregiftExit == false)
                        new ESP.Finance.DataAccess.ForeGiftLinkDataProvider().Add(foregift, trans);//添加押金和抵消押金的关联
                    trans.Commit();
                    returnValue = true;
                }
                catch
                {
                    trans.Rollback();
                    returnValue = false;
                }
            }
            return returnValue;
        }

        public decimal GetTotalPNFee(ESP.Finance.Entity.ReturnInfo returnModel)
        {
            StringBuilder strSql = new StringBuilder();
            decimal totalPN = 0;
            strSql.Append(@"select sum(prefee)  from F_Return ");
            strSql.Append(" where projectcode=@projectcode and returnid !=@returnid and departmentid=@departmentid and returntype not in(30,20,31,32,33,34,35,36,37,40,-1)");
            SqlParameter[] parameters = {
                    new SqlParameter("@projectcode", SqlDbType.NVarChar,50),
                    new SqlParameter("@returnid", SqlDbType.Int,4),
                    new SqlParameter("@departmentid", SqlDbType.Int,4)
                                        };
            parameters[0].Value = returnModel.ProjectCode;
            parameters[1].Value = returnModel.ReturnID;
            parameters[2].Value = returnModel.DepartmentID.Value;
            using (SqlConnection conn = new SqlConnection(DbHelperSQL.connectionString))
            {
                conn.Open();
                object obj = DbHelperSQL.ExecuteScalar(conn, CommandType.Text, strSql.ToString(), parameters);
                if (obj == null || obj == DBNull.Value)
                    totalPN = 0;
                else
                    totalPN = Convert.ToDecimal(obj);
            }
            return totalPN;
        }

        /// <summary>
        /// 与userid相关的return信息
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public DataTable GetReturnListJoinHist(int userId)
        {
            string strSql = @"select * from f_return as a
                                left join f_returnaudithist as b on a.returnid=b.returnid and b.auditestatus=0
                                where a.returnstatus not in (140) ";
            strSql += string.Format(" and (a.requestorId={0} or b.auditoruserid={0})", userId);
            return DbHelperSQL.Query(strSql).Tables[0];
        }

        /// <summary>
        /// 变更申请人
        /// </summary>
        /// <param name="returnIds"></param>
        /// <param name="oldUserId"></param>
        /// <param name="newUserId"></param>
        /// <returns></returns>
        public int changeRequestor(string returnIds, int oldUserId, int newUserId, ESP.Compatible.Employee currentUser)
        {
            ESP.Compatible.Employee emp = new ESP.Compatible.Employee(newUserId);
            ESP.Compatible.Employee emp1 = new ESP.Compatible.Employee(oldUserId);
            string strSql = " update f_return set RequestorID={0}, RequestUserCode='{1}', RequestUserName='{2}', RequestEmployeeName='{3}'";
            strSql = string.Format(strSql, emp.SysID, emp.ID, emp.ITCode, emp.Name);
            strSql += " where returnId=@returnId and requestorId=" + oldUserId;
            using (SqlConnection conn = new SqlConnection(DbHelperSQL.connectionString))
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();
                try
                {
                    int count = 0;
                    string[] Ids = returnIds.Split(',');
                    foreach (string id in Ids)
                    {
                        SqlParameter parm = new SqlParameter("@returnId", id);
                        if (DbHelperSQL.ExecuteSql(strSql, trans, parm) > 0)
                        {
                            addPermission(id, newUserId, trans);
                            ESP.Finance.Entity.AuditLogInfo log = new AuditLogInfo();
                            log.AuditDate = DateTime.Now;
                            log.AuditorEmployeeName = currentUser.Name;
                            log.AuditorSysID = int.Parse(currentUser.SysID);
                            log.AuditorUserCode = currentUser.ID;
                            log.AuditorUserName = currentUser.ITCode;
                            log.AuditStatus = 1;
                            log.FormID = int.Parse(id);
                            log.FormType = (int)ESP.Finance.Utility.FormType.Return;
                            log.Suggestion = "申请人" + emp1.Name + "变更为" + emp.Name;
                            ESP.Finance.BusinessLogic.AuditLogManager.Add(log, trans);
                            count++;
                        }
                    }
                    trans.Commit();
                    return count;
                }
                catch
                {
                    trans.Rollback();
                    return 0;
                }
            }
        }

        public int changAuditor(string returnIds, int oldUserId, int newUserId, ESP.Compatible.Employee currentUser)
        {
            ESP.Compatible.Employee emp = new ESP.Compatible.Employee(newUserId);
            ESP.Compatible.Employee emp1 = new ESP.Compatible.Employee(oldUserId);
            string strSql = " update f_returnaudithist set  AuditorUserID={0}, AuditorUserName='{1}', AuditorUserCode='{2}', AuditorEmployeeName='{3}'";
            strSql = string.Format(strSql, emp.SysID, emp.ITCode, emp.ID, emp.Name);
            strSql += " where returnId=@returnId and AuditorUserID=" + oldUserId + " and auditestatus=0";

            string strSql1 = " update f_return set PaymentUserID={0}, PaymentUserName='{1}', PaymentCode='{2}', PaymentEmployeeName='{3}'";
            strSql1 = string.Format(strSql1, emp.SysID, emp.ITCode, emp.ID, emp.Name);
            strSql1 += " where returnId=@returnId and PaymentUserID=" + oldUserId;
            using (SqlConnection conn = new SqlConnection(DbHelperSQL.connectionString))
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();
                try
                {
                    int count = 0;
                    string[] Ids = returnIds.Split(',');
                    foreach (string id in Ids)
                    {
                        SqlParameter parm = new SqlParameter("@returnId", id);
                        if (DbHelperSQL.ExecuteSql(strSql, parm) > 0)
                        {
                            addPermission(id, newUserId, trans);
                            ESP.Finance.Entity.AuditLogInfo log = new AuditLogInfo();
                            log.AuditDate = DateTime.Now;
                            log.AuditorEmployeeName = currentUser.Name;
                            log.AuditorSysID = int.Parse(currentUser.SysID);
                            log.AuditorUserCode = currentUser.ID;
                            log.AuditorUserName = currentUser.ITCode;
                            log.AuditStatus = 1;
                            log.FormID = int.Parse(id);
                            log.FormType = (int)ESP.Finance.Utility.FormType.Return;
                            log.Suggestion = "审核人" + emp1.Name + "变更为" + emp.Name;
                            ESP.Finance.BusinessLogic.AuditLogManager.Add(log, trans);
                            DbHelperSQL.ExecuteSql(strSql1, parm);

                            ESP.Finance.Entity.ReturnInfo returnModel = GetModel(int.Parse(id), trans);
                            if (returnModel.ProcessID != null && returnModel.InstanceID != null)
                            {
                                WorkFlowDAO.ProcessInstanceDao instanceDao = new WorkFlowDAO.ProcessInstanceDao();
                                instanceDao.UpdateRoleWhenLastDay("PN", returnModel.ProcessID.Value, returnModel.InstanceID.Value, oldUserId, newUserId, emp1.Name, emp.Name, trans);
                            }
                            count++;
                        }
                    }
                    trans.Commit();
                    return count;
                }
                catch
                {
                    trans.Rollback();
                    return 0;
                }
            }
        }

        private void addPermission(string returnId, int newUserId, SqlTransaction trans)
        {
            ESP.Purchase.Entity.DataInfo dataModel = new ESP.Purchase.DataAccess.DataPermissionProvider().GetDataInfoModel((int)ESP.Purchase.Common.State.DataType.Return, int.Parse(returnId), trans);
            if (dataModel == null)
            {
                dataModel = new ESP.Purchase.Entity.DataInfo();
                dataModel.DataType = (int)ESP.Purchase.Common.State.DataType.Return;
                dataModel.DataId = int.Parse(returnId);
            }
            ESP.Purchase.Entity.DataPermissionInfo perission = new ESP.Purchase.Entity.DataPermissionInfo();
            perission.UserId = newUserId;
            perission.IsEditor = true;
            perission.IsViewer = true;
            if (dataModel.Id > 0)
                new ESP.Purchase.DataAccess.DataPermissionProvider().AppendDataPermission(dataModel, new List<ESP.Purchase.Entity.DataPermissionInfo>() { perission }, trans);
            else
                new ESP.Purchase.DataAccess.DataPermissionProvider().AddDataPermission(dataModel, new List<ESP.Purchase.Entity.DataPermissionInfo>() { perission }, trans);
        }

        /// <summary>
        /// 得到批次中所有报销单的单据类型
        /// </summary>
        /// <param name="batchid"></param>
        /// <returns></returns>
        public int GetDistinctReturnTypeByBatchID(int batchid)
        {
            string sql = string.Format("SELECT distinct ReturnType FROM F_Return where returnid in (SELECT ReturnID FROM F_PNBatchRelation where batchid = {0} ) ", batchid);
            DataTable dt = DbHelperSQL.Query(sql).Tables[0];
            if (dt.Rows.Count == 0)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(dt.Rows[0]["ReturnType"]);
            }
        }

        public string GetDistinctRecipientByBatchID(int batchId)
        {
            string sql = string.Format("SELECT distinct recipient FROM F_ExpenseaccountDetail where returnid in (SELECT ReturnID FROM F_PNBatchRelation where batchid = {0} ) ", batchId);
            DataTable dt = DbHelperSQL.Query(sql).Tables[0];
            if (dt.Rows.Count == 0)
            {
                return string.Empty;
            }
            else
            {
                return dt.Rows[0]["recipient"].ToString();
            }
        }

        /// <summary>
        /// 得到批次中所有报销单的公司代码
        /// </summary>
        /// <param name="batchid"></param>
        /// <returns></returns>
        public string GetDistinctBranchCodeByBatchID(int batchid)
        {
            string sql = string.Format("SELECT distinct BranchCode FROM F_Return where returnid in (SELECT ReturnID FROM F_PNBatchRelation where batchid = {0} ) ", batchid);
            DataTable dt = DbHelperSQL.Query(sql).Tables[0];
            if (dt.Rows.Count == 0)
            {
                return "";
            }
            else
            {
                return dt.Rows[0]["BranchCode"].ToString();
            }
        }

        public IList<ReturnInfo> GetWaitAuditList(string userIds, string strTerms, List<SqlParameter> parms)
        {
            System.Data.DataTable dt = DbHelperSQL.RunProcedure("P_GetAllTaskItems", new IDataParameter[1], "Ta").Tables[0];
            DataRow[] rows = dt.Select(" approverid in (" + userIds.TrimEnd(',') + ") and operationType='待审批付款申请'");
            string returnIds = "";
            for (int i = 0; i < rows.Length; i++)
            {
                returnIds += rows[i]["FromID"].ToString() + ",";
            }
            strTerms = " 1=1" + strTerms;
            strTerms += returnIds == "" ? " and returnId=0" : " and returnId in (" + returnIds.TrimEnd(',') + ")";
            return GetList(strTerms, parms);
        }

        public IList<ReturnInfo> GetWaitAuditList(int[] userIds)
        {
            if (userIds == null || userIds.Length == 0)
                return new List<ReturnInfo>();

            StringBuilder sql = new StringBuilder(@"
select distinct a.*
from F_Return as a inner join F_ReturnAuditHist as b 
on a.ReturnID = b.ReturnID and b.AuditorUserID=a.PaymentUserid
where  a.ReturnStatus not in(0,90,140,139) and a.ReturnID not in(select f_pnbatchrelation.returnid from f_pnbatchrelation)
 and b.AuditeStatus=0
 and b.AuditorUserID in (").Append(userIds[0]);

            for (var i = 1; i < userIds.Length; i++)
            {
                sql.Append(",").Append(userIds[i]);
            }

            sql.Append(")");


            using (var reader = ESP.Finance.DataAccess.DbHelperSQL.ExecuteReader(sql.ToString()))
            {
                return MyPhotoUtility.CBO.FillCollection<ReturnInfo>(reader);
            }
        }

        public string GetDistinctRecipientByReturnID(int returnId)
        {
            string sql = string.Format("select distinct recipient from f_return a join F_ExpenseAccountDetail b on a.returnid=b.returnid where a.returnId ={0}", returnId);
            DataTable dt = DbHelperSQL.Query(sql).Tables[0];
            if (dt.Rows.Count == 0)
            {
                return "";
            }
            else
            {
                return dt.Rows[0]["recipient"].ToString();
            }
        }

        public int BatchRepayToRequest(int batchId, string reason, ESP.Compatible.Employee CurrentUser)
        {
            int ret = 0;
            using (SqlConnection conn = new SqlConnection(DbHelperSQL.connectionString))
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();
                try
                {
                    //更新批次
                    PNBatchInfo batchModel = ESP.Finance.BusinessLogic.PNBatchManager.GetModel(batchId);
                    batchModel.Status = (int)ESP.Finance.Utility.PaymentStatus.FinanceReject;
                    ESP.Finance.BusinessLogic.PNBatchManager.Update(batchModel);

                    ESP.Compatible.Employee emp = new ESP.Compatible.Employee(batchModel.CreatorID.Value);

                    //增加批次日志
                    ESP.Finance.Entity.AuditLogInfo log = new AuditLogInfo();
                    log.AuditDate = DateTime.Now;
                    log.AuditorEmployeeName = CurrentUser.Name;
                    log.AuditorSysID = Convert.ToInt32(CurrentUser.SysID);
                    log.AuditorUserCode = CurrentUser.ID;
                    log.AuditorUserName = CurrentUser.ITCode;
                    log.AuditStatus = (int)ESP.Finance.Utility.AuditHistoryStatus.TerminateAuditing;
                    log.FormID = batchModel.BatchID;
                    log.FormType = (int)ESP.Finance.Utility.FormType.PNBatch;
                    log.Suggestion = "付款重汇" + reason;
                    ESP.Finance.BusinessLogic.AuditLogManager.Add(log);

                    //更新批次内的付款申请
                    IList<ESP.Finance.Entity.ReturnInfo> ReturnList = ESP.Finance.BusinessLogic.PNBatchManager.GetReturnList(batchId);

                    foreach (ESP.Finance.Entity.ReturnInfo returnModel in ReturnList)
                    {
                        //更新批次内的付款申请
                        returnModel.ReturnStatus = (int)ESP.Finance.Utility.PaymentStatus.FinanceReject;
                        returnModel.RePaymentSuggestion = reason;
                        ESP.Finance.Utility.UpdateResult result = ESP.Finance.BusinessLogic.ReturnManager.Update(returnModel);
                        if (result == ESP.Finance.Utility.UpdateResult.Succeed)
                        {
                            //增加付款申请日志
                            ESP.Finance.Entity.AuditLogInfo logModel = new AuditLogInfo();
                            logModel.AuditDate = DateTime.Now;
                            logModel.AuditorEmployeeName = CurrentUser.Name;
                            logModel.AuditorSysID = Convert.ToInt32(CurrentUser.SysID);
                            logModel.AuditorUserCode = CurrentUser.ID;
                            logModel.AuditorUserName = CurrentUser.ITCode;
                            logModel.AuditStatus = (int)ESP.Finance.Utility.AuditHistoryStatus.TerminateAuditing;
                            logModel.FormID = returnModel.ReturnID;
                            logModel.FormType = (int)ESP.Finance.Utility.FormType.Return;
                            logModel.Suggestion = "付款重汇" + reason;
                            ESP.Finance.BusinessLogic.AuditLogManager.Add(logModel);
                        }
                    }
                    ESP.Finance.Utility.SendMailHelper.SendMailBatchRepay(batchModel, CurrentUser.Name, emp.Name, emp.EMail);
                    ret = 1;
                }
                catch
                {
                    trans.Rollback();
                    return 0;
                }
            }
            return ret;
        }

        public int BatchRepayToFinance(int batchId, string reason, ESP.Compatible.Employee CurrentUser)
        {
            int ret = 0;
            using (SqlConnection conn = new SqlConnection(DbHelperSQL.connectionString))
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();
                try
                {
                    //更新批次
                    PNBatchInfo batchModel = ESP.Finance.BusinessLogic.PNBatchManager.GetModel(batchId);
                    ESP.Compatible.Employee financeEmp = new ESP.Compatible.Employee(ESP.Finance.BusinessLogic.BranchManager.GetModelByCode(batchModel.BranchCode).FirstFinanceID);
                    ESP.Compatible.Employee emp = new ESP.Compatible.Employee(batchModel.CreatorID.Value);

                    batchModel.PaymentCode = financeEmp.ID;
                    batchModel.PaymentUserID = Convert.ToInt32(financeEmp.SysID);
                    batchModel.PaymentUserName = financeEmp.ITCode;
                    batchModel.PaymentEmployeeName = financeEmp.Name;
                    batchModel.Status = (int)ESP.Finance.Utility.PaymentStatus.MajorAudit;
                    ESP.Finance.BusinessLogic.PNBatchManager.Update(batchModel);

                    //增加批次日志
                    ESP.Finance.Entity.AuditLogInfo log = new AuditLogInfo();
                    log.AuditDate = DateTime.Now;
                    log.AuditorEmployeeName = CurrentUser.Name;
                    log.AuditorSysID = Convert.ToInt32(CurrentUser.SysID);
                    log.AuditorUserCode = CurrentUser.ID;
                    log.AuditorUserName = CurrentUser.ITCode;
                    log.AuditStatus = (int)ESP.Finance.Utility.AuditHistoryStatus.TerminateAuditing;
                    log.FormID = batchModel.BatchID;
                    log.FormType = (int)ESP.Finance.Utility.FormType.PNBatch;
                    log.Suggestion = "付款重汇" + reason;
                    ESP.Finance.BusinessLogic.AuditLogManager.Add(log);

                    //更新批次内的付款申请
                    IList<ESP.Finance.Entity.ReturnInfo> ReturnList = ESP.Finance.BusinessLogic.PNBatchManager.GetReturnList(batchId);

                    foreach (ESP.Finance.Entity.ReturnInfo returnModel in ReturnList)
                    {
                        //更新批次内的付款申请
                        returnModel.PaymentCode = financeEmp.ID;
                        returnModel.PaymentUserID = Convert.ToInt32(financeEmp.SysID);
                        returnModel.PaymentUserName = financeEmp.ITCode;
                        returnModel.PaymentEmployeeName = financeEmp.Name;
                        returnModel.ReturnStatus = (int)ESP.Finance.Utility.PaymentStatus.FinanceReject;
                        returnModel.RePaymentSuggestion = reason;
                        ESP.Finance.Utility.UpdateResult result = ESP.Finance.BusinessLogic.ReturnManager.Update(returnModel);
                        if (result == ESP.Finance.Utility.UpdateResult.Succeed)
                        {
                            //增加付款申请日志
                            ESP.Finance.Entity.AuditLogInfo logModel = new AuditLogInfo();
                            logModel.AuditDate = DateTime.Now;
                            logModel.AuditorEmployeeName = CurrentUser.Name;
                            logModel.AuditorSysID = Convert.ToInt32(CurrentUser.SysID);
                            logModel.AuditorUserCode = CurrentUser.ID;
                            logModel.AuditorUserName = CurrentUser.ITCode;
                            logModel.AuditStatus = (int)ESP.Finance.Utility.AuditHistoryStatus.TerminateAuditing;
                            logModel.FormID = returnModel.ReturnID;
                            logModel.FormType = (int)ESP.Finance.Utility.FormType.Return;
                            logModel.Suggestion = "付款重汇" + reason;
                            ESP.Finance.BusinessLogic.AuditLogManager.Add(logModel);
                        }
                    }
                    ESP.Finance.Utility.SendMailHelper.SendMailBatchRepay(batchModel, CurrentUser.Name, emp.Name, emp.EMail);
                    ret = 1;
                }
                catch
                {
                    trans.Rollback();
                    return 0;
                }
            }
            return ret;
        }

        public int BatchRepayCommit(int batchId, string supplier, string bank, string account, string remark, ESP.Compatible.Employee CurrentUser)
        {
            int ret = 0;
            ESP.Finance.Entity.PNBatchInfo batchModel = ESP.Finance.BusinessLogic.PNBatchManager.GetModel(batchId);
            IList<ESP.Finance.Entity.ReturnInfo> returnList = ESP.Finance.BusinessLogic.PNBatchManager.GetReturnList(batchId);
            ESP.Compatible.Employee financeEmp = new ESP.Compatible.Employee(ESP.Finance.BusinessLogic.BranchManager.GetModelByCode(batchModel.BranchCode).FirstFinanceID);

            using (SqlConnection conn = new SqlConnection(DbHelperSQL.connectionString))
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();
                try
                {

                    ESP.Finance.Entity.BankCancelInfo bankInfo = new ESP.Finance.Entity.BankCancelInfo();
                    bankInfo.ReturnID = batchModel.BatchID;
                    bankInfo.ReturnCode = batchModel.PurchaseBatchCode;
                    bankInfo.RequestorID = Convert.ToInt32(CurrentUser.SysID);
                    bankInfo.RequestorName = CurrentUser.ITCode;
                    bankInfo.RequestorCode = CurrentUser.ID;
                    bankInfo.RequestorEmpName = CurrentUser.Name;
                    bankInfo.OldBankAccountName = supplier;
                    bankInfo.OldBankName = batchModel.SupplierBankName;
                    bankInfo.OldBankAccount = batchModel.SupplierBankAccount;

                    bankInfo.NewBankAccountName = supplier;
                    bankInfo.NewBankName = bank;
                    bankInfo.NewBankAccount = account;
                    bankInfo.CommitDate = DateTime.Now;
                    bankInfo.LastUpdateTime = DateTime.Now;
                    bankInfo.RePaymentSuggestion = remark;
                    bankInfo.OrderType = 2;
                    ESP.Finance.BusinessLogic.BankCancelManager.Add(bankInfo, trans);

                    ESP.Finance.Entity.AuditLogInfo log = new ESP.Finance.Entity.AuditLogInfo();
                    log.AuditDate = DateTime.Now;
                    log.AuditorEmployeeName = CurrentUser.Name;
                    log.AuditorSysID = Convert.ToInt32(CurrentUser.SysID);
                    log.AuditorUserCode = CurrentUser.ID;
                    log.AuditorUserName = CurrentUser.ITCode;
                    log.AuditStatus = (int)ESP.Finance.Utility.AuditHistoryStatus.TerminateAuditing;
                    log.FormID = batchModel.BatchID;
                    log.FormType = (int)ESP.Finance.Utility.FormType.PNBatch;
                    log.Suggestion = "重汇业务提交";
                    ESP.Finance.BusinessLogic.AuditLogManager.Add(log, trans);

                    batchModel.Status = (int)ESP.Finance.Utility.PaymentStatus.MajorAudit;
                    batchModel.PaymentUserID = int.Parse(financeEmp.SysID);
                    batchModel.PaymentEmployeeName = financeEmp.Name;
                    batchModel.PaymentCode = financeEmp.ID;
                    batchModel.PaymentUserName = financeEmp.ITCode;
                    batchModel.SupplierBankAccount = account;
                    batchModel.SupplierBankName = bank;
                    batchModel.SupplierName = supplier;
                    ESP.Finance.BusinessLogic.PNBatchManager.Update(batchModel);

                    foreach (ESP.Finance.Entity.ReturnInfo returnModel in returnList)
                    {
                        ESP.Finance.Entity.BankCancelInfo bankdetail = new ESP.Finance.Entity.BankCancelInfo();
                        bankdetail.ReturnID = batchModel.BatchID;
                        bankdetail.ReturnCode = batchModel.PurchaseBatchCode;
                        bankdetail.RequestorID = Convert.ToInt32(CurrentUser.SysID);
                        bankdetail.RequestorName = CurrentUser.ITCode;
                        bankdetail.RequestorCode = CurrentUser.ID;
                        bankdetail.RequestorEmpName = CurrentUser.Name;
                        bankdetail.OldBankAccountName = supplier;
                        bankdetail.OldBankName = batchModel.SupplierBankName;
                        bankdetail.OldBankAccount = batchModel.SupplierBankAccount;

                        bankdetail.NewBankAccountName = supplier;
                        bankdetail.NewBankName = bank;
                        bankdetail.NewBankAccount = account;
                        bankdetail.CommitDate = DateTime.Now;
                        bankdetail.LastUpdateTime = DateTime.Now;
                        bankdetail.RePaymentSuggestion = remark;
                        bankdetail.OrderType = 1;

                        returnModel.SupplierName = supplier;
                        returnModel.SupplierBankName = bank;
                        returnModel.SupplierBankAccount = account;
                        returnModel.ReturnContent = remark;

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
                        ESP.Finance.BusinessLogic.BankCancelManager.CommitReturnInfo(returnModel, FinanceModel, bankdetail, trans);

                        ESP.Finance.Entity.AuditLogInfo returnlog = new ESP.Finance.Entity.AuditLogInfo();
                        returnlog.AuditDate = DateTime.Now;
                        returnlog.AuditorEmployeeName = CurrentUser.Name;
                        returnlog.AuditorSysID = Convert.ToInt32(CurrentUser.SysID);
                        returnlog.AuditorUserCode = CurrentUser.ID;
                        returnlog.AuditorUserName = CurrentUser.ITCode;
                        returnlog.AuditStatus = (int)ESP.Finance.Utility.AuditHistoryStatus.TerminateAuditing;
                        returnlog.FormID = returnModel.ReturnID;
                        returnlog.FormType = (int)ESP.Finance.Utility.FormType.Return;
                        returnlog.Suggestion = "重汇业务提交";
                        ESP.Finance.BusinessLogic.AuditLogManager.Add(returnlog, trans);
                        try
                        {
                            ESP.Finance.Utility.SendMailHelper.SendMailReturnCommit(returnModel, returnModel.RequestEmployeeName, financeEmp.Name, "", financeEmp.EMail);
                        }
                        catch
                        {
                            continue;
                        }

                    }
                    ret = 1;
                    trans.Commit();
                }
                catch
                {
                    trans.Rollback();
                }
            }
            return ret;
        }

        public decimal GetTotalRefund(string projectCode, int departmentId)
        {
            StringBuilder strSql = new StringBuilder();
            decimal totalPN = 0;
            strSql.Append(@"select sum(prefee)  from F_Return ");
            strSql.Append(" where projectcode=@projectcode and departmentid=@departmentid and returntype=40");
            SqlParameter[] parameters = {
                    new SqlParameter("@projectcode", SqlDbType.NVarChar,50),
                    new SqlParameter("@departmentid", SqlDbType.Int,4)
                                        };
            parameters[0].Value = projectCode;
            parameters[1].Value = departmentId;

            using (SqlConnection conn = new SqlConnection(DbHelperSQL.connectionString))
            {
                conn.Open();
                object obj = DbHelperSQL.ExecuteScalar(conn, CommandType.Text, strSql.ToString(), parameters);
                if (obj == null || obj == DBNull.Value)
                    totalPN = 0;
                else
                    totalPN = Convert.ToDecimal(obj);
            }
            return totalPN;
        }

        public DataTable GetFactoringList(string term)
        {
            DataTable dt = new DataTable();
            string sql = @"select a.id,a.prNo ,a.requestorname,a.project_id,a.project_code,a.project_descripttion,a.supplier_name,a.totalprice,
                           a.DepartmentId,c.level1+'-'+c.level2+'-'+c.level3 as department,b.returnid,b.ReturnCode,b.PreFee,b.ReturnStatus,a.factoringDate,b.PaymentUserID,b.PaymentEmployeeName 
                           from T_GeneralInfo a join F_Return b on a.id =b.PRID
                           join V_Department c on a.DepartmentId =c.level3Id
                           where 1=1 ";
            if (!string.IsNullOrEmpty(term))
            {
                sql += term;
            }
            return DbHelperSQL.Query(sql).Tables[0];
        }

        #endregion  成员方法

        public DataTable GetRecipientReport(string where)
        {
            DataTable dt = new DataTable();
            string sql = @"select * from (
                            select a.id as prid,a.prno,substring(a.project_code,3,4)as customer,substring(a.project_code,8,1) as projecttype,
                            (substring(a.project_code,1,1)+substring(a.project_code,10,11)) as projectcode,a.account_name as suppliername,
                            b.recipientamount as fee,a.status as prstatus,a.prtype,b.isconfirm as [pnstatus],
                            '无' as invoice,b.recipientdate as commitdate,'GR' as flag,b.id as pnid,a.requestorname as requestor,
                            c.code as userCode,(e.level1+'-'+e.level2+'-'+e.level3) as dept
                            from t_generalinfo a join t_recipient b on a.id=b.gid
                            left join t_paymentperiod p on a.id=p.gid
                            left join sep_employees c on a.requestor=c.userid
                            left join v_department e on a.departmentid=e.level3id
                            where a.status not in(0,-1,2,17) and a.prtype not in(1,4) and (p.returnid is null or p.returnid=0)
                            union
                            select a.id as prid,a.prno,substring(a.project_code,3,4)as customer,substring(a.project_code,8,1) as projecttype,
                            (substring(a.project_code,1,1)+substring(a.project_code,10,11)) as projectcode,a.account_name as suppliername,
                            b.prefee as fee,a.status as prstatus,a.prtype,b.returnstatus as [pnstatus],
                            case isinvoice when 1 then '有' else '无' end as invoice,
                            b.requestdate as commitdate,'PN' as flag,b.returnid as pnid,a.requestorname as requestor,
                            c.code as userCode,(e.level1+'-'+e.level2+'-'+e.level3) as dept
                            from t_generalinfo a 
                            join t_paymentperiod p on a.id=p.gid
                            join f_return b on a.id=b.prid
                            left join sep_employees c on a.requestor=c.userid
                            left join v_department e on a.departmentid=e.level3id
                            where b.returnstatus not in(139,140,135,136,137) and a.prtype not in(1,4) and p.periodtype<>1
                            )as a where 1=1 ";
            if (!string.IsNullOrEmpty(where))
            {//test
                sql += where;
            }
            return DbHelperSQL.Query(sql).Tables[0];
        }

        public IList<ReturnInfo> GetPaidPNReport(int userid ,DateTime dtbegin ,DateTime dtend)
        {
            string strSql = @"select * from (select * 
                            ,(select top 1 AuditDate from F_AuditLog where F_AuditLog.formid = F_Return.returnid and auditstatus =1  and FormType =4 order by AuditDate desc) as auditDate
                            from F_Return  where ReturnStatus in(140,139,136,137) and ReturnType =0 ) a where AuditDate between @dtbegin and @dtend";

            SqlParameter[] parameters = {
	                    new SqlParameter("@dtbegin", SqlDbType.DateTime,8),
                    new SqlParameter("@dtend", SqlDbType.DateTime,8)};
            parameters[0].Value = dtbegin;
            parameters[1].Value = dtend;

            return ESP.Finance.Utility.CBO.FillCollection<ReturnInfo>(ESP.Finance.DataAccess.DbHelperSQL.Query(strSql, parameters));
        }


        #region IReturnDataProvider 成员


        public int GetCount(string term, List<SqlParameter> param)
        {
            string strSql = "select count(*) FROM F_Return";
            if (!string.IsNullOrEmpty(term))
            {
                strSql += " where " + term;
            }

            using (var reader = DbHelperSQL.ExecuteReader(strSql, param))
            {
                if (reader.Read())
                    return reader.GetInt32(0);

                return 0;
            }
        }

        #endregion
    }
}

