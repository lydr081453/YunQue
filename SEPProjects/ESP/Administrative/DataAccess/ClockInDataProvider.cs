using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using ESP.Administrative.Common;
using System.Collections;

namespace ESP.Administrative.DataAccess
{
    public class ClockInDataProvider
    {
        public ClockInDataProvider()
        { }
        #region  成员方法

        /// <summary>
        /// 得到最大ID
        /// </summary>
        public int GetMaxId()
        {
            return DbHelperSQL.GetMaxID("ID", "AD_ClockIn");
        }

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from AD_ClockIn");
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
        public int Add(ESP.Administrative.Entity.ClockInInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into AD_ClockIn(");
            strSql.Append("CardNO,ReadTime,InOrOut,DoorName,CreateTime,UserCode,Deleted,UpdateTime,OperatorID,OperatorName,Remark)");
            strSql.Append(" values (");
            strSql.Append("@CardNO,@ReadTime,@InOrOut,@DoorName,@CreateTime,@UserCode,@Deleted,@UpdateTime,@OperatorID,@OperatorName,@Remark)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@CardNO", SqlDbType.NVarChar),
					new SqlParameter("@ReadTime", SqlDbType.DateTime),
					new SqlParameter("@InOrOut", SqlDbType.Bit,1),
					new SqlParameter("@DoorName", SqlDbType.NVarChar),
					new SqlParameter("@CreateTime", SqlDbType.DateTime),
					new SqlParameter("@UserCode", SqlDbType.NVarChar),
					new SqlParameter("@Deleted", SqlDbType.Bit,1),
					new SqlParameter("@UpdateTime", SqlDbType.DateTime),
					new SqlParameter("@OperatorID", SqlDbType.Int,4),
					new SqlParameter("@OperatorName", SqlDbType.NVarChar),
					new SqlParameter("@Remark", SqlDbType.NVarChar)};
            parameters[0].Value = model.CardNO;
            parameters[1].Value = model.ReadTime;
            parameters[2].Value = model.InOrOut;
            parameters[3].Value = model.DoorName;
            parameters[4].Value = model.CreateTime;
            parameters[5].Value = model.UserCode;
            parameters[6].Value = model.Deleted;
            parameters[7].Value = model.UpdateTime;
            parameters[8].Value = model.OperatorID;
            parameters[9].Value = model.OperatorName;
            parameters[10].Value = model.Remark;

            DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
            return model.ID;
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public void Update(ESP.Administrative.Entity.ClockInInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update AD_ClockIn set ");
            strSql.Append("CardNO=@CardNO,");
            strSql.Append("ReadTime=@ReadTime,");
            strSql.Append("InOrOut=@InOrOut,");
            strSql.Append("DoorName=@DoorName,");
            strSql.Append("CreateTime=@CreateTime,");
            strSql.Append("UserCode=@UserCode,");
            strSql.Append("Deleted=@Deleted,");
            strSql.Append("UpdateTime=@UpdateTime,");
            strSql.Append("OperatorID=@OperatorID,");
            strSql.Append("OperatorName=@OperatorName,");
            strSql.Append("Remark=@Remark");
            strSql.Append(" where ID=@ID");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4),
					new SqlParameter("@CardNO", SqlDbType.NVarChar),
					new SqlParameter("@ReadTime", SqlDbType.DateTime),
					new SqlParameter("@InOrOut", SqlDbType.Bit,1),
					new SqlParameter("@DoorName", SqlDbType.NVarChar),
					new SqlParameter("@CreateTime", SqlDbType.DateTime),
					new SqlParameter("@UserCode", SqlDbType.NVarChar),
					new SqlParameter("@Deleted", SqlDbType.Bit,1),
					new SqlParameter("@UpdateTime", SqlDbType.DateTime),
					new SqlParameter("@OperatorID", SqlDbType.Int,4),
					new SqlParameter("@OperatorName", SqlDbType.NVarChar),
					new SqlParameter("@Remark", SqlDbType.NVarChar)};
            parameters[0].Value = model.ID;
            parameters[1].Value = model.CardNO;
            parameters[2].Value = model.ReadTime;
            parameters[3].Value = model.InOrOut;
            parameters[4].Value = model.DoorName;
            parameters[5].Value = model.CreateTime;
            parameters[6].Value = model.UserCode;
            parameters[7].Value = model.Deleted;
            parameters[8].Value = model.UpdateTime;
            parameters[9].Value = model.OperatorID;
            parameters[10].Value = model.OperatorName;
            parameters[11].Value = model.Remark;

            DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 更新多条数据
        /// </summary>
        public void Update(string newUserCode, string oldUserCode)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update AD_ClockIn set ");
            strSql.Append("UserCode=@newUserCode");
            strSql.Append(" where UserCode=@oldUserCode");
            SqlParameter[] parameters = {
					new SqlParameter("@newUserCode", SqlDbType.NVarChar),
					new SqlParameter("@oldUserCode", SqlDbType.NVarChar)};
            parameters[0].Value = newUserCode;
            parameters[1].Value = oldUserCode;
            DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }
        /// <summary>
        /// 删除一条数据
        /// </summary>
        public void Delete(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update AD_ClockIn set Deleted=1 ");
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
        public ESP.Administrative.Entity.ClockInInfo GetModel(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from AD_ClockIn ");
            strSql.Append(" where ID=@ID");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)};
            parameters[0].Value = ID;
            ESP.Administrative.Entity.ClockInInfo model = new ESP.Administrative.Entity.ClockInInfo();
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
            strSql.Append("select * FROM AD_ClockIn ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 执行存储过程,
        /// </summary>
        /// <param name="storedProcName">存储过程名</param>
        /// <param name="parameters">存储过程参数</param>
        /// <param name="tableName">DataSet结果中的表名</param>
        /// <returns>DataSet</returns>
        public void GetAttendanceTime(int Userid, DateTime AttendanceDate, out DateTime GoWorkTime, out DateTime OffWorkTime)
        {
            SqlParameter[] parameters = {
                        new SqlParameter("@Userid",SqlDbType.Int,4),
					    new SqlParameter("@AttendanceDate", SqlDbType.DateTime),
					    new SqlParameter("@GoWorkTime", SqlDbType.DateTime),
					    new SqlParameter("@OffWorkTime", SqlDbType.DateTime)};
            parameters[0].Value = Userid;
            parameters[1].Value = AttendanceDate;
            parameters[2].Direction = ParameterDirection.Output;
            parameters[3].Direction = ParameterDirection.Output;

            DbHelperSQL.RunProcedure("AttendanceTime", parameters, "ds");

            if (parameters[2].Value == System.DBNull.Value)
                GoWorkTime = new DateTime(1900, 1, 1);
            else
                GoWorkTime = (DateTime)parameters[2].Value;

            if (parameters[3].Value == System.DBNull.Value)
                OffWorkTime = new DateTime(1900, 1, 1);
            else
                OffWorkTime = (DateTime)parameters[3].Value;
        }


        public System.Collections.Hashtable GetClockInTimesOfMonth(int year, int month, int userId)
        {
            string sql = @"
SELECT ClockIn, AttendanceDay, IsIn, OperatorName, Remark FROM ad_v_Attendance
WHERE AttendanceYear = @Year AND AttendanceMonth = @Month AND UserId = @UserId ";
            using (IDataReader reader = DbHelperSQL.ExecuteReader(sql, new SqlParameter[]{
                new SqlParameter("@UserId", userId),
                new SqlParameter("@Year", year),
                new SqlParameter("@Month", month)
            }))
            {
                System.Collections.Hashtable t = new System.Collections.Hashtable();
                while (reader.Read())
                {
                    // 这个集合中保存：上下班时间、操作人（如果存在）、备注（如果存在）
                    ArrayList list = new ArrayList();
                    DateTime time = reader.GetDateTime(0);
                    int day = reader.GetInt32(1);
                    bool isIn = reader.GetBoolean(2);

                    long key = (long)day;
                    if (!isIn)
                        key = -key;
                    list.Add(time);
                    if (!reader.IsDBNull(3) && !string.IsNullOrEmpty(reader.GetString(3)))
                    {
                        list.Add(reader.GetString(3));
                    }
                    if (!reader.IsDBNull(4) && !string.IsNullOrEmpty(reader.GetString(4)))
                    {
                        list.Add(reader.GetString(4));
                    }
                    if (t.ContainsKey(key))
                        t[key] = list;
                    else
                        t.Add(key, list);
                }
                return t;
            }
        }

        /// <summary>
        /// 获得统计人员的打卡时间信息
        /// </summary>
        /// <param name="year">年份</param>
        /// <param name="month">月份</param>
        /// <param name="day1">日期，如果没有按日期查询则传一个0</param>
        /// <returns>返回所有统计人员的打卡记录信息</returns>
        public Dictionary<int, Dictionary<long, DateTime>> GetClockInTimes(int year, int month, int day1)
        {
            string sql = @"
                SELECT UserId, ClockIn, AttendanceYear, AttendanceMonth, AttendanceDay, IsIn FROM ad_v_Attendance
                WHERE AttendanceYear = @Year AND AttendanceMonth = @Month ";
            List<SqlParameter> parameterList = new List<SqlParameter>();
            parameterList.Add(new SqlParameter("@Year", year));
            parameterList.Add(new SqlParameter("@Month", month));
            if (day1 != 0)
            {
                sql += " AND AttendanceDay=@Day ";
                parameterList.Add(new SqlParameter("@Day", day1));
            }
            sql += " ORDER BY userid ";
            using (IDataReader reader = DbHelperSQL.ExecuteReader(sql, parameterList.ToArray()))
            {
                Dictionary<int, Dictionary<long, DateTime>> t = new Dictionary<int, Dictionary<long, DateTime>>();
                while (reader.Read())
                {
                    int userid = reader.GetInt32(0);
                    DateTime time = reader.GetDateTime(1);
                    int day = reader.GetInt32(4);
                    bool isIn = reader.GetBoolean(5);
                    long key = (long)day;
                    if (!isIn)
                        key = -key;
                    if (t.ContainsKey(userid))
                    {
                        Dictionary<long, DateTime> dic = t[userid];

                        if (dic.ContainsKey(key))
                        {
                            dic[key] = time;
                        }
                        else
                        {
                            dic.Add(key, time);
                        }
                        t[userid] = dic;
                    }
                    else
                    {
                        Dictionary<long, DateTime> dic = new Dictionary<long, DateTime>();
                        dic.Add(key, time);
                        t.Add(userid, dic);
                    }
                }
                return t;
            }
        }

        /// <summary>
        /// 获得当前选择日期前后三天的上下班时间
        /// </summary>
        /// <param name="date">选择日期</param>
        /// <param name="userId">用户编号</param>
        /// <returns>获得前后三天的上下班时间</returns>
        public Dictionary<int, DateTime> GetClockInTimes(DateTime date, int userId)
        {
            // 前一天
            DateTime yesterday = date.Date.AddDays(-1);
            // 后一天
            DateTime tomorrow = date.Date.AddDays(1);

            string sql = @"
                SELECT UserId, ClockIn, AttendanceYear, AttendanceMonth, AttendanceDay, IsIn FROM ad_v_Attendance
                WHERE UserId=@UserId AND ((AttendanceYear=@Year1 AND AttendanceMonth=@Month1 AND AttendanceDay=@Day1) 
                OR (AttendanceYear=@Year2 AND AttendanceMonth=@Month2 AND AttendanceDay=@Day2) 
                OR (AttendanceYear=@Year3 AND AttendanceMonth=@Month3 AND AttendanceDay=@Day3))";
            List<SqlParameter> parameterList = new List<SqlParameter>();
            parameterList.Add(new SqlParameter("@UserId", userId));
            parameterList.Add(new SqlParameter("@Year1", yesterday.Year));
            parameterList.Add(new SqlParameter("@Month1", yesterday.Month));
            parameterList.Add(new SqlParameter("@Day1", yesterday.Day));
            parameterList.Add(new SqlParameter("@Year2", date.Year));
            parameterList.Add(new SqlParameter("@Month2", date.Month));
            parameterList.Add(new SqlParameter("@Day2", date.Day));
            parameterList.Add(new SqlParameter("@Year3", tomorrow.Year));
            parameterList.Add(new SqlParameter("@Month3", tomorrow.Month));
            parameterList.Add(new SqlParameter("@Day3", tomorrow.Day));
            sql += " ORDER BY userid ";
            using (IDataReader reader = DbHelperSQL.ExecuteReader(sql, parameterList.ToArray()))
            {
                Dictionary<int, DateTime> t = new Dictionary<int, DateTime>();
                while (reader.Read())
                {
                    int userid = reader.GetInt32(0);
                    DateTime time = reader.GetDateTime(1);
                    int day = reader.GetInt32(4);
                    bool isIn = reader.GetBoolean(5);
                    int key = day;
                    if (!isIn)
                        key = -key;
                    if (t.ContainsKey(key))
                    {
                        t[key] = time;
                    }
                    else
                    {
                        t.Add(key, time);
                    }
                }
                return t;
            }
        }

        /// <summary>
        /// 通过员工编号获得用户信息
        /// </summary>
        /// <param name="userCode">员工编号</param>
        /// <returns>返回用户信息</returns>
        public ESP.Framework.Entity.EmployeeInfo GetEmployeeInfoByCode(String userCode)
        {
            string sql = @"
SELECT TOP 1 e.*, et.TypeName , u.Username, u.FirstNameCN, u.LastNameCN, u.FirstNameEN, u.LastNameEN, u.Email
    FROM sep_Employees e
    JOIN sep_Users u ON u.UserID = e.UserID
    LEFT JOIN sep_EmployeeTypes et ON e.TypeID=et.TypeID 
WHERE e.Code=@Code
ORDER BY  ISNULL(u.IsDeleted,0) ASC
";
            IDataReader reader = DbHelperSQL.ExecuteReader(sql, new SqlParameter("@Code", userCode));

            return ESP.Framework.DataAccess.Utilities.CBO.LoadObject<ESP.Framework.Entity.EmployeeInfo>(reader);
        }
        #endregion  成员方法
    }
}
