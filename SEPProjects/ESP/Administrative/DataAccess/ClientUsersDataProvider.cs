using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESP.Administrative.Common;
using ESP.Administrative.Entity;
using System.Data.SqlClient;
using System.Data;

namespace ESP.Administrative.DataAccess
{
    public class ClientUsersDataProvider
    {
        public ClientUsersDataProvider()
        { }
		#region  成员方法
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int ID)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from AD_ClientUsers");
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
		public int Add(ClientUsersInfo model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into AD_ClientUsers(");
			strSql.Append("UserID,UserName,FAID,FAName,BeginTime,EndTime,Deleted,CreateTime,UpdateTime,OperateorID,OperateorDepId,Sort)");
			strSql.Append(" values (");
			strSql.Append("@UserID,@UserName,@FAID,@FAName,@BeginTime,@EndTime,@Deleted,@CreateTime,@UpdateTime,@OperateorID,@OperateorDepId,@Sort)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = {
					new SqlParameter("@UserID", SqlDbType.Int,4),
					new SqlParameter("@UserName", SqlDbType.NVarChar),
					new SqlParameter("@FAID", SqlDbType.Int,4),
					new SqlParameter("@FAName", SqlDbType.NVarChar),
					new SqlParameter("@BeginTime", SqlDbType.DateTime),
					new SqlParameter("@EndTime", SqlDbType.DateTime),
					new SqlParameter("@Deleted", SqlDbType.Bit,1),
					new SqlParameter("@CreateTime", SqlDbType.DateTime),
					new SqlParameter("@UpdateTime", SqlDbType.DateTime),
					new SqlParameter("@OperateorID", SqlDbType.Int,4),
					new SqlParameter("@OperateorDepId", SqlDbType.Int,4),
					new SqlParameter("@Sort", SqlDbType.Int,4)};
			parameters[0].Value = model.UserID;
			parameters[1].Value = model.UserName;
			parameters[2].Value = model.FAID;
			parameters[3].Value = model.FAName;
			parameters[4].Value = model.BeginTime;
			parameters[5].Value = model.EndTime;
			parameters[6].Value = model.Deleted;
			parameters[7].Value = model.CreateTime;
			parameters[8].Value = model.UpdateTime;
			parameters[9].Value = model.OperateorID;
			parameters[10].Value = model.OperateorDepId;
			parameters[11].Value = model.Sort;

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
        public void Update(ClientUsersInfo model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update AD_ClientUsers set ");
			strSql.Append("UserID=@UserID,");
			strSql.Append("UserName=@UserName,");
			strSql.Append("FAID=@FAID,");
			strSql.Append("FAName=@FAName,");
			strSql.Append("BeginTime=@BeginTime,");
			strSql.Append("EndTime=@EndTime,");
			strSql.Append("Deleted=@Deleted,");
			strSql.Append("CreateTime=@CreateTime,");
			strSql.Append("UpdateTime=@UpdateTime,");
			strSql.Append("OperateorID=@OperateorID,");
			strSql.Append("OperateorDepId=@OperateorDepId,");
			strSql.Append("Sort=@Sort");
			strSql.Append(" where ID=@ID");
			SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4),
					new SqlParameter("@UserID", SqlDbType.Int,4),
					new SqlParameter("@UserName", SqlDbType.NVarChar),
					new SqlParameter("@FAID", SqlDbType.Int,4),
					new SqlParameter("@FAName", SqlDbType.NVarChar),
					new SqlParameter("@BeginTime", SqlDbType.DateTime),
					new SqlParameter("@EndTime", SqlDbType.DateTime),
					new SqlParameter("@Deleted", SqlDbType.Bit,1),
					new SqlParameter("@CreateTime", SqlDbType.DateTime),
					new SqlParameter("@UpdateTime", SqlDbType.DateTime),
					new SqlParameter("@OperateorID", SqlDbType.Int,4),
					new SqlParameter("@OperateorDepId", SqlDbType.Int,4),
					new SqlParameter("@Sort", SqlDbType.Int,4)};
			parameters[0].Value = model.ID;
			parameters[1].Value = model.UserID;
			parameters[2].Value = model.UserName;
			parameters[3].Value = model.FAID;
			parameters[4].Value = model.FAName;
			parameters[5].Value = model.BeginTime;
			parameters[6].Value = model.EndTime;
			parameters[7].Value = model.Deleted;
			parameters[8].Value = model.CreateTime;
			parameters[9].Value = model.UpdateTime;
			parameters[10].Value = model.OperateorID;
			parameters[11].Value = model.OperateorDepId;
			parameters[12].Value = model.Sort;

			DbHelperSQL.ExecuteSql(strSql.ToString(),parameters);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public void Delete(int ID)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete AD_ClientUsers ");
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
        public ClientUsersInfo GetModel(int ID)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select * from AD_ClientUsers ");
			strSql.Append(" where ID=@ID");
			SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)};
			parameters[0].Value = ID;
            ClientUsersInfo model = new ClientUsersInfo();
			DataSet ds=DbHelperSQL.Query(strSql.ToString(),parameters);
			model.ID=ID;
			if(ds.Tables[0].Rows.Count>0)
			{
                model.PopupData(ds.Tables[0].Rows[0]);
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
			strSql.Append("select * ");
			strSql.Append(" FROM AD_ClientUsers ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			return DbHelperSQL.Query(strSql.ToString());
		}

		#endregion  成员方法
	}
}