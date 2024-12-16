using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using ESP.Purchase.Common;
using ESP.Purchase.Entity;

namespace ESP.Purchase.DataAccess
{
    /// <summary>
    /// 数据访问类MediaOrderDataProvider。
    /// </summary>
    public class MediaOrderDataProvider
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MediaOrderDataProvider"/> class.
        /// </summary>
        public MediaOrderDataProvider()
        { }

        #region  成员方法

        /// <summary>
        /// 得到最大ID
        /// </summary>
        public int GetMaxId()
        {
            return DbHelperSQL.GetMaxID("MeidaOrderID", "T_MediaOrder");
        }


        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int MeidaOrderID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from T_MediaOrder");
            strSql.Append(" where MeidaOrderID=@MeidaOrderID ");
            SqlParameter[] parameters = {
					new SqlParameter("@MeidaOrderID", SqlDbType.Int,4)};
            parameters[0].Value = MeidaOrderID;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public void Add(MediaOrderInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into T_MediaOrder(");
            strSql.Append("Tel,Mobile,PayType,TotalAmount,IsDelegate,IsImage,Subject,WordLength,WrittingURL,BeginDate,MediaID,EndDate,Attachment,OrderID,MediaName,ReporterID,ReporterName,CityName,CardNumber,BankName,BankAccountName,Status,ImageSize,ReceiverName,ReleaseDate,IsTax,IsPayment,PaymentUserID)");
            strSql.Append(" values (");
            strSql.Append("@Tel,@Mobile,@PayType,@TotalAmount,@IsDelegate,@IsImage,@Subject,@WordLength,@WrittingURL,@BeginDate,@MediaID,@EndDate,@Attachment,@OrderID,@MediaName,@ReporterID,@ReporterName,@CityName,@CardNumber,@BankName,@BankAccountName,@Status,@ImageSize,@ReceiverName,@ReleaseDate,@IsTax,@IsPayment,@PaymentUserID)");
            SqlParameter[] parameters = {
					new SqlParameter("@Tel", SqlDbType.NVarChar,50),
					new SqlParameter("@Mobile", SqlDbType.NVarChar,50),
					new SqlParameter("@PayType", SqlDbType.NVarChar,50),
					new SqlParameter("@TotalAmount", SqlDbType.Decimal,9),
					new SqlParameter("@IsDelegate", SqlDbType.Bit,1),
					new SqlParameter("@IsImage", SqlDbType.Bit,1),
					new SqlParameter("@Subject", SqlDbType.NVarChar,200),
					new SqlParameter("@WordLength", SqlDbType.Int,4),
					new SqlParameter("@WrittingURL", SqlDbType.NVarChar,50),
					new SqlParameter("@BeginDate", SqlDbType.DateTime),
					new SqlParameter("@MediaID", SqlDbType.Int,4),
					new SqlParameter("@EndDate", SqlDbType.DateTime),
					new SqlParameter("@Attachment", SqlDbType.NVarChar,200),
					new SqlParameter("@OrderID", SqlDbType.Int,4),
					new SqlParameter("@MediaName", SqlDbType.NVarChar,50),
					new SqlParameter("@ReporterID", SqlDbType.Int,4),
					new SqlParameter("@ReporterName", SqlDbType.NVarChar,50),
					new SqlParameter("@CityName", SqlDbType.NVarChar,50),
					new SqlParameter("@CardNumber", SqlDbType.NVarChar,50),
					new SqlParameter("@BankName", SqlDbType.NVarChar,50),
					new SqlParameter("@BankAccountName", SqlDbType.NVarChar,50),
                    new SqlParameter("@Status",SqlDbType.Int,4),
                    new SqlParameter("@ImageSize", SqlDbType.NVarChar,50),
                    new SqlParameter("@ReceiverName", SqlDbType.NVarChar,50),
new SqlParameter("@ReleaseDate", SqlDbType.DateTime,8),
new SqlParameter("@IsTax", SqlDbType.Bit),
new SqlParameter("@IsPayment", SqlDbType.Int),
new SqlParameter("@PaymentUserID",SqlDbType.Int)
                                        };
            parameters[0].Value = model.Tel;
            parameters[1].Value = model.Mobile;
            parameters[2].Value = model.PayType;
            parameters[3].Value = model.TotalAmount;
            parameters[4].Value = model.IsDelegate;
            parameters[5].Value = model.IsImage;
            parameters[6].Value = model.Subject;
            parameters[7].Value = model.WordLength;
            parameters[8].Value = model.WrittingURL;
            parameters[9].Value = model.BeginDate;
            parameters[10].Value = model.MediaID;
            parameters[11].Value = model.EndDate;
            parameters[12].Value = model.Attachment;
            parameters[13].Value = model.OrderID;
            parameters[14].Value = model.MediaName;
            parameters[15].Value = model.ReporterID;
            parameters[16].Value = model.ReporterName;
            parameters[17].Value = model.CityName;
            parameters[18].Value = model.CardNumber;
            parameters[19].Value = model.BankName;
            parameters[20].Value = model.BankAccountName;
            parameters[21].Value = model.Status;
            parameters[22].Value = model.ImageSize;
            parameters[23].Value = model.ReceiverName;
            parameters[24].Value = model.ReleaseDate;
            parameters[25].Value = model.IsTax;
            parameters[26].Value = model.IsPayment;
            parameters[27].Value = model.PaymentUserID;
            DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }

        public int UpdateMediaIsPayment(List<int> mediaList, string mediaorderIDs, int PaymentUserID, SqlTransaction trans)
        {
            if (!string.IsNullOrEmpty(mediaorderIDs))
            {
                DbHelperSQL.ExecuteSql("update t_mediaorder set ispayment=0,paymentuserid=0 where meidaorderid in(" + mediaorderIDs + ")", trans.Connection, trans);
            }
            if (mediaList != null && mediaList.Count > 0)
            {
                for (int i = 0; i < mediaList.Count; i++)
                {
                    ESP.Purchase.Entity.MediaOrderInfo mediaModel = ESP.Purchase.BusinessLogic.MediaOrderManager.GetModel(Convert.ToInt32(mediaList[i].ToString()), trans);
                    if (mediaModel.IsPayment == null || mediaModel.IsPayment.Value == 0)
                        DbHelperSQL.ExecuteSql("update t_mediaorder set ispayment=1,paymentUserID=" + PaymentUserID.ToString() + " where meidaorderid=" + mediaList[i].ToString(), trans.Connection, trans, null);
                }
            }
            return 1;
        }

        public int UpdateMediaIsPayment(List<int> mediaList, string mediaorderIDs, int PaymentUserID)
        {
            using (SqlConnection conn = new SqlConnection(DbHelperSQL.connectionString))
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();
                try
                {
                    if (!string.IsNullOrEmpty(mediaorderIDs))
                    {
                        DbHelperSQL.ExecuteSql("update t_mediaorder set ispayment=0,paymentuserid=0 where meidaorderid in(" + mediaorderIDs + ")");
                    }
                    for (int i = 0; i < mediaList.Count; i++)
                    {
                        ESP.Purchase.Entity.MediaOrderInfo mediaModel = ESP.Purchase.BusinessLogic.MediaOrderManager.GetModel(Convert.ToInt32(mediaList[i].ToString()), trans);
                        if (mediaModel.IsPayment == null || mediaModel.IsPayment.Value == 0)
                            DbHelperSQL.ExecuteSql("update t_mediaorder set ispayment=1,paymentUserID=" + PaymentUserID.ToString() + " where meidaorderid=" + mediaList[i].ToString(), conn, trans, null);
                    }
                    trans.Commit();
                    return 1;
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    ESP.Logging.Logger.Add(ex.Message, "Is Payment", ESP.Logging.LogLevel.Error, ex);
                    return 0;
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        public string GetSubTotalByPaymentUser(string MediaOrderIDs)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select paymentUserID ");
            strSql.Append(" FROM T_MediaOrder where meidaorderid in(" + MediaOrderIDs + ") group by PaymentUserID");
            string tmpStr = string.Empty;
            string retStr = string.Empty;
            DataSet ds = DbHelperSQL.Query(strSql.ToString());
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                tmpStr = "select sum(totalamount) from t_mediaorder where meidaorderid in(" + MediaOrderIDs + ") and paymentuserid=" + ds.Tables[0].Rows[i].ItemArray[0].ToString() + "";
                object obj = DbHelperSQL.GetSingle(tmpStr);
                if (ds.Tables[0].Rows[i].ItemArray[0] == System.DBNull.Value || ds.Tables[0].Rows[i].ItemArray[0].ToString() == "" || ds.Tables[0].Rows[i].ItemArray[0].ToString() == "0")
                {
                    retStr += "未支付金额小计：" + obj.ToString() + "<br/>";
                }
                else
                {
                    ESP.Compatible.Employee emp = new ESP.Compatible.Employee(Convert.ToInt32(ds.Tables[0].Rows[i].ItemArray[0].ToString()));
                    retStr += emp.Name + "支付金额小计：" + obj.ToString() + "<br/>";
                }
            }
            tmpStr = "select sum(totalamount) from t_mediaorder where meidaorderid in(" + MediaOrderIDs + ") ";
            object obj2 = DbHelperSQL.GetSingle(tmpStr);
            retStr += "总计：" + obj2.ToString();
            return retStr;
        }
        private int UpdateStatusBatch(string IDs, SqlConnection conn, SqlTransaction tans)
        {
            //如果列表中存在已经更新过的行，返回0
            object objcount = DbHelperSQL.GetSingle("select count(*) from T_MediaOrder where Status!=0 and MeidaOrderID in (" + IDs + ")", conn, tans);
            if (Convert.ToInt32(objcount) == 0)
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("update T_MediaOrder set ");
                strSql.Append("Status=@Status");
                strSql.Append(" where MeidaOrderID in (" + IDs + ")");
                SqlParameter[] parameters = {
					new SqlParameter("@Status", SqlDbType.Int,4)               
};
                parameters[0].Value = 1;
                return DbHelperSQL.ExecuteSql(strSql.ToString(), conn, tans, parameters);
            }
            return 0;
        }

