using System.Data;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using ESP.Purchase.DataAccess;
using ESP.Purchase.Entity;
using System;
using WorkFlowDAO;
using WorkFlow.Model;
using WorkFlowLibary;
using WorkFlowImpl;
using ModelTemplate;
using System.Text;

namespace ESP.Purchase.BusinessLogic
{
    /// <summary>
    /// ҵ���߼���GeneralInfoManager ��ժҪ˵����
    /// </summary>
    public static class GeneralInfoManager
    {
        #region  ��Ա����
        /// <summary>
        /// Ups the load.
        /// </summary>
        /// <param name="generalInfo">The general info.</param>
        /// <param name="items">The items.</param>
        /// <returns></returns>
        public static int UpLoad(GeneralInfo generalInfo, List<OrderInfo> items)
        {
            return GeneralInfoDataProvider.UpLoad(generalInfo, items);
        }


        public static DataTable GetCostMonitor(int userid, string supplier, string projectcode, string begindate, string enddate)
        {
            return GeneralInfoDataProvider.GetCostMonitor(userid, supplier, projectcode, begindate, enddate);
        }


        /// <summary>
        /// 3000���µ�ý�鱨�������ɺ��͵����������
        /// </summary>
        /// <param name="projectCode"></param>
        /// <returns></returns>
        public static int GetFinanceAccounter(string projectCode)
        {
            return GeneralInfoDataProvider.GetFinanceAccounter(projectCode);

        }

        public static bool IsOverProjectTotalAmount(GeneralInfo g)
        {
            bool isOver = false;
            if (g.Project_id == null || g.Project_id == 0)
            {
                isOver = false;
            }
            else
            {
                ESP.Finance.Entity.ProjectInfo projectModel = ESP.Finance.BusinessLogic.ProjectManager.GetModelWithOutDetailList(g.Project_id);
                if (projectModel == null)
                {
                    isOver = false;
                }
                else
                {
                    decimal total = ESP.Finance.BusinessLogic.CheckerManager.GetOccupyCost(projectModel.ProjectId, g.Departmentid, g.id);
                    decimal refund = ESP.Finance.BusinessLogic.CheckerManager.GetRefundTotal(projectModel.ProjectCode, g.Departmentid);
                    decimal currentTotal = ESP.Purchase.BusinessLogic.OrderInfoManager.getTotalPriceByGID(g.id);
                    if (g.Departmentid == projectModel.GroupID.Value)//�����뷽
                    {
                        if (currentTotal + refund > ESP.Finance.BusinessLogic.ContractCostManager.GetTotalAmountByProject(projectModel.ProjectId) - total)
                            isOver = true;
                        else
                            isOver = false;
                    }
                    else//֧�ַ�
                    {
                        IList<ESP.Finance.Entity.SupporterInfo> supporterList = ESP.Finance.BusinessLogic.SupporterManager.GetListByProject(projectModel.ProjectId, "GroupID=" + g.Departmentid.ToString(), null);
                        if (supporterList != null && supporterList.Count > 0 && g.Departmentid == supporterList[0].GroupID.Value)
                        {
                            if (currentTotal + refund > ESP.Finance.BusinessLogic.SupporterCostManager.GetTotalCostBySupporter(supporterList[0].SupportID))
                                isOver = true;
                            else
                                isOver = false;
                        }
                        else
                        {
                            isOver = true;
                        }
                    }
                }
            }
            return isOver;
        }

