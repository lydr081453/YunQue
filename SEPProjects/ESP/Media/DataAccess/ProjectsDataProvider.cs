using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using ESP.Media.Access.Utilities;
using System.Collections.Generic;
using ESP.Media.Entity;
namespace ESP.Media.DataAccess
{
    public class ProjectsDataProvider
    {
        #region 构造函数
        public ProjectsDataProvider()
        {
        }
        #endregion
        #region 插入
        //插入字符串
        private static string strinsert(ProjectsInfo obj, ref List<SqlParameter> ht)
        {
            if (ht == null)
            {
                ht = new List<SqlParameter>();
            }
            string sql = @"insert into media_projects (projectname,productid,createdbyuserid,createdbyusername,createddate,status,begindate,enddate,del,projectcode,projectdescription,companyname,bankname,bankaccount,clientid,teamleaderid,teamleadername,departmentid,departmentname,steps) values (@projectname,@productid,@createdbyuserid,@createdbyusername,@createddate,@status,@begindate,@enddate,@del,@projectcode,@projectdescription,@companyname,@bankname,@bankaccount,@clientid,@teamleaderid,@teamleadername,@departmentid,@departmentname,@steps);select @@IDENTITY as rowNum;";
            SqlParameter param_Projectname = new SqlParameter("@Projectname", SqlDbType.NVarChar, 512);
            param_Projectname.Value = obj.Projectname;
            ht.Add(param_Projectname);
            SqlParameter param_Productid = new SqlParameter("@Productid", SqlDbType.Int, 4);
            param_Productid.Value = obj.Productid;
            ht.Add(param_Productid);
            SqlParameter param_Createdbyuserid = new SqlParameter("@Createdbyuserid", SqlDbType.Int, 4);
            param_Createdbyuserid.Value = obj.Createdbyuserid;
            ht.Add(param_Createdbyuserid);
            SqlParameter param_Createdbyusername = new SqlParameter("@Createdbyusername", SqlDbType.NVarChar, 512);
            param_Createdbyusername.Value = obj.Createdbyusername;
            ht.Add(param_Createdbyusername);
            SqlParameter param_Createddate = new SqlParameter("@Createddate", SqlDbType.DateTime, 8);
            param_Createddate.Value = ESP.Media.Access.Utilities.Common.StringToDateTime(obj.Createddate);
            ht.Add(param_Createddate);
            SqlParameter param_Status = new SqlParameter("@Status", SqlDbType.Int, 4);
            param_Status.Value = obj.Status;
            ht.Add(param_Status);
            SqlParameter param_Begindate = new SqlParameter("@Begindate", SqlDbType.DateTime, 8);
            param_Begindate.Value = ESP.Media.Access.Utilities.Common.StringToDateTime(obj.Begindate);
            ht.Add(param_Begindate);
            SqlParameter param_Enddate = new SqlParameter("@Enddate", SqlDbType.DateTime, 8);
            param_Enddate.Value = ESP.Media.Access.Utilities.Common.StringToDateTime(obj.Enddate);
            ht.Add(param_Enddate);
            SqlParameter param_Del = new SqlParameter("@Del", SqlDbType.SmallInt, 2);
            param_Del.Value = obj.Del;
            ht.Add(param_Del);
            SqlParameter param_Projectcode = new SqlParameter("@Projectcode", SqlDbType.NVarChar, 100);
            param_Projectcode.Value = obj.Projectcode;
            ht.Add(param_Projectcode);
            SqlParameter param_Projectdescription = new SqlParameter("@Projectdescription", SqlDbType.NVarChar, 2000);
            param_Projectdescription.Value = obj.Projectdescription;
            ht.Add(param_Projectdescription);
            SqlParameter param_Companyname = new SqlParameter("@Companyname", SqlDbType.NVarChar, 100);
            param_Companyname.Value = obj.Companyname;
            ht.Add(param_Companyname);
            SqlParameter param_Bankname = new SqlParameter("@Bankname", SqlDbType.NVarChar, 100);
            param_Bankname.Value = obj.Bankname;
            ht.Add(param_Bankname);
            SqlParameter param_Bankaccount = new SqlParameter("@Bankaccount", SqlDbType.NVarChar, 100);
            param_Bankaccount.Value = obj.Bankaccount;
            ht.Add(param_Bankaccount);
            SqlParameter param_Clientid = new SqlParameter("@Clientid", SqlDbType.Int, 4);
            param_Clientid.Value = obj.Clientid;
            ht.Add(param_Clientid);
            SqlParameter param_Teamleaderid = new SqlParameter("@Teamleaderid", SqlDbType.Int, 4);
            param_Teamleaderid.Value = obj.Teamleaderid;
            ht.Add(param_Teamleaderid);
            SqlParameter param_Teamleadername = new SqlParameter("@Teamleadername", SqlDbType.NVarChar, 100);
            param_Teamleadername.Value = obj.Teamleadername;
            ht.Add(param_Teamleadername);
            SqlParameter param_Departmentid = new SqlParameter("@Departmentid", SqlDbType.Int, 4);
            param_Departmentid.Value = obj.Departmentid;
            ht.Add(param_Departmentid);
            SqlParameter param_Departmentname = new SqlParameter("@Departmentname", SqlDbType.NVarChar, 100);
            param_Departmentname.Value = obj.Departmentname;
            ht.Add(param_Departmentname);
            SqlParameter param_Steps = new SqlParameter("@Steps", SqlDbType.Int, 4);
            param_Steps.Value = obj.Steps;
            ht.Add(param_Steps);
            return sql;
        }


