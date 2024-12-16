using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using ESP.Media.Entity;
using System.Collections.Generic;
using ESP.Media.Access.Utilities;
namespace ESP.Media.DataAccess
{
    public class EmailsendDataProvider
    {
        #region 构造函数
        public EmailsendDataProvider()
        {
        }
        #endregion
        #region 插入
        //插入字符串
        private static string strinsert(EmailsendInfo obj, ref List<SqlParameter> ht)
        {
            if (ht == null)
            {
                ht = new List<SqlParameter>();
            }
            string sql = @"insert into media_emailsend (emailid,recvuserid,recvusertype,recvaddress,senduserid,senddate,status,del,sendsubject,sendbody,sendattachmentspath) values (@emailid,@recvuserid,@recvusertype,@recvaddress,@senduserid,@senddate,@status,@del,@sendsubject,@sendbody,@sendattachmentspath);select @@IDENTITY as rowNum;";
            SqlParameter param_Emailid = new SqlParameter("@Emailid", SqlDbType.Int, 4);
            param_Emailid.Value = obj.Emailid;
            ht.Add(param_Emailid);
            SqlParameter param_Recvuserid = new SqlParameter("@Recvuserid", SqlDbType.Int, 4);
            param_Recvuserid.Value = obj.Recvuserid;
            ht.Add(param_Recvuserid);
            SqlParameter param_Recvusertype = new SqlParameter("@Recvusertype", SqlDbType.Int, 4);
            param_Recvusertype.Value = obj.Recvusertype;
            ht.Add(param_Recvusertype);
            SqlParameter param_Recvaddress = new SqlParameter("@Recvaddress", SqlDbType.NVarChar, 100);
            param_Recvaddress.Value = obj.Recvaddress;
            ht.Add(param_Recvaddress);
            SqlParameter param_Senduserid = new SqlParameter("@Senduserid", SqlDbType.Int, 4);
            param_Senduserid.Value = obj.Senduserid;
            ht.Add(param_Senduserid);
            SqlParameter param_Senddate = new SqlParameter("@Senddate", SqlDbType.SmallDateTime, 4);
            param_Senddate.Value = ESP.Media.Access.Utilities.Common.StringToDateTime(obj.Senddate);
            ht.Add(param_Senddate);
            SqlParameter param_Status = new SqlParameter("@Status", SqlDbType.Int, 4);
            param_Status.Value = obj.Status;
            ht.Add(param_Status);
            SqlParameter param_Del = new SqlParameter("@Del", SqlDbType.SmallInt, 2);
            param_Del.Value = obj.Del;
            ht.Add(param_Del);
            SqlParameter param_Sendsubject = new SqlParameter("@Sendsubject", SqlDbType.NVarChar, 200);
            param_Sendsubject.Value = obj.Sendsubject;
            ht.Add(param_Sendsubject);
            SqlParameter param_Sendbody = new SqlParameter("@Sendbody", SqlDbType.NVarChar, 2000);
            param_Sendbody.Value = obj.Sendbody;
            ht.Add(param_Sendbody);
            SqlParameter param_Sendattachmentspath = new SqlParameter("@Sendattachmentspath", SqlDbType.NVarChar, 1000);
            param_Sendattachmentspath.Value = obj.Sendattachmentspath;
            ht.Add(param_Sendattachmentspath);
            return sql;
        }


