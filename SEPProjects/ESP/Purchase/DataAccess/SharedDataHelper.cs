using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using ESP.Purchase.Common;

namespace ESP.Purchase.DataAccess
{
    /// <summary>
    ///SharedDataHelper 的摘要说明
    /// </summary>
    public class SharedDataHelper
    {
        public SharedDataHelper()
        {
            //
            //TODO: 在此处添加构造函数逻辑
            //
        }

        #region 物料类别列表
        /// <summary>
        /// 得到一个物料类别对象实体
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns></returns>
        public static Entity.V_GetCostType GetModel(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 * from V_GetCostType ");
            strSql.Append(" where typeid=@id ");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)};
            parameters[0].Value = id;

            Entity.V_GetCostType model = new Entity.V_GetCostType();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (null != ds.Tables[0].Rows[0]["typeid"] && ds.Tables[0].Rows[0]["typeid"].ToString() != "")
                {
                    model.typeid = int.Parse(ds.Tables[0].Rows[0]["typeid"].ToString());
                }
                model.typename = ds.Tables[0].Rows[0]["typename"].ToString();
                if (null != ds.Tables[0].Rows[0]["parentId"] && ds.Tables[0].Rows[0]["parentId"].ToString() != "")
                {
                    model.parentId = int.Parse(ds.Tables[0].Rows[0]["parentId"].ToString());
                }
                if (null != ds.Tables[0].Rows[0]["typelevel"] && ds.Tables[0].Rows[0]["typelevel"].ToString() != "")
                {
                    model.typelevel = int.Parse(ds.Tables[0].Rows[0]["typelevel"].ToString());
                }
                return model;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 物料类别列表
        /// </summary>
        /// <param name="strWhere">The STR where.</param>
        /// <returns></returns>
        public static List<Entity.V_GetCostType> GetTypeList(string strWhere)
        {
            List<Entity.V_GetCostType> list = new List<Entity.V_GetCostType>();
            StringBuilder strB = new StringBuilder();
            strB.Append(" select * from V_GetCostType ");
            string sql = string.Format(strB.ToString() + " where 1=1 {0} order by typeid desc", strWhere);
            using (SqlDataReader r = DbHelperSQL.ExecuteReader(sql))
            {
                while (r.Read())
                {
                    Entity.V_GetCostType c = new Entity.V_GetCostType();
                    c.PopupData(r);
                    list.Add(c);
                }
                r.Close();
            }
            return list;
        }

        /// <summary>
        /// 物料类别列表
        /// </summary>
        /// <param name="strWhere">The STR where.</param>
        /// <param name="parms">The parms.</param>
        /// <returns></returns>
        public static List<Entity.V_GetCostType> GetTypeList(string strWhere, List<SqlParameter> parms)
        {
            List<Entity.V_GetCostType> list = new List<Entity.V_GetCostType>();
            StringBuilder strB = new StringBuilder();
            strB.Append(" select * from V_GetCostType ");
            string sql = string.Format(strB.ToString() + " where 1=1 {0} order by typeid desc", strWhere);
            using (SqlDataReader r = DbHelperSQL.ExecuteReader(sql, parms))
            {
                while (r.Read())
                {
                    Entity.V_GetCostType c = new Entity.V_GetCostType();
                    c.PopupData(r);
                    list.Add(c);
                }
                r.Close();
            }
            return list;
        }
        #endregion  物料类别列表

        #region 物料类别方法
        /// <summary>
        /// 根据项目号Id和成本所属组的Id取得所有PR单中的2级物料类别
        /// </summary>
        /// <param name="projectId">The project id.</param>
        /// <param name="depId">The dep id.</param>
        /// <returns></returns>
        public List<int> GetL2TypeByProjectIDDepId(int projectId,int depId)
        {
            List<int> list = new List<int>();
            StringBuilder strB = new StringBuilder();
            strB.Append(@" select distinct c.parentid from T_OrderInfo as a inner join
	                        T_GeneralInfo as b on b.id=a.general_id inner join
	                        T_Type as c on a.producttype = c.typeid where b.id ={0} and b.DepartmentId={1}");
            string sql = string.Format(strB.ToString(), projectId.ToString(),depId.ToString());
            using (SqlDataReader r = DbHelperSQL.ExecuteReader(sql))
            {
                while (r.Read())
                {
                    if (null != r["parentid"] && r["parentid"].ToString() != string.Empty)
                    {
                        list.Add(int.Parse(r["parentid"].ToString()));
                    }
                }
                r.Close();
            }
            return list;
        }
        #endregion 物料类别方法
    }

