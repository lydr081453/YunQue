using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.IO;
using System.Collections;
using System.Data.SqlClient;
using ESP.Media.Access;
using ESP.Media.Entity;
using ESP.Media.Access.Utilities;

namespace ESP.Media.BusinessLogic
{
    public class ShortmsgManager
    {
        public static DataTable GetAllList()
        {
            return GetList(null, null);
        }

       public static DataTable GetListByMedia(string terms, Hashtable ht, int shortmsgId)
        {
            if (ht == null)
                ht = new Hashtable();
            if (terms == null)
                terms = string.Empty;
            terms +=  "  and id=@mid ";
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
            terms += " and del != @del order by a.id desc";
            if (!ht.ContainsKey("@del"))
            {
                ht.Add("@del", (int)Global.FiledStatus.Del);
            }
            return ESP.Media.DataAccess.ShortmsgDataProvider.QueryInfo(terms, ht);
        }

       public static ShortmsgInfo GetModel(int id)
        {
            return ESP.Media.DataAccess.ShortmsgDataProvider.Load(id);
        }

       public static int Add(ShortmsgInfo obj,int userid, out string errmsg)
        {
            using (SqlConnection conn = new SqlConnection(clsConfigOperate.CustomerSqlConnection()))
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();
                try
                {
                    errmsg = string.Empty;
                    int ret = ESP.Media.DataAccess.ShortmsgDataProvider.insertinfo(obj, trans);
                    OperatelogManager.add((int)Global.SysOperateType.Add, (int)Global.Tables.ShortMsg, userid, trans);//添加短信日志
                    obj.Id= ret;
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
    
       public static int Update(ShortmsgInfo obj,int userid, out string errmsg)
        {
            using (SqlConnection conn = new SqlConnection(clsConfigOperate.CustomerSqlConnection()))
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();
                try
                {
                    errmsg = string.Empty;
               
                    if (ESP.Media.DataAccess.ShortmsgDataProvider.updateInfo(trans, null, obj, string.Empty, null))
                    {
                        OperatelogManager.add((int)Global.SysOperateType.Edit, (int)Global.Tables.ShortMsg, userid, trans);//更新短信日志
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

       public static int Delete(ShortmsgInfo obj, out string errmsg)
        {
            errmsg = "删除成功!";
            try
            {
                obj.Del = (int)Global.FiledStatus.Del;
                if (ESP.Media.DataAccess.ShortmsgDataProvider.updateInfo(null, obj, string.Empty, null))
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
     
    }
}
