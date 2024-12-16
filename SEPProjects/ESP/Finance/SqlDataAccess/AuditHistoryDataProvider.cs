﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using ESP.Finance.Utility;

namespace ESP.Finance.DataAccess
{
    internal class AuditHistoryDataProvider : ESP.Finance.IDataAccess.IAuditHistoryDataProvider
    {
        
		#region  成员方法

        //public int Add(Entity.AuditHistoryInfo model)
        //{
        //    return Add(model, false);
        //}

        public int Add(Entity.AuditHistoryInfo model)
        {
            return Add(model, null);
        }

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(Entity.AuditHistoryInfo model,SqlTransaction trans)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into F_AuditHistory(");
            strSql.Append("AuditStatus,ProjectID,AuditorUserID,AuditorUserName,AuditorUserCode,AuditorEmployeeName,Suggestion,SquenceLevel,TotalLevel,AuditDate,AuditType,Version,FormType)");
			strSql.Append(" values (");
            strSql.Append("@AuditStatus,@ProjectID,@AuditorUserID,@AuditorUserName,@AuditorUserCode,@AuditorEmployeeName,@Suggestion,@SquenceLevel,@TotalLevel,@AuditDate,@AuditType,@Version,@FormType)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = {
					new SqlParameter("@AuditStatus", SqlDbType.Int,4),
					new SqlParameter("@ProjectID", SqlDbType.Int,4),
					new SqlParameter("@AuditorUserID", SqlDbType.Int,4),
					new SqlParameter("@AuditorUserName", SqlDbType.NVarChar,50),
					new SqlParameter("@AuditorUserCode", SqlDbType.NVarChar,50),
					new SqlParameter("@AuditorEmployeeName", SqlDbType.NVarChar,50),
					new SqlParameter("@Suggestion", SqlDbType.NVarChar,4000),
					new SqlParameter("@SquenceLevel", SqlDbType.Int,4),
					new SqlParameter("@TotalLevel", SqlDbType.Int,4),
                    new SqlParameter("@AuditDate",SqlDbType.DateTime,8),
                    new SqlParameter("@AuditType", SqlDbType.Int,4),
                    new SqlParameter("@Version", SqlDbType.Int,4),
                    new SqlParameter("@FormType", SqlDbType.Int,4)
                                        };
			parameters[0].Value =model.AuditStatus;
			parameters[1].Value =model.ProjectID;
			parameters[2].Value =model.AuditorUserID;
			parameters[3].Value =model.AuditorUserName;
			parameters[4].Value =model.AuditorUserCode;
			parameters[5].Value =model.AuditorEmployeeName;
			parameters[6].Value =model.Suggestion;
			parameters[7].Value =model.SquenceLevel;
			parameters[8].Value =model.TotalLevel;
            parameters[9].Value =model.AuditDate;
            parameters[10].Value =model.AuditType;
            parameters[11].Value =model.Version;
            parameters[12].Value = model.FormType;
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
        public int Update(Entity.AuditHistoryInfo model)
        {
            return Update(model, null);
        }

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public int Update(Entity.AuditHistoryInfo model,SqlTransaction trans)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update F_AuditHistory set ");
			strSql.Append("AuditStatus=@AuditStatus,");
			strSql.Append("ProjectID=@ProjectID,");
			strSql.Append("AuditorUserID=@AuditorUserID,");
			strSql.Append("AuditorUserName=@AuditorUserName,");
			strSql.Append("AuditorUserCode=@AuditorUserCode,");
			strSql.Append("AuditorEmployeeName=@AuditorEmployeeName,");
			strSql.Append("Suggestion=@Suggestion,");
			strSql.Append("SquenceLevel=@SquenceLevel,");
			strSql.Append("TotalLevel=@TotalLevel,");
            strSql.Append("AuditDate=@AuditDate, ");
            strSql.Append("AuditType=@AuditType, ");
            strSql.Append("FormType=@FormType, ");
            strSql.Append("Version=@Version ");
			strSql.Append(" where AuditID=@AuditID ");
			SqlParameter[] parameters = {
					new SqlParameter("@AuditID", SqlDbType.Int,4),
					new SqlParameter("@AuditStatus", SqlDbType.Int,4),
					new SqlParameter("@ProjectID", SqlDbType.Int,4),
					new SqlParameter("@AuditorUserID", SqlDbType.Int,4),
					new SqlParameter("@AuditorUserName", SqlDbType.NVarChar,50),
					new SqlParameter("@AuditorUserCode", SqlDbType.NVarChar,50),
					new SqlParameter("@AuditorEmployeeName", SqlDbType.NVarChar,50),
					new SqlParameter("@Suggestion", SqlDbType.NVarChar,200),
					new SqlParameter("@SquenceLevel", SqlDbType.Int,4),
					new SqlParameter("@TotalLevel", SqlDbType.Int,4),
                    new SqlParameter("@AuditDate",SqlDbType.DateTime,8),  
                    new SqlParameter("@AuditType", SqlDbType.Int,4),
                    new SqlParameter("@Version", SqlDbType.Int,4),
                    new SqlParameter("@FormType", SqlDbType.Int,4)
                                        };
			parameters[0].Value =model.AuditID;
			parameters[1].Value =model.AuditStatus;
			parameters[2].Value =model.ProjectID;
			parameters[3].Value =model.AuditorUserID;
			parameters[4].Value =model.AuditorUserName;
			parameters[5].Value =model.AuditorUserCode;
			parameters[6].Value =model.AuditorEmployeeName;
			parameters[7].Value =model.Suggestion;
			parameters[8].Value =model.SquenceLevel;
			parameters[9].Value =model.TotalLevel;
            parameters[10].Value =model.AuditDate;
            parameters[11].Value =model.AuditType;
            parameters[12].Value =model.Version;
            parameters[13].Value = model.FormType;

			return DbHelperSQL.ExecuteSql(strSql.ToString(),trans,parameters);
		}


