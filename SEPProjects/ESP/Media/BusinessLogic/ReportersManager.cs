using System;
using System.Collections.Generic;
using System.Collections;
using ESP.Media.Entity;
using System.Data.SqlClient;
using System.Data;
using ESP.Media.Access.Utilities;

namespace ESP.Media.BusinessLogic
{
    public class ReportersManager
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ReportersManager"/> class.
        /// </summary>
        public ReportersManager()
        {
        }

        /// <summary>
        /// Gets all object list.
        /// </summary>
        /// <param name="term">The term.</param>
        /// <param name="param">The param.</param>
        /// <returns></returns>
        public static List<QueryReporterInfo> GetAllObjectList(string term, List<SqlParameter> param)
        {
            Hashtable ht = new Hashtable();
            if (param != null)
            {
                for (int i = 0; i < param.Count; i++)
                {
                    ht.Add(param[i].ParameterName, param[i].Value);
                }
            }
            DataTable dt = GetListForWrittingFee(term, ht);
            var query = from reporter in dt.AsEnumerable() select new QueryReporterInfo(reporter);
            List<QueryReporterInfo> items = new List<QueryReporterInfo>();
            foreach (QueryReporterInfo item in query)
            {
                items.Add(item);
            }
            return items;
        }

        /// <summary>
        /// Queries the specified terms.
        /// </summary>
        /// <param name="terms">The terms.</param>
        /// <param name="ht">The ht.</param>
        /// <returns></returns>
        private static DataTable query(string terms, Hashtable ht)
        {
            string sql = @"select  mtype.name as TypeName,city.city_name as CityName,a.reporterid as reporterid,
                            a.reportername as reportername,
                            sex = case a.sex when  1 then '男' when 2 then '女' else '保密' end,
                            a.ReporterPosition as ReporterPosition,
                            a.tel_o as tel,
                            a.usualmobile as mobile,a.emailone as email,
                            a.responsibledomain as responsibledomain,
                            medianame = media.mediacname + ' '+media.ChannelName+' '+media.TopicName,
                            TopicProperty = CASE media.TopicProperty
                                 WHEN '1' THEN '新闻'
                                 WHEN '2' THEN '访谈'
                                 WHEN '3' THEN '娱乐'
                                 WHEN '4' THEN '体育'
                                 WHEN '5' THEN '教育'
                                 ELSE '无'
                              END,
	                        media.MediaType as MediaType,media.ReaderSort , 
                           a.ReporterLevel,a.otherMessageSoftware,a.remark,media.mediaitemid,
                           a.CardNumber,a.bankname,a.PayType,a.bankacountname,a.bankcardname
                            from Media_reporters as a 
                            left join media_mediaitems as media on a.media_id = media.mediaitemid
                           left join media_City as city on a.cityid = city.City_ID
                            left join media_mediatype as mtype on media.mediaitemtype = mtype.id
                            where 1=1 {0} order by a.Reporterid desc";

            if (ht == null)
                ht = new Hashtable();
            if (terms == null)
                terms = string.Empty;
            terms = terms + " and a.del != @del ";
            if (!ht.ContainsKey("@del"))
            {
                ht.Add("@del", (int)Global.FiledStatus.Del);
            }
            sql = string.Format(sql, terms);
            return ESP.Media.Access.Utilities.clsSelect.QueryBySql(sql, ESP.Media.Access.Utilities.Common.DictToSqlParam(ht));
        }

