using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.IO;
using ESP.Media.Access;
using ESP.Media.Entity;
using ESP.Media.Access.Utilities;

namespace ESP.Media.BusinessLogic
{
    public class CashloanManager
    {
        public static int add(CashloanbillInfo bill, int userid)
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
                    int id = ESP.Media.DataAccess.CashloanbillDataProvider.insertinfo(bill, trans);
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

        public static bool modify(CashloanbillInfo bill, int userid)
        {
            using (SqlConnection conn = new SqlConnection(ESP.Media.Access.Utilities.clsConfigOperate.CustomerSqlConnection()))
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();
                try
                {
                    bool res = ESP.Media.DataAccess.CashloanbillDataProvider.updateInfo(trans, null, bill, null, null);
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


        public static bool delete(int billid, int userid)
        {
            CashloanbillInfo bill = GetModel(billid);
            using (SqlConnection conn = new SqlConnection(ESP.Media.Access.Utilities.clsConfigOperate.CustomerSqlConnection()))
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();
                try
                {
                    bill.Del = (int)Global.FiledStatus.Del;
                    bool res = ESP.Media.DataAccess.CashloanbillDataProvider.updateInfo(trans, null, bill, null, null);
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

        public static CashloanbillInfo GetModel(int billid)
        {
            CashloanbillInfo bill = ESP.Media.DataAccess.CashloanbillDataProvider.Load(billid);
            if (bill == null)
                bill = new ESP.Media.Entity.CashloanbillInfo(); 
            return bill;
        }


        public static DataTable GetList(string term, Hashtable ht)
        {

            string sql = @"SELECT     item.id, item.cashloanbillid, item.describe, item.amount, item.del, item.projectid, item.projectcode, item.createip, bill.createdate, item.userid, 
                      item.username, item.userdepartmentid, item.userdepartmentname, item.userextensioncode, item.ReimburseDate,  bill.status
                    FROM         media_cashloanitem AS item INNER JOIN
                      media_cashloanbill AS bill ON item.cashloanbillid = bill.cashloanbillid where 1=1 ";
            if (term == null)
                term = string.Empty;
            if (ht == null)
                ht = new Hashtable();
            term += " and item.del != @del and bill.del != @del order by bill.Cashloanbillid desc";
            sql += term;
            if (!ht.ContainsKey("@del"))
                ht.Add("@del", (int)Global.FiledStatus.Del);
            return ESP.Media.Access.Utilities.clsSelect.QueryBySql(sql, ESP.Media.Access.Utilities.Common.DictToSqlParam(ht));
        }

        public static List<CashloanbillInfo> GetObjectList(string term, Hashtable ht)
        {
            DataTable dt = GetList(term, ht);
            List<CashloanbillInfo> bills = new List<CashloanbillInfo>();
            var query = from bill in dt.AsEnumerable() select ESP.Media.DataAccess.CashloanbillDataProvider.setObject(bill);
            foreach (CashloanbillInfo bill in query)
            {
                bills.Add(bill);
            }
            return bills;
        }

        public static List<CashloanbillInfo> GetObjectList(string term, List<SqlParameter> param)
        {
            Hashtable ht = new Hashtable();
            for (int i = 0; i < param.Count; i++)
            {
                ht.Add(param[i].ParameterName,param[i].Value);
            }
            DataTable dt = GetList(term, ht);
            List<CashloanbillInfo> bills = new List<ESP.Media.Entity.CashloanbillInfo>();
            var query = from bill in dt.AsEnumerable() select ESP.Media.DataAccess.CashloanbillDataProvider.setObject(bill);
            foreach (CashloanbillInfo bill in query)
            {
                bills.Add(bill);
            }
            return bills;
        }

        public static string GetCashLaonBill(int id, string serverpath, out string filename, out bool isDelete)
        {
            #region getinfo
            CashloanbillInfo model = GetModel(id);
            string user = model.Username;
            string department = model.Userdepartmentname;
            string projectcode = model.Projectcode;
            DateTime createddate = Convert.ToDateTime(model.Createdate);

            string shunyalog = serverpath + ConfigManager.ShunyaWordLogoPath;

            filename = "CashLaonBill"+DateTime.Now.Ticks.ToString()+".doc";
            string filepath = serverpath + filename;

            DataTable dt = ESP.Media.BusinessLogic.CashloanitemManager.GetListByBillID(id);
            #endregion

            WordClass word = new WordClass();
            try
            {
                word.CreateDocument();
                word.AddPicture(shunyalog);
                word.AppendText("");
                word.WordApplication.Selection.Font.Size = 22;
                word.WordApplication.Selection.Font.Underline = Microsoft.Office.Interop.Word.WdUnderline.wdUnderlineSingle;
                word.WordApplication.Selection.Font.Bold = 1;
                word.WordApplication.Selection.Range.ParagraphFormat.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphCenter;
                word.AppendText("现金借款单");

                word.WordApplication.Selection.Font.Size = 11;
                word.WordApplication.Selection.Font.Underline = Microsoft.Office.Interop.Word.WdUnderline.wdUnderlineNone;
                word.WordApplication.Selection.Font.Bold = 0;
                word.WordApplication.Selection.Range.ParagraphFormat.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphCenter;
                word.AppendText("");
                word.WordApplication.Selection.Range.ParagraphFormat.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft;
                word.AppendText(string.Format("申请日期：{0}年{1}月{2}日 					        领用日期：    年   月   日", createddate.Year, createddate.Month, createddate.Day));
                Microsoft.Office.Interop.Word.Table table = word.AppendTable(2, 4);
                table.Columns[1].Width = 105;
                table.Columns[2].Width = 105;
                table.Columns[3].Width = 105;
                table.Columns[4].Width = 105;

                table.Cell(1, 1).Merge(table.Cell(1, 3));
                table.Cell(1, 1).Width = 280;
                table.Cell(1, 2).Width = 140;

                float amount = 0;
                int i = 0;
                for (i = 0; i < dt.Rows.Count; i++)
                {
                    word.AppendRow(table);

                    word.SetCellText(table.Cell(1, 1), dt.Rows[i]["describe"].ToString(), Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft);
                    word.SetCellText(table.Cell(1, 2), ("￥" + double.Parse(dt.Rows[i]["amount"].ToString()).ToString("0.00")), Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft);
                    amount = amount + Convert.ToSingle(dt.Rows[i]["amount"]);
                }

                word.AppendRow(table);
                word.SetCellText(table.Cell(1, 1), "借款明细：", Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft);
                word.SetCellText(table.Cell(1, 2), "金额：", Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft);

                word.AppendRow(table);
                table.Rows[1].Height = 20;
                word.SetCellText(table.Cell(1, 1), string.Format("借款人：{0}\t\t组别:{1}", user, department), Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft);
                word.SetCellText(table.Cell(1, 2), string.Format("项目号：{0}", projectcode), Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft);

                table.Rows[dt.Rows.Count + 3].Height = 20;
                word.SetCellText(table.Cell(dt.Rows.Count + 3, 1), "借款合计：" + XConvert.ToRMB((double)amount), Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft);
                word.SetCellText(table.Cell(dt.Rows.Count + 3, 2), "￥" + amount.ToString("0.00"), Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft);

                int rownum = dt.Rows.Count + 4;
                table.Rows[rownum].Height = 20;
                word.SetCellText(table.Cell(rownum, 1), "借款人签字：\n\n\n日期:", Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft);
                word.SetCellText(table.Cell(rownum, 2), "第一级批准人签字：\n\n\n日期:", Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft);
                word.SetCellText(table.Cell(rownum, 3), "第二级核准人签字：\n\n\n日期:", Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft);
                word.SetCellText(table.Cell(rownum, 4), "收款人签字：\n\n\n日期:", Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft);

                word.SaveAs(filepath);
                word.Close(true);
                word.Dispose();
                isDelete = true;
                
                return ESP.Media.Access.Utilities.ConfigManager.BillPath + filename;
            }
            catch (Exception ex)
            {
                try
                {
                    //word.Close(false);
                    word.Dispose();
                }
                catch { };
            }
            isDelete = false;
            return ESP.Media.Access.Utilities.ConfigManager.BillPath + "CashLaonBill.doc";
        }
    }
}
