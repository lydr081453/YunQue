using System;
using System.Data;
using System.Data.SqlClient;
using ESP.Media.Entity;
using System.Collections;
using ESP.Media.Access.Utilities;
using System.Collections.Generic;
namespace ESP.Media.DataAccess
{
    public class CityDataProvider
    {
        #region 构造函数
        public CityDataProvider()
        {
        }
        #endregion
        #region 插入
        //插入字符串
        private static string strinsert(CityInfo obj, ref List<SqlParameter> ht)
        {
            if (ht == null)
            {
                ht = new List<SqlParameter>();
            }
            string sql = @"insert into media_city (city_id,city_name,city_level,province_id) values (@city_id,@city_name,@city_level,@province_id);select @@IDENTITY as rowNum;";
            SqlParameter param_City_id = new SqlParameter("@City_id", SqlDbType.Int, 4);
            param_City_id.Value = obj.City_id;
            ht.Add(param_City_id);
            SqlParameter param_City_name = new SqlParameter("@City_name", SqlDbType.NVarChar, 100);
            param_City_name.Value = obj.City_name;
            ht.Add(param_City_name);
            SqlParameter param_City_level = new SqlParameter("@City_level", SqlDbType.NVarChar, 100);
            param_City_level.Value = obj.City_level;
            ht.Add(param_City_level);
            SqlParameter param_Province_id = new SqlParameter("@Province_id", SqlDbType.Int, 4);
            param_Province_id.Value = obj.Province_id;
            ht.Add(param_Province_id);
            return sql;
        }


