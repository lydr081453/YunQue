using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.Collections.Generic;
using ESP.Media.Entity;
using ESP.Media.Access.Utilities;
namespace ESP.Media.DataAccess
{
    public class ProjectcashpaymentbillrelationDataProvider
    {
        #region 构造函数
        public ProjectcashpaymentbillrelationDataProvider()
        {
        }
        #endregion
        #region 插入
        //插入字符串
        private static string strinsert(ProjectcashpaymentbillrelationInfo obj, ref List<SqlParameter> ht)
        {
            if (ht == null)
            {
                ht = new List<SqlParameter>();
            }
            string sql = @"insert into media_projectcashpaymentbillrelation (cashpaymentbillid,projectid,del) values (@cashpaymentbillid,@projectid,@del);select @@IDENTITY as rowNum;";
            SqlParameter param_Cashpaymentbillid = new SqlParameter("@Cashpaymentbillid", SqlDbType.Int, 4);
            param_Cashpaymentbillid.Value = obj.Cashpaymentbillid;
            ht.Add(param_Cashpaymentbillid);
            SqlParameter param_Projectid = new SqlParameter("@Projectid", SqlDbType.Int, 4);
            param_Projectid.Value = obj.Projectid;
            ht.Add(param_Projectid);
            SqlParameter param_Del = new SqlParameter("@Del", SqlDbType.Int, 4);
            param_Del.Value = obj.Del;
            ht.Add(param_Del);
            return sql;
        }


        //插入一条记录
        public static int insertinfo(ProjectcashpaymentbillrelationInfo obj, SqlTransaction trans)
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
        public static int insertinfo(ProjectcashpaymentbillrelationInfo obj)
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
            string sql = "delete media_projectcashpaymentbillrelation where id=@id";
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
        public static string getUpdateString(ProjectcashpaymentbillrelationInfo objTerm, ProjectcashpaymentbillrelationInfo Objupdate, string term, ref List<SqlParameter> ht, params SqlParameter[] param)
        {
            string sql = "update media_projectcashpaymentbillrelation set cashpaymentbillid=@cashpaymentbillid,projectid=@projectid,del=@del where 1=1 ";
            SqlParameter param_cashpaymentbillid = new SqlParameter("@cashpaymentbillid", SqlDbType.Int, 4);
            param_cashpaymentbillid.Value = Objupdate.Cashpaymentbillid;
            ht.Add(param_cashpaymentbillid);
            SqlParameter param_projectid = new SqlParameter("@projectid", SqlDbType.Int, 4);
            param_projectid.Value = Objupdate.Projectid;
            ht.Add(param_projectid);
            SqlParameter param_del = new SqlParameter("@del", SqlDbType.Int, 4);
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
        public static bool updateInfo(SqlTransaction trans, ProjectcashpaymentbillrelationInfo objterm, ProjectcashpaymentbillrelationInfo Objupdate, string term, params SqlParameter[] param)
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
        public static bool updateInfo(ProjectcashpaymentbillrelationInfo objterm, ProjectcashpaymentbillrelationInfo Objupdate, string term, params SqlParameter[] param)
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


        private static string getTerms(ProjectcashpaymentbillrelationInfo obj, ref List<SqlParameter> ht)
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
            if (obj.Cashpaymentbillid > 0)//cashpaymentbillid
            {
                term += " and cashpaymentbillid=@cashpaymentbillid ";
                if (ESP.Media.Access.Utilities.Common.Find(ht, "@cashpaymentbillid") == -1)
                {
                    SqlParameter p = new SqlParameter("@cashpaymentbillid", SqlDbType.Int, 4);
                    p.Value = obj.Cashpaymentbillid;
                    ht.Add(p);
                }
            }
            if (obj.Projectid > 0)//projectid
            {
                term += " and projectid=@projectid ";
                if (ESP.Media.Access.Utilities.Common.Find(ht, "@projectid") == -1)
                {
                    SqlParameter p = new SqlParameter("@projectid", SqlDbType.Int, 4);
                    p.Value = obj.Projectid;
                    ht.Add(p);
                }
            }
            if (obj.Del > 0)//del
            {
                term += " and del=@del ";
                if (ESP.Media.Access.Utilities.Common.Find(ht, "@del") == -1)
                {
                    SqlParameter p = new SqlParameter("@del", SqlDbType.Int, 4);
                    p.Value = obj.Del;
                    ht.Add(p);
                }
            }
            return term;
        }
        #endregion
        #region 查询
        private static string getQueryTerms(ProjectcashpaymentbillrelationInfo obj, ref Hashtable ht)
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
            if (obj.Cashpaymentbillid > 0)//cashpaymentbillid
            {
                term += " and a.cashpaymentbillid=@cashpaymentbillid ";
                if (!ht.ContainsKey("@cashpaymentbillid"))
                {
                    ht.Add("@cashpaymentbillid", obj.Cashpaymentbillid);
                }
            }
            if (obj.Projectid > 0)//projectid
            {
                term += " and a.projectid=@projectid ";
                if (!ht.ContainsKey("@projectid"))
                {
                    ht.Add("@projectid", obj.Projectid);
                }
            }
            if (obj.Del > 0)//del
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
            string sql = @"select {0} a.id as id,a.cashpaymentbillid as cashpaymentbillid,a.projectid as projectid,a.del as del {1} from media_projectcashpaymentbillrelation as a {2} where 1=1 {3} ";
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


        public static DataTable QueryInfoByObj(ProjectcashpaymentbillrelationInfo obj, string terms, Hashtable ht)
        {
            if (ht == null)
            {
                ht = new Hashtable();
            }
            SqlParameter[] para = ESP.Media.Access.Utilities.Common.DictToSqlParam(ht);
            return QueryInfoByObj(obj, terms, para);
        }


        public static DataTable QueryInfoByObj(ProjectcashpaymentbillrelationInfo obj, string terms, params SqlParameter[] param)
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
        public static ProjectcashpaymentbillrelationInfo Load(int id)
        {
            DataTable dt = null;
            dt = QueryInfo(" and a.id=@id ", new SqlParameter("@id", id));
            if (dt != null && dt.Rows.Count > 0)
            {
                return setObject(dt.Rows[0]);
            }
            return null;
        }


        public static ProjectcashpaymentbillrelationInfo Load(int id, SqlTransaction trans)
        {
            DataTable dt = null;
            dt = QueryInfo(trans, " and a.id=@id ", new SqlParameter("@id", id));
            if (dt != null && dt.Rows.Count > 0)
            {
                return setObject(dt.Rows[0]);
            }
            return null;
        }


        public static ProjectcashpaymentbillrelationInfo setObject(DataRow dr)
        {
            ProjectcashpaymentbillrelationInfo obj = new ProjectcashpaymentbillrelationInfo();
            if (dr.Table.Columns.Contains("id") && dr["id"] != DBNull.Value)//id
            {
                obj.Id = Convert.ToInt32(dr["id"]);
            }
            if (dr.Table.Columns.Contains("cashpaymentbillid") && dr["cashpaymentbillid"] != DBNull.Value)//cashpaymentbillid
            {
                obj.Cashpaymentbillid = Convert.ToInt32(dr["cashpaymentbillid"]);
            }
            if (dr.Table.Columns.Contains("projectid") && dr["projectid"] != DBNull.Value)//projectid
            {
                obj.Projectid = Convert.ToInt32(dr["projectid"]);
            }
            if (dr.Table.Columns.Contains("del") && dr["del"] != DBNull.Value)//del
            {
                obj.Del = Convert.ToInt32(dr["del"]);
            }
            return obj;
        }
        #endregion
    }
}
