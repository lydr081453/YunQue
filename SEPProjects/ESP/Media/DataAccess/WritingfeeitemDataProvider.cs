using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.Collections.Generic;
using ESP.Media.Access.Utilities;
using ESP.Media.Entity;
namespace ESP.Media.DataAccess
{
    public class WritingfeeitemDataProvider
    {
        #region 构造函数
        public WritingfeeitemDataProvider()
        {
        }
        #endregion
        #region 插入
        //插入字符串
        private static string strinsert(WritingfeeitemInfo obj, ref List<SqlParameter> ht)
        {
            if (ht == null)
            {
                ht = new List<SqlParameter>();
            }
            string sql = @"insert into media_writingfeeitem (writingfeebillid,projectid,projectcode,happendate,userid,username,userdepartmentid,userdepartmentname,mediaid,medianame,writingsubject,issuedate,wordscount,areawordscount,unitprice,amountprice,subtotal,linkmanid,linkmanname,recvmanname,cityid,cityname,bankname,bankaccount,bankcardcode,idcardcode,phoneno,del,propagatetype,propagateid,propagatename,uploadstartdate,uploadenddate,paytype) values (@writingfeebillid,@projectid,@projectcode,@happendate,@userid,@username,@userdepartmentid,@userdepartmentname,@mediaid,@medianame,@writingsubject,@issuedate,@wordscount,@areawordscount,@unitprice,@amountprice,@subtotal,@linkmanid,@linkmanname,@recvmanname,@cityid,@cityname,@bankname,@bankaccount,@bankcardcode,@idcardcode,@phoneno,@del,@propagatetype,@propagateid,@propagatename,@uploadstartdate,@uploadenddate,@paytype);select @@IDENTITY as rowNum;";
            SqlParameter param_Writingfeebillid = new SqlParameter("@Writingfeebillid", SqlDbType.Int, 4);
            param_Writingfeebillid.Value = obj.Writingfeebillid;
            ht.Add(param_Writingfeebillid);
            SqlParameter param_Projectid = new SqlParameter("@Projectid", SqlDbType.Int, 4);
            param_Projectid.Value = obj.Projectid;
            ht.Add(param_Projectid);
            SqlParameter param_Projectcode = new SqlParameter("@Projectcode", SqlDbType.NVarChar, 100);
            param_Projectcode.Value = obj.Projectcode;
            ht.Add(param_Projectcode);
            SqlParameter param_Happendate = new SqlParameter("@Happendate", SqlDbType.DateTime, 8);
            param_Happendate.Value = ESP.Media.Access.Utilities.Common.StringToDateTime(obj.Happendate);
            ht.Add(param_Happendate);
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
            SqlParameter param_Mediaid = new SqlParameter("@Mediaid", SqlDbType.Int, 4);
            param_Mediaid.Value = obj.Mediaid;
            ht.Add(param_Mediaid);
            SqlParameter param_Medianame = new SqlParameter("@Medianame", SqlDbType.NVarChar, 100);
            param_Medianame.Value = obj.Medianame;
            ht.Add(param_Medianame);
            SqlParameter param_Writingsubject = new SqlParameter("@Writingsubject", SqlDbType.NVarChar, 100);
            param_Writingsubject.Value = obj.Writingsubject;
            ht.Add(param_Writingsubject);
            SqlParameter param_Issuedate = new SqlParameter("@Issuedate", SqlDbType.DateTime, 8);
            param_Issuedate.Value = ESP.Media.Access.Utilities.Common.StringToDateTime(obj.Issuedate);
            ht.Add(param_Issuedate);
            SqlParameter param_Wordscount = new SqlParameter("@Wordscount", SqlDbType.Int, 4);
            param_Wordscount.Value = obj.Wordscount;
            ht.Add(param_Wordscount);
            SqlParameter param_Areawordscount = new SqlParameter("@Areawordscount", SqlDbType.Int, 4);
            param_Areawordscount.Value = obj.Areawordscount;
            ht.Add(param_Areawordscount);
            SqlParameter param_Unitprice = new SqlParameter("@Unitprice", SqlDbType.Float, 8);
            param_Unitprice.Value = obj.Unitprice;
            ht.Add(param_Unitprice);
            SqlParameter param_Amountprice = new SqlParameter("@Amountprice", SqlDbType.Float, 8);
            param_Amountprice.Value = obj.Amountprice;
            ht.Add(param_Amountprice);
            SqlParameter param_Subtotal = new SqlParameter("@Subtotal", SqlDbType.Float, 8);
            param_Subtotal.Value = obj.Subtotal;
            ht.Add(param_Subtotal);
            SqlParameter param_Linkmanid = new SqlParameter("@Linkmanid", SqlDbType.Int, 4);
            param_Linkmanid.Value = obj.Linkmanid;
            ht.Add(param_Linkmanid);
            SqlParameter param_Linkmanname = new SqlParameter("@Linkmanname", SqlDbType.NVarChar, 100);
            param_Linkmanname.Value = obj.Linkmanname;
            ht.Add(param_Linkmanname);
            SqlParameter param_Recvmanname = new SqlParameter("@Recvmanname", SqlDbType.NVarChar, 100);
            param_Recvmanname.Value = obj.Recvmanname;
            ht.Add(param_Recvmanname);
            SqlParameter param_Cityid = new SqlParameter("@Cityid", SqlDbType.Int, 4);
            param_Cityid.Value = obj.Cityid;
            ht.Add(param_Cityid);
            SqlParameter param_Cityname = new SqlParameter("@Cityname", SqlDbType.NVarChar, 100);
            param_Cityname.Value = obj.Cityname;
            ht.Add(param_Cityname);
            SqlParameter param_Bankname = new SqlParameter("@Bankname", SqlDbType.NVarChar, 1000);
            param_Bankname.Value = obj.Bankname;
            ht.Add(param_Bankname);
            SqlParameter param_Bankaccount = new SqlParameter("@Bankaccount", SqlDbType.NVarChar, 100);
            param_Bankaccount.Value = obj.Bankaccount;
            ht.Add(param_Bankaccount);
            SqlParameter param_Bankcardcode = new SqlParameter("@Bankcardcode", SqlDbType.NVarChar, 100);
            param_Bankcardcode.Value = obj.Bankcardcode;
            ht.Add(param_Bankcardcode);
            SqlParameter param_Idcardcode = new SqlParameter("@Idcardcode", SqlDbType.NVarChar, 100);
            param_Idcardcode.Value = obj.Idcardcode;
            ht.Add(param_Idcardcode);
            SqlParameter param_Phoneno = new SqlParameter("@Phoneno", SqlDbType.NVarChar, 100);
            param_Phoneno.Value = obj.Phoneno;
            ht.Add(param_Phoneno);
            SqlParameter param_Del = new SqlParameter("@Del", SqlDbType.Int, 4);
            param_Del.Value = obj.Del;
            ht.Add(param_Del);
            SqlParameter param_Propagatetype = new SqlParameter("@Propagatetype", SqlDbType.Int, 4);
            param_Propagatetype.Value = obj.Propagatetype;
            ht.Add(param_Propagatetype);
            SqlParameter param_Propagateid = new SqlParameter("@Propagateid", SqlDbType.Int, 4);
            param_Propagateid.Value = obj.Propagateid;
            ht.Add(param_Propagateid);
            SqlParameter param_Propagatename = new SqlParameter("@Propagatename", SqlDbType.NVarChar, 100);
            param_Propagatename.Value = obj.Propagatename;
            ht.Add(param_Propagatename);
            SqlParameter param_Uploadstartdate = new SqlParameter("@Uploadstartdate", SqlDbType.DateTime, 8);
            param_Uploadstartdate.Value = ESP.Media.Access.Utilities.Common.StringToDateTime(obj.Uploadstartdate);
            ht.Add(param_Uploadstartdate);
            SqlParameter param_Uploadenddate = new SqlParameter("@Uploadenddate", SqlDbType.DateTime, 8);
            param_Uploadenddate.Value = ESP.Media.Access.Utilities.Common.StringToDateTime(obj.Uploadenddate);
            ht.Add(param_Uploadenddate);
            SqlParameter param_Paytype = new SqlParameter("@Paytype", SqlDbType.Int, 4);
            param_Paytype.Value = obj.Paytype;
            ht.Add(param_Paytype);
            return sql;
        }


