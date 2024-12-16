using System;
using System.Data;
using System.Collections.Generic;
using ESP.Finance.Entity;
using ESP.Finance.Utility;
using System.Data.SqlClient;
namespace ESP.Finance.BusinessLogic
{
	/// <summary>
	/// 业务逻辑类SupportMemberBLL 的摘要说明。
	/// </summary>
     
     
    public static class SupportMemberManager
	{
        //private readonly ESP.Finance.DataAccess.SupportMemberDAL dal = new ESP.Finance.DataAccess.SupportMemberDAL();

        private static ESP.Finance.IDataAccess.ISupportMemberDataProvider DataProvider{get{return ESP.Configuration.ProviderHelper<ESP.Finance.IDataAccess.ISupportMemberDataProvider>.Instance;}}
        //private const string _dalProviderName = "SupportMemberDALProvider";

        
		#region  成员方法


		/// <summary>
		/// 增加一条数据
		/// </summary>
         
         
		public static int  Add(ESP.Finance.Entity.SupportMemberInfo model)
		{

            SupportMemberInfo member = DataProvider.GetModelBySupporterMember(model.SupportID, model.MemberUserID??0);
            if (member != null)
            {
                Update(model);
                return model.SupportMemberId;
            }
			return DataProvider.Add(model);
		}

        public static int Add(ESP.Finance.Entity.SupportMemberInfo model,System.Data.SqlClient.SqlTransaction trans)
        {

            SupportMemberInfo member = DataProvider.GetModelBySupporterMember(model.SupportID, model.MemberUserID ?? 0);
            if (member != null)
            {
                DataProvider.Update(model,trans);
                return model.SupportMemberId;
            }
            return DataProvider.Add(model,trans);
        }

		/// <summary>
		/// 更新一条数据
		/// </summary>
         
         
		public static UpdateResult Update(ESP.Finance.Entity.SupportMemberInfo model)
		{
            int res = 0;
            try
            {
                //trans//res = DataProvider.Update(model,true);
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
		public static DeleteResult Delete(int SupportMemberId)
		{

            int res = 0;
            try
            {
                res = DataProvider.Delete(SupportMemberId);
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
		public static ESP.Finance.Entity.SupportMemberInfo GetModel(int SupportMemberId)
		{
			
			return DataProvider.GetModel(SupportMemberId);
		}


        public static ESP.Finance.Entity.SupportMemberInfo GetModelBySupporterMember(int SupporterId, int memberUserId)
        {
            return DataProvider.GetModelBySupporterMember(SupporterId, memberUserId);
        }
        #region 获得数据列表
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public static IList<SupportMemberInfo> GetAllList()
        {
            return GetList(null);
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public static IList<SupportMemberInfo> GetList(string term)
        {
            return GetList(term, null);
        }

        public static IList<SupportMemberInfo> GetList(int supportId , SqlTransaction trans)
        {
            return DataProvider.GetList(supportId, trans);
        }

        public static IList<SupportMemberInfo> GetList(string term,List<System.Data.SqlClient.SqlParameter> param, SqlTransaction trans)
        {
            return DataProvider.GetList(term,null,trans);
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public static IList<SupportMemberInfo> GetList(string term, List<System.Data.SqlClient.SqlParameter> param)
        {
            return DataProvider.GetList(term, param);
        }
        #endregion 获得数据列表

		#endregion  成员方法
	}
}

