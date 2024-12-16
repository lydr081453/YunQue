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
    public class SC_ProductBatchManager
    {
        private readonly SC_ProductBatchDataProvider dal = new SC_ProductBatchDataProvider();
        public SC_ProductBatchManager()
        { }
        #region  成员方法


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(SC_ProductBatch model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public void Update(SC_ProductBatch model)
        {
            dal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public void Delete(int batchId)
        {

            dal.Delete(batchId);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public SC_ProductBatch GetModel(int batchId)
        {

            return dal.GetModel(batchId);
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
        public List<SC_ProductBatch> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<SC_ProductBatch> DataTableToList(DataTable dt)
        {
            List<SC_ProductBatch> modelList = new List<SC_ProductBatch>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                SC_ProductBatch model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new SC_ProductBatch();
                    if (dt.Rows[n]["batchId"].ToString() != "")
                    {
                        model.batchId = int.Parse(dt.Rows[n]["batchId"].ToString());
                    }
                    model.batchName = dt.Rows[n]["batchName"].ToString();
                    model.batchDes = dt.Rows[n]["batchDes"].ToString();
                    if (dt.Rows[n]["supplierId"].ToString() != "")
                    {
                        model.supplierId = int.Parse(dt.Rows[n]["supplierId"].ToString());
                    }
                    if (dt.Rows[n]["typeId"].ToString() != "")
                    {
                        model.typeId = int.Parse(dt.Rows[n]["typeId"].ToString());
                    }
                    if (dt.Rows[n]["beginTime"].ToString() != "")
                    {
                        model.beginTime = DateTime.Parse(dt.Rows[n]["beginTime"].ToString());
                    }
                    if (dt.Rows[n]["endTime"].ToString() != "")
                    {
                        model.endTime = DateTime.Parse(dt.Rows[n]["endTime"].ToString());
                    }
                    if (dt.Rows[n]["createDate"].ToString() != "")
                    {
                        model.createDate = DateTime.Parse(dt.Rows[n]["createDate"].ToString());
                    }
                    model.createIp = dt.Rows[n]["createIp"].ToString();
                    if (dt.Rows[n]["lastUpdateDate"].ToString() != "")
                    {
                        model.lastUpdateDate = DateTime.Parse(dt.Rows[n]["lastUpdateDate"].ToString());
                    }
                    model.lastUpdateIp = dt.Rows[n]["lastUpdateIp"].ToString();
                    if (dt.Rows[n]["status"].ToString() != "")
                    {
                        model.status = int.Parse(dt.Rows[n]["status"].ToString());
                    }
                    model.remark = dt.Rows[n]["remark"].ToString();
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
        /// 根据供应商ID和物料类型ID获得报价单列表
        /// </summary>
        /// <param name="sid"></param>
        /// <param name="tid"></param>
        /// <returns></returns>
        public List<SC_ProductBatch> GetListBySidTid(int sid, int tid)
        {
            string strwhere = " supplierId=" + sid + " and typeId=" + tid;
            return GetModelList(strwhere);
            
        }

        #endregion  成员方法
    }
}
