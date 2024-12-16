using System;
using System.Data;
using System.Data.SqlClient;
using ESP.Media.Entity;
using System.Collections;
using ESP.Media.Access.Utilities;
using System.Collections.Generic;
namespace ESP.Media.DataAccess
{
    public class EventreporterrelationDataProvider
    {
        #region 构造函数
        public EventreporterrelationDataProvider()
        {
        }
        #endregion
        #region 插入
        //插入字符串
        private static string strinsert(EventreporterrelationInfo obj, ref List<SqlParameter> ht)
        {
            if (ht == null)
            {
                ht = new List<SqlParameter>();
            }
            string sql = @"insert into media_eventreporterrelation (eventid,reporterid,relationdate,relationuserid,remark,del,paytype,bankname,bankcardcode,bankcardname,bankacountname,writingfee,referral,haveinvoice,paystatus,financecode,uploadstarttime,uploadendtime,paymentmode,privateremark,cooperatecircs) values (@eventid,@reporterid,@relationdate,@relationuserid,@remark,@del,@paytype,@bankname,@bankcardcode,@bankcardname,@bankacountname,@writingfee,@referral,@haveinvoice,@paystatus,@financecode,@uploadstarttime,@uploadendtime,@paymentmode,@privateremark,@cooperatecircs);select @@IDENTITY as rowNum;";
            SqlParameter param_Eventid = new SqlParameter("@Eventid", SqlDbType.Int, 4);
            param_Eventid.Value = obj.Eventid;
            ht.Add(param_Eventid);
            SqlParameter param_Reporterid = new SqlParameter("@Reporterid", SqlDbType.Int, 4);
            param_Reporterid.Value = obj.Reporterid;
            ht.Add(param_Reporterid);
            SqlParameter param_Relationdate = new SqlParameter("@Relationdate", SqlDbType.DateTime, 8);
            param_Relationdate.Value = ESP.Media.Access.Utilities.Common.StringToDateTime(obj.Relationdate);
            ht.Add(param_Relationdate);
            SqlParameter param_Relationuserid = new SqlParameter("@Relationuserid", SqlDbType.Int, 4);
            param_Relationuserid.Value = obj.Relationuserid;
            ht.Add(param_Relationuserid);
            SqlParameter param_Remark = new SqlParameter("@Remark", SqlDbType.NVarChar, 1000);
            param_Remark.Value = obj.Remark;
            ht.Add(param_Remark);
            SqlParameter param_Del = new SqlParameter("@Del", SqlDbType.SmallInt, 2);
            param_Del.Value = obj.Del;
            ht.Add(param_Del);
            SqlParameter param_Paytype = new SqlParameter("@Paytype", SqlDbType.Int, 4);
            param_Paytype.Value = obj.Paytype;
            ht.Add(param_Paytype);
            SqlParameter param_Bankname = new SqlParameter("@Bankname", SqlDbType.NVarChar, 100);
            param_Bankname.Value = obj.Bankname;
            ht.Add(param_Bankname);
            SqlParameter param_Bankcardcode = new SqlParameter("@Bankcardcode", SqlDbType.NVarChar, 100);
            param_Bankcardcode.Value = obj.Bankcardcode;
            ht.Add(param_Bankcardcode);
            SqlParameter param_Bankcardname = new SqlParameter("@Bankcardname", SqlDbType.NVarChar, 100);
            param_Bankcardname.Value = obj.Bankcardname;
            ht.Add(param_Bankcardname);
            SqlParameter param_Bankacountname = new SqlParameter("@Bankacountname", SqlDbType.NVarChar, 100);
            param_Bankacountname.Value = obj.Bankacountname;
            ht.Add(param_Bankacountname);
            SqlParameter param_Writingfee = new SqlParameter("@Writingfee", SqlDbType.Float, 8);
            param_Writingfee.Value = obj.Writingfee;
            ht.Add(param_Writingfee);
            SqlParameter param_Referral = new SqlParameter("@Referral", SqlDbType.NVarChar, 100);
            param_Referral.Value = obj.Referral;
            ht.Add(param_Referral);
            SqlParameter param_Haveinvoice = new SqlParameter("@Haveinvoice", SqlDbType.Int, 4);
            param_Haveinvoice.Value = obj.Haveinvoice;
            ht.Add(param_Haveinvoice);
            SqlParameter param_Paystatus = new SqlParameter("@Paystatus", SqlDbType.Int, 4);
            param_Paystatus.Value = obj.Paystatus;
            ht.Add(param_Paystatus);
            SqlParameter param_Financecode = new SqlParameter("@Financecode", SqlDbType.NVarChar, 100);
            param_Financecode.Value = obj.Financecode;
            ht.Add(param_Financecode);
            SqlParameter param_Uploadstarttime = new SqlParameter("@Uploadstarttime", SqlDbType.DateTime, 8);
            param_Uploadstarttime.Value = ESP.Media.Access.Utilities.Common.StringToDateTime(obj.Uploadstarttime);
            ht.Add(param_Uploadstarttime);
            SqlParameter param_Uploadendtime = new SqlParameter("@Uploadendtime", SqlDbType.DateTime, 8);
            param_Uploadendtime.Value = ESP.Media.Access.Utilities.Common.StringToDateTime(obj.Uploadendtime);
            ht.Add(param_Uploadendtime);
            SqlParameter param_Paymentmode = new SqlParameter("@Paymentmode", SqlDbType.Int, 4);
            param_Paymentmode.Value = obj.Paymentmode;
            ht.Add(param_Paymentmode);
            SqlParameter param_Privateremark = new SqlParameter("@Privateremark", SqlDbType.NVarChar, 1000);
            param_Privateremark.Value = obj.Privateremark;
            ht.Add(param_Privateremark);
            SqlParameter param_Cooperatecircs = new SqlParameter("@Cooperatecircs", SqlDbType.NVarChar, 1000);
            param_Cooperatecircs.Value = obj.Cooperatecircs;
            ht.Add(param_Cooperatecircs);
            return sql;
        }


