using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using ESP.Administrative.Common;

namespace ESP.Administrative.DataAccess
{
    public class LeaveDataProvider
    {
        public LeaveDataProvider()
        { }
        #region  成员方法

        /// <summary>
        /// 得到最大ID
        /// </summary>
        public int GetMaxId()
        {
            return DbHelperSQL.GetMaxID("ID", "AD_Leave");
        }

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from AD_Leave");
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
        public int Add(ESP.Administrative.Entity.LeaveInfo model, SqlConnection conn, SqlTransaction trans)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into AD_Leave(");
            strSql.Append("UserID,UserCode,EmployeeName,LeaveType,LeaveTypeName,LeaveCause,BeginTime,EndTime,LeaveState,ApproveID,ApproveRemark,AuditingID,AuditingRemark,Deleted,CreateTime,UpdateTime,OperateorID,OperateorDept,Sort,Leavehours)");
            strSql.Append(" values (");
            strSql.Append("@UserID,@UserCode,@EmployeeName,@LeaveType,@LeaveTypeName,@LeaveCause,@BeginTime,@EndTime,@LeaveState,@ApproveID,@ApproveRemark,@AuditingID,@AuditingRemark,@Deleted,@CreateTime,@UpdateTime,@OperateorID,@OperateorDept,@Sort,@Leavehours)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@UserID", SqlDbType.Int,4),
					new SqlParameter("@UserCode", SqlDbType.NVarChar),
					new SqlParameter("@EmployeeName", SqlDbType.NVarChar),
					new SqlParameter("@LeaveType", SqlDbType.Int,4),
					new SqlParameter("@LeaveTypeName", SqlDbType.NVarChar),
					new SqlParameter("@LeaveCause", SqlDbType.NVarChar),
					new SqlParameter("@BeginTime", SqlDbType.DateTime),
					new SqlParameter("@EndTime", SqlDbType.DateTime),
					new SqlParameter("@LeaveState", SqlDbType.Int,4),
					new SqlParameter("@ApproveID", SqlDbType.Int,4),
					new SqlParameter("@ApproveRemark", SqlDbType.NVarChar),
					new SqlParameter("@AuditingID", SqlDbType.Int,4),
					new SqlParameter("@AuditingRemark", SqlDbType.NVarChar),
					new SqlParameter("@Deleted", SqlDbType.Bit,1),
					new SqlParameter("@CreateTime", SqlDbType.DateTime),
					new SqlParameter("@UpdateTime", SqlDbType.DateTime),
					new SqlParameter("@OperateorID", SqlDbType.Int,4),
					new SqlParameter("@OperateorDept", SqlDbType.Int,4),
					new SqlParameter("@Sort", SqlDbType.Int,4),
                    new SqlParameter("@Leavehours", SqlDbType.Decimal)};
            parameters[0].Value = model.UserID;
            parameters[1].Value = model.UserCode;
            parameters[2].Value = model.EmployeeName;
            parameters[3].Value = model.LeaveType;
            parameters[4].Value = model.LeaveTypeName;
            parameters[5].Value = model.LeaveCause;
            parameters[6].Value = model.BeginTime;
            parameters[7].Value = model.EndTime;
            parameters[8].Value = model.LeaveState;
            parameters[9].Value = model.ApproveID;
            parameters[10].Value = model.ApproveRemark;
            parameters[11].Value = model.AuditingID;
            parameters[12].Value = model.AuditingRemark;
            parameters[13].Value = model.Deleted;
            parameters[14].Value = model.CreateTime;
            parameters[15].Value = model.UpdateTime;
            parameters[16].Value = model.OperateorID;
            parameters[17].Value = model.OperateorDept;
            parameters[18].Value = model.Sort;
            parameters[19].Value = model.Leavehours;

