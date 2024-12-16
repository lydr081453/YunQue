//using System.Data;
//using System.Collections.Generic;
//using ESP.Purchase.DataAccess;
//using ESP.Purchase.Entity;

//namespace ESP.Purchase.BusinessLogic
//{
//    /// <summary>
//    /// 业务逻辑类OperationAuditManageManager 的摘要说明。
//    /// </summary>
//    public class OperationAuditManageManager
//    {
//        private static ESP.Framework.DataAccess.OperationAuditManageDataHelper dal = new ESP.Framework.DataAccess.OperationAuditManageDataHelper();


//        /// <summary>
//        /// Initializes a new instance of the <see cref="OperationAuditManageManager"/> class.
//        /// </summary>
//        public OperationAuditManageManager()
//        { }

//        #region  成员方法
//        /// <summary>
//        /// 是否存在该记录
//        /// </summary>
//        /// <param name="Id">The id.</param>
//        /// <returns></returns>
//        public static bool Exists(int Id)
//        {
//            return dal.Exists(Id);
//        }

//        /*
//        /// <summary>
//        /// 增加一条数据
//        /// </summary>
//        /// <param name="model">The model.</param>
//        /// <returns></returns>
//        public static int Add(OperationAuditManageInfo model)
//        {
//            return dal.Add(model);
//        }

//        /// <summary>
//        /// 更新一条数据
//        /// </summary>
//        /// <param name="model">The model.</param>
//        public static void Update(OperationAuditManageInfo model)
//        {
//            dal.Update(model);
//        }
//        */

//        /// <summary>
//        /// 删除一条数据
//        /// </summary>
//        /// <param name="Id">The id.</param>
//        public static void Delete(int Id)
//        {
//            dal.Delete(Id);
//        }

//        /// <summary>
//        /// 得到一个对象实体
//        /// </summary>
//        /// <param name="Id">The id.</param>
//        /// <returns></returns>
//        public static OperationAuditManageInfo GetModel(int Id)
//        {
//            return TransformModel(dal.GetModel(Id));
//        }

//        /// <summary>
//        /// 根据部门ID获得一个对象实体
//        /// </summary>
//        /// <param name="DepId">The dep id.</param>
//        /// <returns></returns>
//        public static OperationAuditManageInfo GetModelByDepId(int DepId)
//        {
//            return TransformModel(dal.GetModelByDepId(DepId));
//        }

//        public static OperationAuditManageInfo GetModelByDepId(int DepId,System.Data.SqlClient.SqlConnection conn,System.Data.SqlClient.SqlTransaction trans)
//        {
//            return TransformModel(dal.GetModelByDepId(DepId));
//        }

//        public static OperationAuditManageInfo TransformModel(ESP.Framework.Entity.OperationAuditManageInfo oldModel)
//        {
//            if (oldModel == null)
//                return null;
//            OperationAuditManageInfo newModel = new OperationAuditManageInfo();
//            newModel.CEOId = oldModel.CEOId;
//            newModel.CEOName = oldModel.CEOName;
//            newModel.DepId = oldModel.DepId;
//            newModel.DirectorId = oldModel.DirectorId;
//            newModel.DirectorName = oldModel.DirectorName;
//            newModel.FAId = oldModel.FAId;
//            newModel.FAName = oldModel.FAName;
//            newModel.Id = oldModel.Id;
//            newModel.ManagerId = oldModel.ManagerId;
//            newModel.ManagerName = oldModel.ManagerName;
//            return newModel;
//        }

//        /// <summary>
//        /// 获得数据列表
//        /// </summary>
//        /// <param name="strWhere">The STR where.</param>
//        /// <returns></returns>
//        public static DataSet GetList(string strWhere)
//        {
//            return dal.GetList(strWhere);
//        }

//        /// <summary>
//        /// 获得数据列表
//        /// </summary>
//        /// <param name="strWhere">The STR where.</param>
//        /// <returns></returns>
//        public static List<OperationAuditManageInfo> GetModelList(string strWhere)
//        {
//            DataSet ds = dal.GetList(strWhere);
//            List<OperationAuditManageInfo> modelList = new List<OperationAuditManageInfo>();
//            int rowsCount = ds.Tables[0].Rows.Count;
//            if (rowsCount > 0)
//            {
//                OperationAuditManageInfo model;
//                for (int n = 0; n < rowsCount; n++)
//                {
//                    model = new OperationAuditManageInfo();
//                    if (ds.Tables[0].Rows[n]["Id"].ToString() != "")
//                    {
//                        model.Id = int.Parse(ds.Tables[0].Rows[n]["Id"].ToString());
//                    }
//                    if (ds.Tables[0].Rows[n]["DepId"].ToString() != "")
//                    {
//                        model.DepId = int.Parse(ds.Tables[0].Rows[n]["DepId"].ToString());
//                    }
//                    if (ds.Tables[0].Rows[n]["DirectorId"].ToString() != "")
//                    {
//                        model.DirectorId = int.Parse(ds.Tables[0].Rows[n]["DirectorId"].ToString());
//                    }
//                    model.DirectorName = ds.Tables[0].Rows[n]["DirectorName"].ToString();
//                    if (ds.Tables[0].Rows[n]["ManagerId"].ToString() != "")
//                    {
//                        model.ManagerId = int.Parse(ds.Tables[0].Rows[n]["ManagerId"].ToString());
//                    }
//                    model.ManagerName = ds.Tables[0].Rows[n]["ManagerName"].ToString();
//                    if (ds.Tables[0].Rows[n]["CEOId"].ToString() != "")
//                    {
//                        model.CEOId = int.Parse(ds.Tables[0].Rows[n]["CEOId"].ToString());
//                    }
//                    model.CEOName = ds.Tables[0].Rows[n]["CEOName"].ToString();
//                    modelList.Add(model);
//                }
//            }
//            return modelList;
//        }

//        /// <summary>
//        /// 获得数据列表
//        /// </summary>
//        /// <returns></returns>
//        public static DataSet GetAllList()
//        {
//            return dal.GetList("");
//        }

//        /// <summary>
//        /// 获得总监的sysids
//        /// </summary>
//        /// <returns></returns>
//        public static string GetDirectorIds()
//        {
//            return dal.GetDirectorIds();
//        }

//        /// <summary>
//        /// 获得总经理的sysids
//        /// </summary>
//        /// <returns></returns>
//        public static string GetManagerIds()
//        {
//            return dal.GetManagerIds();
//        }

//        /// <summary>
//        /// 获得CEO的sysids
//        /// </summary>
//        /// <returns></returns>
//        public static string GetCEOIds()
//        {
//            return dal.GetCEOIds();
//        }

//        /// <summary>
//        /// 判断当前登录人是否为总经理
//        /// </summary>
//        /// <param name="curUserId"></param>
//        /// <returns></returns>
//        public static bool GetCurrentUserIsManager(string curUserId)
//        {
//            string manageids = GetManagerIds();
//            if (manageids.IndexOf(curUserId) > 0)
//            {
//                return true;
//            }
//            return false;
//        }

//        /// <summary>
//        /// 判断当前登录人是否为总监
//        /// </summary>
//        /// <param name="curUserId"></param>
//        /// <returns></returns>
//        public static bool GetCurrentUserIsDirector(string curUserId)
//        {
//            string directorids = GetDirectorIds();
//            if (directorids.IndexOf(curUserId) > 0)
//            {
//                return true;
//            }
//            return false;
//        }

//        #endregion  成员方法
//    }
//}