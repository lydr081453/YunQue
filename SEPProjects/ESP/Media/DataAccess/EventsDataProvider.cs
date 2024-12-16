using System;
using System.Data;
using System.Data.SqlClient;
using ESP.Media.Entity;
using System.Collections;
using ESP.Media.Access.Utilities;
using System.Collections.Generic;
namespace ESP.Media.DataAccess
{
    public class EventsDataProvider
    {
        #region 构造函数
        public EventsDataProvider()
        {
        }
        #endregion
        #region 插入
        //插入字符串
        private static string strinsert(EventsInfo obj, ref List<SqlParameter> ht)
        {
            if (ht == null)
            {
                ht = new List<SqlParameter>();
            }
            string sql = @"insert into media_events (projectid,createdbyuserid,createdip,createddate,del,eventname,eventcontent,eventstarttime,eventstatus) values (@projectid,@createdbyuserid,@createdip,@createddate,@del,@eventname,@eventcontent,@eventstarttime,@eventstatus);select @@IDENTITY as rowNum;";
            SqlParameter param_Projectid = new SqlParameter("@Projectid", SqlDbType.Int, 4);
            param_Projectid.Value = obj.Projectid;
            ht.Add(param_Projectid);
            SqlParameter param_Createdbyuserid = new SqlParameter("@Createdbyuserid", SqlDbType.Int, 4);
            param_Createdbyuserid.Value = obj.Createdbyuserid;
            ht.Add(param_Createdbyuserid);
            SqlParameter param_Createdip = new SqlParameter("@Createdip", SqlDbType.NVarChar, 512);
            param_Createdip.Value = obj.Createdip;
            ht.Add(param_Createdip);
            SqlParameter param_Createddate = new SqlParameter("@Createddate", SqlDbType.DateTime, 8);
            param_Createddate.Value = ESP.Media.Access.Utilities.Common.StringToDateTime(obj.Createddate);
            ht.Add(param_Createddate);
            SqlParameter param_Del = new SqlParameter("@Del", SqlDbType.SmallInt, 2);
            param_Del.Value = obj.Del;
            ht.Add(param_Del);
            SqlParameter param_Eventname = new SqlParameter("@Eventname", SqlDbType.NVarChar, 100);
            param_Eventname.Value = obj.Eventname;
            ht.Add(param_Eventname);
            SqlParameter param_Eventcontent = new SqlParameter("@Eventcontent", SqlDbType.NVarChar, 1000);
            param_Eventcontent.Value = obj.Eventcontent;
            ht.Add(param_Eventcontent);
            SqlParameter param_Eventstarttime = new SqlParameter("@Eventstarttime", SqlDbType.SmallDateTime, 4);
            param_Eventstarttime.Value = ESP.Media.Access.Utilities.Common.StringToDateTime(obj.Eventstarttime);
            ht.Add(param_Eventstarttime);
            SqlParameter param_Eventstatus = new SqlParameter("@Eventstatus", SqlDbType.Int, 4);
            param_Eventstatus.Value = obj.Eventstatus;
            ht.Add(param_Eventstatus);
            return sql;
        }


        //插入一条记录
        public static int insertinfo(EventsInfo obj, SqlTransaction trans)
        {
            if (obj == null)
            {
                return 0;
            }
            List<SqlParameter> ht = new List<SqlParameter>();
            string sql = strinsert(obj, ref ht);
            SqlParameter[] param = ht.ToArray();
            int rowNum = 0;
            rowNum = Convert.ToInt32(SqlHelper.ExecuteDataset(trans, CommandType.Text, sql, param).Tables[0].Rows[0]["rowNum"]);
            return rowNum;
        }