            object obj = DbHelperSQL.GetSingle(strSql.ToString(),conn, trans, parameters);
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
        public int Update(ESP.Administrative.Entity.LeaveInfo model, SqlConnection conn, SqlTransaction trans)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update AD_Leave set ");
            strSql.Append("UserID=@UserID,");
            strSql.Append("UserCode=@UserCode,");
            strSql.Append("EmployeeName=@EmployeeName,");
            strSql.Append("LeaveType=@LeaveType,");
            strSql.Append("LeaveTypeName=@LeaveTypeName,");
            strSql.Append("LeaveCause=@LeaveCause,");
            strSql.Append("BeginTime=@BeginTime,");
            strSql.Append("EndTime=@EndTime,");
            strSql.Append("LeaveState=@LeaveState,");
            strSql.Append("ApproveID=@ApproveID,");
            strSql.Append("ApproveRemark=@ApproveRemark,");
            strSql.Append("AuditingID=@AuditingID,");
            strSql.Append("AuditingRemark=@AuditingRemark,");
            strSql.Append("Deleted=@Deleted,");
            strSql.Append("CreateTime=@CreateTime,");
            strSql.Append("UpdateTime=@UpdateTime,");
            strSql.Append("OperateorID=@OperateorID,");
            strSql.Append("OperateorDept=@OperateorDept,");
            strSql.Append("Sort=@Sort,");
            strSql.Append("Leavehours=@Leavehours");
            strSql.Append(" where ID=@ID");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4),
					new SqlParameter("@UserID", SqlDbType.Int,4),
					new SqlParameter("@UserCode", SqlDbType.NVarChar),
					new SqlParameter("@EmployeeName", SqlDbType.NVarChar),
					new SqlParameter("@LeaveType", SqlDbType.Int,4),
					new SqlParameter("@LeaveTypeName", SqlDbType.NVarChar),
					new SqlParameter("@LeaveCause", SqlDbType.NVarChar),
					new SqlParameter("@BeginTime", SqlDbType.DateTime),
					new SqlParameter("@EndTime", SqlDbType.DateTime),
					new SqlParameter("@LeaveState", SqlDbType.Int,4),
					new SqlParameter("@ApproveID", SqlDbType.Int,4),
					new SqlParameter("@ApproveRemark", SqlDbType.NVarChar),
					new SqlParameter("@AuditingID", SqlDbType.Int,4),
					new SqlParameter("@AuditingRemark", SqlDbType.NVarChar),
					new SqlParameter("@Deleted", SqlDbType.Bit,1),
					new SqlParameter("@CreateTime", SqlDbType.DateTime),
					new SqlParameter("@UpdateTime", SqlDbType.DateTime),
					new SqlParameter("@OperateorID", SqlDbType.Int,4),
					new SqlParameter("@OperateorDept", SqlDbType.Int,4),
					new SqlParameter("@Sort", SqlDbType.Int,4),
                    new SqlParameter("@Leavehours", SqlDbType.Decimal)};
            parameters[0].Value = model.ID;
            parameters[1].Value = model.UserID;
            parameters[2].Value = model.UserCode;
            parameters[3].Value = model.EmployeeName;
            parameters[4].Value = model.LeaveType;
            parameters[5].Value = model.LeaveTypeName;
            parameters[6].Value = model.LeaveCause;
            parameters[7].Value = model.BeginTime;
            parameters[8].Value = model.EndTime;
            parameters[9].Value = model.LeaveState;
            parameters[10].Value = model.ApproveID;
            parameters[11].Value = model.ApproveRemark;
            parameters[12].Value = model.AuditingID;
            parameters[13].Value = model.AuditingRemark;
            parameters[14].Value = model.Deleted;
            parameters[15].Value = model.CreateTime;
            parameters[16].Value = model.UpdateTime;
            parameters[17].Value = model.OperateorID;
            parameters[18].Value = model.OperateorDept;
            parameters[19].Value = model.Sort;
            parameters[20].Value = model.Leavehours;

            return DbHelperSQL.ExecuteSql(strSql.ToString(), conn, trans, parameters);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public void Update(ESP.Administrative.Entity.LeaveInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update AD_Leave set ");
            strSql.Append("UserID=@UserID,");
            strSql.Append("UserCode=@UserCode,");
            strSql.Append("EmployeeName=@EmployeeName,");
            strSql.Append("LeaveType=@LeaveType,");
            strSql.Append("LeaveTypeName=@LeaveTypeName,");
            strSql.Append("LeaveCause=@LeaveCause,");
            strSql.Append("BeginTime=@BeginTime,");
            strSql.Append("EndTime=@EndTime,");
            strSql.Append("LeaveState=@LeaveState,");
            strSql.Append("ApproveID=@ApproveID,");
            strSql.Append("ApproveRemark=@ApproveRemark,");
            strSql.Append("AuditingID=@AuditingID,");
            strSql.Append("AuditingRemark=@AuditingRemark,");
            strSql.Append("Deleted=@Deleted,");
            strSql.Append("CreateTime=@CreateTime,");
            strSql.Append("UpdateTime=@UpdateTime,");
            strSql.Append("OperateorID=@OperateorID,");
            strSql.Append("OperateorDept=@OperateorDept,");
            strSql.Append("Sort=@Sort,");
            strSql.Append("Leavehours=@Leavehours");
            strSql.Append(" where ID=@ID");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4),
					new SqlParameter("@UserID", SqlDbType.Int,4),
					new SqlParameter("@UserCode", SqlDbType.NVarChar),
					new SqlParameter("@EmployeeName", SqlDbType.NVarChar),
					new SqlParameter("@LeaveType", SqlDbType.Int,4),
					new SqlParameter("@LeaveTypeName", SqlDbType.NVarChar),
					new SqlParameter("@LeaveCause", SqlDbType.NVarChar),
					new SqlParameter("@BeginTime", SqlDbType.DateTime),
					new SqlParameter("@EndTime", SqlDbType.DateTime),
					new SqlParameter("@LeaveState", SqlDbType.Int,4),
					new SqlParameter("@ApproveID", SqlDbType.Int,4),
					new SqlParameter("@ApproveRemark", SqlDbType.NVarChar),
					new SqlParameter("@AuditingID", SqlDbType.Int,4),
					new SqlParameter("@AuditingRemark", SqlDbType.NVarChar),
					new SqlParameter("@Deleted", SqlDbType.Bit,1),
					new SqlParameter("@CreateTime", SqlDbType.DateTime),
					new SqlParameter("@UpdateTime", SqlDbType.DateTime),
					new SqlParameter("@OperateorID", SqlDbType.Int,4),
					new SqlParameter("@OperateorDept", SqlDbType.Int,4),
					new SqlParameter("@Sort", SqlDbType.Int,4),
                    new SqlParameter("@Leavehours", SqlDbType.Decimal)};
            parameters[0].Value = model.ID;
            parameters[1].Value = model.UserID;
            parameters[2].Value = model.UserCode;
            parameters[3].Value = model.EmployeeName;
            parameters[4].Value = model.LeaveType;
            parameters[5].Value = model.LeaveTypeName;
            parameters[6].Value = model.LeaveCause;
            parameters[7].Value = model.BeginTime;
            parameters[8].Value = model.EndTime;
            parameters[9].Value = model.LeaveState;
            parameters[10].Value = model.ApproveID;
            parameters[11].Value = model.ApproveRemark;
            parameters[12].Value = model.AuditingID;
            parameters[13].Value = model.AuditingRemark;
            parameters[14].Value = model.Deleted;
            parameters[15].Value = model.CreateTime;
            parameters[16].Value = model.UpdateTime;
            parameters[17].Value = model.OperateorID;
            parameters[18].Value = model.OperateorDept;
            parameters[19].Value = model.Sort;
            parameters[20].Value = model.Leavehours;

            DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public int Delete(int ID, SqlConnection conn, SqlTransaction trans)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete AD_Leave ");
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
        public ESP.Administrative.Entity.LeaveInfo GetModel(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from AD_Leave ");
            strSql.Append(" where ID=@ID");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)};
            parameters[0].Value = ID;
            ESP.Administrative.Entity.LeaveInfo model = new ESP.Administrative.Entity.LeaveInfo();
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
            strSql.Append("select * FROM AD_Leave ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }

        #endregion  成员方法
    }
}
