using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using ESP.Media.Entity;
using ESP.Media.Access.Utilities;
using System.Collections.Generic;
namespace ESP.Media.DataAccess
{
    public class CounterDataProvider
    {
        #region 构造函数
        public CounterDataProvider()
        {
        }
        #endregion
        #region 插入
        //插入字符串
        private static string strinsert(CounterInfo obj, ref List<SqlParameter> ht)
        {
            if (ht == null)
            {
                ht = new List<SqlParameter>();
            }
            string sql = @"insert into media_counter (userid,usercode,username,counts,operatedate) values (@userid,@usercode,@username,@counts,@operatedate);select @@IDENTITY as rowNum;";
            SqlParameter param_Userid = new SqlParameter("@Userid", SqlDbType.Int, 4);
            param_Userid.Value = obj.Userid;
            ht.Add(param_Userid);
            SqlParameter param_Usercode = new SqlParameter("@Usercode", SqlDbType.NVarChar, 20);
            param_Usercode.Value = obj.Usercode;
            ht.Add(param_Usercode);
            SqlParameter param_Username = new SqlParameter("@Username", SqlDbType.NVarChar, 100);
            param_Username.Value = obj.Username;
            ht.Add(param_Username);
            SqlParameter param_Counts = new SqlParameter("@Counts", SqlDbType.Int, 4);
            param_Counts.Value = obj.Counts;
            ht.Add(param_Counts);
            SqlParameter param_Operatedate = new SqlParameter("@Operatedate", SqlDbType.DateTime, 8);
            param_Operatedate.Value = ESP.Media.Access.Utilities.Common.StringToDateTime(obj.Operatedate);
            ht.Add(param_Operatedate);
            return sql;
        }


        //插入一条记录
        public static int insertinfo(CounterInfo obj, SqlTransaction trans)
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
        public static int insertinfo(CounterInfo obj)
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
        public static bool DeleteInfo(int userid, SqlTransaction trans)
        {
            int rows = 0;
            string sql = "delete media_counter where userid=@userid";
            SqlParameter param = new SqlParameter("@userid", SqlDbType.Int);
            param.Value = userid;
            rows = SqlHelper.ExecuteNonQuery(trans, CommandType.Text, sql, param);
            if (rows > 0)
            {
                return true;
            }
            return false;
        }


