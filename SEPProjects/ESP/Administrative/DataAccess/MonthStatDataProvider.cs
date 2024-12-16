using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using ESP.Administrative.Common;
using System.Data.SqlClient;
using ESP.Administrative.Entity;
using ESP.Administrative.DataAccess;

namespace ESP.Administrative.DataAccess
{
    public class MonthStatDataProvider
    {
        public MonthStatDataProvider()
		{}
		#region  成员方法

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public int GetMaxId()
		{
			return DbHelperSQL.GetMaxID("ID", "AD_MonthStat"); 
		}
		
        /// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int ID)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from AD_MonthStat");
			strSql.Append(" where ID= @ID");
			SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)};
			parameters[0].Value = ID;
			return DbHelperSQL.Exists(strSql.ToString(),parameters);
		}

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(ESP.Administrative.Entity.MonthStatInfo model)
		{
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into AD_MonthStat(");
            strSql.Append("UserID,UserCode,EmployeeName,Year,Month,WorkHours,LateCount,LeaveEarlyCount,SickLeaveHours,AffairLeaveHours,AnnualLeaveDays,OverAnnualLeaveDays,MaternityLeaveHours,MarriageLeaveHours,FuneralLeaveHours,AbsentDays,OverTimeHours,EvectionDays,EgressHours,OffTuneHours,HolidayOverTimeHours,PrenatalCheckHours,IncentiveHours,Other,State,ApproveID,ApproveTime,ApproveRemark,StatisticiansID,StatisticiansTime,HRAdminID,HRAdminTime,ManagerID,ManagerTime,ADAdminID,ADAdminTime,DeductSum,Deleted,CreateTime,UpdateTime,OperateorID,OperateorDept,Sort,AttendanceSubType,IsHavePCRefund,PCRefundAmount,LastAnnualDays)");
            strSql.Append(" values (");
            strSql.Append("@UserID,@UserCode,@EmployeeName,@Year,@Month,@WorkHours,@LateCount,@LeaveEarlyCount,@SickLeaveHours,@AffairLeaveHours,@AnnualLeaveDays,@OverAnnualLeaveDays,@MaternityLeaveHours,@MarriageLeaveHours,@FuneralLeaveHours,@AbsentDays,@OverTimeHours,@EvectionDays,@EgressHours,@OffTuneHours,@HolidayOverTimeHours,@PrenatalCheckHours,@IncentiveHours,@Other,@State,@ApproveID,@ApproveTime,@ApproveRemark,@StatisticiansID,@StatisticiansTime,@HRAdminID,@HRAdminTime,@ManagerID,@ManagerTime,@ADAdminID,@ADAdminTime,@DeductSum,@Deleted,@CreateTime,@UpdateTime,@OperateorID,@OperateorDept,@Sort,@AttendanceSubType,@IsHavePCRefund,@PCRefundAmount,@LastAnnualDays)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@UserID", SqlDbType.Int,4),
					new SqlParameter("@UserCode", SqlDbType.NVarChar),
					new SqlParameter("@EmployeeName", SqlDbType.NVarChar),
					new SqlParameter("@Year", SqlDbType.Int,4),
					new SqlParameter("@Month", SqlDbType.Int,4),
					new SqlParameter("@WorkHours", SqlDbType.Decimal,9),
					new SqlParameter("@LateCount", SqlDbType.Int,4),
					new SqlParameter("@LeaveEarlyCount", SqlDbType.Int,4),
					new SqlParameter("@SickLeaveHours", SqlDbType.Decimal,9),
					new SqlParameter("@AffairLeaveHours", SqlDbType.Decimal,9),
					new SqlParameter("@AnnualLeaveDays", SqlDbType.Decimal,9),
					new SqlParameter("@OverAnnualLeaveDays", SqlDbType.Decimal,9),
					new SqlParameter("@MaternityLeaveHours", SqlDbType.Decimal,9),
					new SqlParameter("@MarriageLeaveHours", SqlDbType.Decimal,9),
					new SqlParameter("@FuneralLeaveHours", SqlDbType.Decimal,9),
					new SqlParameter("@AbsentDays", SqlDbType.Decimal,9),
					new SqlParameter("@OverTimeHours", SqlDbType.Decimal,9),
					new SqlParameter("@EvectionDays", SqlDbType.Decimal,9),
					new SqlParameter("@EgressHours", SqlDbType.Decimal,9),
					new SqlParameter("@OffTuneHours", SqlDbType.Decimal,9),
					new SqlParameter("@HolidayOverTimeHours", SqlDbType.Decimal,9),
					new SqlParameter("@PrenatalCheckHours", SqlDbType.Decimal,9),
					new SqlParameter("@IncentiveHours", SqlDbType.Decimal,9),
					new SqlParameter("@Other", SqlDbType.NVarChar),
					new SqlParameter("@State", SqlDbType.Int,4),
					new SqlParameter("@ApproveID", SqlDbType.Int,4),
					new SqlParameter("@ApproveTime", SqlDbType.DateTime),
					new SqlParameter("@ApproveRemark", SqlDbType.NVarChar),
					new SqlParameter("@StatisticiansID", SqlDbType.Int,4),
					new SqlParameter("@StatisticiansTime", SqlDbType.DateTime),
					new SqlParameter("@HRAdminID", SqlDbType.Int,4),
					new SqlParameter("@HRAdminTime", SqlDbType.DateTime),
					new SqlParameter("@ManagerID", SqlDbType.Int,4),
					new SqlParameter("@ManagerTime", SqlDbType.DateTime),
					new SqlParameter("@ADAdminID", SqlDbType.Int,4),
					new SqlParameter("@ADAdminTime", SqlDbType.DateTime),
					new SqlParameter("@DeductSum", SqlDbType.Decimal,13),
					new SqlParameter("@Deleted", SqlDbType.Bit,1),
					new SqlParameter("@CreateTime", SqlDbType.DateTime),
					new SqlParameter("@UpdateTime", SqlDbType.DateTime),
					new SqlParameter("@OperateorID", SqlDbType.Int,4),
					new SqlParameter("@OperateorDept", SqlDbType.Int,4),
					new SqlParameter("@Sort", SqlDbType.Int,4),
                    new SqlParameter("@AttendanceSubType", SqlDbType.Int, 4),
					new SqlParameter("@IsHavePCRefund", SqlDbType.Bit,1),
					new SqlParameter("@PCRefundAmount", SqlDbType.Int,4),
                    new SqlParameter("@LastAnnualDays", SqlDbType.Decimal,9)
                                        };
            parameters[0].Value = model.UserID;
            parameters[1].Value = model.UserCode;
            parameters[2].Value = model.EmployeeName;
            parameters[3].Value = model.Year;
            parameters[4].Value = model.Month;
            parameters[5].Value = model.WorkHours;
            parameters[6].Value = model.LateCount;
            parameters[7].Value = model.LeaveEarlyCount;
            parameters[8].Value = model.SickLeaveHours;
            parameters[9].Value = model.AffairLeaveHours;
            parameters[10].Value = model.AnnualLeaveDays;
            parameters[11].Value = model.Overannualleavedays;
            parameters[12].Value = model.MaternityLeaveHours;
            parameters[13].Value = model.MarriageLeaveHours;
            parameters[14].Value = model.FuneralLeaveHours;
            parameters[15].Value = model.AbsentDays;
            parameters[16].Value = model.OverTimeHours;
            parameters[17].Value = model.EvectionDays;
            parameters[18].Value = model.EgressHours;
            parameters[19].Value = model.Offtunehours;
            parameters[20].Value = model.HolidayOverTimeHours;
            parameters[21].Value = model.PrenatalCheckHours;
            parameters[22].Value = model.IncentiveHours;
            parameters[23].Value = model.Other;
            parameters[24].Value = model.State;
            parameters[25].Value = model.ApproveID;
            parameters[26].Value = model.ApproveTime;
            parameters[27].Value = model.ApproveRemark;
            parameters[28].Value = model.StatisticiansID;
            parameters[29].Value = model.StatisticiansTime;
            parameters[30].Value = model.HRAdminID;
            parameters[31].Value = model.HRAdminTime;
            parameters[32].Value = model.ManagerID;
            parameters[33].Value = model.ManagerTime;
            parameters[34].Value = model.ADAdminID;
            parameters[35].Value = model.ADAdminTime;
            parameters[36].Value = model.DeductSum;
            parameters[37].Value = model.Deleted;
            parameters[38].Value = model.CreateTime;
            parameters[39].Value = model.UpdateTime;
            parameters[40].Value = model.OperateorID;
            parameters[41].Value = model.OperateorDept;
            parameters[42].Value = model.Sort;
            parameters[43].Value = model.AttendanceSubType;
            parameters[44].Value = model.IsHavePCRefund;
            parameters[45].Value = model.PCRefundAmount;
            parameters[46].Value = model.LastAnnualDays;

            object obj = DbHelperSQL.GetSingle(strSql.ToString(), parameters);
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
        /// 增加一条数据
        /// </summary>
        public int Add(ESP.Administrative.Entity.MonthStatInfo model, SqlTransaction trans)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into AD_MonthStat(");
            strSql.Append("UserID,UserCode,EmployeeName,Year,Month,WorkHours,LateCount,LeaveEarlyCount,SickLeaveHours,AffairLeaveHours,AnnualLeaveDays,OverAnnualLeaveDays,MaternityLeaveHours,MarriageLeaveHours,FuneralLeaveHours,AbsentDays,OverTimeHours,EvectionDays,EgressHours,OffTuneHours,HolidayOverTimeHours,PrenatalCheckHours,IncentiveHours,Other,State,ApproveID,ApproveTime,ApproveRemark,StatisticiansID,StatisticiansTime,HRAdminID,HRAdminTime,ManagerID,ManagerTime,ADAdminID,ADAdminTime,DeductSum,Deleted,CreateTime,UpdateTime,OperateorID,OperateorDept,Sort,AttendanceSubType,IsHavePCRefund,PCRefundAmount,LastAnnualDays)");
            strSql.Append(" values (");
            strSql.Append("@UserID,@UserCode,@EmployeeName,@Year,@Month,@WorkHours,@LateCount,@LeaveEarlyCount,@SickLeaveHours,@AffairLeaveHours,@AnnualLeaveDays,@OverAnnualLeaveDays,@MaternityLeaveHours,@MarriageLeaveHours,@FuneralLeaveHours,@AbsentDays,@OverTimeHours,@EvectionDays,@EgressHours,@OffTuneHours,@HolidayOverTimeHours,@PrenatalCheckHours,@IncentiveHours,@Other,@State,@ApproveID,@ApproveTime,@ApproveRemark,@StatisticiansID,@StatisticiansTime,@HRAdminID,@HRAdminTime,@ManagerID,@ManagerTime,@ADAdminID,@ADAdminTime,@DeductSum,@Deleted,@CreateTime,@UpdateTime,@OperateorID,@OperateorDept,@Sort,@AttendanceSubType,@IsHavePCRefund,@PCRefundAmount,@LastAnnualDays)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@UserID", SqlDbType.Int,4),
					new SqlParameter("@UserCode", SqlDbType.NVarChar),
					new SqlParameter("@EmployeeName", SqlDbType.NVarChar),
					new SqlParameter("@Year", SqlDbType.Int,4),
					new SqlParameter("@Month", SqlDbType.Int,4),
					new SqlParameter("@WorkHours", SqlDbType.Decimal,9),
					new SqlParameter("@LateCount", SqlDbType.Int,4),
					new SqlParameter("@LeaveEarlyCount", SqlDbType.Int,4),
					new SqlParameter("@SickLeaveHours", SqlDbType.Decimal,9),
					new SqlParameter("@AffairLeaveHours", SqlDbType.Decimal,9),
					new SqlParameter("@AnnualLeaveDays", SqlDbType.Decimal,9),
					new SqlParameter("@OverAnnualLeaveDays", SqlDbType.Decimal,9),
					new SqlParameter("@MaternityLeaveHours", SqlDbType.Decimal,9),
					new SqlParameter("@MarriageLeaveHours", SqlDbType.Decimal,9),
					new SqlParameter("@FuneralLeaveHours", SqlDbType.Decimal,9),
					new SqlParameter("@AbsentDays", SqlDbType.Decimal,9),
					new SqlParameter("@OverTimeHours", SqlDbType.Decimal,9),
					new SqlParameter("@EvectionDays", SqlDbType.Decimal,9),
					new SqlParameter("@EgressHours", SqlDbType.Decimal,9),
					new SqlParameter("@OffTuneHours", SqlDbType.Decimal,9),
					new SqlParameter("@HolidayOverTimeHours", SqlDbType.Decimal,9),
					new SqlParameter("@PrenatalCheckHours", SqlDbType.Decimal,9),
					new SqlParameter("@IncentiveHours", SqlDbType.Decimal,9),
					new SqlParameter("@Other", SqlDbType.NVarChar),
					new SqlParameter("@State", SqlDbType.Int,4),
					new SqlParameter("@ApproveID", SqlDbType.Int,4),
					new SqlParameter("@ApproveTime", SqlDbType.DateTime),
					new SqlParameter("@ApproveRemark", SqlDbType.NVarChar),
					new SqlParameter("@StatisticiansID", SqlDbType.Int,4),
					new SqlParameter("@StatisticiansTime", SqlDbType.DateTime),
					new SqlParameter("@HRAdminID", SqlDbType.Int,4),
					new SqlParameter("@HRAdminTime", SqlDbType.DateTime),
					new SqlParameter("@ManagerID", SqlDbType.Int,4),
					new SqlParameter("@ManagerTime", SqlDbType.DateTime),
					new SqlParameter("@ADAdminID", SqlDbType.Int,4),
					new SqlParameter("@ADAdminTime", SqlDbType.DateTime),
					new SqlParameter("@DeductSum", SqlDbType.Decimal,13),
					new SqlParameter("@Deleted", SqlDbType.Bit,1),
					new SqlParameter("@CreateTime", SqlDbType.DateTime),
					new SqlParameter("@UpdateTime", SqlDbType.DateTime),
					new SqlParameter("@OperateorID", SqlDbType.Int,4),
					new SqlParameter("@OperateorDept", SqlDbType.Int,4),
					new SqlParameter("@Sort", SqlDbType.Int,4),
                    new SqlParameter("@AttendanceSubType", SqlDbType.Int, 4),
					new SqlParameter("@IsHavePCRefund", SqlDbType.Bit,1),
					new SqlParameter("@PCRefundAmount", SqlDbType.Int,4),
                    new SqlParameter("@LastAnnualDays", SqlDbType.Decimal,9)
                                        };
            parameters[0].Value = model.UserID;
            parameters[1].Value = model.UserCode;
            parameters[2].Value = model.EmployeeName;
            parameters[3].Value = model.Year;
            parameters[4].Value = model.Month;
            parameters[5].Value = model.WorkHours;
            parameters[6].Value = model.LateCount;
            parameters[7].Value = model.LeaveEarlyCount;
            parameters[8].Value = model.SickLeaveHours;
            parameters[9].Value = model.AffairLeaveHours;
            parameters[10].Value = model.AnnualLeaveDays;
            parameters[11].Value = model.Overannualleavedays;
            parameters[12].Value = model.MaternityLeaveHours;
            parameters[13].Value = model.MarriageLeaveHours;
            parameters[14].Value = model.FuneralLeaveHours;
            parameters[15].Value = model.AbsentDays;
            parameters[16].Value = model.OverTimeHours;
            parameters[17].Value = model.EvectionDays;
            parameters[18].Value = model.EgressHours;
            parameters[19].Value = model.Offtunehours;
            parameters[20].Value = model.HolidayOverTimeHours;
            parameters[21].Value = model.PrenatalCheckHours;
            parameters[22].Value = model.IncentiveHours;
            parameters[23].Value = model.Other;
            parameters[24].Value = model.State;
            parameters[25].Value = model.ApproveID;
            parameters[26].Value = model.ApproveTime;
            parameters[27].Value = model.ApproveRemark;
            parameters[28].Value = model.StatisticiansID;
            parameters[29].Value = model.StatisticiansTime;
            parameters[30].Value = model.HRAdminID;
            parameters[31].Value = model.HRAdminTime;
            parameters[32].Value = model.ManagerID;
            parameters[33].Value = model.ManagerTime;
            parameters[34].Value = model.ADAdminID;
            parameters[35].Value = model.ADAdminTime;
            parameters[36].Value = model.DeductSum;
            parameters[37].Value = model.Deleted;
            parameters[38].Value = model.CreateTime;
            parameters[39].Value = model.UpdateTime;
            parameters[40].Value = model.OperateorID;
            parameters[41].Value = model.OperateorDept;
            parameters[42].Value = model.Sort;
            parameters[43].Value = model.AttendanceSubType;
            parameters[44].Value = model.IsHavePCRefund;
            parameters[45].Value = model.PCRefundAmount;
            parameters[46].Value = model.LastAnnualDays;

            object obj = DbHelperSQL.GetSingle(strSql.ToString(), trans.Connection, trans, parameters);
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
        public void Update(ESP.Administrative.Entity.MonthStatInfo model)
		{
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update AD_MonthStat set ");
            strSql.Append("UserID=@UserID,");
            strSql.Append("UserCode=@UserCode,");
            strSql.Append("EmployeeName=@EmployeeName,");
            strSql.Append("Year=@Year,");
            strSql.Append("Month=@Month,");
            strSql.Append("WorkHours=@WorkHours,");
            strSql.Append("LateCount=@LateCount,");
            strSql.Append("LeaveEarlyCount=@LeaveEarlyCount,");
            strSql.Append("SickLeaveHours=@SickLeaveHours,");
            strSql.Append("AffairLeaveHours=@AffairLeaveHours,");
            strSql.Append("AnnualLeaveDays=@AnnualLeaveDays,");
            strSql.Append("OverAnnualLeaveDays=@OverAnnualLeaveDays,");
            strSql.Append("MaternityLeaveHours=@MaternityLeaveHours,");
            strSql.Append("MarriageLeaveHours=@MarriageLeaveHours,");
            strSql.Append("FuneralLeaveHours=@FuneralLeaveHours,");
            strSql.Append("AbsentDays=@AbsentDays,");
            strSql.Append("OverTimeHours=@OverTimeHours,");
            strSql.Append("EvectionDays=@EvectionDays,");
            strSql.Append("EgressHours=@EgressHours,");
            strSql.Append("OffTuneHours=@OffTuneHours,");
            strSql.Append("HolidayOverTimeHours=@HolidayOverTimeHours,");
            strSql.Append("PrenatalCheckHours=@PrenatalCheckHours,");
            strSql.Append("IncentiveHours=@IncentiveHours,");
            strSql.Append("Other=@Other,");
            strSql.Append("State=@State,");
            strSql.Append("ApproveID=@ApproveID,");
            strSql.Append("ApproveTime=@ApproveTime,");
            strSql.Append("ApproveRemark=@ApproveRemark,");
            strSql.Append("StatisticiansID=@StatisticiansID,");
            strSql.Append("StatisticiansTime=@StatisticiansTime,");
            strSql.Append("HRAdminID=@HRAdminID,");
            strSql.Append("HRAdminTime=@HRAdminTime,");
            strSql.Append("ManagerID=@ManagerID,");
            strSql.Append("ManagerTime=@ManagerTime,");
            strSql.Append("ADAdminID=@ADAdminID,");
            strSql.Append("ADAdminTime=@ADAdminTime,");
            strSql.Append("DeductSum=@DeductSum,");
            strSql.Append("Deleted=@Deleted,");
            strSql.Append("CreateTime=@CreateTime,");
            strSql.Append("UpdateTime=@UpdateTime,");
            strSql.Append("OperateorID=@OperateorID,");
            strSql.Append("OperateorDept=@OperateorDept,");
            strSql.Append("Sort=@Sort,");
            strSql.Append("AttendanceSubType=@AttendanceSubType,");
            strSql.Append("IsHavePCRefund=@IsHavePCRefund,");
            strSql.Append("PCRefundAmount=@PCRefundAmount, ");
            strSql.Append("LastAnnualDays=@LastAnnualDays");
            strSql.Append(" where ID=@ID");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4),
					new SqlParameter("@UserID", SqlDbType.Int,4),
					new SqlParameter("@UserCode", SqlDbType.NVarChar),
					new SqlParameter("@EmployeeName", SqlDbType.NVarChar),
					new SqlParameter("@Year", SqlDbType.Int,4),
					new SqlParameter("@Month", SqlDbType.Int,4),
					new SqlParameter("@WorkHours", SqlDbType.Decimal,9),
					new SqlParameter("@LateCount", SqlDbType.Int,4),
					new SqlParameter("@LeaveEarlyCount", SqlDbType.Int,4),
					new SqlParameter("@SickLeaveHours", SqlDbType.Decimal,9),
					new SqlParameter("@AffairLeaveHours", SqlDbType.Decimal,9),
					new SqlParameter("@AnnualLeaveDays", SqlDbType.Decimal,9),
					new SqlParameter("@OverAnnualLeaveDays", SqlDbType.Decimal,9),
					new SqlParameter("@MaternityLeaveHours", SqlDbType.Decimal,9),
					new SqlParameter("@MarriageLeaveHours", SqlDbType.Decimal,9),
					new SqlParameter("@FuneralLeaveHours", SqlDbType.Decimal,9),
					new SqlParameter("@AbsentDays", SqlDbType.Decimal,9),
					new SqlParameter("@OverTimeHours", SqlDbType.Decimal,9),
					new SqlParameter("@EvectionDays", SqlDbType.Decimal,9),
					new SqlParameter("@EgressHours", SqlDbType.Decimal,9),
					new SqlParameter("@OffTuneHours", SqlDbType.Decimal,9),
					new SqlParameter("@HolidayOverTimeHours", SqlDbType.Decimal,9),
					new SqlParameter("@PrenatalCheckHours", SqlDbType.Decimal,9),
					new SqlParameter("@IncentiveHours", SqlDbType.Decimal,9),
					new SqlParameter("@Other", SqlDbType.NVarChar),
					new SqlParameter("@State", SqlDbType.Int,4),
					new SqlParameter("@ApproveID", SqlDbType.Int,4),
					new SqlParameter("@ApproveTime", SqlDbType.DateTime),
					new SqlParameter("@ApproveRemark", SqlDbType.NVarChar),
					new SqlParameter("@StatisticiansID", SqlDbType.Int,4),
					new SqlParameter("@StatisticiansTime", SqlDbType.DateTime),
					new SqlParameter("@HRAdminID", SqlDbType.Int,4),
					new SqlParameter("@HRAdminTime", SqlDbType.DateTime),
					new SqlParameter("@ManagerID", SqlDbType.Int,4),
					new SqlParameter("@ManagerTime", SqlDbType.DateTime),
					new SqlParameter("@ADAdminID", SqlDbType.Int,4),
					new SqlParameter("@ADAdminTime", SqlDbType.DateTime),
					new SqlParameter("@DeductSum", SqlDbType.Decimal,13),
					new SqlParameter("@Deleted", SqlDbType.Bit,1),
					new SqlParameter("@CreateTime", SqlDbType.DateTime),
					new SqlParameter("@UpdateTime", SqlDbType.DateTime),
					new SqlParameter("@OperateorID", SqlDbType.Int,4),
					new SqlParameter("@OperateorDept", SqlDbType.Int,4),
					new SqlParameter("@Sort", SqlDbType.Int,4),
                    new SqlParameter("@AttendanceSubType", SqlDbType.Int, 4),
					new SqlParameter("@IsHavePCRefund", SqlDbType.Bit,1),
					new SqlParameter("@PCRefundAmount", SqlDbType.Int,4),
                    new SqlParameter("@LastAnnualDays", SqlDbType.Decimal,9)                    
                                        };
            parameters[0].Value = model.ID;
            parameters[1].Value = model.UserID;
            parameters[2].Value = model.UserCode;
            parameters[3].Value = model.EmployeeName;
            parameters[4].Value = model.Year;
            parameters[5].Value = model.Month;
            parameters[6].Value = model.WorkHours;
            parameters[7].Value = model.LateCount;
            parameters[8].Value = model.LeaveEarlyCount;
            parameters[9].Value = model.SickLeaveHours;
            parameters[10].Value = model.AffairLeaveHours;
            parameters[11].Value = model.AnnualLeaveDays;
            parameters[12].Value = model.Overannualleavedays;
            parameters[13].Value = model.MaternityLeaveHours;
            parameters[14].Value = model.MarriageLeaveHours;
            parameters[15].Value = model.FuneralLeaveHours;
            parameters[16].Value = model.AbsentDays;
            parameters[17].Value = model.OverTimeHours;
            parameters[18].Value = model.EvectionDays;
            parameters[19].Value = model.EgressHours;
            parameters[20].Value = model.Offtunehours;
            parameters[21].Value = model.HolidayOverTimeHours;
            parameters[22].Value = model.PrenatalCheckHours;
            parameters[23].Value = model.IncentiveHours;
            parameters[24].Value = model.Other;
            parameters[25].Value = model.State;
            parameters[26].Value = model.ApproveID;
            parameters[27].Value = model.ApproveTime;
            parameters[28].Value = model.ApproveRemark;
            parameters[29].Value = model.StatisticiansID;
            parameters[30].Value = model.StatisticiansTime;
            parameters[31].Value = model.HRAdminID;
            parameters[32].Value = model.HRAdminTime;
            parameters[33].Value = model.ManagerID;
            parameters[34].Value = model.ManagerTime;
            parameters[35].Value = model.ADAdminID;
            parameters[36].Value = model.ADAdminTime;
            parameters[37].Value = model.DeductSum;
            parameters[38].Value = model.Deleted;
            parameters[39].Value = model.CreateTime;
            parameters[40].Value = model.UpdateTime;
            parameters[41].Value = model.OperateorID;
            parameters[42].Value = model.OperateorDept;
            parameters[43].Value = model.Sort;
            parameters[44].Value = model.AttendanceSubType;
            parameters[45].Value = model.IsHavePCRefund;
            parameters[46].Value = model.PCRefundAmount;
            parameters[47].Value = model.LastAnnualDays;

            DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
		}

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public int Update(ESP.Administrative.Entity.MonthStatInfo model, SqlTransaction trans)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update AD_MonthStat set ");
            strSql.Append("UserID=@UserID,");
            strSql.Append("UserCode=@UserCode,");
            strSql.Append("EmployeeName=@EmployeeName,");
            strSql.Append("Year=@Year,");
            strSql.Append("Month=@Month,");
            strSql.Append("WorkHours=@WorkHours,");
            strSql.Append("LateCount=@LateCount,");
            strSql.Append("LeaveEarlyCount=@LeaveEarlyCount,");
            strSql.Append("SickLeaveHours=@SickLeaveHours,");
            strSql.Append("AffairLeaveHours=@AffairLeaveHours,");
            strSql.Append("AnnualLeaveDays=@AnnualLeaveDays,");
            strSql.Append("OverAnnualLeaveDays=@OverAnnualLeaveDays,");
            strSql.Append("MaternityLeaveHours=@MaternityLeaveHours,");
            strSql.Append("MarriageLeaveHours=@MarriageLeaveHours,");
            strSql.Append("FuneralLeaveHours=@FuneralLeaveHours,");
            strSql.Append("AbsentDays=@AbsentDays,");
            strSql.Append("OverTimeHours=@OverTimeHours,");
            strSql.Append("EvectionDays=@EvectionDays,");
            strSql.Append("EgressHours=@EgressHours,");
            strSql.Append("OffTuneHours=@OffTuneHours,");
            strSql.Append("HolidayOverTimeHours=@HolidayOverTimeHours,");
            strSql.Append("PrenatalCheckHours=@PrenatalCheckHours,");
            strSql.Append("IncentiveHours=@IncentiveHours,");
            strSql.Append("Other=@Other,");
            strSql.Append("State=@State,");
            strSql.Append("ApproveID=@ApproveID,");
            strSql.Append("ApproveTime=@ApproveTime,");
            strSql.Append("ApproveRemark=@ApproveRemark,");
            strSql.Append("StatisticiansID=@StatisticiansID,");
            strSql.Append("StatisticiansTime=@StatisticiansTime,");
            strSql.Append("HRAdminID=@HRAdminID,");
            strSql.Append("HRAdminTime=@HRAdminTime,");
            strSql.Append("ManagerID=@ManagerID,");
            strSql.Append("ManagerTime=@ManagerTime,");
            strSql.Append("ADAdminID=@ADAdminID,");
            strSql.Append("ADAdminTime=@ADAdminTime,");
            strSql.Append("DeductSum=@DeductSum,");
            strSql.Append("Deleted=@Deleted,");
            strSql.Append("CreateTime=@CreateTime,");
            strSql.Append("UpdateTime=@UpdateTime,");
            strSql.Append("OperateorID=@OperateorID,");
            strSql.Append("OperateorDept=@OperateorDept,");
            strSql.Append("Sort=@Sort,");
            strSql.Append("AttendanceSubType=@AttendanceSubType,");
            strSql.Append("IsHavePCRefund=@IsHavePCRefund,");
            strSql.Append("PCRefundAmount=@PCRefundAmount, ");
            strSql.Append("LastAnnualDays=@LastAnnualDays ");
            strSql.Append(" where ID=@ID");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4),
					new SqlParameter("@UserID", SqlDbType.Int,4),
					new SqlParameter("@UserCode", SqlDbType.NVarChar),
					new SqlParameter("@EmployeeName", SqlDbType.NVarChar),
					new SqlParameter("@Year", SqlDbType.Int,4),
					new SqlParameter("@Month", SqlDbType.Int,4),
					new SqlParameter("@WorkHours", SqlDbType.Decimal,9),
					new SqlParameter("@LateCount", SqlDbType.Int,4),
					new SqlParameter("@LeaveEarlyCount", SqlDbType.Int,4),
					new SqlParameter("@SickLeaveHours", SqlDbType.Decimal,9),
					new SqlParameter("@AffairLeaveHours", SqlDbType.Decimal,9),
					new SqlParameter("@AnnualLeaveDays", SqlDbType.Decimal,9),
					new SqlParameter("@OverAnnualLeaveDays", SqlDbType.Decimal,9),
					new SqlParameter("@MaternityLeaveHours", SqlDbType.Decimal,9),
					new SqlParameter("@MarriageLeaveHours", SqlDbType.Decimal,9),
					new SqlParameter("@FuneralLeaveHours", SqlDbType.Decimal,9),
					new SqlParameter("@AbsentDays", SqlDbType.Decimal,9),
					new SqlParameter("@OverTimeHours", SqlDbType.Decimal,9),
					new SqlParameter("@EvectionDays", SqlDbType.Decimal,9),
					new SqlParameter("@EgressHours", SqlDbType.Decimal,9),
					new SqlParameter("@OffTuneHours", SqlDbType.Decimal,9),
					new SqlParameter("@HolidayOverTimeHours", SqlDbType.Decimal,9),
					new SqlParameter("@PrenatalCheckHours", SqlDbType.Decimal,9),
					new SqlParameter("@IncentiveHours", SqlDbType.Decimal,9),
					new SqlParameter("@Other", SqlDbType.NVarChar),
					new SqlParameter("@State", SqlDbType.Int,4),
					new SqlParameter("@ApproveID", SqlDbType.Int,4),
					new SqlParameter("@ApproveTime", SqlDbType.DateTime),
					new SqlParameter("@ApproveRemark", SqlDbType.NVarChar),
					new SqlParameter("@StatisticiansID", SqlDbType.Int,4),
					new SqlParameter("@StatisticiansTime", SqlDbType.DateTime),
					new SqlParameter("@HRAdminID", SqlDbType.Int,4),
					new SqlParameter("@HRAdminTime", SqlDbType.DateTime),
					new SqlParameter("@ManagerID", SqlDbType.Int,4),
					new SqlParameter("@ManagerTime", SqlDbType.DateTime),
					new SqlParameter("@ADAdminID", SqlDbType.Int,4),
					new SqlParameter("@ADAdminTime", SqlDbType.DateTime),
					new SqlParameter("@DeductSum", SqlDbType.Decimal,13),
					new SqlParameter("@Deleted", SqlDbType.Bit,1),
					new SqlParameter("@CreateTime", SqlDbType.DateTime),
					new SqlParameter("@UpdateTime", SqlDbType.DateTime),
					new SqlParameter("@OperateorID", SqlDbType.Int,4),
					new SqlParameter("@OperateorDept", SqlDbType.Int,4),
					new SqlParameter("@Sort", SqlDbType.Int,4),
                    new SqlParameter("@AttendanceSubType", SqlDbType.Int, 4),
					new SqlParameter("@IsHavePCRefund", SqlDbType.Bit,1),
					new SqlParameter("@PCRefundAmount", SqlDbType.Int,4),
                    new SqlParameter("@LastAnnualDays", SqlDbType.Decimal,9)                
                                        };
            parameters[0].Value = model.ID;
            parameters[1].Value = model.UserID;
            parameters[2].Value = model.UserCode;
            parameters[3].Value = model.EmployeeName;
            parameters[4].Value = model.Year;
            parameters[5].Value = model.Month;
            parameters[6].Value = model.WorkHours;
            parameters[7].Value = model.LateCount;
            parameters[8].Value = model.LeaveEarlyCount;
            parameters[9].Value = model.SickLeaveHours;
            parameters[10].Value = model.AffairLeaveHours;
            parameters[11].Value = model.AnnualLeaveDays;
            parameters[12].Value = model.Overannualleavedays;
            parameters[13].Value = model.MaternityLeaveHours;
            parameters[14].Value = model.MarriageLeaveHours;
            parameters[15].Value = model.FuneralLeaveHours;
            parameters[16].Value = model.AbsentDays;
            parameters[17].Value = model.OverTimeHours;
            parameters[18].Value = model.EvectionDays;
            parameters[19].Value = model.EgressHours;
            parameters[20].Value = model.Offtunehours;
            parameters[21].Value = model.HolidayOverTimeHours;
            parameters[22].Value = model.PrenatalCheckHours;
            parameters[23].Value = model.IncentiveHours;
            parameters[24].Value = model.Other;
            parameters[25].Value = model.State;
            parameters[26].Value = model.ApproveID;
            parameters[27].Value = model.ApproveTime;
            parameters[28].Value = model.ApproveRemark;
            parameters[29].Value = model.StatisticiansID;
            parameters[30].Value = model.StatisticiansTime;
            parameters[31].Value = model.HRAdminID;
            parameters[32].Value = model.HRAdminTime;
            parameters[33].Value = model.ManagerID;
            parameters[34].Value = model.ManagerTime;
            parameters[35].Value = model.ADAdminID;
            parameters[36].Value = model.ADAdminTime;
            parameters[37].Value = model.DeductSum;
            parameters[38].Value = model.Deleted;
            parameters[39].Value = model.CreateTime;
            parameters[40].Value = model.UpdateTime;
            parameters[41].Value = model.OperateorID;
            parameters[42].Value = model.OperateorDept;
            parameters[43].Value = model.Sort;
            parameters[44].Value = model.AttendanceSubType;
            parameters[45].Value = model.IsHavePCRefund;
            parameters[46].Value = model.PCRefundAmount;
            parameters[47].Value = model.LastAnnualDays;

            object obj = DbHelperSQL.GetSingle(strSql.ToString(), trans.Connection, trans, parameters);
            if (obj == null)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public void Delete(int ID)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete AD_MonthStat ");
			strSql.Append(" where ID=@ID");
			SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)
				};
			parameters[0].Value = ID;
			DbHelperSQL.ExecuteSql(strSql.ToString(),parameters);
		}

        public int Delete(int userid, int year ,int month )
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete AD_MonthStat ");
            strSql.Append(" where userid=@userid and year=@year and month=@month");
            SqlParameter[] parameters = {
					new SqlParameter("@userid", SqlDbType.Int,4),
                    new SqlParameter("@year", SqlDbType.Int,4),
                    new SqlParameter("@month", SqlDbType.Int,4)
				};
            parameters[0].Value = userid;
            parameters[1].Value = year;
            parameters[2].Value = month;
            return DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }


		/// <summary>
		/// 得到一个对象实体
		/// </summary>
        public ESP.Administrative.Entity.MonthStatInfo GetModel(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from AD_MonthStat ");
            strSql.Append(" where ID=@ID");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)};
            parameters[0].Value = ID;
            ESP.Administrative.Entity.MonthStatInfo model = new ESP.Administrative.Entity.MonthStatInfo();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            model.ID = ID;
            if (ds.Tables[0].Rows.Count > 0)
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
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * FROM AD_MonthStat ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());
		}

        /// <summary>
        /// 获得某个用户所有的考勤信息
        /// </summary>
        /// <param name="userid">用户编号</param>
        /// <returns></returns>
        public ESP.Administrative.Entity.MonthStatInfo GetMonthStatInfo(int userid, int year, int month)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from AD_MonthStat ");
            strSql.Append(" where UserId=@userId and [year]=@year and [Month]=@Month and State=@State ");
            SqlParameter[] parameters = {
					new SqlParameter("@userId", SqlDbType.Int, 4),
                    new SqlParameter("@year", SqlDbType.Int, 4),
                    new SqlParameter("@Month", SqlDbType.Int, 4),
                    new SqlParameter("@State", SqlDbType.Int, 4)};
            parameters[0].Value = userid;
            parameters[1].Value = year;
            parameters[2].Value = month;
            parameters[3].Value = Status.MonthStatAppState_Passed;
            ESP.Administrative.Entity.MonthStatInfo model = new ESP.Administrative.Entity.MonthStatInfo();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            model.UserID = userid;
            if (ds.Tables[0].Rows.Count > 0)
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
        /// 获得某个用户所有的考勤信息
        /// </summary>
        /// <param name="userid">用户编号</param>
        /// <returns></returns>
        public ESP.Administrative.Entity.MonthStatInfo GetMonthStatInfo(int userid, int year, int month, int state)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from AD_MonthStat ");
            strSql.Append(" where UserId=@userId and [year]=@year and [Month]=@Month and State=@State ");
            SqlParameter[] parameters = {
					new SqlParameter("@userId", SqlDbType.Int, 4),
                    new SqlParameter("@year", SqlDbType.Int, 4),
                    new SqlParameter("@Month", SqlDbType.Int, 4),
                    new SqlParameter("@State", SqlDbType.Int, 4)};
            parameters[0].Value = userid;
            parameters[1].Value = year;
            parameters[2].Value = month;
            parameters[3].Value = state;
            ESP.Administrative.Entity.MonthStatInfo model = new ESP.Administrative.Entity.MonthStatInfo();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            model.UserID = userid;
            if (ds.Tables[0].Rows.Count > 0)
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
        /// 获得某个用户所有的考勤信息
        /// </summary>
        /// <param name="userid">用户编号</param>
        /// <returns></returns>
        public ESP.Administrative.Entity.MonthStatInfo GetMonthStatInfoApprove(int userid, int year, int month)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from AD_MonthStat ");
            strSql.Append(" where UserId=@userId and [year]=@year and [Month]=@Month ");
            SqlParameter[] parameters = {
					new SqlParameter("@userId", SqlDbType.Int, 4),
                    new SqlParameter("@year", SqlDbType.Int, 4),
                    new SqlParameter("@Month", SqlDbType.Int, 4)};
            parameters[0].Value = userid;
            parameters[1].Value = year;
            parameters[2].Value = month;
            ESP.Administrative.Entity.MonthStatInfo model = new ESP.Administrative.Entity.MonthStatInfo();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            model.UserID = userid;
            if (ds.Tables[0].Rows.Count > 0)
            {
                model.PopupData(ds.Tables[0].Rows[0]);
                return model;
            }
            else
            {
                return null;
            }
        }


        public ESP.Administrative.Entity.MonthStatInfo GetMonthStatInfoApproveByTrans(int userid, int year, int month,SqlTransaction trans)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from AD_MonthStat ");
            strSql.Append(" where UserId=@userId and [year]=@year and [Month]=@Month ");
            SqlParameter[] parameters = {
					new SqlParameter("@userId", SqlDbType.Int, 4),
                    new SqlParameter("@year", SqlDbType.Int, 4),
                    new SqlParameter("@Month", SqlDbType.Int, 4)};
            parameters[0].Value = userid;
            parameters[1].Value = year;
            parameters[2].Value = month;
            ESP.Administrative.Entity.MonthStatInfo model = new ESP.Administrative.Entity.MonthStatInfo();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), trans, parameters);
            model.UserID = userid;
            if (ds.Tables[0].Rows.Count > 0)
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
        /// 获得用户全年的考勤记录信息
        /// </summary>
        /// <param name="userid">用户信息</param>
        /// <param name="year">年份</param>
        /// <returns>返回一个全年考勤的数据集合</returns>
        public DataSet GetMonthStatInfo(int userid, int year)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from AD_MonthStat ");
            strSql.Append(" where UserId=@userId and [year]=@year and State not in (" + Status.MonthStatAppState_NoSubmit + ", " + Status.MonthStatAppState_Overrule + ") ");
            SqlParameter[] parameters = {
					new SqlParameter("@userId", SqlDbType.Int,4),
                    new SqlParameter("@year", SqlDbType.Int,4)};
            parameters[0].Value = userid;
            parameters[1].Value = year;
            return DbHelperSQL.Query(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 获得用户有权限查看的月考勤统计信息
        /// </summary>
        /// <param name="userId">用户编号</param>
        /// <param name="year">年份</param>
        /// <param name="month">月份</param>
        /// <returns>返回月统计信息</returns>
        public Dictionary<int, ESP.Administrative.Entity.MonthStatInfo> GetMonthStatInfoByUserID(string userIds, int year, int month)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select distinct * from AD_MonthStat ");
            strSql.Append(" where UserId in (" + userIds + ") and [year]=@year and [Month]=@Month ");
            SqlParameter[] parameters = {
                    new SqlParameter("@year", SqlDbType.Int, 4),
                    new SqlParameter("@Month", SqlDbType.Int, 4)};
            parameters[0].Value = year;
            parameters[1].Value = month;
            Dictionary<int, ESP.Administrative.Entity.MonthStatInfo> dic = new Dictionary<int, ESP.Administrative.Entity.MonthStatInfo>();
            
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ESP.Administrative.Entity.MonthStatInfo model = new ESP.Administrative.Entity.MonthStatInfo();
                    model.PopupData(dr);
                    if (!dic.ContainsKey(model.UserID))
                        dic.Add(model.UserID, model);
                }
                return dic;
            }
            else
            {
                return null;
            }
        }

        public List<ESP.Administrative.Entity.MonthStatInfo> GetMonthStateByYear(int userId, int year,int excludeMonth)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select distinct * from AD_MonthStat ");
            strSql.Append(" where UserId = " + userId + " and [year]=@year and [Month]!=@Month ");
            SqlParameter[] parameters = {
                    new SqlParameter("@year", SqlDbType.Int, 4),
                    new SqlParameter("@Month", SqlDbType.Int, 4)};
            parameters[0].Value = year;
            parameters[1].Value = excludeMonth;
            List<ESP.Administrative.Entity.MonthStatInfo> list = new List<ESP.Administrative.Entity.MonthStatInfo>();

            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ESP.Administrative.Entity.MonthStatInfo model = new ESP.Administrative.Entity.MonthStatInfo();
                    model.PopupData(dr);
                    list.Add(model);
                }
                return list;
            }
            else
            {
                return null;
            }
        }

        #endregion  成员方法
	}
}