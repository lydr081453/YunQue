using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using ESP.Media.DataAccess;
using ESP.Media.Entity;

namespace ESP.Media.BusinessLogic
{
    public class AgencyManager
    {
        private readonly AgencyDataProvider dal = new AgencyDataProvider();
		public AgencyManager()
		{}
		#region  成员方法
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int AgencyID)
		{
			return dal.Exists(AgencyID);
		}

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(AgencyInfo model)
		{
			return dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public int Update(AgencyInfo model)
		{
			return dal.Update(model);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public void Delete(int AgencyID)
		{
			dal.Delete(AgencyID);
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public AgencyInfo GetModel(int AgencyID)
		{
			return dal.GetModel(AgencyID);
		}

		
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public DataSet GetList(string strWhere)
		{
			return dal.GetList(strWhere);
		}

		/// <summary>
		/// 获得数据列表
		/// </summary>
		public DataSet GetAllList()
		{
			return dal.GetList("");
		}

		

		#endregion  成员方法
    }
}
