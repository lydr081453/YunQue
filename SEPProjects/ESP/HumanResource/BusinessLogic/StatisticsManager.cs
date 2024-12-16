using System;
using System.Data;
using System.Collections.Generic;
using ESP.HumanResource.Entity;
using ESP.HumanResource.Common;
using System.Data.SqlClient;
using ESP.HumanResource.DataAccess;

namespace ESP.HumanResource.BusinessLogic
{    
    public class StatisticsManager
    {
        public StatisticsManager()
        { }

        private static StatisticsDataProvider dal = new StatisticsDataProvider();

        public static List<StatisticsInfo> getListForGender(string strWhere)
        {
            return dal.getListForGender(strWhere);
        }

        /// <summary>
        /// 获得性别统计
        /// </summary>
        /// <param name="UserInfo">当前用户</param>
        /// <param name="DepartmentID">查询的部门，为null是当前用户下的所有部门</param>
        /// <param name="strWhere">查询条件</param>
        /// <param name="strWhere">是否所有用户</param>
        /// <returns></returns>
        public static List<StatisticsInfo> getListForGender(ESP.Framework.Entity.UserInfo UserInfo, int[] DepartmentID, string strWhere, bool IsAll)
        {
            if (UserInfo != null)
            {
                string empid = "";
                string depid = "";
                string strSql = "";
                //取当先用户的所有部门
                if (!IsAll)
                {
                    List<ESP.HumanResource.Entity.EmployeesInPositionsInfo> empinpos = ESP.HumanResource.BusinessLogic.EmployeesInPositionsManager.GetModelList(" a.userid=" + UserInfo.UserID);
                    
                    for (int i = 0; i < empinpos.Count; i++)
                    {
                        empid += empinpos[i].CompanyID.ToString() + ",";
                    }
                    if (empid.Length > 0)
                    {
                        empid = empid.Substring(0, empid.Length - 1);
                        strSql += string.Format(" and a.level1ID in ({0})", empid);
                    }
                }
            

                if (DepartmentID != null)
                {
                    if (DepartmentID.Length > 0)
                    {
                        foreach (int j in DepartmentID)
                        {
                            depid += j.ToString() + ",";
                        }
                    }
                    if (depid.Length > 0)
                    {
                        depid = depid.Substring(0, depid.Length - 1);
                        strSql += string.Format(" and a.level3ID in ({0})", depid);
                    }
                }

                List<StatisticsInfo> list = dal.getListForGender(strSql);

                return list;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 集团总人数
        /// </summary>
        /// <param name="depId"></param>
        /// <returns></returns>
        public static int getCountByCo(int depId)
        {
            return dal.getCountByCo(depId);
        }

        /// <summary>
        /// 团队总人数
        /// </summary>
        /// <param name="depId"></param>
        /// <returns></returns>
        public static int getCountByGroup(int depId)
        {
            return dal.getCountByGroup(depId);
        }

        /// <summary>
        /// 婚否统计
        /// </summary>
        /// <returns></returns>
        public static List<StatisticsInfo> getListForMarried(string strWhere)
        {
            return dal.getListForMarried(strWhere);
        }

        /// <summary>
        /// 获得婚否统计
        /// </summary>
        /// <param name="UserInfo">当前用户</param>
        /// <param name="DepartmentID">查询的部门，为null是当前用户下的所有部门</param>
        /// <param name="strWhere">查询条件</param>
        /// <param name="strWhere">是否所有用户</param>
        /// <returns></returns>
        public static List<StatisticsInfo> getListForMarried(ESP.Framework.Entity.UserInfo UserInfo, int[] DepartmentID, string strWhere, bool IsAll)
        {
            if (UserInfo != null)
            {
                string empid = "";
                string depid = "";
                string strSql = "";
                //取当先用户的所有部门
                if (!IsAll)
                {
                    List<ESP.HumanResource.Entity.EmployeesInPositionsInfo> empinpos = ESP.HumanResource.BusinessLogic.EmployeesInPositionsManager.GetModelList(" a.userid=" + UserInfo.UserID);

                    for (int i = 0; i < empinpos.Count; i++)
                    {
                        empid += empinpos[i].CompanyID.ToString() + ",";
                    }
                    if (empid.Length > 0)
                    {
                        empid = empid.Substring(0, empid.Length - 1);
                        strSql += string.Format(" and a.level1ID in ({0})", empid);
                    }
                }


                if (DepartmentID != null)
                {
                    if (DepartmentID.Length > 0)
                    {
                        foreach (int j in DepartmentID)
                        {
                            depid += j.ToString() + ",";
                        }
                    }
                    if (depid.Length > 0)
                    {
                        depid = depid.Substring(0, depid.Length - 1);
                        strSql += string.Format(" and a.level3ID in ({0})", depid);
                    }
                }

                List<StatisticsInfo> list = dal.getListForMarried(strSql);

                return list;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 年龄段统计
        /// </summary>
        /// <returns></returns>
        public static List<StatisticsInfo> getListForAge(string strWhere)
        {
            return dal.getListForAge(strWhere);
        }

        /// <summary>
        /// 年龄段统计
        /// </summary>
        /// <param name="UserInfo">当前用户</param>
        /// <param name="DepartmentID">查询的部门，为null是当前用户下的所有部门</param>
        /// <param name="strWhere">查询条件</param>
        /// <param name="strWhere">是否所有用户</param>
        /// <returns></returns>
        public static List<StatisticsInfo> getListForAge(ESP.Framework.Entity.UserInfo UserInfo, int[] DepartmentID, string strWhere, bool IsAll)
        {
            if (UserInfo != null)
            {
                string empid = "";
                string depid = "";
                string strSql = "";
                //取当先用户的所有部门
                if (!IsAll)
                {
                    List<ESP.HumanResource.Entity.EmployeesInPositionsInfo> empinpos = ESP.HumanResource.BusinessLogic.EmployeesInPositionsManager.GetModelList(" a.userid=" + UserInfo.UserID);

                    for (int i = 0; i < empinpos.Count; i++)
                    {
                        empid += empinpos[i].CompanyID.ToString() + ",";
                    }
                    if (empid.Length > 0)
                    {
                        empid = empid.Substring(0, empid.Length - 1);
                        strSql += string.Format(" and a.level1ID in ({0})", empid);
                    }
                }


                if (DepartmentID != null)
                {
                    if (DepartmentID.Length > 0)
                    {
                        foreach (int j in DepartmentID)
                        {
                            depid += j.ToString() + ",";
                        }
                    }
                    if (depid.Length > 0)
                    {
                        depid = depid.Substring(0, depid.Length - 1);
                        strSql += string.Format(" and a.level3ID in ({0})", depid);
                    }
                }

                List<StatisticsInfo> list = dal.getListForAge(strSql);

                return list;
            }
            else
            {
                return null;
            }
        }
    }
}
