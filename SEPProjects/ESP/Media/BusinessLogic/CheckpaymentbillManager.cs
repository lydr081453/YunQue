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
    public class CheckpaymentbillManager
    {
        /// <summary>
        /// 支票/电汇付款单添加
        /// </summary>
        /// <param name="bill">支票/电汇付款单数据对象</param>
        /// <param name="userid">操作人</param>
        /// <returns></returns>
        public static int add(CheckpaymentbillInfo bill, int userid)
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
                    int id = ESP.Media.DataAccess.CheckpaymentbillDataProvider.insertinfo(bill, trans);
                    bill.Checkpaymentbillid = id;
 
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
        /// 编辑支票/电汇付款单
        /// </summary>
        /// <param name="bill">支票/电汇付款单数据对象</param>
        /// <param name="userid"></param>
        /// <returns></returns>
        public static bool modify(CheckpaymentbillInfo bill, int userid)
        {
            using (SqlConnection conn = new SqlConnection(ESP.Media.Access.Utilities.clsConfigOperate.CustomerSqlConnection()))
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();
                try
                {
                    bool res = ESP.Media.DataAccess.CheckpaymentbillDataProvider.updateInfo(trans, null, bill, null, null);
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
        /// 删除支票/电汇付款单
        /// </summary>
        /// <param name="billid">支票/电汇付款单ID</param>
        /// <param name="userid">操作人</param>
        /// <returns></returns>
        public static bool delete(int billid, int userid)
        {
            CheckpaymentbillInfo bill = GetModel(billid);
            using (SqlConnection conn = new SqlConnection(ESP.Media.Access.Utilities.clsConfigOperate.CustomerSqlConnection()))
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();
                try
                {
                    bill.Del = (int)Global.FiledStatus.Del;
                    bool res = ESP.Media.DataAccess.CheckpaymentbillDataProvider.updateInfo(trans, null, bill, null, null);
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
        /// 获取支票/电汇付款单对象
        /// </summary>
        /// <param name="billid"></param>
        /// <returns></returns>
        public static CheckpaymentbillInfo GetModel(int billid)
        {
            CheckpaymentbillInfo bill = ESP.Media.DataAccess.CheckpaymentbillDataProvider.Load(billid);
            if (bill == null)
                bill = new ESP.Media.Entity.CheckpaymentbillInfo();
            return bill;
        }

        /// <summary>
        /// 获取支票/电汇付款单列表
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
            term += " and del != @del order by a.Checkpaymentbillid desc";
            if (!ht.ContainsKey("@del"))
                ht.Add("@del", (int)Global.FiledStatus.Del);
            return ESP.Media.DataAccess.CheckpaymentbillDataProvider.QueryInfo(term, ht);
        }

        /// <summary>
        /// 导出支票/电汇付款单
        /// </summary>
        /// <param name="id">支票/电汇付款单ID</param>
        /// <param name="serverpath">保存路径</param>
        /// <param name="filename">文件名</param>
        /// <param name="isDelete">是否需要删除(如果生成成功，会产生临时文件，需要删除，如果生成失败的话，就不需要删除)</param>
        /// <returns></returns>
        public static string GetCheckPaymentBill(int id, string serverpath, out string filename, out bool isDelete)
        {
            #region getinfo
          ESP.Media.Entity.CheckpaymentbillInfo CheckPaymentBill = GetModel(id);

          DataTable dt = CheckpaymentitemManager.GetListByBillID(id);

            string temppath = serverpath + "CheckPaymentBill.xls";
            filename = "CheckPaymentBill" + DateTime.Now.Ticks.ToString() + ".xls";
            string filepath = serverpath + filename;
            isDelete = true;


            string companyname = dt.Rows.Count > 0 ? CheckPaymentBill.Companyname : string.Empty;
            string bankname = dt.Rows.Count > 0 ? CheckPaymentBill.Bankname : string.Empty;
            string bankaccount = dt.Rows.Count > 0 ? CheckPaymentBill.Bankaccount : string.Empty;



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
                    CheckpaymentbillInfo bill = GetModel(id);
                    ExcelHandle.WriteCell(excel.CurSheet, "F9", DateTime.Parse(bill.Createdate).ToString("yyyy-MM-dd"));
                }

                if (dt.Rows.Count > 3)
                {

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
                    string projectcode = dt.Rows.Count > 0 ? dt.Rows[0]["projectcode"].ToString() : string.Empty;
                    ExcelHandle.WriteCell(excel.CurSheet, cell, projectcode);

                    cell = string.Format("B{0}", rownum);
                    ExcelHandle.WriteCell(excel.CurSheet, cell, row["happendate"].ToString().Split(' ')[0]);

                    cell = string.Format("C{0}", rownum);
                    ExcelHandle.WriteCell(excel.CurSheet, cell, row["username"].ToString());

                    cell = string.Format("D{0}", rownum);
                    ExcelHandle.WriteCell(excel.CurSheet, cell, row["userdepartmentname"].ToString());

                    cell = string.Format("E{0}", rownum);
                    ExcelHandle.WriteCell(excel.CurSheet, cell, row["describe"].ToString());

                    cell = string.Format("F{0}", rownum);
                    ExcelHandle.WriteCell(excel.CurSheet, cell, row["amount"].ToString());
                    rownum++;
                }
                if (rownum < 15)
                {
                    rownum = 15;
                }
                else
                {
                    rownum++;
                }
                cell = string.Format("C{0}", rownum);
                ExcelHandle.WriteCell(excel.CurSheet, cell, "公司名称");

                cell = string.Format("D{0}", rownum);
                ExcelHandle.WriteCell(excel.CurSheet, cell, companyname);
                rownum++;

                cell = string.Format("C{0}", rownum);
                ExcelHandle.WriteCell(excel.CurSheet, cell, "开户行名称");

                cell = string.Format("D{0}", rownum);
                ExcelHandle.WriteCell(excel.CurSheet, cell, bankname);
                rownum++;

                cell = string.Format("C{0}", rownum);
                ExcelHandle.WriteCell(excel.CurSheet, cell, "银行帐号");

                cell = string.Format("D{0}", rownum);
                ExcelHandle.WriteCell(excel.CurSheet, cell, bankaccount);
                rownum++;


                ExcelHandle.SaveAS(excel.CurBook, filepath);
                excel.Dispose();
                return ConfigManager.BillPath + filename;
            }
            catch
            {
                isDelete = false;
                excel.Dispose();
                filename = "CheckPaymentBill.xls";
                return ConfigManager.BillPath + "CheckPaymentBill.xls";
            }
            #endregion
        }
    }
}
