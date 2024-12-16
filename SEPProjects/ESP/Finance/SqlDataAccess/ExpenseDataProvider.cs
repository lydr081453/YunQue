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
	/// 数据访问类ExpenseDAL。
	/// </summary>
    internal class ExpenseDataProvider : ESP.Finance.IDataAccess.IExpenseDataProvider
	{
		
		#region  成员方法



		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int ExpenseID)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from F_Expense");
			strSql.Append(" where ExpenseID=@ExpenseID ");
			SqlParameter[] parameters = {
					new SqlParameter("@ExpenseID", SqlDbType.Int,4)};
			parameters[0].Value = ExpenseID;

			return DbHelperSQL.Exists(strSql.ToString(),parameters);
		}


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(ESP.Finance.Entity.ExpenseInfo model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into F_Expense(");
			strSql.Append("DeptID,DeptName,BranchID,BranchName,ExpenseType,LastMonthBalance,Debit,Loan,ConfirmStatus,Balance,ProjectID,CreditCode,InvoiceNo,ReturnOrder,ProjectCode,ApplicantUserID,ApplicantUserName,ApplicantCode,ApplicantEmployeeName,Description,CheckCode)");
			strSql.Append(" values (");
			strSql.Append("@DeptID,@DeptName,@BranchID,@BranchName,@ExpenseType,@LastMonthBalance,@Debit,@Loan,@ConfirmStatus,@Balance,@ProjectID,@CreditCode,@InvoiceNo,@ReturnOrder,@ProjectCode,@ApplicantUserID,@ApplicantUserName,@ApplicantCode,@ApplicantEmployeeName,@Description,@CheckCode)");
            strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = {
					new SqlParameter("@DeptID", SqlDbType.Int,4),
					new SqlParameter("@DeptName", SqlDbType.NVarChar,50),
					new SqlParameter("@BranchID", SqlDbType.Int,4),
					new SqlParameter("@BranchName", SqlDbType.NVarChar,50),
					new SqlParameter("@ExpenseType", SqlDbType.Int,4),
					new SqlParameter("@LastMonthBalance", SqlDbType.Decimal,9),
					new SqlParameter("@Debit", SqlDbType.Decimal,9),
					new SqlParameter("@Loan", SqlDbType.Decimal,9),
					new SqlParameter("@ConfirmStatus", SqlDbType.Int,4),
					new SqlParameter("@Balance", SqlDbType.Decimal,9),
					new SqlParameter("@ProjectID", SqlDbType.Int,4),
					new SqlParameter("@CreditCode", SqlDbType.NVarChar,50),
					new SqlParameter("@InvoiceNo", SqlDbType.NVarChar,50),
					new SqlParameter("@ReturnOrder", SqlDbType.NVarChar,50),
					//new SqlParameter("@Lastupdatetime", SqlDbType.Timestamp,8),
					new SqlParameter("@ProjectCode", SqlDbType.NVarChar,50),
					new SqlParameter("@ApplicantUserID", SqlDbType.Int,4),
					new SqlParameter("@ApplicantUserName", SqlDbType.NVarChar,50),
					new SqlParameter("@ApplicantCode", SqlDbType.NVarChar,50),
					new SqlParameter("@ApplicantEmployeeName", SqlDbType.NVarChar,50),
					new SqlParameter("@Description", SqlDbType.NVarChar,500),
					new SqlParameter("@CheckCode", SqlDbType.NVarChar,50)};
			parameters[0].Value =model.DeptID;
			parameters[1].Value =model.DeptName;
			parameters[2].Value =model.BranchID;
			parameters[3].Value =model.BranchName;
			parameters[4].Value =model.ExpenseType;
			parameters[5].Value =model.LastMonthBalance;
			parameters[6].Value =model.Debit;
			parameters[7].Value =model.Loan;
			parameters[8].Value =model.ConfirmStatus;
			parameters[9].Value =model.Balance;
			parameters[10].Value =model.ProjectID;
			parameters[11].Value =model.CreditCode;
			parameters[12].Value =model.InvoiceNo;
			parameters[13].Value =model.ReturnOrder;
			//parameters[14].Value =model.Lastupdatetime;
			parameters[14].Value =model.ProjectCode;
			parameters[15].Value =model.ApplicantUserID;
			parameters[16].Value =model.ApplicantUserName;
			parameters[17].Value =model.ApplicantCode;
			parameters[18].Value =model.ApplicantEmployeeName;
			parameters[19].Value =model.Description;
			parameters[20].Value =model.CheckCode;

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
		/// <summary>
		/// 更新一条数据
		/// </summary>
		public int Update(ESP.Finance.Entity.ExpenseInfo model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update F_Expense set ");
			strSql.Append("DeptID=@DeptID,");
			strSql.Append("DeptName=@DeptName,");
			strSql.Append("BranchID=@BranchID,");
			strSql.Append("BranchName=@BranchName,");
			strSql.Append("ExpenseType=@ExpenseType,");
			strSql.Append("LastMonthBalance=@LastMonthBalance,");
			strSql.Append("Debit=@Debit,");
			strSql.Append("Loan=@Loan,");
			strSql.Append("ConfirmStatus=@ConfirmStatus,");
			strSql.Append("Balance=@Balance,");
			strSql.Append("ProjectID=@ProjectID,");
			strSql.Append("CreditCode=@CreditCode,");
			strSql.Append("InvoiceNo=@InvoiceNo,");
			strSql.Append("ReturnOrder=@ReturnOrder,");
			//strSql.Append("Lastupdatetime=@Lastupdatetime,");
			strSql.Append("ProjectCode=@ProjectCode,");
			strSql.Append("ApplicantUserID=@ApplicantUserID,");
			strSql.Append("ApplicantUserName=@ApplicantUserName,");
			strSql.Append("ApplicantCode=@ApplicantCode,");
			strSql.Append("ApplicantEmployeeName=@ApplicantEmployeeName,");
			strSql.Append("Description=@Description,");
			strSql.Append("CheckCode=@CheckCode");
            strSql.Append(" where ExpenseID=@ExpenseID and @Lastupdatetime >= Lastupdatetime ");
            SqlParameter[] parameters = {
					new SqlParameter("@ExpenseID", SqlDbType.Int,4),
					new SqlParameter("@DeptID", SqlDbType.Int,4),
					new SqlParameter("@DeptName", SqlDbType.NVarChar,50),
					new SqlParameter("@BranchID", SqlDbType.Int,4),
					new SqlParameter("@BranchName", SqlDbType.NVarChar,50),
					new SqlParameter("@ExpenseType", SqlDbType.Int,4),
					new SqlParameter("@LastMonthBalance", SqlDbType.Decimal,9),
					new SqlParameter("@Debit", SqlDbType.Decimal,9),
					new SqlParameter("@Loan", SqlDbType.Decimal,9),
					new SqlParameter("@ConfirmStatus", SqlDbType.Int,4),
					new SqlParameter("@Balance", SqlDbType.Decimal,9),
					new SqlParameter("@ProjectID", SqlDbType.Int,4),
					new SqlParameter("@CreditCode", SqlDbType.NVarChar,50),
					new SqlParameter("@InvoiceNo", SqlDbType.NVarChar,50),
					new SqlParameter("@ReturnOrder", SqlDbType.NVarChar,50),
					//,
					new SqlParameter("@ProjectCode", SqlDbType.NVarChar,50),
					new SqlParameter("@ApplicantUserID", SqlDbType.Int,4),
					new SqlParameter("@ApplicantUserName", SqlDbType.NVarChar,50),
					new SqlParameter("@ApplicantCode", SqlDbType.NVarChar,50),
					new SqlParameter("@ApplicantEmployeeName", SqlDbType.NVarChar,50),
					new SqlParameter("@Description", SqlDbType.NVarChar,500),
					new SqlParameter("@CheckCode", SqlDbType.NVarChar,50),
                    new SqlParameter("@Lastupdatetime", SqlDbType.Timestamp,8)
                                        };
			parameters[0].Value =model.ExpenseID;
			parameters[1].Value =model.DeptID;
			parameters[2].Value =model.DeptName;
			parameters[3].Value =model.BranchID;
			parameters[4].Value =model.BranchName;
			parameters[5].Value =model.ExpenseType;
			parameters[6].Value =model.LastMonthBalance;
			parameters[7].Value =model.Debit;
			parameters[8].Value =model.Loan;
			parameters[9].Value =model.ConfirmStatus;
			parameters[10].Value =model.Balance;
			parameters[11].Value =model.ProjectID;
			parameters[12].Value =model.CreditCode;
			parameters[13].Value =model.InvoiceNo;
			parameters[14].Value =model.ReturnOrder;
			parameters[15].Value =model.ProjectCode;
			parameters[16].Value =model.ApplicantUserID;
			parameters[17].Value =model.ApplicantUserName;
			parameters[18].Value =model.ApplicantCode;
			parameters[19].Value =model.ApplicantEmployeeName;
			parameters[20].Value =model.Description;
			parameters[21].Value =model.CheckCode;
            parameters[22].Value =model.Lastupdatetime;

			return DbHelperSQL.ExecuteSql(strSql.ToString(),parameters);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public int Delete(int ExpenseID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete F_Expense ");
			strSql.Append(" where ExpenseID=@ExpenseID ");
			SqlParameter[] parameters = {
					new SqlParameter("@ExpenseID", SqlDbType.Int,4)};
			parameters[0].Value = ExpenseID;

			return DbHelperSQL.ExecuteSql(strSql.ToString(),parameters);
		}


		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public ESP.Finance.Entity.ExpenseInfo GetModel(int ExpenseID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 ExpenseID,DeptID,DeptName,BranchID,BranchName,ExpenseType,LastMonthBalance,Debit,Loan,ConfirmStatus,Balance,ProjectID,CreditCode,InvoiceNo,ReturnOrder,Lastupdatetime,ProjectCode,ApplicantUserID,ApplicantUserName,ApplicantCode,ApplicantEmployeeName,Description,CheckCode from F_Expense ");
			strSql.Append(" where ExpenseID=@ExpenseID ");
			SqlParameter[] parameters = {
					new SqlParameter("@ExpenseID", SqlDbType.Int,4)};
			parameters[0].Value = ExpenseID;

            return CBO.FillObject<ExpenseInfo>(DbHelperSQL.Query(strSql.ToString(), parameters));
		}

		/// <summary>
		/// 获得数据列表
		/// </summary>
        public IList<ExpenseInfo> GetList(string term, List<SqlParameter> param)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select ExpenseID,DeptID,DeptName,BranchID,BranchName,ExpenseType,LastMonthBalance,Debit,Loan,ConfirmStatus,Balance,ProjectID,CreditCode,InvoiceNo,ReturnOrder,Lastupdatetime,ProjectCode,ApplicantUserID,ApplicantUserName,ApplicantCode,ApplicantEmployeeName,Description,CheckCode ");
			strSql.Append(" FROM F_Expense ");
            if (!string.IsNullOrEmpty(term))
            {
                strSql.Append(" where " + term);
            }
            //if (param != null && param.Count > 0)
            //{
            //    SqlParameter[] ps = param.ToArray();

            //    return CBO.FillCollection<ExpenseInfo>(DbHelperSQL.ExecuteReader(strSql.ToString(), ps));
            //}
            return CBO.FillCollection<ExpenseInfo>(DbHelperSQL.Query(strSql.ToString(),param));
		}

	

		#endregion  成员方法
	}
}

