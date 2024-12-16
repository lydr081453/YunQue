using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using ESP.Finance.Utility;

namespace ESP.Finance.DataAccess
{
    internal class SupporterAuditHistDataProvider : ESP.Finance.IDataAccess.ISupporterAuditHistDataProvider
    {

        
		#region  成员方法
        //        /// <summary>
        ///// 增加一条数据
        ///// </summary>
        //public int Add(Entity.SupporterAuditHistInfo model)
        //{
        //    return Add(model, false);
        //}

        /// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(Entity.SupporterAuditHistInfo model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into F_SupporterAuditHist(");
            strSql.Append("AuditStatus,SupporterID,AuditorUserID,AuditorUserName,AuditorUserCode,AuditorEmployeeName,Suggestion,SquenceLevel,TotalLevel,AuditDate,Version,AuditType)");
			strSql.Append(" values (");
            strSql.Append("@AuditStatus,@SupporterID,@AuditorUserID,@AuditorUserName,@AuditorUserCode,@AuditorEmployeeName,@Suggestion,@SquenceLevel,@TotalLevel,@AuditDate,@Version,@AuditType)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = {
					new SqlParameter("@AuditStatus", SqlDbType.Int,4),
					new SqlParameter("@SupporterID", SqlDbType.Int,4),
					new SqlParameter("@AuditorUserID", SqlDbType.Int,4),
					new SqlParameter("@AuditorUserName", SqlDbType.NVarChar,50),
					new SqlParameter("@AuditorUserCode", SqlDbType.NVarChar,50),
					new SqlParameter("@AuditorEmployeeName", SqlDbType.NVarChar,50),
					new SqlParameter("@Suggestion", SqlDbType.NVarChar,200),
					new SqlParameter("@SquenceLevel", SqlDbType.Int,4),
					new SqlParameter("@TotalLevel", SqlDbType.Int,4),
                    new SqlParameter("@AuditDate",SqlDbType.DateTime,8),
                    new SqlParameter("@Version",SqlDbType.Int,4),
                    new SqlParameter("@AuditType",SqlDbType.Int,4)
                                        };
			parameters[0].Value =model.AuditStatus;
			parameters[1].Value =model.SupporterID;
			parameters[2].Value =model.AuditorUserID;
			parameters[3].Value =model.AuditorUserName;
			parameters[4].Value =model.AuditorUserCode;
			parameters[5].Value =model.AuditorEmployeeName;
			parameters[6].Value =model.Suggestion;
			parameters[7].Value =model.SquenceLevel;
			parameters[8].Value =model.TotalLevel;
            parameters[9].Value =model.AuditDate;
            parameters[10].Value =model.Version;
            parameters[11].Value =model.AuditType;

			object obj = DbHelperSQL.GetSingle(strSql.ToString(),parameters);
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
        public int Update(Entity.SupporterAuditHistInfo model)
        {
            return Update(model, null);
        }

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public int Update(Entity.SupporterAuditHistInfo model,SqlTransaction trans)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update F_SupporterAuditHist set ");
			strSql.Append("AuditStatus=@AuditStatus,");
			strSql.Append("SupporterID=@SupporterID,");
			strSql.Append("AuditorUserID=@AuditorUserID,");
			strSql.Append("AuditorUserName=@AuditorUserName,");
			strSql.Append("AuditorUserCode=@AuditorUserCode,");
			strSql.Append("AuditorEmployeeName=@AuditorEmployeeName,");
			strSql.Append("Suggestion=@Suggestion,");
			strSql.Append("SquenceLevel=@SquenceLevel,");
			strSql.Append("TotalLevel=@TotalLevel,");
            strSql.Append("AuditDate=@AuditDate, ");
            strSql.Append("Version=@Version,");
            strSql.Append("AuditType=@AuditType ");
			strSql.Append(" where SupporterAuditID=@SupporterAuditID ");
			SqlParameter[] parameters = {
					new SqlParameter("@SupporterAuditID", SqlDbType.Int,4),
					new SqlParameter("@AuditStatus", SqlDbType.Int,4),
					new SqlParameter("@SupporterID", SqlDbType.Int,4),
					new SqlParameter("@AuditorUserID", SqlDbType.Int,4),
					new SqlParameter("@AuditorUserName", SqlDbType.NVarChar,50),
					new SqlParameter("@AuditorUserCode", SqlDbType.NVarChar,50),
					new SqlParameter("@AuditorEmployeeName", SqlDbType.NVarChar,50),
					new SqlParameter("@Suggestion", SqlDbType.NVarChar,200),
					new SqlParameter("@SquenceLevel", SqlDbType.Int,4),
					new SqlParameter("@TotalLevel", SqlDbType.Int,4),
                    new SqlParameter("@AuditDate",SqlDbType.DateTime,8),
                    new SqlParameter("@Version",SqlDbType.Int,4),
                    new SqlParameter("@AuditType",SqlDbType.Int,4)
                                        };
			parameters[0].Value =model.SupporterAuditID;
			parameters[1].Value =model.AuditStatus;
			parameters[2].Value =model.SupporterID;
			parameters[3].Value =model.AuditorUserID;
			parameters[4].Value =model.AuditorUserName;
			parameters[5].Value =model.AuditorUserCode;
			parameters[6].Value =model.AuditorEmployeeName;
			parameters[7].Value =model.Suggestion;
			parameters[8].Value =model.SquenceLevel;
			parameters[9].Value =model.TotalLevel;
            parameters[10].Value =model.AuditDate;
            parameters[11].Value =model.Version;
            parameters[12].Value =model.AuditType;

