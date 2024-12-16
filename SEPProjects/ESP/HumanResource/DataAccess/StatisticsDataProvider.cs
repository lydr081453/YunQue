using System;
using System.Collections.Generic;
using ESP.HumanResource.Entity;
using System.Web;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using ESP.HumanResource.Common;

namespace ESP.HumanResource.DataAccess
{
    public class StatisticsDataProvider
    {
        public StatisticsDataProvider()
        {
        }

        /// <summary>
        /// 性别统计
        /// </summary>
        /// <returns></returns>
        public List<StatisticsInfo> getListForGender(string strWhere)
        {
            string strSql = @"select a.*,isnull(b.empNum,0) as unknow,isnull(c.empNum,0) as Male,isnull(d.empNum,0) as Female from 
                                V_Department as a 

                                left join (
                                SELECT c.departmentid,isnull(c.DepartmentName,'未知') as departmentName,count(a.userid) as empNum FROM sep_Employees as a left join 
                                sep_EmployeesInPositions b on a.userid=b.userid left join sep_Departments c on b.departmentid=c.departmentid
                                where a.status in(1,3) and a.gender = 0 group by c.DepartmentName,c.departmentid 
                                ) as b on a.level3Id = b.departmentid 

                                left join (
                                SELECT c.departmentid,isnull(c.DepartmentName,'未知') as departmentName,count(a.userid) as empNum FROM sep_Employees as a left join 
                                sep_EmployeesInPositions b on a.userid=b.userid left join sep_Departments c on b.departmentid=c.departmentid
                                where a.status in(1,3) and a.gender = 1 group by c.DepartmentName,c.departmentid
                                ) c on a.level3Id = c.departmentid

                                left join (
                                SELECT c.departmentid,isnull(c.DepartmentName,'未知') as departmentName,count(a.userid) as empNum FROM sep_Employees as a left join 
                                sep_EmployeesInPositions b on a.userid=b.userid left join sep_Departments c on b.departmentid=c.departmentid
                                where a.status in(1,3) and a.gender = 2 group by c.DepartmentName,c.departmentid
                                ) d on a.level3Id = d.departmentid where (a.level1 not like '%作废%' and a.level2 not like '%作废%' and a.level3 not like '%作废%')  order by level1 ,level2,level3";
            if (!string.IsNullOrEmpty(strWhere))
            {
                strSql += string.Format(" where 1=1 {0}",strWhere);
            }

            List<StatisticsInfo> list = new List<StatisticsInfo>();
            using (SqlDataReader r = DbHelperSQL.ExecuteReader(strSql))
            {
                while (r.Read())
                {
                    StatisticsInfo model = new StatisticsInfo();
                    model.PopupData(r);
                    list.Add(model);
                }
                r.Close();
            }
            return list;
        }

        /// <summary>
        /// 集团总人数
        /// </summary>
        /// <param name="depId"></param>
        /// <returns></returns>
        public int getCountByCo(int depId)
        {
            string strSql = String.Format(@"SELECT count(a.userid) as empNum FROM sep_Employees as a left join sep_EmployeesInPositions b on a.userid=b.userid inner join (
                                            select * from sep_Departments where parentid = (select level2id from  V_Department as a where a.level3id = {0})
                                            ) c on b.departmentid=c.departmentid where a.status in(1,3)", depId);

            int count = 0;
            using (SqlDataReader r = DbHelperSQL.ExecuteReader(strSql))
            {
                if (r.Read())
                {
                    if (r["empNum"].ToString() != "")
                        count = int.Parse(r["empNum"].ToString());
                }
                r.Close();
            }
            return count;
        }

