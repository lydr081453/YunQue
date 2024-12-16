//using System;
//using System.Data;
//using System.Text;
//using System.Data.SqlClient;
//using ESP.Purchase.Common;
//using ESP.Purchase.Entity;

//namespace ESP.Purchase.DataAccess
//{
//    /// <summary>
//    /// 数据访问类OperationAuditManageDataHelper。
//    /// </summary>
//    public class OperationAuditManageDataHelper
//    {
//        /// <summary>
//        /// Initializes a new instance of the <see cref="OperationAuditManageDataHelper"/> class.
//        /// </summary>
//        public OperationAuditManageDataHelper()
//        { }

//        #region  成员方法
//        /// <summary>
//        /// 是否存在该记录
//        /// </summary>
//        /// <param name="Id">The id.</param>
//        /// <returns></returns>
//        public bool Exists(int Id)
//        {
//            StringBuilder strSql = new StringBuilder();
//            strSql.Append("select count(1) from T_OperationAuditManage");
//            strSql.Append(" where Id=@Id ");
//            SqlParameter[] parameters = {
//                    new SqlParameter("@Id", SqlDbType.Int,4)};
//            parameters[0].Value = Id;

//            return DbHelperSQL.Exists(strSql.ToString(), parameters);
//        }

//        /// <summary>
//        /// 增加一条数据
//        /// </summary>
//        /// <param name="model">The model.</param>
//        /// <returns></returns>
//        public int Add(OperationAuditManageInfo model)
//        {
//            StringBuilder strSql = new StringBuilder();
//            strSql.Append("insert into T_OperationAuditManage(");
//            strSql.Append("DepId,DirectorId,DirectorName,ManagerId,ManagerName,CEOId,CEOName,FAId,FAName)");
//            strSql.Append(" values (");
//            strSql.Append("@DepId,@DirectorId,@DirectorName,@ManagerId,@ManagerName,@CEOId,@CEOName,@FAId,@FAName)");
//            strSql.Append(";select @@IDENTITY");
//            SqlParameter[] parameters = {
//                    new SqlParameter("@DepId", SqlDbType.Int,4),
//                    new SqlParameter("@DirectorId", SqlDbType.Int,4),
//                    new SqlParameter("@DirectorName", SqlDbType.NVarChar,20),
//                    new SqlParameter("@ManagerId", SqlDbType.Int,4),
//                    new SqlParameter("@ManagerName", SqlDbType.NVarChar,20),
//                    new SqlParameter("@CEOId", SqlDbType.Int,4),
//                    new SqlParameter("@CEOName", SqlDbType.NVarChar,20),
//                    new SqlParameter("@FAId",SqlDbType.Int,4),
//                    new SqlParameter("@FAName",SqlDbType.NVarChar,20)
//                                        };
//            parameters[0].Value = model.DepId;
//            parameters[1].Value = model.DirectorId;
//            parameters[2].Value = model.DirectorName;
//            parameters[3].Value = model.ManagerId;
//            parameters[4].Value = model.ManagerName;
//            parameters[5].Value = model.CEOId;
//            parameters[6].Value = model.CEOName;
//            parameters[7].Value = model.FAId;
//            parameters[8].Value = model.FAName;

//            object obj = DbHelperSQL.GetSingle(strSql.ToString(), parameters);
//            if (obj == null)
//            {
//                return 1;
//            }
//            else
//            {
//                return Convert.ToInt32(obj);
//            }
//        }

//        /// <summary>
//        /// 更新一条数据
//        /// </summary>
//        /// <param name="model">The model.</param>
//        public void Update(OperationAuditManageInfo model)
//        {
//            StringBuilder strSql = new StringBuilder();
//            strSql.Append("update T_OperationAuditManage set ");
//            strSql.Append("DepId=@DepId,");
//            strSql.Append("DirectorId=@DirectorId,");
//            strSql.Append("DirectorName=@DirectorName,");
//            strSql.Append("ManagerId=@ManagerId,");
//            strSql.Append("ManagerName=@ManagerName,");
//            strSql.Append("CEOId=@CEOId,");
//            strSql.Append("CEOName=@CEOName,FAId=@FAId,FAName=@FAName");
//            strSql.Append(" where Id=@Id ");
//            SqlParameter[] parameters = {
//                    new SqlParameter("@Id", SqlDbType.Int,4),
//                    new SqlParameter("@DepId", SqlDbType.Int,4),
//                    new SqlParameter("@DirectorId", SqlDbType.Int,4),
//                    new SqlParameter("@DirectorName", SqlDbType.NVarChar,20),
//                    new SqlParameter("@ManagerId", SqlDbType.Int,4),
//                    new SqlParameter("@ManagerName", SqlDbType.NVarChar,20),
//                    new SqlParameter("@CEOId", SqlDbType.Int,4),
//                    new SqlParameter("@CEOName", SqlDbType.NVarChar,20),
//                    new SqlParameter("@FAId",SqlDbType.Int,4),
//                    new SqlParameter("@FAName",SqlDbType.NVarChar,20)};
//            parameters[0].Value = model.Id;
//            parameters[1].Value = model.DepId;
//            parameters[2].Value = model.DirectorId;
//            parameters[3].Value = model.DirectorName;
//            parameters[4].Value = model.ManagerId;
//            parameters[5].Value = model.ManagerName;
//            parameters[6].Value = model.CEOId;
//            parameters[7].Value = model.CEOName;
//            parameters[8].Value = model.FAId;
//            parameters[9].Value = model.FAName;

