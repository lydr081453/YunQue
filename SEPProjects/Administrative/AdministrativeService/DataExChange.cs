using System;
using System.Collections.Generic;
using System.Threading;
using System.Text;
using ESP.Administrative.BusinessLogic;
using ESP.Administrative.Common;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Transactions;

using MySql.Data.MySqlClient;

namespace AdministrativeService
{
    /// <summary>
    /// 数据导入操作类
    /// </summary>
    public class DataExChange
    {
        #region 局部变量定义
        /// <summary>
        /// 导入数据的最大ID值
        /// </summary>
        public static int mastIdValue = 0;

        public static int mastIdValue2 = 0;

        public static int GoldenBlockChainMastIdValue = 0;

        public static int ViveMastIdValue = 0;

        public static int DigitalMastIdValue = 0;

        public static int DigitalMastIdValue2 = 0;

        public static int XingYanMastIdValue = 0;
        /// <summary>
        /// 日志记录对象
        /// </summary>
        private LogManager logger = new LogManager();
        /// <summary>
        /// 出勤业务类
        /// </summary>
        private Check attendanceBuss = new Check();
        /// <summary>
        /// 下班打卡有效时间点
        /// </summary>
        //public static string offWorkPointTime = ConfigurationManager.AppSettings["OffWorkPointTime"];
        /// <summary>
        /// 考勤记录的时间
        /// </summary>
        public static DateTime attendanceDateTime = DateTime.Now;
        /// <summary>
        /// 下班时间更新表示，0表示没有更新前一天下班时间，1表示已经更新了
        /// </summary>
        private static int count = 0;
        #endregion

        #region 导入考勤表
        /// <summary>
        /// 开始启动数据导入
        /// </summary>
        public void StartDateExChange()
        {
            try
            {
                DateTime temptime = DateTime.Now;
                if (attendanceDateTime.Date < temptime.Date)
                {
                    count = 0;
                }
                attendanceDateTime = temptime;

                //北京新打卡系统
                GetBJDateInfoSource2();
                Thread.Sleep(10000);

                //北京云目新打卡系统
                GetDigitalDateInfoSource2();
                Thread.Sleep(10000);

                //金色区块
                GetGoldenBlockChainDateInfo();
                Thread.Sleep(10000);

                //星言
                GetXingYanDateInfo();
                Thread.Sleep(10000);
               
            }
            catch (Exception ex)
            {
                // logger.Add("AdministrativeService：" + ex.Message + ex.StackTrace);
                // 获得线程休息的时间间隔
                int sleeptime = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["sleeptime"]);
                Thread.Sleep(sleeptime);
                StartDateExChange();
            }
        }

        public void GetXingYanDateInfo()
        {
            string M_str_sqlcon = System.Configuration.ConfigurationManager.ConnectionStrings["connsource2"].ConnectionString; ;
            DataSet myds = new DataSet();
            MySqlConnection mycon = new MySqlConnection();
            mycon.ConnectionString = M_str_sqlcon;
            try
            {

                SetXingYanMastIdValue();

                string shortEN = ConfigurationManager.AppSettings["XingyanShortEn"];

                string sql = "select eventid as f_cardrecordid,cardno as f_Cardno, eventtime as readDateTime,1 as f_readdate, "
                + " employeeNo as f_workno from event where eventid >" + XingYanMastIdValue + " and cardno<>'' and employeeNo like '" + shortEN + "%'"
                + " union "
                + " select eventid as f_cardrecordid,cardno as f_Cardno, eventtime as readDateTime,0 as f_readdate, "
                + " employeeNo as f_workno from event"
                + " where eventid>" + XingYanMastIdValue + " and cardno<>'' and employeeNo like '" + shortEN + "%' order by f_cardrecordid;";
                // 连接打卡服务器
                mycon.Open();
                MySqlDataAdapter mda = new MySqlDataAdapter(sql, mycon);

                mda.Fill(myds, "table1");

            }
            catch (Exception ex)
            {
                logger.Add("XingyanService：" + ex.Message + ex.StackTrace);
                throw new Exception(ex.Message);
            }
            finally
            {
                mycon.Close();
            }
            // 将数据插入到内部系统的数据库中
            SaveXingYanDateInfo(myds);
        }


