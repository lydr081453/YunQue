using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESP.HumanResource.DataAccess;
using ESP.HumanResource.Entity;
using System.Data;

namespace ESP.HumanResource.BusinessLogic
{
    public class DeptSalaryManager
    {
        public readonly static DeptSalaryProvider Provider = new DeptSalaryProvider();

        public static int Add(DeptSalaryInfo model)
        {
            return Provider.Add(model);
        }

        public static bool Update(DeptSalaryInfo model)
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

        public static DeptSalaryInfo GetModel(int Id)
        {
            return Provider.GetModel(Id);
        }

        public static List<DeptSalaryInfo> GetList(string strWhere)
        {
            return Provider.GetList(strWhere);
        }


        public static DataSet GetSalaySum(string strWhere)
        {
            return Provider.GetSalaySum(strWhere);
        }


        public static DataSet GetDataSet(string groupids,string strWhere)
        {
            return Provider.GetDataSet(groupids,strWhere);
        }

        public static DataSet GetDataSetSaveData( string strWhere)
        {
            return Provider.GetDataSetSaveData(strWhere);
        }

        public static DataSet GetDataSetForExportFinance(string groupids, DateTime beginDate, DateTime endDate)
        {
            return Provider.GetDataSetForExportFinance(groupids, beginDate, endDate);
        }

        public static DataSet GetDsForExportProject(string groupid, DateTime beginDate, DateTime endDate)
        {
            return Provider.GetDsForExportProject(groupid, beginDate, endDate);
        }
        public static List<DeptSalaryInfo> GetList(int Top, string strWhere, string filedOrder)
        {
            return Provider.GetList(Top, strWhere, filedOrder);
        }

        public static int GetRecordCount(string strWhere)
        {
            return Provider.GetRecordCount(strWhere);
        }

        public static List<DeptSalaryInfo> GetListByPage(string strWhere, string orderby, int startIndex, int endIndex)
        {
            return Provider.GetListByPage(strWhere, orderby, startIndex, endIndex);
        }
    }
}
