using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESP.HumanResource.Common;
using System.Data.SqlClient;
using System.Data;

namespace ESP.HumanResource.DataAccess
{
    public class DimissionFormProvider
    {
        public DimissionFormProvider()
        { }
        #region  成员方法
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int DimissionId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT count(1) FROM SEP_DimissionForm");
            strSql.Append(" WHERE DimissionId= @DimissionId");
            SqlParameter[] parameters = {
					new SqlParameter("@DimissionId", SqlDbType.Int,4)
				};
            parameters[0].Value = DimissionId;
            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool ExistsUser(int UserId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT count(1) FROM SEP_DimissionForm");
            strSql.Append(" WHERE UserId= @UserId");
            SqlParameter[] parameters = {
					new SqlParameter("@UserId", SqlDbType.Int,4)
				};
            parameters[0].Value = UserId;
            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(ESP.HumanResource.Entity.DimissionFormInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("INSERT INTO SEP_DimissionForm(");
            strSql.Append("UserId,UserCode,UserName,DepartmentId,DepartmentName,LastDay,AppDate,Reason,Status,CreateTime,MobilePhone,PrivateMail,HopeLastDay,FinanceAuditStatus,ITAuditStatus,ADAuditStatus,HRAuditStatus,DirectorId,DirectorName,ManagerId,ManagerName,IsNormalPer,SumPerformance,TotalIndemnityAmount,HRReason,HRReasonRemark,PreAuditorId,PreAuditor)");
            strSql.Append(" VALUES (");
            strSql.Append("@UserId,@UserCode,@UserName,@DepartmentId,@DepartmentName,@LastDay,@AppDate,@Reason,@Status,@CreateTime,@MobilePhone,@PrivateMail,@HopeLastDay,@FinanceAuditStatus,@ITAuditStatus,@ADAuditStatus,@HRAuditStatus,@DirectorId,@DirectorName,@ManagerId,@ManagerName,@IsNormalPer,@SumPerformance,@TotalIndemnityAmount,@HRReason,@HRReasonRemark,@PreAuditorId,@PreAuditor)");
            strSql.Append(";SELECT @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@UserId", SqlDbType.Int,4),
					new SqlParameter("@UserCode", SqlDbType.NVarChar),
					new SqlParameter("@UserName", SqlDbType.NVarChar),
					new SqlParameter("@DepartmentId", SqlDbType.Int,4),
					new SqlParameter("@DepartmentName", SqlDbType.NVarChar),
					new SqlParameter("@LastDay", SqlDbType.DateTime),
					new SqlParameter("@AppDate", SqlDbType.DateTime),
					new SqlParameter("@Reason", SqlDbType.NVarChar),
					new SqlParameter("@Status", SqlDbType.Int,4),
					new SqlParameter("@CreateTime", SqlDbType.DateTime),
					new SqlParameter("@MobilePhone", SqlDbType.NVarChar),
					new SqlParameter("@PrivateMail", SqlDbType.NVarChar),
					new SqlParameter("@HopeLastDay", SqlDbType.DateTime),
					new SqlParameter("@FinanceAuditStatus", SqlDbType.Int,4),
					new SqlParameter("@ITAuditStatus", SqlDbType.Int,4),
					new SqlParameter("@ADAuditStatus", SqlDbType.Int,4),
					new SqlParameter("@HRAuditStatus", SqlDbType.Int,4),
					new SqlParameter("@DirectorId", SqlDbType.Int,4),
					new SqlParameter("@DirectorName", SqlDbType.NVarChar),
					new SqlParameter("@ManagerId", SqlDbType.Int,4),
					new SqlParameter("@ManagerName", SqlDbType.NVarChar),
					new SqlParameter("@IsNormalPer", SqlDbType.Bit,1),
					new SqlParameter("@SumPerformance", SqlDbType.Decimal,9),
                    new SqlParameter("@TotalIndemnityAmount", SqlDbType.Decimal,9),
                    new SqlParameter("@HRReason", SqlDbType.NVarChar,50),
					new SqlParameter("@HRReasonRemark", SqlDbType.NVarChar,500),
                    new SqlParameter("@PreAuditorId", SqlDbType.Int,4),
					new SqlParameter("@PreAuditor", SqlDbType.NVarChar)
                                        };
            parameters[0].Value = model.UserId;
            parameters[1].Value = model.UserCode;
            parameters[2].Value = model.UserName;
            parameters[3].Value = model.DepartmentId;
            parameters[4].Value = model.DepartmentName;
            parameters[5].Value = model.LastDay;
            parameters[6].Value = model.AppDate;
            parameters[7].Value = model.Reason;
            parameters[8].Value = model.Status;
            parameters[9].Value = model.CreateTime;
            parameters[10].Value = model.MobilePhone;
            parameters[11].Value = model.PrivateMail;
            parameters[12].Value = model.HopeLastDay;
            parameters[13].Value = model.FinanceAuditStatus;
            parameters[14].Value = model.ITAuditStatus;
            parameters[15].Value = model.ADAuditStatus;
            parameters[16].Value = model.HRAuditStatus;
            parameters[17].Value = model.DirectorId;
            parameters[18].Value = model.DirectorName;
            parameters[19].Value = model.ManagerId;
            parameters[20].Value = model.ManagerName;
            parameters[21].Value = model.IsNormalPer;
            parameters[22].Value = model.SumPerformance;
            parameters[23].Value = model.TotalIndemnityAmount;
            parameters[24].Value = model.HRReason;
            parameters[25].Value = model.HRReasonRemark;
            parameters[26].Value = model.PreAuditorId;
            parameters[27].Value = model.PreAuditor;

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
        public int Add(ESP.HumanResource.Entity.DimissionFormInfo model, SqlTransaction stran)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("INSERT INTO SEP_DimissionForm(");
            strSql.Append("UserId,UserCode,UserName,DepartmentId,DepartmentName,LastDay,AppDate,Reason,Status,CreateTime,MobilePhone,PrivateMail,HopeLastDay,FinanceAuditStatus,ITAuditStatus,ADAuditStatus,HRAuditStatus,DirectorId,DirectorName,ManagerId,ManagerName,IsNormalPer,SumPerformance,TotalIndemnityAmount,HRReason,HRReasonRemark,PreAuditorId,PreAuditor)");
            strSql.Append(" VALUES (");
            strSql.Append("@UserId,@UserCode,@UserName,@DepartmentId,@DepartmentName,@LastDay,@AppDate,@Reason,@Status,@CreateTime,@MobilePhone,@PrivateMail,@HopeLastDay,@FinanceAuditStatus,@ITAuditStatus,@ADAuditStatus,@HRAuditStatus,@DirectorId,@DirectorName,@ManagerId,@ManagerName,@IsNormalPer,@SumPerformance,@TotalIndemnityAmount,@HRReason,@HRReasonRemark,@PreAuditorId,@PreAuditor)");
            strSql.Append(";SELECT @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@UserId", SqlDbType.Int,4),
					new SqlParameter("@UserCode", SqlDbType.NVarChar),
					new SqlParameter("@UserName", SqlDbType.NVarChar),
					new SqlParameter("@DepartmentId", SqlDbType.Int,4),
					new SqlParameter("@DepartmentName", SqlDbType.NVarChar),
					new SqlParameter("@LastDay", SqlDbType.DateTime),
					new SqlParameter("@AppDate", SqlDbType.DateTime),
					new SqlParameter("@Reason", SqlDbType.NVarChar),
					new SqlParameter("@Status", SqlDbType.Int,4),
					new SqlParameter("@CreateTime", SqlDbType.DateTime),
					new SqlParameter("@MobilePhone", SqlDbType.NVarChar),
					new SqlParameter("@PrivateMail", SqlDbType.NVarChar),
					new SqlParameter("@HopeLastDay", SqlDbType.DateTime),
					new SqlParameter("@FinanceAuditStatus", SqlDbType.Int,4),
					new SqlParameter("@ITAuditStatus", SqlDbType.Int,4),
					new SqlParameter("@ADAuditStatus", SqlDbType.Int,4),
					new SqlParameter("@HRAuditStatus", SqlDbType.Int,4),
					new SqlParameter("@DirectorId", SqlDbType.Int,4),
					new SqlParameter("@DirectorName", SqlDbType.NVarChar),
					new SqlParameter("@ManagerId", SqlDbType.Int,4),
					new SqlParameter("@ManagerName", SqlDbType.NVarChar),
					new SqlParameter("@IsNormalPer", SqlDbType.Bit,1),
					new SqlParameter("@SumPerformance", SqlDbType.Decimal,9),
                    new SqlParameter("@TotalIndemnityAmount", SqlDbType.Decimal,9),
                    new SqlParameter("@HRReason", SqlDbType.NVarChar,50),
					new SqlParameter("@HRReasonRemark", SqlDbType.NVarChar,500),
                    new SqlParameter("@PreAuditorId", SqlDbType.Int,4),
					new SqlParameter("@PreAuditor", SqlDbType.NVarChar)                                        
                                        };
            parameters[0].Value = model.UserId;
            parameters[1].Value = model.UserCode;
            parameters[2].Value = model.UserName;
            parameters[3].Value = model.DepartmentId;
            parameters[4].Value = model.DepartmentName;
            parameters[5].Value = model.LastDay;
            parameters[6].Value = model.AppDate;
            parameters[7].Value = model.Reason;
            parameters[8].Value = model.Status;
            parameters[9].Value = model.CreateTime;
            parameters[10].Value = model.MobilePhone;
            parameters[11].Value = model.PrivateMail;
            parameters[12].Value = model.HopeLastDay;
            parameters[13].Value = model.FinanceAuditStatus;
            parameters[14].Value = model.ITAuditStatus;
            parameters[15].Value = model.ADAuditStatus;
            parameters[16].Value = model.HRAuditStatus;
            parameters[17].Value = model.DirectorId;
            parameters[18].Value = model.DirectorName;
            parameters[19].Value = model.ManagerId;
            parameters[20].Value = model.ManagerName;
            parameters[21].Value = model.IsNormalPer;
            parameters[22].Value = model.SumPerformance;
            parameters[23].Value = model.TotalIndemnityAmount;
            parameters[24].Value = model.HRReason;
            parameters[25].Value = model.HRReasonRemark;
            parameters[26].Value = model.PreAuditorId;
            parameters[27].Value = model.PreAuditor;

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
        public void Update(ESP.HumanResource.Entity.DimissionFormInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("UPDATE SEP_DimissionForm SET ");
            strSql.Append("UserId=@UserId,");
            strSql.Append("UserCode=@UserCode,");
            strSql.Append("UserName=@UserName,");
            strSql.Append("DepartmentId=@DepartmentId,");
            strSql.Append("DepartmentName=@DepartmentName,");
            strSql.Append("LastDay=@LastDay,");
            strSql.Append("AppDate=@AppDate,");
            strSql.Append("Reason=@Reason,");
            strSql.Append("Status=@Status,");
            strSql.Append("CreateTime=@CreateTime,");
            strSql.Append("MobilePhone=@MobilePhone,");
            strSql.Append("PrivateMail=@PrivateMail,");
            strSql.Append("HopeLastDay=@HopeLastDay ,");
            strSql.Append("FinanceAuditStatus=@FinanceAuditStatus,");
            strSql.Append("ITAuditStatus=@ITAuditStatus,");
            strSql.Append("ADAuditStatus=@ADAuditStatus,");
            strSql.Append("HRAuditStatus=@HRAuditStatus,");
            strSql.Append("DirectorId=@DirectorId,");
            strSql.Append("DirectorName=@DirectorName,");
            strSql.Append("ManagerId=@ManagerId,");
            strSql.Append("ManagerName=@ManagerName,");
            strSql.Append("IsNormalPer=@IsNormalPer,");
            strSql.Append("SumPerformance=@SumPerformance,");
            strSql.Append("TotalIndemnityAmount=@TotalIndemnityAmount,HRReason=@HRReason,HRReasonRemark=@HRReasonRemark,PreAuditorId=@PreAuditorId,PreAuditor=@PreAuditor ");
            strSql.Append(" WHERE DimissionId=@DimissionId");
            SqlParameter[] parameters = {
					new SqlParameter("@DimissionId", SqlDbType.Int,4),
					new SqlParameter("@UserId", SqlDbType.Int,4),
					new SqlParameter("@UserCode", SqlDbType.NVarChar),
					new SqlParameter("@UserName", SqlDbType.NVarChar),
					new SqlParameter("@DepartmentId", SqlDbType.Int,4),
					new SqlParameter("@DepartmentName", SqlDbType.NVarChar),
					new SqlParameter("@LastDay", SqlDbType.DateTime),
					new SqlParameter("@AppDate", SqlDbType.DateTime),
					new SqlParameter("@Reason", SqlDbType.NVarChar),
					new SqlParameter("@Status", SqlDbType.Int,4),
					new SqlParameter("@CreateTime", SqlDbType.DateTime),
					new SqlParameter("@MobilePhone", SqlDbType.NVarChar),
					new SqlParameter("@PrivateMail", SqlDbType.NVarChar),
					new SqlParameter("@HopeLastDay", SqlDbType.DateTime),
					new SqlParameter("@FinanceAuditStatus", SqlDbType.Int,4),
					new SqlParameter("@ITAuditStatus", SqlDbType.Int,4),
					new SqlParameter("@ADAuditStatus", SqlDbType.Int,4),
					new SqlParameter("@HRAuditStatus", SqlDbType.Int,4),
					new SqlParameter("@DirectorId", SqlDbType.Int,4),
					new SqlParameter("@DirectorName", SqlDbType.NVarChar),
					new SqlParameter("@ManagerId", SqlDbType.Int,4),
					new SqlParameter("@ManagerName", SqlDbType.NVarChar),
					new SqlParameter("@IsNormalPer", SqlDbType.Bit,1),
					new SqlParameter("@SumPerformance", SqlDbType.Decimal,9),
                    new SqlParameter("@TotalIndemnityAmount", SqlDbType.Decimal,9),
                    new SqlParameter("@HRReason", SqlDbType.NVarChar,50),
					new SqlParameter("@HRReasonRemark", SqlDbType.NVarChar,500),
                   new SqlParameter("@PreAuditorId", SqlDbType.Int,4),
					new SqlParameter("@PreAuditor", SqlDbType.NVarChar)         
                                        };
            parameters[0].Value = model.DimissionId;
            parameters[1].Value = model.UserId;
            parameters[2].Value = model.UserCode;
            parameters[3].Value = model.UserName;
            parameters[4].Value = model.DepartmentId;
            parameters[5].Value = model.DepartmentName;
            parameters[6].Value = model.LastDay;
            parameters[7].Value = model.AppDate;
            parameters[8].Value = model.Reason;
            parameters[9].Value = model.Status;
            parameters[10].Value = model.CreateTime;
            parameters[11].Value = model.MobilePhone;
            parameters[12].Value = model.PrivateMail;
            parameters[13].Value = model.HopeLastDay;
            parameters[14].Value = model.FinanceAuditStatus;
            parameters[15].Value = model.ITAuditStatus;
            parameters[16].Value = model.ADAuditStatus;
            parameters[17].Value = model.HRAuditStatus;
            parameters[18].Value = model.DirectorId;
            parameters[19].Value = model.DirectorName;
            parameters[20].Value = model.ManagerId;
            parameters[21].Value = model.ManagerName;
            parameters[22].Value = model.IsNormalPer;
            parameters[23].Value = model.SumPerformance;
            parameters[24].Value = model.TotalIndemnityAmount;
            parameters[25].Value = model.HRReason;
            parameters[26].Value = model.HRReasonRemark;
            parameters[27].Value = model.PreAuditorId;
            parameters[28].Value = model.PreAuditor;
            DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public void Update(ESP.HumanResource.Entity.DimissionFormInfo model, SqlTransaction stran)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("UPDATE SEP_DimissionForm SET ");
            strSql.Append("UserId=@UserId,");
            strSql.Append("UserCode=@UserCode,");
            strSql.Append("UserName=@UserName,");
            strSql.Append("DepartmentId=@DepartmentId,");
            strSql.Append("DepartmentName=@DepartmentName,");
            strSql.Append("LastDay=@LastDay,");
            strSql.Append("AppDate=@AppDate,");
            strSql.Append("Reason=@Reason,");
            strSql.Append("Status=@Status,");
            strSql.Append("CreateTime=@CreateTime,");
            strSql.Append("MobilePhone=@MobilePhone,");
            strSql.Append("PrivateMail=@PrivateMail,");
            strSql.Append("HopeLastDay=@HopeLastDay,");
            strSql.Append("FinanceAuditStatus=@FinanceAuditStatus,");
            strSql.Append("ITAuditStatus=@ITAuditStatus,");
            strSql.Append("ADAuditStatus=@ADAuditStatus,");
            strSql.Append("HRAuditStatus=@HRAuditStatus,");
            strSql.Append("DirectorId=@DirectorId,");
            strSql.Append("DirectorName=@DirectorName,");
            strSql.Append("ManagerId=@ManagerId,");
            strSql.Append("ManagerName=@ManagerName,");
            strSql.Append("IsNormalPer=@IsNormalPer,");
            strSql.Append("SumPerformance=@SumPerformance,");
            strSql.Append("TotalIndemnityAmount=@TotalIndemnityAmount,HRReason=@HRReason,HRReasonRemark=@HRReasonRemark,PreAuditorId=@PreAuditorId,PreAuditor=@PreAuditor ");
            strSql.Append(" WHERE DimissionId=@DimissionId");
            SqlParameter[] parameters = {
					new SqlParameter("@DimissionId", SqlDbType.Int,4),
					new SqlParameter("@UserId", SqlDbType.Int,4),
					new SqlParameter("@UserCode", SqlDbType.NVarChar),
					new SqlParameter("@UserName", SqlDbType.NVarChar),
					new SqlParameter("@DepartmentId", SqlDbType.Int,4),
					new SqlParameter("@DepartmentName", SqlDbType.NVarChar),
					new SqlParameter("@LastDay", SqlDbType.DateTime),
					new SqlParameter("@AppDate", SqlDbType.DateTime),
					new SqlParameter("@Reason", SqlDbType.NVarChar),
					new SqlParameter("@Status", SqlDbType.Int,4),
					new SqlParameter("@CreateTime", SqlDbType.DateTime),
					new SqlParameter("@MobilePhone", SqlDbType.NVarChar),
					new SqlParameter("@PrivateMail", SqlDbType.NVarChar),
					new SqlParameter("@HopeLastDay", SqlDbType.DateTime),
					new SqlParameter("@FinanceAuditStatus", SqlDbType.Int,4),
					new SqlParameter("@ITAuditStatus", SqlDbType.Int,4),
					new SqlParameter("@ADAuditStatus", SqlDbType.Int,4),
					new SqlParameter("@HRAuditStatus", SqlDbType.Int,4),
					new SqlParameter("@DirectorId", SqlDbType.Int,4),
					new SqlParameter("@DirectorName", SqlDbType.NVarChar),
					new SqlParameter("@ManagerId", SqlDbType.Int,4),
					new SqlParameter("@ManagerName", SqlDbType.NVarChar),
					new SqlParameter("@IsNormalPer", SqlDbType.Bit,1),
					new SqlParameter("@SumPerformance", SqlDbType.Decimal,9),
                    new SqlParameter("@TotalIndemnityAmount", SqlDbType.Decimal,9),
                    new SqlParameter("@HRReason", SqlDbType.NVarChar,50),
					new SqlParameter("@HRReasonRemark", SqlDbType.NVarChar,500),
                    new SqlParameter("@PreAuditorId", SqlDbType.Int,4),
					new SqlParameter("@PreAuditor", SqlDbType.NVarChar)          
                                        };
            parameters[0].Value = model.DimissionId;
            parameters[1].Value = model.UserId;
            parameters[2].Value = model.UserCode;
            parameters[3].Value = model.UserName;
            parameters[4].Value = model.DepartmentId;
            parameters[5].Value = model.DepartmentName;
            parameters[6].Value = model.LastDay;
            parameters[7].Value = model.AppDate;
            parameters[8].Value = model.Reason;
            parameters[9].Value = model.Status;
            parameters[10].Value = model.CreateTime;
            parameters[11].Value = model.MobilePhone;
            parameters[12].Value = model.PrivateMail;
            parameters[13].Value = model.HopeLastDay;
            parameters[14].Value = model.FinanceAuditStatus;
            parameters[15].Value = model.ITAuditStatus;
            parameters[16].Value = model.ADAuditStatus;
            parameters[17].Value = model.HRAuditStatus;
            parameters[18].Value = model.DirectorId;
            parameters[19].Value = model.DirectorName;
            parameters[20].Value = model.ManagerId;
            parameters[21].Value = model.ManagerName;
            parameters[22].Value = model.IsNormalPer;
            parameters[23].Value = model.SumPerformance;
            parameters[24].Value = model.TotalIndemnityAmount;
            parameters[25].Value = model.HRReason;
            parameters[26].Value = model.HRReasonRemark;
            parameters[27].Value = model.PreAuditorId;
            parameters[28].Value = model.PreAuditor;

            DbHelperSQL.ExecuteSql(strSql.ToString(), stran.Connection, stran, parameters);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public void Delete(int DimissionId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete SEP_DimissionForm ");
            strSql.Append(" where DimissionId=@DimissionId");
            SqlParameter[] parameters = {
					new SqlParameter("@DimissionId", SqlDbType.Int,4)
				};
            parameters[0].Value = DimissionId;
            DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public ESP.HumanResource.Entity.DimissionFormInfo GetModel(int DimissionId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM SEP_DimissionForm ");
            strSql.Append(" WHERE DimissionId=@DimissionId");
            SqlParameter[] parameters = {
					new SqlParameter("@DimissionId", SqlDbType.Int,4)};
            parameters[0].Value = DimissionId;
            ESP.HumanResource.Entity.DimissionFormInfo model = new ESP.HumanResource.Entity.DimissionFormInfo();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            model.DimissionId = DimissionId;
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

        /// <summary>
        /// 通过用户编号获得离职单信息
        /// </summary>
        /// <param name="userid">用户编号</param>
        /// <returns>离职单信息</returns>
        public ESP.HumanResource.Entity.DimissionFormInfo GetModelByUserid(int userid)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM SEP_DimissionForm ");
            strSql.Append(" WHERE Userid=@Userid ");
            SqlParameter[] parameters = {
					new SqlParameter("@Userid", SqlDbType.Int,4)};
            parameters[0].Value = userid;
            ESP.HumanResource.Entity.DimissionFormInfo model = new ESP.HumanResource.Entity.DimissionFormInfo();
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

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * ");
            strSql.Append(" FROM SEP_DimissionForm ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 获得待审批的离职单信息
        /// </summary>
        /// <param name="auditId">审批人编号</param>
        public DataSet GetWaitAuditList(int auditId, string strWhere, List<SqlParameter> prarameters)
        {
            IList<ESP.Framework.Entity.AuditBackUpInfo> auditlist = ESP.Framework.BusinessLogic.AuditBackUpManager.GetModelsByBackUpUserID(auditId);
            ESP.Framework.Entity.AuditBackUpInfo model = null;
            if (auditlist != null && auditlist.Count > 0)
                model = auditlist[0];

            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM SEP_DimissionForm a ");
            strSql.Append(" WHERE dimissionid in(select formid from SEP_AuditLog where  AuditStatus=" + ((int)AuditStatus.NotAudit));
          
                if (model != null)
                    strSql.Append(" AND (AuditorId=" + auditId + " OR AuditorId=" + model.UserID + ")");
                else
                    strSql.Append(" AND (AuditorId=" + auditId + ")");

            strSql.Append(")");

            if (strWhere.Trim() != "")
            {
                strSql.Append(strWhere);
            }
            strSql.Append(" ORDER BY lastday DESC ");
            return DbHelperSQL.Query(strSql.ToString(), prarameters.ToArray());
        }

        public string GetFinanceAuditList(int auditId, int depId)
        {
            IList<ESP.Framework.Entity.AuditBackUpInfo> auditlist = ESP.Framework.BusinessLogic.AuditBackUpManager.GetModelsByBackUpUserID(auditId);
            ESP.Framework.Entity.AuditBackUpInfo model = null;
            int auditDepId = 0;
            if (auditlist != null && auditlist.Count > 0)
                model = auditlist[0];
            if (model != null)
            {
                IList<ESP.Framework.Entity.EmployeePositionInfo> list = ESP.Framework.BusinessLogic.DepartmentPositionManager.GetEmployeePositions(model.UserID);
                if (list != null && list.Count > 0)
                {
                    ESP.Framework.Entity.DepartmentInfo departmentInfo = ESP.Framework.BusinessLogic.DepartmentManager.Get(list[0].DepartmentID);
                    if (departmentInfo != null && departmentInfo.DepartmentLevel == 3)
                    {
                        auditDepId = departmentInfo.DepartmentID;
                    }
                }
            }

            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM SEP_DimissionForm ");
            strSql.Append(" WHERE dimissionid in(select formid from SEP_AuditLog where  AuditStatus=" + ((int)AuditStatus.NotAudit));
          
                if (model != null)
                    strSql.Append(" AND (AuditorId=" + auditId + " OR AuditorId=" + model.UserID + " )");
                else
                    strSql.Append(" AND (AuditorId=" + auditId + ")");
            
            strSql.Append(")");
            string dimissionids = ",";
            DataTable dt = DbHelperSQL.Query(strSql.ToString()).Tables[0];
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                dimissionids += dt.Rows[i]["dimissionid"].ToString() + ",";
            }
            return dimissionids;
        }

        /// <summary>
        /// 获得已经提交的离职单信息
        /// </summary>
        /// <param name="strWhere">查询条件</param>
        /// <param name="prarameters">查询参数</param>
        /// <returns>返回离职单信息</returns>
        public DataSet GetSubmitDimissionList(string strWhere, List<SqlParameter> prarameters)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM SEP_DimissionForm a");
            strSql.Append(" WHERE Status not in(1,12)");

            if (strWhere.Trim() != "")
            {
                strSql.Append(strWhere);
            }
            strSql.Append(" ORDER BY lastday DESC ");
            return DbHelperSQL.Query(strSql.ToString(), prarameters.ToArray());
        }

        /// <summary>
        /// 获得离职单信息。
        /// </summary>
        /// <param name="strWhere">查询条件。</param>
        /// <param name="prarameters">查询参数</param>
        /// <returns>返回离职单信息。</returns>
        public DataSet GetGroupDimissionList(string strWhere, string depId)
        {
            StringBuilder strSql = new StringBuilder();

            strSql.Append("SELECT a.*, p.*,d.level1id,d.level2id,d.level3id,d.level1+'-'+d.level2+'-'+d.level3 as DepartmentName ");
            strSql.Append("FROM sep_dimissionform a ");
            strSql.Append("LEFT JOIN SEP_Employees em ON a.userid = em.userid ");
            strSql.Append("LEFT JOIN ( ");
            strSql.Append("     SELECT * FROM ( ");
            strSql.Append("		SELECT p.*,ROW_NUMBER() OVER(PARTITION BY p.userid ORDER BY p.userid) AS r ");
            strSql.Append("            FROM SEP_EmployeesInPositions p) P WHERE r=1) e ON a.userid = e.userid ");
            strSql.Append("LEFT JOIN SEP_DepartmentPositions p ON p.DepartmentPositionid=e.Departmentpositionid ");
            strSql.Append("LEFT JOIN V_Department d ON e.Departmentid = d.level3Id ");
            strSql.Append(" WHERE a.Status NOT IN (" + ((int)DimissionFormStatus.NotSubmit) + ") AND (d.level1id in (" + depId + ") OR d.level2id in (" + depId + "))");
            if (!string.IsNullOrEmpty(strWhere))
            {
                strSql.Append(strWhere);
            }
            strSql.Append(" ORDER BY lastday DESC ");
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 获得已经提交的离职单信息
        /// </summary>
        /// <param name="strWhere">查询条件</param>
        /// <param name="prarameters">查询参数</param>
        /// <returns>返回离职单信息</returns>
        public DataSet GetUnfinishedDimissionList(string strWhere, List<SqlParameter> prarameters)
        {
            DateTime dt = DateTime.Now.AddMonths(-1);
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM SEP_DimissionForm ");
            strSql.Append(" WHERE Status NOT IN (" + ((int)DimissionFormStatus.NotSubmit) + "," + ((int)DimissionFormStatus.AuditComplete) + ") AND LastDay < '" + dt.ToString("yyyy-MM-dd") + "' ");

            if (strWhere.Trim() != "")
            {
                strSql.Append(strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString(), prarameters.ToArray());
        }

        /// <summary>
        /// 获得已审批通过的离职单信息
        /// </summary>
        /// <param name="strWhere">查询条件</param>
        /// <param name="parameter">查询参数</param>
        /// <returns>返回已审批的离职单信息</returns>
        public DataSet GetAuditedDimissionList(string strWhere, List<SqlParameter> parameter)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" SELECT * FROM sep_auditlog a ");
            strSql.Append(" LEFT JOIN sep_dimissionform d ON a.formid=d.dimissionid ");
            strSql.Append(" WHERE a.formtype=" + (int)ESP.HumanResource.Common.HRFormType.DimissionForm + " AND a.auditstatus != " + (int)ESP.HumanResource.Common.AuditStatus.NotAudit + "");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" AND " + strWhere);
            }
            strSql.Append(" order by d.lastday desc");
            return DbHelperSQL.Query(strSql.ToString(), parameter.ToArray());
        }
        public DataSet GetAllAuditedDimissionList(int currentuserid ,string strWhere, List<SqlParameter> parameter)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" SELECT * FROM sep_auditlog a ");
            strSql.Append(" LEFT JOIN sep_dimissionform d ON a.formid=d.dimissionid ");
            strSql.Append(" WHERE (a.formtype=" + (int)ESP.HumanResource.Common.HRFormType.DimissionForm + " AND a.auditstatus != " + (int)ESP.HumanResource.Common.AuditStatus.NotAudit + " and  a.auditorid="+currentuserid+") or status =12 ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" AND " + strWhere);
            }
            strSql.Append(" order by d.lastday desc");

            return DbHelperSQL.Query(strSql.ToString());
        }
        #endregion  成员方法
    }
}
