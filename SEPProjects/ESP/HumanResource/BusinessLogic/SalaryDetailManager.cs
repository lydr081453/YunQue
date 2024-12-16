using System;
using System.Data;
using System.Collections.Generic;
using ESP.HumanResource.Entity;
using ESP.HumanResource.Common;
using System.Data.SqlClient;
using ESP.HumanResource.DataAccess;

namespace ESP.HumanResource.BusinessLogic
{   
    public class SalaryDetailManager
    {
        private static SalaryDetailDataProvider dal = new SalaryDetailDataProvider();
        public SalaryDetailManager()
        { }
        #region  成员方法
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public static int Add(SalaryDetailInfo model)
        {
            return dal.Add(model);
        }

        public static int Add(SalaryDetailInfo model, SqlTransaction trans)
        {
            return dal.Add(model, trans);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public static void Update(SalaryDetailInfo model)
        {
            dal.Update(model);
        }

        public static void Update(SalaryDetailInfo model, SqlTransaction trans)
        {
            dal.Update(model, trans);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public static void Delete(int id, SqlTransaction trans)
        {

            dal.Delete(id, trans);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public static SalaryDetailInfo GetModel(int id)
        {

            return dal.GetModel(id);
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public static DataSet GetList(string strWhere)
        {
            return dal.GetList(strWhere);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public static List<SalaryDetailInfo> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            List<SalaryDetailInfo> modelList = new List<SalaryDetailInfo>();
            int rowsCount = ds.Tables[0].Rows.Count;
            if (rowsCount > 0)
            {
                SalaryDetailInfo model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new SalaryDetailInfo();
                    if (ds.Tables[0].Rows[n]["id"].ToString() != "")
                    {
                        model.id = int.Parse(ds.Tables[0].Rows[n]["id"].ToString());
                    }
                    if (ds.Tables[0].Rows[n]["creater"].ToString() != "")
                    {
                        model.creater = int.Parse(ds.Tables[0].Rows[n]["creater"].ToString());
                    }
                    model.createrName = ds.Tables[0].Rows[n]["createrName"].ToString();
                    if (ds.Tables[0].Rows[n]["createDate"].ToString() != "")
                    {
                        model.createDate = DateTime.Parse(ds.Tables[0].Rows[n]["createDate"].ToString());
                    }
                    if (ds.Tables[0].Rows[n]["sysid"].ToString() != "")
                    {
                        model.sysid = int.Parse(ds.Tables[0].Rows[n]["sysid"].ToString());
                    }
                    model.sysUserName = ds.Tables[0].Rows[n]["sysUserName"].ToString();
                    if (ds.Tables[0].Rows[n]["nowBasePay"].ToString() != "")
                    {
                        model.nowBasePay = decimal.Parse(ds.Tables[0].Rows[n]["nowBasePay"].ToString());
                    }
                    if (ds.Tables[0].Rows[n]["nowMeritPay"].ToString() != "")
                    {
                        model.nowMeritPay = decimal.Parse(ds.Tables[0].Rows[n]["nowMeritPay"].ToString());
                    }
                    if (ds.Tables[0].Rows[n]["newBasePay"].ToString() != "")
                    {
                        model.newBasePay = decimal.Parse(ds.Tables[0].Rows[n]["newBasePay"].ToString());
                    }
                    if (ds.Tables[0].Rows[n]["newMeritPay"].ToString() != "")
                    {
                        model.newMeritPay = decimal.Parse(ds.Tables[0].Rows[n]["newMeritPay"].ToString());
                    }
                    if (ds.Tables[0].Rows[n]["status"].ToString() != "")
                    {
                        model.status = int.Parse(ds.Tables[0].Rows[n]["status"].ToString());
                    }
                    modelList.Add(model);
                }
            }
            return modelList;
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public static DataSet GetAllList()
        {
            return GetList("");
        }

        /// <summary>
        /// 获得对象列表
        /// </summary>
        /// <param name="strWhere"></param>
        /// <param name="parms"></param>
        /// <returns></returns>
        public static List<SalaryDetailInfo> GetModelList(string strWhere, List<SqlParameter> parms)
        {
            return GetModelList(strWhere, parms);
        }
        /// <summary>
        /// 获得最新的薪资对象
        /// </summary>
        /// <returns></returns>
        public static SalaryDetailInfo GetTopModel(int sysId)
        {
            return dal.GetTopModel(sysId);
        }

        #endregion  成员方法
    }
}
