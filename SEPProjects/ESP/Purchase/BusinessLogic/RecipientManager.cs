using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using ESP.Purchase.Common;
using ESP.Purchase.DataAccess;
using ESP.Purchase.Entity;

namespace ESP.Purchase.BusinessLogic
{
    /// <summary>
    ///RecipientManager 的摘要说明
    /// </summary>
    public static class RecipientManager
    {
        private static RecipientDataHelper dal = new RecipientDataHelper();

        #region  成员方法
        /// <summary>
        /// 增加一条数据
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public static int Add(RecipientInfo model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 添加数据并更新GeneralInfo
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="generalModel">The general model.</param>
        /// <returns></returns>
        public static int AddAndUpdateStatus(RecipientInfo model, GeneralInfo generalModel,List<Entity.ScoreRecordInfo> scoreList)
        {
            return dal.AddAndUpdateStatus(model, generalModel,scoreList);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        /// <param name="model">The model.</param>
        public static void Update(RecipientInfo model)
        {
            RecipientDataHelper.Update(model);
        }

        public static void UpdateConfirm(RecipientInfo model)
        {
            RecipientDataHelper.UpdateConfirm(model);
        }

        public static void Update(RecipientInfo model,SqlTransaction trans)
        {
            RecipientDataHelper.Update(model, trans);
        }

        /// <summary>
        /// 删除一条数据并记录日志
        /// </summary>
        /// <param name="Id">The id.</param>
        public static bool Delete(int Id, string CurrentUser, System.Web.HttpRequest request)
        {
            //RecipientDataHelper.Delete(Id);
            using (SqlConnection conn = new SqlConnection(Common.DbHelperSQL.connectionString))
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();
                try
                {
                    RecipientInfo recipientModel = RecipientManager.GetModel(Id);
                    RecipientDataHelper.Delete(Id, conn, trans);//删除收货
                    GeneralInfo generalModel = GeneralInfoManager.GetModel(recipientModel.Gid);
                    ESP.ITIL.BusinessLogic.申请单业务设置.申请单撤销收货(trans, ref generalModel);
                    GeneralInfoDataProvider.Update(generalModel, conn, trans);
                    trans.Commit();
                    return true;
                }
                catch
                {
                    trans.Rollback();
                    return false;
                }
            }
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        /// <param name="Id">The id.</param>
        /// <returns></returns>
        public static RecipientInfo GetModel(int Id)
        {
            return RecipientDataHelper.GetModel(Id);
        }

        /// <summary>
        /// 获得每条单子第一次收货的对象
        /// </summary>
        /// <param name="generalId">The general id.</param>
        /// <returns></returns>
        public static RecipientInfo getTopModelByGeneralID(int generalId)
        {
            return RecipientDataHelper.getTopModelByGeneralID(generalId);
        }

        /// <summary>
        /// 更新付款状态
        /// </summary>
        /// <param name="recipientIds">更新的Ids</param>
        /// <param name="receivePrice">状态</param>
        public static void updateStatus(string recipientIds, int receivePrice)
        {
            RecipientDataHelper.updateStatus(recipientIds, receivePrice);
        }

        /// <summary>
        /// 更新确认状态为确认(内部员工确认不记日志）
        /// </summary>
        /// <param name="recipientId">The recipient id.</param>
        /// <param name="generalInfo">The general info.</param>
        /// <param name="reccount">The reccount.</param>
        /// <param name="saveLog">if set to <c>true</c> [save log].</param>
        /// <returns></returns>
        public static bool updateConfirm(int recipientId, GeneralInfo generalInfo, int reccount, bool saveLog, System.Web.HttpRequest request, int currentUserId)
        {
            RecipientInfo recipientModel = RecipientManager.GetModel(recipientId);
            if (saveLog)
            {
                using (SqlConnection conn = new SqlConnection(Common.DbHelperSQL.connectionString))
                {
                    conn.Open();
                    SqlTransaction trans = conn.BeginTransaction();
                    try
                    {
                        List<Entity.RecipientLogInfo> logList = BusinessLogic.RecipientLogManager.GetLoglist(" and a.rid=" + recipientId, new List<SqlParameter>());
                        if (recipientModel.IsConfirm != State.recipentConfirm_PaymentCommit && recipientModel.IsConfirm != State.recipentConfirm_PaymentCommit)
                            RecipientDataHelper.updateConfirm(recipientId, Common.State.recipentConfirm_Supplier, conn, trans);

                        //收货日志----begin
                        bool haveLog = false;
                        foreach (Entity.RecipientLogInfo mLog in logList)
                        {
                            if (mLog.Des.IndexOf("已确认") != -1 && mLog.Des.IndexOf("收货单") != -1)
                            {
                                haveLog = true;
                                break;
                            }
                        }
                        if (!haveLog)
                        {
                            RecipientLogInfo rlog = new RecipientLogInfo();
                            rlog.Des = string.Format(Common.State.log_recipient_confrim, generalInfo.supplier_name, recipientModel.RecipientNo, DateTime.Now.ToString());
                            rlog.LogMedifiedTeme = DateTime.Now;
                            rlog.Rid = recipientId;
                            RecipientLogManager.AddLog(rlog, request);

                            LogInfo log = new LogInfo();
                            //log.Des = "供应商:" + generalInfo.supplier_name + "在" + DateTime.Now.ToString("yyyy-MM-dd") + "对此订单的第" + reccount.ToString() + "次收货进行了确认";
                            log.Des = string.Format(Common.State.log_recipient_confrim, generalInfo.supplier_name, recipientModel.RecipientNo, DateTime.Now.ToString());
                            log.LogMedifiedTeme = DateTime.Now;
                            log.Gid = generalInfo.id;
                            LogManager.AddLog(log, request); 
                        }

                        trans.Commit();
                    }
                    catch
                    {
                        trans.Rollback();
                        return false;
                    }
                }
            }
            else
            {
                try
                {
                    if (generalInfo.appendReceiver == 0)
                    {
                        RecipientDataHelper.updateConfirm(recipientId, State.recipentConfirm_Emp2, null, null);
                    }
                    else
                    {
                        if (currentUserId == generalInfo.appendReceiver)
                            RecipientDataHelper.updateConfirm(recipientId, State.recipentConfirm_Emp2, null, null);
                        else if (currentUserId == generalInfo.goods_receiver)
                            RecipientDataHelper.updateConfirm(recipientId, State.recipentConfirm_Emp1, null, null);
                    }
                }
                catch
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        /// <param name="strWhere">The STR where.</param>
        /// <returns></returns>
        public static DataSet GetList(string strWhere)
        {
            return dal.GetList(strWhere);
        }

        /// <summary>
        /// Gets the model list.
        /// </summary>
        /// <param name="terms">The terms.</param>
        /// <param name="parms">The parms.</param>
        /// <returns></returns>
        public static List<RecipientInfo> getModelList(string terms, List<SqlParameter> parms)
        {
            return RecipientDataHelper.getModelList(terms, parms);
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        /// <returns></returns>
        public static DataSet GetAllList()
        {
            return dal.GetList("");
        }

        /// <summary>
        /// 获得已收货列表
        /// </summary>
        /// <param name="strWhere">The STR where.</param>
        /// <param name="parms">The parms.</param>
        /// <returns></returns>
        public static DataSet GetRecipientList(string strWhere, List<SqlParameter> parms)
        {
            return RecipientDataHelper.GetRecipientList(strWhere, parms);
        }

        /// <summary>
        /// 获得订单收货次数
        /// </summary>
        /// <param name="generalid">流水号</param>
        /// <returns></returns>
        public static int getRecipientCount(int generalid)
        {
            return RecipientDataHelper.getRecipientCount(generalid);
        }

        /// <summary>
        /// 获得流水号ID的聚合
        /// </summary>
        /// <param name="wherestr">The wherestr.</param>
        /// <returns></returns>
        public static IList<string> getGroupByRecipient(string wherestr)
        {
            return RecipientDataHelper.getGroupByRecipient(wherestr);
        }

        /// <summary>
        /// 检查传进来的一组收货ID中是否存在金额不符的收货
        /// </summary>
        /// <param name="wherestr">The wherestr.</param>
        /// <returns>
        /// 	<c>true</c> if [is unsure recipient] [the specified wherestr]; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsUnsureRecipient(string wherestr)
        {
            return RecipientDataHelper.IsUnsureRecipient(wherestr);
        }

        public static bool IsUnsureRecipient(string wherestr, System.Data.SqlClient.SqlConnection conn, System.Data.SqlClient.SqlTransaction trans)
        {
            return RecipientDataHelper.IsUnsureRecipient(wherestr, conn, trans);
        }
        /// <summary>
        /// 取得一个pr单里面所有的收货总金额
        /// </summary>
        /// <param name="generalid">The generalid.</param>
        /// <returns></returns>
        public static decimal getRecipientSum(int generalid)
        {
            return RecipientDataHelper.getRecipientSum(generalid);
        }
        public static decimal getRecipientSum(int generalid, System.Data.SqlClient.SqlConnection conn, System.Data.SqlClient.SqlTransaction trans)
        {
            return RecipientDataHelper.getRecipientSum(generalid, conn, trans);
        }
        /// <summary>
        /// 生成收货单号
        /// </summary>
        /// <param name="gid">The gid.</param>
        /// <param name="isfp">if set to <c>true</c> [isfp].</param>
        /// <returns></returns>
        public static string CreateRecipientNo(int gid, bool isfp)
        {
            string RNo = string.Empty;
            if (gid > 0)
            {
                GeneralInfo g = GeneralInfoManager.GetModel(gid);
                string prNo = g.PrNo;
                if (prNo != string.Empty)
                {
                    if (isfp)
                    {
                        int num = 0;
                        List<RecipientInfo> list = RecipientDataHelper.getModelList(" and gid=" + gid, new List<SqlParameter>());
                        foreach (RecipientInfo model in list)
                        {
                            if (model.RecipientNo.Split('-').Length > 1)
                            {
                                if (int.Parse(model.RecipientNo.Split('-')[1]) > num)
                                    num = int.Parse(model.RecipientNo.Split('-')[1]);
                            }
                        }
                        RNo = prNo.Replace("PR", "GR") + "-" + (num + 1).ToString();
                    }
                    else
                        RNo = prNo.Replace("PR", "GR");
                }
            }
            return RNo;
        }

        /// <summary>
        /// 获取未创建付款申请的收货信息
        /// </summary>
        /// <param name="terms"></param>
        /// <param name="parms"></param>
        /// <returns></returns>
        public static DataSet getNotCreatePNList(string terms, List<SqlParameter> parms)
        {
            return DataAccess.RecipientDataHelper.getNotCreatePNList(terms, parms);
        }

        public static DataSet GetPaymentNotifyAuditing(string terms, List<SqlParameter> parms)
        {
            return DataAccess.RecipientDataHelper.GetPaymentNotifyAuditing(terms, parms);
        }
        /// <summary>
        /// 获取已经关联收货的总金额
        /// </summary>
        /// <param name="generalID"></param>
        /// <returns></returns>
        public static decimal GetSumRecipientConfirmed(int generalID)
        {
            return DataAccess.RecipientDataHelper.GetSumRecipientConfirmed(generalID);
        }
        #endregion  成员方法
    }
}