    public class V_GetProjectList
    {
        public V_GetProjectList()
		{}

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns></returns>
        public static Entity.V_GetProjectList GetModel(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 * from V_GetProjectList ");
            strSql.Append(" where ProjectId=@id ");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)};
            parameters[0].Value = id;

            Entity.V_GetProjectList model = new Entity.V_GetProjectList();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (null != ds.Tables[0].Rows[0]["ProjectId"] && ds.Tables[0].Rows[0]["ProjectId"].ToString() != "")
                {
                    model.ProjectId = int.Parse(ds.Tables[0].Rows[0]["ProjectId"].ToString());
                }
                model.SerialCode = ds.Tables[0].Rows[0]["SerialCode"].ToString();
                model.ProjectCode = ds.Tables[0].Rows[0]["ProjectCode"].ToString();
                model.BusinessDescription = ds.Tables[0].Rows[0]["BusinessDescription"].ToString();
                if (null != ds.Tables[0].Rows[0]["GroupID"] && ds.Tables[0].Rows[0]["GroupID"].ToString() != "")
                {
                    model.GroupID = int.Parse(ds.Tables[0].Rows[0]["GroupID"].ToString());
                }
                model.GroupName = ds.Tables[0].Rows[0]["GroupName"].ToString();
                if (null != ds.Tables[0].Rows[0]["TotalAmount"] && ds.Tables[0].Rows[0]["TotalAmount"].ToString() != "")
                {
                    model.TotalAmount = decimal.Parse(ds.Tables[0].Rows[0]["TotalAmount"].ToString());
                }
                model.CreateDate = ds.Tables[0].Rows[0]["CreateDate"] == DBNull.Value ? DateTime.Parse(Common.State.datetime_minvalue) : DateTime.Parse(ds.Tables[0].Rows[0]["CreateDate"].ToString());
                model.SubmitDate = ds.Tables[0].Rows[0]["SubmitDate"] == DBNull.Value ? DateTime.Parse(Common.State.datetime_minvalue) : DateTime.Parse(ds.Tables[0].Rows[0]["SubmitDate"].ToString());
                if (null != ds.Tables[0].Rows[0]["Status"] && ds.Tables[0].Rows[0]["Status"].ToString() != "")
                {
                    model.Status = int.Parse(ds.Tables[0].Rows[0]["Status"].ToString());
                }
                if (null != ds.Tables[0].Rows[0]["MemberUserID"] && ds.Tables[0].Rows[0]["MemberUserID"].ToString() != "")
                {
                    model.MemberUserID = int.Parse(ds.Tables[0].Rows[0]["MemberUserID"].ToString());
                }
                model.MemberUserName = ds.Tables[0].Rows[0]["MemberUserName"].ToString();
                return model;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Gets the status list.
        /// </summary>
        /// <param name="strWhere">The STR where.</param>
        /// <returns></returns>
        public static List<Entity.V_GetProjectList> GetProjectList(string strWhere)
        {
            List<Entity.V_GetProjectList> list = new List<Entity.V_GetProjectList>();
            StringBuilder strB = new StringBuilder();
            strB.Append(" select * from V_GetProjectList ");
            string sql = string.Format(strB.ToString() + " where (1=1 {0}) or Projectid=3372 order by ProjectId desc", strWhere);
            using (SqlDataReader r = DbHelperSQL.ExecuteReader(sql))
            {
                while (r.Read())
                {
                    Entity.V_GetProjectList c = new Entity.V_GetProjectList();
                    c.PopupData(r);
                    list.Add(c);
                }
                r.Close();
            }
            return list;
        }

