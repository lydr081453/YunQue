using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Collections;
using Excel = Microsoft.Office.Interop.Excel;
using ESP.Media.Access;
using ESP.Media.Entity;
using ESP.Media.Access.Utilities;

namespace ESP.Media.BusinessLogic
{
    /// <summary>
    /// 稿费报销单
    /// </summary>
    public class WritingfeebillManager
    {
        /// <summary>
        /// 添加稿费报销单
        /// </summary>
        /// <param name="bill">稿费报销单</param>
        /// <param name="userid">操作人</param>
        /// <returns></returns>
        public static int add(WritingfeebillInfo bill, int userid)
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
                    bill.Financecode = GetFinanceCode(trans);
                    int id = ESP.Media.DataAccess.WritingfeebillDataProvider.insertinfo(bill, trans);
                    bill.Writingfeebillid = id;
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

        public static int add(WritingfeebillInfo bill, int userid, SqlTransaction trans)
        {

                return  ESP.Media.DataAccess.WritingfeebillDataProvider.insertinfo(bill, trans);
            
        }


        /// <summary>
        /// 编辑稿费报销单
        /// </summary>
        /// <param name="bill"></param>
        /// <param name="userid"></param>
        /// <returns></returns>
        public static bool modify(WritingfeebillInfo bill, int userid)
        {
            using (SqlConnection conn = new SqlConnection(ESP.Media.Access.Utilities.clsConfigOperate.CustomerSqlConnection()))
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();
                try
                {
                    bool res = ESP.Media.DataAccess.WritingfeebillDataProvider.updateInfo(trans, null, bill, null, null);
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
        /// 删除稿费报销单
        /// </summary>
        /// <param name="billid">稿费报销单ID</param>
        /// <param name="userid">操作人</param>
        /// <returns></returns>
        public static bool delete(int billid, int userid)
        {
            WritingfeebillInfo bill = GetModel(billid);
            using (SqlConnection conn = new SqlConnection(ESP.Media.Access.Utilities.clsConfigOperate.CustomerSqlConnection()))
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();
                try
                {
                    bill.Del = (int)Global.FiledStatus.Del;
                    bool res = ESP.Media.DataAccess.WritingfeebillDataProvider.updateInfo(trans, null, bill, null, null);
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
            return ESP.Media.DataAccess.WritingfeebillDataProvider.DeleteInfo(billid, trans);
        }

        
        /// <summary>
        /// FP08070001 财务流水号获取
        /// </summary>
        /// <returns></returns>
        private static string GetFinanceCode(SqlTransaction trans)
        {
            string prefix = "FP";
            string date = DateTime.Now.ToString("yy-MM").Replace("-", "");
            string strSql = "select max(financecode) as maxId from Media_writingfeebill as a where a.financecode like '" + prefix + date + "%'";
            DataTable dt = ESP.Media.Access.Utilities.clsSelect.QueryBySql(strSql, trans);
            int num = dt.Rows[0]["maxId"] == DBNull.Value ? 0 : int.Parse(dt.Rows[0]["maxId"].ToString().Substring(6).ToString());
            string id = prefix + date;
            num++;
            return id + num.ToString("0000");
        }

        /// <summary>
        /// 获取稿费报销单对象
        /// </summary>
        /// <param name="billid">稿费报销单ID</param>
        /// <returns></returns>
        public static WritingfeebillInfo GetModel(int billid)
        {
            WritingfeebillInfo bill = ESP.Media.DataAccess.WritingfeebillDataProvider.Load(billid);
            if (bill == null)
                bill = new ESP.Media.Entity.WritingfeebillInfo();
            return bill;
        }

        /// <summary>
        /// 获取稿费报销单列表
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
            term += " and del != @del order by a.Writingfeebillid desc";
            if (!ht.ContainsKey("@del"))
                ht.Add("@del", (int)Global.FiledStatus.Del);
            return ESP.Media.DataAccess.WritingfeebillDataProvider.QueryInfo(term, ht);
        }

        public static List<WritingfeebillInfo> GetObjectList(string term, Hashtable ht)
        {
            DataTable dt = GetList(term,ht);
            var query = from bill in dt.AsEnumerable() select ESP.Media.DataAccess.WritingfeebillDataProvider.setObject(bill);
            List<WritingfeebillInfo> items = new List<WritingfeebillInfo>();
            foreach (WritingfeebillInfo item in query)
            {
                items.Add(item);
            }
            return items;


            //List<MediaLib.WritingfeebillInfo> lists=new List<MediaLib.WritingfeebillInfo>();
            //if (this.validCredential())
            //{
            //    Hashtable ht = new Hashtable();
            //    ht.Add("","");
            //    DataTable dt = MediaLib.BLL.Media_writingfeebill.GetList(null,null);
            //    for (int i = 0; i < dt.Rows.Count; i++)
            //    {
            //        MediaLib.WritingfeebillInfo bill = new ESP.Media.Entity.WritingfeebillInfo();
            //        if (dt.Rows[i]["writingfeebillid"] != DBNull.Value)
            //            bill.Writingfeebillid = Convert.ToInt32(dt.Rows[i]["writingfeebillid"]);
            //        bill.Createip = dt.Rows[i]["createip"].ToString();
            //        bill.Createdate = dt.Rows[i]["createdate"].ToString();
            //        if (dt.Rows[i]["userid"] != DBNull.Value)
            //            bill.Userid = Convert.ToInt32(dt.Rows[i]["userid"]);
            //        bill.Username = dt.Rows[i]["username"].ToString();
            //        if (dt.Rows[i]["userdepartmentid"] != DBNull.Value)
            //            bill.Userdepartmentid = Convert.ToInt32(dt.Rows[i]["userdepartmentid"]);
            //        bill.Userdepartmentname = dt.Rows[i]["userdepartmentname"].ToString();
            //        bill.Describe = dt.Rows[i]["describe"].ToString();
            //        bill.Userextensioncode = dt.Rows[i]["userextensioncode"].ToString();
            //        bill.Reimbursedate = dt.Rows[i]["reimbursedate"].ToString();
            //        if (dt.Rows[i]["del"] != DBNull.Value)
            //            bill.Del = Convert.ToInt32(dt.Rows[i]["del"]);
            //        if (dt.Rows[i]["projectid"] != DBNull.Value)
            //            bill.Projectid = Convert.ToInt32(dt.Rows[i]["projectid"]);
            //        bill.Projectcode = dt.Rows[i]["projectcode"].ToString();
            //        bill.Financecode = dt.Rows[i]["financecode"].ToString();
            //        if (dt.Rows[i]["status"] != DBNull.Value)
            //            bill.Status = Convert.ToInt32(dt.Rows[i]["status"]);
            //        if (dt.Rows[i]["applicantid"] != DBNull.Value)
            //            bill.Applicantid = Convert.ToInt32(dt.Rows[i]["applicantid"]);
            //        bill.Applicantname = dt.Rows[i]["applicantname"].ToString();
            //        lists.Add(bill);
            //    }
            //}
            //    return lists;
        }

        /// <summary>
        /// 导出稿费报销单EXCEL
        /// </summary>
        /// <param name="userid">操作人</param>
        /// <param name="id">稿费报销单ID</param>
        /// <param name="serverpath">存放路径</param>
        /// <param name="ofname">文件名</param>
        /// <param name="isDelete">是否需要删除(如果生成成功，会产生临时文件，需要删除，如果生成失败的话，就不需要删除)</param>
        /// <returns></returns>
        public static string GetWritingFeeBill(int userid,int id, string serverpath,out string ofname,out bool isDelete,bool submitCreate)
        {
            //TODO
            WritingfeebillInfo bill = ESP.Media.BusinessLogic.WritingfeebillManager.GetModel(id);
            if (submitCreate)
            {
                ofname = "WritingFeeBill_" + bill.Financecode + ".xls";
            }
            else
            {
                ofname = "WritingFeeBill" + DateTime.Now.Ticks.ToString() + ".xls";
            }
            if (System.IO.File.Exists(serverpath + ofname))//如果有则删除
            {
                try
                {
                    System.IO.File.Delete(serverpath + ofname);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            DataTable dt = ESP.Media.BusinessLogic.WritingfeeitemManager.GetExportItemList(id);//BLL.Media_writingfeeitem.GetListByBillID(id);

            string temppath = serverpath + "稿费报销明细单.xls";
            //string filename = "WritingFeeBill.xls";
            //string filepath = serverpath + filename;
            //ENDTODO

            ExcelHandle excel = new ExcelHandle();
            try
            {
                excel.Load(temppath);
                excel.CurSheet = (Excel._Worksheet)excel.CurBook.Sheets[1];
                ESP.Compatible.Employee emp = new ESP.Compatible.Employee(userid);
                if (dt.Rows.Count > 0)
                {
                    string username = emp.Name;
                    string userdepartmentname = emp.PositionDescription;
                    string projectcode = dt.Rows[0]["projectcode"].ToString();
                    ExcelHandle.WriteAfterMerge(excel.CurSheet, "A1", "P1", string.Format("记者发稿费用申请单      \r\n 项目号：{0}               组别：{1}         分机号：{2}            申请人：{3}                报销日期：", projectcode,userdepartmentname, emp.Telephone, username));
                }

                int rownum = 3;
                string cell = "A3";
                foreach (DataRow row in dt.Rows)
                {
                    //cell = string.Format("A{0}", rownum);
                    //ExcelHandle.WriteCell(excel.CurSheet, cell, row["projectcode"].ToString());

                    cell = string.Format("A{0}", rownum);
                    ExcelHandle.WriteCell(excel.CurSheet, cell, row["medianame"].ToString());

                    cell = string.Format("B{0}", rownum);
                    ExcelHandle.WriteCell(excel.CurSheet, cell, row["briefsubject"].ToString());

                    cell = string.Format("C{0}", rownum);
                    ExcelHandle.WriteCell(excel.CurSheet, cell, row["issuedate"].ToString().Split(' ')[0]);

                    cell = string.Format("D{0}", rownum);
                    ExcelHandle.WriteCell(excel.CurSheet, cell, row["wordscount"].ToString());

                    //cell = string.Format("F{0}", rownum);
                    //ExcelHandle.WriteCell(excel.CurSheet, cell, row["areawordscount"].ToString());

                    //cell = string.Format("G{0}", rownum);
                    //ExcelHandle.WriteCell(excel.CurSheet, cell, row["unitprice"].ToString());

                    cell = string.Format("E{0}", rownum);
                    ExcelHandle.WriteCell(excel.CurSheet, cell, row["amountprice"].ToString());

                    //cell = string.Format("I{0}", rownum);
                    //ExcelHandle.WriteCell(excel.CurSheet, cell, row["subtotal"].ToString());

                    cell = string.Format("F{0}", rownum);
                    ExcelHandle.WriteCell(excel.CurSheet, cell, row["linkmanname"].ToString());

                    cell = string.Format("G{0}", rownum);
                    ExcelHandle.WriteCell(excel.CurSheet, cell, row["recvmanname"].ToString());

                    cell = string.Format("H{0}", rownum);
                    ExcelHandle.WriteCell(excel.CurSheet, cell, row["cityname"].ToString());

                    cell = string.Format("I{0}", rownum);
                    ExcelHandle.WriteCell(excel.CurSheet, cell, row["bankname"].ToString());

                    cell = string.Format("J{0}", rownum);
                    ExcelHandle.WriteCell(excel.CurSheet, cell, row["bankcardcode"].ToString());

                    cell = string.Format("K{0}", rownum);
                    ExcelHandle.WriteCell(excel.CurSheet, cell, row["IDcardcode"].ToString());

                    cell = string.Format("L{0}", rownum);
                    ExcelHandle.WriteCell(excel.CurSheet, cell, row["phoneno"].ToString());

                    cell = string.Format("M{0}", rownum);
                    ExcelHandle.WriteCell(excel.CurSheet, cell, row["amountprice"].ToString());

                    cell = string.Format("N{0}", rownum);
                    ExcelHandle.WriteCell(excel.CurSheet, cell, row["ProjectAvgPrice"].ToString());

                    cell = string.Format("O{0}", rownum);
                    ExcelHandle.WriteCell(excel.CurSheet, cell, row["MediaAvgprice"].ToString());

                    cell = string.Format("P{0}",rownum);
                    ExcelHandle.WriteCell(excel.CurSheet, cell, row["isupload"].ToString());
                    rownum++;
                }

                //ExcelHandle.SetBorderAll(excel.CurSheet, "A4", cell);

                cell = string.Format("A{0}", rownum);
                ExcelHandle.WriteAfterMerge(excel.CurSheet, cell, string.Format("N{0}", rownum), "合计：");
                ExcelHandle.SetHAlignCenter(excel.CurSheet, cell);
                //ExcelHandle.SetBorderAll(excel.CurSheet, "A4", string.Format("N{0}", rownum));
                rownum++;

                cell = string.Format("A{0}", rownum);
                string signed = "  申请人签字:                       第一级批准人:                      第二级批准人:                       *第三级批准人:";
                ExcelHandle.WriteAfterMerge(excel.CurSheet, cell, string.Format("P{0}", rownum), signed);
                //ExcelHandle.SetBold(excel.CurSheet, cell, string.Format("P{0}", rownum));
                rownum++;

                cell = string.Format("A{0}", rownum);
                signed = "  日期:                             日期:                              日期:                               日期:";
                ExcelHandle.WriteAfterMerge(excel.CurSheet, cell, string.Format("P{0}", rownum), signed);
                //ExcelHandle.SetBold(excel.CurSheet, cell, string.Format("P{0}", rownum));
                rownum += 2;

                cell = string.Format("A{0}", rownum);
                signed = "  媒介中心:                                    采购部:                                     财务部:";
                ExcelHandle.WriteAfterMerge(excel.CurSheet, cell, string.Format("P{0}", rownum), signed);
                //ExcelHandle.SetBold(excel.CurSheet, cell, string.Format("P{0}", rownum));
                rownum++;

                cell = string.Format("A{0}", rownum);   
                signed = "  日期:                                        日期:                                       日期:";
                ExcelHandle.WriteAfterMerge(excel.CurSheet, cell, string.Format("P{0}", rownum), signed);
                //ExcelHandle.SetBold(excel.CurSheet, cell, string.Format("P{0}", rownum));
                rownum++;

                cell = string.Format("A{0}", rownum);
                ExcelHandle.WriteCell(excel.CurSheet, cell, "注：*第三级批准人在金额超过一定标准时才需签署");
                //ExcelHandle.SetColor(excel.CurSheet, cell, System.Drawing.Color.Red);

                ExcelHandle.SaveAS(excel.CurBook, serverpath+ofname);
                excel.Dispose();
                if (submitCreate)
                {
                    isDelete = false;//不用册除
                }
                else
                {
                    isDelete = true;
                }
                return serverpath+ofname;
            }
            catch
            {
                excel.Dispose();
                ofname =  "稿费报销明细单.xls";
                isDelete = false;
                return ConfigManager.BillPath + "稿费报销明细单.xls";
            }
        }
    }
}
