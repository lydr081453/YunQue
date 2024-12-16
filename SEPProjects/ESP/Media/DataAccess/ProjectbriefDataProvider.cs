using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using ESP.Media.Access.Utilities;
using System.Collections.Generic;
using ESP.Media.Entity;
namespace ESP.Media.DataAccess
{
    public class ProjectbriefDataProvider
    {
        #region 构造函数
        public ProjectbriefDataProvider()
        {
        }
        #endregion
        #region 插入
        //插入字符串
        private static string strinsert(ProjectbriefInfo obj, ref List<SqlParameter> ht)
        {
            if (ht == null)
            {
                ht = new List<SqlParameter>();
            }
            string sql = @"insert into media_projectbrief (projectid,briefsubject,wordsaccount,reporterid,mediaitemid,mediaitemtypeid,des,del,linkurl,filepath,createdbyuserid,createdip,createddate,paymentid,issuedate,propagatetype,progagateid) values (@projectid,@briefsubject,@wordsaccount,@reporterid,@mediaitemid,@mediaitemtypeid,@des,@del,@linkurl,@filepath,@createdbyuserid,@createdip,@createddate,@paymentid,@issuedate,@propagatetype,@progagateid);select @@IDENTITY as rowNum;";
            SqlParameter param_Projectid = new SqlParameter("@Projectid", SqlDbType.Int, 4);
            param_Projectid.Value = obj.Projectid;
            ht.Add(param_Projectid);
            SqlParameter param_Briefsubject = new SqlParameter("@Briefsubject", SqlDbType.NVarChar, 200);
            param_Briefsubject.Value = obj.Briefsubject;
            ht.Add(param_Briefsubject);
            SqlParameter param_Wordsaccount = new SqlParameter("@Wordsaccount", SqlDbType.Int, 4);
            param_Wordsaccount.Value = obj.Wordsaccount;
            ht.Add(param_Wordsaccount);
            SqlParameter param_Reporterid = new SqlParameter("@Reporterid", SqlDbType.Int, 4);
            param_Reporterid.Value = obj.Reporterid;
            ht.Add(param_Reporterid);
            SqlParameter param_Mediaitemid = new SqlParameter("@Mediaitemid", SqlDbType.Int, 4);
            param_Mediaitemid.Value = obj.Mediaitemid;
            ht.Add(param_Mediaitemid);
            SqlParameter param_Mediaitemtypeid = new SqlParameter("@Mediaitemtypeid", SqlDbType.Int, 4);
            param_Mediaitemtypeid.Value = obj.Mediaitemtypeid;
            ht.Add(param_Mediaitemtypeid);
            SqlParameter param_Des = new SqlParameter("@Des", SqlDbType.NVarChar, 2000);
            param_Des.Value = obj.Des;
            ht.Add(param_Des);
            SqlParameter param_Del = new SqlParameter("@Del", SqlDbType.SmallInt, 2);
            param_Del.Value = obj.Del;
            ht.Add(param_Del);
            SqlParameter param_Linkurl = new SqlParameter("@Linkurl", SqlDbType.NVarChar, 1000);
            param_Linkurl.Value = obj.Linkurl;
            ht.Add(param_Linkurl);
            SqlParameter param_Filepath = new SqlParameter("@Filepath", SqlDbType.NVarChar, 1000);
            param_Filepath.Value = obj.Filepath;
            ht.Add(param_Filepath);
            SqlParameter param_Createdbyuserid = new SqlParameter("@Createdbyuserid", SqlDbType.Int, 4);
            param_Createdbyuserid.Value = obj.Createdbyuserid;
            ht.Add(param_Createdbyuserid);
            SqlParameter param_Createdip = new SqlParameter("@Createdip", SqlDbType.NVarChar, 100);
            param_Createdip.Value = obj.Createdip;
            ht.Add(param_Createdip);
            SqlParameter param_Createddate = new SqlParameter("@Createddate", SqlDbType.DateTime, 8);
            param_Createddate.Value = ESP.Media.Access.Utilities.Common.StringToDateTime(obj.Createddate);
            ht.Add(param_Createddate);
            SqlParameter param_Paymentid = new SqlParameter("@Paymentid", SqlDbType.Int, 4);
            param_Paymentid.Value = obj.Paymentid;
            ht.Add(param_Paymentid);
            SqlParameter param_Issuedate = new SqlParameter("@Issuedate", SqlDbType.DateTime, 8);
            param_Issuedate.Value = ESP.Media.Access.Utilities.Common.StringToDateTime(obj.Issuedate);
            ht.Add(param_Issuedate);
            SqlParameter param_Propagatetype = new SqlParameter("@Propagatetype", SqlDbType.Int, 4);
            param_Propagatetype.Value = obj.Propagatetype;
            ht.Add(param_Propagatetype);
            SqlParameter param_Progagateid = new SqlParameter("@Progagateid", SqlDbType.Int, 4);
            param_Progagateid.Value = obj.Progagateid;
            ht.Add(param_Progagateid);
            return sql;
        }


