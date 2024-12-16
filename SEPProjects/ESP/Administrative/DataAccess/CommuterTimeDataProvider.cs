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
    public class CommuterTimeDataProvider
    {
        public CommuterTimeDataProvider()
        { }
        #region  成员方法
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from AD_CommuterTime");
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
        public int Add(CommuterTimeInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into AD_CommuterTime(");
            strSql.Append("UserID,UserCode,UserName,BeginTime,EndTime,GoWorkTime,OffWorkTime,AttendanceType,Deleted,CreateTime,UpdateTime,OperatorID,OperatorDept,Sort,WorkingDays_OverTime1,WorkingDays_OverTime2,LateGoWorkTime_OverTime1)");
            strSql.Append(" values (");
            strSql.Append("@UserID,@UserCode,@UserName,@BeginTime,@EndTime,@GoWorkTime,@OffWorkTime,@AttendanceType,@Deleted,@CreateTime,@UpdateTime,@OperatorID,@OperatorDept,@Sort,@WorkingDays_OverTime1,@WorkingDays_OverTime2,@LateGoWorkTime_OverTime1)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@UserID", SqlDbType.Int,4),
					new SqlParameter("@UserCode", SqlDbType.NVarChar),
					new SqlParameter("@UserName", SqlDbType.NVarChar),
					new SqlParameter("@BeginTime", SqlDbType.DateTime),
					new SqlParameter("@EndTime", SqlDbType.DateTime),
					new SqlParameter("@GoWorkTime", SqlDbType.DateTime),
					new SqlParameter("@OffWorkTime", SqlDbType.DateTime),
					new SqlParameter("@AttendanceType", SqlDbType.Int,4),
					new SqlParameter("@Deleted", SqlDbType.Bit,1),
					new SqlParameter("@CreateTime", SqlDbType.DateTime),
					new SqlParameter("@UpdateTime", SqlDbType.DateTime),
					new SqlParameter("@OperatorID", SqlDbType.Int,4),
					new SqlParameter("@OperatorDept", SqlDbType.Int,4),
					new SqlParameter("@Sort", SqlDbType.Int,4),
                    new SqlParameter("@WorkingDays_OverTime1", SqlDbType.Float),
                    new SqlParameter("@WorkingDays_OverTime2", SqlDbType.Float),
                    new SqlParameter("@LateGoWorkTime_OverTime1", SqlDbType.Float)};
            parameters[0].Value = model.UserID;
            parameters[1].Value = model.UserCode;
            parameters[2].Value = model.UserName;
            parameters[3].Value = model.BeginTime;
            parameters[4].Value = model.EndTime;
            parameters[5].Value = model.GoWorkTime;
            parameters[6].Value = model.OffWorkTime;
            parameters[7].Value = model.AttendanceType;
            parameters[8].Value = model.Deleted;
            parameters[9].Value = model.CreateTime;
            parameters[10].Value = model.UpdateTime;
            parameters[11].Value = model.OperatorID;
            parameters[12].Value = model.OperatorDept;
            parameters[13].Value = model.Sort;
            parameters[14].Value = model.WorkingDays_OverTime1;
            parameters[15].Value = model.WorkingDays_OverTime2;
            parameters[16].Value = model.LateGoWorkTime_OverTime1;

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

        public int Add(CommuterTimeInfo model, SqlConnection conn, SqlTransaction tran)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into AD_CommuterTime(");
            strSql.Append("UserID,UserCode,UserName,BeginTime,EndTime,GoWorkTime,OffWorkTime,AttendanceType,Deleted,CreateTime,UpdateTime,OperatorID,OperatorDept,Sort,WorkingDays_OverTime1,WorkingDays_OverTime2,LateGoWorkTime_OverTime1)");
            strSql.Append(" values (");
            strSql.Append("@UserID,@UserCode,@UserName,@BeginTime,@EndTime,@GoWorkTime,@OffWorkTime,@AttendanceType,@Deleted,@CreateTime,@UpdateTime,@OperatorID,@OperatorDept,@Sort,@WorkingDays_OverTime1,@WorkingDays_OverTime2,@LateGoWorkTime_OverTime1)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@UserID", SqlDbType.Int,4),
					new SqlParameter("@UserCode", SqlDbType.NVarChar),
					new SqlParameter("@UserName", SqlDbType.NVarChar),
					new SqlParameter("@BeginTime", SqlDbType.DateTime),
					new SqlParameter("@EndTime", SqlDbType.DateTime),
					new SqlParameter("@GoWorkTime", SqlDbType.DateTime),
					new SqlParameter("@OffWorkTime", SqlDbType.DateTime),
					new SqlParameter("@AttendanceType", SqlDbType.Int,4),
					new SqlParameter("@Deleted", SqlDbType.Bit,1),
					new SqlParameter("@CreateTime", SqlDbType.DateTime),
					new SqlParameter("@UpdateTime", SqlDbType.DateTime),
					new SqlParameter("@OperatorID", SqlDbType.Int,4),
					new SqlParameter("@OperatorDept", SqlDbType.Int,4),
					new SqlParameter("@Sort", SqlDbType.Int,4),
                    new SqlParameter("@WorkingDays_OverTime1", SqlDbType.Float),
                    new SqlParameter("@WorkingDays_OverTime2", SqlDbType.Float),
                    new SqlParameter("@LateGoWorkTime_OverTime1", SqlDbType.Float)};
            parameters[0].Value = model.UserID;
            parameters[1].Value = model.UserCode;
            parameters[2].Value = model.UserName;
            parameters[3].Value = model.BeginTime;
            parameters[4].Value = model.EndTime;
            parameters[5].Value = model.GoWorkTime;
            parameters[6].Value = model.OffWorkTime;
            parameters[7].Value = model.AttendanceType;
            parameters[8].Value = model.Deleted;
            parameters[9].Value = model.CreateTime;
            parameters[10].Value = model.UpdateTime;
            parameters[11].Value = model.OperatorID;
            parameters[12].Value = model.OperatorDept;
            parameters[13].Value = model.Sort;
            parameters[14].Value = model.WorkingDays_OverTime1;
            parameters[15].Value = model.WorkingDays_OverTime2;
            parameters[16].Value = model.LateGoWorkTime_OverTime1;

            object obj = DbHelperSQL.GetSingle(strSql.ToString(), conn, tran, parameters);
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
        public void Update(CommuterTimeInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update AD_CommuterTime set ");
            strSql.Append("UserID=@UserID,");
            strSql.Append("UserCode=@UserCode,");
            strSql.Append("UserName=@UserName,");
            strSql.Append("BeginTime=@BeginTime,");
            strSql.Append("EndTime=@EndTime,");
            strSql.Append("GoWorkTime=@GoWorkTime,");
            strSql.Append("OffWorkTime=@OffWorkTime,");
            strSql.Append("AttendanceType=@AttendanceType,");
            strSql.Append("Deleted=@Deleted,");
            strSql.Append("CreateTime=@CreateTime,");
            strSql.Append("UpdateTime=@UpdateTime,");
            strSql.Append("OperatorID=@OperatorID,");
            strSql.Append("OperatorDept=@OperatorDept,");
            strSql.Append("Sort=@Sort,");
            strSql.Append("WorkingDays_OverTime1=@WorkingDays_OverTime1,");
            strSql.Append("WorkingDays_OverTime2=@WorkingDays_OverTime2,");
            strSql.Append("LateGoWorkTime_OverTime1=@LateGoWorkTime_OverTime1 ");
            strSql.Append(" where ID=@ID");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4),
					new SqlParameter("@UserID", SqlDbType.Int,4),
					new SqlParameter("@UserCode", SqlDbType.NVarChar),
					new SqlParameter("@UserName", SqlDbType.NVarChar),
					new SqlParameter("@BeginTime", SqlDbType.DateTime),
					new SqlParameter("@EndTime", SqlDbType.DateTime),
					new SqlParameter("@GoWorkTime", SqlDbType.DateTime),
					new SqlParameter("@OffWorkTime", SqlDbType.DateTime),
					new SqlParameter("@AttendanceType", SqlDbType.Int,4),
					new SqlParameter("@Deleted", SqlDbType.Bit,1),
					new SqlParameter("@CreateTime", SqlDbType.DateTime),
					new SqlParameter("@UpdateTime", SqlDbType.DateTime),
					new SqlParameter("@OperatorID", SqlDbType.Int,4),
					new SqlParameter("@OperatorDept", SqlDbType.Int,4),
					new SqlParameter("@Sort", SqlDbType.Int,4),
                    new SqlParameter("@WorkingDays_OverTime1", SqlDbType.Float),
                    new SqlParameter("@WorkingDays_OverTime2", SqlDbType.Float),
                    new SqlParameter("@LateGoWorkTime_OverTime1", SqlDbType.Float)};
            parameters[0].Value = model.ID;
            parameters[1].Value = model.UserID;
            parameters[2].Value = model.UserCode;
            parameters[3].Value = model.UserName;
            parameters[4].Value = model.BeginTime;
            parameters[5].Value = model.EndTime;
            parameters[6].Value = model.GoWorkTime;
            parameters[7].Value = model.OffWorkTime;
            parameters[8].Value = model.AttendanceType;
            parameters[9].Value = model.Deleted;
            parameters[10].Value = model.CreateTime;
            parameters[11].Value = model.UpdateTime;
            parameters[12].Value = model.OperatorID;
            parameters[13].Value = model.OperatorDept;
            parameters[14].Value = model.Sort;
            parameters[15].Value = model.WorkingDays_OverTime1;
            parameters[16].Value = model.WorkingDays_OverTime2;
            parameters[17].Value = model.LateGoWorkTime_OverTime1;

            DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }

        public void Update(CommuterTimeInfo model, SqlConnection conn, SqlTransaction trans)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update AD_CommuterTime set ");
            strSql.Append("UserID=@UserID,");
            strSql.Append("UserCode=@UserCode,");
            strSql.Append("UserName=@UserName,");
            strSql.Append("BeginTime=@BeginTime,");
            strSql.Append("EndTime=@EndTime,");
            strSql.Append("GoWorkTime=@GoWorkTime,");
            strSql.Append("OffWorkTime=@OffWorkTime,");
            strSql.Append("AttendanceType=@AttendanceType,");
            strSql.Append("Deleted=@Deleted,");
            strSql.Append("CreateTime=@CreateTime,");
            strSql.Append("UpdateTime=@UpdateTime,");
            strSql.Append("OperatorID=@OperatorID,");
            strSql.Append("OperatorDept=@OperatorDept,");
            strSql.Append("Sort=@Sort,");
            strSql.Append("WorkingDays_OverTime1=@WorkingDays_OverTime1,");
            strSql.Append("WorkingDays_OverTime2=@WorkingDays_OverTime2,");
            strSql.Append("LateGoWorkTime_OverTime1=@LateGoWorkTime_OverTime1 ");
            strSql.Append(" where ID=@ID");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4),
					new SqlParameter("@UserID", SqlDbType.Int,4),
					new SqlParameter("@UserCode", SqlDbType.NVarChar),
					new SqlParameter("@UserName", SqlDbType.NVarChar),
					new SqlParameter("@BeginTime", SqlDbType.DateTime),
					new SqlParameter("@EndTime", SqlDbType.DateTime),
					new SqlParameter("@GoWorkTime", SqlDbType.DateTime),
					new SqlParameter("@OffWorkTime", SqlDbType.DateTime),
					new SqlParameter("@AttendanceType", SqlDbType.Int,4),
					new SqlParameter("@Deleted", SqlDbType.Bit,1),
					new SqlParameter("@CreateTime", SqlDbType.DateTime),
					new SqlParameter("@UpdateTime", SqlDbType.DateTime),
					new SqlParameter("@OperatorID", SqlDbType.Int,4),
					new SqlParameter("@OperatorDept", SqlDbType.Int,4),
					new SqlParameter("@Sort", SqlDbType.Int,4),
                    new SqlParameter("@WorkingDays_OverTime1", SqlDbType.Float),
                    new SqlParameter("@WorkingDays_OverTime2", SqlDbType.Float),
                    new SqlParameter("@LateGoWorkTime_OverTime1", SqlDbType.Float)};
            parameters[0].Value = model.ID;
            parameters[1].Value = model.UserID;
            parameters[2].Value = model.UserCode;
            parameters[3].Value = model.UserName;
            parameters[4].Value = model.BeginTime;
            parameters[5].Value = model.EndTime;
            parameters[6].Value = model.GoWorkTime;
            parameters[7].Value = model.OffWorkTime;
            parameters[8].Value = model.AttendanceType;
            parameters[9].Value = model.Deleted;
            parameters[10].Value = model.CreateTime;
            parameters[11].Value = model.UpdateTime;
            parameters[12].Value = model.OperatorID;
            parameters[13].Value = model.OperatorDept;
            parameters[14].Value = model.Sort;
            parameters[15].Value = model.WorkingDays_OverTime1;
            parameters[16].Value = model.WorkingDays_OverTime2;
            parameters[17].Value = model.LateGoWorkTime_OverTime1;

            DbHelperSQL.ExecuteSql(strSql.ToString(), conn, trans, parameters);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public void Delete(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete AD_CommuterTime ");
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
        public CommuterTimeInfo GetModel(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from AD_CommuterTime ");
            strSql.Append(" where ID=@ID");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)};
            parameters[0].Value = ID;
            CommuterTimeInfo model = new CommuterTimeInfo();
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
            strSql.Append("select * ");
            strSql.Append(" FROM AD_CommuterTime ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 通过用户编号获得用的上下班时间信息集合
        /// </summary>
        /// <param name="userId">用户编号</param>
        /// <returns>返回用户的上下班时间信息集合</returns>
        public List<CommuterTimeInfo> GetCommuterTimeByUserId(int userId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * ");
            strSql.Append(" FROM AD_CommuterTime ");
            strSql.Append(" Where UserId=@UserId AND deleted=0");
            SqlParameter[] parameters = { new SqlParameter("@UserId", SqlDbType.Int, 4) };
            parameters[0].Value = userId;
            List<CommuterTimeInfo> list = new List<CommuterTimeInfo>();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    CommuterTimeInfo model = new CommuterTimeInfo();
                    model.PopupData(dr);
                    list.Add(model);
                }
            }
            return list;
        }

        public int UpdateUserCommuteType(int userid, int attendanceType)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" update AD_CommuterTime set attendanceType = " + attendanceType.ToString());
            strSql.Append(" Where UserId=@UserId ");
            SqlParameter[] parameters = { new SqlParameter("@UserId", SqlDbType.Int, 4) };
            parameters[0].Value = userid;
            return DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
            
        }

        public int UpdateUserCommuteType(int userid, int attendanceType,SqlTransaction trans)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" update AD_CommuterTime set attendanceType = " + attendanceType.ToString());
            strSql.Append(" Where UserId=@UserId ");
            SqlParameter[] parameters = { new SqlParameter("@UserId", SqlDbType.Int, 4) };
            parameters[0].Value = userid;
            return DbHelperSQL.ExecuteSql(strSql.ToString(),trans.Connection,trans, parameters);

        }
        #endregion  成员方法
    }
}
