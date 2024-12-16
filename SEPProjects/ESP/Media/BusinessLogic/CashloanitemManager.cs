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
    public class CashloanitemManager
    {
        public static int add(CashloanitemInfo item, int userid, ref int billid)
        {
            using (SqlConnection conn = new SqlConnection(ESP.Media.Access.Utilities.clsConfigOperate.CustomerSqlConnection()))
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();
                item.Del = (int)Global.FiledStatus.Usable;
                try
                {
                    billid = item.Cashloanbillid;//如果还没有相应的借款单记录，则先保存借款单信息
                    if (billid == 0)
                    {
                        CashloanbillInfo bill = new ESP.Media.Entity.CashloanbillInfo();
                        bill.Createdate = DateTime.Now.ToString();
                        bill.Projectid = item.Projectid;
                        bill.Projectcode = item.Projectcode;
                        bill.Userdepartmentid = item.Userdepartmentid;
                        bill.Userdepartmentname = item.Userdepartmentname;
                        bill.Userid = item.Userid;
                        bill.Username = item.Username;
                        bill.Userextensioncode = item.Userextensioncode;
                        billid = ESP.Media.DataAccess.CashloanbillDataProvider.insertinfo(bill, trans);
                        item.Cashloanbillid = billid;
                    }

                    int id = ESP.Media.DataAccess.CashloanitemDataProvider.insertinfo(item, trans);
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

        public static int add(CashloanitemInfo item, int userid)
        {
            int billid = 0;
            return add(item, userid, ref billid);
        }

        public static bool modify(CashloanitemInfo item, int userid)
        {
            using (SqlConnection conn = new SqlConnection(ESP.Media.Access.Utilities.clsConfigOperate.CustomerSqlConnection()))
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();
                try
                {
                    ESP.Media.DataAccess.CashloanitemDataProvider.updateInfo(trans, null, item, null, null);
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


        public static bool delete(int itemid, int userid)
        {
            CashloanitemInfo item = GetModel(itemid);
            using (SqlConnection conn = new SqlConnection(ESP.Media.Access.Utilities.clsConfigOperate.CustomerSqlConnection()))
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();
                try
                {
                    CashloanbillInfo bill = ESP.Media.BusinessLogic.CashloanManager.GetModel(item.Cashloanbillid);
                    bill.Del = (int)Global.FiledStatus.Del;
                    bool res = ESP.Media.DataAccess.CashloanbillDataProvider.updateInfo(trans, null, bill, null, null);//先将个人借款单删除
                    item.Del = (int)Global.FiledStatus.Del;
                    res = ESP.Media.DataAccess.CashloanitemDataProvider.updateInfo(trans, null, item, null, null);
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


        public static CashloanitemInfo GetModel(int itemid)
        {
            CashloanitemInfo item = ESP.Media.DataAccess.CashloanitemDataProvider.Load(itemid);
            if (item == null)
                item = new ESP.Media.Entity.CashloanitemInfo();
            return item;
        }


        public static DataTable GetList(string term, Hashtable ht)
        {
            if (term == null)
                term = string.Empty;
            if (ht == null)
                ht = new Hashtable();
            term += " and del != @del order by a.Cashloanbillid desc";
            if (!ht.ContainsKey("@del"))
                ht.Add("@del", (int)Global.FiledStatus.Del);
            return ESP.Media.DataAccess.CashloanitemDataProvider.QueryInfo(term, ht);
        }

        public static DataTable GetListByBillID(int billid)
        {

            string term = " and Cashloanbillid=@Cashloanbillid ";
            Hashtable ht = new Hashtable();
            ht.Add("@Cashloanbillid", billid);
            return GetList(term, ht);
        }

        public static DataTable GetListByBillID(int billid, string term, List<SqlParameter> param)
        {
            if (param == null)
            {
                param = new List<SqlParameter>();
            }
            if (term == null)
                term = string.Empty;
            term += " and Cashloanbillid=@Cashloanbillid ";
            Hashtable ht = new Hashtable();
            for (int i = 0; i < param.Count; i++)
            {
                ht.Add(param[i].ParameterName, param[i].Value);
            }
            ht.Add("@Cashloanbillid", billid);

            return GetList(term, ht);
        }

        public static List<CashloanitemInfo> GetObjectListByBillID(int billid, string term, List<SqlParameter> param)
        {
            DataTable dt = GetListByBillID(billid, term, param);
            List<CashloanitemInfo> items = new List<CashloanitemInfo>();
            var query = from project in dt.AsEnumerable() select ESP.Media.DataAccess.CashloanitemDataProvider.setObject(project);
            foreach (CashloanitemInfo item in query)
            {
                items.Add(item);
            }
            return items;
        }
    }
}
