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
    public class SC_SupplierProductPhotoManager
    {
        private readonly SC_SupplierProductPhotoDataProvider dal = new SC_SupplierProductPhotoDataProvider();
        public SC_SupplierProductPhotoManager()
        { }
        #region  成员方法
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int Id)
        {
            return dal.Exists(Id);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(SC_SupplierProductPhoto model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public void Update(SC_SupplierProductPhoto model)
        {
            dal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public void Delete(int Id)
        {

            dal.Delete(Id);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public SC_SupplierProductPhoto GetModel(int Id)
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
        /// <summary>
        /// 获得前几行数据
        /// </summary>
        public DataSet GetList(int Top, string strWhere, string filedOrder)
        {
            return dal.GetList(Top, strWhere, filedOrder);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<SC_SupplierProductPhoto> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<SC_SupplierProductPhoto> DataTableToList(DataTable dt)
        {
            List<SC_SupplierProductPhoto> modelList = new List<SC_SupplierProductPhoto>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                SC_SupplierProductPhoto model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new SC_SupplierProductPhoto();
                    if (dt.Rows[n]["Id"].ToString() != "")
                    {
                        model.Id = int.Parse(dt.Rows[n]["Id"].ToString());
                    }
                    if (dt.Rows[n]["SupplierId"].ToString() != "")
                    {
                        model.SupplierId = int.Parse(dt.Rows[n]["SupplierId"].ToString());
                    }
                    if (dt.Rows[n]["ProductId"].ToString() != "")
                    {
                        model.ProductId = int.Parse(dt.Rows[n]["ProductId"].ToString());
                    }
                    model.ShowTxt = dt.Rows[n]["ShowTxt"].ToString();
                    model.PhotoUrl = dt.Rows[n]["PhotoUrl"].ToString();
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

        public List<SC_SupplierProductPhoto> GetListByProductId(int pid)
        {           
                
                string strwhere = " productid="+pid;
                return GetModelList(strwhere);
        }

        #endregion  成员方法
    }
}
