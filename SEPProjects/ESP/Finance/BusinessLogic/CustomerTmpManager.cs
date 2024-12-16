using System;
using System.Data;
using System.Collections.Generic;
using ESP.Finance.Entity;
using ESP.Finance.Utility;
using System.Data.SqlClient;
using System.Reflection;

namespace ESP.Finance.BusinessLogic
{
	/// <summary>
    /// 业务逻辑类CustomerTmpBLL 的摘要说明。
	/// </summary>

     
     
    public static class CustomerTmpManager
	{
		//private readonly ESP.Finance.DataAccess.CustomerDAL dal=new ESP.Finance.DataAccess.CustomerDAL();

        private static ESP.Finance.IDataAccess.ICustomerTmpDataProvider DataProvider{get{return ESP.Configuration.ProviderHelper<ESP.Finance.IDataAccess.ICustomerTmpDataProvider>.Instance;}}
        //private const string _dalProviderName = "CustomerTmpDALProvider";

        private const string tablename = "P_CustomerTmp";

        
		#region  成员方法



		/// <summary>
		/// 增加一条数据
        /// 在插入之前首先判断 客户代码(CustomerCode) 或 客户英文缩写(ShortEN) 是否有重复,重复则不插入
		/// </summary>
         
         
        public static int Add(ESP.Finance.Entity.CustomerTmpInfo model)
        {
            //string term = " CustomerCode=@CustomerCode or ShortEN=@ShortEN ";
            //List<SqlParameter> existsParam = new List<SqlParameter>();
            //SqlParameter param = new SqlParameter("@CustomerCode", SqlDbType.NVarChar, 50);
            //param.Value = model.CustomerCode;
            //existsParam.Add(param);
            //param = new SqlParameter("@ShortEN", SqlDbType.NVarChar, 50);
            //param.Value = model.ShortEN;
            //existsParam.Add(param);

            int result = 0;
            //if (DataProvider.Exists(term, existsParam, true))
            //{
            //    return 0;
            //}
            if (string.IsNullOrEmpty(model.CustomerCode))
            {
                //trans//model.CustomerCode = DataProvider.CreateCustomerCode(model.ShortEN, true);//生成客户代码
                model.CustomerCode = DataProvider.CreateCustomerCode(model.ShortEN);//生成客户代码
            }
            model.Createdate = DateTime.Now;
            //trans//result = DataProvider.Add(model, true);
            result = DataProvider.Add(model);

            //if (result > 0 && model.ProjectID > 0)
            //{
            //    ProjectInfo prj = ProjectManager.GetModel(model.ProjectID);
            //    if (prj != null)
            //    {
            //        prj.CustomerID = result;

            //        ProjectManager.Update(prj);
            //    }
            //}

            //客户税率
            //if (result > 0 && model.CustomerID > 0)
            //{
            //    model.CustomerTmpID = result;

            //    Model.TaxRateInfo taxrate = new TaxRateInfo();
            //    taxrate.CustomerID = model.CustomerID;
            //    taxrate.CustomerName = model.FullNameCN;
            //    taxrate.CustomerShortName = model.ShortCN;
            //    taxrate.BizTypeID = 0;
            //    taxrate.BranchID = 0;
            //    CustomerInfo cust = CustomerManager.GetModel(model.CustomerID);
            //    taxrate.TaxRate = cust.DefaultTaxRate == null ? 0 : cust.DefaultTaxRate;
            //    TaxRateManager.Add(taxrate);
            //}
            //todo:add log

            //LogManager.Add(ESP.Finance.Utility.OperateType.Insert.ToString(), tablename, model);
            return result;
        }

