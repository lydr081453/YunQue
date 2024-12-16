using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESP.Administrative.Entity;
using ESP.Administrative.DataAccess;
using System.Data;
using ESP.Administrative.Common;

namespace ESP.Administrative.BusinessLogic
{
    /// <summary>
    /// 用印申请业务类
    /// </summary>
    public class RequestForSealManager
    {
        private readonly RequestForSealDataProvider dal = new RequestForSealDataProvider();
        public RequestForSealManager()
        { }
        #region  成员方法
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int ID)
        {
            return dal.Exists(ID);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(RequestForSealInfo model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public void Update(RequestForSealInfo model)
        {
            dal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public void Delete(int ID)
        {
            dal.Delete(ID);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public RequestForSealInfo GetModel(int ID)
        {
            return dal.GetModel(ID);
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<RequestForSealInfo> GetList(string strWhere)
        {
            return dal.GetList(strWhere);
        }

        /// <summary>
        /// 获取有审批权限的列表
        /// </summary>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        public List<RequestForSealInfo> GetAuditList(string strWhere)
        {
            return dal.GetAuditList(strWhere);
        }

        /// <summary>
        /// 用印申请审批
        /// </summary>
        /// <param name="requestForSealModel"></param>
        /// <param name="currentUser"></param>
        /// <param name="status"></param>
        /// <param name="suggestion"></param>
        /// <returns></returns>
        public int Audit(RequestForSealInfo requestForSealModel, ESP.Compatible.Employee currentUser, int status, string suggestion)
        {
            return dal.Audit(requestForSealModel, currentUser, status, suggestion);
        }
        #endregion  成员方法
    }
}
