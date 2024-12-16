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
    /// <summary>
    /// 考勤统计数据操作类
    /// </summary>
    public class AttendanceStatisticInfoDataProvider
    {
        public AttendanceStatisticInfoDataProvider()
        { }
        #region  成员方法
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from AD_AttendanceStatistic");
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
        public int Add(AttendanceStatisticInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into AD_AttendanceStatistic(");
            strSql.Append("UserID,UserCode,UserName,EmployeeName,Level1ID,Level2ID,Level3ID,Department,Position,AttendanceYear,AttendanceMonth,LateCount1,LateCount2,OverTimeCount,AbsentCount1,AbsentCount2,AbsentCount3,LeaveEarly,AttendanceType)");
            strSql.Append(" values (");
            strSql.Append("@UserID,@UserCode,@UserName,@EmployeeName,@Level1ID,@Level2ID,@Level3ID,@Department,@Position,@AttendanceYear,@AttendanceMonth,@LateCount1,@LateCount2,@OverTimeCount,@AbsentCount1,@AbsentCount2,@AbsentCount3,@LeaveEarly,@AttendanceType)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@UserID", SqlDbType.Int,4),
					new SqlParameter("@UserCode", SqlDbType.NVarChar),
					new SqlParameter("@UserName", SqlDbType.NVarChar),
					new SqlParameter("@EmployeeName", SqlDbType.NVarChar),
					new SqlParameter("@Level1ID", SqlDbType.Int,4),
					new SqlParameter("@Level2ID", SqlDbType.Int,4),
					new SqlParameter("@Level3ID", SqlDbType.Int,4),
					new SqlParameter("@Department", SqlDbType.NVarChar),
					new SqlParameter("@Position", SqlDbType.NVarChar),
					new SqlParameter("@AttendanceYear", SqlDbType.Int,4),
					new SqlParameter("@AttendanceMonth", SqlDbType.Int,4),
					new SqlParameter("@LateCount1", SqlDbType.Int,4),
					new SqlParameter("@LateCount2", SqlDbType.Int,4),
					new SqlParameter("@OverTimeCount", SqlDbType.Int,4),
					new SqlParameter("@AbsentCount1", SqlDbType.Int,4),
					new SqlParameter("@AbsentCount2", SqlDbType.Int,4),
					new SqlParameter("@AbsentCount3", SqlDbType.Int,4),
					new SqlParameter("@LeaveEarly", SqlDbType.Int,4),
                    new SqlParameter("@AttendanceType", SqlDbType.Int, 4)};
            parameters[0].Value = model.UserID;
            parameters[1].Value = model.UserCode;
            parameters[2].Value = model.UserName;
            parameters[3].Value = model.EmployeeName;
            parameters[4].Value = model.Level1ID;
            parameters[5].Value = model.Level2ID;
            parameters[6].Value = model.Level3ID;
            parameters[7].Value = model.Department;
            parameters[8].Value = model.Position;
            parameters[9].Value = model.AttendanceYear;
            parameters[10].Value = model.AttendanceMonth;
            parameters[11].Value = model.LateCount1;
            parameters[12].Value = model.LateCount2;
            parameters[13].Value = model.OverTimeCount;
            parameters[14].Value = model.AbsentCount1;
            parameters[15].Value = model.AbsentCount2;
            parameters[16].Value = model.AbsentCount3;
            parameters[17].Value = model.LeaveEarly;
            parameters[18].Value = model.AttendanceType;

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
        /// 更新一条数据
        /// </summary>
        public void Update(AttendanceStatisticInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update AD_AttendanceStatistic set ");
            strSql.Append("UserID=@UserID,");
            strSql.Append("UserCode=@UserCode,");
            strSql.Append("UserName=@UserName,");
            strSql.Append("EmployeeName=@EmployeeName,");
            strSql.Append("Level1ID=@Level1ID,");
            strSql.Append("Level2ID=@Level2ID,");
            strSql.Append("Level3ID=@Level3ID,");
            strSql.Append("Department=@Department,");
            strSql.Append("Position=@Position,");
            strSql.Append("AttendanceYear=@AttendanceYear,");
            strSql.Append("AttendanceMonth=@AttendanceMonth,");
            strSql.Append("LateCount1=@LateCount1,");
            strSql.Append("LateCount2=@LateCount2,");
            strSql.Append("OverTimeCount=@OverTimeCount,");
            strSql.Append("AbsentCount1=@AbsentCount1,");
            strSql.Append("AbsentCount2=@AbsentCount2,");
            strSql.Append("AbsentCount3=@AbsentCount3,");
            strSql.Append("LeaveEarly=@LeaveEarly,");
            strSql.Append("AttendanceType=@AttendanceType ");
            strSql.Append(" where ID=@ID");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4),
					new SqlParameter("@UserID", SqlDbType.Int,4),
					new SqlParameter("@UserCode", SqlDbType.NVarChar),
					new SqlParameter("@UserName", SqlDbType.NVarChar),
					new SqlParameter("@EmployeeName", SqlDbType.NVarChar),
					new SqlParameter("@Level1ID", SqlDbType.Int,4),
					new SqlParameter("@Level2ID", SqlDbType.Int,4),
					new SqlParameter("@Level3ID", SqlDbType.Int,4),
					new SqlParameter("@Department", SqlDbType.NVarChar),
					new SqlParameter("@Position", SqlDbType.NVarChar),
					new SqlParameter("@AttendanceYear", SqlDbType.Int,4),
					new SqlParameter("@AttendanceMonth", SqlDbType.Int,4),
					new SqlParameter("@LateCount1", SqlDbType.Int,4),
					new SqlParameter("@LateCount2", SqlDbType.Int,4),
					new SqlParameter("@OverTimeCount", SqlDbType.Int,4),
					new SqlParameter("@AbsentCount1", SqlDbType.Int,4),
					new SqlParameter("@AbsentCount2", SqlDbType.Int,4),
					new SqlParameter("@AbsentCount3", SqlDbType.Int,4),
					new SqlParameter("@LeaveEarly", SqlDbType.Int,4),
                    new SqlParameter("@AttendanceType", SqlDbType.Int, 4)};
            parameters[0].Value = model.ID;
            parameters[1].Value = model.UserID;
            parameters[2].Value = model.UserCode;
            parameters[3].Value = model.UserName;
            parameters[4].Value = model.EmployeeName;
            parameters[5].Value = model.Level1ID;
            parameters[6].Value = model.Level2ID;
            parameters[7].Value = model.Level3ID;
            parameters[8].Value = model.Department;
            parameters[9].Value = model.Position;
            parameters[10].Value = model.AttendanceYear;
            parameters[11].Value = model.AttendanceMonth;
            parameters[12].Value = model.LateCount1;
            parameters[13].Value = model.LateCount2;
            parameters[14].Value = model.OverTimeCount;
            parameters[15].Value = model.AbsentCount1;
            parameters[16].Value = model.AbsentCount2;
            parameters[17].Value = model.AbsentCount3;
            parameters[18].Value = model.LeaveEarly;
            parameters[19].Value = model.AttendanceType;

            DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public void Delete(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete AD_AttendanceStatistic ");
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
        public AttendanceStatisticInfo GetModel(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from AD_AttendanceStatistic ");
            strSql.Append(" where ID=@ID");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)};
            parameters[0].Value = ID;
            AttendanceStatisticInfo model = new AttendanceStatisticInfo();
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
            strSql.Append("select * FROM AD_AttendanceStatistic ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }


        /// <summary>
        /// 删除该月的统计信息
        /// </summary>
        /// <param name="year">年份</param>
        /// <param name="month">月份</param>
        public void DeleteAttendanceStatInfos(int year, int month)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("DELETE AD_AttendanceStatistic ");
            strSql.Append(" WHERE AttendanceYear=@Year AND AttendanceMonth=@Month");
            SqlParameter[] parameters = {
					new SqlParameter("@Year", SqlDbType.Int,4),
                    new SqlParameter("@Month", SqlDbType.Int,4)
				};
            parameters[0].Value = year;
            parameters[1].Value = month;
            DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 获得考勤统计信息
        /// </summary>
        /// <param name="strWhere">查询条件</param>
        /// <param name="parameterList">查询参数</param>
        /// <returns>返回考勤统计信息集合</returns>
        public DataSet GetList(string strWhere, List<SqlParameter> parameterList)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * FROM AD_AttendanceStatistic ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" ORDER BY usercode ");
            return DbHelperSQL.Query(strSql.ToString(), parameterList.ToArray());
        }

        /// <summary>
        /// 获得考勤统计信息
        /// </summary>
        /// <param name="UserID">用户编号</param>
        /// <param name="year">年份</param>
        /// <param name="month">月份</param>
        /// <returns></returns>
        public AttendanceStatisticInfo GetAttendanceStatisticModel(int UserID, int year, int month)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * FROM AD_AttendanceStatistic where userid=@userid and AttendanceYear=@Year and AttendanceMonth=@month");
            SqlParameter[] parameters = {
                    new SqlParameter("@userid", SqlDbType.Int, 4),
					new SqlParameter("@Year", SqlDbType.Int,4),
                    new SqlParameter("@Month", SqlDbType.Int,4)
            };

            parameters[0].Value = UserID;
            parameters[1].Value = year;
            parameters[2].Value = month;
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            AttendanceStatisticInfo model = new AttendanceStatisticInfo();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
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
        /// 获得考勤提醒人员信息列表。
        /// </summary>
        /// <param name="year">年份。</param>
        /// <param name="month">月份。</param>
        public DataSet GetAttendanceRemind(int year, int month)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM AD_AttendanceStatistic a ");
            strSql.Append("LEFT JOIN sep_users u ON a.userid=u.userid ");
            strSql.Append(" WHERE AttendanceType=@AttendanceType AND AttendanceYear=@AttendanceYear ");
            strSql.Append(" AND AttendanceMonth=@AttendanceMonth ");
            strSql.Append(" AND AbsentCount1 + absentCount2 + absentcount3 >= 3");
            SqlParameter[] parameters = {
                    new SqlParameter("@AttendanceType", SqlDbType.Int, Status.UserBasicAttendanceType_Normal),
					new SqlParameter("@Year", SqlDbType.Int, year),
                    new SqlParameter("@Month", SqlDbType.Int, month)
            };
            return DbHelperSQL.Query(strSql.ToString(), parameters);
        }

        #endregion  成员方法
    }
}