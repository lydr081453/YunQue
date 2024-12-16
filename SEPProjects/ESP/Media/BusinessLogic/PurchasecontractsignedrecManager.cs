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
    public class PurchasecontractsignedrecManager
    {
        //��Ӳɹ���ͬǩ�ּ�¼
        public static int add(PurchasecontractsignedrecInfo obj, int userid)
        {
            using (SqlConnection conn = new SqlConnection(ESP.Media.Access.Utilities.clsConfigOperate.CustomerSqlConnection()))
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();
                if (obj.Createdate == string.Empty)
                    obj.Createdate = DateTime.Now.ToString();
                obj.Del = (int)Global.FiledStatus.Usable;
                try
                {
                    int id = ESP.Media.DataAccess.PurchasecontractsignedrecDataProvider.insertinfo(obj, trans);
                    obj.Id = id;
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

        //�޸Ĳɹ���ͬǩ�ּ�¼
        public static bool modify(PurchasecontractsignedrecInfo obj, int userid)
        {
            using (SqlConnection conn = new SqlConnection(ESP.Media.Access.Utilities.clsConfigOperate.CustomerSqlConnection()))
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();
                try
                {
                    bool res = ESP.Media.DataAccess.PurchasecontractsignedrecDataProvider.updateInfo(trans, null, obj, null, null);
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


        //ɾ���ɹ���ͬǩ�ּ�¼
        public static bool del(int recid, int userid)
        {
            PurchasecontractsignedrecInfo rec = ESP.Media.DataAccess.PurchasecontractsignedrecDataProvider.Load(recid);
            if (rec == null) return false;
            using (SqlConnection conn = new SqlConnection(ESP.Media.Access.Utilities.clsConfigOperate.CustomerSqlConnection()))
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();
                try
                {
                    rec.Del = (int)Global.FiledStatus.Del;
                    bool res = ESP.Media.DataAccess.PurchasecontractsignedrecDataProvider.updateInfo(trans, null, rec, null, null);
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


        public static PurchasecontractsignedrecInfo GetModel(int recid)
        {
            return ESP.Media.DataAccess.PurchasecontractsignedrecDataProvider.Load(recid);
        }

        public static DataTable GetList(string term, Hashtable ht)
        {
            if (term == null)
                term = string.Empty;
            if (ht == null)
                ht = new Hashtable();
            term += " and del != @del order by a.id desc";
            if (!ht.ContainsKey("@del"))
                ht.Add("@del", (int)Global.FiledStatus.Del);
            return ESP.Media.DataAccess.PurchasecontractsignedrecDataProvider.QueryInfo(term, ht);
        }

        public static string GetPurchaseContractSigned(int id, string serverpath)
        {
            #region getinfo
            PurchasecontractsignedrecInfo model = GetModel(id);
            string body = model.Contractbody;
            double amount = model.Contractamount;

            if (!Directory.Exists(serverpath))
                Directory.CreateDirectory(serverpath);

            string filename =  "PurchaseContractSigned.doc";
            string filepath = serverpath + filename;
            #endregion

            WordClass word = new WordClass();
            word.CreateDocument();

            word.WordApplication.Selection.Font.Size = 22;
            word.WordApplication.Selection.Font.Bold = 1;
            word.WordApplication.Selection.Range.ParagraphFormat.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphCenter;
            word.AppendText("");
            word.AppendText("");
            word.AppendText("�ɹ���ͬǩ���¼");
            word.AppendText("");

            word.WordApplication.Selection.Font.Size = 16;
            word.WordApplication.Selection.Font.Bold = 1;
            word.WordApplication.Selection.Range.ParagraphFormat.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft;
            word.AppendTextNoParagraph("��ͬ���ݣ�");
            word.WordApplication.Selection.Font.Underline = Microsoft.Office.Interop.Word.WdUnderline.wdUnderlineSingle;
            word.AppendText(body);
            word.AppendText("");

            word.WordApplication.Selection.Font.Underline = Microsoft.Office.Interop.Word.WdUnderline.wdUnderlineNone;
            word.AppendTextNoParagraph("��ͬ��");
            word.WordApplication.Selection.Font.Underline = Microsoft.Office.Interop.Word.WdUnderline.wdUnderlineSingle;
            word.AppendText("��" + amount);
            word.AppendText("");

            word.WordApplication.Selection.Font.Underline = Microsoft.Office.Interop.Word.WdUnderline.wdUnderlineNone;
            word.AppendText("ǩ�֣� ____________________       ���ڣ�_____________");
            word.AppendText("");
            word.AppendText("ǩ�֣� ____________________       ���ڣ�_____________");
            word.AppendText("");
            word.AppendText("ǩ�֣� ____________________       ���ڣ�_____________");
            word.AppendText("");
            word.AppendText("ǩ�֣� ____________________       ���ڣ�_____________");
            word.AppendText("");

            word.WordApplication.Selection.Font.Size = 12;
            word.AppendText("˵����");
            word.AppendText("");
            word.WordApplication.Selection.Font.Bold = 0;
            word.AppendText("    ����һ�������ͬ������ǩ��ֻӦ����˾���մ�����ǩ������Ϊһ��ǩ���ˡ�����˹��ú�ͬ�ĸ����쵼, ����ʽ��ͬ����������ǩ�����ϰ��ո�����˵�ǰ�����ǩ�֣���������˲�ͨ���ú�ͬ��лл��");
            word.SaveAs(filepath);
            word.Dispose();
            return ESP.Media.Access.Utilities.ConfigManager.BillPath + filename;
        }
    }
}
