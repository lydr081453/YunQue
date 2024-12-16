using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using ESP.Finance.Utility;
using System.Collections.Generic;

namespace ESP.Finance.DataAccess
{
    /// <summary>
    /// 数据访问类F_ReturnAuditHist。
    /// </summary>
    internal class ReturnAuditHistDataProvider : ESP.Finance.IDataAccess.IReturnAuditHistDataProvider
    {
        
        #region  成员方法
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int ReturnAuditID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from F_ReturnAuditHist");
            strSql.Append(" where ReturnAuditID=@ReturnAuditID ");
            SqlParameter[] parameters = {
					new SqlParameter("@ReturnAuditID", SqlDbType.Int,4)};
            parameters[0].Value = ReturnAuditID;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(Entity.ReturnAuditHistInfo model)
        {
            return Add(model, null);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(Entity.ReturnAuditHistInfo model, SqlTransaction trans)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into F_ReturnAuditHist(");
            strSql.Append(@"ReturnID,AuditorUserID,AuditorUserName,AuditorUserCode,AuditorEmployeeName,
                            Suggestion,AuditeDate,AuditeStatus,NextAuditorID,NextAuditorUserCode,
                            NextAuditorUserName,NextAUditorEmployeeName,AuditType,Version)");

            strSql.Append(" values (");
            strSql.Append(@"@ReturnID,@AuditorUserID,@AuditorUserName,@AuditorUserCode,@AuditorEmployeeName,
                            @Suggestion,@AuditeDate,@AuditeStatus,@NextAuditorID,@NextAuditorUserCode,
                            @NextAuditorUserName,@NextAUditorEmployeeName,@AuditType,@Version)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@ReturnID", SqlDbType.Int,4),
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
            parameters[0].Value =model.ReturnID;
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

            object obj = DbHelperSQL.GetSingle(strSql.ToString(),trans, parameters);
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
        public int Update(Entity.ReturnAuditHistInfo model)
        {
            return Update(model, null);
        }
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public int Update(Entity.ReturnAuditHistInfo model,System.Data.SqlClient.SqlTransaction trans)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update F_ReturnAuditHist set ");
            strSql.Append("ReturnID=@ReturnID,");
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
            strSql.Append(" where ReturnAuditID=@ReturnAuditID  ");
            SqlParameter[] parameters = {
					new SqlParameter("@ReturnAuditID", SqlDbType.Int,4),
					new SqlParameter("@ReturnID", SqlDbType.Int,4),
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
            parameters[0].Value =model.ReturnAuditID;
            parameters[1].Value =model.ReturnID;
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

            return DbHelperSQL.ExecuteSql(strSql.ToString(),trans, parameters);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public int Delete(int ReturnAuditID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete F_ReturnAuditHist ");
            strSql.Append(" where ReturnAuditID=@ReturnAuditID ");
            SqlParameter[] parameters = {
					new SqlParameter("@ReturnAuditID", SqlDbType.Int,4)};
            parameters[0].Value = ReturnAuditID;

            return DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }

        public int DeleteByParameters(string term, List<SqlParameter> parms)
        {
            return DeleteByParameters(term, parms, null);
        }

        public int DeleteByParameters(string term,List<SqlParameter> parms,SqlTransaction trans)
        {
            if (string.IsNullOrEmpty(term))
                return 0;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete F_ReturnAuditHist ");
            strSql.Append(" where "+term);
            return DbHelperSQL.ExecuteSql(strSql.ToString(),trans, parms.ToArray());
        }

        /// <summary>
        /// 删除未审核的采购部审核信息
        /// </summary>
        /// <param name="returnId"></param>
        /// <returns></returns>
        public int DeleteNotAudit(int returnId,SqlTransaction trans)
        {
            string strSql = "delete F_ReturnAuditHist where returnId="+returnId;
            strSql += " and auditType in (" + auditorType.purchase_first + "," + auditorType.purchase_major2 + ")";
            strSql += " and auditeStatus=" + (int)AuditHistoryStatus.UnAuditing;
            return DbHelperSQL.ExecuteSql(strSql.ToString(),trans);
        }

        public int DeleteNotAudit(int returnId)
        {
            return DeleteNotAudit(returnId, null);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Entity.ReturnAuditHistInfo GetModel(int ReturnAuditID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"select  top 1 ReturnAuditID,NextAuditorUserCode,NextAuditorUserName,
                            NextAUditorEmployeeName,AuditorUserID,AuditorUserName,AuditorUserCode,
                            AuditorEmployeeName,Suggestion,AuditeDate,AuditeStatus,NextAuditorID,ReturnID,AuditType,Version 
                            from F_ReturnAuditHist ");
            strSql.Append(" where ReturnAuditID=@ReturnAuditID ");
            SqlParameter[] parameters = {
					new SqlParameter("@ReturnAuditID", SqlDbType.Int,4)};
            parameters[0].Value = ReturnAuditID;

            return CBO.FillObject<Entity.ReturnAuditHistInfo>(DbHelperSQL.Query(strSql.ToString(), parameters));

        }

        public IList<Entity.ReturnAuditHistInfo> GetList(string term, List<SqlParameter> param)
        {
            return GetList(term, param, null);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public IList<Entity.ReturnAuditHistInfo> GetList(string term, List<SqlParameter> param,SqlTransaction trans)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"select ReturnAuditID,NextAuditorUserCode,NextAuditorUserName,
                            NextAUditorEmployeeName,AuditorUserID,AuditorUserName,AuditorUserCode,
                            AuditorEmployeeName,Suggestion,AuditeDate,AuditeStatus,NextAuditorID,ReturnID,AuditType,Version ");
            strSql.Append(" FROM F_ReturnAuditHist ");
            if (!string.IsNullOrEmpty(term))
            {
                strSql.Append(" where " + term);
            }
            return CBO.FillCollection<Entity.ReturnAuditHistInfo>(DbHelperSQL.Query(strSql.ToString(),trans,(param == null ? null : param.ToArray())));
        }

        public int DeleteByReturnID(int ReturnID)
        {
            return DeleteByReturnID(ReturnID, string.Empty, null,null);
        }

        public int DeleteByReturnID(int ReturnID, string term, List<SqlParameter> param)
        {
            return DeleteByReturnID(ReturnID, term,null,param);
        }

        public int DeleteByReturnID(int ReturnID, string term,SqlTransaction trans, List<SqlParameter> param)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete F_ReturnAuditHist ");
            strSql.Append(" where ReturnID=@ReturnID ");
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

            SqlParameter pm = new SqlParameter("@ReturnID", SqlDbType.Int, 4);
            pm.Value = ReturnID;

            param.Add(pm);

            return DbHelperSQL.ExecuteSql(strSql.ToString(), trans, param.ToArray());
        }


        #endregion  成员方法
    }
}

