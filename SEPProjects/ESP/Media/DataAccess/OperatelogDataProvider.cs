using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.Collections.Generic;
using ESP.Media.Entity;
using ESP.Media.Access.Utilities;
namespace ESP.Media.DataAccess
{
    public class OperatelogDataProvider
    {
        #region 构造函数
        public OperatelogDataProvider()
        {
        }
        #endregion
        #region 插入
        //插入字符串
        private static string strinsert(OperatelogInfo obj, ref List<SqlParameter> ht)
        {
            if (ht == null)
            {
                ht = new List<SqlParameter>();
            }
            string sql = @"insert into media_operatelog (userid,operatetypeid,operatetableid,operatetime,operatedes,integralid,integral,del) values (@userid,@operatetypeid,@operatetableid,@operatetime,@operatedes,@integralid,@integral,@del);select @@IDENTITY as rowNum;";
            SqlParameter param_Userid = new SqlParameter("@Userid", SqlDbType.Int, 4);
            param_Userid.Value = obj.Userid;
            ht.Add(param_Userid);
            SqlParameter param_Operatetypeid = new SqlParameter("@Operatetypeid", SqlDbType.Int, 4);
            param_Operatetypeid.Value = obj.Operatetypeid;
            ht.Add(param_Operatetypeid);
            SqlParameter param_Operatetableid = new SqlParameter("@Operatetableid", SqlDbType.Int, 4);
            param_Operatetableid.Value = obj.Operatetableid;
            ht.Add(param_Operatetableid);
            SqlParameter param_Operatetime = new SqlParameter("@Operatetime", SqlDbType.SmallDateTime, 4);
            param_Operatetime.Value = ESP.Media.Access.Utilities.Common.StringToDateTime(obj.Operatetime);
            ht.Add(param_Operatetime);
            SqlParameter param_Operatedes = new SqlParameter("@Operatedes", SqlDbType.NVarChar, 1000);
            param_Operatedes.Value = obj.Operatedes;
            ht.Add(param_Operatedes);
            SqlParameter param_Integralid = new SqlParameter("@Integralid", SqlDbType.Int, 4);
            param_Integralid.Value = obj.Integralid;
            ht.Add(param_Integralid);
            SqlParameter param_Integral = new SqlParameter("@Integral", SqlDbType.Int, 4);
            param_Integral.Value = obj.Integral;
            ht.Add(param_Integral);
            SqlParameter param_Del = new SqlParameter("@Del", SqlDbType.SmallInt, 2);
            param_Del.Value = obj.Del;
            ht.Add(param_Del);
            return sql;
        }


