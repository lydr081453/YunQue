using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace ESP.HumanResource.BusinessLogic
{
    public class DimissionFinanceDetailsManager
    {
        private static readonly ESP.HumanResource.DataAccess.DimissionFinanceDetailsDataProvider dal = new ESP.HumanResource.DataAccess.DimissionFinanceDetailsDataProvider();
        public DimissionFinanceDetailsManager()
        { }
        #region  成员方法
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public static bool Exists(int DimissionFinanceDetailId)
        {
            return dal.Exists(DimissionFinanceDetailId);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public static int Add(ESP.HumanResource.Entity.DimissionFinanceDetailsInfo model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public static void Update(ESP.HumanResource.Entity.DimissionFinanceDetailsInfo model)
        {
            dal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public static void Delete(int DimissionFinanceDetailId)
        {
            dal.Delete(DimissionFinanceDetailId);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public static ESP.HumanResource.Entity.DimissionFinanceDetailsInfo GetModel(int DimissionFinanceDetailId)
        {
            return dal.GetModel(DimissionFinanceDetailId);
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
        public static ESP.HumanResource.Entity.DimissionFinanceDetailsInfo GetFinanceDetailInfo(int dimissionId)
        {
            return dal.GetFinanceDetailInfo(dimissionId);
        }
        #endregion  成员方法
    }
}