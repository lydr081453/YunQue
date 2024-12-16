using System;
using System.Data;
using ESP.Purchase.Common;
using System.Data.SqlClient;
using System.Collections.Generic;
using ESP.Purchase.DataAccess;
using ESP.Purchase.Entity;
using System.Text;
using ESP.Framework.Entity;
using ESP.Framework.BusinessLogic;

namespace ESP.Purchase.BusinessLogic
{
    /// <summary>
    /// 业务逻辑类PaymentPeriodManager 的摘要说明。
    /// </summary>
    public static class PaymentPeriodManager
    {
        private static PaymentPeriodDataHelper dal = new PaymentPeriodDataHelper();
        private static PeriodRecipientDataHelper perriodRecipientDal = new PeriodRecipientDataHelper();

        #region  成员方法
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public static bool Exists(int id)
        {
            return dal.Exists(id);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public static int Add(PaymentPeriodInfo model)
        {
            return dal.Add(model);
        }

        public static int Add(PaymentPeriodInfo model, SqlConnection conn, SqlTransaction trans)
        {
            return dal.Add(model,conn,trans);
        }

        public static int Add(PaymentPeriodInfo model, GeneralInfo gmodel)
        {
            using (SqlConnection conn = new SqlConnection(Common.DbHelperSQL.connectionString))
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();
                try
                {
                    int id = dal.Add(model, conn, trans);
                    GeneralInfoDataProvider.Update(gmodel, conn, trans);
                    trans.Commit();
                    return id;
                }
                catch
                {
                    trans.Rollback();
                    return 0;
                }
            }
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public static int Update(PaymentPeriodInfo model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// 当申请单只有一条付款帐期，更新帐期预计支付金额
        /// </summary>
        /// <param name="gid">申请单ID</param>
        /// <param name="orderTotalPrice">采购物品总金额</param>
        /// <param name="trans"></param>
        public static void AutoChagedExpectPaymentPrice(int gid, decimal orderTotalPrice, SqlTransaction trans)
        {
            List<PaymentPeriodInfo> list = GetModelList(" gid=" + gid, trans);
            if (list.Count == 1)
            {
                PaymentPeriodInfo model = list[0];
                model.expectPaymentPrice = orderTotalPrice;
                dal.Update(model, trans.Connection, trans);
            }
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public static int Update(PaymentPeriodInfo model, GeneralInfo gmodel)
        {
            using (SqlConnection conn = new SqlConnection(Common.DbHelperSQL.connectionString))
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();
                try
                {
                    int rows = dal.Update(model, conn, trans);
                    GeneralInfoDataProvider.Update(gmodel, conn, trans);
                    trans.Commit();
                    return rows;
                }
                catch
                {
                    trans.Rollback();
                    return 0;
                }
            }
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public static bool CommitPeriod(string paymentPeriodIds, string PaymentUserID, string RequestorID, string RequestUserCode, string RequestUserName, string RequestEmployeeName, ref int returnId, bool NeedPurchaseAudit, int IsMediaOrder)
        {
            using (SqlConnection conn = new SqlConnection(Common.DbHelperSQL.connectionString))
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();
                try
                {
                    List<PaymentPeriodInfo> models = GetModelList(string.Format(" id in ({0}) ", paymentPeriodIds));
                    foreach (PaymentPeriodInfo model in models)
                    {
                        string RID = string.Empty;
                        List<PeriodRecipientInfo> periodRecModes = PeriodRecipientManager.GetModelList(" periodId = " + model.id);
                        foreach (PeriodRecipientInfo periodRecMode in periodRecModes)
                        {
                            if (/*null != periodRecMode.recipientId && */periodRecMode.recipientId > 0)
                            {
                                RID += periodRecMode.recipientId.ToString() + ",";
                            }
                        }
                        if (RID.Length > 0)
                        {
                            RID = RID.Substring(0, RID.Length - 1);
                        }
                        int result = 0;
                        SqlParameter[] parameters = {
                        new SqlParameter("@ReturnID",SqlDbType.Int,4),
					    new SqlParameter("@id", SqlDbType.Int,4),
					    new SqlParameter("@gid", SqlDbType.Int,4),
					    new SqlParameter("@beginDate", SqlDbType.DateTime,8),
					    new SqlParameter("@endDate", SqlDbType.DateTime,8),
					    new SqlParameter("@periodRemark", SqlDbType.NVarChar,500),
					    new SqlParameter("@inceptPrice", SqlDbType.Decimal,9),
					    new SqlParameter("@inceptDate", SqlDbType.DateTime,8),
					    new SqlParameter("@periodType", SqlDbType.Int,4),
					    new SqlParameter("@periodDatumPoint", SqlDbType.Int,4),
					    new SqlParameter("@periodDay", SqlDbType.NVarChar,50),
					    new SqlParameter("@dateType", SqlDbType.Int,4),
                        new SqlParameter("@expectPaymentPrice",SqlDbType.Decimal,9),
                        new SqlParameter("@expectPaymentPercent",SqlDbType.Decimal,9),
                        new SqlParameter("@RID",SqlDbType.VarChar,200),
					    new SqlParameter("@PaymentUserID", SqlDbType.Int,4),
					    new SqlParameter("@RequestorID", SqlDbType.Int,4),
                        new SqlParameter("@RequestUserCode",SqlDbType.NVarChar,50),
                        new SqlParameter("@RequestUserName",SqlDbType.NVarChar,50),
                        new SqlParameter("@RequestEmployeeName",SqlDbType.NVarChar,50),
                        new SqlParameter("@NeedPurchaseAudit",SqlDbType.Bit),
                        new SqlParameter("@IsMediaOrder", SqlDbType.Int,4)
                                        };
                        parameters[0].Direction = ParameterDirection.Output;
                        parameters[1].Value = model.id;
                        parameters[2].Value = model.gid;
                        parameters[3].Value = model.beginDate;
                        parameters[4].Value = model.endDate;
                        parameters[5].Value = DataAccess.GeneralInfoDataProvider.GetPNDes(model.gid);
                        parameters[6].Value = model.inceptPrice;
                        parameters[7].Value = model.inceptDate;
                        parameters[8].Value = model.periodType;
                        parameters[9].Value = model.periodDatumPoint;
                        parameters[10].Value = model.periodDay;
                        parameters[11].Value = model.dateType;
                        parameters[12].Value = model.expectPaymentPrice;
                        parameters[13].Value = model.expectPaymentPercent;
                        parameters[14].Value = RID;
                        parameters[15].Value = PaymentUserID;
                        parameters[16].Value = RequestorID;
                        parameters[17].Value = RequestUserCode;
                        parameters[18].Value = RequestUserName;
                        parameters[19].Value = RequestEmployeeName;
                        parameters[20].Value = NeedPurchaseAudit;
                        parameters[21].Value = IsMediaOrder;

                        if (returnId == 0)
                            returnId = Common.DbHelperSQL.RunProcedure("P_CreateReturnInPurchase", parameters, 0, out result);
                        else
                            Common.DbHelperSQL.RunProcedure("P_CreateReturnInPurchase", parameters, out result);
                    }
                    trans.Commit();
                    return true;
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    return false;
                }
            }
        }

        //public static bool CommitPeriod(ESP.Purchase.Entity.GeneralInfo generalmodel,string paymentPeriodIds, string PaymentUserID, string RequestorID, string RequestUserCode, string RequestUserName, string RequestEmployeeName, ref int returnId,System.Data.SqlClient.SqlConnection conn,System.Data.SqlClient.SqlTransaction trans)
        //{
        //    string PNno = string.Empty;
        //    object RccCount = null;
        //    object NowDate = null;//生成PN号时使用，获取当前日期
        //    object ReturnCount = null;//PN单号记录表行数
        //    string strRccCount = string.Empty;//PN当前计数值
        //    ESP.Finance.Entity.ReturnInfo returnModel =null;
        //      try
        //        {
        //            #region 生成新的PN单号
        //            NowDate = DbHelperSQL.GetSingle("Select Substring(Convert(Varchar(6),GetDate(),112),3,4);");
        //            ReturnCount = DbHelperSQL.GetSingle("select count(ReturnCount) from dbo.F_ReturnCounter where ReturnBaseTime like '%" + NowDate.ToString() + "%';", conn, trans);
        //            if (Convert.ToInt32(ReturnCount) > 0)
        //            {
        //                RccCount = DbHelperSQL.GetSingle("select ReturnCount from F_ReturnCounter where ReturnBaseTime like '%" + NowDate.ToString() + "%' ");
        //                DbHelperSQL.ExecuteSql("UPDATE F_ReturnCounter SET [ReturnCount] = " + (Convert.ToInt32(RccCount) + 1).ToString() + " WHERE ReturnBaseTime like '%" + NowDate.ToString() + "%' ");
        //            }
        //            else
        //            {
        //                DbHelperSQL.ExecuteSql("INSERT INTO F_ReturnCounter (ReturnBaseTime,ReturnCount) VALUES ('" + NowDate.ToString() + "',2)", conn, trans, null);
        //                RccCount = "1";
        //            }
        //            strRccCount = RccCount.ToString();
        //            while (strRccCount.Length < 4)
        //                strRccCount = "0" + strRccCount;
        //            PNno = "PN" + NowDate.ToString() + strRccCount.ToString();
        //            #endregion
        //          returnModel=new ESP.Finance.Entity.ReturnInfo();
        //          returnModel.ReturnCode = PNno;
        //          returnModel.ProjectID = generalmodel.Project_id;
        //          returnModel.ProjectCode = generalmodel.project_code;
        //          returnModel.ReturnContent = string.Empty;
        //          returnModel.PreFee = overModel.TotalPrice;
        //          returnModel.ReturnStatus = (int)ESP.Finance.Utility.PaymentStatus.MajorAudit;
        //          returnModel.PRID = overModel.GeneralID;
        //          returnModel.PRNo = overModel.PRNO;
        //          returnModel.RequestorID = Convert.ToInt32(overModel.CurrentUserID);
        //          returnModel.RequestEmployeeName = overModel.CurrentUserEmpName;
        //          returnModel.RequestUserCode = overModel.CurrentUserCode;
        //          returnModel.RequestUserName = overModel.CurrentUserName;
        //          returnModel.RequestDate = DateTime.Now;
        //          returnModel.Attachment = overModel.FileName;
        //          returnModel.ReturnType = generalmodel.PRType;
        //          returnModel.PaymentUserID = branch.FirstFinanceID;
        //          emp = new ESP.Compatible.Employee(branch.FirstFinanceID);
        //          returnModel.PaymentEmployeeName = emp.Name;
        //          returnModel.PaymentCode = emp.ID;
        //          returnModel.PaymentUserName = emp.ITCode;
        //          returnModel.PreBeginDate = PaymentBeginDate;
        //          returnModel.PreEndDate = PaymentEndDate;
        //          returnModel.MediaOrderIDs = overModel.MediaOrderIDS;
        //          returnModel.SupplierBankAccount = overModel.BankAccount;
        //          returnModel.SupplierBankName = overModel.BankName;
        //          returnModel.SupplierName = overModel.BankAccountName;
        //          ESP.Finance.BusinessLogic.ReturnManager.Add(returnModel, trans);
        //           return true;
        //        }
        //        catch
        //        {
        //            return false;
        //        }
        //    }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public static bool DeletePeriod(PaymentPeriodInfo model)
        {
            using (SqlConnection conn = new SqlConnection(Common.DbHelperSQL.connectionString))
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();
                try
                {
                    dal.Update(model, conn, trans);
                    RecipientInfo recModel = null;
                    List<PeriodRecipientInfo> periodRecModes = ESP.Purchase.BusinessLogic.PeriodRecipientManager.GetModelList(" periodId = " + model.id);
                    foreach (PeriodRecipientInfo periodRecMode in periodRecModes)
                    {
                        recModel = RecipientDataHelper.GetModel(periodRecMode.recipientId, trans);
                        GeneralInfo generalModel = GeneralInfoManager.GetModel(model.gid);
                        RecipientDataHelper.updateConfirmPayment(recModel.Id.ToString(), State.recipentConfirm_Supplier, conn, trans);
                        perriodRecipientDal.Delete(periodRecMode.id, conn, trans);
                    }
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

        public static void DeletePeriod(PaymentPeriodInfo model, SqlTransaction trans)
        {
            dal.Update(model, trans.Connection, trans);
            RecipientInfo recModel = null;
            List<PeriodRecipientInfo> periodRecModes = ESP.Purchase.BusinessLogic.PeriodRecipientManager.GetModelList(" periodId = " + model.id);
            foreach (PeriodRecipientInfo periodRecMode in periodRecModes)
            {
                recModel = RecipientDataHelper.GetModel(periodRecMode.recipientId, trans);
                GeneralInfo generalModel = GeneralInfoManager.GetModel(model.gid);
                RecipientDataHelper.updateConfirmPayment(recModel.Id.ToString(), State.recipentConfirm_Supplier, trans.Connection, trans);
                perriodRecipientDal.Delete(periodRecMode.id, trans.Connection, trans);
            }

        }

        /// <summary>
        /// 添加或更新付款账期中的预付款
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        //public static int UpdateOrAddPrepay(PaymentPeriodInfo model)
        //{
        //    int id = 0;
        //    if (ExistsPrepay(model.gid, ref id))
        //    {
        //        model.id = id;
        //        return Update(model);
        //    }
        //    else
        //        return Add(model);
        //}

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public static void Delete(int id)
        {
            dal.Delete(id);
        }

        public static bool Delete(int id, GeneralInfo gmodel)
        {
            using (SqlConnection conn = new SqlConnection(Common.DbHelperSQL.connectionString))
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();
                try
                {
                    dal.Delete(id, trans);
                    GeneralInfoDataProvider.Update(gmodel, conn, trans);
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

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public static PaymentPeriodInfo GetModel(int id)
        {
            return dal.GetModel(id);
        }
        public static PaymentPeriodInfo GetModelByPN(int pnid)
        {
            return dal.GetModelByReturnId(pnid);
        }
        public static PaymentPeriodInfo GetModelByPN(int pnid,SqlTransaction trans)
        {
            return dal.GetModelByReturnId(pnid, trans);
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public static DataSet GetList(string strWhere)
        {
            return dal.GetList(strWhere);
        }
        public static List<PaymentPeriodInfo> GetList(int[] prIds)
        {
            if (prIds == null || prIds.Length == 0)
                return new List<PaymentPeriodInfo>();

            var sql = new StringBuilder("select * FROM T_PaymentPeriod where gid in (");
            sql.Append(prIds[0]);
            for (var i = 1; i < prIds.Length; i++)
            {
                sql.Append(",").Append(prIds[i]);
            }
            sql.Append(")");

            using (var reader = ESP.Finance.DataAccess.DbHelperSQL.ExecuteReader(sql.ToString()))
            {
                return MyPhotoUtility.CBO.FillCollection<PaymentPeriodInfo>(reader);
            }
        }
        /// <summary>
        /// 获得申请单的付款账期列表
        /// </summary>
        /// <param name="gid"></param>
        /// <returns></returns>
        public static DataTable GetPaymentPeriodList(int gid)
        {
            DataSet ds = dal.GetList(" gid=" + gid);
            return ds.Tables[0];
        }

        /// <summary>
        /// 获得申请单的付款账期和预付款列表
        /// </summary>
        /// <param name="gid"></param>
        /// <returns></returns>
        public static DataTable GetPaymentPeriodAllList(int gid)
        {
            DataSet ds = dal.GetList(" gid=" + gid + " and (inceptPrice = 0 or inceptPrice is null)");
            return ds.Tables[0];
        }

        /// <summary>
        /// 更新付款账期和收货单
        /// </summary>
        /// <param name="periodId"></param>
        /// <param name="recipientIds"></param>
        /// <returns></returns>
        public static bool UpdatePaymentPeriodAndRecipient(PaymentPeriodInfo model, string recipientIds)
        {
            using (SqlConnection conn = new SqlConnection(Common.DbHelperSQL.connectionString))
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();
                try
                {

                    if (!string.IsNullOrEmpty(recipientIds))
                    {
                        //插入关联表
                        string[] ids = recipientIds.Split(',');
                        PeriodRecipientInfo periodRecObj = null;
                        for (int i = 0; i < ids.Length; i++)
                        {
                            periodRecObj = new PeriodRecipientInfo();
                            periodRecObj.periodId = model.id;
                            periodRecObj.recipientId = int.Parse(ids[i]);
                            perriodRecipientDal.Add(periodRecObj, conn, trans);
                        }
                        //更新收货单
                        RecipientDataHelper.updateConfirmPayment(recipientIds, State.recipentConfirm_PaymentCommit, conn, trans);
                    }
                    //更新付款账期
                    dal.Update(model, conn, trans);


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

        public static bool UpdatePaymentPeriodAndRecipient(PaymentPeriodInfo model, string recipientIds, System.Data.SqlClient.SqlConnection conn, System.Data.SqlClient.SqlTransaction trans)
        {
            try
            {

                if (!string.IsNullOrEmpty(recipientIds))
                {
                    //插入关联表
                    string[] ids = recipientIds.Split(',');
                    PeriodRecipientInfo periodRecObj = null;
                    for (int i = 0; i < ids.Length; i++)
                    {
                        periodRecObj = new PeriodRecipientInfo();
                        periodRecObj.periodId = model.id;
                        periodRecObj.recipientId = int.Parse(ids[i]);
                        perriodRecipientDal.Add(periodRecObj, conn, trans);
                    }
                    //更新收货单
                    RecipientDataHelper.updateConfirmPayment(recipientIds, State.recipentConfirm_PaymentCommit, conn, trans);
                }
                //更新付款账期
                dal.Update(model, conn, trans);
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public static DataSet GetAllList()
        {
            return dal.GetList("");
        }

        [AjaxPro.AjaxMethod]
        public static string GetMydateStr(string date)
        {
            if (!string.IsNullOrEmpty(date))
            {
                DateTime mydate = DateTime.Parse(date);
                DateTime mydateTemp = mydate.AddDays(7);
                return mydateTemp.ToString("yyyy-MM-dd");
            }
            return "";
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        /// <param name="id">The id.</param>
        public static void DeleteAllByGeneralId(int gid, SqlTransaction trans)
        {
            dal.DeleteAllByGeneralId(gid, trans);
        }

        public static DataSet GetGeneralPaymentList(string strWhere, List<SqlParameter> parms)
        {
            return dal.GetGeneralPaymentList(strWhere, parms);
        }

        public static DataSet GetGeneralPaymentList(string strWhere)
        {
            return dal.GetGeneralPaymentList(strWhere, null);
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public static List<PaymentPeriodInfo> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            List<PaymentPeriodInfo> modelList = new List<PaymentPeriodInfo>();
            int rowsCount = ds.Tables[0].Rows.Count;
            if (rowsCount > 0)
            {
                PaymentPeriodInfo model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new PaymentPeriodInfo();
                    if (ds.Tables[0].Rows[n]["id"].ToString() != "")
                    {
                        model.id = int.Parse(ds.Tables[0].Rows[n]["id"].ToString());
                    }
                    model.periodDay = ds.Tables[0].Rows[n]["periodDay"].ToString();
                    if (ds.Tables[0].Rows[n]["dateType"].ToString() != "")
                    {
                        model.dateType = int.Parse(ds.Tables[0].Rows[n]["dateType"].ToString());
                    }
                    if (ds.Tables[0].Rows[n]["expectPaymentPrice"].ToString() != "")
                    {
                        model.expectPaymentPrice = decimal.Parse(ds.Tables[0].Rows[n]["expectPaymentPrice"].ToString());
                    }
                    if (ds.Tables[0].Rows[n]["expectPaymentPercent"].ToString() != "")
                    {
                        model.expectPaymentPercent = decimal.Parse(ds.Tables[0].Rows[n]["expectPaymentPercent"].ToString());
                    }
                    if (ds.Tables[0].Rows[n]["finallyPaymentPrice"].ToString() != "")
                    {
                        model.FinallyPaymentPrice = decimal.Parse(ds.Tables[0].Rows[n]["finallyPaymentPrice"].ToString());
                    }
                    if (ds.Tables[0].Rows[n]["finallyPaymentDate"].ToString() != "")
                    {
                        model.FinallyPaymentDate = DateTime.Parse(ds.Tables[0].Rows[n]["finallyPaymentDate"].ToString());
                    }
                    model.FinallyPaymentUserId = ds.Tables[0].Rows[n]["finallyPaymentUserId"].ToString();
                    model.FinallyPaymentUserName = ds.Tables[0].Rows[n]["finallyPaymentUserName"].ToString();
                    if (ds.Tables[0].Rows[n]["Status"].ToString() != "")
                    {
                        model.Status = int.Parse(ds.Tables[0].Rows[n]["Status"].ToString());
                    }
                    if (ds.Tables[0].Rows[n]["gid"].ToString() != "")
                    {
                        model.gid = int.Parse(ds.Tables[0].Rows[n]["gid"].ToString());
                    }
                    if (ds.Tables[0].Rows[n]["beginDate"].ToString() != "")
                    {
                        model.beginDate = DateTime.Parse(ds.Tables[0].Rows[n]["beginDate"].ToString());
                    }
                    if (ds.Tables[0].Rows[n]["endDate"].ToString() != "")
                    {
                        model.endDate = DateTime.Parse(ds.Tables[0].Rows[n]["endDate"].ToString());
                    }
                    model.periodRemark = ds.Tables[0].Rows[n]["periodRemark"].ToString();
                    if (ds.Tables[0].Rows[n]["inceptPrice"].ToString() != "")
                    {
                        model.inceptPrice = decimal.Parse(ds.Tables[0].Rows[n]["inceptPrice"].ToString());
                    }
                    if (ds.Tables[0].Rows[n]["inceptDate"].ToString() != "")
                    {
                        model.inceptDate = DateTime.Parse(ds.Tables[0].Rows[n]["inceptDate"].ToString());
                    }
                    if (ds.Tables[0].Rows[n]["periodType"].ToString() != "")
                    {
                        model.periodType = int.Parse(ds.Tables[0].Rows[n]["periodType"].ToString());
                    }
                    if (ds.Tables[0].Rows[n]["periodDatumPoint"].ToString() != "")
                    {
                        model.periodDatumPoint = int.Parse(ds.Tables[0].Rows[n]["periodDatumPoint"].ToString());
                    }
                    if (ds.Tables[0].Rows[n]["ReturnId"].ToString() != "")
                    {
                        model.ReturnId = int.Parse(ds.Tables[0].Rows[n]["ReturnId"].ToString());
                    }
                    if (ds.Tables[0].Rows[n]["ReturnCode"].ToString() != "")
                    {
                        model.ReturnCode = ds.Tables[0].Rows[n]["ReturnCode"].ToString();
                    }

                    if (ds.Tables[0].Rows[0]["TaxTypes"].ToString() != "")
                    {
                        model.TaxTypes = int.Parse(ds.Tables[0].Rows[0]["TaxTypes"].ToString());
                    }
                    modelList.Add(model);
                }
            }
            return modelList;
        }

        public static List<PaymentPeriodInfo> GetModelList(string strWhere, System.Data.SqlClient.SqlTransaction trans)
        {
            if (trans == null)
                return null;
            DataSet ds = dal.GetList(strWhere, trans);
            List<PaymentPeriodInfo> modelList = new List<PaymentPeriodInfo>();
            int rowsCount = ds.Tables[0].Rows.Count;
            if (rowsCount > 0)
            {
                PaymentPeriodInfo model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new PaymentPeriodInfo();
                    if (ds.Tables[0].Rows[n]["id"].ToString() != "")
                    {
                        model.id = int.Parse(ds.Tables[0].Rows[n]["id"].ToString());
                    }
                    model.periodDay = ds.Tables[0].Rows[n]["periodDay"].ToString();
                    if (ds.Tables[0].Rows[n]["dateType"].ToString() != "")
                    {
                        model.dateType = int.Parse(ds.Tables[0].Rows[n]["dateType"].ToString());
                    }
                    if (ds.Tables[0].Rows[n]["expectPaymentPrice"].ToString() != "")
                    {
                        model.expectPaymentPrice = decimal.Parse(ds.Tables[0].Rows[n]["expectPaymentPrice"].ToString());
                    }
                    if (ds.Tables[0].Rows[n]["expectPaymentPercent"].ToString() != "")
                    {
                        model.expectPaymentPercent = decimal.Parse(ds.Tables[0].Rows[n]["expectPaymentPercent"].ToString());
                    }
                    if (ds.Tables[0].Rows[n]["finallyPaymentPrice"].ToString() != "")
                    {
                        model.FinallyPaymentPrice = decimal.Parse(ds.Tables[0].Rows[n]["finallyPaymentPrice"].ToString());
                    }
                    if (ds.Tables[0].Rows[n]["finallyPaymentDate"].ToString() != "")
                    {
                        model.FinallyPaymentDate = DateTime.Parse(ds.Tables[0].Rows[n]["finallyPaymentDate"].ToString());
                    }
                    model.FinallyPaymentUserId = ds.Tables[0].Rows[n]["finallyPaymentUserId"].ToString();
                    model.FinallyPaymentUserName = ds.Tables[0].Rows[n]["finallyPaymentUserName"].ToString();
                    if (ds.Tables[0].Rows[n]["Status"].ToString() != "")
                    {
                        model.Status = int.Parse(ds.Tables[0].Rows[n]["Status"].ToString());
                    }
                    if (ds.Tables[0].Rows[n]["gid"].ToString() != "")
                    {
                        model.gid = int.Parse(ds.Tables[0].Rows[n]["gid"].ToString());
                    }
                    if (ds.Tables[0].Rows[n]["beginDate"].ToString() != "")
                    {
                        model.beginDate = DateTime.Parse(ds.Tables[0].Rows[n]["beginDate"].ToString());
                    }
                    if (ds.Tables[0].Rows[n]["endDate"].ToString() != "")
                    {
                        model.endDate = DateTime.Parse(ds.Tables[0].Rows[n]["endDate"].ToString());
                    }
                    model.periodRemark = ds.Tables[0].Rows[n]["periodRemark"].ToString();
                    if (ds.Tables[0].Rows[n]["inceptPrice"].ToString() != "")
                    {
                        model.inceptPrice = decimal.Parse(ds.Tables[0].Rows[n]["inceptPrice"].ToString());
                    }
                    if (ds.Tables[0].Rows[n]["inceptDate"].ToString() != "")
                    {
                        model.inceptDate = DateTime.Parse(ds.Tables[0].Rows[n]["inceptDate"].ToString());
                    }
                    if (ds.Tables[0].Rows[n]["periodType"].ToString() != "")
                    {
                        model.periodType = int.Parse(ds.Tables[0].Rows[n]["periodType"].ToString());
                    }
                    if (ds.Tables[0].Rows[n]["periodDatumPoint"].ToString() != "")
                    {
                        model.periodDatumPoint = int.Parse(ds.Tables[0].Rows[n]["periodDatumPoint"].ToString());
                    }
                    if (ds.Tables[0].Rows[0]["TaxTypes"].ToString() != "")
                    {
                        model.TaxTypes = int.Parse(ds.Tables[0].Rows[0]["TaxTypes"].ToString());
                    }

                    modelList.Add(model);
                }
            }
            return modelList;
        }

        /// <summary>
        /// 取得一个pr单里面所有的已提交至财务系统的付款总金额.
        /// </summary>
        /// <param name="generalid">The generalid.</param>
        /// <returns></returns>
        public static decimal getPaymentSum(int generalid)
        {
            return dal.getPaymentSum(generalid);
        }

        /// <summary>
        /// 获得申请单下已使用并未和收货关联的预付帐期
        /// </summary>
        /// <param name="generalid"></param>
        /// <returns></returns>
        public static decimal getPayemntYF(int generalid)
        {
            return dal.getPayemntYF(generalid);
        }

        public static decimal getPaymentSum(int generalid, System.Data.SqlClient.SqlConnection conn, System.Data.SqlClient.SqlTransaction trans)
        {
            return dal.getPaymentSum(generalid, conn, trans);
        }
        /// <summary>
        /// 取得一个pr单里面所有的已提交至财务系统的付款总金额.
        /// </summary>
        /// <param name="generalid">The generalid.</param>
        /// <param name="status">The status.</param>
        /// <returns></returns>
        public static decimal getPaymentSum(int generalid, int status)
        {
            return dal.getPaymentSum(generalid, status);
        }
        #endregion  成员方法
        public static decimal getPaymentTotal(int generalid)
        {
            return dal.getPaymentTotal(generalid);
        }
        public static decimal getPaymentTypeTotal(int TypeID, int projectid, string projectcode)
        {
            return dal.getPaymentTypeTotal(TypeID, projectid, projectcode);
        }
        public static int CreatePaymentToFinance(ESP.Purchase.Entity.GeneralInfo generalInfo, int paymentPeriodId, string recipientIds, List<ESP.Purchase.Entity.RecipientInfo> recipientList, int CurrentUserID, string CurrentUserEmpName, string CurrentUserCode, string CurrentUserName)
        {
            int ret = 0;
            PaymentPeriodInfo paymentPeriod = GetModel(paymentPeriodId);
            //收货单列表
            decimal recipientPrice = 0; //收货金额
            foreach (RecipientInfo model in recipientList)
            {
                recipientPrice += model.RecipientAmount;
            }
            if (recipientIds == "" && recipientPrice == 0)
            {
                paymentPeriod.inceptPrice = paymentPeriod.expectPaymentPrice;
                recipientPrice = paymentPeriod.expectPaymentPrice;
            }
            paymentPeriod.inceptDate = DateTime.Now;
            paymentPeriod.Status = State.PaymentStatus_wait;

            using (SqlConnection conn = new SqlConnection(Common.DbHelperSQL.connectionString))
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();
                try
                {

                    //付款金额设置
                    //检查是否是金额不符收货的付款
                    if (!string.IsNullOrEmpty(recipientIds) && RecipientManager.IsUnsureRecipient(recipientIds))
                    {
                        paymentPeriod.inceptPrice = RecipientManager.getRecipientSum(paymentPeriod.gid, conn, trans) - PaymentPeriodManager.getPaymentSum(paymentPeriod.gid, conn, trans);
                    }
                    else
                    {
                        if (recipientPrice > paymentPeriod.expectPaymentPrice) //如果所选收货金额大于账期金额，只收账期金额
                        {
                            paymentPeriod.inceptPrice = paymentPeriod.expectPaymentPrice;

                        }
                        else //如果所选收货金额小于账期金额，只收收货金额
                        {
                            paymentPeriod.inceptPrice = recipientPrice;
                        }
                    }

                    //更新付款账期和收货单
                    if (!UpdatePaymentPeriodAndRecipient(paymentPeriod, recipientIds, conn, trans))
                        ret = -1;
                    else
                    {
                        //记录操作日志
                    //    ESP.Logging.Logger.Add(string.Format("{0}对T_PaymentPeriod表中的id={1}和T_Recipient表中的id={2}进行{3}操作", CurrentUserEmpName, paymentPeriod.id, recipientIds, "关联"), "创建付款申请");
                    }
                    try
                    {
                        GeneralInfoManager.Update(generalInfo);
                        //记录操作日志
                   //     ESP.Logging.Logger.Add(string.Format("{0}对T_GeneralInfo表中的id={1}进行{2}操作", CurrentUserEmpName, generalInfo.id, "付款申请处更新账户"), "编辑");
                    }
                    catch
                    {
                        ret = -1;
                    }
                    int periodId = paymentPeriodId;

                   // int[] depts = new ESP.Compatible.Employee(CurrentUserID).GetDepartmentIDs();
                   // OperationAuditManageInfo manageModel = OperationAuditManageManager.GetModelByDepId(depts[0], conn, trans);
                   // int returnId = 0;
                    //if (PaymentPeriodManager.CommitPeriod(periodId.ToString(), manageModel.DirectorId.ToString(), CurrentUserID.ToString(), CurrentUserCode, CurrentUserCode, CurrentUserEmpName, ref returnId,conn,trans))
                    //{
                    //    //记录操作日志
                    //    ESP.Logging.Logger.Add(string.Format("{0}对T_PaymentPeriod表中的id={1}进行{2}操作", CurrentUserEmpName, periodId, "提交付款申请"), "付款申请");

                    //}
                    //else
                    //{
                    //    ret = -1;
                    //}
                    trans.Commit();
                    return ret;
                }
                catch
                {
                    trans.Rollback();
                    return 0;
                }
            }

        }

    }
}

