using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESP.Supplier.Common;
using ESP.Supplier.Entity;
using System.Data;
using System.Data.SqlClient;

namespace ESP.Supplier.DataAccess
{
    public class SC_AreaDataProvider
    {
        public SC_AreaDataProvider()
		{ }
        #region  成员方法
        /// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(SC_Area model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into SC_Area(");
			strSql.Append("AreaDes,AreaStatus)");
			strSql.Append(" values (");
			strSql.Append("@AreaDes,@AreaStatus)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = {
					new SqlParameter("@AreaDes", SqlDbType.NVarChar),
					new SqlParameter("@AreaStatus", SqlDbType.Int,4)};
			parameters[0].Value = model.AreaDes;
			parameters[1].Value = model.AreaStatus;

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
		public void Update(SC_Area model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update SC_Area set ");
			strSql.Append("AreaDes=@AreaDes,");
			strSql.Append("AreaStatus=@AreaStatus");
			strSql.Append(" where AreaId=@AreaId");
			SqlParameter[] parameters = {
					new SqlParameter("@AreaId", SqlDbType.Int,4),
					new SqlParameter("@AreaDes", SqlDbType.NVarChar),
					new SqlParameter("@AreaStatus", SqlDbType.Int,4)};
			parameters[0].Value = model.AreaId;
			parameters[1].Value = model.AreaDes;
			parameters[2].Value = model.AreaStatus;

			DbHelperSQL.ExecuteSql(strSql.ToString(),parameters);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public void Delete(int AreaId)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete SC_Area ");
			strSql.Append(" where AreaId=@AreaId");
			SqlParameter[] parameters = {
					new SqlParameter("@AreaId", SqlDbType.Int,4)
				};
			parameters[0].Value = AreaId;
			DbHelperSQL.ExecuteSql(strSql.ToString(),parameters);
		}


		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public SC_Area GetModel(int AreaId)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select * from SC_Area ");
			strSql.Append(" where AreaId=@AreaId");
			SqlParameter[] parameters = {
					new SqlParameter("@AreaId", SqlDbType.Int,4)};
			parameters[0].Value = AreaId;
			SC_Area model=new SC_Area();
			DataSet ds=DbHelperSQL.Query(strSql.ToString(),parameters);
			model.AreaId=AreaId;
			if(ds.Tables[0].Rows.Count>0)
			{
				model.AreaDes=ds.Tables[0].Rows[0]["AreaDes"].ToString();
				if(ds.Tables[0].Rows[0]["AreaStatus"].ToString()!="")
				{
					model.AreaStatus=int.Parse(ds.Tables[0].Rows[0]["AreaStatus"].ToString());
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
			strSql.Append("select [AreaId],[AreaDes],[AreaStatus] ");
			strSql.Append(" FROM SC_Area ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			return DbHelperSQL.Query(strSql.ToString());
		}

        public List<SC_Area> GetList(string strWhere, SqlParameter[] parameters)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append(" FROM SC_Area ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return ESP.ConfigCommon.CBO.FillCollection<SC_Area>(DbHelperSQL.Query(strSql.ToString(), parameters));
        }

        public List<SC_Area> GetAllLists()
        {
            return ESP.ConfigCommon.CBO.FillCollection<SC_Area>(GetList(""));
        }

		#endregion  成员方法
    }
}
