using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.Collections.Generic;
using ESP.Media.Entity;
using ESP.Media.Access.Utilities;
namespace ESP.Media.DataAccess
{
    public class ShortmsgsendDataProvider
    {
        #region ���캯��
        public ShortmsgsendDataProvider()
        {
        }
        #endregion
        #region ����
        //�����ַ���
        private static string strinsert(ShortmsgsendInfo obj, ref List<SqlParameter> ht)
        {
            if (ht == null)
            {
                ht = new List<SqlParameter>();
            }
            string sql = @"insert into media_shortmsgsend (shortmsgid,recvuserid,recvusertype,recvphoneno,senduserid,senddate,status,del,sendsubject,sendbody) values (@shortmsgid,@recvuserid,@recvusertype,@recvphoneno,@senduserid,@senddate,@status,@del,@sendsubject,@sendbody);select @@IDENTITY as rowNum;";
            SqlParameter param_Shortmsgid = new SqlParameter("@Shortmsgid", SqlDbType.Int, 4);
            param_Shortmsgid.Value = obj.Shortmsgid;
            ht.Add(param_Shortmsgid);
            SqlParameter param_Recvuserid = new SqlParameter("@Recvuserid", SqlDbType.Int, 4);
            param_Recvuserid.Value = obj.Recvuserid;
            ht.Add(param_Recvuserid);
            SqlParameter param_Recvusertype = new SqlParameter("@Recvusertype", SqlDbType.Int, 4);
            param_Recvusertype.Value = obj.Recvusertype;
            ht.Add(param_Recvusertype);
            SqlParameter param_Recvphoneno = new SqlParameter("@Recvphoneno", SqlDbType.NVarChar, 100);
            param_Recvphoneno.Value = obj.Recvphoneno;
            ht.Add(param_Recvphoneno);
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
            return sql;
        }


