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
    /// ��ѱ�����
    /// </summary>
    public class WritingfeebillManager
    {
        /// <summary>
        /// ��Ӹ�ѱ�����
        /// </summary>
        /// <param name="bill">��ѱ�����</param>
        /// <param name="userid">������</param>
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
        /// �༭��ѱ�����
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
        /// ɾ����ѱ�����
        /// </summary>
        /// <param name="billid">��ѱ�����ID</param>
        /// <param name="userid">������</param>
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
        /// FP08070001 ������ˮ�Ż�ȡ
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
        /// ��ȡ��ѱ���������
        /// </summary>
        /// <param name="billid">��ѱ�����ID</param>
        /// <returns></returns>
        public static WritingfeebillInfo GetModel(int billid)
        {
            WritingfeebillInfo bill = ESP.Media.DataAccess.WritingfeebillDataProvider.Load(billid);
            if (bill == null)
                bill = new ESP.Media.Entity.WritingfeebillInfo();
            return bill;
        }

        /// <summary>
        /// ��ȡ��ѱ������б�
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
        /// ������ѱ�����EXCEL
        /// </summary>
        /// <param name="userid">������</param>
        /// <param name="id">��ѱ�����ID</param>
        /// <param name="serverpath">���·��</param>
        /// <param name="ofname">�ļ���</param>
        /// <param name="isDelete">�Ƿ���Ҫɾ��(������ɳɹ����������ʱ�ļ�����Ҫɾ�����������ʧ�ܵĻ����Ͳ���Ҫɾ��)</param>
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
            if (System.IO.File.Exists(serverpath + ofname))//�������ɾ��
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

            string temppath = serverpath + "��ѱ�����ϸ��.xls";
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
                    ExcelHandle.WriteAfterMerge(excel.CurSheet, "A1", "P1", string.Format("���߷���������뵥      \r\n ��Ŀ�ţ�{0}               ���{1}         �ֻ��ţ�{2}            �����ˣ�{3}                �������ڣ�", projectcode,userdepartmentname, emp.Telephone, username));
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
                ExcelHandle.WriteAfterMerge(excel.CurSheet, cell, string.Format("N{0}", rownum), "�ϼƣ�");
                ExcelHandle.SetHAlignCenter(excel.CurSheet, cell);
                //ExcelHandle.SetBorderAll(excel.CurSheet, "A4", string.Format("N{0}", rownum));
                rownum++;

                cell = string.Format("A{0}", rownum);
                string signed = "  ������ǩ��:                       ��һ����׼��:                      �ڶ�����׼��:                       *��������׼��:";
                ExcelHandle.WriteAfterMerge(excel.CurSheet, cell, string.Format("P{0}", rownum), signed);
                //ExcelHandle.SetBold(excel.CurSheet, cell, string.Format("P{0}", rownum));
                rownum++;

                cell = string.Format("A{0}", rownum);
                signed = "  ����:                             ����:                              ����:                               ����:";
                ExcelHandle.WriteAfterMerge(excel.CurSheet, cell, string.Format("P{0}", rownum), signed);
                //ExcelHandle.SetBold(excel.CurSheet, cell, string.Format("P{0}", rownum));
                rownum += 2;

                cell = string.Format("A{0}", rownum);
                signed = "  ý������:                                    �ɹ���:                                     ����:";
                ExcelHandle.WriteAfterMerge(excel.CurSheet, cell, string.Format("P{0}", rownum), signed);
                //ExcelHandle.SetBold(excel.CurSheet, cell, string.Format("P{0}", rownum));
                rownum++;

                cell = string.Format("A{0}", rownum);   
                signed = "  ����:                                        ����:                                       ����:";
                ExcelHandle.WriteAfterMerge(excel.CurSheet, cell, string.Format("P{0}", rownum), signed);
                //ExcelHandle.SetBold(excel.CurSheet, cell, string.Format("P{0}", rownum));
                rownum++;

                cell = string.Format("A{0}", rownum);
                ExcelHandle.WriteCell(excel.CurSheet, cell, "ע��*��������׼���ڽ���һ����׼ʱ����ǩ��");
                //ExcelHandle.SetColor(excel.CurSheet, cell, System.Drawing.Color.Red);

                ExcelHandle.SaveAS(excel.CurBook, serverpath+ofname);
                excel.Dispose();
                if (submitCreate)
                {
                    isDelete = false;//���ò��
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
                ofname =  "��ѱ�����ϸ��.xls";
                isDelete = false;
                return ConfigManager.BillPath + "��ѱ�����ϸ��.xls";
            }
        }
    }
}
