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
    public class EmpWorkExpProvider
    {
        public EmpWorkExpProvider()
        { }
        #region  成员方法
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(ESP.HumanResource.Entity.EmpWorkExpInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("INSERT INTO sep_empWorkExp(");
            strSql.Append("UserId,Company,Dept,Position,JoinDate,Email,Skill,Experience,ServeYear,Director)");
            strSql.Append(" VALUES (");
            strSql.Append("@UserId,@Company,@Dept,@Position,@JoinDate,@Email,@Skill,@Experience,@ServeYear,@Director)");
            strSql.Append(";SELECT @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@UserId", SqlDbType.Int,4),
					new SqlParameter("@Company", SqlDbType.NVarChar),
					new SqlParameter("@Dept", SqlDbType.NVarChar),
					new SqlParameter("@Position", SqlDbType.NVarChar),
					new SqlParameter("@JoinDate", SqlDbType.DateTime),
					new SqlParameter("@Email", SqlDbType.NVarChar),
					new SqlParameter("@Skill", SqlDbType.NVarChar),
                    new SqlParameter("@Experience", SqlDbType.NVarChar),
					new SqlParameter("@ServeYear", SqlDbType.NVarChar),
					new SqlParameter("@Director", SqlDbType.NVarChar)           
                                        };
            parameters[0].Value = model.UserId;
            parameters[1].Value = model.Company;
            parameters[2].Value = model.Dept;
            parameters[3].Value = model.Position;
            parameters[4].Value = model.JoinDate;
            parameters[5].Value = model.Email;
            parameters[6].Value = model.Skill;
            parameters[7].Value = model.Experience;
            parameters[8].Value = model.ServeYear;
            parameters[9].Value = model.Director;


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
        public void Update(ESP.HumanResource.Entity.EmpWorkExpInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("UPDATE sep_empWorkExp SET ");
            strSql.Append("UserId=@UserId,Company=@Company,Dept=@Dept,Position=@Position,JoinDate=@JoinDate,Email=@Email,Skill=@Skill,Experience=@Experience,ServeYear=@ServeYear,Director=@Director");
            strSql.Append(" WHERE WorkId=@WorkId");
            SqlParameter[] parameters = {
					new SqlParameter("@UserId", SqlDbType.Int,4),
					new SqlParameter("@Company", SqlDbType.NVarChar),
					new SqlParameter("@Dept", SqlDbType.NVarChar),
					new SqlParameter("@Position", SqlDbType.NVarChar),
					new SqlParameter("@JoinDate", SqlDbType.DateTime),
					new SqlParameter("@Email", SqlDbType.NVarChar),
					new SqlParameter("@Skill", SqlDbType.NVarChar),
                    new SqlParameter("@Experience", SqlDbType.NVarChar),
					new SqlParameter("@ServeYear", SqlDbType.NVarChar),
					new SqlParameter("@Director", SqlDbType.NVarChar),
                    new SqlParameter("@WorkId", SqlDbType.Int,4)           
                                        };
            parameters[0].Value = model.UserId;
            parameters[1].Value = model.Company;
            parameters[2].Value = model.Dept;
            parameters[3].Value = model.Position;
            parameters[4].Value = model.JoinDate;
            parameters[5].Value = model.Email;
            parameters[6].Value = model.Skill;
            parameters[7].Value = model.Experience;
            parameters[8].Value = model.ServeYear;
            parameters[9].Value = model.Director;
            parameters[10].Value = model.WorkId;

            DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public void Delete(int WorkId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete sep_empWorkExp ");
            strSql.Append(" where WorkId=@WorkId");
            SqlParameter[] parameters = {
					new SqlParameter("@WorkId", SqlDbType.Int,4)
				};
            parameters[0].Value = WorkId;
            DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public ESP.HumanResource.Entity.EmpWorkExpInfo GetModel(int WorkId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM sep_empWorkExp ");
            strSql.Append(" WHERE WorkId=@WorkId");
            SqlParameter[] parameters = {
					new SqlParameter("@WorkId", SqlDbType.Int,4)};
            parameters[0].Value = WorkId;
            ESP.HumanResource.Entity.EmpWorkExpInfo model = new ESP.HumanResource.Entity.EmpWorkExpInfo();
            return CBO.FillObject<EmpWorkExpInfo>(DbHelperSQL.Query(strSql.ToString(), parameters));
            
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public IList<ESP.HumanResource.Entity.EmpWorkExpInfo> GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * ");
            strSql.Append(" FROM sep_empWorkExp ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return CBO.FillCollection<EmpWorkExpInfo>(DbHelperSQL.Query(strSql.ToString()));
        }
        #endregion  成员方法
    }
}
