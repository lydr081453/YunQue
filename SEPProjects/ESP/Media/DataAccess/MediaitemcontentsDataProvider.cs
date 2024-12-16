using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using ESP.Media.Entity;
using System.Collections.Generic;
using ESP.Media.Access.Utilities;
namespace ESP.Media.DataAccess
{
    public class MediaitemcontentsDataProvider
    {
        #region 构造函数
        public MediaitemcontentsDataProvider()
        {
        }
        #endregion
        #region 插入
        //插入字符串
        private static string strinsert(MediaitemcontentsInfo obj, ref List<SqlParameter> ht)
        {
            if (ht == null)
            {
                ht = new List<SqlParameter>();
            }
            string sql = @"insert into media_mediaitemcontents (mediaitemid,version,logo,contentxml,modifiedbyuserid,modifiedip,modifieddate,del) values (@mediaitemid,@version,@logo,@contentxml,@modifiedbyuserid,@modifiedip,@modifieddate,@del);select @@IDENTITY as rowNum;";
            SqlParameter param_Mediaitemid = new SqlParameter("@Mediaitemid", SqlDbType.Int, 4);
            param_Mediaitemid.Value = obj.Mediaitemid;
            ht.Add(param_Mediaitemid);
            SqlParameter param_Version = new SqlParameter("@Version", SqlDbType.Int, 4);
            param_Version.Value = obj.Version;
            ht.Add(param_Version);
            SqlParameter param_Logo = new SqlParameter("@Logo", SqlDbType.NVarChar, 512);
            param_Logo.Value = obj.Logo;
            ht.Add(param_Logo);
            SqlParameter param_Contentxml = new SqlParameter("@Contentxml", SqlDbType.Xml, 4000);
            param_Contentxml.Value = obj.Contentxml;
            ht.Add(param_Contentxml);
            SqlParameter param_Modifiedbyuserid = new SqlParameter("@Modifiedbyuserid", SqlDbType.Int, 4);
            param_Modifiedbyuserid.Value = obj.Modifiedbyuserid;
            ht.Add(param_Modifiedbyuserid);
            SqlParameter param_Modifiedip = new SqlParameter("@Modifiedip", SqlDbType.NVarChar, 512);
            param_Modifiedip.Value = obj.Modifiedip;
            ht.Add(param_Modifiedip);
            SqlParameter param_Modifieddate = new SqlParameter("@Modifieddate", SqlDbType.DateTime, 8);
            param_Modifieddate.Value = ESP.Media.Access.Utilities.Common.StringToDateTime(obj.Modifieddate);
            ht.Add(param_Modifieddate);
            SqlParameter param_Del = new SqlParameter("@Del", SqlDbType.SmallInt, 2);
            param_Del.Value = obj.Del;
            ht.Add(param_Del);
            return sql;
        }