        private static bool IsCommitDateLessCloseDate(ESP.Finance.Entity.ProjectInfo projectModel, GeneralInfo g)
        {
            if (projectModel.Status == (int)ESP.Finance.Utility.Status.ProjectPreClose)
            {
                IList<ESP.Finance.Entity.TimingLogInfo> closeList = ESP.Finance.BusinessLogic.TimingLogManager.GetList(" projectid=0 and ','+remark+',' like '%," + projectModel.ProjectId.ToString() + ",%'");
                if (closeList != null && closeList.Count > 0)
                {
                    if (g.app_date <= closeList[0].Time)//PR������Ŀ�Źر�ǰ��������
                    {
                        if (closeList[0].Time.AddMonths(1) < DateTime.Now)
                        {
                            return false;
                        }
                        else
                            return true;
                    }
                    else
                        return false;
                }
                else
                {
                    if (g.app_date <= projectModel.EndDate.Value.AddMonths(1))
                        return true;
                    else
                        return false;//��Ŀ�رպ������
                }

            }
            else if (projectModel.Status == (int)ESP.Finance.Utility.Status.ProjectClosed)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public static bool IsProjectClosed(GeneralInfo g, bool IsAudit)
        {
            bool isClosed = false;
            ESP.Finance.Entity.ProjectInfo projectModel = null;
            if (g.Project_id == null || g.Project_id == 0)
            {
                projectModel = ESP.Finance.BusinessLogic.ProjectManager.GetModelByProjectCode(g.project_code);
            }
            else
            {
                projectModel = ESP.Finance.BusinessLogic.ProjectManager.GetModelWithOutDetailList(g.Project_id);

            }
            if (projectModel == null)
                isClosed = false;
            else
            {
                if (IsAudit == true)//�����������ֻ�ж��Ƿ�رգ�Ԥ�رյĿ��Լ�������
                {
                    if (projectModel.Status == (int)ESP.Finance.Utility.Status.ProjectClosed)
                    {
                        isClosed = true;
                    }
                }
                else//������ڴ���ʱ��Ԥ�ر���Ŀ��Ҳ���ܴ���
                {
                    //���PR���Ǳ����صģ�Ԥ�رյ���Ŀ��Ҳ���Լ����ύ
                    if (g.status == (int)ESP.Purchase.Common.State.requisition_return || g.status == (int)ESP.Purchase.Common.State.order_return)
                    {
                        if (projectModel.Status == (int)ESP.Finance.Utility.Status.ProjectClosed)
                        {
                            isClosed = true;
                        }
                        else if (projectModel.Status == (int)ESP.Finance.Utility.Status.ProjectPreClose)
                        {
                            if (!IsCommitDateLessCloseDate(projectModel, g))
                            {
                                isClosed = true;
                            }
                        }
                    }
                    else if (g.status == (int)ESP.Purchase.Common.State.requisition_save)
                    {
                        if (projectModel.Status == (int)ESP.Finance.Utility.Status.ProjectClosed || projectModel.Status == (int)ESP.Finance.Utility.Status.ProjectPreClose)
                        {
                            isClosed = true;
                        }
                    }
                    else
                    {
                        if (projectModel.Status == (int)ESP.Finance.Utility.Status.ProjectClosed)
                        {
                            isClosed = true;
                        }
                        else if (projectModel.Status == (int)ESP.Finance.Utility.Status.ProjectPreClose)
                        {
                            if (!IsCommitDateLessCloseDate(projectModel, g))
                            {
                                isClosed = true;
                            }
                        }
                    }
                }
            }
            return isClosed;
        }

        /// <summary>
        /// ���û���ͣ��Ŀ�µ����뵥
        /// </summary>
        /// <param name="projectId">��ĿID</param>
        /// <param name="isUse">�Ƿ�����</param>
        /// <param name="userId"></param>
        /// <param name="userName"></param>
        /// <param name="request"></param>
        public static void UsedPRByProjectId(int projectId, string projectCode, bool isUse, int userId, string userName, System.Web.HttpRequest request, SqlTransaction trans)
        {
            List<ESP.Purchase.Entity.GeneralInfo> modelList = ESP.Purchase.DataAccess.GeneralInfoDataProvider.GetStatusList(" and (project_id=" + projectId + " or project_Code='" + projectCode + "')");
            foreach (GeneralInfo model in modelList)
            {
                model.InUse = isUse == true ? (int)Common.State.PRInUse.Use : (int)Common.State.PRInUse.DisableProject;
                Update(model, trans.Connection, trans);
                //��¼������־
                ESP.Logging.Logger.Add(string.Format("{0}��T_GeneralInfo���е�id={1}�����ݽ��� {2} �Ĳ���", userId, model.id, isUse == true ? "�������뵥��Ŀ" : "��ͣ���뵥��Ŀ"), isUse == true ? "�������뵥��Ŀ" : "��ͣ���뵥��Ŀ");

                LogInfo log = new LogInfo();
                log.Gid = model.id;
                log.LogMedifiedTeme = DateTime.Now;
                log.LogUserId = userId;
                log.Des = string.Format((isUse == true ? Common.State.log_usedProject : Common.State.log_disabledProject), userName, model.id.ToString("0000000"));
                new ESP.Purchase.DataAccess.LogDataProvider().Add(log, request, trans.Connection, trans);
            }
        }

        /// <summary>
        /// ����һ������
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="opertionUserID">The opertion user ID.</param>
        /// <param name="opertionUserName">Name of the opertion user.</param>
        /// <returns></returns>
        public static int Add(GeneralInfo model, int opertionUserID, string opertionUserName)
        {
            return GeneralInfoDataProvider.Add(model);
        }
        public static int Add(GeneralInfo model, SqlConnection conn, SqlTransaction trans)
        {
            return GeneralInfoDataProvider.Add(model, conn, trans);
        }

        public static void UpdateProjectCode(int projectId, string projectCode, SqlTransaction trans)
        {
            GeneralInfoDataProvider.UpdateProjectCode(projectId, projectCode, trans);
        }

        /// <summary>
        /// ����һ������
        /// </summary>
        /// <param name="model">The model.</param>
        public static void Update(GeneralInfo model)
        {
            if (string.IsNullOrEmpty(model.sow4))
            {
                model.sow4 = "0.00";
            }
            if (string.IsNullOrEmpty(model.consult))
            {
                model.consult = "0.00";
            }
            if (string.IsNullOrEmpty(model.contrast))
            {
                model.contrast = "0.00";
            }
            GeneralInfoDataProvider.Update(model);
        }
        public static void Update(GeneralInfo model, System.Data.SqlClient.SqlConnection conn, System.Data.SqlClient.SqlTransaction trans)
        {
            if (string.IsNullOrEmpty(model.sow4))
            {
                model.sow4 = "0.00";
            }
            if (string.IsNullOrEmpty(model.consult))
            {
                model.consult = "0.00";
            }
            if (string.IsNullOrEmpty(model.contrast))
            {
                model.contrast = "0.00";
            }
            GeneralInfoDataProvider.Update(model, conn, trans);
        }

        /// <summary>
        /// �������뵥����¼��־�����ã���ʹ����һ������
        /// </summary>
        /// <param name="model"></param>
        /// <param name="log"></param>
        /// <param name="auditlog"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        public static bool UpdateAndAddLog(GeneralInfo model, LogInfo log, AuditLogInfo auditlog, System.Web.HttpRequest request)
        {
            using (SqlConnection conn = new SqlConnection(Common.DbHelperSQL.connectionString))
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();
                try
                {
                    GeneralInfoDataProvider.Update(model, conn, trans);
                    if (log != null)
                        new LogDataProvider().Add(log, request, conn, trans);
                    if (auditlog != null)
                        new AuditLogDataProvider().Add(auditlog, request, trans);

                    trans.Commit();
                    return true;
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    ESP.Logging.Logger.Add(ex.Message, "GenerlInfoManager.UpdateAndAddLog", ESP.Logging.LogLevel.Error, ex);
                    return false;
                }
            }
        }

        /// <summary>
        /// �������뵥����¼��־
        /// </summary>
        /// <param name="model">���뵥����</param>
        /// <param name="log">��־����</param>
        /// <returns></returns>
        public static bool UpdateAndAddLog(GeneralInfo model, LogInfo log, AuditLogInfo auditlog, System.Web.HttpRequest request, Common.State.PR_CostRecordsType crType)
        {
            int icount = GetList(" prno='" + model.PrNo + "' and id!=" + model.id.ToString()).Tables[0].Rows.Count;
            if (icount != 0)
            {
                return false;
            }

            using (SqlConnection conn = new SqlConnection(Common.DbHelperSQL.connectionString))
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();
                try
                {
                    GeneralInfoDataProvider.Update(model, conn, trans);
                    if (log != null)
                        new LogDataProvider().Add(log, request, conn, trans);
                    if (auditlog != null)
                        new AuditLogDataProvider().Add(auditlog, request, trans);

                    trans.Commit();
                    return true;
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    //ESP.Logging.Logger.Add(ex.Message, "GenerlInfoManager.UpdateAndAddLog", ESP.Logging.LogLevel.Error, ex);
                    return false;
                }
            }
        }

