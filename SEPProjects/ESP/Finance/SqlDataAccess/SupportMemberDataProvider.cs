using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using ESP.Finance.Entity;
using System.Collections.Generic;
using ESP.Finance.Utility;
//using Maticsoft.DBUtility;//请先添加引用
namespace ESP.Finance.DataAccess
{
    /// <summary>
    /// 数据访问类SupportMemberDAL。
    /// </summary>
    public class SupportMemberDataProvider : ESP.Finance.IDataAccess.ISupportMemberDataProvider
    {

        #region  成员方法
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int SupportMemberId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from F_SupportMember");
            strSql.Append(" where SupportMemberId=@SupportMemberId ");
            SqlParameter[] parameters = {
					new SqlParameter("@SupportMemberId", SqlDbType.Int,4)};
            parameters[0].Value = SupportMemberId;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }


        //public bool Exists(string term, List<System.Data.SqlClient.SqlParameter> param)
        //{
        //    return Exists(term, param, false);
        //}

        public bool Exists(string term, List<System.Data.SqlClient.SqlParameter> param)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(*)");
            strSql.Append(" FROM F_SupportMember ");
            if (!string.IsNullOrEmpty(term))
            {
                strSql.Append(" where " + term);
            }
            if (param != null && param.Count > 0)
            {
                return DbHelperSQL.Exists(strSql.ToString(), param.ToArray());
            }
            return DbHelperSQL.Exists(term);
        }

        public int Add(ESP.Finance.Entity.SupportMemberInfo model)
        {
            return Add(model, null);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(ESP.Finance.Entity.SupportMemberInfo model, SqlTransaction trans)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into F_SupportMember(");
            strSql.Append("ProjectID,ProjectCode,SupportID,MemberUserID,MemberCode,MemberEmployeeName,MemberUserName,CreateTime,GroupID,GroupName,MemberEmail,MemberPhone,RoleID,RoleName)");
            strSql.Append(" values (");
            strSql.Append("@ProjectID,@ProjectCode,@SupportID,@MemberUserID,@MemberCode,@MemberEmployeeName,@MemberUserName,@CreateTime,@GroupID,@GroupName,@MemberEmail,@MemberPhone,@RoleID,@RoleName)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					//new SqlParameter("@Lastupdatetime", SqlDbType.Timestamp,8),
					new SqlParameter("@ProjectID", SqlDbType.Int,4),
					new SqlParameter("@ProjectCode", SqlDbType.NChar,10),
					new SqlParameter("@SupportID", SqlDbType.Int,4),
					new SqlParameter("@MemberUserID", SqlDbType.Int,4),
					new SqlParameter("@MemberCode", SqlDbType.NVarChar,10),
					new SqlParameter("@MemberEmployeeName", SqlDbType.NVarChar,50),
					new SqlParameter("@MemberUserName", SqlDbType.NVarChar,50),
					new SqlParameter("@CreateTime", SqlDbType.DateTime),
                                        
                    new SqlParameter("@GroupID", SqlDbType.Int,4),
					new SqlParameter("@GroupName", SqlDbType.NVarChar,50),
					new SqlParameter("@MemberEmail", SqlDbType.NVarChar,100),
					new SqlParameter("@MemberPhone", SqlDbType.NVarChar,50),
					new SqlParameter("@RoleID", SqlDbType.Int,4),
					new SqlParameter("@RoleName", SqlDbType.NVarChar,50)
                                        };
            //parameters[0].Value =model.Lastupdatetime;
            parameters[0].Value = model.ProjectID;
            parameters[1].Value = model.ProjectCode;
            parameters[2].Value = model.SupportID;
            parameters[3].Value = model.MemberUserID;
            parameters[4].Value = model.MemberCode;
            parameters[5].Value = model.MemberEmployeeName;
            parameters[6].Value = model.MemberUserName;
            parameters[7].Value = model.CreateTime;
            parameters[8].Value = model.GroupID;
            parameters[9].Value = model.GroupName;
            parameters[10].Value = model.MemberEmail;
            parameters[11].Value = model.MemberPhone;
            parameters[12].Value = model.RoleID;
            parameters[13].Value = model.RoleName;

            object obj = DbHelperSQL.GetSingle(strSql.ToString(), trans, parameters);
            if (obj == null)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(obj);
            }
        }

        public int Update(ESP.Finance.Entity.SupportMemberInfo model)
        {
            return Update(model, null);
        }
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public int Update(ESP.Finance.Entity.SupportMemberInfo model, SqlTransaction trans)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update F_SupportMember set ");
            //strSql.Append("Lastupdatetime=@Lastupdatetime,");
            strSql.Append("ProjectID=@ProjectID,");
            strSql.Append("ProjectCode=@ProjectCode,");
            strSql.Append("SupportID=@SupportID,");
            strSql.Append("MemberUserID=@MemberUserID,");
            strSql.Append("MemberCode=@MemberCode,");
            strSql.Append("MemberEmployeeName=@MemberEmployeeName,");
            strSql.Append("MemberUserName=@MemberUserName,");
            strSql.Append("CreateTime=@CreateTime,");
            strSql.Append("GroupID=@GroupID,");
            strSql.Append("GroupName=@GroupName,");
            strSql.Append("MemberEmail=@MemberEmail,");
            strSql.Append("MemberPhone=@MemberPhone,");
            strSql.Append("RoleID=@RoleID,");
            strSql.Append("RoleName=@RoleName");
            strSql.Append(" where SupportMemberId=@SupportMemberId and @Lastupdatetime >= Lastupdatetime ");
            SqlParameter[] parameters = {
					new SqlParameter("@SupportMemberId", SqlDbType.Int,4),
					
					new SqlParameter("@ProjectID", SqlDbType.Int,4),
					new SqlParameter("@ProjectCode", SqlDbType.NChar,10),
					new SqlParameter("@SupportID", SqlDbType.Int,4),
					new SqlParameter("@MemberUserID", SqlDbType.Int,4),
					new SqlParameter("@MemberCode", SqlDbType.NVarChar,10),
					new SqlParameter("@MemberEmployeeName", SqlDbType.NVarChar,50),
					new SqlParameter("@MemberUserName", SqlDbType.NVarChar,50),
					new SqlParameter("@CreateTime", SqlDbType.DateTime),
                    new SqlParameter("@Lastupdatetime", SqlDbType.Timestamp,8),
                                        
                    new SqlParameter("@GroupID", SqlDbType.Int,4),
					new SqlParameter("@GroupName", SqlDbType.NVarChar,50),
					new SqlParameter("@MemberEmail", SqlDbType.NVarChar,100),
					new SqlParameter("@MemberPhone", SqlDbType.NVarChar,50),
					new SqlParameter("@RoleID", SqlDbType.Int,4),
					new SqlParameter("@RoleName", SqlDbType.NVarChar,50)};
            parameters[0].Value = model.SupportMemberId;
            parameters[1].Value = model.ProjectID;
            parameters[2].Value = model.ProjectCode;
            parameters[3].Value = model.SupportID;
            parameters[4].Value = model.MemberUserID;
            parameters[5].Value = model.MemberCode;
            parameters[6].Value = model.MemberEmployeeName;
            parameters[7].Value = model.MemberUserName;
            parameters[8].Value = model.CreateTime;
            parameters[9].Value = model.Lastupdatetime;
            parameters[10].Value = model.GroupID;
            parameters[11].Value = model.GroupName;
            parameters[12].Value = model.MemberEmail;
            parameters[13].Value = model.MemberPhone;
            parameters[14].Value = model.RoleID;
            parameters[15].Value = model.RoleName;

            return DbHelperSQL.ExecuteSql(strSql.ToString(), trans, parameters);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public int Delete(int SupportMemberId)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete F_SupportMember ");
            strSql.Append(" where SupportMemberId=@SupportMemberId ");
            SqlParameter[] parameters = {
					new SqlParameter("@SupportMemberId", SqlDbType.Int,4)};
            parameters[0].Value = SupportMemberId;

            return DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public ESP.Finance.Entity.SupportMemberInfo GetModelBySupporterMember(int SupporterId, int memberUserId)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 SupportMemberId,Lastupdatetime,ProjectID,ProjectCode,SupportID,MemberUserID,MemberCode,MemberEmployeeName,MemberUserName,CreateTime,GroupID,GroupName,MemberEmail,MemberPhone,RoleID,RoleName from F_SupportMember ");
            strSql.Append(" where SupportID=@SupportID and MemberUserID=@MemberUserID ");
            SqlParameter[] parameters = {
                    new SqlParameter("@SupportID", SqlDbType.Int,4),
					new SqlParameter("@MemberUserID", SqlDbType.Int,4)};
            parameters[0].Value = SupporterId;
            parameters[1].Value = memberUserId;

            return CBO.FillObject<SupportMemberInfo>(DbHelperSQL.Query(strSql.ToString(), parameters));
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public ESP.Finance.Entity.SupportMemberInfo GetModel(int SupportMemberId)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 SupportMemberId,Lastupdatetime,ProjectID,ProjectCode,SupportID,MemberUserID,MemberCode,MemberEmployeeName,MemberUserName,CreateTime,GroupID,GroupName,MemberEmail,MemberPhone,RoleID,RoleName from F_SupportMember ");
            strSql.Append(" where SupportMemberId=@SupportMemberId ");
            SqlParameter[] parameters = {
					new SqlParameter("@SupportMemberId", SqlDbType.Int,4)};
            parameters[0].Value = SupportMemberId;

            return CBO.FillObject<SupportMemberInfo>(DbHelperSQL.Query(strSql.ToString(), parameters));
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public IList<SupportMemberInfo> GetList(string term, List<SqlParameter> param)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select SupportMemberId,Lastupdatetime,ProjectID,ProjectCode,SupportID,MemberUserID,MemberCode,MemberEmployeeName,MemberUserName,CreateTime,GroupID,GroupName,MemberEmail,MemberPhone,RoleID,RoleName ");
            strSql.Append(" FROM F_SupportMember ");
            if (!string.IsNullOrEmpty(term))
            {
                strSql.Append(" where " + term);
            }
            //if (param != null && param.Count > 0)
            //{
            //    SqlParameter[] ps = param.ToArray();
            //    return CBO.FillCollection<F_SupportMember>(DbHelperSQL.ExecuteReader(strSql.ToString(), ps));
            //}
            return CBO.FillCollection<SupportMemberInfo>(DbHelperSQL.Query(strSql.ToString(), param));
        }

        public IList<SupportMemberInfo> GetList(int supportId, SqlTransaction trans)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select SupportMemberId,Lastupdatetime,ProjectID,ProjectCode,SupportID,MemberUserID,MemberCode,MemberEmployeeName,MemberUserName,CreateTime,GroupID,GroupName,MemberEmail,MemberPhone,RoleID,RoleName ");
            strSql.Append(" FROM F_SupportMember where SupportID=" + supportId.ToString());

            return CBO.FillCollection<SupportMemberInfo>(DbHelperSQL.Query(strSql.ToString(), trans));
        }

        public IList<SupportMemberInfo> GetList(string term, List<System.Data.SqlClient.SqlParameter> param, SqlTransaction trans)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select SupportMemberId,Lastupdatetime,ProjectID,ProjectCode,SupportID,MemberUserID,MemberCode,MemberEmployeeName,MemberUserName,CreateTime,GroupID,GroupName,MemberEmail,MemberPhone,RoleID,RoleName ");
            strSql.Append(" FROM F_SupportMember");
            if (!string.IsNullOrEmpty(term))
            {
                strSql.Append(" where " + term);
            }
            if (param == null)
                return CBO.FillCollection<SupportMemberInfo>(DbHelperSQL.Query(strSql.ToString(), trans));
            else
                return CBO.FillCollection<SupportMemberInfo>(DbHelperSQL.Query(strSql.ToString(),trans,param.ToArray()));
        }
        #endregion  成员方法
    }
}

