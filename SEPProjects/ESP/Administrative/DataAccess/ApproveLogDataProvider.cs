using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESP.Administrative.Common;
using System.Data.SqlClient;
using System.Data;

namespace ESP.Administrative.DataAccess
{
    public class ApproveLogDataProvider
    {
        public ApproveLogDataProvider()
        { }
        #region  成员方法

        /// <summary>
        /// 得到最大ID
        /// </summary>
        public int GetMaxId()
        {
            return DbHelperSQL.GetMaxID("ID", "AD_ApproveLog");
        }

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from AD_ApproveLog");
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
        public int Add(ESP.Administrative.Entity.ApproveLogInfo model, SqlConnection conn, SqlTransaction trans)
        {
            //model.ID=GetMaxId();
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into AD_ApproveLog(");
            strSql.Append("ApproveID,ApproveName,ApproveType,ApproveDateID,ApproveState,ApproveUpUserID,IsLastApprove,Deleted,CreateTime,UpdateTime,OperateorID, ApproveRemark)");
            strSql.Append(" values (");
            strSql.Append("@ApproveID,@ApproveName,@ApproveType,@ApproveDateID,@ApproveState,@ApproveUpUserID,@IsLastApprove,@Deleted,@CreateTime,@UpdateTime,@OperateorID,@ApproveRemark)");
            strSql.Append(";SELECT @@IDENTITY");
            SqlParameter[] parameters = {					
					new SqlParameter("@ApproveID", SqlDbType.Int,4),
					new SqlParameter("@ApproveName", SqlDbType.NVarChar),
					new SqlParameter("@ApproveType", SqlDbType.Int,4),
					new SqlParameter("@ApproveDateID", SqlDbType.Int,4),
					new SqlParameter("@ApproveState", SqlDbType.Int,4),
					new SqlParameter("@ApproveUpUserID", SqlDbType.Int,4),
					new SqlParameter("@IsLastApprove", SqlDbType.Int,4),
					new SqlParameter("@Deleted", SqlDbType.Bit,4),
					new SqlParameter("@CreateTime", SqlDbType.DateTime),
					new SqlParameter("@UpdateTime", SqlDbType.DateTime),
					new SqlParameter("@OperateorID", SqlDbType.Int,4),
                    new SqlParameter("@ApproveRemark", SqlDbType.NVarChar)};            
            parameters[0].Value = model.ApproveID;
            parameters[1].Value = model.ApproveName;
            parameters[2].Value = model.ApproveType;
            parameters[3].Value = model.ApproveDateID;
            parameters[4].Value = model.ApproveState;
            parameters[5].Value = model.ApproveUpUserID;
            parameters[6].Value = model.IsLastApprove;
            parameters[7].Value = model.Deleted;
            parameters[8].Value = model.CreateTime;
            parameters[9].Value = model.UpdateTime;
            parameters[10].Value = model.OperateorID;
            parameters[11].Value = model.Approveremark;

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
        /// 增加一条数据
        /// </summary>
        public int Add(ESP.Administrative.Entity.ApproveLogInfo model)
        {
            //model.ID=GetMaxId();
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into AD_ApproveLog(");
            strSql.Append("ApproveID,ApproveName,ApproveType,ApproveDateID,ApproveState,ApproveUpUserID,IsLastApprove,Deleted,CreateTime,UpdateTime,OperateorID,Approveremark)");
            strSql.Append(" values (");
            strSql.Append("@ApproveID,@ApproveName,@ApproveType,@ApproveDateID,@ApproveState,@ApproveUpUserID,@IsLastApprove,@Deleted,@CreateTime,@UpdateTime,@OperateorID,@Approveremark)");
            strSql.Append(";SELECT @@IDENTITY");
            SqlParameter[] parameters = {					
					new SqlParameter("@ApproveID", SqlDbType.Int,4),
					new SqlParameter("@ApproveName", SqlDbType.NVarChar),
					new SqlParameter("@ApproveType", SqlDbType.Int,4),
					new SqlParameter("@ApproveDateID", SqlDbType.Int,4),
					new SqlParameter("@ApproveState", SqlDbType.Int,4),
					new SqlParameter("@ApproveUpUserID", SqlDbType.Int,4),
					new SqlParameter("@IsLastApprove", SqlDbType.Int,4),
					new SqlParameter("@Deleted", SqlDbType.Bit,4),
					new SqlParameter("@CreateTime", SqlDbType.DateTime),
					new SqlParameter("@UpdateTime", SqlDbType.DateTime),
					new SqlParameter("@OperateorID", SqlDbType.Int,4),
                    new SqlParameter("@Approveremark", SqlDbType.NVarChar)};
            parameters[0].Value = model.ApproveID;
            parameters[1].Value = model.ApproveName;
            parameters[2].Value = model.ApproveType;
            parameters[3].Value = model.ApproveDateID;
            parameters[4].Value = model.ApproveState;
            parameters[5].Value = model.ApproveUpUserID;
            parameters[6].Value = model.IsLastApprove;
            parameters[7].Value = model.Deleted;
            parameters[8].Value = model.CreateTime;
            parameters[9].Value = model.UpdateTime;
            parameters[10].Value = model.OperateorID;
            parameters[11].Value = model.Approveremark;

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
        public int Update(ESP.Administrative.Entity.ApproveLogInfo model, SqlConnection conn, SqlTransaction trans)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update AD_ApproveLog set ");
            strSql.Append("ApproveID=@ApproveID,");
            strSql.Append("ApproveName=@ApproveName,");
            strSql.Append("ApproveType=@ApproveType,");
            strSql.Append("ApproveDateID=@ApproveDateID,");
            strSql.Append("ApproveState=@ApproveState,");
            strSql.Append("ApproveUpUserID=@ApproveUpUserID,");
            strSql.Append("IsLastApprove=@IsLastApprove,");
            strSql.Append("Deleted=@Deleted,");
            strSql.Append("CreateTime=@CreateTime,");
            strSql.Append("UpdateTime=@UpdateTime,");
            strSql.Append("OperateorID=@OperateorID,");
            strSql.Append("Approveremark=@Approveremark");
            strSql.Append(" where ID=@ID");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4),
					new SqlParameter("@ApproveID", SqlDbType.Int,4),
					new SqlParameter("@ApproveName", SqlDbType.NVarChar),
					new SqlParameter("@ApproveType", SqlDbType.Int,4),
					new SqlParameter("@ApproveDateID", SqlDbType.Int,4),
					new SqlParameter("@ApproveState", SqlDbType.Int,4),
					new SqlParameter("@ApproveUpUserID", SqlDbType.Int,4),
					new SqlParameter("@IsLastApprove", SqlDbType.Int,4),
					new SqlParameter("@Deleted", SqlDbType.Bit,4),
					new SqlParameter("@CreateTime", SqlDbType.DateTime),
					new SqlParameter("@UpdateTime", SqlDbType.DateTime),
					new SqlParameter("@OperateorID", SqlDbType.Int,4),
                    new SqlParameter("@Approveremark", SqlDbType.NVarChar)};
            parameters[0].Value = model.ID;
            parameters[1].Value = model.ApproveID;
            parameters[2].Value = model.ApproveName;
            parameters[3].Value = model.ApproveType;
            parameters[4].Value = model.ApproveDateID;
            parameters[5].Value = model.ApproveState;
            parameters[6].Value = model.ApproveUpUserID;
            parameters[7].Value = model.IsLastApprove;
            parameters[8].Value = model.Deleted;
            parameters[9].Value = model.CreateTime;
            parameters[10].Value = model.UpdateTime;
            parameters[11].Value = model.OperateorID;
            parameters[12].Value = model.Approveremark;

            return DbHelperSQL.ExecuteSql(strSql.ToString(), conn, trans, parameters);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public int Update(ESP.Administrative.Entity.ApproveLogInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update AD_ApproveLog set ");
            strSql.Append("ApproveID=@ApproveID,");
            strSql.Append("ApproveName=@ApproveName,");
            strSql.Append("ApproveType=@ApproveType,");
            strSql.Append("ApproveDateID=@ApproveDateID,");
            strSql.Append("ApproveState=@ApproveState,");
            strSql.Append("ApproveUpUserID=@ApproveUpUserID,");
            strSql.Append("IsLastApprove=@IsLastApprove,");
            strSql.Append("Deleted=@Deleted,");
            strSql.Append("CreateTime=@CreateTime,");
            strSql.Append("UpdateTime=@UpdateTime,");
            strSql.Append("OperateorID=@OperateorID,");
            strSql.Append("Approveremark=@Approveremark");
            strSql.Append(" where ID=@ID");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4),
					new SqlParameter("@ApproveID", SqlDbType.Int,4),
					new SqlParameter("@ApproveName", SqlDbType.NVarChar),
					new SqlParameter("@ApproveType", SqlDbType.Int,4),
					new SqlParameter("@ApproveDateID", SqlDbType.Int,4),
					new SqlParameter("@ApproveState", SqlDbType.Int,4),
					new SqlParameter("@ApproveUpUserID", SqlDbType.Int,4),
					new SqlParameter("@IsLastApprove", SqlDbType.Int,4),
					new SqlParameter("@Deleted", SqlDbType.Bit,4),
					new SqlParameter("@CreateTime", SqlDbType.DateTime),
					new SqlParameter("@UpdateTime", SqlDbType.DateTime),
					new SqlParameter("@OperateorID", SqlDbType.Int,4),
                    new SqlParameter("@Approveremark", SqlDbType.NVarChar)};
            parameters[0].Value = model.ID;
            parameters[1].Value = model.ApproveID;
            parameters[2].Value = model.ApproveName;
            parameters[3].Value = model.ApproveType;
            parameters[4].Value = model.ApproveDateID;
            parameters[5].Value = model.ApproveState;
            parameters[6].Value = model.ApproveUpUserID;
            parameters[7].Value = model.IsLastApprove;
            parameters[8].Value = model.Deleted;
            parameters[9].Value = model.CreateTime;
            parameters[10].Value = model.UpdateTime;
            parameters[11].Value = model.OperateorID;
            parameters[12].Value = model.Approveremark;

            return DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public int Delete(int ID, SqlConnection conn, SqlTransaction trans)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete AD_ApproveLog ");
            strSql.Append(" where ID=@ID");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)
			    };
            parameters[0].Value = ID;
            return DbHelperSQL.ExecuteSql(strSql.ToString(), conn, trans, parameters);
        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public ESP.Administrative.Entity.ApproveLogInfo GetModel(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from AD_ApproveLog ");
            strSql.Append(" where ID=@ID");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)};
            parameters[0].Value = ID;
            ESP.Administrative.Entity.ApproveLogInfo model = new ESP.Administrative.Entity.ApproveLogInfo();
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
        public ESP.Administrative.Entity.ApproveLogInfo GetModel(int approveID, int approveDateID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from AD_ApproveLog ");
            strSql.Append(" where approveDateID=@approveDateID and ApproveState=@ApproveState");
            SqlParameter[] parameters = {
                    new SqlParameter("@approveDateID",SqlDbType.Int, 4),
                    new SqlParameter("@ApproveState", SqlDbType.Int, 4)};
            parameters[0].Value = approveDateID;
            parameters[1].Value = Status.ApproveState_NoPassed;
            ESP.Administrative.Entity.ApproveLogInfo model = new ESP.Administrative.Entity.ApproveLogInfo();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            
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
            strSql.Append("select * FROM AD_ApproveLog ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 获得待审批数据信息列表
        /// </summary>
        /// <param name="userId">用户编号</param>
        /// <param name="type">数据类型1.考勤数据，2.请假数据</param>
        public DataSet GetWaitApproveList(string sql)
        {
            DataSet ds = new DataSet();
            if (!string.IsNullOrEmpty(sql))
            {
                ds = DbHelperSQL.Query(sql.ToString());
            }
            return ds;
        }

        /// <summary>
        /// 获得待审批数据信息列表
        /// </summary>
        /// <param name="userId">用户编号</param>
        /// <param name="type">数据类型1.考勤数据，2.请假数据</param>
        public DataSet GetApprovedList(string sql)
        {
            DataSet ds = new DataSet();
            if (!string.IsNullOrEmpty(sql))
            {
                ds = DbHelperSQL.Query(sql.ToString());
            }
            return ds;
        }

        /// <summary>
        /// 获得用户月审列表
        /// </summary>
        /// <param name="approveid">审批人ID</param>        
        /// <param name="state">等待总监审批：2；等待考勤统计员审批：3；等待HRAdmin审批：4；等待团队经理审批：5； 等待考勤管理员审批：6；审批通过 ：7；</param>
        /// <param name="Year">审批年份</param>
        /// <param name="Month">审批月份</param>
        /// <returns></returns>
        public DataSet GetApproveList(string approveid, int state, int Year, int Month, string strwhere, List<SqlParameter> parameter)
        {
            string sql = string.Format(@"SELECT distinct m.UserID AS ApplicantID,m.LateCount,m.ID,m.LeaveEarlyCount, m.SickLeaveHours, m.AffairLeaveHours, m.AnnualLeaveDays, m.DeductSum,m.Other,
                      m.OverAnnualLeaveDays, m.MaternityLeaveHours, m.MarriageLeaveHours, m.FuneralLeaveHours, m.AbsentDays, m.OverTimeHours, m.EvectionDays, m.EgressHours, m.OffTuneHours,m.DeductSum,m.HolidayOverTimeHours,m.PrenatalCheckHours,m.IncentiveHours,m.AttendanceSubType,m.LastAnnualDays,
                            u.LastNameCN + u.FirstNameCN AS ApplicantName,
		                    l.CreateTime AS AppliedTime,
		                    l.ApproveID AS ApproverID,
		                    l.ApproveName AS ApproverName,
                            m.UserCode,d.level2,d.level3,d.level2ID,
		                    N'' AS ApproversUrl,
		                    u.LastNameCN + u.FirstNameCN + CAST(m.Year AS varchar(10))+ N'年' + CAST(m.Month AS varchar(10)) + N'月的考勤月统计单' AS Description,
		                    l.ID AS FromID,
		                    N'' AS FormNumber,
		                    N'考勤月统计单' AS FormType,
		                    CAST(ApproveType AS nvarchar(10)) AS Url,
		                    l.ApproveID as UserID,
		                    N'待审批' as OperationType, 
		                    N'' as AuditType,
		                    NULL as DataItem
                            FROM AD_MonthStat m 
	                        JOIN AD_ApproveLog l ON m.ID = l.ApproveDateID AND l.Deleted = 0 AND l.ApproveState=0 AND l.ApproveType = 1 
	                        JOIN sep_Users u ON m.UserID = u.UserID
							join sep_EmployeesInPositions eip on m.userid = eip.userid
							join V_Department d on eip.departmentid = d.level3Id
                            where m.state={0} and l.approveid in ({1}) and m.deleted=0 and m.year={2} and m.month={3} "
                , state, approveid, Year, Month);

            if (strwhere.Trim() != "")
            {
                sql += strwhere;
            }
            sql += " ORDER BY m.UserCode ASC";
            DataSet ds = DbHelperSQL.Query(sql.ToString(), parameter.ToArray());
            return ds;
        }

        public DataSet GetNewApproveList(string adminId, int state, int Year, int Month, string strwhere, List<SqlParameter> parameter)
        {
            string sql = string.Format(@"SELECT distinct m.UserID AS ApplicantID,m.LateCount,m.ID,m.LeaveEarlyCount, m.SickLeaveHours, m.AffairLeaveHours, m.AnnualLeaveDays, m.DeductSum,m.Other,
                      m.OverAnnualLeaveDays, m.MaternityLeaveHours, m.MarriageLeaveHours, m.FuneralLeaveHours, m.AbsentDays, m.OverTimeHours, m.EvectionDays, m.EgressHours, m.OffTuneHours,m.DeductSum,m.HolidayOverTimeHours,m.PrenatalCheckHours,m.IncentiveHours,m.AttendanceSubType,m.LastAnnualDays,
                            u.LastNameCN + u.FirstNameCN AS ApplicantName,(CAST(m.Year AS varchar(10)) + '-' + CAST(m.Month AS varchar(10)) + '#' + CAST(m.UserID AS varchar(10))) as monthData,
                            m.UserCode,d.level2,d.level3,d.level2ID,
		                    N'' AS ApproversUrl,
		                    u.LastNameCN + u.FirstNameCN + CAST(m.Year AS varchar(10))+ N'年' + CAST(m.Month AS varchar(10)) + N'月的考勤月统计单' AS Description,
		                    N'' AS FormNumber,
		                    N'考勤月统计单' AS FormType,
		                    N'待审批' as OperationType, 
		                    N'' as AuditType,
		                    NULL as DataItem
                            FROM AD_MonthStat m 
	                        JOIN ad_operationauditmanage l ON m.userId = l.userid
	                        JOIN sep_Users u ON m.UserID = u.UserID
							join sep_EmployeesInPositions eip on m.userid = eip.userid
							join V_Department d on eip.departmentid = d.level3Id
                            where m.state={0} and l.hrAdminId in ({1}) and m.deleted=0 and m.year={2} and m.month={3} "
                , state, adminId, Year, Month);

            if (strwhere.Trim() != "")
            {
                sql += strwhere;
            }
            sql += " ORDER BY m.UserCode ASC";
            DataSet ds = DbHelperSQL.Query(sql.ToString(), parameter.ToArray());
            return ds;
        }

        /// <summary>
        /// 获得用户月审列表
        /// </summary>
        /// <param name="approveid">审批人ID</param>        
        /// <param name="state">等待总监审批：2；等待考勤统计员审批：3；等待HRAdmin审批：4；等待团队经理审批：5； 等待考勤管理员审批：6；审批通过 ：7；</param>
        /// <param name="Year">审批年份</param>
        /// <param name="Month">审批月份</param>
        /// <returns></returns>
        public DataSet GetExportApproveList(string approveid, int state, int Year, int Month)
        {
            string sql = string.Format(@"SELECT distinct m.UserID AS ApplicantID,m.LateCount,m.ID,m.LeaveEarlyCount, m.SickLeaveHours, m.AffairLeaveHours, m.AnnualLeaveDays, m.DeductSum,m.Other,
                      m.OverAnnualLeaveDays, m.MaternityLeaveHours, m.MarriageLeaveHours, m.FuneralLeaveHours, m.AbsentDays, m.OverTimeHours, m.EvectionDays, m.EgressHours, m.OffTuneHours,m.DeductSum,m.HolidayOverTimeHours,m.PrenatalCheckHours,m.IncentiveHours,m.LastAnnualDays,
                            u.LastNameCN + u.FirstNameCN AS ApplicantName,
		                    l.CreateTime AS AppliedTime,
		                    l.ApproveID AS ApproverID,
		                    l.ApproveName AS ApproverName,
                            m.userCode,d.level2,d.level3,d.level2ID,
		                    N'' AS ApproversUrl,
		                    u.LastNameCN + u.FirstNameCN + CAST(m.Year AS varchar(10))+ N'年' + CAST(m.Month AS varchar(10)) + N'月的考勤月统计单' AS Description,
		                    l.ID AS FromID,
		                    N'' AS FormNumber,
		                    N'考勤月统计单' AS FormType,
		                    CAST(ApproveType AS nvarchar(10)) AS Url,
		                    l.ApproveID as UserID,
		                    N'待审批' as OperationType, 
		                    N'' as AuditType,
		                    NULL as DataItem
                            FROM AD_MonthStat m 
	                        JOIN AD_ApproveLog l ON m.ID = l.ApproveDateID AND l.Deleted = 0 AND l.ApproveState=0 AND l.ApproveType = 1 
	                        JOIN sep_Users u ON m.UserID = u.UserID
							join sep_EmployeesInPositions eip on m.userid = eip.userid
							join V_Department d on eip.departmentid = d.level3Id
                            where m.state={0} and l.approveid in ({1}) and m.deleted=0 and m.year={2} and m.month={3} ORDER BY d.level2ID asc"
                , state, approveid, Year, Month);
            DataSet ds = DbHelperSQL.Query(sql.ToString());
            return ds;
        }

        /// <summary>
        /// 查询已经审批通过的考勤统计信息
        /// </summary>
        /// <param name="approveid">审批人ID</param>        
        /// <param name="state">等待总监审批：2；等待考勤统计员审批：3；等待HRAdmin审批：4；等待团队经理审批：5； 等待考勤管理员审批：6；审批通过 ：7；</param>
        /// <param name="Year">审批年份</param>
        /// <param name="Month">审批月份</param>
        /// <returns></returns>
        public DataSet GetExportApprovedList(string approveid, int state, int Year, int Month)
        {
            string sql = string.Format(@"SELECT distinct m.UserID AS ApplicantID,m.LateCount,m.ID,m.LeaveEarlyCount, m.SickLeaveHours, m.AffairLeaveHours, m.AnnualLeaveDays, m.DeductSum,m.Other,
                      m.OverAnnualLeaveDays, m.MaternityLeaveHours, m.MarriageLeaveHours, m.FuneralLeaveHours, m.AbsentDays, m.OverTimeHours, m.EvectionDays, m.EgressHours, m.OffTuneHours,m.DeductSum,m.State,m.HolidayOverTimeHours,m.PrenatalCheckHours,m.IncentiveHours,m.LastAnnualDays,
	                    	u.LastNameCN + u.FirstNameCN AS ApplicantName,
		                    l.CreateTime AS AppliedTime,
		                    l.ApproveID AS ApproverID,
		                    l.ApproveName AS ApproverName,
                            m.userCode,d.level2,d.level3,d.level2ID,
		                    N'' AS ApproversUrl,
		                    u.LastNameCN + u.FirstNameCN + CAST(m.Year AS varchar(10))+ N'年' + CAST(m.Month AS varchar(10)) + N'月的考勤月统计单' AS Description,
		                    l.ID AS FromID,
		                    N'' AS FormNumber,
		                    N'考勤月统计单' AS FormType,
		                    CAST(ApproveType AS nvarchar(10)) AS Url,
		                    l.ApproveID as UserID,
		                    N'待审批' as OperationType, 
		                    N'' as AuditType,
		                    NULL as DataItem
                            FROM AD_MonthStat m 
	                        JOIN AD_ApproveLog l ON m.ID = l.ApproveDateID AND l.Deleted = 0 AND l.ApproveState=1 AND l.ApproveType = 1 
	                        JOIN sep_Users u ON m.UserID = u.UserID
							join sep_EmployeesInPositions eip on m.userid = eip.userid
							join V_Department d on eip.departmentid = d.level3Id
                            where m.state={0} and l.approveid in ({1}) and m.deleted=0 and m.year={2} and m.month={3} ORDER BY d.level2ID asc"
                , state, approveid, Year, Month);
            DataSet ds = DbHelperSQL.Query(sql.ToString());
            return ds;
        }

        /// <summary>
        /// 获得在职员工审批过的月考勤统计信息
        /// </summary>
        /// <param name="approveid">审批人ID</param>
        /// <param name="state">审批状态</param>
        /// <param name="year">年份</param>
        /// <param name="month">月份</param>
        /// <returns></returns>
        public DataSet GetExprotApprovedList2(string approveid, int state, int year, int month, string userids, int attendanceSubType)
        {
            int isLastApprove = 0;
            string begindate = new DateTime(year, month, 1).ToString("yyyy-MM-dd");
            string enddate = new DateTime(year, month, 1).AddMonths(1).ToString("yyyy-MM-dd");
            if (state == Status.MonthStatAppState_Passed)
                isLastApprove = 1;

            string sql = string.Format(@"SELECT m.UserID AS ApplicantID,m.LateCount,m.ID,m.LeaveEarlyCount, m.SickLeaveHours, m.AffairLeaveHours, m.AnnualLeaveDays, m.DeductSum,m.Other,
                            m.OverAnnualLeaveDays, m.MaternityLeaveHours, m.MarriageLeaveHours, m.FuneralLeaveHours, m.AbsentDays, m.LastAnnualDays,0 OverTimeHours, 
                            m.EvectionDays, m.EgressHours, m.OffTuneHours,m.DeductSum,0  HolidayOverTimeHours,
                            m.PrenatalCheckHours,m.IncentiveHours,
                            m.approveid,m.approvetime,m.HRAdminId, m.HRAdmintime, m.managerid,m.managertime,m.adadminid,m.adadmintime,m.approveremark,m.State,m.IsHavePCRefund,m.PCRefundAmount,
                            u.LastNameCN + u.FirstNameCN AS ApplicantName,
                            m.CreateTime AS AppliedTime,
                            m.UserCode,d.level2,d.level3,d.level2ID,
                            N'' AS ApproversUrl,
                            u.LastNameCN + u.FirstNameCN + CAST(m.Year AS varchar(10))+ N'年' + CAST(m.Month AS varchar(10)) + N'月的考勤月统计单' AS Description,
                            m.ID AS FromID,
                            N'' AS FormNumber,
                            N'考勤月统计单' AS FormType,
                            m.UserId as UserID,
                            N'待审批' as OperationType, 
                            N'' as AuditType,
                            NULL as DataItem  FROM ad_monthstat m 
	                            LEFT JOIN sep_Users u ON m.UserID = u.UserID
	                            LEFT JOIN sep_EmployeesInPositions eip ON m.userid = eip.userid
	                            LEFT JOIN V_Department d ON eip.departmentid = d.level3Id
	                            left join sep_dimissionform dim on m.userid =dim.userid
	                            WHERE m.deleted=0 AND
	                                m.[year]={0} AND m.[month]={1} and dim.status>1 and year(dim.lastday)={0} and month(dim.lastday)={1}", year, month);
            if (!string.IsNullOrEmpty(userids))
            {
                sql += " and m.userid in (" + userids + ")";
            }
            sql += " order by d.level3, m.UserCode";
            List<SqlParameter> parms = new List<SqlParameter>();
            parms.Add(new SqlParameter("@begindate", begindate));
            parms.Add(new SqlParameter("@enddate", enddate));

            DataSet ds = DbHelperSQL.Query(sql.ToString(), parms.ToArray());
            return ds;
        }

        public DataSet GetExprotApprovedListNormal(string approveid, int state, int year, int month, string userids, int attendanceSubType)
        {
            int isLastApprove = 0;
            string begindate = new DateTime(year, month, 1).ToString("yyyy-MM-dd");
            string enddate = new DateTime(year, month, 1).AddMonths(1).ToString("yyyy-MM-dd");
            if (state == Status.MonthStatAppState_Passed)
                isLastApprove = 1;

            string sql = string.Format(@"SELECT m.UserID AS ApplicantID,m.LateCount,m.ID,m.LeaveEarlyCount, m.SickLeaveHours, m.AffairLeaveHours, m.AnnualLeaveDays, m.DeductSum,m.Other,
                            m.OverAnnualLeaveDays, m.MaternityLeaveHours, m.MarriageLeaveHours, m.FuneralLeaveHours, m.AbsentDays, m.LastAnnualDays,
'0' OverTimeHours, 
                            m.EvectionDays, m.EgressHours, m.OffTuneHours,m.DeductSum,
'0'  HolidayOverTimeHours,
                            m.PrenatalCheckHours,m.IncentiveHours,
                            m.approveid,m.approvetime,m.HRAdminId, m.HRAdmintime, m.managerid,m.managertime,m.adadminid,m.adadmintime,m.approveremark,m.State,m.IsHavePCRefund,m.PCRefundAmount,
                            u.LastNameCN + u.FirstNameCN AS ApplicantName,
                            m.CreateTime AS AppliedTime,
                            m.UserCode,d.level2,d.level3,d.level2ID,
                            N'' AS ApproversUrl,
                            u.LastNameCN + u.FirstNameCN + CAST(m.Year AS varchar(10))+ N'年' + CAST(m.Month AS varchar(10)) + N'月的考勤月统计单' AS Description,
                            m.ID AS FromID,
                            N'' AS FormNumber,
                            N'考勤月统计单' AS FormType,
                            m.UserId as UserID,
                            N'待审批' as OperationType, 
                            N'' as AuditType,
                            NULL as DataItem  FROM ad_monthstat m 
	                            LEFT JOIN sep_Users u ON m.UserID = u.UserID
	                            LEFT JOIN sep_EmployeesInPositions eip ON m.userid = eip.userid
	                            LEFT JOIN V_Department d ON eip.departmentid = d.level3Id
	                            WHERE m.deleted=0 AND
	                                m.[year]={0} AND m.[month]={1} and m.attendanceSubType=1 ", year, month);
            if (!string.IsNullOrEmpty(userids))
            {
                sql += " and m.userid in (" + userids + ")";
            }
            sql += " order by d.level3, m.UserCode";

            List<SqlParameter> parms = new List<SqlParameter>();
            parms.Add(new SqlParameter("@begindate", begindate));
            parms.Add(new SqlParameter("@enddate", enddate));

            DataSet ds = DbHelperSQL.Query(sql.ToString(),parms.ToArray());
            //return DbHelperSQL.Query(strSql.ToString(), parms.ToArray()).Tables[0];
            return ds;
        }

        /// <summary>
        /// 撤销刚提交的审批记录
        /// </summary>
        public int CancelApproveLog(int approveDataId, int approveState)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete AD_ApproveLog ");
            strSql.Append(" where approveDateID=@approveDateID and ApproveState=@ApproveState ");
            SqlParameter[] parameters = {
					new SqlParameter("@approveDateID", SqlDbType.Int,4),
                    new SqlParameter("@ApproveState", SqlDbType.Int,4)
			    };
            parameters[0].Value = approveDataId;
            parameters[1].Value = approveState;
            return DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 获得待审批的月考勤统计单信息
        /// </summary>
        /// <param name="userId">用户编号</param>
        /// <returns>返回月考勤统计单信息集合</returns>
        public DataSet GetApproveMonthStatInfo(int userId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from ad_approvelog a ");
            strSql.Append(" left join ad_monthstat m on a.approvedateid=m.id ");
            strSql.Append(" where m.deleted=0 and a.approveState=0 and a.approvetype=1 and a.approveid=@approveid ");
            SqlParameter[] parameters = {
                new SqlParameter("@approveid", SqlDbType.Int,4)                       
            };
            parameters[0].Value = userId;
            return DbHelperSQL.Query(strSql.ToString(), parameters);
        }
        #endregion  成员方法
    }
}