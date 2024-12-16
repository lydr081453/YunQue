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
    /// <summary>
    /// 目前不使用此类
    /// </summary>
    public class PaymentbillManager
    {
        public static PaymentbillInfo  GetModel(int id)
        {
            PaymentbillInfo bill = null;
            bill = ESP.Media.DataAccess.PaymentbillDataProvider.Load(id);
            if (bill == null) bill = new ESP.Media.Entity.PaymentbillInfo ();
            return bill;
        }


        public static int add(PaymentbillInfo c, int userid)
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
        public static int add(PaymentbillInfo  c, SqlTransaction trans, int userid)
        {
            int id = 0;
            c.Financecode = GetFinanceCode(trans);
            id = ESP.Media.DataAccess.PaymentbillDataProvider.insertinfo(c, trans);
            return id;
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="p">p</param>
        /// <param name="emp">当前登录人</param>
        /// <returns></returns>
        public static bool modify(PaymentbillInfo c, int userid)
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
        public static bool modify(PaymentbillInfo c, SqlTransaction trans, int userid)
        {
            bool result = false;
            result = ESP.Media.DataAccess.PaymentbillDataProvider.updateInfo(trans, null, c, null, null);
            return result;
        }


        /// <summary>
        /// 获取列表
        /// </summary>
        /// <returns></returns>
        public static DataTable getAllList()
        {
            return ESP.Media.DataAccess.PaymentbillDataProvider.QueryInfo(null, new Hashtable());
        }

        /// <summary>
        /// FP08070001
        /// </summary>
        /// <returns></returns>
        private static string GetFinanceCode(SqlTransaction trans)
        {
            string prefix = "FB";
            string date = DateTime.Now.ToString("yy-MM").Replace("-", "");
            string strSql = "select max(financecode) as maxId from media_Paymentbill as a where a.financecode like '" + prefix + date + "%'";
            DataTable dt = ESP.Media.Access.Utilities.clsSelect.QueryBySql(strSql, trans);
            int num = dt.Rows[0]["maxId"] == DBNull.Value ? 0 : int.Parse(dt.Rows[0]["maxId"].ToString().Substring(6).ToString());
            string id = prefix + date;
            num++;
            return id + num.ToString("0000");
        }

        public static DataTable GetListByProjectID(int projectid, string term, Hashtable ht)
        {
            if (ht == null) ht = new Hashtable();
            string sql = @"SELECT     paymentbill.paymentbillid, paymentbill.projectid,projectcode, paymentbill.PaymentDate, paymentbill.PaymentUserid, paymentbill.PaymentUsername, 
                          paymentbill.PaymentRemark, paymentbill.del, paymentbill.FinanceCode
                            FROM         Media_PaymentBill AS paymentbill INNER JOIN
                                                  media_Projects AS prj ON paymentbill.projectid = prj.ProjectID
                            WHERE     (prj.del != @del) and  ";
            if(projectid > 0){
                sql += @"AND (prj.ProjectID = @projectid)";
                ht.Add("@projectid", projectid);
            }
            
            sql += @"AND (paymentbill.del != @del) {0}
                            ORDER BY paymentbill.paymentbillid DESC
                            ";
            if (term == null) term = string.Empty;
            

            ht.Add("@del", (int)Global.FiledStatus.Del);
            

            sql = string.Format(sql, term);
            SqlParameter[] param = ESP.Media.Access.Utilities.Common.DictToSqlParam(ht);
            return ESP.Media.Access.Utilities.clsSelect.QueryBySql(sql, param);
        }

    }
}
