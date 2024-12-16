
using System;
using System.Collections.Generic;
using System.Text;
using ESP.Compatible;
using System.Data;
using System.Data.SqlClient;
using System.Collections;

using ESP.Media.Access;
using ESP.Media.Entity;
using ESP.Media.Access.Utilities;
namespace ESP.Media.BusinessLogic
{

    /// <summary>
    ///目前不用此类
    /// </summary>
    public class PaymentManager
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static PaymentInfo GetModel(int id)
        {
            PaymentInfo indust = null;
            indust =ESP.Media.DataAccess.PaymentDataProvider.Load(id);
            if (indust == null) indust = new PaymentInfo();
            return indust;
        }

        public static int AddPropagateBrief(PaymentInfo payment, DailybriefInfo daily, EventbriefInfo evt, string filename, byte[] filedata,int userid, out string errmsg)
        {
            if (daily != null)
            {
                if (filedata != null && filename != string.Empty)
                {
                    daily.Filepath = CommonManager.SaveFile(ESP.Media.Access.Utilities.ConfigManager.DailyBriefPath, filename, filedata, true);
                }
            }
            else if (evt != null)
            {
                if (filedata != null && filename != string.Empty)
                {
                    evt.Filepath = CommonManager.SaveFile(ESP.Media.Access.Utilities.ConfigManager.EventBriefPath, filename, filedata, true);
                }
            }
            using (SqlConnection conn = new SqlConnection(ESP.Media.Access.Utilities.clsConfigOperate.CustomerSqlConnection()))
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();
                try
                {
                    payment.Financecode = GetFinanceCode(trans);
                    int id = add(payment, trans, userid);
                    payment.Id = id;

                    if (daily != null)
                    {
                        if (payment != null)
                        {
                            daily.Paymentid = payment.Id;
                        }
                        //Media_dailybrief.Add(daily,trans);
                    }
                    else if (evt != null)
                    {
                        if (payment != null)
                        {
                            evt.Paymentid = payment.Id;
                        }
                         //Media_eventbrief.Add(evt, trans);
                    }

                    trans.Commit();
                    errmsg = "支付成功";
                    return id;
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    errmsg = "支付失败!";
                    return -1;
                }
                finally
                {
                    conn.Close();
                }
            }
           
        }

        public static int add(PaymentInfo c, int userid)
        {
            using (SqlConnection conn = new SqlConnection(ESP.Media.Access.Utilities.clsConfigOperate.CustomerSqlConnection()))
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();
                try
                {
                    int id = add(c, trans, userid);
                    trans.Commit();
                    return id;
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    throw new Exception(ex.Message);
                }
                finally
                {
                    conn.Close();
                }
            }
        }
        public static int add(PaymentInfo c, SqlTransaction trans, int userid)
        {
            int id = 0;
            id =  ESP.Media.DataAccess.PaymentDataProvider.insertinfo(c, trans);
            return id;
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="p">p</param>
        /// <param name="emp">当前登录人</param>
        /// <returns></returns>
        public static bool modify(PaymentInfo c, int userid)
        {
            using (SqlConnection conn = new SqlConnection(ESP.Media.Access.Utilities.clsConfigOperate.CustomerSqlConnection()))
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();
                try
                {
                    bool result = modify(c, trans, userid);
                    trans.Commit();
                    return result;
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    throw new Exception(ex.Message);
                }
                finally
                {
                    conn.Close();
                }
            }
        }
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="a">a</param>
        /// <param name="trans">trans</param>
        /// <param name="emp">当前登录人</param>
        /// <returns></returns>
        public static bool modify(PaymentInfo c, SqlTransaction trans, int userid)
        {
            bool result = false;
            result = ESP.Media.DataAccess.PaymentDataProvider.updateInfo(trans, null, c, null, null);
            return result;
        }


        /// <summary>
        /// 获取列表
        /// </summary>
        /// <returns></returns>
        public static DataTable GetAllList()
        {
            return  ESP.Media.DataAccess.PaymentDataProvider.QueryInfo(null, new Hashtable());
        }

        /// <summary>
        /// FP08070001
        /// </summary>
        /// <returns></returns>
        private static string GetFinanceCode(SqlTransaction trans)
        {
            string prefix = "F";
            string date = DateTime.Now.ToString("yy-MM").Replace("-", "");
            string strSql = "select max(financecode) as maxId from media_Payment as a where a.financecode like '" + prefix + date + "%'";
            DataTable dt = ESP.Media.Access.Utilities.clsSelect.QueryBySql(strSql,trans);
            int num = dt.Rows[0]["maxId"] == DBNull.Value ? 0 : int.Parse(dt.Rows[0]["maxId"].ToString().Substring(5).ToString());
            string id = prefix + date;
            num++;
            return id + num.ToString("0000");
        }


        public static DataTable GetPaymentByBillID(int paymentbillid, string term, Hashtable ht)
        {
            string sql = @"SELECT     payment.id as paymentid, payment.reporterid, payment.Propagatetype, payment.ProgagateID, payment.PaymentDate, payment.PaymentUserid, payment.PaymentRemark, 
                      payment.paytype, payment.payamount, payment.FinanceCode, payment.UploadStartDate, payment.UploadEndDate,
                      reporter.CurrentVersion, reporter.SN, reporter.Status, reporter.CreatedByUserID, reporter.CreatedIP, reporter.CreatedDate, reporter.LastModifiedByUserID, 
                      reporter.ReporterName, prj.ProjectID,prj.projectcode as projectcode ,media.mediaitemid as mediaitemid,       
                    medianame = media.mediacname + ' ' + media.ChannelName + ' ' + media.TopicName 
                    FROM         Media_Payment AS payment INNER JOIN
                      media_Reporters AS reporter ON payment.reporterid = reporter.ReporterID INNER JOIN
                      media_projects AS prj ON payment.projectid = prj.projectid
                       inner join media_mediaitems AS media ON media.mediaitemid = reporter.media_id
                            where reporter.del!=@del and prj.del != @del
                            and payment.paymentbillid = @paymentbillid
                            {0} order by payment.id desc
                            ";
            if (term == null) term = string.Empty;
            if (ht == null) ht = new Hashtable();
            ht.Add("@Propagatetype", (int)Global.Propagatetype.Event);

            ht.Add("@del", (int)Global.FiledStatus.Del);
            ht.Add("@paymentbillid", paymentbillid);

            sql = string.Format(sql, term);
            SqlParameter[] param = ESP.Media.Access.Utilities.Common.DictToSqlParam(ht);
            return ESP.Media.Access.Utilities.clsSelect.QueryBySql(sql, param);
        }

        public static DataTable GetPaymentByProjectID(int projectid, string term, Hashtable ht)
        {
            string sql = @"SELECT     payment.id as paymentid, payment.reporterid, payment.Propagatetype, payment.ProgagateID, payment.PaymentDate, payment.PaymentUserid, payment.PaymentRemark, 
                      payment.paytype, payment.payamount, payment.FinanceCode, payment.UploadStartDate, payment.UploadEndDate,
                      reporter.CurrentVersion, reporter.SN, reporter.Status, reporter.CreatedByUserID, reporter.CreatedIP, reporter.CreatedDate, reporter.LastModifiedByUserID, 
                      reporter.ReporterName, prj.ProjectID,prj.projectcode as projectcode ,media.mediaitemid as mediaitemid,       
                    medianame = media.mediacname + ' ' + media.ChannelName + ' ' + media.TopicName 
                    FROM         Media_Payment AS payment INNER JOIN
                      media_Reporters AS reporter ON payment.reporterid = reporter.ReporterID INNER JOIN
                      media_projects AS prj ON payment.projectid = prj.projectid
                       inner join media_mediaitems AS media ON media.mediaitemid = reporter.media_id
                            where reporter.del!=@del and prj.del != @del
                            and prj.projectid = @projectid
                            {0} order by payment.id desc
                            ";
            if (term == null) term = string.Empty;
            if (ht == null) ht = new Hashtable();
            ht.Add("@Propagatetype", (int)Global.Propagatetype.Event);

            ht.Add("@del", (int)Global.FiledStatus.Del);
            ht.Add("@projectid", projectid);

            sql = string.Format(sql, term);
            SqlParameter[] param = ESP.Media.Access.Utilities.Common.DictToSqlParam(ht);
            return ESP.Media.Access.Utilities.clsSelect.QueryBySql(sql, param);
        }


        public static DataTable GetEventPaymentList(int pjid,string term, Hashtable ht)
        {
            string sql = @"SELECT     payment.id as paymentid, payment.reporterid, payment.Propagatetype, payment.ProgagateID, payment.PaymentDate, payment.PaymentUserid, payment.PaymentRemark, 
                      payment.paytype, payment.payamount, payment.FinanceCode, payment.UploadStartDate, payment.UploadEndDate,
                      reporter.CurrentVersion, reporter.SN, reporter.Status, reporter.CreatedByUserID, reporter.CreatedIP, reporter.CreatedDate, reporter.LastModifiedByUserID, 
                      reporter.ReporterName, evt.ProjectID, 
                     evt.EventName, evt.EventContent, evt.EventStartTime, 
                      evt.Eventstatus,                     
                    medianame = media.mediacname + ' ' + media.ChannelName + ' ' + media.TopicName 
                    FROM         Media_Payment AS payment INNER JOIN
                      media_Reporters AS reporter ON payment.reporterid = reporter.ReporterID INNER JOIN
                      media_Events AS evt ON payment.ProgagateID = evt.EventID
                       inner join media_mediaitems AS media ON media.mediaitemid = reporter.media_id
                            where payment.Propagatetype = @Propagatetype and reporter.del!=@del and evt.del != @del
                            and evt.projectid = @projectid
                            {0} order by payment.id desc
                            ";
            if (term == null) term = string.Empty;
            if (ht == null) ht = new Hashtable();
            ht.Add("@Propagatetype",(int)Global.Propagatetype.Event);

            ht.Add("@del", (int)Global.FiledStatus.Del);
            ht.Add("@projectid", pjid);

            sql = string.Format(sql, term);
            SqlParameter[] param = ESP.Media.Access.Utilities.Common.DictToSqlParam(ht);
            return ESP.Media.Access.Utilities.clsSelect.QueryBySql(sql, param);
        }

        public static DataTable GetDailyPaymentList(int pjid,string term, Hashtable ht)
        {
            string sql = @"SELECT     payment.id as paymentid, payment.reporterid, payment.Propagatetype, payment.ProgagateID, payment.PaymentDate, payment.PaymentUserid, payment.PaymentRemark, 
                      payment.paytype, payment.payamount, payment.FinanceCode, payment.UploadStartDate, payment.UploadEndDate, 
                      reporter.ReporterName, dal.ProjectID, 
                      dal.DailyName, dal.DailyContent, dal.DailyStartTime, 
                      dal.Dailystatus, dal.DailyCycleType, dal.DailyCycleDays,medianame = media.mediacname + ' '+media.ChannelName+' '+media.TopicName
FROM         Media_Payment AS payment INNER JOIN
                      media_Reporters AS reporter ON payment.reporterid = reporter.ReporterID INNER JOIN
                      media_dailys AS dal ON payment.ProgagateID = dal.dailyid
                       inner join media_mediaitems AS media ON media.mediaitemid = reporter.media_id
                            where payment.Propagatetype = @Propagatetype and reporter.del!=@del and dal.del != @del
                            and dal.projectid = @projectid
                            {0} order by payment.id desc
                            ";
            if (term == null) term = string.Empty;
            if (ht == null) ht = new Hashtable();
            ht.Add("@Propagatetype", (int)Global.Propagatetype.Daily);

            ht.Add("@del", (int)Global.FiledStatus.Del);

            ht.Add("@projectid",pjid);

            sql = string.Format(sql, term);
            SqlParameter[] param = ESP.Media.Access.Utilities.Common.DictToSqlParam(ht);
            return ESP.Media.Access.Utilities.clsSelect.QueryBySql(sql, param);
        }
    }
}