        /// <summary>
        /// ��¼PR���ɱ���Ϣ��Ѻ�𲻼�¼����������ɵĴ���3000��ý���ѵ��Ͷ�˽�������м�¼
        /// </summary>
        /// <param name="model"></param>
        /// <param name="returnModel">С��3000���ɵ�PN��Model</param>
        /// <param name="trans"></param>
        /// <param name="crType">�ɱ���¼����</param>
        /// <param name="parentId"></param>
        public static void InsertPrCostRecords(GeneralInfo model, ESP.Finance.Entity.ReturnInfo returnModel, SqlTransaction trans, Common.State.PR_CostRecordsType crType, int parentId, int productType)
        {
            ESP.Purchase.Entity.CostRecordsInfo costRecord = new ESP.Purchase.Entity.CostRecordsInfo();
            if (model != null)
            {
                costRecord.ProjectId = model.Project_id;
                costRecord.ProjectCode = model.project_code;
                costRecord.DepartmentId = model.Departmentid;
                costRecord.DepartmentName = model.Department;
            }
            else
            {
                costRecord.ProjectId = returnModel.ProjectID ?? 0;
                costRecord.ProjectCode = returnModel.ProjectCode;
                costRecord.DepartmentId = returnModel.DepartmentID ?? 0;
                costRecord.DepartmentName = returnModel.DepartmentName;
            }

            //if (returnModel == null)
            //{
            OrderInfo firstOrder = new OrderInfo();
            TypeInfo material = new TypeInfo();
            if (productType == 0)
            {
                firstOrder = OrderInfoManager.GetModelByGeneralID(model.id);
                material = TypeManager.GetModel(TypeManager.GetModel(firstOrder.producttype).parentId);
            }
            else
            {
                material = TypeManager.GetModel(TypeManager.GetModel(productType).parentId);
            }

            costRecord.MaterialId = material.typeid;
            costRecord.MaterialName = material.typename;
            //}
            if ((int)crType < 14)
            {
                costRecord.PreAmount = OrderInfoManager.getTotalPriceByGID(model.id);
                costRecord.FactAmount = 0;
                costRecord.FormType = ESP.Purchase.Common.State.CostRecord_FormType_PR;
            }
            else if ((int)crType == 15 || (int)crType == 17)
            {
                ////����3000���ɵ���PR��
                //costRecord.PreAmount = model.totalprice;
                //costRecord.FactAmount = model.totalprice;
                //costRecord.FormType = ESP.Purchase.Common.State.CostRecord_FormType_PRNew;
                return;
            }
            else
            {
                //С��3000���ɵ�PN��
                costRecord.PreAmount = returnModel.PreFee ?? 0;
                costRecord.FactAmount = returnModel.PreFee ?? 0;
                costRecord.FormType = ESP.Purchase.Common.State.CostRecord_FormType_PNNew;

                costRecord.FormId = returnModel.ReturnID;
                costRecord.RefFormId = returnModel.PRID ?? 0;
                costRecord.ParentId = parentId;
                costRecord.Status = returnModel.ReturnStatus ?? 0;
                costRecord.Remark = "";
                costRecord.CreateDate = DateTime.Now;

                CostRecordsManager.InsertPN(costRecord, trans);
                return;
            }
            costRecord.FormId = returnModel == null ? model.id : returnModel.ReturnID;
            costRecord.RefFormId = returnModel == null ? 0 : returnModel.PRID ?? 0;
            costRecord.ParentId = parentId;
            costRecord.Status = returnModel == null ? model.status : returnModel.ReturnStatus ?? 0;
            costRecord.Remark = "";
            costRecord.CreateDate = DateTime.Now;

            CostRecordsManager.InsertPR(costRecord, trans);
        }

        /// <summary>
        /// ɾ��һ������
        /// </summary>
        /// <param name="id">The id.</param>
        /// <param name="opertionUserID">The opertion user ID.</param>
        /// <param name="opertionUserName">Name of the opertion user.</param>
        /// <returns></returns>
        public static int LogicDelete(int id)
        {
            return GeneralInfoDataProvider.LogicDelete(id);
        }

        /// <summary>
        /// ɾ��t_generalinfo,t_orderinfo,t_mediaorder
        /// </summary>
        /// <param name="generalId"></param>
        /// <returns></returns>
        public static int DeletePrAll(int generalId)
        {
            string strSql = " delete t_mediaorder where orderid in (select id from t_orderinfo where general_id=" + generalId + ")";
            strSql += " delete t_orderinfo where general_id=" + generalId;
            strSql += " delete t_generalinfo where id=" + generalId;

            return Common.DbHelperSQL.ExecuteSql(strSql);
        }

        /// <summary>
        /// ��ȡ��Ӧ�̻�Pr�����Ŀ�ܺ�ͬ�б�
        /// </summary>
        /// <param name="supplierName">��Ӧ������</param>
        /// <param name="FCPrIds">Pr������ܺ�ͬprids</param>
        /// <returns></returns>
        public static DataTable GetFCPrList(GeneralInfo prModel,string FCPrIds)
        {
            string sql = @"SELECT A.id AS prId,A.prNo,A.supplier_name,B.desctiprtion,B.upfile,B.id as orderId FROM T_GeneralInfo AS A
                            INNER JOIN T_OrderInfo AS B ON A.id=B.general_id
                            WHERE A.requisitionflow=3 AND A.status >=7";
            if (prModel != null)
                sql += " and a.supplier_name ='" + prModel.supplier_name + "' and a.id<>"+prModel.id;
            if (FCPrIds.Length > 0)
                sql += " and a.id in (" + FCPrIds + ")";
            return Common.DbHelperSQL.Query(sql).Tables[0];
        }


        /// <summary>
        /// �õ�һ������ʵ��
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns></returns>
        public static GeneralInfo GetModel(int id)
        {
            return GeneralInfoDataProvider.GetModel(id);
        }
        public static GeneralInfo GetModel(int id, SqlTransaction trans)
        {
            return GeneralInfoDataProvider.GetModel(id, trans);
        }

