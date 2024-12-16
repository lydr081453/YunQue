using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using ESP.Administrative.Common;
using ESP.HumanResource.Entity;

namespace ESP.HumanResource.DataAccess
{
    public class DeptSalaryProvider
    {
        public DeptSalaryProvider()
        { }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(DeptSalaryInfo model)
        {


            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into SEP_DeptSalary(");
            strSql.Append("Year,Month,Day,DepartmentId,SalaryAverage)");
            strSql.Append(" values (");
            strSql.Append("@Year,@Month,@Day,@DepartmentId,@SalaryAverage)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@Year", SqlDbType.NVarChar,50),
                    new SqlParameter("@Month", SqlDbType.DateTime),
                    new SqlParameter("@Day", SqlDbType.DateTime),
                    new SqlParameter("@DepartmentId", SqlDbType.Decimal,20),
                    new SqlParameter("@SalaryAverage", SqlDbType.Decimal,20)};
            parameters[0].Value = model.Year;
            parameters[1].Value = model.Month;
            parameters[2].Value = model.Day;
            parameters[3].Value = model.DepartmentId;
            parameters[4].Value = model.SalaryAverage;

            object obj = DbHelperSQL.GetSingle(strSql.ToString(), parameters);
            if (obj == null)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(obj);
            }
        }
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(DeptSalaryInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update SEP_DeptSalary set ");
            strSql.Append("Year=@Year,");
            strSql.Append("Month=@Month,");
            strSql.Append("Day=@Day,");
            strSql.Append("DepartmentId=@DepartmentId,");
            strSql.Append("SalaryAverage=@SalaryAverage");
            strSql.Append(" where Id=@Id");
            SqlParameter[] parameters = {
					new SqlParameter("@Year", SqlDbType.Int,4),
					new SqlParameter("@Month", SqlDbType.Int,4),
					new SqlParameter("@Day", SqlDbType.Int,4),
                    new SqlParameter("@DepartmentId", SqlDbType.Int,4),
                    new SqlParameter("@SalaryAverage", SqlDbType.Decimal,20),
                    new SqlParameter("@Id", SqlDbType.Int,4)
                                        };
            parameters[0].Value = model.Year;
            parameters[1].Value = model.Month;
            parameters[2].Value = model.Day;
            parameters[3].Value = model.DepartmentId;
            parameters[4].Value = model.SalaryAverage;
            parameters[5].Value = model.Id;

            int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int Id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from SEP_DeptSalary ");
            strSql.Append(" where Id=@Id");
            SqlParameter[] parameters = {
					new SqlParameter("@Id", SqlDbType.Int,4)
			};
            parameters[0].Value = Id;

            int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 批量删除数据
        /// </summary>
        public bool DeleteList(string Idlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from SEP_DeptSalary ");
            strSql.Append(" where Id in (" + Idlist + ")  ");
            int rows = DbHelperSQL.ExecuteSql(strSql.ToString());
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public DeptSalaryInfo GetModel(int Id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  * from SEP_DeptSalary ");
            strSql.Append(" where Id=@Id");
            SqlParameter[] parameters = {
					new SqlParameter("@Id", SqlDbType.Int,4)
			};
            parameters[0].Value = Id;

            DeptSalaryInfo model = new DeptSalaryInfo();
            return CBO.FillObject<DeptSalaryInfo>(DbHelperSQL.Query(strSql.ToString(), parameters));
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<DeptSalaryInfo> GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append(" FROM SEP_DeptSalary ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return CBO.FillCollection<DeptSalaryInfo>(DbHelperSQL.Query(strSql.ToString()));
        }


        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetSalaySum(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select SUM(SalaryAverage) as total ");
            strSql.Append(" FROM SEP_DeptSalary ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetDataSet(string groupids, string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"select distinct * from (
                            select f_project.projectid,f_project.projectcode,businessdescription,applicantuserid,applicantemployeename,
                            groupid,(d.level1+'-'+d.level2+'-'+d.level3) as deptname,status,
                            totalamount,enddate,
                            (select sum(budgetallocation) from f_supporter where projectid=f_project.projectid) as supporter,
                            (select sum(cost) from f_contractcost where projectid=f_project.projectid) as cost,
                            (select sum(expense) from f_projectexpense where projectid=f_project.projectid) as oop,
                            (select isnull( sum(isnull(PaymentFee ,0)), 0 )  from F_Payment where ProjectID = f_project.projectid and (billdate is null))+
                            (select isnull( sum(isnull(PaymentFee ,0)), 0 ) from F_Payment where ProjectID = f_project.projectid and ((billdate is not null) and billdate<=GETDATE()))as payment,
                            (select isnull( sum(isnull(PaymentFee ,0)), 0 ) from F_Payment where ProjectID = f_project.projectid and ((billdate is not null) and billdate>GETDATE()))as billPayment
                            from f_project join F_Payment b on f_project.ProjectId =b.ProjectID 
                            join v_department d on f_project.groupid =d.level3id
                            where f_project.projectcode <> '' and f_project.status!=35 and (b.costStatus is null or b.coststatus =0) ");
             if (strWhere.Trim() != "")
            {
                strSql.Append(" AND " + strWhere);
            }

            strSql.Append(@"union
select f_project.projectid,f_project.projectcode,businessdescription,applicantuserid,applicantemployeename,
groupid,(d.level1+'-'+d.level2+'-'+d.level3) as deptname,status,
totalamount,enddate,
(select sum(budgetallocation) from f_supporter where projectid=f_project.projectid) as supporter,
(select sum(cost) from f_contractcost where projectid=f_project.projectid) as cost,
(select sum(expense) from f_projectexpense where projectid=f_project.projectid) as oop,
(select isnull( sum(isnull(PaymentFee ,0)), 0 )  from F_Payment where ProjectID = f_project.projectid and (billdate is null))+
(select isnull( sum(isnull(PaymentFee ,0)), 0 ) from F_Payment where ProjectID = f_project.projectid and ((billdate is not null) and billdate<=GETDATE()))as payment,
(select isnull( sum(isnull(PaymentFee ,0)), 0 ) from F_Payment where ProjectID = f_project.projectid and ((billdate is not null) and billdate>GETDATE()))as billPayment
from f_project
join F_Payment b on f_project.ProjectId=b.ProjectID
join v_department d on f_project.groupid =d.level3id
where b.PaymentCode<>'' and f_project.status!=35 and (b.costStatus is null or b.coststatus =0) ");
            if (!string.IsNullOrEmpty(groupids))
            {
                strSql.Append(" and GroupID in("+groupids+")");
            }
            strSql.Append(" ) a order by deptname,EndDate desc ");
            return DbHelperSQL.Query(strSql.ToString());
        }

        public DataSet GetDataSetSaveData(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"select distinct * from (
                            select f_project.projectid,f_project.projectcode,businessdescription,applicantuserid,applicantemployeename,
                            groupid,(d.level1+'-'+d.level2+'-'+d.level3) as deptname,status,
                            totalamount,enddate,
                            (select sum(budgetallocation) from f_supporter where projectid=f_project.projectid) as supporter,
                            (select sum(cost) from f_contractcost where projectid=f_project.projectid) as cost,
                            (select sum(expense) from f_projectexpense where projectid=f_project.projectid) as oop,
                            (select isnull( sum(isnull(PaymentFee ,0)), 0 )  from F_Payment where ProjectID = f_project.projectid and (billdate is null))+
                            (select isnull( sum(isnull(PaymentFee ,0)), 0 ) from F_Payment where ProjectID = f_project.projectid and ((billdate is not null) and billdate<=GETDATE()))as payment,
                            (select isnull( sum(isnull(PaymentFee ,0)), 0 ) from F_Payment where ProjectID = f_project.projectid and ((billdate is not null) and billdate>GETDATE()))as billPayment
                            from f_project join F_Payment b on f_project.ProjectId =b.ProjectID 
                            join v_department d on f_project.groupid =d.level3id
                            where f_project.projectcode <> '' and f_project.status!=35 and (b.costStatus is null or b.coststatus =0) ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" AND " + strWhere);
            }

            strSql.Append(@"union
select f_project.projectid,f_project.projectcode,businessdescription,applicantuserid,applicantemployeename,
groupid,(d.level1+'-'+d.level2+'-'+d.level3) as deptname,status,
totalamount,enddate,
(select sum(budgetallocation) from f_supporter where projectid=f_project.projectid) as supporter,
(select sum(cost) from f_contractcost where projectid=f_project.projectid) as cost,
(select sum(expense) from f_projectexpense where projectid=f_project.projectid) as oop,
(select isnull( sum(isnull(PaymentFee ,0)), 0 )  from F_Payment where ProjectID = f_project.projectid and (billdate is null))+
(select isnull( sum(isnull(PaymentFee ,0)), 0 ) from F_Payment where ProjectID = f_project.projectid and ((billdate is not null) and billdate<=GETDATE()))as payment,
(select isnull( sum(isnull(PaymentFee ,0)), 0 ) from F_Payment where ProjectID = f_project.projectid and ((billdate is not null) and billdate>GETDATE()))as billPayment
from f_project
join F_Payment b on f_project.ProjectId=b.ProjectID
join v_department d on f_project.groupid =d.level3id
where b.PaymentCode<>'' and f_project.status!=35 and (b.costStatus is null or b.coststatus =0) ");

            strSql.Append(" ) a order by EndDate desc ");
            return DbHelperSQL.Query(strSql.ToString());
        }


        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetDataSetForExportFinance(string groupids, DateTime beginDate, DateTime endDate)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"select yearvalue,monthvalue,sum(fee) as Fee from (
                            select yearvalue,monthvalue,sum(fee) as Fee 
                            from 
                            (select b.groupid,a.*,CAST(CAST(yearvalue AS varchar) + '-' + CAST(monthvalue AS varchar) + '-' + CAST(1 AS varchar) AS DATETIME) as feeenddate
                             from f_projectschedule a join f_project b on a.projectid=b.projectid where (monthvalue<>'' and monthvalue<>13)) a
                             where projectid in(select projectid from f_project where projectcode<>'' and status<>35 and groupid in(" + groupids + @"))
                            and (feeenddate between '" + beginDate + @"' and '" + endDate + @"')
                            group by yearvalue,monthvalue 
                            union
                            select yearvalue,monthvalue,sum(fee) as Fee 
                            from 
                            (select b.groupid,a.*,CAST(CAST(yearvalue AS varchar) + '-' + CAST(monthvalue AS varchar) + '-' + CAST(1 AS varchar) AS DATETIME) as feeenddate
                             from dbo.F_SupporterSchedule a join f_supporter b on a.supporterid=b.supportid 
                             where (monthvalue<>'' and monthvalue<>13) and b.projectid in(select projectid from f_project where projectcode<>''and status<>35)) a
                             where groupid in(" + groupids + @")
                            and (feeenddate between '" + beginDate + @"' and '" + endDate + @"')
                            group by yearvalue,monthvalue 
                            ) a group by yearvalue,monthvalue 
                        ");
            return DbHelperSQL.Query(strSql.ToString());
        }

        public DataSet GetDsForExportProject(string groupid, DateTime beginDate, DateTime endDate)
        {
            string dateStr =string.Empty;
            string scheduleStr = string.Empty;

            if (beginDate.Year == endDate.Year)
            {
                string month = string.Empty;
                for (int i = beginDate.Month; i <= endDate.Month; i++)
                {
                    month += i + ",";
                    if (endDate.Month == 12)
                        month += "13,";
                }
                month = month.TrimEnd(',');
                dateStr = " (year=" + beginDate.Year.ToString() + " and month in(" + month + "))";
                scheduleStr = " (yearvalue=" + beginDate.Year.ToString() + " and monthvalue in(" + month + "))";
            }
            else if (endDate.Year > beginDate.Year)
            {
                dateStr = "(";
                scheduleStr = "(";
                for (int j = beginDate.Year; j <= endDate.Year; j++)
                {
                    string month = string.Empty;
                    int maxmonth = 0;
                    int beginmonth = 0;

                    if (j != endDate.Year)
                        maxmonth = 12;
                    else
                        maxmonth = endDate.Month;

                    if (j == beginDate.Year)
                        beginmonth = beginDate.Month;
                    else
                        beginmonth = 1;

                    for (int i = beginmonth; i <= maxmonth; i++)
                    {
                        month += i + ",";
                        if (i == 12)
                            month += "13,";
                    }
                    month = month.TrimEnd(',');
                    dateStr += " (year=" + j.ToString() + " and month in(" + month + ")) or ";
                    scheduleStr += " (yearvalue=" + j.ToString() + " and monthvalue in(" + month + ")) or ";
                }
                dateStr = dateStr.Substring(0, dateStr.Length - 3)+")";
                scheduleStr = scheduleStr.Substring(0, scheduleStr.Length - 3) + ")";

            }
            else
                return null;

            StringBuilder strSql = new StringBuilder();
            //(CAST(CAST(yearvalue AS varchar) + '-' + CAST(monthvalue AS varchar) + '-' + CAST(1 AS varchar) AS DATETIME)  between '" + beginDate + @"' and '" + endDate + @"')
            strSql.Append(@"select * from (
                            select distinct b.projectid,b.projectcode,b.businessdescription,b.applicantuserid,b.applicantemployeename,b.groupid,(level1+'-'+level2+'-'+level3) as deptname,b.status,0 as supportid,'主' as projecttype
                             from f_projectschedule a join f_project b on a.projectid=b.projectid 
                             join v_department c on b.groupid =c.level3id
                             left join F_ProjectReportFee d on b.projectcode =d.projectcode and b.groupid=d.deptid
                             where (monthvalue<>'' and monthvalue<>13)
                             and  b.projectcode<>'' and b.groupid in(" + groupid + @") and b.status<>35
                             and (a.Fee<>0 or d.fee<>0)
                             and (" + scheduleStr + @")
                             union

select distinct b.projectid,b.projectcode,b.businessdescription,b.applicantuserid,b.applicantemployeename,
b.groupid,(level1+'-'+level2+'-'+level3) as deptname,b.status,0 as supportid,'主' as projecttype
 from f_project b
 join v_department c on b.groupid =c.level3id
 where b.projectcode<>'' and b.status<>35 and 
 b.ProjectCode IN(select ProjectCode from F_ProjectReportFee where  DeptId in(" + groupid + @") and ProjectType ='主'
 and ("+dateStr+@")
 )
union

select distinct c.projectid,c.projectcode,c.businessdescription,b.LeaderUserID,b.LeaderUserName,b.groupid,(level1+'-'+level2+'-'+level3) as deptname,c.status ,b.supportid,'支持方' as projecttype
from f_supporter b 
join f_project c on b.projectid=c.projectid 
join v_department d on b.groupid =d.level3id 
where 
b.projectcode<>'' and c.status<>35 and b.groupid in(" + groupid + @") and
 b.ProjectCode IN(select ProjectCode from F_ProjectReportFee where  DeptId in(" + groupid + @") and ProjectType ='支'
 and (" + dateStr + @")
 )
 
                            union
                            select distinct c.projectid,c.projectcode,c.businessdescription,b.LeaderUserID,b.LeaderUserName,b.groupid,(level1+'-'+level2+'-'+level3) as deptname,c.status ,b.supportid,'支持方' as projecttype
                            from dbo.F_SupporterSchedule a join f_supporter b on a.SupporterID=b.SupportID 
                            join f_project c on b.projectid=c.projectid 
                            join v_department d on b.groupid =d.level3id 
                            where (monthvalue<>'' and monthvalue<>13) 
                            and  b.projectcode<>'' and b.groupid in(" + groupid + @") and c.status<>35 and (a.Fee<>0)
                             and (" + scheduleStr + @")
                            ) a order by groupid, projectcode,projecttype");

            return DbHelperSQL.Query(strSql.ToString());
        }


        /// <summary>
        /// 获得前几行数据
        /// </summary>
        public List<DeptSalaryInfo> GetList(int Top, string strWhere, string filedOrder)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ");
            if (Top > 0)
            {
                strSql.Append(" top " + Top.ToString());
            }
            strSql.Append(" * ");
            strSql.Append(" FROM SEP_DeptSalary ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return CBO.FillCollection<DeptSalaryInfo>(DbHelperSQL.Query(strSql.ToString()));
        }

        /// <summary>
        /// 获取记录总数
        /// </summary>
        public int GetRecordCount(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) FROM SEP_DeptSalary ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            object obj = DbHelperSQL.GetSingle(strSql.ToString());
            if (obj == null)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(obj);
            }
        }
        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        public List<DeptSalaryInfo> GetListByPage(string strWhere, string orderby, int startIndex, int endIndex)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM ( ");
            strSql.Append(" SELECT ROW_NUMBER() OVER (");
            if (!string.IsNullOrEmpty(orderby.Trim()))
            {
                strSql.Append("order by T." + orderby);
            }
            else
            {
                strSql.Append("order by T.Id desc");
            }
            strSql.Append(")AS Row, T.*  from SEP_DeptSalary T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                strSql.Append(" WHERE " + strWhere);
            }
            strSql.Append(" ) TT");
            strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return CBO.FillCollection<DeptSalaryInfo>(DbHelperSQL.Query(strSql.ToString()));
        }
    }
}
