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
    /// 数据访问类SupporterDAL。
    /// </summary>
    internal class SupporterDataProvider : ESP.Finance.IDataAccess.ISupporterDataProvider
    {

        #region  成员方法
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int SupportID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from F_Supporter");
            strSql.Append(" where SupportID=@SupportID ");
            SqlParameter[] parameters = {
					new SqlParameter("@SupportID", SqlDbType.Int,4)};
            parameters[0].Value = SupportID;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(ESP.Finance.Entity.SupporterInfo model)
        {
            return Add(model, null);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(ESP.Finance.Entity.SupporterInfo model,SqlTransaction trans)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into F_Supporter(");
            strSql.Append("LeaderCode,LeaderEmployeeName,BudgetAllocation,CompletedPercent,IncomeFee,SupporterCost,BilledTax,ApplicantUserID,ApplicantUserName,ApplicantCode,ProjectID,ApplicantEmployeeName,ApplicantDate,IncomeType,ProjectCode,GroupID,GroupName,ServiceType,ServiceDescription,LeaderUserID,LeaderUserName,BizBeginDate,BizEndDate,Status,SupporterCode,TaxType)");
            strSql.Append(" values (");
            strSql.Append("@LeaderCode,@LeaderEmployeeName,@BudgetAllocation,@CompletedPercent,@IncomeFee,@SupporterCost,@BilledTax,@ApplicantUserID,@ApplicantUserName,@ApplicantCode,@ProjectID,@ApplicantEmployeeName,getdate(),@IncomeType,@ProjectCode,@GroupID,@GroupName,@ServiceType,@ServiceDescription,@LeaderUserID,@LeaderUserName,@BizBeginDate,@BizEndDate,@Status,@SupporterCode,@TaxType)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@LeaderCode", SqlDbType.NVarChar,50),
					new SqlParameter("@LeaderEmployeeName", SqlDbType.NVarChar,50),
					new SqlParameter("@BudgetAllocation", SqlDbType.Decimal,9),
					new SqlParameter("@CompletedPercent", SqlDbType.NVarChar,2000),
					new SqlParameter("@IncomeFee", SqlDbType.Decimal,9),
					new SqlParameter("@SupporterCost", SqlDbType.Decimal,9),
					new SqlParameter("@BilledTax", SqlDbType.Decimal,9),
					new SqlParameter("@ApplicantUserID", SqlDbType.Int,4),
					new SqlParameter("@ApplicantUserName", SqlDbType.NVarChar,50),
					new SqlParameter("@ApplicantCode", SqlDbType.NVarChar,50),
					new SqlParameter("@ProjectID", SqlDbType.Int,4),
					new SqlParameter("@ApplicantEmployeeName", SqlDbType.NVarChar,50),
					//new SqlParameter("@ApplicantDate", SqlDbType.DateTime),
					new SqlParameter("@IncomeType", SqlDbType.NVarChar,50),
					//new SqlParameter("@Lastupdatetime", SqlDbType.Timestamp,8),
					new SqlParameter("@ProjectCode", SqlDbType.NVarChar,50),
					new SqlParameter("@GroupID", SqlDbType.Int,4),
					new SqlParameter("@GroupName", SqlDbType.NVarChar,50),
					new SqlParameter("@ServiceType", SqlDbType.NVarChar,50),
					new SqlParameter("@ServiceDescription", SqlDbType.NVarChar,1000),
					new SqlParameter("@LeaderUserID", SqlDbType.Int,4),
					new SqlParameter("@LeaderUserName", SqlDbType.NVarChar,50),
                    new SqlParameter("@BizBeginDate",SqlDbType.DateTime,8),
                    new SqlParameter("@BizEndDate",SqlDbType.DateTime,8),
                    new SqlParameter("@Status",SqlDbType.Int,4),
                    new SqlParameter("@SupporterCode",SqlDbType.NVarChar,10),
                    new SqlParameter("@TaxType",SqlDbType.Bit)
                                        };
            parameters[0].Value = model.LeaderCode;
            parameters[1].Value = model.LeaderEmployeeName;
            parameters[2].Value = model.BudgetAllocation;
            parameters[3].Value = model.CompletedPercent;
            parameters[4].Value = model.IncomeFee;
            parameters[5].Value = model.SupporterCost;
            parameters[6].Value = model.BilledTax;
            parameters[7].Value = model.ApplicantUserID;
            parameters[8].Value = model.ApplicantUserName;
            parameters[9].Value = model.ApplicantCode;
            parameters[10].Value = model.ProjectID;
            parameters[11].Value = model.ApplicantEmployeeName;
            //parameters[12].Value =model.ApplicantDate;
            parameters[12].Value = model.IncomeType;
            //parameters[14].Value =model.Lastupdatetime;
            parameters[13].Value = model.ProjectCode;
            parameters[14].Value = model.GroupID;
            parameters[15].Value = model.GroupName;
            parameters[16].Value = model.ServiceType;
            parameters[17].Value = model.ServiceDescription;
            parameters[18].Value = model.LeaderUserID;
            parameters[19].Value = model.LeaderUserName;
            parameters[20].Value = model.BizBeginDate;
            parameters[21].Value = model.BizEndDate;
            parameters[22].Value = model.Status;
            parameters[23].Value = model.SupporterCode;
            parameters[24].Value = model.TaxType;

            object obj = DbHelperSQL.GetSingle(strSql.ToString(),trans, parameters);
            if (obj == null)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(obj);
            }
        }

        // /// <summary>
        ///// 根据项目ID更新项目号
        ///// </summary>
        ///// <param name="projectId"></param>
        ///// <param name="projectCode"></param>
        ///// <param name="isInTrans"></param>
        ///// <returns></returns>
        //public int UpdateProjectCode(int projectId, string projectCode)
        //{
        //    return UpdateProjectCode(projectId, projectCode,false);
        //}

        public int UpdateProjectCode(int projectId, string projectCode)
        {
            return UpdateProjectCode(projectId, projectCode, null);
        }
        /// <summary>
        /// 根据项目ID更新项目号
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="projectCode"></param>
        /// <param name="isInTrans"></param>
        /// <returns></returns>
        public int UpdateProjectCode(int projectId, string projectCode,SqlTransaction trans)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update F_Supporter set ");
            strSql.Append(" ProjectCode=@ProjectCode ");
            strSql.Append(" where ProjectID=@ProjectID ");
            SqlParameter[] parameters = {
					new SqlParameter("@ProjectID", SqlDbType.Int,4),
					new SqlParameter("@ProjectCode", SqlDbType.NVarChar,50)};
            parameters[0].Value = projectId;
            parameters[1].Value = projectCode;
            return DbHelperSQL.ExecuteSql(strSql.ToString(),trans, parameters);
        }

        //public int UpdateWorkFlow(int SupportID, int workItemID, string workItemName, int processID, int instanceID)
        //{
        //    return UpdateWorkFlow(SupportID, workItemID, workItemName, processID, instanceID, false);
        //}

        public int UpdateWorkFlow(int SupportID, int workItemID, string workItemName, int processID, int instanceID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update F_Supporter set ");
            strSql.Append("WorkItemID=@WorkItemID,");
            strSql.Append("WorkItemName=@WorkItemName,");
            strSql.Append("ProcessID=@ProcessID,");
            strSql.Append("InstanceID=@InstanceID ");
            strSql.Append(" where SupportID=@SupportID ");
            SqlParameter[] parameters = {
					new SqlParameter("@SupportID", SqlDbType.Int,4),
					new SqlParameter("@WorkItemID", SqlDbType.Int,4),
					new SqlParameter("@WorkItemName", SqlDbType.NVarChar,50),
					new SqlParameter("@ProcessID", SqlDbType.Int,4),
					new SqlParameter("@InstanceID", SqlDbType.Int,4)
                                        };
            parameters[0].Value = SupportID;
            parameters[1].Value = workItemID;
            parameters[2].Value = workItemName;
            parameters[3].Value = processID;
            parameters[4].Value = instanceID;

            return DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public int Update(ESP.Finance.Entity.SupporterInfo model)
        {
            return Update(model, null);
        }
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public int Update(ESP.Finance.Entity.SupporterInfo model,SqlTransaction trans)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update F_Supporter set ");
            strSql.Append("LeaderCode=@LeaderCode,");
            strSql.Append("LeaderEmployeeName=@LeaderEmployeeName,");
            strSql.Append("BudgetAllocation=@BudgetAllocation,");
            strSql.Append("CompletedPercent=@CompletedPercent,");
            strSql.Append("IncomeFee=@IncomeFee,");
            strSql.Append("SupporterCost=@SupporterCost,");
            strSql.Append("BilledTax=@BilledTax,");
            strSql.Append("ApplicantUserID=@ApplicantUserID,");
            strSql.Append("ApplicantUserName=@ApplicantUserName,");
            strSql.Append("ApplicantCode=@ApplicantCode,");
            strSql.Append("ProjectID=@ProjectID,");
            strSql.Append("ApplicantEmployeeName=@ApplicantEmployeeName,");
            strSql.Append("ApplicantDate=getdate(),");
            strSql.Append("IncomeType=@IncomeType,");
            //strSql.Append("Lastupdatetime=@Lastupdatetime,");
            strSql.Append("ProjectCode=@ProjectCode,");
            strSql.Append("GroupID=@GroupID,");
            strSql.Append("GroupName=@GroupName,");
            strSql.Append("ServiceType=@ServiceType,");
            strSql.Append("ServiceDescription=@ServiceDescription,");
            strSql.Append("LeaderUserID=@LeaderUserID,");
            strSql.Append("LeaderUserName=@LeaderUserName, ");
            strSql.Append("BizBeginDate=@BizBeginDate,");
            strSql.Append("BizEndDate=@BizEndDate, ");
            strSql.Append("Status=@Status, ");
            strSql.Append("SupporterCode=@SupporterCode, ");
            strSql.Append("TaxType=@TaxType ");
            strSql.Append(" where SupportID=@SupportID and @Lastupdatetime >= Lastupdatetime ");
            SqlParameter[] parameters = {
					new SqlParameter("@SupportID", SqlDbType.Int,4),
					new SqlParameter("@LeaderCode", SqlDbType.NVarChar,50),
					new SqlParameter("@LeaderEmployeeName", SqlDbType.NVarChar,50),
					new SqlParameter("@BudgetAllocation", SqlDbType.Decimal,9),
					new SqlParameter("@CompletedPercent", SqlDbType.NVarChar,2000),
					new SqlParameter("@IncomeFee", SqlDbType.Decimal,9),
					new SqlParameter("@SupporterCost", SqlDbType.Decimal,9),
					new SqlParameter("@BilledTax", SqlDbType.Decimal,9),
					new SqlParameter("@ApplicantUserID", SqlDbType.Int,4),
					new SqlParameter("@ApplicantUserName", SqlDbType.NVarChar,50),
					new SqlParameter("@ApplicantCode", SqlDbType.NVarChar,50),
					new SqlParameter("@ProjectID", SqlDbType.Int,4),
					new SqlParameter("@ApplicantEmployeeName", SqlDbType.NVarChar,50),
					//new SqlParameter("@ApplicantDate", SqlDbType.DateTime),
					new SqlParameter("@IncomeType", SqlDbType.NVarChar,50),
					new SqlParameter("@ProjectCode", SqlDbType.NVarChar,50),
					new SqlParameter("@GroupID", SqlDbType.Int,4),
					new SqlParameter("@GroupName", SqlDbType.NVarChar,50),
					new SqlParameter("@ServiceType", SqlDbType.NVarChar,50),
					new SqlParameter("@ServiceDescription", SqlDbType.NVarChar,1000),
					new SqlParameter("@LeaderUserID", SqlDbType.Int,4),
					new SqlParameter("@LeaderUserName", SqlDbType.NVarChar,50),
                    new SqlParameter("@Lastupdatetime", SqlDbType.Timestamp,8),
                    new SqlParameter("@BizBeginDate",SqlDbType.DateTime,8),
                    new SqlParameter("@BizEndDate",SqlDbType.DateTime,8),
                    new SqlParameter("@Status",SqlDbType.Int,4),
                    new SqlParameter("@SupporterCode",SqlDbType.NVarChar,10),
                    new SqlParameter("@TaxType",SqlDbType.Bit)
                                        };
            parameters[0].Value = model.SupportID;
            parameters[1].Value = model.LeaderCode;
            parameters[2].Value = model.LeaderEmployeeName;
            parameters[3].Value = model.BudgetAllocation;
            parameters[4].Value = model.CompletedPercent;
            parameters[5].Value = model.IncomeFee;
            parameters[6].Value = model.SupporterCost;
            parameters[7].Value = model.BilledTax;
            parameters[8].Value = model.ApplicantUserID;
            parameters[9].Value = model.ApplicantUserName;
            parameters[10].Value = model.ApplicantCode;
            parameters[11].Value = model.ProjectID;
            parameters[12].Value = model.ApplicantEmployeeName;
            //parameters[13].Value =model.ApplicantDate;
            parameters[13].Value = model.IncomeType;
            parameters[14].Value = model.ProjectCode;
            parameters[15].Value = model.GroupID;
            parameters[16].Value = model.GroupName;
            parameters[17].Value = model.ServiceType;
            parameters[18].Value = model.ServiceDescription;
            parameters[19].Value = model.LeaderUserID;
            parameters[20].Value = model.LeaderUserName;
            parameters[21].Value = model.Lastupdatetime;
            parameters[22].Value = model.BizBeginDate;
            parameters[23].Value = model.BizEndDate;
            parameters[24].Value = model.Status;
            parameters[25].Value = model.SupporterCode;
            parameters[26].Value = model.TaxType;
            return DbHelperSQL.ExecuteSql(strSql.ToString(),trans, parameters);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public int Delete(int SupportID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete F_Supporter ");
            strSql.Append(" where SupportID=@SupportID ");
            SqlParameter[] parameters = {
					new SqlParameter("@SupportID", SqlDbType.Int,4)};
            parameters[0].Value = SupportID;

            return DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public ESP.Finance.Entity.SupporterInfo GetModel(int SupportID,SqlTransaction trans)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"select  top 1 SupportID,LeaderCode,LeaderEmployeeName,
                            BudgetAllocation,CompletedPercent,IncomeFee,SupporterCost,
                            BilledTax,ApplicantUserID,ApplicantUserName,ApplicantCode,
                            ProjectID,ApplicantEmployeeName,ApplicantDate,IncomeType,
                            Lastupdatetime,ProjectCode,GroupID,GroupName,ServiceType,
                            ServiceDescription,LeaderUserID,LeaderUserName,BizBeginDate,
                            BizEndDate,WorkItemID,WorkItemName,ProcessID,InstanceID,
                            Status,SupporterCode,TaxType  from F_Supporter ");
            strSql.Append(" where SupportID=@SupportID ");
            SqlParameter[] parameters = {
					new SqlParameter("@SupportID", SqlDbType.Int,4)};
            parameters[0].Value = SupportID;

            return CBO.FillObject<SupporterInfo>(DbHelperSQL.Query(strSql.ToString(),trans, parameters));
        }

        public ESP.Finance.Entity.SupporterInfo GetModel(int SupportID)
        {
            return GetModel(SupportID, null);
        }

        //public string CreateSupporterCode()
        //{
        //    return CreateSupporterCode(false);
        //}

        public string CreateSupporterCode()
        {
            string prefix = "SR";
            string date = DateTime.Now.ToString("yyMMdd");
            string strSql = "select max(SupporterCode) as maxId from F_Supporter as a where a.SupporterCode like '" + prefix + date + "%'";

            object maxid = DbHelperSQL.GetSingle(strSql);
            int no = maxid == null ? 0 : Convert.ToInt32(maxid.ToString().Substring(8));
            no++;
            return prefix + date + no.ToString("00");
        }

        //public decimal GetTotalAmountByProject(int projectId)
        //{
        //    return GetTotalAmountByProject(projectId, false);
        //}


        public decimal GetTotalAmountByProject(int projectId)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select sum(BudgetAllocation) from F_Supporter");
            strSql.Append(" where ProjectID=@ProjectID  ");
            SqlParameter[] parameters = {
                    new SqlParameter("@ProjectID", SqlDbType.Int,4)};
            parameters[0].Value = projectId;

            object res = DbHelperSQL.GetSingle(strSql.ToString(), parameters);
            if (res != null)
            {
                return Convert.ToDecimal(res);
            }
            return 0;
        }

        public IList<ESP.Finance.Entity.SupporterInfo> GetListByProject(int projectID, string term, List<System.Data.SqlClient.SqlParameter> param)
        {
            return GetListByProject(projectID,term, param, null);
        }

        public IList<ESP.Finance.Entity.SupporterInfo> GetListByProject(int projectID, string term, List<System.Data.SqlClient.SqlParameter> param,SqlTransaction trans)
        {
            if (string.IsNullOrEmpty(term))
            {
                term = " 1=1 ";
            }
            if (param == null)
            {
                param = new List<SqlParameter>();
            }

            term += " and ProjectId = @ProjectId";
            SqlParameter pm = new SqlParameter("@ProjectId", SqlDbType.Int, 4);
            pm.Value = projectID;

            param.Add(pm);

            return GetList(term, param,trans);
        }

        public IList<SupporterInfo> GetList(string term, List<SqlParameter> param)
        {
            return GetList(term, param, null);
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public IList<SupporterInfo> GetList(string term, List<SqlParameter> param,SqlTransaction trans)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"select SupportID,LeaderCode,LeaderEmployeeName,BudgetAllocation,
                            CompletedPercent,IncomeFee,SupporterCost,BilledTax,ApplicantUserID,
                            ApplicantUserName,ApplicantCode,ProjectID,ApplicantEmployeeName,
                            ApplicantDate,IncomeType,Lastupdatetime,ProjectCode,GroupID,
                            GroupName,ServiceType,ServiceDescription,LeaderUserID,LeaderUserName,
                            BizBeginDate,BizEndDate,WorkItemID,WorkItemName,ProcessID,InstanceID,
                            Status,SupporterCode,TaxType ");
            strSql.Append(" FROM F_Supporter");
            if (!string.IsNullOrEmpty(term))
            {
                strSql.Append(" where " + term);
            }
            strSql.Append(" order by Lastupdatetime desc ");
            if (param == null)
            {
                param = new List<SqlParameter>();
            }

            SqlParameter spm = new SqlParameter("@del", (int)Utility.RecordStatus.Del);
            param.Add(spm);
            return CBO.FillCollection<SupporterInfo>(DbHelperSQL.Query(strSql.ToString(), trans, (param == null ? null : param.ToArray())));
        }

        public int GetSupporterCount(int projectid, int supporterid, int groupid)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(*) from F_Supporter");
            strSql.Append(" where ProjectID=@ProjectID  and groupid=@groupid");
            List<SqlParameter> parameters = new List<SqlParameter>();

            SqlParameter p1 = new SqlParameter("@ProjectID", SqlDbType.Int, 4);
            p1.Value = projectid;
            SqlParameter p2 = new SqlParameter("@groupid", SqlDbType.Int, 4);
            p2.Value = groupid;
            parameters.Add(p1);
            parameters.Add(p2);
            if (supporterid != 0)
            {
                strSql.Append(" and supportid!=@supportid");
                SqlParameter p3 = new SqlParameter("@supportid", SqlDbType.Int, 4);
                p3.Value = supporterid;
                parameters.Add(p3);
            }
            object res = DbHelperSQL.GetSingle(strSql.ToString(), parameters.ToArray());
            if (res != null)
            {
                return Convert.ToInt32(res);
            }
            return 0;
        }

        /// <summary>
        /// 与userid相关的项目号信息
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public DataTable GetSupportListJoinHist(int userId)
        {
            string strSql = @"select * from f_supporter as a
                                inner join f_project as c on a.projectid=c.projectid
                                left join F_SupporterAuditHist as b on a.SupportID=b.SupporterID and b.auditstatus=0";
            strSql += " where c.status not in (34)";
            strSql += string.Format(" and (a.leaderUserId={0} or b.auditoruserid={0})", userId);
            return DbHelperSQL.Query(strSql).Tables[0];
        }

        /// <summary>
        /// 变更负责人
        /// </summary>
        /// <param name="returnIds"></param>
        /// <param name="oldUserId"></param>
        /// <param name="newUserId"></param>
        /// <returns></returns>
        public int changeLeader(string supportIds, int oldUserId, int newUserId, ESP.Compatible.Employee currentUser)
        {
            ESP.Compatible.Employee emp = new ESP.Compatible.Employee(newUserId);
            ESP.Compatible.Employee emp1 = new ESP.Compatible.Employee(oldUserId);
            string strSql = " update f_supporter set  LeaderUserID={0}, LeaderUserName='{1}', LeaderCode='{2}', LeaderEmployeeName='{3}'";
            strSql = string.Format(strSql, emp.SysID, emp.ID, emp.ITCode, emp.Name);
            strSql += " where supportId=@supportId and LeaderUserID=" + oldUserId;
            using (SqlConnection conn = new SqlConnection(DbHelperSQL.connectionString))
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();
                try
                {
                    int count = 0;
                    string[] Ids = supportIds.Split(',');
                    foreach (string id in Ids)
                    {
                        SqlParameter parm = new SqlParameter("@supportId", id);
                        if (DbHelperSQL.ExecuteSql(strSql, trans, parm) > 0)
                        {
                            addPermission(id, newUserId, trans);
                            ESP.Finance.Entity.AuditLogInfo log = new AuditLogInfo();
                            log.AuditDate = DateTime.Now;
                            log.AuditorEmployeeName = currentUser.Name;
                            log.AuditorSysID = int.Parse(currentUser.SysID);
                            log.AuditorUserCode = currentUser.ID;
                            log.AuditorUserName = currentUser.ITCode;
                            log.AuditStatus = 1;
                            log.FormID = int.Parse(id);
                            log.FormType = (int)ESP.Finance.Utility.FormType.Supporter;
                            log.Suggestion = "负责人" + emp1.Name + "变更为" + emp.Name;
                            ESP.Finance.BusinessLogic.AuditLogManager.Add(log, trans);
                            count++;
                        }
                    }
                    trans.Commit();
                    return count;
                }
                catch
                {
                    trans.Rollback();
                    return 0;
                }
            }
        }

        /// <summary>
        /// 变更审核人
        /// </summary>
        /// <param name="projectIds"></param>
        /// <param name="oldUserId"></param>
        /// <param name="newUserId"></param>
        /// <returns></returns>
        public int changAuditor(string supportIds, int oldUserId, int newUserId, ESP.Compatible.Employee currentUser)
        {
            ESP.Compatible.Employee emp = new ESP.Compatible.Employee(newUserId);
            ESP.Compatible.Employee emp1 = new ESP.Compatible.Employee(oldUserId);
            string strSql = " update F_SupporterAuditHist set  AuditorUserID={0}, AuditorUserName='{1}', AuditorUserCode='{2}', AuditorEmployeeName='{3}'";
            strSql = string.Format(strSql, emp.SysID, emp.ITCode, emp.ID, emp.Name);
            strSql += " where supporterId=@supporterId and AuditorUserID=" + oldUserId + " and auditestatus=0";
            using (SqlConnection conn = new SqlConnection(DbHelperSQL.connectionString))
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();
                try
                {
                    int count = 0;
                    string[] Ids = supportIds.Split(',');
                    foreach (string id in Ids)
                    {
                        SqlParameter parm = new SqlParameter("@supporterId", id);
                        if (DbHelperSQL.ExecuteSql(strSql, parm) > 0)
                        {
                            addPermission(id, newUserId, trans);
                            ESP.Finance.Entity.AuditLogInfo log = new AuditLogInfo();
                            log.AuditDate = DateTime.Now;
                            log.AuditorEmployeeName = currentUser.Name;
                            log.AuditorSysID = int.Parse(currentUser.SysID);
                            log.AuditorUserCode = currentUser.ID;
                            log.AuditorUserName = currentUser.ITCode;
                            log.AuditStatus = 1;
                            log.FormID = int.Parse(id);
                            log.FormType = (int)ESP.Finance.Utility.FormType.Supporter;
                            log.Suggestion = "审核人" + emp1.Name + "变更为" + emp.Name;
                            ESP.Finance.BusinessLogic.AuditLogManager.Add(log, trans);

                            ESP.Finance.Entity.SupporterInfo Model = GetModel(int.Parse(id), trans);
                            if (Model.ProcessID != null && Model.InstanceID != null)
                            {
                                WorkFlowDAO.ProcessInstanceDao instanceDao = new WorkFlowDAO.ProcessInstanceDao();
                                instanceDao.UpdateRoleWhenLastDay("SR", Model.ProcessID.Value, Model.InstanceID.Value, oldUserId, newUserId, emp1.Name, emp.Name, trans);
                            }
                            count++;
                        }
                    }
                    trans.Commit();
                    return count;
                }
                catch
                {
                    trans.Rollback();
                    return 0;
                }
            }
        }

        private void addPermission(string supportId, int newUserId, SqlTransaction trans)
        {
            ESP.Purchase.Entity.DataInfo dataModel = new ESP.Purchase.DataAccess.DataPermissionProvider().GetDataInfoModel((int)ESP.Purchase.Common.State.DataType.Supporter, int.Parse(supportId), trans);
            if (dataModel == null)
            {
                dataModel = new ESP.Purchase.Entity.DataInfo();
                dataModel.DataType = (int)ESP.Purchase.Common.State.DataType.Supporter;
                dataModel.DataId = int.Parse(supportId);
            }
            ESP.Purchase.Entity.DataPermissionInfo perission = new ESP.Purchase.Entity.DataPermissionInfo();
            perission.UserId = newUserId;
            perission.IsEditor = true;
            perission.IsViewer = true;
            if(dataModel.Id > 0)
                new ESP.Purchase.DataAccess.DataPermissionProvider().AppendDataPermission(dataModel, new List<ESP.Purchase.Entity.DataPermissionInfo>() { perission }, trans);
            else
                new ESP.Purchase.DataAccess.DataPermissionProvider().AddDataPermission(dataModel, new List<ESP.Purchase.Entity.DataPermissionInfo>() { perission }, trans);
        }

        public IList<SupporterInfo> GetWaitAuditList(string userIds, string strTerms, List<SqlParameter> parms)
        {
            System.Data.DataTable dt = DbHelperSQL.RunProcedure("P_GetAllTaskItems", new IDataParameter[1], "Ta").Tables[0];
            DataRow[] rows = dt.Select(" approverid in (" + userIds.TrimEnd(',') + ") and operationType='待审批支持方'");
            string supporterIds = "";
            for (int i = 0; i < rows.Length; i++)
            {
                supporterIds += rows[i]["FromID"].ToString() + ",";
            }
            strTerms = " 1=1" + strTerms;
            strTerms += supporterIds == "" ? " and SupportID=0" : " and SupportID in (" + supporterIds.TrimEnd(',') + ")";
            return GetList(strTerms, parms);
        }


        #endregion  成员方法

        #region ISupporterDataProvider 成员


        public IList<SupporterInfo> GetWaitAuditList(int[] userIds)
        {
            if (userIds == null || userIds.Length == 0)
                return new List<SupporterInfo>();

            StringBuilder sql = new StringBuilder(@"
select a.*
from F_Supporter as a 
inner join (
    select distinct AuditorUserID, AuditorEmployeeName,AuditType,b.SupporterID 
    from F_SupporterAuditHist as a 
    inner join(
        select  min(squencelevel) as squencelevel,SupporterID  from F_SupporterAuditHist where auditstatus=0 group by SupporterID
    ) as b
        on a.squencelevel = b.squencelevel and a.SupporterID=b.SupporterID
) as b 
    on a.SupportID = b.SupporterID
where  Status not in(0,10,19,20,30,32,33,34)
    and b.AuditorUserID in (").Append(userIds[0]);

            for (var i = 1; i < userIds.Length; i++)
            {
                sql.Append(",").Append(userIds[i]);
            }

            sql.Append(")");

            using (var reader = ESP.Finance.DataAccess.DbHelperSQL.ExecuteReader(sql.ToString()))
            {
                return MyPhotoUtility.CBO.FillCollection<SupporterInfo>(reader);
            }
        }

        #endregion
    }
}

