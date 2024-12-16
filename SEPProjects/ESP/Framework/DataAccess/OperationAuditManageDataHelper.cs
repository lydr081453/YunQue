using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using ESP.Framework.Common;
using ESP.Framework.Entity;
using System.Collections.Generic;
using System.Collections;


namespace ESP.Framework.DataAccess
{
    /// <summary>
    /// 数据访问类OperationAuditManageDataHelper。
    /// </summary>
    public class OperationAuditManageDataHelper
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OperationAuditManageDataHelper"/> class.
        /// </summary>
        public OperationAuditManageDataHelper()
        { }

        #region  成员方法
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        /// <param name="Id">The id.</param>
        /// <returns></returns>
        public bool Exists(int Id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from sep_OperationAuditManage");
            strSql.Append(" where Id=@Id ");
            SqlParameter[] parameters = {
					new SqlParameter("@Id", SqlDbType.Int,4)};
            parameters[0].Value = Id;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public int Add(OperationAuditManageInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into sep_OperationAuditManage(");
            strSql.Append("DepId,DirectorId,DirectorName,ManagerId,ManagerName,CEOId,CEOName,AttendanceId,AttendanceName,HRId,HRName,FAId,FAName,Hrattendanceid,Hrattendancename,ReceptionId,Reception,TicketPurchaseId,TicketPurchase,ADManagerID,ADManagerName,DimissionFinanceAuditorId,DimissionFinanceAuditorName,DimissionADAuditorId,DimissionADAuditorName,DimissionDirectorid,DimissionDirectorName,DimissionManagerId,DimissionManagerName,PurchaseAuditorId,PurchaseAuditor,PurchaseDirectorId,PurchaseDirector,userid,ProjectCEOIndependent,ARReportUsers,HeadCountAuditorId,HeadCountAuditor,HeadCountDirectorId,HeadCountDirector,AppendReceiverId,AppendReceiver,DirectorAmount,ManagerAmount,CEOAmount,CostView,RiskControlAccounter,RiskControlAccounterName,HCFinalAuditor,DimissionPreAuditorId,DimissionPreAuditor,ITOperatorId,ITOperator )");
            strSql.Append(" values (");
            strSql.Append("@DepId,@DirectorId,@DirectorName,@ManagerId,@ManagerName,@CEOId,@CEOName,@AttendanceId,@AttendanceName,@HRId,@HRName,@FAId,@FAName,@Hrattendanceid,@Hrattendancename,@ReceptionId,@Reception,@TicketPurchaseId,@TicketPurchase,@ADManagerID,@ADManagerName,@DimissionFinanceAuditorId,@DimissionFinanceAuditorName,@DimissionADAuditorId,@DimissionADAuditorName,@DimissionDirectorid,@DimissionDirectorName,@DimissionManagerId,@DimissionManagerName,@PurchaseAuditorId,@PurchaseAuditor,@PurchaseDirectorId,@PurchaseDirector,@userid,@ProjectCEOIndependent,@ARReportUsers,@HeadCountAuditorId,@HeadCountAuditor,@HeadCountDirectorId,@HeadCountDirector,@AppendReceiverId,@AppendReceiver,@DirectorAmount,@ManagerAmount,@CEOAmount,@CostView,@RiskControlAccounter,@RiskControlAccounterName,@HCFinalAuditor,@DimissionPreAuditorId,@DimissionPreAuditor,@ITOperatorId,@ITOperator )");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@DepId", SqlDbType.Int,4),
					new SqlParameter("@DirectorId", SqlDbType.Int,4),
					new SqlParameter("@DirectorName", SqlDbType.NVarChar,20),
					new SqlParameter("@ManagerId", SqlDbType.Int,4),
					new SqlParameter("@ManagerName", SqlDbType.NVarChar,20),
					new SqlParameter("@CEOId", SqlDbType.Int,4),
					new SqlParameter("@CEOName", SqlDbType.NVarChar,20),
                    new SqlParameter("@AttendanceId",SqlDbType.Int,4),
                    new SqlParameter("@AttendanceName",SqlDbType.NVarChar,20),
                    new SqlParameter("@HRId",SqlDbType.Int,4),
                    new SqlParameter("@HRName",SqlDbType.NVarChar,20),
                    new SqlParameter("@FAId",SqlDbType.Int,4),
                    new SqlParameter("@FAName",SqlDbType.NVarChar,20),
                    new SqlParameter("@Hrattendanceid", SqlDbType.Int, 4),
                    new SqlParameter("@Hrattendancename", SqlDbType.NVarChar, 50),
                    new SqlParameter("@ReceptionId", SqlDbType.Int, 4),
                    new SqlParameter("@Reception", SqlDbType.NVarChar, 50),
                    new SqlParameter("@TicketPurchaseId", SqlDbType.Int, 4),
                    new SqlParameter("@TicketPurchase", SqlDbType.NVarChar, 50),
                    new SqlParameter("@ADManagerID", SqlDbType.Int,4),
					new SqlParameter("@ADManagerName", SqlDbType.NVarChar),
					new SqlParameter("@DimissionFinanceAuditorId", SqlDbType.Int,4),
					new SqlParameter("@DimissionFinanceAuditorName", SqlDbType.NVarChar),
					new SqlParameter("@DimissionADAuditorId", SqlDbType.Int,4),
					new SqlParameter("@DimissionADAuditorName", SqlDbType.NVarChar),
                    new SqlParameter("@DimissionDirectorid", SqlDbType.Int,4),
					new SqlParameter("@DimissionDirectorName", SqlDbType.NVarChar),
                    new SqlParameter("@DimissionManagerId", SqlDbType.Int, 4),
                    new SqlParameter("@DimissionManagerName", SqlDbType.NVarChar),
                    new SqlParameter("@PurchaseAuditorId", SqlDbType.Int, 4),
                    new SqlParameter("@PurchaseAuditor", SqlDbType.NVarChar),
                    new SqlParameter("@PurchaseDirectorId", SqlDbType.Int, 4),
                    new SqlParameter("@PurchaseDirector", SqlDbType.NVarChar),
                    new SqlParameter("@userid", SqlDbType.Int, 4),
                    new SqlParameter("@ProjectCEOIndependent", SqlDbType.Int, 4),
                    //ARReportUsers
                    new SqlParameter("@ARReportUsers", SqlDbType.NVarChar),

                    new SqlParameter("@HeadCountAuditorId", SqlDbType.Int, 4),
                    new SqlParameter("@HeadCountAuditor", SqlDbType.NVarChar),
                    new SqlParameter("@HeadCountDirectorId", SqlDbType.Int, 4),
                    new SqlParameter("@HeadCountDirector", SqlDbType.NVarChar),
                    new SqlParameter("@AppendReceiverId", SqlDbType.Int, 4),
                    new SqlParameter("@AppendReceiver", SqlDbType.NVarChar),
                    new SqlParameter("@DirectorAmount", SqlDbType.Decimal, 18),
                    new SqlParameter("@ManagerAmount", SqlDbType.Decimal, 18),
                    new SqlParameter("@CEOAmount", SqlDbType.Decimal, 18),
                    new SqlParameter("@CostView",SqlDbType.NVarChar),
                    new SqlParameter("@RiskControlAccounter", SqlDbType.Int, 4),
                    new SqlParameter("@RiskControlAccounterName",SqlDbType.NVarChar),
                    new SqlParameter("@HCFinalAuditor", SqlDbType.Int, 4),
                    new SqlParameter("@DimissionPreAuditorId", SqlDbType.Int, 4),
                    new SqlParameter("@DimissionPreAuditor",SqlDbType.NVarChar),
                    new SqlParameter("@ITOperatorId", SqlDbType.Int, 4),
                    new SqlParameter("@ITOperator",SqlDbType.NVarChar)
                                        };
            parameters[0].Value = model.DepId;
            parameters[1].Value = model.DirectorId;
            parameters[2].Value = model.DirectorName;
            parameters[3].Value = model.ManagerId;
            parameters[4].Value = model.ManagerName;
            parameters[5].Value = model.CEOId;
            parameters[6].Value = model.CEOName;
            parameters[7].Value = model.AttendanceId;
            parameters[8].Value = model.AttendanceName;
            parameters[9].Value = model.HRId;
            parameters[10].Value = model.HRName;
            parameters[11].Value = model.FAId;
            parameters[12].Value = model.FAName;
            parameters[13].Value = model.Hrattendanceid;
            parameters[14].Value = model.Hrattendancename;
            parameters[15].Value = model.ReceptionId;
            parameters[16].Value = model.Reception;
            parameters[17].Value = model.TicketPurchaseId;
            parameters[18].Value = model.TicketPurchase;
            parameters[19].Value = model.ADManagerID;
            parameters[20].Value = model.ADManagerName;
            parameters[21].Value = model.DimissionFinanceAuditorId;
            parameters[22].Value = model.DimissionFinanceAuditorName;
            parameters[23].Value = model.DimissionADAuditorId;
            parameters[24].Value = model.DimissionADAuditorName;
            parameters[25].Value = model.DimissionDirectorid;
            parameters[26].Value = model.DimissionDirectorName;
            parameters[27].Value = model.DimissionManagerId;
            parameters[28].Value = model.DimissionManagerName;
            parameters[29].Value = model.PurchaseAuditorId;
            parameters[30].Value = model.PurchaseAuditor;
            parameters[31].Value = model.PurchaseDirectorId;
            parameters[32].Value = model.PurchaseDirector;
            parameters[33].Value = model.UserId;
            parameters[34].Value = model.ProjectCEOIndependent;
            parameters[35].Value = model.ARReportUsers;

            parameters[36].Value = model.HeadCountAuditorId;
            parameters[37].Value = model.HeadCountAuditor;
            parameters[38].Value = model.HeadCountDirectorId;
            parameters[39].Value = model.HeadCountDirector;
            parameters[40].Value = model.AppendReceiverId;
            parameters[41].Value = model.AppendReceiver;
            parameters[42].Value = model.DirectorAmount;
            parameters[43].Value = model.ManagerAmount;
            parameters[44].Value = model.CEOAmount;
            parameters[45].Value = model.CostView;
            parameters[46].Value = model.RiskControlAccounter;
            parameters[47].Value = model.RiskControlAccounterName;
            parameters[48].Value = model.HCFinalAuditor;
            parameters[49].Value = model.DimissionPreAuditorId;
            parameters[50].Value = model.DimissionPreAuditor;
            parameters[51].Value = model.ITOperatorId;
            parameters[52].Value = model.ITOperator;
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
        /// <param name="model">The model.</param>
        public void Update(OperationAuditManageInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update sep_OperationAuditManage set ");
            strSql.Append("DepId=@DepId,");
            strSql.Append("DirectorId=@DirectorId,");
            strSql.Append("DirectorName=@DirectorName,");
            strSql.Append("ManagerId=@ManagerId,");
            strSql.Append("ManagerName=@ManagerName,");
            strSql.Append("CEOId=@CEOId,");
            strSql.Append("CEOName=@CEOName,");
            strSql.Append("AttendanceId=@AttendanceId,");
            strSql.Append("AttendanceName=@AttendanceName,");
            strSql.Append("HRId=@HRId,");
            strSql.Append("HRName=@HRName,");
            strSql.Append("FAId=@FAId,");
            strSql.Append("FAName=@FAName,");
            strSql.Append("Hrattendanceid=@Hrattendanceid,");
            strSql.Append("Hrattendancename=@Hrattendancename,");
            strSql.Append("ReceptionId=@ReceptionId,");
            strSql.Append("Reception=@Reception,");
            strSql.Append("TicketPurchaseId=@TicketPurchaseId,");
            strSql.Append("TicketPurchase=@TicketPurchase,");
            strSql.Append("ADManagerID=@ADManagerID,");
            strSql.Append("ADManagerName=@ADManagerName,");
            strSql.Append("DimissionFinanceAuditorId=@DimissionFinanceAuditorId,");
            strSql.Append("DimissionFinanceAuditorName=@DimissionFinanceAuditorName,");
            strSql.Append("DimissionADAuditorId=@DimissionADAuditorId,");
            strSql.Append("DimissionADAuditorName=@DimissionADAuditorName,");
            strSql.Append("DimissionDirectorid=@DimissionDirectorid,");
            strSql.Append("DimissionDirectorName=@DimissionDirectorName,");
            strSql.Append("DimissionManagerId=@DimissionManagerId,");
            strSql.Append("DimissionManagerName=@DimissionManagerName,");
            strSql.Append("PurchaseAuditorId=@PurchaseAuditorId,");
            strSql.Append("PurchaseAuditor=@PurchaseAuditor,");
            strSql.Append("PurchaseDirectorId=@PurchaseDirectorId,");
            strSql.Append("PurchaseDirector=@PurchaseDirector,userid=@userid,ProjectCEOIndependent=@ProjectCEOIndependent,ARReportUsers=@ARReportUsers, ");
            strSql.Append("HeadCountAuditorId=@HeadCountAuditorId,HeadCountAuditor=@HeadCountAuditor,HeadCountDirectorId=@HeadCountDirectorId,HeadCountDirector=@HeadCountDirector,AppendReceiverId=@AppendReceiverId,AppendReceiver=@AppendReceiver,");
            strSql.Append("DirectorAmount=@DirectorAmount,ManagerAmount=@ManagerAmount,CEOAmount=@CEOAmount,CostView=@CostView,RiskControlAccounter=@RiskControlAccounter,RiskControlAccounterName=@RiskControlAccounterName,");
            strSql.Append("HCFinalAuditor=@HCFinalAuditor,DimissionPreAuditorId=@DimissionPreAuditorId,DimissionPreAuditor=@DimissionPreAuditor,ITOperatorId=@ITOperatorId,ITOperator=@ITOperator ");
            strSql.Append(" where Id=@Id ");
            SqlParameter[] parameters = {
					new SqlParameter("@Id", SqlDbType.Int,4),
					new SqlParameter("@DepId", SqlDbType.Int,4),
					new SqlParameter("@DirectorId", SqlDbType.Int,4),
					new SqlParameter("@DirectorName", SqlDbType.NVarChar,20),
					new SqlParameter("@ManagerId", SqlDbType.Int,4),
					new SqlParameter("@ManagerName", SqlDbType.NVarChar,20),
					new SqlParameter("@CEOId", SqlDbType.Int,4),
					new SqlParameter("@CEOName", SqlDbType.NVarChar,20),
                    new SqlParameter("@AttendanceId",SqlDbType.Int,4),
                    new SqlParameter("@AttendanceName",SqlDbType.NVarChar,20),
                    new SqlParameter("@HRId",SqlDbType.Int,4),
                    new SqlParameter("@HRName",SqlDbType.NVarChar,20),
                    new SqlParameter("@FAId",SqlDbType.Int,4),
                    new SqlParameter("@FAName",SqlDbType.NVarChar,20),
                    new SqlParameter("@Hrattendanceid", SqlDbType.Int, 4),
                    new SqlParameter("@Hrattendancename", SqlDbType.NVarChar, 50),
                    new SqlParameter("@ReceptionId", SqlDbType.Int,4),
                    new SqlParameter("@Reception", SqlDbType.NVarChar, 50),
                    new SqlParameter("@TicketPurchaseId", SqlDbType.Int,4),
                    new SqlParameter("@TicketPurchase", SqlDbType.NVarChar, 50),
                    new SqlParameter("@ADManagerID", SqlDbType.Int,4),
					new SqlParameter("@ADManagerName", SqlDbType.NVarChar),
					new SqlParameter("@DimissionFinanceAuditorId", SqlDbType.Int,4),
					new SqlParameter("@DimissionFinanceAuditorName", SqlDbType.NVarChar),
					new SqlParameter("@DimissionADAuditorId", SqlDbType.Int,4),
					new SqlParameter("@DimissionADAuditorName", SqlDbType.NVarChar),
                    new SqlParameter("@DimissionDirectorid", SqlDbType.Int,4),
					new SqlParameter("@DimissionDirectorName", SqlDbType.NVarChar),
                    new SqlParameter("@DimissionManagerId", SqlDbType.Int, 4),
                    new SqlParameter("@DimissionManagerName", SqlDbType.NVarChar),
                    new SqlParameter("@PurchaseAuditorId", SqlDbType.Int, 4),
                    new SqlParameter("@PurchaseAuditor", SqlDbType.NVarChar),
                    new SqlParameter("@PurchaseDirectorId", SqlDbType.Int, 4),
                    new SqlParameter("@PurchaseDirector", SqlDbType.NVarChar),
                    new SqlParameter("@userid", SqlDbType.Int, 4),
                    new SqlParameter("@ProjectCEOIndependent", SqlDbType.Int, 4),
                    new SqlParameter("@ARReportUsers", SqlDbType.NVarChar),

                    new SqlParameter("@HeadCountAuditorId", SqlDbType.Int, 4),
                    new SqlParameter("@HeadCountAuditor", SqlDbType.NVarChar),
                    new SqlParameter("@HeadCountDirectorId", SqlDbType.Int, 4),
                    new SqlParameter("@HeadCountDirector", SqlDbType.NVarChar),
                    new SqlParameter("@AppendReceiverId", SqlDbType.Int, 4),
                    new SqlParameter("@AppendReceiver", SqlDbType.NVarChar),
                    new SqlParameter("@DirectorAmount", SqlDbType.Decimal, 18),
                    new SqlParameter("@ManagerAmount", SqlDbType.Decimal, 18),
                    new SqlParameter("@CEOAmount", SqlDbType.Decimal, 18),
                    new SqlParameter("@CostView",SqlDbType.NVarChar),
                    new SqlParameter("@RiskControlAccounter", SqlDbType.Int, 4),
                    new SqlParameter("@RiskControlAccounterName", SqlDbType.NVarChar),
                    new SqlParameter("@HCFinalAuditor", SqlDbType.Int, 4),
                    new SqlParameter("@DimissionPreAuditorId", SqlDbType.Int, 4),
                    new SqlParameter("@DimissionPreAuditor",SqlDbType.NVarChar),
                    new SqlParameter("@ITOperatorId", SqlDbType.Int, 4),
                    new SqlParameter("@ITOperator",SqlDbType.NVarChar)
                                        };
            parameters[0].Value = model.Id;
            parameters[1].Value = model.DepId;
            parameters[2].Value = model.DirectorId;
            parameters[3].Value = model.DirectorName;
            parameters[4].Value = model.ManagerId;
            parameters[5].Value = model.ManagerName;
            parameters[6].Value = model.CEOId;
            parameters[7].Value = model.CEOName;
            parameters[8].Value = model.AttendanceId;
            parameters[9].Value = model.AttendanceName;
            parameters[10].Value = model.HRId;
            parameters[11].Value = model.HRName;
            parameters[12].Value = model.FAId;
            parameters[13].Value = model.FAName;
            parameters[14].Value = model.Hrattendanceid;
            parameters[15].Value = model.Hrattendancename;
            parameters[16].Value = model.ReceptionId;
            parameters[17].Value = model.Reception;
            parameters[18].Value = model.TicketPurchaseId;
            parameters[19].Value = model.TicketPurchase;
            parameters[20].Value = model.ADManagerID;
            parameters[21].Value = model.ADManagerName;
            parameters[22].Value = model.DimissionFinanceAuditorId;
            parameters[23].Value = model.DimissionFinanceAuditorName;
            parameters[24].Value = model.DimissionADAuditorId;
            parameters[25].Value = model.DimissionADAuditorName;
            parameters[26].Value = model.DimissionDirectorid;
            parameters[27].Value = model.DimissionDirectorName;
            parameters[28].Value = model.DimissionManagerId;
            parameters[29].Value = model.DimissionManagerName;
            parameters[30].Value = model.PurchaseAuditorId;
            parameters[31].Value = model.PurchaseAuditor;
            parameters[32].Value = model.PurchaseDirectorId;
            parameters[33].Value = model.PurchaseDirector;
            parameters[34].Value = model.UserId;
            parameters[35].Value = model.ProjectCEOIndependent;
            parameters[36].Value = model.ARReportUsers;

            parameters[37].Value = model.HeadCountAuditorId;
            parameters[38].Value = model.HeadCountAuditor;
            parameters[39].Value = model.HeadCountDirectorId;
            parameters[40].Value = model.HeadCountDirector;
            parameters[41].Value = model.AppendReceiverId;
            parameters[42].Value = model.AppendReceiver;
            parameters[43].Value = model.DirectorAmount;
            parameters[44].Value = model.ManagerAmount;
            parameters[45].Value = model.CEOAmount;
            parameters[46].Value = model.CostView;
            parameters[47].Value = model.RiskControlAccounter;
            parameters[48].Value = model.RiskControlAccounterName;
            parameters[49].Value = model.HCFinalAuditor;
            parameters[50].Value = model.DimissionPreAuditorId;
            parameters[51].Value = model.DimissionPreAuditor;
            parameters[52].Value = model.ITOperatorId;
            parameters[53].Value = model.ITOperator;
            DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        /// <param name="Id">The id.</param>
        public void Delete(int Id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete sep_OperationAuditManage ");
            strSql.Append(" where Id=@Id ");
            SqlParameter[] parameters = {
					new SqlParameter("@Id", SqlDbType.Int,4)};
            parameters[0].Value = Id;

            DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        /// <param name="Id">The id.</param>
        /// <returns></returns>
        public OperationAuditManageInfo GetModel(int Id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 * from sep_OperationAuditManage ");
            strSql.Append(" where Id=@Id ");
            SqlParameter[] parameters = {
					new SqlParameter("@Id", SqlDbType.Int,4)};
            parameters[0].Value = Id;

            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            return setModel(ds);
        }

        /// <summary>
        /// 根据部门ID获得一个对象实体
        /// </summary>
        /// <param name="DepId">The dep id.</param>
        /// <returns></returns>
        public OperationAuditManageInfo GetModelByDepId(int DepId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 * from sep_OperationAuditManage ");
            strSql.Append(" where DepId=@DepId ");
            SqlParameter[] parameters = {
					new SqlParameter("@DepId", SqlDbType.Int,4)};
            parameters[0].Value = DepId;

            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            return setModel(ds);
        }

        /// <summary>
        /// 根据特殊人员ID取得审批路径
        /// </summary>
        /// <param name="userid">userid</param>
        /// <returns>entity</returns>
        public OperationAuditManageInfo GetModelByUserId(int userid)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 * from sep_OperationAuditManage ");
            strSql.Append(" where userid=@userid ");
            SqlParameter[] parameters = {
					new SqlParameter("@userid", SqlDbType.Int,4)};
            parameters[0].Value = userid;

            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            return setModel(ds);
        }

        /// <summary>
        /// 根据特殊项目取得审批路径
        /// </summary>
        /// <param name="ProjectId">ProjectId</param>
        /// <returns>entity</returns>
        public OperationAuditManageInfo GetModelByProjectId(int ProjectId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 * from sep_OperationAuditManage ");
            strSql.Append(" where ProjectId=@ProjectId ");
            SqlParameter[] parameters = {
					new SqlParameter("@ProjectId", SqlDbType.Int,4)};
            parameters[0].Value = ProjectId;

            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            return setModel(ds);
        }

        public OperationAuditManageInfo GetModelByDepId(int DepId, System.Data.SqlClient.SqlTransaction trans)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 * from sep_OperationAuditManage ");
            strSql.Append(" where DepId=@DepId ");
            SqlParameter[] parameters = {
					new SqlParameter("@DepId", SqlDbType.Int,4)};
            parameters[0].Value = DepId;

            DataSet ds = DbHelperSQL.Query(strSql.ToString(), trans, parameters);
            return setModel(ds);
        }

        /// <summary>
        /// Sets the model.
        /// </summary>
        /// <param name="ds">The ds.</param>
        /// <returns></returns>
        private OperationAuditManageInfo setModel(DataSet ds)
        {
            OperationAuditManageInfo model = new OperationAuditManageInfo();
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
        /// <param name="strWhere">The STR where.</param>
        /// <returns></returns>
        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append(" FROM sep_OperationAuditManage ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 根据用户ID获得有部门管理级的相关部门ID集
        /// </summary>
        /// <param name="userid">The INT UserID.</param>
        /// <returns></returns>
        public DataSet GetListByUserID(int userid)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select distinct DepId from dbo.sep_OperationAuditManage");
            strSql.Append(@" where DirectorId="+userid+" OR ManagerId="+userid+" OR CEOId="+userid+" OR FAId="+userid +" OR HRId="+userid);
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 获得总监的sysids
        /// </summary>
        /// <returns></returns>
        public string GetDirectorIds()
        {
            string directorIds = "";
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select distinct DirectorId ");
            strSql.Append(" FROM sep_OperationAuditManage ");
            DataSet ds = DbHelperSQL.Query(strSql.ToString());
            if (ds != null && ds.Tables.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    directorIds += dr["DirectorId"].ToString() + ",";
                }
            }
            if (directorIds.Length > 0)
                directorIds = directorIds.TrimEnd(',');
            return directorIds;
        }

        public Hashtable GetPurchaseDirectorsByAuditor(int auditorId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select distinct PurchaseDirectorId,PurchaseDirector from  sep_OperationAuditManage where PurchaseAuditorId=" + auditorId +" and purchaseDirectorId<>0");
            DataSet ds = DbHelperSQL.Query(strSql.ToString());

            Hashtable ht = new Hashtable();

            if (ds != null && ds.Tables.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ht.Add( dr["PurchaseDirector"].ToString(),dr["PurchaseDirectorId"].ToString());
                }
            }

            return ht;
        }

        public string GetDeptByPurchaseDirector(int purchaseDirectorId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select distinct DepId from dbo.sep_OperationAuditManage");
            strSql.Append(@" where purchaseDirectorId=" + purchaseDirectorId );

            DataSet ds = DbHelperSQL.Query(strSql.ToString());

            string deptids = string.Empty;

            if (ds != null && ds.Tables.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    deptids += dr["depid"].ToString() + ",";
                }
            }
            if (deptids.Length > 0)
                deptids = deptids.TrimEnd(',');
            return deptids;
        }

        /// <summary>
        /// 获得总经理的sysids
        /// </summary>
        /// <returns></returns>
        public string GetManagerIds()
        {
            string ManagerIds = "";
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select distinct ManagerId ");
            strSql.Append(" FROM sep_OperationAuditManage ");
            DataSet ds = DbHelperSQL.Query(strSql.ToString());
            if (ds != null && ds.Tables.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ManagerIds += dr["ManagerId"].ToString() + ",";
                }
            }
            if (ManagerIds.Length > 0)
                ManagerIds = ManagerIds.TrimEnd(',');
            return ManagerIds;
        }

