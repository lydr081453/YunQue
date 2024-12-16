using System;
using System.Data;
using System.Text;
using System.Collections.Generic;
using System.Data.SqlClient;
using ESP.Finance.Utility;

namespace ESP.Finance.DataAccess
{
    /// <summary>
    /// 数据访问类F_InvoiceDetail。
    /// </summary>
    internal class InvoiceDetailDataProvider : ESP.Finance.IDataAccess.IInvoiceDetailDataProvider 
    {
        
        #region  成员方法
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int InvoiceDetailID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from F_InvoiceDetail");
            strSql.Append(" where InvoiceDetailID=@InvoiceDetailID ");
            SqlParameter[] parameters = {
					new SqlParameter("@InvoiceDetailID", SqlDbType.Int,4)};
            parameters[0].Value = InvoiceDetailID;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(ESP.Finance.Entity.InvoiceDetailInfo model)
        {
            return Add(model, null);
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(ESP.Finance.Entity.InvoiceDetailInfo model,SqlTransaction trans)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into F_InvoiceDetail(");
            strSql.Append(@"PaymentID,PaymentCode,InvoiceID,InvoiceNo,Amounts,
                            USDDiffer,ResponseUserID,ResponseUserName,ResponseCode,ResponseEmployeeName,
                            ProjectID,ProjectCode,CreateDate,Description,Remark)");

            strSql.Append(" values (");
            strSql.Append(@"@PaymentID,@PaymentCode,@InvoiceID,@InvoiceNo,@Amounts,
                            @USDDiffer,@ResponseUserID,@ResponseUserName,@ResponseCode,@ResponseEmployeeName,
                            @ProjectID,@ProjectCode,@CreateDate,@Description,@Remark)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@PaymentID", SqlDbType.Int,4),
					new SqlParameter("@PaymentCode", SqlDbType.NVarChar,50),
					new SqlParameter("@InvoiceID", SqlDbType.Int,4),
					new SqlParameter("@InvoiceNo", SqlDbType.NVarChar,50),
					new SqlParameter("@Amounts", SqlDbType.Decimal,9),
					new SqlParameter("@USDDiffer", SqlDbType.Decimal,9),
					new SqlParameter("@ResponseUserID", SqlDbType.Int,4),
					new SqlParameter("@ResponseUserName", SqlDbType.NVarChar,10),
					new SqlParameter("@ResponseCode", SqlDbType.NVarChar,10),
					new SqlParameter("@ResponseEmployeeName", SqlDbType.NVarChar,50),
					new SqlParameter("@ProjectID", SqlDbType.Int,4),
					new SqlParameter("@ProjectCode", SqlDbType.NVarChar,50),
					new SqlParameter("@CreateDate", SqlDbType.DateTime),
					new SqlParameter("@Description", SqlDbType.NVarChar,200),
					new SqlParameter("@Remark", SqlDbType.NVarChar,200)};
            parameters[0].Value =model.PaymentID;
            parameters[1].Value =model.PaymentCode;
            parameters[2].Value =model.InvoiceID;
            parameters[3].Value =model.InvoiceNo;
            parameters[4].Value =model.Amounts;
            parameters[5].Value =model.USDDiffer;
            parameters[6].Value =model.ResponseUserID;
            parameters[7].Value =model.ResponseUserName;
            parameters[8].Value =model.ResponseCode;
            parameters[9].Value =model.ResponseEmployeeName;
            parameters[10].Value =model.ProjectID;
            parameters[11].Value =model.ProjectCode;
            parameters[12].Value =model.CreateDate;
            parameters[13].Value =model.Description;
            parameters[14].Value =model.Remark;

            object obj = null;
            if (trans == null)
                obj = DbHelperSQL.GetSingle(strSql.ToString(), parameters);
            else
                obj = DbHelperSQL.GetSingle(strSql.ToString(), trans.Connection, trans, parameters);
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
        public int Update(ESP.Finance.Entity.InvoiceDetailInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update F_InvoiceDetail set ");
            strSql.Append("PaymentID=@PaymentID,");
            strSql.Append("PaymentCode=@PaymentCode,");
            strSql.Append("InvoiceID=@InvoiceID,");
            strSql.Append("InvoiceNo=@InvoiceNo,");
            strSql.Append("Amounts=@Amounts,");
            strSql.Append("USDDiffer=@USDDiffer,");
            strSql.Append("ResponseUserID=@ResponseUserID,");
            strSql.Append("ResponseUserName=@ResponseUserName,");
            strSql.Append("ResponseCode=@ResponseCode,");
            strSql.Append("ResponseEmployeeName=@ResponseEmployeeName,");
            strSql.Append("ProjectID=@ProjectID,");
            strSql.Append("ProjectCode=@ProjectCode,");
            strSql.Append("CreateDate=@CreateDate,");
            strSql.Append("Description=@Description,");
            strSql.Append("Remark=@Remark");
            strSql.Append(" where InvoiceDetailID=@InvoiceDetailID  ");
            SqlParameter[] parameters = {
					new SqlParameter("@InvoiceDetailID", SqlDbType.Int,4),
					new SqlParameter("@PaymentID", SqlDbType.Int,4),
					new SqlParameter("@PaymentCode", SqlDbType.NVarChar,50),
					new SqlParameter("@InvoiceID", SqlDbType.Int,4),
					new SqlParameter("@InvoiceNo", SqlDbType.NVarChar,50),
					new SqlParameter("@Amounts", SqlDbType.Decimal,9),
					new SqlParameter("@USDDiffer", SqlDbType.Decimal,9),
					new SqlParameter("@ResponseUserID", SqlDbType.Int,4),
					new SqlParameter("@ResponseUserName", SqlDbType.NVarChar,10),
					new SqlParameter("@ResponseCode", SqlDbType.NVarChar,10),
					new SqlParameter("@ResponseEmployeeName", SqlDbType.NVarChar,50),
					new SqlParameter("@ProjectID", SqlDbType.Int,4),
					new SqlParameter("@ProjectCode", SqlDbType.NVarChar,50),
					new SqlParameter("@CreateDate", SqlDbType.DateTime),
					new SqlParameter("@Description", SqlDbType.NVarChar,200),
					new SqlParameter("@Remark", SqlDbType.NVarChar,200)};
            parameters[0].Value =model.InvoiceDetailID;
            parameters[1].Value =model.PaymentID;
            parameters[2].Value =model.PaymentCode;
            parameters[3].Value =model.InvoiceID;
            parameters[4].Value =model.InvoiceNo;
            parameters[5].Value =model.Amounts;
            parameters[6].Value =model.USDDiffer;
            parameters[7].Value =model.ResponseUserID;
            parameters[8].Value =model.ResponseUserName;
            parameters[9].Value =model.ResponseCode;
            parameters[10].Value =model.ResponseEmployeeName;
            parameters[11].Value =model.ProjectID;
            parameters[12].Value =model.ProjectCode;
            parameters[13].Value =model.CreateDate;
            parameters[14].Value =model.Description;
            parameters[15].Value =model.Remark;

            return DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public int Delete(int InvoiceDetailID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete F_InvoiceDetail ");
            strSql.Append(" where InvoiceDetailID=@InvoiceDetailID ");
            SqlParameter[] parameters = {
					new SqlParameter("@InvoiceDetailID", SqlDbType.Int,4)};
            parameters[0].Value = InvoiceDetailID;

            return DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }

        public int DeleteByInvoiceId(int invoiceId,SqlTransaction trans)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete F_InvoiceDetail ");
            strSql.Append(" where invoiceId=@invoiceId ");
            SqlParameter[] parameters = {
					new SqlParameter("@invoiceId", SqlDbType.Int,4)};
            parameters[0].Value = invoiceId;

            return DbHelperSQL.ExecuteSql(strSql.ToString(),trans, parameters);
        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public ESP.Finance.Entity.InvoiceDetailInfo GetModel(int InvoiceDetailID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1  ");
            strSql.Append(@"InvoiceDetailID,PaymentID,PaymentCode,InvoiceID,InvoiceNo,
                            Amounts,USDDiffer,ResponseUserID,ResponseUserName,ResponseCode,
                            ResponseEmployeeName,ProjectID,ProjectCode,CreateDate,Description,
                            Remark");
            strSql.Append(" FROM F_InvoiceDetail ");
            strSql.Append(" where InvoiceDetailID=@InvoiceDetailID ");
            SqlParameter[] parameters = {
					new SqlParameter("@InvoiceDetailID", SqlDbType.Int,4)};
            parameters[0].Value = InvoiceDetailID;

            return CBO.FillObject<ESP.Finance.Entity.InvoiceDetailInfo>(DbHelperSQL.Query(strSql.ToString(), parameters));
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public IList<ESP.Finance.Entity.InvoiceDetailInfo> GetList(string term, List<SqlParameter> param)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ");
            strSql.Append(@"InvoiceDetailID,PaymentID,PaymentCode,InvoiceID,InvoiceNo,
                            Amounts,USDDiffer,ResponseUserID,ResponseUserName,ResponseCode,
                            ResponseEmployeeName,ProjectID,ProjectCode,CreateDate,Description,
                            Remark");
            strSql.Append(" FROM F_InvoiceDetail ");
            if (!string.IsNullOrEmpty(term))
            {
                strSql.Append(" where " + term);
            }
            return CBO.FillCollection<ESP.Finance.Entity.InvoiceDetailInfo>(DbHelperSQL.Query(strSql.ToString(), param));
        }


        #endregion  成员方法

        #region IInvoiceDetailProvider 成员


        //public decimal GetTotalAmountByInvoice(int invoiceId)
        //{
        //    return GetTotalAmountByInvoice(invoiceId,false);
        //}

        public decimal GetTotalAmountByInvoice(int invoiceId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select sum(Amounts) from F_InvoiceDetail");
            strSql.Append(" where InvoiceID=@InvoiceID ");
            SqlParameter[] parameters = {
                    new SqlParameter("@InvoiceID", SqlDbType.Int,4)};
            parameters[0].Value = invoiceId;

            object res = DbHelperSQL.GetSingle(strSql.ToString(),  parameters);
            if (res != null)
            {
                return Convert.ToDecimal(res);
            }
            return 0;
        }


        //public decimal GetTotalAmountByPayment(int PaymentID)
        //{
        //    return GetTotalAmountByPayment(PaymentID, false);
        //}

        public decimal GetTotalAmountByPayment(int PaymentID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select sum(Amounts) from F_InvoiceDetail");
            strSql.Append(" where PaymentID=@PaymentID ");
            SqlParameter[] parameters = {
                    new SqlParameter("@PaymentID", SqlDbType.Int,4)};
            parameters[0].Value = PaymentID;

            object res = DbHelperSQL.GetSingle(strSql.ToString(),  parameters);
            if (res != null)
            {
                return Convert.ToDecimal(res);
            }
            return 0;
        }

        #endregion
    }
}

