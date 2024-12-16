using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.Collections.Generic;
using ESP.Media.Access.Utilities;
using ESP.Media.Entity;
namespace ESP.Media.DataAccess
{
    public class PersonfeebillDataProvider
    {
        #region 构造函数
        public PersonfeebillDataProvider()
        {
        }
        #endregion
        #region 插入
        //插入字符串
        private static string strinsert(PersonfeebillInfo obj, ref List<SqlParameter> ht)
        {
            if (ht == null)
            {
                ht = new List<SqlParameter>();
            }
            string sql = @"insert into media_personfeebill (createip,createdate,userid,username,userdepartmentid,userdepartmentname,describe,del,projectid,projectcode,userextensioncode,reimbursedate,status) values (@createip,@createdate,@userid,@username,@userdepartmentid,@userdepartmentname,@describe,@del,@projectid,@projectcode,@userextensioncode,@reimbursedate,@status);select @@IDENTITY as rowNum;";
            SqlParameter param_Createip = new SqlParameter("@Createip", SqlDbType.NVarChar, 40);
            param_Createip.Value = obj.Createip;
            ht.Add(param_Createip);
            SqlParameter param_Createdate = new SqlParameter("@Createdate", SqlDbType.SmallDateTime, 4);
            param_Createdate.Value = ESP.Media.Access.Utilities.Common.StringToDateTime(obj.Createdate);
            ht.Add(param_Createdate);
            SqlParameter param_Userid = new SqlParameter("@Userid", SqlDbType.Int, 4);
            param_Userid.Value = obj.Userid;
            ht.Add(param_Userid);
            SqlParameter param_Username = new SqlParameter("@Username", SqlDbType.NVarChar, 100);
            param_Username.Value = obj.Username;
            ht.Add(param_Username);
            SqlParameter param_Userdepartmentid = new SqlParameter("@Userdepartmentid", SqlDbType.Int, 4);
            param_Userdepartmentid.Value = obj.Userdepartmentid;
            ht.Add(param_Userdepartmentid);
            SqlParameter param_Userdepartmentname = new SqlParameter("@Userdepartmentname", SqlDbType.NVarChar, 100);
            param_Userdepartmentname.Value = obj.Userdepartmentname;
            ht.Add(param_Userdepartmentname);
            SqlParameter param_Describe = new SqlParameter("@Describe", SqlDbType.NVarChar, 1000);
            param_Describe.Value = obj.Describe;
            ht.Add(param_Describe);
            SqlParameter param_Del = new SqlParameter("@Del", SqlDbType.Int, 4);
            param_Del.Value = obj.Del;
            ht.Add(param_Del);
            SqlParameter param_Projectid = new SqlParameter("@Projectid", SqlDbType.Int, 4);
            param_Projectid.Value = obj.Projectid;
            ht.Add(param_Projectid);
            SqlParameter param_Projectcode = new SqlParameter("@Projectcode", SqlDbType.NVarChar, 100);
            param_Projectcode.Value = obj.Projectcode;
            ht.Add(param_Projectcode);
            SqlParameter param_Userextensioncode = new SqlParameter("@Userextensioncode", SqlDbType.NVarChar, 100);
            param_Userextensioncode.Value = obj.Userextensioncode;
            ht.Add(param_Userextensioncode);
            SqlParameter param_Reimbursedate = new SqlParameter("@Reimbursedate", SqlDbType.DateTime, 8);
            param_Reimbursedate.Value = ESP.Media.Access.Utilities.Common.StringToDateTime(obj.Reimbursedate);
            ht.Add(param_Reimbursedate);
            SqlParameter param_Status = new SqlParameter("@Status", SqlDbType.Int, 4);
            param_Status.Value = obj.Status;
            ht.Add(param_Status);
            return sql;
        }