        /// <summary>
        /// ��������б�
        /// </summary>
        /// <param name="strWhere">The STR where.</param>
        /// <returns></returns>
        public static DataSet GetList(string strWhere)
        {
            return GeneralInfoDataProvider.GetList(strWhere);
        }

        /// <summary>
        /// ��������б�
        /// </summary>
        /// <returns></returns>
        public static DataSet GetAllList()
        {
            return GetList("");
        }

        /// <summary>
        /// ��õ����б�ÿ�����Ӷ�Ӧ�Լ�����Ĳɹ���Ʒ
        /// </summary>
        /// <param name="strWhere">The STR where.</param>
        /// <param name="parms">The parms.</param>
        /// <returns></returns>
        public static DataTable GetList(string strWhere, List<SqlParameter> parms)
        {
            return GeneralInfoDataProvider.GetList(strWhere, parms);
        }
        public static List<GeneralInfo> GetModelList(string strWhere, List<SqlParameter> parms)
        {
            return GeneralInfoDataProvider.GetModelList(strWhere, parms);
        }
        public static List<GeneralInfo> GetModelList(int projectId, int departmentId)
        {
            return GeneralInfoDataProvider.GetModelList(" project_id=" + projectId + " and departmentid=" + departmentId + " and status not in(-1,0,2,4)", new List<SqlParameter>());
        }
        /// <summary>
        /// Gets the status list.
        /// </summary>
        /// <param name="strWhere">The STR where.</param>
        /// <returns></returns>
        public static List<GeneralInfo> GetStatusList(string strWhere)
        {
            return GeneralInfoDataProvider.GetStatusList(strWhere);
        }

        /// <summary>
        /// Gets the status list.
        /// </summary>
        /// <param name="strWhere">The STR where.</param>
        /// <param name="parms">The parms.</param>
        /// <returns></returns>
        public static List<GeneralInfo> GetStatusList(string strWhere, List<SqlParameter> parms)
        {
            return GeneralInfoDataProvider.GetStatusList(strWhere, parms);
        }

        public static List<GeneralInfo> GetRequisitionCommitList(string strWhere, List<SqlParameter> parms)
        {
            return GeneralInfoDataProvider.GetRequisitionCommitList(strWhere, parms);
        }


        /// <summary>
        /// ��ȡ���Խ����ջ���δ�ջ���PR�б�
        /// </summary>
        /// <param name="terms"></param>
        /// <param name="parms"></param>
        /// <returns></returns>
        public static DataTable GetRemindRecipientList(string terms, List<SqlParameter> parms)
        {
            return GeneralInfoDataProvider.GetRemindRecipientList(terms, parms);
        }

        /// <summary>
        /// ����ý�����������ɵ�PR����ѯ
        /// </summary>
        /// <param name="strWhere"></param>
        /// <param name="parms"></param>
        /// <returns></returns>
        public static DataTable GetTableByNewMedia(string strWhere, List<SqlParameter> parms)
        {
            return GeneralInfoDataProvider.GetTableByNewMedia(strWhere, parms);
        }

        /// <summary>
        /// Gets the status list by media.
        /// </summary>
        /// <param name="strWhere">The STR where.</param>
        /// <param name="parms">The parms.</param>
        /// <returns></returns>
        public static List<GeneralInfo> GetStatusListByMedia(string strWhere, List<SqlParameter> parms)
        {
            return GeneralInfoDataProvider.GetStatusListByMedia(strWhere, parms);
        }

        /// <summary>
        /// Gets the status list by pri order.
        /// </summary>
        /// <param name="strWhere">The STR where.</param>
        /// <param name="parms">The parms.</param>
        /// <returns></returns> 
        public static List<GeneralInfo> GetStatusListByPriOrder(string strWhere, List<SqlParameter> parms)
        {
            return GeneralInfoDataProvider.GetStatusListByPriOrder(strWhere, parms);
        }

        /// <summary>
        /// Gets the status list by pri order.
        /// </summary>
        /// <param name="strWhere">The STR where.</param>
        /// <returns></returns>
        public static DataSet GetStatusListByPriOrder(string strWhere)
        {
            return GeneralInfoDataProvider.GetStatusListByPriOrder(strWhere);
        }

        /// <summary>
        /// ������ջ����ջ���Ϊȷ��״̬�����뵥�б�
        /// </summary>
        /// <param name="strWhere"></param>
        /// <param name="parms"></param>
        /// <returns></returns>
        public static List<GeneralInfo> GetPaymentGeneralList(string strWhere, List<SqlParameter> parms)
        {
            return GeneralInfoDataProvider.GetPaymentGeneralList(strWhere, parms);
        }

        /// <summary>
        /// ���ɶ������
        /// </summary>
        /// <returns></returns>
        public static string createOrderID()
        {
            return GeneralInfoDataProvider.createOrderID();
        }

        /// <summary>
        /// �������뵥���
        /// </summary>
        /// <returns></returns>
        public static string createPrNo()
        {
            return GeneralInfoDataProvider.createPrNo();
        }

        /// <summary>
        /// ��������ܼ��Ƿ񳬹���Ԥ����
        /// </summary>
        /// <param name="id">The id.</param>
        /// <param name="price">The price.</param>
        /// <param name="orderid">The orderid.</param>
        /// <returns></returns>
        //public static bool contrastPrice(int id, decimal price, int orderid)
        //{
        //    return GeneralInfoDataProvider.contrastPrice(id, price, orderid);
        //}

        /// <summary>
        /// ��������б�
        /// </summary>
        /// <param name="strWhere">The STR where.</param>
        /// <returns></returns>
        public static IList<GeneralInfo> GetConfrimStatusList(string strWhere)
        {
            return GeneralInfoDataProvider.GetConfrimStatusList(strWhere);
        }

        /// <summary>
        /// Gets the group by suppliername.
        /// </summary>
        /// <param name="wherestr">The wherestr.</param>
        /// <returns></returns>
        public static IList<string[]> getGroupBySuppliername(string wherestr)
        {
            return GeneralInfoDataProvider.getGroupBySuppliername(wherestr);
        }

