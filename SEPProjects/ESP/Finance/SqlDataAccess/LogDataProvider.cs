using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using ESP.Finance.Entity;
using System.Collections.Generic;
using ESP.Finance.Utility;
//using Maticsoft.DBUtility;//请先添加引用
namespace ESP.Finance.DataAccess
{
	/// <summary>
	/// 数据访问类LogDAL。
	/// </summary>
    internal class LogDataProvider : ESP.Finance.IDataAccess.ILogDataProvider
	{
		
		#region  成员方法

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public int GetMaxId()
		{
		return DbHelperSQL.GetMaxID("LogID", "F_Log"); 
		}

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int LogID)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from F_Log");
			strSql.Append(" where LogID=@LogID ");
			SqlParameter[] parameters = {
					new SqlParameter("@LogID", SqlDbType.Int,4)};
			parameters[0].Value = LogID;

			return DbHelperSQL.Exists(strSql.ToString(),parameters);
		}



        ///// <summary>
        ///// 是否存在该记录
        ///// </summary>
        //public bool Exists(string term, List<SqlParameter> param)
        //{
        //    return Exists(term, param, false);
        //}

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(string term, List<SqlParameter> param)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(*)");
            strSql.Append(" FROM F_Log ");
            if (!string.IsNullOrEmpty(term))
            {
                strSql.Append(" where " + term);
            }
            if (param != null && param.Count > 0)
            {
                return DbHelperSQL.Exists(term,  param.ToArray());

            }
            return DbHelperSQL.Exists(term);
        }

        public int AddException(Exception ex)
        {
            return Add("Exception", string.Empty,DateTime.Now.ToString("yy:MM:dd hh:mm:ss") +" "+ ex.GetType().Name + " "+ ex.StackTrace);
        }


        public int Add(string operation, string tablename, string des)
        {
            return Add(operation, tablename, des, false);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="operation"></param>
        /// <param name="tablename"></param>
        /// <param name="des"></param>
        /// <returns></returns>
        public int Add(string operation, string tablename, string des,bool isInTrans)
        {
            Entity.LogInfo log = new LogInfo();
            log.OperateDate = DateTime.Now;
            log.Operation = operation;
            log.TableName = tablename;
            log.Contents = des;
            log.OperatorUserID = Common.CurrentUserSysID;
            log.OperatorUserName = Common.CurrentUserName;
            log.OperatorCode = Common.CurrentUserCode;
            log.OperatorEmployeeName = Common.CurrentUserEmpName;
            return this.Add(log);
        }

        ///// <summary>
        ///// 增加一条数据
        ///// </summary>
        //public int Add(ESP.Finance.Entity.LogInfo model)
        //{
        //    return Add(model, false);
        //}

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(ESP.Finance.Entity.LogInfo model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into F_Log(");
			strSql.Append("Operation,TableName,Contents,OperatorUserID,OperatorCode,OperatorUserName,OperatorEmployeeName,OperateDate)");
			strSql.Append(" values (");
			strSql.Append("@Operation,@TableName,@Contents,@OperatorUserID,@OperatorCode,@OperatorUserName,@OperatorEmployeeName,@OperateDate)");
            strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = {
					new SqlParameter("@Operation", SqlDbType.NVarChar,50),
					new SqlParameter("@TableName", SqlDbType.NVarChar,50),
					new SqlParameter("@Contents", SqlDbType.NVarChar,500),
					new SqlParameter("@OperatorUserID", SqlDbType.Int,4),
					new SqlParameter("@OperatorCode", SqlDbType.NVarChar,50),
					new SqlParameter("@OperatorUserName", SqlDbType.NVarChar,50),
					new SqlParameter("@OperatorEmployeeName", SqlDbType.NVarChar,50),
					new SqlParameter("@OperateDate", SqlDbType.DateTime)};
			parameters[0].Value =model.Operation;
			parameters[1].Value =model.TableName;
			parameters[2].Value =model.Contents;
			parameters[3].Value =model.OperatorUserID;
			parameters[4].Value =model.OperatorCode;
			parameters[5].Value =model.OperatorUserName;
			parameters[6].Value =model.OperatorEmployeeName;
			parameters[7].Value =model.OperateDate;

            object obj = DbHelperSQL.GetSingle(strSql.ToString(), parameters);
            if (obj == null)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(obj);
            }
		}

        //        /// <summary>
        ///// 更新一条数据
        ///// </summary>
        //public int Update(ESP.Finance.Entity.LogInfo model)
        //{
        //    return Update(model, false);
        //}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public int Update(ESP.Finance.Entity.LogInfo model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update F_Log set ");
			strSql.Append("Operation=@Operation,");
			strSql.Append("TableName=@TableName,");
			strSql.Append("Contents=@Contents,");
			strSql.Append("OperatorUserID=@OperatorUserID,");
			strSql.Append("OperatorCode=@OperatorCode,");
			strSql.Append("OperatorUserName=@OperatorUserName,");
			strSql.Append("OperatorEmployeeName=@OperatorEmployeeName,");
			strSql.Append("OperateDate=@OperateDate");
			strSql.Append(" where LogID=@LogID ");
			SqlParameter[] parameters = {
					new SqlParameter("@LogID", SqlDbType.Int,4),
					new SqlParameter("@Operation", SqlDbType.NVarChar,50),
					new SqlParameter("@TableName", SqlDbType.NVarChar,50),
					new SqlParameter("@Contents", SqlDbType.NVarChar,500),
					new SqlParameter("@OperatorUserID", SqlDbType.Int,4),
					new SqlParameter("@OperatorCode", SqlDbType.NVarChar,50),
					new SqlParameter("@OperatorUserName", SqlDbType.NVarChar,50),
					new SqlParameter("@OperatorEmployeeName", SqlDbType.NVarChar,50),
					new SqlParameter("@OperateDate", SqlDbType.DateTime)};
			parameters[0].Value =model.LogID;
			parameters[1].Value =model.Operation;
			parameters[2].Value =model.TableName;
			parameters[3].Value =model.Contents;
			parameters[4].Value =model.OperatorUserID;
			parameters[5].Value =model.OperatorCode;
			parameters[6].Value =model.OperatorUserName;
			parameters[7].Value =model.OperatorEmployeeName;
			parameters[8].Value =model.OperateDate;

            return DbHelperSQL.ExecuteSql(strSql.ToString(),  parameters);
		}

        ///// <summary>
        ///// 删除一条数据
        ///// </summary>
        //public int Delete(int LogID)
        //{
        //    return Delete(LogID, false);
        //}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public int Delete(int LogID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete F_Log ");
			strSql.Append(" where LogID=@LogID ");
			SqlParameter[] parameters = {
					new SqlParameter("@LogID", SqlDbType.Int,4)};
			parameters[0].Value = LogID;

			return DbHelperSQL.ExecuteSql(strSql.ToString(),parameters);
		}



		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public ESP.Finance.Entity.LogInfo GetModel(int LogID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 LogID,Operation,TableName,Contents,OperatorUserID,OperatorCode,OperatorUserName,OperatorEmployeeName,OperateDate from F_Log ");
			strSql.Append(" where LogID=@LogID ");
			SqlParameter[] parameters = {
					new SqlParameter("@LogID", SqlDbType.Int,4)};
			parameters[0].Value = LogID;
            return CBO.FillObject<LogInfo>(DbHelperSQL.Query(strSql.ToString(), parameters));
		}



		/// <summary>
		/// 获得数据列表
		/// </summary>
        public IList<LogInfo> GetList(string term,List<SqlParameter> param)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select LogID,Operation,TableName,Contents,OperatorUserID,OperatorCode,OperatorUserName,OperatorEmployeeName,OperateDate ");
			strSql.Append(" FROM F_Log ");
            if (!string.IsNullOrEmpty(term))
            {
                strSql.Append(" where " + term);
            }
            //if (param != null && param.Count > 0)
            //{
            //    SqlParameter[] ps = param.ToArray();

            //    return CBO.FillCollection<LogInfo>(DbHelperSQL.ExecuteReader(strSql.ToString(), ps));
            //}

            return CBO.FillCollection<LogInfo>(DbHelperSQL.Query(strSql.ToString(),param));
		}

		

		#endregion  成员方法


    }
}

