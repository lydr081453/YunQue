using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using ESP.Finance.Entity;
using System.Collections.Generic;
using ESP.Finance.Utility;
namespace ESP.Finance.DataAccess
{
	/// <summary>
	/// 数据访问类ExpenseDAL。
	/// </summary>
    internal class ExpenseAuditerListDataProvider : ESP.Finance.IDataAccess.IExpenseAuditerListDataProvider
	{

        #region  成员方法

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(ESP.Finance.Entity.ExpenseAuditerListInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into F_ExpenseAuditerList(");
            strSql.Append("ReturnID,Auditer,AuditerName,AuditType)");
            strSql.Append(" values (");
            strSql.Append("@ReturnID,@Auditer,@AuditerName,@AuditType)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@ReturnID", SqlDbType.Int,4),
					new SqlParameter("@Auditer", SqlDbType.Int,4),
					new SqlParameter("@AuditerName", SqlDbType.NVarChar,50),
					new SqlParameter("@AuditType", SqlDbType.Int,4)};
            parameters[0].Value = model.ReturnID;
            parameters[1].Value = model.Auditer;
            parameters[2].Value = model.AuditerName;
            parameters[3].Value = model.AuditType;

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
        public int Add(ESP.Finance.Entity.ExpenseAuditerListInfo model,SqlTransaction trans)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into F_ExpenseAuditerList(");
            strSql.Append("ReturnID,Auditer,AuditerName,AuditType)");
            strSql.Append(" values (");
            strSql.Append("@ReturnID,@Auditer,@AuditerName,@AuditType)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@ReturnID", SqlDbType.Int,4),
					new SqlParameter("@Auditer", SqlDbType.Int,4),
					new SqlParameter("@AuditerName", SqlDbType.NVarChar,50),
					new SqlParameter("@AuditType", SqlDbType.Int,4)};
            parameters[0].Value = model.ReturnID;
            parameters[1].Value = model.Auditer;
            parameters[2].Value = model.AuditerName;
            parameters[3].Value = model.AuditType;

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
        public int Update(ESP.Finance.Entity.ExpenseAuditerListInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update F_ExpenseAuditerList set ");
            strSql.Append("ReturnID=@ReturnID,");
            strSql.Append("Auditer=@Auditer,");
            strSql.Append("AuditerName=@AuditerName,");
            strSql.Append("AuditType=@AuditType");
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4),
					new SqlParameter("@ReturnID", SqlDbType.Int,4),
					new SqlParameter("@Auditer", SqlDbType.Int,4),
					new SqlParameter("@AuditerName", SqlDbType.NVarChar,50),
					new SqlParameter("@AuditType", SqlDbType.Int,4)};
            parameters[0].Value = model.ID;
            parameters[1].Value = model.ReturnID;
            parameters[2].Value = model.Auditer;
            parameters[3].Value = model.AuditerName;
            parameters[4].Value = model.AuditType;

            return DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public int Delete(int ID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete F_ExpenseAuditerList ");
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)};
            parameters[0].Value = ID;

            return DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public ESP.Finance.Entity.ExpenseAuditerListInfo GetModel(int ID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 ID,ReturnID,Auditer,AuditerName,AuditType from F_ExpenseAuditerList ");
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)};
            parameters[0].Value = ID;

            return CBO.FillObject<ExpenseAuditerListInfo>(DbHelperSQL.Query(strSql.ToString(), parameters));
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<ESP.Finance.Entity.ExpenseAuditerListInfo> GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * FROM F_ExpenseAuditerList ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where 1=1 " + strWhere);
            }
            return CBO.FillCollection<ExpenseAuditerListInfo>(DbHelperSQL.Query(strSql.ToString()));
        }

        /// <summary>
        /// 根据报销单ID 删除所有相关联的审核人员
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int DeleteByReturnID(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete F_ExpenseAuditerList ");
            strSql.Append(" where ReturnID=@ReturnID ");
            SqlParameter[] parameters = {
					new SqlParameter("@ReturnID", SqlDbType.Int,4)};
            parameters[0].Value = id;

            return DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 根据报销单ID 删除所有相关联的审核人员
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int DeleteByReturnID(int id, SqlTransaction trans)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete F_ExpenseAuditerList ");
            strSql.Append(" where ReturnID=@ReturnID ");
            SqlParameter[] parameters = {
					new SqlParameter("@ReturnID", SqlDbType.Int,4)};
            parameters[0].Value = id;

            return DbHelperSQL.ExecuteSql(strSql.ToString(),trans, parameters);
        }

        #endregion  成员方法
        
	}
}

