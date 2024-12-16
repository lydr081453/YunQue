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
    public class ReporterfavoriteManager
    {
        public static ReporterfavoriteInfo GetModel(int id)
        {
            ReporterfavoriteInfo favorite = null;
            favorite = ESP.Media.DataAccess.ReporterfavoriteDataProvider.Load(id);
            if (favorite == null) favorite = new ESP.Media.Entity.ReporterfavoriteInfo();
            return favorite;
        }

        public static int add(int[] reporterids, int userid)
        {
            if (reporterids == null || reporterids.Length == 0) return -1;
            using (SqlConnection conn = new SqlConnection(ESP.Media.Access.Utilities.clsConfigOperate.CustomerSqlConnection()))
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();
                try
                {
                    int id = 0;
                    for (int i = 0; i < reporterids.Length; i++)
                    {
                        id = add(reporterids[i], userid,trans);
                    }
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

        public static int add(ReporterfavoriteInfo[] c, int userid)
        {
            if (c == null || c.Length == 0) return -1;
            using (SqlConnection conn = new SqlConnection(ESP.Media.Access.Utilities.clsConfigOperate.CustomerSqlConnection()))
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();
                try
                {
                    int id=0;
                    for (int i = 0; i < c.Length; i++)
                    {
                        id = add(c[i], trans, userid);
                    }
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

        public static int add(ReporterfavoriteInfo c, int userid)
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

        public static int add(int rid,  int userid,SqlTransaction trans)
        {
            int id = 0;
            ReporterfavoriteInfo c = new ESP.Media.Entity.ReporterfavoriteInfo();
            c.Reporterid = rid;
            c.Userid = userid;
            c.Del = (int)Global.FiledStatus.Usable;
            c.Createdate = DateTime.Now.ToString();
            id = ESP.Media.DataAccess.ReporterfavoriteDataProvider.insertinfo(c, trans);
            return id;
        }

        public static int add(ReporterfavoriteInfo c, SqlTransaction trans, int userid)
        {
            int id = 0;
            id = ESP.Media.DataAccess.ReporterfavoriteDataProvider.insertinfo(c, trans);
            return id;
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="p">p</param>
        /// <param name="emp">当前登录人</param>
        /// <returns></returns>
        public static bool modify(ReporterfavoriteInfo c, Employee emp)
        {
            using (SqlConnection conn = new SqlConnection(ESP.Media.Access.Utilities.clsConfigOperate.CustomerSqlConnection()))
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();
                try
                {
                    bool result = modify(c, trans, emp);
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
        public static bool modify(ReporterfavoriteInfo c, SqlTransaction trans, Employee emp)
        {
            bool result = false;
            result = ESP.Media.DataAccess.ReporterfavoriteDataProvider.updateInfo(trans, null, c, null, null);
            return result;
        }


        public static int delete(int[] reporterids, int userid)
        {
            if (reporterids == null || reporterids.Length == 0) return -1;
            using (SqlConnection conn = new SqlConnection(ESP.Media.Access.Utilities.clsConfigOperate.CustomerSqlConnection()))
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();
                try
                {
                    int id = 0;
                    for(int i=0;i<reporterids.Length;i++)
                    {
                        id += delete(reporterids[i], userid, trans);
                    }
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

        public static int delete(int rid, int userid,SqlTransaction trans)
        {

            string sql = "delete media_reporterfavorite where reporterid=@reporterid and userid=@userid";
            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@reporterid",SqlDbType.Int);
            param[0].Value = rid;
            param[1] = new SqlParameter("@userid",SqlDbType.Int);
            param[1].Value = userid;

            int id = ESP.Media.Access.Utilities.SqlHelper.ExecuteNonQuery(trans, CommandType.Text, sql, param);
            return id;
        }

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <returns></returns>
        public static DataTable getAllList()
        {
            return ESP.Media.DataAccess.ReporterfavoriteDataProvider.QueryInfo(null, new Hashtable());
        }


        public static DataTable GetSelectedReporter(int userid,string term,Hashtable ht)
        {
            string sql = @"select city.city_name as CityName,
	                        mtype.name as TypeName,
                            a.MediaCName as MediaCName,
                            reporter.EmailOne as Email,
                            reporter.UsualMobile as Mobile,
	                        a.ReaderSort as ReaderSort,
                            TopicProperty = CASE a.TopicProperty
                                 WHEN '1' THEN '新闻'
                                 WHEN '2' THEN '访谈'
                                 WHEN '3' THEN '娱乐'
                                 WHEN '4' THEN '体育'
                                 WHEN '5' THEN '教育'
                                 ELSE '无'
                              END,
	                        a.MediaCName as MediaCName,
	                        a.MediaType as MediaType,
                            a.mediaitemtype as TypeId,
	                        reporter.reporterId as reporterId,
	                        reporter.ReporterName as ReporterName,
	                        reporter.ReporterPosition as ReporterPosition,
	                        reporter.ReporterLevel as ReporterLevel,
                            sex = case reporter.sex when  1 then '男' when 2 then '女' else '保密' end,
                            reporter.ReporterPosition as ReporterPosition,
                            reporter.Tel_O as tel,
                            reporter.responsibledomain as responsibledomain,
                            medianame = a.mediacname + ' '+a.ChannelName+' '+a.TopicName
                            from media_Reporters as reporter {0} where 1=1 {1}";

            string jointable = @"left join Media_reporterfavorite as favorite on favorite.reporterid = reporter.reporterid
                                left join media_City as city on reporter.cityid = city.City_ID
	                            left join Media_mediaitems as a on reporter.media_id = a.mediaitemid
                                left join media_mediatype as mtype on a.mediaitemtype = mtype.id";
            if (term == null) term = string.Empty;
            term += @" and reporter.del!=@del and favorite.del !=@del and favorite.userid=@userid

                            order by favorite.id desc
                            ";


            if (ht == null) ht = new Hashtable();
            if (!ht.ContainsKey("@del"))
            {
                ht.Add("@del", (int)Global.FiledStatus.Del);
            }
            if (!ht.ContainsKey("@userid"))
            {
                ht.Add("@userid", userid);
            }
            sql = string.Format(sql, jointable, term);
            SqlParameter[] param = ESP.Media.Access.Utilities.Common.DictToSqlParam(ht);
            return ESP.Media.Access.Utilities.clsSelect.QueryBySql(sql, param);
        }


        public static DataTable GetUnSelectedReporter(int userid, string term, Hashtable ht)
        {
            string sql = @"select
                            favorite.id as favoriteid,favorite.reporterid as frid,
                            favorite.userid as fuid,
                             city.city_name as CityName,
	                        mtype.name as TypeName,
                            a.MediaCName as MediaCName,
                            reporter.EmailOne as Email,
                            reporter.UsualMobile as Mobile,
	                        a.ReaderSort as ReaderSort,
                            TopicProperty = CASE a.TopicProperty
                                 WHEN '1' THEN '新闻'
                                 WHEN '2' THEN '访谈'
                                 WHEN '3' THEN '娱乐'
                                 WHEN '4' THEN '体育'
                                 WHEN '5' THEN '教育'
                                 ELSE '无'
                              END,
	                        a.MediaCName as MediaCName,
	                        a.MediaType as MediaType,
                            a.mediaitemtype as TypeId,
	                        reporter.reporterId as reporterId,
	                        reporter.ReporterName as ReporterName,
	                        reporter.ReporterPosition as ReporterPosition,
	                        reporter.ReporterLevel as ReporterLevel,
                            sex = case reporter.sex when  1 then '男' when 2 then '女' else '保密' end,
                            reporter.ReporterPosition as ReporterPosition,
                            reporter.Tel_O as tel,
                            reporter.responsibledomain as responsibledomain,
                            medianame = a.mediacname + ' '+a.ChannelName+' '+a.TopicName
                            from media_Reporters as reporter 
                                left join Media_reporterfavorite as favorite on favorite.reporterid = reporter.reporterid
                                left join media_City as city on reporter.cityid = city.City_ID
	                            left join Media_mediaitems as a on reporter.media_id = a.mediaitemid
                                left join media_mediatype as mtype on a.mediaitemtype = mtype.id
                            where  reporter.del!=@del
                            and (favorite.userid is null or favorite.userid!=@userid)
                            {0}
                            order by favorite.id desc";

            if (term == null) term = string.Empty;

            if (ht == null) ht = new Hashtable();
            if (!ht.ContainsKey("@del"))
            {
                ht.Add("@del", (int)Global.FiledStatus.Del);
            }
            if (!ht.ContainsKey("@status"))
            {
                ht.Add("@status", (int)Global.MediaAuditStatus.FirstLevelAudit);
            }
            if (!ht.ContainsKey("@userid"))
            {
                ht.Add("@userid", userid);
            }
            sql = string.Format(sql, term);
            SqlParameter[] param = ESP.Media.Access.Utilities.Common.DictToSqlParam(ht);
            return ESP.Media.Access.Utilities.clsSelect.QueryBySql(sql, param);
        }

    }

  
}
