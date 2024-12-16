

using System;
using ESP.Framework.Entity;
using ESP.Framework.BusinessLogic;
using System.Collections.Generic;
using ESP.Framework.DataAccess.Utilities;

namespace ESP.Framework.DataAccess
{
    /// <summary>
    /// 用户信息数据访问接口
    /// </summary>
    [ESP.Configuration.Provider]
    public interface IUserDataProvider
    {

        /// <summary>
        /// 获取指定用户名的用户信息
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <returns>用户信息</returns>
        UserInfo Get(string userName);

        /// <summary>
        /// 获取所有注册用户的信息列表
        /// </summary>
        /// <returns>系统中所有用户的信息列表</returns>
        IList<UserInfo> GetAll();

        /// <summary>
        /// 删除指定ID的用户
        /// </summary>
        /// <param name="id">要删除的用户ID</param>
        /// <remarks>
        /// 该操作并不执行实际的删除操作，
        /// 仅设置IsDeleted标志字段
        /// </remarks>
        void Delete(int id);

        /// <summary>
        /// 创建新用户
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <param name="firstNameCN">中文名</param>
        /// <param name="lastNameCN">中文姓氏</param>
        /// <param name="firstNameEN">英文名</param>
        /// <param name="lastNameEN">英文姓氏</param>
        /// <param name="email">用户的安全email</param>
        /// <param name="isApproved">新用户是否通过审核</param>
        /// <param name="password">密码</param>
        /// <param name="newUserId">返回新用户的ID</param>
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
        UserCreateStatus Create(string userName, string firstNameCN, string lastNameCN, string firstNameEN, string lastNameEN,
            string email, bool isApproved, string password, out int newUserId);

        /// <summary>
        /// 更新用户信息
        /// </summary>
        /// <param name="user">新的用户信息</param>
        void Update(UserInfo user);

        /// <summary>
        /// 验证用户名密码是否匹配
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <param name="password">密码</param>
        /// <returns>
        /// 如果用户名密码不匹配则返回 UserErrorCodes.UnmatchedUserPassword;
        /// 如果用户已经被锁定，则返回 UserErrorCodes.UserLockedOut;
        /// 否则返回成功 UserErrorCodes.Success
        /// </returns>
        bool ValidateUser(string userName, string password);

        /// <summary>
        /// 变更用户密码
        /// 要检查用户原始密码正确性
        /// 更新用户密码时还要检测userName、userid和old password是否全部一致
        /// </summary>
        /// <param name="userName">要修改密码的用户名</param>
        /// <param name="oldPassword">旧密码</param>
        /// <param name="newPassword">新密码</param>
        /// <returns>
        /// 如果用户名密码不匹配则返回 UserErrorCodes.UnmatchedUserPassword;
        /// 如果用户已经被锁定，则返回 UserErrorCodes.UserLockedOut;
        /// 否则返回成功 UserErrorCodes.Success
        /// </returns>
        bool ChangePassword(string userName, string oldPassword, string newPassword);

        /// <summary>
        /// 强制修改用户密码，用于管理员操作，或密码重置操作
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <param name="password">新的密码</param>
        void ResetPassword(string userName, string password);

        /// <summary>
        /// 设置重置密码操作中的验证码
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <param name="code">验证码</param>
        void SetResetPasswordCode(string userName, string code);

        /// <summary>
        /// 获取最后一封重置密码邮件中使用的验证码
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <returns>验证码</returns>
        string GetResetPasswordCode(string userName);

        /// <summary>
        /// 获取最后登录过的count名用户
        /// </summary>
        /// <param name="count">要获取的用户数量</param>
        /// <returns>用户列表</returns>
        IList<UserInfo> GetLatestSignedIn(int count);

        /// <summary>
        /// 获取最后活动过的count名用户
        /// </summary>
        /// <param name="count">要获取的用户数量</param>
        /// <returns>用户列表</returns>
        IList<UserInfo> GetLatestActivity(int count);

        /// <summary>
        /// 获取最后注册的count名用户
        /// </summary>
        /// <param name="count">要获取的用户数量</param>
        /// <returns>用户列表</returns>
        IList<UserInfo> GetLatestRegistered(int count);

        /// <summary>
        /// 搜索用户
        /// </summary>
        /// <param name="pageSize">分页大小</param>
        /// <param name="pageIndex">页索引</param>
        /// <param name="orderBy">排序规则</param>
        /// <param name="where">查询条件</param>
        /// <param name="paras">查询条件中使用的参数</param>
        /// <returns>匹配条件的用户的列表</returns>
        IList<UserInfo> Search(int pageSize, int pageIndex, string orderBy, string where, DbDataParameter[] paras);

        /// <summary>
        /// 获取指定ID的用户信息
        /// </summary>
        /// <param name="id">用户ID</param>
        /// <param name="isUserOnline">是否更新用户的最后活动时间</param>
        /// <returns>用户信息</returns>
        UserInfo Get(int id, bool isUserOnline);

        /// <summary>
        /// 锁定用户
        /// </summary>
        void LockUser(int userId);

        /// <summary>
        /// 取消锁定用户
        /// </summary>
        void UnlockUser(int userId);

        /// <summary>
        /// 获取所有被锁定用户
        /// </summary>
        /// <returns>所有被锁定用户的列表</returns>
        IList<UserInfo> GetLockedOutUsers();

        /// <summary>
        /// 根据中文名模糊查询用户
        /// </summary>
        /// <param name="nameKeyword">中文名关键字</param>
        /// <returns>用户列表</returns>
        IList<UserInfo> SearchUsersByChineseName(string nameKeyword);
    }
}
