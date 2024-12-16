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
    public class SC_SupplierHistoryManager
    {
        private readonly SC_SupplierHistoryDataProvider dal = new SC_SupplierHistoryDataProvider();
        public SC_SupplierHistoryManager()
        { }
        #region  成员方法

        public SC_SupplierHistory SetModel(SC_Supplier supplier)
        {
            SC_SupplierHistory model = new SC_SupplierHistory();

            //model.account_bank = supplier.account_bank;
            model.Supplierid = supplier.id ;
            model.LogName = supplier.LogName;
            model.Password = supplier.Password;
            model.supplier_name = supplier.supplier_name;
            model.supplier_sn = supplier.supplier_sn;
            model.supplier_area = supplier.supplier_area;
            model.supplier_province = supplier.supplier_province;
            model.supplier_city = supplier.supplier_city;
            model.supplier_industry = supplier.supplier_industry;
            model.supplier_scale = supplier.supplier_scale;
            model.supplier_principal = supplier.supplier_principal;
            model.supplier_builttime = supplier.supplier_builttime;
            model.supplier_website = supplier.supplier_website;
            model.supplier_source = supplier.supplier_source;
            model.supplier_Intro = supplier.supplier_Intro;
            model.contact_fax = supplier.contact_fax;
            model.contact_Tel = supplier.contact_Tel;
            model.contact_Mobile = supplier.contact_Mobile;
            model.contact_Email = supplier.contact_Email;
            model.contact_address = supplier.contact_address;
            model.contact_ZIP = supplier.contact_ZIP;
            model.service_content = supplier.service_content;
            model.service_area = supplier.service_area;
            model.service_workamount = supplier.service_workamount;
            model.service_customization = supplier.service_customization;
            model.service_ohter = supplier.service_ohter;
            model.business_price = supplier.business_price;
            model.business_paytime = supplier.business_paytime;
            model.business_prepay = supplier.business_prepay;
            model.evaluation_department = supplier.evaluation_department;
            model.evaluation_level = supplier.evaluation_level;
            model.evaluation_feedback = supplier.evaluation_feedback;
            model.evaluation_note = supplier.evaluation_note;
            model.account_name = supplier.account_name;
            model.account_bank = supplier.account_bank;
            model.account_number = supplier.account_number;
            model.Credit = supplier.Credit;
            model.Cachet = supplier.Cachet;
            model.Money = supplier.Money;
            model.IsPerson = supplier.IsPerson;
            model.CreatTime = supplier.CreatTime;
            model.LastUpdateTime = DateTime.Now;
            model.Type = supplier.Type;
            model.Status = supplier.Status;

            return model;
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(SC_SupplierHistory model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public void Update(SC_SupplierHistory model)
        {
            dal.Update(model);
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
        public SC_SupplierHistory GetModel(int id)
        {
            return dal.GetModel(id);
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            return dal.GetList(strWhere);
        }

        public List<SC_SupplierHistory> GetList(string strWhere, SqlParameter[] parameters)
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
