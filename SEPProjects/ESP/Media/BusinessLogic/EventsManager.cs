using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Collections;
using System.IO;
using System.Data.SqlClient;
using Excel = Microsoft.Office.Interop.Excel;
using ESP.Media.Access;
using ESP.Media.Entity;
using ESP.Media.Access.Utilities;
namespace ESP.Media.BusinessLogic
{
    public class EventsManager
    {

        /// <summary>
        /// ��ȡ�¼����߹����� ����
        /// </summary>
        /// <param name="id">�¼����߹���ID</param>
        /// <returns></returns>
        public static EventreporterrelationInfo GetRelationReporterModel(int id)
        {
            EventreporterrelationInfo r = ESP.Media.DataAccess.EventreporterrelationDataProvider.Load(id);
            if (r == null) r = new EventreporterrelationInfo();
            return r;
        }

        /// <summary>
        /// ��ȡ�¼����߹����� ����
        /// </summary>
        /// <param name="dailyid">�¼�������ID</param>
        /// <param name="reporterid">����ID</param>
        /// <returns></returns>
        public static EventreporterrelationInfo GetRelationReporterModel(int eventid, int reporterid)
        {
            string term = " and eventid = @eventid and reporterid = @reporterid and del!=@del";
            Hashtable ht = new Hashtable();
            ht.Add("@eventid", eventid);
            ht.Add("@reporterid", reporterid);
            ht.Add("@del", (int)Global.FiledStatus.Del);
            DataTable dt = ESP.Media.DataAccess.EventreporterrelationDataProvider.QueryInfo(term, ht);
            if (dt == null || dt.Rows.Count == 0)
                return null;
            else
            {
                int rid = Convert.ToInt32(dt.Rows[0]["id"]);
                return GetRelationReporterModel(rid);
            }
        }

        /// <summary>
        /// ��ȡ�����¼���Ϣ�б�
        /// </summary>
        /// <returns></returns>
        public static DataTable GetAllList()
        {
            return GetList(null, new Hashtable());
        }

        /// <summary>
        /// ��ȡ�¼���Ϣ�б�
        /// </summary>
        /// <param name="terms">����</param>
        /// <param name="ht">SqlParameter �ļ���</param>
        /// <returns></returns>
        public static DataTable GetList(string terms, Hashtable ht)
        {
            if (ht == null)
                ht = new Hashtable();
            if (terms == null)
            {
                terms = string.Empty;
            }
            terms += " and del!=@del order by a.eventid desc";
            if (!ht.ContainsKey("@del"))
            {
                ht.Add("@del", (int)Global.FiledStatus.Del);
            }
            return ESP.Media.DataAccess.EventsDataProvider.QueryInfo(terms, ht);
        }


        public static List<EventsInfo> GetObjectList(string term, Hashtable ht)
        {
            DataTable dt = GetList(term,ht);
            var query = from events in dt.AsEnumerable() select ESP.Media.DataAccess.EventsDataProvider.setObject(events);
            List<EventsInfo> items = new List<EventsInfo>();
            foreach (EventsInfo item in query)
            {
                items.Add(item);
            }
            return items;
        }

        public static List<EventsInfo> GetObjectList(string terms, List<SqlParameter> param)
        {
            if (terms == null)
            {
                terms = string.Empty;
            }
            terms += " and del!=@del order by a.eventid desc";

            SqlParameter[] sparam = new SqlParameter[param.Count + 1];
            Array.Copy(param.ToArray(), sparam, param.Count);
            SqlParameter sp = new SqlParameter("@del", (int)Global.FiledStatus.Del);
            sparam[param.Count] = sp;

            DataTable dt = ESP.Media.DataAccess.EventsDataProvider.QueryInfo(terms, sparam);
            var query = from events in dt.AsEnumerable() select ESP.Media.DataAccess.EventsDataProvider.setObject(events);
            List<EventsInfo> items = new List<EventsInfo>();
            foreach (EventsInfo item in query)
            {
                items.Add(item);
            }
            return items;
        }
        /// <summary>
        /// �����¼�������ID��ȡһ���¼�����
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static EventsInfo GetModel(int id)
        {
            if (id <= 0) return new EventsInfo();
            return ESP.Media.DataAccess.EventsDataProvider.Load(id);
        }


        /// <summary>
        /// ���һ���¼�������Ϣ
        /// </summary>
        /// <param name="obj">Ҫ��ӵ��¼�������Ϣ����</param>
        /// <param name="errmsg"></param>
        /// <returns></returns>
        public static int Add(EventsInfo obj, out string errmsg)
        {
            using (SqlConnection conn = new SqlConnection(clsConfigOperate.CustomerSqlConnection()))
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();
                string term = " EventName= @EventName AND del!=@del ";
                SqlParameter[] param = new SqlParameter[2];
                param[0] = new SqlParameter("@EventName", SqlDbType.NVarChar);
                param[0].Value = obj.Eventname;
                param[1] = new SqlParameter("@del", SqlDbType.Int);
                param[1].Value = (int)Global.FiledStatus.Del;
                DataTable dt = ESP.Media.DataAccess.EventsDataProvider.QueryInfo(trans, term, param);
                if (dt.Rows.Count > 0)
                {
                    errmsg = "�¼������Ѵ���!";
                    trans.Rollback();
                    conn.Close();
                    return -1;
                }
                try
                {
                    errmsg = string.Empty;
                    int ret = ESP.Media.DataAccess.EventsDataProvider.insertinfo(obj, trans);
                    DataTable dtmedias = ESP.Media.BusinessLogic.ProjectsManager.GetRelationMedias(obj.Projectid, null, null);
                    if (dtmedias != null && dtmedias.Rows.Count > 0)
                    {
                        int [] medias =  new int[dtmedias.Rows.Count];
                        for (int i = 0; i < dtmedias.Rows.Count;i++ )
                        {
                            int mediaid = dtmedias.Rows[i]["mediaitemid"] == DBNull.Value ? 0 : Convert.ToInt32(dtmedias.Rows[i]["mediaitemid"]);
                            medias[i] = mediaid;
                        }
                        AddMediatrans(ret, medias,trans, out errmsg);
                    }
                    DataTable dtreporters = ESP.Media.BusinessLogic.ProjectsManager.GetRelationReporters(obj.Projectid, null, null);
                    if (dtreporters != null && dtreporters.Rows.Count > 0)
                    {
                        int[] reporters = new int[dtreporters.Rows.Count];
                        for (int i = 0; i < dtreporters.Rows.Count; i++)
                        {
                            int reporterid = dtreporters.Rows[i]["reporterId"] == DBNull.Value ? 0 : Convert.ToInt32(dtreporters.Rows[i]["reporterId"]);
                            reporters[i] = reporterid;
                        }
                        AddReportertrans(ret, reporters,trans, out errmsg);
                    }
                    

                    trans.Commit();
                    conn.Close();
                    return ret;
                }
                catch (Exception exception)
                {
                    trans.Rollback();
                    conn.Close();
                    errmsg = exception.Message;
                    return -2;
                }
            }
        }


