/*********************************************************
 * 类中文名称：部门职位与人员关联操作类
 * 类详细描述：
 * 
 * 
 * 
 * 
 * 主要制作人：zhouqi
 ********************************************************/
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using ESP.HumanResource.DataAccess;
using ESP.HumanResource.Entity;

namespace ESP.HumanResource.BusinessLogic
{
    public class EmployeesInPositionsManager
    {
        private static readonly EmployeesInPositionsDataProvider dal = new EmployeesInPositionsDataProvider();
        public EmployeesInPositionsManager()
        { }
        #region  成员方法
        /// <summary>
        /// 增加一条部门职位与人员的关联数据
        /// </summary>
        public static bool Add(EmployeesInPositionsInfo model, LogInfo logModel)
        {
            using (SqlConnection conn = new SqlConnection(Common.DbHelperSQL.connectionString))
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();
                try
                {
                    dal.Add(model, trans);
                    LogManager.AddLog(logModel, trans);
                    trans.Commit();
                }
                catch (Exception ex)
                {
                    ESP.Logging.Logger.Add(ex.ToString());
                    trans.Rollback();
                    return false;
                }
            }
            return true;
        }

        public static bool Add(EmployeesInPositionsInfo model, LogInfo logModel, PositionLogInfo pLInfo)
        {
            using (SqlConnection conn = new SqlConnection(Common.DbHelperSQL.connectionString))
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();
                try
                {
                    dal.Add(model, trans);
                    LogManager.AddLog(logModel, trans);
                    if (pLInfo != null)
                        PositionLogManager.Add(pLInfo, trans);
                    trans.Commit();
                }
                catch (Exception ex)
                {
                    ESP.Logging.Logger.Add(ex.ToString());
                    trans.Rollback();
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// 增加一条部门职位与人员的关联数据
        /// </summary>
        public static bool Add(EmployeesInPositionsInfo model, LogInfo logModel, SqlTransaction trans)
        {
            try
            {
                dal.Add(model, trans);
                LogManager.AddLog(logModel, trans);
            }
            catch (Exception ex)
            {
                ESP.Logging.Logger.Add(ex.ToString());
                trans.Rollback();
                return false;
            }
            return true;
        }

        /// <summary>
        /// 更新一条部门职位与人员的关联数据
        /// </summary>
        public static int Update(EmployeesInPositionsInfo model, LogInfo logModel)
        {
            int returnValue = 0;
            using (SqlConnection conn = new SqlConnection(Common.DbHelperSQL.connectionString))
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();
                try
                {
                    returnValue = dal.Update(model, trans);
                    LogManager.AddLog(logModel, trans);
                    trans.Commit();
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    ESP.Logging.Logger.Add(ex.ToString());
                    returnValue = 0;
                }
            }
            return returnValue;
        }
        public static int UpdateByDepartmentID(EmployeesInPositionsInfo model)
        {
            return dal.UpdateByDepartmentID(model);
        }
        /// <summary>
        /// 删除一条部门职位与人员的关联数据
        /// </summary>
        /// <param name="UserID">用户ID</param>
        /// <param name="DepartmentPositionID">职位ID</param>
        /// <param name="DepartmentID">部门ID</param>
        /// <param name="logModel"></param>
        /// <returns></returns>
        public static bool Delete(int UserID, int DepartmentPositionID, int DepartmentID, LogInfo logModel)
        {
            using (SqlConnection conn = new SqlConnection(Common.DbHelperSQL.connectionString))
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();
                try
                {
                    dal.Delete(UserID, DepartmentPositionID, DepartmentID, trans);
                    LogManager.AddLog(logModel, trans);
                    trans.Commit();
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    ESP.Logging.Logger.Add(ex.ToString());
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// 增加一条部门职位与人员的关联数据，不添加日志
        /// </summary>
        public static void Add(EmployeesInPositionsInfo model, SqlTransaction trans)
        {
            dal.Add(model, trans);
        }

        /// <summary>
        /// 更新一条部门职位与人员的关联数据，不添加日志
        /// </summary>
        public static int Update(EmployeesInPositionsInfo model, SqlTransaction trans)
        {
            return dal.Update(model, trans);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public static void Delete(int UserID, int DepartmentPositionID, int DepartmentID, SqlTransaction trans)
        {
            dal.Delete(UserID, DepartmentPositionID, DepartmentID, trans);
        }

        /// <summary>
        /// 通过用户ID、职位ID获得一条部门职位与人员的关联数据
        /// </summary>
        /// <param name="UserID">用户ID</param>
        /// <param name="DepartmentPositionID">职位ID</param>
        /// <returns></returns>
        public static EmployeesInPositionsInfo GetModel(int UserID, int DepartmentPositionID)
        {
            return dal.GetModel(UserID, DepartmentPositionID);
        }
        public static EmployeesInPositionsInfo GetModel(int UserID)
        {
            return dal.GetModel(UserID);
        }

        /// <summary>
        /// 通过用户ID、职位ID和部门ID获得一条部门职位与人员的关联数据
        /// </summary>
        /// <param name="UserID">用户ID</param>
        /// <param name="DepartmentPositionID">职位ID</param>
        /// <param name="DepartmentID">部门ID</param>
        /// <returns></returns>
        public static EmployeesInPositionsInfo GetModel(int UserID, int DepartmentPositionID, int DepartmentID)
        {
            return dal.GetModel(UserID, DepartmentPositionID, DepartmentID);
        }

        /// <summary>
        /// 通过条件获得部门职位与人员的关联数据列表
        /// </summary>
        public static DataSet GetList(string strWhere)
        {
            return dal.GetList(strWhere);
        }

        /// <summary>
        /// 通过团队ID获得部门职位与人员的关联数据列表
        /// </summary>
        /// <param name="GroupID"></param>
        /// <returns></returns>
        public static DataSet GetList(int GroupID)
        {
            return dal.GetList(GroupID);
        }

        /// <summary>
        /// 通过条件获得部门职位与人员的关联数据列表
        /// </summary>
        public static List<EmployeesInPositionsInfo> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            List<EmployeesInPositionsInfo> modelList = new List<EmployeesInPositionsInfo>();
            int rowsCount = ds.Tables[0].Rows.Count;
            if (rowsCount > 0)
            {
                EmployeesInPositionsInfo model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new EmployeesInPositionsInfo();
                    if (ds.Tables[0].Rows[n]["UserID"].ToString() != "")
                    {
                        model.UserID = int.Parse(ds.Tables[0].Rows[n]["UserID"].ToString());
                    }
                    if (ds.Tables[0].Rows[n]["DepartmentPositionID"].ToString() != "")
                    {
                        model.DepartmentPositionID = int.Parse(ds.Tables[0].Rows[n]["DepartmentPositionID"].ToString());
                    }
                    if (ds.Tables[0].Rows[n]["IsManager"].ToString() != "")
                    {
                        if ((ds.Tables[0].Rows[n]["IsManager"].ToString() == "1") || (ds.Tables[0].Rows[n]["IsManager"].ToString().ToLower() == "true"))
                        {
                            model.IsManager = true;
                        }
                        else
                        {
                            model.IsManager = false;
                        }
                    }
                    if (ds.Tables[0].Rows[n]["IsActing"].ToString() != "")
                    {
                        if ((ds.Tables[0].Rows[n]["IsActing"].ToString() == "1") || (ds.Tables[0].Rows[n]["IsActing"].ToString().ToLower() == "true"))
                        {
                            model.IsActing = true;
                        }
                        else
                        {
                            model.IsActing = false;
                        }
                    }
                    if (ds.Tables[0].Rows[n]["RowVersion"].ToString() != "")
                    {
                        model.RowVersion = (Byte[])ds.Tables[0].Rows[n]["RowVersion"];
                    }
                    if (ds.Tables[0].Rows[n]["userName"].ToString() != "")
                    {
                        model.UserName = ds.Tables[0].Rows[n]["userName"].ToString();
                    }
                    if (ds.Tables[0].Rows[n]["departmentid"].ToString() != "")
                    {
                        model.DepartmentID = int.Parse(ds.Tables[0].Rows[n]["departmentid"].ToString());
                    }
                    if (ds.Tables[0].Rows[n]["DepartmentName"].ToString() != "")
                    {
                        model.DepartmentName = ds.Tables[0].Rows[n]["DepartmentName"].ToString();
                    }
                    if (ds.Tables[0].Rows[n]["DepartmentPositionName"].ToString() != "")
                    {
                        model.DepartmentPositionName = ds.Tables[0].Rows[n]["DepartmentPositionName"].ToString();
                    }
                    if (ds.Tables[0].Rows[n]["companyID"].ToString() != "")
                    {
                        model.CompanyID = int.Parse(ds.Tables[0].Rows[n]["companyID"].ToString());
                    }
                    model.CompanyName = ds.Tables[0].Rows[n]["companyName"].ToString();
                    if (ds.Tables[0].Rows[n]["groupID"].ToString() != "")
                    {
                        model.GroupID = int.Parse(ds.Tables[0].Rows[n]["groupID"].ToString());
                    }

                    model.GroupName = ds.Tables[0].Rows[n]["groupName"].ToString();
                    model.WorkCity = ds.Tables[0].Rows[n]["workcity"].ToString();
                    model.WorkCountry = ds.Tables[0].Rows[n]["workcountry"].ToString();
                    model.WorkAddress = ds.Tables[0].Rows[n]["WorkAddress"].ToString();
                    model.UserCode = ds.Tables[0].Rows[n]["Code"].ToString();
                    model.Email = ds.Tables[0].Rows[n]["email"].ToString();
                    model.LevelName = ds.Tables[0].Rows[n]["LevelName"].ToString();
                    modelList.Add(model);
                }
            }
            return modelList;
        }

        /// <summary>
        /// 通过条件获得部门职位与人员的关联数据列表
        /// </summary>
        public static DataSet GetAllList()
        {
            return GetList("");
        }

        /// <summary>
        /// 获得当前用户相同公司下的人员列表
        /// </summary>
        /// <param name="UserInfo">当前用户</param>
        /// <param name="strWhere">查询条件</param>
        /// <returns></returns>
        public static List<EmployeeBaseInfo> GetUserModelList(ESP.Framework.Entity.UserInfo UserInfo, string strWhere)
        {
            if (UserInfo != null)
            {
                //取当先用户的所有部门
                List<ESP.HumanResource.Entity.EmployeesInPositionsInfo> empinpos = ESP.HumanResource.BusinessLogic.EmployeesInPositionsManager.GetModelList(" a.userid=" + UserInfo.UserID);
                string empid = "";
                string strSql = "";
                for (int i = 0; i < empinpos.Count; i++)
                {
                    empid += empinpos[i].CompanyID.ToString() + ",";
                }
                if (empid.Length > 0)
                {
                    empid = empid.Substring(0, empid.Length - 1);
                    strSql += string.Format(" and c.level1ID in ({0})", empid);
                }

                //取出所有人
                List<ESP.HumanResource.Entity.EmployeeBaseInfo> list = EmployeeBaseManager.GetModelList(strWhere);

                //给多部门的人付部门
                for (int i = list.Count - 1; i >= 0; i--)
                {
                    //获得和登陆用户相同公司的人员部门
                    List<ESP.HumanResource.Entity.EmployeesInPositionsInfo> empinposlist = GetModelList(string.Format(" a.userid={0} " + strSql, list[i].UserID));
                    if (empinposlist.Count > 0)
                    {
                        list[i].EmployeesInPositionsList = empinposlist;
                    }
                    else
                    {
                        //移除与当前用户不同公司的人
                        list.RemoveAt(i);
                    }
                }
                return list;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 获得当前用户相同公司下的人员列表
        /// </summary>
        /// <param name="UserInfo">当前用户</param>
        /// <param name="strWhere">查询条件</param>
        /// <returns></returns>
        public static List<EmployeeBaseInfo> GetUserModelAllList(ESP.Framework.Entity.UserInfo UserInfo, string strWhere)
        {
            if (UserInfo != null)
            {
                //取当先用户的所有部门
                //List<ESP.HumanResource.Entity.EmployeesInPositionsInfo> empinpos = ESP.HumanResource.BusinessLogic.EmployeesInPositionsManager.GetModelList(" a.userid=" + UserInfo.UserID);
                //string empid = "";
                //string strSql = "";
                //for (int i = 0; i < empinpos.Count; i++)
                //{
                //    empid += empinpos[i].CompanyID.ToString() + ",";
                //}
                //if (empid.Length > 0)
                //{
                //    empid = empid.Substring(0, empid.Length - 1);
                //    strSql += string.Format(" and c.level1ID in ({0})", empid);
                //}

                //取出所有人
                List<ESP.HumanResource.Entity.EmployeeBaseInfo> list = EmployeeBaseManager.GetModelList(strWhere);

                //给多部门的人付部门
                for (int i = list.Count - 1; i >= 0; i--)
                {
                    //获得和登陆用户相同公司的人员部门
                    List<ESP.HumanResource.Entity.EmployeesInPositionsInfo> empinposlist = GetModelList(string.Format(" a.userid={0} ", list[i].UserID));
                    if (empinposlist.Count > 0)
                    {
                        list[i].EmployeesInPositionsList = empinposlist;
                    }
                    //else
                    //{
                    //    //移除与当前用户不同公司的人
                    //    list.RemoveAt(i);
                    //}
                }
                return list;
            }
            else
            {
                return null;
            }
        }


        public static List<EmployeeBaseInfo> GetWaitEntryTeamHR(ESP.Framework.Entity.UserInfo UserInfo, string strWhere)
        {
            if (UserInfo != null)
            {
                //取当先用户的所有部门
                // 获取用户所负责的团队部门配置信息。
                ESP.Administrative.BusinessLogic.DataCodeManager dataCodeManager = new ESP.Administrative.BusinessLogic.DataCodeManager();
                List<ESP.Administrative.Entity.DataCodeInfo> dataCodeList = dataCodeManager.GetDataCodeByType(UserInfo.UserID.ToString());

                //List<ESP.HumanResource.Entity.EmployeesInPositionsInfo> empinpos = 
                //    ESP.HumanResource.BusinessLogic.EmployeesInPositionsManager.GetModelList(" a.userid=" + UserInfo.UserID);
                string empid = "";
                string strSql = "";
                for (int i = 0; i < dataCodeList.Count; i++)
                {
                    empid += dataCodeList[i].Code + ",";
                }
                if (empid.Length > 0)
                {
                    empid = empid.Substring(0, empid.Length - 1);
                    strSql += string.Format(" and c.level2ID in ({0})", empid);
                }

                //取出所有人
                List<ESP.HumanResource.Entity.EmployeeBaseInfo> list = EmployeeBaseManager.GetModelList(strWhere);

                //给多部门的人付部门
                for (int i = list.Count - 1; i >= 0; i--)
                {
                    //获得和登陆用户相同公司的人员部门
                    List<ESP.HumanResource.Entity.EmployeesInPositionsInfo> empinposlist = GetModelList(string.Format(" a.userid={0} " + strSql, list[i].UserID));
                    if (empinposlist.Count > 0)
                    {
                        list[i].EmployeesInPositionsList = empinposlist;
                    }
                    else
                    {
                        //移除与当前用户不同公司的人
                        list.RemoveAt(i);
                    }
                }
                return list;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 获得所有员工人员列表
        /// </summary> 
        /// <param name="strWhere">查询条件</param>
        /// <returns></returns>
        public static List<EmployeeBaseInfo> GetUserModelList(string strWhere)
        {
            //取出所有人
            List<ESP.HumanResource.Entity.EmployeeBaseInfo> list = EmployeeBaseManager.GetModelList(strWhere);

            //给多部门的人付部门
            for (int i = list.Count - 1; i >= 0; i--)
            {
                //获得和登陆用户相同公司的人员部门
                List<ESP.HumanResource.Entity.EmployeesInPositionsInfo> empinposlist = GetModelList(string.Format(" a.userid={0} ", list[i].UserID));
                if (empinposlist.Count > 0)
                {
                    list[i].EmployeesInPositionsList = empinposlist;
                }
                else
                {
                    //移除与当前用户不同公司的人
                    list.RemoveAt(i);
                }
            }
            return list;
        }

        /// <summary>
        /// 获得待入职人员信息
        /// </summary>
        /// <param name="strWhere">查询条件</param>
        /// <returns>返回带入职人员信息列表</returns>
        public static List<EmployeeBaseInfo> GetWaitEntryUserModelList(int userid, string strWhere)
        {
            //取出所有人
            List<ESP.HumanResource.Entity.EmployeeBaseInfo> list = EmployeeBaseManager.GetWaitEntryModelList(userid, strWhere);

            return list;
        }

        /// <summary>
        /// 获得当前用户相同公司下的人员列表
        /// </summary>
        /// <param name="UserInfo">当前用户</param>
        /// <param name="DepartmentID">查询的部门，为null是当前用户下的所有部门</param>
        /// <param name="strWhere">查询条件</param>
        /// <returns></returns>
        public static List<EmployeeBaseInfo> GetUserModelList(int[] DepartmentID, string strWhere)
        {
            string depid = "";
            string strSql = "";

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
                    strSql += string.Format(" and v.level3ID in ({0})", depid);
                }
            }

            strWhere += strSql;

            //取出所有人
            List<ESP.HumanResource.Entity.EmployeeBaseInfo> list = EmployeeBaseManager.GetModelListForExport(strWhere);

            return list;

        }

        /// <summary>
        /// 通过一组团队ID获得相同团队下的所有人列表
        /// </summary>
        /// <param name="DepartmentID"></param>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        public static List<EmployeeBaseInfo> GetUserModelListByDeparmtnetID(int[] DepartmentID, string strWhere)
        {
            string depid = "";
            string strSql = "";

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
                    strSql += string.Format(" and v.level3ID in ({0})", depid);
                }
            }

            strWhere += strSql;

            //取出所有人
            List<ESP.HumanResource.Entity.EmployeeBaseInfo> list = EmployeeBaseManager.GetModelListForExport(strWhere);


            return list;
        }

        public static List<EmployeeBaseInfo> GetUsersByDeparmtnetID(int[] DepartmentID, string strWhere)
        {
            string depid = "";
            string strSql = "";

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
                    strSql += string.Format(" and h.departmentid in ({0})", depid);
                }
            }

            strWhere = strWhere + strSql;
            //取出所有人
            List<ESP.HumanResource.Entity.EmployeeBaseInfo> list = EmployeeBaseManager.GetModelListForHC(strWhere);


            return list;
        }

