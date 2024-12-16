using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESP.Framework.DataAccess;

namespace ESP.Framework.BusinessLogic
{
    /// <summary>
    /// 审核人管理
    /// </summary>
    public static class OperationAuditorManager
    {
        private static IOperationAuditorDataProvider GetProvider()
        {
            return ESP.Configuration.ProviderHelper<IOperationAuditorDataProvider>.Instance;
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public static int Add(ESP.Framework.Entity.OperationAuditorInfo model)
        {
            return GetProvider().Add(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        /// <param name="id">The id.</param>
        public static void Delete(int id)
        {
            GetProvider().Delete(id);
        }

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns></returns>
        public static bool Exists(int id)
        {
            return GetProvider().Exists(id);
        }

        /// <summary>
        /// 获得考勤审批人的sysids
        /// </summary>
        /// <returns></returns>
        public static int[] GetAllAttendanceIds()
        {
            return GetProvider().GetAllAttendanceIds();
        }

        /// <summary>
        /// 获得CEO的sysids
        /// </summary>
        /// <returns></returns>
        public static int[] GetAllCEOIds()
        {
            return GetProvider().GetAllCEOIds();
        }

        /// <summary>
        /// 获得总监的sysids
        /// </summary>
        /// <returns></returns>
        public static int[] GetAllDirectorIds()
        {
            return GetProvider().GetAllDirectorIds();
        }

        /// <summary>
        /// 获得HR审批人的sysids
        /// </summary>
        /// <returns></returns>
        public static int[] GetAllHRIds()
        {
            return GetProvider().GetAllHRIds();
        }

        /// <summary>
        /// 获得总经理的sysids
        /// </summary>
        /// <returns></returns>
        public static int[] GetAllManagerIds()
        {
            return GetProvider().GetAllManagerIds();
        }
        
        /// <summary>
        /// 获得行政管理员的sysids
        /// </summary>
        /// <returns></returns>
        public static int[] GetAllADManagerIds()
        {
            return GetProvider().GetAllADManagerIds();
        }

        private static string SplittedArray(int[] arr)
        {
            StringBuilder builder = new StringBuilder();
            foreach (int id in arr)
            {
                builder.Append(id).Append(',');
            }
            builder.Length--;
            return builder.ToString();
        }



        /// <summary>
        /// 获得HR审批人的sysids
        /// </summary>
        /// <returns></returns>
        public static string GetHRId()
        {
            return SplittedArray(GetAllHRIds());
        }

        /// <summary>
        /// 获得CEO的sysids
        /// </summary>
        /// <returns></returns>
        public static string GetCEOIds()
        {
            return SplittedArray(GetAllCEOIds());
        }

        /// <summary>
        /// 获得考勤审批人的sysids
        /// </summary>
        /// <returns></returns>
        public static string GetAttendanceId()
        {
            return SplittedArray(GetAllAttendanceIds());
        }


        /// <summary>
        /// 获得总经理的sysids
        /// </summary>
        /// <returns></returns>
        public static string GetManagerIds()
        {
            return SplittedArray(GetAllManagerIds());
        }

        /// <summary>
        /// 获得总监的sysids
        /// </summary>
        /// <returns></returns>
        public static string GetDirectorIds()
        {
            return SplittedArray(GetAllDirectorIds());
        }

        /// <summary>
        /// 获得行政管理员的sysids
        /// </summary>
        /// <returns></returns>
        public static string GetADManagerIds()
        {
            return SplittedArray(GetAllADManagerIds());
        }


        /// <summary>
        /// 获得数据列表
        /// </summary>
        /// <param name="strWhere">The STR where.</param>
        /// <returns></returns>
        public static System.Collections.Generic.IList<ESP.Framework.Entity.OperationAuditorInfo> GetList(string strWhere)
        {
            return GetProvider().GetList(strWhere);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns></returns>
        public static ESP.Framework.Entity.OperationAuditorInfo GetModel(int id)
        {
            return GetProvider().GetModel(id);
        }

        /// <summary>
        /// 根据部门ID获得一个对象实体
        /// </summary>
        /// <param name="departmentId">The dep id.</param>
        /// <returns></returns>
        public static ESP.Framework.Entity.OperationAuditorInfo GetModelByDepId(int departmentId)
        {
            return GetProvider().GetModelByDepId(departmentId);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        /// <param name="model">The model.</param>
        public static void Update(ESP.Framework.Entity.OperationAuditorInfo model)
        {
            GetProvider().Update(model);
        }
    }
}
