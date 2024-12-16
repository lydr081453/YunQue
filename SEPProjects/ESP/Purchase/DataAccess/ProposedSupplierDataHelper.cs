using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using ESP.Purchase.Common;
using ESP.Purchase.Entity;

namespace ESP.Purchase.DataAccess
{
    /// <summary>
    /// 数据访问类ProposedSupplierDataHelper。
    /// </summary>
    public class ProposedSupplierDataHelper
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ProposedSupplierDataHelper"/> class.
        /// </summary>
        public ProposedSupplierDataHelper()
        { }

        #region  成员方法
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public static bool Exists(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from T_ProposedSupplier");
            strSql.Append(" where id=@id ");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)};
            parameters[0].Value = id;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public static int Add(ProposedSupplierInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into T_ProposedSupplier(");
            strSql.Append("supplier_name,supplier_area,supplier_industry,supplier_scale,supplier_principal,supplier_builttime,supplier_website,supplier_source,supplier_frameNO,contact_name,contact_tel,contact_mobile,contact_email,contact_fax,contact_address,service_content,service_area,service_workamount,service_customization,service_ohter,business_price,business_paytime,business_prepay,evaluation_department,evaluation_level,evaluation_feedback,evaluation_note,account_name,account_bank,account_number,service_forshunya)");
            strSql.Append(" values (");
            strSql.Append("@supplier_name,@supplier_area,@supplier_industry,@supplier_scale,@supplier_principal,@supplier_builttime,@supplier_website,@supplier_source,@supplier_frameNO,@contact_name,@contact_tel,@contact_mobile,@contact_email,@contact_fax,@contact_address,@service_content,@service_area,@service_workamount,@service_customization,@service_ohter,@business_price,@business_paytime,@business_prepay,@evaluation_department,@evaluation_level,@evaluation_feedback,@evaluation_note,@account_name,@account_bank,@account_number,@service_forshunya)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@supplier_name", SqlDbType.NVarChar, 200),
					new SqlParameter("@supplier_area", SqlDbType.NVarChar, 100),
					new SqlParameter("@supplier_industry", SqlDbType.NVarChar, 100),
					new SqlParameter("@supplier_scale", SqlDbType.NVarChar, 100),
					new SqlParameter("@supplier_principal", SqlDbType.NVarChar, 50),
					new SqlParameter("@supplier_builttime", SqlDbType.NVarChar, 50),
					new SqlParameter("@supplier_website", SqlDbType.NVarChar, 100),
					new SqlParameter("@supplier_source", SqlDbType.NVarChar, 50),
					new SqlParameter("@supplier_frameNO", SqlDbType.NVarChar, 50),
					new SqlParameter("@contact_name", SqlDbType.NVarChar, 50),
					new SqlParameter("@contact_tel", SqlDbType.NVarChar, 50),
					new SqlParameter("@contact_mobile", SqlDbType.NVarChar, 50),
					new SqlParameter("@contact_email", SqlDbType.NVarChar, 50),
					new SqlParameter("@contact_fax", SqlDbType.NVarChar, 50),
					new SqlParameter("@contact_address", SqlDbType.NVarChar, 200),
					new SqlParameter("@service_content", SqlDbType.NVarChar, 1000),
					new SqlParameter("@service_area", SqlDbType.NVarChar, 500),
					new SqlParameter("@service_workamount", SqlDbType.NVarChar, 500),
					new SqlParameter("@service_customization", SqlDbType.NVarChar, 500),
					new SqlParameter("@service_ohter", SqlDbType.NVarChar, 2000),
					new SqlParameter("@business_price", SqlDbType.NVarChar, 100),
					new SqlParameter("@business_paytime", SqlDbType.NVarChar, 50),
					new SqlParameter("@business_prepay", SqlDbType.NVarChar, 50),
					new SqlParameter("@evaluation_department", SqlDbType.NVarChar, 500),
					new SqlParameter("@evaluation_level", SqlDbType.NVarChar, 500),
					new SqlParameter("@evaluation_feedback", SqlDbType.NVarChar, 2000),
					new SqlParameter("@evaluation_note", SqlDbType.NVarChar, 2000),
					new SqlParameter("@account_name", SqlDbType.NVarChar, 100),
					new SqlParameter("@account_bank", SqlDbType.NVarChar, 100),
					new SqlParameter("@account_number", SqlDbType.NVarChar, 100),
                    new SqlParameter("@service_forshunya",SqlDbType.NVarChar,2000)};
            parameters[0].Value = model.supplier_name;
            parameters[1].Value = model.supplier_area;
            parameters[2].Value = model.supplier_industry;
            parameters[3].Value = model.supplier_scale;
            parameters[4].Value = model.supplier_principal;
            parameters[5].Value = model.supplier_builttime;
            parameters[6].Value = model.supplier_website;
            parameters[7].Value = model.supplier_source;
            parameters[8].Value = model.supplier_frameNO;
            parameters[9].Value = model.contact_name;
            parameters[10].Value = model.contact_tel;
            parameters[11].Value = model.contact_mobile;
            parameters[12].Value = model.contact_email;
            parameters[13].Value = model.contact_fax;
            parameters[14].Value = model.contact_address;
            parameters[15].Value = model.service_content;
            parameters[16].Value = model.service_area;
            parameters[17].Value = model.service_workamount;
            parameters[18].Value = model.service_customization;
            parameters[19].Value = model.service_ohter;
            parameters[20].Value = model.business_price;
            parameters[21].Value = model.business_paytime;
            parameters[22].Value = model.business_prepay;
            parameters[23].Value = model.evaluation_department;
            parameters[24].Value = model.evaluation_level;
            parameters[25].Value = model.evaluation_feedback;
            parameters[26].Value = model.evaluation_note;
            parameters[27].Value = model.account_name;
            parameters[28].Value = model.account_bank;
            parameters[29].Value = model.account_number;
            parameters[30].Value = model.service_forshunya;

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
        public static int Update(ProposedSupplierInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update T_ProposedSupplier set ");
            strSql.Append("supplier_name=@supplier_name,");
            strSql.Append("supplier_area=@supplier_area,");
            strSql.Append("supplier_industry=@supplier_industry,");
            strSql.Append("supplier_scale=@supplier_scale,");
            strSql.Append("supplier_principal=@supplier_principal,");
            strSql.Append("supplier_builttime=@supplier_builttime,");
            strSql.Append("supplier_website=@supplier_website,");
            strSql.Append("supplier_source=@supplier_source,");
            strSql.Append("supplier_frameNO=@supplier_frameNO,");
            strSql.Append("contact_name=@contact_name,");
            strSql.Append("contact_tel=@contact_tel,");
            strSql.Append("contact_mobile=@contact_mobile,");
            strSql.Append("contact_email=@contact_email,");
            strSql.Append("contact_fax=@contact_fax,");
            strSql.Append("contact_address=@contact_address,");
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
            strSql.Append("service_forshunya=@service_forshunya");
            strSql.Append(" where id=@id");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4),
					new SqlParameter("@supplier_name", SqlDbType.NVarChar, 200),
					new SqlParameter("@supplier_area", SqlDbType.NVarChar, 100),
					new SqlParameter("@supplier_industry", SqlDbType.NVarChar, 100),
					new SqlParameter("@supplier_scale", SqlDbType.NVarChar, 100),
					new SqlParameter("@supplier_principal", SqlDbType.NVarChar, 50),
					new SqlParameter("@supplier_builttime", SqlDbType.NVarChar, 50),
					new SqlParameter("@supplier_website", SqlDbType.NVarChar, 100),
					new SqlParameter("@supplier_source", SqlDbType.NVarChar, 50),
					new SqlParameter("@supplier_frameNO", SqlDbType.NVarChar, 50),
					new SqlParameter("@contact_name", SqlDbType.NVarChar, 50),
					new SqlParameter("@contact_tel", SqlDbType.NVarChar, 50),
					new SqlParameter("@contact_mobile", SqlDbType.NVarChar, 50),
					new SqlParameter("@contact_email", SqlDbType.NVarChar, 50),
					new SqlParameter("@contact_fax", SqlDbType.NVarChar, 50),
					new SqlParameter("@contact_address", SqlDbType.NVarChar, 200),
					new SqlParameter("@service_content", SqlDbType.NVarChar, 1000),
					new SqlParameter("@service_area", SqlDbType.NVarChar, 500),
					new SqlParameter("@service_workamount", SqlDbType.NVarChar, 500),
					new SqlParameter("@service_customization", SqlDbType.NVarChar, 500),
					new SqlParameter("@service_ohter", SqlDbType.NVarChar, 2000),
					new SqlParameter("@business_price", SqlDbType.NVarChar, 100),
					new SqlParameter("@business_paytime", SqlDbType.NVarChar, 50),
					new SqlParameter("@business_prepay", SqlDbType.NVarChar, 50),
					new SqlParameter("@evaluation_department", SqlDbType.NVarChar, 500),
					new SqlParameter("@evaluation_level", SqlDbType.NVarChar, 500),
					new SqlParameter("@evaluation_feedback", SqlDbType.NVarChar, 2000),
					new SqlParameter("@evaluation_note", SqlDbType.NVarChar, 2000),
					new SqlParameter("@account_name", SqlDbType.NVarChar, 100),
					new SqlParameter("@account_bank", SqlDbType.NVarChar, 100),
					new SqlParameter("@account_number", SqlDbType.NVarChar, 100),
                    new SqlParameter("@service_forshunya",SqlDbType.NVarChar,2000)};
            parameters[0].Value = model.id;
            parameters[1].Value = model.supplier_name;
            parameters[2].Value = model.supplier_area;
            parameters[3].Value = model.supplier_industry;
            parameters[4].Value = model.supplier_scale;
            parameters[5].Value = model.supplier_principal;
            parameters[6].Value = model.supplier_builttime;
            parameters[7].Value = model.supplier_website;
            parameters[8].Value = model.supplier_source;
            parameters[9].Value = model.supplier_frameNO;
            parameters[10].Value = model.contact_name;
            parameters[11].Value = model.contact_tel;
            parameters[12].Value = model.contact_mobile;
            parameters[13].Value = model.contact_email;
            parameters[14].Value = model.contact_fax;
            parameters[15].Value = model.contact_address;
            parameters[16].Value = model.service_content;
            parameters[17].Value = model.service_area;
            parameters[18].Value = model.service_workamount;
            parameters[19].Value = model.service_customization;
            parameters[20].Value = model.service_ohter;
            parameters[21].Value = model.business_price;
            parameters[22].Value = model.business_paytime;
            parameters[23].Value = model.business_prepay;
            parameters[24].Value = model.evaluation_department;
            parameters[25].Value = model.evaluation_level;
            parameters[26].Value = model.evaluation_feedback;
            parameters[27].Value = model.evaluation_note;
            parameters[28].Value = model.account_name;
            parameters[29].Value = model.account_bank;
            parameters[30].Value = model.account_number;
            parameters[31].Value = model.service_forshunya;

            return DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        /// <param name="id">The id.</param>
        public static void Delete(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete T_ProposedSupplier ");
            strSql.Append(" where id=@id ");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)};
            parameters[0].Value = id;
            DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns></returns>
        public static ProposedSupplierInfo GetModel(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from T_ProposedSupplier ");
            strSql.Append(" where id=@id");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)};
            parameters[0].Value = id;
            ProposedSupplierInfo model = new ProposedSupplierInfo();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            return setModel(ds);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        /// <param name="supplierName">供应商名称</param>
        /// <returns></returns>
        public static ProposedSupplierInfo GetModel(string supplierName)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from T_ProposedSupplier ");
            strSql.Append(" where supplier_name=@supplier_name");
            SqlParameter[] parameters = {
					new SqlParameter("@supplier_name", SqlDbType.NVarChar,200)};
            parameters[0].Value = supplierName;
            ProposedSupplierInfo model = new ProposedSupplierInfo();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            return setModel(ds);
        }

        /// <summary>
        /// Sets the model.
        /// </summary>
        /// <param name="ds">The ds.</param>
        /// <returns></returns>
        private static ProposedSupplierInfo setModel(DataSet ds)
        {
            ProposedSupplierInfo model = new ProposedSupplierInfo();
            if (ds.Tables[0].Rows.Count > 0)
            {
                model.id = int.Parse(ds.Tables[0].Rows[0]["id"].ToString());
                model.supplier_name = ds.Tables[0].Rows[0]["supplier_name"].ToString();
                model.supplier_area = ds.Tables[0].Rows[0]["supplier_area"].ToString();
                model.supplier_industry = ds.Tables[0].Rows[0]["supplier_industry"].ToString();
                model.supplier_scale = ds.Tables[0].Rows[0]["supplier_scale"].ToString();
                model.supplier_principal = ds.Tables[0].Rows[0]["supplier_principal"].ToString();
                model.supplier_builttime = ds.Tables[0].Rows[0]["supplier_builttime"].ToString();
                model.supplier_website = ds.Tables[0].Rows[0]["supplier_website"].ToString();
                model.supplier_source = ds.Tables[0].Rows[0]["supplier_source"].ToString();
                model.supplier_frameNO = ds.Tables[0].Rows[0]["supplier_frameNO"].ToString();
                model.contact_name = ds.Tables[0].Rows[0]["contact_name"].ToString();
                model.contact_tel = ds.Tables[0].Rows[0]["contact_tel"].ToString();
                model.contact_mobile = ds.Tables[0].Rows[0]["contact_mobile"].ToString();
                model.contact_email = ds.Tables[0].Rows[0]["contact_email"].ToString();
                model.contact_fax = ds.Tables[0].Rows[0]["contact_fax"].ToString();
                model.contact_address = ds.Tables[0].Rows[0]["contact_address"].ToString();
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
                model.service_forshunya = ds.Tables[0].Rows[0]["service_forshunya"].ToString();
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
        /// <param name="strWhere">The STR where.</param>
        /// <param name="parms">The parms.</param>
        /// <returns></returns>
        public static DataSet GetList(string strWhere, List<SqlParameter> parms)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select [id],[supplier_name],[supplier_area],[supplier_industry],[supplier_scale],[supplier_principal],[supplier_builttime],[supplier_website],[supplier_source],[supplier_frameNO],[contact_name],[contact_tel],[contact_mobile],[contact_email],[contact_fax],[contact_address],[service_content],[service_area],[service_workamount],[service_customization],[service_ohter],[business_price],[business_paytime],[business_prepay],[evaluation_department],[evaluation_level],[evaluation_feedback],[evaluation_note],[account_name],[account_bank],[account_number],[service_forshunya] ");
            strSql.Append(" FROM T_ProposedSupplier ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by supplier_name");
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// Gets the model list.
        /// </summary>
        /// <param name="terms">The terms.</param>
        /// <param name="parms">The parms.</param>
        /// <returns></returns>
        public static List<ProposedSupplierInfo> getModelList(string terms, List<SqlParameter> parms)
        {
            List<ProposedSupplierInfo> list = new List<ProposedSupplierInfo>();
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" select * from T_ProposedSupplier ");
            strSql.Append(" where 1=1 {0} order by supplier_name");
            string sql = string.Format(strSql.ToString(), terms);
            using (SqlDataReader r = DbHelperSQL.ExecuteReader(sql, parms))
            {
                while (r.Read())
                {
                    ProposedSupplierInfo c = new ProposedSupplierInfo();
                    c.PopupData(r);
                    list.Add(c);
                }
                r.Close();
            }
            return list;
        }

        /// <summary>
        /// Gets the top model list.
        /// </summary>
        /// <returns></returns>
        public static List<ProposedSupplierInfo> getTopModelList()
        {
            List<ProposedSupplierInfo> list = new List<ProposedSupplierInfo>();
            string sql = "select Top 5 * from T_ProposedSupplier order by id desc ";
            using (SqlDataReader r = DbHelperSQL.ExecuteReader(sql))
            {
                while (r.Read())
                {
                    ProposedSupplierInfo c = new ProposedSupplierInfo();
                    c.PopupData(r);
                    list.Add(c);
                }
                r.Close();
            }
            return list;
        }

        /// <summary>
        /// 根据物料类型ID获得供应商
        /// </summary>
        /// <param name="productTypeId"></param>
        /// <param name="terms"></param>
        /// <param name="parms"></param>
        /// <returns></returns>
        public static List<ProposedSupplierInfo> getSupplierListByProductTypeId(int productTypeId, string terms, List<SqlParameter> parms)
        {
            List<ProposedSupplierInfo> list = new List<ProposedSupplierInfo>();
            string strSql = @"select a.* from T_ProposedSupplier as a 
                                inner join (select distinct supplierid from t_product where productType=@productType) as b on a.id=b.supplierid
                                where 1=1 ";
            if (parms == null)
            {
                parms = new List<SqlParameter>();
            }
            parms.Add(new SqlParameter("@productType", productTypeId));
            if (terms != "")
            {
                strSql += terms;
            }
            using (SqlDataReader r = DbHelperSQL.ExecuteReader(strSql, parms.ToArray()))
            {
                while (r.Read())
                {
                    ProposedSupplierInfo c = new ProposedSupplierInfo();
                    c.PopupData(r);
                    list.Add(c);
                }
                r.Close();
            }
            return list;
        }

        /// <summary>
        /// 根据物料类型ID集合获得供应商
        /// </summary>
        /// <param name="productTypeId"></param>
        /// <param name="terms"></param>
        /// <param name="parms"></param>
        /// <returns></returns>
        public static List<ProposedSupplierInfo> getSupplierListByProductTypeIds(string productTypes, string terms, List<SqlParameter> parms)
        {
            List<ProposedSupplierInfo> list = new List<ProposedSupplierInfo>();
            string strSql = @"select a.* from T_ProposedSupplier as a 
                                inner join (select distinct supplierid from t_product where productType in (@productType)) as b on a.id=b.supplierid
                                where 1=1 ";
            if (parms == null)
            {
                parms = new List<SqlParameter>();
            }
            parms.Add(new SqlParameter("@productType", productTypes));
            if (terms != "")
                strSql += terms;
            using (SqlDataReader r = DbHelperSQL.ExecuteReader(strSql, parms.ToArray()))
            {
                while (r.Read())
                {
                    ProposedSupplierInfo c = new ProposedSupplierInfo();
                    c.PopupData(r);
                    list.Add(c);
                }
                r.Close();
            }
            return list;
        }

        /// <summary>
        /// Gets the supplier list by type names.
        /// </summary>
        /// <param name="productTypes">The product types.</param>
        /// <param name="terms">The terms.</param>
        /// <param name="parms">The parms.</param>
        /// <returns></returns>
        public static List<ProposedSupplierInfo> getSupplierListByTypeNames(string productTypes, string terms, List<SqlParameter> parms)
        {
            string sel = string.Format(@"select c.typeid  from t_type c inner join (
                                            select a.typeid,a.typeid as level2,b.level1 from t_type a inner join (
                                            select typeid,typeid as level1 from t_type where parentid=0 ) b on a.parentid=b.typeid ) d on c.parentid=d.typeid
                                            where d.level1 in (select typeid from t_type where typename like '%{0}%')
                                            union
                                            select c.typeid from t_type c inner join (
                                            select a.typeid,a.typeid as level2,b.level1 from t_type a inner join (
                                            select typeid,typeid as level1 from t_type where parentid=0 ) b on a.parentid=b.typeid ) d on c.parentid=d.typeid
                                            where d.level2 in (select typeid from t_type where typename like '%{0}%')
                                            union 
                                            select c.typeid from t_type c inner join (
                                            select a.typeid,a.typeid as level2,b.level1 from t_type a inner join (
                                            select typeid,typeid as level1 from t_type where parentid=0 ) b on a.parentid=b.typeid ) d on c.parentid=d.typeid
                                            where c.typeid in (select typeid from t_type where typename like '%{0}%')", productTypes);
            List<ProposedSupplierInfo> list = new List<ProposedSupplierInfo>();
            string strSql = string.Format("select a.* from T_ProposedSupplier as a  inner join (select distinct supplierid from t_product where productType in ({0})) as b on a.id=b.supplierid where 1=1 ", sel);
            if (parms == null)
            {
                parms = new List<SqlParameter>();
            }
            if (terms != "")
            {
                strSql += terms;
            }
            using (SqlDataReader r = DbHelperSQL.ExecuteReader(strSql, parms.ToArray()))
            {
                while (r.Read())
                {
                    ProposedSupplierInfo c = new ProposedSupplierInfo();
                    c.PopupData(r);
                    list.Add(c);
                }
                r.Close();
            }
            return list;
        }
        #endregion  成员方法
    }
}