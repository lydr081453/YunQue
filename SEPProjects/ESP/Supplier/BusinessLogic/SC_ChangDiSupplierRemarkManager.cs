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
    public class SC_ChangDiSupplierRemarkManager
    {
        private readonly SC_ChangDiSupplierRemarkDataProvider dal = new SC_ChangDiSupplierRemarkDataProvider();
        public SC_ChangDiSupplierRemarkManager()
        { }

        #region  成员方法
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(SC_ChangDiSupplierRemark model)
        {
            return dal.Add(model);
        }


        /// <summary>
        /// 删除一条数据
        /// </summary>
        public void Delete(int id)
        {
            dal.Delete(id);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public SC_ChangDiSupplierRemark GetModel(int id)
        {
            return dal.GetModel(id);
        }

        public List<SC_ChangDiSupplierRemark> GetList(string strWhere)
        {
            return dal.GetList(strWhere);
        }

        #endregion  成员方法
    }
}
