
using System;
using System.Collections.Generic;

using System.Text;
using ESP.Framework.DataAccess;
using ESP.Framework.Entity;
using System.Text.RegularExpressions;
using System.Security.Cryptography;
using ESP.Configuration;
using ESP.Framework.DataAccess.Utilities;

namespace ESP.Framework.BusinessLogic
{
    /// <summary>
    /// 用户信息访问控制类
    /// </summary>
    public static class UserManager
    {
        #region Private Members
        private static IUserDataProvider GetProvider()
        {
            return ESP.Configuration.ProviderHelper<IUserDataProvider>.Instance;
        }
        #endregion

        /// <summary>
        /// 获取指定ID的用户信息
        /// </summary>
        /// <param name="id">用户ID</param>
        /// <param name="isUserOnline">是否更新用户的最后活动时间</param>
        /// <returns>用户信息</returns>
        public static ESP.Framework.Entity.UserInfo Get(int id, bool isUserOnline)
        {
            return GetProvider().Get(id, isUserOnline);
        }

        /// <summary>
        /// 获取指定ID的用户信息
        /// </summary>
        /// <param name="id">用户ID</param>
        /// <returns>用户信息</returns>
        public static ESP.Framework.Entity.UserInfo Get(int id)
        {
            return Get(id, false);
        }

        /// <summary>
        /// 获取指定用户名的用户信息
        /// </summary>
        /// <param name="username">用户名</param>
        /// <returns>用户信息</returns>
        public static ESP.Framework.Entity.UserInfo Get(string username)
        {
            if (string.IsNullOrEmpty(username))
                return null;
            return GetProvider().Get(username);
        }

        /// <summary>
        /// 获取当前用户的ID
        /// </summary>
        /// <returns>当前用户的ID，如果当前上下文不存在用户ID则返回0</returns>
        public static int GetCurrentUserID()
        {
            System.Web.HttpContext current = System.Web.HttpContext.Current;
            if (current != null && current.User != null && current.User.Identity != null && current.User.Identity.IsAuthenticated)
            {
                string str = current.User.Identity.Name;
                int userId = 0;
                int.TryParse(str, out userId);
                return userId > 0 ? userId : 0;
            }

            return 0;
        }

        /// <summary>
        /// 获取当前用户的用户信息并更新最后活动时间
        /// </summary>
        /// <returns>当前用户的信息</returns>
        public static ESP.Framework.Entity.UserInfo Get()
        {

            System.Web.HttpContext context = System.Web.HttpContext.Current;
            if (context != null)
            {
                UserInfo user = context.Items["sep_currentUser"] as UserInfo;
                if (user == null)
                {
                    user = Get(GetCurrentUserID(), true);
                    context.Items["sep_currentUser"] = user;
                }
                return user;
            }

            int userId = GetCurrentUserID();
            if (userId > 0)
                return Get(userId, true);
            return null;
        }

        /// <summary>
        /// 获取所有注册用户的信息列表
        /// </summary>
        /// <returns>系统中所有用户的信息列表</returns>
        public static IList<UserInfo> GetAll()
        {
            return GetProvider().GetAll();
        }

        /// <summary>
        /// 获取MemberInfo
        /// 包含UserInfo、EmployeeInfo和EmployeePositionList。
        /// 用于判断数据权限
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public static MemberInfo GetMember(UserInfo user)
        {
            MemberInfo member = new MemberInfo();
            member.UserInfomation = user;
            member.EmployeeInformation = EmployeeManager.Get(user.UserID);
            member.EmployeePositionInfomationList = DepartmentPositionManager.GetEmployeePositions(user.UserID);
            return member;
        }

        /// <summary>
        /// 搜索用户
        /// </summary>
        /// <param name="pageSize">分页大小</param>
        /// <param name="pageIndex">页索引</param>
        /// <param name="orderBy">排序规则</param>
        /// <param name="where">查询条件</param>
        /// <param name="paras">查询条件中使用的参数</param>
        /// <returns>匹配条件的用户的列表</returns>
        public static IList<UserInfo> Search(int pageSize, int pageIndex, string orderBy, string where, DbDataParameter[] paras)
        {
            return GetProvider().Search(pageSize, pageIndex, orderBy, where, paras);
        }

        /// <summary>
        /// 更新用户信息
        /// </summary>
        /// <param name="user">新的用户信息</param>
        public static void Update( ESP.Framework.Entity.UserInfo user)
        {
            GetProvider().Update( user);
        }

        /// <summary>
        /// 删除指定ID的用户
        /// </summary>
        /// <param name="id">要删除的用户ID</param>
        /// <remarks>
        /// 该操作并不执行实际的删除操作，
        /// 仅设置IsDeleted标志字段
        /// </remarks>
        public static void Delete(int id)
        {
            GetProvider().Delete(id);
        }

        /// <summary>
        /// 创建新用户
        /// </summary>
        /// <param name="username">用户名</param>
        /// <param name="firstNameCN">中文名</param>
        /// <param name="lastNameCN">中文姓氏</param>
        /// <param name="firstNameEN">英文名</param>
        /// <param name="lastNameEN">英文姓氏</param>
        /// <param name="email">用户的安全email</param>
        /// <param name="isApproved">新用户是否通过审核</param>
        /// <param name="password">密码</param>
        /// <param name="newUserID">返回新用户的ID</param>
        /// <returns>
        /// 操作错误状态
        /// 如果创建用户成功，返回 UserCreateStatus.Success;
        /// 如果用户名效，返回 UserCreateStatus.InvalidUserName;
        /// 如果密码无效，返回 UserCreateStatus.InvalidPassword;
        /// 如果Email无效，返回 UserCreateStatus.InvalidEmail;
        /// 如果用户名已经存在，返回 UserCreateStatus.DuplicateUserName;
        /// 如果Email已经存在且配置了Email唯一，返回 UserCreateStatus.DuplicateEmail;
        /// 如果发生未知错误，返回 UserCreateStatus.ProviderError;
        /// </returns>
        public static UserCreateStatus Create(string username, string firstNameCN, string lastNameCN, string firstNameEN, string lastNameEN, string email, bool isApproved, string password, out int newUserID)
        {
            return GetProvider().Create(username, firstNameCN, lastNameCN, firstNameEN, lastNameEN, email, isApproved, password, out newUserID);
        }

