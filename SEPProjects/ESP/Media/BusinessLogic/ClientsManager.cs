using System;
using System.Collections.Generic;
using System.Data;
using System.Collections;
using System.Data.SqlClient;
using ESP.Media.DataAccess;
using ESP.Media.Entity;
using ESP.Media.Access.Utilities;

namespace ESP.Media.BusinessLogic
{
    public class ClientsManager
    {
        public ClientsManager()
        {

        }

        /// <summary>
        /// Gets all object list.
        /// </summary>
        /// <param name="term">The term.</param>
        /// <param name="ht">The ht.</param>
        /// <returns></returns>
        public static List<ClientsInfo> GetAllObjectList(string term, Hashtable ht)
        {
            DataTable dt = GetList(term, ht);
            var query = from client in dt.AsEnumerable() select ESP.Media.DataAccess.ClientsDataProvider.setObject(client);
            List<ClientsInfo> items = new List<ClientsInfo>();
            foreach (ClientsInfo item in query)
            {
                items.Add(item);
            }
            return items;
        }

        /// <summary>
        /// Gets all list.
        /// </summary>
        /// <returns></returns>
        public static DataTable GetAllList()
        {
            return GetList(null, null);
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
            if (terms.IndexOf("order by") < 0)
            {
                terms += " and del!=@del order by clientid desc";
            }
            else
                terms = " del!=@del and " + terms;

            ht.Add("@del", (int)Global.FiledStatus.Del);
            return ESP.Media.DataAccess.ClientsDataProvider.QueryInfo(terms, ht);
        }

        /// <summary>
        /// Gets the model.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns></returns>
        public static ClientsInfo GetModel(int id)
        {
            if (id <= 0) return new ClientsInfo();
            return ClientsDataProvider.Load(id);
        }

