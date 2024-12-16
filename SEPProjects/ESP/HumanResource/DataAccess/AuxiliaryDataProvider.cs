using System;
using System.Collections.Generic;
using System.Web;
using System.Data.SqlClient;
using ESP.HumanResource.Common;
using System.Data;
using System.Text;
using ESP.HumanResource.Entity;

namespace ESP.HumanResource.DataAccess
{
    public class AuxiliaryDataProvider
    {
        public AuxiliaryDataProvider()
        { }
        #region  成员方法

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(AuxiliaryInfo model, SqlConnection conn, SqlTransaction trans)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into sep_Auxiliary(");
            strSql.Append("auxiliaryName,Description,isDisable,CompanyID,CompanyName,Apply)");
            strSql.Append(" values (");
            strSql.Append("@auxiliaryName,@Description,@isDisable,@CompanyID,@CompanyName,@Apply)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@auxiliaryName", SqlDbType.NVarChar,50),
					new SqlParameter("@Description", SqlDbType.NVarChar,200),
					new SqlParameter("@isDisable", SqlDbType.Bit,1),
                    new SqlParameter("@CompanyID",SqlDbType.Int,4),
                    new SqlParameter("@CompanyName",SqlDbType.NVarChar,200),
                    new SqlParameter("@Apply",SqlDbType.Int,4)};
            parameters[0].Value = model.auxiliaryName;
            parameters[1].Value = model.Description;
            parameters[2].Value = model.isDisable;
            parameters[3].Value = model.companyID;
            parameters[4].Value = model.companyName;
            parameters[5].Value = model.apply;

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
        public int Update(AuxiliaryInfo model, SqlConnection conn, SqlTransaction trans)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update sep_Auxiliary set ");
            strSql.Append("auxiliaryName=@auxiliaryName,");
            strSql.Append("Description=@Description,");
            strSql.Append("isDisable=@isDisable,");
            strSql.Append("CompanyID=@CompanyID,");
            strSql.Append("CompanyName=@CompanyName,");
            strSql.Append("Apply=@Apply ");
            strSql.Append(" where id=@id ");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4),
					new SqlParameter("@auxiliaryName", SqlDbType.NVarChar,50),
					new SqlParameter("@Description", SqlDbType.NVarChar,200),
					new SqlParameter("@isDisable", SqlDbType.Bit,1),
                    new SqlParameter("@CompanyID",SqlDbType.Int,4),
                    new SqlParameter("@CompanyName",SqlDbType.NVarChar,200),
                    new SqlParameter("@Apply",SqlDbType.Int,4)};
            parameters[0].Value = model.id;
            parameters[1].Value = model.auxiliaryName;
            parameters[2].Value = model.Description;
            parameters[3].Value = model.isDisable;
            parameters[4].Value = model.companyID;
            parameters[5].Value = model.companyName;
            parameters[6].Value = model.apply;

            return DbHelperSQL.ExecuteSql(strSql.ToString(), conn, trans, parameters);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public int Delete(int id, SqlConnection conn, SqlTransaction trans)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete sep_Auxiliary ");
            strSql.Append(" where id=@id ");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)};
            parameters[0].Value = id;

           return DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public AuxiliaryInfo GetModel(int id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 * from sep_Auxiliary ");
            strSql.Append(" where id=@id ");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)};
            parameters[0].Value = id;

            AuxiliaryInfo model = new AuxiliaryInfo();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["id"].ToString() != "")
                {
                    model.id = int.Parse(ds.Tables[0].Rows[0]["id"].ToString());
                }
                model.auxiliaryName = ds.Tables[0].Rows[0]["auxiliaryName"].ToString();
                model.Description = ds.Tables[0].Rows[0]["Description"].ToString();
                if (ds.Tables[0].Rows[0]["isDisable"].ToString() != "")
                {
                    if ((ds.Tables[0].Rows[0]["isDisable"].ToString() == "1") || (ds.Tables[0].Rows[0]["isDisable"].ToString().ToLower() == "true"))
                    {
                        model.isDisable = true;
                    }
                    else
                    {
                        model.isDisable = false;
                    }
                }
                if (ds.Tables[0].Rows[0]["CompanyID"].ToString() != "")
                {
                    model.companyID = int.Parse(ds.Tables[0].Rows[0]["CompanyID"].ToString());
                }
                model.companyName = ds.Tables[0].Rows[0]["CompanyName"].ToString();
                if (ds.Tables[0].Rows[0]["Apply"].ToString() != "")
                {
                    model.apply = int.Parse(ds.Tables[0].Rows[0]["Apply"].ToString());
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
            strSql.Append("select * ");
            strSql.Append(" FROM sep_Auxiliary ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }      

        #endregion  成员方法
    }
}
