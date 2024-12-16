using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESP.HumanResource.DataAccess;
using ESP.HumanResource.Entity;
using System.Data;

namespace ESP.HumanResource.BusinessLogic
{
    public class DeptFeeManager
    {
        public readonly static DeptFeeProvider Provider = new DeptFeeProvider();

        public static int Add(DeptFeeInfo model)
        {
            return Provider.Add(model);
        }

        public static bool Update(DeptFeeInfo model)
        {
            return Provider.Update(model);
        }

        public static bool Delete(int Id)
        {
            return Provider.Delete(Id);
        }

        public static bool DeleteList(string Idlist)
        {
            return Provider.DeleteList(Idlist);
        }

        public static DeptFeeInfo GetModel(int Id)
        {
            return Provider.GetModel(Id);
        }


        public static DeptFeeInfo GetMaxMonthModel()
        {
           return Provider.GetMaxMonthModel();
        }

        public static IList<DeptFeeInfo> GetList(string strWhere)
        {
            return Provider.GetList(strWhere);
        }

        public static IList<ESP.Finance.Entity.ReportJoinInfo> GetJoinDS(string strWhere, string strWhere2)
        {
            return Provider.GetJoinDS(strWhere, strWhere2);
        }

        public static IList<ESP.Finance.Entity.ReportDimissionInfo> GetLastDS(string strWhere, string strWhere2)
        {
            return Provider.GetLastDS(strWhere, strWhere2);
        }
        //public static DataSet GetPreMonthLastDS(string strWhere, DateTime monthSalaryDate,DateTime monthSalaryDateEnd)
        //{
        //    return Provider.GetPreMonthLastDS(strWhere, monthSalaryDate, monthSalaryDateEnd);
        //}

        public static DataSet GetTotalFeeSalaryByDeptIDsYearMonth(string groupids, int year, int month)
        {
            return Provider.GetTotalFeeSalaryByDeptIDsYearMonth(groupids, year, month);
        }

        public static List<DeptFeeInfo> GetList(int Top, string strWhere, string filedOrder)
        {
            return Provider.GetList(Top, strWhere, filedOrder);
        }

        public static int GetRecordCount(string strWhere)
        {
            return Provider.GetRecordCount(strWhere);
        }

        public static List<DeptFeeInfo> GetListByPage(string strWhere, string orderby, int startIndex, int endIndex)
        {
            return Provider.GetListByPage(strWhere, orderby, startIndex, endIndex);
        }

        public static DataSet GetFeeForProjectReportOnTimeSheet(DateTime beginTime, DateTime endTime, int projectid)
        {
            return Provider.GetFeeForProjectReportOnTimeSheet(beginTime, endTime, projectid);
        }

        public static DataSet GetMonthProjectGroupReport_Fee(DateTime beginTime, DateTime endTime,int projectid)
        {
            return Provider.GetMonthProjectGroupReport_Fee(beginTime, endTime, projectid);
        }

    }
}
