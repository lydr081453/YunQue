using System;
using System.Data;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using ESP.Finance.IDataAccess;
using ESP.Finance.Utility;
namespace ESP.Finance.DataAccess
{
    /// <summary>
    /// 数据访问类PaymentAuditHistDAL。
    /// </summary>
    internal class PaymentAuditHistDataProvider : ESP.Finance.IDataAccess.IPaymentAuditHistDataProvider
    {
        
        #region  成员方法
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int PaymentAuditID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from F_PaymentAuditHist");
            strSql.Append(" where PaymentAuditID=@PaymentAuditID ");
            SqlParameter[] parameters = {
					new SqlParameter("@PaymentAuditID", SqlDbType.Int,4)};
            parameters[0].Value = PaymentAuditID;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(ESP.Finance.Entity.PaymentAuditHistInfo model)
        {
            return Add(model, null);
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(ESP.Finance.Entity.PaymentAuditHistInfo model,SqlTransaction trans)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into F_PaymentAuditHist(");
            strSql.Append(@"PaymentID,AuditorUserID,AuditorUserName,AuditorUserCode,AuditorEmployeeName,
                            Suggestion,AuditeDate,AuditeStatus,NextAuditorID,NextAuditorUserCode,
                            NextAuditorUserName,NextAUditorEmployeeName,AuditType,Version)");

            strSql.Append(" values (");
            strSql.Append(@"@PaymentID,@AuditorUserID,@AuditorUserName,@AuditorUserCode,@AuditorEmployeeName,
                            @Suggestion,@AuditeDate,@AuditeStatus,@NextAuditorID,@NextAuditorUserCode,
                            @NextAuditorUserName,@NextAUditorEmployeeName,@AuditType,@Version)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@PaymentID", SqlDbType.Int,4),
					new SqlParameter("@AuditorUserID", SqlDbType.Int,4),
					new SqlParameter("@AuditorUserName", SqlDbType.NVarChar,50),
					new SqlParameter("@AuditorUserCode", SqlDbType.NVarChar,50),
					new SqlParameter("@AuditorEmployeeName", SqlDbType.NVarChar,50),
					new SqlParameter("@Suggestion", SqlDbType.NVarChar,200),
					new SqlParameter("@AuditeDate", SqlDbType.DateTime),
					new SqlParameter("@AuditeStatus", SqlDbType.Int,4),
					new SqlParameter("@NextAuditorID", SqlDbType.Int,4),
					new SqlParameter("@NextAuditorUserCode", SqlDbType.NVarChar,50),
					new SqlParameter("@NextAuditorUserName", SqlDbType.NVarChar,50),
					new SqlParameter("@NextAUditorEmployeeName", SqlDbType.NVarChar,50),
					new SqlParameter("@AuditType", SqlDbType.Int,4),
					new SqlParameter("@Version", SqlDbType.Int,4)};
            parameters[0].Value =model.PaymentID;
            parameters[1].Value =model.AuditorUserID;
            parameters[2].Value =model.AuditorUserName;
            parameters[3].Value =model.AuditorUserCode;
            parameters[4].Value =model.AuditorEmployeeName;
            parameters[5].Value =model.Suggestion;
            parameters[6].Value =model.AuditeDate;
            parameters[7].Value =model.AuditeStatus;
            parameters[8].Value =model.NextAuditorID;
            parameters[9].Value =model.NextAuditorUserCode;
            parameters[10].Value =model.NextAuditorUserName;
            parameters[11].Value =model.NextAUditorEmployeeName;
            parameters[12].Value =model.AuditType;
            parameters[13].Value =model.Version;

            object obj = DbHelperSQL.GetSingle(strSql.ToString(),trans,  parameters);
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
        public int Update(ESP.Finance.Entity.PaymentAuditHistInfo model)
        {
            return Update(model, null);
        }
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public int Update(ESP.Finance.Entity.PaymentAuditHistInfo model,SqlTransaction trans)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update F_PaymentAuditHist set ");
            strSql.Append("PaymentID=@PaymentID,");
            strSql.Append("AuditorUserID=@AuditorUserID,");
            strSql.Append("AuditorUserName=@AuditorUserName,");
            strSql.Append("AuditorUserCode=@AuditorUserCode,");
            strSql.Append("AuditorEmployeeName=@AuditorEmployeeName,");
            strSql.Append("Suggestion=@Suggestion,");
            strSql.Append("AuditeDate=@AuditeDate,");
            strSql.Append("AuditeStatus=@AuditeStatus,");
            strSql.Append("NextAuditorID=@NextAuditorID,");
            strSql.Append("NextAuditorUserCode=@NextAuditorUserCode,");
            strSql.Append("NextAuditorUserName=@NextAuditorUserName,");
            strSql.Append("NextAUditorEmployeeName=@NextAUditorEmployeeName,");
            strSql.Append("AuditType=@AuditType,");
            strSql.Append("Version=@Version");
            strSql.Append(" where PaymentAuditID=@PaymentAuditID  ");
            SqlParameter[] parameters = {
					new SqlParameter("@PaymentAuditID", SqlDbType.Int,4),
					new SqlParameter("@PaymentID", SqlDbType.Int,4),
					new SqlParameter("@AuditorUserID", SqlDbType.Int,4),
					new SqlParameter("@AuditorUserName", SqlDbType.NVarChar,50),
					new SqlParameter("@AuditorUserCode", SqlDbType.NVarChar,50),
					new SqlParameter("@AuditorEmployeeName", SqlDbType.NVarChar,50),
					new SqlParameter("@Suggestion", SqlDbType.NVarChar,200),
					new SqlParameter("@AuditeDate", SqlDbType.DateTime),
					new SqlParameter("@AuditeStatus", SqlDbType.Int,4),
					new SqlParameter("@NextAuditorID", SqlDbType.Int,4),
					new SqlParameter("@NextAuditorUserCode", SqlDbType.NVarChar,50),
					new SqlParameter("@NextAuditorUserName", SqlDbType.NVarChar,50),
					new SqlParameter("@NextAUditorEmployeeName", SqlDbType.NVarChar,50),
					new SqlParameter("@AuditType", SqlDbType.Int,4),
					new SqlParameter("@Version", SqlDbType.Int,4)};
            parameters[0].Value =model.PaymentAuditID;
            parameters[1].Value =model.PaymentID;
            parameters[2].Value =model.AuditorUserID;
            parameters[3].Value =model.AuditorUserName;
            parameters[4].Value =model.AuditorUserCode;
            parameters[5].Value =model.AuditorEmployeeName;
            parameters[6].Value =model.Suggestion;
            parameters[7].Value =model.AuditeDate;
            parameters[8].Value =model.AuditeStatus;
            parameters[9].Value =model.NextAuditorID;
            parameters[10].Value =model.NextAuditorUserCode;
            parameters[11].Value =model.NextAuditorUserName;
            parameters[12].Value =model.NextAUditorEmployeeName;
            parameters[13].Value =model.AuditType;
            parameters[14].Value =model.Version;

            return DbHelperSQL.ExecuteSql(strSql.ToString(), trans, parameters);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public int Delete(int PaymentAuditID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete F_PaymentAuditHist ");
            strSql.Append(" where PaymentAuditID=@PaymentAuditID ");
            SqlParameter[] parameters = {
					new SqlParameter("@PaymentAuditID", SqlDbType.Int,4)};
            parameters[0].Value = PaymentAuditID;

            return DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public ESP.Finance.Entity.PaymentAuditHistInfo GetModel(int PaymentAuditID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1  ");
            strSql.Append(@"PaymentAuditID,PaymentID,AuditorUserID,AuditorUserName,AuditorUserCode,
                            AuditorEmployeeName,Suggestion,AuditeDate,AuditeStatus,NextAuditorID,
                            NextAuditorUserCode,NextAuditorUserName,NextAUditorEmployeeName,AuditType,Version");
            strSql.Append(" FROM F_PaymentAuditHist ");
            strSql.Append(" where PaymentAuditID=@PaymentAuditID ");
            SqlParameter[] parameters = {
					new SqlParameter("@PaymentAuditID", SqlDbType.Int,4)};
            parameters[0].Value = PaymentAuditID;

            return CBO.FillObject<ESP.Finance.Entity.PaymentAuditHistInfo>(DbHelperSQL.Query(strSql.ToString(), parameters));
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public IList<ESP.Finance.Entity.PaymentAuditHistInfo> GetList(string term, List<SqlParameter> param)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ");
            strSql.Append(@"PaymentAuditID,PaymentID,AuditorUserID,AuditorUserName,AuditorUserCode,
                            AuditorEmployeeName,Suggestion,AuditeDate,AuditeStatus,NextAuditorID,
                            NextAuditorUserCode,NextAuditorUserName,NextAUditorEmployeeName,AuditType,Version");
                                        strSql.Append(" FROM F_PaymentAuditHist ");
            if (!string.IsNullOrEmpty(term))
            {
                strSql.Append(" where " + term);
            }
            return CBO.FillCollection<ESP.Finance.Entity.PaymentAuditHistInfo>(DbHelperSQL.Query(strSql.ToString(), param));
        }


        #endregion  成员方法

        #region IPaymentAuditHistProvider 成员


        //public int DeleteByPaymentId(int PaymentId)
        //{
        //    return DeleteByPaymentId(PaymentId, false);
        //}

        //public int DeleteByPaymentId(int PaymentId, string term, System.Collections.Generic.List<SqlParameter> param)
        //{
        //    return DeleteByPaymentId(PaymentId, term, param, false);
        //}

        public int DeleteByPaymentId(int PaymentId)
        {
            return DeleteByPaymentId(PaymentId, string.Empty, null,null);
        }

        public int DeleteByPaymentId(int PaymentId, string term, System.Collections.Generic.List<SqlParameter> param)
        {
            return DeleteByPaymentId(PaymentId, term, param, null);
        }

        public int DeleteByPaymentId(int PaymentId, string term, System.Collections.Generic.List<SqlParameter> param,SqlTransaction trans)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete F_PaymentAuditHist ");
            strSql.Append(" where PaymentID=@PaymentID ");
            if (!string.IsNullOrEmpty(term))
            {
                if (term.Trim().StartsWith("and"))
                {
                    strSql.Append(term);
                }
                else
                {
                    strSql.Append(" and " + term);
                }
            }
            if (param == null)
            {
                param = new List<SqlParameter>();
            }

            SqlParameter pm = new SqlParameter("@PaymentID", SqlDbType.Int, 4);
            pm.Value = PaymentId;

            param.Add(pm);

            return DbHelperSQL.ExecuteSql(strSql.ToString(), trans,  (param == null ? null : param.ToArray()));
        }

        #endregion
    }
}