        //插入一条记录
        public static int insertinfo(ProjectsInfo obj, SqlTransaction trans)
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
        public static int insertinfo(ProjectsInfo obj)
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
            string sql = "delete media_projects where ProjectID=@id";
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
        public static string getUpdateString(ProjectsInfo objTerm, ProjectsInfo Objupdate, string term, ref List<SqlParameter> ht, params SqlParameter[] param)
        {
            string sql = "update media_projects set projectname=@projectname,productid=@productid,createdbyuserid=@createdbyuserid,createdbyusername=@createdbyusername,createddate=@createddate,status=@status,begindate=@begindate,enddate=@enddate,del=@del,projectcode=@projectcode,projectdescription=@projectdescription,companyname=@companyname,bankname=@bankname,bankaccount=@bankaccount,clientid=@clientid,teamleaderid=@teamleaderid,teamleadername=@teamleadername,departmentid=@departmentid,departmentname=@departmentname,steps=@steps where 1=1 ";
            SqlParameter param_projectname = new SqlParameter("@projectname", SqlDbType.NVarChar, 512);
            param_projectname.Value = Objupdate.Projectname;
            ht.Add(param_projectname);
            SqlParameter param_productid = new SqlParameter("@productid", SqlDbType.Int, 4);
            param_productid.Value = Objupdate.Productid;
            ht.Add(param_productid);
            SqlParameter param_createdbyuserid = new SqlParameter("@createdbyuserid", SqlDbType.Int, 4);
            param_createdbyuserid.Value = Objupdate.Createdbyuserid;
            ht.Add(param_createdbyuserid);
            SqlParameter param_createdbyusername = new SqlParameter("@createdbyusername", SqlDbType.NVarChar, 512);
            param_createdbyusername.Value = Objupdate.Createdbyusername;
            ht.Add(param_createdbyusername);
            SqlParameter param_createddate = new SqlParameter("@createddate", SqlDbType.DateTime, 8);
            param_createddate.Value = ESP.Media.Access.Utilities.Common.StringToDateTime(Objupdate.Createddate);
            ht.Add(param_createddate);
            SqlParameter param_status = new SqlParameter("@status", SqlDbType.Int, 4);
            param_status.Value = Objupdate.Status;
            ht.Add(param_status);
            SqlParameter param_begindate = new SqlParameter("@begindate", SqlDbType.DateTime, 8);
            param_begindate.Value = ESP.Media.Access.Utilities.Common.StringToDateTime(Objupdate.Begindate);
            ht.Add(param_begindate);
            SqlParameter param_enddate = new SqlParameter("@enddate", SqlDbType.DateTime, 8);
            param_enddate.Value = ESP.Media.Access.Utilities.Common.StringToDateTime(Objupdate.Enddate);
            ht.Add(param_enddate);
            SqlParameter param_del = new SqlParameter("@del", SqlDbType.SmallInt, 2);
            param_del.Value = Objupdate.Del;
            ht.Add(param_del);
            SqlParameter param_projectcode = new SqlParameter("@projectcode", SqlDbType.NVarChar, 100);
            param_projectcode.Value = Objupdate.Projectcode;
            ht.Add(param_projectcode);
            SqlParameter param_projectdescription = new SqlParameter("@projectdescription", SqlDbType.NVarChar, 2000);
            param_projectdescription.Value = Objupdate.Projectdescription;
            ht.Add(param_projectdescription);
            SqlParameter param_companyname = new SqlParameter("@companyname", SqlDbType.NVarChar, 100);
            param_companyname.Value = Objupdate.Companyname;
            ht.Add(param_companyname);
            SqlParameter param_bankname = new SqlParameter("@bankname", SqlDbType.NVarChar, 100);
            param_bankname.Value = Objupdate.Bankname;
            ht.Add(param_bankname);
            SqlParameter param_bankaccount = new SqlParameter("@bankaccount", SqlDbType.NVarChar, 100);
            param_bankaccount.Value = Objupdate.Bankaccount;
            ht.Add(param_bankaccount);
            SqlParameter param_clientid = new SqlParameter("@clientid", SqlDbType.Int, 4);
            param_clientid.Value = Objupdate.Clientid;
            ht.Add(param_clientid);
            SqlParameter param_teamleaderid = new SqlParameter("@teamleaderid", SqlDbType.Int, 4);
            param_teamleaderid.Value = Objupdate.Teamleaderid;
            ht.Add(param_teamleaderid);
            SqlParameter param_teamleadername = new SqlParameter("@teamleadername", SqlDbType.NVarChar, 100);
            param_teamleadername.Value = Objupdate.Teamleadername;
            ht.Add(param_teamleadername);
            SqlParameter param_departmentid = new SqlParameter("@departmentid", SqlDbType.Int, 4);
            param_departmentid.Value = Objupdate.Departmentid;
            ht.Add(param_departmentid);
            SqlParameter param_departmentname = new SqlParameter("@departmentname", SqlDbType.NVarChar, 100);
            param_departmentname.Value = Objupdate.Departmentname;
            ht.Add(param_departmentname);
            SqlParameter param_steps = new SqlParameter("@steps", SqlDbType.Int, 4);
            param_steps.Value = Objupdate.Steps;
            ht.Add(param_steps);
            if (objTerm == null && (term == null || term.Trim().Length <= 0))
            {
                sql += " and ProjectID=@id ";
                if (ESP.Media.Access.Utilities.Common.Find(ht, "@id") == -1)
                {
                    SqlParameter p = new SqlParameter("@id", SqlDbType.Int, 4);
                    p.Value = Objupdate.Projectid;
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
        public static bool updateInfo(SqlTransaction trans, ProjectsInfo objterm, ProjectsInfo Objupdate, string term, params SqlParameter[] param)
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
        public static bool updateInfo(ProjectsInfo objterm, ProjectsInfo Objupdate, string term, params SqlParameter[] param)
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


        private static string getTerms(ProjectsInfo obj, ref List<SqlParameter> ht)
        {
            string term = string.Empty;
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
            if (obj.Projectname != null && obj.Projectname.Trim().Length > 0)
            {
                term += " and projectname=@projectname ";
                if (ESP.Media.Access.Utilities.Common.Find(ht, "@projectname") == -1)
                {
                    SqlParameter p = new SqlParameter("@projectname", SqlDbType.NVarChar, 512);
                    p.Value = obj.Projectname;
                    ht.Add(p);
                }
            }
            if (obj.Productid > 0)//ProductID
            {
                term += " and productid=@productid ";
                if (ESP.Media.Access.Utilities.Common.Find(ht, "@productid") == -1)
                {
                    SqlParameter p = new SqlParameter("@productid", SqlDbType.Int, 4);
                    p.Value = obj.Productid;
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
            if (obj.Createdbyusername != null && obj.Createdbyusername.Trim().Length > 0)
            {
                term += " and createdbyusername=@createdbyusername ";
                if (ESP.Media.Access.Utilities.Common.Find(ht, "@createdbyusername") == -1)
                {
                    SqlParameter p = new SqlParameter("@createdbyusername", SqlDbType.NVarChar, 512);
                    p.Value = obj.Createdbyusername;
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
            if (obj.Projectdescription != null && obj.Projectdescription.Trim().Length > 0)
            {
                term += " and projectdescription=@projectdescription ";
                if (ESP.Media.Access.Utilities.Common.Find(ht, "@projectdescription") == -1)
                {
                    SqlParameter p = new SqlParameter("@projectdescription", SqlDbType.NVarChar, 2000);
                    p.Value = obj.Projectdescription;
                    ht.Add(p);
                }
            }
            if (obj.Companyname != null && obj.Companyname.Trim().Length > 0)
            {
                term += " and companyname=@companyname ";
                if (ESP.Media.Access.Utilities.Common.Find(ht, "@companyname") == -1)
                {
                    SqlParameter p = new SqlParameter("@companyname", SqlDbType.NVarChar, 100);
                    p.Value = obj.Companyname;
                    ht.Add(p);
                }
            }
            if (obj.Bankname != null && obj.Bankname.Trim().Length > 0)
            {
                term += " and bankname=@bankname ";
                if (ESP.Media.Access.Utilities.Common.Find(ht, "@bankname") == -1)
                {
                    SqlParameter p = new SqlParameter("@bankname", SqlDbType.NVarChar, 100);
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
            if (obj.Clientid > 0)//clientID
            {
                term += " and clientid=@clientid ";
                if (ESP.Media.Access.Utilities.Common.Find(ht, "@clientid") == -1)
                {
                    SqlParameter p = new SqlParameter("@clientid", SqlDbType.Int, 4);
                    p.Value = obj.Clientid;
                    ht.Add(p);
                }
            }
            if (obj.Teamleaderid > 0)//TeamLeaderID
            {
                term += " and teamleaderid=@teamleaderid ";
                if (ESP.Media.Access.Utilities.Common.Find(ht, "@teamleaderid") == -1)
                {
                    SqlParameter p = new SqlParameter("@teamleaderid", SqlDbType.Int, 4);
                    p.Value = obj.Teamleaderid;
                    ht.Add(p);
                }
            }
            if (obj.Teamleadername != null && obj.Teamleadername.Trim().Length > 0)
            {
                term += " and teamleadername=@teamleadername ";
                if (ESP.Media.Access.Utilities.Common.Find(ht, "@teamleadername") == -1)
                {
                    SqlParameter p = new SqlParameter("@teamleadername", SqlDbType.NVarChar, 100);
                    p.Value = obj.Teamleadername;
                    ht.Add(p);
                }
            }
            if (obj.Departmentid > 0)//DepartmentID
            {
                term += " and departmentid=@departmentid ";
                if (ESP.Media.Access.Utilities.Common.Find(ht, "@departmentid") == -1)
                {
                    SqlParameter p = new SqlParameter("@departmentid", SqlDbType.Int, 4);
                    p.Value = obj.Departmentid;
                    ht.Add(p);
                }
            }
            if (obj.Departmentname != null && obj.Departmentname.Trim().Length > 0)
            {
                term += " and departmentname=@departmentname ";
                if (ESP.Media.Access.Utilities.Common.Find(ht, "@departmentname") == -1)
                {
                    SqlParameter p = new SqlParameter("@departmentname", SqlDbType.NVarChar, 100);
                    p.Value = obj.Departmentname;
                    ht.Add(p);
                }
            }
            if (obj.Steps > 0)//steps
            {
                term += " and steps=@steps ";
                if (ESP.Media.Access.Utilities.Common.Find(ht, "@steps") == -1)
                {
                    SqlParameter p = new SqlParameter("@steps", SqlDbType.Int, 4);
                    p.Value = obj.Steps;
                    ht.Add(p);
                }
            }
            return term;
        }
        #endregion
        #region 查询
        private static string getQueryTerms(ProjectsInfo obj, ref Hashtable ht)
        {
            string term = string.Empty;
            if (obj.Projectid > 0)//ProjectID
            {
                term += " and a.projectid=@projectid ";
                if (!ht.ContainsKey("@projectid"))
                {
                    ht.Add("@projectid", obj.Projectid);
                }
            }
            if (obj.Projectname != null && obj.Projectname.Trim().Length > 0)
            {
                term += " and a.projectname=@projectname ";
                if (!ht.ContainsKey("@projectname"))
                {
                    ht.Add("@projectname", obj.Projectname);
                }
            }
            if (obj.Productid > 0)//ProductID
            {
                term += " and a.productid=@productid ";
                if (!ht.ContainsKey("@productid"))
                {
                    ht.Add("@productid", obj.Productid);
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
            if (obj.Createdbyusername != null && obj.Createdbyusername.Trim().Length > 0)
            {
                term += " and a.createdbyusername=@createdbyusername ";
                if (!ht.ContainsKey("@createdbyusername"))
                {
                    ht.Add("@createdbyusername", obj.Createdbyusername);
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
            if (obj.Status > 0)//Status
            {
                term += " and a.status=@status ";
                if (!ht.ContainsKey("@status"))
                {
                    ht.Add("@status", obj.Status);
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
            if (obj.Del > 0)//删除标记
            {
                term += " and a.del=@del ";
                if (!ht.ContainsKey("@del"))
                {
                    ht.Add("@del", obj.Del);
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
            if (obj.Projectdescription != null && obj.Projectdescription.Trim().Length > 0)
            {
                term += " and a.projectdescription=@projectdescription ";
                if (!ht.ContainsKey("@projectdescription"))
                {
                    ht.Add("@projectdescription", obj.Projectdescription);
                }
            }
            if (obj.Companyname != null && obj.Companyname.Trim().Length > 0)
            {
                term += " and a.companyname=@companyname ";
                if (!ht.ContainsKey("@companyname"))
                {
                    ht.Add("@companyname", obj.Companyname);
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
            if (obj.Clientid > 0)//clientID
            {
                term += " and a.clientid=@clientid ";
                if (!ht.ContainsKey("@clientid"))
                {
                    ht.Add("@clientid", obj.Clientid);
                }
            }
            if (obj.Teamleaderid > 0)//TeamLeaderID
            {
                term += " and a.teamleaderid=@teamleaderid ";
                if (!ht.ContainsKey("@teamleaderid"))
                {
                    ht.Add("@teamleaderid", obj.Teamleaderid);
                }
            }
            if (obj.Teamleadername != null && obj.Teamleadername.Trim().Length > 0)
            {
                term += " and a.teamleadername=@teamleadername ";
                if (!ht.ContainsKey("@teamleadername"))
                {
                    ht.Add("@teamleadername", obj.Teamleadername);
                }
            }
            if (obj.Departmentid > 0)//DepartmentID
            {
                term += " and a.departmentid=@departmentid ";
                if (!ht.ContainsKey("@departmentid"))
                {
                    ht.Add("@departmentid", obj.Departmentid);
                }
            }
            if (obj.Departmentname != null && obj.Departmentname.Trim().Length > 0)
            {
                term += " and a.departmentname=@departmentname ";
                if (!ht.ContainsKey("@departmentname"))
                {
                    ht.Add("@departmentname", obj.Departmentname);
                }
            }
            if (obj.Steps > 0)//steps
            {
                term += " and a.steps=@steps ";
                if (!ht.ContainsKey("@steps"))
                {
                    ht.Add("@steps", obj.Steps);
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
            string sql = @"select {0} a.projectid as projectid,a.projectname as projectname,a.productid as productid,a.createdbyuserid as createdbyuserid,a.createdbyusername as createdbyusername,a.createddate as createddate,a.status as status,a.begindate as begindate,a.enddate as enddate,a.del as del,a.projectcode as projectcode,a.projectdescription as projectdescription,a.companyname as companyname,a.bankname as bankname,a.bankaccount as bankaccount,a.clientid as clientid,a.teamleaderid as teamleaderid,a.teamleadername as teamleadername,a.departmentid as departmentid,a.departmentname as departmentname,a.steps as steps {1} from media_projects as a {2} where 1=1 {3} ";
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


        public static DataTable QueryInfoByObj(ProjectsInfo obj, string terms, Hashtable ht)
        {
            if (ht == null)
            {
                ht = new Hashtable();
            }
            SqlParameter[] para = ESP.Media.Access.Utilities.Common.DictToSqlParam(ht);
            return QueryInfoByObj(obj, terms, para);
        }


        public static DataTable QueryInfoByObj(ProjectsInfo obj, string terms, params SqlParameter[] param)
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
        public static ProjectsInfo Load(int id)
        {
            DataTable dt = null;
            dt = QueryInfo(" and a.projectid=@id ", new SqlParameter("@id", id));
            if (dt != null && dt.Rows.Count > 0)
            {
                return setObject(dt.Rows[0]);
            }
            return null;
        }


        public static ProjectsInfo Load(int id, SqlTransaction trans)
        {
            DataTable dt = null;
            dt = QueryInfo(trans, " and a.projectid=@id ", new SqlParameter("@id", id));
            if (dt != null && dt.Rows.Count > 0)
            {
                return setObject(dt.Rows[0]);
            }
            return null;
        }


        public static ProjectsInfo setObject(DataRow dr)
        {
            ProjectsInfo obj = new ProjectsInfo();
            if (dr.Table.Columns.Contains("projectid") && dr["projectid"] != DBNull.Value)//id
            {
                obj.Projectid = Convert.ToInt32(dr["projectid"]);
            }
            if (dr.Table.Columns.Contains("projectname") && dr["projectname"] != DBNull.Value)//项目名称
            {
                obj.Projectname = (dr["projectname"]).ToString();
            }
            if (dr.Table.Columns.Contains("productid") && dr["productid"] != DBNull.Value)//产品id
            {
                obj.Productid = Convert.ToInt32(dr["productid"]);
            }
            if (dr.Table.Columns.Contains("createdbyuserid") && dr["createdbyuserid"] != DBNull.Value)//创建用户id
            {
                obj.Createdbyuserid = Convert.ToInt32(dr["createdbyuserid"]);
            }
            if (dr.Table.Columns.Contains("createdbyusername") && dr["createdbyusername"] != DBNull.Value)//创建用户姓名
            {
                obj.Createdbyusername = (dr["createdbyusername"]).ToString();
            }
            if (dr.Table.Columns.Contains("createddate") && dr["createddate"] != DBNull.Value)//创建日期
            {
                obj.Createddate = (dr["createddate"]).ToString();
            }
            if (dr.Table.Columns.Contains("status") && dr["status"] != DBNull.Value)//状态
            {
                obj.Status = Convert.ToInt32(dr["status"]);
            }
            if (dr.Table.Columns.Contains("begindate") && dr["begindate"] != DBNull.Value)//开始时间
            {
                obj.Begindate = (dr["begindate"]).ToString();
            }
            if (dr.Table.Columns.Contains("enddate") && dr["enddate"] != DBNull.Value)//结束时间
            {
                obj.Enddate = (dr["enddate"]).ToString();
            }
            if (dr.Table.Columns.Contains("del") && dr["del"] != DBNull.Value)//删除标记
            {
                obj.Del = Convert.ToInt32(dr["del"]);
            }
            if (dr.Table.Columns.Contains("projectcode") && dr["projectcode"] != DBNull.Value)//项目编号
            {
                obj.Projectcode = (dr["projectcode"]).ToString();
            }
            if (dr.Table.Columns.Contains("projectdescription") && dr["projectdescription"] != DBNull.Value)//项目描述
            {
                obj.Projectdescription = (dr["projectdescription"]).ToString();
            }
            if (dr.Table.Columns.Contains("companyname") && dr["companyname"] != DBNull.Value)//companyname
            {
                obj.Companyname = (dr["companyname"]).ToString();
            }
            if (dr.Table.Columns.Contains("bankname") && dr["bankname"] != DBNull.Value)//bankname
            {
                obj.Bankname = (dr["bankname"]).ToString();
            }
            if (dr.Table.Columns.Contains("bankaccount") && dr["bankaccount"] != DBNull.Value)//bankaccount
            {
                obj.Bankaccount = (dr["bankaccount"]).ToString();
            }
            if (dr.Table.Columns.Contains("clientid") && dr["clientid"] != DBNull.Value)//clientid
            {
                obj.Clientid = Convert.ToInt32(dr["clientid"]);
            }
            if (dr.Table.Columns.Contains("teamleaderid") && dr["teamleaderid"] != DBNull.Value)//teamleaderid
            {
                obj.Teamleaderid = Convert.ToInt32(dr["teamleaderid"]);
            }
            if (dr.Table.Columns.Contains("teamleadername") && dr["teamleadername"] != DBNull.Value)//teamleadername
            {
                obj.Teamleadername = (dr["teamleadername"]).ToString();
            }
            if (dr.Table.Columns.Contains("departmentid") && dr["departmentid"] != DBNull.Value)//departmentid
            {
                obj.Departmentid = Convert.ToInt32(dr["departmentid"]);
            }
            if (dr.Table.Columns.Contains("departmentname") && dr["departmentname"] != DBNull.Value)//departmentname
            {
                obj.Departmentname = (dr["departmentname"]).ToString();
            }
            if (dr.Table.Columns.Contains("steps") && dr["steps"] != DBNull.Value)//steps
            {
                obj.Steps = Convert.ToInt32(dr["steps"]);
            }

            //加客户名称及产品线名称（未持久化到数据库）
            if (dr.Table.Columns.Contains("clientname") && dr["clientname"] != DBNull.Value)//steps
            {
                obj.ClientName = dr["clientname"].ToString();
            }

            if (dr.Table.Columns.Contains("productlinename") && dr["productlinename"] != DBNull.Value)//steps
            {
                obj.ProductlineName = dr["productlinename"].ToString();
            }

            return obj;
        }
        #endregion
    }
}
