using System;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Practices.EnterpriseLibrary.Data;

namespace ESP.DataAccessAuthorization.SqlDataAccess
{
    /// <summary>
    /// 权限成员 SQL Server 操作类
    /// </summary>
    public class DataAccessMember : DataAccess.IDataAccessMember
    {
        #region IDataAccessMember 成员
        /// <summary>
        /// 添加一条记录
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int Add(ESP.DataAccessAuthorization.Entity.DataAccessMember model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into ESP_DataAccessMember(");
            strSql.Append("MemberName,MemberType,MemberDefinition,MemberService,CreateTime,Creator,CreatorName)");
            strSql.Append(" values (");
            strSql.Append("@MemberName,@MemberType,@MemberDefinition,@MemberService,@CreateTime,@Creator,@CreatorName)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@MemberName", SqlDbType.NVarChar,50),
					new SqlParameter("@MemberType", SqlDbType.Int,4),
					new SqlParameter("@MemberDefinition", SqlDbType.NVarChar,4000),
					new SqlParameter("@MemberService", SqlDbType.NVarChar,400),
					new SqlParameter("@CreateTime", SqlDbType.DateTime),
					new SqlParameter("@Creator", SqlDbType.Int,4),
					new SqlParameter("@CreatorName", SqlDbType.NVarChar,50)};
            parameters[0].Value = model.MemberName;
            parameters[1].Value = model.MemberType;
            parameters[2].Value = model.MemberDefinition;
            parameters[3].Value = model.MemberService;
            parameters[4].Value = model.CreateTime;
            parameters[5].Value = model.Creator;
            parameters[6].Value = model.CreatorName;

            Database db = DatabaseFactory.CreateDatabase(ESP.Configuration.ConfigurationManager.ConnectionStringName);
            DbCommand cmd = db.GetSqlStringCommand(strSql.ToString());
            Framework.DataAccess.Utilities.SqlUtil.SetParameters(cmd, parameters);
            object obj = db.ExecuteScalar(cmd);
            if (obj == null)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(obj);
            }
        }
        /// <summary>
        /// 更新一条记录
        /// </summary>
        /// <param name="model"></param>
        /// <returns>返回更新数据数量</returns>
        public int Update(ESP.DataAccessAuthorization.Entity.DataAccessMember model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update ESP_DataAccessMember set ");
            strSql.Append("MemberName=@MemberName,");
            strSql.Append("MemberType=@MemberType,");
            strSql.Append("MemberDefinition=@MemberDefinition,");
            strSql.Append("MemberService=@MemberService,");
            strSql.Append("CreateTime=@CreateTime,");
            strSql.Append("Creator=@Creator,");
            strSql.Append("CreatorName=@CreatorName");
            strSql.Append(" where DataAccessMemberID=@DataAccessMemberID ");
            SqlParameter[] parameters = {
					new SqlParameter("@DataAccessMemberID", SqlDbType.Int,4),
					new SqlParameter("@MemberName", SqlDbType.NVarChar,50),
					new SqlParameter("@MemberType", SqlDbType.Int,4),
					new SqlParameter("@MemberDefinition", SqlDbType.NVarChar,4000),
					new SqlParameter("@MemberService", SqlDbType.NVarChar,400),
					new SqlParameter("@CreateTime", SqlDbType.DateTime),
					new SqlParameter("@Creator", SqlDbType.Int,4),
					new SqlParameter("@CreatorName", SqlDbType.NVarChar,50)};
            parameters[0].Value = model.DataAccessMemberID;
            parameters[1].Value = model.MemberName;
            parameters[2].Value = model.MemberType;
            parameters[3].Value = model.MemberDefinition;
            parameters[4].Value = model.MemberService;
            parameters[5].Value = model.CreateTime;
            parameters[6].Value = model.Creator;
            parameters[7].Value = model.CreatorName;

            Database db = DatabaseFactory.CreateDatabase(ESP.Configuration.ConfigurationManager.ConnectionStringName);
            DbCommand cmd = db.GetSqlStringCommand(strSql.ToString());
            Framework.DataAccess.Utilities.SqlUtil.SetParameters(cmd, parameters);
            return db.ExecuteNonQuery(cmd);
        }
        /// <summary>
        /// 删除一条记录
        /// </summary>
        /// <param name="DataAccessMemberID"></param>
        /// <returns>返回删除数据数量</returns>
        public int Delete(int DataAccessMemberID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete ESP_DataAccessMember ");
            strSql.Append(" where DataAccessMemberID=@DataAccessMemberID ");
            SqlParameter[] parameters = {
					new SqlParameter("@DataAccessMemberID", SqlDbType.Int,4)};
            parameters[0].Value = DataAccessMemberID;

            Database db = DatabaseFactory.CreateDatabase(ESP.Configuration.ConfigurationManager.ConnectionStringName);
            DbCommand cmd = db.GetSqlStringCommand(strSql.ToString());
            Framework.DataAccess.Utilities.SqlUtil.SetParameters(cmd, parameters);
            return db.ExecuteNonQuery(cmd);
        }
        /// <summary>
        /// 获取某个对象
        /// </summary>
        /// <param name="DataAccessMemberID">某个主键值</param>
        /// <returns>返回拥有指定主键的对象</returns>
        public ESP.DataAccessAuthorization.Entity.DataAccessMember GetModel(int DataAccessMemberID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 DataAccessMemberID,MemberName,MemberType,MemberDefinition,MemberService,CreateTime,Creator,CreatorName from ESP_DataAccessMember ");
            strSql.Append(" where DataAccessMemberID=@DataAccessMemberID ");
            SqlParameter[] parameters = {
					new SqlParameter("@DataAccessMemberID", SqlDbType.Int,4)};
            parameters[0].Value = DataAccessMemberID;

            Database db = DatabaseFactory.CreateDatabase(ESP.Configuration.ConfigurationManager.ConnectionStringName);
            DbCommand cmd = db.GetSqlStringCommand(strSql.ToString());
            Framework.DataAccess.Utilities.SqlUtil.SetParameters(cmd, parameters);
            return Framework.DataAccess.Utilities.CBO.LoadObject<Entity.DataAccessMember>(db.ExecuteReader(cmd));
        }
        /// <summary>
        /// 根据查询条件获取对象的List
        /// </summary>
        /// <param name="strWhere">条件参数（这个函数有安全漏洞，需要严格控制strWhere，不能把用户的输入直接传递给strWhere变量）</param>
        /// <returns>返回对象List</returns>
        public IList<ESP.DataAccessAuthorization.Entity.DataAccessMember> GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select DataAccessMemberID,MemberName,MemberType,MemberDefinition,MemberService,CreateTime,Creator,CreatorName ");
            strSql.Append(" FROM ESP_DataAccessMember ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere.Replace("\r\n", "").Replace("\'", "\'\'").Replace(";", ""));//做了基本屏蔽
            }

            Database db = DatabaseFactory.CreateDatabase(ESP.Configuration.ConfigurationManager.ConnectionStringName);
            DbCommand cmd = db.GetSqlStringCommand(strSql.ToString());
            return Framework.DataAccess.Utilities.CBO.LoadList<Entity.DataAccessMember>(db.ExecuteReader(cmd));
        }

        #endregion
    }
}
