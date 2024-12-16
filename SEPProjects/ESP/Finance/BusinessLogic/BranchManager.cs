using System;
using System.Data;
using System.Collections.Generic;
using ESP.Finance.Entity;
using ESP.Finance.Utility;
using System.Linq;
namespace ESP.Finance.BusinessLogic
{
	/// <summary>
	/// 业务逻辑类BranchBLL 的摘要说明。
	/// </summary>
     
     
    public static class BranchManager
	{
        //private readonly ESP.Finance.DataAccess.BranchDAL dal = new ESP.Finance.DataAccess.BranchDAL();

        private static ESP.Finance.IDataAccess.IBranchDataProvider DataProvider{get{return ESP.Configuration.ProviderHelper<ESP.Finance.IDataAccess.IBranchDataProvider>.Instance;}}
        //private const string _dalProviderName = "BranchDALProvider";

        
		#region  成员方法


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public static int  Add(ESP.Finance.Entity.BranchInfo model)
		{
			return DataProvider.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public static UpdateResult Update(ESP.Finance.Entity.BranchInfo model)
		{
            int res = 0;
            try
            {
                res = DataProvider.Update(model);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return UpdateResult.Failed;
            }
            if (res > 0)
            {
                return UpdateResult.Succeed;
            }
            else if (res == 0)
            {
                return UpdateResult.UnExecute;
            }
            return UpdateResult.Failed;
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public static DeleteResult Delete(int BranchID)
		{

            int res = 0;
            try
            {
                res = DataProvider.Delete(BranchID);
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
		public static ESP.Finance.Entity.BranchInfo GetModel(int BranchID)
		{
			
			return DataProvider.GetModel(BranchID);
		}

        public static ESP.Finance.Entity.BranchInfo GetModel(int BranchID,System.Data.SqlClient.SqlTransaction trans)
        {

            return DataProvider.GetModel(BranchID,trans);
        }

        public static ESP.Finance.Entity.BranchInfo GetModelByCode(string Code)
        {
            return DataProvider.GetModelByCode(Code);
        }
        public static ESP.Finance.Entity.BranchInfo GetModelByCode(string Code,System.Data.SqlClient.SqlTransaction trans)
        {
            return DataProvider.GetModelByCode(Code,trans);
        }
        #region 获得数据列表
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public static IList<BranchInfo> GetAllList()
        {
            return GetList(null,null);
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public static IList<BranchInfo> GetList(string term)
        {
            return GetList(term, null);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public static IList<BranchInfo> GetList(string term, List<System.Data.SqlClient.SqlParameter> param)
        {
            return DataProvider.GetList(term, param);
        }


        public static string GetPaymentAccounters()
        {
            string accounters = string.Empty;
            var branchs = GetAllList().GroupBy(n => n.PaymentAccounter);
            foreach (var g in branchs)
            {
                accounters += g.Key + ",";
            }
            return accounters.TrimEnd(' ',',');
        }

        public static string GetProjectAccounters()
        {
            string accounters = string.Empty;
            var branchs = GetAllList().GroupBy(n => n.ProjectAccounter);
            foreach (var g in branchs)
            {
                accounters += g.Key + ",";
            }
            return accounters.TrimEnd(' ', ',');
        }

        public static string GetContractAccounters()
        {
            string accounters = string.Empty;
            var branchs = GetAllList().GroupBy(n => n.ContractAccounter);
            foreach (var g in branchs)
            {
                accounters += g.Key + ",";
            }
            return accounters.TrimEnd(' ', ',');
        }

        public static string GetFinalAccounters()
        {
            string accounters = string.Empty;
            var branchs = GetAllList().GroupBy(n => n.FinalAccounter);
            foreach (var g in branchs)
            {
                accounters += g.Key + ",";
            }
            return accounters.TrimEnd(' ', ',');
        }

        public static string GetOtherAccounters()
        {
            string accounters = string.Empty;
            var branchs = GetAllList().GroupBy(n => n.OtherFinancialUsers);
            foreach (var g in branchs)
            {
                accounters += g.Key + ",";
            }
            return accounters.TrimEnd(' ', ',');
        }

        public static string GetLevel1Users(int departmentId)
        {
            return DataProvider.GetLevel1Users(departmentId);
        }

        public static string GetLevel1Users()
        {
            return DataProvider.GetLevel1Users();
        }

        public static string GetDimissionAuditors(int departmentId)
        {
            return DataProvider.GetDimissionAuditors(departmentId);
        }

        public static string GetLevel2Users(int departmentId)
        {
            return DataProvider.GetLevel2Users(departmentId);
        }

        public static string GetLevel2Users()
        {
            return DataProvider.GetLevel2Users();
        }

        public static string GetSalaryUsers(int departmentId)
        {
            return DataProvider.GetSalaryUsers(departmentId);
        }

        public static string GetCardUsers(int departmentId)
        {
            return DataProvider.GetCardUsers(departmentId);
        }

        public static IList<BranchInfo> GetLevel1BranchByUser(int userId)
        {
            return GetList(" FirstFinanceID="+userId.ToString());
        }

        public static IList<BranchInfo> GetBranchByDimission(int userId)
        {
            return GetList(" DimissionAuditor=" + userId.ToString());
        }

        public static IList<BranchInfo> GetLevel2BranchByUser(int userId)
        {
            return GetList(" PaymentAccounter=" + userId.ToString());
        }

        #endregion 获得数据列表

		#endregion  成员方法
	}
}

