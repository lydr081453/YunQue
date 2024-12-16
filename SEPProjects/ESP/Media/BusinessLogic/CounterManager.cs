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
    public class CounterManager
    {
        public static CounterInfo getModel(int userID)
        {
            if (userID <= 0)
            {
                return new ESP.Media.Entity.CounterInfo();
             }

            return ESP.Media.DataAccess.CounterDataProvider.Load(userID);
        }
        /// <summary>
        /// Adds the specified c.
        /// </summary>
        /// <param name="c">The c.</param>
        /// <param name="emp">The emp.</param>
        /// <returns></returns>
        public static int add(CounterInfo c, int userid)
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

        /// <summary>
        /// Adds the specified c.
        /// </summary>
        /// <param name="c">The c.</param>
        /// <param name="trans">The trans.</param>
        /// <param name="emp">The emp.</param>
        /// <returns></returns>
        public static int add(CounterInfo c, SqlTransaction trans, int userid)
        {
            int id = 0;
            if (string.IsNullOrEmpty(c.Operatedate))
            {
                c.Operatedate = DateTime.Now.ToString();
            }
            //string sql = "select * from media_counter where userid = @userid ";//如果已经有此用户记录则更新积分，没有则添加
            //SqlParameter param = new SqlParameter("@userid", SqlDbType.Int);
            //param.Value = c.Userid;
            //DataTable dt = ESP.Media.Access.Utilities.clsSelect.QueryBySql(trans, sql, param);
            //if (dt.Rows.Count > 0)
            //{
            //    c.Integralid = dt.Rows[0]["Integralid"] == DBNull.Value ? 0 : Convert.ToInt32(dt.Rows[0]["Integralid"]);
            //    c.Counts += dt.Rows[0]["Counts"] == DBNull.Value ? 0 : Convert.ToInt32(dt.Rows[0]["Counts"]);
            //    modify(c,trans, userid);
            //    id = c.Integralid;
            //}
            //else
            //{
                id = ESP.Media.DataAccess.CounterDataProvider.insertinfo(c, trans);
            //}
            return id;
        }

        /// <summary>
        /// 修改一个积分
        /// </summary>
        /// <param name="c">c</param>
        /// <param name="emp">当前登录人</param>
        /// <returns></returns>
        public static bool modify(CounterInfo c, int userid)
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
        /// 修改一个积分
        /// </summary>
        /// <param name="c">c</param>
        /// <param name="trans">trans</param>
        /// <param name="emp">当前登录人</param>
        /// <returns></returns>
        public static bool modify(CounterInfo c, SqlTransaction trans, int userid)
        {
            bool result = false;
            result = ESP.Media.DataAccess.CounterDataProvider.updateInfo(trans,c);
            return result;
        }

        /// <summary>
        /// 获得积分列表 
        /// </summary>
        /// <returns></returns>
        public static DataTable GetAllList()
        {
            return GetList(null, new Hashtable());
        }

        //public static DataTable GetList( string term, Hashtable ht)
        //{
        //    if (string.IsNullOrEmpty(term))
        //    {
        //        term = string.Empty;
        //    }
        //    term += " and 1=1 order by counts desc ";
        //    DataTable dt = ESP.Media.DataAccess.CounterDataProvider.QueryInfo(term,ht);
        //    return dt;
        //}

        public static DataTable GetList(string term, Hashtable ht)
        {
            string sql = @"select distinct a.userid as userid,a.usercode as usercode,a.username as username,
                            counts= sum(a.counts) from Media_counter as a where 1=1 {0}
                            group by a.userid,a.usercode,a.username
                            order by counts desc
                            ";
            if (string.IsNullOrEmpty(term))
            {
                term = string.Empty;
            }
            sql = string.Format(sql, term);
            DataTable dt = ESP.Media.Access.Utilities.clsSelect.QueryBySql(sql, ESP.Media.Access.Utilities.Common.DictToSqlParam(ht));
            return dt;
        }


        static bool checkRole(int[] source, int[] des)
        {
            if (source == null || source.Length == 0) return true;
            if (des == null || des.Length == 0) throw new Exception("get NotIntegralRoleID error! ");

            for (int i = 0; i < source.Length; i++)
            {
                for (int j = 0; j < des.Length; j++)
                {
                    if (source[i] == des[j])
                        return false;
                }
            }
            return true;
        }

        public static bool DeleteAll(int userid)
        {
            return ESP.Media.DataAccess.CounterDataProvider.DeleteInfo(userid);
        }

        public static int GetDays(int year,int month)
        {
            if (month <= 0 || month > 12) return 0;
            if (month == 2)
            {
                 if ((year %400) == 0 || ((year % 4)== 0 && (year%100) != 0)) 
                 {
                     return 29;
                 }
                 return 28;
            }
            else if (month == 1 || month == 3 || month == 5 || month == 7 || month == 8 || month == 10 || month == 12)
            {
                return 31;
            }
            else
            {
                return 30;
            }
        }

        static DataTable GetRoleCounts(System.Web.HttpApplicationState app, string term)
        {
           
            DataTable dtIntegral = GetList(term, null);
            if (dtIntegral == null || dtIntegral.Rows.Count == 0) return new DataTable();
            DataTable dt = dtIntegral.Clone();
            List<int> roleidList = ESP.Media.Access.Utilities.ConfigManager.NotIntegralRoleIDList;
            int[] roleIds = roleidList == null ? new int[0] : roleidList.ToArray();

            foreach (DataRow dr in dtIntegral.Rows)
            {
                int empid = dr["userid"] == DBNull.Value ? 0 : Convert.ToInt32(dr["userid"]);
                if (empid > 0)
                {
                    int[] roles = ESP.Framework.BusinessLogic.RoleManager.GetUserRoleIDs(empid);
                    //List<FrameWork.Security.Role> roles = Common.GetRoleList(app, empid);
                    if (checkRole(roles, roleIds) && dt.Rows.Count < 10)
                    {
                        dt.ImportRow(dr);
                    }
                }
            }
            return dt;
        }
        public static DataTable GetMonthCounts(System.Web.HttpApplicationState app, int year,int month)
        {
            string term = string.Format(" and a.operatedate between  '{0}-{1}-1 0:0:0' and  '{0}-{1}-{2} 23:23:59'", year, month, GetDays(year,month));
            return GetRoleCounts(app, term);
        }

        public static DataTable GetYearCounts(System.Web.HttpApplicationState app, int year)
        {
            string term = string.Format(" and a.operatedate between  '{0}-1-1 0:0:0' and  '{0}-12-31 23:23:59'", year);
            return GetRoleCounts(app, term);
        }

        public static DataTable GetSeasonCounts(System.Web.HttpApplicationState app,int year, int season)
        {
            int startmonth = 01;
            int endmonth = 3;
            switch (season)
            { 
                case 1:
                    startmonth = 1;
                    endmonth = 3;
                    break;
                case 2:
                    startmonth = 4;
                    endmonth = 6;
                    break;
                case 3:
                    startmonth = 7;
                    endmonth = 9;
                    break;
                case 4:
                    startmonth = 10;
                    endmonth = 12;
                    break;
            }

            string term = string.Format(" and a.operatedate between  '{0}-{1}-{2} 0:0:0' and  '{0}-{3}-{4} 23:23:59'", year,startmonth,1,endmonth,GetDays(year,endmonth));
            return GetRoleCounts(app, term);
        }

      


        static List<CounterInfo> GetObjectList(DataTable dt)
        {
            List<CounterInfo> counters = new List<CounterInfo>();
            if (dt == null || dt.Rows.Count == 0) return counters;
            foreach (DataRow dr in dt.Rows)
            {
               CounterInfo c = ESP.Media.DataAccess.CounterDataProvider.setObject(dr);
               counters.Add(c);
            }
            return counters;
        }

        public static List<CounterInfo> GetMonthList(System.Web.HttpApplicationState app, int year, int month)
         {
            DataTable dt = GetMonthCounts(app,year,month);
            return GetObjectList(dt);
         }
         public static List<CounterInfo> GetYearList(System.Web.HttpApplicationState app, int year)
         {
             DataTable dt = GetYearCounts(app, year);
             return GetObjectList(dt);
         }

         public static List<CounterInfo> GetSeasonList(System.Web.HttpApplicationState app, int year,int season)
         {
             DataTable dt = GetSeasonCounts(app, year,season);
             return GetObjectList(dt);
         }
    }
       
}
