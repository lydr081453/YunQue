using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using ESP.Media.Access.Utilities;
using System.Collections.Generic;
using ESP.Media.Entity;
namespace ESP.Media.DataAccess
{
    public class ProvinceDataProvider
    {
        #region ���캯��
        public ProvinceDataProvider()
        {
        }
        #endregion
        #region ����
        //�����ַ���
        private static string strinsert(ProvinceInfo obj, ref List<SqlParameter> ht)
        {
            if (ht == null)
            {
                ht = new List<SqlParameter>();
            }
            string sql = @"insert into media_province (province_id,province_name,country_id) values (@province_id,@province_name,@country_id);select @@IDENTITY as rowNum;";
            SqlParameter param_Province_id = new SqlParameter("@Province_id", SqlDbType.Int, 4);
            param_Province_id.Value = obj.Province_id;
            ht.Add(param_Province_id);
            SqlParameter param_Province_name = new SqlParameter("@Province_name", SqlDbType.NVarChar, 100);
            param_Province_name.Value = obj.Province_name;
            ht.Add(param_Province_name);
            SqlParameter param_Country_id = new SqlParameter("@Country_id", SqlDbType.Int, 4);
            param_Country_id.Value = obj.Country_id;
            ht.Add(param_Country_id);
            return sql;
        }


        //����һ����¼
        public static int insertinfo(ProvinceInfo obj, SqlTransaction trans)
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


        //����һ����¼
        public static int insertinfo(ProvinceInfo obj)
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
        #region ɾ��
        //ɾ������
        public static bool DeleteInfo(int id, SqlTransaction trans)
        {
            int rows = 0;
            string sql = "delete media_province";
            SqlParameter param = new SqlParameter("@id", SqlDbType.Int);
            param.Value = id;
            rows = SqlHelper.ExecuteNonQuery(trans, CommandType.Text, sql, param);
            if (rows > 0)
            {
                return true;
            }
            return false;
        }


        //ɾ������
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
        #region ����
        //����sql
        public static string getUpdateString(ProvinceInfo objTerm, ProvinceInfo Objupdate, string term, ref List<SqlParameter> ht, params SqlParameter[] param)
        {
            string sql = "update media_province set province_id=@province_id,province_name=@province_name,country_id=@country_id where 1=1 ";
            SqlParameter param_province_id = new SqlParameter("@province_id", SqlDbType.Int, 4);
            param_province_id.Value = Objupdate.Province_id;
            ht.Add(param_province_id);
            SqlParameter param_province_name = new SqlParameter("@province_name", SqlDbType.NVarChar, 100);
            param_province_name.Value = Objupdate.Province_name;
            ht.Add(param_province_name);
            SqlParameter param_country_id = new SqlParameter("@country_id", SqlDbType.Int, 4);
            param_country_id.Value = Objupdate.Country_id;
            ht.Add(param_country_id);
            if (objTerm == null && (term == null || term.Trim().Length <= 0))
            {
                sql += " and province_id=@id ";
                if (ESP.Media.Access.Utilities.Common.Find(ht, "@id") == -1)
                {
                    SqlParameter p = new SqlParameter("@id", SqlDbType.Int, 4);
                    p.Value = Objupdate.Province_id;
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


        //���²���
        public static bool updateInfo(SqlTransaction trans, ProvinceInfo objterm, ProvinceInfo Objupdate, string term, params SqlParameter[] param)
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


        //���²���
        public static bool updateInfo(ProvinceInfo objterm, ProvinceInfo Objupdate, string term, params SqlParameter[] param)
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


        private static string getTerms(ProvinceInfo obj, ref List<SqlParameter> ht)
        {
            string term = string.Empty;
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
            if (obj.Province_name != null && obj.Province_name.Trim().Length > 0)
            {
                term += " and province_name=@province_name ";
                if (ESP.Media.Access.Utilities.Common.Find(ht, "@province_name") == -1)
                {
                    SqlParameter p = new SqlParameter("@province_name", SqlDbType.NVarChar, 100);
                    p.Value = obj.Province_name;
                    ht.Add(p);
                }
            }
            if (obj.Country_id > 0)//Country_ID
            {
                term += " and country_id=@country_id ";
                if (ESP.Media.Access.Utilities.Common.Find(ht, "@country_id") == -1)
                {
                    SqlParameter p = new SqlParameter("@country_id", SqlDbType.Int, 4);
                    p.Value = obj.Country_id;
                    ht.Add(p);
                }
            }
            return term;
        }
        #endregion
        #region ��ѯ
        private static string getQueryTerms(ProvinceInfo obj, ref Hashtable ht)
        {
            string term = string.Empty;
            if (obj.Province_id > 0)//Province_ID
            {
                term += " and a.province_id=@province_id ";
                if (!ht.ContainsKey("@province_id"))
                {
                    ht.Add("@province_id", obj.Province_id);
                }
            }
            if (obj.Province_name != null && obj.Province_name.Trim().Length > 0)
            {
                term += " and a.province_name=@province_name ";
                if (!ht.ContainsKey("@province_name"))
                {
                    ht.Add("@province_name", obj.Province_name);
                }
            }
            if (obj.Country_id > 0)//Country_ID
            {
                term += " and a.country_id=@country_id ";
                if (!ht.ContainsKey("@country_id"))
                {
                    ht.Add("@country_id", obj.Country_id);
                }
            }
            return term;
        }
        //�õ���ѯ�ַ���
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
            string sql = @"select {0} a.province_id as province_id,a.province_name as province_name,a.country_id as country_id {1} from media_province as a {2} where 1=1 {3} ";
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


        public static DataTable QueryInfoByObj(ProvinceInfo obj, string terms, Hashtable ht)
        {
            if (ht == null)
            {
                ht = new Hashtable();
            }
            SqlParameter[] para = ESP.Media.Access.Utilities.Common.DictToSqlParam(ht);
            return QueryInfoByObj(obj, terms, para);
        }


        public static DataTable QueryInfoByObj(ProvinceInfo obj, string terms, params SqlParameter[] param)
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
        public static ProvinceInfo Load(int id)
        {
            DataTable dt = null;
            dt = QueryInfo(" and a.province_id=@id ", new SqlParameter("@id", id));
            if (dt != null && dt.Rows.Count > 0)
            {
                return setObject(dt.Rows[0]);
            }
            return null;
        }


        public static ProvinceInfo Load(int id, SqlTransaction trans)
        {
            DataTable dt = null;
            dt = QueryInfo(trans, " and a.province_id=@id ", new SqlParameter("@id", id));
            if (dt != null && dt.Rows.Count > 0)
            {
                return setObject(dt.Rows[0]);
            }
            return null;
        }


        public static ProvinceInfo setObject(DataRow dr)
        {
            ProvinceInfo obj = new ProvinceInfo();
            if (dr.Table.Columns.Contains("province_id") && dr["province_id"] != DBNull.Value)//id
            {
                obj.Province_id = Convert.ToInt32(dr["province_id"]);
            }
            if (dr.Table.Columns.Contains("province_name") && dr["province_name"] != DBNull.Value)//ʡ����
            {
                obj.Province_name = (dr["province_name"]).ToString();
            }
            if (dr.Table.Columns.Contains("country_id") && dr["country_id"] != DBNull.Value)//����id
            {
                obj.Country_id = Convert.ToInt32(dr["country_id"]);
            }
            return obj;
        }
        #endregion
    }
}
