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
    public class ALAndRLDataProvider
    {
        public ALAndRLDataProvider()
        { }

        #region  成员方法
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT COUNT(1) FROM AD_ALAndRL");
            strSql.Append(" WHERE ID= @ID");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)
				};
            parameters[0].Value = ID;
            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(ALAndRLInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("INSERT INTO AD_ALAndRL(");
            strSql.Append("UserID,UserCode,UserName,EmployeeName,LeaveYear,LeaveMonth,LeaveNumber,RemainingNumber,LeaveType,ValidTo,Deleted,CreateTime,UpdateTime,OperatorID,OperatorDept)");
            strSql.Append(" VALUES (");
            strSql.Append("@UserID,@UserCode,@UserName,@EmployeeName,@LeaveYear,@LeaveMonth,@LeaveNumber,@RemainingNumber,@LeaveType,@ValidTo,@Deleted,@CreateTime,@UpdateTime,@OperatorID,@OperatorDept)");
            strSql.Append(";SELECT @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@UserID", SqlDbType.Int,4),
					new SqlParameter("@UserCode", SqlDbType.NVarChar),
					new SqlParameter("@UserName", SqlDbType.NVarChar),
					new SqlParameter("@EmployeeName", SqlDbType.NVarChar),
					new SqlParameter("@LeaveYear", SqlDbType.Int,4),
					new SqlParameter("@LeaveMonth", SqlDbType.Int,4),
					new SqlParameter("@LeaveNumber", SqlDbType.Decimal,9),
					new SqlParameter("@RemainingNumber", SqlDbType.Decimal,9),
					new SqlParameter("@LeaveType", SqlDbType.Int,4),
					new SqlParameter("@ValidTo", SqlDbType.DateTime),
					new SqlParameter("@Deleted", SqlDbType.Bit,1),
					new SqlParameter("@CreateTime", SqlDbType.DateTime),
					new SqlParameter("@UpdateTime", SqlDbType.DateTime),
					new SqlParameter("@OperatorID", SqlDbType.Int,4),
					new SqlParameter("@OperatorDept", SqlDbType.Int,4)};
            parameters[0].Value = model.UserID;
            parameters[1].Value = model.UserCode;
            parameters[2].Value = model.UserName;
            parameters[3].Value = model.EmployeeName;
            parameters[4].Value = model.LeaveYear;
            parameters[5].Value = model.LeaveMonth;
            parameters[6].Value = model.LeaveNumber;
            parameters[7].Value = model.RemainingNumber;
            parameters[8].Value = model.LeaveType;
            parameters[9].Value = model.ValidTo;
            parameters[10].Value = model.Deleted;
            parameters[11].Value = model.CreateTime;
            parameters[12].Value = model.UpdateTime;
            parameters[13].Value = model.OperatorID;
            parameters[14].Value = model.OperatorDept;

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
        /// 增加一条数据，在同一个事务里面
        /// </summary>
        public int Add(ALAndRLInfo model, SqlConnection conn, SqlTransaction trans)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("INSERT INTO AD_ALAndRL(");
            strSql.Append("UserID,UserCode,UserName,EmployeeName,LeaveYear,LeaveMonth,LeaveNumber,RemainingNumber,LeaveType,ValidTo,Deleted,CreateTime,UpdateTime,OperatorID,OperatorDept)");
            strSql.Append(" VALUES (");
            strSql.Append("@UserID,@UserCode,@UserName,@EmployeeName,@LeaveYear,@LeaveMonth,@LeaveNumber,@RemainingNumber,@LeaveType,@ValidTo,@Deleted,@CreateTime,@UpdateTime,@OperatorID,@OperatorDept)");
            strSql.Append(";SELECT @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@UserID", SqlDbType.Int,4),
					new SqlParameter("@UserCode", SqlDbType.NVarChar),
					new SqlParameter("@UserName", SqlDbType.NVarChar),
					new SqlParameter("@EmployeeName", SqlDbType.NVarChar),
					new SqlParameter("@LeaveYear", SqlDbType.Int,4),
					new SqlParameter("@LeaveMonth", SqlDbType.Int,4),
					new SqlParameter("@LeaveNumber", SqlDbType.Decimal,9),
					new SqlParameter("@RemainingNumber", SqlDbType.Decimal,9),
					new SqlParameter("@LeaveType", SqlDbType.Int,4),
					new SqlParameter("@ValidTo", SqlDbType.DateTime),
					new SqlParameter("@Deleted", SqlDbType.Bit,1),
					new SqlParameter("@CreateTime", SqlDbType.DateTime),
					new SqlParameter("@UpdateTime", SqlDbType.DateTime),
					new SqlParameter("@OperatorID", SqlDbType.Int,4),
					new SqlParameter("@OperatorDept", SqlDbType.Int,4)};
            parameters[0].Value = model.UserID;
            parameters[1].Value = model.UserCode;
            parameters[2].Value = model.UserName;
            parameters[3].Value = model.EmployeeName;
            parameters[4].Value = model.LeaveYear;
            parameters[5].Value = model.LeaveMonth;
            parameters[6].Value = model.LeaveNumber;
            parameters[7].Value = model.RemainingNumber;
            parameters[8].Value = model.LeaveType;
            parameters[9].Value = model.ValidTo;
            parameters[10].Value = model.Deleted;
            parameters[11].Value = model.CreateTime;
            parameters[12].Value = model.UpdateTime;
            parameters[13].Value = model.OperatorID;
            parameters[14].Value = model.OperatorDept;

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
        public void Update(ALAndRLInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("UPDATE AD_ALAndRL SET ");
            strSql.Append("UserID=@UserID,");
            strSql.Append("UserCode=@UserCode,");
            strSql.Append("UserName=@UserName,");
            strSql.Append("EmployeeName=@EmployeeName,");
            strSql.Append("LeaveYear=@LeaveYear,");
            strSql.Append("LeaveMonth=@LeaveMonth,");
            strSql.Append("LeaveNumber=@LeaveNumber,");
            strSql.Append("RemainingNumber=@RemainingNumber,");
            strSql.Append("LeaveType=@LeaveType,");
            strSql.Append("ValidTo=@ValidTo,");
            strSql.Append("Deleted=@Deleted,");
            strSql.Append("CreateTime=@CreateTime,");
            strSql.Append("UpdateTime=@UpdateTime,");
            strSql.Append("OperatorID=@OperatorID,");
            strSql.Append("OperatorDept=@OperatorDept");
            strSql.Append(" WHERE ID=@ID");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4),
					new SqlParameter("@UserID", SqlDbType.Int,4),
					new SqlParameter("@UserCode", SqlDbType.NVarChar),
					new SqlParameter("@UserName", SqlDbType.NVarChar),
					new SqlParameter("@EmployeeName", SqlDbType.NVarChar),
					new SqlParameter("@LeaveYear", SqlDbType.Int,4),
					new SqlParameter("@LeaveMonth", SqlDbType.Int,4),
					new SqlParameter("@LeaveNumber", SqlDbType.Decimal,9),
					new SqlParameter("@RemainingNumber", SqlDbType.Decimal,9),
					new SqlParameter("@LeaveType", SqlDbType.Int,4),
					new SqlParameter("@ValidTo", SqlDbType.DateTime),
					new SqlParameter("@Deleted", SqlDbType.Bit,1),
					new SqlParameter("@CreateTime", SqlDbType.DateTime),
					new SqlParameter("@UpdateTime", SqlDbType.DateTime),
					new SqlParameter("@OperatorID", SqlDbType.Int,4),
					new SqlParameter("@OperatorDept", SqlDbType.Int,4)};
            parameters[0].Value = model.ID;
            parameters[1].Value = model.UserID;
            parameters[2].Value = model.UserCode;
            parameters[3].Value = model.UserName;
            parameters[4].Value = model.EmployeeName;
            parameters[5].Value = model.LeaveYear;
            parameters[6].Value = model.LeaveMonth;
            parameters[7].Value = model.LeaveNumber;
            parameters[8].Value = model.RemainingNumber;
            parameters[9].Value = model.LeaveType;
            parameters[10].Value = model.ValidTo;
            parameters[11].Value = model.Deleted;
            parameters[12].Value = model.CreateTime;
            parameters[13].Value = model.UpdateTime;
            parameters[14].Value = model.OperatorID;
            parameters[15].Value = model.OperatorDept;

            DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }

        public void Update(ALAndRLInfo model, SqlTransaction trans)
        {
            Update(model, null, trans);
        }

        /// <summary>
        /// 更新一条数据，带事务的修改操作
        /// </summary>
        public void Update(ALAndRLInfo model, SqlConnection conn, SqlTransaction trans)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("UPDATE AD_ALAndRL SET ");
            strSql.Append("UserID=@UserID,");
            strSql.Append("UserCode=@UserCode,");
            strSql.Append("UserName=@UserName,");
            strSql.Append("EmployeeName=@EmployeeName,");
            strSql.Append("LeaveYear=@LeaveYear,");
            strSql.Append("LeaveMonth=@LeaveMonth,");
            strSql.Append("LeaveNumber=@LeaveNumber,");
            strSql.Append("RemainingNumber=@RemainingNumber,");
            strSql.Append("LeaveType=@LeaveType,");
            strSql.Append("ValidTo=@ValidTo,");
            strSql.Append("Deleted=@Deleted,");
            strSql.Append("CreateTime=@CreateTime,");
            strSql.Append("UpdateTime=@UpdateTime,");
            strSql.Append("OperatorID=@OperatorID,");
            strSql.Append("OperatorDept=@OperatorDept");
            strSql.Append(" WHERE ID=@ID");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4),
					new SqlParameter("@UserID", SqlDbType.Int,4),
					new SqlParameter("@UserCode", SqlDbType.NVarChar),
					new SqlParameter("@UserName", SqlDbType.NVarChar),
					new SqlParameter("@EmployeeName", SqlDbType.NVarChar),
					new SqlParameter("@LeaveYear", SqlDbType.Int,4),
					new SqlParameter("@LeaveMonth", SqlDbType.Int,4),
					new SqlParameter("@LeaveNumber", SqlDbType.Decimal,9),
					new SqlParameter("@RemainingNumber", SqlDbType.Decimal,9),
					new SqlParameter("@LeaveType", SqlDbType.Int,4),
					new SqlParameter("@ValidTo", SqlDbType.DateTime),
					new SqlParameter("@Deleted", SqlDbType.Bit,1),
					new SqlParameter("@CreateTime", SqlDbType.DateTime),
					new SqlParameter("@UpdateTime", SqlDbType.DateTime),
					new SqlParameter("@OperatorID", SqlDbType.Int,4),
					new SqlParameter("@OperatorDept", SqlDbType.Int,4)};
            parameters[0].Value = model.ID;
            parameters[1].Value = model.UserID;
            parameters[2].Value = model.UserCode;
            parameters[3].Value = model.UserName;
            parameters[4].Value = model.EmployeeName;
            parameters[5].Value = model.LeaveYear;
            parameters[6].Value = model.LeaveMonth;
            parameters[7].Value = model.LeaveNumber;
            parameters[8].Value = model.RemainingNumber;
            parameters[9].Value = model.LeaveType;
            parameters[10].Value = model.ValidTo;
            parameters[11].Value = model.Deleted;
            parameters[12].Value = model.CreateTime;
            parameters[13].Value = model.UpdateTime;
            parameters[14].Value = model.OperatorID;
            parameters[15].Value = model.OperatorDept;

            if(trans != null)
                DbHelperSQL.ExecuteSql(strSql.ToString(), trans.Connection, trans, parameters);
            else
                DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public void Delete(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("Update AD_ALAndRL SET Deleted=1  ");
            strSql.Append(" WHERE ID=@ID");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)
				};
            parameters[0].Value = ID;
            DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public ALAndRLInfo GetModel(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM AD_ALAndRL ");
            strSql.Append(" WHERE ID=@ID");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)};
            parameters[0].Value = ID;
            ALAndRLInfo model = new ALAndRLInfo();
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
            strSql.Append("SELECT * FROM AD_ALAndRL ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" WHERE " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }

        public List<ALAndRLInfo> GetModelList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM AD_ALAndRL ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" WHERE " + strWhere);
            }

            return CBO.FillCollection<ALAndRLInfo>(DbHelperSQL.Query(strSql.ToString()));
        }

        /// <summary>
        /// 获得年假统计信息
        /// </summary>
        /// <param name="userID">员工编号</param>
        /// <param name="strWhere">查询条件</param>
        /// <returns>返回年假统计信息</returns>
        public DataSet GetAnnualLeaveInfo(int userID, string strWhere, List<SqlParameter> parameterList)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT a.*, p.*,d.level1id,d.level2id,d.level3id,d.level1+'-'+d.level2+'-'+d.level3 as DepartmentName,em.InternalEmail FROM AD_ALAndRL a ");
            strSql.Append(" LEFT JOIN SEP_Employees em ON a.userid = em.userid ");
            strSql.Append(" LEFT JOIN ( ");
            strSql.Append("     SELECT * FROM ( ");
            strSql.Append("         SELECT p.*,ROW_NUMBER() OVER(PARTITION BY p.userid ORDER BY p.userid) AS r ");
            strSql.Append("         FROM SEP_EmployeesInPositions p) P  WHERE r=1) e ON a.userid = e.userid ");
            strSql.Append(" LEFT JOIN SEP_DepartmentPositions p ON p.DepartmentPositionid=e.Departmentpositionid ");
            strSql.Append(" LEFT JOIN V_Department d ON e.Departmentid = d.level3Id ");
            strSql.Append(" WHERE a.Deleted=0 AND a.LeaveType=@LeaveType AND em.status NOT IN (5,6,-1,0) ");
            parameterList.Add(new SqlParameter("@LeaveType", AAndRLeaveType.AnnualType));
            if (!string.IsNullOrEmpty(strWhere))
            {
                strSql.Append(strWhere);
            }
            strSql.Append(" ORDER BY a.UserCode asc");
            return DbHelperSQL.Query(strSql.ToString(), parameterList.ToArray());
        }

        /// <summary>
        /// 获得奖励假期信息
        /// </summary>
        /// <param name="userID">用户编号</param>
        /// <param name="strWhere">查询条件</param>
        /// <param name="parameterList">查询参数</param>
        /// <returns>返回奖励假期信息集合</returns>
        public DataSet GetRewardLeaveInfo(int userID, string strWhere, List<SqlParameter> parameterList)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT a.*, p.*,d.level1id,d.level2id,d.level3id,d.level1+'-'+d.level2+'-'+d.level3 as DepartmentName FROM AD_ALAndRL a ");
            strSql.Append(" LEFT JOIN SEP_Employees em ON a.userid = em.userid ");
            strSql.Append(" LEFT JOIN ( ");
            strSql.Append("     SELECT * FROM ( ");
            strSql.Append("         SELECT p.*,ROW_NUMBER() OVER(PARTITION BY p.userid ORDER BY p.userid) AS r ");
            strSql.Append("         FROM SEP_EmployeesInPositions p) P  WHERE r=1) e ON a.userid = e.userid ");
            strSql.Append(" LEFT JOIN SEP_DepartmentPositions p ON p.DepartmentPositionid=e.Departmentpositionid ");
            strSql.Append(" LEFT JOIN V_Department d ON e.Departmentid = d.level3Id ");
            strSql.Append(" WHERE a.Deleted=0 AND a.LeaveType=@LeaveType AND em.status NOT IN (5,6,-1,0) ");
            parameterList.Add(new SqlParameter("@LeaveType", AAndRLeaveType.RewardType));
            if (!string.IsNullOrEmpty(strWhere))
            {
                strSql.Append(strWhere);
            }
            strSql.Append(" ORDER BY a.UserCode asc");
            return DbHelperSQL.Query(strSql.ToString(), parameterList.ToArray());
        }

        /// <summary>
        /// 获得用户年度的年假总数
        /// </summary>
        /// <param name="userId">用户编号</param>
        /// <param name="year">年份</param>
        /// <returns>返回用户年假信息</returns>
        public ALAndRLInfo GetALAndRLModel(int userId, int year, int leaveType,SqlTransaction trans)
        {
            //DateTime validto = new DateTime(year, Status.AnnualLeaveLastMonth, Status.AnnualLeaveLastDay);
            DateTime validto = DateTime.Now;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM AD_ALAndRL");
            strSql.Append(" WHERE Deleted=0 AND UserId=@UserID AND (LeaveYear=@LeaveYear and ValidTo>=@ValidTo) AND LeaveType=@LeaveType ");
            SqlParameter[] parameters = {
					new SqlParameter("@UserID", SqlDbType.Int, 4),
                    new SqlParameter("@LeaveYear", SqlDbType.Int, 4),
                    new SqlParameter("@ValidTo", SqlDbType.DateTime),
                    new SqlParameter("@LeaveType", SqlDbType.Int, 4)
				};
            parameters[0].Value = userId;
            parameters[1].Value = year;
            parameters[2].Value = validto;
            parameters[3].Value = leaveType;
            DataSet ds = new DataSet();
            if(trans == null)
                ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            else
                ds = DbHelperSQL.Query(strSql.ToString(),trans, parameters);
            ALAndRLInfo alandrlModel = new ALAndRLInfo();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                alandrlModel.PopupData(ds.Tables[0].Rows[0]);
                return alandrlModel;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 获得奖励假信息
        /// </summary>
        /// <param name="userId">用户编号</param>
        /// <param name="validto">申请奖励假的结束日期</param>
        /// <param name="leaveType">奖励假类型</param>
        /// <returns>返回一个奖励假信息</returns>
        public ALAndRLInfo GetALAndRLModel(int userId, DateTime validto, int leaveType)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM AD_ALAndRL");
            strSql.Append(" WHERE Deleted=0 AND UserId=@UserID AND ValidTo>=@ValidTo AND LeaveType=@LeaveType ");
            SqlParameter[] parameters = {
					new SqlParameter("@UserID", SqlDbType.Int, 4),
                    new SqlParameter("@ValidTo", SqlDbType.DateTime),
                    new SqlParameter("@LeaveType", SqlDbType.Int, 4)
				};
            parameters[0].Value = userId;
            parameters[1].Value = validto.Date;
            parameters[2].Value = leaveType;
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            ALAndRLInfo alandrlModel = new ALAndRLInfo();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                alandrlModel.PopupData(ds.Tables[0].Rows[0]);
                return alandrlModel;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 获得用户剩余的所有的年假或奖励假信息集合
        /// </summary>
        /// <param name="userId">用户编号</param>
        /// <param name="year">年份</param>
        /// <param name="leaveType">假期类型：年假\奖励假</param>
        /// <returns>返回用户所查询的假期信息</returns>
        public List<ALAndRLInfo> GetAlandRlList(int userId, int year, int leaveType)
        {
            DateTime validto = DateTime.Now;
            // new DateTime(year, Status.AnnualLeaveLastMonth, Status.AnnualLeaveLastDay);
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM AD_ALAndRL");
            strSql.Append(" WHERE Deleted=0 AND UserId=@UserID AND (LeaveYear=@LeaveYear OR ValidTo>=@ValidTo) AND LeaveType=@LeaveType ");
            SqlParameter[] parameters = {
					new SqlParameter("@UserID", SqlDbType.Int, 4),
                    new SqlParameter("@LeaveYear", SqlDbType.Int, 4),
                    new SqlParameter("@ValidTo", SqlDbType.DateTime),
                    new SqlParameter("@LeaveType", SqlDbType.Int, 4)
				};
            parameters[0].Value = userId;
            parameters[1].Value = year;
            parameters[2].Value = validto;
            parameters[3].Value = leaveType;
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            List<ALAndRLInfo> alandrlList = new List<ALAndRLInfo>();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ALAndRLInfo alandrlModel = new ALAndRLInfo();
                    alandrlModel.PopupData(ds.Tables[0].Rows[0]);
                    alandrlList.Add(alandrlModel);
                }
                return alandrlList;
            }
            else
            {
                return null;
            }
        }
        #endregion  成员方法
    }
}