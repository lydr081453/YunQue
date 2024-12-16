using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using ESP.Finance.Utility;


namespace ESP.Finance.DataAccess
{
	/// <summary>
	/// 数据访问类SupporterScheduleInfo。
	/// </summary>
	internal class SupporterScheduleDataProvider : ESP.Finance.IDataAccess.ISupporterScheduleDataProvider
	{
        
		#region  成员方法

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(Entity.SupporterScheduleInfo model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into F_SupporterSchedule(");
			strSql.Append("ProjectID,ProjectCode,SupporterID,YearValue,monthValue,MonthPercent,Fee)");
			strSql.Append(" values (");
			strSql.Append("@ProjectID,@ProjectCode,@SupporterID,@YearValue,@monthValue,@MonthPercent,@Fee)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = {
					new SqlParameter("@ProjectID", SqlDbType.Int,4),
					new SqlParameter("@ProjectCode", SqlDbType.NVarChar,50),
					new SqlParameter("@SupporterID", SqlDbType.Int,4),
					new SqlParameter("@YearValue", SqlDbType.Int,4),
					new SqlParameter("@monthValue", SqlDbType.Int,4),
					new SqlParameter("@MonthPercent", SqlDbType.Decimal,9),
					new SqlParameter("@Fee", SqlDbType.Decimal,9)};
			parameters[0].Value =model.ProjectID;
			parameters[1].Value =model.ProjectCode;
			parameters[2].Value =model.SupporterID;
			parameters[3].Value =model.YearValue;
			parameters[4].Value =model.monthValue;
			parameters[5].Value =model.MonthPercent;
			parameters[6].Value =model.Fee;

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
		public int Update(Entity.SupporterScheduleInfo model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update F_SupporterSchedule set ");
			strSql.Append("ProjectID=@ProjectID,");
			strSql.Append("ProjectCode=@ProjectCode,");
			strSql.Append("SupporterID=@SupporterID,");
			strSql.Append("YearValue=@YearValue,");
			strSql.Append("monthValue=@monthValue,");
			strSql.Append("MonthPercent=@MonthPercent,");
			strSql.Append("Fee=@Fee");
			strSql.Append(" where SupporterScheduleID=@SupporterScheduleID ");
			SqlParameter[] parameters = {
					new SqlParameter("@SupporterScheduleID", SqlDbType.Int,4),
					new SqlParameter("@ProjectID", SqlDbType.Int,4),
					new SqlParameter("@ProjectCode", SqlDbType.NVarChar,50),
					new SqlParameter("@SupporterID", SqlDbType.Int,4),
					new SqlParameter("@YearValue", SqlDbType.Int,4),
					new SqlParameter("@monthValue", SqlDbType.Int,4),
					new SqlParameter("@MonthPercent", SqlDbType.Decimal,9),
					new SqlParameter("@Fee", SqlDbType.Decimal,9)};
			parameters[0].Value =model.SupporterScheduleID;
			parameters[1].Value =model.ProjectID;
			parameters[2].Value =model.ProjectCode;
			parameters[3].Value =model.SupporterID;
			parameters[4].Value =model.YearValue;
			parameters[5].Value =model.monthValue;
			parameters[6].Value =model.MonthPercent;
			parameters[7].Value =model.Fee;

			return DbHelperSQL.ExecuteSql(strSql.ToString(),parameters);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public int Delete(int SupporterScheduleID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete F_SupporterSchedule ");
			strSql.Append(" where SupporterScheduleID=@SupporterScheduleID ");
			SqlParameter[] parameters = {
					new SqlParameter("@SupporterScheduleID", SqlDbType.Int,4)};
			parameters[0].Value = SupporterScheduleID;

			return DbHelperSQL.ExecuteSql(strSql.ToString(),parameters);
		}


		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public Entity.SupporterScheduleInfo GetModel(int SupporterScheduleID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 SupporterScheduleID,ProjectID,ProjectCode,SupporterID,YearValue,monthValue,MonthPercent,Fee from F_SupporterSchedule ");
			strSql.Append(" where SupporterScheduleID=@SupporterScheduleID ");
			SqlParameter[] parameters = {
					new SqlParameter("@SupporterScheduleID", SqlDbType.Int,4)};
			parameters[0].Value = SupporterScheduleID;

            return CBO.FillObject<Entity.SupporterScheduleInfo>(DbHelperSQL.Query(strSql.ToString(), parameters));
		}

		/// <summary>
		/// 获得数据列表
		/// </summary>
        public IList<Entity.SupporterScheduleInfo> GetList(string term, List<SqlParameter> param)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select SupporterScheduleID,ProjectID,ProjectCode,SupporterID,YearValue,monthValue,MonthPercent,Fee ");
			strSql.Append(" FROM F_SupporterSchedule ");
            if (!string.IsNullOrEmpty(term))
            {
                strSql.Append(" where " + term);
            }
            //if (param != null && param.Count > 0)
            //{
            //    SqlParameter[] ps = param.ToArray();

            //    return CBO.FillCollection<Entity.SupporterScheduleInfo>(DbHelperSQL.ExecuteReader(strSql.ToString(), ps));
            //}
            return CBO.FillCollection<Entity.SupporterScheduleInfo>(DbHelperSQL.Query(strSql.ToString(),param));
        }

        public IList<ESP.Finance.Entity.SupporterScheduleInfo> GetList(int supportId, SqlTransaction trans)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select SupporterScheduleID,ProjectID,ProjectCode,SupporterID,YearValue,monthValue,MonthPercent,Fee ");
            strSql.Append(" FROM F_SupporterSchedule where SupporterID="+supportId.ToString());

            return CBO.FillCollection<Entity.SupporterScheduleInfo>(DbHelperSQL.Query(strSql.ToString(), trans));
        }


		#endregion  成员方法
	}
}