//            DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
//        }

//        /// <summary>
//        /// 删除一条数据
//        /// </summary>
//        /// <param name="Id">The id.</param>
//        public void Delete(int Id)
//        {
//            StringBuilder strSql = new StringBuilder();
//            strSql.Append("delete T_OperationAuditManage ");
//            strSql.Append(" where Id=@Id ");
//            SqlParameter[] parameters = {
//                    new SqlParameter("@Id", SqlDbType.Int,4)};
//            parameters[0].Value = Id;

//            DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
//        }

//        /// <summary>
//        /// 得到一个对象实体
//        /// </summary>
//        /// <param name="Id">The id.</param>
//        /// <returns></returns>
//        public OperationAuditManageInfo GetModel(int Id)
//        {
//            StringBuilder strSql = new StringBuilder();
//            strSql.Append("select  top 1 Id,DepId,DirectorId,DirectorName,ManagerId,ManagerName,CEOId,CEOName,FAId,FAName from T_OperationAuditManage ");
//            strSql.Append(" where Id=@Id ");
//            SqlParameter[] parameters = {
//                    new SqlParameter("@Id", SqlDbType.Int,4)};
//            parameters[0].Value = Id;

//            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
//            return setModel(ds);
//        }

//        /// <summary>
//        /// 根据部门ID获得一个对象实体
//        /// </summary>
//        /// <param name="DepId">The dep id.</param>
//        /// <returns></returns>
//        public OperationAuditManageInfo GetModelByDepId(int DepId)
//        {
//            StringBuilder strSql = new StringBuilder();
//            strSql.Append("select  top 1 Id,DepId,DirectorId,DirectorName,ManagerId,ManagerName,CEOId,CEOName,FAId,FAName from T_OperationAuditManage ");
//            strSql.Append(" where DepId=@DepId ");
//            SqlParameter[] parameters = {
//                    new SqlParameter("@DepId", SqlDbType.Int,4)};
//            parameters[0].Value = DepId;

//            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
//            return setModel(ds);
//        }

//        public OperationAuditManageInfo GetModelByDepId(int DepId,System.Data.SqlClient.SqlConnection conn, System.Data.SqlClient.SqlTransaction trans)
//        {
//            StringBuilder strSql = new StringBuilder();
//            strSql.Append("select  top 1 Id,DepId,DirectorId,DirectorName,ManagerId,ManagerName,CEOId,CEOName,FAId,FAName from T_OperationAuditManage ");
//            strSql.Append(" where DepId=@DepId ");
//            SqlParameter[] parameters = {
//                    new SqlParameter("@DepId", SqlDbType.Int,4)};
//            parameters[0].Value = DepId;

//            DataSet ds = DbHelperSQL.Query(strSql.ToString(),conn,trans, parameters);
//            return setModel(ds);
//        }


