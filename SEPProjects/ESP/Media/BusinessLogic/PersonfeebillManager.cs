using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using Excel = Microsoft.Office.Interop.Excel;
using ESP.Media.Access;
using ESP.Media.Entity;
using ESP.Media.Access.Utilities;

namespace ESP.Media.BusinessLogic
{
    /// <summary>
    /// 个人费用报销单
    /// </summary>
    public class PersonfeebillManager
    {
        /// <summary>
        /// 添加个人费用报销单
        /// </summary>
        /// <param name="bill">要添加的个人费用报销单对象</param>
        /// <param name="userid">操作人</param>
        /// <returns></returns>
        public static int add(PersonfeebillInfo bill, int userid)
        {
            using (SqlConnection conn = new SqlConnection(ESP.Media.Access.Utilities.clsConfigOperate.CustomerSqlConnection()))
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();
                if (bill.Createdate == string.Empty)
                    bill.Createdate = DateTime.Now.ToString();
                bill.Del = (int)Global.FiledStatus.Usable;
                try
                {
                    int id = add(bill, userid, trans);
                    bill.Personfeebillid = id;
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

        public static int add(PersonfeebillInfo bill, int userid, SqlTransaction trans)
        {
            return ESP.Media.DataAccess.PersonfeebillDataProvider.insertinfo(bill, trans); 
        }

        /// <summary>
        /// 修改个人费用报销单
        /// </summary>
        /// <param name="bill">要更新的个人费用报销单对象</param>
        /// <param name="userid">操作人</param>
        /// <returns></returns>
        public static bool modify(PersonfeebillInfo bill, int userid)
        {
            using (SqlConnection conn = new SqlConnection(ESP.Media.Access.Utilities.clsConfigOperate.CustomerSqlConnection()))
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();
                try
                {
                    bool res = ESP.Media.DataAccess.PersonfeebillDataProvider.updateInfo(trans,null,bill,null,null);
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

        /// <summary>
        /// 删除个人费用报销单
        /// </summary>
        /// <param name="bill">要删除的个人费用报销单ID</param>
        /// <param name="userid">操作人</param>
        /// <returns></returns>
        public static bool delete(int billid, int userid)
        {
            PersonfeebillInfo bill = GetModel(billid);
            using (SqlConnection conn = new SqlConnection(ESP.Media.Access.Utilities.clsConfigOperate.CustomerSqlConnection()))
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();
                try
                {
                    bill.Del = (int)Global.FiledStatus.Del;
                    bool res = ESP.Media.DataAccess.PersonfeebillDataProvider.updateInfo(trans, null, bill, null, null);
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

        public static bool delete(int billid, int userid, SqlTransaction trans)
        {
            PersonfeebillInfo bill = GetModel(billid);

            bill.Del = (int)Global.FiledStatus.Del;
            return ESP.Media.DataAccess.PersonfeebillDataProvider.updateInfo(trans, null, bill, null, null);

        }

        /// <summary>
        /// 个人费用报销单
        /// </summary>
        /// <param name="billid">The billid.</param>
        /// <returns></returns>
        public static PersonfeebillInfo GetModel(int billid)
        {
            PersonfeebillInfo bill = ESP.Media.DataAccess.PersonfeebillDataProvider.Load(billid);
            if (bill == null)
                bill = new ESP.Media.Entity.PersonfeebillInfo();
            return bill;
        }


        public static DataTable GetList(string term, Hashtable ht)
        {
            if (term == null)
                term = string.Empty;
            if (ht == null)
                ht = new Hashtable();
            term += " and del != @del order by a.personfeebillid desc";
            if (!ht.ContainsKey("@del"))
                ht.Add("@del", (int)Global.FiledStatus.Del);
            return ESP.Media.DataAccess.PersonfeebillDataProvider.QueryInfo(term, ht);
        }

        public static List<PersonfeebillInfo> GetObjectList(string term,Hashtable ht)
        {
            DataTable dt = GetList(term, ht);
            var query = from bill in dt.AsEnumerable() select ESP.Media.DataAccess.PersonfeebillDataProvider.setObject(bill);
            List<PersonfeebillInfo> items = new List<PersonfeebillInfo>();
            foreach (PersonfeebillInfo item in query)
            {
                items.Add(item);
            }
            return items;
        }

        /// <summary>
        /// 导出个人费用报销单
        /// </summary>
        /// <param name="id">报销单ID</param>
        /// <param name="serverpath">存放路径</param>
        /// <param name="filename">文件名</param>
        /// <param name="isDelete">是否需要删除(如果生成成功，会产生临时文件，需要删除，如果生成失败的话，就不需要删除)</param>
        /// <returns></returns>
        public static string GetPersonFeeBill(int id,string serverpath, out string filename, out bool isDelete)
        {
            #region getinfo
            isDelete = true;
            DataTable dt = PersonfeeitemManager.GetListByBillID(id);

            string temppath = serverpath + "PersonFeeBill.xls";
            filename = "PersonFeeBill"+DateTime.Now.Ticks.ToString()+".xls";
            string filepath = serverpath + filename;
            #endregion

            ExcelHandle excel = new ExcelHandle();
            try
            {
                excel.Load(temppath);
                excel.CurSheet = (Excel._Worksheet)excel.CurBook.Sheets[1];

                string[] departments = CommonManager.GetDepartmentStrings(dt, 3);
                if (departments.Length > 0)
                {
                    ExcelHandle.WriteCell(excel.CurSheet, "B6", departments[0]);
                    ExcelHandle.WriteCell(excel.CurSheet, "B7", departments[1]);
                    ExcelHandle.WriteCell(excel.CurSheet, "B8", departments[2]);
                }
                if (dt.Rows.Count > 0)
                {
                    PersonfeebillInfo bill = GetModel(int.Parse(dt.Rows[0]["personfeebillid"].ToString()));
                    ExcelHandle.WriteCell(excel.CurSheet, "B9", bill.Userextensioncode);
                    ExcelHandle.WriteCell(excel.CurSheet, "F9", DateTime.Parse(bill.Createdate).ToString("yyyy-MM-dd"));
                }

                if (dt.Rows.Count > 3)
                {
                    //插入采购物品行
                    Microsoft.Office.Interop.Excel.Range range = (Microsoft.Office.Interop.Excel.Range)excel.CurSheet.Rows[12, Type.Missing];
                    for (int i = 0; i < (dt.Rows.Count - 3); i++)
                    {
                        range.Insert(Microsoft.Office.Interop.Excel.XlInsertShiftDirection.xlShiftDown, Type.Missing);
                    }
                }

                int rownum = 11;
                string cell = "";
                foreach (DataRow row in dt.Rows)
                {
                    cell = string.Format("A{0}", rownum);
                    ExcelHandle.WriteCell(excel.CurSheet, cell, row["projectcode"].ToString());

                    cell = string.Format("B{0}", rownum);
                    ExcelHandle.WriteCell(excel.CurSheet, cell, row["happendate"].ToString() == Global.DateTimeNullValue ? "" : row["happendate"].ToString().Split(' ')[0]);

                    cell = string.Format("C{0}", rownum);
                    ExcelHandle.WriteCell(excel.CurSheet, cell, row["username"].ToString());

                    cell = string.Format("D{0}", rownum);
                    ExcelHandle.WriteCell(excel.CurSheet, cell, row["userdepartmentname"].ToString());

                    cell = string.Format("E{0}", rownum);
                    ExcelHandle.WriteCell(excel.CurSheet, cell, row["describe"].ToString());

                    cell = string.Format("F{0}", rownum);
                    ExcelHandle.WriteCell(excel.CurSheet, cell,row["cash"].ToString());
                    rownum++;
                }
                if (rownum < 14)
                    rownum = 14;
                cell = string.Format("F{0}", rownum);
                ExcelHandle.WriteCell(excel.CurSheet, cell, "=SUM(F11:F"+(rownum-1)+")");

                cell = string.Format("F{0}", rownum + 3);
                ExcelHandle.WriteCell(excel.CurSheet, cell, "=F" + rownum + "-F" + (rownum + 1) + "+F" + (rownum + 2));

                ExcelHandle.SaveAS(excel.CurBook, filepath);
                excel.Dispose();
                return ConfigManager.BillPath + filename;
            }
            catch
            {
                isDelete = false;
                excel.Dispose();
                filename = "PersonFeeBill.xls";
                return ConfigManager.BillPath + "PersonFeeBill.xls";
            }
        }
    }
}