        /// <summary>
        /// Adds the specified obj.
        /// </summary>
        /// <param name="obj">The obj.</param>
        /// <param name="filename">The filename.</param>
        /// <param name="userid">The userid.</param>
        /// <param name="errmsg">The errmsg.</param>
        /// <returns></returns>
        public static int Add(ClientsInfo obj, string filename, int userid, out string errmsg)
        {
            using (SqlConnection conn = new SqlConnection(ESP.Media.Access.Utilities.clsConfigOperate.CustomerSqlConnection()))
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();
                string term = "(ClientCFullName= @ClientCFullName) AND del!=@del";
                SqlParameter[] param = new SqlParameter[3];
                param[0] = new SqlParameter("@ClientCFullName", SqlDbType.NVarChar);
                param[0].Value = obj.Clientcfullname;
                param[1] = new SqlParameter("@ClientCShortName", SqlDbType.NVarChar);
                param[1].Value = obj.Clientcshortname;
                param[2] = new SqlParameter("@del", SqlDbType.Int);
                param[2].Value = (int)Global.FiledStatus.Del;
                DataTable dt = ESP.Media.DataAccess.ClientsDataProvider.QueryInfo(trans, term, param);
                if (dt.Rows.Count > 0)
                {
                    errmsg = "客户中文名称已存在!";
                    trans.Rollback();
                    conn.Close();
                    return -1;
                }
                try
                {
                    errmsg = string.Empty;
                    if (!string.IsNullOrEmpty(filename) && filename.Length > 0)
                    {
                        obj.Clientlogo = filename.ToString();
                    }

                    obj.Currentversion = CommonManager.GetLastVersion("Client", obj.Clientid, trans);
                    int ret = ESP.Media.DataAccess.ClientsDataProvider.insertinfo(obj, trans);
                   //OperatelogManager.add((int)Global.SysOperateType.Add, (int)Global.Tables.clients, userid, trans);//添加客户日志
                    obj.Clientid = ret;
                    obj.Lastmodifiedbyuserid = userid;
                    obj.Lastmodifiedip = obj.Lastmodifiedip;
                    obj.Lastmodifieddate = DateTime.Now.ToString();
                    SaveHist(trans, obj, userid);
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
        /// Updates the specified obj.
        /// </summary>
        /// <param name="obj">The obj.</param>
        /// <param name="filename">The filename.</param>
        /// <param name="userid">The userid.</param>
        /// <param name="errmsg">The errmsg.</param>
        /// <returns></returns>
        public static int Update(ClientsInfo obj, string filename, int userid, out string errmsg)
        {
            using (SqlConnection conn = new SqlConnection(ESP.Media.Access.Utilities.clsConfigOperate.CustomerSqlConnection()))
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();
                try
                {
                    string term = "(ClientCFullName=@ClientCFullName) AND ClientID != @ClientID AND del!=@del";
                    SqlParameter[] param = new SqlParameter[4];
                    param[0] = new SqlParameter("@ClientCFullName", SqlDbType.NVarChar);
                    param[0].Value = obj.Clientcfullname;
                    param[1] = new SqlParameter("@ClientCShortName", SqlDbType.NVarChar);
                    param[1].Value = obj.Clientcshortname;
                    param[2] = new SqlParameter("@ClientID", SqlDbType.Int);
                    param[2].Value = obj.Clientid;
                    param[3] = new SqlParameter("@del", SqlDbType.Int);
                    param[3].Value = (int)Global.FiledStatus.Del;

                    DataTable dt = ESP.Media.DataAccess.ClientsDataProvider.QueryInfo(trans, term, param);
                    if (dt.Rows.Count > 0)
                    {
                        errmsg = "客户中文名称已存在!";
                        trans.Rollback();
                        conn.Close();
                        return -1;
                    }
                    errmsg = string.Empty;
                    if (!string.IsNullOrEmpty(filename) && filename.Length > 0)
                    {
                        obj.Clientlogo = filename.ToString();
                    }
                    obj.Currentversion = CommonManager.GetLastVersion("Client", obj.Clientid, trans);
                    if (ESP.Media.DataAccess.ClientsDataProvider.updateInfo(trans, null, obj, string.Empty, null))
                    {
                        obj.Lastmodifiedbyuserid = userid;
                        obj.Lastmodifiedip = obj.Lastmodifiedip;
                        obj.Lastmodifieddate = DateTime.Now.ToString();
                        SaveHist(trans, obj, userid);
                       //OperatelogManager.add((int)Global.SysOperateType.Edit, (int)Global.Tables.clients, userid, trans);//更新客户日志
                        trans.Commit();
                        conn.Close();
                        return 1;
                    }
                    else
                    {
                        errmsg = "修改失败!";
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

        /// <summary>
        /// Deletes the specified obj.
        /// </summary>
        /// <param name="obj">The obj.</param>
        /// <param name="errmsg">The errmsg.</param>
        /// <returns></returns>
        public static int Delete(ClientsInfo obj, out string errmsg)
        {
            errmsg = "删除成功!";
            try
            {
                obj.Del = (int)Global.FiledStatus.Del;
                if (ESP.Media.DataAccess.ClientsDataProvider.updateInfo(null, obj, string.Empty, null))
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

        /// <summary>
        /// Gets the hist list by client ID.
        /// </summary>
        /// <param name="clientID">The client ID.</param>
        /// <returns></returns>
        public static DataTable GetHistListByClientID(int clientID)
        {
            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@id", SqlDbType.Int);
            param[0].Value = clientID;
            param[1] = new SqlParameter("@del", SqlDbType.Int);
            param[1].Value = (int)Global.FiledStatus.Del;
            return ESP.Media.DataAccess.ClientshistDataProvider.QueryInfo("del!=@del and ClientID=@id order by a.Clientid desc", param);
        }

        /// <summary>
        /// Gets the hist model.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns></returns>
        public static ClientshistInfo GetHistModel(int id)
        {
            if (id <= 0) return new ClientshistInfo();
            return ESP.Media.DataAccess.ClientshistDataProvider.Load(id);
        }

        /// <summary>
        /// Saves the hist.
        /// </summary>
        /// <param name="trans">The trans.</param>
        /// <param name="obj">The obj.</param>
        /// <param name="userid">The userid.</param>
        private static void SaveHist(SqlTransaction trans, ClientsInfo obj, int userid)
        {
            ClientshistInfo hist = new ClientshistInfo();
            hist.Lastmodifiedbyuserid = userid;
            hist.Lastmodifiedip = obj.Lastmodifiedip;
            hist.Lastmodifieddate = DateTime.Now.ToString();
            hist.Clientcfullname = obj.Clientcfullname;
            hist.Clientcshortname = obj.Clientcshortname;
            hist.Clientdescription = obj.Clientdescription;
            hist.Clientefullname = obj.Clientefullname;
            hist.Clienteshortname = obj.Clienteshortname;
            hist.Clientid = obj.Clientid;
            hist.Clientlogo = obj.Clientlogo;
            hist.Createdbyuserid = obj.Lastmodifiedbyuserid;
            hist.Createddate = obj.Lastmodifieddate;
            hist.Createdip = obj.Lastmodifiedip;
            hist.Status = obj.Status;
            hist.Version = CommonManager.GetLastVersion("Client", obj.Clientid, trans);
            ESP.Media.DataAccess.ClientshistDataProvider.insertinfo(hist, trans);
        }
    }
}
