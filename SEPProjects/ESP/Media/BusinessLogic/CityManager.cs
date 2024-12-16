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
    public class CityManager
    {
        /// <summary>
        /// Gets the model.
        /// </summary>
        /// <param name="cityid">The cityid.</param>
        /// <returns></returns>
        public static CityInfo GetModel(int cityid)
        {
            if (cityid <= 0)
            {
                CityInfo city = new ESP.Media.Entity.CityInfo();
                city.City_name = string.Empty;
                city.City_level = "0";
                city.City_id =0;
                city.Province_id = 0;
                return city;
            }
            return ESP.Media.DataAccess.CityDataProvider.Load(cityid);
        }

        public static DataTable GetModelsByName(string cityname)
        {
            Hashtable ht = new Hashtable();
            ht.Add("@City_name",cityname);
            return ESP.Media.DataAccess.CityDataProvider.QueryInfo(" and City_name=@City_name", ht);
        }

        /// <summary>
        /// Adds the specified c.
        /// </summary>
        /// <param name="c">The c.</param>
        /// <param name="emp">The emp.</param>
        /// <returns></returns>
        public static int add(CityInfo c, Employee emp)
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
        public static int add(CityInfo c, SqlTransaction trans, Employee emp)
        {
            int id = 0;
            id = ESP.Media.DataAccess.CityDataProvider.insertinfo(c, trans);
            return id;
        }

        /// <summary>
        /// 修改一个省
        /// </summary>
        /// <param name="p">p</param>
        /// <param name="emp">当前登录人</param>
        /// <returns></returns>
        public static bool modify(CityInfo c, Employee emp)
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
        /// 修改一个市
        /// </summary>
        /// <param name="a">a</param>
        /// <param name="trans">trans</param>
        /// <param name="emp">当前登录人</param>
        /// <returns></returns>
        public static bool modify(CityInfo c, SqlTransaction trans, Employee emp)
        {
            bool result = false;
            result = ESP.Media.DataAccess.CityDataProvider.updateInfo(trans, null, c, null, null);
            return result;
        }

        /// <summary>
        /// 根据国家得到省份信息
        /// </summary>
        /// <param name="provinceid">The provinceid.</param>
        /// <returns></returns>
        public static DataTable getAllListByProvince(int provinceid)
        {
            string term = "Province_id=@Provinceid";
            SqlParameter param = new SqlParameter("@Provinceid",SqlDbType.Int);
            param.Value = provinceid;
            return ESP.Media.DataAccess.CityDataProvider.QueryInfo(term,param);
        }

        /// <summary>
        /// Gets all list by province A.
        /// </summary>
        /// <param name="provinceid">The provinceid.</param>
        /// <returns></returns>
        [AjaxPro.AjaxMethod]
        public static List<List<string>> getAllListByProvinceA(int provinceid)
        {
            List<List<string>> list = new List<List<string>>();

            List<string> c = new List<string>();
            c.Add("0");
            c.Add("请选择");
            list.Add(c);

            string term = "Province_id=@Provinceid";
            SqlParameter param = new SqlParameter("@Provinceid", SqlDbType.Int);
            param.Value = provinceid;
            DataTable dt = ESP.Media.DataAccess.CityDataProvider.QueryInfo(term, param);
            if(dt != null && dt.Rows.Count> 0)
            {
                for(int i = 0; i<dt.Rows.Count;i++)
                {
                    List<string> s = new List<string>();
                    s.Add(dt.Rows[i]["city_id"].ToString());
                    s.Add(dt.Rows[i]["city_name"].ToString());
                    list.Add(s);
                }
            }
            return list;
        }

        ///// <summary>
        ///// Gets all list.
        ///// </summary>
        ///// <returns></returns>
        //public static DataTable getAllList()
        //{
        //    string term = " and city_name not like '%其它%'";
        //    return ESP.Media.DataAccess.CityDataProvider.QueryInfo(term, new Hashtable());
        //}
    }
}
