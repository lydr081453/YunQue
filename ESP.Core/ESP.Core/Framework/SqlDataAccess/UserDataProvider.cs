using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.Practices.EnterpriseLibrary.Data;
using ESP.Framework.BusinessLogic;
using ESP.Configuration;
using ESP.Framework.DataAccess.Utilities;
using ESP.Framework.Entity;
using ESP.Framework.DataAccess;

namespace ESP.Framework.SqlDataAccess
{
    /// <summary>
    /// 用户信息数据访问类
    /// </summary>
    public class UserDataProvider : IUserDataProvider
    {
        internal static string EncodePassword(string password, byte[] passwordSaltBytes)
        {
            byte[] bytes = Encoding.Unicode.GetBytes(password);
            byte[] dest = new byte[passwordSaltBytes.Length + bytes.Length];
            Buffer.BlockCopy(passwordSaltBytes, 0, dest, 0, passwordSaltBytes.Length);
            Buffer.BlockCopy(bytes, 0, dest, passwordSaltBytes.Length, bytes.Length);
            HashAlgorithm algorithm = HashAlgorithm.Create("SHA1");
            byte[] inArray = algorithm.ComputeHash(dest);
            return Convert.ToBase64String(inArray);
        }

        internal static byte[] GenerateSalt()
        {
            byte[] data = new byte[0x10];
            new RNGCryptoServiceProvider().GetBytes(data);
            return data;
        }

        /// <summary>
        /// 获取指定ID的用户信息
        /// </summary>
        /// <param name="id">用户ID</param>
        /// <param name="isUserOnline">是否更新用户的最后活动时间</param>
        /// <returns>用户信息</returns>
        public ESP.Framework.Entity.UserInfo Get(int id, bool isUserOnline)
        {
            string sql = @"
IF (@IsUserOnline = 1)
BEGIN
    UPDATE sep_Users SET LastActivityDate = GETDATE()
    WHERE [UserID]=@UserID AND ISNULL(IsDeleted,0)=0
END
SELECT * FROM sep_Users
WHERE [UserID]=@UserID
";

            Database db = DatabaseFactory.CreateDatabase(ESP.Configuration.ConfigurationManager.ConnectionStringName);
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "UserID", DbType.Int32, id);
            db.AddInParameter(cmd, "IsUserOnline", DbType.Boolean, isUserOnline);
            return CBO.LoadObject<UserInfo>(db.ExecuteReader(cmd));
        }

        /// <summary>
        /// 获取所有注册用户的信息列表
        /// </summary>
        /// <returns>系统中所有用户的信息列表</returns>
        public IList<UserInfo> GetAll()
        {
            string sql = @"
SELECT * FROM sep_Users
WHERE ISNULL(IsDeleted,0)=0
";

            Database db = DatabaseFactory.CreateDatabase(ESP.Configuration.ConfigurationManager.ConnectionStringName);
            DbCommand cmd = db.GetSqlStringCommand(sql);
            return CBO.LoadList<UserInfo>(db.ExecuteReader(cmd));
        }

        /// <summary>
        /// 更新用户信息
        /// </summary>
        /// <param name="user">新的用户信息</param>
        public void Update(ESP.Framework.Entity.UserInfo user)
        {
            /*
             * CREATE PROCEDURE sep_Users_Update
             * @UserID int
             * ,@Username nvarchar(256)
             * ,@FirstNameCN nvarchar(256)
             * ,@LastNameCN nvarchar(256)
             * ,@FirstNameEN nvarchar(256)
             * ,@LastNameEN nvarchar(256)
             * ,@Email nvarchar(256)
             * ,@Status int
             * ,@IsApproved bit
             * ,@IsLockedOut bit
             * ,@Comment nvarchar(1024)
             * AS
             */
            Database db = DatabaseFactory.CreateDatabase(ESP.Configuration.ConfigurationManager.ConnectionStringName);
            DbCommand cmd = db.GetStoredProcCommand("sep_Users_Update",
                user.UserID,
                user.Username,
                user.FirstNameCN,
                user.LastNameCN,
                user.FirstNameEN,
                user.LastNameEN,
                user.Email,
                user.Status,
                user.IsApproved,
                user.IsLockedOut,
                user.Comment
                );
            db.ExecuteNonQuery(cmd);
            object ret = (int)cmd.Parameters[0].Value;
            if (!int.Equals(ret, 5))
                throw new UnknownSqlException();
        }

