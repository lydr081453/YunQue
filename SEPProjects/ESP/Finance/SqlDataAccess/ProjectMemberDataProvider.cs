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
	/// 数据访问类ProjectMemberDAL。
	/// </summary>
    public class ProjectMemberDataProvider : ESP.Finance.IDataAccess.IProjectMemberDataProvider
	{
		
		#region  成员方法
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int MemberId)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from F_ProjectMember");
			strSql.Append(" where MemberId=@MemberId ");
			SqlParameter[] parameters = {
					new SqlParameter("@MemberId", SqlDbType.Int,4)};
			parameters[0].Value = MemberId;

			return DbHelperSQL.Exists(strSql.ToString(),parameters);
		}

        //public bool Exists(string term, List<System.Data.SqlClient.SqlParameter> param)
        //{
        //    return Exists(term, param, false);
        //}

        public bool Exists(string term, List<System.Data.SqlClient.SqlParameter> param)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(*)");
            strSql.Append(" FROM F_ProjectMember ");
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




        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(ESP.Finance.Entity.ProjectMemberInfo model)
        {
            return Add(model, null);
        }

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(ESP.Finance.Entity.ProjectMemberInfo model,SqlTransaction trans)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into F_ProjectMember(");
            strSql.Append("ProjectId,ProjectCode,MemberUserID,MemberUserName,MemberCode,MemberEmployeeName,CreateTime,GroupID,GroupName,MemberEmail,MemberPhone,RoleID,RoleName)");
			strSql.Append(" values (");
            strSql.Append("@ProjectId,@ProjectCode,@MemberUserID,@MemberUserName,@MemberCode,@MemberEmployeeName,@CreateTime,@GroupID,@GroupName,@MemberEmail,@MemberPhone,@RoleID,@RoleName)");
			strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@ProjectId", SqlDbType.Int,4),
					new SqlParameter("@ProjectCode", SqlDbType.NVarChar,50),
					new SqlParameter("@MemberUserID", SqlDbType.Int,4),
					new SqlParameter("@MemberUserName", SqlDbType.NVarChar,50),
					new SqlParameter("@MemberCode", SqlDbType.NVarChar,50),
					new SqlParameter("@MemberEmployeeName", SqlDbType.NVarChar,50),
					new SqlParameter("@CreateTime", SqlDbType.DateTime),
                                        
                    new SqlParameter("@GroupID", SqlDbType.Int,4),
					new SqlParameter("@GroupName", SqlDbType.NVarChar,50),
					new SqlParameter("@MemberEmail", SqlDbType.NVarChar,100),
					new SqlParameter("@MemberPhone", SqlDbType.NVarChar,50),
					new SqlParameter("@RoleID", SqlDbType.Int,4),
					new SqlParameter("@RoleName", SqlDbType.NVarChar,50)};
					//new SqlParameter("@Lastupdatetime", SqlDbType.Timestamp,8)};
			parameters[0].Value =model.ProjectId;
			parameters[1].Value =model.ProjectCode;
			parameters[2].Value =model.MemberUserID;
			parameters[3].Value =model.MemberUserName;
			parameters[4].Value =model.MemberCode;
			parameters[5].Value =model.MemberEmployeeName;
			parameters[6].Value =model.CreateTime;
			parameters[7].Value =model.GroupID;
            parameters[8].Value =model.GroupName;
            parameters[9].Value =model.MemberEmail;
            parameters[10].Value =model.MemberPhone;
            parameters[11].Value =model.RoleID;
            parameters[12].Value =model.RoleName;

			object obj = DbHelperSQL.GetSingle(strSql.ToString(),parameters);
			if (obj == null)
			{
				return 0;
			}
			else
			{
				return Convert.ToInt32(obj);
			}
		}

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public int Update(ESP.Finance.Entity.ProjectMemberInfo model)
        {
            return Update(model, null);
        }

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public int Update(ESP.Finance.Entity.ProjectMemberInfo model,SqlTransaction trans)
		{
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update F_ProjectMember set ");
            strSql.Append("ProjectId=@ProjectId,");
            strSql.Append("ProjectCode=@ProjectCode,");
            strSql.Append("MemberUserID=@MemberUserID,");
            strSql.Append("MemberUserName=@MemberUserName,");
            strSql.Append("MemberCode=@MemberCode,");
            strSql.Append("MemberEmployeeName=@MemberEmployeeName,");
            strSql.Append("CreateTime=@CreateTime,");
            strSql.Append("GroupID=@GroupID,");
            strSql.Append("GroupName=@GroupName,");
            strSql.Append("MemberEmail=@MemberEmail,");
            strSql.Append("MemberPhone=@MemberPhone,");
            strSql.Append("RoleID=@RoleID,");
            strSql.Append("RoleName=@RoleName");
            strSql.Append(" where MemberId=@MemberId  and Lastupdatetime <= @Lastupdatetime ");
            SqlParameter[] parameters = {
					new SqlParameter("@MemberId", SqlDbType.Int,4),
					new SqlParameter("@ProjectId", SqlDbType.Int,4),
					new SqlParameter("@ProjectCode", SqlDbType.NVarChar,50),
					new SqlParameter("@MemberUserID", SqlDbType.Int,4),
					new SqlParameter("@MemberUserName", SqlDbType.NVarChar,50),
					new SqlParameter("@MemberCode", SqlDbType.NVarChar,50),
					new SqlParameter("@MemberEmployeeName", SqlDbType.NVarChar,50),
					new SqlParameter("@CreateTime", SqlDbType.DateTime),
					new SqlParameter("@Lastupdatetime", SqlDbType.Timestamp,8),
					new SqlParameter("@GroupID", SqlDbType.Int,4),
					new SqlParameter("@GroupName", SqlDbType.NVarChar,50),
					new SqlParameter("@MemberEmail", SqlDbType.NVarChar,100),
					new SqlParameter("@MemberPhone", SqlDbType.NVarChar,50),
					new SqlParameter("@RoleID", SqlDbType.Int,4),
					new SqlParameter("@RoleName", SqlDbType.NVarChar,50)};
            parameters[0].Value =model.MemberId;
            parameters[1].Value =model.ProjectId;
            parameters[2].Value =model.ProjectCode;
            parameters[3].Value =model.MemberUserID;
            parameters[4].Value =model.MemberUserName;
            parameters[5].Value =model.MemberCode;
            parameters[6].Value =model.MemberEmployeeName;
            parameters[7].Value =model.CreateTime;
            parameters[8].Value =model.Lastupdatetime;
            parameters[9].Value =model.GroupID;
            parameters[10].Value =model.GroupName;
            parameters[11].Value =model.MemberEmail;
            parameters[12].Value =model.MemberPhone;
            parameters[13].Value =model.RoleID;
            parameters[14].Value =model.RoleName;
			return DbHelperSQL.ExecuteSql(strSql.ToString(),trans,parameters);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public int Delete(int MemberId)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete F_ProjectMember ");
			strSql.Append(" where MemberId=@MemberId ");
			SqlParameter[] parameters = {
					new SqlParameter("@MemberId", SqlDbType.Int,4)};
			parameters[0].Value = MemberId;

			return DbHelperSQL.ExecuteSql(strSql.ToString(),parameters);
		}


		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public ESP.Finance.Entity.ProjectMemberInfo GetModel(int MemberId)
		{
			
			StringBuilder strSql=new StringBuilder();
            strSql.Append("select  top 1 MemberId,ProjectId,ProjectCode,MemberUserID,MemberUserName,MemberCode,MemberEmployeeName,CreateTime,Lastupdatetime,GroupID,GroupName,MemberEmail,MemberPhone,RoleID,RoleName from F_ProjectMember ");
			strSql.Append(" where MemberId=@MemberId ");
			SqlParameter[] parameters = {
					new SqlParameter("@MemberId", SqlDbType.Int,4)};
			parameters[0].Value = MemberId;
            return CBO.FillObject<ProjectMemberInfo>(DbHelperSQL.Query(strSql.ToString(), parameters));
		}


        /// <summary>
        /// 根据项目号及员工ID得到一个对象实体
        /// </summary>
        public ESP.Finance.Entity.ProjectMemberInfo GetModelByPrjMember(int projectId, int memberUserId)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 MemberId,ProjectId,ProjectCode,MemberUserID,MemberUserName,MemberCode,MemberEmployeeName,CreateTime,Lastupdatetime,GroupID,GroupName,MemberEmail,MemberPhone,RoleID,RoleName from F_ProjectMember ");
            strSql.Append(" where ProjectId=@ProjectId and MemberUserID=@MemberUserID ");
            SqlParameter[] parameters = {
                    new SqlParameter("@ProjectId", SqlDbType.Int,4),
					new SqlParameter("@MemberUserID", SqlDbType.Int,4)};
            parameters[0].Value = projectId;
            parameters[1].Value = memberUserId;
            return CBO.FillObject<ProjectMemberInfo>(DbHelperSQL.Query(strSql.ToString(), parameters));
        }

        /// <summary>
        /// 根据项目号及员工ID得到一个对象实体
        /// </summary>
        public ESP.Finance.Entity.ProjectMemberInfo GetModelByPrjMember(int projectId, int memberUserId,SqlTransaction trans)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 MemberId,ProjectId,ProjectCode,MemberUserID,MemberUserName,MemberCode,MemberEmployeeName,CreateTime,Lastupdatetime,GroupID,GroupName,MemberEmail,MemberPhone,RoleID,RoleName from F_ProjectMember ");
            strSql.Append(" where ProjectId=@ProjectId and MemberUserID=@MemberUserID ");
            SqlParameter[] parameters = {
                    new SqlParameter("@ProjectId", SqlDbType.Int,4),
					new SqlParameter("@MemberUserID", SqlDbType.Int,4)};
            parameters[0].Value = projectId;
            parameters[1].Value = memberUserId;
            return CBO.FillObject<ProjectMemberInfo>(DbHelperSQL.Query(strSql.ToString(),trans, parameters));
        }

        public IList<ESP.Finance.Entity.ProjectMemberInfo> GetListByProject(int projectID, string term, List<System.Data.SqlClient.SqlParameter> param)
        {
            return GetListByProject(projectID, term, param, null);
        }

        public IList<ESP.Finance.Entity.ProjectMemberInfo> GetListByProject(int projectID, string term, List<System.Data.SqlClient.SqlParameter> param,SqlTransaction trans)
        {
            if (string.IsNullOrEmpty(term))
            {
                term = " 1=1 ";
            }
            if (param == null)
            {
                param = new List<SqlParameter>();
            }

            term += " and ProjectId = @ProjectId";
            SqlParameter pm = new SqlParameter("@ProjectId", SqlDbType.Int, 4);
            pm.Value = projectID;

            param.Add(pm);

            return GetList(term, param,trans);
        }

        public IList<ProjectMemberInfo> GetList(string term, List<SqlParameter> param)
        {
            return GetList(term, param, null);
        }
		/// <summary>
		/// 获得数据列表
		/// </summary>
        public IList<ProjectMemberInfo> GetList(string term, List<SqlParameter> param,SqlTransaction trans)
		{
			StringBuilder strSql=new StringBuilder();
            strSql.Append("select MemberId,ProjectId,ProjectCode,MemberUserID,MemberUserName,MemberCode,MemberEmployeeName,CreateTime,Lastupdatetime,GroupID,GroupName,MemberEmail,MemberPhone,RoleID,RoleName ");
			strSql.Append(" FROM F_ProjectMember ");
            if (!string.IsNullOrEmpty(term))
            {
                strSql.Append(" where " + term);
            }
            return CBO.FillCollection<ProjectMemberInfo>(DbHelperSQL.Query(strSql.ToString(), trans, (param == null ? null : param.ToArray())));
		}



		#endregion  成员方法
	}
}

