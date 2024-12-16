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
    public class RefundDataProvider
    {
        public RefundDataProvider()
        { }
        #region  成员方法
        
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(RefundInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into AD_Refund(");
            strSql.Append("UserId,BeginTime,EndTime,BeginOperator,EndOperator,Cost,Type,Status,Creator,CreatorIP,CreateTime,LastUpdater,LastUpdaterIP,LastUpdateTime,IsDeleted,Remark,ProductName,ProductNo)");
            strSql.Append(" values (");
            strSql.Append("@UserId,@BeginTime,@EndTime,@BeginOperator,@EndOperator,@Cost,@Type,@Status,@Creator,@CreatorIP,@CreateTime,@LastUpdater,@LastUpdaterIP,@LastUpdateTime,@IsDeleted,@Remark,@ProductName,@ProductNo)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@UserId", SqlDbType.Int,4),
					new SqlParameter("@BeginTime", SqlDbType.SmallDateTime),
					new SqlParameter("@EndTime", SqlDbType.SmallDateTime),
					new SqlParameter("@BeginOperator", SqlDbType.NVarChar,50),
					new SqlParameter("@EndOperator", SqlDbType.NVarChar,50),
					new SqlParameter("@Cost", SqlDbType.Decimal,9),
					new SqlParameter("@Type", SqlDbType.Int,4),
					new SqlParameter("@Status", SqlDbType.Int,4),
					new SqlParameter("@Creator", SqlDbType.NVarChar,50),
					new SqlParameter("@CreatorIP", SqlDbType.NVarChar,100),
					new SqlParameter("@CreateTime", SqlDbType.SmallDateTime),
					new SqlParameter("@LastUpdater", SqlDbType.NVarChar,50),
					new SqlParameter("@LastUpdaterIP", SqlDbType.NVarChar,100),
					new SqlParameter("@LastUpdateTime", SqlDbType.SmallDateTime),
					new SqlParameter("@IsDeleted", SqlDbType.Bit,1),
                    new SqlParameter("@Remark", SqlDbType.NVarChar, 500),
                    new SqlParameter("@ProductName", SqlDbType.NVarChar, 500),
                    new SqlParameter("@ProductNo", SqlDbType.NVarChar, 50)
                                        };
            parameters[0].Value = model.UserId;
            parameters[1].Value = model.BeginTime;
            parameters[2].Value = model.EndTime;
            parameters[3].Value = model.BeginOperator;
            parameters[4].Value = model.EndOperator;
            parameters[5].Value = model.Cost;
            parameters[6].Value = model.Type;
            parameters[7].Value = model.Status;
            parameters[8].Value = model.Creator;
            parameters[9].Value = model.CreatorIP;
            parameters[10].Value = model.CreateTime;
            parameters[11].Value = model.LastUpdater;
            parameters[12].Value = model.LastUpdaterIP;
            parameters[13].Value = model.LastUpdateTime;
            parameters[14].Value = model.IsDeleted;
            parameters[15].Value = model.Remark;
            parameters[16].Value = model.ProductName;
            parameters[17].Value = model.ProductNo;

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
        public int Update(RefundInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update AD_Refund set ");
            strSql.Append("UserId=@UserId,");
            strSql.Append("BeginTime=@BeginTime,");
            strSql.Append("EndTime=@EndTime,");
            strSql.Append("BeginOperator=@BeginOperator,");
            strSql.Append("EndOperator=@EndOperator,");
            strSql.Append("Cost=@Cost,");
            strSql.Append("Type=@Type,");
            strSql.Append("Status=@Status,");
            strSql.Append("Creator=@Creator,");
            strSql.Append("CreatorIP=@CreatorIP,");
            strSql.Append("CreateTime=@CreateTime,");
            strSql.Append("LastUpdater=@LastUpdater,");
            strSql.Append("LastUpdaterIP=@LastUpdaterIP,");
            strSql.Append("LastUpdateTime=@LastUpdateTime,");
            strSql.Append("IsDeleted=@IsDeleted,");
            strSql.Append("Remark=@Remark,ProductName=@ProductName,ProductNo=@ProductNo ");
            strSql.Append(" where Id=@Id ");
            SqlParameter[] parameters = {
					new SqlParameter("@Id", SqlDbType.Int,4),
					new SqlParameter("@UserId", SqlDbType.Int,4),
					new SqlParameter("@BeginTime", SqlDbType.SmallDateTime),
					new SqlParameter("@EndTime", SqlDbType.SmallDateTime),
					new SqlParameter("@BeginOperator", SqlDbType.NVarChar,50),
					new SqlParameter("@EndOperator", SqlDbType.NVarChar,50),
					new SqlParameter("@Cost", SqlDbType.Decimal,9),
					new SqlParameter("@Type", SqlDbType.Int,4),
					new SqlParameter("@Status", SqlDbType.Int,4),
					new SqlParameter("@Creator", SqlDbType.NVarChar,50),
					new SqlParameter("@CreatorIP", SqlDbType.NVarChar,100),
					new SqlParameter("@CreateTime", SqlDbType.SmallDateTime),
					new SqlParameter("@LastUpdater", SqlDbType.NVarChar,50),
					new SqlParameter("@LastUpdaterIP", SqlDbType.NVarChar,100),
					new SqlParameter("@LastUpdateTime", SqlDbType.SmallDateTime),
					new SqlParameter("@IsDeleted", SqlDbType.Bit,1),
                    new SqlParameter("@Remark", SqlDbType.NVarChar, 500),
                    new SqlParameter("@ProductName", SqlDbType.NVarChar, 500),
                    new SqlParameter("@ProductNo", SqlDbType.NVarChar, 50)
                                        };
            parameters[0].Value = model.Id;
            parameters[1].Value = model.UserId;
            parameters[2].Value = model.BeginTime;
            parameters[3].Value = model.EndTime;
            parameters[4].Value = model.BeginOperator;
            parameters[5].Value = model.EndOperator;
            parameters[6].Value = model.Cost;
            parameters[7].Value = model.Type;
            parameters[8].Value = model.Status;
            parameters[9].Value = model.Creator;
            parameters[10].Value = model.CreatorIP;
            parameters[11].Value = model.CreateTime;
            parameters[12].Value = model.LastUpdater;
            parameters[13].Value = model.LastUpdaterIP;
            parameters[14].Value = model.LastUpdateTime;
            parameters[15].Value = model.IsDeleted;
            parameters[16].Value = model.Remark;
            parameters[17].Value = model.ProductName;
            parameters[18].Value = model.ProductNo;

            return DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public void Delete(int Id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("UPDATE AD_Refund SET isDeleted=1 ");
            strSql.Append(" where Id=@Id ");
            SqlParameter[] parameters = {
					new SqlParameter("@Id", SqlDbType.Int,4)};
            parameters[0].Value = Id;

            DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 增加一条数据，带事务
        /// </summary>
        public int Add(RefundInfo model, SqlConnection conn, SqlTransaction trans)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into AD_Refund(");
            strSql.Append("UserId,BeginTime,EndTime,BeginOperator,EndOperator,Cost,Type,Status,Creator,CreatorIP,CreateTime,LastUpdater,LastUpdaterIP,LastUpdateTime,IsDeleted,Remark,ProductName,ProductNo)");
            strSql.Append(" values (");
            strSql.Append("@UserId,@BeginTime,@EndTime,@BeginOperator,@EndOperator,@Cost,@Type,@Status,@Creator,@CreatorIP,@CreateTime,@LastUpdater,@LastUpdaterIP,@LastUpdateTime,@IsDeleted,@Remark,@ProductName,@ProductNo)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@UserId", SqlDbType.Int,4),
					new SqlParameter("@BeginTime", SqlDbType.SmallDateTime),
					new SqlParameter("@EndTime", SqlDbType.SmallDateTime),
					new SqlParameter("@BeginOperator", SqlDbType.NVarChar,50),
					new SqlParameter("@EndOperator", SqlDbType.NVarChar,50),
					new SqlParameter("@Cost", SqlDbType.Decimal,9),
					new SqlParameter("@Type", SqlDbType.Int,4),
					new SqlParameter("@Status", SqlDbType.Int,4),
					new SqlParameter("@Creator", SqlDbType.NVarChar,50),
					new SqlParameter("@CreatorIP", SqlDbType.NVarChar,100),
					new SqlParameter("@CreateTime", SqlDbType.SmallDateTime),
					new SqlParameter("@LastUpdater", SqlDbType.NVarChar,50),
					new SqlParameter("@LastUpdaterIP", SqlDbType.NVarChar,100),
					new SqlParameter("@LastUpdateTime", SqlDbType.SmallDateTime),
					new SqlParameter("@IsDeleted", SqlDbType.Bit,1),
                    new SqlParameter("@Remark", SqlDbType.NVarChar, 500),
                    new SqlParameter("@ProductName", SqlDbType.NVarChar, 500),
                    new SqlParameter("@ProductNo", SqlDbType.NVarChar, 50)
                                        
                                        };
            parameters[0].Value = model.UserId;
            parameters[1].Value = model.BeginTime;
            parameters[2].Value = model.EndTime;
            parameters[3].Value = model.BeginOperator;
            parameters[4].Value = model.EndOperator;
            parameters[5].Value = model.Cost;
            parameters[6].Value = model.Type;
            parameters[7].Value = model.Status;
            parameters[8].Value = model.Creator;
            parameters[9].Value = model.CreatorIP;
            parameters[10].Value = model.CreateTime;
            parameters[11].Value = model.LastUpdater;
            parameters[12].Value = model.LastUpdaterIP;
            parameters[13].Value = model.LastUpdateTime;
            parameters[14].Value = model.IsDeleted;
            parameters[15].Value = model.Remark;
            parameters[16].Value = model.ProductName;
            parameters[17].Value = model.ProductNo;

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
        /// 更新一条数据，带事务
        /// </summary>
        public void Update(RefundInfo model, SqlConnection conn, SqlTransaction trans)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("UPDATE AD_Refund set ");
            strSql.Append("UserId=@UserId,");
            strSql.Append("BeginTime=@BeginTime,");
            strSql.Append("EndTime=@EndTime,");
            strSql.Append("BeginOperator=@BeginOperator,");
            strSql.Append("EndOperator=@EndOperator,");
            strSql.Append("Cost=@Cost,");
            strSql.Append("Type=@Type,");
            strSql.Append("Status=@Status,");
            strSql.Append("Creator=@Creator,");
            strSql.Append("CreatorIP=@CreatorIP,");
            strSql.Append("CreateTime=@CreateTime,");
            strSql.Append("LastUpdater=@LastUpdater,");
            strSql.Append("LastUpdaterIP=@LastUpdaterIP,");
            strSql.Append("LastUpdateTime=@LastUpdateTime,");
            strSql.Append("IsDeleted=@IsDeleted,");
            strSql.Append("Remark=@Remark,ProductName=@ProductName,ProductNo=@ProductNo");
            strSql.Append(" WHERE Id=@Id ");
            SqlParameter[] parameters = {
					new SqlParameter("@Id", SqlDbType.Int,4),
					new SqlParameter("@UserId", SqlDbType.Int,4),
					new SqlParameter("@BeginTime", SqlDbType.SmallDateTime),
					new SqlParameter("@EndTime", SqlDbType.SmallDateTime),
					new SqlParameter("@BeginOperator", SqlDbType.NVarChar,50),
					new SqlParameter("@EndOperator", SqlDbType.NVarChar,50),
					new SqlParameter("@Cost", SqlDbType.Decimal,9),
					new SqlParameter("@Type", SqlDbType.Int,4),
					new SqlParameter("@Status", SqlDbType.Int,4),
					new SqlParameter("@Creator", SqlDbType.NVarChar,50),
					new SqlParameter("@CreatorIP", SqlDbType.NVarChar,100),
					new SqlParameter("@CreateTime", SqlDbType.SmallDateTime),
					new SqlParameter("@LastUpdater", SqlDbType.NVarChar,50),
					new SqlParameter("@LastUpdaterIP", SqlDbType.NVarChar,100),
					new SqlParameter("@LastUpdateTime", SqlDbType.SmallDateTime),
					new SqlParameter("@IsDeleted", SqlDbType.Bit,1),
                     new SqlParameter("@Remark", SqlDbType.NVarChar, 500),
                    new SqlParameter("@ProductName", SqlDbType.NVarChar, 500),
                    new SqlParameter("@ProductNo", SqlDbType.NVarChar, 50)
                                        
                                        };
            parameters[0].Value = model.Id;
            parameters[1].Value = model.UserId;
            parameters[2].Value = model.BeginTime;
            parameters[3].Value = model.EndTime;
            parameters[4].Value = model.BeginOperator;
            parameters[5].Value = model.EndOperator;
            parameters[6].Value = model.Cost;
            parameters[7].Value = model.Type;
            parameters[8].Value = model.Status;
            parameters[9].Value = model.Creator;
            parameters[10].Value = model.CreatorIP;
            parameters[11].Value = model.CreateTime;
            parameters[12].Value = model.LastUpdater;
            parameters[13].Value = model.LastUpdaterIP;
            parameters[14].Value = model.LastUpdateTime;
            parameters[15].Value = model.IsDeleted;
            parameters[16].Value = model.Remark;
            parameters[17].Value = model.ProductName;
            parameters[18].Value = model.ProductNo;

            DbHelperSQL.ExecuteSql(strSql.ToString(), conn, trans, parameters);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public void Delete(int Id, SqlConnection conn, SqlTransaction trans)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from AD_Refund ");
            strSql.Append(" where Id=@Id ");
            SqlParameter[] parameters = {
					new SqlParameter("@Id", SqlDbType.Int,4)};
            parameters[0].Value = Id;

            DbHelperSQL.ExecuteSql(strSql.ToString(), conn, trans, parameters);
        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public RefundInfo GetModel(int Id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT TOP 1 * FROM AD_Refund ");
            strSql.Append(" WHERE Id=@Id");
            SqlParameter[] parameters = {
					new SqlParameter("@Id", SqlDbType.Int,4)};
            parameters[0].Value = Id;

            RefundInfo model = new RefundInfo();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            model.Id = Id;
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
            strSql.Append(" FROM AD_Refund WHERE isdeleted=0 ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" AND " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }

        public DataSet GetListByUser(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"SELECT r.*,u.userid,u.lastnamecn,u.firstnamecn,u.lastnameen,u.firstnameen,e.code,d.* FROM AD_Refund r 
                              JOIN sep_users u ON r.userid=u.userid 
                              JOIN sep_employees e ON r.userid=e.userid 
                              JOIN sep_EmployeesInPositions eip ON r.userid = eip.userid 
                              LEFT JOIN v_Department d ON eip.departmentId = d.level3Id ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" WHERE (e.status in(1,3) AND r.isdeleted=0) " + strWhere);
            }
            strSql.Append(" ORDER BY r.Status, r.BeginTime desc");
            return DbHelperSQL.Query(strSql.ToString());
        }
               
        /// <summary>
        /// 获得用户所申请的报销数据对象
        /// </summary>
        /// <param name="userID">用户编号</param>
        /// <param name="type">报销类型</param>
        /// <param name="status">报销数据状态</param>
        /// <returns>返回用户申请的报销数据集合</returns>
        public List<RefundInfo> GetRefundInfos(int userID, RefundType type)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM AD_Refund ");
            strSql.Append(" WHERE isDeleted=0 AND UserId=@UserId AND Type=@Type AND Status=@Status ORDER BY BeginTime");
            SqlParameter[] parameters = {
					new SqlParameter("@UserId", SqlDbType.Int, 4),
                    new SqlParameter("@Type", SqlDbType.Int, 4),
                    new SqlParameter("@Status", SqlDbType.Int, 4)};
            parameters[0].Value = userID;
            parameters[1].Value = (int)type;
            parameters[2].Value = (int)Common.RefundStatus.BeginStatus;

            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            List<RefundInfo> list = new List<RefundInfo>();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    RefundInfo model = new RefundInfo();
                    model.PopupData(dr);
                    if (model != null && model.EndTime == null)
                        model.EndTime = DateTime.Now.Date;
                    list.Add(model);
                }
            }
            return list;
        }

        /// <summary>
        /// 获得用户报销信息
        /// </summary>
        /// <param name="userId">用户编号</param>
        /// <param name="type">报销类型</param>
        /// <param name="status">报销状态</param>
        /// <returns>返回笔记本报销信息</returns>
        public RefundInfo GetEnableRefundList(int userId, RefundType type, RefundStatus status)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM AD_Refund ");
            strSql.Append(" WHERE isDeleted=0 AND UserId=@UserId AND Type=@Type AND Status=@Status ORDER BY BeginTime");
            SqlParameter[] parameters = {
					new SqlParameter("@UserId", SqlDbType.Int, 4),
                    new SqlParameter("@Type", SqlDbType.Int, 4),
                    new SqlParameter("@Status", SqlDbType.Int, 4)};
            parameters[0].Value = userId;
            parameters[1].Value = (int)type;
            parameters[2].Value = (int)status;

            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            RefundInfo model = new RefundInfo();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                model.PopupData(ds.Tables[0].Rows[0]);
                if (model != null && model.EndTime == null)
                    model.EndTime = DateTime.Now.Date;
            }
            return model;
        }
        #endregion  成员方法
    }
}