using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.Collections.Generic;
using ESP.Media.Access.Utilities;
using ESP.Media.Entity;
namespace ESP.Media.DataAccess
{
    public class PostsDataProvider
    {
        #region 构造函数
        public PostsDataProvider()
        {
        }
        #endregion
        #region 插入
        //插入字符串
        private static string strinsert(PostsInfo obj, ref List<SqlParameter> ht)
        {
            if (ht == null)
            {
                ht = new List<SqlParameter>();
            }
            string sql = @"insert into media_posts (parentid,no,type,issysmsg,issuedate,userid,subject,body,lastreplyuserid,lastreplydate,del,begindate,enddate) values (@parentid,@no,@type,@issysmsg,@issuedate,@userid,@subject,@body,@lastreplyuserid,@lastreplydate,@del,@begindate,@enddate);select @@IDENTITY as rowNum;";
            SqlParameter param_Parentid = new SqlParameter("@Parentid", SqlDbType.Int, 4);
            param_Parentid.Value = obj.Parentid;
            ht.Add(param_Parentid);
            SqlParameter param_No = new SqlParameter("@No", SqlDbType.Int, 4);
            param_No.Value = obj.No;
            ht.Add(param_No);
            SqlParameter param_Type = new SqlParameter("@Type", SqlDbType.Int, 4);
            param_Type.Value = obj.Type;
            ht.Add(param_Type);
            SqlParameter param_Issysmsg = new SqlParameter("@Issysmsg", SqlDbType.Int, 4);
            param_Issysmsg.Value = obj.Issysmsg;
            ht.Add(param_Issysmsg);
            SqlParameter param_Issuedate = new SqlParameter("@Issuedate", SqlDbType.SmallDateTime, 4);
            param_Issuedate.Value = ESP.Media.Access.Utilities.Common.StringToDateTime(obj.Issuedate);
            ht.Add(param_Issuedate);
            SqlParameter param_Userid = new SqlParameter("@Userid", SqlDbType.Int, 4);
            param_Userid.Value = obj.Userid;
            ht.Add(param_Userid);
            SqlParameter param_Subject = new SqlParameter("@Subject", SqlDbType.NVarChar, 100);
            param_Subject.Value = obj.Subject;
            ht.Add(param_Subject);
            SqlParameter param_Body = new SqlParameter("@Body", SqlDbType.NVarChar, 4000);
            param_Body.Value = obj.Body;
            ht.Add(param_Body);
            SqlParameter param_Lastreplyuserid = new SqlParameter("@Lastreplyuserid", SqlDbType.Int, 4);
            param_Lastreplyuserid.Value = obj.Lastreplyuserid;
            ht.Add(param_Lastreplyuserid);
            SqlParameter param_Lastreplydate = new SqlParameter("@Lastreplydate", SqlDbType.SmallDateTime, 4);
            param_Lastreplydate.Value = ESP.Media.Access.Utilities.Common.StringToDateTime(obj.Lastreplydate);
            ht.Add(param_Lastreplydate);
            SqlParameter param_Del = new SqlParameter("@Del", SqlDbType.SmallInt, 2);
            param_Del.Value = obj.Del;
            ht.Add(param_Del);
            SqlParameter param_Begindate = new SqlParameter("@Begindate", SqlDbType.DateTime, 8);
            param_Begindate.Value = ESP.Media.Access.Utilities.Common.StringToDateTime(obj.Begindate);
            ht.Add(param_Begindate);
            SqlParameter param_Enddate = new SqlParameter("@Enddate", SqlDbType.DateTime, 8);
            param_Enddate.Value = ESP.Media.Access.Utilities.Common.StringToDateTime(obj.Enddate);
            ht.Add(param_Enddate);
            return sql;
        }


