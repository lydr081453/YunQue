using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using ESP.Media.Access.Utilities;
using System.Collections.Generic;
using ESP.Media.Entity;
namespace ESP.Media.DataAccess
{
    public class ProductlinesDataProvider
    {
        #region 构造函数
        public ProductlinesDataProvider()
        {
        }
        #endregion
        #region 插入
        //插入字符串
        private static string strinsert(ProductlinesInfo obj, ref List<SqlParameter> ht)
        {
            if (ht == null)
            {
                ht = new List<SqlParameter>();
            }
            string sql = @"insert into media_productlines (productlinename,productlinedescription,productlinetitle,clientid,currentversion,status,createdbyuserid,createdip,createddate,lastmodifiedbyuserid,lastmodifiedip,lastmodifieddate,del) values (@productlinename,@productlinedescription,@productlinetitle,@clientid,@currentversion,@status,@createdbyuserid,@createdip,@createddate,@lastmodifiedbyuserid,@lastmodifiedip,@lastmodifieddate,@del);select @@IDENTITY as rowNum;";
            SqlParameter param_Productlinename = new SqlParameter("@Productlinename", SqlDbType.NVarChar, 400);
            param_Productlinename.Value = obj.Productlinename;
            ht.Add(param_Productlinename);
            SqlParameter param_Productlinedescription = new SqlParameter("@Productlinedescription", SqlDbType.NVarChar, 400);
            param_Productlinedescription.Value = obj.Productlinedescription;
            ht.Add(param_Productlinedescription);
            SqlParameter param_Productlinetitle = new SqlParameter("@Productlinetitle", SqlDbType.NVarChar, 1024);
            param_Productlinetitle.Value = obj.Productlinetitle;
            ht.Add(param_Productlinetitle);
            SqlParameter param_Clientid = new SqlParameter("@Clientid", SqlDbType.Int, 4);
            param_Clientid.Value = obj.Clientid;
            ht.Add(param_Clientid);
            SqlParameter param_Currentversion = new SqlParameter("@Currentversion", SqlDbType.Int, 4);
            param_Currentversion.Value = obj.Currentversion;
            ht.Add(param_Currentversion);
            SqlParameter param_Status = new SqlParameter("@Status", SqlDbType.Int, 4);
            param_Status.Value = obj.Status;
            ht.Add(param_Status);
            SqlParameter param_Createdbyuserid = new SqlParameter("@Createdbyuserid", SqlDbType.Int, 4);
            param_Createdbyuserid.Value = obj.Createdbyuserid;
            ht.Add(param_Createdbyuserid);
            SqlParameter param_Createdip = new SqlParameter("@Createdip", SqlDbType.NVarChar, 512);
            param_Createdip.Value = obj.Createdip;
            ht.Add(param_Createdip);
            SqlParameter param_Createddate = new SqlParameter("@Createddate", SqlDbType.DateTime, 8);
            param_Createddate.Value = ESP.Media.Access.Utilities.Common.StringToDateTime(obj.Createddate);
            ht.Add(param_Createddate);
            SqlParameter param_Lastmodifiedbyuserid = new SqlParameter("@Lastmodifiedbyuserid", SqlDbType.Int, 4);
            param_Lastmodifiedbyuserid.Value = obj.Lastmodifiedbyuserid;
            ht.Add(param_Lastmodifiedbyuserid);
            SqlParameter param_Lastmodifiedip = new SqlParameter("@Lastmodifiedip", SqlDbType.NVarChar, 512);
            param_Lastmodifiedip.Value = obj.Lastmodifiedip;
            ht.Add(param_Lastmodifiedip);
            SqlParameter param_Lastmodifieddate = new SqlParameter("@Lastmodifieddate", SqlDbType.DateTime, 8);
            param_Lastmodifieddate.Value = ESP.Media.Access.Utilities.Common.StringToDateTime(obj.Lastmodifieddate);
            ht.Add(param_Lastmodifieddate);
            SqlParameter param_Del = new SqlParameter("@Del", SqlDbType.SmallInt, 2);
            param_Del.Value = obj.Del;
            ht.Add(param_Del);
            return sql;
        }


