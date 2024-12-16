using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESP.HumanResource.Common;
using System.Data.SqlClient;
using System.Data;
using System.Web;

namespace ESP.HumanResource.DataAccess
{
    public class HRAuditLogProvider
    {
        public HRAuditLogProvider()
		{}

		#region  成员方法
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int AuditLogId)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from SEP_AuditLog");
			strSql.Append(" where AuditLogId= @AuditLogId");
			SqlParameter[] parameters = {
					new SqlParameter("@AuditLogId", SqlDbType.Int,4)
				};
			parameters[0].Value = AuditLogId;
			return DbHelperSQL.Exists(strSql.ToString(),parameters);
		}

		/// <summary>
		/// 增加一条数据
		/// </summary>
        public int Add(ESP.HumanResource.Entity.HRAuditLogInfo model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into SEP_AuditLog(");
            strSql.Append("FormId,FormType,Requesition,AuditorId,AuditorName,AuditDate,AuditStatus,AuditLevel)");
			strSql.Append(" values (");
            strSql.Append("@FormId,@FormType,@Requesition,@AuditorId,@AuditorName,@AuditDate,@AuditStatus,@AuditLevel)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = {
					new SqlParameter("@FormId", SqlDbType.Int,4),
					new SqlParameter("@FormType", SqlDbType.Int,4),
					new SqlParameter("@Requesition", SqlDbType.NVarChar),
					new SqlParameter("@AuditorId", SqlDbType.Int,4),
					new SqlParameter("@AuditorName", SqlDbType.NVarChar),
					new SqlParameter("@AuditDate", SqlDbType.DateTime),
					new SqlParameter("@AuditStatus", SqlDbType.Int,4),
					new SqlParameter("@AuditLevel", SqlDbType.Int,4)};
			parameters[0].Value = model.FormId;
			parameters[1].Value = model.FormType;
			parameters[2].Value = model.Requesition;
			parameters[3].Value = model.AuditorId;
			parameters[4].Value = model.AuditorName;
			parameters[5].Value = model.AuditDate;
            parameters[6].Value = model.AuditStatus;
            parameters[7].Value = model.AuditLevel;

			object obj = DbHelperSQL.GetSingle(strSql.ToString(),parameters);
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
        public int Add(ESP.HumanResource.Entity.HRAuditLogInfo model, SqlTransaction stran)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into SEP_AuditLog(");
            strSql.Append("FormId,FormType,Requesition,AuditorId,AuditorName,AuditDate,AuditStatus,AuditLevel)");
            strSql.Append(" values (");
            strSql.Append("@FormId,@FormType,@Requesition,@AuditorId,@AuditorName,@AuditDate,@AuditStatus,@AuditLevel)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@FormId", SqlDbType.Int,4),
					new SqlParameter("@FormType", SqlDbType.Int,4),
					new SqlParameter("@Requesition", SqlDbType.NVarChar),
					new SqlParameter("@AuditorId", SqlDbType.Int,4),
					new SqlParameter("@AuditorName", SqlDbType.NVarChar),
					new SqlParameter("@AuditDate", SqlDbType.DateTime),
					new SqlParameter("@AuditStatus", SqlDbType.Int,4),
					new SqlParameter("@AuditLevel", SqlDbType.Int,4)};
            parameters[0].Value = model.FormId;
            parameters[1].Value = model.FormType;
            parameters[2].Value = model.Requesition;
            parameters[3].Value = model.AuditorId;
            parameters[4].Value = model.AuditorName;
            parameters[5].Value = model.AuditDate;
            parameters[6].Value = model.AuditStatus;
            parameters[7].Value = model.AuditLevel;

            object obj = DbHelperSQL.GetSingle(strSql.ToString(), stran.Connection, stran, parameters);
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
        public void Update(ESP.HumanResource.Entity.HRAuditLogInfo model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update SEP_AuditLog set ");
			strSql.Append("FormId=@FormId,");
			strSql.Append("FormType=@FormType,");
			strSql.Append("Requesition=@Requesition,");
			strSql.Append("AuditorId=@AuditorId,");
			strSql.Append("AuditorName=@AuditorName,");
			strSql.Append("AuditDate=@AuditDate,");
            strSql.Append("AuditStatus=@AuditStatus,");
            strSql.Append("AuditLevel=@AuditLevel");
			strSql.Append(" where AuditLogId=@AuditLogId");
			SqlParameter[] parameters = {
					new SqlParameter("@AuditLogId", SqlDbType.Int,4),
					new SqlParameter("@FormId", SqlDbType.Int,4),
					new SqlParameter("@FormType", SqlDbType.Int,4),
					new SqlParameter("@Requesition", SqlDbType.NVarChar),
					new SqlParameter("@AuditorId", SqlDbType.Int,4),
					new SqlParameter("@AuditorName", SqlDbType.NVarChar),
					new SqlParameter("@AuditDate", SqlDbType.DateTime),
					new SqlParameter("@AuditStatus", SqlDbType.Int,4),
					new SqlParameter("@AuditLevel", SqlDbType.Int,4)};
			parameters[0].Value = model.AuditLogId;
			parameters[1].Value = model.FormId;
			parameters[2].Value = model.FormType;
			parameters[3].Value = model.Requesition;
			parameters[4].Value = model.AuditorId;
			parameters[5].Value = model.AuditorName;
			parameters[6].Value = model.AuditDate;
            parameters[7].Value = model.AuditStatus;
            parameters[8].Value = model.AuditLevel;

			DbHelperSQL.ExecuteSql(strSql.ToString(),parameters);
		}

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public void Update(ESP.HumanResource.Entity.HRAuditLogInfo model, SqlTransaction stran)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update SEP_AuditLog set ");
            strSql.Append("FormId=@FormId,");
            strSql.Append("FormType=@FormType,");
            strSql.Append("Requesition=@Requesition,");
            strSql.Append("AuditorId=@AuditorId,");
            strSql.Append("AuditorName=@AuditorName,");
            strSql.Append("AuditDate=@AuditDate,");
            strSql.Append("AuditStatus=@AuditStatus,");
            strSql.Append("AuditLevel=@AuditLevel");
            strSql.Append(" where AuditLogId=@AuditLogId");
            SqlParameter[] parameters = {
					new SqlParameter("@AuditLogId", SqlDbType.Int,4),
					new SqlParameter("@FormId", SqlDbType.Int,4),
					new SqlParameter("@FormType", SqlDbType.Int,4),
					new SqlParameter("@Requesition", SqlDbType.NVarChar),
					new SqlParameter("@AuditorId", SqlDbType.Int,4),
					new SqlParameter("@AuditorName", SqlDbType.NVarChar),
					new SqlParameter("@AuditDate", SqlDbType.DateTime),
					new SqlParameter("@AuditStatus", SqlDbType.Int,4),
					new SqlParameter("@AuditLevel", SqlDbType.Int,4)};
            parameters[0].Value = model.AuditLogId;
            parameters[1].Value = model.FormId;
            parameters[2].Value = model.FormType;
            parameters[3].Value = model.Requesition;
            parameters[4].Value = model.AuditorId;
            parameters[5].Value = model.AuditorName;
            parameters[6].Value = model.AuditDate;
            parameters[7].Value = model.AuditStatus;
            parameters[8].Value = model.AuditLevel;

            DbHelperSQL.ExecuteSql(strSql.ToString(), stran.Connection, stran, parameters);
        }

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public void Delete(int AuditLogId)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete SEP_AuditLog ");
			strSql.Append(" where AuditLogId=@AuditLogId");
			SqlParameter[] parameters = {
					new SqlParameter("@AuditLogId", SqlDbType.Int,4)
				};
			parameters[0].Value = AuditLogId;
			DbHelperSQL.ExecuteSql(strSql.ToString(),parameters);
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
        public ESP.HumanResource.Entity.HRAuditLogInfo GetModel(int AuditLogId)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select * from SEP_AuditLog ");
			strSql.Append(" where AuditLogId=@AuditLogId");
			SqlParameter[] parameters = {
					new SqlParameter("@AuditLogId", SqlDbType.Int,4)};
			parameters[0].Value = AuditLogId;
            ESP.HumanResource.Entity.HRAuditLogInfo model = new ESP.HumanResource.Entity.HRAuditLogInfo();
			DataSet ds=DbHelperSQL.Query(strSql.ToString(),parameters);
			model.AuditLogId=AuditLogId;
			if(ds.Tables[0].Rows.Count>0)
			{
                model.PopupData(ds.Tables[0].Rows[0]);
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
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select * ");
			strSql.Append(" FROM SEP_AuditLog ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			return DbHelperSQL.Query(strSql.ToString());
		}

        /// <summary>
        /// 获得审批单据审批记录信息
        /// </summary>
        /// <param name="userId">用户编号</param>
        /// <param name="formId">单据编号</param>
        /// <param name="formType">单据类型</param>
        /// <param name="auditStatus">审批状态</param>
        /// <returns></returns>
        public ESP.HumanResource.Entity.HRAuditLogInfo GetAuditLogInfo(int userId, int formId, int formType, int auditStatus)
        {
            int auditDepId = userId;
            int backupDepId = 0;
            IList<ESP.Framework.Entity.EmployeePositionInfo> list = null;
            IList<ESP.Framework.Entity.AuditBackUpInfo> auditlist = ESP.Framework.BusinessLogic.AuditBackUpManager.GetModelsByBackUpUserID(userId);
            ESP.Framework.Entity.AuditBackUpInfo backupmodel = null;
            if (auditlist != null && auditlist.Count > 0)
                backupmodel = auditlist[0];

            if (backupmodel != null)
            {
                list = ESP.Framework.BusinessLogic.DepartmentPositionManager.GetEmployeePositions(backupmodel.UserID);
                if (list != null && list.Count > 0)
                {
                    ESP.Framework.Entity.DepartmentInfo departmentInfo = ESP.Framework.BusinessLogic.DepartmentManager.Get(list[0].DepartmentID);
                    if (departmentInfo != null && departmentInfo.DepartmentLevel == 3)
                    {
                        backupDepId = departmentInfo.DepartmentID;
                    }
                }
            }


            // 获得当前登录用户的人员部门信息
            list = null;
            list = ESP.Framework.BusinessLogic.DepartmentPositionManager.GetEmployeePositions(userId);
            if (list != null && list.Count > 0)
            {
                ESP.Framework.Entity.DepartmentInfo departmentInfo = ESP.Framework.BusinessLogic.DepartmentManager.Get(list[0].DepartmentID);
                if (departmentInfo != null && departmentInfo.DepartmentLevel == 3)
                {
                    auditDepId = departmentInfo.DepartmentID;
                }
            }
            string deptids = "";
            if (auditDepId == 43 || auditDepId == 31)
            {
                deptids = "31,43";
            }
            else
                deptids = auditDepId.ToString();

            if (auditDepId == 34 || auditDepId == 256 || auditDepId == 258 || auditDepId == 261 || auditDepId == 287)
            {
                deptids = "34,256,258,261,287";
            }
            else
                deptids = auditDepId.ToString();

            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM SEP_AuditLog ");
            if (backupDepId == 0)
            {
                strSql.Append(" WHERE (AuditorId=@AuditorId OR AuditorId in(" + deptids + ") ) AND AuditStatus=@AuditStatus AND FormId=@FormId AND FormType=@FormType");

                SqlParameter[] parameters = {
					new SqlParameter("@AuditorId", SqlDbType.Int, 4),
                    new SqlParameter("@AuditStatus", SqlDbType.Int, 4),
                    new SqlParameter("@FormId", SqlDbType.Int, 4),
                    new SqlParameter("@FormType", SqlDbType.Int, 4)};
                parameters[0].Value = userId;
                parameters[1].Value = auditStatus;
                parameters[2].Value = formId;
                parameters[3].Value = formType;
                ESP.HumanResource.Entity.HRAuditLogInfo model = new ESP.HumanResource.Entity.HRAuditLogInfo();
                DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    model.PopupData(ds.Tables[0].Rows[0]);
                    return model;
                }
                else
                {
                    return null;
                }
            }
            else
            {
                strSql.Append(" WHERE (AuditorId=@AuditorId or AuditorId=@BackupAuditorId OR AuditorId in(" + deptids + ")) AND AuditStatus=@AuditStatus AND FormId=@FormId AND FormType=@FormType");

                SqlParameter[] parameters = {
					new SqlParameter("@AuditorId", SqlDbType.Int, 4),
                    new SqlParameter("@BackupAuditorId", SqlDbType.Int, 4),
                    new SqlParameter("@AuditStatus", SqlDbType.Int, 4),
                    new SqlParameter("@FormId", SqlDbType.Int, 4),
                    new SqlParameter("@FormType", SqlDbType.Int, 4)};
                parameters[0].Value = userId;
                parameters[1].Value = backupmodel.UserID;
                parameters[2].Value = auditStatus;
                parameters[3].Value = formId;
                parameters[4].Value = formType;
                ESP.HumanResource.Entity.HRAuditLogInfo model = new ESP.HumanResource.Entity.HRAuditLogInfo();
                DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    model.PopupData(ds.Tables[0].Rows[0]);
                    return model;
                }
                else
                {
                    return null;
                }

               
            }
           
        }

        /// <summary>
        /// 获得审批单据审批记录信息
        /// </summary>
        /// <param name="userId">用户编号</param>
        /// <param name="formId">单据编号</param>
        /// <param name="formType">单据类型</param>
        /// <param name="auditStatus">审批状态</param>
        /// <returns></returns>
        public List<ESP.HumanResource.Entity.HRAuditLogInfo> GetAuditLogInfo(int formId, int formType, int auditStatus)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM SEP_AuditLog ");
            strSql.Append(" WHERE AuditStatus=@AuditStatus AND FormId=@FormId AND FormType=@FormType");
            SqlParameter[] parameters = {
                    new SqlParameter("@AuditStatus", SqlDbType.Int, 4),
                    new SqlParameter("@FormId", SqlDbType.Int, 4),
                    new SqlParameter("@FormType", SqlDbType.Int, 4)};
            parameters[0].Value = auditStatus;
            parameters[1].Value = formId;
            parameters[2].Value = formType;
           
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            List<ESP.HumanResource.Entity.HRAuditLogInfo> listDr = new List<ESP.HumanResource.Entity.HRAuditLogInfo>();
            if (ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ESP.HumanResource.Entity.HRAuditLogInfo model = new ESP.HumanResource.Entity.HRAuditLogInfo();
                    model.PopupData(dr);
                    listDr.Add(model);
                }
                return listDr;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 获得审批单据审批记录信息
        /// </summary>
        /// <param name="formId">单据编号</param>
        /// <param name="formType">单据类型</param>
        /// <returns></returns>
        public List<ESP.HumanResource.Entity.HRAuditLogInfo> GetAuditLogInfo(int formId, int formType)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM SEP_AuditLog ");
            strSql.Append(" WHERE FormId=@FormId AND FormType=@FormType");
            SqlParameter[] parameters = {
                    new SqlParameter("@FormId", SqlDbType.Int, 4),
                    new SqlParameter("@FormType", SqlDbType.Int, 4)};
            parameters[0].Value = formId;
            parameters[1].Value = formType;

            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            List<ESP.HumanResource.Entity.HRAuditLogInfo> listDr = new List<ESP.HumanResource.Entity.HRAuditLogInfo>();
            if (ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ESP.HumanResource.Entity.HRAuditLogInfo model = new ESP.HumanResource.Entity.HRAuditLogInfo();
                    model.PopupData(dr);
                    listDr.Add(model);
                }
                return listDr;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 获得审批日志信息集合
        /// </summary>
        /// <param name="formId">单据编号</param>
        /// <param name="formType">单据类型</param>
        /// <returns>返回审批日志信息集合</returns>
        public List<ESP.HumanResource.Entity.HRAuditLogInfo> GetAuditLogInfos(int formId, int formType)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM SEP_AuditLog ");
            strSql.Append(" WHERE AuditStatus<>@AuditStatus AND FormId=@FormId AND FormType=@FormType ORDER BY AuditDate ");
            SqlParameter[] parameters = {
                    new SqlParameter("@AuditStatus", SqlDbType.Int, 4),
                    new SqlParameter("@FormId", SqlDbType.Int, 4),
                    new SqlParameter("@FormType", SqlDbType.Int, 4)};
            parameters[0].Value = ESP.HumanResource.Common.AuditStatus.NotAudit;
            parameters[1].Value = formId;
            parameters[2].Value = formType;

            List<ESP.HumanResource.Entity.HRAuditLogInfo> hrAuditLogList = new List<ESP.HumanResource.Entity.HRAuditLogInfo>();
            
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ESP.HumanResource.Entity.HRAuditLogInfo model = new ESP.HumanResource.Entity.HRAuditLogInfo(); 
                    model.PopupData(dr);
                    hrAuditLogList.Add(model);
                }
            }
            return hrAuditLogList;
        }
		#endregion  成员方法
    }
}
