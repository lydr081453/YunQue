using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.Collections.Generic;
using ESP.Media.Access.Utilities;
using ESP.Media.Entity;
namespace ESP.Media.DataAccess
{
    public class PaymentDataProvider
    {
        #region 构造函数
        public PaymentDataProvider()
        {
        }
        #endregion
        #region 插入
        //插入字符串
        private static string strinsert(PaymentInfo obj, ref List<SqlParameter> ht)
        {
            if (ht == null)
            {
                ht = new List<SqlParameter>();
            }
            string sql = @"insert into media_payment (reporterid,propagatetype,progagateid,paymentdate,paymentuserid,paymentremark,paytype,payamount,financecode,uploadstartdate,uploadenddate,del,projectid,paymentbillid) values (@reporterid,@propagatetype,@progagateid,@paymentdate,@paymentuserid,@paymentremark,@paytype,@payamount,@financecode,@uploadstartdate,@uploadenddate,@del,@projectid,@paymentbillid);select @@IDENTITY as rowNum;";
            SqlParameter param_Reporterid = new SqlParameter("@Reporterid", SqlDbType.Int, 4);
            param_Reporterid.Value = obj.Reporterid;
            ht.Add(param_Reporterid);
            SqlParameter param_Propagatetype = new SqlParameter("@Propagatetype", SqlDbType.Int, 4);
            param_Propagatetype.Value = obj.Propagatetype;
            ht.Add(param_Propagatetype);
            SqlParameter param_Progagateid = new SqlParameter("@Progagateid", SqlDbType.Int, 4);
            param_Progagateid.Value = obj.Progagateid;
            ht.Add(param_Progagateid);
            SqlParameter param_Paymentdate = new SqlParameter("@Paymentdate", SqlDbType.DateTime, 8);
            param_Paymentdate.Value = ESP.Media.Access.Utilities.Common.StringToDateTime(obj.Paymentdate);
            ht.Add(param_Paymentdate);
            SqlParameter param_Paymentuserid = new SqlParameter("@Paymentuserid", SqlDbType.Int, 4);
            param_Paymentuserid.Value = obj.Paymentuserid;
            ht.Add(param_Paymentuserid);
            SqlParameter param_Paymentremark = new SqlParameter("@Paymentremark", SqlDbType.NVarChar, 100);
            param_Paymentremark.Value = obj.Paymentremark;
            ht.Add(param_Paymentremark);
            SqlParameter param_Paytype = new SqlParameter("@Paytype", SqlDbType.Int, 4);
            param_Paytype.Value = obj.Paytype;
            ht.Add(param_Paytype);
            SqlParameter param_Payamount = new SqlParameter("@Payamount", SqlDbType.Float, 8);
            param_Payamount.Value = obj.Payamount;
            ht.Add(param_Payamount);
            SqlParameter param_Financecode = new SqlParameter("@Financecode", SqlDbType.NVarChar, 100);
            param_Financecode.Value = obj.Financecode;
            ht.Add(param_Financecode);
            SqlParameter param_Uploadstartdate = new SqlParameter("@Uploadstartdate", SqlDbType.DateTime, 8);
            param_Uploadstartdate.Value = ESP.Media.Access.Utilities.Common.StringToDateTime(obj.Uploadstartdate);
            ht.Add(param_Uploadstartdate);
            SqlParameter param_Uploadenddate = new SqlParameter("@Uploadenddate", SqlDbType.DateTime, 8);
            param_Uploadenddate.Value = ESP.Media.Access.Utilities.Common.StringToDateTime(obj.Uploadenddate);
            ht.Add(param_Uploadenddate);
            SqlParameter param_Del = new SqlParameter("@Del", SqlDbType.Int, 4);
            param_Del.Value = obj.Del;
            ht.Add(param_Del);
            SqlParameter param_Projectid = new SqlParameter("@Projectid", SqlDbType.Int, 4);
            param_Projectid.Value = obj.Projectid;
            ht.Add(param_Projectid);
            SqlParameter param_Paymentbillid = new SqlParameter("@Paymentbillid", SqlDbType.Int, 4);
            param_Paymentbillid.Value = obj.Paymentbillid;
            ht.Add(param_Paymentbillid);
            return sql;
        }


