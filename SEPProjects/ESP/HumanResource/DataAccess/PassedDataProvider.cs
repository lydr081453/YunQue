using System;
using System.Collections.Generic;
using ESP.HumanResource.Entity;
using System.Web;
using System.Data.SqlClient;
using ESP.HumanResource.Common;
using System.Data;
using System.Text;

namespace ESP.HumanResource.DataAccess
{    
    public class PassedDataProvider
    {
        public PassedDataProvider()
        { }
        #region  成员方法
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from SEP_PassedInfo");
            strSql.Append(" where id= @id");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)
				};
            parameters[0].Value = id;
            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(PassedInfo model, SqlConnection conn, SqlTransaction trans)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into SEP_PassedInfo(");
            strSql.Append("sysid,groupName,sysUserName,joinJobDate,companyID,companyName,departmentID,departmentName,groupID,currentTitle,salaryDetailID,operationDate,memo,creater,createrName,createDate)");
            strSql.Append(" values (");
            strSql.Append("@sysid,@groupName,@sysUserName,@joinJobDate,@companyID,@companyName,@departmentID,@departmentName,@groupID,@currentTitle,@salaryDetailID,@operationDate,@memo,@creater,@createrName,@createDate)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@sysid", SqlDbType.Int,4),
					new SqlParameter("@groupName", SqlDbType.NVarChar,50),
					new SqlParameter("@sysUserName", SqlDbType.NVarChar,50),
					new SqlParameter("@joinJobDate", SqlDbType.SmallDateTime),
					new SqlParameter("@companyID", SqlDbType.Int,4),
					new SqlParameter("@companyName", SqlDbType.NVarChar,50),
					new SqlParameter("@departmentID", SqlDbType.Int,4),
					new SqlParameter("@departmentName", SqlDbType.NVarChar,50),
					new SqlParameter("@groupID", SqlDbType.Int,4),
					new SqlParameter("@currentTitle", SqlDbType.NVarChar,50),
					new SqlParameter("@salaryDetailID", SqlDbType.Int,4),
					new SqlParameter("@operationDate", SqlDbType.SmallDateTime),
					new SqlParameter("@memo", SqlDbType.NVarChar,2000),
					new SqlParameter("@creater", SqlDbType.Int,4),
					new SqlParameter("@createrName", SqlDbType.NVarChar,50),
					new SqlParameter("@createDate", SqlDbType.SmallDateTime)};
            parameters[0].Value = model.sysid;
            parameters[1].Value = model.groupName;
            parameters[2].Value = model.sysUserName;
            parameters[3].Value = model.joinJobDate;
            parameters[4].Value = model.companyID;
            parameters[5].Value = model.companyName;
            parameters[6].Value = model.departmentID;
            parameters[7].Value = model.departmentName;
            parameters[8].Value = model.groupID;
            parameters[9].Value = model.currentTitle;
            parameters[10].Value = model.salaryDetailID;
            parameters[11].Value = model.operationDate;
            parameters[12].Value = model.memo;
            parameters[13].Value = model.creater;
            parameters[14].Value = model.createrName;
            parameters[15].Value = model.createDate;

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
        public int Update(PassedInfo model, SqlConnection conn, SqlTransaction trans)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update SEP_PassedInfo set ");
            strSql.Append("sysid=@sysid,");
            strSql.Append("groupName=@groupName,");
            strSql.Append("sysUserName=@sysUserName,");
            strSql.Append("joinJobDate=@joinJobDate,");
            strSql.Append("companyID=@companyID,");
            strSql.Append("companyName=@companyName,");
            strSql.Append("departmentID=@departmentID,");
            strSql.Append("departmentName=@departmentName,");
            strSql.Append("groupID=@groupID,");
            strSql.Append("currentTitle=@currentTitle,");
            strSql.Append("salaryDetailID=@salaryDetailID,");
            strSql.Append("operationDate=@operationDate,");
            strSql.Append("memo=@memo,");
            strSql.Append("creater=@creater,");
            strSql.Append("createrName=@createrName,");
            strSql.Append("createDate=@createDate");
            strSql.Append(" where id=@id");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4),
					new SqlParameter("@sysid", SqlDbType.Int,4),
					new SqlParameter("@groupName", SqlDbType.NVarChar,50),
					new SqlParameter("@sysUserName", SqlDbType.NVarChar,50),
					new SqlParameter("@joinJobDate", SqlDbType.SmallDateTime),
					new SqlParameter("@companyID", SqlDbType.Int,4),
					new SqlParameter("@companyName", SqlDbType.NVarChar,50),
					new SqlParameter("@departmentID", SqlDbType.Int,4),
					new SqlParameter("@departmentName", SqlDbType.NVarChar,50),
					new SqlParameter("@groupID", SqlDbType.Int,4),
					new SqlParameter("@currentTitle", SqlDbType.NVarChar,50),
					new SqlParameter("@salaryDetailID", SqlDbType.Int,4),
					new SqlParameter("@operationDate", SqlDbType.SmallDateTime),
					new SqlParameter("@memo", SqlDbType.NVarChar,2000),
					new SqlParameter("@creater", SqlDbType.Int,4),
					new SqlParameter("@createrName", SqlDbType.NVarChar,50),
					new SqlParameter("@createDate", SqlDbType.SmallDateTime)};
            parameters[0].Value = model.id;
            parameters[1].Value = model.sysid;
            parameters[2].Value = model.groupName;
            parameters[3].Value = model.sysUserName;
            parameters[4].Value = model.joinJobDate;
            parameters[5].Value = model.companyID;
            parameters[6].Value = model.companyName;
            parameters[7].Value = model.departmentID;
            parameters[8].Value = model.departmentName;
            parameters[9].Value = model.groupID;
            parameters[10].Value = model.currentTitle;
            parameters[11].Value = model.salaryDetailID;
            parameters[12].Value = model.operationDate;
            parameters[13].Value = model.memo;
            parameters[14].Value = model.creater;
            parameters[15].Value = model.createrName;
            parameters[16].Value = model.createDate;

            return DbHelperSQL.ExecuteSql(strSql.ToString(), conn, trans, parameters);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public void Delete(int id, SqlConnection conn, SqlTransaction trans)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete SEP_PassedInfo ");
            strSql.Append(" where id=@id");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)
				};
            parameters[0].Value = id;
            DbHelperSQL.ExecuteSql(strSql.ToString(), conn, trans, parameters);
        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public PassedInfo GetModel(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from SEP_PassedInfo ");
            strSql.Append(" where id=@id");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)};
            parameters[0].Value = id;
            PassedInfo model = new PassedInfo();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            model.id = id;
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["sysid"].ToString() != "")
                {
                    model.sysid = int.Parse(ds.Tables[0].Rows[0]["sysid"].ToString());
                }
                model.groupName = ds.Tables[0].Rows[0]["groupName"].ToString();
                model.sysUserName = ds.Tables[0].Rows[0]["sysUserName"].ToString();
                if (ds.Tables[0].Rows[0]["joinJobDate"].ToString() != "")
                {
                    model.joinJobDate = DateTime.Parse(ds.Tables[0].Rows[0]["joinJobDate"].ToString());
                }
                if (ds.Tables[0].Rows[0]["companyID"].ToString() != "")
                {
                    model.companyID = int.Parse(ds.Tables[0].Rows[0]["companyID"].ToString());
                }
                model.companyName = ds.Tables[0].Rows[0]["companyName"].ToString();
                if (ds.Tables[0].Rows[0]["departmentID"].ToString() != "")
                {
                    model.departmentID = int.Parse(ds.Tables[0].Rows[0]["departmentID"].ToString());
                }
                model.departmentName = ds.Tables[0].Rows[0]["departmentName"].ToString();
                if (ds.Tables[0].Rows[0]["groupID"].ToString() != "")
                {
                    model.groupID = int.Parse(ds.Tables[0].Rows[0]["groupID"].ToString());
                }
                model.currentTitle = ds.Tables[0].Rows[0]["currentTitle"].ToString();
                if (ds.Tables[0].Rows[0]["salaryDetailID"].ToString() != "")
                {
                    model.salaryDetailID = int.Parse(ds.Tables[0].Rows[0]["salaryDetailID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["operationDate"].ToString() != "")
                {
                    model.operationDate = DateTime.Parse(ds.Tables[0].Rows[0]["operationDate"].ToString());
                }
                model.memo = ds.Tables[0].Rows[0]["memo"].ToString();
                if (ds.Tables[0].Rows[0]["creater"].ToString() != "")
                {
                    model.creater = int.Parse(ds.Tables[0].Rows[0]["creater"].ToString());
                }
                model.createrName = ds.Tables[0].Rows[0]["createrName"].ToString();
                if (ds.Tables[0].Rows[0]["createDate"].ToString() != "")
                {
                    model.createDate = DateTime.Parse(ds.Tables[0].Rows[0]["createDate"].ToString());
                }
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
            strSql.Append("select [id],[sysid],[groupName],[sysUserName],[joinJobDate],[companyID],[companyName],[departmentID],[departmentName],[groupID],[currentTitle],[salaryDetailID],[operationDate],[memo],[creater],[createrName],[createDate] ");
            strSql.Append(" FROM SEP_PassedInfo ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 获得对象列表
        /// </summary>
        /// <param name="strWhere"></param>
        /// <param name="parms"></param>
        /// <returns></returns>
        public List<PassedInfo> GetModelList(string strWhere, List<SqlParameter> parms)
        {
            string strSql = "select * from SEP_PassedInfo where 1=1 ";
            strSql += strWhere;
            List<PassedInfo> list = new List<PassedInfo>();
            using (SqlDataReader r = DbHelperSQL.ExecuteReader(strSql, parms))
            {
                while (r.Read())
                {
                    PassedInfo model = new PassedInfo();
                    model.PopupData(r);
                    list.Add(model);
                }
                r.Close();
            }
            return list;
        }

        #endregion  成员方法
    }
}