        /// <summary>
        /// Gets the status list.
        /// </summary>
        /// <param name="strWhere">The STR where.</param>
        /// <param name="parms">The parms.</param>
        /// <returns></returns>
        public static List<Entity.V_GetProjectList> GetProjectList(string strWhere, List<SqlParameter> parms)
        {
            List<Entity.V_GetProjectList> list = new List<Entity.V_GetProjectList>();
            StringBuilder strB = new StringBuilder();
            strB.Append(" select * from V_GetProjectList ");
            string sql = string.Format(strB.ToString() + " where (1=1 {0}) or memberUserID=0 order by ProjectId desc", strWhere);
            using (SqlDataReader r = DbHelperSQL.ExecuteReader(sql, parms))
            {
                while (r.Read())
                {
                    Entity.V_GetProjectList c = new Entity.V_GetProjectList();
                    c.PopupData(r);
                    list.Add(c);
                }
                r.Close();
            }
            return list;
        }

        public static List<Entity.V_GetProjectList> GetProjectListMember(string strWhere, List<SqlParameter> parms)
        {
            List<Entity.V_GetProjectList> list = new List<Entity.V_GetProjectList>();
            StringBuilder strB = new StringBuilder();
            strB.Append(" select * from V_GetProjectList ");
            string sql = string.Format(strB.ToString() + " where (1=1 {0})", strWhere);
            using (SqlDataReader r = DbHelperSQL.ExecuteReader(sql, parms))
            {
                while (r.Read())
                {
                    Entity.V_GetProjectList c = new Entity.V_GetProjectList();
                    c.PopupData(r);
                    list.Add(c);
                }
                r.Close();
            }
            return list;
        }

        public static List<Entity.V_GetProjectList> GetProjectListByDirector(int currentUserID, string strTerms)
        {
            List<Entity.V_GetProjectList> list = new List<Entity.V_GetProjectList>();
            string sql = "SELECT DISTINCT " +
                      " A.ProjectId, A.SerialCode, A.ProjectCode, A.BusinessDescription, A.GroupID, " +
                      " A.GroupName, A.TotalAmount, A.CreateDate, A.SubmitDate, A.Status" +
                      " FROM F_Project A " +

                      " WHERE  ((A.Status = 32) and " +
                      " A.GroupID in(SELECT distinct depid FROM sep_OperationAuditManage where directorid =" + currentUserID + "))  " +
                      strTerms;
            sql += string.Format(" union select ProjectId,SerialCode,ProjectCode,BusinessDescription,GroupID,GroupName,TotalAmount,CreateDate,SubmitDate,Status from V_GetProjectList where (1=1 {0} and (memberUserID=0 or memberUserID = {1})) order by ProjectId desc", strTerms, currentUserID);
            using (SqlDataReader r = DbHelperSQL.ExecuteReader(sql))
            {
                while (r.Read())
                {
                    Entity.V_GetProjectList c = new Entity.V_GetProjectList();
                    c.PopupData(r);
                    list.Add(c);
                }
                r.Close();
            }
            return list;
        }

        public static List<Entity.V_GetProjectList> GetProjectListByManager(int currentUserID, string strTerms)
        {
            List<Entity.V_GetProjectList> list = new List<Entity.V_GetProjectList>();
            string sql = "SELECT DISTINCT " +
                      " A.ProjectId, A.SerialCode, A.ProjectCode, A.BusinessDescription, A.GroupID, " +
                      " A.GroupName, A.TotalAmount, A.CreateDate, A.SubmitDate, A.Status" +
                      " FROM F_Project A "+
                      
                      " WHERE  ((A.Status = 32) and " +
                      " A.GroupID in(SELECT distinct depid FROM sep_OperationAuditManage where directorid =" + currentUserID +" or managerid = " + currentUserID + "))  " +
                      strTerms;
            sql += string.Format(" union select ProjectId,SerialCode,ProjectCode,BusinessDescription,GroupID,GroupName,TotalAmount,CreateDate,SubmitDate,Status from V_GetProjectList where (1=1 {0} and (memberUserID=0 or memberUserID = {1})) order by ProjectId desc", strTerms, currentUserID);
            using (SqlDataReader r = DbHelperSQL.ExecuteReader(sql))
            {
                while (r.Read())
                {
                    Entity.V_GetProjectList c = new Entity.V_GetProjectList();
                    c.PopupData(r);
                    list.Add(c);
                }
                r.Close();
            }
            return list;
        }

