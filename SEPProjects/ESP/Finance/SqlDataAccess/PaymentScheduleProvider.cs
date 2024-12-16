using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using ESP.Finance.Entity;
using System.Collections.Generic;
using ESP.Finance.Utility;
namespace ESP.Finance.DataAccess
{
    /// <summary>
    /// 数据访问类PaymentDAL。
    /// </summary>
    internal class PaymentScheduleProvider : ESP.Finance.IDataAccess.IPaymentScheduleProvider
    {

        #region  成员方法
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int PaymentID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(*) from F_PaymentSchedule");
            strSql.Append(" where Id=@Id ");
            SqlParameter[] parameters = {
					new SqlParameter("@Id", SqlDbType.Int,4)};
            parameters[0].Value = PaymentID;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(Entity.PaymentScheduleInfo model)
        {
            return Add(model, null);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(Entity.PaymentScheduleInfo model, SqlTransaction trans)
        {



            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into F_PaymentSchedule(");
            strSql.Append(@"PaymentID,PaymentPreDate,PaymentFactDate,PaymentContent,Status,
                            PaymentBudget,PaymentFee,InvoiceNo,InvoiceDate,Remark,
                            BankID,PaymentTypeID,PaymentTypeName,PaymentTypeCode,CreditCode,USDDiffer,OperationType,CreateDate)");

            strSql.Append(" values (");
            strSql.Append(@"@PaymentID,@PaymentPreDate,@PaymentFactDate,@PaymentContent,@Status,
                            @PaymentBudget,@PaymentFee,@InvoiceNo,@InvoiceDate,@Remark,
                            @BankID,@PaymentTypeID,@PaymentTypeName,@PaymentTypeCode,@CreditCode,@USDDiffer,@OperationType,@CreateDate)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@PaymentID", SqlDbType.Int,4),
					new SqlParameter("@PaymentPreDate", SqlDbType.DateTime),
					new SqlParameter("@PaymentFactDate", SqlDbType.DateTime),
					new SqlParameter("@PaymentContent", SqlDbType.NVarChar,500),
					new SqlParameter("@Status", SqlDbType.Int,4),
					new SqlParameter("@PaymentBudget", SqlDbType.Decimal,9),
					new SqlParameter("@PaymentFee", SqlDbType.Decimal,9),
					new SqlParameter("@InvoiceNo", SqlDbType.NChar,10),
					new SqlParameter("@InvoiceDate", SqlDbType.DateTime),
					new SqlParameter("@Remark", SqlDbType.NVarChar,500),
					new SqlParameter("@BankID", SqlDbType.Int,4),
					new SqlParameter("@PaymentTypeID", SqlDbType.Int,4),
					new SqlParameter("@PaymentTypeName", SqlDbType.NVarChar,50),
					new SqlParameter("@PaymentTypeCode", SqlDbType.NVarChar,50),
                    new SqlParameter("@CreditCode",SqlDbType.NVarChar,25),
                    new SqlParameter("@USDDiffer",SqlDbType.Decimal,9),
					new SqlParameter("@OperationType", SqlDbType.Int,4),
                    new SqlParameter("@CreateDate", SqlDbType.DateTime)
                                        };

            parameters[0].Value = model.PaymentID;
            parameters[1].Value = model.PaymentFactDate;
            parameters[2].Value = model.PaymentContent;
            parameters[3].Value = model.PaymentBudget;
            parameters[4].Value = model.PaymentFee;
            parameters[5].Value = model.InvoiceNo;
            parameters[6].Value = model.InvoiceDate;
            parameters[7].Value = model.Remark;
            parameters[8].Value = model.BankID;
            parameters[9].Value = model.PaymentTypeID;
            parameters[10].Value = model.PaymentTypeName;
            parameters[11].Value = model.PaymentTypeCode;
            parameters[12].Value = model.CreditCode;
            parameters[13].Value = model.USDDiffer;
            parameters[14].Value = model.OperationType;
            parameters[15].Value = model.CreateDate;

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


        ///// <summary>
        ///// 更新一条数据
        ///// </summary>
        //public int Update(ESP.Finance.Entity.PaymentScheduleInfo model)
        //{
        //    return Update(model, false);
        //}
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public int Update(ESP.Finance.Entity.PaymentScheduleInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update F_PaymentSchedule set ");
            strSql.Append("PaymentID=@PaymentID,");
            strSql.Append("PaymentPreDate=@PaymentPreDate,");
            strSql.Append("PaymentFactDate=@PaymentFactDate,");
            strSql.Append("PaymentContent=@PaymentContent,");
            strSql.Append("Status=@Status,");
            strSql.Append("PaymentBudget=@PaymentBudget,");
            strSql.Append("PaymentFee=@PaymentFee,");
            strSql.Append("InvoiceNo=@InvoiceNo,");
            strSql.Append("InvoiceDate=@InvoiceDate,");
            strSql.Append("Remark=@Remark,");
            strSql.Append("BankID=@BankID,");
            strSql.Append("PaymentTypeID=@PaymentTypeID,");
            strSql.Append("PaymentTypeName=@PaymentTypeName,");
            strSql.Append("PaymentTypeCode=@PaymentTypeCode,");
            strSql.Append("CreditCode=@CreditCode, ");
            strSql.Append("USDDiffer=@USDDiffer, ");
            strSql.Append("OperationType=@OperationType, ");
            strSql.Append("CreateDate=@CreateDate ");
            
            strSql.Append(" where Id=@Id ");
            SqlParameter[] parameters = {
					new SqlParameter("@PaymentID", SqlDbType.Int,4),
					new SqlParameter("@PaymentPreDate", SqlDbType.DateTime),
					new SqlParameter("@PaymentFactDate", SqlDbType.DateTime),
					new SqlParameter("@PaymentContent", SqlDbType.NVarChar,500),
					new SqlParameter("@Status", SqlDbType.Int,4),
					new SqlParameter("@PaymentBudget", SqlDbType.Decimal,9),
					new SqlParameter("@PaymentFee", SqlDbType.Decimal,9),
					new SqlParameter("@InvoiceNo", SqlDbType.NChar,10),
					new SqlParameter("@InvoiceDate", SqlDbType.DateTime),
					new SqlParameter("@Remark", SqlDbType.NVarChar,500),
					new SqlParameter("@BankID", SqlDbType.Int,4),
					new SqlParameter("@PaymentTypeID", SqlDbType.Int,4),
					new SqlParameter("@PaymentTypeName", SqlDbType.NVarChar,50),
					new SqlParameter("@PaymentTypeCode", SqlDbType.NVarChar,50),
                    new SqlParameter("@CreditCode",SqlDbType.NVarChar,25),
                    new SqlParameter("@USDDiffer",SqlDbType.Decimal,9),
                    new SqlParameter("@OperationType",SqlDbType.Int,4),
                    new SqlParameter("@CreateDate",SqlDbType.DateTime)
                                        };
            parameters[0].Value = model.PaymentID;
            parameters[1].Value = model.PaymentPreDate;
            parameters[2].Value = model.PaymentFactDate;
            parameters[3].Value = model.PaymentContent;
            parameters[4].Value = model.Status;
            parameters[5].Value = model.PaymentBudget;
            parameters[6].Value = model.PaymentFee;
            parameters[7].Value = model.InvoiceNo;
            parameters[8].Value = model.InvoiceDate;
            parameters[9].Value = model.Remark;
            parameters[10].Value = model.BankID;
            parameters[11].Value = model.PaymentTypeID;
            parameters[12].Value = model.PaymentTypeName;
            parameters[13].Value = model.PaymentTypeCode;
            parameters[14].Value = model.CreditCode;
            parameters[15].Value = model.USDDiffer;
            parameters[16].Value = model.OperationType;
            parameters[17].Value = model.CreateDate;

            return DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public int Delete(int Id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete F_PaymentSchedule ");
            strSql.Append(" where Id=@Id ");
            SqlParameter[] parameters = {
					new SqlParameter("@Id", SqlDbType.Int,4)};
            parameters[0].Value = Id;

            return DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public ESP.Finance.Entity.PaymentScheduleInfo GetModel(int Id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append(" FROM F_PaymentSchedule ");
            strSql.Append(" where Id=@Id ");
            SqlParameter[] parameters = {
					new SqlParameter("@Id", SqlDbType.Int,4)};
            parameters[0].Value = Id;

            return CBO.FillObject<ESP.Finance.Entity.PaymentScheduleInfo>(DbHelperSQL.Query(strSql.ToString(), parameters));
        }

        public IList<ESP.Finance.Entity.PaymentScheduleInfo> GetList(string term, List<SqlParameter> param)
        {
            return GetList(term, param, null);
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public IList<ESP.Finance.Entity.PaymentScheduleInfo> GetList(string term, List<SqlParameter> param, SqlTransaction trans)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append(" FROM F_PaymentSchedule ");
            if (!string.IsNullOrEmpty(term))
            {
                strSql.Append(" where " + term);
            }
            return CBO.FillCollection<ESP.Finance.Entity.PaymentScheduleInfo>(DbHelperSQL.Query(strSql.ToString(), trans, (param == null ? null : param.ToArray())));
        }

     
        #endregion  成员方法

    }
}