        //删除操作
        public static bool DeleteInfo(int userid)
        {
            using (SqlConnection conn = new SqlConnection(clsConfigOperate.CustomerSqlConnection()))
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();
                try
                {
                    if (DeleteInfo(userid, trans))
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
        public static string getUpdateString(CounterInfo Objupdate, ref List<SqlParameter> ht)
        {
            string sql = "insert into media_counter(userid,usercode,username,counts,operatedate) values(@userid,@usercode,@username,@counts,@operatedate)";
            SqlParameter param_userid = new SqlParameter("@userid", SqlDbType.Int, 4);
            param_userid.Value = Objupdate.Userid;
            ht.Add(param_userid);
            SqlParameter param_usercode = new SqlParameter("@usercode", SqlDbType.NVarChar, 20);
            param_usercode.Value = Objupdate.Usercode;
            ht.Add(param_usercode);
            SqlParameter param_username = new SqlParameter("@username", SqlDbType.NVarChar, 100);
            param_username.Value = Objupdate.Username;
            ht.Add(param_username);
            SqlParameter param_counts = new SqlParameter("@counts", SqlDbType.Int, 4);
            param_counts.Value = 0 - Objupdate.Counts;
            ht.Add(param_counts);
            SqlParameter param_operatedate = new SqlParameter("@operatedate", SqlDbType.DateTime, 8);
            param_operatedate.Value = DateTime.Now;
            ht.Add(param_operatedate);

            return sql;
        }


        //更新操作
        public static bool updateInfo(SqlTransaction trans, CounterInfo Objupdate)
        {
            if (Objupdate == null)
            {
                return false;
            }
            List<SqlParameter> ht = new List<SqlParameter>();
            string sql = getUpdateString(Objupdate, ref ht);
            SqlParameter[] para = ht.ToArray();
            int rows = SqlHelper.ExecuteNonQuery(trans, CommandType.Text, sql, para);
            if (rows >= 0)
            {
                return true;
            }
            return false;
        }


        //更新操作
        public static bool updateInfo(CounterInfo objterm, CounterInfo Objupdate, string term, params SqlParameter[] param)
        {
            if (Objupdate == null)
            {
                return false;
            }
            List<SqlParameter> ht = new List<SqlParameter>();
            string sql = getUpdateString(Objupdate, ref ht);
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


        private static string getTerms(CounterInfo obj, ref List<SqlParameter> ht)
        {
            string term = string.Empty;
            if (obj.Integralid > 0)//IntegralID
            {
                term += " and integralid=@integralid ";
                if (ESP.Media.Access.Utilities.Common.Find(ht, "@integralid") == -1)
                {
                    SqlParameter p = new SqlParameter("@integralid", SqlDbType.Int, 4);
                    p.Value = obj.Integralid;
                    ht.Add(p);
                }
            }
            if (obj.Userid > 0)//UserID
            {
                term += " and userid=@userid ";
                if (ESP.Media.Access.Utilities.Common.Find(ht, "@userid") == -1)
                {
                    SqlParameter p = new SqlParameter("@userid", SqlDbType.Int, 4);
                    p.Value = obj.Userid;
                    ht.Add(p);
                }
            }
            if (obj.Usercode != null && obj.Usercode.Trim().Length > 0)
            {
                term += " and usercode=@usercode ";
                if (ESP.Media.Access.Utilities.Common.Find(ht, "@usercode") == -1)
                {
                    SqlParameter p = new SqlParameter("@usercode", SqlDbType.NVarChar, 20);
                    p.Value = obj.Usercode;
                    ht.Add(p);
                }
            }
            if (obj.Username != null && obj.Username.Trim().Length > 0)
            {
                term += " and username=@username ";
                if (ESP.Media.Access.Utilities.Common.Find(ht, "@username") == -1)
                {
                    SqlParameter p = new SqlParameter("@username", SqlDbType.NVarChar, 100);
                    p.Value = obj.Username;
                    ht.Add(p);
                }
            }
            if (obj.Counts > 0)//counts
            {
                term += " and counts=@counts ";
                if (ESP.Media.Access.Utilities.Common.Find(ht, "@counts") == -1)
                {
                    SqlParameter p = new SqlParameter("@counts", SqlDbType.Int, 4);
                    p.Value = obj.Counts;
                    ht.Add(p);
                }
            }
            if (obj.Operatedate != null && obj.Operatedate.Trim().Length > 0)
            {
                term += " and operatedate=@operatedate ";
                if (ESP.Media.Access.Utilities.Common.Find(ht, "@operatedate") == -1)
                {
                    SqlParameter p = new SqlParameter("@operatedate", SqlDbType.DateTime, 8);
                    p.Value = ESP.Media.Access.Utilities.Common.StringToDateTime(obj.Operatedate);
                    ht.Add(p);
                }
            }
            return term;
        }
        #endregion
        #region 查询
        private static string getQueryTerms(CounterInfo obj, ref Hashtable ht)
        {
            string term = string.Empty;
            if (obj.Integralid > 0)//IntegralID
            {
                term += " and a.integralid=@integralid ";
                if (!ht.ContainsKey("@integralid"))
                {
                    ht.Add("@integralid", obj.Integralid);
                }
            }
            if (obj.Userid > 0)//UserID
            {
                term += " and a.userid=@userid ";
                if (!ht.ContainsKey("@userid"))
                {
                    ht.Add("@userid", obj.Userid);
                }
            }
            if (obj.Usercode != null && obj.Usercode.Trim().Length > 0)
            {
                term += " and a.usercode=@usercode ";
                if (!ht.ContainsKey("@usercode"))
                {
                    ht.Add("@usercode", obj.Usercode);
                }
            }
            if (obj.Username != null && obj.Username.Trim().Length > 0)
            {
                term += " and a.username=@username ";
                if (!ht.ContainsKey("@username"))
                {
                    ht.Add("@username", obj.Username);
                }
            }
            if (obj.Counts > 0)//counts
            {
                term += " and a.counts=@counts ";
                if (!ht.ContainsKey("@counts"))
                {
                    ht.Add("@counts", obj.Counts);
                }
            }
            if (obj.Operatedate != null && obj.Operatedate.Trim().Length > 0)
            {
                term += " and a.operatedate=@operatedate ";
                if (!ht.ContainsKey("@operatedate"))
                {
                    ht.Add("@operatedate", obj.Operatedate);
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
            string sql = @"select {0} a.userid as userid,a.usercode as usercode,a.username as username,sum(a.counts) as counts {1} from media_counter as a {2} where 1=1 {3} group by userid,usercode,username order by counts desc";
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


        public static DataTable QueryInfoByObj(CounterInfo obj, string terms, Hashtable ht)
        {
            if (ht == null)
            {
                ht = new Hashtable();
            }
            SqlParameter[] para = ESP.Media.Access.Utilities.Common.DictToSqlParam(ht);
            return QueryInfoByObj(obj, terms, para);
        }


        public static DataTable QueryInfoByObj(CounterInfo obj, string terms, params SqlParameter[] param)
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

        public static CounterInfo Load(int userID)
        {
            DataTable dt = null;
            dt = QueryInfo(" and a.userid=@userID ", new SqlParameter("@userID", userID));
            if (dt != null && dt.Rows.Count > 0)
            {
                return setObject(dt.Rows[0]);
            }
            return null;
        }
        public static CounterInfo Load(int userID, SqlTransaction trans)
        {
            DataTable dt = null;
            dt = QueryInfo(trans, " and a.userID=@userID ", new SqlParameter("@userID", userID));
            if (dt != null && dt.Rows.Count > 0)
            {
                return setObject(dt.Rows[0]);
            }
            return null;
        }


        public static CounterInfo setObject(DataRow dr)
        {
            CounterInfo obj = new CounterInfo();
            if (dr.Table.Columns.Contains("integralid") && dr["integralid"] != DBNull.Value)//integralid
            {
                obj.Integralid = Convert.ToInt32(dr["integralid"]);
            }
            if (dr.Table.Columns.Contains("userid") && dr["userid"] != DBNull.Value)//userid
            {
                obj.Userid = Convert.ToInt32(dr["userid"]);
            }
            if (dr.Table.Columns.Contains("usercode") && dr["usercode"] != DBNull.Value)//usercode
            {
                obj.Usercode = (dr["usercode"]).ToString();
            }
            if (dr.Table.Columns.Contains("username") && dr["username"] != DBNull.Value)//username
            {
                obj.Username = (dr["username"]).ToString();
            }
            if (dr.Table.Columns.Contains("counts") && dr["counts"] != DBNull.Value)//counts
            {
                obj.Counts = Convert.ToInt32(dr["counts"]);
            }
            if (dr.Table.Columns.Contains("operatedate") && dr["operatedate"] != DBNull.Value)//operatedate
            {
                obj.Operatedate = (dr["operatedate"]).ToString();
            }
            return obj;
        }
        #endregion
    }
}
