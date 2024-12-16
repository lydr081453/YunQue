using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESP.HumanResource.Common;
using System.Data.SqlClient;
using System.Data;

namespace ESP.HumanResource.DataAccess
{
    public class DimissionGrougHRDetailsDataProvider
    {
        public DimissionGrougHRDetailsDataProvider()
        { }
        #region  成员方法
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int DimissionGroupHRDetails)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT COUNT(1) FROM SEP_DimissionGrougHRDetails");
            strSql.Append(" WHERE DimissionGroupHRDetails= @DimissionGroupHRDetails");
            SqlParameter[] parameters = {
					new SqlParameter("@DimissionGroupHRDetails", SqlDbType.Int,4)
				};
            parameters[0].Value = DimissionGroupHRDetails;
            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(ESP.HumanResource.Entity.DimissionGrougHRDetailsInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("INSERT INTO SEP_DimissionGrougHRDetails(");
            strSql.Append("DimissionId,RemainAnnual,AdvanceAnnual,FixedAssets,PrincipalID,PrincipalName)");
            strSql.Append(" VALUES (");
            strSql.Append("@DimissionId,@RemainAnnual,@AdvanceAnnual,@FixedAssets,@PrincipalID,@PrincipalName)");
            strSql.Append(";SELECT @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@DimissionId", SqlDbType.Int,4),
					new SqlParameter("@RemainAnnual", SqlDbType.Decimal,9),
					new SqlParameter("@AdvanceAnnual", SqlDbType.Decimal,9),
					new SqlParameter("@FixedAssets", SqlDbType.NVarChar),
					new SqlParameter("@PrincipalID", SqlDbType.Int,4),
					new SqlParameter("@PrincipalName", SqlDbType.NVarChar)};
            parameters[0].Value = model.DimissionId;
            parameters[1].Value = model.RemainAnnual;
            parameters[2].Value = model.AdvanceAnnual;
            parameters[3].Value = model.FixedAssets;
            parameters[4].Value = model.PrincipalID;
            parameters[5].Value = model.PrincipalName;

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
        public int Add(ESP.HumanResource.Entity.DimissionGrougHRDetailsInfo model, SqlTransaction stran)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("INSERT INTO SEP_DimissionGrougHRDetails(");
            strSql.Append("DimissionId,RemainAnnual,AdvanceAnnual,FixedAssets,PrincipalID,PrincipalName)");
            strSql.Append(" VALUES (");
            strSql.Append("@DimissionId,@RemainAnnual,@AdvanceAnnual,@FixedAssets,@PrincipalID,@PrincipalName)");
            strSql.Append(";SELECT @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@DimissionId", SqlDbType.Int,4),
					new SqlParameter("@RemainAnnual", SqlDbType.Decimal,9),
					new SqlParameter("@AdvanceAnnual", SqlDbType.Decimal,9),
					new SqlParameter("@FixedAssets", SqlDbType.NVarChar),
					new SqlParameter("@PrincipalID", SqlDbType.Int,4),
					new SqlParameter("@PrincipalName", SqlDbType.NVarChar)};
            parameters[0].Value = model.DimissionId;
            parameters[1].Value = model.RemainAnnual;
            parameters[2].Value = model.AdvanceAnnual;
            parameters[3].Value = model.FixedAssets;
            parameters[4].Value = model.PrincipalID;
            parameters[5].Value = model.PrincipalName;

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
        public void Update(ESP.HumanResource.Entity.DimissionGrougHRDetailsInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("UPDATE SEP_DimissionGrougHRDetails SET ");
            strSql.Append("DimissionId=@DimissionId,");
            strSql.Append("RemainAnnual=@RemainAnnual,");
            strSql.Append("AdvanceAnnual=@AdvanceAnnual,");
            strSql.Append("FixedAssets=@FixedAssets,");
            strSql.Append("PrincipalID=@PrincipalID,");
            strSql.Append("PrincipalName=@PrincipalName");
            strSql.Append(" WHERE DimissionGroupHRDetails=@DimissionGroupHRDetails");
            SqlParameter[] parameters = {
					new SqlParameter("@DimissionGroupHRDetails", SqlDbType.Int,4),
					new SqlParameter("@DimissionId", SqlDbType.Int,4),
					new SqlParameter("@RemainAnnual", SqlDbType.Decimal,9),
					new SqlParameter("@AdvanceAnnual", SqlDbType.Decimal,9),
					new SqlParameter("@FixedAssets", SqlDbType.NVarChar),
					new SqlParameter("@PrincipalID", SqlDbType.Int,4),
					new SqlParameter("@PrincipalName", SqlDbType.NVarChar)};
            parameters[0].Value = model.DimissionGroupHRDetails;
            parameters[1].Value = model.DimissionId;
            parameters[2].Value = model.RemainAnnual;
            parameters[3].Value = model.AdvanceAnnual;
            parameters[4].Value = model.FixedAssets;
            parameters[5].Value = model.PrincipalID;
            parameters[6].Value = model.PrincipalName;

            DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public void Update(ESP.HumanResource.Entity.DimissionGrougHRDetailsInfo model, SqlTransaction stran)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("UPDATE SEP_DimissionGrougHRDetails SET ");
            strSql.Append("DimissionId=@DimissionId,");
            strSql.Append("RemainAnnual=@RemainAnnual,");
            strSql.Append("AdvanceAnnual=@AdvanceAnnual,");
            strSql.Append("FixedAssets=@FixedAssets,");
            strSql.Append("PrincipalID=@PrincipalID,");
            strSql.Append("PrincipalName=@PrincipalName");
            strSql.Append(" WHERE DimissionGroupHRDetails=@DimissionGroupHRDetails");
            SqlParameter[] parameters = {
					new SqlParameter("@DimissionGroupHRDetails", SqlDbType.Int,4),
					new SqlParameter("@DimissionId", SqlDbType.Int,4),
					new SqlParameter("@RemainAnnual", SqlDbType.Decimal,9),
					new SqlParameter("@AdvanceAnnual", SqlDbType.Decimal,9),
					new SqlParameter("@FixedAssets", SqlDbType.NVarChar),
					new SqlParameter("@PrincipalID", SqlDbType.Int,4),
					new SqlParameter("@PrincipalName", SqlDbType.NVarChar)};
            parameters[0].Value = model.DimissionGroupHRDetails;
            parameters[1].Value = model.DimissionId;
            parameters[2].Value = model.RemainAnnual;
            parameters[3].Value = model.AdvanceAnnual;
            parameters[4].Value = model.FixedAssets;
            parameters[5].Value = model.PrincipalID;
            parameters[6].Value = model.PrincipalName;

            DbHelperSQL.ExecuteSql(strSql.ToString(), stran.Connection, stran, parameters);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public void Delete(int DimissionGroupHRDetails)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("DELETE SEP_DimissionGrougHRDetails ");
            strSql.Append(" WHERE DimissionGroupHRDetails=@DimissionGroupHRDetails");
            SqlParameter[] parameters = {
					new SqlParameter("@DimissionGroupHRDetails", SqlDbType.Int,4)
				};
            parameters[0].Value = DimissionGroupHRDetails;
            DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public ESP.HumanResource.Entity.DimissionGrougHRDetailsInfo GetModel(int DimissionGroupHRDetails)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM SEP_DimissionGrougHRDetails ");
            strSql.Append(" WHERE DimissionGroupHRDetails=@DimissionGroupHRDetails");
            SqlParameter[] parameters = {
					new SqlParameter("@DimissionGroupHRDetails", SqlDbType.Int,4)};
            parameters[0].Value = DimissionGroupHRDetails;
            ESP.HumanResource.Entity.DimissionGrougHRDetailsInfo model = new ESP.HumanResource.Entity.DimissionGrougHRDetailsInfo();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            model.DimissionGroupHRDetails = DimissionGroupHRDetails;
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
            strSql.Append(" FROM SEP_DimissionGrougHRDetails ");
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
        public ESP.HumanResource.Entity.DimissionGrougHRDetailsInfo GetGroupHRDetailInfo(int dimissionId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM SEP_DimissionGrougHRDetails ");
            strSql.Append(" WHERE DimissionId=@DimissionId");
            SqlParameter[] parameters = {
					new SqlParameter("@DimissionId", SqlDbType.Int,4)};
            parameters[0].Value = dimissionId;
            ESP.HumanResource.Entity.DimissionGrougHRDetailsInfo model = new ESP.HumanResource.Entity.DimissionGrougHRDetailsInfo();
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