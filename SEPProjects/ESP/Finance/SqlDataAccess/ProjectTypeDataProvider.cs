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
	/// 数据访问类ProjectTypeDAL。
	/// </summary>
    internal class ProjectTypeDataProvider : ESP.Finance.IDataAccess.IProjectTypeDataProvider
	{
		
		#region  成员方法
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int ProjectTypeID)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from F_ProjectType");
			strSql.Append(" where ProjectTypeID=@ProjectTypeID ");
			SqlParameter[] parameters = {
					new SqlParameter("@ProjectTypeID", SqlDbType.Int,4)};
			parameters[0].Value = ProjectTypeID;

			return DbHelperSQL.Exists(strSql.ToString(),parameters);
		}


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(ESP.Finance.Entity.ProjectTypeInfo model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into F_ProjectType(");
            strSql.Append("ProjectTypeName,TypeCode,Description,ParentID,Status,ProjectHeadId,CostRate)");
			strSql.Append(" values (");
            strSql.Append("@ProjectTypeName,@TypeCode,@Description,@ParentID,@Status,@ProjectHeadId,@CostRate)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = {
					new SqlParameter("@ProjectTypeName", SqlDbType.NVarChar,50),
					new SqlParameter("@TypeCode", SqlDbType.NVarChar,50),
					new SqlParameter("@Description", SqlDbType.NVarChar,100),
                                        new SqlParameter("@ParentID",SqlDbType.Int),
                                        new SqlParameter("@Status",SqlDbType.Int),
                                        new SqlParameter("@ProjectHeadId",SqlDbType.Int),
                                        new SqlParameter("@CostRate",SqlDbType.Decimal)};
			parameters[0].Value =model.ProjectTypeName;
			parameters[1].Value =model.TypeCode;
			parameters[2].Value =model.Description;
            parameters[3].Value = model.ParentID;
            parameters[4].Value = 1;
            parameters[5].Value = model.ProjectHeadId;
            parameters[6].Value = model.CostRate;

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
		public int Update(ESP.Finance.Entity.ProjectTypeInfo model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update F_ProjectType set ");
			strSql.Append("ProjectTypeName=@ProjectTypeName,");
			strSql.Append("TypeCode=@TypeCode,");
			strSql.Append("Description=@Description,");
            strSql.Append("ParentID=@ParentID,");
            strSql.Append("Status=@Status,ProjectHeadId=@ProjectHeadId,CostRate=@CostRate");
			strSql.Append(" where ProjectTypeID=@ProjectTypeID ");
			SqlParameter[] parameters = {
					new SqlParameter("@ProjectTypeID", SqlDbType.Int,4),
					new SqlParameter("@ProjectTypeName", SqlDbType.NVarChar,50),
					new SqlParameter("@TypeCode", SqlDbType.NVarChar,50),
					new SqlParameter("@Description", SqlDbType.NVarChar,100),new SqlParameter("@ParentID", SqlDbType.Int,4)
                                        ,new SqlParameter("@Status", SqlDbType.Int,4),
                                        new SqlParameter("@ProjectHeadId",SqlDbType.Int),
                                        new SqlParameter("@CostRate",SqlDbType.Decimal)};
			parameters[0].Value =model.ProjectTypeID;
			parameters[1].Value =model.ProjectTypeName;
			parameters[2].Value =model.TypeCode;
			parameters[3].Value =model.Description;
            parameters[4].Value = model.ParentID;
            parameters[5].Value = model.Status;
            parameters[6].Value = model.ProjectHeadId;
            parameters[7].Value = model.CostRate;

			return DbHelperSQL.ExecuteSql(strSql.ToString(),parameters);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public int Delete(int ProjectTypeID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete F_ProjectType ");
			strSql.Append(" where ProjectTypeID=@ProjectTypeID ");
			SqlParameter[] parameters = {
					new SqlParameter("@ProjectTypeID", SqlDbType.Int,4)};
			parameters[0].Value = ProjectTypeID;

			return DbHelperSQL.ExecuteSql(strSql.ToString(),parameters);
		}


		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public ESP.Finance.Entity.ProjectTypeInfo GetModel(int ProjectTypeID)
		{
			
			StringBuilder strSql=new StringBuilder();
            strSql.Append("select  top 1 * from F_ProjectType ");
			strSql.Append(" where ProjectTypeID=@ProjectTypeID ");
			SqlParameter[] parameters = {
					new SqlParameter("@ProjectTypeID", SqlDbType.Int,4)};
			parameters[0].Value = ProjectTypeID;


            return CBO.FillObject<ProjectTypeInfo>(DbHelperSQL.Query(strSql.ToString(), parameters));

            //ESP.Finance.Entity.ProjectTypeInfo model=new ESP.Finance.Entity.ProjectTypeInfo();
            //DataSet ds=DbHelperSQL.Query(strSql.ToString(),parameters);
            //if(ds.Tables[0].Rows.Count>0)
            //{
            //    if(ds.Tables[0].Rows[0]["ProjectTypeID"].ToString()!="")
            //    {
            //        Entity.ProjectTypeID=int.Parse(ds.Tables[0].Rows[0]["ProjectTypeID"].ToString());
            //    }
            //    Entity.ProjectTypeName=ds.Tables[0].Rows[0]["ProjectTypeName"].ToString();
            //    Entity.TypeCode=ds.Tables[0].Rows[0]["TypeCode"].ToString();
            //    Entity.Description=ds.Tables[0].Rows[0]["Description"].ToString();
            //    return model;
            //}
            //else
            //{
            //    return null;
            //}
		}

		/// <summary>
		/// 获得数据列表
		/// </summary>
        public IList<ProjectTypeInfo> GetList(string term, List<SqlParameter> param)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append(@"select * from (select a.*,b.ProjectTypeName as ParentTypeName FROM F_ProjectType as a
                            left join F_ProjectType  as b on a.parentid=b.ProjectTypeID where a.Status=1) as a");
            if (!string.IsNullOrEmpty(term))
            {
                strSql.Append(" where " + term);
            }
            //if (param != null && param.Count > 0)
            //{
            //    SqlParameter[] ps = param.ToArray();

            //    return CBO.FillCollection<ProjectTypeInfo>(DbHelperSQL.ExecuteReader(strSql.ToString(), ps));
            //}
            return CBO.FillCollection<ProjectTypeInfo>(DbHelperSQL.Query(strSql.ToString(),param));
		}


		#endregion  成员方法
	}
}