        //����һ����¼
        public static int insertinfo(ShortmsgsendInfo obj, SqlTransaction trans)
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
        public static int insertinfo(ShortmsgsendInfo obj)
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
            string sql = "delete media_shortmsgsend where id=@id";
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
        public static string getUpdateString(ShortmsgsendInfo objTerm, ShortmsgsendInfo Objupdate, string term, ref List<SqlParameter> ht, params SqlParameter[] param)
        {
            string sql = "update media_shortmsgsend set shortmsgid=@shortmsgid,recvuserid=@recvuserid,recvusertype=@recvusertype,recvphoneno=@recvphoneno,senduserid=@senduserid,senddate=@senddate,status=@status,del=@del,sendsubject=@sendsubject,sendbody=@sendbody where 1=1 ";
            SqlParameter param_shortmsgid = new SqlParameter("@shortmsgid", SqlDbType.Int, 4);
            param_shortmsgid.Value = Objupdate.Shortmsgid;
            ht.Add(param_shortmsgid);
            SqlParameter param_recvuserid = new SqlParameter("@recvuserid", SqlDbType.Int, 4);
            param_recvuserid.Value = Objupdate.Recvuserid;
            ht.Add(param_recvuserid);
            SqlParameter param_recvusertype = new SqlParameter("@recvusertype", SqlDbType.Int, 4);
            param_recvusertype.Value = Objupdate.Recvusertype;
            ht.Add(param_recvusertype);
            SqlParameter param_recvphoneno = new SqlParameter("@recvphoneno", SqlDbType.NVarChar, 100);
            param_recvphoneno.Value = Objupdate.Recvphoneno;
            ht.Add(param_recvphoneno);
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


        //���²���
        public static bool updateInfo(SqlTransaction trans, ShortmsgsendInfo objterm, ShortmsgsendInfo Objupdate, string term, params SqlParameter[] param)
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
        public static bool updateInfo(ShortmsgsendInfo objterm, ShortmsgsendInfo Objupdate, string term, params SqlParameter[] param)
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


        private static string getTerms(ShortmsgsendInfo obj, ref List<SqlParameter> ht)
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
            if (obj.Shortmsgid > 0)//����Ϣid
            {
                term += " and shortmsgid=@shortmsgid ";
                if (ESP.Media.Access.Utilities.Common.Find(ht, "@shortmsgid") == -1)
                {
                    SqlParameter p = new SqlParameter("@shortmsgid", SqlDbType.Int, 4);
                    p.Value = obj.Shortmsgid;
                    ht.Add(p);
                }
            }
            if (obj.Recvuserid > 0)//������id
            {
                term += " and recvuserid=@recvuserid ";
                if (ESP.Media.Access.Utilities.Common.Find(ht, "@recvuserid") == -1)
                {
                    SqlParameter p = new SqlParameter("@recvuserid", SqlDbType.Int, 4);
                    p.Value = obj.Recvuserid;
                    ht.Add(p);
                }
            }
            if (obj.Recvusertype > 0)//����������
            {
                term += " and recvusertype=@recvusertype ";
                if (ESP.Media.Access.Utilities.Common.Find(ht, "@recvusertype") == -1)
                {
                    SqlParameter p = new SqlParameter("@recvusertype", SqlDbType.Int, 4);
                    p.Value = obj.Recvusertype;
                    ht.Add(p);
                }
            }
            if (obj.Recvphoneno != null && obj.Recvphoneno.Trim().Length > 0)
            {
                term += " and recvphoneno=@recvphoneno ";
                if (ESP.Media.Access.Utilities.Common.Find(ht, "@recvphoneno") == -1)
                {
                    SqlParameter p = new SqlParameter("@recvphoneno", SqlDbType.NVarChar, 100);
                    p.Value = obj.Recvphoneno;
                    ht.Add(p);
                }
            }
            if (obj.Senduserid > 0)//������id
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
            if (obj.Status > 0)//״̬
            {
                term += " and status=@status ";
                if (ESP.Media.Access.Utilities.Common.Find(ht, "@status") == -1)
                {
                    SqlParameter p = new SqlParameter("@status", SqlDbType.Int, 4);
                    p.Value = obj.Status;
                    ht.Add(p);
                }
            }
            if (obj.Del > 0)//ɾ�����
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
            return term;
        }
        #endregion
        #region ��ѯ
        private static string getQueryTerms(ShortmsgsendInfo obj, ref Hashtable ht)
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
            if (obj.Shortmsgid > 0)//����Ϣid
            {
                term += " and a.shortmsgid=@shortmsgid ";
                if (!ht.ContainsKey("@shortmsgid"))
                {
                    ht.Add("@shortmsgid", obj.Shortmsgid);
                }
            }
            if (obj.Recvuserid > 0)//������id
            {
                term += " and a.recvuserid=@recvuserid ";
                if (!ht.ContainsKey("@recvuserid"))
                {
                    ht.Add("@recvuserid", obj.Recvuserid);
                }
            }
            if (obj.Recvusertype > 0)//����������
            {
                term += " and a.recvusertype=@recvusertype ";
                if (!ht.ContainsKey("@recvusertype"))
                {
                    ht.Add("@recvusertype", obj.Recvusertype);
                }
            }
            if (obj.Recvphoneno != null && obj.Recvphoneno.Trim().Length > 0)
            {
                term += " and a.recvphoneno=@recvphoneno ";
                if (!ht.ContainsKey("@recvphoneno"))
                {
                    ht.Add("@recvphoneno", obj.Recvphoneno);
                }
            }
            if (obj.Senduserid > 0)//������id
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
            if (obj.Status > 0)//״̬
            {
                term += " and a.status=@status ";
                if (!ht.ContainsKey("@status"))
                {
                    ht.Add("@status", obj.Status);
                }
            }
            if (obj.Del > 0)//ɾ�����
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
            string sql = @"select {0} a.id as id,a.shortmsgid as shortmsgid,a.recvuserid as recvuserid,a.recvusertype as recvusertype,a.recvphoneno as recvphoneno,a.senduserid as senduserid,a.senddate as senddate,a.status as status,a.del as del,a.sendsubject as sendsubject,a.sendbody as sendbody {1} from media_shortmsgsend as a {2} where 1=1 {3} ";
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


