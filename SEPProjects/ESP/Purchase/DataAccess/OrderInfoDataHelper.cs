using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using System.Collections.Generic;
using ESP.Purchase.BusinessLogic;
using ESP.Purchase.Common;
using ESP.Purchase.Entity;

namespace ESP.Purchase.DataAccess
{
    /// <summary>
    /// 数据访问类OrderInfoDataHelper。
    /// </summary>
    public class OrderInfoDataHelper
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OrderInfoDataHelper"/> class.
        /// </summary>
        public OrderInfoDataHelper()
        { }

        #region  成员方法
        /// <summary>
        /// Adds the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public int Add(OrderInfo model)
        {
            return Add(model, null, null);
        }

        /// <summary>
        /// Adds the by trans.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public int addByTrans(OrderInfo model)
        {
            int returnValue = 0;
            using (SqlConnection conn = new SqlConnection(DbHelperSQL.connectionString))
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();
                try
                {
                    returnValue = Add(model, conn, trans);

                    GeneralInfo general = GeneralInfoManager.GetModel(model.general_id);
                    if (model.supplierId > 0)
                    {
                        SupplierInfo supplier = SupplierManager.GetModel(model.supplierId);
                        if (supplier.supplier_type == (int)State.supplier_type.agreement)
                        {
                            general.supplier_name = supplier.supplier_name;
                            general.supplier_address = supplier.contact_address;
                            general.supplier_linkman = supplier.contact_name;
                            general.supplier_phone = supplier.contact_tel;
                            general.Supplier_cellphone = supplier.contact_mobile;
                            general.supplier_fax = supplier.contact_fax;
                            general.supplier_email = supplier.contact_email;
                            //general.source = supplier.supplier_source;
                            general.source = "协议供应商";
                            general.fa_no = supplier.supplier_frameNO;

                            general.account_bank = supplier.account_bank;
                            general.account_name = supplier.account_name;
                            general.account_number = supplier.account_number;

                            GeneralInfoDataProvider.Update(general, conn, trans);
                        }
                    }
                    
                    trans.Commit();
                }
                catch
                {
                    trans.Rollback();
                }
                finally
                {
                    conn.Close();
                }
            }
            return returnValue;
        }

        /// <summary>
        /// Updates the by trans.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public int updateByTrans(OrderInfo model)
        {
            int returnValue = 0;
            using (SqlConnection conn = new SqlConnection(DbHelperSQL.connectionString))
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();
                try
                {
                    returnValue = Update(model, conn, trans);
                    UpdateSupplierInfo(model, conn, trans);
                    
                    GeneralInfo general = GeneralInfoManager.GetModel(model.general_id);
                    if (model.supplierId > 0)
                    {
                        SupplierInfo supplier = SupplierManager.GetModel(model.supplierId);
                        if (supplier.supplier_type == (int)State.supplier_type.agreement)
                        {
                            general.supplier_name = supplier.supplier_name;
                            general.supplier_address = supplier.contact_address;
                            general.supplier_linkman = supplier.contact_name;
                            general.supplier_phone = supplier.contact_tel;
                            general.Supplier_cellphone = supplier.contact_mobile;
                            general.supplier_fax = supplier.contact_fax;
                            general.supplier_email = supplier.contact_email;
                            general.source = supplier.supplier_source;
                            general.fa_no = supplier.supplier_frameNO;
                            general.CusAskEmailFile = "";

                            general.account_bank = supplier.account_bank;
                            general.account_name = supplier.account_name;
                            general.account_number = supplier.account_number;
                            GeneralInfoDataProvider.Update(general, conn, trans);
                        }
                    }
                    trans.Commit();
                }
                catch
                {
                    trans.Rollback();
                }
                finally
                {
                    conn.Close();
                }
            }
            return returnValue;
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="conn">The conn.</param>
        /// <param name="trans">The trans.</param>
        /// <returns></returns>
        public int Add(OrderInfo model, SqlConnection conn, SqlTransaction trans)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into T_OrderInfo(");
            strSql.Append("general_id,Item_No,desctiprtion,intend_receipt_date,price,uom,quantity,total,upfile,producttype,supplierId,productAttribute,supplierName,billid,billtype,OrderContent,OrderStatus,oldPrice,oldQuantity,factTotal)");
            strSql.Append(" values (");
            strSql.Append("@general_id,@Item_No,@desctiprtion,@intend_receipt_date,@price,@uom,@quantity,@total,@upfile,@producttype,@supplierId,@productAttribute,@supplierName,@billid,@billtype,@OrderContent,@OrderStatus,@oldPrice,@oldQuantity,@factTotal)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4),
					new SqlParameter("@general_id", SqlDbType.Int,4),
					new SqlParameter("@Item_No", SqlDbType.NVarChar,50),
					new SqlParameter("@desctiprtion", SqlDbType.NVarChar,500),
					new SqlParameter("@intend_receipt_date", SqlDbType.VarChar,200),
					new SqlParameter("@price", SqlDbType.Decimal,9),
					new SqlParameter("@uom", SqlDbType.NVarChar,10),
					new SqlParameter("@quantity", SqlDbType.Decimal,9),
					new SqlParameter("@total", SqlDbType.Decimal,9),
                    new SqlParameter("@upfile",SqlDbType.VarChar,2000),
                    new SqlParameter("@producttype",SqlDbType.Int,4),
                    new SqlParameter("@supplierId",SqlDbType.Int,4),
                    new SqlParameter("@productAttribute",SqlDbType.Int,4),
                    new SqlParameter("@supplierName",SqlDbType.VarChar,200),
                    new SqlParameter("@billid",SqlDbType.Int,4),
                    new SqlParameter("@billtype",SqlDbType.Int,4),
                    new SqlParameter("OrderContent",SqlDbType.Binary),
                    new SqlParameter("@OrderStatus",SqlDbType.Int,4),
                    new SqlParameter("@oldPrice",SqlDbType.Decimal,9),
                    new SqlParameter("@oldQuantity",SqlDbType.Decimal,9),
                    new SqlParameter("@factTotal",SqlDbType.Decimal,9)
            };
            parameters[0].Value = model.id;
            parameters[1].Value = model.general_id;
            parameters[2].Value = model.Item_No;
            parameters[3].Value = model.desctiprtion;
            parameters[4].Value = model.intend_receipt_date;
            parameters[5].Value = model.price;
            parameters[6].Value = model.uom;
            parameters[7].Value = model.quantity;
            parameters[8].Value = model.total;
            parameters[9].Value = model.upfile;
            parameters[10].Value = model.producttype;
            parameters[11].Value = model.supplierId;
            parameters[12].Value = model.productAttribute;
            parameters[13].Value = model.supplierName;
            parameters[14].Value = model.BillID;
            parameters[15].Value = model.BillType;
            parameters[16].Value = model.OrderContent;
            parameters[17].Value = 0;
            parameters[18].Value = model.oldPrice;
            parameters[19].Value = model.oldQuantity;
            parameters[20].Value = model.FactTotal;
            object obj = null;
            if (conn == null)
            {
                using (SqlConnection newConn = new SqlConnection(DbHelperSQL.connectionString))
                {
                    newConn.Open();
                    SqlTransaction newTrans = newConn.BeginTransaction();
                    try
                    {
                        obj = DbHelperSQL.GetSingle(strSql.ToString(), newConn, newTrans, parameters);
                        ESP.Purchase.BusinessLogic.PaymentPeriodManager.AutoChagedExpectPaymentPrice(model.general_id, GetTotalPrice(model.general_id, newTrans), newTrans); //更新付款帐期金额
                        newTrans.Commit();
                    }
                    catch (Exception ex)
                    {
                        ESP.Logging.Logger.Add(ex.Message, "添加采购物品", ESP.Logging.LogLevel.Error, ex);
                        newTrans.Rollback();
                    }
                }
            }
            else
            {
                obj = DbHelperSQL.GetSingle(strSql.ToString(), conn, trans, parameters);
                ESP.Purchase.BusinessLogic.PaymentPeriodManager.AutoChagedExpectPaymentPrice(model.general_id, GetTotalPrice(model.general_id, trans), trans); //更新付款帐期金额
            }
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
        /// Updates the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        public void Update(OrderInfo model)
        {
            Update(model, null, null);
        }

        /// <summary>
        /// Updates the supplier info.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="conn">The conn.</param>
        /// <param name="trans">The trans.</param>
        public void UpdateSupplierInfo(OrderInfo model, SqlConnection conn, SqlTransaction trans)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update T_OrderInfo set ");
            strSql.Append("supplierId=@supplierId,");
            strSql.Append("supplierName=@supplierName");
            strSql.Append(" where general_id=@general_id ");
            SqlParameter[] parameters = {
                        new SqlParameter("@general_id", SqlDbType.Int,4),
                        new SqlParameter("@supplierId",SqlDbType.Int,4),   
                        new SqlParameter("@supplierName",SqlDbType.VarChar,200)
            };
            parameters[0].Value = model.general_id;
            parameters[1].Value = model.supplierId;
            parameters[2].Value = model.supplierName;
            DbHelperSQL.ExecuteSql(strSql.ToString(), conn, trans, parameters);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="conn">The conn.</param>
        /// <param name="trans">The trans.</param>
        /// <returns></returns>
        public int Update(OrderInfo model, SqlConnection conn, SqlTransaction trans)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update T_OrderInfo set ");
            strSql.Append("general_id=@general_id,");
            strSql.Append("Item_No=@Item_No,");
            strSql.Append("desctiprtion=@desctiprtion,");
            strSql.Append("intend_receipt_date=@intend_receipt_date,");
            strSql.Append("price=@price,");
            strSql.Append("uom=@uom,");
            strSql.Append("quantity=@quantity,");
            strSql.Append("total=@total,");
            strSql.Append("upfile=@upfile,");
            strSql.Append("producttype=@producttype,");
            strSql.Append("supplierId=@supplierId,");
            strSql.Append("productAttribute=@productAttribute,");
            strSql.Append("supplierName=@supplierName,");
            strSql.Append("billid=@billid,");
            strSql.Append("billtype=@billtype,");
            strSql.Append("OrderContent=@OrderContent");
            strSql.Append(",oldPrice=@oldPrice");
            strSql.Append(",oldQuantity=@oldQuantity,factTotal=@factTotal");

            strSql.Append(" where id=@id ");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4),
					new SqlParameter("@general_id", SqlDbType.Int,4),
					new SqlParameter("@Item_No", SqlDbType.NVarChar,50),
					new SqlParameter("@desctiprtion", SqlDbType.NVarChar,500),
					new SqlParameter("@intend_receipt_date", SqlDbType.VarChar,200),
					new SqlParameter("@price", SqlDbType.Decimal,9),
					new SqlParameter("@uom", SqlDbType.NVarChar,10),
					new SqlParameter("@quantity", SqlDbType.Decimal,9),
					new SqlParameter("@total", SqlDbType.Decimal,9),
                    new SqlParameter("@upfile",SqlDbType.VarChar,2000),
                    new SqlParameter("@producttype",SqlDbType.Int,4),
                    new SqlParameter("@supplierId",SqlDbType.Int,4),                    
                    new SqlParameter("@productAttribute",SqlDbType.Int,4),
                    new SqlParameter("@supplierName",SqlDbType.VarChar,200),
                    new SqlParameter("@billid",SqlDbType.Int,4),
                    new SqlParameter("@billtype",SqlDbType.Int,4),
                    new SqlParameter("@OrderContent",SqlDbType.Binary),
                    new SqlParameter("@oldPrice",SqlDbType.Decimal,9),
                    new SqlParameter("@oldQuantity",SqlDbType.Decimal,9),
                    new SqlParameter("@factTotal",SqlDbType.Decimal,9)
                                        };
            parameters[0].Value = model.id;
            parameters[1].Value = model.general_id;
            parameters[2].Value = model.Item_No;
            parameters[3].Value = model.desctiprtion;
            parameters[4].Value = model.intend_receipt_date;
            parameters[5].Value = model.price;
            parameters[6].Value = model.uom;
            parameters[7].Value = model.quantity;
            parameters[8].Value = model.total;
            parameters[9].Value = model.upfile;
            parameters[10].Value = model.producttype;
            parameters[11].Value = model.supplierId;
            parameters[12].Value = model.productAttribute;
            parameters[13].Value = model.supplierName;
            parameters[14].Value = model.BillID;
            parameters[15].Value = model.BillType;
            parameters[16].Value = model.OrderContent;
            parameters[17].Value = model.oldPrice;
            parameters[18].Value = model.oldQuantity;
            parameters[19].Value = model.FactTotal;
            int ret = 0;
            if (conn == null)
            {
                using (SqlConnection newConn = new SqlConnection(DbHelperSQL.connectionString))
                {
                    newConn.Open();
                    SqlTransaction newTrans = newConn.BeginTransaction();
                    try
                    {
                        ret = DbHelperSQL.ExecuteSql(strSql.ToString(), newConn, newTrans, parameters);
                        ESP.Purchase.BusinessLogic.PaymentPeriodManager.AutoChagedExpectPaymentPrice(model.general_id, GetTotalPrice(model.general_id, newTrans), newTrans); //更新付款帐期金额
                        newTrans.Commit();
                    }
                    catch (Exception ex)
                    {
                        newTrans.Rollback();
                        ESP.Logging.Logger.Add(ex.Message, "编辑采购物品", ESP.Logging.LogLevel.Error, ex);
                    }
                }
            }
            else
            {
                ret = DbHelperSQL.ExecuteSql(strSql.ToString(), conn, trans, parameters);
                ESP.Purchase.BusinessLogic.PaymentPeriodManager.AutoChagedExpectPaymentPrice(model.general_id, GetTotalPrice(model.general_id, trans), trans); //更新付款帐期金额
            }
            return ret;
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        /// <param name="id">The id.</param>
        public void Delete(int id)
        {

            Delete(id, null, null);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        /// <param name="id">The id.</param>
        public void Delete(int id, SqlConnection conn, SqlTransaction trans)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete T_OrderInfo ");
            strSql.Append(" where id=@id ");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)};
            parameters[0].Value = id;

            DbHelperSQL.ExecuteSql(strSql.ToString(), conn, trans, parameters);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        /// <param name="id">The id.</param>
        public static void DeleteAllByGeneralId(int general_id, SqlTransaction trans)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("DELETE FROM T_OrderInfo WHERE general_id = @general_id ");
            SqlParameter[] parameters = {
					new SqlParameter("@general_id", SqlDbType.Int,4)};
            parameters[0].Value = general_id;

            DbHelperSQL.ExecuteSql(strSql.ToString(), trans.Connection, trans, parameters);
        }



        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns></returns>
        public OrderInfo GetModel(int id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 id,general_id,Item_No,desctiprtion,intend_receipt_date,price,uom,quantity,total,upfile,producttype,supplierId,productAttribute,supplierName,billid,billtype,ordercontent,oldPrice,oldQuantity,factTotal from T_OrderInfo ");
            strSql.Append(" where id=@id ");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)};
            parameters[0].Value = id;

            OrderInfo model = new OrderInfo();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["id"].ToString() != "")
                {
                    model.id = int.Parse(ds.Tables[0].Rows[0]["id"].ToString());
                }
                if (ds.Tables[0].Rows[0]["general_id"].ToString() != "")
                {
                    model.general_id = int.Parse(ds.Tables[0].Rows[0]["general_id"].ToString());
                }
                model.Item_No = ds.Tables[0].Rows[0]["Item_No"].ToString();
                model.desctiprtion = ds.Tables[0].Rows[0]["desctiprtion"].ToString();
                model.intend_receipt_date = ds.Tables[0].Rows[0]["intend_receipt_date"].ToString();
                if (ds.Tables[0].Rows[0]["price"].ToString() != "")
                {
                    model.price = decimal.Parse(ds.Tables[0].Rows[0]["price"].ToString());
                }
                model.uom = ds.Tables[0].Rows[0]["uom"].ToString();
                if (ds.Tables[0].Rows[0]["quantity"].ToString() != "")
                {
                    model.quantity = decimal.Parse(ds.Tables[0].Rows[0]["quantity"].ToString());
                }
                if (ds.Tables[0].Rows[0]["total"].ToString() != "")
                {
                    model.total = decimal.Parse(ds.Tables[0].Rows[0]["total"].ToString());
                }
                if (ds.Tables[0].Rows[0]["factTotal"].ToString() != "")
                {
                    model.FactTotal = decimal.Parse(ds.Tables[0].Rows[0]["factTotal"].ToString());
                }
                model.upfile = ds.Tables[0].Rows[0]["upfile"].ToString().TrimEnd('#');
                if (ds.Tables[0].Rows[0]["producttype"] != DBNull.Value && ds.Tables[0].Rows[0]["producttype"].ToString() != "")
                    model.producttype = int.Parse(ds.Tables[0].Rows[0]["producttype"].ToString());
                if (ds.Tables[0].Rows[0]["supplierId"] != DBNull.Value && ds.Tables[0].Rows[0]["supplierId"].ToString() != "")
                    model.supplierId = int.Parse(ds.Tables[0].Rows[0]["supplierId"].ToString());
                if (ds.Tables[0].Rows[0]["productAttribute"] != DBNull.Value && ds.Tables[0].Rows[0]["productAttribute"].ToString() != "")
                {
                    model.productAttribute = int.Parse(ds.Tables[0].Rows[0]["productAttribute"].ToString());
                }
                if (ds.Tables[0].Rows[0]["billid"] != DBNull.Value && ds.Tables[0].Rows[0]["billid"].ToString() != "")
                {
                    model.BillID = int.Parse(ds.Tables[0].Rows[0]["billid"].ToString());
                }
                if (ds.Tables[0].Rows[0]["billtype"] != DBNull.Value && ds.Tables[0].Rows[0]["billtype"].ToString() != "")
                {
                    model.BillType = int.Parse(ds.Tables[0].Rows[0]["billtype"].ToString());
                }
                if (ds.Tables[0].Rows[0]["ordercontent"] != DBNull.Value && ds.Tables[0].Rows[0]["ordercontent"].ToString() != "")
                {
                    model.OrderContent = ds.Tables[0].Rows[0]["ordercontent"] == DBNull.Value ? null : (byte[])ds.Tables[0].Rows[0]["ordercontent"];
                }
                model.supplierName = ds.Tables[0].Rows[0]["supplierName"].ToString();
                model.oldPrice = ds.Tables[0].Rows[0]["oldPrice"] == DBNull.Value ? 0m : decimal.Parse(ds.Tables[0].Rows[0]["oldPrice"].ToString());
                model.oldQuantity = ds.Tables[0].Rows[0]["oldQuantity"] == DBNull.Value ? 0m : decimal.Parse(ds.Tables[0].Rows[0]["oldQuantity"].ToString());
               
                return model;
            }
            else
            {
                return null;
            }
        }


        /// <summary>
        /// add by shangxichao 根据GeneralInfoID 得到一个对象实体  //用于媒介对私 Order
        /// </summary>
        /// <param name="generalid">The generalid.</param>
        /// <returns></returns>
        public OrderInfo GetModelByGeneralID(int generalid)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 id,general_id,Item_No,desctiprtion,intend_receipt_date,price,uom,quantity,total,upfile,producttype,supplierId,productAttribute,supplierName,billid,billtype,ordercontent,oldPrice,oldQuantity,factTotal from T_OrderInfo ");
            strSql.Append(" where general_id=@general_id ");
            SqlParameter[] parameters = {
					new SqlParameter("@general_id", SqlDbType.Int,4)};
            parameters[0].Value = generalid;

            OrderInfo model = new OrderInfo();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["id"].ToString() != "")
                {
                    model.id = int.Parse(ds.Tables[0].Rows[0]["id"].ToString());
                }
                if (ds.Tables[0].Rows[0]["general_id"].ToString() != "")
                {
                    model.general_id = int.Parse(ds.Tables[0].Rows[0]["general_id"].ToString());
                }
                model.Item_No = ds.Tables[0].Rows[0]["Item_No"].ToString();
                model.desctiprtion = ds.Tables[0].Rows[0]["desctiprtion"].ToString();
                model.intend_receipt_date = ds.Tables[0].Rows[0]["intend_receipt_date"].ToString();
                if (ds.Tables[0].Rows[0]["price"].ToString() != "")
                {
                    model.price = decimal.Parse(ds.Tables[0].Rows[0]["price"].ToString());
                }
                model.uom = ds.Tables[0].Rows[0]["uom"].ToString();
                if (ds.Tables[0].Rows[0]["quantity"].ToString() != "")
                {
                    model.quantity = decimal.Parse(ds.Tables[0].Rows[0]["quantity"].ToString());
                }
                if (ds.Tables[0].Rows[0]["total"].ToString() != "")
                {
                    model.total = decimal.Parse(ds.Tables[0].Rows[0]["total"].ToString());
                }
                if (ds.Tables[0].Rows[0]["factTotal"].ToString() != "")
                {
                    model.FactTotal = decimal.Parse(ds.Tables[0].Rows[0]["factTotal"].ToString());
                }
                model.upfile = ds.Tables[0].Rows[0]["upfile"].ToString().TrimEnd('#');
                if (ds.Tables[0].Rows[0]["producttype"] != DBNull.Value && ds.Tables[0].Rows[0]["producttype"].ToString() != "")
                    model.producttype = int.Parse(ds.Tables[0].Rows[0]["producttype"].ToString());
                if (ds.Tables[0].Rows[0]["supplierId"] != DBNull.Value && ds.Tables[0].Rows[0]["supplierId"].ToString() != "")
                    model.supplierId = int.Parse(ds.Tables[0].Rows[0]["supplierId"].ToString());
                if (ds.Tables[0].Rows[0]["productAttribute"] != DBNull.Value && ds.Tables[0].Rows[0]["productAttribute"].ToString() != "")
                {
                    model.productAttribute = int.Parse(ds.Tables[0].Rows[0]["productAttribute"].ToString());
                }
                if (ds.Tables[0].Rows[0]["billid"] != DBNull.Value && ds.Tables[0].Rows[0]["billid"].ToString() != "")
                {
                    model.BillID = int.Parse(ds.Tables[0].Rows[0]["billid"].ToString());
                }
                if (ds.Tables[0].Rows[0]["billtype"] != DBNull.Value && ds.Tables[0].Rows[0]["billtype"].ToString() != "")
                {
                    model.BillType = int.Parse(ds.Tables[0].Rows[0]["billtype"].ToString());
                }
                if (ds.Tables[0].Rows[0]["ordercontent"] != DBNull.Value && ds.Tables[0].Rows[0]["ordercontent"].ToString() != "")
                {
                    model.OrderContent = (byte[])ds.Tables[0].Rows[0]["ordercontent"];
                }
                model.supplierName = ds.Tables[0].Rows[0]["supplierName"] == DBNull.Value ? "" : ds.Tables[0].Rows[0]["supplierName"].ToString();
                model.oldPrice = ds.Tables[0].Rows[0]["oldPrice"] == DBNull.Value ? 0m : decimal.Parse(ds.Tables[0].Rows[0]["oldPrice"].ToString());
                model.oldQuantity = ds.Tables[0].Rows[0]["oldQuantity"] == DBNull.Value ? 0m : decimal.Parse(ds.Tables[0].Rows[0]["oldQuantity"].ToString());
               
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
        /// <returns></returns>
        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select id,general_id,Item_No,desctiprtion,intend_receipt_date,price,uom,quantity,total,upfile,producttype,supplierId,productAttribute,supplierName,billid,billtype,oldPrice,oldQuantity,factTotal ");
            strSql.Append(" FROM T_OrderInfo ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 获得数据列表通过主表流水
        /// </summary>
        /// <param name="strWhere">The STR where.</param>
        /// <returns></returns>
        public DataSet GetListByGID(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select a.id as gid,a.requestor,a.prno,a.project_code,b.* from t_generalinfo as a  ");
            strSql.Append("inner join T_OrderInfo as b on a.id=b.general_id   ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// Gets the order list.
        /// </summary>
        /// <param name="strWhere">The STR where.</param>
        /// <returns></returns>
        public static List<OrderInfo> GetOrderList(string strWhere)
        {
            List<OrderInfo> list = new List<OrderInfo>();
            string sql = string.Format("select * from T_OrderInfo as a inner join t_type as b on a.producttype=b.typeid where 1=1 {0} order by id", strWhere);
            //string sql = string.Format("select * from T_OrderInfo as a inner join t_type as b on a.producttypelv2=b.typeid where 1=1 {0} order by id", strWhere);
            using (SqlDataReader r = DbHelperSQL.ExecuteReader(sql))
            {
                while (r.Read())
                {
                    OrderInfo c = new OrderInfo();
                    c.PopupData(r);
                    list.Add(c);
                }
                r.Close();
            }
            return list;
        }

        public static List<OrderInfo> GetModelList(string strWhere, List<System.Data.SqlClient.SqlParameter> parms)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select *");
            strSql.Append(" FROM T_OrderInfo ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            if (parms != null)
                return ESP.Finance.Utility.CBO.FillCollection<OrderInfo>(DbHelperSQL.Query(strSql.ToString(), parms.ToArray()));
            else
                return ESP.Finance.Utility.CBO.FillCollection<OrderInfo>(DbHelperSQL.Query(strSql.ToString(), null));
        }

        /// <summary>
        /// Gets the supplier id.
        /// </summary>
        /// <param name="terms">The terms.</param>
        /// <param name="supplierId">The supplier id.</param>
        /// <param name="supplierName">Name of the supplier.</param>
        /// <returns></returns>
        public static int getSupplierId(string terms, out int supplierId, out string supplierName)
        {
            string strSql = "select top 1 supplierId,supplierName from t_orderInfo where 1=1";
            if (terms != "")
                strSql += terms;
            DataSet ds = DbHelperSQL.Query(strSql);
            supplierId = 0;
            supplierName = "";
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["supplierId"] != DBNull.Value && ds.Tables[0].Rows[0]["supplierId"].ToString() != "")
                    supplierId = int.Parse(ds.Tables[0].Rows[0]["supplierId"].ToString());
                supplierName = ds.Tables[0].Rows[0]["supplierName"].ToString();
            }
            return ds.Tables[0].Rows.Count;
        }

        /// <summary>
        /// 获得初审人
        /// </summary>
        /// <param name="generalid">The generalid.</param>
        /// <returns></returns>
        public static string[] getAuditor(int generalid)
        {
            List<OrderInfo> list = GetOrderList(string.Format(" and general_id = {0}", generalid));
            return new string[] { list[0].auditor.ToString(), ESP.Compatible.UserManager.GetUserName(list[0].auditor) };
        }

        /// <summary>
        /// 取得某PR单中的物料类别的流向
        /// </summary>
        /// <param name="generalid">The generalid.</param>
        /// <returns></returns>
        public static int getTypeOperationFlow(int generalid)
        {
            List<OrderInfo> list = GetOrderList(string.Format(" and general_id = {0}", generalid));
            if (list == null || list.Count == 0)
                return 0;
            else
                return (new TypeDataProvider()).GetModel(list[0].producttype).operationflow;
        }

        /// <summary>
        /// 获取第一级物料类别的ID
        /// </summary>
        /// <param name="generalid"></param>
        /// <returns></returns>
        public static int getTopTypeId(int generalid)
        {
            List<OrderInfo> list = GetOrderList(string.Format(" and general_id = {0}", generalid));
            if (list == null || list.Count == 0)
                return 0;
            else
            {
                TypeInfo type3 = TypeManager.GetModel(list[0].producttype);
                TypeInfo type2 = TypeManager.GetModel(type3.parentId);
                return TypeManager.GetModel(type2.parentId).typeid;
            }
        }

        /// <summary>
        /// 取得某PR单中的物料类别是否需要集团物料审核人复查
        /// </summary>
        /// <param name="generalid">The generalid.</param>
        /// <returns></returns>
        public static int getTypeIsNeedHQCheck(int generalid)
        {
            List<OrderInfo> list = GetOrderList(string.Format(" and general_id = {0}", generalid));
            return (new TypeDataProvider()).GetModel(list[0].producttype).IsNeedHQCheck;
        }

        /// <summary>
        /// 获得分公司审核人
        /// </summary>
        /// <param name="generalid">The generalid.</param>
        /// <param name="filialeName">Name of the filiale.</param>
        /// <returns></returns>
        public static string[] getFilialeAuditor(int generalid, string filialeName)
        {
            List<OrderInfo> list = GetOrderList(string.Format(" and general_id = {0}", generalid));
            if (filialeName == State.filialeName_CQ)
            {
                return new string[] { list[0].filiale_auditor_cq.ToString(), ESP.Compatible.UserManager.GetUserName(list[0].filiale_auditor_cq) };
            }
            return null;
        }
        /// <summary>
        /// 获取采购部提交付款申请人
        /// </summary>
        /// <param name="generalid"></param>
        /// <param name="filialeName"></param>
        /// <returns></returns>
        public static int getPaymentUser(int generalid, string filialeName)
        {
            List<OrderInfo> list = GetOrderList(string.Format(" and general_id = {0}", generalid));
            ESP.Purchase.Entity.TypeInfo TypeModel = ESP.Purchase.BusinessLogic.TypeManager.GetModel(list[0].producttype); 
            if (filialeName == State.filialeName_CQ)
            {
                return TypeModel.SHPaymentUserID;
            }
            else
            {
                return TypeModel.BJPaymentUserID;
            }
        }

        /// <summary>
        /// Gets the list by general id.
        /// </summary>
        /// <param name="generalid">The generalid.</param>
        /// <returns></returns>
        public static List<OrderInfo> GetListByGeneralId(int generalid)
        {
            List<OrderInfo> list = GetOrderList(string.Format(" and general_id = {0}", generalid));
            return list;
        }

        public static List<OrderInfo> GetListByGeneralIds(int[] generalids)
        {
            if (generalids == null || generalids.Length == 0)
                return new List<OrderInfo>();

            var term = new System.Text.StringBuilder(" and general_id in (");
            term.Append(generalids[0]);
            for (var i = 1; i < generalids.Length; i++)
            {
                term.Append(",").Append(generalids[i]);
            }
            term.Append(") ");

            List<OrderInfo> list = GetOrderList(term.ToString());
            return list;
        }

        /// <summary>
        /// 根据订单ID获得采购物品总价
        /// </summary>
        /// <param name="generalid">The generalid.</param>
        /// <returns></returns>
        public static decimal GetTotalPrice(int generalid)
        {
            decimal totalPrice = 0;
            string strSql = @"select sum(total) as totalprice from t_orderinfo where general_id = " + generalid;
            DataSet ds = DbHelperSQL.Query(strSql);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                totalPrice = decimal.Parse(ds.Tables[0].Rows[0]["totalprice"].ToString());
            }
            return totalPrice;
        }

        /// <summary>
        /// 根据订单ID获得采购物品总价
        /// </summary>
        /// <param name="generalid">The generalid.</param>
        /// <returns></returns>
        public static decimal GetTotalPrice(int generalid, SqlTransaction trans)
        {
            decimal totalPrice = 0;
            string strSql = @"select sum(total) as totalprice from t_orderinfo where general_id = " + generalid;
            DataSet ds = DbHelperSQL.Query(strSql, trans.Connection, trans);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                totalPrice = decimal.Parse(ds.Tables[0].Rows[0]["totalprice"].ToString());
            }
            return totalPrice;
        }

        /// <summary>
        /// 获取物料类别名称
        /// </summary>
        /// <param name="generalid"></param>
        /// <returns></returns>
        public static string GetProductTypeDes(int generalid)
        {
            string strSql = @"select (c.typename + ' - ' + b.typename + ' - ' + a.typename) as typeDes from t_type as a
                                inner join t_type as b on a.parentid=b.typeid
                                inner join t_type as c on b.parentid=c.typeid";
            strSql += " where a.typeid in (select producttype from t_orderinfo where general_id=" + generalid + ")";
            DataSet ds = DbHelperSQL.Query(strSql);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                return ds.Tables[0].Rows[0]["typeDes"].ToString();
            return "";
        }

        #endregion  成员方法
    }
}