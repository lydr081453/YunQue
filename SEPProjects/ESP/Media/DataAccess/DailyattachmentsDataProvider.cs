using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using ESP.Media.Entity;
using System.Collections.Generic;
using ESP.Media.Access.Utilities;
namespace ESP.Media.DataAccess
{
    public class DailyattachmentsDataProvider
    {
        #region 构造函数
        public DailyattachmentsDataProvider()
        {
        }
        #endregion
        #region 插入
        //插入字符串
        private static string strinsert(DailyattachmentsInfo obj, ref List<SqlParameter> ht)
        {
            if (ht == null)
            {
                ht = new List<SqlParameter>();
            }
            string sql = @"insert into media_dailyattachments (version,attachmentid,type,createdbyuserid,createdip,createddate,attachmentpath,del,dailyid) values (@version,@attachmentid,@type,@createdbyuserid,@createdip,@createddate,@attachmentpath,@del,@dailyid);select @@IDENTITY as rowNum;";
            SqlParameter param_Version = new SqlParameter("@Version", SqlDbType.Int, 4);
            param_Version.Value = obj.Version;
            ht.Add(param_Version);
            SqlParameter param_Attachmentid = new SqlParameter("@Attachmentid", SqlDbType.Int, 4);
            param_Attachmentid.Value = obj.Attachmentid;
            ht.Add(param_Attachmentid);
            SqlParameter param_Type = new SqlParameter("@Type", SqlDbType.Int, 4);
            param_Type.Value = obj.Type;
            ht.Add(param_Type);
            SqlParameter param_Createdbyuserid = new SqlParameter("@Createdbyuserid", SqlDbType.Int, 4);
            param_Createdbyuserid.Value = obj.Createdbyuserid;
            ht.Add(param_Createdbyuserid);
            SqlParameter param_Createdip = new SqlParameter("@Createdip", SqlDbType.NVarChar, 512);
            param_Createdip.Value = obj.Createdip;
            ht.Add(param_Createdip);
            SqlParameter param_Createddate = new SqlParameter("@Createddate", SqlDbType.DateTime, 8);
            param_Createddate.Value = ESP.Media.Access.Utilities.Common.StringToDateTime(obj.Createddate);
            ht.Add(param_Createddate);
            SqlParameter param_Attachmentpath = new SqlParameter("@Attachmentpath", SqlDbType.NVarChar, 1000);
            param_Attachmentpath.Value = obj.Attachmentpath;
            ht.Add(param_Attachmentpath);
            SqlParameter param_Del = new SqlParameter("@Del", SqlDbType.SmallInt, 2);
            param_Del.Value = obj.Del;
            ht.Add(param_Del);
            SqlParameter param_Dailyid = new SqlParameter("@Dailyid", SqlDbType.Int, 4);
            param_Dailyid.Value = obj.Dailyid;
            ht.Add(param_Dailyid);
            return sql;
        }


