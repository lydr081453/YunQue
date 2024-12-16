using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using ESP.Finance.Entity;
using ESP.Finance.Utility;
using System.Collections.Generic;

namespace ESP.Finance.DataAccess
{
    internal class ProjectExpenseDataProvider : ESP.Finance.IDataAccess.IProjectExpenseDataProvider
    {
        public bool Exists(int ProjectExpenseID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from F_ProjectExpense");
            strSql.Append(" where ProjectExpenseID=@ProjectExpenseID ");
            SqlParameter[] parameters = {
					new SqlParameter("@ProjectExpenseID", SqlDbType.Int,4)};
            parameters[0].Value = ProjectExpenseID;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }

        // /// <summary>
        ///// 增加一条数据
        ///// </summary>
        //public int Add(Entity.ProjectExpenseInfo model)
        //{
        //    return Add(model,false);
        //}


        public int Add(Entity.ProjectExpenseInfo model)
        {
            return Add(model, null);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(Entity.ProjectExpenseInfo model,SqlTransaction trans)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into F_ProjectExpense(");
            strSql.Append("ProjectID,ProjectCode,Description,Expense,Remark)");
            strSql.Append(" values (");
            strSql.Append("@ProjectID,@ProjectCode,@Description,@Expense,@Remark)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@ProjectID", SqlDbType.Int,4),
					new SqlParameter("@ProjectCode", SqlDbType.NVarChar,50),
					new SqlParameter("@Description", SqlDbType.NVarChar,500),
					new SqlParameter("@Expense", SqlDbType.Decimal,9),
					new SqlParameter("@Remark", SqlDbType.NVarChar,500)};
            parameters[0].Value =model.ProjectID;
            parameters[1].Value =model.ProjectCode;
            parameters[2].Value =model.Description;
            parameters[3].Value =model.Expense;
            parameters[4].Value =model.Remark;

            object obj = DbHelperSQL.GetSingle(strSql.ToString(),trans, parameters);
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
        public int Update(Entity.ProjectExpenseInfo model)
        {
            return Update(model, false);
        }


        /// <summary>
        /// 更新一条数据
        /// </summary>
        public int Update(Entity.ProjectExpenseInfo model,bool isInTrans)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update F_ProjectExpense set ");
            strSql.Append("ProjectID=@ProjectID,");
            strSql.Append("ProjectCode=@ProjectCode,");
            strSql.Append("Description=@Description,");
            strSql.Append("Expense=@Expense,");
            strSql.Append("Remark=@Remark");
            strSql.Append(" where ProjectExpenseID=@ProjectExpenseID ");
            SqlParameter[] parameters = {
					new SqlParameter("@ProjectExpenseID", SqlDbType.Int,4),
					new SqlParameter("@ProjectID", SqlDbType.Int,4),
					new SqlParameter("@ProjectCode", SqlDbType.NVarChar,50),
					new SqlParameter("@Description", SqlDbType.NVarChar,500),
					new SqlParameter("@Expense", SqlDbType.Decimal,9),
					new SqlParameter("@Remark", SqlDbType.NVarChar,500)};
            parameters[0].Value =model.ProjectExpenseID;
            parameters[1].Value =model.ProjectID;
            parameters[2].Value =model.ProjectCode;
            parameters[3].Value =model.Description;
            parameters[4].Value =model.Expense;
            parameters[5].Value =model.Remark;

            return DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public int Delete(int ProjectExpenseID)
        {
            return Delete(ProjectExpenseID, null);
        }

        public int Delete(int ProjectExpenseID,SqlTransaction trans)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete F_ProjectExpense ");
            strSql.Append(" where ProjectExpenseID=@ProjectExpenseID ");
            SqlParameter[] parameters = {
					new SqlParameter("@ProjectExpenseID", SqlDbType.Int,4)};
            parameters[0].Value = ProjectExpenseID;

            return DbHelperSQL.ExecuteSql(strSql.ToString(),trans, parameters);
        }

        public int Delete(string terms,List<SqlParameter> parms, SqlTransaction trans)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete F_ProjectExpense ");
            strSql.Append(" where " + terms);

            return DbHelperSQL.ExecuteSql(strSql.ToString(), trans, parms.ToArray());
        }

        //public decimal GetTotalExpense(int projectId)
        //{
        //    return GetTotalExpense(projectId, false);
        //}

        public decimal GetTotalExpense(int projectId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select sum(Expense) from F_ProjectExpense");
            strSql.Append(" where ProjectID=@ProjectID ");
            SqlParameter[] parameters = {
                    new SqlParameter("@ProjectID", SqlDbType.Int,4)};
            parameters[0].Value = projectId;

            object res = DbHelperSQL.GetSingle(strSql.ToString(),  parameters);
            if (res != null)
            {
                return Convert.ToDecimal(res);
            }
            return 0;
        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Entity.ProjectExpenseInfo GetModel(int ProjectExpenseID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 ProjectExpenseID,ProjectID,ProjectCode,Description,Expense,Remark from F_ProjectExpense ");
            strSql.Append(" where ProjectExpenseID=@ProjectExpenseID ");
            SqlParameter[] parameters = {
					new SqlParameter("@ProjectExpenseID", SqlDbType.Int,4)};
            parameters[0].Value = ProjectExpenseID;

            return CBO.FillObject<ProjectExpenseInfo>(DbHelperSQL.Query(strSql.ToString(), parameters));
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public IList<ProjectExpenseInfo> GetList(string term, List<SqlParameter> param)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ProjectExpenseID,ProjectID,ProjectCode,Description,Expense,Remark ");
            strSql.Append(" FROM F_ProjectExpense ");
            if (!string.IsNullOrEmpty(term))
            {
                strSql.Append(" where " + term);
            }
            //if (param != null && param.Count > 0)
            //{
            //    SqlParameter[] ps = param.ToArray();

            //    return CBO.FillCollection<F_ProjectExpense>(DbHelperSQL.ExecuteReader(strSql.ToString(), ps));
            //}
            return CBO.FillCollection<ProjectExpenseInfo>(DbHelperSQL.Query(strSql.ToString(),param));
        }
    }
}
