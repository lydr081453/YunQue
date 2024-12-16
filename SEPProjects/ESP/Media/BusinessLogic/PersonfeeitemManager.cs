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
    public class PersonfeeitemManager
    {
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="item">数据对象(一条个人报销项)</param>
        /// <param name="userid">操作人</param>
        /// <returns></returns>
        public static int add(PersonfeeitemInfo item, int userid)
        {
            using (SqlConnection conn = new SqlConnection(ESP.Media.Access.Utilities.clsConfigOperate.CustomerSqlConnection()))
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();
                item.Del = (int)Global.FiledStatus.Usable;
                try
                {
                    if (item.Personfeebillid == 0)
                    {
                        PersonfeebillInfo bill = new PersonfeebillInfo();
                        bill.Createdate = DateTime.Now.ToString();
                        bill.Userid = userid;
                        ESP.Compatible.Employee emp = new ESP.Compatible.Employee(userid);
                        bill.Username = emp.Name;
                        bill.Createip = System.Net.Dns.GetHostAddresses(System.Net.Dns.GetHostName())[0].ToString();
                        bill.Projectid = item.Projectid;
                        bill.Userextensioncode = emp.Telephone;

                        int billid = ESP.Media.BusinessLogic.PersonfeebillManager.add(bill, bill.Userid, trans);
                        item.Personfeebillid = billid;
                    }
                    int id = ESP.Media.DataAccess.PersonfeeitemDataProvider.insertinfo(item, trans);
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

        public static int add(PersonfeeitemInfo item, int userid,ref int billid)
        {
            using (SqlConnection conn = new SqlConnection(ESP.Media.Access.Utilities.clsConfigOperate.CustomerSqlConnection()))
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();
                item.Del = (int)Global.FiledStatus.Usable;
                try
                {
                    if (item.Personfeebillid == 0)
                    {
                        PersonfeebillInfo bill = new PersonfeebillInfo();
                        bill.Createdate = DateTime.Now.ToString();
                        bill.Userid = userid;
                        ESP.Compatible.Employee emp = new ESP.Compatible.Employee(userid);
                        bill.Username = emp.Name;
                        bill.Createip = System.Net.Dns.GetHostAddresses(System.Net.Dns.GetHostName())[0].ToString();
                        bill.Projectid = item.Projectid;
                        bill.Userextensioncode = emp.Telephone;

                        billid = ESP.Media.BusinessLogic.PersonfeebillManager.add(bill, bill.Userid, trans);
                        item.Personfeebillid = billid;
                    }
                    int id = ESP.Media.DataAccess.PersonfeeitemDataProvider.insertinfo(item, trans);
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

        /// <summary>
        /// 更新一条个人费用报销项
        /// </summary>
        /// <param name="item">要更新的数据对象(一条个人报销项)</param>
        /// <param name="userid"></param>
        /// <returns></returns>
        public static bool modify(PersonfeeitemInfo item, int userid)
        {
            using (SqlConnection conn = new SqlConnection(ESP.Media.Access.Utilities.clsConfigOperate.CustomerSqlConnection()))
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();
                try
                {
                    ESP.Media.DataAccess.PersonfeeitemDataProvider.updateInfo(trans,null,item,null,null);
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
        /// 删除一条个人费用报销项
        /// </summary>
        /// <param name="itemid">要删除的个人费用报销项ID</param>
        /// <param name="userid">操作人</param>
        /// <returns></returns>
        public static bool del(int itemid, int userid)
        {
            PersonfeeitemInfo item = GetModel(itemid);
            using (SqlConnection conn = new SqlConnection(ESP.Media.Access.Utilities.clsConfigOperate.CustomerSqlConnection()))
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();
                item.Del = (int)Global.FiledStatus.Del;
                try
                {
                    if (item.Personfeebillid > 0 && (getItemCount(item.Personfeebillid, trans) == 0))
                    {
                        ESP.Media.BusinessLogic.PersonfeebillManager.delete(item.Personfeebillid, userid, trans);
                    }
                    ESP.Media.DataAccess.PersonfeeitemDataProvider.updateInfo(trans, null, item, null, null);
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
            SqlParameter[] param = new SqlParameter[2];
            param[0].ParameterName = "@personfeebillid";
            param[0].Value = billid;
            param[1].ParameterName = "@del";
            param[1].Value = 0;
            return Convert.ToInt32(ESP.Media.Access.Utilities.SqlHelper.ExecuteScalar(trans, CommandType.Text, "select count(*) from media_personfeeitem where personfeebillid=@personfeebillid and del=@del", param));

        }

        /// <summary>
        /// 获取一条个人费用报销项的对象
        /// </summary>
        /// <param name="itemid">个人费用报销项ID</param>
        /// <returns></returns>
        public static PersonfeeitemInfo GetModel(int itemid)
        {
            PersonfeeitemInfo item = ESP.Media.DataAccess.PersonfeeitemDataProvider.Load(itemid);
            if (item == null) item = new ESP.Media.Entity.PersonfeeitemInfo();
            return item;
        }


        /// <summary>
        /// 获取个人费用报销项列表
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
            term += " and del != @del order by a.personfeeitemid desc";
            if (!ht.ContainsKey("@del"))
                ht.Add("@del", (int)Global.FiledStatus.Del);
            return ESP.Media.DataAccess.PersonfeeitemDataProvider.QueryInfo(term, ht);
        }

        public static List<PersonfeeitemInfo> GetObjectListByBillID(int billid)
        {
            string term = " and personfeebillid=@personfeebillid";
            Hashtable ht = new Hashtable();
            ht.Add("@personfeebillid", billid);

            DataTable dt = GetList(term, ht);
            var query = from bill in dt.AsEnumerable() select ESP.Media.DataAccess.PersonfeeitemDataProvider.setObject(bill);
            List<PersonfeeitemInfo> items = new List< PersonfeeitemInfo>();
            foreach (PersonfeeitemInfo item in query)
            {
                items.Add(item);
            }
            return items;
        }
        /// <summary>
        /// 获取一个个人费用报销单的所有项
        /// </summary>
        /// <param name="billid"></param>
        /// <returns></returns>
        public static DataTable GetListByBillID(int billid)
        {

            string term = " and Personfeebillid=@Personfeebillid ";
            Hashtable ht = new Hashtable();
            ht.Add("@Personfeebillid", billid);
            return GetList(term, ht);
        }

    }
}
