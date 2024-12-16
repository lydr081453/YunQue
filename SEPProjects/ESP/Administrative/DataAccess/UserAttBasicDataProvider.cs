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
    public class UserAttBasicDataProvider
    {
        public UserAttBasicDataProvider()
        { }
        #region  成员方法

        /// <summary>
        /// 得到最大ID
        /// </summary>
        public int GetMaxId()
        {
            return DbHelperSQL.GetMaxID("ID", "AD_UserAttBasicInfo");
        }

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from AD_UserAttBasicInfo");
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
        public int Add(ESP.Administrative.Entity.UserAttBasicInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into AD_UserAttBasicInfo(");
            strSql.Append("Userid,UserCode,UserName,EmployeeName,CardNo,GoWorkTime,OffWorkTime,AttendanceType,AnnualLeaveBase,CardState,CardEnableTime,CardUnEnableTime,Deleted,CreateTime,UpdateTime,OperateorID,OperateorDept,Sort,Remark,AreaID,WorkTimeBeginDate)");
            strSql.Append(" values (");
            strSql.Append("@Userid,@UserCode,@UserName,@EmployeeName,@CardNo,@GoWorkTime,@OffWorkTime,@AttendanceType,@AnnualLeaveBase,@CardState,@CardEnableTime,@CardUnEnableTime,@Deleted,@CreateTime,@UpdateTime,@OperateorID,@OperateorDept,@Sort,@Remark,@AreaID,@WorkTimeBeginDate)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@Userid", SqlDbType.Int,4),
					new SqlParameter("@UserCode", SqlDbType.NVarChar),
					new SqlParameter("@UserName", SqlDbType.NVarChar),
					new SqlParameter("@EmployeeName", SqlDbType.NVarChar),
					new SqlParameter("@CardNo", SqlDbType.NVarChar),
					new SqlParameter("@GoWorkTime", SqlDbType.NVarChar),
					new SqlParameter("@OffWorkTime", SqlDbType.NVarChar),
					new SqlParameter("@AttendanceType", SqlDbType.Int,4),
					new SqlParameter("@AnnualLeaveBase", SqlDbType.Int,4),
					new SqlParameter("@CardState", SqlDbType.Int,4),
					new SqlParameter("@CardEnableTime", SqlDbType.DateTime),
					new SqlParameter("@CardUnEnableTime", SqlDbType.DateTime),
					new SqlParameter("@Deleted", SqlDbType.Bit,1),
					new SqlParameter("@CreateTime", SqlDbType.DateTime),
					new SqlParameter("@UpdateTime", SqlDbType.DateTime),
					new SqlParameter("@OperateorID", SqlDbType.Int,4),
					new SqlParameter("@OperateorDept", SqlDbType.Int,4),
					new SqlParameter("@Sort", SqlDbType.Int,4),
                    new SqlParameter("@Remark", SqlDbType.NVarChar),
                    new SqlParameter("@AreaID", SqlDbType.Int, 4),
                    new SqlParameter("@WorkTimeBeginDate", SqlDbType.DateTime)};
            parameters[0].Value = model.Userid;
            parameters[1].Value = model.UserCode;
            parameters[2].Value = model.UserName;
            parameters[3].Value = model.EmployeeName;
            parameters[4].Value = model.CardNo;
            parameters[5].Value = model.GoWorkTime;
            parameters[6].Value = model.OffWorkTime;
            parameters[7].Value = model.AttendanceType;
            parameters[8].Value = model.AnnualLeaveBase;
            parameters[9].Value = model.CardState;
            parameters[10].Value = model.CardEnableTime;
            parameters[11].Value = model.CardUnEnableTime;
            parameters[12].Value = model.Deleted;
            parameters[13].Value = model.CreateTime;
            parameters[14].Value = model.UpdateTime;
            parameters[15].Value = model.OperateorID;
            parameters[16].Value = model.OperateorDept;
            parameters[17].Value = model.Sort;
            parameters[18].Value = model.Remark;
            parameters[19].Value = model.AreaID;
            parameters[20].Value = model.WorkTimeBeginDate;

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
        public int Add(ESP.Administrative.Entity.UserAttBasicInfo model, SqlConnection conn, SqlTransaction trans)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into AD_UserAttBasicInfo(");
            strSql.Append("Userid,UserCode,UserName,EmployeeName,CardNo,GoWorkTime,OffWorkTime,AttendanceType,AnnualLeaveBase,CardState,CardEnableTime,CardUnEnableTime,Deleted,CreateTime,UpdateTime,OperateorID,OperateorDept,Sort,Remark, AreaID,WorkTimeBeginDate)");
            strSql.Append(" values (");
            strSql.Append("@Userid,@UserCode,@UserName,@EmployeeName,@CardNo,@GoWorkTime,@OffWorkTime,@AttendanceType,@AnnualLeaveBase,@CardState,@CardEnableTime,@CardUnEnableTime,@Deleted,@CreateTime,@UpdateTime,@OperateorID,@OperateorDept,@Sort,@Remark,@AreaID,@WorkTimeBeginDate)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@Userid", SqlDbType.Int,4),
					new SqlParameter("@UserCode", SqlDbType.NVarChar),
					new SqlParameter("@UserName", SqlDbType.NVarChar),
					new SqlParameter("@EmployeeName", SqlDbType.NVarChar),
					new SqlParameter("@CardNo", SqlDbType.NVarChar),
					new SqlParameter("@GoWorkTime", SqlDbType.NVarChar),
					new SqlParameter("@OffWorkTime", SqlDbType.NVarChar),
					new SqlParameter("@AttendanceType", SqlDbType.Int,4),
					new SqlParameter("@AnnualLeaveBase", SqlDbType.Int,4),
					new SqlParameter("@CardState", SqlDbType.Int,4),
					new SqlParameter("@CardEnableTime", SqlDbType.DateTime),
					new SqlParameter("@CardUnEnableTime", SqlDbType.DateTime),
					new SqlParameter("@Deleted", SqlDbType.Bit,1),
					new SqlParameter("@CreateTime", SqlDbType.DateTime),
					new SqlParameter("@UpdateTime", SqlDbType.DateTime),
					new SqlParameter("@OperateorID", SqlDbType.Int,4),
					new SqlParameter("@OperateorDept", SqlDbType.Int,4),
					new SqlParameter("@Sort", SqlDbType.Int,4),
                    new SqlParameter("@Remark", SqlDbType.NVarChar), 
                    new SqlParameter("@AreaID", SqlDbType.Int, 4),
                    new SqlParameter("@WorkTimeBeginDate", SqlDbType.DateTime)};
            parameters[0].Value = model.Userid;
            parameters[1].Value = model.UserCode;
            parameters[2].Value = model.UserName;
            parameters[3].Value = model.EmployeeName;
            parameters[4].Value = model.CardNo;
            parameters[5].Value = model.GoWorkTime;
            parameters[6].Value = model.OffWorkTime;
            parameters[7].Value = model.AttendanceType;
            parameters[8].Value = model.AnnualLeaveBase;
            parameters[9].Value = model.CardState;
            parameters[10].Value = model.CardEnableTime;
            parameters[11].Value = model.CardUnEnableTime;
            parameters[12].Value = model.Deleted;
            parameters[13].Value = model.CreateTime;
            parameters[14].Value = model.UpdateTime;
            parameters[15].Value = model.OperateorID;
            parameters[16].Value = model.OperateorDept;
            parameters[17].Value = model.Sort;
            parameters[18].Value = model.Remark;
            parameters[19].Value = model.AreaID;
            parameters[20].Value = model.WorkTimeBeginDate;

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
        public void Update(ESP.Administrative.Entity.UserAttBasicInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update AD_UserAttBasicInfo set ");
            strSql.Append("Userid=@Userid,");
            strSql.Append("UserCode=@UserCode,");
            strSql.Append("UserName=@UserName,");
            strSql.Append("EmployeeName=@EmployeeName,");
            strSql.Append("CardNo=@CardNo,");
            strSql.Append("GoWorkTime=@GoWorkTime,");
            strSql.Append("OffWorkTime=@OffWorkTime,");
            strSql.Append("AttendanceType=@AttendanceType,");
            strSql.Append("AnnualLeaveBase=@AnnualLeaveBase,");
            strSql.Append("CardState=@CardState,");
            strSql.Append("CardEnableTime=@CardEnableTime,");
            strSql.Append("CardUnEnableTime=@CardUnEnableTime,");
            strSql.Append("Deleted=@Deleted,");
            strSql.Append("CreateTime=@CreateTime,");
            strSql.Append("UpdateTime=@UpdateTime,");
            strSql.Append("OperateorID=@OperateorID,");
            strSql.Append("OperateorDept=@OperateorDept,");
            strSql.Append("Sort=@Sort,");
            strSql.Append("Remark=@Remark,");
            strSql.Append("AreaID=@AreaID,");
            strSql.Append("WorkTimeBeginDate=@WorkTimeBeginDate ");
            strSql.Append(" where ID=@ID");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4),
					new SqlParameter("@Userid", SqlDbType.Int,4),
					new SqlParameter("@UserCode", SqlDbType.NVarChar),
					new SqlParameter("@UserName", SqlDbType.NVarChar),
					new SqlParameter("@EmployeeName", SqlDbType.NVarChar),
					new SqlParameter("@CardNo", SqlDbType.NVarChar),
					new SqlParameter("@GoWorkTime", SqlDbType.NVarChar),
					new SqlParameter("@OffWorkTime", SqlDbType.NVarChar),
					new SqlParameter("@AttendanceType", SqlDbType.Int,4),
					new SqlParameter("@AnnualLeaveBase", SqlDbType.Int,4),
					new SqlParameter("@CardState", SqlDbType.Int,4),
					new SqlParameter("@CardEnableTime", SqlDbType.DateTime),
					new SqlParameter("@CardUnEnableTime", SqlDbType.DateTime),
					new SqlParameter("@Deleted", SqlDbType.Bit,1),
					new SqlParameter("@CreateTime", SqlDbType.DateTime),
					new SqlParameter("@UpdateTime", SqlDbType.DateTime),
					new SqlParameter("@OperateorID", SqlDbType.Int,4),
					new SqlParameter("@OperateorDept", SqlDbType.Int,4),
					new SqlParameter("@Sort", SqlDbType.Int,4),
                    new SqlParameter("@Remark", SqlDbType.NVarChar),
                    new SqlParameter("@AreaID", SqlDbType.Int, 4),
                    new SqlParameter("@WorkTimeBeginDate", SqlDbType.DateTime)};
            parameters[0].Value = model.ID;
            parameters[1].Value = model.Userid;
            parameters[2].Value = model.UserCode;
            parameters[3].Value = model.UserName;
            parameters[4].Value = model.EmployeeName;
            parameters[5].Value = model.CardNo;
            parameters[6].Value = model.GoWorkTime;
            parameters[7].Value = model.OffWorkTime;
            parameters[8].Value = model.AttendanceType;
            parameters[9].Value = model.AnnualLeaveBase;
            parameters[10].Value = model.CardState;
            parameters[11].Value = model.CardEnableTime;
            parameters[12].Value = model.CardUnEnableTime;
            parameters[13].Value = model.Deleted;
            parameters[14].Value = model.CreateTime;
            parameters[15].Value = model.UpdateTime;
            parameters[16].Value = model.OperateorID;
            parameters[17].Value = model.OperateorDept;
            parameters[18].Value = model.Sort;
            parameters[19].Value = model.Remark;
            parameters[20].Value = model.AreaID;
            parameters[21].Value = model.WorkTimeBeginDate;

            DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 更新多条数据
        /// </summary>
        public void Update(string newUserCode, int userId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update AD_UserAttBasicInfo set ");
            strSql.Append("UserCode=@newUserCode");
            strSql.Append(" where UserId=@userId");
            SqlParameter[] parameters = {
					new SqlParameter("@newUserCode", SqlDbType.NVarChar),
                    new SqlParameter("@userId", SqlDbType.NVarChar)};
            parameters[0].Value = newUserCode;
            parameters[1].Value = userId;

            DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public void Update(ESP.Administrative.Entity.UserAttBasicInfo model, SqlConnection conn, SqlTransaction trans)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update AD_UserAttBasicInfo set ");
            strSql.Append("Userid=@Userid,");
            strSql.Append("UserCode=@UserCode,");
            strSql.Append("UserName=@UserName,");
            strSql.Append("EmployeeName=@EmployeeName,");
            strSql.Append("CardNo=@CardNo,");
            strSql.Append("GoWorkTime=@GoWorkTime,");
            strSql.Append("OffWorkTime=@OffWorkTime,");
            strSql.Append("AttendanceType=@AttendanceType,");
            strSql.Append("AnnualLeaveBase=@AnnualLeaveBase,");
            strSql.Append("CardState=@CardState,");
            strSql.Append("CardEnableTime=@CardEnableTime,");
            strSql.Append("CardUnEnableTime=@CardUnEnableTime,");
            strSql.Append("Deleted=@Deleted,");
            strSql.Append("CreateTime=@CreateTime,");
            strSql.Append("UpdateTime=@UpdateTime,");
            strSql.Append("OperateorID=@OperateorID,");
            strSql.Append("OperateorDept=@OperateorDept,");
            strSql.Append("Sort=@Sort,");
            strSql.Append("Remark=@Remark,");
            strSql.Append("AreaID=@AreaID,");
            strSql.Append("WorkTimeBeginDate=@WorkTimeBeginDate ");
            strSql.Append(" where ID=@ID");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4),
					new SqlParameter("@Userid", SqlDbType.Int,4),
					new SqlParameter("@UserCode", SqlDbType.NVarChar),
					new SqlParameter("@UserName", SqlDbType.NVarChar),
					new SqlParameter("@EmployeeName", SqlDbType.NVarChar),
					new SqlParameter("@CardNo", SqlDbType.NVarChar),
					new SqlParameter("@GoWorkTime", SqlDbType.NVarChar),
					new SqlParameter("@OffWorkTime", SqlDbType.NVarChar),
					new SqlParameter("@AttendanceType", SqlDbType.Int,4),
					new SqlParameter("@AnnualLeaveBase", SqlDbType.Int,4),
					new SqlParameter("@CardState", SqlDbType.Int,4),
					new SqlParameter("@CardEnableTime", SqlDbType.DateTime),
					new SqlParameter("@CardUnEnableTime", SqlDbType.DateTime),
					new SqlParameter("@Deleted", SqlDbType.Bit,1),
					new SqlParameter("@CreateTime", SqlDbType.DateTime),
					new SqlParameter("@UpdateTime", SqlDbType.DateTime),
					new SqlParameter("@OperateorID", SqlDbType.Int,4),
					new SqlParameter("@OperateorDept", SqlDbType.Int,4),
					new SqlParameter("@Sort", SqlDbType.Int,4),
                    new SqlParameter("@Remark", SqlDbType.NVarChar),
                    new SqlParameter("@AreaID", SqlDbType.Int, 4),
                    new SqlParameter("@WorkTimeBeginDate", SqlDbType.DateTime)};
            parameters[0].Value = model.ID;
            parameters[1].Value = model.Userid;
            parameters[2].Value = model.UserCode;
            parameters[3].Value = model.UserName;
            parameters[4].Value = model.EmployeeName;
            parameters[5].Value = model.CardNo;
            parameters[6].Value = model.GoWorkTime;
            parameters[7].Value = model.OffWorkTime;
            parameters[8].Value = model.AttendanceType;
            parameters[9].Value = model.AnnualLeaveBase;
            parameters[10].Value = model.CardState;
            parameters[11].Value = model.CardEnableTime;
            parameters[12].Value = model.CardUnEnableTime;
            parameters[13].Value = model.Deleted;
            parameters[14].Value = model.CreateTime;
            parameters[15].Value = model.UpdateTime;
            parameters[16].Value = model.OperateorID;
            parameters[17].Value = model.OperateorDept;
            parameters[18].Value = model.Sort;
            parameters[19].Value = model.Remark;
            parameters[20].Value = model.AreaID;
            parameters[21].Value = model.WorkTimeBeginDate;

            DbHelperSQL.ExecuteSql(strSql.ToString(), conn, trans, parameters);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public void Delete(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete AD_UserAttBasicInfo ");
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
        public ESP.Administrative.Entity.UserAttBasicInfo GetModel(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from AD_UserAttBasicInfo ");
            strSql.Append(" where ID=@ID");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)};
            parameters[0].Value = ID;
            ESP.Administrative.Entity.UserAttBasicInfo model = new ESP.Administrative.Entity.UserAttBasicInfo();
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
        /// 得到一个对象实体
        /// </summary>
        public ESP.Administrative.Entity.UserAttBasicInfo GetModel(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from AD_UserAttBasicInfo ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            ESP.Administrative.Entity.UserAttBasicInfo model = new ESP.Administrative.Entity.UserAttBasicInfo();
            DataSet ds = DbHelperSQL.Query(strSql.ToString());
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
            strSql.Append("select * FROM AD_UserAttBasicInfo where deleted=0  ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" and " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere, List<SqlParameter> parameters)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * FROM AD_UserAttBasicInfo where deleted=0  ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" and " + strWhere);
            }
            strSql.Append(" ORDER BY CardEnableTime desc");
            return DbHelperSQL.Query(strSql.ToString(), parameters.ToArray());
        }

        /// <summary>
        /// 获得用户使用的门卡历史信息
        /// </summary>
        /// <param name="userID">用户编号</param>
        /// <returns>返回用户使用的门卡历史信息集合</returns>
        public List<UserAttBasicInfo> GetUserHistoryCard(int userID)
        {
            List<UserAttBasicInfo> list = new List<UserAttBasicInfo>();
            string strSql = "select * from AD_UserAttBasicInfo where deleted=0 and userid=@userid";
            SqlParameter[] parameter = { new SqlParameter("@userid", SqlDbType.Int, 4) };
            parameter[0].Value = userID;
            DataSet ds = DbHelperSQL.Query(strSql, parameter);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    UserAttBasicInfo model = new UserAttBasicInfo();
                    model.PopupData(dr);
                    list.Add(model);
                }
            }
            return list;
        }

        /// <summary>
        /// 获得考勤用户信息
        /// </summary>
        /// <param name="userids">用户编号字符串</param>
        /// <returns>返回考勤人员用户信息</returns>
        public DataSet GetAttendanceUserInfo(string userids)
        {
            StringBuilder sql = new StringBuilder();
            //sql.Append("SELECT u.*, d.level3id, d.level2id, d.level1id, d.level1+'-'+d.level2+'-'+d.level3 FROM AD_UserAttBasicInfo u ");
            //sql.Append("LEFT JOIN sep_EmployeesInPositions e ON u.userid = e.userid ");
            //sql.Append("LEFT JOIN V_Department d ON e.departmentid = d.level3Id ");
            //sql.Append("WHERE u.deleted=0 AND u.userid IN (" + userids + ")");

            sql.Append("SELECT u.*, a.*, d.level3id, d.level2id, d.level1id, d.level1+'-'+d.level2+'-'+d.level3 as Department FROM AD_UserAttBasicInfo u ");
            sql.Append(" LEFT JOIN sep_EmployeesInPositions e ON u.userid = e.userid ");
            sql.Append(" LEFT JOIN V_Department d ON e.departmentid = d.level3Id ");
            sql.Append(" LEFT JOIN ad_v_Attendance a on u.userid=a.userid ");
            sql.Append(" WHERE u.deleted=0 AND u.userid IN (" + userids + ") and ");
            sql.Append(" a.AttendanceYear = 2009 AND a.AttendanceMonth = 7 AND a.isIn=1 ORDER BY a.clockin DESC ");
            SqlParameter[] parameter = { };
            return DbHelperSQL.Query(sql.ToString(), parameter);
        }

        public int UpdateUserCommuteType(int userid, int attendanceType)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" update AD_UserAttBasicInfo set attendanceType = " + attendanceType.ToString());
            strSql.Append(" Where UserId=@UserId ");
            SqlParameter[] parameters = { new SqlParameter("@UserId", SqlDbType.Int, 4) };
            parameters[0].Value = userid;
            return DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);

        }

        public int UpdateUserCommuteType(int userid, int attendanceType,SqlTransaction trans )
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" update AD_UserAttBasicInfo set attendanceType = " + attendanceType.ToString());
            strSql.Append(" Where UserId=@UserId ");
            SqlParameter[] parameters = { new SqlParameter("@UserId", SqlDbType.Int, 4) };
            parameters[0].Value = userid;
            return DbHelperSQL.ExecuteSql(strSql.ToString(), trans.Connection,trans, parameters);

        }


        /// <summary>
        /// 获得考勤人员考勤基本信息
        /// </summary>
        /// <param name="strWhere">查询条件</param>
        /// <returns></returns>
        public Dictionary<int, DataRow> GetUserAttBasicInfos(string strWhere)
        {
            string sql = @"SELECT u.*, d.level3id, d.level2id, d.level1id, d.level1+'-'+d.level2+'-'+d.level3 as Department, p.* 
                FROM AD_UserAttBasicInfo u 
                LEFT JOIN sep_Employees ep ON u.userid=ep.userid
                LEFT JOIN sep_EmployeesInPositions e ON u.userid = e.userid 
                LEFT JOIN sep_DepartmentPositions p on e.DepartmentPositionID=p.DepartmentPositionID
                LEFT JOIN V_Department d ON e.departmentid = d.level3Id 
                WHERE u.deleted=0 AND ep.status not in (5,6)";
            if (string.IsNullOrEmpty(strWhere))
            {
                sql += strWhere;
            }

            DataSet ds = DbHelperSQL.Query(sql.ToString());
            Dictionary<int, DataRow> userAttBasicDic = new Dictionary<int, DataRow>();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    int userid = int.Parse(dr["UserId"].ToString());

                    if (userAttBasicDic.ContainsKey(userid))
                    {
                        userAttBasicDic[userid] = dr;
                    }
                    else
                    {
                        userAttBasicDic.Add(userid, dr);
                    }
                }
            }
            return userAttBasicDic;
        }

        /// <summary>
        /// 获得门卡信息
        /// </summary>
        /// <param name="strWhere">查询条件</param>
        /// <param name="parameters">查询参数</param>
        /// <returns></returns>
        public DataSet GetCardNoInfos(string strWhere, List<SqlParameter> parameters)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT u.*, d.level3id, d.level2id, d.level1id, d.level1+'-'+d.level2+'-'+d.level3 as Department FROM AD_UserAttBasicInfo u ");
            strSql.Append(" LEFT JOIN sep_EmployeesInPositions e ON u.userid = e.userid ");
            strSql.Append(" LEFT JOIN V_Department d ON e.departmentid = d.level3Id ");
            strSql.Append(" WHERE u.deleted=0 ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" " + strWhere);
            }
            strSql.Append(" ORDER BY u.cardstate, u.CardenableTime DESC");
            return DbHelperSQL.Query(strSql.ToString(), parameters.ToArray());
        }

        /// <summary>
        /// 获得综合查询信息
        /// </summary>
        /// <param name="strWhere">查询条件</param>
        /// <param name="parameterList">查询参数</param>
        /// <returns>返回综合信息查询结果</returns>
        public DataSet GetIntegratedQueryUserInfo(string strWhere, List<SqlParameter> parameterList)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT u.*, p.*, d.level1id, d.level2id, d.level3id, d.level1+'-'+d.level2+'-'+d.level3 AS Department FROM ad_userattbasicinfo u ");
            strSql.Append(" LEFT JOIN SEP_Employees y ON u.userid=y.userid ");
            strSql.Append(" LEFT JOIN SEP_EmployeesInPositions e ON u.userid=e.userid ");
            strSql.Append(" LEFT JOIN SEP_DepartmentPositions p ON e.DepartmentPositionID=p.DepartmentPositionID ");
            strSql.Append(" LEFT JOIN V_Department d ON e.departmentid = d.level3Id ");
            strSql.Append(" WHERE u.deleted=0 ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" " + strWhere);
            }

            strSql.Append(" ORDER BY u.UserCode ");

            return DbHelperSQL.Query(strSql.ToString(), parameterList.ToArray());
            //where u.userid in 
            //(13662,13385,14003,13627,13631,13886,13628,13629,13987,14070,13591,13593,13595,13608,13606,13560,14102,13601,13607, 
            //13596,13604,14071,13613,14050,13962,13624,13623,13583,13879,14123,13589,13622,13621,13674,13618,13880,13614,13517, 
            //14030,13625,14089,13620,14035,14069,14073,14124,13615,13942,13986,13602,13588,13920,13575,13576,13578,13626,14106, 
            //13619,13616,13594) and u.deleted=0 and y.status in (4,5)
        }

        /// <summary>
        /// 获得最后一条考勤基础信息对象
        /// </summary>
        /// <param name="userid">用户编号</param>
        /// <returns>返回一个用户考勤基础信息对象</returns>
        public UserAttBasicInfo GetLastUserAttBasicInfo(int userid)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM AD_UserAttBasicInfo WHERE deleted=0 ");
            strSql.Append(" AND Userid=" + userid);
            strSql.Append(" ORDER BY id DESC ");

            ESP.Administrative.Entity.UserAttBasicInfo model = new ESP.Administrative.Entity.UserAttBasicInfo();
            DataSet ds = DbHelperSQL.Query(strSql.ToString());
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
        #endregion  成员方法
    }
}
