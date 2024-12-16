using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.Collections.Generic;
using ESP.Media.Entity;
using ESP.Media.Access.Utilities;
namespace ESP.Media.DataAccess
{
    public class Modeation_checkrecordsDataProvider
    {
        #region 构造函数
        public Modeation_checkrecordsDataProvider()
        {
        }
        #endregion
        #region 插入
        //插入字符串
        private static string strinsert(Modeation_checkrecordsInfo obj, ref List<SqlParameter> ht)
        {
            if (ht == null)
            {
                ht = new List<SqlParameter>();
            }
            string sql = @"insert into media_modeation_checkrecords (mediaitemid,userid,username,checkaction,checkdate) values (@mediaitemid,@userid,@username,@checkaction,@checkdate);select @@IDENTITY as rowNum;";
            SqlParameter param_Mediaitemid = new SqlParameter("@Mediaitemid", SqlDbType.Int, 4);
            param_Mediaitemid.Value = obj.Mediaitemid;
            ht.Add(param_Mediaitemid);
            SqlParameter param_Userid = new SqlParameter("@Userid", SqlDbType.Int, 4);
            param_Userid.Value = obj.Userid;
            ht.Add(param_Userid);
            SqlParameter param_Username = new SqlParameter("@Username", SqlDbType.NVarChar, 512);
            param_Username.Value = obj.Username;
            ht.Add(param_Username);
            SqlParameter param_Checkaction = new SqlParameter("@Checkaction", SqlDbType.Int, 4);
            param_Checkaction.Value = obj.Checkaction;
            ht.Add(param_Checkaction);
            SqlParameter param_Checkdate = new SqlParameter("@Checkdate", SqlDbType.DateTime, 8);
            param_Checkdate.Value = ESP.Media.Access.Utilities.Common.StringToDateTime(obj.Checkdate);
            ht.Add(param_Checkdate);
            return sql;
        }


