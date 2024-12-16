using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using ESP.Finance.Utility;

namespace ESP.Finance.DataAccess
{
    internal class CheckDataProvider : ESP.Finance.IDataAccess.ICheckDataProvider
    {
        
		#region  成员方法

        public bool Exists(string CheckSysCode, string CheckCode)
        {
            return Exists(CheckSysCode, CheckCode, null);
        }
		/// <summary>
		/// 是否存在该记录
		/// </summary>
        public bool Exists(string CheckSysCode, string CheckCode,SqlTransaction trans)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(*) from F_Check");
            strSql.Append(" where CheckSysCode=@CheckSysCode or CheckCode=@CheckCode ");
			SqlParameter[] parameters = {
					new SqlParameter("@CheckSysCode", SqlDbType.NVarChar,50),
                                        new SqlParameter("@CheckCode", SqlDbType.NVarChar,50)
                                        };
            parameters[0].Value = CheckSysCode;
            parameters[1].Value = CheckCode;

			return DbHelperSQL.Exists(strSql.ToString(),trans,parameters);
		}

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(ESP.Finance.Entity.CheckInfo model)
        {
            return Add(model, null);
        }

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(ESP.Finance.Entity.CheckInfo model,SqlTransaction trans)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into F_Check(");
			strSql.Append(@"CheckSysCode,CheckCode,CreateDate,CheckStatus,CreatorID,
                            CreatorUserCode,CreatorUserName,CreatorEmployeeName)");

			strSql.Append(" values (");
			strSql.Append(@"@CheckSysCode,@CheckCode,@CreateDate,@CheckStatus,@CreatorID,
                            @CreatorUserCode,@CreatorUserName,@CreatorEmployeeName)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = {
					new SqlParameter("@CheckSysCode", SqlDbType.NVarChar,50),
					new SqlParameter("@CheckCode", SqlDbType.NVarChar,50),
					new SqlParameter("@CreateDate", SqlDbType.DateTime),
					new SqlParameter("@CheckStatus", SqlDbType.Int,4),
					new SqlParameter("@CreatorID", SqlDbType.Int,4),
					new SqlParameter("@CreatorUserCode", SqlDbType.NVarChar,50),
					new SqlParameter("@CreatorUserName", SqlDbType.NVarChar,50),
					new SqlParameter("@CreatorEmployeeName", SqlDbType.NVarChar,50)};
			parameters[0].Value =model.CheckSysCode;
			parameters[1].Value =model.CheckCode;
			parameters[2].Value =model.CreateDate;
			parameters[3].Value =model.CheckStatus;
			parameters[4].Value =model.CreatorID;
			parameters[5].Value =model.CreatorUserCode;
			parameters[6].Value =model.CreatorUserName;
			parameters[7].Value =model.CreatorEmployeeName;

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

        ///// <summary>
        ///// 更新一条数据
        ///// </summary>
        //public int Update(ESP.Finance.Entity.CheckInfo model)
        //{
        //    return Update(model, false);
        //}
		/// <summary>
		/// 更新一条数据
		/// </summary>
		public int Update(ESP.Finance.Entity.CheckInfo model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update F_Check set ");
			strSql.Append("CheckSysCode=@CheckSysCode,");
			strSql.Append("CheckCode=@CheckCode,");
			strSql.Append("CreateDate=@CreateDate,");
			strSql.Append("CheckStatus=@CheckStatus,");
			strSql.Append("CreatorID=@CreatorID,");
			strSql.Append("CreatorUserCode=@CreatorUserCode,");
			strSql.Append("CreatorUserName=@CreatorUserName,");
			strSql.Append("CreatorEmployeeName=@CreatorEmployeeName");
			strSql.Append(" where CheckID=@CheckID  ");
			SqlParameter[] parameters = {
					new SqlParameter("@CheckID", SqlDbType.Int,4),
					new SqlParameter("@CheckSysCode", SqlDbType.NVarChar,50),
					new SqlParameter("@CheckCode", SqlDbType.NVarChar,50),
					new SqlParameter("@CreateDate", SqlDbType.DateTime),
					new SqlParameter("@CheckStatus", SqlDbType.Int,4),
					new SqlParameter("@CreatorID", SqlDbType.Int,4),
					new SqlParameter("@CreatorUserCode", SqlDbType.NVarChar,50),
					new SqlParameter("@CreatorUserName", SqlDbType.NVarChar,50),
					new SqlParameter("@CreatorEmployeeName", SqlDbType.NVarChar,50)};
			parameters[0].Value =model.CheckID;
			parameters[1].Value =model.CheckSysCode;
			parameters[2].Value =model.CheckCode;
			parameters[3].Value =model.CreateDate;
			parameters[4].Value =model.CheckStatus;
			parameters[5].Value =model.CreatorID;
			parameters[6].Value =model.CreatorUserCode;
			parameters[7].Value =model.CreatorUserName;
			parameters[8].Value =model.CreatorEmployeeName;

			return DbHelperSQL.ExecuteSql(strSql.ToString(),parameters);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public int Delete(int CheckID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete F_Check ");
			strSql.Append(" where CheckID=@CheckID ");
			SqlParameter[] parameters = {
					new SqlParameter("@CheckID", SqlDbType.Int,4)};
			parameters[0].Value = CheckID;

			return DbHelperSQL.ExecuteSql(strSql.ToString(),parameters);
		}


		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public ESP.Finance.Entity.CheckInfo GetModel(int CheckID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1  ");
			strSql.Append(@"CheckID,CheckSysCode,CheckCode,CreateDate,CheckStatus,
CreatorID,CreatorUserCode,CreatorUserName,CreatorEmployeeName");
			strSql.Append(" FROM F_Check ");
			strSql.Append(" where CheckID=@CheckID ");
			SqlParameter[] parameters = {
					new SqlParameter("@CheckID", SqlDbType.Int,4)};
			parameters[0].Value = CheckID;

			return CBO.FillObject<ESP.Finance.Entity.CheckInfo>(DbHelperSQL.Query(strSql.ToString(), parameters));
		}

		/// <summary>
		/// 获得数据列表
		/// </summary>
		public IList<ESP.Finance.Entity.CheckInfo> GetList(string term,List<SqlParameter> param)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select ");
			strSql.Append(@"CheckID,CheckSysCode,CheckCode,CreateDate,CheckStatus,
CreatorID,CreatorUserCode,CreatorUserName,CreatorEmployeeName");
			strSql.Append(" FROM F_Check ");
			if (!string.IsNullOrEmpty(term))
			{
				strSql.Append(" where " + term);
			}
			return CBO.FillCollection<ESP.Finance.Entity.CheckInfo>(DbHelperSQL.Query(strSql.ToString(), param));
		}


		#endregion  成员方法
    }
}
