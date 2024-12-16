using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.Collections.Generic;
using ESP.Media.Access.Utilities;
using ESP.Media.Entity;
namespace ESP.Media.DataAccess
{
    public class CountryDataProvider
    {
        #region 构造函数
        public CountryDataProvider()
        {
        }
        #endregion
        #region 插入
        //插入字符串
        private static string strinsert(CountryInfo obj, ref List<SqlParameter> ht)
        {
            if (ht == null)
            {
                ht = new List<SqlParameter>();
            }
            string sql = @"insert into media_country (countryid,countrynum,countryname,regionattributeid) values (@countryid,@countrynum,@countryname,@regionattributeid);select @@IDENTITY as rowNum;";
            SqlParameter param_Countryid = new SqlParameter("@Countryid", SqlDbType.Int, 4);
            param_Countryid.Value = obj.Countryid;
            ht.Add(param_Countryid);
            SqlParameter param_Countrynum = new SqlParameter("@Countrynum", SqlDbType.NVarChar, 100);
            param_Countrynum.Value = obj.Countrynum;
            ht.Add(param_Countrynum);
            SqlParameter param_Countryname = new SqlParameter("@Countryname", SqlDbType.NVarChar, 100);
            param_Countryname.Value = obj.Countryname;
            ht.Add(param_Countryname);
            SqlParameter param_Regionattributeid = new SqlParameter("@Regionattributeid", SqlDbType.Int, 4);
            param_Regionattributeid.Value = obj.Regionattributeid;
            ht.Add(param_Regionattributeid);
            return sql;
        }


