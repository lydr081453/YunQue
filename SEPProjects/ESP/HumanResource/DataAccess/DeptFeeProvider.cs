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
    public class DeptFeeProvider
    {
        public DeptFeeProvider()
        { }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(DeptFeeInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into SEP_DeptFee(");
            strSql.Append("DeptId,DeptName,TotalFee,TotalSalary,AdvancePaid,Year,Month,CurrentDate,EmpAmounts,OtherFee)");
            strSql.Append(" values (");
            strSql.Append("@DeptId,@DeptName,@TotalFee,@TotalSalary,@Year,@Month,@CurrentDate,@EmpAmounts,@OtherFee)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@DeptId", SqlDbType.Int,8),
					new SqlParameter("@DeptName", SqlDbType.NVarChar,50),
					new SqlParameter("@Year", SqlDbType.Int,4),
                    new SqlParameter("@Month", SqlDbType.Int,4),
                    new SqlParameter("@TotalFee", SqlDbType.Decimal,20),
                    new SqlParameter("@TotalSalary", SqlDbType.Decimal,20),
                    new SqlParameter("@AdvancePaid", SqlDbType.Decimal,20),
                    new SqlParameter("@CurrentDate", SqlDbType.DateTime),
                    new SqlParameter("@EmpAmounts", SqlDbType.Int,6),
                    new SqlParameter("@OtherFee", SqlDbType.Decimal,20) };
            parameters[0].Value = model.DeptId;
            parameters[1].Value = model.DeptName;
            parameters[2].Value = model.Year;
            parameters[3].Value = model.Month;
            parameters[4].Value = model.TotalFee;
            parameters[5].Value = model.TotalSalary;
            parameters[6].Value = model.AdvancePaid;
            parameters[7].Value = DateTime.Now;
            parameters[8].Value = model.EmpAmounts;
            parameters[9].Value = model.OtherFee;
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
        public bool Update(DeptFeeInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update SEP_DeptFee set ");
            strSql.Append("DeptId=@DeptId,");
            strSql.Append("DeptName=@DeptName,");
            strSql.Append("Year=@Year,");
            strSql.Append("Month=@Month,");
            strSql.Append("TotalFee=@TotalFee,");
            strSql.Append("TotalSalary=@TotalSalary,AdvancePaid=@AdvancePaid,");
            strSql.Append("CurrentDate=@CurrentDate,");
            strSql.Append("EmpAmounts=@EmpAmounts,OtherFee=@OtherFee ");
            strSql.Append(" where Id=@Id");
            SqlParameter[] parameters = {
					new SqlParameter("@DeptId", SqlDbType.Int,8),
					new SqlParameter("@DeptName", SqlDbType.NVarChar,50),
					new SqlParameter("@Year", SqlDbType.Int,4),
					new SqlParameter("@Month", SqlDbType.Int,4),
                    new SqlParameter("@TotalFee", SqlDbType.Decimal,20),
                    new SqlParameter("@TotalSalary", SqlDbType.Decimal,20),
                     new SqlParameter("@AdvancePaid", SqlDbType.Decimal,20),
                    new SqlParameter("@CurrentDate", SqlDbType.DateTime),
                    new SqlParameter("@EmpAmounts", SqlDbType.Int,6),
                    new SqlParameter("@OtherFee", SqlDbType.Decimal,20),
                    new SqlParameter("@Id", SqlDbType.Int,8)
                                        };
            parameters[0].Value = model.DeptId;
            parameters[1].Value = model.DeptName;
            parameters[2].Value = model.Year;
            parameters[3].Value = model.Month;
            parameters[4].Value = model.TotalFee;
            parameters[5].Value = model.TotalSalary;
            parameters[6].Value = model.AdvancePaid;
            parameters[7].Value = DateTime.Now;
            parameters[8].Value = model.EmpAmounts;
            parameters[9].Value = model.OtherFee;
            parameters[10].Value = model.Id;

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
            strSql.Append("delete from SEP_DeptFee ");
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
            strSql.Append("delete from SEP_DeptFee ");
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
        public DeptFeeInfo GetModel(int Id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  * from SEP_DeptFee ");
            strSql.Append(" where Id=@Id");
            SqlParameter[] parameters = {
					new SqlParameter("@Id", SqlDbType.Int,4)
			};
            parameters[0].Value = Id;

            DeptFeeInfo model = new DeptFeeInfo();
            return CBO.FillObject<DeptFeeInfo>(DbHelperSQL.Query(strSql.ToString(), parameters));
        }

        public DeptFeeInfo GetMaxMonthModel()
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 * from SEP_DeptFee ");
            strSql.Append(" order by year desc,month desc");
        
            DeptFeeInfo model = new DeptFeeInfo();
            return CBO.FillObject<DeptFeeInfo>(DbHelperSQL.Query(strSql.ToString()));
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public IList<DeptFeeInfo> GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append(" FROM SEP_DeptFee ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return CBO.FillCollection<DeptFeeInfo>(DbHelperSQL.Query(strSql.ToString()));
        }

        /// <summary>
        /// 根据部门Ids、年、月获得数据集
        /// </summary>
        public DataSet GetTotalFeeSalaryByDeptIDsYearMonth(string groupids, int year, int month)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Sum(TotalFee) AS TotalFee, Sum(TotalSalary) AS TotalSalary ");
            strSql.Append(" FROM SEP_DeptFee ");
            strSql.Append(" where DeptId in (" + groupids + ") and [YEAR]=" + year + " and [month]=" + month);
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 获得前几行数据
        /// </summary>
        public List<DeptFeeInfo> GetList(int Top, string strWhere, string filedOrder)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ");
            if (Top > 0)
            {
                strSql.Append(" top " + Top.ToString());
            }
            strSql.Append(" * ");
            strSql.Append(" FROM SEP_DeptFee ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return CBO.FillCollection<DeptFeeInfo>(DbHelperSQL.Query(strSql.ToString()));
        }

        /// <summary>
        /// 获取记录总数
        /// </summary>
        public int GetRecordCount(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) FROM SEP_DeptFee ");
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
        public List<DeptFeeInfo> GetListByPage(string strWhere, string orderby, int startIndex, int endIndex)
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
            strSql.Append(")AS Row, T.*  from SEP_DeptFee T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                strSql.Append(" WHERE " + strWhere);
            }
            strSql.Append(" ) TT");
            strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return CBO.FillCollection<DeptFeeInfo>(DbHelperSQL.Query(strSql.ToString()));
        }




        /// <summary>
        /// 获得Join数据集
        /// </summary>
        public IList<ESP.Finance.Entity.ReportJoinInfo> GetJoinDS(string strWhere, string strWhere2)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("Select distinct b.userid,us.LastNameCN+us.FirstNameCN as username,emp.code,a.joindate,b.departmentid,c.departmentpositionname,e.chargerate,e.salaryhigh,e.salarylow,e.levelname ");
            strSql.Append(@" ,(v.level1+' - '+v.level2+' - '+v.level3) as departmentname,dead.salarydate,ha.id as HeadCountId,'入职' as operationType FROM sep_employees emp 
join sep_employeejobinfo a on emp.userid =a.sysid
join sep_employeesinpositions b on a.sysid =b.userid 
join sep_departmentpositions c on b.departmentpositionid = c.departmentpositionid
join sep_positionbase d on c.positionbaseid =d.id
join sep_positionlevels e on d.leveid=e.id 
join v_department v on c.departmentid =v.level3id
join sep_Users us on emp.UserID = us.UserID
join f_deadline dead on year(a.joindate)=dead.deadlineyear and month(a.joindate)=dead.deadlinemonth
left join Sep_HeadAccount ha on emp.userid=ha.OfferLetterUserId
");
            strSql.Append(" Where emp.status not in(6,11,12,10,9,-1,0,13)");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" and " + strWhere);
            }
            strSql.Append(" union ");

            strSql.Append(@" select emp.userid,us.LastNameCN+us.FirstNameCN as username,emp.code,a.TransInDate,a.NewGroupId, 
a.NewPositionName,e.chargerate,e.salaryhigh,e.salarylow,e.levelname 
,(v.level1+' - '+v.level2+' - '+v.level3) as departmentname,
dead.salarydate,ha.id as HeadCountId,'转入' as operationType from SEP_Transfer a join 
 sep_employees emp
on emp.userid=a.TransId
join sep_departmentpositions c on a.OldPositionId = c.departmentpositionid
join sep_positionbase d on c.positionbaseid =d.id
join sep_positionlevels e on d.leveid=e.id 
join v_department v on c.departmentid =v.level3id
join sep_Users us on emp.UserID = us.UserID 
join f_deadline dead on year(a.TransInDate)=dead.deadlineyear and month(a.TransInDate)=dead.deadlinemonth
left join Sep_HeadAccount ha on emp.userid=ha.OfferLetterUserId where 1=1");

            if (strWhere2.Trim() != "")
            {
                strSql.Append(" and " + strWhere2);
            }

            return CBO.FillCollection<ESP.Finance.Entity.ReportJoinInfo>(DbHelperSQL.Query(strSql.ToString()));
        }

        /// <summary>
        /// 获得Last数据集
        /// </summary>
        public IList<ESP.Finance.Entity.ReportDimissionInfo> GetLastDS(string strWhere,string strWhere2)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"Select distinct a.dimissionid,emp.userid,us.LastNameCN+us.FirstNameCN as username,emp.code,a.lastday,a.departmentid, c.departmentpositionname,e.chargerate,e.salaryhigh,e.salarylow,e.levelname ");
            strSql.Append(@",(v.level1+' - '+v.level2+' - '+v.level3) as departmentname,f.auditdate,a.status,dead.salarydate,'离职' as operationType From sep_dimissionform a join sep_employees emp
on emp.userid=a.userid
join sep_employeesinpositions b on emp.userid =b.userid 
join sep_departmentpositions c on b.departmentpositionid = c.departmentpositionid
join sep_positionbase d on c.positionbaseid =d.id
join sep_positionlevels e on d.leveid=e.id 
join v_department v on c.departmentid =v.level3id
join sep_Users us on emp.UserID = us.UserID 
join (select formid,max(auditdate) auditdate from sep_auditlog where formtype=4 group by formid ) f
on a.dimissionid=f.formid
join f_deadline dead on year(a.lastday)=dead.deadlineyear and month(a.lastday)=dead.deadlinemonth
");
            strSql.Append(" Where 1=1 and emp.status not in(6) ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" And " + strWhere);
            }

            strSql.Append(" union ");

            strSql.Append(@" select 0,emp.userid,us.LastNameCN+us.FirstNameCN as username,emp.code,a.TransOutDate,a.OldGroupId, 
a.OldPositionName,e.chargerate,e.salaryhigh,e.salarylow,e.levelname 
,(v.level1+' - '+v.level2+' - '+v.level3) as departmentname,
a.TransOutDate,a.status,dead.salarydate,'转出' as operationType from SEP_Transfer a join 
 sep_employees emp
on emp.userid=a.TransId
join sep_departmentpositions c on a.OldPositionId = c.departmentpositionid
join sep_positionbase d on c.positionbaseid =d.id
join sep_positionlevels e on d.leveid=e.id 
join v_department v on c.departmentid =v.level3id
join sep_Users us on emp.UserID = us.UserID 
join (select formid,max(auditdate) auditdate from sep_auditlog where formtype=3 group by formid ) f
on a.Id=f.formid
join f_deadline dead on year(a.TransOutDate)=dead.deadlineyear and month(a.TransOutDate)=dead.deadlinemonth
Where 1=1  ");
            if (strWhere2.Trim() != "")
            {
                strSql.Append(" And " + strWhere2);
            }


            return CBO.FillCollection<ESP.Finance.Entity.ReportDimissionInfo>(DbHelperSQL.Query(strSql.ToString()));
        }




        /// <summary>
        /// 获得项目报告中的服务费的值（timasheet）
        /// </summary>
        /// <returns>DataSet ds</returns>
        public DataSet GetFeeForProjectReportOnTimeSheet(DateTime beginTime, DateTime endTime, int projectId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"Select ScheduleID,ProjectID,YearValue,monthValue,MonthPercent,ISNULL(fee,0)as fee 
From dbo.F_ProjectSchedule
Where (YearValue>='"+beginTime.Year+"' AND monthValue>='"+beginTime.Month+"') AND (YearValue<='"+endTime.Year+"' AND monthValue<='"+endTime.Month+@"')
AND ProjectID="+projectId);

            return DbHelperSQL.Query(strSql.ToString());
        }



        /// <summary>
        /// 获得月度报告-团队(获得TimeSheet中Fees)
        /// </summary>
        /// <returns>DataSet ds</returns>
        public DataSet GetMonthProjectGroupReport_Fee(DateTime beginTime, DateTime endTime, int projectid)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"select *
from dbo.F_ProjectSchedule
where (YearValue>='" + beginTime.Year + "' AND monthValue>='" + beginTime.Month + "') and (YearValue<='" + endTime.Year + "' AND monthValue<='" + endTime.Month + @"')
and projectid=" + projectid);
            return DbHelperSQL.Query(strSql.ToString());
        }
    }
}
