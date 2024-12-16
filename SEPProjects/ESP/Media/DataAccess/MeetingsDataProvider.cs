using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.Collections.Generic;
using ESP.Media.Entity;
using ESP.Media.Access.Utilities;
namespace ESP.Media.DataAccess
{
    public class MeetingsDataProvider
    {
        #region 构造函数
        public MeetingsDataProvider()
        {
        }
        #endregion
        #region 插入
        //插入字符串
        private static string strinsert(MeetingsInfo obj, ref List<SqlParameter> ht)
        {
            if (ht == null)
            {
                ht = new List<SqlParameter>();
            }
            string sql = @"insert into media_meetings (cycle,createuserid,createdate,path,subject,remark) values (@cycle,@createuserid,@createdate,@path,@subject,@remark);select @@IDENTITY as rowNum;";
            SqlParameter param_Cycle = new SqlParameter("@Cycle", SqlDbType.Int, 4);
            param_Cycle.Value = obj.Cycle;
            ht.Add(param_Cycle);
            SqlParameter param_Createuserid = new SqlParameter("@Createuserid", SqlDbType.Int, 4);
            param_Createuserid.Value = obj.Createuserid;
            ht.Add(param_Createuserid);
            SqlParameter param_Createdate = new SqlParameter("@Createdate", SqlDbType.DateTime, 8);
            param_Createdate.Value = ESP.Media.Access.Utilities.Common.StringToDateTime(obj.Createdate);
            ht.Add(param_Createdate);
            SqlParameter param_Path = new SqlParameter("@Path", SqlDbType.NVarChar, 400);
            param_Path.Value = obj.Path;
            ht.Add(param_Path);
            SqlParameter param_Subject = new SqlParameter("@Subject", SqlDbType.NVarChar, 400);
            param_Subject.Value = obj.Subject;
            ht.Add(param_Subject);
            SqlParameter param_Remark = new SqlParameter("@Remark", SqlDbType.NVarChar, 1000);
            param_Remark.Value = obj.Remark;
            ht.Add(param_Remark);
            return sql;
        }