        //插入一条记录
        public static int insertinfo(CountryInfo obj, SqlTransaction trans)
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
        public static int insertinfo(CountryInfo obj)
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
            string sql = "delete media_country";
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
        public static string getUpdateString(CountryInfo objTerm, CountryInfo Objupdate, string term, ref List<SqlParameter> ht, params SqlParameter[] param)
        {
            string sql = "update media_country set countryid=@countryid,countrynum=@countrynum,countryname=@countryname,regionattributeid=@regionattributeid where 1=1 ";
            SqlParameter param_countryid = new SqlParameter("@countryid", SqlDbType.Int, 4);
            param_countryid.Value = Objupdate.Countryid;
            ht.Add(param_countryid);
            SqlParameter param_countrynum = new SqlParameter("@countrynum", SqlDbType.NVarChar, 100);
            param_countrynum.Value = Objupdate.Countrynum;
            ht.Add(param_countrynum);
            SqlParameter param_countryname = new SqlParameter("@countryname", SqlDbType.NVarChar, 100);
            param_countryname.Value = Objupdate.Countryname;
            ht.Add(param_countryname);
            SqlParameter param_regionattributeid = new SqlParameter("@regionattributeid", SqlDbType.Int, 4);
            param_regionattributeid.Value = Objupdate.Regionattributeid;
            ht.Add(param_regionattributeid);
            if (objTerm == null && (term == null || term.Trim().Length <= 0))
            {
                sql += " and countryid=@id ";
                if (ESP.Media.Access.Utilities.Common.Find(ht, "@id") == -1)
                {
                    SqlParameter p = new SqlParameter("@id", SqlDbType.Int, 4);
                    p.Value = Objupdate.Countryid;
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
        public static bool updateInfo(SqlTransaction trans, CountryInfo objterm, CountryInfo Objupdate, string term, params SqlParameter[] param)
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
        public static bool updateInfo(CountryInfo objterm, CountryInfo Objupdate, string term, params SqlParameter[] param)
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


        private static string getTerms(CountryInfo obj, ref List<SqlParameter> ht)
        {
            string term = string.Empty;
            if (obj.Countryid > 0)//CountryID
            {
                term += " and countryid=@countryid ";
                if (ESP.Media.Access.Utilities.Common.Find(ht, "@countryid") == -1)
                {
                    SqlParameter p = new SqlParameter("@countryid", SqlDbType.Int, 4);
                    p.Value = obj.Countryid;
                    ht.Add(p);
                }
            }
            if (obj.Countrynum != null && obj.Countrynum.Trim().Length > 0)
            {
                term += " and countrynum=@countrynum ";
                if (ESP.Media.Access.Utilities.Common.Find(ht, "@countrynum") == -1)
                {
                    SqlParameter p = new SqlParameter("@countrynum", SqlDbType.NVarChar, 100);
                    p.Value = obj.Countrynum;
                    ht.Add(p);
                }
            }
            if (obj.Countryname != null && obj.Countryname.Trim().Length > 0)
            {
                term += " and countryname=@countryname ";
                if (ESP.Media.Access.Utilities.Common.Find(ht, "@countryname") == -1)
                {
                    SqlParameter p = new SqlParameter("@countryname", SqlDbType.NVarChar, 100);
                    p.Value = obj.Countryname;
                    ht.Add(p);
                }
            }
            if (obj.Regionattributeid > 0)//RegionAttributeID
            {
                term += " and regionattributeid=@regionattributeid ";
                if (ESP.Media.Access.Utilities.Common.Find(ht, "@regionattributeid") == -1)
                {
                    SqlParameter p = new SqlParameter("@regionattributeid", SqlDbType.Int, 4);
                    p.Value = obj.Regionattributeid;
                    ht.Add(p);
                }
            }
            return term;
        }
        #endregion
        #region 查询
        private static string getQueryTerms(CountryInfo obj, ref Hashtable ht)
        {
            string term = string.Empty;
            if (obj.Countryid > 0)//CountryID
            {
                term += " and a.countryid=@countryid ";
                if (!ht.ContainsKey("@countryid"))
                {
                    ht.Add("@countryid", obj.Countryid);
                }
            }
            if (obj.Countrynum != null && obj.Countrynum.Trim().Length > 0)
            {
                term += " and a.countrynum=@countrynum ";
                if (!ht.ContainsKey("@countrynum"))
                {
                    ht.Add("@countrynum", obj.Countrynum);
                }
            }
            if (obj.Countryname != null && obj.Countryname.Trim().Length > 0)
            {
                term += " and a.countryname=@countryname ";
                if (!ht.ContainsKey("@countryname"))
                {
                    ht.Add("@countryname", obj.Countryname);
                }
            }
            if (obj.Regionattributeid > 0)//RegionAttributeID
            {
                term += " and a.regionattributeid=@regionattributeid ";
                if (!ht.ContainsKey("@regionattributeid"))
                {
                    ht.Add("@regionattributeid", obj.Regionattributeid);
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
            string sql = @"select {0} a.countryid as countryid,a.countrynum as countrynum,a.countryname as countryname,a.regionattributeid as regionattributeid {1} from media_country as a {2} where 1=1 {3} ";
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


        public static DataTable QueryInfoByObj(CountryInfo obj, string terms, Hashtable ht)
        {
            if (ht == null)
            {
                ht = new Hashtable();
            }
            SqlParameter[] para = ESP.Media.Access.Utilities.Common.DictToSqlParam(ht);
            return QueryInfoByObj(obj, terms, para);
        }


        public static DataTable QueryInfoByObj(CountryInfo obj, string terms, params SqlParameter[] param)
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
        public static CountryInfo Load(int id)
        {
            DataTable dt = null;
            dt = QueryInfo(" and a.countryid=@id ", new SqlParameter("@id", id));
            if (dt != null && dt.Rows.Count > 0)
            {
                return setObject(dt.Rows[0]);
            }
            return null;
        }


        public static CountryInfo Load(int id, SqlTransaction trans)
        {
            DataTable dt = null;
            dt = QueryInfo(trans, " and a.countryid=@id ", new SqlParameter("@id", id));
            if (dt != null && dt.Rows.Count > 0)
            {
                return setObject(dt.Rows[0]);
            }
            return null;
        }


        public static CountryInfo setObject(DataRow dr)
        {
            CountryInfo obj = new CountryInfo();
            if (dr.Table.Columns.Contains("countryid") && dr["countryid"] != DBNull.Value)//countryid
            {
                obj.Countryid = Convert.ToInt32(dr["countryid"]);
            }
            if (dr.Table.Columns.Contains("countrynum") && dr["countrynum"] != DBNull.Value)//countrynum
            {
                obj.Countrynum = (dr["countrynum"]).ToString();
            }
            if (dr.Table.Columns.Contains("countryname") && dr["countryname"] != DBNull.Value)//国家名字
            {
                obj.Countryname = (dr["countryname"]).ToString();
            }
            if (dr.Table.Columns.Contains("regionattributeid") && dr["regionattributeid"] != DBNull.Value)//地域属性ID
            {
                obj.Regionattributeid = Convert.ToInt32(dr["regionattributeid"]);
            }
            return obj;
        }
        #endregion
    }
}
