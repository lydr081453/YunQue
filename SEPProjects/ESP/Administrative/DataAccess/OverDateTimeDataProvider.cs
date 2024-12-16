using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using ESP.Administrative.Common;
using ESP.Administrative.Entity;

namespace ESP.Administrative.DataAccess
{
    public class OverDateTimeDataProvider
    {
        public OverDateTimeDataProvider()
		{}
		#region  成员方法
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int ID)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from AD_OverDateTimeInfo");
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
		public int Add(OverDateTimeInfo model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into AD_OverDateTimeInfo(");
			strSql.Append("SingleOvertimeID,OverTimeDateTime,BeginTime,EndTime,OverTimeHours,OverTimeCause,Deleted,CreateTime,UpdateTime,OperatorID,OperatorDeptID)");
			strSql.Append(" values (");
			strSql.Append("@SingleOvertimeID,@OverTimeDateTime,@BeginTime,@EndTime,@OverTimeHours,@OverTimeCause,@Deleted,@CreateTime,@UpdateTime,@OperatorID,@OperatorDeptID)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = {
					new SqlParameter("@SingleOvertimeID", SqlDbType.Int,4),
					new SqlParameter("@OverTimeDateTime", SqlDbType.DateTime),
					new SqlParameter("@BeginTime", SqlDbType.NVarChar),
					new SqlParameter("@EndTime", SqlDbType.NVarChar),
					new SqlParameter("@OverTimeHours", SqlDbType.Decimal,9),
					new SqlParameter("@OverTimeCause", SqlDbType.NVarChar),
					new SqlParameter("@Deleted", SqlDbType.Bit,1),
					new SqlParameter("@CreateTime", SqlDbType.DateTime),
					new SqlParameter("@UpdateTime", SqlDbType.DateTime),
					new SqlParameter("@OperatorID", SqlDbType.Int,4),
					new SqlParameter("@OperatorDeptID", SqlDbType.Int,4)};
			parameters[0].Value = model.SingleOvertimeID;
			parameters[1].Value = model.OverTimeDateTime;
			parameters[2].Value = model.BeginTime;
			parameters[3].Value = model.EndTime;
			parameters[4].Value = model.OverTimeHours;
			parameters[5].Value = model.OverTimeCause;
			parameters[6].Value = model.Deleted;
			parameters[7].Value = model.CreateTime;
			parameters[8].Value = model.UpdateTime;
			parameters[9].Value = model.OperateorID;
			parameters[10].Value = model.OperateorDept;

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
		public void Update(OverDateTimeInfo model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update AD_OverDateTimeInfo set ");
			strSql.Append("SingleOvertimeID=@SingleOvertimeID,");
			strSql.Append("OverTimeDateTime=@OverTimeDateTime,");
			strSql.Append("BeginTime=@BeginTime,");
			strSql.Append("EndTime=@EndTime,");
			strSql.Append("OverTimeHours=@OverTimeHours,");
			strSql.Append("OverTimeCause=@OverTimeCause,");
			strSql.Append("Deleted=@Deleted,");
			strSql.Append("CreateTime=@CreateTime,");
			strSql.Append("UpdateTime=@UpdateTime,");
			strSql.Append("OperatorID=@OperatorID,");
			strSql.Append("OperatorDeptID=@OperatorDeptID");
			strSql.Append(" where ID=@ID");
			SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4),
					new SqlParameter("@SingleOvertimeID", SqlDbType.Int,4),
					new SqlParameter("@OverTimeDateTime", SqlDbType.DateTime),
					new SqlParameter("@BeginTime", SqlDbType.NVarChar),
					new SqlParameter("@EndTime", SqlDbType.NVarChar),
					new SqlParameter("@OverTimeHours", SqlDbType.Decimal,9),
					new SqlParameter("@OverTimeCause", SqlDbType.NVarChar),
					new SqlParameter("@Deleted", SqlDbType.Bit,1),
					new SqlParameter("@CreateTime", SqlDbType.DateTime),
					new SqlParameter("@UpdateTime", SqlDbType.DateTime),
					new SqlParameter("@OperatorID", SqlDbType.Int,4),
					new SqlParameter("@OperatorDeptID", SqlDbType.Int,4)};
			parameters[0].Value = model.ID;
			parameters[1].Value = model.SingleOvertimeID;
			parameters[2].Value = model.OverTimeDateTime;
			parameters[3].Value = model.BeginTime;
			parameters[4].Value = model.EndTime;
			parameters[5].Value = model.OverTimeHours;
			parameters[6].Value = model.OverTimeCause;
			parameters[7].Value = model.Deleted;
			parameters[8].Value = model.CreateTime;
			parameters[9].Value = model.UpdateTime;
			parameters[10].Value = model.OperateorID;
			parameters[11].Value = model.OperateorDept;

			DbHelperSQL.ExecuteSql(strSql.ToString(),parameters);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public void Delete(int ID)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete AD_OverDateTimeInfo ");
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
		public OverDateTimeInfo GetModel(int ID)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select * from AD_OverDateTimeInfo ");
			strSql.Append(" where ID=@ID");
			SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)};
			parameters[0].Value = ID;
			OverDateTimeInfo model=new OverDateTimeInfo();
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
			strSql.Append("select * FROM AD_OverDateTimeInfo ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			return DbHelperSQL.Query(strSql.ToString());
		}
		#endregion  成员方法
	}
}