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
    public class WaitingTaskDataProvider
    {
        public WaitingTaskDataProvider()
        { }
        #region  成员方法
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from AD_WaitingTask");
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
        public int Add(WaitingTaskInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into AD_WaitingTask(");
            strSql.Append("TaskType,UserCode,CardNo,AreaID,Deleted,CreateTime,UpdateTime,OperatorID,OperatorDept,Sort,UserName)");
            strSql.Append(" values (");
            strSql.Append("@TaskType,@UserCode,@CardNo,@AreaID,@Deleted,@CreateTime,@UpdateTime,@OperatorID,@OperatorDept,@Sort,@UserName)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@TaskType", SqlDbType.Int,4),
					new SqlParameter("@UserCode", SqlDbType.NVarChar),
					new SqlParameter("@CardNo", SqlDbType.BigInt,8),
					new SqlParameter("@AreaID", SqlDbType.Int,4),
					new SqlParameter("@Deleted", SqlDbType.Bit,1),
					new SqlParameter("@CreateTime", SqlDbType.DateTime),
					new SqlParameter("@UpdateTime", SqlDbType.DateTime),
					new SqlParameter("@OperatorID", SqlDbType.Int,4),
					new SqlParameter("@OperatorDept", SqlDbType.Int,4),
					new SqlParameter("@Sort", SqlDbType.Int,4), 
                    new SqlParameter("@UserName", SqlDbType.NVarChar)};
            parameters[0].Value = model.TaskType;
            parameters[1].Value = model.UserCode;
            parameters[2].Value = model.CardNo;
            parameters[3].Value = model.AreaID;
            parameters[4].Value = model.Deleted;
            parameters[5].Value = model.CreateTime;
            parameters[6].Value = model.UpdateTime;
            parameters[7].Value = model.OperatorID;
            parameters[8].Value = model.OperatorDept;
            parameters[9].Value = model.Sort;
            parameters[10].Value = model.UserName;

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
        public int Add(WaitingTaskInfo model, SqlConnection conn, SqlTransaction trans)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into AD_WaitingTask(");
            strSql.Append("TaskType,UserCode,CardNo,AreaID,Deleted,CreateTime,UpdateTime,OperatorID,OperatorDept,Sort,UserName)");
            strSql.Append(" values (");
            strSql.Append("@TaskType,@UserCode,@CardNo,@AreaID,@Deleted,@CreateTime,@UpdateTime,@OperatorID,@OperatorDept,@Sort,@UserName)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@TaskType", SqlDbType.Int,4),
					new SqlParameter("@UserCode", SqlDbType.NVarChar),
					new SqlParameter("@CardNo", SqlDbType.BigInt,8),
					new SqlParameter("@AreaID", SqlDbType.Int,4),
					new SqlParameter("@Deleted", SqlDbType.Bit,1),
					new SqlParameter("@CreateTime", SqlDbType.DateTime),
					new SqlParameter("@UpdateTime", SqlDbType.DateTime),
					new SqlParameter("@OperatorID", SqlDbType.Int,4),
					new SqlParameter("@OperatorDept", SqlDbType.Int,4),
					new SqlParameter("@Sort", SqlDbType.Int,4), 
                    new SqlParameter("@UserName", SqlDbType.NVarChar)};
            parameters[0].Value = model.TaskType;
            parameters[1].Value = model.UserCode;
            parameters[2].Value = model.CardNo;
            parameters[3].Value = model.AreaID;
            parameters[4].Value = model.Deleted;
            parameters[5].Value = model.CreateTime;
            parameters[6].Value = model.UpdateTime;
            parameters[7].Value = model.OperatorID;
            parameters[8].Value = model.OperatorDept;
            parameters[9].Value = model.Sort;
            parameters[10].Value = model.UserName;

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
        public void Update(WaitingTaskInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update AD_WaitingTask set ");
            strSql.Append("TaskType=@TaskType,");
            strSql.Append("UserCode=@UserCode,");
            strSql.Append("CardNo=@CardNo,");
            strSql.Append("AreaID=@AreaID,");
            strSql.Append("Deleted=@Deleted,");
            strSql.Append("CreateTime=@CreateTime,");
            strSql.Append("UpdateTime=@UpdateTime,");
            strSql.Append("OperatorID=@OperatorID,");
            strSql.Append("OperatorDept=@OperatorDept,");
            strSql.Append("Sort=@Sort,");
            strSql.Append("UserName=@UserName");
            strSql.Append(" where ID=@ID");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4),
					new SqlParameter("@TaskType", SqlDbType.Int,4),
					new SqlParameter("@UserCode", SqlDbType.NVarChar),
					new SqlParameter("@CardNo", SqlDbType.BigInt,8),
					new SqlParameter("@AreaID", SqlDbType.Int,4),
					new SqlParameter("@Deleted", SqlDbType.Bit,1),
					new SqlParameter("@CreateTime", SqlDbType.DateTime),
					new SqlParameter("@UpdateTime", SqlDbType.DateTime),
					new SqlParameter("@OperatorID", SqlDbType.Int,4),
					new SqlParameter("@OperatorDept", SqlDbType.Int,4),
					new SqlParameter("@Sort", SqlDbType.Int,4),
                    new SqlParameter("@UserName", SqlDbType.NVarChar)};
            parameters[0].Value = model.ID;
            parameters[1].Value = model.TaskType;
            parameters[2].Value = model.UserCode;
            parameters[3].Value = model.CardNo;
            parameters[4].Value = model.AreaID;
            parameters[5].Value = model.Deleted;
            parameters[6].Value = model.CreateTime;
            parameters[7].Value = model.UpdateTime;
            parameters[8].Value = model.OperatorID;
            parameters[9].Value = model.OperatorDept;
            parameters[10].Value = model.Sort;
            parameters[11].Value = model.UserName;
            
            DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public void Update(WaitingTaskInfo model, SqlConnection conn, SqlTransaction trans)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update AD_WaitingTask set ");
            strSql.Append("TaskType=@TaskType,");
            strSql.Append("UserCode=@UserCode,");
            strSql.Append("CardNo=@CardNo,");
            strSql.Append("AreaID=@AreaID,");
            strSql.Append("Deleted=@Deleted,");
            strSql.Append("CreateTime=@CreateTime,");
            strSql.Append("UpdateTime=@UpdateTime,");
            strSql.Append("OperatorID=@OperatorID,");
            strSql.Append("OperatorDept=@OperatorDept,");
            strSql.Append("Sort=@Sort,");
            strSql.Append("UserName=@UserName");
            strSql.Append(" where ID=@ID");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4),
					new SqlParameter("@TaskType", SqlDbType.Int,4),
					new SqlParameter("@UserCode", SqlDbType.NVarChar),
					new SqlParameter("@CardNo", SqlDbType.BigInt,8),
					new SqlParameter("@AreaID", SqlDbType.Int,4),
					new SqlParameter("@Deleted", SqlDbType.Bit,1),
					new SqlParameter("@CreateTime", SqlDbType.DateTime),
					new SqlParameter("@UpdateTime", SqlDbType.DateTime),
					new SqlParameter("@OperatorID", SqlDbType.Int,4),
					new SqlParameter("@OperatorDept", SqlDbType.Int,4),
					new SqlParameter("@Sort", SqlDbType.Int,4),
                    new SqlParameter("@UserName", SqlDbType.NVarChar)};
            parameters[0].Value = model.ID;
            parameters[1].Value = model.TaskType;
            parameters[2].Value = model.UserCode;
            parameters[3].Value = model.CardNo;
            parameters[4].Value = model.AreaID;
            parameters[5].Value = model.Deleted;
            parameters[6].Value = model.CreateTime;
            parameters[7].Value = model.UpdateTime;
            parameters[8].Value = model.OperatorID;
            parameters[9].Value = model.OperatorDept;
            parameters[10].Value = model.Sort;
            parameters[11].Value = model.UserName;

            DbHelperSQL.ExecuteSql(strSql.ToString(), conn, trans, parameters);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public void Delete(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete AD_WaitingTask ");
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
        public WaitingTaskInfo GetModel(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from AD_WaitingTask ");
            strSql.Append(" where ID=@ID");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)};
            parameters[0].Value = ID;
            WaitingTaskInfo model = new WaitingTaskInfo();
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
            strSql.Append(" FROM AD_WaitingTask where Deleted=0 ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" and " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }
        #endregion  成员方法
    }
}