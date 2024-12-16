using System;
using System.Data;
using System.Collections.Generic;
using ESP.Finance.Entity;
using ESP.Finance.Utility;
using System.Data.SqlClient;
namespace ESP.Finance.BusinessLogic
{
	/// <summary>
	/// 业务逻辑类ProjectMemberBLL 的摘要说明。
	/// </summary>
     
     
    public static class ProjectMemberManager
	{
		//private readonly ESP.Finance.DataAccess.ProjectMemberDAL dal=new ESP.Finance.DataAccess.ProjectMemberDAL();

        private static ESP.Finance.IDataAccess.IProjectMemberDataProvider DataProvider{get{return ESP.Configuration.ProviderHelper<ESP.Finance.IDataAccess.IProjectMemberDataProvider>.Instance;}}
        //private const string _dalProviderName = "ProjectMemberDALProvider";

        
		#region  成员方法


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public static int  Add(ESP.Finance.Entity.ProjectMemberInfo model)
		{
            using (SqlConnection conn = new SqlConnection(DataAccess.DbHelperSQL.connectionString))
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();
                try
                {
                    Entity.ProjectMemberInfo prjmem = DataProvider.GetModelByPrjMember(model.ProjectId, (int)model.MemberUserID);
                    if (prjmem != null)
                    {
                        DataProvider.Update(model, trans);
                        return model.MemberId;
                    }
                    int returnValue = DataProvider.Add(model,trans);
                    trans.Commit();
                    return returnValue;
                }
                catch
                {
                    trans.Rollback();
                    return 0;
                }
            }
		}

         public static int Add(ESP.Finance.Entity.ProjectMemberInfo model,SqlTransaction trans)
         {
             Entity.ProjectMemberInfo prjmem = DataProvider.GetModelByPrjMember(model.ProjectId, (int)model.MemberUserID);
             if (prjmem != null)
             {
                 DataProvider.Update(model, trans);
                 return model.MemberId;
             }
             return DataProvider.Add(model,trans);
         }

		/// <summary>
		/// 更新一条数据
		/// </summary>
         
         
		public static UpdateResult Update(ESP.Finance.Entity.ProjectMemberInfo model)
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
		public static DeleteResult Delete(int MemberId)
		{

            int res = 0;
            try
            {
                res = DataProvider.Delete(MemberId);
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
		public static ESP.Finance.Entity.ProjectMemberInfo GetModel(int MemberId)
		{

            return DataProvider.GetModel(MemberId);
		}

        public static ESP.Finance.Entity.ProjectMemberInfo GetModelByPrjMember(int projectId, int memberUserId)
        {
            return DataProvider.GetModelByPrjMember(projectId,memberUserId);
        }
        public static ESP.Finance.Entity.ProjectMemberInfo GetModelByPrjMember(int projectId, int memberUserId,SqlTransaction trans)
        {
            return DataProvider.GetModelByPrjMember(projectId, memberUserId, trans);
        }
        #region 获得数据列表
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public static IList<ProjectMemberInfo> GetAllList()
        {
            return GetList(null);
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public static IList<ProjectMemberInfo> GetList(string term)
        {
            return DataProvider.GetList(term, null);
        }

        public static IList<ProjectMemberInfo> GetList(string term, List<System.Data.SqlClient.SqlParameter> param, SqlTransaction trans)
        {
            return DataProvider.GetList(term, param, trans);        
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public static IList<ProjectMemberInfo> GetList(string term, List<System.Data.SqlClient.SqlParameter> param)
        {
            return DataProvider.GetList(term, param);
        }


        public static IList<ESP.Finance.Entity.ProjectMemberInfo> GetListByProject(int projectID, string term, List<System.Data.SqlClient.SqlParameter> param)
        {
            return DataProvider.GetListByProject(projectID, term, param);
        }

        public static IList<ESP.Finance.Entity.ProjectMemberInfo> GetListByProject(int projectID, string term, List<System.Data.SqlClient.SqlParameter> param,SqlTransaction trans)
        {
            return DataProvider.GetListByProject(projectID, term, param,trans);
        }

        #endregion 获得数据列表

		#endregion  成员方法
	}
}

