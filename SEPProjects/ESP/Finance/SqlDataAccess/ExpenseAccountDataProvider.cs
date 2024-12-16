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
    internal class ExpenseAccountDataProvider : ESP.Finance.IDataAccess.IExpenseAccountProvider
    {

        #region  成员方法

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(ESP.Finance.Entity.ExpenseAccountInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into F_ExpenseAccount(");
            strSql.Append("ReturnID,ConfirmFee)");
            strSql.Append(" values (");
            strSql.Append("@ReturnID,@ConfirmFee)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@ReturnID", SqlDbType.Int,4),
					new SqlParameter("@ConfirmFee", SqlDbType.Decimal,9)
					};
            parameters[0].Value = model.ReturnID;
            parameters[1].Value = model.ConfirmFee;

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
        /// 更新一条数据
        /// </summary>
        public int Update(ESP.Finance.Entity.ExpenseAccountInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update F_ExpenseAccount set ");
            strSql.Append("ReturnID=@ReturnID,");
            strSql.Append("ConfirmFee=@ConfirmFee");
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4),
					new SqlParameter("@ReturnID", SqlDbType.Int,4),
					new SqlParameter("@ConfirmFee", SqlDbType.Decimal,9)};
            parameters[0].Value = model.ID;
            parameters[1].Value = model.ReturnID;
            parameters[2].Value = model.ConfirmFee;

            return DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }

        public int CloseWorkFlow(Guid instanceId,int operatorId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update wf_WorkflowInstances set status=@Status where InstanceId=@InstanceId; ");
            strSql.Append("update wf_WorkItems set status=@WorkItemStatus,operatorId=@operatorId,closedBy=@operatorId,closedTime=getdate() where WorkflowInstanceId=@InstanceId and webPage like '%step=f4%'; ");
            SqlParameter[] parameters = {
					new SqlParameter("@Status", SqlDbType.Int,4),
					new SqlParameter("@InstanceId", SqlDbType.UniqueIdentifier),
                    new SqlParameter("@WorkItemStatus", SqlDbType.Int,4),
                    new SqlParameter("@operatorId", SqlDbType.Int,4)
                                        };

            parameters[0].Value = (int)ESP.Workflow.WorkflowProcessStatus.Closed;
            parameters[1].Value = instanceId;
            parameters[2].Value = 1;
            parameters[3].Value = operatorId;

            return DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }
        public int WorkFlowRejectToFinance1(Guid instanceId, int returnId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete wf_WorkItems where WorkflowInstanceId=@InstanceId and ParticipantName in ('财务第二级','财务第三级','财务第四级'); ");
            strSql.Append("update wf_WorkItems set status=@WorkItemStatus where WorkflowInstanceId=@InstanceId and ParticipantName='财务第一级'; ");
            strSql.Append("update wf_WorkflowInstances set Threads=@Threads,EventQueues=@EventQueues where InstanceId=@InstanceId; ");
            strSql.Append("update f_return set returnStatus=@returnStatus where returnId=@returnId; ");
            SqlParameter[] parameters = {
					new SqlParameter("@InstanceId", SqlDbType.UniqueIdentifier),
                    new SqlParameter("@WorkItemStatus", SqlDbType.Int,4),
                    new SqlParameter("@Threads", SqlDbType.NText),
                    new SqlParameter("@EventQueues", SqlDbType.NText),
                    new SqlParameter("@returnStatus", SqlDbType.Int,4),
                    new SqlParameter("@returnId", SqlDbType.Int,4)
                                        };

            parameters[0].Value = instanceId;
            parameters[1].Value = 0;
            parameters[2].Value = "<threads><thread activitySetId=\"_iziLsHafEd6If49b21rqkA\" currentActivityId=\"_Rlxp5nagEd6If49b21rqkA\" threadId=\"0\" token=\"/None.0\" activityStatus=\"Excute\" status=\"Blocked\"><item key=\"Participants\" dataFormat=\"ArrayXml\"><array elementType=\"System.Int32, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089\" /></item><item key=\"WorkItemDispatched\" dataFormat=\"Boolean\">True</item></thread></threads>";
            parameters[3].Value = "<eventQueues><eventQueue key=\"/None.0\" threadId=\"0\" participant=\"FinanceLevel1\"><eventName name=\"Signal/Approved\" /><eventName name=\"Signal/Rejected\" /></eventQueue></eventQueues>";
            parameters[4].Value = (int)ESP.Finance.Utility.PaymentStatus.CEOAudit;
            parameters[5].Value = returnId;

            return DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }

        public int WorkFlowReject(Guid instanceId, int returnId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete wf_WorkItems where WorkflowInstanceId=@InstanceId; ");
            strSql.Append("delete wf_WorkflowInstances where InstanceId=@InstanceId; ");
            strSql.Append("update f_return set returnStatus=@returnStatus where returnId=@returnId; ");
            SqlParameter[] parameters = {
					new SqlParameter("@InstanceId", SqlDbType.UniqueIdentifier),
                    new SqlParameter("@returnStatus", SqlDbType.Int,4),
                    new SqlParameter("@returnId", SqlDbType.Int,4)
                                        };

            parameters[0].Value = instanceId;
            parameters[1].Value = (int)ESP.Finance.Utility.PaymentStatus.Rejected;
            parameters[2].Value = returnId;

            return DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public int Delete(int ID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete F_ExpenseAccount ");
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)};
            parameters[0].Value = ID;

            return DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public ESP.Finance.Entity.ExpenseAccountInfo GetModel(int ID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 ID,ReturnID,ConfirmFee from F_ExpenseAccount ");
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)};
            parameters[0].Value = ID;

            return CBO.FillObject<ExpenseAccountInfo>(DbHelperSQL.Query(strSql.ToString(), parameters));
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public ESP.Finance.Entity.ExpenseAccountInfo GetModelByReturnID(int ReturnID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 ID,ReturnID,ConfirmFee from F_ExpenseAccount ");
            strSql.Append(" where ReturnID=@ReturnID  order by id desc ");
            SqlParameter[] parameters = {
					new SqlParameter("@ReturnID", SqlDbType.Int,4)};
            parameters[0].Value = ReturnID;

            return CBO.FillObject<ExpenseAccountInfo>(DbHelperSQL.Query(strSql.ToString(), parameters));
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<ExpenseAccountInfo> GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ID,ReturnID,ConfirmFee ");
            strSql.Append(" FROM F_ExpenseAccount ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where 1=1 " + strWhere);
            }
            strSql.Append(" order by id desc ");
            return CBO.FillCollection<ExpenseAccountInfo>(DbHelperSQL.Query(strSql.ToString()));
        }

        public List<ExpenseAccountInfo> GetList(string strWhere, List<SqlParameter> parms)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ID,ReturnID,ConfirmFee ");
            strSql.Append(" FROM F_ExpenseAccount ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where 1=1 " + strWhere);
            }
            strSql.Append(" order by id desc ");
            return CBO.FillCollection<ExpenseAccountInfo>(DbHelperSQL.Query(strSql.ToString(), parms));
        }

        /// <summary>
        /// 单笔金额是否大于2000
        /// </summary>
        /// <param name="projectCode"></param>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <returns></returns>
        public bool ExpenseMoneyGreaterThan2000(int ReturnID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" SELECT count(1) FROM F_ExpenseAccountDetail where ExpenseMoney > 2000 and ReturnID = @ReturnID and ExpenseType in ( select id from F_ExpenseType where ExpenseType like '%餐费%') ");
            SqlParameter[] parameters = { new SqlParameter("@ReturnID", SqlDbType.Int, 4) };
            parameters[0].Value = ReturnID;
            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public ESP.Finance.Entity.ExpenseAccountExtendsInfo GetWorkItemModel(int ReturnID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 * from F_Return ");
            strSql.Append(" where ReturnID=@ReturnID ");
            SqlParameter[] parameters = {
					new SqlParameter("@ReturnID", SqlDbType.Int,4)};
            parameters[0].Value = ReturnID;

            return CBO.FillObject<ExpenseAccountExtendsInfo>(DbHelperSQL.Query(strSql.ToString(), parameters));
        }

        public DataSet GetMajorAuditList(string whereStr, string WhereStr2)
        {
            string year = DateTime.Now.Year.ToString();
            string month = DateTime.Now.Month.ToString();
            string day = DateTime.Now.Day.ToString();

            string sql = "SELECT (select ExpenseCommitDeadLine from f_deadline where DeadLineYear=" + year + " and DeadLineMonth=" + month + ") as ExpenseCommitDeadLine,";
            sql += "(select ExpenseCommitDeadLine2 from f_deadline where DeadLineYear=" + year + " and DeadLineMonth=" + month + ") as ExpenseCommitDeadLine2,";

            sql += @"case r.ReturnType when 30 then '报销单' when 31 then '支票/电汇付款单' when 32 then '现金借款单' when 33 then '商务卡报销单' when 34 then 'PR现金借款冲销' when 35 then '第三方报销单' when 36 then '借款冲销单' when 37 then '媒体预付申请' when 40 then '机票申请' else '未知' end as ReturnTypeName
                    ,r.PreFee confirmFee,wi.WorkItemId,wi.WorkItemName,wi.WebPage,wi.ParticipantName,wa.assigneeid,
                    r.ReturnID, r.ReturnCode,r.ProjectID,r.ProjectCode,r.ReturnContent,r.PreFee,r.FactFee,
                    r.ReturnStatus,r.ReturnType,r.RequestorID,r.RequestEmployeeName,r.RequestDate,
                    r.DepartmentId,r.DepartmentName,r.Remark,r.CommitDate,r.ParentID,r.LastAuditPassTime,r.FaAuditPassTime
                    FROM wf_WorkItems wi 
                    join wf_WorkflowInstances wf on wi.WorkflowInstanceId = wf.InstanceId
                    join wf_WorkItemAssignees wa on wi.WorkItemId = wa.WorkItemId
                    join F_Return r on wi.entityid = r.returnid

                    where (wf.Definition = '/Workflows/Reimbursement.xpdl/Reimbursement' or wf.Definition = '/Workflows/Reimbursement.xpdl/Loan' or wf.Definition = '/Workflows/Reimbursement.xpdl/Charge') ";
            if (!String.IsNullOrEmpty(whereStr))
            {
                sql += whereStr;
            }

            sql = @"select 'WorkMonth' = case when 
                    (((getdate()>=a.ExpenseCommitDeadLine ) or 
                           (getdate()>=a.ExpenseCommitDeadLine2)
                    or (datepart(yyyy,a.LastAuditPassTime)<>datepart(yyyy,getdate()) or datepart(mm,a.LastAuditPassTime)<>datepart(mm,getdate()))
                    ) or (a.ReturnType not in(30,33,35))  
                    or  (a.ParticipantName not in('财务第一级','财务第二级','财务第三级','财务第四级','FA')))
                     then '1.本次处理' 
                     when (((getdate()<a.ExpenseCommitDeadLine ) or 
                           (getdate()<a.ExpenseCommitDeadLine2 )) and 
                           (datepart(yyyy,a.LastAuditPassTime)=datepart(yyyy,getdate()) and datepart(mm,a.LastAuditPassTime)=datepart(mm,getdate())))
                     then '2.下次处理' else '3.未知' end 
                    ,a.LastAuditPassTime,a.* from (" + sql + ") a where 1=1 " + WhereStr2 + " order by WorkMonth,RequestEmployeeName,ReturnCode";
            return DbHelperSQL.Query(sql);
        }


        public DataSet GetExpenseOrderView(string whereStr)
        {
            string sql = @"SELECT
case r.ReturnType when 30 then '报销单' when 31 then '支票/电汇付款单' when 32 then '现金借款单' when 33 then '商务卡报销单' 
when 34 then 'PR现金借款冲销' when 35 then '第三方报销单' when 36 then '借款冲销单' when 37 then '媒体预付申请' 
when 40 then '机票申请' else '未知' end as ReturnTypeName
,ea.confirmFee,
r.ReturnID, r.ReturnCode,r.ProjectID,r.ProjectCode,r.ReturnContent,r.PreFee,r.FactFee,
r.ReturnStatus,r.ReturnType,r.RequestorID,r.RequestEmployeeName,r.RequestDate,
r.DepartmentId,r.DepartmentName,r.Remark,
r.CommitDate,r.ParentID,r.LastAuditPassTime,r.FaAuditPassTime
from F_Return r 
left join F_ExpenseAccount ea on r.returnid=ea.ReturnID
join F_PNBatchRelation pb on r.returnid=pb.returnid";

            if (!String.IsNullOrEmpty(whereStr))
            {
                sql += whereStr;
            }

            sql += " order by pb.batchrelationid asc ";
            return DbHelperSQL.Query(sql);
        }

        public DataSet GetMajorAuditListByBatch(string whereStr)
        {
            string year = DateTime.Now.Year.ToString();
            string month = DateTime.Now.Month.ToString();
            string day = DateTime.Now.Day.ToString();

            string sql = "SELECT (select ExpenseCommitDeadLine from f_deadline where DeadLineYear=" + year + " and DeadLineMonth=" + month + ") as ExpenseCommitDeadLine,";
            sql += "(select ExpenseCommitDeadLine2 from f_deadline where DeadLineYear=" + year + " and DeadLineMonth=" + month + ") as ExpenseCommitDeadLine2,";

            sql += @"case r.ReturnType when 30 then '报销单' when 31 then '支票/电汇付款单' when 32 then '现金借款单' when 33 then '商务卡报销单' when 34 then 'PR现金借款冲销' when 35 then '第三方报销单' when 36 then '借款冲销单' when 37 then '媒体预付申请' when 40 then '机票申请' else '未知' end as ReturnTypeName
                    ,ea.confirmFee,wi.WorkItemId,wi.WorkItemName,wi.WebPage,wi.ParticipantName,wa.assigneeid,
                    r.ReturnID, r.ReturnCode,r.ProjectID,r.ProjectCode,r.ReturnContent,r.PreFee,r.FactFee,
                    r.ReturnStatus,r.ReturnType,r.RequestorID,r.RequestEmployeeName,r.RequestDate,
                    r.DepartmentId,r.DepartmentName,r.Remark,r.CommitDate,r.ParentID,r.LastAuditPassTime,r.FaAuditPassTime
                    FROM wf_WorkItems wi 
                    join wf_WorkflowInstances wf on wi.WorkflowInstanceId = wf.InstanceId
                    join wf_WorkItemAssignees wa on wi.WorkItemId = wa.WorkItemId
                    join F_Return r on wi.entityid = r.returnid
                    left join F_ExpenseAccount ea on r.returnid=ea.ReturnID
                    join F_PNBatchRelation pb on r.returnid=pb.returnid
                    where (wf.Definition = '/Workflows/Reimbursement.xpdl/Reimbursement' or wf.Definition = '/Workflows/Reimbursement.xpdl/Loan' or wf.Definition = '/Workflows/Reimbursement.xpdl/Charge') ";
            if (!String.IsNullOrEmpty(whereStr))
            {
                sql += whereStr;
            }

            sql += " order by pb.batchrelationid asc ";
            return DbHelperSQL.Query(sql);
        }

        public DataSet GetMajorAlreadyAuditList(string whereStr)
        {
            string year = DateTime.Now.Year.ToString();
            string month = DateTime.Now.Month.ToString();
            string day = DateTime.Now.Day.ToString();

            string sql = "SELECT (select ExpenseCommitDeadLine from f_deadline where DeadLineYear=" + year + " and DeadLineMonth=" + month + ") as ExpenseCommitDeadLine,";
            sql += "(select ExpenseCommitDeadLine2 from f_deadline where DeadLineYear=" + year + " and DeadLineMonth=" + month + ") as ExpenseCommitDeadLine2,";

            sql += @"case r.ReturnType when 30 then '报销单' when 31 then '支票/电汇付款单' when 32 then '现金借款单' when 33 then '商务卡报销单' when 34 then 'PR现金借款冲销' when 35 then '第三方报销单' when 36 then '借款冲销单' when 37 then '媒体预付申请' when 40 then '机票申请' else '未知' end as ReturnTypeName,
                r.ReturnID, r.ReturnCode,r.ProjectID,r.ProjectCode,r.ReturnContent,r.PreFee,r.FactFee,
                r.ReturnStatus,r.ReturnType,r.RequestorID,r.RequestEmployeeName,r.RequestDate,
                r.DepartmentId,r.DepartmentName,r.Remark,r.CommitDate,r.ParentID,r.LastAuditPassTime,r.FaAuditPassTime
                from f_return as r where 1=1 ";
            if (!String.IsNullOrEmpty(whereStr))
            {
                sql += whereStr;
            }
            sql += " order by r.Lastupdatetime desc ";
            return DbHelperSQL.Query(sql);
        }

        public DataSet GetAlreadyAuditDetailList(string whereStr)
        {
            string year = DateTime.Now.Year.ToString();
            string month = DateTime.Now.Month.ToString();
            string day = DateTime.Now.Day.ToString();
            string sql = "SELECT (select ExpenseCommitDeadLine from f_deadline where DeadLineYear=" + year + " and DeadLineMonth=" + month + ") as ExpenseCommitDeadLine,";
            sql += "(select ExpenseCommitDeadLine2 from f_deadline where DeadLineYear=" + year + " and DeadLineMonth=" + month + ") as ExpenseCommitDeadLine2,";

            sql += @"case r.ReturnType when 30 then '报销单' when 31 then '支票/电汇付款单' when 32 then '现金借款单' when 33 then '商务卡报销单' when 34 then 'PR现金借款冲销' when 35 then '第三方报销单' when 36 then '借款冲销单' when 37 then '媒体预付申请' when 40 then '机票申请' else '未知' end as ReturnTypeName,
                r.ReturnCode,r.ProjectID,r.ProjectCode,r.ReturnContent,r.PreFee,r.FactFee,
                r.ReturnStatus,r.ReturnType,r.RequestorID,r.RequestEmployeeName,r.RequestDate,
                r.DepartmentId,r.DepartmentName,r.Remark,r.CommitDate,r.ParentID,r.LastAuditPassTime,r.FaAuditPassTime,d.*
                from f_return r inner join f_expenseaccountdetail d on r.returnid=d.returnid where 1=1 ";

            if (!String.IsNullOrEmpty(whereStr))
            {
                sql += whereStr;
            }
            sql += " order by r.RequestEmployeeName asc,r.ProjectCode asc,d.CreateTime asc ";
            return DbHelperSQL.Query(sql);
        }

        public DataSet GetExportExpenseDetail(string whereStr)
        {
            string sql = @"SELECT 
                case r.ReturnType when 30 then '报销单' when 31 then '支票/电汇付款单' when 32 then '现金借款单' when 33 then '商务卡报销单' when 34 then 'PR现金借款冲销' when 35 then '第三方报销单' when 36 then '借款冲销单' when 37 then '媒体预付申请' when 40 then '机票申请' else '未知' end as ReturnTypeName
                ,ea.confirmFee,wi.WorkItemId,wi.WorkItemName,wi.WebPage,wi.ParticipantName,wa.assigneeid,
                r.ReturnCode,r.ProjectID,r.ProjectCode,r.ReturnContent,r.PreFee,r.FactFee,
                r.ReturnStatus,r.ReturnType,r.RequestorID,r.RequestEmployeeName,r.RequestUserCode,r.RequestDate,
                r.DepartmentId,r.DepartmentName,r.Remark,r.CommitDate,r.ParentID,r.LastAuditPassTime,r.ReturnPreDate,
                ed.*,et.ExpenseType as ExpenseTypeName,case ed.CostDetailID when 0 then 'OOP' else tt.TypeName end as CostDetailTypeName,r.FaAuditPassTime
                FROM wf_WorkItems wi 
                join wf_WorkflowInstances wf on wi.WorkflowInstanceId = wf.InstanceId
                join wf_WorkItemAssignees wa on wi.WorkItemId = wa.WorkItemId
                join F_Return r on wi.entityid = r.returnid
                left join F_ExpenseAccount ea on r.returnid=ea.ReturnID
                left join F_ExpenseAccountDetail ed on r.returnid=ed.returnid
                left join F_ExpenseType et on ed.ExpenseType = et.id
                left join T_Type tt on ed.CostDetailID = tt.typeid
                where (wf.Definition = '/Workflows/Reimbursement.xpdl/Reimbursement' or wf.Definition = '/Workflows/Reimbursement.xpdl/Loan' or wf.Definition = '/Workflows/Reimbursement.xpdl/Charge') ";
            if (!String.IsNullOrEmpty(whereStr))
            {
                sql += whereStr;
            }
            sql += " order by RequestEmployeeName,ReturnCode ,ed.ID";
            return DbHelperSQL.Query(sql);
        }

        /// <summary>
        /// 根据批次ID 获得工作项
        /// </summary>
        /// <param name="batchid"></param>
        /// <returns></returns>
        public List<ESP.Finance.SqlDataAccess.WorkItem> GetWorkItemsByBatchID(int batchid)
        {
            string sql = string.Format(" select * FROM wf_WorkItems wi where wi.entityid in (select returnid from F_PNBatchRelation where Batchid = {0}) and wi.status = {1} ", batchid, (int)ESP.Workflow.WorkItemStatus.Open);
            return CBO.FillCollection<ESP.Finance.SqlDataAccess.WorkItem>(DbHelperSQL.Query(sql));
        }
        #endregion  成员方法

    }
}

