
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
    public class MeetingsManager
    {
        public static MeetingsInfo GetModel(int id)
        {
            MeetingsInfo indust = null;
            indust = ESP.Media.DataAccess.MeetingsDataProvider.Load(id);
            if (indust == null) indust = new ESP.Media.Entity.MeetingsInfo();
            return indust;
        }

        public static int add(MeetingsInfo meeting, string filename, byte[] filedata, int userid, out string errmsg)
        {
 if (filedata != null && filename != string.Empty)
                {
                   meeting.Path = CommonManager.SaveFile(ESP.Media.Access.Utilities.ConfigManager.MediaMeetings, filename, filedata, true);
                }
         
            using (SqlConnection conn = new SqlConnection(ESP.Media.Access.Utilities.clsConfigOperate.CustomerSqlConnection()))
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();
                try
                {
                    int id = add(meeting, trans, userid);
                    trans.Commit();
                    errmsg = "成功";
                    return id;
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    errmsg = "失败!";
                    return -1;
                }
                finally
                {
                    conn.Close();
                }
            }

        }

        public static int add(MeetingsInfo c, int userid)
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
        public static int add(MeetingsInfo c, SqlTransaction trans, int userid)
        {
            int id = 0;
            id = ESP.Media.DataAccess.MeetingsDataProvider.insertinfo(c, trans);
            return id;
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="p">p</param>
        /// <param name="emp">当前登录人</param>
        /// <returns></returns>
        public static bool modify(MeetingsInfo c, int userid)
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
        public static bool modify(MeetingsInfo c, SqlTransaction trans, int userid)
        {
            bool result = false;
            result = ESP.Media.DataAccess.MeetingsDataProvider.updateInfo(trans, null, c, null, null);
            return result;
        }

        public static bool delete(int meeingid)
        {
            return ESP.Media.DataAccess.MeetingsDataProvider.DeleteInfo(meeingid);
        }


        /// <summary>
        /// 获取列表
        /// </summary>
        /// <returns></returns>
        public static DataTable GetAllList()
        {
            return ESP.Media.DataAccess.MeetingsDataProvider.QueryInfo(" and 1=1 order by cycle desc", new Hashtable());
        }

        public static MeetingsInfo GetNew()
        {
            string sql = "select top 1 * from media_meetings order by cycle desc";
            DataTable dt = ESP.Media.Access.Utilities.clsSelect.QueryBySql(sql);
            if (dt == null || dt.Rows.Count == 0) return null;
            int id = dt.Rows[0]["id"] == DBNull.Value ? 0 : Convert.ToInt32(dt.Rows[0]["id"]);
            return ESP.Media.DataAccess.MeetingsDataProvider.Load(id);
        }
    }
}
