using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Collections;
using System.IO;
using System.Data.SqlClient;
using ESP.Compatible;
using ESP.Media.Access;
using ESP.Media.Entity;
using ESP.Media.Access.Utilities;

namespace ESP.Media.BusinessLogic
{
    public class EmailsendManager
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
            return ESP.Media.DataAccess.EmailsendDataProvider.QueryInfo(terms, ht);
        }

      public static EmailsendInfo GetModel(int id)
        {
            return ESP.Media.DataAccess.EmailsendDataProvider.Load(id);
        }

      public static int Add(EmailsendInfo obj, int userid, out string errmsg)
        {
            using (SqlConnection conn = new SqlConnection(ESP.Media.Access.Utilities.clsConfigOperate.CustomerSqlConnection()))
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();
                try
                {
                    errmsg = string.Empty;
                    
                    int ret = ESP.Media.DataAccess.EmailsendDataProvider.insertinfo(obj, trans);
                    OperatelogManager.add((int)Global.SysOperateType.Add, (int)Global.Tables.SendEmail, userid, trans);//发送邮件日志
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


      public static int Add_UpdateDailyStatus(EmailsendInfo obj, int userid, int dailyid, out string errmsg)
      {
          using (SqlConnection conn = new SqlConnection(ESP.Media.Access.Utilities.clsConfigOperate.CustomerSqlConnection()))
          {
              conn.Open();
              SqlTransaction trans = conn.BeginTransaction();
              try
              {
                  errmsg = string.Empty;
                                   
                  int ret = ESP.Media.DataAccess.EmailsendDataProvider.insertinfo(obj, trans);
                  OperatelogManager.add((int)Global.SysOperateType.Add, (int)Global.Tables.SendEmail, userid, trans);//发送邮件日志
                  obj.Id = ret;
                  ESP.Media.Entity.DailysInfo evt = ESP.Media.BusinessLogic.DailysManager.GetModel(dailyid);
                  if (evt != null)
                  {
                      //int status = evt.Dailystatus > 0 ? evt.Dailystatus + 1 : 0;
                      //已经发送邀请 = 2
                      ESP.Media.BusinessLogic.DailysManager.SetStatus(dailyid, 2, trans);

                  }
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

      public static int Add_UpdateEventStatus(EmailsendInfo obj, int userid, int eventid, out string errmsg)
      {
          using (SqlConnection conn = new SqlConnection(ESP.Media.Access.Utilities.clsConfigOperate.CustomerSqlConnection()))
          {
              conn.Open();
              SqlTransaction trans = conn.BeginTransaction();
              try
              {
                  errmsg = string.Empty;
                                   
                  int ret = ESP.Media.DataAccess.EmailsendDataProvider.insertinfo(obj, trans);
                  OperatelogManager.add((int)Global.SysOperateType.Add, (int)Global.Tables.SendEmail, userid, trans);//发送邮件日志
                  obj.Id = ret;
                  ESP.Media.Entity.EventsInfo evt = ESP.Media.BusinessLogic.EventsManager.GetModel(eventid);
                  if (evt != null)
                  {
                      //int status = evt.Dailystatus > 0 ? evt.Dailystatus + 1 : 0;
                      //已经发送邀请 = 2
                      ESP.Media.BusinessLogic.EventsManager.SetStatus(eventid, 2, trans);

                  }
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



      public static int Delete(EmailsendInfo obj, out string errmsg)
        {
            errmsg = "删除成功!";
            try
            {
                obj.Del = (int)Global.FiledStatus.Del;
                if (ESP.Media.DataAccess.EmailsendDataProvider.updateInfo(null, obj, string.Empty, null))
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