			return DbHelperSQL.ExecuteSql(strSql.ToString(),trans,parameters);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public int Delete(int SupporterAuditID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete F_SupporterAuditHist ");
			strSql.Append(" where SupporterAuditID=@SupporterAuditID ");
			SqlParameter[] parameters = {
					new SqlParameter("@SupporterAuditID", SqlDbType.Int,4)};
			parameters[0].Value = SupporterAuditID;

			return DbHelperSQL.ExecuteSql(strSql.ToString(),parameters);
		}


		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public Entity.SupporterAuditHistInfo GetModel(int SupporterAuditID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append(@"select  top 1 SupporterAuditID,AuditStatus,SupporterID,AuditorUserID,
                            AuditorUserName,AuditorUserCode,AuditorEmployeeName,Suggestion,
                            SquenceLevel,TotalLevel,AuditDate,Version,AuditType
                            from F_SupporterAuditHist ");
			strSql.Append(" where SupporterAuditID=@SupporterAuditID ");
			SqlParameter[] parameters = {
					new SqlParameter("@SupporterAuditID", SqlDbType.Int,4)};
			parameters[0].Value = SupporterAuditID;

            return CBO.FillObject<Entity.SupporterAuditHistInfo>(DbHelperSQL.Query(strSql.ToString(), parameters));

        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public IList<Entity.SupporterAuditHistInfo> GetList(string term, List<SqlParameter> param)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"select SupporterAuditID,AuditStatus,SupporterID,
                            AuditorUserID,AuditorUserName,AuditorUserCode,
                            AuditorEmployeeName,Suggestion,SquenceLevel,
                            TotalLevel,AuditDate,Version,AuditType ");
            strSql.Append(" FROM F_SupporterAuditHist ");
            if (!string.IsNullOrEmpty(term))
            {
                strSql.Append(" where " + term);
            }
            //if (param != null && param.Count > 0)
            //{
            //    SqlParameter[] ps = param.ToArray();

            //    return CBO.FillCollection<Entity.SupporterAuditHistInfo>(DbHelperSQL.ExecuteReader(strSql.ToString(), ps));
            //}
            return CBO.FillCollection<Entity.SupporterAuditHistInfo>(DbHelperSQL.Query(strSql.ToString(),param));
        }


        #endregion


        #region ISupporterAuditHistProvider 成员


        //public int DeleteBySupporterId(int SupporterId)
        //{
        //    return DeleteBySupporterId(SupporterId, false);
        //}

        //public int DeleteBySupporterId(int SupporterId, string term, List<SqlParameter> param)
        //{
        //    return DeleteBySupporterId(SupporterId, term, param, false);
        //}

        public int DeleteBySupporterId(int SupporterId)
        {
            return DeleteBySupporterId(SupporterId, string.Empty,null,null);
        }

        public int DeleteBySupporterId(int SupporterId, string term, List<SqlParameter> param)
        {
            return DeleteBySupporterId(SupporterId, term, param, null);
        }

        public int DeleteBySupporterId(int SupporterId, string term, List<SqlParameter> param,SqlTransaction trans)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete F_SupporterAuditHist ");
            strSql.Append(" where SupporterID=@SupporterID ");
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

            SqlParameter pm = new SqlParameter("@SupporterID", SqlDbType.Int, 4);
            pm.Value = SupporterId;

            param.Add(pm);

            return DbHelperSQL.ExecuteSql(strSql.ToString(),trans, param.ToArray());
        }

        #endregion
    }
}