        //插入一条记录
        public static int insertinfo(EventsInfo obj)
        {
            if (obj == null)
            {
                return 0;
            }
            List<SqlParameter> ht = new List<SqlParameter>();
            string sql = strinsert(obj, ref ht);
            SqlParameter[] param = ht.ToArray();
            int rowNum = 0;
            using (SqlConnection conn = new SqlConnection(clsConfigOperate.CustomerSqlConnection()))
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();
                try
                {
                    rowNum = Convert.ToInt32(SqlHelper.ExecuteDataset(trans, CommandType.Text, sql, param).Tables[0].Rows[0]["rowNum"]);
                    trans.Commit();
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    throw new Exception(ex.Message);
                }
                finally
                {
                    conn.Close();
                }
            }
            return rowNum;
        }
        #endregion
        #region 删除
        //删除操作
        public static bool DeleteInfo(int id, SqlTransaction trans)
        {
            int rows = 0;
            string sql = "delete media_events where EventID=@id";
            SqlParameter param = new SqlParameter("@id", SqlDbType.Int);
            param.Value = id;
            rows = SqlHelper.ExecuteNonQuery(trans, CommandType.Text, sql, param);
            if (rows > 0)
            {
                return true;
            }
            return false;
        }


        //删除操作
        public static bool DeleteInfo(int id)
        {
            using (SqlConnection conn = new SqlConnection(clsConfigOperate.CustomerSqlConnection()))
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();
                try
                {
                    if (DeleteInfo(id, trans))
                    {
                        trans.Commit();
                        return true;
                    }
                    else
                    {
                        trans.Rollback();
                    }
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    throw new Exception(ex.Message);
                }
                finally
                {
                    conn.Close();
                }
            }
            return false;
        }
        #endregion
        #region 更新
        //更新sql
        public static string getUpdateString(EventsInfo objTerm, EventsInfo Objupdate, string term, ref List<SqlParameter> ht, params SqlParameter[] param)
        {
            string sql = "update media_events set projectid=@projectid,createdbyuserid=@createdbyuserid,createdip=@createdip,createddate=@createddate,del=@del,eventname=@eventname,eventcontent=@eventcontent,eventstarttime=@eventstarttime,eventstatus=@eventstatus where 1=1 ";
            SqlParameter param_projectid = new SqlParameter("@projectid", SqlDbType.Int, 4);
            param_projectid.Value = Objupdate.Projectid;
            ht.Add(param_projectid);
            SqlParameter param_createdbyuserid = new SqlParameter("@createdbyuserid", SqlDbType.Int, 4);
            param_createdbyuserid.Value = Objupdate.Createdbyuserid;
            ht.Add(param_createdbyuserid);
            SqlParameter param_createdip = new SqlParameter("@createdip", SqlDbType.NVarChar, 512);
            param_createdip.Value = Objupdate.Createdip;
            ht.Add(param_createdip);
            SqlParameter param_createddate = new SqlParameter("@createddate", SqlDbType.DateTime, 8);
            param_createddate.Value = ESP.Media.Access.Utilities.Common.StringToDateTime(Objupdate.Createddate);
            ht.Add(param_createddate);
            SqlParameter param_del = new SqlParameter("@del", SqlDbType.SmallInt, 2);
            param_del.Value = Objupdate.Del;
            ht.Add(param_del);
            SqlParameter param_eventname = new SqlParameter("@eventname", SqlDbType.NVarChar, 100);
            param_eventname.Value = Objupdate.Eventname;
            ht.Add(param_eventname);
            SqlParameter param_eventcontent = new SqlParameter("@eventcontent", SqlDbType.NVarChar, 1000);
            param_eventcontent.Value = Objupdate.Eventcontent;
            ht.Add(param_eventcontent);
            SqlParameter param_eventstarttime = new SqlParameter("@eventstarttime", SqlDbType.SmallDateTime, 4);
            param_eventstarttime.Value = ESP.Media.Access.Utilities.Common.StringToDateTime(Objupdate.Eventstarttime);
            ht.Add(param_eventstarttime);
            SqlParameter param_eventstatus = new SqlParameter("@eventstatus", SqlDbType.Int, 4);
            param_eventstatus.Value = Objupdate.Eventstatus;
            ht.Add(param_eventstatus);
            if (objTerm == null && (term == null || term.Trim().Length <= 0))
            {
                sql += " and EventID=@id ";
                if (ESP.Media.Access.Utilities.Common.Find(ht, "@id") == -1)
                {
                    SqlParameter p = new SqlParameter("@id", SqlDbType.Int, 4);
                    p.Value = Objupdate.Eventid;
                    ht.Add(p);
                }

            }
            if (objTerm != null)
            {
                sql += getTerms(objTerm, ref ht);
            }
            if (term != null && term.Trim().Length > 0)
            {
                sql += term;
            }
            if (param != null && param.Length > 0)
            {
                for (int i = 0; i < param.Length; i++)
                {
                    if (ESP.Media.Access.Utilities.Common.Find(ht, param[i].ParameterName) == -1)
                    {
                        ht.Add(param[i]);
                    }
                }
            }
            return sql;
        }


        //更新操作
        public static bool updateInfo(SqlTransaction trans, EventsInfo objterm, EventsInfo Objupdate, string term, params SqlParameter[] param)
        {
            if (Objupdate == null)
            {
                return false;
            }
            List<SqlParameter> ht = new List<SqlParameter>();
            string sql = getUpdateString(objterm, Objupdate, term, ref ht, param);
            SqlParameter[] para = ht.ToArray();
            int rows = SqlHelper.ExecuteNonQuery(trans, CommandType.Text, sql, para);
            if (rows >= 0)
            {
                return true;
            }
            return false;
        }


        //更新操作
        public static bool updateInfo(EventsInfo objterm, EventsInfo Objupdate, string term, params SqlParameter[] param)
        {
            if (Objupdate == null)
            {
                return false;
            }
            List<SqlParameter> ht = new List<SqlParameter>();
            string sql = getUpdateString(objterm, Objupdate, term, ref ht, param);
            SqlParameter[] para = ht.ToArray();
            int rowNum = 0;
            using (SqlConnection conn = new SqlConnection(clsConfigOperate.CustomerSqlConnection()))
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();
                try
                {
                    rowNum = SqlHelper.ExecuteNonQuery(trans, CommandType.Text, sql, para);
                    trans.Commit();
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    throw new Exception(ex.Message);
                }
                finally
                {
                    conn.Close();
                }
            }
            if (rowNum >= 0)
            {
                return true;
            }
            return false;
        }


        private static string getTerms(EventsInfo obj, ref List<SqlParameter> ht)
        {
            string term = string.Empty;
            if (obj.Eventid > 0)//EventID
            {
                term += " and eventid=@eventid ";
                if (ESP.Media.Access.Utilities.Common.Find(ht, "@eventid") == -1)
                {
                    SqlParameter p = new SqlParameter("@eventid", SqlDbType.Int, 4);
                    p.Value = obj.Eventid;
                    ht.Add(p);
                }
            }
            if (obj.Projectid > 0)//ProjectID
            {
                term += " and projectid=@projectid ";
                if (ESP.Media.Access.Utilities.Common.Find(ht, "@projectid") == -1)
                {
                    SqlParameter p = new SqlParameter("@projectid", SqlDbType.Int, 4);
                    p.Value = obj.Projectid;
                    ht.Add(p);
                }
            }
            if (obj.Createdbyuserid > 0)//CreatedByUserID
            {
                term += " and createdbyuserid=@createdbyuserid ";
                if (ESP.Media.Access.Utilities.Common.Find(ht, "@createdbyuserid") == -1)
                {
                    SqlParameter p = new SqlParameter("@createdbyuserid", SqlDbType.Int, 4);
                    p.Value = obj.Createdbyuserid;
                    ht.Add(p);
                }
            }
            if (obj.Createdip != null && obj.Createdip.Trim().Length > 0)
            {
                term += " and createdip=@createdip ";
                if (ESP.Media.Access.Utilities.Common.Find(ht, "@createdip") == -1)
                {
                    SqlParameter p = new SqlParameter("@createdip", SqlDbType.NVarChar, 512);
                    p.Value = obj.Createdip;
                    ht.Add(p);
                }
            }
            if (obj.Createddate != null && obj.Createddate.Trim().Length > 0)
            {
                term += " and createddate=@createddate ";
                if (ESP.Media.Access.Utilities.Common.Find(ht, "@createddate") == -1)
                {
                    SqlParameter p = new SqlParameter("@createddate", SqlDbType.DateTime, 8);
                    p.Value = ESP.Media.Access.Utilities.Common.StringToDateTime(obj.Createddate);
                    ht.Add(p);
                }
            }
            if (obj.Del > 0)//删除标记
            {
                term += " and del=@del ";
                if (ESP.Media.Access.Utilities.Common.Find(ht, "@del") == -1)
                {
                    SqlParameter p = new SqlParameter("@del", SqlDbType.SmallInt, 2);
                    p.Value = obj.Del;
                    ht.Add(p);
                }
            }
            if (obj.Eventname != null && obj.Eventname.Trim().Length > 0)
            {
                term += " and eventname=@eventname ";
                if (ESP.Media.Access.Utilities.Common.Find(ht, "@eventname") == -1)
                {
                    SqlParameter p = new SqlParameter("@eventname", SqlDbType.NVarChar, 100);
                    p.Value = obj.Eventname;
                    ht.Add(p);
                }
            }
            if (obj.Eventcontent != null && obj.Eventcontent.Trim().Length > 0)
            {
                term += " and eventcontent=@eventcontent ";
                if (ESP.Media.Access.Utilities.Common.Find(ht, "@eventcontent") == -1)
                {
                    SqlParameter p = new SqlParameter("@eventcontent", SqlDbType.NVarChar, 1000);
                    p.Value = obj.Eventcontent;
                    ht.Add(p);
                }
            }
            if (obj.Eventstarttime != null && obj.Eventstarttime.Trim().Length > 0)
            {
                term += " and eventstarttime=@eventstarttime ";
                if (ESP.Media.Access.Utilities.Common.Find(ht, "@eventstarttime") == -1)
                {
                    SqlParameter p = new SqlParameter("@eventstarttime", SqlDbType.SmallDateTime, 4);
                    p.Value = ESP.Media.Access.Utilities.Common.StringToDateTime(obj.Eventstarttime);
                    ht.Add(p);
                }
            }
            if (obj.Eventstatus > 0)//Eventstatus
            {
                term += " and eventstatus=@eventstatus ";
                if (ESP.Media.Access.Utilities.Common.Find(ht, "@eventstatus") == -1)
                {
                    SqlParameter p = new SqlParameter("@eventstatus", SqlDbType.Int, 4);
                    p.Value = obj.Eventstatus;
                    ht.Add(p);
                }
            }
            return term;
        }
        #endregion
        #region 查询
        private static string getQueryTerms(EventsInfo obj, ref Hashtable ht)
        {
            string term = string.Empty;
            if (obj.Eventid > 0)//EventID
            {
                term += " and a.eventid=@eventid ";
                if (!ht.ContainsKey("@eventid"))
                {
                    ht.Add("@eventid", obj.Eventid);
                }
            }
            if (obj.Projectid > 0)//ProjectID
            {
                term += " and a.projectid=@projectid ";
                if (!ht.ContainsKey("@projectid"))
                {
                    ht.Add("@projectid", obj.Projectid);
                }
            }
            if (obj.Createdbyuserid > 0)//CreatedByUserID
            {
                term += " and a.createdbyuserid=@createdbyuserid ";
                if (!ht.ContainsKey("@createdbyuserid"))
                {
                    ht.Add("@createdbyuserid", obj.Createdbyuserid);
                }
            }
            if (obj.Createdip != null && obj.Createdip.Trim().Length > 0)
            {
                term += " and a.createdip=@createdip ";
                if (!ht.ContainsKey("@createdip"))
                {
                    ht.Add("@createdip", obj.Createdip);
                }
            }
            if (obj.Createddate != null && obj.Createddate.Trim().Length > 0)
            {
                term += " and a.createddate=@createddate ";
                if (!ht.ContainsKey("@createddate"))
                {
                    ht.Add("@createddate", obj.Createddate);
                }
            }
            if (obj.Del > 0)//删除标记
            {
                term += " and a.del=@del ";
                if (!ht.ContainsKey("@del"))
                {
                    ht.Add("@del", obj.Del);
                }
            }
            if (obj.Eventname != null && obj.Eventname.Trim().Length > 0)
            {
                term += " and a.eventname=@eventname ";
                if (!ht.ContainsKey("@eventname"))
                {
                    ht.Add("@eventname", obj.Eventname);
                }
            }
            if (obj.Eventcontent != null && obj.Eventcontent.Trim().Length > 0)
            {
                term += " and a.eventcontent=@eventcontent ";
                if (!ht.ContainsKey("@eventcontent"))
                {
                    ht.Add("@eventcontent", obj.Eventcontent);
                }
            }
            if (obj.Eventstarttime != null && obj.Eventstarttime.Trim().Length > 0)
            {
                term += " and a.eventstarttime=@eventstarttime ";
                if (!ht.ContainsKey("@eventstarttime"))
                {
                    ht.Add("@eventstarttime", obj.Eventstarttime);
                }
            }
            if (obj.Eventstatus > 0)//Eventstatus
            {
                term += " and a.eventstatus=@eventstatus ";
                if (!ht.ContainsKey("@eventstatus"))
                {
                    ht.Add("@eventstatus", obj.Eventstatus);
                }
            }
            return term;
        }
        //得到查询字符串
        private static string getQueryString(string front, string columns, string LinkTable, string terms)
        {
            if (front == null)
            {
                front = string.Empty;
            }
            if (columns == null)
            {
                columns = string.Empty;
            }
            else
            {
                columns = "," + columns;
            }
            columns = columns.TrimEnd(',');
            if (LinkTable == null)
            {
                LinkTable = string.Empty;
            }
            if (terms == null)
            {
                terms = string.Empty;
            }
            if (terms != null && terms.Trim().Length > 0)
            {
                if (!terms.Trim().StartsWith("and"))
                {
                    terms = " and " + terms;
                }
            }
            string sql = @"select {0} a.eventid as eventid,a.projectid as projectid,a.createdbyuserid as createdbyuserid,a.createdip as createdip,a.createddate as createddate,a.del as del,a.eventname as eventname,a.eventcontent as eventcontent,a.eventstarttime as eventstarttime,a.eventstatus as eventstatus {1} from media_events as a {2} where 1=1 {3} ";
            return string.Format(sql, front, columns, LinkTable, terms);
        }


        private static string getQueryString(string front, ArrayList columns, string LinkTable, string terms)
        {
            if (columns == null)
            {
                columns = new ArrayList();
            }
            string col = string.Empty;
            if (columns.Count > 0)
            {
                col += ",";
                for (int i = 0; i < columns.Count; i++)
                {
                    col += columns[i].ToString();
                }
            }
            col = col.TrimEnd(',');
            return getQueryString(front, col, LinkTable, terms);
        }


        public static DataTable QueryInfo(string terms, Hashtable ht)
        {
            if (ht == null)
            {
                ht = new Hashtable();
            }
            SqlParameter[] para = ESP.Media.Access.Utilities.Common.DictToSqlParam(ht);
            return QueryInfo(terms, para);
        }


        public static DataTable QueryInfo(string terms, Hashtable ht, SqlTransaction trans)
        {
            if (ht == null)
            {
                ht = new Hashtable();
            }
            SqlParameter[] para = ESP.Media.Access.Utilities.Common.DictToSqlParam(ht);
            return QueryInfo(trans, terms, para);
        }


        public static DataTable QueryInfo(string terms, params SqlParameter[] param)
        {
            DataTable dt = null;
            string front = " distinct ";
            string columns = null;
            string LinkTable = null;
            string sql = getQueryString(front, columns, LinkTable, terms);
            if (param != null && param.Length > 0)
            {
                dt = clsSelect.QueryBySql(sql, param);
            }
            else
            {
                dt = clsSelect.QueryBySql(sql);
            }
            return dt;
        }


        public static DataTable QueryInfo(SqlTransaction trans, string terms, params SqlParameter[] param)
        {
            DataTable dt = null;
            string front = " distinct ";
            string columns = null;
            string LinkTable = null;
            string sql = getQueryString(front, columns, LinkTable, terms);
            if (param != null && param.Length > 0)
            {
                dt = clsSelect.QueryBySql(trans, sql, param);
            }
            else
            {
                dt = clsSelect.QueryBySql(sql, trans);
            }
            return dt;
        }


        public static DataTable QueryInfoByObj(EventsInfo obj, string terms, Hashtable ht)
        {
            if (ht == null)
            {
                ht = new Hashtable();
            }
            SqlParameter[] para = ESP.Media.Access.Utilities.Common.DictToSqlParam(ht);
            return QueryInfoByObj(obj, terms, para);
        }


        public static DataTable QueryInfoByObj(EventsInfo obj, string terms, params SqlParameter[] param)
        {
            if (terms == null)
            {
                terms = string.Empty;
            }
            Hashtable ht = new Hashtable();
            string temp = getQueryTerms(obj, ref ht);
            if (temp != null && temp.Trim().Length > 0)
            {
                terms += temp;
            }
            if (param != null && param.Length > 0)
            {
                for (int i = 0; i < param.Length; i++)
                {
                    if (!ht.ContainsKey(param[i].ParameterName))
                    {
                        ht.Add(param[i].ParameterName, param[i].Value);
                    }
                }
            }
            SqlParameter[] para = ESP.Media.Access.Utilities.Common.DictToSqlParam(ht);
            return QueryInfo(terms, para);
        }


        #endregion
        #region load
        public static EventsInfo Load(int id)
        {
            DataTable dt = null;
            dt = QueryInfo(" and a.eventid=@id ", new SqlParameter("@id", id));
            if (dt != null && dt.Rows.Count > 0)
            {
                return setObject(dt.Rows[0]);
            }
            return null;
        }


        public static EventsInfo Load(int id, SqlTransaction trans)
        {
            DataTable dt = null;
            dt = QueryInfo(trans, " and a.eventid=@id ", new SqlParameter("@id", id));
            if (dt != null && dt.Rows.Count > 0)
            {
                return setObject(dt.Rows[0]);
            }
            return null;
        }


        public static EventsInfo setObject(DataRow dr)
        {
            EventsInfo obj = new EventsInfo();
            if (dr.Table.Columns.Contains("eventid") && dr["eventid"] != DBNull.Value)//eventid
            {
                obj.Eventid = Convert.ToInt32(dr["eventid"]);
            }
            if (dr.Table.Columns.Contains("projectid") && dr["projectid"] != DBNull.Value)//项目ID
            {
                obj.Projectid = Convert.ToInt32(dr["projectid"]);
            }
            if (dr.Table.Columns.Contains("createdbyuserid") && dr["createdbyuserid"] != DBNull.Value)//创建用户ID
            {
                obj.Createdbyuserid = Convert.ToInt32(dr["createdbyuserid"]);
            }
            if (dr.Table.Columns.Contains("createdip") && dr["createdip"] != DBNull.Value)//创建IP
            {
                obj.Createdip = (dr["createdip"]).ToString();
            }
            if (dr.Table.Columns.Contains("createddate") && dr["createddate"] != DBNull.Value)//创建日期
            {
                obj.Createddate = (dr["createddate"]).ToString();
            }
            if (dr.Table.Columns.Contains("del") && dr["del"] != DBNull.Value)//删除标记
            {
                obj.Del = Convert.ToInt32(dr["del"]);
            }
            if (dr.Table.Columns.Contains("eventname") && dr["eventname"] != DBNull.Value)//事件名称
            {
                obj.Eventname = (dr["eventname"]).ToString();
            }
            if (dr.Table.Columns.Contains("eventcontent") && dr["eventcontent"] != DBNull.Value)//时间内容
            {
                obj.Eventcontent = (dr["eventcontent"]).ToString();
            }
            if (dr.Table.Columns.Contains("eventstarttime") && dr["eventstarttime"] != DBNull.Value)//时间开始时间
            {
                obj.Eventstarttime = (dr["eventstarttime"]).ToString();
            }
            if (dr.Table.Columns.Contains("eventstatus") && dr["eventstatus"] != DBNull.Value)//事件状态
            {
                obj.Eventstatus = Convert.ToInt32(dr["eventstatus"]);
            }
            return obj;
        }
        #endregion
    }
}
