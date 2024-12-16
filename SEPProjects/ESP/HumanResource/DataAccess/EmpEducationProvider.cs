using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESP.HumanResource.Common;
using System.Data.SqlClient;
using System.Data;
using ESP.HumanResource.Entity;
using ESP.HumanResource.Utilities;

namespace ESP.HumanResource.DataAccess
{
    public class EmpEducationProvider
    {
        public EmpEducationProvider()
        { }
        #region  成员方法
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(ESP.HumanResource.Entity.EmpEducationInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("INSERT INTO sep_EmpEducation(");
            strSql.Append("UserId,School,BeginDate,EndDate,Degree,Profession)");
            strSql.Append(" VALUES (");
            strSql.Append("@UserId,@School,@BeginDate,@EndDate,@Degree,@Profession)");
            strSql.Append(";SELECT @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@UserId", SqlDbType.Int,4),
					new SqlParameter("@School", SqlDbType.NVarChar),
					new SqlParameter("@BeginDate", SqlDbType.DateTime),
					new SqlParameter("@EndDate", SqlDbType.DateTime),
					new SqlParameter("@Degree", SqlDbType.NVarChar),
					new SqlParameter("@Profession", SqlDbType.NVarChar)
                                        };
            parameters[0].Value = model.UserId;
            parameters[1].Value = model.School;
            parameters[2].Value = model.BeginDate;
            parameters[3].Value = model.EndDate;
            parameters[4].Value = model.Degree;
            parameters[5].Value = model.Profession;


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
        public void Update(ESP.HumanResource.Entity.EmpEducationInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("UPDATE sep_EmpEducation SET ");
            strSql.Append("UserId=@UserId,");
            strSql.Append("School=@School,BeginDate=@BeginDate,EndDate=@EndDate,Degree=@Degree,Profession=@Profession");
            strSql.Append(" WHERE EducationId=@EducationId");
            SqlParameter[] parameters = {
					new SqlParameter("@UserId", SqlDbType.Int,4),
					new SqlParameter("@School", SqlDbType.NVarChar),
					new SqlParameter("@BeginDate", SqlDbType.DateTime),
					new SqlParameter("@EndDate", SqlDbType.DateTime),
					new SqlParameter("@Degree", SqlDbType.NVarChar),
					new SqlParameter("@Profession", SqlDbType.NVarChar),
                    new SqlParameter("@EducationId", SqlDbType.Int,4)
                                        };
            parameters[0].Value = model.UserId;
            parameters[1].Value = model.School;
            parameters[2].Value = model.BeginDate;
            parameters[3].Value = model.EndDate;
            parameters[4].Value = model.Degree;
            parameters[5].Value = model.Profession;
            parameters[6].Value = model.EducationId;
         
            DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public void Delete(int EducationId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete sep_EmpEducation ");
            strSql.Append(" where EducationId=@EducationId");
            SqlParameter[] parameters = {
					new SqlParameter("@EducationId", SqlDbType.Int,4)
				};
            parameters[0].Value = EducationId;
            DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public ESP.HumanResource.Entity.EmpEducationInfo GetModel(int EducationId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM sep_EmpEducation ");
            strSql.Append(" WHERE EducationId=@EducationId");
            SqlParameter[] parameters = {
					new SqlParameter("@EducationId", SqlDbType.Int,4)};
            parameters[0].Value = EducationId;
            ESP.HumanResource.Entity.EmpEducationInfo model = new ESP.HumanResource.Entity.EmpEducationInfo();
            return CBO.FillObject<EmpEducationInfo>(DbHelperSQL.Query(strSql.ToString(), parameters));
            
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public IList<ESP.HumanResource.Entity.EmpEducationInfo> GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * ");
            strSql.Append(" FROM sep_EmpEducation ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return CBO.FillCollection<EmpEducationInfo>(DbHelperSQL.Query(strSql.ToString()));
        }
        #endregion  成员方法
    }
}
