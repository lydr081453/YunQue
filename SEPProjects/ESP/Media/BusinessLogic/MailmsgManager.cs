using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.IO;
using System.Collections;
using System.Data.SqlClient;
using ESP.Compatible;
using ESP.Media.Access;
using ESP.Media.Entity;
using ESP.Framework.BusinessLogic;
using ESP.Framework.Entity;
using ESP.Media.Access.Utilities;

namespace ESP.Media.BusinessLogic
{
    public struct MailAnnexManager
    {
        public string AnnexFileName;//邮件附件文件名
        public byte[] AnnexFileData;//邮件附件文件内容
       
    }

    public  class MailmsgManager
    {
        public static DataTable GetAllList()
        {
            return GetList(null, null);
        }

        public static DataTable GetList(string terms, Hashtable ht, int shortmsgId)
        {
            if (ht == null)
                ht = new Hashtable();
            if (terms == null)
                terms = string.Empty;
            terms = terms + " and a.id=@mid ";
            if (!ht.ContainsKey("@mid"))
            {
                ht.Add("@mid", shortmsgId);
            }
            return GetList(terms, ht);
        }

        public static DataTable GetList(string terms, Hashtable ht)
        {
            if (ht == null)
                ht = new Hashtable();
            if (terms == null)
                terms = string.Empty;
            terms = terms + " and del != @del order by a.id desc";
            if (!ht.ContainsKey("@del"))
            {
                ht.Add("@del", (int)Global.FiledStatus.Del);
            }
            return ESP.Media.DataAccess.MailmsgDataProvider.QueryInfo(terms, ht);
        }

        public static MailmsgInfo GetModel(int id)
        {
            return ESP.Media.DataAccess.MailmsgDataProvider.Load(id);
        }

        public static int Add(MailmsgInfo obj, MailAnnexManager annex, int userid, out string errmsg)
        {
            
            using (SqlConnection conn = new SqlConnection(ESP.Media.Access.Utilities.clsConfigOperate.CustomerSqlConnection()))
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();
                try
                {
                    string filename = SaveMailAnnex(annex);
                    obj.Attachmentspath = filename;
                    errmsg = string.Empty;
                    int ret = ESP.Media.DataAccess.MailmsgDataProvider.insertinfo(obj, trans);
                    OperatelogManager.add((int)Global.SysOperateType.Add, (int)Global.Tables.EMail, userid, trans);//添加Email编辑日志
                    obj.Id = ret;
                    trans.Commit();
                    conn.Close();
                    return ret;
                }
                catch (Exception exception)
                {
                    trans.Rollback();
                    conn.Close();
                    errmsg = exception.Message;
                    return -2;
                }
            }
        }

        public static int Update(MailmsgInfo obj, MailAnnexManager annex, int userid, out string errmsg)
        {
            using (SqlConnection conn = new SqlConnection(ESP.Media.Access.Utilities.clsConfigOperate.CustomerSqlConnection()))
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();
                try
                {
                    errmsg = string.Empty;

                    string filename = SaveMailAnnex(annex);
                    if (filename != "")
                    {
                        obj.Attachmentspath = filename; 
                    }
                    

                    if (ESP.Media.DataAccess.MailmsgDataProvider.updateInfo(trans, null, obj, string.Empty, null))
                    {
                        OperatelogManager.add((int)Global.SysOperateType.Edit, (int)Global.Tables.EMail, userid, trans);//更新Email日志
                        trans.Commit();
                        conn.Close();
                        return 1;
                    }
                    else
                    {
                        errmsg = "修改失败!";
                        trans.Rollback();
                        conn.Close();
                        return -3;
                    }
                }
                catch (Exception exception)
                {
                    trans.Rollback();
                    conn.Close();
                    errmsg = exception.Message;
                    return -2;
                }
            }
        }

        public static int Delete(MailmsgInfo obj, out string errmsg)
        {
            errmsg = "删除成功!";
            try
            {
                obj.Del = (int)Global.FiledStatus.Del;
                if (ESP.Media.DataAccess.MailmsgDataProvider.updateInfo(null, obj, string.Empty, null))
                {
                    return 1;
                }
                else
                {
                    errmsg = "删除失败!";
                    return -3;
                }
            }
            catch (Exception exception)
            {
                errmsg = exception.Message;
                return -2;
            }
        }

        private static string SaveMailAnnex(MailAnnexManager annex) 
        {
            string filename = "";
            if (annex.AnnexFileData != null && annex.AnnexFileName != "")
            {
                filename = CommonManager.SaveFile(ESP.Media.Access.Utilities.ConfigManager.MailAttachmentPath,annex.AnnexFileName,annex.AnnexFileData,true);
 
            }            
            return filename;

        }

        public static DataTable GetCreaterList()
        {
            IList<EmployeeInfo> list = EmployeeManager.GetAll();

            DataTable dt2 = new DataTable();
            dt2.Columns.Add(new DataColumn("SysUserID"));
            dt2.Columns.Add(new DataColumn("UserName"));

            if (list == null || list.Count == 0)
                return dt2;

            for (int i = 0; i < list.Count; i++)
            {
                DataRow dr = dt2.NewRow();
                dr[0] = list[i].UserID;
                dr[1] = list[i].FullNameCN;
                dt2.Rows.Add(dr);
            }
            return dt2;

        }

        public static DataTable GetCreater(string uname)
        {
            IList<EmployeeInfo> list = EmployeeManager.SearchByChineseName(uname);

            DataTable dt2 = new DataTable();
            dt2.Columns.Add(new DataColumn("SysUserID"));
            dt2.Columns.Add(new DataColumn("UserName"));

            if (list == null || list.Count == 0)
                return dt2;

            for (int i = 0; i < list.Count; i++)
            {
                DataRow dr = dt2.NewRow();
                dr[0] = list[i].UserID;
                dr[1] = list[i].FullNameCN;
                dt2.Rows.Add(dr);
            }
            return dt2;

        }

    }
}