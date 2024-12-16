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
    public class SingleOvertimeDataProvider
    {
        public SingleOvertimeDataProvider()
		{}
//        #region  成员方法
//        /// <summary>
//        /// 是否存在该记录
//        /// </summary>
//        public bool Exists(int ID)
//        {
//            StringBuilder strSql=new StringBuilder();
//            strSql.Append("select count(1) from AD_SingleOvertime");
//            strSql.Append(" where ID= @ID");
//            SqlParameter[] parameters = {
//                    new SqlParameter("@ID", SqlDbType.Int,4)
//                };
//            parameters[0].Value = ID;
//            return DbHelperSQL.Exists(strSql.ToString(),parameters);
//        }


//        /// <summary>
//        /// 增加一条数据
//        /// </summary>
//        public int Add(SingleOvertimeInfo model)
//        {
//            StringBuilder strSql=new StringBuilder();
//            strSql.Append("insert into AD_SingleOvertime(");
//            strSql.Append("UserID,UserCode,EmployeeName,AppTime,OverTimeType,ProjectID,ProjectNo,ApproveID,ApproveRemark,Deleted,CreateTime,UpdateTime,OperateorID,OperateorDept,Sort,ApproveName,State)");
//            strSql.Append(" values (");
//            strSql.Append("@UserID,@UserCode,@EmployeeName,@AppTime,@OverTimeType,@ProjectID,@ProjectNo,@ApproveID,@ApproveRemark,@Deleted,@CreateTime,@UpdateTime,@OperateorID,@OperateorDept,@Sort,@ApproveName,@State)");
//            strSql.Append(";select @@IDENTITY");
//            SqlParameter[] parameters = {
//                    new SqlParameter("@UserID", SqlDbType.Int,4),
//                    new SqlParameter("@UserCode", SqlDbType.NVarChar),
//                    new SqlParameter("@EmployeeName", SqlDbType.NVarChar),
//                    new SqlParameter("@AppTime", SqlDbType.DateTime),
//                    new SqlParameter("@OverTimeType", SqlDbType.Int,4),
//                    new SqlParameter("@ProjectID", SqlDbType.Int,4),
//                    new SqlParameter("@ProjectNo", SqlDbType.NVarChar),
//                    new SqlParameter("@ApproveID", SqlDbType.Int,4),
//                    new SqlParameter("@ApproveRemark", SqlDbType.NVarChar),
//                    new SqlParameter("@Deleted", SqlDbType.Bit,1),
//                    new SqlParameter("@CreateTime", SqlDbType.DateTime),
//                    new SqlParameter("@UpdateTime", SqlDbType.DateTime),
//                    new SqlParameter("@OperateorID", SqlDbType.Int,4),
//                    new SqlParameter("@OperateorDept", SqlDbType.Int,4),
//                    new SqlParameter("@Sort", SqlDbType.Int,4),
//                    new SqlParameter("@ApproveName", SqlDbType.NVarChar),
//                    new SqlParameter("@State", SqlDbType.Int, 4)};
//            parameters[0].Value = model.UserID;
//            parameters[1].Value = model.UserCode;
//            parameters[2].Value = model.EmployeeName;
//            parameters[3].Value = model.AppTime;
//            parameters[4].Value = model.OverTimeType;
//            parameters[5].Value = model.ProjectID;
//            parameters[6].Value = model.ProjectNo;
//            parameters[7].Value = model.ApproveID;
//            parameters[8].Value = model.ApproveRemark;
//            parameters[9].Value = model.Deleted;
//            parameters[10].Value = model.CreateTime;
//            parameters[11].Value = model.UpdateTime;
//            parameters[12].Value = model.OperateorID;
//            parameters[13].Value = model.OperateorDept;
//            parameters[14].Value = model.Sort;
//            parameters[15].Value = model.Approvename;
//            parameters[16].Value = model.State;

//            object obj = DbHelperSQL.GetSingle(strSql.ToString(),parameters);
//            if (obj == null)
//            {
//                return 1;
//            }
//            else
//            {
//                return Convert.ToInt32(obj);
//            }
//        }
//        /// <summary>
//        /// 更新一条数据
//        /// </summary>
//        public void Update(SingleOvertimeInfo model)
//        {
//            StringBuilder strSql=new StringBuilder();
//            strSql.Append("update AD_SingleOvertime set ");
//            strSql.Append("UserID=@UserID,");
//            strSql.Append("UserCode=@UserCode,");
//            strSql.Append("EmployeeName=@EmployeeName,");
//            strSql.Append("AppTime=@AppTime,");
//            strSql.Append("OverTimeType=@OverTimeType,");
//            strSql.Append("ProjectID=@ProjectID,");
//            strSql.Append("ProjectNo=@ProjectNo,");
//            strSql.Append("ApproveID=@ApproveID,");
//            strSql.Append("ApproveRemark=@ApproveRemark,");
//            strSql.Append("Deleted=@Deleted,");
//            strSql.Append("CreateTime=@CreateTime,");
//            strSql.Append("UpdateTime=@UpdateTime,");
//            strSql.Append("OperateorID=@OperateorID,");
//            strSql.Append("OperateorDept=@OperateorDept,");
//            strSql.Append("Sort=@Sort,");
//            strSql.Append("Approvename = @Approvename,");
//            strSql.Append("State = @State");
//            strSql.Append(" where ID=@ID");
//            SqlParameter[] parameters = {
//                    new SqlParameter("@ID", SqlDbType.Int,4),
//                    new SqlParameter("@UserID", SqlDbType.Int,4),
//                    new SqlParameter("@UserCode", SqlDbType.NVarChar),
//                    new SqlParameter("@EmployeeName", SqlDbType.NVarChar),
//                    new SqlParameter("@AppTime", SqlDbType.DateTime),
//                    new SqlParameter("@OverTimeType", SqlDbType.Int,4),
//                    new SqlParameter("@ProjectID", SqlDbType.Int,4),
//                    new SqlParameter("@ProjectNo", SqlDbType.NVarChar),
//                    new SqlParameter("@ApproveID", SqlDbType.Int,4),
//                    new SqlParameter("@ApproveRemark", SqlDbType.NVarChar),
//                    new SqlParameter("@Deleted", SqlDbType.Bit,1),
//                    new SqlParameter("@CreateTime", SqlDbType.DateTime),
//                    new SqlParameter("@UpdateTime", SqlDbType.DateTime),
//                    new SqlParameter("@OperateorID", SqlDbType.Int,4),
//                    new SqlParameter("@OperateorDept", SqlDbType.Int,4),
//                    new SqlParameter("@Sort", SqlDbType.Int,4),
//                    new SqlParameter("@Approvename", SqlDbType.NVarChar),
//                    new SqlParameter("@State", SqlDbType.Int, 4)};
//            parameters[0].Value = model.ID;
//            parameters[1].Value = model.UserID;
//            parameters[2].Value = model.UserCode;
//            parameters[3].Value = model.EmployeeName;
//            parameters[4].Value = model.AppTime;
//            parameters[5].Value = model.OverTimeType;
//            parameters[6].Value = model.ProjectID;
//            parameters[7].Value = model.ProjectNo;
//            parameters[8].Value = model.ApproveID;
//            parameters[9].Value = model.ApproveRemark;
//            parameters[10].Value = model.Deleted;
//            parameters[11].Value = model.CreateTime;
//            parameters[12].Value = model.UpdateTime;
//            parameters[13].Value = model.OperateorID;
//            parameters[14].Value = model.OperateorDept;
//            parameters[15].Value = model.Sort;
//            parameters[16].Value = model.Approvename;
//            parameters[17].Value = model.State;

//            DbHelperSQL.ExecuteSql(strSql.ToString(),parameters);
//        }

//        /// <summary>
//        /// 删除一条数据
//        /// </summary>
//        public void Delete(int ID)
//        {
//            StringBuilder strSql=new StringBuilder();
//            strSql.Append("delete AD_SingleOvertime ");
//            strSql.Append(" where ID=@ID");
//            SqlParameter[] parameters = {
//                    new SqlParameter("@ID", SqlDbType.Int,4)
//                };
//            parameters[0].Value = ID;
//            DbHelperSQL.ExecuteSql(strSql.ToString(),parameters);
//        }


//        /// <summary>
//        /// 得到一个对象实体
//        /// </summary>
//        public SingleOvertimeInfo GetModel(int ID)
//        {
//            StringBuilder strSql=new StringBuilder();
//            strSql.Append("select * from AD_SingleOvertime ");
//            strSql.Append(" where ID=@ID");
//            SqlParameter[] parameters = {
//                    new SqlParameter("@ID", SqlDbType.Int,4)};
//            parameters[0].Value = ID;
//            SingleOvertimeInfo model = new SingleOvertimeInfo();
//            DataSet ds=DbHelperSQL.Query(strSql.ToString(),parameters);
//            model.ID=ID;
//            if(ds.Tables[0].Rows.Count>0)
//            {
//                model.PopupData(ds.Tables[0].Rows[0]);
//                return model;
//            }
//            else
//            {
//            return null;
//            }
//        }
//        /// <summary>
//        /// 获得数据列表
//        /// </summary>
//        public DataSet GetList(string strWhere)
//        {
//            StringBuilder strSql=new StringBuilder();
//            strSql.Append("select * FROM AD_SingleOvertime ");
//            if(strWhere.Trim()!="")
//            {
//                strSql.Append(" where "+strWhere);
//            }
//            return DbHelperSQL.Query(strSql.ToString());
//        }

        /// <summary>
        /// 获得OT单数据信息，这个查询是一个关联查询，OT单表的别名是：s，OT时间信息表的别名是：o
        /// </summary>
        /// <param name="strWhere">查询条件</param>
        /// <returns>返回一个OT查询的集合</returns>
        public DataSet GetSingleOverTimeInfo(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"select s.id as SingleID, 
                            replace(convert(char, o.overtimedatetime,111),'/','-') + ' ' + o.begintime as overbegintime,
                            replace(convert(char, o.overtimedatetime,111),'/','-') + ' ' + o.endtime as overendtime,
                            * from ad_overdatetimeinfo o left join 
                            ad_singleovertime s on o.singleovertimeid = s.id ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }
//        #endregion  成员方法

		#region  成员方法
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int ID)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from AD_SingleOvertime");
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
		public int Add(SingleOvertimeInfo model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into AD_SingleOvertime(");
            strSql.Append("UserID,UserCode,EmployeeName,AppTime,BeginTime,EndTime,OverTimeHours,OverTimeCause,OverTimeType,ProjectID,ProjectNo,ApproveID,ApproveName,ApproveRemark,State,Deleted,CreateTime,UpdateTime,OperateorID,OperateorDept,Sort,ApproveState,RemainingHours)");
			strSql.Append(" values (");
            strSql.Append("@UserID,@UserCode,@EmployeeName,@AppTime,@BeginTime,@EndTime,@OverTimeHours,@OverTimeCause,@OverTimeType,@ProjectID,@ProjectNo,@ApproveID,@ApproveName,@ApproveRemark,@State,@Deleted,@CreateTime,@UpdateTime,@OperateorID,@OperateorDept,@Sort,@ApproveState,@RemainingHours)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = {
					new SqlParameter("@UserID", SqlDbType.Int,4),
					new SqlParameter("@UserCode", SqlDbType.NVarChar),
					new SqlParameter("@EmployeeName", SqlDbType.NVarChar),
					new SqlParameter("@AppTime", SqlDbType.DateTime),
					new SqlParameter("@BeginTime", SqlDbType.DateTime),
					new SqlParameter("@EndTime", SqlDbType.DateTime),
					new SqlParameter("@OverTimeHours", SqlDbType.Decimal,9),
					new SqlParameter("@OverTimeCause", SqlDbType.NVarChar),
					new SqlParameter("@OverTimeType", SqlDbType.Int,4),
					new SqlParameter("@ProjectID", SqlDbType.Int,4),
					new SqlParameter("@ProjectNo", SqlDbType.NVarChar),
					new SqlParameter("@ApproveID", SqlDbType.Int,4),
					new SqlParameter("@ApproveName", SqlDbType.NVarChar),
					new SqlParameter("@ApproveRemark", SqlDbType.NVarChar),
					new SqlParameter("@State", SqlDbType.Int,4),
					new SqlParameter("@Deleted", SqlDbType.Bit,1),
					new SqlParameter("@CreateTime", SqlDbType.DateTime),
					new SqlParameter("@UpdateTime", SqlDbType.DateTime),
					new SqlParameter("@OperateorID", SqlDbType.Int,4),
					new SqlParameter("@OperateorDept", SqlDbType.Int,4),
					new SqlParameter("@Sort", SqlDbType.Int,4),
                    new SqlParameter("@ApproveState", SqlDbType.Int,4),
                    new SqlParameter("@RemainingHours", SqlDbType.Decimal)};
			parameters[0].Value = model.UserID;
			parameters[1].Value = model.UserCode;
			parameters[2].Value = model.EmployeeName;
			parameters[3].Value = model.AppTime;
			parameters[4].Value = model.BeginTime;
			parameters[5].Value = model.EndTime;
			parameters[6].Value = model.OverTimeHours;
			parameters[7].Value = model.OverTimeCause;
			parameters[8].Value = model.OverTimeType;
			parameters[9].Value = model.ProjectID;
			parameters[10].Value = model.ProjectNo;
			parameters[11].Value = model.ApproveID;
			parameters[12].Value = model.ApproveName;
			parameters[13].Value = model.ApproveRemark;
			parameters[14].Value = model.State;
			parameters[15].Value = model.Deleted;
			parameters[16].Value = model.CreateTime;
			parameters[17].Value = model.UpdateTime;
			parameters[18].Value = model.OperateorID;
			parameters[19].Value = model.OperateorDept;
			parameters[20].Value = model.Sort;
            parameters[21].Value = model.Approvestate;
            parameters[22].Value = model.Remaininghours;

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
		public void Update(SingleOvertimeInfo model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update AD_SingleOvertime set ");
			strSql.Append("UserID=@UserID,");
			strSql.Append("UserCode=@UserCode,");
			strSql.Append("EmployeeName=@EmployeeName,");
			strSql.Append("AppTime=@AppTime,");
			strSql.Append("BeginTime=@BeginTime,");
			strSql.Append("EndTime=@EndTime,");
			strSql.Append("OverTimeHours=@OverTimeHours,");
			strSql.Append("OverTimeCause=@OverTimeCause,");
			strSql.Append("OverTimeType=@OverTimeType,");
			strSql.Append("ProjectID=@ProjectID,");
			strSql.Append("ProjectNo=@ProjectNo,");
			strSql.Append("ApproveID=@ApproveID,");
			strSql.Append("ApproveName=@ApproveName,");
			strSql.Append("ApproveRemark=@ApproveRemark,");
			strSql.Append("State=@State,");
			strSql.Append("Deleted=@Deleted,");
			strSql.Append("CreateTime=@CreateTime,");
			strSql.Append("UpdateTime=@UpdateTime,");
			strSql.Append("OperateorID=@OperateorID,");
			strSql.Append("OperateorDept=@OperateorDept,");
			strSql.Append("Sort=@Sort,");
            strSql.Append("ApproveState=@ApproveState,");
            strSql.Append("RemainingHours=@RemainingHours");
			strSql.Append(" where ID=@ID");
			SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4),
					new SqlParameter("@UserID", SqlDbType.Int,4),
					new SqlParameter("@UserCode", SqlDbType.NVarChar),
					new SqlParameter("@EmployeeName", SqlDbType.NVarChar),
					new SqlParameter("@AppTime", SqlDbType.DateTime),
					new SqlParameter("@BeginTime", SqlDbType.DateTime),
					new SqlParameter("@EndTime", SqlDbType.DateTime),
					new SqlParameter("@OverTimeHours", SqlDbType.Decimal,9),
					new SqlParameter("@OverTimeCause", SqlDbType.NVarChar),
					new SqlParameter("@OverTimeType", SqlDbType.Int,4),
					new SqlParameter("@ProjectID", SqlDbType.Int,4),
					new SqlParameter("@ProjectNo", SqlDbType.NVarChar),
					new SqlParameter("@ApproveID", SqlDbType.Int,4),
					new SqlParameter("@ApproveName", SqlDbType.NVarChar),
					new SqlParameter("@ApproveRemark", SqlDbType.NVarChar),
					new SqlParameter("@State", SqlDbType.Int,4),
					new SqlParameter("@Deleted", SqlDbType.Bit,1),
					new SqlParameter("@CreateTime", SqlDbType.DateTime),
					new SqlParameter("@UpdateTime", SqlDbType.DateTime),
					new SqlParameter("@OperateorID", SqlDbType.Int,4),
					new SqlParameter("@OperateorDept", SqlDbType.Int,4),
					new SqlParameter("@Sort", SqlDbType.Int,4),
                    new SqlParameter("@ApproveState", SqlDbType.Int,4),
                    new SqlParameter("@RemainingHours", SqlDbType.Decimal)};
			parameters[0].Value = model.ID;
			parameters[1].Value = model.UserID;
			parameters[2].Value = model.UserCode;
			parameters[3].Value = model.EmployeeName;
			parameters[4].Value = model.AppTime;
			parameters[5].Value = model.BeginTime;
			parameters[6].Value = model.EndTime;
			parameters[7].Value = model.OverTimeHours;
			parameters[8].Value = model.OverTimeCause;
			parameters[9].Value = model.OverTimeType;
			parameters[10].Value = model.ProjectID;
			parameters[11].Value = model.ProjectNo;
			parameters[12].Value = model.ApproveID;
			parameters[13].Value = model.ApproveName;
			parameters[14].Value = model.ApproveRemark;
			parameters[15].Value = model.State;
			parameters[16].Value = model.Deleted;
			parameters[17].Value = model.CreateTime;
			parameters[18].Value = model.UpdateTime;
			parameters[19].Value = model.OperateorID;
			parameters[20].Value = model.OperateorDept;
			parameters[21].Value = model.Sort;
            parameters[22].Value = model.Approvestate;
            parameters[23].Value = model.Remaininghours;

			DbHelperSQL.ExecuteSql(strSql.ToString(),parameters);
		}

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public void Update(SingleOvertimeInfo model, SqlTransaction stran)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update AD_SingleOvertime set ");
            strSql.Append("UserID=@UserID,");
            strSql.Append("UserCode=@UserCode,");
            strSql.Append("EmployeeName=@EmployeeName,");
            strSql.Append("AppTime=@AppTime,");
            strSql.Append("BeginTime=@BeginTime,");
            strSql.Append("EndTime=@EndTime,");
            strSql.Append("OverTimeHours=@OverTimeHours,");
            strSql.Append("OverTimeCause=@OverTimeCause,");
            strSql.Append("OverTimeType=@OverTimeType,");
            strSql.Append("ProjectID=@ProjectID,");
            strSql.Append("ProjectNo=@ProjectNo,");
            strSql.Append("ApproveID=@ApproveID,");
            strSql.Append("ApproveName=@ApproveName,");
            strSql.Append("ApproveRemark=@ApproveRemark,");
            strSql.Append("State=@State,");
            strSql.Append("Deleted=@Deleted,");
            strSql.Append("CreateTime=@CreateTime,");
            strSql.Append("UpdateTime=@UpdateTime,");
            strSql.Append("OperateorID=@OperateorID,");
            strSql.Append("OperateorDept=@OperateorDept,");
            strSql.Append("Sort=@Sort,");
            strSql.Append("ApproveState=@ApproveState,");
            strSql.Append("RemainingHours=@RemainingHours");
            strSql.Append(" where ID=@ID");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4),
					new SqlParameter("@UserID", SqlDbType.Int,4),
					new SqlParameter("@UserCode", SqlDbType.NVarChar),
					new SqlParameter("@EmployeeName", SqlDbType.NVarChar),
					new SqlParameter("@AppTime", SqlDbType.DateTime),
					new SqlParameter("@BeginTime", SqlDbType.DateTime),
					new SqlParameter("@EndTime", SqlDbType.DateTime),
					new SqlParameter("@OverTimeHours", SqlDbType.Decimal,9),
					new SqlParameter("@OverTimeCause", SqlDbType.NVarChar),
					new SqlParameter("@OverTimeType", SqlDbType.Int,4),
					new SqlParameter("@ProjectID", SqlDbType.Int,4),
					new SqlParameter("@ProjectNo", SqlDbType.NVarChar),
					new SqlParameter("@ApproveID", SqlDbType.Int,4),
					new SqlParameter("@ApproveName", SqlDbType.NVarChar),
					new SqlParameter("@ApproveRemark", SqlDbType.NVarChar),
					new SqlParameter("@State", SqlDbType.Int,4),
					new SqlParameter("@Deleted", SqlDbType.Bit,1),
					new SqlParameter("@CreateTime", SqlDbType.DateTime),
					new SqlParameter("@UpdateTime", SqlDbType.DateTime),
					new SqlParameter("@OperateorID", SqlDbType.Int,4),
					new SqlParameter("@OperateorDept", SqlDbType.Int,4),
					new SqlParameter("@Sort", SqlDbType.Int,4),
                    new SqlParameter("@ApproveState", SqlDbType.Int,4),
                    new SqlParameter("@RemainingHours", SqlDbType.Decimal)};
            parameters[0].Value = model.ID;
            parameters[1].Value = model.UserID;
            parameters[2].Value = model.UserCode;
            parameters[3].Value = model.EmployeeName;
            parameters[4].Value = model.AppTime;
            parameters[5].Value = model.BeginTime;
            parameters[6].Value = model.EndTime;
            parameters[7].Value = model.OverTimeHours;
            parameters[8].Value = model.OverTimeCause;
            parameters[9].Value = model.OverTimeType;
            parameters[10].Value = model.ProjectID;
            parameters[11].Value = model.ProjectNo;
            parameters[12].Value = model.ApproveID;
            parameters[13].Value = model.ApproveName;
            parameters[14].Value = model.ApproveRemark;
            parameters[15].Value = model.State;
            parameters[16].Value = model.Deleted;
            parameters[17].Value = model.CreateTime;
            parameters[18].Value = model.UpdateTime;
            parameters[19].Value = model.OperateorID;
            parameters[20].Value = model.OperateorDept;
            parameters[21].Value = model.Sort;
            parameters[22].Value = model.Approvestate;
            parameters[23].Value = model.Remaininghours;

            DbHelperSQL.ExecuteSql(strSql.ToString(), stran.Connection, stran, parameters);
        }

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public void Delete(int ID)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete AD_SingleOvertime ");
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
		public SingleOvertimeInfo GetModel(int ID)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select * from AD_SingleOvertime ");
			strSql.Append(" where ID=@ID");
			SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)};
			parameters[0].Value = ID;
			SingleOvertimeInfo model=new SingleOvertimeInfo();
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
			strSql.Append("select * FROM AD_SingleOvertime ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			return DbHelperSQL.Query(strSql.ToString());
		}
		#endregion  成员方法


        public IList<SingleOvertimeInfo> GetModelListByMonth(int userId, int year, int month)
        {
            string sql = @"
                DECLARE @RangeStart DATETIME
                DECLARE @RangeEnd DATETIME

                SET @RangeStart = CAST(@Year AS nvarchar(10)) + N'-' + CAST(@Month AS nvarchar(10)) + '-1'
                SET @RangeEnd = DATEADD(month, 1, @RangeStart)

                SELECT * FROM AD_SingleOvertime
                WHERE UserId= @UserId AND Deleted='false' 
	                AND ((BeginTime >= @RangeStart AND BeginTime < @RangeEnd)
	                OR (EndTime >= @RangeStart AND EndTime < @RangeEnd))
                ";
            IDataReader reader = DbHelperSQL.ExecuteReader(sql, new SqlParameter[]{
                new SqlParameter("@UserId", userId),
                new SqlParameter("@Year", year),
                new SqlParameter("@Month", month)
            });

            return ESP.Framework.DataAccess.Utilities.CBO.LoadList<SingleOvertimeInfo>(reader);
        }

        /// <summary>
        /// 获取OT统计信息
        /// </summary>
        /// <param name="year">年</param>
        /// <param name="month">月</param>
        /// <param name="day">日</param>
        /// <returns></returns>
        public DataSet GetModelList(int year, int month, int day, string strwhere, List<SqlParameter> parameterList)
        {
            DateTime rangeStart = new DateTime(year, month, 1, 0, 0, 0);
            DateTime rangeEnd = new DateTime(year, month, DateTime.DaysInMonth(year, month), 23, 59, 59);
            if (day != 0)
            {
                rangeStart = new DateTime(year, month, day, 0, 0, 0);
                rangeEnd = new DateTime(year, month, day, 23, 59, 59);
            }
            
            string sql = @"
                        DECLARE @RangeStart DATETIME
                        DECLARE @RangeEnd DATETIME
                        SET @RangeStart = @RangeStartTime
                        SET @RangeEnd = @RangeEndTime

                        SELECT s.*, d.level3id, d.level2id, d.level1id, d.level1 + '--' + d.level2 + '--' + d.level3 AS Department,p.* FROM AD_SingleOvertime s
                        LEFT JOIN sep_EmployeesInPositions e ON s.userid = e.userid
                        LEFT JOIN sep_DepartmentPositions p on e.DepartmentPositionID=p.DepartmentPositionID
                        LEFT JOIN V_Department d ON e.departmentid = d.level3Id
                        WHERE Deleted='false' 
	                        AND ((BeginTime >= @RangeStart AND BeginTime < @RangeEnd)
	                        OR (EndTime >= @RangeStart AND EndTime < @RangeEnd)) " + strwhere;
            parameterList.Add(new SqlParameter("@RangeStartTime", rangeStart));
            parameterList.Add(new SqlParameter("@RangeEndTime", rangeEnd));

            return DbHelperSQL.Query(sql, parameterList.ToArray());
        }

        /// <summary>
        /// 获得统计OT信息，前一天的OT可以抵消第二天的迟到或者旷工半天
        /// </summary>
        /// <param name="year">年月</param>
        /// <param name="month">月份</param>
        /// <param name="day">日期</param>
        /// <param name="isApproved">是否已经审批通过，true表示审批通过，false表示审批未通过</param>
        /// <returns>返回统计的OT信息集合</returns>
        public Dictionary<int, List<SingleOvertimeInfo>> GetStatSingleOvertimeInfos(int year, int month, int day)
        {
            DateTime rangeStart = new DateTime(year, month, 1, 0, 0, 0);
            rangeStart = rangeStart.AddDays(-1);
            DateTime rangeEnd = new DateTime(year, month, DateTime.DaysInMonth(year, month), 23, 59, 59);
            rangeEnd = rangeEnd.AddDays(-1);
            if (day != 0)
            {
                rangeStart = new DateTime(year, month, day, 0, 0, 0);
                rangeEnd = new DateTime(year, month, day, 23, 59, 59);
                rangeStart = rangeStart.AddDays(-1);
                rangeEnd = rangeEnd.AddDays(-1);
            }

            string sql = @"
                DECLARE @RangeStart DATETIME
                DECLARE @RangeEnd DATETIME

                SET @RangeStart = @RangeStartTime
                SET @RangeEnd = @RangeEndTime

                SELECT * FROM AD_SingleOvertime
                WHERE Deleted='false' AND ApproveState=@ApproveState 
	                AND ((BeginTime >= @RangeStart AND BeginTime < @RangeEnd)
	                OR (EndTime >= @RangeStart AND EndTime < @RangeEnd))
                    ";
            using (IDataReader reader = DbHelperSQL.ExecuteReader(sql, new SqlParameter[]{
                new SqlParameter("@RangeStartTime", rangeStart),
                new SqlParameter("@RangeEndTime", rangeEnd),
                new SqlParameter("@ApproveState", Status.OverTimeState_Passed)
            }))
            {
                Dictionary<int, List<SingleOvertimeInfo>> dic = new Dictionary<int, List<SingleOvertimeInfo>>();
                while (reader.Read())
                {
                    int userid = reader.GetInt32(1);
                    SingleOvertimeInfo singleOvertimeModel = new SingleOvertimeInfo();
                    singleOvertimeModel.PopupData(reader);
                    if (dic.ContainsKey(userid))
                    {
                        dic[userid].Add(singleOvertimeModel);
                    }
                    else
                    {
                        List<SingleOvertimeInfo> list = new List<SingleOvertimeInfo>();
                        list.Add(singleOvertimeModel);
                        dic.Add(userid, list);
                    }
                }
                return dic;
            }
        }

        /// <summary>
        /// 获得用户当月的OT信息
        /// </summary>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <returns></returns>
        public Dictionary<int, List<SingleOvertimeInfo>> GetModelListByMonth(int year, int month)
        {
            string sql = @"
                DECLARE @RangeStart DATETIME
                DECLARE @RangeEnd DATETIME

                SET @RangeStart = CAST(@Year AS nvarchar(10)) + N'-' + CAST(@Month AS nvarchar(10)) + '-1'
                SET @RangeEnd = DATEADD(month, 1, @RangeStart)

                SELECT * FROM AD_SingleOvertime
                WHERE Deleted='false' 
	                AND ((BeginTime >= @RangeStart AND BeginTime < @RangeEnd)
	                OR (EndTime >= @RangeStart AND EndTime < @RangeEnd))
                ";
            using (IDataReader reader = DbHelperSQL.ExecuteReader(sql, new SqlParameter[]{
                new SqlParameter("@Year", year),
                new SqlParameter("@Month", month)
            }))
            {
                Dictionary<int, List<SingleOvertimeInfo>> dic = new Dictionary<int, List<SingleOvertimeInfo>>();
                while (reader.Read())
                {
                    int userid = reader.GetInt32(1);
                    SingleOvertimeInfo singleOvertimeModel = new SingleOvertimeInfo();
                    singleOvertimeModel.PopupData(reader);
                    if (dic.ContainsKey(userid))
                    {
                        dic[userid].Add(singleOvertimeModel);
                    }
                    else
                    {
                        List<SingleOvertimeInfo> list = new List<SingleOvertimeInfo>();
                        list.Add(singleOvertimeModel);
                        dic.Add(userid, list);
                    }
                }
                return dic;
            }
        }

        public List<SingleOvertimeInfo> GetSingleOvertimeByDepid(int depid)
        {
            string sql = @"SELECT s.* FROM ad_singleovertime s
left join sep_employees e on s.userid=e.userid
LEFT JOIN sep_EmployeesInPositions p ON s.userid=p.userid 
LEFT JOIN V_Department d ON p.departmentid=d.level3Id 
--LEFT JOIN AD_V_Attendance v on v.userid=s.userid
WHERE s.beginTime>'2009-11-01 00:00:00' AND s.deleted=0 AND s.Approvestate=3 AND
d.level2id=107 
ORDER BY s.userid
";
            List<SingleOvertimeInfo> list = new List<SingleOvertimeInfo>();
            DataSet ds = DbHelperSQL.Query(sql.ToString());
            if (ds != null && ds.Tables.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    SingleOvertimeInfo model = new SingleOvertimeInfo();
                    model.PopupData(dr);
                    list.Add(model);
                }
            }
            return list;
        }
	}
}