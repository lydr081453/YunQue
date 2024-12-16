using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using ESP.Finance.Entity;
using System.Collections.Generic;
using ESP.Finance.Utility;
using ESP.Purchase.BusinessLogic;

namespace ESP.Finance.DataAccess
{
    /// <summary>
    /// 数据访问类ReturnDAL。
    /// </summary>
    internal class RefundDataProvider : ESP.Finance.IDataAccess.IRefundDataProvider
    {

        #region  成员方法

        public string CreateRefundCode()
        {
            object RccCount = null;
            string refundcode = string.Empty; ;
            object NowDate = DbHelperSQL.GetSingle("Select Substring(Convert(Varchar(6),GetDate(),112),3,4);");
            object RefundCount = DbHelperSQL.GetSingle("select count(Count) from F_RefundCounter where BaseTime like '%" + NowDate.ToString() + "%';");
            if (Convert.ToInt32(RefundCount) > 0)
            {
                RccCount = DbHelperSQL.GetSingle("select Count from F_RefundCounter where BaseTime like '%" + NowDate.ToString() + "%' ");
                DbHelperSQL.ExecuteSql("UPDATE F_RefundCounter SET [Count] = " + (Convert.ToInt32(RccCount) + 1).ToString() + " WHERE BaseTime like '%" + NowDate.ToString() + "%' ");
            }
            else
            {
                DbHelperSQL.ExecuteSql("INSERT INTO F_RefundCounter (BaseTime,Count) VALUES ('" + NowDate.ToString() + "',2)");
                RccCount = "1";
            }
            string strRccCount = RccCount.ToString();
            while (strRccCount.Length < 4)
                strRccCount = "0" + strRccCount;
            refundcode = "RN" + NowDate.ToString() + strRccCount.ToString();

            object rndistinct = DbHelperSQL.GetSingle("select count(*) from F_Refund where Refundcode='" + refundcode + "'");
            if (Convert.ToInt32(rndistinct) == 0)
                return refundcode;
            else
                return "";
        }

        public int Add(List<ESP.Finance.Entity.RefundInfo> refundList)
        {
            int count = 0;
            using (SqlConnection conn = new SqlConnection(DbHelperSQL.connectionString))
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();
                try
                {
                    foreach (ESP.Finance.Entity.RefundInfo model in refundList)
                    {
                        if (Add(model, trans) > 0)
                            count++;
                    }
                    trans.Commit();
                }
                catch
                {
                    trans.Rollback();
                }
                if (count != refundList.Count)
                {
                    trans.Rollback();
                    count = 0;
                }
                return count;
            }
        }
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(ESP.Finance.Entity.RefundInfo model)
        {
            return Add(model, null);
        }

        public int Add(ESP.Finance.Entity.RefundInfo model, System.Data.SqlClient.SqlTransaction trans)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into F_Refund(");
            strSql.Append("PRID,ProjectId,Amounts,RefundDate,Status,Remark,CostId,RefundCode,ProjectCode,DeptId,RequestEmployeeName,RequestDate,SupplierName,SupplierBank,SupplierAccount,ProjectName,RequestorID,PRNO,PaymentUserID)");
            strSql.Append(" values (");
            strSql.Append("@PRID,@ProjectId,@Amounts,@RefundDate,@Status,@Remark,@CostId,@RefundCode,@ProjectCode,@DeptId,@RequestEmployeeName,@RequestDate,@SupplierName,@SupplierBank,@SupplierAccount,@ProjectName,@RequestorID,@PRNO,@PaymentUserID)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@PRID", SqlDbType.Int,4),
					new SqlParameter("@ProjectID", SqlDbType.Int,4),
                    new SqlParameter("@Amounts", SqlDbType.Decimal,9),
					new SqlParameter("@RefundDate", SqlDbType.DateTime),
                    new SqlParameter("@Status", SqlDbType.Int,4),
					new SqlParameter("@Remark", SqlDbType.NVarChar,500),
                    new SqlParameter("@CostId",SqlDbType.Int),
                    new SqlParameter("@RefundCode", SqlDbType.NVarChar,50),
                    new SqlParameter("@ProjectCode", SqlDbType.NVarChar,50),
                    new SqlParameter("@DeptId",SqlDbType.Int),
                    new SqlParameter("@RequestEmployeeName", SqlDbType.NVarChar,50),
                    new SqlParameter("@RequestDate", SqlDbType.DateTime),
                    new SqlParameter("@SupplierName", SqlDbType.NVarChar,200),
                    new SqlParameter("@SupplierBank", SqlDbType.NVarChar,100),
                    new SqlParameter("@SupplierAccount", SqlDbType.NVarChar,50),
                    new SqlParameter("@ProjectName", SqlDbType.NVarChar,200),
                    new SqlParameter("@RequestorID",SqlDbType.Int),
                    new SqlParameter("@PRNO", SqlDbType.NVarChar,50),
                    new SqlParameter("@PaymentUserID",SqlDbType.Int)
                                        };
            parameters[0].Value = model.PRID;
            parameters[1].Value = model.ProjectId;
            parameters[2].Value = model.Amounts;
            parameters[3].Value = model.RefundDate;
            parameters[4].Value = model.Status;
            parameters[5].Value = model.Remark;
            parameters[6].Value = model.CostId;
            parameters[7].Value = model.RefundCode;
            parameters[8].Value = model.ProjectCode;
            parameters[9].Value = model.DeptId;
            parameters[10].Value = model.RequestEmployeeName;
            parameters[11].Value = model.RequestDate;
            parameters[12].Value = model.SupplierName;
            parameters[13].Value = model.SupplierBank;
            parameters[14].Value = model.SupplierAccount;
            parameters[15].Value = model.ProjectName;
            parameters[16].Value = model.RequestorID;
            parameters[17].Value = model.PRNO;
            parameters[18].Value = model.PaymentUserID;

