using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using ESP.Finance.Entity;
using ESP.Finance.Utility;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
namespace ESP.Finance.DataAccess
{
    /// <summary>
    /// 数据访问类F_RebateRegistration。
    /// </summary>
    internal class RebateRegistrationDataProvider : ESP.Finance.IDataAccess.IRebateRegistrationDataProvider
    {
        
        #region  成员方法
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int Id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from F_RebateRegistration");
            strSql.Append(" where Id=@Id ");
            SqlParameter[] parameters = {
					new SqlParameter("@Id", SqlDbType.Int,4)};
            parameters[0].Value = Id;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(ESP.Finance.Entity.RebateRegistrationInfo model,SqlTransaction trans)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into F_RebateRegistration(");
            strSql.Append("ProjectId, SupplierId, RebateAmount, CreditedDate, Remark, Status, CreateDate, CreateUserId,BatchId,AccountingNum,SettleType,Branch)");
            strSql.Append(" values (");
            strSql.Append("@ProjectId, @SupplierId, @RebateAmount, @CreditedDate, @Remark, @Status, @CreateDate, @CreateUserId,@BatchId,@AccountingNum,@SettleType,@Branch)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@ProjectId", SqlDbType.Int),
					new SqlParameter("@SupplierId", SqlDbType.Int),
					new SqlParameter("@RebateAmount", SqlDbType.Decimal,9),
					new SqlParameter("@CreditedDate", SqlDbType.NVarChar),
					new SqlParameter("@Remark", SqlDbType.NVarChar),
                    new SqlParameter("@Status", SqlDbType.Int),
                    new SqlParameter("@CreateDate", SqlDbType.DateTime),
                    new SqlParameter("@CreateUserId", SqlDbType.Int),
                    new SqlParameter("@BatchId",SqlDbType.Int),
                                        new SqlParameter("@AccountingNum", SqlDbType.NVarChar),
                                        new SqlParameter("@SettleType", SqlDbType.NVarChar),
                                        new SqlParameter("@Branch", SqlDbType.NVarChar),};
            parameters[0].Value =model.ProjectId;
            parameters[1].Value =model.SupplierId;
            parameters[2].Value =model.RebateAmount;
            parameters[3].Value =model.CreditedDate;
            parameters[4].Value =model.Remark;
            parameters[5].Value = model.Status;
            parameters[6].Value = model.CreateDate;
            parameters[7].Value = model.CreateUserId;
            parameters[8].Value = model.BatchId;
            parameters[9].Value = model.AccountingNum;
            parameters[10].Value = model.SettleType;
            parameters[11].Value = model.Branch;

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

        public int Add(ESP.Finance.Entity.RebateRegistrationInfo model)
        {
            return Add(model, null);
        }

        public int Update(ESP.Finance.Entity.RebateRegistrationInfo model)
        {
            return Update(model, null);
        }
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public int Update(ESP.Finance.Entity.RebateRegistrationInfo model,SqlTransaction trans)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update F_RebateRegistration set ");
            strSql.Append("ProjectId=@ProjectId,");
            strSql.Append("SupplierId=@SupplierId,");
            strSql.Append("RebateAmount=@RebateAmount,");
            strSql.Append("CreditedDate=@CreditedDate,");
            strSql.Append("Remark=@Remark,Status=@Status,AccountingNum=@AccountingNum,SettleType=@SettleType,Branch=@Branch");
            strSql.Append(" where Id=@Id ");
            SqlParameter[] parameters = {
					new SqlParameter("@Id", SqlDbType.Int,4),
					new SqlParameter("@ProjectId", SqlDbType.Int),
					new SqlParameter("@SupplierId", SqlDbType.Int),
					new SqlParameter("@RebateAmount", SqlDbType.Decimal,9),
					new SqlParameter("@CreditedDate", SqlDbType.NVarChar),
					new SqlParameter("@Remark", SqlDbType.NVarChar),
                    new SqlParameter("@Status", SqlDbType.Int),
                    new SqlParameter("@AccountingNum", SqlDbType.NVarChar),
                    new SqlParameter("@SettleType", SqlDbType.NVarChar),
                                        new SqlParameter("@Branch", SqlDbType.NVarChar),};
            parameters[0].Value =model.Id;
            parameters[1].Value =model.ProjectId;
            parameters[2].Value =model.SupplierId;
            parameters[3].Value =model.RebateAmount;
            parameters[4].Value =model.CreditedDate;
            parameters[5].Value =model.Remark;
            parameters[6].Value = model.Status;
            parameters[7].Value = model.AccountingNum;
            parameters[8].Value = model.SettleType;
            parameters[9].Value = model.Branch;

            return DbHelperSQL.ExecuteSql(strSql.ToString(),trans, parameters);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public int Delete(int Id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete F_RebateRegistration ");
            strSql.Append(" where Id=@Id ");
            SqlParameter[] parameters = {
					new SqlParameter("@Id", SqlDbType.Int,4)};
            parameters[0].Value = Id;

            return DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }

