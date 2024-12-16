using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace ESP.Purchase.BusinessLogic
{
    /// <summary>
    ///SharedDataHelper 的摘要说明
    /// </summary>
    public static class SharedDataManager
    {
        private static ESP.Purchase.DataAccess.SharedDataHelper dal = new ESP.Purchase.DataAccess.SharedDataHelper();
        #region 物料类别列表
        public static Entity.V_GetCostType GetModel(int id)
        {
            return DataAccess.SharedDataHelper.GetModel(id);
        }

        public static List<Entity.V_GetCostType> GetTypeList(string strWhere, List<SqlParameter> parms)
        {
            return DataAccess.SharedDataHelper.GetTypeList(strWhere, parms);
        }

        public static List<Entity.V_GetCostType> GetTypeList(string strWhere)
        {
            return DataAccess.SharedDataHelper.GetTypeList(strWhere);
        }

        /// <summary>
        /// 根据项目号Id和成本所属组的Id取得所有PR单中的2级物料类别.
        /// </summary>
        /// <param name="projectId">The project id.</param>
        /// <param name="depId">The dep id.</param>
        /// <returns></returns>
        public static List<int> GetL2TypeByProjectIDDepId(int projectId,int depId)
        {
            return dal.GetL2TypeByProjectIDDepId(projectId, depId);
        }

        #endregion 物料类别列表
    }

    public class V_GetProjectList
    {
        public static Entity.V_GetProjectList GetModel(int id)
        {
            return DataAccess.V_GetProjectList.GetModel(id);
        }

        public static List<Entity.V_GetProjectList> GetList(string strWhere, List<SqlParameter> parms)
        {
            return DataAccess.V_GetProjectList.GetProjectList(strWhere, parms);
        }

        public static List<Entity.V_GetProjectList> GetProjectListByDirector(int currentUserID, string strTerms)
        {
            return DataAccess.V_GetProjectList.GetProjectListByDirector(currentUserID, strTerms);
        }

        public static List<Entity.V_GetProjectList> GetProjectListByManager(int currentUserID, string strTerms)
        {
            return DataAccess.V_GetProjectList.GetProjectListByManager(currentUserID, strTerms);
        }

        public static List<Entity.V_GetProjectList> GetProjectListByDept(int currentUserID, string deptids, string strTerms)
        {
            return DataAccess.V_GetProjectList.GetProjectListByDept(currentUserID,deptids, strTerms);
        }

        public static List<Entity.V_GetProjectList> GetStatusList(string strWhere)
        {
            return DataAccess.V_GetProjectList.GetProjectList(strWhere);
        }

        public static bool MemberInProjectGroup(int pid,int gid,int mid)
        {
            List<SqlParameter> parms = new List<SqlParameter>();
            parms.Add(new SqlParameter("@ProjectId", SqlDbType.Int, 4));
            parms.Add(new SqlParameter("@GroupID", SqlDbType.Int, 4));
            parms.Add(new SqlParameter("@MemberUserID", SqlDbType.Int, 4));
            parms[0].Value = pid;
            parms[1].Value = gid;
            parms[2].Value = mid;
            List<Entity.V_GetProjectList> list = DataAccess.V_GetProjectList.GetProjectListMember(" and ProjectId=@ProjectId and GroupID=@GroupID and MemberUserId=@MemberUserId", parms);

            if (list.Count > 0)
                return true;
            else
                return false;
        }
    }

    public class V_GetProjectGroupList
    {
        public static Entity.V_GetProjectGroupList GetModel(int pid, int gid)
        {
            return DataAccess.V_GetProjectGroupList.GetModel(pid, gid);
        }

        /// <summary>
        /// 根据项目ID获得项目中包含的组列表
        /// </summary>
        /// <param name="pid"></param>
        /// <returns></returns>
        public static List<Entity.V_GetProjectGroupList> GetGroupListByPid(int pid)
        {
            List<SqlParameter> parms = new List<SqlParameter>();
            parms.Add(new SqlParameter("@pid", SqlDbType.Int, 4));
            parms[0].Value = pid;
            return DataAccess.V_GetProjectGroupList.GetList(" and ProjectId=@pid", parms);
        }

        /// <summary>
        /// 根据项目ID获得项目中包含的组列表
        /// </summary>
        /// <param name="pid"></param>
        /// <returns></returns>
        public static List<Entity.V_GetProjectGroupList> GetGroupListByPidMid(int pid,int memberid)
        {
            List<SqlParameter> parms = new List<SqlParameter>();
            parms.Add(new SqlParameter("@pid", SqlDbType.Int, 4));
            parms.Add(new SqlParameter("@MemberUserId", SqlDbType.Int, 4));
            parms[0].Value = pid;
            parms[1].Value = memberid;
            return DataAccess.V_GetProjectGroupList.GetList(" and ProjectId=@pid and MemberUserId=@MemberUserId", parms);
        }

        public static List<Entity.V_GetProjectGroupList> GetList(string strWhere, List<SqlParameter> parms)
        {
            return DataAccess.V_GetProjectGroupList.GetList(strWhere, parms);
        }


        public static List<Entity.V_GetProjectGroupList> GetStatusList(string strWhere)
        {
            return DataAccess.V_GetProjectGroupList.GetList(strWhere);
        }
    }

    public class V_GetProjectTypeList
    {
        public static Entity.V_GetProjectTypeList GetModel(int pid, int gid, int tid)
        {
            return DataAccess.V_GetProjectTypeList.GetModel(pid, gid, tid);
        }

        public static List<Entity.V_GetProjectTypeList> GetList(string strWhere, List<SqlParameter> parms)
        {
            return DataAccess.V_GetProjectTypeList.GetList(strWhere, parms);
        }

        public static List<Entity.V_GetProjectTypeList> GetStatusList(string strWhere)
        {
            return DataAccess.V_GetProjectTypeList.GetList(strWhere);
        }
    }
}
