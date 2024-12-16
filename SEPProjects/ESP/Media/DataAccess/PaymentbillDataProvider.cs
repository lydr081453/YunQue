using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.Collections.Generic;
using ESP.Media.Entity;
using ESP.Media.Access.Utilities;
namespace ESP.Media.DataAccess
{
    public class PaymentbillDataProvider
    {
        #region 构造函数
        public PaymentbillDataProvider()
        {
        }
        #endregion
        #region 插入
        //插入字符串
        private static string strinsert(PaymentbillInfo obj, ref List<SqlParameter> ht)
        {
            if (ht == null)
            {
                ht = new List<SqlParameter>();
            }
            string sql = @"insert into media_paymentbill (projectid,paymentdate,paymentuserid,paymentusername,paymentremark,del,financecode,status) values (@projectid,@paymentdate,@paymentuserid,@paymentusername,@paymentremark,@del,@financecode,@status);select @@IDENTITY as rowNum;";
            SqlParameter param_Projectid = new SqlParameter("@Projectid", SqlDbType.Int, 4);
            param_Projectid.Value = obj.Projectid;
            ht.Add(param_Projectid);
            SqlParameter param_Paymentdate = new SqlParameter("@Paymentdate", SqlDbType.DateTime, 8);
            param_Paymentdate.Value = ESP.Media.Access.Utilities.Common.StringToDateTime(obj.Paymentdate);
            ht.Add(param_Paymentdate);
            SqlParameter param_Paymentuserid = new SqlParameter("@Paymentuserid", SqlDbType.Int, 4);
            param_Paymentuserid.Value = obj.Paymentuserid;
            ht.Add(param_Paymentuserid);
            SqlParameter param_Paymentusername = new SqlParameter("@Paymentusername", SqlDbType.NVarChar, 100);
            param_Paymentusername.Value = obj.Paymentusername;
            ht.Add(param_Paymentusername);
            SqlParameter param_Paymentremark = new SqlParameter("@Paymentremark", SqlDbType.NVarChar, 100);
            param_Paymentremark.Value = obj.Paymentremark;
            ht.Add(param_Paymentremark);
            SqlParameter param_Del = new SqlParameter("@Del", SqlDbType.Int, 4);
            param_Del.Value = obj.Del;
            ht.Add(param_Del);
            SqlParameter param_Financecode = new SqlParameter("@Financecode", SqlDbType.NVarChar, 100);
            param_Financecode.Value = obj.Financecode;
            ht.Add(param_Financecode);
            SqlParameter param_Status = new SqlParameter("@Status", SqlDbType.Int, 4);
            param_Status.Value = obj.Status;
            ht.Add(param_Status);
            return sql;
        }