        public int DeleteByBatch(int batchId, SqlTransaction trans)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete F_RebateRegistration ");
            strSql.Append(" where batchId=@batchId ");
            SqlParameter[] parameters = {
					new SqlParameter("@batchId", SqlDbType.Int,4)};
            parameters[0].Value = batchId;

            return DbHelperSQL.ExecuteSql(strSql.ToString(), trans, parameters);
        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public ESP.Finance.Entity.RebateRegistrationInfo GetModel(int Id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 * from F_RebateRegistration ");
            strSql.Append(" where Id=@Id ");
            SqlParameter[] parameters = {
					new SqlParameter("@Id", SqlDbType.Int,4)};
            parameters[0].Value = Id;

            return CBO.FillObject<RebateRegistrationInfo>(DbHelperSQL.Query(strSql.ToString(), parameters));


        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public IList<RebateRegistrationInfo> GetList(string term,List<SqlParameter> param)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select a.*,b.PurchaseBatchCode ");
            strSql.Append(" FROM F_RebateRegistration a join f_pnbatch b on a.batchid= b.batchid ");
            if (!string.IsNullOrEmpty(term))
            {
                strSql.Append(" where " + term);
            }
            //if (param != null && param.Count > 0)
            //{
            //    SqlParameter[] ps = param.ToArray();
            //    return CBO.FillCollection<F_RebateRegistration>(DbHelperSQL.ExecuteReader(strSql.ToString(),ps));
            //}
            return CBO.FillCollection < RebateRegistrationInfo >( DbHelperSQL.Query(strSql.ToString(),param));
        }

         /// <summary>
        /// 批量事务导入消耗
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public int ImpList(PNBatchInfo batchModel, List<ESP.Finance.Entity.RebateRegistrationInfo> list)
        {
            int icount = 0;
            int workflowseq = 1;
            List<string> workflowUsers = new List<string>();

            workflowUsers = ConfigurationManager.AppSettings["ConsumptionWorkFlow"].Split(new char[] { ',' }).ToList<string>();
            if (batchModel.Amounts.Value >= 100000)
            {
                workflowUsers = ConfigurationManager.AppSettings["ConsumptionWorkFlow10W"].Split(new char[] { ',' }).ToList<string>();
            }

            ESP.HumanResource.Entity.EmployeeBaseInfo firstRole = ESP.HumanResource.BusinessLogic.EmployeeBaseManager.GetModel(int.Parse(workflowUsers[0]));


            using (SqlConnection conn = new SqlConnection(DbHelperSQL.connectionString))
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();
                try
                {
                    batchModel.PaymentUserID = firstRole.UserID;
                    batchModel.PaymentEmployeeName = firstRole.FullNameCN;
                    batchModel.PaymentUserName = firstRole.Username;
                    batchModel.PaymentCode = firstRole.Code;

                    int batchId = ESP.Finance.BusinessLogic.PNBatchManager.Add(batchModel, trans);

                    foreach (ESP.Finance.Entity.RebateRegistrationInfo model in list)
                    {
                        model.BatchId = batchId;
                        Add(model, trans);
                        icount++;
                    }

                    foreach (string userid in workflowUsers)
                    {
                        ESP.Finance.Entity.ConsumptionAuditInfo role = new ESP.Finance.Entity.ConsumptionAuditInfo();
                        ESP.HumanResource.Entity.EmployeeBaseInfo emp = ESP.HumanResource.BusinessLogic.EmployeeBaseManager.GetModel(int.Parse(userid));
                        role.AuditorEmployeeName = emp.FullNameCN;
                        role.AuditorUserCode = emp.Code;
                        role.AuditorUserID = emp.UserID;
                        role.AuditorUserName = emp.Username;
                        role.BatchID = batchId;
                        role.AuditStatus = (int)AuditHistoryStatus.UnAuditing;
                        role.SquenceLevel = workflowseq;
                        role.FormType = (int)ESP.Finance.Utility.FormType.RebateRegistration;
                        switch (workflowseq)
                        {
                            case 1:
                                role.AuditType = (int)auditorType.operationAudit_Type_ZJSP;
                                break;
                            case 2:
                                role.AuditType = (int)auditorType.operationAudit_Type_ZJLSP;
                                break;
                            case 3:
                                role.AuditType = (int)auditorType.operationAudit_Type_Financial;
                                break;
                            case 4:
                                if (batchModel.Amounts >= 100000)
                                    role.AuditType = (int)auditorType.operationAudit_Type_Financial2;
                                else
                                    role.AuditType = (int)auditorType.operationAudit_Type_Financial3;
                                break;
                            case 5:
                                role.AuditType = (int)auditorType.operationAudit_Type_Financial3;
                                break;
                        }
                        workflowseq++;
                        ESP.Finance.BusinessLogic.ConsumptionAuditManager.Add(role, trans);
                    }

                    trans.Commit();
                }
                catch
                {
                    trans.Rollback();
                    return 0;
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

            try
            {
                SendMailHelper.SendMailRebateRegistrationCommit(batchModel, firstRole.InternalEmail);
            }
            catch { }

            return icount;
        }

        #endregion  成员方法
    }
}