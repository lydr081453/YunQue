using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using ESP.Compatible;
using System.Collections;
using ESP.Framework.Entity;
using ESP.Framework.BusinessLogic;
using ESP.Media.Access;
using ESP.Media.Entity;
using ESP.Media.Access.Utilities;

namespace ESP.Media.BusinessLogic
{
    public class UsersManager
    {
        public static IList<Employee> GetList()
        {
            IList<EmployeeInfo> list =  EmployeeManager.GetAll();
            if(list == null || list.Count == 0)
                return new List<Employee>(0);

            IList<Employee> copy = new List<Employee>(list.Count);
            foreach (EmployeeInfo e in list)
            {
                copy.Add(Employee.CreateFromEmployeeInfo(e));
            }

            return copy;
        }

        //public static DataTable GetListByName(string name)
        //{
        //    return Employee.GetDataSetByName(name).Tables[0];
        //}


        public static IList<Employee> GetListByName(string name)
        {
            IList<EmployeeInfo> list = EmployeeManager.SearchByChineseName(name);
            if (list == null || list.Count == 0)
                return new List<Employee>(0);

            IList<Employee> copy = new List<Employee>(list.Count);
            foreach (EmployeeInfo e in list)
            {
                copy.Add(Employee.CreateFromEmployeeInfo(e));
            }

            return copy;
        }


        //public static DataTable GetDepartmentByUser(int userid)
        //{
        //    Employee emp = new Employee(userid);
        //    return emp.GetDepartments(null);
        //}



        public static IList<Department> GetDepartmentByUser(int userid)
        {
            IList<EmployeePositionInfo> list = DepartmentPositionManager.GetEmployeePositions(userid);
            if (list == null)
                return new List<Department>();

            List<Department> list2 = new List<Department>(list.Count);
            foreach (EmployeePositionInfo epi in list)
            {
                Department d = ESP.Compatible.DepartmentManager.GetDepartmentByPK(epi.DepartmentID);
                list2.Add(d);
            }
            return list2;
        }

        public static DataTable GetReportersByBirthday(int userID,DateTime startDate,DateTime endDate)
        {
            string sql = @"SELECT distinct a.ReporterName as ReporterName,
		                    MediaName = media.mediacname + ' '+media.ChannelName+' '+media.TopicName,
                            birthday_month = MONTH(a.Birthday),birthday_day = DAY(a.Birthday),
                            a.Birthday as Birthday
                            --Birthday = convert(char(2),MONTH(a.Birthday))+ '-' + convert(char(2),DAY(a.Birthday))
                            FROM Media_Reporters as a {0} where {1}";
            string jointable = @"left join Media_mediaitems as media on a.media_id = media.mediaitemid";
            string term = @"a.del!=@del
                            and substring(convert(varchar(10),a.birthday,21),6,5) >= substring(convert(varchar(10),@sdate,21),6,5)
                            and substring(convert(varchar(10),a.birthday,21),6,5) <= substring(convert(varchar(10),@edate,21),6,5) 
                            order by birthday_month,birthday_day asc ";

            Hashtable ht = new Hashtable();
            ht.Add("@del", (int)Global.FiledStatus.Del);
            ht.Add("@sdate", startDate);
            ht.Add("@edate", endDate);

            sql = string.Format(sql, jointable, term);
            SqlParameter[] param = ESP.Media.Access.Utilities.Common.DictToSqlParam(ht);
            DataTable dt = clsSelect.QueryBySql(sql, param);
            dt.Columns.Add("SortDate",typeof(DateTime));
            foreach(DataRow row in dt.Rows)
            {
                DateTime birthday = (DateTime)row["Birthday"];
                row["SortDate"] = new DateTime(1,birthday.Month,birthday.Day);
            }
            dt.DefaultView.Sort = "SortDate ASC";
            return dt;
        }

        public static DataTable GetIntegralTopN(int count)
        {
            string sql = @"SELECT top {0} a.UserID,
                            sum(a.Integral) as Integral
                            FROM OperatelogManager as a  {1}";
            //string jointable = @"inner join framework.dbo.F_Employee as employee on employee.SysUserID = a.UserID";
            string term = @"group by a.UserID order by Integral desc";

            sql = string.Format(sql, count, term);
            return clsSelect.QueryBySql(sql);
        }

        public static DataTable GetProjects(int userid)
        {
            return ESP.Media.BusinessLogic.ProjectmembersManager.GetListByUserid(userid);
        }

        //public static DataTable GetListByProjectID(int projectid)
        //{ 
           
        //}

        public static DataTable GetNoUploadReporterList(int userid)
        {
            string sql = @"select daily.DailyName as DailyName,
	                        daily.DailyStartTime as DailyStartTime,
	                        project.ProjectName as ProjectName,
	                        member.UserID as UserID,
	                        brief.createdDate as briefcreatedDate,
	                        reporter.reportername as reportername,
                            reporter.ReporterID as ReporterID,
	                        reporter.EmailOne as Email,
	                        reporter.UsualMobile as Mobile
                            from Media_DailyReporterRelation as a {0} where {1}";

            string jointable = @"inner join media_Reporters as reporter on reporter.ReporterID = a.ReporterID
                                inner join Media_Dailys as daily on daily.DailyID = a.DailyID
                                inner join Media_projects as project on project.ProjectID = daily.ProjectID
                                inner join media_ProjectMembers as member on project.ProjectID = member.ProjectID
                                left join Media_DailyBrief as brief on daily.DailyID = brief.DailyID";

            string term = @" (brief.createdDate < daily.DailyStartTime or brief.createdDate > DATEADD(day, 15, daily.DailyStartTime) or brief.createdDate is null)
                            and UserID=@uid and a.paytype=@paytype and a.del!=@del and daily.del!=@del group by project.ProjectName,daily.DailyName,brief.createdDate,reporter.reportername,
                            member.UserID,daily.DailyStartTime,reporter.EmailOne,reporter.UsualMobile,reporter.ReporterID ";

            Hashtable ht = new Hashtable();
            ht.Add("@paytype", (int)Global.PayType.Before);
            ht.Add("@uid", userid);
            ht.Add("@del", (int)Global.FiledStatus.Del);

            sql = string.Format(sql, jointable, term);
            SqlParameter[] param = ESP.Media.Access.Utilities.Common.DictToSqlParam(ht);
            return clsSelect.QueryBySql(sql, param);
        }
    }
}
