using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.Collections.Generic;
using ESP.Media.Entity;
using ESP.Media.Access.Utilities;
namespace ESP.Media.DataAccess
{
    public class TablesDataProvider
    {
        #region 构造函数
        public TablesDataProvider()
        {
        }
        #endregion
        #region 插入
        //插入字符串
        private static string strinsert(TablesInfo obj, ref List<SqlParameter> ht)
        {
            if (ht == null)
            {
                ht = new List<SqlParameter>();
            }
            string sql = @"insert into media_tables (tablename,alttablename) values (@tablename,@alttablename);select @@IDENTITY as rowNum;";
            SqlParameter param_Tablename = new SqlParameter("@Tablename", SqlDbType.NVarChar, 100);
            param_Tablename.Value = obj.Tablename;
            ht.Add(param_Tablename);
            SqlParameter param_Alttablename = new SqlParameter("@Alttablename", SqlDbType.NVarChar, 200);
            param_Alttablename.Value = obj.Alttablename;
            ht.Add(param_Alttablename);
            return sql;
        }


        //插入一条记录
        public static int insertinfo(TablesInfo obj, SqlTransaction trans)
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
        public static int insertinfo(TablesInfo obj)
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
            string sql = "delete media_tables where TableID=@id";
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
        public static string getUpdateString(TablesInfo objTerm, TablesInfo Objupdate, string term, ref List<SqlParameter> ht, params SqlParameter[] param)
        {
            string sql = "update media_tables set tablename=@tablename,alttablename=@alttablename where 1=1 ";
            SqlParameter param_tablename = new SqlParameter("@tablename", SqlDbType.NVarChar, 100);
            param_tablename.Value = Objupdate.Tablename;
            ht.Add(param_tablename);
            SqlParameter param_alttablename = new SqlParameter("@alttablename", SqlDbType.NVarChar, 200);
            param_alttablename.Value = Objupdate.Alttablename;
            ht.Add(param_alttablename);
            if (objTerm == null && (term == null || term.Trim().Length <= 0))
            {
                sql += " and TableID=@id ";
                if (ESP.Media.Access.Utilities.Common.Find(ht, "@id") == -1)
                {
                    SqlParameter p = new SqlParameter("@id", SqlDbType.Int, 4);
                    p.Value = Objupdate.Tableid;
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
        public static bool updateInfo(SqlTransaction trans, TablesInfo objterm, TablesInfo Objupdate, string term, params SqlParameter[] param)
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
        public static bool updateInfo(TablesInfo objterm, TablesInfo Objupdate, string term, params SqlParameter[] param)
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


        private static string getTerms(TablesInfo obj, ref List<SqlParameter> ht)
        {
            string term = string.Empty;
            if (obj.Tableid > 0)//TableID
            {
                term += " and tableid=@tableid ";
                if (ESP.Media.Access.Utilities.Common.Find(ht, "@tableid") == -1)
                {
                    SqlParameter p = new SqlParameter("@tableid", SqlDbType.Int, 4);
                    p.Value = obj.Tableid;
                    ht.Add(p);
                }
            }
            if (obj.Tablename != null && obj.Tablename.Trim().Length > 0)
            {
                term += " and tablename=@tablename ";
                if (ESP.Media.Access.Utilities.Common.Find(ht, "@tablename") == -1)
                {
                    SqlParameter p = new SqlParameter("@tablename", SqlDbType.NVarChar, 100);
                    p.Value = obj.Tablename;
                    ht.Add(p);
                }
            }
            if (obj.Alttablename != null && obj.Alttablename.Trim().Length > 0)
            {
                term += " and alttablename=@alttablename ";
                if (ESP.Media.Access.Utilities.Common.Find(ht, "@alttablename") == -1)
                {
                    SqlParameter p = new SqlParameter("@alttablename", SqlDbType.NVarChar, 200);
                    p.Value = obj.Alttablename;
                    ht.Add(p);
                }
            }
            return term;
        }
        #endregion
        #region 查询
        private static string getQueryTerms(TablesInfo obj, ref Hashtable ht)
        {
            string term = string.Empty;
            if (obj.Tableid > 0)//TableID
            {
                term += " and a.tableid=@tableid ";
                if (!ht.ContainsKey("@tableid"))
                {
                    ht.Add("@tableid", obj.Tableid);
                }
            }
            if (obj.Tablename != null && obj.Tablename.Trim().Length > 0)
            {
                term += " and a.tablename=@tablename ";
                if (!ht.ContainsKey("@tablename"))
                {
                    ht.Add("@tablename", obj.Tablename);
                }
            }
            if (obj.Alttablename != null && obj.Alttablename.Trim().Length > 0)
            {
                term += " and a.alttablename=@alttablename ";
                if (!ht.ContainsKey("@alttablename"))
                {
                    ht.Add("@alttablename", obj.Alttablename);
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
            string sql = @"select {0} a.tableid as tableid,a.tablename as tablename,a.alttablename as alttablename {1} from media_tables as a {2} where 1=1 {3} ";
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


        public static DataTable QueryInfoByObj(TablesInfo obj, string terms, Hashtable ht)
        {
            if (ht == null)
            {
                ht = new Hashtable();
            }
            SqlParameter[] para = ESP.Media.Access.Utilities.Common.DictToSqlParam(ht);
            return QueryInfoByObj(obj, terms, para);
        }


        public static DataTable QueryInfoByObj(TablesInfo obj, string terms, params SqlParameter[] param)
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
        public static TablesInfo Load(int id)
        {
            DataTable dt = null;
            dt = QueryInfo(" and a.tableid=@id ", new SqlParameter("@id", id));
            if (dt != null && dt.Rows.Count > 0)
            {
                return setObject(dt.Rows[0]);
            }
            return null;
        }


        public static TablesInfo Load(int id, SqlTransaction trans)
        {
            DataTable dt = null;
            dt = QueryInfo(trans, " and a.tableid=@id ", new SqlParameter("@id", id));
            if (dt != null && dt.Rows.Count > 0)
            {
                return setObject(dt.Rows[0]);
            }
            return null;
        }


        public static TablesInfo setObject(DataRow dr)
        {
            TablesInfo obj = new TablesInfo();
            if (dr.Table.Columns.Contains("tableid") && dr["tableid"] != DBNull.Value)//tableid
            {
                obj.Tableid = Convert.ToInt32(dr["tableid"]);
            }
            if (dr.Table.Columns.Contains("tablename") && dr["tablename"] != DBNull.Value)//表英文名称
            {
                obj.Tablename = (dr["tablename"]).ToString();
            }
            if (dr.Table.Columns.Contains("alttablename") && dr["alttablename"] != DBNull.Value)//表中文名称
            {
                obj.Alttablename = (dr["alttablename"]).ToString();
            }
            return obj;
        }
        #endregion
    }
}