        public static DataTable QueryInfoByObj(ShortmsgsendInfo obj, string terms, Hashtable ht)
        {
            if (ht == null)
            {
                ht = new Hashtable();
            }
            SqlParameter[] para = ESP.Media.Access.Utilities.Common.DictToSqlParam(ht);
            return QueryInfoByObj(obj, terms, para);
        }


        public static DataTable QueryInfoByObj(ShortmsgsendInfo obj, string terms, params SqlParameter[] param)
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
        public static ShortmsgsendInfo Load(int id)
        {
            DataTable dt = null;
            dt = QueryInfo(" and a.id=@id ", new SqlParameter("@id", id));
            if (dt != null && dt.Rows.Count > 0)
            {
                return setObject(dt.Rows[0]);
            }
            return null;
        }


        public static ShortmsgsendInfo Load(int id, SqlTransaction trans)
        {
            DataTable dt = null;
            dt = QueryInfo(trans, " and a.id=@id ", new SqlParameter("@id", id));
            if (dt != null && dt.Rows.Count > 0)
            {
                return setObject(dt.Rows[0]);
            }
            return null;
        }


        public static ShortmsgsendInfo setObject(DataRow dr)
        {
            ShortmsgsendInfo obj = new ShortmsgsendInfo();
            if (dr.Table.Columns.Contains("id") && dr["id"] != DBNull.Value)//id
            {
                obj.Id = Convert.ToInt32(dr["id"]);
            }
            if (dr.Table.Columns.Contains("shortmsgid") && dr["shortmsgid"] != DBNull.Value)//����Ϣid
            {
                obj.Shortmsgid = Convert.ToInt32(dr["shortmsgid"]);
            }
            if (dr.Table.Columns.Contains("recvuserid") && dr["recvuserid"] != DBNull.Value)//������id
            {
                obj.Recvuserid = Convert.ToInt32(dr["recvuserid"]);
            }
            if (dr.Table.Columns.Contains("recvusertype") && dr["recvusertype"] != DBNull.Value)//����������
            {
                obj.Recvusertype = Convert.ToInt32(dr["recvusertype"]);
            }
            if (dr.Table.Columns.Contains("recvphoneno") && dr["recvphoneno"] != DBNull.Value)//���յĵ绰����
            {
                obj.Recvphoneno = (dr["recvphoneno"]).ToString();
            }
            if (dr.Table.Columns.Contains("senduserid") && dr["senduserid"] != DBNull.Value)//������id
            {
                obj.Senduserid = Convert.ToInt32(dr["senduserid"]);
            }
            if (dr.Table.Columns.Contains("senddate") && dr["senddate"] != DBNull.Value)//��������
            {
                obj.Senddate = (dr["senddate"]).ToString();
            }
            if (dr.Table.Columns.Contains("status") && dr["status"] != DBNull.Value)//״̬
            {
                obj.Status = Convert.ToInt32(dr["status"]);
            }
            if (dr.Table.Columns.Contains("del") && dr["del"] != DBNull.Value)//ɾ�����
            {
                obj.Del = Convert.ToInt32(dr["del"]);
            }
            if (dr.Table.Columns.Contains("sendsubject") && dr["sendsubject"] != DBNull.Value)//��������
            {
                obj.Sendsubject = (dr["sendsubject"]).ToString();
            }
            if (dr.Table.Columns.Contains("sendbody") && dr["sendbody"] != DBNull.Value)//��������
            {
                obj.Sendbody = (dr["sendbody"]).ToString();
            }
            return obj;
        }
        #endregion
    }
}