        //插入一条记录
        public static int insertinfo(MediaitemcontentsInfo obj, SqlTransaction trans)
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
        public static int insertinfo(MediaitemcontentsInfo obj)
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
            string sql = "delete media_mediaitemcontents where id=@id";
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
        public static string getUpdateString(MediaitemcontentsInfo objTerm, MediaitemcontentsInfo Objupdate, string term, ref List<SqlParameter> ht, params SqlParameter[] param)
        {
            string sql = "update media_mediaitemcontents set mediaitemid=@mediaitemid,version=@version,logo=@logo,contentxml=@contentxml,modifiedbyuserid=@modifiedbyuserid,modifiedip=@modifiedip,modifieddate=@modifieddate,del=@del where 1=1 ";
            SqlParameter param_mediaitemid = new SqlParameter("@mediaitemid", SqlDbType.Int, 4);
            param_mediaitemid.Value = Objupdate.Mediaitemid;
            ht.Add(param_mediaitemid);
            SqlParameter param_version = new SqlParameter("@version", SqlDbType.Int, 4);
            param_version.Value = Objupdate.Version;
            ht.Add(param_version);
            SqlParameter param_logo = new SqlParameter("@logo", SqlDbType.NVarChar, 512);
            param_logo.Value = Objupdate.Logo;
            ht.Add(param_logo);
            SqlParameter param_contentxml = new SqlParameter("@contentxml", SqlDbType.Xml, 4000);
            param_contentxml.Value = Objupdate.Contentxml;
            ht.Add(param_contentxml);
            SqlParameter param_modifiedbyuserid = new SqlParameter("@modifiedbyuserid", SqlDbType.Int, 4);
            param_modifiedbyuserid.Value = Objupdate.Modifiedbyuserid;
            ht.Add(param_modifiedbyuserid);
            SqlParameter param_modifiedip = new SqlParameter("@modifiedip", SqlDbType.NVarChar, 512);
            param_modifiedip.Value = Objupdate.Modifiedip;
            ht.Add(param_modifiedip);
            SqlParameter param_modifieddate = new SqlParameter("@modifieddate", SqlDbType.DateTime, 8);
            param_modifieddate.Value = ESP.Media.Access.Utilities.Common.StringToDateTime(Objupdate.Modifieddate);
            ht.Add(param_modifieddate);
            SqlParameter param_del = new SqlParameter("@del", SqlDbType.SmallInt, 2);
            param_del.Value = Objupdate.Del;
            ht.Add(param_del);
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
        public static bool updateInfo(SqlTransaction trans, MediaitemcontentsInfo objterm, MediaitemcontentsInfo Objupdate, string term, params SqlParameter[] param)
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
        public static bool updateInfo(MediaitemcontentsInfo objterm, MediaitemcontentsInfo Objupdate, string term, params SqlParameter[] param)
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


        private static string getTerms(MediaitemcontentsInfo obj, ref List<SqlParameter> ht)
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
            if (obj.Mediaitemid > 0)//MediaItemID
            {
                term += " and mediaitemid=@mediaitemid ";
                if (ESP.Media.Access.Utilities.Common.Find(ht, "@mediaitemid") == -1)
                {
                    SqlParameter p = new SqlParameter("@mediaitemid", SqlDbType.Int, 4);
                    p.Value = obj.Mediaitemid;
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
            if (obj.Logo != null && obj.Logo.Trim().Length > 0)
            {
                term += " and logo=@logo ";
                if (ESP.Media.Access.Utilities.Common.Find(ht, "@logo") == -1)
                {
                    SqlParameter p = new SqlParameter("@logo", SqlDbType.NVarChar, 512);
                    p.Value = obj.Logo;
                    ht.Add(p);
                }
            }
            if (obj.Contentxml != null && obj.Contentxml.Trim().Length > 0)
            {
                term += " and contentxml=@contentxml ";
                if (ESP.Media.Access.Utilities.Common.Find(ht, "@contentxml") == -1)
                {
                    SqlParameter p = new SqlParameter("@contentxml", SqlDbType.Xml, 4000);
                    p.Value = obj.Contentxml;
                    ht.Add(p);
                }
            }
            if (obj.Modifiedbyuserid > 0)//ModifiedByUserID
            {
                term += " and modifiedbyuserid=@modifiedbyuserid ";
                if (ESP.Media.Access.Utilities.Common.Find(ht, "@modifiedbyuserid") == -1)
                {
                    SqlParameter p = new SqlParameter("@modifiedbyuserid", SqlDbType.Int, 4);
                    p.Value = obj.Modifiedbyuserid;
                    ht.Add(p);
                }
            }
            if (obj.Modifiedip != null && obj.Modifiedip.Trim().Length > 0)
            {
                term += " and modifiedip=@modifiedip ";
                if (ESP.Media.Access.Utilities.Common.Find(ht, "@modifiedip") == -1)
                {
                    SqlParameter p = new SqlParameter("@modifiedip", SqlDbType.NVarChar, 512);
                    p.Value = obj.Modifiedip;
                    ht.Add(p);
                }
            }
            if (obj.Modifieddate != null && obj.Modifieddate.Trim().Length > 0)
            {
                term += " and modifieddate=@modifieddate ";
                if (ESP.Media.Access.Utilities.Common.Find(ht, "@modifieddate") == -1)
                {
                    SqlParameter p = new SqlParameter("@modifieddate", SqlDbType.DateTime, 8);
                    p.Value = ESP.Media.Access.Utilities.Common.StringToDateTime(obj.Modifieddate);
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
            return term;
        }
        #endregion
        #region 查询
        private static string getQueryTerms(MediaitemcontentsInfo obj, ref Hashtable ht)
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
            if (obj.Mediaitemid > 0)//MediaItemID
            {
                term += " and a.mediaitemid=@mediaitemid ";
                if (!ht.ContainsKey("@mediaitemid"))
                {
                    ht.Add("@mediaitemid", obj.Mediaitemid);
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
            if (obj.Logo != null && obj.Logo.Trim().Length > 0)
            {
                term += " and a.logo=@logo ";
                if (!ht.ContainsKey("@logo"))
                {
                    ht.Add("@logo", obj.Logo);
                }
            }
            if (obj.Contentxml != null && obj.Contentxml.Trim().Length > 0)
            {
                term += " and a.contentxml=@contentxml ";
                if (!ht.ContainsKey("@contentxml"))
                {
                    ht.Add("@contentxml", obj.Contentxml);
                }
            }
            if (obj.Modifiedbyuserid > 0)//ModifiedByUserID
            {
                term += " and a.modifiedbyuserid=@modifiedbyuserid ";
                if (!ht.ContainsKey("@modifiedbyuserid"))
                {
                    ht.Add("@modifiedbyuserid", obj.Modifiedbyuserid);
                }
            }
            if (obj.Modifiedip != null && obj.Modifiedip.Trim().Length > 0)
            {
                term += " and a.modifiedip=@modifiedip ";
                if (!ht.ContainsKey("@modifiedip"))
                {
                    ht.Add("@modifiedip", obj.Modifiedip);
                }
            }
            if (obj.Modifieddate != null && obj.Modifieddate.Trim().Length > 0)
            {
                term += " and a.modifieddate=@modifieddate ";
                if (!ht.ContainsKey("@modifieddate"))
                {
                    ht.Add("@modifieddate", obj.Modifieddate);
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
            string sql = @"select {0} a.id as id,a.mediaitemid as mediaitemid,a.version as version,a.logo as logo,a.contentxml as contentxml,a.modifiedbyuserid as modifiedbyuserid,a.modifiedip as modifiedip,a.modifieddate as modifieddate,a.del as del {1} from media_mediaitemcontents as a {2} where 1=1 {3} ";
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


        public static DataTable QueryInfoByObj(MediaitemcontentsInfo obj, string terms, Hashtable ht)
        {
            if (ht == null)
            {
                ht = new Hashtable();
            }
            SqlParameter[] para = ESP.Media.Access.Utilities.Common.DictToSqlParam(ht);
            return QueryInfoByObj(obj, terms, para);
        }


        public static DataTable QueryInfoByObj(MediaitemcontentsInfo obj, string terms, params SqlParameter[] param)
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
        public static MediaitemcontentsInfo Load(int id)
        {
            DataTable dt = null;
            dt = QueryInfo(" and a.id=@id ", new SqlParameter("@id", id));
            if (dt != null && dt.Rows.Count > 0)
            {
                return setObject(dt.Rows[0]);
            }
            return null;
        }


        public static MediaitemcontentsInfo Load(int id, SqlTransaction trans)
        {
            DataTable dt = null;
            dt = QueryInfo(trans, " and a.id=@id ", new SqlParameter("@id", id));
            if (dt != null && dt.Rows.Count > 0)
            {
                return setObject(dt.Rows[0]);
            }
            return null;
        }


        public static MediaitemcontentsInfo setObject(DataRow dr)
        {
            MediaitemcontentsInfo obj = new MediaitemcontentsInfo();
            if (dr.Table.Columns.Contains("id") && dr["id"] != DBNull.Value)//id
            {
                obj.Id = Convert.ToInt32(dr["id"]);
            }
            if (dr.Table.Columns.Contains("mediaitemid") && dr["mediaitemid"] != DBNull.Value)//媒体id
            {
                obj.Mediaitemid = Convert.ToInt32(dr["mediaitemid"]);
            }
            if (dr.Table.Columns.Contains("version") && dr["version"] != DBNull.Value)//版本
            {
                obj.Version = Convert.ToInt32(dr["version"]);
            }
            if (dr.Table.Columns.Contains("logo") && dr["logo"] != DBNull.Value)//媒体图片
            {
                obj.Logo = (dr["logo"]).ToString();
            }
            if (dr.Table.Columns.Contains("contentxml") && dr["contentxml"] != DBNull.Value)//内容
            {
                obj.Contentxml = (dr["contentxml"]).ToString();
            }
            if (dr.Table.Columns.Contains("modifiedbyuserid") && dr["modifiedbyuserid"] != DBNull.Value)//修改用户id
            {
                obj.Modifiedbyuserid = Convert.ToInt32(dr["modifiedbyuserid"]);
            }
            if (dr.Table.Columns.Contains("modifiedip") && dr["modifiedip"] != DBNull.Value)//修改ip
            {
                obj.Modifiedip = (dr["modifiedip"]).ToString();
            }
            if (dr.Table.Columns.Contains("modifieddate") && dr["modifieddate"] != DBNull.Value)//修改时间
            {
                obj.Modifieddate = (dr["modifieddate"]).ToString();
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