        /// <summary>
        /// Valids the PRJ code.
        /// </summary>
        /// <param name="prjcode">The prjcode.</param>
        /// <returns></returns>
        //[AjaxPro.AjaxMethod]
        //public static int validPrjCode(string prjcode)
        //{
        //     PRMediaBLL.Project.PrjService service = new PRMediaBLL.Project.PrjService(ESP.Purchase.Common.ServiceURL.ProjectServicePath);
        //     PRMediaBLL.Project.SqlParameter[] parameters = new PRMediaBLL.Project.SqlParameter[1];
        //     PRMediaBLL.Project.SqlParameter param = new PRMediaBLL.Project.SqlParameter();
        //     param.ParameterName = "@ProjectCode";
        //     param.Value = prjcode;
        //    parameters[0]=param;
        //    PRMediaBLL.Project.Media_projects[] prjs = service.GetList(" and ProjectCode=@ProjectCode",parameters);
        //    if (prjs.Length > 0)
        //    {
        //        return prjs[0].Projectid;
        //    }
        //    else
        //    {
        //        return 0;
        //    }
        //}

        /// <summary>
        /// Gets the type of the media fee.
        /// </summary>
        /// <returns></returns>
        [AjaxPro.AjaxMethod]
        public static string[] getMediaFeeType()
        {
            return Common.State.MediaFeeType;
        }