        /// <summary>
        /// 获取最小的进门时间，按门卡号进行分组
        /// </summary>
        public void GetDateInfo()
        {
            //logger.Add("AdministrativeService：获取最小的进门时间");
            DataSet ds = new DataSet();
            SqlConnection conn = new SqlConnection();
            try
            {

                    SetMastIdValue();
           
                int hour = int.Parse(ConfigurationManager.AppSettings["OffWorkPointTime2"]);
                string sql = "select max(f_cardrecordid) as f_cardrecordid, f_Cardno, min(f_readdate) as readDateTime, 1 as f_readdate, f_workno from v_d_cardrecord"
                             + " where f_CardRecordID > " + mastIdValue + " and f_CardNo<>'' "
                             + " group by f_workno, f_Cardno, DATEPART(year, DATEADD(hour, - " + hour + ", f_readdate)), "
                             + " DATEPART(month, DATEADD(hour, - " + hour + ", f_readdate)), DATEPART(day, DATEADD(hour, - " + hour + ", f_readdate))"
                             + " union"
                             + " select max(f_cardrecordid) as f_cardrecordid, f_Cardno, max(f_readdate), 0 as f_readdate, f_workno from v_d_cardrecord"
                             + " where f_CardRecordID > " + mastIdValue + " and f_CardNo<>'' "
                             + " group by f_workno, f_Cardno, DATEPART(year, DATEADD(hour, - " + hour + ", f_readdate)), "
                             + " DATEPART(month, DATEADD(hour, - " + hour + ", f_readdate)), DATEPART(day, DATEADD(hour, - " + hour + ", f_readdate)) "
                             + " order by f_cardrecordid asc";
                // 连接打卡服务器
                conn.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["connsource"].ConnectionString;
                SqlCommand cmd = new SqlCommand();
                SqlDataAdapter adapter = new SqlDataAdapter();
                cmd.Connection = conn;
                cmd.CommandTimeout = 180;
                cmd.CommandText = sql;

                conn.Open();
                adapter.SelectCommand = cmd;
                ds.Tables.Clear();
                // 读取打卡记录信息
                adapter.Fill(ds);
            }
            catch (Exception ex)
            {
                //logger.Add("AdministrativeService：" + ex.Message + ex.StackTrace);
                throw new Exception(ex.Message);
            }
            finally
            {
                conn.Close();
            }
            // 将数据插入到内部系统的数据库中
            SaveDateInfo(ds);
        }

        public void GetBJDateInfoSource2()
        {
            string M_str_sqlcon = System.Configuration.ConfigurationManager.ConnectionStrings["connsource2"].ConnectionString; ;
            DataSet myds = new DataSet();
            MySqlConnection mycon = new MySqlConnection();
            mycon.ConnectionString = M_str_sqlcon;
            try
            {

                SetMastIdValue2();

                string sql = "select eventid as f_cardrecordid,cardno as f_Cardno, eventtime as readDateTime,1 as f_readdate, "
                +" employeeNo as f_workno from event where eventid >" + mastIdValue2 + " and cardno<>'' and employeeNo<>''"
                +" union "
                +" select eventid as f_cardrecordid,cardno as f_Cardno, eventtime as readDateTime,0 as f_readdate, "
                +" employeeNo as f_workno from event"
                +" where eventid>" + mastIdValue2 + " and cardno<>'' and employeeNo<>'' order by f_cardrecordid;";
                // 连接打卡服务器
                mycon.Open();
                MySqlDataAdapter mda = new MySqlDataAdapter(sql, mycon);
              
                mda.Fill(myds, "table1");

            }
            catch (Exception ex)
            {
                logger.Add("AdministrativeService：" + ex.Message + ex.StackTrace);
                throw new Exception(ex.Message);
            }
            finally
            {
                mycon.Close();
            }
            // 将数据插入到内部系统的数据库中
            SaveDateInfo2(myds);
        }


        /// <summary>
        /// 金色区块获取最小的进门时间，按门卡号进行分组
        /// </summary>
       