        public static List<Entity.V_GetProjectList> GetProjectListByDept(int currentUserID, string deptids, string strTerms)
        {
            List<Entity.V_GetProjectList> list = new List<Entity.V_GetProjectList>();
            string sql = "SELECT DISTINCT " +
                      " A.ProjectId, A.SerialCode, A.ProjectCode, A.BusinessDescription, A.GroupID, " +
                      " A.GroupName, A.TotalAmount, A.CreateDate, A.SubmitDate, A.Status" +
                      " FROM F_Project A " +

                      " WHERE  ((A.Status = 32) and " +
                      " A.GroupID in("+deptids+"))  " +
                      strTerms;
            sql += string.Format(" union select ProjectId,SerialCode,ProjectCode,BusinessDescription,GroupID,GroupName,TotalAmount,CreateDate,SubmitDate,Status from V_GetProjectList where (1=1 {0} and (memberUserID=0 or memberUserID = {1})) order by ProjectId desc", strTerms, currentUserID);
            using (SqlDataReader r = DbHelperSQL.ExecuteReader(sql))
            {
                while (r.Read())
                {
                    Entity.V_GetProjectList c = new Entity.V_GetProjectList();
                    c.PopupData(r);
                    list.Add(c);
                }
                r.Close();
            }
            return list;
        }
    }

    public class V_GetProjectGroupList
    {
        public V_GetProjectGroupList()
        { }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        /// <param name="pid">The pid.</param>
        /// <param name="gid">The gid.</param>
        /// <returns></returns>
        public static Entity.V_GetProjectGroupList GetModel(int pid, int gid)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 * from V_GetProjectGroupList ");
            strSql.Append(" where ProjectId=@pid and GroupID = @gid ");
            SqlParameter[] parameters = {
					new SqlParameter("@pid", SqlDbType.Int,4),
					new SqlParameter("@gid", SqlDbType.Int,4)};
            parameters[0].Value = pid;
            parameters[1].Value = gid;

            Entity.V_GetProjectGroupList model = new Entity.V_GetProjectGroupList();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (null != ds.Tables[0].Rows[0]["ProjectId"] && ds.Tables[0].Rows[0]["ProjectId"].ToString() != "")
                {
                    model.ProjectId = int.Parse(ds.Tables[0].Rows[0]["ProjectId"].ToString());
                }
                if (null != ds.Tables[0].Rows[0]["GroupID"] && ds.Tables[0].Rows[0]["GroupID"].ToString() != "")
                {
                    model.GroupID = int.Parse(ds.Tables[0].Rows[0]["GroupID"].ToString());
                }
                model.GroupName = ds.Tables[0].Rows[0]["GroupName"].ToString();
                if (null != ds.Tables[0].Rows[0]["COST"] && ds.Tables[0].Rows[0]["COST"].ToString() != "")
                {
                    model.COST = decimal.Parse(ds.Tables[0].Rows[0]["COST"].ToString());
                }
                return model;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Gets the status list.
        /// </summary>
        /// <param name="strWhere">The STR where.</param>
        /// <returns></returns>
        public static List<Entity.V_GetProjectGroupList> GetList(string strWhere)
        {
            List<Entity.V_GetProjectGroupList> list = new List<Entity.V_GetProjectGroupList>();
            StringBuilder strB = new StringBuilder();
            strB.Append(" select * from V_GetProjectGroupList ");
            string sql = string.Format(strB.ToString() + " where (1=1 {0}) or Projectid=3372 order by ProjectId desc", strWhere);
            using (SqlDataReader r = DbHelperSQL.ExecuteReader(sql))
            {
                while (r.Read())
                {
                    Entity.V_GetProjectGroupList c = new Entity.V_GetProjectGroupList();
                    c.PopupData(r);
                    list.Add(c);
                }
                r.Close();
            }
            return list;
        }

        /// <summary>
        /// Gets the status list.
        /// </summary>
        /// <param name="strWhere">The STR where.</param>
        /// <param name="parms">The parms.</param>
        /// <returns></returns>
        public static List<Entity.V_GetProjectGroupList> GetList(string strWhere, List<SqlParameter> parms)
        {
            List<Entity.V_GetProjectGroupList> list = new List<Entity.V_GetProjectGroupList>();
            StringBuilder strB = new StringBuilder();
            strB.Append(" select * from V_GetProjectGroupList ");
            string sql = string.Format(strB.ToString() + " where 1=1 {0} order by ProjectId desc", strWhere);
            using (SqlDataReader r = DbHelperSQL.ExecuteReader(sql, parms))
            {
                while (r.Read())
                {
                    Entity.V_GetProjectGroupList c = new Entity.V_GetProjectGroupList();
                    c.PopupData(r);
                    list.Add(c);
                }
                r.Close();
            }
            return list;
        }
    }

    public class V_GetProjectTypeList
    {
        public V_GetProjectTypeList()
        { }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        /// <param name="pid">The pid.</param>
        /// <param name="gid">The gid.</param>
        /// <returns></returns>
        public static Entity.V_GetProjectTypeList GetModel(int pid, int gid, int tid)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 * from V_GetProjectTypeList ");
            strSql.Append(" where ProjectId=@pid and GroupID = @gid ");
            SqlParameter[] parameters = {
					new SqlParameter("@pid", SqlDbType.Int,4),
					new SqlParameter("@gid", SqlDbType.Int,4),
					new SqlParameter("@tid", SqlDbType.Int,4)};
            parameters[0].Value = pid;
            parameters[1].Value = gid;
            parameters[2].Value = tid;

            Entity.V_GetProjectTypeList model = new Entity.V_GetProjectTypeList();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (null != ds.Tables[0].Rows[0]["ProjectId"] && ds.Tables[0].Rows[0]["ProjectId"].ToString() != "")
                {
                    model.ProjectId = int.Parse(ds.Tables[0].Rows[0]["ProjectId"].ToString());
                }
                if (null != ds.Tables[0].Rows[0]["GroupID"] && ds.Tables[0].Rows[0]["GroupID"].ToString() != "")
                {
                    model.GroupID = int.Parse(ds.Tables[0].Rows[0]["GroupID"].ToString());
                }
                if (null != ds.Tables[0].Rows[0]["TypeID"] && ds.Tables[0].Rows[0]["TypeID"].ToString() != "")
                {
                    model.TypeID = int.Parse(ds.Tables[0].Rows[0]["TypeID"].ToString());
                }
                if (null != ds.Tables[0].Rows[0]["Amounts"] && ds.Tables[0].Rows[0]["Amounts"].ToString() != "")
                {
                    model.Amounts = decimal.Parse(ds.Tables[0].Rows[0]["Amounts"].ToString());
                }
                return model;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Gets the status list.
        /// </summary>
        /// <param name="strWhere">The STR where.</param>
        /// <returns></returns>
        public static List<Entity.V_GetProjectTypeList> GetList(string strWhere)
        {
            List<Entity.V_GetProjectTypeList> list = new List<Entity.V_GetProjectTypeList>();
            StringBuilder strB = new StringBuilder();
            strB.Append(" select a.*,b.parentId from V_GetProjectTypeList as a inner join T_Type as b on a.typeid = b.typeid ");
            string sql = string.Format(strB.ToString() + " where 1=1 {0} order by ProjectId desc", strWhere);
            using (SqlDataReader r = DbHelperSQL.ExecuteReader(sql))
            {
                while (r.Read())
                {
                    Entity.V_GetProjectTypeList c = new Entity.V_GetProjectTypeList();
                    c.PopupData(r);
                    list.Add(c);
                }
                r.Close();
            }
            return list;
        }

        /// <summary>
        /// Gets the status list.
        /// </summary>
        /// <param name="strWhere">The STR where.</param>
        /// <param name="parms">The parms.</param>
        /// <returns></returns>
        public static List<Entity.V_GetProjectTypeList> GetList(string strWhere, List<SqlParameter> parms)
        {
            List<Entity.V_GetProjectTypeList> list = new List<Entity.V_GetProjectTypeList>();
            StringBuilder strB = new StringBuilder();
            strB.Append(" select a.*,b.parentId,b.typeName from V_GetProjectTypeList as a inner join T_Type as b on a.typeid = b.typeid ");
            string sql = string.Format(strB.ToString() + " where 1=1 {0} order by ProjectId desc", strWhere);
            using (SqlDataReader r = DbHelperSQL.ExecuteReader(sql, parms))
            {
                while (r.Read())
                {
                    Entity.V_GetProjectTypeList c = new Entity.V_GetProjectTypeList();
                    c.PopupData(r);
                    list.Add(c);
                }
                r.Close();
            }
            return list;
        }
    }

}