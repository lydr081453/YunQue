using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Data.SqlClient;
using System.Data;
using ESP.Media.Access;
using ESP.Media.Entity;
using ESP.Media.Access.Utilities;

namespace ESP.Media.BusinessLogic
{
    /// <summary>
    /// 媒体分部
    /// </summary>
    public class BranchManager
    {
        public static int add(BranchInfo branch, int userid, SqlTransaction trans)
        {
            return ESP.Media.DataAccess.BranchDataProvider.insertinfo(branch, trans);
        }

        public static int add(BranchInfo branch, int userid)
        {
            using (SqlConnection conn = new SqlConnection(ESP.Media.Access.Utilities.clsConfigOperate.CustomerSqlConnection()))
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();
                try
                {
                    int id = ESP.Media.DataAccess.BranchDataProvider.insertinfo(branch, trans);
                    trans.Commit();
                    conn.Close();
                    return id;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    trans.Rollback();
                    conn.Close();
                    return -1;
                }
            }
        }


        public static bool DeleteInfoByMedia(int mediaItemID)
        {
            using (SqlConnection conn = new SqlConnection(ESP.Media.Access.Utilities.clsConfigOperate.CustomerSqlConnection()))
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();
                try
                {
                    if (DeleteBranchByMediaItemID(mediaItemID, trans))
                    {
                        trans.Commit();
                        return true;
                    }
                    else
                    {
                        trans.Rollback();
                    }
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
            return false;
        }

        public static bool DeleteBranchByMediaItemID(int mediaItemID, SqlTransaction trans)
        {
            int rows = 0;
            string sql = "delete Media_branch where mediaitemid=@mediaitemid";
            SqlParameter param = new SqlParameter("@mediaitemid", SqlDbType.Int);
            param.Value = mediaItemID;
            rows = ESP.Media.Access.Utilities.SqlHelper.ExecuteNonQuery(trans, CommandType.Text, sql, param);
            if (rows > 0)
            {
                return true;
            }
            return false;
        }

        public static bool delete(int mediaItemID)
        {
            using (SqlConnection conn = new SqlConnection(ESP.Media.Access.Utilities.clsConfigOperate.CustomerSqlConnection()))
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();
                try
                {
                    bool res = DeleteInfoByMedia(mediaItemID);
                    trans.Commit();
                    conn.Close();
                    return res;
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

        public static DataTable GetListByMediaItemID(int mediaItemID)
        {
            using (SqlConnection conn = new SqlConnection(ESP.Media.Access.Utilities.clsConfigOperate.CustomerSqlConnection()))
            {
                try
                {
                    return GetInfosByMediaItemID(mediaItemID);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return null;
                }
            }
        }

        public static DataTable GetInfosByMediaItemID(int mediaItemID)
        {
            DataTable dt = null;
            string sql = "select id,cityid,cityname from Media_Branch where mediaitemid =" + mediaItemID.ToString();
            dt = ESP.Media.Access.Utilities.clsSelect.QueryBySql(sql);
            return dt;
        }
    }
}