using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using ESP.Administrative.Entity;
using ESP.Administrative.Common;


namespace ESP.Administrative.BusinessLogic
{
    /// <summary>
    /// 业务逻辑类MatterReasonInfo 的摘要说明。
    /// </summary>
     
     
    public class MatterReasonManager
    {
        private readonly ESP.Administrative.DataAccess.MatterReasonDataProvider dal = new ESP.Administrative.DataAccess.MatterReasonDataProvider();
        public MatterReasonManager()
        { }
        #region  成员方法

        /// <summary>
        /// 得到最大ID
        /// </summary>
        public int GetMaxId()
        {
            return dal.GetMaxId();
        }

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int ID)
        {
            return dal.Exists(ID);
        }

        // <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(MatterReasonInfo model)
        {
            int returnValue = 0;
            using (SqlConnection conn = new SqlConnection(Common.DbHelperSQL.connectionString))
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();
                try
                {
                    returnValue = dal.Add(model);
                    trans.Commit();
                }
                catch (Exception)
                {
                    trans.Rollback();
                    returnValue = 0;
                }
            }
            return returnValue;
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public void Update(ESP.Administrative.Entity.MatterReasonInfo model)
        {
            dal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public void Delete(int id)
        {
            dal.Delete(id);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public ESP.Administrative.Entity.MatterReasonInfo GetModel(int ID)
        {
            return dal.GetModel(ID);
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public IList<MatterReasonInfo> GetList(string strWhere)
        {
            return dal.GetList(strWhere);
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public IList<MatterReasonInfo> GetAllList()
        {
            return dal.GetList("");
        }
        #endregion  成员方法
    }
}