//        /// <summary>
//        /// Sets the model.
//        /// </summary>
//        /// <param name="ds">The ds.</param>
//        /// <returns></returns>
//        private OperationAuditManageInfo setModel(DataSet ds)
//        {
//            OperationAuditManageInfo model = new OperationAuditManageInfo();
//            if (ds.Tables[0].Rows.Count > 0)
//            {
//                if (ds.Tables[0].Rows[0]["Id"].ToString() != "")
//                {
//                    model.Id = int.Parse(ds.Tables[0].Rows[0]["Id"].ToString());
//                }
//                if (ds.Tables[0].Rows[0]["DepId"].ToString() != "")
//                {
//                    model.DepId = int.Parse(ds.Tables[0].Rows[0]["DepId"].ToString());
//                }
//                if (ds.Tables[0].Rows[0]["DirectorId"].ToString() != "")
//                {
//                    model.DirectorId = int.Parse(ds.Tables[0].Rows[0]["DirectorId"].ToString());
//                }
//                model.DirectorName = ds.Tables[0].Rows[0]["DirectorName"].ToString();
//                if (ds.Tables[0].Rows[0]["ManagerId"].ToString() != "")
//                {
//                    model.ManagerId = int.Parse(ds.Tables[0].Rows[0]["ManagerId"].ToString());
//                }
//                model.ManagerName = ds.Tables[0].Rows[0]["ManagerName"].ToString();
//                if (ds.Tables[0].Rows[0]["CEOId"].ToString() != "")
//                {
//                    model.CEOId = int.Parse(ds.Tables[0].Rows[0]["CEOId"].ToString());
//                }
//                model.CEOName = ds.Tables[0].Rows[0]["CEOName"].ToString();
//                if (ds.Tables[0].Rows[0]["FAId"].ToString() != "")
//                {
//                    model.FAId = int.Parse(ds.Tables[0].Rows[0]["FAId"].ToString());
//                }
//                model.FAName = ds.Tables[0].Rows[0]["FAName"].ToString();
//                return model;
//            }
//            else
//            {
//                return null;
//            }
//        }

//        /// <summary>
//        /// 获得数据列表
//        /// </summary>
//        /// <param name="strWhere">The STR where.</param>
//        /// <returns></returns>
//        public DataSet GetList(string strWhere)
//        {
//            StringBuilder strSql = new StringBuilder();
//            strSql.Append("select Id,DepId,DirectorId,DirectorName,ManagerId,ManagerName,CEOId,CEOName,FAId,FAName ");
//            strSql.Append(" FROM T_OperationAuditManage ");
//            if (strWhere.Trim() != "")
//            {
//                strSql.Append(" where " + strWhere);
//            }
//            return DbHelperSQL.Query(strSql.ToString());
//        }


//        /// <summary>
//        /// 获得总监的sysids
//        /// </summary>
//        /// <returns></returns>
//        public string GetDirectorIds()
//        {
//            string directorIds = "";
//            StringBuilder strSql = new StringBuilder();
//            strSql.Append("select distinct DirectorId ");
//            strSql.Append(" FROM T_OperationAuditManage ");
//            DataSet ds = DbHelperSQL.Query(strSql.ToString());
//            if (ds != null && ds.Tables.Count > 0)
//            {
//                foreach (DataRow dr in ds.Tables[0].Rows)
//                {
//                    directorIds += dr["DirectorId"].ToString() + ",";
//                }
//            }
//            if (directorIds.Length > 0)
//                directorIds = directorIds.TrimEnd(',');
//            return directorIds;
//        }

//        /// <summary>
//        /// 获得总经理的sysids
//        /// </summary>
//        /// <returns></returns>
//        public string GetManagerIds()
//        {
//            string ManagerIds = "";
//            StringBuilder strSql = new StringBuilder();
//            strSql.Append("select distinct ManagerId ");
//            strSql.Append(" FROM T_OperationAuditManage ");
//            DataSet ds = DbHelperSQL.Query(strSql.ToString());
//            if (ds != null && ds.Tables.Count > 0)
//            {
//                foreach (DataRow dr in ds.Tables[0].Rows)
//                {
//                    ManagerIds += dr["ManagerId"].ToString() + ",";
//                }
//            }
//            if (ManagerIds.Length > 0)
//                ManagerIds = ManagerIds.TrimEnd(',');
//            return ManagerIds;
//        }
//        /// <summary>
//        /// 获得CEO的sysids
//        /// </summary>
//        /// <returns></returns>
//        public string GetCEOIds()
//        {
//            string CEOIds = "";
//            StringBuilder strSql = new StringBuilder();
//            strSql.Append("select distinct CEOID ");
//            strSql.Append(" FROM T_OperationAuditManage ");
//            DataSet ds = DbHelperSQL.Query(strSql.ToString());
//            if (ds != null && ds.Tables.Count > 0)
//            {
//                foreach (DataRow dr in ds.Tables[0].Rows)
//                {
//                    CEOIds += dr["CEOID"].ToString() + ",";
//                }
//            }
//            if (CEOIds.Length > 0)
//                CEOIds = CEOIds.TrimEnd(',');
//            return CEOIds;
//        }
//        #endregion  成员方法
//    }
//}