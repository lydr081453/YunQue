using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using ESP.Finance.Entity;
using ESP.Finance.Utility;
using System.Collections.Generic;

namespace ESP.Finance.DataAccess
{
    internal class ProjectExpenseTmpDataProvider : ESP.Finance.IDataAccess.IProjectExpenseTmpDataProvider
    {
        public bool Exists(int ProjectExpenseID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from F_ProjectExpenseTmp");
            strSql.Append(" where ProjectExpenseID=@ProjectExpenseID ");
            SqlParameter[] parameters = {
					new SqlParameter("@ProjectExpenseID", SqlDbType.Int,4)};
            parameters[0].Value = ProjectExpenseID;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(Entity.ProjectExpenseTmpInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into F_ProjectExpenseTmp(");
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
            parameters[0].Value = model.ProjectID;
            parameters[1].Value = model.ProjectCode;
            parameters[2].Value = model.Description;
            parameters[3].Value = model.Expense;
            parameters[4].Value = model.Remark;

            object obj = DbHelperSQL.GetSingle(strSql.ToString(), parameters);
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
        public int Update(Entity.ProjectExpenseTmpInfo model)
        {
            return Update(model, false);
        }


        /// <summary>
        /// 更新一条数据
        /// </summary>
        public int Update(Entity.ProjectExpenseTmpInfo model, bool isInTrans)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update F_ProjectExpenseTmp set ");
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
            parameters[0].Value = model.ProjectExpenseID;
            parameters[1].Value = model.ProjectID;
            parameters[2].Value = model.ProjectCode;
            parameters[3].Value = model.Description;
            parameters[4].Value = model.Expense;
            parameters[5].Value = model.Remark;

            return DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public int Delete(int ProjectExpenseID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete F_ProjectExpenseTmp ");
            strSql.Append(" where ProjectExpenseID=@ProjectExpenseID ");
            SqlParameter[] parameters = {
					new SqlParameter("@ProjectExpenseID", SqlDbType.Int,4)};
            parameters[0].Value = ProjectExpenseID;

            return DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }

        public decimal GetTotalExpense(int projectId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select sum(Expense) from F_ProjectExpenseTmp");
            strSql.Append(" where ProjectID=@ProjectID ");
            SqlParameter[] parameters = {
                    new SqlParameter("@ProjectID", SqlDbType.Int,4)};
            parameters[0].Value = projectId;

            object res = DbHelperSQL.GetSingle(strSql.ToString(), parameters);
            if (res != null)
            {
                return Convert.ToDecimal(res);
            }
            return 0;
        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Entity.ProjectExpenseTmpInfo GetModel(int ProjectExpenseID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 ProjectExpenseID,ProjectID,ProjectCode,Description,Expense,Remark from F_ProjectExpenseTmp ");
            strSql.Append(" where ProjectExpenseID=@ProjectExpenseID ");
            SqlParameter[] parameters = {
					new SqlParameter("@ProjectExpenseID", SqlDbType.Int,4)};
            parameters[0].Value = ProjectExpenseID;

            return CBO.FillObject<ProjectExpenseTmpInfo>(DbHelperSQL.Query(strSql.ToString(), parameters));
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public IList<ProjectExpenseTmpInfo> GetList(string term, List<SqlParameter> param)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ProjectExpenseID,ProjectID,ProjectCode,Description,Expense,Remark ");
            strSql.Append(" FROM F_ProjectExpenseTmp ");
            if (!string.IsNullOrEmpty(term))
            {
                strSql.Append(" where " + term);
            }
            return CBO.FillCollection<ProjectExpenseTmpInfo>(DbHelperSQL.Query(strSql.ToString(), param));
        }
    }
}