        //插入一条记录
        public static int insertinfo(EventreporterrelationInfo obj, SqlTransaction trans)
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
        public static int insertinfo(EventreporterrelationInfo obj)
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
            string sql = "delete media_eventreporterrelation where id=@id";
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
        public static string getUpdateString(EventreporterrelationInfo objTerm, EventreporterrelationInfo Objupdate, string term, ref List<SqlParameter> ht, params SqlParameter[] param)
        {
            string sql = "update media_eventreporterrelation set eventid=@eventid,reporterid=@reporterid,relationdate=@relationdate,relationuserid=@relationuserid,remark=@remark,del=@del,paytype=@paytype,bankname=@bankname,bankcardcode=@bankcardcode,bankcardname=@bankcardname,bankacountname=@bankacountname,writingfee=@writingfee,referral=@referral,haveinvoice=@haveinvoice,paystatus=@paystatus,financecode=@financecode,uploadstarttime=@uploadstarttime,uploadendtime=@uploadendtime,paymentmode=@paymentmode,privateremark=@privateremark,cooperatecircs=@cooperatecircs where 1=1 ";
            SqlParameter param_eventid = new SqlParameter("@eventid", SqlDbType.Int, 4);
            param_eventid.Value = Objupdate.Eventid;
            ht.Add(param_eventid);
            SqlParameter param_reporterid = new SqlParameter("@reporterid", SqlDbType.Int, 4);
            param_reporterid.Value = Objupdate.Reporterid;
            ht.Add(param_reporterid);
            SqlParameter param_relationdate = new SqlParameter("@relationdate", SqlDbType.DateTime, 8);
            param_relationdate.Value = ESP.Media.Access.Utilities.Common.StringToDateTime(Objupdate.Relationdate);
            ht.Add(param_relationdate);
            SqlParameter param_relationuserid = new SqlParameter("@relationuserid", SqlDbType.Int, 4);
            param_relationuserid.Value = Objupdate.Relationuserid;
            ht.Add(param_relationuserid);
            SqlParameter param_remark = new SqlParameter("@remark", SqlDbType.NVarChar, 1000);
            param_remark.Value = Objupdate.Remark;
            ht.Add(param_remark);
            SqlParameter param_del = new SqlParameter("@del", SqlDbType.SmallInt, 2);
            param_del.Value = Objupdate.Del;
            ht.Add(param_del);
            SqlParameter param_paytype = new SqlParameter("@paytype", SqlDbType.Int, 4);
            param_paytype.Value = Objupdate.Paytype;
            ht.Add(param_paytype);
            SqlParameter param_bankname = new SqlParameter("@bankname", SqlDbType.NVarChar, 100);
            param_bankname.Value = Objupdate.Bankname;
            ht.Add(param_bankname);
            SqlParameter param_bankcardcode = new SqlParameter("@bankcardcode", SqlDbType.NVarChar, 100);
            param_bankcardcode.Value = Objupdate.Bankcardcode;
            ht.Add(param_bankcardcode);
            SqlParameter param_bankcardname = new SqlParameter("@bankcardname", SqlDbType.NVarChar, 100);
            param_bankcardname.Value = Objupdate.Bankcardname;
            ht.Add(param_bankcardname);
            SqlParameter param_bankacountname = new SqlParameter("@bankacountname", SqlDbType.NVarChar, 100);
            param_bankacountname.Value = Objupdate.Bankacountname;
            ht.Add(param_bankacountname);
            SqlParameter param_writingfee = new SqlParameter("@writingfee", SqlDbType.Float, 8);
            param_writingfee.Value = Objupdate.Writingfee;
            ht.Add(param_writingfee);
            SqlParameter param_referral = new SqlParameter("@referral", SqlDbType.NVarChar, 100);
            param_referral.Value = Objupdate.Referral;
            ht.Add(param_referral);
            SqlParameter param_haveinvoice = new SqlParameter("@haveinvoice", SqlDbType.Int, 4);
            param_haveinvoice.Value = Objupdate.Haveinvoice;
            ht.Add(param_haveinvoice);
            SqlParameter param_paystatus = new SqlParameter("@paystatus", SqlDbType.Int, 4);
            param_paystatus.Value = Objupdate.Paystatus;
            ht.Add(param_paystatus);
            SqlParameter param_financecode = new SqlParameter("@financecode", SqlDbType.NVarChar, 100);
            param_financecode.Value = Objupdate.Financecode;
            ht.Add(param_financecode);
            SqlParameter param_uploadstarttime = new SqlParameter("@uploadstarttime", SqlDbType.DateTime, 8);
            param_uploadstarttime.Value = ESP.Media.Access.Utilities.Common.StringToDateTime(Objupdate.Uploadstarttime);
            ht.Add(param_uploadstarttime);
            SqlParameter param_uploadendtime = new SqlParameter("@uploadendtime", SqlDbType.DateTime, 8);
            param_uploadendtime.Value = ESP.Media.Access.Utilities.Common.StringToDateTime(Objupdate.Uploadendtime);
            ht.Add(param_uploadendtime);
            SqlParameter param_paymentmode = new SqlParameter("@paymentmode", SqlDbType.Int, 4);
            param_paymentmode.Value = Objupdate.Paymentmode;
            ht.Add(param_paymentmode);
            SqlParameter param_privateremark = new SqlParameter("@privateremark", SqlDbType.NVarChar, 1000);
            param_privateremark.Value = Objupdate.Privateremark;
            ht.Add(param_privateremark);
            SqlParameter param_cooperatecircs = new SqlParameter("@cooperatecircs", SqlDbType.NVarChar, 1000);
            param_cooperatecircs.Value = Objupdate.Cooperatecircs;
            ht.Add(param_cooperatecircs);
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
        public static bool updateInfo(SqlTransaction trans, EventreporterrelationInfo objterm, EventreporterrelationInfo Objupdate, string term, params SqlParameter[] param)
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
        public static bool updateInfo(EventreporterrelationInfo objterm, EventreporterrelationInfo Objupdate, string term, params SqlParameter[] param)
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


        private static string getTerms(EventreporterrelationInfo obj, ref List<SqlParameter> ht)
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
            if (obj.Eventid > 0)//EventID
            {
                term += " and eventid=@eventid ";
                if (ESP.Media.Access.Utilities.Common.Find(ht, "@eventid") == -1)
                {
                    SqlParameter p = new SqlParameter("@eventid", SqlDbType.Int, 4);
                    p.Value = obj.Eventid;
                    ht.Add(p);
                }
            }
            if (obj.Reporterid > 0)//ReporterID
            {
                term += " and reporterid=@reporterid ";
                if (ESP.Media.Access.Utilities.Common.Find(ht, "@reporterid") == -1)
                {
                    SqlParameter p = new SqlParameter("@reporterid", SqlDbType.Int, 4);
                    p.Value = obj.Reporterid;
                    ht.Add(p);
                }
            }
            if (obj.Relationdate != null && obj.Relationdate.Trim().Length > 0)
            {
                term += " and relationdate=@relationdate ";
                if (ESP.Media.Access.Utilities.Common.Find(ht, "@relationdate") == -1)
                {
                    SqlParameter p = new SqlParameter("@relationdate", SqlDbType.DateTime, 8);
                    p.Value = ESP.Media.Access.Utilities.Common.StringToDateTime(obj.Relationdate);
                    ht.Add(p);
                }
            }
            if (obj.Relationuserid > 0)//RelationUserID
            {
                term += " and relationuserid=@relationuserid ";
                if (ESP.Media.Access.Utilities.Common.Find(ht, "@relationuserid") == -1)
                {
                    SqlParameter p = new SqlParameter("@relationuserid", SqlDbType.Int, 4);
                    p.Value = obj.Relationuserid;
                    ht.Add(p);
                }
            }
            if (obj.Remark != null && obj.Remark.Trim().Length > 0)
            {
                term += " and remark=@remark ";
                if (ESP.Media.Access.Utilities.Common.Find(ht, "@remark") == -1)
                {
                    SqlParameter p = new SqlParameter("@remark", SqlDbType.NVarChar, 1000);
                    p.Value = obj.Remark;
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
            if (obj.Paytype > 0)//PayType
            {
                term += " and paytype=@paytype ";
                if (ESP.Media.Access.Utilities.Common.Find(ht, "@paytype") == -1)
                {
                    SqlParameter p = new SqlParameter("@paytype", SqlDbType.Int, 4);
                    p.Value = obj.Paytype;
                    ht.Add(p);
                }
            }
            if (obj.Bankname != null && obj.Bankname.Trim().Length > 0)
            {
                term += " and bankname=@bankname ";
                if (ESP.Media.Access.Utilities.Common.Find(ht, "@bankname") == -1)
                {
                    SqlParameter p = new SqlParameter("@bankname", SqlDbType.NVarChar, 100);
                    p.Value = obj.Bankname;
                    ht.Add(p);
                }
            }
            if (obj.Bankcardcode != null && obj.Bankcardcode.Trim().Length > 0)
            {
                term += " and bankcardcode=@bankcardcode ";
                if (ESP.Media.Access.Utilities.Common.Find(ht, "@bankcardcode") == -1)
                {
                    SqlParameter p = new SqlParameter("@bankcardcode", SqlDbType.NVarChar, 100);
                    p.Value = obj.Bankcardcode;
                    ht.Add(p);
                }
            }
            if (obj.Bankcardname != null && obj.Bankcardname.Trim().Length > 0)
            {
                term += " and bankcardname=@bankcardname ";
                if (ESP.Media.Access.Utilities.Common.Find(ht, "@bankcardname") == -1)
                {
                    SqlParameter p = new SqlParameter("@bankcardname", SqlDbType.NVarChar, 100);
                    p.Value = obj.Bankcardname;
                    ht.Add(p);
                }
            }
            if (obj.Bankacountname != null && obj.Bankacountname.Trim().Length > 0)
            {
                term += " and bankacountname=@bankacountname ";
                if (ESP.Media.Access.Utilities.Common.Find(ht, "@bankacountname") == -1)
                {
                    SqlParameter p = new SqlParameter("@bankacountname", SqlDbType.NVarChar, 100);
                    p.Value = obj.Bankacountname;
                    ht.Add(p);
                }
            }
            if (obj.Writingfee > 0)//writingfee
            {
                term += " and writingfee=@writingfee ";
                if (ESP.Media.Access.Utilities.Common.Find(ht, "@writingfee") == -1)
                {
                    SqlParameter p = new SqlParameter("@writingfee", SqlDbType.Float, 8);
                    p.Value = obj.Writingfee;
                    ht.Add(p);
                }
            }
            if (obj.Referral != null && obj.Referral.Trim().Length > 0)
            {
                term += " and referral=@referral ";
                if (ESP.Media.Access.Utilities.Common.Find(ht, "@referral") == -1)
                {
                    SqlParameter p = new SqlParameter("@referral", SqlDbType.NVarChar, 100);
                    p.Value = obj.Referral;
                    ht.Add(p);
                }
            }
            if (obj.Haveinvoice > 0)//haveInvoice
            {
                term += " and haveinvoice=@haveinvoice ";
                if (ESP.Media.Access.Utilities.Common.Find(ht, "@haveinvoice") == -1)
                {
                    SqlParameter p = new SqlParameter("@haveinvoice", SqlDbType.Int, 4);
                    p.Value = obj.Haveinvoice;
                    ht.Add(p);
                }
            }
            if (obj.Paystatus > 0)//paystatus
            {
                term += " and paystatus=@paystatus ";
                if (ESP.Media.Access.Utilities.Common.Find(ht, "@paystatus") == -1)
                {
                    SqlParameter p = new SqlParameter("@paystatus", SqlDbType.Int, 4);
                    p.Value = obj.Paystatus;
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
            if (obj.Uploadstarttime != null && obj.Uploadstarttime.Trim().Length > 0)
            {
                term += " and uploadstarttime=@uploadstarttime ";
                if (ESP.Media.Access.Utilities.Common.Find(ht, "@uploadstarttime") == -1)
                {
                    SqlParameter p = new SqlParameter("@uploadstarttime", SqlDbType.DateTime, 8);
                    p.Value = ESP.Media.Access.Utilities.Common.StringToDateTime(obj.Uploadstarttime);
                    ht.Add(p);
                }
            }
            if (obj.Uploadendtime != null && obj.Uploadendtime.Trim().Length > 0)
            {
                term += " and uploadendtime=@uploadendtime ";
                if (ESP.Media.Access.Utilities.Common.Find(ht, "@uploadendtime") == -1)
                {
                    SqlParameter p = new SqlParameter("@uploadendtime", SqlDbType.DateTime, 8);
                    p.Value = ESP.Media.Access.Utilities.Common.StringToDateTime(obj.Uploadendtime);
                    ht.Add(p);
                }
            }
            if (obj.Paymentmode > 0)//付款方式
            {
                term += " and paymentmode=@paymentmode ";
                if (ESP.Media.Access.Utilities.Common.Find(ht, "@paymentmode") == -1)
                {
                    SqlParameter p = new SqlParameter("@paymentmode", SqlDbType.Int, 4);
                    p.Value = obj.Paymentmode;
                    ht.Add(p);
                }
            }
            if (obj.Privateremark != null && obj.Privateremark.Trim().Length > 0)
            {
                term += " and privateremark=@privateremark ";
                if (ESP.Media.Access.Utilities.Common.Find(ht, "@privateremark") == -1)
                {
                    SqlParameter p = new SqlParameter("@privateremark", SqlDbType.NVarChar, 1000);
                    p.Value = obj.Privateremark;
                    ht.Add(p);
                }
            }
            if (obj.Cooperatecircs != null && obj.Cooperatecircs.Trim().Length > 0)
            {
                term += " and cooperatecircs=@cooperatecircs ";
                if (ESP.Media.Access.Utilities.Common.Find(ht, "@cooperatecircs") == -1)
                {
                    SqlParameter p = new SqlParameter("@cooperatecircs", SqlDbType.NVarChar, 1000);
                    p.Value = obj.Cooperatecircs;
                    ht.Add(p);
                }
            }
            return term;
        }
        #endregion
        #region 查询
        private static string getQueryTerms(EventreporterrelationInfo obj, ref Hashtable ht)
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
            if (obj.Eventid > 0)//EventID
            {
                term += " and a.eventid=@eventid ";
                if (!ht.ContainsKey("@eventid"))
                {
                    ht.Add("@eventid", obj.Eventid);
                }
            }
            if (obj.Reporterid > 0)//ReporterID
            {
                term += " and a.reporterid=@reporterid ";
                if (!ht.ContainsKey("@reporterid"))
                {
                    ht.Add("@reporterid", obj.Reporterid);
                }
            }
            if (obj.Relationdate != null && obj.Relationdate.Trim().Length > 0)
            {
                term += " and a.relationdate=@relationdate ";
                if (!ht.ContainsKey("@relationdate"))
                {
                    ht.Add("@relationdate", obj.Relationdate);
                }
            }
            if (obj.Relationuserid > 0)//RelationUserID
            {
                term += " and a.relationuserid=@relationuserid ";
                if (!ht.ContainsKey("@relationuserid"))
                {
                    ht.Add("@relationuserid", obj.Relationuserid);
                }
            }
            if (obj.Remark != null && obj.Remark.Trim().Length > 0)
            {
                term += " and a.remark=@remark ";
                if (!ht.ContainsKey("@remark"))
                {
                    ht.Add("@remark", obj.Remark);
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
            if (obj.Paytype > 0)//PayType
            {
                term += " and a.paytype=@paytype ";
                if (!ht.ContainsKey("@paytype"))
                {
                    ht.Add("@paytype", obj.Paytype);
                }
            }
            if (obj.Bankname != null && obj.Bankname.Trim().Length > 0)
            {
                term += " and a.bankname=@bankname ";
                if (!ht.ContainsKey("@bankname"))
                {
                    ht.Add("@bankname", obj.Bankname);
                }
            }
            if (obj.Bankcardcode != null && obj.Bankcardcode.Trim().Length > 0)
            {
                term += " and a.bankcardcode=@bankcardcode ";
                if (!ht.ContainsKey("@bankcardcode"))
                {
                    ht.Add("@bankcardcode", obj.Bankcardcode);
                }
            }
            if (obj.Bankcardname != null && obj.Bankcardname.Trim().Length > 0)
            {
                term += " and a.bankcardname=@bankcardname ";
                if (!ht.ContainsKey("@bankcardname"))
                {
                    ht.Add("@bankcardname", obj.Bankcardname);
                }
            }
            if (obj.Bankacountname != null && obj.Bankacountname.Trim().Length > 0)
            {
                term += " and a.bankacountname=@bankacountname ";
                if (!ht.ContainsKey("@bankacountname"))
                {
                    ht.Add("@bankacountname", obj.Bankacountname);
                }
            }
            if (obj.Writingfee > 0)//writingfee
            {
                term += " and a.writingfee=@writingfee ";
                if (!ht.ContainsKey("@writingfee"))
                {
                    ht.Add("@writingfee", obj.Writingfee);
                }
            }
            if (obj.Referral != null && obj.Referral.Trim().Length > 0)
            {
                term += " and a.referral=@referral ";
                if (!ht.ContainsKey("@referral"))
                {
                    ht.Add("@referral", obj.Referral);
                }
            }
            if (obj.Haveinvoice > 0)//haveInvoice
            {
                term += " and a.haveinvoice=@haveinvoice ";
                if (!ht.ContainsKey("@haveinvoice"))
                {
                    ht.Add("@haveinvoice", obj.Haveinvoice);
                }
            }
            if (obj.Paystatus > 0)//paystatus
            {
                term += " and a.paystatus=@paystatus ";
                if (!ht.ContainsKey("@paystatus"))
                {
                    ht.Add("@paystatus", obj.Paystatus);
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
            if (obj.Uploadstarttime != null && obj.Uploadstarttime.Trim().Length > 0)
            {
                term += " and a.uploadstarttime=@uploadstarttime ";
                if (!ht.ContainsKey("@uploadstarttime"))
                {
                    ht.Add("@uploadstarttime", obj.Uploadstarttime);
                }
            }
            if (obj.Uploadendtime != null && obj.Uploadendtime.Trim().Length > 0)
            {
                term += " and a.uploadendtime=@uploadendtime ";
                if (!ht.ContainsKey("@uploadendtime"))
                {
                    ht.Add("@uploadendtime", obj.Uploadendtime);
                }
            }
            if (obj.Paymentmode > 0)//付款方式
            {
                term += " and a.paymentmode=@paymentmode ";
                if (!ht.ContainsKey("@paymentmode"))
                {
                    ht.Add("@paymentmode", obj.Paymentmode);
                }
            }
            if (obj.Privateremark != null && obj.Privateremark.Trim().Length > 0)
            {
                term += " and a.privateremark=@privateremark ";
                if (!ht.ContainsKey("@privateremark"))
                {
                    ht.Add("@privateremark", obj.Privateremark);
                }
            }
            if (obj.Cooperatecircs != null && obj.Cooperatecircs.Trim().Length > 0)
            {
                term += " and a.cooperatecircs=@cooperatecircs ";
                if (!ht.ContainsKey("@cooperatecircs"))
                {
                    ht.Add("@cooperatecircs", obj.Cooperatecircs);
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
            string sql = @"select {0} a.id as id,a.eventid as eventid,a.reporterid as reporterid,a.relationdate as relationdate,a.relationuserid as relationuserid,a.remark as remark,a.del as del,a.paytype as paytype,a.bankname as bankname,a.bankcardcode as bankcardcode,a.bankcardname as bankcardname,a.bankacountname as bankacountname,a.writingfee as writingfee,a.referral as referral,a.haveinvoice as haveinvoice,a.paystatus as paystatus,a.financecode as financecode,a.uploadstarttime as uploadstarttime,a.uploadendtime as uploadendtime,a.paymentmode as paymentmode,a.privateremark as privateremark,a.cooperatecircs as cooperatecircs {1} from media_eventreporterrelation as a {2} where 1=1 {3} ";
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


        public static DataTable QueryInfoByObj(EventreporterrelationInfo obj, string terms, Hashtable ht)
        {
            if (ht == null)
            {
                ht = new Hashtable();
            }
            SqlParameter[] para = ESP.Media.Access.Utilities.Common.DictToSqlParam(ht);
            return QueryInfoByObj(obj, terms, para);
        }


        public static DataTable QueryInfoByObj(EventreporterrelationInfo obj, string terms, params SqlParameter[] param)
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
        public static EventreporterrelationInfo Load(int id)
        {
            DataTable dt = null;
            dt = QueryInfo(" and a.id=@id ", new SqlParameter("@id", id));
            if (dt != null && dt.Rows.Count > 0)
            {
                return setObject(dt.Rows[0]);
            }
            return null;
        }


        public static EventreporterrelationInfo Load(int id, SqlTransaction trans)
        {
            DataTable dt = null;
            dt = QueryInfo(trans, " and a.id=@id ", new SqlParameter("@id", id));
            if (dt != null && dt.Rows.Count > 0)
            {
                return setObject(dt.Rows[0]);
            }
            return null;
        }


        public static EventreporterrelationInfo setObject(DataRow dr)
        {
            EventreporterrelationInfo obj = new EventreporterrelationInfo();
            if (dr.Table.Columns.Contains("id") && dr["id"] != DBNull.Value)//id
            {
                obj.Id = Convert.ToInt32(dr["id"]);
            }
            if (dr.Table.Columns.Contains("eventid") && dr["eventid"] != DBNull.Value)//事件传播编号
            {
                obj.Eventid = Convert.ToInt32(dr["eventid"]);
            }
            if (dr.Table.Columns.Contains("reporterid") && dr["reporterid"] != DBNull.Value)//记者编号
            {
                obj.Reporterid = Convert.ToInt32(dr["reporterid"]);
            }
            if (dr.Table.Columns.Contains("relationdate") && dr["relationdate"] != DBNull.Value)//关联日期
            {
                obj.Relationdate = (dr["relationdate"]).ToString();
            }
            if (dr.Table.Columns.Contains("relationuserid") && dr["relationuserid"] != DBNull.Value)//关联人ID
            {
                obj.Relationuserid = Convert.ToInt32(dr["relationuserid"]);
            }
            if (dr.Table.Columns.Contains("remark") && dr["remark"] != DBNull.Value)//备注
            {
                obj.Remark = (dr["remark"]).ToString();
            }
            if (dr.Table.Columns.Contains("del") && dr["del"] != DBNull.Value)//删除标记
            {
                obj.Del = Convert.ToInt32(dr["del"]);
            }
            if (dr.Table.Columns.Contains("paytype") && dr["paytype"] != DBNull.Value)//支付类型(0刊后,1刊前)
            {
                obj.Paytype = Convert.ToInt32(dr["paytype"]);
            }
            if (dr.Table.Columns.Contains("bankname") && dr["bankname"] != DBNull.Value)//bankname
            {
                obj.Bankname = (dr["bankname"]).ToString();
            }
            if (dr.Table.Columns.Contains("bankcardcode") && dr["bankcardcode"] != DBNull.Value)//bankcardcode
            {
                obj.Bankcardcode = (dr["bankcardcode"]).ToString();
            }
            if (dr.Table.Columns.Contains("bankcardname") && dr["bankcardname"] != DBNull.Value)//bankcardname
            {
                obj.Bankcardname = (dr["bankcardname"]).ToString();
            }
            if (dr.Table.Columns.Contains("bankacountname") && dr["bankacountname"] != DBNull.Value)//bankacountname
            {
                obj.Bankacountname = (dr["bankacountname"]).ToString();
            }
            if (dr.Table.Columns.Contains("writingfee") && dr["writingfee"] != DBNull.Value)//writingfee
            {
                obj.Writingfee = Convert.ToDouble(dr["writingfee"]);
            }
            if (dr.Table.Columns.Contains("referral") && dr["referral"] != DBNull.Value)//referral
            {
                obj.Referral = (dr["referral"]).ToString();
            }
            if (dr.Table.Columns.Contains("haveinvoice") && dr["haveinvoice"] != DBNull.Value)//haveinvoice
            {
                obj.Haveinvoice = Convert.ToInt32(dr["haveinvoice"]);
            }
            if (dr.Table.Columns.Contains("paystatus") && dr["paystatus"] != DBNull.Value)//paystatus
            {
                obj.Paystatus = Convert.ToInt32(dr["paystatus"]);
            }
            if (dr.Table.Columns.Contains("financecode") && dr["financecode"] != DBNull.Value)//财务流水号
            {
                obj.Financecode = (dr["financecode"]).ToString();
            }
            if (dr.Table.Columns.Contains("uploadstarttime") && dr["uploadstarttime"] != DBNull.Value)//剪报上传起始时间
            {
                obj.Uploadstarttime = (dr["uploadstarttime"]).ToString();
            }
            if (dr.Table.Columns.Contains("uploadendtime") && dr["uploadendtime"] != DBNull.Value)//剪报上传结束时间
            {
                obj.Uploadendtime = (dr["uploadendtime"]).ToString();
            }
            if (dr.Table.Columns.Contains("paymentmode") && dr["paymentmode"] != DBNull.Value)//付款方式
            {
                obj.Paymentmode = Convert.ToInt32(dr["paymentmode"]);
            }
            if (dr.Table.Columns.Contains("privateremark") && dr["privateremark"] != DBNull.Value)//备注
            {
                obj.Privateremark = (dr["privateremark"]).ToString();
            }
            if (dr.Table.Columns.Contains("cooperatecircs") && dr["cooperatecircs"] != DBNull.Value)//合作情况
            {
                obj.Cooperatecircs = (dr["cooperatecircs"]).ToString();
            }
            return obj;
        }
        #endregion
    }
}
