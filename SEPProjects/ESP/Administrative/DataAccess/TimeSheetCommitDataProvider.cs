using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using ESP.Administrative.Common;
using System.Data;
using ESP.Administrative.Entity;
using System.Net.Mail;

namespace ESP.Administrative.DataAccess
{
    /// <summary>
    /// 数据访问类:TimeSheetCommitDataProvider
    /// </summary>
    public partial class TimeSheetCommitDataProvider
    {
        public TimeSheetCommitDataProvider()
        { }

        public int Add(TimeSheetCommitInfo model)
        {
            return Add(model, null);
        }
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(TimeSheetCommitInfo model, SqlTransaction trans)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into AD_TimeSheetCommit(");
            strSql.Append("WeekId,UserId,UserCode,UserName,DepartmentId,DepartmentName,CreateDate,CommitDate,Status,IP,GoWorkTime,OffWorkTime,CurrentDate,CommitType,Description,CurrentGoWorkTime,CurrentOffWorkTime,OverWorkHours,BeginDate,EndDate,CategoryId,CategoryName,SerialNo,AuditDate)");
            strSql.Append(" values (");
            strSql.Append("@WeekId,@UserId,@UserCode,@UserName,@DepartmentId,@DepartmentName,@CreateDate,@CommitDate,@Status,@IP,@GoWorkTime,@OffWorkTime,@CurrentDate,@CommitType,@Description,@CurrentGoWorkTime,@CurrentOffWorkTime,@OverWorkHours,@BeginDate,@EndDate,@CategoryId,@CategoryName,@SerialNo,@AuditDate)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@WeekId", SqlDbType.Int,4),
					new SqlParameter("@UserId", SqlDbType.Int,4),
					new SqlParameter("@UserCode", SqlDbType.NVarChar,50),
					new SqlParameter("@UserName", SqlDbType.NVarChar,50),
					new SqlParameter("@DepartmentId", SqlDbType.Int,4),
					new SqlParameter("@DepartmentName", SqlDbType.NVarChar,50),
					new SqlParameter("@CreateDate", SqlDbType.DateTime),
					new SqlParameter("@CommitDate", SqlDbType.DateTime),
					new SqlParameter("@Status", SqlDbType.Int,4),
					new SqlParameter("@IP", SqlDbType.NVarChar,50),
					new SqlParameter("@GoWorkTime", SqlDbType.DateTime),
					new SqlParameter("@OffWorkTime", SqlDbType.DateTime),
					new SqlParameter("@CurrentDate", SqlDbType.DateTime),
					new SqlParameter("@CommitType", SqlDbType.NVarChar,50),
					new SqlParameter("@Description", SqlDbType.NVarChar,500),
                    new SqlParameter("@CurrentGoWorkTime", SqlDbType.DateTime),
                    new SqlParameter("@CurrentOffWorkTime", SqlDbType.DateTime),
                    new SqlParameter("@OverWorkHours",SqlDbType.Decimal)
                    ,
                    new SqlParameter("@BeginDate", SqlDbType.DateTime),
                    new SqlParameter("@EndDate",SqlDbType.DateTime),
                    new SqlParameter("@CategoryId", SqlDbType.Int),
                    new SqlParameter("@CategoryName",SqlDbType.NVarChar),
                    new SqlParameter("@SerialNo",SqlDbType.NVarChar),
                    new SqlParameter("@AuditDate",SqlDbType.DateTime)
                                        };
            parameters[0].Value = model.WeekId;
            parameters[1].Value = model.UserId;
            parameters[2].Value = model.UserCode;
            parameters[3].Value = model.UserName;
            parameters[4].Value = model.DepartmentId;
            parameters[5].Value = model.DepartmentName;
            parameters[6].Value = model.CreateDate;
            parameters[7].Value = model.CommitDate;
            parameters[8].Value = model.Status;
            parameters[9].Value = model.IP;
            parameters[10].Value = model.GoWorkTime;
            parameters[11].Value = model.OffWorkTime;
            parameters[12].Value = model.CurrentDate;
            parameters[13].Value = model.CommitType;
            parameters[14].Value = model.Description;
            parameters[15].Value = model.CurrentGoWorkTime;
            parameters[16].Value = model.CurrentOffWorkTime;
            parameters[17].Value = model.OverWorkHours;
            parameters[18].Value = model.BeginDate;
            parameters[19].Value = model.EndDate;
            parameters[20].Value = model.CategoryId;
            parameters[21].Value = model.CategoryName;
            parameters[22].Value = model.SerialNo;
            parameters[23].Value = DateTime.Now;

