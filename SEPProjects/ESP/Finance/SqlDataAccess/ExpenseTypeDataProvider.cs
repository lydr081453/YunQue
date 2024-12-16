using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using ESP.Finance.Entity;
using System.Collections.Generic;
using ESP.Finance.Utility;
namespace ESP.Finance.DataAccess
{
	/// <summary>
	/// 数据访问类ExpenseDAL。
	/// </summary>
    internal class ExpenseTypeDataProvider : ESP.Finance.IDataAccess.IExpenseTypeProvider
	{

        #region  成员方法

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(ESP.Finance.Entity.ExpenseTypeInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into F_ExpenseType(");
            strSql.Append("ExpenseType,ExpenseDesc,ParentID,CostDetailID,TypeLevel,Status)");
            strSql.Append(" values (");
            strSql.Append("@ExpenseType,@ExpenseDesc,@ParentID,@CostDetailID,@TypeLevel,@Status)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@ExpenseType", SqlDbType.NVarChar,200),
					new SqlParameter("@ExpenseDesc", SqlDbType.NVarChar,200),
                    new SqlParameter("@ParentID", SqlDbType.Int,4),
                    new SqlParameter("@CostDetailID", SqlDbType.Int,4),
                    new SqlParameter("@TypeLevel", SqlDbType.Int,4),
                    new SqlParameter("@Status", SqlDbType.Int,4)
                                        };
            parameters[0].Value = model.ExpenseType;
            parameters[1].Value = model.ExpenseDesc;
            parameters[2].Value = model.ParentID;
            parameters[3].Value = model.CostDetailID;
            parameters[4].Value = model.TypeLevel;
            parameters[5].Value = model.Status;

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
        /// 更新一条数据
        /// </summary>
        public int Update(ESP.Finance.Entity.ExpenseTypeInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update F_ExpenseType set ");
            strSql.Append("ExpenseType=@ExpenseType,");
            strSql.Append("ExpenseDesc=@ExpenseDesc,");
            strSql.Append("ParentID=@ParentID,");
            strSql.Append("CostDetailID=@CostDetailID ,");
            strSql.Append("TypeLevel=@TypeLevel ,");
            strSql.Append("Status = @Status ");
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4),
					new SqlParameter("@ExpenseType", SqlDbType.NVarChar,200),
					new SqlParameter("@ExpenseDesc", SqlDbType.NVarChar,200),
                    new SqlParameter("@ParentID", SqlDbType.Int,4),                    
                    new SqlParameter("@CostDetailID", SqlDbType.Int,4),
                    new SqlParameter("@TypeLevel", SqlDbType.Int,4),
                    new SqlParameter("@Status", SqlDbType.Int,4)                    
                                        };
            parameters[0].Value = model.ID;
            parameters[1].Value = model.ExpenseType;
            parameters[2].Value = model.ExpenseDesc;
            parameters[3].Value = model.ParentID;
            parameters[4].Value = model.CostDetailID;
            parameters[5].Value = model.TypeLevel;
            parameters[6].Value = model.Status;
            return DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public int Delete(int ID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete F_ExpenseType ");
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)};
            parameters[0].Value = ID;

            return DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public ESP.Finance.Entity.ExpenseTypeInfo GetModel(int ID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 ID,ExpenseType,ExpenseDesc,ParentID,CostDetailID,TypeLevel,Status from F_ExpenseType ");
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)};
            parameters[0].Value = ID;

            return CBO.FillObject<ExpenseTypeInfo>(DbHelperSQL.Query(strSql.ToString(), parameters));
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<ExpenseTypeInfo> GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ID,ExpenseType,ExpenseDesc,ParentID,CostDetailID,TypeLevel,Status ");
            strSql.Append(" FROM F_ExpenseType ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where 1=1 " + strWhere);
            }
            return CBO.FillCollection<ExpenseTypeInfo>(DbHelperSQL.Query(strSql.ToString()));
        }

        #endregion  成员方法
        
	}
}

