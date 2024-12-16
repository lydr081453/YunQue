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
    /// 现金报销单
    /// </summary>
    public class CashpaymentbillManager
    {

        /// <summary>
        /// 添加现在报销单
        /// </summary>
        /// <param name="bill">现金报销单对象</param>
        /// <param name="userid">操作人</param>
        /// <returns></returns>
        public static int add(CashpaymentbillInfo bill, int userid)
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
                    int id = ESP.Media.DataAccess.CashpaymentbillDataProvider.insertinfo(bill, trans);
                    bill.Cashpaymentbillid = id;
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
        /// 编辑一个现金报销单
        /// </summary>
        /// <param name="bill">要编辑的现金报销单</param>
        /// <param name="userid">操作人</param>
        /// <returns></returns>
        public static bool modify(CashpaymentbillInfo bill, int userid)
        {
            using (SqlConnection conn = new SqlConnection(ESP.Media.Access.Utilities.clsConfigOperate.CustomerSqlConnection()))
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();
                try
                {
                    bool res = ESP.Media.DataAccess.CashpaymentbillDataProvider.updateInfo(trans, null, bill, null, null);
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
        /// 删除一个现金支付单
        /// </summary>
        /// <param name="billid">要删除的现金支付单ID</param>
        /// <param name="userid"></param>
        /// <returns></returns>
        public static bool delete(int billid, int userid)
        {
            CashpaymentbillInfo bill = GetModel(billid);
            using (SqlConnection conn = new SqlConnection(ESP.Media.Access.Utilities.clsConfigOperate.CustomerSqlConnection()))
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();
                try
                {
                    bill.Del = (int)Global.FiledStatus.Del;
                    bool res = ESP.Media.DataAccess.CashpaymentbillDataProvider.updateInfo(trans, null, bill, null, null);
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
        /// 删除一个现金支付单
        /// </summary>
        /// <param name="billid">要删除的现金支付单ID</param>
        /// <param name="userid"></param>
        /// <returns></returns>
        public static bool delete(int billid, int userid, SqlTransaction trans)
        {
            CashpaymentbillInfo bill = GetModel(billid);

            try
            {
                bill.Del = (int)Global.FiledStatus.Del;
                bool res = ESP.Media.DataAccess.CashpaymentbillDataProvider.updateInfo(trans, null, bill, null, null);
                return res;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }

        }

        /// <summary>
        /// 获取一个现金支付单对象
        /// </summary>
        /// <param name="billid">现金支付单id</param>
        /// <returns></returns>
        public static CashpaymentbillInfo GetModel(int billid)
        {
            CashpaymentbillInfo bill = ESP.Media.DataAccess.CashpaymentbillDataProvider.Load(billid);
            if (bill == null)
                bill = new CashpaymentbillInfo();
            return bill;
        }


        public static DataTable GetList(string term, Hashtable ht)
        {
            if (term == null)
                term = string.Empty;
            if (ht == null)
                ht = new Hashtable();
            term += " and del != @del order by a.Cashpaymentbillid desc";
            if (!ht.ContainsKey("@del"))
                ht.Add("@del", (int)Global.FiledStatus.Del);
            return ESP.Media.DataAccess.CashpaymentbillDataProvider.QueryInfo(term, ht);
        }

        public static List<CashpaymentbillInfo> GetObjectList(string term, Hashtable ht)
        {

            DataTable dt = GetList(term, ht);
            List<CashpaymentbillInfo> bills = new List<CashpaymentbillInfo>();
            var query = from bill in dt.AsEnumerable() select ESP.Media.DataAccess.CashpaymentbillDataProvider.setObject(bill);
            foreach (CashpaymentbillInfo bill in query)
            {
                bills.Add(bill);
            }
            return bills;
        }

        public static List<CashpaymentbillInfo> GetObjectList(string term, List<SqlParameter> param)
        {
            Hashtable ht = new Hashtable();
            for (int i = 0; i < param.Count; i++)
            {
                ht.Add(param[i].ParameterName,param[i].Value);
            }
            DataTable dt = GetList( term, ht);
            List<CashpaymentbillInfo> bills = new List<CashpaymentbillInfo>();
            var query = from bill in dt.AsEnumerable() select ESP.Media.DataAccess.CashpaymentbillDataProvider.setObject(bill);
            foreach (CashpaymentbillInfo bill in query)
            {
                bills.Add(bill);
            }
            return bills;
        }

        /// <summary>
        /// 生成现金报销单的Excel
        /// </summary>
        /// <param name="id">现金报销单ID</param>
        /// <param name="serverpath">存放路径</param>
        /// <param name="filename">文件名</param>
        /// <param name="isDelete">是否需要删除(如果生成成功，会产生临时文件，需要删除，如果生成失败的话，就不需要删除)</param>
        /// <returns></returns>
        public static string GetCashPaymentBill(int id, string serverpath, out string filename, out bool isDelete)
        {
            #region getinfo
            isDelete = true;
            DataTable dt = CashpaymentitemManager.GetListByBillID(id);

            string temppath = serverpath + "CashPaymentBill.xls";
            filename = "CashPaymentBill" + DateTime.Now.Ticks.ToString() + ".xls";
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
                    CashpaymentbillInfo bill = GetModel(int.Parse(dt.Rows[0]["cashpaymentbillid"].ToString()));
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

                ExcelHandle.SaveAS(excel.CurBook, filepath);
                excel.Dispose();
                return ConfigManager.BillPath + filename;
            }
            catch
            {
                isDelete = false;
                excel.Dispose();
                filename = "CashPaymentBill.xls";
                return ConfigManager.BillPath + "CashPaymentBill.xls";
            }
        }

    }
}
