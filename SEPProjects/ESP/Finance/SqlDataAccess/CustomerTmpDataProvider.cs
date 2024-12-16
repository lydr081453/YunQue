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
	/// 数据访问类CustomerDAL。
	/// </summary>
    /// 
     
     
    internal class CustomerTmpDataProvider : ESP.Finance.IDataAccess.ICustomerTmpDataProvider
	{
        
		#region  成员方法
		/// <summary>
		/// 是否存在该记录
		/// </summary>
        public bool Exists(int CustomerTmpID)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from F_CustomerTmp");
			strSql.Append(" where CustomerTmpID=@CustomerTmpID ");
			SqlParameter[] parameters = {
					new SqlParameter("@CustomerTmpID", SqlDbType.Int,4)};
            parameters[0].Value = CustomerTmpID;

			return DbHelperSQL.Exists(strSql.ToString(),parameters);
		}

        ///// <summary>
        ///// 是否存在该记录
        ///// </summary>
        //public bool Exists(string term, List<SqlParameter> param)
        //{
        //    return Exists(term, param,false);
        //}

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(string term,List<SqlParameter> param)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(*)");
            strSql.Append(" FROM F_CustomerTmp ");
            if (!string.IsNullOrEmpty(term))
            {
                strSql.Append(" where " + term);
            }
            if (param != null && param.Count > 0)
            {
                return DbHelperSQL.Exists(strSql.ToString(), param.ToArray());
            }
            return DbHelperSQL.Exists(term);
        }

        public string CreateCustomerCode(string shorten)
        {
            return CreateCustomerCode(shorten,false);
        }

        public string CreateCustomerCode(string shorten,bool isInTrans)
        {
            string prefix = shorten.Substring(0,1);
            string strSql = "select max(customercode) as maxId from F_CustomerTmp as a where a.customercode like '" + prefix + "%'";

            object maxid = DbHelperSQL.GetSingle(strSql);
            if(maxid==null)
                maxid = prefix + "000";
            int submax = 0;
            int.TryParse(maxid.ToString().Substring(1), out submax);

            int no = submax;
            no++;
            return prefix + no.ToString("000");
        }

        ///// <summary>
        ///// 增加一条数据
        ///// </summary>
        ///// 
        //public int Add(ESP.Finance.Entity.CustomerTmpInfo model)
        //{
        //    return Add(model, false);
        //}

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(ESP.Finance.Entity.CustomerTmpInfo model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into F_CustomerTmp(");
            strSql.Append("AddressCode,InvoiceTitle,AreaID,AreaCode,AreaName,Address1,Address2,IndustryID,IndustryCode,IndustryName,CustomerCode,Scale,Principal,Builttime,ContactName,ContactPosition,ContactTel,ContactFax,Website,ContactMobile,ContactEmail,NameCN1,PostCode,AccountName,AccountBank,AccountNumber,Remark,LogoUrl,AppDate,AppCompany,NameCN2,NameEN1,NameEN2,ShortCN,ShortEN,AO,IsProxy,CreatorID,CreatorName,CreatorCode,CreatorUserID,Createdate,ProjectID,CustomerID)");
			strSql.Append(" values (");
            strSql.Append("@AddressCode,@InvoiceTitle,@AreaID,@AreaCode,@AreaName,@Address1,@Address2,@IndustryID,@IndustryCode,@IndustryName,@CustomerCode,@Scale,@Principal,@Builttime,@ContactName,@ContactPosition,@ContactTel,@ContactFax,@Website,@ContactMobile,@ContactEmail,@NameCN1,@PostCode,@AccountName,@AccountBank,@AccountNumber,@Remark,@LogoUrl,@AppDate,@AppCompany,@NameCN2,@NameEN1,@NameEN2,@ShortCN,@ShortEN,@AO,@IsProxy,@CreatorID,@CreatorName,@CreatorCode,@CreatorUserID,@Createdate,@ProjectID,@CustomerID)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = {
					new SqlParameter("@AddressCode", SqlDbType.NVarChar,10),
					new SqlParameter("@InvoiceTitle", SqlDbType.NVarChar,100),
					new SqlParameter("@AreaID", SqlDbType.Int,4),
					new SqlParameter("@AreaCode", SqlDbType.NVarChar,10),
					new SqlParameter("@AreaName", SqlDbType.NVarChar,100),
					new SqlParameter("@Address1", SqlDbType.NVarChar,200),
					new SqlParameter("@Address2", SqlDbType.NVarChar,200),
					new SqlParameter("@IndustryID", SqlDbType.Int,4),
					new SqlParameter("@IndustryCode", SqlDbType.NVarChar,10),
					new SqlParameter("@IndustryName", SqlDbType.NVarChar,100),
					new SqlParameter("@CustomerCode", SqlDbType.NVarChar,10),
					new SqlParameter("@Scale", SqlDbType.NVarChar,100),
					new SqlParameter("@Principal", SqlDbType.NVarChar,50),
					new SqlParameter("@Builttime", SqlDbType.NVarChar,50),
					new SqlParameter("@ContactName", SqlDbType.NVarChar,50),
					new SqlParameter("@ContactPosition", SqlDbType.NVarChar,50),
					new SqlParameter("@ContactTel", SqlDbType.NVarChar,50),
					new SqlParameter("@ContactFax", SqlDbType.NVarChar,50),
					new SqlParameter("@Website", SqlDbType.NVarChar,100),
					new SqlParameter("@ContactMobile", SqlDbType.NVarChar,50),
					new SqlParameter("@ContactEmail", SqlDbType.NVarChar,50),
					new SqlParameter("@NameCN1", SqlDbType.NVarChar,100),
					new SqlParameter("@PostCode", SqlDbType.NVarChar,10),
					new SqlParameter("@AccountName", SqlDbType.NVarChar,100),
					new SqlParameter("@AccountBank", SqlDbType.NVarChar,100),
					new SqlParameter("@AccountNumber", SqlDbType.NVarChar,100),
					new SqlParameter("@Remark", SqlDbType.NVarChar,2000),
					new SqlParameter("@LogoUrl", SqlDbType.NVarChar,200),
					new SqlParameter("@AppDate", SqlDbType.DateTime),
					new SqlParameter("@AppCompany", SqlDbType.NVarChar,50),
					//new SqlParameter("@Lastupdatetime", SqlDbType.Timestamp,8),
					new SqlParameter("@NameCN2", SqlDbType.NVarChar,100),
					new SqlParameter("@NameEN1", SqlDbType.NVarChar,100),
					new SqlParameter("@NameEN2", SqlDbType.NVarChar,100),
					new SqlParameter("@ShortCN", SqlDbType.NVarChar,100),
					new SqlParameter("@ShortEN", SqlDbType.NVarChar,100),
					new SqlParameter("@AO", SqlDbType.NVarChar,10),
                    new SqlParameter("@IsProxy", SqlDbType.Int,4),
					new SqlParameter("@CreatorID", SqlDbType.Int,4),
					new SqlParameter("@CreatorName", SqlDbType.NVarChar,50),
					new SqlParameter("@CreatorCode", SqlDbType.NVarChar,50),
					new SqlParameter("@CreatorUserID", SqlDbType.NVarChar,50),
                    new SqlParameter("@Createdate",SqlDbType.DateTime,8),
                    new SqlParameter("@ProjectID",SqlDbType.Int,4),
                    new SqlParameter("@CustomerID",SqlDbType.Int,4)
                                        };
			parameters[0].Value =model.AddressCode;
			parameters[1].Value =model.InvoiceTitle;
			parameters[2].Value =model.AreaID;
			parameters[3].Value =model.AreaCode;
			parameters[4].Value =model.AreaName;
			parameters[5].Value =model.Address1;
			parameters[6].Value =model.Address2;
			parameters[7].Value =model.IndustryID;
			parameters[8].Value =model.IndustryCode;
			parameters[9].Value =model.IndustryName;
			parameters[10].Value =model.CustomerCode;
			parameters[11].Value =model.Scale;
			parameters[12].Value =model.Principal;
			parameters[13].Value =model.Builttime;
			parameters[14].Value =model.ContactName;
			parameters[15].Value =model.ContactPosition;
			parameters[16].Value =model.ContactTel;
			parameters[17].Value =model.ContactFax;
			parameters[18].Value =model.Website;
			parameters[19].Value =model.ContactMobile;
			parameters[20].Value =model.ContactEmail;
			parameters[21].Value =model.NameCN1;
			parameters[22].Value =model.PostCode;
			parameters[23].Value =model.AccountName;
			parameters[24].Value =model.AccountBank;
			parameters[25].Value =model.AccountNumber;
			parameters[26].Value =model.Remark;
			parameters[27].Value =model.LogoUrl;
			parameters[28].Value =model.AppDate;
			parameters[29].Value =model.AppCompany;
			//parameters[30].Value =model.Lastupdatetime;
			parameters[30].Value =model.NameCN2;
			parameters[31].Value =model.NameEN1;
			parameters[32].Value =model.NameEN2;
			parameters[33].Value =model.ShortCN;
			parameters[34].Value =model.ShortEN;
			parameters[35].Value =model.AO;
            parameters[36].Value =model.IsProxy;
            parameters[37].Value =model.CreatorID;
            parameters[38].Value =model.CreatorName;
            parameters[39].Value =model.CreatorCode;
            parameters[40].Value =model.CreatorUserID;
            parameters[41].Value =model.Createdate;
            parameters[42].Value =model.ProjectID;
            parameters[43].Value =model.CustomerID;


            object obj = DbHelperSQL.GetSingle(strSql.ToString(),  parameters);
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
        //public int Update(ESP.Finance.Entity.CustomerTmpInfo model)
        //{
        //    return Update(model, false);
        //}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public int Update(ESP.Finance.Entity.CustomerTmpInfo model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update F_CustomerTmp set ");
			strSql.Append("AddressCode=@AddressCode,");
			strSql.Append("InvoiceTitle=@InvoiceTitle,");
			strSql.Append("AreaID=@AreaID,");
			strSql.Append("AreaCode=@AreaCode,");
			strSql.Append("AreaName=@AreaName,");
			strSql.Append("Address1=@Address1,");
			strSql.Append("Address2=@Address2,");
			strSql.Append("IndustryID=@IndustryID,");
			strSql.Append("IndustryCode=@IndustryCode,");
			strSql.Append("IndustryName=@IndustryName,");
			strSql.Append("CustomerCode=@CustomerCode,");
			strSql.Append("Scale=@Scale,");
			strSql.Append("Principal=@Principal,");
			strSql.Append("Builttime=@Builttime,");
			strSql.Append("ContactName=@ContactName,");
			strSql.Append("ContactPosition=@ContactPosition,");
			strSql.Append("ContactTel=@ContactTel,");
			strSql.Append("ContactFax=@ContactFax,");
			strSql.Append("Website=@Website,");
			strSql.Append("ContactMobile=@ContactMobile,");
			strSql.Append("ContactEmail=@ContactEmail,");
			strSql.Append("NameCN1=@NameCN1,");
			strSql.Append("PostCode=@PostCode,");
			strSql.Append("AccountName=@AccountName,");
			strSql.Append("AccountBank=@AccountBank,");
			strSql.Append("AccountNumber=@AccountNumber,");
			strSql.Append("Remark=@Remark,");
			strSql.Append("LogoUrl=@LogoUrl,");
			strSql.Append("AppDate=@AppDate,");
			strSql.Append("AppCompany=@AppCompany,");
			//strSql.Append("Lastupdatetime=@Lastupdatetime,");
			strSql.Append("NameCN2=@NameCN2,");
			strSql.Append("NameEN1=@NameEN1,");
			strSql.Append("NameEN2=@NameEN2,");
			strSql.Append("ShortCN=@ShortCN,");
			strSql.Append("ShortEN=@ShortEN,");
			strSql.Append("AO=@AO,");
            strSql.Append("IsProxy=@IsProxy,");
            strSql.Append("CreatorID=@CreatorID,");
            strSql.Append("CreatorName=@CreatorName,");
            strSql.Append("CreatorCode=@CreatorCode,");
            strSql.Append("CreatorUserID=@CreatorUserID, ");
            strSql.Append("ProjectID=@ProjectID, ");
            strSql.Append("CustomerID=@CustomerID ");
            strSql.Append(" where CustomerTmpID=@CustomerTmpID and @Lastupdatetime >= Lastupdatetime ");
			SqlParameter[] parameters = {
					new SqlParameter("@CustomerTmpID", SqlDbType.Int,4),
					new SqlParameter("@AddressCode", SqlDbType.NVarChar,10),
					new SqlParameter("@InvoiceTitle", SqlDbType.NVarChar,100),
					new SqlParameter("@AreaID", SqlDbType.Int,4),
					new SqlParameter("@AreaCode", SqlDbType.NVarChar,10),
					new SqlParameter("@AreaName", SqlDbType.NVarChar,100),
					new SqlParameter("@Address1", SqlDbType.NVarChar,200),
					new SqlParameter("@Address2", SqlDbType.NVarChar,200),
					new SqlParameter("@IndustryID", SqlDbType.Int,4),
					new SqlParameter("@IndustryCode", SqlDbType.NVarChar,10),
					new SqlParameter("@IndustryName", SqlDbType.NVarChar,100),
					new SqlParameter("@CustomerCode", SqlDbType.NVarChar,10),
					new SqlParameter("@Scale", SqlDbType.NVarChar,100),
					new SqlParameter("@Principal", SqlDbType.NVarChar,50),
					new SqlParameter("@Builttime", SqlDbType.NVarChar,50),
					new SqlParameter("@ContactName", SqlDbType.NVarChar,50),
					new SqlParameter("@ContactPosition", SqlDbType.NVarChar,50),
					new SqlParameter("@ContactTel", SqlDbType.NVarChar,50),
					new SqlParameter("@ContactFax", SqlDbType.NVarChar,50),
					new SqlParameter("@Website", SqlDbType.NVarChar,100),
					new SqlParameter("@ContactMobile", SqlDbType.NVarChar,50),
					new SqlParameter("@ContactEmail", SqlDbType.NVarChar,50),
					new SqlParameter("@NameCN1", SqlDbType.NVarChar,100),
					new SqlParameter("@PostCode", SqlDbType.NVarChar,10),
					new SqlParameter("@AccountName", SqlDbType.NVarChar,100),
					new SqlParameter("@AccountBank", SqlDbType.NVarChar,100),
					new SqlParameter("@AccountNumber", SqlDbType.NVarChar,100),
					new SqlParameter("@Remark", SqlDbType.NVarChar,2000),
					new SqlParameter("@LogoUrl", SqlDbType.NVarChar,200),
					new SqlParameter("@AppDate", SqlDbType.DateTime),
					new SqlParameter("@AppCompany", SqlDbType.NVarChar,50),
					new SqlParameter("@NameCN2", SqlDbType.NVarChar,100),
					new SqlParameter("@NameEN1", SqlDbType.NVarChar,100),
					new SqlParameter("@NameEN2", SqlDbType.NVarChar,100),
					new SqlParameter("@ShortCN", SqlDbType.NVarChar,100),
					new SqlParameter("@ShortEN", SqlDbType.NVarChar,100),
					new SqlParameter("@AO", SqlDbType.NVarChar,10),
                    new SqlParameter("@Lastupdatetime", SqlDbType.Timestamp,8),
                    new SqlParameter("@IsProxy", SqlDbType.Int,4),
					new SqlParameter("@CreatorID", SqlDbType.Int,4),
					new SqlParameter("@CreatorName", SqlDbType.NVarChar,50),
					new SqlParameter("@CreatorCode", SqlDbType.NVarChar,50),
					new SqlParameter("@CreatorUserID", SqlDbType.NVarChar,50),
                    new SqlParameter("@ProjectID",SqlDbType.Int,4),
                    new SqlParameter("@CustomerID",SqlDbType.Int,4)
                                        };
			parameters[0].Value =model.CustomerTmpID;
			parameters[1].Value =model.AddressCode;
			parameters[2].Value =model.InvoiceTitle;
			parameters[3].Value =model.AreaID;
			parameters[4].Value =model.AreaCode;
			parameters[5].Value =model.AreaName;
			parameters[6].Value =model.Address1;
			parameters[7].Value =model.Address2;
			parameters[8].Value =model.IndustryID;
			parameters[9].Value =model.IndustryCode;
			parameters[10].Value =model.IndustryName;
			parameters[11].Value =model.CustomerCode;
			parameters[12].Value =model.Scale;
			parameters[13].Value =model.Principal;
			parameters[14].Value =model.Builttime;
			parameters[15].Value =model.ContactName;
			parameters[16].Value =model.ContactPosition;
			parameters[17].Value =model.ContactTel;
			parameters[18].Value =model.ContactFax;
			parameters[19].Value =model.Website;
			parameters[20].Value =model.ContactMobile;
			parameters[21].Value =model.ContactEmail;
			parameters[22].Value =model.NameCN1;
			parameters[23].Value =model.PostCode;
			parameters[24].Value =model.AccountName;
			parameters[25].Value =model.AccountBank;
			parameters[26].Value =model.AccountNumber;
			parameters[27].Value =model.Remark;
			parameters[28].Value =model.LogoUrl;
			parameters[29].Value =model.AppDate;
			parameters[30].Value =model.AppCompany;
			parameters[31].Value =model.NameCN2;
			parameters[32].Value =model.NameEN1;
			parameters[33].Value =model.NameEN2;
			parameters[34].Value =model.ShortCN;
			parameters[35].Value =model.ShortEN;
			parameters[36].Value =model.AO;
            parameters[37].Value =model.Lastupdatetime;
            parameters[38].Value =model.IsProxy;
            parameters[39].Value =model.CreatorID;
            parameters[40].Value =model.CreatorName;
            parameters[41].Value =model.CreatorCode;
            parameters[42].Value =model.CreatorUserID;
            parameters[43].Value =model.ProjectID;
            parameters[44].Value =model.CustomerID;

			return DbHelperSQL.ExecuteSql(strSql.ToString(),parameters);
		}

        //        /// <summary>
        ///// 删除一条数据
        ///// </summary>
        //public int Delete(int CustomerTmpID)
        //{
        //    return Delete(CustomerTmpID, false);
        //}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public int Delete(int CustomerTmpID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete F_CustomerTmp ");
			strSql.Append(" where CustomerTmpID=@CustomerTmpID ");
			SqlParameter[] parameters = {
					new SqlParameter("@CustomerTmpID", SqlDbType.Int,4)};
			parameters[0].Value = CustomerTmpID;

			return DbHelperSQL.ExecuteSql(strSql.ToString(),parameters);
		}


        ///// <summary>
        ///// 得到一个对象实体
        ///// </summary>
        //public ESP.Finance.Entity.CustomerTmpInfo GetModel(int CustomerTmpID)
        //{
        //    return GetModel(CustomerTmpID, false);
        //}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public ESP.Finance.Entity.CustomerTmpInfo GetModel(int CustomerTmpID)
		{
			
			StringBuilder strSql=new StringBuilder();
            strSql.Append("select  top 1 CustomerTmpID,AddressCode,InvoiceTitle,AreaID,AreaCode,AreaName,Address1,Address2,IndustryID,IndustryCode,IndustryName,CustomerCode,Scale,Principal,Builttime,ContactName,ContactPosition,ContactTel,ContactFax,Website,ContactMobile,ContactEmail,NameCN1,PostCode,AccountName,AccountBank,AccountNumber,Remark,LogoUrl,AppDate,AppCompany,Lastupdatetime,NameCN2,NameEN1,NameEN2,ShortCN,ShortEN,AO,IsProxy,CreatorID,CreatorName,CreatorCode,CreatorUserID,Createdate,ProjectID,CustomerID from F_CustomerTmp ");
			strSql.Append(" where CustomerTmpID=@CustomerTmpID ");
			SqlParameter[] parameters = {
					new SqlParameter("@CustomerTmpID", SqlDbType.Int,4)};
            parameters[0].Value = CustomerTmpID;

            return CBO.FillObject<CustomerTmpInfo>(DbHelperSQL.Query(strSql.ToString(), parameters));
		}

        //        /// <summary>
        ///// 获得数据列表
        ///// </summary>
        //public IList<CustomerTmpInfo> GetList(string term, List<SqlParameter> param)
        //{
        //    return GetList(term, param, false);
        //}

		/// <summary>
		/// 获得数据列表
		/// </summary>
        public IList<CustomerTmpInfo> GetList(string term,List<SqlParameter> param)
		{
			StringBuilder strSql=new StringBuilder();
            strSql.Append("select CustomerTmpID,AddressCode,InvoiceTitle,AreaID,AreaCode,AreaName,Address1,Address2,IndustryID,IndustryCode,IndustryName,CustomerCode,Scale,Principal,Builttime,ContactName,ContactPosition,ContactTel,ContactFax,Website,ContactMobile,ContactEmail,NameCN1,PostCode,AccountName,AccountBank,AccountNumber,Remark,LogoUrl,AppDate,AppCompany,Lastupdatetime,NameCN2,NameEN1,NameEN2,ShortCN,ShortEN,AO,IsProxy,CreatorID,CreatorName,CreatorCode,CreatorUserID,Createdate,ProjectID,CustomerID  ");
			strSql.Append(" FROM F_CustomerTmp ");
            if (!string.IsNullOrEmpty(term))
            {
                strSql.Append(" where " + term);
            }
            if (param != null && param.Count > 0)
            {
                SqlParameter[] ps = param.ToArray();

                return CBO.FillCollection<CustomerTmpInfo>(DbHelperSQL.Query(strSql.ToString(),  ps));
            }
            return CBO.FillCollection<CustomerTmpInfo>(DbHelperSQL.Query(strSql.ToString()));
		}
		#endregion  成员方法
	}
}

