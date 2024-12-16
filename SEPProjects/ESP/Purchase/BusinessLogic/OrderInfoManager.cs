using System.Data;
using System.Collections.Generic;
using System.Data.SqlClient;
using ESP.Purchase.DataAccess;
using ESP.Purchase.Entity;
using System;

namespace ESP.Purchase.BusinessLogic
{
    /// <summary>
    /// 业务逻辑类OrderInfoManager 的摘要说明。
    /// </summary>
    public static class OrderInfoManager
    {
        private static OrderInfoDataHelper dal = new OrderInfoDataHelper();
        private static LogDataProvider dalLog = new LogDataProvider();

        #region  成员方法
        /// <summary>
        /// 增加一条数据
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="opertionUserID">The opertion user ID.</param>
        /// <param name="opertionUserName">Name of the opertion user.</param>
        /// <returns></returns>
        public static int Add(OrderInfo model, int opertionUserID, string opertionUserName)
        {
            int orderID = dal.Add(model);
            return orderID;
        }

        public static int Add(OrderInfo model, SqlConnection conn, SqlTransaction trans)
        {
            int orderID = dal.Add(model,conn,trans);
            return orderID;
        }


        public static double TaxCalculator(double price, int taxType)
        {
            double retvalue = price;
            if (taxType == 1)//税前,计算出税后金额
            {

                if (price > 800 && price <= 4000)
                {
                    retvalue = Math.Round((price - 800) * 0.2, 2);
                }
                else if (price > 4000 && price <= 25000)
                {
                    retvalue = Math.Round(price * 0.8 * 0.2, 2);
                }
                else if (price > 25000 && price <= 62500)
                {
                    retvalue = Math.Round((price * 0.8 * 0.3 - 2000), 2);
                }
                else if (price > 62500)
                {
                    retvalue = Math.Round((price * 0.8 * 0.4 - 7000), 2);
                }
                else
                {
                    retvalue = 0;
                }
            }
            else if (taxType == 2)//税后，计算出税前金额
            {
                if (price >= 800 && price <= 3360)
                {
                    retvalue = Math.Round((price - 160) / 0.8, 2);
                }
                else if (price > 3360 && price <= 21000)
                {
                    retvalue = Math.Round(price / 0.84, 2);
                }
                else if (price > 21000 && price <= 49500)
                {
                    retvalue = Math.Round((price - 2000) / 0.76, 2);
                }
                else if (price > 49500)
                {
                    retvalue = Math.Round((price - 7000) / 68, 2);
                }
                else
                {
                    retvalue = price;
                }
            }
            else//不计算
            {
                retvalue = price;
            }

            return retvalue;
        }

