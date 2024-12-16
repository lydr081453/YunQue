using System;
using System.Data;
using ESP.Media.Entity;
using System.Data.SqlClient;
using ESP.Media.Access.Utilities;
using System.Collections;
using System.Collections.Generic;
namespace ESP.Media.DataAccess
{
    public class IntegralruleDataProvider
    {
        #region 构造函数
        public IntegralruleDataProvider()
        {
        }
        #endregion
        #region 插入
        //插入字符串
        private static string strinsert(IntegralruleInfo obj, ref List<SqlParameter> ht)
        {
            if (ht == null)
            {
                ht = new List<SqlParameter>();
            }
            string sql = @"insert into media_integralrule (operateid,tableid,name,altname,integral,del) values (@operateid,@tableid,@name,@altname,@integral,@del);select @@IDENTITY as rowNum;";
            SqlParameter param_Operateid = new SqlParameter("@Operateid", SqlDbType.Int, 4);
            param_Operateid.Value = obj.Operateid;
            ht.Add(param_Operateid);
            SqlParameter param_Tableid = new SqlParameter("@Tableid", SqlDbType.Int, 4);
            param_Tableid.Value = obj.Tableid;
            ht.Add(param_Tableid);
            SqlParameter param_Name = new SqlParameter("@Name", SqlDbType.NVarChar, 100);
            param_Name.Value = obj.Name;
            ht.Add(param_Name);
            SqlParameter param_Altname = new SqlParameter("@Altname", SqlDbType.NVarChar, 100);
            param_Altname.Value = obj.Altname;
            ht.Add(param_Altname);
            SqlParameter param_Integral = new SqlParameter("@Integral", SqlDbType.Int, 4);
            param_Integral.Value = obj.Integral;
            ht.Add(param_Integral);
            SqlParameter param_Del = new SqlParameter("@Del", SqlDbType.SmallInt, 2);
            param_Del.Value = obj.Del;
            ht.Add(param_Del);
            return sql;
        }


