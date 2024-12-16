using System;
using System.Collections.Generic;
using ESP.HumanResource.Entity;
using System.Web;
using System.Data.SqlClient;
using ESP.HumanResource.Common;
using System.Data;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;


namespace ESP.HumanResource.DataAccess
{    
    public class UsersDataProvider
    {
        public UsersDataProvider()
        { }
        #region  成员方法

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(UsersInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into sep_Users(");
            strSql.Append("Status,Password,PasswordSalt,IsApproved,IsLockedOut,LastLoginDate,LastPasswordChangedDate,LastLockoutDate,FailedPasswordAttemptCount,FailedPasswordAttemptWindowStart,Username,Comment,ResetPasswordCode,IsDeleted,FirstNameCN,LastNameCN,FirstNameEN,LastNameEN,Email,CreatedDate,LastActivityDate)");
            strSql.Append(" values (");
            strSql.Append("@Status,@Password,@PasswordSalt,@IsApproved,@IsLockedOut,@LastLoginDate,@LastPasswordChangedDate,@LastLockoutDate,@FailedPasswordAttemptCount,@FailedPasswordAttemptWindowStart,@Username,@Comment,@ResetPasswordCode,@IsDeleted,@FirstNameCN,@LastNameCN,@FirstNameEN,@LastNameEN,@Email,@CreatedDate,@LastActivityDate)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@Status", SqlDbType.Int,4),
					new SqlParameter("@Password", SqlDbType.NVarChar,128),
					new SqlParameter("@PasswordSalt", SqlDbType.NVarChar,128),
					new SqlParameter("@IsApproved", SqlDbType.Bit,1),
					new SqlParameter("@IsLockedOut", SqlDbType.Bit,1),
					new SqlParameter("@LastLoginDate", SqlDbType.DateTime),
					new SqlParameter("@LastPasswordChangedDate", SqlDbType.DateTime),
					new SqlParameter("@LastLockoutDate", SqlDbType.DateTime),
					new SqlParameter("@FailedPasswordAttemptCount", SqlDbType.Int,4),
					new SqlParameter("@FailedPasswordAttemptWindowStart", SqlDbType.DateTime),
					new SqlParameter("@Username", SqlDbType.NVarChar,256),
					new SqlParameter("@Comment", SqlDbType.NVarChar,1024),
					new SqlParameter("@ResetPasswordCode", SqlDbType.NVarChar,128),
					new SqlParameter("@IsDeleted", SqlDbType.Bit,1),
					new SqlParameter("@FirstNameCN", SqlDbType.NVarChar,256),
					new SqlParameter("@LastNameCN", SqlDbType.NVarChar,256),
					new SqlParameter("@FirstNameEN", SqlDbType.NVarChar,256),
					new SqlParameter("@LastNameEN", SqlDbType.NVarChar,256),
					new SqlParameter("@Email", SqlDbType.NVarChar,256),
					new SqlParameter("@CreatedDate", SqlDbType.DateTime),
					new SqlParameter("@LastActivityDate", SqlDbType.DateTime)};
            parameters[0].Value = model.Status;
            parameters[1].Value = model.Password;
            parameters[2].Value = model.PasswordSalt;
            parameters[3].Value = model.IsApproved;
            parameters[4].Value = model.IsLockedOut;
            parameters[5].Value = model.LastLoginDate;
            parameters[6].Value = model.LastPasswordChangedDate;
            parameters[7].Value = model.LastLockoutDate;
            parameters[8].Value = model.FailedPasswordAttemptCount;
            parameters[9].Value = model.FailedPasswordAttemptWindowStart;
            parameters[10].Value = model.Username;
            parameters[11].Value = model.Comment;
            parameters[12].Value = model.ResetPasswordCode;
            parameters[13].Value = model.IsDeleted;
            parameters[14].Value = model.FirstNameCN;
            parameters[15].Value = model.LastNameCN;
            parameters[16].Value = model.FirstNameEN;
            parameters[17].Value = model.LastNameEN;
            parameters[18].Value = model.Email;
            parameters[19].Value = model.CreatedDate;
            parameters[20].Value = model.LastActivityDate;

            object obj = DbHelperSQL.GetSingle(strSql.ToString(), parameters);
            if (obj == null)
            {
                return 1;
            }
            else
            {
                return Convert.ToInt32(obj);
            }
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(UsersInfo model, SqlTransaction stran)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into sep_Users(");
            strSql.Append("Status,Password,PasswordSalt,IsApproved,IsLockedOut,LastLoginDate,LastPasswordChangedDate,LastLockoutDate,FailedPasswordAttemptCount,FailedPasswordAttemptWindowStart,Username,Comment,ResetPasswordCode,IsDeleted,FirstNameCN,LastNameCN,FirstNameEN,LastNameEN,Email,CreatedDate,LastActivityDate)");
            strSql.Append(" values (");
            strSql.Append("@Status,@Password,@PasswordSalt,@IsApproved,@IsLockedOut,@LastLoginDate,@LastPasswordChangedDate,@LastLockoutDate,@FailedPasswordAttemptCount,@FailedPasswordAttemptWindowStart,@Username,@Comment,@ResetPasswordCode,@IsDeleted,@FirstNameCN,@LastNameCN,@FirstNameEN,@LastNameEN,@Email,@CreatedDate,@LastActivityDate)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@Status", SqlDbType.Int,4),
					new SqlParameter("@Password", SqlDbType.NVarChar,128),
					new SqlParameter("@PasswordSalt", SqlDbType.NVarChar,128),
					new SqlParameter("@IsApproved", SqlDbType.Bit,1),
					new SqlParameter("@IsLockedOut", SqlDbType.Bit,1),
					new SqlParameter("@LastLoginDate", SqlDbType.DateTime),
					new SqlParameter("@LastPasswordChangedDate", SqlDbType.DateTime),
					new SqlParameter("@LastLockoutDate", SqlDbType.DateTime),
					new SqlParameter("@FailedPasswordAttemptCount", SqlDbType.Int,4),
					new SqlParameter("@FailedPasswordAttemptWindowStart", SqlDbType.DateTime),
					new SqlParameter("@Username", SqlDbType.NVarChar,256),
					new SqlParameter("@Comment", SqlDbType.NVarChar,1024),
					new SqlParameter("@ResetPasswordCode", SqlDbType.NVarChar,128),
					new SqlParameter("@IsDeleted", SqlDbType.Bit,1),
					new SqlParameter("@FirstNameCN", SqlDbType.NVarChar,256),
					new SqlParameter("@LastNameCN", SqlDbType.NVarChar,256),
					new SqlParameter("@FirstNameEN", SqlDbType.NVarChar,256),
					new SqlParameter("@LastNameEN", SqlDbType.NVarChar,256),
					new SqlParameter("@Email", SqlDbType.NVarChar,256),
					new SqlParameter("@CreatedDate", SqlDbType.DateTime),
					new SqlParameter("@LastActivityDate", SqlDbType.DateTime)};
            parameters[0].Value = model.Status;
            parameters[1].Value = model.Password;
            parameters[2].Value = model.PasswordSalt;
            parameters[3].Value = model.IsApproved;
            parameters[4].Value = model.IsLockedOut;
            parameters[5].Value = model.LastLoginDate;
            parameters[6].Value = model.LastPasswordChangedDate;
            parameters[7].Value = model.LastLockoutDate;
            parameters[8].Value = model.FailedPasswordAttemptCount;
            parameters[9].Value = model.FailedPasswordAttemptWindowStart;
            parameters[10].Value = model.Username;
            parameters[11].Value = model.Comment;
            parameters[12].Value = model.ResetPasswordCode;
            parameters[13].Value = model.IsDeleted;
            parameters[14].Value = model.FirstNameCN;
            parameters[15].Value = model.LastNameCN;
            parameters[16].Value = model.FirstNameEN;
            parameters[17].Value = model.LastNameEN;
            parameters[18].Value = model.Email;
            parameters[19].Value = model.CreatedDate;
            parameters[20].Value = model.LastActivityDate;

            object obj = DbHelperSQL.GetSingle(strSql.ToString(), stran.Connection, stran, parameters);
            if (obj == null)
            {
                return 1;
            }
            else
            {
                return Convert.ToInt32(obj);
            }
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public void Update(UsersInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update sep_Users set ");
            strSql.Append("Status=@Status,");
            strSql.Append("Password=@Password,");
            strSql.Append("PasswordSalt=@PasswordSalt,");
            strSql.Append("IsApproved=@IsApproved,");
            strSql.Append("IsLockedOut=@IsLockedOut,");
            strSql.Append("LastLoginDate=@LastLoginDate,");
            strSql.Append("LastPasswordChangedDate=@LastPasswordChangedDate,");
            strSql.Append("LastLockoutDate=@LastLockoutDate,");
            strSql.Append("FailedPasswordAttemptCount=@FailedPasswordAttemptCount,");
            strSql.Append("FailedPasswordAttemptWindowStart=@FailedPasswordAttemptWindowStart,");
            strSql.Append("Username=@Username,");
            strSql.Append("Comment=@Comment,");
            strSql.Append("ResetPasswordCode=@ResetPasswordCode,");
            strSql.Append("IsDeleted=@IsDeleted,");
            strSql.Append("FirstNameCN=@FirstNameCN,");
            strSql.Append("LastNameCN=@LastNameCN,");
            strSql.Append("FirstNameEN=@FirstNameEN,");
            strSql.Append("LastNameEN=@LastNameEN,");
            strSql.Append("Email=@Email,");
            strSql.Append("CreatedDate=@CreatedDate,");
            strSql.Append("LastActivityDate=@LastActivityDate");
            strSql.Append(" where UserID=@UserID ");
            SqlParameter[] parameters = {
					new SqlParameter("@UserID", SqlDbType.Int,4),
					new SqlParameter("@Status", SqlDbType.Int,4),
					new SqlParameter("@Password", SqlDbType.NVarChar,128),
					new SqlParameter("@PasswordSalt", SqlDbType.NVarChar,128),
					new SqlParameter("@IsApproved", SqlDbType.Bit,1),
					new SqlParameter("@IsLockedOut", SqlDbType.Bit,1),
					new SqlParameter("@LastLoginDate", SqlDbType.DateTime),
					new SqlParameter("@LastPasswordChangedDate", SqlDbType.DateTime),
					new SqlParameter("@LastLockoutDate", SqlDbType.DateTime),
					new SqlParameter("@FailedPasswordAttemptCount", SqlDbType.Int,4),
					new SqlParameter("@FailedPasswordAttemptWindowStart", SqlDbType.DateTime),
					new SqlParameter("@Username", SqlDbType.NVarChar,256),
					new SqlParameter("@Comment", SqlDbType.NVarChar,1024),
					new SqlParameter("@ResetPasswordCode", SqlDbType.NVarChar,128),
					new SqlParameter("@IsDeleted", SqlDbType.Bit,1),
					new SqlParameter("@FirstNameCN", SqlDbType.NVarChar,256),
					new SqlParameter("@LastNameCN", SqlDbType.NVarChar,256),
					new SqlParameter("@FirstNameEN", SqlDbType.NVarChar,256),
					new SqlParameter("@LastNameEN", SqlDbType.NVarChar,256),
					new SqlParameter("@Email", SqlDbType.NVarChar,256),
					new SqlParameter("@CreatedDate", SqlDbType.DateTime),
					new SqlParameter("@LastActivityDate", SqlDbType.DateTime)};
            parameters[0].Value = model.UserID;
            parameters[1].Value = model.Status;
            parameters[2].Value = model.Password;
            parameters[3].Value = model.PasswordSalt;
            parameters[4].Value = model.IsApproved;
            parameters[5].Value = model.IsLockedOut;
            parameters[6].Value = model.LastLoginDate;
            parameters[7].Value = model.LastPasswordChangedDate;
            parameters[8].Value = model.LastLockoutDate;
            parameters[9].Value = model.FailedPasswordAttemptCount;
            parameters[10].Value = model.FailedPasswordAttemptWindowStart;
            parameters[11].Value = model.Username;
            parameters[12].Value = model.Comment;
            parameters[13].Value = model.ResetPasswordCode;
            parameters[14].Value = model.IsDeleted;
            parameters[15].Value = model.FirstNameCN;
            parameters[16].Value = model.LastNameCN;
            parameters[17].Value = model.FirstNameEN;
            parameters[18].Value = model.LastNameEN;
            parameters[19].Value = model.Email;
            parameters[20].Value = model.CreatedDate;
            parameters[21].Value = model.LastActivityDate;

            DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public void Update(UsersInfo model, SqlTransaction stran)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("update sep_Users set ");
            strSql.Append("Status=@Status,");
            strSql.Append("Password=@Password,");
            strSql.Append("PasswordSalt=@PasswordSalt,");
            strSql.Append("IsApproved=@IsApproved,");
            strSql.Append("IsLockedOut=@IsLockedOut,");
            strSql.Append("LastLoginDate=@LastLoginDate,");
            strSql.Append("LastPasswordChangedDate=@LastPasswordChangedDate,");
            strSql.Append("LastLockoutDate=@LastLockoutDate,");
            strSql.Append("FailedPasswordAttemptCount=@FailedPasswordAttemptCount,");
            strSql.Append("FailedPasswordAttemptWindowStart=@FailedPasswordAttemptWindowStart,");
            strSql.Append("Username=@Username,");
            strSql.Append("Comment=@Comment,");
            strSql.Append("ResetPasswordCode=@ResetPasswordCode,");
            strSql.Append("IsDeleted=@IsDeleted,");
            strSql.Append("FirstNameCN=@FirstNameCN,");
            strSql.Append("LastNameCN=@LastNameCN,");
            strSql.Append("FirstNameEN=@FirstNameEN,");
            strSql.Append("LastNameEN=@LastNameEN,");
            strSql.Append("Email=@Email,");
            strSql.Append("CreatedDate=@CreatedDate,");
            strSql.Append("LastActivityDate=@LastActivityDate");
            strSql.Append(" where UserID=@UserID ");
            SqlParameter[] parameters = {
					new SqlParameter("@UserID", SqlDbType.Int,4),
					new SqlParameter("@Status", SqlDbType.Int,4),
					new SqlParameter("@Password", SqlDbType.NVarChar,128),
					new SqlParameter("@PasswordSalt", SqlDbType.NVarChar,128),
					new SqlParameter("@IsApproved", SqlDbType.Bit,1),
					new SqlParameter("@IsLockedOut", SqlDbType.Bit,1),
					new SqlParameter("@LastLoginDate", SqlDbType.DateTime),
					new SqlParameter("@LastPasswordChangedDate", SqlDbType.DateTime),
					new SqlParameter("@LastLockoutDate", SqlDbType.DateTime),
					new SqlParameter("@FailedPasswordAttemptCount", SqlDbType.Int,4),
					new SqlParameter("@FailedPasswordAttemptWindowStart", SqlDbType.DateTime),
					new SqlParameter("@Username", SqlDbType.NVarChar,256),
					new SqlParameter("@Comment", SqlDbType.NVarChar,1024),
					new SqlParameter("@ResetPasswordCode", SqlDbType.NVarChar,128),
					new SqlParameter("@IsDeleted", SqlDbType.Bit,1),
					new SqlParameter("@FirstNameCN", SqlDbType.NVarChar,256),
					new SqlParameter("@LastNameCN", SqlDbType.NVarChar,256),
					new SqlParameter("@FirstNameEN", SqlDbType.NVarChar,256),
					new SqlParameter("@LastNameEN", SqlDbType.NVarChar,256),
					new SqlParameter("@Email", SqlDbType.NVarChar,256),
					new SqlParameter("@CreatedDate", SqlDbType.DateTime),
					new SqlParameter("@LastActivityDate", SqlDbType.DateTime)};
            parameters[0].Value = model.UserID;
            parameters[1].Value = model.Status;
            parameters[2].Value = model.Password;
            parameters[3].Value = model.PasswordSalt;
            parameters[4].Value = model.IsApproved;
            parameters[5].Value = model.IsLockedOut;
            parameters[6].Value = DateTime.Now;
            parameters[7].Value = model.LastPasswordChangedDate;
            parameters[8].Value = DateTime.Now;
            parameters[9].Value = model.FailedPasswordAttemptCount;
            parameters[10].Value = DateTime.Now;
            parameters[11].Value = model.Username;
            parameters[12].Value = model.Comment;
            parameters[13].Value = model.ResetPasswordCode;
            parameters[14].Value = model.IsDeleted;
            parameters[15].Value = model.FirstNameCN;
            parameters[16].Value = model.LastNameCN;
            parameters[17].Value = model.FirstNameEN;
            parameters[18].Value = model.LastNameEN;
            parameters[19].Value = model.Email;
            parameters[20].Value = model.CreatedDate;
            parameters[21].Value = model.LastActivityDate;

            DbHelperSQL.ExecuteSql(strSql.ToString(), stran.Connection, stran, parameters);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public void Delete(int UserID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete sep_Users ");
            strSql.Append(" where UserID=@UserID ");
            SqlParameter[] parameters = {
					new SqlParameter("@UserID", SqlDbType.Int,4)};
            parameters[0].Value = UserID;

            DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public void Delete(int UserID, SqlTransaction stran)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete sep_Users ");
            strSql.Append(" where UserID=@UserID ");
            SqlParameter[] parameters = {
					new SqlParameter("@UserID", SqlDbType.Int,4)};
            parameters[0].Value = UserID;

            DbHelperSQL.ExecuteSql(strSql.ToString(), stran.Connection, stran, parameters);
        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public UsersInfo GetModel(int UserID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 UserID,Status,Password,PasswordSalt,IsApproved,IsLockedOut,LastLoginDate,LastPasswordChangedDate,LastLockoutDate,FailedPasswordAttemptCount,FailedPasswordAttemptWindowStart,Username,Comment,ResetPasswordCode,IsDeleted,FirstNameCN,LastNameCN,FirstNameEN,LastNameEN,Email,CreatedDate,LastActivityDate from sep_Users ");
            strSql.Append(" where UserID=@UserID ");
            SqlParameter[] parameters = {
					new SqlParameter("@UserID", SqlDbType.Int,4)};
            parameters[0].Value = UserID;

            UsersInfo model = new UsersInfo();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["UserID"].ToString() != "")
                {
                    model.UserID = int.Parse(ds.Tables[0].Rows[0]["UserID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Status"].ToString() != "")
                {
                    model.Status = int.Parse(ds.Tables[0].Rows[0]["Status"].ToString());
                }
                model.Password = ds.Tables[0].Rows[0]["Password"].ToString();
                model.PasswordSalt = ds.Tables[0].Rows[0]["PasswordSalt"].ToString();
                if (ds.Tables[0].Rows[0]["IsApproved"].ToString() != "")
                {
                    if ((ds.Tables[0].Rows[0]["IsApproved"].ToString() == "1") || (ds.Tables[0].Rows[0]["IsApproved"].ToString().ToLower() == "true"))
                    {
                        model.IsApproved = true;
                    }
                    else
                    {
                        model.IsApproved = false;
                    }
                }
                if (ds.Tables[0].Rows[0]["IsLockedOut"].ToString() != "")
                {
                    if ((ds.Tables[0].Rows[0]["IsLockedOut"].ToString() == "1") || (ds.Tables[0].Rows[0]["IsLockedOut"].ToString().ToLower() == "true"))
                    {
                        model.IsLockedOut = true;
                    }
                    else
                    {
                        model.IsLockedOut = false;
                    }
                }
                if (ds.Tables[0].Rows[0]["LastLoginDate"].ToString() != "")
                {
                    model.LastLoginDate = DateTime.Parse(ds.Tables[0].Rows[0]["LastLoginDate"].ToString());
                }
                if (ds.Tables[0].Rows[0]["LastPasswordChangedDate"].ToString() != "")
                {
                    model.LastPasswordChangedDate = DateTime.Parse(ds.Tables[0].Rows[0]["LastPasswordChangedDate"].ToString());
                }
                if (ds.Tables[0].Rows[0]["LastLockoutDate"].ToString() != "")
                {
                    model.LastLockoutDate = DateTime.Parse(ds.Tables[0].Rows[0]["LastLockoutDate"].ToString());
                }
                if (ds.Tables[0].Rows[0]["FailedPasswordAttemptCount"].ToString() != "")
                {
                    model.FailedPasswordAttemptCount = int.Parse(ds.Tables[0].Rows[0]["FailedPasswordAttemptCount"].ToString());
                }
                if (ds.Tables[0].Rows[0]["FailedPasswordAttemptWindowStart"].ToString() != "")
                {
                    model.FailedPasswordAttemptWindowStart = DateTime.Parse(ds.Tables[0].Rows[0]["FailedPasswordAttemptWindowStart"].ToString());
                }
                model.Username = ds.Tables[0].Rows[0]["Username"].ToString();
                model.Comment = ds.Tables[0].Rows[0]["Comment"].ToString();
                model.ResetPasswordCode = ds.Tables[0].Rows[0]["ResetPasswordCode"].ToString();
                if (ds.Tables[0].Rows[0]["IsDeleted"].ToString() != "")
                {
                    if ((ds.Tables[0].Rows[0]["IsDeleted"].ToString() == "1") || (ds.Tables[0].Rows[0]["IsDeleted"].ToString().ToLower() == "true"))
                    {
                        model.IsDeleted = true;
                    }
                    else
                    {
                        model.IsDeleted = false;
                    }
                }
                model.FirstNameCN = ds.Tables[0].Rows[0]["FirstNameCN"].ToString();
                model.LastNameCN = ds.Tables[0].Rows[0]["LastNameCN"].ToString();
                model.FirstNameEN = ds.Tables[0].Rows[0]["FirstNameEN"].ToString();
                model.LastNameEN = ds.Tables[0].Rows[0]["LastNameEN"].ToString();
                model.Email = ds.Tables[0].Rows[0]["Email"].ToString();
                if (ds.Tables[0].Rows[0]["CreatedDate"].ToString() != "")
                {
                    model.CreatedDate = DateTime.Parse(ds.Tables[0].Rows[0]["CreatedDate"].ToString());
                }
                if (ds.Tables[0].Rows[0]["LastActivityDate"].ToString() != "")
                {
                    model.LastActivityDate = DateTime.Parse(ds.Tables[0].Rows[0]["LastActivityDate"].ToString());
                }
                return model;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select UserID,Status,Password,PasswordSalt,IsApproved,IsLockedOut,LastLoginDate,LastPasswordChangedDate,LastLockoutDate,FailedPasswordAttemptCount,FailedPasswordAttemptWindowStart,Username,Comment,ResetPasswordCode,IsDeleted,FirstNameCN,LastNameCN,FirstNameEN,LastNameEN,Email,CreatedDate,LastActivityDate ");
            strSql.Append(" FROM sep_Users ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 通过团队id获得对应楼层前台人员列表
        /// </summary>
        public DataSet GetListByGroupID(int groupid)
        {
            string strSql = string.Format(@"select d.* from sep_users as d join sep_employees e on d.userid=e.userid where d.userid in (
                                            select c.userid from sep_employeesinpositions as c where c.departmentpositionid = (
                                            select b.description from sep_departmenttypes as b where b.departmenttypeid = (
                                            select a.departmenttypeid from sep_departments as a where a.departmentid = {0}))) and e.status > 0 and e.status!=5 and d.IsDeleted='false'", groupid);
//            string strSql = string.Format(@"select c.* from sep_Users as c where c.userid=(
//                                            select b.Description from sep_departmentTypes as b where b.DepartmentTypeID =(
//                                            select a.DepartmentTypeID from sep_departments as a where a.DepartmentID = {0}))", groupid);
            return DbHelperSQL.Query(strSql);
        }

         /// <summary>
        /// 获得待入职辅助人员列表
        /// </summary>
        public DataSet GetUserList(int companyID,int apply)
        {
            string strSql = string.Format(@"select b.* from sep_users as b join sep_employees e on b.userid=e.userid where b.userid in(
                                            select a.userid from sep_employeesinauxiliaries as a where auxiliaryid in(
                                            select id from sep_auxiliary where companyid={0} and apply={1}) and b.IsDeleted='false') and e.status>0 and e.status!=5", companyID, apply);
            return DbHelperSQL.Query(strSql);
        }

        /// <summary>
        /// 获得总部待入职抄送人员列表
        /// </summary>
        public DataSet GetUserList()
        {
            string strSql = @"select b.* from sep_users as b join sep_employees e on b.userid=e.userid where b.userid in(
                                            select a.userid from sep_employeesinauxiliaries as a where auxiliaryid =5) and e.status!=5
                                            ";
            return DbHelperSQL.Query(strSql);
        }

        /// <summary>
        /// 获得待入职辅助人员列表
        /// </summary>
        public DataSet GetUserList( int apply)
        {
            string strSql = string.Format(@"select b.* from sep_users as b join sep_employees e on b.userid=e.userid where b.userid in(
                                            select a.userid from sep_employeesinauxiliaries as a where auxiliaryid in(
                                            select id from sep_auxiliary where apply={0}) and b.IsDeleted='false') and e.status!=5", apply);
            return DbHelperSQL.Query(strSql);
        }

        /// <summary>
        /// 通过部门id获得对应部门Admin列表
        /// </summary>
        public DataSet GetUserListByDepartmentID(int departmentid)
        {
            string strSql = string.Format(@"select c.* from sep_Users as c join sep_employees e on c.userid=e.userid where c.userid=(
                                            select b.Description from sep_departmentTypes as b where b.DepartmentTypeID =(
                                            select a.DepartmentTypeID from sep_departments as a where a.DepartmentID = {0})) and e.status!=5 and c.IsDeleted='false'", departmentid);
            return DbHelperSQL.Query(strSql);
        }

        /// <summary>
        /// 通过部门id获得对应部门Admin列表
        /// </summary>
        public DataSet GetUsersByDepartmentID(int userid)
        {
            string strSql = string.Format(@"select c.* from sep_Users as c  join sep_employees e on c.userid=e.userid where c.userid={0}  and e.status!=5 and c.IsDeleted='false'", userid);
            return DbHelperSQL.Query(strSql);
        }

        /// <summary>
        /// 通过部门ID获得对应部门Admin的用户ID列表
        /// </summary>
        /// <param name="departmentid"></param>
        /// <returns></returns>
        public DataSet GetUserIDsByDepartmentID(int departmentid)
        {
            string strSql = string.Format(@"select b.Description from sep_departmentTypes as b where b.DepartmentTypeID =(
                                            select a.DepartmentTypeID from sep_departments as a where a.DepartmentID = {0})", departmentid);
            return DbHelperSQL.Query(strSql);

        }

        public long GetDBAutoNumber(string numberType)
        {
            Database db = DatabaseFactory.CreateDatabase(ESP.Configuration.ConfigurationManager.ConnectionStringName);
            DbCommand cmd = db.GetStoredProcCommand("sep_AutoNumbers_Generate", numberType, -1);
            db.ExecuteNonQuery(cmd);
            int errorCode = (int)cmd.Parameters[0].Value;
            long number = (long)db.GetParameterValue(cmd, "Value");
            if (errorCode != 0)
                return -1;
            return number;
        }

        /// <summary>
        /// 入职员工分配普通用户权限
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="conn"></param>
        /// <param name="trans"></param>
        public void AddRoleByUserid(int userid, SqlConnection conn, SqlTransaction trans)
        {
            #region 入职员工分配普通用户权限
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into sep_UsersInRoles(");
            strSql.Append("UserID,RoleID,OwnerType)");
            strSql.Append(" values (");
            strSql.Append("@UserID,@RoleID,@OwnerType)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@UserID", SqlDbType.Int,4),
					new SqlParameter("@RoleID", SqlDbType.Int,4),
					new SqlParameter("@OwnerType", SqlDbType.Int,4)
					};
            parameters[0].Value = userid;
            parameters[1].Value = 129;
            parameters[2].Value = 1;

            object obj = DbHelperSQL.GetSingle(strSql.ToString(), conn, trans, parameters);
            #endregion 
        }
        #endregion  成员方法
    }
}