        public void GetGoldenBlockChainDateInfo()
        {
            string M_str_sqlcon = System.Configuration.ConfigurationManager.ConnectionStrings["connsource2"].ConnectionString; ;
            DataSet myds = new DataSet();
            MySqlConnection mycon = new MySqlConnection();
            mycon.ConnectionString = M_str_sqlcon;
            try
            {
                SetGoldenBlockChainMastIdValue();
                
                
                string shortEN = ConfigurationManager.AppSettings["GoldenBlockChainShortEn"];

                string sql = "select eventid as f_cardrecordid,cardno as f_Cardno, eventtime as readDateTime,1 as f_readdate, "
                + " employeeNo as f_workno from event where eventid > " + GoldenBlockChainMastIdValue + " and cardno<>'' and employeeNo like '" + shortEN + "%'"
                + " union "
                + " select eventid as f_cardrecordid,cardno as f_Cardno, eventtime as readDateTime,0 as f_readdate, "
                + " employeeNo as f_workno from event"
                + " where eventid > " + GoldenBlockChainMastIdValue + " and cardno<>'' and employeeNo like '" + shortEN + "%' order by f_cardrecordid;";

                // 连接打卡服务器
                mycon.Open();
                MySqlDataAdapter mda = new MySqlDataAdapter(sql, mycon);

                mda.Fill(myds, "table1");
            }
            catch (Exception ex)
            {
              //  logger.Add("AdministrativeService：" + ex.Message + ex.StackTrace);
                throw new Exception(ex.Message);
            }
            finally
            {
                mycon.Close();
            }
            // 将数据插入到内部系统的数据库中
            SaveGoldenBlockChainDateInfo(myds);
        }

        /// <summary>
        /// vive获取最小的进门时间，按门卡号进行分组
        /// </summary>
        public void GetViveDateInfo()
        {
            string M_str_sqlcon = System.Configuration.ConfigurationManager.ConnectionStrings["connsource2"].ConnectionString; ;
            DataSet myds = new DataSet();
            MySqlConnection mycon = new MySqlConnection();
            mycon.ConnectionString = M_str_sqlcon;

            try
            {
                    SetViveMastIdValue();
                
                string shortEN = ConfigurationManager.AppSettings["ViveShortEn"];

                string sql = "select eventid as f_cardrecordid,cardno as f_Cardno, eventtime as readDateTime,1 as f_readdate, "
              + " employeeNo as f_workno from event where eventid > " + ViveMastIdValue + " and cardno<>'' and employeeNo like '" + shortEN + "%'"
              + " union "
              + " select eventid as f_cardrecordid,cardno as f_Cardno, eventtime as readDateTime,0 as f_readdate, "
              + " employeeNo as f_workno from event"
              + " where eventid > " + ViveMastIdValue + " and cardno<>'' and employeeNo like '" + shortEN + "%' order by f_cardrecordid;";

                // 连接打卡服务器
                mycon.Open();
                MySqlDataAdapter mda = new MySqlDataAdapter(sql, mycon);

                mda.Fill(myds, "table1");
            }
            catch (Exception ex)
            {
                //  logger.Add("AdministrativeService：" + ex.Message + ex.StackTrace);
                throw new Exception(ex.Message);
            }
            finally
            {
                mycon.Close();
            }
            // 将数据插入到内部系统的数据库中
            SaveViveDateInfo(myds);
        }

        /// <summary>
        /// digital获取最小的进门时间，按门卡号进行分组
        /// </summary>
        public void GetDigitalDateInfo()
        {
            //logger.Add("AdministrativeService：获取最小的进门时间");
            DataSet ds = new DataSet();
            SqlConnection conn = new SqlConnection();
            try
            {

                    SetDigitalMastIdValue();
                int hour = int.Parse(ConfigurationManager.AppSettings["OffWorkPointTime2"]);
                string shortEN = ConfigurationManager.AppSettings["DigitalShortEn"];

                string sql = "select max(f_cardrecordid) as f_cardrecordid, f_Cardno, min(f_readdate) as readDateTime, 1 as f_readdate, f_workno from v_d_cardrecord"
                             + " where f_CardRecordID > " + DigitalMastIdValue + " and f_CardNo<>'' and f_workno like '" + shortEN + "%'"
                             + " group by f_workno, f_Cardno, DATEPART(year, DATEADD(hour, - " + hour + ", f_readdate)), "
                             + " DATEPART(month, DATEADD(hour, - " + hour + ", f_readdate)), DATEPART(day, DATEADD(hour, - " + hour + ", f_readdate))"
                             + " union "
                             + " select max(f_cardrecordid) as f_cardrecordid, f_Cardno, max(f_readdate), 0 as f_readdate, f_workno from v_d_cardrecord"
                             + " where f_CardRecordID > " + DigitalMastIdValue + " and f_CardNo<>'' and f_workno like '" + shortEN + "%'"
                             + " group by f_workno, f_Cardno, DATEPART(year, DATEADD(hour, - " + hour + ", f_readdate)), "
                             + " DATEPART(month, DATEADD(hour, - " + hour + ", f_readdate)), DATEPART(day, DATEADD(hour, - " + hour + ", f_readdate)) "
                             + " order by f_cardrecordid asc";
                // 连接打卡服务器
                conn.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["connsource"].ConnectionString;
                SqlCommand cmd = new SqlCommand();
                SqlDataAdapter adapter = new SqlDataAdapter();
                cmd.Connection = conn;
                cmd.CommandTimeout = 180;
                cmd.CommandText = sql;

                conn.Open();
                adapter.SelectCommand = cmd;
                ds.Tables.Clear();
                // 读取打卡记录信息
                adapter.Fill(ds);
            }
            catch (Exception ex)
            {
                //  logger.Add("AdministrativeService：" + ex.Message + ex.StackTrace);
                throw new Exception(ex.Message);
            }
            finally
            {
                conn.Close();
            }
            // 将数据插入到内部系统的数据库中
            SaveDigitalDateInfo(ds);
        }

