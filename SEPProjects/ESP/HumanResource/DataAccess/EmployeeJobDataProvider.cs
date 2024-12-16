using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using ESP.HumanResource.Common;
using ESP.HumanResource.Entity;

namespace ESP.HumanResource.DataAccess
{   
    public class EmployeeJobDataProvider
    {
        public EmployeeJobDataProvider()
        { }
        #region  成员方法

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from SEP_EmployeeJobInfo");
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
        public int Add(EmployeeJobInfo model, SqlTransaction trans)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into SEP_EmployeeJobInfo(");
            strSql.Append("sysid,companyid,companyName,departmentid,departmentName,groupid,groupName,workPlace,joinDate,joinJob,directorName,directorJob,joinjobID,memo,selfIntroduction,objective,workingExperience,educationalBackground,languagesAndDialect)");
            strSql.Append(" values (");
            strSql.Append("@sysid,@companyid,@companyName,@departmentid,@departmentName,@groupid,@groupName,@workPlace,@joinDate,@joinJob,@directorName,@directorJob,@joinjobID,@memo,@selfIntroduction,@objective,@workingExperience,@educationalBackground,@languagesAndDialect)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@sysid", SqlDbType.Int,4),
					new SqlParameter("@companyid", SqlDbType.Int,4),
					new SqlParameter("@companyName", SqlDbType.NVarChar),
					new SqlParameter("@departmentid", SqlDbType.Int,4),
					new SqlParameter("@departmentName", SqlDbType.NVarChar),
					new SqlParameter("@groupid", SqlDbType.Int,4),
					new SqlParameter("@groupName", SqlDbType.NVarChar),
					new SqlParameter("@workPlace", SqlDbType.NVarChar),
					new SqlParameter("@joinDate", SqlDbType.SmallDateTime),
					new SqlParameter("@joinJob", SqlDbType.NVarChar),
					new SqlParameter("@directorName", SqlDbType.NVarChar),
					new SqlParameter("@directorJob", SqlDbType.NVarChar),
					new SqlParameter("@joinjobID", SqlDbType.Int,4),
					new SqlParameter("@memo", SqlDbType.NVarChar),
					new SqlParameter("@selfIntroduction", SqlDbType.NVarChar),
					new SqlParameter("@objective", SqlDbType.NVarChar),
					new SqlParameter("@workingExperience", SqlDbType.NVarChar),
					new SqlParameter("@educationalBackground", SqlDbType.NVarChar),
					new SqlParameter("@languagesAndDialect", SqlDbType.NChar)};
            parameters[0].Value = model.sysid;
            parameters[1].Value = model.companyid;
            parameters[2].Value = model.companyName;
            parameters[3].Value = model.departmentid;
            parameters[4].Value = model.departmentName;
            parameters[5].Value = model.groupid;
            parameters[6].Value = model.groupName;
            parameters[7].Value = model.workPlace;
            parameters[8].Value = model.joinDate;
            parameters[9].Value = model.joinJob;
            parameters[10].Value = model.directorName;
            parameters[11].Value = model.directorJob;
            parameters[12].Value = model.joinjobID;
            parameters[13].Value = model.memo;
            parameters[14].Value = model.selfIntroduction;
            parameters[15].Value = model.objective;
            parameters[16].Value = model.workingExperience;
            parameters[17].Value = model.educationalBackground;
            parameters[18].Value = model.languagesAndDialect;