        //插入一条记录
        public static int insertinfo(WritingfeeitemInfo obj, SqlTransaction trans)
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
        public static int insertinfo(WritingfeeitemInfo obj)
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
            string sql = "delete media_writingfeeitem where WritingFeeitemid=@id";
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
        public static string getUpdateString(WritingfeeitemInfo objTerm, WritingfeeitemInfo Objupdate, string term, ref List<SqlParameter> ht, params SqlParameter[] param)
        {
            string sql = "update media_writingfeeitem set writingfeebillid=@writingfeebillid,projectid=@projectid,projectcode=@projectcode,happendate=@happendate,userid=@userid,username=@username,userdepartmentid=@userdepartmentid,userdepartmentname=@userdepartmentname,mediaid=@mediaid,medianame=@medianame,writingsubject=@writingsubject,issuedate=@issuedate,wordscount=@wordscount,areawordscount=@areawordscount,unitprice=@unitprice,amountprice=@amountprice,subtotal=@subtotal,linkmanid=@linkmanid,linkmanname=@linkmanname,recvmanname=@recvmanname,cityid=@cityid,cityname=@cityname,bankname=@bankname,bankaccount=@bankaccount,bankcardcode=@bankcardcode,idcardcode=@idcardcode,phoneno=@phoneno,del=@del,propagatetype=@propagatetype,propagateid=@propagateid,propagatename=@propagatename,uploadstartdate=@uploadstartdate,uploadenddate=@uploadenddate,paytype=@paytype where 1=1 ";
            SqlParameter param_writingfeebillid = new SqlParameter("@writingfeebillid", SqlDbType.Int, 4);
            param_writingfeebillid.Value = Objupdate.Writingfeebillid;
            ht.Add(param_writingfeebillid);
            SqlParameter param_projectid = new SqlParameter("@projectid", SqlDbType.Int, 4);
            param_projectid.Value = Objupdate.Projectid;
            ht.Add(param_projectid);
            SqlParameter param_projectcode = new SqlParameter("@projectcode", SqlDbType.NVarChar, 100);
            param_projectcode.Value = Objupdate.Projectcode;
            ht.Add(param_projectcode);
            SqlParameter param_happendate = new SqlParameter("@happendate", SqlDbType.DateTime, 8);
            param_happendate.Value = ESP.Media.Access.Utilities.Common.StringToDateTime(Objupdate.Happendate);
            ht.Add(param_happendate);
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
            SqlParameter param_mediaid = new SqlParameter("@mediaid", SqlDbType.Int, 4);
            param_mediaid.Value = Objupdate.Mediaid;
            ht.Add(param_mediaid);
            SqlParameter param_medianame = new SqlParameter("@medianame", SqlDbType.NVarChar, 100);
            param_medianame.Value = Objupdate.Medianame;
            ht.Add(param_medianame);
            SqlParameter param_writingsubject = new SqlParameter("@writingsubject", SqlDbType.NVarChar, 100);
            param_writingsubject.Value = Objupdate.Writingsubject;
            ht.Add(param_writingsubject);
            SqlParameter param_issuedate = new SqlParameter("@issuedate", SqlDbType.DateTime, 8);
            param_issuedate.Value = ESP.Media.Access.Utilities.Common.StringToDateTime(Objupdate.Issuedate);
            ht.Add(param_issuedate);
            SqlParameter param_wordscount = new SqlParameter("@wordscount", SqlDbType.Int, 4);
            param_wordscount.Value = Objupdate.Wordscount;
            ht.Add(param_wordscount);
            SqlParameter param_areawordscount = new SqlParameter("@areawordscount", SqlDbType.Int, 4);
            param_areawordscount.Value = Objupdate.Areawordscount;
            ht.Add(param_areawordscount);
            SqlParameter param_unitprice = new SqlParameter("@unitprice", SqlDbType.Float, 8);
            param_unitprice.Value = Objupdate.Unitprice;
            ht.Add(param_unitprice);
            SqlParameter param_amountprice = new SqlParameter("@amountprice", SqlDbType.Float, 8);
            param_amountprice.Value = Objupdate.Amountprice;
            ht.Add(param_amountprice);
            SqlParameter param_subtotal = new SqlParameter("@subtotal", SqlDbType.Float, 8);
            param_subtotal.Value = Objupdate.Subtotal;
            ht.Add(param_subtotal);
            SqlParameter param_linkmanid = new SqlParameter("@linkmanid", SqlDbType.Int, 4);
            param_linkmanid.Value = Objupdate.Linkmanid;
            ht.Add(param_linkmanid);
            SqlParameter param_linkmanname = new SqlParameter("@linkmanname", SqlDbType.NVarChar, 100);
            param_linkmanname.Value = Objupdate.Linkmanname;
            ht.Add(param_linkmanname);
            SqlParameter param_recvmanname = new SqlParameter("@recvmanname", SqlDbType.NVarChar, 100);
            param_recvmanname.Value = Objupdate.Recvmanname;
            ht.Add(param_recvmanname);
            SqlParameter param_cityid = new SqlParameter("@cityid", SqlDbType.Int, 4);
            param_cityid.Value = Objupdate.Cityid;
            ht.Add(param_cityid);
            SqlParameter param_cityname = new SqlParameter("@cityname", SqlDbType.NVarChar, 100);
            param_cityname.Value = Objupdate.Cityname;
            ht.Add(param_cityname);
            SqlParameter param_bankname = new SqlParameter("@bankname", SqlDbType.NVarChar, 1000);
            param_bankname.Value = Objupdate.Bankname;
            ht.Add(param_bankname);
            SqlParameter param_bankaccount = new SqlParameter("@bankaccount", SqlDbType.NVarChar, 100);
            param_bankaccount.Value = Objupdate.Bankaccount;
            ht.Add(param_bankaccount);
            SqlParameter param_bankcardcode = new SqlParameter("@bankcardcode", SqlDbType.NVarChar, 100);
            param_bankcardcode.Value = Objupdate.Bankcardcode;
            ht.Add(param_bankcardcode);
            SqlParameter param_idcardcode = new SqlParameter("@idcardcode", SqlDbType.NVarChar, 100);
            param_idcardcode.Value = Objupdate.Idcardcode;
            ht.Add(param_idcardcode);
            SqlParameter param_phoneno = new SqlParameter("@phoneno", SqlDbType.NVarChar, 100);
            param_phoneno.Value = Objupdate.Phoneno;
            ht.Add(param_phoneno);
            SqlParameter param_del = new SqlParameter("@del", SqlDbType.Int, 4);
            param_del.Value = Objupdate.Del;
            ht.Add(param_del);
            SqlParameter param_propagatetype = new SqlParameter("@propagatetype", SqlDbType.Int, 4);
            param_propagatetype.Value = Objupdate.Propagatetype;
            ht.Add(param_propagatetype);
            SqlParameter param_propagateid = new SqlParameter("@propagateid", SqlDbType.Int, 4);
            param_propagateid.Value = Objupdate.Propagateid;
            ht.Add(param_propagateid);
            SqlParameter param_propagatename = new SqlParameter("@propagatename", SqlDbType.NVarChar, 100);
            param_propagatename.Value = Objupdate.Propagatename;
            ht.Add(param_propagatename);
            SqlParameter param_uploadstartdate = new SqlParameter("@uploadstartdate", SqlDbType.DateTime, 8);
            param_uploadstartdate.Value = ESP.Media.Access.Utilities.Common.StringToDateTime(Objupdate.Uploadstartdate);
            ht.Add(param_uploadstartdate);
            SqlParameter param_uploadenddate = new SqlParameter("@uploadenddate", SqlDbType.DateTime, 8);
            param_uploadenddate.Value = ESP.Media.Access.Utilities.Common.StringToDateTime(Objupdate.Uploadenddate);
            ht.Add(param_uploadenddate);
            SqlParameter param_paytype = new SqlParameter("@paytype", SqlDbType.Int, 4);
            param_paytype.Value = Objupdate.Paytype;
            ht.Add(param_paytype);
            if (objTerm == null && (term == null || term.Trim().Length <= 0))
            {
                sql += " and WritingFeeitemid=@id ";
                if (ESP.Media.Access.Utilities.Common.Find(ht, "@id") == -1)
                {
                    SqlParameter p = new SqlParameter("@id", SqlDbType.Int, 4);
                    p.Value = Objupdate.Writingfeeitemid;
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
        public static bool updateInfo(SqlTransaction trans, WritingfeeitemInfo objterm, WritingfeeitemInfo Objupdate, string term, params SqlParameter[] param)
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
        public static bool updateInfo(WritingfeeitemInfo objterm, WritingfeeitemInfo Objupdate, string term, params SqlParameter[] param)
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


        private static string getTerms(WritingfeeitemInfo obj, ref List<SqlParameter> ht)
        {
            string term = string.Empty;
            if (obj.Writingfeeitemid > 0)//WritingFeeitemid
            {
                term += " and writingfeeitemid=@writingfeeitemid ";
                if (ESP.Media.Access.Utilities.Common.Find(ht, "@writingfeeitemid") == -1)
                {
                    SqlParameter p = new SqlParameter("@writingfeeitemid", SqlDbType.Int, 4);
                    p.Value = obj.Writingfeeitemid;
                    ht.Add(p);
                }
            }
            if (obj.Writingfeebillid > 0)//WritingFeeBillID
            {
                term += " and writingfeebillid=@writingfeebillid ";
                if (ESP.Media.Access.Utilities.Common.Find(ht, "@writingfeebillid") == -1)
                {
                    SqlParameter p = new SqlParameter("@writingfeebillid", SqlDbType.Int, 4);
                    p.Value = obj.Writingfeebillid;
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
            if (obj.Happendate != null && obj.Happendate.Trim().Length > 0)
            {
                term += " and happendate=@happendate ";
                if (ESP.Media.Access.Utilities.Common.Find(ht, "@happendate") == -1)
                {
                    SqlParameter p = new SqlParameter("@happendate", SqlDbType.DateTime, 8);
                    p.Value = ESP.Media.Access.Utilities.Common.StringToDateTime(obj.Happendate);
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
            if (obj.Mediaid > 0)//mediaid
            {
                term += " and mediaid=@mediaid ";
                if (ESP.Media.Access.Utilities.Common.Find(ht, "@mediaid") == -1)
                {
                    SqlParameter p = new SqlParameter("@mediaid", SqlDbType.Int, 4);
                    p.Value = obj.Mediaid;
                    ht.Add(p);
                }
            }
            if (obj.Medianame != null && obj.Medianame.Trim().Length > 0)
            {
                term += " and medianame=@medianame ";
                if (ESP.Media.Access.Utilities.Common.Find(ht, "@medianame") == -1)
                {
                    SqlParameter p = new SqlParameter("@medianame", SqlDbType.NVarChar, 100);
                    p.Value = obj.Medianame;
                    ht.Add(p);
                }
            }
            if (obj.Writingsubject != null && obj.Writingsubject.Trim().Length > 0)
            {
                term += " and writingsubject=@writingsubject ";
                if (ESP.Media.Access.Utilities.Common.Find(ht, "@writingsubject") == -1)
                {
                    SqlParameter p = new SqlParameter("@writingsubject", SqlDbType.NVarChar, 100);
                    p.Value = obj.Writingsubject;
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
            if (obj.Wordscount > 0)//wordscount
            {
                term += " and wordscount=@wordscount ";
                if (ESP.Media.Access.Utilities.Common.Find(ht, "@wordscount") == -1)
                {
                    SqlParameter p = new SqlParameter("@wordscount", SqlDbType.Int, 4);
                    p.Value = obj.Wordscount;
                    ht.Add(p);
                }
            }
            if (obj.Areawordscount > 0)//areawordscount
            {
                term += " and areawordscount=@areawordscount ";
                if (ESP.Media.Access.Utilities.Common.Find(ht, "@areawordscount") == -1)
                {
                    SqlParameter p = new SqlParameter("@areawordscount", SqlDbType.Int, 4);
                    p.Value = obj.Areawordscount;
                    ht.Add(p);
                }
            }
            if (obj.Unitprice > 0)//unitprice
            {
                term += " and unitprice=@unitprice ";
                if (ESP.Media.Access.Utilities.Common.Find(ht, "@unitprice") == -1)
                {
                    SqlParameter p = new SqlParameter("@unitprice", SqlDbType.Float, 8);
                    p.Value = obj.Unitprice;
                    ht.Add(p);
                }
            }
            if (obj.Amountprice > 0)//amountprice
            {
                term += " and amountprice=@amountprice ";
                if (ESP.Media.Access.Utilities.Common.Find(ht, "@amountprice") == -1)
                {
                    SqlParameter p = new SqlParameter("@amountprice", SqlDbType.Float, 8);
                    p.Value = obj.Amountprice;
                    ht.Add(p);
                }
            }
            if (obj.Subtotal > 0)//subtotal
            {
                term += " and subtotal=@subtotal ";
                if (ESP.Media.Access.Utilities.Common.Find(ht, "@subtotal") == -1)
                {
                    SqlParameter p = new SqlParameter("@subtotal", SqlDbType.Float, 8);
                    p.Value = obj.Subtotal;
                    ht.Add(p);
                }
            }
            if (obj.Linkmanid > 0)//linkmanid
            {
                term += " and linkmanid=@linkmanid ";
                if (ESP.Media.Access.Utilities.Common.Find(ht, "@linkmanid") == -1)
                {
                    SqlParameter p = new SqlParameter("@linkmanid", SqlDbType.Int, 4);
                    p.Value = obj.Linkmanid;
                    ht.Add(p);
                }
            }
            if (obj.Linkmanname != null && obj.Linkmanname.Trim().Length > 0)
            {
                term += " and linkmanname=@linkmanname ";
                if (ESP.Media.Access.Utilities.Common.Find(ht, "@linkmanname") == -1)
                {
                    SqlParameter p = new SqlParameter("@linkmanname", SqlDbType.NVarChar, 100);
                    p.Value = obj.Linkmanname;
                    ht.Add(p);
                }
            }
            if (obj.Recvmanname != null && obj.Recvmanname.Trim().Length > 0)
            {
                term += " and recvmanname=@recvmanname ";
                if (ESP.Media.Access.Utilities.Common.Find(ht, "@recvmanname") == -1)
                {
                    SqlParameter p = new SqlParameter("@recvmanname", SqlDbType.NVarChar, 100);
                    p.Value = obj.Recvmanname;
                    ht.Add(p);
                }
            }
            if (obj.Cityid > 0)//cityid
            {
                term += " and cityid=@cityid ";
                if (ESP.Media.Access.Utilities.Common.Find(ht, "@cityid") == -1)
                {
                    SqlParameter p = new SqlParameter("@cityid", SqlDbType.Int, 4);
                    p.Value = obj.Cityid;
                    ht.Add(p);
                }
            }
            if (obj.Cityname != null && obj.Cityname.Trim().Length > 0)
            {
                term += " and cityname=@cityname ";
                if (ESP.Media.Access.Utilities.Common.Find(ht, "@cityname") == -1)
                {
                    SqlParameter p = new SqlParameter("@cityname", SqlDbType.NVarChar, 100);
                    p.Value = obj.Cityname;
                    ht.Add(p);
                }
            }
            if (obj.Bankname != null && obj.Bankname.Trim().Length > 0)
            {
                term += " and bankname=@bankname ";
                if (ESP.Media.Access.Utilities.Common.Find(ht, "@bankname") == -1)
                {
                    SqlParameter p = new SqlParameter("@bankname", SqlDbType.NVarChar, 1000);
                    p.Value = obj.Bankname;
                    ht.Add(p);
                }
            }
            if (obj.Bankaccount != null && obj.Bankaccount.Trim().Length > 0)
            {
                term += " and bankaccount=@bankaccount ";
                if (ESP.Media.Access.Utilities.Common.Find(ht, "@bankaccount") == -1)
                {
                    SqlParameter p = new SqlParameter("@bankaccount", SqlDbType.NVarChar, 100);
                    p.Value = obj.Bankaccount;
                    ht.Add(p);
                }
            }
            if (obj.Bankcardcode != null && obj.Bankcardcode.Trim().Length > 0)
            {
                term += " and bankcardcode=@bankcardcode ";
                if (ESP.Media.Access.Utilities.Common.Find(ht, "@bankcardcode") == -1)
                {
                    SqlParameter p = new SqlParameter("@bankcardcode", SqlDbType.NVarChar, 100);
                    p.Value = obj.Bankcardcode;
                    ht.Add(p);
                }
            }
            if (obj.Idcardcode != null && obj.Idcardcode.Trim().Length > 0)
            {
                term += " and idcardcode=@idcardcode ";
                if (ESP.Media.Access.Utilities.Common.Find(ht, "@idcardcode") == -1)
                {
                    SqlParameter p = new SqlParameter("@idcardcode", SqlDbType.NVarChar, 100);
                    p.Value = obj.Idcardcode;
                    ht.Add(p);
                }
            }
            if (obj.Phoneno != null && obj.Phoneno.Trim().Length > 0)
            {
                term += " and phoneno=@phoneno ";
                if (ESP.Media.Access.Utilities.Common.Find(ht, "@phoneno") == -1)
                {
                    SqlParameter p = new SqlParameter("@phoneno", SqlDbType.NVarChar, 100);
                    p.Value = obj.Phoneno;
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
            if (obj.Propagatetype > 0)//PropagateType
            {
                term += " and propagatetype=@propagatetype ";
                if (ESP.Media.Access.Utilities.Common.Find(ht, "@propagatetype") == -1)
                {
                    SqlParameter p = new SqlParameter("@propagatetype", SqlDbType.Int, 4);
                    p.Value = obj.Propagatetype;
                    ht.Add(p);
                }
            }
            if (obj.Propagateid > 0)//PropagateID
            {
                term += " and propagateid=@propagateid ";
                if (ESP.Media.Access.Utilities.Common.Find(ht, "@propagateid") == -1)
                {
                    SqlParameter p = new SqlParameter("@propagateid", SqlDbType.Int, 4);
                    p.Value = obj.Propagateid;
                    ht.Add(p);
                }
            }
            if (obj.Propagatename != null && obj.Propagatename.Trim().Length > 0)
            {
                term += " and propagatename=@propagatename ";
                if (ESP.Media.Access.Utilities.Common.Find(ht, "@propagatename") == -1)
                {
                    SqlParameter p = new SqlParameter("@propagatename", SqlDbType.NVarChar, 100);
                    p.Value = obj.Propagatename;
                    ht.Add(p);
                }
            }
            if (obj.Uploadstartdate != null && obj.Uploadstartdate.Trim().Length > 0)
            {
                term += " and uploadstartdate=@uploadstartdate ";
                if (ESP.Media.Access.Utilities.Common.Find(ht, "@uploadstartdate") == -1)
                {
                    SqlParameter p = new SqlParameter("@uploadstartdate", SqlDbType.DateTime, 8);
                    p.Value = ESP.Media.Access.Utilities.Common.StringToDateTime(obj.Uploadstartdate);
                    ht.Add(p);
                }
            }
            if (obj.Uploadenddate != null && obj.Uploadenddate.Trim().Length > 0)
            {
                term += " and uploadenddate=@uploadenddate ";
                if (ESP.Media.Access.Utilities.Common.Find(ht, "@uploadenddate") == -1)
                {
                    SqlParameter p = new SqlParameter("@uploadenddate", SqlDbType.DateTime, 8);
                    p.Value = ESP.Media.Access.Utilities.Common.StringToDateTime(obj.Uploadenddate);
                    ht.Add(p);
                }
            }
            if (obj.Paytype > 0)//支付类型,刊前\刊后
            {
                term += " and paytype=@paytype ";
                if (ESP.Media.Access.Utilities.Common.Find(ht, "@paytype") == -1)
                {
                    SqlParameter p = new SqlParameter("@paytype", SqlDbType.Int, 4);
                    p.Value = obj.Paytype;
                    ht.Add(p);
                }
            }
            return term;
        }
        #endregion
        #region 查询
        private static string getQueryTerms(WritingfeeitemInfo obj, ref Hashtable ht)
        {
            string term = string.Empty;
            if (obj.Writingfeeitemid > 0)//WritingFeeitemid
            {
                term += " and a.writingfeeitemid=@writingfeeitemid ";
                if (!ht.ContainsKey("@writingfeeitemid"))
                {
                    ht.Add("@writingfeeitemid", obj.Writingfeeitemid);
                }
            }
            if (obj.Writingfeebillid > 0)//WritingFeeBillID
            {
                term += " and a.writingfeebillid=@writingfeebillid ";
                if (!ht.ContainsKey("@writingfeebillid"))
                {
                    ht.Add("@writingfeebillid", obj.Writingfeebillid);
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
            if (obj.Happendate != null && obj.Happendate.Trim().Length > 0)
            {
                term += " and a.happendate=@happendate ";
                if (!ht.ContainsKey("@happendate"))
                {
                    ht.Add("@happendate", obj.Happendate);
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
            if (obj.Mediaid > 0)//mediaid
            {
                term += " and a.mediaid=@mediaid ";
                if (!ht.ContainsKey("@mediaid"))
                {
                    ht.Add("@mediaid", obj.Mediaid);
                }
            }
            if (obj.Medianame != null && obj.Medianame.Trim().Length > 0)
            {
                term += " and a.medianame=@medianame ";
                if (!ht.ContainsKey("@medianame"))
                {
                    ht.Add("@medianame", obj.Medianame);
                }
            }
            if (obj.Writingsubject != null && obj.Writingsubject.Trim().Length > 0)
            {
                term += " and a.writingsubject=@writingsubject ";
                if (!ht.ContainsKey("@writingsubject"))
                {
                    ht.Add("@writingsubject", obj.Writingsubject);
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
            if (obj.Wordscount > 0)//wordscount
            {
                term += " and a.wordscount=@wordscount ";
                if (!ht.ContainsKey("@wordscount"))
                {
                    ht.Add("@wordscount", obj.Wordscount);
                }
            }
            if (obj.Areawordscount > 0)//areawordscount
            {
                term += " and a.areawordscount=@areawordscount ";
                if (!ht.ContainsKey("@areawordscount"))
                {
                    ht.Add("@areawordscount", obj.Areawordscount);
                }
            }
            if (obj.Unitprice > 0)//unitprice
            {
                term += " and a.unitprice=@unitprice ";
                if (!ht.ContainsKey("@unitprice"))
                {
                    ht.Add("@unitprice", obj.Unitprice);
                }
            }
            if (obj.Amountprice > 0)//amountprice
            {
                term += " and a.amountprice=@amountprice ";
                if (!ht.ContainsKey("@amountprice"))
                {
                    ht.Add("@amountprice", obj.Amountprice);
                }
            }
            if (obj.Subtotal > 0)//subtotal
            {
                term += " and a.subtotal=@subtotal ";
                if (!ht.ContainsKey("@subtotal"))
                {
                    ht.Add("@subtotal", obj.Subtotal);
                }
            }
            if (obj.Linkmanid > 0)//linkmanid
            {
                term += " and a.linkmanid=@linkmanid ";
                if (!ht.ContainsKey("@linkmanid"))
                {
                    ht.Add("@linkmanid", obj.Linkmanid);
                }
            }
            if (obj.Linkmanname != null && obj.Linkmanname.Trim().Length > 0)
            {
                term += " and a.linkmanname=@linkmanname ";
                if (!ht.ContainsKey("@linkmanname"))
                {
                    ht.Add("@linkmanname", obj.Linkmanname);
                }
            }
            if (obj.Recvmanname != null && obj.Recvmanname.Trim().Length > 0)
            {
                term += " and a.recvmanname=@recvmanname ";
                if (!ht.ContainsKey("@recvmanname"))
                {
                    ht.Add("@recvmanname", obj.Recvmanname);
                }
            }
            if (obj.Cityid > 0)//cityid
            {
                term += " and a.cityid=@cityid ";
                if (!ht.ContainsKey("@cityid"))
                {
                    ht.Add("@cityid", obj.Cityid);
                }
            }
            if (obj.Cityname != null && obj.Cityname.Trim().Length > 0)
            {
                term += " and a.cityname=@cityname ";
                if (!ht.ContainsKey("@cityname"))
                {
                    ht.Add("@cityname", obj.Cityname);
                }
            }
            if (obj.Bankname != null && obj.Bankname.Trim().Length > 0)
            {
                term += " and a.bankname=@bankname ";
                if (!ht.ContainsKey("@bankname"))
                {
                    ht.Add("@bankname", obj.Bankname);
                }
            }
            if (obj.Bankaccount != null && obj.Bankaccount.Trim().Length > 0)
            {
                term += " and a.bankaccount=@bankaccount ";
                if (!ht.ContainsKey("@bankaccount"))
                {
                    ht.Add("@bankaccount", obj.Bankaccount);
                }
            }
            if (obj.Bankcardcode != null && obj.Bankcardcode.Trim().Length > 0)
            {
                term += " and a.bankcardcode=@bankcardcode ";
                if (!ht.ContainsKey("@bankcardcode"))
                {
                    ht.Add("@bankcardcode", obj.Bankcardcode);
                }
            }
            if (obj.Idcardcode != null && obj.Idcardcode.Trim().Length > 0)
            {
                term += " and a.idcardcode=@idcardcode ";
                if (!ht.ContainsKey("@idcardcode"))
                {
                    ht.Add("@idcardcode", obj.Idcardcode);
                }
            }
            if (obj.Phoneno != null && obj.Phoneno.Trim().Length > 0)
            {
                term += " and a.phoneno=@phoneno ";
                if (!ht.ContainsKey("@phoneno"))
                {
                    ht.Add("@phoneno", obj.Phoneno);
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
            if (obj.Propagatetype > 0)//PropagateType
            {
                term += " and a.propagatetype=@propagatetype ";
                if (!ht.ContainsKey("@propagatetype"))
                {
                    ht.Add("@propagatetype", obj.Propagatetype);
                }
            }
            if (obj.Propagateid > 0)//PropagateID
            {
                term += " and a.propagateid=@propagateid ";
                if (!ht.ContainsKey("@propagateid"))
                {
                    ht.Add("@propagateid", obj.Propagateid);
                }
            }
            if (obj.Propagatename != null && obj.Propagatename.Trim().Length > 0)
            {
                term += " and a.propagatename=@propagatename ";
                if (!ht.ContainsKey("@propagatename"))
                {
                    ht.Add("@propagatename", obj.Propagatename);
                }
            }
            if (obj.Uploadstartdate != null && obj.Uploadstartdate.Trim().Length > 0)
            {
                term += " and a.uploadstartdate=@uploadstartdate ";
                if (!ht.ContainsKey("@uploadstartdate"))
                {
                    ht.Add("@uploadstartdate", obj.Uploadstartdate);
                }
            }
            if (obj.Uploadenddate != null && obj.Uploadenddate.Trim().Length > 0)
            {
                term += " and a.uploadenddate=@uploadenddate ";
                if (!ht.ContainsKey("@uploadenddate"))
                {
                    ht.Add("@uploadenddate", obj.Uploadenddate);
                }
            }
            if (obj.Paytype > 0)//支付类型,刊前\刊后
            {
                term += " and a.paytype=@paytype ";
                if (!ht.ContainsKey("@paytype"))
                {
                    ht.Add("@paytype", obj.Paytype);
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
            string sql = @"select {0} a.writingfeeitemid as writingfeeitemid,a.writingfeebillid as writingfeebillid,a.projectid as projectid,a.projectcode as projectcode,a.happendate as happendate,a.userid as userid,a.username as username,a.userdepartmentid as userdepartmentid,a.userdepartmentname as userdepartmentname,a.mediaid as mediaid,a.medianame as medianame,a.writingsubject as writingsubject,a.issuedate as issuedate,a.wordscount as wordscount,a.areawordscount as areawordscount,a.unitprice as unitprice,a.amountprice as amountprice,a.subtotal as subtotal,a.linkmanid as linkmanid,a.linkmanname as linkmanname,a.recvmanname as recvmanname,a.cityid as cityid,a.cityname as cityname,a.bankname as bankname,a.bankaccount as bankaccount,a.bankcardcode as bankcardcode,a.idcardcode as idcardcode,a.phoneno as phoneno,a.del as del,a.propagatetype as propagatetype,a.propagateid as propagateid,a.propagatename as propagatename,a.uploadstartdate as uploadstartdate,a.uploadenddate as uploadenddate,a.paytype as paytype {1} from media_writingfeeitem as a {2} where 1=1 {3} ";
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


        public static DataTable QueryInfoByObj(WritingfeeitemInfo obj, string terms, Hashtable ht)
        {
            if (ht == null)
            {
                ht = new Hashtable();
            }
            SqlParameter[] para = ESP.Media.Access.Utilities.Common.DictToSqlParam(ht);
            return QueryInfoByObj(obj, terms, para);
        }


        public static DataTable QueryInfoByObj(WritingfeeitemInfo obj, string terms, params SqlParameter[] param)
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
        public static WritingfeeitemInfo Load(int id)
        {
            DataTable dt = null;
            dt = QueryInfo(" and a.writingfeeitemid=@id ", new SqlParameter("@id", id));
            if (dt != null && dt.Rows.Count > 0)
            {
                return setObject(dt.Rows[0]);
            }
            return null;
        }


        public static WritingfeeitemInfo Load(int id, SqlTransaction trans)
        {
            DataTable dt = null;
            dt = QueryInfo(trans, " and a.writingfeeitemid=@id ", new SqlParameter("@id", id));
            if (dt != null && dt.Rows.Count > 0)
            {
                return setObject(dt.Rows[0]);
            }
            return null;
        }


        public static WritingfeeitemInfo setObject(DataRow dr)
        {
            WritingfeeitemInfo obj = new WritingfeeitemInfo();
            if (dr.Table.Columns.Contains("writingfeeitemid") && dr["writingfeeitemid"] != DBNull.Value)//writingfeeitemid
            {
                obj.Writingfeeitemid = Convert.ToInt32(dr["writingfeeitemid"]);
            }
            if (dr.Table.Columns.Contains("writingfeebillid") && dr["writingfeebillid"] != DBNull.Value)//writingfeebillid
            {
                obj.Writingfeebillid = Convert.ToInt32(dr["writingfeebillid"]);
            }
            if (dr.Table.Columns.Contains("projectid") && dr["projectid"] != DBNull.Value)//projectid
            {
                obj.Projectid = Convert.ToInt32(dr["projectid"]);
            }
            if (dr.Table.Columns.Contains("projectcode") && dr["projectcode"] != DBNull.Value)//projectcode
            {
                obj.Projectcode = (dr["projectcode"]).ToString();
            }
            if (dr.Table.Columns.Contains("happendate") && dr["happendate"] != DBNull.Value)//happendate
            {
                obj.Happendate = (dr["happendate"]).ToString();
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
            if (dr.Table.Columns.Contains("mediaid") && dr["mediaid"] != DBNull.Value)//mediaid
            {
                obj.Mediaid = Convert.ToInt32(dr["mediaid"]);
            }
            if (dr.Table.Columns.Contains("medianame") && dr["medianame"] != DBNull.Value)//medianame
            {
                obj.Medianame = (dr["medianame"]).ToString();
            }
            if (dr.Table.Columns.Contains("writingsubject") && dr["writingsubject"] != DBNull.Value)//writingsubject
            {
                obj.Writingsubject = (dr["writingsubject"]).ToString();
            }
            if (dr.Table.Columns.Contains("issuedate") && dr["issuedate"] != DBNull.Value)//issuedate
            {
                obj.Issuedate = (dr["issuedate"]).ToString();
            }
            if (dr.Table.Columns.Contains("wordscount") && dr["wordscount"] != DBNull.Value)//wordscount
            {
                obj.Wordscount = Convert.ToInt32(dr["wordscount"]);
            }
            if (dr.Table.Columns.Contains("areawordscount") && dr["areawordscount"] != DBNull.Value)//areawordscount
            {
                obj.Areawordscount = Convert.ToInt32(dr["areawordscount"]);
            }
            if (dr.Table.Columns.Contains("unitprice") && dr["unitprice"] != DBNull.Value)//unitprice
            {
                obj.Unitprice = Convert.ToDouble(dr["unitprice"]);
            }
            if (dr.Table.Columns.Contains("amountprice") && dr["amountprice"] != DBNull.Value)//amountprice
            {
                obj.Amountprice = Convert.ToDouble(dr["amountprice"]);
            }
            if (dr.Table.Columns.Contains("subtotal") && dr["subtotal"] != DBNull.Value)//subtotal
            {
                obj.Subtotal = Convert.ToDouble(dr["subtotal"]);
            }
            if (dr.Table.Columns.Contains("linkmanid") && dr["linkmanid"] != DBNull.Value)//linkmanid
            {
                obj.Linkmanid = Convert.ToInt32(dr["linkmanid"]);
            }
            if (dr.Table.Columns.Contains("linkmanname") && dr["linkmanname"] != DBNull.Value)//linkmanname
            {
                obj.Linkmanname = (dr["linkmanname"]).ToString();
            }
            if (dr.Table.Columns.Contains("recvmanname") && dr["recvmanname"] != DBNull.Value)//recvmanname
            {
                obj.Recvmanname = (dr["recvmanname"]).ToString();
            }
            if (dr.Table.Columns.Contains("cityid") && dr["cityid"] != DBNull.Value)//cityid
            {
                obj.Cityid = Convert.ToInt32(dr["cityid"]);
            }
            if (dr.Table.Columns.Contains("cityname") && dr["cityname"] != DBNull.Value)//cityname
            {
                obj.Cityname = (dr["cityname"]).ToString();
            }
            if (dr.Table.Columns.Contains("bankname") && dr["bankname"] != DBNull.Value)//bankname
            {
                obj.Bankname = (dr["bankname"]).ToString();
            }
            if (dr.Table.Columns.Contains("bankaccount") && dr["bankaccount"] != DBNull.Value)//bankaccount
            {
                obj.Bankaccount = (dr["bankaccount"]).ToString();
            }
            if (dr.Table.Columns.Contains("bankcardcode") && dr["bankcardcode"] != DBNull.Value)//bankcardcode
            {
                obj.Bankcardcode = (dr["bankcardcode"]).ToString();
            }
            if (dr.Table.Columns.Contains("idcardcode") && dr["idcardcode"] != DBNull.Value)//idcardcode
            {
                obj.Idcardcode = (dr["idcardcode"]).ToString();
            }
            if (dr.Table.Columns.Contains("phoneno") && dr["phoneno"] != DBNull.Value)//phoneno
            {
                obj.Phoneno = (dr["phoneno"]).ToString();
            }
            if (dr.Table.Columns.Contains("del") && dr["del"] != DBNull.Value)//del
            {
                obj.Del = Convert.ToInt32(dr["del"]);
            }
            if (dr.Table.Columns.Contains("propagatetype") && dr["propagatetype"] != DBNull.Value)//propagatetype
            {
                obj.Propagatetype = Convert.ToInt32(dr["propagatetype"]);
            }
            if (dr.Table.Columns.Contains("propagateid") && dr["propagateid"] != DBNull.Value)//propagateid
            {
                obj.Propagateid = Convert.ToInt32(dr["propagateid"]);
            }
            if (dr.Table.Columns.Contains("propagatename") && dr["propagatename"] != DBNull.Value)//propagatename
            {
                obj.Propagatename = (dr["propagatename"]).ToString();
            }
            if (dr.Table.Columns.Contains("uploadstartdate") && dr["uploadstartdate"] != DBNull.Value)//uploadstartdate
            {
                obj.Uploadstartdate = (dr["uploadstartdate"]).ToString();
            }
            if (dr.Table.Columns.Contains("uploadenddate") && dr["uploadenddate"] != DBNull.Value)//uploadenddate
            {
                obj.Uploadenddate = (dr["uploadenddate"]).ToString();
            }
            if (dr.Table.Columns.Contains("paytype") && dr["paytype"] != DBNull.Value)//支付类型,刊前\刊后
            {
                obj.Paytype = Convert.ToInt32(dr["paytype"]);
            }
            return obj;
        }
        #endregion
    }
}