        public void GetDigitalDateInfoSource2()
        {
            string M_str_sqlcon = System.Configuration.ConfigurationManager.ConnectionStrings["connsource2"].ConnectionString; ;
            DataSet myds = new DataSet();
            MySqlConnection mycon = new MySqlConnection();
            mycon.ConnectionString = M_str_sqlcon;

            try
            {
                    SetDigitalMastIdValue2();
                
                string shortEN = ConfigurationManager.AppSettings["DigitalShortEn"];

                string sql = "select eventid as f_cardrecordid,cardno as f_Cardno, eventtime as readDateTime,1 as f_readdate, "
              + " employeeNo as f_workno from event where eventid > " + DigitalMastIdValue2 + " and cardno<>'' and employeeNo like '" + shortEN + "%'"
              + " union "
              + " select eventid as f_cardrecordid,cardno as f_Cardno, eventtime as readDateTime,0 as f_readdate, "
              + " employeeNo as f_workno from event"
              + " where eventid > " + DigitalMastIdValue2 + " and cardno<>'' and employeeNo like '" + shortEN + "%' order by f_cardrecordid;";

                // 连接打卡服务器
                mycon.Open();
                MySqlDataAdapter mda = new MySqlDataAdapter(sql, mycon);

                mda.Fill(myds, "table1");
            }
            catch (Exception ex)
            {
                //  logger.Add("AdministrativeService：" + ex.Message + ex.StackTrace);
                throw new Exception(ex.Message);
            }
            finally
            {
                mycon.Close();
            }
            // 将数据插入到内部系统的数据库中
            SaveDigitalDateInfo2(myds);
        }