        /// <summary>
        /// Adds the by trans.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public static int addByTrans(OrderInfo model)
        {
            return dal.addByTrans(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="opertionUserID">The opertion user ID.</param>
        /// <param name="opertionUserName">Name of the opertion user.</param>
        public static void Update(OrderInfo model, int opertionUserID, string opertionUserName)
        {
            dal.Update(model);
        }

        public static void update(OrderInfo model, SqlTransaction trans)
        {
            dal.Update(model, trans.Connection, trans);
        }

        /// <summary>
        /// Updates the by trans.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public static int updateByTrans(OrderInfo model)
        {
            return dal.updateByTrans(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        /// <param name="id">The id.</param>
        /// <param name="opertionUserID">The opertion user ID.</param>
        /// <param name="opertionUserName">Name of the opertion user.</param>
        public static bool Delete(int id, int opertionUserID, string opertionUserName, GeneralInfo gmodel)
        {
            int orderinfoCount = 0;
            orderinfoCount = GetList(" general_id = " + gmodel.id).Tables[0].Rows.Count;
            if (orderinfoCount == 1)
            {
                gmodel.supplier_address = string.Empty;
                gmodel.Supplier_cellphone = string.Empty;
                gmodel.supplier_email = string.Empty;
                gmodel.supplier_fax = string.Empty;
                gmodel.supplier_linkman = string.Empty;
                gmodel.supplier_name = string.Empty;
                gmodel.supplier_phone = string.Empty;
                gmodel.source = string.Empty;
                gmodel.fa_no = string.Empty;
                gmodel.sow = string.Empty;

                gmodel.account_bank = string.Empty;
                gmodel.account_name = string.Empty;
                gmodel.account_number = string.Empty;
                gmodel.CusAskEmailFile = string.Empty;

                using (SqlConnection conn = new SqlConnection(Common.DbHelperSQL.connectionString))
                {
                    conn.Open();
                    SqlTransaction trans = conn.BeginTransaction();
                    try
                    {
                        GeneralInfoDataProvider.Update(gmodel, conn, trans);
                        dal.Delete(id, conn, trans);
                        MediaOrderManager.DeleteByOrderID(id, conn, trans);
                        
                        trans.Commit();
                        return true;
                    }
                    catch
                    {
                        trans.Rollback();
                        return false;
                    }
                }

            }
            else
            {
                try
                {
                    dal.Delete(id);
                    return true;
                }
                catch
                {
                    return false;
                }
            }

        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns></returns>
        public static OrderInfo GetModel(int id)
        {
            return dal.GetModel(id);
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        /// <param name="strWhere">The STR where.</param>
        /// <returns></returns>
        // add by shangxichao 根据GeneralInfoID 得到一个对象实体  //用于媒介对私 Order
        public static DataSet GetList(string strWhere)
        {
            return dal.GetList(strWhere);
        }
        public static List<OrderInfo> GetInfoList(string strWhere)
        {
            return OrderInfoDataHelper.GetModelList(strWhere, null);
        }
        public static List<OrderInfo> GetModelList(string strWhere, List<System.Data.SqlClient.SqlParameter> parms)
        {
            return OrderInfoDataHelper.GetModelList(strWhere, parms);
        }
        /// <summary>
        /// 获得数据列表通过主表流水
        /// </summary>
        /// <param name="strWhere">The STR where.</param>
        /// <returns></returns>
        public static DataSet GetListByGID(string strWhere)
        {
            return dal.GetListByGID(strWhere);
        }

        /// <summary>
        /// Gets the model by general ID.
        /// </summary>
        /// <param name="generalid">The generalid.</param>
        /// <returns></returns>
        public static OrderInfo GetModelByGeneralID(int generalid)
        {
            return dal.GetModelByGeneralID(generalid);
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        /// <returns></returns>
        public static DataSet GetAllList()
        {
            return GetList("");
        }

        /// <summary>
        /// Gets the order list.
        /// </summary>
        /// <param name="strWhere">The STR where.</param>
        /// <returns></returns>
        public static List<OrderInfo> GetOrderList(string strWhere)
        {
            return OrderInfoDataHelper.GetOrderList(strWhere);
        }

        /// <summary>
        /// 根据申请单ID获得采购物品总金额
        /// </summary>
        /// <returns></returns>
        public static decimal getTotalPriceByGID(int gid)
        {
            DataSet ds = OrderInfoManager.GetList(" general_id=" + gid);
            decimal totalprice = 0;
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                totalprice += decimal.Parse(ds.Tables[0].Rows[i]["total"].ToString());
            }
            return totalprice;
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
            return OrderInfoDataHelper.getSupplierId(terms, out supplierId, out supplierName);
        }

        /// <summary>
        /// Gets the list by general id.
        /// </summary>
        /// <param name="generalid">The generalid.</param>
        /// <returns></returns>
        public static List<OrderInfo> GetListByGeneralId(int generalid)
        {
            return OrderInfoDataHelper.GetListByGeneralId(generalid);
        }
        public static List<OrderInfo> GetListByGeneralIds(int[] generalids)
        {
            return OrderInfoDataHelper.GetListByGeneralIds(generalids);
        }
        /// <summary>
        /// 获得初审人
        /// </summary>
        /// <param name=RequestName.GeneralID></param>
        /// <returns></returns>
        public static string[] getAuditor(int generalid)
        {
            return OrderInfoDataHelper.getAuditor(generalid);
        }

        /// <summary>
        /// 取得某PR单中的物料类别的流向
        /// </summary>
        /// <param name="generalid">The generalid.</param>
        /// <returns></returns>
        public static int getTypeOperationFlow(int generalid)
        {
            return OrderInfoDataHelper.getTypeOperationFlow(generalid);
        }

        /// <summary>
        /// 获取第一级物料类别的ID
        /// </summary>
        /// <param name="generalid"></param>
        /// <returns></returns>
        public static int getTopTypeId(int generalid)
        {
            return OrderInfoDataHelper.getTopTypeId(generalid);
        }

        /// <summary>
        /// 取得某PR单中的物料类别是否需要集团物料审核人复查
        /// </summary>
        /// <param name="generalid">The generalid.</param>
        /// <returns></returns>
        public static int getTypeIsNeedHQCheck(int generalid)
        {
            return OrderInfoDataHelper.getTypeIsNeedHQCheck(generalid);
        }

        /// <summary>
        /// 获得分公司审核人
        /// </summary>
        /// <param name="generalid">The generalid.</param>
        /// <param name="filialeName">Name of the filiale.</param>
        /// <returns></returns>
        public static string[] getFilialeAuditor(int generalid, string filialeName)
        {
            return OrderInfoDataHelper.getFilialeAuditor(generalid, filialeName);
        }
        /// <summary>
        /// 获得采购部付款申请人
        /// </summary>
        /// <param name="generalid"></param>
        /// <param name="filialeName"></param>
        /// <returns></returns>
        public static int getPaymentUser(int generalid, string filialeName)
        {
            return OrderInfoDataHelper.getPaymentUser(generalid, filialeName);
        }

        public static void DeleteAllByGeneralId(int general_id, SqlTransaction trans)
        {
            OrderInfoDataHelper.DeleteAllByGeneralId(general_id, trans);
        }

        /// <summary>
        /// 获取物料类别名称
        /// </summary>
        /// <param name="generalid"></param>
        /// <returns></returns>
        public static string GetProductTypeDes(int generalid)
        {
            return OrderInfoDataHelper.GetProductTypeDes(generalid);
        }

        #region 存储过程中处理个人pr单
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public static System.Collections.ArrayList GetReturnInPriOrderSql(string OrderIDs, decimal inceptPrice, string RequestorID, string RequestUserCode, string RequestUserName, string RequestEmployeeName, string filepath)
        {
            System.Collections.ArrayList ht = new System.Collections.ArrayList();
            SqlParameter[] parameters = {
                new SqlParameter("@ReturnID",SqlDbType.Int,4),
			    new SqlParameter("@inceptPrice", SqlDbType.Decimal,9),
                new SqlParameter("@OrderID",SqlDbType.VarChar,1000),
			    new SqlParameter("@RequestorID", SqlDbType.Int,4),
                new SqlParameter("@RequestUserCode",SqlDbType.NVarChar,50),
                new SqlParameter("@RequestUserName",SqlDbType.NVarChar,50),
                new SqlParameter("@RequestEmployeeName",SqlDbType.NVarChar,50),
                new SqlParameter("@Attachment",SqlDbType.NVarChar,100)
                                };
            parameters[0].Direction = ParameterDirection.Output;
            parameters[1].Value = inceptPrice;
            parameters[2].Value = OrderIDs;
            parameters[3].Value = RequestorID;
            parameters[4].Value = RequestUserCode;
            parameters[5].Value = RequestUserName;
            parameters[6].Value = RequestEmployeeName;
            parameters[7].Value = filepath;
            ht.Add("exec P_CreateReturnInPriOrder @ReturnID,@inceptPrice,@OrderID,@RequestorID,@RequestUserCode,@RequestUserName,@RequestEmployeeName,@Attachment");
            ht.Add(parameters);

            return ht;
        }

        public static bool CreateReturnInMediaOrderAndPRInMediaOrder(System.Collections.Hashtable ht)
        {
            try
            {
                Common.DbHelperSQL.ExecuteSqlTran(ht);
                return true;
            }
            catch { return false; }
        }

        public static bool CreateReturnInMediaOrderAndPRInMediaOrder(List<System.Collections.Hashtable> list)
        {
            try
            {
                Common.DbHelperSQL.ExcuteSqlTranByList(list);
                return true;
            }
            catch { return false; }
        }

        public static System.Collections.ArrayList GetPRInPriOrderSql(string OrderIDs, decimal inceptPrice, string RequestorID, string RequestEmployeeName, string filepath)
        {
            System.Collections.ArrayList ht = new System.Collections.ArrayList();
            string ms = string.Empty;
            if (OrderIDs.Length > 0)
                ms = OrderIDs;
            SqlParameter[] parameters = {
                new SqlParameter("@GID",SqlDbType.Int,4),
			    new SqlParameter("@inceptPrice", SqlDbType.Decimal,9),
                new SqlParameter("@OrderID",SqlDbType.VarChar,1000),
			    new SqlParameter("@RequestorID", SqlDbType.Int,4),
                new SqlParameter("@RequestEmployeeName",SqlDbType.NVarChar,50),
                new SqlParameter("@Attachment",SqlDbType.NVarChar,100),
                new SqlParameter("@requestor_info",SqlDbType.NVarChar,30),
                new SqlParameter("@requestor_group",SqlDbType.NVarChar,30)
                                };
            parameters[0].Direction = ParameterDirection.Output;
            parameters[1].Value = inceptPrice;
            parameters[2].Value = ms;
            parameters[3].Value = RequestorID;
            parameters[4].Value = RequestEmployeeName;
            parameters[5].Value = filepath;
            
            parameters[6].Value = "";
            parameters[7].Value = "" ;
            ht.Add("exec P_CreatePRInPriOrder @GID,@inceptPrice,@OrderID,@RequestorID,@RequestEmployeeName,@Attachment,@requestor_info,@requestor_group");
            ht.Add(parameters);

            return ht;
        }
        #endregion
        #endregion  成员方法
    }


}