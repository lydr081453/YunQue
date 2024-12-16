using System;
using System.Data;
using System.Collections;
using System.Data.SqlClient;
using ESP.Media.Entity;
using ESP.Media.Access.Utilities;

namespace ESP.Media.BusinessLogic
{
    public class ShortmsgsendManager
    {
        /// <summary>
        /// Gets all list.
        /// </summary>
        /// <returns></returns>
        public static DataTable GetAllList()
        {
            return GetList(null, null);
        }

        /// <summary>
        /// Gets the list by media.
        /// </summary>
        /// <param name="terms">The terms.</param>
        /// <param name="ht">The ht.</param>
        /// <param name="shortmsgId">The shortmsg id.</param>
        /// <returns></returns>
        public static DataTable GetListByMedia(string terms, Hashtable ht, int shortmsgId)
        {
            if (ht == null)
                ht = new Hashtable();
            if (terms == null)
                terms = string.Empty;
            terms = terms + " and id=@mid ";
            if (!ht.ContainsKey("@mid"))
            {
                ht.Add("@mid", shortmsgId);
            }
            return GetList(terms, ht);
        }

        /// <summary>
        /// Gets the list.
        /// </summary>
        /// <param name="terms">The terms.</param>
        /// <param name="ht">The ht.</param>
        /// <returns></returns>
        public static DataTable GetList(string terms, Hashtable ht)
        {
            if (ht == null)
                ht = new Hashtable();
            if (terms == null)
                terms = string.Empty;
            terms += " and del != @del order by id desc";
            if (!ht.ContainsKey("@del"))
            {
                ht.Add("@del", (int)Global.FiledStatus.Del);
            }
            return ESP.Media.DataAccess.ShortmsgsendDataProvider.QueryInfo(terms, ht);
        }

        /// <summary>
        /// Gets the model.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns></returns>
        public static ShortmsgsendInfo GetModel(int id)
        {
            return ESP.Media.DataAccess.ShortmsgsendDataProvider.Load(id);
        }

        /// <summary>
        /// Adds the specified obj.
        /// </summary>
        /// <param name="obj">The obj.</param>
        /// <param name="userid">The userid.</param>
        /// <param name="errmsg">The errmsg.</param>
        /// <returns></returns>
        public static int Add(ShortmsgsendInfo obj,int userid, out string errmsg)
        {
            using (SqlConnection conn = new SqlConnection(ESP.Media.Access.Utilities.clsConfigOperate.CustomerSqlConnection()))
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();
                try
                {
                    errmsg = string.Empty;
                    int ret = ESP.Media.DataAccess.ShortmsgsendDataProvider.insertinfo(obj, trans);
                    OperatelogManager.add((int)Global.SysOperateType.Add, (int)Global.Tables.SendShortMsg, userid, trans);//发送短信日志
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

        /// <summary>
        /// Add_s the update daily status.
        /// </summary>
        /// <param name="obj">The obj.</param>
        /// <param name="userid">The userid.</param>
        /// <param name="dailyid">The dailyid.</param>
        /// <param name="errmsg">The errmsg.</param>
        /// <returns></returns>
        public static int Add_UpdateDailyStatus(ShortmsgsendInfo obj, int userid, int dailyid, out string errmsg)
        {
            using (
                SqlConnection conn =
                    new SqlConnection(ESP.Media.Access.Utilities.clsConfigOperate.CustomerSqlConnection()))
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();
                try
                {
                    errmsg = string.Empty;
                    int ret = ESP.Media.DataAccess.ShortmsgsendDataProvider.insertinfo(obj, trans);
                    OperatelogManager.add((int) Global.SysOperateType.Add, (int) Global.Tables.SendEmail, userid, trans);
                        //发送邮件日志
                    obj.Id = ret;
                    DailysInfo evt = ESP.Media.BusinessLogic.DailysManager.GetModel(dailyid);
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

        /// <summary>
        /// Add_s the update event status.
        /// </summary>
        /// <param name="obj">The obj.</param>
        /// <param name="userid">The userid.</param>
        /// <param name="eventid">The eventid.</param>
        /// <param name="errmsg">The errmsg.</param>
        /// <returns></returns>
        public static int Add_UpdateEventStatus(ShortmsgsendInfo obj, int userid, int eventid, out string errmsg)
        {
            using (
                SqlConnection conn =
                    new SqlConnection(ESP.Media.Access.Utilities.clsConfigOperate.CustomerSqlConnection()))
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();
                try
                {
                    errmsg = string.Empty;
                    int ret = ESP.Media.DataAccess.ShortmsgsendDataProvider.insertinfo(obj, trans);
                    OperatelogManager.add((int) Global.SysOperateType.Add, (int) Global.Tables.SendEmail, userid, trans);
                        //发送邮件日志
                    obj.Id = ret;
                    EventsInfo evt = ESP.Media.BusinessLogic.EventsManager.GetModel(eventid);
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

        /// <summary>
        /// Deletes the specified obj.
        /// </summary>
        /// <param name="obj">The obj.</param>
        /// <param name="errmsg">The errmsg.</param>
        /// <returns></returns>
        public static int Delete(ShortmsgsendInfo obj, out string errmsg)
        {
            errmsg = "删除成功!";
            try
            {
                obj.Del = (int) Global.FiledStatus.Del;
                if (ESP.Media.DataAccess.ShortmsgsendDataProvider.updateInfo(null, obj, string.Empty, null))
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
