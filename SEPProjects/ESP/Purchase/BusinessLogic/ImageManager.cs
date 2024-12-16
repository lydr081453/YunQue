using System.Collections.Generic;
using System.Data;
using ESP.Purchase.DataAccess;
using ESP.Purchase.Entity;

namespace ESP.Purchase.BusinessLogic
{
    /// <summary>
    /// 业务逻辑类ImageManager 的摘要说明。
    /// </summary>
    public static class ImageManager
    {       
        #region  成员方法
        /// <summary>
        /// 增加一条数据
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public static int Add(ImageInfo model)
        {
            return ImageDataProvider.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        /// <param name="model">The model.</param>
        public static void Update(ImageInfo model)
        {
            ImageDataProvider.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        /// <param name="id">The id.</param>
        public static void Delete(int id)
        {
            ImageDataProvider.Delete(id);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns></returns>
        public static ImageInfo GetModel(int id)
        {
            return ImageDataProvider.GetModel(id);
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        /// <param name="strWhere">The STR where.</param>
        /// <returns></returns>
        public static DataSet GetList(string strWhere)
        {
            return ImageDataProvider.GetList(strWhere);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        /// <param name="strWhere">The STR where.</param>
        /// <returns></returns>
        public static List<ImageInfo> GetModelList(string strWhere)
        {
            DataSet ds = ImageDataProvider.GetList(strWhere);
            List<ImageInfo> modelList = new List<ImageInfo>();
            int rowsCount = ds.Tables[0].Rows.Count;
            if (rowsCount > 0)
            {
                ImageInfo model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new ImageInfo();
                    if (ds.Tables[0].Rows[0]["id"].ToString() != "")
                    {
                        model.id = int.Parse(ds.Tables[0].Rows[0]["id"].ToString());
                    }
                    if (ds.Tables[0].Rows[0]["supplier_id"].ToString() != "")
                    {
                        model.supplier_id = int.Parse(ds.Tables[0].Rows[0]["supplier_id"].ToString());
                    }
                    model.imagename = ds.Tables[0].Rows[0]["imagename"].ToString();
                    model.imageurl = ds.Tables[0].Rows[0]["imageurl"].ToString();
                    modelList.Add(model);
                }
            }
            return modelList;
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        /// <returns></returns>
        public static DataSet GetAllList()
        {
            return GetList("");
        }

        /// <summary>
        /// Gets the list by supplier ID.
        /// </summary>
        /// <param name="supplierid">The supplierid.</param>
        /// <returns></returns>
        public static List<ImageInfo> GetListBySupplierID(int supplierid)
        {
            return ImageDataProvider.GetListBySupplierID(supplierid);
        }
        #endregion  成员方法
    }
}