        //插入一条记录
        public static int insertinfo(PaymentbillInfo obj, SqlTransaction trans)
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
        public static int insertinfo(PaymentbillInfo obj)
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
            string sql = "delete media_paymentbill where paymentbillid=@id";
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
        public static string getUpdateString(PaymentbillInfo objTerm, PaymentbillInfo Objupdate, string term, ref List<SqlParameter> ht, params SqlParameter[] param)
        {
            string sql = "update media_paymentbill set projectid=@projectid,paymentdate=@paymentdate,paymentuserid=@paymentuserid,paymentusername=@paymentusername,paymentremark=@paymentremark,del=@del,financecode=@financecode,status=@status where 1=1 ";
            SqlParameter param_projectid = new SqlParameter("@projectid", SqlDbType.Int, 4);
            param_projectid.Value = Objupdate.Projectid;
            ht.Add(param_projectid);
            SqlParameter param_paymentdate = new SqlParameter("@paymentdate", SqlDbType.DateTime, 8);
            param_paymentdate.Value = ESP.Media.Access.Utilities.Common.StringToDateTime(Objupdate.Paymentdate);
            ht.Add(param_paymentdate);
            SqlParameter param_paymentuserid = new SqlParameter("@paymentuserid", SqlDbType.Int, 4);
            param_paymentuserid.Value = Objupdate.Paymentuserid;
            ht.Add(param_paymentuserid);
            SqlParameter param_paymentusername = new SqlParameter("@paymentusername", SqlDbType.NVarChar, 100);
            param_paymentusername.Value = Objupdate.Paymentusername;
            ht.Add(param_paymentusername);
            SqlParameter param_paymentremark = new SqlParameter("@paymentremark", SqlDbType.NVarChar, 100);
            param_paymentremark.Value = Objupdate.Paymentremark;
            ht.Add(param_paymentremark);
            SqlParameter param_del = new SqlParameter("@del", SqlDbType.Int, 4);
            param_del.Value = Objupdate.Del;
            ht.Add(param_del);
            SqlParameter param_financecode = new SqlParameter("@financecode", SqlDbType.NVarChar, 100);
            param_financecode.Value = Objupdate.Financecode;
            ht.Add(param_financecode);
            SqlParameter param_status = new SqlParameter("@status", SqlDbType.Int, 4);
            param_status.Value = Objupdate.Status;
            ht.Add(param_status);
            if (objTerm == null && (term == null || term.Trim().Length <= 0))
            {
                sql += " and paymentbillid=@id ";
                if (ESP.Media.Access.Utilities.Common.Find(ht, "@id") == -1)
                {
                    SqlParameter p = new SqlParameter("@id", SqlDbType.Int, 4);
                    p.Value = Objupdate.Paymentbillid;
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
        public static bool updateInfo(SqlTransaction trans, PaymentbillInfo objterm, PaymentbillInfo Objupdate, string term, params SqlParameter[] param)
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
        public static bool updateInfo(PaymentbillInfo objterm, PaymentbillInfo Objupdate, string term, params SqlParameter[] param)
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


        private static string getTerms(PaymentbillInfo obj, ref List<SqlParameter> ht)
        {
            string term = string.Empty;
            if (obj.Paymentbillid > 0)//paymentbillid
            {
                term += " and paymentbillid=@paymentbillid ";
                if (ESP.Media.Access.Utilities.Common.Find(ht, "@paymentbillid") == -1)
                {
                    SqlParameter p = new SqlParameter("@paymentbillid", SqlDbType.Int, 4);
                    p.Value = obj.Paymentbillid;
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
            if (obj.Paymentdate != null && obj.Paymentdate.Trim().Length > 0)
            {
                term += " and paymentdate=@paymentdate ";
                if (ESP.Media.Access.Utilities.Common.Find(ht, "@paymentdate") == -1)
                {
                    SqlParameter p = new SqlParameter("@paymentdate", SqlDbType.DateTime, 8);
                    p.Value = ESP.Media.Access.Utilities.Common.StringToDateTime(obj.Paymentdate);
                    ht.Add(p);
                }
            }
            if (obj.Paymentuserid > 0)//PaymentUserid
            {
                term += " and paymentuserid=@paymentuserid ";
                if (ESP.Media.Access.Utilities.Common.Find(ht, "@paymentuserid") == -1)
                {
                    SqlParameter p = new SqlParameter("@paymentuserid", SqlDbType.Int, 4);
                    p.Value = obj.Paymentuserid;
                    ht.Add(p);
                }
            }
            if (obj.Paymentusername != null && obj.Paymentusername.Trim().Length > 0)
            {
                term += " and paymentusername=@paymentusername ";
                if (ESP.Media.Access.Utilities.Common.Find(ht, "@paymentusername") == -1)
                {
                    SqlParameter p = new SqlParameter("@paymentusername", SqlDbType.NVarChar, 100);
                    p.Value = obj.Paymentusername;
                    ht.Add(p);
                }
            }
            if (obj.Paymentremark != null && obj.Paymentremark.Trim().Length > 0)
            {
                term += " and paymentremark=@paymentremark ";
                if (ESP.Media.Access.Utilities.Common.Find(ht, "@paymentremark") == -1)
                {
                    SqlParameter p = new SqlParameter("@paymentremark", SqlDbType.NVarChar, 100);
                    p.Value = obj.Paymentremark;
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
            if (obj.Financecode != null && obj.Financecode.Trim().Length > 0)
            {
                term += " and financecode=@financecode ";
                if (ESP.Media.Access.Utilities.Common.Find(ht, "@financecode") == -1)
                {
                    SqlParameter p = new SqlParameter("@financecode", SqlDbType.NVarChar, 100);
                    p.Value = obj.Financecode;
                    ht.Add(p);
                }
            }
            if (obj.Status > 0)//Status
            {
                term += " and status=@status ";
                if (ESP.Media.Access.Utilities.Common.Find(ht, "@status") == -1)
                {
                    SqlParameter p = new SqlParameter("@status", SqlDbType.Int, 4);
                    p.Value = obj.Status;
                    ht.Add(p);
                }
            }
            return term;
        }
        #endregion
        #region 查询
        private static string getQueryTerms(PaymentbillInfo obj, ref Hashtable ht)
        {
            string term = string.Empty;
            if (obj.Paymentbillid > 0)//paymentbillid
            {
                term += " and a.paymentbillid=@paymentbillid ";
                if (!ht.ContainsKey("@paymentbillid"))
                {
                    ht.Add("@paymentbillid", obj.Paymentbillid);
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
            if (obj.Paymentdate != null && obj.Paymentdate.Trim().Length > 0)
            {
                term += " and a.paymentdate=@paymentdate ";
                if (!ht.ContainsKey("@paymentdate"))
                {
                    ht.Add("@paymentdate", obj.Paymentdate);
                }
            }
            if (obj.Paymentuserid > 0)//PaymentUserid
            {
                term += " and a.paymentuserid=@paymentuserid ";
                if (!ht.ContainsKey("@paymentuserid"))
                {
                    ht.Add("@paymentuserid", obj.Paymentuserid);
                }
            }
            if (obj.Paymentusername != null && obj.Paymentusername.Trim().Length > 0)
            {
                term += " and a.paymentusername=@paymentusername ";
                if (!ht.ContainsKey("@paymentusername"))
                {
                    ht.Add("@paymentusername", obj.Paymentusername);
                }
            }
            if (obj.Paymentremark != null && obj.Paymentremark.Trim().Length > 0)
            {
                term += " and a.paymentremark=@paymentremark ";
                if (!ht.ContainsKey("@paymentremark"))
                {
                    ht.Add("@paymentremark", obj.Paymentremark);
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
            if (obj.Financecode != null && obj.Financecode.Trim().Length > 0)
            {
                term += " and a.financecode=@financecode ";
                if (!ht.ContainsKey("@financecode"))
                {
                    ht.Add("@financecode", obj.Financecode);
                }
            }
            if (obj.Status > 0)//Status
            {
                term += " and a.status=@status ";
                if (!ht.ContainsKey("@status"))
                {
                    ht.Add("@status", obj.Status);
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
            string sql = @"select {0} a.paymentbillid as paymentbillid,a.projectid as projectid,a.paymentdate as paymentdate,a.paymentuserid as paymentuserid,a.paymentusername as paymentusername,a.paymentremark as paymentremark,a.del as del,a.financecode as financecode,a.status as status {1} from media_paymentbill as a {2} where 1=1 {3} ";
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


        public static DataTable QueryInfoByObj(PaymentbillInfo obj, string terms, Hashtable ht)
        {
            if (ht == null)
            {
                ht = new Hashtable();
            }
            SqlParameter[] para = ESP.Media.Access.Utilities.Common.DictToSqlParam(ht);
            return QueryInfoByObj(obj, terms, para);
        }


        public static DataTable QueryInfoByObj(PaymentbillInfo obj, string terms, params SqlParameter[] param)
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
        public static PaymentbillInfo Load(int id)
        {
            DataTable dt = null;
            dt = QueryInfo(" and a.paymentbillid=@id ", new SqlParameter("@id", id));
            if (dt != null && dt.Rows.Count > 0)
            {
                return setObject(dt.Rows[0]);
            }
            return null;
        }


        public static PaymentbillInfo Load(int id, SqlTransaction trans)
        {
            DataTable dt = null;
            dt = QueryInfo(trans, " and a.paymentbillid=@id ", new SqlParameter("@id", id));
            if (dt != null && dt.Rows.Count > 0)
            {
                return setObject(dt.Rows[0]);
            }
            return null;
        }


        public static PaymentbillInfo setObject(DataRow dr)
        {
            PaymentbillInfo obj = new PaymentbillInfo();
            if (dr.Table.Columns.Contains("paymentbillid") && dr["paymentbillid"] != DBNull.Value)//paymentbillid
            {
                obj.Paymentbillid = Convert.ToInt32(dr["paymentbillid"]);
            }
            if (dr.Table.Columns.Contains("projectid") && dr["projectid"] != DBNull.Value)//projectid
            {
                obj.Projectid = Convert.ToInt32(dr["projectid"]);
            }
            if (dr.Table.Columns.Contains("paymentdate") && dr["paymentdate"] != DBNull.Value)//支付日期
            {
                obj.Paymentdate = (dr["paymentdate"]).ToString();
            }
            if (dr.Table.Columns.Contains("paymentuserid") && dr["paymentuserid"] != DBNull.Value)//支付人
            {
                obj.Paymentuserid = Convert.ToInt32(dr["paymentuserid"]);
            }
            if (dr.Table.Columns.Contains("paymentusername") && dr["paymentusername"] != DBNull.Value)//paymentusername
            {
                obj.Paymentusername = (dr["paymentusername"]).ToString();
            }
            if (dr.Table.Columns.Contains("paymentremark") && dr["paymentremark"] != DBNull.Value)//描述
            {
                obj.Paymentremark = (dr["paymentremark"]).ToString();
            }
            if (dr.Table.Columns.Contains("del") && dr["del"] != DBNull.Value)//del
            {
                obj.Del = Convert.ToInt32(dr["del"]);
            }
            if (dr.Table.Columns.Contains("financecode") && dr["financecode"] != DBNull.Value)//financecode
            {
                obj.Financecode = (dr["financecode"]).ToString();
            }
            if (dr.Table.Columns.Contains("status") && dr["status"] != DBNull.Value)//status
            {
                obj.Status = Convert.ToInt32(dr["status"]);
            }
            return obj;
        }
        #endregion
    }
}