        //插入一条记录
        public static int insertinfo(ProductlinesInfo obj, SqlTransaction trans)
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
        public static int insertinfo(ProductlinesInfo obj)
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
            string sql = "delete media_productlines where ProductLineID=@id";
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
        public static string getUpdateString(ProductlinesInfo objTerm, ProductlinesInfo Objupdate, string term, ref List<SqlParameter> ht, params SqlParameter[] param)
        {
            string sql = "update media_productlines set productlinename=@productlinename,productlinedescription=@productlinedescription,productlinetitle=@productlinetitle,clientid=@clientid,currentversion=@currentversion,status=@status,createdbyuserid=@createdbyuserid,createdip=@createdip,createddate=@createddate,lastmodifiedbyuserid=@lastmodifiedbyuserid,lastmodifiedip=@lastmodifiedip,lastmodifieddate=@lastmodifieddate,del=@del where 1=1 ";
            SqlParameter param_productlinename = new SqlParameter("@productlinename", SqlDbType.NVarChar, 400);
            param_productlinename.Value = Objupdate.Productlinename;
            ht.Add(param_productlinename);
            SqlParameter param_productlinedescription = new SqlParameter("@productlinedescription", SqlDbType.NVarChar, 400);
            param_productlinedescription.Value = Objupdate.Productlinedescription;
            ht.Add(param_productlinedescription);
            SqlParameter param_productlinetitle = new SqlParameter("@productlinetitle", SqlDbType.NVarChar, 1024);
            param_productlinetitle.Value = Objupdate.Productlinetitle;
            ht.Add(param_productlinetitle);
            SqlParameter param_clientid = new SqlParameter("@clientid", SqlDbType.Int, 4);
            param_clientid.Value = Objupdate.Clientid;
            ht.Add(param_clientid);
            SqlParameter param_currentversion = new SqlParameter("@currentversion", SqlDbType.Int, 4);
            param_currentversion.Value = Objupdate.Currentversion;
            ht.Add(param_currentversion);
            SqlParameter param_status = new SqlParameter("@status", SqlDbType.Int, 4);
            param_status.Value = Objupdate.Status;
            ht.Add(param_status);
            SqlParameter param_createdbyuserid = new SqlParameter("@createdbyuserid", SqlDbType.Int, 4);
            param_createdbyuserid.Value = Objupdate.Createdbyuserid;
            ht.Add(param_createdbyuserid);
            SqlParameter param_createdip = new SqlParameter("@createdip", SqlDbType.NVarChar, 512);
            param_createdip.Value = Objupdate.Createdip;
            ht.Add(param_createdip);
            SqlParameter param_createddate = new SqlParameter("@createddate", SqlDbType.DateTime, 8);
            param_createddate.Value = ESP.Media.Access.Utilities.Common.StringToDateTime(Objupdate.Createddate);
            ht.Add(param_createddate);
            SqlParameter param_lastmodifiedbyuserid = new SqlParameter("@lastmodifiedbyuserid", SqlDbType.Int, 4);
            param_lastmodifiedbyuserid.Value = Objupdate.Lastmodifiedbyuserid;
            ht.Add(param_lastmodifiedbyuserid);
            SqlParameter param_lastmodifiedip = new SqlParameter("@lastmodifiedip", SqlDbType.NVarChar, 512);
            param_lastmodifiedip.Value = Objupdate.Lastmodifiedip;
            ht.Add(param_lastmodifiedip);
            SqlParameter param_lastmodifieddate = new SqlParameter("@lastmodifieddate", SqlDbType.DateTime, 8);
            param_lastmodifieddate.Value = ESP.Media.Access.Utilities.Common.StringToDateTime(Objupdate.Lastmodifieddate);
            ht.Add(param_lastmodifieddate);
            SqlParameter param_del = new SqlParameter("@del", SqlDbType.SmallInt, 2);
            param_del.Value = Objupdate.Del;
            ht.Add(param_del);
            if (objTerm == null && (term == null || term.Trim().Length <= 0))
            {
                sql += " and ProductLineID=@id ";
                if (ESP.Media.Access.Utilities.Common.Find(ht, "@id") == -1)
                {
                    SqlParameter p = new SqlParameter("@id", SqlDbType.Int, 4);
                    p.Value = Objupdate.Productlineid;
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
        public static bool updateInfo(SqlTransaction trans, ProductlinesInfo objterm, ProductlinesInfo Objupdate, string term, params SqlParameter[] param)
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
        public static bool updateInfo(ProductlinesInfo objterm, ProductlinesInfo Objupdate, string term, params SqlParameter[] param)
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


        private static string getTerms(ProductlinesInfo obj, ref List<SqlParameter> ht)
        {
            string term = string.Empty;
            if (obj.Productlineid > 0)//ProductLineID
            {
                term += " and productlineid=@productlineid ";
                if (ESP.Media.Access.Utilities.Common.Find(ht, "@productlineid") == -1)
                {
                    SqlParameter p = new SqlParameter("@productlineid", SqlDbType.Int, 4);
                    p.Value = obj.Productlineid;
                    ht.Add(p);
                }
            }
            if (obj.Productlinename != null && obj.Productlinename.Trim().Length > 0)
            {
                term += " and productlinename=@productlinename ";
                if (ESP.Media.Access.Utilities.Common.Find(ht, "@productlinename") == -1)
                {
                    SqlParameter p = new SqlParameter("@productlinename", SqlDbType.NVarChar, 400);
                    p.Value = obj.Productlinename;
                    ht.Add(p);
                }
            }
            if (obj.Productlinedescription != null && obj.Productlinedescription.Trim().Length > 0)
            {
                term += " and productlinedescription=@productlinedescription ";
                if (ESP.Media.Access.Utilities.Common.Find(ht, "@productlinedescription") == -1)
                {
                    SqlParameter p = new SqlParameter("@productlinedescription", SqlDbType.NVarChar, 400);
                    p.Value = obj.Productlinedescription;
                    ht.Add(p);
                }
            }
            if (obj.Productlinetitle != null && obj.Productlinetitle.Trim().Length > 0)
            {
                term += " and productlinetitle=@productlinetitle ";
                if (ESP.Media.Access.Utilities.Common.Find(ht, "@productlinetitle") == -1)
                {
                    SqlParameter p = new SqlParameter("@productlinetitle", SqlDbType.NVarChar, 1024);
                    p.Value = obj.Productlinetitle;
                    ht.Add(p);
                }
            }
            if (obj.Clientid > 0)//ClientID
            {
                term += " and clientid=@clientid ";
                if (ESP.Media.Access.Utilities.Common.Find(ht, "@clientid") == -1)
                {
                    SqlParameter p = new SqlParameter("@clientid", SqlDbType.Int, 4);
                    p.Value = obj.Clientid;
                    ht.Add(p);
                }
            }
            if (obj.Currentversion > 0)//CurrentVersion
            {
                term += " and currentversion=@currentversion ";
                if (ESP.Media.Access.Utilities.Common.Find(ht, "@currentversion") == -1)
                {
                    SqlParameter p = new SqlParameter("@currentversion", SqlDbType.Int, 4);
                    p.Value = obj.Currentversion;
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
            if (obj.Lastmodifiedbyuserid > 0)//LastModifiedByUserID
            {
                term += " and lastmodifiedbyuserid=@lastmodifiedbyuserid ";
                if (ESP.Media.Access.Utilities.Common.Find(ht, "@lastmodifiedbyuserid") == -1)
                {
                    SqlParameter p = new SqlParameter("@lastmodifiedbyuserid", SqlDbType.Int, 4);
                    p.Value = obj.Lastmodifiedbyuserid;
                    ht.Add(p);
                }
            }
            if (obj.Lastmodifiedip != null && obj.Lastmodifiedip.Trim().Length > 0)
            {
                term += " and lastmodifiedip=@lastmodifiedip ";
                if (ESP.Media.Access.Utilities.Common.Find(ht, "@lastmodifiedip") == -1)
                {
                    SqlParameter p = new SqlParameter("@lastmodifiedip", SqlDbType.NVarChar, 512);
                    p.Value = obj.Lastmodifiedip;
                    ht.Add(p);
                }
            }
            if (obj.Lastmodifieddate != null && obj.Lastmodifieddate.Trim().Length > 0)
            {
                term += " and lastmodifieddate=@lastmodifieddate ";
                if (ESP.Media.Access.Utilities.Common.Find(ht, "@lastmodifieddate") == -1)
                {
                    SqlParameter p = new SqlParameter("@lastmodifieddate", SqlDbType.DateTime, 8);
                    p.Value = ESP.Media.Access.Utilities.Common.StringToDateTime(obj.Lastmodifieddate);
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
        private static string getQueryTerms(ProductlinesInfo obj, ref Hashtable ht)
        {
            string term = string.Empty;
            if (obj.Productlineid > 0)//ProductLineID
            {
                term += " and a.productlineid=@productlineid ";
                if (!ht.ContainsKey("@productlineid"))
                {
                    ht.Add("@productlineid", obj.Productlineid);
                }
            }
            if (obj.Productlinename != null && obj.Productlinename.Trim().Length > 0)
            {
                term += " and a.productlinename=@productlinename ";
                if (!ht.ContainsKey("@productlinename"))
                {
                    ht.Add("@productlinename", obj.Productlinename);
                }
            }
            if (obj.Productlinedescription != null && obj.Productlinedescription.Trim().Length > 0)
            {
                term += " and a.productlinedescription=@productlinedescription ";
                if (!ht.ContainsKey("@productlinedescription"))
                {
                    ht.Add("@productlinedescription", obj.Productlinedescription);
                }
            }
            if (obj.Productlinetitle != null && obj.Productlinetitle.Trim().Length > 0)
            {
                term += " and a.productlinetitle=@productlinetitle ";
                if (!ht.ContainsKey("@productlinetitle"))
                {
                    ht.Add("@productlinetitle", obj.Productlinetitle);
                }
            }
            if (obj.Clientid > 0)//ClientID
            {
                term += " and a.clientid=@clientid ";
                if (!ht.ContainsKey("@clientid"))
                {
                    ht.Add("@clientid", obj.Clientid);
                }
            }
            if (obj.Currentversion > 0)//CurrentVersion
            {
                term += " and a.currentversion=@currentversion ";
                if (!ht.ContainsKey("@currentversion"))
                {
                    ht.Add("@currentversion", obj.Currentversion);
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
            if (obj.Lastmodifiedbyuserid > 0)//LastModifiedByUserID
            {
                term += " and a.lastmodifiedbyuserid=@lastmodifiedbyuserid ";
                if (!ht.ContainsKey("@lastmodifiedbyuserid"))
                {
                    ht.Add("@lastmodifiedbyuserid", obj.Lastmodifiedbyuserid);
                }
            }
            if (obj.Lastmodifiedip != null && obj.Lastmodifiedip.Trim().Length > 0)
            {
                term += " and a.lastmodifiedip=@lastmodifiedip ";
                if (!ht.ContainsKey("@lastmodifiedip"))
                {
                    ht.Add("@lastmodifiedip", obj.Lastmodifiedip);
                }
            }
            if (obj.Lastmodifieddate != null && obj.Lastmodifieddate.Trim().Length > 0)
            {
                term += " and a.lastmodifieddate=@lastmodifieddate ";
                if (!ht.ContainsKey("@lastmodifieddate"))
                {
                    ht.Add("@lastmodifieddate", obj.Lastmodifieddate);
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
            string sql = @"select {0} a.productlineid as productlineid,a.productlinename as productlinename,a.productlinedescription as productlinedescription,a.productlinetitle as productlinetitle,a.clientid as clientid,a.currentversion as currentversion,a.status as status,a.createdbyuserid as createdbyuserid,a.createdip as createdip,a.createddate as createddate,a.lastmodifiedbyuserid as lastmodifiedbyuserid,a.lastmodifiedip as lastmodifiedip,a.lastmodifieddate as lastmodifieddate,a.del as del {1} from media_productlines as a {2} where 1=1 {3} ";
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


        public static DataTable QueryInfoByObj(ProductlinesInfo obj, string terms, Hashtable ht)
        {
            if (ht == null)
            {
                ht = new Hashtable();
            }
            SqlParameter[] para = ESP.Media.Access.Utilities.Common.DictToSqlParam(ht);
            return QueryInfoByObj(obj, terms, para);
        }


        public static DataTable QueryInfoByObj(ProductlinesInfo obj, string terms, params SqlParameter[] param)
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
        public static ProductlinesInfo Load(int id)
        {
            DataTable dt = null;
            dt = QueryInfo(" and a.productlineid=@id ", new SqlParameter("@id", id));
            if (dt != null && dt.Rows.Count > 0)
            {
                return setObject(dt.Rows[0]);
            }
            return null;
        }


        public static ProductlinesInfo Load(int id, SqlTransaction trans)
        {
            DataTable dt = null;
            dt = QueryInfo(trans, " and a.productlineid=@id ", new SqlParameter("@id", id));
            if (dt != null && dt.Rows.Count > 0)
            {
                return setObject(dt.Rows[0]);
            }
            return null;
        }


        public static ProductlinesInfo setObject(DataRow dr)
        {
            ProductlinesInfo obj = new ProductlinesInfo();
            if (dr.Table.Columns.Contains("productlineid") && dr["productlineid"] != DBNull.Value)//id
            {
                obj.Productlineid = Convert.ToInt32(dr["productlineid"]);
            }
            if (dr.Table.Columns.Contains("productlinename") && dr["productlinename"] != DBNull.Value)//产品线名称
            {
                obj.Productlinename = (dr["productlinename"]).ToString();
            }
            if (dr.Table.Columns.Contains("productlinedescription") && dr["productlinedescription"] != DBNull.Value)//产品线描述
            {
                obj.Productlinedescription = (dr["productlinedescription"]).ToString();
            }
            if (dr.Table.Columns.Contains("productlinetitle") && dr["productlinetitle"] != DBNull.Value)//产品线标题
            {
                obj.Productlinetitle = (dr["productlinetitle"]).ToString();
            }
            if (dr.Table.Columns.Contains("clientid") && dr["clientid"] != DBNull.Value)//客户id
            {
                obj.Clientid = Convert.ToInt32(dr["clientid"]);
            }
            if (dr.Table.Columns.Contains("currentversion") && dr["currentversion"] != DBNull.Value)//当前版本
            {
                obj.Currentversion = Convert.ToInt32(dr["currentversion"]);
            }
            if (dr.Table.Columns.Contains("status") && dr["status"] != DBNull.Value)//状态
            {
                obj.Status = Convert.ToInt32(dr["status"]);
            }
            if (dr.Table.Columns.Contains("createdbyuserid") && dr["createdbyuserid"] != DBNull.Value)//创建用户id
            {
                obj.Createdbyuserid = Convert.ToInt32(dr["createdbyuserid"]);
            }
            if (dr.Table.Columns.Contains("createdip") && dr["createdip"] != DBNull.Value)//创建ip
            {
                obj.Createdip = (dr["createdip"]).ToString();
            }
            if (dr.Table.Columns.Contains("createddate") && dr["createddate"] != DBNull.Value)//创建日期
            {
                obj.Createddate = (dr["createddate"]).ToString();
            }
            if (dr.Table.Columns.Contains("lastmodifiedbyuserid") && dr["lastmodifiedbyuserid"] != DBNull.Value)//最后修改用户id
            {
                obj.Lastmodifiedbyuserid = Convert.ToInt32(dr["lastmodifiedbyuserid"]);
            }
            if (dr.Table.Columns.Contains("lastmodifiedip") && dr["lastmodifiedip"] != DBNull.Value)//最后修改ip
            {
                obj.Lastmodifiedip = (dr["lastmodifiedip"]).ToString();
            }
            if (dr.Table.Columns.Contains("lastmodifieddate") && dr["lastmodifieddate"] != DBNull.Value)//最后修改时间
            {
                obj.Lastmodifieddate = (dr["lastmodifieddate"]).ToString();
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