        /// <summary>
        /// 将前一天的打卡记录数据插入到考勤数据中
        /// </summary>
        /// <param name="ds">打卡数据记录</param>
        public void SaveDateInfo(DataSet ds)
        {
            string sql = "";
            System.Data.SqlClient.SqlTransaction trans;
            System.Data.SqlClient.SqlConnection conn = new SqlConnection();
            conn.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ADSqlConnection"].ConnectionString;
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            try
            {
                conn.Open();
                trans = conn.BeginTransaction();
                cmd.Transaction = trans;

                // 插入考勤记录信息
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    sql = @"insert into AD_ClockIn([CardNO],[ReadTime],[InOrOut],[DoorName],[CreateTime],[UserCode]) 
                           values('" + ds.Tables[0].Rows[i][1].ToString() + "','" + ds.Tables[0].Rows[i][2].ToString() + "','"
                                     + ds.Tables[0].Rows[i][3].ToString() + "','','" + attendanceDateTime.ToString("yyyy-MM-dd HH:mm:ss") + "','"
                                     + ds.Tables[0].Rows[i][4].ToString() + "')";
                    cmd.CommandText = sql;
                    cmd.ExecuteNonQuery();
                    mastIdValue = int.Parse(ds.Tables[0].Rows[i][0].ToString());
                }
                // 将导入的打卡记录中最大的ID值更新到数据库中
                sql = "update ad_datacode set code='" + mastIdValue + "' where type= 'MastIdValue' and deleted = 'false'";
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
               // logger.Add("AdministrativeService：更新打卡记录导入的最大ID值:" + mastIdValue);

                // 提交事务
                trans.Commit();
               // logger.Add("AdministrativeService：打卡记录导入完成");
                ds.Tables.Clear();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        public void SaveDateInfo2(DataSet ds)
        {
            string sql = "";
            System.Data.SqlClient.SqlTransaction trans;
            System.Data.SqlClient.SqlConnection conn = new SqlConnection();
            conn.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ADSqlConnection"].ConnectionString;
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            try
            {
                conn.Open();
                trans = conn.BeginTransaction();
                cmd.Transaction = trans;

                // 插入考勤记录信息
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    sql = @"insert into AD_ClockIn([CardNO],[ReadTime],[InOrOut],[DoorName],[CreateTime],[UserCode]) 
                           values('" + ds.Tables[0].Rows[i][1].ToString() + "','" + ds.Tables[0].Rows[i][2].ToString() + "','"
                                     + ds.Tables[0].Rows[i][3].ToString() + "','new','" + attendanceDateTime.ToString("yyyy-MM-dd HH:mm:ss") + "','"
                                     + ds.Tables[0].Rows[i][4].ToString() + "')";
                    cmd.CommandText = sql;
                    cmd.ExecuteNonQuery();
                    mastIdValue2 = int.Parse(ds.Tables[0].Rows[i][0].ToString());
                }
                // 将导入的打卡记录中最大的ID值更新到数据库中
                sql = "update ad_datacode set code='" + mastIdValue2 + "' where type= 'MastIdValue2' and deleted = 'false'";
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
                // logger.Add("AdministrativeService：更新打卡记录导入的最大ID值:" + mastIdValue2);

                // 提交事务
                trans.Commit();
                logger.Add("AdministrativeService：打卡记录导入完成");
                ds.Tables.Clear();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        public void SaveXingYanDateInfo(DataSet ds)
        {
            string sql = "";
            System.Data.SqlClient.SqlTransaction trans;
            System.Data.SqlClient.SqlConnection conn = new SqlConnection();
            conn.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["XingyanSqlConnection"].ConnectionString;
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            try
            {
                conn.Open();
                trans = conn.BeginTransaction();
                cmd.Transaction = trans;

                // 插入考勤记录信息
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    sql = @"insert into AD_ClockIn([CardNO],[ReadTime],[InOrOut],[DoorName],[CreateTime],[UserCode]) 
                           values('" + ds.Tables[0].Rows[i][1].ToString() + "','" + ds.Tables[0].Rows[i][2].ToString() + "','"
                                     + ds.Tables[0].Rows[i][3].ToString() + "','xynew','" + attendanceDateTime.ToString("yyyy-MM-dd HH:mm:ss") + "','"
                                     + ds.Tables[0].Rows[i][4].ToString() + "')";
                    cmd.CommandText = sql;
                    cmd.ExecuteNonQuery();
                    XingYanMastIdValue = int.Parse(ds.Tables[0].Rows[i][0].ToString());
                }
                // 将导入的打卡记录中最大的ID值更新到数据库中
                sql = "update ad_datacode set code='" + XingYanMastIdValue + "' where type= 'MastIdValue' and deleted = 'false'";
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
                // logger.Add("AdministrativeService：更新打卡记录导入的最大ID值:" + mastIdValue2);

                // 提交事务
                trans.Commit();
                logger.Add("AdministrativeService：打卡记录导入完成");
                ds.Tables.Clear();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }


        /// <summary>
        /// 金色区块将前一天的打卡记录数据插入到考勤数据中
        /// </summary>
        /// <param name="ds">打卡数据记录</param>
        public void SaveGoldenBlockChainDateInfo(DataSet ds)
        {
            //logger.Add("AdministrativeService：开始将打卡记录插入到考勤系统中");
            string sql = "";
            System.Data.SqlClient.SqlTransaction trans;
            System.Data.SqlClient.SqlConnection conn = new SqlConnection();
            conn.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["GoldenBlockChainADSqlConnection"].ConnectionString;
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            try
            {
                conn.Open();
                trans = conn.BeginTransaction();
                cmd.Transaction = trans;

                // 插入考勤记录信息
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    sql = @"insert into AD_ClockIn([CardNO],[ReadTime],[InOrOut],[DoorName],[CreateTime],[UserCode]) 
                           values('" + ds.Tables[0].Rows[i][1].ToString() + "','" + ds.Tables[0].Rows[i][2].ToString() + "','"
                                     + ds.Tables[0].Rows[i][3].ToString() + "','','" + attendanceDateTime.ToString("yyyy-MM-dd HH:mm:ss") + "','"
                                     + ds.Tables[0].Rows[i][4].ToString() + "')";
                    cmd.CommandText = sql;
                    cmd.ExecuteNonQuery();
                    GoldenBlockChainMastIdValue = int.Parse(ds.Tables[0].Rows[i][0].ToString());
                }
                // 将导入的打卡记录中最大的ID值更新到数据库中
                sql = "update ad_datacode set code='" + GoldenBlockChainMastIdValue + "' where type= 'MastIdValue' and deleted = 'false'";
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
                // logger.Add("AdministrativeService：更新打卡记录导入的最大ID值:" + GoldenBlockChainMastIdValue);

                // 提交事务
                trans.Commit();
               // logger.Add("AdministrativeService：打卡记录导入完成");
                ds.Tables.Clear();
            }
            catch (Exception ex)
            {
                logger.Add("AdministrativeService：打卡记录插入出现异常：" + ex.Message + ex.StackTrace);
                throw new Exception(ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        /// <summary>
        /// 金色区块将前一天的打卡记录数据插入到考勤数据中
        /// </summary>
        /// <param name="ds">打卡数据记录</param>
        public void SaveViveDateInfo(DataSet ds)
        {
            //logger.Add("AdministrativeService：开始将打卡记录插入到考勤系统中");
            string sql = "";
            System.Data.SqlClient.SqlTransaction trans;
            System.Data.SqlClient.SqlConnection conn = new SqlConnection();
            conn.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ViveSqlConnection"].ConnectionString;
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            try
            {
                conn.Open();
                trans = conn.BeginTransaction();
                cmd.Transaction = trans;

                // 插入考勤记录信息
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    sql = @"insert into AD_ClockIn([CardNO],[ReadTime],[InOrOut],[DoorName],[CreateTime],[UserCode]) 
                           values('" + ds.Tables[0].Rows[i][1].ToString() + "','" + ds.Tables[0].Rows[i][2].ToString() + "','"
                                     + ds.Tables[0].Rows[i][3].ToString() + "','','" + attendanceDateTime.ToString("yyyy-MM-dd HH:mm:ss") + "','"
                                     + ds.Tables[0].Rows[i][4].ToString() + "')";
                    cmd.CommandText = sql;
                    cmd.ExecuteNonQuery();
                    ViveMastIdValue = int.Parse(ds.Tables[0].Rows[i][0].ToString());
                }
                // 将导入的打卡记录中最大的ID值更新到数据库中
                sql = "update ad_datacode set code='" + ViveMastIdValue + "' where type= 'MastIdValue' and deleted = 'false'";
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
                // logger.Add("AdministrativeService：更新打卡记录导入的最大ID值:" + ViveMastIdValue);

                // 提交事务
                trans.Commit();
                // logger.Add("AdministrativeService：打卡记录导入完成");
                ds.Tables.Clear();
            }
            catch (Exception ex)
            {
                logger.Add("AdministrativeService：打卡记录插入出现异常：" + ex.Message + ex.StackTrace);
                throw new Exception(ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        /// <summary>
        /// Digital将前一天的打卡记录数据插入到考勤数据中
        /// </summary>
        /// <param name="ds"></param>
        public void SaveDigitalDateInfo(DataSet ds)
        {
            //logger.Add("AdministrativeService：开始将打卡记录插入到考勤系统中");
            string sql = "";
            System.Data.SqlClient.SqlTransaction trans;
            System.Data.SqlClient.SqlConnection conn = new SqlConnection();
            conn.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["DigitalSqlConnection"].ConnectionString;
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            try
            {
                conn.Open();
                trans = conn.BeginTransaction();
                cmd.Transaction = trans;

                // 插入考勤记录信息
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    sql = @"insert into AD_ClockIn([CardNO],[ReadTime],[InOrOut],[DoorName],[CreateTime],[UserCode]) 
                           values('" + ds.Tables[0].Rows[i][1].ToString() + "','" + ds.Tables[0].Rows[i][2].ToString() + "','"
                                     + ds.Tables[0].Rows[i][3].ToString() + "','','" + attendanceDateTime.ToString("yyyy-MM-dd HH:mm:ss") + "','"
                                     + ds.Tables[0].Rows[i][4].ToString() + "')";
                    cmd.CommandText = sql;
                    cmd.ExecuteNonQuery();
                    DigitalMastIdValue = int.Parse(ds.Tables[0].Rows[i][0].ToString());
                }
                // 将导入的打卡记录中最大的ID值更新到数据库中
                sql = "update ad_datacode set code='" + DigitalMastIdValue + "' where type= 'MastIdValue' and deleted = 'false'";
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
                // logger.Add("AdministrativeService：更新打卡记录导入的最大ID值:" + DigitalMastIdValue);

                // 提交事务
                trans.Commit();
                // logger.Add("AdministrativeService：打卡记录导入完成");
                ds.Tables.Clear();
            }
            catch (Exception ex)
            {
                logger.Add("AdministrativeService：打卡记录插入出现异常：" + ex.Message + ex.StackTrace);
                throw new Exception(ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        public void SaveDigitalDateInfo2(DataSet ds)
        {
            //logger.Add("AdministrativeService：开始将打卡记录插入到考勤系统中");
            string sql = "";
            System.Data.SqlClient.SqlTransaction trans;
            System.Data.SqlClient.SqlConnection conn = new SqlConnection();
            conn.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["DigitalSqlConnection"].ConnectionString;
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            try
            {
                conn.Open();
                trans = conn.BeginTransaction();
                cmd.Transaction = trans;

                // 插入考勤记录信息
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    sql = @"insert into AD_ClockIn([CardNO],[ReadTime],[InOrOut],[DoorName],[CreateTime],[UserCode]) 
                           values('" + ds.Tables[0].Rows[i][1].ToString() + "','" + ds.Tables[0].Rows[i][2].ToString() + "','"
                                     + ds.Tables[0].Rows[i][3].ToString() + "','new','" + attendanceDateTime.ToString("yyyy-MM-dd HH:mm:ss") + "','"
                                     + ds.Tables[0].Rows[i][4].ToString() + "')";
                    cmd.CommandText = sql;
                    cmd.ExecuteNonQuery();
                    DigitalMastIdValue2 = int.Parse(ds.Tables[0].Rows[i][0].ToString());
                }
                // 将导入的打卡记录中最大的ID值更新到数据库中
                sql = "update ad_datacode set code='" + DigitalMastIdValue2 + "' where type= 'MastIdValue2' and deleted = 'false'";
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
                // logger.Add("AdministrativeService：更新打卡记录导入的最大ID值:" + DigitalMastIdValue2);

                // 提交事务
                trans.Commit();
                // logger.Add("AdministrativeService：打卡记录导入完成");
                ds.Tables.Clear();
            }
            catch (Exception ex)
            {
                logger.Add("AdministrativeService：打卡记录插入出现异常：" + ex.Message + ex.StackTrace);
                throw new Exception(ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }


        /// <summary>
        /// 设置上次导入的最大打卡记录ID
        /// </summary>
        private void SetMastIdValue()
        {
            System.Data.SqlClient.SqlConnection conn = new SqlConnection();
            conn.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ADSqlConnection"].ConnectionString;
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();
            try
            {
                cmd.Connection = conn;
                conn.Open();
                string sql = "";
                if (mastIdValue == 0)
                {
                    sql = "select code from ad_datacode where type= 'MastIdValue' and deleted='false' ";
                    cmd.CommandText = sql;
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        mastIdValue = int.Parse(dr[0].ToString());
                    }
                    dr.Close();
                }
            }
            catch (Exception ex)
            {
                //logger.Add("AdministrativeService：查询最大打卡记录ID时出现异常：" + ex.Message + ex.StackTrace);
                throw new Exception(ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        private void SetMastIdValue2()
        {
            System.Data.SqlClient.SqlConnection conn = new SqlConnection();
            conn.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ADSqlConnection"].ConnectionString;
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();
            try
            {
                cmd.Connection = conn;
                conn.Open();
                string sql = "";
                if (mastIdValue2 == 0)
                {
                    sql = "select code from ad_datacode where type= 'MastIdValue2' and deleted='false' ";
                    cmd.CommandText = sql;
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        mastIdValue2 = int.Parse(dr[0].ToString());
                    }
                    dr.Close();
                }
            }
            catch (Exception ex)
            {
                //logger.Add("AdministrativeService：查询最大打卡记录ID时出现异常：" + ex.Message + ex.StackTrace);
                throw new Exception(ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        private void SetXingYanMastIdValue()
        {
            System.Data.SqlClient.SqlConnection conn = new SqlConnection();
            conn.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["XingyanSqlConnection"].ConnectionString;
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();
            try
            {
                cmd.Connection = conn;
                conn.Open();
                string sql = "";
                if (XingYanMastIdValue == 0)
                {
                    sql = "select code from ad_datacode where type= 'MastIdValue' and deleted='false' ";
                    cmd.CommandText = sql;
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        XingYanMastIdValue = int.Parse(dr[0].ToString());
                    }
                    dr.Close();
                }
            }
            catch (Exception ex)
            {
                //logger.Add("AdministrativeService：查询最大打卡记录ID时出现异常：" + ex.Message + ex.StackTrace);
                throw new Exception(ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }


        /// <summary>
        /// 设置上次导入的最大打卡记录ID
        /// </summary>
        private void SetGoldenBlockChainMastIdValue()
        {
            System.Data.SqlClient.SqlConnection conn = new SqlConnection();
            conn.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["GoldenBlockChainADSqlConnection"].ConnectionString;
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();
            try
            {
                cmd.Connection = conn;
                conn.Open();
                string sql = "";
                if (GoldenBlockChainMastIdValue == 0)
                {
                    sql = "select code from ad_datacode where type= 'MastIdValue' and deleted='false' ";
                    cmd.CommandText = sql;
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        GoldenBlockChainMastIdValue = int.Parse(dr[0].ToString());
                    }
                    dr.Close();
                }
            }
            catch (Exception ex)
            {
                //logger.Add("AdministrativeService：查询最大打卡记录ID时出现异常：" + ex.Message + ex.StackTrace);
                throw new Exception(ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }


        /// <summary>
        /// Vive设置上次导入的最大打卡记录ID
        /// </summary>
        private void SetViveMastIdValue()
        {
            System.Data.SqlClient.SqlConnection conn = new SqlConnection();
            conn.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ViveSqlConnection"].ConnectionString;
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();
            try
            {
                cmd.Connection = conn;
                conn.Open();
                string sql = "";
                if (ViveMastIdValue == 0)
                {
                    sql = "select code from ad_datacode where type= 'MastIdValue' and deleted='false' ";
                    cmd.CommandText = sql;
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        ViveMastIdValue = int.Parse(dr[0].ToString());
                    }
                    dr.Close();
                }
            }
            catch (Exception ex)
            {
                //logger.Add("AdministrativeService：查询最大打卡记录ID时出现异常：" + ex.Message + ex.StackTrace);
                throw new Exception(ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        /// <summary>
        /// Digital设置上次导入的最大打卡记录ID
        /// </summary>
        private void SetDigitalMastIdValue()
        {
            System.Data.SqlClient.SqlConnection conn = new SqlConnection();
            conn.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["DigitalSqlConnection"].ConnectionString;
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();
            try
            {
                cmd.Connection = conn;
                conn.Open();
                string sql = "";
                if (DigitalMastIdValue == 0)
                {
                    sql = "select code from ad_datacode where type= 'MastIdValue' and deleted='false' ";
                    cmd.CommandText = sql;
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        DigitalMastIdValue = int.Parse(dr[0].ToString());
                    }
                    dr.Close();
                }
            }
            catch (Exception ex)
            {
                //logger.Add("AdministrativeService：查询最大打卡记录ID时出现异常：" + ex.Message + ex.StackTrace);
                throw new Exception(ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        private void SetDigitalMastIdValue2()
        {
            System.Data.SqlClient.SqlConnection conn = new SqlConnection();
            conn.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["DigitalSqlConnection"].ConnectionString;
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();
            try
            {
                cmd.Connection = conn;
                conn.Open();
                string sql = "";
                if (DigitalMastIdValue2 == 0)
                {
                    sql = "select code from ad_datacode where type= 'MastIdValue2' and deleted='false' ";
                    cmd.CommandText = sql;
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        DigitalMastIdValue2 = int.Parse(dr[0].ToString());
                    }
                    dr.Close();
                }
            }
            catch (Exception ex)
            {
                //logger.Add("AdministrativeService：查询最大打卡记录ID时出现异常：" + ex.Message + ex.StackTrace);
                throw new Exception(ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }



        #endregion

    }
}