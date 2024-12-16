using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using ESP.Supplier.Common;
using ESP.Supplier.Entity;
using ESP.Supplier.BusinessLogic;

namespace ESP.Supplier.DataAccess
{
    public class SC_SupplierDataProvider
    {
        public SC_SupplierDataProvider()
        { }

        #region  成员方法

        public int Add(SC_Supplier model)
        {
            return Add(model, null);
        }
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(SC_Supplier model, SqlTransaction trans)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into SC_Supplier(");
            strSql.Append("LogName,Password,supplier_code,supplier_name,supplier_nameEN,supplier_sn,supplier_area,supplier_province,supplier_city,supplier_industry,supplier_scale,supplier_principal,supplier_property,supplier_builttime,supplier_website,supplier_source,supplier_Intro,contact_fax,contact_Tel,contact_Mobile,contact_Email,contact_address,reg_address,contact_ZIP,contact_msn,service_content,service_area,service_workamount,service_customization,service_ohter,business_price,business_paytime,business_prepay,evaluation_department,evaluation_level,evaluation_feedback,evaluation_note,account_name,account_bank,account_number,introfile,productfile,pricefile,filialeamount,filialeaddress,Credit,Cachet,Money,IsPerson,AuditorId,AuditorName,CreatTime,CreatIP,LastUpdateTime,LastUpdateIP,Type,Status,KnowWays,Reviewers,IsSendMail,linkRemark,auditRemark,PriceLevel)");
            strSql.Append(" values (");
            strSql.Append("@LogName,@Password,@supplier_code,@supplier_name,@supplier_nameEN,@supplier_sn,@supplier_area,@supplier_province,@supplier_city,@supplier_industry,@supplier_scale,@supplier_principal,@supplier_property,@supplier_builttime,@supplier_website,@supplier_source,@supplier_Intro,@contact_fax,@contact_Tel,@contact_Mobile,@contact_Email,@contact_address,@reg_address,@contact_ZIP,@contact_msn,@service_content,@service_area,@service_workamount,@service_customization,@service_ohter,@business_price,@business_paytime,@business_prepay,@evaluation_department,@evaluation_level,@evaluation_feedback,@evaluation_note,@account_name,@account_bank,@account_number,@introfile,@productfile,@pricefile,@filialeamount,@filialeaddress,@Credit,@Cachet,@Money,@IsPerson,@AuditorId,@AuditorName,@CreatTime,@CreatIP,@LastUpdateTime,@LastUpdateIP,@Type,@Status,@KnowWays,@Reviewers,@IsSendMail,@linkRemark,@auditRemark,@PriceLevel)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@LogName", SqlDbType.NVarChar,50),
					new SqlParameter("@Password", SqlDbType.NVarChar,50),
					new SqlParameter("@supplier_code", SqlDbType.NVarChar,50),
					new SqlParameter("@supplier_name", SqlDbType.NVarChar,200),
					new SqlParameter("@supplier_nameEN", SqlDbType.NVarChar,200),
					new SqlParameter("@supplier_sn", SqlDbType.NVarChar,100),
					new SqlParameter("@supplier_area", SqlDbType.Int,4),
					new SqlParameter("@supplier_province", SqlDbType.NVarChar,50),
					new SqlParameter("@supplier_city", SqlDbType.NVarChar,50),
					new SqlParameter("@supplier_industry", SqlDbType.Int,4),
					new SqlParameter("@supplier_scale", SqlDbType.Decimal,9),
					new SqlParameter("@supplier_principal", SqlDbType.Decimal,9),
					new SqlParameter("@supplier_property", SqlDbType.Int,4),
					new SqlParameter("@supplier_builttime", SqlDbType.NVarChar,50),
					new SqlParameter("@supplier_website", SqlDbType.NVarChar,100),
					new SqlParameter("@supplier_source", SqlDbType.Int,4),
					new SqlParameter("@supplier_Intro", SqlDbType.Text),
					new SqlParameter("@contact_fax", SqlDbType.NVarChar,50),
					new SqlParameter("@contact_Tel", SqlDbType.NVarChar,50),
					new SqlParameter("@contact_Mobile", SqlDbType.NVarChar,50),
					new SqlParameter("@contact_Email", SqlDbType.NVarChar,50),
					new SqlParameter("@contact_address", SqlDbType.NVarChar,200),
					new SqlParameter("@reg_address", SqlDbType.NVarChar,300),
					new SqlParameter("@contact_ZIP", SqlDbType.NChar,10),
					new SqlParameter("@contact_msn", SqlDbType.NVarChar,50),
					new SqlParameter("@service_content", SqlDbType.NVarChar,1000),
					new SqlParameter("@service_area", SqlDbType.NVarChar,500),
					new SqlParameter("@service_workamount", SqlDbType.NVarChar,500),
					new SqlParameter("@service_customization", SqlDbType.NVarChar,500),
					new SqlParameter("@service_ohter", SqlDbType.NVarChar,2000),
					new SqlParameter("@business_price", SqlDbType.NVarChar,100),
					new SqlParameter("@business_paytime", SqlDbType.NVarChar,50),
					new SqlParameter("@business_prepay", SqlDbType.NVarChar,50),
					new SqlParameter("@evaluation_department", SqlDbType.NVarChar,500),
					new SqlParameter("@evaluation_level", SqlDbType.NVarChar,500),
					new SqlParameter("@evaluation_feedback", SqlDbType.NVarChar,2000),
					new SqlParameter("@evaluation_note", SqlDbType.NVarChar,2000),
					new SqlParameter("@account_name", SqlDbType.NVarChar,100),
					new SqlParameter("@account_bank", SqlDbType.NVarChar,100),
					new SqlParameter("@account_number", SqlDbType.NVarChar,100),
					new SqlParameter("@introfile", SqlDbType.NVarChar,200),
					new SqlParameter("@productfile", SqlDbType.NVarChar,200),
					new SqlParameter("@pricefile", SqlDbType.NVarChar,200),
					new SqlParameter("@filialeamount", SqlDbType.Int,4),
					new SqlParameter("@filialeaddress", SqlDbType.NVarChar,4000),
					new SqlParameter("@Credit", SqlDbType.Int,4),
					new SqlParameter("@Cachet", SqlDbType.Int,4),
					new SqlParameter("@Money", SqlDbType.Int,4),
					new SqlParameter("@IsPerson", SqlDbType.Int,4),
					new SqlParameter("@AuditorId", SqlDbType.NVarChar,50),
					new SqlParameter("@AuditorName", SqlDbType.NVarChar,50),
					new SqlParameter("@CreatTime", SqlDbType.SmallDateTime),
					new SqlParameter("@CreatIP", SqlDbType.NVarChar,20),
					new SqlParameter("@LastUpdateTime", SqlDbType.SmallDateTime),
					new SqlParameter("@LastUpdateIP", SqlDbType.NVarChar,20),
					new SqlParameter("@Type", SqlDbType.Int,4),
					new SqlParameter("@Status", SqlDbType.Int,4),
					new SqlParameter("@KnowWays", SqlDbType.NVarChar,50),
					new SqlParameter("@Reviewers", SqlDbType.NVarChar,100),
                                        new SqlParameter("@IsSendMail",SqlDbType.Int,4),
                                        new SqlParameter("@linkRemark",SqlDbType.NVarChar,2000),
                                        new SqlParameter("@auditRemark",SqlDbType.NVarChar,500),
                                        new SqlParameter("@PriceLevel", SqlDbType.Int,4)
                                        };
            parameters[0].Value = model.LogName;
            parameters[1].Value = model.Password;
            parameters[2].Value = model.Supplier_code;
            parameters[3].Value = model.supplier_name;
            parameters[4].Value = model.supplier_nameEN;
            parameters[5].Value = model.supplier_sn;
            parameters[6].Value = model.supplier_area;
            parameters[7].Value = model.supplier_province;
            parameters[8].Value = model.supplier_city;
            parameters[9].Value = model.supplier_industry;
            parameters[10].Value = model.supplier_scale;
            parameters[11].Value = model.supplier_principal;
            parameters[12].Value = model.supplier_property;
            parameters[13].Value = model.supplier_builttime;
            parameters[14].Value = model.supplier_website;
            parameters[15].Value = model.supplier_source;
            parameters[16].Value = model.supplier_Intro;
            parameters[17].Value = model.contact_fax;
            parameters[18].Value = model.contact_Tel;
            parameters[19].Value = model.contact_Mobile;
            parameters[20].Value = model.contact_Email;
            parameters[21].Value = model.contact_address;
            parameters[22].Value = model.RegAddress;
            parameters[23].Value = model.contact_ZIP;
            parameters[24].Value = model.contact_msn;
            parameters[25].Value = model.service_content;
            parameters[26].Value = model.service_area;
            parameters[27].Value = model.service_workamount;
            parameters[28].Value = model.service_customization;
            parameters[29].Value = model.service_ohter;
            parameters[30].Value = model.business_price;
            parameters[31].Value = model.business_paytime;
            parameters[32].Value = model.business_prepay;
            parameters[33].Value = model.evaluation_department;
            parameters[34].Value = model.evaluation_level;
            parameters[35].Value = model.evaluation_feedback;
            parameters[36].Value = model.evaluation_note;
            parameters[37].Value = model.account_name;
            parameters[38].Value = model.account_bank;
            parameters[39].Value = model.account_number;
            parameters[40].Value = model.introfile;
            parameters[41].Value = model.productfile;
            parameters[42].Value = model.pricefile;
            parameters[43].Value = model.filialeamount;
            parameters[44].Value = model.filialeaddress;
            parameters[45].Value = model.Credit;
            parameters[46].Value = model.Cachet;
            parameters[47].Value = model.Money;
            parameters[48].Value = model.IsPerson;
            parameters[49].Value = model.AuditorId;
            parameters[50].Value = model.AuditorName;
            parameters[51].Value = model.CreatTime;
            parameters[52].Value = model.CreatIP;
            parameters[53].Value = model.LastUpdateTime;
            parameters[54].Value = model.LastUpdateIP;
            parameters[55].Value = model.Type;
            parameters[56].Value = model.Status;
            parameters[57].Value = model.KnowWays;
            parameters[58].Value = model.Reviewers;
            parameters[59].Value = model.issendmail;
            parameters[60].Value = model.LinkRemark;
            parameters[61].Value = model.AuditRemark;
            parameters[62].Value = model.PriceLevel;
            object obj = null;
            if (trans == null)
                obj = DbHelperSQL.GetSingle(strSql.ToString(), parameters);
            else
                obj = DbHelperSQL.GetSingle(strSql.ToString(), trans.Connection, trans, parameters);
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
        public void Update(SC_Supplier model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update SC_Supplier set ");
            strSql.Append("LogName=@LogName,");
            strSql.Append("Password=@Password,");
            strSql.Append("supplier_code=@supplier_code,");
            strSql.Append("supplier_name=@supplier_name,");
            strSql.Append("supplier_nameEN=@supplier_nameEN,");
            strSql.Append("supplier_sn=@supplier_sn,");
            strSql.Append("supplier_area=@supplier_area,");
            strSql.Append("supplier_province=@supplier_province,");
            strSql.Append("supplier_city=@supplier_city,");
            strSql.Append("supplier_industry=@supplier_industry,");
            strSql.Append("supplier_scale=@supplier_scale,");
            strSql.Append("supplier_principal=@supplier_principal,");
            strSql.Append("supplier_property=@supplier_property,");
            strSql.Append("supplier_builttime=@supplier_builttime,");
            strSql.Append("supplier_website=@supplier_website,");
            strSql.Append("supplier_source=@supplier_source,");
            strSql.Append("supplier_Intro=@supplier_Intro,");
            strSql.Append("contact_fax=@contact_fax,");
            strSql.Append("contact_Tel=@contact_Tel,");
            strSql.Append("contact_Mobile=@contact_Mobile,");
            strSql.Append("contact_Email=@contact_Email,");
            strSql.Append("contact_address=@contact_address,");
            strSql.Append("reg_address=@reg_address,");
            strSql.Append("contact_ZIP=@contact_ZIP,");
            strSql.Append("contact_msn=@contact_msn,");
            strSql.Append("service_content=@service_content,");
            strSql.Append("service_area=@service_area,");
            strSql.Append("service_workamount=@service_workamount,");
            strSql.Append("service_customization=@service_customization,");
            strSql.Append("service_ohter=@service_ohter,");
            strSql.Append("business_price=@business_price,");
            strSql.Append("business_paytime=@business_paytime,");
            strSql.Append("business_prepay=@business_prepay,");
            strSql.Append("evaluation_department=@evaluation_department,");
            strSql.Append("evaluation_level=@evaluation_level,");
            strSql.Append("evaluation_feedback=@evaluation_feedback,");
            strSql.Append("evaluation_note=@evaluation_note,");
            strSql.Append("account_name=@account_name,");
            strSql.Append("account_bank=@account_bank,");
            strSql.Append("account_number=@account_number,");
            strSql.Append("introfile=@introfile,");
            strSql.Append("productfile=@productfile,");
            strSql.Append("pricefile=@pricefile,");
            strSql.Append("filialeamount=@filialeamount,");
            strSql.Append("filialeaddress=@filialeaddress,");
            strSql.Append("Credit=@Credit,");
            strSql.Append("Cachet=@Cachet,");
            strSql.Append("Money=@Money,");
            strSql.Append("IsPerson=@IsPerson,");
            strSql.Append("AuditorId=@AuditorId,");
            strSql.Append("AuditorName=@AuditorName,");
            strSql.Append("CreatTime=@CreatTime,");
            strSql.Append("CreatIP=@CreatIP,");
            strSql.Append("LastUpdateTime=@LastUpdateTime,");
            strSql.Append("LastUpdateIP=@LastUpdateIP,");
            strSql.Append("Type=@Type,");
            strSql.Append("Status=@Status,");
            strSql.Append("KnowWays=@KnowWays,");
            strSql.Append("Reviewers=@Reviewers,");
            strSql.Append("IsSendMail=@IsSendMail ,linkRemark=@linkRemark,auditRemark=@auditRemark,PriceLevel=@PriceLevel");
            strSql.Append(" where id=@id ");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4),
					new SqlParameter("@LogName", SqlDbType.NVarChar,50),
					new SqlParameter("@Password", SqlDbType.NVarChar,50),
					new SqlParameter("@supplier_code", SqlDbType.NVarChar,50),
					new SqlParameter("@supplier_name", SqlDbType.NVarChar,200),
					new SqlParameter("@supplier_nameEN", SqlDbType.NVarChar,200),
					new SqlParameter("@supplier_sn", SqlDbType.NVarChar,100),
					new SqlParameter("@supplier_area", SqlDbType.Int,4),
					new SqlParameter("@supplier_province", SqlDbType.NVarChar,50),
					new SqlParameter("@supplier_city", SqlDbType.NVarChar,50),
					new SqlParameter("@supplier_industry", SqlDbType.Int,4),
					new SqlParameter("@supplier_scale", SqlDbType.Decimal,9),
					new SqlParameter("@supplier_principal", SqlDbType.Decimal,9),
					new SqlParameter("@supplier_property", SqlDbType.Int,4),
					new SqlParameter("@supplier_builttime", SqlDbType.NVarChar,50),
					new SqlParameter("@supplier_website", SqlDbType.NVarChar,100),
					new SqlParameter("@supplier_source", SqlDbType.Int,4),
					new SqlParameter("@supplier_Intro", SqlDbType.Text),
					new SqlParameter("@contact_fax", SqlDbType.NVarChar,50),
					new SqlParameter("@contact_Tel", SqlDbType.NVarChar,50),
					new SqlParameter("@contact_Mobile", SqlDbType.NVarChar,50),
					new SqlParameter("@contact_Email", SqlDbType.NVarChar,50),
					new SqlParameter("@contact_address", SqlDbType.NVarChar,200),
					new SqlParameter("@reg_address", SqlDbType.NVarChar,300),
					new SqlParameter("@contact_ZIP", SqlDbType.NChar,10),
					new SqlParameter("@contact_msn", SqlDbType.NVarChar,50),
					new SqlParameter("@service_content", SqlDbType.NVarChar,1000),
					new SqlParameter("@service_area", SqlDbType.NVarChar,500),
					new SqlParameter("@service_workamount", SqlDbType.NVarChar,500),
					new SqlParameter("@service_customization", SqlDbType.NVarChar,500),
					new SqlParameter("@service_ohter", SqlDbType.NVarChar,2000),
					new SqlParameter("@business_price", SqlDbType.NVarChar,100),
					new SqlParameter("@business_paytime", SqlDbType.NVarChar,50),
					new SqlParameter("@business_prepay", SqlDbType.NVarChar,50),
					new SqlParameter("@evaluation_department", SqlDbType.NVarChar,500),
					new SqlParameter("@evaluation_level", SqlDbType.NVarChar,500),
					new SqlParameter("@evaluation_feedback", SqlDbType.NVarChar,2000),
					new SqlParameter("@evaluation_note", SqlDbType.NVarChar,2000),
					new SqlParameter("@account_name", SqlDbType.NVarChar,100),
					new SqlParameter("@account_bank", SqlDbType.NVarChar,100),
					new SqlParameter("@account_number", SqlDbType.NVarChar,100),
					new SqlParameter("@introfile", SqlDbType.NVarChar,200),
					new SqlParameter("@productfile", SqlDbType.NVarChar,200),
					new SqlParameter("@pricefile", SqlDbType.NVarChar,200),
					new SqlParameter("@filialeamount", SqlDbType.Int,4),
					new SqlParameter("@filialeaddress", SqlDbType.NVarChar,4000),
					new SqlParameter("@Credit", SqlDbType.Int,4),
					new SqlParameter("@Cachet", SqlDbType.Int,4),
					new SqlParameter("@Money", SqlDbType.Int,4),
					new SqlParameter("@IsPerson", SqlDbType.Int,4),
					new SqlParameter("@AuditorId", SqlDbType.NVarChar,50),
					new SqlParameter("@AuditorName", SqlDbType.NVarChar,50),
					new SqlParameter("@CreatTime", SqlDbType.SmallDateTime),
					new SqlParameter("@CreatIP", SqlDbType.NVarChar,20),
					new SqlParameter("@LastUpdateTime", SqlDbType.SmallDateTime),
					new SqlParameter("@LastUpdateIP", SqlDbType.NVarChar,20),
					new SqlParameter("@Type", SqlDbType.Int,4),
					new SqlParameter("@Status", SqlDbType.Int,4),
					new SqlParameter("@KnowWays", SqlDbType.NVarChar,50),
					new SqlParameter("@Reviewers", SqlDbType.NVarChar,100),
                                        new SqlParameter("@IsSendMail",SqlDbType.Int,4),
                                        new SqlParameter("@linkRemark",SqlDbType.NVarChar,2000),
                                        new SqlParameter("@auditRemark",SqlDbType.NVarChar,500),
                                        new SqlParameter("@PriceLevel",SqlDbType.Int,4)
                                        };
            parameters[0].Value = model.id;
            parameters[1].Value = model.LogName;
            parameters[2].Value = model.Password;
            parameters[3].Value = model.Supplier_code;
            parameters[4].Value = model.supplier_name;
            parameters[5].Value = model.supplier_nameEN;
            parameters[6].Value = model.supplier_sn;
            parameters[7].Value = model.supplier_area;
            parameters[8].Value = model.supplier_province;
            parameters[9].Value = model.supplier_city;
            parameters[10].Value = model.supplier_industry;
            parameters[11].Value = model.supplier_scale;
            parameters[12].Value = model.supplier_principal;
            parameters[13].Value = model.supplier_property;
            parameters[14].Value = model.supplier_builttime;
            parameters[15].Value = model.supplier_website;
            parameters[16].Value = model.supplier_source;
            parameters[17].Value = model.supplier_Intro;
            parameters[18].Value = model.contact_fax;
            parameters[19].Value = model.contact_Tel;
            parameters[20].Value = model.contact_Mobile;
            parameters[21].Value = model.contact_Email;
            parameters[22].Value = model.contact_address;
            parameters[23].Value = model.RegAddress;
            parameters[24].Value = model.contact_ZIP;
            parameters[25].Value = model.contact_msn;
            parameters[26].Value = model.service_content;
            parameters[27].Value = model.service_area;
            parameters[28].Value = model.service_workamount;
            parameters[29].Value = model.service_customization;
            parameters[30].Value = model.service_ohter;
            parameters[31].Value = model.business_price;
            parameters[32].Value = model.business_paytime;
            parameters[33].Value = model.business_prepay;
            parameters[34].Value = model.evaluation_department;
            parameters[35].Value = model.evaluation_level;
            parameters[36].Value = model.evaluation_feedback;
            parameters[37].Value = model.evaluation_note;
            parameters[38].Value = model.account_name;
            parameters[39].Value = model.account_bank;
            parameters[40].Value = model.account_number;
            parameters[41].Value = model.introfile;
            parameters[42].Value = model.productfile;
            parameters[43].Value = model.pricefile;
            parameters[44].Value = model.filialeamount;
            parameters[45].Value = model.filialeaddress;
            parameters[46].Value = model.Credit;
            parameters[47].Value = model.Cachet;
            parameters[48].Value = model.Money;
            parameters[49].Value = model.IsPerson;
            parameters[50].Value = model.AuditorId;
            parameters[51].Value = model.AuditorName;
            parameters[52].Value = model.CreatTime;
            parameters[53].Value = model.CreatIP;
            parameters[54].Value = model.LastUpdateTime;
            parameters[55].Value = model.LastUpdateIP;
            parameters[56].Value = model.Type;
            parameters[57].Value = model.Status;
            parameters[58].Value = model.KnowWays;
            parameters[59].Value = model.Reviewers;
            parameters[60].Value = model.issendmail;
            parameters[61].Value = model.LinkRemark;
            parameters[62].Value = model.AuditRemark;
            parameters[63].Value = model.PriceLevel;
            DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 更新一条数据，暂时没有添加操作
        /// </summary>
        public void Update(SC_Supplier model, SqlTransaction trans, SqlConnection conn)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update SC_Supplier set ");
            strSql.Append("LogName=@LogName,");
            strSql.Append("Password=@Password,");
            strSql.Append("supplier_code=@supplier_code,");
            strSql.Append("supplier_name=@supplier_name,");
            strSql.Append("supplier_nameEN=@supplier_nameEN,");
            strSql.Append("supplier_sn=@supplier_sn,");
            strSql.Append("supplier_area=@supplier_area,");
            strSql.Append("supplier_province=@supplier_province,");
            strSql.Append("supplier_city=@supplier_city,");
            strSql.Append("supplier_industry=@supplier_industry,");
            strSql.Append("supplier_scale=@supplier_scale,");
            strSql.Append("supplier_principal=@supplier_principal,");
            strSql.Append("supplier_property=@supplier_property,");
            strSql.Append("supplier_builttime=@supplier_builttime,");
            strSql.Append("supplier_website=@supplier_website,");
            strSql.Append("supplier_source=@supplier_source,");
            strSql.Append("supplier_Intro=@supplier_Intro,");
            strSql.Append("contact_fax=@contact_fax,");
            strSql.Append("contact_Tel=@contact_Tel,");
            strSql.Append("contact_Mobile=@contact_Mobile,");
            strSql.Append("contact_Email=@contact_Email,");
            strSql.Append("contact_address=@contact_address,");
            strSql.Append("reg_address=@reg_address,");
            strSql.Append("contact_ZIP=@contact_ZIP,");
            strSql.Append("contact_msn=@contact_msn,");
            strSql.Append("service_content=@service_content,");
            strSql.Append("service_area=@service_area,");
            strSql.Append("service_workamount=@service_workamount,");
            strSql.Append("service_customization=@service_customization,");
            strSql.Append("service_ohter=@service_ohter,");
            strSql.Append("business_price=@business_price,");
            strSql.Append("business_paytime=@business_paytime,");
            strSql.Append("business_prepay=@business_prepay,");
            strSql.Append("evaluation_department=@evaluation_department,");
            strSql.Append("evaluation_level=@evaluation_level,");
            strSql.Append("evaluation_feedback=@evaluation_feedback,");
            strSql.Append("evaluation_note=@evaluation_note,");
            strSql.Append("account_name=@account_name,");
            strSql.Append("account_bank=@account_bank,");
            strSql.Append("account_number=@account_number,");
            strSql.Append("introfile=@introfile,");
            strSql.Append("productfile=@productfile,");
            strSql.Append("pricefile=@pricefile,");
            strSql.Append("filialeamount=@filialeamount,");
            strSql.Append("filialeaddress=@filialeaddress,");
            strSql.Append("Credit=@Credit,");
            strSql.Append("Cachet=@Cachet,");
            strSql.Append("Money=@Money,");
            strSql.Append("IsPerson=@IsPerson,");
            strSql.Append("AuditorId=@AuditorId,");
            strSql.Append("AuditorName=@AuditorName,");
            strSql.Append("CreatTime=@CreatTime,");
            strSql.Append("CreatIP=@CreatIP,");
            strSql.Append("LastUpdateTime=@LastUpdateTime,");
            strSql.Append("LastUpdateIP=@LastUpdateIP,");
            strSql.Append("Type=@Type,");
            strSql.Append("Status=@Status,");
            strSql.Append("KnowWays=@KnowWays,");
            strSql.Append("Reviewers=@Reviewers,");
            strSql.Append("IsSendMail=@IsSendMail,linkRemark=@linkRemark,auditRemark=@auditRemark,PriceLevel=@PriceLevel ");
            strSql.Append(" where id=@id ");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4),
					new SqlParameter("@LogName", SqlDbType.NVarChar,50),
					new SqlParameter("@Password", SqlDbType.NVarChar,50),
					new SqlParameter("@supplier_code", SqlDbType.NVarChar,50),
					new SqlParameter("@supplier_name", SqlDbType.NVarChar,200),
					new SqlParameter("@supplier_nameEN", SqlDbType.NVarChar,200),
					new SqlParameter("@supplier_sn", SqlDbType.NVarChar,100),
					new SqlParameter("@supplier_area", SqlDbType.Int,4),
					new SqlParameter("@supplier_province", SqlDbType.NVarChar,50),
					new SqlParameter("@supplier_city", SqlDbType.NVarChar,50),
					new SqlParameter("@supplier_industry", SqlDbType.Int,4),
					new SqlParameter("@supplier_scale", SqlDbType.Decimal,9),
					new SqlParameter("@supplier_principal", SqlDbType.Decimal,9),
					new SqlParameter("@supplier_property", SqlDbType.Int,4),
					new SqlParameter("@supplier_builttime", SqlDbType.NVarChar,50),
					new SqlParameter("@supplier_website", SqlDbType.NVarChar,100),
					new SqlParameter("@supplier_source", SqlDbType.Int,4),
					new SqlParameter("@supplier_Intro", SqlDbType.Text),
					new SqlParameter("@contact_fax", SqlDbType.NVarChar,50),
					new SqlParameter("@contact_Tel", SqlDbType.NVarChar,50),
					new SqlParameter("@contact_Mobile", SqlDbType.NVarChar,50),
					new SqlParameter("@contact_Email", SqlDbType.NVarChar,50),
					new SqlParameter("@contact_address", SqlDbType.NVarChar,200),
					new SqlParameter("@reg_address", SqlDbType.NVarChar,300),
					new SqlParameter("@contact_ZIP", SqlDbType.NChar,10),
					new SqlParameter("@contact_msn", SqlDbType.NVarChar,50),
					new SqlParameter("@service_content", SqlDbType.NVarChar,1000),
					new SqlParameter("@service_area", SqlDbType.NVarChar,500),
					new SqlParameter("@service_workamount", SqlDbType.NVarChar,500),
					new SqlParameter("@service_customization", SqlDbType.NVarChar,500),
					new SqlParameter("@service_ohter", SqlDbType.NVarChar,2000),
					new SqlParameter("@business_price", SqlDbType.NVarChar,100),
					new SqlParameter("@business_paytime", SqlDbType.NVarChar,50),
					new SqlParameter("@business_prepay", SqlDbType.NVarChar,50),
					new SqlParameter("@evaluation_department", SqlDbType.NVarChar,500),
					new SqlParameter("@evaluation_level", SqlDbType.NVarChar,500),
					new SqlParameter("@evaluation_feedback", SqlDbType.NVarChar,2000),
					new SqlParameter("@evaluation_note", SqlDbType.NVarChar,2000),
					new SqlParameter("@account_name", SqlDbType.NVarChar,100),
					new SqlParameter("@account_bank", SqlDbType.NVarChar,100),
					new SqlParameter("@account_number", SqlDbType.NVarChar,100),
					new SqlParameter("@introfile", SqlDbType.NVarChar,200),
					new SqlParameter("@productfile", SqlDbType.NVarChar,200),
					new SqlParameter("@pricefile", SqlDbType.NVarChar,200),
					new SqlParameter("@filialeamount", SqlDbType.Int,4),
					new SqlParameter("@filialeaddress", SqlDbType.NVarChar,4000),
					new SqlParameter("@Credit", SqlDbType.Int,4),
					new SqlParameter("@Cachet", SqlDbType.Int,4),
					new SqlParameter("@Money", SqlDbType.Int,4),
					new SqlParameter("@IsPerson", SqlDbType.Int,4),
					new SqlParameter("@AuditorId", SqlDbType.NVarChar,50),
					new SqlParameter("@AuditorName", SqlDbType.NVarChar,50),
					new SqlParameter("@CreatTime", SqlDbType.SmallDateTime),
					new SqlParameter("@CreatIP", SqlDbType.NVarChar,20),
					new SqlParameter("@LastUpdateTime", SqlDbType.SmallDateTime),
					new SqlParameter("@LastUpdateIP", SqlDbType.NVarChar,20),
					new SqlParameter("@Type", SqlDbType.Int,4),
					new SqlParameter("@Status", SqlDbType.Int,4),
					new SqlParameter("@KnowWays", SqlDbType.NVarChar,50),
					new SqlParameter("@Reviewers", SqlDbType.NVarChar,100),
                                        new SqlParameter("@IsSendMail",SqlDbType.Int,4),
                                        new SqlParameter("@linkRemark",SqlDbType.NVarChar,2000),
                                        new SqlParameter("@auditRemark",SqlDbType.NVarChar,500),
                                        new SqlParameter("@PriceLevel",SqlDbType.Int,4)
                                        };
            parameters[0].Value = model.id;
            parameters[1].Value = model.LogName;
            parameters[2].Value = model.Password;
            parameters[3].Value = model.Supplier_code;
            parameters[4].Value = model.supplier_name;
            parameters[5].Value = model.supplier_nameEN;
            parameters[6].Value = model.supplier_sn;
            parameters[7].Value = model.supplier_area;
            parameters[8].Value = model.supplier_province;
            parameters[9].Value = model.supplier_city;
            parameters[10].Value = model.supplier_industry;
            parameters[11].Value = model.supplier_scale;
            parameters[12].Value = model.supplier_principal;
            parameters[13].Value = model.supplier_property;
            parameters[14].Value = model.supplier_builttime;
            parameters[15].Value = model.supplier_website;
            parameters[16].Value = model.supplier_source;
            parameters[17].Value = model.supplier_Intro;
            parameters[18].Value = model.contact_fax;
            parameters[19].Value = model.contact_Tel;
            parameters[20].Value = model.contact_Mobile;
            parameters[21].Value = model.contact_Email;
            parameters[22].Value = model.contact_address;
            parameters[23].Value = model.RegAddress;
            parameters[24].Value = model.contact_ZIP;
            parameters[25].Value = model.contact_msn;
            parameters[26].Value = model.service_content;
            parameters[27].Value = model.service_area;
            parameters[28].Value = model.service_workamount;
            parameters[29].Value = model.service_customization;
            parameters[30].Value = model.service_ohter;
            parameters[31].Value = model.business_price;
            parameters[32].Value = model.business_paytime;
            parameters[33].Value = model.business_prepay;
            parameters[34].Value = model.evaluation_department;
            parameters[35].Value = model.evaluation_level;
            parameters[36].Value = model.evaluation_feedback;
            parameters[37].Value = model.evaluation_note;
            parameters[38].Value = model.account_name;
            parameters[39].Value = model.account_bank;
            parameters[40].Value = model.account_number;
            parameters[41].Value = model.introfile;
            parameters[42].Value = model.productfile;
            parameters[43].Value = model.pricefile;
            parameters[44].Value = model.filialeamount;
            parameters[45].Value = model.filialeaddress;
            parameters[46].Value = model.Credit;
            parameters[47].Value = model.Cachet;
            parameters[48].Value = model.Money;
            parameters[49].Value = model.IsPerson;
            parameters[50].Value = model.AuditorId;
            parameters[51].Value = model.AuditorName;
            parameters[52].Value = model.CreatTime;
            parameters[53].Value = model.CreatIP;
            parameters[54].Value = model.LastUpdateTime;
            parameters[55].Value = model.LastUpdateIP;
            parameters[56].Value = model.Type;
            parameters[57].Value = model.Status;
            parameters[58].Value = model.KnowWays;
            parameters[59].Value = model.Reviewers;
            parameters[60].Value = model.issendmail;
            parameters[61].Value = model.LinkRemark;
            parameters[62].Value = model.AuditRemark;
            parameters[62].Value = model.PriceLevel;
            DbHelperSQL.ExecuteSql(strSql.ToString(), conn, trans, parameters);
        }
        /// <summary>
        /// 删除一条数据
        /// </summary>
        public void Delete(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete SC_Supplier ");
            strSql.Append(" where id=@id");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)
				};
            parameters[0].Value = id;
            DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public void Delete(int id, SqlTransaction trans, SqlConnection conn)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete SC_Supplier ");
            strSql.Append(" where id=@id");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)
				};
            parameters[0].Value = id;
            DbHelperSQL.ExecuteSql(strSql.ToString(), conn, trans, parameters);
        }

        /// <summary>
        /// 逻辑删除供应商
        /// </summary>
        /// <param name="id"></param>
        /// <param name="trans"></param>
        public void LogicDelete(int id, SqlTransaction trans)
        {
            string sql = @"update sc_supplier set status=" + State.SupplierStatus_del + " where id=" + id;
            DbHelperSQL.ExecuteSql(sql, trans.Connection, trans);
        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public SC_Supplier GetModel(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from SC_Supplier ");
            strSql.Append(" where id=@id");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)};
            parameters[0].Value = id;
            SC_Supplier model = new SC_Supplier();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            model.id = id;
            if (ds.Tables[0].Rows.Count > 0)
            {
                model.LogName = ds.Tables[0].Rows[0]["LogName"].ToString();
                model.Password = ds.Tables[0].Rows[0]["Password"].ToString();
                model.supplier_name = ds.Tables[0].Rows[0]["supplier_name"].ToString();
                model.supplier_nameEN = ds.Tables[0].Rows[0]["supplier_nameEN"].ToString();
                model.supplier_sn = ds.Tables[0].Rows[0]["supplier_sn"].ToString();
                if (ds.Tables[0].Rows[0]["supplier_area"].ToString() != "")
                {
                    model.supplier_area = int.Parse(ds.Tables[0].Rows[0]["supplier_area"].ToString());
                }
                if (ds.Tables[0].Rows[0]["supplier_province"].ToString() != "")
                {
                    model.supplier_province = (ds.Tables[0].Rows[0]["supplier_province"].ToString());
                }
                if (ds.Tables[0].Rows[0]["supplier_city"].ToString() != "")
                {
                    model.supplier_city = (ds.Tables[0].Rows[0]["supplier_city"].ToString());
                }
                if (ds.Tables[0].Rows[0]["supplier_industry"].ToString() != "")
                {
                    model.supplier_industry = int.Parse(ds.Tables[0].Rows[0]["supplier_industry"].ToString());
                }
                if (ds.Tables[0].Rows[0]["supplier_scale"].ToString() != "")
                {
                    model.supplier_scale = decimal.Parse(ds.Tables[0].Rows[0]["supplier_scale"].ToString());
                }
                if (ds.Tables[0].Rows[0]["supplier_principal"].ToString() != "")
                {
                    model.supplier_principal = decimal.Parse(ds.Tables[0].Rows[0]["supplier_principal"].ToString());
                }
                if (ds.Tables[0].Rows[0]["supplier_property"].ToString() != "")
                {
                    model.supplier_property = int.Parse(ds.Tables[0].Rows[0]["supplier_property"].ToString());
                }
                model.supplier_builttime = ds.Tables[0].Rows[0]["supplier_builttime"].ToString();
                model.supplier_website = ds.Tables[0].Rows[0]["supplier_website"].ToString();
                if (ds.Tables[0].Rows[0]["supplier_source"].ToString() != "")
                {
                    model.supplier_source = int.Parse(ds.Tables[0].Rows[0]["supplier_source"].ToString());
                }
                model.supplier_Intro = ds.Tables[0].Rows[0]["supplier_Intro"].ToString();
                model.contact_fax = ds.Tables[0].Rows[0]["contact_fax"].ToString();
                model.contact_Tel = ds.Tables[0].Rows[0]["contact_Tel"].ToString();
                model.contact_Mobile = ds.Tables[0].Rows[0]["contact_Mobile"].ToString();
                model.contact_Email = ds.Tables[0].Rows[0]["contact_Email"].ToString();
                model.contact_address = ds.Tables[0].Rows[0]["contact_address"].ToString();
                model.contact_ZIP = ds.Tables[0].Rows[0]["contact_ZIP"].ToString();
                model.contact_msn = ds.Tables[0].Rows[0]["contact_msn"].ToString();
                model.service_content = ds.Tables[0].Rows[0]["service_content"].ToString();
                model.service_area = ds.Tables[0].Rows[0]["service_area"].ToString();
                model.service_workamount = ds.Tables[0].Rows[0]["service_workamount"].ToString();
                model.service_customization = ds.Tables[0].Rows[0]["service_customization"].ToString();
                model.service_ohter = ds.Tables[0].Rows[0]["service_ohter"].ToString();
                model.business_price = ds.Tables[0].Rows[0]["business_price"].ToString();
                model.business_paytime = ds.Tables[0].Rows[0]["business_paytime"].ToString();
                model.business_prepay = ds.Tables[0].Rows[0]["business_prepay"].ToString();
                model.evaluation_department = ds.Tables[0].Rows[0]["evaluation_department"].ToString();
                model.evaluation_level = ds.Tables[0].Rows[0]["evaluation_level"].ToString();
                model.evaluation_feedback = ds.Tables[0].Rows[0]["evaluation_feedback"].ToString();
                model.evaluation_note = ds.Tables[0].Rows[0]["evaluation_note"].ToString();
                model.account_name = ds.Tables[0].Rows[0]["account_name"].ToString();
                model.account_bank = ds.Tables[0].Rows[0]["account_bank"].ToString();
                model.account_number = ds.Tables[0].Rows[0]["account_number"].ToString();
                model.introfile = ds.Tables[0].Rows[0]["introfile"].ToString();
                model.productfile = ds.Tables[0].Rows[0]["productfile"].ToString();
                model.pricefile = ds.Tables[0].Rows[0]["pricefile"].ToString();
                if (ds.Tables[0].Rows[0]["filialeamount"].ToString() != "")
                {
                    model.filialeamount = int.Parse(ds.Tables[0].Rows[0]["filialeamount"].ToString());
                }
                model.filialeaddress = ds.Tables[0].Rows[0]["filialeaddress"].ToString();
                if (ds.Tables[0].Rows[0]["Credit"].ToString() != "")
                {
                    model.Credit = int.Parse(ds.Tables[0].Rows[0]["Credit"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Cachet"].ToString() != "")
                {
                    model.Cachet = int.Parse(ds.Tables[0].Rows[0]["Cachet"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Money"].ToString() != "")
                {
                    model.Money = int.Parse(ds.Tables[0].Rows[0]["Money"].ToString());
                }
                if (ds.Tables[0].Rows[0]["IsPerson"].ToString() != "")
                {
                    model.IsPerson = int.Parse(ds.Tables[0].Rows[0]["IsPerson"].ToString());
                }
                if (ds.Tables[0].Rows[0]["CreatTime"].ToString() != "")
                {
                    model.CreatTime = DateTime.Parse(ds.Tables[0].Rows[0]["CreatTime"].ToString());
                }
                model.CreatIP = ds.Tables[0].Rows[0]["CreatIP"].ToString();
                if (ds.Tables[0].Rows[0]["LastUpdateTime"].ToString() != "")
                {
                    model.LastUpdateTime = DateTime.Parse(ds.Tables[0].Rows[0]["LastUpdateTime"].ToString());
                }
                model.LastUpdateIP = ds.Tables[0].Rows[0]["LastUpdateIP"].ToString();
                if (ds.Tables[0].Rows[0]["Type"].ToString() != "")
                {
                    model.Type = int.Parse(ds.Tables[0].Rows[0]["Type"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Status"].ToString() != "")
                {
                    model.Status = int.Parse(ds.Tables[0].Rows[0]["Status"].ToString());
                }
                model.RegAddress = ds.Tables[0].Rows[0]["reg_address"].ToString();
                model.KnowWays = ds.Tables[0].Rows[0]["KnowWays"].ToString();
                model.Reviewers = ds.Tables[0].Rows[0]["Reviewers"].ToString();
                model.Supplier_code = ds.Tables[0].Rows[0]["supplier_code"].ToString();
                model.AuditorId = ds.Tables[0].Rows[0]["auditorId"].ToString();
                model.AuditorName = ds.Tables[0].Rows[0]["auditorName"].ToString();
                if (ds.Tables[0].Rows[0]["IsSendMail"].ToString() != "")
                {
                    model.issendmail = int.Parse(ds.Tables[0].Rows[0]["IsSendMail"].ToString());
                }
                model.LinkRemark = ds.Tables[0].Rows[0]["linkRemark"].ToString();
                model.AuditRemark = ds.Tables[0].Rows[0]["auditRemark"].ToString();
                model.InvoiceTitle = ds.Tables[0].Rows[0]["InvoiceTitle"].ToString();

                if (ds.Tables[0].Rows[0]["isgroup"].ToString() != "")
                {
                    model.IsGroup = bool.Parse(ds.Tables[0].Rows[0]["isgroup"].ToString());
                }

                if (ds.Tables[0].Rows[0]["PriceLevel"].ToString() != "")
                {
                    model.PriceLevel = int.Parse(ds.Tables[0].Rows[0]["PriceLevel"].ToString());
                }

                return model;
            }
            else
            {
                return null;
            }
        }

        public string getSupplierCode(string sqlwhere)
        {
            string strSql = "";
            string sql = "select supplier_code from SC_Supplier {0} order by supplier_code desc";
            if (!string.IsNullOrEmpty(sqlwhere))
            {
                strSql = string.Format(sql, "where supplier_code like 'S" + sqlwhere + "%'");
            }
            string code = "";
            DataSet ds = DbHelperSQL.Query(strSql.ToString());
            if (ds.Tables[0].Rows.Count > 0)
            {
                code = ds.Tables[0].Rows[0]["supplier_code"].ToString();
            }

            return code;

        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append(" FROM SC_Supplier ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }

        public List<SC_Supplier> GetList(string strWhere, SqlParameter[] parameters)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append(" FROM SC_Supplier ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return ESP.ConfigCommon.CBO.FillCollection<SC_Supplier>(DbHelperSQL.Query(strSql.ToString(), parameters));
        }
        public List<SC_Supplier> GetListforgroup(string strWhere, SqlParameter[] parameters)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append(" FROM SC_Supplier ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return ESP.ConfigCommon.CBO.FillCollection<SC_Supplier>(DbHelperSQL.Query(strSql.ToString(), parameters));
        }

        public List<SC_Supplier> GetList(string strWhere, List<SqlParameter> parms)
        {
            List<SC_Supplier> list = new List<SC_Supplier>();
            StringBuilder strB = new StringBuilder();
            strB.Append(" select * ");
            strB.Append(" FROM SC_Supplier ");
            string sql = string.Format(strB.ToString() + " where 1=1 {0} order by LastUpdateTime desc", strWhere);
            using (SqlDataReader r = DbHelperSQL.ExecuteReader(sql, parms))
            {
                while (r.Read())
                {
                    SC_Supplier c = new SC_Supplier();
                    c.PopupData(r);
                    list.Add(c);
                }
                r.Close();
            }
            return list;
        }

        public DataTable GetSupplierLinkField(string typeIds, string strWhere, List<SqlParameter> parms)
        {
            string sql = @"select *,c.fieldCount,d.sysId,d.sysName from sc_supplier as a
                                inner join (select distinct supplierid from sc_suppliertype where typelv=2 and typeid in (" + typeIds + @") ) as b on b.supplierid=a.id
                                left join (select count(id) as fieldCount,supplierid from sc_supplierfield group by supplierid) as c on c.supplierid=a.id 
                                left join (select supplierid,sysid,sysName from SC_SupplierActiveEmailSendLog group by supplierid,sysid,sysname ) as d on d.supplierId=a.id
                                where 1=1";
            sql += strWhere + " order by a.id desc";
            return DbHelperSQL.Query(sql, parms.ToArray()).Tables[0];

        }

        public List<SC_Supplier> GetListByAuditor(string strWhere, List<SqlParameter> parms)
        {
            List<SC_Supplier> list = new List<SC_Supplier>();
            StringBuilder strB = new StringBuilder();
            strB.Append(" select s.*,u.lastnamecn+u.firstnamecn as UserName ");
            strB.Append(" FROM SC_Supplier s left join sep_users u on s.auditorid = u.userid ");
            string sql = string.Format(strB.ToString() + " where 1=1 {0} order by s.LastUpdateTime desc", strWhere);
            using (SqlDataReader r = DbHelperSQL.ExecuteReader(sql, parms))
            {
                while (r.Read())
                {
                    SC_Supplier c = new SC_Supplier();
                    c.PopupData(r);
                    c.UserName = r["UserName"].ToString();
                    list.Add(c);
                }
                r.Close();
            }
            return list;
        }

        public List<SC_Supplier> GetListByQualificationAuditor(string strWhere, List<SqlParameter> parms)
        {
            List<SC_Supplier> list = new List<SC_Supplier>();
            StringBuilder strB = new StringBuilder();
            strB.Append(" select s.*,u.lastnamecn+u.firstnamecn as QualificationUserName ");
            strB.Append(" FROM SC_Supplier s left join sep_users u on s.QualificationAuditorId = u.userid ");
            string sql = string.Format(strB.ToString() + " where 1=1 {0} order by s.LastUpdateTime desc", strWhere);
            using (SqlDataReader r = DbHelperSQL.ExecuteReader(sql, parms))
            {
                while (r.Read())
                {
                    SC_Supplier c = new SC_Supplier();
                    c.PopupData(r);
                    c.QualificationUserName = r["QualificationUserName"].ToString();
                    list.Add(c);
                }
                r.Close();
            }
            return list;
        }

        /// <summary>
        /// 获取关联三级物料的供应商列表
        /// </summary>
        /// <param name="terms"></param>
        /// <param name="parms"></param>
        /// <returns></returns>
        public DataTable GetSupplierListJoinSupplierType(string terms, List<SqlParameter> parms)
        {
            string strSql = @"select * from(select c.id as typeId,(cast(c2.id as varchar)+'-'+cast(c1.id as varchar)+'-'+cast(c.id as varchar)) as allTypeId,d.supplierType,c2.name +' - ' + c1.name + ' - ' + c.name as typeName,a.* from sc_supplier as a
                                inner join sc_suppliertype as b on a.id=b.supplierid
                                inner join xml_versionlist as c on b.typeid=c.id
                                inner join xml_versionclass as c1 on c.classid=c1.id
                                inner join xml_versionclass as c2 on c2.id=c1.parentid
                                left join sc_supplierprotocolType as d on d.supplierid=a.id and d.typeid=b.typeid
                                where b.typelv=3 and c2.state=1 and c.State=1) as a where 1=1";
            strSql += terms;
            return DbHelperSQL.Query(strSql, parms.ToArray()).Tables[0];
        }


        public int Submit(SC_Supplier model, List<SC_LinkMan> linker, SC_Log logmodel, ESP.Compatible.Employee currentUser)
        {
            using (SqlConnection conn = new SqlConnection(DbHelperSQL.connectionString))
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();
                try
                {
                    int modelId = Add(model, trans);
                    if (linker != null)
                    {
                        //添加联系人
                        foreach (SC_LinkMan link in linker)
                        {
                            link.SupplierId = modelId;
                            link.LinkerId = 0;
                            link.CreatTime = DateTime.Now;
                            new SC_LinkManDataProvider().Add(link, trans);
                        }
                    }

                    logmodel.LogUserId = modelId;
                    logmodel.Des = "[" + currentUser.Name + "(" + currentUser.SysID + ")]代替[" + model.supplier_name + "]提交了供应商平台的注册信息";
                    new SC_LogDataProvider().Add(logmodel, trans, trans.Connection);

                    SC_AgencySupplierReg agency = new SC_AgencySupplierReg();
                    agency.SupplierId = modelId;
                    agency.SuplierName = model.supplier_name;
                    agency.CreateUserId = int.Parse(currentUser.SysID);
                    agency.CreateUserName = currentUser.Name;
                    agency.CreateDate = DateTime.Now;
                    agency.CreateIp = logmodel.IpAddress;
                    agency.CreateDesc = "";

                    new SC_AgencySupplierRegProvider().Add(agency, trans);

                    trans.Commit();
                    return modelId;
                }
                catch
                {
                    trans.Rollback();
                    return 0;
                }
            }
        }

        #endregion  成员方法
    }

    public class SC_SupplierPriceFilesProvider
    {
        public int Add(ESP.Supplier.Entity.SC_SupplierPriceFiles model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into SC_SupplierPriceFiles(SupplierId,FileUrl,FileName,Remark");
            strSql.Append(")");
            strSql.Append(" values (@SupplierId,@FileUrl,@FileName,@Remark");
            strSql.Append(")");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@SupplierId", SqlDbType.Int,4),
					new SqlParameter("@FileUrl", SqlDbType.NVarChar,500),
                    new SqlParameter("@FileName", SqlDbType.NVarChar,50),
					new SqlParameter("@Remark", SqlDbType.NVarChar,500)
                                        };
            parameters[0].Value = model.SupplierId;
            parameters[1].Value = model.FileUrl;
            parameters[2].Value = model.FileName;
            parameters[3].Value = model.Remark;

            object obj = null;

            obj = DbHelperSQL.GetSingle(strSql.ToString(), parameters);

            if (obj == null)
            {
                return 1;
            }
            else
            {
                return Convert.ToInt32(obj);
            }
        }

        public List<SC_SupplierPriceFiles> GetList(int supplierId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append(" FROM SC_SupplierPriceFiles ");
            strSql.Append(" where SupplierId=" + supplierId.ToString());

            return ESP.ConfigCommon.CBO.FillCollection<SC_SupplierPriceFiles>(DbHelperSQL.Query(strSql.ToString()));
        }

        public int Delete(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete SC_SupplierPriceFiles ");
            strSql.Append(" where id=@id");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)
				};
            parameters[0].Value = id;
            return DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public SC_SupplierPriceFiles GetModel(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from SC_SupplierPriceFiles ");
            strSql.Append(" where id=@id");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)};
            parameters[0].Value = id;
            return ESP.ConfigCommon.CBO.FillObject<SC_SupplierPriceFiles>(DbHelperSQL.Query(strSql.ToString(), parameters));
            
        }
    }
}