        //插入一条记录
        public static int insertinfo(PaymentInfo obj, SqlTransaction trans)
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
        public static int insertinfo(PaymentInfo obj)
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
            string sql = "delete media_payment where id=@id";
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
        public static string getUpdateString(PaymentInfo objTerm, PaymentInfo Objupdate, string term, ref List<SqlParameter> ht, params SqlParameter[] param)
        {
            string sql = "update media_payment set reporterid=@reporterid,propagatetype=@propagatetype,progagateid=@progagateid,paymentdate=@paymentdate,paymentuserid=@paymentuserid,paymentremark=@paymentremark,paytype=@paytype,payamount=@payamount,financecode=@financecode,uploadstartdate=@uploadstartdate,uploadenddate=@uploadenddate,del=@del,projectid=@projectid,paymentbillid=@paymentbillid where 1=1 ";
            SqlParameter param_reporterid = new SqlParameter("@reporterid", SqlDbType.Int, 4);
            param_reporterid.Value = Objupdate.Reporterid;
            ht.Add(param_reporterid);
            SqlParameter param_propagatetype = new SqlParameter("@propagatetype", SqlDbType.Int, 4);
            param_propagatetype.Value = Objupdate.Propagatetype;
            ht.Add(param_propagatetype);
            SqlParameter param_progagateid = new SqlParameter("@progagateid", SqlDbType.Int, 4);
            param_progagateid.Value = Objupdate.Progagateid;
            ht.Add(param_progagateid);
            SqlParameter param_paymentdate = new SqlParameter("@paymentdate", SqlDbType.DateTime, 8);
            param_paymentdate.Value = ESP.Media.Access.Utilities.Common.StringToDateTime(Objupdate.Paymentdate);
            ht.Add(param_paymentdate);
            SqlParameter param_paymentuserid = new SqlParameter("@paymentuserid", SqlDbType.Int, 4);
            param_paymentuserid.Value = Objupdate.Paymentuserid;
            ht.Add(param_paymentuserid);
            SqlParameter param_paymentremark = new SqlParameter("@paymentremark", SqlDbType.NVarChar, 100);
            param_paymentremark.Value = Objupdate.Paymentremark;
            ht.Add(param_paymentremark);
            SqlParameter param_paytype = new SqlParameter("@paytype", SqlDbType.Int, 4);
            param_paytype.Value = Objupdate.Paytype;
            ht.Add(param_paytype);
            SqlParameter param_payamount = new SqlParameter("@payamount", SqlDbType.Float, 8);
            param_payamount.Value = Objupdate.Payamount;
            ht.Add(param_payamount);
            SqlParameter param_financecode = new SqlParameter("@financecode", SqlDbType.NVarChar, 100);
            param_financecode.Value = Objupdate.Financecode;
            ht.Add(param_financecode);
            SqlParameter param_uploadstartdate = new SqlParameter("@uploadstartdate", SqlDbType.DateTime, 8);
            param_uploadstartdate.Value = ESP.Media.Access.Utilities.Common.StringToDateTime(Objupdate.Uploadstartdate);
            ht.Add(param_uploadstartdate);
            SqlParameter param_uploadenddate = new SqlParameter("@uploadenddate", SqlDbType.DateTime, 8);
            param_uploadenddate.Value = ESP.Media.Access.Utilities.Common.StringToDateTime(Objupdate.Uploadenddate);
            ht.Add(param_uploadenddate);
            SqlParameter param_del = new SqlParameter("@del", SqlDbType.Int, 4);
            param_del.Value = Objupdate.Del;
            ht.Add(param_del);
            SqlParameter param_projectid = new SqlParameter("@projectid", SqlDbType.Int, 4);
            param_projectid.Value = Objupdate.Projectid;
            ht.Add(param_projectid);
            SqlParameter param_paymentbillid = new SqlParameter("@paymentbillid", SqlDbType.Int, 4);
            param_paymentbillid.Value = Objupdate.Paymentbillid;
            ht.Add(param_paymentbillid);
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
        public static bool updateInfo(SqlTransaction trans, PaymentInfo objterm, PaymentInfo Objupdate, string term, params SqlParameter[] param)
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
        public static bool updateInfo(PaymentInfo objterm, PaymentInfo Objupdate, string term, params SqlParameter[] param)
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


        private static string getTerms(PaymentInfo obj, ref List<SqlParameter> ht)
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
            if (obj.Reporterid > 0)//reporterid
            {
                term += " and reporterid=@reporterid ";
                if (ESP.Media.Access.Utilities.Common.Find(ht, "@reporterid") == -1)
                {
                    SqlParameter p = new SqlParameter("@reporterid", SqlDbType.Int, 4);
                    p.Value = obj.Reporterid;
                    ht.Add(p);
                }
            }
            if (obj.Propagatetype > 0)//Propagatetype
            {
                term += " and propagatetype=@propagatetype ";
                if (ESP.Media.Access.Utilities.Common.Find(ht, "@propagatetype") == -1)
                {
                    SqlParameter p = new SqlParameter("@propagatetype", SqlDbType.Int, 4);
                    p.Value = obj.Propagatetype;
                    ht.Add(p);
                }
            }
            if (obj.Progagateid > 0)//ProgagateID
            {
                term += " and progagateid=@progagateid ";
                if (ESP.Media.Access.Utilities.Common.Find(ht, "@progagateid") == -1)
                {
                    SqlParameter p = new SqlParameter("@progagateid", SqlDbType.Int, 4);
                    p.Value = obj.Progagateid;
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
            if (obj.Paytype > 0)//支付类型
            {
                term += " and paytype=@paytype ";
                if (ESP.Media.Access.Utilities.Common.Find(ht, "@paytype") == -1)
                {
                    SqlParameter p = new SqlParameter("@paytype", SqlDbType.Int, 4);
                    p.Value = obj.Paytype;
                    ht.Add(p);
                }
            }
            if (obj.Payamount > 0)//payamount
            {
                term += " and payamount=@payamount ";
                if (ESP.Media.Access.Utilities.Common.Find(ht, "@payamount") == -1)
                {
                    SqlParameter p = new SqlParameter("@payamount", SqlDbType.Float, 8);
                    p.Value = obj.Payamount;
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
            if (obj.Uploadstartdate != null && obj.Uploadstartdate.Trim().Length > 0)
            {
                term += " and uploadstartdate=@uploadstartdate ";
                if (ESP.Media.Access.Utilities.Common.Find(ht, "@uploadstartdate") == -1)
                {
                    SqlParameter p = new SqlParameter("@uploadstartdate", SqlDbType.DateTime, 8);
                    p.Value = ESP.Media.Access.Utilities.Common.StringToDateTime(obj.Uploadstartdate);
                    ht.Add(p);
                }
            }
            if (obj.Uploadenddate != null && obj.Uploadenddate.Trim().Length > 0)
            {
                term += " and uploadenddate=@uploadenddate ";
                if (ESP.Media.Access.Utilities.Common.Find(ht, "@uploadenddate") == -1)
                {
                    SqlParameter p = new SqlParameter("@uploadenddate", SqlDbType.DateTime, 8);
                    p.Value = ESP.Media.Access.Utilities.Common.StringToDateTime(obj.Uploadenddate);
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
            return term;
        }
        #endregion
        #region 查询
        private static string getQueryTerms(PaymentInfo obj, ref Hashtable ht)
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
            if (obj.Reporterid > 0)//reporterid
            {
                term += " and a.reporterid=@reporterid ";
                if (!ht.ContainsKey("@reporterid"))
                {
                    ht.Add("@reporterid", obj.Reporterid);
                }
            }
            if (obj.Propagatetype > 0)//Propagatetype
            {
                term += " and a.propagatetype=@propagatetype ";
                if (!ht.ContainsKey("@propagatetype"))
                {
                    ht.Add("@propagatetype", obj.Propagatetype);
                }
            }
            if (obj.Progagateid > 0)//ProgagateID
            {
                term += " and a.progagateid=@progagateid ";
                if (!ht.ContainsKey("@progagateid"))
                {
                    ht.Add("@progagateid", obj.Progagateid);
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
            if (obj.Paymentremark != null && obj.Paymentremark.Trim().Length > 0)
            {
                term += " and a.paymentremark=@paymentremark ";
                if (!ht.ContainsKey("@paymentremark"))
                {
                    ht.Add("@paymentremark", obj.Paymentremark);
                }
            }
            if (obj.Paytype > 0)//支付类型
            {
                term += " and a.paytype=@paytype ";
                if (!ht.ContainsKey("@paytype"))
                {
                    ht.Add("@paytype", obj.Paytype);
                }
            }
            if (obj.Payamount > 0)//payamount
            {
                term += " and a.payamount=@payamount ";
                if (!ht.ContainsKey("@payamount"))
                {
                    ht.Add("@payamount", obj.Payamount);
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
            if (obj.Uploadstartdate != null && obj.Uploadstartdate.Trim().Length > 0)
            {
                term += " and a.uploadstartdate=@uploadstartdate ";
                if (!ht.ContainsKey("@uploadstartdate"))
                {
                    ht.Add("@uploadstartdate", obj.Uploadstartdate);
                }
            }
            if (obj.Uploadenddate != null && obj.Uploadenddate.Trim().Length > 0)
            {
                term += " and a.uploadenddate=@uploadenddate ";
                if (!ht.ContainsKey("@uploadenddate"))
                {
                    ht.Add("@uploadenddate", obj.Uploadenddate);
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
            if (obj.Projectid > 0)//projectid
            {
                term += " and a.projectid=@projectid ";
                if (!ht.ContainsKey("@projectid"))
                {
                    ht.Add("@projectid", obj.Projectid);
                }
            }
            if (obj.Paymentbillid > 0)//paymentbillid
            {
                term += " and a.paymentbillid=@paymentbillid ";
                if (!ht.ContainsKey("@paymentbillid"))
                {
                    ht.Add("@paymentbillid", obj.Paymentbillid);
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
            string sql = @"select {0} a.id as id,a.reporterid as reporterid,a.propagatetype as propagatetype,a.progagateid as progagateid,a.paymentdate as paymentdate,a.paymentuserid as paymentuserid,a.paymentremark as paymentremark,a.paytype as paytype,a.payamount as payamount,a.financecode as financecode,a.uploadstartdate as uploadstartdate,a.uploadenddate as uploadenddate,a.del as del,a.projectid as projectid,a.paymentbillid as paymentbillid {1} from media_payment as a {2} where 1=1 {3} ";
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


        public static DataTable QueryInfoByObj(PaymentInfo obj, string terms, Hashtable ht)
        {
            if (ht == null)
            {
                ht = new Hashtable();
            }
            SqlParameter[] para = ESP.Media.Access.Utilities.Common.DictToSqlParam(ht);
            return QueryInfoByObj(obj, terms, para);
        }


        public static DataTable QueryInfoByObj(PaymentInfo obj, string terms, params SqlParameter[] param)
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
        public static PaymentInfo Load(int id)
        {
            DataTable dt = null;
            dt = QueryInfo(" and a.id=@id ", new SqlParameter("@id", id));
            if (dt != null && dt.Rows.Count > 0)
            {
                return setObject(dt.Rows[0]);
            }
            return null;
        }


        public static PaymentInfo Load(int id, SqlTransaction trans)
        {
            DataTable dt = null;
            dt = QueryInfo(trans, " and a.id=@id ", new SqlParameter("@id", id));
            if (dt != null && dt.Rows.Count > 0)
            {
                return setObject(dt.Rows[0]);
            }
            return null;
        }


        public static PaymentInfo setObject(DataRow dr)
        {
            PaymentInfo obj = new PaymentInfo();
            if (dr.Table.Columns.Contains("id") && dr["id"] != DBNull.Value)//id
            {
                obj.Id = Convert.ToInt32(dr["id"]);
            }
            if (dr.Table.Columns.Contains("reporterid") && dr["reporterid"] != DBNull.Value)//reporterid
            {
                obj.Reporterid = Convert.ToInt32(dr["reporterid"]);
            }
            if (dr.Table.Columns.Contains("propagatetype") && dr["propagatetype"] != DBNull.Value)//传播类型
            {
                obj.Propagatetype = Convert.ToInt32(dr["propagatetype"]);
            }
            if (dr.Table.Columns.Contains("progagateid") && dr["progagateid"] != DBNull.Value)//progagateid
            {
                obj.Progagateid = Convert.ToInt32(dr["progagateid"]);
            }
            if (dr.Table.Columns.Contains("paymentdate") && dr["paymentdate"] != DBNull.Value)//支付日期
            {
                obj.Paymentdate = (dr["paymentdate"]).ToString();
            }
            if (dr.Table.Columns.Contains("paymentuserid") && dr["paymentuserid"] != DBNull.Value)//支付人
            {
                obj.Paymentuserid = Convert.ToInt32(dr["paymentuserid"]);
            }
            if (dr.Table.Columns.Contains("paymentremark") && dr["paymentremark"] != DBNull.Value)//描述
            {
                obj.Paymentremark = (dr["paymentremark"]).ToString();
            }
            if (dr.Table.Columns.Contains("paytype") && dr["paytype"] != DBNull.Value)//支付类型
            {
                obj.Paytype = Convert.ToInt32(dr["paytype"]);
            }
            if (dr.Table.Columns.Contains("payamount") && dr["payamount"] != DBNull.Value)//payamount
            {
                obj.Payamount = Convert.ToDouble(dr["payamount"]);
            }
            if (dr.Table.Columns.Contains("financecode") && dr["financecode"] != DBNull.Value)//financecode
            {
                obj.Financecode = (dr["financecode"]).ToString();
            }
            if (dr.Table.Columns.Contains("uploadstartdate") && dr["uploadstartdate"] != DBNull.Value)//上传开始时间
            {
                obj.Uploadstartdate = (dr["uploadstartdate"]).ToString();
            }
            if (dr.Table.Columns.Contains("uploadenddate") && dr["uploadenddate"] != DBNull.Value)//上传截止时间
            {
                obj.Uploadenddate = (dr["uploadenddate"]).ToString();
            }
            if (dr.Table.Columns.Contains("del") && dr["del"] != DBNull.Value)//del
            {
                obj.Del = Convert.ToInt32(dr["del"]);
            }
            if (dr.Table.Columns.Contains("projectid") && dr["projectid"] != DBNull.Value)//projectid
            {
                obj.Projectid = Convert.ToInt32(dr["projectid"]);
            }
            if (dr.Table.Columns.Contains("paymentbillid") && dr["paymentbillid"] != DBNull.Value)//paymentbillid
            {
                obj.Paymentbillid = Convert.ToInt32(dr["paymentbillid"]);
            }
            return obj;
        }
        #endregion
    }
}
