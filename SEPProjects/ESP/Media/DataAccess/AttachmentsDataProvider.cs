using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.Collections.Generic;
using ESP.Media.Access.Utilities;
using ESP.Media.Entity;
namespace ESP.Media.DataAccess
{
    public class AttachmentsDataProvider
    {
        #region 构造函数
        public AttachmentsDataProvider()
        {
        }
        #endregion
        #region 插入
        //插入字符串
        private static string strinsert(AttachmentsInfo obj, ref List<SqlParameter> ht)
        {
            if (ht == null)
            {
                ht = new List<SqlParameter>();
            }
            string sql = @"insert into media_attachments (attachmentid,contenttype,contentlength,contentbody,lastupdateddate,createddate,isimage,imageheight,imagewidth,del) values (@attachmentid,@contenttype,@contentlength,@contentbody,@lastupdateddate,@createddate,@isimage,@imageheight,@imagewidth,@del);select @@IDENTITY as rowNum;";
            SqlParameter param_Attachmentid = new SqlParameter("@Attachmentid", SqlDbType.Int, 4);
            param_Attachmentid.Value = obj.Attachmentid;
            ht.Add(param_Attachmentid);
            SqlParameter param_Contenttype = new SqlParameter("@Contenttype", SqlDbType.NVarChar, 128);
            param_Contenttype.Value = obj.Contenttype;
            ht.Add(param_Contenttype);
            SqlParameter param_Contentlength = new SqlParameter("@Contentlength", SqlDbType.Int, 4);
            param_Contentlength.Value = obj.Contentlength;
            ht.Add(param_Contentlength);
            SqlParameter param_Contentbody = new SqlParameter("@Contentbody", SqlDbType.NVarChar, 1000);
            param_Contentbody.Value = obj.Contentbody;
            ht.Add(param_Contentbody);
            SqlParameter param_Lastupdateddate = new SqlParameter("@Lastupdateddate", SqlDbType.DateTime, 8);
            param_Lastupdateddate.Value = ESP.Media.Access.Utilities.Common.StringToDateTime(obj.Lastupdateddate);
            ht.Add(param_Lastupdateddate);
            SqlParameter param_Createddate = new SqlParameter("@Createddate", SqlDbType.DateTime, 8);
            param_Createddate.Value = ESP.Media.Access.Utilities.Common.StringToDateTime(obj.Createddate);
            ht.Add(param_Createddate);
            SqlParameter param_Isimage = new SqlParameter("@Isimage", SqlDbType.Bit, 1);
            param_Isimage.Value = obj.Isimage;
            ht.Add(param_Isimage);
            SqlParameter param_Imageheight = new SqlParameter("@Imageheight", SqlDbType.Int, 4);
            param_Imageheight.Value = obj.Imageheight;
            ht.Add(param_Imageheight);
            SqlParameter param_Imagewidth = new SqlParameter("@Imagewidth", SqlDbType.Int, 4);
            param_Imagewidth.Value = obj.Imagewidth;
            ht.Add(param_Imagewidth);
            SqlParameter param_Del = new SqlParameter("@Del", SqlDbType.SmallInt, 2);
            param_Del.Value = obj.Del;
            ht.Add(param_Del);
            return sql;
        }


