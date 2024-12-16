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
    /// 业务逻辑类GeneralInfoManager 的摘要说明。
    /// </summary>
    public static class GeneralInfoManager
    {
        #region  成员方法
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
        /// 3000以下的媒介报销单生成后发送到财务审核人
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
                    if (g.Departmentid == projectModel.GroupID.Value)//主申请方
                    {
                        if (currentTotal + refund > ESP.Finance.BusinessLogic.ContractCostManager.GetTotalAmountByProject(projectModel.ProjectId) - total)
                            isOver = true;
                        else
                            isOver = false;
                    }
                    else//支持方
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
                    if (g.app_date <= closeList[0].Time)//PR单在项目号关闭前就申请了
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
                        return false;//项目关闭后申请的
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
                if (IsAudit == true)//如果是审批，只判断是否关闭，预关闭的可以继续进行
                {
                    if (projectModel.Status == (int)ESP.Finance.Utility.Status.ProjectClosed)
                    {
                        isClosed = true;
                    }
                }
                else//如果是在创建时，预关闭项目号也不能创建
                {
                    //如果PR单是被驳回的，预关闭的项目号也可以继续提交
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
        /// 启用或暂停项目下的申请单
        /// </summary>
        /// <param name="projectId">项目ID</param>
        /// <param name="isUse">是否启用</param>
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
                //记录操作日志
                ESP.Logging.Logger.Add(string.Format("{0}对T_GeneralInfo表中的id={1}的数据进行 {2} 的操作", userId, model.id, isUse == true ? "启用申请单项目" : "暂停申请单项目"), isUse == true ? "启用申请单项目" : "暂停申请单项目");

                LogInfo log = new LogInfo();
                log.Gid = model.id;
                log.LogMedifiedTeme = DateTime.Now;
                log.LogUserId = userId;
                log.Des = string.Format((isUse == true ? Common.State.log_usedProject : Common.State.log_disabledProject), userName, model.id.ToString("0000000"));
                new ESP.Purchase.DataAccess.LogDataProvider().Add(log, request, trans.Connection, trans);
            }
        }

        /// <summary>
        /// 增加一条数据
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
        /// 更新一条数据
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
        /// 更新申请单并记录日志，无用，请使用另一个重载
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
        /// 更新申请单并记录日志
        /// </summary>
        /// <param name="model">申请单对象</param>
        /// <param name="log">日志对象</param>
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
        /// 记录PR单成本信息。押金不记录，李彦娥生成的大于3000的媒体稿费单和对私单不进行记录
        /// </summary>
        /// <param name="model"></param>
        /// <param name="returnModel">小于3000生成的PN单Model</param>
        /// <param name="trans"></param>
        /// <param name="crType">成本记录类型</param>
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
                ////大于3000生成的新PR单
                //costRecord.PreAmount = model.totalprice;
                //costRecord.FactAmount = model.totalprice;
                //costRecord.FormType = ESP.Purchase.Common.State.CostRecord_FormType_PRNew;
                return;
            }
            else
            {
                //小于3000生成的PN单
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
        /// 删除一条数据
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
        /// 删除t_generalinfo,t_orderinfo,t_mediaorder
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
        /// 获取供应商或Pr关联的框架合同列表
        /// </summary>
        /// <param name="supplierName">供应商名称</param>
        /// <param name="FCPrIds">Pr关联框架合同prids</param>
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
        /// 得到一个对象实体
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
        /// 获得数据列表
        /// </summary>
        /// <param name="strWhere">The STR where.</param>
        /// <returns></returns>
        public static DataSet GetList(string strWhere)
        {
            return GeneralInfoDataProvider.GetList(strWhere);
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        /// <returns></returns>
        public static DataSet GetAllList()
        {
            return GetList("");
        }

        /// <summary>
        /// 获得单子列表，每条单子对应自己多个的采购物品
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
        /// 获取可以进行收货但未收货的PR列表
        /// </summary>
        /// <param name="terms"></param>
        /// <param name="parms"></param>
        /// <returns></returns>
        public static DataTable GetRemindRecipientList(string terms, List<SqlParameter> parms)
        {
            return GeneralInfoDataProvider.GetRemindRecipientList(terms, parms);
        }

        /// <summary>
        /// 用于媒介或个人新生成的PR单查询
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
        /// 获得已收货并收货单为确认状态的申请单列表
        /// </summary>
        /// <param name="strWhere"></param>
        /// <param name="parms"></param>
        /// <returns></returns>
        public static List<GeneralInfo> GetPaymentGeneralList(string strWhere, List<SqlParameter> parms)
        {
            return GeneralInfoDataProvider.GetPaymentGeneralList(strWhere, parms);
        }

        /// <summary>
        /// 生成订单编号
        /// </summary>
        /// <returns></returns>
        public static string createOrderID()
        {
            return GeneralInfoDataProvider.createOrderID();
        }

        /// <summary>
        /// 生成申请单编号
        /// </summary>
        /// <returns></returns>
        public static string createPrNo()
        {
            return GeneralInfoDataProvider.createPrNo();
        }

        /// <summary>
        /// 检查物料总价是否超过了预算金额
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
        /// 获得数据列表
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
        /// 根据申请单ID 删除全部信息
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
        /// 费用明细描述
        /// </summary>
        /// <param name="gid"></param>
        /// <returns></returns>
        public static string GetPNDes(int gid)
        {
            return GeneralInfoDataProvider.GetPNDes(gid);
        }

        /// <summary>
        /// 根据新pr单取得老PR单id
        /// </summary>
        /// <param name="gid">The gid.</param>
        /// <returns></returns>
        public static int GetOldPRIdByNewPRId(int gid)
        {
            return GeneralInfoDataProvider.GetOldPRIdByNewPRId(gid);
        }

        /// <summary>
        /// 根据新pN单取得老PR单id
        /// </summary>
        /// <param name="pnid">The pnid.</param>
        /// <returns></returns>
        public static int GetOldPRIdByNewPNId(int pnid)
        {
            return GeneralInfoDataProvider.GetOldPRIdByNewPNId(pnid);
        }

        /// <summary>
        /// 检查审核编辑时状态是否正确
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

        #endregion  成员方法

        #region workflow method
        public static int AuditComplete(ESP.Purchase.Entity.GeneralInfo generalInfo, ESP.Compatible.Employee CurrentUser, string suggestion, System.Web.HttpRequest request)
        {

            var ret = AuditCompleteInternal(generalInfo, CurrentUser, suggestion, request);

            return ret;

        }
        private static int AuditCompleteInternal(ESP.Purchase.Entity.GeneralInfo generalInfo, ESP.Compatible.Employee CurrentUser, string suggestion, System.Web.HttpRequest request)
        {
            int ret = 0;
            WFProcessMgrImpl processMgr = new WFProcessMgrImpl();//持久层工作流的管理类
            IWFProcess np;//持久层的工作流实例(接口对象)
            Hashtable context = new Hashtable();//所有工作流对外对象的存储器
            WorkFlowDAO.WorkItemDataDao workitemdao = new WorkItemDataDao();
            WFUSERS[] initiators;//工作流的发起者，有可能是由多个人同时发起创建的
            WorkFlowImpl.WorkItemData workitemdata = new WorkFlowImpl.WorkItemData();
            if (!checkAudit(generalInfo, CurrentUser))
            {
                return -1;
            }
            //创建工作流实例
            context = new Hashtable();
            SetWorkItemData(generalInfo, workitemdata, CurrentUser);
            WFUSERS wfuser = new WFUSERS();
            wfuser.Id = generalInfo.requestor;
            initiators = new WFUSERS[1];
            initiators[0] = wfuser;
            context.Add(ContextConstants.CURRENT_USER, wfuser);//将发起人加入上下文
            context.Add(ContextConstants.SUBMIT_ACTION_TYPE, "1");//提交操作代码：1
            context.Add(ContextConstants.SUBMIT_ACTION_NAME, "PR单业务审核");//提交操作代码：1
            context.Add(ContextConstants.SUBMIT_ACTION_DISPLAYNAME, "PR单业务审核");//提交操作代码：1

            np = processMgr.load_process(generalInfo.ProcessID, generalInfo.InstanceID.ToString(), context);

            //激活start任务
            np.load_task(generalInfo.WorkitemID.ToString(), generalInfo.WorkItemName);
            np.get_lastActivity().active();

            generalInfo.WorkitemID = Convert.ToInt32(np.get_lastActivity().getWorkItem().Id);
            generalInfo.WorkItemName = np.get_lastActivity().getWorkItem().TASKNAME;
            generalInfo.InstanceID = Convert.ToInt32(np.get_instance_data().Id);
            generalInfo.ProcessID = generalInfo.ProcessID;
            //complete start任务
            np.load_task(generalInfo.WorkitemID.ToString(), generalInfo.WorkItemName);
            np.complete_task(generalInfo.WorkitemID.ToString(), generalInfo.WorkItemName, context[ContextConstants.SUBMIT_ACTION_NAME].ToString());
            //所有工作项都已经完成，工作流结束
            if (np.get_lastActivity() != null)//还有后续任务，该工作流尚未结束
            {
                //发信
                int sysId = int.Parse(np.get_lastActivity().getWorkItem().RoleID);
                ESP.Compatible.Employee employee = new ESP.Compatible.Employee(sysId);

                ArrayList notifylist = (ArrayList)context[ContextConstants.NOTIFY_LIST];//得到当前workitem的会知人的ID
                if (notifylist != null && notifylist.Count != 0)//如果有会知列表
                {
                    string ZHEmails = "";
                    for (int i = 0; i < notifylist.Count; i++)//循环查找会知人的email
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
            else//工作流完全结束
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


                //写工作流结束的事件

                //记操作日志
                GeneralInfoManager.UpdateAndAddLog(generalInfo, null, getAuditLog(generalInfo, CurrentUser, true, generalInfo.PrNo, suggestion), request);

                FiliorAcrName = generalInfo.Filiale_Auditor == 0 ? (new ESP.Compatible.Employee(generalInfo.first_assessor)).Name : (new ESP.Compatible.Employee(generalInfo.Filiale_Auditor)).Name;
                FiliorAcrEmail = generalInfo.Filiale_Auditor == 0 ? ESP.Purchase.Common.State.getEmployeeEmailBySysUserId(generalInfo.first_assessor) : ESP.Purchase.Common.State.getEmployeeEmailBySysUserId(generalInfo.Filiale_Auditor);
                isFili = generalInfo.Filiale_Auditor == 0 ? false : true;

                ESP.Purchase.BusinessLogic.SendMailHelper.SendMailPRLastOperaPass(generalInfo, generalInfo.PrNo, CurrentUser.Name, FiliorAcrName, ESP.Purchase.Common.State.getEmployeeEmailBySysUserId(generalInfo.requestor), FiliorAcrEmail, isFili, true);
            }
            //插入审核日志
            InsertAuditLog((int)ESP.Purchase.Common.State.operationAudit_status.Yes, generalInfo, CurrentUser, suggestion, request);
            //记录操作日志
            ESP.Logging.Logger.Add(string.Format("{0}对T_GeneralInfo表中的id={1}的数据进行 {2} 的操作", CurrentUser.Name, generalInfo.id, "业务审核通过"), "业务审核");
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
            WFProcessMgrImpl processMgr = new WFProcessMgrImpl();//持久层工作流的管理类
            IWFProcess np;//持久层的工作流实例(接口对象)
            Hashtable context = new Hashtable();//所有工作流对外对象的存储器
            WorkFlowDAO.WorkItemDataDao workitemdao = new WorkItemDataDao();
            WFUSERS[] initiators;//工作流的发起者，有可能是由多个人同时发起创建的
            WorkFlowImpl.WorkItemData workitemdata = new WorkFlowImpl.WorkItemData();
            if (!checkAudit(generalInfo, CurrentUser))
            {
                return -1;
            }
            //创建工作流实例
            context = new Hashtable();
            SetWorkItemData(generalInfo, workitemdata, CurrentUser);
            WFUSERS wfuser = new WFUSERS();
            wfuser.Id = generalInfo.requestor;
            initiators = new WFUSERS[1];
            initiators[0] = wfuser;
            context.Add(ContextConstants.CURRENT_USER, wfuser);//将发起人加入上下文
            context.Add(ContextConstants.SUBMIT_ACTION_TYPE, "1");//提交操作代码：1
            context.Add(ContextConstants.SUBMIT_ACTION_NAME, "PR单业务审核");//提交操作代码：1
            context.Add(ContextConstants.SUBMIT_ACTION_DISPLAYNAME, "PR单业务审核");//提交操作代码：1

            np = processMgr.load_process(generalInfo.ProcessID, generalInfo.InstanceID.ToString(), context);
            np.terminate();
            //驳回
            //插入审核日志
            InsertAuditLog((int)ESP.Purchase.Common.State.operationAudit_status.No, generalInfo, CurrentUser, suggestion, request);
            //更新状态到申请单驳回
            generalInfo.status = ESP.Purchase.Common.State.requisition_return;

            //记操作日志
            GeneralInfoManager.UpdateAndAddLog(generalInfo, null, getAuditLog(generalInfo, CurrentUser, false, generalInfo.PrNo, suggestion), request);
            //记录操作日志
            ESP.Logging.Logger.Add(string.Format("{0}对T_GeneralInfo表中的id={1}的数据进行 {2} 的操作", CurrentUser.Name, generalInfo.id, "业务审核驳回"), "业务审核");
            //发信
            ESP.Purchase.BusinessLogic.SendMailHelper.SendMailPROperaPass(generalInfo, generalInfo.PrNo, CurrentUser.Name, "", ESP.Purchase.Common.State.getEmployeeEmailBySysUserId(generalInfo.requestor), "", 0, false);
            return ret;
        }
        private static AuditLogInfo getAuditLog(ESP.Purchase.Entity.GeneralInfo generalInfo, ESP.Compatible.Employee CurrentUser, bool isOk, string prNo, string suggestion)
        {
            //审核日志
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
            List<WorkFlowModel.WorkItemData> list1 = workitemdata.getProcessDataList(CurrentUser.SysID, "PR单");

            //取得授权审核人的数据 begin
            List<WorkFlowModel.WorkItemData> list2 = new List<WorkFlowModel.WorkItemData>();
            IList<ESP.Framework.Entity.AuditBackUpInfo> delegates = ESP.Framework.BusinessLogic.AuditBackUpManager.GetModelsByBackUpUserID(Convert.ToInt32(CurrentUser.SysID));
            foreach (ESP.Framework.Entity.AuditBackUpInfo backUp in delegates)
            {
                list2 = workitemdata.getProcessDataList(backUp.UserID.ToString(), "PR单");
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
            //取得授权审核人的数据 end

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
            List<WorkFlowModel.WorkItemData> list1 = workitemdata.getProcessDataList(CurrentUser.SysID, "PR单");
            List<GeneralInfo> list = new List<GeneralInfo>();
            List<WorkFlowModel.WorkItemData> list2 = null;
            //取得授权审核人的数据 begin
            IList<ESP.Framework.Entity.AuditBackUpInfo> delegates = ESP.Framework.BusinessLogic.AuditBackUpManager.GetModelsByBackUpUserID(Convert.ToInt32(CurrentUser.SysID));
            foreach (ESP.Framework.Entity.AuditBackUpInfo backUp in delegates)
            {
                list2 = workitemdata.getProcessDataList(backUp.UserID.ToString(), "PR单");
                foreach (WorkFlowModel.WorkItemData o in list2)
                {
                    if (((GeneralInfo)o.ItemData).id == generalModel.id)
                    {
                        isHave = true;
                        break;
                    }
                }
            }
            //取得授权审核人的数据 end
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
        /// 变更申请单人员信息
        /// </summary>
        /// <param name="prIds">申请单ID</param>
        /// <param name="changeColumn">要变更的字段</param>
        /// <param name="oldUserId">原始人员ID</param>
        /// <param name="newUserId">新人员ID</param>
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

        #region "验证PN重复申请"
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
    /// List<T_GeneralInfo>根据lasttime降序排列
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
