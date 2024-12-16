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
    internal class ConsumptionProvider : ESP.Finance.IDataAccess.IConsumptionProvider
    {
        #region  成员方法
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int ConsumptionId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from F_Consumption");
            strSql.Append(" where Id=@Id ");
            SqlParameter[] parameters = {
					new SqlParameter("@Id", SqlDbType.Int,4)};
            parameters[0].Value = ConsumptionId;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(ESP.Finance.Entity.ConsumptionInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into F_Consumption(");
            strSql.Append("BatchId,OrderYM,ProjectId,ProjectCode,Description,Amount,Media,OrderType,ErrorContent,JSCode,XMCode,RowNo,ReturnId)");
            strSql.Append(" values (");
            strSql.Append("@BatchId,@OrderYM,@ProjectId,@ProjectCode,@Description,@Amount,@Media,@OrderType,@ErrorContent,@JSCode,@XMCode,@RowNo,@ReturnId)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@BatchId", SqlDbType.Int),
					new SqlParameter("@OrderYM", SqlDbType.NVarChar,50),
                    new SqlParameter("@ProjectId", SqlDbType.Int),
					new SqlParameter("@ProjectCode", SqlDbType.NVarChar,50),
					new SqlParameter("@Description", SqlDbType.NVarChar,500),
					new SqlParameter("@Amount", SqlDbType.Decimal,9),
	                new SqlParameter("@Media", SqlDbType.NVarChar,50),
					new SqlParameter("@OrderType", SqlDbType.NVarChar,50),
                    new SqlParameter("@ErrorContent", SqlDbType.NVarChar,500),
                    new SqlParameter("@JSCode", SqlDbType.NVarChar,50),
                    new SqlParameter("@XMCode", SqlDbType.NVarChar,50),
                    new SqlParameter("@RowNo", SqlDbType.Int),
                    new SqlParameter("@ReturnId", SqlDbType.Int)
                                        };
            parameters[0].Value = model.BatchId;
            parameters[1].Value = model.OrderYM;
            parameters[2].Value = model.ProjectId;
            parameters[3].Value = model.ProjectCode;
            parameters[4].Value = model.Description;
            parameters[5].Value = model.Amount;
            parameters[6].Value = model.Media;
            parameters[7].Value = model.OrderType;
            parameters[8].Value = model.ErrorContent;
            parameters[9].Value = model.JSCode;
            parameters[10].Value = model.XMCode;
            parameters[11].Value = model.RowNo;
            parameters[12].Value = model.ReturnId;
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
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public int Update(ESP.Finance.Entity.ConsumptionInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update F_Consumption set ");
            strSql.Append("BatchId=@BatchId,OrderYM=@OrderYM,ProjectId=@ProjectId,ProjectCode=@ProjectCode,Description=@Description,Amount=@Amount,Media=@Media,");
            strSql.Append("OrderType=@OrderType,ErrorContent=@ErrorContent,JSCode=@JSCode,XMCode=@XMCode,RowNo=@RowNo,ReturnId=ReturnId ");
            strSql.Append(" where Id=@Id ");
            SqlParameter[] parameters = {
					new SqlParameter("@Id", SqlDbType.Int,4),
					new SqlParameter("@BatchId", SqlDbType.Int),
					new SqlParameter("@OrderYM", SqlDbType.NVarChar,50),
                    new SqlParameter("@ProjectId", SqlDbType.Int),
					new SqlParameter("@ProjectCode", SqlDbType.NVarChar,50),
					new SqlParameter("@Description", SqlDbType.NVarChar,500),
					new SqlParameter("@Amount", SqlDbType.Decimal,9),
	                new SqlParameter("@Media", SqlDbType.NVarChar,50),
					new SqlParameter("@OrderType", SqlDbType.NVarChar,50),
                    new SqlParameter("@ErrorContent", SqlDbType.NVarChar,500),
                    new SqlParameter("@JSCode", SqlDbType.NVarChar,50),
                    new SqlParameter("@XMCode", SqlDbType.NVarChar,50),
                    new SqlParameter("@RowNo", SqlDbType.Int,4),
                     new SqlParameter("@ReturnId", SqlDbType.Int,4)
                                        };
            parameters[0].Value = model.Id;
            parameters[1].Value = model.BatchId;
            parameters[2].Value = model.OrderYM;
            parameters[3].Value = model.ProjectId;
            parameters[4].Value = model.ProjectCode;
            parameters[5].Value = model.Description;
            parameters[6].Value = model.Amount;
            parameters[7].Value = model.Media;
            parameters[8].Value = model.OrderType;
            parameters[9].Value = model.ErrorContent;
            parameters[10].Value = model.JSCode;
            parameters[11].Value = model.XMCode;
            parameters[12].Value = model.RowNo;
            parameters[13].Value = model.ReturnId;
            return DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }

        public int Update(ESP.Finance.Entity.ConsumptionInfo model, SqlTransaction trans)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update F_Consumption set ");
            strSql.Append("BatchId=@BatchId,OrderYM=@OrderYM,ProjectId=@ProjectId,ProjectCode=@ProjectCode,Description=@Description,Amount=@Amount,Media=@Media,");
            strSql.Append("OrderType=@OrderType,ErrorContent=@ErrorContent,JSCode=@JSCode,XMCode=@XMCode,RowNo=@RowNo,ReturnId=@ReturnId ");
            strSql.Append(" where Id=@Id ");
            SqlParameter[] parameters = {
					new SqlParameter("@Id", SqlDbType.Int,4),
					new SqlParameter("@BatchId", SqlDbType.Int),
					new SqlParameter("@OrderYM", SqlDbType.NVarChar,50),
                    new SqlParameter("@ProjectId", SqlDbType.Int),
					new SqlParameter("@ProjectCode", SqlDbType.NVarChar,50),
					new SqlParameter("@Description", SqlDbType.NVarChar,500),
					new SqlParameter("@Amount", SqlDbType.Decimal,9),
	                new SqlParameter("@Media", SqlDbType.NVarChar,50),
					new SqlParameter("@OrderType", SqlDbType.NVarChar,50),
                    new SqlParameter("@ErrorContent", SqlDbType.NVarChar,500),
                    new SqlParameter("@JSCode", SqlDbType.NVarChar,50),
                    new SqlParameter("@XMCode", SqlDbType.NVarChar,50),
                    new SqlParameter("@RowNo", SqlDbType.Int,4),
                     new SqlParameter("@ReturnId", SqlDbType.Int,4)
                                        };
            parameters[0].Value = model.Id;
            parameters[1].Value = model.BatchId;
            parameters[2].Value = model.OrderYM;
            parameters[3].Value = model.ProjectId;
            parameters[4].Value = model.ProjectCode;
            parameters[5].Value = model.Description;
            parameters[6].Value = model.Amount;
            parameters[7].Value = model.Media;
            parameters[8].Value = model.OrderType;
            parameters[9].Value = model.ErrorContent;
            parameters[10].Value = model.JSCode;
            parameters[11].Value = model.XMCode;
            parameters[12].Value = model.RowNo;
            parameters[13].Value = model.ReturnId;
            return DbHelperSQL.ExecuteSql(strSql.ToString(), trans, parameters);
        }


        /// <summary>
        /// 删除一条数据
        /// </summary>
        public int Delete(int ConsumptionId)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete F_Consumption ");
            strSql.Append(" where Id=@Id ");
            SqlParameter[] parameters = {
					new SqlParameter("@Id", SqlDbType.Int,4)};
            parameters[0].Value = ConsumptionId;

            return DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }

        public int DeleteByBatch(int batchId, SqlTransaction trans)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete F_Consumption ");
            strSql.Append(" where batchId=@batchId ");
            SqlParameter[] parameters = {
					new SqlParameter("@batchId", SqlDbType.Int,4)};
            parameters[0].Value = batchId;

            return DbHelperSQL.ExecuteSql(strSql.ToString(), trans, parameters);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public ESP.Finance.Entity.ConsumptionInfo GetModel(int ConsumptionId)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from F_Consumption ");
            strSql.Append(" where Id=@Id ");
            SqlParameter[] parameters = {
					new SqlParameter("@Id", SqlDbType.Int,4)};
            parameters[0].Value = ConsumptionId;

            return CBO.FillObject<ConsumptionInfo>(DbHelperSQL.Query(strSql.ToString(), parameters));


        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public IList<ConsumptionInfo> GetList(string term, List<SqlParameter> param)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select* FROM F_Consumption ");
            if (!string.IsNullOrEmpty(term))
            {
                strSql.Append(" where " + term);
            }
            return CBO.FillCollection<ConsumptionInfo>(DbHelperSQL.Query(strSql.ToString(), param));
        }
        /// <summary>
        /// 获取占用成本的消耗
        /// </summary>
        /// <param name="term"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public IList<ConsumptionInfo> GetCostList(string term)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select a.*,c.PurchaseBatchCode FROM F_Consumption a join f_return b on a.returnid =b.returnid join F_PNBatch c on a.BatchId = c.BatchID where b.returnstatus =140");
            if (!string.IsNullOrEmpty(term))
            {
                strSql.Append(" and " + term);
            }
            return CBO.FillCollection<ConsumptionInfo>(DbHelperSQL.Query(strSql.ToString()));
        }

        public int Add(ESP.Finance.Entity.ConsumptionInfo model, System.Data.SqlClient.SqlTransaction trans)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into F_Consumption(");
            strSql.Append("BatchId,OrderYM,ProjectId,ProjectCode,Description,Amount,Media,OrderType,ErrorContent,JSCode,XMCode,RowNo,ReturnId)");
            strSql.Append(" values (");
            strSql.Append("@BatchId,@OrderYM,@ProjectId,@ProjectCode,@Description,@Amount,@Media,@OrderType,@ErrorContent,@JSCode,@XMCode,@RowNo,@ReturnId)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@BatchId", SqlDbType.Int),
					new SqlParameter("@OrderYM", SqlDbType.NVarChar,50),
                    new SqlParameter("@ProjectId", SqlDbType.Int),
					new SqlParameter("@ProjectCode", SqlDbType.NVarChar,50),
					new SqlParameter("@Description", SqlDbType.NVarChar,500),
					new SqlParameter("@Amount", SqlDbType.Decimal,9),
	                new SqlParameter("@Media", SqlDbType.NVarChar,50),
					new SqlParameter("@OrderType", SqlDbType.NVarChar,50),
                    new SqlParameter("@ErrorContent", SqlDbType.NVarChar,500),
                    new SqlParameter("@JSCode", SqlDbType.NVarChar,50),
                    new SqlParameter("@XMCode", SqlDbType.NVarChar,50),
                    new SqlParameter("@RowNo", SqlDbType.Int),
                    new SqlParameter("@ReturnId", SqlDbType.Int)
                                        };
            parameters[0].Value = model.BatchId;
            parameters[1].Value = model.OrderYM;
            parameters[2].Value = model.ProjectId;
            parameters[3].Value = model.ProjectCode;
            parameters[4].Value = model.Description;
            parameters[5].Value = model.Amount;
            parameters[6].Value = model.Media;
            parameters[7].Value = model.OrderType;
            parameters[8].Value = model.ErrorContent;
            parameters[9].Value = model.JSCode;
            parameters[10].Value = model.XMCode;
            parameters[11].Value = model.RowNo;
            parameters[12].Value = model.ReturnId;
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

        /// <summary>
        /// 批量事务导入消耗
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public int ImpList(PNBatchInfo batchModel, List<ESP.Finance.Entity.ConsumptionInfo> list)
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

                    foreach (ESP.Finance.Entity.ConsumptionInfo model in list)
                    {
                        model.BatchId = batchId;
                        model.ReturnId = 0;
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
                        role.FormType = (int)ESP.Finance.Utility.FormType.Consumption;
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
                SendMailHelper.SendMailConsumptionCommit(batchModel, firstRole.InternalEmail);
            }
            catch { }

            return icount;
        }

        public IList<ConsumptionInfo> GetAuditingConsumption(int projectId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select a.* FROM F_Consumption a join f_pnbatch b on a.batchid =b.batchid where b.status in(2,100) and a.projectId=@projectId");
            List<System.Data.SqlClient.SqlParameter> paramlist = new List<System.Data.SqlClient.SqlParameter>();
            SqlParameter paramPid = new SqlParameter("@projectId", SqlDbType.Int, 4);
            paramPid.Value = projectId;
            paramlist.Add(paramPid);

            return CBO.FillCollection<ConsumptionInfo>(DbHelperSQL.Query(strSql.ToString(),paramlist));
        }

        #endregion  成员方法

    }
}
