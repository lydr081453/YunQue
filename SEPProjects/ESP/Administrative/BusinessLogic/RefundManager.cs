using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using ESP.Administrative.Entity;
using ESP.Administrative.DataAccess;
using System.Data.SqlClient;
using ESP.Administrative.Common;

namespace ESP.Administrative.BusinessLogic
{
    /// <summary>
    /// 业务逻辑类RefundManager 的摘要说明。
    /// </summary>
    public class RefundManager
    {
        private readonly RefundDataProvider dal = new RefundDataProvider();
        public RefundManager()
        { }
        #region  成员方法
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(RefundInfo model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public int Update(RefundInfo model)
        {
           return dal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public void Delete(int Id)
        {

            dal.Delete(Id);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public RefundInfo GetModel(int Id)
        {

            return dal.GetModel(Id);
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
        public List<RefundInfo> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            List<RefundInfo> modelList = new List<RefundInfo>();
            if (ds.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    RefundInfo model = new RefundInfo();
                    model.PopupData(ds.Tables[0].Rows[i]);
                    modelList.Add(model);
                }
            }
            return modelList;
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetAllList()
        {
            return GetList("");
        }

        public DataSet GetListByUser(string strWhere, string typevalue)
        {
            int[] depids = null;
            if (!string.IsNullOrEmpty(typevalue) && ESP.HumanResource.Utilities.StringHelper.IsConvertInt(typevalue))
            {
                IList<ESP.Framework.Entity.DepartmentInfo> dlist;
                int selectedDep = int.Parse(typevalue);
                dlist = ESP.Framework.BusinessLogic.DepartmentManager.GetChildrenRecursion(selectedDep);
                if (dlist != null && dlist.Count > 0)
                {
                    depids = new int[dlist.Count];
                    for (int i = 0; i < dlist.Count; i++)
                    {
                        depids[i] = dlist[i].DepartmentID;
                    }
                }
                else
                {
                    depids = new int[] { selectedDep };
                }
            }
            else
            {
                depids = null;
            }

            string depid = "";
            if (depids != null)
            {
                if (depids.Length > 0)
                {
                    foreach (int j in depids)
                    {
                        depid += j.ToString() + ",";
                    }
                }
                if (depid.Length > 0)
                {
                    depid = depid.Substring(0, depid.Length - 1);
                    strWhere += string.Format(" and d.level3ID in ({0})", depid);
                }
            }

            return dal.GetListByUser(strWhere);
        }

        public List<RefundInfo> GetModelList(int UserID, int Type, int Status, int IsDeleted)
        {
            string strwhere = string.Format(" UserId={0} AND type={1} AND status={2} AND isdeleted={3}", UserID, Type, Status, IsDeleted);
            return GetModelList(strwhere);
        }

        /// <summary>
        /// 获得用户所申请的报销数据对象
        /// </summary>
        /// <param name="userID">用户编号</param>
        /// <param name="type">报销类型</param>
        /// <param name="status">报销数据状态</param>     
        /// <returns>返回用户申请的报销数据集合</returns>
        public List<RefundInfo> GetRefundInfos(int userID, RefundType type)
        {
            return dal.GetRefundInfos(userID, type);
        }

        /// <summary>
        /// 获得用户报销信息
        /// </summary>
        /// <param name="userId">用户编号</param>
        /// <param name="type">报销类型</param>
        /// <param name="status">报销状态</param>
        /// <returns>返回笔记本报销信息</returns>
        public RefundInfo GetEnableRefundList(int userId, RefundType type, RefundStatus status)
        {
            return dal.GetEnableRefundList(userId, type, status);
        }
        #endregion  成员方法
    }
}