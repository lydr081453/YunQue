using System;
using System.Data;
using ESP.Media.Entity;
using System.Data.SqlClient;
using System.Collections;
using System.Collections.Generic;
using ESP.Media.Access.Utilities;
namespace ESP.Media.DataAccess
{
    public class DailysDataProvider
    {
        #region 构造函数
        public DailysDataProvider()
        {
        }
        #endregion
        #region 插入
        //插入字符串
        private static string strinsert(DailysInfo obj, ref List<SqlParameter> ht)
        {
            if (ht == null)
            {
                ht = new List<SqlParameter>();
            }
            string sql = @"insert into media_dailys (projectid,createdbyuserid,createdip,createddate,del,dailyname,dailycontent,dailystarttime,dailystatus,dailycycletype,dailycycledays) values (@projectid,@createdbyuserid,@createdip,@createddate,@del,@dailyname,@dailycontent,@dailystarttime,@dailystatus,@dailycycletype,@dailycycledays);select @@IDENTITY as rowNum;";
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
            SqlParameter param_Dailyname = new SqlParameter("@Dailyname", SqlDbType.NVarChar, 100);
            param_Dailyname.Value = obj.Dailyname;
            ht.Add(param_Dailyname);
            SqlParameter param_Dailycontent = new SqlParameter("@Dailycontent", SqlDbType.NVarChar, 1000);
            param_Dailycontent.Value = obj.Dailycontent;
            ht.Add(param_Dailycontent);
            SqlParameter param_Dailystarttime = new SqlParameter("@Dailystarttime", SqlDbType.SmallDateTime, 4);
            param_Dailystarttime.Value = ESP.Media.Access.Utilities.Common.StringToDateTime(obj.Dailystarttime);
            ht.Add(param_Dailystarttime);
            SqlParameter param_Dailystatus = new SqlParameter("@Dailystatus", SqlDbType.Int, 4);
            param_Dailystatus.Value = obj.Dailystatus;
            ht.Add(param_Dailystatus);
            SqlParameter param_Dailycycletype = new SqlParameter("@Dailycycletype", SqlDbType.Int, 4);
            param_Dailycycletype.Value = obj.Dailycycletype;
            ht.Add(param_Dailycycletype);
            SqlParameter param_Dailycycledays = new SqlParameter("@Dailycycledays", SqlDbType.Int, 4);
            param_Dailycycledays.Value = obj.Dailycycledays;
            ht.Add(param_Dailycycledays);
            return sql;
        }