        /// <summary>
        /// 通过一组团队ID获得相同团队下的所有人列表
        /// </summary>
        /// <param name="DepartmentID"></param>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        public static DataSet GetUserModelByDeparmtnetID(int[] DepartmentID, string strWhere)
        {
            string depid = "";
            string strSql = "";

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
                    strSql += string.Format(" and c.level3ID in ({0})", depid);
                }
            }

            //取出所有人
            DataSet ds = EmployeeBaseManager.GetList(strWhere);

            //给多部门的人付部门
            for (int n = ds.Tables[0].Rows.Count - 1; n >= 0; n--)
            {
                //获得和登陆用户相同公司的人员部门
                List<ESP.HumanResource.Entity.EmployeesInPositionsInfo> empinposlist = GetModelList(string.Format(" a.userid={0} " + strSql, ds.Tables[0].Rows[n]["UserID"].ToString()));
                if (empinposlist.Count > 0)
                {
                }
                else
                {
                    //移除与当前用户不同公司的人                   
                    ds.Tables[0].Rows.RemoveAt(n);
                }
            }
            return ds;
        }

        /// <summary>
        /// 获得当前用户相同公司下的人员列表
        /// </summary>
        /// <param name="UserInfo">当前用户</param>
        /// <param name="DepartmentID">查询的部门，为null是当前用户下的所有部门</param>
        /// <param name="strWhere">查询条件</param>
        /// <returns></returns>
        public static DataSet GetUserModel(int[] DepartmentID, string strWhere)
        {
            //取当先用户的所有部门
            //List<ESP.HumanResource.Entity.EmployeesInPositionsInfo> empinpos = ESP.HumanResource.BusinessLogic.EmployeesInPositionsManager.GetModelList(" a.userid=" + UserInfo.UserID);
            // string empid = "";
            string depid = "";
            string strSql = "";

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
                    strSql += string.Format(" and v.level3ID in ({0})", depid);
                }
            }

            strWhere += strSql;

            //取出所有人
            DataSet ds = EmployeeBaseManager.GetListForExport(strWhere);

            return ds;

        }

        /// <summary>
        /// 获得用户的团队ID数组
        /// </summary>
        /// <param name="userids">用户ID字符串</param>
        /// <returns></returns>
        public static int[] GetUserGroup(string userids)
        {
            return dal.GetUserGroup(userids);
        }

        public static int[] GetUserDepartmentID(string userids)
        {
            return dal.GetUserDepartmentID(userids);
        }

        /// <summary>
        /// 获得全公司现有的所有职位
        /// </summary>
        /// <returns></returns>
        public static DataSet GetPositionList()
        {
            return dal.GetPositionList();
        }

        /// <summary>
        /// 通过部门id获得本部门的人数
        /// </summary>
        /// <param name="level2id"></param>
        /// <returns></returns>
        public static int GetJobNumberByLevel2Id(int level2id)
        {
            return dal.GetJobNumberByLevel2Id(level2id);
        }

        /// <summary>
        /// 通过公司id获得本公司的人数
        /// </summary>
        /// <param name="level1id"></param>
        /// <returns></returns>
        public static int GetJobNumberByLevel1Id(int level1id)
        {
            return dal.GetJobNumberByLevel1Id(level1id);
        }

        /// <summary>
        /// 通过部门id和职位名字获得该部门、职位下的人数
        /// </summary>
        /// <param name="level2id"></param>
        /// <param name="departmentpositionname"></param>
        /// <returns></returns>
        public static int GetJobNumberBylv2ordp(int level2id, string departmentpositionname)
        {
            return dal.GetJobNumberBylv2ordp(level2id, departmentpositionname);
        }

        /// <summary>
        /// 通过公司id和职位名字获得该公司、职位下的人数
        /// </summary>
        /// <param name="level1id"></param>
        /// <param name="departmentpositionname"></param>
        /// <returns></returns>
        public static int GetJobNumberBylv1ordp(int level1id, string departmentpositionname)
        {
            return dal.GetJobNumberBylv1ordp(level1id, departmentpositionname);
        }
        #endregion  成员方法
    }
}
