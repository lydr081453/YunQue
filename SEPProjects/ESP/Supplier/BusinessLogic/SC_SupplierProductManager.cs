using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESP.Supplier.Common;
using ESP.Supplier.Entity;
using ESP.Supplier.DataAccess;
using System.Data;
using System.Data.SqlClient;

namespace ESP.Supplier.BusinessLogic
{
    public class SC_SupplierProductManager
    {
        private readonly SC_SupplierProductDataProvider dal = new SC_SupplierProductDataProvider();
        public SC_SupplierProductManager()
        { }
        #region  成员方法
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(SC_SupplierProduct model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public void Update(SC_SupplierProduct model)
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
        public SC_SupplierProduct GetModel(int id)
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
      
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<SC_SupplierProduct> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<SC_SupplierProduct> DataTableToList(DataTable dt)
        {
            List<SC_SupplierProduct> modelList = new List<SC_SupplierProduct>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                SC_SupplierProduct model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new SC_SupplierProduct();
                    if (dt.Rows[n]["id"].ToString() != "")
                    {
                        model.id = int.Parse(dt.Rows[n]["id"].ToString());
                    }
                    if (dt.Rows[n]["Version"].ToString() != "")
                    {
                        model.Version = int.Parse(dt.Rows[n]["Version"].ToString());
                    }
                    if (dt.Rows[n]["SupplierId"].ToString() != "")
                    {
                        model.SupplierId = int.Parse(dt.Rows[n]["SupplierId"].ToString());
                    }
                    model.SN = dt.Rows[n]["SN"].ToString();
                    model.Name = dt.Rows[n]["Name"].ToString();
                    if (dt.Rows[n]["UsedBeginTime"].ToString() != "")
                    {
                        model.UsedBeginTime = DateTime.Parse(dt.Rows[n]["UsedBeginTime"].ToString());
                    }
                    if (dt.Rows[n]["UsedEndTime"].ToString() != "")
                    {
                        model.UsedEndTime = DateTime.Parse(dt.Rows[n]["UsedEndTime"].ToString());
                    }
                    if (dt.Rows[n]["PayDays"].ToString() != "")
                    {
                        model.PayDays = int.Parse(dt.Rows[n]["PayDays"].ToString());
                    }
                    if (dt.Rows[n]["ReceiveType"].ToString() != "")
                    {
                        model.ReceiveType = int.Parse(dt.Rows[n]["ReceiveType"].ToString());
                    }
                    model.Receiver = dt.Rows[n]["Receiver"].ToString();
                    model.Description = dt.Rows[n]["Description"].ToString();
                    model.Unit = dt.Rows[n]["Unit"].ToString();
                    model.Class = dt.Rows[n]["Class"].ToString();
                    if (dt.Rows[n]["Price"].ToString() != "")
                    {
                        model.Price = decimal.Parse(dt.Rows[n]["Price"].ToString());
                    }
                    if (dt.Rows[n]["ProductTypeid"].ToString() != "")
                    {
                        model.ProductTypeid = int.Parse(dt.Rows[n]["ProductTypeid"].ToString());
                    }
                    model.ProductTypeName = dt.Rows[n]["ProductTypeName"].ToString();
                    model.ProductContentSheet = dt.Rows[n]["ProductContentSheet"].ToString();
                    model.CreatedIP = dt.Rows[n]["CreatedIP"].ToString();
                    if (dt.Rows[n]["CreatTime"].ToString() != "")
                    {
                        model.CreatTime = DateTime.Parse(dt.Rows[n]["CreatTime"].ToString());
                    }
                    model.LastModifiedIP = dt.Rows[n]["LastModifiedIP"].ToString();
                    if (dt.Rows[n]["LastUpdateTime"].ToString() != "")
                    {
                        model.LastUpdateTime = DateTime.Parse(dt.Rows[n]["LastUpdateTime"].ToString());
                    }
                    if (dt.Rows[n]["Type"].ToString() != "")
                    {
                        model.Type = int.Parse(dt.Rows[n]["Type"].ToString());
                    }
                    if (dt.Rows[n]["Status"].ToString() != "")
                    {
                        model.Status = int.Parse(dt.Rows[n]["Status"].ToString());
                    }
                    model.productPic = dt.Rows[n]["productPic"].ToString();
                    if (dt.Rows[n]["ProductBatchId"].ToString() != "")
                    {
                        model.ProductBatchId = int.Parse(dt.Rows[n]["ProductBatchId"].ToString());
                    }
                    modelList.Add(model);
                }
            }
            return modelList;
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetAllList()
        {
            return GetList("");
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
