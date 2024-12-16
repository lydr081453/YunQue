using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using ESP.Administrative.Common;

namespace ESP.Administrative.DataAccess
{
    public class AttendanceDataProvider
    {
        public AttendanceDataProvider()
        { }

        #region  成员方法

        /// <summary>
        /// 得到最大ID
        /// </summary>
        public int GetMaxId()
        {
            return DbHelperSQL.GetMaxID("ID", "AD_Attendance");
        }

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from AD_Attendance");
            strSql.Append(" where ID= @ID");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)
				};
            parameters[0].Value = ID;
            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(ESP.Administrative.Entity.AttendanceInfo model)
        {
            //model.ID=GetMaxId();
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into AD_Attendance(");
            strSql.Append("UserID,UserCode,EmployeeName,AttendanceDate,GoWorkTime,OffWorkTime,IsLate,IsLeaveEarly,IsLeave,LeaveID,LeaveType,IsAbsent,AbsentDays,IsOverTime,OverTimeHours,IsAnnualLeave,AnnualLeaveDays,IsEvection,EvectionDays,IsEgress,EgressHours,Other,Remark,Deleted,CreateTime,UpdateTime,OperateorID,OperateorDept,Sort,SingleOverTimeID)");
            strSql.Append(" values (");
            strSql.Append("@UserID,@UserCode,@EmployeeName,@AttendanceDate,@GoWorkTime,@OffWorkTime,@IsLate,@IsLeaveEarly,@IsLeave,@LeaveID,@LeaveType,@IsAbsent,@AbsentDays,@IsOverTime,@OverTimeHours,@IsAnnualLeave,@AnnualLeaveDays,@IsEvection,@EvectionDays,@IsEgress,@EgressHours,@Other,@Remark,@Deleted,@CreateTime,@UpdateTime,@OperateorID,@OperateorDept,@Sort,@SingleOverTimeID)");
            SqlParameter[] parameters = {
					new SqlParameter("@UserID", SqlDbType.Int,4),
					new SqlParameter("@UserCode", SqlDbType.NVarChar),
					new SqlParameter("@EmployeeName", SqlDbType.NVarChar),
                    new SqlParameter("@AttendanceDate", SqlDbType.DateTime),
					new SqlParameter("@GoWorkTime", SqlDbType.DateTime),
					new SqlParameter("@OffWorkTime", SqlDbType.DateTime),
					new SqlParameter("@IsLate", SqlDbType.Bit,1),
					new SqlParameter("@IsLeaveEarly", SqlDbType.Bit,1),
					new SqlParameter("@IsLeave", SqlDbType.Bit,1),
					new SqlParameter("@LeaveID", SqlDbType.Int,4),
					new SqlParameter("@LeaveType", SqlDbType.Int,4),
					new SqlParameter("@IsAbsent", SqlDbType.Bit,1),
					new SqlParameter("@AbsentDays", SqlDbType.Decimal,9),
					new SqlParameter("@IsOverTime", SqlDbType.Bit,1),
					new SqlParameter("@OverTimeHours", SqlDbType.Int,4),
					new SqlParameter("@IsAnnualLeave", SqlDbType.Bit,1),
					new SqlParameter("@AnnualLeaveDays", SqlDbType.Decimal,9),
					new SqlParameter("@IsEvection", SqlDbType.Bit,1),
					new SqlParameter("@EvectionDays", SqlDbType.Int,4),
					new SqlParameter("@IsEgress", SqlDbType.Bit,1),
					new SqlParameter("@EgressHours", SqlDbType.Decimal,9),
					new SqlParameter("@Other", SqlDbType.NVarChar),
					new SqlParameter("@Remark", SqlDbType.NVarChar),
					new SqlParameter("@Deleted", SqlDbType.Bit,1),
					new SqlParameter("@CreateTime", SqlDbType.DateTime),
					new SqlParameter("@UpdateTime", SqlDbType.DateTime),
					new SqlParameter("@OperateorID", SqlDbType.Int,4),
					new SqlParameter("@OperateorDept", SqlDbType.Int,4),
					new SqlParameter("@Sort", SqlDbType.Int,4),
                    new SqlParameter("@SingleOverTimeID", SqlDbType.Int, 4)};
            parameters[0].Value = model.UserID;
            parameters[1].Value = model.UserCode;
            parameters[2].Value = model.EmployeeName;
            parameters[3].Value = model.Attendancedate;
            parameters[4].Value = model.GoWorkTime;
            parameters[5].Value = model.OffWorkTime;
            parameters[6].Value = model.IsLate;
            parameters[7].Value = model.IsLeaveEarly;
            parameters[8].Value = model.IsLeave;
            parameters[9].Value = model.LeaveID;
            parameters[10].Value = model.LeaveType;
            parameters[11].Value = model.IsAbsent;
            parameters[12].Value = model.AbsentDays;
            parameters[13].Value = model.IsOverTime;
            parameters[14].Value = model.OverTimeHours;
            parameters[15].Value = model.IsAnnualLeave;
            parameters[16].Value = model.AnnualLeaveDays;
            parameters[17].Value = model.IsEvection;
            parameters[18].Value = model.EvectionDays;
            parameters[19].Value = model.IsEgress;
            parameters[20].Value = model.EgressHours;
            parameters[21].Value = model.Other;
            parameters[22].Value = model.Remark;
            parameters[23].Value = model.Deleted;
            parameters[24].Value = model.CreateTime;
            parameters[25].Value = model.UpdateTime;
            parameters[26].Value = model.OperateorID;
            parameters[27].Value = model.OperateorDept;
            parameters[28].Value = model.Sort;
            parameters[29].Value = model.Singleovertimeid;

            DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
            return model.ID;
        }
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public void Update(ESP.Administrative.Entity.AttendanceInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update AD_Attendance set ");
            strSql.Append("UserID=@UserID,");
            strSql.Append("UserCode=@UserCode,");
            strSql.Append("EmployeeName=@EmployeeName,");
            strSql.Append("AttendanceDate=@AttendanceDate,");
            strSql.Append("GoWorkTime=@GoWorkTime,");
            strSql.Append("OffWorkTime=@OffWorkTime,");
            strSql.Append("IsLate=@IsLate,");
            strSql.Append("IsLeaveEarly=@IsLeaveEarly,");
            strSql.Append("IsLeave=@IsLeave,");
            strSql.Append("LeaveID=@LeaveID,");
            strSql.Append("LeaveType=@LeaveType,");
            strSql.Append("IsAbsent=@IsAbsent,");
            strSql.Append("AbsentDays=@AbsentDays,");
            strSql.Append("IsOverTime=@IsOverTime,");
            strSql.Append("OverTimeHours=@OverTimeHours,");
            strSql.Append("IsAnnualLeave=@IsAnnualLeave,");
            strSql.Append("AnnualLeaveDays=@AnnualLeaveDays,");
            strSql.Append("IsEvection=@IsEvection,");
            strSql.Append("EvectionDays=@EvectionDays,");
            strSql.Append("IsEgress=@IsEgress,");
            strSql.Append("EgressHours=@EgressHours,");
            strSql.Append("Other=@Other,");
            strSql.Append("Remark=@Remark,");
            strSql.Append("Deleted=@Deleted,");
            strSql.Append("CreateTime=@CreateTime,");
            strSql.Append("UpdateTime=@UpdateTime,");
            strSql.Append("OperateorID=@OperateorID,");
            strSql.Append("OperateorDept=@OperateorDept,");
            strSql.Append("Sort=@Sort,");
            strSql.Append("SingleOverTimeID=@SingleOverTimeID");
            strSql.Append(" where ID=@ID");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4),
					new SqlParameter("@UserID", SqlDbType.Int,4),
					new SqlParameter("@UserCode", SqlDbType.NVarChar),
					new SqlParameter("@EmployeeName", SqlDbType.NVarChar),
                    new SqlParameter("@AttendanceDate", SqlDbType.DateTime),
					new SqlParameter("@GoWorkTime", SqlDbType.DateTime),
					new SqlParameter("@OffWorkTime", SqlDbType.DateTime),
					new SqlParameter("@IsLate", SqlDbType.Bit,1),
					new SqlParameter("@IsLeaveEarly", SqlDbType.Bit,1),
					new SqlParameter("@IsLeave", SqlDbType.Bit,1),
					new SqlParameter("@LeaveID", SqlDbType.Int,4),
					new SqlParameter("@LeaveType", SqlDbType.Int,4),
					new SqlParameter("@IsAbsent", SqlDbType.Bit,1),
					new SqlParameter("@AbsentDays", SqlDbType.Decimal,9),
					new SqlParameter("@IsOverTime", SqlDbType.Bit,1),
					new SqlParameter("@OverTimeHours", SqlDbType.Int,4),
					new SqlParameter("@IsAnnualLeave", SqlDbType.Bit,1),
					new SqlParameter("@AnnualLeaveDays", SqlDbType.Decimal,9),
					new SqlParameter("@IsEvection", SqlDbType.Bit,1),
					new SqlParameter("@EvectionDays", SqlDbType.Int,4),
					new SqlParameter("@IsEgress", SqlDbType.Bit,1),
					new SqlParameter("@EgressHours", SqlDbType.Decimal,9),
					new SqlParameter("@Other", SqlDbType.NVarChar),
					new SqlParameter("@Remark", SqlDbType.NVarChar),
					new SqlParameter("@Deleted", SqlDbType.Bit,1),
					new SqlParameter("@CreateTime", SqlDbType.DateTime),
					new SqlParameter("@UpdateTime", SqlDbType.DateTime),
					new SqlParameter("@OperateorID", SqlDbType.Int,4),
					new SqlParameter("@OperateorDept", SqlDbType.Int,4),
					new SqlParameter("@Sort", SqlDbType.Int,4),
                    new SqlParameter("@SingleOverTimeID", SqlDbType.Int, 4)};
            parameters[0].Value = model.ID;
            parameters[1].Value = model.UserID;
            parameters[2].Value = model.UserCode;
            parameters[3].Value = model.EmployeeName;
            parameters[4].Value = model.Attendancedate;
            parameters[5].Value = model.GoWorkTime;
            parameters[6].Value = model.OffWorkTime;
            parameters[7].Value = model.IsLate;
            parameters[8].Value = model.IsLeaveEarly;
            parameters[9].Value = model.IsLeave;
            parameters[10].Value = model.LeaveID;
            parameters[11].Value = model.LeaveType;
            parameters[12].Value = model.IsAbsent;
            parameters[13].Value = model.AbsentDays;
            parameters[14].Value = model.IsOverTime;
            parameters[15].Value = model.OverTimeHours;
            parameters[16].Value = model.IsAnnualLeave;
            parameters[17].Value = model.AnnualLeaveDays;
            parameters[18].Value = model.IsEvection;
            parameters[19].Value = model.EvectionDays;
            parameters[20].Value = model.IsEgress;
            parameters[21].Value = model.EgressHours;
            parameters[22].Value = model.Other;
            parameters[23].Value = model.Remark;
            parameters[24].Value = model.Deleted;
            parameters[25].Value = model.CreateTime;
            parameters[26].Value = model.UpdateTime;
            parameters[27].Value = model.OperateorID;
            parameters[28].Value = model.OperateorDept;
            parameters[29].Value = model.Sort;
            parameters[30].Value = model.Singleovertimeid;

            DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public void Delete(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete AD_Attendance ");
            strSql.Append(" where ID=@ID");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)
				};
            parameters[0].Value = ID;
            DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public ESP.Administrative.Entity.AttendanceInfo GetModel(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from AD_Attendance ");
            strSql.Append(" where ID=@ID");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)};
            parameters[0].Value = ID;
           
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            
            if (ds.Tables[0].Rows.Count > 0)
                return GetDataModel(ds);
            else
                return null;
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public ESP.Administrative.Entity.AttendanceInfo GetModel(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from AD_Attendance ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }

            DataSet ds = DbHelperSQL.Query(strSql.ToString());

            if (ds.Tables[0].Rows.Count > 0)
                return GetDataModel(ds);
            else
                return null;
        }


        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * FROM AD_Attendance ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }         

        private ESP.Administrative.Entity.AttendanceInfo GetDataModel(DataSet ds)
        {
            ESP.Administrative.Entity.AttendanceInfo model = new ESP.Administrative.Entity.AttendanceInfo();
            model.PopupData(ds.Tables[0].Rows[0]);
            return model;
        }
        #endregion  成员方法
    }
}