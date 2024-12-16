using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using System.Collections.Generic;
using ESP.Purchase.Common;
using ESP.Purchase.Entity;

namespace ESP.Purchase.DataAccess
{
    /// <summary>
    /// 数据访问类SupplierDataProvider
    /// </summary>
    public class SupplierDataProvider 
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SupplierDataProvider"/> class.
        /// </summary>
        public SupplierDataProvider()
        { }

        #region  成员方法

        public static int Add(SupplierInfo model)
        {
            return Add(model, null, null);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public static int Add(SupplierInfo model,SqlConnection conn, SqlTransaction trans)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into T_Supplier(");
            strSql.Append("supplier_name,supplier_area,supplier_industry,supplier_scale,supplier_principal,supplier_builttime,supplier_website,supplier_source,supplier_frameNO,contact_name,contact_tel,contact_mobile,contact_email,contact_fax,contact_address,service_content,service_area,service_workamount,service_customization,service_ohter,business_price,business_paytime,business_prepay,evaluation_department,evaluation_level,evaluation_feedback,evaluation_note,account_name,account_bank,account_number,service_forshunya,supplier_type,supplier_status,general_id,payment_tax,CostRate)");
            strSql.Append(" values (");
            strSql.Append("@supplier_name,@supplier_area,@supplier_industry,@supplier_scale,@supplier_principal,@supplier_builttime,@supplier_website,@supplier_source,@supplier_frameNO,@contact_name,@contact_tel,@contact_mobile,@contact_email,@contact_fax,@contact_address,@service_content,@service_area,@service_workamount,@service_customization,@service_ohter,@business_price,@business_paytime,@business_prepay,@evaluation_department,@evaluation_level,@evaluation_feedback,@evaluation_note,@account_name,@account_bank,@account_number,@service_forshunya,@supplier_type,@supplier_status,@general_id,@payment_tax,@CostRate)");
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
                    new SqlParameter("@service_forshunya",SqlDbType.NVarChar,2000),
                    new SqlParameter("@supplier_type",SqlDbType.Int),
                    new SqlParameter("@supplier_status",SqlDbType.Int),
                    new SqlParameter("@general_id",SqlDbType.Int),
                    new SqlParameter("@payment_tax",SqlDbType.Decimal),
                    new SqlParameter("@CostRate",SqlDbType.Decimal,9),
                                        };
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
            parameters[31].Value = model.supplier_type;
            parameters[32].Value = model.supplier_status;
            parameters[33].Value = model.general_id;
            parameters[34].Value = model.Payment_Tax;
            parameters[35].Value = model.CostRate;

            object obj = DbHelperSQL.GetSingle(strSql.ToString(),conn,trans, parameters);
            if (obj == null)
            {
                return 1;
            }
            else
            {
                return Convert.ToInt32(obj);
            }
        }
        public static int Update(SupplierInfo model)
        {
            return Update(model, null, null);
        }
        /// <summary>
        /// 更新一条数据
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public static int Update(SupplierInfo model,SqlConnection conn, SqlTransaction trans)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update T_Supplier set ");
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
            strSql.Append("service_forshunya=@service_forshunya,");
            strSql.Append("supplier_type=@supplier_type,");
            strSql.Append("supplier_status=@supplier_status");
            strSql.Append(",general_id=@general_id,payment_tax=@payment_tax,CostRate=@CostRate");
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
                    new SqlParameter("@service_forshunya",SqlDbType.NVarChar,2000),
                    new SqlParameter("@supplier_type",SqlDbType.Int,4),
                    new SqlParameter("@supplier_status",SqlDbType.Int,4),
                    new SqlParameter("@general_id",SqlDbType.Int,4),
                    new SqlParameter("@payment_tax",SqlDbType.Decimal,20),
                    new SqlParameter("@CostRate",SqlDbType.Decimal,9),
                                        };
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
            parameters[32].Value = model.supplier_type;
            parameters[33].Value = model.supplier_status;
            parameters[34].Value = model.general_id;
            parameters[35].Value = model.Payment_Tax;
            parameters[36].Value = model.CostRate;
            return DbHelperSQL.ExecuteSql(strSql.ToString(),conn,trans, parameters);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        /// <param name="id">The id.</param>
        public static void Delete(int id)
        {
            StringBuilder strSql = new StringBuilder();
            //strSql.Append("delete T_Supplier ");
            strSql.Append(" update T_Supplier set ");
            strSql.Append(" supplier_status=@supplier_status ");
            strSql.Append(" where id=@id ");
            SqlParameter[] parameters = {
                    new SqlParameter("@supplier_status",SqlDbType.Int,4),
                    new SqlParameter("@id", SqlDbType.Int,4)};
            parameters[0].Value = State.supplierstatus_block;
            parameters[1].Value = id;

            DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns></returns>
        public static SupplierInfo GetModel(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from T_Supplier ");
            strSql.Append(" where id=@id");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)};
            parameters[0].Value = id;
            SupplierInfo model = new SupplierInfo();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            return setModel(ds);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        /// <param name="supplierName">供应商名称</param>
        /// <returns></returns>
        public static SupplierInfo GetModel(string supplierName,int type)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from T_Supplier ");
            strSql.Append(" where supplier_name=@supplier_name");
            List<SqlParameter> parms = new List<SqlParameter>();
            parms.Add(new SqlParameter("@supplier_name", supplierName));
            if (type != -1)
            {
                strSql.Append(" and supplier_Type=@type and supplier_Status=1");
                parms.Add(new SqlParameter("@type", type));
            }
            SupplierInfo model = new SupplierInfo();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parms.ToArray());
            return setModel(ds);
        }

        /// <summary>
        /// Sets the model.
        /// </summary>
        /// <param name="ds">The ds.</param>
        /// <returns></returns>
        private static SupplierInfo setModel(DataSet ds)
        {
            SupplierInfo model = new SupplierInfo();
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
                model.supplier_type = ds.Tables[0].Rows[0]["supplier_type"] == DBNull.Value ? 1 : int.Parse(ds.Tables[0].Rows[0]["supplier_type"].ToString());
                model.supplier_status = ds.Tables[0].Rows[0]["supplier_status"] == DBNull.Value ? 1 : int.Parse(ds.Tables[0].Rows[0]["supplier_status"].ToString());
                model.general_id = ds.Tables[0].Rows[0]["general_id"] == DBNull.Value ? 0 : int.Parse(ds.Tables[0].Rows[0]["general_id"].ToString());
                model.Payment_Tax = ds.Tables[0].Rows[0]["payment_tax"] == DBNull.Value ? 0 : decimal.Parse(ds.Tables[0].Rows[0]["payment_tax"].ToString());
                model.CostRate = ds.Tables[0].Rows[0]["CostRate"] == DBNull.Value ? 0 : decimal.Parse(ds.Tables[0].Rows[0]["CostRate"].ToString());
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
            strSql.Append("select '' as supplyName,'' as supplyId,[id],[supplier_name],[supplier_area],[supplier_industry],[supplier_scale],[supplier_principal],[supplier_builttime],[supplier_website],[supplier_source],[supplier_frameNO],[contact_name],[contact_tel],[contact_mobile],[contact_email],[contact_fax],[contact_address],[service_content],[service_area],[service_workamount],[service_customization],[service_ohter],[business_price],[business_paytime],[business_prepay],[evaluation_department],[evaluation_level],[evaluation_feedback],[evaluation_note],[account_name],[account_bank],[account_number],[service_forshunya] ,[supplier_status],[general_id],[payment_tax],[CostRate] ");
            strSql.Append(" FROM T_Supplier where supplier_status = "+State.supplierstatus_used.ToString());
            if (strWhere.Trim() != "")
            {
                strSql.Append(strWhere);
            }
            strSql.Append(" order by supplier_name");
            return DbHelperSQL.Query(strSql.ToString());
        }

        public static List<SupplierInfo> getModelList(string terms, List<SqlParameter> parms)
        {
            return getModelList(terms, parms, false);
        }

        /// <summary>
        /// 获取供应链平台供应商（添加采购物品新改动）
        /// </summary>
        /// <param name="terms"></param>
        /// <param name="parms"></param>
        /// <returns></returns>
        public static DataTable getLinkSupplySupplierList(string terms,int typeId, List<SqlParameter> parms,string typeName)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"select (case when c.supplier_type is null or c.supplier_type=0 then 99 else c.supplier_type end) supplierType ,a.id as supplyId,a.supplier_name as supplyName,c.*,
                            (
                                select count(a1.typeid) from t_type as a1 
                                inner join t_product as b1 on b1.producttype=a1.typeid
                                where b1.isshow =1 and b1.supplierid=c.id and a1.typeid=" + typeId + (typeName != "" ? (" and b1.productname like '%" + typeName + "%'") : "") + @"
                            )
                            as productNum from sc_supplier as a 
                            left join t_espandsupplysuppliersrelation as b on a.id=b.supplysupplierid
                            left join t_supplier as c on b.espsupplierid=c.id and c.supplier_status = "+State.supplierstatus_used.ToString()+@"
                            where a.status in (3,4,5)");
            if (typeId != 0)
            {
                strSql.Append(@" and a.id in (select distinct supplierid from sc_suppliertype where typelv=3 and typeid in (
                                    select supplytypeid from t_type as a
                                    inner join T_ESPAndSupplyTypeRelation as b on a.typeid=esptypeid
                                    where a.status<>0  and a.typeid="+typeId+"))");
            }
            if (terms.Trim() != "")
                strSql.Append(terms);
            strSql.Append(" order by productNum desc,supplierType ");
            return DbHelperSQL.Query(strSql.ToString(), parms.ToArray()).Tables[0];
        }

        public static int insertSupplierAndLinkSupply(GeneralInfo g, SupplierInfo model,int supplyId)
        {
            using (SqlConnection conn = new SqlConnection(Common.DbHelperSQL.connectionString))
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();
                try
                {
                    int newId = SupplierDataProvider.Add(model, conn, trans);
                    ESP.Purchase.DataAccess.GeneralInfoDataProvider.Update(g, conn, trans);
                    string insertSql = "insert t_espandsupplysuppliersrelation values('"+newId+"','"+supplyId+"','"+DateTime.Now+"','0')";
                    DbHelperSQL.ExecuteSql(insertSql, conn, trans);
                    trans.Commit();
                    return newId;
                }
                catch { trans.Rollback(); return 0; }
            }
        }

        /// <summary>
        /// Gets the model list.
        /// </summary>
        /// <param name="terms">The terms.</param>
        /// <param name="parms">The parms.</param>
        /// <returns></returns>
        public static List<SupplierInfo> getModelList(string terms, List<SqlParameter> parms, bool isAll)
        {
            List<SupplierInfo> list = new List<SupplierInfo>();
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" select * from t_supplier ");
            strSql.Append(" where 1=1 {0} {1} order by supplier_name");
            string sql = string.Format(strSql.ToString(), (isAll ? "" : " and supplier_status= " + State.supplierstatus_used.ToString()), terms);
            using (SqlDataReader r = DbHelperSQL.ExecuteReader(sql, parms))
            {
                while (r.Read())
                {
                    SupplierInfo c = new SupplierInfo();
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
        /// <param name="strWhere">The STR where.</param>
        /// <returns></returns>
        public static List<SupplierInfo> getTopModelList(string strWhere)
        {
            List<SupplierInfo> list = new List<SupplierInfo>();
            string sql = "select Top 5 * from t_supplier where 1=1 and supplier_status="+State.typestatus_used.ToString()+" " + strWhere + " order by supplier_name ";
            using (SqlDataReader r = DbHelperSQL.ExecuteReader(sql))
            {
                while (r.Read())
                {
                    SupplierInfo c = new SupplierInfo();
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
        public static List<SupplierInfo> getSupplierListByProductTypeId(int productTypeId, string terms, List<SqlParameter> parms)
        {
            List<SupplierInfo> list = new List<SupplierInfo>();
            string strSql = @"select a.* from t_supplier as a 
                                inner join (select distinct supplierid from t_product where productType=@productType) as b on a.id=b.supplierid
                                where 1=1 and  supplier_status =@supplier_status ";
            if(parms == null)
                parms = new List<SqlParameter>();
            parms.Add(new SqlParameter("@productType", productTypeId));
            parms.Add(new SqlParameter("@supplier_status",State.typestatus_used));
            if (terms != "")
                strSql += terms;
            using (SqlDataReader r = DbHelperSQL.ExecuteReader(strSql,parms.ToArray()))
            {
                while (r.Read())
                {
                    SupplierInfo c = new SupplierInfo();
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
        /// <param name="productTypes">The product types.</param>
        /// <param name="terms">The terms.</param>
        /// <param name="parms">The parms.</param>
        /// <returns></returns>
        public static List<SupplierInfo> getSupplierListByProductTypeIds(string productTypes, string terms, List<SqlParameter> parms)
        {
            List<SupplierInfo> list = new List<SupplierInfo>();
            string strSql = @"select a.* from t_supplier as a 
                                inner join (select distinct supplierid from t_product where productType in (@productType)) as b on a.id=b.supplierid
                                where 1=1  and  supplier_status =@supplier_status";
            if (parms == null)
                parms = new List<SqlParameter>();
            parms.Add(new SqlParameter("@productType", productTypes));
            parms.Add(new SqlParameter("@supplier_status", State.typestatus_used));
            if (terms != "")
                strSql += terms;
            using (SqlDataReader r = DbHelperSQL.ExecuteReader(strSql, parms.ToArray()))
            {
                while (r.Read())
                {
                    SupplierInfo c = new SupplierInfo();
                    c.PopupData(r);
                    list.Add(c);
                }
                r.Close();
            }
            return list;
        }

        public static List<SupplierInfo> getSupplierListByTypeNames(string productTypes, string terms, List<SqlParameter> parms)
        {
            return getSupplierListByTypeNames(productTypes, terms, parms, false);
        }

        /// <summary>
        /// Gets the supplier list by type names.
        /// </summary>
        /// <param name="productTypes">The product types.</param>
        /// <param name="terms">The terms.</param>
        /// <param name="parms">The parms.</param>
        /// <returns></returns>
        public static List<SupplierInfo> getSupplierListByTypeNames(string productTypes, string terms, List<SqlParameter> parms,bool isAll)
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
                                            where c.typeid in (select typeid from t_type where typename like '%{0}%')",productTypes);
            List<SupplierInfo> list = new List<SupplierInfo>();
            string strSql = string.Format("select a.* from t_supplier as a  inner join (select distinct supplierid from t_product where productType in ({0})) as b on a.id=b.supplierid where 1=1 {1}", sel,(isAll ? "" : "and a.supplier_status=" + State.supplierstatus_used.ToString()));
            if (parms == null)
                parms = new List<SqlParameter>();
            
            if (terms != "")
                strSql += terms;
            using (SqlDataReader r = DbHelperSQL.ExecuteReader(strSql, parms.ToArray()))
            {
                while (r.Read())
                {
                    SupplierInfo c = new SupplierInfo();
                    c.PopupData(r);
                    list.Add(c);
                }
                r.Close();
            }
            return list;
        }

        public static List<SupplierInfo> getSupplierListByAuditorId(int auditorId, string terms, List<SqlParameter> parms, bool isAll)
        {
            string sel = string.Format(@"select c.typeid  from t_type c inner join (
                                            select a.typeid,a.typeid as level2,b.level1 from t_type a inner join (
                                            select typeid,typeid as level1 from t_type where parentid=0 ) b on a.parentid=b.typeid ) d on c.parentid=d.typeid
                                            where d.level1 in (select typeid from t_type where (auditorid={0} or shauditorid={0} or gzauditorid={0}) and status=1)
                                            union
                                            select c.typeid from t_type c inner join (
                                            select a.typeid,a.typeid as level2,b.level1 from t_type a inner join (
                                            select typeid,typeid as level1 from t_type where parentid=0 ) b on a.parentid=b.typeid ) d on c.parentid=d.typeid
                                            where d.level2 in (select typeid from t_type where (auditorid={0} or shauditorid={0} or gzauditorid={0}) and status=1)
                                            union 
                                            select c.typeid from t_type c inner join (
                                            select a.typeid,a.typeid as level2,b.level1 from t_type a inner join (
                                            select typeid,typeid as level1 from t_type where parentid=0 ) b on a.parentid=b.typeid ) d on c.parentid=d.typeid
                                            where c.typeid in (select typeid from t_type where (auditorid={0} or shauditorid={0} or gzauditorid={0}) and status=1)", auditorId);
            List<SupplierInfo> list = new List<SupplierInfo>();
            string strSql = string.Format("select a.* from t_supplier as a  inner join (select distinct supplierid from t_product where productType in ({0})) as b on a.id=b.supplierid where 1=1 {1}", sel, (isAll ? "" : "and a.supplier_status=" + State.supplierstatus_used.ToString()));
            if (parms == null)
                parms = new List<SqlParameter>();

            if (terms != "")
                strSql += terms;
            using (SqlDataReader r = DbHelperSQL.ExecuteReader(strSql, parms.ToArray()))
            {
                while (r.Read())
                {
                    SupplierInfo c = new SupplierInfo();
                    c.PopupData(r);
                    list.Add(c);
                }
                r.Close();
            }
            return list;
        }

        /// <summary>
        ///  变更供应商类型
        /// </summary>
        /// <param name="supplierId"></param>
        /// <param name="changedType"></param>
        public static void ChangedSupplierType(int supplierId, int changedType, int isShow)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" update t_supplier set supplier_type=@supplierType where id=@supplierId;");
            strSql.Append(" update t_product set isShow=@isShow where supplierId=@supplierId");
            SqlParameter[] parms = { 
                    new SqlParameter("@supplierType",SqlDbType.Int,4),
                    new SqlParameter("@supplierId",SqlDbType.Int,4),
                    new SqlParameter("@isShow",SqlDbType.Int,4)
            };
            parms[0].Value = changedType;
            parms[1].Value = supplierId;
            parms[2].Value = isShow;
            DbHelperSQL.ExecuteSql(strSql.ToString(), parms);
        }


        #endregion  成员方法


        #region 同步供应商

        /// <summary>
        /// 获取供应商同步列表
        /// </summary>
        /// <param name="where"></param>
        /// <param name="parms"></param>
        /// <returns></returns>
        public static DataTable GetSyncSupplierList(string where,List<SqlParameter> parms)
        {
            if (parms == null)
                parms = new List<SqlParameter>();
            string strWhere = @"select c.id as espId, a.*,b.id as RealationId,c.supplier_frameno from sc_supplier as a
                                    left join T_ESPAndSupplySuppliersRelation as b on b.supplysupplierid=a.id
                                    left join t_supplier as c on c.id=b.espsupplierid";
            strWhere += " where a.status in (2,3,4,5)" + where;

            return DbHelperSQL.Query(strWhere, parms.ToArray()).Tables[0];
        }

        /// <summary>
        /// 同步供应商
        /// </summary>
        /// <param name="Emodel"></param>
        /// <param name="Smodel"></param>
        /// <param name="relationId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public static DataTable SyncSupplier(ESP.Purchase.Entity.SupplierInfo Emodel, ESP.Supplier.Entity.SC_Supplier Smodel, int relationId,int userId)
        {
            using (SqlConnection conn = new SqlConnection(DbHelperSQL.connectionString))
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();
                try
                {
                    //保存采购部供应商
                    int Eid = Emodel.id;
                    if (Emodel.id == 0)
                    {
                        Emodel.supplier_type = 3;
                        Emodel.supplier_status = 1;
                        Emodel.supplier_source = "临时供应商";
                        Eid = Add(Emodel, conn, trans);
                    }
                    else
                    {
                        Update(Emodel, conn, trans);
                    }

                    //创建关联
                    if (relationId == 0)
                    {
                        StringBuilder strSql = new StringBuilder();
                        strSql.Append("insert into T_ESPAndSupplySuppliersRelation(");
                        strSql.Append("ESPSupplierId,SupplySupplierId,CreatedDate,CreatedUserId)");
                        strSql.Append(" values (");
                        strSql.Append("@ESPSupplierId,@SupplySupplierId,@CreatedDate,@CreatedUserId)");
                        strSql.Append(";select @@IDENTITY");
                        SqlParameter[] parameters = {
					            new SqlParameter("@ESPSupplierId", SqlDbType.Int,4),
					            new SqlParameter("@SupplySupplierId", SqlDbType.Int,4),
					            new SqlParameter("@CreatedDate", SqlDbType.DateTime),
					            new SqlParameter("@CreatedUserId", SqlDbType.Int,4)};
                        parameters[0].Value = Eid;
                        parameters[1].Value = Smodel.id;
                        parameters[2].Value = DateTime.Now;
                        parameters[3].Value = userId;

                        object obj = DbHelperSQL.GetSingle(strSql.ToString(),conn,trans, parameters);
                        relationId = Convert.ToInt32(obj);
                    }
                    trans.Commit();
                    return DbHelperSQL.Query("select * from T_ESPAndSupplySuppliersRelation where id=" + relationId).Tables[0];
                }
                catch { trans.Rollback(); return null; }
            }
        }
         

        #endregion

        /// <summary>
        /// 推荐，一般次序；分值高低排序，按地域，北上广
        /// </summary>
        /// <param name="strWhere"></param>
        /// <param name="parms"></param>
        /// <returns></returns>
        public static DataTable getSupplierListOrderByFeiXieYi(string strWhere, List<SqlParameter> parms, string typeid)
        {
            string sql = @"select * from (select distinct ISNULL( goo.orderamount,0) as orderamount,
0 as goodAmount,0 as normalAmount,0 as badAmount,0 as countnum,
c2.id,0 as supplyId,'' as logName,c2.supplier_frameno,'' as supplier_code,
c2.supplier_name,'' as PriceLevel,c2.supplier_area as supplier_province,c2.supplier_area as supplier_city,
0 as isperson,c2.contact_Email, 0 as score,c2.supplier_Type as supplierType,
                            case c2.supplier_area when '北京' then '3'
                            when '上海' then '2' when '广东' then '1' else '0' end as supplierIndestry,
                            c2.contact_name,c2.contact_mobile,c2.contact_fax,c2.business_price,c2.supplier_industry,
                            c2.contact_tel
                            from  t_supplier as c2 
left join T_Product b on b.supplierId =c2.id 
left join 
(
select count(*) as orderamount,supplier_name 
from t_generalinfo 
where status not in(-1,2,4,0)
group by supplier_name
) goo on c2.supplier_name =goo.supplier_name
where (c2.supplier_type<>1) and b.productType=@typeid {0}) as a
                            order by supplierType desc,supplierindestry desc,orderamount desc, score desc";
 
                sql = string.Format(sql, strWhere);
            if (!string.IsNullOrEmpty(typeid))
                parms.Add(new SqlParameter("@typeid", typeid));
            return DbHelperSQL.Query(sql, parms.ToArray()).Tables[0];
        }

        /// <summary>
        /// 通过当前登录人读取PR单历史供应商
        /// </summary>
        /// <param name="strWhere"></param>
        /// <param name="parms"></param>
        /// <param name="typeid"></param>
        /// <returns></returns>
        public static DataTable getSupplierListOrderByHist(string strWhere, List<SqlParameter> parms, string typeid)
        {
            string sql = @"select distinct * from (
select distinct ISNULL( goo.orderamount,0) as orderamount,
0 as goodAmount,0 as normalAmount,0 as badAmount,0 as countnum,
(select top 1 id from T_GeneralInfo as tid  where tid.supplier_name  =c2.supplier_name and requestor=c2.requestor and tid.status not in(-1,0,2,4) order by prNo desc) as id ,
0 as supplyId,'' as logName,'' as supplier_frameno,'' as supplier_code,
c2.supplier_name,'' as PriceLevel,'' as supplier_province,'' as  supplier_city,
0 as isperson,c2.supplier_email as contact_Email, 0 as score,'' as supplierType,
case  SUBSTRING(c2.supplier_address,0,2) when '北京' then '3'
when '上海' then '2' when '广东' then '1' else '0' end as supplierIndestry,
c2.supplier_linkman as contact_name,c2.supplier_cellphone as contact_mobile,c2.supplier_fax as contact_fax,'' as business_price,'' as supplier_industry,
c2.supplier_phone as contact_tel
from  t_generalinfo as c2 
join T_OrderInfo it on c2.id = it.general_id
left join T_Product b on b.supplierId =it.supplierId 

left join 
(
select count(*) as orderamount,supplier_name 
from t_generalinfo 
where status not in(-1,2,4,0)
group by supplier_name
) goo on c2.supplier_name =goo.supplier_name
where c2.status not in(-1,2,4,0) and it.productType=@typeid {0}) as a
                            order by orderamount desc, score desc";

            sql = string.Format(sql, strWhere);
            if (!string.IsNullOrEmpty(typeid))
                parms.Add(new SqlParameter("@typeid", typeid));
            return DbHelperSQL.Query(sql, parms.ToArray()).Tables[0];
        }



        public static DataTable getSupplierListOrderByRecommend(string strWhere, List<SqlParameter> parms, string productName)
        {
            string sql = @"select * from (select distinct c3.countnum,c2.id,c.id supplyId,c.logName,c2.supplier_frameno,c.supplier_code,c.supplier_name,c.supplier_province,c.supplier_city,c.isperson,c.contact_Email,e.score
                            ,case supplierType when '0' then '1'
                            when '1' then '3' when '2' then '2'
                            else '1' end as supplierType,
                            case supplier_province when '北京' then '3'
                            when '上海' then '2' when '广东' then '1' else '0' end as supplierIndestry,
                            c2.contact_name,c2.contact_mobile,c2.contact_fax,c2.business_price,c2.supplier_industry,c2.contact_tel
                            from sc_supplier as c
left join t_espandsupplysuppliersrelation as c1 on c.id=c1.supplysupplierid
left join t_supplier as c2 on c1.espsupplierid=c2.id
                            inner join sc_suppliertype as b on b.supplierid=c.id and typelv=3
                            left join SC_SupplierProtocolType as d on d.supplierid=c.id and d.typeid=b.typeid
left join T_ESPAndSupplyTypeRelation as d1 on d1.supplytypeid=d.typeid
left join (
	select count(id) countnum,producttype,supplierid from t_product 
where isshow=1 " + (string.IsNullOrEmpty(productName) ? "" : " and productname like '%'+@productname+'%' ") + @" group by producttype,supplierid
) as c3  on c3.supplierid=c2.id and c3.producttype=d1.esptypeid
                            left join (select sum(score) as score,supplierid from SC_SupplierScore group by supplierid) as e on c.id=e.supplierid


                            where c.status in (2,3,4,5)  {0}) as a where suppliertype=2
                            order by supplierType desc,supplierindestry desc,countnum desc, score desc";
       
                sql = string.Format(sql, strWhere);
            if (!string.IsNullOrEmpty(productName))
                parms.Add(new SqlParameter("@productname", productName));
            return DbHelperSQL.Query(sql, parms.ToArray()).Tables[0];
        }

        /// <summary>
        /// 协议供应商，分值高低排序，按地域，北上广
        /// </summary>
        /// <param name="strWhere"></param>
        /// <param name="parms"></param>
        /// <returns></returns>
        public static DataTable getSupplierListOrderByXieYi(string strWhere, List<SqlParameter> parms, string typeid)
        {
            string sql = @"select * from (select distinct  ISNULL( goo.orderamount,0) as orderamount,0 as goodAmount,0 as normalAmount
	,0 as badAmount,0 as countnum,c2.id,c2.supplier_frameno,'' as supplier_code,c2.supplier_name,'' as PriceLevel,
	c2.supplier_area as supplier_province,c2.supplier_area as supplier_city,0 as isperson,c2.contact_Email,
	'' as score,c2.supplier_type as supplierType,
	case supplier_area when '北京' then '3'
	when '上海' then '2' when '广东' then '1' else '0' end as supplierIndestry,
	c2.contact_name,c2.contact_mobile,c2.contact_fax,c2.business_price,
	c2.supplier_industry,c2.contact_tel
	from  t_supplier as c2 
	
	left join T_Product b on c2.id = b.supplierId

	left join 
	(
	select count(*) as orderamount,supplier_name 
	from t_generalinfo 
	where status not in(-1,2,4,0)
	group by supplier_name
	) goo on c2.supplier_name =goo.supplier_name

   where supplier_source ='协议供应商' and supplier_type=1 and supplier_status =1 and b.productType=@typeid  {0} ) as a 
   order by supplierindestry desc,orderamount desc, score desc";

            sql = string.Format(sql, strWhere);
            if (!string.IsNullOrEmpty(typeid))
                parms.Add(new SqlParameter("@typeid", typeid));
            return DbHelperSQL.Query(sql, parms.ToArray()).Tables[0];
        }

        /// <summary>
        /// 是否为协议供应商
        /// </summary>
        /// <param name="supplierId">采购供应商ID</param>
        /// <param name="typeId">采购第3级物料ID</param>
        /// <returns></returns>
        public static bool isAgreementSupplier(int supplierId, int typeId)
        {
            string sql = @"select * from SC_SupplierProtocolType
                                where supplierid in (select distinct supplysupplierid from T_ESPAndSupplySuppliersRelation where espsupplierid="+supplierId+@")
                                and typeid  in (select distinct supplytypeid from T_ESPAndSupplyTypeRelation where esptypeid="+typeId+")";
            DataTable dt = DbHelperSQL.Query(sql).Tables[0];
            if (dt != null && dt.Rows.Count > 0 && dt.Rows[0]["suppliertype"].ToString() == "1")
                return true;
            else
                return false;
        }
    }
}