        //插入一条记录
        public static int insertinfo(Modeation_checkrecordsInfo obj, SqlTransaction trans)
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
        public static int insertinfo(Modeation_checkrecordsInfo obj)
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
            string sql = "delete media_modeation_checkrecords";
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
        public static string getUpdateString(Modeation_checkrecordsInfo objTerm, Modeation_checkrecordsInfo Objupdate, string term, ref List<SqlParameter> ht, params SqlParameter[] param)
        {
            string sql = "update media_modeation_checkrecords set mediaitemid=@mediaitemid,userid=@userid,username=@username,checkaction=@checkaction,checkdate=@checkdate where 1=1 ";
            SqlParameter param_mediaitemid = new SqlParameter("@mediaitemid", SqlDbType.Int, 4);
            param_mediaitemid.Value = Objupdate.Mediaitemid;
            ht.Add(param_mediaitemid);
            SqlParameter param_userid = new SqlParameter("@userid", SqlDbType.Int, 4);
            param_userid.Value = Objupdate.Userid;
            ht.Add(param_userid);
            SqlParameter param_username = new SqlParameter("@username", SqlDbType.NVarChar, 512);
            param_username.Value = Objupdate.Username;
            ht.Add(param_username);
            SqlParameter param_checkaction = new SqlParameter("@checkaction", SqlDbType.Int, 4);
            param_checkaction.Value = Objupdate.Checkaction;
            ht.Add(param_checkaction);
            SqlParameter param_checkdate = new SqlParameter("@checkdate", SqlDbType.DateTime, 8);
            param_checkdate.Value = ESP.Media.Access.Utilities.Common.StringToDateTime(Objupdate.Checkdate);
            ht.Add(param_checkdate);
            if (objTerm == null && (term == null || term.Trim().Length <= 0))
            {
                sql += " and mediaitemid=@id ";
                if (ESP.Media.Access.Utilities.Common.Find(ht, "@id") == -1)
                {
                    SqlParameter p = new SqlParameter("@id", SqlDbType.Int, 4);
                    p.Value = Objupdate.Mediaitemid;
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
        public static bool updateInfo(SqlTransaction trans, Modeation_checkrecordsInfo objterm, Modeation_checkrecordsInfo Objupdate, string term, params SqlParameter[] param)
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
        public static bool updateInfo(Modeation_checkrecordsInfo objterm, Modeation_checkrecordsInfo Objupdate, string term, params SqlParameter[] param)
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


        private static string getTerms(Modeation_checkrecordsInfo obj, ref List<SqlParameter> ht)
        {
            string term = string.Empty;
            if (obj.Mediaitemid > 0)//MediaItemID
            {
                term += " and mediaitemid=@mediaitemid ";
                if (ESP.Media.Access.Utilities.Common.Find(ht, "@mediaitemid") == -1)
                {
                    SqlParameter p = new SqlParameter("@mediaitemid", SqlDbType.Int, 4);
                    p.Value = obj.Mediaitemid;
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
            if (obj.Username != null && obj.Username.Trim().Length > 0)
            {
                term += " and username=@username ";
                if (ESP.Media.Access.Utilities.Common.Find(ht, "@username") == -1)
                {
                    SqlParameter p = new SqlParameter("@username", SqlDbType.NVarChar, 512);
                    p.Value = obj.Username;
                    ht.Add(p);
                }
            }
            if (obj.Checkaction > 0)//CheckAction
            {
                term += " and checkaction=@checkaction ";
                if (ESP.Media.Access.Utilities.Common.Find(ht, "@checkaction") == -1)
                {
                    SqlParameter p = new SqlParameter("@checkaction", SqlDbType.Int, 4);
                    p.Value = obj.Checkaction;
                    ht.Add(p);
                }
            }
            if (obj.Checkdate != null && obj.Checkdate.Trim().Length > 0)
            {
                term += " and checkdate=@checkdate ";
                if (ESP.Media.Access.Utilities.Common.Find(ht, "@checkdate") == -1)
                {
                    SqlParameter p = new SqlParameter("@checkdate", SqlDbType.DateTime, 8);
                    p.Value = ESP.Media.Access.Utilities.Common.StringToDateTime(obj.Checkdate);
                    ht.Add(p);
                }
            }
            return term;
        }
        #endregion
        #region 查询
        private static string getQueryTerms(Modeation_checkrecordsInfo obj, ref Hashtable ht)
        {
            string term = string.Empty;
            if (obj.Mediaitemid > 0)//MediaItemID
            {
                term += " and a.mediaitemid=@mediaitemid ";
                if (!ht.ContainsKey("@mediaitemid"))
                {
                    ht.Add("@mediaitemid", obj.Mediaitemid);
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
            if (obj.Username != null && obj.Username.Trim().Length > 0)
            {
                term += " and a.username=@username ";
                if (!ht.ContainsKey("@username"))
                {
                    ht.Add("@username", obj.Username);
                }
            }
            if (obj.Checkaction > 0)//CheckAction
            {
                term += " and a.checkaction=@checkaction ";
                if (!ht.ContainsKey("@checkaction"))
                {
                    ht.Add("@checkaction", obj.Checkaction);
                }
            }
            if (obj.Checkdate != null && obj.Checkdate.Trim().Length > 0)
            {
                term += " and a.checkdate=@checkdate ";
                if (!ht.ContainsKey("@checkdate"))
                {
                    ht.Add("@checkdate", obj.Checkdate);
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
            string sql = @"select {0} a.mediaitemid as mediaitemid,a.userid as userid,a.username as username,a.checkaction as checkaction,a.checkdate as checkdate {1} from media_modeation_checkrecords as a {2} where 1=1 {3} ";
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


        public static DataTable QueryInfoByObj(Modeation_checkrecordsInfo obj, string terms, Hashtable ht)
        {
            if (ht == null)
            {
                ht = new Hashtable();
            }
            SqlParameter[] para = ESP.Media.Access.Utilities.Common.DictToSqlParam(ht);
            return QueryInfoByObj(obj, terms, para);
        }


        public static DataTable QueryInfoByObj(Modeation_checkrecordsInfo obj, string terms, params SqlParameter[] param)
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
        public static Modeation_checkrecordsInfo Load(int id)
        {
            DataTable dt = null;
            dt = QueryInfo(" and a.mediaitemid=@id ", new SqlParameter("@id", id));
            if (dt != null && dt.Rows.Count > 0)
            {
                return setObject(dt.Rows[0]);
            }
            return null;
        }


        public static Modeation_checkrecordsInfo Load(int id, SqlTransaction trans)
        {
            DataTable dt = null;
            dt = QueryInfo(trans, " and a.mediaitemid=@id ", new SqlParameter("@id", id));
            if (dt != null && dt.Rows.Count > 0)
            {
                return setObject(dt.Rows[0]);
            }
            return null;
        }


        public static Modeation_checkrecordsInfo setObject(DataRow dr)
        {
            Modeation_checkrecordsInfo obj = new Modeation_checkrecordsInfo();
            if (dr.Table.Columns.Contains("mediaitemid") && dr["mediaitemid"] != DBNull.Value)//媒体id
            {
                obj.Mediaitemid = Convert.ToInt32(dr["mediaitemid"]);
            }
            if (dr.Table.Columns.Contains("userid") && dr["userid"] != DBNull.Value)//用户id
            {
                obj.Userid = Convert.ToInt32(dr["userid"]);
            }
            if (dr.Table.Columns.Contains("username") && dr["username"] != DBNull.Value)//用户名
            {
                obj.Username = (dr["username"]).ToString();
            }
            if (dr.Table.Columns.Contains("checkaction") && dr["checkaction"] != DBNull.Value)//选择类型
            {
                obj.Checkaction = Convert.ToInt32(dr["checkaction"]);
            }
            if (dr.Table.Columns.Contains("checkdate") && dr["checkdate"] != DBNull.Value)//选择日期
            {
                obj.Checkdate = (dr["checkdate"]).ToString();
            }
            return obj;
        }
        #endregion
    }
}
