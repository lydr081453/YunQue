using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESP.Administrative.Entity;
using ESP.Administrative.Common;
using System.Data.SqlClient;
using System.Data;
using ESP.Framework.DataAccess.Utilities;

namespace ESP.Administrative.DataAccess
{
    public class MattersDataProvider
    {
        public MattersDataProvider()
		{}
		#region  成员方法

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int ID)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from AD_Matters");
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
		public int Add(MattersInfo model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into AD_Matters(");
            strSql.Append("UserID,BeginTime,EndTime,MatterType,MatterContent,MatterState,Deleted,CreateTime,UpdateTime,OperateorID,ApproveID,ApproveName,ApproveDesc,AuditingID,AuditingName,AuditingDesc,TotalHours,ProjectID,ProjectNo,AnnualHours,AwardHours)");
			strSql.Append(" values (");
            strSql.Append("@UserID,@BeginTime,@EndTime,@MatterType,@MatterContent,@MatterState,@Deleted,@CreateTime,@UpdateTime,@OperateorID,@ApproveID,@ApproveName,@ApproveDesc,@AuditingID,@AuditingName,@AuditingDesc,@TotalHours,@ProjectID,@ProjectNo,@AnnualHours,@AwardHours)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = {
					new SqlParameter("@UserID", SqlDbType.Int,4),
					new SqlParameter("@BeginTime", SqlDbType.DateTime),
					new SqlParameter("@EndTime", SqlDbType.DateTime),
					new SqlParameter("@MatterType", SqlDbType.Int,4),
					new SqlParameter("@MatterContent", SqlDbType.NVarChar),
					new SqlParameter("@MatterState", SqlDbType.Int,4),
					new SqlParameter("@Deleted", SqlDbType.Bit,1),
					new SqlParameter("@CreateTime", SqlDbType.DateTime),
					new SqlParameter("@UpdateTime", SqlDbType.DateTime),
					new SqlParameter("@OperateorID", SqlDbType.Int,4),
                    new SqlParameter("@ApproveID", SqlDbType.Int, 4),
                    new SqlParameter("@ApproveName", SqlDbType.NVarChar),
                    new SqlParameter("@ApproveDesc", SqlDbType.NVarChar),
                    new SqlParameter("@AuditingID", SqlDbType.Int, 4),
                    new SqlParameter("@AuditingName", SqlDbType.NVarChar),
                    new SqlParameter("@AuditingDesc", SqlDbType.NVarChar),
                    new SqlParameter("@TotalHours", SqlDbType.Decimal),
                    new SqlParameter("@ProjectID", SqlDbType.Int),
                    new SqlParameter("@ProjectNo", SqlDbType.NVarChar, 100),
                    new SqlParameter("@AnnualHours", SqlDbType.Decimal),
                    new SqlParameter("@AwardHours", SqlDbType.Decimal)
                                        };
			parameters[0].Value = model.UserID;
			parameters[1].Value = model.BeginTime;
			parameters[2].Value = model.EndTime;
			parameters[3].Value = model.MatterType;
			parameters[4].Value = model.MatterContent;
			parameters[5].Value = model.MatterState;
			parameters[6].Value = model.Deleted;
			parameters[7].Value = model.CreateTime;
			parameters[8].Value = model.UpdateTime;
			parameters[9].Value = model.OperateorID;
            parameters[10].Value = model.Approveid;
            parameters[11].Value = model.Approvename;
            parameters[12].Value = model.Approvedesc;
            parameters[13].Value = model.Auditingid;
            parameters[14].Value = model.Auditingname;
            parameters[15].Value = model.Auditingdesc;
            parameters[16].Value = model.TotalHours;
            parameters[17].Value = model.ProjectID;
            parameters[18].Value = model.ProjectNo;
            parameters[19].Value = model.AnnualHours;
            parameters[20].Value = model.AwardHours;

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
        /// 增加一条数据
        /// </summary>
        public int Add(MattersInfo model, SqlConnection conn, SqlTransaction trans)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into AD_Matters(");
            strSql.Append("UserID,BeginTime,EndTime,MatterType,MatterContent,MatterState,Deleted,CreateTime,UpdateTime,OperateorID,ApproveID,ApproveName,ApproveDesc,AuditingID,AuditingName,AuditingDesc,TotalHours,ProjectID,ProjectNo,AnnualHours,AwardHours)");
            strSql.Append(" values (");
            strSql.Append("@UserID,@BeginTime,@EndTime,@MatterType,@MatterContent,@MatterState,@Deleted,@CreateTime,@UpdateTime,@OperateorID,@ApproveID,@ApproveName,@ApproveDesc,@AuditingID,@AuditingName,@AuditingDesc,@TotalHours,@ProjectID,@ProjectNo,@AnnualHours,@AwardHours)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@UserID", SqlDbType.Int,4),
					new SqlParameter("@BeginTime", SqlDbType.DateTime),
					new SqlParameter("@EndTime", SqlDbType.DateTime),
					new SqlParameter("@MatterType", SqlDbType.Int,4),
					new SqlParameter("@MatterContent", SqlDbType.NVarChar),
					new SqlParameter("@MatterState", SqlDbType.Int,4),
					new SqlParameter("@Deleted", SqlDbType.Bit,1),
					new SqlParameter("@CreateTime", SqlDbType.DateTime),
					new SqlParameter("@UpdateTime", SqlDbType.DateTime),
					new SqlParameter("@OperateorID", SqlDbType.Int,4),
                    new SqlParameter("@ApproveID", SqlDbType.Int, 4),
                    new SqlParameter("@ApproveName", SqlDbType.NVarChar),
                    new SqlParameter("@ApproveDesc", SqlDbType.NVarChar),
                    new SqlParameter("@AuditingID", SqlDbType.Int, 4),
                    new SqlParameter("@AuditingName", SqlDbType.NVarChar),
                    new SqlParameter("@AuditingDesc", SqlDbType.NVarChar),
                    new SqlParameter("@TotalHours", SqlDbType.Decimal),
                    new SqlParameter("@ProjectID", SqlDbType.Int),
                    new SqlParameter("@ProjectNo", SqlDbType.NVarChar, 100),
                    new SqlParameter("@AnnualHours", SqlDbType.Decimal),
                    new SqlParameter("@AwardHours", SqlDbType.Decimal)
                                        };
            parameters[0].Value = model.UserID;
            parameters[1].Value = model.BeginTime;
            parameters[2].Value = model.EndTime;
            parameters[3].Value = model.MatterType;
            parameters[4].Value = model.MatterContent;
            parameters[5].Value = model.MatterState;
            parameters[6].Value = model.Deleted;
            parameters[7].Value = model.CreateTime;
            parameters[8].Value = model.UpdateTime;
            parameters[9].Value = model.OperateorID;
            parameters[10].Value = model.Approveid;
            parameters[11].Value = model.Approvename;
            parameters[12].Value = model.Approvedesc;
            parameters[13].Value = model.Auditingid;
            parameters[14].Value = model.Auditingname;
            parameters[15].Value = model.Auditingdesc;
            parameters[16].Value = model.TotalHours;
            parameters[17].Value = model.ProjectID;
            parameters[18].Value = model.ProjectNo;
            parameters[19].Value = model.AnnualHours;
            parameters[20].Value = model.AwardHours;

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
		public void Update(MattersInfo model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update AD_Matters set ");
			strSql.Append("UserID=@UserID,");
			strSql.Append("BeginTime=@BeginTime,");
			strSql.Append("EndTime=@EndTime,");
			strSql.Append("MatterType=@MatterType,");
			strSql.Append("MatterContent=@MatterContent,");
			strSql.Append("MatterState=@MatterState,");
			strSql.Append("Deleted=@Deleted,");
			strSql.Append("CreateTime=@CreateTime,");
			strSql.Append("UpdateTime=@UpdateTime,");
            strSql.Append("OperateorID=@OperateorID,");
            strSql.Append("ApproveID=@ApproveID,");
            strSql.Append("ApproveName=@ApproveName,");
            strSql.Append("ApproveDesc=@ApproveDesc,");
            strSql.Append("AuditingID=@AuditingID,");
            strSql.Append("AuditingName=@AuditingName,");
            strSql.Append("AuditingDesc=@AuditingDesc,");
            strSql.Append("TotalHours=@TotalHours,");
            strSql.Append("ProjectID=@ProjectID,");
            strSql.Append("ProjectNo=@ProjectNo,AnnualHours=@AnnualHours,AwardHours=@AwardHours");
			strSql.Append(" where ID=@ID");
			SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4),
					new SqlParameter("@UserID", SqlDbType.Int,4),
					new SqlParameter("@BeginTime", SqlDbType.DateTime),
					new SqlParameter("@EndTime", SqlDbType.DateTime),
					new SqlParameter("@MatterType", SqlDbType.Int,4),
					new SqlParameter("@MatterContent", SqlDbType.NVarChar),
					new SqlParameter("@MatterState", SqlDbType.Int,4),
					new SqlParameter("@Deleted", SqlDbType.Bit,1),
					new SqlParameter("@CreateTime", SqlDbType.DateTime),
					new SqlParameter("@UpdateTime", SqlDbType.DateTime),
					new SqlParameter("@OperateorID", SqlDbType.Int,4),
                    new SqlParameter("@ApproveID", SqlDbType.Int, 4),
                    new SqlParameter("@ApproveName", SqlDbType.NVarChar),
                    new SqlParameter("@ApproveDesc", SqlDbType.NVarChar),
                    new SqlParameter("@AuditingID", SqlDbType.Int, 4),
                    new SqlParameter("@AuditingName", SqlDbType.NVarChar),
                    new SqlParameter("@AuditingDesc", SqlDbType.NVarChar),
                    new SqlParameter("@TotalHours", SqlDbType.Decimal),
                    new SqlParameter("@ProjectID", SqlDbType.Int),
                    new SqlParameter("@ProjectNo", SqlDbType.NVarChar, 100),
                     new SqlParameter("@AnnualHours", SqlDbType.Decimal),
                      new SqlParameter("@AwardHours", SqlDbType.Decimal)
                                        };
			parameters[0].Value = model.ID;
			parameters[1].Value = model.UserID;
			parameters[2].Value = model.BeginTime;
			parameters[3].Value = model.EndTime;
			parameters[4].Value = model.MatterType;
			parameters[5].Value = model.MatterContent;
			parameters[6].Value = model.MatterState;
			parameters[7].Value = model.Deleted;
			parameters[8].Value = model.CreateTime;
			parameters[9].Value = model.UpdateTime;
			parameters[10].Value = model.OperateorID;
            parameters[11].Value = model.Approveid;
            parameters[12].Value = model.Approvename;
            parameters[13].Value = model.Approvedesc;
            parameters[14].Value = model.Auditingid;
            parameters[15].Value = model.Auditingname;
            parameters[16].Value = model.Auditingdesc;
            parameters[17].Value = model.TotalHours;
            parameters[18].Value = model.ProjectID;
            parameters[19].Value = model.ProjectNo;
            parameters[20].Value = model.AnnualHours;
            parameters[21].Value = model.AwardHours;
			DbHelperSQL.ExecuteSql(strSql.ToString(),parameters);
		}

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public int Update(MattersInfo model, SqlConnection conn, SqlTransaction trans)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update AD_Matters set ");
            strSql.Append("UserID=@UserID,");
            strSql.Append("BeginTime=@BeginTime,");
            strSql.Append("EndTime=@EndTime,");
            strSql.Append("MatterType=@MatterType,");
            strSql.Append("MatterContent=@MatterContent,");
            strSql.Append("MatterState=@MatterState,");
            strSql.Append("Deleted=@Deleted,");
            strSql.Append("CreateTime=@CreateTime,");
            strSql.Append("UpdateTime=@UpdateTime,");
            strSql.Append("OperateorID=@OperateorID,");
            strSql.Append("ApproveID=@ApproveID,");
            strSql.Append("ApproveName=@ApproveName,");
            strSql.Append("ApproveDesc=@ApproveDesc,");
            strSql.Append("AuditingID=@AuditingID,");
            strSql.Append("AuditingName=@AuditingName,");
            strSql.Append("AuditingDesc=@AuditingDesc,");
            strSql.Append("TotalHours=@TotalHours,");
            strSql.Append("ProjectID=@ProjectID,");
            strSql.Append("ProjectNo=@ProjectNo,AnnualHours=@AnnualHours,AwardHours=@AwardHours");
            strSql.Append(" where ID=@ID");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4),
					new SqlParameter("@UserID", SqlDbType.Int,4),
					new SqlParameter("@BeginTime", SqlDbType.DateTime),
					new SqlParameter("@EndTime", SqlDbType.DateTime),
					new SqlParameter("@MatterType", SqlDbType.Int,4),
					new SqlParameter("@MatterContent", SqlDbType.NVarChar),
					new SqlParameter("@MatterState", SqlDbType.Int,4),
					new SqlParameter("@Deleted", SqlDbType.Bit,1),
					new SqlParameter("@CreateTime", SqlDbType.DateTime),
					new SqlParameter("@UpdateTime", SqlDbType.DateTime),
					new SqlParameter("@OperateorID", SqlDbType.Int,4),
                    new SqlParameter("@ApproveID", SqlDbType.Int, 4),
                    new SqlParameter("@ApproveName", SqlDbType.NVarChar),
                    new SqlParameter("@ApproveDesc", SqlDbType.NVarChar),
                    new SqlParameter("@AuditingID", SqlDbType.Int, 4),
                    new SqlParameter("@AuditingName", SqlDbType.NVarChar),
                    new SqlParameter("@AuditingDesc", SqlDbType.NVarChar),
                    new SqlParameter("@TotalHours", SqlDbType.Decimal),
                    new SqlParameter("@ProjectID", SqlDbType.Int),
                    new SqlParameter("@ProjectNo", SqlDbType.NVarChar, 100),
                     new SqlParameter("@AnnualHours", SqlDbType.Decimal),
                      new SqlParameter("@AwardHours", SqlDbType.Decimal)
                                        };
            parameters[0].Value = model.ID;
            parameters[1].Value = model.UserID;
            parameters[2].Value = model.BeginTime;
            parameters[3].Value = model.EndTime;
            parameters[4].Value = model.MatterType;
            parameters[5].Value = model.MatterContent;
            parameters[6].Value = model.MatterState;
            parameters[7].Value = model.Deleted;
            parameters[8].Value = model.CreateTime;
            parameters[9].Value = model.UpdateTime;
            parameters[10].Value = model.OperateorID;
            parameters[11].Value = model.Approveid;
            parameters[12].Value = model.Approvename;
            parameters[13].Value = model.Approvedesc;
            parameters[14].Value = model.Auditingid;
            parameters[15].Value = model.Auditingname;
            parameters[16].Value = model.Auditingdesc;
            parameters[17].Value = model.TotalHours;
            parameters[18].Value = model.ProjectID;
            parameters[19].Value = model.ProjectNo;
            parameters[20].Value = model.AnnualHours;
            parameters[21].Value = model.AwardHours;
            return DbHelperSQL.ExecuteSql(strSql.ToString(), conn, trans, parameters);
        }

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public void Delete(int ID)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete AD_Matters ");
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
		public MattersInfo GetModel(int ID)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select * from AD_Matters ");
			strSql.Append(" where ID=@ID");
			SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)};
			parameters[0].Value = ID;
			MattersInfo model=new MattersInfo();
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
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * FROM AD_V_Matters ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 获得考勤事由信息集合
        /// </summary>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        public DataSet GetMatterInfosList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM AD_Matters ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }
       
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public DataSet GetMattersViewList(string strWhere)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select * FROM AD_V_Matters ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where 1=1 " + strWhere);
			}
            strSql.Append(" ORDER BY BeginTime desc");
            //IDataReader reader = DbHelperSQL.ExecuteReader(strSql.ToString(), new SqlParameter[]{});
            //return (List<ESP.Administrative.Entity.MattersExtendInfo>) CBO.LoadList<MattersExtendInfo>(reader);

            //return ESP.Administrative.Common.CBO.FillCollection<MattersExtendInfo>(DbHelperSQL.Query(strSql.ToString()));
			return DbHelperSQL.Query(strSql.ToString());
        }


        public IList<MattersInfo> GetModelListByMonth(int userId, int year, int month)
        {
            string sql = @"
                DECLARE @RangeStart DATETIME
                DECLARE @RangeEnd DATETIME

                SET @RangeStart = CAST(@Year AS nvarchar(10)) + N'-' + CAST(@Month AS nvarchar(10)) + '-1'
                SET @RangeEnd = DATEADD(month, 1, @RangeStart)

                SELECT * FROM AD_Matters
                WHERE UserId= @UserId and Deleted='false' 
	                AND ((BeginTime >= @RangeStart AND BeginTime < @RangeEnd)
	                OR (EndTime >= @RangeStart AND EndTime < @RangeEnd)
                    OR (BeginTime < @RangeStart AND EndTime > @RangeEnd))
                ";
            IDataReader reader = DbHelperSQL.ExecuteReader(sql, new SqlParameter[]{
                new SqlParameter("@UserId", userId),
                new SqlParameter("@Year", year),
                new SqlParameter("@Month", month)
            });

            return ESP.Framework.DataAccess.Utilities.CBO.LoadList<MattersInfo>(reader);
        }

        /// <summary>
        /// 获得考勤统计数据
        /// </summary>
        /// <param name="userId">用户编号</param>
        /// <param name="year">年份</param>
        /// <param name="month">月份</param>
        /// <returns>返回考勤统计对象</returns>
        public Dictionary<int, List<MattersInfo>> GetStatModelList(int year, int month, int day)
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

                SELECT * FROM AD_Matters
                WHERE Deleted='false' 
	                AND ((BeginTime >= @RangeStart AND BeginTime < @RangeEnd)
	                OR (EndTime >= @RangeStart AND EndTime < @RangeEnd)) ";
            using (IDataReader reader = DbHelperSQL.ExecuteReader(sql, new SqlParameter[]{
                new SqlParameter("@RangeStartTime", rangeStart),
                new SqlParameter("@RangeEndTime", rangeEnd)
            }))
            {
                Dictionary<int, List<MattersInfo>> dic = new Dictionary<int, List<MattersInfo>>();
                while (reader.Read())
                {
                    int userid = int.Parse(reader["UserId"].ToString());
                    MattersInfo matterModel = new MattersInfo();
                    matterModel.PopupData(reader);
                    if (dic.ContainsKey(userid))
                    {
                        dic[userid].Add(matterModel);
                    }
                    else
                    {
                        List<MattersInfo> list = new List<MattersInfo>();
                        list.Add(matterModel);
                        dic.Add(userid, list);
                    }
                }
                return dic;
            }
        }

        /// <summary>
        /// 获得笔记本报销15个工作日计算不包含的所有考勤事由类型，并返回一个Dictionary,key:用户编号,value:事由类型对象
        /// </summary>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <returns></returns>
        public Dictionary<int, List<MattersInfo>> GetNoRefundMatters(int year, int month)
        {
            string sql = @"
                DECLARE @RangeStart DATETIME
                DECLARE @RangeEnd DATETIME

                SET @RangeStart = CAST(@Year AS nvarchar(10)) + N'-' + CAST(@Month AS nvarchar(10)) + '-1'
                SET @RangeEnd = DATEADD(month, 1, @RangeStart)

                SELECT * FROM AD_Matters
                WHERE Deleted='false' 
                    AND MattetType IN (@MatterType) 
	                AND ((BeginTime >= @RangeStart AND BeginTime < @RangeEnd)
	                OR (EndTime >= @RangeStart AND EndTime < @RangeEnd)
                    OR (BeginTime < @RangeStart AND EndTime > @RangeEnd))
                ";
            
            using (IDataReader reader = DbHelperSQL.ExecuteReader(sql, new SqlParameter[]{
                new SqlParameter("@Year", year),
                new SqlParameter("@Month", month),
                new SqlParameter("@MatterType", Status.NoRefundMatterTypes)
            }))
            {
                Dictionary<int, List<MattersInfo>> dic = new Dictionary<int, List<MattersInfo>>();
                while (reader.Read())
                {
                    int userid = int.Parse(reader["UserId"].ToString());
                    MattersInfo matterModel = new MattersInfo();
                    matterModel.PopupData(reader);
                    if (dic.ContainsKey(userid))
                    {
                        dic[userid].Add(matterModel);
                    }
                    else
                    {
                        List<MattersInfo> list = new List<MattersInfo>();
                        list.Add(matterModel);
                        dic.Add(userid, list);
                    }
                }
                return dic;
            }
        }
		#endregion  成员方法
	}
}