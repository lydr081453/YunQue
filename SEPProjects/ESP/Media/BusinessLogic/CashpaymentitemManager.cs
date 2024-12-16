using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using ESP.Media.Access;
using ESP.Media.Entity;
using ESP.Media.Access.Utilities;

namespace ESP.Media.BusinessLogic
{
    public class CashpaymentitemManager
    {
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="item"></param>
        /// <param name="userid"></param>
        /// <returns></returns>
        public static int add(CashpaymentitemInfo item, int userid ,ref int billid)
        {
            using (SqlConnection conn = new SqlConnection(ESP.Media.Access.Utilities.clsConfigOperate.CustomerSqlConnection()))
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();
                item.Del = (int)Global.FiledStatus.Usable;
                try
                {
                    billid = item.Cashpaymentbillid;//如果还没有相应的借款单记录，则先保存借款单信息
                    if (billid == 0)
                    {
                        CashpaymentbillInfo bill = new ESP.Media.Entity.CashpaymentbillInfo();
                        bill.Createdate = DateTime.Now.ToString();
                        bill.Projectid = item.Projectid;
                        bill.Projectcode = item.Projectcode;
                        bill.Userdepartmentid = item.Userdepartmentid;
                        bill.Userdepartmentname = item.Userdepartmentname;
                        bill.Userid = item.Userid;
                        bill.Username = item.Username;
                        bill.Userextensioncode = item.Userextensioncode;
                        billid =ESP.Media.DataAccess.CashpaymentbillDataProvider.insertinfo(bill, trans);
                        item.Cashpaymentbillid = billid;
                    }

                    int id = ESP.Media.DataAccess.CashpaymentitemDataProvider.insertinfo(item, trans);
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

        public static int add(CashpaymentitemInfo item, int userid)
        {
            int billid = 0;
            return add(item, userid, ref billid);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="item"></param>
        /// <param name="userid"></param>
        /// <returns></returns>
        public static bool modify(CashpaymentitemInfo item, int userid)
        {
            using (SqlConnection conn = new SqlConnection(ESP.Media.Access.Utilities.clsConfigOperate.CustomerSqlConnection()))
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();
                try
                {
                    ESP.Media.DataAccess.CashpaymentitemDataProvider.updateInfo(trans, null, item, null, null);
                    trans.Commit();
                    conn.Close();
                    return true;
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

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="itemid"></param>
        /// <param name="userid"></param>
        /// <returns></returns>
        public static bool del(int itemid, int userid)
        {
            CashpaymentitemInfo item = GetModel(itemid);
            using (SqlConnection conn = new SqlConnection(ESP.Media.Access.Utilities.clsConfigOperate.CustomerSqlConnection()))
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();
                item.Del = (int)Global.FiledStatus.Del;
                try
                {
                    ESP.Media.DataAccess.CashpaymentitemDataProvider.updateInfo(trans, null, item, null, null);


                    if (item.Cashpaymentbillid > 0 && (getItemCount(item.Cashpaymentbillid, trans) == 0))
                    {
                        CashpaymentbillManager.delete(item.Cashpaymentbillid, userid, trans);
                    }

                    trans.Commit();
                    conn.Close();
                    return true;
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


        private static int getItemCount(int billid, SqlTransaction trans)
        {
            return Convert.ToInt32(ESP.Media.Access.Utilities.SqlHelper.ExecuteScalar(trans, CommandType.Text, "select count(*) from Media_cashpaymentitem where del=0 and Cashpaymentbillid=" + billid + ""));

        }


        /// <summary>
        /// 获取对象
        /// </summary>
        /// <param name="itemid"></param>
        /// <returns></returns>
        public static CashpaymentitemInfo GetModel(int itemid)
        {
            CashpaymentitemInfo item = ESP.Media.DataAccess.CashpaymentitemDataProvider.Load(itemid);
            if (item == null) item = new ESP.Media.Entity.CashpaymentitemInfo();
            return item;
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="term"></param>
        /// <param name="ht"></param>
        /// <returns></returns>
        private static DataTable query(string term, Hashtable ht)
        {
            string sql = @"select  a.cashpaymentitemid as cashpaymentitemid,a.cashpaymentbillid as cashpaymentbillid,a.projectid as projectid,
                            a.projectcode as projectcode,a.happendate as happendate,a.userid as userid,a.username as username,
                            a.userdepartmentid as userdepartmentid,a.userdepartmentname as userdepartmentname,
                            a.describe as describe,a.cash as cash,a.del as del,
                            project.companyname as companyname, project.bankname as bankname,project.bankaccount as bankaccount
                            from Media_cashpaymentitem as a 
                            inner join media_projects as project on a.projectid = project.projectid
                            where 1=1 {0} order by a.cashpaymentitemid desc";
            if (term == null)
                term = string.Empty;
            sql = string.Format(sql, term);
            if (ht == null) ht = new Hashtable();

            return ESP.Media.Access.Utilities.clsSelect.QueryBySql(sql, ESP.Media.Access.Utilities.Common.DictToSqlParam(ht));

        }

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="term"></param>
        /// <param name="ht"></param>
        /// <returns></returns>
        public static DataTable GetList(string term, Hashtable ht)
        {
            if (term == null)
                term = string.Empty;
            if (ht == null)
                ht = new Hashtable();
            term += " and a.del != @del";
            if (!ht.ContainsKey("@del"))
                ht.Add("@del", (int)Global.FiledStatus.Del);
            return query(term, ht);
        }

        /// <summary>
        /// 根据现金费用报销单获取现金费用项列表
        /// </summary>
        /// <param name="billid"></param>
        /// <returns></returns>
        public static DataTable GetListByBillID(int billid)
        {

            string term = " and a.Cashpaymentbillid=@Cashpaymentbillid ";
            Hashtable ht = new Hashtable();
            ht.Add("@Cashpaymentbillid", billid);
            return GetList(term, ht);
        }

        public static DataTable GetListByBillID(int billid, string term, Hashtable ht)
        {
            if (term == null)
                term = string.Empty;
            term += " and Cashpaymentbillid=@Cashpaymentbillid ";
            if (ht == null)
            {
                ht = new Hashtable();
            }
            ht.Add("@Cashpaymentbillid", billid);
            return GetList(term, ht);
        }

        public static List<CashpaymentitemInfo> GetObjectListByBillID(int billid, string term, List<SqlParameter> param)
        {
            Hashtable ht = new Hashtable();
            if (param == null)
            {
                param = new List<SqlParameter>();
            }
            for (int i = 0; i < param.Count; i++)
            {
                ht.Add(param[i].ParameterName,param[i].Value);
            }
            DataTable dt = GetListByBillID(billid, term, ht);
            List<ESP.Media.Entity.CashpaymentitemInfo> items = new List<ESP.Media.Entity.CashpaymentitemInfo>();
            var query = from project in dt.AsEnumerable() select ESP.Media.DataAccess.CashpaymentitemDataProvider.setObject(project);
            foreach (CashpaymentitemInfo item in query)
            {
                items.Add(item);
            }
            return items;
        }
    }
}
