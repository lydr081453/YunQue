using System;
using System.Data;
using System.Collections.Generic;
using ESP.Finance.Entity;
using ESP.Finance.Utility;
using System.Data.SqlClient;

namespace ESP.Finance.BusinessLogic
{
	/// <summary>
	/// 业务逻辑类CustomerBLL 的摘要说明。
	/// </summary>

     
     
    public static class CustomerManager
	{
		//private readonly ESP.Finance.DataAccess.CustomerDAL dal=new ESP.Finance.DataAccess.CustomerDAL();

        private static ESP.Finance.IDataAccess.ICustomerDataProvider DataProvider{get{return ESP.Configuration.ProviderHelper<ESP.Finance.IDataAccess.ICustomerDataProvider>.Instance;}}
        //private const string _dalProviderName = "CustomerDALProvider";

        private const string tablename = "P_Customer";

        
		#region  成员方法



		/// <summary>
		/// 增加一条数据
        /// 在插入之前首先判断 客户代码(CustomerCode) 或 客户英文缩写(ShortEN) 是否有重复,重复则不插入
        /// 增加客户名称查重
		/// </summary>
         
         
        public static int Add(ESP.Finance.Entity.CustomerInfo model)
        {
            string term = " CustomerCode=@CustomerCode or ShortEN=@ShortEN or NameCN1=@NameCN1";
            List<SqlParameter> existsParam = new List<SqlParameter>();
            SqlParameter param = new SqlParameter("@CustomerCode", SqlDbType.NVarChar, 50);
            param.Value = model.CustomerCode;
            existsParam.Add(param);
            param = new SqlParameter("@ShortEN", SqlDbType.NVarChar, 50);
            param.Value = model.ShortEN;
            existsParam.Add(param);
            existsParam.Add(new SqlParameter("@NameCN1", model.NameCN1));

            int result = 0;
            //trans//if (DataProvider.Exists(term, existsParam, true))
            if (DataProvider.Exists(term, existsParam))
            {
                return 0;
            }
            //trans//model.CustomerCode = DataProvider.CreateCustomerCode(model.ShortEN,true);//生成客户代码
            model.CustomerCode = DataProvider.CreateCustomerCode(model.ShortEN);//生成客户代码
            model.Createdate = DateTime.Now;
            //trans//result = DataProvider.Add(model, true);
            result = DataProvider.Add(model);
            //客户税率
            //if (result > 0)
            //{ 
            //    model.CustomerID = result;
            //    Model.TaxRateInfo taxrate = new TaxRateInfo();
            //    taxrate.CustomerID = model.CustomerID;
            //    taxrate.CustomerName = model.FullNameCN;
            //    taxrate.CustomerShortName = model.ShortCN;
            //    taxrate.BizTypeID = 0;
            //    taxrate.BranchID = 0;
            //    taxrate.TaxRate = model.DefaultTaxRate == null ? 0 : model.DefaultTaxRate;
            //    TaxRateManager.Add(taxrate);
            //}
            //todo:add log

            //LogManager.Add(ESP.Finance.Utility.OperateType.Insert.ToString(), tablename, model);
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="term"></param>
        /// <param name="existsParam"></param>
        /// <returns></returns>
        public static bool Exists(string term, List<SqlParameter> existsParam)
        {
            return DataProvider.Exists(term, existsParam);
        }

		/// <summary>
		/// 更新一条数据 
        /// 在更新之前首先判断
        /// 最后更新时间 时间戳 是否大于 model 中的时间戳 , 大于则不更新
        /// 除本条记录 客户代码(CustomerCode) 或 客户英文缩写(ShortEN) 是否有重复,重复则更新失败
		/// </summary>
         
         
        public static UpdateResult Update(ESP.Finance.Entity.CustomerInfo model)
        {
            string term = "  (CustomerCode=@CustomerCode or ShortEN=@ShortEN) and CustomerID!=@CustomerID ";// 
            List<SqlParameter> existsParam = new List<SqlParameter>();
            SqlParameter param = new SqlParameter("@CustomerCode", SqlDbType.NVarChar, 50);
            param.Value = model.CustomerCode;
            existsParam.Add(param);

            param = new SqlParameter("@ShortEN", SqlDbType.NVarChar, 50);
            param.Value = model.ShortEN;
            existsParam.Add(param);

            param = new SqlParameter("@CustomerID", SqlDbType.Int, 4);
            param.Value = model.CustomerID;
            existsParam.Add(param);

            int result = 0;
            UpdateResult uRes = UpdateResult.Failed;



            //验重
            //trans//if (DataProvider.Exists(term, existsParam, true))
            if (DataProvider.Exists(term, existsParam))
            {
                return UpdateResult.Iterative;
            }
            //trans//result = DataProvider.Update(model, true);
            result = DataProvider.Update(model);
            if (result > 0)
            {
                uRes = UpdateResult.Succeed;


                //客户税率
                //if (model.DefaultTaxRate != null)
                //{
                //    Model.TaxRateInfo taxrate = new TaxRateInfo();
                //    taxrate.CustomerID = model.CustomerID;
                //    taxrate.CustomerName = model.FullNameCN;
                //    taxrate.CustomerShortName = model.ShortCN;
                //    taxrate.BizTypeID = 0;
                //    taxrate.BranchID = 0;
                //    taxrate.TaxRate = model.DefaultTaxRate == null ? 0 : model.DefaultTaxRate;
                //    TaxRateManager.Add(taxrate);
                //}
            }
            else if (result == 0)
            {
                uRes = UpdateResult.UnExecute;
            }


            return uRes;
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
		public static  ESP.Finance.Entity.CustomerInfo GetModel(int CustomerID)
		{
            Entity.CustomerInfo cus = DataProvider.GetModel(CustomerID);
            //Model.TaxRateInfo taxrate = TaxRateManager.GetModelByCustomer(CustomerID,0,0);
            //if (taxrate != null)
            //{
            //    cus.DefaultTaxRate = taxrate.TaxRate;
            //}
            return cus;
		}


        #region 获得数据列表
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public static IList<CustomerInfo> GetAllList()
        {
            return GetList(null);
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public static IList<CustomerInfo> GetList(string term)
        {
            return GetList(term, null);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public static IList<CustomerInfo> GetList(string term, List<System.Data.SqlClient.SqlParameter> param)
        {
            return DataProvider.GetList(term, param);
        }


        #endregion 获得数据列表

		#endregion  成员方法
	}
}

