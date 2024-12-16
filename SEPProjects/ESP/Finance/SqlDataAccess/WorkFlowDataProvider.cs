using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using ESP.Finance.Utility;
using System.Collections.Generic;

namespace ESP.Finance.DataAccess
{
    /// <summary>
    /// 数据访问类RefundAuditHist。
    /// </summary>
    internal class WorkFlowDataProvider : ESP.Finance.IDataAccess.IWorkFlowDataProvider
    {

        #region  成员方法
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from F_WorkFlow");
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)};
            parameters[0].Value = ID;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(Entity.WorkFlowInfo model)
        {
            return Add(model, null);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(Entity.WorkFlowInfo model, SqlTransaction trans)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into F_WorkFlow(");
            strSql.Append(@"ModelId,AuditorUserID,AuditorUserName,AuditorUserCode,AuditorEmployeeName,Suggestion,AuditDate,AuditStatus,AuditType,ModelType)");

            strSql.Append(" values (");
            strSql.Append(@"@ModelId,@AuditorUserID,@AuditorUserName,@AuditorUserCode,@AuditorEmployeeName,@Suggestion,@AuditDate,@AuditStatus,@AuditType,@ModelType)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@ModelId", SqlDbType.Int,4),
					new SqlParameter("@AuditorUserID", SqlDbType.Int,4),
					new SqlParameter("@AuditorUserName", SqlDbType.NVarChar,50),
					new SqlParameter("@AuditorUserCode", SqlDbType.NVarChar,50),
					new SqlParameter("@AuditorEmployeeName", SqlDbType.NVarChar,50),
					new SqlParameter("@Suggestion", SqlDbType.NVarChar,200),
					new SqlParameter("@AuditDate", SqlDbType.DateTime),
					new SqlParameter("@AuditStatus", SqlDbType.Int,4),
				    new SqlParameter("@AuditType", SqlDbType.Int,4),
                    new SqlParameter("@ModelType", SqlDbType.Int,4)
                                        };

            parameters[0].Value = model.ModelId;
            parameters[1].Value = model.AuditorUserID;
            parameters[2].Value = model.AuditorUserName;
            parameters[3].Value = model.AuditorUserCode;
            parameters[4].Value = model.AuditorEmployeeName;
            parameters[5].Value = model.Suggestion;
            parameters[6].Value = model.AuditDate;
            parameters[7].Value = model.AuditStatus;
            parameters[8].Value = model.AuditType;
            parameters[9].Value = model.ModelType;

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
        /// 更新一条数据
        /// </summary>
        public int Update(Entity.WorkFlowInfo model)
        {
            return Update(model, null);
        }
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public int Update(Entity.WorkFlowInfo model, System.Data.SqlClient.SqlTransaction trans)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update F_WorkFlow set ModelId=@ModelId,AuditorUserID=@AuditorUserID,AuditorUserName=@AuditorUserName,");
            strSql.Append("AuditorUserCode=@AuditorUserCode,AuditorEmployeeName=@AuditorEmployeeName,Suggestion=@Suggestion,");
            strSql.Append("AuditDate=@AuditDate,AuditStatus=@AuditStatus,AuditType=@AuditType,ModelType=@ModelType");
            strSql.Append(" where ID=@ID  ");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4),
					new SqlParameter("@ModelId", SqlDbType.Int,4),
					new SqlParameter("@AuditorUserID", SqlDbType.Int,4),
					new SqlParameter("@AuditorUserName", SqlDbType.NVarChar,50),
					new SqlParameter("@AuditorUserCode", SqlDbType.NVarChar,50),
					new SqlParameter("@AuditorEmployeeName", SqlDbType.NVarChar,50),
					new SqlParameter("@Suggestion", SqlDbType.NVarChar,200),
					new SqlParameter("@AuditDate", SqlDbType.DateTime),
					new SqlParameter("@AuditStatus", SqlDbType.Int,4),
					new SqlParameter("@AuditType", SqlDbType.Int,4),
                    new SqlParameter("@ModelType", SqlDbType.Int,4)                    
                                        };
            parameters[0].Value = model.Id;
            parameters[1].Value = model.ModelId;
            parameters[2].Value = model.AuditorUserID;
            parameters[3].Value = model.AuditorUserName;
            parameters[4].Value = model.AuditorUserCode;
            parameters[5].Value = model.AuditorEmployeeName;
            parameters[6].Value = model.Suggestion;
            parameters[7].Value = model.AuditDate;
            parameters[8].Value = model.AuditStatus;
            parameters[9].Value = model.AuditType;
            parameters[10].Value = model.ModelType;
            return DbHelperSQL.ExecuteSql(strSql.ToString(), trans, parameters);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public int Delete(int ID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete F_WorkFlow ");
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)};
            parameters[0].Value = ID;

            return DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }

        public int DeleteByParameters(string term, List<SqlParameter> parms)
        {
            return DeleteByParameters(term, parms, null);
        }

        public int DeleteByParameters(string term, List<SqlParameter> parms, SqlTransaction trans)
        {
            if (string.IsNullOrEmpty(term))
                return 0;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete F_WorkFlow ");
            strSql.Append(" where " + term);
            return DbHelperSQL.ExecuteSql(strSql.ToString(), trans, parms.ToArray());
        }

        /// <summary>
        /// 删除未审核的采购部审核信息
        /// </summary>
        /// <param name="ModelId"></param>
        /// <returns></returns>
        public int DeleteNotAudit(int ModelId,int ModelType, SqlTransaction trans)
        {
            string strSql = "delete F_WorkFlow where ModelId=" + ModelId;
            strSql += " and ModelType=" + ModelType + " and auditType in (" + auditorType.purchase_first + "," + auditorType.purchase_major2 + ")";
            strSql += " and auditStatus=" + (int)AuditHistoryStatus.UnAuditing;
            return DbHelperSQL.ExecuteSql(strSql.ToString(), trans);
        }

        public int DeleteNotAudit(int ModelId,int ModelType)
        {
            return DeleteNotAudit(ModelId,ModelType, null);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Entity.WorkFlowInfo GetModel(int ID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"select  top 1 * from F_WorkFlow ");
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)};
            parameters[0].Value = ID;

            return CBO.FillObject<Entity.WorkFlowInfo>(DbHelperSQL.Query(strSql.ToString(), parameters));

        }

        public IList<Entity.WorkFlowInfo> GetList(string term, List<SqlParameter> param)
        {
            return GetList(term, param, null);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public IList<Entity.WorkFlowInfo> GetList(string term, List<SqlParameter> param, SqlTransaction trans)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"select * FROM F_WorkFlow ");
            if (!string.IsNullOrEmpty(term))
            {
                strSql.Append(" where " + term);
            }
            return CBO.FillCollection<Entity.WorkFlowInfo>(DbHelperSQL.Query(strSql.ToString(), trans, (param == null ? null : param.ToArray())));
        }

        public int DeleteByModelId(int ModelId,int ModelType)
        {
            return DeleteByModelId(ModelId, ModelType,string.Empty, null, null);
        }

        public int DeleteByModelId(int ModelId,int ModelType, string term, List<SqlParameter> param)
        {
            return DeleteByModelId(ModelId,ModelType, term, null, param);
        }

        public int DeleteByModelId(int ModelId,int ModelType, string term, SqlTransaction trans, List<SqlParameter> param)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete F_WorkFlow ");
            strSql.Append(" where ModelId=@ModelId and ModelType=@ModelType");
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

            SqlParameter pm = new SqlParameter("@ModelId", SqlDbType.Int, 4);
            pm.Value = ModelId;
            param.Add(pm);

            SqlParameter pm2 = new SqlParameter("@ModelType", SqlDbType.Int, 4);
            pm2.Value = ModelType;
            param.Add(pm2);

            return DbHelperSQL.ExecuteSql(strSql.ToString(), trans, param.ToArray());
        }



        #endregion  成员方法
  
    }
}