        /// <summary>
        /// 获得CEO的sysids
        /// </summary>
        /// <returns></returns>
        public string GetCEOIds()
        {
            string CEOIds = "";
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select distinct CEOID ");
            strSql.Append(" FROM sep_OperationAuditManage ");
            DataSet ds = DbHelperSQL.Query(strSql.ToString());
            if (ds != null && ds.Tables.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    CEOIds += dr["CEOID"].ToString() + ",";
                }
            }
            if (CEOIds.Length > 0)
                CEOIds = CEOIds.TrimEnd(',');
            return CEOIds;
        }

        public string GetReceptionIds()
        {
            string CEOIds = "";
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select distinct ReceptionId ");
            strSql.Append(" FROM sep_OperationAuditManage ");
            DataSet ds = DbHelperSQL.Query(strSql.ToString());
            if (ds != null && ds.Tables.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    CEOIds += dr["ReceptionId"].ToString() + ",";
                }
            }
            if (CEOIds.Length > 0)
                CEOIds = CEOIds.TrimEnd(',');
            return CEOIds;
        }

        /// <summary>
        /// 获得考勤审批人的sysids
        /// </summary>
        /// <returns></returns>
        public string GetAttendanceId()
        {
            string AttendanceId = "";
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select distinct AttendanceId ");
            strSql.Append(" FROM sep_OperationAuditManage ");
            DataSet ds = DbHelperSQL.Query(strSql.ToString());
            if (ds != null && ds.Tables.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    AttendanceId += dr["AttendanceId"].ToString() + ",";
                }
            }
            if (AttendanceId.Length > 0)
                AttendanceId = AttendanceId.TrimEnd(',');
            return AttendanceId;
        }

        /// <summary>
        /// 获得HR审批人的sysids
        /// </summary>
        /// <returns></returns>
        public string GetHRId()
        {
            string HRId = "";
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select distinct HRId ");
            strSql.Append(" FROM sep_OperationAuditManage ");
            DataSet ds = DbHelperSQL.Query(strSql.ToString());
            if (ds != null && ds.Tables.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    HRId += dr["HRId"].ToString() + ",";
                }
            }
            if (HRId.Length > 0)
                HRId = HRId.TrimEnd(',');
            return HRId;
        }

        /// <summary>
        /// 获得HR助理审批人
        /// </summary>
        /// <returns></returns>
        public string GetHRAttendanceId()
        {
            string HRAttendanceId = "";
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select distinct HRAttendanceId ");
            strSql.Append(" FROM sep_OperationAuditManage ");
            DataSet ds = DbHelperSQL.Query(strSql.ToString());
            if (ds != null && ds.Tables.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    HRAttendanceId += dr["HRAttendanceId"].ToString() + ",";
                }
            }
            if (HRAttendanceId.Length > 0)
                HRAttendanceId = HRAttendanceId.TrimEnd(',');
            return HRAttendanceId;
        }

        #endregion  成员方法
    }
}
