using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.IO;
using System.Data.SqlClient;
using System.Data;
using ESP.Framework.Entity;
using ESP.Framework.BusinessLogic;
using ESP.Media.Access;
using ESP.Media.Entity;
using ESP.Compatible;
using ESP.Media.Access.Utilities;


namespace ESP.Media.BusinessLogic
{
    public class ProjectsManager
    {
        public ProjectsManager()
        {
        }

        /// <summary>
        /// 获取项目记者关联 的对象.
        /// </summary>
        /// <param name="projectid">The projectid.</param>
        /// <param name="reporterid">The reporterid.</param>
        /// <returns></returns>
        public static ProjectreporterrelationInfo GetRelationReporterModel(int projectid, int reporterid)
        {
            string term = " and projectid = @projectid and reporterid = @reporterid and del!=@del";
            Hashtable ht = new Hashtable();
            ht.Add("@projectid", projectid);
            ht.Add("@reporterid", reporterid);
            ht.Add("@del", (int)Global.FiledStatus.Del);
            DataTable dt = ESP.Media.DataAccess.ProjectreporterrelationDataProvider.QueryInfo(term, ht);
            if (dt == null || dt.Rows.Count == 0)
                return null;
            else
            {
                int rid = Convert.ToInt32(dt.Rows[0]["id"]);
                return GetRelationReporterModel(rid);
            }
        }

        /// <summary>
        /// Gets the relation reporter model.
        /// </summary>
        /// <param name="rid">The rid.</param>
        /// <returns></returns>
        public static ProjectreporterrelationInfo GetRelationReporterModel(int rid)
        {
            ProjectreporterrelationInfo r = ESP.Media.DataAccess.ProjectreporterrelationDataProvider.Load(rid);
            if (r == null) r = new ProjectreporterrelationInfo();
            return r;
        }

        /// <summary>
        /// 设置记者私密信息
        /// </summary>
        /// <param name="relation">The relation.</param>
        /// <param name="userid">The userid.</param>
        /// <param name="errmsg">The errmsg.</param>
        /// <returns></returns>
        public static int SetReporterProviteMsg(ProjectreporterrelationInfo relation, int userid, out string errmsg)
        {
            errmsg = "私密信息设置成功!";
            if (relation == null)
            {
                errmsg = "对象未赋值!";
                return -1;
            }
            using (SqlConnection conn = new SqlConnection(clsConfigOperate.CustomerSqlConnection()))
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();
                try
                {
                    if (relation.Paystatus == (int)Global.ReporterPayStatus.NotPay)
                    {
                        relation.Paystatus = (int)Global.ReporterPayStatus.SetPrivateMsg;
                    }
                    ESP.Media.DataAccess.ProjectreporterrelationDataProvider.updateInfo(trans, null, relation, null, null);
                    trans.Commit();
                    conn.Close();
                    return 1;
                }
                catch (Exception exp)
                {
                    trans.Rollback();
                    conn.Close();
                    errmsg = exp.Message;
                    return -2;
                }
            }
        }

        /// <summary>
        /// 获取项目列表
        /// </summary>
        /// <returns></returns>
        public static DataTable GetAllList()
        {
            return GetList(null, null);
        }

        public static List<ProjectsInfo> GetObjectList(string term, List<SqlParameter> param)
        {
            Hashtable ht = new Hashtable();
            for (int i = 0; i < param.Count; i++)
            {
                ht.Add(param[i].ParameterName, param[i].Value);
            }
            DataTable dt = GetList(term, ht);
            List<ProjectsInfo> projects = new List<ProjectsInfo>();
            var query = from project in dt.AsEnumerable() select ESP.Media.DataAccess.ProjectsDataProvider.setObject(project);
            foreach (ProjectsInfo project in query)
            {
                projects.Add(project);
            }
            return projects;
        }

        /// <summary>
        /// 获取项目列表
        /// </summary>
        /// <param name="terms">The terms.</param>
        /// <param name="ht">The Sqlparameter hashtable.</param>
        /// <returns></returns>
        public static DataTable GetList(string terms, Hashtable ht)
        {
            string sql = @"select a.ProjectId as ProjectId,
                            a.ProjectName as ProjectName,
                            a.ProjectCode as ProjectCode,
                            a.BeginDate as BeginDate,a.EndDate as EndDate,
                            a.ProjectDescription as ProjectDescription,a.companyname as companyname,a.bankname as bankname,a.bankaccount as bankaccoun,a.createddate as createddate,a.CreatedByUserID as createuserid
                            {0} from Media_projects as a {1} where 1=1 {2}";
            string newcol = ",productLine.ProductLineName as ProductLineName,client.ClientCFullName as ClientName";
            string jointable = @"left join Media_ProductLines as productLine on a.ProductID = productLine.ProductLineID
                                 left join Media_Clients as client on a.ClientID = client.ClientID
                                 left join Media_ProjectMembers as member on member.projectid = a.projectid ";//add by sxc 每一个登陆人员现在能看到所有的项目信息，应该是每个人只能看到自己已经成为项目组成员的项目信息，只要他不是某个项目组成员，他就在项目列表中看不到该项目
            if (terms == null)
            {
                terms = string.Empty;
            }
            terms += @" and a.del!=@del group by ProjectName,ProjectCode,BeginDate,EndDate,
                            ProductLineName,client.ClientCFullName,ProjectDescription,a.ProjectId,a.companyname,a.bankname,a.bankaccount,a.createddate,a.CreatedByUserID ";

            if (ht == null)
            {
                ht = new Hashtable();
            }
            if (!ht.ContainsKey("@del"))
            {
                ht.Add("@del", (int)Global.FiledStatus.Del);
            }

            sql = string.Format(sql, newcol, jointable, terms);
            SqlParameter[] param = ESP.Media.Access.Utilities.Common.DictToSqlParam(ht);
            return clsSelect.QueryBySql(sql, param);
        }

        /// <summary>
        /// 根据项目ID获取一个项目对象.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns></returns>
        public static ProjectsInfo GetModel(int id)
        {
            return ESP.Media.DataAccess.ProjectsDataProvider.Load(id);
        }

