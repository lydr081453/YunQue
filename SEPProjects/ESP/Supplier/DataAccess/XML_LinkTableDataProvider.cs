using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESP.Supplier.Common;
using ESP.Supplier.Entity;
using System.Data;
using System.Data.SqlClient;

namespace ESP.Supplier.DataAccess
{
    public class XML_LinkTableDataProvider
    {
        public XML_LinkTableDataProvider()
        { }
        #region  成员方法
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from XML_LinkTable");
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)};
            parameters[0].Value = ID;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(XML_LinkTable model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into XML_LinkTable(");
            strSql.Append("Key,Value,ParentName,TableName,Version,Name)");
            strSql.Append(" values (");
            strSql.Append("@Key,@Value,@ParentName,@TableName,@Version,@Name)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@Key", SqlDbType.NVarChar,50),
					new SqlParameter("@Value", SqlDbType.NVarChar,200),
					new SqlParameter("@ParentName", SqlDbType.NVarChar,200),
					new SqlParameter("@TableName", SqlDbType.NVarChar,200),
					new SqlParameter("@Version", SqlDbType.NVarChar,100),
					new SqlParameter("@Name", SqlDbType.NVarChar,50)};
            parameters[0].Value = model.Key;
            parameters[1].Value = model.Value;
            parameters[2].Value = model.ParentName;
            parameters[3].Value = model.TableName;
            parameters[4].Value = model.Version;
            parameters[5].Value = model.Name;

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
        public void Update(XML_LinkTable model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update XML_LinkTable set ");
            strSql.Append("Key=@Key,");
            strSql.Append("Value=@Value,");
            strSql.Append("ParentName=@ParentName,");
            strSql.Append("TableName=@TableName,");
            strSql.Append("Version=@Version,");
            strSql.Append("Name=@Name");
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4),
					new SqlParameter("@Key", SqlDbType.NVarChar,50),
					new SqlParameter("@Value", SqlDbType.NVarChar,200),
					new SqlParameter("@ParentName", SqlDbType.NVarChar,200),
					new SqlParameter("@TableName", SqlDbType.NVarChar,200),
					new SqlParameter("@Version", SqlDbType.NVarChar,100),
					new SqlParameter("@Name", SqlDbType.NVarChar,50)};
            parameters[0].Value = model.ID;
            parameters[1].Value = model.Key;
            parameters[2].Value = model.Value;
            parameters[3].Value = model.ParentName;
            parameters[4].Value = model.TableName;
            parameters[5].Value = model.Version;
            parameters[6].Value = model.Name;

            DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public void Delete(int ID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from XML_LinkTable ");
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)};
            parameters[0].Value = ID;

            DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public XML_LinkTable GetModel(int ID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 ID,Key,Value,ParentName,TableName,Version,Name from XML_LinkTable ");
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)};
            parameters[0].Value = ID;

            XML_LinkTable model = new XML_LinkTable();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["ID"].ToString() != "")
                {
                    model.ID = int.Parse(ds.Tables[0].Rows[0]["ID"].ToString());
                }
                model.Key = ds.Tables[0].Rows[0]["Key"].ToString();
                model.Value = ds.Tables[0].Rows[0]["Value"].ToString();
                model.ParentName = ds.Tables[0].Rows[0]["ParentName"].ToString();
                model.TableName = ds.Tables[0].Rows[0]["TableName"].ToString();
                model.Version = ds.Tables[0].Rows[0]["Version"].ToString();
                model.Name = ds.Tables[0].Rows[0]["Name"].ToString();
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
            strSql.Append("select ID,Key,Value,ParentName,TableName,Version,Name ");
            strSql.Append(" FROM XML_LinkTable ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 获得前几行数据
        /// </summary>
        public DataSet GetList(int Top, string strWhere, string filedOrder)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ");
            if (Top > 0)
            {
                strSql.Append(" top " + Top.ToString());
            }
            strSql.Append(" ID,Key,Value,ParentName,TableName,Version,Name ");
            strSql.Append(" FROM XML_LinkTable ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return DbHelperSQL.Query(strSql.ToString());
        }

        /*
        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        public DataSet GetList(int PageSize,int PageIndex,string strWhere)
        {
            SqlParameter[] parameters = {
                    new SqlParameter("@tblName", SqlDbType.VarChar, 255),
                    new SqlParameter("@fldName", SqlDbType.VarChar, 255),
                    new SqlParameter("@PageSize", SqlDbType.Int),
                    new SqlParameter("@PageIndex", SqlDbType.Int),
                    new SqlParameter("@IsReCount", SqlDbType.Bit),
                    new SqlParameter("@OrderType", SqlDbType.Bit),
                    new SqlParameter("@strWhere", SqlDbType.VarChar,1000),
                    };
            parameters[0].Value = "XML_LinkTable";
            parameters[1].Value = "ID";
            parameters[2].Value = PageSize;
            parameters[3].Value = PageIndex;
            parameters[4].Value = 0;
            parameters[5].Value = 0;
            parameters[6].Value = strWhere;	
            return DbHelperSQL.RunProcedure("UP_GetRecordByPage",parameters,"ds");
        }*/

        #endregion  成员方法
    }
}
