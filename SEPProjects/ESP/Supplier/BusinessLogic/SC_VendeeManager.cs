using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESP.Supplier.Entity;
using ESP.Supplier.DataAccess;
using System.Data;
using System.Data.SqlClient;

namespace ESP.Supplier.BusinessLogic
{
    public class SC_VendeeManager
    {
        //private readonly SC_AreaDataProvider dal = new SC_AreaDataProvider();
        private readonly SC_VendeeDataProvider dal = new SC_VendeeDataProvider();
        public SC_VendeeManager()
        { }

        //#region  成员方法
        ///// <summary>
        ///// 增加一条数据
        ///// </summary>
        //public int Add(SC_Area model)
        //{
        //    return dal.Add(model);
        //}

        ///// <summary>
        ///// 更新一条数据
        ///// </summary>
        //public void Update(SC_Area model)
        //{
        //    dal.Update(model);
        //}

        ///// <summary>
        ///// 删除一条数据
        ///// </summary>
        //public void Delete(int AreaId)
        //{
        //    dal.Delete(AreaId);
        //}

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public SC_Vendee GetModel(int Id)
        {
            return dal.GetModel(Id);
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            return dal.GetList(strWhere);
        }

        public List<SC_Vendee> GetList(string strWhere, SqlParameter[] parameters)
        {
            return dal.GetList(strWhere, parameters);
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetAllList()
        {
            return dal.GetList("");
        }

        public List<SC_Vendee> GetAllLists()
        {
            return ESP.ConfigCommon.CBO.FillCollection<SC_Vendee>(GetAllList());
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        //public DataSet GetList(int PageSize, int PageIndex, string strWhere)
        //{
        //    return dal.GetList(PageSize, PageIndex, strWhere);
        //}

        //#endregion  成员方法
    }
}
