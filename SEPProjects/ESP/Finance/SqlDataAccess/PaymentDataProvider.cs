using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using ESP.Finance.Entity;
using System.Collections.Generic;
using ESP.Finance.Utility;
//using Maticsoft.DBUtility;//请先添加引用
namespace ESP.Finance.DataAccess
{
    /// <summary>
    /// 数据访问类PaymentDAL。
    /// </summary>
    internal class PaymentDataProvider : ESP.Finance.IDataAccess.IPaymentDataProvider
    {

        #region  成员方法
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int PaymentID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from F_Payment");
            strSql.Append(" where PaymentID=@PaymentID ");
            SqlParameter[] parameters = {
					new SqlParameter("@PaymentID", SqlDbType.Int,4)};
            parameters[0].Value = PaymentID;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }

        public bool PaymentCodeExist(int paymentId, string paymentCode)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from F_Payment");
            strSql.Append(" where PaymentCode=@PaymentCode and paymentId!=@paymentId");
            SqlParameter[] parameters = {
					new SqlParameter("@PaymentCode", SqlDbType.NVarChar,50),
                    new SqlParameter("@PaymentID", SqlDbType.Int,4)                                        
                                        };
            parameters[0].Value = paymentCode;
            parameters[1].Value = paymentId;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }

        public string CreateCode(string branchDesCode)
        {

            string date = DateTime.Now.ToString("yyMM");
            string strSql = "select max(PaymentCode) as maxId from F_Payment as a where a.PaymentCode like '" + branchDesCode + date + "%'";

            object maxid = DbHelperSQL.GetSingle(strSql);
            int no = maxid == null ? 0 : Convert.ToInt32(maxid.ToString().Substring(7,3));
            no++;
            return branchDesCode + date + no.ToString("000");
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(Entity.PaymentInfo model)
        {
            return Add(model, null);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(Entity.PaymentInfo model, SqlTransaction trans)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into F_Payment(");
            strSql.Append(@"PaymentCode,ProjectID,ProjectCode,PaymentPreDate,PaymentFactDate,
                            PaymentContent,PaymentStatus,PaymentBudget,PaymentFee,InvoiceNo,
                            InvoiceDate,BranchID,BranchCode,BranchName,Remark,
                            BankID,BankName,DBCode,DBManager,BankAccount,
                            BankAccountName,PhoneNo,ExchangeNo,RequestPhone,BankAddress,
                            PaymentTypeID,PaymentTypeName,PaymentTypeCode,
                            PaymentUserID,PaymentUserCode,PaymentEmployeeName,PaymentUserName,
                            CreditCode,USDDiffer,PaymentExtensionStatus,BudgetForiegn,BudgetForiegnUnit,
                            PaymentFactForiegn,PaymentFactForiegnUnit,InvoiceTitle,InvoiceAmount,InvoiceReceiver,
                            InvoiceSignIn,ConfirmRemark,BillDate,EstReturnDate,PaymentBudgetConfirm,BadDebt,ConfirmYear,
                            ConfirmMonth,InnerRelation,ReBateTitle,RebateNo,RebateDate,RebateAmount,RebateReceiver,RebateSignIn,RebateYear,RebateMonth,InvoiceType,RebateType)");

            strSql.Append(" values (");
            strSql.Append(@"@PaymentCode,@ProjectID,@ProjectCode,@PaymentPreDate,@PaymentFactDate,
                            @PaymentContent,@PaymentStatus,@PaymentBudget,@PaymentFee,@InvoiceNo,
                            @InvoiceDate,@BranchID,@BranchCode,@BranchName,@Remark,
                            @BankID,@BankName,@DBCode,@DBManager,@BankAccount,
                            @BankAccountName,@PhoneNo,@ExchangeNo,@RequestPhone,@BankAddress,
                            @PaymentTypeID,@PaymentTypeName,@PaymentTypeCode,
                            @PaymentUserID,@PaymentUserCode,@PaymentEmployeeName,@PaymentUserName,
                            @CreditCode,@USDDiffer,@PaymentExtensionStatus,@BudgetForiegn,@BudgetForiegnUnit,
                            @PaymentFactForiegn,@PaymentFactForiegnUnit,@InvoiceTitle,@InvoiceAmount,@InvoiceReceiver,
                            @InvoiceSignIn,@ConfirmRemark,@BillDate,@EstReturnDate,@PaymentBudgetConfirm,@BadDebt,
                            @ConfirmYear,@ConfirmMonth,@InnerRelation,@ReBateTitle,@RebateNo,@RebateDate,@RebateAmount,
                            @RebateReceiver,@RebateSignIn,@RebateYear,@RebateMonth,@InvoiceType,@RebateType)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@PaymentCode", SqlDbType.NVarChar,50),
					new SqlParameter("@ProjectID", SqlDbType.Int,4),
					new SqlParameter("@ProjectCode", SqlDbType.NVarChar,50),
					new SqlParameter("@PaymentPreDate", SqlDbType.DateTime),
					new SqlParameter("@PaymentFactDate", SqlDbType.DateTime),
					new SqlParameter("@PaymentContent", SqlDbType.NVarChar,500),
					new SqlParameter("@PaymentStatus", SqlDbType.Int,4),
					new SqlParameter("@PaymentBudget", SqlDbType.Decimal,20),
					new SqlParameter("@PaymentFee", SqlDbType.Decimal,20),
					new SqlParameter("@InvoiceNo", SqlDbType.NVarChar,100),
					new SqlParameter("@InvoiceDate", SqlDbType.DateTime),
					new SqlParameter("@BranchID", SqlDbType.Int,4),
					new SqlParameter("@BranchCode", SqlDbType.NVarChar,10),
					new SqlParameter("@BranchName", SqlDbType.NVarChar,200),
					new SqlParameter("@Remark", SqlDbType.NVarChar,500),
					new SqlParameter("@BankID", SqlDbType.Int,4),
					new SqlParameter("@BankName", SqlDbType.NVarChar,100),
					new SqlParameter("@DBCode", SqlDbType.NVarChar,10),
					new SqlParameter("@DBManager", SqlDbType.NVarChar,10),
					new SqlParameter("@BankAccount", SqlDbType.NVarChar,50),
					new SqlParameter("@BankAccountName", SqlDbType.NVarChar,50),
					new SqlParameter("@PhoneNo", SqlDbType.NVarChar,50),
					new SqlParameter("@ExchangeNo", SqlDbType.NVarChar,50),
					new SqlParameter("@RequestPhone", SqlDbType.NVarChar,50),
					new SqlParameter("@BankAddress", SqlDbType.NVarChar,100),
					new SqlParameter("@PaymentTypeID", SqlDbType.Int,4),
					new SqlParameter("@PaymentTypeName", SqlDbType.NVarChar,50),
					new SqlParameter("@PaymentTypeCode", SqlDbType.NVarChar,50),
                    new SqlParameter("@PaymentUserID", SqlDbType.Int,4),
                    new SqlParameter("@PaymentUserCode", SqlDbType.NVarChar,50),
                    new SqlParameter("@PaymentEmployeeName", SqlDbType.NVarChar,50),
                    new SqlParameter("@PaymentUserName", SqlDbType.NVarChar,50),
                    new SqlParameter("@CreditCode",SqlDbType.NVarChar,25),
                    new SqlParameter("@USDDiffer",SqlDbType.Decimal,9),
					new SqlParameter("@PaymentExtensionStatus", SqlDbType.Int,4),
                    
                    new SqlParameter("@BudgetForiegn", SqlDbType.Decimal,20),
                    new SqlParameter("@BudgetForiegnUnit", SqlDbType.NVarChar,10),

                    new SqlParameter("@PaymentFactForiegn", SqlDbType.Decimal,20),
                    new SqlParameter("@PaymentFactForiegnUnit", SqlDbType.NVarChar,10),

                    new SqlParameter("@InvoiceTitle", SqlDbType.NVarChar,50),
                    new SqlParameter("@InvoiceAmount", SqlDbType.Decimal,20),
                    new SqlParameter("@InvoiceReceiver", SqlDbType.NVarChar,50),
                    new SqlParameter("@InvoiceSignIn", SqlDbType.NVarChar,50),
                    new SqlParameter("@ConfirmRemark", SqlDbType.NVarChar,50),
                    new SqlParameter("@BillDate", SqlDbType.DateTime),
                    new SqlParameter("@EstReturnDate", SqlDbType.DateTime),
                    new SqlParameter("@PaymentBudgetConfirm", SqlDbType.Decimal,20),
                    new SqlParameter("@BadDebt", SqlDbType.Int,4),
                    new SqlParameter("@ConfirmYear", SqlDbType.Int,4),
                    new SqlParameter("@ConfirmMonth", SqlDbType.Int,4),
                    new SqlParameter("@InnerRelation", SqlDbType.Int,4),
                    new SqlParameter("@ReBateTitle", SqlDbType.NVarChar,100),
                    new SqlParameter("@RebateNo", SqlDbType.NVarChar,100),
                    new SqlParameter("@RebateDate", SqlDbType.DateTime),
                    new SqlParameter("@RebateAmount", SqlDbType.Decimal,20),
                    new SqlParameter("@RebateReceiver", SqlDbType.NVarChar,50),
                    new SqlParameter("@RebateSignIn", SqlDbType.NVarChar,50),
                    new SqlParameter("@RebateYear", SqlDbType.Int,4),
                    new SqlParameter("@RebateMonth", SqlDbType.Int,4),
                    new SqlParameter("@InvoiceType", SqlDbType.NVarChar,50),
                    new SqlParameter("@RebateType", SqlDbType.NVarChar,50)
                                        };
            parameters[0].Value = model.PaymentCode;
            parameters[1].Value = model.ProjectID;
            parameters[2].Value = model.ProjectCode;
            parameters[3].Value = model.PaymentPreDate;
            parameters[4].Value = model.PaymentFactDate;
            parameters[5].Value = model.PaymentContent;
            parameters[6].Value = model.PaymentStatus;
            parameters[7].Value = model.PaymentBudget;
            parameters[8].Value = model.PaymentFee;
            parameters[9].Value = model.InvoiceNo;
            parameters[10].Value = model.InvoiceDate;
            parameters[11].Value = model.BranchID;
            parameters[12].Value = model.BranchCode;
            parameters[13].Value = model.BranchName;
            parameters[14].Value = model.Remark;
            parameters[15].Value = model.BankID;
            parameters[16].Value = model.BankName;
            parameters[17].Value = model.DBCode;
            parameters[18].Value = model.DBManager;
            parameters[19].Value = model.BankAccount;
            parameters[20].Value = model.BankAccountName;
            parameters[21].Value = model.PhoneNo;
            parameters[22].Value = model.ExchangeNo;
            parameters[23].Value = model.RequestPhone;
            parameters[24].Value = model.BankAddress;
            parameters[25].Value = model.PaymentTypeID;
            parameters[26].Value = model.PaymentTypeName;
            parameters[27].Value = model.PaymentTypeCode;
            parameters[28].Value = model.PaymentUserID;
            parameters[29].Value = model.PaymentUserCode;
            parameters[30].Value = model.PaymentEmployeeName;
            parameters[31].Value = model.PaymentUserName;
            parameters[32].Value = model.CreditCode;
            parameters[33].Value = model.USDDiffer;
            parameters[34].Value = model.PaymentExtensionStatus;

            parameters[35].Value = model.BudgetForiegn;
            parameters[36].Value = model.BudgetForiegnUnit;
            parameters[37].Value = model.PaymentFactForiegn;
            parameters[38].Value = model.PaymentFactForiegnUnit;
            parameters[39].Value = model.InvoiceTitle;
            parameters[40].Value = model.InvoiceAmount;
            parameters[41].Value = model.InvoiceReceiver;
            parameters[42].Value = model.InvoiceSignIn;
            parameters[43].Value = model.ConfirmRemark;
            parameters[44].Value = model.BillDate;
            parameters[45].Value = model.EstReturnDate;
            parameters[46].Value = model.PaymentBudgetConfirm;
            parameters[47].Value = model.BadDebt;
            parameters[48].Value = model.ConfirmYear;
            parameters[49].Value = model.ConfirmMonth;
            parameters[50].Value = model.InnerRelation;
            parameters[51].Value = model.ReBateTitle;
            parameters[52].Value = model.RebateNo;
            parameters[53].Value = model.RebateDate;
            parameters[54].Value = model.RebateAmount;
            parameters[55].Value = model.RebateReceiver;
            parameters[56].Value = model.RebateSignIn;
            parameters[57].Value = model.RebateYear;
            parameters[58].Value = model.RebateMonth;
            parameters[59].Value = model.InvoiceType;
            parameters[60].Value = model.RebateType;

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

        public  int UpdatePaymentBankInfo(int projectId, int bankId )
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update F_Payment set ");
            strSql.Append(" BankId=@BankId,BankName=''  ");
            strSql.Append(" where ProjectID=@ProjectID ");
            SqlParameter[] parameters = {
					new SqlParameter("@BankId", SqlDbType.Int,4),
                    new SqlParameter("@ProjectID", SqlDbType.Int,4)                    
                                        };
            parameters[0].Value = bankId;
            parameters[1].Value = projectId;

            return DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
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
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update F_Payment set ");
            strSql.Append(" ProjectCode=@ProjectCode ");
            strSql.Append(" where ProjectID=@ProjectID ");
            SqlParameter[] parameters = {
					new SqlParameter("@ProjectID", SqlDbType.Int,4),
					new SqlParameter("@ProjectCode", SqlDbType.NVarChar,50)};
            parameters[0].Value = projectId;
            parameters[1].Value = projectCode;
            return DbHelperSQL.ExecuteSql(strSql.ToString(), trans, parameters);
        }

        public int UpdatePaymentBudgetConfirm(int projectid, SqlTransaction trans)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update F_Payment set ");
            strSql.Append(" PaymentBudgetConfirm=PaymentBudget ");
            strSql.Append(" where ProjectID=@ProjectID ");
            SqlParameter[] parameters = {
					new SqlParameter("@ProjectID", SqlDbType.Int,4)};
            parameters[0].Value = projectid;
            return DbHelperSQL.ExecuteSql(strSql.ToString(), trans, parameters);
        }

        ///// <summary>
        ///// 更新一条数据
        ///// </summary>
        //public int Update(ESP.Finance.Entity.PaymentInfo model)
        //{
        //    return Update(model, false);
        //}
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public int Update(ESP.Finance.Entity.PaymentInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update F_Payment set ");
            strSql.Append("PaymentCode=@PaymentCode,");
            strSql.Append("ProjectID=@ProjectID,");
            strSql.Append("ProjectCode=@ProjectCode,");
            strSql.Append("PaymentPreDate=@PaymentPreDate,");
            strSql.Append("PaymentFactDate=@PaymentFactDate,");
            strSql.Append("PaymentContent=@PaymentContent,");
            strSql.Append("PaymentStatus=@PaymentStatus,");
            strSql.Append("PaymentBudget=@PaymentBudget,");
            strSql.Append("PaymentFee=@PaymentFee,");
            strSql.Append("InvoiceNo=@InvoiceNo,");
            strSql.Append("InvoiceDate=@InvoiceDate,");
            strSql.Append("BranchID=@BranchID,");
            strSql.Append("BranchCode=@BranchCode,");
            strSql.Append("BranchName=@BranchName,");
            strSql.Append("Remark=@Remark,");
            strSql.Append("BankID=@BankID,");
            strSql.Append("BankName=@BankName,");
            strSql.Append("DBCode=@DBCode,");
            strSql.Append("DBManager=@DBManager,");
            strSql.Append("BankAccount=@BankAccount,");
            strSql.Append("BankAccountName=@BankAccountName,");
            strSql.Append("PhoneNo=@PhoneNo,");
            strSql.Append("ExchangeNo=@ExchangeNo,");
            strSql.Append("RequestPhone=@RequestPhone,");
            strSql.Append("BankAddress=@BankAddress,");
            strSql.Append("PaymentTypeID=@PaymentTypeID,");
            strSql.Append("PaymentTypeName=@PaymentTypeName,");
            strSql.Append("PaymentTypeCode=@PaymentTypeCode,");
            strSql.Append("PaymentUserID=@PaymentUserID,");
            strSql.Append("PaymentUserCode=@PaymentUserCode,");
            strSql.Append("PaymentEmployeeName=@PaymentEmployeeName,");
            strSql.Append("PaymentUserName=@PaymentUserName,");
            strSql.Append("CreditCode=@CreditCode, ");
            strSql.Append("USDDiffer=@USDDiffer,PaymentExtensionStatus=@PaymentExtensionStatus, ");
            strSql.Append("BudgetForiegn=@BudgetForiegn,BudgetForiegnUnit=@BudgetForiegnUnit,PaymentFactForiegn=@PaymentFactForiegn,PaymentFactForiegnUnit=@PaymentFactForiegnUnit,InvoiceTitle=@InvoiceTitle,InvoiceAmount=@InvoiceAmount,InvoiceReceiver=@InvoiceReceiver,InvoiceSignIn=@InvoiceSignIn,ConfirmRemark=@ConfirmRemark,BillDate=@BillDate,EstReturnDate=@EstReturnDate, ");
            strSql.Append("PaymentBudgetConfirm=@PaymentBudgetConfirm,BadDebt=@BadDebt,ConfirmYear=@ConfirmYear,ConfirmMonth=@ConfirmMonth,InnerRelation=@InnerRelation, ");
            strSql.Append("ReBateTitle=@ReBateTitle,RebateNo=@RebateNo,RebateDate=@RebateDate,RebateAmount=@RebateAmount,RebateReceiver=@RebateReceiver,RebateSignIn=@RebateSignIn,RebateYear=@RebateYear,RebateMonth=@RebateMonth, ");
            strSql.Append("InvoiceType=@InvoiceType,RebateType=@RebateType ");
            strSql.Append(" where PaymentID=@PaymentID  and Lastupdatetime <= @Lastupdatetime ");
            SqlParameter[] parameters = {
					new SqlParameter("@PaymentID", SqlDbType.Int,4),
					new SqlParameter("@PaymentCode", SqlDbType.NVarChar,50),
					new SqlParameter("@ProjectID", SqlDbType.Int,4),
					new SqlParameter("@ProjectCode", SqlDbType.NVarChar,50),
					new SqlParameter("@PaymentPreDate", SqlDbType.DateTime),
					new SqlParameter("@PaymentFactDate", SqlDbType.DateTime),
					new SqlParameter("@PaymentContent", SqlDbType.NVarChar,500),
					new SqlParameter("@PaymentStatus", SqlDbType.Int,4),
					new SqlParameter("@PaymentBudget", SqlDbType.Decimal,20),
					new SqlParameter("@PaymentFee", SqlDbType.Decimal,9),
					new SqlParameter("@InvoiceNo", SqlDbType.NVarChar,100),
					new SqlParameter("@InvoiceDate", SqlDbType.DateTime),
					new SqlParameter("@BranchID", SqlDbType.Int,4),
					new SqlParameter("@BranchCode", SqlDbType.NVarChar,10),
					new SqlParameter("@BranchName", SqlDbType.NVarChar,200),
					new SqlParameter("@Lastupdatetime", SqlDbType.Timestamp,8),
					new SqlParameter("@Remark", SqlDbType.NVarChar,500),
					new SqlParameter("@BankID", SqlDbType.Int,4),
					new SqlParameter("@BankName", SqlDbType.NVarChar,100),
					new SqlParameter("@DBCode", SqlDbType.NVarChar,10),
					new SqlParameter("@DBManager", SqlDbType.NVarChar,10),
					new SqlParameter("@BankAccount", SqlDbType.NVarChar,50),
					new SqlParameter("@BankAccountName", SqlDbType.NVarChar,50),
					new SqlParameter("@PhoneNo", SqlDbType.NVarChar,50),
					new SqlParameter("@ExchangeNo", SqlDbType.NVarChar,50),
					new SqlParameter("@RequestPhone", SqlDbType.NVarChar,50),
					new SqlParameter("@BankAddress", SqlDbType.NVarChar,100),
					new SqlParameter("@PaymentTypeID", SqlDbType.Int,4),
					new SqlParameter("@PaymentTypeName", SqlDbType.NVarChar,50),
					new SqlParameter("@PaymentTypeCode", SqlDbType.NVarChar,50),
                    new SqlParameter("@PaymentUserID", SqlDbType.Int,4),
                    new SqlParameter("@PaymentUserCode", SqlDbType.NVarChar,50),
                    new SqlParameter("@PaymentEmployeeName", SqlDbType.NVarChar,50),
                    new SqlParameter("@PaymentUserName", SqlDbType.NVarChar,50),
                    new SqlParameter("@CreditCode",SqlDbType.NVarChar,25),
                    new SqlParameter("@USDDiffer",SqlDbType.Decimal,9),
                    new SqlParameter("@PaymentExtensionStatus",SqlDbType.Int,9),
                    new SqlParameter("@BudgetForiegn", SqlDbType.Decimal,20),
                    new SqlParameter("@BudgetForiegnUnit", SqlDbType.NVarChar,10),
                    new SqlParameter("@PaymentFactForiegn", SqlDbType.Decimal,20),
                    new SqlParameter("@PaymentFactForiegnUnit", SqlDbType.NVarChar,10),
                    new SqlParameter("@InvoiceTitle", SqlDbType.NVarChar,50),
                    new SqlParameter("@InvoiceAmount", SqlDbType.Decimal,20),
                    new SqlParameter("@InvoiceReceiver", SqlDbType.NVarChar,50),
                    new SqlParameter("@InvoiceSignIn", SqlDbType.NVarChar,50),
                    new SqlParameter("@ConfirmRemark", SqlDbType.NVarChar,50),
                    new SqlParameter("@BillDate", SqlDbType.DateTime),
                    new SqlParameter("@EstReturnDate", SqlDbType.DateTime),
                    new SqlParameter("@PaymentBudgetConfirm", SqlDbType.Decimal,20),
                    new SqlParameter("@BadDebt",SqlDbType.Int,9),
                    new SqlParameter("@ConfirmYear", SqlDbType.Int,4),
                    new SqlParameter("@ConfirmMonth", SqlDbType.Int,4),
                    new SqlParameter("@InnerRelation", SqlDbType.Int,4),
                    new SqlParameter("@ReBateTitle", SqlDbType.NVarChar,100),
                    new SqlParameter("@RebateNo", SqlDbType.NVarChar,100),
                    new SqlParameter("@RebateDate", SqlDbType.DateTime),
                    new SqlParameter("@RebateAmount", SqlDbType.Decimal,20),
                    new SqlParameter("@RebateReceiver", SqlDbType.NVarChar,50),
                    new SqlParameter("@RebateSignIn", SqlDbType.NVarChar,50),
                    new SqlParameter("@RebateYear", SqlDbType.Int,4),
                    new SqlParameter("@RebateMonth", SqlDbType.Int,4),
                    new SqlParameter("@InvoiceType", SqlDbType.NVarChar,50),
                    new SqlParameter("@RebateType", SqlDbType.NVarChar,50)
                                        };
            parameters[0].Value = model.PaymentID;
            parameters[1].Value = model.PaymentCode;
            parameters[2].Value = model.ProjectID;
            parameters[3].Value = model.ProjectCode;
            parameters[4].Value = model.PaymentPreDate;
            parameters[5].Value = model.PaymentFactDate;
            parameters[6].Value = model.PaymentContent;
            parameters[7].Value = model.PaymentStatus;
            parameters[8].Value = model.PaymentBudget;
            parameters[9].Value = model.PaymentFee;
            parameters[10].Value = model.InvoiceNo;
            parameters[11].Value = model.InvoiceDate;
            parameters[12].Value = model.BranchID;
            parameters[13].Value = model.BranchCode;
            parameters[14].Value = model.BranchName;
            parameters[15].Value = model.Lastupdatetime;
            parameters[16].Value = model.Remark;
            parameters[17].Value = model.BankID;
            parameters[18].Value = model.BankName;
            parameters[19].Value = model.DBCode;
            parameters[20].Value = model.DBManager;
            parameters[21].Value = model.BankAccount;
            parameters[22].Value = model.BankAccountName;
            parameters[23].Value = model.PhoneNo;
            parameters[24].Value = model.ExchangeNo;
            parameters[25].Value = model.RequestPhone;
            parameters[26].Value = model.BankAddress;
            parameters[27].Value = model.PaymentTypeID;
            parameters[28].Value = model.PaymentTypeName;
            parameters[29].Value = model.PaymentTypeCode;
            parameters[30].Value = model.PaymentUserID;
            parameters[31].Value = model.PaymentUserCode;
            parameters[32].Value = model.PaymentEmployeeName;
            parameters[33].Value = model.PaymentUserName;
            parameters[34].Value = model.CreditCode;
            parameters[35].Value = model.USDDiffer;
            parameters[36].Value = model.PaymentExtensionStatus;

            parameters[37].Value = model.BudgetForiegn;
            parameters[38].Value = model.BudgetForiegnUnit;
            parameters[39].Value = model.PaymentFactForiegn;
            parameters[40].Value = model.PaymentFactForiegnUnit;
            parameters[41].Value = model.InvoiceTitle;
            parameters[42].Value = model.InvoiceAmount;
            parameters[43].Value = model.InvoiceReceiver;
            parameters[44].Value = model.InvoiceSignIn;
            parameters[45].Value = model.ConfirmRemark;
            parameters[46].Value = model.BillDate;
            parameters[47].Value = model.EstReturnDate;
            parameters[48].Value = model.PaymentBudgetConfirm;
            parameters[49].Value = model.BadDebt;
            parameters[50].Value = model.ConfirmYear;
            parameters[51].Value = model.ConfirmMonth;
            parameters[52].Value = model.InnerRelation;
            parameters[53].Value = model.ReBateTitle;
            parameters[54].Value = model.RebateNo;
            parameters[55].Value = model.RebateDate;
            parameters[56].Value = model.RebateAmount;
            parameters[57].Value = model.RebateReceiver;
            parameters[58].Value = model.RebateSignIn;
            parameters[59].Value = model.RebateYear;
            parameters[60].Value = model.RebateMonth;
            parameters[61].Value = model.InvoiceType;
            parameters[62].Value = model.RebateType;

            return DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public int Delete(int PaymentID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete F_Payment ");
            strSql.Append(" where PaymentID=@PaymentID ");
            SqlParameter[] parameters = {
					new SqlParameter("@PaymentID", SqlDbType.Int,4)};
            parameters[0].Value = PaymentID;

            return DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }

        public int DeleteByProject(int projectId)
        {
            return DeleteByProject(projectId, null);
        }

        /// <summary>
        /// 根据项目删除记录
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="isInTrans"></param>
        /// <returns></returns>
        public int DeleteByProject(int projectId, SqlTransaction trans)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete F_Payment ");
            strSql.Append(" where ProjectID=@ProjectID ");
            SqlParameter[] parameters = {
					new SqlParameter("@ProjectID", SqlDbType.Int,4)};
            parameters[0].Value = projectId;

            return DbHelperSQL.ExecuteSql(strSql.ToString(), trans, parameters);
        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public ESP.Finance.Entity.PaymentInfo GetModel(int PaymentID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 * ");
            strSql.Append(" FROM F_Payment ");
            strSql.Append(" where PaymentID=@PaymentID ");
            SqlParameter[] parameters = {
					new SqlParameter("@PaymentID", SqlDbType.Int,4)};
            parameters[0].Value = PaymentID;

            return CBO.FillObject<ESP.Finance.Entity.PaymentInfo>(DbHelperSQL.Query(strSql.ToString(), parameters));
        }

        public IList<ESP.Finance.Entity.PaymentInfo> GetList(string term, List<SqlParameter> param)
        {
            return GetList(term, param, null);
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public IList<ESP.Finance.Entity.PaymentInfo> GetList(string term, List<SqlParameter> param, SqlTransaction trans)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * FROM F_Payment ");
            if (!string.IsNullOrEmpty(term))
            {
                strSql.Append(" where " + term);
            }
            strSql.Append(" order by PaymentPreDate");
            return CBO.FillCollection<ESP.Finance.Entity.PaymentInfo>(DbHelperSQL.Query(strSql.ToString(), trans, (param == null ? null : param.ToArray())));
        }

        public IList<ESP.Finance.Entity.PaymentInfo> GetListByProject(int projectID, string term, List<System.Data.SqlClient.SqlParameter> param)
        {
            return GetListByProject(projectID, term, param, null);
        }

        public IList<ESP.Finance.Entity.PaymentInfo> GetListByProject(int projectID, string term, List<System.Data.SqlClient.SqlParameter> param, SqlTransaction trans)
        {
            if (string.IsNullOrEmpty(term))
            {
                term = " 1=1 ";
            }
            if (param == null)
            {
                param = new List<SqlParameter>();
            }

            term += " and ProjectId = @ProjectId";
            SqlParameter pm = new SqlParameter("@ProjectId", SqlDbType.Int, 4);
            pm.Value = projectID;

            param.Add(pm);

            return GetList(term, param, trans);
        }

        public IList<PaymentInfo> GetWaitAuditList(string userIds, string strTerms, List<SqlParameter> parms)
        {
            System.Data.DataTable dt = DbHelperSQL.RunProcedure("P_GetAllTaskItems", new IDataParameter[1], "Ta").Tables[0];
            DataRow[] rows = dt.Select(" approverid in (" + userIds.TrimEnd(',') + ") and operationType='待审批付款通知'");
            string paymentIds = "";
            for (int i = 0; i < rows.Length; i++)
            {
                paymentIds += rows[i]["FromID"].ToString() + ",";
            }
            strTerms = " 1=1" + strTerms;
            strTerms += paymentIds == "" ? " and PaymentID=0" : " and PaymentID in (" + paymentIds.TrimEnd(',') + ")";
            return GetList(strTerms, parms);
        }
        #endregion  成员方法


        public IList<PaymentInfo> GetWaitAuditList(int[] userIds)
        {
            string useridstr = string.Empty;
            if (userIds == null || userIds.Length == 0)
                return new List<PaymentInfo>();
            for (int i = 0; i < userIds.Length; i++)
            {
                useridstr += userIds[i].ToString() + ",";
            }
            useridstr = useridstr.TrimEnd(',');
            // or (projectid in(select projectid from f_project where branchcode in(select branchcode from f_branch where projectaccounter in({0}))) and PaymentExtensionStatus=1)
            string str = string.Format(@" (PaymentExtensionStatus=1 and PaymentID in (select PaymentID from F_PaymentAuditHist where AuditorUserID in({0}) and AuditType=2)) 
                                          or (PaymentExtensionStatus=2 and PaymentID in (select PaymentID from F_PaymentAuditHist where AuditorUserID in({0}) and AuditType =11)) 
                                          or (PaymentStatus =1 and PaymentID in (select PaymentID from F_PaymentAuditHist where AuditorUserID in({0}) and AuditType=2)) 
                                          or (PaymentStatus =2 and PaymentID in (select PaymentID from F_PaymentAuditHist where AuditorUserID in({0}) and AuditType=11))
                                          ", useridstr);
            return GetList(str, null);

        }


        public DataTable GetPaymentReportList(string term)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"select a.ProjectID,a.PaymentID, PaymentCode,PaymentPreDate,replace((select top 1 namecn1 from F_CustomerTmp where ProjectID =b.ProjectId order by CustomerTmpID desc),' ','') cutomername,b.ProjectCode,b.BusinessDescription,a.PaymentContent,
datediff(day,PaymentPreDate,GETDATE()) paymentAge,b.groupid,b.branchcode,c.level3 dept,a.PaymentBudget,b.ApplicantEmployeeName,a.Remark,ba.BankName,a.InvoiceTitle,
a.InvoiceDate,a.InvoiceNo,a.InvoiceAmount,datediff(day,a.InvoiceDate,GETDATE()) invoiceAge,a.InvoiceReceiver,a.InvoiceSignIn,
a.ConfirmRemark ,a.PaymentFactDate,a.PaymentFee,a.PaymentFactForiegn,a.PaymentFactForiegnUnit,a.USDDiffer,a.PaymentBudgetConfirm,
case when datediff(day,PaymentPreDate,GETDATE())<=180 then PaymentBudgetConfirm else 0 end as '180',
case when datediff(day,PaymentPreDate,GETDATE())>180 and datediff(day,PaymentPreDate,GETDATE())<=365 then PaymentBudgetConfirm else 0 end as '180to365',
case when datediff(day,PaymentPreDate,GETDATE())>365 and datediff(day,PaymentPreDate,GETDATE())<=730 then PaymentBudgetConfirm else 0 end as '365to730',
case when datediff(day,PaymentPreDate,GETDATE())>730 then PaymentBudgetConfirm else 0 end as '730',
a.confirmYear ,a.confirmMonth,a.BadDebt,a.InnerRelation
 from F_Payment a join F_Project b on a.ProjectID =b.ProjectId 
 join V_Department c on b.GroupID =c.level3Id
 left join F_Bank ba on a.BankId =ba.BankID
 where (PaymentCode <>'' and PaymentBudgetConfirm<>0)");

            if (!string.IsNullOrEmpty(term))
            {
                strSql.Append(term);
            }
            strSql.Append("  order by c.ordinal,cutomername,b.ProjectCode,paymentage desc");

            return DbHelperSQL.Query(strSql.ToString()).Tables[0];


        }

        public DataTable GetPaymentReportListByMonth(string groupids, int year, int month)
        {
            DateTime dt1 = new DateTime(year,month,1);
            DateTime dt2 = new DateTime(year, month, DateTime.DaysInMonth(year,month));

            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"select a.GroupID ,c.level3,replace((select top 1 NameCN1 from F_CustomerTmp where ProjectID =a.ProjectId),' ','') customer,
a.ProjectCode,a.BusinessDescription,b.EstReturnDate,b.PaymentBudget,b.PaymentBudgetConfirm
from F_Project a join F_Payment b on a.ProjectId=b.ProjectID
join V_Department c on a.GroupID =c.level3Id
where  a.ProjectCode<>'' and b.PaymentBudgetConfirm<>0 and (b.EstReturnDate between @beginTime and @endTime)");

            if (!string.IsNullOrEmpty(groupids))
            {
                strSql.Append(groupids);
            }
            strSql.Append(" order by c.ordinal,a.ProjectCode");

            SqlParameter[] parameters = {
                    new SqlParameter("@beginTime",SqlDbType.DateTime,8),
                    new SqlParameter("@endTime",SqlDbType.DateTime,8)};
            parameters[0].Value = dt1;
            parameters[1].Value = dt2;
            return DbHelperSQL.Query(strSql.ToString(),parameters).Tables[0];

        }

        public DataTable GetPaymentReportByGroup(string term)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"select level3,SUM(case when paymentAge<=180 then PaymentBudgetConfirm else 0 end) as '180',
SUM(case when paymentAge>180 and paymentAge<=365 then PaymentBudgetConfirm else 0 end) as '180to365',
SUM(case when paymentAge>365 and paymentAge<=730 then PaymentBudgetConfirm else 0 end) as '365to730',
SUM(case when paymentAge>730 then PaymentBudgetConfirm else 0 end) as '730',
SUM(PaymentBudgetConfirm) as 'total'
from(
select a.ProjectID,a.PaymentID, PaymentCode,PaymentPreDate,replace((select top 1 namecn1 from F_CustomerTmp where ProjectID =b.ProjectId order by CustomerTmpID desc),' ','') cutomername,b.ProjectCode,b.BusinessDescription,a.PaymentContent,
datediff(day,PaymentPreDate,GETDATE()) paymentAge,b.groupid,b.branchcode,c.level3,a.PaymentBudgetConfirm,b.ApplicantEmployeeName,a.Remark,ba.BankName,a.InvoiceTitle,
a.InvoiceDate,a.InvoiceNo,a.InvoiceAmount,datediff(day,a.InvoiceDate,GETDATE()) invoiceAge,a.InvoiceReceiver,a.InvoiceSignIn,
a.ConfirmRemark ,a.PaymentFactDate,a.PaymentFee,a.PaymentFactForiegn,a.PaymentFactForiegnUnit,a.USDDiffer,c.Ordinal
 from F_Payment a join F_Project b on a.ProjectID =b.ProjectId 
 join V_Department c on b.GroupID =c.level3Id
 left join F_Bank ba on a.BankId =ba.BankID
 where PaymentCode <>'' and PaymentBudgetConfirm<>0");
            
            if (!string.IsNullOrEmpty(term))
            {
                strSql.Append(term);
            }
            strSql.Append(") searchResults  group by level3,ordinal order by  ordinal");

            return DbHelperSQL.Query(strSql.ToString()).Tables[0];

        }

        public DataTable GetPaymentReportByCustomer(string term)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"select cutomername,SUM(case when paymentAge<=180 then PaymentBudgetConfirm else 0 end) as '180',
SUM(case when paymentAge>180 and paymentAge<=365 then PaymentBudgetConfirm else 0 end) as '180to365',
SUM(case when paymentAge>365 and paymentAge<=730 then PaymentBudgetConfirm else 0 end) as '365to730',
SUM(case when paymentAge>730 then PaymentBudgetConfirm else 0 end) as '730',
SUM(PaymentBudgetConfirm) as 'total'
from(
select a.ProjectID,a.PaymentID, PaymentCode,PaymentPreDate,replace((select top 1 namecn1 from F_CustomerTmp where ProjectID =b.ProjectId order by CustomerTmpID desc),' ','') cutomername,b.ProjectCode,b.BusinessDescription,a.PaymentContent,
datediff(day,PaymentPreDate,GETDATE()) paymentAge,b.groupid,b.branchcode,c.level3 dept,a.PaymentBudgetConfirm,b.ApplicantEmployeeName,a.Remark,ba.BankName,a.InvoiceTitle,
a.InvoiceDate,a.InvoiceNo,a.InvoiceAmount,datediff(day,a.InvoiceDate,GETDATE()) invoiceAge,a.InvoiceReceiver,a.InvoiceSignIn,
a.ConfirmRemark ,a.PaymentFactDate,a.PaymentFee,a.PaymentFactForiegn,a.PaymentFactForiegnUnit,a.USDDiffer
 from F_Payment a join F_Project b on a.ProjectID =b.ProjectId 
 join V_Department c on b.GroupID =c.level3Id
 left join F_Bank ba on a.BankId =ba.BankID
 where PaymentCode <>'' and PaymentBudgetConfirm<>0");
            if (!string.IsNullOrEmpty(term))
            {
                strSql.Append(term);
            }

            strSql.Append(") searchResults  group by cutomername order by total desc,cutomername");

            return DbHelperSQL.Query(strSql.ToString()).Tables[0];

        }


        public int UpdatePaymentConfirmMonth(int projectId, int year, int month)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update F_Payment set ");
            strSql.Append("ConfirmYear=@ConfirmYear,ConfirmMonth=@ConfirmMonth  where ProjectId = @ProjectId");

            SqlParameter[] parameters = {
					new SqlParameter("@ConfirmYear", SqlDbType.Int,4),
					new SqlParameter("@ConfirmMonth", SqlDbType.Int,4),
					new SqlParameter("@ProjectId", SqlDbType.Int,4)
                                         };

            parameters[0].Value = year;
            parameters[1].Value = month;
            parameters[2].Value = projectId;
            

            return DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }
    }
}

