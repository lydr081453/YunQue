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
	/// 数据访问类F_Contract。
	/// </summary>
	internal class ContractDataProvider : ESP.Finance.IDataAccess.IContractDataProvider
	{
		
		#region  成员方法
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int ContractID)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from F_Contract");
			strSql.Append(" where ContractID=@ContractID ");
			SqlParameter[] parameters = {
					new SqlParameter("@ContractID", SqlDbType.Int,4)};
			parameters[0].Value = ContractID;

			return DbHelperSQL.Exists(strSql.ToString(),parameters);
		}


        //public int GetNewVersion(int contractId)
        //{
        //    return GetNewVersion(contractId, false);
        //}

        public int GetNewVersion(int contractId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(*) from F_Contract");
            strSql.Append(" where ContractID=@ContractID ");
            SqlParameter[] parameters = {
					new SqlParameter("@ContractID", SqlDbType.Int,4)};
            parameters[0].Value = contractId;

            return (int)DbHelperSQL.GetSingle(strSql.ToString(), parameters) + 1;
        }

        public int Add(ContractInfo model)
        {
            return Add(model, null);
        }

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(ContractInfo model,SqlTransaction trans)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into F_Contract(");
            strSql.Append("ProjectID,projectCode,Description,TotalAmounts,Cost,Fee,Attachment,Version,ParentID,Usable,Remark,CustomerPOID,POCode,Del,IsDelay,CreatorUserId,CreatorUserName,CreateDate,Status,ContractType)");
			strSql.Append(" values (");
            strSql.Append("@ProjectID,@projectCode,@Description,@TotalAmounts,@Cost,@Fee,@Attachment,@Version,@ParentID,@Usable,@Remark,@CustomerPOID,@POCode,@Del,@IsDelay,@CreatorUserId,@CreatorUserName,@CreateDate,@Status,@ContractType)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = {
					new SqlParameter("@ProjectID", SqlDbType.Int,4),
					new SqlParameter("@projectCode", SqlDbType.NVarChar,50),
					new SqlParameter("@Description", SqlDbType.NVarChar,500),
					new SqlParameter("@TotalAmounts", SqlDbType.Decimal,9),
					new SqlParameter("@Cost", SqlDbType.Decimal,9),
                    new SqlParameter("@Fee",SqlDbType.Decimal,9),
					new SqlParameter("@Attachment", SqlDbType.NVarChar,200),
                    new SqlParameter("@Version",SqlDbType.Int,4),
                    new SqlParameter("@ParentID",SqlDbType.Int,4),
                    new SqlParameter("@Usable",SqlDbType.Bit,1),
                    new SqlParameter("@Remark",SqlDbType.NVarChar,200),
                    new SqlParameter("@CustomerPOID",SqlDbType.Int,4),
                    new SqlParameter("@POCode",SqlDbType.NVarChar,50),
                    new SqlParameter("@Del",SqlDbType.Bit),
                    new SqlParameter("@IsDelay",SqlDbType.Bit),
                    new SqlParameter("@CreatorUserId",SqlDbType.Int,4),
                    new SqlParameter("@CreatorUserName",SqlDbType.NVarChar,200),
                    new SqlParameter("@CreateDate",SqlDbType.DateTime),
                    new SqlParameter("@Status",SqlDbType.Int,4),
                    new SqlParameter("@ContractType",SqlDbType.Int,4)
                                        };
			parameters[0].Value =model.ProjectID;
			parameters[1].Value =model.projectCode;
			parameters[2].Value =model.Description;
			parameters[3].Value =model.TotalAmounts;
			parameters[4].Value =model.Cost;
            parameters[5].Value =model.Fee;
            parameters[6].Value =model.Attachment;
            parameters[7].Value =model.Version;
            parameters[8].Value =model.ParentID;
            parameters[9].Value =model.Usable;
            parameters[10].Value =model.Remark;
            parameters[11].Value =model.CustomerPOID;
            parameters[12].Value =model.POCode;
            parameters[13].Value = model.Del;
            parameters[14].Value = model.IsDelay;
            parameters[15].Value = model.CreatorUserId;
            parameters[16].Value = model.CreatorUserName;
            parameters[17].Value = model.CreateDate;
            parameters[18].Value = model.Status;
            parameters[19].Value = model.ContractType;
			object obj = DbHelperSQL.GetSingle(strSql.ToString(),trans,parameters);
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
        //public int Update(ContractInfo model)
        //{
        //    return Update(model,false);
        //}

        public int UpdateContractDel(int projectID, bool Del)
        {
            return UpdateContractDel(projectID, Del, null);
        }

        public int UpdateContractDelay(int projectID,SqlTransaction trans)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update F_Contract set ");
            strSql.Append("IsDelay=@IsDelay ");
            strSql.Append(" where ProjectID=@ProjectID");
            SqlParameter[] parameters = {
					new SqlParameter("@IsDelay", SqlDbType.Bit),
					new SqlParameter("@ProjectID", SqlDbType.Int,4)};
            parameters[0].Value = 0;
            parameters[1].Value = projectID;
            return DbHelperSQL.ExecuteSql(strSql.ToString(), trans, parameters);
        }
        public int UpdateContractDel(int projectID,bool Del,SqlTransaction trans)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update F_Contract set ");
            strSql.Append("Del=@Del ");
            strSql.Append(" where ProjectID=@ProjectID");
            SqlParameter[] parameters = {
					new SqlParameter("@Del", SqlDbType.Bit),
					new SqlParameter("@ProjectID", SqlDbType.Int,4)};
            parameters[0].Value = Del;
            parameters[1].Value = projectID;
            return DbHelperSQL.ExecuteSql(strSql.ToString(),trans, parameters);
        }

        public int Update(ContractInfo model)
        {
            return Update(model, null);
        }
		/// <summary>
		/// 更新一条数据
		/// </summary>
		public int Update(ContractInfo model,SqlTransaction trans)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update F_Contract set ");
			strSql.Append("ProjectID=@ProjectID,");
			strSql.Append("projectCode=@projectCode,");
			strSql.Append("Description=@Description,");
			strSql.Append("TotalAmounts=@TotalAmounts,");
			strSql.Append("Cost=@Cost,");
            strSql.Append("Fee=@Fee,");
            strSql.Append("Attachment=@Attachment, ");
            strSql.Append("Version=@Version,");
            strSql.Append("ParentID=@ParentID,");
            strSql.Append("Usable=@Usable,");
            strSql.Append("Remark=@Remark, ");
            strSql.Append("CustomerPOID=@CustomerPOID,");
            strSql.Append("POCode=@POCode, ");
            strSql.Append("Del=@Del,IsDelay=@IsDelay,Status=@Status,ContractType=@ContractType");
			strSql.Append(" where ContractID=@ContractID ");
			SqlParameter[] parameters = {
					new SqlParameter("@ContractID", SqlDbType.Int,4),
					new SqlParameter("@ProjectID", SqlDbType.Int,4),
					new SqlParameter("@projectCode", SqlDbType.NVarChar,50),
					new SqlParameter("@Description", SqlDbType.NVarChar,500),
					new SqlParameter("@TotalAmounts", SqlDbType.Decimal,9),
					new SqlParameter("@Cost", SqlDbType.Decimal,9),
                    new SqlParameter("@Fee",SqlDbType.Decimal,9),
					new SqlParameter("@Attachment", SqlDbType.NVarChar,200),
                    new SqlParameter("@Version",SqlDbType.Int,4),
                    new SqlParameter("@ParentID",SqlDbType.Int,4),
                    new SqlParameter("@Usable",SqlDbType.Bit,1),
                    new SqlParameter("@Remark",SqlDbType.NVarChar,200),
                    new SqlParameter("@CustomerPOID",SqlDbType.Int,4),
                    new SqlParameter("@POCode",SqlDbType.NVarChar,50),
                    new SqlParameter("@Del",SqlDbType.Bit),
                    new SqlParameter("@IsDelay",SqlDbType.Bit),
                    new SqlParameter("@Status",SqlDbType.Int,4),
                    new SqlParameter("@ContractType",SqlDbType.Int,4)
                                        };
			parameters[0].Value =model.ContractID;
			parameters[1].Value =model.ProjectID;
			parameters[2].Value =model.projectCode;
			parameters[3].Value =model.Description;
			parameters[4].Value =model.TotalAmounts;
			parameters[5].Value =model.Cost;
            parameters[6].Value =model.Fee;
            parameters[7].Value =model.Attachment;
            parameters[8].Value =model.Version;
            parameters[9].Value =model.ParentID;
            parameters[10].Value =model.Usable;
            parameters[11].Value =model.Remark;
            parameters[12].Value =model.CustomerPOID;
            parameters[13].Value =model.POCode;
            parameters[14].Value = model.Del;
            parameters[15].Value = model.IsDelay;
            parameters[16].Value = model.Status;
            parameters[17].Value = model.ContractType;
			return DbHelperSQL.ExecuteSql(strSql.ToString(),trans,parameters);
		}

        /// <summary>
        /// 更新证据链状态
        /// </summary>
        /// <param name="contractIds"></param>
        /// <param name="status">证据链状态</param>
        /// <returns></returns>
        public int UpdateContractStatus(string contractIds,ESP.Finance.Utility.ContractStatus.Status status)
        {
            string sql = @"update F_Contract set Status="+(int)status + " where ContractId in ("+contractIds+")";
            return DbHelperSQL.ExecuteSql(sql);
        }

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public int Delete(int ContractID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update F_Contract set Del=1");
			strSql.Append(" where ContractID=@ContractID ");
			SqlParameter[] parameters = {
					new SqlParameter("@ContractID", SqlDbType.Int,4)};
			parameters[0].Value = ContractID;

			return DbHelperSQL.ExecuteSql(strSql.ToString(),parameters);
		}

        ///// <summary>
        ///// 得到一个对象实体
        ///// </summary>
        //public ContractInfo GetModel(int ContractID)
        //{
        //    return GetModel(ContractID,false);
        //}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public ContractInfo GetModel(int ContractID)
		{
			
			StringBuilder strSql=new StringBuilder();
            strSql.Append("select  top 1 ContractID,ProjectID,projectCode,Description,TotalAmounts,Cost,Fee,Attachment,Version,ParentID,Usable,Remark,CustomerPOID,POCode,Del,IsDelay from F_Contract ");
			strSql.Append(" where ContractID=@ContractID ");
			SqlParameter[] parameters = {
					new SqlParameter("@ContractID", SqlDbType.Int,4)};
			parameters[0].Value = ContractID;


            return CBO.FillObject<ContractInfo>(DbHelperSQL.Query(strSql.ToString(), parameters));
		}

        //public decimal GetTotalCostByProject(int projectId)
        //{
        //    return GetTotalCostByProject(projectId, false);
        //}
        

        public decimal GetTotalCostByProject(int projectId)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select sum(cost) from F_Contract");
            strSql.Append(" where ProjectID=@ProjectID  ");
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


        //public decimal GetTotalAmountByProject(int projectId)
        //{
        //    return GetTotalAmountByProject(projectId, false);
        //}


        public decimal GetTotalAmountByProject(int projectId, int originalContractID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select sum(TotalAmounts) from F_Contract");
            strSql.Append(" where ProjectID=@ProjectID  and usable=1 and del=0 and contractid<>@contractid");
            SqlParameter[] parameters = {
                    new SqlParameter("@ProjectID", SqlDbType.Int,4),new SqlParameter("@contractid", SqlDbType.Int,4)};
            parameters[0].Value = projectId;
            parameters[1].Value = originalContractID;
            object res = DbHelperSQL.GetSingle(strSql.ToString(),  parameters);
            if (res != null)
            {
                return Convert.ToDecimal(res);
            }
            return 0;
        }

        public decimal GetOddAmountByProject(int projectID, int contractID, int originalContractID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select sum(TotalAmounts) from F_Contract");
            strSql.Append(" where ProjectID=@ProjectID  and contractid!=@contractID and contractid!=@OriginalContractID and usable=1 and del=0");
            SqlParameter[] parameters = {
                    new SqlParameter("@ProjectID", SqlDbType.Int,4),new SqlParameter("@contractID",SqlDbType.Int,4),new SqlParameter("@OriginalContractID",SqlDbType.Int,4)};
            parameters[0].Value = projectID;
            parameters[1].Value = contractID;
            parameters[2].Value = originalContractID;

            object res = DbHelperSQL.GetSingle(strSql.ToString(), parameters);
            if (res != null)
            {
                return Convert.ToDecimal(res);
            }
            return 0;
        }

        public IList<ESP.Finance.Entity.ContractInfo> GetListByProject(int projectID, string term, List<System.Data.SqlClient.SqlParameter> param)
        {
            return GetListByProject(projectID, term, param, null);
        }

        public IList<ESP.Finance.Entity.ContractInfo> GetListByProject(int projectID, string term, List<System.Data.SqlClient.SqlParameter> param,SqlTransaction trans)
        {
            if (string.IsNullOrEmpty(term))
            {
                term = " ProjectId = @ProjectId ";
            }
            else
            {
                term += " and ProjectId = @ProjectId ";
            }
            if (param == null)
            {
                param = new List<SqlParameter>();
            }

            SqlParameter pm = new SqlParameter("@ProjectId", SqlDbType.Int, 4);
            pm.Value = projectID;

            param.Add(pm);

            return GetList(term, param,trans);
        }

        public IList<ContractInfo> GetList(string term, List<SqlParameter> param)
        {
            return GetList(term, param, null);
        }
		/// <summary>
		/// 获得数据列表
		/// </summary>
        public IList<ContractInfo> GetList(string term, List<SqlParameter> param,SqlTransaction trans)
		{
			StringBuilder strSql=new StringBuilder();
            strSql.Append("select * ");
			strSql.Append(" FROM F_Contract ");
            if (!string.IsNullOrEmpty(term))
            {
                strSql.Append(" where del=0 and" + term);
            }
            return CBO.FillCollection<ContractInfo>(DbHelperSQL.Query(strSql.ToString(), trans, (param == null ? null : param.ToArray())));
		}



		#endregion  成员方法
	}
}

