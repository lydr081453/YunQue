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
    public class CheckpaymentitemManager
    {
        public static int add(CheckpaymentitemInfo item, int userid)
        {
            using (SqlConnection conn = new SqlConnection(ESP.Media.Access.Utilities.clsConfigOperate.CustomerSqlConnection()))
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();
                item.Del = (int)Global.FiledStatus.Usable;
                try
                {
                    int id = ESP.Media.DataAccess.CheckpaymentitemDataProvider.insertinfo(item, trans);
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


        public static bool modify(CheckpaymentitemInfo item, int userid)
        {
            using (SqlConnection conn = new SqlConnection(ESP.Media.Access.Utilities.clsConfigOperate.CustomerSqlConnection()))
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();
                try
                {
                    ESP.Media.DataAccess.CheckpaymentitemDataProvider.updateInfo(trans, null, item, null, null);
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


        public static bool del(int itemid, int userid)
        {
            CheckpaymentitemInfo item = GetModel(itemid);
            using (SqlConnection conn = new SqlConnection(ESP.Media.Access.Utilities.clsConfigOperate.CustomerSqlConnection()))
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();
                item.Del = (int)Global.FiledStatus.Del;
                try
                {
                    ESP.Media.DataAccess.CheckpaymentitemDataProvider.updateInfo(trans, null, item, null, null);
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

        public static CheckpaymentitemInfo GetModel(int itemid)
        {
            CheckpaymentitemInfo item = ESP.Media.DataAccess.CheckpaymentitemDataProvider.Load(itemid);
            if (item == null) item = new ESP.Media.Entity.CheckpaymentitemInfo();
            return item;
        }

        private static DataTable query(string term, Hashtable ht)
        {
            string sql = @"select  a.checkpaymentitemid as checkpaymentitemid,a.checkpaymentbillid as checkpaymentbillid,
                           a.projectid as projectid,a.projectcode as projectcode,a.happendate as happendate,a.userid as userid,
                            a.username as username,a.userdepartmentid as userdepartmentid,a.userdepartmentname as userdepartmentname,
                            a.describe as describe,a.amount as amount,a.del as del,
                            project.companyname as companyname, project.bankname as bankname,project.bankaccount as bankaccount
                            from Media_checkpaymentitem as a 
                            inner join media_projects as project on a.projectid = project.projectid
                            where 1=1 {0} order by a.checkpaymentitemid desc";
            if (term == null)
                term = string.Empty;
            sql = string.Format(sql, term);
            if (ht == null) ht = new Hashtable();

            return ESP.Media.Access.Utilities.clsSelect.QueryBySql(sql, ESP.Media.Access.Utilities.Common.DictToSqlParam(ht));

        }

        public static DataTable GetList(string term, Hashtable ht)
        {
            if (term == null)
                term = string.Empty;
            if (ht == null)
                ht = new Hashtable();
            term += " and a.del != @del ";
            if (!ht.ContainsKey("@del"))
                ht.Add("@del", (int)Global.FiledStatus.Del);
            return query(term, ht);
        }

        public static DataTable GetListByBillID(int billid)
        {
            string term = " and Checkpaymentbillid=@Checkpaymentbillid ";
            Hashtable ht = new Hashtable();
            ht.Add("@Checkpaymentbillid", billid);
            return GetList(term, ht);
        }
    }
}
