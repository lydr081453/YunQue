using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace ESP.HumanResource.BusinessLogic
{
    public class DimissionADDetailsManager
    {
        private static readonly ESP.HumanResource.DataAccess.DimissionADDetailsDataProvider dal = new ESP.HumanResource.DataAccess.DimissionADDetailsDataProvider();
        public DimissionADDetailsManager()
        { }
        #region  成员方法

        /// <summary>
        /// 得到最大ID
        /// </summary>
        public static int GetMaxId()
        {
            return dal.GetMaxId();
        }

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public static bool Exists(int DimissionADDetailId)
        {
            return dal.Exists(DimissionADDetailId);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public static int Add(ESP.HumanResource.Entity.DimissionADDetailsInfo model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public static void Update(ESP.HumanResource.Entity.DimissionADDetailsInfo model)
        {
            dal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public static void Delete(int DimissionADDetailId)
        {
            dal.Delete(DimissionADDetailId);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public static ESP.HumanResource.Entity.DimissionADDetailsInfo GetModel(int DimissionADDetailId)
        {
            return dal.GetModel(DimissionADDetailId);
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
        public static ESP.HumanResource.Entity.DimissionADDetailsInfo GetADDetailInfo(int dimissionId)
        {
            return dal.GetADDetailInfo(dimissionId);
        }
        #endregion  成员方法
    }
}