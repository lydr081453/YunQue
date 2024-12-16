using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESP.Supplier.DataAccess;
using ESP.Supplier.Entity;
using System.Data;

namespace ESP.Supplier.BusinessLogic
{
    /// <summary>
    /// 业务逻辑类SC_SupplierEvaluation 的摘要说明。
    /// </summary>
    public class SC_SupplierEvaluationManager
    {
        private readonly SC_SupplierEvaluationDataProvider dal = new SC_SupplierEvaluationDataProvider();
        public SC_SupplierEvaluationManager()
        { }
        #region  成员方法
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int id)
        {
            return dal.Exists(id);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(SC_SupplierEvaluation model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public void Update(SC_SupplierEvaluation model)
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
        public SC_SupplierEvaluation GetModel(int id)
        {

            return dal.GetModel(id);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public SC_SupplierEvaluation GetModelBySupplierId(int supplierid)
        {
            return dal.GetModelBySupplierId(supplierid);
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
        public List<SC_SupplierEvaluation> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<SC_SupplierEvaluation> DataTableToList(DataTable dt)
        {
            List<SC_SupplierEvaluation> modelList = new List<SC_SupplierEvaluation>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                SC_SupplierEvaluation model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new SC_SupplierEvaluation();
                    if (dt.Rows[n]["id"].ToString() != "")
                    {
                        model.id = int.Parse(dt.Rows[n]["id"].ToString());
                    }
                    if (dt.Rows[n]["supplierid"].ToString() != "")
                    {
                        model.supplierid = int.Parse(dt.Rows[n]["supplierid"].ToString());
                    }
                    if (dt.Rows[n]["questionid"].ToString() != "")
                    {
                        model.questionid = int.Parse(dt.Rows[n]["questionid"].ToString());
                    }
                    model.questionnum = dt.Rows[n]["questionnum"].ToString();
                    model.Evaluation = dt.Rows[n]["Evaluation"].ToString();
                    if (dt.Rows[n]["createtime"].ToString() != "")
                    {
                        model.createtime = DateTime.Parse(dt.Rows[n]["createtime"].ToString());
                    }
                    model.creator = dt.Rows[n]["creator"].ToString();
                    model.createip = dt.Rows[n]["createip"].ToString();
                    if (dt.Rows[n]["lastupdatetime"].ToString() != "")
                    {
                        model.lastupdatetime = DateTime.Parse(dt.Rows[n]["lastupdatetime"].ToString());
                    }
                    model.lastupdateman = dt.Rows[n]["lastupdateman"].ToString();
                    model.lastupdateip = dt.Rows[n]["lastupdateip"].ToString();
                    if (dt.Rows[n]["type"].ToString() != "")
                    {
                        model.type = int.Parse(dt.Rows[n]["type"].ToString());
                    }
                    if (dt.Rows[n]["status"].ToString() != "")
                    {
                        model.status = int.Parse(dt.Rows[n]["status"].ToString());
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
