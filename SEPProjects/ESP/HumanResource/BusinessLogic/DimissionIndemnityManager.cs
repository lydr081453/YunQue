using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using ESP.HumanResource.DataAccess;
using ESP.HumanResource.Entity;

namespace ESP.HumanResource.BusinessLogic
{
    public class DimissionIndemnityManager
    {
        private static readonly DimissionIndemnityDataProvider dal = new DimissionIndemnityDataProvider();
        public DimissionIndemnityManager()
		{}
		#region  Method

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
        public static bool Exists(int DimissionIndemnityId)
		{
			return dal.Exists(DimissionIndemnityId);
		}

		/// <summary>
		/// 增加一条数据
		/// </summary>
        public static void Add(DimissionIndemnityInfo model)
		{
			dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
        public static bool Update(DimissionIndemnityInfo model)
		{
			return dal.Update(model);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
        public static bool Delete(int DimissionIndemnityId)
		{
			
			return dal.Delete(DimissionIndemnityId);
		}
		/// <summary>
		/// 删除一条数据
		/// </summary>
        public static bool DeleteList(string DimissionIndemnityIdlist)
		{
			return dal.DeleteList(DimissionIndemnityIdlist );
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
        public static DimissionIndemnityInfo GetModel(int DimissionIndemnityId)
		{
			
			return dal.GetModel(DimissionIndemnityId);
		}

		/// <summary>
		/// 获得数据列表
		/// </summary>
        public static DataSet GetList(string strWhere)
		{
			return dal.GetList(strWhere);
		}
		/// <summary>
		/// 获得前几行数据
		/// </summary>
        public static DataSet GetList(int Top, string strWhere, string filedOrder)
		{
			return dal.GetList(Top,strWhere,filedOrder);
		}

		/// <summary>
		/// 获得数据列表
		/// </summary>
        public static List<DimissionIndemnityInfo> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}

		/// <summary>
		/// 获得数据列表
		/// </summary>
        public static List<DimissionIndemnityInfo> DataTableToList(DataTable dt)
		{
            List<DimissionIndemnityInfo> modelList = new List<DimissionIndemnityInfo>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
                DimissionIndemnityInfo model;
				for (int n = 0; n < rowsCount; n++)
				{
                    model = new DimissionIndemnityInfo();
                    model.PopupData(dt.Rows[n]);
					modelList.Add(model);
				}
			}
			return modelList;
		}

		/// <summary>
		/// 获得数据列表
		/// </summary>
        public static DataSet GetAllList()
		{
			return dal.GetList("");
		}

        /// <summary>
        /// 通过离职单编号获得赔款项信息。
        /// </summary>
        /// <param name="dimissionId">离职单编号</param>
        /// <returns>返回离职赔款项信息集合</returns>
        public static List<DimissionIndemnityInfo> GetDimissionIndemnityInfo(int dimissionId)
        {
            return dal.GetDimissionIndemnityList(dimissionId);
        }
		#endregion  Method
    }
}
