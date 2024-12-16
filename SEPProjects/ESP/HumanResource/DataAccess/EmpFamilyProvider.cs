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
    public class EmpFamilyProvider
    {
        public EmpFamilyProvider()
        { }
        #region  成员方法
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(ESP.HumanResource.Entity.EmpFamilyInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("INSERT INTO sep_empfamily(");
            strSql.Append("UserId,MemberName,Phone,Email,Age,Relation,Company)");
            strSql.Append(" VALUES (");
            strSql.Append("@UserId,@MemberName,@Phone,@Email,@Age,@Relation,@Company)");
            strSql.Append(";SELECT @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@UserId", SqlDbType.Int,4),
					new SqlParameter("@MemberName", SqlDbType.NVarChar),
					new SqlParameter("@Phone", SqlDbType.NVarChar),
					new SqlParameter("@Email", SqlDbType.NVarChar),
					new SqlParameter("@Age", SqlDbType.Int,4),
					new SqlParameter("@Relation", SqlDbType.NVarChar),
					new SqlParameter("@Company", SqlDbType.NVarChar)           
                                        };
            parameters[0].Value = model.UserId;
            parameters[1].Value = model.MemberName;
            parameters[2].Value = model.Phone;
            parameters[3].Value = model.Email;
            parameters[4].Value = model.Age;
            parameters[5].Value = model.Relation;
            parameters[6].Value = model.Company;


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
        public void Update(ESP.HumanResource.Entity.EmpFamilyInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("UPDATE sep_empfamily SET ");
            strSql.Append("UserId=@UserId,");
            strSql.Append("MemberName=@MemberName,Phone=@Phone,Email=@Email,Age=@Age,Relation=@Relation,Company=@Company");
            strSql.Append(" WHERE MemberId=@MemberId");
            SqlParameter[] parameters = {
					new SqlParameter("@UserId", SqlDbType.Int,4),
					new SqlParameter("@MemberName", SqlDbType.NVarChar),
					new SqlParameter("@Phone", SqlDbType.NVarChar),
					new SqlParameter("@Email", SqlDbType.NVarChar),
					new SqlParameter("@Age", SqlDbType.Int),
					new SqlParameter("@Relation", SqlDbType.NVarChar),
					new SqlParameter("@Company", SqlDbType.NVarChar),
                    new SqlParameter("@MemberId", SqlDbType.Int)
                                        };
            parameters[0].Value = model.UserId;
            parameters[1].Value = model.MemberName;
            parameters[2].Value = model.Phone;
            parameters[3].Value = model.Email;
            parameters[4].Value = model.Age;
            parameters[5].Value = model.Relation;
            parameters[6].Value = model.Company;
            parameters[7].Value = model.MemberId;
         
            DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public void Delete(int memberid)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete sep_empfamily ");
            strSql.Append(" where memberid=@memberid");
            SqlParameter[] parameters = {
					new SqlParameter("@memberid", SqlDbType.Int,4)
				};
            parameters[0].Value = memberid;
            DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public ESP.HumanResource.Entity.EmpFamilyInfo GetModel(int memberid)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM sep_empfamily ");
            strSql.Append(" WHERE memberid=@memberid");
            SqlParameter[] parameters = {
					new SqlParameter("@memberid", SqlDbType.Int,4)};
            parameters[0].Value = memberid;
            ESP.HumanResource.Entity.EmpFamilyInfo model = new ESP.HumanResource.Entity.EmpFamilyInfo();
            return CBO.FillObject<EmpFamilyInfo>(DbHelperSQL.Query(strSql.ToString(), parameters));
            
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public IList<ESP.HumanResource.Entity.EmpFamilyInfo> GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * ");
            strSql.Append(" FROM sep_empfamily ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return CBO.FillCollection<EmpFamilyInfo>(DbHelperSQL.Query(strSql.ToString()));
        }
        #endregion  成员方法
    }
}
