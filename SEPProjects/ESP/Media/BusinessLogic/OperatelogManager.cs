using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using ESP.Media.Access;
using ESP.Media.Entity;
using System.Collections;
using ESP.Media.Access.Utilities;

namespace ESP.Media.BusinessLogic
{
    public class OperatelogManager
    {
        /// <summary>
        /// 添加日志
        /// </summary>
        /// <param name="operatetypeid">操作类型</param>
        /// <param name="operatetableid">操作表</param>
        /// <param name="userid">操作人</param>
        /// <returns></returns>
        public static int add(int operatetypeid, int operatetableid, int userid)
        {
            OperatelogInfo log = new ESP.Media.Entity.OperatelogInfo();
            log.Operatetypeid = operatetypeid;
            log.Operatetableid = operatetableid;
            log.Userid = userid;
            log.Operatetime = DateTime.Now.ToString();
            log.Del = (int)Global.FiledStatus.Usable;

            return add(log);
        }


        /// <summary>
        /// 添加日志
        /// </summary>
        /// <param name="operatetypeid">操作类型</param>
        /// <param name="operatetableid">操作表</param>
        /// <param name="userid">操作人</param>
        /// <param name="trans">事务</param>
        /// <returns></returns>
        public static int add(int operatetypeid, int operatetableid, int userid,SqlTransaction trans)
        {
            OperatelogInfo log = new ESP.Media.Entity.OperatelogInfo();
            log.Operatetypeid = operatetypeid;
            log.Operatetableid = operatetableid;
            log.Userid = userid;
            log.Operatetime = DateTime.Now.ToString();
            log.Del = (int)Global.FiledStatus.Usable;
            return add(log,trans);
        }


        /// <summary>
        /// 添加日志
        /// </summary>
        /// <param name="log">日志对象</param>
        /// <returns></returns>
        public static int add(OperatelogInfo log)
        {
            int result = 0;
            using (SqlConnection conn = new SqlConnection(ESP.Media.Access.Utilities.clsConfigOperate.CustomerSqlConnection()))
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();
                try
                {
                    result = add(log, trans);
                    trans.Commit();
                    conn.Close();
                    return result;
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    conn.Close();
                    Console.WriteLine(ex.Message);
                    return -1;
                }
            }
        }


        /// <summary>
        /// 添加日志
        /// </summary>
        /// <param name="log">日志对象</param>
        /// <param name="trans">事务</param>
        /// <returns></returns>
        public static int add(OperatelogInfo log, SqlTransaction trans)//最终调用
        {
            log.Del = (int)Global.FiledStatus.Usable;
            IntegralruleInfo rule = ESP.Media.BusinessLogic.IntegralruleManager.GetModel(log.Operatetypeid, log.Operatetableid, trans);//获取相应的积分
            if (rule == null) rule = new ESP.Media.Entity.IntegralruleInfo();
            log.Integralid = rule.Id;
            log.Integral = rule.Integral;
            CounterInfo counter = new ESP.Media.Entity.CounterInfo();
            ESP.Compatible.Employee emp = new ESP.Compatible.Employee(log.Userid);
            counter.Userid = log.Userid;
            counter.Username = emp.Name;
            counter.Counts = rule.Integral;
            ESP.Media.BusinessLogic.CounterManager.add(counter, log.Userid);
            if (log.Operatetime == null || log.Operatetime == string.Empty)
            {
                log.Operatetime = DateTime.Now.ToString();
            }
            return ESP.Media.DataAccess.OperatelogDataProvider.insertinfo(log, trans);
        }


       
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="terms">条件</param>
        /// <param name="ht">Sqlparameter集合</param>
        /// <returns></returns>
        public static DataTable GetList(string terms, Hashtable ht)
        {
            string sql = @"select a.ID as OperateLogId,
                            a.UserID as UserID,
                            b.AltName as OperateAltName,
                            c.AltTableName as AltTableName,
                            a.OperateTime as OperateTime,
                            a.OperateDes as OperateDes
                             from media_OperateLog as a 
                            inner join media_OperateType as b on a.OperatetypeID = b.id
                            inner join media_Tables as c on a.OperateTableID = c.TableID  where 1=1 {0}";
           
            if (terms == null)
            {
                terms = string.Empty;
            }
            terms += " and del !=@del order by a.id desc";
            sql = string.Format(sql, terms);
            if (ht == null)
            {
                ht = new Hashtable();
            }
            if (!ht.ContainsKey("@del"))
            {
                ht.Add("@del", (int)Global.FiledStatus.Del);
            }
            SqlParameter[] param = ESP.Media.Access.Utilities.Common.DictToSqlParam(ht);
            return ESP.Media.Access.Utilities.clsSelect.QueryBySql(sql, param);
        }

        /// <summary>
        /// 获取积分
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public static int GetIntegral(int userid)
        {
            string sql = "select sum(Integral) from media_Operatelog where del !=@del and UserID=@UserID ";
            SqlParameter [] param = new SqlParameter[2] ;
            param[0] = new SqlParameter("@UserID", SqlDbType.Int);
            param[0].Value = userid;
            param[1] = new SqlParameter("@del", SqlDbType.Int);
            param[1].Value = (int)Global.FiledStatus.Del;
            DataTable dt = ESP.Media.Access.Utilities.clsSelect.QueryBySql(sql, param);
            if (dt == null || dt.Rows.Count <= 0) return 0;
            return (dt.Rows[0][0] == DBNull.Value || dt.Rows[0][0].ToString().Length <= 0) ? 0 : Convert.ToInt32(dt.Rows[0][0]);
        }

      
    }
}
