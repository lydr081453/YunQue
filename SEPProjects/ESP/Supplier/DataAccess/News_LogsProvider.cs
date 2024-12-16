using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using Supply.Common;
using Supply.Entity;
using ESP.Supplier.Common;
using ESP.ConfigCommon;

namespace Supply.DataAccess
{
    public class LogsProvider
    {
        public LogsProvider()
		{}
		#region  成员方法

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(Logs model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into Logs(");
			strSql.Append("LogType,LogContent,CreateTime,CreateIp,UserId)");
			strSql.Append(" values (");
			strSql.Append("@LogType,@LogContent,@CreateTime,@CreateIp,@UserId)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = {
					new SqlParameter("@LogType", SqlDbType.Int,4),
					new SqlParameter("@LogContent", SqlDbType.NVarChar,200),
					new SqlParameter("@CreateTime", SqlDbType.DateTime),
					new SqlParameter("@CreateIp", SqlDbType.NVarChar,50),
					new SqlParameter("@UserId", SqlDbType.Int,4)};
			parameters[0].Value = model.LogType;
			parameters[1].Value = model.LogContent;
			parameters[2].Value = model.CreateTime;
			parameters[3].Value = model.CreateIp;
			parameters[4].Value = model.UserId;

			object obj = DbHelperSQL.GetSingle(strSql.ToString(),parameters);
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
		public void Update(Logs model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update Logs set ");
			strSql.Append("LogType=@LogType,");
			strSql.Append("LogContent=@LogContent,");
			strSql.Append("CreateTime=@CreateTime,");
			strSql.Append("CreateIp=@CreateIp,");
			strSql.Append("UserId=@UserId");
			strSql.Append(" where ID=@ID ");
			SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4),
					new SqlParameter("@LogType", SqlDbType.Int,4),
					new SqlParameter("@LogContent", SqlDbType.NVarChar,200),
					new SqlParameter("@CreateTime", SqlDbType.DateTime),
					new SqlParameter("@CreateIp", SqlDbType.NVarChar,50),
					new SqlParameter("@UserId", SqlDbType.Int,4)};
			parameters[0].Value = model.ID;
			parameters[1].Value = model.LogType;
			parameters[2].Value = model.LogContent;
			parameters[3].Value = model.CreateTime;
			parameters[4].Value = model.CreateIp;
			parameters[5].Value = model.UserId;

			DbHelperSQL.ExecuteSql(strSql.ToString(),parameters);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public void Delete(int ID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from Logs ");
			strSql.Append(" where ID=@ID ");
			SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)};
			parameters[0].Value = ID;

			DbHelperSQL.ExecuteSql(strSql.ToString(),parameters);
		}


		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public Logs GetModel(int ID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  ID,LogType,LogContent,CreateTime,CreateIp,UserId from Logs ");
			strSql.Append(" where ID=@ID ");
			SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)};
			parameters[0].Value = ID;

            return CBO.FillObject<Logs>(DbHelperSQL.Query(strSql.ToString(), parameters));
		}

		/// <summary>
		/// 获得数据列表
		/// </summary>
		public DataSet GetList(string strWhere)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select ID,LogType,LogContent,CreateTime,CreateIp,UserId ");
			strSql.Append(" FROM Logs ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			return DbHelperSQL.Query(strSql.ToString());
        }
        #endregion
    }
}
