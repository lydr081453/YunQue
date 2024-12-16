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
    public class OperationAuditManageDataProvider
    {
        public OperationAuditManageDataProvider()
		{}

		#region  成员方法
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int ID)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from AD_OperationAuditManage");
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
		public int Add(OperationAuditManageInfo model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into AD_OperationAuditManage(");
            strSql.Append("UserID,TeamLeaderID,TeamLeaderName,HRAdminID,HRAdminName,ManagerID,ManagerName,StatisticianID,StatisticianName,Deleted,CreateTime,UpdateTime,OperatorID,OperatorDept,Sort,AreaID)");
			strSql.Append(" values (");
            strSql.Append("@UserID,@TeamLeaderID,@TeamLeaderName,@HRAdminID,@HRAdminName,@ManagerID,@ManagerName,@StatisticianID,@StatisticianName,@Deleted,@CreateTime,@UpdateTime,@OperatorID,@OperatorDept,@Sort,@AreaID)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = {
					new SqlParameter("@UserID", SqlDbType.Int,4),
					new SqlParameter("@TeamLeaderID", SqlDbType.Int,4),
					new SqlParameter("@TeamLeaderName", SqlDbType.NVarChar),
					new SqlParameter("@HRAdminID", SqlDbType.Int,4),
					new SqlParameter("@HRAdminName", SqlDbType.NVarChar),
					new SqlParameter("@ManagerID", SqlDbType.Int,4),
					new SqlParameter("@ManagerName", SqlDbType.NVarChar),
					new SqlParameter("@StatisticianID", SqlDbType.Int,4),
					new SqlParameter("@StatisticianName", SqlDbType.NVarChar),
					new SqlParameter("@Deleted", SqlDbType.Bit,1),
					new SqlParameter("@CreateTime", SqlDbType.DateTime),
					new SqlParameter("@UpdateTime", SqlDbType.DateTime),
					new SqlParameter("@OperatorID", SqlDbType.Int,4),
					new SqlParameter("@OperatorDept", SqlDbType.Int,4),
					new SqlParameter("@Sort", SqlDbType.Int,4),
                    new SqlParameter("@AreaID",SqlDbType.Int,4)};
			parameters[0].Value = model.UserID;
			parameters[1].Value = model.TeamLeaderID;
			parameters[2].Value = model.TeamLeaderName;
			parameters[3].Value = model.HRAdminID;
			parameters[4].Value = model.HRAdminName;
			parameters[5].Value = model.ManagerID;
			parameters[6].Value = model.ManagerName;
			parameters[7].Value = model.StatisticianID;
			parameters[8].Value = model.StatisticianName;
			parameters[9].Value = model.Deleted;
			parameters[10].Value = model.CreateTime;
			parameters[11].Value = model.UpdateTime;
			parameters[12].Value = model.OperatorID;
			parameters[13].Value = model.OperatorDept;
			parameters[14].Value = model.Sort;
            parameters[15].Value = model.AreaID;

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
        /// 添加考勤审批人信息
        /// </summary>
        /// <param name="model"></param>
        /// <param name="conn"></param>
        /// <param name="trans"></param>
        /// <returns></returns>
        public int Add(OperationAuditManageInfo model, SqlConnection conn, SqlTransaction trans)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into AD_OperationAuditManage(");
            strSql.Append("UserID,TeamLeaderID,TeamLeaderName,HRAdminID,HRAdminName,ManagerID,ManagerName,StatisticianID,StatisticianName,Deleted,CreateTime,UpdateTime,OperatorID,OperatorDept,Sort,AreaID)");
            strSql.Append(" values (");
            strSql.Append("@UserID,@TeamLeaderID,@TeamLeaderName,@HRAdminID,@HRAdminName,@ManagerID,@ManagerName,@StatisticianID,@StatisticianName,@Deleted,@CreateTime,@UpdateTime,@OperatorID,@OperatorDept,@Sort,@AreaID)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@UserID", SqlDbType.Int,4),
					new SqlParameter("@TeamLeaderID", SqlDbType.Int,4),
					new SqlParameter("@TeamLeaderName", SqlDbType.NVarChar),
					new SqlParameter("@HRAdminID", SqlDbType.Int,4),
					new SqlParameter("@HRAdminName", SqlDbType.NVarChar),
					new SqlParameter("@ManagerID", SqlDbType.Int,4),
					new SqlParameter("@ManagerName", SqlDbType.NVarChar),
					new SqlParameter("@StatisticianID", SqlDbType.Int,4),
					new SqlParameter("@StatisticianName", SqlDbType.NVarChar),
					new SqlParameter("@Deleted", SqlDbType.Bit,1),
					new SqlParameter("@CreateTime", SqlDbType.DateTime),
					new SqlParameter("@UpdateTime", SqlDbType.DateTime),
					new SqlParameter("@OperatorID", SqlDbType.Int,4),
					new SqlParameter("@OperatorDept", SqlDbType.Int,4),
					new SqlParameter("@Sort", SqlDbType.Int,4),
                    new SqlParameter("@AreaID",SqlDbType.Int,4)};
            parameters[0].Value = model.UserID;
            parameters[1].Value = model.TeamLeaderID;
            parameters[2].Value = model.TeamLeaderName;
            parameters[3].Value = model.HRAdminID;
            parameters[4].Value = model.HRAdminName;
            parameters[5].Value = model.ManagerID;
            parameters[6].Value = model.ManagerName;
            parameters[7].Value = model.StatisticianID;
            parameters[8].Value = model.StatisticianName;
            parameters[9].Value = model.Deleted;
            parameters[10].Value = model.CreateTime;
            parameters[11].Value = model.UpdateTime;
            parameters[12].Value = model.OperatorID;
            parameters[13].Value = model.OperatorDept;
            parameters[14].Value = model.Sort;
            parameters[15].Value = model.AreaID;

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
        public void Update(OperationAuditManageInfo model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update AD_OperationAuditManage set ");
			strSql.Append("UserID=@UserID,");
			strSql.Append("TeamLeaderID=@TeamLeaderID,");
			strSql.Append("TeamLeaderName=@TeamLeaderName,");
			strSql.Append("HRAdminID=@HRAdminID,");
			strSql.Append("HRAdminName=@HRAdminName,");
			strSql.Append("ManagerID=@ManagerID,");
			strSql.Append("ManagerName=@ManagerName,");
			strSql.Append("StatisticianID=@StatisticianID,");
			strSql.Append("StatisticianName=@StatisticianName,");
			strSql.Append("Deleted=@Deleted,");
			strSql.Append("CreateTime=@CreateTime,");
			strSql.Append("UpdateTime=@UpdateTime,");
			strSql.Append("OperatorID=@OperatorID,");
			strSql.Append("OperatorDept=@OperatorDept,");
			strSql.Append("Sort=@Sort,");
            strSql.Append("AreaID=@AreaID ");
			strSql.Append(" where ID=@ID");
			SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4),
					new SqlParameter("@UserID", SqlDbType.Int,4),
					new SqlParameter("@TeamLeaderID", SqlDbType.Int,4),
					new SqlParameter("@TeamLeaderName", SqlDbType.NVarChar),
					new SqlParameter("@HRAdminID", SqlDbType.Int,4),
					new SqlParameter("@HRAdminName", SqlDbType.NVarChar),
					new SqlParameter("@ManagerID", SqlDbType.Int,4),
					new SqlParameter("@ManagerName", SqlDbType.NVarChar),
					new SqlParameter("@StatisticianID", SqlDbType.Int,4),
					new SqlParameter("@StatisticianName", SqlDbType.NVarChar),
					new SqlParameter("@Deleted", SqlDbType.Bit,1),
					new SqlParameter("@CreateTime", SqlDbType.DateTime),
					new SqlParameter("@UpdateTime", SqlDbType.DateTime),
					new SqlParameter("@OperatorID", SqlDbType.Int,4),
					new SqlParameter("@OperatorDept", SqlDbType.Int,4),
					new SqlParameter("@Sort", SqlDbType.Int,4),
                    new SqlParameter("@AreaID",SqlDbType.Int,4)};
			parameters[0].Value = model.ID;
			parameters[1].Value = model.UserID;
			parameters[2].Value = model.TeamLeaderID;
			parameters[3].Value = model.TeamLeaderName;
			parameters[4].Value = model.HRAdminID;
			parameters[5].Value = model.HRAdminName;
			parameters[6].Value = model.ManagerID;
			parameters[7].Value = model.ManagerName;
			parameters[8].Value = model.StatisticianID;
			parameters[9].Value = model.StatisticianName;
			parameters[10].Value = model.Deleted;
			parameters[11].Value = model.CreateTime;
			parameters[12].Value = model.UpdateTime;
			parameters[13].Value = model.OperatorID;
			parameters[14].Value = model.OperatorDept;
			parameters[15].Value = model.Sort;
            parameters[16].Value = model.AreaID;

			DbHelperSQL.ExecuteSql(strSql.ToString(),parameters);
		}

        /// <summary>
        /// 更新考勤审批人信息
        /// </summary>
        /// <param name="model"></param>
        /// <param name="conn"></param>
        /// <param name="trans"></param>
        public void Update(OperationAuditManageInfo model, SqlConnection conn, SqlTransaction trans)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update AD_OperationAuditManage set ");
            strSql.Append("UserID=@UserID,");
            strSql.Append("TeamLeaderID=@TeamLeaderID,");
            strSql.Append("TeamLeaderName=@TeamLeaderName,");
            strSql.Append("HRAdminID=@HRAdminID,");
            strSql.Append("HRAdminName=@HRAdminName,");
            strSql.Append("ManagerID=@ManagerID,");
            strSql.Append("ManagerName=@ManagerName,");
            strSql.Append("StatisticianID=@StatisticianID,");
            strSql.Append("StatisticianName=@StatisticianName,");
            strSql.Append("Deleted=@Deleted,");
            strSql.Append("CreateTime=@CreateTime,");
            strSql.Append("UpdateTime=@UpdateTime,");
            strSql.Append("OperatorID=@OperatorID,");
            strSql.Append("OperatorDept=@OperatorDept,");
            strSql.Append("Sort=@Sort,");
            strSql.Append("AreaID=@AreaID ");
            strSql.Append(" where ID=@ID");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4),
					new SqlParameter("@UserID", SqlDbType.Int,4),
					new SqlParameter("@TeamLeaderID", SqlDbType.Int,4),
					new SqlParameter("@TeamLeaderName", SqlDbType.NVarChar),
					new SqlParameter("@HRAdminID", SqlDbType.Int,4),
					new SqlParameter("@HRAdminName", SqlDbType.NVarChar),
					new SqlParameter("@ManagerID", SqlDbType.Int,4),
					new SqlParameter("@ManagerName", SqlDbType.NVarChar),
					new SqlParameter("@StatisticianID", SqlDbType.Int,4),
					new SqlParameter("@StatisticianName", SqlDbType.NVarChar),
					new SqlParameter("@Deleted", SqlDbType.Bit,1),
					new SqlParameter("@CreateTime", SqlDbType.DateTime),
					new SqlParameter("@UpdateTime", SqlDbType.DateTime),
					new SqlParameter("@OperatorID", SqlDbType.Int,4),
					new SqlParameter("@OperatorDept", SqlDbType.Int,4),
					new SqlParameter("@Sort", SqlDbType.Int,4),
                    new SqlParameter("@AreaID",SqlDbType.Int,4)};
            parameters[0].Value = model.ID;
            parameters[1].Value = model.UserID;
            parameters[2].Value = model.TeamLeaderID;
            parameters[3].Value = model.TeamLeaderName;
            parameters[4].Value = model.HRAdminID;
            parameters[5].Value = model.HRAdminName;
            parameters[6].Value = model.ManagerID;
            parameters[7].Value = model.ManagerName;
            parameters[8].Value = model.StatisticianID;
            parameters[9].Value = model.StatisticianName;
            parameters[10].Value = model.Deleted;
            parameters[11].Value = model.CreateTime;
            parameters[12].Value = model.UpdateTime;
            parameters[13].Value = model.OperatorID;
            parameters[14].Value = model.OperatorDept;
            parameters[15].Value = model.Sort;
            parameters[16].Value = model.AreaID;

            DbHelperSQL.ExecuteSql(strSql.ToString(), conn, trans, parameters);
        }

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public void Delete(int ID)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete AD_OperationAuditManage ");
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
        public OperationAuditManageInfo GetModel(int ID)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select * from AD_OperationAuditManage ");
			strSql.Append(" where ID=@ID");
			SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)};
			parameters[0].Value = ID;
            OperationAuditManageInfo model = new OperationAuditManageInfo();
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
			strSql.Append("select * ");
			strSql.Append(" FROM AD_OperationAuditManage ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			return DbHelperSQL.Query(strSql.ToString());
		}

        /// <summary>
        /// 获得所有的权限信息
        /// </summary>
        /// <returns></returns>
        public IList<ESP.Framework.Entity.EmployeeInfo> GetAllList()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * ");
            strSql.Append(" FROM AD_OperationAuditManage WHERE Deleted=0");
            DataSet ds = DbHelperSQL.Query(strSql.ToString());
            IList<ESP.Framework.Entity.EmployeeInfo> list = new List<ESP.Framework.Entity.EmployeeInfo>();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                StringBuilder userids = new StringBuilder();
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    userids.Append(dr["UserID"].ToString() + ",");
                }
                string strUserIds = "";
                if (userids.ToString().EndsWith(","))
                {
                    strUserIds = userids.ToString().TrimEnd(new char[] { ',' });
                }
                if (!string.IsNullOrEmpty(strUserIds))
                {
                    DataSet employeeds = ESP.HumanResource.BusinessLogic.EmployeeBaseManager.GetList(" AND a.UserID IN (" + strUserIds + ") ");
                    if (employeeds != null && employeeds.Tables.Count > 0 && employeeds.Tables[0].Rows.Count > 0)
                    {
                        list = CBO.FillCollection(employeeds.Tables[0], ref list);
                    }
                }
            }
            return list;
        }

        /// <summary>
        /// 获得用户审批信息
        /// </summary>
        /// <param name="Userid">用户编号</param>
        /// <returns>返回审批人信息</returns>
        public OperationAuditManageInfo GetOperationAuditModelByUserID(int Userid)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from AD_OperationAuditManage ");
            strSql.Append(" where Deleted=0 AND UserID=@UserID ");
            SqlParameter[] parameters = {
					new SqlParameter("@UserID", SqlDbType.Int,4)};
            parameters[0].Value = Userid;

            OperationAuditManageInfo model = new OperationAuditManageInfo();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
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
        /// 获得被用户所审批人员的信息
        /// </summary>
        /// <param name="UserID">用户编号</param>
        /// <returns>返回被审批人的信息集合</returns>
        public List<OperationAuditManageInfo> GetUnderlingsInfo(int UserID)
        {
            string sql = "select * from Ad_OperationAuditManage where deleted=0 and TeamLeaderID=@TeamLeaderID";
            SqlParameter[] parameters = { 
                    new SqlParameter("@TeamLeaderID", SqlDbType.Int, 4)};
            parameters[0].Value = UserID;
            List<OperationAuditManageInfo> list = new List<OperationAuditManageInfo>();
            DataSet ds = DbHelperSQL.Query(sql, parameters);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    OperationAuditManageInfo model = new OperationAuditManageInfo();
                    model.PopupData(dr);
                    list.Add(model);
                }
            }
            return list;
        }

        /// <summary>
        /// 获得用户所有的下属人员信息
        /// </summary>
        /// <param name="UserId">用户编号</param>
        /// <returns>返回用户所有下属人员信息</returns>
        public void GetAllSubordinates(int UserId, ref List<ESP.Framework.Entity.EmployeeInfo> list)
        {
            string sql = @"select emp.* from sep_employees emp join ad_operationauditmanage a on emp.userid=a.userid join SEP_EmployeesInPositions b ON a.userid=b.userid 
join sep_OperationAuditManage c on b.DepartmentID =c.DepId 
where (c.DirectorId= @UserId or c.ManagerId =@UserId or c.CEOId =@UserId or HRAdminID =@UserId or HRAttendanceId =@UserId or a.teamleaderid =@UserId)
 ORDER BY a.UserID";
            SqlParameter[] parameters = {
					new SqlParameter("@UserId", SqlDbType.Int, 4)};
            parameters[0].Value = UserId;
            //DataSet ds = DbHelperSQL.Query(sql, parameters);
            //if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            //{
            //    foreach (DataRow dr in ds.Tables[0].Rows)
            //    {
            //        ESP.Framework.Entity.EmployeeInfo employeeModel = ESP.Framework.BusinessLogic.EmployeeManager.Get(int.Parse(dr["UserID"].ToString()));
            //        list.Add(employeeModel);
            //      //  GetAllSubordinates(int.Parse(dr["UserID"].ToString()), ref list);
            //    }
            //}

            list = CBO.FillCollection<ESP.Framework.Entity.EmployeeInfo>(DbHelperSQL.Query(sql, parameters));
        }

          

        /// <summary>
        /// 获得团队HRAdmin下的所有人员信息
        /// </summary>
        /// <param name="UserId">团队HRAdmin人员编号</param>
        /// <returns>返回所有人员信息</returns>
        public List<ESP.Framework.Entity.EmployeeInfo> GetHRAdminSubordinates(int UserId)
        {
            string sql = "SELECT * FROM AD_OperationauditManage WHERE (HRAdminID=@HRAdminID) ORDER BY UserID";
            SqlParameter[] parameters = {
					new SqlParameter("@HRAdminID", SqlDbType.Int, 4)};
            parameters[0].Value = UserId;
            DataSet ds = DbHelperSQL.Query(sql, parameters);
            List<ESP.Framework.Entity.EmployeeInfo> list = new List<ESP.Framework.Entity.EmployeeInfo>();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ESP.Framework.Entity.EmployeeInfo employeeModel = ESP.Framework.BusinessLogic.EmployeeManager.Get(int.Parse(dr["UserID"].ToString()));
                    list.Add(employeeModel);
                }
            }
            return list;
        }
        
        /// <summary>
        /// 获得团队考勤统计员下的所有人员信息
        /// </summary>
        /// <param name="UserID"></param>
        /// <returns></returns>
        public List<ESP.Framework.Entity.EmployeeInfo> GetStatisticianSubordinates(int UserId)
        {
            string sql = "SELECT * FROM AD_OperationauditManage WHERE StatisticianID=@StatisticianID ORDER BY UserID";
            SqlParameter[] parameters = {
					new SqlParameter("@StatisticianID", SqlDbType.Int, 4)};
            parameters[0].Value = UserId;
            DataSet ds = DbHelperSQL.Query(sql, parameters);
            List<ESP.Framework.Entity.EmployeeInfo> list = new List<ESP.Framework.Entity.EmployeeInfo>();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ESP.Framework.Entity.EmployeeInfo employeeModel = ESP.Framework.BusinessLogic.EmployeeManager.Get(int.Parse(dr["UserID"].ToString()));
                    list.Add(employeeModel);
                }
            }
            return list;
        }


        /// <summary>
        /// 获得当前领导下属的所有人员信息
        /// </summary>
        /// <param name="UserID"></param>
        /// <returns></returns>
        public List<ESP.Framework.Entity.EmployeeInfo> GetListForSingleOvertimes(int UserId)
        {
            string sql = "SELECT distinct UserID FROM AD_OperationauditManage WHERE (StatisticianID=@StatisticianID or HRAdminID=@HRAdminID or TeamLeaderid=@TeamLeaderid or ManagerID =@ManagerID) ORDER BY UserID";
            SqlParameter[] parameters = {
					new SqlParameter("@StatisticianID", SqlDbType.Int, 4),
					new SqlParameter("@HRAdminID", SqlDbType.Int, 4),
					new SqlParameter("@TeamLeaderid", SqlDbType.Int, 4),
					new SqlParameter("@ManagerID", SqlDbType.Int, 4)};
            parameters[0].Value = UserId;
            parameters[1].Value = UserId;
            parameters[2].Value = UserId;
            parameters[3].Value = UserId;
            DataSet ds = DbHelperSQL.Query(sql, parameters);
            List<ESP.Framework.Entity.EmployeeInfo> list = new List<ESP.Framework.Entity.EmployeeInfo>();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ESP.Framework.Entity.EmployeeInfo employeeModel = ESP.Framework.BusinessLogic.EmployeeManager.Get(int.Parse(dr["UserID"].ToString()));
                    list.Add(employeeModel);
                }
            }
            return list;
        }

        public List<ESP.Framework.Entity.EmployeeInfo> GetCreativeUsers(string deptids)
        {
            string sql = "SELECT * FROM AD_OperationauditManage WHERE userid in(select userid from sep_employeesinpositions where departmentid in("+deptids+")) ORDER BY UserID";
           
            DataSet ds = DbHelperSQL.Query(sql);
            List<ESP.Framework.Entity.EmployeeInfo> list = new List<ESP.Framework.Entity.EmployeeInfo>();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ESP.Framework.Entity.EmployeeInfo employeeModel = ESP.Framework.BusinessLogic.EmployeeManager.Get(int.Parse(dr["UserID"].ToString()));
                    list.Add(employeeModel);
                }
            }
            return list;
        }

        /// <summary>
        /// 获得各地区的人员信息
        /// </summary>
        /// <param name="areaId">地区编号</param>
        /// <returns>返回该地区的人员信息集合</returns>
        public List<ESP.Framework.Entity.EmployeeInfo> GetADAdminInfos(int areaId)
        {
            string sql = "SELECT * FROM AD_OperationauditManage WHERE AreaID=@AreaID ORDER BY UserID";
            SqlParameter[] parameters = {
					new SqlParameter("@AreaID", SqlDbType.Int, 4)};
            parameters[0].Value = areaId;
            DataSet ds = DbHelperSQL.Query(sql, parameters);
            List<ESP.Framework.Entity.EmployeeInfo> list = new List<ESP.Framework.Entity.EmployeeInfo>();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ESP.Framework.Entity.EmployeeInfo employeeModel = ESP.Framework.BusinessLogic.EmployeeManager.Get(int.Parse(dr["UserID"].ToString()));
                    list.Add(employeeModel);
                }
            }
            return list;
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetListIncludeUserName(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select o.*,u.lastnamecn+u.firstnamecn as UserName ");
            strSql.Append(" FROM AD_OperationAuditManage o left join sep_users u on o.userid = u.userid  ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }
		#endregion  成员方法
	}
}