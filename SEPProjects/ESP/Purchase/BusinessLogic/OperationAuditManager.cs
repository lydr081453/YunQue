using System.Data;
using System.Collections.Generic;
using System.Data.SqlClient;
using ESP.Purchase.DataAccess;
using ESP.Purchase.Entity;

namespace ESP.Purchase.BusinessLogic
{
    /// <summary>
    /// 业务逻辑类OperationAuditManager 的摘要说明。
    /// </summary>
    public static class OperationAuditManager
    {
        private static readonly OperationAuditDataProvider dal = new OperationAuditDataProvider();

        #region  成员方法

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public static bool Add(List<OperationAuditInfo> models,int gid)
        {
            using (SqlConnection conn = new SqlConnection(Common.DbHelperSQL.connectionString))
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();
                try
                {
                    dal.Delete(gid, conn, trans);
                    foreach (OperationAuditInfo model in models)
                    {
                        dal.Add(model, conn, trans);
                    }
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
        /// 更新一条数据
        /// </summary>
        public static void Update(OperationAuditInfo model)
        {
            dal.Update(model);
        }

        ///// <summary>
        ///// 删除一条数据
        ///// </summary>
        //public static void Delete(int id)
        //{

        //    dal.Delete(id);
        //}

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public static OperationAuditInfo GetModel(int id)
        {

            return dal.GetModel(id);
        }

        /// <summary>
        /// 根据申请单ID，审核人类型获得对象
        /// </summary>
        /// <param name="gid"></param>
        /// <param name="auditType"></param>
        /// <returns></returns>
        public static OperationAuditInfo GetModel(int gid, int auditType)
        {
            return dal.GetModel(gid, auditType);
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
        public static List<OperationAuditInfo> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            List<OperationAuditInfo> modelList = new List<OperationAuditInfo>();
            int rowsCount = ds.Tables[0].Rows.Count;
            if (rowsCount > 0)
            {
                OperationAuditInfo model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new OperationAuditInfo();
                    if (ds.Tables[0].Rows[n]["id"].ToString() != "")
                    {
                        model.id = int.Parse(ds.Tables[0].Rows[n]["id"].ToString());
                    }
                    if (ds.Tables[0].Rows[n]["general_id"].ToString() != "")
                    {
                        model.general_id = int.Parse(ds.Tables[0].Rows[n]["general_id"].ToString());
                    }
                    if (ds.Tables[0].Rows[n]["auditorId"].ToString() != "")
                    {
                        model.auditorId = int.Parse(ds.Tables[0].Rows[n]["auditorId"].ToString());
                    }
                    if (ds.Tables[0].Rows[n]["aduitType"].ToString() != "")
                    {
                        model.aduitType = int.Parse(ds.Tables[0].Rows[n]["aduitType"].ToString());
                    }
                    modelList.Add(model);
                }
            }
            return modelList;
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public static DataSet GetAllList()
        {
            return GetList("");
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        //public DataSet GetList(int PageSize,int PageIndex,string strWhere)
        //{
        //return dal.GetList(PageSize,PageIndex,strWhere);
        //}

        public static List<OperationAuditInfo> getList(int gid, string types)
        {
            bool isOk = false;
            using (SqlConnection conn = new SqlConnection(Common.DbHelperSQL.connectionString))
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();
                try
                {
                    string[] type = types.Split(',');
                    for (int i = 0; i < type.Length; i++)
                    {
                        if(type[i] != "")
                            dal.Delete(gid, int.Parse(type[i]), conn, trans);
                    }
                    isOk = true;
                    trans.Commit();
                    
                }
                catch
                {
                    trans.Rollback();
                    isOk = false;
                }
            }
            if (isOk)
            {
                return GetModelList(string.Format(" general_id = {0}",gid.ToString()));
            }
            return new List<OperationAuditInfo>();
        }


        #endregion  成员方法
    }
}