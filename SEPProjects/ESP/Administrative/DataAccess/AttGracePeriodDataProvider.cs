using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using ESP.Administrative.Common;
using ESP.Administrative.Entity;
using System.Data;

namespace ESP.Administrative.DataAccess
{
    public class AttGracePeriodDataProvider
    {
        public AttGracePeriodDataProvider()
        { }
        #region  成员方法
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from AD_AttendanceGracePeriod");
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
        public int Add(AttGracePeriodInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into AD_AttendanceGracePeriod(");
            strSql.Append("UserID,UserCode,EmployeeName,BeginTime,EndTime,CreateTime,UpdateTime,OperatorID,OperatorName,Remark,Deleted)");
            strSql.Append(" values (");
            strSql.Append("@UserID,@UserCode,@EmployeeName,@BeginTime,@EndTime,@CreateTime,@UpdateTime,@OperatorID,@OperatorName,@Remark,@Deleted)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@UserID", SqlDbType.Int,4),
					new SqlParameter("@UserCode", SqlDbType.NVarChar),
					new SqlParameter("@EmployeeName", SqlDbType.NVarChar),
					new SqlParameter("@BeginTime", SqlDbType.DateTime),
					new SqlParameter("@EndTime", SqlDbType.DateTime),
					new SqlParameter("@CreateTime", SqlDbType.DateTime),
					new SqlParameter("@UpdateTime", SqlDbType.DateTime),
					new SqlParameter("@OperatorID", SqlDbType.Int,4),
					new SqlParameter("@OperatorName", SqlDbType.NVarChar),
					new SqlParameter("@Remark", SqlDbType.NVarChar),
                    new SqlParameter("@Deleted", SqlDbType.Bit)};
            parameters[0].Value = model.UserID;
            parameters[1].Value = model.UserCode;
            parameters[2].Value = model.EmployeeName;
            parameters[3].Value = model.BeginTime;
            parameters[4].Value = model.EndTime;
            parameters[5].Value = model.CreateTime;
            parameters[6].Value = model.UpdateTime;
            parameters[7].Value = model.OperatorID;
            parameters[8].Value = model.OperatorName;
            parameters[9].Value = model.Remark;
            parameters[10].Value = model.Deleted;

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
        public int Add(AttGracePeriodInfo model, SqlConnection conn, SqlTransaction trans)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into AD_AttendanceGracePeriod(");
            strSql.Append("UserID,UserCode,EmployeeName,BeginTime,EndTime,CreateTime,UpdateTime,OperatorID,OperatorName,Remark,Deleted)");
            strSql.Append(" values (");
            strSql.Append("@UserID,@UserCode,@EmployeeName,@BeginTime,@EndTime,@CreateTime,@UpdateTime,@OperatorID,@OperatorName,@Remark,@Deleted)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@UserID", SqlDbType.Int,4),
					new SqlParameter("@UserCode", SqlDbType.NVarChar),
					new SqlParameter("@EmployeeName", SqlDbType.NVarChar),
					new SqlParameter("@BeginTime", SqlDbType.DateTime),
					new SqlParameter("@EndTime", SqlDbType.DateTime),
					new SqlParameter("@CreateTime", SqlDbType.DateTime),
					new SqlParameter("@UpdateTime", SqlDbType.DateTime),
					new SqlParameter("@OperatorID", SqlDbType.Int,4),
					new SqlParameter("@OperatorName", SqlDbType.NVarChar),
					new SqlParameter("@Remark", SqlDbType.NVarChar),
                    new SqlParameter("@Deleted", SqlDbType.Bit)};
            parameters[0].Value = model.UserID;
            parameters[1].Value = model.UserCode;
            parameters[2].Value = model.EmployeeName;
            parameters[3].Value = model.BeginTime;
            parameters[4].Value = model.EndTime;
            parameters[5].Value = model.CreateTime;
            parameters[6].Value = model.UpdateTime;
            parameters[7].Value = model.OperatorID;
            parameters[8].Value = model.OperatorName;
            parameters[9].Value = model.Remark;
            parameters[10].Value = model.Deleted;

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
        public void Update(AttGracePeriodInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update AD_AttendanceGracePeriod set ");
            strSql.Append("UserID=@UserID,");
            strSql.Append("UserCode=@UserCode,");
            strSql.Append("EmployeeName=@EmployeeName,");
            strSql.Append("BeginTime=@BeginTime,");
            strSql.Append("EndTime=@EndTime,");
            strSql.Append("CreateTime=@CreateTime,");
            strSql.Append("UpdateTime=@UpdateTime,");
            strSql.Append("OperatorID=@OperatorID,");
            strSql.Append("OperatorName=@OperatorName,");
            strSql.Append("Remark=@Remark,"); 
            strSql.Append("Deleted=@Deleted ");
            strSql.Append(" where ID=@ID");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4),
					new SqlParameter("@UserID", SqlDbType.Int,4),
					new SqlParameter("@UserCode", SqlDbType.NVarChar),
					new SqlParameter("@EmployeeName", SqlDbType.NVarChar),
					new SqlParameter("@BeginTime", SqlDbType.DateTime),
					new SqlParameter("@EndTime", SqlDbType.DateTime),
					new SqlParameter("@CreateTime", SqlDbType.DateTime),
					new SqlParameter("@UpdateTime", SqlDbType.DateTime),
					new SqlParameter("@OperatorID", SqlDbType.Int,4),
					new SqlParameter("@OperatorName", SqlDbType.NVarChar),
					new SqlParameter("@Remark", SqlDbType.NVarChar),
                    new SqlParameter("@Deleted", SqlDbType.Bit)};
            parameters[0].Value = model.ID;
            parameters[1].Value = model.UserID;
            parameters[2].Value = model.UserCode;
            parameters[3].Value = model.EmployeeName;
            parameters[4].Value = model.BeginTime;
            parameters[5].Value = model.EndTime;
            parameters[6].Value = model.CreateTime;
            parameters[7].Value = model.UpdateTime;
            parameters[8].Value = model.OperatorID;
            parameters[9].Value = model.OperatorName;
            parameters[10].Value = model.Remark;
            parameters[11].Value = model.Deleted;

            DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public void Delete(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete AD_AttendanceGracePeriod ");
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
        public AttGracePeriodInfo GetModel(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM AD_AttendanceGracePeriod ");
            strSql.Append(" WHERE ID=@ID");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)};
            parameters[0].Value = ID;
            AttGracePeriodInfo model = new AttGracePeriodInfo();
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
            strSql.Append("SELECT * ");
            strSql.Append(" FROM AD_AttendanceGracePeriod ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public void DeleteByUserId(int userId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("UPDATE AD_AttendanceGracePeriod SET Deleted=1 ");
            strSql.Append(" WHERE UserID=@UserID AND Deleted=0 ");
            SqlParameter[] parameters = {
					new SqlParameter("@UserID", SqlDbType.Int,4)
				};
            parameters[0].Value = userId;
            DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }
        #endregion  成员方法
    }
}