using System;
using System.Data;
using System.Collections.Generic;
using ESP.Finance.Entity;
using ESP.Finance.Utility;
using System.Reflection;
using System.Data.SqlClient;
namespace ESP.Finance.BusinessLogic
{
	/// <summary>
	/// 业务逻辑类ProjectHistBLL 的摘要说明。
	/// </summary>
     
     
    public static class ProjectHistManager
	{
		//private readonly ESP.Finance.DataAccess.ProjectHistDAL dal=new ESP.Finance.DataAccess.ProjectHistDAL();

        private static ESP.Finance.IDataAccess.IProjectHistDataProvider DataProvider{get{return ESP.Configuration.ProviderHelper<ESP.Finance.IDataAccess.IProjectHistDataProvider>.Instance;}}
        //private const string _dalProviderName = "ProjectHistDALProvider";

        
		#region  成员方法

        private static ESP.Finance.Entity.ProjectHistInfo GetHist(ESP.Finance.Entity.ProjectInfo prj,SqlTransaction trans)
        {
            Entity.ProjectHistInfo hist = new ProjectHistInfo();
            PropertyInfo[] Prjproperties = prj.GetType().GetProperties();//得到project的所有属性
            PropertyInfo[] Histproperties = hist.GetType().GetProperties();//得到project的所有属性
            foreach (PropertyInfo property in Prjproperties)//依次给projecthist 对象的属性赋值
            {
                try
                {
                    object value = property.GetValue(prj, null);

                    PropertyInfo pin = hist.GetType().GetProperty(property.Name);
                    pin.SetValue(hist, value, null);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            if (prj != null && prj.ProjectId > 0)
            {
                prj = ProjectManager.GetModel(prj.ProjectId,trans);//得到project的所有信息,并序列化
                hist.ProjectModel = prj.ObjectSerialize();
            }
            return hist;
        }
        /// <summary>
        /// 根据Project对象得到 ProjectHist对象
        /// </summary>
        /// <param name="prj"></param>
        /// <returns></returns>
        private static ESP.Finance.Entity.ProjectHistInfo GetHist(ESP.Finance.Entity.ProjectInfo prj)
        {
            Entity.ProjectHistInfo hist = new ProjectHistInfo();
            PropertyInfo[] Prjproperties = prj.GetType().GetProperties();//得到project的所有属性
            PropertyInfo[] Histproperties = hist.GetType().GetProperties();//得到project的所有属性
            foreach (PropertyInfo property in Prjproperties)//依次给projecthist 对象的属性赋值
            {
                try
                {
                    object value = property.GetValue(prj, null);

                    PropertyInfo pin = hist.GetType().GetProperty(property.Name);
                    pin.SetValue(hist, value, null);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            if (prj != null && prj.ProjectId > 0)
            {
                prj = ProjectManager.GetModel(prj.ProjectId);//得到project的所有信息,并序列化
                hist.ProjectModel = prj.ObjectSerialize();
            }
            return hist;
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
         
         
        public static int Add(ESP.Finance.Entity.ProjectHistInfo model)
        {
            //trans//model.VersionID = DataProvider.GetNewVersion(model.ProjectId, true);
            //trans//return DataProvider.Add(model,true);
            model.VersionID = DataProvider.GetNewVersion(model.ProjectId);
            return DataProvider.Add(model);
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
         
         
        public static int Add(ESP.Finance.Entity.ProjectInfo model,SqlTransaction trans)
        {
            ProjectHistInfo hist = GetHist(model,trans);
            hist.VersionID = DataProvider.GetNewVersion(model.ProjectId);
            return DataProvider.Add(hist,trans);
        }



		/// <summary>
		/// 更新一条数据
		/// </summary>
         
         
		public static UpdateResult Update(ESP.Finance.Entity.ProjectHistInfo model)
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
		public static DeleteResult Delete(int ProjectHistID)
		{

            int res = 0;
            try
            {
                res = DataProvider.Delete(ProjectHistID);
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
		public static ESP.Finance.Entity.ProjectHistInfo GetModel(int ProjectHistID)
		{
			
			return DataProvider.GetModel(ProjectHistID);
		}

        #region 获得数据列表
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public static IList<ProjectHistInfo> GetAllList()
        {
            return GetList(null);
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public static IList<ProjectHistInfo> GetList(string term)
        {
            return GetList(term, null);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public static IList<ProjectHistInfo> GetList(string term, List<System.Data.SqlClient.SqlParameter> param)
        {
            return DataProvider.GetList(term, param);
        }

        public static IList<ESP.Finance.Entity.ProjectHistInfo> GetListByProject(int projectId, string term, List<System.Data.SqlClient.SqlParameter> param)
        {
            return DataProvider.GetListByProject(projectId, term, param);
        }
        #endregion 获得数据列表

		#endregion  成员方法
	}
}