        //插入一条记录
        public static int insertinfo(DailysInfo obj, SqlTransaction trans)
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
        public static int insertinfo(DailysInfo obj)
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
            string sql = "delete media_dailys where DailyID=@id";
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
        public static string getUpdateString(DailysInfo objTerm, DailysInfo Objupdate, string term, ref List<SqlParameter> ht, params SqlParameter[] param)
        {
            string sql = "update media_dailys set projectid=@projectid,createdbyuserid=@createdbyuserid,createdip=@createdip,createddate=@createddate,del=@del,dailyname=@dailyname,dailycontent=@dailycontent,dailystarttime=@dailystarttime,dailystatus=@dailystatus,dailycycletype=@dailycycletype,dailycycledays=@dailycycledays where 1=1 ";
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
            SqlParameter param_dailyname = new SqlParameter("@dailyname", SqlDbType.NVarChar, 100);
            param_dailyname.Value = Objupdate.Dailyname;
            ht.Add(param_dailyname);
            SqlParameter param_dailycontent = new SqlParameter("@dailycontent", SqlDbType.NVarChar, 1000);
            param_dailycontent.Value = Objupdate.Dailycontent;
            ht.Add(param_dailycontent);
            SqlParameter param_dailystarttime = new SqlParameter("@dailystarttime", SqlDbType.SmallDateTime, 4);
            param_dailystarttime.Value = ESP.Media.Access.Utilities.Common.StringToDateTime(Objupdate.Dailystarttime);
            ht.Add(param_dailystarttime);
            SqlParameter param_dailystatus = new SqlParameter("@dailystatus", SqlDbType.Int, 4);
            param_dailystatus.Value = Objupdate.Dailystatus;
            ht.Add(param_dailystatus);
            SqlParameter param_dailycycletype = new SqlParameter("@dailycycletype", SqlDbType.Int, 4);
            param_dailycycletype.Value = Objupdate.Dailycycletype;
            ht.Add(param_dailycycletype);
            SqlParameter param_dailycycledays = new SqlParameter("@dailycycledays", SqlDbType.Int, 4);
            param_dailycycledays.Value = Objupdate.Dailycycledays;
            ht.Add(param_dailycycledays);
            if (objTerm == null && (term == null || term.Trim().Length <= 0))
            {
                sql += " and DailyID=@id ";
                if (ESP.Media.Access.Utilities.Common.Find(ht, "@id") == -1)
                {
                    SqlParameter p = new SqlParameter("@id", SqlDbType.Int, 4);
                    p.Value = Objupdate.Dailyid;
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
        public static bool updateInfo(SqlTransaction trans, DailysInfo objterm, DailysInfo Objupdate, string term, params SqlParameter[] param)
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
        public static bool updateInfo(DailysInfo objterm, DailysInfo Objupdate, string term, params SqlParameter[] param)
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


        private static string getTerms(DailysInfo obj, ref List<SqlParameter> ht)
        {
            string term = string.Empty;
            if (obj.Dailyid > 0)//DailyID
            {
                term += " and dailyid=@dailyid ";
                if (ESP.Media.Access.Utilities.Common.Find(ht, "@dailyid") == -1)
                {
                    SqlParameter p = new SqlParameter("@dailyid", SqlDbType.Int, 4);
                    p.Value = obj.Dailyid;
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
            if (obj.Dailyname != null && obj.Dailyname.Trim().Length > 0)
            {
                term += " and dailyname=@dailyname ";
                if (ESP.Media.Access.Utilities.Common.Find(ht, "@dailyname") == -1)
                {
                    SqlParameter p = new SqlParameter("@dailyname", SqlDbType.NVarChar, 100);
                    p.Value = obj.Dailyname;
                    ht.Add(p);
                }
            }
            if (obj.Dailycontent != null && obj.Dailycontent.Trim().Length > 0)
            {
                term += " and dailycontent=@dailycontent ";
                if (ESP.Media.Access.Utilities.Common.Find(ht, "@dailycontent") == -1)
                {
                    SqlParameter p = new SqlParameter("@dailycontent", SqlDbType.NVarChar, 1000);
                    p.Value = obj.Dailycontent;
                    ht.Add(p);
                }
            }
            if (obj.Dailystarttime != null && obj.Dailystarttime.Trim().Length > 0)
            {
                term += " and dailystarttime=@dailystarttime ";
                if (ESP.Media.Access.Utilities.Common.Find(ht, "@dailystarttime") == -1)
                {
                    SqlParameter p = new SqlParameter("@dailystarttime", SqlDbType.SmallDateTime, 4);
                    p.Value = ESP.Media.Access.Utilities.Common.StringToDateTime(obj.Dailystarttime);
                    ht.Add(p);
                }
            }
            if (obj.Dailystatus > 0)//Dailystatus
            {
                term += " and dailystatus=@dailystatus ";
                if (ESP.Media.Access.Utilities.Common.Find(ht, "@dailystatus") == -1)
                {
                    SqlParameter p = new SqlParameter("@dailystatus", SqlDbType.Int, 4);
                    p.Value = obj.Dailystatus;
                    ht.Add(p);
                }
            }
            if (obj.Dailycycletype > 0)//DailyCycleType
            {
                term += " and dailycycletype=@dailycycletype ";
                if (ESP.Media.Access.Utilities.Common.Find(ht, "@dailycycletype") == -1)
                {
                    SqlParameter p = new SqlParameter("@dailycycletype", SqlDbType.Int, 4);
                    p.Value = obj.Dailycycletype;
                    ht.Add(p);
                }
            }
            if (obj.Dailycycledays > 0)//DailyCycleDays
            {
                term += " and dailycycledays=@dailycycledays ";
                if (ESP.Media.Access.Utilities.Common.Find(ht, "@dailycycledays") == -1)
                {
                    SqlParameter p = new SqlParameter("@dailycycledays", SqlDbType.Int, 4);
                    p.Value = obj.Dailycycledays;
                    ht.Add(p);
                }
            }
            return term;
        }
        #endregion
        #region 查询
        private static string getQueryTerms(DailysInfo obj, ref Hashtable ht)
        {
            string term = string.Empty;
            if (obj.Dailyid > 0)//DailyID
            {
                term += " and a.dailyid=@dailyid ";
                if (!ht.ContainsKey("@dailyid"))
                {
                    ht.Add("@dailyid", obj.Dailyid);
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
            if (obj.Dailyname != null && obj.Dailyname.Trim().Length > 0)
            {
                term += " and a.dailyname=@dailyname ";
                if (!ht.ContainsKey("@dailyname"))
                {
                    ht.Add("@dailyname", obj.Dailyname);
                }
            }
            if (obj.Dailycontent != null && obj.Dailycontent.Trim().Length > 0)
            {
                term += " and a.dailycontent=@dailycontent ";
                if (!ht.ContainsKey("@dailycontent"))
                {
                    ht.Add("@dailycontent", obj.Dailycontent);
                }
            }
            if (obj.Dailystarttime != null && obj.Dailystarttime.Trim().Length > 0)
            {
                term += " and a.dailystarttime=@dailystarttime ";
                if (!ht.ContainsKey("@dailystarttime"))
                {
                    ht.Add("@dailystarttime", obj.Dailystarttime);
                }
            }
            if (obj.Dailystatus > 0)//Dailystatus
            {
                term += " and a.dailystatus=@dailystatus ";
                if (!ht.ContainsKey("@dailystatus"))
                {
                    ht.Add("@dailystatus", obj.Dailystatus);
                }
            }
            if (obj.Dailycycletype > 0)//DailyCycleType
            {
                term += " and a.dailycycletype=@dailycycletype ";
                if (!ht.ContainsKey("@dailycycletype"))
                {
                    ht.Add("@dailycycletype", obj.Dailycycletype);
                }
            }
            if (obj.Dailycycledays > 0)//DailyCycleDays
            {
                term += " and a.dailycycledays=@dailycycledays ";
                if (!ht.ContainsKey("@dailycycledays"))
                {
                    ht.Add("@dailycycledays", obj.Dailycycledays);
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
            string sql = @"select {0} a.dailyid as dailyid,a.projectid as projectid,a.createdbyuserid as createdbyuserid,a.createdip as createdip,a.createddate as createddate,a.del as del,a.dailyname as dailyname,a.dailycontent as dailycontent,a.dailystarttime as dailystarttime,a.dailystatus as dailystatus,a.dailycycletype as dailycycletype,a.dailycycledays as dailycycledays {1} from media_dailys as a {2} where 1=1 {3} ";
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


        public static DataTable QueryInfoByObj(DailysInfo obj, string terms, Hashtable ht)
        {
            if (ht == null)
            {
                ht = new Hashtable();
            }
            SqlParameter[] para = ESP.Media.Access.Utilities.Common.DictToSqlParam(ht);
            return QueryInfoByObj(obj, terms, para);
        }


        public static DataTable QueryInfoByObj(DailysInfo obj, string terms, params SqlParameter[] param)
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
        public static DailysInfo Load(int id)
        {
            DataTable dt = null;
            dt = QueryInfo(" and a.dailyid=@id ", new SqlParameter("@id", id));
            if (dt != null && dt.Rows.Count > 0)
            {
                return setObject(dt.Rows[0]);
            }
            return null;
        }


        public static DailysInfo Load(int id, SqlTransaction trans)
        {
            DataTable dt = null;
            dt = QueryInfo(trans, " and a.dailyid=@id ", new SqlParameter("@id", id));
            if (dt != null && dt.Rows.Count > 0)
            {
                return setObject(dt.Rows[0]);
            }
            return null;
        }


        public static DailysInfo setObject(DataRow dr)
        {
            DailysInfo obj = new DailysInfo();
            if (dr.Table.Columns.Contains("dailyid") && dr["dailyid"] != DBNull.Value)//dailyid
            {
                obj.Dailyid = Convert.ToInt32(dr["dailyid"]);
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
            if (dr.Table.Columns.Contains("dailyname") && dr["dailyname"] != DBNull.Value)//日常传播名称
            {
                obj.Dailyname = (dr["dailyname"]).ToString();
            }
            if (dr.Table.Columns.Contains("dailycontent") && dr["dailycontent"] != DBNull.Value)//日常传播内容
            {
                obj.Dailycontent = (dr["dailycontent"]).ToString();
            }
            if (dr.Table.Columns.Contains("dailystarttime") && dr["dailystarttime"] != DBNull.Value)//日常传播开始时间
            {
                obj.Dailystarttime = (dr["dailystarttime"]).ToString();
            }
            if (dr.Table.Columns.Contains("dailystatus") && dr["dailystatus"] != DBNull.Value)//日常传播状态
            {
                obj.Dailystatus = Convert.ToInt32(dr["dailystatus"]);
            }
            if (dr.Table.Columns.Contains("dailycycletype") && dr["dailycycletype"] != DBNull.Value)//周期类型
            {
                obj.Dailycycletype = Convert.ToInt32(dr["dailycycletype"]);
            }
            if (dr.Table.Columns.Contains("dailycycledays") && dr["dailycycledays"] != DBNull.Value)//周期天数
            {
                obj.Dailycycledays = Convert.ToInt32(dr["dailycycledays"]);
            }
            return obj;
        }
        #endregion
    }
}
