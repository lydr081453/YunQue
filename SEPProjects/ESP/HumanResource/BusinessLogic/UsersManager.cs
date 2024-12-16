using System;
using System.Data;
using System.Collections.Generic;
using ESP.HumanResource.Entity;
using ESP.HumanResource.Common;
using System.Data.SqlClient;
using ESP.HumanResource.DataAccess;

namespace ESP.HumanResource.BusinessLogic
{
    public class UsersManager
    {
        private static UsersDataProvider dal = new UsersDataProvider();
        public UsersManager()
        { }
        #region  成员方法
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public static int Add(UsersInfo model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public static int Add(UsersInfo model, SqlTransaction stran)
        {
            return dal.Add(model, stran);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public static void Update(UsersInfo model)
        {
            dal.Update(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public static void Update(UsersInfo model, SqlTransaction stran)
        {
            dal.Update(model, stran);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public static void Delete(int UserID)
        {

            dal.Delete(UserID);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public static UsersInfo GetModel(int UserID)
        {

            return dal.GetModel(UserID);
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
        public static List<UsersInfo> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            List<UsersInfo> modelList = new List<UsersInfo>();
            int rowsCount = ds.Tables[0].Rows.Count;
            if (rowsCount > 0)
            {
                UsersInfo model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new UsersInfo();
                    if (ds.Tables[0].Rows[n]["UserID"].ToString() != "")
                    {
                        model.UserID = int.Parse(ds.Tables[0].Rows[n]["UserID"].ToString());
                    }
                    if (ds.Tables[0].Rows[n]["Status"].ToString() != "")
                    {
                        model.Status = int.Parse(ds.Tables[0].Rows[n]["Status"].ToString());
                    }
                    model.Password = ds.Tables[0].Rows[n]["Password"].ToString();
                    model.PasswordSalt = ds.Tables[0].Rows[n]["PasswordSalt"].ToString();
                    if (ds.Tables[0].Rows[n]["IsApproved"].ToString() != "")
                    {
                        if ((ds.Tables[0].Rows[n]["IsApproved"].ToString() == "1") || (ds.Tables[0].Rows[n]["IsApproved"].ToString().ToLower() == "true"))
                        {
                            model.IsApproved = true;
                        }
                        else
                        {
                            model.IsApproved = false;
                        }
                    }
                    if (ds.Tables[0].Rows[n]["IsLockedOut"].ToString() != "")
                    {
                        if ((ds.Tables[0].Rows[n]["IsLockedOut"].ToString() == "1") || (ds.Tables[0].Rows[n]["IsLockedOut"].ToString().ToLower() == "true"))
                        {
                            model.IsLockedOut = true;
                        }
                        else
                        {
                            model.IsLockedOut = false;
                        }
                    }
                    if (ds.Tables[0].Rows[n]["LastLoginDate"].ToString() != "")
                    {
                        model.LastLoginDate = DateTime.Parse(ds.Tables[0].Rows[n]["LastLoginDate"].ToString());
                    }
                    if (ds.Tables[0].Rows[n]["LastPasswordChangedDate"].ToString() != "")
                    {
                        model.LastPasswordChangedDate = DateTime.Parse(ds.Tables[0].Rows[n]["LastPasswordChangedDate"].ToString());
                    }
                    if (ds.Tables[0].Rows[n]["LastLockoutDate"].ToString() != "")
                    {
                        model.LastLockoutDate = DateTime.Parse(ds.Tables[0].Rows[n]["LastLockoutDate"].ToString());
                    }
                    if (ds.Tables[0].Rows[n]["FailedPasswordAttemptCount"].ToString() != "")
                    {
                        model.FailedPasswordAttemptCount = int.Parse(ds.Tables[0].Rows[n]["FailedPasswordAttemptCount"].ToString());
                    }
                    if (ds.Tables[0].Rows[n]["FailedPasswordAttemptWindowStart"].ToString() != "")
                    {
                        model.FailedPasswordAttemptWindowStart = DateTime.Parse(ds.Tables[0].Rows[n]["FailedPasswordAttemptWindowStart"].ToString());
                    }
                    model.Username = ds.Tables[0].Rows[n]["Username"].ToString();
                    model.Comment = ds.Tables[0].Rows[n]["Comment"].ToString();
                    model.ResetPasswordCode = ds.Tables[0].Rows[n]["ResetPasswordCode"].ToString();
                    if (ds.Tables[0].Rows[n]["IsDeleted"].ToString() != "")
                    {
                        if ((ds.Tables[0].Rows[n]["IsDeleted"].ToString() == "1") || (ds.Tables[0].Rows[n]["IsDeleted"].ToString().ToLower() == "true"))
                        {
                            model.IsDeleted = true;
                        }
                        else
                        {
                            model.IsDeleted = false;
                        }
                    }
                    model.FirstNameCN = ds.Tables[0].Rows[n]["FirstNameCN"].ToString();
                    model.LastNameCN = ds.Tables[0].Rows[n]["LastNameCN"].ToString();
                    model.FirstNameEN = ds.Tables[0].Rows[n]["FirstNameEN"].ToString();
                    model.LastNameEN = ds.Tables[0].Rows[n]["LastNameEN"].ToString();
                    model.Email = ds.Tables[0].Rows[n]["Email"].ToString();
                    if (ds.Tables[0].Rows[n]["CreatedDate"].ToString() != "")
                    {
                        model.CreatedDate = DateTime.Parse(ds.Tables[0].Rows[n]["CreatedDate"].ToString());
                    }
                    if (ds.Tables[0].Rows[n]["LastActivityDate"].ToString() != "")
                    {
                        model.LastActivityDate = DateTime.Parse(ds.Tables[0].Rows[n]["LastActivityDate"].ToString());
                    }
                    modelList.Add(model);
                }
            }
            return modelList;
        }

        /// <summary>
        /// 通过团队id获得对应楼层前台人员列表
        /// </summary>
        public static List<UsersInfo> GetUserListByGroupID(int groupid)
        {
            DataSet ds = dal.GetListByGroupID(groupid);
            List<UsersInfo> modelList = new List<UsersInfo>();
            int rowsCount = ds.Tables[0].Rows.Count;
            if (rowsCount > 0)
            {
                UsersInfo model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new UsersInfo();
                    if (ds.Tables[0].Rows[n]["UserID"].ToString() != "")
                    {
                        model.UserID = int.Parse(ds.Tables[0].Rows[n]["UserID"].ToString());
                    }

                    model.Username = ds.Tables[0].Rows[n]["Username"].ToString();
                    model.FirstNameCN = ds.Tables[0].Rows[n]["FirstNameCN"].ToString();
                    model.LastNameCN = ds.Tables[0].Rows[n]["LastNameCN"].ToString();
                    model.FirstNameEN = ds.Tables[0].Rows[n]["FirstNameEN"].ToString();
                    model.LastNameEN = ds.Tables[0].Rows[n]["LastNameEN"].ToString();
                    model.Email = ds.Tables[0].Rows[n]["Email"].ToString();

                    modelList.Add(model);
                }
            }
            return modelList;
        }

        /// <summary>
        /// 通过部门id获得对应部门Admin列表
        /// </summary>
        public static List<UsersInfo> GetUserListByDepartmentID(int departmentid)
        {
            List<UsersInfo> modelList = new List<UsersInfo>();

            DataSet dsuser = dal.GetUserIDsByDepartmentID(departmentid);
            if (0 < dsuser.Tables[0].Rows.Count)
            {
                string str = dsuser.Tables[0].Rows[0]["Description"].ToString();
                string[] strs = str.Split(',');



                foreach (string st in strs)
                {
                    DataSet ds = dal.GetUsersByDepartmentID(int.Parse(st));
                    int rowsCount = ds.Tables[0].Rows.Count;
                    if (rowsCount > 0)
                    {
                        UsersInfo model;
                        for (int n = 0; n < rowsCount; n++)
                        {
                            model = new UsersInfo();
                            if (ds.Tables[0].Rows[n]["UserID"].ToString() != "")
                            {
                                model.UserID = int.Parse(ds.Tables[0].Rows[n]["UserID"].ToString());
                            }

                            model.Username = ds.Tables[0].Rows[n]["Username"].ToString();
                            model.FirstNameCN = ds.Tables[0].Rows[n]["FirstNameCN"].ToString();
                            model.LastNameCN = ds.Tables[0].Rows[n]["LastNameCN"].ToString();
                            model.FirstNameEN = ds.Tables[0].Rows[n]["FirstNameEN"].ToString();
                            model.LastNameEN = ds.Tables[0].Rows[n]["LastNameEN"].ToString();
                            model.Email = ds.Tables[0].Rows[n]["Email"].ToString();

                            modelList.Add(model);
                        }
                    }

                }

            }
            return modelList;
        }

        /// <summary>
        /// 获得待入职辅助人员列表
        /// </summary>
        /// <param name="companyID">要发送邮件的公司ID</param>
        /// <param name="apply">辅助人员类型</param>
        /// <returns></returns>
        public static List<UsersInfo> GetUserList(int companyID, int apply)
        {
            DataSet ds = dal.GetUserList(companyID, apply);
            List<UsersInfo> modelList = new List<UsersInfo>();
            int rowsCount = ds.Tables[0].Rows.Count;
            if (rowsCount > 0)
            {
                UsersInfo model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new UsersInfo();
                    if (ds.Tables[0].Rows[n]["UserID"].ToString() != "")
                    {
                        model.UserID = int.Parse(ds.Tables[0].Rows[n]["UserID"].ToString());
                    }

                    model.Username = ds.Tables[0].Rows[n]["Username"].ToString();
                    model.FirstNameCN = ds.Tables[0].Rows[n]["FirstNameCN"].ToString();
                    model.LastNameCN = ds.Tables[0].Rows[n]["LastNameCN"].ToString();
                    model.FirstNameEN = ds.Tables[0].Rows[n]["FirstNameEN"].ToString();
                    model.LastNameEN = ds.Tables[0].Rows[n]["LastNameEN"].ToString();
                    model.Email = ds.Tables[0].Rows[n]["Email"].ToString();

                    modelList.Add(model);
                }
            }
            return modelList;
        }

        /// <summary>
        /// 获得总部待入职抄送人员列表
        /// </summary>        
        /// <returns></returns>
        public static List<UsersInfo> GetUserList()
        {
            DataSet ds = dal.GetUserList();
            List<UsersInfo> modelList = new List<UsersInfo>();
            int rowsCount = ds.Tables[0].Rows.Count;
            if (rowsCount > 0)
            {
                UsersInfo model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new UsersInfo();
                    if (ds.Tables[0].Rows[n]["UserID"].ToString() != "")
                    {
                        model.UserID = int.Parse(ds.Tables[0].Rows[n]["UserID"].ToString());
                    }

                    model.Username = ds.Tables[0].Rows[n]["Username"].ToString();
                    model.FirstNameCN = ds.Tables[0].Rows[n]["FirstNameCN"].ToString();
                    model.LastNameCN = ds.Tables[0].Rows[n]["LastNameCN"].ToString();
                    model.FirstNameEN = ds.Tables[0].Rows[n]["FirstNameEN"].ToString();
                    model.LastNameEN = ds.Tables[0].Rows[n]["LastNameEN"].ToString();
                    model.Email = ds.Tables[0].Rows[n]["Email"].ToString();

                    modelList.Add(model);
                }
            }
            return modelList;
        }

        /// <summary>
        /// 获得待入职辅助人员列表
        /// </summary>
        /// <param name="apply">辅助人员类型</param>
        /// <returns></returns>
        public static List<UsersInfo> GetUserList(int apply)
        {
            DataSet ds = dal.GetUserList(apply);
            List<UsersInfo> modelList = new List<UsersInfo>();
            int rowsCount = ds.Tables[0].Rows.Count;
            if (rowsCount > 0)
            {
                UsersInfo model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new UsersInfo();
                    if (ds.Tables[0].Rows[n]["UserID"].ToString() != "")
                    {
                        model.UserID = int.Parse(ds.Tables[0].Rows[n]["UserID"].ToString());
                    }

                    model.Username = ds.Tables[0].Rows[n]["Username"].ToString();
                    model.FirstNameCN = ds.Tables[0].Rows[n]["FirstNameCN"].ToString();
                    model.LastNameCN = ds.Tables[0].Rows[n]["LastNameCN"].ToString();
                    model.FirstNameEN = ds.Tables[0].Rows[n]["FirstNameEN"].ToString();
                    model.LastNameEN = ds.Tables[0].Rows[n]["LastNameEN"].ToString();
                    model.Email = ds.Tables[0].Rows[n]["Email"].ToString();

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

        public static long GetDBAutoNumber(string numberType)
        {
            return dal.GetDBAutoNumber(numberType);
        }

        #endregion  成员方法
    }
}