        //插入一条记录
        public static int insertinfo(DailyattachmentsInfo obj, SqlTransaction trans)
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
        public static int insertinfo(DailyattachmentsInfo obj)
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
            string sql = "delete media_dailyattachments where id=@id";
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
        public static string getUpdateString(DailyattachmentsInfo objTerm, DailyattachmentsInfo Objupdate, string term, ref List<SqlParameter> ht, params SqlParameter[] param)
        {
            string sql = "update media_dailyattachments set version=@version,attachmentid=@attachmentid,type=@type,createdbyuserid=@createdbyuserid,createdip=@createdip,createddate=@createddate,attachmentpath=@attachmentpath,del=@del,dailyid=@dailyid where 1=1 ";
            SqlParameter param_version = new SqlParameter("@version", SqlDbType.Int, 4);
            param_version.Value = Objupdate.Version;
            ht.Add(param_version);
            SqlParameter param_attachmentid = new SqlParameter("@attachmentid", SqlDbType.Int, 4);
            param_attachmentid.Value = Objupdate.Attachmentid;
            ht.Add(param_attachmentid);
            SqlParameter param_type = new SqlParameter("@type", SqlDbType.Int, 4);
            param_type.Value = Objupdate.Type;
            ht.Add(param_type);
            SqlParameter param_createdbyuserid = new SqlParameter("@createdbyuserid", SqlDbType.Int, 4);
            param_createdbyuserid.Value = Objupdate.Createdbyuserid;
            ht.Add(param_createdbyuserid);
            SqlParameter param_createdip = new SqlParameter("@createdip", SqlDbType.NVarChar, 512);
            param_createdip.Value = Objupdate.Createdip;
            ht.Add(param_createdip);
            SqlParameter param_createddate = new SqlParameter("@createddate", SqlDbType.DateTime, 8);
            param_createddate.Value = ESP.Media.Access.Utilities.Common.StringToDateTime(Objupdate.Createddate);
            ht.Add(param_createddate);
            SqlParameter param_attachmentpath = new SqlParameter("@attachmentpath", SqlDbType.NVarChar, 1000);
            param_attachmentpath.Value = Objupdate.Attachmentpath;
            ht.Add(param_attachmentpath);
            SqlParameter param_del = new SqlParameter("@del", SqlDbType.SmallInt, 2);
            param_del.Value = Objupdate.Del;
            ht.Add(param_del);
            SqlParameter param_dailyid = new SqlParameter("@dailyid", SqlDbType.Int, 4);
            param_dailyid.Value = Objupdate.Dailyid;
            ht.Add(param_dailyid);
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
        public static bool updateInfo(SqlTransaction trans, DailyattachmentsInfo objterm, DailyattachmentsInfo Objupdate, string term, params SqlParameter[] param)
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
        public static bool updateInfo(DailyattachmentsInfo objterm, DailyattachmentsInfo Objupdate, string term, params SqlParameter[] param)
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


        private static string getTerms(DailyattachmentsInfo obj, ref List<SqlParameter> ht)
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
            if (obj.Version > 0)//Version
            {
                term += " and version=@version ";
                if (ESP.Media.Access.Utilities.Common.Find(ht, "@version") == -1)
                {
                    SqlParameter p = new SqlParameter("@version", SqlDbType.Int, 4);
                    p.Value = obj.Version;
                    ht.Add(p);
                }
            }
            if (obj.Attachmentid > 0)//AttachmentID
            {
                term += " and attachmentid=@attachmentid ";
                if (ESP.Media.Access.Utilities.Common.Find(ht, "@attachmentid") == -1)
                {
                    SqlParameter p = new SqlParameter("@attachmentid", SqlDbType.Int, 4);
                    p.Value = obj.Attachmentid;
                    ht.Add(p);
                }
            }
            if (obj.Type > 0)//Type
            {
                term += " and type=@type ";
                if (ESP.Media.Access.Utilities.Common.Find(ht, "@type") == -1)
                {
                    SqlParameter p = new SqlParameter("@type", SqlDbType.Int, 4);
                    p.Value = obj.Type;
                    ht.Add(p);
                }
            }
            if (obj.Createdbyuserid > 0)//CreatedByUserID
            {
                term += " and createdbyuserid=@createdbyuserid ";
                if (ESP.Media.Access.Utilities.Common.Find(ht, "@createdbyuserid") == -1)
                {
                    SqlParameter p = new SqlParameter("@createdbyuserid", SqlDbType.Int, 4);
                    p.Value = obj.Createdbyuserid;
                    ht.Add(p);
                }
            }
            if (obj.Createdip != null && obj.Createdip.Trim().Length > 0)
            {
                term += " and createdip=@createdip ";
                if (ESP.Media.Access.Utilities.Common.Find(ht, "@createdip") == -1)
                {
                    SqlParameter p = new SqlParameter("@createdip", SqlDbType.NVarChar, 512);
                    p.Value = obj.Createdip;
                    ht.Add(p);
                }
            }
            if (obj.Createddate != null && obj.Createddate.Trim().Length > 0)
            {
                term += " and createddate=@createddate ";
                if (ESP.Media.Access.Utilities.Common.Find(ht, "@createddate") == -1)
                {
                    SqlParameter p = new SqlParameter("@createddate", SqlDbType.DateTime, 8);
                    p.Value = ESP.Media.Access.Utilities.Common.StringToDateTime(obj.Createddate);
                    ht.Add(p);
                }
            }
            if (obj.Attachmentpath != null && obj.Attachmentpath.Trim().Length > 0)
            {
                term += " and attachmentpath=@attachmentpath ";
                if (ESP.Media.Access.Utilities.Common.Find(ht, "@attachmentpath") == -1)
                {
                    SqlParameter p = new SqlParameter("@attachmentpath", SqlDbType.NVarChar, 1000);
                    p.Value = obj.Attachmentpath;
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
            if (obj.Dailyid > 0)//DailyID
            {
                term += " and dailyid=@dailyid ";
                if (ESP.Media.Access.Utilities.Common.Find(ht, "@dailyid") == -1)
                {
                    SqlParameter p = new SqlParameter("@dailyid", SqlDbType.Int, 4);
                    p.Value = obj.Dailyid;
                    ht.Add(p);
                }
            }
            return term;
        }
        #endregion
        #region 查询
        private static string getQueryTerms(DailyattachmentsInfo obj, ref Hashtable ht)
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
            if (obj.Version > 0)//Version
            {
                term += " and a.version=@version ";
                if (!ht.ContainsKey("@version"))
                {
                    ht.Add("@version", obj.Version);
                }
            }
            if (obj.Attachmentid > 0)//AttachmentID
            {
                term += " and a.attachmentid=@attachmentid ";
                if (!ht.ContainsKey("@attachmentid"))
                {
                    ht.Add("@attachmentid", obj.Attachmentid);
                }
            }
            if (obj.Type > 0)//Type
            {
                term += " and a.type=@type ";
                if (!ht.ContainsKey("@type"))
                {
                    ht.Add("@type", obj.Type);
                }
            }
            if (obj.Createdbyuserid > 0)//CreatedByUserID
            {
                term += " and a.createdbyuserid=@createdbyuserid ";
                if (!ht.ContainsKey("@createdbyuserid"))
                {
                    ht.Add("@createdbyuserid", obj.Createdbyuserid);
                }
            }
            if (obj.Createdip != null && obj.Createdip.Trim().Length > 0)
            {
                term += " and a.createdip=@createdip ";
                if (!ht.ContainsKey("@createdip"))
                {
                    ht.Add("@createdip", obj.Createdip);
                }
            }
            if (obj.Createddate != null && obj.Createddate.Trim().Length > 0)
            {
                term += " and a.createddate=@createddate ";
                if (!ht.ContainsKey("@createddate"))
                {
                    ht.Add("@createddate", obj.Createddate);
                }
            }
            if (obj.Attachmentpath != null && obj.Attachmentpath.Trim().Length > 0)
            {
                term += " and a.attachmentpath=@attachmentpath ";
                if (!ht.ContainsKey("@attachmentpath"))
                {
                    ht.Add("@attachmentpath", obj.Attachmentpath);
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
            if (obj.Dailyid > 0)//DailyID
            {
                term += " and a.dailyid=@dailyid ";
                if (!ht.ContainsKey("@dailyid"))
                {
                    ht.Add("@dailyid", obj.Dailyid);
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
            string sql = @"select {0} a.id as id,a.version as version,a.attachmentid as attachmentid,a.type as type,a.createdbyuserid as createdbyuserid,a.createdip as createdip,a.createddate as createddate,a.attachmentpath as attachmentpath,a.del as del,a.dailyid as dailyid {1} from media_dailyattachments as a {2} where 1=1 {3} ";
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


        public static DataTable QueryInfoByObj(DailyattachmentsInfo obj, string terms, Hashtable ht)
        {
            if (ht == null)
            {
                ht = new Hashtable();
            }
            SqlParameter[] para = ESP.Media.Access.Utilities.Common.DictToSqlParam(ht);
            return QueryInfoByObj(obj, terms, para);
        }


        public static DataTable QueryInfoByObj(DailyattachmentsInfo obj, string terms, params SqlParameter[] param)
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
        public static DailyattachmentsInfo Load(int id)
        {
            DataTable dt = null;
            dt = QueryInfo(" and a.id=@id ", new SqlParameter("@id", id));
            if (dt != null && dt.Rows.Count > 0)
            {
                return setObject(dt.Rows[0]);
            }
            return null;
        }


        public static DailyattachmentsInfo Load(int id, SqlTransaction trans)
        {
            DataTable dt = null;
            dt = QueryInfo(trans, " and a.id=@id ", new SqlParameter("@id", id));
            if (dt != null && dt.Rows.Count > 0)
            {
                return setObject(dt.Rows[0]);
            }
            return null;
        }


        public static DailyattachmentsInfo setObject(DataRow dr)
        {
            DailyattachmentsInfo obj = new DailyattachmentsInfo();
            if (dr.Table.Columns.Contains("id") && dr["id"] != DBNull.Value)//id
            {
                obj.Id = Convert.ToInt32(dr["id"]);
            }
            if (dr.Table.Columns.Contains("version") && dr["version"] != DBNull.Value)//版本
            {
                obj.Version = Convert.ToInt32(dr["version"]);
            }
            if (dr.Table.Columns.Contains("attachmentid") && dr["attachmentid"] != DBNull.Value)//附件id
            {
                obj.Attachmentid = Convert.ToInt32(dr["attachmentid"]);
            }
            if (dr.Table.Columns.Contains("type") && dr["type"] != DBNull.Value)//类型
            {
                obj.Type = Convert.ToInt32(dr["type"]);
            }
            if (dr.Table.Columns.Contains("createdbyuserid") && dr["createdbyuserid"] != DBNull.Value)//创建用户id
            {
                obj.Createdbyuserid = Convert.ToInt32(dr["createdbyuserid"]);
            }
            if (dr.Table.Columns.Contains("createdip") && dr["createdip"] != DBNull.Value)//创建ip
            {
                obj.Createdip = (dr["createdip"]).ToString();
            }
            if (dr.Table.Columns.Contains("createddate") && dr["createddate"] != DBNull.Value)//创建时间
            {
                obj.Createddate = (dr["createddate"]).ToString();
            }
            if (dr.Table.Columns.Contains("attachmentpath") && dr["attachmentpath"] != DBNull.Value)//附件路径
            {
                obj.Attachmentpath = (dr["attachmentpath"]).ToString();
            }
            if (dr.Table.Columns.Contains("del") && dr["del"] != DBNull.Value)//删除标记
            {
                obj.Del = Convert.ToInt32(dr["del"]);
            }
            if (dr.Table.Columns.Contains("dailyid") && dr["dailyid"] != DBNull.Value)//日常传播id
            {
                obj.Dailyid = Convert.ToInt32(dr["dailyid"]);
            }
            return obj;
        }
        #endregion
    }
}
