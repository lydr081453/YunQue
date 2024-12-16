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
    public class XML_VersionLogDataProvider
    {
        public XML_VersionLogDataProvider()
        { }
        #region  成员方法
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from XML_VersionLog");
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)};
            parameters[0].Value = ID;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(XML_VersionLog model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into XML_VersionLog(");
            strSql.Append("VersionID,Name,TableName,Url,Content,Version,InsertUser,InsertTime,InsertIP,UpdateUser,UpdateTime,UpdateIP,ClassID,XML)");
            strSql.Append(" values (");
            strSql.Append("@VersionID,@Name,@TableName,@Url,@Content,@Version,@InsertUser,@InsertTime,@InsertIP,@UpdateUser,@UpdateTime,@UpdateIP,@ClassID,@XML)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@VersionID", SqlDbType.Int,4),
					new SqlParameter("@Name", SqlDbType.NVarChar,50),
					new SqlParameter("@TableName", SqlDbType.NVarChar,50),
					new SqlParameter("@Url", SqlDbType.NVarChar,50),
					new SqlParameter("@Content", SqlDbType.NText),
					new SqlParameter("@Version", SqlDbType.NVarChar,100),
					new SqlParameter("@InsertUser", SqlDbType.NVarChar,100),
					new SqlParameter("@InsertTime", SqlDbType.NVarChar,100),
					new SqlParameter("@InsertIP", SqlDbType.NVarChar,100),
					new SqlParameter("@UpdateUser", SqlDbType.NVarChar,100),
					new SqlParameter("@UpdateTime", SqlDbType.NVarChar,100),
					new SqlParameter("@UpdateIP", SqlDbType.NVarChar,100),
					new SqlParameter("@ClassID", SqlDbType.Int,4),
					new SqlParameter("@XML", SqlDbType.NText)};
            parameters[0].Value = model.VersionID;
            parameters[1].Value = model.Name;
            parameters[2].Value = model.TableName;
            parameters[3].Value = model.Url;
            parameters[4].Value = model.Content;
            parameters[5].Value = model.Version;
            parameters[6].Value = model.InsertUser;
            parameters[7].Value = model.InsertTime;
            parameters[8].Value = model.InsertIP;
            parameters[9].Value = model.UpdateUser;
            parameters[10].Value = model.UpdateTime;
            parameters[11].Value = model.UpdateIP;
            parameters[12].Value = model.ClassID;
            parameters[13].Value = model.XML;

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
        public void Update(XML_VersionLog model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update XML_VersionLog set ");
            strSql.Append("VersionID=@VersionID,");
            strSql.Append("Name=@Name,");
            strSql.Append("TableName=@TableName,");
            strSql.Append("Url=@Url,");
            strSql.Append("Content=@Content,");
            strSql.Append("Version=@Version,");
            strSql.Append("InsertUser=@InsertUser,");
            strSql.Append("InsertTime=@InsertTime,");
            strSql.Append("InsertIP=@InsertIP,");
            strSql.Append("UpdateUser=@UpdateUser,");
            strSql.Append("UpdateTime=@UpdateTime,");
            strSql.Append("UpdateIP=@UpdateIP,");
            strSql.Append("ClassID=@ClassID,");
            strSql.Append("XML=@XML");
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4),
					new SqlParameter("@VersionID", SqlDbType.Int,4),
					new SqlParameter("@Name", SqlDbType.NVarChar,50),
					new SqlParameter("@TableName", SqlDbType.NVarChar,50),
					new SqlParameter("@Url", SqlDbType.NVarChar,50),
					new SqlParameter("@Content", SqlDbType.NText),
					new SqlParameter("@Version", SqlDbType.NVarChar,100),
					new SqlParameter("@InsertUser", SqlDbType.NVarChar,100),
					new SqlParameter("@InsertTime", SqlDbType.NVarChar,100),
					new SqlParameter("@InsertIP", SqlDbType.NVarChar,100),
					new SqlParameter("@UpdateUser", SqlDbType.NVarChar,100),
					new SqlParameter("@UpdateTime", SqlDbType.NVarChar,100),
					new SqlParameter("@UpdateIP", SqlDbType.NVarChar,100),
					new SqlParameter("@ClassID", SqlDbType.Int,4),
					new SqlParameter("@XML", SqlDbType.NText)};
            parameters[0].Value = model.ID;
            parameters[1].Value = model.VersionID;
            parameters[2].Value = model.Name;
            parameters[3].Value = model.TableName;
            parameters[4].Value = model.Url;
            parameters[5].Value = model.Content;
            parameters[6].Value = model.Version;
            parameters[7].Value = model.InsertUser;
            parameters[8].Value = model.InsertTime;
            parameters[9].Value = model.InsertIP;
            parameters[10].Value = model.UpdateUser;
            parameters[11].Value = model.UpdateTime;
            parameters[12].Value = model.UpdateIP;
            parameters[13].Value = model.ClassID;
            parameters[14].Value = model.XML;

            DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public void Delete(int ID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from XML_VersionLog ");
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)};
            parameters[0].Value = ID;

            DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public XML_VersionLog GetModel(int ID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 ID,VersionID,Name,TableName,Url,Content,Version,InsertUser,InsertTime,InsertIP,UpdateUser,UpdateTime,UpdateIP,ClassID,XML from XML_VersionLog ");
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)};
            parameters[0].Value = ID;

            XML_VersionLog model = new XML_VersionLog();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["ID"].ToString() != "")
                {
                    model.ID = int.Parse(ds.Tables[0].Rows[0]["ID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["VersionID"].ToString() != "")
                {
                    model.VersionID = int.Parse(ds.Tables[0].Rows[0]["VersionID"].ToString());
                }
                model.Name = ds.Tables[0].Rows[0]["Name"].ToString();
                model.TableName = ds.Tables[0].Rows[0]["TableName"].ToString();
                model.Url = ds.Tables[0].Rows[0]["Url"].ToString();
                model.Content = ds.Tables[0].Rows[0]["Content"].ToString();
                model.Version = ds.Tables[0].Rows[0]["Version"].ToString();
                model.InsertUser = ds.Tables[0].Rows[0]["InsertUser"].ToString();
                model.InsertTime = ds.Tables[0].Rows[0]["InsertTime"].ToString();
                model.InsertIP = ds.Tables[0].Rows[0]["InsertIP"].ToString();
                model.UpdateUser = ds.Tables[0].Rows[0]["UpdateUser"].ToString();
                model.UpdateTime = ds.Tables[0].Rows[0]["UpdateTime"].ToString();
                model.UpdateIP = ds.Tables[0].Rows[0]["UpdateIP"].ToString();
                if (ds.Tables[0].Rows[0]["ClassID"].ToString() != "")
                {
                    model.ClassID = int.Parse(ds.Tables[0].Rows[0]["ClassID"].ToString());
                }
                model.XML = ds.Tables[0].Rows[0]["XML"].ToString();
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
            strSql.Append("select ID,VersionID,Name,TableName,Url,Content,Version,InsertUser,InsertTime,InsertIP,UpdateUser,UpdateTime,UpdateIP,ClassID,XML ");
            strSql.Append(" FROM XML_VersionLog ");
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
            strSql.Append(" ID,VersionID,Name,TableName,Url,Content,Version,InsertUser,InsertTime,InsertIP,UpdateUser,UpdateTime,UpdateIP,ClassID,XML ");
            strSql.Append(" FROM XML_VersionLog ");
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
            parameters[0].Value = "XML_VersionLog";
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
