using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace ESP.HumanResource.BusinessLogic
{
    public class DimissionGrougHRDetailsManager
    {
        private static readonly ESP.HumanResource.DataAccess.DimissionGrougHRDetailsDataProvider dal = new ESP.HumanResource.DataAccess.DimissionGrougHRDetailsDataProvider();
        public DimissionGrougHRDetailsManager()
        { }
        #region  成员方法
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public static bool Exists(int DimissionGroupHRDetails)
        {
            return dal.Exists(DimissionGroupHRDetails);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public static int Add(ESP.HumanResource.Entity.DimissionGrougHRDetailsInfo model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public static void Update(ESP.HumanResource.Entity.DimissionGrougHRDetailsInfo model)
        {
            dal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public static void Delete(int DimissionGroupHRDetails)
        {
            dal.Delete(DimissionGroupHRDetails);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public static ESP.HumanResource.Entity.DimissionGrougHRDetailsInfo GetModel(int DimissionGroupHRDetails)
        {
            return dal.GetModel(DimissionGroupHRDetails);
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public static DataSet GetList(string strWhere)
        {
            return dal.GetList(strWhere);
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public static DataSet GetAllList()
        {
            return dal.GetList("");
        }

        /// <summary>
        /// 通过离职单编号获得人力资源审批信息
        /// </summary>
        /// <param name="dimissionId">离职单编号</param>
        /// <returns></returns>
        public static ESP.HumanResource.Entity.DimissionGrougHRDetailsInfo GetGroupHRDetailInfo(int dimissionId)
        {
            return dal.GetGroupHRDetailInfo(dimissionId);
        }
        #endregion  成员方法
    }
}