        /// <summary>
        /// 团队总人数
        /// </summary>
        /// <param name="depId"></param>
        /// <returns></returns>
        public int getCountByGroup(int depId)
        {
            string strSql = String.Format(@"SELECT count(a.userid) as empNum 
                                            FROM sep_Employees as a left join 
                                            sep_EmployeesInPositions b on a.userid=b.userid inner join (
                                            select * from sep_Departments where departmentid = {0}
                                            ) c on b.departmentid=c.departmentid 
                                            where a.status in(1,3) ", depId);

            int count = 0;
            using (SqlDataReader r = DbHelperSQL.ExecuteReader(strSql))
            {
                if (r.Read())
                {
                    if (r["empNum"].ToString() != "")
                        count = int.Parse(r["empNum"].ToString());
                }
                r.Close();
            }
            return count;
        }

        /// <summary>
        /// 婚否统计
        /// </summary>
        /// <returns></returns>
        public List<StatisticsInfo> getListForMarried(string strWhere)
        {
            string strSql = @"select a.*,isnull(b.empNum,0) as unknow2,isnull(c.empNum,0) as Married,isnull(d.empNum,0) as Unmarried from 
                                V_Department as a 
                                 left join (
                                SELECT c.departmentid,isnull(c.DepartmentName,'未知') as departmentName,count(a.userid) as empNum FROM sep_Employees as a left join 
                                sep_EmployeesInPositions b on a.userid=b.userid left join sep_Departments c on b.departmentid=c.departmentid
                                where a.status in(1,3) and a.MaritalStatus = 0 group by c.DepartmentName,c.departmentid 
                                ) as b on a.level3Id = b.departmentid 
                                 left join (
                                SELECT c.departmentid,isnull(c.DepartmentName,'未知') as departmentName,count(a.userid) as empNum FROM sep_Employees as a left join 
                                sep_EmployeesInPositions b on a.userid=b.userid left join sep_Departments c on b.departmentid=c.departmentid
                                where a.status in(1,3) and a.MaritalStatus = 1 group by c.DepartmentName,c.departmentid
                                ) c on a.level3Id = c.departmentid
                                 left join (
                                SELECT c.departmentid,isnull(c.DepartmentName,'未知') as departmentName,count(a.userid) as empNum FROM sep_Employees as a left join 
                                sep_EmployeesInPositions b on a.userid=b.userid left join sep_Departments c on b.departmentid=c.departmentid
                                where a.status in(1,3) and a.MaritalStatus = 2 group by c.DepartmentName,c.departmentid
                                ) d on a.level3Id = d.departmentid where (a.level1 not like '%作废%' and a.level2 not like '%作废%' and a.level3 not like '%作废%')  order by level1 ,level2,level3";

            if (!string.IsNullOrEmpty(strWhere))
            {
                strSql += string.Format(" where 1=1 {0}", strWhere);
            }

            List<StatisticsInfo> list = new List<StatisticsInfo>();
            using (SqlDataReader r = DbHelperSQL.ExecuteReader(strSql))
            {
                while (r.Read())
                {
                    StatisticsInfo model = new StatisticsInfo();
                    model.PopupData2(r);
                    list.Add(model);
                }
                r.Close();
            }
            return list;
        }

        /// <summary>
        /// 年龄段统计
        /// </summary>
        /// <returns></returns>
        public List<StatisticsInfo> getListForAge(string strWhere)
        {
            string strSql = @"select a.*,isnull(b.empNum,0) as lessThan60,isnull(c.empNum,0) as year60To70, isnull(d.empNum,0) as year70To80, 
                                isnull(e.empNum,0) as year80To90, isnull(f.empNum,0) as year90To2000,isnull(g.empNum,0) as greaterThan2000,
                                isnull(h.empNum,0) as unknow3
                                from 
                                V_Department as a 

                                left join (
                                SELECT c.departmentid,isnull(c.DepartmentName,'未知') as departmentName,count(a.userid) as empNum FROM sep_Employees as a left join 
                                sep_EmployeesInPositions b on a.userid=b.userid left join sep_Departments c on b.departmentid=c.departmentid
                                where a.status in(1,3)
                                and datediff(dd, cast('1754-1-2' as datetime), a.birthday ) >= 0
                                and datediff(dd, cast('1960-01-01' as smalldatetime), a.birthday ) <= 0
                                group by c.DepartmentName,c.departmentid 
                                ) b on a.level3Id = b.departmentid

                                left join (
                                SELECT c.departmentid,isnull(c.DepartmentName,'未知') as departmentName,count(a.userid) as empNum FROM sep_Employees as a left join 
                                sep_EmployeesInPositions b on a.userid=b.userid left join sep_Departments c on b.departmentid=c.departmentid
                                where a.status in(1,3)
                                and datediff(dd, cast('1960-01-01' as smalldatetime), a.birthday ) >= 0
                                and datediff(dd, cast('1969-12-31' as smalldatetime), a.birthday ) <= 0
                                group by c.DepartmentName,c.departmentid 
                                ) c on a.level3Id = c.departmentid

                                left join (
                                SELECT c.departmentid,isnull(c.DepartmentName,'未知') as departmentName,count(a.userid) as empNum FROM sep_Employees as a left join 
                                sep_EmployeesInPositions b on a.userid=b.userid left join sep_Departments c on b.departmentid=c.departmentid
                                where a.status in(1,3)
                                and datediff(dd, cast('1970-01-01' as smalldatetime), a.birthday ) >= 0
                                and datediff(dd, cast('1979-12-31' as smalldatetime), a.birthday ) <= 0
                                group by c.DepartmentName,c.departmentid 
                                ) d on a.level3Id = d.departmentid

                                left join (
                                SELECT c.departmentid,isnull(c.DepartmentName,'未知') as departmentName,count(a.userid) as empNum FROM sep_Employees as a left join 
                                sep_EmployeesInPositions b on a.userid=b.userid left join sep_Departments c on b.departmentid=c.departmentid
                                where a.status in(1,3) 
                                and datediff(dd, cast('1980-01-01' as smalldatetime), a.birthday ) >= 0
                                and datediff(dd, cast('1989-12-31' as smalldatetime), a.birthday ) <= 0
                                group by c.DepartmentName,c.departmentid 
                                ) e on a.level3Id = e.departmentid

                                left join (
                                SELECT c.departmentid,isnull(c.DepartmentName,'未知') as departmentName,count(a.userid) as empNum FROM sep_Employees as a left join 
                                sep_EmployeesInPositions b on a.userid=b.userid left join sep_Departments c on b.departmentid=c.departmentid
                                where a.status in(1,3)
                                and datediff(dd, cast('1990-01-01' as smalldatetime), a.birthday ) >= 0
                                and datediff(dd, cast('1999-12-31' as smalldatetime), a.birthday ) <= 0
                                group by c.DepartmentName,c.departmentid 
                                ) f on a.level3Id = f.departmentid

                                left join (
                                SELECT c.departmentid,isnull(c.DepartmentName,'未知') as departmentName,count(a.userid) as empNum FROM sep_Employees as a left join 
                                sep_EmployeesInPositions b on a.userid=b.userid left join sep_Departments c on b.departmentid=c.departmentid
                                where a.status in(1,3) 
                                and datediff(dd, cast('1999-12-31' as smalldatetime), a.birthday ) >= 0
                                group by c.DepartmentName,c.departmentid 
                                ) g on a.level3Id = g.departmentid

                                left join (
                                SELECT c.departmentid,isnull(c.DepartmentName,'未知') as departmentName,count(a.userid) as empNum FROM sep_Employees as a left join 
                                sep_EmployeesInPositions b on a.userid=b.userid left join sep_Departments c on b.departmentid=c.departmentid
                                where a.status in(1,3)
                                and datediff(dd, cast('1754-1-1' as datetime), a.birthday ) =  0
                                group by c.DepartmentName,c.departmentid 
                                ) h on a.level3Id = h.departmentid where (a.level1 not like '%作废%' and a.level2 not like '%作废%' and a.level3 not like '%作废%')  order by level1 ,level2,level3";

            if (!string.IsNullOrEmpty(strWhere))
            {
                strSql += string.Format(" where 1=1 {0}", strWhere);
            }

            List<StatisticsInfo> list = new List<StatisticsInfo>();
            using (SqlDataReader r = DbHelperSQL.ExecuteReader(strSql))
            {
                while (r.Read())
                {
                    StatisticsInfo model = new StatisticsInfo();
                    model.PopupData3(r);
                    list.Add(model);
                }
                r.Close();
            }
            return list;
        }
    }
}