        //插入一条记录
        public static int insertinfo(MeetingsInfo obj, SqlTransaction trans)
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
        public static int insertinfo(MeetingsInfo obj)
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
            string sql = "delete media_meetings where id=@id";
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
        public static string getUpdateString(MeetingsInfo objTerm, MeetingsInfo Objupdate, string term, ref List<SqlParameter> ht, params SqlParameter[] param)
        {
            string sql = "update media_meetings set cycle=@cycle,createuserid=@createuserid,createdate=@createdate,path=@path,subject=@subject,remark=@remark where 1=1 ";
            SqlParameter param_cycle = new SqlParameter("@cycle", SqlDbType.Int, 4);
            param_cycle.Value = Objupdate.Cycle;
            ht.Add(param_cycle);
            SqlParameter param_createuserid = new SqlParameter("@createuserid", SqlDbType.Int, 4);
            param_createuserid.Value = Objupdate.Createuserid;
            ht.Add(param_createuserid);
            SqlParameter param_createdate = new SqlParameter("@createdate", SqlDbType.DateTime, 8);
            param_createdate.Value = ESP.Media.Access.Utilities.Common.StringToDateTime(Objupdate.Createdate);
            ht.Add(param_createdate);
            SqlParameter param_path = new SqlParameter("@path", SqlDbType.NVarChar, 400);
            param_path.Value = Objupdate.Path;
            ht.Add(param_path);
            SqlParameter param_subject = new SqlParameter("@subject", SqlDbType.NVarChar, 400);
            param_subject.Value = Objupdate.Subject;
            ht.Add(param_subject);
            SqlParameter param_remark = new SqlParameter("@remark", SqlDbType.NVarChar, 1000);
            param_remark.Value = Objupdate.Remark;
            ht.Add(param_remark);
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
        public static bool updateInfo(SqlTransaction trans, MeetingsInfo objterm, MeetingsInfo Objupdate, string term, params SqlParameter[] param)
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
        public static bool updateInfo(MeetingsInfo objterm, MeetingsInfo Objupdate, string term, params SqlParameter[] param)
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


        private static string getTerms(MeetingsInfo obj, ref List<SqlParameter> ht)
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
            if (obj.Cycle > 0)//cycle
            {
                term += " and cycle=@cycle ";
                if (ESP.Media.Access.Utilities.Common.Find(ht, "@cycle") == -1)
                {
                    SqlParameter p = new SqlParameter("@cycle", SqlDbType.Int, 4);
                    p.Value = obj.Cycle;
                    ht.Add(p);
                }
            }
            if (obj.Createuserid > 0)//createuserid
            {
                term += " and createuserid=@createuserid ";
                if (ESP.Media.Access.Utilities.Common.Find(ht, "@createuserid") == -1)
                {
                    SqlParameter p = new SqlParameter("@createuserid", SqlDbType.Int, 4);
                    p.Value = obj.Createuserid;
                    ht.Add(p);
                }
            }
            if (obj.Createdate != null && obj.Createdate.Trim().Length > 0)
            {
                term += " and createdate=@createdate ";
                if (ESP.Media.Access.Utilities.Common.Find(ht, "@createdate") == -1)
                {
                    SqlParameter p = new SqlParameter("@createdate", SqlDbType.DateTime, 8);
                    p.Value = ESP.Media.Access.Utilities.Common.StringToDateTime(obj.Createdate);
                    ht.Add(p);
                }
            }
            if (obj.Path != null && obj.Path.Trim().Length > 0)
            {
                term += " and path=@path ";
                if (ESP.Media.Access.Utilities.Common.Find(ht, "@path") == -1)
                {
                    SqlParameter p = new SqlParameter("@path", SqlDbType.NVarChar, 400);
                    p.Value = obj.Path;
                    ht.Add(p);
                }
            }
            if (obj.Subject != null && obj.Subject.Trim().Length > 0)
            {
                term += " and subject=@subject ";
                if (ESP.Media.Access.Utilities.Common.Find(ht, "@subject") == -1)
                {
                    SqlParameter p = new SqlParameter("@subject", SqlDbType.NVarChar, 400);
                    p.Value = obj.Subject;
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
            return term;
        }
        #endregion
        #region 查询
        private static string getQueryTerms(MeetingsInfo obj, ref Hashtable ht)
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
            if (obj.Cycle > 0)//cycle
            {
                term += " and a.cycle=@cycle ";
                if (!ht.ContainsKey("@cycle"))
                {
                    ht.Add("@cycle", obj.Cycle);
                }
            }
            if (obj.Createuserid > 0)//createuserid
            {
                term += " and a.createuserid=@createuserid ";
                if (!ht.ContainsKey("@createuserid"))
                {
                    ht.Add("@createuserid", obj.Createuserid);
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
            if (obj.Path != null && obj.Path.Trim().Length > 0)
            {
                term += " and a.path=@path ";
                if (!ht.ContainsKey("@path"))
                {
                    ht.Add("@path", obj.Path);
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
            if (obj.Remark != null && obj.Remark.Trim().Length > 0)
            {
                term += " and a.remark=@remark ";
                if (!ht.ContainsKey("@remark"))
                {
                    ht.Add("@remark", obj.Remark);
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
            string sql = @"select {0} a.id as id,a.cycle as cycle,a.createuserid as createuserid,a.createdate as createdate,a.path as path,a.subject as subject,a.remark as remark {1} from media_meetings as a {2} where 1=1 {3} ";
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


        public static DataTable QueryInfoByObj(MeetingsInfo obj, string terms, Hashtable ht)
        {
            if (ht == null)
            {
                ht = new Hashtable();
            }
            SqlParameter[] para = ESP.Media.Access.Utilities.Common.DictToSqlParam(ht);
            return QueryInfoByObj(obj, terms, para);
        }


        public static DataTable QueryInfoByObj(MeetingsInfo obj, string terms, params SqlParameter[] param)
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
        public static MeetingsInfo Load(int id)
        {
            DataTable dt = null;
            dt = QueryInfo(" and a.id=@id ", new SqlParameter("@id", id));
            if (dt != null && dt.Rows.Count > 0)
            {
                return setObject(dt.Rows[0]);
            }
            return null;
        }


        public static MeetingsInfo Load(int id, SqlTransaction trans)
        {
            DataTable dt = null;
            dt = QueryInfo(trans, " and a.id=@id ", new SqlParameter("@id", id));
            if (dt != null && dt.Rows.Count > 0)
            {
                return setObject(dt.Rows[0]);
            }
            return null;
        }


        public static MeetingsInfo setObject(DataRow dr)
        {
            MeetingsInfo obj = new MeetingsInfo();
            if (dr.Table.Columns.Contains("id") && dr["id"] != DBNull.Value)//id
            {
                obj.Id = Convert.ToInt32(dr["id"]);
            }
            if (dr.Table.Columns.Contains("cycle") && dr["cycle"] != DBNull.Value)//cycle
            {
                obj.Cycle = Convert.ToInt32(dr["cycle"]);
            }
            if (dr.Table.Columns.Contains("createuserid") && dr["createuserid"] != DBNull.Value)//createuserid
            {
                obj.Createuserid = Convert.ToInt32(dr["createuserid"]);
            }
            if (dr.Table.Columns.Contains("createdate") && dr["createdate"] != DBNull.Value)//createdate
            {
                obj.Createdate = (dr["createdate"]).ToString();
            }
            if (dr.Table.Columns.Contains("path") && dr["path"] != DBNull.Value)//path
            {
                obj.Path = (dr["path"]).ToString();
            }
            if (dr.Table.Columns.Contains("subject") && dr["subject"] != DBNull.Value)//subject
            {
                obj.Subject = (dr["subject"]).ToString();
            }
            if (dr.Table.Columns.Contains("remark") && dr["remark"] != DBNull.Value)//remark
            {
                obj.Remark = (dr["remark"]).ToString();
            }
            return obj;
        }
        #endregion
    }
}
