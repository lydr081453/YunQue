using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using ESP.Supplier.DataAccess;
using ESP.Supplier.Entity;
using System.Data.SqlClient;
using ESP.Supplier.Common;

namespace ESP.Supplier.BusinessLogic
{
    public class SC_NoCheckSupplierScoreManager
    {
        private readonly SC_NoCheckSupplierScoreDataProvider dal = new SC_NoCheckSupplierScoreDataProvider();
        public SC_NoCheckSupplierScoreManager()
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
        public int Add(SC_NoCheckSupplierScore model, SqlTransaction trans, SqlConnection conn)
        {
            return dal.Add(model,trans,conn);
        }

        /// <summary>
        /// 增加一组数据
        /// </summary>
        public int AddList(List<SC_NoCheckSupplierScore> nclist,int supplierId)
        {
            if (nclist.Count <= 0)
                return 0;
            int ret = 0;
            using (SqlConnection conn = new SqlConnection(DbHelperSQL.connectionString))
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();
                try
                {                    
                    //删除未提交的审核人评分
                    new Supplier.DataAccess.SC_NoCheckSupplierScoreDataProvider().DeleteBySupplierId(supplierId, 2, trans, conn);
                    
                    foreach (SC_NoCheckSupplierScore nc in nclist)
                    {
                        ret = new SC_NoCheckSupplierScoreDataProvider().Add(nc, trans, conn);
                    }                  

                    trans.Commit();

                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    ret = 0;
                }
                return ret;
            }
        }

        /// <summary>
        /// 增加一组数据
        /// </summary>
        public int AddList(List<SC_NoCheckSupplierScore> nclist,SC_SupplierEvaluation se, SC_Supplier model)
        {
            if (nclist.Count <= 0)
                return 0;
            int ret = 0;
            using (SqlConnection conn = new SqlConnection(DbHelperSQL.connectionString))
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();
                try
                {
                    //删除未提交的审核人评分
                    new Supplier.DataAccess.SC_NoCheckSupplierScoreDataProvider().DeleteBySupplierId(model.id, 2, trans, conn);

                    foreach (SC_NoCheckSupplierScore nc in nclist)
                    {
                        ret = new SC_NoCheckSupplierScoreDataProvider().Add(nc, trans, conn);
                    }

                    //删除采购部意见备注
                    new Supplier.DataAccess.SC_SupplierEvaluationDataProvider().DeleteBySupplierId(model.id, trans, conn);
                    ret = new SC_SupplierEvaluationDataProvider().Add(se, trans, conn);

                    new SC_SupplierDataProvider().Update(model, trans, conn);

                    trans.Commit();

                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    ret = 0;
                }
                return ret;
            }
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public void Update(SC_NoCheckSupplierScore model, SqlTransaction trans, SqlConnection conn)
        {
            dal.Update(model,trans,conn);
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
        public SC_NoCheckSupplierScore GetModel(int id)
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
        /// 获得前几行数据
        /// </summary>
        public DataSet GetList(int Top, string strWhere, string filedOrder)
        {
            return dal.GetList(Top, strWhere, filedOrder);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<SC_NoCheckSupplierScore> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<SC_NoCheckSupplierScore> DataTableToList(DataTable dt)
        {
            List<SC_NoCheckSupplierScore> modelList = new List<SC_NoCheckSupplierScore>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                SC_NoCheckSupplierScore model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new SC_NoCheckSupplierScore();
                    if (dt.Rows[n]["id"].ToString() != "")
                    {
                        model.id = int.Parse(dt.Rows[n]["id"].ToString());
                    }
                    if (dt.Rows[n]["qaid"].ToString() != "")
                    {
                        model.qaid = int.Parse(dt.Rows[n]["qaid"].ToString());
                    }
                    model.questionnum = dt.Rows[n]["questionnum"].ToString();
                    if (dt.Rows[n]["supplierid"].ToString() != "")
                    {
                        model.supplierid = int.Parse(dt.Rows[n]["supplierid"].ToString());
                    }
                    if (dt.Rows[n]["score"].ToString() != "")
                    {
                        model.score = decimal.Parse(dt.Rows[n]["score"].ToString());
                    }
                    if (dt.Rows[n]["scoreType"].ToString() != "")
                    {
                        model.scoreType = int.Parse(dt.Rows[n]["scoreType"].ToString());
                    }
                    if (dt.Rows[n]["createTime"].ToString() != "")
                    {
                        model.createTime = DateTime.Parse(dt.Rows[n]["createTime"].ToString());
                    }
                    model.createIP = dt.Rows[n]["createIP"].ToString();
                    model.creator = dt.Rows[n]["creator"].ToString();
                    modelList.Add(model);
                }
            }
            return modelList;
        }

        public List<SC_NoCheckSupplierScore> GetListBySupplierId(int supplierid)
        {
            List<SC_NoCheckSupplierScore> modelList = new List<SC_NoCheckSupplierScore>();

            modelList = GetModelList(" supplierid = " +supplierid);
            return modelList;
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetAllList()
        {
            return GetList("");
        }
                

        #endregion  成员方法
    }
}
