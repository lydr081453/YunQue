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
    public class PrivateinfoManager
    {

        public static Privateinfo GetModel(int id)
        {
            Privateinfo info = null;
            info = ESP.Media.DataAccess.PrivateinfoDataProvider.Load(id);
            if (info == null) info = new ESP.Media.Entity.Privateinfo();
            return info;
        }


        public static int add(Privateinfo c, int userid)
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
        public static int add(Privateinfo c, SqlTransaction trans, int userid)
        {
            int id = 0;
            id = ESP.Media.DataAccess.PrivateinfoDataProvider.insertinfo(c, trans);
            return id;
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="p">p</param>
        /// <param name="emp">当前登录人</param>
        /// <returns></returns>
        public static bool modify(Privateinfo c, int userid)
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
        public static bool modify(Privateinfo c, SqlTransaction trans, int userid)
        {
            bool result = false;
            result = ESP.Media.DataAccess.PrivateinfoDataProvider.updateInfo(trans, null, c, null, null);
            return result;
        }


        /// <summary>
        /// 获取列表
        /// </summary>
        /// <returns></returns>
        public static DataTable getAllList()
        {
            return ESP.Media.DataAccess.PrivateinfoDataProvider.QueryInfo(null, new Hashtable());
        }

        /// <summary>
        /// 获得记者的私密信息列表
        /// </summary>
        /// <param name="reportId">记者ID</param>
        /// <returns></returns>
        public static DataTable getListByReportId(int reportId)
        {
            string terms = " ReporterID=@ReporterID";
            Hashtable ht = new Hashtable();
            ht.Add("@ReporterID", reportId);

            return ESP.Media.DataAccess.PrivateinfoDataProvider.QueryInfo(terms, ht);
        }

    }
}