            object obj = null;
            if (trans == null)
                obj = DbHelperSQL.GetSingle(strSql.ToString(), parameters);
            else
                obj = DbHelperSQL.GetSingle(strSql.ToString(), trans.Connection, trans, parameters);
            if (obj == null)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(obj);
            }
        }


        public bool Update(TimeSheetCommitInfo model)
        {
            return Update(model, null);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(TimeSheetCommitInfo model, SqlTransaction trans)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update AD_TimeSheetCommit set ");
            strSql.Append("WeekId=@WeekId,");
            strSql.Append("UserId=@UserId,");
            strSql.Append("UserCode=@UserCode,");
            strSql.Append("UserName=@UserName,");
            strSql.Append("DepartmentId=@DepartmentId,");
            strSql.Append("DepartmentName=@DepartmentName,");
            strSql.Append("CreateDate=@CreateDate,");
            strSql.Append("CommitDate=@CommitDate,");
            strSql.Append("Status=@Status,");
            strSql.Append("IP=@IP,");
            strSql.Append("GoWorkTime=@GoWorkTime,");
            strSql.Append("OffWorkTime=@OffWorkTime,");
            strSql.Append("CurrentDate=@CurrentDate,");
            strSql.Append("CommitType=@CommitType,");
            strSql.Append("Description=@Description,");
            strSql.Append("CurrentGoWorkTime=@CurrentGoWorkTime,");
            strSql.Append("CurrentOffWorkTime=@CurrentOffWorkTime,OverWorkHours=@OverWorkHours,BeginDate=@BeginDate,EndDate=@EndDate,CategoryId=@CategoryId,CategoryName=@CategoryName,SerialNo=@SerialNo,AuditDate=@AuditDate");
            strSql.Append(" where Id=@Id");
            SqlParameter[] parameters = {
					new SqlParameter("@WeekId", SqlDbType.Int,4),
					new SqlParameter("@UserId", SqlDbType.Int,4),
					new SqlParameter("@UserCode", SqlDbType.NVarChar,50),
					new SqlParameter("@UserName", SqlDbType.NVarChar,50),
					new SqlParameter("@DepartmentId", SqlDbType.Int,4),
					new SqlParameter("@DepartmentName", SqlDbType.NVarChar,50),
					new SqlParameter("@CreateDate", SqlDbType.DateTime),
					new SqlParameter("@CommitDate", SqlDbType.DateTime),
					new SqlParameter("@Status", SqlDbType.Int,4),
					new SqlParameter("@IP", SqlDbType.NVarChar,50),
					new SqlParameter("@GoWorkTime", SqlDbType.DateTime),
					new SqlParameter("@OffWorkTime", SqlDbType.DateTime),
					new SqlParameter("@CurrentDate", SqlDbType.DateTime),
					new SqlParameter("@CommitType", SqlDbType.NVarChar,50),
					new SqlParameter("@Description", SqlDbType.NVarChar,500),
                    new SqlParameter("@CurrentGoWorkTime", SqlDbType.DateTime),
                    new SqlParameter("@CurrentOffWorkTime", SqlDbType.DateTime),
					new SqlParameter("@Id", SqlDbType.Int,4),
                    new SqlParameter("@OverWorkHours",SqlDbType.Decimal),
                    new SqlParameter("@BeginDate", SqlDbType.DateTime),
                    new SqlParameter("@EndDate",SqlDbType.DateTime),
                    new SqlParameter("@CategoryId", SqlDbType.Int),
                    new SqlParameter("@CategoryName",SqlDbType.NVarChar),
                    new SqlParameter("@SerialNo",SqlDbType.NVarChar)  ,
                    new SqlParameter("@AuditDate",SqlDbType.DateTime)  ,                    
                                        };
            parameters[0].Value = model.WeekId;
            parameters[1].Value = model.UserId;
            parameters[2].Value = model.UserCode;
            parameters[3].Value = model.UserName;
            parameters[4].Value = model.DepartmentId;
            parameters[5].Value = model.DepartmentName;
            parameters[6].Value = model.CreateDate;
            parameters[7].Value = model.CommitDate;
            parameters[8].Value = model.Status;
            parameters[9].Value = model.IP;
            parameters[10].Value = model.GoWorkTime;
            parameters[11].Value = model.OffWorkTime;
            parameters[12].Value = model.CurrentDate;
            parameters[13].Value = model.CommitType;
            parameters[14].Value = model.Description;
            parameters[15].Value = model.CurrentGoWorkTime;
            parameters[16].Value = model.CurrentOffWorkTime;
            parameters[17].Value = model.Id;
            parameters[18].Value = model.OverWorkHours;
            parameters[19].Value = model.BeginDate;
            parameters[20].Value = model.EndDate;
            parameters[21].Value = model.CategoryId;
            parameters[22].Value = model.CategoryName;
            parameters[23].Value = model.SerialNo;
            parameters[24].Value = model.AuditDate;

            int rows = 0;
            if (trans == null)
                rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
            else
                rows = DbHelperSQL.ExecuteSql(strSql.ToString(), trans.Connection, trans, parameters);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool UpdateStatus(string Ids, int status, SqlTransaction trans)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update AD_TimeSheetCommit set status=@status ");
            strSql.Append(" where Id in (" + Ids + ")");
            SqlParameter[] parameters = {
					//new SqlParameter("@Id", SqlDbType.NVarChar,2000),
                    new SqlParameter("@status",SqlDbType.Int,4)
			};
            //parameters[0].Value = Ids;
            parameters[0].Value = status;

            int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), trans.Connection, trans, parameters);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool Delete(int Id)
        {
            return Delete(Id, null);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int Id, SqlTransaction trans)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from AD_TimeSheetCommit ");
            strSql.Append(" where Id=@Id");
            SqlParameter[] parameters = {
					new SqlParameter("@Id", SqlDbType.Int,4)
			};
            parameters[0].Value = Id;

            int rows = 0;
            if (trans == null)
                rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
            else
                rows = DbHelperSQL.ExecuteSql(strSql.ToString(), trans.Connection, trans, parameters);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool RollbackLongHoliday(string serialNo, SqlTransaction trans)
        {
            string sql = "update ad_timesheetcommit set status=@status, committype=@committype , categoryid=null , categoryname = null , begindate = null, enddate = null , SerialNo = null where serialNo=@serialNo";
            SqlParameter[] parameters = {
                    new SqlParameter("@status",SqlDbType.Int),
					new SqlParameter("@committype", SqlDbType.NVarChar),
                    new SqlParameter("@serialNo",SqlDbType.NVarChar)
			};
            parameters[0].Value = (int)ESP.Administrative.Common.TimeSheetCommitStatus.Save;
            parameters[1].Value = "Normal";
            parameters[2].Value = serialNo;
            int rows = DbHelperSQL.ExecuteSql(sql, trans.Connection, trans, parameters);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 批量删除数据
        /// </summary>
        public bool DeleteList(string Idlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from AD_TimeSheetCommit ");
            strSql.Append(" where Id in (" + Idlist + ")  ");
            int rows = DbHelperSQL.ExecuteSql(strSql.ToString());
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public TimeSheetCommitInfo GetModel(int Id, SqlTransaction trans)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 *,(OverWorkHours -(select case when sum(hours) is null then 0 else sum(hours) end from dbo.AD_OverWorkRecords where OverWorkId=AD_TimeSheetCommit.id) ) as OverWorkRemain  from AD_TimeSheetCommit ");
            strSql.Append(" where Id=@Id");
            SqlParameter[] parameters = {
					new SqlParameter("@Id", SqlDbType.Int,4)
			};
            parameters[0].Value = Id;

            if (trans == null)
                return CBO.FillObject<TimeSheetCommitInfo>(DbHelperSQL.Query(strSql.ToString(), parameters));
            else
                return CBO.FillObject<TimeSheetCommitInfo>(DbHelperSQL.Query(strSql.ToString(), trans, parameters));
        }

        public int ValidTimeSheetRecords(int UserId,DateTime weekbegin)
        { 
             StringBuilder strSql = new StringBuilder();
             strSql.Append(" select count(*) from ad_timesheetcommit where userid =@UserId and status in(2,3,4) and (currentdate between '" + weekbegin.AddDays(-7).ToString("yyyy-MM-dd") + "' and '" + weekbegin.ToString("yyyy-MM-dd") + "')");
            SqlParameter[] parameters = {
					new SqlParameter("@UserId", SqlDbType.Int,4)
			};
            parameters[0].Value = UserId;

          return int.Parse( DbHelperSQL.GetSingle(strSql.ToString(),parameters).ToString());
        }


        public TimeSheetCommitInfo GetModelByUserId(int UserId, string CurrentDate)
        {
            return GetModelByUserId(UserId, CurrentDate, null);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public TimeSheetCommitInfo GetModelByUserId(int UserId, string CurrentDate, SqlTransaction trans)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 *,(OverWorkHours -(select case when sum(hours) is null then 0 else sum(hours) end from dbo.AD_OverWorkRecords where OverWorkId=AD_TimeSheetCommit.id) ) as OverWorkRemain from AD_TimeSheetCommit ");
            strSql.Append(" where UserId=@UserId and datediff(day,@CurrentDate,CurrentDate) = 0");
            SqlParameter[] parameters = {
					new SqlParameter("@UserId", SqlDbType.Int,4),
                    new SqlParameter("@CurrentDate",SqlDbType.DateTime,8)
			};
            parameters[0].Value = UserId;
            parameters[1].Value = CurrentDate;

            if (trans == null)
                return CBO.FillObject<TimeSheetCommitInfo>(DbHelperSQL.Query(strSql.ToString(), parameters));
            else
                return CBO.FillObject<TimeSheetCommitInfo>(DbHelperSQL.Query(strSql.ToString(), trans, parameters));
        }

        public List<TimeSheetCommitInfo> GetList(string strWhere)
        {
            return GetList(strWhere, null);
        }

        public List<TimeSheetCommitInfo> GetLongHolidayList(string terms)
        {
            string sql = @"select min(a.id) as id, a.userid,a.usercode,a.username,a.departmentid,a.departmentname,a.begindate
                            ,a.enddate,a.categoryid,a.categoryname,a.serialno,a.[status],b.workitem from ad_timesheetcommit as a
                            inner join ad_timesheet as b on b.commitid=a.id and typeid=2
                            where serialno is not null and serialno <> '' " + terms + @" group by a.userid,a.usercode,a.username,a.departmentid,a.departmentname,a.begindate
                            ,a.enddate,a.categoryid,a.categoryname,a.serialno,a.[status],b.workitem ,a.AuditDate
                             order by begindate desc";
            return CBO.FillCollection<TimeSheetCommitInfo>(DbHelperSQL.Query(sql));
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<TimeSheetCommitInfo> GetList(string strWhere, SqlTransaction trans)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from (select *,(OverWorkHours -(select case when sum(hours) is null then 0 else sum(hours) end from dbo.AD_OverWorkRecords where OverWorkId=AD_TimeSheetCommit.id) ) as OverWorkRemain ");
            strSql.Append(" FROM AD_TimeSheetCommit) as a ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by currentdate asc");
            if (trans == null)
                return CBO.FillCollection<TimeSheetCommitInfo>(DbHelperSQL.Query(strSql.ToString()));
            else
                return CBO.FillCollection<TimeSheetCommitInfo>(DbHelperSQL.Query(strSql.ToString(), trans));
        }

        /// <summary>
        /// 获得前几行数据
        /// </summary>
        public List<TimeSheetCommitInfo> GetList(int Top, string strWhere, string filedOrder)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ");
            if (Top > 0)
            {
                strSql.Append(" top " + Top.ToString());
            }
            strSql.Append(" *,(OverWorkHours -(select case when sum(hours) is null then 0 else sum(hours) end from dbo.AD_OverWorkRecords where OverWorkId=AD_TimeSheetCommit.id) ) as OverWorkRemain ");
            strSql.Append(" FROM AD_TimeSheetCommit ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return CBO.FillCollection<TimeSheetCommitInfo>(DbHelperSQL.Query(strSql.ToString()));
        }

        /// <summary>
        /// 获取记录总数
        /// </summary>
        public int GetRecordCount(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) FROM AD_TimeSheetCommit ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            object obj = DbHelperSQL.GetSingle(strSql.ToString());
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
        /// 分页获取数据列表
        /// </summary>
        public List<TimeSheetCommitInfo> GetListByPage(string strWhere, string orderby, int startIndex, int endIndex)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT *,(OverWorkHours -(select case when sum(hours) is null then 0 else sum(hours) end from dbo.AD_OverWorkRecords where OverWorkId=AD_TimeSheetCommit.id) ) as OverWorkRemain FROM ( ");
            strSql.Append(" SELECT ROW_NUMBER() OVER (");
            if (!string.IsNullOrEmpty(orderby.Trim()))
            {
                strSql.Append("order by T." + orderby);
            }
            else
            {
                strSql.Append("order by T.Id desc");
            }
            strSql.Append(")AS Row, T.*  from AD_TimeSheetCommit T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                strSql.Append(" WHERE " + strWhere);
            }
            strSql.Append(" ) TT");
            strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return CBO.FillCollection<TimeSheetCommitInfo>(DbHelperSQL.Query(strSql.ToString()));
        }

        /// <summary>
        /// 根据userid和日期获得当天的timesheet信息
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="day"></param>
        /// <returns></returns>
        public TimeSheetCommitInfo GetTimeSheetInfoForDay(int userId, DateTime day)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"select a.*,b.timesheetcount,b.timesheethours 

                                ,isnull(waichu.commitid,0) as WaiChu
                                ,isnull(qingjia.commitid,0) as QingJia
                                ,isnull(chuchai.commitid,0) as ChuChai
                                ,isnull(jiaban.commitid,0) as JiaBan
                                ,isnull(tiaoxiu.commitid,0) as TiaoXiu
                                ,isnull(chidao.commitid,0) as ChiDao
                                ,isnull(zaotui.commitid,0) as ZaoTui
                                ,isnull(kuanggong.commitid,0) as KuangGong                                

                                from ad_timesheetcommit as a 

                                inner join (
	                                select userid,commitid, count(id) as timesheetCount,sum([hours]) as timesheethours from ad_timesheet group by userid,commitid
                                ) as b on a.id=b.commitid 

                                left join (
                                select commitid from ad_timesheet where categoryid = 9
                                ) as waichu on a.id = waichu.commitid

                                left join (
                                select commitid from ad_timesheet where categoryid in (7,8,12,13,14)
                                ) as qingjia on a.id = qingjia.commitid

                                left join (
                                select commitid from ad_timesheet where categoryid = 15
                                ) as chuchai on a.id = chuchai.commitid

                                left join (
                                select commitid from ad_timesheet where categoryid = 10
                                ) as jiaban on a.id = jiaban.commitid

                                left join (
                                select commitid from ad_timesheet where categoryid = 11
                                ) as tiaoxiu on a.id = tiaoxiu.commitid

                                left join (
                                select commitid from ad_timesheet where typeid = 10 and categoryid=4 and ischecked = 1
                                ) as chidao on a.id = chidao.commitid

                                left join (
                                select commitid from ad_timesheet where typeid = 11 and categoryid=5 and ischecked = 1
                                ) as zaotui on a.id = zaotui.commitid

                                left join (
                                select commitid from ad_timesheet where typeid = 12 and categoryid=6 and ischecked = 1
                                ) as kuanggong on a.id = kuanggong.commitid

                            where 1=1");
            strSql.Append(" and a.userId=@userId and a.CurrentDate=@CurrentDate");
            SqlParameter[] parms = {
                                   new SqlParameter("@userId",SqlDbType.Int,4),
                                   new SqlParameter("@CurrentDate",SqlDbType.DateTime,8)
                                   };
            parms[0].Value = userId;
            parms[1].Value = day;
            return CBO.FillObject<TimeSheetCommitInfo>(DbHelperSQL.Query(strSql.ToString(), parms));
        }

        /// <summary>
        /// 获取管理人员下所有员工在某一时间段的timesheet天数
        /// </summary>
        /// <param name="ManagerId"></param>
        /// <param name="BeginDate"></param>
        /// <param name="EndDate"></param>
        /// <param name="keys"></param>
        /// <returns></returns>
        public DataTable GetUserListByManagerId(int ManagerId, string BeginDate, string EndDate, string keys)
        {
            DataTable dt = new DataTable();

            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"select d.commitCount,c.*,b.code as userCode,b.phone1, b.internalEmail,(dp.level1+'-'+dp.level2+'-'+dp.level3) as dpName from AD_OperationAuditManage as a 
                                inner join sep_employees as b on a.userid=b.userid and status=1
                                inner join sep_users as c on c.userid=b.userid
                                inner join sep_employeesinpositions as ep on ep.userid=a.userid
                                inner join v_department as dp on dp.level3id=ep.departmentid
                                left join (select userid,count(id) as commitCount from ad_timesheetcommit 
                                where currentdate between @Begin and @End group by userid) as d on c.userid=d.userid
                                left join sep_operationauditmanage e on dp.level3id=e.depid ");
            strSql.Append(" where (TeamLeaderId=@ManagerId or HRAdminId=@ManagerId or a.ManagerId=@ManagerId or e.directorid=@ManagerId or e.managerid =@ManagerId)");
            if (!string.IsNullOrEmpty(keys))
            {
                strSql.Append(" and (b.code like '%'+@Keys+'%' or c.lastnamecn+c.firstnamecn like '%'+@Keys+'%' or c.username like '%'+@Keys+'%' or dp.level3 like '%'+@Keys+'%')");
            }
            strSql.Append(" order by dp.level3");
            List<SqlParameter> parms = new List<SqlParameter>();
            parms.Add(new SqlParameter("@Begin", BeginDate));
            parms.Add(new SqlParameter("@End", DateTime.Parse(EndDate).AddDays(1).ToString("yyyy-MM-dd")));
            parms.Add(new SqlParameter("@ManagerId", ManagerId));
            parms.Add(new SqlParameter("@Keys", keys));

            return DbHelperSQL.Query(strSql.ToString(), parms.ToArray()).Tables[0];
        }

        public DataTable GetUserListByHrAdmin(string BeginDate, string EndDate, string keys)
        {
            DataTable dt = new DataTable();

            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"select d.commitCount,c.*,b.code as userCode,b.phone1, b.internalEmail,(dp.level1+'-'+dp.level2+'-'+dp.level3) as dpName from AD_OperationAuditManage as a 
                                inner join sep_employees as b on a.userid=b.userid and status=1
                                inner join sep_users as c on c.userid=b.userid
                                inner join sep_employeesinpositions as ep on ep.userid=a.userid
                                inner join v_department as dp on dp.level3id=ep.departmentid
                                left join (select userid,count(id) as commitCount from ad_timesheetcommit 
                                where currentdate between @Begin and @End group by userid) as d on c.userid=d.userid ");
            strSql.Append(" where 1=1 ");
            if (!string.IsNullOrEmpty(keys))
            {
                strSql.Append(" and (b.code like '%'+@Keys+'%' or c.lastnamecn+c.firstnamecn like '%'+@Keys+'%' or c.username like '%'+@Keys+'%' or dp.level3 like '%'+@Keys+'%')");
            }
            strSql.Append(" order by dp.level3");
            List<SqlParameter> parms = new List<SqlParameter>();
            parms.Add(new SqlParameter("@Begin", BeginDate));
            parms.Add(new SqlParameter("@End", DateTime.Parse(EndDate).AddDays(1).ToString("yyyy-MM-dd")));
            parms.Add(new SqlParameter("@Keys", keys));

            return DbHelperSQL.Query(strSql.ToString(), parms.ToArray()).Tables[0];
        }

        /// <summary>
        /// 待审核
        /// </summary>
        /// <param name="LeaderId"></param>
        /// <param name="keys"></param>
        /// <returns></returns>
        public DataTable GetWaitAuditTimeSheetList(int LeaderId, string keys)
        {
            DataTable dt = new DataTable();

            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"select d.waitCount,c.*,b.code as userCode,b.phone1, b.internalEmail,(dp.level1+'-'+dp.level2+'-'+dp.level3) as dpName from AD_OperationAuditManage as a 
                                inner join sep_employees as b on a.userid=b.userid and status=1
                                inner join sep_users as c on c.userid=b.userid
                                inner join sep_employeesinpositions as ep on ep.userid=a.userid
                                inner join v_department as dp on dp.level3id=ep.departmentid
                                inner join (select userid,count(id) as waitCount from ad_timesheetcommit 
                                where status=@status and (select count(*) from AD_TimeSheet where CommitId =ad_timesheetcommit.Id)<>0 group by userid) as d on c.userid=d.userid ");
            strSql.Append(" where (TeamLeaderId=@LeaderId)");
            if (!string.IsNullOrEmpty(keys))
            {
                strSql.Append(" and (b.code like '%'+@Keys+'%' or c.lastnamecn+c.firstnamecn like '%'+@Keys+'%' or c.username like '%'+@Keys+'%' or dp.level3 like '%'+@Keys+'%' )");
            }
            List<SqlParameter> parms = new List<SqlParameter>();
            parms.Add(new SqlParameter("@LeaderId", LeaderId));
            parms.Add(new SqlParameter("@Keys", keys));
            parms.Add(new SqlParameter("@status", (int)Common.TimeSheetCommitStatus.Commit));

            return DbHelperSQL.Query(strSql.ToString(), parms.ToArray()).Tables[0];
        }

        /// <summary>
        /// 待hr审核
        /// </summary>
        /// <param name="LeaderId"></param>
        /// <param name="keys"></param>
        /// <returns></returns>
        public DataTable GetWaitHRAuditTimeSheetList(int hrId, string keys)
        {
            DataTable dt = new DataTable();

            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"select d.waitCount,c.*,b.code as userCode,b.phone1, b.internalEmail,(dp.level1+'-'+dp.level2+'-'+dp.level3) as dpName from AD_OperationAuditManage as a 
                                inner join sep_employees as b on a.userid=b.userid and status=1
                                inner join sep_users as c on c.userid=b.userid
                                inner join sep_employeesinpositions as ep on ep.userid=a.userid
                                inner join v_department as dp on dp.level3id=ep.departmentid
                                inner join (select userid,count(id) as waitCount from ad_timesheetcommit 
                                where status=@status group by userid) as d on c.userid=d.userid ");
            strSql.Append(" where (HRAdminID=@hrId)");
            if (!string.IsNullOrEmpty(keys))
            {
                strSql.Append(" and (b.code like '%'+@Keys+'%' or c.lastnamecn+c.firstnamecn like '%'+@Keys+'%' or c.username like '%'+@Keys+'%' or dp.level3 like '%'+@Keys+'%' )");
            }
            List<SqlParameter> parms = new List<SqlParameter>();
            parms.Add(new SqlParameter("@hrId", hrId));
            parms.Add(new SqlParameter("@Keys", keys));
            parms.Add(new SqlParameter("@status", (int)Common.TimeSheetCommitStatus.WaitHR));

            return DbHelperSQL.Query(strSql.ToString(), parms.ToArray()).Tables[0];
        }

        /// <summary>
        /// 提交周TimeSheet
        /// </summary>
        public bool SubmitWeek(int userId, string beginDate, string endDate, SqlTransaction trans)
        {
            ESP.Administrative.Entity.OperationAuditManageInfo operation = (new ESP.Administrative.BusinessLogic.OperationAuditManageManager()).GetOperationAuditModelByUserID(userId);
            ESP.HumanResource.Entity.UsersInfo userModel = ESP.HumanResource.BusinessLogic.UsersManager.GetModel(userId);    
            TimeSheetDataProvider timeSheetProvider = new TimeSheetDataProvider();
            DateTime bdate = new DateTime(DateTime.Parse(beginDate).Year, DateTime.Parse(beginDate).Month, 1);
            int endyear = DateTime.Parse(endDate).Year;
            int endmonth = DateTime.Parse(endDate).Month + 1;
            if (endmonth > 12)
            {
                endmonth = 1;
                endyear = DateTime.Parse(endDate).Year+1;
              
            }
            DateTime edate = new DateTime(endyear, endmonth, 1);
            string hr = new ESP.Compatible.Employee(operation.HRAdminID).EMail;

            List<TimeSheetInfo> sicklistcurrent = timeSheetProvider.GetList(" categoryId=" + (int)TimeSheetCategoryIds.SickLeaveId + " and commitid in(select id from ad_timesheetcommit where status not in(2,3,4) and userid=" + userId + " and (currentdate between '" + beginDate + "' and '" + endDate + "'))");
            List<TimeSheetInfo> sicklist = timeSheetProvider.GetList(" categoryId=" + (int)TimeSheetCategoryIds.SickLeaveId + " and commitid in(select id from ad_timesheetcommit where status in(2,3,4) and  userid=" + userId + " and (currentdate between '" + bdate.ToString("yyyy-MM-dd") + "' and '" + edate.ToString("yyyy-MM-dd") + "'))");
            decimal sickhours = sicklist.Sum(x => x.Hours) + sicklistcurrent.Sum(x => x.Hours);

            StringBuilder strSql = new StringBuilder();
            strSql.Append("update ad_timesheetcommit set ");
            strSql.Append("CommitDate=@CommitDate,Status=@Status,AuditDate=@CommitDate");
            strSql.Append(@" where currentdate between @begin and @end and (Status = @Save or Status = @Reject) and userId=@userId and 
                     id not in (select a.id from ad_timesheetcommit a join ad_timesheet b 
                                on a.id=b.commitid where 
                                   currentdate between @begin and @end and (a.Status = @Save or a.Status = @Reject) and a.userId=@userId and
                                a.categoryid in(12,13,14,18,7,16,17));");
            
            //产、病、丧、产检，需HR批，单独更新状态
            strSql.Append("update ad_timesheetcommit set ");
            strSql.Append("CommitDate=@CommitDate,Status=@Status1,AuditDate=@CommitDate");
            strSql.Append(" where currentdate between @begin and @end and (Status = @Save or Status = @Reject) and userId=@userId and id in (select commitid from ad_timesheet where categoryid in(12,13,14,18) and userId=@userId);");
            SqlParameter[] parameters = {
                    new SqlParameter("@CommitDate",SqlDbType.DateTime),
                    new SqlParameter("@Status",SqlDbType.Int,4),
                    new SqlParameter("@begin",SqlDbType.NVarChar,50),
                    new SqlParameter("@end",SqlDbType.NVarChar,50),
                    new SqlParameter("@Save",SqlDbType.Int,4),
                    new SqlParameter("@Reject",SqlDbType.Int,4),
                    new SqlParameter("@userId",SqlDbType.Int,4),
                    new SqlParameter("@Status1",SqlDbType.Int,4)
                                        };
            parameters[0].Value = DateTime.Now;
            parameters[1].Value = (int)ESP.Administrative.Common.TimeSheetCommitStatus.Commit;
            parameters[2].Value = beginDate;
            parameters[3].Value = endDate;
            parameters[4].Value = (int)ESP.Administrative.Common.TimeSheetCommitStatus.Save;
            parameters[5].Value = (int)ESP.Administrative.Common.TimeSheetCommitStatus.Reject;
            parameters[6].Value = userId;
            parameters[7].Value = (int)ESP.Administrative.Common.TimeSheetCommitStatus.Commit;

            int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), trans.Connection, trans, parameters);

            int sickstatus =(int)ESP.Administrative.Common.TimeSheetCommitStatus.Commit;
            int eventstatus = (int)ESP.Administrative.Common.TimeSheetCommitStatus.Commit;

            if (sickhours >= 16)
                {
                    sickstatus = (int)ESP.Administrative.Common.TimeSheetCommitStatus.Commit;
                }
            //病假
            StringBuilder strSql2 = new StringBuilder();
            strSql2.Append("update ad_timesheetcommit set ");
            strSql2.Append("CommitDate=@CommitDate,Status=@Status1,AuditDate=@CommitDate");
            strSql2.Append(" where currentdate between @begin and @end and (Status = @Save or Status = @Reject) and id in (select commitid from ad_timesheet where categoryid in(16) and userId=@userId);");
            SqlParameter[] parameters2 = {
            new SqlParameter("@CommitDate",SqlDbType.DateTime),
            new SqlParameter("@Status1",SqlDbType.Int,4),
            new SqlParameter("@begin",SqlDbType.NVarChar,50),
            new SqlParameter("@end",SqlDbType.NVarChar,50),
            new SqlParameter("@Save",SqlDbType.Int,4),
            new SqlParameter("@Reject",SqlDbType.Int,4),
            new SqlParameter("@userId",SqlDbType.Int,4)
                                };
            parameters2[0].Value = DateTime.Now;
            parameters2[1].Value = sickstatus;
            parameters2[2].Value = beginDate;
            parameters2[3].Value = endDate;
            parameters2[4].Value = (int)ESP.Administrative.Common.TimeSheetCommitStatus.Save;
            parameters2[5].Value = (int)ESP.Administrative.Common.TimeSheetCommitStatus.Reject;
            parameters2[6].Value = userId;
            int rows2 = DbHelperSQL.ExecuteSql(strSql2.ToString(), trans.Connection, trans, parameters2);
            
            //产婚。。。假
            StringBuilder strSql3 = new StringBuilder();
            strSql3.Append("select * from ad_timesheetcommit ");
            strSql3.Append(" where currentdate between @begin and @end and (Status = @commit) and id in (select commitid from ad_timesheet where categoryid in(12,13,14,18) and userId=@userId);");
            SqlParameter[] parameters3 = {
            new SqlParameter("@begin",SqlDbType.NVarChar,50),
            new SqlParameter("@end",SqlDbType.NVarChar,50),
            new SqlParameter("@commit",SqlDbType.Int,4),
            new SqlParameter("@userId",SqlDbType.Int,4)
                                };
            parameters3[0].Value = beginDate;
            parameters3[1].Value = endDate;
            parameters3[2].Value = (int)ESP.Administrative.Common.TimeSheetCommitStatus.Commit;
            parameters3[3].Value = userId;
            var hrauditlist = CBO.FillCollection<TimeSheetCommitInfo>(DbHelperSQL.Query(strSql3.ToString(),trans, parameters3));

            //产婚丧产前email
            //if (hrauditlist.Count > 0)
            //{
            //    if (!string.IsNullOrEmpty(hr))
            //    {
            //        string body = "<br><br>" + userModel.Username + "提交了" + beginDate + "至"+endDate+"的Time Sheet,等待您的审批。";

            //        MailAddress[] recipientAddress = { new MailAddress(hr) };
            //        ESP.Mail.MailManager.Send("Time Sheet审批", body, true, null, recipientAddress, null, null, null);
            //    }
            //}
            //List<TimeSheetInfo> sicklist = timeSheetProvider.GetList(" categoryId=" + (int)TimeSheetCategoryIds.SickLeaveId + " and commitid in(select id from ad_timesheetcommit where status in(2,3,4) and  userid=" + userId + " and (currentdate between '" + bdate.ToString("yyyy-MM-dd") + "' and '" + edate.ToString("yyyy-MM-dd") + "'))");
           // List<TimeSheetInfo> anuallist = timeSheetProvider.GetList("  (categoryId=" + (int)TimeSheetCategoryIds.AnnualLeaveId + " or categoryId=" + (int)TimeSheetCategoryIds.AffairLeaveId + ") and commitid in(select id from ad_timesheetcommit where status in(2,3,4) and  userid=" + userId + " and (currentdate between '" + bdate.ToString("yyyy-MM-dd") + "' and '" + edate.ToString("yyyy-MM-dd") + "'))",trans);
           // decimal sickhours = sicklist.Sum(x => x.Hours);
            //decimal eventhours = anuallist.Where(x => x.CategoryId == (int)TimeSheetCategoryIds.AffairLeaveId).Sum(x => x.Hours);
            //decimal annualhours = anuallist.Where(x => x.CategoryId == (int)TimeSheetCategoryIds.AnnualLeaveId).Sum(x => x.Hours);

            ////病假email
            //if (sickhours >= 16)
            //{
            //    string body = "<br><br>" + userModel.Username + "提交了" + beginDate + "至" + endDate + "的Time Sheet（病假）,等待您的审批。";

            //    MailAddress[] recipientAddress = { new MailAddress(hr) };
            //    ESP.Mail.MailManager.Send("Time Sheet病假审批", body, true, null, recipientAddress, null, null, null);
            //}
            ////年事假email
            //if (eventhours >= 16 || annualhours >= 16)
            //{
            //    string body = "<br><br>" + userModel.Username + "提交了" + beginDate + "至" + endDate + "的Time Sheet（年假、事假）,等待您的审批。";

            //    MailAddress[] recipientAddress = { new MailAddress(hr) };
            //    ESP.Mail.MailManager.Send("Time Sheet年事假提醒", body, true, null, recipientAddress, null, null, null);
            //}

            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 获得有效剩余调休时间
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns></returns>
        public decimal GetUserOverWorkRemain(int UserId)
        {
            string sql = "select sum(overworkRemain) from (select *,(OverWorkHours -(select case when sum(hours) is null then 0 else sum(hours) end from dbo.AD_OverWorkRecords where OverWorkId=AD_TimeSheetCommit.id) ) as OverWorkRemain from dbo.AD_TimeSheetCommit ) a where a.status=3 and userid=@userid and AuditDate >= @endDate";
            SqlParameter[] parms = {
                new SqlParameter("@userid",SqlDbType.Int),
                new SqlParameter("@endDate",SqlDbType.DateTime)
            };

            parms[0].Value = UserId;
            parms[1].Value = DateTime.Now.AddMonths(-1).AddDays(-1).ToString("yyyy-MM-dd");

            object obj = DbHelperSQL.GetSingle(sql, parms);
            if (obj == null)
            {
                return 0;
            }
            else
            {
                return decimal.Parse(obj.ToString());
            }
        }

        public decimal GetRecentUserOverWorkRemain(int UserId)
        {
            string sql = "select sum(overworkRemain) from (select *,(OverWorkHours -(select case when sum(hours) is null then 0 else sum(hours) end from dbo.AD_OverWorkRecords where OverWorkId=AD_TimeSheetCommit.id) ) as OverWorkRemain from dbo.AD_TimeSheetCommit ) a where a.status=3 and userid=@userid and (AuditDate between @beginDate and @endDate)";
            SqlParameter[] parms = {
                new SqlParameter("@userid",SqlDbType.Int),
                new SqlParameter("@beginDate",SqlDbType.DateTime),
                new SqlParameter("@endDate",SqlDbType.DateTime)
            };

            parms[0].Value = UserId;
            parms[1].Value = DateTime.Now.AddMonths(-1).AddDays(-1).ToString("yyyy-MM-dd");
            parms[2].Value = DateTime.Now.AddMonths(-1).AddDays(7).ToString("yyyy-MM-dd");

            object obj = DbHelperSQL.GetSingle(sql, parms);
            if (obj == null)
            {
                return 0;
            }
            else
            {
                return decimal.Parse(obj.ToString());
            }
        }

        public decimal GetUserSumTimeSheetHours(ESP.Administrative.Entity.TimeSheetCommitInfo commitModel)
        {
            string sql = "select sum(hours) from ad_timesheet a join ad_timesheetcommit b on a.commitid=b.id where commitid =@commitId and b.status not in(0,1,2) and a.categoryid not in(4,5,6)";
            SqlParameter[] parms = {
                new SqlParameter("@commitId",SqlDbType.Int)
            };

            parms[0].Value = commitModel.Id;

            object obj = DbHelperSQL.GetSingle(sql, parms);
            if (obj == null || obj==DBNull.Value || obj.ToString()==string.Empty)
            {
                return 0;
            }
            else
            {
                try
                {
                    return decimal.Parse(obj.ToString());
                }
                catch
                {
                    return 0;
                }
            }
        }

        public DataTable GetOTList(string begindate ,string enddate ,string userids)
        {
            DataTable dt = new DataTable();

            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"select * from (
                            select a.userid,b.code,(a.lastnamecn+a.firstnamecn)username,b.phone1,b.mobilephone,a.email ,(d.level1+'-'+d.level2+'-'+d.level3) dept,
                            (select sum(OverWorkHours) from AD_TimeSheetCommit where AD_TimeSheetCommit.userid=a.userid and 
                             currentdate not in(select convert(varchar(10),holidate,120) from dbo.AD_HolidaysInfo where 
                             (holidate>@begindate and holidate<@enddate)) and (currentdate>@begindate and currentdate<@enddate) and status=3) weekOT,
                            (select sum(OverWorkHours) from AD_TimeSheetCommit where AD_TimeSheetCommit.userid=a.userid and 
                             currentdate     in(select convert(varchar(10),holidate,120) from dbo.AD_HolidaysInfo where 
                             (holidate>@begindate and holidate<@enddate)) and (currentdate>@begindate and currentdate<@enddate) and status=3) holidayOT
                            from sep_users a join sep_employees b on a.userid =b.userid 
                            join sep_employeesinpositions c on a.userid =c.userid
                            join v_department d on c.departmentid=d.level3id
                            where a.userid in(");
            strSql.Append(userids);
            strSql.Append(@") and b.status=1) aaa where  (weekOT<>0 or holidayOT<>0) order by dept,code");

          
            List<SqlParameter> parms = new List<SqlParameter>();
            parms.Add(new SqlParameter("@begindate", begindate));
            parms.Add(new SqlParameter("@enddate", enddate));

            return DbHelperSQL.Query(strSql.ToString(), parms.ToArray()).Tables[0];
        }
    }
}
