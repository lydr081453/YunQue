using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using ESP.Finance.Entity;
using System.Collections.Generic;
using ESP.Finance.Utility;

namespace ESP.Finance.DataAccess
{
    internal class ContractCostTmpDataProvider : ESP.Finance.IDataAccess.IContractCostTmpDataProvider
    {
        #region  成员方法
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int ContractCostID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from F_ContractCostTmp");
            strSql.Append(" where ContractCostID=@ContractCostID ");
            SqlParameter[] parameters = {
					new SqlParameter("@ContractCostID", SqlDbType.Int,4)};
            parameters[0].Value = ContractCostID;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }
        //        /// <summary>
        ///// 增加一条数据
        ///// </summary>
        //public int Add(F_ContractCostTmp model)
        //{
        //    return Add(model,false);
        //}

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(ContractCostTmpInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into F_ContractCostTmp(");
            strSql.Append("ContractID,ProjectID,ProjectCode,Description,Cost,Remark,CostTypeID)");
            strSql.Append(" values (");
            strSql.Append("@ContractID,@ProjectID,@ProjectCode,@Description,@Cost,@Remark,@CostTypeID)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@ContractID", SqlDbType.Int,4),
					new SqlParameter("@ProjectID", SqlDbType.Int,4),
					new SqlParameter("@ProjectCode", SqlDbType.NVarChar,50),
					new SqlParameter("@Description", SqlDbType.NVarChar,500),
					new SqlParameter("@Cost", SqlDbType.Decimal,9),
					new SqlParameter("@Remark", SqlDbType.NVarChar,500),
                    new SqlParameter("@CostTypeID",SqlDbType.Int,4)
                                        };
            parameters[0].Value = model.ContractID;
            parameters[1].Value = model.ProjectID;
            parameters[2].Value = model.ProjectCode;
            parameters[3].Value = model.Description;
            parameters[4].Value = model.Cost;
            parameters[5].Value = model.Remark;
            parameters[6].Value = model.CostTypeID;

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

        ///// <summary>
        ///// 更新一条数据
        ///// </summary>
        //public int Update(F_ContractCostTmp model)
        //{
        //    return Update(model,false);
        //}
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public int Update(ContractCostTmpInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update F_ContractCostTmp set ");
            strSql.Append("ContractID=@ContractID,");
            strSql.Append("ProjectID=@ProjectID,");
            strSql.Append("ProjectCode=@ProjectCode,");
            strSql.Append("Description=@Description,");
            strSql.Append("Cost=@Cost,");
            strSql.Append("Remark=@Remark,");
            strSql.Append("CostTypeID=@CostTypeID ");
            strSql.Append(" where ContractCostID=@ContractCostID ");
            SqlParameter[] parameters = {
					new SqlParameter("@ContractCostID", SqlDbType.Int,4),
					new SqlParameter("@ContractID", SqlDbType.Int,4),
					new SqlParameter("@ProjectID", SqlDbType.Int,4),
					new SqlParameter("@ProjectCode", SqlDbType.NVarChar,50),
					new SqlParameter("@Description", SqlDbType.NVarChar,500),
					new SqlParameter("@Cost", SqlDbType.Decimal,9),
					new SqlParameter("@Remark", SqlDbType.NVarChar,500),
                    new SqlParameter("@CostTypeID",SqlDbType.Int,4)
                                        };
            parameters[0].Value = model.ContractCostID;
            parameters[1].Value = model.ContractID;
            parameters[2].Value = model.ProjectID;
            parameters[3].Value = model.ProjectCode;
            parameters[4].Value = model.Description;
            parameters[5].Value = model.Cost;
            parameters[6].Value = model.Remark;
            parameters[7].Value = model.CostTypeID;

            return DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }

        ///// <summary>
        ///// 删除一条数据
        ///// </summary>
        //public int Delete(int ContractCostID)
        //{
        //    return Delete(ContractCostID,false);
        //}

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public int Delete(int ContractCostID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete F_ContractCostTmp ");
            strSql.Append(" where ContractCostID=@ContractCostID ");
            SqlParameter[] parameters = {
					new SqlParameter("@ContractCostID", SqlDbType.Int,4)};
            parameters[0].Value = ContractCostID;

            return DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }

        ///// <summary>
        ///// 得到一个对象实体
        ///// </summary>
        //public F_ContractCostTmp GetModel(int ContractCostID)
        //{
        //    return GetModel(ContractCostID,false);
        //}


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public ContractCostTmpInfo GetModel(int ContractCostID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 ContractCostID,ContractID,ProjectID,ProjectCode,Description,Cost,Remark,CostTypeID from F_ContractCostTmp ");
            strSql.Append(" where ContractCostID=@ContractCostID ");
            SqlParameter[] parameters = {
					new SqlParameter("@ContractCostID", SqlDbType.Int,4)};
            parameters[0].Value = ContractCostID;

            return CBO.FillObject<ContractCostTmpInfo>(DbHelperSQL.Query(strSql.ToString(), parameters));
        }

        //public decimal GetTotalCost(int projectId)
        //{
        //    return GetTotalCost(projectId, false);
        //}

        public decimal GetTotalCost(int projectId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select sum(cost) from F_ContractCostTmp");
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

        public IList<ContractCostTmpInfo> GetList(string term, List<SqlParameter> param)
        {
            return GetList(term, param, null);
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public IList<ContractCostTmpInfo> GetList(string term, List<SqlParameter> param, SqlTransaction trans)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ContractCostID,ContractID,ProjectID,ProjectCode,Description,Cost,Remark,CostTypeID ");
            strSql.Append(" FROM F_ContractCostTmp ");
            if (!string.IsNullOrEmpty(term))
            {
                strSql.Append(" where " + term);
            }
            return CBO.FillCollection<ContractCostTmpInfo>(DbHelperSQL.Query(strSql.ToString(), trans, (param == null ? null : param.ToArray())));
        }

        public IList<ESP.Finance.Entity.ContractCostTmpInfo> GetListByProject(int projectId, string term, List<System.Data.SqlClient.SqlParameter> param)
        {
            return GetListByProject(projectId, term, param, null);
        }

        public IList<ESP.Finance.Entity.ContractCostTmpInfo> GetListByProject(int projectId, string term, List<System.Data.SqlClient.SqlParameter> param, SqlTransaction trans)
        {
            if (string.IsNullOrEmpty(term))
            {
                term = " 1=1 ";
            }
            if (param == null)
            {
                param = new List<SqlParameter>();
            }

            term += " and ProjectID = @ProjectID";
            SqlParameter pm = new SqlParameter("@ProjectID", SqlDbType.Int, 4);
            pm.Value = projectId;

            param.Add(pm);

            return GetList(term, param, trans);
        }

        #endregion  成员方法
    }
}