        public static int GetReporterCount(string keys)
        {
            string terms = " a.del != @del";
            List<SqlParameter> parms = new List<SqlParameter>();
            parms.Add(new SqlParameter("@del", (int)Global.FiledStatus.Del));
            if (!string.IsNullOrEmpty(keys))
            {
                terms += " and (mtype.name {0} or city.city_name {0} or  a.reportername {0} or a.ReporterPosition {0} or a.usualmobile {0} or a.emailone {0} or a.responsibledomain {0} or media.mediacname + ' '+media.ChannelName+' '+media.TopicName {0})";
                terms = string.Format(terms, "like '%'+@keys+'%'");
                parms.Add(new SqlParameter("@keys", keys));
            }

            string sql = @"select  mtype.name as TypeName,city.city_name as CityName,a.reporterid as reporterid,
                            a.reportername as reportername,
                            sex = case a.sex when  1 then '男' when 2 then '女' else '保密' end,
                            a.ReporterPosition as ReporterPosition,
                            a.tel_o as tel,
                            a.usualmobile as mobile,a.emailone as email,
                            a.responsibledomain as responsibledomain,
                            medianame = media.mediacname + ' '+media.ChannelName+' '+media.TopicName,
                            TopicProperty = CASE media.TopicProperty
                                 WHEN '1' THEN '新闻'
                                 WHEN '2' THEN '访谈'
                                 WHEN '3' THEN '娱乐'
                                 WHEN '4' THEN '体育'
                                 WHEN '5' THEN '教育'
                                 ELSE '无'
                              END,
	                        media.MediaType as MediaType,media.ReaderSort , 
                           a.ReporterLevel,a.otherMessageSoftware,a.remark,media.mediaitemid,
                           a.CardNumber,a.bankname,a.PayType,a.bankacountname,a.bankcardname
                            from Media_reporters as a 
                            left join media_mediaitems as media on a.media_id = media.mediaitemid
                           left join media_City as city on a.cityid = city.City_ID
                            left join media_mediatype as mtype on media.mediaitemtype = mtype.id
                            where {0} order by a.Reporterid desc";
            sql = string.Format(sql, terms);
            return ESP.Media.Common.DbHelperSQL.Query(sql, parms.ToArray()).Tables[0].Rows.Count;
        }

        public static DataTable GetReporterPage(int pageSize, int pageIndex, string keys, string orderBy)
        {
            string terms = " a.del != @del";
            List<SqlParameter> parms = new List<SqlParameter>();
            parms.Add(new SqlParameter("@del", (int)Global.FiledStatus.Del));
            if (!string.IsNullOrEmpty(keys))
            {
                terms += " and (mtype.name {0} or city.city_name {0} or  a.reportername {0} or a.ReporterPosition {0} or a.usualmobile {0} or a.emailone {0} or a.responsibledomain {0} or media.mediacname + ' '+media.ChannelName+' '+media.TopicName {0})";
                terms = string.Format(terms, "like '%'+@keys+'%'");
                parms.Add(new SqlParameter("@keys", keys));
            }
            string sql = @"SELECT TOP (@PageSize) * FROM(
                            select  mtype.name as TypeName,city.city_name as CityName,a.reporterid as reporterid,
                            a.reportername as reportername,a.photo,
                            sex = case a.sex when  1 then '男' when 2 then '女' else '保密' end,
                            a.ReporterPosition as ReporterPosition,
                            a.tel_o as tel,
                            a.usualmobile as mobile,a.emailone as email,
                            a.responsibledomain as responsibledomain,
                            medianame = media.mediacname + ' '+media.ChannelName+' '+media.TopicName,
                            TopicProperty = CASE media.TopicProperty
                                 WHEN '1' THEN '新闻'
                                 WHEN '2' THEN '访谈'
                                 WHEN '3' THEN '娱乐'
                                 WHEN '4' THEN '体育'
                                 WHEN '5' THEN '教育'
                                 ELSE '无'
                              END,
	                        media.MediaType as MediaType,media.ReaderSort , 
                           a.ReporterLevel,a.otherMessageSoftware,a.remark,media.mediaitemid,
                           a.CardNumber,a.bankname,a.PayType,a.bankacountname,a.bankcardname
                           , ROW_NUMBER() OVER (ORDER BY @@@ORDERBY@@@) AS [__i_RowNumber]
                            from Media_reporters as a 
                            left join media_mediaitems as media on a.media_id = media.mediaitemid
                           left join media_City as city on a.cityid = city.City_ID
                            left join media_mediatype as mtype on media.mediaitemtype = mtype.id
                            WHERE @@@WHERE@@@ ) t where t.[__i_RowNumber] > @PageStart";
            if (!string.IsNullOrEmpty(orderBy))
                sql = sql.Replace("@@@ORDERBY@@@", orderBy);
            else
                sql = sql.Replace("@@@ORDERBY@@@", "a.Reporterid desc");

