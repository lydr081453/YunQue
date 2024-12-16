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
	/// 数据访问类SupporterCostDAL。
	/// </summary>
    internal class SupporterCostDataProvider : ESP.Finance.IDataAccess.ISupporterCostDataProvider
	{
		
		#region  成员方法
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int SupportCostId)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from F_SupporterCost");
			strSql.Append(" where SupportCostId=@SupportCostId ");
			SqlParameter[] parameters = {
					new SqlParameter("@SupportCostId", SqlDbType.Int,4)};
			parameters[0].Value = SupportCostId;

			return DbHelperSQL.Exists(strSql.ToString(),parameters);
		}

        //        /// <summary>
        ///// 增加一条数据
        ///// </summary>
        //public int Add(ESP.Finance.Entity.SupporterCostInfo model)
        //{
        //    return Add(model, false);
        //}

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(ESP.Finance.Entity.SupporterCostInfo model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into F_SupporterCost(");
			strSql.Append("ProjectId,SupportId,Description,Amounts,Type,Remark,CostTypeID)");
			strSql.Append(" values (");
			strSql.Append("@ProjectId,@SupportId,@Description,@Amounts,@Type,@Remark,@CostTypeID)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = {
					new SqlParameter("@ProjectId", SqlDbType.Int,4),
					new SqlParameter("@SupportId", SqlDbType.Int,4),
					new SqlParameter("@Description", SqlDbType.NVarChar,1000),
					new SqlParameter("@Amounts", SqlDbType.Decimal,9),
					new SqlParameter("@Type", SqlDbType.Decimal,9),
					new SqlParameter("@Remark", SqlDbType.NVarChar,500),
                    new SqlParameter("@CostTypeID",SqlDbType.Int,4)
                                        };
			parameters[0].Value =model.ProjectId;
			parameters[1].Value =model.SupportId;
			parameters[2].Value =model.Description;
			parameters[3].Value =model.Amounts;
			parameters[4].Value =model.Type;
			parameters[5].Value =model.Remark;
            parameters[6].Value =model.CostTypeID;

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
        //        /// <summary>
        ///// 更新一条数据
        ///// </summary>
        //public int Update(ESP.Finance.Entity.SupporterCostInfo model)
        //{
        //    return Update(model,false);
        //}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public int Update(ESP.Finance.Entity.SupporterCostInfo model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update F_SupporterCost set ");
			strSql.Append("ProjectId=@ProjectId,");
			strSql.Append("SupportId=@SupportId,");
			strSql.Append("Description=@Description,");
			strSql.Append("Amounts=@Amounts ");
            strSql.Append("Type=@Type, ");
            strSql.Append("Remark=@Remark, ");
            strSql.Append("CostTypeID=@CostTypeID ");
            strSql.Append(" where SupportCostId=@SupportCostId and @Lastupdatetime >= Lastupdatetime ");
			SqlParameter[] parameters = {
					new SqlParameter("@SupportCostId", SqlDbType.Int,4),
					new SqlParameter("@ProjectId", SqlDbType.Int,4),
					new SqlParameter("@SupportId", SqlDbType.Int,4),
					new SqlParameter("@Description", SqlDbType.NVarChar,1000),
					new SqlParameter("@Amounts", SqlDbType.Decimal,9),
					new SqlParameter("@Type", SqlDbType.Decimal,9),
				    new SqlParameter("@Lastupdatetime", SqlDbType.Timestamp,8),
                    new SqlParameter("@Remark",SqlDbType.NVarChar,500),
                    new SqlParameter("@CostTypeID",SqlDbType.Int,4)
                                        };
			parameters[0].Value =model.SupportCostId;
			parameters[1].Value =model.ProjectId;
			parameters[2].Value =model.SupportId;
			parameters[3].Value =model.Description;
			parameters[4].Value =model.Amounts;
			parameters[5].Value =model.Type;
			parameters[6].Value =model.Lastupdatetime;
            parameters[7].Value =model.Remark;
            parameters[8].Value =model.CostTypeID;

			return DbHelperSQL.ExecuteSql(strSql.ToString(),parameters);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public int Delete(int SupportCostId)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete F_SupporterCost ");
			strSql.Append(" where SupportCostId=@SupportCostId ");
			SqlParameter[] parameters = {
					new SqlParameter("@SupportCostId", SqlDbType.Int,4)};
			parameters[0].Value = SupportCostId;

			return DbHelperSQL.ExecuteSql(strSql.ToString(),parameters);
		}

        //public decimal GetTotalCostBySupporter(int SupporterId)
        //{
        //    return GetTotalCostBySupporter(SupporterId, false);
        //}

        public decimal GetTotalCostBySupporter(int SupporterId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select sum(Amounts) from F_SupporterCost ");
            strSql.Append(" where SupportId=@SupportId ");
            SqlParameter[] parameters = {
                    new SqlParameter("@SupportId", SqlDbType.Int,4)};
            parameters[0].Value = SupporterId;

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
		public ESP.Finance.Entity.SupporterCostInfo GetModel(int SupportCostId)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 SupportCostId,ProjectId,SupportId,Description,Amounts,Type,Lastupdatetime,Remark,CostTypeID from F_SupporterCost ");
			strSql.Append(" where SupportCostId=@SupportCostId ");
			SqlParameter[] parameters = {
					new SqlParameter("@SupportCostId", SqlDbType.Int,4)};
			parameters[0].Value = SupportCostId;
            return CBO.FillObject<SupporterCostInfo>(DbHelperSQL.Query(strSql.ToString(), parameters));
		}

		/// <summary>
		/// 获得数据列表
		/// </summary>
        public IList<SupporterCostInfo> GetList(string term, List<SqlParameter> param)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select SupportCostId,ProjectId,SupportId,Description,Amounts,Type,Lastupdatetime,Remark,CostTypeID ");
			strSql.Append(" FROM F_SupporterCost ");
            if (!string.IsNullOrEmpty(term))
            {
                strSql.Append(" where " + term);
            }
            //if (param != null && param.Count > 0)
            //{
            //    SqlParameter[] ps = param.ToArray();

            //    return CBO.FillCollection<F_SupporterCost>(DbHelperSQL.ExecuteReader(strSql.ToString(), ps));
            //}
            return CBO.FillCollection<SupporterCostInfo>(DbHelperSQL.Query(strSql.ToString(),param));
		}

        public IList<SupporterCostInfo> GetList(int SupporterId,SqlTransaction trans)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select SupportCostId,ProjectId,SupportId,Description,Amounts,Type,Lastupdatetime,Remark,CostTypeID ");
            strSql.Append(" FROM F_SupporterCost where SupportId="+SupporterId.ToString());

            return CBO.FillCollection<SupporterCostInfo>(DbHelperSQL.Query(strSql.ToString(),trans));
        }


        public IList<SupporterCostInfo> GetList(int SupporterId, string term, List<SqlParameter> param)
        {
            if(string.IsNullOrEmpty(term))
            {
                term = " 1=1 ";
            }
            term += " and SupportId=@SupportId ";
            if(param == null)
            {
                param = new List<SqlParameter>();
            }
            SqlParameter pm = new SqlParameter("@SupportId",SqlDbType.Int,4);
            pm.Value = SupporterId;
            param.Add(pm);
            return GetList(term,param);
        }

        #endregion
    }
}

