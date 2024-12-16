using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.Collections.Generic;
using ESP.Media.Entity;
using ESP.Media.Access.Utilities;
namespace ESP.Media.DataAccess
{
    public class ShortmsgDataProvider
    {
        #region 构造函数
        public ShortmsgDataProvider()
        {
        }
        #endregion
        #region 插入
        //插入字符串
        private static string strinsert(ShortmsgInfo obj, ref List<SqlParameter> ht)
        {
            if (ht == null)
            {
                ht = new List<SqlParameter>();
            }
            string sql = @"insert into media_shortmsg (subject,body,createid,createdate,senddate,del) values (@subject,@body,@createid,@createdate,@senddate,@del);select @@IDENTITY as rowNum;";
            SqlParameter param_Subject = new SqlParameter("@Subject", SqlDbType.NVarChar, 100);
            param_Subject.Value = obj.Subject;
            ht.Add(param_Subject);
            SqlParameter param_Body = new SqlParameter("@Body", SqlDbType.NVarChar, 1000);
            param_Body.Value = obj.Body;
            ht.Add(param_Body);
            SqlParameter param_Createid = new SqlParameter("@Createid", SqlDbType.Int, 4);
            param_Createid.Value = obj.Createid;
            ht.Add(param_Createid);
            SqlParameter param_Createdate = new SqlParameter("@Createdate", SqlDbType.SmallDateTime, 4);
            param_Createdate.Value = ESP.Media.Access.Utilities.Common.StringToDateTime(obj.Createdate);
            ht.Add(param_Createdate);
            SqlParameter param_Senddate = new SqlParameter("@Senddate", SqlDbType.SmallDateTime, 4);
            param_Senddate.Value = ESP.Media.Access.Utilities.Common.StringToDateTime(obj.Senddate);
            ht.Add(param_Senddate);
            SqlParameter param_Del = new SqlParameter("@Del", SqlDbType.SmallInt, 2);
            param_Del.Value = obj.Del;
            ht.Add(param_Del);
            return sql;
        }


