using System;
using System.Collections.Generic;
using ESP.HumanResource.Entity;
using System.Web;
using System.Data.SqlClient;
using ESP.HumanResource.Common;
using System.Data;
using System.Text;

namespace ESP.HumanResource.DataAccess
{
    public class ProtectionLineDataProvider
    {
        public ProtectionLineDataProvider()
		{}
		#region  成员方法
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int ID)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from sep_ProtectionLine");
			strSql.Append(" where ID= @ID");
			SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)
				};
			parameters[0].Value = ID;
			return DbHelperSQL.Exists(strSql.ToString(),parameters);
		}


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(ProtectionLineInfo model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into sep_ProtectionLine(");
			strSql.Append("ProtectionLineName,ProtectionLineNameAmount,Creator,CreateTime,LastUpdaterID,LastUpdateTime)");
			strSql.Append(" values (");
			strSql.Append("@ProtectionLineName,@ProtectionLineNameAmount,@Creator,@CreateTime,@LastUpdaterID,@LastUpdateTime)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = {
					new SqlParameter("@ProtectionLineName", SqlDbType.NVarChar),
					new SqlParameter("@ProtectionLineNameAmount", SqlDbType.Decimal,9),
					new SqlParameter("@Creator", SqlDbType.Int,4),
					new SqlParameter("@CreateTime", SqlDbType.DateTime),
					new SqlParameter("@LastUpdaterID", SqlDbType.Int,4),
					new SqlParameter("@LastUpdateTime", SqlDbType.DateTime)};
			parameters[0].Value = model.ProtectionLineName;
			parameters[1].Value = model.ProtectionLineNameAmount;
			parameters[2].Value = model.Creator;
			parameters[3].Value = model.CreateTime;
			parameters[4].Value = model.LastUpdaterID;
			parameters[5].Value = model.LastUpdateTime;

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
		public void Update(ProtectionLineInfo model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update sep_ProtectionLine set ");
			strSql.Append("ProtectionLineName=@ProtectionLineName,");
			strSql.Append("ProtectionLineNameAmount=@ProtectionLineNameAmount,");
			strSql.Append("Creator=@Creator,");
			strSql.Append("CreateTime=@CreateTime,");
			strSql.Append("LastUpdaterID=@LastUpdaterID,");
			strSql.Append("LastUpdateTime=@LastUpdateTime");
			strSql.Append(" where ID=@ID");
			SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4),
					new SqlParameter("@ProtectionLineName", SqlDbType.NVarChar),
					new SqlParameter("@ProtectionLineNameAmount", SqlDbType.Decimal,9),
					new SqlParameter("@Creator", SqlDbType.Int,4),
					new SqlParameter("@CreateTime", SqlDbType.DateTime),
					new SqlParameter("@LastUpdaterID", SqlDbType.Int,4),
					new SqlParameter("@LastUpdateTime", SqlDbType.DateTime)};
			parameters[0].Value = model.ID;
			parameters[1].Value = model.ProtectionLineName;
			parameters[2].Value = model.ProtectionLineNameAmount;
			parameters[3].Value = model.Creator;
			parameters[4].Value = model.CreateTime;
			parameters[5].Value = model.LastUpdaterID;
			parameters[6].Value = model.LastUpdateTime;

			DbHelperSQL.ExecuteSql(strSql.ToString(),parameters);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public void Delete(int ID)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete sep_ProtectionLine ");
			strSql.Append(" where ID=@ID");
			SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)
				};
			parameters[0].Value = ID;
			DbHelperSQL.ExecuteSql(strSql.ToString(),parameters);
		}


		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public ProtectionLineInfo GetModel(int ID)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select * from sep_ProtectionLine ");
			strSql.Append(" where ID=@ID");
			SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)};
			parameters[0].Value = ID;
			ProtectionLineInfo model=new ProtectionLineInfo();
			DataSet ds=DbHelperSQL.Query(strSql.ToString(),parameters);
			model.ID=ID;
			if(ds.Tables[0].Rows.Count>0)
			{
				model.ProtectionLineName=ds.Tables[0].Rows[0]["ProtectionLineName"].ToString();
				if(ds.Tables[0].Rows[0]["ProtectionLineNameAmount"].ToString()!="")
				{
					model.ProtectionLineNameAmount=decimal.Parse(ds.Tables[0].Rows[0]["ProtectionLineNameAmount"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Creator"].ToString()!="")
				{
					model.Creator=int.Parse(ds.Tables[0].Rows[0]["Creator"].ToString());
				}
				if(ds.Tables[0].Rows[0]["CreateTime"].ToString()!="")
				{
					model.CreateTime=DateTime.Parse(ds.Tables[0].Rows[0]["CreateTime"].ToString());
				}
				if(ds.Tables[0].Rows[0]["LastUpdaterID"].ToString()!="")
				{
					model.LastUpdaterID=int.Parse(ds.Tables[0].Rows[0]["LastUpdaterID"].ToString());
				}
				if(ds.Tables[0].Rows[0]["LastUpdateTime"].ToString()!="")
				{
					model.LastUpdateTime=DateTime.Parse(ds.Tables[0].Rows[0]["LastUpdateTime"].ToString());
				}
				return model;
			}
			else
			{
			return null;
			}
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public DataSet GetList(string strWhere)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select [ID],[ProtectionLineName],[ProtectionLineNameAmount],[Creator],[CreateTime],[LastUpdaterID],[LastUpdateTime] ");
			strSql.Append(" FROM sep_ProtectionLine ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			return DbHelperSQL.Query(strSql.ToString());
		}

		#endregion  成员方法
    }
}
