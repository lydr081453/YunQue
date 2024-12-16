using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESP.Finance.Utility;
namespace ESP.Finance.BusinessLogic
{
     
     
    public static class DeadLineManager
    {

        private static ESP.Finance.IDataAccess.IDeadLineDataProvider DataProvider{get{return ESP.Configuration.ProviderHelper<ESP.Finance.IDataAccess.IDeadLineDataProvider>.Instance;}}
        //private const string _dalProviderName = "DeadLineDALProvider";

        


        #region  成员方法


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public static int Add(ESP.Finance.Entity.DeadLineInfo model)
        {
            IList<ESP.Finance.Entity.DeadLineInfo> list = GetList(n => n.DeadLineYear == model.DeadLineYear && n.DeadLineMonth == model.DeadLineMonth && n.DeadLineDay==model.DeadLineDay);//按年月查重
            if (list != null && list.Count > 0)
            {
                return 0;
            }
           return DataProvider.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public static UpdateResult Update(ESP.Finance.Entity.DeadLineInfo model)
        {
            int res = 0;
            try
            {
                res = DataProvider.Update(model);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return UpdateResult.Failed;
            }
            if (res > 0)
            {
                return UpdateResult.Succeed;
            }
            else if (res == 0)
            {
                return UpdateResult.UnExecute;
            }
            return UpdateResult.Failed;
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public static DeleteResult Delete(int DeadLineID)
        {
            int res = 0;
            try
            {
                res = DataProvider.Delete(DeadLineID);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return DeleteResult.Failed;
            }
            if (res > 0)
            {
                return DeleteResult.Succeed;
            }
            else if (res == 0)
            {
                return DeleteResult.UnExecute;
            }
            return DeleteResult.Failed;
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public static ESP.Finance.Entity.DeadLineInfo GetModel(int DeadLineID)
        {

            return DataProvider.GetModel(DeadLineID);
        }

        public static ESP.Finance.Entity.DeadLineInfo GetMonthModel(int year ,int month)
        {

            return DataProvider.GetMonthModel(year, month);
        }
        
        public static ESP.Finance.Entity.DeadLineInfo GetCurrentMonthModel()
        {
            int currentYear = DateTime.Now.Year;
            int currentMonth = DateTime.Now.Month;
            int currentDay = DateTime.Now.Day;
            IList<ESP.Finance.Entity.DeadLineInfo> Lists = GetAllList();
            var ModelRequest = from m in Lists
                          where m.DeadLineYear == currentYear && m.DeadLineMonth==currentMonth 
                          select m;

            Entity.DeadLineInfo model = null;
            try
            {
                model = ModelRequest.First<ESP.Finance.Entity.DeadLineInfo>();
            }
            catch
            {
                model = null;
            }
           if (model == null)
           {
               model = new ESP.Finance.Entity.DeadLineInfo();
               model.ProjectDeadLineDay = DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month);
               model.ProjectDeadLineMonth = DateTime.Now.Month;
               model.ProjectDeadLineYear = DateTime.Now.Year;
               model.ProjectDeadLine = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month));

               model.DeadLineDay = DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month);
               model.DeadLineMonth = DateTime.Now.Month;
               model.DeadLineYear = DateTime.Now.Year;
               model.DeadLine = new DateTime(DateTime.Now.Year, DateTime.Now.Month, model.DeadLineDay);

               model.DeadLineDay2 = DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month);
               model.DeadLineMonth2 = DateTime.Now.Month;
               model.DeadLineYear2 = DateTime.Now.Year;
               model.DeadLine2 = new DateTime(DateTime.Now.Year, DateTime.Now.Month, model.DeadLineDay);
           }
           return model;
        }
                
        public static ESP.Finance.Entity.DeadLineInfo GetPreviousMonthModel()
        {
            int currentYear = DateTime.Now.AddMonths(-1).Year;
            int preMonth = DateTime.Now.AddMonths(-1).Month;
            int currentDay = DateTime.Now.Day;
            IList<ESP.Finance.Entity.DeadLineInfo> Lists = GetAllList();
            var ModelRequest = from m in Lists
                               where m.DeadLineYear == currentYear && m.DeadLineMonth == preMonth 
                          select m;

            Entity.DeadLineInfo model = null;
            try
            {
                model = ModelRequest.First<ESP.Finance.Entity.DeadLineInfo>();
            }
            catch
            {
                model = null;
            }
           if (model == null)
           {
               model = new ESP.Finance.Entity.DeadLineInfo();
               model.ProjectDeadLineDay = DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month);
               model.ProjectDeadLineMonth = DateTime.Now.Month;
               model.ProjectDeadLineYear = DateTime.Now.Year;
               model.ProjectDeadLine = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month));

               model.DeadLineDay = DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month);
               model.DeadLineMonth = DateTime.Now.Month;
               model.DeadLineYear = DateTime.Now.Year;
               model.DeadLine = new DateTime(DateTime.Now.Year, DateTime.Now.Month, model.DeadLineDay);

               model.DeadLineDay2 = DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month);
               model.DeadLineMonth2 = DateTime.Now.Month;
               model.DeadLineYear2 = DateTime.Now.Year;
               model.DeadLine2 = new DateTime(DateTime.Now.Year, DateTime.Now.Month, model.DeadLineDay);
           }
           return model;
        }
        public static ESP.Finance.Entity.DeadLineInfo GetNextMonthModel()
        {
            int currentYear = DateTime.Now.Year;
            int currentMonth = DateTime.Now.Month+1;
            int currentDay = DateTime.Now.Day;
            if (currentMonth == 13)
            {
                currentYear = currentYear + 1;
                currentMonth = 1;
            }
            IList<ESP.Finance.Entity.DeadLineInfo> Lists = GetAllList();
            var ModelRequest = from m in Lists
                               where m.DeadLineYear == currentYear && m.DeadLineMonth == currentMonth
                               select m;

            Entity.DeadLineInfo model = null;
            try
            {
                model = ModelRequest.First<ESP.Finance.Entity.DeadLineInfo>();
            }
            catch
            {
                model = null;
            }
            return model;
        }

        #region 获得数据列表
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public static IList<ESP.Finance.Entity.DeadLineInfo> GetAllList()
        {
            return GetList(string.Empty);
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public static IList<ESP.Finance.Entity.DeadLineInfo> GetList(string term)
        {
            return GetList(term, null);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public static IList<ESP.Finance.Entity.DeadLineInfo> GetList(string term, List<System.Data.SqlClient.SqlParameter> param)
        {
            return DataProvider.GetList(term, param);
        }

        public static IList<ESP.Finance.Entity.DeadLineInfo> GetList(Func<ESP.Finance.Entity.DeadLineInfo, bool> predicate)
        {
            return GetAllList().Where(predicate).ToList<ESP.Finance.Entity.DeadLineInfo>();
        }

        public static IList<ESP.Finance.Entity.DeadLineInfo> GetList(Func<ESP.Finance.Entity.DeadLineInfo, int, bool> predicate)
        {
            return GetAllList().Where(predicate).ToList<ESP.Finance.Entity.DeadLineInfo>();
        }
        #endregion 获得数据列表
        #endregion  成员方法


    }
}
