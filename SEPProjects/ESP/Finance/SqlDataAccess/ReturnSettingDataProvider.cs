using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace ESP.Finance.DataAccess
{
    internal class ReturnSettingDataProvider : ESP.Finance.IDataAccess.IReturnSettingDataProvider
    {
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(Entity.ReturnSettingInfo model)
        {
            return Add(model, null);
        }
        
		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(Entity.ReturnSettingInfo model,SqlTransaction trans)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into F_ReturnSetting(");
			strSql.Append("ReturnID,AuditID,AuditType)");
			strSql.Append(" values (");
			strSql.Append("@ReturnID,@AuditID,@AuditType)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = {
					new SqlParameter("@ReturnID", SqlDbType.Int,4),
					new SqlParameter("@AuditID", SqlDbType.Int,4),
					new SqlParameter("@AuditType", SqlDbType.Int,4)};
			parameters[0].Value =model.ReturnID;
			parameters[1].Value =model.AuditID;
			parameters[2].Value =model.AuditType;

			object obj = DbHelperSQL.GetSingle(strSql.ToString(),trans,parameters);
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
		public int Update(Entity.ReturnSettingInfo model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update F_ReturnSetting set ");
			strSql.Append("ReturnID=@ReturnID,");
			strSql.Append("AuditID=@AuditID,");
			strSql.Append("AuditType=@AuditType");
			strSql.Append(" where AuditBizID=@AuditBizID ");
			SqlParameter[] parameters = {
					new SqlParameter("@AuditBizID", SqlDbType.Int,4),
					new SqlParameter("@ReturnID", SqlDbType.Int,4),
					new SqlParameter("@AuditID", SqlDbType.Int,4),
					new SqlParameter("@AuditType", SqlDbType.Int,4)};
			parameters[0].Value =model.AuditBizID;
			parameters[1].Value =model.ReturnID;
			parameters[2].Value =model.AuditID;
			parameters[3].Value =model.AuditType;

			return DbHelperSQL.ExecuteSql(strSql.ToString(),parameters);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public int Delete(int AuditBizID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete F_ReturnSetting ");
			strSql.Append(" where AuditBizID=@AuditBizID ");
			SqlParameter[] parameters = {
					new SqlParameter("@AuditBizID", SqlDbType.Int,4)};
			parameters[0].Value = AuditBizID;

			return DbHelperSQL.ExecuteSql(strSql.ToString(),parameters);
		}


		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public Entity.ReturnSettingInfo GetModel(int AuditBizID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 AuditBizID,ReturnID,AuditID,AuditType from F_ReturnSetting ");
			strSql.Append(" where AuditBizID=@AuditBizID ");
			SqlParameter[] parameters = {
					new SqlParameter("@AuditBizID", SqlDbType.Int,4)};
			parameters[0].Value = AuditBizID;

            return ESP.Finance.Utility.CBO.FillObject<Entity.ReturnSettingInfo>(DbHelperSQL.Query(strSql.ToString(), parameters));
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public IList<Entity.ReturnSettingInfo> GetList(string term, List<SqlParameter> param)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select AuditBizID,ReturnID,AuditID,AuditType ");
            strSql.Append(" FROM F_ReturnSetting ");
            if (!string.IsNullOrEmpty(term))
            {
                strSql.Append(" where " + term);
            }
            //if (param != null && param.Count > 0)
            //{
            //    SqlParameter[] ps = param.ToArray();

            //    return ESP.Finance.Utility.CBO.FillCollection<Entity.ReturnSettingInfo>(DbHelperSQL.ExecuteReader(strSql.ToString(), ps));
            //}
            return ESP.Finance.Utility.CBO.FillCollection<Entity.ReturnSettingInfo>(DbHelperSQL.Query(strSql.ToString(),param));
        }



        #region IReturnSettingProvider 成员


        public int DeleteByReturnId(int ReturnId)
        {
            return DeleteByReturnId(ReturnId,"",null,null);
        }

        public int DeleteByReturnId(int ReturnId, string term, List<SqlParameter> param)
        {
            return DeleteByReturnId(ReturnId, term, param, null);
        }

        //public int DeleteByReturnId(int ReturnId, bool isInTrans)
        //{
        //    return DeleteByReturnId(ReturnId,string.Empty,null,isInTrans);
        //}

        public int DeleteByReturnId(int ReturnId, string term, List<SqlParameter> param,SqlTransaction trans)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete F_ReturnSetting ");
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
            pm.Value = ReturnId;

            param.Add(pm);

            return DbHelperSQL.ExecuteSql(strSql.ToString(),trans, param.ToArray());
        }

        #endregion
    }
}