        //插入一条记录
        public static int insertinfo(CityInfo obj, SqlTransaction trans)
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
        public static int insertinfo(CityInfo obj)
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
            string sql = "delete media_city";
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
        public static string getUpdateString(CityInfo objTerm, CityInfo Objupdate, string term, ref List<SqlParameter> ht, params SqlParameter[] param)
        {
            string sql = "update media_city set city_id=@city_id,city_name=@city_name,city_level=@city_level,province_id=@province_id where 1=1 ";
            SqlParameter param_city_id = new SqlParameter("@city_id", SqlDbType.Int, 4);
            param_city_id.Value = Objupdate.City_id;
            ht.Add(param_city_id);
            SqlParameter param_city_name = new SqlParameter("@city_name", SqlDbType.NVarChar, 100);
            param_city_name.Value = Objupdate.City_name;
            ht.Add(param_city_name);
            SqlParameter param_city_level = new SqlParameter("@city_level", SqlDbType.NVarChar, 100);
            param_city_level.Value = Objupdate.City_level;
            ht.Add(param_city_level);
            SqlParameter param_province_id = new SqlParameter("@province_id", SqlDbType.Int, 4);
            param_province_id.Value = Objupdate.Province_id;
            ht.Add(param_province_id);
            if (objTerm == null && (term == null || term.Trim().Length <= 0))
            {
                sql += " and city_id=@id ";
                if (ESP.Media.Access.Utilities.Common.Find(ht, "@id") == -1)
                {
                    SqlParameter p = new SqlParameter("@id", SqlDbType.Int, 4);
                    p.Value = Objupdate.City_id;
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
        public static bool updateInfo(SqlTransaction trans, CityInfo objterm, CityInfo Objupdate, string term, params SqlParameter[] param)
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
        public static bool updateInfo(CityInfo objterm, CityInfo Objupdate, string term, params SqlParameter[] param)
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


        private static string getTerms(CityInfo obj, ref List<SqlParameter> ht)
        {
            string term = string.Empty;
            if (obj.City_id > 0)//City_ID
            {
                term += " and city_id=@city_id ";
                if (ESP.Media.Access.Utilities.Common.Find(ht, "@city_id") == -1)
                {
                    SqlParameter p = new SqlParameter("@city_id", SqlDbType.Int, 4);
                    p.Value = obj.City_id;
                    ht.Add(p);
                }
            }
            if (obj.City_name != null && obj.City_name.Trim().Length > 0)
            {
                term += " and city_name=@city_name ";
                if (ESP.Media.Access.Utilities.Common.Find(ht, "@city_name") == -1)
                {
                    SqlParameter p = new SqlParameter("@city_name", SqlDbType.NVarChar, 100);
                    p.Value = obj.City_name;
                    ht.Add(p);
                }
            }
            if (obj.City_level != null && obj.City_level.Trim().Length > 0)
            {
                term += " and city_level=@city_level ";
                if (ESP.Media.Access.Utilities.Common.Find(ht, "@city_level") == -1)
                {
                    SqlParameter p = new SqlParameter("@city_level", SqlDbType.NVarChar, 100);
                    p.Value = obj.City_level;
                    ht.Add(p);
                }
            }
            if (obj.Province_id > 0)//Province_ID
            {
                term += " and province_id=@province_id ";
                if (ESP.Media.Access.Utilities.Common.Find(ht, "@province_id") == -1)
                {
                    SqlParameter p = new SqlParameter("@province_id", SqlDbType.Int, 4);
                    p.Value = obj.Province_id;
                    ht.Add(p);
                }
            }
            return term;
        }
        #endregion
        #region 查询
        private static string getQueryTerms(CityInfo obj, ref Hashtable ht)
        {
            string term = string.Empty;
            if (obj.City_id > 0)//City_ID
            {
                term += " and a.city_id=@city_id ";
                if (!ht.ContainsKey("@city_id"))
                {
                    ht.Add("@city_id", obj.City_id);
                }
            }
            if (obj.City_name != null && obj.City_name.Trim().Length > 0)
            {
                term += " and a.city_name=@city_name ";
                if (!ht.ContainsKey("@city_name"))
                {
                    ht.Add("@city_name", obj.City_name);
                }
            }
            if (obj.City_level != null && obj.City_level.Trim().Length > 0)
            {
                term += " and a.city_level=@city_level ";
                if (!ht.ContainsKey("@city_level"))
                {
                    ht.Add("@city_level", obj.City_level);
                }
            }
            if (obj.Province_id > 0)//Province_ID
            {
                term += " and a.province_id=@province_id ";
                if (!ht.ContainsKey("@province_id"))
                {
                    ht.Add("@province_id", obj.Province_id);
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
            string sql = @"select {0} a.city_id as city_id,a.city_name as city_name,a.city_level as city_level,a.province_id as province_id {1} from media_city as a {2} where 1=1 {3} ";
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


        public static DataTable QueryInfoByObj(CityInfo obj, string terms, Hashtable ht)
        {
            if (ht == null)
            {
                ht = new Hashtable();
            }
            SqlParameter[] para = ESP.Media.Access.Utilities.Common.DictToSqlParam(ht);
            return QueryInfoByObj(obj, terms, para);
        }


        public static DataTable QueryInfoByObj(CityInfo obj, string terms, params SqlParameter[] param)
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
        public static CityInfo Load(int id)
        {
            DataTable dt = null;
            dt = QueryInfo(" and a.city_id=@id ", new SqlParameter("@id", id));
            if (dt != null && dt.Rows.Count > 0)
            {
                return setObject(dt.Rows[0]);
            }
            return null;
        }


        public static CityInfo Load(int id, SqlTransaction trans)
        {
            DataTable dt = null;
            dt = QueryInfo(trans, " and a.city_id=@id ", new SqlParameter("@id", id));
            if (dt != null && dt.Rows.Count > 0)
            {
                return setObject(dt.Rows[0]);
            }
            return null;
        }


        public static CityInfo setObject(DataRow dr)
        {
            CityInfo obj = new CityInfo();
            if (dr.Table.Columns.Contains("city_id") && dr["city_id"] != DBNull.Value)//城市ID
            {
                obj.City_id = Convert.ToInt32(dr["city_id"]);
            }
            if (dr.Table.Columns.Contains("city_name") && dr["city_name"] != DBNull.Value)//城市名称
            {
                obj.City_name = (dr["city_name"]).ToString();
            }
            if (dr.Table.Columns.Contains("city_level") && dr["city_level"] != DBNull.Value)//city_level
            {
                obj.City_level = (dr["city_level"]).ToString();
            }
            if (dr.Table.Columns.Contains("province_id") && dr["province_id"] != DBNull.Value)//省id
            {
                obj.Province_id = Convert.ToInt32(dr["province_id"]);
            }
            return obj;
        }
        #endregion
    }
}
