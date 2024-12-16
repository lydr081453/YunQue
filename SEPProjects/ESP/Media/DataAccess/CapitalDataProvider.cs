using System;
using System.Data;
using System.Data.SqlClient;
using ESP.Media.Entity;
using ESP.Media.Access.Utilities;
using System.Collections;
using System.Collections.Generic;
namespace ESP.Media.DataAccess
{
    public class CapitalDataProvider
    {
        #region 构造函数
        public CapitalDataProvider()
        {
        }
        #endregion
        #region 插入
        //插入字符串
        private static string strinsert(CapitalInfo obj, ref List<SqlParameter> ht)
        {
            if (ht == null)
            {
                ht = new List<SqlParameter>();
            }
            string sql = @"insert into media_capital (cityname,province) values (@cityname,@province);select @@IDENTITY as rowNum;";
            SqlParameter param_Cityname = new SqlParameter("@Cityname", SqlDbType.NVarChar, 20);
            param_Cityname.Value = obj.Cityname;
            ht.Add(param_Cityname);
            SqlParameter param_Province = new SqlParameter("@Province", SqlDbType.NVarChar, 20);
            param_Province.Value = obj.Province;
            ht.Add(param_Province);
            return sql;
        }


        //插入一条记录
        public static int insertinfo(CapitalInfo obj, SqlTransaction trans)
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
        public static int insertinfo(CapitalInfo obj)
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
            string sql = "delete media_capital where cityID=@id";
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
        public static string getUpdateString(CapitalInfo objTerm, CapitalInfo Objupdate, string term, ref List<SqlParameter> ht, params SqlParameter[] param)
        {
            string sql = "update media_capital set cityname=@cityname,province=@province where 1=1 ";
            SqlParameter param_cityname = new SqlParameter("@cityname", SqlDbType.NVarChar, 20);
            param_cityname.Value = Objupdate.Cityname;
            ht.Add(param_cityname);
            SqlParameter param_province = new SqlParameter("@province", SqlDbType.NVarChar, 20);
            param_province.Value = Objupdate.Province;
            ht.Add(param_province);
            if (objTerm == null && (term == null || term.Trim().Length <= 0))
            {
                sql += " and cityID=@id ";
                if (ESP.Media.Access.Utilities.Common.Find(ht, "@id") == -1)
                {
                    SqlParameter p = new SqlParameter("@id", SqlDbType.Int, 4);
                    p.Value = Objupdate.Cityid;
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
        public static bool updateInfo(SqlTransaction trans, CapitalInfo objterm, CapitalInfo Objupdate, string term, params SqlParameter[] param)
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
        public static bool updateInfo(CapitalInfo objterm, CapitalInfo Objupdate, string term, params SqlParameter[] param)
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


        private static string getTerms(CapitalInfo obj, ref List<SqlParameter> ht)
        {
            string term = string.Empty;
            if (obj.Cityid > 0)//cityID
            {
                term += " and cityid=@cityid ";
                if (ESP.Media.Access.Utilities.Common.Find(ht, "@cityid") == -1)
                {
                    SqlParameter p = new SqlParameter("@cityid", SqlDbType.Int, 4);
                    p.Value = obj.Cityid;
                    ht.Add(p);
                }
            }
            if (obj.Cityname != null && obj.Cityname.Trim().Length > 0)
            {
                term += " and cityname=@cityname ";
                if (ESP.Media.Access.Utilities.Common.Find(ht, "@cityname") == -1)
                {
                    SqlParameter p = new SqlParameter("@cityname", SqlDbType.NVarChar, 20);
                    p.Value = obj.Cityname;
                    ht.Add(p);
                }
            }
            if (obj.Province != null && obj.Province.Trim().Length > 0)
            {
                term += " and province=@province ";
                if (ESP.Media.Access.Utilities.Common.Find(ht, "@province") == -1)
                {
                    SqlParameter p = new SqlParameter("@province", SqlDbType.NVarChar, 20);
                    p.Value = obj.Province;
                    ht.Add(p);
                }
            }
            return term;
        }
        #endregion
        #region 查询
        private static string getQueryTerms(CapitalInfo obj, ref Hashtable ht)
        {
            string term = string.Empty;
            if (obj.Cityid > 0)//cityID
            {
                term += " and a.cityid=@cityid ";
                if (!ht.ContainsKey("@cityid"))
                {
                    ht.Add("@cityid", obj.Cityid);
                }
            }
            if (obj.Cityname != null && obj.Cityname.Trim().Length > 0)
            {
                term += " and a.cityname=@cityname ";
                if (!ht.ContainsKey("@cityname"))
                {
                    ht.Add("@cityname", obj.Cityname);
                }
            }
            if (obj.Province != null && obj.Province.Trim().Length > 0)
            {
                term += " and a.province=@province ";
                if (!ht.ContainsKey("@province"))
                {
                    ht.Add("@province", obj.Province);
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
            string sql = @"select {0} a.cityid as cityid,a.cityname as cityname,a.province as province {1} from media_capital as a {2} where 1=1 {3} ";
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


        public static DataTable QueryInfoByObj(CapitalInfo obj, string terms, Hashtable ht)
        {
            if (ht == null)
            {
                ht = new Hashtable();
            }
            SqlParameter[] para = ESP.Media.Access.Utilities.Common.DictToSqlParam(ht);
            return QueryInfoByObj(obj, terms, para);
        }


        public static DataTable QueryInfoByObj(CapitalInfo obj, string terms, params SqlParameter[] param)
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
        public static CapitalInfo Load(int id)
        {
            DataTable dt = null;
            dt = QueryInfo(" and a.cityid=@id ", new SqlParameter("@id", id));
            if (dt != null && dt.Rows.Count > 0)
            {
                return setObject(dt.Rows[0]);
            }
            return null;
        }


        public static CapitalInfo Load(int id, SqlTransaction trans)
        {
            DataTable dt = null;
            dt = QueryInfo(trans, " and a.cityid=@id ", new SqlParameter("@id", id));
            if (dt != null && dt.Rows.Count > 0)
            {
                return setObject(dt.Rows[0]);
            }
            return null;
        }


        public static CapitalInfo setObject(DataRow dr)
        {
            CapitalInfo obj = new CapitalInfo();
            if (dr.Table.Columns.Contains("cityid") && dr["cityid"] != DBNull.Value)//cityid
            {
                obj.Cityid = Convert.ToInt32(dr["cityid"]);
            }
            if (dr.Table.Columns.Contains("cityname") && dr["cityname"] != DBNull.Value)//cityname
            {
                obj.Cityname = (dr["cityname"]).ToString();
            }
            if (dr.Table.Columns.Contains("province") && dr["province"] != DBNull.Value)//province
            {
                obj.Province = (dr["province"]).ToString();
            }
            return obj;
        }
        #endregion
    }
}