        //插入一条记录
        public static int insertinfo(ShortmsgInfo obj, SqlTransaction trans)
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
        public static int insertinfo(ShortmsgInfo obj)
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
            string sql = "delete media_shortmsg where id=@id";
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
        public static string getUpdateString(ShortmsgInfo objTerm, ShortmsgInfo Objupdate, string term, ref List<SqlParameter> ht, params SqlParameter[] param)
        {
            string sql = "update media_shortmsg set subject=@subject,body=@body,createid=@createid,createdate=@createdate,senddate=@senddate,del=@del where 1=1 ";
            SqlParameter param_subject = new SqlParameter("@subject", SqlDbType.NVarChar, 100);
            param_subject.Value = Objupdate.Subject;
            ht.Add(param_subject);
            SqlParameter param_body = new SqlParameter("@body", SqlDbType.NVarChar, 1000);
            param_body.Value = Objupdate.Body;
            ht.Add(param_body);
            SqlParameter param_createid = new SqlParameter("@createid", SqlDbType.Int, 4);
            param_createid.Value = Objupdate.Createid;
            ht.Add(param_createid);
            SqlParameter param_createdate = new SqlParameter("@createdate", SqlDbType.SmallDateTime, 4);
            param_createdate.Value = ESP.Media.Access.Utilities.Common.StringToDateTime(Objupdate.Createdate);
            ht.Add(param_createdate);
            SqlParameter param_senddate = new SqlParameter("@senddate", SqlDbType.SmallDateTime, 4);
            param_senddate.Value = ESP.Media.Access.Utilities.Common.StringToDateTime(Objupdate.Senddate);
            ht.Add(param_senddate);
            SqlParameter param_del = new SqlParameter("@del", SqlDbType.SmallInt, 2);
            param_del.Value = Objupdate.Del;
            ht.Add(param_del);
            if (objTerm == null && (term == null || term.Trim().Length <= 0))
            {
                sql += " and id=@id ";
                if (ESP.Media.Access.Utilities.Common.Find(ht, "@id") == -1)
                {
                    SqlParameter p = new SqlParameter("@id", SqlDbType.Int, 4);
                    p.Value = Objupdate.Id;
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
        public static bool updateInfo(SqlTransaction trans, ShortmsgInfo objterm, ShortmsgInfo Objupdate, string term, params SqlParameter[] param)
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
        public static bool updateInfo(ShortmsgInfo objterm, ShortmsgInfo Objupdate, string term, params SqlParameter[] param)
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


        private static string getTerms(ShortmsgInfo obj, ref List<SqlParameter> ht)
        {
            string term = string.Empty;
            if (obj.Id > 0)//id
            {
                term += " and id=@id ";
                if (ESP.Media.Access.Utilities.Common.Find(ht, "@id") == -1)
                {
                    SqlParameter p = new SqlParameter("@id", SqlDbType.Int, 4);
                    p.Value = obj.Id;
                    ht.Add(p);
                }
            }
            if (obj.Subject != null && obj.Subject.Trim().Length > 0)
            {
                term += " and subject=@subject ";
                if (ESP.Media.Access.Utilities.Common.Find(ht, "@subject") == -1)
                {
                    SqlParameter p = new SqlParameter("@subject", SqlDbType.NVarChar, 100);
                    p.Value = obj.Subject;
                    ht.Add(p);
                }
            }
            if (obj.Body != null && obj.Body.Trim().Length > 0)
            {
                term += " and body=@body ";
                if (ESP.Media.Access.Utilities.Common.Find(ht, "@body") == -1)
                {
                    SqlParameter p = new SqlParameter("@body", SqlDbType.NVarChar, 1000);
                    p.Value = obj.Body;
                    ht.Add(p);
                }
            }
            if (obj.Createid > 0)//创建id
            {
                term += " and createid=@createid ";
                if (ESP.Media.Access.Utilities.Common.Find(ht, "@createid") == -1)
                {
                    SqlParameter p = new SqlParameter("@createid", SqlDbType.Int, 4);
                    p.Value = obj.Createid;
                    ht.Add(p);
                }
            }
            if (obj.Createdate != null && obj.Createdate.Trim().Length > 0)
            {
                term += " and createdate=@createdate ";
                if (ESP.Media.Access.Utilities.Common.Find(ht, "@createdate") == -1)
                {
                    SqlParameter p = new SqlParameter("@createdate", SqlDbType.SmallDateTime, 4);
                    p.Value = ESP.Media.Access.Utilities.Common.StringToDateTime(obj.Createdate);
                    ht.Add(p);
                }
            }
            if (obj.Senddate != null && obj.Senddate.Trim().Length > 0)
            {
                term += " and senddate=@senddate ";
                if (ESP.Media.Access.Utilities.Common.Find(ht, "@senddate") == -1)
                {
                    SqlParameter p = new SqlParameter("@senddate", SqlDbType.SmallDateTime, 4);
                    p.Value = ESP.Media.Access.Utilities.Common.StringToDateTime(obj.Senddate);
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
            return term;
        }
        #endregion
        #region 查询
        private static string getQueryTerms(ShortmsgInfo obj, ref Hashtable ht)
        {
            string term = string.Empty;
            if (obj.Id > 0)//id
            {
                term += " and a.id=@id ";
                if (!ht.ContainsKey("@id"))
                {
                    ht.Add("@id", obj.Id);
                }
            }
            if (obj.Subject != null && obj.Subject.Trim().Length > 0)
            {
                term += " and a.subject=@subject ";
                if (!ht.ContainsKey("@subject"))
                {
                    ht.Add("@subject", obj.Subject);
                }
            }
            if (obj.Body != null && obj.Body.Trim().Length > 0)
            {
                term += " and a.body=@body ";
                if (!ht.ContainsKey("@body"))
                {
                    ht.Add("@body", obj.Body);
                }
            }
            if (obj.Createid > 0)//创建id
            {
                term += " and a.createid=@createid ";
                if (!ht.ContainsKey("@createid"))
                {
                    ht.Add("@createid", obj.Createid);
                }
            }
            if (obj.Createdate != null && obj.Createdate.Trim().Length > 0)
            {
                term += " and a.createdate=@createdate ";
                if (!ht.ContainsKey("@createdate"))
                {
                    ht.Add("@createdate", obj.Createdate);
                }
            }
            if (obj.Senddate != null && obj.Senddate.Trim().Length > 0)
            {
                term += " and a.senddate=@senddate ";
                if (!ht.ContainsKey("@senddate"))
                {
                    ht.Add("@senddate", obj.Senddate);
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
            string sql = @"select {0} a.id as id,a.subject as subject,a.body as body,a.createid as createid,a.createdate as createdate,a.senddate as senddate,a.del as del {1} from media_shortmsg as a {2} where 1=1 {3} ";
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


        public static DataTable QueryInfoByObj(ShortmsgInfo obj, string terms, Hashtable ht)
        {
            if (ht == null)
            {
                ht = new Hashtable();
            }
            SqlParameter[] para = ESP.Media.Access.Utilities.Common.DictToSqlParam(ht);
            return QueryInfoByObj(obj, terms, para);
        }


        public static DataTable QueryInfoByObj(ShortmsgInfo obj, string terms, params SqlParameter[] param)
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
        public static ShortmsgInfo Load(int id)
        {
            DataTable dt = null;
            dt = QueryInfo(" and a.id=@id ", new SqlParameter("@id", id));
            if (dt != null && dt.Rows.Count > 0)
            {
                return setObject(dt.Rows[0]);
            }
            return null;
        }


        public static ShortmsgInfo Load(int id, SqlTransaction trans)
        {
            DataTable dt = null;
            dt = QueryInfo(trans, " and a.id=@id ", new SqlParameter("@id", id));
            if (dt != null && dt.Rows.Count > 0)
            {
                return setObject(dt.Rows[0]);
            }
            return null;
        }


        public static ShortmsgInfo setObject(DataRow dr)
        {
            ShortmsgInfo obj = new ShortmsgInfo();
            if (dr.Table.Columns.Contains("id") && dr["id"] != DBNull.Value)//id
            {
                obj.Id = Convert.ToInt32(dr["id"]);
            }
            if (dr.Table.Columns.Contains("subject") && dr["subject"] != DBNull.Value)//主题
            {
                obj.Subject = (dr["subject"]).ToString();
            }
            if (dr.Table.Columns.Contains("body") && dr["body"] != DBNull.Value)//内容
            {
                obj.Body = (dr["body"]).ToString();
            }
            if (dr.Table.Columns.Contains("createid") && dr["createid"] != DBNull.Value)//创建id
            {
                obj.Createid = Convert.ToInt32(dr["createid"]);
            }
            if (dr.Table.Columns.Contains("createdate") && dr["createdate"] != DBNull.Value)//创建日期
            {
                obj.Createdate = (dr["createdate"]).ToString();
            }
            if (dr.Table.Columns.Contains("senddate") && dr["senddate"] != DBNull.Value)//发送时间
            {
                obj.Senddate = (dr["senddate"]).ToString();
            }
            if (dr.Table.Columns.Contains("del") && dr["del"] != DBNull.Value)//删除标记
            {
                obj.Del = Convert.ToInt32(dr["del"]);
            }
            return obj;
        }
        #endregion
    }
}