        //插入一条记录
        public static int insertinfo(OperatelogInfo obj, SqlTransaction trans)
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
        public static int insertinfo(OperatelogInfo obj)
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
            string sql = "delete media_operatelog where ID=@id";
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
        public static string getUpdateString(OperatelogInfo objTerm, OperatelogInfo Objupdate, string term, ref List<SqlParameter> ht, params SqlParameter[] param)
        {
            string sql = "update media_operatelog set userid=@userid,operatetypeid=@operatetypeid,operatetableid=@operatetableid,operatetime=@operatetime,operatedes=@operatedes,integralid=@integralid,integral=@integral,del=@del where 1=1 ";
            SqlParameter param_userid = new SqlParameter("@userid", SqlDbType.Int, 4);
            param_userid.Value = Objupdate.Userid;
            ht.Add(param_userid);
            SqlParameter param_operatetypeid = new SqlParameter("@operatetypeid", SqlDbType.Int, 4);
            param_operatetypeid.Value = Objupdate.Operatetypeid;
            ht.Add(param_operatetypeid);
            SqlParameter param_operatetableid = new SqlParameter("@operatetableid", SqlDbType.Int, 4);
            param_operatetableid.Value = Objupdate.Operatetableid;
            ht.Add(param_operatetableid);
            SqlParameter param_operatetime = new SqlParameter("@operatetime", SqlDbType.SmallDateTime, 4);
            param_operatetime.Value = ESP.Media.Access.Utilities.Common.StringToDateTime(Objupdate.Operatetime);
            ht.Add(param_operatetime);
            SqlParameter param_operatedes = new SqlParameter("@operatedes", SqlDbType.NVarChar, 1000);
            param_operatedes.Value = Objupdate.Operatedes;
            ht.Add(param_operatedes);
            SqlParameter param_integralid = new SqlParameter("@integralid", SqlDbType.Int, 4);
            param_integralid.Value = Objupdate.Integralid;
            ht.Add(param_integralid);
            SqlParameter param_integral = new SqlParameter("@integral", SqlDbType.Int, 4);
            param_integral.Value = Objupdate.Integral;
            ht.Add(param_integral);
            SqlParameter param_del = new SqlParameter("@del", SqlDbType.SmallInt, 2);
            param_del.Value = Objupdate.Del;
            ht.Add(param_del);
            if (objTerm == null && (term == null || term.Trim().Length <= 0))
            {
                sql += " and ID=@id ";
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
        public static bool updateInfo(SqlTransaction trans, OperatelogInfo objterm, OperatelogInfo Objupdate, string term, params SqlParameter[] param)
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
        public static bool updateInfo(OperatelogInfo objterm, OperatelogInfo Objupdate, string term, params SqlParameter[] param)
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


        private static string getTerms(OperatelogInfo obj, ref List<SqlParameter> ht)
        {
            string term = string.Empty;
            if (obj.Id > 0)//ID
            {
                term += " and id=@id ";
                if (ESP.Media.Access.Utilities.Common.Find(ht, "@id") == -1)
                {
                    SqlParameter p = new SqlParameter("@id", SqlDbType.Int, 4);
                    p.Value = obj.Id;
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
            if (obj.Operatetypeid > 0)//OperateTypeID
            {
                term += " and operatetypeid=@operatetypeid ";
                if (ESP.Media.Access.Utilities.Common.Find(ht, "@operatetypeid") == -1)
                {
                    SqlParameter p = new SqlParameter("@operatetypeid", SqlDbType.Int, 4);
                    p.Value = obj.Operatetypeid;
                    ht.Add(p);
                }
            }
            if (obj.Operatetableid > 0)//OperateTableID
            {
                term += " and operatetableid=@operatetableid ";
                if (ESP.Media.Access.Utilities.Common.Find(ht, "@operatetableid") == -1)
                {
                    SqlParameter p = new SqlParameter("@operatetableid", SqlDbType.Int, 4);
                    p.Value = obj.Operatetableid;
                    ht.Add(p);
                }
            }
            if (obj.Operatetime != null && obj.Operatetime.Trim().Length > 0)
            {
                term += " and operatetime=@operatetime ";
                if (ESP.Media.Access.Utilities.Common.Find(ht, "@operatetime") == -1)
                {
                    SqlParameter p = new SqlParameter("@operatetime", SqlDbType.SmallDateTime, 4);
                    p.Value = ESP.Media.Access.Utilities.Common.StringToDateTime(obj.Operatetime);
                    ht.Add(p);
                }
            }
            if (obj.Operatedes != null && obj.Operatedes.Trim().Length > 0)
            {
                term += " and operatedes=@operatedes ";
                if (ESP.Media.Access.Utilities.Common.Find(ht, "@operatedes") == -1)
                {
                    SqlParameter p = new SqlParameter("@operatedes", SqlDbType.NVarChar, 1000);
                    p.Value = obj.Operatedes;
                    ht.Add(p);
                }
            }
            if (obj.Integralid > 0)//IntegralID
            {
                term += " and integralid=@integralid ";
                if (ESP.Media.Access.Utilities.Common.Find(ht, "@integralid") == -1)
                {
                    SqlParameter p = new SqlParameter("@integralid", SqlDbType.Int, 4);
                    p.Value = obj.Integralid;
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
            if (obj.Del > 0)//Del
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
        private static string getQueryTerms(OperatelogInfo obj, ref Hashtable ht)
        {
            string term = string.Empty;
            if (obj.Id > 0)//ID
            {
                term += " and a.id=@id ";
                if (!ht.ContainsKey("@id"))
                {
                    ht.Add("@id", obj.Id);
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
            if (obj.Operatetypeid > 0)//OperateTypeID
            {
                term += " and a.operatetypeid=@operatetypeid ";
                if (!ht.ContainsKey("@operatetypeid"))
                {
                    ht.Add("@operatetypeid", obj.Operatetypeid);
                }
            }
            if (obj.Operatetableid > 0)//OperateTableID
            {
                term += " and a.operatetableid=@operatetableid ";
                if (!ht.ContainsKey("@operatetableid"))
                {
                    ht.Add("@operatetableid", obj.Operatetableid);
                }
            }
            if (obj.Operatetime != null && obj.Operatetime.Trim().Length > 0)
            {
                term += " and a.operatetime=@operatetime ";
                if (!ht.ContainsKey("@operatetime"))
                {
                    ht.Add("@operatetime", obj.Operatetime);
                }
            }
            if (obj.Operatedes != null && obj.Operatedes.Trim().Length > 0)
            {
                term += " and a.operatedes=@operatedes ";
                if (!ht.ContainsKey("@operatedes"))
                {
                    ht.Add("@operatedes", obj.Operatedes);
                }
            }
            if (obj.Integralid > 0)//IntegralID
            {
                term += " and a.integralid=@integralid ";
                if (!ht.ContainsKey("@integralid"))
                {
                    ht.Add("@integralid", obj.Integralid);
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
            if (obj.Del > 0)//Del
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
            string sql = @"select {0} a.id as id,a.userid as userid,a.operatetypeid as operatetypeid,a.operatetableid as operatetableid,a.operatetime as operatetime,a.operatedes as operatedes,a.integralid as integralid,a.integral as integral,a.del as del {1} from media_operatelog as a {2} where 1=1 {3} ";
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


        public static DataTable QueryInfoByObj(OperatelogInfo obj, string terms, Hashtable ht)
        {
            if (ht == null)
            {
                ht = new Hashtable();
            }
            SqlParameter[] para = ESP.Media.Access.Utilities.Common.DictToSqlParam(ht);
            return QueryInfoByObj(obj, terms, para);
        }


        public static DataTable QueryInfoByObj(OperatelogInfo obj, string terms, params SqlParameter[] param)
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
        public static OperatelogInfo Load(int id)
        {
            DataTable dt = null;
            dt = QueryInfo(" and a.id=@id ", new SqlParameter("@id", id));
            if (dt != null && dt.Rows.Count > 0)
            {
                return setObject(dt.Rows[0]);
            }
            return null;
        }


        public static OperatelogInfo Load(int id, SqlTransaction trans)
        {
            DataTable dt = null;
            dt = QueryInfo(trans, " and a.id=@id ", new SqlParameter("@id", id));
            if (dt != null && dt.Rows.Count > 0)
            {
                return setObject(dt.Rows[0]);
            }
            return null;
        }


        public static OperatelogInfo setObject(DataRow dr)
        {
            OperatelogInfo obj = new OperatelogInfo();
            if (dr.Table.Columns.Contains("id") && dr["id"] != DBNull.Value)//id
            {
                obj.Id = Convert.ToInt32(dr["id"]);
            }
            if (dr.Table.Columns.Contains("userid") && dr["userid"] != DBNull.Value)//用户id
            {
                obj.Userid = Convert.ToInt32(dr["userid"]);
            }
            if (dr.Table.Columns.Contains("operatetypeid") && dr["operatetypeid"] != DBNull.Value)//操作类型id
            {
                obj.Operatetypeid = Convert.ToInt32(dr["operatetypeid"]);
            }
            if (dr.Table.Columns.Contains("operatetableid") && dr["operatetableid"] != DBNull.Value)//操作类型表id
            {
                obj.Operatetableid = Convert.ToInt32(dr["operatetableid"]);
            }
            if (dr.Table.Columns.Contains("operatetime") && dr["operatetime"] != DBNull.Value)//操作时间
            {
                obj.Operatetime = (dr["operatetime"]).ToString();
            }
            if (dr.Table.Columns.Contains("operatedes") && dr["operatedes"] != DBNull.Value)//操作描述
            {
                obj.Operatedes = (dr["operatedes"]).ToString();
            }
            if (dr.Table.Columns.Contains("integralid") && dr["integralid"] != DBNull.Value)//积分id
            {
                obj.Integralid = Convert.ToInt32(dr["integralid"]);
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