        public int Delete(int AuditID)
        {
            return Delete(AuditID, null);
        }
		/// <summary>
		/// 删除一条数据
		/// </summary>
		public int Delete(int AuditID,SqlTransaction trans)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete F_AuditHistory ");
			strSql.Append(" where AuditID=@AuditID ");
			SqlParameter[] parameters = {
					new SqlParameter("@AuditID", SqlDbType.Int,4)};
			parameters[0].Value = AuditID;

			return DbHelperSQL.ExecuteSql(strSql.ToString(),trans,parameters);
		}


		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public Entity.AuditHistoryInfo GetModel(int AuditID)
		{
			
			StringBuilder strSql=new StringBuilder();
            strSql.Append(@"select  top 1 AuditID,AuditStatus,ProjectID,AuditorUserID,
                            AuditorUserName,AuditorUserCode,AuditorEmployeeName,
                            Suggestion,SquenceLevel,TotalLevel,AuditDate,AuditType,Version,FormType
                            from F_AuditHistory ");
			strSql.Append(" where AuditID=@AuditID ");
			SqlParameter[] parameters = {
					new SqlParameter("@AuditID", SqlDbType.Int,4)};
			parameters[0].Value = AuditID;

            return CBO.FillObject<Entity.AuditHistoryInfo>(DbHelperSQL.Query(strSql.ToString(), parameters));

        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public IList<Entity.AuditHistoryInfo> GetList(string term, List<SqlParameter> param)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"select AuditID,AuditStatus,ProjectID,AuditorUserID,
                                    AuditorUserName,AuditorUserCode,AuditorEmployeeName,
                                    Suggestion,SquenceLevel,TotalLevel,AuditDate,AuditType,Version,FormType ");
            strSql.Append(" FROM F_AuditHistory ");
            if (!string.IsNullOrEmpty(term))
            {
                strSql.Append(" where " + term);
            }
            return CBO.FillCollection<Entity.AuditHistoryInfo>(DbHelperSQL.Query(strSql.ToString(),param));
        }

        public IList<Entity.AuditHistoryInfo> GetList(string term, SqlTransaction trans, params SqlParameter[] param)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"select AuditID,AuditStatus,ProjectID,AuditorUserID,
                                    AuditorUserName,AuditorUserCode,AuditorEmployeeName,
                                    Suggestion,SquenceLevel,TotalLevel,AuditDate,AuditType,Version,FormType ");
            strSql.Append(" FROM F_AuditHistory ");
            if (!string.IsNullOrEmpty(term))
            {
                strSql.Append(" where " + term);
            }
            return CBO.FillCollection<Entity.AuditHistoryInfo>(DbHelperSQL.Query(strSql.ToString(),trans, param));
        }

		#endregion  成员方法

        #region IAuditHistoryProvider 成员


        //public int DeleteByProjectId(int ProjectId)
        //{
        //    return DeleteByProjectId(ProjectId, false);
        //}

        //public int DeleteByProjectId(int ProjectId, string term, List<SqlParameter> param)
        //{
        //    return DeleteByProjectId(ProjectId, term, param, false);
        //}

        public int DeleteByProjectId(int ProjectId)
        {
            return DeleteByProjectId(ProjectId,string.Empty,null,null);
        }

        public int DeleteByProjectId(int ProjectId,SqlTransaction trans)
        {
            return DeleteByProjectId(ProjectId, string.Empty, null, trans);
        }

        public int DeleteByProjectId(int ProjectId, string term, List<SqlParameter> param,SqlTransaction trans)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete F_AuditHistory ");
            strSql.Append(" where ProjectID=@ProjectID ");
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

            SqlParameter pm = new SqlParameter("@ProjectID", SqlDbType.Int, 4);
            pm.Value = ProjectId;

            param.Add(pm);

            return DbHelperSQL.ExecuteSql(strSql.ToString(), trans, param.ToArray());
        }

        #endregion
    }
}
