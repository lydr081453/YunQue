using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.Collections.Generic;

using ESP.Media.Access.Utilities;
using ESP.Media.Entity;
namespace ESP.Media.DataAccess
{
    public class ClientCategoryDataProvider
    {
        #region 构造函数
        public ClientCategoryDataProvider()
        {
        }
        #endregion

        #region 查询
        private static string getQueryTerms(ClientCategoryInfo obj, ref Hashtable ht)
        {
            string term = string.Empty;
            if (obj.CategoryID > 0)//City_ID
            {
                term += " and a.CategoryID=@CategoryID ";
                if (!ht.ContainsKey("@CategoryID"))
                {
                    ht.Add("@CategoryID", obj.CategoryID);
                }
            }
            if (obj.CategoryName != null && obj.CategoryName.Trim().Length > 0)
            {
                term += " and a.CategoryName=@CategoryName ";
                if (!ht.ContainsKey("@CategoryName"))
                {
                    ht.Add("@CategoryName", obj.CategoryName);
                }
            }
            if (obj.SortID > 0)
            {
                term += " and a.SortID=@SortID ";
                if (!ht.ContainsKey("@SortID"))
                {
                    ht.Add("@SortID", obj.SortID);
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
            string sql = @"select {0} a.categoryid as categoryid,a.categoryname as categoryname,a.sortid as sortid {1} from media_ClientCategory as a {2} where 1=1 {3} ";
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


        public static DataTable QueryInfoByObj(ClientCategoryInfo obj, string terms, Hashtable ht)
        {
            if (ht == null)
            {
                ht = new Hashtable();
            }
            SqlParameter[] para = ESP.Media.Access.Utilities.Common.DictToSqlParam(ht);
            return QueryInfoByObj(obj, terms, para);
        }


        public static DataTable QueryInfoByObj(ClientCategoryInfo obj, string terms, params SqlParameter[] param)
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
        public static ClientCategoryInfo Load(int categoryid)
        {
            DataTable dt = null;
            dt = QueryInfo(" and a.categoryid=@categoryid ", new SqlParameter("@categoryid", categoryid));
            if (dt != null && dt.Rows.Count > 0)
            {
                return setObject(dt.Rows[0]);
            }
            return null;
        }


        public static ClientCategoryInfo Load(int categoryid, SqlTransaction trans)
        {
            DataTable dt = null;
            dt = QueryInfo(trans, " and a.categoryid=@categoryid ", new SqlParameter("@categoryid", categoryid));
            if (dt != null && dt.Rows.Count > 0)
            {
                return setObject(dt.Rows[0]);
            }
            return null;
        }


        public static ClientCategoryInfo setObject(DataRow dr)
        {
            ClientCategoryInfo obj = new ClientCategoryInfo();
            if (dr.Table.Columns.Contains("categoryid") && dr["categoryid"] != DBNull.Value)//ID
            {
                obj.CategoryID = Convert.ToInt32(dr["categoryid"]);
            }
            if (dr.Table.Columns.Contains("categoryname") && dr["categoryname"] != DBNull.Value)//名称
            {
                obj.CategoryName = (dr["categoryname"]).ToString();
            }
            if (dr.Table.Columns.Contains("sortid") && dr["sortid"] != DBNull.Value)//
            {
                obj.SortID = Convert.ToInt32((dr["sortid"]));
            }

            return obj;
        }
        #endregion

    }
}
