using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESP.Administrative.Entity;
using ESP.Administrative.DataAccess;
using System.Data;
using System.Data.SqlClient;

namespace ESP.Administrative.BusinessLogic
{
    public class OperationAuditManageManager
    {
        private readonly OperationAuditManageDataProvider dal = new OperationAuditManageDataProvider();
        public OperationAuditManageManager()
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
        public int Add(OperationAuditManageInfo model)
        {
            return dal.Add(model);
        }        

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public void Update(OperationAuditManageInfo model)
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
        public OperationAuditManageInfo GetModel(int ID)
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
        public IList<ESP.Framework.Entity.EmployeeInfo> GetAllList()
        {
            return dal.GetAllList();
        }

        /// <summary>
        /// 获得用户审批信息
        /// </summary>
        /// <param name="Userid">用户编号</param>
        /// <returns>返回审批人信息</returns>
        public OperationAuditManageInfo GetOperationAuditModelByUserID(int Userid)
        {
            return dal.GetOperationAuditModelByUserID(Userid);
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        /// <param name="strWhere">The STR where.</param>
        /// <returns></returns>
        public List<OperationAuditManageInfo> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            List<OperationAuditManageInfo> modelList = new List<OperationAuditManageInfo>();
            int rowsCount = ds.Tables[0].Rows.Count;
            if (rowsCount > 0)
            {
                OperationAuditManageInfo model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new OperationAuditManageInfo();
                    model.PopupData(ds.Tables[0].Rows[n]);
                    modelList.Add(model);
                }
            }
            return modelList;
        }

        /// <summary>
        /// 获得被用户所审批人员的信息
        /// </summary>
        /// <param name="UserID">用户编号</param>
        /// <returns>返回被审批人的信息集合</returns>
        public List<OperationAuditManageInfo> GetUnderlingsInfo(int UserID)
        {
            return dal.GetUnderlingsInfo(UserID);
        }

        /// <summary>
        /// 获得用户所有的下属人员信息
        /// </summary>
        /// <param name="UserId">用户编号</param>
        /// <returns>返回用户所有下属人员信息</returns>
        public List<ESP.Framework.Entity.EmployeeInfo> GetAllSubordinates(int UserId)
        {
            List<ESP.Framework.Entity.EmployeeInfo> list = new List<ESP.Framework.Entity.EmployeeInfo>();
            dal.GetAllSubordinates(UserId, ref list);
            return list;
        }

        /// <summary>
        /// 获得用户所有的下属人员信息
        /// </summary>
        /// <param name="UserId">用户编号</param>
        /// <returns>返回用户所有下属人员信息</returns>
        public List<ESP.Framework.Entity.EmployeeInfo> GetListForSingleOvertimes(int UserId)
        {
            List<ESP.Framework.Entity.EmployeeInfo> list = new List<ESP.Framework.Entity.EmployeeInfo>();
            return dal.GetListForSingleOvertimes(UserId);
        }

        /// <summary>
        /// 获得团队HRAdmin下的所有人员信息
        /// </summary>
        /// <param name="UserID"></param>
        /// <returns></returns>
        public List<ESP.Framework.Entity.EmployeeInfo> GetHRAdminSubordinates(int UserId)
        {
            return dal.GetHRAdminSubordinates(UserId);
        }

        /// <summary>
        /// 获得团队考勤统计员下的所有人员信息
        /// </summary>
        /// <param name="UserID"></param>
        /// <returns></returns>
        public List<ESP.Framework.Entity.EmployeeInfo> GetStatisticianSubordinates(int UserId)
        {
            return dal.GetStatisticianSubordinates(UserId);
        }
        public List<ESP.Framework.Entity.EmployeeInfo> GetCreativeUsers(string deptids)
        {
            return dal.GetCreativeUsers(deptids);
        }

        /// <summary>
        /// 获得各地区的人员信息
        /// </summary>
        /// <param name="areaId">地区编号</param>
        /// <returns>返回该地区的人员信息</returns>
        public List<ESP.Framework.Entity.EmployeeInfo> GetADAdminInfos(int areaId)
        {
            return dal.GetADAdminInfos(areaId);
        }

        
        /// <summary>
        /// 获得数据列表
        /// </summary>
        /// <param name="strWhere">The STR where.</param>
        /// <returns></returns>
        public List<OperationAuditManagerExtendInfo> GetModelListIncludeUserName(string strWhere)
        {
            DataSet ds = dal.GetListIncludeUserName(strWhere);
            List<OperationAuditManagerExtendInfo> modelList = new List<OperationAuditManagerExtendInfo>();
            int rowsCount = ds.Tables[0].Rows.Count;
            if (rowsCount > 0)
            {
                OperationAuditManagerExtendInfo model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new OperationAuditManagerExtendInfo();
                    model.PopupData(ds.Tables[0].Rows[n]);
                    modelList.Add(model);
                }
            }
            return modelList;
        }
        #endregion  成员方法
    }
}