        /// <summary>
        /// �������뵥ID ɾ��ȫ����Ϣ
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns></returns>
        public static bool ClearAllByGeneralId(int id)
        {
            using (SqlConnection conn = new SqlConnection(Common.DbHelperSQL.connectionString))
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();
                try
                {
                    GeneralInfo model = new GeneralInfo();
                    GeneralInfo modelTemp = GetModel(id);
                    model.id = modelTemp.id;
                    model.PrNo = modelTemp.PrNo;
                    model.app_date = modelTemp.app_date;
                    model.lasttime = modelTemp.lasttime;

                    GeneralInfoDataProvider.Update(model, conn, trans);
                    OrderInfoManager.DeleteAllByGeneralId(model.id, trans);
                    PaymentPeriodManager.DeleteAllByGeneralId(model.id, trans);

                    trans.Commit();
                    return true;
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    ESP.Logging.Logger.Add(ex.Message, "GenerlInfoManager.ClearAllByGeneralId", ESP.Logging.LogLevel.Error, ex);
                    return false;
                }
            }
        }

        /// <summary>
        /// ������ϸ����
        /// </summary>
        /// <param name="gid"></param>
        /// <returns></returns>
        public static string GetPNDes(int gid)
        {
            return GeneralInfoDataProvider.GetPNDes(gid);
        }

        /// <summary>
        /// ������pr��ȡ����PR��id
        /// </summary>
        /// <param name="gid">The gid.</param>
        /// <returns></returns>
        public static int GetOldPRIdByNewPRId(int gid)
        {
            return GeneralInfoDataProvider.GetOldPRIdByNewPRId(gid);
        }

        /// <summary>
        /// ������pN��ȡ����PR��id
        /// </summary>
        /// <param name="pnid">The pnid.</param>
        /// <returns></returns>
        public static int GetOldPRIdByNewPNId(int pnid)
        {
            return GeneralInfoDataProvider.GetOldPRIdByNewPNId(pnid);
        }

        /// <summary>
        /// �����˱༭ʱ״̬�Ƿ���ȷ
        /// </summary>
        /// <param name="model"></param>
        /// <param name="checkStatus"></param>
        /// <returns></returns>
        public static bool CheckStatusForAudit(GeneralInfo model, int[] checkStatus)
        {
            foreach (int status in checkStatus)
            {
                if (model.status == status)
                    return true;
            }
            return false;
        }

        #endregion  ��Ա����

        #region workflow method
        public static int AuditComplete(ESP.Purchase.Entity.GeneralInfo generalInfo, ESP.Compatible.Employee CurrentUser, string suggestion, System.Web.HttpRequest request)
        {

            var ret = AuditCompleteInternal(generalInfo, CurrentUser, suggestion, request);

            return ret;

        }
        private static int AuditCompleteInternal(ESP.Purchase.Entity.GeneralInfo generalInfo, ESP.Compatible.Employee CurrentUser, string suggestion, System.Web.HttpRequest request)
        {
            int ret = 0;
            WFProcessMgrImpl processMgr = new WFProcessMgrImpl();//�־ò㹤�����Ĺ�����
            IWFProcess np;//�־ò�Ĺ�����ʵ��(�ӿڶ���)
            Hashtable context = new Hashtable();//���й������������Ĵ洢��
            WorkFlowDAO.WorkItemDataDao workitemdao = new WorkItemDataDao();
            WFUSERS[] initiators;//�������ķ����ߣ��п������ɶ����ͬʱ���𴴽���
            WorkFlowImpl.WorkItemData workitemdata = new WorkFlowImpl.WorkItemData();
            if (!checkAudit(generalInfo, CurrentUser))
            {
                return -1;
            }
            //����������ʵ��
            context = new Hashtable();
            SetWorkItemData(generalInfo, workitemdata, CurrentUser);
            WFUSERS wfuser = new WFUSERS();
            wfuser.Id = generalInfo.requestor;
            initiators = new WFUSERS[1];
            initiators[0] = wfuser;
            context.Add(ContextConstants.CURRENT_USER, wfuser);//�������˼���������
            context.Add(ContextConstants.SUBMIT_ACTION_TYPE, "1");//�ύ�������룺1
            context.Add(ContextConstants.SUBMIT_ACTION_NAME, "PR��ҵ�����");//�ύ�������룺1
            context.Add(ContextConstants.SUBMIT_ACTION_DISPLAYNAME, "PR��ҵ�����");//�ύ�������룺1

            np = processMgr.load_process(generalInfo.ProcessID, generalInfo.InstanceID.ToString(), context);

            //����start����
            np.load_task(generalInfo.WorkitemID.ToString(), generalInfo.WorkItemName);
            np.get_lastActivity().active();

            generalInfo.WorkitemID = Convert.ToInt32(np.get_lastActivity().getWorkItem().Id);
            generalInfo.WorkItemName = np.get_lastActivity().getWorkItem().TASKNAME;
            generalInfo.InstanceID = Convert.ToInt32(np.get_instance_data().Id);
            generalInfo.ProcessID = generalInfo.ProcessID;
            //complete start����
            np.load_task(generalInfo.WorkitemID.ToString(), generalInfo.WorkItemName);
            np.complete_task(generalInfo.WorkitemID.ToString(), generalInfo.WorkItemName, context[ContextConstants.SUBMIT_ACTION_NAME].ToString());
            //���й�����Ѿ���ɣ�����������
            if (np.get_lastActivity() != null)//���к������񣬸ù�������δ����
            {
                //����
                int sysId = int.Parse(np.get_lastActivity().getWorkItem().RoleID);
                ESP.Compatible.Employee employee = new ESP.Compatible.Employee(sysId);

                ArrayList notifylist = (ArrayList)context[ContextConstants.NOTIFY_LIST];//�õ���ǰworkitem�Ļ�֪�˵�ID
                if (notifylist != null && notifylist.Count != 0)//����л�֪�б�
                {
                    string ZHEmails = "";
                    for (int i = 0; i < notifylist.Count; i++)//ѭ�����һ�֪�˵�email
                    {
                        ESP.Compatible.Employee zhEmployee = new ESP.Compatible.Employee(int.Parse(notifylist[i].ToString()));
                        ZHEmails += ESP.Purchase.Common.State.getEmployeeEmailBySysUserId(int.Parse(notifylist[i].ToString())) + ",";
                    }
                    if (ZHEmails != "")
                    {
                        ESP.Purchase.BusinessLogic.SendMailHelper.SendMailToZH(generalInfo, generalInfo.PrNo, CurrentUser.Name, employee.Name, ZHEmails.TrimEnd(','));
                    }
                }
                AuditLogManager.Add(getAuditLog(generalInfo, CurrentUser, true, generalInfo.PrNo, suggestion), request);
                ESP.Purchase.BusinessLogic.SendMailHelper.SendMailPROperaPass(generalInfo, generalInfo.PrNo, CurrentUser.Name, employee.Name, ESP.Purchase.Common.State.getEmployeeEmailBySysUserId(generalInfo.requestor), ESP.Purchase.Common.State.getEmployeeEmailBySysUserId(sysId), 0, true);
                workitemdao.insertItemData(Convert.ToInt32(np.get_lastActivity().getWorkItem().Id), Convert.ToInt32(np.get_instance_data().Id), SerializeFactory.Serialize(generalInfo));
            }
            else//��������ȫ����
            {
                IList<ESP.Compatible.Department> dtdep = ESP.Compatible.Employee.GetDepartments(generalInfo.requestor);
                string nodename = "";
                if (dtdep.Count > 0)
                {
                    string level = dtdep[0].Level.ToString();
                    if (level == "1")
                    {
                        nodename = dtdep[0].NodeName;
                    }
                    else if (level == "2")
                    {
                        ESP.Compatible.Department dep = ESP.Compatible.DepartmentManager.GetDepartmentByPK(dtdep[0].UniqID);
                        nodename = dep.Parent.DepartmentName;

                    }
                    else if (level == "3")
                    {
                        ESP.Compatible.Department dep = ESP.Compatible.DepartmentManager.GetDepartmentByPK(dtdep[0].UniqID);
                        ESP.Compatible.Department dep2 = ESP.Compatible.DepartmentManager.GetDepartmentByPK(dep.Parent.UniqID);
                        nodename = dep2.Parent.DepartmentName;

                    }
                }
                string FiliorAcrName = "";
                string FiliorAcrEmail = "";
                bool isFili = false;
                if (OrderInfoManager.getTypeOperationFlow(generalInfo.id) != ESP.Purchase.Common.State.typeoperationflow_AD)
                {
                    if (nodename != ESP.Purchase.Common.State.filialeName_CQ )
                    {
                        generalInfo.status = ESP.Purchase.Common.State.requisition_commit;
                    }
                    else
                    {
                        generalInfo.status = ESP.Purchase.Common.State.requisition_temporary_commit;
                        string[] filialeAuditor = OrderInfoManager.getFilialeAuditor(generalInfo.id, nodename);
                        if (filialeAuditor != null)
                        {
                            generalInfo.Filiale_Auditor = int.Parse(filialeAuditor[0]);
                            generalInfo.Filiale_AuditName = filialeAuditor[1];
                        }
                    }
                }
                else
                {
                    generalInfo.status = ESP.Purchase.Common.State.order_ADAuditWait;
                }

                if (generalInfo.PRType == (int)ESP.Purchase.Common.PRTYpe.MediaPR || generalInfo.PRType == (int)ESP.Purchase.Common.PRTYpe.PR_MediaFA || generalInfo.PRType == (int)ESP.Purchase.Common.PRTYpe.MPPR || generalInfo.PRType == (int)ESP.Purchase.Common.PRTYpe.PR_PriFA)
                {
                    generalInfo.status = ESP.Purchase.Common.State.order_mediaAuditWait;
                }
                if (generalInfo.PRType == (int)ESP.Purchase.Common.PRTYpe.PR_TMP1 || generalInfo.PRType == (int)ESP.Purchase.Common.PRTYpe.PR_TMP2)
                {
                    generalInfo.status = ESP.Purchase.Common.State.requisition_commit;
                }


                //д�������������¼�

                //�ǲ�����־
                GeneralInfoManager.UpdateAndAddLog(generalInfo, null, getAuditLog(generalInfo, CurrentUser, true, generalInfo.PrNo, suggestion), request);

                FiliorAcrName = generalInfo.Filiale_Auditor == 0 ? (new ESP.Compatible.Employee(generalInfo.first_assessor)).Name : (new ESP.Compatible.Employee(generalInfo.Filiale_Auditor)).Name;
                FiliorAcrEmail = generalInfo.Filiale_Auditor == 0 ? ESP.Purchase.Common.State.getEmployeeEmailBySysUserId(generalInfo.first_assessor) : ESP.Purchase.Common.State.getEmployeeEmailBySysUserId(generalInfo.Filiale_Auditor);
                isFili = generalInfo.Filiale_Auditor == 0 ? false : true;

                ESP.Purchase.BusinessLogic.SendMailHelper.SendMailPRLastOperaPass(generalInfo, generalInfo.PrNo, CurrentUser.Name, FiliorAcrName, ESP.Purchase.Common.State.getEmployeeEmailBySysUserId(generalInfo.requestor), FiliorAcrEmail, isFili, true);
            }
            //���������־
            InsertAuditLog((int)ESP.Purchase.Common.State.operationAudit_status.Yes, generalInfo, CurrentUser, suggestion, request);
            //��¼������־
            ESP.Logging.Logger.Add(string.Format("{0}��T_GeneralInfo���е�id={1}�����ݽ��� {2} �Ĳ���", CurrentUser.Name, generalInfo.id, "ҵ�����ͨ��"), "ҵ�����");
            return ret;
        }
        public static int AuditTerminate(ESP.Purchase.Entity.GeneralInfo generalInfo, ESP.Compatible.Employee CurrentUser, string suggestion, System.Web.HttpRequest request)
        {

            var r = AuditTerminateInternal(generalInfo, CurrentUser, suggestion, request);
            return r;
        }

        private static int AuditTerminateInternal(ESP.Purchase.Entity.GeneralInfo generalInfo, ESP.Compatible.Employee CurrentUser, string suggestion, System.Web.HttpRequest request)
        {
            int ret = 0;
            WFProcessMgrImpl processMgr = new WFProcessMgrImpl();//�־ò㹤�����Ĺ�����
            IWFProcess np;//�־ò�Ĺ�����ʵ��(�ӿڶ���)
            Hashtable context = new Hashtable();//���й������������Ĵ洢��
            WorkFlowDAO.WorkItemDataDao workitemdao = new WorkItemDataDao();
            WFUSERS[] initiators;//�������ķ����ߣ��п������ɶ����ͬʱ���𴴽���
            WorkFlowImpl.WorkItemData workitemdata = new WorkFlowImpl.WorkItemData();
            if (!checkAudit(generalInfo, CurrentUser))
            {
                return -1;
            }
            //����������ʵ��
            context = new Hashtable();
            SetWorkItemData(generalInfo, workitemdata, CurrentUser);
            WFUSERS wfuser = new WFUSERS();
            wfuser.Id = generalInfo.requestor;
            initiators = new WFUSERS[1];
            initiators[0] = wfuser;
            context.Add(ContextConstants.CURRENT_USER, wfuser);//�������˼���������
            context.Add(ContextConstants.SUBMIT_ACTION_TYPE, "1");//�ύ�������룺1
            context.Add(ContextConstants.SUBMIT_ACTION_NAME, "PR��ҵ�����");//�ύ�������룺1
            context.Add(ContextConstants.SUBMIT_ACTION_DISPLAYNAME, "PR��ҵ�����");//�ύ�������룺1

            np = processMgr.load_process(generalInfo.ProcessID, generalInfo.InstanceID.ToString(), context);
            np.terminate();
            //����
            //���������־
            InsertAuditLog((int)ESP.Purchase.Common.State.operationAudit_status.No, generalInfo, CurrentUser, suggestion, request);
            //����״̬�����뵥����
            generalInfo.status = ESP.Purchase.Common.State.requisition_return;

            //�ǲ�����־
            GeneralInfoManager.UpdateAndAddLog(generalInfo, null, getAuditLog(generalInfo, CurrentUser, false, generalInfo.PrNo, suggestion), request);
            //��¼������־
            ESP.Logging.Logger.Add(string.Format("{0}��T_GeneralInfo���е�id={1}�����ݽ��� {2} �Ĳ���", CurrentUser.Name, generalInfo.id, "ҵ����˲���"), "ҵ�����");
            //����
            ESP.Purchase.BusinessLogic.SendMailHelper.SendMailPROperaPass(generalInfo, generalInfo.PrNo, CurrentUser.Name, "", ESP.Purchase.Common.State.getEmployeeEmailBySysUserId(generalInfo.requestor), "", 0, false);
            return ret;
        }
        private static AuditLogInfo getAuditLog(ESP.Purchase.Entity.GeneralInfo generalInfo, ESP.Compatible.Employee CurrentUser, bool isOk, string prNo, string suggestion)
        {
            //�����־
            AuditLogInfo auditLog = new AuditLogInfo();
            auditLog.gid = generalInfo.id;
            auditLog.prNo = prNo;
            auditLog.auditUserId = int.Parse(CurrentUser.SysID);
            auditLog.auditUserName = CurrentUser.Name;
            auditLog.remark = suggestion;
            auditLog.remarkDate = DateTime.Now;
            if (isOk)
                auditLog.auditType = (int)ESP.Purchase.Common.State.operationAudit_status.Yes;
            else
                auditLog.auditType = (int)ESP.Purchase.Common.State.operationAudit_status.No;

            return auditLog;
        }
        private static void SetWorkItemData(GeneralInfo gmodel, WorkFlowImpl.WorkItemData workitemdata, ESP.Compatible.Employee CurrentUser)
        {
            List<WorkFlowModel.WorkItemData> list1 = workitemdata.getProcessDataList(CurrentUser.SysID, "PR��");

            //ȡ����Ȩ����˵����� begin
            List<WorkFlowModel.WorkItemData> list2 = new List<WorkFlowModel.WorkItemData>();
            IList<ESP.Framework.Entity.AuditBackUpInfo> delegates = ESP.Framework.BusinessLogic.AuditBackUpManager.GetModelsByBackUpUserID(Convert.ToInt32(CurrentUser.SysID));
            foreach (ESP.Framework.Entity.AuditBackUpInfo backUp in delegates)
            {
                list2 = workitemdata.getProcessDataList(backUp.UserID.ToString(), "PR��");
                foreach (WorkFlowModel.WorkItemData o in list2)
                {
                    if (((GeneralInfo)o.ItemData).id == gmodel.id)
                    {
                        gmodel.WorkitemID = o.WorkItemID;
                        gmodel.InstanceID = o.InstanceID;
                        gmodel.WorkItemName = o.WorkItemName;
                        gmodel.ProcessID = o.ProcessID;
                        break;
                    }
                }
            }
            //ȡ����Ȩ����˵����� end

            List<GeneralInfo> list = new List<GeneralInfo>();
            foreach (WorkFlowModel.WorkItemData o in list1)
            {
                GeneralInfo model = (GeneralInfo)o.ItemData;
                if (model.id == gmodel.id)
                {
                    gmodel.WorkitemID = o.WorkItemID;
                    gmodel.InstanceID = o.InstanceID;
                    gmodel.WorkItemName = o.WorkItemName;
                    gmodel.ProcessID = o.ProcessID;
                    break;
                }
            }
        }
        public static bool checkAudit(ESP.Purchase.Entity.GeneralInfo generalModel, ESP.Compatible.Employee CurrentUser)
        {
            bool isHave = false;
            WorkFlowImpl.WorkItemData workitemdata = new WorkFlowImpl.WorkItemData();
            List<WorkFlowModel.WorkItemData> list1 = workitemdata.getProcessDataList(CurrentUser.SysID, "PR��");
            List<GeneralInfo> list = new List<GeneralInfo>();
            List<WorkFlowModel.WorkItemData> list2 = null;
            //ȡ����Ȩ����˵����� begin
            IList<ESP.Framework.Entity.AuditBackUpInfo> delegates = ESP.Framework.BusinessLogic.AuditBackUpManager.GetModelsByBackUpUserID(Convert.ToInt32(CurrentUser.SysID));
            foreach (ESP.Framework.Entity.AuditBackUpInfo backUp in delegates)
            {
                list2 = workitemdata.getProcessDataList(backUp.UserID.ToString(), "PR��");
                foreach (WorkFlowModel.WorkItemData o in list2)
                {
                    if (((GeneralInfo)o.ItemData).id == generalModel.id)
                    {
                        isHave = true;
                        break;
                    }
                }
            }
            //ȡ����Ȩ����˵����� end
            foreach (WorkFlowModel.WorkItemData o in list1)
            {
                if (((GeneralInfo)o.ItemData).id == generalModel.id)
                {
                    isHave = true;
                    break;
                }
            }
            return isHave;
        }
        private static void InsertAuditLog(int auditStatus, ESP.Purchase.Entity.GeneralInfo generalModel, ESP.Compatible.Employee CurrentUser, string suggestion, System.Web.HttpRequest request)
        {
            OperationAuditLogInfo auditLog = new OperationAuditLogInfo();
            auditLog.auditorId = int.Parse(CurrentUser.SysID);
            auditLog.auditorName = CurrentUser.Name;
            auditLog.auditTime = DateTime.Now;
            auditLog.Gid = generalModel.id;
            auditLog.audtiStatus = auditStatus;
            auditLog.auditRemark = suggestion.Length > 300 ? suggestion.Substring(0, 300) : suggestion;
            OperationAuditLogManager.Add(auditLog, request);
        }

        /// <summary>
        /// ������뵥��Ա��Ϣ
        /// </summary>
        /// <param name="prIds">���뵥ID</param>
        /// <param name="changeColumn">Ҫ������ֶ�</param>
        /// <param name="oldUserId">ԭʼ��ԱID</param>
        /// <param name="newUserId">����ԱID</param>
        /// <returns></returns>
        public static int ChangePrUsers(string prIds, string changeColumn, int oldUserId, int newUserId, ESP.Compatible.Employee currentUser, System.Web.HttpRequest request)
        {
            return ESP.Purchase.DataAccess.GeneralInfoDataProvider.ChangePrUsers(prIds, changeColumn, oldUserId, newUserId, currentUser, request);
        }

        public static int ChangePrOperationAuditor(string prIds, int oldUserId, int newUserId, ESP.Compatible.Employee currentUser, System.Web.HttpRequest request)
        {
            return DataAccess.GeneralInfoDataProvider.ChangePrOperationAuditor(prIds, oldUserId, newUserId, currentUser, request);
        }

        public static DataTable getGeneralJoinOperationAudit(int oldUserId, string changeColumn)
        {
            return DataAccess.GeneralInfoDataProvider.getGeneralJoinOperationAudit(oldUserId, changeColumn);
        }

        #endregion

        #region "��֤PN�ظ�����"
        public static int GetPaymentPeriodCount(int prid)
        {
            return GeneralInfoDataProvider.GetPaymentPeriodCount(prid);
        }

        public static int GetPNCount(int prid)
        {
            return GeneralInfoDataProvider.GetPNCount(prid);
        }

        #endregion
        public static Dictionary<int, ESP.Purchase.Common.State.PRInUse> GetInUses(int[] prs)
        {
            Dictionary<int, ESP.Purchase.Common.State.PRInUse> dict = new Dictionary<int, ESP.Purchase.Common.State.PRInUse>();
            if (prs == null || prs.Length == 0)
                return dict;

            int start = 0;

            while (start < prs.Length)
            {
                int count = prs.Length - start;
                if (count > 200)
                    count = 200;

                StringBuilder sql = new StringBuilder(@"
select id, inUse from T_GeneralInfo
where id in (");
                sql.Append(prs[start]);
                for (var i = 1; i < count; i++)
                {
                    sql.Append(',').Append(prs[start + i]);
                }
                sql.Append(@")");

                using (var reader = ESP.Finance.DataAccess.DbHelperSQL.ExecuteReader(sql.ToString()))
                {
                    while (reader.Read())
                    {
                        var prid = reader.GetInt32(0);
                        if (reader.IsDBNull(1))
                            continue;
                        var inUse = (ESP.Purchase.Common.State.PRInUse)reader.GetInt32(1);
                        dict[prid] = inUse;
                    }
                }

                start += count;
            }

            return dict;
        }
    }

    /// <summary>
    /// List<T_GeneralInfo>����lasttime��������
    /// </summary>
    public class GeneralInfoComparer : IComparer<GeneralInfo>
    {
        public int Compare(GeneralInfo x, GeneralInfo y)
        {
            return y.lasttime.CompareTo(x.lasttime);
        }

    }
    public class GeneralInfoCompareAudit : IComparer<GeneralInfo>
    {
        public int Compare(GeneralInfo x, GeneralInfo y)
        {
            return y.order_audittime.CompareTo(x.order_audittime);
        }

    }
}
