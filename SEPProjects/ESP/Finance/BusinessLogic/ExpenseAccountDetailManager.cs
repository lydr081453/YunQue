using System;
using System.Data;
using System.Collections.Generic;
using ESP.Finance.Entity;
using ESP.Finance.Utility;
using System.Data.SqlClient;
namespace ESP.Finance.BusinessLogic
{
    /// <summary>
    /// 业务逻辑类ExpenseBLL 的摘要说明。
    /// </summary>


    public static class ExpenseAccountDetailManager
    {

        private static ESP.Finance.IDataAccess.IExpenseAccountDetailProvider DataProvider { get { return ESP.Configuration.ProviderHelper<ESP.Finance.IDataAccess.IExpenseAccountDetailProvider>.Instance; } }


        #region  成员方法

        public static int UpdateTicketUsed(string ids)
        {
            return DataProvider.UpdateTicketUsed(ids);
        }

        public static int UpdateTicketConfirm(string ids)
        {
            return DataProvider.UpdateTicketConfirm(ids);
        }
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public static int Add(ESP.Finance.Entity.ExpenseAccountDetailInfo model)
        {
            return DataProvider.Add(model);
        }

        public static int CloseWorkflow(string instanceid, int workitemid)
        {
            return DataProvider.CloseWorkflow(instanceid, workitemid);
        }
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public static UpdateResult Update(ESP.Finance.Entity.ExpenseAccountDetailInfo model)
        {
            int res = 0;
            try
            {
                res = DataProvider.Update(model);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return UpdateResult.Failed;
            }
            if (res > 0)
            {
                return UpdateResult.Succeed;
            }
            else if (res == 0)
            {
                return UpdateResult.UnExecute;
            }
            return UpdateResult.Failed;
        }

