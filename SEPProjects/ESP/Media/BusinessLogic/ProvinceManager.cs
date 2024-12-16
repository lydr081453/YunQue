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
    public class ProvinceManager
    {
        /// <summary>
        /// Gets the model.
        /// </summary>
        /// <param name="provinceid">The provinceid.</param>
        /// <returns></returns>
        public static ProvinceInfo GetModel(int provinceid)
        {
            if (provinceid <= 0)
                return new ESP.Media.Entity.ProvinceInfo();
            return ESP.Media.DataAccess.ProvinceDataProvider.Load(provinceid);
        }

        /// <summary>
        /// Adds the specified p.
        /// </summary>
        /// <param name="p">The p.</param>
        /// <param name="emp">The emp.</param>
        /// <returns></returns>
        public static int add(ProvinceInfo p, Employee emp)
        {
            using (SqlConnection conn = new SqlConnection(ESP.Media.Access.Utilities.clsConfigOperate.CustomerSqlConnection()))
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();
                try
                {
                    int id = add(p, trans, emp);
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
        /// Adds the specified p.
        /// </summary>
        /// <param name="p">The p.</param>
        /// <param name="trans">The trans.</param>
        /// <param name="emp">The emp.</param>
        /// <returns></returns>
        public static int add(ProvinceInfo p, SqlTransaction trans, Employee emp)
        {
            int id = 0;
            id = ESP.Media.DataAccess.ProvinceDataProvider.insertinfo(p, trans);
            return id;
        }

        /// <summary>
        /// 修改一个省
        /// </summary>
        /// <param name="p">p</param>
        /// <param name="emp">当前登录人</param>
        /// <returns></returns>
        public static bool modify(ProvinceInfo p, Employee emp)
        {
            using (SqlConnection conn = new SqlConnection(ESP.Media.Access.Utilities.clsConfigOperate.CustomerSqlConnection()))
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();
                try
                {
                    bool result = modify(p, trans, emp);
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
        /// 修改一个省
        /// </summary>
        /// <param name="a">a</param>
        /// <param name="trans">trans</param>
        /// <param name="emp">当前登录人</param>
        /// <returns></returns>
        public static bool modify(ProvinceInfo p, SqlTransaction trans, Employee emp)
        {
            bool result = false;
            result = ESP.Media.DataAccess.ProvinceDataProvider.updateInfo(trans, null, p, null, null);
            return result;
        }

        /// <summary>
        /// 根据国家得到省份信息
        /// </summary>
        /// <param name="countryid"></param>
        /// <returns></returns>
        public static DataTable getAllListByCountry(int countryid)
        {
            string term = "country_id=@countryid";
            SqlParameter param = new SqlParameter("@countryid", SqlDbType.Int);
            param.Value = countryid;
            return ESP.Media.DataAccess.ProvinceDataProvider.QueryInfo(term,param);
        }

        /// <summary>
        /// Gets all list by country A.
        /// </summary>
        /// <param name="countryid">The countryid.</param>
        /// <returns></returns>
        [AjaxPro.AjaxMethod]
        public static List<List<string>> getAllListByCountryA()
        {
            int countryid = Global.DefaultCountry;

            List<List<string>> list = new List<List<string>>();

            List<string> c = new List<string>();
            c.Add("0");
            c.Add("请选择");
            list.Add(c);

            string term = "country_id=@countryid";
            SqlParameter param = new SqlParameter("@countryid", SqlDbType.Int);
            param.Value = countryid;
            DataTable dt = ESP.Media.DataAccess.ProvinceDataProvider.QueryInfo(term, param);
            if (dt != null && dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    List<string> s = new List<string>();
                    s.Add(dt.Rows[i]["province_id"].ToString());
                    s.Add(dt.Rows[i]["province_name"].ToString());
                    list.Add(s);
                }
            }
            return list;
        }
    }
}
