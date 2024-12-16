using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace ESP.HumanResource.BusinessLogic
{
    public class DimissionHRDetailsManager
    {
        private static readonly ESP.HumanResource.DataAccess.DimissionHRDetailsDataProvider dal = new ESP.HumanResource.DataAccess.DimissionHRDetailsDataProvider();
		public DimissionHRDetailsManager()
		{}
		#region  成员方法
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public static bool Exists(int DimissionHRDetailId)
		{
			return dal.Exists(DimissionHRDetailId);
		}

		/// <summary>
		/// 增加一条数据
		/// </summary>
        public static int Add(ESP.HumanResource.Entity.DimissionHRDetailsInfo model)
		{
			return dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
        public static void Update(ESP.HumanResource.Entity.DimissionHRDetailsInfo model)
		{
			dal.Update(model);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
        public static void Delete(int DimissionHRDetailId)
		{
			dal.Delete(DimissionHRDetailId);
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
        public static ESP.HumanResource.Entity.DimissionHRDetailsInfo GetModel(int DimissionHRDetailId)
		{
			return dal.GetModel(DimissionHRDetailId);
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
        public static ESP.HumanResource.Entity.DimissionHRDetailsInfo GetHRDetailInfo(int dimissionId)
        {
            return dal.GetHRDetailInfo(dimissionId);
        }
		#endregion  成员方法
	}
}