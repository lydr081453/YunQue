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
    public class SC_SupplierActiveEmailSendLogManager
    {
        private readonly SC_SupplierActiveEmailSendLogDataProvider dal = new SC_SupplierActiveEmailSendLogDataProvider();
        public SC_SupplierActiveEmailSendLogManager()
        { }

        #region  成员方法
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(SC_SupplierActiveEmailSendLog model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public SC_SupplierActiveEmailSendLog GetModel(int id)
        {
            return dal.GetModel(id);
        }

        public List<SC_SupplierActiveEmailSendLog> GetList(string strWhere)
        {
            return dal.GetList(strWhere);
        }

        #endregion  成员方法
    }
}