        /// <summary>
        /// 验证用户名密码是否匹配
        /// </summary>
        /// <param name="username">用户名</param>
        /// <param name="password">密码</param>
        /// <returns>
        /// 如果用户名密码不匹配则返回 UserErrorCodes.UnmatchedUserPassword;
        /// 如果用户已经被锁定，则返回 UserErrorCodes.UserLockedOut;
        /// 否则返回成功 UserErrorCodes.Success
        /// </returns>
        public static bool ValidateUser(string username, string password)
        {
            return GetProvider().ValidateUser(username, password);
        }

        /// <summary>
        /// 变更用户密码
        /// 要检查用户原始密码正确性
        /// 更新用户密码时还要检测username、userid和old password是否全部一致
        /// </summary>
        /// <param name="username">要修改密码的用户名</param>
        /// <param name="oldPassword">旧密码</param>
        /// <param name="newPassword">新密码</param>
        /// <returns>
        /// 如果用户名密码不匹配则返回 UserErrorCodes.UnmatchedUserPassword;
        /// 如果用户已经被锁定，则返回 UserErrorCodes.UserLockedOut;
        /// 否则返回成功 UserErrorCodes.Success
        /// </returns>
        public static bool ChangePassword(string username, string oldPassword, string newPassword)
        {
            return GetProvider().ChangePassword(username, oldPassword, newPassword);
        }

        /// <summary>
        /// 强制修改用户密码，用于管理员操作，或密码重置操作
        /// </summary>
        /// <param name="username">用户名</param>
        /// <param name="password">新的密码</param>
        public static void ResetPassword(string username, string password)
        {
            GetProvider().ResetPassword(username, password);
        }

        /// <summary>
        /// 设置重置密码操作中的验证码
        /// </summary>
        /// <param name="username">用户名</param>
        /// <param name="code">验证码</param>
        public static void SetResetPasswordCode(string username, string code)
        {
            GetProvider().SetResetPasswordCode(username, code);
        }

        /// <summary>
        /// 获取最后一封重置密码邮件中使用的验证码
        /// </summary>
        /// <param name="username">用户名</param>
        /// <returns>验证码</returns>
        public static string GetResetPasswordCode(string username)
        {
            return GetProvider().GetResetPasswordCode(username);
        }

        /// <summary>
        /// 锁定用户
        /// </summary>
        public static void LockUser(int userId)
        {
            GetProvider().LockUser(userId);
        }

        /// <summary>
        /// 取消锁定用户
        /// </summary>
        public static void UnlockUser(int userId)
        {
            GetProvider().UnlockUser(userId);
        }


        /// <summary>
        /// 获取所有被锁定用户
        /// </summary>
        /// <returns>所有被锁定用户的列表</returns>
        public static IList<UserInfo> GetLockedoutUsers()
        {
            return GetProvider().GetLockedOutUsers();
        }

        /// <summary>
        /// 获取最后注册的count名用户
        /// </summary>
        /// <param name="count">要获取的用户数量</param>
        /// <returns>用户列表</returns>
        public static IList<UserInfo> GetLatestRegistered(int count)
        {
            return GetProvider().GetLatestRegistered(count);
        }

        /// <summary>
        /// 获取最后登录过的count名用户
        /// </summary>
        /// <param name="count">要获取的用户数量</param>
        /// <returns>用户列表</returns>
        public static IList<UserInfo> GetLatestSignedIn(int count)
        {
            return GetProvider().GetLatestSignedIn(count);
        }

        /// <summary>
        /// 获取最后活动过的count名用户
        /// </summary>
        /// <param name="count">要获取的用户数量</param>
        /// <returns>用户列表</returns>
        public static IList<UserInfo> GetLatestActivity(int count)
        {
            return GetProvider().GetLatestActivity(count);
        }

        /// <summary>
        /// 根据中文名模糊查询用户
        /// </summary>
        /// <param name="nameKeyword">中文名关键字</param>
        /// <returns>用户列表</returns>
        public static IList<UserInfo> SearchUsersByChineseName(string nameKeyword)
        {
            return GetProvider().SearchUsersByChineseName(nameKeyword);
        }
    }

    /// <summary>
    /// 用户创建结果
    /// </summary>
    [Flags]
    public enum UserCreateStatus : int
    {
        /// <summary>
        /// 成功
        /// </summary>
        Success = 0,

        /// <summary>
        /// 无效的用户名
        /// </summary>
        InvalidUserName = 1,

        /// <summary>
        /// 无效的密码
        /// </summary>
        InvalidPassword = 2,

        /// <summary>
        /// 无效的邮件地址
        /// </summary>
        InvalidEmail = 4,

        /// <summary>
        /// 重复的用户名
        /// </summary>
        DuplicateUserName = 8,

        /// <summary>
        /// 重复的邮件地址
        /// </summary>
        DuplicateEmail = 16,

        /// <summary>
        /// 用户被驳回
        /// </summary>
        UserRejected = 32,

        /// <summary>
        /// 未知的提供程序错误
        /// </summary>
        ProviderError = -1
    }





}
