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
    public class MediaindustryrelationManager
    {
        /// <summary>
        /// 插入行业属性
        /// </summary>
        /// <param name="industries"></param>
        /// <param name="trans"></param>
        /// <returns></returns>
        public static int AddforAddMediaItem(int itemid,MediaindustryrelationInfo[] industries, SqlTransaction trans)
        {
            if (itemid <= 0) return -1;//媒体id错误
            if (industries == null || industries.Length <= 0) return -2;//没有行业信息
            int id = 0;
            for (int i = 0; i < industries.Length; i++)
            {
                industries[i].Del = (int)Global.FiledStatus.Usable;
                industries[i].Mediaitemid = itemid;
                id = add(industries[i], trans);
                if (id <= 0)
                {
                    return -3;//插入失败
                }
            }
            return 1;//成功
        }

        public static int add(MediaindustryrelationInfo r)
        {
            using (SqlConnection conn = new SqlConnection(ESP.Media.Access.Utilities.clsConfigOperate.CustomerSqlConnection()))
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();
                try
                {
                    int id = add(r, trans);
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

        public static int add(MediaindustryrelationInfo r, SqlTransaction trans)
        {
            int id = 0;
            id = ESP.Media.DataAccess.MediaindustryrelationDataProvider.insertinfo(r, trans);
            return id;
        }

        /// <summary>
        /// 更新行业属性
        /// </summary>
        /// <param name="industries"></param>
        /// <param name="trans"></param>
        /// <returns></returns>
        public static int modifyforAddMediaItem(int itemid, MediaindustryrelationInfo[] industries, SqlTransaction trans)
        {
            if (itemid <= 0) return -1;//媒体id错误
            
            int id = 0;

            DelByMediaItem(itemid, trans);
            if (industries == null || industries.Length <= 0) return 1;//没有行业信息
            for (int i = 0; i < industries.Length; i++)
            {
                industries[i].Del = (int)Global.FiledStatus.Usable;
                id = add(industries[i], trans);
                if (id <= 0)
                {
                    return -3;//插入失败
                }
            }
            return 1;//成功
        }

        /// <summary>
        /// 删除行业属性(置位)
        /// </summary>
        /// <param name="industries"></param>
        /// <param name="trans"></param>
        /// <returns></returns>
        public static int modifyforDelMediaItem(int itemid, SqlTransaction trans)
        {

            if (itemid <= 0) return -1;//媒体id错误
            DataTable dt = getAllListByMediaid(itemid);
            if (dt == null || dt.Rows.Count <= 0) return 0;
            int count = 0;
            for (int i = 0; i < dt.Rows.Count; i++)
            { 
                MediaindustryrelationInfo industry =  ESP.Media.DataAccess.MediaindustryrelationDataProvider.Load(Convert.ToInt32( dt.Rows[i]["Id"]));
                industry.Industryid =  dt.Rows[i]["Industryid"] == DBNull.Value ? 0 :Convert.ToInt32(dt.Rows[i]["Industryid"] );
                industry.Del = (int)Global.FiledStatus.Del;
                if (ESP.Media.DataAccess.MediaindustryrelationDataProvider.updateInfo(trans,null, industry, null, null))
                {
                    count++;
                }
            }
            return count;
        }

        public static int DelByMediaItem(int itemid, SqlTransaction trans)
        {
            int rows = 0;
            string sql = "delete Media_mediaindustryrelation where Mediaitemid=@Mediaitemid";
            SqlParameter param = new SqlParameter("@Mediaitemid", SqlDbType.Int);
            param.Value = itemid;
            rows = ESP.Media.Access.Utilities.SqlHelper.ExecuteNonQuery(trans, CommandType.Text, sql, param);
            return rows;
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="r">The r.</param>
        /// <param name="emp">当前登录人</param>
        /// <returns></returns>
        public static bool modify(MediaindustryrelationInfo r, Employee emp)
        {
            using (SqlConnection conn = new SqlConnection(ESP.Media.Access.Utilities.clsConfigOperate.CustomerSqlConnection()))
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();
                try
                {
                    bool result = modify(r, trans, emp);
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
        public static bool modify(MediaindustryrelationInfo r, SqlTransaction trans, Employee emp)
        {
            bool result = false;
            result = ESP.Media.DataAccess.MediaindustryrelationDataProvider.updateInfo(trans, null, r, null, null);
            return result;
        }


        /// <summary>
        /// 获取列表
        /// </summary>
        /// <returns></returns>
        public static DataTable getAllList()
        {
            return ESP.Media.DataAccess.MediaindustryrelationDataProvider.QueryInfo(null, new Hashtable());
        }

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <returns></returns>
        //public static DataTable getAllListByMedia(int mediaitemid)
        //{
        //    string term = "Mediaitemid=@Mediaitemid";
        //    return ESP.Media.DataAccess.MediaindustryrelationDataProvider.QueryInfo(term, new SqlParameter("@Mediaitemid",mediaitemid));
        //}


        public static DataTable getAllListByMediaid(int mediaitemid)
        {
            string sql = @"
            select a.id as id,a.industryid as industryid,a.mediaitemid as mediaitemid,
            a.id as relationid,a.del as del,b.industryname as industryname
            from media_mediaindustryrelation as a 
            inner join media_industries as b on a.industryid = b.industryid
            where a.Mediaitemid=@Mediaitemid and a.del!=@relationdel and b.del !=@industrydel order by a.id desc";
            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@Mediaitemid", SqlDbType.Int);
            param[0].Value = mediaitemid;
            param[1] = new SqlParameter("@relationdel", SqlDbType.Int);
            param[1].Value = (int)Global.FiledStatus.Del;
            param[2] = new SqlParameter("@industrydel", SqlDbType.Int);
            param[2].Value = (int)Global.FiledStatus.Del;
            return ESP.Media.Access.Utilities.clsSelect.QueryBySql(sql, param);
        }


        public static MediaindustryrelationInfo[] GetAllIndustryByMedia(int mediaitemid)
        {
            DataTable dt = getAllListByMediaid(mediaitemid);
            if (dt == null || dt.Rows.Count <= 0) return null;
            MediaindustryrelationInfo [] industries = new MediaindustryrelationInfo[dt.Rows.Count];
            for (int i = 0; i < industries.Length; i++)
            {
                industries[i] = ESP.Media.DataAccess.MediaindustryrelationDataProvider.Load(Convert.ToInt32(dt.Rows[i]["id"]));
            }
            return industries;
        }

    }
}