            object obj = DbHelperSQL.GetSingle(strSql.ToString(), trans, parameters);
            if (obj == null)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(obj);
            }
        }

        public int Update(ESP.Finance.Entity.RefundInfo model)
        {
            return Update(model, null);
        }
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public int Update(ESP.Finance.Entity.RefundInfo model, SqlTransaction trans)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update F_Refund set ");
            strSql.Append("PRID=@PRID,ProjectId=@ProjectId,Amounts=@Amounts,RefundDate=@RefundDate,Status=@Status,Remark=@Remark,CostId=@CostId,RefundCode=@RefundCode,ProjectCode=@ProjectCode,DeptId=@DeptId,");
            strSql.Append("RequestEmployeeName=@RequestEmployeeName,RequestDate=@RequestDate,SupplierName=@SupplierName,SupplierBank=@SupplierBank,SupplierAccount=@SupplierAccount,ProjectName=@ProjectName,RequestorID=@RequestorID,PRNO=@PRNO,PaymentUserID=@PaymentUserID ");
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = {
						new SqlParameter("@PRID", SqlDbType.Int,4),
					new SqlParameter("@ProjectID", SqlDbType.Int,4),
                    new SqlParameter("@Amounts", SqlDbType.Decimal,9),
					new SqlParameter("@RefundDate", SqlDbType.DateTime),
                    new SqlParameter("@Status", SqlDbType.Int,4),
					new SqlParameter("@Remark", SqlDbType.NVarChar,500),
                    new SqlParameter("@CostId",SqlDbType.Int),
                    new SqlParameter("@RefundCode", SqlDbType.NVarChar,50),
                    new SqlParameter("@ProjectCode", SqlDbType.NVarChar,50),
                    new SqlParameter("@DeptId",SqlDbType.Int),
                    new SqlParameter("@RequestEmployeeName", SqlDbType.NVarChar,50),
                    new SqlParameter("@RequestDate", SqlDbType.DateTime),
                    new SqlParameter("@SupplierName", SqlDbType.NVarChar,200),
                    new SqlParameter("@SupplierBank", SqlDbType.NVarChar,100),
                    new SqlParameter("@SupplierAccount", SqlDbType.NVarChar,50),
                    new SqlParameter("@ProjectName", SqlDbType.NVarChar,200),
                    new SqlParameter("@RequestorID", SqlDbType.Int,4),
                    new SqlParameter("@PRNO", SqlDbType.NVarChar,50),
                    new SqlParameter("@PaymentUserID", SqlDbType.Int,4),
                    new SqlParameter("@ID", SqlDbType.Int,4)
                                        };
            parameters[0].Value = model.PRID;
            parameters[1].Value = model.ProjectId;
            parameters[2].Value = model.Amounts;
            parameters[3].Value = model.RefundDate;
            parameters[4].Value = model.Status;
            parameters[5].Value = model.Remark;
            parameters[6].Value = model.CostId;
            parameters[7].Value = model.RefundCode;
            parameters[8].Value = model.ProjectCode;
            parameters[9].Value = model.DeptId;
            parameters[10].Value = model.RequestEmployeeName;
            parameters[11].Value = model.RequestDate;
            parameters[12].Value = model.SupplierName;
            parameters[13].Value = model.SupplierBank;
            parameters[14].Value = model.SupplierAccount;
            parameters[15].Value = model.ProjectName;
            parameters[16].Value = model.RequestorID;
            parameters[17].Value = model.PRNO;
            parameters[18].Value = model.PaymentUserID;
            parameters[19].Value = model.Id;
            return DbHelperSQL.ExecuteSql(strSql.ToString(), trans, parameters);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public int Delete(int RefundID)
        {
            int ret = 0;
            ESP.Finance.Entity.RefundInfo refundModel = GetModel(RefundID);
            using (SqlConnection conn = new SqlConnection(DbHelperSQL.connectionString))
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();
                try
                {
                    ret = Delete(RefundID, trans);

                    trans.Commit();
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                }

            }
            return ret;
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public int Delete(int RefundID, SqlTransaction trans)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete F_Refund ");
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)};
            parameters[0].Value = RefundID;

            return DbHelperSQL.ExecuteSql(strSql.ToString(), trans, parameters);
        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public ESP.Finance.Entity.RefundInfo GetModel(int RefundID, SqlTransaction trans)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"select * from F_Refund ");
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)};
            parameters[0].Value = RefundID;
            return CBO.FillObject<RefundInfo>(DbHelperSQL.Query(strSql.ToString(), trans, parameters));

        }

        public ESP.Finance.Entity.RefundInfo GetModel(int RefundID)
        {
            return GetModel(RefundID, null);
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public IList<RefundInfo> GetList(string term, List<SqlParameter> param)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * FROM F_Refund ");
            if (!string.IsNullOrEmpty(term))
            {
                strSql.Append(" where " + term);
            }
            strSql.Append(" order by id desc ");

            using (var reader = DbHelperSQL.ExecuteReader(strSql.ToString(), param))
            {
                return MyPhotoUtility.CBO.FillCollection<RefundInfo>(reader);
            }
        }

        public int GetRecordsCount(string strWhere, List<SqlParameter> parms)
        {

            List<RefundInfo> list = new List<RefundInfo>();
            StringBuilder strB = new StringBuilder();
            strB.Append(" select count(*) from F_Refund ");
            string sql = string.Format(strB.ToString() + " where 1=1 {0}", strWhere);

            return int.Parse(DbHelperSQL.GetSingle(sql, parms.ToArray()).ToString());
        }

        public List<RefundInfo> GetModelListPage(int pageSize, int pageIndex,
    string strWhere, List<SqlParameter> parms)
        {

            List<RefundInfo> list = new List<RefundInfo>();
            StringBuilder strB = new StringBuilder();
            strB.Append("SELECT TOP (@PageSize) * FROM(");

            strB.Append(@" select *,ROW_NUMBER() OVER (ORDER BY refundCode desc) AS [__i_RowNumber] from F_Refund ");

            string sql = string.Format(strB.ToString() + " where 1=1 {0} ", strWhere);
            sql += ") t WHERE t.[__i_RowNumber] > @PageStart order by refundCode desc";
            SqlParameter psize = new SqlParameter("@PageSize", pageSize);
            SqlParameter pstart = new SqlParameter("@PageStart", pageIndex * pageSize);
            parms.Add(psize);
            parms.Add(pstart);
            return ESP.Finance.Utility.CBO.FillCollection<RefundInfo>(ESP.Finance.DataAccess.DbHelperSQL.Query(sql, parms.ToArray()));
        }


        public IList<RefundInfo> GetList(string term, SqlTransaction trans)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * FROM F_Refund ");
            if (!string.IsNullOrEmpty(term))
            {
                strSql.Append(" where " + term);
            }
            strSql.Append(" order by id desc ");

            return CBO.FillCollection<RefundInfo>(DbHelperSQL.Query(strSql.ToString(), trans, null));
        }

        private void addPermission(string RefundID, int newUserId, SqlTransaction trans)
        {
            ESP.Purchase.Entity.DataInfo dataModel = new ESP.Purchase.DataAccess.DataPermissionProvider().GetDataInfoModel((int)Utility.FormType.Refund, int.Parse(RefundID), trans);
            if (dataModel == null)
            {
                dataModel = new ESP.Purchase.Entity.DataInfo();
                dataModel.DataType = (int)Utility.FormType.Refund;
                dataModel.DataId = int.Parse(RefundID);
            }
            ESP.Purchase.Entity.DataPermissionInfo perission = new ESP.Purchase.Entity.DataPermissionInfo();
            perission.UserId = newUserId;
            perission.IsEditor = true;
            perission.IsViewer = true;
            if (dataModel.Id > 0)
                new ESP.Purchase.DataAccess.DataPermissionProvider().AppendDataPermission(dataModel, new List<ESP.Purchase.Entity.DataPermissionInfo>() { perission }, trans);
            else
                new ESP.Purchase.DataAccess.DataPermissionProvider().AddDataPermission(dataModel, new List<ESP.Purchase.Entity.DataPermissionInfo>() { perission }, trans);
        }



        #endregion  成员方法

        #region workflow methods


        public IList<RefundInfo> GetWaitAuditList(string userIds, string strTerms, List<SqlParameter> parms)
        {
            return null;
        }


        public int CommitWorkflow(RefundInfo refundModel, List<ESP.Finance.Entity.WorkFlowInfo> operationList)
        {
            int ret = 0;
            using (SqlConnection conn = new SqlConnection(DbHelperSQL.connectionString))
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();
                try
                {
                    Update(refundModel, trans);
                    ESP.Finance.BusinessLogic.WorkFlowManager.DeleteByModelId(refundModel.Id, (int)ESP.Finance.Utility.FormType.Refund, trans);
                    foreach (WorkFlowInfo wf in operationList)
                    {
                        ESP.Finance.BusinessLogic.WorkFlowManager.Add(wf, trans);
                    }
                    trans.Commit();
                    ret = 1;
                }
                catch
                {
                    trans.Rollback();
                    return 0;
                }
            }
            return ret;
        }

        public IList<RefundInfo> GetWaitAuditList(int[] userIds)
        {
            if (userIds == null || userIds.Length == 0)
                return new List<RefundInfo>();

            StringBuilder sql = new StringBuilder(@"
select distinct a.*
from F_Refund as a inner join F_Workflow as b 
on a.Id = b.ModelId and b.ModelType=13 and b.AuditorUserID=a.PaymentUserid
where  a.Status not in(0,1,90,140,139) and b.AuditStatus=0 and b.AuditorUserID in (").Append(userIds[0]);

            for (var i = 1; i < userIds.Length; i++)
            {
                sql.Append(",").Append(userIds[i]);
            }

            sql.Append(")");


            using (var reader = ESP.Finance.DataAccess.DbHelperSQL.ExecuteReader(sql.ToString()))
            {
                return MyPhotoUtility.CBO.FillCollection<RefundInfo>(reader);
            }
        }

        public int RefundAudit(ESP.Finance.Entity.RefundInfo refundModel, ESP.Compatible.Employee currentUser, int status, string suggestion, int NextFinanceAuditor)
        {
            int ret = 0;
            string term = string.Empty;
            List<SqlParameter> paramlist = new List<SqlParameter>();
            string DelegateUsers = string.Empty;
            ESP.Compatible.Employee NFA = null;

            IList<ESP.Framework.Entity.AuditBackUpInfo> delegates = ESP.Framework.BusinessLogic.AuditBackUpManager.GetModelsByBackUpUserID(int.Parse(currentUser.SysID));
            foreach (ESP.Framework.Entity.AuditBackUpInfo model in delegates)
            {
                DelegateUsers += model.UserID.ToString() + ",";
            }
            DelegateUsers = DelegateUsers.TrimEnd(',');

            term = " ModelId=@ModelId and AuditStatus=@AuditStatus and ModelType=@ModelType";

            SqlParameter p1 = new SqlParameter("@ModelId", SqlDbType.Int, 4);
            p1.SqlValue = refundModel.Id;
            paramlist.Add(p1);

            SqlParameter p2 = new SqlParameter("@AuditStatus", SqlDbType.Int, 4);
            p2.SqlValue = (int)AuditHistoryStatus.UnAuditing;
            paramlist.Add(p2);

            SqlParameter p3 = new SqlParameter("@ModelType", SqlDbType.Int, 4);
            p3.SqlValue = (int)Utility.FormType.Refund;
            paramlist.Add(p3);

            var auditList = ESP.Finance.BusinessLogic.WorkFlowManager.GetList(term, paramlist);

            if (auditList == null || auditList.Count == 0)
            {
                ret = 0;
            }
            else
            {
                //当前审核人
                WorkFlowInfo firstRole = auditList[0];
                //下一级审核人
                WorkFlowInfo nextRole = null;
                if (auditList.Count >= 2)
                    nextRole = auditList[1];

                if (firstRole.AuditorUserID == int.Parse(currentUser.SysID) || DelegateUsers.IndexOf(firstRole.AuditorUserID.ToString()) >= 0)//审核人与登录人校验
                {
                    if (status == (int)AuditHistoryStatus.PassAuditing)
                    {
                        switch (firstRole.AuditType)
                        {
                            case 2://业务第一级审批，状态不变

                                break;
                            case 8://业务第二级审批后到财务第一级审批
                                refundModel.Status = (int)PaymentStatus.MajorAudit;
                                break;
                            case 11://财务第一级审批后到第二级审批
                                refundModel.Status = (int)PaymentStatus.FinanceLevel1;
                                break;
                            case 12://财务第2级审批后到第3级审批
                                refundModel.Status = (int)PaymentStatus.FinanceLevel2;
                                break;
                            case 13://财务第三级审批end
                                refundModel.Status = (int)PaymentStatus.FinanceComplete;
                                break;
                        }
                    }
                    else
                    {
                        refundModel.Status = (int)PaymentStatus.Rejected;
                    }

                    //更新当前审批流角色状态
                    firstRole.AuditStatus = status;
                    firstRole.AuditDate = DateTime.Now;
                    firstRole.Suggestion = suggestion;
                    if (nextRole != null)
                    {
                        refundModel.PaymentUserID = nextRole.AuditorUserID;
                    }
                    else
                    {
                        if (NextFinanceAuditor != 0)
                        {
                            refundModel.PaymentUserID = NextFinanceAuditor;
                            NFA = new Compatible.Employee(NextFinanceAuditor);
                        }
                    }

                    #region begin transaction
                    using (SqlConnection conn = new SqlConnection(DbHelperSQL.connectionString))
                    {
                        conn.Open();
                        SqlTransaction trans = conn.BeginTransaction();
                        try
                        {
                            ESP.Finance.BusinessLogic.RefundManager.Update(refundModel, trans);//更新单据状态
                            ret++;
                            ESP.Finance.BusinessLogic.WorkFlowManager.Update(firstRole, trans);//更新工作流状态
                            ret++;
                            if (NextFinanceAuditor != 0)
                            {

                                ESP.Finance.Entity.WorkFlowInfo auditModel = new ESP.Finance.Entity.WorkFlowInfo();
                                auditModel.ModelId = refundModel.Id;
                                if (refundModel.Status == (int)PaymentStatus.MajorAudit)
                                    auditModel.AuditType = ESP.Finance.Utility.auditorType.operationAudit_Type_Financial;
                                else if (refundModel.Status == (int)PaymentStatus.FinanceLevel1)
                                    auditModel.AuditType = ESP.Finance.Utility.auditorType.operationAudit_Type_Financial2;
                                else if (refundModel.Status == (int)PaymentStatus.FinanceLevel2)
                                    auditModel.AuditType = ESP.Finance.Utility.auditorType.operationAudit_Type_Financial3;
                                else if (refundModel.Status == (int)PaymentStatus.FinanceLevel3)
                                    auditModel.AuditType = ESP.Finance.Utility.auditorType.operationAudit_Type_FinancialMajor;

                                auditModel.AuditorUserID = int.Parse(NFA.SysID);
                                auditModel.AuditorUserCode = NFA.ID;
                                auditModel.AuditorEmployeeName = NFA.Name;
                                auditModel.AuditorUserName = NFA.ITCode;
                                auditModel.AuditStatus = (int)ESP.Finance.Utility.AuditHistoryStatus.UnAuditing;
                                auditModel.ModelType = (int)ESP.Finance.Utility.FormType.Refund;
                                ESP.Finance.BusinessLogic.WorkFlowManager.Add(auditModel, trans);
                            }
                            trans.Commit();
                        }
                        catch
                        {
                            trans.Rollback();
                        }
                        finally
                        {
                            if (trans.Connection != null && trans.Connection.State != ConnectionState.Closed)
                            {
                                trans.Connection.Close();
                            }
                            if (trans != null)
                                trans = null;
                        }
                    }
                    #endregion

                    try
                    {
                        ESP.HumanResource.Entity.EmployeeBaseInfo creator = ESP.HumanResource.BusinessLogic.EmployeeBaseManager.GetModel(refundModel.RequestorID);

                        ESP.HumanResource.Entity.EmployeeBaseInfo nextEmp = null;
                        string nextAuditorMail = string.Empty;
                        if (nextRole != null)
                        {
                            nextEmp = ESP.HumanResource.BusinessLogic.EmployeeBaseManager.GetModel(nextRole.AuditorUserID);
                            nextAuditorMail = nextEmp.InternalEmail;
                        }

                        ESP.Finance.Utility.SendMailHelper.SendMailRefundAudit(refundModel, creator.InternalEmail, currentUser.Name, nextAuditorMail);
                    }
                    catch { }
                }
                else
                {
                    ret = 0;
                }
            }

            return ret;
        }
        #endregion

    }
}
