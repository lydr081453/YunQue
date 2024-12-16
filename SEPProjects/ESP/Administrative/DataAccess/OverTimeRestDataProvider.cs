using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESP.Administrative.Entity;
using ESP.Administrative.Common;
using System.Data.SqlClient;
using System.Data;

namespace ESP.Administrative.DataAccess
{
    public class OverTimeRestDataProvider
    {
        public OverTimeRestDataProvider()
		{}
		#region  成员方法
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int ID)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from AD_OverTimeRest");
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
		public int Add(OverTimeRestInfo model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into AD_OverTimeRest(");
            strSql.Append("OverTimeID,MatterID,UseOverTimeHours)");
			strSql.Append(" values (");
            strSql.Append("@OverTimeID,@MatterID,@UseOverTimeHours)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = {
					new SqlParameter("@OverTimeID", SqlDbType.Int,4),
					new SqlParameter("@MatterID", SqlDbType.Int,4),
                    new SqlParameter("@UseOverTimeHours", SqlDbType.Int, 4)};
			parameters[0].Value = model.OverTimeID;
			parameters[1].Value = model.MatterID;
            parameters[2].Value = model.Useovertimehours;

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
        public void Update(OverTimeRestInfo model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update AD_OverTimeRest set ");
			strSql.Append("OverTimeID=@OverTimeID,");
			strSql.Append("MatterID=@MatterID,");
            strSql.Append("UseOverTimeHours=@UseOverTimeHours");
			strSql.Append(" where ID=@ID");
			SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4),
					new SqlParameter("@OverTimeID", SqlDbType.Int,4),
					new SqlParameter("@MatterID", SqlDbType.Int,4),
                    new SqlParameter("@UseOverTimeHours", SqlDbType.Int, 4)};
			parameters[0].Value = model.ID;
			parameters[1].Value = model.OverTimeID;
			parameters[2].Value = model.MatterID;
            parameters[3].Value = model.Useovertimehours;

			DbHelperSQL.ExecuteSql(strSql.ToString(),parameters);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public void Delete(int ID)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete AD_OverTimeRest ");
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
        public OverTimeRestInfo GetModel(int ID)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select * from AD_OverTimeRest ");
			strSql.Append(" where ID=@ID");
			SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)};
			parameters[0].Value = ID;
            OverTimeRestInfo model = new OverTimeRestInfo();
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
			strSql.Append(" FROM AD_OverTimeRest ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			return DbHelperSQL.Query(strSql.ToString());
		}

        /// <summary>
        /// 通过调休单编号删除相对应的调休OT关联时间信息
        /// </summary>
        /// <param name="offTuneId">调休单编号</param>
        public void DeleteOffTuneInfos(int offTuneId)
        {
            string sql = "delete from AD_OverTimeRest where MatterID=@MatterID";
            DbHelperSQL.ExecuteSql(sql, new SqlParameter[] { 
                    new SqlParameter("@MatterID", offTuneId) });

        }
		#endregion  成员方法
	}
}