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
	/// 数据访问类ExpenseDAL。
	/// </summary>
    internal class ExpenseAuditDetailDataProvider : ESP.Finance.IDataAccess.IExpenseAuditDetailProvider
	{

        #region  成员方法

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(ESP.Finance.Entity.ExpenseAuditDetailInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into F_ExpenseAuditDetail(");
            strSql.Append("ExpenseAuditID,AuditorUserID,AuditorUserName,AuditorUserCode,AuditorEmployeeName,Suggestion,AuditeDate,AuditeStatus,AuditType)");
            strSql.Append(" values (");
            strSql.Append("@ExpenseAuditID,@AuditorUserID,@AuditorUserName,@AuditorUserCode,@AuditorEmployeeName,@Suggestion,@AuditeDate,@AuditeStatus,@AuditType)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@ExpenseAuditID", SqlDbType.Int,4),
					new SqlParameter("@AuditorUserID", SqlDbType.Int,4),
					new SqlParameter("@AuditorUserName", SqlDbType.NVarChar,50),
					new SqlParameter("@AuditorUserCode", SqlDbType.NVarChar,50),
					new SqlParameter("@AuditorEmployeeName", SqlDbType.NVarChar,50),
					new SqlParameter("@Suggestion", SqlDbType.NVarChar,200),
					new SqlParameter("@AuditeDate", SqlDbType.DateTime),
					new SqlParameter("@AuditeStatus", SqlDbType.Int,4),
					new SqlParameter("@AuditType", SqlDbType.Int,4)};
            parameters[0].Value = model.ExpenseAuditID;
            parameters[1].Value = model.AuditorUserID;
            parameters[2].Value = model.AuditorUserName;
            parameters[3].Value = model.AuditorUserCode;
            parameters[4].Value = model.AuditorEmployeeName;
            parameters[5].Value = model.Suggestion;
            parameters[6].Value = model.AuditeDate;
            parameters[7].Value = model.AuditeStatus;
            parameters[8].Value = model.AuditType;

            object obj = DbHelperSQL.GetSingle(strSql.ToString(), parameters);
            if (obj == null)
            {
                return 1;
            }
            else
            {
                return Convert.ToInt32(obj);
            }
        }

        public int Add(ESP.Finance.Entity.ExpenseAuditDetailInfo model,SqlTransaction trans)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into F_ExpenseAuditDetail(");
            strSql.Append("ExpenseAuditID,AuditorUserID,AuditorUserName,AuditorUserCode,AuditorEmployeeName,Suggestion,AuditeDate,AuditeStatus,AuditType)");
            strSql.Append(" values (");
            strSql.Append("@ExpenseAuditID,@AuditorUserID,@AuditorUserName,@AuditorUserCode,@AuditorEmployeeName,@Suggestion,@AuditeDate,@AuditeStatus,@AuditType)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@ExpenseAuditID", SqlDbType.Int,4),
					new SqlParameter("@AuditorUserID", SqlDbType.Int,4),
					new SqlParameter("@AuditorUserName", SqlDbType.NVarChar,50),
					new SqlParameter("@AuditorUserCode", SqlDbType.NVarChar,50),
					new SqlParameter("@AuditorEmployeeName", SqlDbType.NVarChar,50),
					new SqlParameter("@Suggestion", SqlDbType.NVarChar,200),
					new SqlParameter("@AuditeDate", SqlDbType.DateTime),
					new SqlParameter("@AuditeStatus", SqlDbType.Int,4),
					new SqlParameter("@AuditType", SqlDbType.Int,4)};
            parameters[0].Value = model.ExpenseAuditID;
            parameters[1].Value = model.AuditorUserID;
            parameters[2].Value = model.AuditorUserName;
            parameters[3].Value = model.AuditorUserCode;
            parameters[4].Value = model.AuditorEmployeeName;
            parameters[5].Value = model.Suggestion;
            parameters[6].Value = model.AuditeDate;
            parameters[7].Value = model.AuditeStatus;
            parameters[8].Value = model.AuditType;

            object obj = DbHelperSQL.GetSingle(strSql.ToString(),trans, parameters);
            if (obj == null)
            {
                return 1;
            }
            else
            {
                return Convert.ToInt32(obj);
            }
        }
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public int Update(ESP.Finance.Entity.ExpenseAuditDetailInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update F_ExpenseAuditDetail set ");
            strSql.Append("ExpenseAuditID=@ExpenseAuditID,");
            strSql.Append("AuditorUserID=@AuditorUserID,");
            strSql.Append("AuditorUserName=@AuditorUserName,");
            strSql.Append("AuditorUserCode=@AuditorUserCode,");
            strSql.Append("AuditorEmployeeName=@AuditorEmployeeName,");
            strSql.Append("Suggestion=@Suggestion,");
            strSql.Append("AuditeDate=@AuditeDate,");
            strSql.Append("AuditeStatus=@AuditeStatus,");
            strSql.Append("AuditType=@AuditType");
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4),
					new SqlParameter("@ExpenseAuditID", SqlDbType.Int,4),
					new SqlParameter("@AuditorUserID", SqlDbType.Int,4),
					new SqlParameter("@AuditorUserName", SqlDbType.NVarChar,50),
					new SqlParameter("@AuditorUserCode", SqlDbType.NVarChar,50),
					new SqlParameter("@AuditorEmployeeName", SqlDbType.NVarChar,50),
					new SqlParameter("@Suggestion", SqlDbType.NVarChar,200),
					new SqlParameter("@AuditeDate", SqlDbType.DateTime),
					new SqlParameter("@AuditeStatus", SqlDbType.Int,4),
					new SqlParameter("@AuditType", SqlDbType.Int,4)};
            parameters[0].Value = model.ID;
            parameters[1].Value = model.ExpenseAuditID;
            parameters[2].Value = model.AuditorUserID;
            parameters[3].Value = model.AuditorUserName;
            parameters[4].Value = model.AuditorUserCode;
            parameters[5].Value = model.AuditorEmployeeName;
            parameters[6].Value = model.Suggestion;
            parameters[7].Value = model.AuditeDate;
            parameters[8].Value = model.AuditeStatus;
            parameters[9].Value = model.AuditType;

            return DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public int Delete(int ID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete F_ExpenseAuditDetail ");
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)};
            parameters[0].Value = ID;

            return DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public ESP.Finance.Entity.ExpenseAuditDetailInfo GetModel(int ID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 ID,ExpenseAuditID,AuditorUserID,AuditorUserName,AuditorUserCode,AuditorEmployeeName,Suggestion,AuditeDate,AuditeStatus,AuditType from F_ExpenseAuditDetail ");
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)};
            parameters[0].Value = ID;

            return CBO.FillObject<ExpenseAuditDetailInfo>(DbHelperSQL.Query(strSql.ToString(), parameters));
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<ExpenseAuditDetailInfo> GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ID,ExpenseAuditID,AuditorUserID,AuditorUserName,AuditorUserCode,AuditorEmployeeName,Suggestion,AuditeDate,AuditeStatus,AuditType ");
            strSql.Append(" FROM F_ExpenseAuditDetail ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where 1=1 " + strWhere);
            }
            strSql.Append(" order by ID");
            return CBO.FillCollection<ExpenseAuditDetailInfo>(DbHelperSQL.Query(strSql.ToString()));
        }

        public System.Data.DataTable GetWorkflow(int id,int level)
        {
            string strWhere = string.Empty;
            if (level == 1)
                strWhere = " where (auditType<11 or auditType=17)";
            else if (level == 2)
                strWhere = " where (auditType>=11 and auditType!=17)";

            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"select distinct * from (
                            select a.*,b.Suggestion,b.AuditeStatus,b.AuditeDate from F_ExpenseAuditerList a 
                            left join F_ExpenseAuditDetail b on a.ReturnID=b.ExpenseAuditID and a.Auditer=b.AuditorUserID where a.ReturnID="+ id.ToString()+
                            @"union
                            select id,ExpenseAuditID as ReturnID,AuditorUserID as Auditer,AuditorEmployeeName as AuditerName,AuditType,Suggestion,AuditeStatus,AuditeDate 
                            from F_ExpenseAuditDetail where AuditType<>0 and AuditorUserID not in(select auditer from F_ExpenseAuditerList where ReturnID =" + id.ToString()+" ) and ExpenseAuditID="+ id.ToString()+") wf ");

            if (!string.IsNullOrEmpty(strWhere))
                strSql.Append(strWhere);
            strSql.Append(" order by id");

            return DbHelperSQL.Query(strSql.ToString()).Tables[0];
        }

        public int DeleteByReturnID(int returnId, SqlTransaction trans)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete F_ExpenseAuditDetail ");
            strSql.Append(" where ExpenseAuditID=@ExpenseAuditID ");
            SqlParameter[] parameters = {
					new SqlParameter("@ExpenseAuditID", SqlDbType.Int,4)};
            parameters[0].Value = returnId;

            return DbHelperSQL.ExecuteSql(strSql.ToString(),trans, parameters);
        }
        #endregion  成员方法
        
	}
}

