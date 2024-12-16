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
	/// 数据访问类InvoiceDAL。
	/// </summary>
    internal class InvoiceDataProvider : ESP.Finance.IDataAccess.IInvoiceDataProvider
	{
		#region  成员方法
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int InvoiceID)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from F_Invoice");
			strSql.Append(" where InvoiceID=@InvoiceID ");
			SqlParameter[] parameters = {
					new SqlParameter("@InvoiceID", SqlDbType.Int,4)};
			parameters[0].Value = InvoiceID;

			return DbHelperSQL.Exists(strSql.ToString(),parameters);
		}



        ///// <summary>
        ///// 是否存在该记录
        ///// </summary>
        //public bool Exists(string term, List<SqlParameter> param)
        //{
        //    return Exists(term, param, false);
        //}

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(string term, List<SqlParameter> param)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(*)");
            strSql.Append(" FROM F_Invoice ");
            if (!string.IsNullOrEmpty(term))
            {
                strSql.Append(" where " + term);
            }
            if (param != null && param.Count > 0)
            {
                return DbHelperSQL.Exists(strSql.ToString(),  param.ToArray());
            }
            return DbHelperSQL.Exists(term);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(ESP.Finance.Entity.InvoiceInfo model)
        {
            return Add(model, null);
        }

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(ESP.Finance.Entity.InvoiceInfo model,SqlTransaction trans)
		{
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into F_Invoice(");
            strSql.Append(@"InvoiceCode,CreateDate,InvoiceAmounts,USDDiffer,CustomerID,
                            CustomerShortName,CustomerName,CustomerTitle,InvoiceStatus,BranchID,
                            BranchCode,BranchName,Remark,CreatorID,CreatorUserCode,
                            CreatorUserName,CreatorEmployeeName,ReceiverUserID,ReceiverUserCode,ReceiverUserName,
                            ReceiverEmployeeName,ProjectId,GroupId,InvoiceDate,CancelDate,ProjectCode,GroupName)");

            strSql.Append(" values (");
            strSql.Append(@"@InvoiceCode,@CreateDate,@InvoiceAmounts,@USDDiffer,@CustomerID,
                            @CustomerShortName,@CustomerName,@CustomerTitle,@InvoiceStatus,@BranchID,
                            @BranchCode,@BranchName,@Remark,@CreatorID,@CreatorUserCode,
                            @CreatorUserName,@CreatorEmployeeName,@ReceiverUserID,@ReceiverUserCode,@ReceiverUserName,
                            @ReceiverEmployeeName,@ProjectId,@GroupId,@InvoiceDate,@CancelDate,@ProjectCode,@GroupName)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@InvoiceCode", SqlDbType.NVarChar,50),
					new SqlParameter("@CreateDate", SqlDbType.DateTime),
					new SqlParameter("@InvoiceAmounts", SqlDbType.Decimal,9),
					new SqlParameter("@USDDiffer", SqlDbType.Decimal,9),
					new SqlParameter("@CustomerID", SqlDbType.Int,4),
					new SqlParameter("@CustomerShortName", SqlDbType.NVarChar,50),
					new SqlParameter("@CustomerName", SqlDbType.NVarChar,50),
					new SqlParameter("@CustomerTitle", SqlDbType.NVarChar,50),
					new SqlParameter("@InvoiceStatus", SqlDbType.Int,4),
					new SqlParameter("@BranchID", SqlDbType.Int,4),
					new SqlParameter("@BranchCode", SqlDbType.NVarChar,10),
					new SqlParameter("@BranchName", SqlDbType.NVarChar,200),
					new SqlParameter("@Remark", SqlDbType.NVarChar,200),
					new SqlParameter("@CreatorID", SqlDbType.Int,4),
					new SqlParameter("@CreatorUserCode", SqlDbType.NVarChar,50),
					new SqlParameter("@CreatorUserName", SqlDbType.NVarChar,50),
					new SqlParameter("@CreatorEmployeeName", SqlDbType.NVarChar,50),
					new SqlParameter("@ReceiverUserID", SqlDbType.Int,4),
					new SqlParameter("@ReceiverUserCode", SqlDbType.NVarChar,50),
					new SqlParameter("@ReceiverUserName", SqlDbType.NVarChar,50),
					new SqlParameter("@ReceiverEmployeeName", SqlDbType.NVarChar,50),
					new SqlParameter("@ProjectId", SqlDbType.Int,4),
                                        new SqlParameter("@GroupId", SqlDbType.Int,4),
                                        new SqlParameter("@InvoiceDate", SqlDbType.DateTime),
                                        new SqlParameter("@CancelDate", SqlDbType.DateTime),
                                        new SqlParameter("@ProjectCode", SqlDbType.NVarChar,50),
                                        new SqlParameter("@GroupName", SqlDbType.NVarChar,50)};
            parameters[0].Value =model.InvoiceCode;
            parameters[1].Value =model.CreateDate;
            parameters[2].Value =model.InvoiceAmounts;
            parameters[3].Value =model.USDDiffer;
            parameters[4].Value =model.CustomerID;
            parameters[5].Value =model.CustomerShortName;
            parameters[6].Value =model.CustomerName;
            parameters[7].Value =model.CustomerTitle;
            parameters[8].Value =model.InvoiceStatus;
            parameters[9].Value =model.BranchID;
            parameters[10].Value =model.BranchCode;
            parameters[11].Value =model.BranchName;
            parameters[12].Value =model.Remark;
            parameters[13].Value =model.CreatorID;
            parameters[14].Value =model.CreatorUserCode;
            parameters[15].Value =model.CreatorUserName;
            parameters[16].Value =model.CreatorEmployeeName;
            parameters[17].Value =model.ReceiverUserID;
            parameters[18].Value =model.ReceiverUserCode;
            parameters[19].Value =model.ReceiverUserName;
            parameters[20].Value =model.ReceiverEmployeeName;
            parameters[21].Value = model.ProjectId;
            parameters[22].Value = model.GroupId;
            parameters[23].Value = model.InvoiceDate;
            parameters[24].Value = model.CancelDate;
            parameters[25].Value = model.ProjectCode;
            parameters[26].Value = model.GroupName;

            object obj = null;
            if(trans == null)
                obj = DbHelperSQL.GetSingle(strSql.ToString(), parameters);
            else
                obj = DbHelperSQL.GetSingle(strSql.ToString(),trans.Connection,trans, parameters);
            if (obj == null)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(obj);
            }
		}

        public int Update(ESP.Finance.Entity.InvoiceInfo model)
        {
            return Update(model, null);
        }

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public int Update(ESP.Finance.Entity.InvoiceInfo model,SqlTransaction trans)
		{
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update F_Invoice set ");
            strSql.Append("InvoiceCode=@InvoiceCode,");
            strSql.Append("CreateDate=@CreateDate,");
            strSql.Append("InvoiceAmounts=@InvoiceAmounts,");
            strSql.Append("USDDiffer=@USDDiffer,");
            strSql.Append("CustomerID=@CustomerID,");
            strSql.Append("CustomerShortName=@CustomerShortName,");
            strSql.Append("CustomerName=@CustomerName,");
            strSql.Append("CustomerTitle=@CustomerTitle,");
            strSql.Append("InvoiceStatus=@InvoiceStatus,");
            strSql.Append("BranchID=@BranchID,");
            strSql.Append("BranchCode=@BranchCode,");
            strSql.Append("BranchName=@BranchName,");
            strSql.Append("Remark=@Remark,");
            strSql.Append("CreatorID=@CreatorID,");
            strSql.Append("CreatorUserCode=@CreatorUserCode,");
            strSql.Append("CreatorUserName=@CreatorUserName,");
            strSql.Append("CreatorEmployeeName=@CreatorEmployeeName,");
            strSql.Append("ReceiverUserID=@ReceiverUserID,");
            strSql.Append("ReceiverUserCode=@ReceiverUserCode,");
            strSql.Append("ReceiverUserName=@ReceiverUserName,");
            strSql.Append("ReceiverEmployeeName=@ReceiverEmployeeName,");
            strSql.Append("ProjectId=@ProjectId,");
            strSql.Append("GroupId=@GroupId,InvoiceDate=@InvoiceDate,CancelDate=@CancelDate,ProjectCode=@ProjectCode,GroupName=@GroupName");
            strSql.Append(" where InvoiceID=@InvoiceID  ");
            SqlParameter[] parameters = {
					new SqlParameter("@InvoiceID", SqlDbType.Int,4),
					new SqlParameter("@InvoiceCode", SqlDbType.NVarChar,50),
					new SqlParameter("@CreateDate", SqlDbType.DateTime),
					new SqlParameter("@InvoiceAmounts", SqlDbType.Decimal,9),
					new SqlParameter("@USDDiffer", SqlDbType.Decimal,9),
					new SqlParameter("@CustomerID", SqlDbType.Int,4),
					new SqlParameter("@CustomerShortName", SqlDbType.NVarChar,50),
					new SqlParameter("@CustomerName", SqlDbType.NVarChar,50),
					new SqlParameter("@CustomerTitle", SqlDbType.NVarChar,50),
					new SqlParameter("@InvoiceStatus", SqlDbType.Int,4),
					new SqlParameter("@BranchID", SqlDbType.Int,4),
					new SqlParameter("@BranchCode", SqlDbType.NVarChar,10),
					new SqlParameter("@BranchName", SqlDbType.NVarChar,200),
					new SqlParameter("@Remark", SqlDbType.NVarChar,200),
					new SqlParameter("@CreatorID", SqlDbType.Int,4),
					new SqlParameter("@CreatorUserCode", SqlDbType.NVarChar,50),
					new SqlParameter("@CreatorUserName", SqlDbType.NVarChar,50),
					new SqlParameter("@CreatorEmployeeName", SqlDbType.NVarChar,50),
					new SqlParameter("@ReceiverUserID", SqlDbType.Int,4),
					new SqlParameter("@ReceiverUserCode", SqlDbType.NVarChar,50),
					new SqlParameter("@ReceiverUserName", SqlDbType.NVarChar,50),
					new SqlParameter("@ReceiverEmployeeName", SqlDbType.NVarChar,50),
					new SqlParameter("@ProjectId", SqlDbType.Int,4),
                                        new SqlParameter("@GroupId", SqlDbType.Int,4),
                                        new SqlParameter("@InvoiceDate", SqlDbType.DateTime),
                                        new SqlParameter("@CancelDate", SqlDbType.DateTime),
                                        new SqlParameter("@ProjectCode", SqlDbType.NVarChar,50),
                                        new SqlParameter("@GroupName", SqlDbType.NVarChar,50)};
            parameters[0].Value =model.InvoiceID;
            parameters[1].Value =model.InvoiceCode;
            parameters[2].Value =model.CreateDate;
            parameters[3].Value =model.InvoiceAmounts;
            parameters[4].Value =model.USDDiffer;
            parameters[5].Value =model.CustomerID;
            parameters[6].Value =model.CustomerShortName;
            parameters[7].Value =model.CustomerName;
            parameters[8].Value =model.CustomerTitle;
            parameters[9].Value =model.InvoiceStatus;
            parameters[10].Value =model.BranchID;
            parameters[11].Value =model.BranchCode;
            parameters[12].Value =model.BranchName;
            parameters[13].Value =model.Remark;
            parameters[14].Value =model.CreatorID;
            parameters[15].Value =model.CreatorUserCode;
            parameters[16].Value =model.CreatorUserName;
            parameters[17].Value =model.CreatorEmployeeName;
            parameters[18].Value =model.ReceiverUserID;
            parameters[19].Value =model.ReceiverUserCode;
            parameters[20].Value =model.ReceiverUserName;
            parameters[21].Value =model.ReceiverEmployeeName;
            parameters[22].Value = model.ProjectId;
            parameters[23].Value = model.GroupId;
            parameters[24].Value = model.InvoiceDate;
            parameters[25].Value = model.CancelDate;
            parameters[26].Value = model.ProjectCode;
            parameters[27].Value = model.GroupName;

            if (trans == null)
                return DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
            else
                return DbHelperSQL.ExecuteSql(strSql.ToString(),trans, parameters);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public int Delete(int InvoiceID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete F_Invoice ");
			strSql.Append(" where InvoiceID=@InvoiceID ");
			SqlParameter[] parameters = {
					new SqlParameter("@InvoiceID", SqlDbType.Int,4)};
			parameters[0].Value = InvoiceID;

			return DbHelperSQL.ExecuteSql(strSql.ToString(),parameters);
		}


		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public ESP.Finance.Entity.InvoiceInfo GetModel(int InvoiceID)
		{

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append(" FROM F_Invoice ");
            strSql.Append(" where InvoiceID=@InvoiceID ");
            SqlParameter[] parameters = {
					new SqlParameter("@InvoiceID", SqlDbType.Int,4)};
            parameters[0].Value = InvoiceID;

            return CBO.FillObject<ESP.Finance.Entity.InvoiceInfo>(DbHelperSQL.Query(strSql.ToString(), parameters));

		}

		/// <summary>
		/// 获得数据列表
		/// </summary>
        public IList<InvoiceInfo> GetList(string term, List<SqlParameter> param)
		{
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append(" FROM F_Invoice ");
            if (!string.IsNullOrEmpty(term))
            {
                strSql.Append(" where " + term);
            }
            return CBO.FillCollection<ESP.Finance.Entity.InvoiceInfo>(DbHelperSQL.Query(strSql.ToString(), param));
		}



		#endregion  成员方法

        #region IInvoiceProvider 成员


        //public int UpdateStatusByPaymentID(int paymentId, int status)
        //{
        //    return UpdateStatusByPaymentID(paymentId, status, false);
        //}


        public int UpdateStatusByPaymentID(int paymentId,int status)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" update F_Invoice set InvoiceStatus=@InvoiceStatus where PaymentID=@PaymentID ");
            SqlParameter[] sp = new SqlParameter[]
                {
                    new SqlParameter    ("@PaymentID", SqlDbType.Int, 4),
                    new SqlParameter("@InvoiceStatus",SqlDbType.Int,4)
            };
            sp[0].Value = paymentId;
            sp[1].Value = status;
            return DbHelperSQL.ExecuteSql(strSql.ToString(),  sp);
        }

        
        public bool InsertInvoiceAndDetail(InvoiceInfo model, List<InvoiceDetailInfo> dlist)
        {
            InvoiceDetailDataProvider dp = new InvoiceDetailDataProvider();
            using (SqlConnection conn = new SqlConnection(DbHelperSQL.connectionString))
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();
                try
                {
                    //保存主表
                    int invoiceId = 0;
                    if (model.InvoiceID == 0){
                        invoiceId = Add(model,trans);
                    }
                    else
                    {
                        Update(model, trans);
                        invoiceId = model.InvoiceID;
                        //删除detail
                        dp.DeleteByInvoiceId(invoiceId,trans);
                    }
                    foreach (InvoiceDetailInfo d in dlist)
                    {
                        d.InvoiceID = invoiceId;
                        dp.Add(d, trans);
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

        #endregion
    }
}

