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
    public class SC_LinkManManager
    {
        private readonly SC_LinkManDataProvider dal = new SC_LinkManDataProvider();
        public SC_LinkManManager()
        {}
        #region  成员方法
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(SC_LinkMan model)
        {
            model.CreatTime = DateTime.Now;
            model.LastUpdateTime = DateTime.Now;
            model.Birthday = DateTime.Now;
            return dal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public void Update(SC_LinkMan model)
        {
            model.CreatTime = DateTime.Now;
            model.LastUpdateTime = DateTime.Now;
            model.Birthday = DateTime.Now;
            dal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public void Delete(int LinkerId)
        {
            dal.Delete(LinkerId);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public SC_LinkMan GetModel(int LinkerId)
        {
            return dal.GetModel(LinkerId);
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            return dal.GetList(strWhere);
        }

        public List<SC_LinkMan> GetList(string strWhere, SqlParameter[] parameters)
        {
            return dal.GetList(strWhere, parameters);
        }

        public List<SC_LinkMan> GetListBySupplierId(int supplierid)
        {
            string strWhere = string.Empty;
            strWhere += " SupplierId=@SupplierId";
            SqlParameter[] parameters = {
					new SqlParameter("@SupplierId", SqlDbType.NVarChar,50)};
            parameters[0].Value = supplierid;

            return dal.GetList(strWhere, parameters);
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetAllList()
        {
            return dal.GetList("");
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        //public DataSet GetList(int PageSize,int PageIndex,string strWhere)
        //{
        //return dal.GetList(PageSize,PageIndex,strWhere);
        //}

        #endregion  成员方法
    }
}