        public static int Update(ESP.Finance.Entity.ExpenseAccountDetailInfo model, SqlTransaction trans)
        {
            return DataProvider.Update(model, trans);

        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public static DeleteResult Delete(int id)
        {

            int res = 0;
            try
            {
                res = DataProvider.Delete(id);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return DeleteResult.Failed;
            }
            if (res > 0)
            {
                return DeleteResult.Succeed;
            }
            else if (res == 0)
            {
                return DeleteResult.UnExecute;
            }
            return DeleteResult.Failed;
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public static ESP.Finance.Entity.ExpenseAccountDetailInfo GetModel(int id)
        {

            return DataProvider.GetModel(id);
        }

        public static DataTable GetIicketCheck(string strWhere)
        {
            return DataProvider.GetIicketCheck(strWhere);
        }

        #region 获得数据列表
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public static List<ExpenseAccountDetailInfo> GetAllList()
        {
            return DataProvider.GetList("");
        }

        public static List<ESP.Finance.Entity.TicketUserInfo> GetTicketUserList(string strWhere)
        {
            return DataProvider.GetTicketUserList(strWhere);
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public static List<ExpenseAccountDetailInfo> GetList(string term)
        {
            return DataProvider.GetList(term);
        }

        public static DataTable  GetDetailListForReport(int batchId)
        {
            return DataProvider.GetDetailListForReport(batchId);
        }

        public static int CheckPhoneInvoiceNo(int detailId, string invoiceNo)
        {
            return DataProvider.CheckPhoneInvoiceNo(detailId, invoiceNo);
        }

        public static List<ExpenseAccountDetailInfo> GetList(int[] returnIds)
        {
            if (returnIds == null || returnIds.Length == 0)
                return new List<ExpenseAccountDetailInfo>();

            var term = new System.Text.StringBuilder(" and ReturnID in (").Append(returnIds[0]);
            for (var i = 1; i < returnIds.Length; i++)
            {
                term.Append(",").Append(returnIds[i]);
            }
            term.Append(") ");

            return DataProvider.GetList(term.ToString());
        }

        public static List<ExpenseAccountDetailInfo> GetTicketCanceled(int[] returnIds)
        {
            if (returnIds == null || returnIds.Length == 0)
                return new List<ExpenseAccountDetailInfo>();

            var term = new System.Text.StringBuilder(" and id in(select parentid from f_return where  ReturnID in (").Append(returnIds[0]);
            for (var i = 1; i < returnIds.Length; i++)
            {
                term.Append(",").Append(returnIds[i]);
            }
            term.Append(") and parentid<>0) ");

            return DataProvider.GetList(term.ToString());
        }

        public static List<ExpenseAccountDetailInfo> GetIicketUsed(string term)
        {
            return DataProvider.GetIicketUsed(term);
        }

        public static List<ExpenseAccountDetailInfo> GetIicketConfirm(string term)
        {
            return DataProvider.GetIicketConfirm(term);
        }



        /// <summary>
        /// 获得报销申请单总额
        /// </summary>
        /// <param name="returnID"></param>
        /// <returns></returns>
        public static Decimal GetTotalMoneyByReturnID(int returnID)
        {
            return DataProvider.GetTotalMoneyByReturnID(returnID);
        }

        /// <summary>
        /// 获得当前报销单下,除了此条明细以外的总金额
        /// </summary>
        /// <param name="ReturnID"></param>
        /// <returns></returns>
        public static Decimal GetTotalMoneyByReturnID(int returnID, int detailID)
        {
            return DataProvider.GetTotalMoneyByReturnID(returnID, detailID);
        }

        /// <summary>
        /// 获得个人手机费可报销月份
        /// </summary>
        /// <param name="year"></param>
        /// <param name="userid"></param>
        /// <returns></returns>
        public static List<int> GetPhoneMonthList(int year, int userid, int detailid)
        {
            return DataProvider.GetPhoneMonthList(year, userid, detailid);
        }

        /// <summary>
        /// 判断餐费是否已报销过
        /// </summary>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <param name="userid"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static bool ExistsMellFee(int year, int month, int userid, string type)
        {
            return DataProvider.ExistsMellFee(year, month, userid, type);
        }

        /// <summary>
        /// 获得此报销单中所有的OOP费用
        /// </summary>
        /// <param name="returnid"></param>
        /// <returns></returns>
        public static decimal GetTotalOOPByReturnID(int returnid)
        {
            return DataProvider.GetTotalOOPByReturnID(returnid);
        }

        /// <summary>
        /// 更新明细状态
        /// </summary>
        /// <param name="returnID"></param>
        /// <param name="status">0 不计算成本  1 计算成本</param>
        /// <returns></returns>
        public static int UpdateStatusByReturnID(int returnID, int status)
        {
            return DataProvider.UpdateStatusByReturnID(returnID, status);
        }
        #endregion 获得数据列表

        /// <summary>
        /// 财务修改明细 记录日志
        /// </summary>
        /// <param name="model"></param>
        /// <param name="detail"></param>
        /// <param name="his"></param>
        /// <returns></returns>
        public static bool UpdateDetailByFinance(ESP.Finance.Entity.ReturnInfo model, ESP.Finance.Entity.ExpenseAccountDetailInfo detail, ESP.Finance.Entity.ExpenseAccountDetailHisInfo his)
        {

            using (SqlConnection conn = new SqlConnection(DataAccess.DbHelperSQL.connectionString))
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();
                try
                {
                    ESP.Finance.BusinessLogic.ExpenseAccountDetailManager.Update(detail);
                    model.PreFee = ESP.Finance.BusinessLogic.ExpenseAccountDetailManager.GetTotalMoneyByReturnID(model.ReturnID);
                    ESP.Finance.BusinessLogic.ReturnManager.Update(model);

                    his.ExpenseDescNew = detail.ExpenseDesc;
                    his.ExpenseMoneyNew = detail.ExpenseMoney;
                    his.CurrentPreFeeNew = model.PreFee;
                    ESP.Finance.BusinessLogic.ExpenseAccountDetailHisManager.Add(his);

                    PNBatchRelationInfo relation = ESP.Finance.BusinessLogic.PNBatchRelationManager.GetModelByReturnId(model.ReturnID, trans);
                    if (relation != null)
                    {
                        PNBatchInfo batch = ESP.Finance.BusinessLogic.PNBatchManager.GetModel(relation.BatchID.Value, trans);

                        batch.Amounts = PNBatchManager.GetTotalAmounts(batch.BatchID, trans);

                        PNBatchManager.Update(batch, trans);
                    }

                    trans.Commit();
                    return true;
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    return false;
                }
            }
        }

        public static ESP.Finance.Entity.ExpenseAccountDetailInfo GetParentModel(int parentId)
        {
            return DataProvider.GetParentModel(parentId);
        }

        public static List<ExpenseAccountDetailInfo> GetTicketDetail(int returnid)
        {
            return DataProvider.GetTicketDetail(returnid);
        }

        #endregion  成员方法
    }
}