		/// <summary>
		/// 更新一条数据 
        /// 在更新之前首先判断
        /// 最后更新时间 时间戳 是否大于 model 中的时间戳 , 大于则不更新
        /// 除本条记录 客户代码(CustomerCode) 或 客户英文缩写(ShortEN) 是否有重复,重复则更新失败
		/// </summary>
         
         
        public static UpdateResult Update(ESP.Finance.Entity.CustomerTmpInfo model)
        {
            //string term = "  (CustomerCode=@CustomerCode or ShortEN=@ShortEN) and CustomerTmpID!=@CustomerTmpID ";// 
            //List<SqlParameter> existsParam = new List<SqlParameter>();
            //SqlParameter param = new SqlParameter("@CustomerCode", SqlDbType.NVarChar, 50);
            //param.Value = model.CustomerCode;
            //existsParam.Add(param);

            //param = new SqlParameter("@ShortEN", SqlDbType.NVarChar, 50);
            //param.Value = model.ShortEN;
            //existsParam.Add(param);

            //param = new SqlParameter("@CustomerTmpID", SqlDbType.Int, 4);
            //param.Value = model.CustomerID;
            //existsParam.Add(param);

            int result = 0;
            UpdateResult uRes = UpdateResult.Failed;



            ////验重
            //if (DataProvider.Exists(term, existsParam, true))
            //{
            //    return UpdateResult.Iterative;
            //}
            //trans//result = DataProvider.Update(model,true);
            result = DataProvider.Update(model);
            if (result > 0)
            {

                uRes = UpdateResult.Succeed;

                //if (model.ProjectID > 0)
                //{
                //    ProjectInfo prj = ProjectManager.GetModel(model.ProjectID);
                //    if (prj != null)
                //    {
                //        prj.CustomerID = result;
                //        uRes = ProjectManager.Update(prj);
                //    }
                //}


                //客户税率
                //if (model.DefaultTaxRate != null && model.CustomerID > 0)
                //{
                //    Model.TaxRateInfo taxrate = new TaxRateInfo();
                //    taxrate.CustomerID = model.CustomerID;
                //    taxrate.CustomerName = model.FullNameCN;
                //    taxrate.CustomerShortName = model.ShortCN;
                //    taxrate.BizTypeID = 0;
                //    taxrate.BranchID = 0;
                //    CustomerInfo cust = CustomerManager.GetModel(model.CustomerID);
                //    taxrate.TaxRate = cust.DefaultTaxRate == null ? 0 : cust.DefaultTaxRate;
                //    TaxRateManager.Add(taxrate);
                //}
            }
            else if (result == 0)
            {
                uRes = UpdateResult.UnExecute;
            }


            return uRes;
        }


        private static ESP.Finance.Entity.CustomerInfo GetCustomer(ESP.Finance.Entity.CustomerTmpInfo tmp)
        {
            Entity.CustomerInfo cust = new CustomerInfo();
            PropertyInfo[] Tmpproperties = tmp.GetType().GetProperties();
            //PropertyInfo[] Custproperties = cust.GetType().GetProperties();
            foreach (PropertyInfo property in Tmpproperties)
            {
                try
                {
                    object value = property.GetValue(tmp, null);

                    PropertyInfo pin = cust.GetType().GetProperty(property.Name);
                    pin.SetValue(cust, value, null);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            return cust;
        }


		/// <summary>
		/// 删除一条数据
		/// </summary>
		public static  DeleteResult Delete(int CustomerID)
		{
            int res = 0;
            try
            {
                res = DataProvider.Delete(CustomerID);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return DeleteResult.Failed;
            }
            if (res > 0)
            {
                return DeleteResult.Succeed;
            }
            else if (res == 0)
            {
                return DeleteResult.UnExecute;
            }
            return DeleteResult.Failed;
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public static  ESP.Finance.Entity.CustomerTmpInfo GetModel(int CustomerTmpID)
		{
            Entity.CustomerTmpInfo model = DataProvider.GetModel(CustomerTmpID);
            //if (model != null)
            //{
            //    if (model.CustomerID > 0)
            //    {
            //        Model.TaxRateInfo taxrate = TaxRateManager.GetModel(model.CustomerID, 0, 0);
            //        if (taxrate != null)
            //        {
            //            model.DefaultTaxRate = taxrate.TaxRate;
            //        }
            //    }
            //    else
            //    {
            //        model.DefaultTaxRate = 0;
            //    }
            //}
            return model;
		}


        #region 获得数据列表
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public static IList<CustomerTmpInfo> GetAllList()
        {
            return GetList(null);
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public static IList<CustomerTmpInfo> GetList(string term)
        {
            return GetList(term, null);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public static IList<CustomerTmpInfo> GetList(string term, List<System.Data.SqlClient.SqlParameter> param)
        {
            return DataProvider.GetList(term, param);
        }


        #endregion 获得数据列表

		#endregion  成员方法
	}
}