            sql = sql.Replace("@@@WHERE@@@", terms);
            parms.Add(new SqlParameter("@PageSize", pageSize));
            parms.Add(new SqlParameter("@PageStart", pageSize * pageIndex));
            return ESP.Media.Common.DbHelperSQL.Query(sql, parms.ToArray()).Tables[0];
        }

        /// <summary>
        /// Gets all list.
        /// </summary>
        /// <returns></returns>
        public static DataTable GetAllList()
        {
            return GetList(null, null);
        }

        /// <summary>
        /// Gets the list by media.
        /// </summary>
        /// <param name="terms">The terms.</param>
        /// <param name="ht">The ht.</param>
        /// <param name="mediaId">The media id.</param>
        /// <returns></returns>
        public static DataTable GetListByMedia(string terms, Hashtable ht, int mediaId)
        {
            if (ht == null)
                ht = new Hashtable();
            if (terms == null)
                terms = string.Empty;
            terms = terms + " and Media_ID=@mid ";
            if (!ht.ContainsKey("@mid"))
            {
                ht.Add("@mid", mediaId);
            }
            return GetList(terms, ht);
        }

        /// <summary>
        /// Gets the list.
        /// </summary>
        /// <param name="terms">The terms.</param>
        /// <param name="ht">The ht.</param>
        /// <returns></returns>
        public static DataTable GetList(string terms, Hashtable ht)
        {
            return query(terms, ht);
        }

        /// <summary>
        /// Gets the list for writting fee.
        /// </summary>
        /// <param name="terms">The terms.</param>
        /// <param name="ht">The ht.</param>
        /// <returns></returns>
        public static DataTable GetListForWrittingFee(string terms, Hashtable ht)
        {

            string sql = @"select  mtype.name as TypeName,a.cityname as CityName,a.reporterid as reporterid,
                            a.reportername as reportername,
                            sex = case a.sex when  1 then '男' when 2 then '女' else '保密' end,
                            a.ReporterPosition as ReporterPosition,
                            a.tel_o as tel,
                            a.usualmobile as mobile,a.emailone as email,
                            a.responsibledomain as responsibledomain,
                            medianame = media.mediacname + ' '+media.ChannelName+' '+media.TopicName,
                            TopicProperty = CASE media.TopicProperty
                                 WHEN '1' THEN '新闻'
                                 WHEN '2' THEN '访谈'
                                 WHEN '3' THEN '娱乐'
                                 WHEN '4' THEN '体育'
                                 WHEN '5' THEN '教育'
                                 ELSE '无'
                              END,
	                        media.MediaType as MediaType,media.ReaderSort , 
                           a.ReporterLevel,a.otherMessageSoftware,a.remark,media.mediaitemid,
                           a.CardNumber,a.bankname,a.PayType,a.bankacountname,a.bankcardname
                            from Media_reporters as a 
                            left join media_mediaitems as media on a.media_id = media.mediaitemid
                            left join media_mediatype as mtype on media.mediaitemtype = mtype.id
                            where 1=1 {0} order by a.Reporterid desc";

            if (ht == null)
                ht = new Hashtable();
            if (terms == null)
                terms = string.Empty;
            terms = terms + " and a.del != @del ";
            if (!ht.ContainsKey("@del"))
            {
                ht.Add("@del", (int)Global.FiledStatus.Del);
            }
            sql = string.Format(sql, terms);
            return ESP.Media.Access.Utilities.clsSelect.QueryBySql(sql, ESP.Media.Access.Utilities.Common.DictToSqlParam(ht));
        }

        /// <summary>
        /// Gets the model.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns></returns>
        public static ReportersInfo GetModel(int id)
        {
            return ESP.Media.DataAccess.ReportersDataProvider.Load(id);
        }

        /// <summary>
        /// Adds the specified obj.
        /// </summary>
        /// <param name="obj">The obj.</param>
        /// <param name="filename">The filename.</param>
        /// <param name="userid">The userid.</param>
        /// <param name="errmsg">The errmsg.</param>
        /// <returns></returns>
        public static int Add(ReportersInfo obj, string filename, int userid, out string errmsg)
        {

            using (SqlConnection conn = new SqlConnection(ESP.Media.Access.Utilities.clsConfigOperate.CustomerSqlConnection()))
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();
                try
                {
                    errmsg = string.Empty;
                    if (!string.IsNullOrEmpty(filename) && filename.Length > 0)
                    {
                        obj.Photo = filename.ToString();
                    }
                    obj.Currentversion = CommonManager.GetLastVersion("Reporter", obj.Reporterid, trans);
                    int ret = ESP.Media.DataAccess.ReportersDataProvider.insertinfo(obj, trans);

                    OperatelogManager.add((int)Global.SysOperateType.Add, (int)Global.Tables.reporters, userid, trans);//添加记者日志
                    obj.Reporterid = ret;
                    obj.Lastmodifiedbyuserid = userid;
                    obj.Lastmodifiedip = obj.Lastmodifiedip;
                    obj.Lastmodifieddate = DateTime.Now.ToString();
                    SaveHist(trans, obj, userid);
                    trans.Commit();
                    conn.Close();
                    ESP.Logging.Logger.Add("Save a new reporter is success.", "Media system", ESP.Logging.LogLevel.Information);
                    return ret;
                }
                catch (Exception exception)
                {
                    ESP.Logging.Logger.Add("Save a new reporter is error.", "Media system", ESP.Logging.LogLevel.Information, exception);
                    trans.Rollback();
                    conn.Close();
                    errmsg = exception.Message;
                    return -2;
                }
            }
        }

        /// <summary>
        /// Adds the with project.
        /// </summary>
        /// <param name="obj">The obj.</param>
        /// <param name="filename">The filename.</param>
        /// <param name="projectid">The projectid.</param>
        /// <param name="userid">The userid.</param>
        /// <param name="errmsg">The errmsg.</param>
        /// <returns></returns>
        public static int AddWithProject(ReportersInfo obj, string filename, int projectid, int userid, out string errmsg)
        {
            using (SqlConnection conn = new SqlConnection(ESP.Media.Access.Utilities.clsConfigOperate.CustomerSqlConnection()))
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();
                try
                {
                    errmsg = string.Empty;
                    if (!string.IsNullOrEmpty(filename) && filename.Length > 0)
                    {
                        obj.Photo = filename.ToString();
                    }
                    obj.Currentversion = CommonManager.GetLastVersion("Reporter", obj.Reporterid, trans);
                    int ret = ESP.Media.DataAccess.ReportersDataProvider.insertinfo(obj, trans);
                    ProjectreporterrelationInfo r = new ESP.Media.Entity.ProjectreporterrelationInfo();
                    r.Projectid = projectid;
                    r.Reporterid = ret;
                    r.Bankacountname = obj.Bankacountname;
                    r.Bankcardcode = obj.Bankcardcode;
                    r.Bankcardname = obj.Bankcardname;
                    r.Cooperatecircs = obj.Cooperatecircs;
                    r.Haveinvoice = obj.Haveinvoice;
                    r.Paystatus = obj.Paystatus;
                    r.Paytype = obj.Paytype;
                    r.Writingfee = obj.Writingfee;
                    r.Referral = obj.Referral;
                    r.Paymentmode = obj.Paymentmode;
                    r.Relationdate = DateTime.Now.ToString();
                    r.Relationuserid = userid;
                    ret = ESP.Media.DataAccess.ProjectreporterrelationDataProvider.insertinfo(r, trans);
                    //OperatelogManager.add((int)Global.SysOperateType.Add, (int)Global.Tables.reporters, userid, trans);//添加记者日志
                    obj.Reporterid = ret;
                    obj.Lastmodifiedbyuserid = userid;
                    obj.Lastmodifiedip = obj.Lastmodifiedip;
                    obj.Lastmodifieddate = DateTime.Now.ToString();
                    trans.Commit();
                    conn.Close();
                    ESP.Logging.Logger.Add("Save a new reporter is sucess.", "Media system", ESP.Logging.LogLevel.Information);
                    return ret;
                }
                catch (Exception exception)
                {
                    ESP.Logging.Logger.Add("Save a new reporter is error.", "Media system", ESP.Logging.LogLevel.Information, exception);
                    trans.Rollback();
                    conn.Close();
                    errmsg = exception.Message;
                    return -2;
                }
            }
        }

        /// <summary>
        /// Updates the specified obj.
        /// </summary>
        /// <param name="obj">The obj.</param>
        /// <param name="filename">The filename.</param>
        /// <param name="userid">The userid.</param>
        /// <param name="errmsg">The errmsg.</param>
        /// <returns></returns>
        public static int Update(ReportersInfo obj, string filename, int userid, out string errmsg)
        {
            int histcount = 0;
            DateTime begin = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0);
            DateTime end = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 23, 59, 59);
            DataTable dt = ESP.Media.BusinessLogic.ReportersManager.GetHistListCurrentDay(obj.Reporterid, userid, begin, end);
            histcount = dt.Rows.Count;
            using (SqlConnection conn = new SqlConnection(ESP.Media.Access.Utilities.clsConfigOperate.CustomerSqlConnection()))
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();
                try
                {
                    errmsg = string.Empty;
                    if (!string.IsNullOrEmpty(filename) && filename.Length > 0)
                    {
                        obj.Photo = filename.ToString();
                    }
                    obj.Currentversion = CommonManager.GetLastVersion("Reporter", obj.Reporterid, trans);
                    if (ESP.Media.DataAccess.ReportersDataProvider.updateInfo(trans, null, obj, string.Empty, null))
                    {
                        obj.Lastmodifiedbyuserid = userid;
                        obj.Lastmodifiedip = obj.Lastmodifiedip;
                        obj.Lastmodifieddate = DateTime.Now.ToString();
                        SaveHist(trans, obj, userid);
                        if (histcount == 0)
                            OperatelogManager.add((int)Global.SysOperateType.Edit, (int)Global.Tables.reporters, userid, trans);//更新记者信息
                        trans.Commit();
                        conn.Close();
                        ESP.Logging.Logger.Add("Save a new reporter is sucess.", "Media system", ESP.Logging.LogLevel.Information);
                        return 1;
                    }
                    else
                    {
                        errmsg = "修改失败!";
                        trans.Rollback();
                        ESP.Logging.Logger.Add("Save a new reporter is failed.", "Media system", ESP.Logging.LogLevel.Information);
                        conn.Close();
                        return -3;
                    }
                }
                catch (Exception exception)
                {
                    trans.Rollback();
                    conn.Close();
                    ESP.Logging.Logger.Add("Update a reporter is error.", "Media system", ESP.Logging.LogLevel.Information, exception);
                    errmsg = exception.Message;
                    return -2;
                }
            }
        }

        /// <summary>
        /// Deletes the specified obj.
        /// </summary>
        /// <param name="obj">The obj.</param>
        /// <param name="errmsg">The errmsg.</param>
        /// <returns></returns>
        public static int Delete(ReportersInfo obj, out string errmsg)
        {
            errmsg = "删除成功!";
            try
            {
                obj.Del = (int)Global.FiledStatus.Del;
                if (ESP.Media.DataAccess.ReportersDataProvider.updateInfo(null, obj, string.Empty, null))
                {
                    ESP.Logging.Logger.Add("Delete a reporter is sucess.", "Media system", ESP.Logging.LogLevel.Information);
                    return 1;
                }
                else
                {
                    errmsg = "删除失败!";
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
        /// Links to media.
        /// </summary>
        /// <param name="reporterIds">The reporter ids.</param>
        /// <param name="mediaId">The media id.</param>
        /// <param name="errmsg">The errmsg.</param>
        /// <returns></returns>
        public static int LinkToMedia(int[] reporterIds, int mediaId, out string errmsg)
        {
            if (reporterIds.Length <= 0)
            {
                errmsg = "没有关联记者";
                return -1;
            }
            errmsg = string.Empty;
            string where = "WHERE ReporterID in({0}) ";
            string ids = string.Empty;
            foreach (int i in reporterIds)
            {
                ids += i.ToString() + ",";
            }
            where = string.Format(where, ids.Trim(','));
            string sql = string.Format("UPDATE media_Reporters SET Media_ID={0} {1}", mediaId, where);
            try
            {
                int value = ESP.Media.Access.Utilities.clsUpdate.funUpdate(sql);
                ESP.Logging.Logger.Add("Reporter link to media is sucess.", "Media system", ESP.Logging.LogLevel.Information, null, (object)("reporterid = " + reporterIds.ToString()));
                return value;
            }
            catch (Exception exception)
            {
                ESP.Logging.Logger.Add("Reporter link to media is error.", "Media system", ESP.Logging.LogLevel.Information, exception, (object)("reporterid = " + reporterIds.ToString()));
                errmsg = exception.Message;
                return -2;
            }
        }

        /// <summary>
        /// Deletes the relation.
        /// </summary>
        /// <param name="reporterid">The reporterid.</param>
        /// <param name="errmsg">The errmsg.</param>
        /// <returns></returns>
        public static int DeleteRelation(int reporterid, out string errmsg)
        {
            ReportersInfo obj = GetModel(reporterid);
            return DeleteRelation(obj, out errmsg);
        }

        /// <summary>
        /// Deletes the relation.
        /// </summary>
        /// <param name="obj">The obj.</param>
        /// <param name="errmsg">The errmsg.</param>
        /// <returns></returns>
        public static int DeleteRelation(ReportersInfo obj, out string errmsg)
        {
            errmsg = "删除成功!";
            try
            {
                obj.Media_id = 0;
                if (ESP.Media.DataAccess.ReportersDataProvider.updateInfo(null, obj, string.Empty, null))
                {
                    ESP.Logging.Logger.Add("Delete a reporter relation is success.", "Media system", ESP.Logging.LogLevel.Information);
                    return 1;
                }
                else
                {
                    errmsg = "删除失败!";
                    ESP.Logging.Logger.Add("Delete a reporter relation is failed.", "Media system", ESP.Logging.LogLevel.Information);
                    return -3;
                }
            }
            catch (Exception exception)
            {
                ESP.Logging.Logger.Add("Delete a reporter relation is error.", "Media system", ESP.Logging.LogLevel.Information, exception);
                errmsg = exception.Message;
                return -2;
            }
        }

        /// <summary>
        /// Queryhists the specified terms.
        /// </summary>
        /// <param name="terms">The terms.</param>
        /// <param name="param">The param.</param>
        /// <returns></returns>
        private static DataTable queryhist(string terms, params SqlParameter[] param)
        {
            string sql = @"select  a.media_id as media_id,a.version as version,a.reporterid as reporterid,
                            a.reportername as reportername,
                            sex = case a.sex when  1 then '男' when 2 then '女' else '保密' end,
                            a.ReporterPosition as ReporterPosition,
                            a.tel_o as tel,
                            a.usualmobile as mobile,a.emailone as email,
                            a.responsibledomain as responsibledomain,
                            medianame = media.mediacname + ' '+media.ChannelName+' '+media.TopicName,a.otherMessageSoftware,a.remark
                            from Media_reportershist as a 
                            left join media_mediaitems as media on a.media_id = media.mediaitemid
                            where 1=1 {0} order by a.Reporterid,version desc";


            sql = string.Format(sql, terms);
            return ESP.Media.Access.Utilities.clsSelect.QueryBySql(sql, param);
        }

        private static DataTable queryhistFullInfo(string terms, params SqlParameter[] param)
        {
            string sql = @"select  a.*,
                            sex = case a.sex when  1 then '男' when 2 then '女' else '保密' end,
                            medianame = media.mediacname + ' '+media.ChannelName+' '+media.TopicName
                            from Media_reportershist as a 
                            left join media_mediaitems as media on a.media_id = media.mediaitemid
                            where 1=1 {0} order by a.Reporterid,version desc";
            sql = string.Format(sql, terms);
            return ESP.Media.Access.Utilities.clsSelect.QueryBySql(sql, param);
        }

        /// <summary>
        /// Gets the hist list by client ID.
        /// </summary>
        /// <param name="reporterID">The reporter ID.</param>
        /// <returns></returns>
        public static DataTable GetHistListByClientID(int reporterID)
        {
            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@id", SqlDbType.Int);
            param[0].Value = reporterID;
            param[1] = new SqlParameter("@del", SqlDbType.Int);
            param[1].Value = (int)Global.FiledStatus.Del;
            return queryhist(" and a.del!=@del and a.reporterID=@id", param);
        }

        public static DataTable GetHistListCurrentDay(int reporterID, int userid, DateTime beginDate, DateTime enddate)
        {
            SqlParameter[] param = new SqlParameter[5];
            param[0] = new SqlParameter("@id", SqlDbType.Int);
            param[0].Value = reporterID;
            param[1] = new SqlParameter("@del", SqlDbType.Int);
            param[1].Value = (int)Global.FiledStatus.Del;
            param[2] = new SqlParameter("@createdbyuserid", SqlDbType.Int);
            param[2].Value = userid;
            param[3] = new SqlParameter("@begindate", SqlDbType.DateTime,8);
            param[3].Value = beginDate;
            param[4] = new SqlParameter("@enddate", SqlDbType.DateTime,8);
            param[4].Value = enddate;

            return queryhist(" and a.reporterID=@id and a.del!=@del and a.createdbyuserid=@createdbyuserid and (a.createddate between @begindate and @enddate)", param);
        }


        public static DataTable GetHistFullInfoByClientID(int reporterID)
        {
            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@id", SqlDbType.Int);
            param[0].Value = reporterID;
            param[1] = new SqlParameter("@del", SqlDbType.Int);
            param[1].Value = (int)Global.FiledStatus.Del;
            return queryhistFullInfo(" and a.del!=@del and a.reporterID=@id", param);
        }

        /// <summary>
        /// Gets the hist model.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns></returns>
        public static ReportershistInfo GetHistModel(int id)
        {
            if (id <= 0) return new ReportershistInfo();
            return ESP.Media.DataAccess.ReportershistDataProvider.Load(id);
        }

        /// <summary>
        /// Saves the contents.
        /// </summary>
        /// <param name="trans">The trans.</param>
        /// <param name="obj">The obj.</param>
        private static void SaveContents(SqlTransaction trans, ReportersInfo obj)
        {
            ReportercontentsInfo contents = new ReportercontentsInfo();

            contents.Contentxml = CommonManager.GetCntentsXml("Reporter", obj,
                //基本信息
                "Reportername", "Penname", "Sex", "Birthday", "Postcode_h", "Cardnumber", "Address_h",
                //联系信息
                "Tel_o", "Tel_h", "Usualmobile", "Backupmobile", "Fax", "Qq", "Msn", "Emailone", "Emailtwo", "Emailthree",
                //个人信息
                "Attention", "Hobby", "Character", "Marriage", "Family", "Writing",
                //教育信息
                "Education",
                //照片
                "Photo",
                //工作信息
                "Experience"
                );
            contents.Version = obj.Currentversion;
            contents.Reporterid = obj.Reporterid;
            contents.Modifiedbyuserid = obj.Lastmodifiedbyuserid;
            contents.Modifiedbyusername = obj.Lastmodifiedip;
            contents.Modifieddate = obj.Lastmodifieddate;
            try
            {
                ESP.Media.DataAccess.ReportercontentsDataProvider.insertinfo(contents, trans);
                ESP.Logging.Logger.Add("Save reporter contents is success.", "Media system", ESP.Logging.LogLevel.Information);
            }
            catch (Exception exception)
            {
                ESP.Logging.Logger.Add("Save reporter contents is error.", "Media system", ESP.Logging.LogLevel.Information, exception);
            }
        }

        /// <summary>
        /// Saves the hist.
        /// </summary>
        /// <param name="trans">The trans.</param>
        /// <param name="obj">The obj.</param>
        /// <param name="userid">The userid.</param>
        private static void SaveHist(SqlTransaction trans, ReportersInfo obj, int userid)
        {
            ReportershistInfo hist = new ReportershistInfo();
            hist.Lastmodifiedbyuserid = userid;
            hist.Lastmodifiedip = obj.Lastmodifiedip;
            hist.Lastmodifieddate = DateTime.Now.ToString();
            //基本信息
            hist.Reporterid = obj.Reporterid;
            hist.Reportername = obj.Reportername;
            hist.Penname = obj.Penname;
            hist.Sex = obj.Sex;
            hist.Birthday = obj.Birthday;
            hist.Postcode_h = obj.Postcode_h;
            hist.Cardnumber = obj.Cardnumber;
            hist.Address_h = obj.Address_h;
            //联系信息
            hist.Tel_o = obj.Tel_o;
            hist.Tel_h = obj.Tel_h;
            hist.Usualmobile = obj.Usualmobile;
            hist.Backupmobile = obj.Backupmobile;
            hist.Fax = obj.Fax;
            hist.Qq = obj.Qq;
            hist.Msn = obj.Msn;
            hist.Emailone = obj.Emailone;
            hist.Emailtwo = obj.Emailtwo;
            hist.Emailthree = obj.Emailthree;
            //个人信息
            hist.Attention = obj.Attention;
            hist.Hobby = obj.Hobby;
            hist.Character = obj.Character;
            hist.Marriage = obj.Marriage;
            hist.Family = obj.Family;
            hist.Writing = obj.Writing;
            //教育信息
            hist.Education = obj.Education;
            //照片
            hist.Photo = obj.Photo;
            //工作信息
            hist.Experience = obj.Experience;
            hist.Hometown = obj.Hometown;
            hist.Createdbyuserid = obj.Lastmodifiedbyuserid;
            hist.Createddate = obj.Lastmodifieddate;
            hist.Createdip = obj.Lastmodifiedip;
            hist.Status = obj.Status;

            hist.Bankname = obj.Bankname;
            hist.Bankacountname = obj.Bankacountname;
            hist.Paytype = obj.Paytype;
            hist.Paymentmode = obj.Paymentmode;
            hist.Writingfee = obj.Writingfee;
            hist.Referral = obj.Referral;
            hist.Haveinvoice = obj.Haveinvoice;
            hist.Paystatus = obj.Paystatus;
            hist.Uploadstarttime = obj.Uploadstarttime;
            hist.Uploadendtime = obj.Uploadendtime;
            hist.Privateremark = obj.Privateremark;
            hist.Cooperatecircs = obj.Cooperatecircs;
            hist.Cityid = obj.Cityid;
            hist.CityName = obj.CityName;
            hist.Othermessagesoftware = obj.Othermessagesoftware;
            hist.Remark = obj.Remark;
            hist.OfficeAddress = obj.OfficeAddress;
            hist.OfficePostID = obj.OfficePostID;
            hist.Media_id = obj.Media_id;
            try
            {
                hist.Version = CommonManager.GetLastVersion("Reporter", obj.Reporterid, trans);
                ESP.Media.DataAccess.ReportershistDataProvider.insertinfo(hist, trans);
                ESP.Logging.Logger.Add("Save reporter history is success.", "Media system", ESP.Logging.LogLevel.Information);
            }
            catch (Exception exception)
            {
                ESP.Logging.Logger.Add("Save reporter history is error.", "Media system", ESP.Logging.LogLevel.Information, exception);
            }
        }
    }
}