            object obj = DbHelperSQL.GetSingle(strSql.ToString(), trans.Connection, trans, parameters);
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
        public void Update(EmployeeJobInfo model, SqlTransaction trans)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update SEP_EmployeeJobInfo set ");
            strSql.Append("sysid=@sysid,");
            strSql.Append("companyid=@companyid,");
            strSql.Append("companyName=@companyName,");
            strSql.Append("departmentid=@departmentid,");
            strSql.Append("departmentName=@departmentName,");
            strSql.Append("groupid=@groupid,");
            strSql.Append("groupName=@groupName,");
            strSql.Append("workPlace=@workPlace,");
            strSql.Append("joinDate=@joinDate,");
            strSql.Append("joinJob=@joinJob,");
            strSql.Append("directorName=@directorName,");
            strSql.Append("directorJob=@directorJob,");
            strSql.Append("joinjobID=@joinjobID,");
            strSql.Append("memo=@memo,");
            strSql.Append("selfIntroduction=@selfIntroduction,");
            strSql.Append("objective=@objective,");
            strSql.Append("workingExperience=@workingExperience,");
            strSql.Append("educationalBackground=@educationalBackground,");
            strSql.Append("languagesAndDialect=@languagesAndDialect");
            strSql.Append(" where id=@id");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4),
					new SqlParameter("@sysid", SqlDbType.Int,4),
					new SqlParameter("@companyid", SqlDbType.Int,4),
					new SqlParameter("@companyName", SqlDbType.NVarChar),
					new SqlParameter("@departmentid", SqlDbType.Int,4),
					new SqlParameter("@departmentName", SqlDbType.NVarChar),
					new SqlParameter("@groupid", SqlDbType.Int,4),
					new SqlParameter("@groupName", SqlDbType.NVarChar),
					new SqlParameter("@workPlace", SqlDbType.NVarChar),
					new SqlParameter("@joinDate", SqlDbType.SmallDateTime),
					new SqlParameter("@joinJob", SqlDbType.NVarChar),
					new SqlParameter("@directorName", SqlDbType.NVarChar),
					new SqlParameter("@directorJob", SqlDbType.NVarChar),
					new SqlParameter("@joinjobID", SqlDbType.Int,4),
					new SqlParameter("@memo", SqlDbType.NVarChar),
					new SqlParameter("@selfIntroduction", SqlDbType.NVarChar),
					new SqlParameter("@objective", SqlDbType.NVarChar),
					new SqlParameter("@workingExperience", SqlDbType.NVarChar),
					new SqlParameter("@educationalBackground", SqlDbType.NVarChar),
					new SqlParameter("@languagesAndDialect", SqlDbType.NChar)};
            parameters[0].Value = model.id;
            parameters[1].Value = model.sysid;
            parameters[2].Value = model.companyid;
            parameters[3].Value = model.companyName;
            parameters[4].Value = model.departmentid;
            parameters[5].Value = model.departmentName;
            parameters[6].Value = model.groupid;
            parameters[7].Value = model.groupName;
            parameters[8].Value = model.workPlace;
            parameters[9].Value = model.joinDate;
            parameters[10].Value = model.joinJob;
            parameters[11].Value = model.directorName;
            parameters[12].Value = model.directorJob;
            parameters[13].Value = model.joinjobID;
            parameters[14].Value = model.memo;
            parameters[15].Value = model.selfIntroduction;
            parameters[16].Value = model.objective;
            parameters[17].Value = model.workingExperience;
            parameters[18].Value = model.educationalBackground;
            parameters[19].Value = model.languagesAndDialect;

            DbHelperSQL.ExecuteSql(strSql.ToString(), trans.Connection, trans, parameters);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public void Delete(int id, SqlTransaction trans)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete SEP_EmployeeJobInfo ");
            strSql.Append(" where id=@id");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)
				};
            parameters[0].Value = id;
            DbHelperSQL.ExecuteSql(strSql.ToString(), trans.Connection, trans, parameters);
        }

        /// <summary>
        /// 删除数据
        /// </summary>
        public void DeleteBySysUserID(int sysid, SqlTransaction trans)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete SEP_EmployeeJobInfo ");
            strSql.Append(" where sysid=@sysid");
            SqlParameter[] parameters = {
					new SqlParameter("@sysid", SqlDbType.Int,4)
				};
            parameters[0].Value = sysid;
            DbHelperSQL.ExecuteSql(strSql.ToString(), trans.Connection, trans, parameters);
        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public EmployeeJobInfo GetModel(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from SEP_EmployeeJobInfo ");
            strSql.Append(" where id=@id");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)};
            parameters[0].Value = id;
            EmployeeJobInfo model = new EmployeeJobInfo();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            model.id = id;
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["sysid"].ToString() != "")
                {
                    model.sysid = int.Parse(ds.Tables[0].Rows[0]["sysid"].ToString());
                }
                if (ds.Tables[0].Rows[0]["companyid"].ToString() != "")
                {
                    model.companyid = int.Parse(ds.Tables[0].Rows[0]["companyid"].ToString());
                }
                model.companyName = ds.Tables[0].Rows[0]["companyName"].ToString();
                if (ds.Tables[0].Rows[0]["departmentid"].ToString() != "")
                {
                    model.departmentid = int.Parse(ds.Tables[0].Rows[0]["departmentid"].ToString());
                }
                model.departmentName = ds.Tables[0].Rows[0]["departmentName"].ToString();
                if (ds.Tables[0].Rows[0]["groupid"].ToString() != "")
                {
                    model.groupid = int.Parse(ds.Tables[0].Rows[0]["groupid"].ToString());
                }
                model.groupName = ds.Tables[0].Rows[0]["groupName"].ToString();
                model.workPlace = ds.Tables[0].Rows[0]["workPlace"].ToString();
                if (ds.Tables[0].Rows[0]["joinDate"].ToString() != "")
                {
                    model.joinDate = DateTime.Parse(ds.Tables[0].Rows[0]["joinDate"].ToString());
                }
                model.joinJob = ds.Tables[0].Rows[0]["joinJob"].ToString();
                model.directorName = ds.Tables[0].Rows[0]["directorName"].ToString();
                model.directorJob = ds.Tables[0].Rows[0]["directorJob"].ToString();
                if (ds.Tables[0].Rows[0]["joinjobID"].ToString() != "")
                {
                    model.joinjobID = int.Parse(ds.Tables[0].Rows[0]["joinjobID"].ToString());
                }
                model.memo = ds.Tables[0].Rows[0]["memo"].ToString();
                model.selfIntroduction = ds.Tables[0].Rows[0]["selfIntroduction"].ToString();
                model.objective = ds.Tables[0].Rows[0]["objective"].ToString();
                model.workingExperience = ds.Tables[0].Rows[0]["workingExperience"].ToString();
                model.educationalBackground = ds.Tables[0].Rows[0]["educationalBackground"].ToString();
                model.languagesAndDialect = ds.Tables[0].Rows[0]["languagesAndDialect"].ToString();
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
            strSql.Append("select [id],[sysid],[companyid],[companyName],[departmentid],[departmentName],[groupid],[groupName],[workPlace],[joinDate],[joinJob],[directorName],[directorJob],[joinjobID],[memo],[selfIntroduction],[objective],[workingExperience],[educationalBackground],[languagesAndDialect] ");
            strSql.Append(" FROM SEP_EmployeeJobInfo ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }

        public EmployeeJobInfo getModelBySysId(int sysid)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 * from SEP_EmployeeJobInfo ");
            strSql.Append(" where sysid=@sysid order by id desc ");
            SqlParameter[] parameters = {
					new SqlParameter("@sysid", SqlDbType.Int,4)};
            parameters[0].Value = sysid;

            EmployeeJobInfo model = new EmployeeJobInfo();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["id"].ToString() != "")
                {
                    model.id = int.Parse(ds.Tables[0].Rows[0]["id"].ToString());
                }
                if (ds.Tables[0].Rows[0]["joinDate"].ToString() != "")
                {
                    model.joinDate = DateTime.Parse(ds.Tables[0].Rows[0]["joinDate"].ToString());
                }
                model.joinJob = ds.Tables[0].Rows[0]["joinJob"].ToString();
                model.directorName = ds.Tables[0].Rows[0]["directorName"].ToString();
                model.directorJob = ds.Tables[0].Rows[0]["directorJob"].ToString();
                if (ds.Tables[0].Rows[0]["joinjobID"].ToString() != "")
                {
                    model.joinjobID = int.Parse(ds.Tables[0].Rows[0]["joinjobID"].ToString());
                }
                model.memo = ds.Tables[0].Rows[0]["memo"].ToString();
                if (ds.Tables[0].Rows[0]["sysid"].ToString() != "")
                {
                    model.sysid = int.Parse(ds.Tables[0].Rows[0]["sysid"].ToString());
                }
                if (ds.Tables[0].Rows[0]["companyid"].ToString() != "")
                {
                    model.companyid = int.Parse(ds.Tables[0].Rows[0]["companyid"].ToString());
                }
                model.companyName = ds.Tables[0].Rows[0]["companyName"].ToString();
                if (ds.Tables[0].Rows[0]["departmentid"].ToString() != "")
                {
                    model.departmentid = int.Parse(ds.Tables[0].Rows[0]["departmentid"].ToString());
                }
                model.departmentName = ds.Tables[0].Rows[0]["departmentName"].ToString();
                if (ds.Tables[0].Rows[0]["groupid"].ToString() != "")
                {
                    model.groupid = int.Parse(ds.Tables[0].Rows[0]["groupid"].ToString());
                }
                model.groupName = ds.Tables[0].Rows[0]["groupName"].ToString();
                model.workPlace = ds.Tables[0].Rows[0]["workPlace"].ToString();

                model.selfIntroduction = ds.Tables[0].Rows[0]["selfIntroduction"].ToString();
                model.objective = ds.Tables[0].Rows[0]["objective"].ToString();
                model.workingExperience = ds.Tables[0].Rows[0]["workingExperience"].ToString();
                model.educationalBackground = ds.Tables[0].Rows[0]["educationalBackground"].ToString();
                model.languagesAndDialect = ds.Tables[0].Rows[0]["languagesAndDialect"].ToString();

                return model;
            }
            else
            {
                return null;
            }
        }

        #endregion  成员方法
    }
}