        private int UpdatePaymentStatusBatch(string IDs, int returnID, string ReturnCode, SqlConnection conn, SqlTransaction tans)
        {
            //如果列表中存在已经更新过的行，返回0
            object objcount = DbHelperSQL.GetSingle("select count(*) from T_PaymentPeriod where Status!=0 and id in (" + IDs + ")", conn, tans);
            if (Convert.ToInt32(objcount) == 0)
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("UPDATE T_PaymentPeriod set ");
                strSql.Append("Status=@Status,returnID=@returnID,ReturnCode=@ReturnCode ");
                strSql.Append(" where id in (" + IDs + ")");
                SqlParameter[] parameters = {
					new SqlParameter("@Status", SqlDbType.Int,4),  
                    new SqlParameter("@returnID", SqlDbType.Int,4),
                    new SqlParameter("@ReturnCode", SqlDbType.NVarChar,50)
                                        };
                parameters[0].Value = 2;
                parameters[1].Value = returnID;
                parameters[2].Value = ReturnCode;
                return DbHelperSQL.ExecuteSql(strSql.ToString(), conn, tans, parameters);
            }
            return 0;
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public void Update(MediaOrderInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update T_MediaOrder set ");
            strSql.Append("Tel=@Tel,");
            strSql.Append("Mobile=@Mobile,");
            strSql.Append("PayType=@PayType,");
            strSql.Append("TotalAmount=@TotalAmount,");
            strSql.Append("IsDelegate=@IsDelegate,");
            strSql.Append("IsImage=@IsImage,");
            strSql.Append("Subject=@Subject,");
            strSql.Append("WordLength=@WordLength,");
            strSql.Append("WrittingURL=@WrittingURL,");
            strSql.Append("BeginDate=@BeginDate,");
            strSql.Append("MediaID=@MediaID,");
            strSql.Append("EndDate=@EndDate,");
            strSql.Append("Attachment=@Attachment,");
            strSql.Append("OrderID=@OrderID,");
            strSql.Append("MediaName=@MediaName,");
            strSql.Append("ReporterID=@ReporterID,");
            strSql.Append("ReporterName=@ReporterName,");
            strSql.Append("CityName=@CityName,");
            strSql.Append("CardNumber=@CardNumber,");
            strSql.Append("BankName=@BankName,");
            strSql.Append("BankAccountName=@BankAccountName,");
            strSql.Append("Status=@Status,");
            strSql.Append("ImageSize=@ImageSize,");
            strSql.Append("ReceiverName=@ReceiverName,");
            strSql.Append("ReleaseDate=@ReleaseDate,");
            strSql.Append("IsTax=@IsTax,");
            strSql.Append("IsPayment=@IsPayment,");
            strSql.Append("PaymentUserID=@PaymentUserID");
            strSql.Append(" where MeidaOrderID=@MeidaOrderID ");
            SqlParameter[] parameters = {
					new SqlParameter("@MeidaOrderID", SqlDbType.Int,4),
					new SqlParameter("@Tel", SqlDbType.NVarChar,50),
					new SqlParameter("@Mobile", SqlDbType.NVarChar,50),
					new SqlParameter("@PayType", SqlDbType.NVarChar,50),
					new SqlParameter("@TotalAmount", SqlDbType.Decimal,9),
					new SqlParameter("@IsDelegate", SqlDbType.Bit,1),
					new SqlParameter("@IsImage", SqlDbType.Bit,1),
					new SqlParameter("@Subject", SqlDbType.NVarChar,200),
					new SqlParameter("@WordLength", SqlDbType.Int,4),
					new SqlParameter("@WrittingURL", SqlDbType.NVarChar,50),
					new SqlParameter("@BeginDate", SqlDbType.DateTime),
					new SqlParameter("@MediaID", SqlDbType.Int,4),
					new SqlParameter("@EndDate", SqlDbType.DateTime),
					new SqlParameter("@Attachment", SqlDbType.NVarChar,200),
					new SqlParameter("@OrderID", SqlDbType.Int,4),
					new SqlParameter("@MediaName", SqlDbType.NVarChar,50),
					new SqlParameter("@ReporterID", SqlDbType.Int,4),
					new SqlParameter("@ReporterName", SqlDbType.NVarChar,50),
					new SqlParameter("@CityName", SqlDbType.NVarChar,50),
					new SqlParameter("@CardNumber", SqlDbType.NVarChar,50),
					new SqlParameter("@BankName", SqlDbType.NVarChar,50),
					new SqlParameter("@BankAccountName", SqlDbType.NVarChar,50),
                    new SqlParameter("@Status",SqlDbType.Int,4),
                    new SqlParameter("@ImageSize", SqlDbType.NVarChar,50),
                    new SqlParameter("@ReceiverName", SqlDbType.NVarChar,50),
                                        new SqlParameter("@ReleaseDate", SqlDbType.DateTime,8),
                    new SqlParameter("@IsTax", SqlDbType.Bit),
                    new SqlParameter("@IsPayment", SqlDbType.Int),
                    new SqlParameter("@PaymentUserID",SqlDbType.Int)
                                        };
            parameters[0].Value = model.MeidaOrderID;
            parameters[1].Value = model.Tel;
            parameters[2].Value = model.Mobile;
            parameters[3].Value = model.PayType;
            parameters[4].Value = model.TotalAmount;
            parameters[5].Value = model.IsDelegate;
            parameters[6].Value = model.IsImage;
            parameters[7].Value = model.Subject;
            parameters[8].Value = model.WordLength;
            parameters[9].Value = model.WrittingURL;
            parameters[10].Value = model.BeginDate;
            parameters[11].Value = model.MediaID;
            parameters[12].Value = model.EndDate;
            parameters[13].Value = model.Attachment;
            parameters[14].Value = model.OrderID;
            parameters[15].Value = model.MediaName;
            parameters[16].Value = model.ReporterID;
            parameters[17].Value = model.ReporterName;
            parameters[18].Value = model.CityName;
            parameters[19].Value = model.CardNumber;
            parameters[20].Value = model.BankName;
            parameters[21].Value = model.BankAccountName;
            parameters[22].Value = model.Status;
            parameters[23].Value = model.ImageSize;
            parameters[24].Value = model.ReceiverName;
            parameters[25].Value = model.ReleaseDate;
            parameters[26].Value = model.IsTax;
            parameters[27].Value = model.IsPayment;
            parameters[28].Value = model.PaymentUserID;
            DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public void Delete(int MeidaOrderID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete T_MediaOrder ");
            strSql.Append(" where MeidaOrderID=@MeidaOrderID ");
            SqlParameter[] parameters = {
					new SqlParameter("@MeidaOrderID", SqlDbType.Int,4)};
            parameters[0].Value = MeidaOrderID;

            DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }

        public void DeleteByOrderID(int OrderID, System.Data.SqlClient.SqlConnection conn, System.Data.SqlClient.SqlTransaction trans)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete T_MediaOrder ");
            strSql.Append(" where OrderID=@OrderID ");
            SqlParameter[] parameters = {
					new SqlParameter("@OrderID", SqlDbType.Int,4)};
            parameters[0].Value = OrderID;

            DbHelperSQL.ExecuteSql(strSql.ToString(), conn, trans, parameters);
        }