        /// <summary>
        /// 添加一个项目
        /// </summary>
        /// <param name="obj">The obj.</param>
        /// <param name="userid">The userid.</param>
        /// <param name="errmsg">The errmsg.</param>
        /// <returns></returns>
        public static int Add(ProjectsInfo obj, int userid, out string errmsg)
        {
            using (SqlConnection conn = new SqlConnection(clsConfigOperate.CustomerSqlConnection()))
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();
                string term = "ProjectName=@ProjectName AND del!=@del ";//项目名称查重
                SqlParameter[] param = new SqlParameter[2];
                param[0] = new SqlParameter("@ProjectName", SqlDbType.NVarChar);
                param[0].Value = obj.Projectname;
                param[1] = new SqlParameter("@del", SqlDbType.Int);
                param[1].Value = (int)Global.FiledStatus.Del;


                DataTable dt = ESP.Media.DataAccess.ProjectsDataProvider.QueryInfo(trans, term, param);
                if (dt.Rows.Count > 0)
                {
                    errmsg = "项目名称或项目号已存在!";
                    trans.Rollback();
                    conn.Close();
                    return -1;
                }
                try
                {
                    errmsg = string.Empty;
                    int ret = ESP.Media.DataAccess.ProjectsDataProvider.insertinfo(obj, trans);
                    ProjectmembersInfo member = new ProjectmembersInfo();
                    member.Projectid = ret;
                    member.Userid = userid;
                    member.Relationdate = DateTime.Now.ToString();
                    member.Relationuserid = userid;
                    member.Del = (int)Global.FiledStatus.Usable;
                    ESP.Media.DataAccess.ProjectmembersDataProvider.insertinfo(member, trans);//将创建人及使用人(teamleader)加入到项目成员表
                    member.Relationuserid = obj.Teamleaderid;
                    ESP.Media.DataAccess.ProjectmembersDataProvider.insertinfo(member, trans);
                    trans.Commit();
                    conn.Close();
                    return ret;
                }
                catch (Exception exception)
                {
                    trans.Rollback();
                    conn.Close();
                    errmsg = exception.Message;
                    return -2;
                }
            }
        }

        /// <summary>
        /// 更新项目信息
        /// </summary>
        /// <param name="obj">The obj.</param>
        /// <param name="errmsg">The errmsg.</param>
        /// <returns></returns>
        public static int Update(ProjectsInfo obj, out string errmsg)
        {

            using (SqlConnection conn = new SqlConnection(clsConfigOperate.CustomerSqlConnection()))
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();
                try
                {
                    string term = "ProjectName=@ProjectName and ProjectID!=@ProjectID and del!=@del ";//项目名称查重
                    SqlParameter[] param = new SqlParameter[3];
                    param[0] = new SqlParameter("@ProjectName", SqlDbType.NVarChar);
                    param[0].Value = obj.Projectname;
                    param[1] = new SqlParameter("@ProjectID", SqlDbType.Int);
                    param[1].Value = obj.Projectid;
                    param[2] = new SqlParameter("@del", SqlDbType.Int);
                    param[2].Value = (int)Global.FiledStatus.Del;
                    DataTable dt =ESP.Media.DataAccess.ProjectsDataProvider.QueryInfo(trans, term, param);
                    if (dt.Rows.Count > 0)
                    {
                        errmsg = "项目名称或项目号已存在!";
                        trans.Rollback();
                        conn.Close();
                        return -1;
                    }
                    errmsg = string.Empty;
                    if (ESP.Media.DataAccess.ProjectsDataProvider.updateInfo(trans, null, obj, string.Empty, null))
                    {
                        errmsg = "项目修改成功!";
                        trans.Commit();
                        conn.Close();
                        return 1;
                    }
                    else
                    {
                        errmsg = "修改失败!";
                        conn.Close();
                        return -3;
                    }
                }
                catch (Exception exception)
                {
                    trans.Rollback();
                    conn.Close();
                    errmsg = exception.Message;
                    return -2;
                }
            }
        }

        /// <summary>
        ///删除项目，(目前需求：项目不能删除)
        /// </summary>
        /// <param name="obj">The obj.</param>
        /// <param name="errmsg">The errmsg.</param>
        /// <returns></returns>
        public static int Delete(ProjectsInfo obj, out string errmsg)
        {
            errmsg = "删除成功!";
            try
            {
                obj.Del = (int)Global.FiledStatus.Del;
                if (ESP.Media.DataAccess.ProjectsDataProvider.updateInfo(null, obj, string.Empty, null))
                {
                    return 1;
                }
                else
                {
                    errmsg = "删除失败!";
                    return -3;
                }
            }
            catch (Exception exception)
            {
                errmsg = exception.Message;
                return -2;
            }
        }

        /// <summary>
        /// 给项目添加媒体
        /// </summary>
        /// <param name="projectId">项目 id.</param>
        /// <param name="mediaId">The media id 集合.</param>
        /// <param name="userid">The userid.</param>
        /// <param name="errmsg">The errmsg.</param>
        /// <returns></returns>
        public static int AddMedia(int projectId, int[] mediaId, int userid, out string errmsg)
        {
            errmsg = "添加成功!";
            using (SqlConnection conn = new SqlConnection(clsConfigOperate.CustomerSqlConnection()))
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();
                try
                {
                    foreach (int mid in mediaId)
                    {
                        ProjectmediarelationInfo pmrelation = new ProjectmediarelationInfo();
                        pmrelation.Projectid = projectId;
                        pmrelation.Mediaitemid = mid;
                        pmrelation.Relationdate = DateTime.Now.ToString();
                        pmrelation.Relationuserid = userid;
                        ESP.Media.DataAccess.ProjectmediarelationDataProvider.insertinfo(pmrelation, trans);
                    }
                    trans.Commit();
                    conn.Close();
                    return mediaId.Length;
                }
                catch (Exception exp)
                {
                    trans.Rollback();
                    conn.Close();
                    errmsg = exp.Message;
                    return -2;
                }
            }
        }

        /// <summary>
        /// 所有未被项目选中的用户列表
        /// </summary>
        /// <param name="projectid">The projectid.</param>
        /// <returns></returns>
        public static IList<Employee> GetUnSelectMembers(int projectid)
        {
            return GetUnSelectMembers(null, projectid);
        }

        /// <summary>
        /// 所有未被项目选中的用户列表
        /// </summary>
        /// <param name="username">The username.</param>
        /// <param name="projectid">The projectid.</param>
        /// <returns></returns>
        public static IList<Employee> GetUnSelectMembers(string username, int projectid)
        {
            int[] uids = ProjectmembersManager.GetHaveProjectUserID(projectid);

            IList<EmployeeInfo> list = null;
            if (!string.IsNullOrEmpty(username))
                list = ESP.Framework.BusinessLogic.EmployeeManager.SearchByChineseName(username);
            else
                list = ESP.Framework.BusinessLogic.EmployeeManager.GetAll();


            if (list == null || list.Count == 0)
                list = new List<EmployeeInfo>(0);

            bool flag = (uids != null && uids.Length > 0);

            IList<Employee> copy = new List<Employee>();

            foreach (EmployeeInfo emp in list)
            {
                if (!flag || Array.IndexOf<int>(uids, emp.UserID) < 0)
                {
                    copy.Add(Employee.CreateFromEmployeeInfo(emp));
                }
            }

            return copy;
        }


        /// <summary>
        /// 所有未被项目选中的用户列表
        /// </summary>
        /// <param name="username">The username.</param>
        /// <param name="projectid">The projectid.</param>
        /// <returns></returns>
        public static IList<Employee> GetUnSelectMembers(string username, int depid, int projectid)
        {
            IList<EmployeeInfo> list = EmployeeManager.GetEmployeesByDepartment(depid);
            int[] uids = ProjectmembersManager.GetHaveProjectUserID(projectid);

            if (list == null)
                list = new List<EmployeeInfo>(0);


            bool ignoreUsername = string.IsNullOrEmpty(username);
            bool ignoreProjectUsers = (uids == null || uids.Length == 0);

            //if (ignoreUsername && ignoreProjectUsers)
            //    return list;

            IList<Employee> copy = new List<Employee>();
            foreach (EmployeeInfo e in list)
            {
                //用户已经在项目中
                if (!ignoreProjectUsers && Array.IndexOf<int>(uids, e.UserID) >= 0)
                {
                    continue;
                }

                //用户不在项目中且 username 参数不为空
                if (!ignoreUsername && (e.FullNameCN == null || e.FullNameCN.IndexOf(username, StringComparison.OrdinalIgnoreCase) < 0))
                {
                    continue;
                }

                copy.Add(Employee.CreateFromEmployeeInfo(e));
            }

            return copy;
        }

        /// <summary>
        /// 获得项目中项目组成员
        /// </summary>
        /// <param name="projectid">The projectid.</param>
        /// <returns></returns>
        public static IList<Employee> GetMembers(int projectid)
        {
            int[] uids = ProjectmembersManager.GetHaveProjectUserID(projectid);
            if (uids == null || uids.Length <= 0)
                return null;


            IList<Employee> employees = new List<Employee>(uids.Length);

            foreach (int id in uids)
            {
                Employee emp = new Employee(id);
                if (emp != null)
                    employees.Add(emp);
            }

            if (employees.Count == 0)
                return null;

            return employees;
        }

        /// <summary>
        /// 添加项目成员
        /// </summary>
        /// <param name="projectId">The project id.</param>
        /// <param name="uids">The uids.</param>
        /// <param name="userid">The userid.</param>
        /// <param name="errmsg">The errmsg.</param>
        /// <returns></returns>
        public static int AddMembers(int projectId, int[] uids, int userid, out string errmsg)
        {
            errmsg = "添加成功!";
            using (SqlConnection conn = new SqlConnection(clsConfigOperate.CustomerSqlConnection()))
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();
                try
                {
                    AddMembers(projectId, uids, userid, out errmsg, trans);
                    trans.Commit();
                    conn.Close();
                    return uids.Length;
                }
                catch (Exception exp)
                {
                    trans.Rollback();
                    conn.Close();
                    errmsg = exp.Message;
                    return -2;
                }
            }
        }

        /// <summary>
        /// 添加项目成员
        /// </summary>
        /// <param name="projectId">The project id.</param>
        /// <param name="uids">The uids.</param>
        /// <param name="userid">The userid.</param>
        /// <param name="errmsg">The errmsg.</param>
        /// <param name="trans">The trans.</param>
        /// <returns></returns>
        public static int AddMembers(int projectId, int[] uids, int userid, out string errmsg, SqlTransaction trans)
        {
            errmsg = "添加成功!";
            try
            {
                TablesInfo table = TablesManager.GetModel("Media_projectmembers", trans);
                int tableid = 0;
                if (table != null)
                {
                    tableid = table.Tableid;
                }
                foreach (int uid in uids)
                {
                    ProjectmembersInfo members = new ProjectmembersInfo();
                    members.Projectid = projectId;
                    members.Userid = uid;
                    members.Relationuserid = userid;
                    members.Relationdate = DateTime.Now.ToString();
                    members.Del = (int)Global.FiledStatus.Usable;
                    ESP.Media.DataAccess.ProjectmembersDataProvider.insertinfo(members, trans);
                    //OperatelogManager.add((int)Global.OperateType.Add, tableid, userid);//日志
                }
                return uids.Length;
            }
            catch (Exception exp)
            {
                errmsg = exp.Message;
                return -2;
            }
        }

        /// <summary>
        /// 删除项目组成员
        /// </summary>
        /// <param name="projectid">The projectid.</param>
        /// <param name="uid">The uid.</param>
        /// <param name="userid">The userid.</param>
        /// <param name="errmsg">The errmsg.</param>
        /// <returns></returns>
        public static int DelMember(int projectid, int uid, int userid, out string errmsg)
        {
            using (SqlConnection conn = new SqlConnection(clsConfigOperate.CustomerSqlConnection()))
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();
                try
                {
                    int delid = DelMember(projectid, uid, userid, out errmsg, trans);
                    trans.Commit();
                    conn.Close();
                    return delid;
                }
                catch (Exception exp)
                {
                    trans.Rollback();
                    conn.Close();
                    errmsg = exp.Message;
                    return -2;
                }
            }
        }

        /// <summary>
        /// 删除项目组成员.
        /// </summary>
        /// <param name="projectid">The projectid.</param>
        /// <param name="uid">The uid.</param>
        /// <param name="userid">The userid.</param>
        /// <param name="errmsg">The errmsg.</param>
        /// <param name="trans">The trans.</param>
        /// <returns></returns>
        public static int DelMember(int projectid, int uid, int userid, out string errmsg, SqlTransaction trans)
        {
            errmsg = "删除成功!";
            try
            {
                ProjectmembersInfo mem = ProjectmembersManager.GetModelByprojectmember(projectid, uid, trans);
                ESP.Media.DataAccess.ProjectmembersDataProvider.DeleteInfo(mem.Id, trans);
                return mem.Id;

            }
            catch (Exception exp)
            {
                errmsg = exp.Message;
                return -2;
            }
        }

        /// <summary>
        /// 删除项目成员
        /// </summary>
        /// <param name="projectId">The project id.</param>
        /// <param name="uids">The uids.</param>
        /// <param name="userid">The userid.</param>
        /// <param name="errmsg">The errmsg.</param>
        /// <returns></returns>
        public static int DelMembers(int projectId, int[] uids, int userid, out string errmsg)
        {
            using (SqlConnection conn = new SqlConnection(clsConfigOperate.CustomerSqlConnection()))
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();
                try
                {
                    DelMembers(projectId, uids, userid, out errmsg, trans);
                    trans.Commit();
                    conn.Close();
                    return uids.Length;
                }
                catch (Exception exp)
                {
                    trans.Rollback();
                    conn.Close();
                    errmsg = exp.Message;
                    return -2;
                }
            }
        }

        /// <summary>
        /// 删除项目组成员
        /// </summary>
        /// <param name="projectid">The projectid.</param>
        /// <param name="uids">The uids.</param>
        /// <param name="userid">The userid.</param>
        /// <param name="errmsg">The errmsg.</param>
        /// <param name="trans">The trans.</param>
        /// <returns></returns>
        public static int DelMembers(int projectid, int[] uids, int userid, out string errmsg, SqlTransaction trans)
        {
            errmsg = "删除成功!";
            try
            {
                foreach (int uid in uids)
                {
                    ProjectmembersInfo mem = ProjectmembersManager.GetModelByprojectmember(projectid, uid, trans);
                    ESP.Media.DataAccess.ProjectmembersDataProvider.DeleteInfo(mem.Id, trans);
                }
                return uids.Length;
            }
            catch (Exception exp)
            {
                errmsg = exp.Message;
                return -2;
            }
        }

        /// <summary>
        /// Adds the reporter.
        /// </summary>
        /// <param name="projectid">The projectid.</param>
        /// <param name="reporterId">The reporter id.</param>
        /// <param name="userid">The userid.</param>
        /// <param name="errmsg">The errmsg.</param>
        /// <returns></returns>
        public static int AddReporter(int projectid, int[] reporterId, int userid, out string errmsg)
        {
            errmsg = "成功!";
            using (SqlConnection conn = new SqlConnection(clsConfigOperate.CustomerSqlConnection()))
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();
                try
                {
                    //string sql = "delete from Media_projectReporterRelation where projectid = @projectid";
                    //Hashtable ht = new Hashtable();
                    //ht.Add("@projectid", projectid);
                    //SqlParameter[] param ESP.Media.Access.Utilities.Common.DictToSqlParam(ht);
                    //= ESP.Media.Access.Utilities.Common.DictToSqlParamSqlHelper.ExecuteNonQuery(trans, CommandType.Text, sql, param);

                    foreach (int rid in reporterId)
                    {
                        ProjectreporterrelationInfo emrelation = new ProjectreporterrelationInfo();
                        ReportersInfo reporter = new ReportersInfo();
                        emrelation.Projectid = projectid;
                        emrelation.Reporterid = rid;
                        emrelation.Relationdate = DateTime.Now.ToString();
                        emrelation.Relationuserid = userid;
                        emrelation.Bankacountname = reporter.Bankacountname;
                        emrelation.Bankcardcode = reporter.Bankcardcode;
                        emrelation.Bankcardname = reporter.Bankcardname;
                        emrelation.Bankname = reporter.Bankname;
                        emrelation.Cooperatecircs = reporter.Cooperatecircs;
                        emrelation.Haveinvoice = reporter.Haveinvoice;
                        emrelation.Paymentmode = reporter.Paymentmode;
                        emrelation.Privateremark = reporter.Privateremark;
                        emrelation.Referral = reporter.Referral;
                        emrelation.Paystatus = (int)Global.ReporterPayStatus.SetPrivateMsg;
                        ESP.Media.DataAccess.ProjectreporterrelationDataProvider.insertinfo(emrelation, trans);
                    }

                    trans.Commit();
                    conn.Close();
                    return reporterId.Length;
                }
                catch (Exception exp)
                {
                    trans.Rollback();
                    conn.Close();
                    errmsg = exp.Message;
                    return -2;
                }
            }
        }

        /// <summary>
        /// Dels the reporter.
        /// </summary>
        /// <param name="relationid">The relationid.</param>
        /// <param name="userid">The userid.</param>
        /// <param name="errmsg">The errmsg.</param>
        /// <returns></returns>
        public static int DelReporter(int relationid, int userid, out string errmsg)
        {
            using (SqlConnection conn = new SqlConnection(clsConfigOperate.CustomerSqlConnection()))
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();
                try
                {
                    int delid = DelReporter(relationid, out errmsg, trans);
                    trans.Commit();
                    conn.Close();
                    return delid;
                }
                catch (Exception exp)
                {
                    trans.Rollback();
                    conn.Close();
                    errmsg = exp.Message;
                    return -2;
                }
            }
        }

        /// <summary>
        /// Dels the reporter.
        /// </summary>
        /// <param name="relationid">The relationid.</param>
        /// <param name="errmsg">The errmsg.</param>
        /// <param name="trans">The trans.</param>
        /// <returns></returns>
        public static int DelReporter(int relationid, out string errmsg, SqlTransaction trans)
        {
            errmsg = "删除成功!";
            try
            {
                ESP.Media.DataAccess.ProjectreporterrelationDataProvider.DeleteInfo(relationid, trans);
                return relationid;

            }
            catch (Exception exp)
            {
                errmsg = exp.Message;
                return -2;
            }
        }

        /// <summary>
        /// Dels the reporters.
        /// </summary>
        /// <param name="relationids">The relationids.</param>
        /// <param name="userid">The userid.</param>
        /// <param name="errmsg">The errmsg.</param>
        /// <returns></returns>
        public static int DelReporters(int[] relationids, int userid, out string errmsg)
        {
            using (SqlConnection conn = new SqlConnection(clsConfigOperate.CustomerSqlConnection()))
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();
                try
                {
                    DelReporters(relationids, userid, out errmsg, trans);
                    trans.Commit();
                    conn.Close();
                    return relationids.Length;
                }
                catch (Exception exp)
                {
                    trans.Rollback();
                    conn.Close();
                    errmsg = exp.Message;
                    return -2;
                }
            }
        }

        /// <summary>
        /// Dels the reporters.
        /// </summary>
        /// <param name="relationids">The relationids.</param>
        /// <param name="userid">The userid.</param>
        /// <param name="errmsg">The errmsg.</param>
        /// <param name="trans">The trans.</param>
        /// <returns></returns>
        public static int DelReporters(int[] relationids, int userid, out string errmsg, SqlTransaction trans)
        {
            errmsg = "删除成功!";
            try
            {
                foreach (int rid in relationids)
                {
                    ESP.Media.DataAccess.ProjectreporterrelationDataProvider.DeleteInfo(rid, trans);
                }
                return relationids.Length;
            }
            catch (Exception exp)
            {
                errmsg = exp.Message;
                return -2;
            }
        }

        /// <summary>
        /// Dels the media.
        /// </summary>
        /// <param name="relationid">The relationid.</param>
        /// <param name="userid">The userid.</param>
        /// <param name="errmsg">The errmsg.</param>
        /// <returns></returns>
        public static int DelMedia(int relationid, int userid, out string errmsg)
        {
            using (SqlConnection conn = new SqlConnection(clsConfigOperate.CustomerSqlConnection()))
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();
                try
                {
                    int delid = DelMedia(relationid, out errmsg, trans);
                    trans.Commit();
                    conn.Close();
                    return delid;
                }
                catch (Exception exp)
                {
                    trans.Rollback();
                    conn.Close();
                    errmsg = exp.Message;
                    return -2;
                }
            }
        }

        /// <summary>
        /// Dels the media.
        /// </summary>
        /// <param name="relationid">The relationid.</param>
        /// <param name="errmsg">The errmsg.</param>
        /// <param name="trans">The trans.</param>
        /// <returns></returns>
        public static int DelMedia(int relationid, out string errmsg, SqlTransaction trans)
        {
            errmsg = "删除成功!";
            try
            {
                ESP.Media.DataAccess.ProjectmediarelationDataProvider.DeleteInfo(relationid, trans);
                return relationid;

            }
            catch (Exception exp)
            {
                errmsg = exp.Message;
                return -2;
            }
        }

        /// <summary>
        /// Dels the media.
        /// </summary>
        /// <param name="relationids">The relationids.</param>
        /// <param name="userid">The userid.</param>
        /// <param name="errmsg">The errmsg.</param>
        /// <returns></returns>
        public static int DelMedias(int[] relationids, int userid, out string errmsg)
        {
            using (SqlConnection conn = new SqlConnection(clsConfigOperate.CustomerSqlConnection()))
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();
                try
                {
                    DelMedias(relationids, userid, out errmsg, trans);
                    trans.Commit();
                    conn.Close();
                    return relationids.Length;
                }
                catch (Exception exp)
                {
                    trans.Rollback();
                    conn.Close();
                    errmsg = exp.Message;
                    return -2;
                }
            }
        }

        /// <summary>
        /// Dels the medias.
        /// </summary>
        /// <param name="relationids">The relationids.</param>
        /// <param name="userid">The userid.</param>
        /// <param name="errmsg">The errmsg.</param>
        /// <param name="trans">The trans.</param>
        /// <returns></returns>
        public static int DelMedias(int[] relationids, int userid, out string errmsg, SqlTransaction trans)
        {
            errmsg = "删除成功!";
            try
            {
                foreach (int rid in relationids)
                {
                    ESP.Media.DataAccess.ProjectmediarelationDataProvider.DeleteInfo(rid, trans);
                }
                return relationids.Length;
            }
            catch (Exception exp)
            {
                errmsg = exp.Message;
                return -2;
            }
        }

        /// <summary>
        /// Dels the media.
        /// </summary>
        /// <param name="mediaid">The mediaid.</param>
        /// <param name="projectid">The projectid.</param>
        /// <param name="userid">The userid.</param>
        /// <returns></returns>
        public static int DelMedia(int mediaid, int projectid, int userid)
        {
            string sql = @"delete media_projectmediarelation where mediaitemid=@mediaitemid and projectid=@projectid;";
            string sql1 = @"delete media_projectreporterrelation where projectid=@projectid and reporterid in (select reporterid from media_reporters as a where a.media_id=@mediaitemid);";
            string sql2 = @"delete media_eventmediarelation where mediaitemid=@mediaitemid and eventid in (select eventid from media_events as a where a.projectid = @projectid) ;";
            string sql3 = @"delete media_dailymediarelation where mediaitemid=@mediaitemid and dailyid in (select dailyid from media_dailys as a where a.projectid = @projectid) ;";
            string sql4 = @"delete media_eventreporterrelation where eventid in (select eventid from media_events as a where a.projectid = @projectid)  and reporterid in (select reporterid from media_reporters as a where a.media_id = @mediaitemid);";
            string sql5 = @"delete media_dailyreporterrelation where dailyid in (select dailyid from media_dailys as a where a.projectid = @projectid)  and reporterid in (select reporterid from media_reporters as a where a.media_id = @mediaitemid);";


            using (SqlConnection conn = new SqlConnection(clsConfigOperate.CustomerSqlConnection()))
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();
                try
                {
                    SqlParameter[] param = new SqlParameter[2];
                    param[0] = new SqlParameter("@projectid", SqlDbType.Int);
                    param[0].Value = projectid;
                    param[1] = new SqlParameter("@mediaitemid", SqlDbType.Int);//(int)Global.PostType.Issue);
                    param[1].Value = mediaid;
                    int delid = SqlHelper.ExecuteNonQuery(trans, CommandType.Text, sql5, param);
                    delid += SqlHelper.ExecuteNonQuery(trans, CommandType.Text, sql4, param);
                    delid += SqlHelper.ExecuteNonQuery(trans, CommandType.Text, sql3, param);
                    delid += SqlHelper.ExecuteNonQuery(trans, CommandType.Text, sql2, param);
                    delid += SqlHelper.ExecuteNonQuery(trans, CommandType.Text, sql1, param);
                    delid += SqlHelper.ExecuteNonQuery(trans, CommandType.Text, sql, param);
                    trans.Commit();
                    conn.Close();
                    return delid;
                }
                catch (Exception exp)
                {
                    trans.Rollback();
                    conn.Close();
                    string errmsg = exp.Message;
                    return -2;
                }
            }

        }

        /// <summary>
        /// Dels the reporter.
        /// </summary>
        /// <param name="reporterid">The reporterid.</param>
        /// <param name="projectid">The projectid.</param>
        /// <param name="userid">The userid.</param>
        /// <returns></returns>
        public static int DelReporter(int reporterid, int projectid, int userid)
        {
            string sql = @"delete media_projectreporterrelation where reporterid=@reporterid and projectid=@projectid;";
            string sql1 = @"delete media_eventreporterrelation where eventid in (select eventid from media_events as a where a.projectid = @projectid)  and reporterid =@reporterid;";
            string sql2 = @"delete media_dailyreporterrelation where dailyid in (select dailyid from media_dailys as a where a.projectid = @projectid)  and reporterid =@reporterid;";


            using (SqlConnection conn = new SqlConnection(clsConfigOperate.CustomerSqlConnection()))
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();
                try
                {
                    SqlParameter[] param = new SqlParameter[2];
                    param[0] = new SqlParameter("@projectid", SqlDbType.Int);
                    param[0].Value = projectid;
                    param[1] = new SqlParameter("@reporterid", SqlDbType.Int);//(int)Global.PostType.Issue);
                    param[1].Value = reporterid;
                    int delid = SqlHelper.ExecuteNonQuery(trans, CommandType.Text, sql2, param);
                    delid += SqlHelper.ExecuteNonQuery(trans, CommandType.Text, sql1, param);
                    delid += SqlHelper.ExecuteNonQuery(trans, CommandType.Text, sql, param);
                    //OperatelogManager.add((int)Global.SysOperateType.Del, (int)Global.Tables.Media, userid, trans);
                    trans.Commit();
                    conn.Close();
                    return delid;
                }
                catch (Exception exp)
                {
                    trans.Rollback();
                    conn.Close();
                    string errmsg = exp.Message;
                    return -2;
                }
            }

        }

        /// <summary>
        /// Gets the unselected media.
        /// </summary>
        /// <param name="term">The term.</param>
        /// <param name="ht">The ht.</param>
        /// <param name="projectid">The projectid.</param>
        /// <returns></returns>
        public static DataTable GetUnselectedMedia(string term, Hashtable ht, int projectid)
        {
            if (term == null) term = string.Empty;
            if (ht == null) ht = new Hashtable();
            //            term += "and (project.projectid is null or project.projectid != @projectid)";
            //            term += @"and a.mediaitemid not in 
            //(select mediaitemid from media_projectmediarelation as a where a.projectid = @projectid) ";
            //            if (!ht.ContainsKey("@projectid"))
            //            {
            //                ht.Add("@projectid", projectid);
            //            }
            //            return Media_mediaitems.GetAuditList(term, ht);


            DataTable dtProjectmedia = GetRelationMedias(projectid, null, null);
            DataTable dtAuditMedia = MediaitemsManager.GetAuditList(term, ht);
            if (dtProjectmedia == null || dtProjectmedia.Rows.Count == 0) return dtAuditMedia;
            string mediaids = string.Empty;
            foreach (DataRow dr in dtProjectmedia.Rows)
            {
                mediaids += dr["mediaitemid"] == DBNull.Value ? "0" : dr["mediaitemid"].ToString() + ",";
            }
            mediaids.Trim(',');
            term = string.Format(" mediaitemid not in ({0})", mediaids);
            DataRow[] drs = dtAuditMedia.Select(term);
            DataTable dt = dtAuditMedia.Clone();
            ESP.Media.Access.Utilities.Common.DataRowsToDataTable(drs, ref dt);
            return dt;



        }

        /// <summary>
        /// Gets the un selected event media.
        /// </summary>
        /// <param name="term">The term.</param>
        /// <param name="ht">The ht.</param>
        /// <param name="eventid">The eventid.</param>
        /// <param name="projectid">The projectid.</param>
        /// <returns></returns>
        public static DataTable GetUnSelectedEventMedia(string term, Hashtable ht, int eventid, int projectid)
        {
            if (term == null) term = string.Empty;
            if (ht == null) ht = new Hashtable();

            DataTable dteventmedia = EventsManager.GetRelationMedias(eventid);
            DataTable dtProjectmedia = GetRelationMedias(projectid, term, ht);
            if (dteventmedia == null || dteventmedia.Rows.Count == 0) return dtProjectmedia;
            string mediaids = string.Empty;
            foreach (DataRow dr in dteventmedia.Rows)
            {
                mediaids += dr["mediaitemid"] == DBNull.Value ? "0" : dr["mediaitemid"].ToString() + ",";
            }
            mediaids.Trim(',');
            term = string.Format(" mediaitemid not in ({0})", mediaids);
            DataRow[] drs = dtProjectmedia.Select(term);
            DataTable dt = dtProjectmedia.Clone();
            ESP.Media.Access.Utilities.Common.DataRowsToDataTable(drs, ref dt);
            return dt;

        }

        /// <summary>
        /// Gets the un selected daily media.
        /// </summary>
        /// <param name="term">The term.</param>
        /// <param name="ht">The ht.</param>
        /// <param name="dailyid">The dailyid.</param>
        /// <param name="projectid">The projectid.</param>
        /// <returns></returns>
        public static DataTable GetUnSelectedDailyMedia(string term, Hashtable ht, int dailyid, int projectid)
        {
            if (term == null) term = string.Empty;
            if (ht == null) ht = new Hashtable();

            DataTable dtdailymedia = DailysManager.GetRelationMedias(dailyid);
            DataTable dtProjectmedia = GetRelationMedias(projectid, term, ht);
            if (dtdailymedia == null || dtdailymedia.Rows.Count == 0) return dtProjectmedia;
            string mediaids = string.Empty;
            foreach (DataRow dr in dtdailymedia.Rows)
            {
                mediaids += dr["mediaitemid"] == DBNull.Value ? "0" : dr["mediaitemid"].ToString() + ",";
            }
            mediaids.Trim(',');
            term = string.Format(" mediaitemid not in ({0})", mediaids);
            DataRow[] drs = dtProjectmedia.Select(term);
            DataTable dt = dtProjectmedia.Clone();
            ESP.Media.Access.Utilities.Common.DataRowsToDataTable(drs, ref dt);
            return dt;
        }

        public static List<QueryMediaItemInfo> GetRelationMediasList(int projectId, string term, Hashtable ht)
        {
            DataTable dt = GetRelationMedias(projectId, term, ht);
            var query = from querymediaitem in dt.AsEnumerable() select new QueryMediaItemInfo(querymediaitem);
            List<QueryMediaItemInfo> items = new List<QueryMediaItemInfo>();
            foreach (QueryMediaItemInfo item in query)
            {
                items.Add(item);
            }
            return items;
        }

        /// <summary>
        /// Gets the relation medias.
        /// </summary>
        /// <param name="projectId">The project id.</param>
        /// <param name="term">The term.</param>
        /// <param name="ht">The ht.</param>
        /// <returns></returns>
        public static DataTable GetRelationMedias(int projectId, string term, Hashtable ht)
        {
            string sql = @"select a.mediaitemid as mediaitemid,
                            a.mediacname as mediacname,a.mediaename as mediaename,
                            a.cshortname as cshortname,a.eshortname as eshortname,
                            a.mediaitemtype as mediaitemtype,a.currentversion as currentversion,
                            a.status as status,a.createdbyuserid as createdbyuserid,
                            a.createddate as createddate,a.lastmodifiedbyuserid as lastmodifiedbyuserid,
                            a.lastmodifieddate as lastmodifieddate,a.mediumsort as mediumsort
                            ,mediatype.name as mediatypename,project.id as relationid,
                            a.RegionAttribute as RegionAttribute,a.industryid as industryid,
                            a.Countryid as Countryid,TelephoneExchange,
                            medianame = a.mediacname + ' '+a.ChannelName+' '+a.TopicName,
                            a.issueregion as issueregion,indust.industryname as industryname,
                            headquarter = country.countryname + ' '+ province.province_name + ' '+city.city_name
                            from Media_ProjectMediaRelation as project {0} where 1=1 {1}";
            string jointable = @"inner join Media_mediaitems as a on a.mediaitemid = project.mediaitemid     
                                 inner join media_mediatype as mediatype on a.mediaitemtype = mediatype.id
                                 left join Media_eventmediarelation as event on a.mediaitemid = event.mediaitemid
                                 left join Media_dailymediarelation as daily on a.mediaitemid = daily.mediaitemid

                                left join media_industries as indust on a.IndustryID = indust.IndustryID 
                                left join media_country as country on a.countryid = country.countryid
                                left join Media_province as province on province.province_id = a.provinceid
                                left join media_city as city on city.city_id = a.cityid
                                ";
            if (term == null) term = string.Empty;
            term += @" and a.del!=@del and project.del!=@del and a.status=@status and project.ProjectID = @pjid group by a.mediaitemid,mediacname,mediaename,cshortname,
                            eshortname,mediaitemtype,currentversion,
                            status,createdbyuserid,createddate,lastmodifiedbyuserid,
                            lastmodifieddate,mediumsort,mediatype.name,project.id,
                            
                            a.RegionAttribute,a.industryid,a.Countryid,TelephoneExchange,
                            a.channelname,a.topicname,a.issueregion,
                            indust.industryname,country.countryname,province.province_name,city.city_name
                            order by a.mediaitemid desc
                            ";

            if (ht == null) ht = new Hashtable();
            ht.Add("@del", (int)Global.FiledStatus.Del);
            ht.Add("@status", (int)Global.MediaAuditStatus.FirstLevelAudit);
            ht.Add("@pjid", projectId);

            sql = string.Format(sql, jointable, term);
            SqlParameter[] param =ESP.Media.Access.Utilities.Common.DictToSqlParam(ht);
            return  clsSelect.QueryBySql(sql, param);
        }

        /// <summary>
        /// Gets the relation reporters.
        /// </summary>
        /// <param name="projectId">The project id.</param>
        /// <param name="term">The term.</param>
        /// <param name="ht">The ht.</param>
        /// <returns></returns>
        public static DataTable GetRelationReporters(int projectId, string term, Hashtable ht)
        {
            string sql = @"select city.city_name as CityName,
	                        mtype.name as TypeName,
	                        media.ReaderSort as ReaderSort,
                            TopicProperty = CASE media.TopicProperty
                                 WHEN '1' THEN '新闻'
                                 WHEN '2' THEN '访谈'
                                 WHEN '3' THEN '娱乐'
                                 WHEN '4' THEN '体育'
                                 WHEN '5' THEN '教育'
                                 ELSE '无'
                              END,
	                        media.MediaCName as MediaCName,
	                        media.MediaType as MediaType,
	                        a.reporterId as reporterId,
	                        a.ReporterName as ReporterName,
	                        a.ReporterPosition as ReporterPosition,
	                        a.ReporterLevel as ReporterLevel,
                            a.cardnumber as cardnumber,a.cityid as cityid,
                            a.birthday as birthday,a.UsualMobile as UsualMobile,
                            a.Tel_O as Tel_O,a.QQ as QQ,project.id as relationid,
                            a.MSN as MSN,a.Experience as Experience,
                            sex = case a.sex when  1 then '男' when 2 then '女' else '保密' end,
                            a.ReporterPosition as ReporterPosition,
                            a.tel_o as tel,
                            a.usualmobile as mobile,a.emailone as email,
                            a.responsibledomain as responsibledomain,
                            medianame = media.mediacname + ' '+media.ChannelName+' '+media.TopicName,
                            media.mediaitemid as mediaitemid
                            
                            from Media_ProjectReporterRelation as project {1} where 1=1 {2}";

            string jointable = @"
                                inner join Media_reporters as a on project.reporterid = a.reporterid
                                inner join Media_mediaitems as media on media.MediaitemID = a.Media_ID
                                inner join media_mediatype as mtype on media.mediaitemtype = mtype.id  
                                left join media_City as city on a.cityid = city.City_ID  
                                left join Media_eventReporterrelation as event on project.reporterid = event.reporterid
                                left join Media_dailyReporterrelation as daily on project.reporterid = daily.reporterid
                                ";
            if (term == null) term = string.Empty;
            term += @" and a.del!=@del and project.del!=@del and media.status=@status and project.ProjectID = @pjid 
                            group by city.city_name,mtype.name,ReaderSort,TopicProperty,
                            MediaCName,MediaType,
	                        a.reporterId ,
	                        a.ReporterName ,
	                        a.ReporterPosition ,
	                        a.ReporterLevel ,
                            a.sex ,a.cardnumber,a.cityid,
                            a.birthday ,a.UsualMobile ,
                            a.Tel_O ,a.QQ ,project.id ,
                            a.MSN ,a.Experience,
                            a.emailone,
                            a.responsibledomain,
                            media.ChannelName,media.TopicName,media.mediaitemid
                            order by a.reporterid desc
                            ";

            if (ht == null) ht = new Hashtable();
            ht.Add("@del", (int)Global.FiledStatus.Del);
            ht.Add("@status", (int)Global.MediaAuditStatus.FirstLevelAudit);
            ht.Add("@pjid", projectId);

            sql = string.Format(sql, string.Empty, jointable, term);
            SqlParameter[] param =ESP.Media.Access.Utilities.Common.DictToSqlParam(ht);
            return clsSelect.QueryBySql(sql, param);
        }

        /// <summary>
        /// Gets the unselected reporter.
        /// </summary>
        /// <param name="projectId">The project id.</param>
        /// <param name="term">The term.</param>
        /// <param name="ht">The ht.</param>
        /// <returns></returns>
        public static DataTable GetUnselectedReporter(int projectId, string term, Hashtable ht)
        {
            string sql = @"select city.city_name as CityName,
	                        mtype.name as TypeName,
	                        a.ReaderSort  as ReaderSort,
                            TopicProperty = CASE a.TopicProperty
                                 WHEN '1' THEN '新闻'
                                 WHEN '2' THEN '访谈'
                                 WHEN '3' THEN '娱乐'
                                 WHEN '4' THEN '体育'
                                 WHEN '5' THEN '教育'
                                 ELSE '无'
                              END,
	                        a.MediaCName as MediaCName,
	                        a.MediaType as MediaType,
	                        reporter.reporterId as reporterId,
	                        reporter.ReporterName as ReporterName,
	                        reporter.ReporterPosition as ReporterPosition,
	                        reporter.ReporterLevel as ReporterLevel,
                             sex = case reporter.sex when  1 then '男' when 2 then '女' else '保密' end,
                            reporter.birthday as birthday,reporter.UsualMobile as UsualMobile,
                            reporter.Tel_O as Tel_O,reporter.QQ as QQ,
                            reporter.MSN as MSN,reporter.Experience as Experience,
                            sex = case reporter.sex when  1 then '男' when 2 then '女' else '保密' end,
                            reporter.ReporterPosition as ReporterPosition,
                            reporter.tel_o as tel,
                            reporter.usualmobile as mobile,reporter.emailone as email,
                            reporter.responsibledomain as responsibledomain,
                            medianame = a.mediacname + ' '+a.ChannelName+' '+a.TopicName
                            from media_reporters as reporter  {0} where 1=1 {1}";


            string jointable = @"
                                inner join media_mediaitems as a on reporter.media_id = a.mediaitemid
                                inner join media_projectmediarelation as project on a.mediaitemid = project.mediaitemid
                                left join media_city as city on city.city_id = reporter.cityid
                                left join media_mediatype as mtype on a.mediaitemtype = mtype.id";

            if (term == null) term = string.Empty;
            term += @"and a.del!=@del and reporter.del!=@del and project.del !=@del and a.status = @status and project.projectid = @projectid and
                            reporter.reporterid not in(select reporterid from media_projectreporterrelation as a where a.projectid = @projectid)
	                        group by mtype.name,city.city_name,a.MediaCName,a.MediaType,
		                    ReaderSort,TopicProperty,reporter.ReporterName,reporter.ReporterPosition,reporter.sex,reporter.birthday,
                            reporter.UsualMobile,reporter.Tel_O,reporter.QQ,reporter.MSN,reporter.Experience,
		                    reporter.ReporterLevel,reporter.reporterId,

                            reporter.emailone,
                            reporter.responsibledomain,
                            a.ChannelName,a.TopicName
                            order by reporter.reporterid desc
                            ";

            if (ht == null) ht = new Hashtable();
            ht.Add("@del", (int)Global.FiledStatus.Del);
            ht.Add("@status", (int)Global.MediaAuditStatus.FirstLevelAudit);
            ht.Add("@projectid", projectId);

            sql = string.Format(sql, jointable, term);
            SqlParameter[] param =ESP.Media.Access.Utilities.Common.DictToSqlParam(ht);
            return  clsSelect.QueryBySql(sql, param);
        }

        /// <summary>
        /// Gets the propagate list by projectid.
        /// </summary>
        /// <param name="projectid">The projectid.</param>
        /// <param name="propagatetype">1日常,2事件</param>
        /// <returns></returns>
        public static DataTable GetPropagateListByProjectid(int projectid, int propagatetype)
        {
            Hashtable ht = new Hashtable();
            ht.Add("@projectid", projectid);
            if (propagatetype == (int)Global.Propagatetype.Daily)
            {

                return DailysManager.GetList(" and projectid = @projectid", ht);
            }
            else if (propagatetype == (int)Global.Propagatetype.Event)
            {
                EventsManager.GetList(" and projectid = @projectid", ht);
            }
            return null;
        }

        /// <summary>
        /// Gets the propagate list by reporter.
        /// </summary>
        /// <param name="reporterid">The reporterid.</param>
        /// <param name="propagatetype">1日常,2事件</param>
        /// <returns></returns>
        public static DataTable GetPropagateListByReporter(int reporterid, int propagatetype, int projectid)
        {
            Hashtable ht = new Hashtable();

            if (propagatetype == (int)Global.Propagatetype.Daily)
            {

                return DailysManager.GetDailysByReporter(reporterid, projectid, null, null);
            }
            else if (propagatetype == (int)Global.Propagatetype.Event)
            {
                return EventsManager.GetEventsByReporter(reporterid, projectid, null, null);
            }
            return null;
        }

        [AjaxPro.AjaxMethod]
        public static string GetPropagateListByReporterJson(int reporterid, int propagatetype, int projectid)
        {
            string[] fields = { "id", "name" };
            DataTable dt = GetPropagateListByReporter(reporterid, propagatetype, projectid);
            string jsonData = "{\"totalCount\":\"" + dt.Rows.Count + "\",\"data\":[";

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    jsonData += "{";
                    for (int i = 0; i < fields.Length; i++)
                        jsonData += "\"" + fields[i] + "\":\"" + row[i].ToString() + "\",";
                    jsonData = jsonData.Substring(0, jsonData.Length - 1);
                    jsonData += "},";
                }
                jsonData = jsonData.Substring(0, jsonData.Length - 1);
                jsonData += "]}";
            }
            else
            {
                jsonData += "]}";
            }

            return jsonData;

        }
        /// <summary>
        /// Checks the relation media del.
        /// </summary>
        /// <param name="mediaitemid">The mediaitemid.</param>
        /// <param name="projectid">The projectid.</param>
        /// <returns></returns>
        public static bool CheckRelationMediaDel(int mediaitemid, int projectid)
        {
            bool nothasEvent = false;
            bool nothasDaily = false;
            using (SqlConnection conn = new SqlConnection(clsConfigOperate.CustomerSqlConnection()))
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();
                try
                {
                    string sql = @"select a.id from media_eventreporterrelation as a
                                 inner join media_reporters as b on a.reporterid = b.reporterid
                                 inner join media_mediaitems as c on b.media_id=c.mediaitemid 
                                 inner join media_events as d on a.eventid = d.eventid
                                 where c.mediaitemid = @mediaitemid and d.projectid = @projectid
                                ";
                    SqlParameter[] param = new SqlParameter[2];
                    param[0] = new SqlParameter("@projectid", SqlDbType.Int);
                    param[0].Value = projectid;
                    param[1] = new SqlParameter("@mediaitemid", SqlDbType.Int);//(int)Global.PostType.Issue);
                    param[1].Value = mediaitemid;

                    DataTable dt = clsSelect.QueryBySql(trans, sql, param);
                    if (dt == null || dt.Rows.Count == 0)
                        nothasEvent = true;
                    sql = @"select a.id from media_dailyreporterrelation as a
                                 inner join media_reporters as b on a.reporterid = b.reporterid
                                 inner join media_mediaitems as c on b.media_id=c.mediaitemid 
                                 inner join media_dailys as d on a.dailyid = d.dailyid
                                 where c.mediaitemid = @mediaitemid and d.projectid = @projectid
                                ";
                    dt = clsSelect.QueryBySql(trans, sql, param);
                    if (dt == null || dt.Rows.Count == 0)
                        nothasDaily = true;
                    trans.Rollback();
                    conn.Close();
                    return (nothasDaily && nothasEvent);
                }
                catch (Exception exception)
                {
                    string errmsg = exception.Message;
                    trans.Rollback();
                    conn.Close();
                    return false;
                }
            }
        }

        /// <summary>
        /// Checks the relation reporter.
        /// </summary>
        /// <param name="reporterid">The reporterid.</param>
        /// <param name="projectid">The projectid.</param>
        /// <returns></returns>
        public static bool CheckRelationReporter(int reporterid, int projectid)
        {
            bool nothasEvent = false;
            bool nothasDaily = false;
            using (SqlConnection conn = new SqlConnection(clsConfigOperate.CustomerSqlConnection()))
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();
                try
                {
                    string sql = @"select a.id from media_eventreporterrelation as a
                                 inner join media_reporters as b on a.reporterid = b.reporterid
                                 inner join media_events as d on a.eventid = d.eventid
                                 where b.reporterid = @reporterid and d.projectid = @projectid
                                ";
                    SqlParameter[] param = new SqlParameter[2];
                    param[0] = new SqlParameter("@projectid", SqlDbType.Int);
                    param[0].Value = projectid;
                    param[1] = new SqlParameter("@reporterid", SqlDbType.Int);//(int)Global.PostType.Issue);
                    param[1].Value = reporterid;
                    DataTable dt = clsSelect.QueryBySql(trans, sql, param);
                    if (dt == null || dt.Rows.Count == 0)
                        nothasEvent = true;
                    sql = @"select a.id from media_dailyreporterrelation as a
                                 inner join media_reporters as b on a.reporterid = b.reporterid
                                 inner join media_dailys as d on a.dailyid = d.dailyid
                                 where b.reporterid = @reporterid and d.projectid = @projectid
                                ";
                    dt = clsSelect.QueryBySql(trans, sql, param);
                    if (dt == null || dt.Rows.Count == 0)
                        nothasDaily = true;
                    trans.Rollback();
                    conn.Close();
                    return (nothasDaily && nothasEvent);
                }
                catch (Exception exception)
                {
                    string errmsg = exception.Message;
                    trans.Rollback();
                    conn.Close();
                    return false;
                }
            }
        }
    }
}
