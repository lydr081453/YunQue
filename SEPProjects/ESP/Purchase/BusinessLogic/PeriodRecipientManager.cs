using System.Data;
using System.Collections.Generic;
using ESP.Purchase.DataAccess;
using ESP.Purchase.Entity;

namespace ESP.Purchase.BusinessLogic
{
    /// <summary>
    /// 业务逻辑类PeriodRecipientManager 的摘要说明。
    /// </summary>
    public static class PeriodRecipientManager
    {
        private static readonly PeriodRecipientDataHelper dal = new PeriodRecipientDataHelper();

        #region  成员方法
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public static int Add(PeriodRecipientInfo model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public static void Update(PeriodRecipientInfo model)
        {
            dal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public static void Delete(int id)
        {

            dal.Delete(id);
        }



        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public static PeriodRecipientInfo GetModel(int id)
        {

            return dal.GetModel(id);
        }

        public static PeriodRecipientInfo GetModelByPeriodId(int periodid)
        {

            return dal.GetModelByPeriodId(periodid);
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public static DataSet GetList(string strWhere)
        {
            return dal.GetList(strWhere);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public static List<PeriodRecipientInfo> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            List<PeriodRecipientInfo> modelList = new List<PeriodRecipientInfo>();
            int rowsCount = ds.Tables[0].Rows.Count;
            if (rowsCount > 0)
            {
                PeriodRecipientInfo model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new PeriodRecipientInfo();
                    if (ds.Tables[0].Rows[n]["id"].ToString() != "")
                    {
                        model.id = int.Parse(ds.Tables[0].Rows[n]["id"].ToString());
                    }
                    if (ds.Tables[0].Rows[n]["periodId"].ToString() != "")
                    {
                        model.periodId = int.Parse(ds.Tables[0].Rows[n]["periodId"].ToString());
                    }
                    if (ds.Tables[0].Rows[n]["recipientId"].ToString() != "")
                    {
                        model.recipientId = int.Parse(ds.Tables[0].Rows[n]["recipientId"].ToString());
                    }
                    modelList.Add(model);
                }
            }
            return modelList;
        }

        /// <summary>
        /// 收货单是否关联付款申请
        /// </summary>
        /// <param name="recipientId">收货单Id</param>
        /// <returns></returns>
        public static bool isJoinPeriod(int recipientId)
        {
            return dal.isJoinPeriod(recipientId);
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public static DataSet GetAllList()
        {
            return GetList("");
        }

        #endregion  成员方法
    }
}

