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
    public class SC_LogDataProvider
    {
        public SC_LogDataProvider()
        { }
        #region  成员方法
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int Id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from SC_Log");
            strSql.Append(" where Id=@Id ");
            SqlParameter[] parameters = {
					new SqlParameter("@Id", SqlDbType.Int,4)};
            parameters[0].Value = Id;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(SC_Log model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into SC_Log(");
            strSql.Append("Des,LogUserId,LogUserType,IpAddress,CreatTime,LastUpdateTime,Type,Status)");
            strSql.Append(" values (");
            strSql.Append("@Des,@LogUserId,@LogUserType,@IpAddress,@CreatTime,@LastUpdateTime,@Type,@Status)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@Des", SqlDbType.NVarChar,500),
					new SqlParameter("@LogUserId", SqlDbType.Int,4),
					new SqlParameter("@LogUserType", SqlDbType.Int,4),
					new SqlParameter("@IpAddress", SqlDbType.VarChar,20),
					new SqlParameter("@CreatTime", SqlDbType.SmallDateTime),
					new SqlParameter("@LastUpdateTime", SqlDbType.SmallDateTime),
					new SqlParameter("@Type", SqlDbType.Int,4),
					new SqlParameter("@Status", SqlDbType.Int,4)};
            parameters[0].Value = model.Des;
            parameters[1].Value = model.LogUserId;
            parameters[2].Value = model.LogUserType;
            parameters[3].Value = model.IpAddress;
            parameters[4].Value = model.CreatTime;
            parameters[5].Value = model.LastUpdateTime;
            parameters[6].Value = model.Type;
            parameters[7].Value = model.Status;

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
        /// 增加一条数据
        /// </summary>
        public int Add(SC_Log model, SqlTransaction trans, SqlConnection conn)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into SC_Log(");
            strSql.Append("Des,LogUserId,LogUserType,IpAddress,CreatTime,LastUpdateTime,Type,Status)");
            strSql.Append(" values (");
            strSql.Append("@Des,@LogUserId,@LogUserType,@IpAddress,@CreatTime,@LastUpdateTime,@Type,@Status)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@Des", SqlDbType.NVarChar,500),
					new SqlParameter("@LogUserId", SqlDbType.Int,4),
					new SqlParameter("@LogUserType", SqlDbType.Int,4),
					new SqlParameter("@IpAddress", SqlDbType.VarChar,20),
					new SqlParameter("@CreatTime", SqlDbType.SmallDateTime),
					new SqlParameter("@LastUpdateTime", SqlDbType.SmallDateTime),
					new SqlParameter("@Type", SqlDbType.Int,4),
					new SqlParameter("@Status", SqlDbType.Int,4)};
            parameters[0].Value = model.Des;
            parameters[1].Value = model.LogUserId;
            parameters[2].Value = model.LogUserType;
            parameters[3].Value = model.IpAddress;
            parameters[4].Value = model.CreatTime;
            parameters[5].Value = model.LastUpdateTime;
            parameters[6].Value = model.Type;
            parameters[7].Value = model.Status;

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
        public void Update(SC_Log model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update SC_Log set ");
            strSql.Append("Des=@Des,");
            strSql.Append("LogUserId=@LogUserId,");
            strSql.Append("LogUserType=@LogUserType,");
            strSql.Append("IpAddress=@IpAddress,");
            strSql.Append("CreatTime=@CreatTime,");
            strSql.Append("LastUpdateTime=@LastUpdateTime,");
            strSql.Append("Type=@Type,");
            strSql.Append("Status=@Status");
            strSql.Append(" where Id=@Id ");
            SqlParameter[] parameters = {
					new SqlParameter("@Id", SqlDbType.Int,4),
					new SqlParameter("@Des", SqlDbType.NVarChar,500),
					new SqlParameter("@LogUserId", SqlDbType.Int,4),
					new SqlParameter("@LogUserType", SqlDbType.Int,4),
					new SqlParameter("@IpAddress", SqlDbType.VarChar,20),
					new SqlParameter("@CreatTime", SqlDbType.SmallDateTime),
					new SqlParameter("@LastUpdateTime", SqlDbType.SmallDateTime),
					new SqlParameter("@Type", SqlDbType.Int,4),
					new SqlParameter("@Status", SqlDbType.Int,4)};
            parameters[0].Value = model.Id;
            parameters[1].Value = model.Des;
            parameters[2].Value = model.LogUserId;
            parameters[3].Value = model.LogUserType;
            parameters[4].Value = model.IpAddress;
            parameters[5].Value = model.CreatTime;
            parameters[6].Value = model.LastUpdateTime;
            parameters[7].Value = model.Type;
            parameters[8].Value = model.Status;

            DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public void Delete(int Id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from SC_Log ");
            strSql.Append(" where Id=@Id ");
            SqlParameter[] parameters = {
					new SqlParameter("@Id", SqlDbType.Int,4)};
            parameters[0].Value = Id;

            DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public SC_Log GetModel(int Id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 Id,Des,LogUserId,LogUserType,IpAddress,CreatTime,LastUpdateTime,Type,Status from SC_Log ");
            strSql.Append(" where Id=@Id ");
            SqlParameter[] parameters = {
					new SqlParameter("@Id", SqlDbType.Int,4)};
            parameters[0].Value = Id;

            SC_Log model = new SC_Log();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["Id"].ToString() != "")
                {
                    model.Id = int.Parse(ds.Tables[0].Rows[0]["Id"].ToString());
                }
                model.Des = ds.Tables[0].Rows[0]["Des"].ToString();
                if (ds.Tables[0].Rows[0]["LogUserId"].ToString() != "")
                {
                    model.LogUserId = int.Parse(ds.Tables[0].Rows[0]["LogUserId"].ToString());
                }
                if (ds.Tables[0].Rows[0]["LogUserType"].ToString() != "")
                {
                    model.LogUserType = int.Parse(ds.Tables[0].Rows[0]["LogUserType"].ToString());
                }
                model.IpAddress = ds.Tables[0].Rows[0]["IpAddress"].ToString();
                if (ds.Tables[0].Rows[0]["CreatTime"].ToString() != "")
                {
                    model.CreatTime = DateTime.Parse(ds.Tables[0].Rows[0]["CreatTime"].ToString());
                }
                if (ds.Tables[0].Rows[0]["LastUpdateTime"].ToString() != "")
                {
                    model.LastUpdateTime = DateTime.Parse(ds.Tables[0].Rows[0]["LastUpdateTime"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Type"].ToString() != "")
                {
                    model.Type = int.Parse(ds.Tables[0].Rows[0]["Type"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Status"].ToString() != "")
                {
                    model.Status = int.Parse(ds.Tables[0].Rows[0]["Status"].ToString());
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
            strSql.Append("select Id,Des,LogUserId,LogUserType,IpAddress,CreatTime,LastUpdateTime,Type,Status ");
            strSql.Append(" FROM SC_Log ");
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
            strSql.Append(" Id,Des,LogUserId,LogUserType,IpAddress,CreatTime,LastUpdateTime,Type,Status ");
            strSql.Append(" FROM SC_Log ");
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
            parameters[0].Value = "SC_Log";
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
