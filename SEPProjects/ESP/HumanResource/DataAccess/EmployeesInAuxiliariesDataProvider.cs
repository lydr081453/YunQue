using System;
using System.Collections.Generic;
using System.Web;
using System.Data.SqlClient;
using ESP.HumanResource.Common;
using System.Data;
using System.Text;
using ESP.HumanResource.Entity;

namespace ESP.HumanResource.DataAccess
{    
    public class EmployeesInAuxiliariesDataProvider
	{
        public EmployeesInAuxiliariesDataProvider()
		{}
		#region  成员方法

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(EmployeesInAuxiliariesInfo model, SqlConnection conn, SqlTransaction trans)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into sep_EmployeesInAuxiliaries(");
			strSql.Append("userId,auxiliaryId)");
			strSql.Append(" values (");
			strSql.Append("@userId,@auxiliaryId)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = {
					new SqlParameter("@userId", SqlDbType.Int,4),
					new SqlParameter("@auxiliaryId", SqlDbType.Int,4)};
			parameters[0].Value = model.userId;
			parameters[1].Value = model.auxiliaryId;

            object obj = DbHelperSQL.GetSingle(strSql.ToString(), conn, trans, parameters);
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
		public void Update(EmployeesInAuxiliariesInfo model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update sep_EmployeesInAuxiliaries set ");
			strSql.Append("userId=@userId,");
			strSql.Append("auxiliaryId=@auxiliaryId");
			strSql.Append(" where id=@id ");
			SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4),
					new SqlParameter("@userId", SqlDbType.Int,4),
					new SqlParameter("@auxiliaryId", SqlDbType.Int,4)};
			parameters[0].Value = model.id;
			parameters[1].Value = model.userId;
			parameters[2].Value = model.auxiliaryId;

			DbHelperSQL.ExecuteSql(strSql.ToString(),parameters);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
        public int Delete(int id, SqlConnection conn, SqlTransaction trans)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete sep_EmployeesInAuxiliaries ");
			strSql.Append(" where id=@id ");
			SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)};
			parameters[0].Value = id;

            return DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
		}

        /// <summary>
        /// 根据用户id和辅助工作ID删除数据
        /// </summary>
        public int Delete(int userId, int auxiliaryId, SqlConnection conn, SqlTransaction trans)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete sep_EmployeesInAuxiliaries ");
            strSql.Append(" where userId=@userId and ");
            strSql.Append(" auxiliaryId=@auxiliaryId");
            SqlParameter[] parameters = {
					new SqlParameter("@userId", SqlDbType.Int,4),
                    new SqlParameter("@auxiliaryId", SqlDbType.Int,4)
				};
            parameters[0].Value = userId;
            parameters[1].Value = auxiliaryId;

            return DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }

        /// <summary>
		/// 根据Auxiliaryid删除数据
		/// </summary>
        public int DeleteByAuxID(int auxiliaryid, SqlConnection conn, SqlTransaction trans)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete sep_EmployeesInAuxiliaries ");
            strSql.Append(" where auxiliaryid=@auxiliaryid ");
            SqlParameter[] parameters = {
					new SqlParameter("@auxiliaryid", SqlDbType.Int,4)};
            parameters[0].Value = auxiliaryid;

            return DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }
		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public EmployeesInAuxiliariesInfo GetModel(int id)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 id,userId,auxiliaryId from sep_EmployeesInAuxiliaries ");
			strSql.Append(" where id=@id ");
			SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)};
			parameters[0].Value = id;

			EmployeesInAuxiliariesInfo model=new EmployeesInAuxiliariesInfo();
			DataSet ds=DbHelperSQL.Query(strSql.ToString(),parameters);
			if(ds.Tables[0].Rows.Count>0)
			{
				if(ds.Tables[0].Rows[0]["id"].ToString()!="")
				{
					model.id=int.Parse(ds.Tables[0].Rows[0]["id"].ToString());
				}
				if(ds.Tables[0].Rows[0]["userId"].ToString()!="")
				{
					model.userId=int.Parse(ds.Tables[0].Rows[0]["userId"].ToString());
				}
				if(ds.Tables[0].Rows[0]["auxiliaryId"].ToString()!="")
				{
					model.auxiliaryId=int.Parse(ds.Tables[0].Rows[0]["auxiliaryId"].ToString());
				}
				return model;
			}
			else
			{
				return null;
			}
		}

        /// <summary>
        /// 得到一个对象List
        /// </summary>
        public List<EmployeesInAuxiliariesInfo> GetModelList(string strWhere, List<SqlParameter> parms)
        {
            List<EmployeesInAuxiliariesInfo> list = new List<EmployeesInAuxiliariesInfo>();
            string strSql = @"select a.*,b.LastNameCN+b.FirstNameCN as FullNameCN,b.FirstNameEN+b.LastNameEN as FullNameEN,b.email,b.UserName,c.auxiliaryname from sep_EmployeesInAuxiliaries as a 
                                left join sep_Users as b on a.userid = b.userid 
                                left join sep_Auxiliary as c on a.auxiliaryid = c.id 
                                where (1=1) ";
            strSql += strWhere;

            using (SqlDataReader r = DbHelperSQL.ExecuteReader(strSql, parms))
            {
                while (r.Read())
                {
                    EmployeesInAuxiliariesInfo model = new EmployeesInAuxiliariesInfo();
                    model.id = int.Parse(r["id"].ToString());
                    if (r["userId"].ToString() != "")
                    {
                        model.userId = int.Parse(r["userId"].ToString());
                    }
                    model.FullNameCN = r["FullNameCN"].ToString();
                    model.FullNameEN = r["FullNameEN"].ToString();
                    model.username = r["username"].ToString();
                    model.email = r["email"].ToString();
                    if (r["auxiliaryId"].ToString() != "")
                    {
                        model.auxiliaryId = int.Parse(r["auxiliaryId"].ToString());
                    }
                    model.auxiliaryname = r["auxiliaryname"].ToString();
                    list.Add(model);
                }
                r.Close();
            }
            return list;
        }

		/// <summary>
		/// 获得数据列表
		/// </summary>
		public DataSet GetList(string strWhere)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select id,userId,auxiliaryId ");
			strSql.Append(" FROM sep_EmployeesInAuxiliaries ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			return DbHelperSQL.Query(strSql.ToString());
		}


        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int userId, int auxiliaryId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from sep_EmployeesInAuxiliaries");
            strSql.Append(" where userId=@userId and ");
            strSql.Append(" auxiliaryId=@auxiliaryId");
            SqlParameter[] parameters = {
					new SqlParameter("@userId", SqlDbType.Int,4),
                    new SqlParameter("@auxiliaryId", SqlDbType.Int,4)
				};
            parameters[0].Value = userId;
            parameters[1].Value = auxiliaryId;
            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }
		#endregion  成员方法
	}
}
