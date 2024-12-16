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
	/// 数据访问类TaxRateDAL。
	/// </summary>
	internal class TaxRateDataProvider : ESP.Finance.IDataAccess.ITaxRateDataProvider
	{
		#region  成员方法

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public int GetMaxId()
		{
		return DbHelperSQL.GetMaxID("TaxRateID", "F_TaxRate"); 
		}

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int TaxRateID)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from F_TaxRate");
			strSql.Append(" where TaxRateID=@TaxRateID ");
			SqlParameter[] parameters = {
					new SqlParameter("@TaxRateID", SqlDbType.Int,4)};
			parameters[0].Value = TaxRateID;

			return DbHelperSQL.Exists(strSql.ToString(),parameters);
		}

        //public bool Exists(string term, List<System.Data.SqlClient.SqlParameter> param)
        //{
        //    return Exists(term,param,false);
        //}

        public bool Exists(string term, List<System.Data.SqlClient.SqlParameter> param)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(*)");
            strSql.Append(" FROM F_TaxRate ");
            if (!string.IsNullOrEmpty(term))
            {
                strSql.Append(" where " + term);
            }
            if (param != null && param.Count > 0)
            {
                return DbHelperSQL.Exists(strSql.ToString(),  param.ToArray());
            }
            return DbHelperSQL.Exists(term);
        }


        public int Add(ESP.Finance.Entity.TaxRateInfo model)
        {
            return Add(model,false);
        }

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(ESP.Finance.Entity.TaxRateInfo model,bool isInTrans)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into F_TaxRate(");
            strSql.Append("BizTypeName,BranchID,BranchCode,BranchName,CustomerID,CustomerName,CustomerShortName,TaxRate,BizTypeID,Remark,BeginDate,EndDate,InvoiceRate,VATParam1,VATParam2,VATParam3,VATParam4,VATParam5)");
			strSql.Append(" values (");
            strSql.Append("@BizTypeName,@BranchID,@BranchCode,@BranchName,@CustomerID,@CustomerName,@CustomerShortName,@TaxRate,@BizTypeID,@Remark,@BeginDate,@EndDate,@InvoiceRate,@VATParam1,@VATParam2,@VATParam3,@VATParam4,@VATParam5)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@BizTypeName", SqlDbType.NVarChar,50),
					new SqlParameter("@BranchID", SqlDbType.Int,4),
					new SqlParameter("@BranchCode", SqlDbType.NVarChar,50),
					new SqlParameter("@BranchName", SqlDbType.NVarChar,50),
					new SqlParameter("@CustomerID", SqlDbType.Int,4),
					new SqlParameter("@CustomerName", SqlDbType.NVarChar,50),
					new SqlParameter("@CustomerShortName", SqlDbType.NVarChar,50),
					new SqlParameter("@TaxRate", SqlDbType.Decimal,9),
					new SqlParameter("@BizTypeID", SqlDbType.Int,4),
                    new SqlParameter("@Remark",SqlDbType.NVarChar,100),
                    new SqlParameter("@BeginDate",SqlDbType.DateTime),
                    new SqlParameter("@EndDate",SqlDbType.DateTime),
                    new SqlParameter("@InvoiceRate",SqlDbType.Decimal),
                     new SqlParameter("@VATParam1",SqlDbType.Decimal),
                      new SqlParameter("@VATParam2",SqlDbType.Decimal),
                       new SqlParameter("@VATParam3",SqlDbType.Decimal),
                        new SqlParameter("@VATParam4",SqlDbType.Decimal),
                         new SqlParameter("@VATParam5",SqlDbType.Decimal)
                                        };
			parameters[0].Value =model.BizTypeName;
			parameters[1].Value =model.BranchID;
			parameters[2].Value =model.BranchCode;
			parameters[3].Value =model.BranchName;
			parameters[4].Value =model.CustomerID;
			parameters[5].Value =model.CustomerName;
			parameters[6].Value =model.CustomerShortName;
			parameters[7].Value =model.TaxRate;
			parameters[8].Value =model.BizTypeID;
            parameters[9].Value = model.BeginDate;
            parameters[10].Value = model.EndDate;
            parameters[11].Value = model.InvoiceRate;
            parameters[12].Value = model.VATParam1;
            parameters[13].Value = model.VATParam2;
            parameters[14].Value = model.VATParam3;
            parameters[15].Value = model.VATParam4;
            parameters[16].Value = model.VATParam5;

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
        //public int Update(ESP.Finance.Entity.TaxRateInfo model)
        //{
        //    return Update(model, false);    
        //}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public int Update(ESP.Finance.Entity.TaxRateInfo model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update F_TaxRate set ");
			strSql.Append("BizTypeName=@BizTypeName,");
			strSql.Append("BranchID=@BranchID,");
			strSql.Append("BranchCode=@BranchCode,");
			strSql.Append("BranchName=@BranchName,");
			strSql.Append("CustomerID=@CustomerID,");
			strSql.Append("CustomerName=@CustomerName,");
			strSql.Append("CustomerShortName=@CustomerShortName,");
			strSql.Append("TaxRate=@TaxRate,");
            strSql.Append("BizTypeID=@BizTypeID,BeginDate=@BeginDate,EndDate=@EndDate,InvoiceRate=@InvoiceRate,");
            strSql.Append("VATParam1=@VATParam1,VATParam2=@VATParam2,VATParam3=@VATParam3,VATParam4=@VATParam4,VATParam5=@VATParam5");
			strSql.Append(" where TaxRateID=@TaxRateID ");
			SqlParameter[] parameters = {
					new SqlParameter("@TaxRateID", SqlDbType.Int,4),
					new SqlParameter("@BizTypeName", SqlDbType.NVarChar,50),
					new SqlParameter("@BranchID", SqlDbType.Int,4),
					new SqlParameter("@BranchCode", SqlDbType.NVarChar,50),
					new SqlParameter("@BranchName", SqlDbType.NVarChar,50),
					new SqlParameter("@CustomerID", SqlDbType.Int,4),
					new SqlParameter("@CustomerName", SqlDbType.NVarChar,50),
					new SqlParameter("@CustomerShortName", SqlDbType.NVarChar,50),
					new SqlParameter("@TaxRate", SqlDbType.Decimal,9),
					new SqlParameter("@BizTypeID", SqlDbType.Int,4),
                                                            new SqlParameter("@BeginDate",SqlDbType.DateTime),
                    new SqlParameter("@EndDate",SqlDbType.DateTime),
                    new SqlParameter("@InvoiceRate",SqlDbType.Decimal)};
			parameters[0].Value =model.TaxRateID;
			parameters[1].Value =model.BizTypeName;
			parameters[2].Value =model.BranchID;
			parameters[3].Value =model.BranchCode;
			parameters[4].Value =model.BranchName;
			parameters[5].Value =model.CustomerID;
			parameters[6].Value =model.CustomerName;
			parameters[7].Value =model.CustomerShortName;
			parameters[8].Value =model.TaxRate;
			parameters[9].Value =model.BizTypeID;
            parameters[10].Value = model.BeginDate;
            parameters[11].Value = model.EndDate;
            parameters[12].Value = model.InvoiceRate;

			return DbHelperSQL.ExecuteSql(strSql.ToString(),parameters);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public int Delete(int TaxRateID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete F_TaxRate ");
			strSql.Append(" where TaxRateID=@TaxRateID ");
			SqlParameter[] parameters = {
					new SqlParameter("@TaxRateID", SqlDbType.Int,4)};
			parameters[0].Value = TaxRateID;

			return DbHelperSQL.ExecuteSql(strSql.ToString(),parameters);
		}

        //        /// <summary>
        ///// 根据客户得到一个对象实体
        ///// </summary>
        //public ESP.Finance.Entity.TaxRateInfo GetModel( int bizTypeId, int branchId)
        //{
        //    return GetModel( bizTypeId, branchId, false);
        //}

        /// <summary>
		/// 根据客户得到一个对象实体
		/// </summary>
        public ESP.Finance.Entity.TaxRateInfo GetModel(int bizTypeId,int branchId)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 * from F_TaxRate ");
            strSql.Append(" where CustomerID=@CustomerID and BizTypeID=@BizTypeID and BranchID=@BranchID ");
            SqlParameter[] parameters = {
                                    new SqlParameter("@BizTypeID", SqlDbType.Int,4),
                                    new SqlParameter("@BranchID", SqlDbType.Int,4),
                                        };
            parameters[0].Value = bizTypeId;
            parameters[1].Value = branchId;

            return CBO.FillObject<TaxRateInfo>(DbHelperSQL.Query(strSql.ToString(), parameters));
        }

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public ESP.Finance.Entity.TaxRateInfo GetModel(int TaxRateID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 * from F_TaxRate ");
			strSql.Append(" where TaxRateID=@TaxRateID ");
			SqlParameter[] parameters = {
					new SqlParameter("@TaxRateID", SqlDbType.Int,4)};
			parameters[0].Value = TaxRateID;

            return CBO.FillObject<TaxRateInfo>(DbHelperSQL.Query(strSql.ToString(), parameters));
		}

		/// <summary>
		/// 获得数据列表
		/// </summary>
        public IList<TaxRateInfo> GetList(string term,List<SqlParameter> param)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select * ");
			strSql.Append(" FROM F_TaxRate ");
            if (!string.IsNullOrEmpty(term))
            {
                strSql.Append(" where enddate is null and " + term);
            }
            strSql.Append(" ORDER BY Remark ");
            //if (param != null && param.Count > 0)
            //{
            //    SqlParameter[] ps = param.ToArray();
            //    return CBO.FillCollection<F_TaxRate>(DbHelperSQL.Query(strSql.ToString(), ps));
            //}
            return CBO.FillCollection<TaxRateInfo>(DbHelperSQL.Query(strSql.ToString(),param));
            //return DbHelperSQL.Query(strSql.ToString());
		}


        public IList<ESP.Finance.Entity.TaxRateInfo> GetList(int branchId, string term, List<System.Data.SqlClient.SqlParameter> param)
        {
            /*
            string allterm = " (BizTypeID=@BizTypeID and BranchID=@BranchID )";
            allterm += " or  (BranchID=@BranchID) ";
            allterm += " or  (BizTypeID=@BizTypeID  )";
             */
            string allterm = "  BranchID=@BranchID ";
            if (param == null)
            {
                param = new List<SqlParameter>();
            }
            //SqlParameter pm = new SqlParameter("@BizTypeID", SqlDbType.Int, 4);
            //pm.Value = bizTypeId;
            //param.Add(pm);

            SqlParameter pm = new SqlParameter("@BranchID", SqlDbType.Int, 4);
            pm.Value = branchId;
            param.Add(pm);


            if (!string.IsNullOrEmpty(term))
            {
                allterm = allterm + " " + term;
            }
            return GetList(allterm, param);
        }

		#endregion  成员方法
	}
}

