using System;
using System.Collections.Generic;
using ESP.HumanResource.Entity;
using System.Web;
using System.Data.SqlClient;
using ESP.HumanResource.Common;
using System.Data;
using System.Text;
using System.Data.Common;

namespace ESP.HumanResource.DataAccess
{
    public class UserValidateDataProvider
    {
        public UserValidateDataProvider()
		{}
		#region  成员方法
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int ID)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from ESP_UserValidateInfo");
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
		public int Add(UserValidateInfo model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into ESP_UserValidateInfo(");
			strSql.Append("UserID,Pwd,CreatedDate,CreatedUserID,ModifiedDate,ModifiedUserID,LogonDate)");
			strSql.Append(" values (");
			strSql.Append("@UserID,@Pwd,@CreatedDate,@CreatedUserID,@ModifiedDate,@ModifiedUserID,@LogonDate)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = {
					new SqlParameter("@UserID", SqlDbType.Int,4),
					new SqlParameter("@Pwd", SqlDbType.NVarChar),
					new SqlParameter("@CreatedDate", SqlDbType.DateTime),
					new SqlParameter("@CreatedUserID", SqlDbType.Int,4),
					new SqlParameter("@ModifiedDate", SqlDbType.DateTime),
					new SqlParameter("@ModifiedUserID", SqlDbType.NVarChar),
					new SqlParameter("@LogonDate", SqlDbType.DateTime)};
			parameters[0].Value = model.UserID;
			parameters[1].Value = model.Pwd;
			parameters[2].Value = model.CreatedDate;
			parameters[3].Value = model.CreatedUserID;
			parameters[4].Value = model.ModifiedDate;
			parameters[5].Value = model.ModifiedUserID;
			parameters[6].Value = model.LogonDate;

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
		public void Update(UserValidateInfo model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update ESP_UserValidateInfo set ");
			strSql.Append("UserID=@UserID,");
			strSql.Append("Pwd=@Pwd,");
			strSql.Append("CreatedDate=@CreatedDate,");
			strSql.Append("CreatedUserID=@CreatedUserID,");
			strSql.Append("ModifiedDate=@ModifiedDate,");
			strSql.Append("ModifiedUserID=@ModifiedUserID,");
			strSql.Append("LogonDate=@LogonDate");
			strSql.Append(" where ID=@ID");
			SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4),
					new SqlParameter("@UserID", SqlDbType.Int,4),
					new SqlParameter("@Pwd", SqlDbType.NVarChar),
					new SqlParameter("@CreatedDate", SqlDbType.DateTime),
					new SqlParameter("@CreatedUserID", SqlDbType.Int,4),
					new SqlParameter("@ModifiedDate", SqlDbType.DateTime),
					new SqlParameter("@ModifiedUserID", SqlDbType.NVarChar),
					new SqlParameter("@LogonDate", SqlDbType.DateTime)};
			parameters[0].Value = model.ID;
			parameters[1].Value = model.UserID;
			parameters[2].Value = model.Pwd;
			parameters[3].Value = model.CreatedDate;
			parameters[4].Value = model.CreatedUserID;
			parameters[5].Value = model.ModifiedDate;
			parameters[6].Value = model.ModifiedUserID;
			parameters[7].Value = model.LogonDate;

			DbHelperSQL.ExecuteSql(strSql.ToString(),parameters);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public void Delete(int ID)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete ESP_UserValidateInfo ");
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
		public UserValidateInfo GetModel(int ID)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select * from ESP_UserValidateInfo ");
			strSql.Append(" where ID=@ID");
			SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)};
			parameters[0].Value = ID;
            UserValidateInfo model = new UserValidateInfo();
			DataSet ds=DbHelperSQL.Query(strSql.ToString(),parameters);			
			if(ds.Tables[0].Rows.Count>0)
			{
                if (ds.Tables[0].Rows[0]["ID"].ToString() != "")
                {
                    model.ID = int.Parse(ds.Tables[0].Rows[0]["ID"].ToString());
                }
				if(ds.Tables[0].Rows[0]["UserID"].ToString()!="")
				{
					model.UserID=int.Parse(ds.Tables[0].Rows[0]["UserID"].ToString());
				}
				model.Pwd=ds.Tables[0].Rows[0]["Pwd"].ToString();
				if(ds.Tables[0].Rows[0]["CreatedDate"].ToString()!="")
				{
					model.CreatedDate=DateTime.Parse(ds.Tables[0].Rows[0]["CreatedDate"].ToString());
				}
				if(ds.Tables[0].Rows[0]["CreatedUserID"].ToString()!="")
				{
					model.CreatedUserID=int.Parse(ds.Tables[0].Rows[0]["CreatedUserID"].ToString());
				}
				if(ds.Tables[0].Rows[0]["ModifiedDate"].ToString()!="")
				{
					model.ModifiedDate=DateTime.Parse(ds.Tables[0].Rows[0]["ModifiedDate"].ToString());
				}
				model.ModifiedUserID=ds.Tables[0].Rows[0]["ModifiedUserID"].ToString();
				if(ds.Tables[0].Rows[0]["LogonDate"].ToString()!="")
				{
					model.LogonDate=DateTime.Parse(ds.Tables[0].Rows[0]["LogonDate"].ToString());
				}
				return model;
			}
			else
			{
			return null;
			}
		}

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public UserValidateInfo GetModel(int userid, string pwd)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from ESP_UserValidateInfo ");
            strSql.Append(" where UserID=@UserID and Pwd=@Pwd");
            SqlParameter[] parameters = {
					new SqlParameter("@UserID", SqlDbType.Int,4),
                    new SqlParameter("@Pwd",SqlDbType.NVarChar,50)};
            parameters[0].Value = userid;
            parameters[1].Value = pwd;
            UserValidateInfo model = new UserValidateInfo();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
           
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["ID"].ToString() != "")
                {
                    model.ID = int.Parse(ds.Tables[0].Rows[0]["ID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["UserID"].ToString() != "")
                {
                    model.UserID = int.Parse(ds.Tables[0].Rows[0]["UserID"].ToString());
                }
                model.Pwd = ds.Tables[0].Rows[0]["Pwd"].ToString();
                if (ds.Tables[0].Rows[0]["CreatedDate"].ToString() != "")
                {
                    model.CreatedDate = DateTime.Parse(ds.Tables[0].Rows[0]["CreatedDate"].ToString());
                }
                if (ds.Tables[0].Rows[0]["CreatedUserID"].ToString() != "")
                {
                    model.CreatedUserID = int.Parse(ds.Tables[0].Rows[0]["CreatedUserID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["ModifiedDate"].ToString() != "")
                {
                    model.ModifiedDate = DateTime.Parse(ds.Tables[0].Rows[0]["ModifiedDate"].ToString());
                }
                model.ModifiedUserID = ds.Tables[0].Rows[0]["ModifiedUserID"].ToString();
                if (ds.Tables[0].Rows[0]["LogonDate"].ToString() != "")
                {
                    model.LogonDate = DateTime.Parse(ds.Tables[0].Rows[0]["LogonDate"].ToString());
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
			strSql.Append("select [ID],[UserID],[Pwd],[CreatedDate],[CreatedUserID],[ModifiedDate],[ModifiedUserID],[LogonDate] ");
			strSql.Append(" FROM ESP_UserValidateInfo ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			return DbHelperSQL.Query(strSql.ToString());
		}


		#endregion  成员方法
    }
}
