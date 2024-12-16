using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Collections;
using ESP.Media.Access;
using ESP.Media.Entity;
using ESP.Media.Access.Utilities;

namespace ESP.Media.BusinessLogic
{
    public class WritingfeeitemManager
    {
        public static int add(WritingfeeitemInfo item,ProjectbriefInfo brief, string filename, byte[] filedata, int userid,out string errmsg)
        {
            errmsg = "添加成功";
            using (SqlConnection conn = new SqlConnection(ESP.Media.Access.Utilities.clsConfigOperate.CustomerSqlConnection()))
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();

                item.Del = (int)Global.FiledStatus.Usable;
                try
                {
                    int id = ESP.Media.DataAccess.WritingfeeitemDataProvider.insertinfo(item, trans);
                    if (brief != null)
                    {
                        brief.Paymentid = id;
                        ProjectbriefManager.add(brief, trans, userid);
                    }
                    trans.Commit();
                    conn.Close();
                    return id;
                }
                catch (Exception ex)
                {
                    errmsg = "添加失败!";
                    Console.WriteLine(ex.Message);
                    trans.Rollback();
                    conn.Close();
                    return -2;
                }
            }
        }

        public static int add(WritingfeeitemInfo item, ProjectbriefInfo brief,int userid, ref int billid)
        {
            using (SqlConnection conn = new SqlConnection(ESP.Media.Access.Utilities.clsConfigOperate.CustomerSqlConnection()))
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();

                item.Del = (int)Global.FiledStatus.Usable;
                try
                {
                    if (item.Writingfeebillid == 0)
                    {
                        WritingfeebillInfo bill = new ESP.Media.Entity.WritingfeebillInfo();
                        bill.Createdate = DateTime.Now.ToString();
                        bill.Userid = userid;
                        ESP.Compatible.Employee emp = new ESP.Compatible.Employee(userid);
                        bill.Username = emp.Name;
                        bill.Createip = System.Net.Dns.GetHostAddresses(System.Net.Dns.GetHostName())[0].ToString();
                        bill.Projectid = item.Projectid;
                        bill.Userextensioncode = emp.Telephone;

                        billid = ESP.Media.BusinessLogic.WritingfeebillManager.add(bill, bill.Userid, trans);
                        item.Writingfeebillid = billid;
                    }

                    int id = ESP.Media.DataAccess.WritingfeeitemDataProvider.insertinfo(item, trans);
                    if (brief != null)
                    {
                        brief.Paymentid = id;
                        ProjectbriefManager.add(brief, trans, userid);
                    }
                    trans.Commit();
                    conn.Close();
                    return id;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    trans.Rollback();
                    conn.Close();
                    return -2;
                }
            }
        }


        [AjaxPro.AjaxMethod]
        public static string getReportInfo(int reporterId, int projectId)
        {
            string strret=string.Empty ;
            ReportersInfo reporter = ReportersManager.GetModel(reporterId);
            if (reporter != null)
            {
                MediaitemsInfo media = BusinessLogic.MediaitemsManager.GetModel(reporter.Media_id);
                if (media != null)
                    strret += media.Mediacname.Replace(Convert.ToChar(","), ' ') + " " + media.Channelname.Replace(Convert.ToChar(","), ' ') + " " + media.Topicname.Replace(Convert.ToChar(","), ' ') + ",";


                strret+= reporter.CityName.Replace(Convert.ToChar(","),' ')+",";
                strret += reporter.Cardnumber.Replace(Convert.ToChar(","), ' ') + ",";
                strret += reporter.Usualmobile.Replace(Convert.ToChar(","), ' ') + ",";
                strret += reporter.Bankname.Replace(Convert.ToChar(","), ' ') + ",";
                strret += reporter.Bankacountname.Replace(Convert.ToChar(","), ' ') + ",";
                strret += reporter.Bankcardcode.Replace(Convert.ToChar(","), ' ') + ",";
                //私密信息
                ProjectreporterrelationInfo relation = BusinessLogic.ProjectsManager.GetRelationReporterModel(projectId, reporter.Reporterid);
                if (relation != null && relation.Id > 0)
                {
                    strret+= relation.Privateinfoid.ToString();
                }

            }

            return strret;
        }

        public static bool modify(WritingfeeitemInfo item,ProjectbriefInfo brief, int userid,out string errmsg)
        {
            errmsg = "修改成功";
            using (SqlConnection conn = new SqlConnection(ESP.Media.Access.Utilities.clsConfigOperate.CustomerSqlConnection()))
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();
                //string term = " and propagatetype=@propagatetype and propagateid = @propagateid and linkmanid = @linkmanid and Writingfeeitemid != @Writingfeeitemid and del!=@del";
                //Hashtable ht = new Hashtable();
                //ht.Add("@propagatetype", item.Propagatetype);
                //ht.Add("@propagateid", item.Propagateid);
                //ht.Add("@linkmanid", item.Linkmanid);
                //ht.Add("@Writingfeeitemid", item.Writingfeeitemid);
                //ht.Add("@del", (int)Global.FiledStatus.Del);
                //DataTable dt = ESP.Media.DataAccess.WritingfeeitemDataProvider.QueryInfo(term, ht, trans);
                //if (dt != null && dt.Rows.Count > 0)
                //{
                //    errmsg = "已经添加过此条费用!";
                //    trans.Rollback();
                //    conn.Close();
                //    return false;
                //}
                try
                {
                    ESP.Media.DataAccess.WritingfeeitemDataProvider.updateInfo(trans, null, item, null, null);
                    if (brief != null)
                    {
                        brief.Paymentid = item.Writingfeeitemid;
                        ProjectbriefManager.modify(brief, trans, userid);
                    }
                    trans.Commit();
                    conn.Close();
                    return true;
                }
                catch (Exception ex)
                {
                    errmsg = "修改失败!";
                    Console.WriteLine(ex.Message);
                    trans.Rollback();
                    conn.Close();
                    return false;
                }
            }
        }


        public static bool del(int itemid, int userid)
        {
            WritingfeeitemInfo item = GetModel(itemid);
            using (SqlConnection conn = new SqlConnection(ESP.Media.Access.Utilities.clsConfigOperate.CustomerSqlConnection()))
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();
                item.Del = (int)Global.FiledStatus.Del;
                try
                {
                    ESP.Media.DataAccess.WritingfeeitemDataProvider.updateInfo(trans, null, item, null, null);
                    
                    if (item.Writingfeebillid>0 && (getItemCount(item.Writingfeebillid,trans) == 0 ) )
                    {
                        BusinessLogic.WritingfeebillManager.delete(item.Writingfeebillid, userid, trans);
                    }
                    trans.Commit();
                    conn.Close();
                    return true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    trans.Rollback();
                    conn.Close();
                    return false;
                }
            }
        }

        private static int getItemCount(int billid,SqlTransaction trans)
        {
            SqlParameter[] param = new SqlParameter[2];
            param[0].ParameterName = "@WritingFeeBillID";
            param[0].Value = billid;
            param[1].ParameterName = "@del";
            param[1].Value = 0;
            return Convert.ToInt32(ESP.Media.Access.Utilities.SqlHelper.ExecuteScalar(trans, CommandType.Text, "select count(*) from Media_WritingFeeItem where WritingFeeBillID=@WritingFeeBillID and del=@del",param));

        }

        public static WritingfeeitemInfo GetModel(int itemid)
        {
            WritingfeeitemInfo item = ESP.Media.DataAccess.WritingfeeitemDataProvider.Load(itemid);
            if (item == null)
            {
                item = new ESP.Media.Entity.WritingfeeitemInfo();
                item.Paytype = -1;
            }
            return item;
        }

        public static DataTable GetList(string term, Hashtable ht)
        {
            if (term == null)
                term = string.Empty;
            if (ht == null)
                ht = new Hashtable();
            term += " and del != @del order by a.Writingfeeitemid desc";
            if (!ht.ContainsKey("@del"))
                ht.Add("@del", (int)Global.FiledStatus.Del);
            return ESP.Media.DataAccess.WritingfeeitemDataProvider.QueryInfo(term, ht);
        }


 


        public static DataTable GetListByUser(int userid, string term, Hashtable ht)
        {

            string sql = @"SELECT     feeitem.WritingFeeitemid, feeitem.WritingFeeBillID, feeitem.projectid, feeitem.projectcode, feeitem.happendate, feeitem.userid, feeitem.username, 
                      feeitem.userdepartmentid, feeitem.userdepartmentname, feeitem.mediaid, feeitem.medianame, feeitem.writingsubject, brief.issuedate, 
                      feeitem.wordscount, feeitem.areawordscount, feeitem.unitprice, feeitem.amountprice, feeitem.subtotal, feeitem.linkmanid, feeitem.linkmanname, 
                      feeitem.recvmanname, feeitem.cityid, feeitem.cityname, feeitem.bankname, feeitem.bankaccount, feeitem.bankcardcode, feeitem.IDcardcode, 
                      feeitem.phoneno, feeitem.PropagateType, feeitem.PropagateID, feeitem.PropagateName, reporter.CurrentVersion, reporter.SN, reporter.Status, 
                      reporter.ReporterName, prj.ProjectCode AS projectcode, media.MediaitemID AS mediaitemid, 
                      media.MediaCName + ' ' + media.ChannelName + ' ' + media.TopicName AS medianame, brief.BriefSubject, brief.wordsaccount, brief.reporterid, 
                      brief.MediaItemTypeID, brief.Des, brief.Del, brief.LinkUrl, brief.FilePath, brief.paymentid, brief.ProgagateID,
                      isupload = case isnull(brief.paymentid,0) when 0 then '未上传' else '已上传' end
                        FROM         Media_WritingFeeItem AS feeitem INNER JOIN
                      media_Reporters AS reporter ON feeitem.linkmanid = reporter.ReporterID INNER JOIN
                      media_Projects AS prj ON feeitem.projectid = prj.ProjectID INNER JOIN
                      media_MediaItems AS media ON media.MediaitemID = reporter.Media_ID 
                        left JOIN Media_ProjectBrief AS brief ON brief.paymentid = feeitem.WritingFeeitemid
                        where  {0}
                        ";
            if (term == null)
            {
                term = string.Empty;
            }
            sql = string.Format(sql, term);
            //SqlParameter param = new SqlParameter("@BillID", SqlDbType.Int);

            //param.Value = billid;
            if (ht == null) ht = new Hashtable();
            ht.Add("@BillID", userid);
            return ESP.Media.Access.Utilities.clsSelect.QueryBySql(sql, ESP.Media.Access.Utilities.Common.DictToSqlParam(ht));
        }


        public static DataTable GetNotUploadListByBillID(int billid, string term, Hashtable ht)
        {
            if (term == null)
            {
                term = string.Empty;
            }
            term += " and (brief.projectid is null or brief.projectid = 0)";
            if (ht == null) ht = new Hashtable();
            return GetListByBillID(billid, term, ht);
        }

        public static DataTable GetListByBillID(int billid ,string term,Hashtable ht)
        {

            string sql = @"SELECT     feeitem.WritingFeeitemid, feeitem.WritingFeeBillID as WritingFeeBillID, feeitem.projectid as projectid, feeitem.projectcode, feeitem.happendate, feeitem.userid, feeitem.username, 
                      feeitem.userdepartmentid, feeitem.userdepartmentname, feeitem.mediaid, feeitem.medianame, feeitem.writingsubject, brief.issuedate, 
                      feeitem.wordscount, feeitem.areawordscount, feeitem.unitprice, feeitem.amountprice, feeitem.subtotal, feeitem.linkmanid as linkmanid, feeitem.linkmanname, 
                      feeitem.recvmanname, feeitem.cityid, feeitem.cityname, feeitem.bankname, feeitem.bankaccount, feeitem.bankcardcode, feeitem.IDcardcode, 
                      feeitem.phoneno, feeitem.PropagateType, feeitem.PropagateID, feeitem.PropagateName, reporter.CurrentVersion, reporter.SN, reporter.Status, 
                      reporter.ReporterName, prj.ProjectCode AS projectcode, media.MediaitemID AS mediaitemid, 
                      media.MediaCName + ' ' + media.ChannelName + ' ' + media.TopicName AS medianame, brief.BriefSubject, brief.wordsaccount, brief.reporterid, 
                      brief.MediaItemTypeID, brief.Des, brief.Del, brief.LinkUrl, brief.FilePath, brief.paymentid,
                        isupload = case isnull(brief.paymentid,0) when 0 then '未上传' else '已上传' end
                        FROM         Media_WritingFeeItem AS feeitem INNER JOIN
                      media_Reporters AS reporter ON feeitem.linkmanid = reporter.ReporterID INNER JOIN
                      media_Projects AS prj ON feeitem.projectid = prj.ProjectID INNER JOIN
                      media_MediaItems AS media ON media.MediaitemID = reporter.Media_ID 
                        left JOIN Media_ProjectBrief AS brief ON brief.paymentid = feeitem.WritingFeeitemid
                        where feeitem.WritingFeeBillID  = @BillID {0}
                        ";
            if (term == null)
            {
                term = string.Empty;
            }
            sql = string.Format(sql, term);
            //SqlParameter param = new SqlParameter("@BillID", SqlDbType.Int);
            
            //param.Value = billid;
            if (ht == null) ht = new Hashtable();
            ht.Add("@BillID", billid);
            return ESP.Media.Access.Utilities.clsSelect.QueryBySql(sql, ESP.Media.Access.Utilities.Common.DictToSqlParam(ht));
        }

        public static List<WritingfeeitemInfo> GetObjectListByBillID(int billid, string term, Hashtable ht)
        {
            DataTable dt = GetListByBillID(billid, term, ht);
            var query = from item in dt.AsEnumerable() select ESP.Media.DataAccess.WritingfeeitemDataProvider.setObject(item);
            List<WritingfeeitemInfo> items = new List<WritingfeeitemInfo>();
            foreach (WritingfeeitemInfo item in query)
            {
                items.Add(item);
            }
            return items;
        }

        public static DataTable GetExportItemList(int billid)
        {
            DataTable dt = GetListByBillID(billid,null,null);
            if (dt != null)
            {
                dt.Columns.Add("ProjectAvgPrice", typeof(double));
                dt.Columns.Add("MediaAvgprice", typeof(double));
                for (int i = 0; i < dt.Rows.Count;i++ )
                {
                    int projectid = dt.Rows[i]["projectid"] == DBNull.Value ? 0 : Convert.ToInt32(dt.Rows[i]["projectid"]);
                    int feeitemid = dt.Rows[i]["WritingFeeitemid"] == DBNull.Value ? 0 : Convert.ToInt32(dt.Rows[i]["WritingFeeitemid"]);
                    int reporterid = dt.Rows[i]["linkmanid"] == DBNull.Value ? 0 : Convert.ToInt32(dt.Rows[i]["linkmanid"]);
                    int mediaitemid = dt.Rows[i]["mediaitemid"] == DBNull.Value ? 0 : Convert.ToInt32(dt.Rows[i]["mediaitemid"]);
                    Global.ReporterComparePrice prices = GetReporterPriceCompare(billid,feeitemid,projectid,reporterid,mediaitemid);
                    dt.Rows[i]["ProjectAvgPrice"] = prices.ProjectAvgPrice;
                    dt.Rows[i]["MediaAvgprice"] = prices.MediaAvgprice;
                }
            }
            return dt;
        }

        public static Global.ReporterComparePrice GetReporterPriceCompare(int feebillid,int feeitemid, int projectid, int reporterid, int mediaitemid)
        {
            Global.ReporterComparePrice prices = new Global.ReporterComparePrice();
            prices.AmountPrice = WritingfeeitemManager.GetModel(feeitemid).Amountprice;

            string term = string.Empty;
            Hashtable ht = new Hashtable();
            DataTable dt = new DataTable();
            ht.Add("@projectid", projectid);
            ht.Add("@reporterid", reporterid);

            term = " and feeitem.projectid=@projectid and feeitem.linkmanid = @reporter ";
            ht = new Hashtable();
            ht.Add("@projectid", projectid);
            ht.Add("@reporter", reporterid);
            dt = GetListByBillID(feebillid, term, ht);

            if (dt == null || dt.Rows.Count == 0) prices.ProjectAvgPrice = 0;
            else
            {
                // prices.ProjectAvgPrice = dt.Rows[0][0] == DBNull.Value ? 0 : Convert.ToDouble(dt.Rows[0][0]);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    double price = dt.Rows[i]["Amountprice"] == DBNull.Value ? 0 : Convert.ToDouble(dt.Rows[i]["Amountprice"]);
                    prices.ProjectAvgPrice += price;
                }
                prices.ProjectAvgPrice = prices.ProjectAvgPrice / dt.Rows.Count;

            }

            term = " and feeitem.projectid=@projectid and reporter.media_id = @mediaitemid ";
            ht = new Hashtable();
            ht.Add("@projectid", projectid);
            ht.Add("@mediaitemid", mediaitemid);
            dt = GetListByBillID(feebillid, term, ht);

            if (dt == null || dt.Rows.Count == 0) prices.MediaAvgprice = 0;
            else
            {
                // prices.ProjectAvgPrice = dt.Rows[0][0] == DBNull.Value ? 0 : Convert.ToDouble(dt.Rows[0][0]);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    double price = dt.Rows[i]["Amountprice"] == DBNull.Value ? 0 : Convert.ToDouble(dt.Rows[i]["Amountprice"]);
                    prices.MediaAvgprice += price;
                }
                prices.MediaAvgprice = prices.MediaAvgprice / dt.Rows.Count;

            }
            return prices;
        }
    }
}