        //插入一条记录
        public static int insertinfo(EmailsendInfo obj, SqlTransaction trans)
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
        public static int insertinfo(EmailsendInfo obj)
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
            string sql = "delete media_emailsend where id=@id";
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
        public static string getUpdateString(EmailsendInfo objTerm, EmailsendInfo Objupdate, string term, ref List<SqlParameter> ht, params SqlParameter[] param)
        {
            string sql = "update media_emailsend set emailid=@emailid,recvuserid=@recvuserid,recvusertype=@recvusertype,recvaddress=@recvaddress,senduserid=@senduserid,senddate=@senddate,status=@status,del=@del,sendsubject=@sendsubject,sendbody=@sendbody,sendattachmentspath=@sendattachmentspath where 1=1 ";
            SqlParameter param_emailid = new SqlParameter("@emailid", SqlDbType.Int, 4);
            param_emailid.Value = Objupdate.Emailid;
            ht.Add(param_emailid);
            SqlParameter param_recvuserid = new SqlParameter("@recvuserid", SqlDbType.Int, 4);
            param_recvuserid.Value = Objupdate.Recvuserid;
            ht.Add(param_recvuserid);
            SqlParameter param_recvusertype = new SqlParameter("@recvusertype", SqlDbType.Int, 4);
            param_recvusertype.Value = Objupdate.Recvusertype;
            ht.Add(param_recvusertype);
            SqlParameter param_recvaddress = new SqlParameter("@recvaddress", SqlDbType.NVarChar, 100);
            param_recvaddress.Value = Objupdate.Recvaddress;
            ht.Add(param_recvaddress);
            SqlParameter param_senduserid = new SqlParameter("@senduserid", SqlDbType.Int, 4);
            param_senduserid.Value = Objupdate.Senduserid;
            ht.Add(param_senduserid);
            SqlParameter param_senddate = new SqlParameter("@senddate", SqlDbType.SmallDateTime, 4);
            param_senddate.Value = ESP.Media.Access.Utilities.Common.StringToDateTime(Objupdate.Senddate);
            ht.Add(param_senddate);
            SqlParameter param_status = new SqlParameter("@status", SqlDbType.Int, 4);
            param_status.Value = Objupdate.Status;
            ht.Add(param_status);
            SqlParameter param_del = new SqlParameter("@del", SqlDbType.SmallInt, 2);
            param_del.Value = Objupdate.Del;
            ht.Add(param_del);
            SqlParameter param_sendsubject = new SqlParameter("@sendsubject", SqlDbType.NVarChar, 200);
            param_sendsubject.Value = Objupdate.Sendsubject;
            ht.Add(param_sendsubject);
            SqlParameter param_sendbody = new SqlParameter("@sendbody", SqlDbType.NVarChar, 2000);
            param_sendbody.Value = Objupdate.Sendbody;
            ht.Add(param_sendbody);
            SqlParameter param_sendattachmentspath = new SqlParameter("@sendattachmentspath", SqlDbType.NVarChar, 1000);
            param_sendattachmentspath.Value = Objupdate.Sendattachmentspath;
            ht.Add(param_sendattachmentspath);
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
        public static bool updateInfo(SqlTransaction trans, EmailsendInfo objterm, EmailsendInfo Objupdate, string term, params SqlParameter[] param)
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
        public static bool updateInfo(EmailsendInfo objterm, EmailsendInfo Objupdate, string term, params SqlParameter[] param)
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


        private static string getTerms(EmailsendInfo obj, ref List<SqlParameter> ht)
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
            if (obj.Emailid > 0)//Email的ID
            {
                term += " and emailid=@emailid ";
                if (ESP.Media.Access.Utilities.Common.Find(ht, "@emailid") == -1)
                {
                    SqlParameter p = new SqlParameter("@emailid", SqlDbType.Int, 4);
                    p.Value = obj.Emailid;
                    ht.Add(p);
                }
            }
            if (obj.Recvuserid > 0)//接收人id
            {
                term += " and recvuserid=@recvuserid ";
                if (ESP.Media.Access.Utilities.Common.Find(ht, "@recvuserid") == -1)
                {
                    SqlParameter p = new SqlParameter("@recvuserid", SqlDbType.Int, 4);
                    p.Value = obj.Recvuserid;
                    ht.Add(p);
                }
            }
            if (obj.Recvusertype > 0)//接收人类型
            {
                term += " and recvusertype=@recvusertype ";
                if (ESP.Media.Access.Utilities.Common.Find(ht, "@recvusertype") == -1)
                {
                    SqlParameter p = new SqlParameter("@recvusertype", SqlDbType.Int, 4);
                    p.Value = obj.Recvusertype;
                    ht.Add(p);
                }
            }
            if (obj.Recvaddress != null && obj.Recvaddress.Trim().Length > 0)
            {
                term += " and recvaddress=@recvaddress ";
                if (ESP.Media.Access.Utilities.Common.Find(ht, "@recvaddress") == -1)
                {
                    SqlParameter p = new SqlParameter("@recvaddress", SqlDbType.NVarChar, 100);
                    p.Value = obj.Recvaddress;
                    ht.Add(p);
                }
            }
            if (obj.Senduserid > 0)//发送地址
            {
                term += " and senduserid=@senduserid ";
                if (ESP.Media.Access.Utilities.Common.Find(ht, "@senduserid") == -1)
                {
                    SqlParameter p = new SqlParameter("@senduserid", SqlDbType.Int, 4);
                    p.Value = obj.Senduserid;
                    ht.Add(p);
                }
            }
            if (obj.Senddate != null && obj.Senddate.Trim().Length > 0)
            {
                term += " and senddate=@senddate ";
                if (ESP.Media.Access.Utilities.Common.Find(ht, "@senddate") == -1)
                {
                    SqlParameter p = new SqlParameter("@senddate", SqlDbType.SmallDateTime, 4);
                    p.Value = ESP.Media.Access.Utilities.Common.StringToDateTime(obj.Senddate);
                    ht.Add(p);
                }
            }
            if (obj.Status > 0)//状态
            {
                term += " and status=@status ";
                if (ESP.Media.Access.Utilities.Common.Find(ht, "@status") == -1)
                {
                    SqlParameter p = new SqlParameter("@status", SqlDbType.Int, 4);
                    p.Value = obj.Status;
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
            if (obj.Sendsubject != null && obj.Sendsubject.Trim().Length > 0)
            {
                term += " and sendsubject=@sendsubject ";
                if (ESP.Media.Access.Utilities.Common.Find(ht, "@sendsubject") == -1)
                {
                    SqlParameter p = new SqlParameter("@sendsubject", SqlDbType.NVarChar, 200);
                    p.Value = obj.Sendsubject;
                    ht.Add(p);
                }
            }
            if (obj.Sendbody != null && obj.Sendbody.Trim().Length > 0)
            {
                term += " and sendbody=@sendbody ";
                if (ESP.Media.Access.Utilities.Common.Find(ht, "@sendbody") == -1)
                {
                    SqlParameter p = new SqlParameter("@sendbody", SqlDbType.NVarChar, 2000);
                    p.Value = obj.Sendbody;
                    ht.Add(p);
                }
            }
            if (obj.Sendattachmentspath != null && obj.Sendattachmentspath.Trim().Length > 0)
            {
                term += " and sendattachmentspath=@sendattachmentspath ";
                if (ESP.Media.Access.Utilities.Common.Find(ht, "@sendattachmentspath") == -1)
                {
                    SqlParameter p = new SqlParameter("@sendattachmentspath", SqlDbType.NVarChar, 1000);
                    p.Value = obj.Sendattachmentspath;
                    ht.Add(p);
                }
            }
            return term;
        }
        #endregion
        #region 查询
        private static string getQueryTerms(EmailsendInfo obj, ref Hashtable ht)
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
            if (obj.Emailid > 0)//Email的ID
            {
                term += " and a.emailid=@emailid ";
                if (!ht.ContainsKey("@emailid"))
                {
                    ht.Add("@emailid", obj.Emailid);
                }
            }
            if (obj.Recvuserid > 0)//接收人id
            {
                term += " and a.recvuserid=@recvuserid ";
                if (!ht.ContainsKey("@recvuserid"))
                {
                    ht.Add("@recvuserid", obj.Recvuserid);
                }
            }
            if (obj.Recvusertype > 0)//接收人类型
            {
                term += " and a.recvusertype=@recvusertype ";
                if (!ht.ContainsKey("@recvusertype"))
                {
                    ht.Add("@recvusertype", obj.Recvusertype);
                }
            }
            if (obj.Recvaddress != null && obj.Recvaddress.Trim().Length > 0)
            {
                term += " and a.recvaddress=@recvaddress ";
                if (!ht.ContainsKey("@recvaddress"))
                {
                    ht.Add("@recvaddress", obj.Recvaddress);
                }
            }
            if (obj.Senduserid > 0)//发送地址
            {
                term += " and a.senduserid=@senduserid ";
                if (!ht.ContainsKey("@senduserid"))
                {
                    ht.Add("@senduserid", obj.Senduserid);
                }
            }
            if (obj.Senddate != null && obj.Senddate.Trim().Length > 0)
            {
                term += " and a.senddate=@senddate ";
                if (!ht.ContainsKey("@senddate"))
                {
                    ht.Add("@senddate", obj.Senddate);
                }
            }
            if (obj.Status > 0)//状态
            {
                term += " and a.status=@status ";
                if (!ht.ContainsKey("@status"))
                {
                    ht.Add("@status", obj.Status);
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
            if (obj.Sendsubject != null && obj.Sendsubject.Trim().Length > 0)
            {
                term += " and a.sendsubject=@sendsubject ";
                if (!ht.ContainsKey("@sendsubject"))
                {
                    ht.Add("@sendsubject", obj.Sendsubject);
                }
            }
            if (obj.Sendbody != null && obj.Sendbody.Trim().Length > 0)
            {
                term += " and a.sendbody=@sendbody ";
                if (!ht.ContainsKey("@sendbody"))
                {
                    ht.Add("@sendbody", obj.Sendbody);
                }
            }
            if (obj.Sendattachmentspath != null && obj.Sendattachmentspath.Trim().Length > 0)
            {
                term += " and a.sendattachmentspath=@sendattachmentspath ";
                if (!ht.ContainsKey("@sendattachmentspath"))
                {
                    ht.Add("@sendattachmentspath", obj.Sendattachmentspath);
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
            string sql = @"select {0} a.id as id,a.emailid as emailid,a.recvuserid as recvuserid,a.recvusertype as recvusertype,a.recvaddress as recvaddress,a.senduserid as senduserid,a.senddate as senddate,a.status as status,a.del as del,a.sendsubject as sendsubject,a.sendbody as sendbody,a.sendattachmentspath as sendattachmentspath {1} from media_emailsend as a {2} where 1=1 {3} ";
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


        public static DataTable QueryInfoByObj(EmailsendInfo obj, string terms, Hashtable ht)
        {
            if (ht == null)
            {
                ht = new Hashtable();
            }
            SqlParameter[] para = ESP.Media.Access.Utilities.Common.DictToSqlParam(ht);
            return QueryInfoByObj(obj, terms, para);
        }


        public static DataTable QueryInfoByObj(EmailsendInfo obj, string terms, params SqlParameter[] param)
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
        public static EmailsendInfo Load(int id)
        {
            DataTable dt = null;
            dt = QueryInfo(" and a.id=@id ", new SqlParameter("@id", id));
            if (dt != null && dt.Rows.Count > 0)
            {
                return setObject(dt.Rows[0]);
            }
            return null;
        }


        public static EmailsendInfo Load(int id, SqlTransaction trans)
        {
            DataTable dt = null;
            dt = QueryInfo(trans, " and a.id=@id ", new SqlParameter("@id", id));
            if (dt != null && dt.Rows.Count > 0)
            {
                return setObject(dt.Rows[0]);
            }
            return null;
        }


        public static EmailsendInfo setObject(DataRow dr)
        {
            EmailsendInfo obj = new EmailsendInfo();
            if (dr.Table.Columns.Contains("id") && dr["id"] != DBNull.Value)//id
            {
                obj.Id = Convert.ToInt32(dr["id"]);
            }
            if (dr.Table.Columns.Contains("emailid") && dr["emailid"] != DBNull.Value)//Email的ID
            {
                obj.Emailid = Convert.ToInt32(dr["emailid"]);
            }
            if (dr.Table.Columns.Contains("recvuserid") && dr["recvuserid"] != DBNull.Value)//接收人id
            {
                obj.Recvuserid = Convert.ToInt32(dr["recvuserid"]);
            }
            if (dr.Table.Columns.Contains("recvusertype") && dr["recvusertype"] != DBNull.Value)//接收人类型
            {
                obj.Recvusertype = Convert.ToInt32(dr["recvusertype"]);
            }
            if (dr.Table.Columns.Contains("recvaddress") && dr["recvaddress"] != DBNull.Value)//接收地址
            {
                obj.Recvaddress = (dr["recvaddress"]).ToString();
            }
            if (dr.Table.Columns.Contains("senduserid") && dr["senduserid"] != DBNull.Value)//发送地址
            {
                obj.Senduserid = Convert.ToInt32(dr["senduserid"]);
            }
            if (dr.Table.Columns.Contains("senddate") && dr["senddate"] != DBNull.Value)//发送日期
            {
                obj.Senddate = (dr["senddate"]).ToString();
            }
            if (dr.Table.Columns.Contains("status") && dr["status"] != DBNull.Value)//状态
            {
                obj.Status = Convert.ToInt32(dr["status"]);
            }
            if (dr.Table.Columns.Contains("del") && dr["del"] != DBNull.Value)//删除标记
            {
                obj.Del = Convert.ToInt32(dr["del"]);
            }
            if (dr.Table.Columns.Contains("sendsubject") && dr["sendsubject"] != DBNull.Value)//发送主题
            {
                obj.Sendsubject = (dr["sendsubject"]).ToString();
            }
            if (dr.Table.Columns.Contains("sendbody") && dr["sendbody"] != DBNull.Value)//发送内容
            {
                obj.Sendbody = (dr["sendbody"]).ToString();
            }
            if (dr.Table.Columns.Contains("sendattachmentspath") && dr["sendattachmentspath"] != DBNull.Value)//sendattachmentspath
            {
                obj.Sendattachmentspath = (dr["sendattachmentspath"]).ToString();
            }
            return obj;
        }
        #endregion
    }
}
