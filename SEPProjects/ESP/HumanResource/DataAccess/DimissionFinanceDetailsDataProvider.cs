using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESP.HumanResource.Common;
using System.Data.SqlClient;
using System.Data;

namespace ESP.HumanResource.DataAccess
{
    public class DimissionFinanceDetailsDataProvider
    {
        public DimissionFinanceDetailsDataProvider()
        { }
        #region  成员方法
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int DimissionFinanceDetailId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT COUNT(1) FROM SEP_DimissionFinanceDetails");
            strSql.Append(" WHERE DimissionFinanceDetailId= @DimissionFinanceDetailId");
            SqlParameter[] parameters = {
					new SqlParameter("@DimissionFinanceDetailId", SqlDbType.Int,4)
				};
            parameters[0].Value = DimissionFinanceDetailId;
            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(ESP.HumanResource.Entity.DimissionFinanceDetailsInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("INSERT INTO SEP_DimissionFinanceDetails(");
            strSql.Append("DimissionId,Loan,BusinessCard,AccountsPayable,Other,FinanceAuditStatus,Salary,TellerIds,TellerNames,AccountantIds,AccountantNames,BusinessCardAuditIds,BusinessCardAuditNames,DirectorId,DirectorName,Salary2,SalaryTellerId,SalaryTellerName,SalaryBranch)");
            strSql.Append(" VALUES (");
            strSql.Append("@DimissionId,@Loan,@BusinessCard,@AccountsPayable,@Other,@FinanceAuditStatus,@Salary,@TellerIds,@TellerNames,@AccountantIds,@AccountantNames,@BusinessCardAuditIds,@BusinessCardAuditNames,@DirectorId,@DirectorName,@Salary2,@SalaryTellerId,@SalaryTellerName,@SalaryBranch)");
            strSql.Append(";SELECT @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@DimissionId", SqlDbType.Int,4),
					new SqlParameter("@Loan", SqlDbType.NVarChar),
					new SqlParameter("@BusinessCard", SqlDbType.NVarChar),
					new SqlParameter("@AccountsPayable", SqlDbType.NVarChar),
					new SqlParameter("@Other", SqlDbType.NVarChar),
					new SqlParameter("@FinanceAuditStatus", SqlDbType.Int,4),
                    new SqlParameter("@Salary", SqlDbType.NVarChar),
					new SqlParameter("@TellerIds", SqlDbType.NVarChar),
					new SqlParameter("@TellerNames", SqlDbType.NVarChar),
					new SqlParameter("@AccountantIds", SqlDbType.NVarChar),
					new SqlParameter("@AccountantNames", SqlDbType.NVarChar),
					new SqlParameter("@BusinessCardAuditIds", SqlDbType.NVarChar),
					new SqlParameter("@BusinessCardAuditNames", SqlDbType.NVarChar),
					new SqlParameter("@DirectorId", SqlDbType.Int,4),
					new SqlParameter("@DirectorName", SqlDbType.NVarChar),
                    new SqlParameter("@Salary2", SqlDbType.NVarChar),
					new SqlParameter("@SalaryTellerId", SqlDbType.Int,4),
					new SqlParameter("@SalaryTellerName", SqlDbType.NVarChar),
                    new SqlParameter("@SalaryBranch", SqlDbType.Int, 4)};
            parameters[0].Value = model.DimissionId;
            parameters[1].Value = model.Loan;
            parameters[2].Value = model.BusinessCard;
            parameters[3].Value = model.AccountsPayable;
            parameters[4].Value = model.Other;
            parameters[5].Value = model.FinanceAuditStatus;
            parameters[6].Value = model.Salary;
            parameters[7].Value = model.TellerIds;
            parameters[8].Value = model.TellerNames;
            parameters[9].Value = model.AccountantIds;
            parameters[10].Value = model.AccountantNames;
            parameters[11].Value = model.BusinessCardAuditIds;
            parameters[12].Value = model.BusinessCardAuditNames;
            parameters[13].Value = model.DirectorId;
            parameters[14].Value = model.DirectorName;
            parameters[15].Value = model.Salary2;
            parameters[16].Value = model.SalaryTellerId;
            parameters[17].Value = model.SalaryTellerName;
            parameters[18].Value = model.SalaryBranch;

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
        public int Add(ESP.HumanResource.Entity.DimissionFinanceDetailsInfo model, SqlTransaction stran)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("INSERT INTO SEP_DimissionFinanceDetails(");
            strSql.Append("DimissionId,Loan,BusinessCard,AccountsPayable,Other,FinanceAuditStatus,Salary,TellerIds,TellerNames,AccountantIds,AccountantNames,BusinessCardAuditIds,BusinessCardAuditNames,DirectorId,DirectorName,Salary2,SalaryTellerId,SalaryTellerName,SalaryBranch)");
            strSql.Append(" VALUES (");
            strSql.Append("@DimissionId,@Loan,@BusinessCard,@AccountsPayable,@Other,@FinanceAuditStatus,@Salary,@TellerIds,@TellerNames,@AccountantIds,@AccountantNames,@BusinessCardAuditIds,@BusinessCardAuditNames,@DirectorId,@DirectorName,@Salary2,@SalaryTellerId,@SalaryTellerName,@SalaryBranch)");
            strSql.Append(";SELECT @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@DimissionId", SqlDbType.Int,4),
					new SqlParameter("@Loan", SqlDbType.NVarChar),
					new SqlParameter("@BusinessCard", SqlDbType.NVarChar),
					new SqlParameter("@AccountsPayable", SqlDbType.NVarChar),
					new SqlParameter("@Other", SqlDbType.NVarChar),
					new SqlParameter("@FinanceAuditStatus", SqlDbType.Int,4),
                    new SqlParameter("@Salary", SqlDbType.NVarChar),
					new SqlParameter("@TellerIds", SqlDbType.NVarChar),
					new SqlParameter("@TellerNames", SqlDbType.NVarChar),
					new SqlParameter("@AccountantIds", SqlDbType.NVarChar),
					new SqlParameter("@AccountantNames", SqlDbType.NVarChar),
					new SqlParameter("@BusinessCardAuditIds", SqlDbType.NVarChar),
					new SqlParameter("@BusinessCardAuditNames", SqlDbType.NVarChar),
					new SqlParameter("@DirectorId", SqlDbType.Int,4),
					new SqlParameter("@DirectorName", SqlDbType.NVarChar),
                    new SqlParameter("@Salary2", SqlDbType.NVarChar),
					new SqlParameter("@SalaryTellerId", SqlDbType.Int,4),
					new SqlParameter("@SalaryTellerName", SqlDbType.NVarChar),
                    new SqlParameter("@SalaryBranch", SqlDbType.Int, 4)};
            parameters[0].Value = model.DimissionId;
            parameters[1].Value = model.Loan;
            parameters[2].Value = model.BusinessCard;
            parameters[3].Value = model.AccountsPayable;
            parameters[4].Value = model.Other;
            parameters[5].Value = model.FinanceAuditStatus;
            parameters[6].Value = model.Salary;
            parameters[7].Value = model.TellerIds;
            parameters[8].Value = model.TellerNames;
            parameters[9].Value = model.AccountantIds;
            parameters[10].Value = model.AccountantNames;
            parameters[11].Value = model.BusinessCardAuditIds;
            parameters[12].Value = model.BusinessCardAuditNames;
            parameters[13].Value = model.DirectorId;
            parameters[14].Value = model.DirectorName;
            parameters[15].Value = model.Salary2;
            parameters[16].Value = model.SalaryTellerId;
            parameters[17].Value = model.SalaryTellerName;
            parameters[18].Value = model.SalaryBranch;

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
        public void Update(ESP.HumanResource.Entity.DimissionFinanceDetailsInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("UPDATE SEP_DimissionFinanceDetails set ");
            strSql.Append("DimissionId=@DimissionId,");
            strSql.Append("Loan=@Loan,");
            strSql.Append("BusinessCard=@BusinessCard,");
            strSql.Append("AccountsPayable=@AccountsPayable,");
            strSql.Append("Other=@Other,");
            strSql.Append("FinanceAuditStatus=@FinanceAuditStatus,");
            strSql.Append("Salary=@Salary,");
            strSql.Append("TellerIds=@TellerIds,");
            strSql.Append("TellerNames=@TellerNames,");
            strSql.Append("AccountantIds=@AccountantIds,");
            strSql.Append("AccountantNames=@AccountantNames,");
            strSql.Append("BusinessCardAuditIds=@BusinessCardAuditIds,");
            strSql.Append("BusinessCardAuditNames=@BusinessCardAuditNames,");
            strSql.Append("DirectorId=@DirectorId,");
            strSql.Append("DirectorName=@DirectorName, ");
            strSql.Append("Salary2=@Salary2,");
            strSql.Append("SalaryTellerId=@SalaryTellerId,");
            strSql.Append("SalaryTellerName=@SalaryTellerName,");
            strSql.Append("SalaryBranch=@SalaryBranch  ");
            strSql.Append(" WHERE DimissionFinanceDetailId=@DimissionFinanceDetailId");
            SqlParameter[] parameters = {
					new SqlParameter("@DimissionFinanceDetailId", SqlDbType.Int,4),
					new SqlParameter("@DimissionId", SqlDbType.Int,4),
					new SqlParameter("@Loan", SqlDbType.NVarChar),
					new SqlParameter("@BusinessCard", SqlDbType.NVarChar),
					new SqlParameter("@AccountsPayable", SqlDbType.NVarChar),
					new SqlParameter("@Other", SqlDbType.NVarChar),
					new SqlParameter("@FinanceAuditStatus", SqlDbType.Int,4),
                    new SqlParameter("@Salary", SqlDbType.NVarChar),
					new SqlParameter("@TellerIds", SqlDbType.NVarChar),
					new SqlParameter("@TellerNames", SqlDbType.NVarChar),
					new SqlParameter("@AccountantIds", SqlDbType.NVarChar),
					new SqlParameter("@AccountantNames", SqlDbType.NVarChar),
					new SqlParameter("@BusinessCardAuditIds", SqlDbType.NVarChar),
					new SqlParameter("@BusinessCardAuditNames", SqlDbType.NVarChar),
					new SqlParameter("@DirectorId", SqlDbType.Int,4),
					new SqlParameter("@DirectorName", SqlDbType.NVarChar),
                    new SqlParameter("@Salary2", SqlDbType.NVarChar),
					new SqlParameter("@SalaryTellerId", SqlDbType.Int,4),
					new SqlParameter("@SalaryTellerName", SqlDbType.NVarChar),
                    new SqlParameter("@SalaryBranch", SqlDbType.Int,4)};
            parameters[0].Value = model.DimissionFinanceDetailId;
            parameters[1].Value = model.DimissionId;
            parameters[2].Value = model.Loan;
            parameters[3].Value = model.BusinessCard;
            parameters[4].Value = model.AccountsPayable;
            parameters[5].Value = model.Other;
            parameters[6].Value = model.FinanceAuditStatus;
            parameters[7].Value = model.Salary;
            parameters[8].Value = model.TellerIds;
            parameters[9].Value = model.TellerNames;
            parameters[10].Value = model.AccountantIds;
            parameters[11].Value = model.AccountantNames;
            parameters[12].Value = model.BusinessCardAuditIds;
            parameters[13].Value = model.BusinessCardAuditNames;
            parameters[14].Value = model.DirectorId;
            parameters[15].Value = model.DirectorName;
            parameters[16].Value = model.Salary2;
            parameters[17].Value = model.SalaryTellerId;
            parameters[18].Value = model.SalaryTellerName;
            parameters[19].Value = model.SalaryBranch;

            DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public void Update(ESP.HumanResource.Entity.DimissionFinanceDetailsInfo model, SqlTransaction stran)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("UPDATE SEP_DimissionFinanceDetails SET ");
            strSql.Append("DimissionId=@DimissionId,");
            strSql.Append("Loan=@Loan,");
            strSql.Append("BusinessCard=@BusinessCard,");
            strSql.Append("AccountsPayable=@AccountsPayable,");
            strSql.Append("Other=@Other,");
            strSql.Append("FinanceAuditStatus=@FinanceAuditStatus,");
            strSql.Append("Salary=@Salary,");
            strSql.Append("TellerIds=@TellerIds,");
            strSql.Append("TellerNames=@TellerNames,");
            strSql.Append("AccountantIds=@AccountantIds,");
            strSql.Append("AccountantNames=@AccountantNames,");
            strSql.Append("BusinessCardAuditIds=@BusinessCardAuditIds,");
            strSql.Append("BusinessCardAuditNames=@BusinessCardAuditNames,");
            strSql.Append("DirectorId=@DirectorId,");
            strSql.Append("DirectorName=@DirectorName, ");
            strSql.Append("Salary2=@Salary2,");
            strSql.Append("SalaryTellerId=@SalaryTellerId,");
            strSql.Append("SalaryTellerName=@SalaryTellerName,");
            strSql.Append("SalaryBranch=@SalaryBranch  ");
            strSql.Append(" WHERE DimissionFinanceDetailId=@DimissionFinanceDetailId");
            SqlParameter[] parameters = {
					new SqlParameter("@DimissionFinanceDetailId", SqlDbType.Int,4),
					new SqlParameter("@DimissionId", SqlDbType.Int,4),
					new SqlParameter("@Loan", SqlDbType.NVarChar),
					new SqlParameter("@BusinessCard", SqlDbType.NVarChar),
					new SqlParameter("@AccountsPayable", SqlDbType.NVarChar),
					new SqlParameter("@Other", SqlDbType.NVarChar),
					new SqlParameter("@FinanceAuditStatus", SqlDbType.Int,4),
                    new SqlParameter("@Salary", SqlDbType.NVarChar),
					new SqlParameter("@TellerIds", SqlDbType.NVarChar),
					new SqlParameter("@TellerNames", SqlDbType.NVarChar),
					new SqlParameter("@AccountantIds", SqlDbType.NVarChar),
					new SqlParameter("@AccountantNames", SqlDbType.NVarChar),
					new SqlParameter("@BusinessCardAuditIds", SqlDbType.NVarChar),
					new SqlParameter("@BusinessCardAuditNames", SqlDbType.NVarChar),
					new SqlParameter("@DirectorId", SqlDbType.Int,4),
					new SqlParameter("@DirectorName", SqlDbType.NVarChar),
                    new SqlParameter("@Salary2", SqlDbType.NVarChar),
					new SqlParameter("@SalaryTellerId", SqlDbType.Int,4),
					new SqlParameter("@SalaryTellerName", SqlDbType.NVarChar),
                    new SqlParameter("@SalaryBranch", SqlDbType.Int,4)};
            parameters[0].Value = model.DimissionFinanceDetailId;
            parameters[1].Value = model.DimissionId;
            parameters[2].Value = model.Loan;
            parameters[3].Value = model.BusinessCard;
            parameters[4].Value = model.AccountsPayable;
            parameters[5].Value = model.Other;
            parameters[6].Value = model.FinanceAuditStatus;
            parameters[7].Value = model.Salary;
            parameters[8].Value = model.TellerIds;
            parameters[9].Value = model.TellerNames;
            parameters[10].Value = model.AccountantIds;
            parameters[11].Value = model.AccountantNames;
            parameters[12].Value = model.BusinessCardAuditIds;
            parameters[13].Value = model.BusinessCardAuditNames;
            parameters[14].Value = model.DirectorId;
            parameters[15].Value = model.DirectorName;
            parameters[16].Value = model.Salary2;
            parameters[17].Value = model.SalaryTellerId;
            parameters[18].Value = model.SalaryTellerName;
            parameters[19].Value = model.SalaryBranch;

            DbHelperSQL.ExecuteSql(strSql.ToString(), stran.Connection, stran, parameters);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public void Delete(int DimissionFinanceDetailId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("DELETE SEP_DimissionFinanceDetails ");
            strSql.Append(" WHERE DimissionFinanceDetailId=@DimissionFinanceDetailId");
            SqlParameter[] parameters = {
					new SqlParameter("@DimissionFinanceDetailId", SqlDbType.Int,4)
				};
            parameters[0].Value = DimissionFinanceDetailId;
            DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public ESP.HumanResource.Entity.DimissionFinanceDetailsInfo GetModel(int DimissionFinanceDetailId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM SEP_DimissionFinanceDetails ");
            strSql.Append(" WHERE DimissionFinanceDetailId=@DimissionFinanceDetailId");
            SqlParameter[] parameters = {
					new SqlParameter("@DimissionFinanceDetailId", SqlDbType.Int,4)};
            parameters[0].Value = DimissionFinanceDetailId;
            ESP.HumanResource.Entity.DimissionFinanceDetailsInfo model = new ESP.HumanResource.Entity.DimissionFinanceDetailsInfo();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            model.DimissionFinanceDetailId = DimissionFinanceDetailId;
            if (ds.Tables[0].Rows.Count > 0)
            {
                model.PopupData(ds.Tables[0].Rows[0]);
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
            strSql.Append("SELECT * ");
            strSql.Append(" FROM SEP_DimissionFinanceDetails ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 通过离职单编号获得人力资源审批信息
        /// </summary>
        /// <param name="dimissionId">离职单编号</param>
        /// <returns></returns>
        public ESP.HumanResource.Entity.DimissionFinanceDetailsInfo GetFinanceDetailInfo(int dimissionId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM SEP_DimissionFinanceDetails ");
            strSql.Append(" WHERE DimissionId=@DimissionId");
            SqlParameter[] parameters = {
					new SqlParameter("@DimissionId", SqlDbType.Int,4)};
            parameters[0].Value = dimissionId;
            ESP.HumanResource.Entity.DimissionFinanceDetailsInfo model = new ESP.HumanResource.Entity.DimissionFinanceDetailsInfo();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                model.PopupData(ds.Tables[0].Rows[0]);
                return model;
            }
            else
            {
                return null;
            }
        }
        #endregion  成员方法
    }
}