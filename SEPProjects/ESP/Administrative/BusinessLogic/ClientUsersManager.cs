using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESP.Administrative.Entity;
using ESP.Administrative.DataAccess;
using System.Data;

namespace ESP.Administrative.BusinessLogic
{
    public class ClientUsersManager
    {
        private readonly ClientUsersDataProvider dal = new ClientUsersDataProvider();

        public ClientUsersManager()
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
        public int Add(ClientUsersInfo model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public void Update(ClientUsersInfo model)
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
        public ClientUsersInfo GetModel(int ID)
        {
            return dal.GetModel(ID);
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            return dal.GetList(strWhere);
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetAllList()
        {
            return dal.GetList("");
        }

        /// <summary>
        /// 判断用户是否是驻客户端用户，如果是返回true，否则返回false
        /// </summary>
        /// <param name="userId">用户编号</param>
        /// <returns>如果是驻客户端用户返回true，否则返回false</returns>
        public bool checkIsClientUser(int userId)
        {
            bool b = false;
            DataSet ds = dal.GetList(" UserId=" + userId + " and Deleted=0");
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                b = true;
            }
            return b;
        }
        #endregion  成员方法
    }
}