        //插入一条记录
        public static int insertinfo(AttachmentsInfo obj, SqlTransaction trans)
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
        public static int insertinfo(AttachmentsInfo obj)
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
            string sql = "delete media_attachments";
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
        public static string getUpdateString(AttachmentsInfo objTerm, AttachmentsInfo Objupdate, string term, ref List<SqlParameter> ht, params SqlParameter[] param)
        {
            string sql = "update media_attachments set attachmentid=@attachmentid,contenttype=@contenttype,contentlength=@contentlength,contentbody=@contentbody,lastupdateddate=@lastupdateddate,createddate=@createddate,isimage=@isimage,imageheight=@imageheight,imagewidth=@imagewidth,del=@del where 1=1 ";
            SqlParameter param_attachmentid = new SqlParameter("@attachmentid", SqlDbType.Int, 4);
            param_attachmentid.Value = Objupdate.Attachmentid;
            ht.Add(param_attachmentid);
            SqlParameter param_contenttype = new SqlParameter("@contenttype", SqlDbType.NVarChar, 128);
            param_contenttype.Value = Objupdate.Contenttype;
            ht.Add(param_contenttype);
            SqlParameter param_contentlength = new SqlParameter("@contentlength", SqlDbType.Int, 4);
            param_contentlength.Value = Objupdate.Contentlength;
            ht.Add(param_contentlength);
            SqlParameter param_contentbody = new SqlParameter("@contentbody", SqlDbType.NVarChar, 1000);
            param_contentbody.Value = Objupdate.Contentbody;
            ht.Add(param_contentbody);
            SqlParameter param_lastupdateddate = new SqlParameter("@lastupdateddate", SqlDbType.DateTime, 8);
            param_lastupdateddate.Value = ESP.Media.Access.Utilities.Common.StringToDateTime(Objupdate.Lastupdateddate);
            ht.Add(param_lastupdateddate);
            SqlParameter param_createddate = new SqlParameter("@createddate", SqlDbType.DateTime, 8);
            param_createddate.Value = ESP.Media.Access.Utilities.Common.StringToDateTime(Objupdate.Createddate);
            ht.Add(param_createddate);
            SqlParameter param_isimage = new SqlParameter("@isimage", SqlDbType.Bit, 1);
            param_isimage.Value = Objupdate.Isimage;
            ht.Add(param_isimage);
            SqlParameter param_imageheight = new SqlParameter("@imageheight", SqlDbType.Int, 4);
            param_imageheight.Value = Objupdate.Imageheight;
            ht.Add(param_imageheight);
            SqlParameter param_imagewidth = new SqlParameter("@imagewidth", SqlDbType.Int, 4);
            param_imagewidth.Value = Objupdate.Imagewidth;
            ht.Add(param_imagewidth);
            SqlParameter param_del = new SqlParameter("@del", SqlDbType.SmallInt, 2);
            param_del.Value = Objupdate.Del;
            ht.Add(param_del);
            if (objTerm == null && (term == null || term.Trim().Length <= 0))
            {
                sql += " and attachmentid=@id ";
                if (ESP.Media.Access.Utilities.Common.Find(ht, "@id") == -1)
                {
                    SqlParameter p = new SqlParameter("@id", SqlDbType.Int, 4);
                    p.Value = Objupdate.Attachmentid;
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
        public static bool updateInfo(SqlTransaction trans, AttachmentsInfo objterm, AttachmentsInfo Objupdate, string term, params SqlParameter[] param)
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
        public static bool updateInfo(AttachmentsInfo objterm, AttachmentsInfo Objupdate, string term, params SqlParameter[] param)
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


        private static string getTerms(AttachmentsInfo obj, ref List<SqlParameter> ht)
        {
            string term = string.Empty;
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
            if (obj.Contenttype != null && obj.Contenttype.Trim().Length > 0)
            {
                term += " and contenttype=@contenttype ";
                if (ESP.Media.Access.Utilities.Common.Find(ht, "@contenttype") == -1)
                {
                    SqlParameter p = new SqlParameter("@contenttype", SqlDbType.NVarChar, 128);
                    p.Value = obj.Contenttype;
                    ht.Add(p);
                }
            }
            if (obj.Contentlength > 0)//ContentLength
            {
                term += " and contentlength=@contentlength ";
                if (ESP.Media.Access.Utilities.Common.Find(ht, "@contentlength") == -1)
                {
                    SqlParameter p = new SqlParameter("@contentlength", SqlDbType.Int, 4);
                    p.Value = obj.Contentlength;
                    ht.Add(p);
                }
            }
            if (obj.Contentbody != null && obj.Contentbody.Trim().Length > 0)
            {
                term += " and contentbody=@contentbody ";
                if (ESP.Media.Access.Utilities.Common.Find(ht, "@contentbody") == -1)
                {
                    SqlParameter p = new SqlParameter("@contentbody", SqlDbType.NVarChar, 1000);
                    p.Value = obj.Contentbody;
                    ht.Add(p);
                }
            }
            if (obj.Lastupdateddate != null && obj.Lastupdateddate.Trim().Length > 0)
            {
                term += " and lastupdateddate=@lastupdateddate ";
                if (ESP.Media.Access.Utilities.Common.Find(ht, "@lastupdateddate") == -1)
                {
                    SqlParameter p = new SqlParameter("@lastupdateddate", SqlDbType.DateTime, 8);
                    p.Value = ESP.Media.Access.Utilities.Common.StringToDateTime(obj.Lastupdateddate);
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
            {
                term += " and isimage=@isimage ";
                if (ESP.Media.Access.Utilities.Common.Find(ht, "@isimage") == -1)
                {
                    SqlParameter p = new SqlParameter("@isimage", SqlDbType.Bit, 1);
                    p.Value = obj.Isimage;
                    ht.Add(p);
                }
            }
            if (obj.Imageheight > 0)//ImageHeight
            {
                term += " and imageheight=@imageheight ";
                if (ESP.Media.Access.Utilities.Common.Find(ht, "@imageheight") == -1)
                {
                    SqlParameter p = new SqlParameter("@imageheight", SqlDbType.Int, 4);
                    p.Value = obj.Imageheight;
                    ht.Add(p);
                }
            }
            if (obj.Imagewidth > 0)//ImageWidth
            {
                term += " and imagewidth=@imagewidth ";
                if (ESP.Media.Access.Utilities.Common.Find(ht, "@imagewidth") == -1)
                {
                    SqlParameter p = new SqlParameter("@imagewidth", SqlDbType.Int, 4);
                    p.Value = obj.Imagewidth;
                    ht.Add(p);
                }
            }
            if (obj.Del > 0)//删除标示位
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
        private static string getQueryTerms(AttachmentsInfo obj, ref Hashtable ht)
        {
            string term = string.Empty;
            if (obj.Attachmentid > 0)//AttachmentID
            {
                term += " and a.attachmentid=@attachmentid ";
                if (!ht.ContainsKey("@attachmentid"))
                {
                    ht.Add("@attachmentid", obj.Attachmentid);
                }
            }
            if (obj.Contenttype != null && obj.Contenttype.Trim().Length > 0)
            {
                term += " and a.contenttype=@contenttype ";
                if (!ht.ContainsKey("@contenttype"))
                {
                    ht.Add("@contenttype", obj.Contenttype);
                }
            }
            if (obj.Contentlength > 0)//ContentLength
            {
                term += " and a.contentlength=@contentlength ";
                if (!ht.ContainsKey("@contentlength"))
                {
                    ht.Add("@contentlength", obj.Contentlength);
                }
            }
            if (obj.Contentbody != null && obj.Contentbody.Trim().Length > 0)
            {
                term += " and a.contentbody=@contentbody ";
                if (!ht.ContainsKey("@contentbody"))
                {
                    ht.Add("@contentbody", obj.Contentbody);
                }
            }
            if (obj.Lastupdateddate != null && obj.Lastupdateddate.Trim().Length > 0)
            {
                term += " and a.lastupdateddate=@lastupdateddate ";
                if (!ht.ContainsKey("@lastupdateddate"))
                {
                    ht.Add("@lastupdateddate", obj.Lastupdateddate);
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
            {
                term += " and a.isimage=@isimage ";
                if (!ht.ContainsKey("@isimage"))
                {
                    ht.Add("@isimage", obj.Isimage);
                }
            }
            if (obj.Imageheight > 0)//ImageHeight
            {
                term += " and a.imageheight=@imageheight ";
                if (!ht.ContainsKey("@imageheight"))
                {
                    ht.Add("@imageheight", obj.Imageheight);
                }
            }
            if (obj.Imagewidth > 0)//ImageWidth
            {
                term += " and a.imagewidth=@imagewidth ";
                if (!ht.ContainsKey("@imagewidth"))
                {
                    ht.Add("@imagewidth", obj.Imagewidth);
                }
            }
            if (obj.Del > 0)//删除标示位
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
            string sql = @"select {0} a.attachmentid as attachmentid,a.contenttype as contenttype,a.contentlength as contentlength,a.contentbody as contentbody,a.lastupdateddate as lastupdateddate,a.createddate as createddate,a.isimage as isimage,a.imageheight as imageheight,a.imagewidth as imagewidth,a.del as del {1} from media_attachments as a {2} where 1=1 {3} ";
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


        public static DataTable QueryInfoByObj(AttachmentsInfo obj, string terms, Hashtable ht)
        {
            if (ht == null)
            {
                ht = new Hashtable();
            }
            SqlParameter[] para = ESP.Media.Access.Utilities.Common.DictToSqlParam(ht);
            return QueryInfoByObj(obj, terms, para);
        }


        public static DataTable QueryInfoByObj(AttachmentsInfo obj, string terms, params SqlParameter[] param)
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
        public static AttachmentsInfo Load(int id)
        {
            DataTable dt = null;
            dt = QueryInfo(" and a.attachmentid=@id ", new SqlParameter("@id", id));
            if (dt != null && dt.Rows.Count > 0)
            {
                return setObject(dt.Rows[0]);
            }
            return null;
        }


        public static AttachmentsInfo Load(int id, SqlTransaction trans)
        {
            DataTable dt = null;
            dt = QueryInfo(trans, " and a.attachmentid=@id ", new SqlParameter("@id", id));
            if (dt != null && dt.Rows.Count > 0)
            {
                return setObject(dt.Rows[0]);
            }
            return null;
        }


        public static AttachmentsInfo setObject(DataRow dr)
        {
            AttachmentsInfo obj = new AttachmentsInfo();
            if (dr.Table.Columns.Contains("attachmentid") && dr["attachmentid"] != DBNull.Value)//媒体图片附件ID
            {
                obj.Attachmentid = Convert.ToInt32(dr["attachmentid"]);
            }
            if (dr.Table.Columns.Contains("contenttype") && dr["contenttype"] != DBNull.Value)//附件类型
            {
                obj.Contenttype = (dr["contenttype"]).ToString();
            }
            if (dr.Table.Columns.Contains("contentlength") && dr["contentlength"] != DBNull.Value)//长度
            {
                obj.Contentlength = Convert.ToInt32(dr["contentlength"]);
            }
            if (dr.Table.Columns.Contains("contentbody") && dr["contentbody"] != DBNull.Value)//内容
            {
                obj.Contentbody = (dr["contentbody"]).ToString();
            }
            if (dr.Table.Columns.Contains("lastupdateddate") && dr["lastupdateddate"] != DBNull.Value)//最后更新日期
            {
                obj.Lastupdateddate = (dr["lastupdateddate"]).ToString();
            }
            if (dr.Table.Columns.Contains("createddate") && dr["createddate"] != DBNull.Value)//创建日期
            {
                obj.Createddate = (dr["createddate"]).ToString();
            }
            if (dr.Table.Columns.Contains("isimage") && dr["isimage"] != DBNull.Value)//是否为图片
            {
                obj.Isimage = Convert.ToBoolean(dr["isimage"]);
            }
            if (dr.Table.Columns.Contains("imageheight") && dr["imageheight"] != DBNull.Value)//图片的高度
            {
                obj.Imageheight = Convert.ToInt32(dr["imageheight"]);
            }
            if (dr.Table.Columns.Contains("imagewidth") && dr["imagewidth"] != DBNull.Value)//图片的宽度
            {
                obj.Imagewidth = Convert.ToInt32(dr["imagewidth"]);
            }
            if (dr.Table.Columns.Contains("del") && dr["del"] != DBNull.Value)//删除标示位
            {
                obj.Del = Convert.ToInt32(dr["del"]);
            }
            return obj;
        }
        #endregion
    }
}