        /// <summary>
        /// ����һ���¼���Ϣ
        /// </summary>
        /// <param name="obj">Ҫ���µ��¼�������Ϣ</param>
        /// <param name="errmsg"></param>
        /// <returns></returns>
        public static int Update(EventsInfo obj, out string errmsg)
        {
            using (SqlConnection conn = new SqlConnection(clsConfigOperate.CustomerSqlConnection()))
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();
                string term = " EventName= @EventName AND EventID=@EventID AND del!=@del ";
                SqlParameter[] param = new SqlParameter[3];
                param[0] = new SqlParameter("@EventName", SqlDbType.NVarChar);
                param[0].Value = obj.Eventname;
                param[1] = new SqlParameter("@EventID", SqlDbType.Int);
                param[1].Value = obj.Eventid;
                param[2] = new SqlParameter("@del", SqlDbType.Int);
                param[2].Value = (int)Global.FiledStatus.Del;
                DataTable dt = ESP.Media.DataAccess.EventsDataProvider.QueryInfo(trans, term, param);
                if (dt.Rows.Count > 0)
                {
                    errmsg = "�¼������Ѵ���!";
                    trans.Rollback();
                    conn.Close();
                    return -1;
                }
                try
                {
                    errmsg = string.Empty;
                    if (ESP.Media.DataAccess.EventsDataProvider.updateInfo(trans, null, obj, string.Empty, null))
                    {
                        trans.Commit();
                        conn.Close();
                        return 1;
                    }
                    else
                    {
                        errmsg = "�޸�ʧ��!";
                        conn.Close();
                        return -3;
                    }
                }
                catch (Exception exception)
                {
                    trans.Rollback();
                    conn.Close();
                    errmsg = exception.Message;
                    return -2;
                }
            }
        }

        /// <summary>
        /// ɾ��һ���¼�������Ϣ
        /// </summary>
        /// <param name="obj">Ҫɾ�����¼�����</param>
        /// <param name="errmsg"></param>
        /// <returns></returns>
        public static int Delete(EventsInfo obj, out string errmsg)
        {
            errmsg = "ɾ���ɹ�!";
            try
            {
                obj.Del = (int)Global.FiledStatus.Del;
                if (ESP.Media.DataAccess.EventsDataProvider.updateInfo(null, obj, string.Empty, null))
                {
                    return 1;
                }
                else
                {
                    errmsg = "ɾ��ʧ��!";
                    return -3;
                }
            }
            catch (Exception exception)
            {
                errmsg = exception.Message;
                return -2;
            }
        }

        /// <summary>
        /// ���¼�������ɾ��һ��ý��Ĺ���
        /// </summary>
        /// <param name="mediaid">Ҫ�Ƴ���ý��</param>
        /// <param name="dailyid">�¼�����ID</param>
        /// <param name="userid">������</param>
        /// <returns></returns>
        public static int DelMedia(int mediaid, int eventid, int userid)
        {
            string sql = @"delete media_eventmediarelation where mediaitemid=@mediaitemid and eventid  = @eventid ;";
            string sql1 = @"delete media_eventreporterrelation where eventid = @eventid  and reporterid in (select reporterid from media_reporters as a where a.media_id = @mediaitemid);";


            using (SqlConnection conn = new SqlConnection(clsConfigOperate.CustomerSqlConnection()))
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();
                try
                {
                    SqlParameter[] param = new SqlParameter[2];
                    param[0] = new SqlParameter("@eventid", SqlDbType.Int);
                    param[0].Value = eventid;
                    param[1] = new SqlParameter("@mediaitemid", SqlDbType.Int);//(int)Global.PostType.Issue);
                    param[1].Value = mediaid;
                    int delid = 0;
                    delid += SqlHelper.ExecuteNonQuery(trans, CommandType.Text, sql1, param);
                    delid += SqlHelper.ExecuteNonQuery(trans, CommandType.Text, sql, param);
                    trans.Commit();
                    conn.Close();
                    return delid;
                }
                catch (Exception exp)
                {
                    trans.Rollback();
                    conn.Close();
                    string errmsg = exp.Message;
                    return -2;
                }
            }

        }

        /// <summary>
        /// �¼���������ӹ���ý��(�������)
        /// </summary>
        /// <param name="dailyId">�¼�����ID</param>
        /// <param name="mediaId">ý�弯��</param>
        /// <param name="trans">����</param>
        /// <param name="errmsg">������Ϣ</param>
        public static void AddMediatrans(int eventId, int[] mediaId, SqlTransaction trans, out string errmsg)
        {
            errmsg = "��ӳɹ�!";
            foreach (int mid in mediaId)
            {
                EventmediarelationInfo emrelation = new EventmediarelationInfo();
                emrelation.Eventid = eventId;
                emrelation.Mediaitemid = mid;
                emrelation.Relationdate = DateTime.Now.ToString();
                emrelation.Relationuserid = 1;
                ESP.Media.DataAccess.EventmediarelationDataProvider.insertinfo(emrelation, trans);
            }
        }

        /// <summary>
        /// �¼���������ӹ���ý��
        /// </summary>
        /// <param name="dailyId">�¼�����ID</param>
        /// <param name="mediaId">ý�弯��</param>
        /// <param name="errmsg">������Ϣ</param>
        public static int AddMedia(int eventId, int[] mediaId, out string errmsg)
        {
            errmsg = "��ӳɹ�!";
            using (SqlConnection conn = new SqlConnection(clsConfigOperate.CustomerSqlConnection()))
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();
                try
                {
                    //string sql = "delete from Media_EventMediaRelation where eventid = @evid";
                    //Hashtable ht = new Hashtable();
                    //ht.Add("@evid", eventId);
                    //SqlParameter[] param = ESP.Media.Access.Utilities.Common.DictToSqlParam(ht);
                    //SqlHelper.ExecuteNonQuery(trans, CommandType.Text, sql, param);

                    AddMediatrans(eventId, mediaId, trans, out errmsg);
                    trans.Commit();
                    conn.Close();
                    return mediaId.Length;
                }
                catch (Exception exp)
                {
                    trans.Rollback();
                    conn.Close();
                    errmsg = exp.Message;
                    return -2;
                }
            }
        }

        /// <summary>
        /// ���¼���������Ӽ��߹���(�������)
        /// </summary>
        /// <param name="dailyId">�¼�����ID</param>
        /// <param name="reporterId">����ID</param>
        /// <param name="trans">����</param>
        /// <param name="errmsg">������Ϣ</param>
        public static void AddReportertrans(int eventId, int[] reporterId,SqlTransaction trans, out string errmsg)
        {
            errmsg = "�ɹ�!";
            foreach (int rid in reporterId)
            {
                EventreporterrelationInfo emrelation = new EventreporterrelationInfo();
                emrelation.Eventid = eventId;
                emrelation.Reporterid = rid;
                emrelation.Relationdate = DateTime.Now.ToString();
                emrelation.Relationuserid = 1;
                ESP.Media.DataAccess.EventreporterrelationDataProvider.insertinfo(emrelation, trans);
            }
        }

        /// <summary>
        /// ���¼���������Ӽ��߹���
        /// </summary>
        /// <param name="dailyId">�¼�����ID</param>
        /// <param name="reporterId">����ID</param>
        /// <param name="errmsg">������Ϣ</param>
        public static int AddReporter(int eventId, int[] reporterId, out string errmsg)
        {
            errmsg = "�ɹ�!";
            using (SqlConnection conn = new SqlConnection(clsConfigOperate.CustomerSqlConnection()))
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();
                try
                {
                    //string sql = "delete from Media_EventReporterRelation where eventid = @evid";
                    //Hashtable ht = new Hashtable();
                    //ht.Add("@evid", eventId);
                    //SqlParameter[] param = ESP.Media.Access.Utilities.Common.DictToSqlParam(ht);
                    //SqlHelper.ExecuteNonQuery(trans, CommandType.Text, sql, param);

                    AddReportertrans(eventId, reporterId, trans, out errmsg);
                    //ht = new Hashtable();
                    //ht.Add("@evid", eventId);
                    //param = ESP.Media.Access.Utilities.Common.DictToSqlParam(ht);
                    //sql = "update media_events set eventstatus = 1 where eventid = @evid";
                    //SqlHelper.ExecuteNonQuery(trans, CommandType.Text, sql, param);
                    SetStatus(eventId,1,trans);
                    trans.Commit();
                    conn.Close();
                    return reporterId.Length;
                }
                catch (Exception exp)
                {
                    trans.Rollback();
                    conn.Close();
                    errmsg = exp.Message;
                    return -2;
                }
            }
        }



        /// <summary>
        /// ȡ���¼��������ѹ���ý����б�
        /// </summary>
        /// <param name="dailyId">�¼�ID</param>
        /// <returns></returns>
        public static DataTable GetRelationMedias(int eventId)
        {
            string sql = @"select a.mediaitemid as mediaitemid,
                            a.mediacname as mediacname,a.mediaename as mediaename,
                            a.cshortname as cshortname,a.eshortname as eshortname,
                            a.mediaitemtype as mediaitemtype,a.currentversion as currentversion,
                            a.status as status,a.createdbyuserid as createdbyuserid,
                            a.createddate as createddate,a.lastmodifiedbyuserid as lastmodifiedbyuserid,
                            a.lastmodifieddate as lastmodifieddate,a.mediumsort as mediumsort,
                            a.RegionAttribute as RegionAttribute,a.industryid as industryid,
                            a.Countryid as Countryid,TelephoneExchange,
                            medianame = a.mediacname + ' '+a.ChannelName+' '+a.TopicName,
                            a.issueregion as issueregion,indust.industryname as industryname,
                            headquarter = country.countryname + ' '+ province.province_name + ' '+city.city_name
                            {0} from Media_mediaitems as a {1} where 1=1 {2}";
            string newcol = ",mediatype.name as mediatypename ";
            string jointable = @"left join Media_mediaindustryrelation as industryrelation on a.mediaitemid = industryrelation.mediaitemid
                                 left join media_Industries as industry on industryrelation.industryid = industry.industryid
                                 inner join media_mediatype as mediatype on a.mediaitemtype = mediatype.id  
                                 inner join Media_EventMediaRelation as event on a.MediaitemID = event.MediaitemID

                                left join media_industries as indust on a.IndustryID = indust.IndustryID 
                                left join media_country as country on a.countryid = country.countryid
                                left join Media_province as province on province.province_id = a.provinceid
                                left join media_city as city on city.city_id = a.cityid
                                ";

            string term = @" and a.del!=@del and event.del!=@del and a.status = @status and event.EventID = @evid 
                    group by a.mediaitemid,mediacname,mediaename,cshortname,
                            eshortname,mediaitemtype,currentversion,
                            status,createdbyuserid,createddate,lastmodifiedbyuserid,
                            lastmodifieddate,mediumsort,mediatype.name, 

                            a.RegionAttribute,a.industryid,a.Countryid,TelephoneExchange,
                            a.channelname,a.topicname,a.issueregion,
                            indust.industryname,country.countryname,province.province_name,city.city_name
                            order by a.mediaitemid desc
                            ";

            Hashtable ht = new Hashtable();
            ht.Add("@del", (int)Global.FiledStatus.Del);
            ht.Add("@status", (int)Global.MediaAuditStatus.FirstLevelAudit);
            ht.Add("@evid", eventId);

            sql = string.Format(sql, newcol, jointable, term);
            SqlParameter[] param = ESP.Media.Access.Utilities.Common.DictToSqlParam(ht);
            return clsSelect.QueryBySql(sql, param);
        }

        /// <summary>
        /// ��ȡ���߲�����¼�������Ϣ�б�
        /// </summary>
        /// <param name="reporterid">����ID</param>
        /// <param name="term">����</param>
        /// <param name="ht">Sqlparameter ����</param>
        /// <returns></returns>
        public static DataTable GetEventsByReporter(int reporterid,int projectid, string term, Hashtable ht)
        {
            string sql = @"select b.eventid as id,b.eventname as name
                            from Media_eventreporterRelation as event {0} where 1=1 {1}";

            string jointable = @"inner join media_Reporters as reporter on event.reporterid = reporter.reporterid
                                inner join media_events as b on event.eventid = b.eventid
                                ";
            if (term == null) term = string.Empty;
            term += @" and reporter.del!=@del and event.del !=@del and b.del!=@del and event.reporterid = @reporterid and b.projectid = @projectid
                            order by event.id desc
                            ";
            if (ht == null) ht = new Hashtable();
            if (!ht.ContainsKey("@del"))
            {
                ht.Add("@del", (int)Global.FiledStatus.Del);
            }
            if (!ht.ContainsKey("@reporterid"))
            {
                ht.Add("@reporterid", reporterid);
            }
            if (!ht.ContainsKey("@projectid"))
            {
                ht.Add("@projectid", projectid);
            }


            sql = string.Format(sql, jointable, term);
            SqlParameter[] param = ESP.Media.Access.Utilities.Common.DictToSqlParam(ht);
            return clsSelect.QueryBySql(sql, param);
        }

        /// <summary>
        /// ȡ���¼��������ѹ������ߵ��б�
        /// </summary>
        /// <param name="dailyId">�¼�����ID</param>
        /// <param name="term">����</param>
        /// <param name="ht">SqlParameters ����</param>
        /// <returns></returns>
        public static DataTable GetRelationReporters(int eventid, string term, Hashtable ht)
        {
            string sql = @"select city.city_name as CityName,
	                        mtype.name as TypeName,
                            a.MediaCName as MediaCName,
                            reporter.EmailOne as Email,
                            reporter.UsualMobile as Mobile,
	                        a.ReaderSort as ReaderSort,
                            TopicProperty = CASE a.TopicProperty
                                 WHEN '1' THEN '����'
                                 WHEN '2' THEN '��̸'
                                 WHEN '3' THEN '����'
                                 WHEN '4' THEN '����'
                                 WHEN '5' THEN '����'
                                 ELSE '��'
                              END,
	                        a.MediaCName as MediaCName,
	                        a.MediaType as MediaType,
                            a.mediaitemtype as TypeId,
	                        reporter.reporterId as reporterId,
	                        reporter.ReporterName as ReporterName,
	                        reporter.ReporterPosition as ReporterPosition,
	                        reporter.ReporterLevel as ReporterLevel,
                            event.paystatus as paystatus,
                            PayType = CASE event.PayType
                                 WHEN 0 THEN '����֧��'
                                 WHEN 1 THEN '��ǰ֧��'
                                 ELSE 'δ֪'
                              END,
                            event.id as relationid,
                            event.bankname as bankname,event.bankcardcode as bankcardcode,
                            event.bankcardname as bankcardname,event.bankacountname as bankacountname,
                            event.writingfee as writingfee,event.referral as referral, 
                            haveinvoice = case event.haveinvoice
                            when 0 then '��'
                            when 1 then '��'
                            ELSE 'δ֪'
                            end,a.mediaitemid as Mediaid,
                            sex = case reporter.sex when  1 then '��' when 2 then 'Ů' else '����' end,
                            reporter.ReporterPosition as ReporterPosition,
                            reporter.Tel_O as tel,
                            reporter.responsibledomain as responsibledomain,
                            medianame = a.mediacname + ' '+a.ChannelName+' '+a.TopicName,
                            b.eventname as eventname,b.eventid as eventid
                            
                            from Media_EventReporterRelation as event {0} where 1=1 {1}";

            string jointable = @"inner join media_Reporters as reporter on event.reporterid = reporter.reporterid
                                 inner join media_events as b on b.eventid = event.eventid
                                left join media_City as city on reporter.cityid = city.City_ID
	                            inner join Media_mediaitems as a on reporter.media_id = a.mediaitemid
                                left join media_mediatype as mtype on a.mediaitemtype = mtype.id";
            if (term == null) term = string.Empty;
            term += @"and a.del!=@del and reporter.del!=@del and event.del !=@del and b.del != @del
                            and a.status = @status and event.eventID = @evid
	                        group by mtype.name,a.mediaitemtype,city.city_name,a.MediaCName,a.MediaType,
		                    ReaderSort,TopicProperty,reporter.ReporterName,reporter.ReporterPosition,
		                    reporter.ReporterLevel,reporter.reporterId,reporter.UsualMobile,reporter.EmailOne,event.PayType,event.paystatus,
                            event.bankname,event.bankcardcode,a.mediaitemtype,
                            event.bankcardname,event.bankacountname,event.writingfee,a.mediaitemid,
                            event.referral,event.haveinvoice,event.id,
                            reporter.Tel_O,reporter.responsibledomain,reporter.sex,
                            a.channelname,a.topicname,
                            b.eventname,b.eventid
                            order by event.id desc
                            ";


            if (ht == null) ht = new Hashtable();
            if (!ht.ContainsKey("@del"))
            {
                ht.Add("@del", (int)Global.FiledStatus.Del);
            }
            if (!ht.ContainsKey("@status"))
            {
                ht.Add("@status", (int)Global.MediaAuditStatus.FirstLevelAudit);
            }
            if (!ht.ContainsKey("@evid"))
            {
                ht.Add("@evid", eventid);
            }

            sql = string.Format(sql, jointable, term);
            SqlParameter[] param = ESP.Media.Access.Utilities.Common.DictToSqlParam(ht);
            return clsSelect.QueryBySql(sql, param);
        }



        /// <summary>
        /// ɾ�����ߵĹ���
        /// </summary>
        /// <param name="relationid">���߹���ID</param>
        /// <param name="userid">������</param>
        /// <param name="errmsg">������Ϣ</param>
        /// <returns></returns>
        public static int DelReporter(int relationid, int userid, out string errmsg)
        {
            using (SqlConnection conn = new SqlConnection(clsConfigOperate.CustomerSqlConnection()))
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();
                try
                {
                    int delid = DelReporter(relationid, out errmsg, trans);
                    trans.Commit();
                    conn.Close();
                    return delid;
                }
                catch (Exception exp)
                {
                    trans.Rollback();
                    conn.Close();
                    errmsg = exp.Message;
                    return -2;
                }
            }
        }

        /// <summary>
        /// ɾ�����ߵĹ���(������)
        /// </summary>
        /// <param name="relationid">���߹���ID</param>
        /// <param name="trans">����</param>
        /// <param name="errmsg">������Ϣ</param>
        /// <returns></returns>
        public static int DelReporter(int relationid, out string errmsg, SqlTransaction trans)
        {
            errmsg = "ɾ���ɹ�!";
            try
            {
                ESP.Media.DataAccess.EventreporterrelationDataProvider.DeleteInfo(relationid, trans);
                return relationid;

            }
            catch (Exception exp)
            {
                errmsg = exp.Message;
                return -2;
            }
        }


        /// <summary>
        /// ɾ�����ߵĹ���
        /// </summary>
        /// <param name="relationids">���߹���ID����</param>
        /// <param name="userid">������</param>
        /// <param name="errmsg">������Ϣ</param>
        /// <returns></returns>
        public static int DelReporters(int[] relationids, int userid, out string errmsg)
        {
            using (SqlConnection conn = new SqlConnection(clsConfigOperate.CustomerSqlConnection()))
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();
                try
                {
                    DelReporters(relationids, userid, out errmsg, trans);
                    trans.Commit();
                    conn.Close();
                    return relationids.Length;
                }
                catch (Exception exp)
                {
                    trans.Rollback();
                    conn.Close();
                    errmsg = exp.Message;
                    return -2;
                }
            }
        }

        /// <summary>
        /// ɾ�����ߵĹ���(������)
        /// </summary>
        /// <param name="relationids">���߹���ID</param>
        /// <param name="userid">������</param>
        /// <param name="trans">����</param>
        /// <param name="errmsg">������Ϣ</param>
        /// <returns></returns>
        public static int DelReporters(int[] relationids, int userid, out string errmsg, SqlTransaction trans)
        {
            errmsg = "ɾ���ɹ�!";
            try
            {
                foreach (int rid in relationids)
                {
                    ESP.Media.DataAccess.EventreporterrelationDataProvider.DeleteInfo(rid, trans);
                }
                return relationids.Length;
            }
            catch (Exception exp)
            {
                errmsg = exp.Message;
                return -2;
            }
        }

        /// <summary>
        /// ɾ��ý��Ĺ���
        /// </summary>
        /// <param name="relationid">ý�����ID</param>
        /// <param name="userid">������</param>
        /// <param name="errmsg">������Ϣ</param>
        /// <returns></returns>
        public static int DelMedia(int relationid, int userid, out string errmsg)
        {
            using (SqlConnection conn = new SqlConnection(clsConfigOperate.CustomerSqlConnection()))
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();
                try
                {
                    int delid = DelMedia(relationid, out errmsg, trans);
                    trans.Commit();
                    conn.Close();
                    return delid;
                }
                catch (Exception exp)
                {
                    errmsg = exp.Message;
                    return -2;
                }
            }
        }

        /// <summary>
        /// ɾ��ý��Ĺ���(������)
        /// </summary>
        /// <param name="relationid">ý�����ID</param>
        /// <param name="trans">����</param>
        /// <param name="errmsg">������Ϣ</param>
        /// <returns></returns>
        public static int DelMedia(int relationid, out string errmsg, SqlTransaction trans)
        {
            errmsg = "ɾ���ɹ�!";
            try
            {
                ESP.Media.DataAccess.EventmediarelationDataProvider.DeleteInfo(relationid, trans);
                return relationid;

            }
            catch (Exception exp)
            {
                errmsg = exp.Message;
                return -2;
            }
        }

        /// <summary>
        /// ɾ��ý��Ĺ���
        /// </summary>
        /// <param name="relationids">ý�����ID����</param>
        /// <param name="userid">������</param>
        /// <param name="errmsg">������Ϣ</param>
        /// <returns></returns>
        public static int DelMedia(int[] relationids, int userid, out string errmsg)
        {
            using (SqlConnection conn = new SqlConnection(clsConfigOperate.CustomerSqlConnection()))
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();
                try
                {
                    DelMedias(relationids, userid, out errmsg, trans);
                    trans.Commit();
                    conn.Close();
                    return relationids.Length;
                }
                catch (Exception exp)
                {
                    trans.Rollback();
                    conn.Close();
                    errmsg = exp.Message;
                    return -2;
                }
            }
        }


        /// <summary>
        /// ɾ��ý��Ĺ���(������)
        /// </summary>
        /// <param name="relationid">ý�����ID����</param>
        /// <param name="userid">������</param>
        /// <param name="trans">����</param>
        /// <param name="errmsg">������Ϣ</param>
        /// <returns></returns>
        public static int DelMedias(int[] relationids, int userid, out string errmsg, SqlTransaction trans)
        {
            errmsg = "ɾ���ɹ�!";
            try
            {
                foreach (int rid in relationids)
                {
                    ESP.Media.DataAccess.EventmediarelationDataProvider.DeleteInfo(rid, trans);
                }
                return relationids.Length;
            }
            catch (Exception exp)
            {
                errmsg = exp.Message;
                return -2;
            }
        }



        public static bool SaveSignExcel(int eventId, string filename, out string errmsg,int userid)
        {
            errmsg = "�ɹ�";

            DataTable dt = GetRelationReporters(eventId, null, null);
            if (dt.Rows.Count == 0)
            {
                errmsg = "û�м���!";
                return false;
            }

            string fname = filename.Substring(filename.LastIndexOf('\\') + 1);
            string serverpath = filename.Substring(0, filename.LastIndexOf('\\'));
            if (!Directory.Exists(serverpath))
                Directory.CreateDirectory(serverpath);

                ExcelHandle excel = new ExcelHandle();
                excel.Load(serverpath + "\\ý��ǩ����.xls");
                excel.CurSheet = (Excel._Worksheet)excel.CurBook.Sheets[1];
                try
                {
                    string cell = "A4";
                    string medianame = dt.Rows[0]["MediaCName"].ToString().Trim();
                    string oldcell = "C4";
                    string fcell = string.Format("C{0}", dt.Rows.Count + 3);
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        cell = string.Format("A{0}", i + 4);
                        ExcelHandle.WriteCell(excel.CurSheet, cell, ((int)(i+1)).ToString());

                        cell = string.Format("B{0}", i + 4);
                        string cityname = dt.Rows[i]["CityName"] == DBNull.Value ? "δ֪" : dt.Rows[i]["CityName"].ToString();
                        ExcelHandle.WriteCell(excel.CurSheet, cell, cityname.Trim());

                        cell = string.Format("C{0}", i + 3);
                        if (dt.Rows[i]["MediaCName"].ToString().Trim() != medianame)
                        {
                            ExcelHandle.WriteAfterMerge(excel.CurSheet, oldcell, cell, medianame);
                            medianame = dt.Rows[i]["MediaCName"].ToString().Trim();
                            oldcell = string.Format("C{0}", i + 4);
                        }

                        cell = string.Format("D{0}", i + 4);
                        ExcelHandle.WriteCell(excel.CurSheet, cell, dt.Rows[i]["ReporterName"].ToString().Trim());

                        cell = string.Format("E{0}", i + 4);
                    }
                    ExcelHandle.WriteAfterMerge(excel.CurSheet, oldcell, fcell, medianame);
                    ExcelHandle.SetHAlignCenter(excel.CurSheet, "A4", cell);
                    ExcelHandle.SetBorderAll(excel.CurSheet, "A3", cell);

                    excel.CurSheet.Name = DateTime.Now.ToString().Split(' ')[0];
                    ExcelHandle.SaveAS(excel.CurBook, filename);

                    EventattachmentsInfo attach = new EventattachmentsInfo();
                    attach.Attachmentpath = ConfigManager.EventSignPath + fname;
                    attach.Createdbyuserid = userid;
                    attach.Createddate = DateTime.Now.ToString();
                    attach.Type = 0;
                    attach.Eventid = eventId;
                    using (SqlConnection conn = new SqlConnection(clsConfigOperate.CustomerSqlConnection()))
                    {
                        conn.Open();
                        SqlTransaction trans = conn.BeginTransaction();
                        try
                        {
                            attach.Version = GetLastEventAttachVersion(trans, 0, eventId);
                            ESP.Media.DataAccess.EventattachmentsDataProvider.insertinfo(attach, trans);
                            SetStatus(eventId, 3, trans);
                            excel.Dispose();
                            trans.Commit();
                            conn.Close();
                            return true;
                        }
                        catch { trans.Rollback(); conn.Close(); return false; }
                    }
                }
                catch (Exception exp)
                {
                    excel.Dispose();
                    errmsg = exp.Message;
                    if (File.Exists(filename))
                        File.Delete(filename);
                }
            return false;
        }

        /// <summary>
        /// ý�����û�б��¼�����ѡ�����ý���б�
        /// </summary>
        /// <param name="term">The term.</param>
        /// <param name="ht">The ht.</param>
        /// <param name="eventid">The eventid.</param>
        /// <returns></returns>
        public static DataTable GetUnSelectedMediaFromAll(string term, Hashtable ht, int eventid)
        {
            if (term == null) term = string.Empty;
            if (ht == null) ht = new Hashtable();
            DataTable dteventmedia = ESP.Media.BusinessLogic.EventsManager.GetRelationMedias(eventid);
            DataTable dtAllMedia = MediaitemsManager.GetAuditList(term, ht);
            if (dteventmedia == null || dteventmedia.Rows.Count == 0) return dtAllMedia;
            string mediaids = string.Empty;
            foreach (DataRow dr in dteventmedia.Rows)
            {
                mediaids += dr["mediaitemid"] == DBNull.Value ? "0" : dr["mediaitemid"].ToString() + ",";
            }
            mediaids.Trim(',');
            term = string.Format(" mediaitemid not in ({0})", mediaids);
            DataRow[] drs = dtAllMedia.Select(term);
            DataTable dt = dtAllMedia.Clone();
            ESP.Media.Access.Utilities.Common.DataRowsToDataTable(drs, ref dt);
            return dt;

        }


        /// <summary>
        /// �¼����ѹ���ý���е����м���
        /// </summary>
        /// <param name="projectid"></param>
        /// <param name="term"></param>
        /// <param name="ht"></param>
        /// <returns></returns>
        public static DataTable GetReporterFromMedia(int eventid)
        {
            string sql = @"select 
city.city_name as CityName,
	                        mtype.name as TypeName,
	                        b.ReaderSort as ReaderSort,
                            TopicProperty = CASE b.TopicProperty
                                 WHEN '1' THEN '����'
                                 WHEN '2' THEN '��̸'
                                 WHEN '3' THEN '����'
                                 WHEN '4' THEN '����'
                                 WHEN '5' THEN '����'
                                 ELSE '��'
                              END,
	                        b.MediaCName as MediaCName,
	                        b.MediaType as MediaType,
	                        a.reporterId as reporterId,
	                        a.ReporterName as ReporterName,
	                        a.ReporterPosition as ReporterPosition,
	                        a.ReporterLevel as ReporterLevel,
                            a.cardnumber as cardnumber,a.cityid as cityid,
                            a.birthday as birthday,a.UsualMobile as UsualMobile,
                            a.Tel_O as Tel_O,a.QQ as QQ,
                            a.MSN as MSN,a.Experience as Experience,
                            sex = case a.sex when  1 then '��' when 2 then 'Ů' else '����' end,
                            a.ReporterPosition as ReporterPosition,
                            a.tel_o as tel,
                            a.usualmobile as mobile,a.emailone as email,
                            a.responsibledomain as responsibledomain,
                            medianame = b.mediacname + ' '+b.ChannelName+' '+b.TopicName,
                            b.mediaitemid as mediaitemid
                             from media_reporters as a
                            inner join media_mediaitems as b on a.media_id = b.mediaitemid
                            inner join media_eventmediarelation as eventmedia on b.mediaitemid = eventmedia.mediaitemid
                            inner join media_mediatype as mtype on b.mediaitemtype = mtype.id  
                            left join media_City as city on a.cityid = city.City_ID 
                            where eventmedia.eventid = @eventid and b.del != @del and a.del != @del";
            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@eventid", SqlDbType.Int);
            param[0].Value = eventid;
            param[1] = new SqlParameter("@del", SqlDbType.Int);
            param[1].Value = (int)Global.FiledStatus.Del;
            return clsSelect.QueryBySql(sql, param);
        }

        /// <summary>
        /// Gets the un selected daily reporter.
        /// </summary>
        /// <param name="term">The term.</param>
        /// <param name="ht">The ht.</param>
        /// <param name="dailyid">The dailyid.</param>
        /// <param name="projectid">The projectid.</param>
        /// <returns></returns>
        public static DataTable GetUnSelectedReporterFromMedia(string term, Hashtable ht, int eventid)
        {
            if (term == null) term = string.Empty;
            if (ht == null) ht = new Hashtable();

            //DataTable dtdailymedia = Media_dailys.GetRelationMedias(dailyid);
            DataTable dteventreporter = ESP.Media.BusinessLogic.EventsManager.GetRelationReporters(eventid, null, null);
            DataTable dtAllReporters = GetReporterFromMedia(eventid);

            string mediaids = string.Empty;

            if (dteventreporter != null && dteventreporter.Rows.Count > 0)
            {
                foreach (DataRow dr in dteventreporter.Rows)
                {
                    mediaids += dr["reporterId"] == DBNull.Value ? "0" : dr["reporterId"].ToString() + ",";
                }
                mediaids.Trim(',');
                term += string.Format(" reporterId not  in ({0})", mediaids);

                DataTable ndt = dtAllReporters.Clone();
                if (!string.IsNullOrEmpty(term))
                {
                    DataRow[] drs = dtAllReporters.Select(term);
                    ESP.Media.Access.Utilities.Common.DataRowsToDataTable(drs, ref ndt);
                    return ndt;
                }
            }
            return dtAllReporters;

        }

        public static bool SaveCommunicateExcel(int eventId, string filename, out string errmsg, int userid)
        {
            errmsg = "�ɹ�";

            DataTable dt = GetRelationReporters(eventId, null, null);
            if (dt == null || dt.Rows.Count == 0)
            {
                errmsg = "û�м���!";
                return false;
            }
            string fname = filename.Substring(filename.LastIndexOf('\\') + 1);
            string serverpath = filename.Substring(0, filename.LastIndexOf('\\'));
            if (!Directory.Exists(serverpath))
                Directory.CreateDirectory(serverpath);
            ExcelHandle excel = new ExcelHandle();
            excel.Load(serverpath + "\\�����.xls");
            excel.CurSheet = (Excel._Worksheet)excel.CurBook.Sheets[1];
            try
            {

                string typeName = string.Empty;
                int rownum = 3;
                string cell = string.Empty;
                string medianame = string.Empty;
                int mcount = 1;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (typeName != dt.Rows[i]["TypeName"].ToString().Trim())
                    {
                        typeName = dt.Rows[i]["TypeName"].ToString().Trim();
                        ExcelHandle.SetHAlignLeft(excel.CurSheet, "A" + rownum.ToString(), "K" + rownum.ToString());
                        ExcelHandle.SetBackGroundColor(excel.CurSheet, "A" + rownum.ToString(), "K" + rownum.ToString(), System.Drawing.Color.LightYellow);
                        ExcelHandle.WriteAfterMerge(excel.CurSheet, "A" + rownum.ToString(), "K" + rownum.ToString(), typeName);
                        rownum++;
                    }

                    if (medianame != dt.Rows[i]["MediaCName"].ToString().Trim())
                    {
                        medianame = dt.Rows[i]["MediaCName"].ToString().Trim();
                        int j = i;
                        int c = -1;
                        while (j < dt.Rows.Count && medianame == dt.Rows[j]["MediaCName"].ToString().Trim())
                        {
                            j++;
                            c++;
                        }
                        string scell = string.Format("A{0}", rownum);
                        string ecell = string.Format("A{0}", rownum + c);
                        ExcelHandle.SetHAlignCenter(excel.CurSheet, scell, ecell);
                        ExcelHandle.WriteAfterMerge(excel.CurSheet, scell, ecell, mcount.ToString());

                        scell = string.Format("B{0}", rownum);
                        ecell = string.Format("B{0}", rownum + c);
                        ExcelHandle.SetHAlignCenter(excel.CurSheet, scell, ecell);
                        ExcelHandle.WriteAfterMerge(excel.CurSheet, scell, ecell, dt.Rows[i]["CityName"].ToString().Trim());

                        scell = string.Format("C{0}", rownum);
                        ecell = string.Format("C{0}", rownum + c);
                        ExcelHandle.SetHAlignCenter(excel.CurSheet, scell, ecell);
                        ExcelHandle.WriteAfterMerge(excel.CurSheet, scell, ecell, dt.Rows[i]["TypeName"].ToString().Trim());

                        scell = string.Format("D{0}", rownum);
                        ecell = string.Format("D{0}", rownum + c);
                        ExcelHandle.SetHAlignCenter(excel.CurSheet, scell, ecell);
                        if (dt.Rows[i]["TypeName"].ToString().Trim() == "����ý��")
                            ExcelHandle.WriteAfterMerge(excel.CurSheet, scell, ecell, dt.Rows[i]["TopicProperty"].ToString().Trim());
                        else
                            ExcelHandle.WriteAfterMerge(excel.CurSheet, scell, ecell, dt.Rows[i]["ReaderSort"].ToString().Trim());

                        scell = string.Format("E{0}", rownum);
                        ecell = string.Format("E{0}", rownum + c);
                        ExcelHandle.SetHAlignCenter(excel.CurSheet, scell, ecell);
                        ExcelHandle.WriteAfterMerge(excel.CurSheet, scell, ecell, dt.Rows[i]["MediaType"].ToString().Trim());

                        scell = string.Format("F{0}", rownum);
                        ecell = string.Format("F{0}", rownum + c);
                        ExcelHandle.SetHAlignCenter(excel.CurSheet, scell, ecell);
                        ExcelHandle.WriteAfterMerge(excel.CurSheet, scell, ecell, medianame);
                        mcount++;
                    }

                    cell = string.Format("G{0}", rownum);
                    ExcelHandle.SetHAlignCenter(excel.CurSheet, cell);
                    ExcelHandle.WriteCell(excel.CurSheet, cell, dt.Rows[i]["ReporterName"].ToString().Trim());

                    cell = string.Format("H{0}", rownum);
                    ExcelHandle.SetHAlignCenter(excel.CurSheet, cell);
                    ExcelHandle.WriteCell(excel.CurSheet, cell, dt.Rows[i]["ReporterLevel"].ToString().Trim());

                    cell = string.Format("I{0}", rownum);
                    ExcelHandle.SetHAlignCenter(excel.CurSheet, cell);
                    ExcelHandle.WriteCell(excel.CurSheet, cell, dt.Rows[i]["ReporterPosition"].ToString().Trim());

                    cell = string.Format("J{0}", rownum);
                    ExcelHandle.SetHAlignCenter(excel.CurSheet, cell);
                    //ExcelHandle.SetColumnWidth(excel.CurSheet, "J", 120.0);
                    //ExcelHandle.SetFormula(excel.CurSheet, cell, "#");
                    ExcelHandle.WriteCell(excel.CurSheet, cell, dt.Rows[i]["Mobile"].ToString().Trim());

                    cell = string.Format("K{0}", rownum);
                    ExcelHandle.SetHAlignCenter(excel.CurSheet, cell);
                    ExcelHandle.WriteCell(excel.CurSheet, cell, dt.Rows[i]["Email"].ToString().Trim());
                    rownum++;
                }

                ExcelHandle.SetBorderAll(excel.CurSheet, "A3", cell);

                excel.CurSheet.Name = DateTime.Now.ToString().Split(' ')[0];
                ExcelHandle.SaveAS(excel.CurBook, filename);

                EventattachmentsInfo attach = new EventattachmentsInfo();
                attach.Attachmentpath = ConfigManager.EventCommunicatePath + fname;
                attach.Createdbyuserid = userid;
                attach.Createddate = DateTime.Now.ToString();
                attach.Type = 1;
                attach.Eventid = eventId;



                using (SqlConnection conn = new SqlConnection(clsConfigOperate.CustomerSqlConnection()))
                {
                    conn.Open();
                    SqlTransaction trans = conn.BeginTransaction();
                    try
                    {
                        attach.Version = GetLastEventAttachVersion(trans, 1, eventId);
                        ESP.Media.DataAccess.EventattachmentsDataProvider.insertinfo(attach, trans);
                        SetStatus(eventId, 3, trans);
                        excel.Dispose();
                        trans.Commit();
                        conn.Close();
                        return true;
                    }
                    catch { trans.Rollback(); conn.Close(); return false; }
                }

            }
            catch (Exception exp)
            {
                excel.Dispose();
                errmsg = exp.Message;
                if (File.Exists(filename))
                    File.Delete(filename);
            }
            return false;
        }

        /// <summary>
        /// ��ȡ������ɵ�ͨ�����ý��ǩ����Ĵ��·��
        /// </summary>
        /// <param name="dvid">�¼�����ID</param>
        /// <param name="type">0ý��ǩ����1ͨ����</param>
        /// <returns></returns>
        public static string GetLastestAttachment(int evid, int type)
        {
            string sql = "select AttachmentPath from media_EventAttachments where Type=@type AND Eventid=@evid AND Version = (select Max(Version) from media_EventAttachments where Type=@type AND Eventid=@evid )";
            Hashtable ht = new Hashtable();
            ht.Add("@type", type);
            ht.Add("@evid", evid);
            SqlParameter[] param = ESP.Media.Access.Utilities.Common.DictToSqlParam(ht);

            DataTable dt;
            dt = clsSelect.QueryBySql(sql, param);
            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0][0] != DBNull.Value)
                {
                    return dt.Rows[0][0].ToString();
                }
            }
            return string.Empty;
        }

        /// <summary>
        /// ��ȡ������ɵ�ͨ�����ý��ǩ����İ汾��
        /// </summary>
        /// <param name="ts">����</param>
        /// <param name="type">0ý��ǩ����1ͨ����</param>
        /// <param name="dvid">�¼�����ID</param>
        /// <returns></returns>
        private static int GetLastEventAttachVersion(SqlTransaction ts, int type, int evid)
        {
            string sql = string.Format("select Max(Version) from media_EventAttachments where Type={0} AND Eventid={1}", type, evid);
            DataTable dt;
            if (ts == null)
                dt = clsSelect.QueryBySql(sql);
            else
                dt = clsSelect.QueryBySql(sql, ts);
            if (dt.Rows[0][0] != DBNull.Value)
                return (int)(dt.Rows[0][0]) + 1;
            else
                return 0;
        }

        /// <summary>
        /// �����¼�������״̬
        /// </summary>
        /// <param name="id">�¼�����ID</param>
        /// <param name="status"></param>
        /// <param name="ts"></param>
        public static void SetStatus(int id, int status, SqlTransaction ts)
        {
            Hashtable ht = new Hashtable();
            ht.Add("@id", id);
            ht.Add("@status", status);
            SqlParameter[] param = ESP.Media.Access.Utilities.Common.DictToSqlParam(ht);
            string sql = "update media_events set eventstatus = @status where eventid = @id and eventstatus < @status";
            if (ts == null)
            {
                using (SqlConnection conn = new SqlConnection(clsConfigOperate.CustomerSqlConnection()))
                {
                    SqlHelper.ExecuteNonQuery(conn, CommandType.Text, sql, param);
                }
            }
            else
                SqlHelper.ExecuteNonQuery(ts, CommandType.Text, sql, param);
        }


        /// <summary>
        /// ״̬����(��ʱ����)
        /// </summary>
        /// <param name="id">�¼�����ID</param>
        public static void SetBackStatus(int id)
        {
            SetBackStatus(id, null);
        }


        /// <summary>
        /// �������״̬����(��ʱ����)
        /// </summary>
        /// <param name="id">�¼�����ID</param>
        /// <param name="ts">����</param>
        public static void SetBackStatus(int id, SqlTransaction ts)
        {
            Hashtable ht = new Hashtable();
            ht.Add("@id", id);
            SqlParameter[] param = ESP.Media.Access.Utilities.Common.DictToSqlParam(ht);
            string sqlgetstatus = "select eventstatus from media_events where eventid = @id";
            string sql = @"update media_events set eventstatus = eventstatus-1 where eventid = @id and eventstatus > 0;";
            if (ts == null)
            {
                using (SqlConnection conn = new SqlConnection(clsConfigOperate.CustomerSqlConnection()))
                {
                    //SqlHelper.ExecuteNonQuery(conn, CommandType.Text, sql, param);
                    conn.Open();
                    SqlTransaction trans = conn.BeginTransaction();
                    try
                    {
                        DataTable dt = SqlHelper.ExecuteDataset(trans, CommandType.Text, sqlgetstatus,param).Tables[0];
                        if (dt == null || dt.Rows.Count == 0)
                        {
                            trans.Rollback();
                            conn.Close();
                            return;
                        }
                        int status = (dt.Rows[0][0] == DBNull.Value || dt.Rows[0][0].ToString().Trim().Length == 0) ? 0 : Convert.ToInt32(dt.Rows[0][0]);
                        switch (status)
                        {
                            case 0://�ѽ���
                                break;
                            case 1://�ѹ���ý��
                                string sql1 = "delete from Media_EventMediaRelation where eventid = @id;delete from Media_EventReporterRelation where eventid = @id";
                                SqlHelper.ExecuteNonQuery(trans, CommandType.Text, sql1, param);
                                break;
                            case 2://�ѷ�������
                                break;
                            case 3://�����ɱ��
                                break;
                            case 4://�ѽ���                   
                                break;
                            default:
                                break;
                        }
                        SqlHelper.ExecuteNonQuery(trans, CommandType.Text, sql, param);
                        trans.Commit();
                        conn.Close();
                    }
                    catch (Exception ex)
                    {
                        string err = ex.Message;
                        trans.Rollback();
                        conn.Close();
                    }
                }
            }
            else
            {
                DataTable dt = SqlHelper.ExecuteDataset(ts, CommandType.Text, sqlgetstatus,param).Tables[0];
                if (dt == null || dt.Rows.Count == 0)
                {
                    return;
                }
                int status = (dt.Rows[0][0] == DBNull.Value || dt.Rows[0][0].ToString().Trim().Length == 0) ? 0 : Convert.ToInt32(dt.Rows[0][0]);
                switch (status)
                {
                    case 0://�ѽ���
                        break;
                    case 1://�ѹ���ý��
                        string sql1 = "delete from Media_EventMediaRelation where eventid = @id;delete from Media_EventReporterRelation where eventid = @id";
                        SqlHelper.ExecuteNonQuery(ts, CommandType.Text, sql1, param);
                        break;
                    case 2://�ѷ�������
                        break;
                    case 3://�����ɱ��
                        break;
                    case 4://�ѽ���                   
                        break;
                    default:
                        break;
                }
                SqlHelper.ExecuteNonQuery(ts, CommandType.Text, sql, param);
            }


        }
    }
}
