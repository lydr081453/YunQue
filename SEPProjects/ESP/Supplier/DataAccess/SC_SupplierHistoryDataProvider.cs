using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESP.Supplier.Common;
using ESP.Supplier.Entity;
using System.Data;
using System.Data.SqlClient;

namespace ESP.Supplier.DataAccess
{
    public class SC_SupplierHistoryDataProvider
    {
        public SC_SupplierHistoryDataProvider()
        { }
        #region  成员方法

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(SC_SupplierHistory model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into SC_SupplierHistory(");
            strSql.Append("Supplierid,HistoryEdition,HistoryInfomation,LogName,Password,supplier_name,supplier_nameEN,supplier_sn,supplier_area,supplier_province,supplier_city,supplier_industry,supplier_scale,supplier_principal,supplier_property,supplier_builttime,supplier_website,supplier_source,supplier_Intro,contact_fax,contact_Tel,contact_Mobile,contact_Email,contact_address,contact_ZIP,contact_msn,service_content,service_area,service_workamount,service_customization,service_ohter,business_price,business_paytime,business_prepay,evaluation_department,evaluation_level,evaluation_feedback,evaluation_note,account_name,account_bank,account_number,introfile,productfile,pricefile,filialeamount,filialeaddress,Credit,Cachet,Money,IsPerson,CreatTime,CreatIP,LastUpdateTime,LastUpdateIP,Type,Status)");
            strSql.Append(" values (");
            strSql.Append("@Supplierid,@HistoryEdition,@HistoryInfomation,@LogName,@Password,@supplier_name,@supplier_nameEN,@supplier_sn,@supplier_area,@supplier_province,@supplier_city,@supplier_industry,@supplier_scale,@supplier_principal,@supplier_property,@supplier_builttime,@supplier_website,@supplier_source,@supplier_Intro,@contact_fax,@contact_Tel,@contact_Mobile,@contact_Email,@contact_address,@contact_ZIP,@contact_msn,@service_content,@service_area,@service_workamount,@service_customization,@service_ohter,@business_price,@business_paytime,@business_prepay,@evaluation_department,@evaluation_level,@evaluation_feedback,@evaluation_note,@account_name,@account_bank,@account_number,@introfile,@productfile,@pricefile,@filialeamount,@filialeaddress,@Credit,@Cachet,@Money,@IsPerson,@CreatTime,@CreatIP,@LastUpdateTime,@LastUpdateIP,@Type,@Status)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@Supplierid", SqlDbType.Int,4),
					new SqlParameter("@HistoryEdition", SqlDbType.Int,4),
					new SqlParameter("@HistoryInfomation", SqlDbType.Image,16),
					new SqlParameter("@LogName", SqlDbType.NVarChar),
					new SqlParameter("@Password", SqlDbType.NVarChar),
					new SqlParameter("@supplier_name", SqlDbType.NVarChar),
					new SqlParameter("@supplier_nameEN", SqlDbType.NVarChar),
					new SqlParameter("@supplier_sn", SqlDbType.NVarChar),
					new SqlParameter("@supplier_area", SqlDbType.Int,4),
					new SqlParameter("@supplier_province", SqlDbType.NVarChar),
					new SqlParameter("@supplier_city", SqlDbType.NVarChar),
					new SqlParameter("@supplier_industry", SqlDbType.Int,4),
					new SqlParameter("@supplier_scale", SqlDbType.Decimal,9),
					new SqlParameter("@supplier_principal", SqlDbType.Decimal,9),
					new SqlParameter("@supplier_property", SqlDbType.Int,4),
					new SqlParameter("@supplier_builttime", SqlDbType.NVarChar),
					new SqlParameter("@supplier_website", SqlDbType.NVarChar),
					new SqlParameter("@supplier_source", SqlDbType.Int,4),
					new SqlParameter("@supplier_Intro", SqlDbType.Text),
					new SqlParameter("@contact_fax", SqlDbType.NVarChar),
					new SqlParameter("@contact_Tel", SqlDbType.NVarChar),
					new SqlParameter("@contact_Mobile", SqlDbType.NVarChar),
					new SqlParameter("@contact_Email", SqlDbType.NVarChar),
					new SqlParameter("@contact_address", SqlDbType.NVarChar),
					new SqlParameter("@contact_ZIP", SqlDbType.NChar),
					new SqlParameter("@contact_msn", SqlDbType.NVarChar),
					new SqlParameter("@service_content", SqlDbType.NVarChar),
					new SqlParameter("@service_area", SqlDbType.NVarChar),
					new SqlParameter("@service_workamount", SqlDbType.NVarChar),
					new SqlParameter("@service_customization", SqlDbType.NVarChar),
					new SqlParameter("@service_ohter", SqlDbType.NVarChar),
					new SqlParameter("@business_price", SqlDbType.NVarChar),
					new SqlParameter("@business_paytime", SqlDbType.NVarChar),
					new SqlParameter("@business_prepay", SqlDbType.NVarChar),
					new SqlParameter("@evaluation_department", SqlDbType.NVarChar),
					new SqlParameter("@evaluation_level", SqlDbType.NVarChar),
					new SqlParameter("@evaluation_feedback", SqlDbType.NVarChar),
					new SqlParameter("@evaluation_note", SqlDbType.NVarChar),
					new SqlParameter("@account_name", SqlDbType.NVarChar),
					new SqlParameter("@account_bank", SqlDbType.NVarChar),
					new SqlParameter("@account_number", SqlDbType.NVarChar),
					new SqlParameter("@introfile", SqlDbType.NVarChar),
					new SqlParameter("@productfile", SqlDbType.NVarChar),
					new SqlParameter("@pricefile", SqlDbType.NVarChar),
					new SqlParameter("@filialeamount", SqlDbType.Int,4),
					new SqlParameter("@filialeaddress", SqlDbType.NVarChar),
					new SqlParameter("@Credit", SqlDbType.Int,4),
					new SqlParameter("@Cachet", SqlDbType.Int,4),
					new SqlParameter("@Money", SqlDbType.Int,4),
					new SqlParameter("@IsPerson", SqlDbType.Int,4),
					new SqlParameter("@CreatTime", SqlDbType.SmallDateTime),
					new SqlParameter("@CreatIP", SqlDbType.NVarChar),
					new SqlParameter("@LastUpdateTime", SqlDbType.SmallDateTime),
					new SqlParameter("@LastUpdateIP", SqlDbType.NVarChar),
					new SqlParameter("@Type", SqlDbType.Int,4),
					new SqlParameter("@Status", SqlDbType.Int,4)};
            parameters[0].Value = model.Supplierid;
            parameters[1].Value = model.HistoryEdition;
            parameters[2].Value = model.HistoryInfomation;
            parameters[3].Value = model.LogName;
            parameters[4].Value = model.Password;
            parameters[5].Value = model.supplier_name;
            parameters[6].Value = model.supplier_nameEN;
            parameters[7].Value = model.supplier_sn;
            parameters[8].Value = model.supplier_area;
            parameters[9].Value = model.supplier_province;
            parameters[10].Value = model.supplier_city;
            parameters[11].Value = model.supplier_industry;
            parameters[12].Value = model.supplier_scale;
            parameters[13].Value = model.supplier_principal;
            parameters[14].Value = model.supplier_property;
            parameters[15].Value = model.supplier_builttime;
            parameters[16].Value = model.supplier_website;
            parameters[17].Value = model.supplier_source;
            parameters[18].Value = model.supplier_Intro;
            parameters[19].Value = model.contact_fax;
            parameters[20].Value = model.contact_Tel;
            parameters[21].Value = model.contact_Mobile;
            parameters[22].Value = model.contact_Email;
            parameters[23].Value = model.contact_address;
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
            parameters[50].Value = model.CreatTime;
            parameters[51].Value = model.CreatIP;
            parameters[52].Value = model.LastUpdateTime;
            parameters[53].Value = model.LastUpdateIP;
            parameters[54].Value = model.Type;
            parameters[55].Value = model.Status;

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
        public void Update(SC_SupplierHistory model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update SC_SupplierHistory set ");
            strSql.Append("Supplierid=@Supplierid,");
            strSql.Append("HistoryEdition=@HistoryEdition,");
            strSql.Append("HistoryInfomation=@HistoryInfomation,");
            strSql.Append("LogName=@LogName,");
            strSql.Append("Password=@Password,");
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
            strSql.Append("CreatTime=@CreatTime,");
            strSql.Append("CreatIP=@CreatIP,");
            strSql.Append("LastUpdateTime=@LastUpdateTime,");
            strSql.Append("LastUpdateIP=@LastUpdateIP,");
            strSql.Append("Type=@Type,");
            strSql.Append("Status=@Status");
            strSql.Append(" where id=@id");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4),
					new SqlParameter("@Supplierid", SqlDbType.Int,4),
					new SqlParameter("@HistoryEdition", SqlDbType.Int,4),
					new SqlParameter("@HistoryInfomation", SqlDbType.Image,16),
					new SqlParameter("@LogName", SqlDbType.NVarChar),
					new SqlParameter("@Password", SqlDbType.NVarChar),
					new SqlParameter("@supplier_name", SqlDbType.NVarChar),
					new SqlParameter("@supplier_nameEN", SqlDbType.NVarChar),
					new SqlParameter("@supplier_sn", SqlDbType.NVarChar),
					new SqlParameter("@supplier_area", SqlDbType.Int,4),
					new SqlParameter("@supplier_province", SqlDbType.NVarChar),
					new SqlParameter("@supplier_city", SqlDbType.NVarChar),
					new SqlParameter("@supplier_industry", SqlDbType.Int,4),
					new SqlParameter("@supplier_scale", SqlDbType.Decimal,9),
					new SqlParameter("@supplier_principal", SqlDbType.Decimal,9),
					new SqlParameter("@supplier_property", SqlDbType.Int,4),
					new SqlParameter("@supplier_builttime", SqlDbType.NVarChar),
					new SqlParameter("@supplier_website", SqlDbType.NVarChar),
					new SqlParameter("@supplier_source", SqlDbType.Int,4),
					new SqlParameter("@supplier_Intro", SqlDbType.Text),
					new SqlParameter("@contact_fax", SqlDbType.NVarChar),
					new SqlParameter("@contact_Tel", SqlDbType.NVarChar),
					new SqlParameter("@contact_Mobile", SqlDbType.NVarChar),
					new SqlParameter("@contact_Email", SqlDbType.NVarChar),
					new SqlParameter("@contact_address", SqlDbType.NVarChar),
					new SqlParameter("@contact_ZIP", SqlDbType.NChar),
					new SqlParameter("@contact_msn", SqlDbType.NVarChar),
					new SqlParameter("@service_content", SqlDbType.NVarChar),
					new SqlParameter("@service_area", SqlDbType.NVarChar),
					new SqlParameter("@service_workamount", SqlDbType.NVarChar),
					new SqlParameter("@service_customization", SqlDbType.NVarChar),
					new SqlParameter("@service_ohter", SqlDbType.NVarChar),
					new SqlParameter("@business_price", SqlDbType.NVarChar),
					new SqlParameter("@business_paytime", SqlDbType.NVarChar),
					new SqlParameter("@business_prepay", SqlDbType.NVarChar),
					new SqlParameter("@evaluation_department", SqlDbType.NVarChar),
					new SqlParameter("@evaluation_level", SqlDbType.NVarChar),
					new SqlParameter("@evaluation_feedback", SqlDbType.NVarChar),
					new SqlParameter("@evaluation_note", SqlDbType.NVarChar),
					new SqlParameter("@account_name", SqlDbType.NVarChar),
					new SqlParameter("@account_bank", SqlDbType.NVarChar),
					new SqlParameter("@account_number", SqlDbType.NVarChar),
					new SqlParameter("@introfile", SqlDbType.NVarChar),
					new SqlParameter("@productfile", SqlDbType.NVarChar),
					new SqlParameter("@pricefile", SqlDbType.NVarChar),
					new SqlParameter("@filialeamount", SqlDbType.Int,4),
					new SqlParameter("@filialeaddress", SqlDbType.NVarChar),
					new SqlParameter("@Credit", SqlDbType.Int,4),
					new SqlParameter("@Cachet", SqlDbType.Int,4),
					new SqlParameter("@Money", SqlDbType.Int,4),
					new SqlParameter("@IsPerson", SqlDbType.Int,4),
					new SqlParameter("@CreatTime", SqlDbType.SmallDateTime),
					new SqlParameter("@CreatIP", SqlDbType.NVarChar),
					new SqlParameter("@LastUpdateTime", SqlDbType.SmallDateTime),
					new SqlParameter("@LastUpdateIP", SqlDbType.NVarChar),
					new SqlParameter("@Type", SqlDbType.Int,4),
					new SqlParameter("@Status", SqlDbType.Int,4)};
            parameters[0].Value = model.id;
            parameters[1].Value = model.Supplierid;
            parameters[2].Value = model.HistoryEdition;
            parameters[3].Value = model.HistoryInfomation;
            parameters[4].Value = model.LogName;
            parameters[5].Value = model.Password;
            parameters[6].Value = model.supplier_name;
            parameters[7].Value = model.supplier_nameEN;
            parameters[8].Value = model.supplier_sn;
            parameters[9].Value = model.supplier_area;
            parameters[10].Value = model.supplier_province;
            parameters[11].Value = model.supplier_city;
            parameters[12].Value = model.supplier_industry;
            parameters[13].Value = model.supplier_scale;
            parameters[14].Value = model.supplier_principal;
            parameters[15].Value = model.supplier_property;
            parameters[16].Value = model.supplier_builttime;
            parameters[17].Value = model.supplier_website;
            parameters[18].Value = model.supplier_source;
            parameters[19].Value = model.supplier_Intro;
            parameters[20].Value = model.contact_fax;
            parameters[21].Value = model.contact_Tel;
            parameters[22].Value = model.contact_Mobile;
            parameters[23].Value = model.contact_Email;
            parameters[24].Value = model.contact_address;
            parameters[25].Value = model.contact_ZIP;
            parameters[26].Value = model.contact_msn;
            parameters[27].Value = model.service_content;
            parameters[28].Value = model.service_area;
            parameters[29].Value = model.service_workamount;
            parameters[30].Value = model.service_customization;
            parameters[31].Value = model.service_ohter;
            parameters[32].Value = model.business_price;
            parameters[33].Value = model.business_paytime;
            parameters[34].Value = model.business_prepay;
            parameters[35].Value = model.evaluation_department;
            parameters[36].Value = model.evaluation_level;
            parameters[37].Value = model.evaluation_feedback;
            parameters[38].Value = model.evaluation_note;
            parameters[39].Value = model.account_name;
            parameters[40].Value = model.account_bank;
            parameters[41].Value = model.account_number;
            parameters[42].Value = model.introfile;
            parameters[43].Value = model.productfile;
            parameters[44].Value = model.pricefile;
            parameters[45].Value = model.filialeamount;
            parameters[46].Value = model.filialeaddress;
            parameters[47].Value = model.Credit;
            parameters[48].Value = model.Cachet;
            parameters[49].Value = model.Money;
            parameters[50].Value = model.IsPerson;
            parameters[51].Value = model.CreatTime;
            parameters[52].Value = model.CreatIP;
            parameters[53].Value = model.LastUpdateTime;
            parameters[54].Value = model.LastUpdateIP;
            parameters[55].Value = model.Type;
            parameters[56].Value = model.Status;

            DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public void Delete(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete SC_SupplierHistory ");
            strSql.Append(" where id=@id");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)
				};
            parameters[0].Value = id;
            DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public SC_SupplierHistory GetModel(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from SC_SupplierHistory ");
            strSql.Append(" where id=@id");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)};
            parameters[0].Value = id;
            SC_SupplierHistory model = new SC_SupplierHistory();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            model.id = id;
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["Supplierid"].ToString() != "")
                {
                    model.Supplierid = int.Parse(ds.Tables[0].Rows[0]["Supplierid"].ToString());
                }
                if (ds.Tables[0].Rows[0]["HistoryEdition"].ToString() != "")
                {
                    model.HistoryEdition = int.Parse(ds.Tables[0].Rows[0]["HistoryEdition"].ToString());
                }
                //if (ds.Tables[0].Rows[0]["HistoryInfomation"].ToString() != "")
                //{
                //    model.HistoryInfomation = (byte[])ds.Tables[0].Rows[0]["HistoryInfomation"].ToString();
                //}
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
            strSql.Append("select * ");
            strSql.Append(" FROM SC_SupplierHistory ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }


        public List<SC_SupplierHistory> GetList(string strWhere, SqlParameter[] parameters)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append(" FROM SC_SupplierHistory ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return ESP.ConfigCommon.CBO.FillCollection<SC_SupplierHistory>(DbHelperSQL.Query(strSql.ToString(), parameters));
        }

        #endregion  成员方法
    }
}