        //插入一条记录
        public static int insertinfo(IntegralruleInfo obj, SqlTransaction trans)
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
        public static int insertinfo(IntegralruleInfo obj)
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
            string sql = "delete media_integralrule where id=@id";
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
        public static string getUpdateString(IntegralruleInfo objTerm, IntegralruleInfo Objupdate, string term, ref List<SqlParameter> ht, params SqlParameter[] param)
        {
            string sql = "update media_integralrule set operateid=@operateid,tableid=@tableid,name=@name,altname=@altname,integral=@integral,del=@del where 1=1 ";
            SqlParameter param_operateid = new SqlParameter("@operateid", SqlDbType.Int, 4);
            param_operateid.Value = Objupdate.Operateid;
            ht.Add(param_operateid);
            SqlParameter param_tableid = new SqlParameter("@tableid", SqlDbType.Int, 4);
            param_tableid.Value = Objupdate.Tableid;
            ht.Add(param_tableid);
            SqlParameter param_name = new SqlParameter("@name", SqlDbType.NVarChar, 100);
            param_name.Value = Objupdate.Name;
            ht.Add(param_name);
            SqlParameter param_altname = new SqlParameter("@altname", SqlDbType.NVarChar, 100);
            param_altname.Value = Objupdate.Altname;
            ht.Add(param_altname);
            SqlParameter param_integral = new SqlParameter("@integral", SqlDbType.Int, 4);
            param_integral.Value = Objupdate.Integral;
            ht.Add(param_integral);
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
        public static bool updateInfo(SqlTransaction trans, IntegralruleInfo objterm, IntegralruleInfo Objupdate, string term, params SqlParameter[] param)
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
        public static bool updateInfo(IntegralruleInfo objterm, IntegralruleInfo Objupdate, string term, params SqlParameter[] param)
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


        private static string getTerms(IntegralruleInfo obj, ref List<SqlParameter> ht)
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
            if (obj.Operateid > 0)//OperateID
            {
                term += " and operateid=@operateid ";
                if (ESP.Media.Access.Utilities.Common.Find(ht, "@operateid") == -1)
                {
                    SqlParameter p = new SqlParameter("@operateid", SqlDbType.Int, 4);
                    p.Value = obj.Operateid;
                    ht.Add(p);
                }
            }
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
            if (obj.Name != null && obj.Name.Trim().Length > 0)
            {
                term += " and name=@name ";
                if (ESP.Media.Access.Utilities.Common.Find(ht, "@name") == -1)
                {
                    SqlParameter p = new SqlParameter("@name", SqlDbType.NVarChar, 100);
                    p.Value = obj.Name;
                    ht.Add(p);
                }
            }
            if (obj.Altname != null && obj.Altname.Trim().Length > 0)
            {
                term += " and altname=@altname ";
                if (ESP.Media.Access.Utilities.Common.Find(ht, "@altname") == -1)
                {
                    SqlParameter p = new SqlParameter("@altname", SqlDbType.NVarChar, 100);
                    p.Value = obj.Altname;
                    ht.Add(p);
                }
            }
            if (obj.Integral > 0)//Integral
            {
                term += " and integral=@integral ";
                if (ESP.Media.Access.Utilities.Common.Find(ht, "@integral") == -1)
                {
                    SqlParameter p = new SqlParameter("@integral", SqlDbType.Int, 4);
                    p.Value = obj.Integral;
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
        private static string getQueryTerms(IntegralruleInfo obj, ref Hashtable ht)
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
            if (obj.Operateid > 0)//OperateID
            {
                term += " and a.operateid=@operateid ";
                if (!ht.ContainsKey("@operateid"))
                {
                    ht.Add("@operateid", obj.Operateid);
                }
            }
            if (obj.Tableid > 0)//TableID
            {
                term += " and a.tableid=@tableid ";
                if (!ht.ContainsKey("@tableid"))
                {
                    ht.Add("@tableid", obj.Tableid);
                }
            }
            if (obj.Name != null && obj.Name.Trim().Length > 0)
            {
                term += " and a.name=@name ";
                if (!ht.ContainsKey("@name"))
                {
                    ht.Add("@name", obj.Name);
                }
            }
            if (obj.Altname != null && obj.Altname.Trim().Length > 0)
            {
                term += " and a.altname=@altname ";
                if (!ht.ContainsKey("@altname"))
                {
                    ht.Add("@altname", obj.Altname);
                }
            }
            if (obj.Integral > 0)//Integral
            {
                term += " and a.integral=@integral ";
                if (!ht.ContainsKey("@integral"))
                {
                    ht.Add("@integral", obj.Integral);
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
            string sql = @"select {0} a.id as id,a.operateid as operateid,a.tableid as tableid,a.name as name,a.altname as altname,a.integral as integral,a.del as del {1} from media_integralrule as a {2} where 1=1 {3} ";
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


        public static DataTable QueryInfoByObj(IntegralruleInfo obj, string terms, Hashtable ht)
        {
            if (ht == null)
            {
                ht = new Hashtable();
            }
            SqlParameter[] para = ESP.Media.Access.Utilities.Common.DictToSqlParam(ht);
            return QueryInfoByObj(obj, terms, para);
        }


        public static DataTable QueryInfoByObj(IntegralruleInfo obj, string terms, params SqlParameter[] param)
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
        public static IntegralruleInfo Load(int id)
        {
            DataTable dt = null;
            dt = QueryInfo(" and a.id=@id ", new SqlParameter("@id", id));
            if (dt != null && dt.Rows.Count > 0)
            {
                return setObject(dt.Rows[0]);
            }
            return null;
        }


        public static IntegralruleInfo Load(int id, SqlTransaction trans)
        {
            DataTable dt = null;
            dt = QueryInfo(trans, " and a.id=@id ", new SqlParameter("@id", id));
            if (dt != null && dt.Rows.Count > 0)
            {
                return setObject(dt.Rows[0]);
            }
            return null;
        }


        public static IntegralruleInfo setObject(DataRow dr)
        {
            IntegralruleInfo obj = new IntegralruleInfo();
            if (dr.Table.Columns.Contains("id") && dr["id"] != DBNull.Value)//id
            {
                obj.Id = Convert.ToInt32(dr["id"]);
            }
            if (dr.Table.Columns.Contains("operateid") && dr["operateid"] != DBNull.Value)//操作id
            {
                obj.Operateid = Convert.ToInt32(dr["operateid"]);
            }
            if (dr.Table.Columns.Contains("tableid") && dr["tableid"] != DBNull.Value)//表id
            {
                obj.Tableid = Convert.ToInt32(dr["tableid"]);
            }
            if (dr.Table.Columns.Contains("name") && dr["name"] != DBNull.Value)//英文名称
            {
                obj.Name = (dr["name"]).ToString();
            }
            if (dr.Table.Columns.Contains("altname") && dr["altname"] != DBNull.Value)//中文名称
            {
                obj.Altname = (dr["altname"]).ToString();
            }
            if (dr.Table.Columns.Contains("integral") && dr["integral"] != DBNull.Value)//积分
            {
                obj.Integral = Convert.ToInt32(dr["integral"]);
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