        /// <summary>
        /// 删除指定ID的用户
        /// </summary>
        /// <param name="id">要删除的用户ID</param>
        /// <remarks>
        /// 该操作并不执行实际的删除操作，
        /// 仅设置IsDeleted标志字段
        /// </remarks>
        public void Delete(int id)
        {
            string sql = "UPDATE sep_Users SET IsDeleted=1 WHERE UserID=@UserID";
            Database db = DatabaseFactory.CreateDatabase(ESP.Configuration.ConfigurationManager.ConnectionStringName);
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "UserID", DbType.Int32, id);
            db.ExecuteNonQuery(cmd);
        }

        /// <summary>
        /// 获取指定用户名的用户信息
        /// </summary>
        /// <param name="username">用户名</param>
        /// <returns>用户信息</returns>
        public UserInfo Get(string username)
        {
            string sql = @"
SELECT * FROM sep_Users
WHERE LOWER([Username])=LOWER(@Username)
";

            Database db = DatabaseFactory.CreateDatabase(ESP.Configuration.ConfigurationManager.ConnectionStringName);
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "Username", DbType.String, username);
            return CBO.LoadObject<UserInfo>(db.ExecuteReader(cmd));
        }

        private bool GetSecInfo(string username,
            out int userId, out bool isApproved, out bool isLockedOut, out DateTime lastLockoutDate,
            out string password, out string passwordSalt, 
            out int failedPasswordAttemptCount, out DateTime failedPasswordAttemptWindowStart)
        {
            Database db = DatabaseFactory.CreateDatabase(ESP.Configuration.ConfigurationManager.ConnectionStringName);
            DbCommand cmd = db.GetStoredProcCommand("sep_Users_GetSecInfo", username,
                0, 0, 0, NullValues.DateTime, null, null, 0, NullValues.DateTime);
            db.ExecuteNonQuery(cmd);

            int retCode = (int)cmd.Parameters[0].Value;
            if (retCode != 0)
            {
                userId = 0;
                isApproved = false;
                isLockedOut = false;
                lastLockoutDate = NullValues.DateTime;
                password = null;
                passwordSalt = null;
                failedPasswordAttemptCount = 0;
                failedPasswordAttemptWindowStart = NullValues.DateTime;
                return false;
            }
            else
            {
                userId = (int)db.GetParameterValue(cmd, "UserID");
                isApproved = (bool)db.GetParameterValue(cmd, "IsApproved");
                isLockedOut = (bool)db.GetParameterValue(cmd, "IsLockedOut");
                lastLockoutDate = (DateTime)db.GetParameterValue(cmd, "LastLockoutDate");
                password = (string)db.GetParameterValue(cmd, "Password");
                passwordSalt = (string)db.GetParameterValue(cmd, "PasswordSalt");
                failedPasswordAttemptCount = (int)db.GetParameterValue(cmd, "FailedPasswordAttemptCount");
                failedPasswordAttemptWindowStart = (DateTime)db.GetParameterValue(cmd, "FailedPasswordAttemptWindowStart");

                return true;
            }
        }

        private bool CheckPassword(string username, string password, out bool isLockedOut, out string passwordSalt)
        {
            Database db = null;

#if TRIAL
            string trialTest = @"
WITH t AS(
	SELECT TOP (20) * FROM sep_Users 
	WHERE ISNULL(IsDeleted,0)=0
	ORDER BY UserID ASC
)
SELECT COUNT(UserID) FROM  t
WHERE t.Username=@Username
";

            db = DatabaseFactory.CreateDatabase(ESP.Configuration.ConfigurationManager.ConnectionStringName);
            DbCommand cmd = db.GetSqlStringCommand(trialTest);
            db.AddInParameter(cmd, "Username", DbType.String, username);
            int count = (int)db.ExecuteScalar(cmd);

            if (count == 0)
            {
                isLockedOut = false;
                passwordSalt = null;
                return false;
            }
#endif

            int userId;
            bool isApproved;
            DateTime lastLockoutDate;
            string password_database;
            int failedPasswordAttemptCount;
            DateTime failedPasswordAttemptWindowStart;

            if (!GetSecInfo(username, out userId, out isApproved, out isLockedOut, out lastLockoutDate,
                out password_database, out passwordSalt, out failedPasswordAttemptCount, out failedPasswordAttemptWindowStart))
                return false;

            if (!isApproved || isLockedOut)
                return false;

            string encInputPassword = EncodePassword(password, Convert.FromBase64String(passwordSalt));
            bool isValid = encInputPassword == password_database;

#if !TRIAL
            db = DatabaseFactory.CreateDatabase(ESP.Configuration.ConfigurationManager.ConnectionStringName);
#endif
            /* CREATE PROCEDURE sep_Users_UpdateSecInfo
             * @UserName                       nvarchar(256),
             * @IsPasswordCorrect              bit,
             * @MaxInvalidPasswordAttempts     int,
             * @PasswordAttemptWindow          int */
            //Database db = DatabaseFactory.CreateDatabase(ESP.Configuration.ConfigurationManager.ConnectionStringName);
            db.ExecuteNonQuery("sep_Users_UpdateSecInfo", username, isValid,
                ConfigurationManager.MaxInvalidPasswordAttempts, ConfigurationManager.PasswordAttemptWindow, DateTime.UtcNow);

            return isValid;
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
        public bool ValidateUser(string username, string password)
        {
            bool isLockedOut;
            string tmp;
            return CheckPassword(username, password, out isLockedOut, out tmp);
        }


        internal static UserCreateStatus CheckCreateUserParameters(string username, string email, string password)
        {
            UserCreateStatus status = UserCreateStatus.Success;
            if (!string.IsNullOrEmpty(ConfigurationManager.UsernameRegularExpression)
                && !Regex.IsMatch(username, ConfigurationManager.UsernameRegularExpression, RegexOptions.IgnoreCase))
                status |= UserCreateStatus.InvalidUserName;

            if (!string.IsNullOrEmpty(ConfigurationManager.PasswordStrengthRegularExpression)
                && !Regex.IsMatch(password, ConfigurationManager.PasswordStrengthRegularExpression))
                status |= UserCreateStatus.InvalidPassword;

            if (!Regex.IsMatch(email, ConfigurationManager.EmailRegularExpression, RegexOptions.IgnoreCase))
                status |= UserCreateStatus.InvalidEmail;

            return status;
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
        public UserCreateStatus Create(string username, string firstNameCN, string lastNameCN, 
            string firstNameEN, string lastNameEN, string email, bool isApproved, string password, out int newUserID)
        {
            newUserID = 0;

            UserCreateStatus status = CheckCreateUserParameters(username, email, password);
            if (status != UserCreateStatus.Success)
                return status;

            int errorCode = 0;
            int userId = 0;
            try
            {
                byte[] passwordSaltBytes = GenerateSalt();
                string passwordSalt = Convert.ToBase64String(passwordSaltBytes);
                string encPassword = EncodePassword(password, passwordSaltBytes);

                Database db = DatabaseFactory.CreateDatabase(ESP.Configuration.ConfigurationManager.ConnectionStringName);
                DbCommand cmd = db.GetStoredProcCommand("sep_Users_Create",
                    username, firstNameCN, lastNameCN, firstNameEN, lastNameEN, email, 
                    isApproved, password, passwordSalt, 
                    ConfigurationManager.IsUniqueEmailRequired,
                    0);
                db.ExecuteNonQuery(cmd);
                errorCode = (int)cmd.Parameters[0].Value;
                userId = (int)db.GetParameterValue(cmd, "NewUserID");
            }
            catch
            {
                errorCode = -999;
            }

            if (errorCode == 0)
            {
                newUserID = userId;
                return UserCreateStatus.Success;
            }
            if (errorCode == -1)
                return UserCreateStatus.DuplicateUserName;
            else if (errorCode == -2)
                return UserCreateStatus.DuplicateEmail;
            else
                return UserCreateStatus.ProviderError;
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
        public bool ChangePassword(string username, string oldPassword, string newPassword)
        {
            bool isLockedOut;

            if (!string.IsNullOrEmpty(ConfigurationManager.PasswordStrengthRegularExpression)
                && !Regex.IsMatch(newPassword, ConfigurationManager.PasswordStrengthRegularExpression))
                throw new Exception();

            string passwordSalt;
            if (!CheckPassword(username, oldPassword, out isLockedOut, out passwordSalt))
            {
                return false;
            }

            string encPassword = EncodePassword(newPassword, Convert.FromBase64String(passwordSalt));

            Database db = DatabaseFactory.CreateDatabase(ESP.Configuration.ConfigurationManager.ConnectionStringName);
            db.ExecuteNonQuery("sep_Users_SetPassword", username, encPassword, passwordSalt, DateTime.UtcNow);

            return true;
        }

        /// <summary>
        /// 强制修改用户密码，用于管理员操作，或密码重置操作
        /// </summary>
        /// <param name="username">用户名</param>
        /// <param name="password">新的密码</param>
        public void ResetPassword(string username, string password)
        {
            if (password == null)
            {
                byte[] p = GenerateSalt();
                password = Convert.ToBase64String(p).Trim('=');
            }

            byte[] passwordSaltBytes = GenerateSalt();
            string passwordSalt = Convert.ToBase64String(passwordSaltBytes);
            string encPassword = EncodePassword(password, passwordSaltBytes);

            Database db = DatabaseFactory.CreateDatabase(ESP.Configuration.ConfigurationManager.ConnectionStringName);
            db.ExecuteNonQuery("sep_Users_SetPassword", username, encPassword, passwordSalt, DateTime.UtcNow);
        }

        /// <summary>
        /// 设置重置密码操作中的验证码
        /// </summary>
        /// <param name="username">用户名</param>
        /// <param name="code">验证码</param>
        public void SetResetPasswordCode(string username, string code)
        {
            Database db = DatabaseFactory.CreateDatabase(ESP.Configuration.ConfigurationManager.ConnectionStringName);
            DbCommand cmd = db.GetStoredProcCommand("sep_Users_SetResetPasswordCode", username, code);
            db.ExecuteNonQuery(cmd);
        }

        /// <summary>
        /// 获取最后一封重置密码邮件中使用的验证码
        /// </summary>
        /// <param name="username">用户名</param>
        /// <returns>验证码</returns>
        public string GetResetPasswordCode(string username)
        {
            Database db = DatabaseFactory.CreateDatabase(ESP.Configuration.ConfigurationManager.ConnectionStringName);
            DbCommand cmd = db.GetStoredProcCommand("sep_Users_GetResetPasswordCode", username, null);
            db.ExecuteNonQuery(cmd);
            return (string)db.GetParameterValue(cmd, "ResetPasswordCode");
        }

        /// <summary>
        /// 搜索用户
        /// </summary>
        /// <param name="pageSize">分页大小</param>
        /// <param name="pageIndex">要获取的页的索引</param>
        /// <param name="orderBy">排序规则</param>
        /// <param name="where">过滤条件</param>
        /// <param name="paras">参数列表</param>
        /// <returns></returns>
        public IList<UserInfo> Search(int pageSize, int pageIndex, string orderBy, string where, DbDataParameter[] paras)
        {
            if (where == null)
                where = "";
            where = "ISNULL([IsDeleted], 0)=0 AND " + where;

            if (orderBy == null || orderBy.Length == 0)
                orderBy = "UserID ASC";
            //if (where == null || where.Length == 0)
            //    where = "ISNULL([IsDeleted], 0)=0";
            string sql = @"
;WITH t AS(
    SELECT *,
        (ROW_NUMBER() OVER (ORDER BY " + orderBy + @") - 1) AS RowIndex 
    FROM sep_Users
    WHERE ISNULL(IsDeleted,0)=0 AND (" + where + @")
)SELECT * FROM t 
WHERE RowIndex >= @_PageIndex * @_PageSize AND RowIndex < @_PageIndex * @_PageSize + @_PageSize
";
            Database db = DatabaseFactory.CreateDatabase(ESP.Configuration.ConfigurationManager.ConnectionStringName);
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "_PageIndex", DbType.Int32, pageIndex);
            db.AddInParameter(cmd, "_PageSize", DbType.Int32, pageSize);
            if (paras != null)
            {
                foreach (DbDataParameter p in paras)
                {
                    db.AddInParameter(cmd, p.Name, p.DbType, p.Value);
                }
            }

            return CBO.LoadList<UserInfo>(db.ExecuteReader(cmd));
        }

        /// <summary>
        /// 获取最后活动的指定数量的用户
        /// </summary>
        /// <param name="count">结果的数量</param>
        /// <returns>最后活动的用户的列表</returns>
        public IList<UserInfo> GetLatestActivity(int count)
        {
            string sql = @"
SELECT TOP " + count + @" * FROM sep_Users
WHERE ISNULL([IsDeleted], 0)=0 
ORDER BY LastActivityDate DESC
";

            Database db = DatabaseFactory.CreateDatabase(ESP.Configuration.ConfigurationManager.ConnectionStringName);
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "Count", DbType.Int32, count);
            return CBO.LoadList<UserInfo>(db.ExecuteReader(cmd));
        }

        /// <summary>
        /// 获取最后登录过的指定数量的用户
        /// </summary>
        /// <param name="count">结果的数量</param>
        /// <returns>最后登录过的用户的列表</returns>
        public IList<UserInfo> GetLatestSignedIn(int count)
        {
            string sql = @"
            SELECT TOP " + count + @" * FROM sep_Users
            WHERE ISNULL([IsDeleted], 0)=0 
            ORDER BY LastLoginDate DESC
";

            Database db = DatabaseFactory.CreateDatabase(ESP.Configuration.ConfigurationManager.ConnectionStringName);
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "Count", DbType.Int32, count);
            return CBO.LoadList<UserInfo>(db.ExecuteReader(cmd));
        }

        /// <summary>
        /// 获取最后注册的指定数量的用户
        /// </summary>
        /// <param name="count">结果的数量</param>
        /// <returns>最后注册的用户的列表</returns>
        public IList<UserInfo> GetLatestRegistered(int count)
        {
            string sql = @"
            SELECT TOP " + count + @" * FROM sep_Users
            WHERE ISNULL([IsDeleted], 0)=0
            ORDER BY CreatedDate DESC
";

            Database db = DatabaseFactory.CreateDatabase(ESP.Configuration.ConfigurationManager.ConnectionStringName);
            DbCommand cmd = db.GetSqlStringCommand(sql);
            return CBO.LoadList<UserInfo>(db.ExecuteReader(cmd));
        }



        /// <summary>
        /// 锁定用户
        /// </summary>
        public void LockUser(int userId)
        {
            string sql = @"UPDATE sep_Users SET IsLockedOut=1 WHERE UserID=@UserID";

            Database db = DatabaseFactory.CreateDatabase(ESP.Configuration.ConfigurationManager.ConnectionStringName);
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "UserID", DbType.Int32, userId);
            db.ExecuteNonQuery(cmd);

        }

        /// <summary>
        /// 取消锁定用户
        /// </summary>
        public void UnlockUser(int userId)
        {
            string sql = @"UPDATE sep_Users SET IsLockedOut=0 WHERE UserID=@UserID";

            Database db = DatabaseFactory.CreateDatabase(ESP.Configuration.ConfigurationManager.ConnectionStringName);
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "UserID", DbType.Int32, userId);
            db.ExecuteNonQuery(cmd);

        }


        /// <summary>
        /// 获取所有被锁定了的用户
        /// </summary>
        /// <returns>所有被锁定的用户的列表</returns>
        public IList<UserInfo> GetLockedOutUsers()
        {
            string sql = @"
            SELECT * FROM sep_Users
            WHERE ISNULL([IsDeleted], 0)=0 AND ISNULL(IsLockedOut, 0) = 1
            ORDER BY LastLockoutDate DESC
";

            Database db = DatabaseFactory.CreateDatabase(ESP.Configuration.ConfigurationManager.ConnectionStringName);
            DbCommand cmd = db.GetSqlStringCommand(sql);
            return CBO.LoadList<UserInfo>(db.ExecuteReader(cmd));

        }



        /// <summary>
        /// 根据中文名模糊查询用户
        /// </summary>
        /// <param name="nameKeyword">中文名关键字</param>
        /// <returns>用户列表</returns>
        public IList<UserInfo> SearchUsersByChineseName(string nameKeyword)
        {
            string sql = @"
            SELECT * FROM sep_Users
            WHERE ISNULL([IsDeleted], 0)=0 AND (LastNameCN + FirstNameCN) LIKE '%' + @Keyword + '%'
            ORDER BY UserID ASC
";

            Database db = DatabaseFactory.CreateDatabase(ESP.Configuration.ConfigurationManager.ConnectionStringName);
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "Keyword", DbType.String, nameKeyword);
            return CBO.LoadList<UserInfo>(db.ExecuteReader(cmd));
        }

    }
}