        //插入一条记录
        public static int insertinfo(PostsInfo obj, SqlTransaction trans)
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
        public static int insertinfo(PostsInfo obj)
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
            string sql = "delete media_posts where id=@id";
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
        public static string getUpdateString(PostsInfo objTerm, PostsInfo Objupdate, string term, ref List<SqlParameter> ht, params SqlParameter[] param)
        {
            string sql = "update media_posts set parentid=@parentid,no=@no,type=@type,issysmsg=@issysmsg,issuedate=@issuedate,userid=@userid,subject=@subject,body=@body,lastreplyuserid=@lastreplyuserid,lastreplydate=@lastreplydate,del=@del,begindate=@begindate,enddate=@enddate where 1=1 ";
            SqlParameter param_parentid = new SqlParameter("@parentid", SqlDbType.Int, 4);
            param_parentid.Value = Objupdate.Parentid;
            ht.Add(param_parentid);
            SqlParameter param_no = new SqlParameter("@no", SqlDbType.Int, 4);
            param_no.Value = Objupdate.No;
            ht.Add(param_no);
            SqlParameter param_type = new SqlParameter("@type", SqlDbType.Int, 4);
            param_type.Value = Objupdate.Type;
            ht.Add(param_type);
            SqlParameter param_issysmsg = new SqlParameter("@issysmsg", SqlDbType.Int, 4);
            param_issysmsg.Value = Objupdate.Issysmsg;
            ht.Add(param_issysmsg);
            SqlParameter param_issuedate = new SqlParameter("@issuedate", SqlDbType.SmallDateTime, 4);
            param_issuedate.Value = ESP.Media.Access.Utilities.Common.StringToDateTime(Objupdate.Issuedate);
            ht.Add(param_issuedate);
            SqlParameter param_userid = new SqlParameter("@userid", SqlDbType.Int, 4);
            param_userid.Value = Objupdate.Userid;
            ht.Add(param_userid);
            SqlParameter param_subject = new SqlParameter("@subject", SqlDbType.NVarChar, 100);
            param_subject.Value = Objupdate.Subject;
            ht.Add(param_subject);
            SqlParameter param_body = new SqlParameter("@body", SqlDbType.NVarChar, 4000);
            param_body.Value = Objupdate.Body;
            ht.Add(param_body);
            SqlParameter param_lastreplyuserid = new SqlParameter("@lastreplyuserid", SqlDbType.Int, 4);
            param_lastreplyuserid.Value = Objupdate.Lastreplyuserid;
            ht.Add(param_lastreplyuserid);
            SqlParameter param_lastreplydate = new SqlParameter("@lastreplydate", SqlDbType.SmallDateTime, 4);
            param_lastreplydate.Value = ESP.Media.Access.Utilities.Common.StringToDateTime(Objupdate.Lastreplydate);
            ht.Add(param_lastreplydate);
            SqlParameter param_del = new SqlParameter("@del", SqlDbType.SmallInt, 2);
            param_del.Value = Objupdate.Del;
            ht.Add(param_del);
            SqlParameter param_begindate = new SqlParameter("@begindate", SqlDbType.DateTime, 8);
            param_begindate.Value = ESP.Media.Access.Utilities.Common.StringToDateTime(Objupdate.Begindate);
            ht.Add(param_begindate);
            SqlParameter param_enddate = new SqlParameter("@enddate", SqlDbType.DateTime, 8);
            param_enddate.Value = ESP.Media.Access.Utilities.Common.StringToDateTime(Objupdate.Enddate);
            ht.Add(param_enddate);
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
        public static bool updateInfo(SqlTransaction trans, PostsInfo objterm, PostsInfo Objupdate, string term, params SqlParameter[] param)
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
        public static bool updateInfo(PostsInfo objterm, PostsInfo Objupdate, string term, params SqlParameter[] param)
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


        private static string getTerms(PostsInfo obj, ref List<SqlParameter> ht)
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
            if (obj.Parentid > 0)//上级id
            {
                term += " and parentid=@parentid ";
                if (ESP.Media.Access.Utilities.Common.Find(ht, "@parentid") == -1)
                {
                    SqlParameter p = new SqlParameter("@parentid", SqlDbType.Int, 4);
                    p.Value = obj.Parentid;
                    ht.Add(p);
                }
            }
            if (obj.No > 0)//编号
            {
                term += " and no=@no ";
                if (ESP.Media.Access.Utilities.Common.Find(ht, "@no") == -1)
                {
                    SqlParameter p = new SqlParameter("@no", SqlDbType.Int, 4);
                    p.Value = obj.No;
                    ht.Add(p);
                }
            }
            if (obj.Type > 0)//类型
            {
                term += " and type=@type ";
                if (ESP.Media.Access.Utilities.Common.Find(ht, "@type") == -1)
                {
                    SqlParameter p = new SqlParameter("@type", SqlDbType.Int, 4);
                    p.Value = obj.Type;
                    ht.Add(p);
                }
            }
            if (obj.Issysmsg > 0)//是否是系统消息
            {
                term += " and issysmsg=@issysmsg ";
                if (ESP.Media.Access.Utilities.Common.Find(ht, "@issysmsg") == -1)
                {
                    SqlParameter p = new SqlParameter("@issysmsg", SqlDbType.Int, 4);
                    p.Value = obj.Issysmsg;
                    ht.Add(p);
                }
            }
            if (obj.Issuedate != null && obj.Issuedate.Trim().Length > 0)
            {
                term += " and issuedate=@issuedate ";
                if (ESP.Media.Access.Utilities.Common.Find(ht, "@issuedate") == -1)
                {
                    SqlParameter p = new SqlParameter("@issuedate", SqlDbType.SmallDateTime, 4);
                    p.Value = ESP.Media.Access.Utilities.Common.StringToDateTime(obj.Issuedate);
                    ht.Add(p);
                }
            }
            if (obj.Userid > 0)//用户id
            {
                term += " and userid=@userid ";
                if (ESP.Media.Access.Utilities.Common.Find(ht, "@userid") == -1)
                {
                    SqlParameter p = new SqlParameter("@userid", SqlDbType.Int, 4);
                    p.Value = obj.Userid;
                    ht.Add(p);
                }
            }
            if (obj.Subject != null && obj.Subject.Trim().Length > 0)
            {
                term += " and subject=@subject ";
                if (ESP.Media.Access.Utilities.Common.Find(ht, "@subject") == -1)
                {
                    SqlParameter p = new SqlParameter("@subject", SqlDbType.NVarChar, 100);
                    p.Value = obj.Subject;
                    ht.Add(p);
                }
            }
            if (obj.Body != null && obj.Body.Trim().Length > 0)
            {
                term += " and body=@body ";
                if (ESP.Media.Access.Utilities.Common.Find(ht, "@body") == -1)
                {
                    SqlParameter p = new SqlParameter("@body", SqlDbType.NVarChar, 4000);
                    p.Value = obj.Body;
                    ht.Add(p);
                }
            }
            if (obj.Lastreplyuserid > 0)//最后回复id
            {
                term += " and lastreplyuserid=@lastreplyuserid ";
                if (ESP.Media.Access.Utilities.Common.Find(ht, "@lastreplyuserid") == -1)
                {
                    SqlParameter p = new SqlParameter("@lastreplyuserid", SqlDbType.Int, 4);
                    p.Value = obj.Lastreplyuserid;
                    ht.Add(p);
                }
            }
            if (obj.Lastreplydate != null && obj.Lastreplydate.Trim().Length > 0)
            {
                term += " and lastreplydate=@lastreplydate ";
                if (ESP.Media.Access.Utilities.Common.Find(ht, "@lastreplydate") == -1)
                {
                    SqlParameter p = new SqlParameter("@lastreplydate", SqlDbType.SmallDateTime, 4);
                    p.Value = ESP.Media.Access.Utilities.Common.StringToDateTime(obj.Lastreplydate);
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
            if (obj.Begindate != null && obj.Begindate.Trim().Length > 0)
            {
                term += " and begindate=@begindate ";
                if (ESP.Media.Access.Utilities.Common.Find(ht, "@begindate") == -1)
                {
                    SqlParameter p = new SqlParameter("@begindate", SqlDbType.DateTime, 8);
                    p.Value = ESP.Media.Access.Utilities.Common.StringToDateTime(obj.Begindate);
                    ht.Add(p);
                }
            }
            if (obj.Enddate != null && obj.Enddate.Trim().Length > 0)
            {
                term += " and enddate=@enddate ";
                if (ESP.Media.Access.Utilities.Common.Find(ht, "@enddate") == -1)
                {
                    SqlParameter p = new SqlParameter("@enddate", SqlDbType.DateTime, 8);
                    p.Value = ESP.Media.Access.Utilities.Common.StringToDateTime(obj.Enddate);
                    ht.Add(p);
                }
            }
            return term;
        }
        #endregion
        #region 查询
        private static string getQueryTerms(PostsInfo obj, ref Hashtable ht)
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
            if (obj.Parentid > 0)//上级id
            {
                term += " and a.parentid=@parentid ";
                if (!ht.ContainsKey("@parentid"))
                {
                    ht.Add("@parentid", obj.Parentid);
                }
            }
            if (obj.No > 0)//编号
            {
                term += " and a.no=@no ";
                if (!ht.ContainsKey("@no"))
                {
                    ht.Add("@no", obj.No);
                }
            }
            if (obj.Type > 0)//类型
            {
                term += " and a.type=@type ";
                if (!ht.ContainsKey("@type"))
                {
                    ht.Add("@type", obj.Type);
                }
            }
            if (obj.Issysmsg > 0)//是否是系统消息
            {
                term += " and a.issysmsg=@issysmsg ";
                if (!ht.ContainsKey("@issysmsg"))
                {
                    ht.Add("@issysmsg", obj.Issysmsg);
                }
            }
            if (obj.Issuedate != null && obj.Issuedate.Trim().Length > 0)
            {
                term += " and a.issuedate=@issuedate ";
                if (!ht.ContainsKey("@issuedate"))
                {
                    ht.Add("@issuedate", obj.Issuedate);
                }
            }
            if (obj.Userid > 0)//用户id
            {
                term += " and a.userid=@userid ";
                if (!ht.ContainsKey("@userid"))
                {
                    ht.Add("@userid", obj.Userid);
                }
            }
            if (obj.Subject != null && obj.Subject.Trim().Length > 0)
            {
                term += " and a.subject=@subject ";
                if (!ht.ContainsKey("@subject"))
                {
                    ht.Add("@subject", obj.Subject);
                }
            }
            if (obj.Body != null && obj.Body.Trim().Length > 0)
            {
                term += " and a.body=@body ";
                if (!ht.ContainsKey("@body"))
                {
                    ht.Add("@body", obj.Body);
                }
            }
            if (obj.Lastreplyuserid > 0)//最后回复id
            {
                term += " and a.lastreplyuserid=@lastreplyuserid ";
                if (!ht.ContainsKey("@lastreplyuserid"))
                {
                    ht.Add("@lastreplyuserid", obj.Lastreplyuserid);
                }
            }
            if (obj.Lastreplydate != null && obj.Lastreplydate.Trim().Length > 0)
            {
                term += " and a.lastreplydate=@lastreplydate ";
                if (!ht.ContainsKey("@lastreplydate"))
                {
                    ht.Add("@lastreplydate", obj.Lastreplydate);
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
            if (obj.Begindate != null && obj.Begindate.Trim().Length > 0)
            {
                term += " and a.begindate=@begindate ";
                if (!ht.ContainsKey("@begindate"))
                {
                    ht.Add("@begindate", obj.Begindate);
                }
            }
            if (obj.Enddate != null && obj.Enddate.Trim().Length > 0)
            {
                term += " and a.enddate=@enddate ";
                if (!ht.ContainsKey("@enddate"))
                {
                    ht.Add("@enddate", obj.Enddate);
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
            string sql = @"select {0} a.id as id,a.parentid as parentid,a.no as no,a.type as type,a.issysmsg as issysmsg,a.issuedate as issuedate,a.userid as userid,a.subject as subject,a.body as body,a.lastreplyuserid as lastreplyuserid,a.lastreplydate as lastreplydate,a.del as del,a.begindate as begindate,a.enddate as enddate {1} from media_posts as a {2} where 1=1 {3} ";
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


        public static DataTable QueryInfoByObj(PostsInfo obj, string terms, Hashtable ht)
        {
            if (ht == null)
            {
                ht = new Hashtable();
            }
            SqlParameter[] para = ESP.Media.Access.Utilities.Common.DictToSqlParam(ht);
            return QueryInfoByObj(obj, terms, para);
        }


        public static DataTable QueryInfoByObj(PostsInfo obj, string terms, params SqlParameter[] param)
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
        public static PostsInfo Load(int id)
        {
            DataTable dt = null;
            dt = QueryInfo(" and a.id=@id ", new SqlParameter("@id", id));
            if (dt != null && dt.Rows.Count > 0)
            {
                return setObject(dt.Rows[0]);
            }
            return null;
        }


        public static PostsInfo Load(int id, SqlTransaction trans)
        {
            DataTable dt = null;
            dt = QueryInfo(trans, " and a.id=@id ", new SqlParameter("@id", id));
            if (dt != null && dt.Rows.Count > 0)
            {
                return setObject(dt.Rows[0]);
            }
            return null;
        }


        public static PostsInfo setObject(DataRow dr)
        {
            PostsInfo obj = new PostsInfo();
            if (dr.Table.Columns.Contains("id") && dr["id"] != DBNull.Value)//id
            {
                obj.Id = Convert.ToInt32(dr["id"]);
            }
            if (dr.Table.Columns.Contains("parentid") && dr["parentid"] != DBNull.Value)//上级id
            {
                obj.Parentid = Convert.ToInt32(dr["parentid"]);
            }
            if (dr.Table.Columns.Contains("no") && dr["no"] != DBNull.Value)//编号
            {
                obj.No = Convert.ToInt32(dr["no"]);
            }
            if (dr.Table.Columns.Contains("type") && dr["type"] != DBNull.Value)//类型
            {
                obj.Type = Convert.ToInt32(dr["type"]);
            }
            if (dr.Table.Columns.Contains("issysmsg") && dr["issysmsg"] != DBNull.Value)//是否是系统消息
            {
                obj.Issysmsg = Convert.ToInt32(dr["issysmsg"]);
            }
            if (dr.Table.Columns.Contains("issuedate") && dr["issuedate"] != DBNull.Value)//发布时间
            {
                obj.Issuedate = (dr["issuedate"]).ToString();
            }
            if (dr.Table.Columns.Contains("userid") && dr["userid"] != DBNull.Value)//用户id
            {
                obj.Userid = Convert.ToInt32(dr["userid"]);
            }
            if (dr.Table.Columns.Contains("subject") && dr["subject"] != DBNull.Value)//主题
            {
                obj.Subject = (dr["subject"]).ToString();
            }
            if (dr.Table.Columns.Contains("body") && dr["body"] != DBNull.Value)//内容
            {
                obj.Body = (dr["body"]).ToString();
            }
            if (dr.Table.Columns.Contains("lastreplyuserid") && dr["lastreplyuserid"] != DBNull.Value)//最后回复id
            {
                obj.Lastreplyuserid = Convert.ToInt32(dr["lastreplyuserid"]);
            }
            if (dr.Table.Columns.Contains("lastreplydate") && dr["lastreplydate"] != DBNull.Value)//最后回复时间
            {
                obj.Lastreplydate = (dr["lastreplydate"]).ToString();
            }
            if (dr.Table.Columns.Contains("del") && dr["del"] != DBNull.Value)//删除标记
            {
                obj.Del = Convert.ToInt32(dr["del"]);
            }
            if (dr.Table.Columns.Contains("begindate") && dr["begindate"] != DBNull.Value)//begindate
            {
                obj.Begindate = (dr["begindate"]).ToString();
            }
            if (dr.Table.Columns.Contains("enddate") && dr["enddate"] != DBNull.Value)//enddate
            {
                obj.Enddate = (dr["enddate"]).ToString();
            }
            return obj;
        }
        #endregion
    }
}
