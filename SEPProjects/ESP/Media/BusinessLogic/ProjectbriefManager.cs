
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
    public class ProjectbriefManager
    {
        public static ProjectbriefInfo GetModel(int id)
        {
            if (id <= 0)
            {

                return new ESP.Media.Entity.ProjectbriefInfo() ;
            }
            return ESP.Media.DataAccess.ProjectbriefDataProvider.Load(id);
        }

        public static int add(ProjectbriefInfo c, int userid)
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
        public static int add(ProjectbriefInfo c, SqlTransaction trans,int userid)
        {
            int id = 0;
            if (c.FileData != null && c.Filepath != string.Empty)
            {
                c.Filepath = CommonManager.SaveFile(ESP.Media.Access.Utilities.ConfigManager.ProjectBriefPath, c.Filepath, c.FileData, true);
            }
            id = ESP.Media.DataAccess.ProjectbriefDataProvider.insertinfo(c, trans);
            return id;
        }

        /// <summary>
        /// 修改一个省
        /// </summary>
        /// <param name="p">p</param>
        /// <param name="emp">当前登录人</param>
        /// <returns></returns>
        public static bool modify(ProjectbriefInfo c, int userid)
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
        /// 修改一个市
        /// </summary>
        /// <param name="a">a</param>
        /// <param name="trans">trans</param>
        /// <param name="emp">当前登录人</param>
        /// <returns></returns>
        public static bool modify(ProjectbriefInfo c, SqlTransaction trans, int userid)
        {
            bool result = false;
            if (c.FileData != null && c.Filepath != string.Empty)
            {
                c.Filepath = CommonManager.SaveFile(ESP.Media.Access.Utilities.ConfigManager.ProjectBriefPath, c.Filepath, c.FileData, true);
            }
            result = ESP.Media.DataAccess.ProjectbriefDataProvider.updateInfo(trans, null, c, null, null);
            return result;
        }

        /// <summary>
        /// 根据支付项ID取得列表,取得支付项的剪报
        /// </summary>
        /// <param name="paymentid"></param>
        /// <param name="term"></param>
        /// <param name="ht"></param>
        /// <returns></returns>
        public static DataTable GetListByPayID(int paymentid, string term, Hashtable ht)
        {
            string sql = @"select payment.projectcode as projectcode,
                            medianame = media.mediacname + ' '+media.ChannelName+' '+media.TopicName,
                            reporter.ReporterName as linkmanname,
                            brief.briefsubject as briefsubject,
                            brief.issuedate as issuedate,
                            brief.wordsaccount as wordsaccount,
                            '' as recvmanname,
                        city.city_name as cityname,
                            reporter.CardNumber as CardNumber,
                            reporter.UsualMobile as UsualMobile,
                            brief.linkurl as linkurl,
                            brief.filepath as filepath,
                            brief.des as des,brief.id as id
                            from Media_ProjectBrief as brief
                            inner join Media_writingfeeitem as payment  on brief.paymentid = payment.writingfeeitemid
                            inner join media_reporters as reporter on payment.linkmanid = reporter.reporterid
                            inner join media_mediaitems as media on reporter.media_id = media.mediaitemid 
                            left join media_city as city on reporter.cityid = city.city_id                        
                            where  media.del != @del and reporter.del != @del
                            and brief.paymentid =  @paymentid
                            {0} order by brief.id desc
                            ";
            if (term == null) term = string.Empty;
            if (ht == null) ht = new Hashtable();

            ht.Add("@del", (int)Global.FiledStatus.Del);
            ht.Add("@paymentid", paymentid);

            sql = string.Format(sql, term);
            SqlParameter[] param = ESP.Media.Access.Utilities.Common.DictToSqlParam(ht);
            return ESP.Media.Access.Utilities.clsSelect.QueryBySql(sql, param);
        }

        public static DataTable GetListByProjectID(int projectid, string term, Hashtable ht)
        {
            string sql = @"select payment.projectcode as projectcode,
                            medianame = media.mediacname + ' '+media.ChannelName+' '+media.TopicName,
                            reporter.ReporterName as linkmanname,
                            brief.briefsubject as briefsubject,
                            brief.issuedate as issuedate,
                            brief.wordsaccount as wordsaccount,
                            '' as recvmanname,
                        city.city_name as cityname,
                            reporter.CardNumber as CardNumber,
                            reporter.UsualMobile as UsualMobile,
                            brief.linkurl as linkurl,
                            brief.filepath as filepath,
                            brief.des as des,brief.id as id
                            from Media_ProjectBrief as brief
                            inner join Media_writingfeeitem as payment  on brief.paymentid = payment.writingfeeitemid
                            inner join media_reporters as reporter on payment.linkmanid = reporter.reporterid
                            inner join media_mediaitems as media on reporter.media_id = media.mediaitemid 
                            left join media_city as city on reporter.cityid = city.city_id                        
                            where  media.del != @del and reporter.del != @del
                            and payment.projectid =  @projectid
                            {0} order by brief.id desc
                            ";
            if (term == null) term = string.Empty;
            if (ht == null) ht = new Hashtable();

            ht.Add("@del", (int)Global.FiledStatus.Del);
            ht.Add("@projectid", projectid);

            sql = string.Format(sql, term);
            SqlParameter[] param = ESP.Media.Access.Utilities.Common.DictToSqlParam(ht);
            return ESP.Media.Access.Utilities.clsSelect.QueryBySql(sql, param);
        }
    }
}