        //插入一条记录
        public static int insertinfo(PersonfeebillInfo obj, SqlTransaction trans)
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
        public static int insertinfo(PersonfeebillInfo obj)
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
            string sql = "delete media_personfeebill where personfeebillid=@id";
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
        public static string getUpdateString(PersonfeebillInfo objTerm, PersonfeebillInfo Objupdate, string term, ref List<SqlParameter> ht, params SqlParameter[] param)
        {
            string sql = "update media_personfeebill set createip=@createip,createdate=@createdate,userid=@userid,username=@username,userdepartmentid=@userdepartmentid,userdepartmentname=@userdepartmentname,describe=@describe,del=@del,projectid=@projectid,projectcode=@projectcode,userextensioncode=@userextensioncode,reimbursedate=@reimbursedate,status=@status where 1=1 ";
            SqlParameter param_createip = new SqlParameter("@createip", SqlDbType.NVarChar, 40);
            param_createip.Value = Objupdate.Createip;
            ht.Add(param_createip);
            SqlParameter param_createdate = new SqlParameter("@createdate", SqlDbType.SmallDateTime, 4);
            param_createdate.Value = ESP.Media.Access.Utilities.Common.StringToDateTime(Objupdate.Createdate);
            ht.Add(param_createdate);
            SqlParameter param_userid = new SqlParameter("@userid", SqlDbType.Int, 4);
            param_userid.Value = Objupdate.Userid;
            ht.Add(param_userid);
            SqlParameter param_username = new SqlParameter("@username", SqlDbType.NVarChar, 100);
            param_username.Value = Objupdate.Username;
            ht.Add(param_username);
            SqlParameter param_userdepartmentid = new SqlParameter("@userdepartmentid", SqlDbType.Int, 4);
            param_userdepartmentid.Value = Objupdate.Userdepartmentid;
            ht.Add(param_userdepartmentid);
            SqlParameter param_userdepartmentname = new SqlParameter("@userdepartmentname", SqlDbType.NVarChar, 100);
            param_userdepartmentname.Value = Objupdate.Userdepartmentname;
            ht.Add(param_userdepartmentname);
            SqlParameter param_describe = new SqlParameter("@describe", SqlDbType.NVarChar, 1000);
            param_describe.Value = Objupdate.Describe;
            ht.Add(param_describe);
            SqlParameter param_del = new SqlParameter("@del", SqlDbType.Int, 4);
            param_del.Value = Objupdate.Del;
            ht.Add(param_del);
            SqlParameter param_projectid = new SqlParameter("@projectid", SqlDbType.Int, 4);
            param_projectid.Value = Objupdate.Projectid;
            ht.Add(param_projectid);
            SqlParameter param_projectcode = new SqlParameter("@projectcode", SqlDbType.NVarChar, 100);
            param_projectcode.Value = Objupdate.Projectcode;
            ht.Add(param_projectcode);
            SqlParameter param_userextensioncode = new SqlParameter("@userextensioncode", SqlDbType.NVarChar, 100);
            param_userextensioncode.Value = Objupdate.Userextensioncode;
            ht.Add(param_userextensioncode);
            SqlParameter param_reimbursedate = new SqlParameter("@reimbursedate", SqlDbType.DateTime, 8);
            param_reimbursedate.Value = ESP.Media.Access.Utilities.Common.StringToDateTime(Objupdate.Reimbursedate);
            ht.Add(param_reimbursedate);
            SqlParameter param_status = new SqlParameter("@status", SqlDbType.Int, 4);
            param_status.Value = Objupdate.Status;
            ht.Add(param_status);
            if (objTerm == null && (term == null || term.Trim().Length <= 0))
            {
                sql += " and personfeebillid=@id ";
                if (ESP.Media.Access.Utilities.Common.Find(ht, "@id") == -1)
                {
                    SqlParameter p = new SqlParameter("@id", SqlDbType.Int, 4);
                    p.Value = Objupdate.Personfeebillid;
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
        public static bool updateInfo(SqlTransaction trans, PersonfeebillInfo objterm, PersonfeebillInfo Objupdate, string term, params SqlParameter[] param)
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
        public static bool updateInfo(PersonfeebillInfo objterm, PersonfeebillInfo Objupdate, string term, params SqlParameter[] param)
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


        private static string getTerms(PersonfeebillInfo obj, ref List<SqlParameter> ht)
        {
            string term = string.Empty;
            if (obj.Personfeebillid > 0)//personfeebillid
            {
                term += " and personfeebillid=@personfeebillid ";
                if (ESP.Media.Access.Utilities.Common.Find(ht, "@personfeebillid") == -1)
                {
                    SqlParameter p = new SqlParameter("@personfeebillid", SqlDbType.Int, 4);
                    p.Value = obj.Personfeebillid;
                    ht.Add(p);
                }
            }
            if (obj.Createip != null && obj.Createip.Trim().Length > 0)
            {
                term += " and createip=@createip ";
                if (ESP.Media.Access.Utilities.Common.Find(ht, "@createip") == -1)
                {
                    SqlParameter p = new SqlParameter("@createip", SqlDbType.NVarChar, 40);
                    p.Value = obj.Createip;
                    ht.Add(p);
                }
            }
            if (obj.Createdate != null && obj.Createdate.Trim().Length > 0)
            {
                term += " and createdate=@createdate ";
                if (ESP.Media.Access.Utilities.Common.Find(ht, "@createdate") == -1)
                {
                    SqlParameter p = new SqlParameter("@createdate", SqlDbType.SmallDateTime, 4);
                    p.Value = ESP.Media.Access.Utilities.Common.StringToDateTime(obj.Createdate);
                    ht.Add(p);
                }
            }
            if (obj.Userid > 0)//userid
            {
                term += " and userid=@userid ";
                if (ESP.Media.Access.Utilities.Common.Find(ht, "@userid") == -1)
                {
                    SqlParameter p = new SqlParameter("@userid", SqlDbType.Int, 4);
                    p.Value = obj.Userid;
                    ht.Add(p);
                }
            }
            if (obj.Username != null && obj.Username.Trim().Length > 0)
            {
                term += " and username=@username ";
                if (ESP.Media.Access.Utilities.Common.Find(ht, "@username") == -1)
                {
                    SqlParameter p = new SqlParameter("@username", SqlDbType.NVarChar, 100);
                    p.Value = obj.Username;
                    ht.Add(p);
                }
            }
            if (obj.Userdepartmentid > 0)//userdepartmentid
            {
                term += " and userdepartmentid=@userdepartmentid ";
                if (ESP.Media.Access.Utilities.Common.Find(ht, "@userdepartmentid") == -1)
                {
                    SqlParameter p = new SqlParameter("@userdepartmentid", SqlDbType.Int, 4);
                    p.Value = obj.Userdepartmentid;
                    ht.Add(p);
                }
            }
            if (obj.Userdepartmentname != null && obj.Userdepartmentname.Trim().Length > 0)
            {
                term += " and userdepartmentname=@userdepartmentname ";
                if (ESP.Media.Access.Utilities.Common.Find(ht, "@userdepartmentname") == -1)
                {
                    SqlParameter p = new SqlParameter("@userdepartmentname", SqlDbType.NVarChar, 100);
                    p.Value = obj.Userdepartmentname;
                    ht.Add(p);
                }
            }
            if (obj.Describe != null && obj.Describe.Trim().Length > 0)
            {
                term += " and describe=@describe ";
                if (ESP.Media.Access.Utilities.Common.Find(ht, "@describe") == -1)
                {
                    SqlParameter p = new SqlParameter("@describe", SqlDbType.NVarChar, 1000);
                    p.Value = obj.Describe;
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
            if (obj.Projectcode != null && obj.Projectcode.Trim().Length > 0)
            {
                term += " and projectcode=@projectcode ";
                if (ESP.Media.Access.Utilities.Common.Find(ht, "@projectcode") == -1)
                {
                    SqlParameter p = new SqlParameter("@projectcode", SqlDbType.NVarChar, 100);
                    p.Value = obj.Projectcode;
                    ht.Add(p);
                }
            }
            if (obj.Userextensioncode != null && obj.Userextensioncode.Trim().Length > 0)
            {
                term += " and userextensioncode=@userextensioncode ";
                if (ESP.Media.Access.Utilities.Common.Find(ht, "@userextensioncode") == -1)
                {
                    SqlParameter p = new SqlParameter("@userextensioncode", SqlDbType.NVarChar, 100);
                    p.Value = obj.Userextensioncode;
                    ht.Add(p);
                }
            }
            if (obj.Reimbursedate != null && obj.Reimbursedate.Trim().Length > 0)
            {
                term += " and reimbursedate=@reimbursedate ";
                if (ESP.Media.Access.Utilities.Common.Find(ht, "@reimbursedate") == -1)
                {
                    SqlParameter p = new SqlParameter("@reimbursedate", SqlDbType.DateTime, 8);
                    p.Value = ESP.Media.Access.Utilities.Common.StringToDateTime(obj.Reimbursedate);
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
        private static string getQueryTerms(PersonfeebillInfo obj, ref Hashtable ht)
        {
            string term = string.Empty;
            if (obj.Personfeebillid > 0)//personfeebillid
            {
                term += " and a.personfeebillid=@personfeebillid ";
                if (!ht.ContainsKey("@personfeebillid"))
                {
                    ht.Add("@personfeebillid", obj.Personfeebillid);
                }
            }
            if (obj.Createip != null && obj.Createip.Trim().Length > 0)
            {
                term += " and a.createip=@createip ";
                if (!ht.ContainsKey("@createip"))
                {
                    ht.Add("@createip", obj.Createip);
                }
            }
            if (obj.Createdate != null && obj.Createdate.Trim().Length > 0)
            {
                term += " and a.createdate=@createdate ";
                if (!ht.ContainsKey("@createdate"))
                {
                    ht.Add("@createdate", obj.Createdate);
                }
            }
            if (obj.Userid > 0)//userid
            {
                term += " and a.userid=@userid ";
                if (!ht.ContainsKey("@userid"))
                {
                    ht.Add("@userid", obj.Userid);
                }
            }
            if (obj.Username != null && obj.Username.Trim().Length > 0)
            {
                term += " and a.username=@username ";
                if (!ht.ContainsKey("@username"))
                {
                    ht.Add("@username", obj.Username);
                }
            }
            if (obj.Userdepartmentid > 0)//userdepartmentid
            {
                term += " and a.userdepartmentid=@userdepartmentid ";
                if (!ht.ContainsKey("@userdepartmentid"))
                {
                    ht.Add("@userdepartmentid", obj.Userdepartmentid);
                }
            }
            if (obj.Userdepartmentname != null && obj.Userdepartmentname.Trim().Length > 0)
            {
                term += " and a.userdepartmentname=@userdepartmentname ";
                if (!ht.ContainsKey("@userdepartmentname"))
                {
                    ht.Add("@userdepartmentname", obj.Userdepartmentname);
                }
            }
            if (obj.Describe != null && obj.Describe.Trim().Length > 0)
            {
                term += " and a.describe=@describe ";
                if (!ht.ContainsKey("@describe"))
                {
                    ht.Add("@describe", obj.Describe);
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
            if (obj.Projectcode != null && obj.Projectcode.Trim().Length > 0)
            {
                term += " and a.projectcode=@projectcode ";
                if (!ht.ContainsKey("@projectcode"))
                {
                    ht.Add("@projectcode", obj.Projectcode);
                }
            }
            if (obj.Userextensioncode != null && obj.Userextensioncode.Trim().Length > 0)
            {
                term += " and a.userextensioncode=@userextensioncode ";
                if (!ht.ContainsKey("@userextensioncode"))
                {
                    ht.Add("@userextensioncode", obj.Userextensioncode);
                }
            }
            if (obj.Reimbursedate != null && obj.Reimbursedate.Trim().Length > 0)
            {
                term += " and a.reimbursedate=@reimbursedate ";
                if (!ht.ContainsKey("@reimbursedate"))
                {
                    ht.Add("@reimbursedate", obj.Reimbursedate);
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
            string sql = @"select {0} a.personfeebillid as personfeebillid,a.createip as createip,a.createdate as createdate,a.userid as userid,a.username as username,a.userdepartmentid as userdepartmentid,a.userdepartmentname as userdepartmentname,a.describe as describe,a.del as del,a.projectid as projectid,a.projectcode as projectcode,a.userextensioncode as userextensioncode,a.reimbursedate as reimbursedate,a.status as status {1} from media_personfeebill as a {2} where 1=1 {3} ";
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


        public static DataTable QueryInfoByObj(PersonfeebillInfo obj, string terms, Hashtable ht)
        {
            if (ht == null)
            {
                ht = new Hashtable();
            }
            SqlParameter[] para = ESP.Media.Access.Utilities.Common.DictToSqlParam(ht);
            return QueryInfoByObj(obj, terms, para);
        }


        public static DataTable QueryInfoByObj(PersonfeebillInfo obj, string terms, params SqlParameter[] param)
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
        public static PersonfeebillInfo Load(int id)
        {
            DataTable dt = null;
            dt = QueryInfo(" and a.personfeebillid=@id ", new SqlParameter("@id", id));
            if (dt != null && dt.Rows.Count > 0)
            {
                return setObject(dt.Rows[0]);
            }
            return null;
        }


        public static PersonfeebillInfo Load(int id, SqlTransaction trans)
        {
            DataTable dt = null;
            dt = QueryInfo(trans, " and a.personfeebillid=@id ", new SqlParameter("@id", id));
            if (dt != null && dt.Rows.Count > 0)
            {
                return setObject(dt.Rows[0]);
            }
            return null;
        }


        public static PersonfeebillInfo setObject(DataRow dr)
        {
            PersonfeebillInfo obj = new PersonfeebillInfo();
            if (dr.Table.Columns.Contains("personfeebillid") && dr["personfeebillid"] != DBNull.Value)//personfeebillid
            {
                obj.Personfeebillid = Convert.ToInt32(dr["personfeebillid"]);
            }
            if (dr.Table.Columns.Contains("createip") && dr["createip"] != DBNull.Value)//createip
            {
                obj.Createip = (dr["createip"]).ToString();
            }
            if (dr.Table.Columns.Contains("createdate") && dr["createdate"] != DBNull.Value)//createdate
            {
                obj.Createdate = (dr["createdate"]).ToString();
            }
            if (dr.Table.Columns.Contains("userid") && dr["userid"] != DBNull.Value)//userid
            {
                obj.Userid = Convert.ToInt32(dr["userid"]);
            }
            if (dr.Table.Columns.Contains("username") && dr["username"] != DBNull.Value)//username
            {
                obj.Username = (dr["username"]).ToString();
            }
            if (dr.Table.Columns.Contains("userdepartmentid") && dr["userdepartmentid"] != DBNull.Value)//userdepartmentid
            {
                obj.Userdepartmentid = Convert.ToInt32(dr["userdepartmentid"]);
            }
            if (dr.Table.Columns.Contains("userdepartmentname") && dr["userdepartmentname"] != DBNull.Value)//userdepartmentname
            {
                obj.Userdepartmentname = (dr["userdepartmentname"]).ToString();
            }
            if (dr.Table.Columns.Contains("describe") && dr["describe"] != DBNull.Value)//describe
            {
                obj.Describe = (dr["describe"]).ToString();
            }
            if (dr.Table.Columns.Contains("del") && dr["del"] != DBNull.Value)//del
            {
                obj.Del = Convert.ToInt32(dr["del"]);
            }
            if (dr.Table.Columns.Contains("projectid") && dr["projectid"] != DBNull.Value)//projectid
            {
                obj.Projectid = Convert.ToInt32(dr["projectid"]);
            }
            if (dr.Table.Columns.Contains("projectcode") && dr["projectcode"] != DBNull.Value)//projectcode
            {
                obj.Projectcode = (dr["projectcode"]).ToString();
            }
            if (dr.Table.Columns.Contains("userextensioncode") && dr["userextensioncode"] != DBNull.Value)//userextensioncode
            {
                obj.Userextensioncode = (dr["userextensioncode"]).ToString();
            }
            if (dr.Table.Columns.Contains("reimbursedate") && dr["reimbursedate"] != DBNull.Value)//reimbursedate
            {
                obj.Reimbursedate = (dr["reimbursedate"]).ToString();
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
