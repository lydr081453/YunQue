using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESP.Administrative.Entity;
using ESP.Administrative.DataAccess;
using System.Data;
using ESP.Administrative.Common;

namespace ESP.Administrative.BusinessLogic
{
    public class CardStoreManager
    {
        private readonly CardStoreDataProvider dal = new CardStoreDataProvider();
        public CardStoreManager()
        { }

		#region  成员方法
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int ID)
		{
			return dal.Exists(ID);
		}

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(CardStoreInfo model)
		{
			return dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
        public void Update(CardStoreInfo model)
		{
			dal.Update(model);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public void Delete(int ID)
		{
			dal.Delete(ID);
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
        public CardStoreInfo GetModel(int ID)
		{
			return dal.GetModel(ID);
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

        /// <summary>
        /// 获得最小的门卡记录信息对象
        /// </summary>
        /// <returns>返回一个门卡记录信息对象</returns>
        public CardStoreInfo GetFirstCardModel()
        {
            return dal.GetFirstCardModel();
        }

        /// <summary>
        /// 各地区获得各地区的可用门卡信息
        /// </summary>
        /// <param name="areaid">地区编号</param>
        /// <returns>返回一个可以使用门卡信息</returns>
        public CardStoreInfo GetFirstCardModel(int areaid)
        {
            return dal.GetFirstCardModel(areaid);
        }

        /// <summary>
        /// 检查门卡号是否存在门卡信息库中
        /// </summary>
        /// <param name="cardno">门卡号</param>
        /// <returns>如果存在于门卡信息库返回true,否则返回false</returns>
        public CardStoreInfo GetModelByCardNo(string cardno)
        {
            return dal.GetModelByCardNo(cardno);
        }

        /// <summary>
        /// 获得门卡库存数量
        /// </summary>
        /// <returns>返回一个门卡库存数量值</returns>
        public int GetCardStoreCount(int areaid)
        {
            return dal.GetCardStoreCount(areaid);
        }
		#endregion  成员方法
	}
}

