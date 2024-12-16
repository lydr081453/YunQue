using System;
using System.Collections.Generic;
using System.Text;
using ESP.Compatible;
using System.Data;
using System.Data.SqlClient;
using System.Collections;

using ESP.Media.Access.Utilities;
using ESP.Media.Access;
using ESP.Media.Entity;
namespace ESP.Media.BusinessLogic
{
    public class IndustriesManager
    {
        public static IndustriesInfo GetModel(int id)
        {
            IndustriesInfo indust = null;
            indust = ESP.Media.DataAccess.IndustriesDataProvider.Load(id);
            if (indust == null) indust = new ESP.Media.Entity.IndustriesInfo();
            return indust;
        }

        public static bool Del(int id)
        {
            return ESP.Media.DataAccess.IndustriesDataProvider.DeleteInfo(id);
        }

        public static int add(IndustriesInfo c, Employee emp)
        {
            using (SqlConnection conn = new SqlConnection(ESP.Media.Access.Utilities.clsConfigOperate.CustomerSqlConnection()))
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();
                try
                {
                    int id = add(c, trans, emp);
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
        public static int add(IndustriesInfo c, SqlTransaction trans, Employee emp)
        {
            int id = 0;
            id = ESP.Media.DataAccess.IndustriesDataProvider.insertinfo(c, trans);
            return id;
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="p">p</param>
        /// <param name="emp">当前登录人</param>
        /// <returns></returns>
        public static bool modify(IndustriesInfo c, Employee emp)
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
        public static bool modify(IndustriesInfo c, SqlTransaction trans, Employee emp)
        {
            bool result = false;
            result = ESP.Media.DataAccess.IndustriesDataProvider.updateInfo(trans, null, c, null, null);
            return result;
        }


        /// <summary>
        /// 获取列表
        /// </summary>
        /// <returns></returns>
        public static DataTable GetAllList()
        {
            return GetList(null,null);
        }

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <returns></returns>
        public static DataTable GetList(string term,Hashtable ht)
        {
            return ESP.Media.DataAccess.IndustriesDataProvider.QueryInfo(term, ht);
        }


    }
}