        //插入一条记录
        public static int insertinfo(ProjectbriefInfo obj, SqlTransaction trans)
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
        public static int insertinfo(ProjectbriefInfo obj)
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
            string sql = "delete media_projectbrief where id=@id";
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
        public static string getUpdateString(ProjectbriefInfo objTerm, ProjectbriefInfo Objupdate, string term, ref List<SqlParameter> ht, params SqlParameter[] param)
        {
            string sql = "update media_projectbrief set projectid=@projectid,briefsubject=@briefsubject,wordsaccount=@wordsaccount,reporterid=@reporterid,mediaitemid=@mediaitemid,mediaitemtypeid=@mediaitemtypeid,des=@des,del=@del,linkurl=@linkurl,filepath=@filepath,createdbyuserid=@createdbyuserid,createdip=@createdip,createddate=@createddate,paymentid=@paymentid,issuedate=@issuedate,propagatetype=@propagatetype,progagateid=@progagateid where 1=1 ";
            SqlParameter param_projectid = new SqlParameter("@projectid", SqlDbType.Int, 4);
            param_projectid.Value = Objupdate.Projectid;
            ht.Add(param_projectid);
            SqlParameter param_briefsubject = new SqlParameter("@briefsubject", SqlDbType.NVarChar, 200);
            param_briefsubject.Value = Objupdate.Briefsubject;
            ht.Add(param_briefsubject);
            SqlParameter param_wordsaccount = new SqlParameter("@wordsaccount", SqlDbType.Int, 4);
            param_wordsaccount.Value = Objupdate.Wordsaccount;
            ht.Add(param_wordsaccount);
            SqlParameter param_reporterid = new SqlParameter("@reporterid", SqlDbType.Int, 4);
            param_reporterid.Value = Objupdate.Reporterid;
            ht.Add(param_reporterid);
            SqlParameter param_mediaitemid = new SqlParameter("@mediaitemid", SqlDbType.Int, 4);
            param_mediaitemid.Value = Objupdate.Mediaitemid;
            ht.Add(param_mediaitemid);
            SqlParameter param_mediaitemtypeid = new SqlParameter("@mediaitemtypeid", SqlDbType.Int, 4);
            param_mediaitemtypeid.Value = Objupdate.Mediaitemtypeid;
            ht.Add(param_mediaitemtypeid);
            SqlParameter param_des = new SqlParameter("@des", SqlDbType.NVarChar, 2000);
            param_des.Value = Objupdate.Des;
            ht.Add(param_des);
            SqlParameter param_del = new SqlParameter("@del", SqlDbType.SmallInt, 2);
            param_del.Value = Objupdate.Del;
            ht.Add(param_del);
            SqlParameter param_linkurl = new SqlParameter("@linkurl", SqlDbType.NVarChar, 1000);
            param_linkurl.Value = Objupdate.Linkurl;
            ht.Add(param_linkurl);
            SqlParameter param_filepath = new SqlParameter("@filepath", SqlDbType.NVarChar, 1000);
            param_filepath.Value = Objupdate.Filepath;
            ht.Add(param_filepath);
            SqlParameter param_createdbyuserid = new SqlParameter("@createdbyuserid", SqlDbType.Int, 4);
            param_createdbyuserid.Value = Objupdate.Createdbyuserid;
            ht.Add(param_createdbyuserid);
            SqlParameter param_createdip = new SqlParameter("@createdip", SqlDbType.NVarChar, 100);
            param_createdip.Value = Objupdate.Createdip;
            ht.Add(param_createdip);
            SqlParameter param_createddate = new SqlParameter("@createddate", SqlDbType.DateTime, 8);
            param_createddate.Value = ESP.Media.Access.Utilities.Common.StringToDateTime(Objupdate.Createddate);
            ht.Add(param_createddate);
            SqlParameter param_paymentid = new SqlParameter("@paymentid", SqlDbType.Int, 4);
            param_paymentid.Value = Objupdate.Paymentid;
            ht.Add(param_paymentid);
            SqlParameter param_issuedate = new SqlParameter("@issuedate", SqlDbType.DateTime, 8);
            param_issuedate.Value = ESP.Media.Access.Utilities.Common.StringToDateTime(Objupdate.Issuedate);
            ht.Add(param_issuedate);
            SqlParameter param_propagatetype = new SqlParameter("@propagatetype", SqlDbType.Int, 4);
            param_propagatetype.Value = Objupdate.Propagatetype;
            ht.Add(param_propagatetype);
            SqlParameter param_progagateid = new SqlParameter("@progagateid", SqlDbType.Int, 4);
            param_progagateid.Value = Objupdate.Progagateid;
            ht.Add(param_progagateid);
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
        public static bool updateInfo(SqlTransaction trans, ProjectbriefInfo objterm, ProjectbriefInfo Objupdate, string term, params SqlParameter[] param)
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
        public static bool updateInfo(ProjectbriefInfo objterm, ProjectbriefInfo Objupdate, string term, params SqlParameter[] param)
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


        private static string getTerms(ProjectbriefInfo obj, ref List<SqlParameter> ht)
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
            if (obj.Projectid > 0)//ProjectID
            {
                term += " and projectid=@projectid ";
                if (ESP.Media.Access.Utilities.Common.Find(ht, "@projectid") == -1)
                {
                    SqlParameter p = new SqlParameter("@projectid", SqlDbType.Int, 4);
                    p.Value = obj.Projectid;
                    ht.Add(p);
                }
            }
            if (obj.Briefsubject != null && obj.Briefsubject.Trim().Length > 0)
            {
                term += " and briefsubject=@briefsubject ";
                if (ESP.Media.Access.Utilities.Common.Find(ht, "@briefsubject") == -1)
                {
                    SqlParameter p = new SqlParameter("@briefsubject", SqlDbType.NVarChar, 200);
                    p.Value = obj.Briefsubject;
                    ht.Add(p);
                }
            }
            if (obj.Wordsaccount > 0)//wordsaccount
            {
                term += " and wordsaccount=@wordsaccount ";
                if (ESP.Media.Access.Utilities.Common.Find(ht, "@wordsaccount") == -1)
                {
                    SqlParameter p = new SqlParameter("@wordsaccount", SqlDbType.Int, 4);
                    p.Value = obj.Wordsaccount;
                    ht.Add(p);
                }
            }
            if (obj.Reporterid > 0)//reporterid
            {
                term += " and reporterid=@reporterid ";
                if (ESP.Media.Access.Utilities.Common.Find(ht, "@reporterid") == -1)
                {
                    SqlParameter p = new SqlParameter("@reporterid", SqlDbType.Int, 4);
                    p.Value = obj.Reporterid;
                    ht.Add(p);
                }
            }
            if (obj.Mediaitemid > 0)//mediaitemid
            {
                term += " and mediaitemid=@mediaitemid ";
                if (ESP.Media.Access.Utilities.Common.Find(ht, "@mediaitemid") == -1)
                {
                    SqlParameter p = new SqlParameter("@mediaitemid", SqlDbType.Int, 4);
                    p.Value = obj.Mediaitemid;
                    ht.Add(p);
                }
            }
            if (obj.Mediaitemtypeid > 0)//MediaItemTypeID
            {
                term += " and mediaitemtypeid=@mediaitemtypeid ";
                if (ESP.Media.Access.Utilities.Common.Find(ht, "@mediaitemtypeid") == -1)
                {
                    SqlParameter p = new SqlParameter("@mediaitemtypeid", SqlDbType.Int, 4);
                    p.Value = obj.Mediaitemtypeid;
                    ht.Add(p);
                }
            }
            if (obj.Des != null && obj.Des.Trim().Length > 0)
            {
                term += " and des=@des ";
                if (ESP.Media.Access.Utilities.Common.Find(ht, "@des") == -1)
                {
                    SqlParameter p = new SqlParameter("@des", SqlDbType.NVarChar, 2000);
                    p.Value = obj.Des;
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
            if (obj.Linkurl != null && obj.Linkurl.Trim().Length > 0)
            {
                term += " and linkurl=@linkurl ";
                if (ESP.Media.Access.Utilities.Common.Find(ht, "@linkurl") == -1)
                {
                    SqlParameter p = new SqlParameter("@linkurl", SqlDbType.NVarChar, 1000);
                    p.Value = obj.Linkurl;
                    ht.Add(p);
                }
            }
            if (obj.Filepath != null && obj.Filepath.Trim().Length > 0)
            {
                term += " and filepath=@filepath ";
                if (ESP.Media.Access.Utilities.Common.Find(ht, "@filepath") == -1)
                {
                    SqlParameter p = new SqlParameter("@filepath", SqlDbType.NVarChar, 1000);
                    p.Value = obj.Filepath;
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
                    SqlParameter p = new SqlParameter("@createdip", SqlDbType.NVarChar, 100);
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
            if (obj.Paymentid > 0)//paymentid
            {
                term += " and paymentid=@paymentid ";
                if (ESP.Media.Access.Utilities.Common.Find(ht, "@paymentid") == -1)
                {
                    SqlParameter p = new SqlParameter("@paymentid", SqlDbType.Int, 4);
                    p.Value = obj.Paymentid;
                    ht.Add(p);
                }
            }
            if (obj.Issuedate != null && obj.Issuedate.Trim().Length > 0)
            {
                term += " and issuedate=@issuedate ";
                if (ESP.Media.Access.Utilities.Common.Find(ht, "@issuedate") == -1)
                {
                    SqlParameter p = new SqlParameter("@issuedate", SqlDbType.DateTime, 8);
                    p.Value = ESP.Media.Access.Utilities.Common.StringToDateTime(obj.Issuedate);
                    ht.Add(p);
                }
            }
            if (obj.Propagatetype > 0)//Propagatetype
            {
                term += " and propagatetype=@propagatetype ";
                if (ESP.Media.Access.Utilities.Common.Find(ht, "@propagatetype") == -1)
                {
                    SqlParameter p = new SqlParameter("@propagatetype", SqlDbType.Int, 4);
                    p.Value = obj.Propagatetype;
                    ht.Add(p);
                }
            }
            if (obj.Progagateid > 0)//ProgagateID
            {
                term += " and progagateid=@progagateid ";
                if (ESP.Media.Access.Utilities.Common.Find(ht, "@progagateid") == -1)
                {
                    SqlParameter p = new SqlParameter("@progagateid", SqlDbType.Int, 4);
                    p.Value = obj.Progagateid;
                    ht.Add(p);
                }
            }
            return term;
        }
        #endregion
        #region 查询
        private static string getQueryTerms(ProjectbriefInfo obj, ref Hashtable ht)
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
            if (obj.Projectid > 0)//ProjectID
            {
                term += " and a.projectid=@projectid ";
                if (!ht.ContainsKey("@projectid"))
                {
                    ht.Add("@projectid", obj.Projectid);
                }
            }
            if (obj.Briefsubject != null && obj.Briefsubject.Trim().Length > 0)
            {
                term += " and a.briefsubject=@briefsubject ";
                if (!ht.ContainsKey("@briefsubject"))
                {
                    ht.Add("@briefsubject", obj.Briefsubject);
                }
            }
            if (obj.Wordsaccount > 0)//wordsaccount
            {
                term += " and a.wordsaccount=@wordsaccount ";
                if (!ht.ContainsKey("@wordsaccount"))
                {
                    ht.Add("@wordsaccount", obj.Wordsaccount);
                }
            }
            if (obj.Reporterid > 0)//reporterid
            {
                term += " and a.reporterid=@reporterid ";
                if (!ht.ContainsKey("@reporterid"))
                {
                    ht.Add("@reporterid", obj.Reporterid);
                }
            }
            if (obj.Mediaitemid > 0)//mediaitemid
            {
                term += " and a.mediaitemid=@mediaitemid ";
                if (!ht.ContainsKey("@mediaitemid"))
                {
                    ht.Add("@mediaitemid", obj.Mediaitemid);
                }
            }
            if (obj.Mediaitemtypeid > 0)//MediaItemTypeID
            {
                term += " and a.mediaitemtypeid=@mediaitemtypeid ";
                if (!ht.ContainsKey("@mediaitemtypeid"))
                {
                    ht.Add("@mediaitemtypeid", obj.Mediaitemtypeid);
                }
            }
            if (obj.Des != null && obj.Des.Trim().Length > 0)
            {
                term += " and a.des=@des ";
                if (!ht.ContainsKey("@des"))
                {
                    ht.Add("@des", obj.Des);
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
            if (obj.Linkurl != null && obj.Linkurl.Trim().Length > 0)
            {
                term += " and a.linkurl=@linkurl ";
                if (!ht.ContainsKey("@linkurl"))
                {
                    ht.Add("@linkurl", obj.Linkurl);
                }
            }
            if (obj.Filepath != null && obj.Filepath.Trim().Length > 0)
            {
                term += " and a.filepath=@filepath ";
                if (!ht.ContainsKey("@filepath"))
                {
                    ht.Add("@filepath", obj.Filepath);
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
            if (obj.Paymentid > 0)//paymentid
            {
                term += " and a.paymentid=@paymentid ";
                if (!ht.ContainsKey("@paymentid"))
                {
                    ht.Add("@paymentid", obj.Paymentid);
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
            if (obj.Propagatetype > 0)//Propagatetype
            {
                term += " and a.propagatetype=@propagatetype ";
                if (!ht.ContainsKey("@propagatetype"))
                {
                    ht.Add("@propagatetype", obj.Propagatetype);
                }
            }
            if (obj.Progagateid > 0)//ProgagateID
            {
                term += " and a.progagateid=@progagateid ";
                if (!ht.ContainsKey("@progagateid"))
                {
                    ht.Add("@progagateid", obj.Progagateid);
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
            string sql = @"select {0} a.id as id,a.projectid as projectid,a.briefsubject as briefsubject,a.wordsaccount as wordsaccount,a.reporterid as reporterid,a.mediaitemid as mediaitemid,a.mediaitemtypeid as mediaitemtypeid,a.des as des,a.del as del,a.linkurl as linkurl,a.filepath as filepath,a.createdbyuserid as createdbyuserid,a.createdip as createdip,a.createddate as createddate,a.paymentid as paymentid,a.issuedate as issuedate,a.propagatetype as propagatetype,a.progagateid as progagateid {1} from media_projectbrief as a {2} where 1=1 {3} ";
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


        public static DataTable QueryInfoByObj(ProjectbriefInfo obj, string terms, Hashtable ht)
        {
            if (ht == null)
            {
                ht = new Hashtable();
            }
            SqlParameter[] para = ESP.Media.Access.Utilities.Common.DictToSqlParam(ht);
            return QueryInfoByObj(obj, terms, para);
        }


        public static DataTable QueryInfoByObj(ProjectbriefInfo obj, string terms, params SqlParameter[] param)
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
        public static ProjectbriefInfo Load(int id)
        {
            DataTable dt = null;
            dt = QueryInfo(" and a.id=@id ", new SqlParameter("@id", id));
            if (dt != null && dt.Rows.Count > 0)
            {
                return setObject(dt.Rows[0]);
            }
            return null;
        }


        public static ProjectbriefInfo Load(int id, SqlTransaction trans)
        {
            DataTable dt = null;
            dt = QueryInfo(trans, " and a.id=@id ", new SqlParameter("@id", id));
            if (dt != null && dt.Rows.Count > 0)
            {
                return setObject(dt.Rows[0]);
            }
            return null;
        }


        public static ProjectbriefInfo setObject(DataRow dr)
        {
            ProjectbriefInfo obj = new ProjectbriefInfo();
            if (dr.Table.Columns.Contains("id") && dr["id"] != DBNull.Value)//id
            {
                obj.Id = Convert.ToInt32(dr["id"]);
            }
            if (dr.Table.Columns.Contains("projectid") && dr["projectid"] != DBNull.Value)//项目ID
            {
                obj.Projectid = Convert.ToInt32(dr["projectid"]);
            }
            if (dr.Table.Columns.Contains("briefsubject") && dr["briefsubject"] != DBNull.Value)//briefsubject
            {
                obj.Briefsubject = (dr["briefsubject"]).ToString();
            }
            if (dr.Table.Columns.Contains("wordsaccount") && dr["wordsaccount"] != DBNull.Value)//wordsaccount
            {
                obj.Wordsaccount = Convert.ToInt32(dr["wordsaccount"]);
            }
            if (dr.Table.Columns.Contains("reporterid") && dr["reporterid"] != DBNull.Value)//reporterid
            {
                obj.Reporterid = Convert.ToInt32(dr["reporterid"]);
            }
            if (dr.Table.Columns.Contains("mediaitemid") && dr["mediaitemid"] != DBNull.Value)//mediaitemid
            {
                obj.Mediaitemid = Convert.ToInt32(dr["mediaitemid"]);
            }
            if (dr.Table.Columns.Contains("mediaitemtypeid") && dr["mediaitemtypeid"] != DBNull.Value)//媒体类型id
            {
                obj.Mediaitemtypeid = Convert.ToInt32(dr["mediaitemtypeid"]);
            }
            if (dr.Table.Columns.Contains("des") && dr["des"] != DBNull.Value)//说明
            {
                obj.Des = (dr["des"]).ToString();
            }
            if (dr.Table.Columns.Contains("del") && dr["del"] != DBNull.Value)//删除标记
            {
                obj.Del = Convert.ToInt32(dr["del"]);
            }
            if (dr.Table.Columns.Contains("linkurl") && dr["linkurl"] != DBNull.Value)//链接地址
            {
                obj.Linkurl = (dr["linkurl"]).ToString();
            }
            if (dr.Table.Columns.Contains("filepath") && dr["filepath"] != DBNull.Value)//文件路径
            {
                obj.Filepath = (dr["filepath"]).ToString();
            }
            if (dr.Table.Columns.Contains("createdbyuserid") && dr["createdbyuserid"] != DBNull.Value)//创建人
            {
                obj.Createdbyuserid = Convert.ToInt32(dr["createdbyuserid"]);
            }
            if (dr.Table.Columns.Contains("createdip") && dr["createdip"] != DBNull.Value)//创建人ip
            {
                obj.Createdip = (dr["createdip"]).ToString();
            }
            if (dr.Table.Columns.Contains("createddate") && dr["createddate"] != DBNull.Value)//创建日期
            {
                obj.Createddate = (dr["createddate"]).ToString();
            }
            if (dr.Table.Columns.Contains("paymentid") && dr["paymentid"] != DBNull.Value)//paymentid
            {
                obj.Paymentid = Convert.ToInt32(dr["paymentid"]);
            }
            if (dr.Table.Columns.Contains("issuedate") && dr["issuedate"] != DBNull.Value)//刊出日期
            {
                obj.Issuedate = (dr["issuedate"]).ToString();
            }
            if (dr.Table.Columns.Contains("propagatetype") && dr["propagatetype"] != DBNull.Value)//传播类型
            {
                obj.Propagatetype = Convert.ToInt32(dr["propagatetype"]);
            }
            if (dr.Table.Columns.Contains("progagateid") && dr["progagateid"] != DBNull.Value)//progagateid
            {
                obj.Progagateid = Convert.ToInt32(dr["progagateid"]);
            }
            return obj;
        }
        #endregion
    }
}
