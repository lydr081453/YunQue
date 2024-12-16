using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace ESP.HumanResource.BusinessLogic
{
    public class DimissionITDetailsManager
    {
        private static readonly ESP.HumanResource.DataAccess.DimissionITDetailsDataProvider dal = new ESP.HumanResource.DataAccess.DimissionITDetailsDataProvider();
        public DimissionITDetailsManager()
		{}
		#region  成员方法
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public static bool Exists(int DimissionITDetailId)
		{
			return dal.Exists(DimissionITDetailId);
		}

		/// <summary>
		/// 增加一条数据
		/// </summary>
        public static int Add(ESP.HumanResource.Entity.DimissionITDetailsInfo model)
		{
			return dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
        public static void Update(ESP.HumanResource.Entity.DimissionITDetailsInfo model)
		{
			dal.Update(model);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
        public static void Delete(int DimissionITDetailId)
		{
			dal.Delete(DimissionITDetailId);
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
        public static ESP.HumanResource.Entity.DimissionITDetailsInfo GetModel(int DimissionITDetailId)
		{
			return dal.GetModel(DimissionITDetailId);
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
        public static ESP.HumanResource.Entity.DimissionITDetailsInfo GetITDetailInfo(int dimissionId)
        {
            return dal.GetITDetailInfo(dimissionId);
        }
		#endregion  成员方法
	}
}
