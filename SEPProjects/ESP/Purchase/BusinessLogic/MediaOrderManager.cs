using System;
using System.Data;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using ESP.Purchase.DataAccess;
using ESP.Purchase.Entity;

namespace ESP.Purchase.BusinessLogic
{
    /// <summary>
    /// 业务逻辑类MediaOrderManager 的摘要说明。
    /// </summary>
    public static class MediaOrderManager
    {
        private static MediaOrderDataProvider dal = new MediaOrderDataProvider();

        #region  成员方法

        /// <summary>
        /// 得到最大ID
        /// </summary>
        public static int GetMaxId()
        {
            return dal.GetMaxId();
        }

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public static bool Exists(int MeidaOrderID)
        {
            return dal.Exists(MeidaOrderID);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public static void Add(MediaOrderInfo model)
        {
            dal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public static void Update(MediaOrderInfo model)
        {
            dal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public static void Delete(int MeidaOrderID)
        {

            dal.Delete(MeidaOrderID);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public static MediaOrderInfo GetModel(int MeidaOrderID, SqlTransaction trans)
        {

            return dal.GetModel(MeidaOrderID, trans);
        }
        public static MediaOrderInfo GetModel(int MeidaOrderID)
        {

            return dal.GetModel(MeidaOrderID, null);
        }
        public static string GetSubTotalByPaymentUser(string MediaOrderIDs)
        {
            return dal.GetSubTotalByPaymentUser(MediaOrderIDs);
        }

        public static DataSet GetBaiduList(string conn)
        {
            return dal.GetBaiduList(conn);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public static DataSet GetList(string strWhere)
        {
            return dal.GetList(strWhere);
        }

        public static DataSet GetMediaOrderList(string strWhere)
        {
            return dal.GetMediaOrderList(strWhere);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public static List<MediaOrderInfo> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            List<MediaOrderInfo> modelList = new List<MediaOrderInfo>();
            int rowsCount = ds.Tables[0].Rows.Count;
            if (rowsCount > 0)
            {
                MediaOrderInfo model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new MediaOrderInfo();
                    if (ds.Tables[0].Rows[n]["MeidaOrderID"].ToString() != "")
                    {
                        model.MeidaOrderID = int.Parse(ds.Tables[0].Rows[n]["MeidaOrderID"].ToString());
                    }
                    model.Tel = ds.Tables[0].Rows[n]["Tel"].ToString();
                    model.Mobile = ds.Tables[0].Rows[n]["Mobile"].ToString();
                    model.PayType = ds.Tables[0].Rows[n]["PayType"].ToString();
                    model.ReceiverName = ds.Tables[0].Rows[n]["ReceiverName"].ToString();
                    if (ds.Tables[0].Rows[n]["TotalAmount"].ToString() != "")
                    {
                        model.TotalAmount = decimal.Parse(ds.Tables[0].Rows[n]["TotalAmount"].ToString());
                    }
                    if (ds.Tables[0].Rows[n]["IsDelegate"].ToString() != "")
                    {
                        if ((ds.Tables[0].Rows[n]["IsDelegate"].ToString() == "1") || (ds.Tables[0].Rows[n]["IsDelegate"].ToString().ToLower() == "true"))
                        {
                            model.IsDelegate = true;
                        }
                        else
                        {
                            model.IsDelegate = false;
                        }
                    }
                    if (ds.Tables[0].Rows[n]["IsImage"].ToString() != "")
                    {
                        if ((ds.Tables[0].Rows[n]["IsImage"].ToString() == "1") || (ds.Tables[0].Rows[n]["IsImage"].ToString().ToLower() == "true"))
                        {
                            model.IsImage = true;
                        }
                        else
                        {
                            model.IsImage = false;
                        }
                    }
                    if (ds.Tables[0].Rows[n]["IsPayment"].ToString() != "")
                    {
                        model.IsPayment = Convert.ToInt32(ds.Tables[0].Rows[n]["IsPayment"]);
                    }
                    if (ds.Tables[0].Rows[n]["PaymentUserID"].ToString() != "")
                    {
                        model.PaymentUserID = Convert.ToInt32(ds.Tables[0].Rows[n]["PaymentUserID"]);
                    }
                    model.Subject = ds.Tables[0].Rows[n]["Subject"].ToString();
                    if (ds.Tables[0].Rows[n]["WordLength"].ToString() != "")
                    {
                        model.WordLength = int.Parse(ds.Tables[0].Rows[n]["WordLength"].ToString());
                    }
                    model.WrittingURL = ds.Tables[0].Rows[n]["WrittingURL"].ToString();
                    if (ds.Tables[0].Rows[n]["BeginDate"].ToString() != "")
                    {
                        model.BeginDate = DateTime.Parse(ds.Tables[0].Rows[n]["BeginDate"].ToString());
                    }
                    if (ds.Tables[0].Rows[n]["MediaID"].ToString() != "")
                    {
                        model.MediaID = int.Parse(ds.Tables[0].Rows[n]["MediaID"].ToString());
                    }
                    if (ds.Tables[0].Rows[n]["EndDate"].ToString() != "")
                    {
                        model.EndDate = DateTime.Parse(ds.Tables[0].Rows[n]["EndDate"].ToString());
                    }
                    model.Attachment = ds.Tables[0].Rows[n]["Attachment"].ToString();
                    if (ds.Tables[0].Rows[n]["OrderID"].ToString() != "")
                    {
                        model.OrderID = int.Parse(ds.Tables[0].Rows[n]["OrderID"].ToString());
                    }
                    model.MediaName = ds.Tables[0].Rows[n]["MediaName"].ToString();
                    if (ds.Tables[0].Rows[n]["ReporterID"].ToString() != "")
                    {
                        model.ReporterID = int.Parse(ds.Tables[0].Rows[n]["ReporterID"].ToString());
                    }
                    model.ReporterName = ds.Tables[0].Rows[n]["ReporterName"].ToString();
                    model.CityName = ds.Tables[0].Rows[n]["CityName"].ToString();
                    model.CardNumber = ds.Tables[0].Rows[n]["CardNumber"].ToString();
                    model.BankName = ds.Tables[0].Rows[n]["BankName"].ToString();
                    model.BankAccountName = ds.Tables[0].Rows[n]["BankAccountName"].ToString();
                    if (ds.Tables[0].Rows[n]["ReleaseDate"] != System.DBNull.Value)
                        model.ReleaseDate = Convert.ToDateTime(ds.Tables[0].Rows[n]["ReleaseDate"]);
                    modelList.Add(model);
                }
            }
            return modelList;
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public static DataSet GetAllList()
        {
            return GetList("");
        }

        public static decimal GetMediaAmount(int orderID, string term)
        {
            return dal.GetMediaAmount(orderID, term);
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

        public static DataSet GetListByGID(string strWhere, List<SqlParameter> parms)
        {
            return dal.GetListByGID(strWhere, parms);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public static System.Collections.ArrayList GetReturnInMediaOrderSql(string MediaOrderIDs, decimal inceptPrice, string RequestorID, string RequestUserCode, string RequestUserName, string RequestEmployeeName, string filepath)
        {
            System.Collections.ArrayList ht = new System.Collections.ArrayList();
            SqlParameter[] parameters = {
                new SqlParameter("@ReturnID",SqlDbType.Int,4),
			    new SqlParameter("@inceptPrice", SqlDbType.Decimal,9),
                new SqlParameter("@MediaOrderID",SqlDbType.VarChar,1000),
			    new SqlParameter("@RequestorID", SqlDbType.Int,4),
                new SqlParameter("@RequestUserCode",SqlDbType.NVarChar,50),
                new SqlParameter("@RequestUserName",SqlDbType.NVarChar,50),
                new SqlParameter("@RequestEmployeeName",SqlDbType.NVarChar,50),
                new SqlParameter("@Attachment",SqlDbType.NVarChar,100)
                                };
            parameters[0].Direction = ParameterDirection.Output;
            parameters[1].Value = inceptPrice;
            parameters[2].Value = MediaOrderIDs;
            parameters[3].Value = RequestorID;
            parameters[4].Value = RequestUserCode;
            parameters[5].Value = RequestUserName;
            parameters[6].Value = RequestEmployeeName;
            parameters[7].Value = filepath;
            ht.Add("exec P_CreateReturnInMediaOrder @ReturnID,@inceptPrice,@MediaOrderID,@RequestorID,@RequestUserCode,@RequestUserName,@RequestEmployeeName,@Attachment");
            ht.Add(parameters);

            return ht;
        }

        public static System.Collections.ArrayList GetTaxReturnInMediaOrderSql(string MediaOrderIDs, decimal inceptPrice, string RequestorID, string RequestUserCode, string RequestUserName, string RequestEmployeeName, string filepath)
        {
            System.Collections.ArrayList ht = new System.Collections.ArrayList();
            SqlParameter[] parameters = {
                new SqlParameter("@ReturnID",SqlDbType.Int,4),
			    new SqlParameter("@inceptPrice", SqlDbType.Decimal,9),
                new SqlParameter("@MediaOrderID",SqlDbType.VarChar,1000),
			    new SqlParameter("@RequestorID", SqlDbType.Int,4),
                new SqlParameter("@RequestUserCode",SqlDbType.NVarChar,50),
                new SqlParameter("@RequestUserName",SqlDbType.NVarChar,50),
                new SqlParameter("@RequestEmployeeName",SqlDbType.NVarChar,50),
                new SqlParameter("@Attachment",SqlDbType.NVarChar,100)
                                };
            parameters[0].Direction = ParameterDirection.Output;
            parameters[1].Value = inceptPrice;
            parameters[2].Value = MediaOrderIDs;
            parameters[3].Value = RequestorID;
            parameters[4].Value = RequestUserCode;
            parameters[5].Value = RequestUserName;
            parameters[6].Value = RequestEmployeeName;
            parameters[7].Value = filepath;
            ht.Add("exec P_CreateTaxReturnInMediaOrder @ReturnID,@inceptPrice,@MediaOrderID,@RequestorID,@RequestUserCode,@RequestUserName,@RequestEmployeeName,@Attachment");
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

        public static System.Collections.ArrayList GetPRInMediaOrderSql(string MediaOrderIDs, decimal inceptPrice, string RequestorID, string RequestEmployeeName, string filepath)
        {
            System.Collections.ArrayList ht = new System.Collections.ArrayList();
            string ms = string.Empty;
            if (MediaOrderIDs.Length > 0)
                ms = MediaOrderIDs;
            SqlParameter[] parameters = {
                new SqlParameter("@GID",SqlDbType.Int,4),
			    new SqlParameter("@inceptPrice", SqlDbType.Decimal,9),
                new SqlParameter("@MediaOrderID",SqlDbType.VarChar,1000),
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

            parameters[6].Value =  "" ;
            parameters[7].Value =  "" ;
            ht.Add("exec P_CreatePRInMediaOrder @GID,@inceptPrice,@MediaOrderID,@RequestorID,@RequestEmployeeName,@Attachment,@requestor_info,@requestor_group");
            ht.Add(parameters);

            return ht;
        }

        #endregion  成员方法
        public static void DeleteByOrderID(int OrderID, System.Data.SqlClient.SqlConnection conn, System.Data.SqlClient.SqlTransaction trans)
        {
            dal.DeleteByOrderID(OrderID, conn, trans);
        }
        public static string GetProjectCodeByMeidaOrderID(string MeidaOrderIDs)
        {
            return dal.GetProjectCodeByMeidaOrderID(MeidaOrderIDs);
        }
        public static DataTable GetBatchMedia3000downInfo(string mediaIds)
        {
            return dal.GetBatchMedia3000downInfo(mediaIds);
        }
        public static DataTable GetBatchMedia3000upInfo(string returnIds)
        {
            return dal.GetBatchMedia3000upInfo(returnIds);
        }
        public static DataTable GetBatchCashInfo(string returnIds)
        {
            return dal.GetBatchCashInfo(returnIds);
        }


        public static int AddReporterIntoOrder(ESP.Purchase.Entity.GeneralInfo GeneralModel, ESP.Purchase.Entity.OrderInfo OrderModel, List<ESP.Purchase.Entity.MediaOrderInfo> MediaList, int CurrentUserID, string CurrentUserName)
        {
          
                var r = AddReporterIntoOrderInternal(GeneralModel, OrderModel, MediaList, CurrentUserID, CurrentUserName);
                return r;
          
        }

        private static int AddReporterIntoOrderInternal(ESP.Purchase.Entity.GeneralInfo GeneralModel, ESP.Purchase.Entity.OrderInfo OrderModel, List<ESP.Purchase.Entity.MediaOrderInfo> MediaList, int CurrentUserID, string CurrentUserName)
        {
            int orderID = OrderModel.id;
            decimal totalAmount = 0;
            if (OrderModel.id == 0)
            {
                orderID = OrderInfoManager.Add(OrderModel, CurrentUserID, CurrentUserName);
            }
            else
            {
                OrderInfoManager.Update(OrderModel, CurrentUserID, CurrentUserName);
            }
            foreach (ESP.Purchase.Entity.MediaOrderInfo MediaModel in MediaList)
            {
                if (MediaModel.MeidaOrderID == 0)
                {
                    MediaModel.OrderID = orderID;
                    MediaOrderManager.Add(MediaModel);
                }
                else
                {
                    MediaOrderManager.Update(MediaModel);
                }
                totalAmount = getTotalMediaAmount(orderID);
                if (totalAmount > GeneralModel.buggeted)
                {
                    throw new Exception("金额超出预算");
                }
                syncReporterData(MediaModel, CurrentUserID);
                GeneralModel.PRType = (int)ESP.Purchase.Common.PRTYpe.MediaPR;
                GeneralModel.OperationType = ESP.Purchase.Common.State.OperationTypePri;
                GeneralModel.Requisitionflow = ESP.Purchase.Common.State.requisitionflow_toR;
                GeneralModel.totalprice = totalAmount;
                ESP.Purchase.BusinessLogic.GeneralInfoManager.Update(GeneralModel);
                if (OrderModel.id == 0)
                {
                    OrderModel.id = orderID;
                }
                OrderModel.total = totalAmount;
                OrderModel.price = totalAmount;
                OrderModel.upfile = "Purchase/Requisition/Print/MediaPrint.aspx?OrderID=" + orderID.ToString() + "&Type=TotalReporter";
                ESP.Purchase.BusinessLogic.OrderInfoManager.Update(OrderModel, CurrentUserID, CurrentUserName);
            }
            return orderID;
        }

        public static int AddReporterIntoOrder(ESP.Purchase.Entity.GeneralInfo GeneralModel, ESP.Purchase.Entity.OrderInfo OrderModel, ESP.Purchase.Entity.MediaOrderInfo MediaModel, int CurrentUserID, string CurrentUserName)
        {
           
                var r = AddReporterIntoOrderInternal(GeneralModel, OrderModel, MediaModel, CurrentUserID, CurrentUserName);
                return r;
        }

        private static int AddReporterIntoOrderInternal(ESP.Purchase.Entity.GeneralInfo GeneralModel, ESP.Purchase.Entity.OrderInfo OrderModel, ESP.Purchase.Entity.MediaOrderInfo MediaModel, int CurrentUserID, string CurrentUserName)
        {
            int orderID = OrderModel.id;
            decimal totalAmount = 0;
            if (OrderModel.id == 0)
            {
                orderID = OrderInfoManager.Add(OrderModel, CurrentUserID, CurrentUserName);
            }
            else
            {
                OrderInfoManager.Update(OrderModel, CurrentUserID, CurrentUserName);
            }
            if (MediaModel.MeidaOrderID == 0)
            {
                MediaModel.OrderID = orderID;
                MediaOrderManager.Add(MediaModel);
            }
            else
            {
                MediaOrderManager.Update(MediaModel);
            }

            totalAmount = getTotalMediaAmount(orderID);
            if (totalAmount > GeneralModel.buggeted)
            {
                throw new Exception("金额超出预算");
            }
            syncReporterData(MediaModel, CurrentUserID);
            GeneralModel.PRType = (int)ESP.Purchase.Common.PRTYpe.MediaPR;
            GeneralModel.OperationType = ESP.Purchase.Common.State.OperationTypePri;
            GeneralModel.Requisitionflow = ESP.Purchase.Common.State.requisitionflow_toR;
            GeneralModel.totalprice = totalAmount;
            ESP.Purchase.BusinessLogic.GeneralInfoManager.Update(GeneralModel);
            if (OrderModel.id == 0)
            {
                OrderModel.id = orderID;
            }
            OrderModel.total = totalAmount;
            OrderModel.price = totalAmount;
            OrderModel.upfile = "Purchase/Requisition/Print/MediaPrint.aspx?OrderID=" + orderID.ToString() + "&Type=TotalReporter";
            ESP.Purchase.BusinessLogic.OrderInfoManager.Update(OrderModel, CurrentUserID, CurrentUserName);
            return orderID;
        }

        public static int DeleteReporterFromOrder(int generalID, int OrderID, int MediaOrderID, int CurrentUserID, string CurrentUserName)
        {
            
                var r = DeleteReporterFromOrderInternal(generalID, OrderID, MediaOrderID, CurrentUserID, CurrentUserName);
                return r;
        }
        private static int DeleteReporterFromOrderInternal(int generalID, int OrderID, int MediaOrderID, int CurrentUserID, string CurrentUserName)
        {
            decimal totalAmount = 0;
            try
            {
                OrderInfo OrderModel = OrderInfoManager.GetModel(OrderID);
                GeneralInfo GeneralModel = GeneralInfoManager.GetModel(generalID);
                MediaOrderManager.Delete(MediaOrderID);

                totalAmount = getTotalMediaAmount(OrderID);
                GeneralModel.totalprice = totalAmount;
                ESP.Purchase.BusinessLogic.GeneralInfoManager.Update(GeneralModel);
                OrderModel.total = totalAmount;
                OrderModel.price = totalAmount;
                ESP.Purchase.BusinessLogic.OrderInfoManager.Update(OrderModel, CurrentUserID, CurrentUserName);
            }
            catch
            {
                return -1;
            }
            return 1;
        }
        private static decimal getTotalMediaAmount(int orderID)
        {
            return dal.GetTotalMediaAmount(orderID);
        }

        private static void syncReporterData(MediaOrderInfo mediaOrder, int CurrentUserID)
        {
            string err = string.Empty;
            ESP.Media.Entity.ReportersInfo obj;
            if (mediaOrder.ReporterID == null || mediaOrder.ReporterID.Value == 0)
            {
                Hashtable ht = new Hashtable();
                ht.Add("@ReporterName", mediaOrder.ReporterName);
                ht.Add("@CardNumber", mediaOrder.CardNumber);
                DataTable dtReporter = ESP.Media.BusinessLogic.ReportersManager.GetListByMedia(" and ReporterName=@ReporterName and CardNumber=@CardNumber", ht, mediaOrder.MediaID.Value);
                if (dtReporter.Rows.Count > 0)
                    return;
                obj = new ESP.Media.Entity.ReportersInfo();
                obj.Reportername = mediaOrder.ReporterName;
                obj.Media_id = mediaOrder.MediaID == null ? 0 : mediaOrder.MediaID.Value;
                obj.CityName = mediaOrder.CityName;
                obj.Cardnumber = mediaOrder.CardNumber;
                obj.Bankname = mediaOrder.BankName;
                obj.Bankacountname = mediaOrder.BankAccountName;
                obj.Paytype = Convert.ToInt32(mediaOrder.PayType);
                obj.Usualmobile = mediaOrder.Mobile;
                obj.Tel_o = mediaOrder.Tel;
                obj.Createddate = DateTime.Now.ToString();
                ESP.Media.BusinessLogic.ReportersManager.Add(obj, null, CurrentUserID, out err);
            }
            else
            {
                //obj = ESP.Media.BusinessLogic.ReportersManager.GetModel(mediaOrder.ReporterID.Value);
                //obj.Reportername = mediaOrder.ReporterName;
                //obj.Media_id = mediaOrder.MediaID == null ? 0 : mediaOrder.MediaID.Value;
                //obj.CityName = mediaOrder.CityName;
                //obj.Cardnumber = mediaOrder.CardNumber;
                //obj.Bankname = mediaOrder.BankName;
                //obj.Bankacountname = mediaOrder.BankAccountName;
                ////obj.Bankcardname = mediaOrder.BankCardName;
                //obj.Paytype = Convert.ToInt32(mediaOrder.PayType);
                //obj.Usualmobile = mediaOrder.Mobile;
                //obj.Tel_o = mediaOrder.Tel;
                //obj.Lastmodifieddate = DateTime.Now.ToString();
                //obj.Lastmodifiedbyuserid = CurrentUserID;

                //ESP.Media.BusinessLogic.ReportersManager.Update(obj, null, CurrentUserID, out err);
            }

        }

        public static int MediaOrderOperation(IList<ESP.Purchase.Entity.MediaOrderOperationInfo> OverModelList)
        {
            return dal.MediaOrderOperation(OverModelList);
        }
        public static bool CheckReceiverExists(string receivername, string receiverID, int orderID)
        {
            return dal.CheckReceiverExists(receivername, receiverID, orderID);
        }
        public static int UpdateMediaIsPayment(List<int> mediaList, string mediaorderIDs, int PaymentUserID, SqlTransaction trans)
        {
            return dal.UpdateMediaIsPayment(mediaList, mediaorderIDs, PaymentUserID, trans);
        }
        public static int UpdateMediaIsPayment(List<int> mediaList, string meidaorderIDs, int PaymentUserID, int prid)
        {
            int ret = dal.UpdateMediaIsPayment(mediaList, meidaorderIDs, PaymentUserID);
            if (ret == 1)
            {
                //DataSet ds =dal.GetList(" meidaorderids in(" + meidaorderIDs + ") and (ispayment is null or ispayment=0)");
                //if (ds.Tables[0].Rows.Count == 0)
                //{
                //    ds = dal.GetList(" meidaorderids in(" + meidaorderIDs + ")");
                //    ESP.Purchase.Entity.GeneralInfo general = ESP.Purchase.BusinessLogic.GeneralInfoManager.GetModel(prid);
                //    ESP.Compatible.Employee emp = new ESP.Compatible.Employee(general.requestor);
                //    string retStr = "申请单："+general.PrNo+"如下记者的稿费已经付款。<br/>";
                //    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                //    {
                //        retStr += ds.Tables[0].Rows[i]["MediaName"].ToString() + "&nbsp;" + ds.Tables[0].Rows[i]["ReporterName"].ToString() + "&nbsp;" + ds.Tables[0].Rows[i]["TotalAmount"].ToString()+"<br/>";
                //    }

                //    ESP.Purchase.DataAccess.SendMailHelper.SendReporterToBiz(retStr, emp.EMail);
                //}
            }
            return ret;
        }
        public static string GetTotalAmountByUser(string MediaOrderIDs)
        {
            return dal.GetTotalAmountByUser(MediaOrderIDs);
        }

        public static bool ChangedSupplier(int supplierId, int generalId)
        {
            return dal.ChangedSupplier(supplierId, generalId);
        }
    }

}

