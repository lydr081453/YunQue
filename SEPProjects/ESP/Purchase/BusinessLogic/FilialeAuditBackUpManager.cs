using System.Data;
using System.Collections.Generic;
using ESP.Purchase.DataAccess;
using ESP.Purchase.Entity;

namespace ESP.Purchase.BusinessLogic
{
    /// <summary>
    /// 业务逻辑类FilialeAuditBackUpManager 的摘要说明。
    /// </summary>
    public static class FilialeAuditBackUpManager
    {
        private static FilialeAuditBackUpDataProvider dal = new FilialeAuditBackUpDataProvider();

        #region  成员方法
        /// <summary>
        /// 增加一条数据
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public static int Add(FilialeAuditBackUpInfo model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public static int Update(FilialeAuditBackUpInfo model)
        {
           return dal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        /// <param name="id">The id.</param>
        public static void Delete(int id)
        {
            dal.Delete(id);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns></returns>
        public static FilialeAuditBackUpInfo GetModel(int id)
        {
            return dal.GetModel(id);
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
        /// 获得数据列表
        /// </summary>
        /// <param name="strWhere">The STR where.</param>
        /// <returns></returns>
        public static List<FilialeAuditBackUpInfo> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            List<FilialeAuditBackUpInfo> modelList = new List<FilialeAuditBackUpInfo>();
            int rowsCount = ds.Tables[0].Rows.Count;
            if (rowsCount > 0)
            {
                FilialeAuditBackUpInfo model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new FilialeAuditBackUpInfo();
                    if (ds.Tables[0].Rows[n]["id"].ToString() != "")
                    {
                        model.id = int.Parse(ds.Tables[0].Rows[n]["id"].ToString());
                    }
                    if (ds.Tables[0].Rows[n]["userid"].ToString() != "")
                    {
                        model.userid = int.Parse(ds.Tables[0].Rows[n]["userid"].ToString());
                    }
                    if (ds.Tables[0].Rows[n]["isBackupUser"].ToString() != "")
                    {
                        model.isBackupUser = int.Parse(ds.Tables[0].Rows[n]["isBackupUser"].ToString());
                    }
                    model.BeginDate = ds.Tables[0].Rows[n]["BeginDate"].ToString();
                    model.EndDate = ds.Tables[0].Rows[n]["EndDate"].ToString();
                    modelList.Add(model);
                }
            }
            return modelList;
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
        /// 通过用户ID得到一个对象实体
        /// </summary>
        /// <param name="userid">The userid.</param>
        /// <returns></returns>
        public static FilialeAuditBackUpInfo GetModelByUserid(int userid)
        {
            return dal.GetModelByUserid(userid);
        }
        #endregion  成员方法
    }
}