        public DataTable GetBatchMedia3000downInfo(string mediaIds)
        {
            if (string.IsNullOrEmpty(mediaIds))
                return null;
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" select r.ReturnID,r.returncode,a.prno,a.project_code,a1.begindate,c.releasedate,a.requestorname,department,c.reportername,c.receivername,c.medianame,c.CityName, c.BankName,");
            strSql.Append("c.totalamount,c.bankaccountname,c.cardnumber from t_generalinfo a ");
            strSql.Append(" join f_return r on a.id=r.prid ");
            strSql.Append(" join t_paymentperiod a1 on a.id=a1.gid");
            strSql.Append(" join t_orderinfo b on a.id=b.general_id ");
            strSql.Append(" join t_mediaorder c on b.id=c.orderid ");
            strSql.Append(" where c.meidaorderid in(");
            strSql.Append(mediaIds);
            strSql.Append(")");
            strSql.Append(" order by r.ReturnID,c.receivername, c.bankaccountname");
            return DbHelperSQL.Query(strSql.ToString()).Tables[0];
        }

        public DataTable GetBatchMedia3000upInfo(string returnIds)
        {
            if (string.IsNullOrEmpty(returnIds))
                return null;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select r.ReturnID,r.returncode,a.prno,a.project_code,a1.begindate,c.releasedate,a.requestorname,department,c.reportername,c.receivername,c.medianame,c.CityName, c.BankName,");
            strSql.Append("c.totalamount,c.bankaccountname,c.cardnumber ");
            strSql.Append(" from t_generalinfo a");
            //strSql.Append(" from (select * from t_generalinfo where id ");
            //strSql.Append(" in(select oldprid from t_mediapredithis where newprid in(");
            //strSql.Append(returnIds);
            //strSql.Append("))) a");
            strSql.Append(" join f_return r on a.id=r.prid");
            strSql.Append(" join t_paymentperiod a1 on a.id=a1.gid");
            strSql.Append(" join t_orderinfo b on a.id=b.general_id ");
            strSql.Append(" join t_mediaorder c on b.id=c.orderid ");
            strSql.Append(" where a.id in(");
            strSql.Append(returnIds);
            strSql.Append(")");
            strSql.Append(" order by r.ReturnID, c.receivername, c.bankaccountname");
            return DbHelperSQL.Query(strSql.ToString()).Tables[0];
        }

        public DataTable GetBatchCashInfo(string returnIds)
        {
            if (string.IsNullOrEmpty(returnIds))
                return null;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select a.returncode,b.prno,a.projectcode,a.returnpredate,b.requestorname,b.departmentid,b.department,");
            strSql.Append("a.suppliername,''desctiprtion,a.prefee,a.supplierbankaccount,b.supplier_address,a.returnid from f_return a");
            strSql.Append(" join t_generalinfo b on a.prid=b.id");
            strSql.Append(" where returnid in(");
            strSql.Append(returnIds);
            strSql.Append(")");
            strSql.Append(" order by a.ReturnID");
            return DbHelperSQL.Query(strSql.ToString()).Tables[0];
        }
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public MediaOrderInfo GetModel(int MeidaOrderID, SqlTransaction trans)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 MeidaOrderID,Tel,Mobile,PayType,TotalAmount,IsDelegate,IsImage,Subject,WordLength,WrittingURL,BeginDate,MediaID,EndDate,Attachment,OrderID,MediaName,ReporterID,ReporterName,CityName,CardNumber,BankName,BankAccountName,Status,ImageSize,ReceiverName,releaseDate,istax,ispayment,PaymentUserID from T_MediaOrder ");
            strSql.Append(" where MeidaOrderID=@MeidaOrderID ");
            SqlParameter[] parameters = {
					new SqlParameter("@MeidaOrderID", SqlDbType.Int,4)};
            parameters[0].Value = MeidaOrderID;

            MediaOrderInfo model = new MediaOrderInfo();
            DataSet ds;
            if (trans != null)
                ds = DbHelperSQL.Query(strSql.ToString(), trans.Connection, trans, parameters);
            else
                ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["MeidaOrderID"].ToString() != "")
                {
                    model.MeidaOrderID = int.Parse(ds.Tables[0].Rows[0]["MeidaOrderID"].ToString());
                }
                model.Tel = ds.Tables[0].Rows[0]["Tel"].ToString();
                model.Mobile = ds.Tables[0].Rows[0]["Mobile"].ToString();
                model.PayType = ds.Tables[0].Rows[0]["PayType"].ToString();
                if (ds.Tables[0].Rows[0]["TotalAmount"].ToString() != "")
                {
                    model.TotalAmount = decimal.Parse(ds.Tables[0].Rows[0]["TotalAmount"].ToString());
                }
                if (ds.Tables[0].Rows[0]["IsDelegate"].ToString() != "")
                {
                    if ((ds.Tables[0].Rows[0]["IsDelegate"].ToString() == "1") || (ds.Tables[0].Rows[0]["IsDelegate"].ToString().ToLower() == "true"))
                    {
                        model.IsDelegate = true;
                    }
                    else
                    {
                        model.IsDelegate = false;
                    }
                }
                if (ds.Tables[0].Rows[0]["IsImage"].ToString() != "")
                {
                    if ((ds.Tables[0].Rows[0]["IsImage"].ToString() == "1") || (ds.Tables[0].Rows[0]["IsImage"].ToString().ToLower() == "true"))
                    {
                        model.IsImage = true;
                    }
                    else
                    {
                        model.IsImage = false;
                    }
                }
                if (ds.Tables[0].Rows[0]["IsPayment"].ToString() != "")
                {
                    model.IsPayment = Convert.ToInt32(ds.Tables[0].Rows[0]["IsPayment"]);
                }
                if (ds.Tables[0].Rows[0]["PaymentUserID"].ToString() != "")
                {
                    model.PaymentUserID = Convert.ToInt32(ds.Tables[0].Rows[0]["PaymentUserID"]);
                }
                model.Subject = ds.Tables[0].Rows[0]["Subject"].ToString();
                if (ds.Tables[0].Rows[0]["WordLength"].ToString() != "")
                {
                    model.WordLength = int.Parse(ds.Tables[0].Rows[0]["WordLength"].ToString());
                }
                model.WrittingURL = ds.Tables[0].Rows[0]["WrittingURL"].ToString();
                if (ds.Tables[0].Rows[0]["BeginDate"].ToString() != "")
                {
                    model.BeginDate = DateTime.Parse(ds.Tables[0].Rows[0]["BeginDate"].ToString());
                }
                if (ds.Tables[0].Rows[0]["MediaID"].ToString() != "")
                {
                    model.MediaID = int.Parse(ds.Tables[0].Rows[0]["MediaID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["EndDate"].ToString() != "")
                {
                    model.EndDate = DateTime.Parse(ds.Tables[0].Rows[0]["EndDate"].ToString());
                }
                model.Attachment = ds.Tables[0].Rows[0]["Attachment"].ToString();
                if (ds.Tables[0].Rows[0]["OrderID"].ToString() != "")
                {
                    model.OrderID = int.Parse(ds.Tables[0].Rows[0]["OrderID"].ToString());
                }
                model.MediaName = ds.Tables[0].Rows[0]["MediaName"].ToString();
                if (ds.Tables[0].Rows[0]["ReporterID"].ToString() != "")
                {
                    model.ReporterID = int.Parse(ds.Tables[0].Rows[0]["ReporterID"].ToString());
                }
                model.ReceiverName = ds.Tables[0].Rows[0]["ReceiverName"].ToString();
                if (ds.Tables[0].Rows[0]["ReleaseDate"].ToString() != "")
                    model.ReleaseDate = Convert.ToDateTime(ds.Tables[0].Rows[0]["ReleaseDate"].ToString());
                if (ds.Tables[0].Rows[0]["IsTax"].ToString() != "")
                    model.IsTax = ds.Tables[0].Rows[0]["IsTax"].ToString() == "0" ? false : true;
                model.ReporterName = ds.Tables[0].Rows[0]["ReporterName"].ToString();
                model.CityName = ds.Tables[0].Rows[0]["CityName"].ToString();
                model.CardNumber = ds.Tables[0].Rows[0]["CardNumber"].ToString();
                model.BankName = ds.Tables[0].Rows[0]["BankName"].ToString();
                model.BankAccountName = ds.Tables[0].Rows[0]["BankAccountName"].ToString();
                if (ds.Tables[0].Rows[0]["Status"] != "")
                    model.Status = Convert.ToInt32(ds.Tables[0].Rows[0]["Status"]);
                model.ImageSize = ds.Tables[0].Rows[0]["ImageSize"].ToString();
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
            strSql.Append("select MeidaOrderID,Tel,Mobile,PayType,TotalAmount,IsDelegate,IsImage,Subject,WordLength,WrittingURL,BeginDate,MediaID,EndDate,Attachment,OrderID,MediaName,ReporterID,ReporterName,CityName,CardNumber,BankName,BankAccountName,Status,ImageSize,ReceiverName,releaseDate,istax,ispayment,PaymentUserID ");
            strSql.Append(" FROM T_MediaOrder ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by receivername, bankaccountname");
            return DbHelperSQL.Query(strSql.ToString());
        }

        public DataSet GetBaiduList(string conn)
        {
            using (SqlConnection connection = new SqlConnection(conn))
            {
                DataSet ds = new DataSet();
                try
                {
                    StringBuilder strSql = new StringBuilder();
                    //strSql.Append("select returncode ");
                    //strSql.Append(" FROM f_return where returncode is not null and returncode <>''");


                    strSql.Append("select * from t_datainfo where dataid in(");
                    strSql.Append("select general_id from t_orderinfo where producttype in(");
                    strSql.Append("select typeid from t_type where auditorid=14410)");
                    strSql.Append(") and datatype=0");
                    connection.Open();
                    SqlDataAdapter command = new SqlDataAdapter(strSql.ToString(), connection);
                    command.Fill(ds, "ds");
                    connection.Close();
                }
                catch (System.Data.SqlClient.SqlException ex)
                {
                    connection.Close();
                    throw new Exception(ex.Message);
                }
                return ds;
            }

        }

        public DataSet GetMediaOrderList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select sum(TotalAmount) as TotalAmount,OrderID,CardNumber,ReceiverName  ");
            strSql.Append(" FROM T_MediaOrder ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" group by OrderID,CardNumber,ReceiverName");
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 获得数据列表通过主表流水
        /// </summary>
        /// <param name="strWhere">The STR where.</param>
        /// <returns></returns>
        public DataSet GetListByGID(string strWhere, List<SqlParameter> parms)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select a.id as gid,a.requestor,a.requestorName,a.prno,a.project_code,c.*,b.billtype from t_generalinfo as a  ");
            strSql.Append("inner join T_OrderInfo as b on a.id=b.general_id   ");
            strSql.Append("inner join T_MediaOrder as c on b.id=c.orderid ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            if (parms == null)
                return DbHelperSQL.Query(strSql.ToString());
            else
                return DbHelperSQL.Query(strSql.ToString(), parms.ToArray());
        }

        public decimal GetMediaAmount(int orderID, string term)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select sum(totalAmount) ");
            strSql.Append(" FROM T_MediaOrder where orderID=" + orderID.ToString());
            if (!string.IsNullOrEmpty(term))
            {
                strSql.Append(term);
            }
            return Convert.ToDecimal(DbHelperSQL.GetSingle(strSql.ToString()));
        }

        public DataSet GetListByGID(string strWhere)
        {
            return GetListByGID(strWhere, null);
        }

        public string GetProjectCodeByMeidaOrderID(string MeidaOrderIDs)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select top 1 ISNULL(a.project_code,'') as pc ");
            strSql.Append("from T_GeneralInfo AS A inner join T_OrderInfo as b on a.id=b.general_id   ");
            strSql.Append("inner join T_MediaOrder as c on b.id=c.orderid ");

            if (MeidaOrderIDs.Trim() != "")
            {
                strSql.Append(" where C.MeidaOrderID in (" + MeidaOrderIDs + ") ");
            }
            string pc = string.Empty;
            DataSet ds = DbHelperSQL.Query(strSql.ToString());
            if (null != ds && ds.Tables.Count > 0)
            {
                pc = ds.Tables[0].Rows[0]["pc"].ToString();
            }
            return pc;
        }

        /*
        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        public DataSet GetList(int PageSize,int PageIndex,string strWhere)
        {
            SqlParameter[] parameters = {
                    new SqlParameter("@tblName", SqlDbType.VarChar, 255),
                    new SqlParameter("@fldName", SqlDbType.VarChar, 255),
                    new SqlParameter("@PageSize", SqlDbType.Int),
                    new SqlParameter("@PageIndex", SqlDbType.Int),
                    new SqlParameter("@IsReCount", SqlDbType.Bit),
                    new SqlParameter("@OrderType", SqlDbType.Bit),
                    new SqlParameter("@strWhere", SqlDbType.VarChar,1000),
                    };
            parameters[0].Value = "T_MediaOrder";
            parameters[1].Value = "ID";
            parameters[2].Value = PageSize;
            parameters[3].Value = PageIndex;
            parameters[4].Value = 0;
            parameters[5].Value = 0;
            parameters[6].Value = strWhere;	
            return DbHelperSQL.RunProcedure("UP_GetRecordByPage",parameters,"ds");
        }*/

        #endregion  成员方法

        public decimal GetTotalMediaAmount(int orderID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select sum(totalAmount) ");
            strSql.Append(" FROM T_MediaOrder where orderID=" + orderID.ToString());
            return Convert.ToDecimal(DbHelperSQL.GetSingle(strSql.ToString()));
        }

        public bool CheckReceiverExists(string receivername, string receiverID, int orderID)
        {
            StringBuilder strSql = new StringBuilder();
            StringBuilder strSql2 = new StringBuilder();

            int total = 0;
            int total2 = 0;
            strSql.Append("select count(*) ");
            strSql.Append(" FROM T_MediaOrder where orderID=" + orderID.ToString() + " and receivername='" + receivername + "'");
            total = Convert.ToInt32(DbHelperSQL.GetSingle(strSql.ToString()));
            strSql2.Append("select count(*) ");
            strSql2.Append(" FROM T_MediaOrder where orderID=" + orderID.ToString() + " and receivername='" + receivername + "' and cardnumber='" + receiverID + "'");
            total2 = Convert.ToInt32(DbHelperSQL.GetSingle(strSql2.ToString()));
            if (total == total2)
                return true;
            else
                return false;
        }
        /// <summary>
        /// 媒体稿费报销单处理
        /// </summary>
        /// <param name="OverModelList"></param>
        /// <param name="LessModelList"></param>
        /// <param name="TaxModelList"></param>
        /// <returns></returns>
        public int MediaOrderOperation(IList<ESP.Purchase.Entity.MediaOrderOperationInfo> OverModelList)
        {
            int ret = 0;
            string[] IDs = null;//mediaOrderID的数组
            int batchCount = 0;//批次更新mediaorder 影响的行数
            List<System.Data.SqlClient.SqlParameter> parmList;
            System.Data.SqlClient.SqlParameter p1 = null;
            string term;
            string PNno = string.Empty;
            object RccCount = null;
            DateTime PaymentBeginDate = DateTime.Now;//3000以下付款起始日期
            DateTime PaymentEndDate = DateTime.Now;//3000以下付款起结束日期
            object NowDate = null;//生成PN号时使用，获取当前日期
            object ReturnCount = null;//PN单号记录表行数
            string strRccCount = string.Empty;//PN当前计数值
            ESP.Finance.Entity.BranchInfo branch = null;//3000以下根据分公司查审批人
            IList<ESP.Purchase.Entity.PaymentPeriodInfo> paymentlist = null;//查原始PR单付款账期
            ESP.Finance.Entity.ReturnInfo returnModel = null;//3000以下新PN model
            int ReturnID = 0;//3000以下新PN model的ID
            object datainfoID = null;//3000以下新PN 的授权
            ESP.Compatible.Employee emp = null;//3000以下新PN 审核人详细信息
            ESP.Compatible.Employee emp2 = null;
            GeneralInfo NewModel = null;
            int NewGID = 0;
            IList<ESP.Purchase.Entity.MediaPREditHisInfo> relationList = null;

            using (SqlConnection conn = new SqlConnection(DbHelperSQL.connectionString))
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();
                try
                {
                    foreach (ESP.Purchase.Entity.MediaOrderOperationInfo overModel in OverModelList)
                    {
                        //原始PR单Model类
                        GeneralInfo generalmodel = ESP.Purchase.BusinessLogic.GeneralInfoManager.GetModel(overModel.GeneralID, trans);
                        ESP.ITIL.BusinessLogic.申请单业务设置.媒体稿费对私处理(ref generalmodel);
                        ESP.Purchase.BusinessLogic.GeneralInfoManager.Update(generalmodel, conn, trans);
                        int newProductType = 0;
                        switch (overModel.Flag)//根据参数类的Flag判定是何种单据
                        {
                            case 1:
                                #region 3000以上
                                //3000以上的新PR Model
                                NewModel = new GeneralInfo();
                                //新PR Model赋值
                                NewModel.Project_id = generalmodel.Project_id;
                                NewModel.project_code = generalmodel.project_code;
                                NewModel.requestor = Convert.ToInt32(overModel.CurrentUserID);
                                NewModel.requestorname = overModel.CurrentUserEmpName;
                                NewModel.app_date = DateTime.Now;
                                NewModel.project_descripttion = generalmodel.project_descripttion;
                                //NewModel.buggeted = getNewValue(overModel.TotalPrice * (decimal)1.1);
                                //NewModel.totalprice = getNewValue(overModel.TotalPrice * (decimal)1.1);
                                NewModel.MediaOldAmount = getNewValue(overModel.TotalPrice);
                                NewModel.sow2 = overModel.FileName;
                                NewModel.NewMediaOrderIDs = overModel.MediaOrderIDS;
                                NewModel.lasttime = DateTime.Now;
                                NewModel.status = 0;
                                NewModel.PRType = (int)ESP.Purchase.Common.PRTYpe.PR_MediaFA;
                                NewModel.Departmentid = generalmodel.Departmentid;
                                NewModel.Department = generalmodel.Department;
                                NewModel.requestor_info = generalmodel.requestor_info;
                                NewModel.requestor_group = generalmodel.requestor_group;
                                SetNewPRInfo(overModel,generalmodel, NewModel);
                                //带事务插入PR，返回新PR ID
                                NewGID = ESP.Purchase.BusinessLogic.GeneralInfoManager.Add(NewModel, conn, trans);
                                NewModel.id = NewGID;
                                addLinkInfo(NewModel, trans,ref newProductType);
                                ret = NewGID;
                                term = " OldPrID=@OldPrID";
                                parmList = new List<SqlParameter>();
                                p1 = new SqlParameter("@OldPRID", SqlDbType.Int, 4);
                                p1.Value = overModel.GeneralID;
                                parmList.Add(p1);
                                //检查新老PR单对照表是否存在数据
                                relationList = ESP.Purchase.BusinessLogic.MediaPREditHisManager.GetList(term, conn, trans, parmList);
                                //如果存在对照表，更新
                                if (relationList.Count > 0)
                                {
                                    relationList[0].NewPRId = NewGID;
                                    relationList[0].OrderType = 1;
                                    ESP.Purchase.BusinessLogic.MediaPREditHisManager.Update(relationList[0], conn, trans);
                                }
                                else//否则插入
                                {
                                    ESP.Purchase.Entity.MediaPREditHisInfo relationModel = new MediaPREditHisInfo();
                                    relationModel.OldPRId = overModel.GeneralID;
                                    relationModel.NewPRId = NewGID;
                                    relationModel.OrderType = 1;
                                    ESP.Purchase.BusinessLogic.MediaPREditHisManager.ADD(relationModel, conn, trans);
                                }
                                IDs = overModel.MediaOrderIDS.Split(',');//更新Media order表之前ID的数量

                                batchCount = UpdateStatusBatch(overModel.MediaOrderIDS, conn, trans);//更新影响的行数
                                //$$$$$ PR单媒介3000以上PR申请

                               // ESP.Purchase.BusinessLogic.GeneralInfoManager.InsertPrCostRecords(NewModel, null, trans, State.PR_CostRecordsType.PR单媒介3000以上PR申请, generalmodel.id, newProductType);
                                if (IDs.Length != batchCount)//如果数量不一致，rollback
                                {
                                    trans.Rollback();
                                    return -1;
                                }
                                #endregion
                                break;
                            case 2:
                                #region 3000以下pr单

                                #region 生成新的PN单号
                                NowDate = DbHelperSQL.GetSingle("Select Substring(Convert(Varchar(6),GetDate(),112),3,4);", conn, trans);
                                ReturnCount = DbHelperSQL.GetSingle("select count(ReturnCount) from dbo.F_ReturnCounter where ReturnBaseTime like '%" + NowDate.ToString() + "%';", conn, trans);
                                if (Convert.ToInt32(ReturnCount) > 0)
                                {
                                    RccCount = DbHelperSQL.GetSingle("select ReturnCount from F_ReturnCounter where ReturnBaseTime like '%" + NowDate.ToString() + "%' ", conn, trans);
                                    DbHelperSQL.ExecuteSql("UPDATE F_ReturnCounter SET [ReturnCount] = " + (Convert.ToInt32(RccCount) + 1).ToString() + " WHERE ReturnBaseTime like '%" + NowDate.ToString() + "%' ", conn, trans);
                                }
                                else
                                {
                                    DbHelperSQL.ExecuteSql("INSERT INTO F_ReturnCounter (ReturnBaseTime,ReturnCount) VALUES ('" + NowDate.ToString() + "',2)", conn, trans, null);
                                    RccCount = "1";
                                }
                                strRccCount = RccCount.ToString();
                                while (strRccCount.Length < 4)
                                    strRccCount = "0" + strRccCount;
                                PNno = "PN" + NowDate.ToString() + strRccCount.ToString();
                                #endregion
                                //根据项目号的第一位判定财务第一级审核人是谁
                                branch = ESP.Finance.BusinessLogic.BranchManager.GetModelByCode(generalmodel.project_code.Substring(0, 1), trans);
                                //查付款账期，如果存在取其默认的付款日期给PN
                                paymentlist = ESP.Purchase.BusinessLogic.PaymentPeriodManager.GetModelList(" gid=" + generalmodel.id.ToString(), trans);
                                if (paymentlist != null && paymentlist.Count > 0)
                                {
                                    PaymentBeginDate = paymentlist[0].beginDate;
                                    PaymentEndDate = paymentlist[0].endDate;
                                }
                                //3000以下生成新的PN model
                                returnModel = new ESP.Finance.Entity.ReturnInfo();
                                //给PN Model 赋值
                                returnModel.ReturnCode = PNno;
                                returnModel.ProjectID = generalmodel.Project_id;
                                returnModel.ProjectCode = generalmodel.project_code;
                                returnModel.DepartmentID = generalmodel.Departmentid;
                                returnModel.DepartmentName = generalmodel.Department;
                                returnModel.ReturnContent = string.Empty;
                                returnModel.PreFee = overModel.TotalPrice;
                                returnModel.ReturnStatus = (int)ESP.Finance.Utility.PaymentStatus.MajorAudit;
                                returnModel.PRID = overModel.GeneralID;
                                returnModel.PRNo = overModel.PRNO;
                                returnModel.RequestorID = Convert.ToInt32(overModel.CurrentUserID);
                                returnModel.RequestEmployeeName = overModel.CurrentUserEmpName;
                                returnModel.RequestUserCode = overModel.CurrentUserCode;
                                returnModel.RequestUserName = overModel.CurrentUserName;
                                returnModel.RequestDate = DateTime.Now;
                                returnModel.Attachment = overModel.FileName;
                                returnModel.ReturnType = generalmodel.PRType;
                                returnModel.PaymentUserID = branch.FirstFinanceID;
                                emp = new ESP.Compatible.Employee(branch.FirstFinanceID);
                                returnModel.PaymentEmployeeName = emp.Name;
                                returnModel.PaymentCode = emp.ID;
                                returnModel.PaymentUserName = emp.ITCode;
                                returnModel.PreBeginDate = PaymentBeginDate;
                                returnModel.PreEndDate = PaymentEndDate;
                                returnModel.MediaOrderIDs = overModel.MediaOrderIDS;
                                returnModel.SupplierBankAccount = overModel.BankAccount;
                                returnModel.SupplierBankName = overModel.BankName;
                                returnModel.SupplierName = overModel.BankAccountName;
                                returnModel.PaymentTypeID = 3;
                                returnModel.PaymentTypeName = "银行转账";
                                //插入PN Model 并取PN ID
                                ReturnID = ESP.Finance.BusinessLogic.ReturnManager.Add(returnModel, trans);
                                ret = ReturnID;
                                //插入PN 的审批列表
                                DbHelperSQL.ExecuteSql("insert into F_ReturnAuditHist(ReturnID,AuditorUserID,AuditorUserName,AuditorUserCode,AuditorEmployeeName,auditeStatus,auditType)  values(" + ReturnID.ToString() + "," + emp.SysID + ",'" + emp.ITCode + "','" + emp.ID + "','" + emp.Name + "',0,11)", conn, trans, null);
                                //插入授权表
                                DbHelperSQL.ExecuteSql("insert into t_datainfo(datatype,dataid) values(5," + ReturnID.ToString() + ");", conn, trans, null);
                                //取授权表ID
                                datainfoID = DbHelperSQL.GetSingle("select id from t_datainfo where datatype=5 and dataid=" + ReturnID.ToString(), conn, trans, null);
                                //申请人授权
                                DbHelperSQL.ExecuteSql("insert into T_dataPermission(datainfoid,userid,iseditor,isviewer) values(" + datainfoID.ToString() + "," + overModel.CurrentUserID + ",1,1)", conn, trans, null);
                                //审批人授权
                                DbHelperSQL.ExecuteSql("insert into T_dataPermission(datainfoid,userid,iseditor,isviewer) values(" + datainfoID.ToString() + "," + branch.FirstFinanceID.ToString() + ",1,1)", conn, trans, null);

                                term = " OldPrID=@OldPrID";
                                parmList = new List<SqlParameter>();
                                p1 = new SqlParameter("@OldPRID", SqlDbType.Int, 4);
                                p1.Value = overModel.GeneralID;
                                parmList.Add(p1);
                                //检查是否做过新旧PR PN关联对照
                                relationList = ESP.Purchase.BusinessLogic.MediaPREditHisManager.GetList(term, conn, trans, parmList);
                                //如果存在对照表，更新
                                if (relationList.Count > 0)
                                {
                                    relationList[0].NewPNId = ReturnID;
                                    relationList[0].OrderType = 1;
                                    ESP.Purchase.BusinessLogic.MediaPREditHisManager.Update(relationList[0], conn, trans);
                                }
                                else//否则插入
                                {
                                    ESP.Purchase.Entity.MediaPREditHisInfo relationModel = new MediaPREditHisInfo();
                                    relationModel.OldPRId = overModel.GeneralID;
                                    relationModel.NewPNId = ReturnID;
                                    relationModel.OrderType = 1;
                                    ESP.Purchase.BusinessLogic.MediaPREditHisManager.ADD(relationModel, conn, trans);
                                }
                                IDs = overModel.MediaOrderIDS.Split(',');//更新Media order表之前ID的数量
                                batchCount = UpdateStatusBatch(overModel.MediaOrderIDS, conn, trans); //更新影响的行数
                                //$$$$$ PR单个人3000以下PN申请

                             //   ESP.Purchase.BusinessLogic.GeneralInfoManager.InsertPrCostRecords(generalmodel, returnModel, trans, State.PR_CostRecordsType.PR单媒介3000以下PN申请, 0, 0);
                                if (IDs.Length != batchCount)//如果数量不一致，rollback
                                {
                                    trans.Rollback();
                                    return -1;
                                }

                                #endregion
                                break;
                            case 3:
                                #region  需要税单的PR单
                                #region 生成新的PN单号
                                NowDate = DbHelperSQL.GetSingle("Select Substring(Convert(Varchar(6),GetDate(),112),3,4);", conn, trans);
                                ReturnCount = DbHelperSQL.GetSingle("select count(ReturnCount) from dbo.F_ReturnCounter where ReturnBaseTime like '%" + NowDate.ToString() + "%';", conn, trans);
                                if (Convert.ToInt32(ReturnCount) > 0)
                                {
                                    RccCount = DbHelperSQL.GetSingle("select ReturnCount from F_ReturnCounter where ReturnBaseTime like '%" + NowDate.ToString() + "%' ", conn, trans);
                                    DbHelperSQL.ExecuteSql("UPDATE F_ReturnCounter SET [ReturnCount] = " + (Convert.ToInt32(RccCount) + 1).ToString() + " WHERE ReturnBaseTime like '%" + NowDate.ToString() + "%' ", conn, trans);
                                }
                                else
                                {
                                    DbHelperSQL.ExecuteSql("INSERT INTO F_ReturnCounter (ReturnBaseTime,ReturnCount) VALUES ('" + NowDate.ToString() + "',2)", conn, trans, null);
                                    RccCount = "1";
                                }
                                strRccCount = RccCount.ToString();
                                while (strRccCount.Length < 4)
                                    strRccCount = "0" + strRccCount;
                                PNno = "PN" + NowDate.ToString() + strRccCount.ToString();
                                #endregion
                                //根据项目号的第一位判定财务第一级审核人是谁
                                branch = ESP.Finance.BusinessLogic.BranchManager.GetModelByCode(generalmodel.project_code.Substring(0, 1), trans);
                                //查付款账期，如果存在取其默认的付款日期给PN
                                paymentlist = ESP.Purchase.BusinessLogic.PaymentPeriodManager.GetModelList(" gid=" + generalmodel.id.ToString(), trans);
                                if (paymentlist != null && paymentlist.Count > 0)
                                {
                                    PaymentBeginDate = paymentlist[0].beginDate;
                                    PaymentEndDate = paymentlist[0].endDate;
                                }
                                //3000以下生成新的PN model
                                returnModel = new ESP.Finance.Entity.ReturnInfo();
                                //给PN Model 赋值
                                returnModel.ReturnCode = PNno;
                                returnModel.ProjectID = generalmodel.Project_id;
                                returnModel.ProjectCode = generalmodel.project_code;
                                returnModel.DepartmentID = generalmodel.Departmentid;
                                returnModel.DepartmentName = generalmodel.Department;
                                returnModel.ReturnContent = string.Empty;
                                returnModel.PreFee = overModel.TotalPrice;
                                returnModel.ReturnStatus = (int)ESP.Finance.Utility.PaymentStatus.MajorAudit;
                                returnModel.PRID = overModel.GeneralID;
                                returnModel.PRNo = overModel.PRNO;
                                returnModel.RequestorID = Convert.ToInt32(overModel.CurrentUserID);
                                returnModel.RequestEmployeeName = overModel.CurrentUserEmpName;
                                returnModel.RequestUserCode = overModel.CurrentUserCode;
                                returnModel.RequestUserName = overModel.CurrentUserName;
                                returnModel.RequestDate = DateTime.Now;
                                returnModel.Attachment = overModel.FileName;
                                returnModel.ReturnType = generalmodel.PRType;
                                returnModel.PaymentUserID = branch.FirstFinanceID;
                                emp = new ESP.Compatible.Employee(branch.FirstFinanceID);
                                returnModel.PaymentEmployeeName = emp.Name;
                                returnModel.PaymentCode = emp.ID;
                                returnModel.PaymentUserName = emp.ITCode;
                                returnModel.PreBeginDate = PaymentBeginDate;
                                returnModel.PreEndDate = PaymentEndDate;
                                returnModel.MediaOrderIDs = overModel.MediaOrderIDS;
                                returnModel.SupplierBankAccount = overModel.BankAccount;
                                returnModel.SupplierBankName = overModel.BankName;
                                returnModel.SupplierName = overModel.BankAccountName;
                                returnModel.ReturnContent = "需要税单";
                                //插入PN Model 并取PN ID
                                ReturnID = ESP.Finance.BusinessLogic.ReturnManager.Add(returnModel, trans);
                                ret = ReturnID;
                                //插入PN 的审批列表
                                DbHelperSQL.ExecuteSql("insert into F_ReturnAuditHist(ReturnID,AuditorUserID,AuditorUserName,AuditorUserCode,AuditorEmployeeName,auditeStatus,auditType)  values(" + ReturnID.ToString() + "," + emp.SysID + ",'" + emp.ITCode + "','" + emp.ID + "','" + emp.Name + "',0,11)", conn, trans, null);
                                //插入授权表
                                DbHelperSQL.ExecuteSql("insert into t_datainfo(datatype,dataid) values(5," + ReturnID.ToString() + ");", conn, trans, null);
                                //取授权表ID
                                datainfoID = DbHelperSQL.GetSingle("select id from t_datainfo where datatype=5 and dataid=" + ReturnID.ToString(), conn, trans, null);
                                //申请人授权
                                DbHelperSQL.ExecuteSql("insert into T_dataPermission(datainfoid,userid,iseditor,isviewer) values(" + datainfoID.ToString() + "," + overModel.CurrentUserID + ",1,1)", conn, trans, null);
                                //审批人授权
                                DbHelperSQL.ExecuteSql("insert into T_dataPermission(datainfoid,userid,iseditor,isviewer) values(" + datainfoID.ToString() + "," + branch.FirstFinanceID.ToString() + ",1,1)", conn, trans, null);

                                term = " OldPrID=@OldPrID";
                                parmList = new List<SqlParameter>();
                                p1 = new SqlParameter("@OldPRID", SqlDbType.Int, 4);
                                p1.Value = overModel.GeneralID;
                                parmList.Add(p1);
                                //检查是否做过新旧PR PN关联对照
                                relationList = ESP.Purchase.BusinessLogic.MediaPREditHisManager.GetList(term, conn, trans, parmList);
                                //如果存在对照表，更新
                                if (relationList.Count > 0)
                                {
                                    relationList[0].NewPNId = ReturnID;
                                    relationList[0].OrderType = 1;
                                    ESP.Purchase.BusinessLogic.MediaPREditHisManager.Update(relationList[0], conn, trans);
                                }
                                else//否则插入
                                {
                                    ESP.Purchase.Entity.MediaPREditHisInfo relationModel = new MediaPREditHisInfo();
                                    relationModel.OldPRId = overModel.GeneralID;
                                    relationModel.NewPNId = ReturnID;
                                    relationModel.OrderType = 1;
                                    ESP.Purchase.BusinessLogic.MediaPREditHisManager.ADD(relationModel, conn, trans);
                                }
                                IDs = overModel.MediaOrderIDS.Split(',');//更新Media order表之前ID的数量
                                batchCount = UpdateStatusBatch(overModel.MediaOrderIDS, conn, trans); //更新影响的行数
                                //$$$$$ PR单个人3000以下PN申请

                            //    ESP.Purchase.BusinessLogic.GeneralInfoManager.InsertPrCostRecords(null, returnModel, trans, State.PR_CostRecordsType.PR单媒介需要税单PN申请, 0, 0);
                                if (IDs.Length != batchCount)//如果数量不一致，rollback
                                {
                                    trans.Rollback();
                                    return -1;
                                }
                                #endregion
                                break;
                            case 4://对私3000以上
                                #region 对私3000以上
                                NewModel = new GeneralInfo();
                                //新PR Model赋值
                                NewModel.Project_id = generalmodel.Project_id;
                                NewModel.project_code = generalmodel.project_code;
                                NewModel.requestor = Convert.ToInt32(overModel.CurrentUserID);
                                NewModel.requestorname = overModel.CurrentUserEmpName;
                                NewModel.app_date = DateTime.Now;
                                NewModel.project_descripttion = generalmodel.project_descripttion;
                                //NewModel.buggeted = getNewValue(overModel.TotalPrice * (decimal)1.1);
                                //NewModel.totalprice = getNewValue(overModel.TotalPrice * (decimal)1.1);
                                NewModel.MediaOldAmount = getNewValue(overModel.TotalPrice);
                                //NewModel.sow2 = overModel.FileName;
                                NewModel.lasttime = DateTime.Now;
                                NewModel.status = 0;
                                NewModel.PRType = (int)ESP.Purchase.Common.PRTYpe.PR_PriFA;
                                NewModel.Departmentid = generalmodel.Departmentid;
                                NewModel.Department = generalmodel.Department;
                                NewModel.requestor_info = generalmodel.requestor_info;
                                NewModel.requestor_group = generalmodel.requestor_group;
                                SetNewPRInfo(overModel,generalmodel, NewModel);
                                //带事务插入PR，返回新PR ID
                                NewGID = ESP.Purchase.BusinessLogic.GeneralInfoManager.Add(NewModel, conn, trans);
                                NewModel.id = NewGID;
                                addLinkInfo(NewModel, trans,ref newProductType);
                                ret = NewGID;
                                term = " OldPrID=@OldPrID";
                                parmList = new List<SqlParameter>();
                                p1 = new SqlParameter("@OldPRID", SqlDbType.Int, 4);
                                p1.Value = overModel.GeneralID;
                                parmList.Add(p1);
                                //检查新老PR单对照表是否存在数据
                                relationList = ESP.Purchase.BusinessLogic.MediaPREditHisManager.GetList(term, conn, trans, parmList);
                                //$$$$$ PR单个人3000以上PR申请

                               // ESP.Purchase.BusinessLogic.GeneralInfoManager.InsertPrCostRecords(NewModel, null, trans, State.PR_CostRecordsType.PR单个人3000以上PR申请, generalmodel.id, newProductType);
                                //如果存在对照表，更新
                                if (relationList != null && relationList.Count > 0)
                                {
                                    trans.Rollback();
                                    return -1;
                                }
                                else//否则插入
                                {
                                    ESP.Purchase.Entity.MediaPREditHisInfo relationModel = new MediaPREditHisInfo();
                                    relationModel.OldPRId = overModel.GeneralID;
                                    relationModel.NewPRId = NewGID;
                                    relationModel.OrderType = 2;
                                    relationModel.OldPaymentID = overModel.MediaOrderIDS;
                                    ESP.Purchase.BusinessLogic.MediaPREditHisManager.ADD(relationModel, conn, trans);
                                }
                                IDs = overModel.MediaOrderIDS.Split(',');//更新Media order表之前ID的数量

                                batchCount = UpdatePaymentStatusBatch(overModel.MediaOrderIDS, 0, "", conn, trans);//更新影响的行数
                                if (IDs.Length != batchCount)//如果数量不一致，rollback
                                {
                                    trans.Rollback();
                                    return -1;
                                }
                                #endregion
                                break;
                            case 5://对私3000以下
                                #region 对私3000以下

                                #region 生成新的PN单号
                                NowDate = DbHelperSQL.GetSingle("Select Substring(Convert(Varchar(6),GetDate(),112),3,4);", conn, trans);
                                ReturnCount = DbHelperSQL.GetSingle("select count(ReturnCount) from dbo.F_ReturnCounter where ReturnBaseTime like '%" + NowDate.ToString() + "%';", conn, trans);
                                if (Convert.ToInt32(ReturnCount) > 0)
                                {
                                    RccCount = DbHelperSQL.GetSingle("select ReturnCount from F_ReturnCounter where ReturnBaseTime like '%" + NowDate.ToString() + "%' ", conn, trans);
                                    DbHelperSQL.ExecuteSql("UPDATE F_ReturnCounter SET [ReturnCount] = " + (Convert.ToInt32(RccCount) + 1).ToString() + " WHERE ReturnBaseTime like '%" + NowDate.ToString() + "%' ", conn, trans);
                                }
                                else
                                {
                                    DbHelperSQL.ExecuteSql("INSERT INTO F_ReturnCounter (ReturnBaseTime,ReturnCount) VALUES ('" + NowDate.ToString() + "',2)", conn, trans, null);
                                    RccCount = "1";
                                }
                                strRccCount = RccCount.ToString();
                                while (strRccCount.Length < 4)
                                    strRccCount = "0" + strRccCount;
                                PNno = "PN" + NowDate.ToString() + strRccCount.ToString();
                                #endregion
                                //根据项目号的第一位判定财务第一级审核人是谁
                                branch = ESP.Finance.BusinessLogic.BranchManager.GetModelByCode(generalmodel.project_code.Substring(0, 1), trans);
                                //查付款账期，如果存在取其默认的付款日期给PN
                                paymentlist = ESP.Purchase.BusinessLogic.PaymentPeriodManager.GetModelList(" gid=" + generalmodel.id.ToString(), trans);
                                if (paymentlist != null && paymentlist.Count > 0)
                                {
                                    PaymentBeginDate = paymentlist[0].beginDate;
                                    PaymentEndDate = paymentlist[0].endDate;
                                }
                                //3000以下生成新的PN model
                                returnModel = new ESP.Finance.Entity.ReturnInfo();
                                //给PN Model 赋值
                                returnModel.ReturnCode = PNno;
                                returnModel.ProjectID = generalmodel.Project_id;
                                returnModel.ProjectCode = generalmodel.project_code;
                                returnModel.DepartmentID = generalmodel.Departmentid;
                                returnModel.DepartmentName = generalmodel.Department;
                                returnModel.ReturnContent = string.Empty;
                                returnModel.PreFee = overModel.TotalPrice;
                                returnModel.ReturnStatus = (int)ESP.Finance.Utility.PaymentStatus.Submit;
                                returnModel.PRID = overModel.GeneralID;
                                returnModel.PRNo = overModel.PRNO;
                                returnModel.RequestorID = Convert.ToInt32(overModel.CurrentUserID);
                                returnModel.RequestEmployeeName = overModel.CurrentUserEmpName;
                                returnModel.RequestUserCode = overModel.CurrentUserCode;
                                returnModel.RequestUserName = overModel.CurrentUserName;
                                returnModel.RequestDate = DateTime.Now;
                                // returnModel.Attachment = overModel.FileName;
                                returnModel.ReturnType = generalmodel.PRType;
                                returnModel.PaymentTypeID = 3;
                                returnModel.PaymentTypeName = "银行转账";
                                //财务级审批
                                emp = new ESP.Compatible.Employee(branch.FirstFinanceID);
                                //总经理级业务审批
                                //object bizaudit = DbHelperSQL.GetSingle("Select top 1 ISNULL(auditorId,0) From T_OperationAudit where general_id=" + generalmodel.id.ToString() + " and aduitType=2 ", conn, trans, null);
                                //if (Convert.ToInt32(bizaudit) > 0)
                                //{
                                //    emp2 = new ESP.Compatible.Employee(Convert.ToInt32(bizaudit));
                                //}
                                //else
                                //{
                                object requestor = DbHelperSQL.GetSingle("Select top 1 ISNULL(requestor,0) From T_Generalinfo where id=" + generalmodel.id.ToString(), conn, trans, null);
                                ESP.Compatible.Employee requestorEmp = new ESP.Compatible.Employee(Convert.ToInt32(requestor));
                                int deptid = requestorEmp.GetDepartmentIDs()[0];
                                ESP.Framework.Entity.OperationAuditManageInfo manager = ESP.Framework.BusinessLogic.OperationAuditManageManager.GetModelByDepId(deptid, trans);//.GetModelByDepId(deptid, conn, trans);
                                ESP.Finance.Entity.DepartmentViewInfo dept = ESP.Finance.BusinessLogic.DepartmentViewManager.GetModel(manager.DepId);

                                if (dept.level1Id == 19)
                                {
                                    emp2 = new ESP.Compatible.Employee(manager.DirectorId);
                                }
                                else
                                {
                                    emp2 = new ESP.Compatible.Employee(manager.ManagerId);
                                }
                                //}
                                if (emp2 == null)
                                {
                                    throw new Exception("未找到总经理级审批人，请联系管理员。");
                                }
                                returnModel.PaymentUserID = Convert.ToInt32(emp2.SysID);
                                returnModel.PaymentEmployeeName = emp2.Name;
                                returnModel.PaymentCode = emp2.ID;
                                returnModel.PaymentUserName = emp2.ITCode;
                                returnModel.PreBeginDate = PaymentBeginDate;
                                returnModel.PreEndDate = PaymentEndDate;
                                returnModel.MediaOrderIDs = overModel.MediaOrderIDS;
                                returnModel.SupplierBankAccount = overModel.BankAccount;
                                returnModel.SupplierBankName = overModel.BankName;
                                returnModel.SupplierName = overModel.BankAccountName;
                                //插入PN Model 并取PN ID
                                ReturnID = ESP.Finance.BusinessLogic.ReturnManager.Add(returnModel, trans);
                                ret = ReturnID;
                                //插入PN 的审批列表
                                if (emp2 != null)
                                {
                                    DbHelperSQL.ExecuteSql("insert into F_ReturnAuditHist(ReturnID,AuditorUserID,AuditorUserName,AuditorUserCode,AuditorEmployeeName,auditeStatus,auditType)  values(" + ReturnID.ToString() + "," + emp2.SysID + ",'" + emp2.ITCode + "','" + emp2.ID + "','" + emp2.Name + "',0,2)", conn, trans, null);
                                }
                                //财务出纳第一级审批
                                DbHelperSQL.ExecuteSql("insert into F_ReturnAuditHist(ReturnID,AuditorUserID,AuditorUserName,AuditorUserCode,AuditorEmployeeName,auditeStatus,auditType)  values(" + ReturnID.ToString() + "," + emp.SysID + ",'" + emp.ITCode + "','" + emp.ID + "','" + emp.Name + "',0,11)", conn, trans, null);
                                //插入授权表
                                DbHelperSQL.ExecuteSql("insert into t_datainfo(datatype,dataid) values(5," + ReturnID.ToString() + ");", conn, trans, null);
                                //取授权表ID
                                datainfoID = DbHelperSQL.GetSingle("select id from t_datainfo where datatype=5 and dataid=" + ReturnID.ToString(), conn, trans, null);
                                //申请人授权
                                DbHelperSQL.ExecuteSql("insert into T_dataPermission(datainfoid,userid,iseditor,isviewer) values(" + datainfoID.ToString() + "," + overModel.CurrentUserID + ",1,1)", conn, trans, null);
                                //审批人授权
                                DbHelperSQL.ExecuteSql("insert into T_dataPermission(datainfoid,userid,iseditor,isviewer) values(" + datainfoID.ToString() + "," + branch.FirstFinanceID.ToString() + ",1,1)", conn, trans, null);
                                if (emp2 != null)
                                {
                                    DbHelperSQL.ExecuteSql("insert into T_dataPermission(datainfoid,userid,iseditor,isviewer) values(" + datainfoID.ToString() + "," + emp2.SysID + ",1,1)", conn, trans, null);

                                }
                                term = " OldPrID=@OldPrID";
                                parmList = new List<SqlParameter>();
                                p1 = new SqlParameter("@OldPRID", SqlDbType.Int, 4);
                                p1.Value = overModel.GeneralID;
                                parmList.Add(p1);
                                //检查是否做过新旧PR PN关联对照
                                relationList = ESP.Purchase.BusinessLogic.MediaPREditHisManager.GetList(term, conn, trans, parmList);
                                //$$$$$ PR单个人3000以下PN申请

                              //  ESP.Purchase.BusinessLogic.GeneralInfoManager.InsertPrCostRecords(generalmodel, returnModel, trans, State.PR_CostRecordsType.PR单个人3000以下PN申请, 0, 0);
                                //如果存在对照表，更新
                                if (relationList.Count > 0)
                                {
                                    trans.Rollback();
                                    return -1;
                                }
                                else//否则插入
                                {
                                    ESP.Purchase.Entity.MediaPREditHisInfo relationModel = new MediaPREditHisInfo();
                                    relationModel.OldPRId = overModel.GeneralID;
                                    relationModel.NewPNId = ReturnID;
                                    relationModel.OrderType = 2;
                                    relationModel.OldPaymentID = overModel.MediaOrderIDS;
                                    ESP.Purchase.BusinessLogic.MediaPREditHisManager.ADD(relationModel, conn, trans);
                                }
                                IDs = overModel.MediaOrderIDS.Split(',');//更新Media order表之前ID的数量
                                batchCount = UpdatePaymentStatusBatch(overModel.MediaOrderIDS, ReturnID, PNno, conn, trans); //更新影响的行数
                                if (IDs.Length != batchCount)//如果数量不一致，rollback
                                {
                                    trans.Rollback();
                                    return -1;
                                }

                                #endregion
                                break;
                        }
                    }
                    trans.Commit();
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    ESP.Logging.Logger.Add(ex.Message, "MediaOrderOperation", ESP.Logging.LogLevel.Error, ex);
                    return 0;
                }
                finally
                {
                    conn.Close();
                }
            }
            return ret;

        }

        /// <summary>
        /// 获取分进角后的金额
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private static decimal getNewValue(decimal value)
        {
            decimal newValue = 0m;
            string[] v = value.ToString("0.####").Split('.');
            if (v.Length > 1)
                if (v[1].Length > 1)
                {
                    if (int.Parse(v[1][0].ToString()) + 1 == 10)
                    {
                        newValue = decimal.Parse(v[0]) + 1;
                    }
                    else
                    {
                        newValue = decimal.Parse(v[0] + "." + (int.Parse(v[1][0].ToString()) + 1));
                    }
                }
                else
                    newValue = value;
            else
                newValue = value;
            return newValue;
        }

        /// <summary>
        /// 设置新PR单信息
        /// </summary>
        /// <param name="oldModel"></param>
        /// <param name="newModel"></param>
        private void SetNewPRInfo(ESP.Purchase.Entity.MediaOrderOperationInfo over3000Model,GeneralInfo oldModel, GeneralInfo newModel)
        {
            //使用人，收货人为原申请单的申请人的FA
            //int[] depts = new ESP.Compatible.Employee(oldModel.requestor).GetDepartmentIDs();
            //OperationAuditManageInfo manageModel = OperationAuditManageManager.GetModelByDepId(depts[0]);
            //int receiverid =int.Parse(System.Configuration.ConfigurationManager.AppSettings["Receiver3000"]);
            //if (manageModel != null)
            //{
            ESP.Compatible.Employee FAUser = null;
                if (FAUser != null)
                {
                    if (newModel.enduser == 0)//使用人信息
                    {
                        newModel.enduser = int.Parse(FAUser.SysID);
                        newModel.endusername = FAUser.Name;
                        newModel.enduser_info = FAUser.Telephone;
                        newModel.enduser_group = FAUser.GetDepartmentNames().Count == 0 ? "" : FAUser.GetDepartmentNames()[0].ToString();
                    }
                    if (newModel.goods_receiver == 0)//收货人信息
                    {
                        newModel.goods_receiver = int.Parse(FAUser.SysID);
                        newModel.receivername = FAUser.Name;
                        newModel.receiver_info = FAUser.Telephone;
                        newModel.ship_address = FAUser.Address == "" ? ESP.Purchase.Common.State.ShunYa_Default_Address : FAUser.Address;
                        newModel.ReceiverGroup = FAUser.GetDepartmentNames().Count == 0 ? "" : FAUser.GetDepartmentNames()[0].ToString();
                    }
                }
            //}
                newModel.appendReceiver = newModel.requestor;
                newModel.appendReceiverName = newModel.requestorname;
                newModel.appendReceiverInfo = newModel.requestor_info;
                newModel.appendReceiverGroup = newModel.requestor_group;

            //采购物料类别默认为稿件费用报销单
            newModel.thirdParty_materielID = ESP.Configuration.ConfigurationManager.SafeAppSettings["WrittingFeeTypeID"].ToString();
            newModel.thirdParty_materielDesc = ESP.Purchase.BusinessLogic.TypeManager.GetModel(int.Parse(newModel.thirdParty_materielID)).typename;

            //设置供应商信息
            SupplierInfo supplier = ESP.Purchase.BusinessLogic.SupplierManager.GetModel(int.Parse(ESP.Configuration.ConfigurationManager.SafeAppSettings["SupplierId_ZC"].ToString()));
            newModel.buggeted = getNewValue(over3000Model.TotalPrice * (decimal)supplier.Payment_Tax.Value);
            newModel.totalprice = getNewValue(over3000Model.TotalPrice * (decimal)supplier.Payment_Tax.Value);

            newModel.supplier_name = supplier.supplier_name;
            newModel.supplier_address = supplier.contact_address;
            newModel.supplier_linkman = supplier.contact_name;
            newModel.supplier_phone = supplier.contact_tel;
            newModel.Supplier_cellphone = supplier.contact_mobile;
            newModel.supplier_fax = supplier.contact_fax;
            newModel.supplier_email = supplier.contact_email;
            newModel.source = supplier.supplier_source;
            newModel.fa_no = supplier.supplier_frameNO;
            newModel.account_bank = supplier.account_bank;
            newModel.account_name = supplier.account_name;
            newModel.account_number = supplier.account_number;
        }

        /// <summary>
        /// 添加采购物品、付款帐期
        /// </summary>
        /// <param name="newModel"></param>
        /// <param name="trans"></param>
        private void addLinkInfo(GeneralInfo newModel, SqlTransaction trans,ref int productType)
        {
            //添加采购物品，预计收货时间为当前日期
            SupplierInfo supplier = ESP.Purchase.BusinessLogic.SupplierManager.GetModel(int.Parse(ESP.Configuration.ConfigurationManager.SafeAppSettings["SupplierId_ZC"].ToString()));
            List<ESP.Purchase.Entity.ProductInfo> productList = ESP.Purchase.BusinessLogic.ProductManager.getModelList(" and a.supplierId=" + supplier.id + " and a.isMedia=1 and a.isShow=1", new List<SqlParameter>(), " a.productprice desc");
            decimal buggeted = newModel.buggeted;
            foreach (ESP.Purchase.Entity.ProductInfo productModel in productList)
            {
                int zCount = 0;
                if (productModel.ProductPrice == 0)
                    zCount = 1;
                else
                    zCount = int.Parse(decimal.Truncate((buggeted / productModel.ProductPrice)).ToString());//获得预计支付金额是采购物品单价的倍数
                if (zCount > 0)
                {
                    //添加采购物品
                    ESP.Purchase.Entity.OrderInfo orderModel = new OrderInfo();
                    orderModel.general_id = newModel.id;
                    orderModel.Item_No = productModel.productName;
                    orderModel.desctiprtion = "";
                    orderModel.intend_receipt_date = DateTime.Now.ToString("yyyy-MM-dd");
                    orderModel.price = productModel.ProductPrice == 0 ? buggeted : productModel.ProductPrice;
                    orderModel.uom = productModel.productUnit;
                    orderModel.quantity = zCount;
                    orderModel.total = orderModel.price * orderModel.quantity;
                    orderModel.producttype = productModel.productType;
                    orderModel.supplierId = supplier.id;
                    orderModel.supplierName = supplier.supplier_name;
                    orderModel.productAttribute = (int)State.productAttribute.ml;
                    new ESP.Purchase.DataAccess.OrderInfoDataHelper().Add(orderModel, trans.Connection, trans);
                    buggeted = buggeted - orderModel.total;
                }
                productType = productModel.productType;
            }

            //自动添加付款帐期,标准条款,当前日期
            PaymentPeriodInfo paymentPeriod = new PaymentPeriodInfo();
            paymentPeriod.gid = newModel.id;
            paymentPeriod.expectPaymentPrice = newModel.buggeted - buggeted;
            paymentPeriod.expectPaymentPercent = 100;
            paymentPeriod.periodDay = State.supplierpayment[0].ToString();
            paymentPeriod.beginDate = DateTime.Now;
            paymentPeriod.periodType = (int)State.PeriodType.period;
            paymentPeriod.Status = State.PaymentStatus_save;
            new ESP.Purchase.DataAccess.PaymentPeriodDataHelper().Add(paymentPeriod, trans.Connection, trans);
        }

        public string GetTotalAmountByUser(string MediaOrderIDs)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select sum(totalAmount),PaymentUserID");
            strSql.Append(" FROM T_MediaOrder where meidaorderid in(" + MediaOrderIDs + ") group by PaymentUserID");
            DataSet ds = DbHelperSQL.Query(strSql.ToString());

            string retstr = string.Empty;
            decimal totalUnPaid = 0;
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                if (ds.Tables[0].Rows[i][1] == null || ds.Tables[0].Rows[i][1] == System.DBNull.Value || ds.Tables[0].Rows[i][1].ToString().Trim() == string.Empty || ds.Tables[0].Rows[i][1].ToString().Trim() == "0")
                    totalUnPaid += Convert.ToDecimal(ds.Tables[0].Rows[i][0].ToString());
                else
                {
                    ESP.Compatible.Employee emp = new ESP.Compatible.Employee(Convert.ToInt32(ds.Tables[0].Rows[i][1]));
                    retstr += emp.Name + "已付金额：" + ds.Tables[0].Rows[i][0].ToString() + "<br/>";
                }
            }
            retstr += "未付金额：" + totalUnPaid.ToString() + "<br/>";

            return retstr;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="supplierId"></param>
        /// <param name="generalId"></param>
        /// <returns></returns>
        public bool ChangedSupplier(int supplierId, int generalId)
        {
            using (SqlConnection conn = new SqlConnection(DbHelperSQL.connectionString))
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();
                try
                {
                    GeneralInfo generalModel = ESP.Purchase.DataAccess.GeneralInfoDataProvider.GetModel(generalId, trans);
                    SupplierInfo supplierModel = ESP.Purchase.DataAccess.SupplierDataProvider.GetModel(supplierId);

                    //设置供应商信息
                    generalModel.supplier_name = supplierModel.supplier_name;
                    generalModel.supplier_address = supplierModel.contact_address;
                    generalModel.supplier_linkman = supplierModel.contact_name;
                    generalModel.supplier_phone = supplierModel.contact_tel;
                    generalModel.Supplier_cellphone = supplierModel.contact_mobile;
                    generalModel.supplier_fax = supplierModel.contact_fax;
                    generalModel.supplier_email = supplierModel.contact_email;
                    generalModel.source = supplierModel.supplier_source;
                    generalModel.fa_no = supplierModel.supplier_frameNO;
                    generalModel.account_bank = supplierModel.account_bank;
                    generalModel.account_name = supplierModel.account_name;
                    generalModel.account_number = supplierModel.account_number;
                    generalModel.buggeted = getNewValue(generalModel.MediaOldAmount * supplierModel.Payment_Tax.Value);
                    ESP.Purchase.DataAccess.GeneralInfoDataProvider.Update(generalModel, trans.Connection, trans);

                    //删除采购物品和付款帐期
                    ESP.Purchase.DataAccess.OrderInfoDataHelper.DeleteAllByGeneralId(generalModel.id, trans);
                    new ESP.Purchase.DataAccess.PaymentPeriodDataHelper().DeleteAllByGeneralId(generalModel.id, trans);

                    //添加采购物品
                    List<ESP.Purchase.Entity.ProductInfo> productList = ESP.Purchase.BusinessLogic.ProductManager.getModelListForMedia(" and a.supplierId=" + supplierId + " and a.isMedia=1 and a.isShow=1", new List<SqlParameter>(), " a.productprice desc");
                    decimal buggeted = generalModel.buggeted;
                    foreach (ESP.Purchase.Entity.ProductInfo productModel in productList)
                    {
                        if (productModel.ProductPrice != 0)
                        {
                            int zCount = int.Parse(decimal.Truncate((buggeted / productModel.ProductPrice)).ToString());//获得预计支付金额是采购物品单价的倍数
                            if (zCount > 0)
                            {
                                //添加采购物品
                                ESP.Purchase.Entity.OrderInfo orderModel = new OrderInfo();
                                orderModel.general_id = generalModel.id;
                                orderModel.Item_No = productModel.productName;
                                orderModel.desctiprtion = "";
                                orderModel.intend_receipt_date = DateTime.Now.ToString("yyyy-MM-dd");
                                orderModel.price = productModel.ProductPrice;
                                orderModel.uom = productModel.productUnit;
                                orderModel.quantity = zCount;
                                orderModel.total = orderModel.price * orderModel.quantity;
                                orderModel.producttype = productModel.productType;
                                orderModel.supplierId = supplierModel.id;
                                orderModel.supplierName = supplierModel.supplier_name;
                                orderModel.productAttribute = (int)State.productAttribute.ml;
                                new ESP.Purchase.DataAccess.OrderInfoDataHelper().Add(orderModel, trans.Connection, trans);
                                buggeted = buggeted - orderModel.total;
                            }
                        }
                        else
                        {
                            //添加采购物品
                            ESP.Purchase.Entity.OrderInfo orderModel = new OrderInfo();
                            orderModel.general_id = generalModel.id;
                            orderModel.Item_No = productModel.productName;
                            orderModel.desctiprtion = "";
                            orderModel.intend_receipt_date = DateTime.Now.ToString("yyyy-MM-dd");
                            orderModel.price = generalModel.buggeted;
                            orderModel.uom = productModel.productUnit;
                            orderModel.quantity = 1;
                            orderModel.total = orderModel.price * orderModel.quantity;
                            orderModel.producttype = productModel.productType;
                            orderModel.supplierId = supplierModel.id;
                            orderModel.supplierName = supplierModel.supplier_name;
                            orderModel.productAttribute = (int)State.productAttribute.ml;
                            new ESP.Purchase.DataAccess.OrderInfoDataHelper().Add(orderModel, trans.Connection, trans);
                            buggeted = buggeted - orderModel.total;
                        }
                    }

                    //自动添加付款帐期,标准条款,当前日期
                    PaymentPeriodInfo paymentPeriod = new PaymentPeriodInfo();
                    paymentPeriod.gid = generalModel.id;
                    paymentPeriod.expectPaymentPrice = generalModel.buggeted - buggeted;
                    paymentPeriod.expectPaymentPercent = 100;
                    paymentPeriod.periodDay = State.supplierpayment[0].ToString();
                    paymentPeriod.beginDate = DateTime.Now;
                    paymentPeriod.periodType = (int)State.PeriodType.period;
                    paymentPeriod.Status = State.PaymentStatus_save;
                    new ESP.Purchase.DataAccess.PaymentPeriodDataHelper().Add(paymentPeriod, trans.Connection, trans);

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
    }
}

