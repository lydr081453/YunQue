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
    public class CountryManager
    {
        /// <summary>
        /// Gets the model.
        /// </summary>
        /// <param name="countryid">The countryid.</param>
        /// <returns></returns>
        public static CountryInfo GetModel(int countryid)
        {
            if (countryid <= 0)
            {
                return new ESP.Media.Entity.CountryInfo();
            }
            return ESP.Media.DataAccess.CountryDataProvider.Load(countryid);
        }

        /// <summary>
        /// Adds the specified c.
        /// </summary>
        /// <param name="c">The c.</param>
        /// <param name="emp">The emp.</param>
        /// <returns></returns>
        public static int add(CountryInfo c, Employee emp)
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

        /// <summary>
        /// Adds the specified c.
        /// </summary>
        /// <param name="c">The c.</param>
        /// <param name="trans">The trans.</param>
        /// <param name="emp">The emp.</param>
        /// <returns></returns>
        public static int add(CountryInfo c, SqlTransaction trans, Employee emp)
        {
            int id = 0;
            id = ESP.Media.DataAccess.CountryDataProvider.insertinfo(c, trans);
            return id;
        }

        /// <summary>
        /// 修改一个国家
        /// </summary>
        /// <param name="c">c</param>
        /// <param name="emp">当前登录人</param>
        /// <returns></returns>
        public static bool modify(CountryInfo c, Employee emp)
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
        /// 修改一个国家
        /// </summary>
        /// <param name="c">c</param>
        /// <param name="trans">trans</param>
        /// <param name="emp">当前登录人</param>
        /// <returns></returns>
        public static bool modify(CountryInfo c, SqlTransaction trans, Employee emp)
        {
            bool result = false;
            result = ESP.Media.DataAccess.CountryDataProvider.updateInfo(trans, null, c, null, null);
            return result;
        }

        /// <summary>
        /// 获得国家列表 
        /// </summary>
        /// <returns></returns>
        public static DataTable getAllList()
        {
            return ESP.Media.DataAccess.CountryDataProvider.QueryInfo(null, new Hashtable());
        }

        /// <summary>
        /// Gets the list by region attribute ID.
        /// </summary>
        /// <param name="regionattributeid">The regionattributeid.</param>
        /// <returns></returns>
        public static DataTable getListByRegionAttributeID(int regionattributeid)
        {
            SqlParameter param = new SqlParameter("@regionattributeid", SqlDbType.Int);
            param.Value = regionattributeid;
            return ESP.Media.DataAccess.CountryDataProvider.QueryInfo(" and regionattributeid=@regionattributeid", param);
        }

        /// <summary>
        /// Gets the list by region attribute IDA.
        /// </summary>
        /// <param name="regionattributeid">The regionattributeid.</param>
        /// <returns></returns>
        [AjaxPro.AjaxMethod]
        public static List<List<string>> getListByRegionAttributeIDA(int regionattributeid)
        {
            List<List<string>> list = new List<List<string>>();
            List<string> c = new List<string>();
            c.Add("0");
            c.Add("请选择");
            list.Add(c);
            SqlParameter param = new SqlParameter("@regionattributeid", SqlDbType.Int);
            param.Value = regionattributeid;
            DataTable dt = ESP.Media.DataAccess.CountryDataProvider.QueryInfo(" and regionattributeid=@regionattributeid", param);
            if (dt != null && dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    List<string> s = new List<string>();
                    s.Add(dt.Rows[i]["countryid"].ToString());
                    s.Add(dt.Rows[i]["countryname"].ToString());
                    list.Add(s);
                }
            }
            return list;
        }
    }
}
